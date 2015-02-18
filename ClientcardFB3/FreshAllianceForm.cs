using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Data.SqlClient;
using Microsoft.Office.Interop.Outlook;
using System.IO;

namespace ClientcardFB3
{
    public partial class FreshAllianceForm : Form
    {
        private System.Collections.ArrayList listDonors = new System.Collections.ArrayList();
        FoodDonations clsFoodDonations = new FoodDonations(CCFBGlobal.connectionString);
        MonthlyReports clsMonthlyReports = new MonthlyReports(CCFBGlobal.connectionString);
        DateTime dateStart;
        DateTime dateEnd;
        DateTime dateWeekStart;
        DateTime dateWeekEnd;
        //System.Collections.ArrayList foodClassList;

        DataSet dset;
        SqlConnection conn;
        SqlDataAdapter dadAdpt;
        SqlCommand commandByDay;
        SqlCommand commandByWeek;
        SqlCommandBuilder commBuilder;
 
        int donorID;
        int idbRowCount = 0;
        int dbRowIndex = -1;
        bool loadingData = false;
        bool isGroceryRescue = false;

        public FreshAllianceForm(int DonorID, int defaultDonorType, bool IsGroceryRescue)
        {
            InitializeComponent();
            pnlEditDaily.Visible = true;

            dset = new DataSet();
            conn = new SqlConnection(CCFBGlobal.connectionString);
            dadAdpt = new SqlDataAdapter();
            commandByWeek = new SqlCommand("GroceryRescueLbs", conn);
            commandByWeek.CommandType = CommandType.StoredProcedure;
            commandByWeek.Parameters.Add(new SqlParameter("@dateStart",SqlDbType.DateTime));
            commandByWeek.Parameters.Add(new SqlParameter("@dateEnd", SqlDbType.DateTime));
            commandByWeek.Parameters.Add(new SqlParameter("@DonorID", SqlDbType.Int));
            donorID = DonorID;
            LoadcboDonor();
            LoadcboYear();
            //foodClassList = CCFBGlobal.TypeCodesArray(CCFBGlobal.parmTbl_FoodClass);
            fillFoodClasses();
            cboReportMonth.SelectedIndex = DateTime.Today.Month - 1;
            CCFBGlobal.InitCombo(cboDonationType, "parm_DonationType");
            clsMonthlyReports.openWhere(" Where ReportName='Grocery Rescue'");
            cboDonationType.SelectedValue = defaultDonorType.ToString();
            isGroceryRescue = IsGroceryRescue;
            bool hasRows = cboStore.Items.Count > 0;
            btnLoadPeriodData.Enabled = hasRows;
        }

        /// <summary>
        /// Fills the Grids with the food classes from the parm_foodclass table
        /// </summary>
        private void fillFoodClasses()
        {
            //dgvMonthReceipts.Rows.Clear();
            //foreach (parmType pt in foodClassList)
            //{
            //    dgvMonthReceipts.Rows.Add();
            //    dgvMonthReceipts[0, dgvMonthReceipts.Rows.Count - 1].Tag = pt;
            //    dgvMonthReceipts[0, dgvMonthReceipts.Rows.Count - 1].Value = pt.LongName;
            //}

            //createTotalsRows();
        }

        private void createTotalsRows()
        {
            dgvMonthReceipts.Rows.Add();
            dgvMonthReceipts[0, dgvMonthReceipts.RowCount - 1].Value = "Total";
            dgvMonthReceipts.Rows[dgvMonthReceipts.RowCount - 1].ReadOnly = true;
            for (int i = 0; i < dgvMonthReceipts.Columns.Count; i++)
            {
                dgvMonthReceipts[i, dgvMonthReceipts.RowCount - 1].Style.BackColor = Color.LightGoldenrodYellow;
            }
            dgvMonthReceipts.Columns[dgvMonthReceipts.Columns.Count - 1].ReadOnly = true;
            for (int i = 0; i < dgvMonthReceipts.Rows.Count - 1; i++)
            {
                dgvMonthReceipts[dgvMonthReceipts.Columns.Count - 1,i].Style.BackColor = Color.LightGoldenrodYellow;
            }
        }

        /// <summary>
        /// Gets the week number of the year for the date
        /// </summary>
        /// <param name="dtPassed">A DateTime</param>
        /// <returns>(Int)The week number for the date in the year</returns>
        public static int GetWeekNumber(DateTime dtPassed)
        {
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            return ciCurr.Calendar.GetWeekOfYear(dtPassed, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
        }

        /// <summary>
        /// Retrives the Donation Data by week totals and loads them into the MonthTotals grid
        /// </summary>
        //private void getAndLoadByWeekData()
        //{
        //    dgvMonthReceipts.Columns[0].HeaderText = "Food Classes - " + cboReportMonth.SelectedItem.ToString()
        //        + "/" + cboYear.SelectedItem.ToString();
        //    commandByWeek.Parameters[0].Value = dateStart.ToString();
        //    commandByWeek.Parameters[1].Value = dateEnd.ToString();
        //    commandByWeek.Parameters[2].Value = Convert.ToInt32(cboStore.SelectedValue);
        //    try
        //    {
        //        if (conn.State == ConnectionState.Closed)
        //        { conn.Open(); }

        //        SqlDataReader reader = commandByWeek.ExecuteReader();

        //        while (reader.Read())
        //        {
        //            int icol = Convert.ToInt32(reader.GetValue(0));
        //            int irow = findRowInGridFromFoodClass(Convert.ToInt32(reader.GetValue(1)));
        //            //dgvMonthTotals[icol, irow].Value = CCFBGlobal.formatNumberWithCommas(reader.GetValue(2));
        //        }

        //        if (conn.State == ConnectionState.Open)
        //        { conn.Close(); }
        //    }
        //    catch(SqlException ex)
        //    {
        //        CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString());
        //        if (conn.State == ConnectionState.Open)
        //            conn.Close();
        //    }

        //    if(conn.State == ConnectionState.Open)
        //        conn.Close();
        //}

        /// <summary>
        /// Initializes the Dates found in period to empty
        /// </summary>
        private void initList()
        {
            for (int i = 0; i < dgvMonthReceipts.Rows.Count-1; i++)
            {
                for (int j = 0; j < dgvMonthReceipts.Columns.Count - 1; j++)
                {
                    dgvMonthReceipts[j, i].Value = null;
                    dgvMonthReceipts[j, i].Tag = null;
                    dgvMonthReceipts[j, i].Style.BackColor = Color.White;
                }
            }
        }

        private int findRowInGridFromFoodClass(int foodClassID)
        {
            for (int i = 0; i < dgvMonthReceipts.Rows.Count; i++)
            {
                if (dgvMonthReceipts[0, i].Tag != null && ((parmType)dgvMonthReceipts[0, i].Tag).ID
                    == foodClassID)
                {
                    return i;
                }
            }
            return 0;
        }

        /// <summary>
        /// Zeros out the Weely Totals
        /// </summary>
        private void zeroOutTotalsByDay()
        {
            for (int i = 0; i < dgvMonthReceipts.Rows.Count; i++)
            {
                dgvMonthReceipts[dgvMonthReceipts.Columns.Count-1, i].Value = 0;
            }
        }


        /// <summary>
        /// Loads the View By Day Donations Grid
        /// </summary>
        private void loadByDayData()
        {
            DateTime trxDate;
            int foodClass = 0;
            int colFoodClass = 0;
            int rowTrxDate = 0;
            for(int i = 0; i < idbRowCount; i++)
            {
                trxDate = dset.Tables[0].Rows[i].Field<DateTime>("TrxDate");
                foodClass = dset.Tables[0].Rows[i].Field<short>("FoodClass");
                foreach (parmType item in CCFBGlobal.TypeCodesArray(CCFBGlobal.parmTbl_FoodClass))
                {
                    if (item.ID == foodClass)
                    {
                        colFoodClass = item.SortOrder;
                        break;
                    }
                }

                rowTrxDate = trxDate.Day - 1;
                if(rowTrxDate >= 0 && rowTrxDate < dgvMonthReceipts.Rows.Count)
                {
                    //for (int j = 0; j < dgvMonthReceipts.Rows.Count; j++)
                    //{
                        //if (dgvMonthReceipts[colFoodClass, rowTrxDate].Tag != null && ((parmType)dgvMonthReceipts[colFoodClass, rowTrxDate].Tag).UID == dset.Tables[0].Rows[i]["FoodClass"].ToString())
                        //{
                            dgvMonthReceipts[colFoodClass, rowTrxDate].Value =
                               CCFBGlobal.formatNumberWithCommas(dset.Tables[0].Rows[i]["Pounds"]);
                            dgvMonthReceipts[colFoodClass, rowTrxDate].Tag =
                                dset.Tables[0].Rows[i]["TrxID"];
                            dgvMonthReceipts[colFoodClass, rowTrxDate].Style.BackColor = Color.White;
                        //}
                    //}
                }
            }
            loadTotals();
        }

        /// <summary>
        /// Returns the number of weeks for the selected period
        /// </summary>
        /// <returns>Int(number of weeks)</returns>
        protected int GetNumWeeksInPeriod()
        {
            int weeksInMonth = 0;
            CultureInfo ci = new CultureInfo("en-US");
            DateTime date = new DateTime(Convert.ToInt32(cboYear.SelectedItem), cboReportMonth.SelectedIndex + 1, 1);
            int daysInMonth = ci.Calendar.GetDaysInMonth(date.Year, date.Month);
            
            if (date.DayOfWeek != DayOfWeek.Monday)
                weeksInMonth++;

            for (int i = 1; i <= daysInMonth; i++)
            {
                if (date.DayOfWeek == DayOfWeek.Monday)
                {
                    weeksInMonth++;
                }
                date = date.AddDays(1);
            }
            return weeksInMonth;
        }

        /// <summary>
        /// Retrives and loads the donors into the Store Combo Box
        /// </summary>
        private void LoadcboDonor()
        {
            Donors clsDonors = new Donors(CCFBGlobal.connectionString);
            cboStore.Items.Clear();

            if (donorID > -1)
                clsDonors.openWhere(" Where ID=" + donorID.ToString());
            else
                clsDonors.openWhere(" Where DefaultDonationType=6");

            for (int i = 0; i < clsDonors.RowCount; i++)
            {
                clsDonors.setDataRow(i);
                parmType clsPT = new parmType(clsDonors.ID, clsDonors.Name, i, clsDonors.Name);
                listDonors.Add(clsPT);
            }
            
            try
            {
                clsDonors.setDataRow(0);
                cboStore.DataSource = listDonors;
                cboStore.DisplayMember = "LongName";
                cboStore.ValueMember = "UID";
                cboStore.SelectedIndex = 0;
                cboDonationType.SelectedValue = clsDonors.DefaultDonationType.ToString();
            }
            catch { }
            
            if (donorID > -1)
                cboStore.Enabled = false;
            clsDonors.Dispose();
        }

        /// <summary>
        /// Loads the Years into the Year Combo Box
        /// </summary>
        private void LoadcboYear()
        {
            cboYear.Items.Clear();
            cboYear.Items.Add(DateTime.Today.Year.ToString());
            clsFoodDonations.openDistinctDonationYears();
            string sYear = "";
            for (int i = 0; i < clsFoodDonations.RowCount; i++)
			{
                sYear = clsFoodDonations.DSet.Tables[0].Rows[i].Field<string>(0).ToString();  
                if (sYear != DateTime.Today.Year.ToString())
                    cboYear.Items.Add(sYear);  
			}
            cboYear.SelectedIndex = 0;
        }
        
        /// <summary>
        /// Loads the period data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLoadPeriodData_Click(object sender, EventArgs e)
        {
            loadingData = true;
            btnEmailRpt.Enabled = true;
            btnPrint.Enabled = true;
            ShowButtons(true);
            loadingData = false;
            initWeekTotals();
            initList();
            LoadListWithRowHeaders();

            getDonorData();
            loadByDayData();
            //getAndLoadByWeekData(); 
            dgvMonthReceipts.Enabled = true;
        }

        private void cbo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            btnPrint.Enabled = false;
            btnEmailRpt.Enabled = false;
            dgvMonthReceipts.Enabled = false;
            ShowButtons(false);
            initList();
        }

        /// <summary>
        /// Gets the Donations for the selected Donor in the given period
        /// </summary>
        private void getDonorData()
        {
            try
            {
                commandByDay = new SqlCommand("Set DateFirst 1; select * From FoodDonations WHERE DonorID = "
                    + cboStore.SelectedValue.ToString() + " AND TrxDate Between '"
                    + dateStart.ToShortDateString() + "' and '" + dateEnd.ToShortDateString() + "' "
                    + "order BY  TrxDate, FoodClass", conn);
                dset.Clear();
                dadAdpt.SelectCommand = commandByDay;
                idbRowCount = dadAdpt.Fill(dset);
            }
            catch (System.Exception ex)
            {
                idbRowCount = 0;
                CCFBGlobal.appendErrorToErrorReport("Donor=" + cboStore.SelectedValue.ToString()
                    + " dateStart=" + dateStart.ToShortDateString() + " dateEnd=" + dateEnd.ToShortDateString(),
                    ex.GetBaseException().ToString());
            }
        }

        /// <summary>
        /// Inserts the proper text into the Column Headers on the View By Day grid
        /// </summary>
        private void LoadListWithRowHeaders()
        {
            dateWeekStart = DateTime.MaxValue;
            dateWeekEnd = DateTime.MinValue;
            dateStart = Convert.ToDateTime((cboReportMonth.SelectedIndex + 1).ToString() + "/1/" + cboYear.Text);
            dateEnd = dateStart.AddMonths(1).AddDays(-1);
//            int displayWeek = 0;
            ////foreach (RadioButton rdo in grpWeek.Controls.OfType<RadioButton>())
            ////{
            ////    if (rdo.Checked == true)
            ////    {
            ////        displayWeek = Convert.ToInt32(rdo.Tag.ToString());
            ////        break;
            ////    }
            ////}

            //Get the first day of the given week

            dgvMonthReceipts.ColumnHeadersDefaultCellStyle = null;
            dgvMonthReceipts.Rows.Clear();
            int numberDaysInMonth = dateEnd.Day;

            //Goes through each day of the displayed week and displays the info for that day
            for (int i = 0; i < numberDaysInMonth; i++)
            {
                DateTime dateDisplay = dateStart.AddDays(i);
                dgvMonthReceipts.Rows.Add();
                dgvMonthReceipts.Rows[i].HeaderCell.Value = dateDisplay.ToShortDateString(); 
                //If the date is not within the Period or The User is not using Calendar Weeks
                //if (CCFBPrefs.UseCalendarWeeks == true && (dateDisplay < dateStart || dateDisplay > dateEnd)) 
                //{
                //    dgvMonthReceipts.Columns[j].HeaderText = dateDisplay.DayOfWeek.ToString().Substring(0, 3);
                //    dgvMonthReceipts.Columns[j].ReadOnly = true;
                //    dgvMonthReceipts.Columns[j].Tag = dateDisplay;

                for (int k = 0; k < dgvMonthReceipts.Columns.Count; k++)
                {
                    dgvMonthReceipts[k, i].Style.BackColor = Color.LightGray;
                }
                //}
                //else
                //{
                //   weekStartSet = setValidDate(weekStartSet, dateDisplay, j);
                //}
            }
            createTotalsRows();
        }

        private bool setValidDate(bool weekStartSet, DateTime dateDisplay, int col)
        {
            if (weekStartSet == false)
            {
                dateWeekStart = dateDisplay;
                weekStartSet = true;
            }
            dateWeekEnd = dateDisplay;
            dgvMonthReceipts.Columns[col].HeaderText = dateDisplay.DayOfWeek.ToString().Substring(0, 3) + " - "
                + CCFBGlobal.formatNumberWithLeadingZero(dateDisplay.Day);
            dgvMonthReceipts.Columns[col].ReadOnly = false;
            dgvMonthReceipts.Columns[col].Tag = dateDisplay;

            for (int k = 0; k < dgvMonthReceipts.Rows.Count; k++)
            {
                if (k != dgvMonthReceipts.Rows.Count - 1)
                    dgvMonthReceipts[col, k].Style.BackColor = Color.White;
                else
                    dgvMonthReceipts[col, k].Style.BackColor = Color.LightGoldenrodYellow;
            }

            return weekStartSet;
        }

        //private void rdoWeek_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (loadingData == false)
        //    {
        //        RadioButton rdo = (RadioButton)sender;
        //        if (rdo.Checked == true)
        //        {
        //            fillForm();
        //            getAndLoadByWeekData();
        //        }
        //    }
        //}

        /// <summary>
        /// Adds up each of the daily totals for each item and inserts that into the total column
        /// </summary>
        private void loadTotals()
        {
            zeroOutTotalsByDay();
            int rowTotal;
            for (int i = 0; i < dgvMonthReceipts.Rows.Count-1; i++)
            {
                rowTotal = 0;
                for (int j = 0; j < dgvMonthReceipts.Columns.Count-2; j++)
                {
                    if (dgvMonthReceipts[j, i].Value != null)
                    {
                        rowTotal += Convert.ToInt32(
                            dgvMonthReceipts[j, i].Value.ToString().Replace(",", ""));
                    }
                }
                if (rowTotal != 0)
                {
                    dgvMonthReceipts[dgvMonthReceipts.Columns.Count - 1, i].Value = CCFBGlobal.formatNumberWithCommas(rowTotal);
                }
            }

            int colTotal = 0;
            for (int i = 0; i < dgvMonthReceipts.Columns.Count; i++)
            {
                colTotal = 0;
                for (int j = 0; j < dgvMonthReceipts.RowCount-1; j++)
                {
                    if (dgvMonthReceipts[i, j].Value != null)
                    {
                        colTotal += Convert.ToInt32(
                             dgvMonthReceipts[i, j].Value.ToString().Replace(",", ""));
                    }
                }
                dgvMonthReceipts[i, dgvMonthReceipts.RowCount - 1].Value = CCFBGlobal.formatNumberWithCommas(colTotal);
            }
        }


        private void fillForm()
        {
            initList();
            LoadListWithRowHeaders();
            loadByDayData();
        }

        private void dgvMonthReceipts_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dbRowIndex = -1;
                int lbs;
                if (dgvMonthReceipts[e.ColumnIndex, e.RowIndex].Value != null)
                    lbs = Convert.ToInt32(dgvMonthReceipts[e.ColumnIndex, e.RowIndex].Value.ToString().Replace(",", ""));
                else
                    lbs = Convert.ToInt32(dgvMonthReceipts[e.ColumnIndex, e.RowIndex].Value);

                //Tag != null means Update
                if (dgvMonthReceipts[e.ColumnIndex, e.RowIndex].Tag != null)
                {
                    dbRowIndex = getRowIndexDependingOnTag(e.RowIndex, e.ColumnIndex);

                    if (lbs == 0)
                    {
                        dgvMonthReceipts[e.ColumnIndex, e.RowIndex].Tag = null;
                        dgvMonthReceipts[e.ColumnIndex, e.RowIndex].Value = null;
                        dset.Tables[0].Rows[dbRowIndex].Delete();
                    }
                    else
                    {
                        dgvMonthReceipts[e.ColumnIndex, e.RowIndex].Tag = dset.Tables[0].Rows[dbRowIndex]["TrxID"];
                        dset.Tables[0].Rows[dbRowIndex]["Pounds"] = lbs;
                        dset.Tables[0].Rows[dbRowIndex]["Modified"] = DateTime.Now;
                        dset.Tables[0].Rows[dbRowIndex]["ModifiedBy"] = CCFBGlobal.dbUserName;
                        dset.Tables[0].Rows[dbRowIndex]["DonationType"] = cboDonationType.SelectedValue;
                    }
                    update();
                    getDonorData();
                    loadByDayData();
                }
                else   //Tag == null means Insert
                {
                    if (lbs != 0)
                    {
                        DataRow drow = dset.Tables[0].NewRow();
                        drow["TrxDate"] = dgvMonthReceipts.Rows[e.RowIndex].HeaderCell.Value;  
                        drow["DonorID"] = cboStore.SelectedValue;
                        drow["Pounds"] = lbs;
                        drow["DonationType"] = cboDonationType.SelectedValue;
                        drow["FoodClass"] = getFoodClass(e.ColumnIndex);
                        drow["CreatedBy"] = CCFBGlobal.dbUserName;
                        drow["Created"] = DateTime.Now;
                        dset.Tables[0].Rows.Add(drow);

                        update();
                        getDonorData();
                        loadByDayData();
                        dgvMonthReceipts[e.ColumnIndex, e.RowIndex].Tag = "IN";
                        //initWeekTotals();
                        //getAndLoadByWeekData();
                    }
                    else
                        dgvMonthReceipts[e.ColumnIndex, e.RowIndex].Value = null;
                }
            }
            catch   //Value not in proper format to convert to an integer so it reloads original value
            {
                if (dgvMonthReceipts[e.ColumnIndex, e.RowIndex].Tag != null)
                {
                    dbRowIndex = getRowIndexDependingOnTag(e.RowIndex, e.ColumnIndex);
                    dgvMonthReceipts[e.ColumnIndex, e.RowIndex].Value = dset.Tables[0].Rows[dbRowIndex]["Pounds"];
                }
                else
                    dgvMonthReceipts[e.ColumnIndex, e.RowIndex].Value = null;
            }
        }

        /// <summary>
        /// Decides which overloaded findRowIndex to call 
        /// depending on the tag for the DataGridViewCell
        /// </summary>
        /// <param name="dgvRow"></param>
        /// <param name="dgvCol"></param>
        /// <returns></returns>
        private int getRowIndexDependingOnTag(int dgvRow, int dgvCol)
        {
            int row;
            if (dgvMonthReceipts[dgvCol, dgvRow].Tag.ToString() == "IN")
            {
                row = findRowIndex(((parmType)dgvMonthReceipts[0, dgvRow].Tag).ID,
                     (DateTime)dgvMonthReceipts.Columns[dgvCol].Tag);
            }
            else
            {
                row = findRowIndex(Convert.ToInt32(dgvMonthReceipts[dgvCol, dgvRow].Tag));
            }

            return row;
        }

        /// <summary>
        /// Initializes the totals for both Grids to Zero
        /// </summary>
        private void initWeekTotals()
        {
            //for (int i = 0; i < dgvMonthTotals.Rows.Count; i++)
            //{
            //    for (int j = 1; j < dgvMonthTotals.Columns.Count-1; j++)
            //    {
            //        dgvMonthTotals[j, i].Value = null;
            //    }
            //}
            //zeroOutTotalsByMonth();
        }
        /// <summary>
        /// Finds the RowIndex in the dataset by ID
        /// </summary>
        /// <param name="id">The TrxID</param>
        /// <returns>(int)RowIndex, If -1 the Donation was not found</returns>
        private int findRowIndex(int id)
        {
            for (int i = 0; i < idbRowCount; i++)
            {
                if (dset.Tables[0].Rows[i].Field<int>("TrxID") == id)
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// Finds the RowIndex in the dataset by FoodClass and Date
        /// </summary>
        /// <param name="foodClass">the integer representing the FoodClass</param>
        /// <param name="date">The transaction date</param>
        /// <returns>(int)RowIndex, If -1 the Donation was not found</returns>
        private int findRowIndex(int foodClass, DateTime date)
        {
            for (int i = 0; i < idbRowCount; i++)
            {
                if (dset.Tables[0].Rows[i].Field<DateTime>("TrxDate") == date
                    && Convert.ToInt32(dset.Tables[0].Rows[i]["FoodClass"]) == foodClass)
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// Commits all changes in the dataset to the Donations table
        /// </summary>
        private void update()
        {
            if (dset.HasChanges())
            {
                try
                {
                    if (dadAdpt.UpdateCommand == null || dadAdpt.InsertCommand == null || dadAdpt.DeleteCommand == null)
                    {
                        commBuilder = new SqlCommandBuilder(dadAdpt);
                    }

                    dadAdpt.Update(dset);
                }
                catch (SqlException ex)
                {
                    CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString());
                }
            }
        }

        /// <summary>
        /// Validates that the data entered by the user is in proper format
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvMonthReceipts_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.FormattedValue != null && e.FormattedValue.ToString().Trim().Length >0 
                && e.ColumnIndex > 1 && e.ColumnIndex < 8)
            {
                try
                {
                    Convert.ToInt32(e.FormattedValue.ToString().Replace(",", ""));
                    //dgvMonthReceipts[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.White;
                }
                catch 
                {
                    if (MessageBox.Show("ERROR With Value. What Would You Like To Do?",
                        "ERROR", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
                    == DialogResult.Retry)
                    {
                        e.Cancel = true;
                    }
                    else
                    {
                        if (dgvMonthReceipts[e.ColumnIndex, e.RowIndex].Tag == null)
                            dgvMonthReceipts[e.ColumnIndex, e.RowIndex].Value = null;
                        else
                        {
                            dgvMonthReceipts[e.ColumnIndex, e.RowIndex].Value =
                                dset.Tables[0].Rows[getRowIndexDependingOnTag(e.RowIndex, e.ColumnIndex)]["Pounds"];
                        }
                    }
                }
            }
            //else if ((e.FormattedValue == null || e.FormattedValue.ToString().Trim())  == true) &&
            //            dgvMonthReceipts.Columns[e.ColumnIndex].ReadOnly == false && e.RowIndex < dgvMonthReceipts.RowCount-1)
            //{
            //    dgvMonthReceipts[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.White;
            //}
            //else if(e.RowIndex == dgvMonthReceipts.RowCount-1)
            //    dgvMonthReceipts[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.LightGoldenrodYellow;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            dgvMonthReceipts.EndEdit();
            this.Close();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            //btnEdit.BackColor = Color.LightSkyBlue;
            //btnSummary.BackColor = Color.Gainsboro;
            //pnlMonthly.Visible = false;
            pnlEditDaily.Visible = true;
            cboDonationType.Enabled = true;
        }

        private void btnSummary_Click(object sender, EventArgs e)
        {
            //btnEdit.BackColor = Color.Gainsboro;
            //btnSummary.BackColor = Color.LightSkyBlue;
            //pnlMonthly.Visible = true;
            pnlEditDaily.Visible = false;
            //pnlViewable = pnlMonthly;
            cboDonationType.Enabled = false;
        }
        private void ShowButtons(bool isVisible)
        {
            //btnEdit.Visible = isVisible;
            //btnSummary.Visible = isVisible;
        }

        private void cboDonationType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (dgvMonthReceipts.SelectedCells.Count > 0)
            {
                for (int i = 0; i < dgvMonthReceipts.SelectedCells.Count; i++)
                {
                    int cellRowIndex;
                    int cellClmIndex;
                    if (dgvMonthReceipts.SelectedCells[i].ColumnIndex > 0
                    && dgvMonthReceipts.SelectedCells[i].ColumnIndex < 8)
                    {
                        cellRowIndex = dgvMonthReceipts.SelectedCells[i].RowIndex;
                        cellClmIndex = dgvMonthReceipts.SelectedCells[i].ColumnIndex;
                        if (dgvMonthReceipts[cellClmIndex, cellRowIndex].Tag != null)
                        {
                            int dsetRowIndex = getRowIndexDependingOnTag(cellRowIndex, cellClmIndex);
                            dset.Tables[0].Rows[dsetRowIndex]["Modified"] = DateTime.Now;
                            dset.Tables[0].Rows[dsetRowIndex]["ModifiedBy"] = CCFBGlobal.dbUserName;
                            dset.Tables[0].Rows[dsetRowIndex]["DonationType"] = cboDonationType.SelectedValue;
                        }
                    }

                }
                update();
            }
        }

        private void dgvMonthReceipts_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (idbRowCount > 0 && dgvMonthReceipts[e.ColumnIndex, e.RowIndex].Tag != null && dgvMonthReceipts[e.ColumnIndex, e.RowIndex].Tag != "IN"
                && e.ColumnIndex < dgvMonthReceipts.Columns.Count-2)
            {
                int dsetRowIndex = getRowIndexDependingOnTag(e.RowIndex, e.ColumnIndex);
                cboDonationType.SelectedValue = dset.Tables[0].Rows[dsetRowIndex]["DonationType"].ToString();
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            RptFreshAlliance clsCreateGRReport;
            string saveAs = "";
            clsCreateGRReport = new RptFreshAlliance(dgvMonthReceipts);
                saveAs = CCFBPrefs.ReportsSavePath + "FreshAlliance_" + cboReportMonth.SelectedItem
               + "_" + cboYear.SelectedItem + "_" + ((parmType)cboStore.SelectedItem).LongName + "_MontlyTotals.xls";

            clsCreateGRReport.createReport(saveAs, CCFBGlobal.fb3TemplatesPath + "FreshAlliance.xls",
                ((parmType)cboStore.SelectedItem).LongName, cboReportMonth.SelectedItem.ToString() + " " + cboYear.SelectedItem.ToString());
        }

        private void btnEmailRpt_Click(object sender, EventArgs e)
        {
            string fileName = "";

            //if (pnlEditDaily.Visible == true)
            //{
            //    fileName = "GroceryRescue_" + cboReportMonth.SelectedItem
            //   + "_" + cboYear.SelectedItem + "_" + ((parmType)cboStore.SelectedItem).LongName + "_";

            //    foreach (RadioButton rdo in grpWeek.Controls.OfType<RadioButton>())
            //    {
            //        if (rdo.Checked == true)
            //            fileName += rdo.Name.Replace("rdo", "");
            //    }

            //    fileName += ".xls";
            //}
            //else
            //{
                fileName = "FreshAlliance_" + cboReportMonth.SelectedItem
               + "_" + cboYear.SelectedItem + "_" + ((parmType)cboStore.SelectedItem).LongName + "_FreshAlliance.xls";
            //}


            string filePath = CCFBPrefs.ReportsSavePath + fileName;

            if (File.Exists(filePath) == true)
            {
                createAndSendEmail(filePath, fileName, clsMonthlyReports.EmailAddresses.Replace('|', ';'));
            }
            else
            {
                MessageBox.Show("No Grocery Rescue Report For This Period Exists. "
                + "Please Create Report And Try Again");
            }
        }

        private void createAndSendEmail(string filePath, string fileName, string emailList)
        {
            try
            {
                Microsoft.Office.Interop.Outlook.Application oApp;
                Microsoft.Office.Interop.Outlook._NameSpace oNameSpace;
                Microsoft.Office.Interop.Outlook.MAPIFolder oOutboxFolder;
                oApp = new Microsoft.Office.Interop.Outlook.Application();
                oNameSpace = oApp.GetNamespace("MAPI");

                oNameSpace.Logon("", "", true, true);
                oOutboxFolder = oNameSpace.GetDefaultFolder(
                    Microsoft.Office.Interop.Outlook.OlDefaultFolders.olFolderOutbox);

                Microsoft.Office.Interop.Outlook._MailItem oMailItem =
                    (Microsoft.Office.Interop.Outlook._MailItem)oApp.CreateItem(
                    Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);

                oMailItem.To = emailList;
                oMailItem.Subject = "Grocery Rescue Report From " + CCFBPrefs.FoodBankName;
                oMailItem.Body = "TEST THIS";
                oMailItem.SaveSentMessageFolder = oOutboxFolder;
                String sSource = filePath;
                String sDisplayName = fileName;
                int iPosition = (int)oMailItem.Body.Length + 1;
                int iAttachType = (int)OlAttachmentType.olByValue;
                oMailItem.Attachments.Add(sSource, iAttachType, iPosition, sDisplayName);

                //uncomment this to also save this in your draft 
                //oMailItem.Save(); 
                //adds it to the outbox 
                oMailItem.Send();
                oMailItem = null;
                oNameSpace = null;
                oApp = null;
            }
            catch (System.Exception ex)
            {
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString());
            }
        }
        private int getFoodClass(int colIndex)
        {
            foreach (parmType item in CCFBGlobal.TypeCodesArray(CCFBGlobal.parmTbl_FoodClass))
            {
                if (item.SortOrder == colIndex)
                {
                    return item.ID;
                }

            }
            return 9;
        }
    }
}
