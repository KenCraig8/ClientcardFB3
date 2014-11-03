
using System;
using System.Data;
using System.Data.SqlClient;

namespace ClientcardFB3
{
    public class IncomeGroups
    {
        SqlDataAdapter dadAdpt;
        DataSet dset;
        DataRow dRow;
        SqlCommand command;
        System.Data.SqlClient.SqlConnection conn;
        static string tbName = "IncomeGroups";
        int rowIndexCurrent = -1;
        int iRowCount;

        public IncomeGroups()
        {
            conn = new System.Data.SqlClient.SqlConnection();
            conn.ConnectionString = CCFBGlobal.connectionString;
            dset = new DataSet();
            dadAdpt = new SqlDataAdapter();
        }

        #region Get/Set Accessors
        public DataSet DSet
        {
            get { return dset; }
            set { dset = value; }
        }

        public DataRow DRow
        {
            get { return dRow; }
            set { dRow = value; }
        }
        public  int Id
        {
        get { return Convert.ToInt32(dRow["Id"]); }
        set { dRow["Id"] = value; }
        }
        public  string Description
        {
        get { return dRow["Description"].ToString(); }
        set { dRow["Description"] = value; }
        }
        public  string ShortName
        {
        get { return dRow["ShortName"].ToString(); }
        set { dRow["ShortName"] = value; }
        }
        public  string Notes
        {
        get { return dRow["Notes"].ToString(); }
        set { dRow["Notes"] = value; }
        }
        public  DateTime AsOfDate
        {
        get 
        {
            if (dRow["AsOfDate"].ToString() == "")
                return CCFBGlobal.FBNullDateValue;
            else
                return (DateTime)dRow["AsOfDate"];
        }
        set { dRow["AsOfDate"] = value; }
        }
        public int ProcessID
        {
            get { return Convert.ToInt32(dRow["ProcessID"]); }
            set { dRow["ProcessID"] = value; }
        }
        public  DateTime Created
        {
        get { return (DateTime)dRow["Created"]; }
        set { dRow["Created"] = value; }
        }
        public  string CreatedBy
        {
        get { return dRow["CreatedBy"].ToString(); }
        set { dRow["CreatedBy"] = value; }
        }
        public  DateTime Modified
        {
        get 
        {
            if (dRow["Modified"].ToString() == "")
                return CCFBGlobal.FBNullDateValue;
            else
                return (DateTime)dRow["Modified"];
        }
        set { dRow["Modified"] = value; }
        }
        public  string ModifiedBy
        {
        get { return dRow["ModifiedBy"].ToString(); }
        set { dRow["ModifiedBy"] = value; }
        }
        public int CurrentRow
        {
            get { return rowIndexCurrent; }
            set { rowIndexCurrent = value; }
        }
        public int RowCount
        {
            get { return iRowCount; }
        }
        #endregion Get/Set Accessors

        public int getCode(string description)
        {
            for (int i = 0; i < iRowCount; i++)
            {
                if (description == dset.Tables[tbName].Rows[i].Field<string>("Description"))
                {
                    return dset.Tables[tbName].Rows[i].Field<int>("ID");
                }
            }
            return -1;
        }


        public void open(int ID)
        {
            try
            {
                command = new SqlCommand("SELECT * FROM " + tbName + " WHERE Id=" + ID.ToString(), conn);
                dadAdpt.SelectCommand = command;
                dset.Clear();
                iRowCount = dadAdpt.Fill(dset, tbName);
            }
            catch (SqlException ex) 
            {
                CCFBGlobal.appendErrorToErrorReport("ID="+ID.ToString(), ex.GetBaseException().ToString());
            }
        }

        public void openAll()
        {
            try
            {
                command = new SqlCommand("SELECT * FROM " + tbName + " ORDER BY Description", conn);
                dadAdpt.SelectCommand = command;
                dset.Clear();
                iRowCount = dadAdpt.Fill(dset, tbName);
            }
            catch (SqlException ex) 
            {
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString());
            }
        }

        public void openWhere(string whereClause)
        {
            try
            {
                command = new SqlCommand("SELECT * FROM " + tbName + whereClause, conn);
                dadAdpt.SelectCommand = command;
                dset.Clear();
                iRowCount = dadAdpt.Fill(dset, tbName);
            }
            catch (SqlException ex)
            {
                CCFBGlobal.appendErrorToErrorReport("WhereClause = "+whereClause, ex.GetBaseException().ToString());
            }
        }
        //-------------------------------------Find------------------------------------
        /// <summary>
        /// Finds the ID in dataset and sets that row to the drow
        /// </summary>
        /// <param name="ID"></param>
        /// 
        public void find(int ID)
        {
            for (int i = 0; i < iRowCount; i++)
            {
                if (ID == dset.Tables[tbName].Rows[i].Field<int>("TrxID"))
                {
                    dRow = dset.Tables[tbName].Rows[i];
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
            if (iRowCount > rowIndex)
            {
                dRow = dset.Tables[tbName].Rows[rowIndex];
                rowIndexCurrent = rowIndex;
                return dRow;
            }
            return null;
        }

        public bool NextDataRow()
        {
            if (rowIndexCurrent + 1 < iRowCount)
            {
                rowIndexCurrent++;
                dRow = dset.Tables[0].Rows[rowIndexCurrent];
                return true;
            }
            else
                return false;
        }

        public bool PrevDataRow()
        {
            if (rowIndexCurrent > 0 && iRowCount > 0)
            {
                rowIndexCurrent--;
                dRow = dset.Tables[0].Rows[rowIndexCurrent];
                return true;
            }
            else
                return false;
        }

        public void delete(System.Int32 key)
        {
            SqlCommand commDelete = new SqlCommand("DELETE FROM " + tbName + " WHERE ID=" + key.ToString(), conn);
            openConnection();
            commDelete.BeginExecuteNonQuery();
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

                    dadAdpt.Update(dset, "IncomeGroups");
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

