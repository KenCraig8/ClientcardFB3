using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace ClientcardFB3
{
    class DailyItemsClass
    {
        String ServiceDate = "";
        bool isCommodityDay = false;
        string SpecialSvcItemList = ""; 
        List<ServiceItem> svcItemsFood;
        List<ServiceItem> svcItemsNonFood;
        List<ServiceItem> svcItemsBabyService;

        ServiceItems clsServiceItems;

        //Client Data Structure
        struct ClientData
        {
            internal bool SupplementalOnly;
            internal bool NoCommodity;
            internal bool HaveCSFP;
            internal bool Homeless;
            internal bool Transient;
            internal int FamilySize;
            internal int NbrServicesThisFiscalYr;
            internal int NbrServicesThisCalYr;
            internal int NbrServicesThisMonth;
            internal int NbrServicesThisWeek;
            internal int NbrTEFAPThisMonth;
            internal int NbrSupplThisMonth;
            internal int NbrFullSvcThisMonth;
            internal DateTime LastFullService;
            internal DateTime LastTEFAPService;
            internal DateTime LastSupplService;
        }

        ClientData ClientStuff;

        public DailyItemsClass()
        {
            clsServiceItems = new ServiceItems(CCFBGlobal.connectionString);
            clsServiceItems.openWhere("");
            svcItemsFood = new List<ServiceItem>();
            svcItemsNonFood = new List<ServiceItem>();
            svcItemsBabyService = new List<ServiceItem>();
        }


        #region Get/Set Accessors

        public List<ServiceItem> FoodItemsList
        {
            get { return svcItemsFood; }
            set { svcItemsFood = value; }
        }

        public List<ServiceItem> NonFoodItemsList
        {
            get { return svcItemsNonFood; }
            set { svcItemsNonFood = value; }
        }

        public List<ServiceItem> BabyServicesList
        {
            get { return svcItemsBabyService; }
            set { svcItemsBabyService = value; }
        }
        #endregion

        public void ClearSelected()
        {
            foreach (ServiceItem svcItm in svcItemsFood)
            { svcItm.IsSelected = false; }
            foreach (ServiceItem svcItm in svcItemsNonFood)
            { svcItm.IsSelected = false; }
            foreach (ServiceItem svcItm in svcItemsBabyService)
            { svcItm.IsSelected = false; }
        }
        public void Refresh(bool bAllowAll)
        {
            bool allowThisItem = false;
            bool isNonFoodItem = false;
            bool isBabyItem = false;

            svcItemsFood.Clear();
            svcItemsNonFood.Clear();
            svcItemsBabyService.Clear();
            clsServiceItems.openWhere("NotAvailable = 0");
            foreach (DataRow drow in clsServiceItems.DSet.Tables[0].Rows)
            {
                isNonFoodItem = false;
                isBabyItem = false;
                switch (Int32.Parse(drow["ItemType"].ToString()))
                {
                    case CCFBGlobal.svcCat_Commodity:
                        {
                            if (bAllowAll == true)
                            {
                                allowThisItem = true;
                            }
                            else 
                            {
                                if (CCFBPrefs.MustBeACommodityDay == true)
                                    allowThisItem = isCommodityDay;
                                else
                                    allowThisItem = true;
                            }
                            break;
                        }
                    case CCFBGlobal.svcCat_NonFood:
                        {
                            allowThisItem = true;
                            isNonFoodItem = true;
                            break;
                        }
                    case CCFBGlobal.svcCat_BabySvc:
                        {
                            allowThisItem = true;
                            isBabyItem = true;
                            break;
                        }
                    default:
                        {
                            allowThisItem = true;
                            break;
                        }
                }
                if (allowThisItem)
                {
                    switch (Convert.ToInt32(drow["ItemRule"]))
                    {
                        case CCFBGlobal.itemRule_SpecialService:
                            {
                                if (IsOnSpecialServiceList(drow["ItemKey"].ToString()))
                                {   //If not a non-food item
                                    if (isNonFoodItem == true)
                                        addItem(drow, svcItemsNonFood);
                                    else if (isBabyItem == true)
                                        addItem(drow, svcItemsBabyService);
                                    else
                                        addItem(drow, svcItemsFood);
                                }
                                break;
                            }
                        default:
                            {   //If not a non-food item
                                if (isNonFoodItem)
                                    addItem(drow, svcItemsNonFood);
                                else if (isBabyItem == true)
                                    addItem(drow, svcItemsBabyService);
                                else
                                    addItem(drow, svcItemsFood);
                                break;
                            }
                    }
                }
            }
            CCFBGlobal.ServiceItemsChanged = false;
        }

        public void SetServiceDate(String NewServiceDate, bool NewIsCommodityDay, String NewSpecialSvcItemList, bool ShowAll)
        {
            if (CCFBGlobal.ServiceItemsChanged || NewServiceDate != ServiceDate || NewIsCommodityDay != isCommodityDay || NewSpecialSvcItemList != SpecialSvcItemList)
            {
                ServiceDate = NewServiceDate;
                isCommodityDay = NewIsCommodityDay;
                SpecialSvcItemList = NewSpecialSvcItemList;
                Refresh(ShowAll);
            }
        }

        /// <summary>
        /// Add the Item to the Food Items Collection
        /// </summary>
        /// <param name="rowIndex">Which Row in the dataset to use</param>
        /// <param name="svcItm">The list of service items</param>
        private void addItem(DataRow myDRow, List<ServiceItem> svcItm)
        {
            svcItm.Add(new ServiceItem(myDRow));
        }

        /// <summary>
        /// Test whether ItemKey in SpecialServiceList
        /// </summary>
        /// <param name="ItemKey"></param>
        /// <returns>True/False</returns>
        private bool IsOnSpecialServiceList(string ItemKey)
        {   //Gets the special items from the currently selected day
            string[] splitSpclItmString;

            if (SpecialSvcItemList != "" && SpecialSvcItemList != null)
            {       
                splitSpclItmString = SpecialSvcItemList.Split('|');
                for (int j = 0; j < splitSpclItmString.Length; j++)
                {
                    if (splitSpclItmString[j] == ItemKey)
                    { return true; }
                }
            }
            return false;
        }

        public void InitClientData(Client clsClient)
        {
            DateTime StartPeriod;
            DateTime EndPeriod;
            DateTime servicedate;
            string[] DateStuff = ServiceDate.Split('/');
            servicedate = Convert.ToDateTime(ServiceDate);
            ClientStuff.NoCommodity = clsClient.clsHH.NoCommodities;
            ClientStuff.SupplementalOnly = clsClient.clsHH.SupplOnly;
            ClientStuff.HaveCSFP = (clsClient.clsHH.NbrCSFP > 0);
            ClientStuff.FamilySize = clsClient.clsHH.TotalFamily;
            ClientStuff.Homeless = clsClient.clsHH.Homeless;
            ClientStuff.Transient = (clsClient.clsHH.ClientType == CCFBPrefs.TransientId);
            ClientStuff.NbrServicesThisCalYr = 0;
            ClientStuff.NbrServicesThisFiscalYr = 0;
            ClientStuff.NbrServicesThisMonth = 0;
            ClientStuff.NbrServicesThisWeek = 0;
            ClientStuff.NbrSupplThisMonth = 0;
            ClientStuff.NbrTEFAPThisMonth = 0;
            ClientStuff.NbrFullSvcThisMonth = 0;
            ClientStuff.LastFullService = clsClient.clsHH.LatestService;
            ClientStuff.LastTEFAPService = clsClient.clsHH.LastCommodityService;
            ClientStuff.LastSupplService = clsClient.clsHH.LastSupplService;

            TrxLog trxLogWork = new TrxLog(CCFBGlobal.connectionString,true,true,false,false);
            //Nbr Service This Month
            StartPeriod = Convert.ToDateTime(DateStuff[0].ToString() + "/01/" + DateStuff[2].ToString());
            EndPeriod = servicedate.AddDays(-1);
            trxLogWork.openUsingDateRange(clsClient.clsHH.ID, StartPeriod, EndPeriod);
            ClientStuff.NbrServicesThisMonth = trxLogWork.RowCount;
            for (int i = 0; i < trxLogWork.RowCount; i++)
            {
                trxLogWork.setDataRow(i);
                if (trxLogWork.RcvdCommodity == true)
                    ClientStuff.NbrTEFAPThisMonth++;
                if (trxLogWork.RcvdSupplemental == true)
                    ClientStuff.NbrSupplThisMonth++;
                if (trxLogWork.FullService == true)
                    ClientStuff.NbrFullSvcThisMonth++;
            }
            //Nbr Services This Fiscal Year
            StartPeriod = CCFBGlobal.CalcFiscalStartDate(Convert.ToDateTime(ServiceDate));
            EndPeriod = servicedate;
            trxLogWork.openUsingDateRange(clsClient.clsHH.ID, StartPeriod, EndPeriod);
            ClientStuff.NbrServicesThisFiscalYr = trxLogWork.RowCount;
            //Nbr Services This Calendar Year
            if (CCFBPrefs.FiscalYearStartMonth != 1)
            {
                StartPeriod = Convert.ToDateTime("01/01/" + DateStuff[2].ToString());
                EndPeriod = servicedate;
                trxLogWork.openUsingDateRange(clsClient.clsHH.ID, StartPeriod, EndPeriod);
            }
            ClientStuff.NbrServicesThisCalYr = trxLogWork.RowCount;
            int dayofweek = Convert.ToInt32(servicedate.DayOfWeek);
            StartPeriod = servicedate.AddDays(-dayofweek);
            EndPeriod = servicedate.AddDays(-1);
            trxLogWork.openUsingDateRange(clsClient.clsHH.ID, StartPeriod, EndPeriod);
            ClientStuff.NbrServicesThisWeek = trxLogWork.RowCount;
            ClearSelected();
        }
        public void fillListViewItems(ListView lvFoodItems, ListView lvNonFood, ListView lvBabySvcs, bool bShowAllFoodSvcItems)
        {
            //Clear the items out of each checkbox
            lvFoodItems.Items.Clear();
            lvNonFood.Items.Clear();
            lvBabySvcs.Items.Clear();
            bool haveexclusive = false;

            //Loop through todays Food items, check the rules, and add and set checks for the aprropriate items
            foreach (ServiceItem svc in svcItemsFood)
            {
                ListViewItem lvItem = new ListViewItem();
                lvItem.Text = "";
                lvItem.Checked = svc.IsSelected; 
                lvItem.SubItems.Add(svc.Description);
                lvItem.SubItems.Add(svc.ItemKey.ToString());
                lvItem.Tag = svc.ItemKey;
                if (ClientStuff.SupplementalOnly) 
                {
                    if (svc.ItemType == CCFBGlobal.svcCat_Supplemental && TestRule(svc))
                    {
                        lvFoodItems.Items.Add(lvItem);
                        if (svc.Exclusive == true && svc.IsSelected == true)
                            haveexclusive = true;
                    }
                }
                else
                {
                    if (bShowAllFoodSvcItems == true)
                    {
                        lvFoodItems.Items.Add(lvItem);
                    }
                    else
                    {
                        switch (svc.ItemType)
                        {
                            case CCFBGlobal.svcCat_Commodity:
                                {
                                    if (ClientStuff.NoCommodity == false && TestRule(svc))
                                    { lvFoodItems.Items.Add(lvItem); }
                                    break;
                                }
                            default:
                                {
                                    if (TestRule(svc))
                                    {
                                        lvFoodItems.Items.Add(lvItem);
                                        if (svc.Exclusive == true && svc.IsSelected == true)
                                            haveexclusive = true;
                                    }
                                    break;
                                }
                        }
                    }
                }
            }
            if (haveexclusive == true)
            {
                foreach (ServiceItem svc in svcItemsFood)
                {
                    if (svc.Exclusive == false && svc.IsSelected)
                    {
                        svc.IsSelected = false;
                        foreach (ListViewItem lvi in lvFoodItems.Items)
                        {
                            if (lvi.SubItems[2].Text == svc.ItemKey.ToString())
                            {
                                lvi.Checked = false;
                                break;
                            }
                        }
                    }
                }
            }
            //Go through the non-food items collection and find which to add and set
            foreach (ServiceItem svc in svcItemsNonFood)
            {
                ListViewItem lvItem = new ListViewItem();
                lvItem.Text = "";
                lvItem.Checked = svc.IsSelected;
                lvItem.SubItems.Add(svc.Description);
                lvItem.SubItems.Add(svc.ItemKey.ToString());
                //Check the rule for the non-food item
                if (TestRule(svc))
                {
                    //Add it to the check list box, set the check to true
                    lvNonFood.Items.Add(lvItem);
                }
            }
            //Go through the non-food items collection and find which to add and set
            foreach (ServiceItem svc in svcItemsBabyService)
            {
                ListViewItem lvItem = new ListViewItem();
                lvItem.Text = "";
                lvItem.Checked = svc.IsSelected;
                lvItem.SubItems.Add(svc.Description);
                lvItem.SubItems.Add(svc.ItemKey.ToString());
                //Check the rule for the non-food item
                if (TestRule(svc))
                {
                    //Add it to the check list box, set the check to true
                    lvBabySvcs.Items.Add(lvItem);
                }
            }
        }

        private bool TestRule(ServiceItem svc)
        {
            if (ClientStuff.SupplementalOnly == false || svc.ItemType == CCFBGlobal.svcCat_Supplemental)
            {
                switch (Int32.Parse(svc.Rule.ToString()))
                {
                    case CCFBGlobal.itemRule_Always:           //Case Always
                        { return true; }
                    case CCFBGlobal.itemRule_OncePerMonth:     //Case Once Per Month
                        {
                            int nbrSvcs =0;
                            if (svc.FullService == true)
                                nbrSvcs = ClientStuff.NbrFullSvcThisMonth;
                            else if (svc.ItemType == CCFBGlobal.svcCat_Commodity)
                                nbrSvcs = ClientStuff.NbrTEFAPThisMonth;
                            else
                                nbrSvcs = ClientStuff.NbrServicesThisMonth;
                            return (nbrSvcs == 0);
                        }
                    case CCFBGlobal.itemRule_SecondService:    //Case Second Service
                        { return (ClientStuff.NbrServicesThisMonth > 0); }
                    case CCFBGlobal.itemRule_ManualSelection:  //Manual Selection
                        { return true; }
                    case CCFBGlobal.itemRule_SpecialService:   //Special Service
                        { return true; }
                    case CCFBGlobal.itemRule_HomelessTransient:
                        {
                            return (ClientStuff.Homeless || ClientStuff.Transient);
                        }
                    case CCFBGlobal.itemRule_MaskArray:
                        {
                            return svc.MaskArray(ClientStuff.NbrServicesThisMonth);
                        }
                    case CCFBGlobal.itemRule_OncePerWeek:
                        {
                            return (ClientStuff.NbrServicesThisWeek == 0);
                        }
                    case CCFBGlobal.itemRule_2Months:     //Case Every 2 Months
                        {
                            return TestNbrMonthSinceLastService(-2);
                        }
                    case CCFBGlobal.itemRule_3Months:     //Case Every 3 Months
                        {
                            return TestNbrMonthSinceLastService(-3);
                        }
                    case CCFBGlobal.itemRule_4Months:     //Case Every 4 Months
                        {
                            return TestNbrMonthSinceLastService(-4);
                        }
                }
            }
            return false;

        }

        /// <summary>
        /// Takes a service Item and checks it rule and enforces the rule
        /// </summary>
        /// <param name="svc">Service Item</param>
        /// <returns>Whether or not the item passed the rule</returns>
        public bool checkRule(ServiceItem svc)
        {
            if (ClientStuff.SupplementalOnly == false && svc.ItemType != CCFBGlobal.svcCat_Supplemental)
            {
                switch (Int32.Parse(svc.Rule.ToString()))
                {
                    case CCFBGlobal.itemRule_Always:           //Case Always
                        {
                            if (svc.ItemType == CCFBGlobal.svcCat_Commodity)
                            {
                                return !ClientStuff.NoCommodity;
                            }
                            return true; 
                        }
                    case CCFBGlobal.itemRule_OncePerMonth:     //Case Once Per Month
                        {
                            int nbrSvcs = 0;
                            if (svc.FullService == true)
                                nbrSvcs = ClientStuff.NbrFullSvcThisMonth;
                            else if (svc.ItemType == CCFBGlobal.svcCat_Commodity)
                                nbrSvcs = ClientStuff.NbrTEFAPThisMonth;
                            else
                                nbrSvcs = ClientStuff.NbrServicesThisMonth;
                            return (nbrSvcs == 0);
                        }
                    case CCFBGlobal.itemRule_SecondService:    //Case Second Service
                        { return (ClientStuff.NbrServicesThisMonth > 0); }
                    case CCFBGlobal.itemRule_ManualSelection:  //Manual Selection
                        { return svc.IsSelected; }
                    case CCFBGlobal.itemRule_SpecialService:   //Special Service
                        { return true; }
                    case CCFBGlobal.itemRule_OncePerWeek:
                        {
                            return (ClientStuff.NbrServicesThisWeek == 0);
                        }
                    case CCFBGlobal.itemRule_HomelessTransient:
                        {
                            return (ClientStuff.Homeless || ClientStuff.Transient);
                        }
                    case CCFBGlobal.itemRule_MaskArray:
                        {
                            return svc.MaskArray(ClientStuff.NbrServicesThisMonth);
                        }
                    case CCFBGlobal.itemRule_2Months:     //Case Every 2 Months
                        {
                            return TestNbrMonthSinceLastService(-2);
                        }
                    case CCFBGlobal.itemRule_3Months:     //Case Every 3 Months
                        {
                            return TestNbrMonthSinceLastService(-3);
                        }
                    case CCFBGlobal.itemRule_4Months:     //Case Every 4 Months
                        {
                            return TestNbrMonthSinceLastService(-4);
                        }
                }
            }
            else if (svc.ItemType == CCFBGlobal.svcCat_Supplemental)
            {
                return (ClientStuff.SupplementalOnly || svc.IsSelected);
            }
            return false;

        }

        public bool TestNbrMonthSinceLastService(int increment)
        {
            DateTime dateOldest = Convert.ToDateTime(CCFBGlobal.DefaultServiceDate).AddMonths(increment);
            if (ClientStuff.LastFullService.CompareTo(dateOldest) < 1)
            { return true; }
            else
            { return false; }
        }
    }
}
