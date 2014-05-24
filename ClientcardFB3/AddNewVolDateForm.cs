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
    public partial class AddNewVolDateForm : Form
    {
        DateTime newTrxDate = new DateTime(1900, 1, 1);
        public AddNewVolDateForm()
        {
            InitializeComponent();

            lblEnterVolHrs.Text = "Enter Date For Volunteer Hours \r\n(mm/dd/yyyy)";

        }

        public DateTime TrxDate
        {
            get { return Convert.ToDateTime(newTrxDate.ToShortDateString()); }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                newTrxDate = dtpNewDate.Value;
                this.Hide();
               
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Your Date Was Not In Correct Format \r\n"
                    + "Please Re-Enter The Date And Try Again");
            }
            
        }
    }
}
