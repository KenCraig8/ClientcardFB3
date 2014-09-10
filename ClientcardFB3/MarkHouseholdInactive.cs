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
    public partial class MarkHouseholdInactive : Form
    {
        bool needToRefresh = false;

        public MarkHouseholdInactive()
        {
            InitializeComponent();
        }

        private void MarkHouseholdInactive_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Value = CCFBGlobal.CurrentFiscalStartDate().AddYears(-2);
            SetDisplay();
        }

        private void SetDisplay()
        {
            dateTimePicker1.Enabled = (rdoBtn2.Checked == true);
        }

        private void rdoBtn_CheckedChanged(object sender, EventArgs e)
        {
            SetDisplay();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            int nbrRows = 0;
            if (rdoBtn1.Checked == true)
            {
                nbrRows = CCFBGlobal.executeQuery("Update Household Set Inactive = 1 Where Inactive = 0");
            }
            else
            {
                nbrRows = CCFBGlobal.executeQuery("Update Household Set Inactive = 1 Where InActive = 0 AND LatestService < '"
                    + dateTimePicker1.Value.ToShortDateString() + "'");
            }
            if (nbrRows != 0)
            {
                MessageBox.Show(nbrRows.ToString() + " households were marked as inactive.", "Mark Households Inactive Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
                needToRefresh = true;
            }
            this.Hide();
        }

        public bool NeedToRefresh
        {
            get { return needToRefresh; }
        }
    }
}
