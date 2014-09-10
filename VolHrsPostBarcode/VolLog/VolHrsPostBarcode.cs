using System;
using System.Windows.Forms;

namespace ClientcardFB3
{
    public partial class VolLogMain : Form
    {
        LoginForm frmLogIn;
        VolList frmVolList;
        int tbFocus = 0;
        bool modeTimeIN = true;
        Volunteers clsVols = new Volunteers(CCFBGlobal.connectionString);
        VolunteerHours clsVolHrs = new VolunteerHours(CCFBGlobal.connectionString);

        public VolLogMain(LoginForm loginform)
        {
            frmLogIn = loginform;
            InitializeComponent();
            //CCFBGlobal.LoadTypes();
            //CCFBPrefs.Init();
            initDisplay();
            
            frmVolList = new VolList(this);
        }

        private void tbVolID_Enter(object sender, EventArgs e)
        {
            tbFocus = 0;
        }

        private void initDisplay()
        {
            tbFBIDNbr.Text = "";
            lblName.Text = "";
            tbTimeIN.Text = "";
            tbTimeOUT.Text = "";
            tbHours.Text = "";
            btnFinish.Enabled = false;
        }

        private void tbTimeIN_Enter(object sender, EventArgs e)
        {
            tbFocus = 1;
        }

        private void tbTimeOUT_Enter(object sender, EventArgs e)
        {
            tbFocus = 2;
        }

        private void tbHours_Enter(object sender, EventArgs e)
        {
            tbFocus = 3;
        }


        private bool LoadVolunteer(string testFBIDNbr, string testDate)
        {
            string testVolId = "";
            if (testFBIDNbr != "")
            {
                clsVols.openWhere(" WHERE FBIDNbr = '" + testFBIDNbr + "'");
                if (clsVols.RowCount > 0)
                {
                    testVolId = clsVols.ID.ToString();
                    lblName.Text = clsVols.Name;
                    clsVolHrs.openWhere("WHERE VolID = " + testVolId + " AND TrxDate = '" + testDate + "'");
                    if (clsVolHrs.IsValid == false) //no record means checkin
                    {
                        modeTimeIN = true;
                        tbTimeIN.Tag = DateTime.Now.ToShortTimeString();
                        tbTimeIN.Text = tbTimeIN.Tag.ToString();
                    }
                    else
                    {
                        modeTimeIN = false;
                        tbTimeIN.Tag = clsVolHrs.VolTimeIn;
                        string tmp = "01/01/2010 " + tbTimeIN.Tag;
                        tbTimeIN.Text = Convert.ToDateTime(tmp).ToShortTimeString();
                        if (clsVolHrs.VolTimeOut == "")
                        {
                            tbTimeOUT.Tag = DateTime.Now.ToShortTimeString();
                        }
                        else
                        {
                            tbTimeOUT.Tag = clsVolHrs.VolTimeOut;
                        }
                        tmp = "01/01/2010 " + tbTimeOUT.Tag;
                        tbTimeOUT.Text = Convert.ToDateTime(tmp).ToShortTimeString();
                        tbHours.Tag = clsVolHrs.TotalHours(tbTimeIN.Tag.ToString(), tbTimeOUT.Tag.ToString());
                        formattedHours();
                    }
                    return true;
                }
                else
                {
                    MessageBox.Show("I cannot find this Volunteer\r\nID Number: " + testFBIDNbr, "Find Volunteer Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return false;
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            postHours();
        }

        private void VolLogMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmLogIn.Close();
        }

        private void formattedHours()
        {
            tbHours.Text = ((double)tbHours.Tag).ToString("F");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            initDisplay();
            tbFBIDNbr.Focus();
        }

        private void dtpTrxDate_ValueChanged(object sender, EventArgs e)
        {
            frmVolList.refresh(dtpTrxDate.Value.ToShortDateString());
        }

        private void btnToggleList_Click(object sender, EventArgs e)
        {
            frmVolList.Visible = !frmVolList.Visible;
        }

        private void VolLogMain_Load(object sender, EventArgs e)
        {
            dtpTrxDate.MaxDate = DateTime.Today;
            frmVolList.refresh(dtpTrxDate.Value.ToShortDateString());
            frmVolList.Show();
        }

        public void setVol(string fbidnbr)
        {
            initDisplay();
            tbFocus = 0;
            tbFBIDNbr.Text = fbidnbr;
            if (LoadVolunteer(fbidnbr, dtpTrxDate.Value.ToShortDateString()) == true)
            {
                btnFinish.Enabled = true;
            }
        }

        private void tbFBIDNbr_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                if (LoadVolunteer(tbFBIDNbr.Text.Trim(), dtpTrxDate.Value.ToShortDateString()) == true)
                {
                    postHours();
                }
            }
        }

        private void postHours()
        {
            btnFinish.Enabled = false;

            if (modeTimeIN == true)
            {
                clsVolHrs.InsertNewRow(clsVols.ID, dtpTrxDate.Value, 1, 0, tbTimeIN.Text, tbTimeOUT.Text);
            }
            else
            {
                clsVolHrs.NumVolHours = (float)Convert.ToDouble(tbHours.Text);
                clsVolHrs.VolTimeIn = tbTimeIN.Text;
                clsVolHrs.VolTimeOut = tbTimeOUT.Text;
                clsVolHrs.update();
            }
            frmVolList.refresh(dtpTrxDate.Value.ToShortDateString());
            timer1.Enabled = true;
        }

        private void btnFindVol_Click(object sender, EventArgs e)
        {
            if (LoadVolunteer(tbFBIDNbr.Text.Trim(), dtpTrxDate.Value.ToShortDateString()) == true)
            {
                btnFinish.Enabled = true;
            }
        }

        private void tbFBIDNbr_TextChanged(object sender, EventArgs e)
        {
            lblName.Text = "";
            tbTimeIN.Text = "";
            tbTimeOUT.Text = "";
            tbHours.Text = "";
            btnFinish.Enabled = false;
        }
    }
}
