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
        DataSet dset;
        SqlCommand command;
        SqlCommand updateAndInsertComm;
        SqlCommand getDfltCSFPLbsComm;
        SqlConnection conn;
        string sqlCommandText = "Select Distinct DateName(yy, CSFPSvcDate) as 'Year' "
            + "From CSFPLog where CSFPSvcDate is not null Order By 'Year' DESC";
        string tbName = "CSFPLog";
        int rowCount = 0;
        string lastSearchText = "";
        int rowIndex = 0;
        int hhID = 0;
        bool loading = false;
        string orderBy = "";
        int dfltCsfpLbs;
        DateTime svcDate = DateTime.Today;
        string savePath = CCFBPrefs.ReportsSavePath;

        public EditCSFPForm()
        {
            InitializeComponent();
            dtpSvcDate.Value = DateTime.Today;
            dadAdpt = new SqlDataAdapter();
            dset = new DataSet();
            conn = new SqlConnection(CCFBGlobal.connectionString);
            SqlCommand selectComm = new SqlCommand(sqlCommandText, conn);
            
            dadAdpt.SelectCommand = selectComm;
            getDistinctYears();
            fillYearsCombo();
            cboOrderBy.SelectedIndex = 0;
            getDefaultCSFPLbs();
            clsCSFPLog.openWhere("");
            tbsMarkNewCSFP.Text = "New CSFP " + Environment.NewLine + "Client";

            bool connected = CCFBGlobal.IsConnectedToInternet();
            menuFNSWebSite.Enabled = connected;
            menuCSFPFederal.Enabled = connected;
            menuCSFPState.Enabled = connected;

            if (cboYear.Items.Count < 1)
                btnRefreshList.Enabled = false;
        }

        private void getDefaultCSFPLbs()
        {
            getDfltCSFPLbsComm = new SqlCommand("Select FldVal From Defaults Where FldName = 'DefaultCSFPLbs'", conn);
            openConnection();
            SqlDataReader reader = getDfltCSFPLbsComm.ExecuteReader(CommandBehavior.SingleResult);
            reader.Read();
            if (reader.HasRows)
            {
                dfltCsfpLbs = Convert.ToInt32(reader.GetValue(0));
            }
            closeConnection();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void EditCSFPForm_Load(object sender, EventArgs e)
        {
            gbRenewExpDate.Left = gbFindClient.Left;
            gbNewService.Left = gbRenewExpDate.Left;
            gbNewService.Top = gbRenewExpDate.Top;
            gbNewService.BackColor = CCFBGlobal.bkColorAltEdit;
            gbRenewExpDate.BackColor = CCFBGlobal.bkColorAltEdit;
        }

        private void fillYearsCombo()
        {
            cboYear.Items.Clear();
            if (dset.Tables[tbName].Rows.Count > 0)
            {
                for (int i = 0; i < dset.Tables[tbName].Rows.Count; i++)
                {
                    cboYear.Items.Add(dset.Tables[tbName].Rows[i]["Year"]);

                    if (dset.Tables[tbName].Rows[i]["Year"].ToString() == DateTime.Today.Year.ToString())
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

                dadAdpt.Fill(dset, tbName);

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

        private void btnLoad_Click(object sender, EventArgs e)
        {
            getCSFPClients();
        }

        /// <summary>
        /// Gets the CSFP Clients for the given period using a stored procedure
        /// in the database
        /// </summary>
        private void getCSFPClients()
        {
            if (cboYear.SelectedIndex != -1 && cboMonth.SelectedIndex != -1)
            {
                try
                {
                    dset = new DataSet("CSFPInfo");
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
                    parameter2.SqlDbType = SqlDbType.SmallInt;
                    parameter2.Direction = ParameterDirection.Input;
                    parameter2.Value = cboOrderBy.SelectedIndex;

                    // Add the parameter to the Parameters collection. 
                    command.Parameters.Add(parameter);
                    command.Parameters.Add(parameter2);

                    using (SqlDataAdapter da = new SqlDataAdapter(command))
                    {
                        rowCount = da.Fill(dset);
                    }
                    closeConnection();
                    
                    loadList();
                    setFindClientVisible();
                    svcDate = new DateTime(Convert.ToInt32(cboYear.SelectedItem), 
                        cboMonth.SelectedIndex + 1, 1);

                    try
                    {
                        dtpSvcDate.MaxDate = new DateTime(svcDate.Year, svcDate.Month,
                            DateTime.DaysInMonth(svcDate.Year, svcDate.Month));
                        dtpSvcDate.MinDate = svcDate;
                    }
                    catch (ArgumentOutOfRangeException ex)
                    {
                        dtpSvcDate.MinDate = svcDate;
                        dtpSvcDate.MaxDate = new DateTime(svcDate.Year, svcDate.Month,
                            DateTime.DaysInMonth(svcDate.Year, svcDate.Month));
                    }
                }
                catch (SqlException ex)
                {
                    setFindClientVisible();
                    closeConnection();
                    dgvCSFP.Rows.Clear();
                    CCFBGlobal.appendErrorToErrorReport("Period=" + cboYear.SelectedItem.ToString()
                        + CCFBGlobal.formatNumberWithLeadingZero((cboMonth.SelectedIndex + 1)),
                        ex.GetBaseException().ToString(), CCFBGlobal.serverName);
                }
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
                gbFindClient.Visible = false;
        }

        /// <summary>
        /// Loads the DataGrid using values obtained in the DataSet
        /// </summary>
        private void loadList()
        {
            tbFindName.Text = "";
            lastSearchText = "";
            dgvCSFP.Rows.Clear();
            progressBar1.Value = 0;
            progressBar1.Show();
            tbFindName.Visible = false;
            Application.DoEvents();
            progressBar1.Maximum = rowCount;

            DateTime toCheck = new DateTime(Convert.ToInt32(cboYear.SelectedItem),
                        Convert.ToInt32(cboMonth.SelectedIndex + 1), 1);

            for (int i = 0; i < rowCount; i++)
            {

                dgvCSFP.Rows.Add();

                if (Convert.ToInt32(dset.Tables[0].Rows[i]["CSFP"]) == 0)
                    dgvCSFP.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                else if (Convert.ToInt32(dset.Tables[0].Rows[i]["Inactive"]) == 1)
                {
                    dgvCSFP.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                    dgvCSFP.Rows[i].DefaultCellStyle.BackColor = Color.White;
                }

                dgvCSFP["clmHHID", i].Value = dset.Tables[0].Rows[i]["HouseholdID"];
                dgvCSFP["clmName", i].Value = dset.Tables[0].Rows[i]["colNameLF"];
                dgvCSFP["clmAddress", i].Value = dset.Tables[0].Rows[i]["Address"];
                dgvCSFP["clmExpiration", i].Value = convertToShortDate(
                    CCFBGlobal.NullToBlank(dset.Tables[0].Rows[i]["CSFPExpiration"]));
                if (dgvCSFP["clmExpiration", i].Value.ToString() != "" && 
                    (DateTime)dset.Tables[0].Rows[i]["CSFPExpiration"] < toCheck)
                {
                    dgvCSFP["clmExpiration", i].Style.BackColor = Color.Yellow;
                }
                dgvCSFP["clmMethod", i].Value = dset.Tables[0].Rows[i]["Route"];
                dgvCSFP["clmMethodAsInt", i].Value = dset.Tables[0].Rows[i]["RouteAsInt"];
                dgvCSFP["clmDateServed", i].Value = convertToShortDate(
                    CCFBGlobal.NullToBlank(dset.Tables[0].Rows[i]["CSFPSvcDate"]));
                dgvCSFP["clmLbs", i].Value = CCFBGlobal.NullToBlank(dset.Tables[0].Rows[i]["Lbs"]);
                dgvCSFP["clmHHMemID", i].Value = CCFBGlobal.NullToBlank(dset.Tables[0].Rows[i]["hhmID"]);
                dgvCSFP.Rows[i].Cells["colNameLF"].Value = dset.Tables[0].Rows[i]["colNameLF"].ToString().ToUpper().Trim();
                dgvCSFP.Rows[i].Cells["colNameFL"].Value = dset.Tables[0].Rows[i]["colNameFL"].ToString().ToUpper().Trim();
                dgvCSFP.Rows[i].Cells["clmCSFP"].Value = dset.Tables[0].Rows[i]["CSFP"];
                dgvCSFP.Rows[i].Cells["clmLogID"].Value = dset.Tables[0].Rows[i]["LogId"];
                progressBar1.PerformStep();
            }

            rowIndex = 0;
            progressBar1.Value = 0;
            progressBar1.Visible = false;
            tbFindName.Visible = true;
        }

        /// <summary>
        /// Loads the DataGrid from the DataRows that are passed to the funtion
        /// </summary>
        /// <param name="drows">The Array of DataRows to load the data from</param>
        private void loadList(DataRow[] drows)
        {
            tbFindName.Text = "";
            lastSearchText = "";
            dgvCSFP.Rows.Clear();
            progressBar1.Value = 0;
            progressBar1.Show();
            tbFindName.Visible = false;
            Application.DoEvents();
            progressBar1.Maximum = rowCount;

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

                dgvCSFP["clmHHID", i].Value = drows[i]["HouseholdID"];
                dgvCSFP["clmName", i].Value = drows[i]["colNameLF"];
                dgvCSFP["clmAddress", i].Value = drows[i]["Address"];
                dgvCSFP["clmExpiration", i].Value = convertToShortDate(
                    CCFBGlobal.NullToBlank(drows[i]["CSFPExpiration"]));
                if (dgvCSFP["clmExpiration", i].Value.ToString() != "" &&
                    (DateTime)drows[i]["CSFPExpiration"] < toCheck)
                {
                    dgvCSFP["clmExpiration", i].Style.BackColor = Color.Yellow;
                }
                dgvCSFP["clmMethod", i].Value = drows[i]["Route"];
                dgvCSFP["clmMethodAsInt", i].Value = drows[i]["RouteAsInt"];
                dgvCSFP["clmDateServed", i].Value = convertToShortDate(
                    CCFBGlobal.NullToBlank(drows[i]["CSFPSvcDate"]));
                dgvCSFP["clmLbs", i].Value = CCFBGlobal.NullToBlank(drows[i]["Lbs"]);
                dgvCSFP["clmHHMemID", i].Value = CCFBGlobal.NullToBlank(drows[i]["hhmID"]);
                dgvCSFP.Rows[i].Cells["colNameLF"].Value = drows[i]["colNameLF"].ToString().ToUpper().Trim();
                dgvCSFP.Rows[i].Cells["colNameFL"].Value = drows[i]["colNameFL"].ToString().ToUpper().Trim();
                dgvCSFP.Rows[i].Cells["clmCSFP"].Value = drows[i]["CSFP"];
                dgvCSFP.Rows[i].Cells["clmLogID"].Value = drows[i]["LogId"];
                progressBar1.PerformStep();
            }

            rowIndex = 0;
            progressBar1.Value = 0;
            progressBar1.Visible = false;
            tbFindName.Visible = true;
        }

        private string convertToShortDate(string date)
        {
            if(date != "")
            {
                return DateTime.Parse(date).ToShortDateString();
            }
            return "";
        }

        private void FindByName(string colNameFull, string colNameSecond)
        {
            int rowStart = 0;
            if (tbFindName.TextLength >= lastSearchText.Length)
                rowStart = rowIndex;
            else
                rowStart = 0;
            lastSearchText = tbFindName.Text.ToUpper().Trim();
            for (int i = rowStart; i < dgvCSFP.Rows.Count; i++)
            {
                if (dgvCSFP.Rows[i].Cells[colNameFull].FormattedValue.ToString().StartsWith(lastSearchText) == true)
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
                switch (cboOrderBy.SelectedIndex)
                {
                    case 0:
                        { FindByName("colNameLF", "clmNameFL"); break; }
                    case 1:
                        { FindByName("colNameFL", "clmNameLF"); break; }
                    case 3:
                        { FindByName("clmHHID", "clmNameLF"); break; }
                    case 2:
                        { FindByName("clmMethod", "colNameLF"); break; }
                    case 5:
                        { FindByName("clmExpiration", "colNameLF"); break; }
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
            if (loading == false && rowCount > 0 && cboFilter.SelectedIndex != 0)
            {
                DataRow[] drows;
                if (orderBy == "Route")
                    drows = dset.Tables[0].Select(orderBy + "='" + cboFilter.SelectedItem.ToString() + "'");
                else
                    drows = dset.Tables[0].Select(orderBy + "=" + cboFilter.SelectedItem.ToString());
               
                loadList(drows);
            }
            else if (cboFilter.SelectedIndex == 0)
            {
                loadList();
            }
        }

        private void fillFilterByCombo(DataTable dt)
        {
            loading = true;
            cboFilter.Items.Clear();
            cboFilter.Items.Add("No Filter");
            cboFilter.SelectedIndex = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cboFilter.Items.Add(dt.Rows[i][0].ToString());
            }
            loading = false;
        }

        private void getDistincts(string colName)
        {
            orderBy = colName;
            cboFilter.Visible = true;
            DataView Dv = dset.Tables[0].DefaultView;
            DataTable DtD = Dv.ToTable(true, colName);
            fillFilterByCombo(DtD);
        }

        private void cboOrderBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboFilter.Visible = false;
            cboFilter.Text = "";
            lblFilterBy.Visible = false;
            getCSFPClients();
            switch (cboOrderBy.SelectedIndex)
            {
                case 3:
                    {
                        getDistincts("HouseholdID");
                        break;
                    }
                case 2:
                    {
                        getDistincts("Route");
                        break;
                    }
            }
            lblFilterBy.Visible = cboFilter.Visible;
        }

        private void EditExpiration_Click(object sender, EventArgs e)
        {
            if (dgvCSFP.SelectedRows.Count > 0)
            {
                changeGroupBoxVisibility(1);
                if (dgvCSFP.SelectedRows[0].Cells["clmExpiration"].Value.ToString() != "")
                {
                    try
                    {
                        dtpExpDate.Value = DateTime.Parse(dgvCSFP.SelectedRows[0].Cells["clmExpiration"].Value.ToString());
                    }
                    catch { dtpExpDate.Value = DateTime.Today; }
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
            switch (option)
            {
                case 0:
                    {
                        gbFindClient.Visible = true;
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
                        gbFindClient.Visible = false;
                        gbNewService.Visible = false;
                        gbRenewExpDate.Visible = true;
                        cboYear.Enabled = false;
                        cboMonth.Enabled = false;
                        btnRefreshList.Enabled = false;
                        dgvCSFP.Enabled = false;

                        string date = CCFBGlobal.NullToBlank(dgvCSFP.SelectedRows[0].Cells["clmExpiration"].Value);

                        if (date != "")
                            dtpExpDate.Value = DateTime.Parse(date);

                        chkCSFP.Checked = (bool)dgvCSFP.SelectedRows[0].Cells["clmCSFP"].Value;
                        break;
                    }
                case 2:
                    {
                        cboYear.Enabled = false;
                        cboMonth.Enabled = false;
                        btnRefreshList.Enabled = false;
                        dgvCSFP.Enabled = false;
                        gbFindClient.Visible = false;
                        gbNewService.Visible = true;
                        gbRenewExpDate.Visible = false;
                        tbLbsCSFP.Text = dfltCsfpLbs.ToString();
                        dtpSvcDate.Value = svcDate;
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
                        + " ID=" + dgvCSFP.SelectedRows[i].Cells["clmHHMemID"].ToString(), ex.GetBaseException().ToString(),
                        CCFBGlobal.serverName);
                }
            }
            changeGroupBoxVisibility(0);
            getCSFPClients();
        }

        private void MarkNewCSFPClient_Click(object sender, EventArgs e)
        {
            FindClientForm frmFindClient = new FindClientForm();
            frmFindClient.ShowDialog();
            hhID = frmFindClient.CurrentHHId;
            frmFindClient.Close();
            
            NewCSFPSelectionForm frmNewCSFP = new NewCSFPSelectionForm(hhID);
            frmNewCSFP.ShowDialog();
            getCSFPClients();
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

        private void giveNewService(string insertIDs)
        {
            try
            {
                updateAndInsertComm = new SqlCommand("Insert Into CSFPLog (Period, CSFPSvcDate, "
                          + "MemID, Lbs, DistributionMethod, Created, CreatedBy) "
                          + "SELECT '" + cboYear.SelectedItem.ToString()
                          + getFormatedMonthNumber(cboMonth.SelectedIndex + 1)
                          + "', '" + dtpSvcDate.Value.ToString()
                          + "', ID, " + tbLbsCSFP.Text + ", CSFPRoute, '"
                          + DateTime.Now.ToString() + "','"
                          + CCFBGlobal.currentUser_Name + "'"
                          + "from HouseholdMembers WHERE ID IN (" + insertIDs + ")", conn);

                updateAndInsertComm.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString(),
                    CCFBGlobal.serverName);
            }
        }

        private void updateExistingService(string updateIDs)
        {
            try
            {
                updateAndInsertComm = new SqlCommand("Update CSFPLog Set CSFPSvcDate='" + dtpSvcDate.Value.ToString() + "', "
                          + "Lbs=" + tbLbsCSFP.Text + ", Modified='" + DateTime.Now.ToString() + "', "
                          + "ModifiedBy='" + CCFBGlobal.currentUser_Name + "' "
                          + "Where ID in (" + updateIDs + ")", conn);
                updateAndInsertComm.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString(),
                    CCFBGlobal.serverName);
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
                giveNewService(insertIDs);

            if (updateIDs != "")
                updateExistingService(updateIDs);

            closeConnection();
            changeGroupBoxVisibility(0);
            getCSFPClients();
        }

        private void tbLbsCSFP_Leave(object sender, EventArgs e)
        {
            if (tbLbsCSFP.Text.Trim() == "")
                tbLbsCSFP.Text = "0";

            dfltCsfpLbs = Convert.ToInt32(tbLbsCSFP.Text.Trim());
            try
            {
                openConnection();
                updateAndInsertComm = new SqlCommand("Update Defaults Set FldVal=" + dfltCsfpLbs.ToString()
                    + " Where FldName='DefaultCSFPLbs'", conn);
                updateAndInsertComm.ExecuteNonQuery();
                closeConnection();
            }
            catch (SqlException ex)
            {
                closeConnection();
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString(),
                    CCFBGlobal.serverName);
            }

        }

        private void tbLbsCSFP_KeyDown(object sender, KeyEventArgs e)
        {
            CCFBGlobal.checkForIntOnKeyPress(e);
        }

        private void PrintPicketlist_Click(object sender, EventArgs e)
        {
            string route = "";
            if(cboOrderBy.SelectedItem.ToString() != "DistributionMethod")
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

            CreatePicklist clsCreatePicklist = new CreatePicklist(dgvCSFP);
            clsCreatePicklist.createReport(saveAs, @"C:\ClientcardFB3\Templates\CSFPPicklist.doc", CCFBPrefs.FoodBankName,
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
                "http://agr.wa.gov/FoodProg/docs/316-CommoditySupplementalFoodProgramFactsheet.pdf");
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
                getCSFPClients();
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
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString(),
                    CCFBGlobal.serverName);
            }
        }
    }
}
