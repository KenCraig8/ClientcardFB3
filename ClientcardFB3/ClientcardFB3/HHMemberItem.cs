using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace ClientcardFB3
{
    public class HHMemberItem
    {
        DataRow drowHhMItem = null;
        DataRow drowItemDemographics = null;
        DataColumnCollection dcolHhM = null;
        DataColumnCollection dcolDemographics = null;

        Boolean bDataChanged = false;

        public HHMemberItem(DataRow drowInHHM, DataColumnCollection dcolInHhM, DataRow drowInDemographics, DataColumnCollection dcolInDemographics)
        {
            drowHhMItem = drowInHHM;
            dcolHhM = dcolInHhM;
            drowItemDemographics = drowInDemographics;
            dcolDemographics = dcolInDemographics;
            bDataChanged = false;
        }

        public DataRow DRowHhM
        {
            get { return drowHhMItem; }
            set { drowHhMItem = value; }
        }

        public DataRow DRowDemographics
        {
            get { return drowItemDemographics; }
            set { drowItemDemographics = value; }
        }

        #region Get/Set Accessors HouseholdMembers

        public int ID
        {
            get { return Convert.ToInt32(drowHhMItem["ID"]); }
            set { drowHhMItem["ID"] = value; }
        }
        public bool Inactive
        {
            get { return (bool)drowHhMItem["Inactive"]; }
            set { drowHhMItem["Inactive"] = value; }
        }
        public int HouseholdID
        {
            get { return Convert.ToInt32(drowHhMItem["HouseholdID"]); }
            set { drowHhMItem["HouseholdID"] = value; }
        }
        public string LastName
        {
            get { return drowHhMItem["LastName"].ToString(); }
            set { drowHhMItem["LastName"] = value; }
        }
        public string FirstName
        {
            get { return drowHhMItem["FirstName"].ToString(); }
            set { drowHhMItem["FirstName"] = value; }
        }
        public string Sex
        {
            get { return drowHhMItem["Sex"].ToString(); }
            set { drowHhMItem["Sex"] = value; }
        }
        public DateTime BirthDate
        {
            get
            {
                try
                {
                    return Convert.ToDateTime(drowHhMItem["Birthdate"]);
                }
                catch { return CCFBGlobal.FBNullDateValue; }
            }
            set { drowHhMItem["Birthdate"] = value; }
        }
        public int AgeGroup
        {
            get { return Convert.ToInt32(drowHhMItem["AgeGroup"]); }
            set { drowHhMItem["AgeGroup"] = value; }
        }
        public bool SpecialDiet
        {
            get { return (bool)drowHhMItem["SpecialDiet"]; }
            set { drowHhMItem["SpecialDiet"] = value; }
        }
        public string Notes
        {
            get { return drowHhMItem["Notes"].ToString(); }
            set { drowHhMItem["Notes"] = value; }
        }
        public bool WorksInArea
        {
            get { return (bool)drowHhMItem["WorksInArea"]; }
            set { drowHhMItem["WorksInArea"] = value; }
        }
        public string Employer
        {
            get { return drowHhMItem["Employer"].ToString(); }
            set { drowHhMItem["Employer"] = value; }
        }
        public string EmpAddress
        {
            get { return drowHhMItem["EmpAddress"].ToString(); }
            set { drowHhMItem["EmpAddress"] = value; }
        }
        public string EmpCity
        {
            get { return drowHhMItem["EmpCity"].ToString(); }
            set { drowHhMItem["EmpCity"] = value; }
        }
        public string EmpZipcode
        {
            get { return drowHhMItem["EmpZipcode"].ToString(); }
            set { drowHhMItem["EmpZipcode"] = value; }
        }
        public string EmpPhone
        {
            get { return drowHhMItem["EmpPhone"].ToString(); }
            set { drowHhMItem["EmpPhone"] = value; }
        }
        public DateTime Created
        {
            get { return Convert.ToDateTime(drowHhMItem["Created"]); }
            set { drowHhMItem["Created"] = value; }
        }
        public string CreatedBy
        {
            get { return drowHhMItem["CreatedBy"].ToString(); }
            set { drowHhMItem["CreatedBy"] = value; }
        }
        public DateTime Modified
        {
            get
            {
                if (drowHhMItem["Modified"].ToString() == "")
                {
                    return CCFBGlobal.FBNullDateValue;
                }
                else
                {
                    return Convert.ToDateTime(drowHhMItem["Modified"]);
                }
            }
            set { drowHhMItem["Modified"] = value; }
        }
        public string ModifiedBy
        {
            get { return drowHhMItem["ModifiedBy"].ToString(); }
            set { drowHhMItem["ModifiedBy"] = value; }
        }
        public bool UserFlag0
        {
            get { return (bool)drowHhMItem["UserFlag0"]; }
            set { drowHhMItem["UserFlag0"] = value; }
        }
        public bool UserFlag1
        {
            get { return (bool)drowHhMItem["UserFlag1"]; }
            set { drowHhMItem["UserFlag1"] = value; }
        }
        public bool VolunteersAtFoodBank
        {
            get { return (bool)drowHhMItem["VolunteersAtFoodBank"]; }
            set { drowHhMItem["VolunteersAtFoodBank"] = value; }
        }
        public int Age
        {
            get { return Convert.ToInt32(drowHhMItem["Age"]); }
            set { drowHhMItem["Age"] = value; }
        }
        public bool UseAge
        {
            get { return (bool)drowHhMItem["UseAge"]; }
            set { drowHhMItem["UseAge"] = value; }
        }
        public bool NotIncludedInClientList
        {
            get { return (bool)drowHhMItem["NotIncludedInClientList"]; }
            set { drowHhMItem["NotIncludedInClientList"] = value; }
        }
        public bool CSFP
        {
            get { return (bool)drowHhMItem["CSFP"]; }
            set { drowHhMItem["CSFP"] = value; }
        }
        public bool HeadHH
        {
            get { return (bool)drowHhMItem["HeadHH"]; }
            set { drowHhMItem["HeadHH"] = value; }
        }
        public int Language
        {
            get { return Convert.ToInt32(drowHhMItem["Language"]); }
            set { drowHhMItem["Language"] = value; }
        }
        public bool IsDisabled
        {
            get { return (bool)drowHhMItem["IsDisabled"]; }
            set { drowHhMItem["IsDisabled"] = value; }
        }
        public DateTime CSFPExpiration
        {
            get { return Convert.ToDateTime(drowHhMItem["CSFPExpiration"]); }
            set { drowHhMItem["CSFPExpiration"] = value; }
        }
        public string CSFPComments
        {
            get { return drowHhMItem["CSFPComments"].ToString(); }
            set { drowHhMItem["CSFPComments"] = value; }
        }
        public int CSFPRoute
        {
            get { return Convert.ToInt32(drowHhMItem["CSFPRoute"]); }
            set { drowHhMItem["CSFPRoute"] = value; }
        }
        public string MemIDNbr
        {
            get { return drowHhMItem["MemIdNbr"].ToString(); }
            set { drowHhMItem["MemIdNbr"] = value; }
        }
        public int MemIDType
        {
            get { return Convert.ToInt32(drowHhMItem["MemIDType"]); }
            set { drowHhMItem["MemIDType"] = value; }
        }
        public int Race
        {
            get { return Convert.ToInt32(drowHhMItem["Race"]); }
            set { drowHhMItem["Race"] = value; }
        }
        public bool Hispanic
        {
            get { return (bool)drowHhMItem["Hispanic"]; }
            set { drowHhMItem["Hispanic"] = value; }
        }
        public bool BackPack
        {
            get { return (bool)drowHhMItem["BackPack"]; }
            set { drowHhMItem["BackPack"] = value; }
        }
        public DateTime BPExpiration
        {
            get { return Convert.ToDateTime(drowHhMItem["BPExpiration"]); }
            set { drowHhMItem["BPExpiration"] = value; }
        }
        public int BPSize
        {
            get { return Convert.ToInt32(drowHhMItem["BPSize"]); }
            set { drowHhMItem["BPSize"] = value; }
        }
        public int BPSchool
        {
            get { return Convert.ToInt32(drowHhMItem["BPSchool"]); }
            set { drowHhMItem["BPSchool"] = value; }
        }
        public string BPNotes
        {
            get { return drowHhMItem["BPNotes"].ToString(); }
            set { drowHhMItem["BPNotes"] = value; }
        }
        public bool NotCounted
        {
            get { return (bool)drowHhMItem["NotCounted"]; }
            set { drowHhMItem["NotCounted"] = value; }
        }
        public int Relationship
        {
            get { return Convert.ToInt32(drowHhMItem["Relationship"]); }
            set { drowHhMItem["Relationship"] = value; }
        }
        public string Phone
        {
            get { return drowHhMItem["Phone"].ToString(); }
            set { drowHhMItem["Phone"] = value; }
        }
        public string EmailAddress
        {
            get { return drowHhMItem["EmailAddress"].ToString(); }
            set { drowHhMItem["EmailAddress"] = value; }
        }
        #endregion Get/Set Accessors

        #region Get/Set Accessors [Demographics]

        public int DemographicsId
        {
            get { return (int)drowItemDemographics["Id"]; }
            set { drowItemDemographics["Id"] = value; }
        }
        public int HispanicLatino
        {
            get { return Convert.ToInt32(drowItemDemographics["HispanicLatino"]); }
            set { drowItemDemographics["HispanicLatino"] = value; }
        }
        public int RefugeeImmigrant
        {
            get { return Convert.ToInt32(drowItemDemographics["RefugeeImmigrant"]); }
            set { drowItemDemographics["RefugeeImmigrant"] = value; }
        }
        public int LimitedEnglish
        {
            get { return Convert.ToInt32(drowItemDemographics["LimitedEnglish"]); }
            set { drowItemDemographics["LimitedEnglish"] = value; }
        }
        public int MilitaryService
        {
            get { return Convert.ToInt32(drowItemDemographics["MilitaryService"]); }
            set { drowItemDemographics["MilitaryService"] = value; }
        }
        public int DischargeStatus
        {
            get { return Convert.ToInt32(drowItemDemographics["DischargeStatus"]); }
            set { drowItemDemographics["DischargeStatus"] = value; }
        }
        public int PartneredMarried
        {
            get { return Convert.ToInt32(drowItemDemographics["PartneredMarried"]); }
            set { drowItemDemographics["PartneredMarried"] = value; }
        }
        public int LongTermHomeless
        {
            get { return Convert.ToInt32(drowItemDemographics["LongTermHomeless"]); }
            set { drowItemDemographics["LongTermHomeless"] = value; }
        }
        public int ChronicallyHomeless
        {
            get { return Convert.ToInt32(drowItemDemographics["ChronicallyHomeless"]); }
            set { drowItemDemographics["ChronicallyHomeless"] = value; }
        }
        public int Employed
        {
            get { return Convert.ToInt32(drowItemDemographics["Employed"]); }
            set { drowItemDemographics["Employed"] = value; }
        }
        public int EmplolymentStatus
        {
            get { return Convert.ToInt32(drowItemDemographics["EmplolymentStatus"]); }
            set { drowItemDemographics["EmplolymentStatus"] = value; }
        }
        public int EducationLevel
        {
            get { return Convert.ToInt32(drowItemDemographics["EducationLevel"]); }
            set { drowItemDemographics["EducationLevel"] = value; }
        }

        #region Ethnicity Accessors
        public bool AmericanIndian
        {
            get { return (bool)drowItemDemographics["AmericanIndian"]; }
            set { drowItemDemographics["AmericanIndian"] = value; }
        }
        public bool AlaskaNative
        {
            get { return (bool)drowItemDemographics["AlaskaNative"]; }
            set { drowItemDemographics["AlaskaNative"] = value; }
        }
        public bool IndigenousToAmericas
        {
            get { return (bool)drowItemDemographics["IndigenousToAmericas"]; }
            set { drowItemDemographics["IndigenousToAmericas"] = value; }
        }
        public bool AsianIndian
        {
            get { return (bool)drowItemDemographics["AsianIndian"]; }
            set { drowItemDemographics["AsianIndian"] = value; }
        }
        public bool Cambodian
        {
            get { return (bool)drowItemDemographics["Cambodian"]; }
            set { drowItemDemographics["Cambodian"] = value; }
        }
        public bool Chinese
        {
            get { return (bool)drowItemDemographics["Chinese"]; }
            set { drowItemDemographics["Chinese"] = value; }
        }
        public bool Filipino
        {
            get { return (bool)drowItemDemographics["Filipino"]; }
            set { drowItemDemographics["Filipino"] = value; }
        }
        public bool Japanese
        {
            get { return (bool)drowItemDemographics["Japanese"]; }
            set { drowItemDemographics["Japanese"] = value; }
        }
        public bool Korean
        {
            get { return (bool)drowItemDemographics["Korean"]; }
            set { drowItemDemographics["Korean"] = value; }
        }
        public bool Vietnamese
        {
            get { return (bool)drowItemDemographics["Vietnamese"]; }
            set { drowItemDemographics["Vietnamese"] = value; }
        }
        public bool OtherAsian
        {
            get { return (bool)drowItemDemographics["OtherAsian"]; }
            set { drowItemDemographics["OtherAsian"] = value; }
        }
        public bool IndigenousAfricanBlack
        {
            get { return (bool)drowItemDemographics["IndigenousAfricanBlack"]; }
            set { drowItemDemographics["IndigenousAfricanBlack"] = value; }
        }
        public bool AfricanAmericanBlack
        {
            get { return (bool)drowItemDemographics["AfricanAmericanBlack"]; }
            set { drowItemDemographics["AfricanAmericanBlack"] = value; }
        }
        public bool OtherBlack
        {
            get { return (bool)drowItemDemographics["OtherBlack"]; }
            set { drowItemDemographics["OtherBlack"] = value; }
        }
        public bool HawaiianNative
        {
            get { return (bool)drowItemDemographics["HawaiianNative"]; }
            set { drowItemDemographics["HawaiianNative"] = value; }
        }
        public bool Polynesian
        {
            get { return (bool)drowItemDemographics["Polynesian"]; }
            set { drowItemDemographics["Polynesian"] = value; }
        }
        public bool Micronesian
        {
            get { return (bool)drowItemDemographics["Micronesian"]; }
            set { drowItemDemographics["Micronesian"] = value; }
        }
        public bool OtherPacificIslander
        {
            get { return (bool)drowItemDemographics["OtherPacificIslander"]; }
            set { drowItemDemographics["OtherPacificIslander"] = value; }
        }
        public bool ArabIranianMiddleEastern
        {
            get { return (bool)drowItemDemographics["ArabIranianMiddleEastern"]; }
            set { drowItemDemographics["ArabIranianMiddleEastern"] = value; }
        }
        public bool OtherWhiteCaucasian
        {
            get { return (bool)drowItemDemographics["OtherWhiteCaucasian"]; }
            set { drowItemDemographics["OtherWhiteCaucasian"] = value; }
        }
        public bool EthnicOther
        {
            get { return (bool)drowItemDemographics["EthnicOther"]; }
            set { drowItemDemographics["EnthnicOther"] = value; }
        }
        public bool EthnicUnknown
        {
            get { return (bool)drowItemDemographics["EthnicUnknown"]; }
            set { drowItemDemographics["EthnicUnknown"] = value; }
        }
        public int Homeless
        {
            get { return  Convert.ToInt32(drowItemDemographics["Homeless"]); }
            set { drowItemDemographics["Homeless"] = value; }
        }
        public int HomelessNbrTimes
        {
            get { return Convert.ToInt32(drowItemDemographics["HomelessNbrTimes"]); }
            set { drowItemDemographics["HomelessNbrTimes"] = value; }
        }
        public int HomelessNbrMonths
        {
            get { return  Convert.ToInt32(drowItemDemographics["HomelessNbrMonths"]); }
            set { drowItemDemographics["HomelessNbrMonths"] = value; }
        }
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
            if (dcolHhM.IndexOf(FieldName) > 0)
            {
                if (drowHhMItem[FieldName].ToString() != FieldValue)
                {
                    bDataChanged = true;
                    drowHhMItem[FieldName] = FieldValue;
                }
            }
            else if (dcolDemographics.IndexOf(FieldName) > 0)
            {
                if (drowItemDemographics[FieldName].ToString() != FieldValue)
                {
                    bDataChanged = true;
                    drowItemDemographics[FieldName] = FieldValue;
                }
            }
        }

        /// <summary>
        /// Sets data value when value is a boolean value
        /// </summary>
        /// <param name="FieldName">Name of the field in the database</param>
        /// <param name="FieldValue">value to set</param>
        public void SetDataValue(string FieldName, bool FieldValue)
        {
            if (dcolHhM.IndexOf(FieldName) >= 0)
            {
                if (Convert.ToBoolean(drowHhMItem[FieldName]) != FieldValue)
                {
                    bDataChanged = true;
                    drowHhMItem[FieldName] = FieldValue;
                }
            }
            else if (dcolDemographics.IndexOf(FieldName) >= 0)
            {
                if (Convert.ToBoolean(drowItemDemographics[FieldName]) != FieldValue)
                {
                    bDataChanged = true;
                    drowItemDemographics[FieldName] = FieldValue;
                }
            }
        }

        //Gets data value from database using the selected data row
        public object GetDataValue(string FieldName)
        {
            if (drowHhMItem != null && dcolHhM !=null)
            {
                if (dcolHhM.IndexOf(FieldName) >= 0)
                    return drowHhMItem[FieldName];
                else if (drowItemDemographics != null && dcolDemographics != null)
                {
                    if (dcolDemographics.IndexOf(FieldName) >= 0)
                        return drowItemDemographics[FieldName];
                }
            }
            return "";
        }

        //Gets property through use of just the collum name in database
        public string GetDataString(string FieldName)
        {
            if (drowHhMItem != null && dcolHhM != null)
            {
                int fldIndex = dcolHhM.IndexOf(FieldName);
                if (fldIndex >= 0)
                {
                    if (dcolHhM[fldIndex].DataType.Name == "DateTime")
                        if (drowHhMItem[FieldName].ToString() != "")
                        { return CCFBGlobal.ValidDateString(drowHhMItem[FieldName]); }
                        else
                        { return ""; }
                    else
                        return drowHhMItem[FieldName].ToString();
                }
                else if (drowItemDemographics != null && dcolDemographics != null)
                {
                    fldIndex = dcolDemographics.IndexOf(FieldName);
                    if (fldIndex >= 0)
                    {
                        if (dcolDemographics[fldIndex].DataType.Name == "DateTime")
                            if (drowItemDemographics[FieldName].ToString() != "")
                            { return CCFBGlobal.ValidDateString(drowItemDemographics[FieldName]); }
                            else
                            { return ""; }
                        else
                            return drowItemDemographics[FieldName].ToString();
                    }
                    return "";
                }
            }
            return "";
        }
        
        #endregion
    }
}
