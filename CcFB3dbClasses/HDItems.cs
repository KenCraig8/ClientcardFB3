using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Linq;
using System.Text;

namespace ClientcardFB3
{
    public class HDItems : IDisposable
    {
        string connString;
        SqlDataAdapter dadAdpt;
        DataRow drow;
        DataTable dtbl;
        SqlCommand command;
        SqlCommandBuilder commBuilder;
        SqlConnection conn;
        bool bisValid = false;
        int iRowCount = 0;
        private bool _disposed;
    
        public HDItems(string connStringIn)
        {
            conn = new SqlConnection();
            connString = connStringIn;
            conn.ConnectionString = connString;
            dtbl = new DataTable();
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
                    if (dtbl != null)
                        dtbl.Dispose();
                    if (command != null)
                        command.Dispose();
                    if (commBuilder != null)
                        commBuilder.Dispose();
                    if (dadAdpt != null)
                        dadAdpt.Dispose();
                }

                // Indicate that the instance has been disposed.
                conn = null;
                dtbl = null;
                command = null;
                dadAdpt = null;
                _disposed = true;
            }
        }

        #region Get/Set Accessors
        public DataTable DTable
        {
            get { return dtbl; }
        }
        public bool ISValid
        {
            get { return bisValid; }
        }
        public int RowCount
        {
            get { return iRowCount; }
        }

        public int ID
        {
            get { return Convert.ToInt32(drow["ID"]); }
            set { drow["ID"] = value; }
        }
        public string Description
        {
            get { return drow["Description"].ToString(); }
            set { drow["Description"] = value; }
        }
        public string ShortName
        {
            get { return drow["ShortName"].ToString(); }
            set { drow["ShortName"] = value; }
        }
        public int LbsStd
        {
            get { return Convert.ToInt32(drow["LbsStd"]); }
            set { drow["LbsStd"] = value; }
        }
        public int LbsOther
        {
            get { return Convert.ToInt32(drow["LbsOther"]); }
            set { drow["LbsOther"] = value; }
        }
        public int LbsCommodity
        {
            get { return Convert.ToInt32(drow["LbsCommodity"]); }
            set { drow["LbsCommodity"] = value; }
        }
        public int LbsSupplemental
        {
            get { return Convert.ToInt32(drow["LbsSupplemental"]); }
            set { drow["LbsSupplemental"] = value; }
        }
        public int LbsNonFood
        {
            get { return Convert.ToInt32(drow["LbsNonFood"]); }
            set { drow["LbsNonFood"] = value; }
        }
        public int LbsBabySvc
        {
            get { return Convert.ToInt32(drow["LbsBabySvc"]); }
            set { drow["LbsBabySvc"] = value; }
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
            if (dtbl.Rows.Count > 0)
            {
                int fldIndex = dtbl.Columns.IndexOf(FieldName);
                if (fldIndex >= 0)
                {
                    if (dtbl.Columns[fldIndex].DataType.Name == "DateTime")
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
                if (ID == dtbl.Rows[i].Field<int>("ID"))
                {
                    drow = dtbl.Rows[i];
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
            if (dtbl.Rows.Count >= rowIndex)
            {
                drow = dtbl.Rows[rowIndex];
                return drow;
            }
            return null;
        }

        public void open(System.Int32 key)
        {
            try
            {
                openConnection();
                command = new SqlCommand("SELECT * FROM HDItems WHERE ID=" + key.ToString(), conn);
                dadAdpt.SelectCommand = command;
                dtbl.Clear();
                iRowCount = dadAdpt.Fill(dtbl);
                closeConnection();
                if (iRowCount > 0)
                    drow = dtbl.Rows[0];
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
                command = new SqlCommand("SELECT * FROM HDItems " + whereClause, conn);
                dadAdpt.SelectCommand = command;
                dtbl.Clear();
                iRowCount = dadAdpt.Fill(dtbl);
                closeConnection();
                if (iRowCount > 0)
                    drow = dtbl.Rows[0];
            }
            catch (SqlException ex)
            {
                iRowCount = 0;
                drow = null;
                CCFBGlobal.appendErrorToErrorReport("CommandText = " + command.CommandText,
                    ex.GetBaseException().ToString());
            }
        }

        public bool delete(int key)
        {
            SqlCommand cmdDelete = new SqlCommand(" DELETE FROM HDItems WHERE Id=" + key.ToString(), conn);
            try
            {
                openConnection();
                int iRows = cmdDelete.ExecuteNonQuery();
                closeConnection();
                return (iRows == 1);
            }
            catch (SqlException ex)
            {
                CCFBGlobal.appendErrorToErrorReport("Delete Command = " + cmdDelete.CommandText,
                    ex.GetBaseException().ToString());
                return false;
            }
            cmdDelete.Dispose();
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

        public void update()
        {
            try
            {
                openConnection();
                if (dadAdpt.UpdateCommand == null)
                {
                    commBuilder = new SqlCommandBuilder(dadAdpt);
                }
                dadAdpt.Update(dtbl);
                conn.Close();
            }
            catch (SqlException ex)
            {
                CCFBGlobal.appendErrorToErrorReport("Update HDItems FAILED ",
                    ex.GetBaseException().ToString());
            }
        }
    }
}
