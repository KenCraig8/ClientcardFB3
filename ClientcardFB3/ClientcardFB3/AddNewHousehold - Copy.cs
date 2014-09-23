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
    public partial class AddNewHousehold : Form
    {
        Client clsClient;
        Zipcodes clsZipcodes;
        DataRow newRow;
        string modedescription = "Add Client Household";
        int newHHID = 0;
        int newHHMID = 0;
        bool addMember; //Tells if we are inserting a Household or a Household Member
        bool canceled = false;
        CSDGSqlDataAccess csdgdataaccess;
        string dateIdVerified = CCFBGlobal.OURNULLDATE;
        bool needToSetTab = false;
        string stbOriValue = "";

        /// <summary>
        /// Constructor for the form that either adds a Household or a HosueholdMember
        /// </summary>
        /// <param name="clsIn">The client class</param>
        /// adds Household</param>
        public AddNewHousehold(Client clsIn)
        {
            InitializeComponent();
            clsClient = clsIn;
            BackColor = CCFBGlobal.bkColorBaseEdit;
            csdgdataaccess = new CSDGSqlDataAccess(CCFBGlobal.connectionString);
            lblPhoneNum.Visible = lblPhoneType.Visible = cboPhoneType.Visible = tbPhone.Visible = CCFBPrefs.EnableClientPhone;
            lblVerifyMethod.Visible = cboIDType.Visible = CCFBPrefs.EnableVerifyId;
            tbBabySvcDescr.Visible = chkBabyServices.Visible = CCFBPrefs.EnableBabyServices;
            chkNoCommodities.Visible = CCFBPrefs.EnableTEFAP;
            chkSupplOnly.Visible = CCFBPrefs.EnableSupplemental;

            this.Text = modedescription;
            lvwSameHouseNbr.Visible = true;
            pnlHH.Visible = true;
            tbLastName.Text = "";
            tbeAddress.Text = "";
            chkInCityLimits.Checked = false;
            chkHomeless.Checked = false;
            tpgSameHouseNbr.Text = "";
            //Fill Combos
            CCFBGlobal.InitCombo(cboSpecialLang, CCFBGlobal.parmTbl_Language);
            CCFBGlobal.InitCombo(cboClientType, CCFBGlobal.parmTbl_Client);
            CCFBGlobal.InitCombo(cboPhoneType, CCFBGlobal.parmTbl_Phone);
            CCFBGlobal.InitCombo(cboIDType, CCFBGlobal.parmTbl_IdVerify);
            CCFBGlobal.InitCombo(cboMemIDType, CCFBGlobal.parmTbl_IdVerify);

            //Set selected value for combos to ID = 0
            cboIDType.SelectedValue = "0";
            cboPhoneType.SelectedValue = "0";
            cboClientType.SelectedValue = "0";
            cboSpecialLang.SelectedValue = "0";

            clsZipcodes = new Zipcodes(CCFBGlobal.connectionString);

            initTextboxes();
            tbeCity.Text = CCFBPrefs.DefaultCity;
            tbeState.Text = CCFBPrefs.DefaultState;
            tbeZipCode.Text = CCFBPrefs.DefaultZipcode;
            btnSaveClient.Enabled = false;
            //spltcontMain.Panel2Collapsed = true;
        }

        private void addHHMember(int hhid)
        {
            HHMemberItem itemHHM = new HHMemberItem(clsClient.clsHHmem.addHHMember(hhid, "")
                                                  , clsClient.clsHHmem.DSet.Tables[0].Columns
                                                  , clsClient.clsHHmem.addHHMDemographics(0)
                                                  , clsClient.clsHHmem.DSet.Tables[1].Columns);
            itemHHM.LastName = tbLastName.Text;
            itemHHM.FirstName = tbFirstName.Text;
            itemHHM.HeadHH = true;
            itemHHM.BirthDate = Convert.ToDateTime(tbBirthDate.Text);
            itemHHM.UseAge = chkEnterAge.Checked;
            itemHHM.Age = Convert.ToInt32(tbAge.Text);
            itemHHM.AgeGroup = clsClient.clsHHmem.GetEFAPAgeGroup(itemHHM.Age);
            itemHHM.Language = Convert.ToInt32(cboSpecialLang.SelectedValue);
            itemHHM.MemIDNbr = tbMemIdNbr.Text;
            itemHHM.MemIDType = Convert.ToInt32(cboMemIDType.SelectedValue);
            itemHHM.Sex = tbSex.Text;
            clsClient.clsHHmem.newHHMemberSave(itemHHM);
            //newRow = clsClient.clsHHmem.DSet.Tables[0].NewRow();
            //newRow["NotIncludedInClientList"] = false;
            //foreach (TextBox tb in spltcontMain.Panel1.Controls.OfType<TextBox>())
            //{
            //    if (tb.Enabled == true)
            //        newRow[tb.Tag.ToString()] = tb.Text;
            //}

            //if (chkEnterAge.Checked == true)
            //{
            //    newRow["BirthDate"] = CCFBGlobal.calcBirthdateFromAge(Convert.ToInt32(tbAge.Text));
            //    newRow["UseAge"] = true;
            //    if (Convert.ToInt32(tbAge.Text) < 18)
            //        newRow["NotIncludedInClientList"] = true;
            //}
            //else
            //{
            //    newRow["BirthDate"] = DateTime.Parse(tbBirthDate.Text);
            //    newRow["Age"] = CCFBGlobal.calcAge(DateTime.Parse(tbBirthDate.Text), DateTime.Today);
            //    newRow["UseAge"] = false;
            //    if (Convert.ToInt32(newRow["Age"])<18)
            //        newRow["NotIncludedInClientList"] = true;
            //}
            //newRow["AgeGroup"] = clsClient.clsHHmem.GetEFAPAgeGroup(Convert.ToInt32(newRow["Age"]));
            //newRow["HouseholdID"] = clsClient.clsHH.ID;
            //newRow["HeadHH"] = true;
            //newRow["Inactive"] = false;
            //newRow["SpecialDiet"] = 0;
            //newRow["WorksInArea"] = 0;
            //newRow["VolunteersAtFoodBank"] = false;
            //newRow["SpecialDiet"] = false;
            //newRow["UserFlag0"] = false;
            //newRow["UserFlag1"] = false;
            //newRow["CSFP"] = false;
            //newRow["IsDisabled"] = false;
            //newRow["Inactive"] = false;
            //newRow["Language"] = cboSpecialLang.SelectedValue;
            //newRow["UserFlag0"] = 0;
            //newRow["UserFlag1"] = 0;
            //newRow["CreatedBy"] = CCFBGlobal.dbUserName;
            //newRow["Created"] = DateTime.Now;
            //newRow["CSFPExpiration"] = CCFBGlobal.OURNULLDATE; 
            //newRow["CSFPComments"] = "";
            //newRow["CSFPRoute"] = 0;
            //newRow["MemIDNbr"] = tbMemIdNbr.Text;
            //if (cboMemIDType.SelectedValue != null)
            //{
            //    newRow["MemIDType"] = cboMemIDType.SelectedValue;
            //}
            //else
            //{
            //    newRow["MemIDType"] = 0;
            //}
            //clsClient.clsHHmem.DSet.Tables[0].Rows.Add(newRow);

            //if (clsClient.clsHHmem.insertMember() == true)
            //{
            //    createNewHHMDemographics();
            //    clsClient.calcAllHHMemAges(Convert.ToDateTime( CCFBGlobal.DefaultServiceDate));
            //}
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

        private void btnSaveClient_Click(object sender, EventArgs e)
        {
            if (tbAge.Text.Trim() == "" && tbBirthDate.Text.Trim() == "")
            {
                MessageBox.Show("Cannot Save When Both Age And Birthdate Are Blank", modedescription);
            }
            else if (lblDateError.Visible == true && chkEnterAge.Checked == false)
            {
                MessageBox.Show("Cannot Save With Invalid Birthdate", modedescription);
                tbBirthDate.Focus();
            }
            else
            {
                string hhName = tbLastName.Text + ", " + tbFirstName.Text;
                newHHID = 0;

                clsClient.clsHH.openWhere("Name='" + CCFBGlobal.SQLApostrophe(tbLastName.Text) + ", " + CCFBGlobal.SQLApostrophe(tbFirstName.Text) + "'");
                if (clsClient.clsHH.ISValid == true && CCFBPrefs.AllowDuplicateHHNames == false)
                {
                    MessageBox.Show("Name " + hhName + "\r\nAlready Exists In Household Table",
                        modedescription, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    if (clsClient.householdMemberExists(tbFirstName.Text, tbLastName.Text) == false || CCFBPrefs.AllowDuplicateMemberNames == true)
                    {

                        newRow = clsClient.clsHH.DSet.Tables[0].NewRow();

                        newRow["Name"] = hhName;
                        newRow["Inactive"] = 0;
                        if (cboIDType.SelectedValue != null)
                            newRow["IdType"] = cboIDType.SelectedValue;
                        else
                            newRow["IdType"] = 0;
                        newRow["ClientType"] = cboClientType.SelectedValue;
                        newRow["PhoneType"] = cboPhoneType.SelectedValue;
                        newRow["Phone"] = tbPhone.Text;
                        newRow["EthnicSpeaking"] = cboSpecialLang.SelectedValue;
                        switch (CCFBPrefs.UseFamilyList)
                        {
                            case CCFBPrefs.UseFamilyListCode.Normally:
                                newRow["UseFamilyList"] = true; break;
                            case CCFBPrefs.UseFamilyListCode.Sometimes:
                                newRow["UseFamilyList"] = false; break;
                            case CCFBPrefs.UseFamilyListCode.Always:
                                newRow["UseFamilyList"] = true; break;
                            case CCFBPrefs.UseFamilyListCode.Never:
                                newRow["UseFamilyList"] = false; break;
                            default:
                                break;
                        }
                        newRow["Inactive"] = false;
                        newRow["UserFlag0"] = false;
                        newRow["UserFlag1"] = false;
                        newRow["UserFlag2"] = false;
                        newRow["UserFlag3"] = false;
                        newRow["UserFlag4"] = false;
                        newRow["UserFlag5"] = false;
                        newRow["UserFlag6"] = false;
                        newRow["UserFlag7"] = false;
                        newRow["UserFlag8"] = false;
                        newRow["UserFlag9"] = false;
                        newRow["UserNum0"] = 0;
                        newRow["UserNum1"] = 0;
                        newRow["Infants"] = 0;
                        newRow["Youth"] = 0;
                        newRow["Teens"] = 0;
                        newRow["Eighteen"] = 0;
                        newRow["Adults"] = 0;
                        newRow["Seniors"] = 0;

                        switch (clsClient.clsHHmem.GetEFAPAgeGroup(Convert.ToInt32(tbAge.Text)))
                        {
                            case 0:
                                { newRow["Infants"] = 1; break; }
                            case 1:
                                { newRow["Youth"] = 1; break; }
                            case 2:
                                { newRow["Teens"] = 1; break; }
                            case 3:
                                { newRow["Eighteen"] = 1; break; }
                            case 4:
                                { newRow["Adults"] = 1; break; }
                            case 5:
                                { newRow["Seniors"] = 1; break; }
                        }

                        newRow["TotalFamily"] = 1;

                        if(cboIDType.SelectedValue.ToString() == "0")
                            newRow["NeedToVerifyId"] = true;
                        else
                            newRow["NeedToVerifyId"] = false;

                        newRow["DateIDVerified"] = dateIdVerified;

                        newRow["SupplOnly"] = false;
                        newRow["IncludeOnLog"] = true;
                        newRow["Disabled"] = 0;
                        newRow["SpecialDiet"] = false;
                        newRow["NoCommodities"] = false;
                        newRow["NeedCommoditySignature"] = false;
                        newRow["IncludeOnLog"] = false;
                        newRow["AutoAlert"] = false;
                        newRow["SecondServiceThisMonth"] = false;
                        newRow["InCityLimits"] = chkInCityLimits.Checked;
                        newRow["Homeless"] = chkHomeless.Checked;
                        newRow["LatestService"] = "01/01/1900";
                        newRow["AnnualIncome"] = 0;
                        newRow["BabyServices"] = chkBabyServices.Checked;
                        newRow["BabySvcDescr"] = tbBabySvcDescr.Text;
                        newRow["SurveyComplete"] = 0;
                        newRow["NbrCSFP"] = 0;
                        newRow["Address"] = tbeAddress.Text;
                        newRow["AptNbr"] = tbeApt.Text;
                        newRow["City"] = tbeCity.Text;
                        newRow["State"] = tbeState.Text;
                        newRow["Zipcode"] = tbeZipCode.Text;
                        newRow["CreatedBy"] = CCFBGlobal.dbUserName;
                        newRow["Created"] = DateTime.Now;
                        newRow["BarCode"] = 0;
                        newRow["TEFAPSignDate"] = CCFBGlobal.OURNULLDATE;
                        newRow["SingleHeadHH"] = chkSingleHeadHH.Checked;
                        newRow["NeedIncomeVerification"] = true;
                        newRow["IncomeVerifiedDate"] = CCFBGlobal.OURNULLDATE;
                        newRow["ServiceMethod"] = 0;
                        newRow["HDRoute"] = 0;
                        newRow["HDBuilding"] = 0;
                        newRow["HDProgram"] = 0;
                        newRow["HUDCategory"] = 0;

                        clsClient.clsHH.DSet.Tables[0].Rows.Add(newRow);
                        clsClient.clsHH.insert();
                        clsClient.clsHH.openWhere("Name='" + CCFBGlobal.SQLApostrophe(hhName) + "' AND CREATED >='" + DateTime.Today.ToShortDateString() + "'");
                        newHHID = clsClient.clsHH.ID;
                        clsClient.clsHHmem.openHHID(newHHID);
                        addHHMember(newHHID);
                        this.Visible = false;
                    }
                    else
                    {
                        MessageBox.Show(tbFirstName.Text + " " + tbLastName.Text + "\r\nAlready Exists In Household Members Table",
                            modedescription, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        tbFirstName.Focus();
                    }
                }
            }
        }

        private void cboIDType_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cboIDType.SelectedValue != null && cboIDType.SelectedValue.ToString() != "0")
                dateIdVerified = DateTime.Today.ToShortDateString();
            else
                dateIdVerified = CCFBGlobal.OURNULLDATE;
        }

        //private void createNewHHMDemographics()
        //{
        //    clsClient.clsHHmem.DRowHhm = clsClient.clsHHmem.DSet.Tables[0].Rows[
        //        clsClient.clsHHmem.DSet.Tables[0].Rows.Count - 1];

        //    newHHMID = clsClient.clsHHmem.ID;
        //    newRow = clsClient.clsHHmem.DSet.Tables[1].NewRow();
        //    newRow["MilitaryService"] = 0;
        //    newRow["DischargeStatus"] = 0;
        //    newRow["HispanicLatino"] = 2;
        //    newRow["RefugeeImmigrant"] = 2;
        //    newRow["LimitedEnglish"] = 2;
        //    newRow["PartneredMarried"] = 2;
        //    newRow["LongTermHomeless"] = 2;
        //    newRow["ChronicallyHomeless"] = 2;
        //    newRow["Employed"] = 2;
        //    newRow["EmplolymentStatus"] = 0;
        //    newRow["AmericanIndian"] = false;
        //    newRow["AlaskaNative"] = false;
        //    newRow["IndigenousToAmericas"] = false;
        //    newRow["AsianIndian"] = false;
        //    newRow["Cambodian"] = false;
        //    newRow["Chinese"] = false;
        //    newRow["Filipino"] = false;
        //    newRow["Japanese"] = false;
        //    newRow["Korean"] = false;
        //    newRow["Vietnamese"] = false;
        //    newRow["OtherAsian"] = false;
        //    newRow["IndigenousAfricanBlack"] = false;
        //    newRow["AfricanAmericanBlack"] = false;
        //    newRow["OtherBlack"] = false;
        //    newRow["HawaiianNative"] = false;
        //    newRow["Polynesian"] = false;
        //    newRow["Micronesian"] = false;
        //    newRow["OtherPacificIslander"] = false;
        //    newRow["ArabIranianMiddleEastern"] = false;
        //    newRow["OtherWhiteCaucasian"] = false;
        //    newRow["EthnicOther"] = false;
        //    newRow["EthnicUnknown"] = false;
        //    newRow["EducationLevel"] = 0;
        //    newRow["ID"] = newHHMID;

        //    clsClient.clsHHmem.DSet.Tables[1].Rows.Add(newRow);
        //    clsClient.clsHHmem.insertDemographics();
        //}

        private void chkEnterAge_CheckedChanged(object sender, EventArgs e)
        {
            tbAge.Enabled = chkEnterAge.Checked;
            tbBirthDate.Enabled = (chkEnterAge.Checked == false);
            if (chkEnterAge.Checked == true)
                lblDateError.Visible = false;
        }

        private void FillBirthDateGrid(string birthdate)
        {
            lvwHHMByBirthdate.Items.Clear();
            tpgSameBirthDate.Text = "Loading ...";
            Application.DoEvents();
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
                    lvwHHMByBirthdate.Items.Add(lvItm);
                }
            }
            else
            {
                tpgSameBirthDate.Text = "";
                lvwHHMByBirthdate.Visible = false;
            }
        }

        private void initTextboxes()
        {
            foreach (TextBox tb in spltcontMain.Panel1.Controls.OfType<TextBox>())
            { tb.Text = ""; }

            foreach (TextBox tb in pnlHH.Controls.OfType<TextBox>())
            {   tb.Text = "";   }

            tbPhone.Text = "";
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
                    btnSaveClient.Enabled = true;
                    if (TestHHMName(1, lvwPeople, tpgLastFirst))
                    {
                        if (addMember == true)
                            btnSaveClient.Enabled = CCFBPrefs.AllowDuplicateMemberNames;
                        else
                            btnSaveClient.Enabled = CCFBPrefs.AllowDuplicateHHNames;

                        lblDupHHError.Visible = !btnSaveClient.Enabled;
                        //if (lblDupHHError.Visible == true)
                        //    tbFirstName.Focus();
                    }
                    else
                    {
                        btnSaveClient.Enabled = true;
                    }
                }
            }
        }

        private void tbBirthDate_Leave(object sender, EventArgs e)
        {
            try
            {
                DateTime tmp = Convert.ToDateTime(tbBirthDate.Text);
                if (tmp > CCFBGlobal.FBNullDateValue && tmp < DateTime.Today)
                {
                    tbBirthDate.Text = tmp.ToShortDateString();
                    tbBirthDate.ForeColor = Color.Black;
                    lblDateError.Visible = false;
                    tbAge.Text = CCFBGlobal.calcAge(tmp, DateTime.Today).ToString();
                    FillBirthDateGrid(tbBirthDate.Text);
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
                //if (MessageBox.Show("Invalid date entered", "Date Vailidation",
                //    MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning) == DialogResult.Retry)
                //{
                //    tbBirthDate.Focus();
                //}
            }
        }

        //private ListViewGroup MakeHouseholdGroup(int hhId, int hhmId)
        //{
        //    Household clsTmpHH = new Household(CCFBGlobal.connectionString);
        //    string hhInfo = "";
        //    ListViewGroup lvg;
        //    clsTmpHH.openWhere("ID = " + hhId);
        //    if (clsTmpHH.RowCount > 0)
        //    {
        //        hhInfo = clsTmpHH.Name + "  " + clsTmpHH.Address + "  " + clsTmpHH.City + ", " + clsTmpHH.State + " " + clsTmpHH.Zipcode
        //               + "  " + CCFBGlobal.IsInactiveString(clsTmpHH.Inactive) + "  " + clsTmpHH.ID.ToString();
        //        lvg = new ListViewGroup(hhInfo, HorizontalAlignment.Left);
        //        lvg.Name = hhId.ToString();
        //    }
        //    else
        //        lvg = new ListViewGroup("HHID = " + hhId.ToString(), HorizontalAlignment.Left);
        //    return lvg;
        //}

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

        private void tbSex_Leave(object sender, EventArgs e)
        {
            tbSex.Text = tbSex.Text.ToUpper();
        }

        private void tbeCity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                clsZipcodes.open(tbeCity.Text, tbeCity.Tag.ToString());

                if (clsZipcodes.RowCount > 1)
                {
                    ZipcodeSelectForm frmZipSelect = new ZipcodeSelectForm(clsZipcodes, "City: " + tbeCity.Text);
                    //frmZipSelect.TopMost = true;
                    frmZipSelect.ShowDialog();
                    if (frmZipSelect.Canceled == false)
                    {
                        tbeZipCode.Text = clsZipcodes.ZipCode;
                        tbeCity.Text = clsZipcodes.City;
                        tbeState.Text = clsZipcodes.State.ToUpper();
                    }
                }
            }
        }

        private void tbeZipCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                clsZipcodes.open(tbeZipCode.Text, tbeZipCode.Tag.ToString());
                if (clsZipcodes.IsValid == true)
                {
                    tbeCity.Text = clsZipcodes.City;
                    tbeState.Text = clsZipcodes.State.ToUpper();
                }
            }
        }

        private void tbeAddress_Leave(object sender, EventArgs e)
        {
            string tmp = CCFBGlobal.cleanAddress(tbeAddress.Text);
            if (tmp != tbeAddress.Text)
                tbeAddress.Text = tmp;
            if (tmp != "")
            {
                FillSameHouseNbrGrid();
                Application.DoEvents();
                tbeApt.Focus();
            }
        }

        private void FillSameHouseNbrGrid()
        {
            Household clsTmpHH = new Household(CCFBGlobal.connectionString);
            TrxLog clsTmpTL = new TrxLog(CCFBGlobal.connectionString,true,true,true,true);
            string[] tmp = tbeAddress.Text.Split(' ');
            tpgSameHouseNbr.Text = "Loading ...";
            lvwSameHouseNbr.Items.Clear();
            Application.DoEvents();
            if (tmp.Length > 0)
            {
                clsTmpHH.openWhere("Left(Address," + tmp[0].Length.ToString() + ") = '" + tmp[0] + "'", "Address, Name");
                for (int i = 0; i < clsTmpHH.RowCount; i++)
                {
                    clsTmpHH.SetRecord(i);
                    ListViewItem lvi = new ListViewItem(clsTmpHH.Name);
                    lvi.SubItems.Add(clsTmpHH.Inactive.ToString());
                    lvi.SubItems.Add(clsTmpHH.Address);
                    lvi.SubItems.Add(clsTmpHH.AptNbr);
                    lvi.SubItems.Add(clsTmpHH.City + ", " + clsTmpHH.State + " " + clsTmpHH.Zipcode);
                    lvi.SubItems.Add(clsTmpTL.GetNBrTrxByHH(clsTmpHH.ID).ToString());
                    lvi.SubItems.Add(clsTmpHH.ID.ToString());
                    lvwSameHouseNbr.Items.Add(lvi);
                }
                tpgSameHouseNbr.Text = "[" + lvwSameHouseNbr.Items.Count.ToString() + "] " + tpgSameHouseNbr.Tag.ToString();
                lvwSameHouseNbr.Visible = true;
                tabControl1.SelectedIndex = 1;
            }
            else
            {
                tpgSameHouseNbr.Text = "";
                lvwSameHouseNbr.Visible = false;
            }
        }

        //private bool FillHHMemberGrid()
        //{
        //    bool HeadHHMatch = false;
        //    int testHHId = 0;
        //    int insertIndex = 0;
        //    string tmp = "";
        //    spltcontMain.Panel2Collapsed = false;
        //    lvwPeople.Items.Clear();
        //    tpgLastFirst.Text = "Loading ..";
        //    Application.DoEvents();
        //    HHMembers clsTmpHHM = new HHMembers(CCFBGlobal.connectionString);
        //    List<int> hhidList = new List<int>();
        //    List<ListViewGroup> hhidGroup = new List<ListViewGroup>();

        //    if (clsTmpHHM.openWhere(" Where SoundEx(LastName) = SoundEx('" + tbLastName.Text + "') AND SoundEx(FirstName) = SoundEx('" + tbFirstName.Text + "')", "HouseholdId, Id", false) == true)
        //    {
        //        for (int i = 0; i < clsTmpHHM.RowCount; i++)
        //        {
        //            clsTmpHHM.SetRecord(i);
        //            testHHId = clsTmpHHM.HouseholdID;
        //            ListViewItem lvItm = new ListViewItem("");  //clsTmpHHM.LastName);
        //            lvItm.SubItems.Add(clsTmpHHM.FirstName);
        //            lvItm.SubItems.Add(CCFBGlobal.ValidDateString(clsTmpHHM.Birthdate));
        //            lvItm.SubItems.Add(clsTmpHHM.Age.ToString());
        //            lvItm.SubItems.Add(clsTmpHHM.HouseholdID.ToString());
        //            lvItm.SubItems.Add(clsTmpHHM.Inactive.ToString());
        //            if (clsTmpHHM.HeadHH == true)
        //            {
        //                tmp = "HeadHH";
        //                if (tbLastName.Text.ToUpper() == clsTmpHHM.LastName.ToUpper() && tbFirstName.Text.ToUpper() == clsTmpHHM.FirstName.ToUpper())
        //                    HeadHHMatch = true;
        //            }
        //            else
        //                tmp = "";
        //            lvItm.SubItems.Add(tmp);
        //            //if (i % 2 == 0)
        //            //    lvItm.BackColor = Color.White;
        //            //else
        //            //    lvItm.BackColor = Color.LightYellow;
        //            insertIndex = -1;
        //            for (int j = 0; j < hhidList.Count; j++)
        //            {
        //                if (testHHId == hhidList[j])
        //                {
        //                    lvItm.Group = hhidGroup[j];
        //                    insertIndex = j;
        //                    break;
        //                }
        //            }
        //            if (insertIndex == -1)
        //            {
        //                hhidList.Add(testHHId);
        //                hhidGroup.Add(MakeHouseholdGroup(testHHId, clsTmpHHM.ID));
        //                insertIndex = hhidGroup.Count - 1;
        //                lvwPeople.Groups.Add(hhidGroup[insertIndex]);
        //                lvItm.Group = hhidGroup[insertIndex];
        //            }
        //            lvwPeople.Items.Add(lvItm);
        //        }
        //        tpgLastFirst.Text = "[ " + clsClient.clsHHmem.RowCount.ToString() + " ] " + tpgLastFirst.Tag.ToString();
        //        //spltcontLists.Panel1Collapsed = false;
        //        if (addMember == true)
        //            return true;
        //        else
        //            return HeadHHMatch;
        //    }
        //    if (addMember == false)
        //        return TestForHousehold(0);
        //    else
        //        return false;
        //}

        private bool TestForHousehold(int TestHHId)
        {
            Household clsTmpHH = new Household(CCFBGlobal.connectionString);
            string sqlWhere = "Name = '" + CCFBGlobal.SQLApostrophe(tbLastName.Text) + ", " + CCFBGlobal.SQLApostrophe(tbFirstName.Text) + "'";
            if (TestHHId > 0)
                sqlWhere += " OR ID = " + TestHHId.ToString();
            clsTmpHH.openWhere(sqlWhere);
            return (clsTmpHH.RowCount > 0);
        }

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
            //tabControl1.SelectedTab = tpg; 
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

        private void tbMemIdNbr_Leave(object sender, EventArgs e)
        {
            if (tbMemIdNbr.Text != stbOriValue)
            {
                tbMemIdNbr.Text = tbMemIdNbr.Text.ToUpper();
                if (tbMemIdNbr.Text != "")
                {
                    int testHHMID = 0;
                    int testID = CCFBGlobal.getHHFromBarCode(tbMemIdNbr.Text,ref testHHMID);
                    if (testID == 0)
                    {
                        cboMemIDType.SelectedValue = 1;
                    }
                    else
                    {
                        MessageBox.Show("already on file");
                    }
                }
            }
        }

        private void tbMemIdNbr_Enter(object sender, EventArgs e)
        {
            stbOriValue = tbMemIdNbr.Text;
        }

        private void tbMemIdNbr_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                tbeAddress.Focus();
            }
        }
    }
}
