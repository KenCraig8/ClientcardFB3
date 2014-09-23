using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

namespace ClientcardFB3
{
    class CSDGSqlDataAccess
    {
        SqlConnection sqlconnect;

        public CSDGSqlDataAccess(string connectionstring)
        {
            sqlconnect = new SqlConnection(connectionstring);
        }

        public DataTable TransferDataToLocalDataTable(string sqlText)
        {
            DataTable dt = new DataTable();
            SqlCommand sqlCmd = new SqlCommand(sqlText, sqlconnect);
            sqlCmd.CommandType = CommandType.Text;
            try
            {
                if (sqlconnect.State == ConnectionState.Closed)
                    sqlconnect.Open();        // Open the connection and execute the reader.

                SqlDataReader reader = sqlCmd.ExecuteReader();

                if (reader.HasRows)
                {
                    DataTable readerSchema = reader.GetSchemaTable().Copy();
                    for (int i = 0; i < readerSchema.Rows.Count; i++)
                    {
                        dt.Columns.Add(readerSchema.Rows[i]["ColumnName"].ToString());
                    }
                    while (reader.Read())
                    {
                        object[] values = new object[reader.FieldCount];
                        reader.GetValues(values);
                        dt.Rows.Add(values);
                    }
                }
            }
            catch { };
            if (sqlconnect.State == ConnectionState.Open)
                sqlconnect.Close();
            return dt;
        }
    }

}
