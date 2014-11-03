//Encapsulates Households, HouseholdMembers, and Household Service Transactions
//into one overall Client.  

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

namespace ClientcardFB3
{
    public class Client
    {
        public Household clsHH;
        public TrxLog clsHHSvcTrans;
        public HHMembers clsHHmem;
        string connString;
        SqlDataAdapter dadAdpt;
        DataSet dset;
        SqlCommand command;
        SqlConnection conn;
        string tbName = "HouseholdMembers";
        int iRowCount = 0;
        bool bHasCSFP = false;
        int servedHHMemID = 0;
        SqlDataReader reader;

//---------------------------------------Constructor-----------------------------------------------------
/// <summary>
/// Initializes all local data members of class
/// </summary>
/// <param name="connectString">connectString = Connection String</param>
        
        public Client(string connectString)
        {
            connString = connectString;
            clsHH = new Household(connString);
            clsHHmem = new HHMembers(connString);
            clsHHSvcTrans = new TrxLog(connString);
            conn = new SqlConnection();
            connString = CCFBGlobal.connectionString;
            conn.ConnectionString = connString;
            dset = new DataSet();
            dadAdpt = new SqlDataAdapter();
            
        }

        public Client()
        {
            connString = CCFBGlobal.connectionString;
            clsHH = new Household(connString);
            clsHHmem = new HHMembers(connString);
            clsHHSvcTrans = new TrxLog(connString);
            conn = new SqlConnection();
            conn.ConnectionString = connString;
            dset = new DataSet();
            dadAdpt = new SqlDataAdapter();
        }

        #region Get/Set Accessors
        public SqlDataReader Reader
        {
            get
            {
                return reader;
            }
        }
        public int ServingHHMemID
        {
            get
            {
                return servedHHMemID;
            }
            set
            {
                servedHHMemID = value;
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

        public string connectionString //The connection string
        {
            get
            {
                return connString;
            }
            set
            {
                connString = value;
            }
        }

        public int RowCount
        {
            get
            {
                return iRowCount;
            }
        }

        public bool HasCSFP
        {
            get { return bHasCSFP; }
        }
        #endregion

        public bool householdExists(int HHID)
        {
            SqlCommand sqlCmd = new SqlCommand("SELECT Count(*) FROM Household WHERE ID = " + HHID.ToString(), conn);
            openConnection();
            int iResult = (int)sqlCmd.ExecuteScalar();
            closeConnection();
            return (iResult>0);
        }
        public bool open(int HHID, bool loadHHmem, bool loadSvcTrans)
        {
            bool toReturn = clsHH.open(HHID);

            if (loadHHmem == true)
                clsHHmem.openHHID(HHID);

            return toReturn;
        }
        public bool open(int HHID, bool loadHHmem, bool loadSvcTrans, DateTime dateFrom, DateTime dateTo)
        {
            bool toReturn = clsHH.open(HHID);

            if (loadHHmem == true)
                clsHHmem.openHHID(HHID);

            return toReturn;
        }

        public void openAll()
        {
            try
            {
                openConnection();
                command = new SqlCommand("SELECT m.*, h.Name, h.Address, h.City, "
                    + "h.Zipcode, h.Phone, h.ClientType FROM " + tbName
                    + " m Inner Join Household h on h.ID=m.HouseholdID Order By LastName ASC, FirstName ASC", conn);
                dadAdpt.SelectCommand = command;
                dset.Clear();
                dadAdpt.SelectCommand.Connection = conn;
                iRowCount = dadAdpt.Fill(dset, tbName);
                closeConnection();
            }
            catch (SqlException ex) 
            {
                closeConnection();
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString(),
                    CCFBGlobal.serverName);
                iRowCount = 0;
            }
        }

        public void openWhere(string whereClause)
        {
            try
            {
                command = new SqlCommand("SELECT h.ID HHId, m.*, h.Name, h.Address, h.City"
                    + ",h.Zipcode, h.Phone, h.ClientType, h.LatestService "
                    + ",ltrim(rtrim(m.LastName)) + ', ' + ltrim(rtrim(m.FirstName)) colNameLF"
                    + ",ltrim(rtrim(m.FirstName)) + ' ' + ltrim(rtrim(m.LastName)) colNameFL"
                    + ",ct.Type ClientTypeName "
                    + ",h.Inactive HHInactive"
                    + " FROM  Household h Left Join HouseholdMembers m on h.ID=m.HouseholdID"
                    + " LEFT JOIN parm_ClientType ct ON h.ClientType = ct.ID"
                    + " Where " + whereClause, conn);
                dadAdpt.SelectCommand = command;
                    
                dset.Clear();
                iRowCount = dadAdpt.Fill(dset, tbName);
            }
            catch (SqlException ex) 
            {
                closeConnection();
                CCFBGlobal.appendErrorToErrorReport("WhereClause="+whereClause, ex.GetBaseException().ToString(),
                    CCFBGlobal.serverName);
                iRowCount = 0;
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
        public void UpdateLatestServiceDates()
        {
            SqlCommand  cmdUpdate= new SqlCommand();
            cmdUpdate.CommandType = CommandType.StoredProcedure;
            cmdUpdate.Connection = conn;
            conn.Open();
            try
            {
                cmdUpdate.CommandText = "UpdateHouseholdTrxDates";
                cmdUpdate.Parameters.AddWithValue("@HHId", clsHH.ID);
                cmdUpdate.Parameters.AddWithValue("@LowDate", CCFBGlobal.CurrentFiscalStartDate().ToShortDateString());
                cmdUpdate.Parameters.AddWithValue("@HiDate", CCFBGlobal.CurrentFiscalEndDate().ToShortDateString());
                cmdUpdate.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error ({0}): {1}", ex.Number, ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("Error: {0}", ex.Message);
            }
            catch (Exception ex)
            {
                // You might want to pass these errors
                // back out to the caller.
                Console.WriteLine("Error: {0}", ex.Message);
            }
            conn.Close();
        }

        public void UpdateDataBasedOn(DateTime DateAsOf)
        {
            if (clsHH.UseFamilyList == true)
            {
                calcSpecialDietAndDissabled();
                calcAllHHMemAges(DateAsOf);
                clsHHmem.update();
                    clsHH.update();
            }
        }

        /// <summary>
        /// Calculates the Ages of each Household Member and sets those values in the form and in the database
        /// </summary>
        public bool calcAllHHMemAges(DateTime BaseDate)
        {
            int[] AgeGroupsTotals = new int[6]{0,0,0,0,0,0};
            DateTime BirthDay;
            if (clsHH.UseFamilyList == true)
            {
                for (int i = 0; i < clsHHmem.RowCount; i++)
                {
                    HHMemberItem clsHHMemberItem = new HHMemberItem(clsHHmem.DSet.Tables[0].Rows[i]);
                    int newAge = 0;
                    if (clsHHMemberItem.Inactive == false)
                    {
                        newAge = clsHHMemberItem.Age;
                        if (clsHHMemberItem.UseAge == false)
                        {
                            BirthDay = clsHHMemberItem.Birthdate;
                            if (BirthDay != null && BirthDay.ToShortDateString() != "" && BirthDay != DateTime.MaxValue)
                            {
                                newAge = CCFBGlobal.calcAge(BirthDay, BaseDate);
                                clsHHMemberItem.Age = newAge;
                            }
                        }
                        clsHHMemberItem.AgeGroup = clsHHmem.GetEFAPAgeGroup(newAge);
                        AgeGroupsTotals[clsHHMemberItem.AgeGroup]++;
                    }
                }
                int totalFamily = AgeGroupsTotals[CCFBGlobal.ageGroup_Infant]
                                  + AgeGroupsTotals[CCFBGlobal.ageGroup_Youth]
                                  + AgeGroupsTotals[CCFBGlobal.ageGroup_Teen]
                                  + AgeGroupsTotals[CCFBGlobal.ageGroup_Eighteen]
                                  + AgeGroupsTotals[CCFBGlobal.ageGroup_Adult]
                                  + AgeGroupsTotals[CCFBGlobal.ageGroup_Senior];

                if (clsHH.Infants != AgeGroupsTotals[CCFBGlobal.ageGroup_Infant]
                   || clsHH.Youth != AgeGroupsTotals[CCFBGlobal.ageGroup_Youth]
                   || clsHH.Teens != AgeGroupsTotals[CCFBGlobal.ageGroup_Teen]
                   || clsHH.Eighteens != AgeGroupsTotals[CCFBGlobal.ageGroup_Eighteen]
                   || clsHH.Adults != AgeGroupsTotals[CCFBGlobal.ageGroup_Adult]
                   || clsHH.Seniors != AgeGroupsTotals[CCFBGlobal.ageGroup_Senior]
                   || clsHH.TotalFamily != totalFamily)
                {
                    clsHH.Infants = AgeGroupsTotals[CCFBGlobal.ageGroup_Infant];
                    clsHH.Youth = AgeGroupsTotals[CCFBGlobal.ageGroup_Youth];
                    clsHH.Teens = AgeGroupsTotals[CCFBGlobal.ageGroup_Teen];
                    clsHH.Eighteens = AgeGroupsTotals[CCFBGlobal.ageGroup_Eighteen];
                    clsHH.Adults = AgeGroupsTotals[CCFBGlobal.ageGroup_Adult];
                    clsHH.Seniors = AgeGroupsTotals[CCFBGlobal.ageGroup_Senior];
                    clsHH.TotalFamily = totalFamily;
                    return true;
                }
            }
            return false;
        }

        private bool getIsAcctive(int row)
        {
            if (clsHH.UseFamilyList == true)
            {
                return !clsHHmem.DSet.Tables[0].Rows[row].Field<bool>("Inactive");
            }
            else
            {
                return false;
            }
        }

        public bool calcSpecialDietAndDissabled()
        {
            if (clsHH.UseFamilyList)
            {
                int specialDiet = 0;
                int dissabled = 0;
                for (int i = 0; i < clsHHmem.RowCount; i++)
                {
                    HHMemberItem clsHHMemberItem = new HHMemberItem(clsHHmem.DSet.Tables[0].Rows[i]);
                    if (clsHHMemberItem.Inactive == false)
                    {
                        if (clsHHMemberItem.SpecialDiet == true)
                        { specialDiet++; }
                        if (clsHHMemberItem.IsDisabled == true)
                        { dissabled++; }
                    }
                }
                if (clsHH.SpecialDiet != specialDiet || clsHH.Disabled != dissabled)
                {
                    clsHH.SpecialDiet = specialDiet;
                    clsHH.Disabled = dissabled;
                    return true;
                }
            }
            return false;
        }

        public void RefreshHousehold()
        {
            clsHH.open(clsHH.ID);
        }
    }
}
