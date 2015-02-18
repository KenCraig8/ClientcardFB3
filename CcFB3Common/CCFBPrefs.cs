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
    public static class CCFBPrefs
    {
        private const string tbName = "Preferences";
        private const string constFldValue = "FldVal";
        private const string constBoolVal = "BoolVal";

        public enum enumPrefs
        {
            agencynumber,
            alertmindaystext,
            alertminimumdays,
            alertminimummonths,
            alertminmonthstext,
            alertmonthsvc,
            alertmonthsvctext,
            alertweeksvc,
            alertweeksvctext,
            alertoamindaystext,
            alertoaminimumdays,
            alertoaminimummonths,
            alertoaminmonthstext,
            alertoamonthsvc,
            alertoamonthsvctext,
            alertoaweeksvc,
            alertoaweeksvctext,
            alertoamessagetext,
            alertoamsgon,
            allowduplicatehhnames,
            allowduplicatemembernames,
            allowintakeediting,
            allowlbsmanualentry,
            apptlogrefreshrate,
            automaticallygiveservice,
            barcodeusefamilymember,
            capturesignature,
            capturetefapsignature,
            commsigvalidfor,
            county,
            csfplbsperservice,
            defaultcity,
            defaultfmidtype,
            defaultstate,
            defaultzipcode,
            donorid01,
            donorid02,
            donorid03,
            donorid04,
            donorid05,
            donorid06,
            donorid07,
            donorid08,
            donorid09,
            donorid10,
            donorid11,
            donorid12,
            donorid13,
            donorid14,
            donorid15,
            donorid16,
            donorid17,
            donorid18,
            donorid19,
            donorid20,
            donorid2harvest,
            donoridefap,
            donoridfll,
            donoridnwh, 
            donoridtefap,
            donorpercentcalcmethod,
            efapleadagency,
            emailaddress,
            enableadditionalhhmdatatab,
            enableappointments,
            enablebabyservices,
            enablebackpack,
            enablebarcodeprompts,
            enablecashdonations,
            enablecdbgreporting,
            enablechristmasprogram,
            enableclientphone,
            enableclientreceipt,
            enablecsfp,
            enablecsfponnewsvc,
            enablecsfpshowroutes,
            enableethnicityhhmtab,
            enablefasttrack,
            enablefooddonations,
            enablefoodservices,
            enableftscale,
            enablehhuserdefinedfieldstab,
            enablehomedeliv,
            enablehouseholdincome,
            enablehudcategory,
            enableidflds,
            enablepointstracking,
            enableprintclientcard,
            enableschsupply,
            enableservicegroups,
            enablesupplemental,
            enabletefap,
            enabletransportation,
            enableverifyid,
            enablevolunteerhours,
            enablevouchers,
            enableworksinarea,
            faxnumber,
            filterperiodfromaddress,
            findclientautorefresh,
            fiscalyearstartmonth,
            foodbankname,
            ftlbsincludecommoditywt,
            includecommoditylbsincoalition,
            includecommoditylbsinfoodlifeline,
            includecommoditylbsinnorthwestharvest,
            includecommoditylbsinsecondharvestinland,
            includelbsonsvclist,
            inkinddollarsperhour,
            inkinddollarsperlb,
            maxpointsperweek,
            mergeteens,
            mustbeacommodityday,
            nbrdaysallowmods,
            nbrmealsperservice,
            nbrsvcdatesfuture,
            nbrsvcdatespast,
            overridelevel,
            phonenumber,
            physicaladdress,
            pointsperfammbr,
            pointsperweek,
            pointsperweekoutofarea,
            postaladdress,
            preparedby,
            reportssavepath,
            servicelogrefreshrate,
            servicemenutype,
            transientid,
            usecalendarweeks,
            usefamilylist,
            uselocaloutofareaalerts,
            usetimeinoutforvols,
            warnsvceachperson
        }
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

        #region Features
        public static bool EnableHomeDeliv = false;
        public static bool EnableChristmasProgram = false;
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
        public static bool EnableSchSupply = false;
        public static bool UseTimeInOutForVols = false;
        public static bool EnableBarcodePrompts = false;
        public static bool BarcodeUseFamilyMember = false;
        public static bool AutomaticallyGiveService = false;
        public static int CommSigValidFor = 0;
        public static bool CaptureTEFAPSignature = false;
        //Alerts
        public static bool UseLocalOutOfAreaAlerts = false;
        public static string AlertMinDaysText = "LAST SERVICE LESS THAN MINIMUM DAYS";
        public static int AlertMinimumDays = 0;
        public static string AlertMinMonthsText = "LAST SERVICE LESS THAN MINIMUM MONTHS";
        public static int AlertMinimumMonths = 0;
        public static int AlertMonthSvc = 0;
        public static string AlertMonthSvcText = "Maximum Monthly Services Reached";
        public static int AlertWeekSvc = 0;
        public static string AlertWeekSvcText = "This Week's services already given";

        public static string AlertOAMinDaysText = "OUT of AREA - LAST SERVICE LESS THAN MINIMUM DAYS";
        public static int AlertOAMinimumDays = 0;
        public static string AlertOAMinMonthsText = "OUT of AREA - LAST SERVICE LESS THAN MINIMUM MONTHS";
        public static int AlertOAMinimumMonths = 0;
        public static int AlertOAMonthSvc = 0;
        public static string AlertOAMonthSvcText = "OUT of AREA - Maximum Monthly Services Reached";
        public static int AlertOAWeekSvc = 0;
        public static string AlertOAWeekSvcText = "OUT of AREA - This Week's services already given";
        public static bool WarnSvcEachPerson = false;
        public static string AlertOAMessageText = ""; 
        public static bool AlertOAMsgON = false;


        //
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
        public static bool IncludeLbsOnSvcList = true;
        public static bool AllowLbsManualEntry = false;
        public static bool AllowIntakeEditing = true;
        #endregion

        #region Automated Scale Feature
        public static bool EnableFTscale = false;
        public static bool FTLbsIncludeCommodityWt =false;
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

        #region Monthly Reports
        public static int FiscalYearStartMonth = 7;
        public static decimal InkindDollarsPerHr = 10;
        public static decimal InkindDollarsPerLb = 1.5m;
        public static bool MergeTeens = true;
        public static Int32 DonorIDNWH = 0;
        public static Int32 DonorIDEFAP = 0;
        public static Int32 DonorIDTEFAP = 0;
        public static Int32 DonorID2ndHarvest = 0;
        public static Int32 DonorIDFLL = 0;
        public static bool IncludeCommodityLbsInCoalition = true;
        public static bool IncludeCommodityLbsInFoodLifeline = true;
        public static bool IncludeCommodityLbsInNorthwestHarvest = true;
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
            string FldName = "";
            string FldValue = "";
            bool BoolValue = false;
            SqlConnection conn = new SqlConnection(CCFBGlobal.connectionString);
            conn.Open();
            SqlCommand sqlCmd = new SqlCommand("SELECT FldName, FldVal, BoolVal FROM Preferences ORDER BY FldName", conn);
            SqlDataReader sqlReader = sqlCmd.ExecuteReader();
            while (sqlReader.Read())
            {
                FldName = sqlReader.GetString(0).ToLower();
                FldValue = sqlReader.GetString(1);
                BoolValue = sqlReader.GetBoolean(2);
                try
                {
                    enumPrefs a = (enumPrefs)Enum.Parse(typeof(enumPrefs), FldName);
                    switch (a)
                    {
                        case enumPrefs.agencynumber:                AgencyNumber = FldValue; break;
                        case enumPrefs.alertmindaystext:            AlertMinDaysText = FldValue; break;
                        case enumPrefs.alertminimumdays:            AlertMinimumDays = Convert.ToInt32(FldValue); break;
                        case enumPrefs.alertminmonthstext:          AlertMinMonthsText = FldValue; break;
                        case enumPrefs.alertminimummonths:          AlertMinimumMonths = Convert.ToInt32(FldValue); break;
                        case enumPrefs.alertmonthsvc:               AlertMonthSvc = Convert.ToInt32(FldValue); break;
                        case enumPrefs.alertmonthsvctext:           AlertMonthSvcText = FldValue; break;
                        case enumPrefs.alertweeksvc:                AlertWeekSvc = Convert.ToInt32(FldValue); break;
                        case enumPrefs.alertweeksvctext:            AlertWeekSvcText = FldValue; break;
                        case enumPrefs.alertoamindaystext:          AlertOAMinDaysText = FldValue; break;
                        case enumPrefs.alertoaminimumdays:          AlertOAMinimumDays = Convert.ToInt32(FldValue); break;
                        case enumPrefs.alertoaminmonthstext:        AlertOAMinMonthsText = FldValue; break;
                        case enumPrefs.alertoaminimummonths:        AlertOAMinimumMonths = Convert.ToInt32(FldValue); break;
                        case enumPrefs.alertoamonthsvc:             AlertOAMonthSvc = Convert.ToInt32(FldValue); break;
                        case enumPrefs.alertoamonthsvctext:         AlertOAMonthSvcText = FldValue; break;
                        case enumPrefs.alertoaweeksvc:              AlertOAWeekSvc = Convert.ToInt32(FldValue); break;
                        case enumPrefs.alertoaweeksvctext:          AlertOAWeekSvcText = FldValue; break;
                        case enumPrefs.alertoamessagetext:          AlertOAMessageText = FldValue; break;
                        case enumPrefs.alertoamsgon:                AlertOAMsgON = BoolValue; break;
                        case enumPrefs.allowduplicatehhnames:       AllowDuplicateHHNames = BoolValue; break;
                        case enumPrefs.allowduplicatemembernames:   AllowDuplicateMemberNames = BoolValue; break;
                        case enumPrefs.allowintakeediting:          AllowIntakeEditing = BoolValue; break;
                        case enumPrefs.allowlbsmanualentry:         AllowLbsManualEntry = BoolValue; break;
                        case enumPrefs.apptlogrefreshrate:          ApptLogRefreshRate = Convert.ToInt32(FldValue); break;
                        case enumPrefs.automaticallygiveservice:    AutomaticallyGiveService = BoolValue; break;
                        case enumPrefs.barcodeusefamilymember:      BarcodeUseFamilyMember = BoolValue; break;
                        case enumPrefs.capturesignature:            CaptureSignature = BoolValue; break;
                        case enumPrefs.capturetefapsignature:       CaptureTEFAPSignature = BoolValue; break;
                        case enumPrefs.commsigvalidfor:             CommSigValidFor = Convert.ToInt32(FldValue); break;
                        case enumPrefs.county:                      County = FldValue; break;
                        case enumPrefs.csfplbsperservice:           CSFPLbsPerService = Convert.ToInt32(FldValue); break;
                        case enumPrefs.defaultcity:                 DefaultCity = FldValue; break;
                        case enumPrefs.defaultfmidtype:             DefaultFMIDType = Convert.ToInt32(FldValue); break;
                        case enumPrefs.defaultstate:                DefaultState = FldValue; break;
                        case enumPrefs.defaultzipcode:              DefaultZipcode = FldValue; break;
                        case enumPrefs.donorid01:                   DonorId01 = Convert.ToInt32(FldValue); break;
                        case enumPrefs.donorid02:                   DonorId02 = Convert.ToInt32(FldValue); break;
                        case enumPrefs.donorid03:                   DonorId03 = Convert.ToInt32(FldValue); break;
                        case enumPrefs.donorid04:                   DonorId04 = Convert.ToInt32(FldValue); break;
                        case enumPrefs.donorid05:                   DonorId05 = Convert.ToInt32(FldValue); break;
                        case enumPrefs.donorid06:                   DonorId06 = Convert.ToInt32(FldValue); break;
                        case enumPrefs.donorid07:                   DonorId07 = Convert.ToInt32(FldValue); break;
                        case enumPrefs.donorid08:                   DonorId08 = Convert.ToInt32(FldValue); break;
                        case enumPrefs.donorid09:                   DonorId09 = Convert.ToInt32(FldValue); break;
                        case enumPrefs.donorid10:                   DonorId10 = Convert.ToInt32(FldValue); break;
                        case enumPrefs.donorid11:                   DonorId11 = Convert.ToInt32(FldValue); break;
                        case enumPrefs.donorid12:                   DonorId12 = Convert.ToInt32(FldValue); break;
                        case enumPrefs.donorid13:                   DonorId13 = Convert.ToInt32(FldValue); break;
                        case enumPrefs.donorid14:                   DonorId14 = Convert.ToInt32(FldValue); break;
                        case enumPrefs.donorid15:                   DonorId15 = Convert.ToInt32(FldValue); break;
                        case enumPrefs.donorid16:                   DonorId16 = Convert.ToInt32(FldValue); break;
                        case enumPrefs.donorid17:                   DonorId17 = Convert.ToInt32(FldValue); break;
                        case enumPrefs.donorid18:                   DonorId18 = Convert.ToInt32(FldValue); break;
                        case enumPrefs.donorid19:                   DonorId19 = Convert.ToInt32(FldValue); break;
                        case enumPrefs.donorid20:                   DonorId20 = Convert.ToInt32(FldValue); break;
                        case enumPrefs.donorid2harvest:             DonorID2ndHarvest = Convert.ToInt32(FldValue); break;
                        case enumPrefs.donoridefap:                 DonorIDEFAP = Convert.ToInt32(FldValue); break;
                        case enumPrefs.donoridfll:                  DonorIDFLL = Convert.ToInt32(FldValue); break;
                        case enumPrefs.donoridnwh:                  DonorIDNWH = Convert.ToInt32(FldValue); break;
                        case enumPrefs.donoridtefap:                DonorIDTEFAP = Convert.ToInt32(FldValue); break;
                        case enumPrefs.donorpercentcalcmethod:
                            if (FldValue == "0")
                                DonorPercentCalcMethod = DonorCalcPercentMethod.LbsServed;
                            else
                                DonorPercentCalcMethod = DonorCalcPercentMethod.LbsDonated;
                            break;
                        case enumPrefs.efapleadagency:              EFAPLeadAgency = FldValue; break;
                        case enumPrefs.emailaddress:                EmailAddress = FldValue; break;
                        case enumPrefs.enableadditionalhhmdatatab:  EnableAdditionalHHMDataTab = BoolValue; break;
                        case enumPrefs.enableappointments:          EnableAppointments = BoolValue; break;
                        case enumPrefs.enablebabyservices:          EnableBabyServices = BoolValue; break;
                        case enumPrefs.enablebackpack:              EnableBackPack = BoolValue; break;
                        case enumPrefs.enablebarcodeprompts:        EnableBarcodePrompts = BoolValue; break;
                        case enumPrefs.enablecashdonations:         EnableCashDonations = BoolValue; break;
                        case enumPrefs.enablecdbgreporting:         EnableCDBGReporting = BoolValue; break;
                        case enumPrefs.enablechristmasprogram:      EnableChristmasProgram = BoolValue; break;
                        case enumPrefs.enableclientphone:           EnableClientPhone = BoolValue; break;
                        case enumPrefs.enableclientreceipt:         EnableClientReceipt = BoolValue; break;
                        case enumPrefs.enablecsfp:                  EnableCSFP = BoolValue; break;
                        case enumPrefs.enablecsfponnewsvc:          EnableCSFPOnNewSvc = BoolValue; break;
                        case enumPrefs.enablecsfpshowroutes:        EnableCSFPShowRoutes = BoolValue; break;
                        case enumPrefs.enableethnicityhhmtab:       EnableEthnicityHHMTab = BoolValue; break;
                        case enumPrefs.enablefasttrack:             EnableFastTrack = BoolValue; break;
                        case enumPrefs.enablefooddonations:         EnableFoodDonations = BoolValue; break;
                        case enumPrefs.enablefoodservices:          EnableFoodServices = BoolValue; break;
                        case enumPrefs.enableftscale:               EnableFTscale = BoolValue; break;
                        case enumPrefs.enablehhuserdefinedfieldstab: EnableHHUserDefinedFields = BoolValue; break;
                        case enumPrefs.enablehomedeliv:             EnableHomeDeliv = BoolValue; break;
                        case enumPrefs.enablehouseholdincome:       EnableHouseholdIncome = BoolValue; break;
                        case enumPrefs.enablehudcategory:           EnableHUDCategory = BoolValue; break;
                        case enumPrefs.enableidflds:                EnableIDFlds = BoolValue; break;
                        case enumPrefs.enablepointstracking:        EnablePointsTracking = BoolValue; break;
                        case enumPrefs.enableprintclientcard:       EnablePrintClientCard = BoolValue; break;
                        case enumPrefs.enableschsupply:             EnableSchSupply = BoolValue; break;
                        case enumPrefs.enableservicegroups:         EnableServiceGroups = BoolValue; break;
                        case enumPrefs.enablesupplemental:          EnableSupplemental = BoolValue; break;
                        case enumPrefs.enabletefap:                 EnableTEFAP = BoolValue; break;
                        case enumPrefs.enabletransportation:        EnableTransportation = BoolValue; break;
                        case enumPrefs.enableverifyid:              EnableVerifyId = BoolValue; break;
                        case enumPrefs.enablevolunteerhours:        EnableVolunteerHours = BoolValue; break;
                        case enumPrefs.enablevouchers:              EnableVouchers = BoolValue; break;
                        case enumPrefs.enableworksinarea:           EnableWorksInArea = BoolValue; break;
                        case enumPrefs.faxnumber:                   FaxNumber = FldValue; break;
                        case enumPrefs.filterperiodfromaddress:     FilterPeriodFromAddress = BoolValue; break;
                        case enumPrefs.findclientautorefresh:       FindClientAutoRefresh = BoolValue; break;
                        case enumPrefs.fiscalyearstartmonth:        FiscalYearStartMonth = Convert.ToInt32(FldValue); break;
                        case enumPrefs.foodbankname:                FoodBankName = FldValue; break;
                        case enumPrefs.ftlbsincludecommoditywt:     FTLbsIncludeCommodityWt = BoolValue; break;
                        case enumPrefs.includecommoditylbsincoalition: IncludeCommodityLbsInCoalition = BoolValue; break;
                        case enumPrefs.includecommoditylbsinfoodlifeline: IncludeCommodityLbsInFoodLifeline = BoolValue; break;
                        case enumPrefs.includecommoditylbsinnorthwestharvest: IncludeCommodityLbsInNorthwestHarvest = BoolValue; break;
                        case enumPrefs.includecommoditylbsinsecondharvestinland: IncludeCommodityLbsInCoalition = BoolValue; break;
                        case enumPrefs.includelbsonsvclist:         IncludeLbsOnSvcList = BoolValue; break;
                        case enumPrefs.inkinddollarsperhour:        InkindDollarsPerHr = Convert.ToDecimal(FldValue); break;
                        case enumPrefs.inkinddollarsperlb:          InkindDollarsPerLb = Convert.ToDecimal(FldValue); break;
                        case enumPrefs.maxpointsperweek:            MaxPointsPerWeek = Convert.ToInt32(FldValue); break;
                        case enumPrefs.mergeteens:                  MergeTeens = BoolValue; break;
                        case enumPrefs.mustbeacommodityday:         MustBeACommodityDay = BoolValue; break;
                        case enumPrefs.nbrdaysallowmods:            NbrDaysAllowMods = Convert.ToInt32(FldValue); break;
                        case enumPrefs.nbrmealsperservice:          NbrMealsPerService = Convert.ToInt32(FldValue); break;
                        case enumPrefs.nbrsvcdatesfuture:           NbrSvcDatesFuture = Convert.ToInt32(FldValue); break;
                        case enumPrefs.nbrsvcdatespast:             NbrSvcDatesPast = Convert.ToInt32(FldValue); break;
                        case enumPrefs.overridelevel:               OverRideLevel = Convert.ToInt32(FldValue); break;
                        case enumPrefs.phonenumber:                 PhoneNumber = FldValue; break;
                        case enumPrefs.physicaladdress:             PhysicalAddress = FldValue; break;
                        case enumPrefs.pointsperfammbr:             PointsPerFamMbr = Convert.ToInt32(FldValue); break;
                        case enumPrefs.pointsperweek:               PointsPerWeek = Convert.ToInt32(FldValue); break;
                        case enumPrefs.pointsperweekoutofarea:      PointsPerWeekOutOfArea = Convert.ToInt32(FldValue); break;
                        case enumPrefs.postaladdress:               PostalAddress = FldValue; break;
                        case enumPrefs.preparedby:                  PreparedBy = FldValue; break;
                        case enumPrefs.reportssavepath:             ReportsSavePath = FldValue; break;
                        case enumPrefs.servicelogrefreshrate:       ServiceLogRefreshRate = Convert.ToInt32(FldValue); break;
                        case enumPrefs.servicemenutype:             ServiceMenuType = Convert.ToInt32(FldValue); break;
                        case enumPrefs.transientid:                 TransientId = Convert.ToInt32(FldValue); break;
                        case enumPrefs.usecalendarweeks:            UseCalendarWeeks = BoolValue; break;
                        case enumPrefs.usefamilylist:
                            if (FldValue == "0")
                                UseFamilyList = UseFamilyListCode.Normally;
                            else if (FldValue == "1")
                                UseFamilyList = UseFamilyListCode.Sometimes;
                            else if (FldValue == "2")
                                UseFamilyList = UseFamilyListCode.Always;
                            else
                                UseFamilyList = UseFamilyListCode.Never;
                            break;
                        case enumPrefs.uselocaloutofareaalerts:     UseLocalOutOfAreaAlerts = BoolValue; break;
                        case enumPrefs.usetimeinoutforvols:         UseTimeInOutForVols = BoolValue; break;
                        case enumPrefs.warnsvceachperson:           WarnSvcEachPerson = BoolValue; break;
                        default:
                            CCFBGlobal.appendErrorToErrorReport(Enum.GetName(typeof(enumPrefs), a), "Load Preferences");
                            break;
                    }

                }
                catch (Exception ex)
                {
                    CCFBGlobal.appendErrorToErrorReport(ex.Message, "Load Preferences get fldname");
                }

            }
            conn.Dispose();
            sqlCmd.Dispose();
            sqlReader.Dispose();
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
            conn.Dispose();
            command.Dispose();
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
