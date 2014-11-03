
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

namespace ClientcardFB3
{
    #region HHMembers Class
    public class HHMembers
    {
        string connString;
        SqlCommandBuilder commBuilder = new SqlCommandBuilder();
        SqlDataAdapter datAdptHhM = new SqlDataAdapter();
        SqlDataAdapter datAdptDemo = new SqlDataAdapter();
        DataSet dset;
        //SqlCommand command;
        SqlConnection conn;
        static string tblHhMName = "HouseholdMembers";
        static string tblDemoName = "Demographics";
        bool isValid;
        int iRowCountHhM = 0;
        int iRowCountDemo = 0;
        DataRow drowHhm = null;
        DataRow drowDemoGraphics = null;
        
        //HouseholdMembers
        const string fldID = "ID";
        const string fldInactive = "Inactive";
        const string fldHouseholdID = "HouseholdID";
        const string fldLastName = "LastName";
        const string fldFirstName = "FirstName";
        const string fldSex = "Sex";
        const string fldBirthDate = "Birthdate";
        const string fldAgeGroup = "AgeGroup";
        const string fldSpecialDiet = "SpecialDiet";
        const string fldNotes = "Notes";
        const string fldWorksInArea = "WorksInArea";
        const string fldEmployer = "Employer";
        const string fldEmpAddress = "EmpAddress";
        const string fldEmpCity = "EmpCity";
        const string fldEmpZipcode = "EmpZipcode";
        const string fldEmpPhone = "EmpPhone";
        const string fldCreated = "Created";
        const string fldCreatedBy = "CreatedBy";
        const string fldModified = "Modified";
        const string fldModifiedBy = "ModifiedBy";
        const string fldUserFlag0 = "UserFlag0";
        const string fldUserFlag1 = "UserFlag1";
        const string fldVolunteersAtFoodBank = "VolunteersAtFoodBank";
        const string fldAge = "Age";
        const string fldUseAge = "UseAge";
        const string fldNotIncludedInClientList = "NotIncludedInClientList";
        const string fldCSFP = "CSFP";
        const string fldHeadHH = "HeadHH";
        const string fldLanguage = "Language";
        const string fldIsDisabled = "IsDisabled";
        const string fldCSFPExpiration = "CSFPExpiration";
        const string fldCSFPComments = "CSFPComments";
        const string fldCSFPRoute = "CSFPRoute";
        const string fldMemIDNbr = "MemIDNbr";
        const string fldMemIDType = "MemIDType";
        const string fldRace = "Race";
        const string fldHispanic = "Hispanic";
        const string fldBackPack = "BackPack";
        const string fldBPExpiration = "BPExpiration";
        const string fldBPSize = "BPSize";
        const string fldBPSchool = "BPSchool";
        const string fldBPNotes = "BPNotes";
        const string fldNotCounted = "NotCounted";
        const string fldRelationship = "Relationship";
        const string fldPhone = "Phone";
        const string fldEmailAddress = "EmailAddress";
        const string fldGrade = "Grade";
        const string fldSchSupply = "SchSupply";
        const string fldSchSupplyDelivered = "SchSupplyDelivered";
        const string fldSchSupplySchool = "SchSupplySchool";
        const string fldCSFPStatus = "CSFPStatus";

        //Demographics
        const string fldMilitaryService = "MilitaryService";
        const string fldDischargeStatus = "DischargeStatus";
        const string fldHispanicLatino = "HispanicLatino";
        const string fldRefugeeImmigrant = "RefugeeImmigrant";
        const string fldLimitedEnglish = "LimitedEnglish";
        const string fldPartneredMarried = "PartneredMarried";
        const string fldLongTermHomeless = "LongTermHomeless";
        const string fldChronicallyHomeless = "ChronicallyHomeless";
        const string fldEmployed = "Employed";
        const string fldEmplolymentStatus = "EmplolymentStatus";
        const string fldAmericanIndian = "AmericanIndian";
        const string fldAlaskaNative = "AlaskaNative";
        const string fldIndigenousToAmericas = "IndigenousToAmericas";
        const string fldAsianIndian = "AsianIndian";
        const string fldCambodian = "Cambodian";
        const string fldChinese = "Chinese";
        const string fldFilipino = "Filipino";
        const string fldJapanese = "Japanese";
        const string fldKorean = "Korean";
        const string fldVietnamese = "Vietnamese";
        const string fldOtherAsian = "OtherAsian";
        const string fldIndigenousAfricanBlack = "IndigenousAfricanBlack";
        const string fldAfricanAmericanBlack = "AfricanAmericanBlack";
        const string fldOtherBlack = "OtherBlack";
        const string fldHawaiianNative = "HawaiianNative";
        const string fldPolynesian = "Polynesian";
        const string fldMicronesian = "Micronesian";
        const string fldOtherPacificIslander = "OtherPacificIslander";
        const string fldArabIranianMiddleEastern = "ArabIranianMiddleEastern";
        const string fldOtherWhiteCaucasian = "OtherWhiteCaucasian";
        const string fldEthnicOther = "EthnicOther";
        const string fldEthnicUnknown = "EthnicUnknown";
        const string fldEducationLevel = "EducationLevel";
        const string fldHomeless = "Homeless";
        const string fldHomelessNbrTimes = "HomelessNbrTimes";
        const string fldHomelessNbrMonths = "HomelessNbrMonths";

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
                for (int i = 0; i < dset.Tables[tblHhMName].Rows.Count; i++)
                {
                    if (dset.Tables[tblHhMName].Rows[i].RowState != DataRowState.Unchanged)
                    {
                        return true;
                    }
                    if (dset.Tables[tblDemoName].Rows[i].RowState != DataRowState.Unchanged)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
        public DataRow DRowHhm
        {
            get { return drowHhm; }
            set
            {
                drowHhm = value;
            }
        }
        public DataRow DRowDemograhics
        {
            get { return drowDemoGraphics; }
            set 
            { 
                drowDemoGraphics = value;
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
            get { return Convert.ToInt32(drowHhm[fldID]); }
            set { drowHhm[fldID] = value; }
        }

        public bool Inactive
        {
            get { return (bool)drowHhm[fldInactive]; }
            set { drowHhm[fldInactive] = value; }
        }

        public int HouseholdID
        {
            get { return Convert.ToInt32(drowHhm[fldHouseholdID]); }
            set { drowHhm[fldHouseholdID] = value; }
        }

        public string LastName
        {
            get { return drowHhm[fldLastName].ToString(); }
            set { drowHhm[fldLastName] = value; }
        }

        public string FirstName
        {
            get { return drowHhm[fldFirstName].ToString(); }
            set { drowHhm[fldFirstName] = value; }
        }

        public string Sex
        {
            get { return drowHhm[fldSex].ToString(); }
            set { drowHhm[fldSex] = value; }
        }

        public DateTime Birthdate
        {
            get 
            {
                try
                {
                    return Convert.ToDateTime(drowHhm[fldBirthDate]);
                }
                catch { return CCFBGlobal.FBNullDateValue; }
            }
            set { drowHhm[fldBirthDate] = value; }
        }

        public int AgeGroup
        {
            get { return Convert.ToInt32(drowHhm[fldAgeGroup]); }
            set { drowHhm[fldAgeGroup] = value; }
        }

        public bool SpecialDiet
        {
            get { return (bool)drowHhm[fldSpecialDiet]; }
            set { drowHhm[fldSpecialDiet] = value; }
        }

        public string Notes
        {
            get { return drowHhm[fldNotes].ToString(); }
            set { drowHhm[fldNotes] = value; }
        }

        public bool WorksInArea
        {
            get { return (bool)drowHhm[fldWorksInArea]; }
            set { drowHhm[fldWorksInArea] = value; }
        }

        public string Employer
        {
            get { return drowHhm[fldEmployer].ToString(); }
            set { drowHhm[fldEmployer] = value; }
        }

        public string EmpAddress
        {
            get { return drowHhm[fldEmpAddress].ToString(); }
            set { drowHhm[fldEmpAddress] = value; }
        }

        public string EmpCity
        {
            get { return drowHhm[fldEmpCity].ToString(); }
            set { drowHhm[fldEmpCity] = value; }
        }

        public string EmpZipcode
        {
            get { return drowHhm[fldEmpZipcode].ToString(); }
            set { drowHhm[fldEmpZipcode] = value; }
        }

        public string EmpPhone
        {
            get { return drowHhm[fldEmpPhone].ToString(); }
            set { drowHhm[fldEmpPhone] = value; }
        }

        public DateTime Created
        {
            get { return (DateTime)drowHhm[fldCreated]; }
            set { drowHhm[fldCreated] = value; }
        }

        public string CreatedBy
        {
            get { return drowHhm[fldCreatedBy].ToString(); }
            set { drowHhm[fldCreatedBy] = value; }
        }

        public DateTime Modified
        {
            get 
            {
                if (drowHhm[fldModified].ToString() == "")
                    return CCFBGlobal.FBNullDateValue;
                else
                    return (DateTime)drowHhm[fldModified]; 
            }
            set { drowHhm[fldModified] = value; }
        }

        public string ModifiedBy
        {
            get { return drowHhm[fldModifiedBy].ToString(); }
            set { drowHhm[fldModifiedBy] = value; }
        }

        public bool UserFlag0
        {
            get { return (bool)drowHhm[fldUserFlag0]; }
            set { drowHhm[fldUserFlag0] = value; }
        }

        public bool UserFlag1
        {
            get { return (bool)drowHhm[fldUserFlag1]; }
            set { drowHhm[fldUserFlag1] = value; }
        }

        public bool VolunteersAtFoodBank
        {
            get { return (bool)drowHhm[fldVolunteersAtFoodBank]; }
            set { drowHhm[fldVolunteersAtFoodBank] = value; }
        }

        public int Age
        {
            get { return Convert.ToInt32(drowHhm[fldAge]); }
            set { drowHhm[fldAge] = value; }
        }

        public bool UseAge
        {
            get { return (bool)drowHhm[fldUseAge]; }
            set { drowHhm[fldUseAge] = value; }
        }

        public bool NotIncludedInClientList
        {
            get { return (bool)drowHhm[fldNotIncludedInClientList]; }
            set { drowHhm[fldNotIncludedInClientList] = value; }
        }

        public bool CSFP
        {
            get { return (bool)drowHhm[fldCSFP]; }
            set { drowHhm[fldCSFP] = value; }
        }

        public bool HeadHH
        {
            get { return (bool)drowHhm[fldHeadHH]; }
            set { drowHhm[fldHeadHH] = value; }
        }

        public int Language
        {
            get { return Convert.ToInt32(drowHhm[fldLanguage]); }
            set { drowHhm[fldLanguage] = value; }
        }

        public bool IsDisabled
        {
            get { return (bool)drowHhm[fldIsDisabled]; }
            set { drowHhm[fldIsDisabled] = value; }
        }

        public DateTime CSFPExpiration
        {
            get 
            {
                if (drowHhm[fldCSFPExpiration].ToString() == "")
                {
                    return CCFBGlobal.FBNullDateValue;
                }
                else
                {
                    return (DateTime)drowHhm[fldCSFPExpiration];
                }
            }
            set { drowHhm[fldCSFPExpiration] = value; }
        }
        public string CSFPComments
        {
            get { return drowHhm[fldCSFPComments].ToString(); }
            set { drowHhm[fldCSFPComments] = value; }
        }
        public int CSFPRoute
        {
            get { return Convert.ToInt32(drowHhm[fldCSFPRoute]); }
            set { drowHhm[fldCSFPRoute] = value; }
        }
        public string MemIDNbr
        {
            get { return drowHhm[fldMemIDNbr].ToString(); }
            set { drowHhm[fldMemIDNbr] = value; }
        }
        public int MemIDType
        {
            get { return Convert.ToInt32(drowHhm[fldMemIDType]); }
            set { drowHhm[fldMemIDType] = value; }
        }
        public int Race
        {
            get { return Convert.ToInt32(drowHhm[fldRace]); }
            set { drowHhm[fldRace] = value; }
        }
        public bool Hispanic
        {
            get { return (bool)drowHhm[fldHispanic]; }
            set { drowHhm[fldHispanic] = value; }
        }
        public bool BackPack
        {
            get { return (bool)drowHhm[fldBackPack]; }
            set { drowHhm[fldBackPack] = value; }
        }
        public DateTime BPExpiration
        {
            get
            {
                if (drowHhm[fldBPExpiration].ToString() == "")
                    return CCFBGlobal.FBNullDateValue;
                else
                    return (DateTime)drowHhm[fldBPExpiration];
            }

            set { drowHhm[fldBPExpiration] = value; }
        }
        public int BPSize
        {
            get { return Convert.ToInt32(drowHhm[fldBPSize]); }
            set { drowHhm[fldBPSize] = value; }
        }
        public int BPSchool
        {
            get { return Convert.ToInt32(drowHhm[fldBPSchool]); }
            set { drowHhm[fldBPSchool] = value; }
        }
        public string BPNotes
        {
            get { return drowHhm[fldBPNotes].ToString(); }
            set { drowHhm[fldBPNotes] = value; }
        }

        public bool NotCounted
        {
            get { return (bool)drowHhm[fldNotCounted]; }
            set { drowHhm[fldNotCounted] = value; }
        }

        public int Relationship
        {
            get { return Convert.ToInt32(drowHhm[fldRelationship]); }
            set { drowHhm[fldRelationship] = value; }
        }
        public string Phone
        {
            get { return drowHhm[fldPhone].ToString(); }
            set { drowHhm[fldPhone] = value; }
        }
        public string EmailAddress
        {
            get { return drowHhm[fldEmailAddress].ToString(); }
            set { drowHhm[fldEmailAddress] = value; }
        }
        public int Grade
        {
            get { return Convert.ToInt32(drowHhm[fldGrade]); }
            set { drowHhm[fldGrade] = value; }
        }
        public bool SchSupply
        {
            get { return (bool)drowHhm[fldSchSupply]; }
            set { drowHhm[fldSchSupply] = value; }
        }
        public DateTime SchSupplyDelivered
        {
            get
            {
                if (drowHhm[fldSchSupplyDelivered].ToString() == "")
                    return CCFBGlobal.FBNullDateValue;
                else
                    return (DateTime)drowHhm[fldSchSupplyDelivered];
            }
            set { drowHhm[fldSchSupplyDelivered] = value; }
        }
        public int SchSupplySchool
        {
            get { return Convert.ToInt32(drowHhm[fldSchSupplySchool]); }
            set { drowHhm[fldSchSupplySchool] = value; }
        }
        public int CSFPStatus
        {
            get { return Convert.ToInt32(drowHhm[fldCSFPStatus]); }
            set { drowHhm[fldCSFPStatus] = value; }
        }

        public bool HasCSFP
        {
            get
            {
                for (int i = 0; i < iRowCountHhM; i++)
                {
                    if (dset.Tables[0].Rows[i].Field<bool>("CSFP"))
                        return true;
                }
                return false;
            }
        }

        public string Name
        {
            get { return (FirstName + " " + LastName).Trim(); }
        }

        public string HHName
        {
            get { return (LastName.Trim() + ", " + FirstName.Trim()).Trim(); }
        }
        #endregion
//Demographics
        #region Get/Set Accessors [Demographics]

        public int DemographicsId
        {
            get { return (int)drowDemoGraphics[fldID]; }
            set { drowDemoGraphics[fldID] = value; }
        }
        public bool HispanicLatino
        {
            get { return (bool)drowDemoGraphics[fldHispanicLatino]; }
            set { drowDemoGraphics[fldHispanicLatino] = value; }
        }
        public bool RefugeeImmigrant
        {
            get { return (bool)drowDemoGraphics[fldRefugeeImmigrant]; }
            set { drowDemoGraphics[fldRefugeeImmigrant] = value; }
        }
        public bool LimitedEnglish
        {
            get { return (bool)drowDemoGraphics[fldLimitedEnglish]; }
            set { drowDemoGraphics[fldLimitedEnglish] = value; }
        }
        public int MilitaryService
        {
            get { return Convert.ToInt32(drowDemoGraphics[fldMilitaryService]); }
            set { drowDemoGraphics[fldMilitaryService] = value; }
        }
        public int DischargeStatus
        {
            get { return Convert.ToInt32(drowDemoGraphics[fldDischargeStatus]); }
            set { drowDemoGraphics[fldDischargeStatus] = value; }
        }
        public bool PartneredMarried
        {
            get { return (bool)drowDemoGraphics[fldPartneredMarried]; }
            set { drowDemoGraphics[fldPartneredMarried] = value; }
        }
        public bool LongTermHomeless
        {
            get { return (bool)drowDemoGraphics[fldLongTermHomeless]; }
            set { drowDemoGraphics[fldLongTermHomeless] = value; }
        }
        public bool ChronicallyHomeless
        {
            get { return (bool)drowDemoGraphics[fldChronicallyHomeless]; }
            set { drowDemoGraphics[fldChronicallyHomeless] = value; }
        }
        public bool Employed
        {
            get { return (bool)drowDemoGraphics[fldEmployed]; }
            set { drowDemoGraphics[fldEmployed] = value; }
        }
        public int EmplolymentStatus
        {
            get { return Convert.ToInt32(drowDemoGraphics[fldEmplolymentStatus]); }
            set { drowDemoGraphics[fldEmplolymentStatus] = value; }
        }
        public int EducationLevel
        {
            get { return Convert.ToInt32(drowDemoGraphics[fldEducationLevel]); }
            set { drowDemoGraphics[fldEducationLevel] = value; }
        }

        #region Ethnicity Accessors
        public bool AmericanIndian
        {
            get { return (bool)drowDemoGraphics[fldAmericanIndian]; }
            set { drowDemoGraphics[fldAmericanIndian] = value; }
        }
        public bool AlaskaNative
        {
            get { return (bool)drowDemoGraphics[fldAlaskaNative]; }
            set { drowDemoGraphics[fldAlaskaNative] = value; }
        }
        public bool IndigenousToAmericas
        {
            get { return (bool)drowDemoGraphics[fldIndigenousToAmericas]; }
            set { drowDemoGraphics[fldIndigenousToAmericas] = value; }
        }
        public bool AsianIndian
        {
            get { return (bool)drowDemoGraphics[fldAsianIndian]; }
            set { drowDemoGraphics[fldAsianIndian] = value; }
        }
        public bool Cambodian
        {
            get { return (bool)drowDemoGraphics[fldCambodian]; }
            set { drowDemoGraphics[fldCambodian] = value; }
        }
        public bool Chinese
        {
            get { return (bool)drowDemoGraphics[fldChinese]; }
            set { drowDemoGraphics[fldChinese] = value; }
        }
        public bool Filipino
        {
            get { return (bool)drowDemoGraphics[fldFilipino]; }
            set { drowDemoGraphics[fldFilipino] = value; }
        }
        public bool Japanese
        {
            get { return (bool)drowDemoGraphics[fldJapanese]; }
            set { drowDemoGraphics[fldJapanese] = value; }
        }
        public bool Korean
        {
            get { return (bool)drowDemoGraphics[fldKorean]; }
            set { drowDemoGraphics[fldKorean] = value; }
        }
        public bool Vietnamese
        {
            get { return (bool)drowDemoGraphics[fldVietnamese]; }
            set { drowDemoGraphics[fldVietnamese] = value; }
        }
        public bool OtherAsian
        {
            get { return (bool)drowDemoGraphics[fldOtherAsian]; }
            set { drowDemoGraphics[fldOtherAsian] = value; }
        }
        public bool IndigenousAfricanBlack
        {
            get { return (bool)drowDemoGraphics[fldIndigenousAfricanBlack]; }
            set { drowDemoGraphics[fldIndigenousAfricanBlack] = value; }
        }
        public bool AfricanAmericanBlack
        {
            get { return (bool)drowDemoGraphics[fldAfricanAmericanBlack]; }
            set { drowDemoGraphics[fldAfricanAmericanBlack] = value; }
        }
        public bool OtherBlack
        {
            get { return (bool)drowDemoGraphics[fldOtherBlack]; }
            set { drowDemoGraphics[fldOtherBlack] = value; }
        }
        public bool HawaiianNative
        {
            get { return (bool)drowDemoGraphics[fldHawaiianNative]; }
            set { drowDemoGraphics[fldHawaiianNative] = value; }
        }
        public bool Polynesian
        {
            get { return (bool)drowDemoGraphics[fldPolynesian]; }
            set { drowDemoGraphics[fldPolynesian] = value; }
        }
        public bool Micronesian
        {
            get { return (bool)drowDemoGraphics[fldMicronesian]; }
            set { drowDemoGraphics[fldMicronesian] = value; }
        }
        public bool OtherPacificIslander
        {
            get { return (bool)drowDemoGraphics[fldOtherPacificIslander]; }
            set { drowDemoGraphics[fldOtherPacificIslander] = value; }
        }
        public bool ArabIranianMiddleEastern
        {
            get { return (bool)drowDemoGraphics[fldArabIranianMiddleEastern]; }
            set { drowDemoGraphics[fldArabIranianMiddleEastern] = value; }
        }
        public bool OtherWhiteCaucasian
        {
            get { return (bool)drowDemoGraphics[fldOtherWhiteCaucasian]; }
            set { drowDemoGraphics[fldOtherWhiteCaucasian] = value; }
        }
        public bool Other
        {
            get { return (bool)drowDemoGraphics[fldEthnicOther]; }
            set { drowDemoGraphics[fldEthnicOther] = value; }
        }
        public bool Unknown
        {
            get { return (bool)drowDemoGraphics[fldEthnicUnknown]; }
            set { drowDemoGraphics[fldEthnicUnknown] = value; }
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
                {
                    drowHhm[FieldName] = FieldValue;
                }
            }
            else if (dset.Tables[tblDemoName].Columns.IndexOf(FieldName) > 0)
                if (drowDemoGraphics[FieldName].ToString() != FieldValue)
                {
                    drowDemoGraphics[FieldName] = FieldValue;
                }
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
                {
                    drowHhm[FieldName] = FieldValue;
                }
            }
            else if (dset.Tables[tblDemoName].Columns.IndexOf(FieldName) >= 0)
                if (Convert.ToBoolean(drowDemoGraphics[FieldName]) != FieldValue)
                {
                    drowDemoGraphics[FieldName] = FieldValue;
                }
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
                        { return CCFBGlobal.ValidDateString(drowHhm[FieldName]); }
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
                            { return CCFBGlobal.ValidDateString(drowDemoGraphics[FieldName]); }
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

        public string getHeadHH(int hhid)
        {
            string headHhName = CCFBGlobal.getSQLValue("if Exists(SELECT * FROM HouseholdMembers WHERE HeadHH = 1 AND HouseholdID = " + hhid.ToString() + ")"
                              + " SELECT LastName + ', ' + FirstName FROM HouseholdMembers WHERE HeadHH = 1 AND HouseholdID = " + hhid.ToString() + " ELSE SELECT ''").ToString();
            return headHhName;
        }

        public int getHeadHHId(int hhid)
        {
            object hhmid = CCFBGlobal.getSQLValue("if Exists(SELECT * FROM HouseholdMembers WHERE HeadHH = 1 AND HouseholdID = " + hhid.ToString() + ")"
                              + " SELECT ID FROM HouseholdMembers WHERE HeadHH = 1 AND HouseholdID = " + hhid.ToString() + " ELSE SELECT MIN(ID) FROM HouseholdMembers WHERE HouseholdID = " + hhid.ToString());
            if (hhmid == DBNull.Value)
                return 0;
            else
                return Convert.ToInt32(hhmid);
        }

        public string getMemName(int idHHM, string hhName)
        {
            if (idHHM == 0)
            {
                return hhName;
            }
            //test open dataset first
            foreach (DataRow item in dset.Tables[tblHhMName].Rows)
            {
                if (item.Field<int>(fldID) == idHHM)
                {
                    return (item[fldLastName].ToString()).Trim() + ", " + item[fldFirstName].ToString().Trim();
                }
            }
            string nameHHM = hhName;
            object dbValue = CCFBGlobal.getSQLValue("SELECT RTRIM(LastName) + ', ' + RTRIM(FirstName) FROM HouseholdMembers WHERE ID = " + idHHM.ToString());
            if (dbValue != DBNull.Value)
            {
                nameHHM = dbValue.ToString();
            }
            return nameHHM;
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
            if (drowHhm != null)
            {
                drowHhm.RejectChanges();
                drowDemoGraphics.RejectChanges();
            }
        }

        /// <summary>
        /// Sets the DataRow of HH Members to be the row index passed in
        /// </summary>
        /// <param name="rowIndex">The row index in the dataset</param>
        public void SetRecord(int rowIndex)
        {
            if (rowIndex >= 0 && rowIndex < iRowCountHhM)
            {
                drowHhm = dset.Tables[tblHhMName].Rows[rowIndex];
                if (dset.Tables[tblDemoName] != null)
                    try
                    {
                        drowDemoGraphics = dset.Tables[tblDemoName].Rows[rowIndex];
                    }
                    catch (Exception ex) 
                    {
                        CCFBGlobal.appendErrorToErrorReport("Missing Demographics Row = " + rowIndex.ToString() + " HouseHoldId = " + HouseholdID.ToString(), ex.GetBaseException().ToString());
                    }
            }
        }

        public void cancelChanges()
        {
            drowHhm.CancelEdit();
            drowDemoGraphics.CancelEdit();
        }

        public bool nameExists(string nameLast, string nameFirst, string dateBirth, bool onlyActive)
        {
            string sqlText = "SELECT COUNT(*) FROM HouseholdMembers"
                           + " WHERE LastName = '" + CCFBGlobal.SQLApostrophe(nameLast) + "'"
                           + "   AND FirstName = '" + CCFBGlobal.SQLApostrophe(nameFirst) + "'";
            if (onlyActive == true)
            {
                sqlText += "   AND Inactive = 0";
            }
            if (dateBirth != "")
                sqlText += " AND BirthDate = '" + dateBirth + "'";
            int result = Convert.ToInt32(CCFBGlobal.getSQLValue(sqlText));
            return (result > 0);
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
                CCFBGlobal.appendErrorToErrorReport(sqlHhM.ToString(), ex.GetBaseException().ToString());
                closeConnection();
                iRowCountHhM = 0;
                iRowCountDemo = 0;
                return isValid = false;
            }
        }

        public bool openWhere(string WhereClause, string OrderByClause, bool incldueDemoGraphics)
        {
            string sqlDemo = "";
            if (incldueDemoGraphics == true)
            {
                sqlDemo = "SELECT * FROM " + tblDemoName + " WHERE Id IN (SELECT ID FROM " + tblHhMName + " " + WhereClause + ")" + " ORDER BY ID";
                OrderByClause = "ID";
            }
            if (OrderByClause == "")
                OrderByClause = "ID";
            return openTables("SELECT * FROM " + tblHhMName + " " + WhereClause + " ORDER BY " + OrderByClause, sqlDemo);
        }

        public bool openHHID(int HHID)
        {
            return openTables("SELECT * FROM " + tblHhMName + " WHERE HouseholdID=" + HHID.ToString() + " ORDER BY HeadHH DESC, BirthDate",
                              "SELECT * FROM " + tblDemoName + " WHERE Id IN (SELECT ID FROM " + tblHhMName + " WHERE HouseholdId = " + HHID.ToString() + ")" + " ORDER BY ID");
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
                datAdptHhM.RowUpdated += new SqlRowUpdatedEventHandler( OnHHMRowUpdated );
                datAdptHhM.Update(dset, tblHhMName);
                datAdptHhM.RowUpdated -= new SqlRowUpdatedEventHandler(OnHHMRowUpdated);
                datAdptDemo.Update(dset, tblDemoName);
                dset.Clear();
                datAdptHhM.Fill(dset, tblHhMName);
                datAdptDemo.Fill(dset, tblDemoName);
                closeConnection();
                return true;
            }
            catch (SqlException ex)
            {
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString());
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
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString());
                closeConnection(); return false;
            }
        }

        private static void OnHHMRowUpdated( object sender, SqlRowUpdatedEventArgs e)
        {
        }

        public bool update(bool changeModified)
        {
            if (dset != null)
            {
                if (dset.HasChanges() == true)
                {
                    try
                    {
                        openConnection();
                        if (datAdptHhM.UpdateCommand == null)
                        {
                            SqlCommandBuilder commBuild = new SqlCommandBuilder(datAdptHhM);
                            datAdptHhM.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                        }
                        if (datAdptDemo.UpdateCommand == null)
                        {
                            SqlCommandBuilder commBuild1 = new SqlCommandBuilder(datAdptDemo);
                            datAdptDemo.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                        }
                        for (int i = 0; i < dset.Tables[tblHhMName].Rows.Count; i++)
                        {
                            DataRow drow = dset.Tables[tblHhMName].Rows[i];
                            if (drow.RowState != DataRowState.Unchanged && changeModified == true)
                            {
                                drow["ModifiedBy"] = CCFBGlobal.dbUserName;
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
                        CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString());
                    }
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

        public DateTime LastCSFPLogEntry(DateTime dateTest)
        {
            object dateResult = CCFBGlobal.getSQLValue("SELECT Max(TrxDate) FROM CSFPLog WHERE MemId = " + ID.ToString() + " AND TrxDate <= '" + dateTest.ToShortDateString() + "'");
            if (dateResult != DBNull.Value)
                return (Convert.ToDateTime(dateResult));
            else
                return CCFBGlobal.FBNullDateValue;
        }

        public int newHHMemberSave(HHMemberItem newHHMItem)
        {
            int newID = 0;
            newHHMItem.CreatedBy = CCFBGlobal.dbUserName;
            SqlCommand cmdInsert = new SqlCommand();
            cmdInsert.CommandType = CommandType.StoredProcedure;
            cmdInsert.Connection = conn;
            if (conn.State != ConnectionState.Open)
            { conn.Open(); }
            try
            {
                cmdInsert.CommandText = "InsertHouseholdMember";
                cmdInsert.Parameters.AddWithValue("@" + fldInactive, newHHMItem.Inactive);
                cmdInsert.Parameters.AddWithValue("@" + fldHouseholdID, newHHMItem.HouseholdID);
                cmdInsert.Parameters.AddWithValue("@" + fldLastName, newHHMItem.LastName);
                cmdInsert.Parameters.AddWithValue("@" + fldFirstName, newHHMItem.FirstName);
                cmdInsert.Parameters.AddWithValue("@" + fldSex, newHHMItem.Sex);
                cmdInsert.Parameters.AddWithValue("@" + fldBirthDate, newHHMItem.BirthDate);
                cmdInsert.Parameters.AddWithValue("@" + fldAgeGroup, newHHMItem.AgeGroup);
                cmdInsert.Parameters.AddWithValue("@" + fldSpecialDiet, newHHMItem.SpecialDiet);
                cmdInsert.Parameters.AddWithValue("@" + fldNotes, newHHMItem.Notes);
                cmdInsert.Parameters.AddWithValue("@" + fldWorksInArea, newHHMItem.WorksInArea);
                cmdInsert.Parameters.AddWithValue("@" + fldEmployer, newHHMItem.Employer);
                cmdInsert.Parameters.AddWithValue("@" + fldEmpAddress, newHHMItem.EmpAddress);
                cmdInsert.Parameters.AddWithValue("@" + fldEmpCity, newHHMItem.EmpCity);
                cmdInsert.Parameters.AddWithValue("@" + fldEmpZipcode, newHHMItem.EmpZipcode);
                cmdInsert.Parameters.AddWithValue("@" + fldEmpPhone, newHHMItem.EmpPhone);
                cmdInsert.Parameters.AddWithValue("@" + fldCreatedBy, newHHMItem.CreatedBy);
                cmdInsert.Parameters.AddWithValue("@" + fldUserFlag0, newHHMItem.UserFlag0);
                cmdInsert.Parameters.AddWithValue("@" + fldUserFlag1, newHHMItem.UserFlag1);
                cmdInsert.Parameters.AddWithValue("@" + fldVolunteersAtFoodBank, newHHMItem.VolunteersAtFoodBank);
                cmdInsert.Parameters.AddWithValue("@" + fldAge, newHHMItem.Age);
                cmdInsert.Parameters.AddWithValue("@" + fldUseAge, newHHMItem.UseAge);
                cmdInsert.Parameters.AddWithValue("@" + fldNotIncludedInClientList, newHHMItem.NotIncludedInClientList);
                cmdInsert.Parameters.AddWithValue("@" + fldCSFP, newHHMItem.CSFP);
                cmdInsert.Parameters.AddWithValue("@" + fldHeadHH, newHHMItem.HeadHH);
                cmdInsert.Parameters.AddWithValue("@" + fldLanguage, newHHMItem.Language);
                cmdInsert.Parameters.AddWithValue("@" + fldIsDisabled, newHHMItem.IsDisabled);
                cmdInsert.Parameters.AddWithValue("@" + fldCSFPExpiration, newHHMItem.CSFPExpiration);
                cmdInsert.Parameters.AddWithValue("@" + fldCSFPComments, newHHMItem.CSFPComments);
                cmdInsert.Parameters.AddWithValue("@" + fldCSFPRoute, newHHMItem.CSFPRoute);
                cmdInsert.Parameters.AddWithValue("@" + fldMemIDNbr, newHHMItem.MemIDNbr);
                cmdInsert.Parameters.AddWithValue("@" + fldMemIDType, newHHMItem.MemIDType);
                cmdInsert.Parameters.AddWithValue("@" + fldRace, newHHMItem.Race);
                cmdInsert.Parameters.AddWithValue("@" + fldHispanic, newHHMItem.Hispanic);
                cmdInsert.Parameters.AddWithValue("@" + fldBackPack, newHHMItem.BackPack);
                cmdInsert.Parameters.AddWithValue("@" + fldBPExpiration, newHHMItem.BPExpiration);
                cmdInsert.Parameters.AddWithValue("@" + fldBPSize, newHHMItem.BPSize);
                cmdInsert.Parameters.AddWithValue("@" + fldBPSchool, newHHMItem.BPSchool);
                cmdInsert.Parameters.AddWithValue("@" + fldBPNotes, newHHMItem.BPNotes);
                cmdInsert.Parameters.AddWithValue("@" + fldNotCounted, newHHMItem.NotCounted);
                cmdInsert.Parameters.AddWithValue("@" + fldRelationship, newHHMItem.Relationship);
                cmdInsert.Parameters.AddWithValue("@" + fldPhone, newHHMItem.Phone);
                cmdInsert.Parameters.AddWithValue("@" + fldEmailAddress, newHHMItem.EmailAddress);
                cmdInsert.Parameters.AddWithValue("@" + fldGrade, newHHMItem.Grade);
                cmdInsert.Parameters.AddWithValue("@" + fldSchSupply, newHHMItem.SchSupply);
                cmdInsert.Parameters.AddWithValue("@" + fldSchSupplyDelivered, newHHMItem.SchSupplyDelivered);
                cmdInsert.Parameters.AddWithValue("@" + fldSchSupplySchool, newHHMItem.SchSupplySchool);
                cmdInsert.Parameters.AddWithValue("@" + fldCSFPStatus, newHHMItem.CSFPStatus);

                cmdInsert.Parameters.AddWithValue("@" + fldHispanicLatino, newHHMItem.HispanicLatino);
                cmdInsert.Parameters.AddWithValue("@" + fldRefugeeImmigrant, newHHMItem.RefugeeImmigrant);
                cmdInsert.Parameters.AddWithValue("@" + fldLimitedEnglish, newHHMItem.LimitedEnglish);
                cmdInsert.Parameters.AddWithValue("@" + fldMilitaryService, newHHMItem.MilitaryService);
                cmdInsert.Parameters.AddWithValue("@" + fldDischargeStatus, newHHMItem.DischargeStatus);
                cmdInsert.Parameters.AddWithValue("@" + fldPartneredMarried, newHHMItem.PartneredMarried);
                cmdInsert.Parameters.AddWithValue("@" + fldLongTermHomeless, newHHMItem.LongTermHomeless);
                cmdInsert.Parameters.AddWithValue("@" + fldChronicallyHomeless, newHHMItem.ChronicallyHomeless);
                cmdInsert.Parameters.AddWithValue("@" + fldEmployed, newHHMItem.Employed);
                cmdInsert.Parameters.AddWithValue("@" + fldEmplolymentStatus, newHHMItem.EmplolymentStatus);
                cmdInsert.Parameters.AddWithValue("@" + fldAmericanIndian, newHHMItem.AmericanIndian);
                cmdInsert.Parameters.AddWithValue("@" + fldAlaskaNative, newHHMItem.AlaskaNative);
                cmdInsert.Parameters.AddWithValue("@" + fldIndigenousToAmericas, newHHMItem.IndigenousToAmericas);
                cmdInsert.Parameters.AddWithValue("@" + fldAsianIndian, newHHMItem.AsianIndian);
                cmdInsert.Parameters.AddWithValue("@" + fldCambodian, newHHMItem.Cambodian);
                cmdInsert.Parameters.AddWithValue("@" + fldChinese, newHHMItem.Chinese);
                cmdInsert.Parameters.AddWithValue("@" + fldFilipino, newHHMItem.Filipino);
                cmdInsert.Parameters.AddWithValue("@" + fldJapanese, newHHMItem.Japanese);
                cmdInsert.Parameters.AddWithValue("@" + fldKorean, newHHMItem.Korean);
                cmdInsert.Parameters.AddWithValue("@" + fldVietnamese, newHHMItem.Vietnamese);
                cmdInsert.Parameters.AddWithValue("@" + fldOtherAsian, newHHMItem.OtherAsian);
                cmdInsert.Parameters.AddWithValue("@" + fldIndigenousAfricanBlack, newHHMItem.IndigenousAfricanBlack);
                cmdInsert.Parameters.AddWithValue("@" + fldAfricanAmericanBlack, newHHMItem.AfricanAmericanBlack);
                cmdInsert.Parameters.AddWithValue("@" + fldOtherBlack, newHHMItem.OtherBlack);
                cmdInsert.Parameters.AddWithValue("@" + fldHawaiianNative, newHHMItem.HawaiianNative);
                cmdInsert.Parameters.AddWithValue("@" + fldPolynesian, newHHMItem.Polynesian);
                cmdInsert.Parameters.AddWithValue("@" + fldMicronesian, newHHMItem.Micronesian);
                cmdInsert.Parameters.AddWithValue("@" + fldOtherPacificIslander, newHHMItem.OtherPacificIslander);
                cmdInsert.Parameters.AddWithValue("@" + fldArabIranianMiddleEastern, newHHMItem.ArabIranianMiddleEastern);
                cmdInsert.Parameters.AddWithValue("@" + fldOtherWhiteCaucasian, newHHMItem.OtherWhiteCaucasian);
                cmdInsert.Parameters.AddWithValue("@" + fldEthnicOther, newHHMItem.EthnicOther);
                cmdInsert.Parameters.AddWithValue("@" + fldEthnicUnknown, newHHMItem.EthnicUnknown);
                cmdInsert.Parameters.AddWithValue("@" + fldEducationLevel, newHHMItem.EducationLevel);
                cmdInsert.Parameters.AddWithValue("@" + fldHomeless, newHHMItem.Homeless);
                cmdInsert.Parameters.AddWithValue("@" + fldHomelessNbrTimes, newHHMItem.HomelessNbrTimes);
                cmdInsert.Parameters.AddWithValue("@" + fldHomelessNbrMonths, newHHMItem.HomelessNbrMonths);
                cmdInsert.Parameters.AddWithValue("@ID", 0);
                cmdInsert.Parameters["@ID"].Direction = ParameterDirection.Output;
                cmdInsert.ExecuteNonQuery();
                newID = Convert.ToInt32(cmdInsert.Parameters["@ID"].Value);
                if (conn.State != ConnectionState.Closed)
                { conn.Close(); }
                return newID;
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
            return 0;
        }
        public DataRow addHHMember(int idHH, string nameLast)
        {
            DataRow newRow = dset.Tables[tblHhMName].NewRow();
            newRow[fldID] = 0;
            newRow[fldInactive] = 0;
            newRow[fldHouseholdID] = idHH;
            newRow[fldLastName] = nameLast;
            newRow[fldFirstName] = "";
            newRow[fldSex] = "";
            newRow[fldBirthDate] = CCFBGlobal.FBNullDateValue;
            newRow[fldAgeGroup] = 0;
            newRow[fldSpecialDiet] = 0;
            newRow[fldNotes] = "";
            newRow[fldWorksInArea] = 0;
            newRow[fldEmployer] = "";
            newRow[fldEmpAddress] = "";
            newRow[fldEmpCity] = "";
            newRow[fldEmpZipcode] = "";
            newRow[fldEmpPhone] = "";
            newRow[fldCreated] = DateTime.Now;
            newRow[fldCreatedBy] = CCFBGlobal.dbUserName;
            newRow[fldModified] = CCFBGlobal.FBNullDateValue;
            newRow[fldModifiedBy] = "";
            newRow[fldUserFlag0] = 0;
            newRow[fldUserFlag1] = 0;
            newRow[fldVolunteersAtFoodBank] = 0;
            newRow[fldAge] = 0;
            newRow[fldUseAge] = 0;
            newRow[fldNotIncludedInClientList] = 0;
            newRow[fldCSFP] = 0;
            newRow[fldHeadHH] = 0;
            newRow[fldLanguage] = 0;
            newRow[fldIsDisabled] = 0;
            newRow[fldCSFPExpiration] = CCFBGlobal.FBNullDateValue;
            newRow[fldCSFPComments] = "";
            newRow[fldCSFPRoute] = 0;
            newRow[fldCSFPStatus] = 0;
            newRow[fldMemIDNbr] = "";
            newRow[fldMemIDType] = 0;
            newRow[fldRace] = 0;
            newRow[fldHispanic] = 0;
            newRow[fldBackPack] = false;
            newRow[fldBPExpiration] = CCFBGlobal.FBNullDateValue;
            newRow[fldBPSize] = 1;
            newRow[fldBPSchool] = 0;
            newRow[fldBPNotes] = "";
            newRow[fldNotCounted] = 0;
            newRow[fldRelationship] = 0;
            newRow[fldPhone] = "";
            newRow[fldEmailAddress] = "";
            newRow[fldGrade] = -1;
            newRow[fldSchSupply] = false;
            newRow[fldSchSupplyDelivered] = CCFBGlobal.FBNullDateValue;
            newRow[fldSchSupplySchool] = 0;
            return newRow;
        }

        public DataRow addHHMDemographics(int idHHM)
        {
            DataRow newRow = dset.Tables[tblDemoName].NewRow();
            newRow[fldID] = idHHM;
            newRow[fldMilitaryService] = 0;
            newRow[fldDischargeStatus] = 0;
            newRow[fldHispanicLatino] = 2;
            newRow[fldRefugeeImmigrant] = 2;
            newRow[fldLimitedEnglish] = 2;
            newRow[fldPartneredMarried] = 2;
            newRow[fldLongTermHomeless] = 2;
            newRow[fldChronicallyHomeless] = 2;
            newRow[fldEmployed] = 2;
            newRow[fldEmplolymentStatus] = 0;
            newRow[fldAmericanIndian] = false;
            newRow[fldAlaskaNative] = false;
            newRow[fldIndigenousToAmericas] = false;
            newRow[fldAsianIndian] = false;
            newRow[fldCambodian] = false;
            newRow[fldChinese] = false;
            newRow[fldFilipino] = false;
            newRow[fldJapanese] = false;
            newRow[fldKorean] = false;
            newRow[fldVietnamese] = false;
            newRow[fldOtherAsian] = false;
            newRow[fldIndigenousAfricanBlack] = false;
            newRow[fldAfricanAmericanBlack] = false;
            newRow[fldOtherBlack] = false;
            newRow[fldHawaiianNative] = false;
            newRow[fldPolynesian] = false;
            newRow[fldMicronesian] = false;
            newRow[fldOtherPacificIslander] = false;
            newRow[fldArabIranianMiddleEastern] = false;
            newRow[fldOtherWhiteCaucasian] = false;
            newRow[fldEthnicOther] = false;
            newRow[fldEthnicUnknown] = false;
            newRow[fldEducationLevel] = 0;
            newRow[fldHomeless] = 2;
            newRow[fldHomelessNbrTimes] = 0;
            newRow[fldHomelessNbrMonths] = 0;
            return newRow;
        }
    }
    #endregion

    /// <summary>
    /// 
    /// </summary>
    #region HHMemberItem Class
    public class HHMemberItem
    {
        DataRow drowHhMItem = null;
        DataRow drowItemDemographics = null;
        DataColumnCollection dcolHhM = null;
        DataColumnCollection dcolDemographics = null;

        public HHMemberItem(DataRow drowInHHM, DataColumnCollection dcolInHhM, DataRow drowInDemographics, DataColumnCollection dcolInDemographics)
        {
            drowHhMItem = drowInHHM;
            dcolHhM = dcolInHhM;
            drowItemDemographics = drowInDemographics;
            dcolDemographics = dcolInDemographics;
        }

        public bool HasChanges
        {
            get
            {
                if (drowHhMItem.RowState == DataRowState.Unchanged
                    && drowItemDemographics.RowState == DataRowState.Unchanged)
                {
                    return false;
                }
                return true;
            }
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
            get
            {
                if (drowHhMItem["BPExpiration"].ToString() == "")
                    return CCFBGlobal.FBNullDateValue;
                else
                    return (DateTime)drowHhMItem["BPExpiration"];
            }
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
        public int Grade
        {
            get { return Convert.ToInt32(drowHhMItem["Grade"]); }
            set { drowHhMItem["Grade"] = value; }
        }
        public bool SchSupply
        {
            get { return (bool)drowHhMItem["SchSupply"]; }
            set { drowHhMItem["SchSupply"] = value; }
        }
        public DateTime SchSupplyDelivered
        {
            get
            {
                if (drowHhMItem["SchSupplyDelivered"].ToString() == "")
                    return CCFBGlobal.FBNullDateValue;
                else
                    return (DateTime)drowHhMItem["SchSupplyDelivered"];
            }
            set { drowHhMItem["SchSupplyDelivered"] = value; }
        }
        public int SchSupplySchool
        {
            get { return Convert.ToInt32(drowHhMItem["SchSupplySchool"]); }
            set { drowHhMItem["SchSupplySchool"] = value; }
        }
        public int CSFPStatus
        {
            get { return Convert.ToInt32(drowHhMItem["CSFPStatus"]); }
            set { drowHhMItem["CSFPStatus"] = value; }
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
            get { return Convert.ToInt32(drowItemDemographics["Homeless"]); }
            set { drowItemDemographics["Homeless"] = value; }
        }
        public int HomelessNbrTimes
        {
            get { return Convert.ToInt32(drowItemDemographics["HomelessNbrTimes"]); }
            set { drowItemDemographics["HomelessNbrTimes"] = value; }
        }
        public int HomelessNbrMonths
        {
            get { return Convert.ToInt32(drowItemDemographics["HomelessNbrMonths"]); }
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
                    drowHhMItem[FieldName] = FieldValue;
                }
            }
            else if (dcolDemographics.IndexOf(FieldName) > 0)
            {
                if (drowItemDemographics[FieldName].ToString() != FieldValue)
                {
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
                    drowHhMItem[FieldName] = FieldValue;
                }
            }
            else if (dcolDemographics.IndexOf(FieldName) >= 0)
            {
                if (Convert.ToBoolean(drowItemDemographics[FieldName]) != FieldValue)
                {
                    drowItemDemographics[FieldName] = FieldValue;
                }
            }
        }

        //Gets data value from database using the selected data row
        public object GetDataValue(string FieldName)
        {
            if (drowHhMItem != null && dcolHhM != null)
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
    #endregion
}

