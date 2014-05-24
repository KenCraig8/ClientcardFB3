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
        string emailBody = "";

        public string EmailBody
        {
            get
            {
                return emailBody;
            }
        }

        public EmailBodyInputForm(string reportName)
        {
            InitializeComponent();
            label1.Text = "Please Enter The Body Of The Email For " + reportName + ":";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EmailBodyInputForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            emailBody = tbEmailBody.Text;
        }
    }
}
