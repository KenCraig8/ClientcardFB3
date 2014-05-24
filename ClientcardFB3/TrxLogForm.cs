using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using System.Drawing;

namespace ClientcardFB3
{
    public partial class TrxLogForm : Form
    {
        MainForm frmMain;
        Client clsClient;
        DistinctLogDays clsSvcDays = new DistinctLogDays(CCFBGlobal.connectionString);
        TrxLog clsHHServiceTrans;
        DateTime curSvcDisplayDate;

        DistinctLogDays clsApptDays; 
        TrxLog clsAppointments;
        DateTime curApptDisplayDate;
        

        ListViewItem curItem = null;
        bool bCurrentItemActive = false;
        ListViewColumnSorter lvColSorterLog;
        ListViewColumnSorter lvColSorterAppt;

        int refreshTimeLeft;
        int refreshTimeStart;
        //int leftForTextBox;
        bool bNormalMode = false;
        //int rowIndex = 0;


        public TrxLogForm(MainForm frmMainIn,Client clsClientIn)
        {
            InitializeComponent();
            bNormalMode = false;
            frmMain = frmMainIn;
            clsClient = clsClientIn;
            pnlIncludeOptions.Visible = (CCFBPrefs.EnableFastTrack==true);
            InitFoodServiceClass();
            clsSvcDays.LoadDateList(0);
            lvColSorterLog = new ListViewColumnSorter();
            lvColSorterLog.Order = SortOrder.None;

            lvColSorterAppt = new ListViewColumnSorter();
            lvColSorterAppt.Order = SortOrder.None;
            PrefsChanged();
            if (clsSvcDays.RowCount > 0 )
            {
                curSvcDisplayDate = DateTime.Parse(clsSvcDays.LastDate());
                //RefreshSvcPage();
            }
            
            if (CCFBPrefs.EnableAppointments == true)
            {
                clsAppointments = new TrxLog(CCFBGlobal.connectionString,false,false,true,true);
                clsAppointments.IncludeAppointments = true;
                clsApptDays = new DistinctLogDays(CCFBGlobal.connectionString);
                clsApptDays.LoadDateList(1);
                if (clsApptDays.RowCount > 0)
                {
                    curApptDisplayDate = DateTime.Parse(clsApptDays.LastDate());
                    //RefreshApptPage();
                }
                RefreshApptPage();
            }
            else
            { tabControl1.TabPages.RemoveByKey("tpgAppt"); }

            refreshTimeStart = refreshTimeLeft = CCFBPrefs.ServiceLogRefreshRate;
            bNormalMode = true;
        }

        #region Daily Service Page Events

        private void btnFirstDL_Click(object sender, EventArgs e)
        {
            if (clsSvcDays.FirstDate() == "")
            {
                btnFirstDL.Enabled = false;
            }
            else
            {
                curSvcDisplayDate = DateTime.Parse(clsSvcDays.FirstDate());
                RefreshSvcPage();
            }
        }

        private void btnLastDL_Click(object sender, EventArgs e)
        {
            if (clsSvcDays.LastDate() == "")
            {
                btnLastDL.Enabled = false;
            }
            else
            {
                btnLastDL.Enabled = true;
                curSvcDisplayDate = DateTime.Parse(clsSvcDays.LastDate());
                RefreshSvcPage();
            }
        }

        private void btnNextDL_Click(object sender, EventArgs e)
        {
            if (clsSvcDays.HaveNextDate() == false)
            {
                btnNextDL.Enabled = false;
            }
            else
            {
                curSvcDisplayDate = DateTime.Parse(clsSvcDays.NextDate());
                RefreshSvcPage();
                btnPrevDL.Enabled = true;
            }
        }

        private void btnPrevDL_Click(object sender, EventArgs e)
        {
            if (clsSvcDays.HavePrevDate() == false)
            {
                btnPrevDL.Enabled = false;
            }
            else
            {
                curSvcDisplayDate = DateTime.Parse(clsSvcDays.PrevDate());
                RefreshSvcPage();
                btnNextDL.Enabled = true;
            }
        }

        private void chkAutoRefreshDL_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAutoRefreshDL.Checked == true)
            {
                refreshTimeLeft =1;
                timer.Start();
            }
            else
            {
                timer.Stop();
            }
        }

        #endregion 

        #region Appointments Page Events

        private void btnFirstAL_Click(object sender, EventArgs e)
        {
            if (clsApptDays.FirstDate() == "")
            {
                btnFirstAL.Enabled = false;
            }
            else
            {
                curApptDisplayDate = DateTime.Parse(clsApptDays.FirstDate());
                RefreshApptPage();
            }
        }

        private void btnLastAL_Click(object sender, EventArgs e)
        {
            if (clsApptDays.LastDate() == "")
            {
                btnLastAL.Enabled = false;
            }
            else
            {
                curApptDisplayDate = DateTime.Parse(clsApptDays.LastDate());
                RefreshApptPage();
            }
        }

        private void btnNextAL_Click(object sender, EventArgs e)
        {
            if (clsApptDays.NextDate() == "" )
            {
                btnNextAL.Enabled = false;
            }
            else
            {
                curApptDisplayDate = DateTime.Parse(clsApptDays.NextDate());
                RefreshApptPage();
            }
        }

        private void btnPrevAL_Click(object sender, EventArgs e)
        {
            if (clsApptDays.PrevDate() == "")
            {
                btnPrevAL.Enabled = false; 
            }
            else
            {
                curApptDisplayDate = DateTime.Parse(clsApptDays.PrevDate());
                RefreshApptPage();
            }
        }

        private void chkAutoRefreshAL_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAutoRefreshAL.Checked == true)
            {
                timer.Start();
            }
            else
            {
                timer.Stop();
            }
        }

        #endregion

        private void clearApptClientData()
        {
            tbNameAL.Text = "";
            tbAddressAL.Text = "";
            tbIDAL.Text = "";
            tbTrxIdAL.Text = "";
            btnOpenHHAL.Enabled = false;
            btnOpenTrxAL.Enabled = false;
            btnDoService.Enabled = false;
        }

        private void clearSvcClientData()
        {
            tbNameDL.Text = "";
            tbAddressDL.Text = "";
            tbIDDL.Text = "";
            tbTrxIdDL.Text = "";
            btnOpenHHDL.Enabled = false;
            btnOpenTrxDL.Enabled = false;
            btnDeleteTrx.Enabled = false;
        }

        private void cmsForListViews_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string date = "";

            if (lvApptLog.Visible == true)
            {
                date = tbDateAL.Text;
                date = date.Replace("/", "-");
                CCFBGlobal.ExportToExcell(lvApptLog, "AppointmentLog_" + date);
            }
            else
            {
                if (e.ClickedItem.Name == "tsmiShowSignature")
                {
                    TrxLogSig tls = new TrxLogSig(CCFBGlobal.connectionString);
                    if (tls.LoadImage(Convert.ToInt32(lvDailyLog.SelectedItems[0].Name),0) == true)
                    {
                        picSignature.Image = tls.SigImage;
                        picSignature.Visible = true;
                        cmsForListViews.Visible = false;
                        MessageBox.Show(this, "Close Signature Display");
                        picSignature.Visible = false;
                    }
                    else
                    {
                        MessageBox.Show("No Signature Found");
                    }
                }
                else
                {
                    date = tbDateDL.Text;
                    date = date.Replace("/", "-");
                    CCFBGlobal.ExportToExcell(lvDailyLog, "DailyLog_" + date);
                }
            }
        }

        public DateTime CurrentDisplayDate
        {
            get { return curSvcDisplayDate; }
        }

        private void TrxLogForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Visible = false;
        }

        private void TrxLogForm_Resize(object sender, EventArgs e)
        {
            tabControl1.Width = this.Width;
            tabControl1.Height = this.Height;
        }

        private void TrxLogForm_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == true)
            {
                if (lvDailyLog.Items.Count != clsHHServiceTrans.NbrSvcRows(curSvcDisplayDate))
                {
                    if (bCurrentItemActive == true)
                    {
                        if (MessageBox.Show("Do YOU want to refresh the list?", "Service data has changed"
                            , MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                        {
                            RefreshSvcPage();
                        }
                    }
                    else
                    {
                        RefreshSvcPage();
                    }
                }
            }
        }

        public void refreshList(string refreshDate)
        {
            try
            {
                curSvcDisplayDate = Convert.ToDateTime(refreshDate);
                RefreshSvcPage();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid Default Service Date");
            }
        }

        private void fillClientData(DataSet DSet, ListView lv)
        {
            try
            {
                if (curItem != null)
                {
                    curItem.BackColor = lv.BackColor;
                    curItem.ForeColor = Color.Black;
                }
                curItem = lv.FocusedItem;
                if (curItem != null)
                {
                    curItem.BackColor = Color.CornflowerBlue;
                    curItem.ForeColor = Color.White;
                    bCurrentItemActive = false;

                    int row = int.Parse(curItem.Tag.ToString());
                    DataRow dr = DSet.Tables[0].Rows[row];
                    tbNameDL.Text = dr["Name"].ToString();
                    tbAddressDL.Text = dr["Address"].ToString()
                        + "\r\n" + dr["City"].ToString()
                        + ", " + dr["State"].ToString()
                        + dr["Zipcode"].ToString();

                    tbIDDL.Text = dr["HouseholdID"].ToString();
                    tbTrxIdDL.Text = dr["TrxID"].ToString();
                    btnOpenHHDL.Enabled = true;
                    btnOpenTrxDL.Enabled = true;
                    btnDeleteTrx.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString());
            }
        }

        private void fillForm(int LogORAppt, TrxLog clsLog, ListView lvw)
        {
            bool bHaveNonFoodList = false;
            bool bHaveNotes = false;
            int totEntries = 0; 
            int totStdLbs = 0;
            int totOtherLbs = 0;
            int totTEFAP = 0;
            int totSuppl = 0;
            int totBaby = 0;

            int infants = 0;
            int yth = 0;
            int teens = 0;
            int eighteen = 0;
            int adlt = 0;
            int senr = 0;
            int totFam = 0;

            DataSet dset = clsLog.DSet;
            DataRow drow;
            ListViewItem lvItm;
            for (int i = 0; i < dset.Tables[0].Rows.Count; i++)
            {
                try
                {
                    drow = dset.Tables[0].Rows[i];
                    if (drow["NonFoodSvcList"].ToString().Trim().Length > 0)
                    { bHaveNonFoodList = true; }
                    if (drow["Notes"].ToString().Trim().Length > 0)
                    { bHaveNotes = true; }

                    infants += int.Parse(drow["Infants"].ToString());
                    yth += int.Parse(drow["Youth"].ToString());
                    teens += int.Parse(drow["Teens"].ToString());
                    eighteen += int.Parse(drow["Eighteen"].ToString());
                    adlt += int.Parse(drow["Adults"].ToString());
                    senr += int.Parse(drow["Seniors"].ToString());
                    totFam += int.Parse(drow["TotalFamily"].ToString());
                    totStdLbs += int.Parse(drow["LbsStd"].ToString());
                    totOtherLbs += int.Parse(drow["LbsOther"].ToString());
                    totTEFAP += int.Parse(drow["LbsCommodity"].ToString());
                    totSuppl += int.Parse(drow["LbsSupplemental"].ToString());
                    totBaby += int.Parse(drow["LbsBabySvc"].ToString());
                    totEntries++;
                    
                    lvItm = new ListViewItem();
                    lvItm.Text = (i+1).ToString();
                    lvItm.Name = drow["TrxId"].ToString();
                    foreach (ColumnHeader col in lvw.Columns)
                    {
                        switch (col.Text.ToLower())
                        {
                            case "name": lvItm.SubItems.Add(drow["Name"].ToString()); break;
                            case "address": lvItm.SubItems.Add(drow["Address"].ToString()); break;
                            case "apt#": lvItm.SubItems.Add(drow["AptNbr"].ToString()); break; 
                            case "type": lvItm.SubItems.Add(clsLog.StatusNameShort); break; 
                            case "<3":   lvItm.SubItems.Add(drow["Infants"].ToString()); break; 
                            case "3-12": lvItm.SubItems.Add(drow["Youth"].ToString()); break;
                            case "3-17": 
                                lvItm.SubItems.Add((Convert.ToInt32(drow["Youth"]) + Convert.ToInt32(drow["Teens"])).ToString()); 
                                break;
                            case "tns":  lvItm.SubItems.Add(drow["Teens"].ToString()); break;
                            case "18":   lvItm.SubItems.Add(drow["Eighteen"].ToString()); break;
                            case "adlt": lvItm.SubItems.Add(drow["Adults"].ToString()); break;
                            case "tot":  lvItm.SubItems.Add(drow["TotalFamily"].ToString()); break;
                            case "":     lvItm.SubItems.Add(""); 
                                         lvItm.SubItems[lvItm.SubItems.Count - 1].BackColor = Color.Gray; break;   
                            case "sen":  lvItm.SubItems.Add(drow["Seniors"].ToString()); break; 
                            case "lbs":  lvItm.SubItems.Add(drow["LbsStd"].ToString()); break; 
                            case "oth":  lvItm.SubItems.Add(drow["LbsOther"].ToString()); break; 
                            case "cm":   lvItm.SubItems.Add(drow["LbsCommodity"].ToString()); break; 
                            case "supl": lvItm.SubItems.Add(drow["LbsSupplemental"].ToString()); break;
                            case "baby": lvItm.SubItems.Add(drow["LbsBabySvc"].ToString()); break; 
                            case "notes": lvItm.SubItems.Add(drow["Notes"].ToString()); break; 
                            case "food svc list": lvItm.SubItems.Add(drow["FoodSvcList"].ToString()); break; 
                            case "non-food svc lst": lvItm.SubItems.Add(drow["NonFoodSvcList"].ToString()); break;
                            case "baby service list": lvItm.SubItems.Add(drow["BabySvcList"].ToString()); break; 
                            case "id":     lvItm.SubItems.Add(drow["HouseholdId"].ToString()); break;
                            case "fiscal": lvItm.SubItems.Add(txtNew((bool)drow["FiscalFirstTime"])); break;
                            case "status": lvItm.SubItems.Add(drow["TrxStatus"].ToString()); break;
                            case "hmless": lvItm.SubItems.Add(txtYes((bool)drow["HomeLess"])); break;
                            case "first":  lvItm.SubItems.Add(txtYes((bool)drow["FirstTimeEver"])); break;
                        }
                    }
                    switch (Convert.ToInt32(drow["TrxStatus"]))
	                {
                        case CCFBGlobal.statusTrxLog_Service:
                            lvItm.BackColor = Color.LightYellow;
                            break;
                        case CCFBGlobal.statusTrxLog_FastTrack:
                            lvItm.BackColor = Color.LightCyan;
                            break;
                        case CCFBGlobal.statusTrxLog_NewAppt:
                            lvItm.BackColor = Color.MistyRose;
                            break;
                        case CCFBGlobal.statusTrxLog_NoShow:
                            lvItm.BackColor = Color.Tan;
                            break;
		                default:
                            lvItm.BackColor = Color.LightGray;
                            break;
                    }
                    lvItm.Tag = i;
                    if (drow["TrxSigID"] != DBNull.Value)
                    {
                        lvItm.ImageIndex = 0;
                    }
                    lvw.Items.Add(lvItm);
                }
                catch (Exception ex)
                {
                    CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString());
                    
                }
                if (rdoDescending.Checked == true)
                {
                    lvColSorterLog.Order = SortOrder.Descending;
                    this.lvDailyLog.Sort();
                }
            }
            int tmpWidth = 0;
            if (bHaveNonFoodList == true)
            { tmpWidth = 200; }
            lvDailyLog.Columns[20].Width = tmpWidth;
            tmpWidth = 50;
            if (bHaveNotes == true)
            { tmpWidth = 200; }
            lvDailyLog.Columns[18].Width = tmpWidth;

            if (LogORAppt == 0)
            {
                tbDateDL.Text = curSvcDisplayDate.ToShortDateString();
                lblDayDL.Text = curSvcDisplayDate.DayOfWeek.ToString();
                
                tbTotInfDL.Text = infants.ToString();
                if (tbTotTeenDL.Visible == true)
                {
                    tbTotYthDL.Text = yth.ToString();
                    tbTotTeenDL.Text = teens.ToString();
                }
                else
                {
                    tbTotYthDL.Text = (yth + teens).ToString();
                }
                tbTotEighteenDL.Text = eighteen.ToString();
                tbTotAdltDL.Text = adlt.ToString();
                tbTotSnrsDL.Text = senr.ToString();
                //tbTotFamilyDL.Text = totFam.ToString();
                tbTotalFamDL.Text = totFam.ToString();
                tbTotLbsDL.Text = totStdLbs.ToString();
                tbTotOthDL.Text = totOtherLbs.ToString();
                tbTotCmDL.Text = totTEFAP.ToString();
                tbTotSuplDL.Text = totSuppl.ToString();
                tbTotBabyDL.Text = totBaby.ToString();
                tbTotalEntriesDL.Text = totEntries.ToString();
                tbTotSrvcLbsDL.Text = (totStdLbs + totOtherLbs + totTEFAP + totSuppl).ToString();
            }
            else
            {
                tbDateAL.Text = curApptDisplayDate.ToShortDateString();
                lblDayAL.Text = curApptDisplayDate.DayOfWeek.ToString();

                tbTotInfAL.Text = infants.ToString();
                tbTotYthAL.Text = yth.ToString();
                tbTotTeenAL.Text = teens.ToString();
                tbTotEighteenAL.Text = teens.ToString();
                tbTotEighteenAL.Text = adlt.ToString();
                tbTotSenAL.Text = senr.ToString();
                tbTotFamilyAL.Text = totFam.ToString();
                tbTotalFamAL.Text = totFam.ToString();
                tbTotLbsAL.Text = totStdLbs.ToString();
                tbTotOtherAL.Text = totOtherLbs.ToString();
                tbTotCmAL.Text = totTEFAP.ToString();
                tbTotSuplAL.Text = totSuppl.ToString();
                tbTotalEntriesAL.Text = totEntries.ToString();
                tbTotSrvcLbsAL.Text = (totStdLbs + totOtherLbs + totTEFAP + totSuppl).ToString();
            }
        }

        //private void lvDailyLog_ColumnClick(object sender, ColumnClickEventArgs e)
        //{
        //    switch (e.Column)
        //    {
        //        case 1: { lvDailyLog.ListViewItemSorter }
        //        case 2: { }
        //    }
        //}

        private void lvApptLog_DoubleClick(object sender, EventArgs e)
        {
            openAppt(true);
        }

        private void lvApptLog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                openAppt(true);
            }
        }

        private void lvApptLog_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillApptData();
        }

        private void fillApptData()
        {
            try
            {
                int row = int.Parse(lvApptLog.FocusedItem.Tag.ToString());
                tbNameAL.Text = clsAppointments.DSet.Tables[0].Rows[row]["Name"].ToString();
                tbAddressAL.Text = clsAppointments.DSet.Tables[0].Rows[row]["Address"].ToString()
                    + "\r\n" + clsAppointments.DSet.Tables[0].Rows[row]["City"].ToString()
                    + ", " + clsAppointments.DSet.Tables[0].Rows[row]["State"].ToString()
                    + clsAppointments.DSet.Tables[0].Rows[row]["Zipcode"].ToString();

                tbIDAL.Text = clsAppointments.DSet.Tables[0].Rows[row]["HouseholdID"].ToString();
                tbTrxIdAL.Text = clsAppointments.DSet.Tables[0].Rows[row]["TrxID"].ToString();
                btnOpenHHAL.Enabled = true;
                btnOpenTrxAL.Enabled = true;
                btnDoService.Enabled = true;
            }
            catch (Exception ex)
            {
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString());
            }
        }

        private void lvDailyLog_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillClientData(clsHHServiceTrans.DSet, lvDailyLog);
            if (lvDailyLog.SelectedItems.Count > 0)
            {
                cmsForListViews.Items["tsmiShowSignature"].Visible = (lvDailyLog.SelectedItems[0].ImageIndex == 0);
            }
            else
            {
                cmsForListViews.Items["tsmiShowSignature"].Visible = false;
            }
        }

        private void openAppt(bool isAppt)
        {
            if (lvApptLog.FocusedItem != null)
            {
            try
            {
                int activeRow = int.Parse(lvApptLog.FocusedItem.Tag.ToString());
                ShowEditServiceForm( clsAppointments.DSet.Tables[0].Rows[activeRow].Field<int>("HouseholdID")
                                   , clsAppointments.DSet.Tables[0].Rows[activeRow].Field<int>("TrxID")
                                   , isAppt );
                clsAppointments.openForADate(curApptDisplayDate,ApptAscDesc());
                if (isAppt == false)
                {
                    clsSvcDays.LoadDateList(0);
                    curSvcDisplayDate = DateTime.Parse(clsSvcDays.LastDate());
                    RefreshSvcPage();
                }
                RefreshApptPage();
            }
                catch (NullReferenceException ex)
                {
                    CCFBGlobal.appendErrorToErrorReport("lvApptLog", ex.GetBaseException().ToString());
                }
            }
        }

        private void openTrx()
        {
            try
            {
                int activeRow = int.Parse(lvDailyLog.FocusedItem.Tag.ToString());
                clsHHServiceTrans.setDataRow(activeRow);
                if ( CCFBPrefs.TestAllowEditTrxDate(Convert.ToDateTime(clsHHServiceTrans.TrxDate),true ) == true)
                {
                    bCurrentItemActive = true;
                    ShowEditServiceForm(clsHHServiceTrans.HouseholdID
                                       , clsHHServiceTrans.TrxId
                                       , false);
                    clsHHServiceTrans.openForADate(curSvcDisplayDate, ServiceAscDesc());
                    RefreshSvcPage();
                }
            }
            catch (NullReferenceException ex)
            {
                CCFBGlobal.appendErrorToErrorReport("lvDailyLog", ex.GetBaseException().ToString());
            }
        }

        private void tbDateDL_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    DateTime date = DateTime.Parse(tbDateDL.Text);
                    curSvcDisplayDate = date;
                    RefreshSvcPage();
                }
                catch (InvalidCastException ex) 
                { MessageBox.Show("The Date Is Not In Proper Format. Please Re-Enter and Try Again"); }
                catch (FormatException ex) 
                { MessageBox.Show("The Date Is Not In Proper Format. Please Re-Enter and Try Again"); }
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            refreshTimeLeft--;

            if (refreshTimeLeft <= 0)
            {
                timer.Stop(); 
                RefreshSvcPage();
                refreshTimeLeft = refreshTimeStart;
                timer.Start();
            }
        }

        private void cmsForListViews_Opening(object sender, CancelEventArgs e)
        {

        }

        private void tabControl1_ClientSizeChanged(object sender, EventArgs e)
        {
            //gbDailyLog.Width = tabControl1.TabPages[0].Width-10;
            //gbDailyLog.Height = tabControl1.TabPages[0].Height - gbDailyLog.Top-5;
            //lvDailyLog.Width = gbDailyLog.Width - lvDailyLog.Left * 2;
            //lvDailyLog.Height = gbDailyLog.Height - lvDailyLog.Top - 30;

            //gbAppoint.Width = tabControl1.TabPages[1].Width - 10;
            //gbAppoint.Height = tabControl1.TabPages[1].Height - gbAppoint.Top - 5;
            //lvApptLog.Width = gbAppoint.Width - lvApptLog.Left * 2;
            //lvApptLog.Height = gbAppoint.Height - lvApptLog.Top - 30;
        }

        private void RefreshSvcPage()
        {
            bCurrentItemActive = false;
            clearSvcClientData();
            lvDailyLog.Items.Clear();
            lvDailyLog.ListViewItemSorter = null;
            clsHHServiceTrans.openForADate(curSvcDisplayDate, ServiceAscDesc());
            fillForm(0, clsHHServiceTrans, lvDailyLog);
            bool hasRows = lvDailyLog.Items.Count > 0;
            btnOpenHHDL.Enabled = hasRows;
            btnOpenTrxDL.Enabled = hasRows;
            btnDeleteTrx.Enabled = hasRows;
            lvDailyLog.ListViewItemSorter = lvColSorterLog;
        }

        private void RefreshApptPage()
        {
            clearApptClientData();
            lvApptLog.Items.Clear();
            lvApptLog.ListViewItemSorter = null;
            clsAppointments.openForADate(curApptDisplayDate, ApptAscDesc());
            fillForm(1, clsAppointments, lvApptLog);
            bool hasRows = lvApptLog.Items.Count > 0;
            btnOpenHHAL.Enabled = hasRows;
            btnOpenTrxAL.Enabled = hasRows;
            btnDoService.Enabled = hasRows;
            lvApptLog.ListViewItemSorter = lvColSorterAppt;
        }

        private void lvApptLog_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            lvColSorterAppt.SortColumn = e.Column;
            if (rdoAscending.Checked == true)
            {
               lvColSorterAppt.Order = SortOrder.Ascending;
            }
            else
            {
                lvColSorterAppt.Order = SortOrder.Descending;
            }
            //////if (e.Column == lvColSorterAppt.SortColumn)
            //////{   // Reverse the current sort direction for this column.
            //////    if (lvColSorterAppt.Order != SortOrder.Descending)
            //////    {
            //////        lvColSorterAppt.Order = SortOrder.Descending;
            //////    }
            //////    else
            //////    {
            //////        lvColSorterAppt.Order = SortOrder.Ascending;
            //////    }
            //////}
            //////else
            //////{   // Set the column number that is to be sorted; default to ascending.
            //////    lvColSorterAppt.SortColumn = e.Column;
            //////    lvColSorterAppt.Order = SortOrder.Ascending;
            //////}
            this.lvApptLog.Sort();     // Perform the sort with these new sort options.
        }

        private void lvDailyLog_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            lvColSorterLog.SortColumn = e.Column;
            if (rdoAscending.Checked == true)
            {
                lvColSorterLog.Order = SortOrder.Ascending;
            }
            else
            {
                lvColSorterLog.Order = SortOrder.Descending;
            }
            //////if (e.Column == lvColSorterLog.SortColumn)
            //////{   // Reverse the current sort direction for this column.
            //////    if (lvColSorterLog.Order != SortOrder.Descending)
            //////    {
            //////        lvColSorterLog.Order = SortOrder.Descending;
            //////    }
            //////    else
            //////    {
            //////        lvColSorterLog.Order = SortOrder.Ascending;
            //////    }
            //////}
            //////else
            //////{   // Set the column number that is to be sorted; default to ascending.
            //////    lvColSorterLog.SortColumn = e.Column;
            //////    lvColSorterLog.Order = SortOrder.Ascending;
            //////}
            this.lvDailyLog.Sort();     // Perform the sort with these new sort options.
        }

        private void btnOpenTrxAL_Click(object sender, EventArgs e)
        {
            openAppt(true);
        }

        private void btnOpenTrxDL_Click(object sender, EventArgs e)
        {
            openTrx();
        }

        private void btnOpenHHDL_Click(object sender, EventArgs e)
        {
            openMainFormHousehold(tbIDDL.Text);
        }

        private void btnOpenHHAL_Click(object sender, EventArgs e)
        {
            openMainFormHousehold(tbIDAL.Text);
        }

        private void openMainFormHousehold(string newHHID)
        {
            bCurrentItemActive = true;
            frmMain.setHousehold(Convert.ToInt32(newHHID),0);
            this.Hide();
        }
        private void ShowEditServiceForm(int HHId,int trxId, bool isAppt)
        {
            clsClient.open(HHId, true, true);
            clsClient.clsHHSvcTrans.openWhere("");
            clsClient.clsHHSvcTrans.find(trxId);
            EditServiceShortForm frmService = new EditServiceShortForm();
            frmService.initForm(clsClient, false, isAppt, trxId,clsClient.clsHHSvcTrans.HHMemID, "");
            frmService.ShowDialog();
        }

        private void btnDoService_Click(object sender, EventArgs e)
        {
            openAppt(false);
        }

        private void btnRefreshDateListLog_Click(object sender, EventArgs e)
        {
            try
            {
                clsSvcDays.LoadDateList(0);
                clsSvcDays.FindDate(Convert.ToDateTime(tbDateDL.Text));
                btnPrevDL.Enabled = true;
                btnNextDL.Enabled = true;
            }
            catch (Exception ex)
            {

            }
        }

        private void btnRefreshDateListAppt_Click(object sender, EventArgs e)
        {
            try
            {
                clsApptDays.LoadDateList(1);
                clsApptDays.FindDate(Convert.ToDateTime(tbDateAL.Text));
            }
            catch (Exception ex) { }
        }

        private void btnDeleteTrx_Click(object sender, EventArgs e)
        {
            if (clsHHServiceTrans.PromptDelete(tbTrxIdDL.Text))
            {
                RefreshSvcPage();
            }
        }

        public void PrefsChanged()
        {
            if (CCFBPrefs.MergeTeens == true)
            {
                lvDailyLog.Columns[6].Text = "3-17";
                lvDailyLog.Columns[7].Text = "";
            }
            else
            {
                lvDailyLog.Columns[6].Text = "3-12";
                lvDailyLog.Columns[7].Text = "Tns";
            }
            EnablelvwColumn(!CCFBPrefs.MergeTeens, lvDailyLog.Columns[7], tbTotTeenDL, 40);
            EnablelvwColumn(CCFBPrefs.EnableTEFAP, lvDailyLog.Columns[15], tbTotCmDL,45);                      //"dlCommodity"
            EnablelvwColumn(CCFBPrefs.EnableSupplemental, lvDailyLog.Columns[16], tbTotSuplDL,45);             //"dlSuppl"
            EnablelvwColumn(CCFBPrefs.EnableBabyServices, lvDailyLog.Columns[17], tbTotBabyDL,45);             //"dlBabySvcLbs"
            EnablelvwColumn(CCFBPrefs.EnableBabyServices, lvDailyLog.Columns[21], null, 200);                  //"dlBabySvcList"
            if (CCFBPrefs.EnableAppointments == true)
            {
                EnablelvwColumn(CCFBPrefs.EnableTEFAP, lvApptLog.Columns[2], tbTotCmAL, 45);                   //"alCm"
                EnablelvwColumn(CCFBPrefs.EnableSupplemental, lvApptLog.Columns[13], tbTotSuplAL,45);          //"alSuppl"
                //EnablelvwColumn(CCFBPrefs.EnableCSFP, lvApptLog.Columns["alCSFP"], tbTotCSFP);
            }
            moveDataTotalsFlds();
        }

        private void EnablelvwColumn(bool isEnabled, ColumnHeader colHdr, TextBox tb, int newwidth)
        {
            if (isEnabled == false)
            {
                newwidth = 0;
            }
            colHdr.Width = newwidth;
            if (tb != null)
            {
                tb.Visible = isEnabled;
                tb.Width = newwidth;
            }
        }

        private void tbIDDL_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == "\r")
            {
            }
        }

        private void InitFoodServiceClass()
        {
            clsHHServiceTrans = new TrxLog(CCFBGlobal.connectionString
                                , (rdoIncludeBoth.Checked || rdoIncludePosted.Checked)
                                , (rdoIncludeBoth.Checked || rdoIncludeFT.Checked)
                                , false, false);
        }

        private void rdoIncludeOption_CheckedChanged(object sender, EventArgs e)
        {
            InitFoodServiceClass();
            RefreshSvcPage();
        }

        private string ServiceAscDesc()
        {
            if (rdoAscending.Checked == true)
                return "ASC";
            else
                return "DESC";
        }

        private string ApptAscDesc()
        {
            return "ASC";
            /*
            if (rdoAscending.Checked == true)
                return "ASC";
            else
                return "DESC";
             * */
        }

        private void rdoAscending_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoAscending.Checked == true)
            {
                lvColSorterLog.Order = SortOrder.Ascending;
                lvDailyLog.Sort();
            }
        }

        private void rdoDescending_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoDescending.Checked == true)
            {
                lvColSorterLog.Order = SortOrder.Descending;
                lvDailyLog.Sort();
            }
        }

        private void lvDailyLog_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
        {
            if (bNormalMode == true)
            { moveDataTotalsFlds(); }
        }

        private void moveDataTotalsFlds()
        {
            int xpos = lvDailyLog.Columns[0].Width + lvDailyLog.Columns[1].Width
                     + lvDailyLog.Columns[2].Width + lvDailyLog.Columns[3].Width + lvDailyLog.Columns[4].Width;
            int ypos = tbTotInfDL.Location.Y;
            label1.Location = new Point(xpos-1, label1.Location.Y);

            tbTotInfDL.Location = new Point(xpos, ypos);
            xpos += lvDailyLog.Columns[5].Width;
            tbTotYthDL.Location = new Point(xpos, ypos);
            xpos += lvDailyLog.Columns[6].Width; ;
            tbTotTeenDL.Location = new Point(xpos, ypos);
            xpos += lvDailyLog.Columns[7].Width; ;
            tbTotEighteenDL.Location = new Point(xpos, ypos);
            xpos += lvDailyLog.Columns[8].Width; ;
            tbTotAdltDL.Location = new Point(xpos, ypos);
            xpos += lvDailyLog.Columns[9].Width; ;
            tbTotSnrsDL.Location = new Point(xpos, ypos);
            xpos += lvDailyLog.Columns[10].Width; ;
            tbTotalFamDL.Location = new Point(xpos, ypos);
            //tbTotFamilyDL.Location = new Point(xpos - 7, tbTotFamilyDL.Location.Y);
            //label1.Location = new Point(xpos - 11 - label1.Size.Width, label1.Location.Y);
            xpos += lvDailyLog.Columns[11].Width; 
            label1.Width = xpos - label1.Location.X;

            xpos += lvDailyLog.Columns[12].Width;
            label3.Location = new Point(xpos-1, label3.Location.Y);
            //xpos += 2;
            tbTotLbsDL.Location = new Point(xpos, ypos);
            xpos += lvDailyLog.Columns[13].Width;
            tbTotOthDL.Location = new Point(xpos, ypos);
            xpos += lvDailyLog.Columns[14].Width;
            tbTotCmDL.Location = new Point(xpos, ypos);
            xpos += lvDailyLog.Columns[15].Width;
            tbTotSuplDL.Location = new Point(xpos, ypos);
            xpos += lvDailyLog.Columns[16].Width;
            tbTotBabyDL.Location = new Point(xpos, ypos);
            xpos += lvDailyLog.Columns[17].Width;
            tbTotSrvcLbsDL.Location = new Point(xpos , tbTotSrvcLbsDL.Location.Y);
            //label3.Location = new Point(xpos - 4 - label3.Size.Width, label3.Location.Y);
            label3.Width = xpos + 1 - label3.Location.X;
        }

        private void TrxLogForm_Load(object sender, EventArgs e)
        {
            RefreshSvcPage();
            moveDataTotalsFlds();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                lvDailyLog.Columns[3].Width = 180;
                lvDailyLog.Columns[4].Width = 60;
            }
            else
            {
                lvDailyLog.Columns[3].Width = 0;
                lvDailyLog.Columns[4].Width = 0;
            }
        }

        private string txtYes(bool testVal)
        {
            if (testVal == true)
                return "YES";
            return "--";
        }

        private string txtNew(bool testVal)
        {
            if (testVal == true)
                return "NEW";
            return "--";
        }
    }
}
