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
    public partial class EditBackpackForm : Form
    {
        BackpackLog clsBackpackLog = new BackpackLog(CCFBGlobal.connectionString);

        SqlDataAdapter dadAdpt;
        DataTable dtbl;
        SqlCommand command;
        SqlCommand updateAndInsertComm;
        SqlConnection conn;
        List<ComboBox> cboSort = new List<ComboBox>();
        string sqlCommandText = "Select Distinct DateName(yy, BackpackSvcDate) as 'Year' "
            + "From BackpackLog where BackpackSvcDate is not null Order By 'Year' DESC";
        //string tbName = "BackpackLog";
        int rowCount = 0;
        string lastSearchText = "";
        int rowIndex = 0;
        //int hhID = 0;
        bool loading = false;
        string filterFldName = "";
        bool isShortDate = false;
        DateTime svcDate = DateTime.Today;
        string savePath = CCFBPrefs.ReportsSavePath;
        const string nofilter = "<No Filter>";

        public EditBackpackForm()
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
            loading = true;
            //getDefaultBackpackLbs();
            clsBackpackLog.openWhere("");
            tbsMarkNewBackpack.Text = "New Backpack " + Environment.NewLine + "Client";

            bool connected = CCFBGlobal.IsConnectedToInternet();

            if (cboYear.Items.Count < 1)
                btnRefreshList.Enabled = false;

            cboMonth.SelectedIndex = DateTime.Today.Month - 1;
            loading = false;
        }

        //private void getDefaultBackpackLbs()
        //{
        //    getDfltBackpackLbsComm = new SqlCommand("Select FldVal From Defaults Where FldName = 'DefaultBackpackLbs'", conn);
        //    openConnection();
        //    SqlDataReader reader = getDfltBackpackLbsComm.ExecuteReader(CommandBehavior.SingleResult);
        //    reader.Read();
        //    if (reader.HasRows)
        //    {
        //        dfltBackpackLbs = Convert.ToInt32(reader.GetValue(0));
        //    }
        //    closeConnection();
        //}

        private void EditBackpackForm_Load(object sender, EventArgs e)
        {
            gbNewService.Left = gbRenewExpDate.Left;
            gbNewService.Top = gbRenewExpDate.Top;
            gbNewService.BackColor = CCFBGlobal.bkColorAltEdit;
            gbRenewExpDate.BackColor = CCFBGlobal.bkColorAltEdit;
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
                    getDistincts("Address", false);
                    break;
                case 3:
                    getDistincts("CSFPExpiration", true);
                    break;
                default:
                    //loadList(dtbl.Select());
                    break;
            }
            lblFilterBy.Visible = cboFilter.Visible;
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
                        dtpSvcDate.MaxDate = new DateTime(svcDate.Year, svcDate.Month,
                            DateTime.DaysInMonth(svcDate.Year, svcDate.Month)).AddDays(14);
                        //dtpSvcDate.MinDate = svcDate;
                    }
                    catch (ArgumentOutOfRangeException ex)
                    {
                        //dtpSvcDate.MinDate = svcDate;
                        dtpSvcDate.MaxDate = new DateTime(svcDate.Year, svcDate.Month,
                            DateTime.DaysInMonth(svcDate.Year, svcDate.Month)).AddDays(14);
                    }
                }
                catch (SqlException ex)
                {
                    closeConnection();
                    dgvBackpack.Rows.Clear();
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
            if (dgvBackpack.Rows.Count > 0)
                changeGroupBoxVisibility(0);
            else
                gbFilter.Visible = false;
        }

        /// <summary>
        /// Loads the DataGrid using values obtained in the DataSet
        /// </summary>
        private void loadList()
        {
            tbFindName.Text = "";
            lastSearchText = "";
            dgvBackpack.Rows.Clear();
            progressBar1.Value = 0;
            progressBar1.Show();
            tbFindName.Visible = false;
            Application.DoEvents();
            progressBar1.Maximum = rowCount;

            DateTime toCheck = new DateTime(Convert.ToInt32(cboYear.SelectedItem),
                        Convert.ToInt32(cboMonth.SelectedIndex + 1), 1);

            for (int i = 0; i < rowCount; i++)
            {

                dgvBackpack.Rows.Add();

                if (Convert.ToInt32(dtbl.Rows[i]["Backpack"]) == 0)
                    dgvBackpack.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                else if (Convert.ToInt32(dtbl.Rows[i]["Inactive"]) == 1)
                {
                    dgvBackpack.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                    dgvBackpack.Rows[i].DefaultCellStyle.BackColor = Color.White;
                }

                dgvBackpack["clmHHID", i].Value = dtbl.Rows[i]["HouseholdID"];
                dgvBackpack["clmName", i].Value = dtbl.Rows[i]["colNameLF"];
                dgvBackpack["clmAddress", i].Value = dtbl.Rows[i]["Address"];
                dgvBackpack["clmAptNum", i].Value = dtbl.Rows[i]["AptNbr"];
                dgvBackpack["clmExpiration", i].Value = convertToShortDate(
                    CCFBGlobal.NullToBlank(dtbl.Rows[i]["BackpackExpiration"]));
                if (dgvBackpack["clmExpiration", i].Value.ToString() != "" &&
                    (DateTime)dtbl.Rows[i]["BackpackExpiration"] < toCheck)
                {
                    dgvBackpack["clmExpiration", i].Style.BackColor = Color.Yellow;
                }
                dgvBackpack["clmMethod", i].Value = dtbl.Rows[i]["Route"];
                dgvBackpack["clmMethodAsInt", i].Value = dtbl.Rows[i]["RouteAsInt"];
                dgvBackpack["clmDateServed", i].Value = convertToShortDate(
                    CCFBGlobal.NullToBlank(dtbl.Rows[i]["BackpackSvcDate"]));
                dgvBackpack["clmLbs", i].Value = CCFBGlobal.NullToBlank(dtbl.Rows[i]["Lbs"]);
                dgvBackpack["clmHHMemID", i].Value = CCFBGlobal.NullToBlank(dtbl.Rows[i]["hhmID"]);
                dgvBackpack.Rows[i].Cells["colNameLF"].Value = dtbl.Rows[i]["colNameLF"].ToString().ToUpper().Trim();
                dgvBackpack.Rows[i].Cells["colNameFL"].Value = dtbl.Rows[i]["colNameFL"].ToString().ToUpper().Trim();
                dgvBackpack.Rows[i].Cells["clmBackpack"].Value = dtbl.Rows[i]["Backpack"];
                dgvBackpack.Rows[i].Cells["clmLogID"].Value = dtbl.Rows[i]["LogId"];
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
            dgvBackpack.Rows.Clear();
            progressBar1.Value = 0;
            progressBar1.Show();
            tbFindName.Visible = false;
            Application.DoEvents();
            progressBar1.Maximum = rowCount;

            DateTime toCheck = new DateTime(Convert.ToInt32(cboYear.SelectedItem),
                        Convert.ToInt32(cboMonth.SelectedIndex + 1), 1);

            for (int i = 0; i < drows.Length; i++)
            {

                dgvBackpack.Rows.Add();

                if (Convert.ToInt32(drows[i]["Backpack"]) == 0)
                    dgvBackpack.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                else if (Convert.ToInt32(drows[i]["Inactive"]) == 1)
                {
                    dgvBackpack.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                    dgvBackpack.Rows[i].DefaultCellStyle.BackColor = Color.White;
                }

                dgvBackpack["clmHHID", i].Value = drows[i]["HouseholdID"];
                dgvBackpack["clmName", i].Value = drows[i]["colNameLF"];
                dgvBackpack["clmAddress", i].Value = drows[i]["Address"];
                dgvBackpack["clmAptNum", i].Value = dtbl.Rows[i]["AptNbr"];
                dgvBackpack["clmExpiration", i].Value = convertToShortDate(
                    CCFBGlobal.NullToBlank(drows[i]["BackpackExpiration"]));
                if (dgvBackpack["clmExpiration", i].Value.ToString() != "" &&
                    (DateTime)drows[i]["BackpackExpiration"] < toCheck)
                {
                    dgvBackpack["clmExpiration", i].Style.BackColor = Color.Yellow;
                }
                dgvBackpack["clmMethod", i].Value = drows[i]["Route"];
                dgvBackpack["clmMethodAsInt", i].Value = drows[i]["RouteAsInt"];
                dgvBackpack["clmDateServed", i].Value = convertToShortDate(
                    CCFBGlobal.NullToBlank(drows[i]["BackpackSvcDate"]));
                dgvBackpack["clmLbs", i].Value = CCFBGlobal.NullToBlank(drows[i]["Lbs"]);
                dgvBackpack["clmHHMemID", i].Value = CCFBGlobal.NullToBlank(drows[i]["hhmID"]);
                dgvBackpack.Rows[i].Cells["colNameLF"].Value = drows[i]["colNameLF"].ToString().ToUpper().Trim();
                dgvBackpack.Rows[i].Cells["colNameFL"].Value = drows[i]["colNameFL"].ToString().ToUpper().Trim();
                dgvBackpack.Rows[i].Cells["clmBackpack"].Value = drows[i]["Backpack"];
                dgvBackpack.Rows[i].Cells["clmLogID"].Value = drows[i]["LogId"];
                progressBar1.PerformStep();
            }

            rowIndex = 0;
            progressBar1.Value = 0;
            progressBar1.Visible = false;
            tbFindName.Visible = true;
        }

        private string convertToShortDate(string date)
        {
            if (date != "")
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
            for (int i = rowStart; i < dgvBackpack.Rows.Count; i++)
            {
                if (dgvBackpack.Rows[i].Cells[colNameFull].FormattedValue.ToString().ToUpper().StartsWith(lastSearchText) == true)
                {
                    dgvBackpack.CurrentCell = dgvBackpack[0, i];
                    if (i < dgvBackpack.FirstDisplayedScrollingRowIndex
                        || i > dgvBackpack.Rows.GetLastRow(DataGridViewElementStates.Displayed) - 5)
                        if (i > 5)
                            dgvBackpack.FirstDisplayedScrollingRowIndex = i - 5;
                        else
                            dgvBackpack.FirstDisplayedScrollingRowIndex = i;
                    break;
                }
            }
        }

        private void tbFindName_TextChanged(object sender, EventArgs e)
        {
            if (tbFindName.Text.Trim() == "")
            { dgvBackpack.CurrentCell = dgvBackpack[0, 0]; }
            else
            {
                if (cboOrderBy0.SelectedIndex > 0)
                {
                    FindByName(((parmType)cboOrderBy0.Items[cboOrderBy0.SelectedIndex]).ShortName);
                }
            }
        }

        private void EditExpiration_Click(object sender, EventArgs e)
        {
            if (dgvBackpack.SelectedRows.Count > 0)
            {
                changeGroupBoxVisibility(1);
                if (dgvBackpack.SelectedRows[0].Cells["clmExpiration"].Value.ToString() != "")
                {
                    try
                    {
                        dtpExpDate.Value = DateTime.Parse(dgvBackpack.SelectedRows[0].Cells["clmExpiration"].Value.ToString());
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
                        gbRenewExpDate.Visible = false;
                        gbNewService.Visible = false;
                        cboYear.Enabled = true;
                        cboMonth.Enabled = true;
                        btnRefreshList.Enabled = true;
                        dgvBackpack.Enabled = true;
                        break;
                    }
                case 1:
                    {
                        gbNewService.Visible = false;
                        gbRenewExpDate.Visible = true;
                        cboYear.Enabled = false;
                        cboMonth.Enabled = false;
                        btnRefreshList.Enabled = false;
                        dgvBackpack.Enabled = false;

                        string date = CCFBGlobal.NullToBlank(dgvBackpack.SelectedRows[0].Cells["clmExpiration"].Value);

                        if (date != "")
                            dtpExpDate.Value = DateTime.Parse(date);

                        chkBackpack.Checked = (bool)dgvBackpack.SelectedRows[0].Cells["clmBackpack"].Value;
                        break;
                    }
                case 2:
                    {
                        cboYear.Enabled = false;
                        cboMonth.Enabled = false;
                        btnRefreshList.Enabled = false;
                        dgvBackpack.Enabled = false;
                        gbNewService.Visible = true;
                        gbRenewExpDate.Visible = false;
                        //tbLbsBackpack.Text = CCFBPrefs.BackpackLbsPerUnit * cls  .ToString();
                        dtpSvcDate.Value = svcDate;
                        break;
                    }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int checkState = 0;

            if (chkBackpack.Checked == true)
                checkState = 1;
            for (int i = 0; i < dgvBackpack.SelectedRows.Count; i++)
            {
                try
                {
                    openConnection();
                    updateAndInsertComm = new SqlCommand("Update HouseholdMembers Set BackpackExpiration =' "
                        + dtpExpDate.Value.ToString() + "', Backpack = " + checkState.ToString()
                    + " Where ID=" + dgvBackpack.SelectedRows[i].Cells["clmHHMemID"].Value.ToString(), conn);
                    updateAndInsertComm.ExecuteNonQuery();
                    closeConnection();
                }
                catch (SqlException ex)
                {
                    closeConnection();
                    CCFBGlobal.appendErrorToErrorReport("BackpackExpiration = " + dtpExpDate.Value.ToString()
                        + " ID=" + dgvBackpack.SelectedRows[i].Cells["clmHHMemID"].ToString(), ex.GetBaseException().ToString());
                }
            }
            changeGroupBoxVisibility(0);
            if (cboFilter.Visible == true)
                cboFilter_SelectedIndexChanged(cboFilter, EventArgs.Empty);
            else
                cboOrderBy_SelectedIndexChanged(cboOrderBy0, EventArgs.Empty);

        }

        //private void MarkNewBackpackClient_Click(object sender, EventArgs e)
        //{
        //    FindClientForm frmFindClient = new FindClientForm();
        //    frmFindClient.ShowDialog();

        //    if (frmFindClient.Canceled == false)
        //    {
        //        hhID = frmFindClient.CurrentHHId;
        //        frmFindClient.Close();

        //        NewBackpackSelectionForm frmNewBackpack = new NewBackpackSelectionForm(hhID);
        //        frmNewBackpack.ShowDialog();
        //        if (cboFilter.Visible == true)
        //            cboFilter_SelectedIndexChanged(cboFilter, EventArgs.Empty);
        //        else
        //            cboOrderBy_SelectedIndexChanged(cboOrderBy, EventArgs.Empty);

        //    }
        //}

        private void GiveBackpackService_Click(object sender, EventArgs e)
        {
            if (dgvBackpack.SelectedRows.Count > 0)
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
                updateAndInsertComm = new SqlCommand("Update BackpackLog Set BackpackSvcDate='" + dtpSvcDate.Value.ToString() + "', "
                          + "Lbs=" + tbLbsBackpack.Text + ", Modified='" + DateTime.Now.ToString() + "', "
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
            for (int i = 0; i < dgvBackpack.SelectedRows.Count; i++)
            {
                if (dgvBackpack.SelectedRows[i].Cells["clmDateServed"].Value.ToString() != "")
                {
                    if (MessageBox.Show("Backpack Client " + dgvBackpack.SelectedRows[i].Cells["clmName"].Value.ToString()
                         + " Already Has A Service For This Period. Would You Like To Update The Service?",
                         "Service Already Exists For This Period", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                        == System.Windows.Forms.DialogResult.Yes)
                    {
                        if (updateIDs != "")
                            updateIDs += ", ";

                        updateIDs += dgvBackpack.SelectedRows[i].Cells["clmLogID"].Value.ToString();
                    }
                }
                else
                {
                    if (insertIDs != "")
                        insertIDs += ", ";

                    insertIDs += dgvBackpack.SelectedRows[i].Cells["clmHHMemID"].Value.ToString();
                }

            }

            openConnection();
            if (insertIDs != "")
                clsBackpackLog.insertNewService(insertIDs, dtpSvcDate.Value, tbLbsBackpack.Text);

            if (updateIDs != "")
                updateExistingService(updateIDs);

            closeConnection();
            changeGroupBoxVisibility(0);
            if (cboFilter.Visible == true)
                cboFilter_SelectedIndexChanged(cboFilter, EventArgs.Empty);
            else
                cboOrderBy_SelectedIndexChanged(cboOrderBy0, EventArgs.Empty);

        }

        private void tbLbsBackpack_Leave(object sender, EventArgs e)
        {
            if (tbLbsBackpack.Text.Trim() == "")
                tbLbsBackpack.Text = "0";

            //dfltBackpackLbs = Convert.ToInt32(tbLbsBackpack.Text.Trim());
            //try
            //{
            //    openConnection();
            //    updateAndInsertComm = new SqlCommand("Update Defaults Set FldVal=" + dfltBackpackLbs.ToString()
            //        + " Where FldName='DefaultBackpackLbs'", conn);
            //    updateAndInsertComm.ExecuteNonQuery();
            //    closeConnection();
            //}
            //catch (SqlException ex)
            //{
            //    closeConnection();
            //    CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString());
            //}

        }

        private void tbLbsBackpack_KeyDown(object sender, KeyEventArgs e)
        {
            CCFBGlobal.checkForIntOnKeyPress(e);
        }

        private void PrintPicketlist_Click(object sender, EventArgs e)
        {
            string route = "";
            if (cboOrderBy0.SelectedItem.ToString() != "DistributionMethod")
            {
                route += "ALL Routes";
            }
            else
            {
                if (cboFilter.SelectedIndex == 0)
                    route += "ALL Routes";
                else
                {
                    route += cboFilter.SelectedItem.ToString();
                }
            }

            Object saveAs = savePath + "\\" + cboYear.SelectedItem.ToString()
                    + getFormatedMonthNumber(cboMonth.SelectedIndex + 1) + "_" +
                    "Backpack_PickList" + ".doc";

            //CreateBackpackPicklist clsCreatePicklist = new CreateBackpackPicklist(dgvBackpack);
            //clsCreatePicklist.createReport(saveAs, @"C:\ClientcardFB3\Templates\BackpackPicklist.doc", CCFBPrefs.FoodBankName,
            //    DateTime.Now, route);
        }

        private void EditBackpackForm_Resize(object sender, EventArgs e)
        {
            if (this.WindowState != FormWindowState.Minimized)
                dgvBackpack.Top = 140;
        }

        ////private void menuBackpackRules_Click(object sender, EventArgs e)
        ////{
        ////    WebPageForm frmTemp = new WebPageForm("Federal Backpack Fact Sheet", "http://www.fns.usda.gov/fdd/programs/Backpack/pfs-Backpack.pdf");
        ////    frmTemp.ShowDialog();
        ////}

        ////private void menuFNSWebSite_Click(object sender, EventArgs e)
        ////{
        ////    WebPageForm frmTemp = new WebPageForm("Food and Nutrition Web Site", 
        ////        "http://www.fns.usda.gov/fdd/programs/Backpack/");
        ////    frmTemp.ShowDialog();
        ////}

        ////private void menuBackpackState_Click(object sender, EventArgs e)
        ////{
        ////    WebPageForm frmTemp = new WebPageForm("State Backpack Information", 
        ////        "http://agr.wa.gov/FoodProg/docs/316-CommoditySupplementalFoodProgramFactsheet.pdf");
        ////    frmTemp.ShowDialog();
        ////}

        private void DeleteService_Click(object sender, EventArgs e)
        {
            string deleteIDs = "";

            for (int i = 0; i < dgvBackpack.SelectedRows.Count; i++)
            {
                if (dgvBackpack.SelectedRows[i].Cells["clmDateServed"].Value.ToString() != "")
                {
                    if (deleteIDs != "")
                        deleteIDs += ", ";

                    deleteIDs += dgvBackpack.SelectedRows[i].Cells["clmLogID"].Value.ToString();
                }
            }

            if (deleteIDs != "")
            {
                openConnection();
                deleteBackpackServices(deleteIDs);
                closeConnection();
                if (cboFilter.Visible == true)
                    cboFilter_SelectedIndexChanged(cboFilter, EventArgs.Empty);
                else
                    cboOrderBy_SelectedIndexChanged(cboOrderBy0, EventArgs.Empty);
            }
        }

        private void deleteBackpackServices(string deleteIDs)
        {
            try
            {
                updateAndInsertComm = new SqlCommand("Delete From BackpackLog "
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
            dgvBackpack.Rows.Clear();

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
                if (isShortDate == true)
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
                for (int i = Convert.ToInt32(cbo.Tag) + 1; i < 3; i++)
                {
                    setcboSortOrder(i);
                }
            }
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
            string sql = "";
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
