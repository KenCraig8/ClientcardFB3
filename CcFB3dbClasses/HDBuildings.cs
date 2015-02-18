using System;
using System.Data;
using System.Data.SqlClient;

namespace ClientcardFB3
{
    public class HDBuildings : IDisposable
    {
        string connString;
        SqlDataAdapter dadAdpt;
        DataSet dset;
        SqlCommand command;
        SqlCommandBuilder commBuilder;
        System.Data.SqlClient.SqlConnection conn;
        static string tbName = "HDBuildings";
        DataRow drow;
        int iRowCount = 0;
        private bool _disposed;
        
        public HDBuildings(string connStringIn)
        {
            conn = new System.Data.SqlClient.SqlConnection();
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
        public DataSet DSet
        {
            get { return dset; }
        }

        public int RowCount
        {
            get { return iRowCount; }
        }

        public DataRow DRow
        {
            get { return drow; }
        }

        public int ID
        {
            get { return Convert.ToInt32(drow["ID"]); }
            set { drow["ID"] = value; }
        }
        public string BldgName
        {
            get { return drow["BldgName"].ToString(); }
            set { drow["BldgName"] = value; }
        }
        public string BldgAddress
        {
            get { return drow["BldgAddress"].ToString(); }
            set { drow["BldgAddress"] = value; }
        }
        public string BldgCity
        {
            get { return drow["BldgCity"].ToString(); }
            set { drow["BldgCity"] = value; }
        }
        public string BldgState
        {
            get { return drow["BldgState"].ToString(); }
            set { drow["BldgState"] = value; }
        }
        public string BldgZip
        {
            get { return drow["BldgZip"].ToString(); }
            set { drow["BldgZip"] = value; }
        }
        public string BldgOperator
        {
            get { return drow["BldgOperator"].ToString(); }
            set { drow["BldgOperator"] = value; }
        }
        public string ContactName
        {
            get { return drow["ContactName"].ToString(); }
            set { drow["ContactName"] = value; }
        }
        public string ContactPhone
        {
            get { return drow["ContactPhone"].ToString(); }
            set { drow["ContactPhone"] = value; }
        }
        public string ContactAptNbr
        {
            get { return drow["ContactAptNbr"].ToString(); }
            set { drow["ContactAptNbr"] = value; }
        }
        public string ContactEmail
        {
            get { return drow["ContactEmail"].ToString(); }
            set { drow["ContactEmail"] = value; }
        }

        public int HDRoute
        {
            get { return Convert.ToInt32(drow["HDRoute"]); }
            set { drow["HDRoute"] = value; }
        }
        public DateTime Created
        {
            get { return Convert.ToDateTime(drow["Created"]); }
            set { drow["Created"] = value; }
        }
        public string CreatedBy
        {
            get { return drow["CreatedBy"].ToString(); }
            set { drow["CreatedBy"] = value; }
        }
        public DateTime Modified
        {
            get { return Convert.ToDateTime(drow["Modified"]); }
            set { drow["Modified"] = value; }
        }
        public string ModifiedBy
        {
            get { return drow["ModifiedBy"].ToString(); }
            set { drow["ModifiedBy"] = value; }
        }

        #endregion

        //-----------------------------DATA VALUE--------------------------------------------------------------------
        /// <summary>
        ///An Overloaded set of get/set funtions that will take in any kind of data value used in 
        ///the front end and accsess the data set for that data type, used mostly for a collection
        ///of textboxes so collection can be itterated through in one loop and have one function called
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
                        if (drow[FieldName].ToString().Length >0)
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
                if (ID == dset.Tables[tbName].Rows[i].Field<int>("ID"))
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
                return drow;
            }
            return null;
        }

        public void open(System.Int32 key)
        {
            try
            {
                openConnection();
                command = new SqlCommand("SELECT * FROM " + tbName + " WHERE ID=" + key.ToString(), conn);
                dadAdpt.SelectCommand = command;
                dset.Clear();
                iRowCount = dadAdpt.Fill(dset, tbName);
                closeConnection();
                if (iRowCount > 0)
                    drow = dset.Tables[tbName].Rows[0];
            }
            catch (SqlException ex) 
            {
                iRowCount = 0;
                drow = null;
                CCFBGlobal.appendErrorToErrorReport("Select Command = " + command.CommandText, 
                    ex.GetBaseException().ToString());
            }
        }

        public void openWhere(string whereClause)
        {
            try
            {
                openConnection();
                command = new SqlCommand("SELECT * FROM " + tbName + whereClause, conn);
                dadAdpt.SelectCommand = command;
                dset.Clear();
                iRowCount = dadAdpt.Fill(dset, tbName);
                closeConnection();
                if (iRowCount > 0)
                    drow = dset.Tables[tbName].Rows[0];
            }
            catch (SqlException ex)
            {
                iRowCount = 0;
                drow = null;
                CCFBGlobal.appendErrorToErrorReport("Select Command = " + command.CommandText,
                    ex.GetBaseException().ToString());
            }
        }

        public bool delete(string key)
        {
            try
            {
                SqlCommand cmdDelete = new SqlCommand(" DELETE FROM " + tbName + " WHERE Id=" + key, conn);
                openConnection();
                int iRows = cmdDelete.ExecuteNonQuery();
                closeConnection();
                cmdDelete.Dispose();
                return (iRows == 1);
            }
            catch (SqlException ex) 
            {
                CCFBGlobal.appendErrorToErrorReport("Delete Command = " + " DELETE FROM " + tbName + " WHERE Id=" + key,
                    ex.GetBaseException().ToString());
                return false;
            }
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
                    dadAdpt.Update(dset, "HDBuildings");
                    conn.Close();
                }
                catch (SqlException ex) { }
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
}

