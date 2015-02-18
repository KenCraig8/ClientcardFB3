using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Data;

namespace ClientcardFB3
{
    public class DistinctLogDays : IDisposable
    {
        static string tbName = "TrxLog";
        int apptStatus;
        DataSet dset;
        DataRow[] dRow;
        int rowCount;
        int CurRowNumber;
        System.Data.SqlClient.SqlConnection conn;
        string connString;
        private bool _disposed;
        //bool isValid;

        public DistinctLogDays(string connectString)
        {
            dset = new DataSet();
            apptStatus = 0;
            rowCount = 0;
            CurRowNumber = 0;
            connString = connectString;
            conn = new System.Data.SqlClient.SqlConnection(connString);
          //  isValid = false;
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
                }

                // Indicate that the instance has been disposed.
                conn = null;
                dset = null;
                _disposed = true;
            }
        }
        #region GET/SET ACCESSORS

        public int ApptStatus
        {
            get { return apptStatus; }
            set { apptStatus = value; }
        }

        public int RowCount
        {
            get { return rowCount; }
        }

        public int RowIndex
        {
            get { return CurRowNumber; }
        }

        public DataSet DSet
        {
            get { return dset; }
            set { dset = value; }
        }

        public int TrxId
        {
            get { return dset.Tables[tbName].Rows[CurRowNumber].Field<int> ("TrxId"); }
            set { dset.Tables[tbName].Rows[CurRowNumber]["TrxId"] = value; }
        }

        public DateTime TrxDate
        {
            get { return dset.Tables[tbName].Rows[CurRowNumber].Field<DateTime>("TrxDate"); }
            set { dset.Tables[tbName].Rows[CurRowNumber]["TrxDate"] = value; }
        }

        #endregion

        public int FindDate(DateTime dateTest)
        {
            CurRowNumber = -1;
            if (rowCount > 0)
            {
                dRow = dset.Tables[tbName].Select(" TrxDate = '" + dateTest.ToShortDateString() + "'");
                int idx = 0;
                for (idx = 0; idx < rowCount; idx++)
                {
                    if (dset.Tables[tbName].Rows[idx].Field<DateTime>("TrxDate") == dateTest)
                    {
                        CurRowNumber = idx;
                        break;
                    }
                }
            }
            else { }
            //{ dRow = new DataRow; }
            return dRow.Length;
        }

        public int LoadDateList(int ApptmntStatus)
        {
            apptStatus = ApptmntStatus;
            string StatusList = "";
            SqlCommand command;
            SqlDataAdapter dataAdpt;
            rowCount = 0;
            if (apptStatus == 0)
                StatusList = "0,1";
            else
                StatusList = "2,3";

            try
            {

                command = new SqlCommand("SELECT DISTINCT TrxDate, Count(*)  FROM TrxLog WHERE TrxStatus IN ("
                    + StatusList + ") GROUP BY TrxDate ORDER BY TrxDate");
                dataAdpt = new SqlDataAdapter(command);
                dataAdpt.SelectCommand.Connection = conn;
                dset.Clear();
                rowCount = dataAdpt.Fill(dset, tbName);
                CurRowNumber = 0;
                return rowCount;
            }
            catch (SqlException ex)
            {
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString());
                return rowCount;
            }
            command.Dispose();
            dataAdpt.Dispose();
        }

        public String  FirstDate()
        {
            if (rowCount > 0)
            {
                CurRowNumber = 0;
                return TrxDate.ToShortDateString();
            }
            else
            { return ""; }
        }

        public String LastDate()
        {
            if (rowCount > 0)
            {
                CurRowNumber = rowCount - 1;
                return TrxDate.ToShortDateString();
            }
            else
            { return ""; }
        }

        public String NextDate()
        {
            if (rowCount > 0)
            {
                if (CurRowNumber + 1 < rowCount)
                { CurRowNumber += 1; }
                return TrxDate.ToShortDateString();
            }
            else
            { return ""; }
        }

        public String PrevDate()
        {
            if (rowCount > 0)
            {
                if (CurRowNumber - 1 >= 0)
                { CurRowNumber -= 1; }
                return TrxDate.ToShortDateString();
            }
            else
            { return ""; }
        }

        public bool HaveNextDate()
        {
            if (CurRowNumber + 1 < rowCount)
            { return true; }
            return false;
        }

        public bool HavePrevDate()
        {
            if (CurRowNumber - 1 >= 0)
            { return true; }
            return false;
        }

    }
}
