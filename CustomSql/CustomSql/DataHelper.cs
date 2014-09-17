using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace CustomSQL
{
    /// <summary>
    /// Includes functions to interact with the database. Used by multiple other classes
    /// </summary>
    public class DataHelper
    {
        public static string homeDrive = @"C:\";

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
                appendErrorToErrorReport(param, ex.ToString());
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

        // this is coppied from CCFBGlobal
        // TODO: find a way to not have this coppied
        #region CCFBGlobal
        /// <summary>
        /// Appends the error log with the error
        /// </summary>
        /// <param name="funtionParams">Any parameters the calling funtioin used</param>
        /// <param name="errorInfo">The info about the error</param>
        public static void appendErrorToErrorReport(string funtionParams, string errorInfo)
        {
            appendGeneralErrorInfo(funtionParams, errorInfo);
            string fileName = "ErrorLog.txt";
            string folderPath = homeDrive + @"ClientcardFB3\Log";
            string filePath = folderPath + "\\" + fileName;
            string whiteSpace = " ";
            using (System.IO.StreamWriter sw = System.IO.File.AppendText(filePath))
            {
                sw.Write(whiteSpace.PadLeft(3, ' '));
            }
        }

        /// <summary>
        /// Appends the error log with the general info about the error
        /// </summary>
        /// <param name="funtionParams">Any parameters the calling funtioin used</param>
        /// <param name="errorInfo">The info about the error</param>
        private static void appendGeneralErrorInfo(string funtionParams, string errorInfo)
        {
            string fileName = "ErrorLog.txt";
            string destFile = System.IO.Path.Combine("~", fileName);
            string[] todaysDateFormats = DateTime.Now.GetDateTimeFormats();
            string now = todaysDateFormats[55];
            string whiteSpace = " ";

            using (System.IO.StreamWriter sw = System.IO.File.AppendText(destFile))
            {
                sw.WriteLine();
                sw.WriteLine();
                sw.Write(now);
                sw.Write(whiteSpace.PadLeft(2, ' '));
                sw.WriteLine(errorInfo);
                sw.Write(whiteSpace.PadLeft(3, ' '));
                sw.WriteLine(funtionParams);
            }
        }
        #endregion
    }
}
