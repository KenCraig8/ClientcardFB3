using System;
using System.Data;
using System.Data.SqlClient;

namespace ClientCardFB3
{
    public class parm_PhoneType
    {
        string connString;
        SqlDataAdapter dadAdpt;
        DataSet dset;
        SqlCommand command;
        SqlConnection conn;
        static string tbName = "parm_PhoneType";
        bool isValid = false;
        int iRowCount = 0;

        public parm_PhoneType(string connStringIn)
        {
            conn = new SqlConnection();
            connString = connStringIn;
            conn.ConnectionString = connString;
            dset = new DataSet();
            dadAdpt = new SqlDataAdapter();
        }

        #region Get/Set Accessors
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

        public int ID
        {
            get
            {
                return dset.Tables[tbName].Rows[0].Field<System.Int32>("ID");
            }
            set
            {
                dset.Tables[tbName].Rows[0]["ID"] = value;
            }
        }

        public string Type
        {
            get
            {
                return dset.Tables[tbName].Rows[0].Field<System.String>("Type");
            }
            set
            {
                dset.Tables[tbName].Rows[0]["Type"] = value;
            }
        }

        public int SortOrder
        {
            get
            {
                return dset.Tables[tbName].Rows[0].Field<System.Int32>("SortOrder");
            }
            set
            {
                dset.Tables[tbName].Rows[0]["SortOrder"] = value;
            }
        }

        public string ShortName
        {
            get
            {
                return dset.Tables[tbName].Rows[0].Field<System.String>("ShortName");
            }
            set
            {
                dset.Tables[tbName].Rows[0]["ShortName"] = value;
            }
        }
        #endregion

        public bool open(int key)
        {
            try
            {
                openConnection();
                command = new SqlCommand("SELECT * FROM " + tbName + " WHERE ID=" + key.ToString(), conn);
                dadAdpt.SelectCommand = command;
                dset.Clear();
                iRowCount = dadAdpt.Fill(dset, tbName);
                closeConnection();
                return isValid = RowCount > 0;
            }
            catch (SqlException ex) 
            {
                closeConnection();
                CCFBGlobal.appendErrorToErrorReport("ID=" + key.ToString(), ex.GetBaseException().ToString(),
                    CCFBGlobal.serverName);
                iRowCount = 0;
                return isValid = false;
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

        public int getParmInt(string toFind)
        {
            for (int i = 0; i < dset.Tables[0].Rows.Count; i++)
            {
                if (toFind == dset.Tables[0].Rows[i]["Type"].ToString())
                {
                    return Int32.Parse(dset.Tables[0].Rows[i]["ID"].ToString());
                }
            }
            return 0;
        }

        public void delete(System.Int32 key)
        {
            SqlCommand commDelete = new SqlCommand(" DELETE FROM parm_PhoneType WHERE ID=" + key.ToString(), conn);
            command = new SqlCommand("SELECT * FROM parm_PhoneType WHERE ID=" + key.ToString(), conn);
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
                    conn.Open();
                    
                    dset.Tables[tbName].Rows[0]["ModifiedBy"] = CCFBGlobal.currentUser_Name;
                    dset.Tables[tbName].Rows[0]["Modified"] = DateTime.Now;
                    if (dadAdpt.UpdateCommand == null)
                    {
                        SqlCommandBuilder commBuilder = new SqlCommandBuilder(dadAdpt);
                    }
                    dadAdpt.Update(dset, "parm_PhoneType");
                    conn.Close();
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

