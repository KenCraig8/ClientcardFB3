
using System;
using System.Data;
using System.Data.SqlClient;

namespace ClientcardFB3
{
    public class HDRSHist

    {
        string connString;
        SqlDataAdapter dadAdptHist;
        DataTable dtblHist;
        SqlCommand command;
        System.Data.SqlClient.SqlConnection conn;
        DataRow drowHist;
        int iHistRowCount = 0;

        public HDRSHist(string connStringIn)
        {
            conn = new System.Data.SqlClient.SqlConnection();
            connString = connStringIn;
            conn.ConnectionString = connString;
            dtblHist = new DataTable();
            dadAdptHist = new SqlDataAdapter();
        }

        #region Get/Set Accessors
        #endregion Get/Set Accessors
        
        #region History Get/Set Accessors
        public DataTable DTableHistory
        {
            get { return dtblHist; }
        }
        public int RowCountHist
        {
            get { return iHistRowCount; }
        }
        public DateTime DelivDate
        {
            get { return (DateTime)drowHist["DelivDate"]; }
        }
        public int NbrPrepared
        {
            get { return Convert.ToInt32(drowHist["NbrPrepared"]); }
        }
        public int NbrPrinted
        {
            get { return Convert.ToInt32(drowHist["NbrPrinted"]); }
        }
        public int NbrPosted
        {
            get { return Convert.ToInt32(drowHist["NbrPosted"]); }
        }
        #endregion History Get/Set Accessors

        /// <summary>
        /// Sets the History DataRow for the given RowIndex
        /// </summary>
        /// <param name="rowIndex"></param>
        public DataRow setHistDataRow(int rowIndex)
        {
            if (dtblHist.Rows.Count >= rowIndex)
            {
                drowHist = dtblHist.Rows[rowIndex];
                return drowHist;
            }
            return null;
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

        public void openHistory(int idxPeriod)
        {
            DateTime dateFrom = Convert.ToDateTime("1/1/1900");
            DateTime dateTo = Convert.ToDateTime("12/31/2099");
            switch (idxPeriod)
            {
                case 0: //4 Weeks
                    dateFrom = DateTime.Today.AddDays(-21);
                    dateTo   = DateTime.Today.AddDays(7);
                    break;
                case 1: //Last Month
                    dateTo   = DateTime.Today.AddDays(-DateTime.Today.Day);
                    dateFrom = dateTo.AddMonths(-1).AddDays(1);
                    break;
                case 2: //Next Month
                    dateFrom = DateTime.Today.AddMonths(1).AddDays(-DateTime.Today.Day+1);
                    dateTo   = dateFrom.AddMonths(1).AddDays(-1);
                    break;
                case 3: //This Month
                    dateFrom = DateTime.Today.AddDays(-DateTime.Today.Day+1);
                    dateTo   = dateFrom.AddMonths(1).AddDays(-1);
                    break;
                case 4: //This Year
                    dateFrom = Convert.ToDateTime("01/01/" + DateTime.Today.Year.ToString());
                    dateTo   = Convert.ToDateTime("12/31/" + DateTime.Today.Year.ToString());
                    break;
                case 5: //Last Year
                    dateFrom = Convert.ToDateTime("01/01/" + (DateTime.Today.Year - 1).ToString());
                    dateTo   = Convert.ToDateTime("12/31/" + (DateTime.Today.Year - 1).ToString());
                    break;
                default:
                    break;
            }
            try
            {
                openConnection();
                command = new SqlCommand("SELECT * FROM RouteSheetHistory WHERE DelivDate BETWEEN '" + dateFrom.ToShortDateString() + "' AND '" + dateTo.ToShortDateString() + "' ORDER BY DelivDate DESC", conn);
                dadAdptHist.SelectCommand = command;
                dtblHist.Clear();
                iHistRowCount = dadAdptHist.Fill(dtblHist);
                closeConnection();
                if (iHistRowCount > 0)
                    drowHist = dtblHist.Rows[0];
            }
            catch (SqlException ex)
            {
                iHistRowCount = 0;
                drowHist = null;
                CCFBGlobal.appendErrorToErrorReport("Select Command = " + command.CommandText,
                    ex.GetBaseException().ToString());
            }
        }
    }
}

