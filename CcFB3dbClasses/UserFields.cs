using System;
using System.Data;
using System.Data.SqlClient;

namespace ClientcardFB3
{
    public class UserFields : IDisposable
    {
        string connString;
        SqlDataAdapter dadAdpt;
        DataSet dset;
        SqlCommand command;
        SqlConnection conn;
        static string tbName = "UserFields";
        bool isValid = false;
        int iRowCount = 0;
        DataRow drow;
        private bool _disposed;

        public UserFields(string connStringIn)
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

        public int RowCount
        {
            get
            {
                return iRowCount;
            }
            set
            {
                iRowCount = value;
            }
        }

        public string TableName
        {
            get
            {
                return drow["TableName"].ToString();
            }
            set
            {
                drow["TableName"] = value;
            }
        }

        public string ControlType
        {
            get
            {
                return drow["ControlType"].ToString();
            }
            set
            {
                drow["ControlType"] = value;
            }
        }

        public string FldName
        {
            get
            {
                return drow["FldName"].ToString();
            }
            set
            {
                drow["FldName"] = value;
            }
        }

        public string FldVal
        {
            get
            {
                return drow["FldVal"].ToString();
            }
            set
            {
                drow["FldVal"] = value;
            }
        }

        public string EditLabel
        {
            get
            {
                return drow["EditLabel"].ToString();
            }
            set
            {
                drow["EditLabel"] = value;
            }
        }

        public string EditTip
        {
            get
            {
                return drow["EditTip"].ToString();
            }
            set
            {
                drow["EditTip"] = value;
            }
        }

        public string FldType
        {
            get
            {
                return drow["FldType"].ToString();
            }
            set
            {
                drow["FldType"] = value;
            }
        }

        public bool AutoAlert
        {
            get
            {
                return (bool)drow["AutoAlert"];
            }
            set
            {
                drow["AutoAlert"] = value;
            }
        }

        public string AutoAlertText
        {
            get
            {
                return drow["AutoAlertText"].ToString();
            }
            set
            {
                drow["AutoAlertText"] = value;
            }
        }

        //public int ControlWidth
        //{
        //    get
        //    {
        //        return Convert.ToInt32(drow["ControlWidth"]);
        //    }
        //    set
        //    {
        //        drow["ControlWidth"] = value;
        //    }
        //}
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
            if (drow != null)
                return drow[FieldName];

            return "";
        }

        public object GetDataValue(string FieldName, int rowNum)
        {
            try
            {
                return dset.Tables[tbName].Rows[rowNum][FieldName];
            }
            catch (IndexOutOfRangeException ex)
            {
                CCFBGlobal.appendErrorToErrorReport("FieldName=" + FieldName, ex.GetBaseException().ToString());
                return "";
            }
        }

        #endregion

        public bool open(string tableName)
        {
            try
            {
                openConnection();
                command = new SqlCommand("SELECT * FROM " + tbName + " WHERE TableName='" + tableName.ToString() + "'", conn);
                dadAdpt.SelectCommand = command;
                dset.Clear();
                iRowCount = dadAdpt.Fill(dset, tbName);
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
                sqlErrorOccured("TableName=" + tableName, ex.GetBaseException().ToString());
                return isValid = false;
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
                if (iRowCount > 0)
                {
                    drow = dset.Tables[tbName].Rows[0];
                }
            }
            catch (SqlException ex)
            {
                sqlErrorOccured("", ex.GetBaseException().ToString());
            }
        }

        public void getUniqeTableNames()
        {
            try
            {
                command = new SqlCommand("Select Distinct(TableName) From UserFields", conn);
                dadAdpt.SelectCommand = command;
                dset.Clear();
                iRowCount = dadAdpt.Fill(dset, tbName);
                closeConnection();
                drow = null;
            }
            catch (SqlException ex)
            {
                sqlErrorOccured("Select Command = " + command.CommandText, ex.GetBaseException().ToString());
            }
        }

        private void sqlErrorOccured(string commandText, string errorText)
        {
            closeConnection();
            CCFBGlobal.appendErrorToErrorReport(commandText, errorText);
            drow = null;
            iRowCount = 0;
        }

        public void setDataRow(int rowIndex)
        {
            if (rowIndex < iRowCount && rowIndex >= 0)
                drow = dset.Tables[tbName].Rows[rowIndex];
        }

        public void setDataRow(String fldName)
        {
            for (int i = 0; i < iRowCount; i++)
            {
                if (fldName == dset.Tables[tbName].Rows[i].Field<string>("FldName"))
                {
                    drow = dset.Tables[tbName].Rows[i];
                    return;
                }
            }
        }

        public void delete(string tableName)
        {
            SqlCommand commDelete = new SqlCommand(" DELETE FROM " + tbName + " WHERE TableName=" + tableName, conn);
            openConnection();
            commDelete.ExecuteNonQuery();
            commDelete.Dispose();
            closeConnection();
        }

        public void resetUserField( string tableName, string fldName, string value)
        {
            try
            {
                openConnection();
                command = new SqlCommand("Update " + tableName + " Set " + fldName + "=" + value
                    + " Where Inactive = 0", conn);
                command.ExecuteNonQuery();
                closeConnection();
            }
            catch (SqlException ex)
            {
                sqlErrorOccured(command.CommandText, ex.GetBaseException().ToString());
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
                        SqlCommandBuilder commBuilder = new SqlCommandBuilder(dadAdpt);
                    }
                    dadAdpt.Update(dset, "UserFields");
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
    public class UserFieldItem
    {
        private int fldindex;
        private string editlabel;
        private string fldname;

        public UserFieldItem(int fldIndex, string editLabel, string fldName)
        {
            fldindex = fldIndex;
            editlabel = editLabel;
            fldname = fldName;
        }

        public int FldIndex
        {
            get { return fldindex; }
            set { fldindex = value; }
        }
        public string EditLabel
        {
            get { return editlabel; }
            set { editlabel = value; }
        }
        public string FldName
        {
            get { return fldname; }
            set { fldname = value; }
        }
        public string LongName
        {
            get { return editlabel; }
        }
        public string Index
        {
            get { return fldindex.ToString(); }
        }
    }
}
