
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

namespace ClientCardFB3
{
    public class parm_CSFPRoutes
    {
        string connString;
        SqlDataAdapter dadAdpt;
        DataSet dset;
        SqlCommand command;
        System.Data.SqlClient.SqlConnection conn;
        static string tbName = "parm_CSFPRoutes";
        
        public parm_CSFPRoutes()
        {
            conn = new System.Data.SqlClient.SqlConnection();
            connString = @"Data Source=localhost\SQLEXPRESS;initial catalog=ClientCardFB; Integrated Security=SSPI";
            conn.ConnectionString = connString;
            dset = new DataSet();
        }

        public int Id
        {
            get
            {
                return dset.Tables[tbName].Rows[0].Field<System.Byte>("Id");
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



        public void open(System.Byte key)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                command = new SqlCommand("SELECT * FROM " + tbName + " WHERE Id=" + key.ToString(), conn);
                dadAdpt = new SqlDataAdapter(command);
                dadAdpt.SelectCommand = command;
                dset.Clear();
                dadAdpt.Fill(dset, tbName);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            catch (SqlException ex) { }
        }


        public void delete(System.Byte key)
        {
            SqlCommand commDelete = new SqlCommand(" DELETE FROM parm_CSFPRoutes WHERE Id=" + key.ToString(), conn);
            command = new SqlCommand("SELECT * FROM parm_CSFPRoutes WHERE Id=" + key.ToString(), conn);
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
                    conn.Open();
                    SqlCommand commUpdate = new SqlCommand("UPDATE parm_CSFPRoutes SET Type=@Type, SortOrder=@SortOrder, ShortName=@ShortName WHERE Id=@Id", conn);
                    dadAdpt.UpdateCommand = commUpdate;
                    commUpdate.Parameters.Add("@Id", SqlDbType.Int, 4, "ID");
                    commUpdate.Parameters.Add("@Type", SqlDbType.NVarChar, 60, "Type");
                    commUpdate.Parameters.Add("@SortOrder", SqlDbType.Int, 4, "SortOrder");
                    commUpdate.Parameters.Add("@ShortName", SqlDbType.NVarChar, 8, "ShortName");
                    dadAdpt.Update(dset, "parm_CSFPRoutes");
                    conn.Close();
                }
                catch (SqlException ex) { }
            }
        }
    }
}

