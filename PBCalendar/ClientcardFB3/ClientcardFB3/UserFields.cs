using System;
using System.Data;
using System.Data.SqlClient;

namespace ClientcardFB3
{
    public class UserFields
    {
        string connString;
        SqlDataAdapter dadAdpt;
        DataSet dset;
        SqlCommand command;
        SqlConnection conn;
        static string tbName = "UserFields";
        bool isValid = false;
        int iRowCount = 0;
        DataRow drow;

        public UserFields(string connStringIn)
        {
            conn = new SqlConnection();
            connString = connStringIn;
            conn.ConnectionString = connString;
            dset = new DataSet();
            dadAdpt = new SqlDataAdapter();
        }

        #region Get/Set Accessors
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

        public bool IsValid
        {
            get
            {
                return isValid;
            }
            set
            {
                isValid = value;
            }
        }

        public int RowCount
        {
            get
            {
                return iRowCount;
            }
            set
            {
                iRowCount = value;
            }
        }

        public string TableName
        {
            get
            {
                return drow["TableName"].ToString();
            }
            set
            {
                drow["TableName"] = value;
            }
        }

        public string ControlType
        {
            get
            {
                return drow["ControlType"].ToString();
            }
            set
            {
                drow["ControlType"] = value;
            }
        }

        public string FldName
        {
            get
            {
                return drow["FldName"].ToString();
            }
            set
            {
                drow["FldName"] = value;
            }
        }

        public string FldVal
        {
            get
            {
                return drow["FldVal"].ToString();
            }
            set
            {
                drow["FldVal"] = value;
            }
        }

        public string EditLabel
        {
            get
            {
                return drow["EditLabel"].ToString();
            }
            set
            {
                drow["EditLabel"] = value;
            }
        }

        public string EditTip
        {
            get
            {
                return drow["EditTip"].ToString();
            }
            set
            {
                drow["EditTip"] = value;
            }
        }

        public string FldType
        {
            get
            {
                return drow["FldType"].ToString();
            }
            set
            {
                drow["FldType"] = value;
            }
        }

        public int ControlWidth
        {
            get
            {
                return Convert.ToInt32(drow["ControlWidth"]);
            }
            set
            {
                drow["ControlWidth"] = value;
            }
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
            drow[FieldName] = FieldValue;
        }
        //Sets data value when value is a bool
        public void SetDataValue(string FieldName, bool FieldValue)
        {
            drow[FieldName] = FieldValue;
        }
        
        //Gets property through use of just the collum name in database
        public object GetDataValue(string FieldName)
        {
            if (drow != null)
                return drow[FieldName];

            return "";
        }

        public object GetDataValue(string FieldName, int rowNum)
        {
            try
            {
                return dset.Tables[tbName].Rows[rowNum][FieldName];
            }
            catch (IndexOutOfRangeException ex)
            {
                CCFBGlobal.appendErrorToErrorReport("FieldName=" + FieldName, ex.GetBaseException().ToString(),
                    CCFBGlobal.serverName);
                return "";
            }
        }

        #endregion

        public bool open(string tableName)
        {
            try
            {
                openConnection();
                command = new SqlCommand("SELECT * FROM " + tbName + " WHERE TableName='" + tableName.ToString() + "'", conn);
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
                closeConnection();
                CCFBGlobal.appendErrorToErrorReport("TableName=" + tableName, ex.GetBaseException().ToString(),
                    CCFBGlobal.serverName);
                iRowCount = 0;
                return isValid = false;
            }
        }

        public void openAll()
        {
            try
            {
                openConnection();
                command = new SqlCommand("SELECT * FROM " + tbName, conn);
                dadAdpt.SelectCommand = command;
                dset.Clear();
                iRowCount = dadAdpt.Fill(dset, tbName);
                closeConnection();
                if (iRowCount > 0)
                {
                    drow = dset.Tables[tbName].Rows[0];
                }
            }
            catch (SqlException ex)
            {
                closeConnection();
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString(),
                    CCFBGlobal.serverName);
                iRowCount = 0;
            }
        }

        public void setDataRow(int rowIndex)
        {
            if (rowIndex < iRowCount && rowIndex >= 0)
                drow = dset.Tables[tbName].Rows[rowIndex];
        }

        public void setDataRow(String fldName)
        {
            for (int i = 0; i < iRowCount; i++)
            {
                if (fldName == dset.Tables[tbName].Rows[i].Field<string>("FldName"))
                {
                    drow = dset.Tables[tbName].Rows[i];
                    return;
                }
            }
        }

        public void delete(string tableName)
        {
            SqlCommand commDelete = new SqlCommand(" DELETE FROM " + tbName + " WHERE TableName=" + tableName, conn);
            openConnection();
            commDelete.ExecuteNonQuery();
            closeConnection();
        }

        public void update()
        {
            if (dset.HasChanges() == true)
            {
                try
                {
                    openConnection();
                    if (dadAdpt.UpdateCommand == null)
                    {
                        SqlCommandBuilder commBuilder = new SqlCommandBuilder(dadAdpt);
                    }
                    dadAdpt.Update(dset, "UserFields");
                    closeConnection();
                }
                catch (SqlException ex) 
                {
                    closeConnection();
                    CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString(),
                        CCFBGlobal.serverName);
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
}

