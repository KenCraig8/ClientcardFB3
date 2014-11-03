using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
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

        bool isNewTrx = false;
        bool isAppt = false;
        bool loadingControls = true;
        string trxDateIn = "";
        int HHMemID = 0;

        public EditServiceShortForm()
        {
            InitializeComponent();
            clsParmClientType.openAll();
            tbeMeals.Visible = (CCFBPrefs.NbrMealsPerService > 0);
            lblMeals.Visible = (CCFBPrefs.NbrMealsPerService > 0);

            tbeLbsComm.Visible = CCFBPrefs.EnableTEFAP;
            lblCommodity.Visible = CCFBPrefs.EnableTEFAP;

            tbeLbsSupp.Visible = CCFBPrefs.EnableSupplemental;
            lblSuppl.Visible = CCFBPrefs.EnableSupplemental;

            //Load cboClientType
            CCFBGlobal.InitCombo(cboClientType, CCFBGlobal.parmTbl_Client);

            traverseAndAddControlsToCollections(this.Controls);

            loadingControls = false;
        }

        public DateTime ServiceDate
        {
            get { return clsTrxItem.TrxDate; }
        }

        public void initForm(Client clsClientIn, bool ModeIn, bool isApptIn, int TrxIndexIn, int hhMemID)
        {
            clsClient = clsClientIn;
            clsTrxLog = clsClient.clsHHSvcTrans;
            isNewTrx = ModeIn;
            isAppt = isApptIn;
            loadingControls = true;
            if (isNewTrx == false)
            {
                chkFoodLbsManualEntry.Checked = true;
                chkNonFoodManualEntry.Checked = true;
            }
            else
            {
                chkFoodLbsManualEntry.Checked = false;
                chkNonFoodManualEntry.Checked = false;
            }

            SetDisplayMode();
            cboClientType.SelectedValue = clsClient.clsHH.ClientType.ToString();
            List<parmType> ptList = new List<parmType>();
            for (int i = 0; i < clsClient.clsHHmem.RowCount; i++)
			{
                clsClient.clsHHmem.SetRecord(i);
                ptList.Add(new parmType(clsClient.clsHHmem.ID, (clsClient.clsHHmem.FirstName + " " 
                    + clsClient.clsHHmem.LastName).Trim(), i, clsClient.clsHHmem.FirstName));
			}
            cboHHMem.DataSource = ptList;
            cboHHMem.DisplayMember = "LongName";
            cboHHMem.ValueMember = "UID";
            
            if (TrxIndexIn < 0)
            {
                clsTrxLog.openForHH(clsClient.clsHH.ID);
                clsTrxItem = new TrxLogItem(clsTrxLog.DSet.Tables[0].NewRow(),clsClient.clsHH, hhMemID);
                if (isAppt == true)
                {
                    BackColor = CCFBGlobal.bkColorApptEdit; 
                    trxDateIn = CCFBGlobal.DefalutApptDate;
                    lblService.Text = "Date New Appointment";
                    clsTrxItem.TrxStatus = CCFBGlobal.statusTrxLog_NewAppt;
                }
                else
                {
                    BackColor = CCFBGlobal.bkColorBaseEdit; 
                    trxDateIn = CCFBGlobal.DefaultServiceDate;
                    lblService.Text = "Date New Service Transaction";
                    clsTrxItem.TrxStatus = CCFBGlobal.statusTrxLog_Service;
                }
                clsTrxItem.TrxDate = Convert.ToDateTime(trxDateIn);
                clsTrxItem.HHMemID = clsClient.ServingHHMemID;
                clsDaysOpen.openTopTwentyWithinDate(clsTrxItem.TrxDate);
                clsDaysOpen.FindDate(clsTrxItem.TrxDate);
                CCFBGlobal.clsDailyItems.SetServiceDate(trxDateIn, clsDaysOpen.IsCommodity, clsDaysOpen.SpecialItems);
                CCFBGlobal.clsDailyItems.InitClientData(clsClient); 
                MarkNewServiceItems();
            }
            else
            {
                clsTrxLog.openWhere("TrxId = " + TrxIndexIn.ToString());
                clsTrxItem = new TrxLogItem(clsTrxLog.DRow);
                trxDateIn = clsTrxItem.TrxDate.ToShortDateString() ;
                if (clsTrxItem.TrxStatus == CCFBGlobal.statusTrxLog_Service || isAppt == false)
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
                CheckServiceItemsFromList(clsTrxItem.ConcatFoodSvcItemsList, CCFBGlobal.clsDailyItems.FoodItemsList);
                CheckServiceItemsFromList(clsTrxItem.ConcatNonFoodSvcItemsList, CCFBGlobal.clsDailyItems.NonFoodItemsList);
            }
            cboHHMem.SelectedValue = clsTrxItem.HHMemID.ToString();
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
            pnlBabyServices.Visible = clsClient.clsHH.BabyServices;

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
        }
      
        private void btnCancel_Click(object sender, EventArgs e)
        {
            clsTrxLog.openForHH(clsClient.clsHH.ID);
            this.Close();
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
            btnSave.Enabled = false;
            getConcatenatedSvcLists();

            if (clsTrxItem.TrxId == 0)
                clsTrxLog.DSet.Tables[0].Rows.Add(clsTrxItem.DRow);
            //else
            //    clsTrxLog.DRow = clsTrxItem.DRow;

            clsTrxLog.update();

            this.Close();
            btnSave.Enabled = true;
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
            clsTrxItem.BabySvcList = TestSvcItems(CCFBGlobal.clsDailyItems.BabyServicesList);
        }

        private string TestSvcItems(List<ServiceItem> siList)
        {
            string svcDescr = "";
            int lbsCalc = 0; 
            foreach (ServiceItem si in siList)
            {
                si.IsSelected = CCFBGlobal.clsDailyItems.checkRule(si);

                if (si.IsSelected)
                {
                    lbsCalc = si.getFamSizeLbs(clsTrxItem.TotalFamily);
                    if (lbsCalc > 0)
                    {
                        switch ((int)si.ItemType)
                        {
                            case CCFBGlobal.svcCat_Commodity:
                                { clsTrxItem.LbsCommodities += lbsCalc; break; }
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
            if (isNewTrx == true)
                SetDisplayMode();
            else if (chkFoodLbsManualEntry.Checked == false)
                chkFoodLbsManualEntry.Checked = true;
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
            }
        }

        private void lvwFoodSvcItems_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (loadingControls == false)
            {
                int SvcItemId = Int32.Parse(e.Item.SubItems[2].Text.ToString());
                ServiceItem si = CCFBGlobal.clsDailyItems.FoodItemsList.Find(delegate(ServiceItem si0) { return si0.ItemKey == SvcItemId;  });
                if (si != null)
                {
                    si.IsSelected = e.Item.Checked;
                    btnSave.Enabled = true;
                    SumFoodServiceItems();
                    ShowFoodLbs();
                }
                else
                {
                }
            }


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
            }
        }

        private void EditServiceForm_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                loadingControls = false;
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
                            case CCFBGlobal.svcCat_NonFood:
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
                                { clsTrxItem.LbsCommodities += lbsCalc; break; }
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
            if (isNewTrx == true)
            {
                if (chkBabySvcManualEntry.Checked == true)
                {
                    lvwBabyServices.Enabled = false;
                    tbBabySvcList.Enabled = true;
                    tbBabyServices.Enabled = true;
                }
                else
                {
                    lvwNonFoodSvcItems.Enabled = true;
                    tbBabySvcList.Enabled = false;
                    tbBabyServices.Enabled = false;
                }
            }
            else
            {
                if (chkNonFoodManualEntry.Checked == false)
                    chkNonFoodManualEntry.Checked = true;

                tbBabySvcList.Enabled = true;
                tbBabyServices.Enabled = true;
                lvwBabyServices.Enabled = false;
            }
        }

        private void chkNonFoodManualEntry_CheckedChanged(object sender, EventArgs e)
        {
            if (isNewTrx == true)
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

        private void tbName_TextChanged(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

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
                clsTrxItem.SetDataValue("HHMemId",((parmType)cboHHMem.SelectedItem).UID);
            }
        }
    }
}