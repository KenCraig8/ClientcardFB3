using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace ClientcardFB3
{
    public class TrxLog
    {
        #region Household Service Transactions Data Members
        static string tbName = "TrxLog";
        DataSet dset;
        DataSet dsetTrx;
        int rowIndexCurrent;
        SqlDataAdapter dadAdpt;
        SqlCommand command;
        SqlCommand sqlCmdCount = new SqlCommand();
        SqlConnection conn;
        string connString;
        bool isValid;
        int iRowCount = 0;
        DataRow drow;
        bool svctypeAppointments = false;
        bool svctypeNormal = true;
        bool svctypeFastTrack = false;
        bool svctypeNoShow = false;
        string[] statusNames = new string[4] { "Service", "Fast Track", "Appointment", "NoShow" };
        string[] statusNamesShort = new string[4] { "Svc", "FT", "Appt", "NoSh" };
        bool validHHData = false;

        public enum TrxLogStatus
        {
            Service = 0,
            FastTrack = 1,
            Appointment = 2,
            NoShow = 3
        }

        //Signature variables
        bool bhavesig = false;
        #endregion

//---------------------------------------Constructor-----------------------------------------------------
/// <summary>
/// Initializes all local data members of class
/// </summary>
/// <param name="connectString">connectString = Connection String</param>
        
        public TrxLog(string connectString, bool normal, bool fasttrack, bool appointments, bool noshow)
        {
            dset = new DataSet();
            dsetTrx = new DataSet();
            dadAdpt = new SqlDataAdapter();
            command = new SqlCommand();
            rowIndexCurrent = 0;
            connString = connectString;
            conn = new SqlConnection();
            conn.ConnectionString = connString;
            svctypeAppointments = appointments;
            svctypeFastTrack = fasttrack;
            svctypeNormal = normal;
            svctypeNoShow = noshow;
            isValid = false;
            sqlCmdCount.Connection = conn;
        }

//----------------------------------------Get/Set Acsessors-----------------------------------------------------
/// <summary>
/// Each of these acsessors either gets or sets a value in the dataset.  
/// These changes are not made in the database untill the update 
/// funtion is called.
/// </summary>

        #region GET/SET ACCESSORS

        public int RowCount
        {
            get { return iRowCount; }
        }

        public int RowIndex
        {
            get { return rowIndexCurrent; }
        }

        public DataRow DRow
        {
            get { return drow; }
            set { drow = value; }
        }
        public DataSet DSet
        {
            get { return dset; }
            set { dset = value; }
        }

        public bool IncludeAppointments
        {
            get { return svctypeAppointments; }
            set { svctypeAppointments = value; }
        }

        public bool IncludeNormalService
        {
            get { return svctypeNormal; }
            set { svctypeNormal = value; }
        }

        public bool IncludeFastTrack
        {
            get { return svctypeFastTrack; }
            set { svctypeFastTrack = value; }
        }

        public bool IncludeNoShow
        {
            get { return svctypeNoShow; }
            set { svctypeNoShow = value; }
        }

        public int TrxId
        {
            get { return Convert.ToInt32(drow["TrxId"]); }
            set { drow["TrxId"] = value; }
        }

        public string HHMemID
        {
            get
            {
                return drow["HHMemID"].ToString();
            }
            set {drow["HHMemID"] = value; }
        }

        public DateTime TrxDate
        {
            get
            {
                return (DateTime)drow["TrxDate"];
            }
            set
            {
                drow["TrxDate"] = value;
            }
        }

        public int HouseholdID
        {
            get
            {
                return Convert.ToInt32(drow["HouseholdID"]);
            }
            set
            {
                drow["HouseholdID"] = value;
            }
        }
        public Int16 Meals
        {
            get
            {
                return Convert.ToInt16(drow["Meals"]);
            }
            set
            {
                drow["Meals"] = value;
            }
        }
        public Int16 Bags
        {
            get
            {
                return Convert.ToInt16(drow["Bags"]);
            }
            set
            {
                drow["Bags"] = value;
            }
        }
        public int LbsStd
        {
            get { return Convert.ToInt32(drow["LbsStd"]); }
            set { drow["LbsStd"] = value; }
        }
        public int LbsOther
        {
            get { return Convert.ToInt32(drow["LbsOther"]); }
            set { drow["LbsOther"] = value; }
        }
        public int LbsSupplemental
        {
            get { return Convert.ToInt32(drow["LbsSupplemental"]); }
            set { drow["LbsSupplemental"] = value; }
        }
        public int LbsCommodity
        {
            get { return Convert.ToInt32(drow["LbsCommodity"]); }
            set { drow["LbsCommodity"] = value; }
        }
        public int LbsBabySvc
        {
            get { return Convert.ToInt32(drow["LbsBabySvc"]); }
            set { drow["LbsBabySvc"] = value; }
        }
        public string Notes
        {
            get { return drow["Notes"].ToString(); } 
            set { drow["Notes"] = value; }
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
        public string BabySvcList
        {
            get { return drow["BabySvcList"].ToString(); }
            set { drow["BabySvcList"] = value; }
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
        public string ConcatBabySvcItemsList
        {
            get { return drow["ConcatBabySvcItemsList"].ToString(); }
            set { drow["ConcatBabySvcItemsList"] = value; }
        }
        public Int16 Infants
        {
            get { return Convert.ToInt16(drow["Infants"]); }
            set { drow["Infants"] = value; }
        }
        public Int16 Youth
        {
            get { return Convert.ToInt16(drow["Youth"]); }
            set { drow["Youth"] = value; }
        }
        public Int16 Adults
        {
            get { return Convert.ToInt16(drow["Adults"]); }
            set { drow["Adults"] = value; }
        }

        public Int16 Seniors
        {
            get { return Convert.ToInt16(drow["Seniors"]); }
            set { drow["Seniors"] = value; }
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
        public bool RcvdSupplemental
        {
            get { return (bool)drow["RcvdSupplemental"]; }
            set { drow["RcvdSupplemental"] = value; }
        }
        public bool FirstTimeEver
        {
            get { return (bool)drow["FirstTimeEver"]; }
            set { drow["FirstTimeEver"] = value;
            }
        }
        public bool FiscalFirstTime
        {
            get
            {
                return (bool)drow["FiscalFirstTime"];
            }
            set
            {
                drow["FiscalFirstTime"] = value;
            }
        }
        public bool CalFirstTime
        {
            get
            {
                return (bool)drow["CalFirstTime"];
            }
            set
            {
                drow["CalFirstTime"] = value;
            }
        }
        public bool Transient
        {
            get
            {
                return (bool)drow["Transient"];
            }
            set
            {
                drow["Transient"] = value;
            }
        }
        public int EthnicSpeaking
        {
            get
            {
                return Convert.ToInt32(drow["EthnicSpeaking"]);
            }
            set
            {
                drow["EthnicSpeaking"] = value;
            }
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
        public int MonthFirstTime
        {
            get
            {
                return Convert.ToInt32(drow["MonthFirstTime"]);
            }
            set
            {
                drow["MonthFirstTime"] = value;
            }
        }
        public int DelivMethod
        {
            get { return Convert.ToInt32(drow["DelivMethod"]); }
            set { drow["DelivMethod"] = value; }
        }
        public int NonFood
        {
            get
            {
                return Convert.ToInt32(drow["NonFood"]);
            }
            set
            {
                drow["NonFood"] = value;
            }
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
        public int Eighteen
        {
            get { return Convert.ToInt32(drow["Eighteen"]); }
            set { drow["Eighteen"] = value; }
        }
        public Int16 TrxStatus
        {
            get { return Convert.ToInt16(drow["TrxStatus"]); }
            set { drow["TrxStatus"] = value; }
        }
        public string StatusName
        {
            get { return statusNames[TrxStatus]; }
        }
        public string StatusNameShort
        {
            get { return statusNamesShort[TrxStatus]; }
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
        public Int16 ClientType
        {
            get { return Convert.ToInt16(drow["ClientType"]); }
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
        public DataSet HHSvcTransDSet
        {
            get
            {
                return dset;
            }
            set
            {
                dset = value;
            }
        }
     
        public bool IsValid
        {
            get
            {
                return isValid;
            }
            set
            {
                isValid = value;
            }
        }
        public bool HaveSig
        {
            get
            {
                return bhavesig;
            }
            set
            {
                bhavesig = value;
            }
        }
        public string connectionString
        {
            get
            {
                return connString;
            }
            set
            {
                connString = value;
            }
        }

        #region Household Data in Trx DataSet
        public string Name
        {
            get
            {
                if (validHHData == true)
                    return drow["Name"].ToString();
                else
                    return "";
            }
        }
        public string Address
        {
            get
            {
                if (validHHData == true)
                    return drow["Address"].ToString();
                else
                    return "";
            }
        }
        public string City
        {
            get
            {
                if (validHHData == true)
                    return drow["City"].ToString();
                else
                    return "";
            }
        }
        public string State
        {
            get
            {
                if (validHHData == true)
                    return drow["State"].ToString();
                else
                    return "";
            }
        }
        #endregion
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
            drow[FieldName] = FieldValue;
        }
        //Sets data value when value is a bool
        public void SetDataValue(string FieldName, bool FieldValue)
        {
            drow[FieldName] = FieldValue;
        }
        
        //Gets property through use of just the column name in database
        public object GetDataValue(string FieldName)
        {
            return drow[FieldName];
        }

        //Gets property through use of just the column name in database as string
        public object GetDataString(string FieldName)
        {
            if (dset.Tables[tbName].Rows.Count > 0)
            {
                int fldIndex = dset.Tables[tbName].Columns.IndexOf(FieldName);
                if (fldIndex >= 0)
                {
                    if (dset.Tables[tbName].Columns[fldIndex].DataType.Name == "DateTime")
                        if (drow[FieldName].ToString() != "")
                        { return CCFBGlobal.ValidDateString(drow[FieldName]); }
                        else
                        { return ""; }
                    else
                        return drow[FieldName].ToString();
                }
            }
            return "";
        }
        #endregion

        public int GetNBrTrxByHH(int HHId)
        {
            object iValue = CCFBGlobal.getSQLValue("SELECT Count(*) FROM TrxLog WHERE HouseholdId = " + HHId.ToString());
            if (iValue != null)
                return Convert.ToInt32(iValue);
            else
                return 0;
        }

//-------------------------------------Find------------------------------------
/// <summary>
/// Finds the TrxID in dataset and sets that row to the drow
/// </summary>
/// <param name="ID"></param>
/// 
        public void find(int ID)
        {
            for (int i = 0; i < iRowCount; i++)
            {
                if (ID == dset.Tables[tbName].Rows[i].Field<int>("TrxID"))
                {
                    drow = dset.Tables[tbName].Rows[i];
                    break;
                }
            }
        }

        /// <summary>
        /// Sets the DataRow for the given RowIndex
        /// </summary>
        /// <param name="rowIndex"></param>
        public DataRow setDataRow(int rowIndex)
        {
            if (dset.Tables[tbName].Rows.Count > 0)
            {
                drow = dset.Tables[tbName].Rows[rowIndex];
                rowIndexCurrent = rowIndex;
                return drow;
            }
            return null;
        }

        public bool NextDataRow()
        {
            if (rowIndexCurrent + 1 < iRowCount)
            {
                rowIndexCurrent ++;
                drow = dset.Tables[0].Rows[rowIndexCurrent];
                return true;
            }
            else
                return false;
        }

        public int NbrSvcRows(DateTime svcDate)
        {
            object retVal = CCFBGlobal.getSQLValue("SELECT Count(*) FROM TrxLog WHERE TrxDate = '" + svcDate.ToShortDateString() + "'");
            if (retVal != null)
            {    return Convert.ToInt32(retVal); }
            return 0;
        }

        public bool PrevDataRow()
        {
            if (rowIndexCurrent > 0 && iRowCount >0 )
            {
                rowIndexCurrent--;
                drow = dset.Tables[0].Rows[rowIndexCurrent];
                return true;
            }
            else
                return false;
        }

        //-------------------------------------Open Service Transactions------------------------------------
        /// <summary>
        /// Populates the dataset for the service transactions using HoueholdID
        /// </summary>
        /// <param name="TrxID">ID=Transaction ID</param>
        /// <returns>Return if dataset got populated correctly</returns>


        public DataRow[] DistinctSvcDays()
        {
            DataSet ds = new DataSet();
            SqlDataAdapter dataAdpt;
            try
            {
                command = new SqlCommand("SELECT DISTINCT TrxDate, Count(*)  FROM TrxLog WHERE TrxStatus = 0 GROUP BY TrxDate ORDER BY TrxDate");
                dataAdpt = new SqlDataAdapter(command);
                dataAdpt.SelectCommand.Connection = conn;
                dataAdpt.Fill(ds);
                return ds.Tables[0].Select();
            }
            catch (SqlException ex)
            {
                CCFBGlobal.appendErrorToErrorReport("",
                    ex.GetBaseException().ToString());
                iRowCount = 0;
                return null;
            }
        }

        public void MoveToHH(int idTrxLog, int newHHId)
        {
            SqlCommand sqlCmd = new SqlCommand("UPDATE TrxLog SET HouseholdId = " + newHHId.ToString() + " WHERE TrxId = " + idTrxLog.ToString(), conn);
            openConnection();
            sqlCmd.ExecuteNonQuery();
            closeConnection();
        }

        public bool open(int TrxID)
        {
            try
            {
                validHHData = false;
                command = new SqlCommand("SELECT * FROM TrxLog WHERE TrxID=" + TrxID.ToString(), conn);
                dadAdpt.SelectCommand = command;
                dsetTrx.Clear();
                iRowCount = dadAdpt.Fill(dsetTrx, "TrxLog");
                if (iRowCount > 0)
                {
                    drow = dsetTrx.Tables[tbName].Rows[0];
                    rowIndexCurrent = 0;
                    return isValid = true;
                }
                return isValid = false;
            }
            catch (SqlException ex)
            {
                CCFBGlobal.appendErrorToErrorReport("TrxID=" + TrxID.ToString(), ex.GetBaseException().ToString());
                closeConnection();
                iRowCount = 0;
                return isValid = false;
            }
        }
        public void openForHH(int HHID)
        {
            validHHData = false;
            try
            {
                openConnection();
                if (svctypeAppointments == true && svctypeNormal == false)
                {
                    command = new SqlCommand("SELECT * FROM " + tbName + " WHERE TrxStatus > 1 AND HouseholdID=" + HHID.ToString(), conn);
                }
                else
                {
                    command = new SqlCommand("SELECT * FROM " + tbName + " WHERE HouseholdID=" + HHID.ToString(), conn);
                }
                dadAdpt.SelectCommand = command;
                dset.Clear();
                iRowCount = dadAdpt.Fill(dset, tbName);
                closeConnection();
                if (iRowCount > 0)
                    drow = dset.Tables[tbName].Rows[0];
            }
            catch (SqlException ex)
            {
                CCFBGlobal.appendErrorToErrorReport("HHID="+HHID.ToString(), ex.GetBaseException().ToString());
                iRowCount = 0;
                closeConnection();
            }
        }

        public void openWhere(string sWhereClause)
        {
            validHHData = false;
            try
            {
                openConnection();
                string sql = "SELECT * FROM " + tbName;
                if (sWhereClause != "")
                    sql += " WHERE " + sWhereClause;
                command = new SqlCommand(sql, conn);
                dadAdpt = new SqlDataAdapter(command);
                dadAdpt.SelectCommand = command;
                dset.Clear();
                dadAdpt.SelectCommand.Connection = conn;
                iRowCount = dadAdpt.Fill(dset, tbName);
                closeConnection();
                if (iRowCount > 0)
                    drow = dset.Tables[tbName].Rows[0];
            }
            catch (SqlException ex)
            {
                closeConnection();
                CCFBGlobal.appendErrorToErrorReport("", 
                    ex.GetBaseException().ToString());
                iRowCount = 0;
            }
        }

//-----------------------------------Open Using Date--------------------------------------------
/// <summary>
/// Populates the dataset using a date range for the Household
/// </summary>
/// <param name="HHID">HHID=HouseholdID</param>
/// <param name="dateFrom">dateFrom=Starting Date</param>
/// <param name="dateTo">dateTo=Ending Date</param>
/// <returns>Returns if the dataset is populated with valid data</returns>
        
        public bool openUsingDateRange(int HHID, DateTime dateFrom, DateTime dateTo)
        {
            validHHData = false;
            try
            {
                openConnection();
                //if (svctypeAppointments== true)
                //{
                //    command = new SqlCommand("SELECT * FROM TrxLog WHERE TrxStatus > 0 AND HouseholdID=" + HHID.ToString()
                //        + " and TrxDate >= '" + dateFrom.ToString() + "' and TrxDate <= '" + dateTo.ToString() + "'", conn);
                //}
                //else
                //{
                    command = new SqlCommand("SELECT * FROM TrxLog WHERE HouseholdID=" + HHID.ToString()
                        + " and TrxDate >= '" + dateFrom.ToString() + "' and TrxDate <= '" + dateTo.ToString() + "'", conn);
                //}
                dadAdpt.SelectCommand = command;
                dset.Clear();
                iRowCount = dadAdpt.Fill(dset, "TrxLog");
                closeConnection();

                if (iRowCount > 0)
                {
                    drow = dset.Tables[tbName].Rows[0];
                    return isValid = true;
                }
                return isValid = false;
            }
            catch (SqlException ex)
            {
                CCFBGlobal.appendErrorToErrorReport("HHID=" + HHID.ToString() + ", dateFrom=" + dateFrom.ToString()
                    + ", dateTo=" + dateTo.ToString(),  ex.GetBaseException().ToString());
                closeConnection();
                iRowCount = 0;
                return isValid = false;
            }
        }

        public void openForADate(DateTime trxdate, string ascDesc)
        {
            validHHData = true;
            string sql = "SELECT CASE WHEN d.HHMemId='' THEN h.Name ELSE d.HHMemId END Name, h.address, h.city, h.state, h.zipcode, h.AptNbr, d.* FROM TrxLog d "
                       + "  LEFT JOIN Household h On d.HouseholdID=h.id "
                       + " WHERE d.TrxDate ='" + trxdate.ToShortDateString() + "'";
            string statuslist = "";
            if (svctypeNormal == true)       { AppendStatus(ref statuslist, "0"); }
            if (svctypeFastTrack == true)    { AppendStatus(ref statuslist, "1"); }
            if (svctypeAppointments == true) { AppendStatus(ref statuslist, "2"); }
            if (svctypeNoShow == true)       { AppendStatus(ref statuslist, "3"); }
            if (statuslist != "")            { sql += " AND TrxStatus IN (" + statuslist + ")"; }
            sql += " ORDER BY TrxId"; 
            try
            {
                command = new SqlCommand(sql, conn);
                dadAdpt.SelectCommand = command;
                dset.Clear();
                iRowCount = dadAdpt.Fill(dset, tbName);
                if (iRowCount > 0)
                    drow = dset.Tables[tbName].Rows[0];
            }
            catch (SqlException ex)
            {
                CCFBGlobal.appendErrorToErrorReport("dateFrom=" + trxdate.ToString(),
                    ex.GetBaseException().ToString());
                closeConnection();
                iRowCount = 0;
            }
        }

        public int getServicesThisMonthCount(int Month, int Year)
        {
            validHHData = false; 
            int transCounter = 0;

            for (int i = 0; i < RowCount; i++)
            {
                try
                {
                    if (dset.Tables[tbName].Rows[i].Field<DateTime>("TrxDate").Month == Month
                        && dset.Tables[tbName].Rows[i].Field<DateTime>("TrxDate").Year == Year
                        && Int32.Parse(dset.Tables[tbName].Rows[i]["TrxStatus"].ToString()) == 0)
                    {
                        transCounter++;
                    }
                }
                catch { transCounter = 0; break; }
            }
            return transCounter;
        }

        //-------------------------------Delete Service Transaction--------------------------------------
        /// <summary>
        /// Deletes a single service transaction from daily log using the TrxID
        /// </summary>
        /// <param name="key">key=TrxID</param>

        public bool delete(string key)
        {
            SqlCommand cmdDelete = new SqlCommand(" DELETE FROM TrxLog WHERE TrxId=" + key, conn);
            openConnection();
            int iRows = cmdDelete.ExecuteNonQuery();
            closeConnection();
            return (iRows == 1);
        }

        public bool PromptDelete(string trxId)
        {
            object dateValue = CCFBGlobal.getSQLValue("SELECT TrxDate FROM TrxLog WHERE TrxId = " + trxId);
            if (dateValue != null)
            {
                DateTime dateTrx = Convert.ToDateTime(dateValue);
                if (MessageBox.Show("Are you SURE you would like to delete trx id "
                    + trxId + "? " +
                    dateTrx.ToShortDateString(), "Delete Transaction",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == System.Windows.Forms.DialogResult.Yes)
                {
                    return delete(trxId);
                }
            }
            return false;
        }

//------------------------Delete ALL Service Transactions For A Household--------------------------------------
/// <summary>
/// Deletes all service transactions from daily log for a household
/// </summary>
/// <param name="key">key=HouseholdID</param>
        public void deleteAllHHSvcTrans(int HHID)
        {
            openConnection();
            SqlCommand commDelete = new SqlCommand(" DELETE FROM TrxLog WHERE HouseholdId=" + HHID.ToString(), conn);
            commDelete.ExecuteNonQuery();
            closeConnection();
        }

//---------------------------------------Update Service Transactions---------------------------------------
/// <summary>
/// Checks for differences between the Service Transaction dataset and the TrxLog Table
/// and then makes the changes in the database
/// </summary>
      
        public int update(int hhID, string dateSvc)
        {
            int newTrxId = 0;
            if (dset.HasChanges() == true)
            {
                try
                {
                    openConnection();

                    if (dadAdpt.UpdateCommand == null)
                    {
                        SqlCommandBuilder commBuilder = new SqlCommandBuilder(dadAdpt);
                    }

                    //drow["ModifiedBy"] = CCFBGlobal.dbUserName;
                    //drow["Modified"] = DateTime.Now;

                    dadAdpt.Update(dset, "TrxLog");
                    if (hhID > 0)
                    {
                        object retVal = CCFBGlobal.getSQLValue("SELECT TrxId FROM TrxLog WHERE TrxDate = '" + dateSvc + "' AND HouseholdId = " + hhID.ToString());
                        if (retVal != null)
                        { newTrxId = Convert.ToInt32(retVal); }
                    }
                    closeConnection();
                }
                catch (SqlException ex)
                {
                    closeConnection();
                    CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString());
                }              
            }
            return newTrxId;
        }

        public void saveFastTrack(string trxid, string lbsStd, string lbsOther, string lbsTEFAP, string lbsSuppl, string lbsBaby)
        {
            if (lbsStd == "") { lbsStd = "0"; }
            if (lbsOther == "") { lbsOther = "0"; }
            if (lbsTEFAP == "") { lbsTEFAP = "0"; }
            if (lbsSuppl == "") { lbsSuppl = "0"; }
            if (lbsBaby == "") { lbsBaby = "0"; }
            try
            {
                SqlCommand sqlcmd = new SqlCommand("UPDATE TrxLog SET TrxStatus = 0, LbsStd = " + lbsStd + ", lbsOther = " + lbsOther
                                                 + ", lbsCommodity = " + lbsTEFAP + ", lbsSupplemental = " + lbsSuppl + ", lbsBabySvc = " + lbsBaby
                                                 + ", Modified = '" + DateTime.Now.ToString()  + "', ModifiedBy = '" + CCFBGlobal.dbUserName + "'"
                                                 + ", RcvdCommodity = CASE WHEN lbsCommodity > 0 THEN 1 ELSE 0 END"
                                                 + " WHERE TrxId = " + trxid
                                                 , conn);
                openConnection();
                sqlcmd.ExecuteNonQuery();
                closeConnection();
            }
            catch (SqlException ex)
            {
                closeConnection();
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString());
            }
        }

//---------------------------------------Insert A Service Transactions---------------------------------------
/// <summary>
/// Inserts a new Transaction into the daily log
/// </summary>

        public void insert()
        {
            try
            {
                openConnection();

                //Checks and sets the insert command
                if (dadAdpt.InsertCommand == null || dadAdpt.UpdateCommand == null)
                {
                    SqlCommandBuilder commBuilder = new SqlCommandBuilder(dadAdpt);
                }

                //Inserts the row into the database
                dadAdpt.Update(dset, tbName);
                closeConnection();
            }
            catch (SqlException ex)
            {
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString());
                closeConnection();
            }

        }

//---------------------------------------Open/Close Connection-----------------------------------------------
/// <summary>
///These methods are modular methods that just check the connection state before trying to 
///open or close a connection
/// </summary>

        private void openConnection()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
        }
        private void closeConnection()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        private void AppendStatus(ref string statusList, string newStatus)
        {
            if (statusList != "")
                statusList += ",";
            statusList += newStatus;
        }

        public void updateServiceBits(int hhid, DateTime updateSvcDate)
        {
            try
                {
                    openConnection();
                    SqlCommand svcBitsCommand = new SqlCommand("UpdateTrxLogFirstBits", conn);
                    svcBitsCommand.CommandType = CommandType.StoredProcedure;
                    svcBitsCommand.Parameters.Add(new SqlParameter("ServiceDate", CCFBGlobal.formatDate(updateSvcDate)));
                    svcBitsCommand.Parameters.Add(new SqlParameter("HouseholdId", hhid));
                    svcBitsCommand.ExecuteReader();
                    closeConnection();
                }
                catch (SqlException ex)
                {
                    CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString());
                }
        }
    }
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

    public class TrxLogSig
    {
        int sigTrxId;
        int sigHHId;
        Image sigImage;
        DateTime sigCreated;
        string sigCreatedBy = "";
        string sigconnString = "";
        string textInsert = "INSERT TrxLogSig (TrxId, HouseholdID, SigImage, Created, CreatedBy)"
                          + "VALUES (@TrxId, @HhID, @Sig, @Created, @CreatedBy)";
        string textLoad = "SELECT * FROM TrxLogSig WHERE TrxId = @TrxId";
        SqlCommand sqlInsertCmd;
        SqlCommand sqlLoadCmd;
        SqlConnection sqlConn;
        public TrxLogSig(string connString)
        {
            sigconnString = connString;
            sigTrxId = 0;
            sigHHId = 0;
            sigImage = null;
            sqlConn = new SqlConnection(sigconnString);
            sqlInsertCmd = new SqlCommand(textInsert, sqlConn);
            sqlInsertCmd.Parameters.Add("@TrxId", SqlDbType.Int);
            sqlInsertCmd.Parameters.Add("@HhID", SqlDbType.Int);
            sqlInsertCmd.Parameters.Add("@Sig", SqlDbType.VarBinary,-1);
            sqlInsertCmd.Parameters.Add("@Created", SqlDbType.DateTime);
            sqlInsertCmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar, 50);

            sqlLoadCmd = new SqlCommand(textLoad, sqlConn);
            sqlLoadCmd.Parameters.Add("@TrxId", SqlDbType.Int);
        }

        public int TrxId
        {
            get { return sigTrxId; }
            set { sigTrxId = value; }
        }
        public int HhID
        {
            get { return sigHHId; }
            set { sigHHId = value; }
        }
        public Image SigImage
        {
            get { return sigImage; }
            set { sigImage = value; }
        }
        public DateTime Created
        {
            get { return sigCreated; }
            set { sigCreated = value; }
        }
        public string CreatedBy
        {
            get { return sigCreatedBy; }
            set { sigCreatedBy = value; }
        }
        public Boolean Insert()
        {
            sqlInsertCmd.Parameters["@TrxId"].Value = sigTrxId;
            sqlInsertCmd.Parameters["@HhID"].Value = sigHHId;
            sqlInsertCmd.Parameters["@Sig"].Value = CCFBGlobal.imageToByteArray(sigImage);
            sqlInsertCmd.Parameters["@Created"].Value = DateTime.Now;
            sqlInsertCmd.Parameters["@CreatedBy"].Value = CCFBGlobal.dbUserName;
            sqlConn.Open();
            int retVal = sqlInsertCmd.ExecuteNonQuery();
            sqlConn.Close();
            return (retVal == 1);
        }

        public Boolean LoadImage(int newTrxId)
        {
            sqlLoadCmd.Parameters["@TrxId"].Value = newTrxId;
            sqlConn.Open();
            SqlDataReader reader = sqlLoadCmd.ExecuteReader();
            int count = 0;
            while (reader.Read())
            {
                sigTrxId = Convert.ToInt32(reader["TrxID"]);
                sigHHId = Convert.ToInt32(reader["HouseholdID"]);
                byte[] imgData = (byte[])reader["SigImage"];
                sigImage = CCFBGlobal.byteArrayToImage(imgData);
                sigCreated = Convert.ToDateTime(reader["Created"]);
                sigCreatedBy = reader["CreatedBy"].ToString();
                count++;
            }
            sqlConn.Close();
            return (count == 1);
        }
    }
}