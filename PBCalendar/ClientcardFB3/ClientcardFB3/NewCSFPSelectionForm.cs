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
        public NewCSFPSelectionForm(int id)
        {
            InitializeComponent();
            clsClient.open(id, true, false);
            loadList();
            dtpExpDate.Value = DateTime.Today.AddMonths(6);
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
                    lvi2.SubItems.Add(drow["Birthdate"].ToString());
                    lvi2.SubItems.Add(drow["ID"].ToString());
                    if ((bool)drow["CSFP"] == true)
                        lvi2.Checked = true;

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
                dtpExpDate.Visible = false;
                label5.Visible = false;
            }
        }

        private void lvHHMems_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            clsClient.clsHHmem.CSFP = e.Item.Checked;
        }

        private void dtpExpDate_Leave(object sender, EventArgs e)
        {
            clsClient.clsHHmem.CSFPExpiration = dtpExpDate.Value;
        }

        private void lvHHMems_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvHHMems.FocusedItem != null)
                clsClient.clsHHmem.find(Convert.ToInt32(lvHHMems.FocusedItem.SubItems[3].Text));
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            clsClient.clsHHmem.update();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
