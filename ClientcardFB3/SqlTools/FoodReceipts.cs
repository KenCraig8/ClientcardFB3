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

namespace ClientCardFB3
{
    public partial class frmFoodReceipts : Form
    {
        private System.Collections.ArrayList listDonors = new System.Collections.ArrayList();
        FoodDonations clsFoodDonations = new FoodDonations(CCFBGlobal.connectionString);
        DateTime dateStart;
        DateTime dateEnd;
        DateTime dateWeekStart;
        DateTime dateWeekEnd;
        System.Collections.ArrayList foodClassList;

        DataSet dset;
        SqlConnection conn;
        SqlDataAdapter dadAdpt;
        SqlCommand commandByDay;
        SqlCommand commandByWeek;

        int iRowCount = 0;
        int rowIndex = -1;
        public frmFoodReceipts()
        {
            InitializeComponent();

            dset = new DataSet();
            conn = new SqlConnection(CCFBGlobal.connectionString);
            dadAdpt = new SqlDataAdapter();

            LoadcboDonor();
            LoadcboYear();
            foodClassList = CCFBGlobal.TypeCodesArray(CCFBGlobal.parmTbl_FoodClass);
            fillFoodClasses();
            cboReportMonth.SelectedIndex = DateTime.Today.Month - 1;
        }

        private void fillFoodClasses()
        {
            dgvMonthReceipts.Rows.Clear();
            dgvMonthTotals.Rows.Clear();
            foreach (parmType pt in foodClassList)
            {
                dgvMonthTotals.Rows.Add();
                dgvMonthReceipts.Rows.Add();
                dgvMonthReceipts[0, dgvMonthReceipts.Rows.Count - 1].Tag = pt;
                dgvMonthReceipts[0, dgvMonthReceipts.Rows.Count - 1].Value = pt.LongName;
                dgvMonthTotals[0, dgvMonthTotals.Rows.Count - 1].Tag = pt;
                dgvMonthTotals[0, dgvMonthTotals.Rows.Count - 1].Value = pt.LongName;
            }
        }

        public static int GetWeekNumber(DateTime dtPassed)
        {
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            return ciCurr.Calendar.GetWeekOfYear(dtPassed, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }

        private void loadPeriodTotals()
        {
            commandByWeek = new SqlCommand("Set DateFirst 1; Select DatePart(WK,TrxDate) WeekOfYr, [FoodClass], "
            + "SUM(Pounds) From FoodDonations WHERE DonorID = " + cboStore.SelectedValue.ToString() + " AND TrxDate Between '"
            + dateStart.ToString() + "' and '" + dateEnd.ToString() + "' "
            + "group by DatePart(WK,TrxDate),[FoodClass] "
            + "order BY  DatePart(WK,TrxDate),[FoodClass]", conn);

            int weekOfYear = GetWeekNumber(dateStart);

            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

               SqlDataReader reader = commandByWeek.ExecuteReader();

              while (reader.Read())
               {
                  dgvMonthTotals[Convert.ToInt32(reader.GetValue(0))-weekOfYear + 1, 
                      Convert.ToInt32(reader.GetValue(1))].Value = 
                      CCFBGlobal.formatNumberWithCommas(reader.GetValue(2));
               }
            }
            catch(SqlException ex)
            {
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString(), CCFBGlobal.serverName);
            }

            if(conn.State == ConnectionState.Open)
                conn.Close();
        }

        private void initList()
        {
            for (int i = 0; i < dgvMonthReceipts.Rows.Count; i++)
            {
                for (int j = 1; j < dgvMonthReceipts.Columns.Count - 1; j++)
                {
                    dgvMonthReceipts[j, i].Value = null;
                    dgvMonthReceipts[j, i].Tag = null;
                    dgvMonthReceipts[j, i].Style.BackColor = Color.White;
                }
            }
        }

        private void zeroOutWeeklyTotals()
        {
            for (int i = 0; i < dgvMonthReceipts.Rows.Count; i++)
            {
                dgvMonthReceipts[8, i].Value = 0;
                
            }
        }

        private void zeroOutMonthlyTotals()
        {
            for (int i = 0; i < dgvMonthTotals.Rows.Count; i++)
            {
                dgvMonthTotals[7, i].Value = 0;

            }
        }

        private void loadListWithDonationData()
        {
            DateTime trxDate;
            for(int i = 0; i < iRowCount; i++)
            {
                trxDate = dset.Tables[0].Rows[i].Field<DateTime>("TrxDate");
                int dow = Convert.ToInt32(trxDate.DayOfWeek);
                if (dow == 0)
                    dow = 7;

                if(trxDate >= dateWeekStart && trxDate <= dateWeekEnd)
                {
                    for (int j = 0; j < dgvMonthReceipts.Rows.Count; j++)
                    {
                        if (((parmType)dgvMonthReceipts["colFoodClass", j].Tag).UID
                            == dset.Tables[0].Rows[i]["FoodClass"].ToString())
                        {
                            dgvMonthReceipts[Convert.ToInt32(dow), j].Value =
                               CCFBGlobal.formatNumberWithCommas(dset.Tables[0].Rows[i]["Pounds"]);
                            dgvMonthReceipts[Convert.ToInt32(dow), j].Tag =
                                dset.Tables[0].Rows[i]["TrxID"];
                        }
                    }
                }
            }
            getTotalsForWeek();
        }

        protected int GetNumWeeksInPeriod()
        {
            int weeksInMonth = 0;
            CultureInfo ci = new CultureInfo("en-US");
            DateTime date = new DateTime(Convert.ToInt32(cboYear.SelectedItem), cboReportMonth.SelectedIndex + 1, 1);
            
            if (date.DayOfWeek != DayOfWeek.Monday)
                weeksInMonth++;

            for (int i = 1; i <= ci.Calendar.GetDaysInMonth(date.Year, date.Month); i++)
            {
                if (date.DayOfWeek == DayOfWeek.Monday)
                {
                    weeksInMonth++;
                }
                date = date.AddDays(1);
            }
            return weeksInMonth;
        }

        private void LoadcboDonor()
        {
            Donors clsDonors = new Donors(CCFBGlobal.connectionString);
            cboStore.Items.Clear();
            clsDonors.openWhere("DefaultDonationType = 6");
            for (int i = 0; i < clsDonors.RowCount ; i++)
            {
                clsDonors.setDataRow(i);
                parmType clsPT = new parmType(clsDonors.ID, clsDonors.Name, i, clsDonors.Name);
                listDonors.Add(clsPT);
            }
            cboStore.DataSource = listDonors;
            cboStore.DisplayMember = "LongName";
            cboStore.ValueMember = "UID";
            cboStore.SelectedIndex = 0;
        }

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
        
        private void btnLoadPeriodData_Click(object sender, EventArgs e)
        {
            tabControl1.Visible = true;
            initWeekTotals();
            initList();
            LoadListWithCollumHeaders();
            getDonorData();
            loadListWithDonationData();
            loadPeriodTotals(); 
            getTotalsForMonth();
            dgvMonthReceipts.Enabled = true;

            switch (GetNumWeeksInPeriod())
            {
                case 4:
                    {
                        rdoWeek5.Visible = false;
                        rdoWeek6.Visible = false;
                        dgvMonthTotals.Columns["clmWeek5"].Visible = false;
                        dgvMonthTotals.Columns["clmWeek6"].Visible = false;
                        break;
                    }
                case 5:
                    {
                        rdoWeek5.Visible = true;
                        rdoWeek6.Visible = false;
                        dgvMonthTotals.Columns["clmWeek5"].Visible = true;
                        dgvMonthTotals.Columns["clmWeek6"].Visible = false;
                        break;
                    }
                case 6:
                    {
                        rdoWeek5.Visible = true;
                        rdoWeek6.Visible = true;
                        dgvMonthTotals.Columns["clmWeek5"].Visible = true;
                        dgvMonthTotals.Columns["clmWeek6"].Visible = true;
                        break;
                    }
                default:
                    {
                        rdoWeek5.Visible = false;
                        rdoWeek6.Visible = false;
                        dgvMonthTotals.Columns["clmWeek5"].Visible = false;
                        dgvMonthTotals.Columns["clmWeek6"].Visible = false;
                        break;
                    }
            }
        }

        private void cbo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            dgvMonthReceipts.Enabled = false;
            tabControl1.Visible = false;
            initList();
        }

        private void getDonorData()
        {
            try
            {
                commandByDay = new SqlCommand("Set DateFirst 1; select * From FoodDonations WHERE DonorID = "
                    + cboStore.SelectedValue.ToString() + " AND TrxDate Between '"
                    + dateStart.ToString() + "' and '" + dateEnd.ToString() + "' "
                    + "order BY  FoodClass, TrxDate", conn);
                dset.Clear();
                dadAdpt.SelectCommand = commandByDay;
                iRowCount = dadAdpt.Fill(dset);
            }
            catch (SqlException ex)
            {
                iRowCount = 0;
                CCFBGlobal.appendErrorToErrorReport("Donor=" + cboStore.SelectedValue.ToString()
                    + " dateStart=" + dateStart.ToShortDateString() + " dateEnd=" + dateEnd.ToShortDateString(),
                    ex.GetBaseException().ToString(), CCFBGlobal.serverName);
            }

        }

        private void LoadListWithCollumHeaders()
        {
            bool weekStartSet = false;
            dateWeekStart = DateTime.MaxValue;
            dateWeekEnd = DateTime.MinValue;
            dateStart = Convert.ToDateTime((cboReportMonth.SelectedIndex + 1).ToString() + "/1/" + cboYear.Text);
            int dowStart = Convert.ToInt32(dateStart.DayOfWeek);
            dateEnd = dateStart.AddMonths(1).AddDays(-1);
            int displayWeek = 0;
            foreach (RadioButton rdo in grpWeek.Controls.OfType<RadioButton>())
            {
                if (rdo.Checked == true)
                {
                    displayWeek = Convert.ToInt32(rdo.Tag.ToString());
                    break;
                }
            }
            DateTime firstDateofDisplay = dateStart.AddDays(1 - dowStart);
            dgvMonthReceipts.ColumnHeadersDefaultCellStyle = null;
            for (int i = 0; i < 7; i++)
            {
                DateTime dateDisplay = firstDateofDisplay.AddDays(i + (displayWeek * 7));
                int j = i + 1;

                if (dateDisplay < dateStart || dateDisplay > dateEnd)
                {
                    dgvMonthReceipts.Columns[j].HeaderText = dateDisplay.DayOfWeek.ToString() + " \r\n ....";
                    dgvMonthReceipts.Columns[j].ReadOnly = true;
                    dgvMonthReceipts.Columns[j].Tag = dateDisplay;

                    for (int k = 0; k < dgvMonthReceipts.Rows.Count; k++)
                        dgvMonthReceipts[j, k].Style.BackColor = Color.LightGoldenrodYellow;
                }
                else
                {
                    if (weekStartSet == false)
                    {
                        dateWeekStart = dateDisplay;
                        weekStartSet = true;
                    }

                    dateWeekEnd = dateDisplay;

                    dgvMonthReceipts.Columns[j].HeaderText = dateDisplay.DayOfWeek.ToString() + " \r\n" + dateDisplay.ToShortDateString();
                    dgvMonthReceipts.Columns[j].ReadOnly = false;
                    dgvMonthReceipts.Columns[j].Tag = dateDisplay;
                    
                    for (int k = 0; k < dgvMonthReceipts.Rows.Count; k++)
                        dgvMonthReceipts[j, k].Style.BackColor = Color.White;
                }
            }
        }

        private void rdoWeek_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdo = (RadioButton)sender;
            if (rdo.Checked == true)
            {
                fillForm();
                loadPeriodTotals();
            }
        }

        /// <summary>
        /// Adds up each of the daily totals for each item and inserts that into the total column
        /// </summary>
        private void getTotalsForWeek()
        {
            zeroOutWeeklyTotals();
            int rowTotal;
            for (int i = 0; i < dgvMonthReceipts.Rows.Count; i++)
            {
                rowTotal = 0;
                for (int j = 1; j < dgvMonthReceipts.Columns.Count-1; j++)
                {
                    if (dgvMonthReceipts[j, i].Value != null)
                    {
                        rowTotal += Convert.ToInt32(
                            dgvMonthReceipts[j, i].Value.ToString().Replace(",", ""));
                    }
                }
                dgvMonthReceipts[8, i].Value = CCFBGlobal.formatNumberWithCommas(rowTotal);
            }
        }

        /// <summary>
        /// Adds up each of the Weekly totals for each item and inserts that into the total column
        /// </summary>
        private void getTotalsForMonth()
        {
            zeroOutMonthlyTotals();
            int rowTotal;
            for (int i = 0; i < dgvMonthTotals.Rows.Count; i++)
            {
                rowTotal = 0;
                for (int j = 1; j < dgvMonthTotals.Columns.Count - 1; j++)
                {
                    if (dgvMonthTotals[j, i].Value != null)
                    {
                        rowTotal += Convert.ToInt32(
                            dgvMonthTotals[j, i].Value.ToString().Replace(",", ""));
                    }
                }
                dgvMonthTotals[7, i].Value = CCFBGlobal.formatNumberWithCommas(rowTotal);
            }
        }

        private void fillForm()
        {
            initList();
            LoadListWithCollumHeaders();
            loadListWithDonationData();
        }

        private void dgvMonthReceipts_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                rowIndex = -1;
                int lbs;
                if (dgvMonthReceipts[e.ColumnIndex, e.RowIndex].Value != null)
                    lbs = Convert.ToInt32(dgvMonthReceipts[e.ColumnIndex, e.RowIndex].Value.ToString().Replace(",", ""));
                else
                    lbs = Convert.ToInt32(dgvMonthReceipts[e.ColumnIndex, e.RowIndex].Value);

                if (dgvMonthReceipts[e.ColumnIndex, e.RowIndex].Tag != null)
                {
                    if (dgvMonthReceipts[e.ColumnIndex, e.RowIndex].Tag.ToString() == "IN")
                    {
                        rowIndex = findRowIndex(((parmType)dgvMonthReceipts.Rows[e.RowIndex].Cells["colFoodClass"].Tag).ID,
                            (DateTime)dgvMonthReceipts.Columns[e.ColumnIndex].Tag);
                    }
                    else
                        rowIndex = findRowIndex(Convert.ToInt32(dgvMonthReceipts[e.ColumnIndex, e.RowIndex].Tag));

                    if (lbs == 0)
                    {
                        dgvMonthReceipts[e.ColumnIndex, e.RowIndex].Tag = null;
                        dgvMonthReceipts[e.ColumnIndex, e.RowIndex].Value = null;
                        dset.Tables[0].Rows[rowIndex].Delete();
                    }
                    else
                    {
                        dgvMonthReceipts[e.ColumnIndex, e.RowIndex].Tag = dset.Tables[0].Rows[rowIndex]["TrxID"];
                        dset.Tables[0].Rows[rowIndex]["Pounds"] = lbs;
                        dset.Tables[0].Rows[rowIndex]["Modified"] = DateTime.Now;
                        dset.Tables[0].Rows[rowIndex]["ModifiedBy"] = CCFBGlobal.currentUser_Name;
                    }
                    update();
                    getDonorData();
                    getTotalsForWeek();
                    loadPeriodTotals();
                    getTotalsForMonth();
                }
                else
                {
                    if (lbs != 0)
                    {
                        DataRow drow = dset.Tables[0].NewRow();
                        drow["TrxDate"] = dgvMonthReceipts.Columns[e.ColumnIndex].Tag;
                        drow["DonorID"] = cboStore.SelectedValue;
                        drow["Pounds"] = lbs;
                        drow["DonationType"] = 6;
                        drow["FoodClass"] = ((parmType)dgvMonthReceipts[0, e.RowIndex].Tag).ID;
                        drow["CreatedBy"] = CCFBGlobal.currentUser_Name;
                        drow["Created"] = DateTime.Now;
                        dset.Tables[0].Rows.Add(drow);

                        update();
                        getDonorData();
                        dgvMonthReceipts[e.ColumnIndex, e.RowIndex].Tag = "IN";
                        getTotalsForWeek();
                        initWeekTotals();
                        loadPeriodTotals();
                        getTotalsForMonth();
                    }
                    else
                        dgvMonthReceipts[e.ColumnIndex, e.RowIndex].Value = null;
                }
            }
            catch
            {
                if (dgvMonthReceipts[e.ColumnIndex, e.RowIndex].Tag != null)
                {
                    rowIndex = getRowIndexDependingOnTag(e.RowIndex, e.ColumnIndex);
                    dgvMonthReceipts[e.ColumnIndex, e.RowIndex].Value = dset.Tables[0].Rows[rowIndex]["Pounds"];
                }
                else
                    dgvMonthReceipts[e.ColumnIndex, e.RowIndex].Value = null;
            }
        }

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

        private void initWeekTotals()
        {
            for (int i = 0; i < dgvMonthTotals.Rows.Count; i++)
            {
                for (int j = 1; j < dgvMonthTotals.Columns.Count-1; j++)
                {
                    dgvMonthTotals[j, i].Value = null;
                }
            }

            zeroOutMonthlyTotals();
        }

        private int findRowIndex(int id)
        {
            for (int i = 0; i < iRowCount; i++)
            {
                if (dset.Tables[0].Rows[i].Field<int>("TrxID") == id)
                {
                    return i;
                }
            }
            return -1;
        }

        private int findRowIndex(int foodClass, DateTime date)
        {
            for (int i = 0; i < iRowCount; i++)
            {
                if (dset.Tables[0].Rows[i].Field<DateTime>("TrxDate") == date
                    && Convert.ToInt32(dset.Tables[0].Rows[i]["FoodClass"]) == foodClass)
                {
                    return i;
                }
            }
            return -1;
        }

        private void update()
        {
            if (dset.HasChanges())
            {
                try
                {
                    if (dadAdpt.UpdateCommand == null || dadAdpt.InsertCommand == null || dadAdpt.DeleteCommand == null)
                    {
                        SqlCommandBuilder commBuilder = new SqlCommandBuilder(dadAdpt);
                    }

                    dadAdpt.Update(dset);
                }
                catch (SqlException ex)
                {
                    CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString(),
                        CCFBGlobal.serverName);
                }
            }
        }

        private void dgvMonthReceipts_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.FormattedValue != null && e.FormattedValue.ToString().Trim() != "" 
                && e.ColumnIndex > 1 && e.ColumnIndex < 8)
            {
                try
                {
                    Convert.ToInt32(e.FormattedValue.ToString().Replace(",", ""));
                    dgvMonthReceipts[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.White;
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
            else if ((e.FormattedValue == null || e.FormattedValue.ToString().Trim() == "") &&
                        dgvMonthReceipts.Columns[e.ColumnIndex].ReadOnly == false)
            {
                dgvMonthReceipts[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.White;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            dgvMonthReceipts.EndEdit();
            this.Close();
        }
    }
}
