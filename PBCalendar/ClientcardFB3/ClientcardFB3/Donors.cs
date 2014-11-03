using System;
using System.Data;
using System.Data.SqlClient;

namespace ClientcardFB3
{
    public class Donors
    {

        #region Donor Class Attributes
        string connString;
        SqlDataAdapter dadAdpt;
        DataSet dset;
        SqlCommand command;
        SqlConnection conn;
        static string tbName = "Donors";
        int iRowCount = 0;
        DataRow drow = null;
        bool isValid = false;
        #endregion

        public Donors(string connStringIn)
        {
            conn = new SqlConnection();
            connString = connStringIn;
            conn.ConnectionString = connString;
            dset = new DataSet();
            dadAdpt = new SqlDataAdapter();
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

        public bool Inactive
        {
            get
            {
                return (bool)drow["Inactive"];
            }
            set
            {
                drow["Inactive"] = value;
            }
        }

        public string Name
        {
            get
            {
                return drow["Name"].ToString();
            }
            set
            {
                drow["Name"] = value;
            }
        }

        public string Address
        {
            get
            {
                return drow["Address"].ToString();
            }
            set
            {
                drow["Address"] = value;
            }
        }

        public string City
        {
            get
            {
                return drow["City"].ToString();
            }
            set
            {
                drow["City"] = value;
            }
        }

        public string State
        {
            get
            {
                return drow["State"].ToString();
            }
            set
            {
                drow["State"] = value;
            }
        }

        public string ZipCode
        {
            get
            {
                return drow["ZipCode"].ToString();
            }
            set
            {
                drow["ZipCode"] = value;
            }
        }

        public string Phone
        {
            get
            {
                return drow["Phone"].ToString();
            }
            set
            {
                drow["Phone"] = value;
            }
        }

        public string CellPhone
        {
            get
            {
                return drow["CellPhone"].ToString();
            }
            set
            {
                drow["CellPhone"] = value;
            }
        }

        public string WorkPhone
        {
            get
            {
                return drow["WorkPhone"].ToString();
            }
            set
            {
                drow["WorkPhone"] = value;
            }
        }

        public string Company
        {
            get
            {
                return drow["Company"].ToString();
            }
            set
            {
                drow["Company"] = value;
            }
        }

        public int RcdType
        {
            get
            {
                return Convert.ToInt32(drow["RcdType"]);
            }
            set
            {
                drow["RcdType"] = value;
            }
        }

        public string ContactName
        {
            get
            {
                return drow["ContactName"].ToString();
            }
            set
            {
                drow["ContactName"] = value;
            }
        }

        public string ContactPhone
        {
            get
            {
                return drow["ContactPhone"].ToString();
            }
            set
            {
                drow["ContactPhone"] = value;
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

        public bool UserFlag0
        {
            get
            {
                return (bool)drow["UserFlag0"];
            }
            set
            {
                drow["UserFlag0"] = value;
            }
        }

        public bool UserFlag1
        {
            get
            {
                return (bool)drow["UserFlag1"];
            }
            set
            {
                drow["UserFlag1"] = value;
            }
        }

        public string Info1
        {
            get
            {
                return drow["Info1"].ToString();
            }
            set
            {
                drow["Info1"] = value;
            }
        }

        public string Info2
        {
            get
            {
                return drow["Info2"].ToString();
            }
            set
            {
                drow["Info2"] = value;
            }
        }

        public DateTime Date1
        {
            get
            {
                return (DateTime)(drow["Date1"]);
            }
            set
            {
                drow["Date1"] = value;
            }
        }

        public DateTime Date2
        {
            get
            {
                return (DateTime)(drow["Date2"]);
            }
            set
            {
                drow["Date2"] = value;
            }
        }

        public DateTime Created
        {
            get
            {
                return (DateTime)(drow["Created"]);
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
                return (DateTime)(drow["Modified"]);
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

        public int DefaultDonationType
        {
            get
            {
                return dset.Tables[tbName].Rows[0].Field<System.Int16>("DefaultDonationType");
            }
            set
            {
                drow["DefaultDonationType"] = value;
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
        //Gets property through use of just the collum name in database
        public object GetDataValue(string FieldName)
        {
            if (drow != null)
                return drow[FieldName];

            return "";
        }
        #endregion

        /// <summary>
        /// Sets the DataRow that the get/set accessors work off of using a Row Index
        /// </summary>
        /// <param name="rowIndex">The row index to set the drow to</param>
        public void setDataRow(int rowIndex)
        {
            if (rowIndex < iRowCount && rowIndex >= 0)
                drow = dset.Tables[tbName].Rows[rowIndex];
        }

        public bool find(int TrxID)
        {
            for (int i = 0; i < iRowCount; i++)
            {
                if (TrxID == dset.Tables[tbName].Rows[i].Field<int>("ID"))
                {
                    drow = dset.Tables[tbName].Rows[i];
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Gets the ditinct values for any collumn
        /// </summary>
        /// <param name="columnName">Collumn name</param>
        public void getDistincts(string columnName, string whereClause)
        {
            try
            {
                openConnection();
                command = new SqlCommand("SELECT " + columnName + ", COUNT(*) FROM "
                    + tbName + whereClause
                    + " Group By " + columnName + " Order By " + columnName, conn);
                dadAdpt.SelectCommand = command;
                dset.Clear();
                dset.Tables.Clear();
                iRowCount = dadAdpt.Fill(dset, tbName);
                closeConnection();
                isValid = false;
                if (iRowCount > 0)
                    drow = dset.Tables[tbName].Rows[0];
            }
            catch (SqlException ex)
            {
                closeConnection();
                CCFBGlobal.appendErrorToErrorReport("columnName=" + columnName, ex.GetBaseException().ToString(),
                    CCFBGlobal.serverName);
                iRowCount = 0;
            }
        }

        public bool open(int dnrID)
        {
            try
            {
                openConnection();
                command = new SqlCommand("SELECT * FROM " + tbName + " WHERE ID=" + dnrID.ToString(), conn);
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
                closeConnection();
                CCFBGlobal.appendErrorToErrorReport("ID=" + dnrID.ToString(), 
                    ex.GetBaseException().ToString(), CCFBGlobal.serverName);
                iRowCount = 0;
                return isValid = false;
            }
        }

        public void openWhere(string sWhereClause = "")
        {
            try
            {
                openConnection();
                if (sWhereClause == "")
                    command = new SqlCommand("SELECT * FROM " + tbName, conn);
                else
                    command = new SqlCommand("SELECT * FROM " + tbName + " WHERE " + sWhereClause, conn);

                dadAdpt.SelectCommand = command;
                dset.Clear();
                iRowCount = dadAdpt.Fill(dset, tbName);
                closeConnection();
                if (iRowCount > 0)
                {
                    drow = dset.Tables[tbName].Rows[0];
                }
                isValid = false;
            }
            catch (SqlException ex)
            {
                closeConnection();
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString(),
                     CCFBGlobal.serverName);
                iRowCount = 0;
                isValid = false;
            }
        }


        public void delete(System.Int32 key)
        {
            SqlCommand commDelete = new SqlCommand(" DELETE FROM " + tbName + " WHERE ID=" + ID.ToString(), conn);
            openConnection();
            commDelete.ExecuteNonQuery();
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
                        SqlCommandBuilder commBuilder = new SqlCommandBuilder(dadAdpt);
                    }
                    dadAdpt.Update(dset, "Donors");
                    closeConnection();
                }
                catch (SqlException ex) { }
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

