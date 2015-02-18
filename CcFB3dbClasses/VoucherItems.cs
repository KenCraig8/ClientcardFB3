using System;
using System.Data;
using System.Data.SqlClient;

namespace ClientcardFB3
{

    public class VoucherItems : IDisposable
    {
        string connString;
        SqlDataAdapter dadAdpt;
        DataSet dset;
        SqlCommand command;
        SqlCommandBuilder commBuilder;
        SqlConnection conn;
        static string tbName = "VoucherItems";
        bool bisValid = false;
        int iRowCount = 0;
        DataRow drow;
        private bool _disposed;

        public VoucherItems(string connStringIn)
        {
            conn = new SqlConnection();
            connString = connStringIn;
            conn.ConnectionString = connString;
            dset = new DataSet();
            dadAdpt = new SqlDataAdapter();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            // If you need thread safety, use a lock around these 
            // operations, as well as in your methods that use the resource.
            if (!_disposed)
            {
                if (disposing)
                {
                    if (conn != null)
                        conn.Dispose();
                    if (dset != null)
                        dset.Dispose();
                    if (command != null)
                        command.Dispose();
                    if (commBuilder != null)
                        commBuilder.Dispose();
                    if (dadAdpt != null)
                        dadAdpt.Dispose();
                }

                // Indicate that the instance has been disposed.
                conn = null;
                dset = null;
                command = null;
                dadAdpt = null;
                _disposed = true;
            }
        }

        #region Get/Set Accessors
        public bool isValid
        {
            get { return bisValid; }
            set { bisValid = value; }
        }

        public DataSet DSet
        {
            get { return dset; }
            set { dset = value; }
        }

        public int RowCount
        {
            get { return iRowCount; }
        }

        public int UID
        {
            get { return Convert.ToInt32(drow["UID"]);}
            set { drow["UID"] = value; }
        }
        public string Description
        {
            get { return drow["Description"].ToString(); }
            set { drow["Description"] = value; }
        }
        public int VoucherType
        {
            get { return Convert.ToInt32(drow["VoucherType"]); }
            set { drow["VoucherType"] = value; }
        }
        public Boolean Inactive
        {
            get { return Convert.ToBoolean(drow["Inactive"]); }
            set { drow["Inactive"] = value; }
        }
        public decimal DefaultAmount
        {
            get { return Convert.ToDecimal(drow["DefaultAmount"]); }
            set { drow["DefaultAmount"] = value; }
        }

        public decimal MaxAmount
        {
            get { return Convert.ToDecimal(drow["MaxAmount"]); }
            set { drow["MaxAmount"] = value; }
        }
        public int DisplayCol
        {
            get { return Convert.ToInt32(drow["DisplayCol"]); }
            set { drow["DisplayCol"] = value; }
        }
        public int DisplayRow
        {
            get { return Convert.ToInt32(drow["DisplayRow"]); }
            set { drow["DisplayRow"] = value; }
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
        //Sets data value when value is a int
        public void SetDataValue(string FieldName, int FieldValue)
        {
            drow[FieldName] = FieldValue;
        }
        //Sets data value when value is a bool
        public void SetDataValue(string FieldName, bool FieldValue)
        {
            drow[FieldName] = FieldValue;
        }
        //Sets data value when value is a DateTime
        public void SetDataValue(string FieldName, DateTime FieldValue)
        {
            drow[FieldName] = FieldValue;
        }
        //Sets data value when value is a float
        public void SetDataValue(string FieldName, float FieldValue)
        {
            drow[FieldName] = FieldValue;
        }
        //Gets property through use of just the collum name in database
        public object GetDataValue(string FieldName)
        {
            return drow[FieldName];
        }

        #endregion

        public bool open(int UID)
        {
            try
            {
                openConnection();
                command = new SqlCommand("SELECT * FROM " + tbName + " WHERE UID=" + UID.ToString(), conn);
                dadAdpt.SelectCommand = command;
                dset.Clear();
                iRowCount = dadAdpt.Fill(dset, tbName);
                closeConnection();
                if (iRowCount > 0)
                {
                    drow = dset.Tables[tbName].Rows[0];
                    return isValid = true;
                }
                return false;
            }
            catch (SqlException ex) 
            {
                closeConnection();
                CCFBGlobal.appendErrorToErrorReport("UID=" + UID.ToString(), ex.GetBaseException().ToString());
                return bisValid = false;
            }
        }

        public void openAll()
        {
            try
            {
                openConnection();
                command = new SqlCommand("SELECT * FROM " + tbName, conn);
                dadAdpt.SelectCommand = command;
                dset.Clear();
                iRowCount = dadAdpt.Fill(dset, tbName);
                closeConnection();
                bisValid = false;
                if (iRowCount > 0)
                {
                    drow = dset.Tables[tbName].Rows[0];
                }
            }
            catch (SqlException ex) 
            {
                bisValid = false; 
                closeConnection();
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString());
            }
        }

        public void delete(int UID)
        {
            SqlCommand commDelete = new SqlCommand(" DELETE FROM " + tbName + " WHERE ID=" + UID.ToString(), conn);
            openConnection();
            commDelete.ExecuteNonQuery();
            commDelete.Dispose();
            closeConnection();
        }


        public void update()
        {
            if (dset.HasChanges() == true)
            {
                try
                {
                    openConnection();
                    if (dadAdpt.UpdateCommand == null)
                    {
                        commBuilder = new SqlCommandBuilder(dadAdpt);
                    }
                    dadAdpt.Update(dset, "VoucherItems");
                    closeConnection();
                }
                catch (SqlException ex) 
                {
                    closeConnection();
                    CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString());
                }
            }
        }

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

    /// <summary>
    /// 
    /// </summary>
    public class VoucherLog : IDisposable
    {
        SqlDataAdapter dadAdpt;
        DataRow dRow;
        DataTable dtbl;
        SqlCommand sqlCmd;
        SqlConnection conn;
        const string tblName = "VoucherLog";
        int iRowCount = 0;
        public string errorMsg;
        private bool _disposed;

        public VoucherLog(SqlConnection connIN)
        {
            conn = connIN;
            dtbl = new DataTable();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            // If you need thread safety, use a lock around these 
            // operations, as well as in your methods that use the resource.
            if (!_disposed)
            {
                if (disposing)
                {
                    if (conn != null)
                        conn.Dispose();
                    if (dtbl != null)
                        dtbl.Dispose();
                    if (sqlCmd != null)
                        sqlCmd.Dispose();
                    if (dadAdpt != null)
                        dadAdpt.Dispose();
                }

                // Indicate that the instance has been disposed.
                conn = null;
                dtbl = null;
                sqlCmd = null;
                dadAdpt = null;
                _disposed = true;
            }
        }

        #region Get/Set Accessors
        public DataTable DTable
        {
            get { return dtbl; }
            set { dtbl = value; iRowCount = dtbl.Rows.Count; }
        }
        public int RowCount
        {
            get { return iRowCount; }
        }

        public int TrxId
        {
            get { return Convert.ToInt32(dRow["TrxId"]); }
            set { dRow["TrxId"] = value; }
        }
        public DateTime TrxDate
        {
            get { return CCFBGlobal.ValidDate(dRow["TrxDate"]); }
            set { dRow["TrxDate"] = value; }
        }
        public int HouseholdID
        {
            get { return Convert.ToInt32(dRow["HouseholdID"]); }
            set { dRow["HouseholdID"] = value; }
        }
        public decimal Amount
        {
            get { return (decimal)dRow["Amount"]; }
            set { dRow["Amount"] = value; }
        }
        public string Notes
        {
            get { return dRow["Notes"].ToString(); }
            set { dRow["Notes"] = value; }
        }
        public int VoucherItemID
        {
            get { return Convert.ToInt32(dRow["VoucherItemID"]); }
            set { dRow["VoucherItemID"] = value; }
        }
        public int Infants
        {
            get { return Convert.ToInt32(dRow["Infants"]); }
            set { dRow["Infants"] = value; }
        }
        public int Youth
        {
            get { return Convert.ToInt32(dRow["Youth"]); }
            set { dRow["Youth"] = value; }
        }
        public int Teens
        {
            get { return Convert.ToInt32(dRow["Teens"]); }
            set { dRow["Teens"] = value; }
        }
        public int Eighteen
        {
            get { return Convert.ToInt32(dRow["Eighteen"]); }
            set { dRow["Eighteen"] = value; }
        }
        public int Adults
        {
            get { return Convert.ToInt32(dRow["Adults"]); }
            set { dRow["Adults"] = value; }
        }
        public int Seniors
        {
            get { return Convert.ToInt32(dRow["Seniors"]); }
            set { dRow["Seniors"] = value; }
        }
        public int TotalFamily
        {
            get { return Convert.ToInt32(dRow["TotalFamily"]); }
            set { dRow["TotalFamily"] = value; }
        }
        public bool FirstTimeEver
        {
            get { return (bool)dRow["FirstTimeEver"]; }
            set { dRow["FirstTimeEver"] = value; }
        }
        public bool FiscalFirstTime
        {
            get { return (bool)dRow["FiscalFirstTime"]; }
            set { dRow["FiscalFirstTime"] = value; }
        }
        public bool CalFirstTime
        {
            get { return (bool)dRow["CalFirstTime"]; }
            set { dRow["CalFirstTime"] = value; }
        }
        public bool MonthFirstTime
        {
            get { return (bool)dRow["MonthFirstTime"]; }
            set { dRow["MonthFirstTime"] = value; }
        }
        public bool Homeless
        {
            get { return (bool)dRow["Homeless"]; }
            set { dRow["Homeless"] = value; }
        }
        public bool InCityLimits
        {
            get { return (bool)dRow["InCityLimits"]; }
            set { dRow["InCityLimits"] = value; }
        }
        public int Disabled
        {
            get { return Convert.ToInt32(dRow["Disabled"]); }
            set { dRow["Disabled"] = value; }
        }
        public DateTime Created
        {
            get { return CCFBGlobal.ValidDate(dRow["Created"]); }
            set { dRow["Created"] = value; }
        }
        public string CreatedBy
        {
            get { return dRow["CreatedBy"].ToString(); }
            set { dRow["CreatedBy"] = value; }
        }
        public DateTime Modified
        {
            get { return CCFBGlobal.ValidDate(dRow["Modified"]); }
            set { dRow["Modified"] = value; }
        }
        public string ModifiedBy
        {
            get { return dRow["ModifiedBy"].ToString(); }
            set { dRow["ModifiedBy"] = value; }
        }
        public int ClientType
        {
            get { return Convert.ToInt32(dRow["ClientType"]); }
            set { dRow["ClientType"] = value; }
        }
        public string Zipcode
        {
            get { return dRow["Zipcode"].ToString(); }
            set { dRow["Zipcode"] = value; }
        }
        public int SpecialDiet
        {
            get { return Convert.ToInt32(dRow["SpecialDiet"]); }
            set { dRow["SpecialDiet"] = value; }
        }

        #endregion Get/Set Accessors

        #region Data Value Accsessors
        //-----------------------------DATA VALUE--------------------------------------------------------------------
        /// <summary>
        ///An Overloaded set of get/set funtions that will take in any kind of data value used in 
        ///the front end and accsess the data set for that data type, used mostly for a collection
        ///of textboxes so collection can be itterated through in one loop and have one function called
        ///no matter what type it actually refrenced
        /// </summary>
        /// <param name="FieldName">Fieldname=Column Name in the Database</param>
        /// <param name="FieldValue">FieldValue= .Net Data type</param>
        //Sets data value when value is a string
        public void SetDataValue(string FieldName, string FieldValue)
        {
            dRow[FieldName] = FieldValue;
        }
        //Sets data value when value is a bool
        public void SetDataValue(string FieldName, bool FieldValue)
        {
            dRow[FieldName] = FieldValue;
        }

        //Gets property through use of just the column name in database
        public object GetDataValue(string FieldName)
        {
            return dRow[FieldName];
        }

        //Gets property through use of just the column name in database as string
        public object GetDataString(string FieldName)
        {
            if (dtbl.Rows.Count > 0)
            {
                int fldIndex = dtbl.Columns.IndexOf(FieldName);
                if (fldIndex >= 0)
                {
                    if (dtbl.Columns[fldIndex].DataType.Name == "DateTime")
                        if (dRow[FieldName].ToString().Length >0)
                        { return CCFBGlobal.ValidDateString(dRow[FieldName]); }
                        else
                        { return ""; }
                    else
                        return dRow[FieldName].ToString();
                }
            }
            return "";
        }
        #endregion

        #region Generic Data Accessors
        //-------------------------------------Find------------------------------------
        /// <summary>
        /// Finds the UID in dataset and sets that row to the dRow
        /// </summary>
        /// <param name="ID"></param>
        ///
        public void find(int ID, bool getName)
        {
            for (int i = 0; i < iRowCount; i++)
            {
                if (ID == dtbl.Rows[i].Field<int>("ID"))
                {
                    dRow = dtbl.Rows[i];
                    break;
                }
            }
        }

        public void refreshDataTable()
        {
            try
            {
                openConnection();
                dtbl.Clear();
                iRowCount = dadAdpt.Fill(dtbl);
                closeConnection();
                if (iRowCount > 0)
                {
                    dRow = dtbl.Rows[0];
                }
            }
            catch (SqlException ex)
            {
                iRowCount = 0;
                dRow = null;
                errorMsg = ex.Message;
                //CCFBGlobal.appendErrorToErrorReport("Select Command = " + sqlCmd.CommandText,ex.GetBaseException().ToString());
            }
        }

        /// <summary>
        /// Sets the DataRow for the given RowIndex
        /// </summary>
        /// <param name="rowIndex"></param>
        public DataRow setDataRow(int rowIndex)
        {
            if (dtbl.Rows.Count >= rowIndex)
            {
                dRow = dtbl.Rows[rowIndex];
                return dRow;
            }
            return null;
        }
        #endregion


        public bool delete(System.Int32 key)
        {
            errorMsg = "";
            try
            {
                openConnection();
                SqlCommand cmdDelete = new SqlCommand(" DELETE FROM VoucherLog WHERE TrxId=" + key.ToString(), conn);
                int iRows = cmdDelete.ExecuteNonQuery();
                cmdDelete.Dispose();
                closeConnection();
                return (iRows > 0);
            }
            catch (SqlException ex)
            {
                errorMsg = ex.Message;
                return false;
            }
        }

        public int AddNewRow()
        {
            dtbl.Rows.Add(dRow);
            return dtbl.Rows.Count;
        }
        
        public bool CreateNewRow()
        {
            errorMsg = "";
            try
            {
                DataRow newrow = dtbl.NewRow();
                foreach (DataColumn item in dtbl.Columns)
                {
                    switch (item.DataType.ToString())
                    {
                        case "System.Boolean":
                            newrow[item.Ordinal] = 0;
                            break;
                        case "System.DateTime":
                            newrow[item.Ordinal] = CCFBGlobal.FBNullDateValue;
                            break;
                        case "System.Decimal":
                            newrow[item.Ordinal] = 0;
                            break;
                        case "System.Double":
                            newrow[item.Ordinal] = 0;
                            break;
                        case "System.Int16":
                            newrow[item.Ordinal] = 0;
                            break;
                        case "System.Int32":
                            newrow[item.Ordinal] = 0;
                            break;
                        case "System.Single":
                            newrow[item.Ordinal] = 0;
                            break;
                        case "System.String":
                            newrow[item.Ordinal] = "";
                            break;
                        default:
                            errorMsg += item.DataType.ToString();
                            break;
                    }
                }
                dRow = newrow;
                return true;
            }
            catch (SqlException ex)
            {
                errorMsg = ex.Message;
                closeConnection();
                //CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString());
                return false;
            }
        }

        public void open(System.Int32 key)
        {
            openWhere("TrxId=" + key.ToString());
        }

        public void openWhere(string whereclause)
        {
            string sqlTxt = "SELECT * FROM " + tblName;
            if (whereclause.Length >0)
            {
                sqlTxt += " WHERE " + whereclause;
            }
            errorMsg = "";
            try
            {
                openConnection();
                sqlCmd = new SqlCommand(sqlTxt, conn);
                dadAdpt = new SqlDataAdapter(sqlCmd);
                dadAdpt.SelectCommand = sqlCmd;
                dtbl.Clear();
                if (dtbl != null)
                {
                    dtbl.TableName = tblName;
                }
                iRowCount = dadAdpt.Fill(dtbl);
                closeConnection();
                if (iRowCount > 0)
                {
                    dRow = dtbl.Rows[0];
                }
            }
            catch (SqlException ex)
            {
                errorMsg = ex.Message;
                closeConnection();
            }
        }

        private void openConnection()
        {
            if (conn.State == ConnectionState.Closed)
            { conn.Open(); }
        }

        private void closeConnection()
        {
            if (conn.State == ConnectionState.Open)
            { conn.Close(); }
        }

        public bool update()
        {
            errorMsg = "";
            try
            {
                openConnection();
                if (dadAdpt.UpdateCommand == null || dadAdpt.InsertCommand == null)
                {
                    SqlCommandBuilder commBuilder = new SqlCommandBuilder(dadAdpt);
                }
                dadAdpt.Update(dtbl);
                closeConnection();
                return true;
            }
            catch (SqlException ex)
            {
                errorMsg = ex.Message;
                return false;
            }
        }
        public DataTable getDistinctRows(string fieldname)
        {
            try
            {
                openConnection();
                SqlCommand cmd = new SqlCommand("SELECT Distinct " + fieldname + " + FROM " + tblName + " WHERE " + fieldname + " > ' '", conn);
                dadAdpt = new SqlDataAdapter(cmd);
                DataTable datatbl = new DataTable();
                dadAdpt.Fill(datatbl);
                cmd.Dispose();
                closeConnection();
                return datatbl;
            }
            catch (SqlException ex)
            {
                closeConnection();
                return null;
            }
        }
    }
}

