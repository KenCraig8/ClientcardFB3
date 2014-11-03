using System;
using System.Data;
using System.Data.SqlClient;

namespace ClientcardFB3
{
    public class Userlist
    {
        string connString;
        SqlDataAdapter dadAdpt;
        DataSet dset;
        SqlCommand command;
        SqlConnection conn;
        static string tbName = "Userlist";
        bool isValid = false;
        int iRowCount = 0;
        DataRow drow;

        public Userlist(string connStringIn)
        {
            conn = new SqlConnection();
            connString = connStringIn;
            conn.ConnectionString = connString;
            dset = new DataSet();
            dadAdpt = new SqlDataAdapter();
        }

        #region Get/Set Accessors

        public string ConnectionString
        {
            get
            {
                return connString;
            }
            set
            {
                connString = value;
                conn.ConnectionString = connString;
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

        public string Username
        {
            get
            {
                return drow["Username"].ToString();
            }
            set
            {
                drow["Username"] = value;
            }
        }

        public string Password
        {
            get
            {
                return drow["Password"].ToString();
            }
            set
            {
                drow["Password"] = value;
            }
        }

        public DateTime LastLogon
        {
            get
            {
                return (DateTime)drow["LastLogon"];
            }
            set
            {
                drow["LastLogon"] = value;
            }
        }

        public string UserRole
        {
            get
            {
                return drow["UserRole"].ToString();
            }
            set
            {
                drow["UserRole"] = value;
            }
        }
        #endregion

        public void SetDataValue(int rowNum, string FieldName, string FieldValue)
        {
            dset.Tables[tbName].Rows[rowNum][FieldName] = FieldValue;
        }

        public void setDataRow(int rowIndex)
        {
            drow = dset.Tables[tbName].Rows[rowIndex];
        }

        public void find(int ID)
        {
            for (int i = 0; i < iRowCount; i++)
            {
                if (dset.Tables[tbName].Rows[i].Field<int>("ID") == ID)
                {
                    drow = dset.Tables[tbName].Rows[i];
                    return;
                }
            }
        }

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
                CCFBGlobal.appendErrorToErrorReport("ID="+ID.ToString(), ex.GetBaseException().ToString(),
                  CCFBGlobal.serverName);
                closeConnection();
                iRowCount = 0;
                return isValid = false;
            }
        }

        public void openAll()
        {
            try
            {
                openConnection();
                command = new SqlCommand("SELECT * FROM " + tbName + " Order By Username", conn);
                dadAdpt = new SqlDataAdapter(command);
                dadAdpt.SelectCommand = command;
                dset.Clear();
                iRowCount = dadAdpt.Fill(dset, tbName);
                closeConnection();
               
                if (iRowCount > 0)
                    drow = dset.Tables[tbName].Rows[0];
            }
            catch (SqlException ex) 
            {
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString(),
                  CCFBGlobal.serverName);
                iRowCount = 0;
                closeConnection();
            }
        }

        public void delete(int key)
        {
            SqlCommand commDelete = new SqlCommand(" DELETE FROM Userlist WHERE ID=" + key.ToString(), conn);
            command = new SqlCommand("SELECT * FROM Userlist WHERE ID=" + key.ToString(), conn);
            dadAdpt.DeleteCommand = commDelete;
            dadAdpt.SelectCommand = command;
            dset.Clear();
            dadAdpt.Fill(dset, tbName);
            dset.Tables[tbName].Rows[0].Delete();
            dadAdpt.Update(dset, tbName);
        }

        public bool insert()
        {
            try
            {
                openConnection();

                if (dadAdpt.InsertCommand == null ||dadAdpt.UpdateCommand == null)
                {
                    SqlCommandBuilder commBuilder = new SqlCommandBuilder(dadAdpt);
                }

                dadAdpt.Update(dset, tbName);
                closeConnection();
                return true;
            }
            catch (SqlException ex) 
            {
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString(),
                  CCFBGlobal.serverName);
                closeConnection(); 
                return false; 
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
                        SqlCommandBuilder commBuilder = new SqlCommandBuilder(dadAdpt);
                    }

                    dadAdpt.Update(dset, tbName);
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

