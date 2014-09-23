using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ClientcardFB3
{
    public partial class AddNewHouseholdMember : Form
    {
        Client clsClient;
        Zipcodes clsZipcodes;
        DataRow newRow;
        int newHHID = 0;
        int newHHMID = 0;
        bool canceled = false;
        CSDGSqlDataAccess csdgdataaccess;
        string dateIdVerified = CCFBGlobal.OURNULLDATE;
        bool bAlreadyHere = false;
        bool needToSetTab = false;
//        bool bTabPressed = false;
        /// <summary>
        /// Constructor for the form that either adds a Household or a HosueholdMember
        /// </summary>
        /// <param name="clsIn">The client class</param>
        /// <param name="AddMember">True is just adding a Household Memeber, False
        /// adds Household</param>
        public AddNewHouseholdMember(Client clsIn)
        {
            InitializeComponent();
            clsClient = clsIn;
            BackColor = CCFBGlobal.bkColorBaseEdit;
            csdgdataaccess = new CSDGSqlDataAccess(CCFBGlobal.connectionString);
            pnlCSFPInfo.Visible = CCFBPrefs.EnableCSFP;
            pnlEmployer.Visible =  CCFBPrefs.EnableWorksInArea;
            for (int i = 0; i < clsClient.clsHHmem.RowCount; i++)
            {
                clsClient.clsHHmem.SetRecord(i);
                if (clsClient.clsHHmem.HeadHH == true)
                {
                    tbLastName.Text = clsClient.clsHHmem.LastName;
                    break;
                }
            }
            btnAddNewMem.Enabled = false;
        }

        private void addHHMember(int hhid)
        {
            newRow = clsClient.clsHHmem.DSet.Tables[0].NewRow();
            newRow["NotIncludedInClientList"] = false;
            foreach (TextBox tb in pnlHH.Controls.OfType<TextBox>())
            {
                if (tb.Enabled == true)
                    newRow[tb.Tag.ToString()] = tb.Text;
            }

            if (chkEnterAge.Checked == true)
            {
                newRow["BirthDate"] = CCFBGlobal.calcBirthdateFromAge(Convert.ToInt32(tbAge.Text));
                newRow["UseAge"] = true;
                if (Convert.ToInt32(tbAge.Text) < 18)
                    newRow["NotIncludedInClientList"] = true;
            }
            else
            {
                newRow["Age"] = CCFBGlobal.calcAge(DateTime.Parse(tbBirthDate.Text), DateTime.Today);
                newRow["UseAge"] = false;
                if (Convert.ToInt32(newRow["Age"])<18)
                    newRow["NotIncludedInClientList"] = true;
            }
            newRow["AgeGroup"] = clsClient.clsHHmem.GetEFAPAgeGroup(Convert.ToInt32(newRow["Age"]));
            newRow["HouseholdID"] = clsClient.clsHH.ID;
            newRow["HeadHH"] = false;
            newRow["Inactive"] = false;
            newRow["SpecialDiet"] = chkSpecialDiet.Checked;
            newRow["WorksInArea"] = chkWorksInArea.Checked;
            newRow["VolunteersAtFoodBank"] = false;
            //newRow["SpecialDiet"] = false;
            newRow["UserFlag0"] = chkUserFlag0.Checked;
            newRow["UserFlag1"] = chkUserFlag1.Checked;
            newRow["CSFP"] = chkCSFP.Checked;
            newRow["CSFPRoute"] = 0;
            newRow["CSFPExpiration"] = CCFBGlobal.ValidDate(tbExpires.Text);
            newRow["IsDisabled"] = chkDissabled.Checked;
            newRow["Inactive"] = false;
            newRow["Language"] = 0;
            //newRow["UserFlag0"] = 0;
            //newRow["UserFlag1"] = 0;
            newRow["CreatedBy"] = CCFBGlobal.dbUserName;
            newRow["Created"] = DateTime.Now;

            clsClient.clsHHmem.DSet.Tables[0].Rows.Add(newRow);

            if (clsClient.clsHHmem.insertMember() == true)
            {
                createNewHHMDemographics();
                clsClient.calcAllHHMemAges(Convert.ToDateTime( CCFBGlobal.DefaultServiceDate));
            }
        }
        public bool Canceled
        {
            get
            {
                return canceled;
            }
        }

        public int HHID
        {
            get { return newHHID; }
        }

        public int HHMID
        {
            get { return newHHMID; }
        }

        private void tbCancel_Click(object sender, EventArgs e)
        {
            canceled = true;
            this.Close();
        }

        private void btnAddNewMem_Click(object sender, EventArgs e)
        {
            if (tbAge.Text.Trim() == "" && tbBirthDate.Text.Trim() == "")
            {
                MessageBox.Show("Cannot Save When Both Age And Birthdate Are Blank", this.Text);
            }
            else if (lblDateError.Visible == true && chkEnterAge.Checked == false)
            {
                MessageBox.Show("Cannot Save With Invalid Birthdate", this.Text);
                tbBirthDate.Focus();
            }
            else
            {
                if (clsClient.clsHHmem.nameExists(tbLastName.Text,tbFirstName.Text, tbBirthDate.Text) == false || CCFBPrefs.AllowDuplicateMemberNames == true)
                {
                    addHHMember(clsClient.clsHH.ID);
                    this.Visible = false;
                }
                else
                {
                    MessageBox.Show(tbFirstName.Text + " " + tbLastName.Text + "\r\nAlready Exists In Household Members Table",
                        this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    tbFirstName.Focus();
                }
            }
        }

        private void createNewHHMDemographics()
        {
            clsClient.clsHHmem.DRowHhm = clsClient.clsHHmem.DSet.Tables[0].Rows[
                clsClient.clsHHmem.DSet.Tables[0].Rows.Count - 1];

            newHHMID = clsClient.clsHHmem.ID;
            newRow = clsClient.clsHHmem.DSet.Tables[1].NewRow();
            newRow["MilitaryService"] = 0;
            newRow["DischargeStatus"] = 0;
            newRow["HispanicLatino"] = 2;
            newRow["RefugeeImmigrant"] = 2;
            newRow["LimitedEnglish"] = 2;
            newRow["PartneredMarried"] = 2;
            newRow["LongTermHomeless"] = 2;
            newRow["ChronicallyHomeless"] = 2;
            newRow["Employed"] = 2;
            newRow["EmplolymentStatus"] = 0;
            newRow["AmericanIndian"] = false;
            newRow["AlaskaNative"] = false;
            newRow["IndigenousToAmericas"] = false;
            newRow["AsianIndian"] = false;
            newRow["Cambodian"] = false;
            newRow["Chinese"] = false;
            newRow["Filipino"] = false;
            newRow["Japanese"] = false;
            newRow["Korean"] = false;
            newRow["Vietnamese"] = false;
            newRow["OtherAsian"] = false;
            newRow["IndigenousAfricanBlack"] = false;
            newRow["AfricanAmericanBlack"] = false;
            newRow["OtherBlack"] = false;
            newRow["HawaiianNative"] = false;
            newRow["Polynesian"] = false;
            newRow["Micronesian"] = false;
            newRow["OtherPacificIslander"] = false;
            newRow["ArabIranianMiddleEastern"] = false;
            newRow["OtherWhiteCaucasian"] = false;
            newRow["EthnicOther"] = false;
            newRow["EthnicUnknown"] = false;
            newRow["EducationLevel"] = 0;
            newRow["ID"] = newHHMID;

            clsClient.clsHHmem.DSet.Tables[1].Rows.Add(newRow);
            clsClient.clsHHmem.insertDemographics();
        }

        private void chkEnterAge_CheckedChanged(object sender, EventArgs e)
        {
            tbAge.Enabled = chkEnterAge.Checked;
            tbBirthDate.Enabled = (chkEnterAge.Checked == false);
            if (chkEnterAge.Checked == true)
                lblDateError.Visible = false;
        }

        private bool FilllvwHHMByBirthdate(string birthdate)
        {
            bool bExactMatch = false;
            lvwHHMByBirthdate.Items.Clear();
            tpgSameBirthDate.Text = "Loading ...";
            Application.DoEvents();
            lblDupHHMError.Visible = false;
            //Font ft = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            string sqlText = "SELECT LastName, FirstName, hhm.HouseholdID HHId, hhm.Inactive HHMInactive"
                           + ", hh.name, hh.address, hh.City, hh.Zipcode, hh.Inactive HHInactive"
                           + " FROM HouseholdMembers hhm INNER JOIN Household hh ON hhm.HouseholdID = hh.ID"
                           + " WHERE Birthdate = '" + birthdate + "' ORDER BY hh.Inactive, hh.name";
            DataTable dtblwrk = csdgdataaccess.TransferDataToLocalDataTable(sqlText);
            if (dtblwrk.Rows.Count > 0)
            {
                tpgSameBirthDate.Text = " [ " + dtblwrk.Rows.Count.ToString() + " ] " + tpgSameBirthDate.Tag.ToString();
                lvwHHMByBirthdate.Visible = true;
                lvwHHMByBirthdate.BackColor = Color.Ivory;

                foreach (DataRow drow in dtblwrk.Rows)
                {
                    ListViewItem lvItm = new ListViewItem(drow.Field<string>("LastName"));
                    lvItm.SubItems.Add(drow.Field<string>("FirstName"));

                    lvItm.SubItems.Add(CCFBGlobal.IsInactiveString(Convert.ToBoolean(drow["HHMInactive"])));
                    lvItm.SubItems.Add(drow.Field<string>("Name"));
                    lvItm.SubItems.Add(drow.Field<string>("Address"));
                    lvItm.SubItems.Add(drow.Field<string>("City"));
                    lvItm.SubItems.Add(drow.Field<string>("ZipCode"));
                    lvItm.SubItems.Add(Convert.ToInt32(drow["HHId"]).ToString());
                    lvItm.SubItems.Add(CCFBGlobal.IsInactiveString(Convert.ToBoolean(drow["HHInactive"])));
                    if (Convert.ToBoolean(drow["HHInactive"]) == true)
                    {
                        //lvItm.Font = ft;
                        //for (int i = 3; i < lvwHHMByBirthdate.Columns.Count; i++)
                        //{
                        //    lvItm.SubItems[i].Font = ft;
                        //    lvItm.SubItems[i].ForeColor = Color.Maroon;
                        //}
                        lvItm.ForeColor = Color.Maroon;
                    }
                    if (nameMatches(1, drow) == true)
                    {

                        bExactMatch = true;
                        lblDupHHMError.Visible = true;
                        lvItm.BackColor = Color.LightPink;
                        btnAddNewMem.Enabled = false;
                    }
                    else
                    {
                        btnAddNewMem.Enabled = (lblDupHHError.Visible == false);
                    }
                    lvwHHMByBirthdate.Items.Add(lvItm);
                }
            }
            else
            {
                tpgSameBirthDate.Text = "";
                lvwHHMByBirthdate.Visible = false;
            }
            return bExactMatch;
        }

        private void initTextboxes()
        {
            foreach (TextBox tb in pnlHH.Controls.OfType<TextBox>())
            {
                tb.Text = "";
            }
        }

        private void tbLastName_Leave(object sender, EventArgs e)
        {
            string tmp = tbLastName.Text.Trim();
            if (tmp.Length > 1)
            {
                if (tmp.Substring(0, 1) != tmp.Substring(0, 1).ToUpper())
                    tbLastName.Text = tmp.Substring(0, 1).ToUpper() + tmp.Substring(1);
                else
                    tbLastName.Text = tmp;
                TestHHMName(0, lvwLastName, tpgLastName);
            }
        }

        private void tbFirstName_Leave(object sender, EventArgs e)
        {
            if (bAlreadyHere == false)
            {
                bAlreadyHere = true;
                string tmp = tbFirstName.Text.Trim();
                lblDupHHError.Visible = false;
                if (tmp != "")
                {
                    if (tmp.Substring(0, 1) != tmp.Substring(0, 1).ToUpper())
                        if (tmp.Length > 1)
                            tbFirstName.Text = tmp.Substring(0, 1).ToUpper() + tmp.Substring(1);
                        else
                            tbFirstName.Text = tmp.Substring(0, 1).ToUpper();
                    else
                        tbFirstName.Text = tmp;
                    if (tbLastName.Text.Length > 1 && tbFirstName.Text.Length > 0)
                    {
                        btnAddNewMem.Enabled = true;
                        if (TestHHMName(1, lvwPeople, tpgLastFirst) && CCFBPrefs.AllowDuplicateMemberNames == false)
                        {
                            btnAddNewMem.Enabled = false;
                            lblDupHHError.Visible = true;
                        }
                        else
                        {
                            btnAddNewMem.Enabled = true;
                        }
                    }
                }
                bAlreadyHere = false;
            }
        }

        private void tbBirthDate_Leave(object sender, EventArgs e)
        {
            if (tbBirthDate.Text.Trim() != "")
            {
                try
                {
                    DateTime dateTmp = Convert.ToDateTime(tbBirthDate.Text);
                    if (dateTmp > Convert.ToDateTime(CCFBGlobal.OURNULLDATE) && dateTmp < DateTime.Today.AddDays(1))
                    {
                        tbBirthDate.Text = dateTmp.ToShortDateString();
                        tbBirthDate.ForeColor = Color.Black;
                        lblDateError.Visible = false;
                        tbAge.Text = CCFBGlobal.calcAge(dateTmp, DateTime.Today).ToString();
                        FilllvwHHMByBirthdate(tbBirthDate.Text);
                    }
                    else
                    {
                        tbBirthDate.ForeColor = Color.DarkRed;
                        lblDateError.Visible = true;
                    }
                }
                catch
                {
                    tbBirthDate.ForeColor = Color.DarkRed;
                    lblDateError.Visible = true;
                }
            }
        }

        private void tbAge_Leave(object sender, EventArgs e)
        {
            if (tbAge.Enabled == true && tbAge.Text == "")
            {
                tbAge.Text = "0";
            }
        }

        private void tbAge_KeyDown(object sender, KeyEventArgs e)
        {
            CCFBGlobal.checkForIntOnKeyPress(e);
        }

        private void tbSex_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.M || e.KeyCode == Keys.F
                || e.KeyCode == Keys.T || e.KeyCode == Keys.U
                || e.KeyCode == Keys.O || e.KeyCode == Keys.Tab
                || e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
                e.SuppressKeyPress = false;
            else
                e.SuppressKeyPress = true;
        }

        private void tbSex_Leave(object sender, EventArgs e)
        {
            tbSex.Text = tbSex.Text.ToUpper();
        }

        //private bool TestForHousehold(int TestHHId)
        //{
        //    Household clsTmpHH = new Household(CCFBGlobal.connectionString);
        //    string sqlWhere = "Name = '" + CCFBGlobal.SQLApostrophe(tbLastName.Text) + ", " + CCFBGlobal.SQLApostrophe(tbFirstName.Text) + "'";
        //    if (TestHHId > 0)
        //        sqlWhere += " OR ID = " + TestHHId.ToString();
        //    clsTmpHH.openWhere(sqlWhere);
        //    if (clsTmpHH.RowCount > 0)
        //    {
        //        //lvwHouseholds.Items.Clear();
        //        //for (int i = 0; i < clsTmpHH.RowCount; i++)
        //        //{
        //        //    clsTmpHH.SetRecord(i);
        //        //    ListViewItem lvItm = new ListViewItem(clsTmpHH.Name);
        //        //    lvItm.SubItems.Add(clsTmpHH.Inactive.ToString());
        //        //    lvItm.SubItems.Add(clsTmpHH.Address.ToString());
        //        //    lvItm.SubItems.Add(clsTmpHH.City.ToString() + ", " + clsTmpHH.State.ToString() + " " + clsTmpHH.Zipcode.ToString());
        //        //    lvItm.SubItems.Add(clsTmpHH.HHMembersCount(clsTmpHH.ID).ToString());
        //        //    lvItm.SubItems.Add(clsTmpHH.ID.ToString());
        //        //    if (i % 2 == 0)
        //        //        lvItm.BackColor = Color.White;
        //        //    else
        //        //        lvItm.BackColor = Color.LightYellow;


        //        //    lvwHouseholds.Items.Add(lvItm);
        //        //}
        //        //spltcontLists.Panel1Collapsed = false;
        //        return true;
        //    }
        //    return false;
        //}

        private bool TestHHMName(int iMode, ListView lvwGrid, TabPage tpg)
        {
            int lvwIndex = -1;
            string sWhereClause;
            bool bIsExactMatch = false;
            lvwGrid.Items.Clear();
            tpg.Text = "Loading ..";
            Application.DoEvents();
            switch (iMode)
            {
                case 1:
                    sWhereClause ="SoundEx(LastName) = SoundEx('" + tbLastName.Text + "') AND SoundEx(FirstName) = SoundEx('" + tbFirstName.Text + "')";
                    break;
                default:
                    sWhereClause = "SoundEx(LastName) = SoundEx('" + tbLastName.Text + "')";
                    break;
            }
            //Font ft = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            string sqlText = "SELECT LastName, FirstName, hhm.HouseholdID HHId, hhm.Inactive HHMInactive, hhm.BirthDate"
                           + ", hh.name, hh.address, hh.City, hh.Zipcode, hh.Inactive HHInactive"
                           + ", (SELECT Count(*) FROM TrxLog WHERE HouseholdId = hhm.HouseholdId"
                           + " AND TrxDate Between '" + CCFBGlobal.CurrentFiscalStartDate() + "' AND '" + CCFBGlobal.CurrentFiscalEndDate() + "') NbrSvcs"
                           + " FROM HouseholdMembers hhm INNER JOIN Household hh ON hhm.HouseholdID = hh.ID"
                           + " WHERE " + sWhereClause + " ORDER BY hh.Name, hhm.LastName, hhm.FirstName";
                           
            DataTable dtblwrk = csdgdataaccess.TransferDataToLocalDataTable(sqlText);
            if (dtblwrk.Rows.Count > 0)
            {
                tpg.Text = " [ " + dtblwrk.Rows.Count.ToString() + " ] " + tpg.Tag.ToString();
                lvwGrid.Visible = true;
                lvwGrid.BackColor = Color.Ivory;

                foreach (DataRow drow in dtblwrk.Rows)
                {
                    ListViewItem lvItm = new ListViewItem(drow.Field<string>("LastName"));
                    lvItm.SubItems.Add(drow.Field<string>("FirstName"));
                    lvItm.SubItems.Add(CCFBGlobal.ValidDateString(drow["BirthDate"]));
                    //lvItm.SubItems.Add(CCFBGlobal.IsInactiveString(Convert.ToBoolean(drow["HHMInactive"])));
                    lvItm.SubItems.Add(drow.Field<string>("Name"));
                    lvItm.SubItems.Add(drow["NbrSvcs"].ToString());
                    lvItm.SubItems.Add(CCFBGlobal.IsInactiveString(Convert.ToBoolean(drow["HHInactive"])));
                    lvItm.SubItems.Add(drow.Field<string>("Address"));
                    lvItm.SubItems.Add(drow.Field<string>("City"));
                    lvItm.SubItems.Add(drow.Field<string>("ZipCode"));
                    lvItm.SubItems.Add(Convert.ToInt32(drow["HHId"]).ToString());
                    if (Convert.ToBoolean(drow["HHInactive"]) == true)
                    {
                        //lvItm.Font = ft;
                        //for (int i = 3; i < lvwGrid.Columns.Count; i++)
                        //{
                        //    lvItm.SubItems[i].Font = ft;
                        //    lvItm.SubItems[i].ForeColor = Color.Maroon;
                        //}
                        lvItm.ForeColor = Color.Maroon;
                    }
                    if (nameMatches(iMode, drow) == true)
                    {
                        lvItm.BackColor = Color.LightCoral;
                        if (lvwIndex < 0)
                        {
                            lvwIndex = lvwGrid.Items.Count;
                        }
                        bIsExactMatch = true;
                    }
                    lvwGrid.Items.Add(lvItm);
                    if (lvwIndex == lvwGrid.Items.Count - 1)
                    {
                        lvwGrid.Items[lvwIndex].Selected = true;
                        lvwGrid.Items[lvwIndex].EnsureVisible();
                    }
                    needToSetTab = true;
                }
            }
            else
            {
                tpg.Text = "no matches";
                needToSetTab = false;
            }

            //tabControl1.SelectedIndex = iMode;
            //Application.DoEvents();
            return bIsExactMatch;
        }

        private bool nameMatches(int iMode, DataRow drow)
        {
            bool result = false;
            switch (iMode)
            {
                case 1:
                    if (tbLastName.Text.ToUpper() == drow["LastName"].ToString().ToUpper() && tbFirstName.Text.ToUpper() == drow["FirstName"].ToString().ToUpper())
                    {
                        result = true;
                    }
                    break;
                default:
                    if (tbLastName.Text.ToUpper() == drow["LastName"].ToString().ToUpper())
                    {
                        result = true;
                    }
                    break;
            }
            return result;
        }

        private void chkEnterAge_Enter(object sender, EventArgs e)
        {
            if (needToSetTab == true)
            {
                needToSetTab = false;
                tabControl1.SelectedIndex = 1;
                chkEnterAge.Focus();
            }
        }
    }
}
