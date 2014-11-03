
using System;
using System.Data;
using System.Data.SqlClient;

namespace ClientcardFB3
{
    public class HDRSClients
    {
        string connString;
        SqlDataAdapter dadAdpt;
        DataTable dtbl;
        SqlCommand command;
        System.Data.SqlClient.SqlConnection conn;
        DataRow drow;
        int iRowCount = 0;
        bool isValid = false;

        public HDRSClients(string connStringIn)
        {
            conn = new System.Data.SqlClient.SqlConnection();
            connString = connStringIn;
            conn.ConnectionString = connString;
            dtbl = new DataTable();
            dadAdpt = new SqlDataAdapter();
        }

        #region Get/Set Accessors
        public DataTable DTable
        {
            get { return dtbl; }
        }
        public bool ISValid
        {
            get { return isValid; }
        }
        public int RowCount
        {
            get { return iRowCount; }
        }

        public int UID
        {
            get { return Convert.ToInt32(drow["UID"]); }
            set { drow["UID"] = value; }
        }
        public int RSUID
        {
            get { return Convert.ToInt32(drow["RSUID"]); }
            set { drow["RSUID"] = value; }
        }
        public int HHID
        {
            get { return Convert.ToInt32(drow["HHID"]); }
            set { drow["HHID"] = value; }
        }
        public int HDBuilding
        {
            get { return Convert.ToInt32(drow["HDBuilding"]); }
            set { drow["HDBuilding"] = value; }
        }
        public int HProgram
        {
            get { return Convert.ToInt32(drow["HDProgram"]); }
            set { drow["HDProgram"] = value; }
        }
        public int HDItem
        {
            get { return Convert.ToInt32(drow["HDItem"]); }
            set { drow["HDItem"] = value; }
        }
        public string ClientComments
        {
            get { return drow["ClientComments"].ToString(); }
            set { drow["ClientComments"] = value; }
        }
        public string DriverNotes
        {
            get { return drow["DriverNotes"].ToString(); }
            set { drow["DriverNotes"] = value; }
        }
        public int Status
        {
            get { return Convert.ToInt32(drow["Status"]); }
            set { drow["Status"] = value; }
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
            get { return (DateTime)drow["Modified"]; }
            set { drow["Modified"] = value; }
        }
        public string ModifiedBy
        {
            get { return drow["ModifiedBy"].ToString(); }
            set { drow["ModifiedBy"] = value; }
        }
        #endregion Get/Set Accessors

        //-----------------------------DATA VALUE--------------------------------------------------------------------
        /// <summary>
        ///An Overloaded set of get/set funtions that will take in any kind of data value used in 
        ///the front end and accsess the data set for that data type, used mostly for a collection
        ///of textboxes so collection can be itterated through in one loop and have one function called
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

        //Gets property through use of just the column name in database
        public object GetDataValue(string FieldName)
        {
            return drow[FieldName];
        }

        //Gets property through use of just the column name in database as string
        public object GetDataString(string FieldName)
        {
            if (dtbl.Rows.Count > 0)
            {
                int fldIndex = dtbl.Columns.IndexOf(FieldName);
                if (fldIndex >= 0)
                {
                    if (dtbl.Columns[fldIndex].DataType.Name == "DateTime")
                        if (drow[FieldName].ToString() != "")
                        { return CCFBGlobal.ValidDateString(drow[FieldName]); }
                        else
                        { return ""; }
                    else
                        return drow[FieldName].ToString();
                }
            }
            return "";
        }
        #endregion


        public int Add(int rsuid, int hhid, int hditem)
        {
            drow = dtbl.NewRow();
            RSUID = rsuid;
            HHID = hhid;
            HDItem = hditem;
            //Notes = "";
            Status = 0;
            Created = DateTime.Now;
            CreatedBy = CCFBGlobal.dbUserName;
            Modified = DateTime.Now;
            ModifiedBy = CCFBGlobal.dbUserName;
            dtbl.Rows.Add(drow);
            update();
            refreshDataTable();
            int lastUID = maxRouteId();
            find(lastUID);
            return Convert.ToInt32(drow[0]);
        }
        
        //-------------------------------------Find------------------------------------
        /// <summary>
        /// Finds the TrxID in dataset and sets that row to the drow
        /// </summary>
        /// <param name="ID"></param>
        /// 
        public void find(int ID)
        {
            for (int i = 0; i < iRowCount; i++)
            {
                if (ID == dtbl.Rows[i].Field<int>("ID"))
                {
                    drow = dtbl.Rows[i];
                    break;
                }
            }
        }

        public int maxRouteId()
        {
            int tmpID = 0;
            foreach (DataRow drow0 in dtbl.Rows)
            {
                if (Convert.ToInt32(drow0[0]) > tmpID)
                {
                    tmpID = Convert.ToInt32(drow0[0]);
                }
            }
            return tmpID;
        }

        public void refreshDataTable()
        {
            try
            {
                openConnection();
                dtbl.Clear();
                iRowCount = dadAdpt.Fill(dtbl);
                closeConnection();
                if (iRowCount > 0)
                    drow = dtbl.Rows[0];
            }
            catch (SqlException ex)
            {
                iRowCount = 0;
                drow = null;
                CCFBGlobal.appendErrorToErrorReport("Select Command = " + command.CommandText,
                    ex.GetBaseException().ToString());
            }
        }

        /// <summary>
        /// Sets the DataRow for the given RowIndex
        /// </summary>
        /// <param name="rowIndex"></param>
        public DataRow setDataRow(int rowIndex)
        {
            if (dtbl.Rows.Count >= rowIndex)
            {
                drow = dtbl.Rows[rowIndex];
                return drow;
            }
            return null;
        }

        public void open(System.Int32 key)
        {
            try
            {
                openConnection();
                command = new SqlCommand("SELECT * FROM HDRSClients WHERE RSUID=" + key.ToString(), conn);
                dadAdpt.SelectCommand = command;
                dtbl.Clear();
                iRowCount = dadAdpt.Fill(dtbl);
                closeConnection();
                if (iRowCount > 0)
                    drow = dtbl.Rows[0];
            }
            catch (SqlException ex)
            {
                iRowCount = 0;
                drow = null;
                CCFBGlobal.appendErrorToErrorReport("Select Command = " + command.CommandText,
                    ex.GetBaseException().ToString());
            }
        }

        public void openWhere(string whereClause)
        {
            try
            {
                openConnection();
                command = new SqlCommand("SELECT * FROM HDRSClients " + whereClause, conn);
                dadAdpt.SelectCommand = command;
                dtbl.Clear();
                iRowCount = dadAdpt.Fill(dtbl);
                closeConnection();
                if (iRowCount > 0)
                    drow = dtbl.Rows[0];
            }
            catch (SqlException ex)
            {
                iRowCount = 0;
                drow = null;
                CCFBGlobal.appendErrorToErrorReport("Select Command = " + command.CommandText,
                    ex.GetBaseException().ToString());
            }
        }

        public bool delete(int key)
        {
            SqlCommand cmdDelete = new SqlCommand(" DELETE FROM HDRSClients WHERE Id=" + key.ToString(), conn);
            try
            {
                openConnection();
                int iRows = cmdDelete.ExecuteNonQuery();
                closeConnection();
                return (iRows == 1);
            }
            catch (SqlException ex)
            {
                CCFBGlobal.appendErrorToErrorReport("Delete Command = " + cmdDelete.CommandText,
                    ex.GetBaseException().ToString());
                return false;
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

        public void update()
        {
            try
            {
                openConnection();
                if (dadAdpt.UpdateCommand == null)
                {
                    SqlCommandBuilder commBuilder = new SqlCommandBuilder(dadAdpt);
                }
                dadAdpt.Update(dtbl);
                conn.Close();
            }
            catch (SqlException ex)
            {
                CCFBGlobal.appendErrorToErrorReport("Update HDRSClients FAILED ",
                    ex.GetBaseException().ToString());
            }

        }

        public string HDFamilySize(int hhid)
        {
            string sql = "SELECT RTRIM(case WHEN Infants = 0 THEN '' ELSE CAST(Infants as Varchar(3)) + 'I ' END"
                + " + case WHEN Youth+Teens+Eighteen = 0 THEN '' ELSE CAST(Youth+Teens+Eighteen as Varchar(3)) + 'C 'END"
                + " + case WHEN Adults = 0 THEN '' ELSE CAST(Adults as Varchar(3)) + 'A ' END"
                + " + case WHEN Seniors = 0 THEN '' ELSE CAST(Seniors as Varchar(3)) + 'S' END) FamilySize"
                + " FROM Household WHERE ServiceMethod = 2 AND ID = " + hhid.ToString();
            object retval = CCFBGlobal.getSQLValue(sql);
            if (retval == null)
                return "";
            else
                return retval.ToString();
        }
    }
}

