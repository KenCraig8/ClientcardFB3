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
    public partial class AddNewEmailRecipientForm : Form
    {
        EmailRecipients clsEmailAddresses;
        MonthlyReportPrefrencesForm frmEmailRecpt;

        public AddNewEmailRecipientForm(EmailRecipients clsEmailAddressesIn, 
            MonthlyReportPrefrencesForm frmEmailRecptIn)
        {
            InitializeComponent();

            clsEmailAddresses = clsEmailAddressesIn;
            frmEmailRecpt = frmEmailRecptIn;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            DataRow drow = clsEmailAddresses.DSet.Tables[0].NewRow();

            drow["RecipientName"] = tbName.Text;
            drow["EmailAddress"] = tbEmailAddress.Text;
            drow["CreatedBy"] = CCFBGlobal.dbUserName;
            drow["Created"] = DateTime.Now.ToString();

            clsEmailAddresses.DSet.Tables[0].Rows.Add(drow);

            clsEmailAddresses.insert();
            clsEmailAddresses.openAll();

            frmEmailRecpt.fillRecipients(true);

            this.Close();

        }
    }
}
