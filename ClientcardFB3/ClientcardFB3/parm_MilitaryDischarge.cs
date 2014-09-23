using System;
using System.Data;
using System.Data.SqlClient;

namespace ClientCardFB3
{
    public class parm_MilitaryDischarge
    {
        string connString;
        SqlDataAdapter dadAdpt;
        DataSet dset;
        SqlCommand command;
        SqlConnection conn;
        static string tbName = "parm_MilitaryDischarge";
        bool isValid = false;
        int iRowCount = 0;

        public parm_MilitaryDischarge(string connStringIn)
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

        public int getCode(string typeToFind)
        {
            for (int i = 0; i < dset.Tables[tbName].Rows.Count; i++)
            {
                if (typeToFind == dset.Tables[tbName].Rows[i]["Type"].ToString())
                {
                    return Convert.ToInt16(dset.Tables[tbName].Rows[i]["ID"]);
                }
            }
            return -1;
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
  
                return isValid = RowCount > 0;
            }
            catch (SqlException ex) 
            {
                CCFBGlobal.appendErrorToErrorReport("ID=" + ID.ToString(), ex.GetBaseException().ToString(),
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
                command = new SqlCommand("SELECT * FROM " + tbName + " Order By SortOrder", conn);
                dadAdpt.SelectCommand = command;
                dset.Clear();
                iRowCount = dadAdpt.Fill(dset, tbName);
                closeConnection();
            }
            catch (SqlException ex)
            {
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString(),
                   CCFBGlobal.serverName);
                iRowCount = 0;
                closeConnection(); 
            }
        }

        public void delete(System.Int32 key)
        {
            SqlCommand commDelete = new SqlCommand(" DELETE FROM parm_MilitaryDischarge WHERE ID=" + key.ToString(), conn);
            command = new SqlCommand("SELECT * FROM parm_MilitaryDischarge WHERE ID=" + key.ToString(), conn);
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

                    dset.Tables[tbName].Rows[0]["ModifiedBy"] = CCFBGlobal.currentUser_Name;
                    dset.Tables[tbName].Rows[0]["Modified"] = DateTime.Now;

                    if (dadAdpt.UpdateCommand == null)
                    {
                        SqlCommandBuilder commBuilder = new SqlCommandBuilder(dadAdpt);
                    }
                    dadAdpt.Update(dset, "parm_MilitaryDischarge");
                    closeConnection();
                }
                catch (SqlException ex) 
                {
                    CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString(),
                   CCFBGlobal.serverName);
                    closeConnection();
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

