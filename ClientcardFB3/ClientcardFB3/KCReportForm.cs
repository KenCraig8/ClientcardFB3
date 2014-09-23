using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ClientcardFB3
{
    public partial class KCReportForm : Form
    {
        SqlDataAdapter dadAdpt;
        DataSet dset;
        //SqlCommand command;
        SqlConnection conn;
        string tbName = "TrxLog";
        string savePath = CCFBPrefs.ReportsSavePath;
        DataTable dtDemographics;
        DataTable dtByZip;
        DateTime startDate;
        DateTime endDate;
        int rowCount = 0;

        RptKCCDBG clsCreateKCReport;

        string sqlCommandText = "Select Distinct DateName(yy, TrxDate) as 'Year' "
    + "From TrxLog where TrxDate is not null Order By 'Year' DESC";

        public KCReportForm()
        {
            InitializeComponent();
            dtDemographics = new DataTable();
            dtByZip = new DataTable();
            dadAdpt = new SqlDataAdapter();
            dset = new DataSet();
            conn = new SqlConnection(CCFBGlobal.connectionString);
            SqlCommand command = new SqlCommand(sqlCommandText, conn);
            dadAdpt.SelectCommand = command;
            getDistinctYears();
            fillYearsCombo();
        }

        private void fillYearsCombo()
        {
            cboReportYear.Items.Clear();
            for (int i = 0; i < rowCount; i++)
            {
                cboReportYear.Items.Add(dset.Tables[tbName].Rows[i]["Year"]);
                
                if (dset.Tables[tbName].Rows[i]["Year"].ToString() == DateTime.Today.Year.ToString())
                    cboReportYear.SelectedIndex = i;
            }

            if (cboReportYear.SelectedIndex < 0 && cboReportYear.Items.Count > 0)
                cboReportYear.SelectedIndex = 0;

            switch (DateTime.Today.Month)
            {
                case 1:
                case 2:
                case 3:
                    {
                        if (cboReportYear.Items.Count > 1)
                        {
                            cboReportYear.SelectedIndex = 1;
                            cboQuarter.SelectedIndex = 3;
 
                        }
                        else
                        {
                            cboQuarter.SelectedIndex = 0;
                        }
                        break;
                    }

                case 4:
                case 5:
                case 6:
                    {
                        cboQuarter.SelectedIndex = 0;
                        break;
                    }
                case 7:
                case 8:
                case 9:
                    {
                        cboQuarter.SelectedIndex = 1;
                        break;
                    }
                case 10:
                case 11:
                case 12:
                    {
                        cboQuarter.SelectedIndex = 2;
                        break;
                    }
            }
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCreateReport_Click(object sender, EventArgs e)
        {
            try
            {
                dtDemographics.Clear();
                SqlCommand cmd = new SqlCommand("CalendarQuarterStats", conn);
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = cmd.Parameters.Add("@StartDate", SqlDbType.VarChar);
                param.Value = CCFBGlobal.formatDate(startDate);
                SqlParameter param2 = cmd.Parameters.Add("@EndDate", SqlDbType.VarChar);
                param2.Value = CCFBGlobal.formatDate(endDate);
                SqlParameter param3 = cmd.Parameters.Add("@County", SqlDbType.VarChar);
                param3.Value = CCFBPrefs.County;
                SqlParameter param4 = cmd.Parameters.Add("@State", SqlDbType.VarChar);
                param4.Value = CCFBPrefs.DefaultState;

                openConnection();

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dtDemographics);
                }

                closeConnection();

                cmd = new SqlCommand("TrxLogTotalFamilyByZipByQuarter", conn);
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;

                param = cmd.Parameters.Add("@StartDate", SqlDbType.VarChar);
                param.Value = CCFBGlobal.formatDate(startDate);
                param2 = cmd.Parameters.Add("@EndDate", SqlDbType.VarChar);
                param2.Value = CCFBGlobal.formatDate(endDate);
                param3 = cmd.Parameters.Add("@County", SqlDbType.VarChar);
                param3.Value = CCFBPrefs.County;
                param4 = cmd.Parameters.Add("@State", SqlDbType.VarChar);
                param4.Value = CCFBPrefs.DefaultState;

                openConnection();
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dtByZip);
                }
                //TrxLogTotalFamilyByZipByQuarter

                clsCreateKCReport = new RptKCCDBG(dtDemographics, dtByZip);
                this.Cursor = Cursors.WaitCursor;
                clsCreateKCReport.createExport(System.IO.Path.Combine(CCFBGlobal.pathReports,@"2011 CDBG Demographics Worksheet" + "_" 
                    + CCFBPrefs.FoodBankName + "_"
                    + DateTime.Today.Year + "_" + DateTime.Today.Month + ".xls"),
                        CCFBGlobal.fb3TemplatesPath + "2011 CDBG Demographics Worksheet.xls", Convert.ToInt32(cboReportYear.SelectedItem));
                this.Cursor = Cursors.Default;
            }
            catch (SqlException ex)
            {
                CCFBGlobal.appendErrorToErrorReport("KCReportForm", ex.GetBaseException().ToString());
                closeConnection();
            }
        }

        /// <summary>
        /// Opens a connection to the Database
        /// </summary>
        private void openConnection()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
        }

        /// <summary>
        /// Closes a connection to the Database
        /// </summary>
        private void closeConnection()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        private void cbo_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cboQuarter.SelectedIndex)
            {
                case 0:
                    {
                        startDate = new DateTime(Convert.ToInt32(cboReportYear.SelectedItem), 1, 1);
                        endDate = new DateTime(Convert.ToInt32(cboReportYear.SelectedItem), 3, 31);
                        break;
                    }            
                case 1:
                    {
                        startDate = new DateTime(Convert.ToInt32(cboReportYear.SelectedItem), 1, 1);
                        endDate = new DateTime(Convert.ToInt32(cboReportYear.SelectedItem), 6, 30);
                        break;
                    }
                case 2:
                    {
                        startDate = new DateTime(Convert.ToInt32(cboReportYear.SelectedItem), 1, 1);
                        endDate = new DateTime(Convert.ToInt32(cboReportYear.SelectedItem), 9, 30);
                        break;
                    }
                case 3:
                    {
                        startDate = new DateTime(Convert.ToInt32(cboReportYear.SelectedItem), 1, 1);
                        endDate = new DateTime(Convert.ToInt32(cboReportYear.SelectedItem), 12, 31);
                        break;
                    }
            }
        }
    }
}
