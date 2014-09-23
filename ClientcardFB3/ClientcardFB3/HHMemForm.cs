using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace ClientcardFB3
{
    public partial class HHMemForm : Form
    {
        List<ComboBox> cboList = new List<ComboBox>();
        List<CheckBox> chkList = new List<CheckBox>();
        List<TextBox> tbList = new List<TextBox>();
        Client clsClient;

        UserFields clsUserFields = new UserFields(CCFBGlobal.connectionString);

        SqlConnection conn = new SqlConnection(CCFBGlobal.connectionString);
        bool ethnicitySelectedFirstTime = false;
        MainForm frmMain;
        int hhMemID;
        string stbOriValue = "";
        bool bchkOriValue = false;
        bool inEditMode = true;
        bool loadingInfo = true;
        SqlCommand storedProcComm;


        /// <summary>
        /// Used in initialization of the HHMemForm(ie Constructor)
        /// </summary>
        /// <param name="clsIn">The Client Class</param>
        /// <param name="frmMainIn">The Main Form-Calling Form</param>
        /// <param name="HHMemID">The Household Member ID</param>
        public HHMemForm(Client clsIn, MainForm frmMainIn, int HHMemID)
        {
            InitializeComponent();

            clsClient = clsIn;
            frmMain = frmMainIn;

            hhMemID = HHMemID;

            clsUserFields.open("HouseholdMembers");

            string userField0 = clsUserFields.GetDataValue("EditLabel", 0).ToString();
            string userField1 = clsUserFields.GetDataValue("EditLabel", 1).ToString();

            if (userField0 == "")
                chkUserFlag0.Visible = false;
            else
                chkUserFlag0.Text = userField0;

            if (userField1 == "")
                chkUserFlag1.Visible = false;
            else
                chkUserFlag1.Text = userField0;

            CCFBGlobal.InitCombo(cboEdLvl, CCFBGlobal.parmTbl_EducationLevel); 
            CCFBGlobal.InitCombo(cboEmploymentStatus, CCFBGlobal.parmTbl_Employment);
            CCFBGlobal.InitCombo(cboMilitaryService, CCFBGlobal.parmTbl_MilitaryService);
            CCFBGlobal.InitCombo(cboDischargeStatus, CCFBGlobal.parmTbl_MilitaryDischarge);
            CCFBGlobal.InitCombo(cboCSFPRoute, CCFBGlobal.parmTbl_CSFPRoutes);
            storedProcComm = new SqlCommand("UpdateHHMembersAdditionalData", conn);
            traverseAndAddControlsToCollections(this.Controls);
            if (CCFBPrefs.EnableAdditionalHHMDataTab == false && CCFBPrefs.EnableEthnicityHHMTab == false)
                panel1.Visible = false;
            else
            {
                panel1.Visible = true;
                if (CCFBPrefs.EnableEthnicityHHMTab == false)
                    tabControl1.TabPages.RemoveByKey("tpEthnicity");  //Remove Ethnicity Tab
                if (CCFBPrefs.EnableAdditionalHHMDataTab == false)
                    tabControl1.TabPages.RemoveByKey("tpAdditional");  //Remove Additional Tab
            }
            setCSFPInfoVisibility();
            
            pnlEmployer.Visible = CCFBPrefs.EnableWorksInArea;
            if (CCFBPrefs.EnableWorksInArea == true)
                splitContainer2.SplitterDistance = 165;
            else
                splitContainer2.SplitterDistance = 235;

            loadList(HHMemID);
            fillForm();

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

            splitContainer2.Panel2.BackColor = CCFBGlobal.bkColorBaseEdit;
            tpAdditional.BackColor = CCFBGlobal.bkColorBaseEdit;
            tpEthnicity.BackColor = CCFBGlobal.bkColorBaseEdit;
            chkSurveyComplete.Visible = false;
            lblSurveyComplete.Visible = false;
            if (CCFBPrefs.EnableAdditionalHHMDataTab == true)
            {
                chkSurveyComplete.Visible = true;
                chkSurveyComplete.Checked = clsClient.clsHH.SurveyComplete;
                lblSurveyComplete.Visible = true;
            }
        }

        private void addHHMember()
        {
            AddNewClientOrHHMem frmAddHHMem = new AddNewClientOrHHMem(clsClient, true);
            frmAddHHMem.ShowDialog();
            clsClient.clsHHmem.openHHID(clsClient.clsHH.ID);
            updateHouseholdData();

            int hhMemID = 0;
            for (int i = 0; i < clsClient.clsHHmem.RowCount; i++)
            {
                if(hhMemID < Convert.ToInt32(clsClient.clsHHmem.DSet.Tables[0].Rows[i]["ID"]))
                {
                    clsClient.clsHHmem.SetRecord(i);
                    hhMemID = clsClient.clsHHmem.ID;
                }
            }

            loadList(clsClient.clsHHmem.ID);
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
                    loadList(clsClient.clsHHmem.ID);
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

        private void updateHouseholdData()
        {
            clsClient.UpdateDataBasedOn(DateTime.Parse(CCFBGlobal.DefaultServiceDate));
        }

        private void btnMarkSame_Click(object sender, EventArgs e)
        {
            if (lvHHMembers.SelectedItems[0] != null)
            {
                clsClient.clsHHmem.update();

                if (tabControl1.SelectedTab.Text == "Additional")
                    storedProcComm = new SqlCommand("UpdateHHMembersAdditionalData", conn);
                else
                    storedProcComm = new SqlCommand("UpdateHHMembersEthnicityData", conn);

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
                        ex.GetBaseException().ToString(), CCFBGlobal.serverName);
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

        private void btnReset_Click(object sender, EventArgs e)
        {
            clsClient.clsHHmem.rejectChanges();
            clearChanges(clsClient.clsHHmem.ID);
            fillForm();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (tbBirthdate.Text == null && tbAge.Text == null)
            {
                MessageBox.Show("Cannot Save Household Member When Both Age And Birthdate Are Blank");
            }
            else if (clsClient.clsHHmem.update() == true)
            {
                updateHouseholdData();
                clsClient.clsHHmem.openHHID(clsClient.clsHH.ID);
                clsClient.clsHHmem.find(hhMemID);
                //fillForm();
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

        private void chkEnterAge_CheckedChanged(object sender, EventArgs e)
        {
            clsClient.clsHHmem.UseAge = chkEnterAge.Checked;
            showBirthdateAndAge(clsClient.clsHHmem.UseAge);
        }

        private void chkHeadHH_CheckedChanged(object sender, EventArgs e)
        {
            if (loadingInfo == false)
            {
                if (chkHeadHH.Checked == true)
                {
                    for (int i = 0; i < clsClient.clsHHmem.RowCount; i++)
                    {
                        clsClient.clsHHmem.SetRecord(i);
                        if (clsClient.clsHHmem.HeadHH == true)
                        {
                            if (MessageBox.Show("You Are Trying To Set Head Of Household For A" +
                                " Household Memeber When Another Memeber" +
                                " Already Is Set To Head Of Household." +
                                " Would You Like to Change The Head Of Household" +
                                " To This Household Memeber?",
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
            SetEmployerVisibility(chkWorksInArea.Checked);
        }

        private void clearChanges(int id)
        {
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

                SetEmployerVisibility(chkWorksInArea.Checked);
                showBirthdateAndAge(clsClient.clsHHmem.UseAge);
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

                SetEmployerVisibility(false);
                showBirthdateAndAge(false);
                inEditMode = false;
            }
            if (chkCSFP.Visible == true)
            setCSFPInfoVisibility();
                
            loadingInfo = false;
        }

        private void setCSFPInfoVisibility()
        {
            if (CCFBPrefs.EnableCSFP == true)
            {
                chkCSFP.Visible = true;
                pnlCSFPInfo.Visible = chkCSFP.Checked;
            }
            else
            {
                pnlCSFPInfo.Visible = false;
                chkCSFP.Visible = false;
            }
        }

        private void HHMemForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            clsClient.clsHHmem.openHHID(clsClient.clsHH.ID);
            frmMain.fillForm();
        }

        private void loadList(int setSelectedID)
        {
            lvHHMembers.Items.Clear();
            int newIndex = 0;
            string sText = "";
            ListViewItem lvi;
            for (int i = 0; i < clsClient.clsHHmem.RowCount; i++)
            {
                clsClient.clsHHmem.SetRecord(i);
                lvi = new ListViewItem(clsClient.clsHHmem.LastName);
                lvi.SubItems.Add(clsClient.clsHHmem.FirstName);
                lvi.SubItems.Add(clsClient.clsHHmem.Age.ToString());
                sText = "";
                if (clsClient.clsHHmem.Inactive == true)
                    sText = "Inactive";
                else if (clsClient.clsHHmem.HeadHH == true)
                    sText = "Head Hh";
                else if (clsClient.clsHHmem.NotIncludedInClientList  == true)
                    sText = "Not Listed";
                lvi.SubItems.Add(sText);
                lvi.Tag = clsClient.clsHHmem.ID;
                lvHHMembers.Items.Add(lvi);
                
                if (setSelectedID == clsClient.clsHHmem.ID)
                {
                    newIndex = i;
                }
            }
            if (newIndex < lvHHMembers.Items.Count)
            {
                clsClient.clsHHmem.SetRecord(newIndex);
                lvHHMembers.Items[newIndex].Selected = true;
            }
        }

        private void lvEthnicity_Enter(object sender, EventArgs e)
        {
            if (ethnicitySelectedFirstTime == false)
                ethnicitySelectedFirstTime = true;
        }

        private void lvEthnicity_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            //if (inEditMode == false && loadingInfo == false && ethnicitySelectedFirstTime == true)
            //    e.NewValue = e.CurrentValue;
        }

        private void lvEthnicity_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (loadingInfo == false)
                clsClient.clsHHmem.SetDataValue(e.Item.Tag.ToString(), e.Item.Checked);
        }

        private void lvHHMembers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (loadingInfo == false && lvHHMembers.FocusedItem != null)
            {
                hhMemID = Convert.ToInt32(
                    lvHHMembers.FocusedItem.Tag.ToString());
                clsClient.clsHHmem.find(hhMemID);
                fillForm();
            }
        }

        private void SetEmployerVisibility(bool isVisible)
        {
            lblEmployer.Visible = isVisible;
            lblAddress.Visible = isVisible;
            lblPhone.Visible = isVisible;
            lblEmpZip.Visible = isVisible;
            tbEmployer.Visible = isVisible;
            tbEmpAddress.Visible = isVisible;
            tbEmployer.Visible = isVisible;
            tbEmplPhone.Visible = isVisible;
            tbEmpZip.Visible = isVisible;
        }

        private void showBirthdateAndAge(bool useAge)
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

        private void tbAge_Leave(object sender, EventArgs e)
        {
            if (tbAge.Text != "" && tbAge.Enabled == true)
            {
                if (clsClient.clsHHmem.UseAge == true)
                {
                    clsClient.clsHHmem.Birthdate = new DateTime(DateTime.Today.Year - Convert.ToInt32(tbAge.Text),
                        07, 01);
                    tbBirthdate.ForeColor = Color.Aqua;
                }
            }
            else if(tbAge.Enabled == true)
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

        private void chkCSFP_CheckedChanged(object sender, EventArgs e)
        {
            setCSFPInfoVisibility();
        }

        private void chkSurveyComplete_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSurveyComplete.Focused == true)
            {
                clsClient.clsHH.SurveyComplete = chkSurveyComplete.Checked;
                clsClient.clsHH.update();
            }   
        }

        private void tbList_Enter(object sender, EventArgs e)
        {
            stbOriValue = ((TextBox)sender).Text;
        }
    }
}
