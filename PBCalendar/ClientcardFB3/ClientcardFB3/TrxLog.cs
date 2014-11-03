using System;
using System.Data;
using System.Data.SqlClient;
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
        SqlConnection conn;
        string connString;
        bool isValid;
        int iRowCount = 0;
        DataRow drow;
        bool appointmentsOnly = false;
        bool serviceTrxOnly = false;
        string[] statusNames = new string[3] { "Service", "Appointment", "NoShow" };
        string[] statusNamesShort = new string[3] { "Svc", "Appt", "NoSh" };


        #endregion

//---------------------------------------Constructor-----------------------------------------------------
/// <summary>
/// Initializes all local data members of class
/// </summary>
/// <param name="connectString">connectString = Connection String</param>
        
        public TrxLog(string connectString)
        {
            dset = new DataSet();
            dsetTrx = new DataSet();
            dadAdpt = new SqlDataAdapter();
            command = new SqlCommand();
            rowIndexCurrent = 0;
            connString = connectString;
            conn = new SqlConnection();
            conn.ConnectionString = connString;
            isValid = false;
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

        public bool AppointmentsOnly
        {
            get { return appointmentsOnly; }
            set { appointmentsOnly = value; }
        }

        public bool ServiceTrxOnly
        {
            get { return serviceTrxOnly; }
            set { serviceTrxOnly = value; }
        }
        public int TrxId
        {
            get { return Convert.ToInt32(drow["TrxId"]); }
            set { drow["TrxId"] = value; }
        }

        public int HHMemID
        {
            get { return Convert.ToInt32(drow["HHMemID"]); }
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
        public int LbsThroughBags
        {
            get
            {
                return Convert.ToInt32(drow["LbsStd"]);
            }
            set
            {
                drow["LbsStd"] = value;
            }
        }
        public int LbsThroughOther
        {
            get
            {
                return Convert.ToInt32(drow["LbsOther"]);
            }
            set
            {
                drow["LbsOther"] = value;
            }
        }
        public int AgeGroup
        {
            get
            {
                return Convert.ToInt32(drow["AgeGroup"]);
            }
            set
            {
                drow["AgeGroup"] = value;
            }
        }
        public int LbsCommodities
        {
            get
            {
                return Convert.ToInt32(drow["LbsCommodity"]);
            }
            set
            {
                drow["LbsCommodity"] = value;
            }
        }
        public string Notes
        {
            get
            {
                return drow["Notes"].ToString();
            }
            set
            {
                drow["Notes"] = value;
            }
        }

        public string ConcatFoodSvcItemsList
        {
            get
            {
                return drow["ConcatFoodSvcItemsList"].ToString();
            }
            set
            {
                drow["ConcatFoodSvcItemsList"] = value;
            }
        }

        public string ConcatNonFoodSvcItemsList
        {
            get
            {
                return drow["ConcatNonFoodSvcItemsList"].ToString();
            }
            set
            {
                drow["ConcatNonFoodSvcItemsList"] = value;
            }
        }

        public string FoodSvcList
        {
            get
            {
                return drow["FoodSvcList"].ToString();
            }
            set
            {
                drow["FoodSvcList"] = value;
            }
        }

        public string NonFoodSvcList
        {
            get
            {
                return drow["NonFoodSvcList"].ToString();
            }
            set
            {
                drow["NonFoodSvcList"] = value;
            }
        }
        public Int16 Adults
        {
            get
            {
                return Convert.ToInt16(drow["Adults"]);
            }
            set
            {
                drow["Adults"] = value;
            }
        }
        public Int16 Youths
        {
            get
            {
                return Convert.ToInt16(drow["Youth"]);
            }
            set
            {
                drow["Youth"] = value;
            }
        }

        public Int16 Seniors
        {
            get
            {
                return Convert.ToInt16(drow["Seniors"]);
            }
            set
            {
                drow["Seniors"] = value;
            }
        }
        public Int16 Infants
        {
            get
            {
                return Convert.ToInt16(drow["Infants"]);
            }
            set
            {
                drow["Infants"] = value;
            }
        }
        public Int16 TotalFamily
        {
            get
            {
                return Convert.ToInt16(drow["TotalFamily"]);
            }
            set
            {
                drow["TotalFamily"] = value;
            }
        }
        public int SpecialDiet
        {
            get
            {
                return Convert.ToInt32(drow["SpecialDiet"]);
            }
            set
            {
                drow["SpecialDiet"] = value;
            }
        }
        public int NumCat1
        {
            get
            {
                return Convert.ToInt32(drow["NumCat1"]);
            }
            set
            {
                drow["NumCat1"] = value;
            }
        }
        public int NumCat2
        {
            get
            {
                return Convert.ToInt32(drow["NumCat2"]);
            }
            set
            {
                drow["NumCat2"] = value;
            }
        }
        public bool RcvdCommodity
        {
            get
            {
                return (bool)drow["RcvdCommodity"];
            }
            set
            {
                drow["RcvdCommodity"] = value;
            }
        }
        public bool FirstTimeEver
        {
            get
            {
                return (bool)drow["FirstTimeEver"];
            }
            set
            {
                drow["FirstTimeEver"] = value;
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
            get
            {
                return (DateTime)drow["Created"];
            }
            set
            {
                drow["Created"] = value;
            }
        }
        public string CreatedBy
        {
            get
            {
                return drow["CreatedBy"].ToString();
            }
            set
            {
                drow["CreatedBy"] = value;
            }
        }
        public DateTime Modified
        {
            get
            {
                return (DateTime)drow["Modified"];
            }
            set
            {
                drow["Modified"] = value;
            }
        }
        public string ModifiedBy
        {
            get
            {
                return drow["ModifiedBy"].ToString();
            }
            set
            {
                drow["ModifiedBy"] = value;
            }
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
        public int Vouchers
        {
            get
            {
                return Convert.ToInt32(drow["Vouchers"]);
            }
            set
            {
                drow["Vouchers"] = value;
            }
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
        public int LbsSupplemental
        {
            get
            {
                return Convert.ToInt32(drow["LbsSupplemental"]);
            }
            set
            {
                drow["LbsSupplemental"] = value;
            }
        }
        public bool RcvdSupplemental
        {
            get
            {
                return (bool)drow["RcvdSupplemental"];
            }
            set
            {
                drow["RcvdSupplemental"] = value;
            }
        }
        public int LbsCSFP
        {
            get
            {
                return Convert.ToInt32(drow["LbsCSFP"]);
            }
            set
            {
                drow["LbsCSFP"] = value;
            }
        }
        public bool RcvdCSFP
        {
            get
            {
                return (bool)drow["RcvdCSFP"];
            }
            set
            {
                drow["RcvdCSFP"] = value;
            }
        }
        public bool RcvdVoucher
        {
            get
            {
                return (bool)drow["RcvdVoucher"];
            }
            set
            {
                drow["RcvdVoucher"] = value;
            }
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
        
        //Gets property through use of just the collum name in database
        public object GetDataValue(string FieldName)
        {
            return drow[FieldName];
        }

        #endregion

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
                    ex.GetBaseException().ToString(), CCFBGlobal.serverName);
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
                CCFBGlobal.appendErrorToErrorReport("TrxID=" + TrxID.ToString(), ex.GetBaseException().ToString(),
                    CCFBGlobal.serverName);
                closeConnection();
                iRowCount = 0;
                return isValid = false;
            }
        }
        public void openForHH(int HHID)
        {
            try
            {
                openConnection();
                if (appointmentsOnly == true)
                {
                    command = new SqlCommand("SELECT * FROM " + tbName + " WHERE TrxStatus > 0 AND HouseholdID=" + HHID.ToString(), conn);
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
                CCFBGlobal.appendErrorToErrorReport("HHID="+HHID.ToString(), ex.GetBaseException().ToString(),
                    CCFBGlobal.serverName);
                iRowCount = 0;
                closeConnection();
            }
        }

        public void openWhere(string sWhereClause)
        {
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
                    ex.GetBaseException().ToString(), CCFBGlobal.serverName);
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
            try
            {
                openConnection();
                if (appointmentsOnly== true)
                {
                    command = new SqlCommand("SELECT * FROM TrxLog WHERE TrxStatus > 0 AND HouseholdID=" + HHID.ToString()
                        + " and TrxDate >= '" + dateFrom.ToString() + "' and TrxDate <= '" + dateTo.ToString() + "'", conn);
                }
                else
                {
                    command = new SqlCommand("SELECT * FROM TrxLog WHERE HouseholdID=" + HHID.ToString()
                        + " and TrxDate >= '" + dateFrom.ToString() + "' and TrxDate <= '" + dateTo.ToString() + "'", conn);
                }
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
                    + ", dateTo=" + dateTo.ToString(),  ex.GetBaseException().ToString(),
                    CCFBGlobal.serverName);
                closeConnection();
                iRowCount = 0;
                return isValid = false;
            }
        }

        public void openForADate(DateTime dateFrom)
        {
            try
            {
                //openConnection();
                if (appointmentsOnly == true)
                {
                    command = new SqlCommand("SELECT h.name, h.address, h.city, h.state, h.zipcode, * FROM TrxLog d "
                        + " Left Outer Join Household h On d.HouseholdID=h.id "
                        + "Where d.TrxStatus > 0 AND d.TrxDate = '" + dateFrom.ToString() + "'", conn);
                }
                else if (serviceTrxOnly == true)
                {
                    command = new SqlCommand("SELECT h.name, h.address, h.city, h.state, h.zipcode, * FROM TrxLog d "
                        + " Left Outer Join Household h On d.HouseholdID=h.id "
                        + "Where d.TrxStatus = 0 AND d.TrxDate = '" + dateFrom.ToString() + "'", conn);
                }
                else
                {
                    command = new SqlCommand("SELECT h.name, h.address, h.city, h.state, h.zipcode, * FROM TrxLog d "
                        + " Left Outer Join Household h On d.HouseholdID=h.id "
                        + "Where d.TrxDate = '" + dateFrom.ToString() + "'", conn);
                }
                dadAdpt.SelectCommand = command;
                dset.Clear();
                iRowCount = dadAdpt.Fill(dset, tbName);
                if (iRowCount > 0)
                    drow = dset.Tables[tbName].Rows[0];
                //closeConnection();
            }
            catch (SqlException ex)
            {
                CCFBGlobal.appendErrorToErrorReport("dateFrom=" + dateFrom.ToString(),
                    ex.GetBaseException().ToString(), CCFBGlobal.serverName);
                closeConnection();
                iRowCount = 0;
            }
        }

        public int getServicesThisMonthCount(int Month, int Year)
        {
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
            SqlCommand cmdSelect = new SqlCommand("SELECT TrxDate FROM TrxLog WHERE TrxId = " + trxId, conn);
            openConnection();
            object dateValue = cmdSelect.ExecuteScalar();
            closeConnection();
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
      
        public void update()
        {
            if (dset.HasChanges() == true)
            {
                try
                {
                    openConnection();

                    if (dadAdpt.UpdateCommand == null)
                    {
                        SqlCommandBuilder commBuilder = new SqlCommandBuilder(dadAdpt);
                    }

                    //drow["ModifiedBy"] = CCFBGlobal.currentUser_Name;
                    //drow["Modified"] = DateTime.Now;

                    dadAdpt.Update(dset, "TrxLog");
                    closeConnection();
                }
                catch (SqlException ex)
                {
                    closeConnection();
                    CCFBGlobal.appendErrorToErrorReport("",
                        ex.GetBaseException().ToString(), CCFBGlobal.serverName);
                }              
            }
        }


        private void setUpdateCommand()
        {
            #region Update Command
            SqlCommand commUpdate = new SqlCommand("UPDATE TrxLog SET TrxDate=@TrxDate, "
            + "HouseholdID=@HouseholdID, Meals=@Meals, Bags=@Bags, LbsStd=@LbsStd, "
            + "LbsOther=@LbsOther, LbsCommodity=@LbsCommodity, Notes=@Notes, "
            + "ConcatNonFoodSvcItemsList=@ConcatNonFoodSvcItemsList, ConcatFoodSvcItemsList=@ConcatFoodSvcItemsList, "
            + "FoodSvcList=@FoodSvcList, NonFoodSvcList=@NonFoodSvcList, Infants=@Infants, "
            + "Youth=@Youth, Adults=@Adults, Seniors=@Seniors, TotalFamily=@TotalFamily, "
            + "SpecialDiet=@SpecialDiet, NumCat1=@NumCat1, NumCat2=@NumCat2, RcvdCommodity=@RcvdCommodity, "
            + "FirstTimeEver=@FirstTimeEver, FiscalFirstTime=@FiscalFirstTime, CalFirstTime=@CalFirstTime, "
            + "Transient=@Transient, EthnicSpeaking=@EthnicSpeaking, Created=@Created, CreatedBy=@CreatedBy, "
            + "Modified=@Modified, ModifiedBy=@ModifiedBy, MonthFirstTime=@MonthFirstTime, Vouchers=@Vouchers, "
            + "NonFood=@NonFood, LbsSupplemental=@LbsSupplemental, RcvdSupplemental=@RcvdSupplemental, "
            + "LbsCSFP=@LbsCSFP, RcvdCSFP=@RcvdCSFP, RcvdVoucher=@RcvdVoucher, Disabled=@Disabled, "
            + "Homeless=@Homeless, InCityLimits=@InCityLimits, ClientType=@ClientType WHERE TrxId=@TrxId", conn);
            #endregion

            dadAdpt.UpdateCommand = commUpdate;

            #region Update Parameters
            commUpdate.Parameters.Add("@TrxId", SqlDbType.Int, 4, "TrxId");
            commUpdate.Parameters.Add("@TrxDate", SqlDbType.DateTime, 4, "TrxDate");
            commUpdate.Parameters.Add("@HouseholdID", SqlDbType.Int, 4, "HouseholdID");
            commUpdate.Parameters.Add("@Meals", SqlDbType.SmallInt, 2, "Meals");
            commUpdate.Parameters.Add("@Bags", SqlDbType.SmallInt, 2, "Bags");
            commUpdate.Parameters.Add("@LbsStd", SqlDbType.Int, 4, "LbsStd");
            commUpdate.Parameters.Add("@LbsOther", SqlDbType.Int, 4, "LbsOther");
            commUpdate.Parameters.Add("@LbsCommodity", SqlDbType.Int, 4, "LbsCommodity");
            commUpdate.Parameters.Add("@Notes", SqlDbType.NVarChar, 50, "Notes");
            commUpdate.Parameters.Add("@ConcatFoodSvcItemsList", SqlDbType.NVarChar, 0, "ConcatFoodSvcItemsList");
            commUpdate.Parameters.Add("@ConcatNonFoodSvcItemsList", SqlDbType.NVarChar, 0, "ConcatNonFoodSvcItemsList");
            commUpdate.Parameters.Add("@FoodSvcList", SqlDbType.NVarChar, 0, "FoodSvcList");
            commUpdate.Parameters.Add("@NonFoodSvcList", SqlDbType.NVarChar, 0, "NonFoodSvcList");
            commUpdate.Parameters.Add("@Infants", SqlDbType.SmallInt, 2, "Infants");
            commUpdate.Parameters.Add("@Youth", SqlDbType.SmallInt, 2, "Youth");
            commUpdate.Parameters.Add("@Adults", SqlDbType.SmallInt, 2, "Adults");
            commUpdate.Parameters.Add("@Seniors", SqlDbType.SmallInt, 2, "Seniors");
            commUpdate.Parameters.Add("@TotalFamily", SqlDbType.Int, 4, "TotalFamily");
            commUpdate.Parameters.Add("@SpecialDiet", SqlDbType.Int, 4, "SpecialDiet");
            commUpdate.Parameters.Add("@NumCat1", SqlDbType.Int, 4, "NumCat1");
            commUpdate.Parameters.Add("@NumCat2", SqlDbType.Int, 4, "NumCat2");
            commUpdate.Parameters.Add("@RcvdCommodity", SqlDbType.Bit, 1, "RcvdCommodity");
            commUpdate.Parameters.Add("@FirstTimeEver", SqlDbType.Bit, 1, "FirstTimeEver");
            commUpdate.Parameters.Add("@FiscalFirstTime", SqlDbType.Bit, 1, "FiscalFirstTime");
            commUpdate.Parameters.Add("@CalFirstTime", SqlDbType.Bit, 1, "CalFirstTime");
            commUpdate.Parameters.Add("@Transient", SqlDbType.Bit, 1, "Transient");
            commUpdate.Parameters.Add("@EthnicSpeaking", SqlDbType.Int, 4, "EthnicSpeaking");
            commUpdate.Parameters.Add("@Created", SqlDbType.DateTime, 4, "Created");
            commUpdate.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 50, "CreatedBy");
            commUpdate.Parameters.Add("@Modified", SqlDbType.DateTime, 4, "Modified");
            commUpdate.Parameters.Add("@ModifiedBy", SqlDbType.NVarChar, 50, "ModifiedBy");
            commUpdate.Parameters.Add("@MonthFirstTime", SqlDbType.Int, 4, "MonthFirstTime");
            commUpdate.Parameters.Add("@Vouchers", SqlDbType.Int, 4, "Vouchers");
            commUpdate.Parameters.Add("@NonFood", SqlDbType.Int, 4, "NonFood");
            commUpdate.Parameters.Add("@LbsSupplemental", SqlDbType.Int, 4, "LbsSupplemental");
            commUpdate.Parameters.Add("@RcvdSupplemental", SqlDbType.Bit, 1, "RcvdSupplemental");
            commUpdate.Parameters.Add("@LbsCSFP", SqlDbType.Int, 4, "LbsCSFP");
            commUpdate.Parameters.Add("@RcvdCSFP", SqlDbType.Bit, 1, "RcvdCSFP");
            commUpdate.Parameters.Add("@RcvdVoucher", SqlDbType.Bit, 1, "RcvdVoucher");
            commUpdate.Parameters.Add("@Homeless", SqlDbType.Bit, 1, "Homeless");
            commUpdate.Parameters.Add("@InCityLimits", SqlDbType.Bit, 1, "InCityLimits");
            commUpdate.Parameters.Add("@Disabled", SqlDbType.Int, 4, "Disabled");
            commUpdate.Parameters.Add("@ClientType", SqlDbType.SmallInt, 2, "ClientType");
            #endregion
        }

        private void setInsertCommand()
        {
            #region InsertString
            string insertCommand = "Insert Into " + tbName + " (TrxDate, "
                    + "HouseholdID, Meals, Bags, LbsStd, "
                    + "LbsOther, LbsCommodity, Notes, "
                    + "ConcatFoodSvcItemsList, ConcatNonFoodSvcItemsList, "
                    + "FoodSvcList, NonFoodSvcList, Infants, "
                    + "Youth, Adults, Seniors, TotalFamily, "
                    + "SpecialDiet, NumCat1, NumCat2, RcvdCommodity, "
                    + "FirstTimeEver, FiscalFirstTime, CalFirstTime, "
                    + "Transient, EthnicSpeaking, Created, CreatedBy, "
                    + "Modified, ModifiedBy, MonthFirstTime, Vouchers, "
                    + "NonFood, LbsSupplemental, RcvdSupplemental, "
                    + "LbsCSFP, RcvdCSFP, RcvdVoucher, Homeless, InCityLimits, "
                    + "ClientType, Disabled) Values(@TrxDate, "
                    + "@HouseholdID, @Meals, @Bags, @LbsStd, "
                    + "@LbsOther, @LbsCommodity, @Notes, "
                    + "@ConcatFoodSvcItemsList, @ConcatNonFoodSvcItemsList, "
                    + "@FoodSvcList, @NonFoodSvcList, @Infants, "
                    + "@Youth, @Adults, @Seniors, @TotalFamily, "
                    + "@SpecialDiet, @NumCat1, @NumCat2, @RcvdCommodity, "
                    + "@FirstTimeEver, @FiscalFirstTime, @CalFirstTime, "
                    + "@Transient, @EthnicSpeaking, @Created, @CreatedBy, "
                    + "@Modified, @ModifiedBy, @MonthFirstTime, @Vouchers, "
                    + "@NonFood, @LbsSupplemental, @RcvdSupplemental, "
                    + "@LbsCSFP, @RcvdCSFP, @RcvdVoucher, @Homeless, "
                    + "@InCityLimits, @ClientType, @Disabled)";
            #endregion

            SqlCommand insertComm = new SqlCommand(insertCommand, conn);
            dadAdpt.InsertCommand = insertComm;

            #region Insert Parameters
            insertComm.Parameters.Add("@TrxId", SqlDbType.Int, 4, "TrxId");
            insertComm.Parameters.Add("@TrxDate", SqlDbType.DateTime, 4, "TrxDate");
            insertComm.Parameters.Add("@HouseholdID", SqlDbType.Int, 4, "HouseholdID");
            insertComm.Parameters.Add("@Meals", SqlDbType.SmallInt, 2, "Meals");
            insertComm.Parameters.Add("@Bags", SqlDbType.SmallInt, 2, "Bags");
            insertComm.Parameters.Add("@LbsStd", SqlDbType.Int, 4, "LbsStd");
            insertComm.Parameters.Add("@LbsOther", SqlDbType.Int, 4, "LbsOther");
            insertComm.Parameters.Add("@LbsCommodity", SqlDbType.Int, 4, "LbsCommodity");
            insertComm.Parameters.Add("@Notes", SqlDbType.NVarChar, 50, "Notes");
            insertComm.Parameters.Add("@ConcatFoodSvcItemsList", SqlDbType.NVarChar, 50, "ConcatFoodSvcItemsList");
            insertComm.Parameters.Add("@ConcatNonFoodSvcItemsList", SqlDbType.NVarChar, 50, "ConcatNonFoodSvcItemsList");
            insertComm.Parameters.Add("@FoodSvcList", SqlDbType.NVarChar, 50, "FoodSvcList");
            insertComm.Parameters.Add("@NonFoodSvcList", SqlDbType.NVarChar, 50, "NonFoodSvcList");
            insertComm.Parameters.Add("@Infants", SqlDbType.SmallInt, 2, "Infants");
            insertComm.Parameters.Add("@Youth", SqlDbType.SmallInt, 2, "Youth");
            insertComm.Parameters.Add("@Adults", SqlDbType.SmallInt, 2, "Adults");
            insertComm.Parameters.Add("@Seniors", SqlDbType.SmallInt, 2, "Seniors");
            insertComm.Parameters.Add("@TotalFamily", SqlDbType.Int, 4, "TotalFamily");
            insertComm.Parameters.Add("@SpecialDiet", SqlDbType.Int, 4, "SpecialDiet");
            insertComm.Parameters.Add("@NumCat1", SqlDbType.Int, 4, "NumCat1");
            insertComm.Parameters.Add("@NumCat2", SqlDbType.Int, 4, "NumCat2");
            insertComm.Parameters.Add("@RcvdCommodity", SqlDbType.Bit, 1, "RcvdCommodity");
            insertComm.Parameters.Add("@FirstTimeEver", SqlDbType.Bit, 1, "FirstTimeEver");
            insertComm.Parameters.Add("@FiscalFirstTime", SqlDbType.Bit, 1, "FiscalFirstTime");
            insertComm.Parameters.Add("@CalFirstTime", SqlDbType.Bit, 1, "CalFirstTime");
            insertComm.Parameters.Add("@Transient", SqlDbType.Bit, 1, "Transient");
            insertComm.Parameters.Add("@EthnicSpeaking", SqlDbType.Int, 4, "EthnicSpeaking");
            insertComm.Parameters.Add("@Created", SqlDbType.DateTime, 4, "Created");
            insertComm.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 50, "CreatedBy");
            insertComm.Parameters.Add("@Modified", SqlDbType.DateTime, 4, "Modified");
            insertComm.Parameters.Add("@ModifiedBy", SqlDbType.NVarChar, 50, "ModifiedBy");
            insertComm.Parameters.Add("@MonthFirstTime", SqlDbType.Int, 4, "MonthFirstTime");
            insertComm.Parameters.Add("@Vouchers", SqlDbType.Int, 4, "Vouchers");
            insertComm.Parameters.Add("@NonFood", SqlDbType.Int, 4, "NonFood");
            insertComm.Parameters.Add("@LbsSupplemental", SqlDbType.Int, 4, "LbsSupplemental");
            insertComm.Parameters.Add("@RcvdSupplemental", SqlDbType.Bit, 1, "RcvdSupplemental");
            insertComm.Parameters.Add("@LbsCSFP", SqlDbType.Int, 4, "LbsCSFP");
            insertComm.Parameters.Add("@RcvdCSFP", SqlDbType.Bit, 1, "RcvdCSFP");
            insertComm.Parameters.Add("@RcvdVoucher", SqlDbType.Bit, 1, "RcvdVoucher");
            insertComm.Parameters.Add("@Homeless", SqlDbType.Bit, 1, "Homeless");
            insertComm.Parameters.Add("@InCityLimits", SqlDbType.Bit, 1, "InCityLimits");
            insertComm.Parameters.Add("@Disabled", SqlDbType.Bit, 1, "Disabled");
            insertComm.Parameters.Add("@ClientType", SqlDbType.SmallInt, 2, "ClientType");
            #endregion
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
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString(),
                    CCFBGlobal.serverName);
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

    }
}