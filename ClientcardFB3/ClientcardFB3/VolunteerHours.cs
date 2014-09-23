using System;
using System.Data;
using System.Data.SqlClient;

namespace ClientcardFB3
{
    public class VolunteerHours
    {
        string connString;
        SqlDataAdapter dadAdpt;
        DataSet dset;
        SqlCommand command;
        SqlCommand readOnlyCommand;
        SqlConnection conn;
        static string tbName = "VolunteerHours";
        bool isValid = false;
        SqlCommandBuilder commBuilder;
        int iRowCount = 0;
        DataRow drow = null;
        string swhereclause = "";
        public VolunteerHours(string connStringIn)
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

        public DateTime TrxDate
        {
            get { return (DateTime)drow["TrxDate"]; }
            set { drow["TrxDate"] = value; }
        }

        public int VolId
        {
            get { return Convert.ToInt32(drow["VolId"]); }
            set { drow["VolId"] = value; }
        }

        public int NumVolunteers
        {
            get { return Convert.ToInt32(drow["NumVolunteers"]); }
            set { drow["NumVolunteers"] = value; }
        }

        public Single NumVolHours
        {
            get { return Convert.ToSingle(drow["NumVolHours"]); }
            set { drow["NumVolHours"] = value; }
        }

        public string VolTimeIn
        {
            get { return drow["VolTimeIn"].ToString(); }
            set { drow["VolTimeIn"] = value; }
        }

        public string VolTimeOut
        {
            get { return drow["VolTimeOut"].ToString(); }
            set { drow["VolTimeOut"] = value; }
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
        //Sets data value when value is a string
        public void SetDataValue(string FieldName, string FieldValue, int rowIndex)
        {
            dset.Tables[tbName].Rows[rowIndex][FieldName] = FieldValue;
        }

        //Gets property through use of just the collum name in database
        public object GetDataValue(string FieldName)
        {
            return drow[FieldName];
        }
        #endregion

        public void find(int ID)
        {
            for (int i = 0; i < iRowCount; i++)
            {
                if (ID == dset.Tables[tbName].Rows[i].Field<int>("ID"))
                {
                    drow = dset.Tables[tbName].Rows[i];
                    break;
                }
            }
        }

        public bool checkExistingDateForVol(DateTime trxDate, int volID)
        {
            int nbrRcds = (int)CCFBGlobal.getSQLValue("Select Count(*) From " + tbName + " Where TrxDate = '" + trxDate + "' And VolID = " + volID.ToString());
            if (nbrRcds == 0)
            {
                return false;
            }

            return true;
        }

        public bool findVolId(int volID)
        {
            for (int i = 0; i < iRowCount; i++)
            {
                if (volID == dset.Tables[tbName].Rows[i].Field<int>("VolId"))
                {
                    drow = dset.Tables[tbName].Rows[i];
                    return true;
                }
            }
            return false;
        }

        public void setDataRow(int rowIndex)
        {
            if (rowIndex < iRowCount)
                drow = dset.Tables[tbName].Rows[rowIndex];
        }

        public bool open(int ID)
        {
            try
            {
                openConnection();
                command = new SqlCommand("SELECT * FROM " + tbName + " WHERE ID=" + ID.ToString(), conn);
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
                CCFBGlobal.appendErrorToErrorReport("ID="+ID.ToString(), ex.GetBaseException().ToString());
                closeConnection();
                iRowCount = 0;
                return isValid = false; 
            }
        }

        public void openWhere(string sWhereClause)
        {
            try
            {
                swhereclause = sWhereClause;
                openConnection();
                command = new SqlCommand("SELECT * FROM " + tbName 
                    + " " + sWhereClause, conn);
                dadAdpt.SelectCommand = command;
                dset.Clear();
                iRowCount = dadAdpt.Fill(dset, tbName);
                closeConnection();
                isValid = false;
                if (iRowCount > 0)
                {
                    drow = dset.Tables[tbName].Rows[0];
                }
            }
            catch (SqlException ex)
            {
                closeConnection(); 
                CCFBGlobal.appendErrorToErrorReport(sWhereClause, ex.GetBaseException().ToString());
                isValid = false;
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
                if (iRowCount > 0)
                {
                    drow = dset.Tables[tbName].Rows[0];
                }
            }
            catch (SqlException ex) 
            { 
                closeConnection();
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString());
                iRowCount = 0;
            }
        }

        public void delete(System.Int32 id)
        {
            SqlCommand commDelete = new SqlCommand("DELETE FROM " + tbName + " WHERE ID=" + id.ToString(), conn);
            openConnection();
            commDelete.ExecuteNonQuery();
            closeConnection();
        }

        public void delete(DateTime dateToDelete)
        {
            SqlCommand commDelete = new SqlCommand("DELETE FROM " + tbName + " WHERE TrxDate='" + dateToDelete.ToShortDateString() + "'", conn);
            openConnection();
            commDelete.ExecuteNonQuery();
            closeConnection();
        }

        public bool insert()
        {
            int newRowId = 0;
            if (dadAdpt.UpdateCommand == null || dadAdpt.InsertCommand == null)
            {
                commBuilder = new SqlCommandBuilder(dadAdpt);
            }

            try
            {
                openConnection();
                dadAdpt.Update(dset, tbName);
                openWhere(swhereclause);
                return true;
            }
            catch (SqlException ex) 
            {
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString());
                closeConnection(); 
                return false; 
            }
        }

        public void update()
        {
            if (dset.HasChanges() == true)
            {
                try
                {
                    openConnection();
                    
                    if(dadAdpt.UpdateCommand == null)
                         commBuilder = new SqlCommandBuilder(dadAdpt);
                   
                    dadAdpt.Update(dset, "VolunteerHours");
                    closeConnection();
                }
                catch (SqlException ex) 
                {
                    closeConnection();
                    CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString());
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

