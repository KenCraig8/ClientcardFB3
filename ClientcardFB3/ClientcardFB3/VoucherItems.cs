using System;
using System.Data;
using System.Data.SqlClient;

namespace ClientcardFB3
{
    public class VoucherItems
    {
        string connString;
        SqlDataAdapter dadAdpt;
        DataSet dset;
        SqlCommand command;
        SqlConnection conn;
        static string tbName = "VoucherItems";
        bool bisValid = false;
        int iRowCount = 0;
        DataRow drow;

        public VoucherItems(string connStringIn)
        {
            conn = new SqlConnection();
            connString = connStringIn;
            conn.ConnectionString = connString;
            dset = new DataSet();
            dadAdpt = new SqlDataAdapter();
        }

        #region Get/Set Accessors
        public bool isValid
        {
            get
            {
                return bisValid;
            }
            set
            {
                bisValid = value;
            }
        }

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

        public int RowCount
        {
            get
            {
                return iRowCount;
            }
        }

        public int UID
        {
            get
            {
                return Convert.ToInt32(drow["UID"]);
            }
            set
            {
                drow["UID"] = value;
            }
        }

        public string Description
        {
            get
            {
                return drow["Description"].ToString();
            }
            set
            {
                drow["Description"] = value;
            }
        }

        public decimal DefaultAmount
        {
            get
            {
                return Convert.ToDecimal(drow["DefaultAmount"]);
            }
            set
            {
                drow["DefaultAmount"] = value;
            }
        }

        public decimal MaxAmount
        {
            get
            {
                return Convert.ToDecimal(drow["MaxAmount"]);
            }
            set
            {
                drow["MaxAmount"] = value;
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
            drow[FieldName] = FieldValue;
        }
        //Sets data value when value is a int
        public void SetDataValue(string FieldName, int FieldValue)
        {
            drow[FieldName] = FieldValue;
        }
        //Sets data value when value is a bool
        public void SetDataValue(string FieldName, bool FieldValue)
        {
            drow[FieldName] = FieldValue;
        }
        //Sets data value when value is a DateTime
        public void SetDataValue(string FieldName, DateTime FieldValue)
        {
            drow[FieldName] = FieldValue;
        }
        //Sets data value when value is a float
        public void SetDataValue(string FieldName, float FieldValue)
        {
            drow[FieldName] = FieldValue;
        }
        //Gets property through use of just the collum name in database
        public object GetDataValue(string FieldName)
        {
            return drow[FieldName];
        }

        #endregion

        public bool open(int UID)
        {
            try
            {
                openConnection();
                command = new SqlCommand("SELECT * FROM " + tbName + " WHERE UID=" + UID.ToString(), conn);
                dadAdpt.SelectCommand = command;
                dset.Clear();
                iRowCount = dadAdpt.Fill(dset, tbName);
                closeConnection();
                if (iRowCount > 0)
                {
                    drow = dset.Tables[tbName].Rows[0];
                    return isValid = true;
                }
                return false;
            }
            catch (SqlException ex) 
            {
                closeConnection();
                CCFBGlobal.appendErrorToErrorReport("UID=" + UID.ToString(), ex.GetBaseException().ToString());
                return bisValid = false;
            }
        }

        public void openAll()
        {
            try
            {
                openConnection();
                command = new SqlCommand("SELECT * FROM " + tbName, conn);
                dadAdpt.SelectCommand = command;
                dset.Clear();
                iRowCount = dadAdpt.Fill(dset, tbName);
                closeConnection();
                bisValid = false;
                if (iRowCount > 0)
                {
                    drow = dset.Tables[tbName].Rows[0];
                }
            }
            catch (SqlException ex) 
            {
                bisValid = false; 
                closeConnection();
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString());
            }
        }

        public void delete(int UID)
        {
            SqlCommand commDelete = new SqlCommand(" DELETE FROM " + tbName + " WHERE ID=" + UID.ToString(), conn);
            openConnection();
            commDelete.ExecuteNonQuery();
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
                        SqlCommandBuilder commBuilder = new SqlCommandBuilder(dadAdpt);
                    }
                    dadAdpt.Update(dset, "VoucherItems");
                    closeConnection();
                }
                catch (SqlException ex) 
                {
                    closeConnection();
                    CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString());
                }
            }
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

