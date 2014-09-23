
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

namespace ClientCardFB3
{
    public class parm_DonationType
    {
        string connString;
        SqlDataAdapter dadAdpt;
        DataSet dset;
        SqlCommand command;
        SqlConnection conn;
        static string tbName = "parm_DonationType";
        bool isValid = false;
        int iRowCount = 0;

        public parm_DonationType(string connStringIn)
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
        #endregion

        public int getCode(string type)
        {
            for (int i = 0; i < iRowCount; i++)
            {
                if (type == dset.Tables[tbName].Rows[i]["Type"].ToString())
                {
                    return Convert.ToInt16(dset.Tables[tbName].Rows[i]["ID"]);
                }
            }
            return -1;
        }

        public string getType(int ID)
        {
            for (int i = 0; i < iRowCount; i++)
            {
                if (ID == Convert.ToInt32(dset.Tables[tbName].Rows[i]["ID"]))
                {
                    return dset.Tables[tbName].Rows[i].Field<string>("Type");
                }
            }
            return "";
        }

        public bool open(int ID)
        {
            try
            {
                openConnection();
                command = new SqlCommand("SELECT * FROM " + tbName + " WHERE Id=" + ID.ToString(), conn);
                dadAdpt.SelectCommand = command;
                dset.Clear();
                iRowCount = dadAdpt.Fill(dset, tbName);
                closeConnection();
                return isValid = iRowCount > 0;
            }
            catch (SqlException ex) 
            {
                iRowCount = 0;
                closeConnection();
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
                isValid = iRowCount > 0;
            }
            catch (SqlException ex)
            {
                iRowCount = 0;
                closeConnection();
                isValid = false;
            }
        }

        public void delete(System.Int32 key)
        {
            SqlCommand commDelete = new SqlCommand(" DELETE FROM parm_DonationType WHERE Id=" + key.ToString(), conn);
            command = new SqlCommand("SELECT * FROM parm_DonationType WHERE Id=" + key.ToString(), conn);
            dadAdpt = new SqlDataAdapter(command);
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
                    dadAdpt.Update(dset, "parm_DonationType");
                    closeConnection();
                }
                catch (SqlException ex) { }
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

