using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Data;

namespace ClientcardFB3
{
    #region TrxLogItem Class
    public class TrxLogItem
    {
        DataRow drow;

        public enum SvcMethod
        {
            Pickup = 0,
            HomeDelivery = 1
        };

//---------------------------------------Constructor-----------------------------------------------------
/// <summary>
/// Initializes all local data members of class
/// </summary>
/// <param name="connectString">connectString = Connection String</param>
        
        public TrxLogItem(DataRow drowIn)
        {
        	drow = drowIn;
        }

        public TrxLogItem(DataRow drowIn, Household clshh, HHMembers clshhm, string HHmemNameIn, SvcMethod svcMethod)
        {
            Household Hh = clshh;
            HHMembers hhm = clshhm;
            drow = drowIn;
            //Initialize new Item
            TrxId = 0;
            TrxDate = Convert.ToDateTime(CCFBGlobal.DefaultServiceDate);
            HouseholdID = Hh.ID;
            TrxStatus = CCFBGlobal.statusTrxLog_Service;
            LbsCommodities = 0;
            LbsBabySvc = 0;
            LbsNonFood = 0;
            LbsStandard = 0;
            LbsSupplemental = 0;
            LbsOther = 0;
            if (Hh.IncludeOnLog)
                Notes = Hh.Comments;
            else
                Notes = "";
            FoodSvcList = "";
            NonFoodSvcList = "";
            BabySvcList = "";
            ConcatFoodSvcItemsList = "";
            ConcatNonFoodSvcItemsList = "";
            ConcatBabySvcItemsList = "";
            Infants = Hh.Infants;
            Youths = Hh.Youth;
            Teens = Hh.Teens;
            Eighteen = Hh.Eighteens;
            Adults = Hh.Adults;
            Seniors = Hh.Seniors;
            TotalFamily = Hh.TotalFamily;
            SpecialDiet = Hh.SpecialDiet;
            NumCat1 = 0;
            NumCat2 = 0;
            RcvdCommodity = false;
            RcvdSupplemental = false;
            FirstTimeEver = false;
            FiscalFirstTime = false;
            CalFirstTime = false;
            Homeless = Hh.Homeless;
            InCityLimits = Hh.InCityLimits;
            SingleHeadHh = Hh.SingleHeadHh;
            EthnicSpeaking = Hh.EthnicSpeaking;
            FBProgram = 0;
            Created = DateTime.Now;
            CreatedBy = CCFBGlobal.dbUserName;
            Modified = DateTime.Now;
            ModifiedBy = "insert";
            MonthFirstTime = false;
            DelivMethod = Convert.ToInt32(svcMethod);
            LbsNonFood = 0;
            RcvdVoucher = false;
            InCityLimits = Hh.InCityLimits;
            ClientType = Hh.ClientType;
            ZipCode = Hh.Zipcode;
            Disabled = Hh.Disabled;
            HHMemID = HHmemNameIn;
            Meals = 0;
            Bags = 0;
            FBProgram = 0;
            White = 0;
            Black = 0;
            Asian = 0;
            Native = 0;
            Pacific = 0;
            WhiteNative = 0;
            WhiteAsian = 0;
            WhiteBlack = 0;
            BlackNative = 0;
            Other = 0;
            Undisclosed = 0;
            WhiteHispanic = 0;
            BlackHispanic = 0;
            AsianHispanic = 0;
            NativeHispanic = 0;
            PacificHispanic = 0;
            WhiteNativeHispanic = 0;
            WhiteAsianHispanic = 0;
            WhiteBlackHispanic = 0;
            BlackNativeHispanic = 0;
            OtherHispanic = 0;
            UndisclosedHispanic = 0;
            for (int i = 0; i < hhm.RowCount; i++)
            {
                hhm.SetRecord(i);
                if (hhm.Hispanic == false)
                {
                    switch (hhm.Race)
                    {
                        case 0: White++; break;
                        case 1: Black++; break;
                        case 2: Asian++; break;
                        case 3: Native++; break;
                        case 4: Pacific++; break;
                        case 5: WhiteNative++; break;
                        case 6: WhiteAsian++; break;
                        case 7: WhiteBlack++; break;
                        case 8: BlackNative++; break;
                        case 9: Other++; break;
                        case 10: Undisclosed++; break;
                        default:
                            break;
                    }
                }
                else
                {
                    switch (hhm.Race)
                    {
                        case 0: WhiteHispanic++; break;
                        case 1: BlackHispanic++; break;
                        case 2: AsianHispanic++; break;
                        case 3: NativeHispanic++; break;
                        case 4: PacificHispanic++; break;
                        case 5: WhiteNativeHispanic++; break;
                        case 6: WhiteAsianHispanic++; break;
                        case 7: WhiteBlackHispanic++; break;
                        case 8: BlackNativeHispanic++; break;
                        case 9: OtherHispanic++; break;
                        case 10: UndisclosedHispanic++; break;
                        default:
                            break;
                    }
                }
            }
            Transient = (Hh.ClientType == CCFBPrefs.TransientId);
        }

        //----------------------------------------Get/Set Acsessors-----------------------------------------------------
        /// <summary>
        /// Each of these acsessors either gets or sets a value in the dataset.  
        /// These changes are not made in the database untill the update 
        /// funtion is called.
        /// </summary>

        #region GET/SET ACCESSORS

        public DataRow DRow
        {
            get { return drow; }
            set { drow = value; }
        }
        public string HHMemID
        {
            get { return drow["HHMemId"].ToString(); }
            set { drow["HHMemId"] = value; }
        }
        public int TrxId
        {
            get { return Convert.ToInt32(drow["TrxId"]); }
            set { drow["TrxId"] = value; }
        }
        public DateTime TrxDate
        {
            get { return CCFBGlobal.ValidDate(drow["TrxDate"]); }
            set { drow["TrxDate"] = value; }
        }
        public int HouseholdID
        {
            get { return Convert.ToInt32(drow["HouseholdID"]); }
            set { drow["HouseholdID"] = value; }
        }
        public Int16 Meals
        {
            get { return Convert.ToInt16(drow["Meals"]); }
            set { drow["Meals"] = value; }
        }
        public Int16 Bags
        {
            get { return Convert.ToInt16(drow["Bags"]); }
            set { drow["Bags"] = value; }
        }
        public int LbsStandard
        {
            get { return Convert.ToInt32(drow["LbsStd"]); }
            set { drow["LbsStd"] = value; }
        }
        public int LbsOther
        {
            get { return Convert.ToInt32(drow["LbsOther"]); }
            set { drow["LbsOther"] = value; }
        }
        public int LbsBabySvc
        {
            get { return Convert.ToInt32(drow["LbsBabySvc"]); }
            set { drow["LbsBabySvc"] = value; }
        }
        public int LbsCommodities
        {
            get { return Convert.ToInt32(drow["LbsCommodity"]); }
            set { drow["LbsCommodity"] = value; }
        }

        public string Notes
        {
            get { return drow["Notes"].ToString(); }
            set { drow["Notes"] = value; }
        }
        public string ConcatBabySvcItemsList
        {
            get { return drow["ConcatBabySvcItemsList"].ToString(); }
            set { drow["ConcatBabySvcItemsList"] = value; }
        }
        public string ConcatFoodSvcItemsList
        {
            get { return drow["ConcatFoodSvcItemsList"].ToString(); }
            set { drow["ConcatFoodSvcItemsList"] = value; }
        }
        public string ConcatNonFoodSvcItemsList
        {
            get { return drow["ConcatNonFoodSvcItemsList"].ToString(); }
            set { drow["ConcatNonFoodSvcItemsList"] = value; }
        }
        public string BabySvcList
        {
            get { return drow["BabySvcList"].ToString(); }
            set { drow["BabySvcList"] = value; }
        }
        public string FoodSvcList
        {
            get { return drow["FoodSvcList"].ToString(); }
            set { drow["FoodSvcList"] = value; }
        }
        public string NonFoodSvcList
        {
            get { return drow["NonFoodSvcList"].ToString(); }
            set { drow["NonFoodSvcList"] = value; }
        }
        public int Adults
        {
            get { return Convert.ToInt32(drow["Adults"]); }
            set { drow["Adults"] = value; }
        }
        public int Youths
        {
            get { return Convert.ToInt32(drow["Youth"]); }
            set { drow["Youth"] = value; }
        }
        public int Seniors
        {
            get { return Convert.ToInt32(drow["Seniors"]); }
            set { drow["Seniors"] = value; }
        }
        public int Infants
        {
            get { return Convert.ToInt32(drow["Infants"]); }
            set { drow["Infants"] = value; }
        }
        public int TotalFamily
        {
            get { return Convert.ToInt32(drow["TotalFamily"]); }
            set { drow["TotalFamily"] = value; }
        }
        public int SpecialDiet
        {
            get { return Convert.ToInt32(drow["SpecialDiet"]); }
            set { drow["SpecialDiet"] = value; }
        }
        public int NumCat1
        {
            get { return Convert.ToInt32(drow["NumCat1"]); }
            set { drow["NumCat1"] = value; }
        }
        public int NumCat2
        {
            get { return Convert.ToInt32(drow["NumCat2"]); }
            set { drow["NumCat2"] = value; }
        }
        public bool RcvdCommodity
        {
            get { return (bool)drow["RcvdCommodity"]; }
            set { drow["RcvdCommodity"] = value; }
        }
        public bool FirstTimeEver
        {
            get { return (bool)drow["FirstTimeEver"]; }
            set { drow["FirstTimeEver"] = value; }
        }
        public bool FiscalFirstTime
        {
            get { return (bool)drow["FiscalFirstTime"]; }
            set { drow["FiscalFirstTime"] = value; }
        }
        public bool CalFirstTime
        {
            get { return (bool)drow["CalFirstTime"]; }
            set { drow["CalFirstTime"] = value; }
        }
        public bool Transient
        {
            get { return (bool)drow["Transient"]; }
            set { drow["Transient"] = value; }
        }
        public int EthnicSpeaking
        {
            get { return Convert.ToInt32(drow["EthnicSpeaking"]); }
            set { drow["EthnicSpeaking"] = value; }
        }
        public DateTime Created
        {
            get { return (DateTime)drow["Created"]; }
            set { drow["Created"] = value; }
        }
        public string CreatedBy
        {
            get { return drow["CreatedBy"].ToString(); }
            set { drow["CreatedBy"] = value; }
        }
        public DateTime Modified
        {
            get 
            {
                if (drow["Modified"].ToString() == "")
                    return CCFBGlobal.FBNullDateValue;
                else
                    return (DateTime)drow["Modified"];
            }
            set { drow["Modified"] = value; }
        }
        public string ModifiedBy
        {
            get { return drow["ModifiedBy"].ToString(); }
            set { drow["ModifiedBy"] = value; }
        }
        public bool MonthFirstTime
        {
            get { return (bool)drow["MonthFirstTime"]; }
            set { drow["MonthFirstTime"] = value; }
        }
        public int DelivMethod
        {
            get { return Convert.ToInt32(drow["DelivMethod"]); }
            set { drow["DelivMethod"] = value; }
        }
        public int LbsNonFood
        {
            get { return Convert.ToInt32(drow["LbsNonFood"]); }
            set { drow["LbsNonFood"] = value; }
        }
        public int LbsSupplemental
        {
            get { return Convert.ToInt32(drow["LbsSupplemental"]); }
            set { drow["LbsSupplemental"] = value; }
        }
        public bool RcvdSupplemental
        {
            get { return (bool)drow["RcvdSupplemental"]; }
            set { drow["RcvdSupplemental"] = value; }
        }
        public bool RcvdVoucher
        {
            get { return (bool)drow["RcvdVoucher"]; }
            set { drow["RcvdVoucher"] = value; }
        }
        public Int32 Teens
        {
            get { return Convert.ToInt32(drow["Teens"]); }
            set { drow["Teens"] = value; }
        }
        public Int32 Eighteen
        {
            get { return Convert.ToInt32(drow["Eighteen"]); }
            set { drow["Eighteen"] = value; }
        }
        public Int16 TrxStatus
        {
            get { return Convert.ToInt16(drow["TrxStatus"]); }
            set { drow["TrxStatus"] = value; }
        }
        public bool Homeless
        {
            get { return (bool)drow["Homeless"]; }
            set { drow["Homeless"] = value; }
        }
        public bool InCityLimits
        {
            get { return (bool)drow["InCityLimits"]; }
            set { drow["InCityLimits"] = value; }
        }
        public bool SingleHeadHh
        {
            get { return (bool)drow["SingleHeadHh"]; }
            set { drow["SingleHeadHh"] = value; }
        }
        public int ClientType
        {
            get { return Convert.ToInt32(drow["ClientType"]); }
            set { drow["ClientType"] = value; }
        }
        public string ZipCode
        {
            get { return drow["ZipCode"].ToString(); }
            set { drow["ZipCode"] = value; }
        }
        public Int32 Disabled
        {
            get { return Convert.ToInt32(drow["Disabled"]); }
            set { drow["Disabled"] = value; }
        }
        public int FBProgram
        {
            get { return Convert.ToInt32(drow["FBProgram"]); }
            set { drow["FBProgram"] = value; }
        }
        public int White
        {
            get { return Convert.ToInt32(drow["White"]); }
            set { drow["White"] = value; }
        }
        public int Black
        {
            get { return Convert.ToInt32(drow["Black"]); }
            set { drow["Black"] = value; }
        }
        public int Asian
        {
            get { return Convert.ToInt32(drow["Asian"]); }
            set { drow["Asian"] = value; }
        }
        public int Native
        {
            get { return Convert.ToInt32(drow["Native"]); }
            set { drow["Native"] = value; }
        }
        public int Pacific
        {
            get { return Convert.ToInt32(drow["Pacific"]); }
            set { drow["Pacific"] = value; }
        }
        public int WhiteNative
        {
            get { return Convert.ToInt32(drow["WhiteNative"]); }
            set { drow["WhiteNative"] = value; }
        }
        public int WhiteAsian
        {
            get { return Convert.ToInt32(drow["WhiteAsian"]); }
            set { drow["WhiteAsian"] = value; }
        }
        public int WhiteBlack
        {
            get { return Convert.ToInt32(drow["WhiteBlack"]); }
            set { drow["WhiteBlack"] = value; }
        }
        public int BlackNative
        {
            get { return Convert.ToInt32(drow["BlackNative"]); }
            set { drow["BlackNative"] = value; }
        }
        public int Other
        {
            get { return Convert.ToInt32(drow["Other"]); }
            set { drow["Other"] = value; }
        }
        public int Undisclosed
        {
            get { return Convert.ToInt32(drow["Undisclosed"]); }
            set { drow["Undisclosed"] = value; }
        }

        public int WhiteHispanic
        {
            get { return Convert.ToInt32(drow["WhiteHispanic"]); }
            set { drow["WhiteHispanic"] = value; }
        }
        public int BlackHispanic
        {
            get { return Convert.ToInt32(drow["BlackHispanic"]); }
            set { drow["BlackHispanic"] = value; }
        }
        public int AsianHispanic
        {
            get { return Convert.ToInt32(drow["AsianHispanic"]); }
            set { drow["AsianHispanic"] = value; }
        }
        public int NativeHispanic
        {
            get { return Convert.ToInt32(drow["NativeHispanic"]); }
            set { drow["NativeHispanic"] = value; }
        }
        public int PacificHispanic
        {
            get { return Convert.ToInt32(drow["PacificHispanic"]); }
            set { drow["PacificHispanic"] = value; }
        }
        public int WhiteNativeHispanic
        {
            get { return Convert.ToInt32(drow["WhiteNativeHispanic"]); }
            set { drow["WhiteNativeHispanic"] = value; }
        }
        public int WhiteAsianHispanic
        {
            get { return Convert.ToInt32(drow["WhiteAsianHispanic"]); }
            set { drow["WhiteAsianHispanic"] = value; }
        }
        public int WhiteBlackHispanic
        {
            get { return Convert.ToInt32(drow["WhiteBlackHispanic"]); }
            set { drow["WhiteBlackHispanic"] = value; }
        }
        public int BlackNativeHispanic
        {
            get { return Convert.ToInt32(drow["BlackNativeHispanic"]); }
            set { drow["BlackNativeHispanic"] = value; }
        }
        public int OtherHispanic
        {
            get { return Convert.ToInt32(drow["OtherHispanic"]); }
            set { drow["OtherHispanic"] = value; }
        }
        public int UndisclosedHispanic
        {
            get { return Convert.ToInt32(drow["UndisclosedHispanic"]); }
            set { drow["UndisclosedHispanic"] = value; }
        }
        #endregion


//-----------------------------DATA VALUE--------------------------------------------------------------------
/// <summary>
///An Overloaded set of get/set funtions that will take in any kind of data value used in 
///the front end and accsess the data set for that data type, used mostly for a collection
///of textboxes so collection can be itterated through in one loop and have one funtion called
///no matter what type it actually refrenced
/// </summary>
/// <param name="FieldName">Fieldname=Collum Name in the Database</param>
/// <param name="FieldValue">FieldValue= .Net Data type</param>

        #region Data Value Accsessors
        //Sets data value when value is a string
        public void SetDataValue(string FieldName, string FieldValue)
        {
            if (FieldName == "FoodSvcList" || FieldName == "NonFoodSvcList")
            {
            }
            drow[FieldName] = FieldValue;
        }
        //Sets data value when value is a bool
        public void SetDataValue(string FieldName, bool FieldValue)
        {
            drow[FieldName] = FieldValue;
        }
        
        //Gets property through use of just the collum name in database
        public object GetDataValue(string FieldName)
        {
            if (FieldName == "FoodSvcList" || FieldName == "NonFoodSvcList")
            {
            }
            return drow[FieldName];
        }

        #endregion


    }
    #endregion
}