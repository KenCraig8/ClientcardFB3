using System;
using System.Data;
using System.Data.SqlClient;

namespace ClientcardFB3
{
    public class MonthlyReports : IDisposable
    {
        string connString;
        SqlDataAdapter dadAdpt;
        DataSet dset;
        SqlCommand command;
        SqlConnection conn;
        static string tbName = "MonthlyReports";
        bool isValid = false;
        int iRowCount = 0;
        DataRow drow = null;
        private bool _disposed;

        public MonthlyReports(string connStringIn)
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
        public bool ISValid
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

        public int RowCount
        {
            get
            {
                return iRowCount;
            }
        }

        public int ID
        {
            get
            {
                return Convert.ToInt32(drow["ID"]);
            }
            set
            {
                drow["ID"] = value;
            }
        }

        public string ReportName
        {
            get
            {
                return drow["ReportName"].ToString();
            }
            set
            {
                drow["ReportName"] = value;
            }
        }

        public string EmailAddresses
        {
            get
            {
                return drow["EmailAddresses"].ToString();
            }
            set
            {
                drow["EmailAddresses"] = value;
            }
        }

        public string ReportPath
        {
            get
            {
                return drow["ReportPath"].ToString();
            }
            set
            {
                drow["ReportPath"] = value;
            }
        }

        public bool ReportActive
        {
            get
            {
                return (bool)drow["ReportActive"];
            }
            set
            {
                drow["ReportActive"] = value;
            }
        }
        public string DocType
        {
            get
            {
                return drow["DocType"].ToString();
            }
            set
            {
                drow["DocType"] = value;
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

        public DateTime Modified
        {
            get
            {
                if (String.IsNullOrEmpty(drow["Modified"].ToString()) == true)
                    return CCFBGlobal.FBNullDateValue;
                else
                    return (DateTime)drow["Modified"];
            }
            set
            {
                drow["Modified"] = value;
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
            if (drow != null)
                return drow[FieldName];

            return "";
        }

        #endregion

        public void setDataRow(int rowIndex)
        {
            drow = dset.Tables[tbName].Rows[rowIndex];
        }

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

        public void find(string ReportName)
        {
            for (int i = 0; i < iRowCount; i++)
            {
                if (ReportName.Trim().ToUpper() == 
                    dset.Tables[tbName].Rows[i].Field<string>("ReportName").Trim().ToUpper())
                {
                    drow = dset.Tables[tbName].Rows[i];
                    break;
                }
            }
        }

        public bool open(int ID)
        {
            try
            {
                openConnection();
                command = new SqlCommand("SELECT * FROM " + tbName + " WHERE ID=" + ID.ToString(), conn);
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
                CCFBGlobal.appendErrorToErrorReport("key="+ID.ToString(), ex.GetBaseException().ToString());
                closeConnection();
                iRowCount = 0;
                return isValid = false;
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
                {
                    drow = dset.Tables[tbName].Rows[0];
                    return;
                }
                drow = null;
            }
            catch (SqlException ex)
            {
                closeConnection();
                iRowCount = 0;
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString());
            }
        }

        public void delete(int ID)
        {  
            SqlCommand commDelete = new SqlCommand(" DELETE FROM " + tbName + " WHERE ID=" + ID.ToString(), conn);
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

                    //drow["Modified"] = DateTime.Now.ToString();
                    //drow["ModifiedBy"] = CCFBGlobal.dbUserName;

                    if (dadAdpt.UpdateCommand == null)
                    {
                        SqlCommandBuilder commBuilder = new SqlCommandBuilder(dadAdpt);
                    }

                    dadAdpt.Update(dset, "MonthlyReports");
                    closeConnection();
                }
                catch (SqlException ex) 
                {
                    CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString());
                    closeConnection();
                }
            }
        }

        /// <summary>
        /// Opens a connection to the Database
        /// </summary>
        private void openConnection()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
        }

        /// <summary>
        /// Closes a connection to the Database
        /// </summary>
        private void closeConnection()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
    }
}

