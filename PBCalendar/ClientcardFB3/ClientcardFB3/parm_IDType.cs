
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

namespace ClientCardFB3
{
    public class parm_IDType
    {
        string connString;
        SqlDataAdapter dadAdpt;
        DataSet dset;
        SqlCommand command;
        SqlConnection conn;
        static string tbName = "parm_IDType";
        int iRowCount = 0;

        public parm_IDType(string connStringIn)
        {
            conn = new SqlConnection();
            connString = connStringIn;
            conn.ConnectionString = connString;
            dset = new DataSet();
            dadAdpt = new SqlDataAdapter();
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
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString(), 
                    CCFBGlobal.serverName);
                closeConnection();
                iRowCount = 0;
            }
        }

        public void open(int ID)
        {
            try
            {
                openConnection();
                command = new SqlCommand("SELECT * FROM " + tbName + " WHERE Id=" + ID.ToString());
                dadAdpt = new SqlDataAdapter(command);
                dadAdpt.SelectCommand = command;
                dset.Clear();
                dadAdpt.SelectCommand.Connection = conn;
                iRowCount = dadAdpt.Fill(dset, tbName);
                closeConnection();
            }
            catch (SqlException ex) 
            {
                CCFBGlobal.appendErrorToErrorReport("ID=" + ID.ToString(), ex.GetBaseException().ToString(),
                    CCFBGlobal.serverName);
                closeConnection();
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
            SqlCommand commDelete = new SqlCommand(" DELETE FROM parm_IDType WHERE Id=" + key.ToString());
            command = new SqlCommand("SELECT * FROM parm_IDType WHERE Id=" + key.ToString(), conn);
            dadAdpt = new SqlDataAdapter(command);
            dadAdpt.DeleteCommand = commDelete;
            dadAdpt.SelectCommand = command;
            dadAdpt.SelectCommand.Connection = conn;
            dadAdpt.DeleteCommand = commDelete;
            dadAdpt.DeleteCommand.Connection = conn;
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

                    dadAdpt.Update(dset, "parm_IDType");
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

