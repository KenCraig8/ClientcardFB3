
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

namespace ClientcardFB3
{
    public class parm_ItemType
    {
        string connString;
        SqlDataAdapter dadAdpt;
        DataSet dset;
        SqlCommand command;
        SqlConnection conn;
        static string tbName = "parm_ItemType";
        bool bisValid = false;
        int iRowCount = 0;

        public parm_ItemType(string connStringIn)
        {
            conn = new SqlConnection();
            connString = connStringIn;
            conn.ConnectionString = connString;
            dset = new DataSet();
            dadAdpt = new SqlDataAdapter();
        }

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

        public int getCode(string type)
        {
            for (int i = 0; i < dset.Tables[tbName].Rows.Count; i++)
            {
                if (type == dset.Tables[tbName].Rows[i].Field<string>("Type").ToString()
                    || type == dset.Tables[tbName].Rows[i].Field<string>("ShortName").ToString())
                {
                    return Convert.ToInt16(dset.Tables[tbName].Rows[i]["ID"]);
                }
            }
            return -1;
        }

        public void open(int ID)
        {
            try
            {
                openConnection();
                command = new SqlCommand("SELECT * FROM " + tbName + " WHERE ID=" + ID.ToString(), conn);
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

        public void delete(System.Int32 ID)
        {
            SqlCommand commDelete = new SqlCommand(" DELETE FROM " + tbName + " WHERE ID=" + ID.ToString(), conn);
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
                    dset.Tables[tbName].Rows[0]["ModifiedBy"] = CCFBGlobal.currentUser_Name;
                    dset.Tables[tbName].Rows[0]["Modified"] = DateTime.Now;
                   
                    if (dadAdpt.UpdateCommand == null)
                    {
                        SqlCommandBuilder commBuilder = new SqlCommandBuilder(dadAdpt);
                    }

                    dadAdpt.Update(dset, "parm_ItemType");
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

