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
        #endregion
        
        #region Features
        public static bool EnableFoodServices = true;
        public static bool EnableAppointments = false;
        public static bool EnableFoodDonations = true;
        public static bool EnableCashDonations = true;
        public static bool EnableVolunteerHours = true;
        public static bool EnablePrintClientCard = true;
        public static bool EnableVouchers = false;
        public static bool EnableCSFP = false;
        public static bool EnableTEFAP = true;
        public static bool MustBeACommodityDay = true;
        public static bool EnableSupplemental = true;
        public static bool EnableClientPhone = true;
        public static bool EnableVerifyId = true;
        public static bool EnableHouseholdIncome = false;
        public static bool EnableHHUserDefinedFields = true;
        public static bool EnableBabyServices = true;
        public static bool EnableWorksInArea = false;
        public static bool EnableAdditionalHHMDataTab = false;
        public static bool EnableEthnicityHHMTab = false;
        public static bool UseTimeInOutForVols = false;
        public static bool EnableBarcodePrompts = false;
        #endregion

        #region Add Client Options
        public static string DefaultCity = "";
        public static string DefaultState = "WA";
        public static string DefaultZipcode = "";
        public static bool AllowDuplicateHHNames = false;
        public static bool AllowDuplicateMemberNames = true;
        public enum UseFamilyListCode { Normally = 0, Sometimes = 1, Always = 2, Never = 3 };
        public static UseFamilyListCode UseFamilyList = UseFamilyListCode.Normally;
        #endregion
        
        #region Form Options
        public static bool FindClientAutoRefresh = false;
        public static int ServiceLogRefreshRate = 60;
        public static int ApptLogRefreshRate = 90;
        public static int NbrMealsPerService = 10;
        public static int NbrDaysAllowMods = 30;
        public static int NbrSvcDatesFuture = 7;
        public static int NbrSvcDatesPast = 45;
        #endregion

        #region Family Card Slots
        public static int FamilyCardSlot1 = -1;
        public static int FamilyCardSlot2 = -1;
        public static int FamilyCardSlot3 = -1;
        public static int FamilyCardSlot4 = -1;
        #endregion

        #region Monthly Reports
        public static int FiscalYearStartMonth = 7;
        public static decimal InkindDollarsPerHr = 10;
        public static decimal InkindDollarsPerLb = 1.5m;
        public static Int32 DonorId01 = 1;
        public static Int32 DonorId02 = 2;
        public static Int32 DonorId03 = 3;
        public static Int32 DonorId04 = 4;
        public static Int32 DonorId05 = 0;
        public static Int32 DonorId06 = 0;
        public static Int32 DonorId07 = 0;
        public static Int32 DonorId08 = 0;
        public static Int32 DonorId09 = 0;
        public static Int32 DonorId10 = 0;
        public static bool IncludeCommodityLbsInCoalition = true;
        public static bool IncludeCommodityLbsInFoodLifeline = true;
        public static bool includecommoditylbsinnorhtwestharvest = true;
        public static bool includecommoditylbsinsecondharvestinland = true;
        public enum DonorCalcPercentMethod { LbsServed = 0, LbsDonated = 1 };
        public static DonorCalcPercentMethod DonorPercentCalcMethod = DonorCalcPercentMethod.LbsServed;
        public static string PreparedBy = "Director";
        public static string ReportsSavePath = @"C:\ClientcardFB3\Reports\";
        public static int TransientId = 3;
        #endregion

        public static void Init()   //Load data from Defaults table
        {
            SqlConnection conn = new SqlConnection(CCFBGlobal.connectionString);
            SqlCommand command = new SqlCommand("SELECT * FROM " + tbName + " ORDER BY EditForm, FldName", conn);
            SqlDataAdapter dadAdpt = new SqlDataAdapter(command);
            DataSet dset = new DataSet();
            int NbrRows = dadAdpt.Fill(dset);
            foreach (DataRow drow in dset.Tables[0].Rows)
            {
                switch (drow.Field<string>("fldname").ToLower())
                {
                    case "foodbankname": FoodBankName = drow.Field<string>(constFldValue); break;
                    case "postaladdress": PostalAddress = drow.Field<string>(constFldValue); break;
                    case "physicaladdress": PhysicalAddress = drow.Field<string>(constFldValue); break;
                    case "phonenumber": PhoneNumber = drow.Field<string>(constFldValue); break;
                    case "county": County = drow.Field<string>(constFldValue); break;
                    case "emailaddress": EmailAddress = drow.Field<string>(constFldValue); break;
                    case "faxnumber": FaxNumber = drow.Field<string>(constFldValue); break;
                    case "enablebarcodeprompts": EnableBarcodePrompts = drow.Field<bool>(constBoolVal); break;
                    case "enablefoodservices": EnableFoodServices = drow.Field<bool>(constBoolVal); break;
                    case "enableappointments": EnableAppointments = drow.Field<bool>(constBoolVal); break;
                    case "enablefooddonations": EnableFoodDonations = drow.Field<bool>(constBoolVal); break;
                    case "enablecashdonations": EnableCashDonations = drow.Field<bool>(constBoolVal); break;
                    case "enablevolunteerhours": EnableVolunteerHours = drow.Field<bool>(constBoolVal); break;
                    case "enableprintclientcard": EnablePrintClientCard = drow.Field<bool>(constBoolVal); break;
                    case "enablevouchers": EnableVouchers = drow.Field<bool>(constBoolVal); break;
                    case "enablecsfp": EnableCSFP = drow.Field<bool>(constBoolVal); break;
                    case "enabletefap": EnableTEFAP = drow.Field<bool>(constBoolVal); break;
                    case "mustbeacommodityday": MustBeACommodityDay = drow.Field<bool>(constBoolVal); break;
                    case "enablebabyservices": EnableBabyServices = drow.Field<bool>(constBoolVal); break;
                    case "enablesupplemental": EnableSupplemental = drow.Field<bool>(constBoolVal); break;
                    case "enableclientphone": EnableClientPhone = drow.Field<bool>(constBoolVal); break;
                    case "enableverifyid": EnableVerifyId = drow.Field<bool>(constBoolVal); break;
                    case "enablehouseholdincome": EnableHouseholdIncome = drow.Field<bool>(constBoolVal); break;
                    case "enablehhuserdefinedfieldstab": EnableHHUserDefinedFields = drow.Field<bool>(constBoolVal); break;
                    case "enableadditionalhhmdatatab": EnableAdditionalHHMDataTab = drow.Field<bool>(constBoolVal); break;
                    case "enableethnicityhhmtab": EnableEthnicityHHMTab = drow.Field<bool>(constBoolVal); break;
                    case "enableworksinarea": EnableWorksInArea = drow.Field<bool>(constBoolVal); break;
                    case "defaultcity": DefaultCity = drow.Field<string>(constFldValue); break;
                    case "defaultstate": DefaultState = drow.Field<string>(constFldValue); break;
                    case "defaultzipcode": DefaultZipcode = drow.Field<string>(constFldValue); break;
                    case "allowduplicatehhnames": AllowDuplicateHHNames = drow.Field<bool>(constBoolVal); break;
                    case "allowduplicatemembernames": AllowDuplicateMemberNames = drow.Field<bool>(constBoolVal); break;
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
                    case "findclientautorefresh": FindClientAutoRefresh = drow.Field<bool>(constBoolVal); break;
                    case "servicelogrefreshrate": ServiceLogRefreshRate = Convert.ToInt32(drow[constFldValue]); break;
                    case "apptlogrefreshrate": ApptLogRefreshRate = Convert.ToInt32(drow[constFldValue]); break;
                    case "nbrmealsperservice": NbrMealsPerService = Convert.ToInt32(drow[constFldValue]); break;
                    case "nbrdaysallowmods": NbrDaysAllowMods = Convert.ToInt32(drow[constFldValue]); break;
                    case "nbrsvcdatesfuture": NbrSvcDatesFuture = Convert.ToInt32(drow[constFldValue]); break;
                    case "nbrsvcdatespast": NbrSvcDatesPast = Convert.ToInt32(drow[constFldValue]); break;
                    case "familycardslot1": FamilyCardSlot1 = Convert.ToInt32(drow[constFldValue]); break;
                    case "familycardslot2": FamilyCardSlot2 = Convert.ToInt32(drow[constFldValue]); break;
                    case "familycardslot3": FamilyCardSlot3 = Convert.ToInt32(drow[constFldValue]); break;
                    case "familycardslot4": FamilyCardSlot4 = Convert.ToInt32(drow[constFldValue]); break;
                    case "fiscalyearstartmonth": FiscalYearStartMonth = Convert.ToInt32(drow[constFldValue]); break;
                    case "inkinddollarsperhour": InkindDollarsPerHr = Convert.ToDecimal(drow[constFldValue]); break;
                    case "inkinddollarsperlb": InkindDollarsPerLb = Convert.ToDecimal(drow[constFldValue]); break;
                    case "donorid01": DonorId01 = Convert.ToInt32(drow[constFldValue]); break;
                    case "donorid02": DonorId02 = Convert.ToInt32(drow[constFldValue]); break;
                    case "donorid03": DonorId03 = Convert.ToInt32(drow[constFldValue]); break;
                    case "donorid04": DonorId04 = Convert.ToInt32(drow[constFldValue]); break;
                    case "donorid05": DonorId05 = Convert.ToInt32(drow[constFldValue]); break;
                    case "donorid06": DonorId06 = Convert.ToInt32(drow[constFldValue]); break;
                    case "donorid07": DonorId07 = Convert.ToInt32(drow[constFldValue]); break;
                    case "donorid08": DonorId08 = Convert.ToInt32(drow[constFldValue]); break;
                    case "donorid09": DonorId09 = Convert.ToInt32(drow[constFldValue]); break;
                    case "donorid10": DonorId10 = Convert.ToInt32(drow[constFldValue]); break;
                    case "includecommoditylbsincoalition": IncludeCommodityLbsInCoalition = drow.Field<bool>(constBoolVal); break;
                    case "includecommoditylbsinfoodlifeline": IncludeCommodityLbsInFoodLifeline = drow.Field<bool>(constBoolVal); break;
                    case "includecommoditylbsinnorhtwestharvest": includecommoditylbsinnorhtwestharvest = drow.Field<bool>(constBoolVal); break;
                    case "includecommoditylbsinsecondharvestinland": IncludeCommodityLbsInCoalition = drow.Field<bool>(constBoolVal); break;
                    case "donorpercentcalcmethod":
                        if (drow[constFldValue].ToString() == "0")
                            DonorPercentCalcMethod = DonorCalcPercentMethod.LbsServed;
                        else
                            DonorPercentCalcMethod = DonorCalcPercentMethod.LbsDonated;
                        break;
                    case "preparedby":      PreparedBy = drow.Field<string>(constFldValue); break;
                    case "reportssavepath": ReportsSavePath = drow.Field<string>(constFldValue); break;
                    case "transientid": TransientId = Convert.ToInt32(drow[constFldValue]); break;
                    case "usetimeinoutforvols": UseTimeInOutForVols = drow.Field<bool>(constBoolVal); break;
                }
            }
        }

        public static bool SaveValue(string fldname, string newvalue)
        {
            return (doUpdate("Update " + tbName 
                            + " SET FldVal = '" + newvalue 
                            + "' WHERE FldName = '" + fldname + "'") > 0);
        }
        public static bool SaveValue(string fldname, bool newvalue)
        {
            return (doUpdate("Update " + tbName 
                            + " SET BoolVal = " + Convert.ToInt16(newvalue).ToString() 
                            + " WHERE FldName = '" + fldname + "'") > 0);
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
    }
}
