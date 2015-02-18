using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ClientcardFB3
{
    public partial class CashDonationsForm : Form
    {
        EditDonorForm frmEditDonors;
        Donors clsDonors = new Donors(CCFBGlobal.connectionString);
        CashDonations clsCashDonations = new CashDonations(CCFBGlobal.connectionString);

        List<TextBox> tbList = new List<TextBox>();
        List<DateTime> donationDates;
        int currentIndex = 0;
        int donorID = -1;
        int trxID = -1;
        ListViewItem lvItmCurrent;
        bool update = false;
        bool bNormalMode = false;

        DateTime periodStartDate;
        DateTime periodEndDate;
                
        /// <summary>
        /// Constructor for the FoodDonations Form
        /// </summary>
        public CashDonationsForm()
        {
            InitializeComponent();
            spltcontLog.Visible = true;
            spltcontEdit.Visible = false;

            //clsParmDonationType.openAll();      //Open all parm data for Donations
            clsDonors.openWhere("");                //Opens all Donors
            //clsParmFoodClass.openAll();         //Opens all Food Classes for Donations

            frmEditDonors = new EditDonorForm(CCFBGlobal.connectionString, true);          

            LoadcboYear();
            cboReportMonth.SelectedIndex = DateTime.Today.Month - 1;


            //Adds each text boxes to the collection for easy use later
            foreach (TextBox tb in spltcontEdit.Panel1.Controls.OfType<TextBox>())
            {
                tbList.Add(tb);
            }
            pnlEntryDate.SetBounds(pnlPeriod.Left, pnlPeriod.Top, pnlPeriod.Width, pnlPeriod.Height);
            cboDisplayType.SelectedIndex = 1;
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
                if (clsDonors.find(donorID) == false)
                {
                    clsDonors.open(donorID);
                }
                tbName.Text = clsDonors.Name;
                tbDonorID.Text = donorID.ToString();
                clsCashDonations.openWhere(" Where DonorID=" + donorID.ToString());
                loadDonorLogList();
                clsCashDonations.openWhere(" Where TrxDate Between '" + periodStartDate.ToShortDateString() 
                    + "' And '" + periodEndDate.ToShortDateString() + "'");
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
            btnHideDonorHist.Enabled = false;
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
                tbTrxID.Text = clsCashDonations.TrxID.ToString();
            }
            else
            {
                MessageBox.Show("There Is No Selected Donation. Please Select A Donation And Try Again");
            }
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
            btnHideDonorHist.Enabled = true;
        }

        /// <summary>
        /// Event fires when the user clicks on the Save buton
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogEntrySave_Click(object sender, EventArgs e)
        {
            btnHideDonorHist.Enabled = true;
            //If we are in Insert Mode
            if (update == false)
            {
                //If a Donor has been selected
                if (tbDonorID.Text.Trim().Length >0 && tbName.Text.Trim().Length >0 && tbDollars.Text.Trim().Length >0)
                {
                    //Create a new row from the DataSet
                    DataRow drow = clsCashDonations.DSet.Tables[0].NewRow();

                    //Go through the textboxes and insert data into the new row
                    foreach (TextBox tb in tbList)
                    {
                        if (tb.Tag != null && tb.Tag.ToString().Length >0)
                            drow[tb.Tag.ToString()] = tb.Text;
                    }

                    //Add the other Data to the row
                    drow["TrxDate"] = dtDonationDate.Value;
                    drow["CreatedBy"] = CCFBGlobal.dbUserName;
                    drow["Created"] = DateTime.Now;
                    clsCashDonations.DSet.Tables[0].Rows.Add(drow);
                    //Insert row into the dataset
                    clsCashDonations.insert();
                    clsCashDonations.openWhere(" Where TrxDate Between '" + periodStartDate.ToShortDateString() 
                        + "' And '" + periodEndDate.ToShortDateString() + "'");
                    //Reload the Donation Log ListView
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
                if (tbDonorID.Text.Trim().Length >0 && tbName.Text.Trim().Length >0 && tbDollars.Text.Trim().Length >0)
                {
                    //Go through each textbox and set data int the dataset
                    foreach (TextBox tb in tbList)
                    {
                        if (tb.Tag != null && tb.Tag.ToString().Length >0)
                        {
                            clsCashDonations.SetDataValue(tb.Tag.ToString(), tb.Text);
                        }
                    }

                    clsCashDonations.TrxDate = dtDonationDate.Value;

                    //Update database
                    clsCashDonations.update();
                    clsCashDonations.openWhere(" Where TrxDate Between '" + periodStartDate.ToShortDateString() 
                        + "' And '" + periodEndDate.ToShortDateString() + "'");
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
        /// Loads the Years into the Year Combo Box
        /// </summary>
        private void LoadcboYear()
        {
            cboYear.Items.Clear();
            cboYear.Items.Add(DateTime.Today.Year.ToString());
            clsCashDonations.openDistinctDonationYears();
            string sYear = "";
            for (int i = 0; i < clsCashDonations.RowCount; i++)
            {
                sYear = clsCashDonations.DSet.Tables[0].Rows[i].Field<string>(0).ToString();
                if (sYear != DateTime.Today.Year.ToString())
                    cboYear.Items.Add(sYear);
            }

            if (cboYear.Items.Count == 0)
                cboYear.Items.Add(DateTime.Today.Year);

            cboYear.SelectedIndex = 0;
        }

        /// <summary>
        /// Fills the Collection of Donation Dates
        /// </summary>
        private void fillDonationDates()
        {
            donationDates = new List<DateTime>();
            //For each date in FoodDonations Dataset of distinct food donnation dates
            for (int i = 0; i < clsCashDonations.RowCount; i++)
            {
                //Add date to collection
                donationDates.Add(Convert.ToDateTime(clsCashDonations.DSet.Tables[0].Rows[i][0]));
            }
            //If at least one was added
            if (donationDates.Count > 0)
            {
                //Set the datePicker to the Max date in list
                dtDateDL.Value = donationDates[0];
                //Open all donations for that date
                clsCashDonations.openForDate(cboDisplayType.SelectedIndex, donationDates[0]);

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
            clsCashDonations.find(Convert.ToInt32(TrxId));
            dtDonationDate.Value = clsCashDonations.TrxDate;
            foreach (TextBox tb in tbList)
            {
                tb.Text = "";
                if(tb.Tag != null && tb.Tag.ToString().Length >0)
                {
                    //Gets the Data from the dataset
                    tb.Text = clsCashDonations.GetDataValue(tb.Tag.ToString()).ToString();
                }
            }
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

            dtDonationDate.Value = DateTime.Today;
            btnHideDonorHist.Enabled = false;
        }

        /// <summary>
        /// Loads the Donation Log with the Donations for the selected date
        /// </summary>
        private void loadDonationLogList()
        {
            decimal totDollars = 0;
            int totCnt = 0;
            lvFoodDonations.Items.Clear();
            ListViewItem lvItm;
            btnEditDnrTrx.Text = "";
            btnEditDnrTrx.Enabled = false;
            btnShowDnrHist.Text = "";
            btnShowDnrHist.Enabled = false;
            btnDeleteTrx.Text = "";
            btnDeleteTrx.Enabled = false;
            string currentDate = "";

            for (int i = 0; i < clsCashDonations.RowCount; i++)
            {
                clsCashDonations.setDataRow(i);

                if (currentDate.Length >0 && currentDate != clsCashDonations.TrxDate.ToShortDateString())
                {
                    lvItm = new ListViewItem();
                    lvItm.BackColor = Color.PaleGoldenrod;
                    lvItm.Tag = "sep";
                    lvFoodDonations.Items.Add(lvItm);
                }           

                lvItm = new ListViewItem();
                lvItm.Tag = i;
                lvItm.Name = clsCashDonations.TrxID.ToString();
                lvItm.Text=(i + 1).ToString();
                clsDonors.find(clsCashDonations.DonorID);
                
                if (currentDate != clsCashDonations.TrxDate.ToShortDateString())
                {
                    currentDate = clsCashDonations.TrxDate.ToShortDateString();
                    lvItm.SubItems.Add(clsCashDonations.TrxDate.ToShortDateString());
                }
                else
                {
                    lvItm.SubItems.Add("..:..:....");
                }

                lvItm.SubItems.Add(clsDonors.Name);
                lvItm.SubItems.Add(String.Format("{0:0,0.00}",clsCashDonations.DollarValue));
                lvItm.SubItems.Add(clsCashDonations.Notes); 
                lvItm.SubItems.Add(clsCashDonations.DonorID.ToString());
                lvItm.SubItems.Add(clsCashDonations.TrxID.ToString());
                lvFoodDonations.Items.Add(lvItm);
                totCnt++;
                totDollars += clsCashDonations.DollarValue;
            }
            tbTotalCount.Text = CCFBGlobal.formatNumberWithCommas(totCnt);
            tbTotalDollars.Text = String.Format("{0:0,0.00}", totDollars);

            if (lvFoodDonations.Items.Count > 0)
                lvFoodDonations.Items[0].Focused = true;

            btnNewDonation.Enabled = true;
        }

        /// <summary>
        /// Loads the Donor log listView with the donoations for the 
        /// Selected donor
        /// </summary>
        private void loadDonorLogList()
        {
            decimal totDollars = 0;
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
                case 5: break;
		        default:donorWhereClause += DateRangeCustom();                   break;
	        }

            clsCashDonations.openWhere(donorWhereClause);

            for (int i = 0; i < clsCashDonations.RowCount; i++)
            {
                clsCashDonations.setDataRow(i);
                lvItm = new ListViewItem((i + 1).ToString());
                lvItm.SubItems.Add(clsCashDonations.TrxDate.ToShortDateString());
                lvItm.SubItems.Add(String.Format("{0:0,0.00}",clsCashDonations.DollarValue));
                lvItm.SubItems.Add(clsCashDonations.Notes);
                lvItm.SubItems.Add(clsCashDonations.TrxID.ToString());
                lvDonorHistory.Items.Add(lvItm);

                totCnt++;
                totDollars += clsCashDonations.DollarValue;
            }
            tbDonorCnt.Text = CCFBGlobal.formatNumberWithCommas(totCnt);
            tbTotalDonorDollars.Text = String.Format("{0:0,0.00}", totDollars);
        }

        public string DateRangeCustom()
        {
            return " And TrxDate BETWEEN '"
                + dtpFrom.Value.ToShortDateString() + "'"
                + " And '" + dtpTo.Value.ToShortDateString() + "'";
        }

        /// <summary>
        /// Changes the current mode from view existing Donations for date to
        /// Enter New or Edit Selected Donation
        /// Changes visibility on the different controls
        /// </summary>
        private void changedMode()
        {
            if (spltcontEdit.Visible == true)
            {
                spltcontEdit.Visible = false;
                spltcontLog.Visible = true;
            }
            else
            {
                spltcontEdit.Visible = true;
                spltcontLog.Visible = false;
            }
        }

        private void btnShowDnrHist_Click(object sender, EventArgs e)
        {
            if (lvFoodDonations.FocusedItem != null)
            {
                clsDonors.find(donorIdFromGrid());
                lblDonorHist.Text = "[" + clsDonors.ID.ToString() + "] " + clsDonors.Name;

                spltcontLog.Visible = false;
                spltcontEdit.Panel1Collapsed = true;
                spltcontEdit.Visible = true;
                loadDonorLogList(); 
            }
        }

        private void btnHideDonorHist_Click(object sender, EventArgs e)
        {
            clsCashDonations.openWhere(" Where TrxDate Between '" + periodStartDate.ToShortDateString() + "' And '"
             + periodEndDate.ToShortDateString() + "'");
            spltcontLog.Visible = true;
            spltcontEdit.Panel1Collapsed = false;
            spltcontEdit.Visible = false;
        }

        private void lvFoodDonations_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvFoodDonations.FocusedItem != null && lvFoodDonations.FocusedItem.Tag.ToString() != "sep")
            {
                if (lvItmCurrent != null)
                {
                    lvItmCurrent.Font = new Font(lvFoodDonations.Font, FontStyle.Regular);
                    lvItmCurrent.ForeColor = Color.Black;
                }
                lvItmCurrent = lvFoodDonations.FocusedItem;

                lvItmCurrent.Font = new Font(lvFoodDonations.Font, FontStyle.Bold);
                lvItmCurrent.ForeColor = Color.Maroon;

                donorID = donorIdFromGrid();
                btnShowDnrHist.Tag = donorID.ToString();
                btnShowDnrHist.Text = "Show Donor [" + donorID.ToString() + "] History\r\n"
                    + lvItmCurrent.SubItems[2].Text;
                btnShowDnrHist.Enabled = true;
                trxID = Convert.ToInt32(lvItmCurrent.Name);
                btnEditDnrTrx.Tag = trxID;
                btnEditDnrTrx.Text = "Edit Donation [" + trxID.ToString() + "]\r\n"
                    + lvItmCurrent.SubItems[2].Text + "\r\n"
                    + lvItmCurrent.SubItems[3].Text;
                btnEditDnrTrx.Enabled = true;
                btnDeleteTrx.Tag = trxID;
                btnDeleteTrx.Text = "Delete Donation [" + trxID.ToString() + "]";
                btnDeleteTrx.Enabled = true;
            }
            else
            {
                btnShowDnrHist.Tag = donorID.ToString();
                btnShowDnrHist.Text = "";
                btnShowDnrHist.Enabled = false;
                btnEditDnrTrx.Tag = -1;
                btnEditDnrTrx.Text = "";
                btnEditDnrTrx.Enabled = false;
                btnDeleteTrx.Tag = -1;
                btnDeleteTrx.Text = "";
                btnDeleteTrx.Enabled = false;
            }
        }

        private void btnDeleteTrx_Click(object sender, EventArgs e)
        {
            int trxId = Convert.ToInt32(btnDeleteTrx.Tag);
            if (trxId > 0)
            {
                clsCashDonations.delete(trxId);
                periodStartDate = new DateTime(Convert.ToInt32(cboYear.SelectedItem), cboReportMonth.SelectedIndex + 1, 1);
                periodEndDate = periodStartDate.AddMonths(1).AddDays(-1);

                clsCashDonations.openWhere(" Where TrxDate Between '" + periodStartDate.ToShortDateString() + "' And '"
                    + periodEndDate.ToShortDateString() + "'");
                loadDonationLogList();
            }
        }

        private void btnLoadCustom_Click(object sender, EventArgs e)
        {
            loadDonorLogList();
        }

        private int donorIdFromGrid()
        {
            return Convert.ToInt32(lvFoodDonations.FocusedItem.SubItems[5].Text);
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

        private void btnLoadPeriodData_Click(object sender, EventArgs e)
        {
            periodStartDate = new DateTime(Convert.ToInt32(cboYear.SelectedItem), cboReportMonth.SelectedIndex + 1, 1);
            periodEndDate = periodStartDate.AddMonths(1).AddDays(-1);

            clsCashDonations.openWhere(" Where TrxDate Between '" + periodStartDate.ToShortDateString() + "' And '" 
                + periodEndDate.ToShortDateString() + "'");
            loadDonationLogList();
        }

        private void tbDollars_KeyDown(object sender, KeyEventArgs e)
        {
            CCFBGlobal.checkForDecimalOnKeyPress(e);
        }

        private void tbDollars_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                Convert.ToDecimal(tbDollars.Text);
            }
            catch
            {
                if (MessageBox.Show("FORMAT ERROR: Please Check Format And Try Again", "FORMAT ERROR",
                    MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == System.Windows.Forms.DialogResult.Retry)
                {
                    e.Cancel = true;
                    tbDollars.SelectAll();
                }
                else
                {
                    tbDollars.Text = "0";
                    e.Cancel = false;
                }
            }
        }

        private void tbDonorID_Leave(object sender, EventArgs e)
        {
            if (tbDonorID.Text.Trim().Length >0 && checkForDonor() == false)
                tbDonorID.Text = "";            
        }

        private void tbDonorID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                CCFBGlobal.checkForIntOnKeyPress(e);
            else
                checkForDonor();
        }

        private bool checkForDonor()
        {
            bool found = false;
            if (found = clsDonors.find(Convert.ToInt32(tbDonorID.Text)) == true)
            {
                tbName.Text = clsDonors.Name;
            }
            else
            {
                MessageBox.Show("Donor ID Not Found. Please Check ID And Try Again");
                tbName.Text = "";
            }

            return found;
        }

        private void cboDisplayType_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlEntryDate.Visible = (cboDisplayType.SelectedIndex == 0);
            pnlPeriod.Visible = (cboDisplayType.SelectedIndex == 1);
            setupDatesToDisplay();
        }

        private void btnNavDL_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int delta = Convert.ToInt32(btn.Tag);
            switch (delta)
            {
                case -1:
                    if (currentIndex > 0)
                        currentIndex --;
                    break;
                case 0:
                    currentIndex = 0;
                    break;
                case 1:
                    if (currentIndex < donationDates.Count)
                        currentIndex ++;
                    break;
                case 99:
                    currentIndex = donationDates.Count - 1;
                    break;
                default:
                    break;
            }
            dtDateDL.Value = donationDates[currentIndex];
        }

        private void setupDatesToDisplay()
        {
            if (cboDisplayType.SelectedIndex == 0)
            {
                clsCashDonations.openDistinctDonationDates(cboDisplayType.SelectedIndex); //Gets all distinct Donation Dates in Database

                fillDonationDates();//Fills a collection with the ditinct Donation Dates
                loadDonationLogList();  //Loads the Donation Log For the current date in the ListView 
            }
        }

        private void dtDateDL_ValueChanged(object sender, EventArgs e)
        {
            clsCashDonations.openForDate(cboDisplayType.SelectedIndex, dtDateDL.Value);
            loadDonationLogList();
        }
    }
}
