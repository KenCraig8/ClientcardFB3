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
    public partial class HHMemGridForm2 : Form
    {
        Client clsClient;
        CSDGSqlDataAccess csdgdataaccess = new CSDGSqlDataAccess(CCFBGlobal.connectionString);
        HHMemberItem itemHHMember;
        DataRow newRow;
        List<CheckBox> chkList = new List<CheckBox>();
        List<ComboBox> cboList = new List<ComboBox>();
        List<TextBox> tbList = new List<TextBox>();
        SqlCommand storedProcComm;
        SqlConnection conn = new SqlConnection(CCFBGlobal.connectionString);

        #region Grid Column Names
        enum dgvColNames
        {
            ID, Inactive, LastName, FirstName,
            BegInfo, UseAge, BirthDate, Age, Sex, HeadHH,
            SpecialDiet, IsDisabled, VolunteersAtFoodBank, NotIncludedInClientList, MemID, MemIDType,
            UserFlag0, UserFlag1, Notes,
            BegCSFP, CSFP, CSFPExpiration, CSFPRoute, CSFPComments,
            BegWork, WorksInArea, Employer, EmpAddress, EmpCity, EmpZipcode, EmpPhone,
            BegOption, HispanicLatino, RefugeeImmigrant, LimitedEnglish, PartneredMarried,
            Homeless, HomelessNbrTimes, HomelessNbrMonths,
            LongTermHomeless, ChronicallyHomeless, 
            BegMilitary, MilitaryService, DischargeStatus,
            BegEmployed, Employed, EmploymentStatus, EducationLevel,
            BegEthnicity, AmericanIndian, AlaskaNative, IndigenousToAmericas, AsianIndian, Cambodian, Chinese,
            Filipino, Japanese, Korean, Vietnamese, OtherAsian, IndigenousAfricanBlack, AfricanAmericanBlack,
            OtherBlack, HawaiianNative, Polynesian, Micronesian, OtherPacificIslander, ArabIranianMiddleEastern,
            OtherWhiteCaucasian, EthnicOther, EthnicUnknown
        }
        #endregion
        
        #region Grid Combo Types
        int[] cboTypes = new int[]
        {-1,0,-1,-1,
         -1,0,-1,-1,1,0,
         0,0,0,0,-1,8,
         0,0,-1,
         -1,0,-1,2,-1,
         -1,0,-1,-1,-1,-1,-1,
         -1,3,3,3,3,
         3,-1,-1,
         3,3,
         -1,4,5,
         -1,3,6,7,
         -1,0,0,0,0,0,0,
         0,0,0,0,0,0,0,
         0,0,0,0,0,0,
         0,0,0
        };
        #endregion

        #region Grid Field Names
        string[] dgvRowNames = new string[]
        {"ID","Inactive","Last Name","First Name",
         "---------------","Use Age not Birth Date","Birth Date","Age","Sex","Head Household",
         "Special Diet","Disabled","Volunteers at Food Bank","Do Not Include in Find Client List","ID Number", "ID Type",
         "UserFlag0","UserFlag1","Notes",
         "-- CSFP --","CSFP","CSFP Expiration Date","CSFP Route","CSFP Comments",
         "-- Works In Area --","Works In Area","Employer","Employer Address","Employer City","Employer Zipcode","Employer Phone",
         "-- Survey --","Hispanic","Refugee","LimitedEnglish","Married or Partnered",
         "Homeless","Homeless Nbr Times","Hoemless Nbr Months",
         "Long Term Homeless","Chronically Homeless",
         "-- Military Service --","Military Service","Discharge Status",
         "-- Employment/Educ --","Employed","Employment Status","Education Level",
         "-- Ethnicity --","American Indian (U.S. Tribe)","Alaska Native", "Indigenous To Americas (No USA)",
            "Asian Indian","Cambodian","Chinese, Except Taiwanese",
            "Filipino","Japanese","Korean",
            "Vietnamese","Other Asian","Indigenous African/Black",
            "African American/Black","Other Black","Hawaiian Native",
            "Polynesian","Micronesian","Other Pacific Islander",
            "Arab/Iranian Middle Eastern","Other/White Caucasian",
            "Other","Unknown"
        };
        #endregion

        #region Grid Field Types
        CSDGCell.CSDGCellTypeEnum[] fldTypes = new CSDGCell.CSDGCellTypeEnum[]
        {CSDGCell.CSDGCellTypeEnum.Label ,CSDGCell.CSDGCellTypeEnum.Combo,CSDGCell.CSDGCellTypeEnum.Text,CSDGCell.CSDGCellTypeEnum.Text,
         CSDGCell.CSDGCellTypeEnum.Label,CSDGCell.CSDGCellTypeEnum.Combo,CSDGCell.CSDGCellTypeEnum.Date,CSDGCell.CSDGCellTypeEnum.Text,CSDGCell.CSDGCellTypeEnum.ComboText,CSDGCell.CSDGCellTypeEnum.Combo,
         CSDGCell.CSDGCellTypeEnum.Combo,CSDGCell.CSDGCellTypeEnum.Combo,CSDGCell.CSDGCellTypeEnum.Combo,CSDGCell.CSDGCellTypeEnum.Combo,CSDGCell.CSDGCellTypeEnum.Text,CSDGCell.CSDGCellTypeEnum.Combo,
         CSDGCell.CSDGCellTypeEnum.Combo,CSDGCell.CSDGCellTypeEnum.Combo,CSDGCell.CSDGCellTypeEnum.Text,
         CSDGCell.CSDGCellTypeEnum.Label,CSDGCell.CSDGCellTypeEnum.Combo,CSDGCell.CSDGCellTypeEnum.Date,CSDGCell.CSDGCellTypeEnum.Combo,CSDGCell.CSDGCellTypeEnum.Text,
         CSDGCell.CSDGCellTypeEnum.Label,CSDGCell.CSDGCellTypeEnum.Combo,CSDGCell.CSDGCellTypeEnum.Text,CSDGCell.CSDGCellTypeEnum.Text,CSDGCell.CSDGCellTypeEnum.Text,CSDGCell.CSDGCellTypeEnum.Text,CSDGCell.CSDGCellTypeEnum.Text,
         CSDGCell.CSDGCellTypeEnum.Label,CSDGCell.CSDGCellTypeEnum.Combo,CSDGCell.CSDGCellTypeEnum.Combo,CSDGCell.CSDGCellTypeEnum.Combo,CSDGCell.CSDGCellTypeEnum.Combo,
         CSDGCell.CSDGCellTypeEnum.Combo,CSDGCell.CSDGCellTypeEnum.Text,CSDGCell.CSDGCellTypeEnum.Text,
         CSDGCell.CSDGCellTypeEnum.Combo,CSDGCell.CSDGCellTypeEnum.Combo,
         CSDGCell.CSDGCellTypeEnum.Label,CSDGCell.CSDGCellTypeEnum.Combo,CSDGCell.CSDGCellTypeEnum.Combo,
         CSDGCell.CSDGCellTypeEnum.Label,CSDGCell.CSDGCellTypeEnum.Combo,CSDGCell.CSDGCellTypeEnum.Combo,CSDGCell.CSDGCellTypeEnum.Combo,
         CSDGCell.CSDGCellTypeEnum.Label,CSDGCell.CSDGCellTypeEnum.Combo,CSDGCell.CSDGCellTypeEnum.Combo,CSDGCell.CSDGCellTypeEnum.Combo,
         CSDGCell.CSDGCellTypeEnum.Combo,CSDGCell.CSDGCellTypeEnum.Combo,CSDGCell.CSDGCellTypeEnum.Combo,
         CSDGCell.CSDGCellTypeEnum.Combo,CSDGCell.CSDGCellTypeEnum.Combo,CSDGCell.CSDGCellTypeEnum.Combo,
         CSDGCell.CSDGCellTypeEnum.Combo,CSDGCell.CSDGCellTypeEnum.Combo,CSDGCell.CSDGCellTypeEnum.Combo,
         CSDGCell.CSDGCellTypeEnum.Combo,CSDGCell.CSDGCellTypeEnum.Combo,CSDGCell.CSDGCellTypeEnum.Combo,
         CSDGCell.CSDGCellTypeEnum.Combo,CSDGCell.CSDGCellTypeEnum.Combo,CSDGCell.CSDGCellTypeEnum.Combo,
         CSDGCell.CSDGCellTypeEnum.Combo,CSDGCell.CSDGCellTypeEnum.Combo,
         CSDGCell.CSDGCellTypeEnum.Combo,CSDGCell.CSDGCellTypeEnum.Combo
        };
        #endregion


        bool bAlreadyHere = false;
        bool bchkOriValue = false;
        bool canceled = false;
        bool inEditMode = true;
        bool loadingInfo = true;
        bool needToSetTab = false;
        bool[] showColumn = new bool[68]; //This array is used to control the visibility of the grid columns
        bool[] tpgNeedToRefresh = new bool[] { false, false };

        int hhMemID;
        int newHHMID = 0;

        string baseLastName = "";
        string dateIdVerified = CCFBGlobal.OURNULLDATE;
        string stbOriValue = "";
        string userField0 = "";
        string userField1 = "";

        /// <summary>
        /// Used in initialization of the HHMemForm(ie Constructor)
        /// </summary>
        /// <param name="clsIn">The Client Class</param>
        /// <param name="frmMainIn">The Main Form-Calling Form</param>
        /// <param name="HHMemID">The Household Member ID</param>
        public HHMemGridForm2(Client clsIn, int HHMemID)
        {
            InitializeComponent();
            cboCSFPRoute.Visible = CCFBPrefs.EnableCSFPShowRoutes;
            lblCSFPRoute.Visible = CCFBPrefs.EnableCSFPShowRoutes;
            clsClient = clsIn;
            this.Text = "[ " + clsClient.clsHH.ID.ToString() + " ] Family Members Maintenance";
            showColumn[0] = false;
            for (int i = 1; i < showColumn.Length; i++)
            {
                showColumn[i] = true;
            }
            hhMemID = HHMemID;
            UserFields clsUserFields = new UserFields(CCFBGlobal.connectionString);
            clsUserFields.open("HouseholdMembers");
            userField0 = clsUserFields.GetDataValue("EditLabel", 0).ToString().Trim();
            userField1 = clsUserFields.GetDataValue("EditLabel", 1).ToString().Trim();
            clsUserFields.Dispose();
            if (String.IsNullOrEmpty(userField0)  == true)
            {
                showColumn[(int)dgvColNames.UserFlag0] = false;
                chkUserFlag0.Visible = false;
            }
            else
                chkUserFlag0.Text = userField0;

            if (String.IsNullOrEmpty(userField1)  == true)
            {
                showColumn[(int)dgvColNames.UserFlag1] = false;
                chkUserFlag1.Visible = false;
            }
            else
                chkUserFlag1.Text = userField1;

            traverseAndAddControlsToCollections(this.Controls);
            setCSFPInfoVisibility();
            setBackPackInfoVisibility();

            CCFBGlobal.InitCombo(cboMemIDType, CCFBGlobal.parmTbl_IdVerify);
            CCFBGlobal.InitCombo(cboRace, CCFBGlobal.parmTbl_Race);
            CCFBGlobal.InitCombo(cboCSFPRoute, CCFBGlobal.parmTbl_CSFPRoutes);
            CCFBGlobal.InitCombo(cboCSFPStatus, CCFBGlobal.parmTbl_CSFPStatus);
            CCFBGlobal.InitCombo(cboBPSchool, CCFBGlobal.parmTbl_BackPackSchool);
            CCFBGlobal.InitCombo(cboBPSize, CCFBGlobal.parmTbl_BackPackSize);
            CCFBGlobal.InitCombo(cboRelationship, CCFBGlobal.parmTbl_Relationship);

            CCFBGlobal.InitCombo(cboLatino, CCFBGlobal.parmTbl_YesNoUnk);
            CCFBGlobal.InitCombo(cboRefugee, CCFBGlobal.parmTbl_YesNoUnk);
            CCFBGlobal.InitCombo(cboLimitedEnglish, CCFBGlobal.parmTbl_YesNoUnk);
            CCFBGlobal.InitCombo(cboMarried, CCFBGlobal.parmTbl_YesNoUnk);
            CCFBGlobal.InitCombo(cboHomeless, CCFBGlobal.parmTbl_YesNoUnk);
            CCFBGlobal.InitCombo(cboLongTermHomeless, CCFBGlobal.parmTbl_YesNoUnk);
            CCFBGlobal.InitCombo(cboChronicallyHomeless, CCFBGlobal.parmTbl_YesNoUnk);
            CCFBGlobal.InitCombo(cboEmployed, CCFBGlobal.parmTbl_YesNoUnk);

            CCFBGlobal.InitCombo(cboEdLvl, CCFBGlobal.parmTbl_EducationLevel);
            CCFBGlobal.InitCombo(cboEmploymentStatus, CCFBGlobal.parmTbl_Employment);
            CCFBGlobal.InitCombo(cboMilitaryService, CCFBGlobal.parmTbl_MilitaryService);
            CCFBGlobal.InitCombo(cboDischargeStatus, CCFBGlobal.parmTbl_MilitaryDischarge);

            TestFeatureEnabled(tsbCSFP, (int)dgvColNames.BegCSFP, (int)dgvColNames.BegWork, CCFBPrefs.EnableCSFP);
            TestFeatureEnabled(tsbWorksInArea, (int)dgvColNames.BegWork, (int)dgvColNames.BegOption, CCFBPrefs.EnableWorksInArea);
            pnlEmployer.Visible = CCFBPrefs.EnableWorksInArea;
            TestFeatureEnabled(tsbOptional, (int)dgvColNames.BegOption, (int)dgvColNames.BegMilitary, CCFBPrefs.EnableAdditionalHHMDataTab);
            TestFeatureEnabled(tsbMilitarySvc, (int)dgvColNames.BegMilitary, (int)dgvColNames.BegEmployed, CCFBPrefs.EnableAdditionalHHMDataTab);
            TestFeatureEnabled(tsbEmployment, (int)dgvColNames.BegEmployed, (int)dgvColNames.EducationLevel+1, CCFBPrefs.EnableAdditionalHHMDataTab);

            pnlHHMFlds.BackColor = CCFBGlobal.bkColorBaseEdit;
            tpgAdditional.BackColor = CCFBGlobal.bkColorBaseEdit;
            tpgEthnicity.BackColor = CCFBGlobal.bkColorBaseEdit;
            pnlName.BackColor = CCFBGlobal.bkColorBaseEdit;
            pnlCSFPInfo.BackColor = CCFBGlobal.bkColorAltEdit;
            pnlBPInfo.BackColor = CCFBGlobal.bkColorAltEdit;
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
            //btnMarkSame.Visible = (CCFBPrefs.EnableEthnicityHHMTab == true || CCFBPrefs.EnableAdditionalHHMDataTab == true);
            btnMarkSame.Visible = (CCFBPrefs.EnableEthnicityHHMTab || CCFBPrefs.EnableAdditionalHHMDataTab);
            if (CCFBPrefs.EnableEthnicityHHMTab == false)
                tabCtrlDemographics.TabPages.RemoveByKey("tpgEthnicity");  //Remove Ethnicity Tab
            if (CCFBPrefs.EnableAdditionalHHMDataTab == false)
                tabCtrlDemographics.TabPages.RemoveByKey("tpgAdditional");  //Remove Additional Tab
            if (CCFBPrefs.EnableWorksInArea == false && CCFBGlobal.currentUser_PermissionLevel< CCFBGlobal.permissions_Admin)
                tabCtrlDemographics.TabPages.RemoveByKey("tpgWIA");  //Remove Work In Area Tab

            storedProcComm = new SqlCommand("UpdateHHMembersAdditionalData", conn);

            try
            {
                lblHouseholdData.Text = "Client ID [ " + clsClient.clsHH.ID.ToString() + " ]\r\n"
                                      + clsClient.clsHH.Name.ToString() + "\r\n"
                                      + clsClient.clsHH.Address.ToString() + "\r\n"
                                      + clsClient.clsHH.City.ToString() + ", "
                                      + clsClient.clsHH.State.ToString() + " "
                                      + clsClient.clsHH.Zipcode.ToString();
            }
            catch { }
            dgvHHM2Init(clsClient.clsHHmem.RowCount);
            string stmp = clsClient.clsHHmem.getHeadHH(clsClient.clsHH.ID);
            if (String.IsNullOrEmpty(stmp) == true)
            {
                stmp = clsClient.clsHH.Name;
            }
            if (stmp.Length >0)
            {
                string[] tmpNames = stmp.Split(',');
                baseLastName = tmpNames[0];
            }
            lvHHMembers.Dock = DockStyle.Fill;
            lvwHHMByBirthdate.Dock = DockStyle.Fill;
            lvwLastName.Dock = DockStyle.Fill;
            lvwPeople.Dock = DockStyle.Fill;
            showlvwControl(0);
            setDisplayMode(true); 
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            newHHMemberStart();
            tbLastName.Focus();
        }

        private void btnCancelAdd_Click(object sender, EventArgs e)
        {
            setDisplayMode(true);
            refreshDisplay();
        }

        private void btnSaveNewMem_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(tbAge.Text.Trim()) == true && String.IsNullOrEmpty(msktbBirthDate.Text.Trim()) == true)
            {
                MessageBox.Show("Cannot Save When Both Age And Birthdate Are Blank", this.Text);
            }
            else if (lblDateError.Visible == true && chkEnterAge.Checked == false)
            {
                MessageBox.Show("Cannot Save With Invalid Birthdate", this.Text);
                msktbBirthDate.Focus();
            }
            else
            {
                if (clsClient.clsHHmem.nameExists(tbLastName.Text, tbFirstName.Text, msktbBirthDate.Text, true) == false || CCFBPrefs.AllowDuplicateMemberNames == true)
                {
                    clsClient.clsHHmem.newHHMemberSave(itemHHMember);
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
                    loadHHMems(hhMemID);
                    fillForm();
                    
                    setDisplayMode(true);
                }
                else
                {
                    MessageBox.Show(tbFirstName.Text + " " + tbLastName.Text + "\r\nAlready Exists In Household Members Table",
                        this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    tbFirstName.Focus();
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (testNeedToSave() != DialogResult.Cancel)
            {
                lblNeedToSave.Visible = false;
                this.Close();
            }
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

        private void btnMarkSame_Click(object sender, EventArgs e)
        {
            if (lvHHMembers.SelectedItems[0] != null)
            {
                clsClient.clsHHmem.update(true);

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
                    itemHHMember = new HHMemberItem(clsClient.clsHHmem.DRowHhm, clsClient.clsHHmem.DSet.Tables[0].Columns
                                                   , clsClient.clsHHmem.DRowDemograhics, clsClient.clsHHmem.DSet.Tables[1].Columns);
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
                    clsClient.clsHHmem.update(true);
                else if (clsClient.clsHHmem.RowCount > 0)
                    clsClient.clsHHmem.rejectChanges();
            }

            this.Close();
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
                if (msktbBirthDate.Text == null && tbAge.Text == null && clsClient.clsHH.UseFamilyList == true)
                {
                    MessageBox.Show("Cannot Save Household Member When Both Age And Birthdate Are Blank");
                }
                else if (clsClient.clsHHmem.update(true) == true)
                {
                    TestFixHHName();
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

        private void cboList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (loadingInfo == false)
            {
                ComboBox cb = (ComboBox)sender;
                itemHHMember.SetDataValue(cb.Tag.ToString(),
                            cb.SelectedValue.ToString());
                lblNeedToSave.Visible = true;
            }
        }

        private void chkCSFP_CheckedChanged(object sender, EventArgs e)
        {
            if (inEditMode == true)
            {
                if (chkCSFP.Checked == itemHHMember.CSFP)
                    chkCSFP.BackColor = CCFBGlobal.bkColorAltEdit;
                else
                    chkCSFP.BackColor = Color.Pink;
            }
            toggleCSFPFields(chkCSFP.Checked);
        }

        private void chkDoNotIncldeFamList_CheckedChanged(object sender, EventArgs e)
        {
            if (inEditMode == true)
            {
                EnableMemIdFields();
            }
        }

        private void chkEnterAge_CheckedChanged(object sender, EventArgs e)
        {
            itemHHMember.UseAge = chkEnterAge.Checked;
            setupBirthdateAndAge(itemHHMember.UseAge);
        }

        private void chkHeadHH_CheckedChanged(object sender, EventArgs e)
        {
            if (loadingInfo == false)
            {
                if (chkHeadHH.Checked == true)
                {
                    itemHHMember.HeadHH = true;
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
                    itemHHMember.HeadHH = false;
                }
                loadHHMems(hhMemID);
            }
        }

        private void chkList_CheckStateChanged(object sender, EventArgs e)
        {
            CheckBox chkBox = (CheckBox)sender; //Get the correct textbox

            if (inEditMode == true)
            {
                if (chkBox.Tag != null && chkBox.Tag.ToString().Trim().Length >0)
                {
                    if (chkBox.Checked != bchkOriValue)
                    {
                        if (lvHHMembers.SelectedItems.Count > 0)
                        {
                            lvHHMembers.SelectedItems[0].ImageIndex = 0;
                        }
                        itemHHMember.SetDataValue(chkBox.Tag.ToString(), chkBox.Checked.ToString());
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

        private void chkList_Enter(object sender, EventArgs e)
        {
            bchkOriValue = ((CheckBox)sender).Checked;
        }

        private void chkList_KeyDown(object sender, KeyEventArgs e)
        {
            if (inEditMode == true && e.KeyCode == Keys.Enter)
            {
                CheckBox chkHH = (CheckBox)sender;
                chkHH.Checked = !chkHH.Checked;
            }
        }


        private void chkList_Leave(object sender, EventArgs e)
        {
            ////CheckBox chkBox = (CheckBox)sender; //Get the correct textbox

            ////if (inEditMode == true)
            ////{
            ////    if (chkBox.Tag != null && chkBox.Tag.ToString().Trim().Length >0)
            ////    {
            ////        if (chkBox.Checked != bchkOriValue)
            ////        {
            ////            lvHHMembers.SelectedItems[0].ImageIndex = 0;
            ////            clsClient.clsHHmem.SetDataValue(chkBox.Tag.ToString(), chkBox.Checked.ToString());
            ////            chkBox.BackColor = Color.Pink;
            ////            lblNeedToSave.Visible = true;
            ////        }
            ////        else
            ////        {
            ////            chkBox.BackColor = CCFBGlobal.bkColorBaseEdit;
            ////        }
            ////    }
            ////}
        }

        private void chkSurveyComplete_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSurveyComplete.Focused == true)
            {
                clsClient.clsHH.SurveyComplete = chkSurveyComplete.Checked;
                clsClient.clsHH.update(true);
            }
        }

        private void chkWorksInArea_CheckedChanged(object sender, EventArgs e)
        {
            itemHHMember.WorksInArea = chkWorksInArea.Checked;
            toggleEmployerFields(chkWorksInArea.Checked);
        }

        private void clearForm()
        {
            foreach (TextBox tb in tbList)
            {
                if (tb.Tag != null && tb.Tag.ToString().Trim().Length >0)
                    tb.Text = "";
                tb.Enabled = false;
            }

            foreach (CheckBox chk in chkList)
            {
                if (chk.Tag != null && chk.Tag.ToString().Trim().Length >0)
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
                cb.SelectedValue = "0";
                cboCSFPRoute.Enabled = false;
            }

            toggleEmployerFields(false);
            toggleCSFPFields(false);
            toggleBackPackFields(false);
            setupBirthdateAndAge(false);
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
                    if (tblName.Length >0)
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

        private void EnableMemIdFields()
        {
            bool isVisible = (chkDoNotIncldeFamList.Checked == false && CCFBPrefs.EnableIDFlds == true);
            tbMemIdNbr.Visible = isVisible;
            lblMemIdNbr.Visible = isVisible;
            lblMemIdType.Visible = isVisible;
            cboMemIDType.Visible = isVisible;
        }

        private void fillForm()
        {
            inEditMode = false;
            loadingInfo = true;
            showlvwControl(0);
            if (itemHHMember.ID >= 0)
            {
                lblHhMCreated.Text = "Created: " + itemHHMember.Created.ToShortDateString() + " " + itemHHMember.Created.ToShortTimeString() + "  " + itemHHMember.CreatedBy;
                lblHhMModified.Text = "Modified: " + itemHHMember.Modified.ToShortDateString() + " " + itemHHMember.Modified.ToShortTimeString() + "  " + itemHHMember.ModifiedBy;
                btnMarkSame.Text = btnMarkSame.Tag.ToString() + " " + itemHHMember.LastName + ", " + itemHHMember.FirstName;
                tbID.Text = itemHHMember.ID.ToString();
                foreach (TextBox tb in tbList)
                {
                    if (tb.Tag != null && tb.Tag.ToString().Trim().Length >0)
                        tb.Text = itemHHMember.GetDataString(tb.Tag.ToString());
                    tb.Enabled = true;
                    tb.ForeColor = Color.Black;
                    tb.BackColor = Color.White;
                }
                msktbBirthDate.Text = CCFBGlobal.ValidDateString(itemHHMember.BirthDate);
                msktbBirthDate.BackColor = Color.White;
                foreach (CheckBox chk in chkList)
                {
                    if (chk.Tag != null && chk.Tag.ToString().Trim().Length >0)
                        chk.Checked = CCFBGlobal.NullToFalse(itemHHMember.GetDataValue(chk.Tag.ToString()));
                    chk.Enabled = true;
                    chk.ForeColor = Color.Black;
                    chk.BackColor = CCFBGlobal.bkColorBaseEdit; 
                }

                foreach (ListViewItem lvi in lvEthnicity.Items)
                {
                    lvEthnicity.Items[lvi.Index].Checked =
                        (bool)CCFBGlobal.NullToFalse(itemHHMember.GetDataValue(lvi.Tag.ToString()));
                }
                lvEthnicity.Enabled = true;
                foreach (ComboBox cb in cboList)
                {
                    cb.SelectedValue = itemHHMember.GetDataValue(cb.Tag.ToString()).ToString();
                }
                cboCSFPRoute.Enabled = true;
                toggleEmployerFields(itemHHMember.WorksInArea);
                setupBirthdateAndAge(itemHHMember.UseAge);
                tbExpires.Text = CCFBGlobal.ValidDateString(itemHHMember.CSFPExpiration);
                toggleCSFPFields(chkCSFP.Checked);
                toggleBackPackFields(chkBackPack.Checked);
                EnableMemIdFields();
                btnDelete.Enabled = (clsClient.clsHHmem.RowCount > 1);
                btnReset.Enabled = true;
                btnSave.Enabled = true;
                btnPrint.Enabled = true;
                inEditMode = true;
            }
            else
            {
                clearForm();
                inEditMode = false;
                btnDelete.Enabled = false;
                btnReset.Enabled = false;
                btnSave.Enabled = false;
                btnPrint.Enabled = false;
            }
            tpgNeedToRefresh[0] = false;
            lblNeedToSave.Visible = clsClient.clsHHmem.HasChanges;
            loadingInfo = false;
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
                            if (String.IsNullOrEmpty(newvalue) == true)
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

        private bool FilllvwHHMByBirthdate(string birthdate)
        {
            bool bExactMatch = false;
            lvwHHMByBirthdate.Items.Clear();
            //tpgSameBirthDate.Text = "Loading ...";
            Application.DoEvents();
            lblErrSameNameBirth.Visible = false;
            //Font ft = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            string sqlText = "SELECT LastName, FirstName, hhm.HouseholdID HHID, hhm.Inactive HHMInactive"
                           + ", hh.name, hh.address, hh.City, hh.Zipcode, hh.Inactive HHInactive, hhm.ID HHMID"
                           + " FROM HouseholdMembers hhm INNER JOIN Household hh ON hhm.HouseholdID = hh.ID"
                           + " WHERE Birthdate = '" + birthdate + "' AND hhm.ID <> " + tbID.Text + " ORDER BY hh.Inactive, hh.name";
            DataTable dtblwrk = csdgdataaccess.TransferDataToLocalDataTable(sqlText);
            if (dtblwrk.Rows.Count > 0)
            {
                //tpgSameBirthDate.Text = " [ " + dtblwrk.Rows.Count.ToString() + " ] " + tpgSameBirthDate.Tag.ToString();
                //lvwHHMByBirthdate.Visible = true;
                //lvwHHMByBirthdate.BackColor = Color.Ivory;

                foreach (DataRow drow in dtblwrk.Rows)
                {
                    ListViewItem lvItm = new ListViewItem(drow.Field<string>("LastName"));
                    lvItm.SubItems.Add(drow.Field<string>("FirstName"));
                    lvItm.SubItems.Add(Convert.ToInt32(drow["HHID"]).ToString());
                    lvItm.SubItems.Add(Convert.ToInt32(drow["HHMID"]).ToString());
                    lvItm.SubItems.Add(CCFBGlobal.IsInactiveString(Convert.ToBoolean(drow["HHInactive"])));
                    lvItm.SubItems.Add(CCFBGlobal.IsInactiveString(Convert.ToBoolean(drow["HHMInactive"])));
                    lvItm.SubItems.Add(drow.Field<string>("Name"));
                    lvItm.SubItems.Add(drow.Field<string>("Address"));
                    lvItm.SubItems.Add(drow.Field<string>("City"));
                    lvItm.SubItems.Add(drow.Field<string>("ZipCode"));
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
                        lblErrSameNameBirth.Visible = true;
                        lvItm.BackColor = Color.LightPink;
                    }
                    
                    lvwHHMByBirthdate.Items.Add(lvItm);
                }
            }
            else
            {
                //tpgSameBirthDate.Text = "";
                //lvwHHMByBirthdate.Visible = false;
            }
            btnSaveNewMem.Enabled = (bExactMatch == false);
            return bExactMatch;
        }

        private void HHMemGridForm_Load(object sender, EventArgs e)
        {
            lvHHMembers.Focus(); 
            loadHHMems(hhMemID);

            fillGridData();
        }

        private void HHMemGridForm2_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = (testNeedToSave() == DialogResult.Cancel);
        }

        private void loadHHMems(int setSelectedID)
        {
            //DataGridViewRow dvr;
            lvHHMembers.Items.Clear();
            int newIndex = 0;
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
                    sText = "HeadHh";
                else if (clsClient.clsHHmem.NotIncludedInClientList == true)
                    sText = "NoList";
                lvi.SubItems.Add(sText);
                lvi.SubItems.Add("");
                lvi.SubItems.Add("");
                lvi.Tag = clsClient.clsHHmem.ID;
                lvHHMembers.Items.Add(lvi);
                if (setSelectedID == clsClient.clsHHmem.ID)
                { newIndex = i; }
            }
            if (newIndex < lvHHMembers.Items.Count)
            {
                clsClient.clsHHmem.SetRecord(newIndex);
                loadingInfo = false;
                lvHHMembers.Items[newIndex].Selected = true;
                if (Convert.ToInt32(lvHHMembers.SelectedItems[0].Tag.ToString()) != hhMemID)
                {
                    refreshDisplay();
                }
            }
        }

        private void lvEthnicity_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (loadingInfo == false)
            {
                itemHHMember.SetDataValue(e.Item.Tag.ToString(), e.Item.Checked);
                lblNeedToSave.Visible = true;
            }
        }

        private void lvHHMembers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (loadingInfo == false && lvHHMembers.SelectedItems.Count>0)
            {
                refreshDisplay();
            }
        }

        private void refreshDisplay()
        {
            hhMemID = Convert.ToInt32(lvHHMembers.SelectedItems[0].Tag.ToString());
            clsClient.clsHHmem.find(hhMemID);
            itemHHMember = new HHMemberItem(clsClient.clsHHmem.DRowHhm
                                          , clsClient.clsHHmem.DSet.Tables[0].Columns
                                          , clsClient.clsHHmem.DRowDemograhics
                                          , clsClient.clsHHmem.DSet.Tables[1].Columns);
            fillForm();
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

        private void newHHMemberStart()
        {
            setDisplayMode(false);
            showlvwControl(4);
            if (clsClient.clsHHmem.RowCount > 0)
                clsClient.clsHHmem.update(true);

            itemHHMember = new HHMemberItem(clsClient.clsHHmem.addHHMember(clsClient.clsHH.ID, baseLastName)
                                          , clsClient.clsHHmem.DSet.Tables[0].Columns
                                          , clsClient.clsHHmem.addHHMDemographics(0)
                                          , clsClient.clsHHmem.DSet.Tables[1].Columns);
            fillForm();

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
                case 8: return CCFBGlobal.parmTbl_IdVerify;
                default: return "";
            }
        }

        private void setBackPackInfoVisibility()
        {
            pnlBPInfo.Visible = (CCFBPrefs.EnableBackPack == true);
            if (CCFBPrefs.EnableBackPack == true)
                toggleBackPackFields(chkBackPack.Checked);
        }

        private void setCSFPInfoVisibility()
        {
            pnlCSFPInfo.Visible = (CCFBPrefs.EnableCSFP == true);
            if (CCFBPrefs.EnableCSFP == true)
                toggleCSFPFields(chkCSFP.Checked);
        }

        private void setDisplayMode(Boolean bEditMode)
        {
            inEditMode = true;
            pnlAddNew.Visible = (bEditMode == false);
            pnlEditBtns.Visible = (bEditMode == true);
            //lvHHMembers.Visible = (bEditMode == true);
            //toolStrip2.Visible = (bEditMode == false);
        }

        private void SetFieldsBasedOnAge(string testAge)
        {
            try
            {
                int iAge = Convert.ToInt32(testAge);
                chkDoNotIncldeFamList.Checked = ((iAge < 18) == true);
                itemHHMember.NotIncludedInClientList = ((iAge < 18) == true);
            }
            catch (Exception)
            {
            }
        }

        private void setupBirthdateAndAge(bool useAge)
        {
            tbAge.Enabled = useAge;
            msktbBirthDate.Enabled = (useAge == false);
            if (useAge == false)
            {
                //tbBirthdate.ReadOnly = false;
                msktbBirthDate.ForeColor = Color.Black;
                //tbAge.ReadOnly = true;
                tbAge.BackColor = Color.White;
                tbAge.ForeColor = CCFBGlobal.AgeBirthdateColor;
            }
            else
            {
                //tbBirthdate.ReadOnly = true;
                msktbBirthDate.BackColor = Color.White;
                msktbBirthDate.ForeColor = CCFBGlobal.AgeBirthdateColor;
                lblDateError.Visible = false;
                tbAge.ForeColor = Color.Black;
            }
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

        //private void dgvHHM_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (dgvOldOne.Columns[e.ColumnIndex].DataPropertyName.Trim().Length >0)
        //    {
        //        changesInGridMade = true;
        //        clsClient.clsHHmem.SetDataValue(dgvOldOne.Columns[e.ColumnIndex].DataPropertyName,dgvOldOne[e.ColumnIndex, e.RowIndex].Value.ToString());
        //        clsClient.clsHHmem.update(true);
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

        private void showWorksInArea()
        {
            ToggleRows(tsbWorksInArea.Checked, (int)dgvColNames.BegWork, (int)dgvColNames.BegOption);
        }

        private void tabCtrl_SelectedIndexChanged(object sender, EventArgs e)
        {
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

        
        private void tbAge_Leave(object sender, EventArgs e)
        {
            if (tbAge.Text.Length >0 && tbAge.Enabled == true)
            {
                if (itemHHMember.UseAge == true)
                {
                    itemHHMember.BirthDate = CCFBGlobal.calcBirthdateFromAge(Convert.ToInt32(tbAge.Text));
                    msktbBirthDate.ForeColor = Color.Aqua;
                    msktbBirthDate.Text = itemHHMember.BirthDate.ToShortDateString();
                    SetFieldsBasedOnAge(tbAge.Text);
                }
            }
            else if (tbAge.Enabled == true)
            {
                tbAge.Text = "0";
            }
        }

        private void tbBirthDate_Enter(object sender, EventArgs e)
        {
            stbOriValue = msktbBirthDate.Text;
        }

        private void tbBirthdate_Leave(object sender, EventArgs e)
        {
            try
            {
                DateTime tmp = Convert.ToDateTime(msktbBirthDate.Text);
                if (tmp > CCFBGlobal.FBNullDateValue && tmp < DateTime.Today)
                {
                    if (stbOriValue != msktbBirthDate.Text)
                    {
                        msktbBirthDate.Text = tmp.ToShortDateString();
                        msktbBirthDate.ForeColor = Color.Black;
                        lblDateError.Visible = false;
                        if (lvHHMembers.SelectedItems.Count > 0)
                        {
                            lvHHMembers.SelectedItems[0].ImageIndex = 0;
                        }
                        itemHHMember.SetDataValue(msktbBirthDate.Tag.ToString(), msktbBirthDate.Text);
                        msktbBirthDate.BackColor = Color.Pink;
                        lblNeedToSave.Visible = true;

                        tbAge.Text = CCFBGlobal.calcAge(tmp, DateTime.Today).ToString();
                        tbList_Leave(tbAge, null);
                        SetFieldsBasedOnAge(tbAge.Text);
                        EnableMemIdFields();
                        FilllvwHHMByBirthdate(msktbBirthDate.Text);
                        showlvwControl(3);
                    }
                }
                else
                {
                    msktbBirthDate.ForeColor = Color.DarkRed;
                    lblDateError.Visible = true;
                }
            }
            catch
            {
                msktbBirthDate.ForeColor = Color.DarkRed;
                lblDateError.Visible = true;
            }
        }

        private void tbBirthdate_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (itemHHMember.UseAge == false)
            {
                try
                {
                    DateTime.Parse(msktbBirthDate.Text);
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
                        msktbBirthDate.Text = itemHHMember.GetDataString(msktbBirthDate.Tag.ToString());
                    }
                }
            }
        }

        private void tbCancel_Click(object sender, EventArgs e)
        {
            canceled = true;
            this.Close();
        }

        private void tbFirstName_Leave(object sender, EventArgs e)
        {
            if (bAlreadyHere == false)
            {
                bAlreadyHere = true;
                string tmp = tbFirstName.Text.Trim();
                lblDupHHMError.Visible = false;
                if (tmp.Length >0)
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
                        btnSaveNewMem.Enabled = true;
                        if (TestHHMName(1, lvwPeople) && CCFBPrefs.AllowDuplicateMemberNames == false)
                        {
                            btnSaveNewMem.Enabled = false;
                            lblDupHHMError.Visible = true;
                        }
                        else
                        {
                            btnSaveNewMem.Enabled = true;
                        }
                        showlvwControl(2);
                    }
                }
                bAlreadyHere = false;
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
                TestHHMName(0, lvwLastName);
                showlvwControl(1);
            }
        }

        private void tbList_Enter(object sender, EventArgs e)
        {
            stbOriValue = ((TextBox)sender).Text.Trim();
        }

        private void tbList_Leave(object sender, EventArgs e)
        {
            TextBox tbHHM = (TextBox)sender; //Get the correct textbox

            if (inEditMode == true)
            {
                if (tbHHM.Tag != null && tbHHM.Tag.ToString().Trim().Length >0)
                    if (tbHHM.Text.Trim() != stbOriValue)
                    {
                        if (lvHHMembers.SelectedItems.Count > 0)
                        {
                            lvHHMembers.SelectedItems[0].ImageIndex = 0;
                        }
                        itemHHMember.SetDataValue(tbHHM.Tag.ToString(), tbHHM.Text);
                        tbHHM.BackColor = Color.Pink;
                        lblNeedToSave.Visible = true;
                    }
            }
        }

        //private void tbMemIdNbr_Enter(object sender, EventArgs e)
        //{
        //    stbOriValue = tbMemIdNbr.Text;
        //}

        private void tbMemIdNbr_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                if (tbMemNbrISOk() == true)
                    chkWorksAtFB.Focus();
            }
        }

        private void tbMemIdNbr_Leave(object sender, EventArgs e)
        {
            if (tbMemIdNbr.Text != stbOriValue)
            {
                if (tbMemNbrISOk() == true)
                    chkWorksAtFB.Focus();
            }
        }

        private bool tbMemNbrISOk()
        {
            if (tbMemIdNbr.Text != stbOriValue)
            {
                tbMemIdNbr.Text = tbMemIdNbr.Text.ToUpper();
                if (tbMemIdNbr.Text.Length >0)
                {
                    int testHHMID = 0;
                    int testID = CCFBGlobal.getHHFromBarCode(tbMemIdNbr.Text, ref testHHMID);
                    if (testID == 0 || testHHMID == clsClient.clsHHmem.ID)
                    {
                        cboMemIDType.SelectedValue = "1";
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("already on file");
                        return false;
                    }
                }
            }
            return true;
        }

        private void tbSex_Enter(object sender, EventArgs e)
        {
            tbSex.SelectAll();
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

        private void tbSex_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar > 96)
            {
                e.KeyChar = Convert.ToChar(Convert.ToInt32(e.KeyChar) - 32);
            }
        }

        private void TestFeatureEnabled(ToolStripButton tsb, int istart, int iend, bool isVisible)
        {
            tsb.Visible = isVisible;
            for (int i = istart; i < iend; i++)
            {
                showColumn[i] = isVisible;
            }
        }

        private bool TestHHMName(int iMode, ListView lvwGrid)
        {
            int lvwIndex = -1;
            string sWhereClause;
            bool bIsExactMatch = false;
            lvwGrid.Items.Clear();
            //tpg.Text = "Loading ..";
            Application.DoEvents();
            switch (iMode)
            {
                case 1:
                    sWhereClause = "SoundEx(LastName) = SoundEx('" + tbLastName.Text + "') AND SoundEx(FirstName) = SoundEx('" + tbFirstName.Text + "')";
                    break;
                default:
                    sWhereClause = "SoundEx(LastName) = SoundEx('" + tbLastName.Text + "')";
                    break;
            }
            //Font ft = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            string sqlText = "SELECT LastName, FirstName, hhm.HouseholdID HHID, hhm.Inactive HHMInactive, hhm.BirthDate"
                           + ", hh.name, hh.address, hh.City, hh.Zipcode, hh.Inactive HHInactive, hhm.ID HHMID"
                           + ", (SELECT Count(*) FROM TrxLog WHERE HouseholdId = hhm.HouseholdId"
                           + " AND TrxDate Between '" + CCFBGlobal.CurrentFiscalStartDate() + "' AND '" + CCFBGlobal.CurrentFiscalEndDate() + "') NbrSvcs"
                           + " FROM HouseholdMembers hhm INNER JOIN Household hh ON hhm.HouseholdID = hh.ID"
                           + " WHERE " + sWhereClause + " AND hhm.ID <> " + tbID.Text + " ORDER BY hh.Name, hhm.LastName, hhm.FirstName";

            DataTable dtblwrk = csdgdataaccess.TransferDataToLocalDataTable(sqlText);
            if (dtblwrk.Rows.Count > 0)
            {
                //tpg.Text = " [ " + dtblwrk.Rows.Count.ToString() + " ] " + tpg.Tag.ToString();
                lvwGrid.Visible = true;
                lvwGrid.BackColor = Color.Ivory;

                foreach (DataRow drow in dtblwrk.Rows)
                {
                    ListViewItem lvItm = new ListViewItem(drow.Field<string>("LastName"));
                    lvItm.SubItems.Add(drow.Field<string>("FirstName"));
                    lvItm.SubItems.Add(CCFBGlobal.ValidDateString(drow["BirthDate"]));
                    lvItm.SubItems.Add(Convert.ToInt32(drow["HHID"]).ToString());
                    lvItm.SubItems.Add(Convert.ToInt32(drow["HHMID"]).ToString());
                    //lvItm.SubItems.Add(CCFBGlobal.IsInactiveString(Convert.ToBoolean(drow["HHMInactive"])));
                    lvItm.SubItems.Add(drow.Field<string>("Name"));
                    lvItm.SubItems.Add(drow["NbrSvcs"].ToString());
                    lvItm.SubItems.Add(CCFBGlobal.IsInactiveString(Convert.ToBoolean(drow["HHInactive"])));
                    lvItm.SubItems.Add(drow.Field<string>("Address"));
                    lvItm.SubItems.Add(drow.Field<string>("City"));
                    lvItm.SubItems.Add(drow.Field<string>("ZipCode"));
                    
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
                //tpg.Text = "no matches";
                needToSetTab = false;
            }

            //tabControl1.SelectedIndex = iMode;
            //Application.DoEvents();
            return bIsExactMatch;
        }

        
        private DialogResult testNeedToSave()
        {
            if (lblNeedToSave.Visible == true)
            {
                DialogResult dResult = MessageBox.Show("Do you want to save changes?", "Close Form", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                switch (dResult)
                {
                    case DialogResult.Yes:
                        {
                            if (clsClient.clsHHmem.update(true) == true)
                            {
                                TestFixHHName();
                                updateHouseholdData();
                            }
                            break;
                        }
                    case DialogResult.No:
                        if (clsClient.clsHHmem.RowCount > 0)
                            clsClient.clsHHmem.rejectChanges();
                        break;
                    default:
                        break;
                }
                return dResult; 
            }
            else
                return DialogResult.No;
        }

        private void toggleBackPackFields(bool isVisible)
        {
            lblBPExpires.Visible = isVisible;
            lblBPSize.Visible = isVisible;
            lblBPSchool.Visible = isVisible;
            lblBPNotes.Visible = isVisible;
            tbBPExpires.Visible = isVisible;
            cboBPSize.Visible = isVisible;
            cboBPSchool.Visible = isVisible;
            tbBPNotes.Visible = isVisible;
        }

        private void toggleCSFPFields(bool isVisible)
        {
            lblCSFPStatus.Visible = isVisible;
            lblCSFPRoute.Visible = isVisible; 
            lblCSFPExpires.Visible = isVisible;
            lblCSFPComments.Visible = isVisible;
            cboCSFPStatus.Visible = isVisible;
            cboCSFPRoute.Visible = isVisible;
            tbExpires.Visible = isVisible;
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

        private void ToggleRows(bool isVisible, int firstRow, int lastRow)
        {
            for (int i = firstRow; i < lastRow; i++)
            {
                dgvHHM2.Rows[i].Visible = isVisible && showColumn[i];
            }
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
                            if (cntrl.Tag != null && cntrl.Tag.ToString().Trim().Length >0)
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
                            if (cntrl.Tag != null && cntrl.Tag.ToString().Trim().Length >0)
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
                                if (cntrl.Tag.ToString().Trim().Length >0)
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

        private void tsbEmployment_Click(object sender, EventArgs e)
        {
            showEmployment();
        }

        private void tsbEthnicity_Click(object sender, EventArgs e)
        {
            showEthnicity();
        }

        private void tsbMilitarySvc_Click(object sender, EventArgs e)
        {
            showMilitaryService();
        }

        private void tsbNotes_Click(object sender, EventArgs e)
        {
            showNotes();
        }

        private void tsbOptional_Click(object sender, EventArgs e)
        {
            showOptional();
        }

        private void tsbWorksInArea_Click(object sender, EventArgs e)
        {
            showWorksInArea();
        }

        private void updateDemographicsWSame()
        {
            if (lvHHMembers.SelectedItems[0] != null)
            {
                clsClient.clsHHmem.update(true);
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
                sqlCmd.Dispose();
            }
        }

        private void updateHouseholdData()
        {
            clsClient.UpdateDataBasedOn(DateTime.Parse(CCFBGlobal.DefaultServiceDate));
        }


        private void showlvwControl(int mode)
        {
            //lvHHMembers.Visible = false;
            lvwLastName.Visible = false;
            lvwPeople.Visible = false;
            lvwHHMByBirthdate.Visible = false;
            //setTSBUnchecked(tsbFamilyList);
            setTSBUnchecked(tsbLastName);
            setTSBUnchecked(tsbPeople);
            setTSBUnchecked(tsbBirthDate);
            switch (mode)
            {
                case 0:
                    toolStrip2.Visible = false;
                    splitContainer3.Panel2Collapsed = true;
                //    lvHHMembers.Visible = true;
                //    setTSBChecked(tsbFamilyList);
                    break;
                case 1:
                    lvwLastName.Visible = true;
                    setTSBChecked(tsbLastName);
                    toolStrip2.Visible = true;
                    splitContainer3.Panel2Collapsed = false;
                    break;
                case 2:
                    lvwPeople.Visible = true;
                    setTSBChecked(tsbPeople);
                    toolStrip2.Visible = true;
                    splitContainer3.Panel2Collapsed = false;
                    break;
                case 3:
                    lvwHHMByBirthdate.Visible = true;
                    setTSBChecked(tsbBirthDate);
                    toolStrip2.Visible = true;
                    splitContainer3.Panel2Collapsed = false;
                    break;
                default:
                    break;
            }
        }

        private void setTSBUnchecked(ToolStripButton tsb)
        {
            tsb.Checked = false;
            tsb.ForeColor = Color.Black;
        }

        private void setTSBChecked(ToolStripButton tsb)
        {
            tsb.Checked = true;
            tsb.ForeColor = Color.DarkBlue;
        }

        private void tsbLastName_Click(object sender, EventArgs e)
        {
            showlvwControl(1);
        }

        private void tsbPeople_Click(object sender, EventArgs e)
        {
            showlvwControl(2);
        }

        private void tsbBirthDate_Click(object sender, EventArgs e)
        {
            showlvwControl(3);
        }

        private void tsbFamilyList_Click(object sender, EventArgs e)
        {
            showlvwControl(0);
        }

        private void TestFixHHName()
        {
            if (clsClient.clsHHmem.HeadHH == true)
            {
                if (clsClient.clsHH.Name != clsClient.clsHHmem.HHName)
                {
                    clsClient.clsHH.Name = clsClient.clsHHmem.HHName;
                    clsClient.clsHH.update(true);
                }
            }
        }

        private void chkBackPack_CheckedChanged(object sender, EventArgs e)
        {
            if (inEditMode == true)
            {
                if (chkBackPack.Checked == itemHHMember.CSFP)
                    chkBackPack.BackColor = CCFBGlobal.bkColorAltEdit;
                else
                    chkBackPack.BackColor = Color.Pink;
            }
            toggleBackPackFields(chkBackPack.Checked);
        }

        private void pnlCSFPInfo_Paint(object sender, PaintEventArgs e)
        {
            
        }
        //private void dgvHHM_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (dgvOldOne.Columns[e.ColumnIndex].DataPropertyName.Trim().Length >0)
        //    {
        //        changesInGridMade = true;
        //        clsClient.clsHHmem.SetDataValue(dgvOldOne.Columns[e.ColumnIndex].DataPropertyName,dgvOldOne[e.ColumnIndex, e.RowIndex].Value.ToString());
        //        clsClient.clsHHmem.update(true);
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
    }
}
