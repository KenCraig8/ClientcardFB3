using System;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Globalization;
using System.Runtime;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace ClientcardFB3
{
    class CCFBGlobal
    {
        const string constLocalServer = "(Local)\\SQLExpress";
        public const string registryKeyCurrentUser = @"HKEY_CURRENT_USER\Software\CSDG\ClientcardFB3";
        public const string ODBC_INI_REG_PATH = @"HKEY_CURRENT_USER\SOFTWARE\ODBC\ODBC.INI\ClientcardFB3";
        public const string regsubkeyServer = "SQLServer";
        public const string regsubkeySavePath = "SaveBackupPath";
        public const string regsubkeyUser = "CCFBUser";
       
        public static string DefaultDatabase = "ClientcardFB3";
        public const string OURNULLDATE = "01/01/1900";
        public const string OUROTHERNULLDATE = "1/1/1900";
        public const string password = "19800612";

        public const int itemRule_Always = 0;
        public const int itemRule_OncePerMonth = 1;
        public const int itemRule_SecondService = 2;
        public const int itemRule_ManualSelection = 3;
        public const int itemRule_SpecialService = 4;
        public const int itemRule_OncePerWeek = 5;
        public const int itemRule_HomelessTransient = 6;
        public const int itemRule_MaskArray = 7;
        

        public const int svcCat_NoSelection = 0;
        public const int svcCat_Standard = 1;
        public const int svcCat_Other = 2;
        public const int svcCat_Commodity = 3;
        public const int svcCat_Supplemental = 4;
        public const int svcCat_NonFood = 5;
        public const int svcCat_BabySvc = 6;

        public const int ageGroup_Infant = 0;
        public const int ageGroup_Youth = 1;
        public const int ageGroup_Teen = 2;
        public const int ageGroup_Eighteen = 3;
        public const int ageGroup_Adult = 4;
        public const int ageGroup_Senior = 5;

        public const int statusTrxLog_Service = 0;
        public const int statusTrxLog_FastTrack = 1;
        public const int statusTrxLog_NewAppt = 2;
        public const int statusTrxLog_NoShow = 3;

        public static DateTime FBNullDateValue = Convert.ToDateTime(OURNULLDATE);
        public static string sq1ServerName ="";
        public static string serverName = "";
        public static string connectionString = "";
        public static int connectionTimeout = 10;
        public static string pcName = "";

        public const int permissions_Admin = 2;
        public const int permissions_IntakeAdmin = 1;
        public const int permissions_Intake = 0;

        public const string nameUserRole_Admin = "Admin";
        public const string nameUserRole_IntakeAdmin = "IntakeAdmin";
        public const string nameUserRole_Intake = "Intake";

        public static Color bkColorBaseEdit = Color.LightGreen;
        public static Color bkColorHasFocus = Color.WhiteSmoke;
        public static Color bkColorAltEdit = Color.PaleGreen;
        public static Color bkColorFormDflt = Color.Wheat;
        public static Color bkColorFormAlt = Color.LightYellow;
        public static Color bkColorApptEdit = Color.PaleTurquoise;
        public static Color AgeBirthdateColor = Color.DodgerBlue;

        public static int currentUser_PermissionLevel = 1;
        public static string currentUser_Name = "";
        public static string dbUserName = "CCFB";
        public static bool HaveCommodities = false;
        public static bool HaveCSFP = false;
        public static bool ServiceItemsChanged = true;

        public static int fiscalYearStartMonth = 1;
        static ListView lvToExport;
        static DataGridView dgvToExport;
        public static System.Collections.ArrayList typeCodeLists;
        /// <summary>
        /// This string is used when the input form has a value stored in the TypeCode field that
        /// is not in the TypeCode table.
        /// </summary>
        public const string INVALID_TYPE_CODE = "No Selection";
        static string locationToSave;
//        public static DailyItemsClass clsDailyItems;
        public static string DefaultServiceDate = "";
        public static string DefalutApptDate = "";

        public static string parmTbl_Client = "parm_ClientType";
        public static string parmTbl_Donation = "parm_DonationType";
        public static string parmTbl_Donor = "parm_DonorType";
        public static string parmTbl_EducationLevel = "parm_EducationLevel";
        public static string parmTbl_Employment = "parm_EmploymentStatus";
        public static string parmTbl_FoodClass = "parm_FoodClass";
        public static string parmTbl_Gender = "parm_Gender";
        public static string parmTbl_IdVerify = "parm_IDType";
        public static string parmTbl_SvcCategory = "parm_SvcCategory";
        public static string parmTbl_SvcRules = "parm_SvcRules";
        public static string parmTbl_Language = "parm_LanguageType";
        public static string parmTbl_MilitaryDischarge = "parm_MilitaryDischarge";
        public static string parmTbl_MilitaryService = "parm_MilitaryService";
        public static string parmTbl_Phone = "parm_PhoneType";
        public static string parmTbl_VolGroups = "parm_VolunteerGroups";
        public static string parmTbl_VolType = "parm_VolunteerType";
        public static string parmTbl_CSFPRoutes = "parm_CSFPRoutes";
        public static string parmTbl_IncomeProcessID = "parm_IncomeProcessID";
        public static string parmTbl_FBProgram = "parm_FBProgram";
        public static string parmTbl_FBJobs = "parm_FBJobs";
        public static string parmTbl_VoucherType = "parm_VoucherType";
        public static string parmTbl_ServiceMethod = "parm_ServiceMethod";
        public static string parmTbl_HDRouteSheetStatus = "parm_HDRouteSheetStatus";
        public static string parmTbl_TrueFalse = "parm_TrueFalse";
        public static string parmTbl_YesNoUnk = "parm_YesNoUnk";
        public static string parmTbl_HDPrograms = "parm_HDPrograms";
        public static string parmTbl_HUDCategory = "parm_HUDCategory";

        public enum ServiceMethodCodes
        {
            Pickup = 0,
            AlternatePickup = 1,
            HomeDelivery = 2,
            OneTimeService = 3
        }

        public struct YearStuff
        {
            public string CurFiscalYrStartDate;
            public string CurFiscalYrEndDate;
            public string PrevFiscalYrStartDate;
            public string PrevFiscalYrEndDate;
            public string NextFiscalYrStartDate;
            public string NextFiscalYrEndDate;
        }

        public static YearStuff FiscalYearStuff;

        /// <summary>
        /// Calculates the age for a given birthdagte using any given date
        /// </summary>
        /// <param name="BirthDay">The birthdate of the client</param>
        /// <param name="TestDate">The date to find Age from</param>
        /// <returns>The age</returns>
        public static int calcAge(DateTime BirthDay, DateTime TestDate)
        {
            if (BirthDay <= TestDate)
            {
                int age = TestDate.Year - BirthDay.Year;
                if (BirthDay > TestDate.AddYears(-age)) age--;

                return age;
            }
            return 0;
        }

        public static DateTime calcBirthdateFromAge(int age)
        {
            if (DateTime.Today.Month < 7)
                return new DateTime(DateTime.Today.Year - age - 1, 07, 01);
            else
                return new DateTime(DateTime.Today.Year - age, 07, 01);
        }

        /// <summary>
        /// Tells if the computer is connected to a network or not
        /// </summary>
        /// <returns>True = Connected To Network</returns>
        public static bool isConnectedToNetwork()
        {
            Microsoft.VisualBasic.Devices.Network network = new Microsoft.VisualBasic.Devices.Network();
            return network.IsAvailable;
        }

        ///Creating the extern function... 
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);

        /// <summary>
        /// Creating a function that uses the API function... 
        /// </summary>
        /// <returns>True = Connected To Internet</returns>
        public static bool IsConnectedToInternet()
        {
            int Desc;
            return InternetGetConnectedState(out Desc, 0);
        }

        /// <summary>
        /// Return the Connection info as a sting value 
        /// </summary>
        /// <returns>String representation of online or offline</returns>
        public static String ConnectionInfo()
        {
            String state;
            if (IsConnectedToInternet() == true)
            {
                state = "Online";
            }
            else
            {
                state = "Offline";
            }
            return state;
        }

        /// <summary>
        /// Verifys that the file path exists and creates the path if it does not
        /// </summary>
        /// <param name="path">File Path</param>
        public static void verifyPath(string path)
        {
            char[] splitChar = { '\\' };
            string[] pathParts = path.Split(splitChar, StringSplitOptions.RemoveEmptyEntries);
            string pathToFind = pathParts[0] + "\\";
            for (int i = 1; i < pathParts.Length; i++)
            {
                pathToFind += pathParts[i] + "\\";

                if (!System.IO.Directory.Exists(pathToFind))
                {
                    System.IO.Directory.CreateDirectory(pathToFind);
                }
            }
        }

        /// <summary>
        /// Appends the error log with the error
        /// </summary>
        /// <param name="funtionParams">Any parameters the calling funtioin used</param>
        /// <param name="errorInfo">The info about the error</param>
        public static void appendErrorToErrorReport(string funtionParams, string errorInfo)
        {
            appendGeneralErrorInfo(funtionParams, errorInfo);
            string fileName = "ErrorLog.txt";
            string folderPath = @"C:\ClientcardFB3\Log";
            string filePath = folderPath + "\\" + fileName;
            string whiteSpace = " ";
            using (StreamWriter sw = File.AppendText(filePath))
            {              
                sw.Write(whiteSpace.PadLeft(3, ' '));
                sw.WriteLine(sq1ServerName);
            }  
        }

        /// <summary>
        /// Appends the error log with the general info about the error
        /// </summary>
        /// <param name="funtionParams">Any parameters the calling funtioin used</param>
        /// <param name="errorInfo">The info about the error</param>
        private static void appendGeneralErrorInfo(string funtionParams, string errorInfo)
        {
            string fileName = "ErrorLog.txt";
            string folderPath = @"C:\ClientcardFB3\Log";
            string filePath = folderPath + "\\" + fileName;
            string[] todaysDateFormats = DateTime.Now.GetDateTimeFormats();
            string now = todaysDateFormats[55];
            string whiteSpace = " ";

            // Use Path class to manipulate file and directory paths.
            string destFile = System.IO.Path.Combine(folderPath, fileName);

            // Create a new target folder, if necessary.
            verifyPath(folderPath);

                using (StreamWriter sw = File.AppendText(filePath))
                {
                    sw.WriteLine();
                    sw.WriteLine();
                    sw.Write(now);
                    sw.Write(whiteSpace.PadLeft(2, ' '));
                    sw.WriteLine(errorInfo);
                    sw.Write(whiteSpace.PadLeft(3, ' '));
                    sw.WriteLine(funtionParams);
                }
        }

        /// <summary>
        /// Check and Supresses Keypress if not a valid key for entering an integer
        /// </summary>
        /// <param name="e">KeyEventArguments for the keypress</param>
        public static void checkForIntOnKeyPress(KeyEventArgs e)
        {
            if ((e.KeyValue > 47 && e.KeyValue < 58)
               || (e.KeyValue > 95 && e.KeyValue < 106)
               || e.KeyCode == Keys.Back
               || e.KeyCode == Keys.Return
               || e.KeyCode == Keys.Delete
               || e.KeyCode == Keys.Tab
               || e.KeyCode == Keys.Left
               || e.KeyCode == Keys.Right
               || e.KeyCode == Keys.OemMinus
               || e.KeyCode == Keys.Subtract)
                e.SuppressKeyPress = false;
            else
                e.SuppressKeyPress = true;
        }

        /// <summary>
        /// Check and Supresses Keypress if not a valid key for entering an integer
        /// </summary>
        /// <param name="e">KeyEventArguments for the keypress</param>
        public static void checkForDecimalOnKeyPress(KeyEventArgs e)
        {
            if ((e.KeyValue > 47 && e.KeyValue < 58)
               || (e.KeyValue > 95 && e.KeyValue < 106)
               || e.KeyCode == Keys.Back
               || e.KeyCode == Keys.Return
               || e.KeyCode == Keys.Delete
               || e.KeyCode == Keys.Tab
               || e.KeyCode == Keys.Left
               || e.KeyCode == Keys.Right
               || e.KeyCode == Keys.OemMinus
               || e.KeyCode == Keys.Subtract
               || e.KeyCode == Keys.OemPeriod
               || e.KeyCode == Keys.Decimal)
                e.SuppressKeyPress = false;
            else
                e.SuppressKeyPress = true;
        }

        public static void dtPopulateCombo(ComboBox cbo,string sqlCmdText, string displayMember, string valueMember, string noValuesText, SqlConnection sqlConn)
        {
            SqlCommand sqlCmd = new SqlCommand(sqlCmdText, sqlConn);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = sqlCmd;

            DataTable table = new DataTable();
            table.Locale = System.Globalization.CultureInfo.InvariantCulture;
            try
            {
                adapter.Fill(table);
                cbo.DataSource = table;
                cbo.DisplayMember = displayMember;
                cbo.ValueMember = valueMember;

            }
            catch (Exception)
            {
                cbo.DataSource = null;
                cbo.Items.Add(noValuesText);
            }
        }
        
        public static void dtPopulateCombo(DataGridViewComboBoxColumn cbo, string sqlCmdText, string displayMember, string valueMember, string noValuesText, SqlConnection sqlConn)
        {
            SqlCommand sqlCmd = new SqlCommand(sqlCmdText, sqlConn);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = sqlCmd;

            DataTable table = new DataTable();
            table.Locale = System.Globalization.CultureInfo.InvariantCulture;
            try
            {
                adapter.Fill(table);
                cbo.DataSource = table;
                cbo.DisplayMember = displayMember;
                cbo.ValueMember = valueMember;

            }
            catch (Exception)
            {
                cbo.DataSource = null;
                cbo.Items.Add(noValuesText);
            }
        }

        /// <summary>
        /// Exports a listview to exvcell
        /// </summary>
        /// <param name="lvi">The ListView to export to Excel</param>
        /// <param name="saveName">The name that the excel file will be saved as</param>
        /// <returns>If the grid was successfully exported and file saved</returns>
        public static bool ExportToExcell(ListView lvi, string saveName)
        {
            lvToExport = lvi;
            locationToSave = "C:\\ClientcardFB3\\GridExports\\";
            verifyPath(locationToSave);
            string strExtension = ".xls";

            if (lvToExport != null)
            {
                try
                {
                    verifyPath(locationToSave);
                    saveName = checkFileExists(saveName, strExtension, locationToSave);
                    StreamWriter sw = new StreamWriter(locationToSave + saveName + strExtension, false);
                    sw.AutoFlush = true;
                    for (int col = 0; col < lvToExport.Columns.Count; col++)
                    {
                        sw.Write(lvToExport.Columns[col].Text.ToString() + "\t");
                    }

                    int rowIndex = 1;
                    int row = 0;
                    string st1 = "";
                    for (row = 0; row < lvToExport.Items.Count; row++)
                    {
                        if (rowIndex <= lvToExport.Items.Count)
                            rowIndex++;
                        st1 = "\n";
                        for (int col = 0; col < lvToExport.Columns.Count; col++)
                        {
                            if (lvToExport.Items[row].SubItems[col].Text.ToString() == "")
                            {
                                st1 += "NULL" + lvToExport.Items[row].SubItems[col].Text.ToString();
                            }
                            else
                            {
                                st1 += lvToExport.Items[row].SubItems[col].Text.ToString();
                            }
                            st1 = st1 + "\t";
                        }
                        sw.Write(st1);
                        //sw.Flush();
                    }
                    sw.Close();
                    FileInfo fil = new FileInfo(locationToSave + saveName);
                    if (fil.Exists == true)
                        return true;

                }
                catch (Exception ex)
                {
                    FileInfo file = new FileInfo(locationToSave + saveName + strExtension);
                    appendErrorToErrorReport(lvToExport.ToString(), ex.GetBaseException().ToString());
                    MessageBox.Show("ERROR: Could not export the grid.", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }

            return true;
        }

        /// <summary>
        /// Exports a grid to Excel
        /// </summary>
        /// <param name="dGv">The DataGridView to export to Excel</param>
        /// <param name="saveName">The name that the excel file will be saved as</param>
        /// <returns>If the grid was successfully exported and file saved</returns>
        public static bool ExportToExcell(DataGridView dGv, string saveName)
        {
            dgvToExport = dGv;
            locationToSave = "C:\\ClientcardFB3\\GridExports\\";
            verifyPath(locationToSave);
            string strExtension = ".xls";
            if (dgvToExport != null)
            {
                try
                {
                    
                    verifyPath(locationToSave);
                    saveName = checkFileExists(saveName, strExtension, locationToSave);
                    StreamWriter sw = new StreamWriter(locationToSave + saveName + strExtension, false);
                    sw.AutoFlush = true;
                    for (int col = 0; col < dgvToExport.Columns.Count; col++)
                    {
                        if (dgvToExport.Columns[col].Visible == true)
                        {
                            if (col == 0)
                            {
                                sw.Write(dgvToExport.Columns[col].HeaderText.ToLower() + "\t");
                            }
                            else
                            {
                                sw.Write(dgvToExport.Columns[col].HeaderText + "\t");
                            }
                        }
                    }

                    int rowIndex = 1;
                    int row = 0;
                    string st1 = "";
                    for (row = 0; row < dgvToExport.Rows.Count; row++)
                    {
                        if (rowIndex <= dgvToExport.Rows.Count)
                            rowIndex++;
                        st1 = "\n";
                        for (int col = 0; col < dgvToExport.Columns.Count; col++)
                        {
                            if (dgvToExport.Columns[col].Visible == true)
                            {
                                if (dgvToExport.Rows[row].Cells[col].Value.ToString() == "")
                                {
                                    st1 += "NULL" + dgvToExport.Rows[row].Cells[col].Value.ToString();
                                }
                                else
                                {
                                    st1 += dgvToExport.Rows[row].Cells[col].Value.ToString();
                                }
                                st1 = st1 + "\t";
                            }
                        }
                        sw.Write(st1);
                    }
                    sw.Close();
                    FileInfo file = new FileInfo(locationToSave + saveName + strExtension);
                    if (file.Exists == true)
                        return true;
                }
                catch (Exception ex)
                {
                    FileInfo file = new FileInfo(locationToSave + saveName + strExtension);
                    appendErrorToErrorReport(dgvToExport.ToString(), ex.GetBaseException().ToString());
                    MessageBox.Show("ERROR: Could not export the grid.", "Error",
                       MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks if file eixists and prompts user if they 
        /// want to overwrite file or create new one
        /// </summary>
        /// <param name="saveName">The name of the file to save</param>
        /// <param name="strExtension">The extension of the file to save (ie .xls, .doc)</param>
        /// <param name="saveLocation">The path of where to save to file</param>
        /// <returns>The full save name</returns>
        private static string checkFileExists(string saveName, string strExtension, string saveLocation)
        {
            FileInfo fil = new FileInfo(saveLocation + saveName + strExtension);
            if (fil.Exists == true)
            {
                if (MessageBox.Show(formatStringWithCapitalization(
                    "this file Already Exists. click Yes to overwirte Existing File Or "
                    + "No To Keep Existing File And Create A New One"), "File Exists",
                     MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    saveName = saveName.Replace(strExtension, "");
                    for (int i = 1; i < 999; i++)
                    {
                        fil = new FileInfo(locationToSave + saveName + strExtension);
                        if (fil.Exists == true)
                        {
                            saveName = saveName.Replace("(" + formatNumberWithThreeLeadingZeros(i - 1) + ")", "");
                            saveName += "(" + formatNumberWithThreeLeadingZeros(i) + ")";
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            return saveName;
        }

        public static int MaxValue(int value1, int value2)
        {
            if (value1 > value2)
            { return value1; }
            return value2;
        }

        public static int MinValue(int value1, int value2)
        {
            if (value1 > value2)
            { return value2; }
            return value1;
        }

        /// <summary>
        /// Checks for null value and either returns the integer 
        /// if no null value found or returns 0 if it is null
        /// </summary>
        /// <param name="value">The value to check for null</param>
        /// <returns>The integer value</returns>
        public static int NullToZero(object value)
        {
            if (value == null)
                return 0;
            else if (value.ToString() == "")
                return 0;

                return Convert.ToInt32(value);
        }

        /// <summary>
        /// Checks for null value and either returns the bool 
        /// if no null value found or returns false if it is null
        /// </summary>
        /// <param name="value">The value to check for null</param>
        /// <returns>The bool value</returns>
        public static bool NullToFalse(object value)
        {
            if (value == null || Convert.ToString(value) == "")
                return false;
            else
                if (value.ToString() == "0" || value.ToString() == "False")
                    return false;
                else
                    return true;
        }

        /// <summary>
        /// Checks for null, if null returns blank 
        /// otherwise returns the string value
        /// </summary>
        /// <param name="value">THe Value to check for null from</param>
        /// <returns>A string value</returns>
        public static string NullToBlank(object value)
        {
            if (value == null)
                return "";
            else
                return value.ToString();
        }

        /// <summary>
        /// Returns the properly formated date as MM\DD\YYYY
        /// </summary>
        /// <param name="value">The DateTime value that needs formatiing</param>
        /// <returns>Formated string value</returns>
        public static string formatDate(DateTime value)
        {
            DateTimeFormatInfo dtfi = CultureInfo.CreateSpecificCulture("en-US").DateTimeFormat;
            dtfi.DateSeparator = @"/";
            dtfi.ShortDatePattern = @"MM/dd/yyyy";
            return value.ToString("d", dtfi);
        }
        /// <summary>
        /// Returns the properly formated date as YYYYMMDD
        /// </summary>
        /// <param name="value">The DateTime value that needs formatiing</param>
        /// <returns>Formated string value</returns>
        public static string formatDateYMD(DateTime value)
        {
            DateTimeFormatInfo dtfi = CultureInfo.CreateSpecificCulture("en-US").DateTimeFormat;
            dtfi.DateSeparator = @"/";
            dtfi.ShortDatePattern = @"yyyyMMdd";
            return value.ToString("d", dtfi);
        }

        /// <summary>
        /// Caplitalizes the first letter of wach word in string
        /// </summary>
        /// <param name="value">The string value that needs formatiing</param>
        /// <returns>Formated string value</returns>
        public static string formatStringWithCapitalization(string value)
        {
            TextInfo UsaTextInfo = new CultureInfo("en-US", false).TextInfo;
            return UsaTextInfo.ToTitleCase(value);
        }

        /// <summary>
        /// Formats the given int value to include commas where needed
        /// </summary>
        /// <param name="value">The value to convert</param>
        /// <returns>A string value with commas</returns>
        public static string formatNumberWithCommas(int value)
        {
            if (value != 0)
                return String.Format("{0:#,0}", value);
            else
                return "0";
        }

        /// <summary>
        /// Formats the given string value to include commas 
        /// and have decimal place with 2 values
        /// </summary>
        /// <param name="value">The value to convert</param>
        /// <returns>A string value with commas</returns>
        public static string formatNumberWithCommas(string value)
        {
            try
            {
                double convertedVal = Convert.ToDouble(value);
                if (convertedVal != 0)
                    return String.Format("{0:#,#.##}", convertedVal);
                else
                    return "0";
            }
            catch { return "0"; }
        }

        /// <summary>
        /// Formats the given object value to include commas 
        /// and have decimal place with 2 values
        /// </summary>
        /// <param name="value">The value to convert</param>
        /// <returns>A string value with commas</returns>
        public static string formatNumberWithCommas(object value)
        {
            try
            {
                double convertedVal = Convert.ToDouble(value);
                if (convertedVal != 0)
                    return String.Format("{0:#,#.##}", convertedVal);
                else
                    return "0";
            }
            catch { return "0"; }
        }

        /// <summary>
        /// Formats the number to have a leading zero if value is less than 10
        /// </summary>
        /// <param name="value">The value integer that needs formating</param>
        /// <returns>The string value with leading zeros</returns>
        public static string formatNumberWithLeadingZero(object value)
        {
            try
            {
                Int64 convertedVal = Convert.ToInt64(value);
                if (convertedVal != 0)
                    return String.Format("{0:0#}", convertedVal);
                else
                    return "00";
            }
            catch { return "00"; }
        }

        /// <summary>
        /// Formats the given value to have 3 leading zeros where zeros are missing
        /// 10 converts to 0010
        /// </summary>
        /// <param name="value">The value integer that needs formating</param>
        /// <returns>The string value with leading zeros</returns>
        public static string formatNumberWithThreeLeadingZeros(object value)
        {
            try
            {
                Int64 convertedVal = Convert.ToInt64(value);
                if (convertedVal != 0)
                    return String.Format("{0:000#}", convertedVal);
                else
                    return "000";
            }
            catch { return "000"; }
        }

        public static string FormatPhone(string phonenbr)
        {
            if (phonenbr.Contains("(") == true)
            {
                return phonenbr;
            }

            string tmp = phonenbr.Replace(" ","").Replace("(","").Replace(")","").Replace("-","");
            if (tmp.Length > 0)
            {
                double number1 = Convert.ToDouble(tmp);
                return number1.ToString("(###) ###-####");
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// Opens the given file in a process outside of ClientcardFB3
        /// </summary>
        /// <param name="filePath"></param>
        public static void openDocumentOutsideCCFB(string filePath)
        {
            if (File.Exists(filePath.ToString()) == true)
            {
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.EnableRaisingEvents = false;
                proc.StartInfo.FileName = filePath;
                proc.Start();
            }
        }

        /// <summary>
        /// Validates that the given date is a valid DateTime
        /// </summary>
        /// <param name="objValue">The value to check</param>
        /// <returns>A Valid DateTime value</returns>
        public static DateTime ValidDate(object objValue)
        {
            try { return Convert.ToDateTime(objValue); }
            catch { return FBNullDateValue; }
        }

        /// <summary>
        /// Validates that the given date is a valid DateTime
        /// </summary>
        /// <param name="objValue">The value to check</param>
        /// <returns>A Valid ShortDateString</returns>
        public static string  ValidDateString(object objValue)
        {
            try 
            { 
                DateTime dt = Convert.ToDateTime(objValue);
                if (dt > FBNullDateValue)
                    return dt.ToShortDateString();
                else
                    return "";
            }
            catch { return ""; }
        }

        public static void LoadTypes()
        {
            typeCodeLists = new System.Collections.ArrayList();
            typeCodeLists.Add(new parmTypeCodes(parmTbl_Client,connectionString));
            typeCodeLists.Add(new parmTypeCodes(parmTbl_Donation,connectionString));
            typeCodeLists.Add(new parmTypeCodes(parmTbl_Donor,connectionString));
            typeCodeLists.Add(new parmTypeCodes(parmTbl_EducationLevel,connectionString));
            typeCodeLists.Add(new parmTypeCodes(parmTbl_Employment,connectionString));
            typeCodeLists.Add(new parmTypeCodes(parmTbl_FoodClass,connectionString));
            typeCodeLists.Add(new parmTypeCodes(parmTbl_IdVerify,connectionString));
            typeCodeLists.Add(new parmTypeCodes(parmTbl_SvcCategory,connectionString));
            typeCodeLists.Add(new parmTypeCodes(parmTbl_SvcRules, connectionString));
            typeCodeLists.Add(new parmTypeCodes(parmTbl_Language,connectionString));
            typeCodeLists.Add(new parmTypeCodes(parmTbl_MilitaryDischarge,connectionString));
            typeCodeLists.Add(new parmTypeCodes(parmTbl_MilitaryService,connectionString));
            typeCodeLists.Add(new parmTypeCodes(parmTbl_Phone,connectionString));
            typeCodeLists.Add(new parmTypeCodes(parmTbl_VolGroups,connectionString));
            typeCodeLists.Add(new parmTypeCodes(parmTbl_VolType,connectionString));
            typeCodeLists.Add(new parmTypeCodes(parmTbl_CSFPRoutes, connectionString));
            typeCodeLists.Add(new parmTypeCodes(parmTbl_IncomeProcessID, connectionString));
            typeCodeLists.Add(new parmTypeCodes(parmTbl_FBProgram, connectionString));
            typeCodeLists.Add(new parmTypeCodes(parmTbl_VoucherType, connectionString));
            typeCodeLists.Add(new parmTypeCodes(parmTbl_FBJobs, connectionString));
            typeCodeLists.Add(new parmTypeCodes(parmTbl_ServiceMethod, connectionString));
            //typeCodeLists.Add(new parmTypeCodes(parmTbl_HomeDeliveryRoutes, connectionString));
            typeCodeLists.Add(new parmTypeCodes(parmTbl_Gender, connectionString));
            typeCodeLists.Add(new parmTypeCodes(parmTbl_TrueFalse, connectionString));
            typeCodeLists.Add(new parmTypeCodes(parmTbl_YesNoUnk, connectionString));
            typeCodeLists.Add(new parmTypeCodes(parmTbl_HDPrograms, connectionString));
            typeCodeLists.Add(new parmTypeCodes(parmTbl_HUDCategory, connectionString));
        }

        public static void InitCombo(ComboBox cbo, string parmTbl)
        {
            //cbo.Items.Clear();
            System.Collections.ArrayList newList = new System.Collections.ArrayList(TypeCodesArray(parmTbl));
            if (newList != null && newList.Count > 0)
            {
                cbo.DataSource = newList;
                cbo.DisplayMember = "LongName";
                cbo.ValueMember = "UID";
            }
            else
            {
                cbo.DataSource = null;
                cbo.Items.Add("Not Initialized");
            }
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Return the index of the TypeCode with the specified long name (Type column in DB).
        /// </summary>
        /// <param name="a_typeCodeId">Lookup this type code value in the TypeCode list.</param>
        /// <returns>The index in the TypeCode list of the </returns>
        //-----------------------------------------------------------------------------------------
        public static int IdxFromLongName(int idxtable, string typeName)
        {
            return ((parmTypeCodes)typeCodeLists[idxtable]).GetId(typeName);
        }		// end of IdxFromLongName

        public static int IdxFromLongName(string tableName, string typeName)
        {
            for (int i = 0; i < typeCodeLists.Count; i++)
            {
                if (((parmTypeCodes)typeCodeLists[i]).ParmTable == tableName)
                    return ((parmTypeCodes)typeCodeLists[i]).GetId(typeName);
            }
            return -1;
        }		// end of IdxLongName

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Return the TypeCode long name string (Type column in DB) that corresponds to the
        /// specified TypeCode ID (primary key column).
        /// </summary>
        /// <param name="a_typeCodeId">Lookup this type code value in the TypeCode list.</param>
        /// <returns></returns>
        //-----------------------------------------------------------------------------------------
        public static string LongNameFromId(int idxtable, int typeCodeId)
        {
            return ((parmTypeCodes)typeCodeLists[idxtable]).GetLongName(typeCodeId);
        }		// end of LongName

        public static string LongNameFromId(string tablename, int typeCodeId)
        {
            for (int i = 0; i < typeCodeLists.Count; i++)
            {
                if (((parmTypeCodes)typeCodeLists[i]).ParmTable == tablename)
                    return ((parmTypeCodes)typeCodeLists[i]).GetLongName(typeCodeId);
            }
            return INVALID_TYPE_CODE;
        }		// end of IdxLongName

        public static string ShortNameFromId(string tablename, int typeCodeId)
        {
            for (int i = 0; i < typeCodeLists.Count; i++)
            {
                if (((parmTypeCodes)typeCodeLists[i]).ParmTable == tablename)
                    return ((parmTypeCodes)typeCodeLists[i]).GetShortName(typeCodeId);
            }
            return INVALID_TYPE_CODE;
        }		// end of IdxLongName

        public static System.Collections.ArrayList TypeCodesArray(string parmTable)
        {
            for (int i = 0; i < typeCodeLists.Count; i++)
            {
                if (((parmTypeCodes)typeCodeLists[i]).ParmTable == parmTable)
                    return ((parmTypeCodes)typeCodeLists[i]).TypeCodesArray;
            }
            return new System.Collections.ArrayList();
        }

        public static string Validate(object obj)
        {
            if (obj.GetType().ToString() == "System.DateTime")
                if (Convert.ToDateTime(obj) <= FBNullDateValue)
                    return "";
            return obj.ToString();
        }

        public static string SQLDateRangeCurMonth()
        {
            return " And TrxDate BETWEEN '"
                        + new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).ToString() + "'"
                        + " And '" + new DateTime(DateTime.Today.Year, DateTime.Today.Month,
                        DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month)).ToString() + "'";
        }

        public static string SQLDateRangeLast90Days()
        {
            return " And TrxDate BETWEEN '"
                        + DateTime.Today.AddDays(-90).ToString() + "'"
                        + " And '" + DateTime.Today.AddDays(1).ToString() + "'";
        }

        public static string SQLDateRangePrevMonth()
        {
            int prevMonth = 0;

            if (DateTime.Today.Month > 1)
                prevMonth = DateTime.Today.Month - 1;
            else
                prevMonth = 12;

            return " And TrxDate BETWEEN '"
                + new DateTime(DateTime.Today.Year, prevMonth, 1).ToString() + "'"
                + " And '" + new DateTime(DateTime.Today.Year, prevMonth,
                DateTime.DaysInMonth(DateTime.Today.Year, prevMonth)).ToString() + "'";
        }

        public static string SQLDateRangeCurYear()
        {
            return " And TrxDate BETWEEN '"
                + CurrentFiscalStartDate().ToShortDateString() + "'"
                + " And '" + CurrentFiscalEndDate().ToShortDateString() + "'";
        }

        public static string SQLDateRangePrevYear()
        {
            return " And TrxDate BETWEEN '"
                + PreviousFiscalStartDate().ToShortDateString() + "'"
                + " And '" + PreviousFiscalEndDate().ToShortDateString() + "'";
        }

        public static String CalcFiscalPeriod(DateTime TestDate)
        {
            int MonthStart = fiscalYearStartMonth;
            int CurrentYear = TestDate.Year;
            int FiscalMonth;
            int FiscalYear;
            if (MonthStart == 1)
            {
                FiscalYear = TestDate.Year;
                FiscalMonth = TestDate.Month;
            }
            else if (TestDate.Month >= MonthStart)
            { 
                FiscalYear = CurrentYear + 1;
                FiscalMonth = TestDate.Month - MonthStart + 1;
            }
            else
            { 
                FiscalYear = CurrentYear; 
                FiscalMonth = TestDate.Month + MonthStart - 1;
            }
            return FiscalYear.ToString() + CCFBGlobal.formatNumberWithLeadingZero(FiscalMonth);
        }

        public static DateTime CalcFiscalStartDate(DateTime TestDate)
        {
            int MonthStart = fiscalYearStartMonth;
            int CurrentYear = TestDate.Year;
            DateTime FiscalStart;
            if (TestDate.Month >= MonthStart)
            { FiscalStart = new DateTime(CurrentYear, MonthStart, 1); }
            else
            { FiscalStart = new DateTime(CurrentYear - 1, MonthStart, 1); }
            return FiscalStart;
        }

        public static DateTime CalcFiscalEndDate(DateTime TestDate)
        {
            return CalcFiscalStartDate(TestDate).AddYears(1).AddDays(-1);
        }


        public static DateTime CurrentFiscalStartDate()
        {
            int MonthStart = fiscalYearStartMonth;
            int CurrentYear = DateTime.Today.Year;
            DateTime FiscalStart;
            if (DateTime.Now.Month >= MonthStart)
            { FiscalStart = new DateTime(CurrentYear, MonthStart, 1); }
            else
            { FiscalStart = new DateTime(CurrentYear - 1, MonthStart, 1); }
            return FiscalStart;
        }

        public static DateTime CurrentFiscalEndDate()
        {
            return CurrentFiscalStartDate().AddYears(1).AddDays(-1);
        }

        public static DateTime PreviousFiscalEndDate()
        {
            return CurrentFiscalStartDate().AddDays(-1);
        }

        public static DateTime PreviousFiscalStartDate()
        {
            return CurrentFiscalStartDate().AddYears(-1);
        }

        public static int ClearFolder(string path)
        {
            int nbrFiles = 0;
            string[] files = Directory.GetFiles(path);
            foreach (string file in files)
            {
                nbrFiles++;
                File.Delete(file);
            }
            return nbrFiles;
        }

        public static int getClientFromBarCode(string barcodevalue)
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlCommand sqlCmd = new SqlCommand("if Exists(SELECT * FROM Household WHERE BarCode = " + barcodevalue + ")"
                              + " SELECT MAX(ID) FROM Household WHERE BarCode = " + barcodevalue + " ELSE SELECT 0", sqlConn);
            sqlConn.Open();
            int iResult = (int)sqlCmd.ExecuteScalar();
            sqlConn.Close();
            return iResult;
        }

        public static int getHHFromBarCode(string barcodevalue, ref int idHHM)
        {
            int iResult = 0;
            int maxLen = MinValue(12, barcodevalue.Length);
            if (maxLen > 0)
            {
                SqlConnection sqlConn = new SqlConnection(connectionString);
                SqlCommand sqlCmd = new SqlCommand("SELECT HouseholdID, ID FROM HouseholdMembers WHERE LEFT(MemIdNbr," + maxLen.ToString() + ") = '" + barcodevalue.Substring(0, maxLen) + "'", sqlConn);
                sqlConn.Open();
                SqlDataReader sqlReader = sqlCmd.ExecuteReader();
                while (sqlReader.Read())
                {
                    idHHM = sqlReader.GetInt32(1);
                    iResult = sqlReader.GetInt32(0);
                    break;
                }
                sqlConn.Close();
            }
            return iResult;
        }

        public static void setConnectionString(string server)
        {
            if (server != "" && server != null )
            {
                sq1ServerName = server;
                string[] tmp = server.Split('\\');
                for (int i = 0; i < tmp.Length; i++)
                {
                    if (tmp[i] != "")
                    {
                        serverName = tmp[i];
                        Registry.SetValue(@"HKEY_CURRENT_USER\Software\CSDG\ClientcardFB3", "ServerName", serverName);
                        break;
                    }
                }
            }
            connectionString = @"Server=" + sq1ServerName + ";initial catalog=" + DefaultDatabase + "; UID=CCFB_User; "
                + "PWD='" + password + "'; Trusted_Connection = False; Connect Timeout=" + connectionTimeout + ";";
        }

        public static string IsInactiveString(bool val)
        {
            if (val == false)
                return "........";
            else
                return "inactive";
        }
        
        public static void AppendTextWithComma(ref string textList, string newValue)
        {
            if (textList != "")
                textList += ",";
            textList += newValue;
        }

        public static int executeQuery(string sqlText)
        {
            int nbrRows = 0;
            SqlConnection sqlConn = new SqlConnection(connectionString);
            try
            {
                sqlConn.Open();
                SqlCommand sqlCMD = new SqlCommand(sqlText, sqlConn);
                nbrRows = sqlCMD.ExecuteNonQuery();
            }
            catch (SqlException ex) 
            {
                appendErrorToErrorReport("CCFBGlobal.executeQuery = " + sqlText,
                    ex.GetBaseException().ToString());
            }
            sqlConn.Close();
            return nbrRows;
        }

        public static string firstDayOfMonth(string testDate)
        {
            string[] tmp = testDate.Split('/');
            return tmp[0] + "/01/" + tmp[2];
        }

        public static DateTime FirstDayOfMonth(DateTime dateWork)
        {
            return new DateTime(dateWork.Year, dateWork.Month, 1);
        }

        public static string SQLApostrophe(string text)
        {
            return text.Replace("'", "''");
        }
        //default external path for database backup 
        public static string getSaveBackupPath()
        {
            return (string)Registry.GetValue(registryKeyCurrentUser, regsubkeySavePath, "");
        }

        public static string getRegSQLServer()
        {
            return (string)Registry.GetValue(registryKeyCurrentUser, regsubkeyServer, constLocalServer);
        }

        public static string getRegDlftUser()
        {
            return (string)Registry.GetValue(registryKeyCurrentUser, regsubkeyUser, "");
        }

        public static string getRegDSNDbName()
        {
            return (string)Registry.GetValue(ODBC_INI_REG_PATH, "DataBase", DefaultDatabase);
        }

        public static string getRegDSNDriverName()
        {
            string retText = "";
            retText = (string)Registry.GetValue(ODBC_INI_REG_PATH, "Driver", @"C:\windows\system32\SQLSRV32.dll");
            if (retText == "" || retText == null)
                retText = @"C:\windows\system32\SQLSRV32.dll";
            return retText;
        }

        public static string getRegDSNServer()
        {
            return (string)Registry.GetValue(ODBC_INI_REG_PATH, "Server", "");
        }

        public static string getRegUpdatePath()
        {
            string tmp = (string)Registry.GetValue(registryKeyCurrentUser, "UpdatePath", "");
            return tmp;
        }

        public static void saveRegSQLServer()
        {
            Registry.SetValue(registryKeyCurrentUser, regsubkeyServer, sq1ServerName);
        }

        public static void saveRegDfltUser()
        {
            Registry.SetValue(registryKeyCurrentUser, regsubkeyUser, currentUser_Name);
        }

        public static void saveRegUpdatePath(string updatepath)
        {
            Registry.SetValue(registryKeyCurrentUser, "UpdatePath", @"\\" + serverName + updatepath);
        }

        public static int getRegValue(string valueName, object dfltValue)
        {
            return Convert.ToInt32(Registry.GetValue(registryKeyCurrentUser,valueName,dfltValue));
        }

        public static void saveRegValue(string valueName, object dfltValue)
        {
            Registry.SetValue(registryKeyCurrentUser, valueName, dfltValue);
        }

        public static string cleanAddress(string txt)
        {
            string tmp = txt.Trim().Replace(".", "");
            tmp = tmp.Replace(" e ", " E ").Replace(" n ", " N ").Replace(" s ", " S ").Replace(" w ", " W ");
            tmp = tmp.Replace(" No ", " N ").Replace(" no ", " N ").Replace(" NO ", " N ").Replace(" so ", " S ").Replace(" So ", " S ");
            tmp = tmp.Replace(" east ", " E ").Replace(" north ", " N ").Replace(" south ", " S ").Replace(" west ", " W ");
            tmp = tmp.Replace(" East ", " E ").Replace(" North ", " N ").Replace(" South ", " S ").Replace(" West ", " W ");
            tmp = tmp.Replace(" EAST ", " E ").Replace(" NORTH ", " N ").Replace(" SOUTH ", " S ").Replace(" WEST ", " W ");
            tmp = tmp.Replace(" NORTHEAST ", " NE ").Replace(" NorthEast ", " NE ").Replace(" Northeast ", " NE ").Replace(" northeast ", " NE ");
            tmp = tmp.Replace(" NORTHWEST ", " NW ").Replace(" NorthWest ", " NW ").Replace(" Northwest ", " NW ").Replace(" northwest ", " NW ");
            tmp = tmp.Replace(" SOUTHEAST ", " SE ").Replace(" SouthEast ", " SE ").Replace(" Southeast ", " SE ").Replace(" southeast ", " SE ");
            tmp = tmp.Replace(" SOUTHWEST ", " SW ").Replace(" SouthWest ", " SW ").Replace(" Southwest ", " SW ").Replace(" southwest ", " SW ");
            tmp = tmp.Replace(" ne ", " NE ").Replace(" nw ", " NW ").Replace(" se ", " SE ").Replace(" sw ", " SW ");
            tmp = tmp.Replace(" ne", " NE").Replace(" nw", " NW").Replace(" se", " SE").Replace(" sw", " SW");
            tmp = tmp.Replace(" street", " St").Replace(" Street", " St").Replace(" st", " St").Replace(" Str", " St").Replace(" str", " St").Replace(" STR", " St");
            tmp = tmp.Replace(" ave", " Ave").Replace(" Avenue", " Ave").Replace(" avenue", " Ave").Replace(" AVE", " Ave");
            tmp = tmp.Replace(" way", " Way").Replace(" WAY", " Way");
            tmp = tmp.Replace(" pl ", " Pl").Replace(" Place", " Pl").Replace(" place", " Pl").Replace(" PLACE", " Pl");
            tmp = tmp.Replace("TH ", "th ").Replace("tH ", "th ").Replace("Th ", "th ");
            tmp = tmp.Replace("ND ", "nd ").Replace("nD ", "nd ").Replace("Nd ", "nd ");
            return tmp;
        }
    }
}
