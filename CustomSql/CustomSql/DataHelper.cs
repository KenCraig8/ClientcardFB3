using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace MapData2
{
    public class DataHelper
    {
        public DataTable sqlQuery(string connectionString, string sqlQuery)
        {
            SqlConnection databaseConnection = new SqlConnection(connectionString);
            databaseConnection.Open();

            SqlCommand dataCommand = new SqlCommand(sqlQuery, databaseConnection);

            DataTable dTable = new DataTable();
            SqlDataAdapter dadAdpt = new SqlDataAdapter();
            dadAdpt.SelectCommand = dataCommand;
            dadAdpt.Fill(dTable);

            databaseConnection.Close();

            return dTable;
        }

        //Returns all of the entrys in a one column DataTable as an array
        public string[] dataTableToArray(DataTable table)
        {
            return table.AsEnumerable().Select(row => Convert.ToString(row.Field<object>(0))).ToArray();
        }

        public string enumToSqlIn(System.Collections.IEnumerable inEnum)
        {
            const string kSeperator = ", ";

            string valuesStr = "";

            //I can't use the String.Join for this because it will put the empty string in for null values
            //so the resulting string will look like "[itemName, , , , , ]
            foreach (object item in inEnum)
            {
                if (item != null)
                    valuesStr += "'"+item+"'" + kSeperator;
            }
            
            return "(" + valuesStr.Substring(0, valuesStr.Length - kSeperator.Length) + ")";
        }
    }
}
