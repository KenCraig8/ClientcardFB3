
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

namespace ClientcardFB3
{
    public class ServiceItems
    {
        string connString;
        SqlDataAdapter dadAdpt;
        DataSet dset;
        SqlCommand command;
        System.Data.SqlClient.SqlConnection conn;
        static string tbName = "ServiceItems";
        bool bisValid = false;
        DataRow drow;
        int iRowCount = 0;

        public ServiceItems(string connStringIn)
        {
            conn = new System.Data.SqlClient.SqlConnection();
            connString = connStringIn;
            conn.ConnectionString = connString;
            dset = new DataSet();
            dadAdpt = new SqlDataAdapter();
        }

        #region Get/Set Accessors
        public bool isValid
        {
            get { return bisValid; }
            set { bisValid = value; }
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

        public int ItemKey
        {
            get { return Convert.ToInt32(drow["ItemKey"]); }
            set { drow["ItemKey"] = value; }
        }

        public string ItemDesc
        {
            get { return drow["ItemDesc"].ToString(); }
            set { drow["ItemDesc"] = value; }
        }

        public int ItemType
        {
            get { return Convert.ToInt32(drow["ItemType"]); }
            set { drow["ItemType"] = value; }
        }

        public int LbsPerItem
        {
            get { return Convert.ToInt32(drow["LbsPerItem"]); }
            set { drow["LbsPerItem"] = value; }
        }

        public int ItemRule
        {
            get { return Convert.ToInt32(drow["ItemRule"]); }
            set { drow["ItemRule"] = value; }
        }

        public bool NotAvailable
        {
            get { return (bool)drow["NotAvailable"]; }
            set { drow["NotAvailable"] = value; }
        }

        public int FS01
        {
            get { return Convert.ToInt32(drow["FS01"]); }
            set { drow["FS01"] = value; }
        }

        public int FS02
        {
            get { return Convert.ToInt32(drow["FS02"]); }
            set { drow["FS02"] = value; }
        }

        public int FS03
        {
            get { return Convert.ToInt32(drow["FS03"]); }
            set { drow["FS03"] = value; }
        }

        public int FS04
        {
            get { return Convert.ToInt32(drow["FS04"]); }
            set { drow["FS04"] = value; }
        }

        public int FS05
        {
            get { return Convert.ToInt32(drow["FS05"]); }
            set { drow["FS05"] = value; }
        }

        public int FS06
        {
            get { return Convert.ToInt32(drow["FS06"]); }
            set { drow["FS06"] = value; }
        }

        public int FS07
        {
            get { return Convert.ToInt32(drow["FS07"]); }
            set { drow["FS07"] = value; }
        }
        public int FS08
        {
            get { return Convert.ToInt32(drow["FS08"]); }
            set { drow["FS08"] = value; }
        }
        public int FS09
        {
            get { return Convert.ToInt32(drow["FS09"]); }
            set { drow["FS09"] = value; }
        }
        public int FS10
        {
            get { return Convert.ToInt32(drow["FS10"]); }
            set { drow["FS10"] = value; }
        }
        public int FS11
        {
            get { return Convert.ToInt32(drow["FS11"]); }
            set { drow["FS11"] = value; }
        }
        public int FS12
        {
            get { return Convert.ToInt32(drow["FS12"]); }
            set { drow["FS12"] = value; }
        }
        public int FS13
        {
            get { return Convert.ToInt32(drow["FS13"]); }
            set { drow["FS13"] = value; }
        }
        public int FS14
        {
            get { return Convert.ToInt32(drow["FS14"]); }
            set { drow["FS14"] = value; }
        }
        public int FS15
        {
            get { return Convert.ToInt32(drow["FS15"]); }
            set { drow["FS15"] = value; }
        }
        public int FS16
        {
            get { return Convert.ToInt32(drow["FS16"]); }
            set { drow["FS16"] = value; }
        }
        public int FS17
        {
            get { return Convert.ToInt32(drow["FS17"]); }
            set { drow["FS17"] = value; }
        }
        public int FS18
        {
            get { return Convert.ToInt32(drow["FS18"]); }
            set { drow["FS18"] = value; }
        }
        public int FS19
        {
            get { return Convert.ToInt32(drow["FS19"]); }
            set { drow["FS19"] = value; }
        }
        public int FS20
        {
            get { return Convert.ToInt32(drow["FS20"]); }
            set { drow["FS20"] = value; }
        }

        public DateTime Created
        {
            get { return (DateTime)drow["Created"]; }
            set { drow["Created"] = value; }
        }
        public string CreatedBy
        {
            get { return drow["CreatedBy"].ToString(); }
            set { drow["CreatedBy"] = value; }
        }
        public DateTime Modified
        {
            get
            {
                if (drow["Modified"].ToString() == "")
                    return CCFBGlobal.FBNullDateValue;
                else
                    return (DateTime)drow["Modified"];
            }
            set { drow["Modified"] = value; }
        }
        public string ModifiedBy
        {
            get { return drow["ModifiedBy"].ToString(); }
            set { drow["ModifiedBy"] = value; }
        }
        public bool UseAgeGroup
        {
            get { return (bool)drow["UseAgeGroup"]; }
            set { drow["UseAgeGroup"] = value; }
        }
        public bool AllowInfants
        {
            get { return (bool)drow["AllowInfants"]; }
            set { drow["AllowInfants"] = value; }
        }
        public bool AllowYouth
        {
            get { return (bool)drow["AllowYouth"]; }
            set { drow["AllowYouth"] = value; }
        }
        public bool AllowTeens
        {
            get { return (bool)drow["AllowTeens"]; }
            set { drow["AllowTeens"] = value; }
        }
        public bool AllowAdults
        {
            get { return (bool)drow["AllowAdults"]; }
            set { drow["AllowAdults"] = value; }
        }
        public bool AllowSeniors
        {
            get { return (bool)drow["AllowSeniors"]; }
            set { drow["AllowSeniors"] = value; }
        }
        public string Mask
        {
            get { return drow["SvcMask"].ToString(); }
            set { drow["SvcMask"] = value; }
        }
        public bool Exclusive
        {
            get { return (bool)drow["Exclusive"]; }
            set { drow["Exclusive"] = value; }
        }
        public int DefaultSvcGrp
        {
            get { return Convert.ToInt32(drow["DefaultSvcGrp"]); }
            set { drow["DefaultSvcGrp"] = value; }
        }
        public bool FastTrack
        {
            get { return (bool)drow["FastTrack"]; }
            set { drow["FastTrack"] = value; }
        }
        public bool PrintReceipt
        {
            get { return (bool)drow["PrintReceipt"]; }
            set { drow["PrintReceipt"] = value; }
        }
        public bool FullService
        {
            get { return (bool)drow["FullService"]; }
            set { drow["FullService"] = value; }
        }
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
            if (dset.Tables[tbName].Columns.Contains(FieldName) == true)
                drow[FieldName] = FieldValue;
        }
        //Sets data value when value is a int
        public void SetDataValue(string FieldName, int FieldValue)
        {
            if (dset.Tables[tbName].Columns.Contains(FieldName) == true)
                drow[FieldName] = FieldValue;
        }
        //Sets data value when value is a bool
        public void SetDataValue(string FieldName, bool FieldValue)
        {
            if (dset.Tables[tbName].Columns.Contains(FieldName) == true)
                drow[FieldName] = FieldValue;
        }
        //Sets data value when value is a DateTime
        public void SetDataValue(string FieldName, DateTime FieldValue)
        {
            if (dset.Tables[tbName].Columns.Contains(FieldName) == true)
                drow[FieldName] = FieldValue;
        }
        //Sets data value when value is a float
        public void SetDataValue(string FieldName, float FieldValue)
        {
            if (dset.Tables[tbName].Columns.Contains(FieldName) == true)
                drow[FieldName] = FieldValue;
        }
        //Gets property through use of just the collum name in database
        public bool GetDataValueBool(string FieldName)
        {
            try
            {
                if (dset.Tables[tbName].Columns.Contains(FieldName) == true)
                    return Convert.ToBoolean(drow[FieldName]);
                else
                    return false;
            }
            catch (IndexOutOfRangeException ex)
            {
                CCFBGlobal.appendErrorToErrorReport("FieldName=" + FieldName, ex.GetBaseException().ToString());
                return false;
            }
        }

        public bool GetDataValueBool(string FieldName, int rowIndex)
        {
            try
            {
                if (dset.Tables[tbName].Columns.Contains(FieldName) == true)
                    return Convert.ToBoolean(dset.Tables[tbName].Rows[rowIndex][FieldName]);
                else
                    return false;
            }
            catch (IndexOutOfRangeException ex)
            {
                CCFBGlobal.appendErrorToErrorReport("FieldName=" + FieldName, ex.GetBaseException().ToString());
                return false;
            }
        }
        
        public object GetDataValue(string FieldName)
        {
            try
            {
                if (dset.Tables[tbName].Columns.Contains(FieldName) == true)
                    return drow[FieldName];
                else
                    return "";
            }
            catch (IndexOutOfRangeException ex) 
            {
                CCFBGlobal.appendErrorToErrorReport("FieldName=" + FieldName, ex.GetBaseException().ToString());
                return "";
            }
        }

        public object GetDataValue(string FieldName, int rowIndex)
        {
            try
            {
                if (dset.Tables[tbName].Columns.Contains(FieldName) == true) 
                    return dset.Tables[tbName].Rows[rowIndex][FieldName];
                else
                    return "";
            }
            catch (IndexOutOfRangeException ex)
            {
                CCFBGlobal.appendErrorToErrorReport("FieldName=" + FieldName, ex.GetBaseException().ToString());
                return "";
            }
        }

        #endregion

        public void find(int ID)
        {
            for (int i = 0; i < iRowCount; i++)
            {
                if (ID == dset.Tables[tbName].Rows[i].Field<int>("ItemKey"))
                {
                    drow = dset.Tables[tbName].Rows[i];
                    return;
                }
            }
        }

        public int findMaxId()
        {
            int MaxId = -1;
            int MaxIdRowNbr = -1;
            for (int i = 0; i < iRowCount; i++)
            {
                if (MaxId < dset.Tables[tbName].Rows[i].Field<int>("ItemKey"))
                {
                    MaxId = dset.Tables[tbName].Rows[i].Field<int>("ItemKey");
                    MaxIdRowNbr = i;
                }
            }
            if (MaxId >=0)
                drow = dset.Tables[tbName].Rows[MaxIdRowNbr];
            return MaxId;
        }

        public bool open(int ItemKey)
        {
            try
            {
                openConnection();
                command = new SqlCommand("SELECT * FROM " + tbName + " WHERE ItemKey=" + ItemKey.ToString(), conn);
                dadAdpt.SelectCommand = command;
                dset.Clear();
                iRowCount = dadAdpt.Fill(dset, tbName);
                closeConnection();

                if (iRowCount > 0)
                {
                    drow = dset.Tables[tbName].Rows[0];
                    return isValid = true;
                }
                return isValid = false;
            }
            catch (SqlException ex) 
            {
                CCFBGlobal.appendErrorToErrorReport("key=" + ItemKey.ToString(), ex.GetBaseException().ToString());
                closeConnection(); return isValid = false; 
            }
        }

        public bool openWhere(String WhereClause)
        {
            try
            {
                openConnection();
                command = new SqlCommand("SELECT * FROM " + tbName );
                if (WhereClause != "")
                    { command.CommandText += " WHERE " + WhereClause ;}
                command.CommandText += " ORDER BY ItemType, ItemDesc";
                dadAdpt = new SqlDataAdapter(command);
                dadAdpt.SelectCommand = command;
                dset.Clear();
                dadAdpt.SelectCommand.Connection = conn;
                iRowCount = dadAdpt.Fill(dset, tbName);
                closeConnection();
                if (iRowCount > 0)
                {
                    drow = dset.Tables[tbName].Rows[0];
                }
                return false;
            }
            catch (SqlException ex) 
            {
                CCFBGlobal.appendErrorToErrorReport("", 
                    ex.GetBaseException().ToString());
                closeConnection(); 
                return isValid = false; 
            }
        }

        public int ItemUsedRecently(int itemKey)
        {
            string sTest = " like '%|" + itemKey.ToString() + "|%'";
            int iResult = (int)CCFBGlobal.getSQLValue("SELECT Count(*) FROM TrxLog WHERE ('|' + ConcatFoodSvcItemsList" + sTest 
                                                      + " OR '|' + ConcatNonFoodSvcItemsList" + sTest + ")"
                                                      + " AND TrxDate > '" + DateTime.Today.AddYears(-1).ToShortDateString() + "'");
            return iResult;
        }

        public void delete(System.Int32 ID)
        {
            SqlCommand commDelete = new SqlCommand(" DELETE FROM " + tbName + " WHERE ItemKey=" + ID.ToString(), conn);
            openConnection();
            commDelete.ExecuteNonQuery();
            closeConnection();
        }

        public void update(bool bReload)
        {
            if (dset.HasChanges() == true)
            {
                try
                {
                    openConnection();

                    if (dadAdpt.UpdateCommand == null || dadAdpt.InsertCommand == null)
                    {
                        SqlCommandBuilder commBuild = new SqlCommandBuilder(dadAdpt);
                    }

                    dadAdpt.Update(dset, tbName);
                    if (bReload == true)
                    {
                        dset.Clear();
                        iRowCount = dadAdpt.Fill(dset, tbName);
                    }
                    closeConnection();
                }
                catch (SqlException ex) 
                {
                    CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString());
                    closeConnection();
                }
            }
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
    }
    public class ServiceItem
    {
        DataRow drow;
        bool isSelected = false;

        //used as an index to the first FamilySizeMultiplyer in datarow
        const int fsmBase = 5;

        public ServiceItem(DataRow drowIn)
        {
            drow = drowIn;
            isSelected = false;
        }

        #region Get/Set Accessors

        public string Description
        {
            get { return drow["ItemDesc"].ToString(); }
            set { drow["Description"] = value; }
        }
        public int Rule
        {
            get { return Convert.ToInt32(drow["ItemRule"]); }
            set { drow["Rule"] = value; }
        }
        public int ItemKey
        {
            get { return Convert.ToInt32(drow["ItemKey"]); }
        }

        public bool UseAgeGroup
        {
            get { return (bool)drow["UseAgeGroup"]; }
            set { drow["UseAgeGroup"] = value; }
        }
        public bool AllowInfants
        {
            get { return (bool)drow["AllowInfants"]; }
            set { drow["AllowInfants"] = value; }
        }
        public bool AllowYouth
        {
            get { return (bool)drow["AllowYouth"]; }
            set { drow["AllowYouth"] = value; }
        }
        public bool AllowTeens
        {
            get { return (bool)drow["AllowTeens"]; }
            set { drow["AllowTeens"] = value; }
        }
        public bool AllowAdults
        {
            get { return (bool)drow["AllowAdults"]; }
            set { drow["AllowAdults"] = value; }
        }
        public bool AllowSeniors
        {
            get { return (bool)drow["AllowSeniors"]; }
            set { drow["AllowSeniors"] = value; }
        }
        public bool Exclusive
        {
            get { return Convert.ToBoolean(drow["Exclusive"]); }
            set { drow["Exclusive"] = value; }
        }
        public int DefaultSvcGrp
        {
            get { return Convert.ToInt32(drow["DefaultSvcGrp"]); }
            set { drow["DefaultSvcGrp"] = value; }
        }
        public bool FastTrack
        {
            get { return (bool)drow["FastTrack"]; }
            set { drow["FastTrack"] = value; }
        }
        public bool PrintReceipt
        {
            get { return (bool)drow["PrintReceipt"]; }
            set { drow["PrintReceipt"] = value; }
        }
        public bool FullService
        {
            get { return (bool)drow["FullService"]; }
            set { drow["FullService"] = value; }
        }

        public bool IsSelected
        {
            get { return isSelected; }
            set { isSelected = value; }
        }

        public int LbsPerItem
        {
            get { return Convert.ToInt32(drow["LbsPerItem"]); }
            set { drow["LbsPerItem"] = value; }
        }
        public int ItemType
        {
            get { return Convert.ToInt32(drow["ItemType"]); }
            set { drow["ItemType"] = value; }
        }
        public bool NotAvailable
        {
            get { return (bool)drow["NotAvailable"]; }
            set { drow["NotAvailable"] = value; }
        }
        #endregion

        public int getFamSizeMultiplyer(int fSize)
        {
            if (fSize != 0)
                return Convert.ToInt32(drow[fSize + fsmBase]);
            else
                return 0;
        }

        public int getFamSizeLbs(int fSize)
        {
            if (fSize != 0)
                return Convert.ToInt32(drow[fSize + fsmBase]) * LbsPerItem;
            else
                return 0;
        }

        public bool MaskArray(int nbrservices)
        {
            int index = nbrservices;
            if (index > 4)
                index = 4;
            return Convert.ToBoolean(Convert.ToInt32(drow["SvcMask"].ToString().Substring(index, 1)));
        }
    }
}

