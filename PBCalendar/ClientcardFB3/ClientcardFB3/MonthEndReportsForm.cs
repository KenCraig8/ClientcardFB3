using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Interop.Outlook;

namespace ClientcardFB3
{
    public partial class MonthEndReportsForm : Form
    {
        MainForm frmMain;
        MonthlyReports clsMonthlyReports = new MonthlyReports(CCFBGlobal.connectionString);
        SqlDataAdapter dadAdpt;
        DataSet dset;
        //SqlCommand command;
        SqlConnection conn;
        string tbName = "TrxLog";
        string file = "";
        string city = "";
        string savePath = CCFBPrefs.ReportsSavePath;

        int rowCount = 0;

        // Create the Outlook application by using inline initialization.
        //Microsoft.Office.Interop.Outlook.Application oApp;

        List<TextBox> tbPerList;
        List<TextBox> tbCumList;
        List<TextBox> tbLbsPerList;
        List<TextBox> tbLbsCumList;
        
        DataTable dtblMonthStats;
        DataTable dtblFamilySizeByDate;
        DataTable dtblFamilySizeByMonth;
        DataTable dtblFoodRecieptsByFunding;
        DataTable dtblFoodRecieptsByDonor;
        DataTable dtblTrxLogDataByDay;
        DataTable dtblTrxLogDataByMonth;

        string sqlCommandText = "Select Distinct DateName(yy, TrxDate) as 'Year' "
            + "From TrxLog where TrxDate is not null Order By 'Year' DESC";


        public MonthEndReportsForm(MainForm mainFormIn)
        {
            InitializeComponent();

            frmMain = mainFormIn;
            tbPerList = new List<TextBox>();
            tbCumList = new List<TextBox>();
            tbLbsPerList = new List<TextBox>();
            tbLbsCumList = new List<TextBox>();
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
            lblDollarsPerHr.Text = "Value @ " + CCFBPrefs.InkindDollarsPerHr.ToString("C") + " /hr";
            lblDollarsPerLb.Text = "Value @ " + CCFBPrefs.InkindDollarsPerLb.ToString("C") + " /lb";
            initForm();
            tabPage1.BackColor = Color.LightGray;
            tabPage2.BackColor = Color.LightGray;
            lvReports.Enabled = false;
            btnCreateReports.Enabled = false;
            btnDisplayExisting.Enabled = false;
            btnEmailReports.Enabled = false;
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
                    file = CCFBPrefs.ReportsSavePath;
                    //Add Underschore
                    clsMonthlyReports.find(Convert.ToInt32(lvReports.Items[i].Tag));
                    file += cboReportYear.SelectedItem.ToString() +
                        getFormatedMonthNumber(cboReportMonth.SelectedIndex + 1).ToString() + "_";
                    //Add Period
                    file += clsMonthlyReports.ReportName + ".doc";

                    //Check if file exists
                    if (File.Exists(file) == true)
                    {
                        lvReports.Items[i].BackColor = Color.LightSteelBlue;
                        lvReports.Items[i].Checked = false;
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
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString(), 
                    CCFBGlobal.serverName);
            }

        }

        private int getValueAsInt(int rowNum, string colName )
        {
            try
            {
                return Int32.Parse(dtblMonthStats.Rows[rowNum][colName].ToString());
            }
            catch (System.Exception ex) 
            {
                CCFBGlobal.appendErrorToErrorReport("rowNum=" + rowNum + ", colName=" + colName,
                    ex.GetBaseException().ToString());
                return 0; 
            }
        }

        private void calcTotals()
        {
            decimal perVolhrs = 0;
            decimal cumVolhrs = 0;

            if (tbCumVolHrs.Text != null && tbCumVolHrs.Text.Trim() != "")
                cumVolhrs = Convert.ToDecimal(tbCumVolHrs.Text);

            if (tbPerVolHrs.Text != null && tbPerVolHrs.Text.Trim() != "")
                perVolhrs = Convert.ToDecimal(tbPerVolHrs.Text);


            tbPerLbsServedTotal.Text = (getValueAsInt(0, "LbsStandard")
                + getValueAsInt(0, "LbsOther")
                + getValueAsInt(0, "LbsCommodity")
                + getValueAsInt(0, "LbsSupplemental")).ToString("N00");

            tbCumLbsServedTotal.Text = (getValueAsInt(1, "LbsStandard")
                + getValueAsInt(1, "LbsOther")
                + getValueAsInt(1, "LbsCommodity")
                + getValueAsInt(1, "LbsSupplemental")).ToString("N00");
            tbDollarsPerHr.Tag = perVolhrs * CCFBPrefs.InkindDollarsPerHr;
            tbDollarsPerHr.Text = (perVolhrs * CCFBPrefs.InkindDollarsPerHr).ToString("C0");
            tbDollarsCumHr.Tag = cumVolhrs * CCFBPrefs.InkindDollarsPerHr;
            tbDollarsCumHr.Text = (cumVolhrs * CCFBPrefs.InkindDollarsPerHr).ToString("C0");
        }

        private void fillYearsCombo()
        {
            cboReportYear.Items.Clear();
            for (int i = 0; i < rowCount; i++)
            {
                cboReportYear.Items.Add(dset.Tables[tbName].Rows[i]["Year"]);

                if (dset.Tables[tbName].Rows[i]["Year"].ToString() == DateTime.Today.Year.ToString())
                    cboReportYear.SelectedIndex = i;

                if (DateTime.Today.Day > 10)
                    cboReportMonth.SelectedIndex = DateTime.Today.Month - 1;
                else if(DateTime.Today.Month  == 1)
                    cboReportMonth.SelectedIndex = 11;
                else
                    cboReportMonth.SelectedIndex = DateTime.Today.Month - 2;
            }

            if (cboReportYear.SelectedIndex < 0 && cboReportYear.Items.Count > 0)
                cboReportYear.SelectedIndex = 0;
        }

        private void btnLoadPeriod_Click(object sender, EventArgs e)
        {
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            SqlCommand sqlCmd;
            lblLoadFamilySize.BackColor = Color.PaleGoldenrod;
            lblLoadStatus.BackColor = Color.DimGray;
            lblLoadFoodRecieptsByFunding.BackColor = Color.DimGray;
            lblLoadFoodRecieptsByDonor.BackColor = Color.DimGray;
            lblFillForm.BackColor = Color.DimGray;
            lblTestForExistingFiles.BackColor = Color.DimGray;
            initForm(); 
            try
            {

                pnlLoadStatus.Visible = true;
                pnlLoadStatus.BringToFront();
                System.Windows.Forms.Application.DoEvents();


                DateTime dateTest = CCFBGlobal.CalcFiscalStartDate(Convert.ToDateTime(getFormatedMonthNumber(cboReportMonth.SelectedIndex + 1) + "/01/" + cboReportYear.Text));
                lblStatsPeriod.Text = cboReportMonth.Text + " " + cboReportYear.Text;
                lblStatsCum.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(dateTest.Month) + " " + dateTest.Year.ToString() + " - " + cboReportMonth.Text + " " + cboReportYear.Text;
                foreach (TextBox item in pnlStatistics.Controls.OfType<TextBox>())
                { item.Text = ""; }
                
                string dateStart = cboReportYear.Text + getFormatedMonthNumber(cboReportMonth.SelectedIndex + 1) + "01";
                string dateEnd = cboReportYear.Text + getFormatedMonthNumber(cboReportMonth.SelectedIndex + 1) + dateTest.AddMonths(1).AddDays(-1).Day.ToString();
                string sql = GetLoadFamilySizeSQL("Date", dateStart, dateEnd);
                sqlCmd = new SqlCommand(sql, conn);
                sqlCmd.CommandType = CommandType.Text;
                dtblFamilySizeByDate = TransferDataToLocalDataTable(sqlCmd);
                System.Windows.Forms.Application.DoEvents(); 
                sqlCmd.Dispose();

                dateStart = dateTest.Year.ToString() + getFormatedMonthNumber(dateTest.Month);
                sql = GetLoadFamilySizeSQL("Month", dateStart, dateEnd.Substring(0, 6));
                sqlCmd = new SqlCommand(sql, conn);
                sqlCmd.CommandType = CommandType.Text;
                dtblFamilySizeByMonth = TransferDataToLocalDataTable(sqlCmd);
                lblLoadFamilySize.BackColor = Color.PaleGreen;
                sqlCmd.Dispose();

                dateStart = getFormatedMonthNumber(cboReportMonth.SelectedIndex + 1) + "/01/" + cboReportYear.Text;
                dateEnd = Convert.ToDateTime(dateStart).AddMonths(1).AddDays(-1).ToShortDateString();
                sql = getTrxLogDataSQL("Date", dateStart, dateEnd);
                sqlCmd = new SqlCommand(sql, conn);
                sqlCmd.CommandType = CommandType.Text;
                dtblTrxLogDataByDay = TransferDataToLocalDataTable(sqlCmd);
                System.Windows.Forms.Application.DoEvents(); 
                sqlCmd.Dispose();

                sql = getTrxLogDataSQL("Month", dateTest.ToShortDateString(), dateEnd);
                sqlCmd = new SqlCommand(sql, conn);
                sqlCmd.CommandType = CommandType.Text;
                dtblTrxLogDataByMonth = TransferDataToLocalDataTable(sqlCmd);
                System.Windows.Forms.Application.DoEvents(); 
                sqlCmd.Dispose();


                lblLoadFoodRecieptsByFunding.BackColor = Color.PaleGoldenrod;
                System.Windows.Forms.Application.DoEvents();
                dateStart = dateTest.ToShortDateString();
                dateEnd = cboReportYear.Text + getFormatedMonthNumber(cboReportMonth.SelectedIndex + 1) + dateTest.AddMonths(1).AddDays(-1).Day.ToString();
                sql = getFoodRecieptsByFundingSQL(dateEnd, dateStart.Substring(6, 4) + dateStart.Substring(0, 2) + dateStart.Substring(3, 2));
                sqlCmd = new SqlCommand(sql, conn);
                sqlCmd.CommandType = CommandType.Text;
                dtblFoodRecieptsByFunding = TransferDataToLocalDataTable(sqlCmd);
                lblLoadFoodRecieptsByFunding.BackColor = Color.PaleGreen;

                lblLoadFoodRecieptsByDonor.BackColor = Color.PaleGoldenrod;
                System.Windows.Forms.Application.DoEvents();
                sql = getFoodRecieptsByDonorSQL(getFormatedMonthNumber(cboReportMonth.SelectedIndex + 1) + "/01/" + cboReportYear.Text, dateTest.ToShortDateString());
                sqlCmd = new SqlCommand(sql, conn);
                sqlCmd.CommandType = CommandType.Text;
                dtblFoodRecieptsByDonor = TransferDataToLocalDataTable(sqlCmd);
                lblLoadFoodRecieptsByDonor.BackColor = Color.PaleGreen;

                lblLoadStatus.BackColor = Color.PaleGoldenrod;
                System.Windows.Forms.Application.DoEvents();
                sqlCmd = new SqlCommand("MonthStatistics", conn);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                //DataTable dtbl = new DataTable();
                SqlParameter parameter = new SqlParameter("@Period", SqlDbType.Char, 6, "Period");
                parameter.Direction = ParameterDirection.Input;
                parameter.Value = cboReportYear.SelectedItem.ToString() + getFormatedMonthNumber(cboReportMonth.SelectedIndex + 1);
                sqlCmd.Parameters.Add(parameter); // Add the parameter to the Parameters collection. 
                dtblMonthStats = TransferDataToLocalDataTable(sqlCmd);
                lblLoadStatus.BackColor = Color.PaleGreen;

                lvReports.Enabled = true;
                btnCreateReports.Enabled = false;
                btnEmailReports.Enabled = false;
                btnDisplayExisting.Enabled = false;
            }
            catch (SqlException ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString(),
                    CCFBGlobal.serverName);
            }

            lblFillForm.BackColor = Color.PaleGoldenrod;
            System.Windows.Forms.Application.DoEvents();
            fillForm();
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
            initTexBoxList(tbPerList);
            initTexBoxList(tbCumList);
            initTexBoxList(tbLbsPerList);
            initTexBoxList(tbLbsCumList);
            lvwFamilySizeBySvcDate.Items.Clear();
            lvwFamilySizeByMonth.Items.Clear();
            lvwLbsFoodByDonor.Items.Clear();
            lvwTrxLogByDay.Items.Clear();
            lvwTrxLogByMonth.Items.Clear();
            System.Windows.Forms.Application.DoEvents();
        }

        private void fillForm()
        {
            if (dtblMonthStats != null)
            {
                foreach (TextBox tb in tbPerList.OfType<TextBox>())
                {
                    if (tb.Tag.ToString() != "")
                    {
                        if (dtblMonthStats.Columns.IndexOf(tb.Tag.ToString()) >= 0)
                        {
                            if (dtblMonthStats.Rows[0][tb.Tag.ToString()].ToString() == "")
                            {
                                tb.Text = "0";
                            }
                            else
                            {
                                tb.Text = Convert.ToDecimal(dtblMonthStats.Rows[0][tb.Tag.ToString()]).ToString("N00");
                            }
                        }
                    }
                }
                foreach (TextBox tb in tbCumList.OfType<TextBox>())
                {
                    if (tb.Tag.ToString() != "")
                    {
                        if (dtblMonthStats.Columns.IndexOf(tb.Tag.ToString()) >= 0)
                        {
                            if (dtblMonthStats.Rows[1][tb.Tag.ToString()].ToString() == "")
                            {
                                tb.Text = "0";
                            }
                            else
                            {
                                tb.Text = Convert.ToDecimal(dtblMonthStats.Rows[1][tb.Tag.ToString()]).ToString("N00");
                            }
                        }
                    }
                }
                calcTotals();
            }
            else
            {
                MessageBox.Show("ERROR: Load Period Data Has Caused An Error."
                + " Please Select A Different Period And Try Again");
            }
            if (dtblFoodRecieptsByFunding.Rows.Count > 0)
            {
                decimal colSum = 0;
                string tagVal = "";
                foreach (TextBox tb in tbLbsPerList.OfType<TextBox>())
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
                                colSum += Convert.ToDecimal(dtblFoodRecieptsByFunding.Rows[0][tagVal]);
                                tb.Text = Convert.ToDecimal(dtblFoodRecieptsByFunding.Rows[0][tagVal]).ToString("N00");
                            }
                            if (tagVal == "LbsInkind")
                            {
                                tbInkindLbsPer.Text = tb.Text;
                                tbDollarsPerLbs.Tag = (Convert.ToDecimal(tb.Text) * CCFBPrefs.InkindDollarsPerLb);
                                tbDollarsPerLbs.Text = (Convert.ToDecimal(tb.Text) * CCFBPrefs.InkindDollarsPerLb).ToString("C0");
                                tbDollarsPer.Tag = Convert.ToDecimal(tbDollarsPerLbs.Tag) + Convert.ToDecimal(tbDollarsPerHr.Tag);
                                tbDollarsPer.Text = Convert.ToDecimal(Convert.ToDecimal(tbDollarsPerLbs.Tag) + Convert.ToDecimal(tbDollarsPerHr.Tag)).ToString("C0");
                            }
                        }
                    }
                }
                tbLbsRcvdPerTotal.Text = colSum.ToString("N00");
                colSum = 0;
                foreach (TextBox tb in tbLbsCumList.OfType<TextBox>())
                {
                    if (tb.Tag.ToString() != "")
                    {
                        tagVal = tb.Tag.ToString();
                        if (dtblFoodRecieptsByFunding.Columns.IndexOf(tagVal) >= 0)
                        {
                            if (dtblFoodRecieptsByFunding.Rows[1][tagVal].ToString() == "")
                            {
                                tb.Text = "0";
                            }
                            else
                            {
                                colSum += Convert.ToDecimal(dtblFoodRecieptsByFunding.Rows[1][tagVal]);
                                tb.Text = Convert.ToDecimal(dtblFoodRecieptsByFunding.Rows[1][tagVal]).ToString("N00");
                            }
                            if (tagVal == "LbsInkind")
                            {
                                tbInkindLbsCum.Text = tb.Text;
                                tbDollarsCumLbs.Tag = Convert.ToDecimal(tb.Text) * CCFBPrefs.InkindDollarsPerLb;
                                tbDollarsCumLbs.Text = (Convert.ToDecimal(tb.Text) * CCFBPrefs.InkindDollarsPerLb).ToString("C0");
                                tbDollarsCum.Tag = Convert.ToDecimal(tbDollarsCumLbs.Tag) + Convert.ToDecimal(tbDollarsCumHr.Tag);
                                tbDollarsCum.Text = Convert.ToDecimal(Convert.ToDecimal(tbDollarsCumLbs.Tag) + Convert.ToDecimal(tbDollarsCumHr.Tag)).ToString("C0");
                            }
                        }
                    }
                }
                tbLbsRcvdCumTotal.Text = colSum.ToString("N00");
            }
            if (dtblFoodRecieptsByDonor.Rows.Count > 0)
            {
                decimal decPerVal = 0;
                decimal decCumVal = 0;
                decimal decPerTot = 0;
                decimal decCumTot = 0;
                int i = 0;
                DataRow drow1;
                DataRow drow2;
                while (i <dtblFoodRecieptsByDonor.Rows.Count)
                {
                    drow1 = dtblFoodRecieptsByDonor.Rows[i];
                    i++;
                    drow2 = dtblFoodRecieptsByDonor.Rows[i];
                    i++;
                    decPerVal = 0;
                    decCumVal = 0;
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
                    ListViewItem lvItm = new ListViewItem("");
                    lvItm.SubItems.Add(decPerVal.ToString("N00"));
                    lvItm.SubItems.Add("..");
                    lvItm.SubItems.Add(decCumVal.ToString("N00"));
                    lvItm.SubItems.Add("..");
                    lvItm.SubItems.Add(drow1["Name"].ToString());
                    lvwLbsFoodByDonor.Items.Add(lvItm);
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
                        decPerVal = Convert.ToDecimal(item.SubItems[1].Text);
                        item.SubItems[2].Text = (decPerVal / decPerTot).ToString("P1");
                        decCumVal = Convert.ToDecimal(item.SubItems[3].Text);
                        item.SubItems[4].Text = (decCumVal / decCumTot).ToString("P1");
                    }
                }
            }
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

            for (int i = 0; i < lvReports.Items.Count; i++)
            {
                if (lvReports.Items[i].Checked == true)
                {
                    switch (lvReports.Items[i].Tag.ToString())
                    {
                        case "3": //FB Coalition Report
                            {
                                //if (chkFBCoalitionReport.Checked == true)
                                //{
                                clsMonthlyReports.find(Convert.ToInt32(lvReports.Items[i].Tag));
                                saveAs = savePath + "\\" + cboReportYear.SelectedItem.ToString()
                                    + getFormatedMonthNumber(cboReportMonth.SelectedIndex + 1) + "_" +
                                    clsMonthlyReports.ReportName + ".doc";

                                templatePath = clsMonthlyReports.ReportPath;

                                CreateFBCoalitionReport clsCreateFBCoalitionReport = new CreateFBCoalitionReport(dtblMonthStats);
                                clsCreateFBCoalitionReport.createReport(tbFBName.Text, cboReportMonth.SelectedItem.ToString(),
                                    tbPerTotalFamily.Text, tbCumTotalFamily.Text, tbPerTotalHH.Text, tbCumTotalHH.Text,
                                    tbPerLbsServedTotal.Text, tbCumLbsServedTotal.Text, tbPreparedBy.Text, saveAs, templatePath);
                                break;
                                //}
                            }
                        case "1":       //EFAP Report
                            //if (chkEFAP.Checked == true)
                            {
                                clsMonthlyReports.find(Convert.ToInt32(lvReports.Items[i].Tag));
                                saveAs = savePath + "\\" + cboReportYear.SelectedItem.ToString()
                                    + getFormatedMonthNumber(cboReportMonth.SelectedIndex + 1) + "_" +
                                    clsMonthlyReports.ReportName + ".doc";

                                templatePath = clsMonthlyReports.ReportPath;

                                CreateNewEFAPReport clsCreateEFAPReport = new CreateNewEFAPReport(dtblMonthStats);
                                clsCreateEFAPReport.createReport(tbFBName.Text, cboReportMonth.SelectedItem.ToString() + "/" + cboReportYear.SelectedItem.ToString(),
                                    tbCounty.Text, tbPreparedBy.Text, saveAs, templatePath);
                                break;
                            }
                        case "4":       //NW Harvest Report
                            //if (chkNthWstHarvest.Checked == true)
                            {
                                clsMonthlyReports.find(Convert.ToInt32(lvReports.Items[i].Tag));
                                saveAs = savePath + "\\" + cboReportYear.SelectedItem.ToString()
                                    + getFormatedMonthNumber(cboReportMonth.SelectedIndex + 1) + "_"
                                    + clsMonthlyReports.ReportName + ".doc";

                                templatePath = clsMonthlyReports.ReportPath;

                                CreateNWHarvestReport clsCreateNWHarReport = new CreateNWHarvestReport(dtblMonthStats);
                                clsCreateNWHarReport.createReport(tbFBName.Text, cboReportMonth.SelectedItem.ToString(),
                                    cboReportYear.SelectedItem.ToString(), tbCounty.Text,
                                    tbPerTotalHH.Text, tbPerTotalFamily.Text, tbPreparedBy.Text, saveAs, templatePath);
                                break;
                            }
                        case "5":       //Food Life Line Report
                            //if (chkFoodLifeline.Checked == true)
                            {
                                clsMonthlyReports.find(Convert.ToInt32(lvReports.Items[i].Tag));
                                saveAs = savePath + "\\" + cboReportYear.SelectedItem.ToString()
                                    + getFormatedMonthNumber(cboReportMonth.SelectedIndex + 1) + "_"
                                    + clsMonthlyReports.ReportName + ".doc";

                                templatePath = clsMonthlyReports.ReportPath;

                                CreateFoodLifeLineReport clsCreateFLLReport = new CreateFoodLifeLineReport(dtblMonthStats);
                                clsCreateFLLReport.createReport(tbFBName.Text, cboReportMonth.SelectedItem.ToString(),
                                    cboReportYear.SelectedItem.ToString(), tbCounty.Text, tbPerTotalHH.Text, tbPerTotalFamily.Text,
                                    tbPreparedBy.Text, tbPhone.Text, tbCumLbsServedTotal.Text, saveAs, templatePath);
                                break;
                            }
                        case "6":
                            //if (chkScndHvstPantry.Checked == true)
                            {
                                clsMonthlyReports.find(Convert.ToInt32(lvReports.Items[i].Tag));
                                saveAs = savePath + "\\" + cboReportYear.SelectedItem.ToString()
                                    + getFormatedMonthNumber(cboReportMonth.SelectedIndex + 1) + "_"
                                    + clsMonthlyReports.ReportName + ".doc";

                                templatePath = clsMonthlyReports.ReportPath;

                                CreateSecHarvPantryMthlyReport clsCreateSecHarvMthRpt =
                                    new CreateSecHarvPantryMthlyReport(dtblMonthStats);
                                clsCreateSecHarvMthRpt.createReport(tbFBName.Text, cboReportMonth.SelectedItem.ToString(),
                                    cboReportYear.SelectedItem.ToString(), tbCounty.Text, city, tbPhone.Text, tbPerTotalFamily.Text,
                                    tbPreparedBy.Text, saveAs, templatePath);
                                break;
                            }
                    }
                }
            }

            findIfReportsExist();
        }

        private void btnEmailReports_Click(object sender, EventArgs e)
        {
            string fileName = "";
            string filePath = "";
            for (int i = 1; i < lvReports.Items.Count; i++)
            {
                EmailBodyInputForm frmEmailBody = new EmailBodyInputForm();
                frmEmailBody.ShowDialog();

                if (lvReports.Items[i].Checked == true)
                {
                    switch (lvReports.Items[i].Tag.ToString())
                    {
                        case "3":
                            //if (chkFBCoalitionReport.Checked == true)
                            {
                                clsMonthlyReports.find(Convert.ToInt32(lvReports.Items[i].Tag));
                                fileName = cboReportYear.SelectedItem.ToString() +
                                            getFormatedMonthNumber(cboReportMonth.SelectedIndex + 1)
                                            + "_" + clsMonthlyReports.ReportName + ".doc";

                                filePath = savePath + "\\" + cboReportYear.SelectedItem.ToString() +
                                            getFormatedMonthNumber(cboReportMonth.SelectedIndex + 1)
                                            + "_" + clsMonthlyReports.ReportName + ".doc";

                                checkFileExistsAndSendEmail(filePath, fileName, clsMonthlyReports.EmailAddresses.Replace('|', ';'), frmEmailBody.EmailBody);
                                break;
                            }
                        case "5":
                            //if (chkFoodLifeline.Checked == true)
                            {
                                clsMonthlyReports.find(Convert.ToInt32(lvReports.Items[i].Tag));
                                fileName = cboReportYear.SelectedItem.ToString() +
                                            getFormatedMonthNumber(cboReportMonth.SelectedIndex + 1)
                                            + "_" + clsMonthlyReports.ReportName + ".doc";

                                filePath = savePath + "\\" + cboReportYear.SelectedItem.ToString() +
                                            getFormatedMonthNumber(cboReportMonth.SelectedIndex + 1)
                                            + "_" + clsMonthlyReports.ReportName + ".doc";

                                checkFileExistsAndSendEmail(filePath, fileName, clsMonthlyReports.EmailAddresses.Replace('|', ';'), frmEmailBody.EmailBody);
                                break;
                            }
                        case "4":
                            //if (chkNthWstHarvest.Checked == true)
                            {
                                clsMonthlyReports.find(Convert.ToInt32(lvReports.Items[i].Tag));

                                fileName = cboReportYear.SelectedItem.ToString() +
                                            getFormatedMonthNumber(cboReportMonth.SelectedIndex + 1)
                                            + "_" + clsMonthlyReports.ReportName + ".doc";

                                filePath = savePath + "\\" + cboReportYear.SelectedItem.ToString() +
                                            getFormatedMonthNumber(cboReportMonth.SelectedIndex + 1)
                                            + "_" + clsMonthlyReports.ReportName + ".doc";

                                checkFileExistsAndSendEmail(filePath, fileName, clsMonthlyReports.EmailAddresses.Replace('|', ';'), frmEmailBody.EmailBody);
                                break;
                            }
                        case "7":
                            //if (chkScndHvstMnthly.Checked == true)
                            {
                                clsMonthlyReports.find(Convert.ToInt32(lvReports.Items[i].Tag));

                                fileName = cboReportYear.SelectedItem.ToString() +
                                            getFormatedMonthNumber(cboReportMonth.SelectedIndex + 1)
                                            + "_" + clsMonthlyReports.ReportName + ".doc";

                                filePath = savePath + "\\" + cboReportYear.SelectedItem.ToString() +
                                            getFormatedMonthNumber(cboReportMonth.SelectedIndex + 1)
                                            + "_" + clsMonthlyReports.ReportName + ".doc";

                                checkFileExistsAndSendEmail(filePath, fileName, clsMonthlyReports.EmailAddresses.Replace('|', ';'), frmEmailBody.EmailBody);
                                break;
                            }
                        case "6":
                            //if (chkScndHvstPantry.Checked == true)
                            {
                                clsMonthlyReports.find(Convert.ToInt32(lvReports.Items[i].Tag));
                                fileName = cboReportYear.SelectedItem.ToString() +
                                            getFormatedMonthNumber(cboReportMonth.SelectedIndex + 1)
                                            + "_" + clsMonthlyReports.ReportName + ".doc";

                                filePath = savePath + "\\" + cboReportYear.SelectedItem.ToString() +
                                            getFormatedMonthNumber(cboReportMonth.SelectedIndex + 1)
                                            + "_" + clsMonthlyReports.ReportName + ".doc";

                                checkFileExistsAndSendEmail(filePath, fileName, clsMonthlyReports.EmailAddresses.Replace('|', ';'), frmEmailBody.EmailBody);
                                break;
                            }
                        case "1":
                            //if (chkEFAP.Checked == true)
                            {
                                clsMonthlyReports.find(Convert.ToInt32(lvReports.Items[i].Tag));
                                fileName = cboReportYear.SelectedItem.ToString() +
                                            getFormatedMonthNumber(cboReportMonth.SelectedIndex + 1)
                                            + "_" + clsMonthlyReports.ReportName + ".doc";

                                filePath = savePath + "\\" + cboReportYear.SelectedItem.ToString() +
                                            getFormatedMonthNumber(cboReportMonth.SelectedIndex + 1)
                                            + "_" + clsMonthlyReports.ReportName + ".doc";

                                checkFileExistsAndSendEmail(filePath, fileName, clsMonthlyReports.EmailAddresses.Replace('|', ';'), frmEmailBody.EmailBody);
                                
                                break;
                            }
                    }
                }
            }
        }

        private void checkFileExistsAndSendEmail(string filePath, string fileName, string emailAddresses, string EmailBody)
        {
            if (File.Exists(filePath) == true)
            {
                sendEmail(filePath, fileName, emailAddresses, EmailBody);
            }
            else
            {
                MessageBox.Show("Report " + fileName + " Does Not Exist.  Please Create Report And Try Again", "Report Does Not Exist",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void sendEmail(string filePath, string fileName, string emailList, string emailBody)
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
                oMailItem.Subject = fileName + " From " + tbFBName.Text; 
                oMailItem.Body = emailBody;
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
                        fileName = @"C:\ClientcardFB3\Reports\" + cboReportYear.SelectedItem.ToString() +
                            getFormatedMonthNumber(cboReportMonth.SelectedIndex + 1) +
                            "_" + clsMonthlyReports.ReportName + ".doc";

                        CCFBGlobal.openDocumentOutsideCCFB(fileName);
                    }
                    catch (System.Exception ex)
                    {
                        CCFBGlobal.appendErrorToErrorReport("FileName=" + fileName.ToString(), ex.GetBaseException().ToString());
                        MessageBox.Show("The File " + fileName.ToString() + " Does Not Exist");
                    }
                }
            }
            //foreach (CheckBox cb in pnlReports.Controls.OfType<CheckBox>())
            //{
            //    if (cb.Checked == true && cb.BackColor == Color.Red)
            //    {
            //        try
            //        {
            //            clsMonthlyReports.find(Convert.ToInt32(cb.Tag));
            //            fileName = @"C:\ClientcardFB3\Reports\" + cboReportYear.SelectedItem.ToString() +
            //                getFormatedMonthNumber(cboReportMonth.SelectedIndex + 1) +
            //                "_" + clsMonthlyReports.ReportName + ".doc";

            //            CCFBGlobal.openDocumentOutsideCCFB(fileName);
            //        }
            //        catch (System.Exception ex)
            //        {
            //            CCFBGlobal.appendErrorToErrorReport("FileName=" + fileName.ToString(), ex.GetBaseException().ToString());
            //            MessageBox.Show("The File " + fileName.ToString() + " Does Not Exist");
            //        }
            //    }
           // }
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
                        if (ctrl.Name.ToString().StartsWith("tbPer") == true)
                            tbPerList.Add((TextBox)ctrl);
                        else if (ctrl.Name.ToString().StartsWith("tbCum") == true)
                            tbCumList.Add((TextBox)ctrl);
                        else if (ctrl.Name.ToString().StartsWith("tbLbsRcvdPer") == true)
                            tbLbsPerList.Add((TextBox)ctrl);
                        else if (ctrl.Name.ToString().StartsWith("tbLbsRcvdCum") == true)
                            tbLbsCumList.Add((TextBox)ctrl);
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
        private string getFoodRecieptsByDonorSQL(string PeriodStart, string FiscalStart)
        {
            string PeriodEnd = Convert.ToDateTime(PeriodStart).AddMonths(1).AddDays(-1).ToShortDateString();
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
                       + " WHERE NOT Exists(SELECT * FROM Preferences WHERE LEFT(FldName,7) = 'DonorId' AND CAST(FldVal as Int) = fd.DonorId)"
                       + "   AND TrxDate Between '" + PeriodStart + "' AND '" + PeriodEnd + "'"
                       + " UNION " + Environment.NewLine
                       + "SELECT 'Cum' as RcdType, p.FldName, d.ID AS Donor, d.Name, Sum(CASE WHEN fd.Pounds IS NULL THEN 0 ELSE fd.Pounds END)  AS LBS"
                       + "  FROM Preferences p "
                       + "  LEFT JOIN Donors d ON CAST(p.FldVal AS int) = d.ID "
                       + "  LEFT JOIN foodDonations fd ON d.ID = fd.DonorId AND fd.TrxDate Between '" + FiscalStart + "' AND '" + PeriodEnd + "'"
                       + " WHERE LEFT(p.FldName,7) = 'DonorId' AND p.FldVal >'0'"
                       + " GROUP BY FldName, d.Id, d.Name"
                       + " UNION " + Environment.NewLine
                       + "SELECT 'Cum' as RcdType, Min('Other') AS FldName, MIN(99999) AS Donor, 'OTHER' AS Name, Sum(Pounds)  AS LBS"
                       + "  FROM foodDonations fd"
                       + " WHERE NOT Exists(SELECT * FROM Preferences WHERE LEFT(FldName,7) = 'DonorId' AND CAST(FldVal as Int) = fd.DonorId)"
                       + "   AND TrxDate Between '" + FiscalStart + "' AND '" + PeriodEnd + "'" + Environment.NewLine
                       + " ORDER BY FldName, RcdType Desc";
            return sql;
        }

        private string getFoodRecieptsByFundingSQL(string PeriodEnd, string FiscalStart)
        {
            string sql = "SELECT SvcDate"
                       + ", sum(CAST(CASE WHEN [1] IS NULL THEN 0 else [1] END AS INT)) AS LbsEFAP"
                       + ", sum(CAST(CASE WHEN [2] IS NULL THEN 0 else [2] END AS INT)) AS LbsTEFAP"
                       + ", sum(CAST(CASE WHEN [3] IS NULL THEN 0 else [3] END AS INT)) AS LbsOtherPurchased"
                       + ", sum(CAST(CASE WHEN [4] IS NULL THEN 0 else [4] END AS INT)) AS LbsInkind"
                       + ", sum(CAST(CASE WHEN [5] IS NULL THEN 0 else [5] END AS INT)) AS LbsEFSP"
                       + ", sum(CAST(CASE WHEN [6] IS NULL THEN 0 else [6] END AS INT)) AS LbsGroceryRescue"
                       + ", sum(CAST(CASE WHEN [7] IS NULL THEN 0 else [7] END AS INT)) AS LbsCSFP"
                       + " FROM "
                       + "(SELECT left(Convert(varchar(10),TrxDate,112),6) SvcDate, DonationType, Pounds"
                       + " FROM FoodDonations ) fd"
                       + " PIVOT ( SUM (Pounds)"
                       + " FOR DonationType IN ( [1], [2], [3], [4],[5],[6], [7], [8], [9]) ) AS pvt"
                       + " WHERE SvcDate = '" + PeriodEnd.Substring(0, 6) + "'"
                       + " Group By SvcDate "
                       + "UNION "
                       + "SELECT left(SvcDate,4) + '99'"
                       + ", sum(CAST(CASE WHEN [1] IS NULL THEN 0 else [1] END AS INT)) AS LbsEFAP"
                       + ", sum(CAST(CASE WHEN [2] IS NULL THEN 0 else [2] END AS INT)) AS LbsTEFAP"
                       + ", sum(CAST(CASE WHEN [3] IS NULL THEN 0 else [3] END AS INT)) AS LbsOtherPurchased"
                       + ", sum(CAST(CASE WHEN [4] IS NULL THEN 0 else [4] END AS INT)) AS LbsInkind"
                       + ", sum(CAST(CASE WHEN [5] IS NULL THEN 0 else [5] END AS INT)) AS LbsEFSP"
                       + ", sum(CAST(CASE WHEN [6] IS NULL THEN 0 else [6] END AS INT)) AS LbsGroceryRescue"
                       + ", sum(CAST(CASE WHEN [7] IS NULL THEN 0 else [7] END AS INT)) AS LbsCSFP"
                       + " FROM "
                       + "(SELECT Convert(varchar(10),TrxDate,112) SvcDate, DonationType, Pounds"
                       + " FROM FoodDonations ) fd"
                       + " PIVOT ( SUM (Pounds)"
                       + " FOR DonationType IN ( [1], [2], [3], [4],[5],[6], [7], [8], [9]) ) AS pvt"
                       + " WHERE SvcDate between '" + FiscalStart + "' AND '" + PeriodEnd + "'"
                       + " GROUP BY left(SvcDate,4)";
            return sql;
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
                        dt.Rows.Add(values);
                    }
                }
            }
            catch { };
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
            string imageSavePath = @"C:\ClientCardFB3\Reports\temp\";
            CCFBGlobal.ClearFolder(imageSavePath);

            List<Panel> pnlList = new List<Panel>();
            pnlList.Add(pnlHousehold);
            pnlList.Add(pnlIndividuals);
            pnlList.Add(pnlLbsFoodServed);
            pnlList.Add(pnlLbsFoodReceipts);
            pnlList.Add(pnlFoodReceiptsByDonor);
            pnlList.Add(spltCFamilySize.Panel1);
            pnlList.Add(spltCFamilySize.Panel2);
            pnlList.Add(spltCStats.Panel1);
            pnlList.Add(spltCStats.Panel2);
            pnlList.Add(pnlStatistics);
            //pnlList.Add(pnlFBInfo);
            foreach(Panel pnl in pnlList)
            {
                if (pnl.Tag != null)
                {
                    switch (pnl.Tag.ToString())
                    {
                        case "Households":
                        case "Individuals":
                            {
                                tabControl1.SelectTab(0);
                                System.Windows.Forms.Application.DoEvents();
                                break;
                            }
                        case "LbsFoodServed":
                        case "LbsFoodReceipts":
                        case "FoodReceiptsByDonor":
                            {
                                tabControl1.SelectTab(1);
                                System.Windows.Forms.Application.DoEvents();
                                break;
                            }
                        case "FamilySizeByDay":
                        case "FamilySizeByMonth":
                            {
                                tabControl1.SelectTab(2);
                                System.Windows.Forms.Application.DoEvents();
                                break;
                            }
                        case "StatsByDay":
                        case "StatsByMonth":
                            {
                                tabControl1.SelectTab(3);
                                System.Windows.Forms.Application.DoEvents();
                                break;
                            }
                        case "Statistics":
                            {
                                tabControl2.SelectTab(0);
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

            clsMonthlyReports.find(3);
            string saveAs = savePath + "\\" + cboReportYear.SelectedItem.ToString()
                + getFormatedMonthNumber(cboReportMonth.SelectedIndex + 1) + "_" +
                clsMonthlyReports.ReportName + ".doc";

            string templatePath = @"C:\ClientCardFB3\Templates\Coalition.doc";

            CreateCoalitionReport clsCreateCoalitionReport = new CreateCoalitionReport(dtblMonthStats);
            clsCreateCoalitionReport.createReport(tbFBName.Text, cboReportMonth.SelectedItem.ToString(),
                tbPerTotalFamily.Text, tbCumTotalFamily.Text, tbPerTotalHH.Text, tbCumTotalHH.Text,
                tbPerLbsServedTotal.Text, tbCumLbsServedTotal.Text, tbPreparedBy.Text, saveAs, templatePath, imageSavePath);
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
