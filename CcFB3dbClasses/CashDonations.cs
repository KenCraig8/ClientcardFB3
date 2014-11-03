using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

namespace ClientcardFB3
{
    public class CashDonations
    {
        SqlDataAdapter dadAdpt;
        DataSet dset;
        SqlCommand command;
        System.Data.SqlClient.SqlConnection conn;
        static string tbName = "CashDonations";
        int iRowCount = 0;
        bool isValid = false;
        DataRow dRow = null;

        public enum datefieldselection { Created = 0, TrxDate = 1 }

        public CashDonations(string connString)
        {
            conn = new System.Data.SqlClient.SqlConnection();
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
        public int TrxID
        {
            get
            {
                return dRow.Field<int>("TrxID");
            }
            set
            {
                dRow["TrxID"] = value;
            }
        }

        public int DonorID
        {
            get
            {
                return dRow.Field<int>("DonorID");
            }
            set
            {
                dRow["DonorID"] = value;
            }
        }

        public DateTime TrxDate
        {
            get
            {
                return dRow.Field<DateTime>("TrxDate");
            }
            set
            {
                dRow["TrxDate"] = value;
            }
        }

        public decimal DollarValue
        {
            get
            {
                return dRow.Field<Decimal>("DollarValue");
            }
            set
            {
                dRow["DollarValue"] = value;
            }
        }

        public string Notes
        {
            get
            {
                return dRow.Field<System.String>("Notes");
            }
            set
            {
                dRow["Notes"] = value;
            }
        }

        public DateTime Created
        {
            get
            {
                return dRow.Field<DateTime>("Created");
            }
            set
            {
                dRow["Created"] = value;
            }
        }

        public string CreatedBy
        {
            get
            {
                return dRow.Field<System.String>("CreatedBy");
            }
            set
            {
                dRow["CreatedBy"] = value;
            }
        }

        public DateTime Modified
        {
            get
            {
                return dRow.Field<DateTime>("Modified");
            }
            set
            {
                dRow["Modified"] = value;
            }
        }

        public string ModifiedBy
        {
            get
            {
                return dRow.Field<System.String>("ModifiedBy");
            }
            set
            {
                dRow["ModifiedBy"] = value;
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

        /// <summary>
        /// Sets the DataRow that the get/set accessors work off of using a Row Index
        /// </summary>
        /// <param name="rowIndex">The row index to set the drow to</param>
        public void setDataRow(int rowIndex)
        {
            if (rowIndex < iRowCount && rowIndex >= 0)
                dRow = dset.Tables[tbName].Rows[rowIndex];
        }

        public void find(int TrxID)
        {
            for (int i = 0; i < iRowCount; i++)
            {
                if (TrxID == dset.Tables[tbName].Rows[i].Field<int>("TrxID"))
                {
                    dRow = dset.Tables[tbName].Rows[i];
                    return;
                }
            }
        }

        public void open(System.Int32 key)
        {
            try
            {
                openConnection();
                command = new SqlCommand("SELECT * FROM " + tbName + " WHERE TrxID=" + key.ToString(), conn);
                dadAdpt.SelectCommand = command;
                dset.Clear();
                iRowCount = dadAdpt.Fill(dset, tbName);
                closeConnection();
                if (iRowCount > 0)
                    dRow = dset.Tables[tbName].Rows[0];

                isValid = false;
            }
            catch (SqlException ex)
            {
                CCFBGlobal.appendErrorToErrorReport("Command Text = " + command.CommandText, ex.GetBaseException().ToString());
                iRowCount = 0;
                closeConnection();
                isValid = false;
            }
        }

        public void openWhere(string whereClause)
        {
            try
            {
                openConnection();
                command = new SqlCommand("SELECT * FROM " + tbName + whereClause + " Order By TrxDate ASC, DonorID", conn);
                dadAdpt.SelectCommand = command;
                dset.Clear();
                iRowCount = dadAdpt.Fill(dset, tbName);
                closeConnection();
                if (iRowCount > 0)
                    dRow = dset.Tables[tbName].Rows[0];

                isValid = false;
            }
            catch (SqlException ex)
            {
                CCFBGlobal.appendErrorToErrorReport("Command Text = " + command.CommandText, ex.GetBaseException().ToString());
                iRowCount = 0;
                closeConnection();
                isValid = false;
            }
        }

        public void openForDate(int selectfield, DateTime TrxDate)
        {
            try
            {
                closeConnection();
                command = new SqlCommand("SELECT * FROM " + tbName + " WHERE Convert(varchar(10)," + selectFieldName(selectfield) + ",101)='" + TrxDate.ToShortDateString() + "'", conn);
                dadAdpt.SelectCommand = command;
                dset.Clear();
                iRowCount = dadAdpt.Fill(dset, tbName);
                closeConnection();
                if (iRowCount > 0)
                    dRow = dset.Tables[tbName].Rows[0];

                isValid = false;
            }
            catch (SqlException ex)
            {
                CCFBGlobal.appendErrorToErrorReport("Command Text = " + command.CommandText, ex.GetBaseException().ToString());
                iRowCount = 0;
                closeConnection();
                isValid = false;
            }
        }

        public void openDistinctDonationDates(int selectfield)
        {
            string fldname = selectFieldName(selectfield);
            try
            {
                closeConnection();

                command = new SqlCommand("SELECT Convert(varchar(10)," + fldname + ",101), Convert(varchar(10)," + fldname + ",111) FROM " + tbName
                    + " Group By Convert(varchar(10)," + fldname + ",101), Convert(varchar(10)," + fldname + ",111)"
                    + " Order By Convert(varchar(10)," + fldname + ",111) DESC", conn);
                dadAdpt = new SqlDataAdapter(command);
                dadAdpt.SelectCommand = command;
                dset.Clear();
                iRowCount = dadAdpt.Fill(dset, tbName);
                closeConnection();
            }
            catch (SqlException ex)
            {
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString());
                closeConnection();
                iRowCount = 0;
            }
        }

        public void openDistinctDonationYears()
        {
            try
            {
                closeConnection();
                command = new SqlCommand("SELECT LEFT(CONVERT(char(4),TrxDate,112),4)  FROM " + tbName
                    + " GROUP BY LEFT(CONVERT(char(4),TrxDate,112),4)"
                    + " ORDER BY LEFT(CONVERT(char(4),TrxDate,112),4) DESC", conn);
                dadAdpt = new SqlDataAdapter(command);
                dadAdpt.SelectCommand = command;
                dset.Clear();
                iRowCount = dadAdpt.Fill(dset, tbName);
                closeConnection();
            }
            catch (SqlException ex)
            {
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString());
                closeConnection();
                iRowCount = 0;
            }
        }

        public void delete(System.Int32 key)
        {
            try
            {
                openConnection();
                command = new SqlCommand("DELETE FROM " + tbName + " WHERE TrxID=" + key.ToString(), conn);
                command.ExecuteNonQuery();
                closeConnection();
            }
            catch (SqlException ex)
            {
                CCFBGlobal.appendErrorToErrorReport("Delete Command = " + command.CommandText, ex.GetBaseException().ToString());
                closeConnection();
            }
        }

        public void insert()
        {
            if (dadAdpt.UpdateCommand == null || dadAdpt.InsertCommand == null)
            {
                SqlCommandBuilder commBuilder = new SqlCommandBuilder(dadAdpt);
            }

            try
            {
                openConnection();
                dadAdpt.Update(dset, tbName);
                closeConnection();
            }
            catch (SqlException ex)
            {
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString());
            }
        }

        public void update()
        {
            if (dset.HasChanges() == true)
            {
                try
                {
                    if (dadAdpt.UpdateCommand == null)
                    {
                        SqlCommandBuilder commBuilder = new SqlCommandBuilder(dadAdpt);
                    }

                    dadAdpt.Update(dset, "CashDonations");
                    conn.Close();
                }
                catch (SqlException ex)
                {
                    CCFBGlobal.appendErrorToErrorReport("Delete Command = " + command.CommandText, ex.GetBaseException().ToString());
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

        public string selectFieldName(int whichfield)
        {
            return Enum.GetName(typeof(datefieldselection), whichfield);
        }
    }
}

