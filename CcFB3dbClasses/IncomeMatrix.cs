
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

namespace ClientcardFB3
{
    public class IncomeMatrix : IDisposable
    {
        SqlDataAdapter dadAdpt;
        DataSet dset;
        DataRow dRow;
        SqlCommand command;
        SqlCommandBuilder commBuilder;
        System.Data.SqlClient.SqlConnection conn;
        static string tbName = "IncomeMatrix";
        int rowIndexCurrent = -1;
        int iRowCount;
        private bool _disposed;

        public IncomeMatrix()
        {
            conn = new System.Data.SqlClient.SqlConnection();
            conn.ConnectionString = CCFBGlobal.connectionString;
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
                commBuilder = null;
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
        }
        public int ID
        {
            get { return Convert.ToInt32(dRow["ID"]); }
            set { dRow["ID"] = value; }
        }
        public int IncomeGroup
        {
            get { return Convert.ToInt32(dRow["IncomeGroup"]); }
            set { dRow["IncomeGroup"] = value; }
        }
        public string Label1
        {
            get { return dRow["Label1"].ToString(); }
            set { dRow["Label1"] = value; }
        }
        public string Label2
        {
            get { return dRow["Label2"].ToString(); }
            set { dRow["Label2"] = value; }
        }
        public string Label3
        {
            get { return dRow["Label3"].ToString(); }
            set { dRow["Label3"] = value; }
        }
        public int IncomeLow1
        {
            get { return Convert.ToInt32(dRow["IncomeLow1"]); }
            set { dRow["IncomeLow1"] = value; }
        }
        public int IncomeLow2
        {
            get { return Convert.ToInt32(dRow["IncomeLow2"]); }
            set { dRow["IncomeLow2"] = value; }
        }
        public int IncomeLow3
        {
            get { return Convert.ToInt32(dRow["IncomeLow3"]); }
            set { dRow["IncomeLow3"] = value; }
        }
        public int IncomeLow4
        {
            get { return Convert.ToInt32(dRow["IncomeLow4"]); }
            set { dRow["IncomeLow4"] = value; }
        }
        public int IncomeLow5
        {
            get { return Convert.ToInt32(dRow["IncomeLow5"]); }
            set { dRow["IncomeLow5"] = value; }
        }
        public int IncomeLow6
        {
            get { return Convert.ToInt32(dRow["IncomeLow6"]); }
            set { dRow["IncomeLow6"] = value; }
        }
        public int IncomeLow7
        {
            get { return Convert.ToInt32(dRow["IncomeLow7"]); }
            set { dRow["IncomeLow7"] = value; }
        }
        public int IncomeLow8
        {
            get { return Convert.ToInt32(dRow["IncomeLow8"]); }
            set { dRow["IncomeLow8"] = value; }
        }
        public int IncomeLow9
        {
            get { return Convert.ToInt32(dRow["IncomeLow9"]); }
            set { dRow["IncomeLow9"] = value; }
        }
        public int IncomeLow10
        {
            get { return Convert.ToInt32(dRow["IncomeLow10"]); }
            set { dRow["IncomeLow10"] = value; }
        }
        public int IncomeHi1
        {
            get { return Convert.ToInt32(dRow["IncomeHi1"]); }
            set { dRow["IncomeHi1"] = value; }
        }
        public int IncomeHi2
        {
            get { return Convert.ToInt32(dRow["IncomeHi2"]); }
            set { dRow["IncomeHi2"] = value; }
        }
        public int IncomeHi3
        {
            get { return Convert.ToInt32(dRow["IncomeHi3"]); }
            set { dRow["IncomeHi3"] = value; }
        }
        public int IncomeHi4
        {
            get { return Convert.ToInt32(dRow["IncomeHi4"]); }
            set { dRow["IncomeHi4"] = value; }
        }
        public int IncomeHi5
        {
            get { return Convert.ToInt32(dRow["IncomeHi5"]); }
            set { dRow["IncomeHi5"] = value; }
        }
        public int IncomeHi6
        {
            get { return Convert.ToInt32(dRow["IncomeHi6"]); }
            set { dRow["IncomeHi6"] = value; }
        }
        public int IncomeHi7
        {
            get { return Convert.ToInt32(dRow["IncomeHi7"]); }
            set { dRow["IncomeHi7"] = value; }
        }
        public int IncomeHi8
        {
            get { return Convert.ToInt32(dRow["IncomeHi8"]); }
            set { dRow["IncomeHi8"] = value; }
        }
        public int IncomeHi9
        {
            get { return Convert.ToInt32(dRow["IncomeHi9"]); }
            set { dRow["IncomeHi9"] = value; }
        }
        public int IncomeHi10
        {
            get { return Convert.ToInt32(dRow["IncomeHi10"]); }
            set { dRow["IncomeHi10"] = value; }
        }
        public DateTime Created
        {
            get { return (DateTime)dRow["Created"]; }
            set { dRow["Created"] = value; }
        }
        public string CreatedBy
        {
            get { return dRow["CreatedBy"].ToString(); }
            set { dRow["CreatedBy"] = value; }
        }
        public DateTime Modified
        {
            get 
            {
                if (String.IsNullOrEmpty(dRow["Modified"].ToString())  == true)
                    return CCFBGlobal.FBNullDateValue;
                else
                    return (DateTime)dRow["Modified"];
            }
            set { dRow["Modified"] = value; }
        }
        public string ModifiedBy
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

        public void setDataValue(string dataField, string dataValue)
        {
            dRow[dataField] = dataValue;
        }

        public object getDataValue(string dataField)
        {
            return dRow[dataField];
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
                CCFBGlobal.appendErrorToErrorReport("WhereClause=" + whereClause, ex.GetBaseException().ToString());
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
                if (ID == dset.Tables[tbName].Rows[i].Field<int>("ID"))
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
            if (dset.Tables[tbName].Rows.Count > 0)
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
            SqlCommand commDelete = new SqlCommand(" DELETE FROM " + tbName + " WHERE ID=" + key.ToString(), conn);
            openConnection();
            commDelete.BeginExecuteNonQuery();
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

                    dadAdpt.Update(dset, tbName);
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

