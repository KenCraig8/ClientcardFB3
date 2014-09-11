using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ClientcardFB3
{
    public partial class HDPlannerForm : Form
    {
        SqlConnection conn;

        TrxLog clsTrxLog;
        TrxLogItem clsTrxItem;
        HDRoutes clsHDRoutes = new HDRoutes(CCFBGlobal.connectionString);
        HDRouteSheet clsHDRouteSheet = new HDRouteSheet(CCFBGlobal.connectionString);
        HDRSHist clsHDRSHist = new HDRSHist(CCFBGlobal.connectionString);
        parmTypeCodes parmHDRouteSheetStatus = new parmTypeCodes(CCFBGlobal.parmTbl_HDRouteSheetStatus, CCFBGlobal.connectionString, "");
        HDItems clsHDItems = new HDItems(CCFBGlobal.connectionString);

        MainForm frmMain;

        DataTable rtplnDTbl = new DataTable();
        SqlDataAdapter rtplnDadAdpt = new SqlDataAdapter();
        SqlCommand rtplnSqlCmd;
        string rtplnSqlCmdText = "Select h.ID, h.Name, h.Address, h.AptNbr, h.City, h.Zipcode, h.LatestService, h.HDRoute, h.Phone"
            + ",RTRIM(case WHEN Infants = 0 THEN '' ELSE CAST(Infants as Varchar(3)) + 'I ' END"
            + " + case WHEN Youth+Teens+Eighteen = 0 THEN '' ELSE CAST(Youth+Teens+Eighteen as Varchar(3)) + 'C 'END"
            + " + case WHEN Adults = 0 THEN '' ELSE CAST(Adults as Varchar(3)) + 'A ' END"
            + " + case WHEN Seniors = 0 THEN '' ELSE CAST(Seniors as Varchar(3)) + 'S' END) FamilySize"
            + ",r.RouteTitle, h.Inactive, h.Comments, h.HDItem, h.DriverNotes"
            + "  FROM Household h "
            + " INNER JOIN HDRoutes r ON h.HDRoute = r.ID"
            + " WHERE h.ServiceMethod = 2 ";
        string rtplnAndStatement = "";
        string rtplnSortColName = "clmName";
        string rtplnOrderBy = " Order By h.Name ";
        string rtplnLastSearchText = "";
        string rtplnOriValue = "";
        string rtplnFilterColName = "";
        int rtplnRowCount = 0;
        int rtplnrowIndex = 0;

        bool loading = false;
        DateTime dfltServiceDate = DateTime.Today;
        DateTime currentServiceDate;
        string savePath = CCFBPrefs.ReportsSavePath;
        string sOriRouteText = "";
        string sOriPhoneText = "";
        //Counters to control tool strip buttons
        const int maxRouteStatusRows = 4;
        int[] cntrsRoutesStatus;

        FindClientForm frmFindClient;


        public HDPlannerForm(MainForm frmMainIn)
        {
            InitializeComponent();
            frmMain = frmMainIn;
            conn = new SqlConnection(CCFBGlobal.connectionString);
            rtplnSqlCmd = new SqlCommand("", conn);
            filllvwStatus();
            clsHDItems.openWhere("");
            //CCFBGlobal.dtPopulateCombo((DataGridViewComboBoxColumn)rtplndgvHD.Columns["clmSvcItem"], "SELECT * From HDItems", "ShortName", "ID", "Std", conn);
            dtpServiceDate.Value = dfltServiceDate;
            loadRouteList(dfltServiceDate);
            rtplnRefreshlbxRoutes(-1);
            rtplnDisplaySelectedRoute();
            frmFindClient = new FindClientForm();
        }

        private string convertToShortDate(string date)
        {
            if(date != "")
            {
                return DateTime.Parse(date).ToShortDateString();
            }
            return "";
        }

        private void FillFilterByCombo(DataTable dt, ComboBox cbo)
        {
            loading = true;
            cbo.Items.Clear();
            cbo.Items.Add("No Filter");
            cbo.SelectedIndex = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cbo.Items.Add(dt.Rows[i][0].ToString());
            }
            loading = false;
            cbo.Visible = (cbo.Items.Count > 1);
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

        private void giveNewService(string insertIDs)
        {
            
        }

        private void updateExistingService(string updateIDs)
        {
            //try
            //{
            //    updateAndInsertComm = new SqlCommand("Update CSFPLog Set TrxDate='" + dtpSvcDate.Value.ToString() + "', "
            //              + "Lbs=" + tbLbsCSFP.Text + ", Modified='" + DateTime.Now.ToString() + "', "
            //              + "ModifiedBy='" + CCFBGlobal.currentUser_Name + "' "
            //              + "Where ID in (" + updateIDs + ")", conn);
            //    updateAndInsertComm.ExecuteNonQuery();
            //}
            //catch (SqlException ex)
            //{
            //    CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString());
            //}
        }

        private void tbLbsCSFP_KeyDown(object sender, KeyEventArgs e)
        {
            CCFBGlobal.checkForIntOnKeyPress(e);
        }

        private void EditHDForm_Resize(object sender, EventArgs e)
        {
            if (this.WindowState != FormWindowState.Minimized)
                rtplndgvHD.Top = 140;
        }

        private void DeleteService_Click(object sender, EventArgs e)
        {
            //string deleteIDs = "";

            //for (int i = 0; i < dgvHD.SelectedRows.Count; i++)
            //{
            //    if (dgvHD.SelectedRows[i].Cells["clmDateServed"].Value.ToString() != "")
            //    {
            //        if (deleteIDs != "")
            //            deleteIDs += ", ";

            //        deleteIDs += dgvHD.SelectedRows[i].Cells["clmLogID"].Value.ToString();
            //    }
            //}

            //if (deleteIDs != "")
            //{
            //    openConnection();
            //    //deleteCSFPServices(deleteIDs);
            //    closeConnection();
            //    getHomeDeliveryClients();
            //}
        }

        //private void cboHDRoute_SelectionChangeCommitted(object sender, EventArgs e)
        //{
        //    if (Convert.ToInt32(cboHDRoute.SelectedValue) == 0)
        //    {
        //        andStatement = "";
        //    }
        //    else
        //    {
        //        andStatement = " AND h.HDRoute = " + cboHDRoute.SelectedValue + " ";
        //    }
        //    command = new SqlCommand(sqlCommandText + andStatement + orderBy, conn);
        //    displayRouteInfo(Convert.ToInt32(cboHDRoute.SelectedValue));
        //    getHomeDeliveryClients();
        //    loadList();
        //}

        private void tsbToggle_CheckedChanged(object sender, EventArgs e)
        {
            string colName  = ((ToolStripButton)sender).Tag.ToString();
            rtplndgvHD.Columns[colName].Visible = !rtplndgvHD.Columns[colName].Visible;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            dfltServiceDate = dtpServiceDate.Value;
            loadRouteList(dfltServiceDate);
        }

        private void loadRouteList(DateTime svcdate)
        {
            currentServiceDate = svcdate;
            clsHDRouteSheet.openWhere(svcdate.ToShortDateString());
            filllvwRouteStatus();
        }

        private void filllvwRouteStatus()
        {
            loading = true;
            cntrRoutesStatusInit();
            lvwRouteStatus.Items.Clear();
            for (int i = 0; i < clsHDRouteSheet.RowCount; i++)
            {
                clsHDRouteSheet.setDataRow(i);
                ListViewItem lvi = new ListViewItem(clsHDRouteSheet.RouteTitle);
                lvi.Tag = clsHDRouteSheet.RouteStatus;
                lvi.SubItems.Add(clsHDRouteSheet.NbrClients.ToString());
                lvi.SubItems.Add(parmHDRouteSheetStatus.GetLongName(Convert.ToInt32(clsHDRouteSheet.RouteStatus)));
                lvi.SubItems.Add(clsHDRouteSheet.HDRoute.ToString());
                lvi.SubItems.Add(clsHDRouteSheet.ID.ToString());

                switch (clsHDRouteSheet.RouteStatus)
                {
                    case 0:
                        lvi.BackColor = Color.LightSalmon;
                        break;
                    case 1:
                        lvi.BackColor = Color.LightCyan;
                        break;
                    case 2:
                        lvi.BackColor = Color.LightSteelBlue;
                        break;
                    case 3:
                        lvi.BackColor = Color.LightSeaGreen;
                        break;
                    default:
                        break;
                }
                lvwRouteStatus.Items.Add(lvi);
            }
            EnableRouteControls(true);
            loading = false;
        }

        private int countNbrSelectedRoutes()
        {
            int cntr = 0;
            foreach (ListViewItem item in lvwRouteStatus.Items)
            {
                if (item.Checked == true)
                {
                    cntr++;
                }
            }
            return cntr;
        }

        private void createRouteStatusRow(int RouteID, DateTime trxDate)
        {
            if (clsHDRouteSheet.Add(RouteID, trxDate) < 0)
            {
                MessageBox.Show("Insert RouteList for RouteID = " + RouteID.ToString() + " and Date = '" + trxDate.ToShortDateString() + "' Failed", "Create Route Status Row", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

        }

        private void filllvwStatus()
        {
            
            lvwStatus.Items.Clear();
            for (int i = 0; i < parmHDRouteSheetStatus.TypeCodesArray.Count; i++)
            {
                string tmp = parmHDRouteSheetStatus.GetLongName(i);
                ListViewItem lvi = new ListViewItem(tmp);
                lvi.Tag = parmHDRouteSheetStatus.GetId(tmp);
                lvi.Checked = true;
                lvwStatus.Items.Add(lvi);
            }
        }

        private void lvwStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            loading = true;
            cntrRoutesStatusInit();
            if (chkSelectAll.Checked == true)
            {
                foreach (ListViewItem item in lvwRouteStatus.Items)
                {
                    item.Checked = true;
                    cntrsRoutesStatus[(int)item.Tag]++;
                }
            }
            else
            {
                foreach (ListViewItem item in lvwRouteStatus.Items)
                {
                    item.Checked = false;
                }
            }
            loading = false;
            EnableRouteButtons();
        }

        private void tsbPost_Click(object sender, EventArgs e)
        {
            clsTrxLog = new TrxLog(CCFBGlobal.connectionString,true,false,false,false);
            Household clsHH = new Household(CCFBGlobal.connectionString);
            HHMembers clsHHM = new HHMembers(CCFBGlobal.connectionString);
            int rsid = 0;
            foreach (ListViewItem item in lvwRouteStatus.Items)
            {
                if (item.Checked == true && Convert.ToInt32(item.Tag) == 2)
                {
                    item.BackColor = Color.DarkOrchid;
                    Application.DoEvents();
                    rsid = Convert.ToInt32(item.SubItems[4].Text);
                    clsHDRouteSheet.find(rsid, true);
                    for (int i = 0; i < clsHDRouteSheet.RSClients.RowCount; i++)
                    {
                        clsHDRouteSheet.RSClients.setDataRow(i);
                        clsTrxLog.openForHH(clsHDRouteSheet.RSClients.HHID);
                        clsHH.open(clsHDRouteSheet.RSClients.HHID);
                        clsHHM.openHHID(clsHDRouteSheet.RSClients.HHID);
                        clsHDItems.find(clsHDRouteSheet.RSClients.HDItem);
                        clsTrxItem = new TrxLogItem(clsTrxLog.DSet.Tables[0].NewRow(), clsHH, clsHHM, clsHDRouteSheet.DriverName,TrxLogItem.SvcMethod.Pickup );
                        clsTrxItem.TrxDate = currentServiceDate;
                        clsTrxItem.LbsBabySvc = clsHDItems.LbsBabySvc;
                        clsTrxItem.LbsCommodities = clsHDItems.LbsCommodity;
                        clsTrxItem.LbsNonFood = clsHDItems.LbsNonFood;
                        clsTrxItem.LbsOther = clsHDItems.LbsOther;
                        clsTrxItem.LbsStandard = clsHDItems.LbsStd;
                        clsTrxItem.LbsSupplemental = clsHDItems.LbsSupplemental;
                        clsTrxItem.FoodSvcList = "HD-" + clsHDItems.Description;
                        if (clsTrxItem.LbsCommodities > 0)
                            clsTrxItem.RcvdCommodity = true;
                        if (clsTrxItem.LbsSupplemental > 0)
                            clsTrxItem.RcvdSupplemental = true;
                        clsTrxItem.Created = DateTime.Now;
                        clsTrxItem.CreatedBy = CCFBGlobal.dbUserName;
                        clsTrxLog.DSet.Tables[0].Rows.Add(clsTrxItem.DRow);
                        clsTrxLog.update(0, "");
                        clsTrxLog.updateServiceBits(clsHH.ID, currentServiceDate);
                        clsHH.UpdateLatestServiceDates(currentServiceDate.ToShortDateString());
                    }
                    clsHDRouteSheet.updateRouteStatus(HDRouteSheet.HDRSStatus.Posted);
                }
            }
            clsHDRouteSheet.refreshDataTable();
            filllvwRouteStatus();
        }

        private void tsbPrepare_Click(object sender, EventArgs e)
        {
            int cntr = 0;
            foreach (ListViewItem item in lvwRouteStatus.Items)
            {
                if (item.Checked == true && Convert.ToInt32(item.Tag) == 0)
                {
                    item.BackColor = Color.DarkOrchid;
                    cntr++;
                    createRouteStatusRow(Convert.ToInt32(item.SubItems[3].Text), dfltServiceDate);
                }
            }
            MessageBox.Show(cntr.ToString() + " route lists have been created for '" + dfltServiceDate.ToShortDateString() + "'", "Create Route Lists", MessageBoxButtons.OK);
            loadRouteList(dfltServiceDate);
            filllvwRouteHist();
        }

        private void tsbPrint_Click(object sender, EventArgs e)
        {
            RptHDRouteSheet clsCreateRS = new RptHDRouteSheet();
            int rsid = 0;
            foreach (ListViewItem item in lvwRouteStatus.Items)
            {
                if (item.Checked == true && Convert.ToInt32(item.Tag) == 1)
                {
                    item.BackColor = Color.DarkOrchid;
                    Application.DoEvents();
                    rsid = Convert.ToInt32(item.SubItems[4].Text);
                    clsHDRouteSheet.find(rsid, true);
                    string saveFolder = savePath + clsHDRouteSheet.rptPath();
                    DirectoryInfo di = new DirectoryInfo(saveFolder);
                    if (di.Exists == false)
                    {
                        di.Create();
                    }
                    Object saveAs = saveFolder + clsHDRouteSheet.rptFileName();
                    clsCreateRS.createReport(saveAs, CCFBGlobal.fb3TemplatesPath + "HDRouteSheet.doc", clsHDRouteSheet);
                }
            }
            //clsHDRouteSheet.openWhere(currentServiceDate.ToShortDateString());
            clsHDRouteSheet.refreshDataTable();
            filllvwRouteStatus();
        }

        private void tsbShowHistory_Click(object sender, EventArgs e)
        {

        }

        private void tsbShowRpts_Click(object sender, EventArgs e)
        {
            int rsid = 0;
            foreach (ListViewItem item in lvwRouteStatus.Items)
            {
                if (item.Checked == true)
                {
                    item.BackColor = Color.DarkOrchid;
                    rsid = Convert.ToInt32(item.SubItems[4].Text);
                    clsHDRouteSheet.find(rsid, false);
                    string rptFullName = savePath + clsHDRouteSheet.rptPath() + clsHDRouteSheet.rptFileName();
                    CCFBGlobal.openDocumentOutsideCCFB(rptFullName);
                }
            }
        }

        private void dtpServiceDate_ValueChanged(object sender, EventArgs e)
        {
            EnableRouteControls(false);
        }

        private void EnableRouteButtons()
        {
             tsbPrepare.Enabled = (cntrsRoutesStatus[0]>0);
             tsbPrint.Enabled = (cntrsRoutesStatus[1]>0);
             tsbPost.Enabled = (cntrsRoutesStatus[2]>0);
        }

        private void EnableRouteControls(bool isEnabled)
        {
            lvwRouteStatus.Enabled = isEnabled;
            if (isEnabled == false)
            {
                tsbPrepare.Enabled = false;
                tsbPrint.Enabled = false;
                tsbPost.Enabled = false;
            }
        }

        private void cntrRoutesStatusInit()
        {
            cntrsRoutesStatus = new int[maxRouteStatusRows];
        }

        private void lvwRouteStatus_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (loading == false)
            {
                int idx = (int)e.Item.Tag;
                if (e.Item.Checked == true)
                {
                    cntrsRoutesStatus[idx]++;
                }
                else
                {
                    if (cntrsRoutesStatus[idx] > 0)
                    {
                        cntrsRoutesStatus[idx]--;
                    }
                }
                EnableRouteButtons();
            }
        }

        private void btnRefreshHist_Click(object sender, EventArgs e)
        {
            filllvwRouteHist();
        }

        private void filllvwRouteHist()
        {
            lvwRouteHist.Items.Clear();
            clsHDRSHist.openHistory(cboHistPeriod.SelectedIndex);
            for (int i = 0; i < clsHDRSHist.RowCountHist; i++)
            {
                clsHDRSHist.setHistDataRow(i);
                ListViewItem lvi = new ListViewItem(clsHDRSHist.DelivDate.ToShortDateString());
                lvi.SubItems.Add((lvwRouteStatus.Items.Count  - (clsHDRSHist.NbrPrepared + clsHDRSHist.NbrPrinted + clsHDRSHist.NbrPosted)).ToString());
                lvi.SubItems.Add(clsHDRSHist.NbrPrepared.ToString());
                lvi.SubItems.Add(clsHDRSHist.NbrPrinted.ToString());
                lvi.SubItems.Add(clsHDRSHist.NbrPosted.ToString());
                lvwRouteHist.Items.Add(lvi);
            }
        }

        private void lvwRouteHist_DoubleClick(object sender, EventArgs e)
        {
            if (lvwRouteHist.SelectedItems.Count > 0)
            {
                dtpServiceDate.Value = Convert.ToDateTime(lvwRouteHist.SelectedItems[0].Text);
                Application.DoEvents();
                dfltServiceDate = dtpServiceDate.Value;
                loadRouteList(dfltServiceDate);
            }
        }

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            loading = true;
            int lastID = clsHDRoutes.maxRouteId();
            int newRouteID = clsHDRoutes.Add();
            loading = false;
            rtplnRefreshlbxRoutes(newRouteID);
            frmMain.refreshHDRoute();
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            if (clsHDRoutes.RowCount > 0)
            {
                DialogResult dr = MessageBox.Show("Press OK to Delete this Route\r\nID = " + clsHDRoutes.ID.ToString() + "\r\nTitle = " + clsHDRoutes.RouteTitle, "Delete Home Delivery Route"
                                   , MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.OK)
                {
                    int idx = rtplnlbxRoutes.SelectedIndex;
                    loading = true;
                    clsHDRoutes.delete(clsHDRoutes.ID);
                    clsHDRoutes.refreshDataTable();
                    loading = false;
                    if (idx < rtplnlbxRoutes.Items.Count)
                    { rtplnlbxRoutes.SelectedIndex = idx; }
                    else if (rtplnlbxRoutes.Items.Count > 0)
                    { rtplnlbxRoutes.SelectedIndex = 0; }
                    rtplnDisplaySelectedRoute();
                    frmMain.refreshHDRoute();
                }
            }
        }

        private void rtplnbtnSaveRoute_Click(object sender, EventArgs e)
        {
            clsHDRoutes.update();
            rtplnbtnSaveRoute.Enabled = false;
        }

        private void rtplncboFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (loading == false && rtplnRowCount > 0 )
            {
                if (rtplncboFilter.SelectedIndex == 0)
                {
                    rtplnLoadList(rtplnDTbl.Select());
                }
                else
                {
                    DataRow[] drows;
                    if (rtplnFilterColName != "")
                    {
                        drows = rtplnDTbl.Select(rtplnFilterColName + "='" + rtplncboFilter.SelectedItem.ToString() + "'");
                        rtplnLoadList(drows);
                    }
                }
            }
        }

        private void rtplncboOrderBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            rtplnSetdgvHDOrder();
        }

        private void rtplnClearRouteInfo()
        {
            rtplntbRouteID.Text = "";
            rtplntbRouteTitle.Text = "";
            rtplntbDriver.Text = "";
            rtplnmtDriverPhone.Text = "";
            rtplntbEstTime.Text = "";
            rtplntbEstMiles.Text = "";
            rtplntbRouteNotes.Text = "";
            rtplntbDriverNotes.Text = "";
            rtplntbFBContact.Text = "";
            rtplnmtFBContactPhone.Text = "";
            rtplndgvHD.Rows.Clear();
        }

        private void rtplndgvHD_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            rtplnOriValue = ((DataGridView)sender).Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
        }

        private void rtplndgvHD_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (rtplnOriValue != ((DataGridView)sender).Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString())
            {
                switch (e.ColumnIndex)
                {
                    case 9:
                    case 10:
                    default:
                        break;
                }

            }
        }

        private void rtplndgvHD_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (frmMain != null)
            {
                if (e.ColumnIndex > 1)
                {
                    int rowIdx = e.RowIndex;
                    int hhID = Convert.ToInt32(rtplndgvHD["clmID", rowIdx].Value);
                    frmMain.setHousehold(hhID, 0);
                }
            }
        }

        private void rtplnDisplayRouteInfo(int idx)
        {
            rtplnClearRouteInfo();
            rtplnSetCmdText(" And h.HDRoute = " + idx.ToString() + " ");
            clsHDRoutes.find(idx, true);
            rtplnbtnSaveRoute.Enabled = false;
            if (clsHDRoutes.ID == idx)
            {
                rtplntbRouteID.Text = clsHDRoutes.ID.ToString();
                rtplntbRouteTitle.Text = clsHDRoutes.RouteTitle;
                rtplntbDriver.Text = clsHDRoutes.DriverName;
                rtplnmtDriverPhone.Text = clsHDRoutes.DriverPhone;
                rtplntbEstTime.Text = clsHDRoutes.EstHours.ToString("0.00");
                rtplntbEstMiles.Text = clsHDRoutes.EstMiles.ToString("0.0");
                rtplntbRouteNotes.Text = clsHDRoutes.Notes;
                rtplntbDriverNotes.Text = clsHDRoutes.DriverNotes;
                rtplntbFBContact.Text = clsHDRoutes.FBContactName;
                rtplnmtFBContactPhone.Text = clsHDRoutes.FBContactPhone;
            }
        }

        private void rtplnDisplaySelectedRoute()
        {
            if (clsHDRoutes.RowCount > 0 && loading == false)
            {
                rtplnDisplayRouteInfo(Convert.ToInt32(rtplnlbxRoutes.SelectedValue));
                pnlRouteInfo.Visible = true;
                rtplnGetHomeDeliveryClients();
            }
            else if (loading == false)
            {
                rtplnClearRouteInfo();
                pnlRouteInfo.Visible = false;
            }
        }

        private void rtplnFindByName(string colNameFull)
        {
            int rowStart = 0;
            if (rtplntbFindName.TextLength >= rtplnLastSearchText.Length)
                rowStart = rtplnrowIndex;
            else
                rowStart = 0;
            rtplnLastSearchText = rtplntbFindName.Text.ToUpper().Trim();
            for (int i = rowStart; i < rtplndgvHD.Rows.Count; i++)
            {
                if (rtplndgvHD.Rows[i].Cells[colNameFull].FormattedValue.ToString().ToUpper().StartsWith(rtplnLastSearchText) == true)
                {
                    rtplndgvHD.CurrentCell = rtplndgvHD[0, i];
                    if (i < rtplndgvHD.FirstDisplayedScrollingRowIndex
                        || i > rtplndgvHD.Rows.GetLastRow(DataGridViewElementStates.Displayed) - 5)
                        if (i > 5)
                            rtplndgvHD.FirstDisplayedScrollingRowIndex = i - 5;
                        else
                            rtplndgvHD.FirstDisplayedScrollingRowIndex = i;
                    break;
                }
            }
        }

        private void rtplnGetDistincts(string filterColName)
        {
            DataView Dv = rtplnDTbl.DefaultView;
            DataTable DtD = Dv.ToTable(true, filterColName);
            FillFilterByCombo(DtD, rtplncboFilter);
            lblFilterBy.Visible = rtplncboFilter.Visible;
        }

        private void rtplnGetHomeDeliveryClients()
        {
            try
            {
                openConnection();
                rtplnDTbl.Clear();
                rtplnDadAdpt.SelectCommand = rtplnSqlCmd;
                rtplnRowCount = rtplnDadAdpt.Fill(rtplnDTbl);
                closeConnection();
                rtplnLoadList(rtplnDTbl.Select());
            }
            catch (Exception ex)
            {
                CCFBGlobal.appendErrorToErrorReport("SelectCommand = " + rtplnSqlCmd.CommandText,
                    ex.GetBaseException().ToString());
                rtplnRowCount = 0;
            }
            //            EnableActionMenu();
        }

        private void rtplnlbxRoutes_SelectedValueChanged(object sender, EventArgs e)
        {
            rtplnDisplaySelectedRoute();
        }

        /// <summary>
        /// Loads the DataGrid using values obtained in the DataSet
        /// </summary>
        private void rtplnLoadList(DataRow[] dRows)
        {
            rtplntbFindName.Text = "";
            rtplnLastSearchText = "";
            rtplndgvHD.Rows.Clear();
            progressBar1.Value = 0;
            progressBar1.Show();
            rtplntbFindName.Visible = rtplncboFilter.Visible = lblFilterBy.Visible = false;
            Application.DoEvents();
            progressBar1.Maximum = dRows.Length;
            for (int i = 0; i < dRows.Length; i++)
            {
                rtplndgvHD.Rows.Add();
                rtplndgvHD["clmCnt", i].Value = (i + 1).ToString();
                rtplndgvHD["clmRouteID", i].Value = true;
                rtplndgvHD["clmRouteID", i].Value = dRows[i]["HDRoute"];
                rtplndgvHD["clmRouteTitle", i].Value = dRows[i]["RouteTitle"];
                rtplndgvHD["clmID", i].Value = dRows[i]["ID"];
                rtplndgvHD["clmName", i].Value = dRows[i]["Name"];
                rtplndgvHD["clmAddress", i].Value = dRows[i]["Address"].ToString() + "\r\n     " + dRows[i]["City"].ToString() + ", " + dRows[i]["ZipCode"].ToString();
                rtplndgvHD["clmApt", i].Value = dRows[i]["AptNbr"];
                rtplndgvHD["clmPhone", i].Value = CCFBGlobal.FormatPhone(dRows[i]["Phone"].ToString());
                rtplndgvHD["clmFamilySize", i].Value = dRows[i]["FamilySize"];
                rtplndgvHD["clmComments", i].Value = dRows[i]["Comments"];
                rtplndgvHD["clmDriverNotes", i].Value = dRows[i]["DriverNotes"];
                rtplndgvHD["clmSvcItem", i].Value = dRows[i]["HDItem"];
                rtplndgvHD["clmLastSvc", i].Value = CCFBGlobal.ValidDateString(dRows[i]["LatestService"]);
                rtplndgvHD[rtplnSortColName, i].Style.BackColor = Color.Azure;
                progressBar1.PerformStep();
            }
            lblRowCnt.Text = "[ " + rtplndgvHD.Rows.Count.ToString() + " ]";
            rtplnrowIndex = 0;
            foreach (ToolStripButton tsb in toolStrip2.Items)
            {
                rtplndgvHD.Columns[tsb.Tag.ToString()].Visible = tsb.Checked;
            }
            progressBar1.Value = 0;
            progressBar1.Visible = false;
            rtplntbFindName.Visible = true;
            if (rtplnFilterColName != "")
            {
                rtplnGetDistincts(rtplnFilterColName);
            }
        }

        private void rtplnRefreshlbxRoutes(int newID)
        {
            loading = true;
            clsHDRoutes.openWhere(" WHERE ID >0 ORDER BY RouteTitle");
            if (clsHDRoutes.RowCount > 0)
            {
                rtplnlbxRoutes.DataSource = clsHDRoutes.DTable;
                rtplnlbxRoutes.DisplayMember = "RouteTitle";
                rtplnlbxRoutes.ValueMember = "ID";
            }
            else
            {
                rtplnlbxRoutes.DataSource = null;
                rtplnlbxRoutes.Items.Add("No Routes Available");
            }
            loading = false;
            if (newID == -1)
            {
                rtplnlbxRoutes.SelectedIndex = 0;
            }
            else
            {
                for (int i = 0; i < rtplnlbxRoutes.Items.Count; i++)
                {
                    if (Convert.ToInt32(((DataRowView)rtplnlbxRoutes.Items[i])["ID"]) == newID)
                    {
                        rtplnlbxRoutes.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        private void rtplnSetCmdText(string andTxt)
        {
            rtplnAndStatement = andTxt;
            rtplnSqlCmd.CommandText = rtplnSqlCmdText + rtplnAndStatement + rtplnOrderBy;
        }

        private void rtplnSetDataChanged()
        {
            rtplnbtnSaveRoute.Enabled = true;
        }

        private void rtplnSetdgvHDOrder()
        {
            rtplncboFilter.Visible = false;
            rtplncboFilter.Text = "";
            lblFilterBy.Visible = false;
            rtplnFilterColName = "";
            switch (rtplncboOrderBy.SelectedIndex)
            {
                case 0:
                    rtplnOrderBy = " Order By h.ID ";
                    rtplnSortColName = "clmID";
                    break;
                case 2:
                    rtplnOrderBy = " Order By h.Address, h.AptNbr, h.Name";
                    rtplnSortColName = "clmAddress";
                    rtplnFilterColName = "Address";
                    break;
                case 3:
                    rtplnOrderBy = " Order By h.AptNbr, h.Name ";
                    rtplnSortColName = "clmApt";
                    rtplnFilterColName = "AptNbr";
                    break;
                case 4:
                    rtplnOrderBy = " Order By h.LatestService, h.Name ";
                    rtplnSortColName = "clmLastSvc";
                    rtplnFilterColName = "LastestService";
                    break;
                case 5:
                    rtplnOrderBy = " Order By h.Phone, h.Name ";
                    rtplnSortColName = "clmPhone";
                    break;
                case 6:
                    rtplnOrderBy = " Order By HDItem, h.Name ";
                    rtplnSortColName = "clmSvcItem";
                    break;
                default:
                    rtplnOrderBy = " Order By h.Name ";
                    rtplnSortColName = "clmName";
                    break;
            }
            rtplnSetCmdText(rtplnAndStatement);
            rtplnGetHomeDeliveryClients();
            lblFilterBy.Visible = rtplncboFilter.Visible;
        }

        private void rtplnSetFindClientVisible()
        {
            if (rtplndgvHD.Rows.Count > 0)
                pnlFindClient.Visible = true;
            else
                pnlFindClient.Visible = false;
        }

        private void rtplntbFindName_TextChanged(object sender, EventArgs e)
        {
            if (rtplntbFindName.Text.Trim() == "")
            { rtplndgvHD.CurrentCell = rtplndgvHD[0, 0]; }
            else
            {
                switch (rtplncboOrderBy.SelectedIndex)
                {
                    case 0:
                        rtplnFindByName("clmID"); break;
                    case 1:
                        rtplnFindByName("clmName"); break;
                    case 2:
                        rtplnFindByName("clmAddress"); break;
                    case 3:
                        rtplnFindByName("clmApt"); break;
                    case 4:
                        rtplnFindByName("clmLastSvc"); break;
                    case 5:
                        rtplnFindByName("clmPhone"); break;
                    case 6:
                        rtplnFindByName("clmSvcItem"); break;
                }
            }
        }

        private void rtplntbRoute_Enter(object sender, EventArgs e)
        {
            sOriRouteText = ((TextBox)sender).Text;
        }

        private void rtplntbRoute_Leave(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text != sOriRouteText)
            {
                clsHDRoutes.SetDataValue(tb.Tag.ToString(), tb.Text);
                rtplnSetDataChanged();
            }
        }

        private void rtplnmtPhone_Enter(object sender, EventArgs e)
        {
            sOriPhoneText = ((MaskedTextBox)sender).ToString();

        }

        private void rtplnmtPhone_Leave(object sender, EventArgs e)
        {
            MaskedTextBox tb = (MaskedTextBox)sender;
            if (tb.Text != sOriPhoneText)
            {
                clsHDRoutes.SetDataValue(tb.Tag.ToString(), tb.Text);
                rtplnSetDataChanged();
            }
        }

        private void btnSelectDriver_Click(object sender, EventArgs e)
        {
            EditVolunteerForm frmVolunteers = new EditVolunteerForm(CCFBGlobal.connectionString, true);
            frmVolunteers.ShowDialog();
            int newVolId = frmVolunteers.SelectedId;
            if (newVolId > 0)
            {
                loading = true;
                clsHDRoutes.DefaultDriver = newVolId;
                clsHDRoutes.loadDriverInfo(newVolId);
                rtplntbDriver.Text = clsHDRoutes.DriverName;
                rtplnmtDriverPhone.Text = clsHDRoutes.DriverPhone;
                loading = false;
                rtplnbtnSaveRoute.Enabled = true;
            }
        }
        private void btnSelectContact_Click(object sender, EventArgs e)
        {
            EditVolunteerForm frmVolunteers = new EditVolunteerForm(CCFBGlobal.connectionString, true);
            frmVolunteers.ShowDialog();
            int newVolId = frmVolunteers.SelectedId;
            if (newVolId > 0)
            {
                loading = true;
                clsHDRoutes.FBContact = newVolId;
                clsHDRoutes.loadFBContactInfo(newVolId);
                rtplntbFBContact.Text = clsHDRoutes.FBContactName;
                rtplnmtFBContactPhone.Text = clsHDRoutes.FBContactPhone;
                loading = false;
                rtplnbtnSaveRoute.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Opens the FindClientForm so the user can select a household to add to the route.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddClient_Click(object sender, EventArgs e)
        {
            frmFindClient.ShowDialog();
            int hhId = frmFindClient.HHMemID;

            if (hhId != FindClientForm.NULL_MEM_ID)
            {
                Household hh = new Household(CCFBGlobal.connectionString);
                hh.open(hhId);
                hh.ServiceMethod = (int)CCFBGlobal.ServiceMethodCodes.HomeDeliveryActive;
                hh.SetDataValue("HDRoute", clsHDRoutes.ID.ToString());
                hh.update(true);

                rtplnDisplaySelectedRoute();
            }
        }
    }
}
