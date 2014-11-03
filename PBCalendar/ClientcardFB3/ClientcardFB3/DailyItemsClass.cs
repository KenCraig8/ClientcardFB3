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

        string[] splitSpclItmString;    //Used when spliting on '|'  for the special items for a service day

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
        public void Refresh()
        {
            bool allowThisItem = false;
            bool isNonFoodItem = false;
            bool isBabyItem = false;

            svcItemsFood.Clear();
            svcItemsNonFood.Clear();
            svcItemsBabyService.Clear();
            clsServiceItems.openWhere("");
            foreach (DataRow drow in clsServiceItems.DSet.Tables[0].Rows)
            {
                isNonFoodItem = false;
                isBabyItem = false;
                switch (Int32.Parse(drow["ItemType"].ToString()))
                {
                    case CCFBGlobal.svcCat_Commodity:
                        {
                            allowThisItem = isCommodityDay || CCFBPrefs.MustBeACommodityDay;
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

        public void SetServiceDate(String NewServiceDate, bool NewIsCommodityDay, String NewSpecialSvcItemList)
        {
            if (CCFBGlobal.ServiceItemsChanged || NewServiceDate != ServiceDate || NewIsCommodityDay != isCommodityDay || NewSpecialSvcItemList != SpecialSvcItemList)
            {
                ServiceDate = NewServiceDate;
                isCommodityDay = NewIsCommodityDay;
                SpecialSvcItemList = NewSpecialSvcItemList;
                Refresh();
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
            string[] DateStuff = ServiceDate.Split('/');
            ClientStuff.NoCommodity = clsClient.clsHH.NoCommodities;
            ClientStuff.SupplementalOnly = clsClient.clsHH.SupplOnly;
            ClientStuff.HaveCSFP = (clsClient.clsHH.NbrCSFP > 0);
            ClientStuff.FamilySize = clsClient.clsHH.TotalFamily;
            ClientStuff.Homeless = clsClient.clsHH.Homeless;
            ClientStuff.Transient = (clsClient.clsHH.ClientType == CCFBPrefs.TransientId);

            TrxLog trxLogWork = new TrxLog(CCFBGlobal.connectionString);
            //Nbr Service This Month
            StartPeriod = Convert.ToDateTime(DateStuff[0].ToString() + "/01/" + DateStuff[2].ToString());
            EndPeriod = StartPeriod.AddMonths(1).AddDays(-1);
            trxLogWork.openUsingDateRange(clsClient.clsHH.ID, StartPeriod, EndPeriod);
            ClientStuff.NbrServicesThisMonth = trxLogWork.RowCount;
            //Nbr Services This Fiscal Year
            StartPeriod = CCFBGlobal.CalcFiscalStartDate(Convert.ToDateTime(ServiceDate));
            EndPeriod = CCFBGlobal.CalcFiscalEndDate(Convert.ToDateTime(ServiceDate));
            trxLogWork.openUsingDateRange(clsClient.clsHH.ID, StartPeriod, EndPeriod);
            ClientStuff.NbrServicesThisFiscalYr = trxLogWork.RowCount;
            //Nbr Services This Calendar Year
            if (CCFBPrefs.FiscalYearStartMonth != 1)
            {
                StartPeriod = Convert.ToDateTime("01/01/" + DateStuff[2].ToString());
                EndPeriod = StartPeriod.AddYears(1).AddDays(-1);
                trxLogWork.openUsingDateRange(clsClient.clsHH.ID, StartPeriod, EndPeriod);
            }
            ClientStuff.NbrServicesThisCalYr = trxLogWork.RowCount;
            ClearSelected();
        }
        public void fillListViewItems(ListView lvFoodItems, ListView lvNonFood, ListView lvBabySvcs)
        {
            //Clear the items out of each checkbox
            lvFoodItems.Items.Clear();
            lvNonFood.Items.Clear();
            lvBabySvcs.Items.Clear();

            //Loop through todays Food items, check the rules, and add and set checks for the aprropriate items
            foreach (ServiceItem svc in svcItemsFood)
            {
                ListViewItem lvItem = new ListViewItem();
                lvItem.Text = "";
                lvItem.Checked = svc.IsSelected; 
                lvItem.SubItems.Add(svc.Description);
                lvItem.SubItems.Add(svc.ItemKey.ToString());
                if (ClientStuff.SupplementalOnly) 
                {
                    if (svc.ItemType == CCFBGlobal.svcCat_Supplemental && TestRule(svc) )
                        { lvFoodItems.Items.Add(lvItem); }
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
                                { lvFoodItems.Items.Add(lvItem); }
                            break; 
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
                            if (ClientStuff.NbrServicesThisMonth == 0)
                            { return true; }
                            else
                            { return false; }
                        }
                    case CCFBGlobal.itemRule_SecondService:    //Case Second Service
                        { return (ClientStuff.NbrServicesThisMonth > 0); }
                    case CCFBGlobal.itemRule_ManualSelection:  //Manual Selection
                        { return true; }
                    case CCFBGlobal.itemRule_SpecialService:   //Special Service
                        { return true; }
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
                        { return true; }
                    case CCFBGlobal.itemRule_OncePerMonth:     //Case Once Per Month
                        {
                            if (ClientStuff.NbrServicesThisMonth == 0)
                            { return true; }
                            else
                            { return false; }
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
                }
            }
            else if (svc.ItemType == CCFBGlobal.svcCat_Supplemental)
            {
                return (ClientStuff.SupplementalOnly || svc.IsSelected);
            }
            return false;

        }

        /// <summary>
        /// Takes a service Item rule and enforces the rule for that item
        /// </summary>
        /// <param name="rule">A string value of the integer for that rule</param>
        /// <returns>Whether or not the service item passed the rule</returns>
        public bool checkRule(string rule)
        {
            switch (Int32.Parse(rule))
            {
                case CCFBGlobal.itemRule_Always:   //Case ALways
                    {
                        return true;
                    }
                case CCFBGlobal.itemRule_OncePerMonth:   //Case Once Per Month
                    {
                        if (ClientStuff.NbrServicesThisMonth ==0)
                        {
                            return true;
                        }
                        return false;
                    }
                case CCFBGlobal.itemRule_SecondService:   //Case Second Service
                    {
                        if (ClientStuff.NbrServicesThisMonth > 0)
                        {
                            return true;
                        }
                        return false;
                    }

                //How do I know if it is a manual selection or not?
                case CCFBGlobal.itemRule_ManualSelection:
                    {                
                        return false;
                    }
                case CCFBGlobal.itemRule_SpecialService:
                    {
                        return true;
                    }
            }
            return false;
        }
    }
}
