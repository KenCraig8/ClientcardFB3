using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Microsoft.Office.Interop.Outlook;
using Ionic.Zip;

namespace ClientcardFB3
{
    public partial class MonthEndReportsForm : Form
    {
        MainForm frmMain;
        MonthlyReports clsMonthlyReports = new MonthlyReports(CCFBGlobal.connectionString);
        SqlDataAdapter dadAdpt;
        SqlCommand sqlCmd; 
        DataSet dset;
        //SqlCommand command;
        SqlConnection conn;
        string tbName = "TrxLog";
        string file = "";
        string city = "";
        string savePath = CCFBPrefs.ReportsSavePath;
        string sFiscalPeriod;
        string sFiscalYear;
        string percentNWH = "";
        string lbsEFAP = "";
        string lbsTEFAP = "";
        string lbs2ndHarvest = "";
        string lbsNWH = "";
        int rowCount = 0;
        string nameLVCtrl = "";
        // Create the Outlook application by using inline initialization.
        //Microsoft.Office.Interop.Outlook.Application oApp;
        //Households Monthly
        List<TextBox> tbHHCurMonthList;
        List<TextBox> tbHHCurMonthBPList;
        List<TextBox> tbHHCurMonthHDList;
        List<Label>   lblBPList;
        List<Label>   lblHDList;
        List<TextBox> tbHHPvYrMonthList;
        List<TextBox> tbHHPvYrMonthBPList;
        List<TextBox> tbHHPvYrMonthHDList;
        List<TextBox> tbHHPvY2MonthList;
        List<TextBox> tbHHDifCurMonthList;
        List<TextBox> tbHHDifCurMonthBPList;
        List<TextBox> tbHHDifCurMonthHDList;
        //Households Year To Date
        List<TextBox> tbHHCurYTDList;
        List<TextBox> tbHHPvYrYTDList;
        List<TextBox> tbHHPvY2YTDList;
        List<TextBox> tbHHDifYTDList;
        //Vol Hours Month
        List<TextBox> tbVolMonthList;
        List<TextBox> tbVolYTDList;

        List<TextBox> tbLbsRcvdPerList;
        List<TextBox> tbLbsRcvdCumList;

        List<TextBox> tbLbsSvdCurMonthList;
        List<TextBox> tbLbsSvdPvYrMonthList;
        List<TextBox> tbLbsSvdPvY2MonthList;

        List<TextBox> tbLbsSvdCurYTDList;
        List<TextBox> tbLbsSvdPvYrYTDList;
        List<TextBox> tbLbsSvdPvY2YTDList;

        TrxLogPeriodTotals clsTrxLogCurFiscalYrStats;
        TrxLogPeriodTotals clsTrxLogPrvFiscalYrStats;
        TrxLogPeriodTotals clsTrxLogPrvFiscalY2Stats;
        TrxLogPeriodTotals clsTrxLogCurFiscalYrHDStats;
        TrxLogPeriodTotals clsTrxLogPrvFiscalYrHDStats;
        VolunteerStats clsVolStats;

        DataTable dtblFamilySizeByDate;
        DataTable dtblFamilySizeByMonth;
        DataTable dtblFoodRecieptsByFunding;
        DataTable dtblFoodRecieptsByDonor;
        DataTable dtblYTDFoodRecieptsByDonor;
        DataTable dtblTrxLogDataByDay;
        DataTable dtblTrxLogDataByMonth;
        DataTable dtblRptMonths;
        DataTable dtblCurNbrServiceDays;
        DataTable dtblCurInkindLbs;
        DataTable dtblPrevNbrServiceDays;
        DataTable dtblPrevInkindLbs;

        DateTime dateFiscalStart; 
        DateTime datePeriodLast;
        DateTime datePeriodFirst;

        string sqlCommandText = "Select Distinct dbo.GetFiscalYear( left(Convert(char(8),TrxDate,112),6)) as 'Year' "
            + "From TrxLog where TrxDate is not null Order By 'Year' DESC";

        ListViewItem lvItmGenYTD;

        public MonthEndReportsForm(MainForm mainFormIn)
        {
            InitializeComponent();

            frmMain = mainFormIn;
            chkMergeTeens.Checked = CCFBPrefs.MergeTeens;
            tbHHCurMonthList = new List<TextBox>();
            tbHHCurMonthBPList = new List<TextBox>();
            tbHHCurMonthHDList = new List<TextBox>();
            lblBPList = new List<Label>();
            lblHDList = new List<Label>();
            tbHHCurYTDList = new List<TextBox>();
            tbHHPvYrMonthList = new List<TextBox>();
            tbHHPvYrMonthBPList = new List<TextBox>();
            tbHHPvYrMonthHDList = new List<TextBox>();
            tbHHPvYrYTDList = new List<TextBox>();
            tbHHPvY2MonthList = new List<TextBox>();
            tbHHPvY2YTDList = new List<TextBox>();
            
            tbHHDifCurMonthList = new List<TextBox>();
            tbHHDifCurMonthBPList = new List<TextBox>();
            tbHHDifCurMonthHDList = new List<TextBox>();
            tbHHDifYTDList = new List<TextBox>();

            tbVolMonthList = new List<TextBox>();
            tbVolYTDList = new List<TextBox>();

            
/*            tbIndCurMonthList = new List<TextBox>();
            tbIndCurYTDList = new List<TextBox>();
            tbIndPvYrMonthList = new List<TextBox>();
            tbIndPvYrYTDList = new List<TextBox>();
            tbIndPvY2MonthList = new List<TextBox>();
            tbIndPvY2YTDList = new List<TextBox>();
*/
            tbLbsRcvdPerList = new List<TextBox>();
            tbLbsRcvdCumList = new List<TextBox>();

            tbLbsSvdCurMonthList = new List<TextBox>();
            tbLbsSvdPvYrMonthList = new List<TextBox>();
            tbLbsSvdPvY2MonthList = new List<TextBox>();

            tbLbsSvdCurYTDList = new List<TextBox>();
            tbLbsSvdPvYrYTDList = new List<TextBox>();
            tbLbsSvdPvY2YTDList = new List<TextBox>(); 

            traverseAndAddControlsToCollections(this.Controls);
            clsMonthlyReports.openWhere(" Where GroupingBy=0");
            pnlLoadStatus.Visible = false; 
            dadAdpt = new SqlDataAdapter();
            dset = new DataSet();
            conn = new SqlConnection(CCFBGlobal.connectionString);
            SqlCommand selectComm = new SqlCommand(sqlCommandText, conn);
            dadAdpt.SelectCommand = selectComm;
            getDistinctYears();
            fillYearsCombo();
            fillFoodBankData();
            loadReports();
            CCFBGlobal.verifyPath(savePath);
            lvwGenCur.Columns[5].Text = "@ " + CCFBPrefs.InkindDollarsPerHr.ToString("C") + " /hr";
            lvwGenCur.Columns[6].Text = "@ " + CCFBPrefs.InkindDollarsPerLb.ToString("C") + " /lb";
            lvwGenPrev.Columns[5].Text = lvwGenCur.Columns[5].Text;
            lvwGenPrev.Columns[6].Text = lvwGenCur.Columns[6].Text;
            initForm();
            setChildTeensVisible(chkMergeTeens.Checked);
            tabPage1.BackColor = Color.LightGray;
            tabPage2.BackColor = Color.LightGray;
            lvReports.Enabled = false;
            btnCreateReports.Enabled = false;
            btnDisplayExisting.Enabled = false;
            btnEmailReports.Enabled = false;
            lblBPList.Add(lblBPStd);
            lblBPList.Add(lblBPTEFAP);
            lblBPList.Add(lblBPTotal);
            lblHDList.Add(lblHDStd);
            lblHDList.Add(lblHDTEFAP);
            lblHDList.Add(lblHDTotal);
            if (CCFBPrefs.EnableBackPack == false)
            {
                disableTB(tbHHCurMonthBPList);
                disableTB(tbHHPvYrMonthBPList);
                disableTB(tbHHDifCurMonthBPList);
                disableLbl(lblBPList);
            }
            if (CCFBPrefs.EnableHomeDeliv == false)
            {
                disableTB(tbHHCurMonthHDList);
                disableTB(tbHHPvYrMonthHDList);
                disableTB(tbHHDifCurMonthHDList);
                disableLbl(lblHDList);
            }
        }

        private void loadReports()
        {
            lvReports.Items.Clear();
            ListViewItem lvi = new ListViewItem("All");
            lvReports.Items.Add(lvi);
            for (int i = 0; i < clsMonthlyReports.RowCount; i++)
            {
                clsMonthlyReports.setDataRow(i);
                if (clsMonthlyReports.ReportActive == true)
                {
                    lvi = new ListViewItem(clsMonthlyReports.ReportName);
                    lvi.Tag = clsMonthlyReports.ID;
                    lvReports.Items.Add(lvi);
                }
            }
        }

        /// <summary>
        /// Checks the Report Path and finds if the reports exist for the current period
        /// </summary>
        private void findIfReportsExist()
        {
            for (int i = 1; i < lvReports.Items.Count; i++)
            {
                if (lvReports.Items[i].Tag != null && lvReports.Items[i].Tag.ToString() != "")
                {
                    //Get Report Path
                    file = makeReportPath() + makeReportPrefix();
                    clsMonthlyReports.find(Convert.ToInt32(lvReports.Items[i].Tag));
                    //Add report name
                    file += clsMonthlyReports.ReportName + clsMonthlyReports.DocType;

                    //Check if file exists
                    if (File.Exists(file) == true)
                    {
                        lvReports.Items[i].BackColor = Color.LightSteelBlue;
                        lvReports.Items[i].Checked = false;
                    }
                    else if (lvReports.Items[i].Checked == true)
                    {
                        lvReports.Items[i].BackColor = Color.IndianRed;
                    }
                    else
                    {
                        lvReports.Items[i].BackColor = Color.Beige;
                    }
                }
            }
        }

        private void fillFoodBankData()
        {
            tbFBName.Text = CCFBPrefs.FoodBankName;
            tbCounty.Text = CCFBPrefs.County;
            tbReportDate.Text = DateTime.Today.ToShortDateString();
            tbPreparedBy.Text = CCFBPrefs.PreparedBy;
            tbPhone.Text = CCFBPrefs.PhoneNumber;

            city = CCFBPrefs.DefaultCity;
        }

        private void getDistinctYears()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                rowCount = dadAdpt.Fill(dset, tbName);

                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            catch (SqlException ex) 
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString());
            }

        }
//Need To Redo This Code

        //private int getValueAsInt(int rowNum, string colName )
        //{
        //    try
        //    {
        //        return Int32.Parse(dtblMonthStats.Rows[rowNum][colName].ToString());
        //    }
        //    catch (System.Exception ex) 
        //    {
        //        CCFBGlobal.appendErrorToErrorReport("rowNum=" + rowNum + ", colName=" + colName,
        //            ex.GetBaseException().ToString());
        //        return 0; 
        //    }
        //}

        //private void calcTotals()
        //{
        //    decimal perVolhrs = 0;
        //    decimal cumVolhrs = 0;

        //    if (tbCumVolHrs.Text != null && tbCumVolHrs.Text.Trim() != "")
        //        cumVolhrs = Convert.ToDecimal(tbCumVolHrs.Text);

        //    if (tbPerVolHrs.Text != null && tbPerVolHrs.Text.Trim() != "")
        //        perVolhrs = Convert.ToDecimal(tbPerVolHrs.Text);


        //    tbPerLbsServedTotal.Text = (getValueAsInt(0, "LbsStandard")
        //        + getValueAsInt(0, "LbsOther")
        //        + getValueAsInt(0, "LbsCommodity")
        //        + getValueAsInt(0, "LbsSupplemental")).ToString("N00");

        //    tbCumLbsServedTotal.Text = (getValueAsInt(1, "LbsStandard")
        //        + getValueAsInt(1, "LbsOther")
        //        + getValueAsInt(1, "LbsCommodity")
        //        + getValueAsInt(1, "LbsSupplemental")).ToString("N00");
        //    tbDollarsPerHr.Tag = perVolhrs * CCFBPrefs.InkindDollarsPerHr;
        //    tbDollarsPerHr.Text = (perVolhrs * CCFBPrefs.InkindDollarsPerHr).ToString("C0");
        //    tbDollarsCumHr.Tag = cumVolhrs * CCFBPrefs.InkindDollarsPerHr;
        //    tbDollarsCumHr.Text = (cumVolhrs * CCFBPrefs.InkindDollarsPerHr).ToString("C0");
        //}

        private void fillYearsCombo()
        {
            cboReportYear.Items.Clear();
            string fiscalyear = CCFBGlobal.CurrentFiscalEndDate().Year.ToString();
            for (int i = 0; i < rowCount; i++)
            {
                cboReportYear.Items.Add(dset.Tables[tbName].Rows[i]["Year"]);

                if (dset.Tables[tbName].Rows[i]["Year"].ToString() ==  fiscalyear) // DateTime.Today.Year.ToString())
                    cboReportYear.SelectedIndex = i;

                //if (DateTime.Today.Day > 10)
                //    cboReportMonth.SelectedIndex = DateTime.Today.Month - 1;
                //else if(DateTime.Today.Month  == 1)
                //    cboReportMonth.SelectedIndex = 11;
                //else
                //    cboReportMonth.SelectedIndex = DateTime.Today.Month - 2;
            }

            if (cboReportYear.SelectedIndex < 0 && cboReportYear.Items.Count > 0)
                cboReportYear.SelectedIndex = 0;
        }

        private void btnLoadPeriod_Click(object sender, EventArgs e)
        {
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            lblLoadFamilySize.BackColor = Color.PaleGoldenrod;
            lblLoadStatistics.BackColor = Color.DimGray;
            lblLoadFoodRecieptsByFunding.BackColor = Color.DimGray;
            lblLoadFoodRecieptsByDonor.BackColor = Color.DimGray;
            lblFillForm.BackColor = Color.DimGray;
            lblTestForExistingFiles.BackColor = Color.DimGray;
            initForm();
            btnCreateReports.Enabled = false;
            btnEmailReports.Enabled = false;
            btnDisplayExisting.Enabled = false;
            sFiscalYear = cboReportYear.Text;
            if (CCFBPrefs.FiscalYearStartMonth == 1)
            {
                tpgGenCurFiscalYr.Text = sFiscalYear;
                tpgGenPrevFiscalYr.Text = (Convert.ToInt32(sFiscalYear) - 1).ToString();
            }
            else
            {
                tpgGenCurFiscalYr.Text = "FY " + sFiscalYear;
                tpgGenPrevFiscalYr.Text = "FY " + (Convert.ToInt32(sFiscalYear) - 1).ToString();
            }
            percentNWH = "";
            try
            {

                pnlLoadStatus.Visible = true;
                pnlLoadStatus.BringToFront();
                System.Windows.Forms.Application.DoEvents();

                if (CCFBPrefs.FiscalYearStartMonth == 1 || ((CCFBPrefs.FiscalYearStartMonth != 1 && (intRptMonth()) < CCFBPrefs.FiscalYearStartMonth)))
                {
                    datePeriodFirst = Convert.ToDateTime(strRptMonth() + "/01/" + sFiscalYear);
                }
                else
                {
                    datePeriodFirst = Convert.ToDateTime(strRptMonth() + "/01/" + sFiscalYear).AddYears(-1);
                }
                datePeriodLast = datePeriodFirst.AddMonths(1).AddDays(-1);
                //lblStatsPeriod.Text = cboReportMonth.Text;
                dateFiscalStart = CCFBGlobal.CalcFiscalStartDate(datePeriodFirst);
                sFiscalPeriod = CCFBGlobal.CalcFiscalPeriod(datePeriodFirst);
                
                foreach (TextBox item in pnlStatistics.Controls.OfType<TextBox>())
                { item.Text = ""; }

                string dateStart = datePeriodFirst.Year.ToString() + strRptMonth() + "01";
                string dateEnd = datePeriodLast.Year.ToString() + getFormatedMonthNumber(datePeriodLast.Month) + getFormatedMonthNumber(datePeriodLast.Day);
                string sql = GetLoadFamilySizeSQL("Date", dateStart, dateEnd);
                sqlCmd = new SqlCommand(sql, conn);
                sqlCmd.CommandType = CommandType.Text;
                dtblFamilySizeByDate = TransferDataToLocalDataTable(sqlCmd);
                System.Windows.Forms.Application.DoEvents(); 
                sqlCmd.Dispose();

                dateStart = dateFiscalStart.Year.ToString() + getFormatedMonthNumber(dateFiscalStart.Month);
                sql = GetLoadFamilySizeSQL("Month", dateStart, dateEnd.Substring(0, 6));
                sqlCmd = new SqlCommand(sql, conn);
                sqlCmd.CommandType = CommandType.Text;
                dtblFamilySizeByMonth = TransferDataToLocalDataTable(sqlCmd);
                lblLoadFamilySize.BackColor = Color.PaleGreen;
                sqlCmd.Dispose();

                sql = getTrxLogDataSQL("Date", datePeriodFirst.ToShortDateString(), datePeriodLast.ToShortDateString());
                sqlCmd = new SqlCommand(sql, conn);
                sqlCmd.CommandType = CommandType.Text;
                dtblTrxLogDataByDay = TransferDataToLocalDataTable(sqlCmd);
                System.Windows.Forms.Application.DoEvents(); 
                sqlCmd.Dispose();

                sql = getTrxLogDataSQL("Month", dateFiscalStart.ToShortDateString(), datePeriodLast.ToShortDateString());
                sqlCmd = new SqlCommand(sql, conn);
                sqlCmd.CommandType = CommandType.Text;
                dtblTrxLogDataByMonth = TransferDataToLocalDataTable(sqlCmd);
                System.Windows.Forms.Application.DoEvents(); 
                sqlCmd.Dispose();

                fillFoodRecieptsByFunding(dateFiscalStart, datePeriodLast);
                filllvwLbsFoodByDonor(datePeriodFirst, datePeriodLast, ref lvwLbsFoodByDonor, ref dtblFoodRecieptsByDonor, ref chartDonorMonth, datePeriodFirst.ToString("MMMM") + " " + datePeriodFirst.Year.ToString());
                filllvwLbsFoodByDonor(dateFiscalStart, datePeriodLast, ref lvwYTDLbsFoodByDonor, ref dtblYTDFoodRecieptsByDonor, ref chart1, dateFiscalStart.ToString("MMMM") + " " + dateFiscalStart.Year.ToString() + " - " + datePeriodFirst.ToString("MMMM") + " " + datePeriodFirst.Year.ToString());
                fillGeneralStats(ref lvwGenPrev, ref dtblPrevNbrServiceDays, ref dtblPrevInkindLbs, datePeriodFirst.AddYears(-1), datePeriodLast.AddYears(-1), dateFiscalStart.AddYears(-1), lblGenPrevFiscalPeriod);
                fillGeneralStats(ref lvwGenCur, ref dtblCurNbrServiceDays, ref dtblCurInkindLbs, datePeriodFirst, datePeriodLast, dateFiscalStart, lblGenCurFiscalPeriod);

                lvItmGenYTD = lvwGenCur.Items[lvwGenCur.Items.Count - 2];
                fillVoucherStats(ref lvwVoucherData, dateFiscalStart, datePeriodFirst);
                //tbDollarsPer.Tag = Convert.ToDecimal(tbDollarsPerLbs.Tag.ToString()) + Convert.ToDecimal(tbDollarsPerHr.Tag.ToString());
                //tbDollarsPer.Text = (Convert.ToDecimal(tbDollarsPerLbs.Tag.ToString()) + Convert.ToDecimal(tbDollarsPerHr.Tag.ToString())).ToString("C0");
                //tbDollarsCum.Tag = Convert.ToDecimal(tbDollarsCumLbs.Tag.ToString()) + Convert.ToDecimal(tbDollarsCumHr.Tag.ToString());
                //tbDollarsCum.Text = Convert.ToDecimal(Convert.ToDecimal(tbDollarsCumLbs.Tag.ToString()) + Convert.ToDecimal(tbDollarsCumHr.Tag.ToString())).ToString("C0");

                initChartsByMonth(chartHHByMonth);
                initChartsByMonth(chartHHCommodities);
                initChartsByMonth(chartHHSupplemental);
                initChartsByMonth(chartHHBabySvcs);
                initChartsByMonth(chartHHHomeless);
                initChartsByMonth(chartHHTransient);
                initChartsByMonth(chartHHInCityLimits);

                //chartHHServed.Series[0].Points.Clear();
                lblLbsSvdForMonth.Text = lblLbsSvdForMonth.Tag.ToString() + datePeriodLast.ToString("MMMM");
                
                clsTrxLogPrvFiscalY2Stats = fillMonthEndStats(tbHHPvY2MonthList, tbHHPvY2YTDList, tbLbsSvdPvY2MonthList, tbLbsSvdPvY2YTDList, dateFiscalStart.AddYears(-2), dateFiscalStart.AddDays(-1).AddYears(-1), CCFBGlobal.CalcFiscalPeriod(datePeriodFirst.AddYears(-2)), 0, lblHHPrev2YTD, lblIndPvY2YTD, lblLbsSvdPvY2Month, lblLbsSvdPvY2YTD, true);
                clsTrxLogPrvFiscalYrStats = fillMonthEndStats(tbHHPvYrMonthList, tbHHPvYrYTDList, tbLbsSvdPvYrMonthList, tbLbsSvdPvYrYTDList, dateFiscalStart.AddYears(-1), dateFiscalStart.AddDays(-1), CCFBGlobal.CalcFiscalPeriod(datePeriodFirst.AddYears(-1)), 1, lblHHPrev1YTD, lblIndPvYrYTD, lblLbsSvdPvYrMonth, lblLbsSvdPvYrYTD, true);
                clsTrxLogCurFiscalYrStats = fillMonthEndStats(tbHHCurMonthList, tbHHCurYTDList, tbLbsSvdCurMonthList, tbLbsSvdCurYTDList, dateFiscalStart, datePeriodLast, CCFBGlobal.CalcFiscalPeriod(datePeriodFirst), 2, lblHHCurYTD, lblIndCurYTD, lblLbsSvdCurMonth, lblLbsSvdCurYTD, true);
                
                clsTrxLogCurFiscalYrHDStats = fillMonthEndStats(tbHHCurMonthHDList, null, null, null, dateFiscalStart, datePeriodLast, CCFBGlobal.CalcFiscalPeriod(datePeriodFirst), -1, null, null, null, null, false);
                clsTrxLogPrvFiscalYrHDStats = fillMonthEndStats(tbHHPvYrMonthHDList, null, null, null, dateFiscalStart.AddYears(-1), dateFiscalStart.AddDays(-1), CCFBGlobal.CalcFiscalPeriod(datePeriodFirst.AddYears(-1)), -1, null, null, null, null, false);
                
                clsTrxLogCurFiscalYrStats.findFiscalPeriod(CCFBGlobal.CalcFiscalPeriod(datePeriodFirst));
                clsTrxLogPrvFiscalYrStats.findFiscalPeriod(CCFBGlobal.CalcFiscalPeriod(datePeriodFirst.AddYears(-1)));
                fillMonthEndChange(clsTrxLogCurFiscalYrStats, clsTrxLogPrvFiscalYrStats, tbHHDifCurMonthList);
                clsTrxLogCurFiscalYrStats.setYTDRow();
                clsTrxLogPrvFiscalYrStats.setYTDRow();
                fillMonthEndChange(clsTrxLogCurFiscalYrStats, clsTrxLogPrvFiscalYrStats, tbHHDifYTDList);
                fillFamilyBreakdownCharts();
                fillServiceCounts(lvwNewHH, lvwSvcDays, dateFiscalStart, datePeriodLast);
                pnlReports.Visible = true;

                lvReports.Enabled = true;
                btnExportData.Enabled = true;
            }
            catch (SqlException ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString());
                btnExportData.Enabled = false;
            }

            lblFillForm.BackColor = Color.PaleGoldenrod;
            System.Windows.Forms.Application.DoEvents();
            fillForm();
            btnCreateReports.Enabled = true;
            lblFillForm.BackColor = Color.PaleGreen;
            lblTestForExistingFiles.BackColor = Color.PaleGoldenrod;
            System.Windows.Forms.Application.DoEvents();
            findIfReportsExist();
            tabPage1.BackColor = Color.Beige;
            tabPage2.BackColor = Color.Beige;
            this.Cursor = System.Windows.Forms.Cursors.Default;
            pnlLoadStatus.Visible = false;
        }

        private void initForm()
        {
            tabControl1.SelectTab("tpgLoadData");
            pnlReports.Visible = false;

            initTexBoxList(tbHHCurMonthList);
            initTexBoxList(tbHHCurMonthBPList);
            initTexBoxList(tbHHCurMonthHDList);
            initTexBoxList(tbHHCurYTDList);
            initTexBoxList(tbHHPvYrMonthList);
            initTexBoxList(tbHHPvYrYTDList);
            initTexBoxList(tbHHPvY2MonthList);
            initTexBoxList(tbHHPvY2YTDList);

            initTexBoxList(tbHHDifCurMonthList);
            initTexBoxList(tbHHDifCurMonthBPList);
            initTexBoxList(tbHHDifCurMonthHDList);
            initTexBoxList(tbHHDifYTDList);

            initTexBoxList(tbVolMonthList);
            initTexBoxList(tbVolYTDList);

            initTexBoxList(tbLbsRcvdPerList);
            initTexBoxList(tbLbsRcvdCumList);

            initTexBoxList(tbLbsSvdCurMonthList);
            initTexBoxList(tbLbsSvdPvYrMonthList);
            initTexBoxList(tbLbsSvdPvY2MonthList);

            initTexBoxList(tbLbsSvdCurYTDList);
            initTexBoxList(tbLbsSvdPvYrYTDList);
            initTexBoxList(tbLbsSvdPvY2YTDList);

            lvwFamilySizeBySvcDate.Items.Clear();
            lvwFamilySizeByMonth.Items.Clear();
            lvwLbsFoodByDonor.Items.Clear();
            lvwTrxLogByDay.Items.Clear();
            lvwTrxLogByMonth.Items.Clear();
            //tbDollarsPerHr.Tag = 0;
            //tbDollarsCumHr.Tag = 0;
            System.Windows.Forms.Application.DoEvents();
        }

        private void fillForm()
        {
            if (dtblFamilySizeByDate.Rows.Count > 0)
            {
                lvwFamilySizeBySvcDate.BackColor = Color.Ivory;
                LoadFamilySizeListView(lvwFamilySizeBySvcDate, dtblFamilySizeByDate, 0);
            }
            if (dtblFamilySizeByMonth.Rows.Count > 0)
            {
                lvwFamilySizeByMonth.BackColor = Color.LightYellow;
                LoadFamilySizeListView(lvwFamilySizeByMonth, dtblFamilySizeByMonth, 1);
            }
            if (dtblTrxLogDataByDay.Rows.Count > 0)
            {
                lvwTrxLogByDay.BackColor = Color.Ivory;
                LoadTrxLogDataListView(lvwTrxLogByDay, dtblTrxLogDataByDay,0);
            }
            if (dtblTrxLogDataByMonth.Rows.Count > 0)
            {
                lvwTrxLogByMonth.BackColor = Color.LightYellow;
                LoadTrxLogDataListView(lvwTrxLogByMonth, dtblTrxLogDataByMonth,1);
            }
        }

        private void LoadFamilySizeListView(ListView lvw, DataTable dt, int dateFormat)
        {
            int[] colTotals = new int[20];
            int rowSum = 0;
            int iVal = 0;
            ListViewItem lvItm;
            foreach (DataRow drow in dt.Rows)
            {
                string dateSvc = drow["SvcDate"].ToString();
                if (dateFormat == 0)
                    lvItm = new ListViewItem(dateSvc.Substring(4, 2) + "/" + dateSvc.Substring(6, 2) + "/" + dateSvc.Substring(0, 4));
                else
                    lvItm = new ListViewItem(dateSvc.Substring(0, 4) + " " + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(dateSvc.Substring(4, 2))));
                rowSum = 0;
                for (int i = 1; i < dt.Columns.Count; i++)
                {
                    iVal = Convert.ToInt32(drow[i]);
                    rowSum += iVal;
                    colTotals[i - 1] += iVal;
                    lvItm.SubItems.Add(iVal.ToString("N00"));
                }
                lvItm.SubItems.Add(rowSum.ToString("N00"));
                lvw.Items.Add(lvItm);
            }
            lvItm = new ListViewItem("Total Households");
            rowSum = 0;
            for (int i = 0; i < colTotals.Length; i++)
            {
                rowSum += colTotals[i];
                lvItm.SubItems.Add(colTotals[i].ToString("N00"));
                if (i > 9)
                {
                    if (colTotals[i] == 0)
                        lvw.Columns[i + 1].Width = 0;
                }
            }
            lvItm.SubItems.Add(rowSum.ToString("N00"));
            lvItm.BackColor = Color.PaleGoldenrod;
            lvw.Items.Add(lvItm);

            lvItm = new ListViewItem("Household %");
            decimal percentVal = 0;
            decimal totalHH = Convert.ToDecimal(rowSum);
            for (int i = 0; i < colTotals.Length; i++)
            {
                if (colTotals[i] != 0)
                {
                    percentVal = Convert.ToDecimal(colTotals[i]) / totalHH;
                    lvItm.SubItems.Add(percentVal.ToString("P01"));
                }
                else
                {
                    lvItm.SubItems.Add("");
                }
            }
            lvItm.SubItems.Add("100%");
            lvItm.BackColor = Color.PaleGoldenrod;
            lvw.Items.Add(lvItm);
            
            lvItm = new ListViewItem("Total Individuals");
            rowSum = 0;
            for (int i = 0; i < colTotals.Length; i++)
            {
                iVal = colTotals[i] * (i + 1);
                rowSum += iVal;
                lvItm.SubItems.Add(iVal.ToString());
            }
            lvItm.SubItems.Add(rowSum.ToString());
            lvItm.BackColor = Color.Beige;
            lvw.Items.Add(lvItm);

            lvItm = new ListViewItem("Individual %");
            totalHH = Convert.ToDecimal(rowSum);
            for (int i = 0; i < colTotals.Length; i++)
            {
                if (colTotals[i] != 0)
                {
                    iVal = colTotals[i] * (i + 1);
                    percentVal = Convert.ToDecimal(iVal) / totalHH;
                    lvItm.SubItems.Add(percentVal.ToString("P01"));
                }
                else
                {
                    lvItm.SubItems.Add("");
                }
            }
            lvItm.SubItems.Add("100%");
            lvItm.BackColor = Color.Beige;
            lvw.Items.Add(lvItm);

        }

        private void LoadTrxLogDataListView(ListView lvw, DataTable dt, int dateFormat)
        {
            int[] colTotals = new int[19];
            //int rowSum = 0;
            int iVal = 0;
            int iColAvgHHSize = -1;
            int iColFamilyTotal = -1;
            int iColNbrServed = -1;
            int iColNbrLbs = -1;
            int iColLbsPerInd = -1;
            int iColLbsPerSvc = -1;
            decimal dVal = 0;
            ListViewItem lvItm;
            for (int i = 1; i < dt.Columns.Count; i++)
            {
                if (dt.Columns[i].Caption == "AvgHHSize")
                    iColAvgHHSize = i - 1;
                else if (dt.Columns[i].Caption == "NbrTotalFamily")
                    iColFamilyTotal = i - 1;
                else if (dt.Columns[i].Caption == "NbrServed")
                    iColNbrServed = i - 1;
                else if (dt.Columns[i].Caption == "TotalLbs")
                    iColNbrLbs = i - 1;
                else if (dt.Columns[i].Caption == "LbsPerInd")
                    iColLbsPerInd = i - 1;
                else if (dt.Columns[i].Caption == "LbsPerSvc")
                    iColLbsPerSvc = i - 1;
            }
            foreach (DataRow drow in dt.Rows)
            {
                string dateSvc = drow["SvcDate"].ToString();
                if (dateFormat == 0)
                    lvItm = new ListViewItem(dateSvc.Substring(4, 2) + "/" + dateSvc.Substring(6, 2) + "/" + dateSvc.Substring(0, 4));
                else
                    lvItm = new ListViewItem(dateSvc.Substring(0, 4) + " " + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(dateSvc.Substring(4, 2))));
                for (int i = 1; i < dt.Columns.Count; i++)
                {
                    if (i == iColAvgHHSize+1)
                    {
                        dVal = Convert.ToDecimal(drow[i]);
                        lvItm.SubItems.Add(dVal.ToString("N02"));
                    }
                    else 
                    {
                        iVal = Convert.ToInt32(drow[i]);
                        lvItm.SubItems.Add(iVal.ToString("N00"));
                        if (i <= iColNbrLbs+1)
                            colTotals[i - 1] += iVal;
                    }
                }
                //lvItm.SubItems.Add(rowSum.ToString());
                lvw.Items.Add(lvItm);
            }
            lvItm = new ListViewItem("Totals");
            for (int i = 0; i < colTotals.Length; i++)
            {
                if (i == iColAvgHHSize)
                {
                    dVal = Convert.ToDecimal(colTotals[iColFamilyTotal]) / Convert.ToDecimal(colTotals[iColNbrServed]);
                    lvItm.SubItems.Add(dVal.ToString("N02"));
                }
                else if (i <= iColNbrLbs)
                {
                    lvItm.SubItems.Add(colTotals[i].ToString("N00"));
                    if (colTotals[i] == 0)
                    {
                        lvw.Columns[i + 1].Width = 0;
                    }
                }
                else if (i == iColLbsPerInd)
                { 
                    dVal = colTotals[iColNbrLbs] / colTotals[iColFamilyTotal];
                    lvItm.SubItems.Add(dVal.ToString("N00"));
                }
                else if (i == iColLbsPerSvc)
                {
                    dVal = Convert.ToDecimal(colTotals[iColNbrLbs]) / Convert.ToDecimal(colTotals[iColNbrServed]);
                    lvItm.SubItems.Add(dVal.ToString("N00"));
                }
            }
            //lvItm.SubItems.Add(rowSum.ToString());
            lvItm.BackColor = Color.PaleGoldenrod;
            lvw.Items.Add(lvItm);

            lvItm = new ListViewItem("Percent");
            for (int i = 0; i < colTotals.Length; i++)
            {
                if (i <= iColFamilyTotal)
                {
                    if (colTotals[i] == 0)
                    {
                        lvItm.SubItems.Add("");
                    }
                    else
                    {
                        dVal = Convert.ToDecimal(colTotals[i]) / Convert.ToDecimal(colTotals[iColFamilyTotal]);
                        lvItm.SubItems.Add(dVal.ToString("P01"));
                    }
                }
                else
                {
                    if (iColAvgHHSize == i || iColNbrServed == i)
                    {
                        lvItm.SubItems.Add("");
                    }
                    else
                    {
                        if (i < iColNbrServed)
                        {
                            if (colTotals[i] == 0)
                            {
                                lvItm.SubItems.Add("");
                            }
                            else
                            {
                                dVal = Convert.ToDecimal(colTotals[i]) / Convert.ToDecimal(colTotals[iColNbrServed]);
                                lvItm.SubItems.Add(dVal.ToString("P01"));
                            }
                        }
                        else
                        {
                            if (i <= iColNbrLbs)
                            {
                                if (colTotals[i] == 0)
                                {
                                    lvItm.SubItems.Add("");
                                }
                                else
                                {
                                    dVal = Convert.ToDecimal(colTotals[i]) / Convert.ToDecimal(colTotals[iColNbrLbs]);
                                    lvItm.SubItems.Add(dVal.ToString("P01"));
                                }
                            }
                        }
                    }

                }
            }
            //lvItm.SubItems.Add(rowSum.ToString());
            lvItm.BackColor = Color.PaleGoldenrod;
            lvw.Items.Add(lvItm);
            //lvItm = new ListViewItem("Household %");
            //decimal percentVal = 0;
            //decimal totalHH = Convert.ToDecimal(rowSum);
            //for (int i = 0; i < colTotals.Length; i++)
            //{
            //    if (colTotals[i] != 0)
            //    {
            //        percentVal = Convert.ToDecimal(colTotals[i]) / totalHH;
            //        lvItm.SubItems.Add(percentVal.ToString("P01"));
            //    }
            //    else
            //    {
            //        lvItm.SubItems.Add("");
            //    }
            //}
            //lvItm.SubItems.Add("100%");
            //lvItm.BackColor = Color.PaleGoldenrod;
            //lvw.Items.Add(lvItm);

            //lvItm = new ListViewItem("Total Individuals");
            //rowSum = 0;
            //for (int i = 0; i < colTotals.Length; i++)
            //{
            //    iVal = colTotals[i] * (i + 1);
            //    rowSum += iVal;
            //    lvItm.SubItems.Add(iVal.ToString());
            //}
            //lvItm.SubItems.Add(rowSum.ToString());
            //lvItm.BackColor = Color.Beige;
            //lvw.Items.Add(lvItm);

            //lvItm = new ListViewItem("Individual %");
            //totalHH = Convert.ToDecimal(rowSum);
            //for (int i = 0; i < colTotals.Length; i++)
            //{
            //    if (colTotals[i] != 0)
            //    {
            //        iVal = colTotals[i] * (i + 1);
            //        percentVal = Convert.ToDecimal(iVal) / totalHH;
            //        lvItm.SubItems.Add(percentVal.ToString("P01"));
            //    }
            //    else
            //    {
            //        lvItm.SubItems.Add("");
            //    }
            //}
            //lvItm.SubItems.Add("100%");
            //lvItm.BackColor = Color.Beige;
            //lvw.Items.Add(lvItm);

        }

        private string getFormatedMonthNumber(int month)
        {
            if (month < 10)
            {
                return "0" + month.ToString();
            }
            else
            {
                return month.ToString();
            }
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCreateReports_Click(object sender, EventArgs e)
        {
            object saveAs = "";
            string templatePath = "";
            bool error = false;
            btnCreateReports.Enabled = false;
            System.Windows.Forms.Application.DoEvents();

            string reportPath = makeReportPath();
            string rptNamePrefix = makeReportPrefix();
            CCFBGlobal.verifyPath(reportPath);
            for (int i = 1; i < lvReports.Items.Count; i++)
            {
                if (lvReports.Items[i].Checked == true)
                {
                    clsMonthlyReports.find(Convert.ToInt32(lvReports.Items[i].Tag));
                    saveAs = reportPath + rptNamePrefix + clsMonthlyReports.ReportName + clsMonthlyReports.DocType;

                    templatePath = clsMonthlyReports.ReportPath;
                    clsTrxLogCurFiscalYrStats.findFiscalPeriod(sFiscalPeriod);
                    clsVolStats.findFiscalPeriod(sFiscalPeriod);
                    switch (lvReports.Items[i].Tag.ToString())
                    {
                        case "1":       //EFAP Report
                            {
                                RptNewEFAP clsEFAPReport = new RptNewEFAP(clsTrxLogCurFiscalYrStats);
                                clsEFAPReport.createReport(tbFBName.Text, cboReportMonth.Text,
                                    tbCounty.Text, tbPreparedBy.Text, saveAs, templatePath, tbReportDate.Text);
                                error = clsEFAPReport.Error;
                                break;
                            }
                        case "3": //FB Coalition Report
                            {
                                RptFBCoalition clsFBCoalitionReport = new RptFBCoalition(clsTrxLogCurFiscalYrStats, clsVolStats);
                                clsFBCoalitionReport.createReport(tbFBName.Text, cboReportMonth.Text, cboReportYear.SelectedText.ToString(),
                                                                        tbPreparedBy.Text, saveAs, templatePath, tbReportDate.Text, false);
                                error = clsFBCoalitionReport.Error;
                                break;
                            }
                        case "4":       //NW Harvest Report
                            {
                                RptNWHarvest clsNWHReport = new RptNWHarvest(clsTrxLogCurFiscalYrStats);
                                clsNWHReport.createReport(tbFBName.Text, cboReportMonth.Text, sFiscalYear
                                                                , tbCounty.Text, tbHHCurMonthTotalServed.Text, tbPreparedBy.Text
                                                                , saveAs, templatePath, tbReportDate.Text, percentNWH);
                                error = clsNWHReport.Error;
                                break;
                            }
                        case "5":       //Food Life Line Report
                            {
                                RptFoodLifeLine clsFLLReport = new RptFoodLifeLine(clsTrxLogCurFiscalYrStats);
                                clsFLLReport.createReport(tbFBName.Text, cboReportMonth.Text, sFiscalYear
                                    , tbCounty.Text, tbPreparedBy.Text, tbPhone.Text
                                    , tbLbsSvdCurMonthTotal.Text, saveAs, templatePath, chkAutoPrint.Checked);
                                error = clsFLLReport.Error;
                                break;
                            }
                        case "6":      //Second Harvest Pantry Monthly Report
                            {
                                Rpt2ndHarvPantryMthly clsSecHarvMthRpt = new Rpt2ndHarvPantryMthly(clsTrxLogCurFiscalYrStats);
                                clsSecHarvMthRpt.createReport(tbFBName.Text, cboReportMonth.Text, sFiscalYear
                                                    , tbCounty.Text, city, tbPhone.Text, tbPreparedBy.Text, saveAs, templatePath);

                                error = clsSecHarvMthRpt.Error;
                                break;
                            }
                        case "7":      //Second Harvest Monthly Report
                            {
                                Rpt2ndHarvMthly clsSecHarvMthRpt = new Rpt2ndHarvMthly(clsTrxLogCurFiscalYrStats);
                                clsSecHarvMthRpt.createReport(tbFBName.Text, cboReportMonth.Text, tbPreparedBy.Text, saveAs, templatePath
                                                    , tbCounty.Text, tbPhone.Text
                                                    , tbLbsSvdCurMonthTotal.Text, nbrDaysOpen());

                                error = clsSecHarvMthRpt.Error;
                                break;
                            }
                        case "8": //EFAP Subcontractor Report 
                            {

                                RptEFAPSubcontractors clsEFAPSubcontractorsReport
                                    = new RptEFAPSubcontractors(clsTrxLogCurFiscalYrStats);
                                if (clsVolStats.RowCount > 0)
                                {
                                    clsEFAPSubcontractorsReport.createReport(saveAs, templatePath, tbLbsSvdCurMonthTotal.Text,
                                        clsVolStats.dollarsInkindHours(sFiscalPeriod), dollarsInkindLbs(), tbPreparedBy.Text, cboReportMonth.Text);
                                }
                                else
                                {
                                    clsEFAPSubcontractorsReport.createReport(saveAs, templatePath, tbLbsSvdCurMonthTotal.Text,
                                        "", dollarsInkindLbs(), tbPreparedBy.Text, cboReportMonth.Text);
                                }
                                error = clsEFAPSubcontractorsReport.Error;
                                break;
                            }
                        case "11": //FB Coalition Report with Teens
                            {
                                RptFBCoalition clsFBCoalitionReport = new RptFBCoalition(clsTrxLogCurFiscalYrStats, clsVolStats);
                                clsFBCoalitionReport.createReport(tbFBName.Text, cboReportMonth.Text, cboReportYear.SelectedText.ToString(),
                                                                        tbPreparedBy.Text, saveAs, templatePath, tbReportDate.Text, true);
                                error = clsFBCoalitionReport.Error;
                                break;
                            }
                        case "12": //EFN Monthly Report
                            {
                                string rptMonthYear = "'" + cboReportMonth.Text.ToString().Substring(5) + " " + cboReportMonth.Text.ToString().Substring(0, 4);
                                RptEFNXls clsEFNReport = new RptEFNXls(clsTrxLogCurFiscalYrStats);
                                clsEFNReport.createReport(tbFBName.Text, rptMonthYear,
                                                                tbPreparedBy.Text, saveAs, templatePath, tbReportDate.Text, tbLbsSvdCurMonthTotal.Text);
                                error = clsEFNReport.Error;
                                break;
                            }
                        case "13": //Skagit County EFAP
                            {
                                RptSkagitEFAP clsSkagitEFAPReport = new RptSkagitEFAP(clsTrxLogCurFiscalYrStats, clsVolStats, dtblFoodRecieptsByFunding);
                                clsSkagitEFAPReport.createReport(tbFBName.Text, cboReportMonth.Text,
                                    tbCounty.Text, tbPreparedBy.Text, saveAs, templatePath, tbReportDate.Text);
                                error = clsSkagitEFAPReport.Error;
                                break;
                            }
                        case "14": //EFN Calendar Year To Date Report
                            {
                                TrxLogPeriodTotals clsTrxLogCurCalYrStats = new TrxLogPeriodTotals(CCFBGlobal.connectionString);
                                DateTime dateCalStart = Convert.ToDateTime("01/01/" + datePeriodLast.Year.ToString());
                                clsTrxLogCurCalYrStats.open(dateCalStart.ToShortDateString(), datePeriodLast.ToShortDateString());
                                clsTrxLogCurCalYrStats.findYearMonth(datePeriodLast.Year.ToString() + CCFBGlobal.formatNumberWithLeadingZero(datePeriodLast.Month));
                                clsTrxLogCurCalYrStats.setYTDRow();
                                string rptMonthYear = "'" + cboReportMonth.Text.ToString().Substring(5) + " " + cboReportMonth.Text.ToString().Substring(0, 4);
                                RptEFNXls clsEFNReport = new RptEFNXls(clsTrxLogCurCalYrStats);
                                clsEFNReport.createReport(tbFBName.Text, rptMonthYear,
                                                                tbPreparedBy.Text, saveAs, templatePath, tbReportDate.Text, tbLbsSvdCurMonthTotal.Text);
                                error = clsEFNReport.Error;
                                break;
                            }
                        case "15": //EFAP Subcontractor Report 
                            {
                                clsTrxLogCurFiscalYrStats.setYTDRow();
                                
                                RptEFAPSubcontractors clsEFAPSubcontractorsReport
                                    = new RptEFAPSubcontractors(clsTrxLogCurFiscalYrStats);
                                if (clsVolStats.RowCount > 0)
                                {
                                    clsVolStats.setYTDRow();
                                    clsEFAPSubcontractorsReport.createReport(saveAs, templatePath, tbLbsSvdCurYTDTotal.Text,
                                        lvItmGenYTD.SubItems[5].Text, lvItmGenYTD.SubItems[6].Text, tbPreparedBy.Text, cboReportMonth.Text + " YTD");
                                }
                                else
                                {
                                    clsEFAPSubcontractorsReport.createReport(saveAs, templatePath, tbLbsSvdCurYTDTotal.Text,
                                        "", lvItmGenYTD.SubItems[6].Text, tbPreparedBy.Text, cboReportMonth.Text + " YTD");
                                }
                                error = clsEFAPSubcontractorsReport.Error;
                                break;
                            }
                        case "16": //BMAC Report - Blue Mountain Action Council 
                            {
                                TrxLogPeriodTotals clsTrxLogCurCalYrStats = new TrxLogPeriodTotals(CCFBGlobal.connectionString);
                                DateTime dateCalStart = Convert.ToDateTime("01/01/" + datePeriodLast.Year.ToString());
                                clsTrxLogCurCalYrStats.openCal(dateCalStart.ToShortDateString(), datePeriodLast.ToShortDateString());
                                clsTrxLogCurCalYrStats.findYearMonth(datePeriodLast.Year.ToString() + CCFBGlobal.formatNumberWithLeadingZero(datePeriodLast.Month));
                                string rptMonthYear = "'" + cboReportMonth.Text.ToString().Substring(5) + " " + cboReportMonth.Text.ToString().Substring(0, 4);
                                RptBMAC clsBMACReport
                                    = new RptBMAC(clsTrxLogCurCalYrStats);
                                    clsBMACReport.createReport(saveAs, templatePath, tbLbsSvdCurMonthTotal.Text,
                                        "", lbsEFAP,lbsTEFAP, lbs2ndHarvest, tbPreparedBy.Text, cboReportMonth.Text);
                                error = clsBMACReport.Error;
                                break;
                            }
                    }
                    if (error == true && File.Exists(saveAs.ToString()))
                    {
                        File.Delete(saveAs.ToString());
                    }
                    error = false;
                }
            }
            btnCreateReports.Enabled = true;
            findIfReportsExist();
        }

        private void btnEmailReports_Click(object sender, EventArgs e)
        {
            string fileName = "";
            string filePath = "";
            for (int i = 1; i < lvReports.Items.Count; i++)
            {
                if (lvReports.Items[i].Checked == true)
                {
                    clsMonthlyReports.find(Convert.ToInt32(lvReports.Items[i].Tag));
                    
                    EmailBodyInputForm frmEmailBody = new EmailBodyInputForm(clsMonthlyReports.ReportName);
                    frmEmailBody.ShowDialog();

                    fileName = makeReportPrefix() + clsMonthlyReports.ReportName + clsMonthlyReports.DocType;
                    filePath = makeReportPath() + fileName;

                    checkFileExistsAndSendEmail(filePath, fileName, clsMonthlyReports.EmailAddresses.Replace('|', ';'), frmEmailBody.EmailBody);
                }
            }
        }

        private void checkFileExistsAndSendEmail(string filePath, string fileName, string emailAddresses, string EmailBody)
        {
            if (File.Exists(filePath) == true)
                sendEmail(filePath, fileName, emailAddresses, EmailBody);
            else
                MessageBox.Show("Report " + fileName + " Does Not Exist.  Please Create Report And Try Again", "Report Does Not Exist",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void sendEmail(string filePath, string fileName, string emailList, string emailBody)
        {
            try
            {
                Microsoft.Office.Interop.Outlook.Application oApp;
                Microsoft.Office.Interop.Outlook._NameSpace oNameSpace;
                Microsoft.Office.Interop.Outlook.MAPIFolder oSaveFolder; 
                oApp = new Microsoft.Office.Interop.Outlook.Application();
                oNameSpace = oApp.GetNamespace("MAPI");

                oNameSpace.Logon("", "", true, true);
                oSaveFolder = oNameSpace.GetDefaultFolder(
                            Microsoft.Office.Interop.Outlook.OlDefaultFolders.olFolderSentMail);

                Microsoft.Office.Interop.Outlook._MailItem oMailItem = 
                        (Microsoft.Office.Interop.Outlook._MailItem)oApp.CreateItem(
                            Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);

                oMailItem.To = emailList;
                oMailItem.Subject = fileName + " From " + tbFBName.Text;
                if (emailBody == "")
                {
                    emailBody = " ";
                }
                oMailItem.Body = emailBody;
                oMailItem.SaveSentMessageFolder =  oSaveFolder;
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
                ////Create the new message by using the simplest approach.
                //MailItem oMsg = (MailItem)oApp.CreateItem(OlItemType.olMailItem);
                //Inspector oAddSig = null;
                ////oAddSig = oMsg.GetInspector;

                ////Add a recipient.
                //// TODO: Change the following recipient where appropriate.
                //Recipient oRecip = (Recipient)oMsg.Recipients.Add(emailList);
                //oRecip.Resolve();

                ////Set the basic properties.
                //oMsg.Subject = fileName + " From " + tbFBName.Text;
                //oMsg.Body = "Put YOUR Message Here";

                ////Add an attachment.
                //// TODO: change file path where appropriate
                //String sSource = filePath;
                //String sDisplayName = fileName;
                //int iPosition = (int)oMsg.Body.Length + 1;
                //int iAttachType = (int)OlAttachmentType.olByValue;
                //Attachment oAttach = oMsg.Attachments.Add(sSource, iAttachType, iPosition, sDisplayName);

                //// If you want to, display the message.
                ////oMsg.Display(true);  //modal

                ////Send the message.
                ////oMsg.Save();
                //((Microsoft.Office.Interop.Outlook._MailItem)oMsg).Send();

                //Explicitly release objects.
                //oRecip = null;
                //oAttach = null;
                //oMsg = null;
                
                oApp = null;
            }

                // Simple error handler.
            catch (System.Exception ex)
            {
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString());
            }
        }

        private void btnDisplayExisting_Click(object sender, EventArgs e)
        {
            string fileName = "";
            foreach (ListViewItem lvi in lvReports.Items)
            {
                if (lvi.Checked == true && lvi.BackColor == Color.LightSteelBlue)
                {
                    try
                    {
                        clsMonthlyReports.find(Convert.ToInt32(lvi.Tag));
                        fileName =makeReportPath() + makeReportPrefix() + clsMonthlyReports.ReportName + clsMonthlyReports.DocType;

                        CCFBGlobal.openDocumentOutsideCCFB(fileName);
                    }
                    catch (System.Exception ex)
                    {
                        CCFBGlobal.appendErrorToErrorReport("FileName=" + fileName.ToString(), ex.GetBaseException().ToString());
                        MessageBox.Show("The File " + fileName.ToString() + " Does Not Exist");
                    }
                }
            }
        }

        private void lvReports_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            bool bEnableCreate = false;
            bool bEnableDisplay = false;
            if (e.Item.Index == 0)
            {
                bEnableCreate = true;
                for (int i = 1; i < lvReports.Items.Count; i++)
                {
                    lvReports.Items[i].Checked = lvReports.Items[0].Checked;
                    bEnableDisplay = (lvReports.Items[i].BackColor == Color.LightSteelBlue);
                }
            }
            else
            {
                for (int i = 1; i < lvReports.Items.Count; i++)
                {
                    if (lvReports.Items[i].Checked == true)
                    {
                        bEnableCreate = true;
                        bEnableDisplay = (lvReports.Items[i].BackColor == Color.LightSteelBlue);
                    }
                }
            }
            btnCreateReports.Enabled = bEnableCreate;
            btnDisplayExisting.Enabled = bEnableDisplay;
            btnEmailReports.Enabled = bEnableDisplay;
        }

        private void cbo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            tabPage1.BackColor = Color.LightGray;
            tabPage2.BackColor = Color.LightGray;
            lvReports.Enabled = false;
            btnCreateReports.Enabled = false;
            btnDisplayExisting.Enabled = false;
            btnEmailReports.Enabled = false;
            updateRptMonthsList();
        }
        /// <summary>
        /// Traverses all controls on the page using recursion and adds the proper ones
        /// to their proper collections and adds LostFocus event to Textboxes and Checkboxes
        /// </summary>
        /// <param name="controlList"></param>
        private void traverseAndAddControlsToCollections(Control.ControlCollection controlList)
        {
            foreach (Control ctrl in controlList.OfType<Control>())
            {
                if (ctrl.GetType().Name == "TextBox")
                {
                    if (ctrl.Tag != null)
                    {
                        Debug.Assert(ctrl.Name.ToString().StartsWith("tbIndCurMonth") == false,ctrl.Tag.ToString());
                        if (ctrl.Name.ToString().StartsWith("tbHHCurMonth") == true || ctrl.Name.ToString().StartsWith("tbIndCurMonth") == true)
                            tbHHCurMonthList.Add((TextBox)ctrl);
                        if (ctrl.Name.ToString().StartsWith("tbBPCurMonth") == true || ctrl.Name.ToString().StartsWith("tbBPIndCurMonth") == true)
                            tbHHCurMonthBPList.Add((TextBox)ctrl);
                        if (ctrl.Name.ToString().StartsWith("tbHDCurMonth") == true || ctrl.Name.ToString().StartsWith("tbHDIndCurMonth") == true)
                            tbHHCurMonthHDList.Add((TextBox)ctrl);
                        else if (ctrl.Name.ToString().StartsWith("tbHHCurYTD") == true || ctrl.Name.ToString().StartsWith("tbIndCurYrYTD") == true)
                            tbHHCurYTDList.Add((TextBox)ctrl);
                        else if (ctrl.Name.ToString().StartsWith("tbHHDifCurMonth") == true || ctrl.Name.ToString().StartsWith("tbIndDifMonth") == true)
                            tbHHDifCurMonthList.Add((TextBox)ctrl);
                        else if (ctrl.Name.ToString().StartsWith("tbBPDifCurMonth") == true || ctrl.Name.ToString().StartsWith("tbIndBPDifMonth") == true)
                            tbHHDifCurMonthBPList.Add((TextBox)ctrl);
                        else if (ctrl.Name.ToString().StartsWith("tbHDDifCurMonth") == true || ctrl.Name.ToString().StartsWith("tbHDIndDifMonth") == true)
                            tbHHDifCurMonthHDList.Add((TextBox)ctrl);
                        else if (ctrl.Name.ToString().StartsWith("tbHHDifYTD") == true || ctrl.Name.ToString().StartsWith("tbIndDifYTD") == true)
                            tbHHDifYTDList.Add((TextBox)ctrl);
                        else if (ctrl.Name.ToString().StartsWith("tbHHPvYrMonth") == true || ctrl.Name.ToString().StartsWith("tbIndPvYrMonth") == true)
                            tbHHPvYrMonthList.Add((TextBox)ctrl);
                        else if (ctrl.Name.ToString().StartsWith("tbBPPvYrMonth") == true || ctrl.Name.ToString().StartsWith("tbBPIndPvYrMonth") == true)
                            tbHHPvYrMonthBPList.Add((TextBox)ctrl);
                        else if (ctrl.Name.ToString().StartsWith("tbHDPvYrMonth") == true || ctrl.Name.ToString().StartsWith("tbHDIndPvYrMonth") == true)
                            tbHHPvYrMonthHDList.Add((TextBox)ctrl);
                        else if (ctrl.Name.ToString().StartsWith("tbHHPvYrYTD") == true || ctrl.Name.ToString().StartsWith("tbIndPvYrYTD") == true)
                            tbHHPvYrYTDList.Add((TextBox)ctrl);
                        else if (ctrl.Name.ToString().StartsWith("tbHHPvY2Month") == true || ctrl.Name.ToString().StartsWith("tbIndPvY2Month") == true)
                            tbHHPvY2MonthList.Add((TextBox)ctrl);
                        else if (ctrl.Name.ToString().StartsWith("tbHHPvY2YTD") == true || ctrl.Name.ToString().StartsWith("tbIndPvY2YTD") == true)
                            tbHHPvY2YTDList.Add((TextBox)ctrl);
                        else if (ctrl.Name.ToString().StartsWith("tbLbsRcvdPer") == true)
                            tbLbsRcvdPerList.Add((TextBox)ctrl);
                        else if (ctrl.Name.ToString().StartsWith("tbLbsRcvdCum") == true)
                            tbLbsRcvdCumList.Add((TextBox)ctrl);
                        else if (ctrl.Name.ToString().StartsWith("tbLbsSvdCurMonth") == true)
                            tbLbsSvdCurMonthList.Add((TextBox)ctrl);
                        else if (ctrl.Name.ToString().StartsWith("tbLbsSvdPvYrMonth") == true)
                            tbLbsSvdPvYrMonthList.Add((TextBox)ctrl);
                        else if (ctrl.Name.ToString().StartsWith("tbLbsSvdPvY2Month") == true)
                            tbLbsSvdPvY2MonthList.Add((TextBox)ctrl);
                        else if (ctrl.Name.ToString().StartsWith("tbLbsSvdCurYTD") == true)
                            tbLbsSvdCurYTDList.Add((TextBox)ctrl);
                        else if (ctrl.Name.ToString().StartsWith("tbLbsSvdPvYrYTD") == true)
                            tbLbsSvdPvYrYTDList.Add((TextBox)ctrl);
                        else if (ctrl.Name.ToString().StartsWith("tbLbsSvdPvY2YTD") == true)
                            tbLbsSvdPvY2YTDList.Add((TextBox)ctrl);
                        else if (ctrl.Name.ToString().StartsWith("tbVolMonth") == true)
                            tbVolMonthList.Add((TextBox)ctrl);
                        else if (ctrl.Name.ToString().StartsWith("tbVolYTD") == true)
                            tbVolYTDList.Add((TextBox)ctrl);
                    }
                }
                traverseAndAddControlsToCollections(ctrl.Controls);
            }
        }

        private string getTrxLogDataSQL(string typeGroup, string StartDate, string EndDate)
        {
            string nbrDigits = "6";
            if (typeGroup == "Date")
                nbrDigits = "8";
            string sql = "SELECT LEFT(CONVERT(varchar(10),trxdate,112)," + nbrDigits + ") SvcDate"
                       + ", SUM(infants) NbrInfants"
                       + ", SUM(youth) NbrYouth"
                       + ", SUM(teens) NbrTeens"
                       + ", SUM(eighteen) NbrEighteen"
                       + ", SUM(adults) NbrAdults"
                       + ", SUM(Seniors) NbrSeniors"
                       + ", SUM(totalFamily) NbrTotalFamily"
                       + ", ROUND(CAST(SUM(TotalFamily) as float)/CAST(COUNT(*) as float),2) AvgHHSize"
                       + ", SUM(cast(RcvdCommodity as int)) NbrRcvdCommodity"
                       + ", SUM(cast(RcvdSupplemental as int)) NbrRcvdSupplemental"
                       + ", COUNT(*) NbrServed"
                       + ", SUM(lbsStd) StdLbs"
                       + ", SUM(lbsOther) OtherLbs"
                       + ", SUM(lbsCommodity) TEFAPLbs"
                       + ", SUM(lbsSupplemental) SupplementalLbs"
                       + ", SUM(lbsBabySvc) BabySvcLbs"
                       + ", SUM(LbsStd + LbsOther + LbsCommodity + LbsSupplemental + LbsBabySvc) TotalLbs"
                       + ", SUM(LbsStd + LbsOther + LbsCommodity + LbsSupplemental + LbsBabySvc)/SUM(TotalFamily) LbsPerInd"
                       + ", SUM(LbsStd + LbsOther + LbsCommodity + LbsSupplemental + LbsBabySvc)/Count(*) LbsPerSvc"
                       + "  FROM TrxLog tl"
                       + " WHERE trxdate between '" + StartDate + "' and '" + EndDate + "'"
                       + " GROUP BY LEFT(CONVERT(varchar(10),trxdate,112)," + nbrDigits + ")"
                       + " ORDER BY LEFT(CONVERT(varchar(10),trxdate,112)," + nbrDigits + ")";
            return sql;
        }
        private string getFoodReceiptsByDonorSQL(string PeriodStart, string PeriodEnd) //, string FiscalStart)
        { 
            string sql = "SELECT 'Period' as RcdType, p.FldName, d.ID AS Donor, d.Name"
                       + ", Sum(CASE WHEN fd.Pounds IS NULL THEN 0 ELSE fd.Pounds END) AS LBS"
                       + "  FROM Preferences p "
                       + "  LEFT JOIN Donors d ON CAST(p.FldVal AS int) = d.ID "
                       + "  LEFT JOIN foodDonations fd ON d.ID = fd.DonorId AND fd.TrxDate Between '" + PeriodStart + "' AND '" + PeriodEnd + "'"
                       + " WHERE LEFT(p.FldName,7) = 'DonorId' AND p.FldVal >'0'"
                       + " GROUP BY FldName, d.ID, d.Name"
                       + " UNION " + Environment.NewLine
                       + "SELECT 'Period' as RcdType, Min('Other') AS FldName, MIN(99999) AS Donor, 'OTHER' AS Name, Sum(Pounds)  AS LBS"
                       + "  FROM foodDonations fd"
                       + " WHERE fd.DonorId NOT IN(SELECT CAST(FldVal as Int) FROM Preferences WHERE LEFT(FldName,7) = 'DonorId')"
                       + "   AND TrxDate Between '" + PeriodStart + "' AND '" + PeriodEnd + "'"
                       //+ " UNION " + Environment.NewLine
                       //+ "SELECT 'Cum' as RcdType, p.FldName, d.ID AS Donor, d.Name, Sum(CASE WHEN fd.Pounds IS NULL THEN 0 ELSE fd.Pounds END)  AS LBS"
                       //+ "  FROM Preferences p "
                       //+ "  LEFT JOIN Donors d ON CAST(p.FldVal AS int) = d.ID "
                       //+ "  LEFT JOIN foodDonations fd ON d.ID = fd.DonorId AND fd.TrxDate Between '" + FiscalStart + "' AND '" + PeriodEnd + "'"
                       //+ " WHERE LEFT(p.FldName,7) = 'DonorId' AND p.FldVal >'0'"
                       //+ " GROUP BY FldName, d.Id, d.Name"
                       //+ " UNION " + Environment.NewLine
                       //+ "SELECT 'Cum' as RcdType, Min('Other') AS FldName, MIN(99999) AS Donor, 'OTHER' AS Name, Sum(Pounds)  AS LBS"
                       //+ "  FROM foodDonations fd"
                       //+ " WHERE fd.DonorId NOT IN(SELECT CAST(FldVal as Int) FROM Preferences WHERE LEFT(FldName,7) = 'DonorId')"
                       //+ "   AND TrxDate Between '" + FiscalStart + "' AND '" + PeriodEnd + "'" + Environment.NewLine
                       + " ORDER BY FldName, RcdType Desc";
            return sql;
        }

        private string getFoodReceiptsByFundingSQL(string PeriodEnd, string FiscalStart)
        {
            try 
            {
                string sql = "SELECT SvcDate"
                           + ", sum(CAST(CASE WHEN [1] IS NULL THEN 0 else [1] END AS INT)) AS LbsEFAP"
                           + ", sum(CAST(CASE WHEN [2] IS NULL THEN 0 else [2] END AS INT)) AS LbsTEFAP"
                           + ", sum(CAST(CASE WHEN [3] IS NULL THEN 0 else [3] END AS INT)) AS LbsOtherPurchased"
                           + ", sum(CAST(CASE WHEN [4] IS NULL THEN 0 else [4] END AS INT)) AS LbsInkind"
                           + ", sum(CAST(CASE WHEN [5] IS NULL THEN 0 else [5] END AS INT)) AS LbsCSFP"
                           + ", sum(CAST(CASE WHEN [6] IS NULL THEN 0 else [6] END AS INT)) AS LbsGroceryRescue"
                           + ", sum(CAST(CASE WHEN [7] IS NULL THEN 0 else [7] END AS INT)) AS LbsFLL"
                           + ", sum(CAST(CASE WHEN [8] IS NULL THEN 0 else [8] END AS INT)) AS LbsWaste"
                           + ", sum(CAST(CASE WHEN [9] IS NULL THEN 0 else [9] END AS INT)) AS LbsFeed"
                           + ", sum(CAST(CASE WHEN [10] IS NULL THEN 0 else [10] END AS INT)) AS LbsShared"
                           + " FROM "
                           + "(SELECT left(Convert(varchar(10),TrxDate,112),6) SvcDate, DonationType, Pounds"
                           + " FROM FoodDonations ) fd"
                           + " PIVOT ( SUM (Pounds)"
                           + " FOR DonationType IN ( [1], [2], [3], [4],[5],[6], [7], [8], [9], [10]) ) AS pvt"
                           + " WHERE SvcDate = '" + PeriodEnd.Substring(0, 6) + "'"
                           + " Group By SvcDate "
                           + "UNION "
                           + "SELECT dbo.GetFiscalYear(left(SvcDate,6)) + '99'"
                           + ", sum(CAST(CASE WHEN [1] IS NULL THEN 0 else [1] END AS INT)) AS LbsEFAP"
                           + ", sum(CAST(CASE WHEN [2] IS NULL THEN 0 else [2] END AS INT)) AS LbsTEFAP"
                           + ", sum(CAST(CASE WHEN [3] IS NULL THEN 0 else [3] END AS INT)) AS LbsOtherPurchased"
                           + ", sum(CAST(CASE WHEN [4] IS NULL THEN 0 else [4] END AS INT)) AS LbsInkind"
                           + ", sum(CAST(CASE WHEN [5] IS NULL THEN 0 else [5] END AS INT)) AS LbsCSFP"
                           + ", sum(CAST(CASE WHEN [6] IS NULL THEN 0 else [6] END AS INT)) AS LbsGroceryRescue"
                           + ", sum(CAST(CASE WHEN [7] IS NULL THEN 0 else [7] END AS INT)) AS LbsFLL"
                           + ", sum(CAST(CASE WHEN [8] IS NULL THEN 0 else [8] END AS INT)) AS LbsWaste"
                           + ", sum(CAST(CASE WHEN [9] IS NULL THEN 0 else [9] END AS INT)) AS LbsFeed"
                           + ", sum(CAST(CASE WHEN [10] IS NULL THEN 0 else [10] END AS INT)) AS LbsShared"
                           + " FROM "
                           + "(SELECT Convert(varchar(10),TrxDate,112) SvcDate, DonationType, Pounds"
                           + " FROM FoodDonations ) fd"
                           + " PIVOT ( SUM (Pounds)"
                           + " FOR DonationType IN ( [1], [2], [3], [4],[5],[6], [7], [8], [9], [10]) ) AS pvt"
                           + " WHERE SvcDate between '" + FiscalStart + "' AND '" + PeriodEnd + "'"
                           + " GROUP BY dbo.GetFiscalYear(left(SvcDate,6))";
                return sql;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Fiscal Start=" + FiscalStart + "\r\nPeriodEnd=" + PeriodEnd + "\r\n" + ex.Message);
                return "";
            }

        }

        private string  GetLoadFamilySizeSQL(string typeGroup, string StartDate, string EndDate)
        {
            string sql = "SELECT SvcDate"
                       + ", sum(CAST(CASE WHEN [1] IS NULL THEN 0 else [1] END AS INT)) AS One"
                       + ", sum(CAST(CASE WHEN [2] IS NULL THEN 0 else [2] END AS INT)) AS Two"
                       + ", sum(CAST(CASE WHEN [3] IS NULL THEN 0 else [3] END AS INT)) AS Three"
                       + ", sum(CAST(CASE WHEN [4] IS NULL THEN 0 else [4] END AS INT)) AS Four"
                       + ", sum(CAST(CASE WHEN [5] IS NULL THEN 0 else [5] END AS INT)) AS Five"
                       + ", sum(CAST(CASE WHEN [6] IS NULL THEN 0 else [6] END AS INT)) AS Six"
                       + ", sum(CAST(CASE WHEN [7] IS NULL THEN 0 else [7] END AS INT)) AS Seven"
                       + ", sum(CAST(CASE WHEN [8] IS NULL THEN 0 else [8] END AS INT)) AS Eight"
                       + ", sum(CAST(CASE WHEN [9] IS NULL THEN 0 else [9] END AS INT)) AS Nine"
                       + ", sum(CAST(CASE WHEN [10] IS NULL THEN 0 else [10] END AS INT)) AS Ten"
                       + ", sum(CAST(CASE WHEN [11] IS NULL THEN 0 else [11] END AS INT)) AS Eleven"
                       + ", sum(CAST(CASE WHEN [12] IS NULL THEN 0 else [12] END AS INT)) AS Twelve"
                       + ", sum(CAST(CASE WHEN [13] IS NULL THEN 0 else [13] END AS INT)) AS Thirteen"
                       + ", sum(CAST(CASE WHEN [14] IS NULL THEN 0 else [14] END AS INT)) AS Fourteen"
                       + ", sum(CAST(CASE WHEN [15] IS NULL THEN 0 else [15] END AS INT)) AS Fifteen"
                       + ", sum(CAST(CASE WHEN [16] IS NULL THEN 0 else [16] END AS INT)) AS Sixteen"
                       + ", sum(CAST(CASE WHEN [17] IS NULL THEN 0 else [17] END AS INT)) AS Seventeen"
                       + ", sum(CAST(CASE WHEN [18] IS NULL THEN 0 else [18] END AS INT)) AS Eighteen"
                       + ", sum(CAST(CASE WHEN [19] IS NULL THEN 0 else [19] END AS INT)) AS Nineteen"
                       + ", sum(CAST(CASE WHEN [20] IS NULL THEN 0 else [20] END AS INT)) AS Twenty"
                       + " FROM  ";
            if (typeGroup == "Date")
            {
                sql += "(SELECT Convert(varchar(10),TrxDate,112) SvcDate, TotalFamily, HouseholdId FROM TrxLog ) tl ";
            }
            else
            {
                sql += "(SELECT Left(Convert(varchar(10),TrxDate,112),6) SvcDate, TotalFamily, HouseholdId FROM TrxLog ) tl ";
            }
            sql += "PIVOT "
                 + "(Count(HouseholdId) FOR TotalFamily IN ( [1], [2], [3], [4],[5],[6], [7], [8], [9], [10], [11], [12], [13], [14], [15], [16], [17], [18], [19], [20]) ) AS pvt "
                 + "WHERE SvcDate between '" + StartDate + "' AND '" + EndDate + "' "
                 + "GROUP BY SvcDate "
                 + "ORDER BY SvcDate";
            return sql;
        }

        private DataTable TransferDataToLocalDataTable(SqlCommand sqlCmd)
        {
            DataTable dt = new DataTable();
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();        // Open the connection and execute the reader.

                SqlDataReader reader = sqlCmd.ExecuteReader();

                if (reader.HasRows)
                {
                    DataTable readerSchema = reader.GetSchemaTable().Copy();
                    for (int i = 0; i < readerSchema.Rows.Count; i++)
                    {
                        dt.Columns.Add(readerSchema.Rows[i]["ColumnName"].ToString());
                    }
                    while (reader.Read())
                    {
                        object[] values = new object[reader.FieldCount];
                        reader.GetValues(values);
                        for (int i = 0; i < values.Length; i++)
                        {
                            if (values[i] == DBNull.Value)
                                values[i] = 0;
                        }
                        
                        dt.Rows.Add(values);
                    }
                }
            }
            catch (SqlException ex)
            {
                CCFBGlobal.appendErrorToErrorReport(sqlCmd.CommandText, ex.GetBaseException().ToString());
            }

            if (conn.State == ConnectionState.Open)
                conn.Close();
            return dt;            
        }

        private void tabControl1_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState != FormWindowState.Minimized)
            {
                lvwFamilySizeBySvcDate.Width = tabControl1.Width - 8;
                lvwFamilySizeByMonth.Width = tabControl1.Width - 8;
            }
        }

        private void initTexBoxList(List<TextBox> tbList)
        {
            foreach (TextBox tb in tbList.OfType<TextBox>())
                { tb.Text = ""; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int tabIndex = tabControl1.SelectedIndex;
            string imageSavePath = CCFBGlobal.pathReports + "temp\\";
            CCFBGlobal.verifyPath(imageSavePath);
            CCFBGlobal.ClearFolder(imageSavePath);

            List<Panel> pnlList = new List<Panel>();
            //pnlList.Add(pnlHouseholdByMonth);
            pnlList.Add(pnlHouseholdYTD);
            pnlList.Add(pnlIndividualByMonth);
            pnlList.Add(pnlIndividualYTD);
            pnlList.Add(pnlLbsFoodServed);
            pnlList.Add(pnlLbsFoodReceipts);
            pnlList.Add(pnlFoodReceiptsByDonor);
            pnlList.Add(spltCFamilySize.Panel1);
            pnlList.Add(spltCFamilySize.Panel2);
            pnlList.Add(spltCStats.Panel1);
            pnlList.Add(spltCStats.Panel2);
            pnlList.Add(pnlStatistics);
            foreach(Panel pnl in pnlList)
            {
                if (pnl.Tag != null)
                {
                    switch (pnl.Tag.ToString())
                    {
                        case "hhMonth":
                            {
                                tabControl1.SelectTab(2);
                                tabControl2.SelectTab(0);
                                System.Windows.Forms.Application.DoEvents();
                                break;
                            }
                        case "hhYTD":
                            {
                                tabControl1.SelectTab(2);
                                tabControl2.SelectTab(1);
                                System.Windows.Forms.Application.DoEvents();
                                break;
                            }
                        case "IndividualByMonth":
                            {
                                tabControl1.SelectTab(3);
                                tabControl5.SelectTab(0);
                                System.Windows.Forms.Application.DoEvents();
                                break;
                            }
                        case "IndividualYTD":
                            {
                                tabControl1.SelectTab(3);
                                tabControl5.SelectTab(1);
                                System.Windows.Forms.Application.DoEvents();
                                break;
                            }
                        case "LbsFoodServed":
                        case "LbsFoodReceipts":
                            {
                                tabControl1.SelectTab(4);
                                tabControl3.SelectTab(0);
                                System.Windows.Forms.Application.DoEvents();
                                break;
                            }
                        case "FoodReceiptsByDonor":
                            {
                                tabControl1.SelectTab(4);
                                tabControl3.SelectTab(1);
                                System.Windows.Forms.Application.DoEvents();
                                break;
                            }
                        case "FamilySizeByDay":
                        case "FamilySizeByMonth":
                            {
                                tabControl1.SelectTab(5);
                                System.Windows.Forms.Application.DoEvents();
                                break;
                            }
                        case "StatsByDay":
                        case "StatsByMonth":
                            {
                                tabControl1.SelectTab(6);
                                System.Windows.Forms.Application.DoEvents();
                                break;
                            }
                        case "Statistics":
                            {
                                tabControl1.SelectTab(1);
                                System.Windows.Forms.Application.DoEvents();
                                break;
                            }
                    }
                    string save = imageSavePath + pnl.Tag.ToString();
                    PrintPanel print = new PrintPanel(pnl, save);
                    print.Print();
                }
            }

            tabControl1.SelectTab(tabIndex);
            MonthlyReports clsMR = new MonthlyReports(CCFBGlobal.connectionString);
            clsMR.open(10);
            //clsMonthlyReports.find(10);
            string saveAs = savePath + "\\" + cboReportYear.SelectedItem.ToString()
                + strRptMonth() + "_" +
                clsMR.ReportName + ".doc";


            RptMonthEndStats clsCreateCoalitionReport = new RptMonthEndStats(clsTrxLogCurFiscalYrStats);
            clsCreateCoalitionReport.createReport(tbFBName.Text, cboReportMonth.Text + " " + cboReportYear.SelectedText.ToString(),
                                        tbPreparedBy.Text, saveAs, clsMR.ReportPath, imageSavePath);
        }

        private void btnExportData_Click(object sender, EventArgs e)
        {
            string exportPath = CCFBGlobal.pathReports + "Exports\\" + CCFBPrefs.FoodBankName + "_" 
                + cboReportYear.SelectedItem.ToString()
                + "_" + (intRptMonth()).ToString();

            CCFBGlobal.verifyPath(exportPath);
//Need to fix this code
            //dtblMonthStats.TableName = "Month Stats";
            //dtblMonthStats.WriteXml(exportPath + @"\MonthStats.XML", XmlWriteMode.WriteSchema, true);
            //dtblMonthStats.TableName = "DonorData";
            //dtblMonthStats.WriteXml(exportPath + @"\DonorData.XML", XmlWriteMode.WriteSchema, true);

            using (FileStream fs = File.Create(exportPath + @"\header.txt"))
            {
                Byte[] info = new UTF8Encoding(true).GetBytes(CCFBPrefs.FoodBankName);
                fs.Write(info, 0, info.Length);
                info = new UTF8Encoding(true).GetBytes(System.Environment.NewLine);
                fs.Write(info, 0, info.Length);
                info = new UTF8Encoding(true).GetBytes(cboReportYear.SelectedItem.ToString());
                fs.Write(info, 0, info.Length);
                info = new UTF8Encoding(true).GetBytes(System.Environment.NewLine);
                fs.Write(info, 0, info.Length);
                info = new UTF8Encoding(true).GetBytes(CCFBGlobal.formatNumberWithLeadingZero(intRptMonth()));
                fs.Write(info, 0, info.Length);
            }

            using (ZipFile zip = new ZipFile())
            {
                zip.AddDirectory(exportPath, "");
                //zip.AddFile( "MonthStats.XML");
                //zip.AddFile(exportPath + @"\DonorData");
                //zip.AddFile("header.txt");
                zip.Save(exportPath + @"\" + CCFBPrefs.FoodBankName + "_" + cboReportYear.SelectedItem.ToString() 
                    + "_" + (intRptMonth()).ToString() + ".zip");
            }
        }

        private void fillFoodRecieptsByFunding(DateTime dateFiscalStart, DateTime datePeriodLast)
        {
            lblLoadFoodRecieptsByFunding.BackColor = Color.PaleGoldenrod;
            System.Windows.Forms.Application.DoEvents();
            string dateStart = CCFBGlobal.formatDate(dateFiscalStart);
            string dateEnd = CCFBGlobal.formatDate(datePeriodLast);
            sqlCmd = new SqlCommand(getFoodReceiptsByFundingSQL(dateEnd.Substring(6, 4) + dateEnd.Substring(0, 2) + dateEnd.Substring(3, 2), dateStart.Substring(6, 4) + dateStart.Substring(0, 2) + dateStart.Substring(3, 2)), conn);
            sqlCmd.CommandType = CommandType.Text;
            dtblFoodRecieptsByFunding = TransferDataToLocalDataTable(sqlCmd);
            lblLoadFoodRecieptsByFunding.BackColor = Color.PaleGreen;
            //tbDollarsPerLbs.Tag = 0;
            //tbDollarsCumLbs.Tag = 0;
            if (dtblFoodRecieptsByFunding.Rows.Count > 0)
            {
                decimal colSum = 0;
                decimal colMinus = 0;
//                decimal lbsInkind = 0;
                string tagVal = "";
                if (dtblFoodRecieptsByFunding.Rows[0]["SvcDate"].ToString().Substring(4, 2) != "99")
                {
                    foreach (TextBox tb in tbLbsRcvdPerList.OfType<TextBox>())
                    {
                        if (tb.Tag.ToString() != "")
                        {
                            tagVal = tb.Tag.ToString();
                            if (dtblFoodRecieptsByFunding.Columns.IndexOf(tagVal) >= 0)
                            {
                                if (dtblFoodRecieptsByFunding.Rows[0][tagVal].ToString() == "")
                                {
                                    tb.Text = "0";
                                }
                                else
                                {
                                    tb.Text = Convert.ToDecimal(dtblFoodRecieptsByFunding.Rows[0][tagVal]).ToString("N00");
                                    if (tagVal == "LbsFeed" || tagVal == "LbsShared" || tagVal == "LbsWaste")
                                        colMinus += Convert.ToDecimal(dtblFoodRecieptsByFunding.Rows[0][tagVal]);
                                    else
                                        colSum += Convert.ToDecimal(dtblFoodRecieptsByFunding.Rows[0][tagVal]);
                                }
                                //if (tagVal == "LbsInkind" || tagVal == "LbsGroceryRescue")
                                //{
                                //    lbsInkind += Convert.ToDecimal(dtblFoodRecieptsByFunding.Rows[0][tagVal]);
                                //}
                            }
                        }
//                        rowIndex = 1;
                    }
                }
                tbLbsRcvdPerTotal.Text = colSum.ToString("N00");
                tbLbsRcvdPerRemoved.Text = colMinus.ToString("N00");
                tbLbsRcvdPerNet.Text = (colSum + colMinus).ToString("N00");
                //if (lbsInkind > 0)
                //{
                //    //tbInkindLbsPer.Text = lbsInkind.ToString("N00");
                //    tbDollarsPerLbs.Tag = (lbsInkind * CCFBPrefs.InkindDollarsPerLb);
                //    tbDollarsPerLbs.Text = (lbsInkind * CCFBPrefs.InkindDollarsPerLb).ToString("C0");
                //}
                //else
                //{
                //    tbDollarsPerLbs.Tag = 0;
                //    tbDollarsPerLbs.Text = "0";
                //}
                colSum = 0;
                colMinus = 0;
//                lbsInkind = 0;
                int rowCum = 1;
                if (dtblFoodRecieptsByFunding.Rows[0]["SvcDate"].ToString().Substring(4, 2) == "99")
                { rowCum = 0; }
                if (dtblFoodRecieptsByFunding.Rows.Count > 0)
                {
                    foreach (TextBox tb in tbLbsRcvdCumList.OfType<TextBox>())
                    {
                        if (tb.Tag.ToString() != "")
                        {
                            tagVal = tb.Tag.ToString();
                            if (dtblFoodRecieptsByFunding.Columns.IndexOf(tagVal) >= 0)
                            {
                                if (dtblFoodRecieptsByFunding.Rows[rowCum][tagVal].ToString() == "")
                                {
                                    tb.Text = "0";
                                }
                                else
                                {
                                    tb.Text = Convert.ToDecimal(dtblFoodRecieptsByFunding.Rows[rowCum][tagVal]).ToString("N00");
                                    if (tagVal == "LbsFeed" || tagVal == "LbsShared" || tagVal == "LbsWaste")
                                        colMinus += Convert.ToDecimal(dtblFoodRecieptsByFunding.Rows[rowCum][tagVal]);
                                    else
                                        colSum += Convert.ToDecimal(dtblFoodRecieptsByFunding.Rows[rowCum][tagVal]);
                                }
                                //if (tagVal == "LbsInkind" || tagVal == "LbsGroceryRescue")
                                //{
                                //    lbsInkind += Convert.ToDecimal(dtblFoodRecieptsByFunding.Rows[1][tagVal]);
                                //}
                            }
                        }
                    }
                }
                tbLbsRcvdCumTotal.Text = colSum.ToString("N00");
                tbLbsRcvdCumRemoved.Text = colMinus.ToString("N00");
                tbLbsRcvdCumNet.Text = (colSum + colMinus).ToString("N00");
                //if (lbsInkind > 0)
                //{
                //    //tbInkindLbsCum.Text = lbsInkind.ToString("N00");
                //    tbDollarsCumLbs.Tag = (lbsInkind * CCFBPrefs.InkindDollarsPerLb);
                //    tbDollarsCumLbs.Text = (lbsInkind * CCFBPrefs.InkindDollarsPerLb).ToString("C0");
                //}
                //else
                //{
                //    tbDollarsCumLbs.Tag = 0;
                //    tbDollarsCumLbs.Text = "0";
                //}

            }

            //else
            //{
            //    tbDollarsPerLbs.Tag = 0;
            //    tbDollarsPerLbs.Text = "0";
            //    tbDollarsCumLbs.Tag = 0;
            //    tbDollarsCumLbs.Text = "0";
            //}
            System.Windows.Forms.Application.DoEvents();
        }

        private void filllvwLbsFoodByDonor(DateTime datePeriodFirst, DateTime datePeriodLast, ref ListView lvw, ref DataTable dtbl, ref Chart chrtPercent, string title)
        {
            chrtPercent.Series[0].Points.Clear();
            Series series1 = chrtPercent.Series[0];
            chrtPercent.Titles[0].Text = title;
            lblLoadFoodRecieptsByDonor.BackColor = Color.PaleGoldenrod;
            System.Windows.Forms.Application.DoEvents();
            sqlCmd = new SqlCommand(getFoodReceiptsByDonorSQL(datePeriodFirst.ToShortDateString(), datePeriodLast.ToShortDateString()), conn);
            sqlCmd.CommandType = CommandType.Text;
            dtbl = TransferDataToLocalDataTable(sqlCmd);
            lblLoadFoodRecieptsByDonor.BackColor = Color.PaleGreen;

            if (dtbl.Rows.Count > 0)
            {
                decimal decPerVal = 0;
                decimal decPerTot = 0;
                int i = 0;
                DataRow drow1;

                while (i < dtbl.Rows.Count)
                {
                    drow1 = dtbl.Rows[i];
                    i++;
                    decPerVal = 0;
                    if (drow1["LBS"] != DBNull.Value)
                    {
                        decPerVal = Convert.ToDecimal(drow1["LBS"]);
                        decPerTot += decPerVal;
                    }
                    ListViewItem lvItm = new ListViewItem("");
                    lvItm.SubItems.Add(decPerVal.ToString("N00"));
                    lvItm.SubItems.Add("..");
                    lvItm.SubItems.Add(drow1["Name"].ToString());
                    lvItm.Tag = drow1["Donor"].ToString();
                    lvw.Items.Add(lvItm);
                    addPiePoint(ref series1, Convert.ToDouble(decPerVal), drow1["Name"].ToString(), false, Color.Transparent);
                }
                ListViewItem lvi = new ListViewItem("");
                lvi.SubItems.Add(decPerTot.ToString("N00"));
                lvi.SubItems.Add("100.0 %");
                lvi.SubItems.Add("Totals");
                lvi.BackColor = Color.PaleGoldenrod;
                lvw.Items.Add(lvi);
                foreach (ListViewItem item in lvw.Items)
                {
                    if (item.SubItems[3].Text != "Totals")
                    {
                        if (decPerTot != 0)
                        {
                            decPerVal = Convert.ToDecimal(item.SubItems[1].Text);
                            item.SubItems[2].Text = (decPerVal / decPerTot).ToString("P1");
                            if (item.Tag.ToString() == CCFBPrefs.DonorIDNWH.ToString())
                            {
                                lbsNWH = item.SubItems[1].Text;
                                percentNWH = item.SubItems[2].Text;
                            }
                            else if (item.Tag.ToString() == CCFBPrefs.DonorID2ndHarvest.ToString())
                            {
                                lbs2ndHarvest = item.SubItems[1].Text;
                            }
                            else if (item.Tag.ToString() == CCFBPrefs.DonorIDEFAP.ToString())
                            {
                                lbsEFAP = item.SubItems[1].Text;
                            }
                            else if (item.Tag.ToString() == CCFBPrefs.DonorIDTEFAP.ToString())
                            {
                                lbsTEFAP = item.SubItems[1].Text;
                            }
                        }
                        else
                            item.SubItems[2].Text = "";
                    }
                }
            }
        }

        private void filllvwLbsFoodByDonorx(DateTime datePeriodFirst, DateTime datePeriodLast, DateTime dateFiscalStart)
        {
            chartDonorMonth.Series[0].Points.Clear();
            Series series1 = chartDonorMonth.Series[0];
            chartDonorMonth.Titles[0].Text = datePeriodFirst.ToString("MMMM") + " " + datePeriodFirst.Year.ToString();
            lblLoadFoodRecieptsByDonor.BackColor = Color.PaleGoldenrod;
            System.Windows.Forms.Application.DoEvents();
           // sqlCmd = new SqlCommand(getFoodReceiptsByDonorSQL(datePeriodFirst.ToShortDateString(), datePeriodLast.ToShortDateString(), dateFiscalStart.ToShortDateString()), conn);
            sqlCmd.CommandType = CommandType.Text;
            dtblFoodRecieptsByDonor = TransferDataToLocalDataTable(sqlCmd);
            lblLoadFoodRecieptsByDonor.BackColor = Color.PaleGreen;

            if (dtblFoodRecieptsByDonor.Rows.Count > 0)
            {
                decimal decPerVal = 0;
                decimal decCumVal = 0;
                decimal decPerTot = 0;
                decimal decCumTot = 0;
                int i = 0;
                DataRow drow1;
                DataRow drow2;
                while (i < dtblFoodRecieptsByDonor.Rows.Count)
                {
                    drow1 = dtblFoodRecieptsByDonor.Rows[i];
                    i++;
                    drow2 = dtblFoodRecieptsByDonor.Rows[i];
                    i++;
                    decPerVal = 0;
                    decCumVal = 0;
                    if (drow1["LBS"] != DBNull.Value)
                    {
                        if (drow1[0].ToString() == "Period")
                        {
                            decPerVal = Convert.ToDecimal(drow1["LBS"]);
                            decPerTot += decPerVal;
                        }
                        else
                        {
                            decCumVal = Convert.ToDecimal(drow1["LBS"]);
                            decCumTot += decCumVal;
                        }
                    }
                    if (drow2["LBS"] != DBNull.Value)
                    {
                        if (drow2[0].ToString() == "Period")
                        {
                            decPerVal = Convert.ToDecimal(drow2["LBS"]);
                            decPerTot += decPerVal;
                        }
                        else
                        {
                            decCumVal = Convert.ToDecimal(drow2["LBS"]);
                            decCumTot += decCumVal;
                        }
                    }
                    ListViewItem lvItm = new ListViewItem("");
                    lvItm.SubItems.Add(decPerVal.ToString("N00"));
                    lvItm.SubItems.Add("..");
                    lvItm.SubItems.Add(decCumVal.ToString("N00"));
                    lvItm.SubItems.Add("..");
                    lvItm.SubItems.Add(drow1["Name"].ToString());
                    lvItm.Tag = drow1["Donor"].ToString();
                    lvwLbsFoodByDonor.Items.Add(lvItm);
                    addPiePoint(ref series1, Convert.ToDouble(decPerVal), drow1["Name"].ToString(), false, Color.Transparent);
                }
                ListViewItem lvi = new ListViewItem("");
                lvi.SubItems.Add(decPerTot.ToString("N00"));
                lvi.SubItems.Add("100.0 %");
                lvi.SubItems.Add(decCumTot.ToString("N00"));
                lvi.SubItems.Add("100.0 %");
                lvi.SubItems.Add("Totals");
                lvi.BackColor = Color.PaleGoldenrod;
                lvwLbsFoodByDonor.Items.Add(lvi);
                foreach (ListViewItem item in lvwLbsFoodByDonor.Items)
                {
                    if (item.SubItems[5].Text != "Totals")
                    {
                        if (decPerTot != 0)
                        {
                            decPerVal = Convert.ToDecimal(item.SubItems[1].Text);
                            item.SubItems[2].Text = (decPerVal / decPerTot).ToString("P1");
                            if (item.Tag.ToString() == CCFBPrefs.DonorIDNWH.ToString())
                            {
                                percentNWH = item.SubItems[2].Text;
                            }
                        }
                        else
                            item.SubItems[2].Text = "";
                        if (decCumTot != 0)
                        {
                            decCumVal = Convert.ToDecimal(item.SubItems[3].Text);
                            item.SubItems[4].Text = (decCumVal / decCumTot).ToString("P1");
                        }
                        else
                            item.SubItems[4].Text = "";
                    }
                }
            }
        }


        private void fillMonthEndChange(TrxLogPeriodTotals clsCurrent, TrxLogPeriodTotals clsPrev, List<TextBox> tblist)
        {
            int iValue = 0;
            foreach (TextBox tb in tblist)
            {
                if (tb.Tag.ToString() != "")
                {
                    iValue = Convert.ToInt32(clsCurrent.GetDataValue(tb.Tag.ToString())) - Convert.ToInt32(clsPrev.GetDataValue(tb.Tag.ToString()));
                    tb.Text = iValue.ToString();
                }
            }
        }

        private TrxLogPeriodTotals fillMonthEndStats(List<TextBox> tbMonthList, List<TextBox> tbHHYTDList, List<TextBox> tbLbsSvdMonthList, List<TextBox> tbLbsSvdYTDList, DateTime dateFiscalStart, DateTime datePeriodLast, string sFiscalPeriod, int seriesIndex, Label lblHHYTD, Label lblIndYTD, Label lblLbsSvdMonth, Label lblLbsSvdYTD, bool spStd)
        {
            int offset = 0;
            lblLoadStatistics.BackColor = Color.PaleGoldenrod;
            if (lblHHYTD != null)
            {
                lblHHYTD.Text = "Fiscal " + sFiscalPeriod.Substring(0, 4) + " YTD";
            }
            if (lblIndYTD != null)
            {
                lblIndYTD.Text = "Fiscal " + sFiscalPeriod.Substring(0, 4) + " YTD";
            }
            if (lblLbsSvdMonth != null)
            {
                lblLbsSvdMonth.Text = sFiscalPeriod.Substring(0, 4);
            }
            if (lblLbsSvdYTD != null)
            {
                lblLbsSvdYTD.Text = lblLbsSvdMonth.Text;
            }
            System.Windows.Forms.Application.DoEvents();
            TrxLogPeriodTotals clsTrxLogStats = new TrxLogPeriodTotals(CCFBGlobal.connectionString);
            if (spStd == true)
            {
                clsTrxLogStats.open(dateFiscalStart.ToShortDateString(), datePeriodLast.ToShortDateString());
            }
            else
            {
                clsTrxLogStats.openHD(dateFiscalStart.ToShortDateString(), datePeriodLast.ToShortDateString());
            }
            lblLoadStatistics.BackColor = Color.PaleGreen;
            if (clsTrxLogStats.RowCount > 0)
            {
                if (tbMonthList != null)
                {
                    clsTrxLogStats.findFiscalPeriod(sFiscalPeriod);
                    foreach (TextBox tb in tbMonthList.OfType<TextBox>())
                    {
                        if (tb.Tag.ToString() != "")
                        {
                            tb.Text = Convert.ToDecimal(clsTrxLogStats.GetDataValue(tb.Tag.ToString())).ToString("N00");
                        }
                    }
                }
                if (tbLbsSvdMonthList != null)
                {
                    foreach (TextBox tb in tbLbsSvdMonthList.OfType<TextBox>())
                    {
                        if (tb.Tag.ToString() != "")
                        {
                            tb.Text = Convert.ToDecimal(clsTrxLogStats.GetDataValue(tb.Tag.ToString())).ToString("N00");
                        }
                    }
                }
                if (tbHHYTDList != null && tbLbsSvdYTDList != null)
                {
                    clsTrxLogStats.setYTDRow();
                    foreach (TextBox tb in tbHHYTDList.OfType<TextBox>())
                    {
                        if (tb.Tag.ToString() != "")
                        {
                            tb.Text = Convert.ToDecimal(clsTrxLogStats.GetDataValue(tb.Tag.ToString())).ToString("N00");
                        }
                    }
                    foreach (TextBox tb in tbLbsSvdYTDList.OfType<TextBox>())
                    {
                        if (tb.Tag.ToString() != "")
                        {
                            tb.Text = Convert.ToDecimal(clsTrxLogStats.GetDataValue(tb.Tag.ToString())).ToString("N00");
                        }
                    }
                }
            }
                // Charts
                //if (seriesIndex < chartHHYTD.Series.Count)
                //{
                //    chartHHYTD.Series[seriesIndex].Points.Clear();
                //}
                //else
                //{
                //    chartHHYTD.Series.Add("Prev" + seriesIndex.ToString());
                //}
            if (seriesIndex >= 0)
            {
                int ytd = 0;
                int lastValue = 0;
                int index = 0;
                int idxquarter = 0;
                offset = seriesIndex * 12;
                for (int i = 0; i < 12; i++)
                {
                    index = i + offset;
                    if (clsTrxLogStats.findFiscalPeriod(i + 1) == true)
                    //if (i < clsTrxLogStats.RowCount && clsTrxLogStats.findFiscalPeriod(i + 1) == true)
                    {
                        ytd += clsTrxLogStats.HHTotalServed;
                        chartHHYTD.Series[seriesIndex].Points[i].YValues[0] = ytd;
                        chartHHYTD.Series[seriesIndex].Points[i].ToolTip = ytd.ToString();
                        chartHHYTD.Series[seriesIndex].Points[i].BorderDashStyle = ChartDashStyle.Solid;
                        chartHHYTD.Series[seriesIndex].Points[i].BorderWidth = seriesIndex + 1;

                        chartHHServed.Series[0].Points[index].YValues[0] = clsTrxLogStats.HHTotalServed;
                        chartHHServed.Series[0].Points[index].ToolTip = clsTrxLogStats.HHTotalServed.ToString();
                        chartHHServed.ChartAreas[0].Axes[0].CustomLabels[index].Text = dateFiscalStart.AddMonths(i).ToString("m").Substring(0, 3);
                        chartHHServed.ChartAreas[0].Axes[0].CustomLabels[index].FromPosition = index + .5;
                        chartHHServed.ChartAreas[0].Axes[0].CustomLabels[index].ToPosition = index + 1.5;
                        if (index % 3 == 0)
                        {
                            idxquarter = index / 3;
                            chartHHServed.ChartAreas[0].Axes[0].CustomLabels[idxquarter + 36].Text = dateFiscalStart.AddMonths(i).ToString("yyyy");
                            chartHHServed.ChartAreas[0].Axes[0].CustomLabels[idxquarter + 36].FromPosition = index + .25;
                            chartHHServed.ChartAreas[0].Axes[0].CustomLabels[idxquarter + 36].ToPosition = index + 1.75;
                        }
                        chartHHServed.Series[0].Points[index].MarkerStyle = MarkerStyle.Diamond;
                        chartHHServed.Series[0].Points[index].BorderDashStyle = ChartDashStyle.Solid;
                        chartHHServed.Series[0].Points[index].BorderWidth = 2;
                        lastValue = clsTrxLogStats.HHTotalServed;
                    }
                    else
                    {
                        chartHHYTD.Series[seriesIndex].Points[i].YValues[0] = ytd;
                        chartHHYTD.Series[seriesIndex].Points[i].MarkerStyle = MarkerStyle.None;
                        chartHHYTD.Series[seriesIndex].Points[i].BorderDashStyle = ChartDashStyle.Dot;
                        chartHHYTD.Series[seriesIndex].Points[i].BorderWidth = 1;
                        chartHHServed.Series[0].Points[index].YValues[0] = lastValue;
                        chartHHServed.Series[0].Points[index].MarkerStyle = MarkerStyle.None;
                        chartHHServed.Series[0].Points[index].BorderDashStyle = ChartDashStyle.Dot;
                        chartHHServed.Series[0].Points[index].BorderWidth = 1;

                    }
                    chartHHServed.Series[0].Points[index].IsValueShownAsLabel = false;
                }
                string yr = sFiscalPeriod.Substring(0, 4);
                chartHHYTD.Series[seriesIndex].Points[11].IsValueShownAsLabel = true;
                chartHHYTD.Series[seriesIndex].LegendText = yr;

                fillByMonthCharts(clsTrxLogStats, chartHHByMonth, seriesIndex, "HHTotalServed", yr);
                fillByMonthCharts(clsTrxLogStats, chartHHCommodities, seriesIndex, "HHRcvdCommodity", yr);
                fillByMonthCharts(clsTrxLogStats, chartHHSupplemental, seriesIndex, "HHRcvdSupplemental", yr);
                fillByMonthCharts(clsTrxLogStats, chartHHBabySvcs, seriesIndex, "HHRcvdBabyServices", yr);
                fillByMonthCharts(clsTrxLogStats, chartHHHomeless, seriesIndex, "HHHomeless", yr);
                fillByMonthCharts(clsTrxLogStats, chartHHTransient, seriesIndex, "HHTransient", yr);
                fillByMonthCharts(clsTrxLogStats, chartHHInCityLimits, seriesIndex, "HHInCityLimits", yr);
            }
            return clsTrxLogStats;
        }

        private void fillGeneralStats(ref ListView lvw,ref DataTable dtblNbrSvcDays, ref DataTable dtblInkindLbs, DateTime datePeriodFirst, DateTime datePeriodLast, DateTime dateFiscalStart, Label lblGenFiscalPeriod)
        {
            int sumVolunteers = 0;
            lvw.Items.Clear();
            lblGenFiscalPeriod.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(dateFiscalStart.Month) + " " + dateFiscalStart.Year.ToString() + " - " + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(datePeriodFirst.Month) + " " + datePeriodFirst.Year.ToString();
            clsVolStats = new VolunteerStats(CCFBGlobal.connectionString);
            clsVolStats.open(dateFiscalStart.ToShortDateString(), datePeriodLast.ToShortDateString());

            sqlCmd = new SqlCommand(getNbrServiceDaysSQL(dateFiscalStart.ToShortDateString(), datePeriodLast.ToShortDateString()), conn);
            sqlCmd.CommandType = CommandType.Text;
            dtblNbrSvcDays = TransferDataToLocalDataTable(sqlCmd);

            sqlCmd = new SqlCommand(getInkindLbsSQL(dateFiscalStart.ToShortDateString(), datePeriodLast.ToShortDateString()), conn);
            sqlCmd.CommandType = CommandType.Text;
            dtblInkindLbs = TransferDataToLocalDataTable(sqlCmd);

            if (dtblNbrSvcDays.Rows.Count > 0)
            {
                for (int i = 0; i < dtblNbrSvcDays.Rows.Count; i++)
                {
                    addlvwGenItem(lvw, dtblNbrSvcDays.Rows[i]);
                }
            }
            else
            {
            }
            if (clsVolStats.RowCount > 0)
            {
                for (int i = 0; i < lvw.Items.Count; i++)
                {
                    if (clsVolStats.findYearMonth(lvw.Items[i].Tag.ToString()) == true)
                    {
                        updatelvwGenVolData(lvw, i, clsVolStats.YearMonth, clsVolStats.NumVolHours, clsVolStats.NumVols);
                        if (clsVolStats.YearMonth.Substring(4,2) != "99")
                        {
                            sumVolunteers += clsVolStats.NumVols;
                        }
                    }
                }
            }
            if (dtblInkindLbs.Rows.Count > 0)
            {
                foreach (ListViewItem item in lvw.Items)
                {
                    updatelvwGenLbs(item, dtblInkindLbs, lvw.Items.Count);
                }
            }

            foreach (ListViewItem item in lvw.Items)
            {
                decimal dollarsinkind = 0;
                for (int i = 5; i < 8; i++)
                {
                    if (item.SubItems[i].Tag != null)
                    {
                        dollarsinkind += Convert.ToDecimal(item.SubItems[i].Tag);
                    }
                }
                if (dollarsinkind > 0)
                {
                    item.SubItems[8].Text = dollarsinkind.ToString("C00");
                }
            }
            if (lvw.Items.Count > 0)
            {
                int nbrRows = lvw.Items.Count - 1;
                ListViewItem lvnew = new ListViewItem("Monthly Average");
                lvnew.BackColor = Color.LightGray;
                lvnew.ForeColor = Color.Maroon;
                ListViewItem lvTotal = lvw.Items[lvw.Items.Count - 1];
                lvnew.SubItems.Add(lvItemCalcAvg(lvTotal.SubItems[1].Text, nbrRows));
                lvnew.SubItems.Add(lvItemCalcAvg(sumVolunteers.ToString(), nbrRows));
                lvnew.SubItems.Add(lvItemCalcAvg(lvTotal.SubItems[3].Text.Replace(",", ""), nbrRows));
                lvnew.SubItems.Add(lvItemCalcAvg(lvTotal.SubItems[4].Text.Replace(",", ""), nbrRows));
                lvw.Items.Add(lvnew);
            }
        }

        private void fillVoucherStats(ref ListView lvw, DateTime dtFiscalStart, DateTime dtPeriodStart)
        {
            ListViewItem lvi;
            ListViewGroup lvgrp = new ListViewGroup("");
            decimal rTotal = 0;
            int vType = 0;
            int curvType = -1;
            int nbrmnths = (((dtPeriodStart.Year - dtFiscalStart.Year) * 12) + dtPeriodStart.Month - dtFiscalStart.Month) + 1;
            string grpName = "";
            string sqlText = "SELECT vi.UID, vi.Description Descr, vi.VoucherType, vt.ShortName vunits, vt.Type vname"
                           + ", (SELECT Sum(Amount) FROM VoucherLog WHERE TrxDate between '" + dtFiscalStart.ToShortDateString() + "' AND '" + dtFiscalStart.AddMonths(1).AddDays(-1).ToShortDateString() + "' AND VoucherItemID = vi.UID)"
                           + ", (SELECT Sum(Amount) FROM VoucherLog WHERE TrxDate between '" + dtFiscalStart.AddMonths(1).ToShortDateString() + "' AND '" + dtFiscalStart.AddMonths(2).AddDays(-1).ToShortDateString() + "' AND VoucherItemID = vi.UID)"
                           + ", (SELECT Sum(Amount) FROM VoucherLog WHERE TrxDate between '" + dtFiscalStart.AddMonths(2).ToShortDateString() + "' AND '" + dtFiscalStart.AddMonths(3).AddDays(-1).ToShortDateString() + "' AND VoucherItemID = vi.UID)"
                           + ", (SELECT Sum(Amount) FROM VoucherLog WHERE TrxDate between '" + dtFiscalStart.AddMonths(3).ToShortDateString() + "' AND '" + dtFiscalStart.AddMonths(4).AddDays(-1).ToShortDateString() + "' AND VoucherItemID = vi.UID)"
                           + ", (SELECT Sum(Amount) FROM VoucherLog WHERE TrxDate between '" + dtFiscalStart.AddMonths(4).ToShortDateString() + "' AND '" + dtFiscalStart.AddMonths(5).AddDays(-1).ToShortDateString() + "' AND VoucherItemID = vi.UID)"
                           + ", (SELECT Sum(Amount) FROM VoucherLog WHERE TrxDate between '" + dtFiscalStart.AddMonths(5).ToShortDateString() + "' AND '" + dtFiscalStart.AddMonths(6).AddDays(-1).ToShortDateString() + "' AND VoucherItemID = vi.UID)"
                           + ", (SELECT Sum(Amount) FROM VoucherLog WHERE TrxDate between '" + dtFiscalStart.AddMonths(6).ToShortDateString() + "' AND '" + dtFiscalStart.AddMonths(7).AddDays(-1).ToShortDateString() + "' AND VoucherItemID = vi.UID)"
                           + ", (SELECT Sum(Amount) FROM VoucherLog WHERE TrxDate between '" + dtFiscalStart.AddMonths(7).ToShortDateString() + "' AND '" + dtFiscalStart.AddMonths(8).AddDays(-1).ToShortDateString() + "' AND VoucherItemID = vi.UID)"
                           + ", (SELECT Sum(Amount) FROM VoucherLog WHERE TrxDate between '" + dtFiscalStart.AddMonths(8).ToShortDateString() + "' AND '" + dtFiscalStart.AddMonths(9).AddDays(-1).ToShortDateString() + "' AND VoucherItemID = vi.UID)"
                           + ", (SELECT Sum(Amount) FROM VoucherLog WHERE TrxDate between '" + dtFiscalStart.AddMonths(9).ToShortDateString() + "' AND '" + dtFiscalStart.AddMonths(10).AddDays(-1).ToShortDateString() + "' AND VoucherItemID = vi.UID)"
                           + ", (SELECT Sum(Amount) FROM VoucherLog WHERE TrxDate between '" + dtFiscalStart.AddMonths(10).ToShortDateString() + "' AND '" + dtFiscalStart.AddMonths(11).AddDays(-1).ToShortDateString() + "' AND VoucherItemID = vi.UID)"
                           + ", (SELECT Sum(Amount) FROM VoucherLog WHERE TrxDate between '" + dtFiscalStart.AddMonths(11).ToShortDateString() + "' AND '" + dtFiscalStart.AddMonths(12).AddDays(-1).ToShortDateString() + "' AND VoucherItemID = vi.UID)"
                           + " FROM VoucherItems vi LEFT JOIN parm_VoucherType vt ON vi.VoucherType = vt.ID ORDER BY vt.SortOrder, Description";
            lvw.Items.Clear();
            
            lblVoucherPeriod.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(dtFiscalStart.Month) + " " + dtFiscalStart.Year.ToString() + " - " + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(dtPeriodStart.Month) + " " + dtPeriodStart.Year.ToString();

            
            sqlCmd = new SqlCommand(sqlText, conn);
            sqlCmd.CommandType = CommandType.Text;
            DataTable dtblVoucherData = TransferDataToLocalDataTable(sqlCmd);

            if (dtblVoucherData.Rows.Count > 0)
            {
                for (int i = 0; i < dtblVoucherData.Rows.Count; i++)
                {
                    vType = Convert.ToInt32(dtblVoucherData.Rows[i][2]);
                    if (vType != curvType)
                    {
                        grpName = dtblVoucherData.Rows[i].Field<string>("vunits");
                        lvgrp = lvw.Groups.Add(grpName, grpName + " - " + dtblVoucherData.Rows[i].Field<String>("vname"));
                        curvType = vType;
                    }
                    lvi = new ListViewItem(dtblVoucherData.Rows[i].Field<String>("Descr"));
                    lvi.Group = lvgrp;
                    rTotal = 0;
                    for (int j = 5; j < 17; j++)
			        {
			            lvi.SubItems.Add(fmtVoucherAmt(vType,dtblVoucherData.Rows[i][j]));
                        try
                        {
                            rTotal += Convert.ToDecimal(dtblVoucherData.Rows[i][j]);
                        }
                        catch (System.Exception)
                        {
                        }
			        }
                    lvi.SubItems.Add(fmtVoucherAmt(vType,rTotal));
                    if (rTotal > 0 && nbrmnths > 1)
                    { lvi.SubItems.Add(fmtVoucherAmt(vType, rTotal / (Decimal)nbrmnths)); }
                    else
                    { lvi.SubItems.Add(fmtVoucherAmt(vType, rTotal)); }
                    lvw.Items.Add(lvi);
                }
            }
        }

        private string fmtVoucherAmt(int vtype, object vdata)
        {
            string tmp = "--";
            decimal vDec = 0;
            if (vdata.ToString() != "")
            {
                vDec = Convert.ToDecimal(vdata);
                switch (vtype)
                {
                    case 0:
                        if (vDec != 0)
                            tmp = vDec.ToString("F");
                        break;

                    default:
                        if (vDec != 0)
                            tmp = Convert.ToInt32(vDec).ToString();
                        break;
                }
            }
            return tmp;
        }

        private string lvItemCalcAvg(string sTotal, int nbrrows)
        {
            int total = 0;
            int avg = 0;
            try
            {
                total = Convert.ToInt32(sTotal);
                avg = total / nbrrows;
                return avg.ToString("N00");
            }
            catch (System.Exception)
            {
            }
            return "";
        }

        private void addlvwGenItem(ListView lvw, DataRow drowNbrSvcDays)
        {
            Color bkColorBase = Color.White;
            Color bkColorDollors = Color.WhiteSmoke;
            ListViewItem lvi;
            int irow = 0;
            string period = "";
            System.Drawing.Font sFont = lvw.Font;

            irow = Convert.ToInt32(drowNbrSvcDays[1].ToString().Substring(4, 2));
            if (irow < 13)
            {
                period = drowNbrSvcDays[1].ToString().Substring(0, 4) + " " + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(irow);
                if (sFiscalPeriod == drowNbrSvcDays[0].ToString())
                {
                    bkColorBase = Color.LightGoldenrodYellow;
                    bkColorDollors = Color.LightGoldenrodYellow;
                }
            }
            else
            {
                period = "YTD";
                sFont = new Font(lvw.Font, FontStyle.Bold);
                bkColorBase = Color.LightGray;
                bkColorDollors = Color.LightGray;
            }
            lvi = new ListViewItem(period);
            lvi.Font = sFont;
            lvi.Tag = drowNbrSvcDays[1].ToString();
            lvi.BackColor = bkColorBase;
            lvi.UseItemStyleForSubItems = false;
            lvi.SubItems.Add(Convert.ToInt32(drowNbrSvcDays[2]).ToString("N00"), Color.Black, bkColorBase, sFont);
            lvi.SubItems.Add("..", Color.Black, bkColorBase, sFont);
            lvi.SubItems.Add("..", Color.Black, bkColorBase, sFont);
            lvi.SubItems.Add("..", Color.Black, bkColorBase, sFont);
            lvi.SubItems.Add("..", Color.DarkBlue, bkColorDollors, sFont);
            lvi.SubItems.Add("..", Color.DarkBlue, bkColorDollors, sFont);
            lvi.SubItems.Add("..", Color.DarkBlue, bkColorDollors, sFont);
            lvi.SubItems.Add("..", Color.DarkBlue, Color.LightGray, sFont);
            sFont = new Font(lvw.Font, FontStyle.Bold);
            lvi.SubItems.Add("..", Color.DarkBlue, bkColorDollors, sFont);
            lvw.Items.Add(lvi);
        }

        private void updatelvwGenLbs(ListViewItem lvi, DataTable dtblInkindLbs, int nbritems)
        {
            string speriod = "";
            int iptr = 0;
            int irow = 0;
            decimal lbsInkind;
            decimal dollarsInkind = 0;
            speriod = lvi.Tag.ToString();

            for (int j = 0; j < dtblInkindLbs.Rows.Count; j++)
            {
                if (speriod == dtblInkindLbs.Rows[j].Field<string>(1))
                {
                    iptr = Convert.ToInt32(speriod.Substring(4, 2));
                    if (iptr < 13)
                    {
                        irow = iptr - 1;
                    }
                    else
                    {
                        irow = nbritems - 1;
                    }
                    lbsInkind = Convert.ToDecimal(dtblInkindLbs.Rows[j][2]);
                    dollarsInkind = lbsInkind * CCFBPrefs.InkindDollarsPerLb;
                    lvi.SubItems[4].Text = lbsInkind.ToString("N00");
                    lvi.SubItems[6].Text = dollarsInkind.ToString("C0");
                    lvi.SubItems[6].Tag = dollarsInkind;
                }
            }
        }

        private void updatelvwGenVolData(ListView lvw, int irow, string yearmonth, Single volHours, int numVols)
        {
            //int iptr = 0;
            //int irow = 0;
            decimal vHours;
            decimal vDollars = 0;
            System.Drawing.Font sFont = lvw.Font;
            //iptr = Convert.ToInt32(yearmonth.Substring(4, 2));
            //if (iptr < 13)
            //{
            //    irow = iptr-1;
            //}
            //else
            //{
            //    irow = lvw.Items.Count - 1;
            //}
            vHours = Math.Round(Convert.ToDecimal(volHours), 0);
            vDollars = vHours * CCFBPrefs.InkindDollarsPerHr;
            lvw.Items[irow].SubItems[2].Text = numVols.ToString("N00");
            lvw.Items[irow].SubItems[3].Text = vHours.ToString("N00");
            lvw.Items[irow].SubItems[5].Text = vDollars.ToString("C0");
            lvw.Items[irow].SubItems[5].Tag = vDollars;
        }

        private void setSeriesType(Chart chart1, SeriesChartType charttype) 
        {
            int newAlpha = 255;
            if (charttype == SeriesChartType.Area)
                newAlpha = 200;
            foreach (Series item in chart1.Series)
            {
                item.ChartType = charttype;
                item.Color = Color.FromArgb(newAlpha, item.Color);
            }
        }

        private void fillChartFamilyBreakdown(Series series1, TrxLogPeriodTotals clsData)
        {
            series1.Points.Clear();
            addPiePoint(ref series1, (double)clsData.Infants, "Infants", true, Color.Salmon);
            if (chkMergeTeens.Checked == true)
            {
                addPiePoint(ref series1, (double)(clsData.Youth + clsData.Teens), "Children", false, Color.CornflowerBlue);
            }
            else
            {
                addPiePoint(ref series1, (double)clsData.Youth, "Children", false, Color.CornflowerBlue);
                addPiePoint(ref series1, (double)clsData.Teens, "Teens", true, Color.Orange);
            }
            addPiePoint(ref series1, (double)(clsData.Eighteen), "Eighteen", true, Color.OrangeRed);
            addPiePoint(ref series1, (double)(clsData.Adults), "Adults", false, Color.DarkSlateGray);
            addPiePoint(ref series1, (double)(clsData.Seniors), "Seniors", false, Color.Silver);
           /*
            for (int i = 0; i < series1.Points.Count; i++)
            {
                series1.Points[i].ToolTip = series1.Points[i].YValues.GetValue(0).ToString();
            }
            */
        }

        private void addPiePoint(ref Series series1, double value, string label, Boolean exploded, Color thecolor)
        {
            DataPoint pointData = new DataPoint();
            pointData.IsValueShownAsLabel = false;
            if (exploded == true)
            {
                pointData.CustomProperties = "Exploded=True";
            }
            else
            {
                pointData.CustomProperties = "Exploded=False";
            }
            pointData.IsVisibleInLegend = true;
            if (series1.Label.StartsWith("#") == true)
            {
                pointData.LegendText = label;
            }
            else
            {
                pointData.Label = label;
                pointData.LegendText = "#LABEL";
            }
            pointData.YValues[0] = value;
            
            if (thecolor != Color.Transparent)
            {
                pointData.Color = thecolor;
            }
            series1.Points.Add(pointData);
        }
        private void chkMergeTeens_CheckedChanged(object sender, EventArgs e)
        {
            if (pnlReports.Visible == true)
            {
                fillFamilyBreakdownCharts();
                setChildTeensVisible(chkMergeTeens.Checked);
            }
        }

        private void fillFamilyBreakdownCharts()
        {
            clsTrxLogCurFiscalYrStats.findFiscalPeriod(CCFBGlobal.CalcFiscalPeriod(datePeriodFirst));
            fillChartFamilyBreakdown(chartFamilyMonth.Series[0], clsTrxLogCurFiscalYrStats);
            chartFamilyMonth.Titles[0].Text = datePeriodFirst.ToString("MMMM") + " " + datePeriodFirst.Year.ToString();
            clsTrxLogCurFiscalYrStats.setYTDRow();
            fillChartFamilyBreakdown(chartFamilyYTD.Series[0], clsTrxLogCurFiscalYrStats);
            chartFamilyYTD.Titles[1].Text = dateFiscalStart.ToString("MMMM") + " " + dateFiscalStart.Year.ToString() + " - " + datePeriodFirst.ToString("MMMM") + " " + datePeriodFirst.Year.ToString();
        }

        private void setChildTeensVisible(bool mergeTeens)
        {
            lblChidren.Visible = mergeTeens;
            tbIndCurMonthChildren.Visible = mergeTeens;
            tbIndCurMonthChildrenNew.Visible = mergeTeens;
            tbIndCurMonthChildrenReturning.Visible = mergeTeens;
            tbIndPvYrMonthChildren.Visible = mergeTeens;
            tbIndPvYrMonthChildrenNew.Visible = mergeTeens;
            tbIndPvYrMonthChildrenReturning.Visible = mergeTeens;
            tbIndDifMonthChildren.Visible = mergeTeens;
            tbIndDifMonthChildrenPercent.Visible = mergeTeens;

            lblChildrenYtd.Visible = mergeTeens;
            tbIndCurYrYTDChildren.Visible = mergeTeens;
            tbIndCurYrYTDChildrenNew.Visible = mergeTeens;
            tbIndCurYrYTDChildrenReturning.Visible = mergeTeens;
            tbIndPvYrYTDChildren.Visible = mergeTeens;
            tbIndPvYrYTDChildrenNew.Visible = mergeTeens;
            tbIndPvYrYTDChildrenReturning.Visible = mergeTeens;
            tbIndPvY2YTDChildren.Visible = mergeTeens;
            tbIndPvY2YTDChildrenNew.Visible = mergeTeens;
            tbIndPvY2YTDChildrenReturning.Visible = mergeTeens;
            tbIndDifYTDChildren.Visible = mergeTeens;
            tbIndDifYTDChildrenPercent.Visible = mergeTeens;

            lblYouth.Visible = !mergeTeens;
            tbIndCurMonthYouth.Visible = !mergeTeens;
            tbIndCurMonthYouthNew.Visible = !mergeTeens;
            tbIndCurMonthYouthReturning.Visible = !mergeTeens;
            tbIndPvYrMonthYouth.Visible = !mergeTeens;
            tbIndPvYrMonthYouthNew.Visible = !mergeTeens;
            tbIndPvYrMonthYouthReturning.Visible = !mergeTeens;
            tbIndDifMonthYouth.Visible = !mergeTeens;
            tbIndDifMonthYouthPercent.Visible = !mergeTeens;

            lblYouthYTD.Visible = !mergeTeens;
            tbIndCurYrYTDYouth.Visible = !mergeTeens;
            tbIndCurYrYTDYouthNew.Visible = !mergeTeens;
            tbIndCurYrYTDYouthReturning.Visible = !mergeTeens;
            tbIndPvY2YTDYouth.Visible = !mergeTeens;
            tbIndPvY2YTDYouthNew.Visible = !mergeTeens;
            tbIndPvY2YTDYouthReturning.Visible = !mergeTeens;
            tbIndPvYrYTDYouth.Visible = !mergeTeens;
            tbIndPvYrYTDYouthNew.Visible = !mergeTeens;
            tbIndPvYrYTDYouthReturning.Visible = !mergeTeens;
            tbIndDifYTDYouth.Visible = !mergeTeens;
            tbIndDifYTDYouthPercent.Visible = !mergeTeens;

            
            lblTeens.Visible = !mergeTeens;
            tbIndCurMonthTeens.Visible = !mergeTeens;
            tbIndCurMonthTeensNew.Visible = !mergeTeens;
            tbIndCurMonthTeensReturning.Visible = !mergeTeens;
            tbIndPvYrMonthTeens.Visible = !mergeTeens;
            tbIndPvYrMonthTeensNew.Visible = !mergeTeens;
            tbIndPvYrMonthTeensReturning.Visible = !mergeTeens;
            tbIndDifMonthTeens.Visible = !mergeTeens;
            tbIndDifMonthTeensPercent.Visible = !mergeTeens;

            lblTeenYTD.Visible = !mergeTeens;
            tbIndCurYrYTDTeens.Visible = !mergeTeens;
            tbIndCurYrYTDTeensNew.Visible = !mergeTeens;
            tbIndCurYrYTDTeensReturning.Visible = !mergeTeens;
            tbIndPvY2YTDTeens.Visible = !mergeTeens;
            tbIndPvY2YTDTeensNew.Visible = !mergeTeens;
            tbIndPvY2YTDTeensReturning.Visible = !mergeTeens;
            tbIndPvYrYTDTeens.Visible = !mergeTeens;
            tbIndPvYrYTDTeensNew.Visible = !mergeTeens;
            tbIndPvYrYTDTeensReturning.Visible = !mergeTeens;
            tbIndDifYTDTeens.Visible = !mergeTeens;
            tbIndDifYTDTeensPercent.Visible = !mergeTeens;
        }

        private void cboChartType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cboChartType = (ComboBox)sender;
            SeriesChartType ct = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), cboChartType.Text, true);
            switch (cboChartType.Tag.ToString())
            {
                case "ByMonth":         setSeriesType(chartHHByMonth, ct); break;
                case "TotalServed":     setSeriesType(chartHHServed, ct); break;
                case "Commodity":       setSeriesType(chartHHCommodities, ct); break;
                case "Supplemental":    setSeriesType(chartHHSupplemental, ct); break;
                case "BabySvcs":        setSeriesType(chartHHBabySvcs, ct); break;
                case "Homeless":        setSeriesType(chartHHHomeless, ct); break;
                case "Transient":       setSeriesType(chartHHTransient, ct); break;
                case "InCityLimits":    setSeriesType(chartHHInCityLimits, ct); break;
            }
        }

        private void chk3DHH_CheckedChanged(object sender, EventArgs e)
        {
            chartHHServed.ChartAreas[0].Area3DStyle.Enable3D = chk3DHHServed.Checked;
            CheckBox chk = (CheckBox)sender;
            switch (chk.Tag.ToString())
            {
                case "ByMonth":         chartHHByMonth.ChartAreas[0].Area3DStyle.Enable3D = chk.Checked; break;
                case "TotalServed":     chartHHServed.ChartAreas[0].Area3DStyle.Enable3D = chk.Checked; break;
                case "Commodity":       chartHHCommodities.ChartAreas[0].Area3DStyle.Enable3D = chk.Checked; break;
                case "Supplemental":    chartHHSupplemental.ChartAreas[0].Area3DStyle.Enable3D = chk.Checked; break;
                case "BabySvcs":        chartHHBabySvcs.ChartAreas[0].Area3DStyle.Enable3D = chk.Checked; break;
                case "Homeless":        chartHHHomeless.ChartAreas[0].Area3DStyle.Enable3D = chk.Checked; break;
                case "Transient":       chartHHTransient.ChartAreas[0].Area3DStyle.Enable3D = chk.Checked; break;
                case "InCityLimits":    chartHHInCityLimits.ChartAreas[0].Area3DStyle.Enable3D = chk.Checked; break;
            }
        }

        private void fillByMonthCharts(TrxLogPeriodTotals clsTrxLogStats, Chart chart1, int seriesIndex, string fldName, string fiscalyr)
        {
            int dataValue = 0;
            int seriesTotal = 0;
            int nbrMonths = 0;
            int maxValue = 0;
            int minValue = 9999999;
            
            //if (seriesIndex < chart1.Series.Count)
            //{
            //    chart1.Series[seriesIndex].Points.Clear();
            //}
            //else
            //{
            //    chart1.Series.Add("Series" + seriesIndex.ToString());
            //}
            for (int i = 0; i < 12; i++)
            {
                if (clsTrxLogStats.findFiscalPeriod(i+1) == true)
                    //if (i < clsTrxLogStats.RowCount && clsTrxLogStats.findFiscalPeriod(i + 1) == true)
                {
                    dataValue = Convert.ToInt32(clsTrxLogStats.GetDataValue(fldName));

                    seriesTotal += dataValue;
                    nbrMonths++;
                    if (dataValue > maxValue)
                        maxValue = dataValue;
                    if (dataValue < minValue)
                        minValue = dataValue;

                    chart1.Series[seriesIndex].Points[i].YValues[0] = dataValue;
                    chart1.Series[seriesIndex].Points[i].ToolTip = dataValue.ToString();
                    chart1.Series[seriesIndex].Points[i].MarkerStyle = MarkerStyle.Diamond;
                    
                }
                else
                {

                    chart1.Series[seriesIndex].Points[i].YValues[0] = 0;
                    chart1.Series[seriesIndex].Points[i].MarkerStyle = MarkerStyle.None;
                    //chart1.Series[seriesIndex].Points[i].BorderDashStyle = ChartDashStyle.Dot;
                    chart1.Series[seriesIndex].Points[i].BorderWidth = 0;

                }
                chart1.Series[seriesIndex].Points[i].IsValueShownAsLabel = false;
            }
            chart1.Legends[0].CustomItems.Add(new LegendItem("Series" + seriesIndex.ToString(), chart1.Series[seriesIndex].Color, ""));
            chart1.Legends[0].CustomItems[seriesIndex].Cells.Add(new LegendCell(LegendCellType.SeriesSymbol, ""));
            chart1.Legends[0].CustomItems[seriesIndex].Cells.Add(new LegendCell(LegendCellType.Text, fiscalyr));
            chart1.Legends[0].CustomItems[seriesIndex].Cells.Add(new LegendCell(LegendCellType.Text, minValue.ToString("0,0")));
            chart1.Legends[0].CustomItems[seriesIndex].Cells.Add(new LegendCell(LegendCellType.Text, maxValue.ToString("0,0")));
            if (seriesTotal != 0 && nbrMonths != 0)
                chart1.Legends[0].CustomItems[seriesIndex].Cells.Add(new LegendCell(LegendCellType.Text, Convert.ToInt32(seriesTotal / nbrMonths).ToString("0,0")));
            else
                chart1.Legends[0].CustomItems[seriesIndex].Cells.Add(new LegendCell(LegendCellType.Text, "0"));

            //chart1.Series[seriesIndex].LegendText = clsTrxLogStats.FiscalPeriod.Substring(0, 4);
        }

        private void initChartsByMonth(Chart chart1)
        {
            for (int i = 0; i < 12; i++)
            {
                chart1.ChartAreas[0].Axes[0].CustomLabels[i].Text = dateFiscalStart.AddMonths(i).ToString("m").Substring(0, 3);
            }
            chart1.Legends[0].CustomItems.Clear();
        }

        private void MonthEndReportsForm_Load(object sender, EventArgs e)
        {
            pnlLoadStatus.Location = pnlReports.Location;
        }

        private string makeReportPath()
        {
            if (CCFBPrefs.FiscalYearStartMonth == 1)
            {
                return savePath + sFiscalYear + "\\" + strRptMonth() + "-" + cboReportMonth.Text + "\\";
            }
            return savePath + "FY" + sFiscalYear + "\\FY" + CCFBGlobal.FiscalPeriod(cboReportMonth.SelectedValue.ToString()).Substring(4) + "-" + cboReportMonth.Text + "\\";
        }

        private string makeReportPrefix()
        {
            if (CCFBPrefs.FiscalYearStartMonth == 1)
            {
                return cboReportYear.SelectedItem.ToString() + "-" + strRptMonth() + "-";
            }
            else
            {
                return "FY" + sFiscalYear + "-" + CCFBGlobal.FiscalPeriod(cboReportMonth.SelectedValue.ToString()).Substring(4) + "-";
            }
        }

        private int intRptMonth()
        {
            return Convert.ToInt32(cboReportMonth.SelectedValue.ToString().Substring(4));
        }

        private string strRptMonth()
        {
            return cboReportMonth.SelectedValue.ToString().Substring(4);
        }

        private void updateRptMonthsList()
        {
            btnLoadFiscalYearMonths.Enabled = false;
            btnLoadPeriod.Enabled = false;
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Application.DoEvents();
            sqlCmd = new SqlCommand("SELECT * FROM TrxLogFiscalMonthsList WHERE LEFT(FiscalPeriod,4) = '" + cboReportYear.Text + "' order by FiscalPeriod Desc", conn);
            sqlCmd.CommandType = CommandType.Text;
            dtblRptMonths = TransferDataToLocalDataTable(sqlCmd);
            if (dtblRptMonths.Rows.Count > 0)
            {
                cboReportMonth.DataSource = dtblRptMonths;
                cboReportMonth.DisplayMember = "CalText";
                cboReportMonth.ValueMember = "CalPeriod";
                btnLoadPeriod.Enabled = true;
            }
            else
            {
                cboReportMonth.DataSource = null;
                cboReportMonth.Items.Add("Not Initialized");
            }
            btnLoadFiscalYearMonths.Enabled = true;
            this.Cursor = System.Windows.Forms.Cursors.Default;
        }

        private void btnLoadFiscalYearMonths_Click(object sender, EventArgs e)
        {
            updateRptMonthsList();
        }

        private void disableTB(List<TextBox> tbList)
        {
            foreach (TextBox tb in tbList)
            {
                tb.Visible = false;
            }
        }

        private void disableLbl(List<Label> lblList)
        {
            foreach (Label lbl in lblList)
            {
                lbl.Visible = false;
            }
        }

        private string getInkindLbsSQL(string dateFirst, string dateLast)
        {
            string sql = "";
            try
            {
                sql = "SELECT dbo.GetFiscalPeriod(LEFT(CONVERT(varchar(12), TrxDate, 112), 6)) AS FiscalPeriod"
                    + "     , LEFT(CONVERT(varchar(12), TrxDate, 112), 6) AS YearMonth"
                    + "     , SUM(Pounds) Lbs" + Environment.NewLine
                    + "  FROM FoodDonations"
                    + " WHERE TrxDate BETWEEN '" + dateFirst + "' AND '" + dateLast + "' AND DonationType IN (4,6)" + Environment.NewLine
                    + " GROUP BY dbo.GetFiscalPeriod(LEFT(CONVERT(varchar(12), TrxDate, 112), 6))" + Environment.NewLine
                    + "       , LEFT(CONVERT(varchar(12), TrxDate, 112), 6)" + Environment.NewLine
                    + "UNION" + Environment.NewLine
                    + "SELECT MIN(LEFT(dbo.GetFiscalPeriod(LEFT(CONVERT(varchar(12), TrxDate, 112), 6)),4) + '99') AS FiscalPeriod"
                    + "     , MIN(LEFT(dbo.GetFiscalPeriod(LEFT(CONVERT(varchar(12), TrxDate, 112), 6)),4) + '99') AS YearMonth"
                    + "     , SUM(Pounds) Lbs" + Environment.NewLine
                    + "  FROM FoodDonations"
                    + " WHERE TrxDate BETWEEN '" + dateFirst + "' AND '" + dateLast + "' AND DonationType IN (4,6)" + Environment.NewLine
                    + " GROUP BY LEFT(dbo.GetFiscalPeriod(LEFT(CONVERT(varchar(12), TrxDate, 112), 6)),4) + '99'"
                    + "        , LEFT(dbo.GetFiscalPeriod(LEFT(CONVERT(varchar(12), TrxDate, 112), 6)),4) + '99'" + Environment.NewLine
                    + "ORDER BY FiscalPeriod";

                return sql;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Fiscal Start=" + dateFirst + "\r\nPeriodEnd=" + dateLast + "\r\n" + ex.Message);
                return "";
            }
        }

        private string getNbrServiceDaysSQL(string dateFirst, string dateLast)
        {
            string sql = "";
            try
            {
                sql = "SELECT dbo.GetFiscalPeriod(LEFT(CONVERT(varchar(12), TrxDate, 112), 6)) AS FiscalPeriod"
                    + "     , LEFT(CONVERT(varchar(12), TrxDate, 112), 6) AS YearMonth"
                    + "     , Count(*) NbrDays"
                    + "     , SUM(NbrServices) NbrSvcs" + Environment.NewLine
                    + "  FROM (select TrxDate, Count(*) NbrServices From TrxLog WHERE TrxDate BETWEEN '" + dateFirst + "' AND '" + dateLast + "' GROUP BY TrxDate) t0" + Environment.NewLine
                    + " GROUP BY dbo.GetFiscalPeriod(LEFT(CONVERT(varchar(12), TrxDate, 112), 6))" + Environment.NewLine
                    + "       , LEFT(CONVERT(varchar(12), TrxDate, 112), 6)" + Environment.NewLine
                    + "UNION" + Environment.NewLine
                    + "SELECT MIN(LEFT(dbo.GetFiscalPeriod(LEFT(CONVERT(varchar(12), TrxDate, 112), 6)),4) + '99') AS FiscalPeriod"
                    + "     , MIN(LEFT(dbo.GetFiscalPeriod(LEFT(CONVERT(varchar(12), TrxDate, 112), 6)),4) + '99') AS YearMonth"
                    + "     , Count(*) NbrDays"
                    + "     , sum(NbrServices) NbrSvcs" + Environment.NewLine
                    + "  FROM (select TrxDate, Count(*) NbrServices From TrxLog WHERE TrxDate BETWEEN '" + dateFirst + "' AND '" + dateLast + "' GROUP BY TrxDate) tl1" + Environment.NewLine
                    + " GROUP BY LEFT(dbo.GetFiscalPeriod(LEFT(CONVERT(varchar(12), TrxDate, 112), 6)),4) + '99'"
                    + "        , LEFT(dbo.GetFiscalPeriod(LEFT(CONVERT(varchar(12), TrxDate, 112), 6)),4) + '99'" + Environment.NewLine
                    + "ORDER BY FiscalPeriod";

                return sql;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Fiscal Start=" + dateFirst + "\r\nPeriodEnd=" + dateLast + "\r\n" + ex.Message);
                return "";
            }
        }

        private string dollarsInkindLbs()
        {
            if (dtblCurInkindLbs.Rows.Count > 0)
            {
                for (int i = 0; i < dtblCurInkindLbs.Rows.Count; i++)
                {
                    if (sFiscalPeriod == dtblCurInkindLbs.Rows[i][0].ToString())
                    {
                        decimal inkindlbs = 0;
                        try
                        {
                            inkindlbs = Convert.ToDecimal(dtblCurInkindLbs.Rows[i][2]);
                            return (inkindlbs * CCFBPrefs.InkindDollarsPerLb).ToString("C00");
                        }
                        catch (System.Exception)
                        {
                        }

                    }
                }
            }
            return "";
        }

        private string dollarsInKindOther()
        {
            return "";
        }

        private string nbrDaysOpen()
        {
            for (int i = 0; i < dtblCurNbrServiceDays.Rows.Count; i++)
            {
                if (sFiscalPeriod == dtblCurNbrServiceDays.Rows[i][0].ToString())
                {
                    return dtblCurNbrServiceDays.Rows[i][2].ToString();
                }
            }
            return "";
        }
        private void cmsExport_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (CCFBGlobal.currentUser_PermissionLevel < CCFBGlobal.permissions_Admin)
            {
                nameLVCtrl = "";
                e.Cancel = true;
            }
            else
            {
                ContextMenuStrip cms = (ContextMenuStrip)sender;
                nameLVCtrl = cms.SourceControl.Name;
            }
        }

        private void cmsExport_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ListView lstview = new ListView(); 
            if (e.ClickedItem.Text == "Export To Excel")
            {
                string sname = "MonthStats-";
                switch (nameLVCtrl)
                {
                    case "lvwGen":
                        sname += "General";
                        lstview = lvwGenCur;
                        break;
                    case "lvwFamilySizeBySvcDate":
                        sname += "FamilySizeByDay";
                        lstview = lvwFamilySizeBySvcDate;
                        break;
                    case "lvwFamilySizeByMonth":
                        sname += "FamilySizeByMonth";
                        lstview = lvwFamilySizeByMonth;
                        break;
                    case "lvwLbsFoodByDonor":
                        sname += "FoodReceipts";
                        lstview = lvwLbsFoodByDonor;
                        break;
                    case "lvwNewHH":
                        sname += "NewHouseholds";
                        lstview = lvwNewHH;
                        break;
                    case "lvwSvcDays":
                        sname += "ServiceCounts";
                        lstview = lvwSvcDays;
                        break;
                    case "lvwTrxLogByDay":
                        sname += "TransactionSummaryByDay";
                        lstview = lvwTrxLogByDay;
                        break;
                    case "lvwTrxLogByMonth":
                        sname += "TransactionSummaryByMonth";
                        lstview = lvwTrxLogByMonth;
                        break;
                    case "lvwYTDLbsFoodByDonor":
                        sname += "YTD-FoodReceipts";
                        lstview = lvwYTDLbsFoodByDonor;
                        break;
                    default:
                        break;
                }
                if (sname != "")
                {
                    CCFBGlobal.ExportToExcell(lstview, sname + "_" + datePeriodFirst.Year.ToString()
                        + "_" + CCFBGlobal.formatNumberWithLeadingZero(datePeriodFirst.Month));
                }
            }
        }

        private void fillServiceCounts(ListView lvNew, ListView lvSvc, DateTime periodStart, DateTime periodEnd)
        {
            string dateRange = "BETWEEN '" + CCFBGlobal.formatDate(periodStart) + "' AND '" + CCFBGlobal.formatDate(periodEnd) + "'";
            lblSvcPeriod.Text = "SERVICE PERIOD: " + periodStart.ToShortDateString() + " to " + periodEnd.ToShortDateString();

            object dbValue = CCFBGlobal.getSQLValue("SELECT Count(*) FROM (select HouseholdId FROM TrxLog "
                                                  + " WHERE TrxDate " + dateRange
                                                  + " GROUP BY HouseholdID ) tmp");
            tbTotalClients.Text = dbValue.ToString();

            dbValue = CCFBGlobal.getSQLValue("SELECT Count(*) FROM "    //NbrServiceDays
                            + "(SELECT TrxDate FROM TrxLog WHERE TrxDate " + dateRange
                            + " GROUP BY TrxDate) tmp");
            tbDaysOpen.Text = dbValue.ToString();

            dbValue = CCFBGlobal.getSQLValue("SELECT Count(*) FROM Household WHERE firstService " + dateRange); //FirstTimeEverHH
            tbFirstSvcHH.Text = dbValue.ToString();

            dbValue = CCFBGlobal.getSQLValue("SELECT Count(*) FROM TrxLog WHERE FirstTimeEver = 1 AND TrxDate " + dateRange); //FirstTimeEverTL
            tbFirstSvcTL.Text = dbValue.ToString();

            string sqlText = "SELECT LEFT(dbo.GetFiscalPeriodFromDate(FirstService),4) Year, Count(*) NbrClients"
                           + "  FROM Household h "
                           + " WHERE ID in (SELECT DISTINCT HouseholdID FROM TrxLog WHERE TrxDate " + dateRange + ") AND FirstService IS NOT NULL"
                           + " GROUP BY LEFT(dbo.GetFiscalPeriodFromDate(FirstService),4)"
                           + " ORDER BY Year";
            filllvwSvcCounts(Convert.ToDecimal(tbTotalClients.Text), lvNew, sqlText, false);
            sqlText = "DECLARE @NbrClients float "
                    + "SELECT @NbrClients = Count(*) FROM (select HouseholdId FROM TrxLog "
                    + " WHERE TrxDate " + dateRange
                    + " GROUP BY HouseholdID ) tmp "
                    + "SELECT NbrVisits, COUNT(*) NbrClients"
                    + " FROM (SELECT HouseholdId, Count(*) NbrVisits FROM TrxLog "
                    + "        WHERE TrxDate " + dateRange
                    + "        GROUP By HouseholdID ) tmp "
                    + " GROUP By NbrVisits ORDER BY NbrVisits";
            filllvwSvcCounts(Convert.ToDecimal(tbTotalClients.Text), lvSvc, sqlText, true);
        }

        private void filllvwSvcCounts(decimal NbrHH, ListView lvw, string sqlQuery, bool showCum)
        {
            lvw.Items.Clear();
            sqlCmd = new SqlCommand(sqlQuery, conn);
            sqlCmd.CommandType = CommandType.Text;
            DataTable dtbl = TransferDataToLocalDataTable(sqlCmd);
            System.Windows.Forms.Application.DoEvents();
            sqlCmd.Dispose();
            int dbVal = 0;
            int total = 0;
            decimal calcPercent = 0;
            decimal cumPercent = 0;
            ListViewItem lvItm;
            for (int i = 0; i < dtbl.Rows.Count; i++)
            {
                lvItm = new ListViewItem("");
                dbVal = Convert.ToInt32(dtbl.Rows[i][0]);
                lvItm.SubItems.Add(dbVal.ToString());
                dbVal = Convert.ToInt32(dtbl.Rows[i][1]);
                total += dbVal;
                lvItm.SubItems.Add(dbVal.ToString("N00"));
                calcPercent = Convert.ToDecimal(dbVal) / NbrHH;
                cumPercent += calcPercent;
                lvItm.SubItems.Add(calcPercent.ToString("P1"));
                lvItm.SubItems.Add(cumPercent.ToString("P1"));
                lvw.Items.Add(lvItm);
            }
            lvItm = new ListViewItem("");
            lvItm.BackColor = Color.Khaki; 
            lvItm.SubItems.Add("Total");
            lvItm.SubItems.Add(total.ToString("N0"));
            lvw.Items.Add(lvItm);
        }
    }
}

