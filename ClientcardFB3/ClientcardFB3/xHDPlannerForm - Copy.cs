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
    public partial class HDPlannerForm : Form
    {
        DataSet dset;
        SqlDataAdapter dadAdpt;
        SqlCommand command;
        SqlConnection conn;
        
        TrxLog clsTrxLog;
        TrxLogItem clsTrxItem;
        HDRoutes clsHdRoutes = new HDRoutes(CCFBGlobal.connectionString);
        MainForm frmMain;
        string sqlCommandText = "Select h.ID, h.Name, h.Address, h.AptNbr, h.City, h.Zipcode, h.LatestService, h.HDRoute, h.Phone"
            + ",RTRIM(case WHEN Infants = 0 THEN '' ELSE CAST(Infants as Varchar(3)) + 'I ' END"
            + " + case WHEN Youth+Teens+Eighteen = 0 THEN '' ELSE CAST(Youth+Teens+Eighteen as Varchar(3)) + 'C 'END"
            + " + case WHEN Adults = 0 THEN '' ELSE CAST(Adults as Varchar(3)) + 'A ' END"
            + " + case WHEN Seniors = 0 THEN '' ELSE CAST(Seniors as Varchar(3)) + 'S' END) FamilySize"
            + ",r.RouteTitle, h.Inactive, h.Comments, h.HDItem, h.DriverNotes"
            + "  FROM Household h "
            + " INNER JOIN HDRoutes r ON h.HDRoute = r.ID"
            + " WHERE h.ServiceMethod = 2 ";
        string andStatement = "";
        int rowCount = 0;
        string lastSearchText = "";
        int rowIndex = 0;
        bool loading = false;
        string orderBy = " ORDER BY h.Name";
        DateTime svcDate = DateTime.Today;
        string savePath = CCFBPrefs.ReportsSavePath;
        string sOriRouteText = "";

        public HDPlannerForm(MainForm frmMainIn)
        {
            InitializeComponent();
            frmMain = frmMainIn;
            dtpSvcDate.Value = DateTime.Today;
            dadAdpt = new SqlDataAdapter();
            dset = new DataSet();
            conn = new SqlConnection(CCFBGlobal.connectionString);
            clsHdRoutes.openWhere(" ORDER BY RouteTitle");
            if (clsHdRoutes.RowCount > 0 )
            {
                cboHDRoute.DataSource = clsHdRoutes.DTable;
                cboHDRoute.DisplayMember = "RouteTitle";
                cboHDRoute.ValueMember = "ID";
            }
            else
            {
                cboHDRoute.DataSource = null;
                cboHDRoute.Items.Add("No Selection");
            }
            CCFBGlobal.dtPopulateCombo((DataGridViewComboBoxColumn)dgvHD.Columns["clmSvcItem"], "SELECT * From HDItems", "ShortName", "ID", "Std", conn);
            //CCFBGlobal.InitCombo(cboHDRoute, CCFBGlobal.parmTbl_HomeDeliveryRoutes);
            andStatement = " And h.HDRoute = " + cboHDRoute.SelectedValue + " ";
            cboOrderBy.SelectedIndex = 0;
            loadList();
        }

        private void HomeDeliveryForm_Load(object sender, EventArgs e)
        {
            pnlGiveService.BackColor = CCFBGlobal.bkColorAltEdit;
        }

        private void setFindClientVisible()
        {
            if (dgvHD.Rows.Count > 0)
                changeGroupBoxVisibility(0);
            else
                pnlFindClient.Visible = false;
        }

        /// <summary>
        /// Loads the DataGrid using values obtained in the DataSet
        /// </summary>
        private void loadList()
        {
            tbFindName.Text = "";
            lastSearchText = "";
            dgvHD.Rows.Clear();
            progressBar1.Value = 0;
            progressBar1.Show();
            tbFindName.Visible = false;
            Application.DoEvents();
            progressBar1.Maximum = rowCount;
            for (int i = 0; i < rowCount; i++)
            {
                dgvHD.Rows.Add();
                dgvHD["clmRouteID", i].Value = true;
                dgvHD["clmRouteID", i].Value = dset.Tables[0].Rows[i]["HDRoute"];
                dgvHD["clmRouteTitle", i].Value = dset.Tables[0].Rows[i]["RouteTitle"];
                dgvHD["clmID", i].Value = dset.Tables[0].Rows[i]["ID"];
                dgvHD["clmName", i].Value = dset.Tables[0].Rows[i]["Name"];
                dgvHD["clmAddress", i].Value = dset.Tables[0].Rows[i]["Address"].ToString() + "\r\n     " + dset.Tables[0].Rows[i]["City"].ToString() + ", " + dset.Tables[0].Rows[i]["ZipCode"].ToString();
                dgvHD["clmApt", i].Value = dset.Tables[0].Rows[i]["AptNbr"];
                dgvHD["clmPhone", i].Value = CCFBGlobal.FormatPhone(dset.Tables[0].Rows[i]["Phone"].ToString());
                dgvHD["clmFamilySize", i].Value = dset.Tables[0].Rows[i]["FamilySize"];
                dgvHD["clmComments", i].Value = dset.Tables[0].Rows[i]["Comments"];
                dgvHD["clmDriverNotes", i].Value = dset.Tables[0].Rows[i]["DriverNotes"];
                dgvHD["clmSvcItem", i].Value = dset.Tables[0].Rows[i]["HDItem"];
                dgvHD["clmLastSvc", i].Value = CCFBGlobal.ValidDateString(dset.Tables[0].Rows[i]["LatestService"]);
                progressBar1.PerformStep();
            }
            lblRowCnt.Text = "[ " + dgvHD.Rows.Count.ToString() + " ]";
            rowIndex = 0;
            foreach (ToolStripButton tsb in toolStrip2.Items)
            {
                dgvHD.Columns[tsb.Tag.ToString()].Visible = tsb.Checked;
            }
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
            for (int i = rowStart; i < dgvHD.Rows.Count; i++)
            {
                if (dgvHD.Rows[i].Cells[colNameFull].FormattedValue.ToString().ToUpper().StartsWith(lastSearchText) == true)
                {
                    dgvHD.CurrentCell = dgvHD[1, i];
                    if (i < dgvHD.FirstDisplayedScrollingRowIndex
                        || i > dgvHD.Rows.GetLastRow(DataGridViewElementStates.Displayed) - 5)
                        if (i > 5)
                            dgvHD.FirstDisplayedScrollingRowIndex = i - 5;
                        else
                            dgvHD.FirstDisplayedScrollingRowIndex = i;
                    break;
                }
            }
        }

        private void tbFindName_TextChanged(object sender, EventArgs e)
        {
            if (tbFindName.Text.Trim() == "")
            { dgvHD.CurrentCell = dgvHD[0, 0]; }
            else
            {
                switch (cboOrderBy.SelectedIndex)
                {
                    case 0:
                        { FindByName("clmName", "clmAddress"); break; }
                    case 1:
                        { FindByName("clmAddress", "clmName"); break; }
                    case 3:
                        { FindByName("clmHHID", "clmAddress"); break; }
                    case 2:
                        { FindByName("clmMethod", "clmAddress"); break; }
                    case 5:
                        { FindByName("clmExpiration", "clmAddress"); break; }
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
            //if (loading == false && rowCount > 0 && cboFilter.SelectedIndex != 0)
            //{
            //    DataRow[] drows;
            //    if (orderBy == "Route")
            //        drows = dset.Tables[0].Select(orderBy + "='" + cboFilter.SelectedItem.ToString() + "'");
            //    else
            //        drows = dset.Tables[0].Select(orderBy + "=" + cboFilter.SelectedItem.ToString());
               
            //    loadList(drows);
            //}
            //else if (cboFilter.SelectedIndex == 0)
            //{
            //    loadList();
            //}
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

        private void getHomeDeliveryClients()
        {
            try
            {
                openConnection();
                dset.Clear();
                dadAdpt.SelectCommand = command;
                rowCount = dadAdpt.Fill(dset, "HomeDeliveryClients");
                closeConnection();
            }
            catch (Exception ex)
            {
                CCFBGlobal.appendErrorToErrorReport("SelectCommand = " + command.CommandText,
                    ex.GetBaseException().ToString());
                rowCount = 0;
            }
//            EnableActionMenu();
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
            switch (cboOrderBy.SelectedIndex)
            {
                case 0:
                    {
                        orderBy = " Order By h.Name ";
                        break;
                    }
                case 1:
                    {
                        orderBy = " Order By h.Address ";
                        break;
                    }
                case 2:
                    {
                        orderBy = " Order By h.AptNbr ";
                        break;
                    }
                case 3:
                    {
                        orderBy = " Order By h.City ";
                        break;
                    }
                    case 4:
                    {
                        orderBy = " Order By r.RouteTitle ";
                        break;
                    }
            }
            command = new SqlCommand(sqlCommandText + andStatement + orderBy, conn);
            getHomeDeliveryClients();
            loadList();
            lblFilterBy.Visible = cboFilter.Visible;
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
                        pnlFindClient.Visible = true;
                        pnlGiveService.Visible = false;
                        dgvHD.Enabled = true;
                        break;
                    }
                case 1:
                    {
                        pnlFindClient.Visible = false;
                        pnlGiveService.Visible = false;
                        dgvHD.Enabled = false;

                        string date = CCFBGlobal.NullToBlank(dgvHD.SelectedRows[0].Cells["clmExpiration"].Value);
                        break;
                    }
                case 2:
                    {
                        dgvHD.Enabled = false;
                        pnlFindClient.Visible = false;
                        pnlGiveService.Visible = true;
                        dtpSvcDate.Value = svcDate;
                        break;
                    }
            }
        }
        
        private void btnSvcCancel_Click(object sender, EventArgs e)
        {
            
            changeGroupBoxVisibility(0);
            getHomeDeliveryClients();
        }

        private void giveNewService(string insertIDs)
        {
            
        }

        private void updateExistingService(string updateIDs)
        {
            //try
            //{
            //    updateAndInsertComm = new SqlCommand("Update CSFPLog Set CSFPSvcDate='" + dtpSvcDate.Value.ToString() + "', "
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

        private void btnSvcSave_Click(object sender, EventArgs e)
        {
            changeGroupBoxVisibility(0);
            getHomeDeliveryClients();
        }

        

        private void tbLbsCSFP_KeyDown(object sender, KeyEventArgs e)
        {
            CCFBGlobal.checkForIntOnKeyPress(e);
        }

        private void PrintPicketlist_Click(object sender, EventArgs e)
        {

            string route = cboHDRoute.Text;
            ///object saveAs = "";
            Object saveAs = savePath + "HDRouteSheet" + ".doc";

            CreateHDRouteSheet clsCreatePicklist = new CreateHDRouteSheet(dgvHD);
            clsCreatePicklist.createReport(saveAs, @"C:\ClientcardFB3\Templates\HDRouteSheet.doc", CCFBPrefs.FoodBankName,
                DateTime.Now, route);
        }

        private void EditHDForm_Resize(object sender, EventArgs e)
        {
            if (this.WindowState != FormWindowState.Minimized)
                dgvHD.Top = 140;
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

        private void cboHDRoute_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cboHDRoute.SelectedValue) == 0)
            {
                andStatement = "";
            }
            else
            {
                andStatement = " AND h.HDRoute = " + cboHDRoute.SelectedValue + " ";
            }
            command = new SqlCommand(sqlCommandText + andStatement + orderBy, conn);
            displayRouteInfo(Convert.ToInt32(cboHDRoute.SelectedValue));
            getHomeDeliveryClients();
            loadList();
        }

        private void clearRoutInfo()
        {
            tbDriver.Text = "";
            tbPhone.Text = "";
            tbEstTime.Text = "";
            tbEstMiles.Text = "";
            tbRouteNotes.Text = "";
            tbDriverComments.Text = "";
        }

        private void displayRouteInfo(int idx)
        {
            clearRoutInfo();
            clsHdRoutes.find(idx, true);
            btnSaveRoute.Visible = false;
            if (clsHdRoutes.ID == idx)
            {
                tbDriver.Text = clsHdRoutes.DriverName;
                tbPhone.Text = clsHdRoutes.DriverPhone;
                tbEstTime.Text = clsHdRoutes.EstHours.ToString("0.00");
                tbEstMiles.Text = clsHdRoutes.EstMiles.ToString("0.0");
                tbRouteNotes.Text = clsHdRoutes.Notes;
                tbDriverComments.Text = clsHdRoutes.DriverNotes;
            }
        }

        private void setRouteDataChanged()
        {
            btnSaveRoute.Visible = true;
        }

        private void tsbToggle_CheckedChanged(object sender, EventArgs e)
        {
            string colName  = ((ToolStripButton)sender).Tag.ToString();
            dgvHD.Columns[colName].Visible = !dgvHD.Columns[colName].Visible;
        }

        private void dgvHD_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (frmMain != null)
            {
                if (e.ColumnIndex > 1)
                {
                    int rowIdx = e.RowIndex;
                    int hhID = Convert.ToInt32(dgvHD["clmID", rowIdx].Value);
                    frmMain.setHousehold(hhID, 0);
                }
            }
        }

        private void tbRoute_Enter(object sender, EventArgs e)
        {
            sOriRouteText = ((TextBox)sender).Text;
        }

        private void tbRoute_Leave(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text != sOriRouteText)
            {
                clsHdRoutes.SetDataValue(tb.Tag.ToString(), tb.Text);
                setRouteDataChanged();
            }
        }

        private void btnSaveRoute_Click(object sender, EventArgs e)
        {
            clsHdRoutes.update();
            btnSaveRoute.Visible = false;
        }
    }
}
