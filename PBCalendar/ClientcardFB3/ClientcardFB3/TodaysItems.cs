using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

namespace ClientCardFB3
{
    class TodaysItems
    {
        List<ServiceItem> FoodItems;
        List<ServiceItem> NonFoodItems;

        DaysOpen clsDaysOpen;
        ServiceItems clsServiceItems;
        Client clsClient;


        //The Transaction date to use
        DateTime transDate = DateTime.Today;

        DateTime beginFiscalYear = new DateTime(DateTime.Today.Year, 7, 1);
        DateTime endFiscalYear = new DateTime(DateTime.Today.Year, 6, 30);
        DateTime beginCalYear = new DateTime(DateTime.Today.Year, 1, 1);
        DateTime endCalYear = new DateTime(DateTime.Today.Year, 12, 31);
        DateTime beginMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
        DateTime endMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 
        DateTime.DaysInMonth(DateTime.Today.Year ,DateTime.Today.Month));

        bool firstFoodItemDescSet = false;  //Used to help fill food sevice string
        bool firstNonFoodItemDescSet = false;   //Used to help fill non-food sevice string
        
        string svcList = "";     //Food Service String
        string nonFoodSvcList = "";  //Non-Food Service String

        string[] splitSpclItmString;    //Used when spliting on '|'  for the special items for a service day

        //Lbs Buckets
        int lbsThroughOther = 0;
        int lbsStandard = 0;
        int lbsCommodity = 0;
        int lbsCSFP = 0;
        int lbsSupp = 0;

        //Client Data Structure
        struct ClientData
        {
            internal bool SupplementalOnly;
            internal bool NoCommodity;
            internal int FamilySize;
        }

        ClientData ClientStuff;

        #region Get/Set Accessors

        public bool FirstNonFoodItemDescSet
        {
            get { return firstNonFoodItemDescSet; }
            set { firstNonFoodItemDescSet = value; }
        }

        public bool FirstFoodItemDescSet
        {
            get { return firstFoodItemDescSet; }
            set { firstFoodItemDescSet = value; }
        }

        public string FoodSvcList
        {
            get { return svcList; }
            set { svcList = value; }
        }

        public string NonFoodSvcList
        {
            get { return nonFoodSvcList; }
            set { nonFoodSvcList = value; }
        }

        public int LbsStandard
        {
            get { return lbsStandard; }
            set { lbsStandard = value; }
        }

        public int LbsOther
        {
            get { return lbsThroughOther; }
            set { lbsThroughOther = value; }
        }

        public int LbsCommodity
        {
            get { return lbsCommodity; }
            set { lbsCommodity = value; }
        }

        public int LbsSupplemental
        {
            get { return lbsSupp; }
            set { lbsSupp = value; }
        }

        public int LbsCSFP
        {
            get { return lbsCSFP; }
            set { lbsCSFP = value; }
        }
        #endregion

        public TodaysItems(Client clsClientIn)
        {
            clsDaysOpen = new DaysOpen(CCFBGlobal.connectionString);
            clsServiceItems = new ServiceItems(CCFBGlobal.connectionString);
            clsClient = clsClientIn;
            setForToday();
        }

        public void setForToday()
        {
            clsServiceItems.openAll();

            //Opens the last 15 and next 5 service dates from today
            clsDaysOpen.openTopTwentyWithinDate(DateTime.Today);

            FoodItems = new List<ServiceItem>();
            NonFoodItems = new List<ServiceItem>();

            findSvcDate();  //Figure out what the best service date to use is
            fillItemsList();    //Fill the service items collections 

            firstFoodItemDescSet = false;
        }

        /// <summary>
        /// Goes through the top 20 dates, finds the one closest to today
        /// without going over and sets the SvcDate to that date
        /// </summary>
        private void findSvcDate()
        {
            DateTime svcDate = DateTime.Today;
            for (int i = clsDaysOpen.RowCount-1; i > 0; i--)
            {
                DateTime currPointer = DateTime.Parse(clsDaysOpen.DSet.Tables[0].Rows[i]["Date"].ToString());
                if (currPointer <= DateTime.Today)
                    svcDate = currPointer;
            }

            clsDaysOpen.openWhere(" Date = '" + svcDate + "'");
            beginMonth = new DateTime(svcDate.Year, svcDate.Month, 1);
            endMonth = new DateTime(svcDate.Year, svcDate.Month,
                DateTime.DaysInMonth(svcDate.Year, svcDate.Month));
        }

        /// <summary>
        /// Allows the svcDate to be set inside or outside of the class
        /// </summary>
        /// <param name="svcDate">(DateTime) The Service Date</param>
        public void setServiceDate(DateTime svcDate)
        {
            clsDaysOpen.openWhere(" Date = '" + svcDate + "'");
        }


        /// <summary>
        /// Used after the lbs holders are all set, Resets Service list 
        /// and then sends each to be added to the SvcList 
        /// </summary>
        public void fillSvcList()
        {
            svcList = "";
            firstFoodItemDescSet = false;
            if (lbsStandard != 0)
                addToSrvcList("Standard", lbsStandard);

            if (lbsThroughOther != 0)
                addToSrvcList("Other", lbsThroughOther);

            if (lbsCommodity != 0)
                addToSrvcList("Commodity", lbsCommodity);

            if (lbsSupp != 0)
                addToSrvcList("Supplemental", lbsSupp);

            if (lbsCSFP != 0)
                addToSrvcList("CSFP", lbsCSFP);            
        }

        /// <summary>
        /// This funtion is used whenever you want to set recived only supplemental
        /// Resets all lbs holders to 0 and calulates the supplemental items lbs
        /// </summary>
        public void setSupplementalOnlyTrue()
        {
            //Reset all lbs holders to 0
            lbsSupp = 0;
            lbsCommodity = 0;
            lbsCSFP = 0;
            lbsStandard = 0;
            lbsThroughOther = 0;

            //Go through the Food Items List
            foreach (ServiceItem svcItm in FoodItems)
            {
                //ItemType = 4 is supplemental
                if (svcItm.ItemType == CCFBGlobal.itemType_Supplemental)
                {
                    lbsSupp += svcItm.getFamSizeMultiplyer(clsClient.clsHH.TotalFamily) * svcItm.LbsPerItem;
                }
            }
        }

        public void fillCheckBoxLists(CheckedListBox foodBox, CheckedListBox nonFoodBox, bool forEdit)
        {
            //Clear the items out of each checkbox
            foodBox.Items.Clear();
            nonFoodBox.Items.Clear();

            //Loop through todays Food items, check the rules, and add and set checks for the aprropriate items
            foreach (ServiceItem svc in FoodItems)
            {
              //If the item is not a commodity item
                if (svc.ItemType != CCFBGlobal.itemType_Commodity)
                {
                    //Add the item to the check list box
                    foodBox.Items.Add(svc.Description);

                    //Check the rule anginst the client or just pass it by if it is manual selection
                    if (checkRule(svc) == true)
                    {
                            foodBox.SetItemChecked(foodBox.Items.Count - 1, true);
                    }
                }
                else  //The Item is a Commodity Item
                {
                    //Check to make sure today is a commodity day
                    if ((clsDaysOpen.IsCommodity == true && clsClient.clsHH.NoCommodities == false)
                        || forEdit == true)
                    {
                        //Add to Check List Box
                        foodBox.Items.Add(svc.Description);

                        //Check rule for the commodity
                        if (checkRule(svc) == true)
                        {
                            //Set check    
                            foodBox.SetItemChecked(foodBox.Items.Count - 1, true);
                        }
                    }
                }
            }

            firstNonFoodItemDescSet = false;

            //Go through the non-food items collection and find which to add and set
            foreach (ServiceItem svc in NonFoodItems)
            {
                //Check the rule for the non-food item
                if (checkRule(svc) == true)
                {
                    //Add it to the check list box, set the check to true
                    nonFoodBox.Items.Add(svc.Description);
                    nonFoodBox.SetItemChecked(nonFoodBox.Items.Count - 1, true);

                    //Add it to the non-food service list
                    addToNonFoodSevcList(svc.Description);
                }
            }
            firstNonFoodItemDescSet = false;
        }

        /// <summary>
        /// Calculates how many pounds are in each bucket for the food service items
        /// </summary>
        /// <param name="clsClientIn">(Class) The Client</param>
        public void calcPounds(Client clsClientIn)
        {
            clsClient = clsClientIn;

            lbsThroughOther = 0;
            lbsStandard = 0;
            lbsCommodity = 0;
            lbsCSFP = 0;
            lbsSupp = 0;

            //Go through each Service Item, Check rules, and add to appropriate bucket if passed
            foreach (ServiceItem svcItm in FoodItems)
            {
                switch (svcItm.ItemType)
                {
                    case CCFBGlobal.itemType_Standard:   //1 = Standard
                        {
                            if (checkRule(svcItm) == true)
                            {
                                lbsStandard += svcItm.getFamSizeMultiplyer(clsClient.clsHH.TotalFamily) * svcItm.LbsPerItem;
                            }
                            break;
                        }
                    case CCFBGlobal.itemType_Other:   //2 = other
                        {
                            if (checkRule(svcItm) == true)
                            {
                                lbsThroughOther += svcItm.getFamSizeMultiplyer(clsClient.clsHH.TotalFamily) * svcItm.LbsPerItem;
                            }
                            break;
                        }
                    case CCFBGlobal.itemType_Commodity:   //3 = Commodity
                        {
                            if (clsDaysOpen.isValid == true && clsDaysOpen.IsCommodity == true 
                                && clsClient.clsHH.NoCommodities == false && checkRule(svcItm) == true)
                            {
                                lbsCommodity += svcItm.getFamSizeMultiplyer(clsClient.clsHH.TotalFamily) * svcItm.LbsPerItem;
                            }
                            break;
                        }
                    case CCFBGlobal.itemType_Supplemental: 
                        {
                            if (ClientStuff.SupplementalOnly == true)
                            {
                                lbsSupp += svcItm.getFamSizeMultiplyer(clsClient.clsHH.TotalFamily) * svcItm.LbsPerItem;
                            }
                            break;
                        }
                }
            }
        }

        /// <summary>
        ///  Calculates how many pounds are in each bucket for the food service items
        ///  using a different family size than the one in the household
        ///  This family size was set manually in NewServiceForm
        /// </summary>
        /// <param name="totalFamilySize">(int) The total family size</param>
        public void calcPounds(int totalFamilySize)
        {
            //Reset all buckets
            lbsThroughOther = 0;
            lbsStandard = 0;
            lbsCommodity = 0;
            lbsCSFP = 0;
            lbsSupp = 0;

            //Go through each Service Item, Check rules, and add to appropriate bucket if passed
            foreach (ServiceItem svcItm in FoodItems)
            {
                //2 = other
                switch (svcItm.ItemType)
                {
                    case CCFBGlobal.itemType_Standard:   //1 = Standard
                        {
                            if (checkRule(svcItm) == true)
                            {
                                lbsStandard += svcItm.getFamSizeMultiplyer(totalFamilySize) * svcItm.LbsPerItem;
                            }
                            break;
                        }
                    case CCFBGlobal.itemType_Other:   //2 = Other
                        {
                            if (checkRule(svcItm) == true)
                            {
                                lbsThroughOther += svcItm.getFamSizeMultiplyer(totalFamilySize) * svcItm.LbsPerItem;
                            }
                            break;
                        }
                    case CCFBGlobal.itemType_Commodity:   //3 = Commodity
                        {
                            if (clsDaysOpen.isValid == true && clsDaysOpen.IsCommodity == true
                                && checkRule(svcItm) == true)
                            {
                                lbsCommodity += svcItm.getFamSizeMultiplyer(totalFamilySize) * svcItm.LbsPerItem;
                            }
                            break;
                        }
                    case CCFBGlobal.itemType_Supplemental:
                        {
                            if (ClientStuff.SupplementalOnly == true)
                            {
                                lbsSupp += svcItm.getFamSizeMultiplyer(totalFamilySize) * svcItm.LbsPerItem;
                            }
                            break;
                        }
                }
            }
        }

        /// <summary>
        /// Adds or subtracts from the Lbs for the ItemType
        /// </summary>
        /// <param name="description">What bucket does the item fall into (LbsStd, LbsOther, ect...)</param>
        /// <param name="totalFamilySize">The size of the family</param>
        /// <param name="checkState">Whehter or not the item was checked or unchecked</param>
        public void setSvcItemManualySelcted(string description, int totalFamilySize, bool checkState)
        {
            if (checkState == true) //We need to add that items lbs to the appropriate buckett
            {
                //Find appropraite item
                foreach (ServiceItem svcItm in FoodItems)
                {
                    if (svcItm.Description == description)
                    {
                        switch (svcItm.ItemType)
                        {
                            case CCFBGlobal.itemType_Standard:   //Standard
                                {
                                    lbsStandard += svcItm.getFamSizeMultiplyer(totalFamilySize) * svcItm.LbsPerItem;
                                    break;
                                }
                            case CCFBGlobal.itemType_Other:   //Other
                                {
                                    lbsThroughOther += svcItm.getFamSizeMultiplyer(totalFamilySize) * svcItm.LbsPerItem;
                                    break;
                                }
                            case CCFBGlobal.itemType_Commodity:   //Commodity
                                {
                                    lbsCommodity += svcItm.getFamSizeMultiplyer(totalFamilySize) * svcItm.LbsPerItem;
                                    break;
                                }
                        }
                    }
                }
            }
            else  //We need to subrtact that item off the appropriate buckett
            {
                foreach (ServiceItem svcItm in FoodItems)
                {
                    //Finds the correct service item
                    if (svcItm.Description == description)
                    {
                        switch (svcItm.ItemType)
                        {
                            case CCFBGlobal.itemType_Standard:   //Case StdService
                                {
                                    if (lbsStandard < 0)
                                        lbsStandard = 0;
                                    lbsStandard -= svcItm.getFamSizeMultiplyer(totalFamilySize) * svcItm.LbsPerItem;
                                    break;
                                }
                            case CCFBGlobal.itemType_Other:   //Case other
                                {
                                    if (lbsThroughOther < 0)
                                        lbsThroughOther = 0;
                                    lbsThroughOther -= svcItm.getFamSizeMultiplyer(totalFamilySize) * svcItm.LbsPerItem;
                                    break;
                                }
                            case CCFBGlobal.itemType_Commodity:   //Case Commodity
                                {
                                    lbsCommodity -= svcItm.getFamSizeMultiplyer(totalFamilySize) * svcItm.LbsPerItem;
                                    if (lbsCommodity < 0)
                                        lbsCommodity = 0;

                                    break;
                                }
                        }
                    }
                }
            }
        }

        public static int getWeekNumber(DateTime dt)
        {
            return CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(dt, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
        }

        /// <summary>
        /// Checks if a service has been given within to transaction date's month
        /// </summary>
        /// <returns>Returns True if a service has already been given this month</returns>
        private bool checkforSecondService()
        {
            for (int i = 0; i < clsClient.clsHHSvcTrans.DSet.Tables[0].Rows.Count; i++)
            {
                if (((DateTime)clsClient.clsHHSvcTrans.DSet.Tables[0].Rows[i]["TrxDate"] > beginMonth
                    && (DateTime)clsClient.clsHHSvcTrans.DSet.Tables[0].Rows[i]["TrxDate"] < endMonth))
                {
                    return true;
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
            if (ClientStuff.SupplementalOnly == false || svc.ItemType == CCFBGlobal.itemType_Supplemental)
            {
                DateTime pointer = transDate;

                switch (Int32.Parse(svc.Rule.ToString()))
                {
                    case CCFBGlobal.itemRule_Always:           //Case Always
                        { return true; }
                    case CCFBGlobal.itemRule_OncePerMonth:     //Case Once Per Month
                        {
                            if (checkforSecondService() == false)
                                { return true; }
                            else 
                                { return false; }
                        }
                    case CCFBGlobal.itemRule_SecondService:    //Case Second Service
                        { return checkforSecondService(); }
                    case CCFBGlobal.itemRule_ManualSelection:  //Manual Selection
                        { return false; }
                    case CCFBGlobal.itemRule_SpecialService:   //Special Service
                        { return true; }
                }
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
            DateTime pointer = transDate;

            switch (Int32.Parse(rule))
            {
                case CCFBGlobal.itemRule_Always:   //Case ALways
                    {
                        return true;
                    }
                case CCFBGlobal.itemRule_OncePerMonth:   //Case Once Per Month
                    {
                        if (checkforSecondService() == false)
                        {
                            return true;
                        }
                        return false;
                    }
                case CCFBGlobal.itemRule_SecondService:   //Case Second Service
                    {
                        if (checkforSecondService() == true)
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

        /// <summary>
        /// Adds the description and the lbs to the service list
        /// </summary>
        /// <param name="desc">The string description of the buckett (LbsStandard, LbsOther, ect...)</param>
        /// <param name="lbs">Amount of pounds for that bucket</param>
        private void addToSrvcList(string desc, int lbs)
        {
            if (firstFoodItemDescSet == false)
            {
                svcList = lbs.ToString() + " " + desc;
                firstFoodItemDescSet = true;
            }
            else
            {
                svcList += ", " + lbs.ToString() + " " + desc;
            }
        }

        /// <summary>
        /// Adds the item to the non-food service list
        /// </summary>
        /// <param name="desc">The description of the service item (string value)</param>
        public void addToNonFoodSevcList(string desc)
        {
            if (firstNonFoodItemDescSet == false)
            {
                nonFoodSvcList = desc;
                firstNonFoodItemDescSet = true;
            }
            else
            {
                nonFoodSvcList += ", " + desc;
            }
        }

        /// <summary>
        /// Used when the TrxDate is changed
        /// </summary>
        /// <param name="newDate">The TrxDate being set</param>
        public void refillList(DateTime newDate)
        {
            clsDaysOpen.openWhere(" Date = '" + newDate + "'");

            beginMonth = new DateTime(newDate.Year, newDate.Month, 1);
            endMonth = new DateTime(newDate.Year, newDate.Month,
                DateTime.DaysInMonth(newDate.Year, newDate.Month));

            fillItemsList();
            calcPounds(clsClient);
            fillSvcList();
        }

        /// <summary>
        /// Fills Todays Service Items into the appropriate lists
        /// </summary>
        public void fillItemsList()
        {
            FoodItems.Clear();
            NonFoodItems.Clear();

            if (clsDaysOpen.isValid == true)
            {
                for (int i = 0; i < clsServiceItems.DSet.Tables[0].Rows.Count; i++)
                {
                    //If not a special service
                    if (Convert.ToInt32 (clsServiceItems.DSet.Tables[0].Rows[i]["ItemRule"]) != CCFBGlobal.itemRule_SpecialService)
                    {   //If not a non-food item
                        if (Convert.ToInt32(clsServiceItems.DSet.Tables[0].Rows[i]["ItemType"]) != CCFBGlobal.itemType_NonFood)
                        {
                            addItem(i, FoodItems);
                        }
                        else
                            addItem(i, NonFoodItems);
                    }
                    else
                    {   //Gets the special items from the currently selected day
                        string specialItemsString = clsDaysOpen.SpecialItems;

                        if (specialItemsString != null)
                        {
                            //Splits on | to get all the special items
                            splitSpclItmString = specialItemsString.Split('|');
                            //For each special items
                            for (int j = 0; j < splitSpclItmString.Length; j++)
                            {
                                if (splitSpclItmString[j] == clsServiceItems.DSet.Tables[0].Rows[i]["ItemKey"].ToString())
                                {
                                    //If this is a food item
                                    if (clsServiceItems.DSet.Tables[0].Rows[i]["ItemType"].ToString() != "6")
                                        addItem(i, FoodItems);
                                    else //It is a non-food item
                                        addItem(i, NonFoodItems);
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Add the Item to the Food Items Collection
        /// </summary>
        /// <param name="rowIndex">Which Row in the dataset to use</param>
        /// <param name="svcItm">The list of service items</param>
        private void addItem(int rowIndex, List<ServiceItem> svcItm)
        {
            svcItm.Add(new ServiceItem(clsServiceItems.DSet.Tables[0].Rows[rowIndex]));
        }

    }
}
