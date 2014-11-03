using System;
using System.Data;
using System.Data.SqlClient;

namespace ClientCardFB3
{
    public class Parm_UserFlags
    {
        string connString;
        SqlDataAdapter dadAdpt;
        DataSet dset;
        SqlCommand command;
        SqlConnection conn;
        static string tbName = "Parm_UserFlags";
        int iRowCount = 0;
 
        public Parm_UserFlags(string connStringIn)
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

        public int Id
        {
            get
            {
                return dset.Tables[tbName].Rows[0].Field<System.Int32>("Id");
            }
            set
            {
                dset.Tables[tbName].Rows[0]["Id"] = value;
            }
        }

        public int RowCount
        {
            get
            {
                return iRowCount;
            }
        }

        public int Flag0
        {
            get
            {
                return dset.Tables[tbName].Rows[0].Field<System.Int32>("Flag0");
            }
            set
            {
                dset.Tables[tbName].Rows[0]["Flag0"] = value;
            }
        }

        public int Flag1
        {
            get
            {
                return dset.Tables[tbName].Rows[0].Field<System.Int32>("Flag1");
            }
            set
            {
                dset.Tables[tbName].Rows[0]["Flag1"] = value;
            }
        }

        public int Flag2
        {
            get
            {
                return dset.Tables[tbName].Rows[0].Field<System.Int32>("Flag2");
            }
            set
            {
                dset.Tables[tbName].Rows[0]["Flag2"] = value;
            }
        }

        public int Flag3
        {
            get
            {
                return dset.Tables[tbName].Rows[0].Field<System.Int32>("Flag3");
            }
            set
            {
                dset.Tables[tbName].Rows[0]["Flag3"] = value;
            }
        }

        public int Flag4
        {
            get
            {
                return dset.Tables[tbName].Rows[0].Field<System.Int32>("Flag4");
            }
            set
            {
                dset.Tables[tbName].Rows[0]["Flag4"] = value;
            }
        }

        public int Flag5
        {
            get
            {
                return dset.Tables[tbName].Rows[0].Field<System.Int32>("Flag5");
            }
            set
            {
                dset.Tables[tbName].Rows[0]["Flag5"] = value;
            }
        }

        public int Flag6
        {
            get
            {
                return dset.Tables[tbName].Rows[0].Field<System.Int32>("Flag6");
            }
            set
            {
                dset.Tables[tbName].Rows[0]["Flag6"] = value;
            }
        }

        public int Flag7
        {
            get
            {
                return dset.Tables[tbName].Rows[0].Field<System.Int32>("Flag7");
            }
            set
            {
                dset.Tables[tbName].Rows[0]["Flag7"] = value;
            }
        }

        public int Flag8
        {
            get
            {
                return dset.Tables[tbName].Rows[0].Field<System.Int32>("Flag8");
            }
            set
            {
                dset.Tables[tbName].Rows[0]["Flag8"] = value;
            }
        }

        public int Flag9
        {
            get
            {
                return dset.Tables[tbName].Rows[0].Field<System.Int32>("Flag9");
            }
            set
            {
                dset.Tables[tbName].Rows[0]["Flag9"] = value;
            }
        }
        #endregion

        public void open(int ID)
        {
            try
            {
                openConnection();
                command = new SqlCommand("SELECT * FROM " + tbName + " WHERE Id=" + ID.ToString(), conn);
                dadAdpt.SelectCommand = command;
                dset.Clear();
                iRowCount = dadAdpt.Fill(dset, tbName);
                closeConnection();
            }
            catch (SqlException ex) 
            {
                closeConnection();
                CCFBGlobal.appendErrorToErrorReport("ID=" + ID.ToString(), ex.GetBaseException().ToString(),
                   CCFBGlobal.serverName);
                iRowCount = 0;
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
            }
            catch (SqlException ex) 
            {
                closeConnection();
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString(),
                   CCFBGlobal.serverName);
                iRowCount = 0;
            }
        }

        public void delete(int key)
        {
            SqlCommand commDelete = new SqlCommand(" DELETE FROM Parm_UserFlags WHERE Id=" + key.ToString(), conn);
            command = new SqlCommand("SELECT * FROM Parm_UserFlags WHERE Id=" + key.ToString(), conn);
            dadAdpt.DeleteCommand = commDelete;
            dadAdpt.SelectCommand = command;
            dset.Clear();
            dadAdpt.Fill(dset, tbName);
            dset.Tables[tbName].Rows[0].Delete();
            dadAdpt.Update(dset, tbName);
            iRowCount = 0;
        }


        public void update()
        {
            if (dset.HasChanges() == true)
            {
                try
                {
                    openConnection();
                   
                    dset.Tables[tbName].Rows[0]["ModifiedBy"] = CCFBGlobal.currentUser_Name;
                    dset.Tables[tbName].Rows[0]["Modified"] = DateTime.Now;

                    if (dadAdpt.UpdateCommand == null)
                    {
                        SqlCommandBuilder commBuilder = new SqlCommandBuilder(dadAdpt);
                    }
                    dadAdpt.Update(dset, "Parm_UserFlags");
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

