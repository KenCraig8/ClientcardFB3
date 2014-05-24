using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace ClientCardFB3
{
    public class VolunteerGroups
    {
        string connString;
        SqlDataAdapter dadAdpt;
        DataSet dset;
        SqlCommand command;
        SqlConnection conn;
        static string tbName = "VolunteerGroups";
        bool isValid = false;
        int iRowCount = 0;
        
        public VolunteerGroups(string connStringIn)
        {
            conn = new SqlConnection();
            connString = connStringIn;
            conn.ConnectionString = connString;
            dset = new DataSet();
            dadAdpt = new SqlDataAdapter();
        }

        #region Get/Set Accessors

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

        public int RowCount
        {
            get
            {
                return iRowCount;
            }
        }
        public int UID
        {
            get
            {
                return dset.Tables[tbName].Rows[0].Field<System.Int32>("UID");
            }
            set
            {
                dset.Tables[tbName].Rows[0]["UID"] = value;
            }
        }

        public int VolID
        {
            get
            {
                return dset.Tables[tbName].Rows[0].Field<System.Int32>("VolID");
            }
            set
            {
                dset.Tables[tbName].Rows[0]["VolID"] = value;
            }
        }

        public int GroupID
        {
            get
            {
                return dset.Tables[tbName].Rows[0].Field<System.Int32>("GroupID");
            }
            set
            {
                dset.Tables[tbName].Rows[0]["GroupID"] = value;
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
            dset.Tables[tbName].Rows[0][FieldName] = FieldValue;
        }
        //Sets data value when value is a string
        public void SetDataValue(string FieldName, string FieldValue, int rowIndex)
        {
            dset.Tables[tbName].Rows[rowIndex][FieldName] = FieldValue;
        }
        //Sets data value when value is a int
        public void SetDataValue(string FieldName, int FieldValue)
        {
            dset.Tables[tbName].Rows[0][FieldName] = FieldValue;
        }
        //Sets data value when value is a bool
        public void SetDataValue(string FieldName, bool FieldValue)
        {
            dset.Tables[tbName].Rows[0][FieldName] = FieldValue;
        }
        //Sets data value when value is a DateTime
        public void SetDataValue(string FieldName, DateTime FieldValue)
        {
            dset.Tables[tbName].Rows[0][FieldName] = FieldValue;
        }
        //Sets data value when value is a float
        public void SetDataValue(string FieldName, float FieldValue)
        {
            dset.Tables[tbName].Rows[0][FieldName] = FieldValue;
        }
        //Gets property through use of just the collum name in database
        public object GetDataValue(string FieldName)
        {
            try
            {
                return dset.Tables[tbName].Rows[0][FieldName];
            }
            catch (IndexOutOfRangeException ex)
            {
                CCFBGlobal.appendErrorToErrorReport("FiledName=" + FieldName, ex.GetBaseException().ToString());
                return "";
            }
        }
        #endregion

        public bool open(System.Int32 key)
        {
            try
            {
                openConnection();
                command = new SqlCommand("SELECT * FROM " + tbName + " WHERE UID=" + key.ToString(), conn);
                dadAdpt.SelectCommand = command;
                dset.Clear();
                iRowCount = dadAdpt.Fill(dset, tbName);
                closeConnection(); 
                
                if (iRowCount > 0)
                    return isValid = true;

                return isValid = false;
            }
            catch (SqlException ex) 
            {
                iRowCount = 0;
                CCFBGlobal.appendErrorToErrorReport("key=" + key.ToString(),
                    ex.GetBaseException().ToString(), CCFBGlobal.serverName);
                closeConnection();
                iRowCount = 0;
                return isValid = false;
            }
        }

        public void openWhere(string whereClause)
        {
            try
            {
                openConnection();
                command = new SqlCommand("SELECT * FROM " + tbName + " " + whereClause, conn);
                dadAdpt.SelectCommand = command;
                dset.Clear();
                iRowCount = dadAdpt.Fill(dset, tbName);
                closeConnection();
            }
            catch (SqlException ex)
            {
                CCFBGlobal.appendErrorToErrorReport("whereClause=" + whereClause,
                    ex.GetBaseException().ToString(), CCFBGlobal.serverName);
                closeConnection();
                iRowCount = 0;
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
            }
            catch (SqlException ex)
            {
                CCFBGlobal.appendErrorToErrorReport("",
                    ex.GetBaseException().ToString(), CCFBGlobal.serverName);
                closeConnection();
                iRowCount = 0;
            }
        }

        public void delete(System.Int32 key)
        {
            SqlCommand commDelete = new SqlCommand(" DELETE FROM VolunteerGroups WHERE UID=" + key.ToString(), conn);
            command = new SqlCommand("SELECT * FROM VolunteerGroups WHERE UID=" + key.ToString(), conn);
            dadAdpt.DeleteCommand = commDelete;
            dadAdpt.SelectCommand = command;
            dset.Clear();
            dadAdpt.Fill(dset, tbName);
            dset.Tables[tbName].Rows[0].Delete();
            dadAdpt.Update(dset, tbName);
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
                    dadAdpt.Update(dset, tbName);
                    closeConnection();
                }
                catch (SqlException ex)
                {
                    CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString(),
                        CCFBGlobal.serverName);
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
}

