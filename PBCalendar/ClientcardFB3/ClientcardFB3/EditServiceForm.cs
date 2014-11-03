using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ClientCardFB3
{
    public partial class EditServiceForm : Form
    {
        Client clsClient;
        TrxLog clsTrxLog;
        TrxLogItem clsTrxItem;
        DaysOpen clsDaysOpen = new DaysOpen(CCFBGlobal.connectionString);
        parm_ClientType clsParmClientType = new parm_ClientType(CCFBGlobal.connectionString);

        List<TextBox> tbList = new List<TextBox>();
        List<TextBox> tbLbsList = new List<TextBox>();
        List<TextBox> tbFamData = new List<TextBox>();

        bool isNewTrx = false;
        bool isAppt = false;
        bool loadingControls = true;
        string trxDateIn = "";
        int HHMemID = 0;
        public EditServiceForm()
        {
            InitializeComponent();
            clsParmClientType.openAll();
            tbeMeals.Visible = (CCFBPrefs.NbrMealsPerService > 0);
            lblMeals.Visible = (CCFBPrefs.NbrMealsPerService > 0);

            tbeLbsCSFP.Visible = CCFBPrefs.EnableCSFP;
            lblCSFP.Visible = CCFBPrefs.EnableCSFP;

            tbeLbsComm.Visible = CCFBPrefs.EnableTEFAP;
            lblCommodity.Visible = CCFBPrefs.EnableTEFAP;

            tbeLbsSupp.Visible = CCFBPrefs.EnableSupplemental;
            lblSuppl.Visible = CCFBPrefs.EnableSupplemental;

            //Load cboClientType
            CCFBGlobal.InitCombo(cboClientType, CCFBGlobal.parmTbl_Client);

            //Fill Textbox lists and set default LostFocus Handler
            foreach (TextBox tb in panel3.Controls.OfType<TextBox>())
            {
                if (tb.Name.Substring(0, 3) == "tbe")
                {
                    tbLbsList.Add(tb);
                }
            }
            foreach (TextBox tb in panel1.Controls.OfType<TextBox>())
            {
                if (tb.Tag.ToString().Length > 2)
                {
                    if (tb.Tag.ToString().Substring(0, 3) == "tbf")
                    {
                        tbFamData.Add(tb);
                    }
                }
            }


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

            if (isNewTrx == false)
            {
                chkManualEntry.Checked = true;
                chkEnterNonFoodManually.Checked = true;
            }
            else
            {
                chkManualEntry.Checked = false;
                chkEnterNonFoodManually.Checked = false;
            }

            SetDisplayMode();
            //cboClientType.
            for (int i = 0; i < cboClientType.Items.Count; i++)
            {
                if (clsClient.clsHH.ClientType == Convert.ToInt16 (clsParmClientType.DSet.Tables[0].Rows[i]["ID"]))
                {
                    cboClientType.SelectedIndex = i;
                    break;
                }
            }
            
            if (TrxIndexIn < 0)
            {
                clsTrxItem = new TrxLogItem(clsTrxLog.DSet.Tables[0].NewRow(),clsClient.clsHH, hhMemID);
                if (isAppt == true)
                {
                    BackColor = CCFBGlobal.bkColorApptEdit; 
                    trxDateIn = CCFBGlobal.DefalutApptDate;
                    lblService.Text = "New Appointment";
                    clsTrxItem.TrxStatus = CCFBGlobal.statusTrxLog_NewAppt;
                }
                else
                {
                    BackColor = CCFBGlobal.bkColorBaseEdit; 
                    trxDateIn = CCFBGlobal.DefaultServiceDate;
                    lblService.Text = "New Service Transaction";
                    clsTrxItem.TrxStatus = CCFBGlobal.statusTrxLog_Service;
                }
                clsTrxItem.TrxDate = Convert.ToDateTime(trxDateIn);
                clsDaysOpen.openTopTwentyWithinDate(clsTrxItem.TrxDate);
                clsDaysOpen.FindDate(clsTrxItem.TrxDate);
                CCFBGlobal.clsDailyItems.SetServiceDate(trxDateIn, clsDaysOpen.IsCommodity, clsDaysOpen.SpecialItems);
                CCFBGlobal.clsDailyItems.InitClientData(clsClient); 
                MarkNewServiceItems();
            }
            else
            {
                if (clsTrxLog.TrxId != TrxIndexIn)
                {
                    clsTrxLog.find(TrxIndexIn);
                }
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
            loadingControls = true;
            CCFBGlobal.clsDailyItems.fillListViewItems(lvwFoodSvcItems, lvwNonFoodSvcItems, lvwBabyServices);
            if (lvwNonFoodSvcItems.Items.Count == 0)
            {
                lvwNonFoodSvcItems.Visible = false;
                chkEnterNonFoodManually.Checked = true;
            }

            fillForm();
        }


        public void fillForm()
        {
            dtpTrxDate.Value = clsTrxItem.TrxDate;
            tbName.Text = clsClient.clsHH.Name;

            //Loads the family data textboxes
            foreach (TextBox tb in panel1.Controls.OfType<TextBox>())
            {
                if (tb.Tag.ToString() != "")
                    tb.Text = clsTrxItem.GetDataValue(tb.Tag.ToString()).ToString();
            }
            ShowFoodLbs();
            ShowNonFoodLbs();
            foreach (CheckBox chk in panel1.Controls.OfType<CheckBox>())
            {
                chk.Checked = (bool)clsTrxItem.GetDataValue(chk.Tag.ToString());
            }
            foreach (CheckBox chk in panel3.Controls.OfType<CheckBox>())
            {
                if (chk.Tag.ToString() != "")
                { chk.Checked = (bool)clsTrxItem.GetDataValue(chk.Tag.ToString()); }
            }
            tbNotes.Text = clsTrxItem.Notes;
            btnRecalcLbs.Enabled = false;
            if (chkManualEntry.Checked)
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
 
            clsTrxLog.update();

            this.Close();
            btnSave.Enabled = true;
        }

        private void getConcatenatedSvcLists()
        {
            clsTrxItem.ConcatFoodSvcItemsList = "";
            clsTrxItem.ConcatNonFoodSvcItemsList = "";
            clsTrxItem.RcvdCommodity = (clsTrxItem.LbsCommodities >0);
            clsTrxItem.RcvdSupplemental = (clsTrxItem.LbsSupplemental > 0);

            if (chkManualEntry.Checked == false)
            {
                for (int i = 0; i < lvwFoodSvcItems.CheckedItems.Count; i++)
                    { clsTrxItem.ConcatFoodSvcItemsList += lvwFoodSvcItems.CheckedItems[i].SubItems[2].Text + "|"; }
            }
            if (chkEnterNonFoodManually.Checked == false)
            {
                for (int i = 0; i < lvwNonFoodSvcItems.CheckedItems.Count; i++)
                    { clsTrxItem.ConcatNonFoodSvcItemsList += lvwNonFoodSvcItems.CheckedItems[i].SubItems[2].Text + "|"; }
            }
        }
        private void cboTrxDate_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (isNewTrx == true)
            {
            }
            else
            {
            }
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
                clsTrxItem.SetDataValue(tbHH.Tag.ToString(), tbHH.Text);
                tbTotalFam.Text = (Int32.Parse(tbfYouth.Text) + Int32.Parse(tbfAdults.Text) +
                    Int32.Parse(tbfTeens.Text) + Int32.Parse(tbfInfants.Text) + Int32.Parse(tbfSeniors.Text)).ToString();
                clsTrxItem.TotalFamily = clsTrxItem.Infants + clsTrxItem.Youths + clsTrxItem.Teens + clsTrxItem.Adults + clsTrxItem.Seniors;
                btnSave.Enabled = true;
                if (chkManualEntry.Checked == false)
                    btnRecalcLbs.Enabled = true;
            }
        }

        private void tbLbs_Leave(object sender, EventArgs e)
        {
            if (chkManualEntry.Checked)
            {
                TextBox tb = (TextBox)sender;
                clsTrxItem.SetDataValue(tb.Tag.ToString(), tb.Text);
                tbTotalLbs.Text = (Int32.Parse(tbeLbsComm.Text) + Int32.Parse(tbeLbsComm.Text)
                                 + Int32.Parse(tbeLbsComm.Text) + Int32.Parse(tbeLbsComm.Text)
                                 + Int32.Parse(tbeLbsComm.Text)).ToString();

                fillFoodListWhenEditing();
                clsTrxItem.FoodSvcList = tbeFoodSvcLst.Text;
                btnSave.Enabled = true;
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

        private void tbNotes_Leave(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text != clsTrxItem.Notes)
            { 
                clsTrxItem.Notes = tb.Text; 
                btnSave.Enabled = true;
            }
        }

        private void tbeFoodSvcLst_Leave(object sender, EventArgs e)
        {
            if (chkManualEntry.Checked)
            {
                TextBox tb = (TextBox)sender;
                clsTrxItem.SetDataValue(tb.Tag.ToString(), tb.Text);
                btnSave.Enabled = true;
            }
        }

        private void tb_Leave(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            clsTrxItem.SetDataValue(tb.Tag.ToString(), tb.Text);
            btnSave.Enabled = true;
        }

        private void tbLbsComm_TextChanged(object sender, EventArgs e)
        {
            int lbsComm = 0;
            try
            {
                lbsComm = Convert.ToInt32(tbeLbsComm.Text);
            }
            catch { lbsComm = 0; }
            if (lbsComm > 0)
            {
                chkRcvdComm.Checked = true;
            }
            else if (lbsComm < 0)
            {
                tbeLbsComm.Text = "0";
            }
            else
            {
                chkRcvdComm.Checked = false;
            }
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
            clsTrxItem.LbsBabySvc = 0;
            clsTrxItem.LbsStandard = 0;
            clsTrxItem.LbsSupplemental = 0;
            clsTrxItem.LbsOther = 0;
            clsTrxItem.FoodSvcList  = TestSvcItems(CCFBGlobal.clsDailyItems.FoodItemsList);
            clsTrxItem.LbsNonFood = 0; 
            clsTrxItem.NonFoodSvcList = TestSvcItems(CCFBGlobal.clsDailyItems.NonFoodItemsList);
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
                        }
                        svcDescr += lbsCalc.ToString() + " " + si.Description + ",";
                    }
                }
            }
            return svcDescr;
        }

        private void chkManualEntry_CheckedChanged(object sender, EventArgs e)
        {
            if (isNewTrx == true)
                SetDisplayMode();
            else if (chkManualEntry.Checked == false)
                chkManualEntry.Checked = true;
        }

        private void SetDisplayMode()
        {
            bool EnableFoodLbsEntry;
            //panel3.BackColor = CCFBGlobal.bkColorBaseEdit; 
            if (chkManualEntry.Checked)
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

        public void SumFoodServiceItems()
        {
            clsTrxItem.LbsCommodities = 0;
            clsTrxItem.LbsBabySvc = 0;
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
                            case CCFBGlobal.svcCat_BabySvc:
                                { clsTrxItem.LbsBabySvc += lbsCalc; break; }
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
            clsTrxItem.Meals = Convert.ToInt16(CCFBPrefs.NbrMealsPerService * clsClient.clsHH.TotalFamily);
        }
        public void ShowFoodLbs()
        {
            foreach (TextBox tb in tbLbsList)
            { tb.Text = clsTrxItem.GetDataValue(tb.Tag.ToString()).ToString(); }
            tbTotalLbs.Text = (clsTrxItem.LbsCommodities + clsTrxItem.LbsBabySvc + clsTrxItem.LbsStandard + clsTrxItem.LbsOther + clsTrxItem.LbsSupplemental).ToString();
        }

        public void ShowNonFoodLbs()
        {
            foreach (TextBox tb in panel2.Controls.OfType<TextBox>())
            { tb.Text = clsTrxItem.GetDataValue(tb.Tag.ToString()).ToString(); }
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

        private void chkRcvdSuppOnly_CheckedChanged(object sender, EventArgs e)
        {
            if (loadingControls == false)
            {
                if (chkRcvdSuppOnly.Checked == true)
                {
                    loadingControls = true;
                    for (int i = 0; i < lvwFoodSvcItems.Items.Count; i++)
                    {
                        lvwFoodSvcItems.Items[i].Checked = false;
                    }
                    for (int i = 0; i < lvwNonFoodSvcItems.Items.Count; i++)
                    {
                        lvwNonFoodSvcItems.Items[i].Checked = false;
                    }
                    loadingControls = false;
                }
                else
                {
//                    calcPounds();
                }
            }
        }

        private void cboClientType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (loadingControls == false)
                clsTrxLog.ClientType = Convert.ToInt16(((parmType)cboClientType.SelectedItem).ID);
        }

        private void chkEnterNonFoodManually_CheckedChanged(object sender, EventArgs e)
        {
            if (isNewTrx == true)
            {
                if (chkEnterNonFoodManually.Checked == true)
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
                if (chkEnterNonFoodManually.Checked == false)
                    chkEnterNonFoodManually.Checked = true;

                tbNonFoodSvcList.Enabled = true;
                tbNonFoodLbs.Enabled = true;
                lvwNonFoodSvcItems.Enabled = false;
            }
        }

        private void dtpTrxDate_ValueChanged(object sender, EventArgs e)
        {

        }
   }
}