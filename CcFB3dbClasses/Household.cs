
using System;
using System.Data;
using System.Data.SqlClient;

namespace ClientcardFB3
{
    public class Household : IDisposable
    {
        string connString;
        SqlDataAdapter dadAdpt;
        DataSet dset;
        SqlCommand command;
        SqlCommandBuilder commBuild;
        SqlConnection conn;
        static string tblName = "Household";
        bool isValid = false;
        int iRowCount = 0;
        DataRow drow;
        private bool _disposed;

        public Household(string ConnString)
        {
            conn = new SqlConnection();
            connString = ConnString;
            conn.ConnectionString = connString;
            dset = new DataSet();
            dadAdpt = new SqlDataAdapter();
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
                    if (dset != null)
                        dset.Dispose();
                    if (command != null)
                        command.Dispose();
                    if (commBuild != null)
                        commBuild.Dispose();
                    if (dadAdpt != null)
                        dadAdpt.Dispose();
                }

                // Indicate that the instance has been disposed.
                conn = null;
                dset = null;
                command = null;
                dadAdpt = null;
                _disposed = true;
            }
        }

        #region Get/Set Accessors

        public bool ISValid
        {
            get { return isValid; }
            set { isValid = value; }
        }
        public DataSet DSet
        {
            get { return dset; }
            set { dset = value; }
        }
        public int RowCount
        {
            get { return iRowCount; }
        }
        public int ID
        {
            get { return Convert.ToInt32(drow["ID"]); }
            set { drow["ID"] = value; }
        }
        public bool Inactive
        {
            get { return (bool)drow["Inactive"]; }
            set { drow["Inactive"] = value; }
        }
        public string Name
        {
            get { return drow["Name"].ToString(); }
            set { drow["Name"] = value; }
        }
        public string Address
        {
            get { return drow["Address"].ToString(); }
            set { drow["Address"] = value; }
        }
        public string AptNbr
        {
            get { return drow["AptNbr"].ToString(); }
            set { drow["AptNbr"] = value; }
        }
        public string City
        {
            get { return drow["City"].ToString(); }
            set { drow["City"] = value; }
        }
        public string State
        {
            get { return drow["State"].ToString(); }
            set { drow["State"] = value; }
        }
        public string Zipcode
        {
            get { return drow["Zipcode"].ToString(); }
            set { drow["Zipcode"] = value; }
        }
        public string Phone
        {
            get { return drow["Phone"].ToString(); }
            set { drow["Phone"] = value; }
        }
        public int PhoneType
        {
            get { return Convert.ToInt32(drow["PhoneType"]); }
            set { drow["PhoneType"] = value; }
        }
        public int Infants
        {
            get { return Convert.ToInt32(drow["Infants"]); }
            set { drow["Infants"] = value; }
        }
        public int Youth
        {
            get { return Convert.ToInt32(drow["Youth"]); }
            set { drow["Youth"] = value; }
        }
        public int Teens
        {
            get { return Convert.ToInt32(drow["Teens"]); }
            set { drow["Teens"] = value; }
        }
        public int Eighteens
        {
            get { return Convert.ToInt32(drow["Eighteen"]); }
            set { drow["Eighteen"] = value; }
        }
        public int Adults
        {
            get { return Convert.ToInt32(drow["Adults"]); }
            set { drow["Adults"] = value; }
        }
        public int Seniors
        {
            get { return Convert.ToInt32(drow["Seniors"]); }
            set { drow["Seniors"] = value; }
        }
        public int TotalFamily
        {
            get { return Convert.ToInt32(drow["TotalFamily"]); }
            set { drow["TotalFamily"] = value; }
        }
        public int SpecialDiet
        {
            get { return Convert.ToInt32(drow["SpecialDiet"]); }
            set { drow["SpecialDiet"] = value; }
        }
        public bool IncludeOnLog
        {
            get { return (bool)drow["IncludeOnLog"]; }
            set { drow["IncludeOnLog"] = value; }
        }
        public bool NeedCommoditySignature
        {
            get { return (bool)drow["NeedCommoditySignature"]; }
            set { drow["NeedCommoditySignature"] = value; }
        }
        public DateTime TEFAPSignDate
        {
            get { return Convert.ToDateTime(drow["TEFAPSignDate"]); }
            set { drow["TEFAPSignDate"] = value; }
        }
        public bool UseFamilyList
        {
            get { return (bool)drow["UseFamilyList"]; }
            set { drow["UseFamilyList"] = value; }
        }
        public int EthnicSpeaking
        {
            get { return Convert.ToInt32(drow["EthnicSpeaking"]); }
            set { drow["EthnicSpeaking"] = value; }
        }
        public string Comments
        {
            get { return drow["Comments"].ToString(); }
            set { drow["Comments"] = value; }
        }
        public bool AutoAlert
        {
            get { return (bool)drow["AutoAlert"]; }
            set { drow["AutoAlert"] = value; }
        }
        public DateTime Created
        {
            get { return CCFBGlobal.ValidDate(drow["Created"]); }
            set { drow["Created"] = value; }
        }
        public string CreatedBy
        {
            get { return drow["CreatedBy"].ToString(); }
            set { drow["CreatedBy"] = value; }
        }
        public DateTime Modified
        {
            get { return CCFBGlobal.ValidDate(drow["Modified"]); }
            set { drow["Modified"] = value; }
        }
        public string ModifiedBy
        {
            get { return drow["ModifiedBy"].ToString(); }
            set { drow["ModifiedBy"] = value; }
        }
        public DateTime FirstService
        {
            get { return CCFBGlobal.ValidDate(drow["FirstService"]); }
            set { drow["FirstService"] = value; }
        }
        public DateTime LatestService
        {
            get { return CCFBGlobal.ValidDate(drow["LatestService"]); }
            set { drow["LatestService"] = value; }
        }
        public DateTime LastCommodityService
        {
            get { return CCFBGlobal.ValidDate(drow["LastCommodityService"]); }
            set { drow["LastCommodityService"] = value; }
        }
        public DateTime FirstSvcThisYear
        {
            get { return CCFBGlobal.ValidDate(drow["FirstSvcThisYear"]); }
            set { drow["FirstSvcThisYear"] = value; }
        }
        public bool SecondServiceThisMonth
        {
            get { return (bool)drow["SecondServiceThisMonth"]; }
            set { drow["SecondServiceThisMonth"] = value; }
        }
        public float UserNum0
        {
            get { return (float)drow["UserNum0"]; }
            set { drow["UserNum0"] = value; }
        }
        public float UserNum1
        {
            get { return (float)drow["UserNum1"]; }
            set { drow["UserNum1"] = value; }
        }
        public bool UserFlag0
        {
            get { return (bool)drow["UserFlag0"]; }
            set { drow["UserFlag0"] = value; }
        }
        public bool UserFlag1
        {
            get { return (bool)drow["UserFlag1"]; }
            set { drow["UserFlag1"] = value; }
        }
        public bool UserFlag2
        {
            get { return (bool)drow["UserFlag2"]; }
            set { drow["UserFlag2"] = value; }
        }
        public bool UserFlag3
        {
            get { return (bool)drow["UserFlag3"]; }
            set { drow["UserFlag3"] = value; }
        }
        public bool UserFlag4
        {
            get { return (bool)drow["UserFlag4"]; }
            set { drow["UserFlag4"] = value; }
        }
        public bool UserFlag5
        {
            get { return (bool)drow["UserFlag5"]; }
            set { drow["UserFlag5"] = value; }
        }
        public bool UserFlag6
        {
            get { return (bool)drow["UserFlag6"]; }
            set { drow["UserFlag6"] = value; }
        }
        public bool UserFlag7
        {
            get { return (bool)drow["UserFlag7"]; }
            set { drow["UserFlag7"] = value; }
        }
        public bool UserFlag8
        {
            get { return (bool)drow["UserFlag8"]; }
            set { drow["UserFlag8"] = value; }
        }
        public bool UserFlag9
        {
            get { return (bool)drow["UserFlag9"]; }
            set { drow["UserFlag9"] = value; }
        }
        public int ClientType
        {
            get { return Convert.ToInt32(drow["ClientType"]); }
            set { drow["ClientType"] = value; }
        }
        public bool NoCommodities
        {
            get { return (bool)drow["NoCommodities"]; }
            set { drow["NoCommodities"] = value; }
        }
        public bool NeedToVerifyID
        {
            get { return (bool)drow["NeedToVerifyID"]; }
            set { drow["NeedToVerifyID"] = value; }
        }
        public DateTime DateIDVerified
        {
            get 
            {
                if (String.IsNullOrEmpty(drow["DateIDVerified"].ToString()) == true)
                    return CCFBGlobal.FBNullDateValue;
                else
                    return (DateTime)drow["DateIDVerified"];
            }
            set { drow["DateIDVerified"] = value; }
        }
        public bool SupplOnly
        {
            get { return (bool)drow["SupplOnly"]; }
            set { drow["SupplOnly"] = value; }
        }
        public int IdType
        {
            get { return Convert.ToInt32(drow["IdType"]); }
            set { drow["IdType"] = value; }
        }
        public int Disabled
        {
            get { return Convert.ToInt32(drow["Disabled"]); }
            set { drow["Disabled"] = value; }
        }
        public bool InCityLimits
        {
            get { return (bool)drow["InCityLimits"]; }
            set { drow["InCityLimits"] = value; }
        }
        public bool Homeless
        {
            get { return (bool)drow["Homeless"]; }
            set { drow["Homeless"] = value; }
        }
        
        public int AnnualIncome
        {
            get { return (int)drow["AnnualIncome"]; }
            set { drow["AnnualIncome"] = value; }
        }
        public bool NeedIncomeVerification
        {
            get { return (bool)drow["NeedIncomeVerification"]; }
            set { drow["NeedIncomeVerification"] = value; }
        }
        public DateTime IncomeVerifiedDate
        {
            get { return Convert.ToDateTime(drow["IncomeVerifiedDate"]); }
            set { drow["IncomeVerifiedDate"] = value; }
        }
        public int NbrCSFP
        {
            get { return Convert.ToInt32(drow["NbrCSFP"]); }
            set { drow["NbrCSFP"] = value; }
        }
        public bool BabyServices
        {
            get { return (bool)drow["BabyServices"]; }
            set { drow["BabyServices"] = value; }
        }
        public string BabySvcDescr
        {
            get { return (string)drow["BabySvcDescr"]; }
            set { drow["BabySvcDescr"] = value; }
        }
        public bool SurveyComplete
        {
            get { return (bool)drow["SurveyComplete"]; }
            set { drow["SurveyComplete"] = value; }
        }
        public bool SingleHeadHh
        {
            get { return (bool)drow["SingleHeadHh"]; }
            set { drow["SingleHeadHh"] = value; }
        }
        public int BarCode
        {
            get { return Convert.ToInt32(drow["BarCode"]); }
            set { drow["BarCode"] = value; }
        }
        public int ServiceMethod
        {
            get { return Convert.ToInt32(drow["ServiceMethod"]); }
            set { drow["ServiceMethod"] = value; }
        }
        public int HDRoute
        {
            get { return Convert.ToInt32(drow["HDRoute"]); }
            set { drow["HDRoute"] = value; }
        }
        public int HUDCategory
        {
            get { return Convert.ToInt32(drow["HUDCategory"]); }
            set { drow["HUDCategory"] = value; }
        }
        public int HDItem
        {
            get { return Convert.ToInt32(drow["HDItem"]); }
            set { drow["HDItem"] = value; }
        }
        public string DriverNotes
        {
            get { return drow["DriverNotes"].ToString(); }
            set { drow["DriverNotes"] = value; }
        }
        public string AlertText
        {
            get { return drow["AlertText"].ToString(); }
            set { drow["AlertText"] = value; }
        }
        public DateTime FirstCalService
        {
            get
            {
                if (String.IsNullOrEmpty(drow["FirstCalService"].ToString()) == true)
                    return CCFBGlobal.FBNullDateValue;
                else
                    return (DateTime)drow["FirstCalService"];
            }
            set { drow["FirstCalService"] = value; }
        }
        public DateTime LastSupplService
        {
            get
            {
                if (String.IsNullOrEmpty(drow["LastSupplService"].ToString()) == true)
                    return CCFBGlobal.FBNullDateValue;
                else
                    return (DateTime)drow["LastSupplService"];
            }
            set { drow["LastSupplService"] = value; }
        }
        public int Transportation
        {
            get { return Convert.ToInt32(drow["Transportation"]); }
            set { drow["Transportation"] = value; }
        }
        #region School Supply Fields
        public string SchSupplyPickupPerson
        {
            get { return drow["SchSupplyPickupPerson"].ToString(); }
            set { drow["SchSupplyPickupPerson"] = value; }
        }
        public DateTime SchSupplyRegDate
        {
            get
            {
                if (String.IsNullOrEmpty(drow["SchSupplyRegDate"].ToString()) == true)
                    return CCFBGlobal.FBNullDateValue;
                else
                    return (DateTime)drow["SchSupplyRegDate"];
            }
            set { drow["SchSupplyRegDate"] = value; }
        }
        public bool SchSupplyFlag
        {
            get { return (bool)drow["SchSupplyFlag"]; }
            set { drow["SchSupplyFlag"] = value; }
        }
        public int SchSupplyRegistration
        {
            get { return Convert.ToInt32(drow["SchSupplyRegistration"]); }
            set { drow["SchSupplyRegistration"] = value; }
        }
        #endregion
        #region Christmas Assistance Program Fields
        public bool CAFlag
        {
            get { return (bool)drow["CAFlag"]; }
            set { drow["CAFlag"] = value; }
        }
        public DateTime CAApplicationDate
        {
            get
            {
                if (drow["CAApplicationDate"].ToString() == "")
                    return CCFBGlobal.FBNullDateValue;
                else
                    return (DateTime)drow["CAApplicationDate"];
            }
            set { drow["CAApplicationDate"] = value; }
        }
        public DateTime CADBInputDate
        {
            get
            {
                if (drow["CADBInputDate"].ToString() == "")
                    return CCFBGlobal.FBNullDateValue;
                else
                    return (DateTime)drow["CADBInputDate"];
            }
            set { drow["CADBInputDate"] = value; }
        }
        public DateTime CAAdoptedDate
        {
            get
            {
                if (drow["CAAdoptedDate"].ToString() == "")
                    return CCFBGlobal.FBNullDateValue;
                else
                    return (DateTime)drow["CAAdoptedDate"];
            }
            set { drow["CAAdoptedDate"] = value; }
        }
        public DateTime CAFilledDate
        {
            get
            {
                if (drow["CAFilledDate"].ToString() == "")
                    return CCFBGlobal.FBNullDateValue;
                else
                    return (DateTime)drow["CAFilledDate"];
            }
            set { drow["CAFilledDate"] = value; }
        }
        public int CAAdoptedBy
        {
            get { return Convert.ToInt32(drow["CAAdoptedBy"]); }
            set { drow["CAAdoptedBy"] = value; }
        }
        public string CAAdoptedContactName
        {
            get { return drow["CAAdoptedContactName"].ToString(); }
            set { drow["CAAdoptedContactName"] = value; }
        }
        public string CAAdoptedContactPhone
        {
            get { return drow["CAAdoptedContactPhone"].ToString(); }
            set { drow["CAAdoptedContactPhone"] = value; }
        }
        public bool CAFoodBoxOnly
        {
            get { return (bool)drow["CAFoodBoxOnly"]; }
            set { drow["CAFoodBoxOnly"] = value; }
        }
        public bool CAHasPickupInfo
        {
            get { return (bool)drow["CAHasPickupInfo"]; }
            set { drow["CAHasPickupInfo"] = value; }
        }
        public bool CAFoodBoxRequest
        {
            get { return (bool)drow["CAFoodBoxRequest"]; }
            set { drow["CAFoodBoxRequest"] = value; }
        }
        public bool CAReceived
        {
            get { return (bool)drow["CAReceived"]; }
            set { drow["CAReceived"] = value; }
        }
        public int CASignedByID
        {
            get { return Convert.ToInt32(drow["CASignedByID"]); }
            set { drow["CASignedByID"] = value; }
        }
        #endregion

        #endregion

        //-----------------------------DATA VALUE--------------------------------------------------------------------
        /// <summary>
        ///An Overloaded set of get/set funtions that will take in any kind of data value used in 
        ///the front end and accsess the data set for that data type, used mostly for a collection
        ///of textboxes so collection can be itterated through in one loop and have one funtion called
        ///no matter what type it actually refrenced
        /// </summary>
        /// <param name="FieldName">Fieldname=Collum Name in the Database</param>
        /// <param name="FieldValue">FieldValue= .Net Data type</param>

        #region Data Value Accsessors
        //Sets data value when value is a string
        public void SetDataValue(string FieldName, string FieldValue)
        {
            if (dset.Tables[tblName].Columns[FieldName].DataType.ToString() == "System.DateTime")
            {
                try
                {
                    Convert.ToDateTime(FieldValue);
                    drow[FieldName] = FieldValue;
                }
                catch (Exception)
                {
                    drow[FieldName] = CCFBGlobal.OURNULLDATE;
                }
            }
            else
            {
                drow[FieldName] = FieldValue;
            }
        }

        public void SetDataValue(string FieldName, bool FieldValue)
        {
            drow[FieldName] = FieldValue;
        }
        
        //Gets property through use of just the collum name in database
        public object GetDataValue(string FieldName)
        {
            if (FieldName.Length >0)
            {
                if (dset.Tables[tblName].Columns[FieldName] != null)
                {
                    try
                    {
                        return drow[FieldName];
                    }
                    catch
                    {
                    }
                }
                else
                {
                    return "";
                }
                if (dset.Tables[tblName].Columns[FieldName].DataType.ToString() == "System.Integer")
                    return 0;
                else
                    return "";
            }
            return "";
        }

        public string GetDataString(string FieldName)
        {
            if (FieldName.Length >0)
                try
                {
                    if (dset.Tables[0].Columns[FieldName].DataType == System.Type.GetType("System.DateTime"))
                    {
                        return CCFBGlobal.ValidDateString(drow[FieldName]);
                    }
                    else if (dset.Tables[0].Columns[FieldName].DataType == System.Type.GetType("System.Boolean"))
                        if (drow[FieldName] != null && drow[FieldName].ToString() !="")
                            return drow[FieldName].ToString();
                        else
                            return "false";
                    else
                        return drow[FieldName].ToString();
                }
                catch { return ""; }
            else
                return "";
        }

        #endregion

        /// <summary>
        /// Opens a single household
        /// </summary>
        /// <param name="ID">key=ID</param>
        /// <returns>Returns if DataSet has an entry or not</returns>
        public int getIdFromBarCode(int barcode)
        {
            object id = CCFBGlobal.getSQLValue("SELECT ID FROM " + tblName + " WHERE BarCode=" + barcode.ToString());
            if (id != null)
            {
                return (int)id;
            }
            return 0;
        }

        /// <summary>
        /// Opens a single household
        /// </summary>
        /// <param name="ID">key=ID</param>
        /// <returns>Returns if DataSet has an entry or not</returns>
        public bool open(int ID)
        {
            try
            {
                openConnection();
                command = new SqlCommand("SELECT * FROM " + tblName + " WHERE ID=" + ID.ToString(), conn);
                dadAdpt.SelectCommand = command;
                dset.Clear();
                iRowCount = dadAdpt.Fill(dset, tblName);
                closeConnection();

                if (iRowCount > 0)
                {
                    drow = dset.Tables[tblName].Rows[0];
                    return isValid = true;
                }
                else
                    return isValid = false;
            }
            catch (SqlException ex) 
            {
                CCFBGlobal.appendErrorToErrorReport("ID="+ID.ToString(), ex.GetBaseException().ToString());
                closeConnection();
                iRowCount = 0;
                return isValid = false; 
            }
        }

        ///// <summary>
        ///// Opens households that meet certian criteria
        ///// </summary>
        ///// <param name="whereClause">SQL Where clause</param>
        public void openWhere(string whereClause)
        {
            try
            {
                openConnection();
                command = new SqlCommand("SELECT * FROM " + tblName + " Where " + whereClause + " Order By Name ASC", conn);
                dadAdpt.SelectCommand = command;
                dset.Clear();
                iRowCount = dadAdpt.Fill(dset, tblName);
                closeConnection();
                isValid = false;
                if (iRowCount > 0)
                    drow = dset.Tables[tblName].Rows[0];
            }
            catch (SqlException ex) 
            { 
                closeConnection();
                CCFBGlobal.appendErrorToErrorReport("WhereClause="+whereClause, ex.GetBaseException().ToString());
                iRowCount = 0;
            }
        }

        ///// <summary>
        ///// Opens households that meet certian criteria
        ///// </summary>
        ///// <param name="whereClause">SQL Where clause</param>
        ///// <param name="orderBYClause">SQL Order By clause</param>
        public void openWhere(string whereClause, String orderByClause)
        {
            try
            {
                openConnection();
                command = new SqlCommand("SELECT * FROM " + tblName + " Where " + whereClause + " Order By " + orderByClause, conn);
                dadAdpt.SelectCommand = command;
                dset.Clear();
                iRowCount = dadAdpt.Fill(dset, tblName);
                closeConnection();
                isValid = false;
                if (iRowCount > 0)
                    drow = dset.Tables[tblName].Rows[0];
            }
            catch (SqlException ex)
            {
                closeConnection();
                CCFBGlobal.appendErrorToErrorReport("WhereClause=" + whereClause + "  orderByClause=" + orderByClause, ex.GetBaseException().ToString());
                iRowCount = 0;
            }
        }

        /// <summary>
        /// Gets the ditinct values for any collumn
        /// </summary>
        /// <param name="columnName">Collumn name</param>
        public void getDistincts(string columnName, string whereClause)
        {
            try
            {
                openConnection();
                command = new SqlCommand("SELECT " + columnName + ", COUNT(*) FROM " 
                    + tblName + whereClause
                    + " Group By " + columnName + " Order By " + columnName, conn);
                dadAdpt.SelectCommand = command;
                dset.Clear();
                dset.Tables.Clear();
                iRowCount = dadAdpt.Fill(dset, tblName);
                closeConnection();
                isValid = false;
                if (iRowCount > 0)
                    drow = dset.Tables[tblName].Rows[0];
            }
            catch (SqlException ex) 
            {
                closeConnection();
                CCFBGlobal.appendErrorToErrorReport("columnName="+ columnName, ex.GetBaseException().ToString());
                iRowCount = 0;
            }
        }

        public int getHouseholdID(string name)
        {
            try
            {
                openConnection();
                command = new SqlCommand("SELECT * FROM " + tblName + " Where Name='" + name + "'", conn);
                dadAdpt.SelectCommand = command;
                dset.Clear();
                iRowCount = dadAdpt.Fill(dset, tblName);
                closeConnection();
                isValid = false;
                if (iRowCount > 0)
                {
                    drow = dset.Tables[tblName].Rows[0];
                    return dset.Tables[0].Rows[0].Field<int>("ID");
                }
                else
                    return -1;
            }
            catch (SqlException ex) 
            {
                closeConnection();
                CCFBGlobal.appendErrorToErrorReport("name="+name, ex.GetBaseException().ToString());
                iRowCount = 0;
                return -1; 
            }
        }

        /// <summary>
        /// Deletes Household from the database
        /// </summary>
        /// <param name="key">ID</param>
        public void delete(System.Int32 ID)
        {
            SqlCommand commDelete = new SqlCommand(" DELETE FROM " + tblName + " WHERE ID=" + ID.ToString(), conn);
            openConnection();
            commDelete.ExecuteNonQuery();
            commDelete.Dispose();
            closeConnection();
        }

        public bool insert()
        {
            if (dadAdpt.UpdateCommand == null || dadAdpt.InsertCommand == null)
            {
                commBuild = new SqlCommandBuilder(dadAdpt);
            }
            try
            {
                openConnection();
                dadAdpt.Update(dset, tblName);
                closeConnection();
                return true;
            }
            catch (SqlException ex)
            {
                closeConnection();
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString());
                return false; 
            }
        }

        /// <summary>
        /// Updates the hosuehold in the database with any changes made to the DataSet
        /// </summary>
        public void update(bool changeModified)
        {
            if (dset.HasChanges() == true)
            {
                try
                {
                    if (drow.RowState != DataRowState.Unchanged && changeModified == true)
                    {
                        drow["ModifiedBy"] = CCFBGlobal.dbUserName;
                        drow["Modified"] = DateTime.Now;
                    }
                    if (dadAdpt.UpdateCommand == null)
                    {
                        SqlCommandBuilder commBuild = new SqlCommandBuilder(dadAdpt);
                    }

                    openConnection();
                   
                    dadAdpt.Update(dset, "Household");
                    closeConnection();
                }
                catch (SqlException ex) 
                {
                    closeConnection();
                    CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString());
                }
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

        public void SetRecord(int rowIndex)
        {
            if (rowIndex >= 0 && rowIndex < iRowCount)
            { drow = dset.Tables[tblName].Rows[rowIndex]; }
        }

        public int HHMembersCount(int HHID)
        {
            SqlCommand cmdQuery = new SqlCommand("HouseholdMembersCount",conn);
            cmdQuery.CommandType = CommandType.StoredProcedure;
            cmdQuery.Parameters.AddWithValue("@HHID", HHID);
            cmdQuery.Parameters.AddWithValue("@NbrRows",0);
            cmdQuery.Parameters[1].Direction = ParameterDirection.Output;
            conn.Open();
            cmdQuery.ExecuteNonQuery();
            conn.Close();
            int retValue = Convert.ToInt32(cmdQuery.Parameters[1].Value.ToString());
            cmdQuery.Dispose();
            return retValue;
;
        }
       
        public void UpdateLatestServiceDates(string dateOfService)
        {
            SqlCommand cmdUpdate = new SqlCommand();
            cmdUpdate.CommandType = CommandType.StoredProcedure;
            cmdUpdate.Connection = conn;
            openConnection();
            try
            {
                cmdUpdate.CommandText = "UpdateHouseholdTrxDates";
                cmdUpdate.Parameters.AddWithValue("@HHId", ID);
                cmdUpdate.Parameters.AddWithValue("@LowDate", CCFBGlobal.CurrentFiscalStartDate().ToShortDateString());
                cmdUpdate.Parameters.AddWithValue("@HiDate", CCFBGlobal.CurrentFiscalEndDate().ToShortDateString());
                cmdUpdate.Parameters.AddWithValue("@ServiceDate", dateOfService);
                cmdUpdate.ExecuteNonQuery();
                if (DateTime.Compare(FirstService, DateTime.Parse(dateOfService)) > 0 || DateTime.Compare(FirstService, CCFBGlobal.FBNullDateValue) <= 0)
                {
                    cmdUpdate = new SqlCommand("UpdateHouseholdFirstServiceDate", conn);
                    cmdUpdate.CommandType = CommandType.StoredProcedure;
                    cmdUpdate.Parameters.Add(new SqlParameter("@HHID", ID));
                    cmdUpdate.ExecuteNonQuery();
                }
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
            cmdUpdate.Dispose();
            closeConnection();
        }
    }
}

