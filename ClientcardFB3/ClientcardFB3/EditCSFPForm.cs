using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ClientcardFB3
{
    public partial class EditCSFPForm : Form
    {
        CSFPLog clsCSFPLog = new CSFPLog(CCFBGlobal.connectionString);

        SqlDataAdapter dadAdpt;
        DataTable dtbl;
        SqlCommand command;
        SqlCommand updateAndInsertComm;
        SqlConnection conn;
        List<ComboBox> cboSort = new List<ComboBox>();
        string sqlCommandText = "Select Distinct DateName(yy, TrxDate) as 'Year' "
            + "From CSFPLog where TrxDate is not null Order By 'Year' DESC";
        int rowCount = 0;
        string lastSearchText = "";
        int rowIndex = 0;
        int hhID = 0;
        bool loading = false;
        string filterFldName = "";
        bool isShortDate = false;
        DateTime svcDate = DateTime.Today;
        string savePath = CCFBPrefs.ReportsSavePath;
        const string nofilter = "<No Filter>";

        public EditCSFPForm()
        {
            InitializeComponent();
            cboSort.Add(cboOrderBy0);
            cboSort.Add(cboOrderBy1);
            cboSort.Add(cboOrderBy2);
            loading = true;
            setcboSortOrder(0);
            cboSort[0].SelectedIndex = 3;
            setcboSortOrder(1);
            setcboSortOrder(2);

            dtpSvcDate.Value = DateTime.Today;
            dadAdpt = new SqlDataAdapter();
            dtbl = new DataTable();
            conn = new SqlConnection(CCFBGlobal.connectionString);
            SqlCommand selectComm = new SqlCommand(sqlCommandText, conn);
            
            dadAdpt.SelectCommand = selectComm;
            getDistinctYears();
            fillYearsCombo();

            //cboOrderBy0.SelectedIndex = 0;
            //getDefaultCSFPLbs();
            clsCSFPLog.openWhere("");
            tbsMarkNewCSFP.Text = "New CSFP " + Environment.NewLine + "Client";

            bool connected = CCFBGlobal.IsConnectedToInternet();
            menuFNSWebSite.Enabled = connected;
            menuCSFPFederal.Enabled = connected;
            menuCSFPState.Enabled = connected;

            if (cboYear.Items.Count < 1)
                btnRefreshList.Enabled = false;

            cboMonth.SelectedIndex = DateTime.Today.Month - 1;
            loading = false;
        }

        private void EditCSFPForm_Load(object sender, EventArgs e)
        {
            gbRenewExpDate.Left = gbNewService.Left;
            gbRenewExpDate.Top = gbNewService.Top;
            gbNewService.BackColor = CCFBGlobal.bkColorAltEdit;
            gbRenewExpDate.BackColor = CCFBGlobal.bkColorAltEdit;
        }

        private void fillYearsCombo()
        {
            cboYear.Items.Clear();
            if (dtbl.Rows.Count > 0)
            {
                for (int i = 0; i < dtbl.Rows.Count; i++)
                {
                    cboYear.Items.Add(dtbl.Rows[i]["Year"]);

                    if (dtbl.Rows[i]["Year"].ToString() == DateTime.Today.Year.ToString())
                        cboYear.SelectedIndex = i;

                    if (DateTime.Today.Day > 10)
                        cboMonth.SelectedIndex = DateTime.Today.Month - 1;
                    else if (DateTime.Today.Month == 1)
                        cboMonth.SelectedIndex = 11;
                    else
                        cboMonth.SelectedIndex = DateTime.Today.Month - 2;
                }
            }
            else
                cboYear.Items.Add(DateTime.Today.Year.ToString());

            if (cboYear.SelectedIndex == -1 && cboYear.Items.Count > 0)
                cboYear.SelectedIndex = 0;
        }

        private void getDistinctYears()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                dadAdpt.Fill(dtbl);

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

        private void btnLoad_Click(object sender, EventArgs e)
        {
            getClientsList();
            toolStrip1.Enabled = true;
        }

        /// <summary>
        /// Gets the CSFP Clients for the given period using a stored procedure
        /// in the database
        /// </summary>
        private void getClientsList()
        {
            if (cboYear.SelectedIndex != -1 && cboMonth.SelectedIndex != -1)
            {
                btnRefreshList.Enabled = false;
                progressBar1.Value = 0;
                progressBar1.Show();
                try
                {
                    dtbl = new DataTable();
                    openConnection();

                    command = new SqlCommand("CSFPListByPeriod", conn);
                    command.CommandType = CommandType.StoredProcedure;

                    // Handle the parameters 
                    SqlParameter parameter = new SqlParameter();
                    parameter.ParameterName = "@Period";
                    parameter.SqlDbType = SqlDbType.Char;
                    parameter.Direction = ParameterDirection.Input;
                    parameter.Value = cboYear.SelectedItem.ToString()
                        + CCFBGlobal.formatNumberWithLeadingZero((cboMonth.SelectedIndex + 1));

                    SqlParameter parameter2 = new SqlParameter();
                    parameter2.ParameterName = "@SortBy";
                    parameter2.SqlDbType = SqlDbType.NVarChar;
                    parameter2.Direction = ParameterDirection.Input;
                    parameter2.Value = getOrderByText();

                    //SqlParameter parameter3 = new SqlParameter();
                    //parameter3.ParameterName = "@SubSort";
                    //parameter3.SqlDbType = SqlDbType.SmallInt;
                    //parameter3.Direction = ParameterDirection.Input;
                    //parameter3.Value = subSortIndexValue();
                    
                    // Add the parameter to the Parameters collection. 
                    command.Parameters.Add(parameter);
                    command.Parameters.Add(parameter2);
                    //command.Parameters.Add(parameter3);

                    using (SqlDataAdapter da = new SqlDataAdapter(command))
                    {
                        rowCount = da.Fill(dtbl);
                    }
                    closeConnection();
                    loadList(dtbl.Select());
                    initFilterInfo();              
                    svcDate = new DateTime(Convert.ToInt32(cboYear.SelectedItem), 
                                            cboMonth.SelectedIndex + 1, 1);
                    try
                    {
                        dtpSvcDate.MaxDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month,
                            DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month)).AddDays(14);
                        //dtpSvcDate.MinDate = svcDate;
                    }
                    catch (ArgumentOutOfRangeException ex)
                    {
                        //dtpSvcDate.MinDate = svcDate;
                        dtpSvcDate.MaxDate = new DateTime(svcDate.Year, svcDate.Month,
                            DateTime.DaysInMonth(svcDate.Year, svcDate.Month)).AddDays(14);                     }
                }
                catch (SqlException ex)
                {
                    closeConnection();
                    dgvCSFP.Rows.Clear();
                    setFindClientVisible();
                    CCFBGlobal.appendErrorToErrorReport("Period=" + cboYear.SelectedItem.ToString()
                        + CCFBGlobal.formatNumberWithLeadingZero((cboMonth.SelectedIndex + 1)),
                        ex.GetBaseException().ToString());
                }
                btnRefreshList.Enabled = true;
            }
        }

        /// <summary>
        /// Formats the Month To have a Leading Zero if month less than 10
        /// </summary>
        /// <param name="month">The Integer Month to Format</param>
        /// <returns>The Formated Month As A String</returns>
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

        private void setFindClientVisible()
        {
            if (dgvCSFP.Rows.Count > 0)
                changeGroupBoxVisibility(0);
            else
                gbFilter.Visible = false;
        }

        /// <summary>
        /// Loads the DataGrid from the DataRows that are passed to the funtion
        /// </summary>
        /// <param name="drows">The Array of DataRows to load the data from</param>
        private void loadList(DataRow[] drows)
        {
            if (progressBar1.Visible == false)
            {
                btnRefreshList.Visible = false;
                progressBar1.Value = 0;
                progressBar1.Visible = true;
                progressBar1.Maximum = drows.Count();
            }
            tbFindName.Text = "";
            lastSearchText = "";
            dgvCSFP.Rows.Clear();
            tbFindName.Visible = false;
            Application.DoEvents();

            DateTime toCheck = new DateTime(Convert.ToInt32(cboYear.SelectedItem),
                        Convert.ToInt32(cboMonth.SelectedIndex + 1), 1);

            for (int i = 0; i < drows.Length; i++)
            {

                dgvCSFP.Rows.Add();

                if (Convert.ToInt32(drows[i]["CSFP"]) == 0)
                    dgvCSFP.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                else if (Convert.ToInt32(drows[i]["Inactive"]) == 1)
                {
                    dgvCSFP.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                    dgvCSFP.Rows[i].DefaultCellStyle.BackColor = Color.White;
                }

                dgvCSFP["HouseholdID", i].Value = drows[i]["HouseholdID"];
                dgvCSFP["clmName", i].Value = drows[i]["colNameLF"];
                dgvCSFP["Address", i].Value = drows[i]["Address"];
                dgvCSFP["AptNbr", i].Value = drows[i]["AptNbr"];
                dgvCSFP["CSFPExpiration", i].Value = convertToShortDate(
                    CCFBGlobal.NullToBlank(drows[i]["CSFPExpiration"]));
                if (dgvCSFP["CSFPExpiration", i].Value.ToString() != "" &&
                    (DateTime)drows[i]["CSFPExpiration"] < toCheck)
                {
                    dgvCSFP["CSFPExpiration", i].Style.BackColor = Color.Yellow;
                }
                dgvCSFP["Route", i].Value = drows[i]["Route"];
                dgvCSFP["clmMethodAsInt", i].Value = drows[i]["RouteAsInt"];
                dgvCSFP["clmDateServed", i].Value = convertToShortDate(
                    CCFBGlobal.NullToBlank(drows[i]["TrxDate"]));
                dgvCSFP["clmLbs", i].Value = CCFBGlobal.NullToBlank(drows[i]["Lbs"]);
                dgvCSFP["clmHHMemID", i].Value = CCFBGlobal.NullToBlank(drows[i]["hhmID"]);
                dgvCSFP.Rows[i].Cells["colNameLF"].Value = drows[i]["colNameLF"].ToString().ToUpper().Trim();
                dgvCSFP.Rows[i].Cells["colNameFL"].Value = drows[i]["colNameFL"].ToString().ToUpper().Trim();
                dgvCSFP.Rows[i].Cells["clmCSFP"].Value = drows[i]["CSFP"];
                dgvCSFP.Rows[i].Cells["clmLogID"].Value = drows[i]["LogId"];
                dgvCSFP.Rows[i].Cells["clmPhone"].Value = drows[i]["Phone"].ToString().Trim();
                progressBar1.PerformStep();
            }

            rowIndex = 0;
            progressBar1.Value = 0;
            progressBar1.Visible = false;
            btnRefreshList.Visible = true;
            setFindClientVisible();
            tbFindName.Visible = true;
            Application.DoEvents();
        }

        private string convertToShortDate(string date)
        {
            if(date != "")
            {
                return DateTime.Parse(date).ToShortDateString();
            }
            return "";
        }

        private void FindByName(string colNameFull)
        {
            int rowStart = 0;
            if (tbFindName.TextLength >= lastSearchText.Length)
                rowStart = rowIndex;
            else
                rowStart = 0;
            lastSearchText = tbFindName.Text.ToUpper().Trim();
            for (int i = rowStart; i < dgvCSFP.Rows.Count; i++)
            {
                if (dgvCSFP.Rows[i].Cells[colNameFull].FormattedValue.ToString().ToUpper().StartsWith(lastSearchText) == true)
                {
                    dgvCSFP.CurrentCell = dgvCSFP[0, i];
                    if (i < dgvCSFP.FirstDisplayedScrollingRowIndex
                        || i > dgvCSFP.Rows.GetLastRow(DataGridViewElementStates.Displayed) - 5)
                        if (i > 5)
                            dgvCSFP.FirstDisplayedScrollingRowIndex = i - 5;
                        else
                            dgvCSFP.FirstDisplayedScrollingRowIndex = i;
                    break;
                }
            }
        }

        private void tbFindName_TextChanged(object sender, EventArgs e)
        {
            if (tbFindName.Text.Trim() == "")
            { dgvCSFP.CurrentCell = dgvCSFP[0, 0]; }
            else
            {
                if (cboOrderBy0.SelectedIndex>0)
                {
                    FindByName(((parmType)cboOrderBy0.Items[cboOrderBy0.SelectedIndex]).ShortName);
                }
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

        private void cboFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            //getCSFPClients();
            if (loading == false)
            {
                if (rowCount > 0 && cboFilter.Text != nofilter)
                {
                    DataRow[] drows;
                    drows = dtbl.Select(filterFldName + "='" + cboFilter.SelectedItem.ToString() + "'");
                    loadList(drows);
                }
                else if (cboFilter.Text == nofilter)
                {
                    loadList(dtbl.Select());
                }
            }
        }

        private void getDistincts(string colName, bool fmtAsShortdate)
        {
            filterFldName = colName;
            isShortDate = fmtAsShortdate;
            fillFilterCombo();
        }

        private void fillFilterCombo()
        {
            cboFilter.Visible = true;
            DataView Dv = dtbl.DefaultView;
            DataTable dtDistinct = Dv.ToTable(true, filterFldName);
            loading = true;
            cboFilter.Items.Clear();
            cboFilter.Items.Add(nofilter);
            cboFilter.SelectedIndex = 0;
            for (int i = 0; i < dtDistinct.Rows.Count; i++)
            {
                if (isShortDate==true)
                    cboFilter.Items.Add(CCFBGlobal.ValidDate(dtDistinct.Rows[i][0]).ToShortDateString());
                else
                    cboFilter.Items.Add(dtDistinct.Rows[i][0].ToString());
            }
            loading = false;
        }

        private void cboOrderBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (loading == false)
            {
                ComboBox cbo = (ComboBox)sender;
                getClientsList();
                for (int i = Convert.ToInt32(cbo.Tag)+1; i < 3; i++)
                {
                    setcboSortOrder(i);
                }
            }
        }

        private void EditExpiration_Click(object sender, EventArgs e)
        {
            if (dgvCSFP.SelectedRows.Count > 0)
            {
                changeGroupBoxVisibility(1);
                DateTime baseDate = DateTime.Parse("01/01/2000");
                DateTime dtTmp = baseDate;
                foreach (DataGridViewRow  dgvRow in dgvCSFP.SelectedRows)
                {
                    if (dgvRow.Cells["CSFPExpiration"].Value.ToString() != "")
                    {
                        try
                        {
                            dtTmp = DateTime.Parse(dgvRow.Cells["CSFPExpiration"].Value.ToString());
                        }
                        catch { dtTmp = baseDate; }
                    }
                    if (dtTmp > baseDate)
                    {
                        baseDate = dtTmp;
                    }
                }
                if (baseDate != DateTime.Parse("01/01/2000"))
                {
                    dtpExpDate.Value = baseDate;
                }
                else
                {
                    dtpExpDate.Value = DateTime.Today;
                }
            }
            else
            {
                MessageBox.Show("Please Select A Client To Renew And Try Again", 
                    "Renewal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            changeGroupBoxVisibility(0);
        }

        private void changeGroupBoxVisibility(int option)
        {
            string tmpdate = "";
            switch (option)
            {
                case 0:
                    {
                        gbFilter.Visible = true;
                        gbRenewExpDate.Visible = false;
                        gbNewService.Visible = false;
                        cboYear.Enabled = true;
                        cboMonth.Enabled = true;
                        btnRefreshList.Enabled = true;
                        dgvCSFP.Enabled = true;
                        break;
                    }
                case 1:
                    {
                        gbFilter.Visible = false;
                        gbNewService.Visible = false;
                        gbRenewExpDate.Visible = true;
                        cboYear.Enabled = false;
                        cboMonth.Enabled = false;
                        btnRefreshList.Enabled = false;
                        dgvCSFP.Enabled = false;

                        tmpdate = CCFBGlobal.NullToBlank(dgvCSFP.SelectedRows[0].Cells["CSFPExpiration"].Value);

                        if (tmpdate != "")
                        {
                            dtpExpDate.Value = DateTime.Parse(tmpdate);
                        }
                        else
                        {
                            dtpExpDate.Value = DateTime.Parse(DateTime.Today.ToShortDateString());
                        }
                        chkCSFP.Checked = (bool)dgvCSFP.SelectedRows[0].Cells["clmCSFP"].Value;
                        break;
                    }
                case 2:
                    {
                        cboYear.Enabled = false;
                        cboMonth.Enabled = false;
                        btnRefreshList.Enabled = false;
                        dgvCSFP.Enabled = false;
                        gbFilter.Visible = false;
                        gbNewService.Visible = true;
                        gbRenewExpDate.Visible = false;
                        tbLbsCSFP.Text = CCFBPrefs.CSFPLbsPerService.ToString();
                        dtpSvcDate.Value = svcDate;
                        tmpdate = CCFBGlobal.NullToBlank(dgvCSFP.SelectedRows[0].Cells["clmDateServed"].Value);

                        if (tmpdate != "")
                        {
                            dtpSvcDate.Value = DateTime.Parse(tmpdate);
                        }
                        else
                        {
                            dtpSvcDate.Value = DateTime.Parse(DateTime.Today.ToShortDateString());
                        }
                        break;
                    }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int checkState = 0;

            if (chkCSFP.Checked == true)
                checkState = 1;
            for (int i = 0; i < dgvCSFP.SelectedRows.Count; i++)
            {
                try
                {
                    openConnection();
                    updateAndInsertComm = new SqlCommand("Update HouseholdMembers Set CSFPExpiration =' "
                        + dtpExpDate.Value.ToString() + "', CSFP = " + checkState.ToString()
                    + " Where ID=" + dgvCSFP.SelectedRows[i].Cells["clmHHMemID"].Value.ToString(), conn);
                    updateAndInsertComm.ExecuteNonQuery();
                    closeConnection();
                }
                catch (SqlException ex)
                {
                    closeConnection();
                    CCFBGlobal.appendErrorToErrorReport("CSFPExpiration = " + dtpExpDate.Value.ToString()
                        + " ID=" + dgvCSFP.SelectedRows[i].Cells["clmHHMemID"].ToString(), ex.GetBaseException().ToString());
                }
            }
            changeGroupBoxVisibility(0);
            if (cboFilter.Visible == true)
                cboFilter_SelectedIndexChanged(cboFilter, EventArgs.Empty);
            else
                cboOrderBy_SelectedIndexChanged(cboOrderBy0, EventArgs.Empty);
           
        }

        private void MarkNewCSFPClient_Click(object sender, EventArgs e)
        {
            FindClientForm frmFindClient = new FindClientForm();
            frmFindClient.ShowDialog();

            if (frmFindClient.Canceled == false)
            {
                hhID = frmFindClient.CurrentHHId;
                frmFindClient.Close();

                NewCSFPSelectionForm frmNewCSFP = new NewCSFPSelectionForm(hhID);
                frmNewCSFP.ShowDialog();
                if (cboFilter.Visible == true)
                    cboFilter_SelectedIndexChanged(cboFilter, EventArgs.Empty);
                else
                    cboOrderBy_SelectedIndexChanged(cboOrderBy0, EventArgs.Empty);
           
            }
        }

        private void GiveCSFPService_Click(object sender, EventArgs e)
        {
            if (dgvCSFP.SelectedRows.Count > 0)
            {
                changeGroupBoxVisibility(2);
            }
            else
                MessageBox.Show("No Clients Have Been Selected.  Please Select Clients And Try Again.", "ERROR",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnSvcCancel_Click(object sender, EventArgs e)
        {
            changeGroupBoxVisibility(0);
        }

        private void updateExistingService(string updateIDs)
        {
            try
            {
                updateAndInsertComm = new SqlCommand("Update CSFPLog Set TrxDate='" + dtpSvcDate.Value.ToString() + "', "
                          + "Lbs=" + tbLbsCSFP.Text + ", Modified='" + DateTime.Now.ToString() + "', "
                          + "ModifiedBy='" + CCFBGlobal.currentUser_Name + "' "
                          + "Where ID in (" + updateIDs + ")", conn);
                updateAndInsertComm.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString());
            }
        }

        private void btnSvcSave_Click(object sender, EventArgs e)
        {
            string insertIDs = "";
            string updateIDs = "";
            for (int i = 0; i < dgvCSFP.SelectedRows.Count; i++)
            {
                if (dgvCSFP.SelectedRows[i].Cells["clmDateServed"].Value.ToString() != "")
                {
                    if (MessageBox.Show("CSFP Client " + dgvCSFP.SelectedRows[i].Cells["clmName"].Value.ToString()
                         + " Already Has A Service For This Period. Would You Like To Update The Service?",
                         "Service Already Exists For This Period", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                        == System.Windows.Forms.DialogResult.Yes)
                    {
                        if (updateIDs != "")
                            updateIDs += ", ";

                        updateIDs += dgvCSFP.SelectedRows[i].Cells["clmLogID"].Value.ToString();
                    }
                }
                else
                {
                    if (insertIDs != "")
                        insertIDs += ", ";

                    insertIDs += dgvCSFP.SelectedRows[i].Cells["clmHHMemID"].Value.ToString();
                }
                
            }

            openConnection();
            if (insertIDs != "")
                clsCSFPLog.insertNewService(insertIDs, dtpSvcDate.Value, tbLbsCSFP.Text);

            if (updateIDs != "")
                updateExistingService(updateIDs);
            svcDate = dtpSvcDate.Value;
            closeConnection();
            changeGroupBoxVisibility(0);
            if (cboFilter.Visible == true)
                cboFilter_SelectedIndexChanged(cboFilter, EventArgs.Empty);
            else
                cboOrderBy_SelectedIndexChanged(cboOrderBy0, EventArgs.Empty);
            if (Convert.ToInt32(tbLbsCSFP.Text) != CCFBPrefs.CSFPLbsPerService)
            {
                if (MessageBox.Show("Do you want to set " + tbLbsCSFP.Text + " as the default CSFP Service Lbs?",
                    "Service Lbs Different Than Default", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    == DialogResult.Yes)
                {
                    CCFBPrefs.UpdateCSFPServiceLbs(tbLbsCSFP.Text);
                }
            }
        }

        private void tbLbsCSFP_Leave(object sender, EventArgs e)
        {
            if (tbLbsCSFP.Text.Trim() == "")
                tbLbsCSFP.Text = "0";
        }

        private void tbLbsCSFP_KeyDown(object sender, KeyEventArgs e)
        {
            CCFBGlobal.checkForIntOnKeyPress(e);
        }

        private void PrintPicketlist_Click(object sender, EventArgs e)
        {
            string route = "";
            if(cboOrderBy0.SelectedItem.ToString() != "DistributionMethod")
            {
                route += "ALL Routes";
            }
            else
            {
                if(cboFilter.SelectedIndex == 0)
                    route += "ALL Routes";
                else
                {
                    route += cboFilter.SelectedItem.ToString();
                }
            }

            Object saveAs = savePath + "\\" + cboYear.SelectedItem.ToString()
                    + getFormatedMonthNumber(cboMonth.SelectedIndex + 1) + "_" + 
                    "CSFP_PickList" + ".doc";

            RptCSFPPicklist clsCreatePicklist = new RptCSFPPicklist(dgvCSFP);
            clsCreatePicklist.createReport(saveAs, CCFBGlobal.fb3TemplatesPath + "CSFPPicklist.doc", CCFBPrefs.FoodBankName,
                DateTime.Now, route);
        }

        private void EditCSFPForm_Resize(object sender, EventArgs e)
        {
            if (this.WindowState != FormWindowState.Minimized)
                dgvCSFP.Top = 140;
        }

        private void menuCSFPRules_Click(object sender, EventArgs e)
        {
            WebPageForm frmTemp = new WebPageForm("Federal CSFP Fact Sheet", "http://www.fns.usda.gov/fdd/programs/csfp/pfs-csfp.pdf");
            frmTemp.ShowDialog();
        }

        private void menuFNSWebSite_Click(object sender, EventArgs e)
        {
            WebPageForm frmTemp = new WebPageForm("Food and Nutrition Web Site", 
                "http://www.fns.usda.gov/fdd/programs/csfp/");
            frmTemp.ShowDialog();
        }

        private void menuCSFPState_Click(object sender, EventArgs e)
        {
            WebPageForm frmTemp = new WebPageForm("State CSFP Information",
                "http://agr.wa.gov/FoodProg/CSFP.aspx");
            frmTemp.ShowDialog();
        }

        private void DeleteService_Click(object sender, EventArgs e)
        {
            string deleteIDs = "";

            for (int i = 0; i < dgvCSFP.SelectedRows.Count; i++)
            {
                if (dgvCSFP.SelectedRows[i].Cells["clmDateServed"].Value.ToString() != "")
                {
                    if (deleteIDs != "")
                        deleteIDs += ", ";

                    deleteIDs += dgvCSFP.SelectedRows[i].Cells["clmLogID"].Value.ToString();
                }
            }

            if (deleteIDs != "")
            {
                openConnection();
                deleteCSFPServices(deleteIDs);
                closeConnection();
                if (cboFilter.Visible == true)
                    cboFilter_SelectedIndexChanged(cboFilter, EventArgs.Empty);
                else
                    cboOrderBy_SelectedIndexChanged(cboOrderBy0, EventArgs.Empty);
            }
        }

        private void deleteCSFPServices(string deleteIDs)
        {
            try
            {
                updateAndInsertComm = new SqlCommand("Delete From CSFPLog "
                          + "Where ID in (" + deleteIDs + ")", conn);
                updateAndInsertComm.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString());
            }
        }

        private void cboPeriod_SelectionChangeCommitted(object sender, EventArgs e)
        {
            toolStrip1.Enabled = false;
            dgvCSFP.Rows.Clear();

        }

        private void cboFilterFld_SelectedIndexChanged(object sender, EventArgs e)
        {
            initFilterInfo();
        }

        private void initFilterInfo()
        {
            cboFilter.Visible = false;
            cboFilter.Items.Clear();
            cboFilter.Text = "";
            lblFilterBy.Visible = false;
            switch (cboFilterFld.SelectedIndex)
            {
                case 1:
                    getDistincts("Route", false);
                    break;
                case 2:
                    getDistincts("Address",false);
                    break;
                case 3:
                    getDistincts("CSFPExpiration",true);
                    break;
                default:
                    //loadList(dtbl.Select());
                    break;
            }
            lblFilterBy.Visible = cboFilter.Visible;
        }

        private void btnAdd6Months_Click(object sender, EventArgs e)
        {
            dtpExpDate.Value = dtpExpDate.Value.AddMonths(6);
        }

        private void setcboSortOrder(int idx)
        {
            bool tmpLoading = loading;
            string whereClause = "";
            if (idx > 0)
            {
                for (int i = 0; i < idx; i++)
                {
                    if (cboSort[i].SelectedIndex > 0)
                    {
                        if (whereClause == "")
                        {
                            whereClause = "WHERE ID NOT IN(" + cboSort[i].SelectedValue.ToString();
                        }
                        else
                        {
                            whereClause += ", " + cboSort[i].SelectedValue.ToString();
                        }
                    }
                }
                if (whereClause != "")
                    whereClause += ")";
            }
            loading = true;
            object sortList = new parmTypeCodes(CCFBGlobal.parmTbl_CSFPSortOrder, CCFBGlobal.connectionString, whereClause);
            if (sortList != null)
            {
                cboSort[idx].DataSource = ((parmTypeCodes)sortList).TypeCodesArray;
                cboSort[idx].DisplayMember = "LongName";
                cboSort[idx].ValueMember = "UID";
            }
            else
            {
                cboSort[idx].DataSource = null;
                cboSort[idx].Items.Add("Not Initialized");
            }
            cboSort[idx].SelectedIndex = 0;
            loading = tmpLoading;
        }

        private string getOrderByText()
        {
            string sql="";
            foreach (ComboBox cbo in cboSort)
            {
                if (cbo.SelectedIndex > 0)
                {
                    if (sql == "")
                    {
                        sql = " ORDER BY ";
                    }
                    else
                    {
                        sql += ", "; 
                    }
                    sql += ((parmType)cbo.Items[cbo.SelectedIndex]).ShortName;
                }
            }
            return sql;
        }
    }
}
