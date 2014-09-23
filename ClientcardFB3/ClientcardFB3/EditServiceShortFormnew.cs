using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ClientcardFB3
{
    public partial class EditServiceShortForm : Form
    {
        Client clsClient;
        TrxLog clsTrxLog;
        TrxLogItem clsTrxItem;
        DaysOpen clsDaysOpen = new DaysOpen(CCFBGlobal.connectionString);
        parm_ClientType clsParmClientType = new parm_ClientType(CCFBGlobal.connectionString);

        List<TextBox> tbList = new List<TextBox>();
        List<TextBox> tbLbsList = new List<TextBox>();
        List<TextBox> tbFamData = new List<TextBox>();
        List<CheckBox> chkList = new List<CheckBox>();

        bool bcanceled = false;
        bool isNewTrx = false;
        bool isAppt = false;
        bool bProcessItemChecked = false;
        bool loadingControls = true;
        bool allowEntryOverride = false;
        bool haveSigPad = false;
        string trxDateIn = "";

        public EditServiceShortForm()
        {
            InitializeComponent();
            clsParmClientType.openAll();
            //tbMeals.Visible = (CCFBPrefs.NbrMealsPerService > 0);
            //lblMeals.Visible = (CCFBPrefs.NbrMealsPerService > 0);

            tbeLbsComm.Visible = CCFBPrefs.EnableTEFAP;
            lblCommodity.Visible = CCFBPrefs.EnableTEFAP;

            tbeLbsSupp.Visible = CCFBPrefs.EnableSupplemental;
            lblSuppl.Visible = CCFBPrefs.EnableSupplemental;

            //Load cboClientType
            CCFBGlobal.InitCombo(cboClientType, CCFBGlobal.parmTbl_Client);

            traverseAndAddControlsToCollections(this.Controls);
            lblCreated.Visible = false;
            lblModified.Visible = false;
            chkShowUserInfo.Checked = false;
            chkShowUserInfo.Visible = false;
            haveSigPad = sigPadInputCtrl1.initSigPad();
            if (haveSigPad == true)
            {
                sigPadInputCtrl1.ClearPromptList();
                SqlConnection conn = new SqlConnection(CCFBGlobal.connectionString);
                conn.Open();
                SqlCommand sqlCmd = new SqlCommand("SELECT PromptText, RightButtonText FROM SignaturePrompts WHERE PromptGroup = 1 ORDER BY UID", conn);
                sqlCmd.CommandType = CommandType.Text;
                SqlDataReader reader = sqlCmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        sigPadInputCtrl1.AddPromptItem(reader.GetString(0), reader.GetString(1));
                    }
                }
                conn.Close();
            }
        }

        public bool TrxCanceled
        {
            get {return bcanceled; }
        }

        public DateTime ServiceDate
        {
            get { return clsTrxItem.TrxDate; }
        }

        public void initForm(Client clsClientIn, bool ModeIn, bool isApptIn, int TrxIndexIn, string hhMemName, string txtAlert)
        {
            clsClient = clsClientIn;
            clsTrxLog = clsClient.clsHHSvcTrans;
            isNewTrx = ModeIn;
            isAppt = isApptIn;
            loadingControls = true;
            tbAlert.Rtf = txtAlert;
            tbCSFPLbsPerService.Text = CCFBPrefs.CSFPLbsPerService.ToString();
            haveSigPad = sigPadInputCtrl1.initSigPad();
            sigPadInputCtrl1.Visible = false;
            SetDisplayMode();
            cboClientType.SelectedValue = clsClient.clsHH.ClientType.ToString();
            List<parmType> ptList = new List<parmType>();
            int tmpPtr = 0;
            for (int i = 0; i < clsClient.clsHHmem.RowCount; i++)
			{
                clsClient.clsHHmem.SetRecord(i);
                ptList.Add(new parmType(clsClient.clsHHmem.ID, clsClient.clsHHmem.LastName.Trim() + ", " + clsClient.clsHHmem.FirstName.Trim(), i, clsClient.clsHHmem.FirstName));
                if (clsClient.clsHHmem.LastName.Trim() + ", " + clsClient.clsHHmem.FirstName.Trim() == hhMemName)
                    tmpPtr = i;
			}
            if (ptList.Count > 0)
            {
                cboHHMem.DataSource = ptList;
                cboHHMem.DisplayMember = "LongName";
                cboHHMem.ValueMember = "LongName";
                cboHHMem.SelectedIndex = tmpPtr;
            }
            else
            {
                cboHHMem.DataSource = null;
                cboHHMem.Items.Clear();
                cboHHMem.Items.Add(clsClient.clsHH.Name);
                cboHHMem.SelectedIndex = 0;
            }
            chkFoodLbsManualEntry.Checked = false;
            chkNonFoodManualEntry.Checked = false;
            if (TrxIndexIn < 0)
            {
                lblCreated.Visible = false;
                lblModified.Visible = false;
                chkShowUserInfo.Visible = false;
                if (isAppt == true)
                {
                    BackColor = CCFBGlobal.bkColorFormAlt;
                    trxDateIn = CCFBGlobal.DefalutApptDate;
                    lblService.Text = "Date New Appointment";
                    clsTrxItem.TrxStatus = CCFBGlobal.statusTrxLog_NewAppt;
                }
                else
                {
                    BackColor = CCFBGlobal.bkColorFormAlt;
                    trxDateIn = CCFBGlobal.DefaultServiceDate;
                    lblService.Text = "Date New Service Transaction";
                }

                clsTrxLog.openForHH(clsClient.clsHH.ID);
                clsTrxItem = new TrxLogItem(clsTrxLog.DSet.Tables[0].NewRow(),clsClient.clsHH, clsClient.clsHHmem, hhMemName, TrxLogItem.SvcMethod.Pickup);
                clsTrxItem.TrxDate = Convert.ToDateTime(trxDateIn);
                clsTrxItem.HHMemID = hhMemName;
                clsDaysOpen.openTopTwentyWithinDate(clsTrxItem.TrxDate);
                clsDaysOpen.FindDate(clsTrxItem.TrxDate);
                CCFBGlobal.clsDailyItems.SetServiceDate(trxDateIn, clsDaysOpen.IsCommodity, clsDaysOpen.SpecialItems);
                CCFBGlobal.clsDailyItems.InitClientData(clsClient); 
                MarkNewServiceItems();
                if (isAppt == false)
                {
                    if (CCFBPrefs.EnableFastTrack == true)
                    {
                        clsTrxItem.TrxStatus = CCFBGlobal.statusTrxLog_FastTrack;
                    }
                    else
                    {
                        clsTrxItem.TrxStatus = CCFBGlobal.statusTrxLog_Service;
                    }
                    showSigPad();
                }
            }
            else
            {
                clsTrxLog.openWhere("TrxId = " + TrxIndexIn.ToString());
                clsTrxItem = new TrxLogItem(clsTrxLog.DRow);
                trxDateIn = clsTrxItem.TrxDate.ToShortDateString() ;
                lblCreated.Text = " Created: " + clsTrxItem.Created.ToShortDateString() + " " + clsTrxItem.Created.ToShortTimeString() + "  " + clsTrxItem.CreatedBy;
                lblModified.Text = "Modified: " + clsTrxItem.Modified.ToShortDateString() + " " + clsTrxItem.Modified.ToShortTimeString() + "  " + clsTrxItem.ModifiedBy;
                chkShowUserInfo.Visible = CCFBGlobal.UserIsAdmin();
                showUserInfo();

                if (clsTrxItem.TrxStatus == CCFBGlobal.statusTrxLog_Service || clsTrxItem.TrxStatus == CCFBGlobal.statusTrxLog_FastTrack || isAppt == false)
                { 
                    lblService.Text = "Edit Service Transaction";
                    BackColor = CCFBGlobal.bkColorBaseEdit;
                    if (isAppt == true)
                    {
                        lblService.Text = "Convert Appointment to Service Transaction";
                        clsTrxItem.TrxStatus = CCFBGlobal.statusTrxLog_Service;
                        MarkNewServiceItems();
                    }
                    else
                        lblService.Text = "Edit Service Transaction";
                }
                else
                { 
                    lblService.Text = "Edit Appointment";
                    BackColor = CCFBGlobal.bkColorApptEdit;
                }
                clsDaysOpen.FindDate(clsTrxItem.TrxDate);
                CCFBGlobal.clsDailyItems.SetServiceDate(trxDateIn, clsDaysOpen.IsCommodity, clsDaysOpen.SpecialItems);
                CCFBGlobal.clsDailyItems.InitClientData(clsClient);
                if (DateTime.Compare(clsTrxItem.TrxDate, Convert.ToDateTime(CCFBGlobal.DefaultServiceDate)) < 0)
                {
                    chkFoodLbsManualEntry.Checked = true;
                    chkNonFoodManualEntry.Checked = true;
                }
                else
                {
                    allowEntryOverride = true;
                }
                CheckServiceItemsFromList(clsTrxItem.ConcatFoodSvcItemsList, CCFBGlobal.clsDailyItems.FoodItemsList);
                CheckServiceItemsFromList(clsTrxItem.ConcatNonFoodSvcItemsList, CCFBGlobal.clsDailyItems.NonFoodItemsList);
            }
            cboHHMem.Text = clsTrxItem.HHMemID.ToString();
            CCFBGlobal.clsDailyItems.fillListViewItems(lvwFoodSvcItems, lvwNonFoodSvcItems, lvwBabyServices);
            lvwNonFoodSvcItems.Visible = (lvwNonFoodSvcItems.Items.Count > 0);
            chkNonFoodManualEntry.Checked = (lvwNonFoodSvcItems.Items.Count == 0);
            if (clsClient.clsHH.BabyServices == true)
            {
                lvwBabyServices.Visible = (lvwBabyServices.Items.Count > 0);
                chkBabySvcManualEntry.Checked = (lvwBabyServices.Items.Count == 0);
            }
            else
            {
                lvwBabyServices.Visible = false; 
            }
            pnlBabyServices.Visible = clsClient.clsHH.BabyServices && CCFBPrefs.EnableBabyServices;
            pnlCSFP.Visible = clsClient.clsHHmem.HasCSFP && CCFBPrefs.EnableCSFP && CCFBPrefs.EnableCSFPOnNewSvc;
            if (TrxIndexIn < 0)
            {
                if ((clsTrxItem.LbsCommodities + clsTrxItem.LbsBabySvc + clsTrxItem.LbsStandard
                              + clsTrxItem.LbsOther + clsTrxItem.LbsSupplemental + clsTrxItem.LbsNonFood
                              + calcLbsCSFP()) < 1)
                {
                    if (lvwFoodSvcItems.Items.Count == 1)
                    {
                        lvwFoodSvcItems.Items[0].Checked = true;
                        markFoodItem(0);
                    }
                }
            }
            fillForm();
        }

        public void fillForm()
        {
            btnTrxDate.Text = clsTrxItem.TrxDate.ToLongDateString();
            tbName.Text = clsClient.clsHH.Name;
            tbID.Text = clsClient.clsHH.ID.ToString();
            tbAddress.Text = clsClient.clsHH.Address;
            //Loads the family data textboxes
            foreach (TextBox tb in tbFamData)
            {
                tb.Text = clsTrxItem.GetDataValue(tb.Tag.ToString()).ToString();
            }
            foreach (TextBox tb in tbList)
            {
                tb.Text = clsTrxItem.GetDataValue(tb.Tag.ToString()).ToString();
            }
            ShowFoodLbs();
            ShowNonFoodLbs();
            ShowBabyServices();
            foreach (CheckBox chk in chkList)
            {
                chk.Checked = (bool)clsTrxItem.GetDataValue(chk.Tag.ToString());
            }
            tbNotes.Text = clsTrxItem.Notes;
            btnRecalcLbs.Enabled = false;
            if (chkFoodLbsManualEntry.Checked)
            {
            }
            if (CCFBPrefs.EnableCSFP == true && CCFBPrefs.EnableCSFPOnNewSvc == true)
            {
                fillCSFP();
            }
            enableSaveButton();
        }

        private void fillCSFP()
        {
            DateTime monthStart = CCFBGlobal.FirstDayOfMonth(clsTrxItem.TrxDate);
            DateTime lastService;   
            lvwCSFP.Items.Clear();
            bool okToAdd = false;
            for (int i = 0; i < clsClient.clsHHmem.RowCount; i++)
            {
                okToAdd = false;
                clsClient.clsHHmem.SetRecord(i);
                if (clsClient.clsHHmem.CSFP == true)
                {
                    if (clsClient.clsHHmem.CSFPExpiration >= monthStart)
                    {
                        lastService = clsClient.clsHHmem.LastCSFPLogEntry(clsTrxItem.TrxDate);
                        if (isNewTrx == true)
                        {
                            if (lastService < monthStart)
                            {
                                okToAdd = true;
                            }
                        }
                        else
                        {
                            if (lastService < monthStart || lastService == clsTrxItem.TrxDate)
                            {
                                okToAdd = true;
                            }
                        }
                        if (okToAdd == true)
                        {
                            ListViewItem lvi = new ListViewItem();
                            lvi.SubItems.Add(clsClient.clsHHmem.FirstName + " " + clsClient.clsHHmem.LastName);
                            lvi.SubItems.Add(lastService.ToShortDateString());
                            lvi.Tag = clsClient.clsHHmem.ID;
                            if (isNewTrx == false && lastService == clsTrxItem.TrxDate)
                            {
                                lvi.Checked = true;
                            }
                            if (clsClient.clsHHmem.CSFPExpiration > CCFBGlobal.FBNullDateValue)
                            {
                                lvi.ToolTipText = "Expires " + clsClient.clsHHmem.CSFPExpiration.ToShortDateString();
                            }
                            lvwCSFP.Items.Add(lvi);
                        }
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            bcanceled = true;
            if (haveSigPad == true)
            {
                sigPadInputCtrl1.ResetTablet();
            }
            this.Visible = false ;
        }

        private void btnRecalcLbs_Click(object sender, EventArgs e)
        {
            btnRecalcLbs.Enabled = false;
            MarkNewServiceItems();
            fillForm();
            btnSave.Enabled = true; 
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int newTrxId = 0;
            if (okToSave() == true)
            {
                btnSave.Enabled = false;
                if (clsTrxItem.FoodSvcList.Length > 0 || clsTrxItem.NonFoodSvcList.Length > 0 || clsTrxItem.BabySvcList.Length > 0)
                {
                    getConcatenatedSvcLists();
                    clsTrxItem.SetDataValue("HHMemId", cboHHMem.Text.Trim());

                    if (clsTrxItem.TrxId == 0)
                    {
                        clsTrxItem.Created = DateTime.Now;
                        clsTrxItem.CreatedBy = CCFBGlobal.dbUserName;
                        clsTrxLog.DSet.Tables[0].Rows.Add(clsTrxItem.DRow);
                        newTrxId = clsTrxLog.update(clsTrxItem.HouseholdID, clsTrxItem.TrxDate.ToShortDateString() );
                    }
                    else
                    {
                        newTrxId = clsTrxItem.TrxId;
                        clsTrxLog.update(0,"");
                    }
                }
                if (CCFBPrefs.EnableCSFP == true && CCFBPrefs.EnableCSFPOnNewSvc == true)
                {
                    string listHHMInsertId = "";
                    foreach (ListViewItem item in lvwCSFP.Items)
                    {
                        if (item.Checked == true)
                        {
                            if (isNewTrx == true)
                            {
                                if (listHHMInsertId != "")
                                    listHHMInsertId += ",";
                                listHHMInsertId += item.Tag.ToString();
                            }
                        }
                    }
                    if (listHHMInsertId != "")
                    {
                        CSFPLog clsCSFPLog = new CSFPLog(CCFBGlobal.connectionString);
                        clsCSFPLog.insertNewService(listHHMInsertId, clsTrxItem.TrxDate, tbCSFPLbsPerService.Text);
                    }
                }
                clsTrxLog.updateServiceBits(clsClient.clsHH.ID, Convert.ToDateTime(CCFBGlobal.DefaultServiceDate));
                if (CCFBPrefs.EnableClientReceipt == true)
                {
                    ReceiptPrinter clsPrintReceipt = new ReceiptPrinter(clsClient);
                    clsPrintReceipt.printIssaquah();
                }
                bcanceled = false;
                if (haveSigPad == true)
                {
                    if (sigPadInputCtrl1.Visible == true)
                    {
                        if (sigPadInputCtrl1.IsSigned == true)
                        {

                            
                                //picSignature.Visible = true;
                                ////cmsLog.Visible = false;
                                //MessageBox.Show(this, "Close Signature Display");
                                //picSignature.Visible = false;

                            TrxLogSig tlSig = new TrxLogSig(CCFBGlobal.connectionString);
                            tlSig.TrxId = newTrxId;
                            tlSig.HhID = clsTrxItem.HouseholdID;
                            tlSig.SigImage = sigPadInputCtrl1.GetImage();
                            tlSig.Insert();
                        }
                        sigPadInputCtrl1.ResetTablet();
                    }
                }
                this.Visible = false;
                btnSave.Enabled = true;
            }
        }

        private void getConcatenatedSvcLists()
        {
            clsTrxItem.ConcatFoodSvcItemsList = "";
            clsTrxItem.ConcatNonFoodSvcItemsList = "";
            clsTrxItem.ConcatBabySvcItemsList = "";
            clsTrxItem.RcvdCommodity = (clsTrxItem.LbsCommodities >0);
            clsTrxItem.RcvdSupplemental = (clsTrxItem.LbsSupplemental > 0);

            if (chkFoodLbsManualEntry.Checked == false)
            {
                for (int i = 0; i < lvwFoodSvcItems.CheckedItems.Count; i++)
                    { clsTrxItem.ConcatFoodSvcItemsList += lvwFoodSvcItems.CheckedItems[i].SubItems[2].Text + "|"; }
            }
            if (chkNonFoodManualEntry.Checked == false)
            {
                for (int i = 0; i < lvwNonFoodSvcItems.CheckedItems.Count; i++)
                    { clsTrxItem.ConcatNonFoodSvcItemsList += lvwNonFoodSvcItems.CheckedItems[i].SubItems[2].Text + "|"; }
            }
            if (chkBabySvcManualEntry.Checked == false)
                for (int i = 0; i < lvwBabyServices.CheckedItems.Count; i++)
                { clsTrxItem.ConcatBabySvcItemsList += lvwBabyServices.CheckedItems[i].SubItems[2].Text + "|"; }
        }

        private void chkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (loadingControls == false)
            {
                CheckBox cb = (CheckBox) sender;
                clsTrxItem.SetDataValue(cb.Tag.ToString(), cb.Checked);
                btnSave.Enabled = true;
            }        
        }

        private void tbFamData_Leave(object sender, EventArgs e)
        {
            TextBox tbHH = (TextBox)sender; //Get the correct textbox
            if (tbHH.Text != clsTrxItem.GetDataValue(tbHH.Tag.ToString()).ToString())
            {   //If current value does not = value of textbox
                if (tbHH.Text == "")
                    tbHH.Text = "0";
                clsTrxItem.SetDataValue(tbHH.Tag.ToString(), tbHH.Text);
                btnSave.Enabled = true;
                clsTrxItem.TotalFamily = clsTrxItem.Infants + clsTrxItem.Youths + clsTrxItem.Teens + clsTrxItem.Adults + clsTrxItem.Seniors;
                tbTotalFam.Text = clsTrxItem.TotalFamily.ToString();
                if (chkFoodLbsManualEntry.Checked == false)
                    btnRecalcLbs.Enabled = true;
            }
        }

        private void tbLbs_Leave(object sender, EventArgs e)
        {
            if (chkFoodLbsManualEntry.Checked)
            {
                TextBox tb = (TextBox)sender;
                clsTrxItem.SetDataValue(tb.Tag.ToString(), tb.Text);
                btnSave.Enabled = true;
                tbTotalLbs.Text = (clsTrxItem.LbsStandard  + clsTrxItem.LbsOther + clsTrxItem.LbsCommodities + clsTrxItem.LbsSupplemental ).ToString();

                fillFoodListWhenEditing();
                clsTrxItem.FoodSvcList = tbeFoodSvcLst.Text;
            }
        }

        private void fillFoodListWhenEditing()
        {
            tbeFoodSvcLst.Text = "";
            for (int i = 0; i < tbLbsList.Count; i++)
            {
                if (tbLbsList[i].Tag.ToString().ToLower().Contains("lbs")
                    && tbLbsList[i].Text.Trim() != ""
                    && Convert.ToInt32(tbLbsList[i].Text.Trim()) > 0)
                {
                    tbeFoodSvcLst.Text += tbLbsList[i].Text + " " + tbLbsList[i].Tag.ToString() + ", ";
                }
            }
        }

        private void tb_Leave(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            clsTrxItem.SetDataValue(tb.Tag.ToString(), tb.Text);
            btnSave.Enabled = true;
        }

        private void tbInteger_KeyDown(object sender, KeyEventArgs e)
        {
            CCFBGlobal.checkForIntOnKeyPress(e);
        }

        private void CheckServiceItemsFromList(String ServiceItemKeyList, List<ServiceItem> siList)
        {
            int j;
            string[] splitList = ServiceItemKeyList.Split('|');
            for (j = 0; j < splitList.Length; j++)
            {
                if ( splitList[j] != "")
                {
                    for (int idx = 0; idx < siList.Count; idx++)
                    {
                        try
                        {
                            if (Int32.Parse(splitList[j].ToString()) == siList.ElementAt(idx).ItemKey)
                            {
                                siList.ElementAt(idx).IsSelected = true;
                                break;
                            }
                        }
                        catch { break; }
                    }
                }
            }
        }
        public void MarkNewServiceItems()
        {
            clsTrxItem.LbsCommodities = 0;
            clsTrxItem.LbsStandard = 0;
            clsTrxItem.LbsSupplemental = 0;
            clsTrxItem.LbsOther = 0;
            clsTrxItem.FoodSvcList  = TestSvcItems(CCFBGlobal.clsDailyItems.FoodItemsList);
            clsTrxItem.LbsNonFood = 0; 
            clsTrxItem.NonFoodSvcList = TestSvcItems(CCFBGlobal.clsDailyItems.NonFoodItemsList);
            clsTrxItem.LbsBabySvc = 0;
            if (clsClient.clsHH.BabyServices == true)
            {
                pnlBabyServices.Enabled = true;
                clsTrxItem.BabySvcList = TestSvcItems(CCFBGlobal.clsDailyItems.BabyServicesList);
            }
            else
            {
                clsTrxItem.BabySvcList = "";
                pnlBabyServices.Enabled = false;
            }
        }

        private string TestSvcItems(List<ServiceItem> siList)
        {
            string svcDescr = "";
            int lbsCalc = 0;
            bool haveexclusive = false;
            foreach (ServiceItem si in siList)
            {
                si.IsSelected = CCFBGlobal.clsDailyItems.checkRule(si); 

                if (si.IsSelected)
                {
                    if (si.Exclusive == true)
                        haveexclusive = true;
                }
            }
            if (haveexclusive==true)
            {
                foreach (ServiceItem si in siList)
                {
                    if (si.Exclusive == false && si.IsSelected ==true)
                        si.IsSelected = false;
                }
            }
            foreach (ServiceItem si in siList)
            {
                if (si.IsSelected)
                {
                    lbsCalc = si.getFamSizeLbs(clsTrxItem.TotalFamily);
                    if (lbsCalc > 0)
                    {
                        switch ((int)si.ItemType)
                        {
                            case CCFBGlobal.svcCat_Commodity:
                                {
                                    if (clsClient.clsHH.NoCommodities == false)
                                    { clsTrxItem.LbsCommodities += lbsCalc; }
                                    break;
                                }
                            case CCFBGlobal.svcCat_Other:
                                { clsTrxItem.LbsOther += lbsCalc; break; }
                            case CCFBGlobal.svcCat_Supplemental:
                                { clsTrxItem.LbsSupplemental += lbsCalc; break; }
                            case CCFBGlobal.svcCat_Standard:
                                { clsTrxItem.LbsStandard += lbsCalc; break; }
                            case CCFBGlobal.svcCat_NonFood:
                                { clsTrxItem.LbsNonFood += lbsCalc; break; }
                            case CCFBGlobal.svcCat_BabySvc:
                                { clsTrxItem.LbsBabySvc += lbsCalc; break; } 
                        }
                        svcDescr += lbsCalc.ToString() + " " + si.Description + ",";
                    }
                }
            }
            return svcDescr;
        }

        private void chkFoodLbsManualEntry_CheckedChanged(object sender, EventArgs e)
        {
            if (isNewTrx == false && allowEntryOverride == false && chkFoodLbsManualEntry.Checked == false)
                chkFoodLbsManualEntry.Checked = true;
            SetDisplayMode();
        }

        private void SetDisplayMode()
        {
            bool EnableFoodLbsEntry;
            //panel3.BackColor = CCFBGlobal.bkColorBaseEdit; 
            if (chkFoodLbsManualEntry.Checked)
            {
                EnableFoodLbsEntry = true;
                lvwFoodSvcItems.Enabled = false;
                btnRecalcLbs.Enabled = false;
            }
            else
            {
                EnableFoodLbsEntry = false;
                lvwFoodSvcItems.Enabled = true;
                btnRecalcLbs.Enabled = true;
            }
            foreach (TextBox tb in tbLbsList)
            {
                tb.Enabled = EnableFoodLbsEntry;
            }
        }

        private void lvwBabyServices_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (loadingControls == false)
            {
                int SvcItemId = Int32.Parse(e.Item.SubItems[2].Text.ToString());
                ServiceItem si = CCFBGlobal.clsDailyItems.BabyServicesList.Find(delegate(ServiceItem si0) { return si0.ItemKey == SvcItemId; });
                if (si != null)
                {
                    si.IsSelected = e.Item.Checked;
                    btnSave.Enabled = true;
                    SumBabyServiceItems();
                    ShowBabyServices();
                }
                else
                {
                }
                enableSaveButton();
            }
        }

        private void lvwFoodSvcItems_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (loadingControls == false && bProcessItemChecked == false)
            {
                bProcessItemChecked = true;
                markFoodItem(e.Item.Index);
                bProcessItemChecked = false;
            }
        }

        private void markFoodItem(int idx)
        {
            ListViewItem itm = lvwFoodSvcItems.Items[idx];
            int svcItemId = Convert.ToInt32(itm.Tag.ToString());
            ServiceItem si = CCFBGlobal.clsDailyItems.FoodItemsList.Find(delegate(ServiceItem si0) { return si0.ItemKey == svcItemId; });
            if (si != null)
            {
                si.IsSelected = itm.Checked;
                if (si.Exclusive == true && itm.Checked == true)
                    foreach (ListViewItem item in lvwFoodSvcItems.Items)
                    {
                        if (item.Index != idx)
                        {
                            item.Checked = false;
                            CCFBGlobal.clsDailyItems.FoodItemsList.Find(delegate(ServiceItem si0) { return si0.ItemKey == Convert.ToInt32(item.Tag.ToString()); }).IsSelected = false;
                        }
                    }
                SumFoodServiceItems();
                ShowFoodLbs();
            }
            else
            {
            }
            enableSaveButton();
        }

        private void lvwNonFoodSvcItems_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (loadingControls == false)
            {
                int SvcItemId = Int32.Parse(e.Item.SubItems[2].Text.ToString());
                ServiceItem si = CCFBGlobal.clsDailyItems.NonFoodItemsList.Find(delegate(ServiceItem si0) { return si0.ItemKey == SvcItemId; });
                if (si != null)
                {
                    si.IsSelected = e.Item.Checked;
                    btnSave.Enabled = true;
                    SumNonFoodServiceItems();
                    ShowNonFoodLbs();
                }
                else
                {
                }
                enableSaveButton();
            }
        }


        private void EditServiceForm_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                loadingControls = false;
                if (btnSave.Enabled == true)
                    btnSave.Focus();

                //                lblNoSvcs.Visible = ! btnSave.Enabled;
            }
        }

        public void SumBabyServiceItems()
        {
            clsTrxItem.BabySvcList = "";
            clsTrxItem.LbsBabySvc = 0;
            int lbsCalc = 0;
            foreach (ServiceItem si in CCFBGlobal.clsDailyItems.BabyServicesList)
            {
                if (si.IsSelected)
                {
                    lbsCalc = si.getFamSizeLbs(clsClient.clsHH.TotalFamily);
                    if (lbsCalc > 0)
                    {
                        switch ((int)si.ItemType)
                        {
                            case CCFBGlobal.svcCat_BabySvc:
                                { clsTrxItem.LbsBabySvc += lbsCalc; break; }
                        }
                        clsTrxItem.BabySvcList += lbsCalc.ToString() + " " + si.Description + ",";
                    }
                }
            }
        }

        public void SumFoodServiceItems()
        {
            clsTrxItem.LbsCommodities = 0;
            clsTrxItem.LbsStandard = 0;
            clsTrxItem.LbsSupplemental = 0;
            clsTrxItem.LbsOther = 0;
            clsTrxItem.FoodSvcList = "";
            int lbsCalc = 0;
            foreach (ServiceItem si in CCFBGlobal.clsDailyItems.FoodItemsList)
            {
                if (si.IsSelected)
                {
                    lbsCalc = si.getFamSizeLbs(clsClient.clsHH.TotalFamily);
                    if (lbsCalc > 0)
                    {
                        switch ((int)si.ItemType)
                        {
                            case CCFBGlobal.svcCat_Commodity:
                                {
                                    if (clsClient.clsHH.NoCommodities == false)
                                    { clsTrxItem.LbsCommodities += lbsCalc; }
                                    break;
                                }
                            case CCFBGlobal.svcCat_Other:
                                { clsTrxItem.LbsOther += lbsCalc; break; }
                            case CCFBGlobal.svcCat_Supplemental:
                                { clsTrxItem.LbsSupplemental += lbsCalc; break; }
                            case CCFBGlobal.svcCat_Standard:
                                { clsTrxItem.LbsStandard += lbsCalc; break; }
                            case CCFBGlobal.svcCat_NonFood:
                                { clsTrxItem.LbsNonFood += lbsCalc; break; }
                        }
                        clsTrxItem.FoodSvcList  += lbsCalc.ToString() + " " + si.Description + ",";
                    }
                }
            }
        }

        public void SumNonFoodServiceItems()
        {
            clsTrxItem.NonFoodSvcList = "";
            clsTrxItem.LbsNonFood = 0;
            int lbsCalc = 0;
            foreach (ServiceItem si in CCFBGlobal.clsDailyItems.NonFoodItemsList)
            {
                if (si.IsSelected)
                {
                    lbsCalc = si.getFamSizeLbs(clsClient.clsHH.TotalFamily);
                    if (lbsCalc > 0)
                    {
                        switch ((int)si.ItemType)
                        {
                            case CCFBGlobal.svcCat_NonFood:
                                { clsTrxItem.LbsNonFood += lbsCalc; break; }
                        }
                        clsTrxItem.NonFoodSvcList += lbsCalc.ToString() + " " + si.Description + ",";
                    }
                }
            }
        }

        public void ShowBabyServices()
        {
            tbBabyServices.Text = clsTrxItem.LbsBabySvc.ToString();
            tbBabySvcList.Text = clsTrxItem.BabySvcList;
        }

        public void ShowFoodLbs()
        {
            foreach (TextBox tb in tbLbsList)
            { tb.Text = clsTrxItem.GetDataValue(tb.Tag.ToString()).ToString(); }
            tbTotalLbs.Text = (clsTrxItem.LbsCommodities + clsTrxItem.LbsBabySvc + clsTrxItem.LbsStandard + clsTrxItem.LbsOther + clsTrxItem.LbsSupplemental).ToString();
        }

        public void ShowNonFoodLbs()
        {
            tbNonFoodLbs.Text = clsTrxItem.LbsNonFood.ToString();
            tbNonFoodSvcList.Text = clsTrxItem.NonFoodSvcList;
        }
 
        private void NewServiceForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isNewTrx == true)
            {
            //    clsClient.clsHHSvcTrans.openForHH(clsClient.clsHH.ID);
            //}
            //else
            //{
            //    clsClient.clsHHSvcTrans.openForHH(clsClient.clsHH.ID);
            //    clsDailyItems.setForToday();
            }
        }

//        private void chkRcvdSuppOnly_CheckedChanged(object sender, EventArgs e)
//        {
//            if (loadingControls == false)
//            {
//                if (chkRcvdSuppOnly.Checked == true)
//                {
//                    loadingControls = true;
//                    for (int i = 0; i < lvwFoodSvcItems.Items.Count; i++)
//                    {
//                        lvwFoodSvcItems.Items[i].Checked = false;
//                    }
//                    for (int i = 0; i < lvwNonFoodSvcItems.Items.Count; i++)
//                    {
//                        lvwNonFoodSvcItems.Items[i].Checked = false;
//                    }
//                    loadingControls = false;
//                }
//                else
//                {
////                    calcPounds();
//                }
//            }
//        }

        private void cboClientType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (loadingControls == false)
                clsTrxLog.ClientType = Convert.ToInt16(((parmType)cboClientType.SelectedItem).UID);
        }

        private void chkBabySvcManualEntry_CheckedChanged(object sender, EventArgs e)
        {
            if (isNewTrx == true || allowEntryOverride == true)
            {
                if (chkBabySvcManualEntry.Checked == true)
                {
                    lvwBabyServices.Enabled = false;
                    tbBabySvcList.Enabled = true;
                    tbBabyServices.Enabled = true;
                }
                else
                {
                    lvwBabyServices.Enabled = true;
                    tbBabySvcList.Enabled = false;
                    tbBabyServices.Enabled = false;
                }
            }
            else
            {
                if (chkBabySvcManualEntry.Checked == false)
                    chkBabySvcManualEntry.Checked = true;

                tbBabySvcList.Enabled = true;
                tbBabyServices.Enabled = true;
                lvwBabyServices.Enabled = false;
            }
        }

        private void chkNonFoodManualEntry_CheckedChanged(object sender, EventArgs e)
        {
            if (isNewTrx == true || allowEntryOverride == true)
            {
                if (chkNonFoodManualEntry.Checked == true)
                {
                    lvwNonFoodSvcItems.Enabled = false;
                    tbNonFoodSvcList.Enabled = true;
                    tbNonFoodLbs.Enabled = true;
                }
                else
                {
                    lvwNonFoodSvcItems.Enabled = true;
                    tbNonFoodSvcList.Enabled = false;
                    tbNonFoodLbs.Enabled = false;
                }
            }
            else
            {
                if (chkNonFoodManualEntry.Checked == false)
                    chkNonFoodManualEntry.Checked = true;

                tbNonFoodSvcList.Enabled = true;
                tbNonFoodLbs.Enabled = true;
                lvwNonFoodSvcItems.Enabled = false;
            }
        }


        /// <summary>
        /// Traverses all controls on the page using recursion and adds the proper ones
        /// to their proper collections and adds LostFocus event to Textboxes and Checkboxes
        /// </summary>
        /// <param name="controlList"></param>
        private void traverseAndAddControlsToCollections(Control.ControlCollection controlList)
        {
            foreach (Control cntrl in controlList.OfType<Control>())
            {
                if (cntrl.Tag != null && cntrl.Tag.ToString().Trim() != "")
                switch (cntrl.GetType().Name)
                {
                    case "TextBox":
                        {
                            TextBox tb = (TextBox)cntrl;
                            if (tb.Name.Substring(0, 3) == "tbe")
                            {
                                tbLbsList.Add(tb);
                            }
                            else if (tb.Name.Substring(0, 3) == "tbf")
                                {
                                    tbFamData.Add(tb);
                                }
                            else if (tb.Tag.ToString() !="")
                            {
                                tbList.Add(tb);
	                        }
                            break;
                        }
                    case "CheckBox":
                        {
                            chkList.Add((CheckBox)cntrl);
                            break;
                        }
                }

                traverseAndAddControlsToCollections(cntrl.Controls);
            }
        }

        private void cboHHMem_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cboHHMem.Focused == true)
            {
                clsTrxItem.SetDataValue("HHMemId",((parmType)cboHHMem.SelectedItem).LongName);
            }
        }

        private void tbeFoodSvcLst_KeyDown(object sender, KeyEventArgs e)
        {
            if (chkFoodLbsManualEntry.Checked == false)
                e.Handled = true;
        }

        private void lvwFoodSvcItems_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void LoadHHDemoGraphics(TrxLogItem clsTL, Household clsHH)
        {
        }

        private void lvwCSFP_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            enableSaveButton();
        }

        private int calcLbsCSFP()
        {
            int sumLbs = 0;
            int lbsPerPerson = Convert.ToInt32(tbCSFPLbsPerService.Text.ToString());
            foreach (ListViewItem item in lvwCSFP.Items)
            {
                if (item.Checked == true)
                    sumLbs += lbsPerPerson;
            }
            return sumLbs;
        }

        private void enableSaveButton()
        {
            btnSave.Enabled = ( clsTrxItem.LbsCommodities + clsTrxItem.LbsBabySvc + clsTrxItem.LbsStandard 
                              + clsTrxItem.LbsOther + clsTrxItem.LbsSupplemental + clsTrxItem.LbsNonFood 
                              + calcLbsCSFP() ) >  0;
            lblNoSvcs.Visible = ! btnSave.Enabled;
            if (btnSave.Enabled == true)
                btnSave.Focus();
        }

        private void chkShowUserInfo_CheckedChanged(object sender, EventArgs e)
        {
            showUserInfo();
        }

        private void showUserInfo()
        {
            lblCreated.Visible = chkShowUserInfo.Checked;
            lblModified.Visible = chkShowUserInfo.Checked;
        }

        private void showSigPad()
        {
            sigPadInputCtrl1.Visible = false;
            btnResetSig.Visible = false;
            if (haveSigPad == true && clsClient.clsHH.NoCommodities == false)
            {
                if (clsTrxItem.LbsCommodities > 0)
                {
                    sigPadInputCtrl1.Visible = true;
                    btnResetSig.Visible = true;
                    if (sigPadInputCtrl1.ScreenMode < 0)
                    {
                        sigPadInputCtrl1.StartCapture();
                    }
                }
            }
        }

        private bool okToSave()
        {
            if (haveSigPad == true)
            {
                if (sigPadInputCtrl1.Visible == true)
                {
                    return sigPadInputCtrl1.IsSigned;
                }
            }
            return true;
        }

        private void tbBabyServices_Leave(object sender, EventArgs e)
        {
            if (tbBabyServices.Enabled == true)
            {
                clsTrxItem.SetDataValue("LbsBabySvc", tbBabyServices.Text);
            }
        }

        private void tbBabySvcList_Leave(object sender, EventArgs e)
        {
            if (tbBabySvcList.Enabled == true)
            {
                clsTrxItem.SetDataValue("BabySvcList", tbBabySvcList.Text);
            }
        }

        private void btnResetSig_Click(object sender, EventArgs e)
        {
            showSigPad();
        }
    }
}