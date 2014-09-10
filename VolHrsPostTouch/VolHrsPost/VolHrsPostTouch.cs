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

        private void btnNBR_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            switch (tbFocus)
            {
                case 0:
                    tbVolID.Text += btn.Text;
                    break;
                case 1:
                    tbTimeIN.Text += btn.Text;
                    break;
                case 2:
                    tbTimeOUT.Text += btn.Text;
                    break;
                case 3:
                    tbHours.Text += btn.Text;
                    break;
                default:
                    break;
            }
        }

        private void btnENTER_Click(object sender, EventArgs e)
        {
            switch (tbFocus)
            {
                case 0:
                    if (LoadVolunteer(tbVolID.Text, dtpTrxDate.Value.ToShortDateString()) == true)
                    {
                        btnFinish.Enabled = true;
                    }
                    break;
                case 1:
                    break;
                case 2:
                    tbHours.Tag = clsVolHrs.TotalHours(tbTimeIN.Text, tbTimeOUT.Text);
                    formattedHours();
                    btnFinish.Enabled = true;
                    break;
                case 3:
                    break;
                default:
                    break;
            }
        }

        private void tbVolID_Enter(object sender, EventArgs e)
        {
            tbFocus = 0;
        }

        private void initDisplay()
        {
            tbVolID.Text = "";
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

        private void btnPM_Click(object sender, EventArgs e)
        {
            switch (tbFocus)
            {
                case 1:
                    if (tbTimeIN.Text.Contains("PM") == false)
                    {
                        if (tbTimeIN.Text.EndsWith(" ") == false)
                            tbTimeIN.Text += " ";
                        tbTimeIN.Text += "PM";
                    }
                    break;
                case 2:
                    if (tbTimeOUT.Text.Contains("PM") == false)
                    {
                        if (tbTimeOUT.Text.EndsWith(" ") == false)
                            tbTimeOUT.Text += " ";
                        tbTimeOUT.Text += "PM";
                    }
                    break;
                default:
                    break;
            }
        }

        private void btnAM_Click(object sender, EventArgs e)
        {
            switch (tbFocus)
            {
                case 1:
                    if (tbTimeIN.Text.Contains("AM") == false)
                    {
                        tbTimeIN.Text += "AM";
                    }
                    break;
                case 2:
                    if (tbTimeOUT.Text.Contains("AM") == false)
                    {
                        tbTimeOUT.Text += "AM";
                    }
                    break;
                default:
                    break;
            }
        }

        private void btnColon_Click(object sender, EventArgs e)
        {
            switch (tbFocus)
            {
                case 1:
                    if (tbTimeIN.Text.Contains(":") == false)
                    {
                        tbTimeIN.Text += ":";
                    }
                    break;
                case 2:
                    if (tbTimeOUT.Text.Contains(":") == false)
                    {
                        tbTimeOUT.Text += ":";
                    }
                    break;
                default:
                    break;
            }
        }

        private void btnBS_Click(object sender, EventArgs e)
        {
            string tmp;
            TextBox tb = null;
            switch (tbFocus)
            {
                case 0:
                    tb = tbVolID;
                    break;
                case 1:
                    tb = tbTimeIN;
                    break;
                case 2:
                    tb = tbTimeOUT;
                    break;
                case 3:
                    tb = tbHours;
                    break;
                default:
                    break;
            }
            tmp = tb.Text;
            if (tmp.Length > 1)
            {
                tb.Text = tmp.Substring(0, tmp.Length - 1);
            }
            else
            {
                tb.Text = "";
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            tbFocus = 0;
            initDisplay();
            tbTimeIN.Tag = DateTime.Now;
            tbTimeOUT.Tag = DateTime.Now;
            tbVolID.Focus();
        }

        private bool LoadVolunteer(string testVolId, string testDate)
        {
            if (testVolId != "")
            {
                if (clsVols.open(Convert.ToInt32(testVolId)) == true)
                {
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
                    MessageBox.Show("I cannot find this Volunteer\r\nID: " + testVolId, "Find Volunteer Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return false;
        }

        private void btnFinish_Click(object sender, EventArgs e)
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

        private void VolLogMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmLogIn.Close();
        }

        private void formattedHours()
        {
            tbHours.Text = ((double)tbHours.Tag).ToString("F");
        }

        private void btnSpace_Click(object sender, EventArgs e)
        {
            switch (tbFocus)
            {
                case 0:
                    tbVolID.Text += " ";
                    break;
                case 1:
                    tbTimeIN.Text += " ";
                    break;
                case 2:
                    tbTimeOUT.Text += " ";
                    break;
                case 3:
                    tbHours.Text += " ";
                    break;
                default:
                    break;
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            initDisplay();
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

        public void setVol(string volid)
        {
            initDisplay();
            tbFocus = 0;
            tbVolID.Text = volid;
            if (LoadVolunteer(volid, dtpTrxDate.Value.ToShortDateString()) == true)
            {
                btnFinish.Enabled = true;
            }
        }

        private void tbVolID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                if (LoadVolunteer(tbVolID.Text, dtpTrxDate.Value.ToShortDateString()) == true)
                {
                    btnFinish.Enabled = true;
                }
            }
        }
    }
}
