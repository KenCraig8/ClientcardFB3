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
    public partial class AddNewClientOrHHMem : Form
    {
        Client clsClient;
        DataRow newRow;
        int newHHID = 0;
        int newHHMID = 0;
        bool addMember; //Tells if we are inserting a Household or a Household Member
        bool canceled = false;

        /// <summary>
        /// Constructor for the form that either adds a Household or a HosueholdMember
        /// </summary>
        /// <param name="clsIn">The client class</param>
        /// <param name="AddMember">True is just adding a Household Memeber, False
        /// adds Household</param>
        public AddNewClientOrHHMem(Client clsIn, bool AddMember)
        {
            InitializeComponent();
            this.TopMost = true;
            clsClient = clsIn;
            addMember = AddMember;
            panel1.Visible = false;
            panel1.BackColor = CCFBGlobal.bkColorFormAlt;
            BackColor = CCFBGlobal.bkColorBaseEdit;

            if (addMember)
            {
                this.Text = "Add Household Member";
                btnAddNewMem.Text = "Add New Hh Member";
            }
            else
            {
                this.Text = "Add New Household";
                btnAddNewMem.Text = "Add New Household";
            }
            btnAddNewMem.Enabled = false;
        }

        private void addHHMember(int hhid)
        {
            newRow = clsClient.clsHHmem.DSet.Tables[0].NewRow();

            foreach (TextBox tb in Controls.OfType<TextBox>())
            {
                if (tb.Enabled == true)
                    newRow[tb.Tag.ToString()] = tb.Text;
            }

            if (chkEnterAge.Checked == true)
            {
                newRow["BirthDate"] = calcBirthdateFromAge();
                newRow["UseAge"] = true;
            }
            else
            {
                newRow["Age"] = CCFBGlobal.calcAge(DateTime.Parse(tbBirthDate.Text), DateTime.Today);
                newRow["UseAge"] = false;
            }
            newRow["AgeGroup"] = clsClient.clsHHmem.GetEFAPAgeGroup(Convert.ToInt32(newRow["Age"]));
            newRow["HouseholdID"] = clsClient.clsHH.ID;
            newRow["HeadHH"] = false;
            newRow["NotIncludedInClientList"] = false;
            newRow["Inactive"] = false;
            newRow["SpecialDiet"] = 0;
            newRow["WorksInArea"] = 0;
            newRow["VolunteersAtFoodBank"] = false;
            newRow["SpecialDiet"] = false;
            newRow["UserFlag0"] = false;
            newRow["UserFlag1"] = false;
            newRow["CSFP"] = false;
            newRow["CSFPRoute"] = 0;
            newRow["IsDisabled"] = false;
            newRow["Inactive"] = false;
            newRow["Language"] = 0;
            newRow["UserFlag0"] = 0;
            newRow["UserFlag1"] = 0;
            newRow["CreatedBy"] = CCFBGlobal.currentUser_Name;
            newRow["Created"] = DateTime.Now;

            clsClient.clsHHmem.DSet.Tables[0].Rows.Add(newRow);

            if (clsClient.clsHHmem.insertMember() == true)
            {
                createNewHHMDemographics();
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
                MessageBox.Show("Cannot Save Household Or Household Member When Both Age And Birthdate Are Blank");
            }
            else
            {
                if (addMember == true)
                {
                    addHHMember(clsClient.clsHH.ID);
                    this.Visible = false;
                }
                else
                {
                    if (TestForHHMember() == false)
                    {
                        string hhName = tbLastName.Text + ", " + tbFirstName.Text;
                        newHHID = 0;

                        clsClient.clsHH.openWhere("Name='" + hhName + "'");

                        if (clsClient.clsHH.ISValid == true)
                        {
                            MessageBox.Show("Name Already Exists In Household Table");
                        }
                        else
                        {

                            newRow = clsClient.clsHH.DSet.Tables[0].NewRow();

                            newRow["Name"] = hhName;
                            newRow["Inactive"] = 0;
                            newRow["IdType"] = 4;
                            newRow["ClientType"] = 0;
                            newRow["PhoneType"] = 9;
                            newRow["EthnicSpeaking"] = 0;
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
                            newRow["TotalFamily"] = 1;
                            newRow["NeedToVerifyId"] = true;
                            newRow["SupplOnly"] = false;
                            newRow["IncludeOnLog"] = true;
                            newRow["Disabled"] = 0;
                            newRow["SpecialDiet"] = false;
                            newRow["NoCommodities"] = false;
                            newRow["NeedCommoditySignature"] = false;
                            newRow["IncludeOnLog"] = false;
                            newRow["AutoAlert"] = false;
                            newRow["SecondServiceThisMonth"] = false;
                            newRow["InCityLimits"] = false;
                            newRow["Homeless"] = false;
                            newRow["LatestService"] = "01/01/1900";
                            newRow["AnnualIncome"] = 0;
                            newRow["BabyServices"] = 0;
                            newRow["SurveyComplete"] = 0;
                            newRow["BabySvcDescr"] = "";
                            newRow["NbrCSFP"] = 0;
                            newRow["City"] = CCFBPrefs.DefaultCity;
                            newRow["State"] = CCFBPrefs.DefaultState;
                            newRow["Zipcode"] = CCFBPrefs.DefaultZipcode;
                            newRow["CreatedBy"] = CCFBGlobal.currentUser_Name;
                            newRow["Created"] = DateTime.Now;
                            newRow["BarCode"] = 0;

                            clsClient.clsHH.DSet.Tables[0].Rows.Add(newRow);
                            clsClient.clsHH.insert();
                            clsClient.clsHH.openWhere("Name='" + tbLastName.Text + ", " + tbFirstName.Text + "'");
                            newHHID = clsClient.clsHH.ID;
                            clsClient.clsHHmem.openHHID(newHHID);
                            addHHMember(newHHID);
                            this.Visible = false; 
                        }
                    }
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
            newRow["HispanicLatino"] = false;
            newRow["RefugeeImmigrant"] = false;
            newRow["LimitedEnglish"] = false;
            newRow["PartneredMarried"] = false;
            newRow["LongTermHomeless"] = false;
            newRow["ChronicallyHomeless"] = false;
            newRow["Employed"] = false;
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
        }

        private void tbLastName_Leave(object sender, EventArgs e)
        {
            if (tbLastName.Text.Length > 1)
            {
                if (tbLastName.Text.Substring(0, 1) != tbLastName.Text.Substring(0, 1).ToUpper())
                    tbLastName.Text = tbLastName.Text.Substring(0, 1).ToUpper() + tbLastName.Text.Substring(1);
            }
        }

        private void tbFirstName_Leave(object sender, EventArgs e)
        {
            if (tbLastName.Text.Length > 1 && tbFirstName.Text.Length > 1)
            {
                btnAddNewMem.Enabled = true;
                if (tbFirstName.Text.Substring(0, 1) != tbFirstName.Text.Substring(0, 1).ToUpper())
                    tbFirstName.Text = tbFirstName.Text.Substring(0, 1).ToUpper() + tbFirstName.Text.Substring(1);
                if (TestForHHMember())
                {
                    if (addMember == true)
                        btnAddNewMem.Enabled = CCFBPrefs.AllowDuplicateMemberNames;
                    else
                        btnAddNewMem.Enabled = CCFBPrefs.AllowDuplicateHHNames;
                }
                else
                {
                    btnAddNewMem.Enabled = true;
                }
            }

        }

        private void tbBirthDate_Leave(object sender, EventArgs e)
        {
            try
            {
                DateTime tmp = Convert.ToDateTime(tbBirthDate.Text);
                tbBirthDate.Text = tmp.ToShortDateString();
            }
            catch
            {
                if (MessageBox.Show("Invalid date entered", "Date Vailidation",
                    MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning) == DialogResult.Retry)
                {
                    tbBirthDate.Focus();
                }
            }
        }

        private bool TestForHHMember()
        {
            panel1.Visible = false;
            HHMembers clsTmpHHM = new HHMembers(CCFBGlobal.connectionString);
            lvwPeople.Items.Clear();
            lvwHouseholds.Items.Clear();
            if (clsTmpHHM.openWhere(" Where LastName = '" + tbLastName.Text + "' AND FirstName = '" + tbFirstName.Text + "'", true) == true)
            {
                for (int i = 0; i < clsTmpHHM.RowCount; i++)
                {
                    clsTmpHHM.SetRecord(i);
                    ListViewItem lvItm = new ListViewItem(clsTmpHHM.LastName);
                    lvItm.SubItems.Add(clsTmpHHM.FirstName);
                    lvItm.SubItems.Add(clsTmpHHM.Birthdate.ToShortDateString());
                    lvItm.SubItems.Add(clsTmpHHM.Age.ToString());
                    lvItm.SubItems.Add(clsTmpHHM.HouseholdID.ToString());
                    if (i % 2 == 0)
                        lvItm.BackColor = Color.White;
                    else
                        lvItm.BackColor = Color.LightYellow;

                    lvwPeople.Items.Add(lvItm);
                    TestForHousehold(clsTmpHHM.HouseholdID);
                }
                panel1.Visible = true;
                return true;
            }
            if (addMember == false)
                return TestForHousehold(0);
            else
                return false;
        }

        private bool TestForHousehold(int TestHHId)
        {
            Household clsTmpHH = new Household(CCFBGlobal.connectionString);
            string sqlWhere = "Name = '" + tbLastName.Text + ", " + tbFirstName.Text + "'";
            if (TestHHId > 0)
                sqlWhere += " OR ID = " + TestHHId.ToString();
            clsTmpHH.openWhere(sqlWhere);
            if (clsTmpHH.RowCount > 0)
            {
                lvwHouseholds.Items.Clear();
                for (int i = 0; i < clsTmpHH.RowCount; i++)
                {
                    clsTmpHH.SetRecord(i);
                    ListViewItem lvItm = new ListViewItem(clsTmpHH.Name);
                    lvItm.SubItems.Add(clsTmpHH.Inactive.ToString());
                    lvItm.SubItems.Add(clsTmpHH.Address.ToString());
                    lvItm.SubItems.Add(clsTmpHH.City.ToString() + ", " + clsTmpHH.State.ToString() + " " + clsTmpHH.Zipcode.ToString());
                    lvItm.SubItems.Add(clsTmpHH.HHMembersCount(clsTmpHH.ID).ToString());
                    lvItm.SubItems.Add(clsTmpHH.ID.ToString());
                    if (i % 2 == 0)
                        lvItm.BackColor = Color.White;
                    else
                        lvItm.BackColor = Color.LightYellow;


                    lvwHouseholds.Items.Add(lvItm);
                }
                panel1.Visible = true;
                return true;
            }
            return false;
        }

        private DateTime calcBirthdateFromAge()
        {
            return new DateTime(DateTime.Today.Year - Convert.ToInt32(tbAge.Text),
                07, 01);
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

        private void tbSex_Leave(object sender, EventArgs e)
        {
            tbSex.Text = tbSex.Text.ToUpper();
        }
    }
}
