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
    public partial class EmailBodyInputForm : Form
    {
        EmailInfo emailInfo;
        bool canceled = false;

        public bool Canceled
        {
            get
            {
                return canceled;
            }
        }

        public EmailBodyInputForm(EmailInfo inEmailInfo, string reportName)
        {
            InitializeComponent();

            lblBody.Text = "Please Enter The Body Of The Email For " + reportName + ":";
            //use emailInfo to fill in the preset feilds
            emailInfo = inEmailInfo;
            tbTo.Text = emailInfo.to;
            tbFrom.Text = emailInfo.from;
            tbSubject.Text = emailInfo.subject;
            tbAttach.Text = emailInfo.attachmentPath;
        }

        private void EmailBodyInputForm_FormClosing(object sender, FormClosingEventArgs e){ }

        private void btnOK_Click(object sender, EventArgs e)
        {
            canceled = false;

            //update the contents of the email
            emailInfo.body = tbEmailBody.Text;
            emailInfo.to = tbTo.Text;
            emailInfo.subject = tbSubject.Text;

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            canceled = true;
            this.Close();
        }
    }
}
