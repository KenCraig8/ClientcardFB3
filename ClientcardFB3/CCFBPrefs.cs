using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ClientcardFB3
{
    public class CCFBPrefs
    {
        private const string tbName = "Preferences";
        private const string constFldValue = "FldVal";
        private const string constBoolVal = "BoolVal";

        #region My Food Bank
        public static string FoodBankName = "";
        public static string PostalAddress = "";
        public static string PhysicalAddress = "";
        public static string PhoneNumber = "";
        public static string County = "";
        public static string EmailAddress = "";
        public static string FaxNumber = "";
        public static string AgencyNumber = "";
        public static bool EnableCDBGReporting = false;
        public static bool CaptureSignature = false;
        public static string EFAPLeadAgency = "";
        #endregion

        public static bool EnableHomeDeliv = false;
        #region Features
        public static bool EnableFoodServices = true;
        public static bool EnableAppointments = false;
        public static bool EnableFoodDonations = true;
        public static bool EnableCashDonations = true;
        public static bool EnableVolunteerHours = true;
        public static bool EnablePrintClientCard = true;
        public static bool EnableVouchers = false;
        public static bool EnableCSFP = false;
        public static bool EnableCSFPOnNewSvc = false;
        public static bool EnableCSFPShowRoutes = false;
        public static bool EnableTEFAP = true;
        public static bool MustBeACommodityDay = true;
        public static bool EnableServiceGroups = false;
        public static bool EnableSupplemental = true;
        public static bool EnableClientPhone = true;
        public static bool EnableVerifyId = true;
        public static bool EnableHouseholdIncome = false;
        public static bool EnableHHUserDefinedFields = true;
        public static bool EnableBabyServices = true;
        public static bool EnableWorksInArea = false;
        public static bool EnableAdditionalHHMDataTab = false;
        public static bool EnableEthnicityHHMTab = false;
        public static bool EnableIDFlds = true;
        public static bool EnableFastTrack = false;
        public static bool EnableHUDCategory = false;
        public static bool EnableTransportation = false;
        public static bool UseTimeInOutForVols = false;
        public static bool EnableBarcodePrompts = false;
        public static bool BarcodeUseFamilyMember = false;
        public static bool AutomaticallyGiveService = false;
        public static int CommSigValidFor = 0;
        public static bool CaptureTEFAPSignature = false;
        public static int AlertMonthSvc = 0;
        public static int AlertWeekSvc = 0;
        public static string AlertMonthSvcText = "Maximum Monthly Services Reached";
        public static string AlertWeekSvcText = "This Week's services already given";
        public static bool WarnSvcEachPerson = false;
        public static bool EnableClientReceipt = false;
        public static int ServiceMenuType = 0;
        public static bool EnablePointsTracking = false;
        public static int PointsPerWeek = 0;
        public static int PointsPerFamMbr = 0;
        public static int MaxPointsPerWeek = 40;
        public static int PointsPerWeekOutOfArea = 0;
        public static bool EnableBackPack = false;
        public static int OverRideLevel = 2;
        public static int DefaultFMIDType = 1;
        public static string AlertMinDaysText = "LAST SERVICE LESS THAN MINIMUM DAYS";
        public static int AlertMinimumDays=0;
        public static string AlertMinMonthsText = "LAST SERVICE LESS THAN MINIMUM MONTHS";
        public static int AlertMinimumMonths = 0;
        public static bool IncludeLbsOnSvcList = true;
        public static bool AllowLbsManualEntry = false;
        #endregion

        #region Add Client Options
        public static string DefaultCity = "";
        public static string DefaultState = "WA";
        public static string DefaultZipcode = "";
        public static bool AllowDuplicateHHNames = false;
        public static bool AllowDuplicateMemberNames = true;
        public enum UseFamilyListCode { Normally = 0, Sometimes = 1, Always = 2, Never = 3 };
        public static UseFamilyListCode UseFamilyList = UseFamilyListCode.Normally;
        public static bool FilterPeriodFromAddress = true;
        #endregion
        
        #region Form Options
        public static bool FindClientAutoRefresh = false;
        public static int ServiceLogRefreshRate = 60;
        public static int ApptLogRefreshRate = 90;
        public static int NbrMealsPerService = 10;
        public static int NbrDaysAllowMods = 30;
        public static int NbrSvcDatesFuture = 7;
        public static int NbrSvcDatesPast = 45;
        public static bool UseCalendarWeeks = true;
        public static int CSFPLbsPerService = 26;
        #endregion

        //#region Family Card Slots
        //public static int FamilyCardSlot1 = -1;
        //public static int FamilyCardSlot2 = -1;
        //public static int FamilyCardSlot3 = -1;
        //public static int FamilyCardSlot4 = -1;
        //#endregion

        #region Monthly Reports
        public static int FiscalYearStartMonth = 7;
        public static decimal InkindDollarsPerHr = 10;
        public static decimal InkindDollarsPerLb = 1.5m;
        public static bool MergeTeens = true;
        public static Int32 DonorIDNWH = 0;
        public static Int32 DonorIDEFAP = 0;
        public static Int32 DonorIDTEFAP = 0;
        public static Int32 DonorID2ndHarvest = 0;
        public static bool IncludeCommodityLbsInCoalition = true;
        public static bool IncludeCommodityLbsInFoodLifeline = true;
        public static bool IncludeCommodityLbsInNorhtwestHarvest = true;
        public static bool IncludeCommodityLbsInSecondHarvestInland = true;
        public enum DonorCalcPercentMethod { LbsServed = 0, LbsDonated = 1 };
        public static DonorCalcPercentMethod DonorPercentCalcMethod = DonorCalcPercentMethod.LbsServed;
        public static string PreparedBy = "Director";
        public static string ReportsSavePath = "";
        public static int TransientId = 3;
        #endregion
        #region Donor Percent List
        public static Int32 DonorId01 = 0;
        public static Int32 DonorId02 = 0;
        public static Int32 DonorId03 = 0;
        public static Int32 DonorId04 = 0;
        public static Int32 DonorId05 = 0;
        public static Int32 DonorId06 = 0;
        public static Int32 DonorId07 = 0;
        public static Int32 DonorId08 = 0;
        public static Int32 DonorId09 = 0;
        public static Int32 DonorId10 = 0;
        public static Int32 DonorId11 = 0;
        public static Int32 DonorId12 = 0;
        public static Int32 DonorId13 = 0;
        public static Int32 DonorId14 = 0;
        public static Int32 DonorId15 = 0;
        public static Int32 DonorId16 = 0;
        public static Int32 DonorId17 = 0;
        public static Int32 DonorId18 = 0;
        public static Int32 DonorId19 = 0;
        public static Int32 DonorId20 = 0;
        #endregion

        public static void Init()   //Load data from Defaults table
        {
            ReportsSavePath = CCFBGlobal.pathReports;
            SqlConnection conn = new SqlConnection(CCFBGlobal.connectionString);
            SqlCommand command = new SqlCommand("SELECT * FROM " + tbName + " ORDER BY EditForm, FldName", conn);
            SqlDataAdapter dadAdpt = new SqlDataAdapter(command);
            DataSet dset = new DataSet();
            int NbrRows = dadAdpt.Fill(dset);
            foreach (DataRow drow in dset.Tables[0].Rows)
            {
                switch (drow.Field<string>("fldname").ToLower())
                {
                    case "agencynumber":            AgencyNumber = drow.Field<string>(constFldValue); break;
                    case "alertmindaystext":        AlertMinDaysText = drow.Field<string>(constFldValue); break;
                    case "alertminimumdays":        AlertMinimumDays = Convert.ToInt32(drow[constFldValue]); break;
                    case "alertminmonthstext":      AlertMinMonthsText = drow.Field<string>(constFldValue); break;
                    case "alertminimummonths":      AlertMinimumMonths = Convert.ToInt32(drow[constFldValue]); break;
                    case "alertmonthsvc":           AlertMonthSvc = Convert.ToInt32(drow[constFldValue]); break;
                    case "alertmonthsvctext":       AlertMonthSvcText = drow.Field<string>(constFldValue); break;
                    case "alertweeksvc":            AlertWeekSvc = Convert.ToInt32(drow[constFldValue]); break;
                    case "alertweeksvctext":        AlertWeekSvcText = drow.Field<string>(constFldValue); break;
                    case "allowduplicatehhnames":   AllowDuplicateHHNames = drow.Field<bool>(constBoolVal); break;
                    case "allowduplicatemembernames": AllowDuplicateMemberNames = drow.Field<bool>(constBoolVal); break;
                    case "allowlbsmanualentry":     AllowLbsManualEntry = drow.Field<bool>(constBoolVal); break;
                    case "apptlogrefreshrate":      ApptLogRefreshRate = Convert.ToInt32(drow[constFldValue]); break;
                    case "automaticallygiveservice": AutomaticallyGiveService = drow.Field<bool>(constBoolVal); break;
                    case "capturesignature":        CaptureSignature = drow.Field<bool>(constBoolVal); break;
                    case "capturetefapsignature":   CaptureTEFAPSignature = drow.Field<bool>(constBoolVal); break;
                    case "commsigvalidfor":         CommSigValidFor = Convert.ToInt32(drow[constFldValue]); break;
                    case "county":                  County = drow.Field<string>(constFldValue); break;
                    case "csfplbsperservice":       CSFPLbsPerService = Convert.ToInt32(drow[constFldValue]); break;
                    case "defaultcity":             DefaultCity = drow.Field<string>(constFldValue); break;
                    case "defaultfmidype":          DefaultFMIDType = Convert.ToInt32(drow[constFldValue]); break;
                    case "defaultstate":            DefaultState = drow.Field<string>(constFldValue); break;
                    case "defaultzipcode":          DefaultZipcode = drow.Field<string>(constFldValue); break;
                    case "donorid01":               DonorId01 = Convert.ToInt32(drow[constFldValue]); break;
                    case "donorid02":               DonorId02 = Convert.ToInt32(drow[constFldValue]); break;
                    case "donorid03":               DonorId03 = Convert.ToInt32(drow[constFldValue]); break;
                    case "donorid04":               DonorId04 = Convert.ToInt32(drow[constFldValue]); break;
                    case "donorid05":               DonorId05 = Convert.ToInt32(drow[constFldValue]); break;
                    case "donorid06":               DonorId06 = Convert.ToInt32(drow[constFldValue]); break;
                    case "donorid07":               DonorId07 = Convert.ToInt32(drow[constFldValue]); break;
                    case "donorid08":               DonorId08 = Convert.ToInt32(drow[constFldValue]); break;
                    case "donorid09":               DonorId09 = Convert.ToInt32(drow[constFldValue]); break;
                    case "donorid10":               DonorId10 = Convert.ToInt32(drow[constFldValue]); break;
                    case "donorid11":               DonorId11 = Convert.ToInt32(drow[constFldValue]); break;
                    case "donorid12":               DonorId12 = Convert.ToInt32(drow[constFldValue]); break;
                    case "donorid13":               DonorId13 = Convert.ToInt32(drow[constFldValue]); break;
                    case "donorid14":               DonorId14 = Convert.ToInt32(drow[constFldValue]); break;
                    case "donorid15":               DonorId15 = Convert.ToInt32(drow[constFldValue]); break;
                    case "donorid16":               DonorId16 = Convert.ToInt32(drow[constFldValue]); break;
                    case "donorid17":               DonorId17 = Convert.ToInt32(drow[constFldValue]); break;
                    case "donorid18":               DonorId18 = Convert.ToInt32(drow[constFldValue]); break;
                    case "donorid19":               DonorId19 = Convert.ToInt32(drow[constFldValue]); break;
                    case "donorid20":               DonorId20 = Convert.ToInt32(drow[constFldValue]); break;
                    case "donorid2ndharvest":       DonorID2ndHarvest = Convert.ToInt32(drow[constFldValue]); break;
                    case "donoridefap":             DonorIDEFAP = Convert.ToInt32(drow[constFldValue]); break;
                    case "donoridtefap":            DonorIDTEFAP = Convert.ToInt32(drow[constFldValue]); break;
                    case "donornwh":                DonorIDNWH = Convert.ToInt32(drow[constFldValue]); break;
                    case "efapleadagency":          EFAPLeadAgency = drow.Field<string>(constFldValue); break;
                    case "emailaddress":            EmailAddress = drow.Field<string>(constFldValue); break;
                    case "enableadditionalhhmdatatab": EnableAdditionalHHMDataTab = drow.Field<bool>(constBoolVal); break;
                    case "enableappointments":      EnableAppointments = drow.Field<bool>(constBoolVal); break;
                    case "enablebabyservices":      EnableBabyServices = drow.Field<bool>(constBoolVal); break;
                    case "enablebackpack":          EnableBackPack = drow.Field<bool>(constBoolVal); break;
                    case "enablebarcodeprompts":    EnableBarcodePrompts = drow.Field<bool>(constBoolVal); break;
                    case "enablecashdonations":     EnableCashDonations = drow.Field<bool>(constBoolVal); break;
                    case "enablecdbgreporting":     EnableCDBGReporting = drow.Field<bool>(constBoolVal); break;
                    case "enableclientphone":       EnableClientPhone = drow.Field<bool>(constBoolVal); break;
                    case "enableclientreceipt":     EnableClientReceipt = drow.Field<bool>(constBoolVal); break;
                    case "enablecsfp":              EnableCSFP = drow.Field<bool>(constBoolVal); break;
                    case "enablecsfponnewsvc":      EnableCSFPOnNewSvc = drow.Field<bool>(constBoolVal); break;
                    case "enablecsfpshowroutes":    EnableCSFPShowRoutes = drow.Field<bool>(constBoolVal); break;
                    case "enableethnicityhhmtab":   EnableEthnicityHHMTab = drow.Field<bool>(constBoolVal); break;
                    case "enablefasttrack":         EnableFastTrack = drow.Field<bool>(constBoolVal); break;
                    case "enablefooddonations":     EnableFoodDonations = drow.Field<bool>(constBoolVal); break;
                    case "enablefoodservices":      EnableFoodServices = drow.Field<bool>(constBoolVal); break;
                    case "enablehhuserdefinedfieldstab": EnableHHUserDefinedFields = drow.Field<bool>(constBoolVal); break;
                    case "enablehomedeliv":         EnableHomeDeliv = drow.Field<bool>(constBoolVal); break;
                    case "enablehouseholdincome":   EnableHouseholdIncome = drow.Field<bool>(constBoolVal); break;
                    case "enablehudcategory":       EnableHUDCategory = drow.Field<bool>(constBoolVal); break;
                    case "enableidflds":            EnableIDFlds = drow.Field<bool>(constBoolVal); break;
                    case "enablepointstracking":    EnablePointsTracking = drow.Field<bool>(constBoolVal); break;
                    case "enableprintclientcard":   EnablePrintClientCard = drow.Field<bool>(constBoolVal); break;
                    case "enableservicegroups":     EnableServiceGroups = drow.Field<bool>(constBoolVal); break;
                    case "enablesupplemental":      EnableSupplemental = drow.Field<bool>(constBoolVal); break;
                    case "enabletefap":             EnableTEFAP = drow.Field<bool>(constBoolVal); break;
                    case "enabletransporation":     EnableTransportation = drow.Field<bool>(constBoolVal); break;
                    case "enableverifyid":          EnableVerifyId = drow.Field<bool>(constBoolVal); break;
                    case "enablevolunteerhours":    EnableVolunteerHours = drow.Field<bool>(constBoolVal); break;
                    case "enablevouchers":          EnableVouchers = drow.Field<bool>(constBoolVal); break;
                    case "enableworksinarea":       EnableWorksInArea = drow.Field<bool>(constBoolVal); break;
                    //case "familycardslot1": FamilyCardSlot1 = Convert.ToInt32(drow[constFldValue]); break;
                    //case "familycardslot2": FamilyCardSlot2 = Convert.ToInt32(drow[constFldValue]); break;
                    //case "familycardslot3": FamilyCardSlot3 = Convert.ToInt32(drow[constFldValue]); break;
                    //case "familycardslot4": FamilyCardSlot4 = Convert.ToInt32(drow[constFldValue]); break;
                    case "faxnumber":               FaxNumber = drow.Field<string>(constFldValue); break;
                    case "filterperiodfromaddress": FilterPeriodFromAddress = drow.Field<bool>(constBoolVal); break;
                    case "findclientautorefresh":   FindClientAutoRefresh = drow.Field<bool>(constBoolVal); break;
                    case "fiscalyearstartmonth":    FiscalYearStartMonth = Convert.ToInt32(drow[constFldValue]); break;
                    case "foodbankname":            FoodBankName = drow.Field<string>(constFldValue); break;
                    case "includecommoditylbsincoalition": IncludeCommodityLbsInCoalition = drow.Field<bool>(constBoolVal); break;
                    case "includecommoditylbsinfoodlifeline": IncludeCommodityLbsInFoodLifeline = drow.Field<bool>(constBoolVal); break;
                    case "includecommoditylbsinnorhtwestharvest": IncludeCommodityLbsInNorhtwestHarvest = drow.Field<bool>(constBoolVal); break;
                    case "includecommoditylbsinsecondharvestinland": IncludeCommodityLbsInCoalition = drow.Field<bool>(constBoolVal); break;
                    case "includelbsonsvclist":     IncludeLbsOnSvcList = drow.Field<bool>(constBoolVal); break;
                    case "inkinddollarsperhour":    InkindDollarsPerHr = Convert.ToDecimal(drow[constFldValue]); break;
                    case "inkinddollarsperlb":      InkindDollarsPerLb = Convert.ToDecimal(drow[constFldValue]); break;
                    case "maxpointsperweek":        MaxPointsPerWeek = Convert.ToInt32(drow[constFldValue]); break;
                    case "mergeteens":              MergeTeens = drow.Field<bool>(constBoolVal); break;
                    case "mustbeacommodityday":     MustBeACommodityDay = drow.Field<bool>(constBoolVal); break;
                    case "nbrdaysallowmods":        NbrDaysAllowMods = Convert.ToInt32(drow[constFldValue]); break;
                    case "nbrmealsperservice":      NbrMealsPerService = Convert.ToInt32(drow[constFldValue]); break;
                    case "nbrsvcdatesfuture":       NbrSvcDatesFuture = Convert.ToInt32(drow[constFldValue]); break;
                    case "nbrsvcdatespast":         NbrSvcDatesPast = Convert.ToInt32(drow[constFldValue]); break;
                    case "overridelevel":           OverRideLevel = Convert.ToInt32(drow[constFldValue]); break;
                    case "phonenumber":             PhoneNumber = drow.Field<string>(constFldValue); break;
                    case "physicaladdress":         PhysicalAddress = drow.Field<string>(constFldValue); break;
                    case "pointsperfammbr":         PointsPerFamMbr = Convert.ToInt32(drow[constFldValue]); break;
                    case "pointsperweek":           PointsPerWeek = Convert.ToInt32(drow[constFldValue]); break;
                    case "pointsperweekoutofarea":  PointsPerWeekOutOfArea = Convert.ToInt32(drow[constFldValue]); break;
                    case "postaladdress":           PostalAddress = drow.Field<string>(constFldValue); break;
                    case "preparedby":              PreparedBy = drow.Field<string>(constFldValue); break;
                    case "reportssavepath":         ReportsSavePath = drow.Field<string>(constFldValue); break;
                    case "servicelogrefreshrate":   ServiceLogRefreshRate = Convert.ToInt32(drow[constFldValue]); break;
                    case "servicemenutype":         ServiceMenuType = Convert.ToInt32(drow[constFldValue]); break;
                    case "transientid":             TransientId = Convert.ToInt32(drow[constFldValue]); break;
                    case "barcodeusefamilymember":  BarcodeUseFamilyMember = drow.Field<bool>(constBoolVal); break;
                    case "usecalendarweeks":        UseCalendarWeeks = drow.Field<bool>(constBoolVal); break;
                    case "usetimeinoutforvols":     UseTimeInOutForVols = drow.Field<bool>(constBoolVal); break;
                    case "usefamilylist":
                        if (drow[constFldValue].ToString() == "0")
                            UseFamilyList = UseFamilyListCode.Normally; 
                        else if (drow[constFldValue].ToString() == "1")
                            UseFamilyList = UseFamilyListCode.Sometimes; 
                        else if (drow[constFldValue].ToString() == "2")
                            UseFamilyList = UseFamilyListCode.Always;
                        else
                            UseFamilyList = UseFamilyListCode.Never;
                        break;
                    case "warnsvceachperson":       WarnSvcEachPerson = drow.Field<bool>(constBoolVal); break;
                    case "donorpercentcalcmethod":
                        if (drow[constFldValue].ToString() == "0")
                            DonorPercentCalcMethod = DonorCalcPercentMethod.LbsServed;
                        else
                            DonorPercentCalcMethod = DonorCalcPercentMethod.LbsDonated;
                        break;
                }
            }
        }

        public static bool SaveValue(string fldname, string newvalue)
        {
            return (doUpdate("Update " + tbName 
                            + " SET FldVal = '" + CCFBGlobal.SQLApostrophe(newvalue)
                            + "' WHERE FldName = '" + fldname + "'") > 0);
        }
        public static bool SaveValue(string fldname, bool newvalue)
        {
            return (doUpdate("Update " + tbName 
                            + " SET BoolVal = " + Convert.ToInt16(newvalue).ToString() 
                            + " WHERE FldName = '" + fldname + "'") > 0);
        }
        public static bool SaveValue(string fldname, int newvalue)
        {
            return (doUpdate("Update " + tbName
                            + " SET FldVal = '" + newvalue.ToString()
                            + "' WHERE FldName = '" + fldname + "'") > 0);
        }
        private static int doUpdate(string sqlText)
        {
            SqlConnection conn = new SqlConnection(CCFBGlobal.connectionString);
            SqlCommand command = new SqlCommand(sqlText, conn);
            conn.Open();
            int NbrRows = command.ExecuteNonQuery();
            conn.Close();
            return NbrRows;
        }

        public static DateTime MinEditTrxDate()
        {
            return DateTime.Today.Subtract(new TimeSpan(NbrDaysAllowMods, 0, 0, 0));
        }

        public static bool TestAllowEditTrxDate(DateTime testDate, bool showMsgbox)
        {
            DateTime minEditDate = MinEditTrxDate();
            string msgText = "";
            if (testDate < minEditDate)
            {
                if (showMsgbox == true)
                {
                    msgText = @"The transaction date of " + testDate.ToShortDateString()
                        + " is before the minimum allowable edit date of " + minEditDate.ToShortDateString() + ".\r\n";
                    if (CCFBGlobal.currentUser_PermissionLevel == CCFBGlobal.permissions_Admin)
                    {
                        return (MessageBox.Show(msgText + "\r\nDo you want to Edit anyway?"
                            , "Edit Service Transaction - Date Range Check"
                            , MessageBoxButtons.YesNo
                            , MessageBoxIcon.Exclamation) == DialogResult.Yes);
                    }
                    else
                    {
                        MessageBox.Show(msgText
                            , "Edit Service Transaction - Date Test"
                            , MessageBoxButtons.OK
                            , MessageBoxIcon.Exclamation);
                        return false;
                    }
                }
                else return (CCFBGlobal.currentUser_PermissionLevel == CCFBGlobal.permissions_Admin);
            }
            else return true;
        }
        public static void UpdateCSFPServiceLbs(string newValue)
        {
            CSFPLbsPerService = Convert.ToInt32(newValue);
            SaveValue("CSFPLbsPerService", CSFPLbsPerService);
        }
    }
}
