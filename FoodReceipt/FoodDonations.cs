using System;
using System.Data;
using System.Data.SqlClient;

namespace FoodReceipt
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
 

        public enum datefieldselection { Created = 0, TrxDate = 1 }

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
            get
            {
                if (dRow["Modified"].ToString() == "")
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
        public void getFavorite()
        {
            try
             {
                openConnection();
                string sql = "Select  DISTINCT TOP 20 DonorID,Name,Trxdate FROM " + tbName + " f1 join Donors f2 on f1.DonorID=f2.ID order by TrxDate DESC";
                command = new SqlCommand(sql, conn);
                dadAdpt.SelectCommand = command;
                dset.Clear();
                iRowCount = dadAdpt.Fill(dset, tbName);
                for (int i = 0; i < iRowCount; i++)
                {
                    dRow = dset.Tables[tbName].Rows[i];
                }
            }
            catch (SqlException ex)
            {
                CCFBGlobal.appendErrorToErrorReport("",ex.GetBaseException().ToString());
                closeConnection();
                iRowCount = 0;
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
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString());
                closeConnection();
                iRowCount = 0;
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
                    CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString());
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

