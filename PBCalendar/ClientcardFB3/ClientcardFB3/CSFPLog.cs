
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

namespace ClientcardFB3
{
    public class CSFPLog
    {
        string connString;
        SqlDataAdapter dadAdpt;
        DataSet dset;
        SqlCommand command;
        System.Data.SqlClient.SqlConnection conn;
        static string tbName = "CSFPLog";
        int iRowCount = 0;
        DataRow drow = null;
        bool isValid = false;

        public CSFPLog(string connStringIn)
        {
            conn = new System.Data.SqlClient.SqlConnection();
            connString = connStringIn;
            conn.ConnectionString = connString;
            dset = new DataSet();
            dadAdpt = new SqlDataAdapter();
        }

        #region Get/Set Accessors

        public DataSet DSet
        {
            get { return dset; }
            set { dset = value; }
        }

        public bool IsValid
        {
            get { return isValid; }
            set { isValid = value; }
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
        
        public DateTime CSFPSvcDate
        {
            get{ return (DateTime)drow["CSFPDate"];}
            set { drow["CSFPDate"] = value; }
        }

        public int MemID
        {
            get { return Convert.ToInt32(drow["MemID"]); }
            set { drow["MemID"] = value; }
        }

        public int Lbs
        {
            get { return Convert.ToInt32(drow["Lbs"]); }
            set { drow["Lbs"] = value; }
        }

        public int DistributionMethod
        {
            get { return Convert.ToInt32(drow["DistributionMethod"]); }
            set { drow["DistributionMethod"] = value; }
        }
        #endregion

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
                return isValid = false;
            }
            catch (SqlException ex)
            {
                CCFBGlobal.appendErrorToErrorReport("ID=" + ID.ToString(), 
                    ex.GetBaseException().ToString(), CCFBGlobal.serverName);
                closeConnection();
                return isValid = false;
            }
        }

        public void openWhere(string whereClause)
        {
            try
            {
                openConnection();
                command = new SqlCommand("SELECT * FROM " + tbName 
                    + " " + whereClause, conn);
                dadAdpt.SelectCommand = command;
                dset.Clear();
                iRowCount = dadAdpt.Fill(dset, tbName);
                closeConnection();
               
                if (iRowCount > 0)
                    drow = dset.Tables[tbName].Rows[0];

                isValid = false;
            }
            catch (SqlException ex)
            {
                CCFBGlobal.appendErrorToErrorReport("WhereClause="+whereClause, ex.GetBaseException().ToString(), CCFBGlobal.serverName);
                closeConnection();
                isValid = false;
            }
        }

        public void delete(DateTime CSFPSvcDate, int HHMemID)
        {
            openConnection();
            SqlCommand commDelete = new SqlCommand(" DELETE FROM CSFPLog WHERE Date=" + CSFPSvcDate.ToString()
                + " And MemID=" + HHMemID.ToString(), conn);
            commDelete.ExecuteNonQuery();
            closeConnection();
        }

        public bool insert()
        {
            try
            {
                if (dadAdpt.InsertCommand == null || dadAdpt.UpdateCommand == null)
                {
                    SqlCommandBuilder commBuilder = new SqlCommandBuilder(dadAdpt);
                }

                openConnection();
                dadAdpt.Update(dset, tbName);
                closeConnection();
                return true;
            }
            catch (SqlException ex)
            {
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString(),
                    CCFBGlobal.serverName);
                closeConnection(); return false;
            }
        }

        public bool update()
        {
            if (dset.HasChanges() == true)
            {
                try
                {
                    openConnection();

                    if (dadAdpt.UpdateCommand == null)
                    {
                        //Sets the Commands in the DataAdapter
                        SqlCommandBuilder commBuilder = new SqlCommandBuilder(dadAdpt);
                    }

                    drow["ModifiedBy"] = CCFBGlobal.currentUser_Name;
                    drow["Modified"] = DateTime.Now;

                    dadAdpt.Update(dset, "HouseholdMembers");
                    closeConnection();
                    return true;
                }
                catch (SqlException ex)
                {
                    CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString(),
                    CCFBGlobal.serverName);
                    return false;
                }
            }
            return false;
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

