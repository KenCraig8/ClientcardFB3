using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace ClientcardFB3
{
    public partial class AddNewVoucher : Form
    {
        SqlCommand command;
        SqlConnection conn = new SqlConnection(CCFBGlobal.connectionString);
        bool newItem = false;

        public bool NewItem
        {
            get
            {
                return newItem;
            }
        }

        public AddNewVoucher()
        {
            InitializeComponent();

            CCFBGlobal.InitCombo(cboVoucherType, CCFBGlobal.parmTbl_VoucherType);
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                command = new SqlCommand("Insert Into VoucherItems (Description, VoucherItem) "
                    + "Values(" + tbDesc.Text + ", " + cboVoucherType.SelectedValue + ")", conn);
                command.ExecuteNonQuery();
                conn.Close();
                newItem = true;
            }
            catch(SqlException ex)
            {
                newItem = false;
                CCFBGlobal.appendErrorToErrorReport("SQL Command = " + command.CommandText, ex.GetBaseException().ToString(),
                    CCFBGlobal.serverName);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
