using System;
using System.Data;
using System.Data.SqlClient;

namespace ClientcardFB3
{
    public class Zipcodes
    {
        string connString;
        SqlDataAdapter dadAdpt;
        DataSet dset;
        SqlCommand command;
        SqlConnection conn;
        static string tbName = "Zipcodes";
        bool isValid = false;
        int iRowCount = 0;
        DataRow drow;

        public Zipcodes(string connStringIn)
        {
            conn = new SqlConnection();
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

        public string ZipCode
        {
            get
            {
                return drow["ZipCode"].ToString();
            }
            set
            {
                drow["ZipCode"] = value;
            }
        }

        public string City
        {
            get
            {
                return drow["City"].ToString();
            }
            set
            {
                drow["City"] = value;
            }
        }

        public string State
        {
            get
            {
                return drow["State"].ToString();
            }
            set
            {
                drow["State"] = value;
            }
        }

        public string AreaCode
        {
            get
            {
                return drow["AreaCode"].ToString();
            }
            set
            {
                drow["AreaCode"] = value;
            }
        }

        public string FIPS
        {
            get
            {
                return drow["FIPS"].ToString();
            }
            set
            {
                drow["FIPS"] = value;
            }
        }

        public string County
        {
            get
            {
                return drow["County"].ToString();
            }
            set
            {
                drow["County"] = value;
            }
        }
        #endregion

        public void setDataRow(int rowIndex)
        {
            if (rowIndex < iRowCount && rowIndex >= 0)
                drow = dset.Tables[tbName].Rows[rowIndex];
        }

        public bool open(string toFind, string collumnName)
        {
            try
            {
                openConnection();
                command = new SqlCommand("SELECT * FROM " + tbName + " WHERE " + collumnName + "='" + toFind + "' "
                   + "AND State='"+ CCFBPrefs.DefaultState + "'", conn);
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
                closeConnection();
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString(),
                    CCFBGlobal.serverName);
                return isValid = false;
            }
        }


        public void delete(System.String key)
        {
            SqlCommand commDelete = new SqlCommand(" DELETE FROM Zipcodes WHERE ZipCode=" + key.ToString(), conn);
            command = new SqlCommand("SELECT * FROM Zipcodes WHERE ZipCode=" + key.ToString(), conn);
            dadAdpt.DeleteCommand = commDelete;
            dadAdpt.SelectCommand = command;
            dset.Clear();
            dadAdpt.Fill(dset, tbName);
            dset.Tables[tbName].Rows[0].Delete();
            dadAdpt.Update(dset, tbName);
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
                    dadAdpt.Update(dset, "Zipcodes");

                    closeConnection();
                }
                catch (SqlException ex) 
                {
                    closeConnection();
                    CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString(),
                        CCFBGlobal.serverName);
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

