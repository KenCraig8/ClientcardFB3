using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace ClientcardFB3
{
    public class HHMemberItem
    {
        DataRow dRow;

        public HHMemberItem(DataRow newDRow)
        {
            dRow = newDRow;
        }
        
        #region Get/Set Accessors
        public int ID
        {
            get { return Convert.ToInt32(dRow["ID"]); }
            set { dRow["ID"] = value; }
        }
        public bool Inactive
        {
            get { return (bool)dRow["Inactive"]; }
            set { dRow["Inactive"] = value; }
        }
        public int HouseholdID
        {
            get { return Convert.ToInt32(dRow["HouseholdID"]); }
            set { dRow["HouseholdID"] = value; }
        }
        public string LastName
        {
            get { return dRow["LastName"].ToString(); }
            set { dRow["LastName"] = value; }
        }
        public string FirstName
        {
            get { return dRow["FirstName"].ToString(); }
            set { dRow["FirstName"] = value; }
        }
        public string Sex
        {
            get { return dRow["Sex"].ToString(); }
            set { dRow["Sex"] = value; }
        }
        public DateTime Birthdate
        {
            get
            {
                try
                {
                    return Convert.ToDateTime(dRow["Birthdate"]);
                }
                catch { return DateTime.MaxValue; }
            }
            set { dRow["Birthdate"] = value; }
        }
        public int AgeGroup
        {
            get { return Convert.ToInt32(dRow["AgeGroup"]); }
            set { dRow["AgeGroup"] = value; }
        }
        public bool SpecialDiet
        {
            get { return (bool)dRow["SpecialDiet"]; }
            set { dRow["SpecialDiet"] = value; }
        }
        public string Notes
        {
            get { return dRow["Notes"].ToString(); }
            set { dRow["Notes"] = value; }
        }
        public bool WorksInArea
        {
            get { return (bool)dRow["WorksInArea"]; }
            set { dRow["WorksInArea"] = value; }
        }
        public string Employer
        {
            get { return dRow["Employer"].ToString(); }
            set { dRow["Employer"] = value; }
        }
        public string EmpAddress
        {
            get { return dRow["EmpAddress"].ToString(); }
            set { dRow["EmpAddress"] = value; }
        }
        public string EmpCity
        {
            get { return dRow["EmpCity"].ToString(); }
            set { dRow["EmpCity"] = value; }
        }
        public string EmpZipcode
        {
            get { return dRow["EmpZipcode"].ToString(); }
            set { dRow["EmpZipcode"] = value; }
        }
        public string EmpPhone
        {
            get { return dRow["EmpPhone"].ToString(); }
            set { dRow["EmpPhone"] = value; }
        }
        public DateTime Created
        {
            get { return Convert.ToDateTime(dRow["Created"]); }
            set { dRow["Created"] = value; }
        }
        public string CreatedBy
        {
            get { return dRow["CreatedBy"].ToString(); }
            set { dRow["CreatedBy"] = value; }
        }
        public DateTime Modified
        {
            get { return Convert.ToDateTime(dRow["Modified"]); }
            set { dRow["Modified"] = value; }
        }
        public string ModifiedBy
        {
            get { return dRow["ModifiedBy"].ToString(); }
            set { dRow["ModifiedBy"] = value; }
        }
        public bool UserFlag0
        {
            get { return (bool)dRow["UserFlag0"]; }
            set { dRow["UserFlag0"] = value; }
        }
        public bool UserFlag1
        {
            get { return (bool)dRow["UserFlag1"]; }
            set { dRow["UserFlag1"] = value; }
        }
        public bool VolunteersAtFoodBank
        {
            get { return (bool)dRow["VolunteersAtFoodBank"]; }
            set { dRow["VolunteersAtFoodBank"] = value; }
        }
        public int Age
        {
            get { return Convert.ToInt32(dRow["Age"]); }
            set { dRow["Age"] = value; }
        }
        public bool UseAge
        {
            get { return (bool)dRow["UseAge"]; }
            set { dRow["UseAge"] = value; }
        }
        public bool NotIncludedInClientList
        {
            get { return (bool)dRow["NotIncludedInClientList"]; }
            set { dRow["NotIncludedInClientList"] = value; }
        }
        public bool CSFP
        {
            get { return (bool)dRow["CSFP"]; }
            set { dRow["CSFP"] = value; }
        }
        public bool HeadHH
        {
            get { return (bool)dRow["HeadHH"]; }
            set { dRow["HeadHH"] = value; }
        }
        public int Language
        {
            get { return Convert.ToInt32(dRow["Language"]); }
            set { dRow["Language"] = value; }
        }
        public bool IsDisabled
        {
            get { return (bool)dRow["IsDisabled"]; }
            set { dRow["IsDisabled"] = value; }
        }
        public DateTime CSFPExpiration
        {
            get { return Convert.ToDateTime(dRow["CSFPExpiration"]); }
            set { dRow["CSFPExpiration"] = value; }
        }
        public string CSFPComments
        {
            get { return dRow["CSFPComments"].ToString(); }
            set { dRow["CSFPComments"] = value; }
        }
        #endregion Get/Set Accessors

    }
}
