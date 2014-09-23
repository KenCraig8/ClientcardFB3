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
        //Client Trx Structure
        public struct statsTrx
        {
            internal int NbrTrxThisMonth;
            internal int NbrTrxThisWeek;
            internal int NbrTEFAPThisMonth;
            internal int NbrSupplThisMonth;
        }

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
            clsHHSvcTrans = new TrxLog(connString,true,true,true,true);
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
            clsHHSvcTrans = new TrxLog(connString, true, true, true, true);
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
            get { return servedHHMemID; }
            set { servedHHMemID = value; }
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
            int iResult = (int)CCFBGlobal.getSQLValue("SELECT Count(*) FROM Household WHERE ID = " + HHID.ToString());
            return (iResult>0);
        }

        public bool householdMemberExists(string nameFirst, string nameLast)
        {
            int iResult = (int)CCFBGlobal.getSQLValue("SELECT Count(*) FROM HouseholdMembers WHERE LastName = '" + CCFBGlobal.SQLApostrophe(nameLast) + "' AND FirstName = '" + CCFBGlobal.SQLApostrophe(nameFirst) + "'");
            return (iResult > 0);
        }

        public bool open(int HHID, bool loadHHmem, bool loadSvcTrans)
        {
            bool toReturn = clsHH.open(HHID);

            if (loadHHmem == true)
                clsHHmem.openHHID(HHID);
            if (loadSvcTrans == true)
                clsHHSvcTrans.openForHH(HHID);

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
                command = new SqlCommand("SELECT m.*, h.Name, h.Address, h.AptNbr, h.City, "
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
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString());
                iRowCount = 0;
            }
        }

        public void openWhere(string whereClause)
        {
            try
            {
                command = new SqlCommand("SELECT h.ID HHId, m.*, h.Name, h.Address, h.AptNbr, h.City"
                    + ",h.Zipcode, h.Phone, h.ClientType, h.LatestService "
                    + ",CASE WHEN m.ID IS NULL THEN h.Name ELSE ltrim(rtrim(m.LastName)) + ', ' + ltrim(rtrim(m.FirstName)) END colNameLF"
                    + ",CASE WHEN m.ID IS NULL THEN h.Name ELSE ltrim(rtrim(m.FirstName)) + ' ' + ltrim(rtrim(m.LastName)) END colNameFL"
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
                CCFBGlobal.appendErrorToErrorReport("WhereClause="+whereClause, ex.GetBaseException().ToString());
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

        public void UpdateDataBasedOn(DateTime DateAsOf)
        {
            if (clsHH.UseFamilyList == true)
            {
                calcSpecialDietAndDissabled();
                calcAllHHMemAges(DateAsOf);
                clsHHmem.update(false);
                clsHH.update(false);
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
                    HHMemberItem clsHHMemberItem = new HHMemberItem(clsHHmem.DSet.Tables[0].Rows[i], clsHHmem.DSet.Tables[0].Columns, null, null);
                    int newAge = 0;
                    if (clsHHMemberItem.Inactive == false && clsHHMemberItem.NotCounted == false)
                    {
                        newAge = clsHHMemberItem.Age;
                        if (clsHHMemberItem.UseAge == false || (clsHHMemberItem.BirthDate.ToShortDateString() != CCFBGlobal.OURNULLDATE
                            && clsHHMemberItem.BirthDate.ToShortDateString() != CCFBGlobal.OUROTHERNULLDATE))
                        {
                            BirthDay = clsHHMemberItem.BirthDate;
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
                    HHMemberItem clsHHMemberItem = new HHMemberItem(clsHHmem.DSet.Tables[0].Rows[i], clsHHmem.DSet.Tables[0].Columns, null, null);
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

        public void getClientTrxStats(ref statsTrx trxStats, string hhId, string periodEnd)
        {
            trxStats.NbrTEFAPThisMonth = 0;
            trxStats.NbrSupplThisMonth = 0;
            trxStats.NbrTrxThisMonth = 0;
            trxStats.NbrTrxThisWeek = 0;
            
            if (hhId != "0")
            {
                SqlCommand sqlCmd = new SqlCommand("select SUM(case WHEN RcvdCommodity = 1 then 1 else 0 end) NbrTEFAP"
                            + ", SUM(case WHEN RcvdSupplemental = 1 THEN 1 else 0 end) NbrSuppl"
                            + ", COUNT(*) nbrSvcs"
                            + ", SUM(CASE WHEN DATEPART(week,TrxDate) = DATEPART(week,'" + periodEnd + "') THEN 1 ELSE 0 END) NbrThisWeek"
                            + " from TrxLog"
                            + " WHERE TrxStatus <2 AND TrxDate between '" + CCFBGlobal.firstDayOfMonth(periodEnd) + "' and '" + periodEnd + "'"
                            + " and HouseholdID = " + hhId, conn);
                try
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();        // Open the connection and execute the reader.

                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.Read())
                    {
                        if (! reader.IsDBNull(0) )
                            trxStats.NbrTEFAPThisMonth = reader.GetInt32(0);
                        if (!reader.IsDBNull(1))
                            trxStats.NbrSupplThisMonth = reader.GetInt32(1);
                        if (!reader.IsDBNull(2))
                            trxStats.NbrTrxThisMonth = reader.GetInt32(2);
                        if (!reader.IsDBNull(3))
                            trxStats.NbrTrxThisWeek = reader.GetInt32(3);
                    }
                }
                catch { };
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        public void RefreshHousehold()
        {
            try
            {
                clsHH.open(clsHH.ID);
            }
            catch (Exception ex)
            {
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString());
            }
        }

        public bool hasTrxLogEntry(String dateTrx)
        {
            int iResult = (int)CCFBGlobal.getSQLValue("SELECT Count(*) FROM TrxLog WHERE HouseholdID = " + clsHH.ID.ToString() + " AND TrxDate = '" + dateTrx + "'");
            return (iResult > 0);
        }

        public string svcWarning(String baseDate)
        {
            string result = "";
            DateTime date2 = Convert.ToDateTime(baseDate);
            int adjustment = Convert.ToInt32(date2.DayOfWeek);
            DateTime date1 = date2.AddDays(-adjustment);
            SqlCommand sqlCmd = new SqlCommand("SELECT HHMemID + ' served ' + DateName(dw,TrxDate) FROM TrxLog WHERE HouseholdID = " + clsHH.ID.ToString() + " AND TrxDate Between '" + date1.ToShortDateString() + "' AND '" + date2.ToShortDateString() + "'", conn);
            openConnection();
            SqlDataReader reader = sqlCmd.ExecuteReader();
            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    if (result != "")
                        result += "~";
                    result += reader.GetString(0);
                }
            }
            closeConnection();
            return result;
        }

        public string getFirstName()
        {
            string[] names;
            if (clsHH.Name.Contains(",") == true)
            {
                names = clsHH.Name.Trim().Split(',');
                if (names.Length > 1)
                {
                    return names[1].Trim();
                }
                return "...";
            }
            else
            {
                names = clsHH.Name.Split(' ');
                if (names.Length > 1)
                {
                    return names[0].Trim();
                }
            }
            return "---";
        }

        public string getLastName()
        {
            string[] names;
            if (clsHH.Name.Contains(",") == true)
            {
                names = clsHH.Name.Trim().Split(',');
                if (names.Length > 1)
                {
                    return names[0];
                }
                return "...";
            }
            else
            {
                string tmp = "";
                names = clsHH.Name.Split(' ');
                if (names.Length > 1)
                {
                    for (int i = 1; i < names.Length; i++)
                    {
                        tmp += names[i] + " ";
                    }
                    return tmp;
                }
            }
            return "---";
        }
    }
}
