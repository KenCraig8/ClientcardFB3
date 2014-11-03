
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

namespace ClientcardFB3
{
    public class HHMembers
    {
        string connString;
        SqlCommandBuilder commBuilder = new SqlCommandBuilder();
        SqlDataAdapter datAdptHhM = new SqlDataAdapter();
        SqlDataAdapter datAdptDemo = new SqlDataAdapter();
        DataSet dset;
        SqlCommand command;
        SqlConnection conn;
        static string tblHhMName = "HouseholdMembers";
        static string tblDemoName = "Demographics";
        bool isValid;
        int iRowCountHhM = 0;
        int iRowCountDemo = 0;
        DataRow drowHhm = null;
        DataRow drowDemoGraphics = null;
        bool hasChanges = false;

        public HHMembers(string ConnString)
        {
            conn = new SqlConnection();
            connString = ConnString;
            conn.ConnectionString = connString;
            dset = new DataSet();
            isValid = false;
        }

        #region Get/Set Accessors [All]
        public bool HasChanges
        {
            get
            {
                return hasChanges;
            }
        }
        public DataRow DRowHhm
        {
            get
            {
                return drowHhm;
            }
            set
            {
                drowHhm = value;
            }
        }

        public DataSet DSet
        {
            get { return dset; }
            set { dset = value; }
        }

        public bool IsValid
        {
            get { return isValid; }
            set { isValid = value; }
        }

        public int RowCount
        {
            get { return iRowCountHhM; }
        }

        #region Get/Set Accessors [HouseholdMembers]

        public int ID
        {
            get { return Convert.ToInt32(drowHhm["ID"]); }
            set { drowHhm["ID"] = value; }
        }

        public bool Inactive
        {
            get { return (bool)drowHhm["Inactive"]; }
            set { drowHhm["Inactive"] = value; }
        }

        public int HouseholdID
        {
            get { return Convert.ToInt32(drowHhm["HouseholdID"]); }
            set { drowHhm["HouseholdID"] = value; }
        }

        public string LastName
        {
            get { return drowHhm["LastName"].ToString(); }
            set { drowHhm["LastName"] = value; }
        }

        public string FirstName
        {
            get { return drowHhm["FirstName"].ToString(); }
            set { drowHhm["FirstName"] = value; }
        }

        public string Sex
        {
            get { return drowHhm["Sex"].ToString(); }
            set { drowHhm["Sex"] = value; }
        }

        public DateTime Birthdate
        {
            get { return (DateTime)drowHhm["Birthdate"]; }
            set { drowHhm["Birthdate"] = value; }
        }

        public int AgeGroup
        {
            get { return Convert.ToInt32(drowHhm["AgeGroup"]); }
            set { drowHhm["AgeGroup"] = value; }
        }

        public bool SpecialDiet
        {
            get { return (bool)drowHhm["SpecialDiet"]; }
            set { drowHhm["SpecialDiet"] = value; }
        }

        public string Notes
        {
            get { return drowHhm["Notes"].ToString(); }
            set { drowHhm["Notes"] = value; }
        }

        public bool WorksInArea
        {
            get { return (bool)drowHhm["WorksInArea"]; }
            set { drowHhm["WorksInArea"] = value; }
        }

        public string Employer
        {
            get { return drowHhm["Employer"].ToString(); }
            set { drowHhm["Employer"] = value; }
        }

        public string EmpAddress
        {
            get { return drowHhm["EmpAddress"].ToString(); }
            set { drowHhm["EmpAddress"] = value; }
        }

        public string EmpCity
        {
            get { return drowHhm["EmpCity"].ToString(); }
            set { drowHhm["EmpCity"] = value; }
        }

        public string EmpZipcode
        {
            get { return drowHhm["EmpZipcode"].ToString(); }
            set { drowHhm["EmpZipcode"] = value; }
        }

        public string EmpPhone
        {
            get { return drowHhm["EmpPhone"].ToString(); }
            set { drowHhm["EmpPhone"] = value; }
        }

        public DateTime Created
        {
            get { return (DateTime)drowHhm["Created"]; }
            set { drowHhm["Created"] = value; }
        }

        public string CreatedBy
        {
            get { return drowHhm["CreatedBy"].ToString(); }
            set { drowHhm["CreatedBy"] = value; }
        }

        public DateTime Modified
        {
            get { return (DateTime)drowHhm["Modified"]; }
            set { drowHhm["Modified"] = value; }
        }

        public string ModifiedBy
        {
            get { return drowHhm["ModifiedBy"].ToString(); }
            set { drowHhm["ModifiedBy"] = value; }
        }

        public bool UserFlag0
        {
            get { return (bool)drowHhm["UserFlag0"]; }
            set { drowHhm["UserFlag0"] = value; }
        }

        public bool UserFlag1
        {
            get { return (bool)drowHhm["UserFlag1"]; }
            set { drowHhm["UserFlag1"] = value; }
        }

        public bool VolunteersAtFoodBank
        {
            get { return (bool)drowHhm["Volunteers"]; }
            set { drowHhm["Volunteers"] = value; }
        }

        public int Age
        {
            get { return Convert.ToInt32(drowHhm["Age"]); }
            set { drowHhm["Age"] = value; }
        }

        public bool UseAge
        {
            get { return (bool)drowHhm["UseAge"]; }
            set { drowHhm["UseAge"] = value; }
        }

        public bool NotIncludedInClientList
        {
            get { return (bool)drowHhm["NotIncludedInClientList"]; }
            set { drowHhm["NotIncludedInClientList"] = value; }
        }

        public bool CSFP
        {
            get { return (bool)drowHhm["CSFP"]; }
            set { drowHhm["CSFP"] = value; }
        }

        public bool HeadHH
        {
            get { return (bool)drowHhm["HeadHH"]; }
            set { drowHhm["HeadHH"] = value; }
        }

        public int Language
        {
            get { return Convert.ToInt32(drowHhm["Language"]); }
            set { drowHhm["Language"] = value; }
        }

        public DateTime CSFPExpiration
        {
            get { return (DateTime)drowHhm["CSFPExpiration"]; }
            set { drowHhm["CSFPExpiration"] = value; }
        }

        public bool HasCSFP
        {
            get
            {
                for (int i = 0; i < iRowCountHhM; i++)
                {
                    if (dset.Tables[0].Rows[i].Field<bool>("CSFP"))
                        return HasCSFP;
                }
                return false;
            }
        }
        #endregion
        #region Get/Set Accessors [Demographics]

        public int DemographicsId
        {
            get { return (int)drowDemoGraphics["Id"]; }
            set { drowDemoGraphics["Id"] = value; }
        }
        public bool HispanicLatino
        {
            get { return (bool)drowDemoGraphics["HispanicLatino"]; }
            set { drowDemoGraphics["HispanicLatino"] = value; }
        }
        public bool RefugeeImmigrant
        {
            get { return (bool)drowDemoGraphics["RefugeeImmigrant"]; }
            set { drowDemoGraphics["RefugeeImmigrant"] = value; }
        }
        public bool LimitedEnglish
        {
            get { return (bool)drowDemoGraphics["LimitedEnglish"]; }
            set { drowDemoGraphics["LimitedEnglish"] = value; }
        }
        public int MilitaryService
        {
            get { return Convert.ToInt32(drowDemoGraphics["MilitaryService"]); }
            set { drowDemoGraphics["MilitaryService"] = value; }
        }
        public int DischargeStatus
        {
            get { return Convert.ToInt32(drowDemoGraphics["DischargeStatus"]); }
            set { drowDemoGraphics["DischargeStatus"] = value; }
        }
        public bool PartneredMarried
        {
            get { return (bool)drowDemoGraphics["PartneredMarried"]; }
            set { drowDemoGraphics["PartneredMarried"] = value; }
        }
        public bool LongTermHomeless
        {
            get { return (bool)drowDemoGraphics["LongTermHomeless"]; }
            set { drowDemoGraphics["LongTermHomeless"] = value; }
        }
        public bool ChronicallyHomeless
        {
            get { return (bool)drowDemoGraphics["ChronicallyHomeless"]; }
            set { drowDemoGraphics["ChronicallyHomeless"] = value; }
        }
        public bool Employed
        {
            get { return (bool)drowDemoGraphics["Employed"]; }
            set { drowDemoGraphics["Employed"] = value; }
        }
        public int EmplolymentStatus
        {
            get { return Convert.ToInt32(drowDemoGraphics["EmplolymentStatus"]); }
            set { drowDemoGraphics["EmplolymentStatus"] = value; }
        }
        public int EducationLevel
        {
            get { return Convert.ToInt32(drowDemoGraphics["EducationLevel"]); }
            set { drowDemoGraphics["EducationLevel"] = value; }
        }

        #region Ethnicity Accessors
        public bool AmericanIndian
        {
            get { return (bool)drowDemoGraphics["AmericanIndian"]; }
            set { drowDemoGraphics["AmericanIndian"] = value; }
        }
        public bool AlaskaNative
        {
            get { return (bool)drowDemoGraphics["AlaskaNative"]; }
            set { drowDemoGraphics["AlaskaNative"] = value; }
        }
        public bool IndigenousToAmericas
        {
            get { return (bool)drowDemoGraphics["IndigenousToAmericas"]; }
            set { drowDemoGraphics["IndigenousToAmericas"] = value; }
        }
        public bool AsianIndian
        {
            get { return (bool)drowDemoGraphics["AsianIndian"]; }
            set { drowDemoGraphics["AsianIndian"] = value; }
        }
        public bool Cambodian
        {
            get { return (bool)drowDemoGraphics["Cambodian"]; }
            set { drowDemoGraphics["Cambodian"] = value; }
        }
        public bool Chinese
        {
            get { return (bool)drowDemoGraphics["Chinese"]; }
            set { drowDemoGraphics["Chinese"] = value; }
        }
        public bool Filipino
        {
            get { return (bool)drowDemoGraphics["Filipino"]; }
            set { drowDemoGraphics["Filipino"] = value; }
        }
        public bool Japanese
        {
            get { return (bool)drowDemoGraphics["Japanese"]; }
            set { drowDemoGraphics["Japanese"] = value; }
        }
        public bool Korean
        {
            get { return (bool)drowDemoGraphics["Korean"]; }
            set { drowDemoGraphics["Korean"] = value; }
        }
        public bool Vietnamese
        {
            get { return (bool)drowDemoGraphics["Vietnamese"]; }
            set { drowDemoGraphics["Vietnamese"] = value; }
        }
        public bool OtherAsian
        {
            get { return (bool)drowDemoGraphics["OtherAsian"]; }
            set { drowDemoGraphics["OtherAsian"] = value; }
        }
        public bool IndigenousAfricanBlack
        {
            get { return (bool)drowDemoGraphics["IndigenousAfricanBlack"]; }
            set { drowDemoGraphics["IndigenousAfricanBlack"] = value; }
        }
        public bool AfricanAmericanBlack
        {
            get { return (bool)drowDemoGraphics["AfricanAmericanBlack"]; }
            set { drowDemoGraphics["AfricanAmericanBlack"] = value; }
        }
        public bool OtherBlack
        {
            get { return (bool)drowDemoGraphics["OtherBlack"]; }
            set { drowDemoGraphics["OtherBlack"] = value; }
        }
        public bool HawaiianNative
        {
            get { return (bool)drowDemoGraphics["HawaiianNative"]; }
            set { drowDemoGraphics["HawaiianNative"] = value; }
        }
        public bool Polynesian
        {
            get { return (bool)drowDemoGraphics["Polynesian"]; }
            set { drowDemoGraphics["Polynesian"] = value; }
        }
        public bool Micronesian
        {
            get { return (bool)drowDemoGraphics["Micronesian"]; }
            set { drowDemoGraphics["Micronesian"] = value; }
        }
        public bool OtherPacificIslander
        {
            get { return (bool)drowDemoGraphics["OtherPacificIslander"]; }
            set { drowDemoGraphics["OtherPacificIslander"] = value; }
        }
        public bool ArabIranianMiddleEastern
        {
            get { return (bool)drowDemoGraphics["ArabIranianMiddleEastern"]; }
            set { drowDemoGraphics["ArabIranianMiddleEastern"] = value; }
        }
        public bool OtherWhiteCaucasian
        {
            get { return (bool)drowDemoGraphics["OtherWhiteCaucasian"]; }
            set { drowDemoGraphics["OtherWhiteCaucasian"] = value; }
        }
        public bool Other
        {
            get { return (bool)drowDemoGraphics["Other"]; }
            set { drowDemoGraphics["Other"] = value; }
        }
        public bool Unknown
        {
            get { return (bool)drowDemoGraphics["Unknown"]; }
            set { drowDemoGraphics["Unknown"] = value; }
        }
        #endregion
        #endregion
        #endregion

        #region Data Value Accsessors
        //-----------------------------DATA VALUE--------------------------------------------------------------------
        /// <summary>
        ///An Overloaded set of get/set funtions that will take in any kind of data value used in 
        ///the front end and accsess the data set for that data type, used mostly for a collection
        ///of textboxes so collection can be itterated through in one loop and have one funtion called
        ///no matter what type it actually refrenced
        ///Sets data value when value is a string
        /// </summary>
        /// <param name="FieldName">Fieldname=Collum Name in the Database</param>
        /// <param name="FieldValue">FieldValue= .Net Data type</param>
        public void SetDataValue(string FieldName, string FieldValue)
        {
            if (dset.Tables[tblHhMName].Columns.IndexOf(FieldName) > 0)
            {
                if (drowHhm[FieldName].ToString() != FieldValue)
                    hasChanges = true;

                drowHhm[FieldName] = FieldValue;
            }
            else if (dset.Tables[tblDemoName].Columns.IndexOf(FieldName) > 0)
                drowDemoGraphics[FieldName] = FieldValue;
        }

        /// <summary>
        /// Sets data value when value is a boolean value
        /// </summary>
        /// <param name="FieldName">Name of the field in the database</param>
        /// <param name="FieldValue">value to set</param>
        public void SetDataValue(string FieldName, bool FieldValue)
        {
            if (dset.Tables[tblHhMName].Columns.IndexOf(FieldName) >= 0)
            {
                if (Convert.ToBoolean(drowHhm[FieldName]) != FieldValue)
                    hasChanges = true;

                drowHhm[FieldName] = FieldValue;
            }
            else if (dset.Tables[tblDemoName].Columns.IndexOf(FieldName) >= 0)
                drowDemoGraphics[FieldName] = FieldValue;
        }

        //Gets data value from database using the selected data row
        public object GetDataValue(string FieldName)
        {
            if (dset.Tables[tblHhMName].Rows.Count > 0)
            {
                if (dset.Tables[tblHhMName].Columns.IndexOf(FieldName) >= 0)
                    return drowHhm[FieldName];
                else if (dset.Tables[tblDemoName].Columns.IndexOf(FieldName) >= 0)
                    return drowDemoGraphics[FieldName];
            }
            return "";
        }

        //Gets property through use of just the collum name in database
        public string GetDataString(string FieldName)
        {
            if (dset.Tables[tblHhMName].Rows.Count > 0)
            {
                int fldIndex = dset.Tables[tblHhMName].Columns.IndexOf(FieldName);
                if (fldIndex >= 0)
                {
                    if (dset.Tables[tblHhMName].Columns[fldIndex].DataType.Name == "DateTime")
                        if (drowHhm[FieldName].ToString() != "")
                        { return ((DateTime)drowHhm[FieldName]).ToShortDateString(); }
                        else
                        { return ""; }
                    else
                        return drowHhm[FieldName].ToString();
                }
                else
                {
                    fldIndex = dset.Tables[tblDemoName].Columns.IndexOf(FieldName);
                    if (fldIndex >= 0)
                    {
                        if (dset.Tables[tblDemoName].Columns[fldIndex].DataType.Name == "DateTime")
                            if (drowDemoGraphics[FieldName].ToString() != "")
                            { return ((DateTime)drowDemoGraphics[FieldName]).ToShortDateString(); }
                            else
                            { return ""; }
                        else
                            return drowDemoGraphics[FieldName].ToString();
                    }
                    return "";
                }
            }
            else
                return "";
        }

        #endregion

        public void find(int ID)
        {
            if (ID == 0)
            {
                if (iRowCountHhM >0 )
                    drowHhm = dset.Tables[tblHhMName].Rows[0];
                if (iRowCountDemo >0 )
                    drowDemoGraphics = dset.Tables[tblDemoName].Rows[0];
            }
            else
            {
                for (int i = 0; i < iRowCountHhM; i++)
                {
                    if (ID == dset.Tables[tblHhMName].Rows[i].Field<int>("ID"))
                    {
                        drowHhm = dset.Tables[tblHhMName].Rows[i];
                        break;
                    }
                }
                drowDemoGraphics = null;
                for (int i = 0; i < iRowCountDemo; i++)
                {
                    if (ID == dset.Tables[tblDemoName].Rows[i].Field<int>("ID"))
                    {
                        drowDemoGraphics = dset.Tables[tblDemoName].Rows[i];
                        break;
                    }
                }
                if (drowDemoGraphics == null)
                {
                    drowDemoGraphics = dset.Tables[tblDemoName].Rows.Add();
                }
            }
        }

        public int getHeadHH(int hhid)
        {
            SqlCommand sqlCmd = new SqlCommand("if Exists(SELECT * FROM HouseholdMembers WHERE HeadHH = 1 AND HouseholdID = " + hhid.ToString() + ")"
                              + " SELECT MAX(ID) FROM HouseholdMembers WHERE HeadHH = 1 AND HouseholdID = " + hhid.ToString() + " ELSE SELECT 0",conn);
            openConnection();
            int hhmid = (Int32)sqlCmd.ExecuteScalar();
            closeConnection();
            return hhmid;
        }

        public void MoveToHH(int idHHM, int newHHId)
        {
            SqlCommand sqlCmd = new SqlCommand("UPDATE HouseholdMembers SET HouseholdId = " + newHHId.ToString() + " WHERE ID = " + idHHM.ToString(), conn);
            openConnection();
            sqlCmd.ExecuteNonQuery();
            closeConnection();
        }

        public void rejectChanges()
        {
            drowHhm.RejectChanges();
            drowDemoGraphics.RejectChanges();
        }

        public void SetRecord(int rowIndex)
        {
            if (rowIndex >= 0 && rowIndex < iRowCountHhM)
            {
                drowHhm = dset.Tables[tblHhMName].Rows[rowIndex];
                drowDemoGraphics = dset.Tables[tblDemoName].Rows[rowIndex]; 
            }
        }

        public void cancelChanges()
        {
            drowHhm.CancelEdit();
            drowDemoGraphics.CancelEdit();
        }

        public bool open(int ID)
        {
            return openTables("SELECT * FROM " + tblHhMName + " WHERE ID=" + ID.ToString(),
                              "SELECT * FROM " + tblDemoName + " WHERE ID=" + ID.ToString());
        }

        public bool openTables(string sqlHhM, string sqlDemographics)
        {
            try
            {
                dset.Clear();
                datAdptHhM.SelectCommand = new SqlCommand(sqlHhM, conn);
                if (sqlDemographics != "")
                    datAdptDemo.SelectCommand = new SqlCommand(sqlDemographics, conn);

                drowHhm = null;
                drowDemoGraphics = null;
                hasChanges = false;
                openConnection();
                iRowCountHhM = datAdptHhM.Fill(dset, tblHhMName);
                if (sqlDemographics != "")
                    iRowCountDemo = datAdptDemo.Fill(dset, tblDemoName);
                else
                    iRowCountDemo = 0;
                closeConnection();
                if (iRowCountDemo > 0)
                    drowDemoGraphics = dset.Tables[tblDemoName].Rows[0];

                if (iRowCountHhM > 0)
                {
                    drowHhm = dset.Tables[tblHhMName].Rows[0];
                    return isValid = true;
                }
                return isValid = false;
            }
            catch (SqlException ex)
            {
                CCFBGlobal.appendErrorToErrorReport(sqlHhM.ToString(), ex.GetBaseException().ToString(),
                    CCFBGlobal.serverName);
                closeConnection();
                iRowCountHhM = 0;
                iRowCountDemo = 0;
                return isValid = false;
            }
        }

        public bool openWhere(string WhereClause, bool incldueDemoGraphics)
        {
            string sqlDemo = "";
            if (incldueDemoGraphics == true)
                sqlDemo = "SELECT * FROM " + tblDemoName + " WHERE Id IN (SELECT ID FROM " + tblHhMName + " " + WhereClause + ")";
            return openTables("SELECT * FROM " + tblHhMName + " " + WhereClause, sqlDemo);
        }

        public bool openHHID(int HHID)
        {
            return openTables("SELECT * FROM " + tblHhMName + " WHERE HouseholdID=" + HHID.ToString(),
                              "SELECT * FROM " + tblDemoName + " WHERE Id IN (SELECT ID FROM " + tblHhMName + " WHERE HouseholdId = " + HHID.ToString() + ")");
        }


        public void delete(System.Int32 ID)
        {
            openConnection();
            SqlCommand commDelete = new SqlCommand("DELETE FROM Demographics WHERE ID=" + ID.ToString(), conn);
            commDelete.ExecuteNonQuery();
            commDelete.CommandText = "DELETE FROM " + tblHhMName + " WHERE ID=" + ID.ToString();
            commDelete.ExecuteNonQuery();
            closeConnection();
        }

        public void deleteAllForHousehold(int HHID)
        {
            openConnection();
            SqlCommand sqlcmdDelete = new SqlCommand("DELETE FROM Demographics WHERE ID IN (SELECT ID FROM " + tblHhMName + " WHERE HouseholdId=" + HHID.ToString() + ")", conn);
            sqlcmdDelete.ExecuteNonQuery();
            sqlcmdDelete.CommandText = "DELETE FROM " + tblHhMName + " WHERE HouseholdID=" + HHID.ToString();
            sqlcmdDelete.ExecuteNonQuery();
            closeConnection();
        }

        public bool insertMember()
        {
            try
            {
                if (datAdptHhM.InsertCommand == null)
                {
                    SqlCommandBuilder commBuild = new SqlCommandBuilder(datAdptHhM);
                    datAdptHhM.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                }
                if (datAdptDemo.InsertCommand == null)
                {
                    SqlCommandBuilder commBldrDemog = new SqlCommandBuilder(datAdptDemo);
                    datAdptDemo.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                }

                openConnection();
                datAdptHhM.Update(dset, tblHhMName);
                datAdptDemo.Update(dset, tblDemoName);
                dset.Clear();
                datAdptHhM.Fill(dset, tblHhMName);
                datAdptDemo.Fill(dset, tblDemoName);
                closeConnection();
                return true;
            }
            catch (SqlException ex)
            {
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString(),
                    CCFBGlobal.serverName);
                closeConnection(); return false;
            }
        }

        public bool insertDemographics()
        {
            try
            {

                if (datAdptDemo.InsertCommand == null)
                {
                    SqlCommandBuilder commBuild1 = new SqlCommandBuilder(datAdptDemo);
                    datAdptDemo.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                }
                openConnection();
                datAdptDemo.Update(dset, tblDemoName);
                closeConnection();
                return true;
            }
            catch (SqlException ex)
            {
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString(),
                    CCFBGlobal.serverName);
                closeConnection(); return false;
            }
        }

        public bool update()
        {
            if (dset.HasChanges() == true)
            {
                try
                {
                    openConnection();
                    if (datAdptHhM.UpdateCommand == null)
                    {
                        SqlCommandBuilder commBuild = new SqlCommandBuilder(datAdptHhM);
                    }
                    if (datAdptDemo.UpdateCommand == null)
                    {
                        SqlCommandBuilder commBuild1 = new SqlCommandBuilder(datAdptDemo);
                    }
                    for (int i = 0; i < dset.Tables[tblHhMName].Rows.Count; i++)
                    {
                        DataRow drow = dset.Tables[tblHhMName].Rows[i];
                        if (drow.RowState != DataRowState.Unchanged)
                        {
                            drow["ModifiedBy"] = CCFBGlobal.currentUser_Name;
                            drow["Modified"] = DateTime.Now;
                        }
                    }
                    datAdptHhM.Update(dset, tblHhMName);
                    datAdptDemo.Update(dset.Tables[tblDemoName]);
                    closeConnection();
                    return true;
                }
                catch (SqlException ex)
                {
                    CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString(),
                    CCFBGlobal.serverName);
                    return false;
                }
            }
            return false;
        }

        private void openConnection()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
        }

        private void closeConnection()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        public int GetEFAPAgeGroup(int TestAge)
        {
            if (TestAge < 3)
            { return CCFBGlobal.ageGroup_Infant; }
            else if (TestAge < 13)
            { return CCFBGlobal.ageGroup_Youth; }
            else if (TestAge < 18)
            { return CCFBGlobal.ageGroup_Teen; }
            else if (TestAge == 18)
            { return CCFBGlobal.ageGroup_Eighteen; }
            else if (TestAge < 55)
            { return CCFBGlobal.ageGroup_Adult; }
            else
            { return CCFBGlobal.ageGroup_Senior; }
        }
    }
}

