
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

namespace ClientcardFB3
{
    public class FBJobsPlan : IDisposable
    {
        string connString;
        SqlDataAdapter dadAdpt;
        DataRow drow;
        DataSet dset;
        SqlCommand command;
        int iRowCount = 0;
        bool isValid = false;
        System.Data.SqlClient.SqlConnection conn;
        static string tblName = "FBJobsPlan";
        private bool _disposed;

        public FBJobsPlan(string connectstring)
        {
            conn = new System.Data.SqlClient.SqlConnection();
            connString = connectstring;
            conn.ConnectionString = connectstring;
            dset = new DataSet();
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
        public int RowCount
        {
            get { return iRowCount; }
        }
        public int PlanID
        {
            get { return Convert.ToInt32(drow["PlanID"]); }
            set { drow["PlanID"] = value; }
        }
        public int WeekDay
        {
            get { return Convert.ToInt32(drow["WeekDay"]); }
            set { drow["WeekDay"] = value; }
        }
        public int JobID
        {
            get { return Convert.ToInt32(drow["JobID"]); }
            set { drow["JobID"] = value; }
        }
        public string JobTitle
        {
            get { return drow["JobTitle"].ToString(); }
            set { drow["JobTitle"] = value; }
        }
        public string ShiftStart
        {
            get { return drow["ShiftStart"].ToString(); }
            set { drow["ShiftStart"] = value; }
        }
        public string ShiftEnd
        {
            get { return drow["ShiftEnd"].ToString(); }
            set { drow["ShiftEnd"] = value; }
        }
        public int VolIdPrimary
        {
            get { return Convert.ToInt32(drow["VolIdPrimary"]); }
            set { drow["VolIdPrimary"] = value; }
        }
        public int VolIdBackup
        {
            get { return Convert.ToInt32(drow["VolIdBackup"]); }
            set { drow["VolIdBackup"] = value; }
        }
        public DateTime Created
        {
            get { return (DateTime)drow["Created"]; }
            set { drow["Created"] = value; }
        }
        public string CreatedBy
        {
            get { return drow["CreatedBy"].ToString(); }
            set { drow["CreatedBy"] = value; }
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
            set { drow["Modified"] = value; }
        }
        public string ModifiedBy
        {
            get { return drow["ModifiedBy"].ToString(); }
            set { drow["ModifiedBy"] = value; }
        }
        public string PrimaryName
        {
            get { return drow["PrimaryName"].ToString(); }
        }
        public string BackupName
        {
            get { return drow["BackupName"].ToString(); }
        }
        #endregion Get/Set Accessors


        public bool open(int uid)
        {
            try
            {
                command = new SqlCommand("SELECT * FROM " + tblName + " WHERE PlanID=" + uid.ToString(), conn);
                dadAdpt = new SqlDataAdapter(command);
                dadAdpt.SelectCommand = command;
                dset.Clear();
                iRowCount =dadAdpt.Fill(dset, tblName);
                if (iRowCount > 0)
                {
                    drow = dset.Tables[tblName].Rows[0];
                    return isValid = true;
                }
            }
            catch (SqlException ex) { }
            return isValid = false;
        }

        public void openForWeekDay(int weekday)
        {
            string sql = "SELECT jp.*, vp.Name PrimaryName, vb.Name BackupName "
                       + "  FROM FBJobsPlan jp "
                       + "  LEFT JOIN Volunteers vp ON jp.VolIDPrimary = vp.ID "
                       + "  LEFT JOIN Volunteers vb ON jp.VolIDBackup = vb.ID "
                       + " WHERE jp.WeekDay =" + weekday.ToString()
                       + " ORDER BY jp.JobTitle, jp.ShiftStart";
            try
            {
                command = new SqlCommand(sql, conn);
                if (dadAdpt == null)
                    dadAdpt = new SqlDataAdapter(command);
                else
                    dadAdpt.SelectCommand = command;
                dset.Clear();
                iRowCount = dadAdpt.Fill(dset, tblName);
                if (iRowCount > 0)
                    drow = dset.Tables[tblName].Rows[0];
            }
            catch (SqlException ex)
            {
                CCFBGlobal.appendErrorToErrorReport("sql=" + sql,
                    ex.GetBaseException().ToString());
                closeConnection();
                iRowCount = 0;
            }
        }

        /// <summary>
        /// Sets the DataRow of HH Members to be the row index passed in
        /// </summary>
        /// <param name="rowIndex">The row index in the dataset</param>
        public void SetRecord(int rowIndex)
        {
            if (rowIndex >= 0 && rowIndex < iRowCount)
            {
                drow = dset.Tables[tblName].Rows[rowIndex];
            }
        }

        public void delete(int ID)
        {
            try
            {
                openConnection();
                SqlCommand commDelete = new SqlCommand("DELETE FROM " + tblName + " WHERE PlanID=" + ID.ToString(), conn);
                commDelete.ExecuteNonQuery();
                commDelete.Dispose();
                closeConnection();
            }
            catch (SqlException ex)
            {
                CCFBGlobal.appendErrorToErrorReport("ID=" + ID.ToString(),
                    ex.GetBaseException().ToString());
                closeConnection();
            }
        }

        public int insert(int weekday, int jobid, string jobtitle, string shiftstart, string shiftend, int volidprimary, int volidbackup)
        {
            string sql = "INSERT INTO [FBJobsPlan] "
                       + "([WeekDay],[JobID],[JobTitle],[ShiftStart],[ShiftEnd]"
                       + ",[VolIdPrimary],[VolIdBackup]"
                       + ",[Created],[CreatedBy],[Modified],[ModifiedBy]) "
                       + "VALUES (" + weekday.ToString() + "," + jobid.ToString() + ",'" + jobtitle + "'"
                       + ",'" + shiftstart + "','" + shiftend + "'"
                       + "," + volidprimary.ToString() + "," + volidbackup.ToString()
                       + ",'" + DateTime.Now + "','" + CCFBGlobal.dbUserName + "'"
                       + ",NULL,'')";
            return CCFBGlobal.executeQuery(sql);
        }

        public int insert(int weekday, string jobid, string jobtitle, string shiftstart, string shiftend, string volidprimary, string volidbackup)
        {
            string sql = "INSERT INTO [FBJobsPlan] "
                       + "([WeekDay],[JobID],[JobTitle],[ShiftStart],[ShiftEnd]"
                       + ",[VolIdPrimary],[VolIdBackup]"
                       + ",[Created],[CreatedBy],[Modified],[ModifiedBy]) "
                       + "VALUES (" + weekday.ToString() + "," + jobid + ",'" + jobtitle + "'"
                       + ",'" + shiftstart + "','" + shiftend + "'"
                       + "," + volidprimary + "," + volidbackup
                       + ",'" + DateTime.Now + "','" + CCFBGlobal.dbUserName + "'"
                       + ",NULL,'')";
            return CCFBGlobal.executeQuery(sql);
        }
        public void update()
        {
            if (dset.HasChanges() == true)
            {
                try
                {
                    if (dadAdpt.UpdateCommand == null || dadAdpt.InsertCommand == null)
                    {
                        SqlCommandBuilder commBuild = new SqlCommandBuilder(dadAdpt);
                    }

                    openConnection();
                    dadAdpt.Update(dset, tblName);
                    closeConnection();
                }
                catch (SqlException ex)
                {
                    closeConnection();
                    CCFBGlobal.appendErrorToErrorReport("FBJobsPlan.Update", ex.GetBaseException().ToString());
                }
            }
        }

        public void updateField(string id, string fldname, string datavalue)
        {
            CCFBGlobal.executeQuery("UPDATE " + tblName + " SET " + fldname + " = '" + datavalue + "' WHERE PlanID = " + id);
        }

        public void updateField(string id, string fldname, int datavalue)
        {
            CCFBGlobal.executeQuery("UPDATE " + tblName + " SET " + fldname + " = " + datavalue.ToString() + " WHERE PlanID = " + id);
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

