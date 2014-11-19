using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using ClientcardFB3;

namespace CustomSQL
{
    /// <summary>
    /// Includes functions to interact with the database. Used by multiple other classes
    /// </summary>
    public class DataHelper
    {
        /// <summary>
        /// Runs the query.
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="sqlSelectQuery"></param>
        /// <returns>DataTable containing the result</returns>
        public virtual DataTable sqlSelectQuery(string connectionString, string sqlQuery)
        {
            try
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
            catch (Exception ex)
            {
                string param = "Con str: " + connectionString + " Sql Query: " + sqlQuery;
                CCFBGlobal.appendErrorToErrorReport(param, ex.ToString());
                return new DataTable();
            }
        }

        /// <summary>
        /// Returns all of the entries in a one column DataTable as an array
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static string[] dataTableToArray(DataTable table)
        {
            return table.AsEnumerable().Select(row => Convert.ToString(row.Field<object>(0))).ToArray();
        }

        /// <summary>
        /// Concatenates the values into a list for SQL.
        /// </summary>
        /// <param name="inEnum"></param>
        /// <returns></returns>
        public static string enumToSqlIn(System.Collections.IEnumerable inEnum)
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

            if (valuesStr.Length >= kSeperator.Length)
            {
                valuesStr = valuesStr.Substring(0, valuesStr.Length - kSeperator.Length);
            }

            return "(" + valuesStr + ")";
        }
    }
}
