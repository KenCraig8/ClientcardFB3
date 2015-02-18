
using System;
using System.Data;
using System.Data.SqlClient;

namespace ClientcardFB3
{
    public class HHPoints : IDisposable
    {
        SqlDataAdapter dadAdpt;
        DataRow dRow;
        DataTable dtbl;
        SqlCommand sqlCmd;
        SqlCommandBuilder commBuilder;
        SqlConnection conn;
        const string tblName = "HHPoints";
        int iRowCount = 0;
        public string errorMsg;
        private bool _disposed;

        public HHPoints(SqlConnection connIN)
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
                    if (commBuilder != null)
                        commBuilder.Dispose();
                    if (dadAdpt != null)
                        dadAdpt.Dispose();
                }

                // Indicate that the instance has been disposed.
                conn = null;
                dtbl = null;
                sqlCmd = null;
                dadAdpt = null;
                commBuilder = null;
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

        public  int UID
        {
            get { return Convert.ToInt32(dRow["UID"]); }
            set { dRow["UID"] = value; }
        }
        public  int HhID
        {
            get { return Convert.ToInt32(dRow["HhID"]); }
            set { dRow["HhID"] = value; }
        }
        public  DateTime WeekOf
        {
            get { return CCFBGlobal.ValidDate(dRow["WeekOf"]); }
            set { dRow["WeekOf"] = value; }
        }
        public  int Allocated
        {
            get { return Convert.ToInt32(dRow["Allocated"]); }
            set { dRow["Allocated"] = value; }
        }
        public  int Pts0
        {
            get { return Convert.ToInt32(dRow["Pts0"]); }
            set { dRow["Pts0"] = value; }
        }
        public  int Pts1
        {
            get { return Convert.ToInt32(dRow["Pts1"]); }
            set { dRow["Pts1"] = value; }
        }
        public  int Pts2
        {
            get { return Convert.ToInt32(dRow["Pts2"]); }
            set { dRow["Pts2"] = value; }
        }
        public  int Pts3
        {
            get { return Convert.ToInt32(dRow["Pts3"]); }
            set { dRow["Pts3"] = value; }
        }
        public  int Pts4
        {
            get { return Convert.ToInt32(dRow["Pts4"]); }
            set { dRow["Pts4"] = value; }
        }
        public  int Pts5
        {
            get { return Convert.ToInt32(dRow["Pts5"]); }
            set { dRow["Pts5"] = value; }
        }
        public  int Pts6
        {
            get { return Convert.ToInt32(dRow["Pts6"]); }
            set { dRow["Pts6"] = value; }
        }
        public  DateTime Created
        {
            get { return CCFBGlobal.ValidDate(dRow["Created"]); }
            set { dRow["Created"] = value; }
        }
        public  string CreatedBy
        {
            get { return dRow["CreatedBy"].ToString(); }
            set { dRow["CreatedBy"] = value; }
        }
        public  DateTime Modified
        {
            get { return CCFBGlobal.ValidDate(dRow["Modified"]); }
            set { dRow["Modified"] = value; }
        }
        public  string ModifiedBy
        {
            get { return dRow["ModifiedBy"].ToString(); }
            set { dRow["ModifiedBy"] = value; }
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
                SqlCommand cmdDelete = new SqlCommand(" DELETE FROM HHPoints WHERE UID=" + key.ToString(), conn);
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

        public bool insert()
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
                dtbl.Rows.Add(newrow);
                dRow = newrow;
                Created = DateTime.Now;
                CreatedBy = CCFBGlobal.dbUserName;
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
            openWhere("UID=" + key.ToString() );
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
                    commBuilder = new SqlCommandBuilder(dadAdpt);
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

