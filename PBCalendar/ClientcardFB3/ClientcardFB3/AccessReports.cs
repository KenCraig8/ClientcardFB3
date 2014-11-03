using System;
using System.Data;
using System.Data.SqlClient;

namespace ClientcardFB3
{
    public class AccessReports
    {
        string connString;
        SqlDataAdapter dadAdpt;
        DataSet dset;
        SqlCommand command;
        System.Data.SqlClient.SqlConnection conn;
        static string tbName = "AccessReports";
        int iRowCount = 0;
        bool isValid = false;
        DataRow dRow;

        public AccessReports(string connStringIn)
        {
            conn = new System.Data.SqlClient.SqlConnection();
            connString = connStringIn;
            conn.ConnectionString = connString;
            dset = new DataSet();
            dadAdpt = new SqlDataAdapter();
        }

        #region Get/Set Accessors
        public bool ISValid
        {
            get { return isValid; }
            set { isValid = value; }
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
        public int ID
        {
            get { return Convert.ToInt32(dRow["ID"]); }
            set { dRow["ID"] = value; }
        }
        public string Grouping
        {
            get { return dRow["Grouping"].ToString(); }
            set { dRow["Grouping"] = value; }
        }
        public string RptGroup
        {
            get { return dRow["RptGroup"].ToString(); }
            set { dRow["RptGroup"] = value; }
        }
        public string ReportTitle
        {
            get { return dRow["ReportTitle"].ToString(); }
            set { dRow["ReportTitle"] = value; }
        }
        public int DateRangeType
        {
            get { return Convert.ToInt32(dRow["DateRangeType"]); }
            set { dRow["DateRangeType"] = value; }
        }
        public string Date0
        {
            get { return dRow["Date0"].ToString(); }
            set { dRow["Date0"] = value; }
        }
        public string Date1
        {
            get { return dRow["Date1"].ToString(); }
            set { dRow["Date1"] = value; }
        }
        public string DisplayName
        {
            get { return dRow["Display Name"].ToString(); }
            set { dRow["Display Name"] = value; }
        }
        public bool UseActive
        {
            get { return (bool)dRow["UseActive"]; }
            set { dRow["UseActive"] = value; }
        }
        public bool UseWhere
        {
            get { return (bool)dRow["UseWhere"]; }
            set { dRow["UseWhere"] = value; }
        }
        public string WhereClause
        {
            get { return dRow["WhereClause"].ToString(); }
            set { dRow["WhereClause"] = value; }
        }
        public bool UseFilter
        {
            get { return (bool)dRow["UseFilter"]; }
            set { dRow["UseFilter"] = value; }
        }
        public string FilterName
        {
            get { return dRow["FilterName"].ToString(); }
            set { dRow["FilterName"] = value; }
        }
        public string SqlQuery
        {
            get { return dRow["SqlQuery"].ToString(); }
            set { dRow["SqlQuery"] = value; }
        }
        public string LabelLowDate
        {
            get { return dRow["LabelLowDate"].ToString(); }
            set { dRow["LabelLowDate"] = value; }
        }
        public string LabelHiDate
        {
            get { return dRow["LabelHiDate"].ToString(); }
            set { dRow["LabelHiDate"] = value; }
        }
        public bool Preview
        {
            get { return (bool)dRow["Preview"]; }
            set { dRow["Preview"] = value; }
        }
        public bool AllowBlank
        {
            get { return (bool)dRow["AllowBlank"]; }
            set { dRow["AllowBlank"] = value; }
        }
        public int CboResultType
        {
            get { return Convert.ToInt32(dRow["CboResultType"]); }
            set { dRow["CboResultType"] = value; }
        }

        #endregion Get/Set Accessors

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
            dRow[FieldName] = FieldValue;
        }
        //Gets property through use of just the collum name in database
        public object GetDataValue(string FieldName)
        {
            if (dRow != null)
                return dRow[FieldName];

            return "";
        }
        #endregion

        public void setDataRow(int rowIndex)
        {
            if (rowIndex < iRowCount)
                dRow = dset.Tables[tbName].Rows[rowIndex];
        }

        public void find(int ID)
        {
            for (int i = 0; i < iRowCount; i++)
            {
                if (ID == dset.Tables[tbName].Rows[i].Field<int>("ID"))
                {
                    dRow = dset.Tables[tbName].Rows[i];
                    break;
                }
            }
        }

        public void getDistincts(string columnName)
        {
            try
            {
                openConnection();
                command = new SqlCommand("SELECT Distinct " + columnName + " FROM " + tbName
                    + " a WHERE DateRangeType >= 0 Order By a." + columnName, conn);
                dadAdpt.SelectCommand = command;
                dset.Clear();
                iRowCount = dadAdpt.Fill(dset, tbName);
                closeConnection();
            }
            catch (SqlException ex)
            {
                CCFBGlobal.appendErrorToErrorReport("key=" + ID.ToString(), ex.GetBaseException().ToString(),
                       CCFBGlobal.serverName);
                closeConnection();
                iRowCount = 0;
            }
        }

        public bool open(System.Int32 key)
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
                {
                    dRow = dset.Tables[0].Rows[0];
                    return isValid = true;
                }
                return isValid = false;
            }
            catch (SqlException ex)
            {
                CCFBGlobal.appendErrorToErrorReport("key=" + ID.ToString(), ex.GetBaseException().ToString(),
                       CCFBGlobal.serverName);
                closeConnection();
                iRowCount = 0;
                return isValid = false;
            }
        }

        public void openAll()
        {
            try
            {
                openConnection();
                command = new SqlCommand("SELECT * FROM " + tbName + " ORDER BY RptGroup, Grouping,[Display Name]", conn);
                dadAdpt.SelectCommand = command;
                dset.Clear();
                iRowCount = dadAdpt.Fill(dset, tbName);
                closeConnection();
                if (iRowCount > 0)
                {
                    dRow = dset.Tables[tbName].Rows[0];
                    return;
                }
                dRow = null;
            }
            catch (SqlException ex)
            {
                closeConnection();
                iRowCount = 0;
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString(),
                    CCFBGlobal.serverName);
            }
        }

        public void openWhere(string whereClause)
        {
            try
            {
                openConnection();
                command = new SqlCommand("SELECT * FROM " + tbName + " " + whereClause + " ORDER BY RptGroup, Grouping,[Display Name]", conn);
                dadAdpt.SelectCommand = command;
                dset.Clear();
                iRowCount = dadAdpt.Fill(dset, tbName);
                closeConnection();
                if (iRowCount > 0)
                {
                    dRow = dset.Tables[tbName].Rows[0];
                    return;
                }
                dRow = null;
            }
            catch (SqlException ex)
            {
                closeConnection();
                iRowCount = 0;
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString(),
                    CCFBGlobal.serverName);
            }
        }

        public void delete(System.Int32 key)
        {
            SqlCommand commDelete = new SqlCommand(" DELETE FROM AccessReports WHERE ID=" + ID.ToString(), conn);
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
                    conn.Open();
                    if (dadAdpt.UpdateCommand == null)
                    {
                        SqlCommandBuilder commBuilder = new SqlCommandBuilder(dadAdpt);
                    }
                    dadAdpt.Update(dset, "AccessReports");
                    conn.Close();
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

