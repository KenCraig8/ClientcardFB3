using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ClientcardFB3
{
    public partial class FoodDonationsForm : Form
    {
        Donors clsDonors = new Donors(CCFBGlobal.connectionString);
        FoodDonations clsFoodDonations = new FoodDonations(CCFBGlobal.connectionString);
        //parm_DonationType clsParmDonationType = new parm_DonationType(CCFBGlobal.connectionString);
        //parm_FoodClass clsParmFoodClass = new parm_FoodClass(CCFBGlobal.connectionString);
        int dateIndex = 0;

      
        List<DateTime> donationDates = new List<DateTime>();
        int donorID = -1;
        int trxID = -1;
        EditDonorForm frmEditDonors;
        List<TextBox> tbList = new List<TextBox>();
        bool update = false;
        bool bNormalMode = false;
                
        /// <summary>
        /// Constructor for the FoodDonations Form
        /// </summary>
        public FoodDonationsForm()
        {
            InitializeComponent();
            gbDnrHist.SetBounds(0, 0, 1000, Height - 25);
            gbDailyLog.SetBounds(0, 0, 1000, Height - 25);
            gbLogEntry.SetBounds(5, 0, 744, 256);
            gbDnrHist.Visible = false;
            gbDailyLog.Visible = true;
            gbLogEntry.Visible = false;

            //clsParmDonationType.openAll();      //Open all parm data for Donations
            clsDonors.openWhere("");                //Opens all Donors
            //clsParmFoodClass.openAll();         //Opens all Food Classes for Donations

            frmEditDonors = new EditDonorForm(CCFBGlobal.connectionString, true);          

            //Gets all distinct Donation Dates in Database
            clsFoodDonations.openDistinctDonationDates();      

            //Fills the combo boxes for Donation Type and Class
            fillCombos();

            //Fills a collection with the ditinct Donation Dates
            fillDonationDates();

            //Loads the Donation Log For the current date in the ListView 
            loadDonationLogList();

            //Adds each text bos to the collection for easy use later
            foreach (TextBox tb in gbLogEntry.Controls.OfType<TextBox>())
            {
                tbList.Add(tb);
            }

            tbName.BackColor = Color.White;
            tbDonorID.BackColor = Color.White;
            cboDonorPeriod.SelectedIndex = 2; 
            bNormalMode = true;
        }

        /// <summary>
        /// Opens the Edit Donors Form so that a user can select which Donor
        /// made the donation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            frmEditDonors.ShowDialog();
            donorID = frmEditDonors.SelectedId;
            if (donorID == 0)
            {
                tbDonorID.Text = "";
                tbName.Text = "";
            }
            else
            {
                clsDonors.find(donorID);
                tbName.Text = clsDonors.Name;
                tbDonorID.Text = donorID.ToString();
                cboFoodCat.SelectedValue = "9";
                cboDonationType.SelectedValue = clsDonors.DefaultDonationType.ToString();
                clsFoodDonations.openWhere(" Where DonorID=" + donorID.ToString());
                loadDonorLogList();
                clsFoodDonations.openForDate(dtDonationDate.Value);
            }
        }

        /// <summary>
        /// Cnacles out of the Edit Donors Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// This event fires when the user hit the button to
        /// edit an existing donation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditDnrTrx_Click(object sender, EventArgs e)
        {
            //IF a donations is selected
            if (lvFoodDonations.FocusedItem != null)
            {
                //Set update bool to true
                update = true;
                //Set mode to Edit Mode
                changedMode();
                //Fill the textboxes with the selected donation data
                fillgbLogEntryControls(lvFoodDonations.FocusedItem.Name);

                //Fill data from the listview
                tbName.Text = lvFoodDonations.FocusedItem.SubItems[2].Text;
                tbTrxID.Text = clsFoodDonations.TrxID.ToString();


            }
            else
            {
                MessageBox.Show("There Is No Selected Donation. Please Select A Donation And Try Again");
            }
        }

        /// <summary>
        /// Event fires when the user clicks on the First Donation Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFirstDL_Click(object sender, EventArgs e)
        {
            //Set dateIndex to last date in collection
            dateIndex = donationDates.Count - 1;
            //Open all donations for the given date
            clsFoodDonations.openForDate(donationDates[dateIndex]);
            //Set DatePicker to proper date
            dtDateDL.Value = donationDates[dateIndex];
            //Load the Donations
            loadDonationLogList();

            //Enable and dissable proper buttons
            btnNextDL.Enabled = true;
            btnPrevDL.Enabled = false;
        }

        /// <summary>
        /// Event fires when the user clicks on the Last Donation Date Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLastDL_Click(object sender, EventArgs e)
        {
            //Set dateIndex to first date in colection
            dateIndex = 0;
            //Open all donations for the given date
            clsFoodDonations.openForDate(donationDates[dateIndex]);
            //Set DatePicker to proper date
            dtDateDL.Value = donationDates[dateIndex];
            //Load the Donations in the listview
            loadDonationLogList();

            //Enable and dissable proper buttons
            btnNextDL.Enabled = false;
            btnPrevDL.Enabled = true;
        }

        /// <summary>
        /// Fires when the user clicks on button to cancel out of
        /// Add/Edit Log Entry
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogEntryCancel_Click(object sender, EventArgs e)
        {
            //Sets the mode of the form
            changedMode();
        }

        /// <summary>
        /// Event fires when the user clicks on the Save buton
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogEntrySave_Click(object sender, EventArgs e)
        {
            //If we are in Insert Mode
            if (update == false)
            {
                //If a Donor has been selected
                if (tbDonorID.Text.Trim() != "" && tbName.Text.Trim() != "" && tbLbs.Text.Trim() != "")
                {
                    //Create a new row from the DataSet
                    DataRow drow = clsFoodDonations.DSet.Tables[0].NewRow();

                    //Go through the textboxes and insert data into the new row
                    foreach (TextBox tb in tbList)
                    {
                        if (tb.Tag != null && tb.Tag.ToString() != "")
                            drow[tb.Tag.ToString()] = tb.Text;
                    }

                    //Add the other Data to the row
                    drow["TrxDate"] = dtDonationDate.Value;
                    drow[cboFoodCat.Tag.ToString()] = cboFoodCat.SelectedValue;
                    drow[cboDonationType.Tag.ToString()] = cboDonationType.SelectedValue; 
                    drow["DollarValue"] = 0;
                    drow["CreatedBy"] = CCFBGlobal.currentUser_Name;
                    drow["Created"] = DateTime.Now;
                    clsFoodDonations.DSet.Tables[0].Rows.Add(drow);
                    //Insert row into the dataset
                    clsFoodDonations.insert();

                    //Check if the new Donation date is different than the 
                    //Viewing Donations date
                    if (dtDonationDate.Value != dtDateDL.Value)
                    {
                        //If so, open all donations for the Add/Edit date
                        clsFoodDonations.openForDate(dtDonationDate.Value);

                        //Checks to see if we can just add this new date to the end
                        //of the colection of Donation dates or if we need to insert
                        //into proper place at the dateIndex
                        if (dateIndex != donationDates.Count - 1)
                            donationDates.Insert(dateIndex, dtDonationDate.Value);
                        else
                            donationDates.Add(dtDonationDate.Value);
                    }
                    clsFoodDonations.openForDate(dtDonationDate.Value);
                    //Reload the Donation Log ListView
                    dateIndex = donationDates.IndexOf(dtDonationDate.Value);
                    loadDonationLogList();

                    //Set mode
                    changedMode();
                }
                else
                {
                    //Tell user they need to select a Donor And Make Sure Number Pounds Inputed
                    MessageBox.Show("Please Select A Donor, Fill In Number Of Pounds, And Try Again");
                }
            }
            else   //In Update MODE
            {
                //If a Donor has been selected
                if (tbDonorID.Text.Trim() != "" && tbName.Text.Trim() != "" && tbLbs.Text.Trim() != "")
                {
                    //Go through each textbox and set data int the dataset
                    foreach (TextBox tb in tbList)
                    {
                        if (tb.Tag != null && tb.Tag.ToString() != "")
                        {
                            clsFoodDonations.SetDataValue(tb.Tag.ToString(), tb.Text);
                        }
                    }
                    //Set the rest of data from datepickers and combos
                    clsFoodDonations.SetDataValue(cboDonationType.Tag.ToString(),
                        cboDonationType.SelectedValue.ToString());
                    clsFoodDonations.SetDataValue(cboFoodCat.Tag.ToString(),
                        cboFoodCat.SelectedValue.ToString());
                    clsFoodDonations.TrxDate = dtDonationDate.Value;
                    //Update database
                    clsFoodDonations.update();
                    clsFoodDonations.openForDate(donationDates[dateIndex]);
                    //Reload the Donation Log ListView
                    loadDonationLogList();
                    //Sets update mode to false
                    update = false;
                    //Set mode
                    changedMode();
                }
                else
                {
                    //Tell user they need to select a Donor And Make Sure Number Pounds Inputed
                    MessageBox.Show("Please Select A Donor, Fill In Number Of Pounds, And Try Again");
                }
            }
        }

        /// <summary>
        /// Event Fires when the user clicks on the new Donation Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewDonation_Click(object sender, EventArgs e)
        {
            lvDonorHistory.Items.Clear();
            //Set Mode for New Donation
            changedMode();
            //Initialize all controls in the new donation GroupBox
            initNewDonationGB();
        }

        /// <summary>
        /// Event fires when user clicks on the Next Donation Date Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNextDL_Click(object sender, EventArgs e)
        {
            //If not at the first date in collection
            if (dateIndex != 0)
            {
                //Subtract one from the dateIndex
                dateIndex--;
                //Open all donations for that date
                clsFoodDonations.openForDate(donationDates[dateIndex]);
                //Set DatePicker
                dtDateDL.Value = donationDates[dateIndex];
                //Load all Donations For that date
                loadDonationLogList();

                //Enable or Dissable proper buttons
                btnNextDL.Enabled = true;
                btnPrevDL.Enabled = true;               
            }
            else
            {
                btnPrevDL.Enabled = true;
                btnNextDL.Enabled = false;
            }
        }

        /// <summary>
        /// Event Fires when the User clicks on the 
        /// previous donoation date button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrevDL_Click(object sender, EventArgs e)
        {
            //If not at the last date in Donation Date Collection
            if (dateIndex != donationDates.Count - 1)
            {
                //Add one to the date index
                dateIndex++;
                //Open all donations for that date
                clsFoodDonations.openForDate(donationDates[dateIndex]);
                //Set DatePicker
                dtDateDL.Value = donationDates[dateIndex];
                //Load the Donations into the listview
                loadDonationLogList();

                //Set State for the proper buttons
                btnNextDL.Enabled = true;
                btnPrevDL.Enabled = true;
            }
            else
            {
                btnPrevDL.Enabled = false;
                btnNextDL.Enabled = true;
            }
        }

        /// <summary>
        /// Event fires when the DatePicker valuse changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtDateDL_ValueChanged(object sender, EventArgs e)
        {
            //Open all Donations for the given date
            clsFoodDonations.openForDate(dtDateDL.Value);
            //See if date exists in collection and set dateIndex if it does
            dateIndex = donationDates.IndexOf(dtDateDL.Value);
            //Load Donations into the listview
            loadDonationLogList();
            //If date was found in collection
            if (dateIndex == -1)
            {
                //If given date is greater than the Max Date in colection
                if(dtDateDL.Value > donationDates[0])
                {
                    //Dissable the next button
                    btnNextDL.Enabled = false;
                }
                //Else, if date is less than the min date in collection
                else if (dtDateDL.Value < donationDates[donationDates.Count - 1])
                {
                    //Set dateIndex to the end of collection
                    dateIndex = donationDates.Count;
                    //Dissable the Previous Button
                    btnPrevDL.Enabled = false;
                }
                else
                {
                    //Traverse the collection and find out where the new
                    //Date should be inserted into collection 
                    //if the user does a new Donation
                    for (int i = 0; i < donationDates.Count; i++)
                    {
                        if (donationDates[i] > dtDateDL.Value)
                        {
                            dateIndex = i - 1;
                        }
                    }
                }
            }
        }

        //Fills the combos for Form with the proper data
        private void fillCombos()
        {
            CCFBGlobal.InitCombo(cboFoodCat, CCFBGlobal.parmTbl_FoodClass);
            CCFBGlobal.InitCombo(cboDonationType, CCFBGlobal.parmTbl_Donation);
        }

        /// <summary>
        /// Fills the Collection of Donation Dates
        /// </summary>
        private void fillDonationDates()
        {
            //For each date in FoodDonations Dataset of distinct food donnation dates
            for (int i = 0; i < clsFoodDonations.RowCount; i++)
            {
                //Add date to collection
                donationDates.Add(clsFoodDonations.DSet.Tables[0].Rows[i].Field<DateTime>("TrxDate"));
            }
            //If at least one was added
            if (donationDates.Count > 0)
            {
                //Set the datePicker to the Max date in list
                dtDateDL.Value = donationDates[0];
                //Open all donations for that date
                clsFoodDonations.openForDate(donationDates[0]);
               
                //Enable and dissable proper buttons
                btnFirstDL.Enabled = true;
                btnLastDL.Enabled = true;
                btnNextDL.Enabled = false;
                btnPrevDL.Enabled = true;
            }
            else
            {
                //Enable and dissable proper buttons
                btnFirstDL.Enabled = false;
                btnLastDL.Enabled = false;
                btnNextDL.Enabled = false;
                btnPrevDL.Enabled = false;
            }
        }

        /// <summary>
        /// Used when an edit of a foodDonation happens
        /// Loads the textboxes with the data from the 
        /// dataset
        /// Called after the Find() is called for the foodDonation
        /// </summary>
        private void fillgbLogEntryControls(string TrxId)
        {
            //Find the Donation in the FoodDonations class dataset
            clsFoodDonations.find(Convert.ToInt32(TrxId));
            foreach (TextBox tb in tbList)
            {
                tb.Text = "";
                if(tb.Tag != null && tb.Tag.ToString() != "")
                {
                    //Gets the Data from the dataset
                    tb.Text = clsFoodDonations.GetDataValue(tb.Tag.ToString()).ToString();
                }
            }
            //Sets the combo box for food catagory to proper index
            cboFoodCat.SelectedValue = clsFoodDonations.FoodClass.ToString();

            if (cboFoodCat.SelectedIndex < 0)
                cboFoodCat.SelectedIndex = 0;

            //Set donation date to current selected donation date
            dtDonationDate.Value = dtDateDL.Value;

            //Set the Donation Type combo to proper index
            cboDonationType.SelectedValue = clsFoodDonations.DonationType.ToString();

            if (cboDonationType.SelectedIndex < 0)
                cboDonationType.SelectedIndex = 0;
        }

        private void gbDailyLog_Enter(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Initializes the New/Edit Donation Groupbox to
        /// Blank textboxes and proper selected indexes
        /// </summary>
        private void initNewDonationGB()
        {
            foreach (TextBox tb in tbList)
            {
                if (tb.Tag == null || tb.Tag.ToString() != "Pounds")
                    tb.Text = "";
                else
                    tb.Text = (0).ToString();
            }
            if (cboDonationType.Items.Count > 0)
                cboDonationType.SelectedValue = clsDonors.DefaultDonationType;

            if (cboFoodCat.Items.Count > 0)
                cboFoodCat.SelectedIndex = 0;

            //Sets the donation date value
            dtDonationDate.Value = dtDateDL.Value;
        }

        /// <summary>
        /// Loads the Donation Log with the Donations for the selected date
        /// </summary>
        private void loadDonationLogList()
        {
            double totLbs = 0;
            int totCnt = 0;
            lvFoodDonations.Items.Clear();
            ListViewItem lvItm;
            btnEditDnrTrx.Text = "";
            btnEditDnrTrx.Enabled = false;
            btnDeleteTrx.Text = "";
            btnDeleteTrx.Enabled = false;
            btnShowDnrHist.Text = "";
            btnShowDnrHist.Enabled = false; 

            for (int i = 0; i < clsFoodDonations.RowCount; i++)
            {
                clsFoodDonations.setDataRow(i);
                lvItm = new ListViewItem();
                lvItm.Name = clsFoodDonations.TrxID.ToString();
                lvItm.Text=(i + 1).ToString();
                lvItm.SubItems.Add(CCFBGlobal.LongNameFromId(CCFBGlobal.parmTbl_Donation,clsFoodDonations.DonationType));
                clsDonors.find(clsFoodDonations.DonorID);
                lvItm.SubItems.Add(clsDonors.Name);
                lvItm.SubItems.Add(clsFoodDonations.Pounds.ToString());
                lvItm.SubItems.Add(clsFoodDonations.Notes);
                lvItm.SubItems.Add(CCFBGlobal.LongNameFromId(CCFBGlobal.parmTbl_FoodClass, clsFoodDonations.FoodClass));
                lvItm.SubItems.Add(clsFoodDonations.FoodCode);   
                lvItm.SubItems.Add(clsFoodDonations.DonorID.ToString());
                lvItm.SubItems.Add(clsFoodDonations.TrxID.ToString());
                lvFoodDonations.Items.Add(lvItm);
                totCnt++;
                totLbs += clsFoodDonations.Pounds;
            }
            tbTotalCount.Text = totCnt.ToString();
            tbTotalLbs.Text = totLbs.ToString();
        }

        /// <summary>
        /// Loads the Donor log listView with the donoations for the 
        /// Selected donor
        /// </summary>
        private void loadDonorLogList()
        {
            double totLbs = 0;
            int totCnt = 0;
            lvDonorHistory.Items.Clear();
            Application.DoEvents();
            ListViewItem lvItm;

            string donorWhereClause = " WHERE DonorID=" + donorID.ToString();
            switch (cboDonorPeriod.SelectedIndex)
	        {
                case 0: donorWhereClause += CCFBGlobal.SQLDateRangeCurMonth();   break;
                case 1: donorWhereClause += CCFBGlobal.SQLDateRangePrevMonth();  break;
                case 2: donorWhereClause += CCFBGlobal.SQLDateRangeLast90Days(); break;
                case 3: donorWhereClause += CCFBGlobal.SQLDateRangeCurYear();    break;
                case 4: donorWhereClause += CCFBGlobal.SQLDateRangePrevYear();   break;
		        default:donorWhereClause += DateRangeCustom();                   break;
	        }

            clsFoodDonations.openWhere(donorWhereClause);

            for (int i = 0; i < clsFoodDonations.RowCount; i++)
            {
                clsFoodDonations.setDataRow(i);
                lvItm = new ListViewItem((i + 1).ToString());
                lvItm.SubItems.Add(CCFBGlobal.LongNameFromId(CCFBGlobal.parmTbl_Donation, clsFoodDonations.DonationType));
                lvItm.SubItems.Add(clsFoodDonations.TrxDate.ToShortDateString());
                lvItm.SubItems.Add(clsFoodDonations.Pounds.ToString());
                lvItm.SubItems.Add(clsFoodDonations.Notes);
                lvItm.SubItems.Add(CCFBGlobal.LongNameFromId(CCFBGlobal.parmTbl_FoodClass, clsFoodDonations.FoodClass));
                lvItm.SubItems.Add(clsFoodDonations.FoodCode);
                lvItm.SubItems.Add(clsFoodDonations.TrxID.ToString());
                lvDonorHistory.Items.Add(lvItm);

                totCnt++;
                totLbs += clsFoodDonations.Pounds;
            }
            tbDonorCnt.Text = totCnt.ToString();
            tbDonorLbs.Text = totLbs.ToString();
        }

        public string DateRangeCustom()
        {
            return " And TrxDate BETWEEN '"
                + dtpFrom.Value.ToShortDateString() + "'"
                + " And '" + dtpTo.Value.ToShortDateString() + "'";
        }

        /// <summary>
        /// Used to set the donor from the Edit Donors Form
        /// when a Donor has been selected
        /// </summary>
        /// <param name="dnrID">The Donor ID that was chosen</param>
        //public void setDonor(int dnrID)
        //{
        //    donorID = dnrID;
        //    clsDonors.find(dnrID);
        //    tbName.Text = clsDonors.Name;
        //    tbDonorID.Text = dnrID.ToString();

        //    clsFoodDonations.openWhere(" Where DonorID=" + dnrID.ToString());
        //    loadDonorLogList();
        //    clsFoodDonations.openForDate(dtDonationDate.Value);
        //}

        /// <summary>
        /// Changes the current mode from view existing Donations for date to
        /// Enter New or Edit Selected Donation
        /// Changes visibility on the different controls
        /// </summary>
        private void changedMode()
        {
            if (gbLogEntry.Visible == true)
            {
                gbLogEntry.Visible = false;
                gbDnrHist.Visible = false;
                gbDailyLog.Visible = true;
                this.BackColor = Color.Cornsilk;
            }
            else
            {
                this.BackColor = Color.LightGreen;
                gbLogEntry.SetBounds(45, 0, 744, 256);
                gbDnrHist.SetBounds(0, 258, Width - 19, Height - 270);
                gbLogEntry.Visible = true;
                gbDnrHist.Visible = true;
                gbDailyLog.Visible = false;
            }
        }

        private void btnShowDnrHist_Click(object sender, EventArgs e)
        {
            if (lvFoodDonations.FocusedItem != null)
            {
                clsDonors.find(donorIdFromGrid());
                gbDnrHist.Text = "[" + clsDonors.ID.ToString() + "] " + clsDonors.Name + " Donation History";

                gbDailyLog.Visible = false;
                gbDnrHist.Visible = true;
                gbDnrHist.SetBounds(0, 0, Width - 19, Height - 19);
                Application.DoEvents();
                gbDnrHist.BringToFront();
                loadDonorLogList(); 
            }
        }

        private void btnHideDonorHist_Click(object sender, EventArgs e)
        {
            clsFoodDonations.openForDate(dtDateDL.Value);
            gbDailyLog.Visible = true;
            gbDnrHist.Visible = false;
        }

        private void lvFoodDonations_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvFoodDonations.FocusedItem != null)
            {
                donorID = donorIdFromGrid();
                btnShowDnrHist.Tag = donorID.ToString();
                btnShowDnrHist.Text = "Show Donor [" + donorID.ToString() + "] History\r\n"
                    + lvFoodDonations.FocusedItem.SubItems[2].Text;
                btnShowDnrHist.Enabled = true;
                trxID = Convert.ToInt32(lvFoodDonations.FocusedItem.Name);
                btnEditDnrTrx.Tag = trxID;
                btnEditDnrTrx.Text = "Edit Donation [" + trxID.ToString() + "]\r\n"
                    + lvFoodDonations.FocusedItem.SubItems[2].Text + "\r\n"
                    + lvFoodDonations.FocusedItem.SubItems[3].Text + " lbs.";
                btnEditDnrTrx.Enabled = true;
                btnDeleteTrx.Tag = trxID;
                btnDeleteTrx.Text = "Delete Donation [" + trxID.ToString() + "]";
                btnDeleteTrx.Enabled = true;
            }
        }

        private void FoodDonationsForm_ResizeEnd(object sender, EventArgs e)
        {
            gbDnrHist.SetBounds(0, 0, Width - 19, Height - 25);
            gbDailyLog.SetBounds(0, 0, Width - 19, Height - 25);
        }

        private void gbDailyLog_Resize(object sender, EventArgs e)
        {
            lvFoodDonations.Height = gbDailyLog.Height - lvFoodDonations.Location.Y -10;
            lvFoodDonations.Width = gbDailyLog.Width - 2 * lvFoodDonations.Location.X;
            btnClose.SetBounds(gbDailyLog.Width - btnClose.Width - 5
                                    , btnClose.Location.Y
                                    , btnClose.Width
                                    , btnClose.Height);
        }

        private void gbDnrHist_Resize(object sender, EventArgs e)
        {
            lvDonorHistory.Height = gbDnrHist.Height - lvDonorHistory.Location.Y - 25;
            lvDonorHistory.Width = gbDnrHist.Width - 2 * lvDonorHistory.Location.X;
            btnHideDonorHist.SetBounds(gbDnrHist.Width - btnHideDonorHist.Width - 5
                                    , btnHideDonorHist.Location.Y
                                    , btnHideDonorHist.Width
                                    , btnHideDonorHist.Height);
        }

        private void btnLoadCustom_Click(object sender, EventArgs e)
        {
            loadDonorLogList();
        }

        private int donorIdFromGrid()
        {
            return Convert.ToInt32(lvFoodDonations.FocusedItem.SubItems[7].Text);
        }

        private void cboDonorPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bNormalMode == true)
            {
                if (cboDonorPeriod.SelectedIndex == 6)
                {
                    lblFrom.Visible = true;
                    lblTo.Visible = true;
                    dtpFrom.Visible = true;
                    dtpTo.Visible = true;
                    btnLoadCustom.Visible = true;
                }
                else
                {
                    lblFrom.Visible = false;
                    lblTo.Visible = false;
                    dtpFrom.Visible = false;
                    dtpTo.Visible = false;
                    btnLoadCustom.Visible = false;
                    loadDonorLogList();
                }
            }
        }

        private void btnDeleteTrx_Click(object sender, EventArgs e)
        {
            int trxId = Convert.ToInt32(btnDeleteTrx.Tag);
            if (trxId > 0)
            {
                clsFoodDonations.delete(trxId);
                clsFoodDonations.openForDate(dtDateDL.Value);
                loadDonationLogList();
            }
        }
    }
}
