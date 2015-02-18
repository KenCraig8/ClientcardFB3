using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ClientcardFB3
{
    public class Zipcodes : IDisposable
    {
        string connString;
        SqlConnection conn;
        
        private string zipcode = "";
        private string cityname = "";
        private string state = "";
        private string areacode = "";
        private string fips = "";
        private string county = "";
        private bool notefap = false;
        private int defaultcategory = 0;
        private bool _disposed;

        private object[] nodata = new object[8] { "", "", "", "", "", "", false, 0 };

        public Zipcodes(string connStringIn)
        {
            conn = new SqlConnection();
            connString = connStringIn;
            conn.ConnectionString = connString;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            // If you need thread safety, use a lock around these 
            // operations, as well as in your methods that use the resource.
            if (!_disposed)
            {
                if (disposing)
                {
                    if (conn != null)
                        conn.Dispose();
                }

                // Indicate that the instance has been disposed.
                conn = null;
                _disposed = true;
            }
        }

        #region Get/Set Accessors
        public string ZipCode
        { get { return zipcode; } }

        public string City
        { get { return cityname; } }

        public string State
        { get { return state; } }

        public string AreaCode
        { get { return areacode; } }

        public string FIPS
        { get { return fips; } }

        public string County
        { get { return county; } }

        public bool NoTEFAP
        { get { return notefap; } }

        public int DefaultCategory
        { get { return defaultcategory; } }
        #endregion

        public bool getCity(string zip)
        {
            System.Collections.ArrayList lstCities = new System.Collections.ArrayList();
            sqlOpenConnection();
            SqlCommand sqlCmd = new SqlCommand("SELECT * FROM ZipCodes WHERE ZipCode = '" + zip + "'", conn);
            try
            {
                SqlDataReader reader = sqlCmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        object[] values = new object[reader.FieldCount];
                        reader.GetValues(values);
                        lstCities.Add(values);
                    }
                }
                if (lstCities.Count == 1)
                {
                    setDataVariables((object[])lstCities[0]);
                }
                else if (lstCities.Count > 1)
                {
                    object[] data;
                    string msg = "Zip code " + zip + " has multiple cities." + System.Environment.NewLine;
                    for (int i = 0; i < lstCities.Count; i++)
                    {
                        data=(object[])lstCities[i];
                        msg += i.ToString() + ": " + data[1].ToString() + System.Environment.NewLine;
                    }
                    string retVal = Microsoft.VisualBasic.Interaction.InputBox(msg, "Enter Number For Desired City", "");
                    if (String.IsNullOrEmpty(retVal) == true)
                    {
                        setDataVariables((object[])lstCities[0]);
                    }
                    else
                    {
                        try
                        {
                            int ptr = Convert.ToInt32(retVal);
                            if (ptr >= 0 && ptr < lstCities.Count)
                            {
                                setDataVariables((object[])lstCities[ptr]);
                            }
                        }
                        catch (Exception)
                        {
                            
                        }
                    }
                }
                sqlCloseConnection();
                return true;
            }
            catch (SqlException ex)
            {
                CCFBGlobal.appendErrorToErrorReport(sqlCmd.CommandText, ex.GetBaseException().ToString());
            }
            setDataVariables(nodata);
            sqlCloseConnection();
            return false;
        }

        public bool getZip(string city, string tstState)
        {
            System.Collections.ArrayList lstCities = new System.Collections.ArrayList();
            sqlOpenConnection();
            string sqry = "SELECT * FROM ZipCodes WHERE City = '" + city + "'";
            if (tstState.Length >0)
            {
                sqry += " AND State = '" + tstState + "'";
            }
            SqlCommand sqlCmd = new SqlCommand(sqry, conn);
            try
            {
                SqlDataReader reader = sqlCmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        object[] values = new object[reader.FieldCount];
                        reader.GetValues(values);
                        lstCities.Add(values);
                    }
                }
                if (lstCities.Count == 1)
                {
                    setDataVariables((object[])lstCities[0]);
                }
                else if (lstCities.Count > 1)
                {
                    object[] data;
                    string msg = city + " has multiple zip codes." + System.Environment.NewLine;
                    for (int i = 0; i < lstCities.Count; i++)
                    {
                        data = (object[])lstCities[i];
                        msg += i.ToString() + ": " + data[0].ToString() + System.Environment.NewLine;
                    }
                    string retVal = Microsoft.VisualBasic.Interaction.InputBox(msg, "Enter Number For Desired Zip Code", "");
                    if (String.IsNullOrEmpty(retVal) == true)
                    {
                        setDataVariables((object[])lstCities[0]);
                    }
                    else
                    {
                        try
                        {
                            int ptr = Convert.ToInt32(retVal);
                            if (ptr >= 0 && ptr < lstCities.Count)
                            {
                                setDataVariables((object[])lstCities[ptr]);
                                return true;
                            }
                        }
                        catch (Exception)
                        {

                        }
                    }
                    reader.Dispose();
                }
            
                sqlCloseConnection();
                return true;
            }
            catch (SqlException ex)
            {
                CCFBGlobal.appendErrorToErrorReport(sqlCmd.CommandText, ex.GetBaseException().ToString());
            }
            setDataVariables(nodata);
            sqlCmd.Dispose();
            sqlCloseConnection();
            return false;
        }

        private void setDataVariables(object[] data)
        {
            zipcode = data[0].ToString();
            cityname = data[1].ToString();
            state = data[2].ToString();
            areacode = data[3].ToString();
            fips = data[4].ToString();
            county = data[5].ToString();
            notefap = (bool)data[6];
            defaultcategory = Convert.ToInt32(data[7]);
        }

        private void sqlCloseConnection()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        private void sqlOpenConnection()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
        }
    }
}

