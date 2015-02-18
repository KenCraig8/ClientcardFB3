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
        SqlConnection conn;
        List<ComboBox> cboSort = new List<ComboBox>();
        List<ComboBox> cboAscDesc = new List<ComboBox>();
        List<CheckBox> chkBoxList = new List<CheckBox>();

        int rowCount = 0;
        string lastSearchText = "";
        int rowIndex = 0;
        int hhID = 0;
        bool loading = true;
        string filterFldName = "";
        bool isShortDate = false;
        DateTime svcDate = DateTime.Today;
        string savePath = CCFBGlobal.pathCSFP;
        const string nofilter = "<No Filter>";

        public EditCSFPForm()
        {
            InitializeComponent();
            cboSort.Add(cboOrderBy0);
            cboSort.Add(cboOrderBy1);
            cboSort.Add(cboOrderBy2);
            cboAscDesc.Add(cboAscDesc0);
            cboAscDesc.Add(cboAscDesc1);
            cboAscDesc.Add(cboAscDesc2);
            foreach (ComboBox cbo in cboAscDesc)
            {
                cbo.SelectedIndex = 0;
            }
            setcboSortOrder(0);
            if (CCFBPrefs.EnableCSFPShowRoutes == true)
            {
                setcboSortSelectedIndex(0,3);
                setcboSortOrder(1);
                setcboSortSelectedIndex(1,1);
            }
            else
            {
                setcboSortSelectedIndex(0,1);
                setcboSortOrder(1);
                cboSort[1].SelectedIndex = 0;
            }
            setcboSortOrder(2);
            cboSort[2].SelectedIndex = 0;

            chkBoxList.Add(chkStatus0);
            chkBoxList.Add(chkStatus1);
            chkBoxList.Add(chkStatus2);
            chkBoxList.Add(chkStatus3);
            chkBoxList.Add(chkStatus4);
            chkBoxList.Add(chkStatus5);

            dtpSvcDate.Value = DateTime.Today;
            dadAdpt = new SqlDataAdapter();
            dtbl = new DataTable();
            conn = new SqlConnection(CCFBGlobal.connectionString);
            getDistinctYears();
            

            //cboOrderBy0.SelectedIndex = 0;
            //getDefaultCSFPLbs();
            clsCSFPLog.openWhere("");
            tsbMarkNewCSFP.Text = "New CSFP " + Environment.NewLine + "Client";

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
            gbNewService.Top = gbOrderBy.Top;
            gbRenewExpDate.Left = gbNewService.Left;
            gbRenewExpDate.Top = gbNewService.Top;
            gbNewService.BackColor = CCFBGlobal.bkColorAltEdit;
            gbRenewExpDate.BackColor = CCFBGlobal.bkColorAltEdit;
        }

        private void fillYearsCombo()
        {
            cboYear.Items.Clear();
            bool bCurYearOK = false;
            if (dtbl.Rows.Count > 0)
            {
                for (int i = 0; i < dtbl.Rows.Count; i++)
                {
                    cboYear.Items.Add(dtbl.Rows[i]["Year"]);

                    if (dtbl.Rows[i]["Year"].ToString() == DateTime.Today.Year.ToString())
                    {
                        cboYear.SelectedItem = cboYear.Items[cboYear.Items.Count - 1];
                        bCurYearOK = true;
                    }
                }
            }
            else
            {
                cboYear.Items.Add(DateTime.Today.Year.ToString());
                cboYear.SelectedItem = cboYear.Items[0];
                bCurYearOK = true;
            }
            if (bCurYearOK == false)
            {
                cboYear.Items.Insert(0,DateTime.Today.Year.ToString());
                cboYear.SelectedItem = cboYear.Items[0];
            }
            if (cboYear.SelectedIndex == -1 && cboYear.Items.Count > 0)
            {
                cboYear.SelectedIndex = 0;
            }
            if (cboYear.Items[0].ToString().Equals(DateTime.Today.Year.ToString()) == true)
            {
                if (DateTime.Today.Month == 12)
                {
                    cboYear.Items.Insert(0, DateTime.Today.AddMonths(1).Year.ToString());
                }
            }
            if (DateTime.Today.Day > 10)
            {
                cboMonth.SelectedIndex = DateTime.Today.Month - 1;
            }
            else if (DateTime.Today.Month == 1)
            {
                cboMonth.SelectedIndex = 11;
            }
            else
            {
                cboMonth.SelectedIndex = DateTime.Today.Month - 2;
            }
        }

        private void getDistinctYears()
        {
            try
            {
                openConnection();
                SqlCommand selectComm = new SqlCommand("Select Distinct DateName(yy, TrxDate) as 'Year' "
                        + "From CSFPLog where TrxDate is not null Order By 'Year' DESC", conn);
                dadAdpt.SelectCommand = selectComm;
                dadAdpt.Fill(dtbl);
                closeConnection();
                fillYearsCombo();
            }
            catch (SqlException ex)
            {
                closeConnection();
                CCFBGlobal.appendErrorToErrorReport("getDistinctsYears", ex.GetBaseException().ToString());
            }

        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            getClientsList(true);
        }

        /// <summary>
        /// Gets the CSFP Clients for the given period using a stored procedure
        /// in the database
        /// </summary>
        private void getClientsList(bool loaddgvCSFP)
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

                    SqlParameter parameter3 = new SqlParameter();
                    parameter3.ParameterName = "@WhereClause";
                    parameter3.SqlDbType = SqlDbType.NVarChar;
                    parameter3.Direction = ParameterDirection.Input;
                    parameter3.Value = getWhereClause();
                    
                    // Add the parameter to the Parameters collection. 
                    command.Parameters.Add(parameter);
                    command.Parameters.Add(parameter2);
                    command.Parameters.Add(parameter3);

                    using (SqlDataAdapter da = new SqlDataAdapter(command))
                    {
                        rowCount = da.Fill(dtbl);
                    }
                    closeConnection();
                    if (loaddgvCSFP == true)
                    {
                        loadList(dtbl.Select());
                    }
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
                    toolStrip1.Enabled = true;
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
            btnRefreshList.Visible = false;
            progressBar1.Value = 0;
            progressBar1.Visible = true;
            progressBar1.Step = 1;
            progressBar1.Maximum = drows.Length;

            tbCnt.Text = drows.Length.ToString();
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

                dgvCSFP["Route", i].Value = drows[i]["Route"]; 
                dgvCSFP["HouseholdID", i].Value = drows[i]["HouseholdID"];
                dgvCSFP["clmCSFPStatus", i].Value = drows[i]["StatusDescr"];
                dgvCSFP["clmName", i].Value = drows[i]["colNameLF"];
                dgvCSFP["Address", i].Value = drows[i]["Address"];
                dgvCSFP["AptNbr", i].Value = drows[i]["AptNbr"];
                dgvCSFP["CSFPExpiration", i].Value = drows[i]["CSFPExpiration"];
                if (dgvCSFP["CSFPExpiration", i].Value.ToString().Length >0 &&
                    Convert.ToDateTime(dgvCSFP["CSFPExpiration", i].Value) < toCheck)
                {
                    dgvCSFP["CSFPExpiration", i].Style.BackColor = Color.Yellow;
                }

                if (drows[i]["PrevService"] == DBNull.Value)
                {
                    dgvCSFP["clmLastService", i].Value = "----";
                }
                else
                {
                    dgvCSFP["clmLastService", i].Value = Convert.ToDateTime(drows[i]["PrevService"]).ToShortDateString();
                }
                dgvCSFP["clmMethodAsInt", i].Value = drows[i]["RouteAsInt"];
                dgvCSFP["clmDateServed", i].Value = drows[i]["TrxDate"];
                dgvCSFP["clmLbs", i].Value = drows[i]["Lbs"];
                dgvCSFP["clmHHMemID", i].Value = drows[i]["hhmID"];
                dgvCSFP.Rows[i].Cells["colNameLF"].Value = drows[i]["colNameLF"].ToString().ToUpper().Trim();
                dgvCSFP.Rows[i].Cells["colNameFL"].Value = drows[i]["colNameFL"].ToString().ToUpper().Trim();
                dgvCSFP.Rows[i].Cells["clmCSFP"].Value = drows[i]["CSFP"];
                dgvCSFP.Rows[i].Cells["clmLogID"].Value = drows[i]["LogId"];
                dgvCSFP.Rows[i].Cells["clmPhone"].Value = drows[i]["Phone"].ToString().Trim();
                dgvCSFP.Rows[i].Cells["clmCSFPComments"].Value = drows[i]["CSFPComments"].ToString().Trim();
                progressBar1.PerformStep();
            }

            rowIndex = 0;
            progressBar1.Value = 0;
            progressBar1.Visible = false;
            btnRefreshList.Show();
            setFindClientVisible();
            tbFindName.Visible = true;

            Application.DoEvents();
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
            if (String.IsNullOrEmpty(tbFindName.Text.Trim()) == true)
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
            if (loading == false)
            {
                loadListOncboFilter();
            }
        }

        private void loadListOncboFilter()
        {
            if (rowCount > 0 && cboFilter.Text != nofilter && cboFilterFld.SelectedIndex >0)
            {
                DataRow[] drows;
                drows = dtbl.Select(filterFldName + "='" + cboFilter.SelectedItem.ToString() + "'");
                loadList(drows);
            }
            else 
            {
                loadList(dtbl.Select());
            }
        }
        
        private void getDistincts(string colName, bool fmtAsShortdate)
        {
            filterFldName = colName;
            //Fill cboFilter
            DataView Dv = dtbl.DefaultView;
            DataTable dtDistinct = Dv.ToTable(true, filterFldName);
            loading = true;
            cboFilter.Items.Clear();
            cboFilter.Items.Add(nofilter);
            cboFilter.SelectedIndex = 0;
            for (int i = 0; i < dtDistinct.Rows.Count; i++)
            {
                if (fmtAsShortdate == true)
                    cboFilter.Items.Add(CCFBGlobal.ValidDate(dtDistinct.Rows[i][0]).ToShortDateString());
                else
                    cboFilter.Items.Add(dtDistinct.Rows[i][0].ToString());
            }
            cboFilter.Visible = (cboFilter.Items.Count > 1);
            loading = false;
        }

        private void cboOrderBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (loading == false)
            {
                ComboBox cbo = (ComboBox)sender;
                string curUID = "";
                loading = true;
                for (int i = Convert.ToInt32(cbo.Tag)+1; i < cboSort.Count; i++)
                {
                    curUID = ((parmType)cboSort[i].SelectedItem).UID;
                    setcboSortOrder(Convert.ToInt32(cboSort[i].Tag));
                    setcboSortSelectedIndex(i,Convert.ToInt32(curUID));
                }
                loading = false;
                getClientsList(true);

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
                    if (dgvRow.Cells["CSFPExpiration"].Value.ToString().Length >0)
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

                        if (tmpdate.Length >0)
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

                        if (tmpdate.Length >0)
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
            Application.DoEvents();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string checkState = "0";
            if (chkCSFP.Checked == true)
            {
                checkState = "1";
            }
            string updateIDs = getHhMUpdateIds();
            if (updateIDs.Length >0)
            {
                int nbrRows = CCFBGlobal.executeQuery("Update HouseholdMembers Set CSFPExpiration =' "
                            + dtpExpDate.Value.ToString() + "', CSFP = " + checkState
                            + " Where ID IN (" + updateIDs + ")");
                MessageBox.Show("The CSFP Expiration date on " + nbrRows.ToString() + " CSFP Clients has been changed to [" + dtpExpDate.Value.ToString() + "]"
               , "CSFP Expiration Date Change", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            changeGroupBoxVisibility(0);
            refreshdgvCSFP();
        }

        private void MarkNewCSFPClient_Click(object sender, EventArgs e)
        {
            FindClientForm frmFindClient = new FindClientForm();
            frmFindClient.ShowDialog();
            bool findCanceled = frmFindClient.Canceled;
            hhID = frmFindClient.CurrentHHId;
            frmFindClient.Dispose();
            if (findCanceled == false)
            {
                NewCSFPSelectionForm frmNewCSFP = new NewCSFPSelectionForm(hhID);
                frmNewCSFP.ShowDialog();
                refreshdgvCSFP();
                frmNewCSFP.Dispose();
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
            int nbrRows = CCFBGlobal.executeQuery("Update CSFPLog Set TrxDate='" + dtpSvcDate.Value.ToString() + "', "
                          + "Lbs=" + tbLbsCSFP.Text + ", Modified='" + DateTime.Now.ToString() + "', "
                          + "ModifiedBy='" + CCFBGlobal.dbUserName + "' "
                          + "Where ID in (" + updateIDs + ")");
            MessageBox.Show("The TrxDate and Lbs on " + nbrRows.ToString() + " CSFP Transaction Log has been changed"
               , "Update Existing CSFP Service", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btnSvcSave_Click(object sender, EventArgs e)
        {
            string insertIDs = "";
            string updateIDs = "";
            for (int i = 0; i < dgvCSFP.SelectedRows.Count; i++)
            {
                if (dgvCSFP.SelectedRows[i].Cells["clmDateServed"].Value.ToString().Length >0)
                {
                    if (MessageBox.Show("CSFP Client " + dgvCSFP.SelectedRows[i].Cells["clmName"].Value.ToString()
                         + " Already Has A Service For This Period. Would You Like To Update The Service?",
                         "Service Already Exists For This Period", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                        == System.Windows.Forms.DialogResult.Yes)
                    {
                        if (updateIDs.Length >0)
                            updateIDs += ", ";

                        updateIDs += dgvCSFP.SelectedRows[i].Cells["clmLogID"].Value.ToString();
                    }
                }
                else
                {
                    if (insertIDs.Length >0)
                        insertIDs += ", ";

                    insertIDs += dgvCSFP.SelectedRows[i].Cells["clmHHMemID"].Value.ToString();
                }
                
            }

            openConnection();
            if (insertIDs.Length >0)
                clsCSFPLog.insertNewService(insertIDs, dtpSvcDate.Value, tbLbsCSFP.Text);

            if (updateIDs.Length >0)
            {
                updateExistingService(updateIDs);
            }
            svcDate = dtpSvcDate.Value;
            closeConnection();
            changeGroupBoxVisibility(0);
            refreshdgvCSFP();

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

        private void refreshdgvCSFP()
        {
            int cboFilterFldValue = cboFilterFld.SelectedIndex;
            string cboFilterValue = cboFilter.Text;
            getClientsList(false);
            loading = true;
            if (cboFilterFldValue != 0)
            {
                cboFilterFld.SelectedIndex = cboFilterFldValue;
            }
            for (int i = 0; i < cboFilter.Items.Count; i++)
            {
                if (cboFilter.Items[i].ToString() == cboFilterValue)
                {
                    cboFilter.SelectedIndex = i;
                    break;
                }
            }
            loading = false;
            loadListOncboFilter();
        }

        private void tbLbsCSFP_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(tbLbsCSFP.Text.Trim()) == true)
                tbLbsCSFP.Text = "0";
        }

        private void tbLbsCSFP_KeyDown(object sender, KeyEventArgs e)
        {
            CCFBGlobal.checkForIntOnKeyPress(e);
        }

        private void PrintPicketlist_Click(object sender, EventArgs e)
        {
            string route = "";
            if(cboFilterFld.SelectedIndex == 1 && cboFilter.SelectedIndex !=0)
                route += cboFilter.SelectedItem.ToString();
            else
            {
                route += "ALL Routes";
            }


            RptCSFPPicklist clsCreatePicklist = new RptCSFPPicklist(CCFBPrefs.FoodBankName);
            clsCreatePicklist.createReport(cboYear.SelectedItem.ToString(), getFormatedMonthNumber(cboMonth.SelectedIndex + 1)
                , CCFBGlobal.fb3TemplatesPath + "CSFPPicklist.doc", dgvCSFP,
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
            frmTemp.Dispose();
        }

        private void menuFNSWebSite_Click(object sender, EventArgs e)
        {
            WebPageForm frmTemp = new WebPageForm("Food and Nutrition Web Site", 
                "http://www.fns.usda.gov/fdd/programs/csfp/");
            frmTemp.ShowDialog();
            frmTemp.Dispose();
        }

        private void menuCSFPState_Click(object sender, EventArgs e)
        {
            WebPageForm frmTemp = new WebPageForm("State CSFP Information",
                "http://agr.wa.gov/FoodProg/CSFP.aspx");
            frmTemp.ShowDialog();
            frmTemp.Dispose();
        }

        private void DeleteService_Click(object sender, EventArgs e)
        {
            string deleteIDs = "";

            for (int i = 0; i < dgvCSFP.SelectedRows.Count; i++)
            {
                if (dgvCSFP.SelectedRows[i].Cells["clmDateServed"].Value.ToString().Length >0)
                {
                    if (deleteIDs.Length >0)
                        deleteIDs += ", ";

                    deleteIDs += dgvCSFP.SelectedRows[i].Cells["clmLogID"].Value.ToString();
                }
            }

            if (deleteIDs.Length >0)
            {
                openConnection();
                deleteCSFPServices(deleteIDs);
                closeConnection();
                refreshdgvCSFP();
            }
        }

        private void deleteCSFPServices(string deleteIDs)
        {
            int nbrRows = CCFBGlobal.executeQuery("Delete From CSFPLog "
                          + "Where ID in (" + deleteIDs + ")");
            MessageBox.Show(nbrRows.ToString() + " CSFP Service have been deleted."
                           , "Delete CSFP Service", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    getDistincts("Route", false);       //Distribution Method
                    break;
                case 2:
                    getDistincts("CSFPStatus", false);  //Status
                    break;
                case 3:
                    getDistincts("Address", false);     //Address
                    break;
                case 4:
                    getDistincts("TrxDate", true);      //Period Service Date
                    break;
                case 5:
                    getDistincts("PrevService", true);  //Previous Service Date
                    break;
                case 6:
                    getDistincts("CSFPExpiration",true);//Expiration Date
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
            bool curLoading = loading;
            string whereClause = "";
            loading = true;
            if (idx > 0)
            {
                for (int i = 0; i < idx; i++)
                {
                    if (cboSort[i].SelectedIndex > 0)
                    {
                        if (String.IsNullOrEmpty(whereClause) == true)
                        {
                            whereClause = "WHERE ID NOT IN(" + cboSort[i].SelectedValue.ToString();
                        }
                        else
                        {
                            whereClause += ", " + cboSort[i].SelectedValue.ToString();
                        }
                    }
                }
                if (whereClause.Length >0)
                    whereClause += ")";
            }
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
            loading = curLoading;
        }

        private void setcboSortSelectedIndex(int idx, int parmID)
        {
            bool curLoading = loading;
            loading = true;
            foreach (parmType item in cboSort[idx].Items)
            {
                if (Convert.ToInt32(item.UID) == parmID)
                {
                    cboSort[idx].SelectedItem = item;
                    break;
                }
            }
            loading = curLoading;
        }

        private string getOrderByText()
        {
            string sql="";
            foreach (ComboBox cbo in cboSort)
            {
                if (cbo.SelectedIndex > 0)
                {
                    if (String.IsNullOrEmpty(sql) == true)
                    {
                        sql = " ORDER BY ";
                    }
                    else
                    {
                        sql += ", "; 
                    }
                    sql += ((parmType)cbo.Items[cbo.SelectedIndex]).ShortName;
                    int i = Convert.ToInt32(cbo.Tag.ToString());
                    if (cboAscDesc[i].SelectedIndex > 0)
                    {
                        sql += " DESC";
                    }
                }
            }
            return sql;
        }

        private string getWhereClause()
        {
            string sList = "";
            foreach (CheckBox chk in chkBoxList)
	        {
		        if (chk.Checked == true)
                {
                    if (String.IsNullOrEmpty(sList)  == true)
                    {
                        sList = chk.Tag.ToString();
                    }
                    else
                    {
                        sList += "," + chk.Tag.ToString();
                    }
                }
	        }
            if (String.IsNullOrEmpty(sList) == true)
            {
                sList = "1";
            }
            return " WHERE hhm.ID IN (SELECT ID hhmID FROM HouseholdMembers WHERE CSFP = 1 AND Inactive = 0 AND CSFPStatus IN (" + sList + "))";
        }

        private void gbRenewExpDate_Enter(object sender, EventArgs e)
        {

        }

        private void cboAscDesc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (loading == false)
            {
                refreshdgvCSFP();
            }
        }

        private void cmsdgvCSFP_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string tmp = "CSFPClients-" + cboYear.Text + '-' + cboMonth.Text;
            if (cboFilterFld.SelectedIndex > 0 && cboFilter.SelectedIndex > 0)
            {
                tmp += "-" + cboFilterFld.Text + " " + cboFilter.Text;
            }
            CCFBGlobal.ExportToExcel(dgvCSFP,tmp);
        }

        private void tsbMarkStatus_Click(object sender, EventArgs e)
        {
            ToolStripButton btn = (ToolStripButton)sender;
            string newStatus = btn.Tag.ToString();
            string updateIDs = getHhMUpdateIds();
            if (updateIDs.Length >0)
            {
                int nbrRows = CCFBGlobal.executeQuery("Update Householdmembers Set CSFPStatus=" + newStatus
                            + ", Modified='" + DateTime.Now.ToString() + "', "
                            + "ModifiedBy='" + CCFBGlobal.dbUserName + "' "
                            + "Where ID in (" + updateIDs + ")");
                MessageBox.Show("The CSFPStatus on " + nbrRows.ToString() + " CSFP Clients has been changed to [" + btn.Text.Replace("Mark", "") + "]"
                               , "CSFP Status Change", MessageBoxButtons.OK, MessageBoxIcon.Information);
                refreshdgvCSFP();
            }
            else
            {
                MessageBox.Show("No CSFP Clients are selected", "Change CSFP Status", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private string getHhMUpdateIds()
        {
            string updateIDs = "";
            for (int i = 0; i < dgvCSFP.SelectedRows.Count; i++)
            {
                if (updateIDs.Length >0)
                {
                    updateIDs += ", ";
                }
                updateIDs += dgvCSFP.SelectedRows[i].Cells["clmHHMemID"].Value.ToString();
            }
            return updateIDs;
        }
    }
}
