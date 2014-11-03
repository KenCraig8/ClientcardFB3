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

        int hhMemID;
        string stbOriValue = "";
        bool bchkOriValue = false;
        bool inEditMode = true;
        bool loadingInfo = true;
        //This array is used to control the visibility of the grid columns
        bool[] showColumn = new bool[43];

        #region Grid Column Names
        enum dgvColNames
        {
            colHhMID,
            colInactive,
            colLastName,
            colFirstName,
            colBegInfo,
            colUseAge,
            colBirthDate,
            colAge,
            colSex,
            colHeadHh,
            colSplDiet,
            colDisabled,
            colVolunteer,
            colDoNotInclude,
            colUserFlag0,
            colUserFlag1,
            colNotes,
            colBegCSFP,
            colCSFP,
            colCSFPExpire,
            colCSFPRoute,
            colCSFPComments,
            colBegWork,
            colWorksInArea,
            colEmployer,
            colEmplAddress,
            colEmpCity,
            colEmpZipcode,
            colEmpPhone,
            colBegOption,
            colHispanic,
            colRefugee,
            colLimitedEnglish,
            colMarried,
            colLTHomeless,
            colChronicallyHomeless,
            colBegMilitary,
            colMilSvc,
            colDischargeStatus,
            colBegEmployed,
            colEmployed,
            colEmploymentStatus,
            colEducationLevel
        }
        #endregion

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
            showColumn[0] = false;
            for (int i = 1; i < showColumn.Length; i++)
            {
                showColumn[i] = true;
            }
            hhMemID = HHMemID;
            UserFields clsUserFields = new UserFields(CCFBGlobal.connectionString);
            clsUserFields.open("HouseholdMembers");

            string userField0 = clsUserFields.GetDataValue("EditLabel", 0).ToString();
            string userField1 = clsUserFields.GetDataValue("EditLabel", 1).ToString();
            if (userField0 == "")
            {
                showColumn[(int)dgvColNames.colUserFlag0] = false;
                chkUserFlag0.Visible = false;
            }
            else
                chkUserFlag0.Text = dgvHHM.Columns["colUserFlag0"].ToolTipText = userField0;

            if (userField1 == "")
            {
                showColumn[(int)dgvColNames.colUserFlag1] = false;
                chkUserFlag1.Visible = false;
            }
            else
                chkUserFlag1.Text = dgvHHM.Columns["colUserFlag1"].ToolTipText = userField1;

            traverseAndAddControlsToCollections(this.Controls);
            setCSFPInfoVisibility();

            CCFBGlobal.InitCombo(cboEdLvl, CCFBGlobal.parmTbl_EducationLevel);
            CCFBGlobal.InitCombo(cboEmploymentStatus, CCFBGlobal.parmTbl_Employment);
            CCFBGlobal.InitCombo(cboMilitaryService, CCFBGlobal.parmTbl_MilitaryService);
            CCFBGlobal.InitCombo(cboDischargeStatus, CCFBGlobal.parmTbl_MilitaryDischarge);
            CCFBGlobal.InitCombo(cboCSFPRoute, CCFBGlobal.parmTbl_CSFPRoutes);

            TestFeatureEnabled(tsbCSFP, (int)dgvColNames.colBegCSFP, (int)dgvColNames.colBegWork, CCFBPrefs.EnableCSFP);
            TestFeatureEnabled(tsbWorksInArea, (int)dgvColNames.colBegWork, (int)dgvColNames.colBegOption, CCFBPrefs.EnableWorksInArea);
            pnlEmployer.Visible = CCFBPrefs.EnableWorksInArea;
            TestFeatureEnabled(tsbOptional, (int)dgvColNames.colBegOption, (int)dgvColNames.colBegMilitary, CCFBPrefs.EnableAdditionalHHMDataTab);
            TestFeatureEnabled(tsbMilitarySvc, (int)dgvColNames.colBegMilitary, (int)dgvColNames.colBegEmployed, CCFBPrefs.EnableAdditionalHHMDataTab);
            TestFeatureEnabled(tsbEmployment, (int)dgvColNames.colBegEmployed, dgvHHM.Columns.Count, CCFBPrefs.EnableAdditionalHHMDataTab);

            pnlHHMFlds.BackColor = CCFBGlobal.bkColorBaseEdit;
            tpgAdditional.BackColor = CCFBGlobal.bkColorBaseEdit;
            tpgEthnicity.BackColor = CCFBGlobal.bkColorBaseEdit;
            pnlCSFPInfo.BackColor = CCFBGlobal.bkColorAltEdit;
            pnlEmployer.BackColor = CCFBGlobal.bkColorAltEdit;
            if (CCFBPrefs.EnableAdditionalHHMDataTab == true)
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


            setupComboxBox(dgvColNames.colCSFPRoute.ToString(), CCFBGlobal.parmTbl_CSFPRoutes);
            setupComboxBox(dgvColNames.colMilSvc.ToString(), CCFBGlobal.parmTbl_MilitaryService);
            setupComboxBox(dgvColNames.colDischargeStatus.ToString(), CCFBGlobal.parmTbl_MilitaryDischarge);
            setupComboxBox(dgvColNames.colEmploymentStatus.ToString(), CCFBGlobal.parmTbl_Employment);
            setupComboxBox(dgvColNames.colEducationLevel.ToString(), CCFBGlobal.parmTbl_EducationLevel);

            try
            {
                tbHouseholdData.Text = "[ " + clsClient.clsHH.ID.ToString() + " ]\r\n"
                                     + clsClient.clsHH.Name.ToString() + "\r\n\r\n"
                                     + clsClient.clsHH.Address.ToString() + "\r\n"
                                     + clsClient.clsHH.City.ToString() + ", "
                                     + clsClient.clsHH.State.ToString() + " "
                                     + clsClient.clsHH.Zipcode.ToString();
            }
            catch { }
            showBasicInfo();
            showCSFP();
            showOptional();
            showWorksInArea();
            showMilitaryService();
            showEmployment();

            loadHHMems(true, hhMemID);
            setViewMode();
            fillForm();
        }

        private void addHHMember()
        {
            clsClient.clsHHmem.update();
            AddNewClientOrHHMem frmAddHHMem = new AddNewClientOrHHMem(clsClient, true);
            frmAddHHMem.ShowDialog();
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
            loadHHMems(false,clsClient.clsHHmem.ID);
            fillForm();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            addHHMember();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lvHHMembers.SelectedItems[0] != null)
            {
                if (clsClient.clsHHmem.HeadHH == false
                    && MessageBox.Show("Are You Sure You Want To Delete "
                    + lvHHMembers.SelectedItems[0].Text.ToUpper()
                    + ", " + lvHHMembers.SelectedItems[0].SubItems[1].Text + "?",
                    "Delete Household Member", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes
                    && lvHHMembers.Items.Count > 1)
                {
                    clsClient.clsHHmem.delete(Convert.ToInt32(lvHHMembers.SelectedItems[0].Tag.ToString()));
                    clsClient.clsHHmem.openHHID(clsClient.clsHH.ID);
                    loadHHMems(true,clsClient.clsHHmem.ID);
                    fillForm();
                    updateHouseholdData();
                }
                else
                {
                    if (clsClient.clsHHmem.HeadHH == true)
                    {
                        MessageBox.Show("You Are Trying To Delete The Head Of "
                        + "Household OR The Last Household Memeber. If you would "
                        + "like to delete this Household Member Please either "
                        + "change the Head of Household OR Delete the Household "
                        + "under the Client Menue", "Cannot Perform Function",
                        MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            clsClient.clsHHmem.rejectChanges();
            //clearChanges(clsClient.clsHHmem.ID);
            fillForm();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (tbBirthdate.Text == null && tbAge.Text == null && clsClient.clsHH.UseFamilyList == true)
            {
                MessageBox.Show("Cannot Save Household Member When Both Age And Birthdate Are Blank");
            }
            else if (clsClient.clsHHmem.update() == true)
            {
                updateHouseholdData();
                clsClient.clsHHmem.openHHID(clsClient.clsHH.ID);
                clsClient.clsHHmem.find(hhMemID);
            }
        }

        private void cboList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (loadingInfo == false)
            {
                ComboBox cb = (ComboBox)sender;
                clsClient.clsHHmem.SetDataValue(cb.Tag.ToString(),
                            ((parmType)cb.SelectedItem).ID.ToString());
            }
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
                    }
                    clsClient.clsHHmem.SetDataValue(chkBox.Tag.ToString(), chkBox.Checked.ToString());
                }
            }
            else
            {
                chkBox.Checked = (bool)clsClient.clsHHmem.GetDataValue(chkBox.Tag.ToString());
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
                foreach (TextBox tb in tbList)
                {
                    if (tb.Tag != null && tb.Tag.ToString().Trim() != "")
                        tb.Text = clsClient.clsHHmem.GetDataString(tb.Tag.ToString());
                }

                foreach (CheckBox chk in chkList)
                {
                    if (chk.Tag != null && chk.Tag.ToString().Trim() != "")
                        chk.Checked = CCFBGlobal.NullToFalse(clsClient.clsHHmem.GetDataValue(chk.Tag.ToString()));

                }

                foreach (ListViewItem lvi in lvEthnicity.Items)
                {
                    lvEthnicity.Items[lvi.Index].Checked =
                        (bool)CCFBGlobal.NullToFalse(clsClient.clsHHmem.GetDataValue(lvi.Tag.ToString()));
                }

                foreach (ComboBox cb in cboList)
                {
                    cb.SelectedValue = clsClient.clsHHmem.GetDataValue(cb.Tag.ToString()).ToString();
                }
                toggleEmployerFields(clsClient.clsHHmem.WorksInArea);
                setupBirthdateAndAge(clsClient.clsHHmem.UseAge);
                inEditMode = true;
            }
            else
            {
                foreach (TextBox tb in tbList)
                {
                    if (tb.Tag != null && tb.Tag.ToString().Trim() != "")
                        tb.Text = "";
                }

                foreach (CheckBox chk in chkList)
                {
                    if (chk.Tag != null && chk.Tag.ToString().Trim() != "")
                        chk.Checked = false;

                }

                foreach (ListViewItem lvi in lvEthnicity.Items)
                {
                    lvEthnicity.Items[lvi.Index].Checked = false;
                }

                foreach (ComboBox cb in cboList)
                {
                    cb.SelectedValue = -1;
                }

                toggleEmployerFields(false);
                setupBirthdateAndAge(false);
                inEditMode = false;
            }
            toggleCSFPFields(chkCSFP.Visible);

            loadingInfo = false;
        }

        private void loadHHMems(bool clearRows, int setSelectedID)
        {
            DataGridViewRow dvr;
            if (clearRows == true)
            {
                dgvHHM.Rows.Clear();
                lvHHMembers.Items.Clear();
            }
            int newIndex = 0;
            int rowCount = 0;
            string sText = "";
            ListViewItem lvi;

            for (int i = 0; i < clsClient.clsHHmem.RowCount; i++)
            {
                clsClient.clsHHmem.SetRecord(i);
                //Member View
                lvi = new ListViewItem(clsClient.clsHHmem.LastName);
                lvi.SubItems.Add(clsClient.clsHHmem.FirstName);
                lvi.SubItems.Add(clsClient.clsHHmem.Age.ToString());
                sText = "";
                if (clsClient.clsHHmem.Inactive == true)
                    sText = "Inactive";
                else if (clsClient.clsHHmem.HeadHH == true)
                    sText = "Head Hh";
                else if (clsClient.clsHHmem.NotIncludedInClientList == true)
                    sText = "Not Listed";
                lvi.SubItems.Add(sText);
                lvi.Tag = clsClient.clsHHmem.ID;
                lvHHMembers.Items.Add(lvi);
                if (setSelectedID == clsClient.clsHHmem.ID)
                    { newIndex = i; }
                //Grid View
                dgvHHM.Rows.Add();
                dvr = dgvHHM.Rows[rowCount];
                foreach (DataGridViewColumn  dgvCol in dgvHHM.Columns)
	            {
		            if (dgvCol.DataPropertyName != null && dgvCol.DataPropertyName !="")
                    {
                        switch (dgvCol.CellType.Name)
                        {
                            case "DataGridViewCheckBoxCell":
                                dvr.Cells[dgvCol.HeaderCell.ColumnIndex].Value = clsClient.clsHHmem.GetDataString(dgvCol.DataPropertyName);
                                break;
                            case "DataGridViewComboBoxCell":
                                string newvalue = clsClient.clsHHmem.GetDataString(dgvCol.DataPropertyName);
                                if (newvalue == "")
                                    newvalue = "0";
                                dvr.Cells[dgvCol.HeaderCell.ColumnIndex].Value = newvalue.ToString();
                                break;
                            case "DataGridViewTextBoxCell":
                                dvr.Cells[dgvCol.HeaderCell.ColumnIndex].Value = clsClient.clsHHmem.GetDataString(dgvCol.DataPropertyName);
                                break;
                            default:
                                break;
                        }
                    }
                }
                rowCount++;
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

        private void setupComboxBox(string colName, string tblName)
        {
            DataGridViewComboBoxColumn myColumn = (DataGridViewComboBoxColumn)dgvHHM.Columns[colName];
            myColumn.DataSource = CCFBGlobal.TypeCodesArray(tblName);
            myColumn.ValueMember = "UID";
            myColumn.DisplayMember = "LongName";
        }

        private void setCSFPInfoVisibility()
        {
            pnlCSFPInfo.Visible = (CCFBPrefs.EnableCSFP == true);
            if (CCFBPrefs.EnableCSFP == true)
                toggleCSFPFields(chkCSFP.Checked);
        }

        private void setupBirthdateAndAge(bool useAge)
        {
            if (useAge == false)
            {
                tbBirthdate.ReadOnly = false;
                tbBirthdate.ForeColor = Color.Black;
                tbAge.ReadOnly = true;
                tbAge.BackColor = Color.White;
                tbAge.ForeColor = CCFBGlobal.AgeBirthdateColor;
            }
            else
            {
                tbBirthdate.ReadOnly = true;
                tbBirthdate.BackColor = Color.White;
                tbBirthdate.ForeColor = CCFBGlobal.AgeBirthdateColor;
                tbAge.ReadOnly = false;
                tbAge.ForeColor = Color.Black;
            }
        }

        private void setViewMode()
        {
            lvHHMembers.Visible = (tabCtrl.SelectedIndex == 0);
        }

        private void showBasicInfo()
        {
            bool isVisible = tsbBasicInfo.Checked;
            for (int i = (int)dgvColNames.colBegInfo; i < (int)dgvColNames.colNotes; i++)
            {
                dgvHHM.Columns[i].Visible = isVisible && showColumn[i];
            }
        }

        private void showCSFP()
        {
            bool isVisible = tsbCSFP.Checked;
            for (int i = (int)dgvColNames.colBegCSFP; i < (int)dgvColNames.colBegWork; i++)
            {
                dgvHHM.Columns[i].Visible = isVisible && showColumn[i];
            }
        }

        private void showEmployment()
        {
            bool isVisible = tsbEmployment.Checked;
            for (int i = (int)dgvColNames.colBegEmployed; i < dgvHHM.Columns.Count; i++)
            {
                dgvHHM.Columns[i].Visible = isVisible && showColumn[i];
            }
        }

        private void showMilitaryService()
        {
            bool isVisible = tsbMilitarySvc.Checked;
            for (int i = (int)dgvColNames.colBegMilitary; i < (int)dgvColNames.colBegEmployed; i++)
            {
                dgvHHM.Columns[i].Visible = isVisible && showColumn[i];
            }
        }

        private void showNotes()
        {
            bool isVisible = tsbNotes.Checked;
            dgvHHM.Columns[(int)dgvColNames.colNotes].Visible = isVisible && showColumn[(int)dgvColNames.colNotes];
        }

        private void showOptional()
        {
            bool isVisible = tsbOptional.Checked;
            for (int i = (int)dgvColNames.colBegOption; i < (int)dgvColNames.colBegMilitary; i++)
            {
                dgvHHM.Columns[i].Visible = isVisible && showColumn[i];
            }
        }

        private void showWorksInArea()
        {
            bool isVisible = tsbWorksInArea.Checked;
            for (int i = (int)dgvColNames.colBegWork; i < (int)dgvColNames.colBegOption; i++)
            {
                dgvHHM.Columns[i].Visible = isVisible && showColumn[i];
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
                    }
            }
            else
            {
                tbHH.Text = clsClient.clsHHmem.GetDataValue(tbHH.Tag.ToString()).ToString();
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
            foreach (Control controls in controlList.OfType<Control>())
            {
                switch (controls.GetType().Name)
                {
                    case "TextBox":
                        {
                            if (controls.Tag != null && controls.Tag.ToString().Trim() != "TotalFamily" &&
                            controls.Tag.ToString() != "AutoAlert")
                            {
                                tbList.Add((TextBox)controls);
                                controls.Leave += new System.EventHandler(this.tbList_Leave);
                            }
                            break;
                        }
                    case "CheckBox":
                        {
                            controls.Enter += new System.EventHandler(this.chkList_Enter);
                            controls.Leave += new System.EventHandler(this.chkList_Leave);
                            controls.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkList_KeyDown);
                            chkList.Add((CheckBox)controls);
                            break;
                        }
                    case "ComboBox":
                        {
                            if (controls.Tag != null)
                            {
                                if (controls.Tag.ToString().Trim() != "")
                                {
                                    cboList.Add((ComboBox)controls);
                                }
                            }
                            break;
                        }
                }
                traverseAndAddControlsToCollections(controls.Controls);
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
                        ex.GetBaseException().ToString(), CCFBGlobal.serverName);
                }
            }
        }

        private void chkCSFP_CheckedChanged(object sender, EventArgs e)
        {
            toggleCSFPFields(chkCSFP.Checked);
        }
    }
}
