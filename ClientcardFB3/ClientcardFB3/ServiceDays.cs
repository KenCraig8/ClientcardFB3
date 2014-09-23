
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

namespace ClientCardFB3
{
    public class ServiceDays
    {
        string connString;
        SqlDataAdapter dadAdpt;
        DataSet dset;
        SqlCommand command;
        System.Data.SqlClient.SqlConnection conn;
        static string tbName = "ServiceDays";
        bool bisValid = false;

        public ServiceDays(string connStringIn)
        {
            conn = new System.Data.SqlClient.SqlConnection();
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
                return dset.Tables[0].Rows.Count;
            }
        }

        public DateTime TrxDate
        {
            get
            {
                return dset.Tables[tbName].Rows[0].Field<System.DateTime>("TrxDate");
            }
            set
            {
                dset.Tables[tbName].Rows[0]["TrxDate"] = value;
            }
        }

        public int Item1
        {
            get
            {
                return dset.Tables[tbName].Rows[0].Field<System.Int32>("Item1");
            }
            set
            {
                dset.Tables[tbName].Rows[0]["Item1"] = value;
            }
        }

        public int Item2
        {
            get
            {
                return dset.Tables[tbName].Rows[0].Field<System.Int32>("Item2");
            }
            set
            {
                dset.Tables[tbName].Rows[0]["Item2"] = value;
            }
        }

        public int Item3
        {
            get
            {
                return dset.Tables[tbName].Rows[0].Field<System.Int32>("Item3");
            }
            set
            {
                dset.Tables[tbName].Rows[0]["Item3"] = value;
            }
        }

        public int Item4
        {
            get
            {
                return dset.Tables[tbName].Rows[0].Field<System.Int32>("Item4");
            }
            set
            {
                dset.Tables[tbName].Rows[0]["Item4"] = value;
            }
        }

        public int Item5
        {
            get
            {
                return dset.Tables[tbName].Rows[0].Field<System.Int32>("Item5");
            }
            set
            {
                dset.Tables[tbName].Rows[0]["Item5"] = value;
            }
        }

        public int Item6
        {
            get
            {
                return dset.Tables[tbName].Rows[0].Field<System.Int32>("Item6");
            }
            set
            {
                dset.Tables[tbName].Rows[0]["Item6"] = value;
            }
        }

        public int Item7
        {
            get
            {
                return dset.Tables[tbName].Rows[0].Field<System.Int32>("Item7");
            }
            set
            {
                dset.Tables[tbName].Rows[0]["Item7"] = value;
            }
        }

        public int Item8
        {
            get
            {
                return dset.Tables[tbName].Rows[0].Field<System.Int32>("Item8");
            }
            set
            {
                dset.Tables[tbName].Rows[0]["Item8"] = value;
            }
        }

        public int Item9
        {
            get
            {
                return dset.Tables[tbName].Rows[0].Field<System.Int32>("Item9");
            }
            set
            {
                dset.Tables[tbName].Rows[0]["Item9"] = value;
            }
        }

        public int Item10
        {
            get
            {
                return dset.Tables[tbName].Rows[0].Field<System.Int32>("Item10");
            }
            set
            {
                dset.Tables[tbName].Rows[0]["Item10"] = value;
            }
        }

        public bool Commodities
        {
            get
            {
                return dset.Tables[tbName].Rows[0].Field<System.Boolean>("Commodities");
            }
            set
            {
                dset.Tables[tbName].Rows[0]["Commodities"] = value;
            }
        }

        public string Notes
        {
            get
            {
                return dset.Tables[tbName].Rows[0].Field<System.String>("Notes");
            }
            set
            {
                dset.Tables[tbName].Rows[0]["Notes"] = value;
            }
        }

        public DateTime Created
        {
            get
            {
                return dset.Tables[tbName].Rows[0].Field<System.DateTime>("Created");
            }
            set
            {
                dset.Tables[tbName].Rows[0]["Created"] = value;
            }
        }

        public string CreatedBy
        {
            get
            {
                return dset.Tables[tbName].Rows[0].Field<System.String>("CreatedBy");
            }
            set
            {
                dset.Tables[tbName].Rows[0]["CreatedBy"] = value;
            }
        }

        public DateTime Modified
        {
            get
            {
                return dset.Tables[tbName].Rows[0].Field<System.DateTime>("Modified");
            }
            set
            {
                dset.Tables[tbName].Rows[0]["Modified"] = value;
            }
        }

        public string ModifiedBy
        {
            get
            {
                return dset.Tables[tbName].Rows[0].Field<System.String>("ModifiedBy");
            }
            set
            {
                dset.Tables[tbName].Rows[0]["ModifiedBy"] = value;
            }
        }



        public bool open(DateTime date)
        {
            try
            {
                openConnection();
                command = new SqlCommand("SELECT * FROM " + tbName + " WHERE TrxDate=" + date.ToString(), conn);
                dadAdpt.SelectCommand = command;
                dset.Clear();
                dadAdpt.Fill(dset, tbName);
                closeConnection();
                return isValid = RowCount > 0;
            }
            catch (SqlException ex) 
            {
                closeConnection(); 
                CCFBGlobal.appendErrorToErrorReport("TrxDate="+date.ToString(), ex.GetBaseException().ToString(),
                   CCFBGlobal.serverName);            
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
                dadAdpt.Fill(dset, tbName);
                closeConnection();
                isValid = false;
            }
            catch (SqlException ex) 
            {
                closeConnection();
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString(),
                   CCFBGlobal.serverName);
            }
        }


        public void delete(System.DateTime key)
        {
            SqlCommand commDelete = new SqlCommand(" DELETE FROM ServiceDays WHERE TrxDate=" + key.ToString());
            command = new SqlCommand("SELECT * FROM ServiceDays WHERE TrxDate=" + key.ToString(), conn);
            dset.Tables[tbName].Rows[0]["ModifiedBy"] = CCFBGlobal.currentUser_Name;
            dset.Tables[tbName].Rows[0]["Modified"] = DateTime.Now;
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
                    if (dadAdpt.UpdateCommand == null)
                    {
                        SqlCommandBuilder commBuilder = new SqlCommandBuilder(dadAdpt);
                    }
                    dadAdpt.Update(dset, "ServiceDays");
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

