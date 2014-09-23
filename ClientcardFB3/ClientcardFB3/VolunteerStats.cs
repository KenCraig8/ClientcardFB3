using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;


namespace ClientcardFB3
{
    class VolunteerStats
    {
        string connString;
        DataTable dtable;
        System.Data.SqlClient.SqlConnection conn;
        DataRow drow;
        DataRow drowYTD;
        int iRowCount = 0;
        int iCurrentRow = 0;
        const string constKeyName = "FiscalPeriod";

        public VolunteerStats(string connStringIn)
        {
            conn = new System.Data.SqlClient.SqlConnection();
            conn.ConnectionString = connString = connStringIn;
            dtable = new DataTable();
        }

        #region Get/Set Accessors
        public int CurrentRow
        {
            get { return iCurrentRow; }
        }
        public int RowCount
        {
            get { return iRowCount; }
        }
        public DateTime TrxDate
        {
            get { return Convert.ToDateTime("TrxDate"); }
            set { drow["TrxDate"] = value; }
        }
        public string FiscalPeriod
        {
            get { return drow["FiscalPeriod"].ToString(); }
            set { drow["FiscalPeriod"] = value; }
        }
        public string YearMonth
        {
            get { return drow["YearMonth"].ToString(); }
            set { drow["YearMonth"] = value; }
        }
        public int NumVols
        {
            get 
            {
                try
                {
                    return Convert.ToInt32(drow["NbrVolunteers"]);
                }
                catch (Exception)
                {
                    return 0;
                }
            }
            set { drow["NbrVolunteers"] = value; }
        }
        public Single NumVolHours
        {
            get 
            {
                try
                {
                    return Convert.ToSingle(drow["NbrVolHours"]);
                }
                catch (Exception)
                {
                    return 0;
                }
            }
            set { drow["NbrVolHours"] = value; }
        }
        #endregion Get/Set Accessors
        #region Data Value Accsessors

        public string dollarsInkindHours(string sfiscalperiod)
        {
            if (FiscalPeriod != sfiscalperiod)
            {
                if (findFiscalPeriod(sfiscalperiod) == true)
                {
                    return (Convert.ToDecimal(NumVolHours) * CCFBPrefs.InkindDollarsPerHr).ToString("C0");
                }
            }
            else
            {
                return (Convert.ToDecimal(NumVolHours) * CCFBPrefs.InkindDollarsPerHr).ToString("C0");
            }
            return "";
        }

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
            try
            {
                return drow[FieldName];
            }
            catch
            {
                return "0";
            }
        }


        //Gets property through use of just the column name in database as string
        public object GetDataString(string FieldName)
        {
            if (dtable.Rows.Count > 0)
            {
                int fldIndex = dtable.Columns.IndexOf(FieldName);
                if (fldIndex >= 0)
                {
                    if (dtable.Columns[fldIndex].DataType.Name == "DateTime")
                        if (drow[FieldName].ToString() != "")
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

        /// <summary>
        /// Sets the DataRow for the given RowIndex
        /// </summary>
        /// <param name='rowIndex'></param>
        public DataRow setDataRow(int rowindex)
        {
            if (rowindex < iRowCount)
            {
                drow = dtable.Rows[rowindex];
                iCurrentRow = rowindex;
                return drow;
            }
            return null;
        }

        public Boolean findFiscalPeriod(string key)
        {
            for (int i = 0; i < iRowCount; i++)
            {
                if (dtable.Rows[i]["FiscalPeriod"].ToString() == key)
                {
                    drow = dtable.Rows[i];
                    iCurrentRow = i;
                    return true;
                }
            }
            return false;
        }

        public Boolean findFiscalPeriod(int period)
        {
            string key = CCFBGlobal.formatNumberWithLeadingZero(period);
            for (int i = 0; i < iRowCount; i++)
            {
                if (dtable.Rows[i]["FiscalPeriod"].ToString().EndsWith(key) == true)
                {
                    drow = dtable.Rows[i];
                    iCurrentRow = i;
                    return true;
                }
            }
            return false;
        }

        public Boolean findYearMonth(string key)
        {
            for (int i = 0; i < iRowCount; i++)
            {
                if (dtable.Rows[i]["YearMonth"].ToString() == key)
                {
                    drow = dtable.Rows[i];
                    iCurrentRow = i;
                    return true;
                }
            }
            return false;
        }

        public void open(string startDate, string endDate)
        {
            dtable = new DataTable();
            SqlCommand sqlCmd = new SqlCommand("VolunteerStats", conn);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add(new SqlParameter("@StartDate", startDate));
            sqlCmd.Parameters.Add(new SqlParameter("@EndDate", endDate));
            iRowCount = TransferDataToLocalDataTable(sqlCmd, dtable);
            if (iRowCount > 0)
            {
                drow = dtable.Rows[0];
                drowYTD = dtable.Rows[iRowCount - 1];
            }
            iCurrentRow = 0;
        }

        public void setYTDRow()
        {
            drow = drowYTD;
        }

        private int TransferDataToLocalDataTable(SqlCommand sqlCmd, DataTable dt)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();        // Open the connection and execute the reader.

                SqlDataReader reader = sqlCmd.ExecuteReader();

                if (reader.HasRows)
                {
                    DataTable readerSchema = reader.GetSchemaTable().Copy();
                    for (int i = 0; i < readerSchema.Rows.Count; i++)
                    {
                        dt.Columns.Add(readerSchema.Rows[i]["ColumnName"].ToString());
                    }
                    while (reader.Read())
                    {
                        object[] values = new object[reader.FieldCount];
                        for (int i = 0; i < values.Length; i++)
                        {
                            if (values[i] == DBNull.Value)
                                values[i] = 0;
                        }
                        reader.GetValues(values);
                        dt.Rows.Add(values);
                    }
                }
            }
            catch { };

            if (conn.State == ConnectionState.Open)
                conn.Close();

            return dt.Rows.Count;
        }
    }
}
