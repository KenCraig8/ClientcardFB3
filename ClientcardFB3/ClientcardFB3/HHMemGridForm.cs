using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Text;
using System.Windows.Forms;

namespace ClientcardFB3
{
    public partial class HHMemGridForm : Form
    {
        Client clsClient;
        MainForm frmMain;
        SqlConnection conn = new SqlConnection(CCFBGlobal.connectionString);

        List<ComboBox> cboList = new List<ComboBox>();
        List<CheckBox> chkList = new List<CheckBox>();
        List<TextBox> tbList = new List<TextBox>();

        SqlCommand storedProcComm;

        int hhMemID;
        string stbOriValue = "";
        bool bchkOriValue = false;
        bool inEditMode = true;
        bool loadingInfo = true;
        //This array is used to control the visibility of the grid columns
        bool[] showColumn = new bool[66];
        
        #region Grid Column Names
        enum dgvColNames
        {
            ID,
            Inactive,
            LastName,
            FirstName,
            BegInfo,
            UseAge,
            BirthDate,
            Age,
            Sex,
            HeadHH,
            SpecialDiet,
            IsDisabled,
            VolunteersAtFoodBank,
            NotIncludedInClientList,
            UserFlag0,
            UserFlag1,
            Notes,
            BegCSFP,
            CSFP,
            CSFPExpiration,
            CSFPRoute,
            CSFPComments,
            BegWork,
            WorksInArea,
            Employer,
            EmpAddress,
            EmpCity,
            EmpZipcode,
            EmpPhone,
            BegOption,
            HispanicLatino,
            RefugeeImmigrant,
            LimitedEnglish,
            PartneredMarried,
            LongTermHomeless,
            ChronicallyHomeless,
            BegMilitary,
            MilitaryService,
            DischargeStatus,
            BegEmployed,
            Employed,
            EmploymentStatus,
            EducationLevel,
            BegEthnicity,
            AmericanIndian,
            AlaskaNative,
            IndigenousToAmericas,
            AsianIndian,
            Cambodian,
            Chinese,
            Filipino,
            Japanese,
            Korean,
            Vietnamese,
            OtherAsian,
            IndigenousAfricanBlack,
            AfricanAmericanBlack,
            OtherBlack,
            HawaiianNative,
            Polynesian,
            Micronesian,
            OtherPacificIslander,
            ArabIranianMiddleEastern,
            OtherWhiteCaucasian,
            EthnicOther,
            EthnicUnknown
        }
        #endregion
        #region Grid Field Names
        string[] dgvRowNames = new string[]
        {"ID","Inactive","Last Name","First Name",
         "---------------","Use Age not Birth Date","Birth Date","Age","Sex","Head Household",
         "Special Diet","Disabled","Volunteers at Food Bank","Do Not Include in Find Client List",
         "UserFlag0","UserFlag1","Notes",
         "-- CSFP --","CSFP","CSFP Expiration Date","CSFP Route","CSFP Comments",
         "-- Works In Area --","Works In Area","Employer","Employer Address","Employer City","Employer Zipcode","Employer Phone",
         "-- Survey --","Hispanic","Refugee","LimitedEnglish","Married or Partnered","Long Term Homeless","Chronically Homeless",
         "-- Military Service --","Military Service","Discharge Status",
         "-- Employment/Educ --","Employed","Employment Status","Education Level",
         "-- Ethnicity --",
         "American Indian (U.S. Tribe)",
            "Alaska Native",
            "Indigenous To Americas (No USA)",
            "Asian Indian",
            "Cambodian",
            "Chinese, Except Taiwanese",
            "Filipino",
            "Japanese",
            "Korean",
            "Vietnamese",
            "Other Asian",
            "Indigenous African/Black",
            "African American/Black",
            "Other Black",
            "Hawaiian Native",
            "Polynesian",
            "Micronesian",
            "Other Pacific Islander",
            "Arab/Iranian Middle Eastern",
            "Other/White Caucasian",
            "Other",
            "Unknown"};
        #endregion
        #region Grid Field Types
        CSDGCell.CSDGCellTypeEnum[] fldTypes = new CSDGCell.CSDGCellTypeEnum[]
        {CSDGCell.CSDGCellTypeEnum.Label ,CSDGCell.CSDGCellTypeEnum.Combo,CSDGCell.CSDGCellTypeEnum.Text,CSDGCell.CSDGCellTypeEnum.Text,
         CSDGCell.CSDGCellTypeEnum.Label,CSDGCell.CSDGCellTypeEnum.Combo,CSDGCell.CSDGCellTypeEnum.Date,CSDGCell.CSDGCellTypeEnum.Text,CSDGCell.CSDGCellTypeEnum.ComboText,CSDGCell.CSDGCellTypeEnum.Combo,
         CSDGCell.CSDGCellTypeEnum.Combo,CSDGCell.CSDGCellTypeEnum.Combo,CSDGCell.CSDGCellTypeEnum.Combo,CSDGCell.CSDGCellTypeEnum.Combo,
         CSDGCell.CSDGCellTypeEnum.Combo,CSDGCell.CSDGCellTypeEnum.Combo,CSDGCell.CSDGCellTypeEnum.Text,
         CSDGCell.CSDGCellTypeEnum.Label,CSDGCell.CSDGCellTypeEnum.Combo,CSDGCell.CSDGCellTypeEnum.Date,CSDGCell.CSDGCellTypeEnum.Combo,CSDGCell.CSDGCellTypeEnum.Text,
         CSDGCell.CSDGCellTypeEnum.Label,CSDGCell.CSDGCellTypeEnum.Combo,CSDGCell.CSDGCellTypeEnum.Text,CSDGCell.CSDGCellTypeEnum.Text,CSDGCell.CSDGCellTypeEnum.Text,CSDGCell.CSDGCellTypeEnum.Text,CSDGCell.CSDGCellTypeEnum.Text,
         CSDGCell.CSDGCellTypeEnum.Label,CSDGCell.CSDGCellTypeEnum.Combo,CSDGCell.CSDGCellTypeEnum.Combo,CSDGCell.CSDGCellTypeEnum.Combo,CSDGCell.CSDGCellTypeEnum.Combo,CSDGCell.CSDGCellTypeEnum.Combo,CSDGCell.CSDGCellTypeEnum.Combo,
         CSDGCell.CSDGCellTypeEnum.Label,CSDGCell.CSDGCellTypeEnum.Combo,CSDGCell.CSDGCellTypeEnum.Combo,
         CSDGCell.CSDGCellTypeEnum.Label,CSDGCell.CSDGCellTypeEnum.Combo,CSDGCell.CSDGCellTypeEnum.Combo,CSDGCell.CSDGCellTypeEnum.Combo,
         CSDGCell.CSDGCellTypeEnum.Label,CSDGCell.CSDGCellTypeEnum.Combo,CSDGCell.CSDGCellTypeEnum.Combo,CSDGCell.CSDGCellTypeEnum.Combo,
         CSDGCell.CSDGCellTypeEnum.Combo,CSDGCell.CSDGCellTypeEnum.Combo,CSDGCell.CSDGCellTypeEnum.Combo,
         CSDGCell.CSDGCellTypeEnum.Combo,CSDGCell.CSDGCellTypeEnum.Combo,CSDGCell.CSDGCellTypeEnum.Combo,
         CSDGCell.CSDGCellTypeEnum.Combo,CSDGCell.CSDGCellTypeEnum.Combo,CSDGCell.CSDGCellTypeEnum.Combo,
         CSDGCell.CSDGCellTypeEnum.Combo,CSDGCell.CSDGCellTypeEnum.Combo,CSDGCell.CSDGCellTypeEnum.Combo,
         CSDGCell.CSDGCellTypeEnum.Combo,CSDGCell.CSDGCellTypeEnum.Combo,CSDGCell.CSDGCellTypeEnum.Combo,
         CSDGCell.CSDGCellTypeEnum.Combo,CSDGCell.CSDGCellTypeEnum.Combo,CSDGCell.CSDGCellTypeEnum.Combo,CSDGCell.CSDGCellTypeEnum.Combo};
        #endregion
        #region Grid Combo Types
        int[] cboTypes = new int[]
        {-1,0,-1,-1,
         -1,0,-1,-1,1,0,
         0,0,0,0,
         0,0,-1,
         -1,0,-1,2,-1,
         -1,0,-1,-1,-1,-1,-1,
         -1,3,3,3,3,3,3,
         -1,4,5,
         -1,3,6,7,
         -1,0,0,0,0,0,0,0,0,0,
         0,0,0,0,0,0,0,0,0,0,
         0,0,0,0};
        #endregion
        string userField0 = "";
        string userField1 = "";
        bool[] tpgNeedToRefresh = new bool[]{ false, false };
        /// <summary>
        /// Used in initialization of the HHMemForm(ie Constructor)
        /// </summary>
        /// <param name="clsIn">The Client Class</param>
        /// <param name="frmMainIn">The Main Form-Calling Form</param>
        /// <param name="HHMemID">The Household Member ID</param>
        public HHMemGridForm(Client clsIn, MainForm frmMainIn, int HHMemID)
        {
            InitializeComponent();

            clsClient = clsIn;
            frmMain = frmMainIn;
            this.Text = "[ " + clsClient.clsHH.ID.ToString() + " ] Family Members Maintenance";
            showColumn[0] = false;
            for (int i = 1; i < showColumn.Length; i++)
            {
                showColumn[i] = true;
            }
            hhMemID = HHMemID;
            UserFields clsUserFields = new UserFields(CCFBGlobal.connectionString);
            clsUserFields.open("HouseholdMembers");
            userField0 = clsUserFields.GetDataValue("EditLabel", 0).ToString();
            userField1 = clsUserFields.GetDataValue("EditLabel", 1).ToString();

            if (userField0 == "")
            {
                showColumn[(int)dgvColNames.UserFlag0] = false;
                chkUserFlag0.Visible = false;
            }
            else
                chkUserFlag0.Text = userField0;

            if (userField1 == "")
            {
                showColumn[(int)dgvColNames.UserFlag1] = false;
                chkUserFlag1.Visible = false;
            }
            else
                chkUserFlag1.Text = userField1;

            traverseAndAddControlsToCollections(this.Controls);
            setCSFPInfoVisibility();

            CCFBGlobal.InitCombo(cboLatino, CCFBGlobal.parmTbl_YesNoUnk);
            CCFBGlobal.InitCombo(cboRefugee, CCFBGlobal.parmTbl_YesNoUnk);
            CCFBGlobal.InitCombo(cboLimitedEnglish, CCFBGlobal.parmTbl_YesNoUnk);
            CCFBGlobal.InitCombo(cboMarried, CCFBGlobal.parmTbl_YesNoUnk);
            CCFBGlobal.InitCombo(cboLongTermHomeless, CCFBGlobal.parmTbl_YesNoUnk);
            CCFBGlobal.InitCombo(cboChronicallyHomeless, CCFBGlobal.parmTbl_YesNoUnk);
            CCFBGlobal.InitCombo(cboEmployed, CCFBGlobal.parmTbl_YesNoUnk);

            CCFBGlobal.InitCombo(cboEdLvl, CCFBGlobal.parmTbl_EducationLevel);
            CCFBGlobal.InitCombo(cboEmploymentStatus, CCFBGlobal.parmTbl_Employment);
            CCFBGlobal.InitCombo(cboMilitaryService, CCFBGlobal.parmTbl_MilitaryService);
            CCFBGlobal.InitCombo(cboDischargeStatus, CCFBGlobal.parmTbl_MilitaryDischarge);
            CCFBGlobal.InitCombo(cboCSFPRoute, CCFBGlobal.parmTbl_CSFPRoutes);

            TestFeatureEnabled(tsbCSFP, (int)dgvColNames.BegCSFP, (int)dgvColNames.BegWork, CCFBPrefs.EnableCSFP);
            TestFeatureEnabled(tsbWorksInArea, (int)dgvColNames.BegWork, (int)dgvColNames.BegOption, CCFBPrefs.EnableWorksInArea);
            pnlEmployer.Visible = CCFBPrefs.EnableWorksInArea;
            TestFeatureEnabled(tsbOptional, (int)dgvColNames.BegOption, (int)dgvColNames.BegMilitary, CCFBPrefs.EnableAdditionalHHMDataTab);
            TestFeatureEnabled(tsbMilitarySvc, (int)dgvColNames.BegMilitary, (int)dgvColNames.BegEmployed, CCFBPrefs.EnableAdditionalHHMDataTab);
            TestFeatureEnabled(tsbEmployment, (int)dgvColNames.BegEmployed, (int)dgvColNames.EducationLevel+1, CCFBPrefs.EnableAdditionalHHMDataTab);

            pnlHHMFlds.BackColor = CCFBGlobal.bkColorBaseEdit;
            tpgAdditional.BackColor = CCFBGlobal.bkColorBaseEdit;
            tpgEthnicity.BackColor = CCFBGlobal.bkColorBaseEdit;
            pnlCSFPInfo.BackColor = CCFBGlobal.bkColorAltEdit;
            pnlEmployer.BackColor = CCFBGlobal.bkColorAltEdit;
            if (CCFBPrefs.EnableAdditionalHHMDataTab == true || CCFBPrefs.EnableEthnicityHHMTab == true)
            {
                chkSurveyComplete.Visible = true;
                chkSurveyComplete.Checked = clsClient.clsHH.SurveyComplete;
                lblSurveyComplete.Visible = true;
            }
            else
            {
                chkSurveyComplete.Visible = false;
                lblSurveyComplete.Visible = false;
            }

            if (CCFBPrefs.EnableAdditionalHHMDataTab == false && CCFBPrefs.EnableEthnicityHHMTab == false)
                splitContainer3.Panel2.Visible = false;
            else
            {
                splitContainer3.Panel2.Visible = true;
                if (CCFBPrefs.EnableEthnicityHHMTab == false)
                    tabCtrlDemographics.TabPages.RemoveByKey("tpgEthnicity");  //Remove Ethnicity Tab
                if (CCFBPrefs.EnableAdditionalHHMDataTab == false)
                    tabCtrlDemographics.TabPages.RemoveByKey("tpgAdditional");  //Remove Additional Tab
            }

            storedProcComm = new SqlCommand("UpdateHHMembersAdditionalData", conn);

            try
            {
                //tbHouseholdData.Text = "[ " + clsClient.clsHH.ID.ToString() + " ]\r\n"
                lblHouseholdData.Text = clsClient.clsHH.Name.ToString() + "\r\n"
                                     + clsClient.clsHH.Address.ToString() + "\r\n"
                                     + clsClient.clsHH.City.ToString() + ", "
                                     + clsClient.clsHH.State.ToString() + " "
                                     + clsClient.clsHH.Zipcode.ToString();
            }
            catch { }
            dgvHHM2Init(clsClient.clsHHmem.RowCount);
            loadHHMems(hhMemID);
            setViewMode();
            fillForm();
            lvHHMembers.Focus();
        }

        private void addHHMember()
        {
            if (clsClient.clsHHmem.RowCount > 0)
                clsClient.clsHHmem.update();

            AddNewHouseholdMember frmAddHHMem = new AddNewHouseholdMember(clsClient);
            frmAddHHMem.ShowDialog();
            if (frmAddHHMem.Canceled == false)
            {
                clsClient.clsHHmem.openHHID(clsClient.clsHH.ID);
                updateHouseholdData();

                int hhMemID = 0;
                int tmpID = 0;
                int rowNbr = 0;
                for (int i = 0; i < clsClient.clsHHmem.RowCount; i++)
                {
                    tmpID = clsClient.clsHHmem.DSet.Tables[0].Rows[i].Field<int>("ID");
                    if (hhMemID < tmpID)
                    {
                        rowNbr = i;
                        hhMemID = tmpID;
                    }
                }
                clsClient.clsHHmem.SetRecord(rowNbr);
                loadHHMems(clsClient.clsHHmem.ID);
                fillForm();
            }
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            addHHMember();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lvHHMembers.SelectedItems[0] != null)
            {
                if (clsClient.clsHHmem.HeadHH == false && lvHHMembers.Items.Count > 1)
                {
                    if (MessageBox.Show("Are You Sure You Want To Delete " + clsClient.clsHHmem.Name + "?",
                    "Delete Family Member", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                    {
                        clsClient.clsHHmem.delete(Convert.ToInt32(lvHHMembers.SelectedItems[0].Tag.ToString()));
                        clsClient.clsHHmem.openHHID(clsClient.clsHH.ID);
                        loadHHMems(clsClient.clsHHmem.ID);
                        fillForm();
                        updateHouseholdData();
                    }
                }
                else
                {
                    string msg = "DELETE family member: " + clsClient.clsHHmem.Name + "\r\n";
                    if (clsClient.clsHHmem.HeadHH == true)
                    {
                        msg += " is the Head of Household.\r\n";
                        if (clsClient.clsHHmem.RowCount > 1)
                            msg += "Please mark another family member as Head of Household. Then you may delete this family member.";
                    }
                    if (clsClient.clsHHmem.RowCount == 1)
                        msg += "Deleting the last memeber of the household is not allowed.\r\n";
                    if (CCFBGlobal.currentUser_PermissionLevel >= CCFBGlobal.permissions_Intake)
                        msg += "You may delete the entrie client from the main form using the menu Client/Delete Client.\r\n";
                    else
                        msg += "You may mark the household as Inactive from the main form.\r\nPlease ask an administrator to delete the household.";

                    MessageBox.Show(msg, "Delete Family Member", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            clsClient.clsHHmem.rejectChanges();
            //clearChanges(clsClient.clsHHmem.ID);
            fillForm();
            fillGridData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (tabCtrl.SelectedTab.Name == tpgMember.Name)
            {
                if (tbBirthdate.Text == null && tbAge.Text == null && clsClient.clsHH.UseFamilyList == true)
                {
                    MessageBox.Show("Cannot Save Household Member When Both Age And Birthdate Are Blank");
                }
                else if (clsClient.clsHHmem.update() == true)
                {
                    updateHouseholdData();
                    loadHHMems(hhMemID);
                    clsClient.clsHHmem.find(hhMemID);
                    fillForm();
                    tpgNeedToRefresh[1] = true;
                }
            }
            else
            {
                updateHouseholdData();
                tpgNeedToRefresh[0] = true;
                tpgNeedToRefresh[1] = false;
                lblNeedToSave.Visible = false;
            }
        }

        private void chkEnterAge_CheckedChanged(object sender, EventArgs e)
        {
            clsClient.clsHHmem.UseAge = chkEnterAge.Checked;
            setupBirthdateAndAge(clsClient.clsHHmem.UseAge);
        }

        private void chkHeadHH_CheckedChanged(object sender, EventArgs e)
        {
            if (loadingInfo == false)
            {
                if (chkHeadHH.Checked == true)
                {
                    clsClient.clsHHmem.HeadHH = true;
                    for (int i = 0; i < clsClient.clsHHmem.RowCount; i++)
                    {
                        clsClient.clsHHmem.SetRecord(i);
                        if (clsClient.clsHHmem.HeadHH == true && clsClient.clsHHmem.ID != hhMemID)
                        {
                            if (MessageBox.Show("Press Yes to mark " + tbFirstName.Text + " " + tbLastName.Text +
                                " as Head of Household.\r\n" +
                                "Press No to leave " + clsClient.clsHHmem.Name + " as Head of Household.",
                                "Head Household Confirmation",
                                 MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                            {
                                clsClient.clsHHmem.HeadHH = false;
                                clsClient.clsHHmem.find(hhMemID);
                                clsClient.clsHHmem.HeadHH = true;
                                break;
                            }
                            else
                            {
                                clsClient.clsHHmem.find(hhMemID);
                                clsClient.clsHHmem.HeadHH = false;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    clsClient.clsHHmem.HeadHH = false;
                }
                loadHHMems(hhMemID);
            }
        }

        private void cboList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (loadingInfo == false)
            {
                ComboBox cb = (ComboBox)sender;
                clsClient.clsHHmem.SetDataValue(cb.Tag.ToString(),
                            cb.SelectedValue.ToString());
                lblNeedToSave.Visible = true;
            }
        }

        private void chkList_CheckStateChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            if (chk.CheckState == CheckState.Indeterminate)
                { chk.ImageIndex = 6; }
            else
                { chk.ImageIndex = -1; }
        }

        private void chkList_KeyDown(object sender, KeyEventArgs e)
        {
            if (inEditMode == true && e.KeyCode == Keys.Enter)
            {
                CheckBox chkHH = (CheckBox)sender;
                chkHH.Checked = !chkHH.Checked;
            }
        }

        private void chkList_Enter(object sender, EventArgs e)
        {
            bchkOriValue = ((CheckBox)sender).Checked;
        }


        private void chkList_Leave(object sender, EventArgs e)
        {
            CheckBox chkBox = (CheckBox)sender; //Get the correct textbox

            if (inEditMode == true)
            {
                if (chkBox.Tag != null && chkBox.Tag.ToString().Trim() != "")
                {
                    if (chkBox.Checked != bchkOriValue)
                    {
                        lvHHMembers.SelectedItems[0].ImageIndex = 0;
                        clsClient.clsHHmem.SetDataValue(chkBox.Tag.ToString(), chkBox.Checked.ToString());
                        chkBox.BackColor = Color.Pink;
                        lblNeedToSave.Visible = true;
                    }
                    else
                    {
                        chkBox.BackColor = CCFBGlobal.bkColorBaseEdit;
                    }
                }
            }
        }

        private void chkSurveyComplete_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSurveyComplete.Focused == true)
            {
                clsClient.clsHH.SurveyComplete = chkSurveyComplete.Checked;
                clsClient.clsHH.update();
            }
        }

        private void chkWorksInArea_CheckedChanged(object sender, EventArgs e)
        {
            clsClient.clsHHmem.WorksInArea = chkWorksInArea.Checked;
            toggleEmployerFields(chkWorksInArea.Checked);
        }

        private void fillForm()
        {
            inEditMode = false;
            loadingInfo = true;
            if (clsClient.clsHHmem.RowCount > 0)
            {
                tbID.Text = clsClient.clsHHmem.ID.ToString();
                foreach (TextBox tb in tbList)
                {
                    if (tb.Tag != null && tb.Tag.ToString().Trim() != "")
                        tb.Text = clsClient.clsHHmem.GetDataString(tb.Tag.ToString());
                    tb.Enabled = true;
                    tb.ForeColor = Color.Black;
                    tb.BackColor = Color.White;
                }

                foreach (CheckBox chk in chkList)
                {
                    if (chk.Tag != null && chk.Tag.ToString().Trim() != "")
                        chk.Checked = CCFBGlobal.NullToFalse(clsClient.clsHHmem.GetDataValue(chk.Tag.ToString()));
                    chk.Enabled = true;
                    chk.ForeColor = Color.Black;
                    chk.BackColor = CCFBGlobal.bkColorBaseEdit; 
                }

                foreach (ListViewItem lvi in lvEthnicity.Items)
                {
                    lvEthnicity.Items[lvi.Index].Checked =
                        (bool)CCFBGlobal.NullToFalse(clsClient.clsHHmem.GetDataValue(lvi.Tag.ToString()));
                }
                lvEthnicity.Enabled = true;
                foreach (ComboBox cb in cboList)
                {
                    cb.SelectedValue = clsClient.clsHHmem.GetDataValue(cb.Tag.ToString()).ToString();
                    cboCSFPRoute.Enabled = true;
                }

                toggleEmployerFields(clsClient.clsHHmem.WorksInArea);
                setupBirthdateAndAge(clsClient.clsHHmem.UseAge);
                tbExpires.Text = CCFBGlobal.ValidDateString(clsClient.clsHHmem.CSFPExpiration);
                toggleCSFPFields(chkCSFP.Checked);
                btnDelete.Enabled = (clsClient.clsHHmem.RowCount > 1);
                btnReset.Enabled = true;
                btnSave.Enabled = true;
                btnPrint.Enabled = true;
                inEditMode = true;
            }
            else
            {
                foreach (TextBox tb in tbList)
                {
                    if (tb.Tag != null && tb.Tag.ToString().Trim() != "")
                        tb.Text = "";
                    tb.Enabled = false;
                }

                foreach (CheckBox chk in chkList)
                {
                    if (chk.Tag != null && chk.Tag.ToString().Trim() != "")
                        chk.Checked = false;
                    chk.Enabled = false;

                }

                foreach (ListViewItem lvi in lvEthnicity.Items)
                {
                    lvEthnicity.Items[lvi.Index].Checked = false;
                }
                lvEthnicity.Enabled = false;
                foreach (ComboBox cb in cboList)
                {
                    cb.SelectedValue = -1;
                    cboCSFPRoute.Enabled = false;
                }

                toggleEmployerFields(false);
                toggleCSFPFields(false);
                setupBirthdateAndAge(false);
                inEditMode = false;
                btnDelete.Enabled = false;
                btnReset.Enabled = false;
                btnSave.Enabled = false;
                btnPrint.Enabled = false;
            }
            tpgNeedToRefresh[0] = false;
            lblNeedToSave.Visible = false;
            loadingInfo = false;
        }

        private void loadHHMems(int setSelectedID)
        {
            DataGridViewRow dvr;
            lvHHMembers.Items.Clear();
            int newIndex = 0;
            int rowCount = 0;
            string sText = "";
            ListViewItem lvi;
            dgvHHM2Init(clsClient.clsHHmem.RowCount);
            for (int i = 0; i < clsClient.clsHHmem.RowCount; i++)
            {
                clsClient.clsHHmem.SetRecord(i);
                //Member View
                lvi = new ListViewItem(clsClient.clsHHmem.LastName,1);
                lvi.SubItems.Add(clsClient.clsHHmem.FirstName);
                lvi.SubItems.Add(clsClient.clsHHmem.Age.ToString());
                sText = "";
                if (clsClient.clsHHmem.Inactive == true)
                    sText = "Inactive";
                else if (clsClient.clsHHmem.HeadHH == true)
                    sText = "Head House";
                else if (clsClient.clsHHmem.NotIncludedInClientList == true)
                    sText = "Not Listed";
                lvi.SubItems.Add(sText);
                lvi.SubItems.Add("");
                lvi.SubItems.Add("");
                lvi.Tag = clsClient.clsHHmem.ID;
                lvHHMembers.Items.Add(lvi);
                if (setSelectedID == clsClient.clsHHmem.ID)
                { newIndex = i; }
                //Grid View
                //dgvOldOne.Rows.Add();
                //dvr = dgvOldOne.Rows[rowCount];
                //foreach (DataGridViewColumn dgvCol in dgvOldOne.Columns)
                //{
                //    if (dgvCol.DataPropertyName != null && dgvCol.DataPropertyName != "")
                //    {
                //        switch (dgvCol.CellType.Name)
                //        {
                //            case "DataGridViewCheckBoxCell":
                //                dvr.Cells[dgvCol.HeaderCell.ColumnIndex].Value = clsClient.clsHHmem.GetDataString(dgvCol.DataPropertyName);
                //                break;
                //            case "DataGridViewComboBoxCell":
                //                string newvalue = clsClient.clsHHmem.GetDataString(dgvCol.DataPropertyName);
                //                if (newvalue == "")
                //                    newvalue = "0";
                //                dvr.Cells[dgvCol.HeaderCell.ColumnIndex].Value = newvalue.ToString();
                //                break;
                //            case "DataGridViewTextBoxCell":
                //                dvr.Cells[dgvCol.HeaderCell.ColumnIndex].Value = clsClient.clsHHmem.GetDataString(dgvCol.DataPropertyName);
                //                break;
                //            default:
                //                break;
                //        }
                //    }
                //}
                //rowCount++;
            }
            if (newIndex < lvHHMembers.Items.Count)
            {
                clsClient.clsHHmem.SetRecord(newIndex);
                lvHHMembers.Items[newIndex].Selected = true;
            }
        }

        private void lvHHMembers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (loadingInfo == false && lvHHMembers.FocusedItem != null)
            {
                hhMemID = Convert.ToInt32(lvHHMembers.FocusedItem.Tag.ToString());
                clsClient.clsHHmem.find(hhMemID);
                fillForm();
            }
        }

        private void setCSFPInfoVisibility()
        {
            pnlCSFPInfo.Visible = (CCFBPrefs.EnableCSFP == true);
            if (CCFBPrefs.EnableCSFP == true)
                toggleCSFPFields(chkCSFP.Checked);
        }

        private void setupBirthdateAndAge(bool useAge)
        {
            tbAge.Enabled = useAge;
            tbBirthdate.Enabled = (useAge == false);
            if (useAge == false)
            {
                //tbBirthdate.ReadOnly = false;
                tbBirthdate.ForeColor = Color.Black;
                //tbAge.ReadOnly = true;
                tbAge.BackColor = Color.White;
                tbAge.ForeColor = CCFBGlobal.AgeBirthdateColor;
            }
            else
            {
                //tbBirthdate.ReadOnly = true;
                tbBirthdate.BackColor = Color.White;
                tbBirthdate.ForeColor = CCFBGlobal.AgeBirthdateColor;
                //tbAge.ReadOnly = false;
                tbAge.ForeColor = Color.Black;
            }
        }

        private void setViewMode()
        {
            lvHHMembers.Visible = (tabCtrl.SelectedIndex == 0);
        }

        private void showBasicInfo()
        {
            ToggleRows(tsbBasicInfo.Checked, (int)dgvColNames.BegInfo, (int)dgvColNames.Notes);
        }

        private void showCSFP()
        {
            ToggleRows(tsbCSFP.Checked, (int)dgvColNames.BegCSFP, (int)dgvColNames.BegWork);
        }

        private void showEmployment()
        {
            ToggleRows(tsbEmployment.Checked, (int)dgvColNames.BegEmployed, (int)dgvColNames.BegEthnicity);
        }

        private void showEthnicity()
        {
            ToggleRows(tsbEthnicity.Checked, (int)dgvColNames.BegEthnicity, (int)dgvColNames.EthnicUnknown + 1);
        }

        private void showMilitaryService()
        {
            ToggleRows(tsbMilitarySvc.Checked, (int)dgvColNames.BegMilitary, (int)dgvColNames.BegEmployed);
        }

        private void showNotes()
        {
            ToggleRows(tsbNotes.Checked,(int)dgvColNames.Notes,(int)dgvColNames.Notes + 1);
        }

        private void showOptional()
        {
            ToggleRows(tsbOptional.Checked, (int)dgvColNames.BegOption, (int)dgvColNames.BegMilitary);
        }

        private void showWorksInArea()
        {
            ToggleRows(tsbWorksInArea.Checked, (int)dgvColNames.BegWork, (int)dgvColNames.BegOption);
        }

        private void ToggleRows(bool isVisible, int firstRow, int lastRow)
        {
            for (int i = firstRow; i < lastRow; i++)
            {
                dgvHHM2.Rows[i].Visible = isVisible && showColumn[i];
            }
        }
        
        private void tbAge_Leave(object sender, EventArgs e)
        {
            if (tbAge.Text != "" && tbAge.Enabled == true)
            {
                if (clsClient.clsHHmem.UseAge == true)
                {
                    clsClient.clsHHmem.Birthdate = new DateTime(DateTime.Today.Year - Convert.ToInt32(tbAge.Text),
                        07, 01);
                    tbBirthdate.ForeColor = Color.Aqua;
                    tbBirthdate.Text = clsClient.clsHHmem.Birthdate.ToShortDateString();
                }
            }
            else if (tbAge.Enabled == true)
            {
                tbAge.Text = "0";
            }
        }

        private void tbBirthdate_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (clsClient.clsHHmem.UseAge == false)
            {
                try
                {
                    DateTime.Parse(tbBirthdate.Text);
                }
                catch
                {
                    if (MessageBox.Show("The Birthdate Was Not In Proper Format. What Would You Like To Do?",
                        "Birdate Format", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning) ==
                       DialogResult.Retry)
                    {
                        e.Cancel = true;
                    }
                    else
                    {
                        tbBirthdate.Text = clsClient.clsHHmem.GetDataString(tbBirthdate.Tag.ToString());
                    }
                }
            }
        }

        private void tbList_Enter(object sender, EventArgs e)
        {
            stbOriValue = ((TextBox)sender).Text;
        }

        private void tbList_Leave(object sender, EventArgs e)
        {
            TextBox tbHH = (TextBox)sender; //Get the correct textbox

            if (inEditMode == true)
            {
                if (tbHH.Tag != null && tbHH.Tag.ToString().Trim() != "")
                    if (tbHH.Text != stbOriValue)
                    {
                        lvHHMembers.SelectedItems[0].ImageIndex = 0;
                        clsClient.clsHHmem.SetDataValue(tbHH.Tag.ToString(), tbHH.Text);
                        tbHH.BackColor = Color.Pink;
                        lblNeedToSave.Visible = true;
                    }
            }
        }

        private void toggleCSFPFields(bool isVisible)
        {
            lblCSFPExpires.Visible = isVisible;
            lblCSFPRoute.Visible = isVisible;
            lblCSFPComments.Visible = isVisible;
            tbExpires.Visible = isVisible;
            cboCSFPRoute.Visible = isVisible;
            tbCSFPComments.Visible = isVisible;
        }

        private void toggleEmployerFields(bool isVisible)
        {
            lblEmployer.Visible = isVisible;
            lblAddress.Visible = isVisible;
            lblPhone.Visible = isVisible;
            lblEmpZip.Visible = isVisible;
            tbEmployer.Visible = isVisible;
            tbEmpAddress.Visible = isVisible;
            tbEmplPhone.Visible = isVisible;
            tbEmpZip.Visible = isVisible;
        }

        /// <summary>
        /// Traverses all controls on the form using recursion and adds each appropriate
        /// control to the appropriate Collection
        /// </summary>
        /// <param name="controlList">The Collection of COntrols</param>
        private void traverseAndAddControlsToCollections(Control.ControlCollection controlList)
        {
            foreach (Control cntrl in controlList.OfType<Control>())
            {
                switch (cntrl.GetType().Name)
                {
                    case "TextBox":
                        {
                            if (cntrl.Tag != null && cntrl.Tag.ToString().Trim() != "")
                            {
                                cntrl.ForeColor = Color.Black;
                                cntrl.BackColor = Color.White;
                                tbList.Add((TextBox)cntrl);
                                cntrl.Enter += new System.EventHandler(this.tbList_Enter);
                                cntrl.Leave += new System.EventHandler(this.tbList_Leave);
                            }
                            break;
                        }
                    case "CheckBox":
                        {
                            if (cntrl.Tag != null && cntrl.Tag.ToString().Trim() != "")
                            {
                                CheckBox chk = (CheckBox)cntrl;
                                chk.Enter += new System.EventHandler(this.chkList_Enter);
                                chk.Leave += new System.EventHandler(this.chkList_Leave);
                                chk.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkList_KeyDown);
                                chk.CheckStateChanged += new System.EventHandler(this.chkList_CheckStateChanged);
                                chkList.Add(chk);
                            }
                            break;
                        }
                    case "ComboBox":
                        {
                            if (cntrl.Tag != null)
                            {
                                if (cntrl.Tag.ToString().Trim() != "")
                                {
                                    cboList.Add((ComboBox)cntrl);
                                }
                            }
                            break;
                        }
                }
                traverseAndAddControlsToCollections(cntrl.Controls);
            }
        }

        private void tsbBasicInfo_Click(object sender, EventArgs e)
        {
            showBasicInfo();
        }

        private void tsbCSFP_Click(object sender, EventArgs e)
        {
            showCSFP();
        }

        private void tsbOptional_Click(object sender, EventArgs e)
        {
            showOptional();
        }

        private void tsbWorksInArea_Click(object sender, EventArgs e)
        {
            showWorksInArea();
        }

        private void tsbNotes_Click(object sender, EventArgs e)
        {
            showNotes();
        }

        private void TestFeatureEnabled(ToolStripButton tsb, int istart, int iend, bool isVisible)
        {
            tsb.Visible = isVisible;
            for (int i = istart; i < iend; i++)
            {
                showColumn[i] = isVisible;
            }
        }

        private void btnMarkSame_Click(object sender, EventArgs e)
        {
            if (lvHHMembers.SelectedItems[0] != null)
            {
                clsClient.clsHHmem.update();

                if (tabCtrlDemographics.SelectedTab.Text == "Additional")
                {
                    storedProcComm = new SqlCommand("UpdateHHMembersAdditionalData", conn);
                    showSameIcon(4);
                }
                else
                {
                    storedProcComm = new SqlCommand("UpdateHHMembersEthnicityData", conn);
                    showSameIcon(5);
                }
                storedProcComm.CommandType = CommandType.StoredProcedure;
                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@HHMId";
                parameter.SqlDbType = SqlDbType.Int;
                parameter.Direction = ParameterDirection.Input;
                parameter.Value = Convert.ToInt32(lvHHMembers.SelectedItems[0].Tag);

                // Add the parameter to the Parameters collection. 
                storedProcComm.Parameters.Add(parameter);

                try
                {
                    // Open the connection and execute the reader.
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    storedProcComm.ExecuteNonQuery();

                    // Open the connection and execute the reader.
                    if (conn.State == ConnectionState.Open)
                        conn.Close();

                    clsClient.clsHHmem.openHHID(clsClient.clsHH.ID);
                    clsClient.clsHHmem.find(Convert.ToInt32(lvHHMembers.SelectedItems[0].Tag));
                    fillForm();
                }
                catch (SqlException ex)
                {
                    CCFBGlobal.appendErrorToErrorReport("HHMID= " + lvHHMembers.SelectedItems[0].Tag.ToString(),
                        ex.GetBaseException().ToString());
                }
            }
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            if (clsClient.clsHHmem.HasChanges == true)
            {
                if (MessageBox.Show("Would You Like To Save Changes?", "WARNING: COULD LOSE CHANGES",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                    clsClient.clsHHmem.update();
                else if (clsClient.clsHHmem.RowCount > 0)
                    clsClient.clsHHmem.rejectChanges();
            }

            this.Close();
        }

        private void tsbMilitarySvc_Click(object sender, EventArgs e)
        {
            showMilitaryService();
        }

        private void tsbEmployment_Click(object sender, EventArgs e)
        {
            showEmployment();
        }

        private void tabCtrl_SelectedIndexChanged(object sender, EventArgs e)
        {
            setViewMode();
            if (tabCtrl.SelectedTab.Name == tpgMember.Name)
            {
                if (tpgNeedToRefresh[0] == true)
                {
                    clsClient.clsHHmem.openHHID(clsClient.clsHH.ID);
                    clsClient.clsHHmem.find(Convert.ToInt32(lvHHMembers.SelectedItems[0].Tag));
                    fillForm();
                }
            }
            else if (tpgNeedToRefresh[1] == true)
            {
                fillGridData();
            }
        }

        private void updateHouseholdData()
        {
            clsClient.UpdateDataBasedOn(DateTime.Parse(CCFBGlobal.DefaultServiceDate));
        }

        private void updateDemographicsWSame()
        {
            if (lvHHMembers.SelectedItems[0] != null)
            {
                clsClient.clsHHmem.update();
                string procName = "UpdateHHMembersEthnicityData";
                if (tabCtrlDemographics.SelectedTab.Text == "Additional")
                    procName = "UpdateHHMembersAdditionalData";
                SqlCommand sqlCmd = new SqlCommand(procName, conn);

                sqlCmd.CommandType = CommandType.StoredProcedure;
                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@HHMId";
                parameter.SqlDbType = SqlDbType.Int;
                parameter.Direction = ParameterDirection.Input;
                parameter.Value = Convert.ToInt32(lvHHMembers.SelectedItems[0].Tag);
                sqlCmd.Parameters.Add(parameter);

                try
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    sqlCmd.ExecuteNonQuery();

                    if (conn.State == ConnectionState.Open)
                        conn.Close();

                    clsClient.clsHHmem.openHHID(clsClient.clsHH.ID);
                    clsClient.clsHHmem.find(Convert.ToInt32(lvHHMembers.SelectedItems[0].Tag));
                    fillForm();
                }
                catch (SqlException ex)
                {
                    CCFBGlobal.appendErrorToErrorReport("HHMID= " + lvHHMembers.SelectedItems[0].Tag.ToString() + "\r\nProcName =" + procName,
                        ex.GetBaseException().ToString());
                }
            }
        }

        private void chkCSFP_CheckedChanged(object sender, EventArgs e)
        {
            if (inEditMode == true)
            {
                if (chkCSFP.Checked == clsClient.clsHHmem.CSFP)
                    chkCSFP.BackColor = CCFBGlobal.bkColorAltEdit;
                else
                    chkCSFP.BackColor = Color.Pink;
            }
            toggleCSFPFields(chkCSFP.Checked);
        }

        private void lvEthnicity_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (loadingInfo == false)
            {
                clsClient.clsHHmem.SetDataValue(e.Item.Tag.ToString(), e.Item.Checked);
                lblNeedToSave.Visible = true;
            }
        }

        //private void dgvHHM_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (dgvOldOne.Columns[e.ColumnIndex].DataPropertyName.Trim() != "")
        //    {
        //        changesInGridMade = true;
        //        clsClient.clsHHmem.SetDataValue(dgvOldOne.Columns[e.ColumnIndex].DataPropertyName,dgvOldOne[e.ColumnIndex, e.RowIndex].Value.ToString());
        //        clsClient.clsHHmem.update();
        //    }
        //}

        //private void dgvHHM_RowEnter(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (loadingInfo == false)
        //    {
        //        clsClient.clsHHmem.SetRecord(e.RowIndex);
        //        lvHHMembers.Items[e.RowIndex].Selected = true;
        //    }
        //}

        private void showSameIcon(int index)
        {
            foreach (ListViewItem item in lvHHMembers.Items)
            {
                item.SubItems[index].Text = "S";
            }
        }

        private void dgvHHM2Init(int familySize)
        {
            string[] cboListGrid = new string[1];
            dgvHHM2.Rows.Clear();
            dgvHHM2.Columns.Clear();
            dgvHHM2.Columns.Add("Property","Property");
            DataGridViewColumn newCol = dgvHHM2.Columns[0];
            newCol.ReadOnly = true;
            newCol.Width = 200;
            for (int j = 0; j < familySize; j++)
            {
                dgvHHM2.Columns.Add(new CSDGColumn());
            }
            dgvHHM2.Columns[0].Frozen = true;
            string tblName = "";
            for (int i = 0; i < (int)dgvColNames.EthnicUnknown + 1; i++)
            {
                int newRow = dgvHHM2.Rows.Add();
                dgvHHM2.Rows[i].Cells[0].Value = dgvRowNames[i];
                dgvHHM2.Rows[i].Tag = Enum.GetNames(typeof(dgvColNames))[i].ToString();
                if (newRow <= (int)dgvColNames.BegInfo)
                    dgvHHM2.Rows[i].Frozen = true;
                if (dgvRowNames[i].StartsWith("--") == true)
                {
                    dgvHHM2.Rows[i].Height = 18;
                    for (int j = 0; j <= familySize; j++)
                    {
                        dgvHHM2.Rows[i].Cells[j].Style.BackColor = Color.LightGray;
                    }
                }
                else if (fldTypes[i] == CSDGCell.CSDGCellTypeEnum.Label)
                {
                    for (int j = 0; j <= familySize; j++)
                    {
                        dgvHHM2.Rows[i].Cells[j].Style.BackColor = Color.Wheat;
                    }
                }
                else
                    dgvHHM2.Rows[i].Cells[0].Style.BackColor = Color.PaleGoldenrod;

                if (fldTypes[i] == CSDGCell.CSDGCellTypeEnum.Combo || fldTypes[i] == CSDGCell.CSDGCellTypeEnum.ComboText)
                {
                    tblName = parmTableName(i);
                    if (tblName != "")
                    {
                        cboListGrid = new string[CCFBGlobal.TypeCodesArray(tblName).Count];
                        int k = 0;
                        foreach (parmType item in CCFBGlobal.TypeCodesArray(tblName))
                        {
                            cboListGrid[k] = item.TypeName;
                            k++;
                        }
                    }
                    else
                        cboListGrid = null;
                }
                else
                    cboListGrid = null;
                for (int j = 0; j < familySize; j++)
                {
                    CSDGCell newCell = (CSDGCell)dgvHHM2.Rows[i].Cells[j+1];
                    newCell.CSDGCellType = fldTypes[i];
                    if (cboListGrid != null)
                        newCell.SetCboItems(cboListGrid);
                }

            }
            showBasicInfo();
            showCSFP();
            showOptional();
            showWorksInArea();
            showMilitaryService();
            showEmployment();
        }


        private void fillGridData()
        {
            string colName;
            int col;

            for (int i = 0; i < clsClient.clsHHmem.RowCount; i++)
            {
                col = i + 1;
                clsClient.clsHHmem.SetRecord(i);
                foreach (DataGridViewRow dvr in dgvHHM2.Rows)
                {
                    colName = dvr.Tag.ToString();
                    CSDGCell theCell = (CSDGCell)dvr.Cells[col];
                    switch (theCell.CSDGCellType)
                    {
                        case CSDGCell.CSDGCellTypeEnum.Combo:
                            string newvalue = clsClient.clsHHmem.GetDataString(colName);
                            if (newvalue == "")
                                newvalue = "0";
                            else if (newvalue == "False")
                                newvalue = "0";
                            else if (newvalue == "True")
                                newvalue = "1";
                            
                            theCell.SetCboTextFromIndex(Convert.ToInt32(newvalue));
                            break;
                        case CSDGCell.CSDGCellTypeEnum.Date:
                        case CSDGCell.CSDGCellTypeEnum.Text:
                        case CSDGCell.CSDGCellTypeEnum.Label:
                            dvr.Cells[col].Value = clsClient.clsHHmem.GetDataString(colName);
                            break;
                        case CSDGCell.CSDGCellTypeEnum.ComboText:
                            theCell.SetCboTextFromChar(clsClient.clsHHmem.GetDataString(colName).Substring(0,1));
                            break;
                        default:
                            //theCell.Value = "";
                            break;
                    }
                }
            }

            clsClient.clsHHmem.find(hhMemID);
        }

        private void HHMemGridForm_Load(object sender, EventArgs e)
        {
            fillGridData();
            //Display family member
        }

        private void dgvHHM2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            clsClient.clsHHmem.SetRecord(e.ColumnIndex-1);
            string fldName = dgvHHM2.Rows[e.RowIndex].Tag.ToString();
            CSDGCell theCell = (CSDGCell)dgvHHM2[e.ColumnIndex, e.RowIndex];
            string fldText = theCell.Value.ToString();
            string fldValue = fldText;
            if (theCell.CSDGCellType == CSDGCell.CSDGCellTypeEnum.Combo)
            {
                fldValue = CCFBGlobal.IdxFromLongName(parmTableName(e.RowIndex), fldText).ToString();
                if (cboTypes[e.RowIndex] == 0)
                {
                    if (fldValue == "0")
                        fldValue = false.ToString();
                    else
                        fldValue = true.ToString();
                }
            }
            else if (theCell.CSDGCellType == CSDGCell.CSDGCellTypeEnum.ComboText)
            {
                fldValue = CCFBGlobal.ShortNameFromId(parmTableName(e.RowIndex), CCFBGlobal.IdxFromLongName(parmTableName(e.RowIndex), fldText));
            }
            clsClient.clsHHmem.SetDataValue(fldName, fldValue);
            lblNeedToSave.Visible = true;
        }

        private string parmTableName(int index)
        {
            switch (cboTypes[index])
            {
                case 0: return CCFBGlobal.parmTbl_TrueFalse;
                case 1: return CCFBGlobal.parmTbl_Gender;
                case 2: return CCFBGlobal.parmTbl_CSFPRoutes;
                case 3: return CCFBGlobal.parmTbl_YesNoUnk;
                case 4: return CCFBGlobal.parmTbl_MilitaryService;
                case 5: return CCFBGlobal.parmTbl_MilitaryDischarge;
                case 6: return CCFBGlobal.parmTbl_Employment;
                case 7: return CCFBGlobal.parmTbl_EducationLevel;
                default: return "";
            }
        }

        private void tsbEthnicity_Click(object sender, EventArgs e)
        {
            showEthnicity();
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

        private void tbBirthdate_Leave(object sender, EventArgs e)
        {
            try
            {
                DateTime tmp = Convert.ToDateTime(tbBirthdate.Text);
                if (tmp > Convert.ToDateTime(CCFBGlobal.OURNULLDATE) && tmp < DateTime.Today)
                {
                    tbBirthdate.Text = tmp.ToShortDateString();
                    tbBirthdate.ForeColor = Color.Black;
                    lblDateError.Visible = false;
                    tbAge.Text = CCFBGlobal.calcAge(tmp, DateTime.Today).ToString();
                    //FillBirthDateList(tbBirthdate.Text);
                }
                else
                {
                    tbBirthdate.ForeColor = Color.DarkRed;
                    lblDateError.Visible = true;
                }
            }
            catch
            {
                tbBirthdate.ForeColor = Color.DarkRed;
                lblDateError.Visible = true;
            }
        }
    }
}
