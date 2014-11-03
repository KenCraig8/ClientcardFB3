using System;
using System.Data;
using System.Data.SqlClient;

namespace ClientcardFB3
{
    public class FoodDonations
    {
        string connString;
        SqlDataAdapter dadAdpt;
        DataSet dset;
        SqlCommand command;
        System.Data.SqlClient.SqlConnection conn;
        static string tbName = "FoodDonations";
        int iRowCount = 0;
        bool isValid = false;
        DataRow dRow = null;

        public FoodDonations(string connStringIn)
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
            get { return Convert.ToInt32(dRow["TrxID"]); }
            set { dRow["TrxID"] = value; }
        }
        public int DonorID
        {
            get { return Convert.ToInt32(dRow["DonorID"]); }
            set { dRow["DonorID"] = value; }
        }
        public DateTime TrxDate
        {
            get { return (DateTime)dRow["TrxDate"]; }
            set { dRow["TrxDate"] = value; }
        }
        public string FoodCode
        {
            get { return dRow["FoodCode"].ToString(); }
            set { dRow["FoodCode"] = value; }
        }
        public float Pounds
        {
            get { return (float)CCFBGlobal.NullToZero(dRow["Pounds"]); }
            set { dRow["Pounds"] = value; }
        }
        public decimal DollarValue
        {
            get { return (decimal)dRow["DollarValue"]; }
            set { dRow["DollarValue"] = value; }
        }
        public string Notes
        {
            get { return dRow["Notes"].ToString(); }
            set { dRow["Notes"] = value; }
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
            get { return (DateTime)dRow["Modified"]; }
            set { dRow["Modified"] = value; }
        }
        public string ModifiedBy
        {
            get { return dRow["ModifiedBy"].ToString(); }
            set { dRow["ModifiedBy"] = value; }
        }
        public bool Flag0
        {
            get { return (bool)dRow["Flag0"]; }
            set { dRow["Flag0"] = value; }
        }
        public bool Flag1
        {
            get { return (bool)dRow["Flag1"]; }
            set { dRow["Flag1"] = value; }
        }
        public bool Flag2
        {
            get { return (bool)dRow["Flag2"]; }
            set { dRow["Flag2"] = value; }
        }
        public int DonationType
        {
            get { return Convert.ToInt16(CCFBGlobal.NullToZero(dRow["DonationType"])); }
            set { dRow["DonationType"] = value; }
        }
        public int FoodClass
        {
            get { return Convert.ToInt16(CCFBGlobal.NullToZero(dRow["FoodClass"])); }
            set { dRow["FoodClass"] = value; }
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

        public bool open(int TrxID)
        {
            try
            {
                closeConnection();
                command = new SqlCommand("SELECT * FROM " + tbName + " WHERE TrxID=" + TrxID.ToString(), conn);
                dadAdpt = new SqlDataAdapter(command);
                dadAdpt.SelectCommand = command;
                dset.Clear();
                iRowCount = dadAdpt.Fill(dset, tbName);
                closeConnection();
                if (iRowCount > 0)
                {
                    dRow = dset.Tables[tbName].Rows[0];
                    return isValid = true;
                }

                return isValid = false;
            }
            catch (SqlException ex)
            {
                CCFBGlobal.appendErrorToErrorReport("TrxID=" + TrxID.ToString(), ex.GetBaseException().ToString(),
                    CCFBGlobal.serverName);
                iRowCount = 0;
                closeConnection();
                return isValid = false;
            }
        }

        public void openForDate(DateTime TrxDate)
        {
            try
            {
                closeConnection();
                command = new SqlCommand("SELECT * FROM " + tbName + " WHERE TrxDate='" + TrxDate.ToString() + "'", conn);
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
                CCFBGlobal.appendErrorToErrorReport("TrxID=" + TrxDate.ToString(), ex.GetBaseException().ToString(),
                    CCFBGlobal.serverName);
                iRowCount = 0;
                closeConnection();
                isValid = false;
            }
        }

        public void openAll()
        {
            try
            {
                closeConnection();
                command = new SqlCommand("SELECT * FROM " + tbName, conn);
                dadAdpt = new SqlDataAdapter(command);
                dadAdpt.SelectCommand = command;
                dset.Clear();
                iRowCount = dadAdpt.Fill(dset, tbName);
                if (iRowCount > 0)
                    dRow = dset.Tables[tbName].Rows[0];
                closeConnection();
            }
            catch (SqlException ex)
            {
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString(),
                    CCFBGlobal.serverName);
                closeConnection();
                iRowCount = 0;
            }
        }

        public void openDistinctDonationDates()
        {
            try
            {
                closeConnection();
                command = new SqlCommand("SELECT Distinct TrxDate FROM " + tbName
                    + " Order By TrxDate DESC", conn);
                dadAdpt = new SqlDataAdapter(command);
                dadAdpt.SelectCommand = command;
                dset.Clear();
                iRowCount = dadAdpt.Fill(dset, tbName);
                closeConnection();
            }
            catch (SqlException ex)
            {
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString(),
                    CCFBGlobal.serverName);
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
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString(),
                    CCFBGlobal.serverName);
                closeConnection();
                iRowCount = 0;
            }
        }

        public void openDonationForMonth(string sDonorId,string sDonationType, string sBetween)
        {
            try
            {
                closeConnection();
                command = new SqlCommand("SELECT *, DATEPART(ww,TrxDate) WeekOfYear,DATEPART(dw,TrxDate) DayOfWeek   FROM " + tbName 
                    + " WHERE DonationType = " + sDonationType
                    + "   AND DonorId = " + sDonorId
                    + "   AND TrxDate Between " + sBetween
                    + " ORDER BY WeekOfYear, DayOfWeek,FoodClass", conn);
                dadAdpt = new SqlDataAdapter(command);
                dadAdpt.SelectCommand = command;
                dset.Clear();
                iRowCount = dadAdpt.Fill(dset, tbName);
                if (iRowCount > 0)
                    dRow = dset.Tables[tbName].Rows[0];
                closeConnection();
            }
            catch (SqlException ex)
            {
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString(),
                    CCFBGlobal.serverName);
                closeConnection();
                iRowCount = 0;
            }
        }

        public void openWhere(string whereClause)
        {
            try
            {
                closeConnection();
                command = new SqlCommand("SELECT * FROM " + tbName + " " + whereClause, conn);
                dadAdpt.SelectCommand = command;
                dset.Clear();
                iRowCount = dadAdpt.Fill(dset, tbName);
                closeConnection();
                if (iRowCount > 0)
                    dRow = dset.Tables[tbName].Rows[0];
            }
            catch (SqlException ex)
            {
                CCFBGlobal.appendErrorToErrorReport("whereClause=" + whereClause, ex.GetBaseException().ToString(),
                    CCFBGlobal.serverName);
                closeConnection();
                iRowCount = 0;
            }
        }

        public void delete(System.Int32 ID)
        {
            SqlCommand commDelete = new SqlCommand(" DELETE FROM " + tbName + " WHERE TrxID=" + ID.ToString(), conn);
            openConnection();
            commDelete.ExecuteNonQuery();
            closeConnection();
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
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString(),
                    CCFBGlobal.serverName);
            }
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
                    dadAdpt.Update(dset, tbName);
                    closeConnection();
                }
                catch (SqlException ex)
                {
                    CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString(),
                        CCFBGlobal.serverName);
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

