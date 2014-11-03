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
        TrxLog clsHHServiceTrans = new TrxLog(CCFBGlobal.connectionString);
        DateTime curSvcDisplayDate;

        DistinctLogDays clsApptDays; 
        TrxLog clsAppointments;
        DateTime curApptDisplayDate;

        ListViewColumnSorter lvColSorterLog;
        ListViewColumnSorter lvColSorterAppt;

        int refreshTimeLeft;
        int refreshTimeStart;
        int leftForTextBox;
        //int rowIndex = 0;


        public TrxLogForm(MainForm frmMainIn,Client clsClientIn)
        {
            InitializeComponent();
            frmMain = frmMainIn;
            clsClient = clsClientIn;
            clsHHServiceTrans.ServiceTrxOnly = true;
            clsSvcDays.LoadDateList(0);
            lvColSorterLog = new ListViewColumnSorter();
            lvColSorterLog.Order = SortOrder.None;

            lvColSorterAppt = new ListViewColumnSorter();
            lvColSorterAppt.Order = SortOrder.None;
            PrefsChanged();
            if (clsSvcDays.RowCount > 0 )
            {
                curSvcDisplayDate = DateTime.Parse(clsSvcDays.LastDate());
                RefreshSvcPage();
            }
            if (CCFBPrefs.EnableAppointments == true)
            {
                clsAppointments = new TrxLog(CCFBGlobal.connectionString);
                clsAppointments.AppointmentsOnly = true;
                clsApptDays = new DistinctLogDays(CCFBGlobal.connectionString);
                clsApptDays.LoadDateList(1);
                if (clsApptDays.RowCount > 0)
                {
                    curApptDisplayDate = DateTime.Parse(clsApptDays.LastDate());
                    RefreshApptPage();
                }
            }
            else
            { tabControl1.TabPages.RemoveByKey("tpgAppt"); }

            refreshTimeStart = refreshTimeLeft = CCFBPrefs.ServiceLogRefreshRate;
            //tbTotBabyDL.Visible = false;
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
            }
        }

        private void chkAutoRefresh_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAutoRefreshDL.Checked == true)
            {
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
                date = tbDateDL.Text;
                date = date.Replace("/", "-");
                CCFBGlobal.ExportToExcell(lvDailyLog, "DailyLog_" + date);
            }
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
            if (this.Visible == false)
            {
//                clsClient.clsHHSvcTrans.openForHH(clsClient.clsHH.ID);
            }
        }

        private void fillClientData(DataSet DSet, ListView lv)
        {
            try
            {
                int row = int.Parse(lv.FocusedItem.Tag.ToString());
                tbNameDL.Text = DSet.Tables[0].Rows[row]["Name"].ToString();
                tbAddressDL.Text = DSet.Tables[0].Rows[row]["Address"].ToString()
                    + "\r\n" + DSet.Tables[0].Rows[row]["City"].ToString()
                    + ", " + DSet.Tables[0].Rows[row]["State"].ToString()
                    + DSet.Tables[0].Rows[row]["Zipcode"].ToString();

                tbIDDL.Text = DSet.Tables[0].Rows[row]["HouseholdID"].ToString();
                tbTrxIdDL.Text = DSet.Tables[0].Rows[row]["TrxID"].ToString();
                btnOpenHHDL.Enabled = true;
                btnOpenTrxDL.Enabled = true;
                btnDeleteTrx.Enabled = true;
            }
            catch (Exception ex)
            {
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString());
            }
        }

        private void fillForm(int LogORAppt, TrxLog clsLog, ListView lvw)
        {
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
                    totSuppl += int.Parse(drow["LbsBabySvc"].ToString());
                    totEntries++;
                    
                    lvItm = new ListViewItem();
                    lvItm.Text = (i+1).ToString();
                    lvItm.Name = drow["TrxId"].ToString();
                    foreach (ColumnHeader col in lvw.Columns)
                    {
                        switch (col.Text.ToLower())
                        {
                            case "name": lvItm.SubItems.Add(drow["Name"].ToString()); break; 
                            case "type": lvItm.SubItems.Add(clsLog.StatusNameShort); break; 
                            case "<3":   lvItm.SubItems.Add(drow["Infants"].ToString()); break; 
                            case "yth":  lvItm.SubItems.Add(drow["Youth"].ToString()); break; 
                            case "tn":   lvItm.SubItems.Add(drow["Teens"].ToString()); break;
                            case "18": lvItm.SubItems.Add(drow["Eighteen"].ToString()); break;
                            case "adlt": lvItm.SubItems.Add(drow["Adults"].ToString()); break; 
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
                            case "id": lvItm.SubItems.Add(drow["HouseholdId"].ToString()); break; 
                            case "fir": lvItm.SubItems.Add(drow["FiscalFirstTime"].ToString()); break;
                            case "status": lvItm.SubItems.Add(drow["TrxStatus"].ToString()); break;
                            case "tra": lvItm.SubItems.Add(drow["HomeLess"].ToString()); break; 
                        }
                    }
                    if (Int16.Parse(drow["TrxStatus"].ToString()) == 2)
                        lvItm.BackColor = Color.MistyRose;
                    else
                        lvItm.BackColor = Color.LightYellow;
                    lvItm.Tag = i;
                    lvw.Items.Add(lvItm);
                }
                catch (Exception ex)
                {
                    CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString(),
                        CCFBGlobal.serverName);
                }
                
            }
            if (LogORAppt == 0)
            {
                tbDateDL.Text = curSvcDisplayDate.ToShortDateString();
                lblDayDL.Text = curSvcDisplayDate.DayOfWeek.ToString();
                
                tbTotInfDL.Text = infants.ToString();
                tbTotYthDL.Text = yth.ToString();
                tbTotTeenDL.Text = teens.ToString();
                tbTotEighteenDL.Text = eighteen.ToString();
                tbTotAdltDL.Text = adlt.ToString();
                tbTotSnrsDL.Text = senr.ToString();
                tbTotFamilyDL.Text = totFam.ToString();
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

        //Im just trying to show this works, this will need to be talked about with ken
        //and figgured out how we want to do this
        private void lvDailyLog_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillClientData(clsHHServiceTrans.DSet, lvDailyLog);
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
                clsAppointments.openForADate(curApptDisplayDate);
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
                    ShowEditServiceForm(clsHHServiceTrans.HouseholdID
                                       , clsHHServiceTrans.TrxId
                                       , false);
                    clsHHServiceTrans.openForADate(curSvcDisplayDate);
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
                refreshTimeLeft = refreshTimeStart;
                clsHHServiceTrans.openForADate(curSvcDisplayDate);
                RefreshSvcPage();
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
            clearSvcClientData();
            lvDailyLog.Items.Clear();
            lvDailyLog.ListViewItemSorter = null;
            clsHHServiceTrans.openForADate(curSvcDisplayDate);
            fillForm(0, clsHHServiceTrans, lvDailyLog);
            lvDailyLog.ListViewItemSorter = lvColSorterLog;
        }

        private void RefreshApptPage()
        {
            clearApptClientData();
            lvApptLog.Items.Clear();
            lvApptLog.ListViewItemSorter = null;
            clsAppointments.openForADate(curApptDisplayDate);
            fillForm(1, clsAppointments, lvApptLog);
            lvApptLog.ListViewItemSorter = lvColSorterAppt;
        }

        private void lvApptLog_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == lvColSorterAppt.SortColumn)
            {   // Reverse the current sort direction for this column.
                if (lvColSorterAppt.Order != SortOrder.Descending)
                {
                    lvColSorterAppt.Order = SortOrder.Descending;
                }
                else
                {
                    lvColSorterAppt.Order = SortOrder.Ascending;
                }
            }
            else
            {   // Set the column number that is to be sorted; default to ascending.
                lvColSorterAppt.SortColumn = e.Column;
                lvColSorterAppt.Order = SortOrder.Ascending;
            }
            this.lvApptLog.Sort();     // Perform the sort with these new sort options.
        }

        private void lvDailyLog_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == lvColSorterLog.SortColumn)
            {   // Reverse the current sort direction for this column.
                if (lvColSorterLog.Order != SortOrder.Descending)
                {
                    lvColSorterLog.Order = SortOrder.Descending;
                }
                else
                {
                    lvColSorterLog.Order = SortOrder.Ascending;
                }
            }
            else
            {   // Set the column number that is to be sorted; default to ascending.
                lvColSorterLog.SortColumn = e.Column;
                lvColSorterLog.Order = SortOrder.Ascending;
            }
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
            frmMain.setHousehold(Convert.ToInt32(newHHID),0);
            this.Hide();
        }
        private void ShowEditServiceForm(int HHId,int trxId, bool isAppt)
        {
            clsClient.open(HHId, true, true);
            clsClient.clsHHSvcTrans.find(trxId);
            EditServiceShortForm frmService = new EditServiceShortForm();
            frmService.initForm(clsClient, false, isAppt, trxId,clsClient.clsHHSvcTrans.HHMemID );
            frmService.ShowDialog();
        }

        private void btnDoService_Click(object sender, EventArgs e)
        {
            openAppt(false);
        }

        private void btnRefreshDateListLog_Click(object sender, EventArgs e)
        {
            clsSvcDays.LoadDateList(0);
            clsSvcDays.FindDate(Convert.ToDateTime(tbDateDL.Text)); 
        }

        private void btnRefreshDateListAppt_Click(object sender, EventArgs e)
        {
            clsApptDays.LoadDateList(1);
            clsApptDays.FindDate(Convert.ToDateTime(tbDateAL.Text));
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
            leftForTextBox = tbTotCmDL.Left;
            EnablelvwColumn(CCFBPrefs.EnableTEFAP, lvDailyLog.Columns[11], tbTotCmDL);                      //"dlCommodity"
            EnablelvwColumn(CCFBPrefs.EnableSupplemental, lvDailyLog.Columns[12], tbTotSuplDL);             //"dlSuppl"
            EnablelvwColumn(CCFBPrefs.EnableBabyServices, lvDailyLog.Columns[13], tbTotBabyDL);             //"dlBabySvcLbs"
            if (CCFBPrefs.EnableAppointments == true)
            {
                leftForTextBox = tbTotCmAL.Left;
                EnablelvwColumn(CCFBPrefs.EnableTEFAP, lvApptLog.Columns[12], tbTotCmAL);                   //"alCm"
                EnablelvwColumn(CCFBPrefs.EnableSupplemental, lvApptLog.Columns[13], tbTotSuplAL);          //"alSuppl"
                //EnablelvwColumn(CCFBPrefs.EnableCSFP, lvApptLog.Columns["alCSFP"], tbTotCSFP);
            }
        }

        private void EnablelvwColumn(bool isEnabled, ColumnHeader colHdr, TextBox tb)
        {
            tb.Visible = isEnabled;
            if (isEnabled == false)
                colHdr.Width = 0;
            else
            {
                colHdr.Width = 45;
                tb.Left = leftForTextBox;
                leftForTextBox += colHdr.Width;
            }
        }

        private void tbIDDL_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == "\r")
            {
            }
        }
    }
}
