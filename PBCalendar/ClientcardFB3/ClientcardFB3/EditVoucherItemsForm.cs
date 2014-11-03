using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ClientcardFB3
{
    public partial class EditVouchersItemForm : Form
    {
        SqlCommand command;
        SqlConnection conn = new SqlConnection(CCFBGlobal.connectionString);
        SqlDataReader reader;
        string desc = "";
        int ID = -1;
        bool loadingData = false;

        public EditVouchersItemForm()
        {
            InitializeComponent();

            loadingData = true;
            CCFBGlobal.InitCombo(cboVoucherType, CCFBGlobal.parmTbl_VoucherType);

            loadList(0);
            fillForm();
            loadingData = false;
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                openConnection();
                command = new SqlCommand("Insert Into VoucherItems (Description, VoucherType, Inactive) "
                    + "Values('NEW ITEM' , " + cboVoucherType.SelectedValue + ", 0)", conn);
                command.ExecuteNonQuery();
                closeConnection();
            }
            catch(SqlException ex)
            {
                CCFBGlobal.appendErrorToErrorReport("SQL Command = " + command.CommandText, ex.GetBaseException().ToString(),
                    CCFBGlobal.serverName);
                closeConnection();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lvVouchers.SelectedItems[0] != null)
                deleteItem();
        }

        private void deleteItem()
        {
            try
            {
                openConnection();
                command = new SqlCommand("Select * From VoucherLog where VoucherItemID=" + ID.ToString(), conn);
                reader = command.ExecuteReader();

                //int found = command.ExecuteNonQuery();
                //found > 0
                if (reader.HasRows)
                {
                    reader.Close();
                    command = new SqlCommand("Update VoucherItems set Inactive = 1 where UID = " + ID.ToString(), conn);
                    command.ExecuteNonQuery();
                }
                else
                {
                    reader.Close();
                    command = new SqlCommand("Delete From VoucherItems where UID = " + ID.ToString(), conn);
                    command.ExecuteNonQuery();
                }
                
                closeConnection();
                loadList(0);
            }
            catch(System.Exception ex) 
            {
                closeConnection();
                CCFBGlobal.appendErrorToErrorReport("Command Text = " + command.CommandText, ex.GetBaseException().ToString(),
                    CCFBGlobal.serverName);
            }
        }

        private void fillForm()
        {
            if (lvVouchers.SelectedItems.Count > 0)
            {
                loadingData = true;
                tbDesc.Text = lvVouchers.SelectedItems[0].Text;
                cboVoucherType.SelectedValue = lvVouchers.SelectedItems[0].SubItems[3].Text;
                ID = Convert.ToInt32(lvVouchers.SelectedItems[0].SubItems[2].Text);
                loadingData = false;
            }
        }

        private void loadList(int row)
        {
            try
            {
                lvVouchers.Items.Clear();
                openConnection();
                command = new SqlCommand("Select vi.*, vt.[Type] From VoucherItems vi Inner Join parm_VoucherType vt on vt.ID=vi.vouchertype "
                    + "Where vi.inactive=0", conn);
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    ListViewItem lvi;
                    while (reader.Read())
                    {
                        lvi = new ListViewItem(reader.GetValue(1).ToString());
                        lvi.SubItems.Add(reader.GetValue(4).ToString());
                        lvi.SubItems.Add(reader.GetValue(0).ToString());
                        lvi.SubItems.Add(reader.GetValue(2).ToString());
                        lvVouchers.Items.Add(lvi);
                    }
                }
                closeConnection();
                if (lvVouchers.Items.Count > row)
                    lvVouchers.Items[row].Selected = true;
                else if(lvVouchers.Items.Count >0)
                    lvVouchers.Items[0].Selected = true;

                Application.DoEvents();
            }
            catch { }
        }

        private void openConnection()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
        }

        private void closeConnection()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        private void lvVouchers_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillForm();
        }

        private void cboVoucherType_SelectedValueChanged(object sender, EventArgs e)
        {
            update();
        }

        private void update()
        {
            if (loadingData == false)
            {
                try
                {
                    openConnection();

                    command = new SqlCommand("Update VoucherItems set Description ='" + tbDesc.Text + "', VoucherType=" + cboVoucherType.SelectedValue
                    + " where UID = " + ID.ToString(), conn);
                    command.ExecuteNonQuery();

                    closeConnection();
                    loadList(lvVouchers.SelectedItems[0].Index);
                }
                catch (System.Exception ex)
                {
                    closeConnection();
                    CCFBGlobal.appendErrorToErrorReport("Command Text = " + command.CommandText, ex.GetBaseException().ToString(),
                        CCFBGlobal.serverName);
                }
            }
        }

        private void tbDesc_Enter(object sender, EventArgs e)
        {
            desc = tbDesc.Text;
        }

        private void tbDesc_Leave(object sender, EventArgs e)
        {
            if (desc != tbDesc.Text)
                update();
        }
    }
}
