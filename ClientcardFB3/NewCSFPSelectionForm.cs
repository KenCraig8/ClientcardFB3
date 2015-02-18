using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ClientcardFB3
{
    public partial class NewCSFPSelectionForm : Form
    {
        Client clsClient = new Client(CCFBGlobal.connectionString);
        int currentMemberId = 0;
        public NewCSFPSelectionForm(int id)
        {
            InitializeComponent();
            clsClient.open(id, true, false);
            loadList();
            tbExpireDate.Text = DateTime.Today.AddMonths(6).ToShortDateString();
            CCFBGlobal.InitCombo(cboRoute, CCFBGlobal.parmTbl_CSFPRoutes);
        }

        private void loadList()
        {
            ListViewItem lvi2;
            DataRow drow ;
            for (int i = 0; i < clsClient.clsHHmem.RowCount; i++)
            {
                drow = clsClient.clsHHmem.DSet.Tables[0].Rows[i];
                if (drow.RowState != DataRowState.Deleted)
                {
                    //Create New item
                    lvi2 = new ListViewItem(drow["LastName"].ToString() + ", " + drow["FirstName"].ToString());

                    //Load subitems
                    lvi2.SubItems.Add(drow["Age"].ToString());
                    lvi2.SubItems.Add(CCFBGlobal.ValidDateString(drow["Birthdate"]));
                    lvi2.SubItems.Add(drow["ID"].ToString());
                    if ((bool)drow["CSFP"] == true)
                    {
                        lvi2.Checked = true;
                        lvi2.SubItems.Add(CCFBGlobal.ValidDateString(drow["CSFPExpiration"]));
                        lvi2.ForeColor = Color.DarkBlue;
                    }
                    else
                        lvi2.SubItems.Add("");

                    // Add Item to the list items                  
                    lvHHMems.Items.Add(lvi2);
                }
            }
            if (lvHHMems.Items.Count > 0)
            {
                lvHHMems.Items[0].Focused = true;
            }
            else
            {
                btnSave.Enabled = false;
                tbExpireDate.Visible = false;
                label5.Visible = false;
            }
        }

        private void lvHHMems_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            ListViewItem lvi = (ListViewItem)e.Item;
            clsClient.clsHHmem.find(rowMemberId(lvi));
            clsClient.clsHHmem.CSFP = e.Item.Checked;
        }

        private void lvHHMems_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvHHMems.FocusedItem != null)
                clsClient.clsHHmem.find(Convert.ToInt32(lvHHMems.FocusedItem.SubItems[3].Text));
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (tbExpireDate.Text.Trim().Length >0 && cboRoute.SelectedIndex != -1)
            {
                foreach (ListViewItem lvi in lvHHMems.Items)
                {
                    if (lvi.Checked && String.IsNullOrEmpty(lvi.SubItems[4].Text) == true)
                    {
                        clsClient.clsHHmem.find(rowMemberId(lvi));
                        clsClient.clsHHmem.SetDataValue("CSFPExpiration", tbExpireDate.Text);
                        clsClient.clsHHmem.SetDataValue("CSFPRoute", cboRoute.SelectedValue.ToString());
                    }
                }
                clsClient.clsHHmem.update(true);
                this.Close();
            }
            else
            {
                MessageBox.Show("Either The Route or Expiration Date Is Not Set.  Please Select A Date Or A Route And Try Again",
                    "FIELDS NOT SET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private int rowMemberId(ListViewItem lvi)
        {
            currentMemberId = Convert.ToInt32(lvi.SubItems[3].Text);
            return currentMemberId;
        }

        private void tbExpireDate_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                Convert.ToDateTime(tbExpireDate.Text);
            }
            catch (Exception ex)
            {
                if (MessageBox.Show("Date Not In Proper Format.  Please Enter Dates As MM/DD/YYYY", "FORMAT ERROR", MessageBoxButtons.RetryCancel,
                    MessageBoxIcon.Error) == System.Windows.Forms.DialogResult.Cancel)
                {
                    tbExpireDate.Text = "";
                }
                else
                {
                    e.Cancel = true;
                    tbExpireDate.SelectAll();
                    Application.DoEvents();
                }

            }
        }
    }
}
