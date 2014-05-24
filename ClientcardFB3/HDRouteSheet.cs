
using System;
using System.Data;
using System.Data.SqlClient;

namespace ClientcardFB3
{
    public class HDRouteSheet
    {
        string connString;
        SqlDataAdapter dadAdpt;
        DataTable dtbl;
        SqlCommand command;
        System.Data.SqlClient.SqlConnection conn;
        DataRow drow;
        int iRowCount = 0;
        string driverName = "";
        string driverPhone = "";
        HDRSClients clsHDRSClients = new HDRSClients(CCFBGlobal.connectionString);
        bool rsPlusRoute = false;
        
        public enum HDRSStatus
        {
            NotCreated = 0,
            Prepared = 1,
            Printed = 2,
            Posted = 3,
        }

        public HDRouteSheet(string connStringIn)
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
        
        public HDRSClients RSClients
        {
            get {return clsHDRSClients;}
            set { clsHDRSClients = value; }
        }

        public int RowCount
        {
            get { return iRowCount; }
        }

        public string DriverName
        {
            get { return driverName; }
        }

        public string DriverPhone
        {
            get { return driverPhone; }
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
        public int HDRoute
        {
            get { return Convert.ToInt32(drow["HDRoute"]); }
        }
        public int RouteStatus
        {
            get { return Convert.ToInt32(drow["RouteStatus"]); }
            set { drow["RouteStatus"] = value; }
        }
        public string Notes
        {
            get { return drow["Notes"].ToString(); }
            set { drow["Notes"] = value; }
        }
        public int VolId
        {
            get { return Convert.ToInt32(drow["VolId"]); }
            set { drow["VolId"] = value; }
        }
        public decimal NbrHours
        {
            get { return (decimal)drow["NbrHours"]; }
            set { drow["NbrHours"] = value; }
        }
        public string DriverNotes
        {
            get { return drow["DriverNotes"].ToString(); }
            set { drow["DriverNotes"] = value; }
        }
        public decimal ActMiles
        {
            get { return (decimal)drow["ActMiles"]; }
            set { drow["ActMiles"] = value; }
        }
        public string FBContact
        {
            get { return drow["FBContact"].ToString(); }
            set { drow["FBContact"] = value; }
        }
        public string FBContactPhone
        {
            get { return drow["FBContactPhone"].ToString(); }
            set { drow["FBContactPhone"] = value; }
        }
        public string BagDescr
        {
            get { return drow["BagDescr"].ToString(); }
            set { drow["BagDescr"] = value; }
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
        public string RouteTitle
        {
            get { return drow["RouteTitle"].ToString(); }
            set { drow["RouteTitle"] = value; }
        }
        public int NbrClients
        {
            get { return Convert.ToInt32(drow["NbrClients"]); }
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


        public int Add(int HDRouteID, DateTime trxDate)
        {
            int newRouteID = -1;
            SqlCommand cmdInsert = new SqlCommand();
            cmdInsert.CommandType = CommandType.StoredProcedure;
            cmdInsert.CommandText = "InsertHDRouteSheet";
            SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@RouteID";
                parameter.SqlDbType = SqlDbType.Int;
                parameter.Direction = ParameterDirection.Input;
                parameter.Value = HDRouteID;
            cmdInsert.Parameters.Add(parameter);
                parameter = new SqlParameter();
                parameter.ParameterName = "@TrxDate";
                parameter.SqlDbType = SqlDbType.DateTime;
                parameter.Direction = ParameterDirection.Input;
                parameter.Value = trxDate;
            cmdInsert.Parameters.Add(parameter);
                parameter = new SqlParameter();
                parameter.ParameterName = "@UserName";
                parameter.SqlDbType = SqlDbType.NVarChar;
                parameter.Size = 50;
                parameter.Direction = ParameterDirection.Input;
                parameter.Value = CCFBGlobal.dbUserName;
            cmdInsert.Parameters.Add(parameter);
            try
            {
                cmdInsert.Connection = conn;
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                cmdInsert.ExecuteNonQuery();
                newRouteID = Convert.ToInt32(cmdInsert.Parameters[0].Value);

                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            catch (SqlException ex)
            {
                CCFBGlobal.appendErrorToErrorReport("HDRouteID= " + HDRoute.ToString() + "\r\nTrxDate =" + trxDate.ToShortDateString(),
                    ex.GetBaseException().ToString());
            }
            return newRouteID;
        }
        
        //-------------------------------------Find------------------------------------
        /// <summary>
        /// Finds the TrxID in dataset and sets that row to the drow
        /// </summary>
        /// <param name="ID"></param>
        /// 
        public void find(int ID, bool getName)
        {
            driverName = "";
            driverPhone = "";
            for (int i = 0; i < iRowCount; i++)
            {
                if (ID == dtbl.Rows[i].Field<int>("ID"))
                {
                    drow = dtbl.Rows[i];
                    if (getName == true)
                    {
                        loadDriverInfo(VolId);
                    }
                    clsHDRSClients.open(ID);
                    break;
                }
            }
        }

        public void loadDriverInfo(int volId)
        {
            driverName = "";
            driverPhone = "";
            if (volId > 0)
            {
                Volunteers clsVol = new Volunteers(connString);
                if (clsVol.open(volId) == true)
                {
                    driverName = clsVol.Name;
                    driverPhone = clsVol.Phone;
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
                {
                    drow = dtbl.Rows[0];
                    clsHDRSClients.open(ID);
                }
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
                clsHDRSClients.open(ID);
                return drow;
            }
            return null;
        }

        public void open(System.Int32 key)
        {
            try
            {
                rsPlusRoute = false;
                openConnection();
                command = new SqlCommand("SELECT [ID]"
                                       + ",[TrxDate]"
                                       + ",[HDRoute]"
                                       + ",[RouteStatus]"
                                       + ",[Notes]"
                                       + ",[VolId]"
                                       + ",[NbrHours]"
                                       + ",[DriverNotes]"
                                       + ",[ActMiles]"
                                       + ",[FBContact]"
                                       + ",[FBContactPhone]"
                                       + ",[BagDescr]"
                                       + ",[Created]"
                                       + ",[CreatedBy]"
                                       + ",[Modified]"
                                       + ",[ModifiedBy]"
                        + " FROM HDRouteSheet WHERE ID=" + key.ToString(), conn);
                dadAdpt.SelectCommand = command;
                dtbl.Clear();
                iRowCount = dadAdpt.Fill(dtbl);
                closeConnection();
                if (iRowCount > 0)
                {
                    drow = dtbl.Rows[0];
                    clsHDRSClients.open(ID);
                }
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
                command = new SqlCommand("select CASE WHEN s.[ID] IS NULL THEN 0 ELSE s.[ID] END ID"
                                       + ",s.[TrxDate]"
                                       + ", case WHEN s.[HDRoute] IS NULL THEN r.ID ELSE s.HDRoute END HDRoute"
                                       + ", CASE WHEN s.[RouteStatus] IS NULL THEN 0 ELSE s.RouteStatus END RouteStatus"
                                       + ", CASE WHEN s.[Notes] IS NULL THEN r.Notes ELSE s.Notes END Notes"
                                       + ", CASE WHEN s.[VolId] IS NULL THEN r.DefaultDriver ELSE s.VolId END VolID"
                                       + ", CASE WHEN s.[NbrHours] IS NULL THEN r.EstHours ELSE s.NbrHours END NbrHours"
                                       + ", CASE WHEN s.[DriverNotes] IS NULL THEN r.DriverNotes ELSE s.[DriverNotes] END DriverNotes"
                                       + ", CASE WHEN s.[ActMiles] IS NULL THEN r.EstMiles ELSE s.[ActMiles] END ActMiles"
                                       + ",s.FBContact"
                                       + ",s.FBContactPhone"
                                       + ",s.[BagDescr]"
                                       + ",s.[Created]"
                                       + ",s.[CreatedBy]"
                                       + ",s.[Modified]"
                                       + ",r.ID RouteID"
                                       + ",r.[RouteTitle]"
                                       + ",r.[DeliveryDOW]"
                                       + ",r.[DeliveryCycle]"
                                       + ",r.[InActive]"
                                       + ",(SELECT COUNT(*) FROM Household WHERE HDRoute = r.ID) NbrClients"
                                       + " FROM [HDRoutes] r"
                                       + " LEFT outer join [HDRouteSheet] s ON r.ID = s.HDRoute AND s.TrxDate = '" + whereClause + "'"
                                       + " WHERE r.id > 0 and (SELECT COUNT(*) FROM Household WHERE HDRoute = r.ID) > 0 and r.Inactive = 0"
                                       + " ORDER by r.RouteTitle"
                        , conn);
                dadAdpt.SelectCommand = command;
                dtbl.Clear();
                iRowCount = dadAdpt.Fill(dtbl);
                closeConnection();
                if (iRowCount > 0)
                {
                    rsPlusRoute = true;
                    drow = dtbl.Rows[0];
                    clsHDRSClients.open(ID);
                }
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
            SqlCommand cmdDelete = new SqlCommand(" DELETE FROM HDRouteSheet WHERE Id=" + key.ToString(), conn);
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
                CCFBGlobal.appendErrorToErrorReport("Update HDRouteSheet FAILED ",
                    ex.GetBaseException().ToString());
            }
        }
        public void updateRouteStatus(HDRSStatus newStatus)
        {
            try
            {
                RouteStatus = Convert.ToInt32(newStatus);
                openConnection();
                SqlCommand cmd = new SqlCommand("UPDATE HDRouteSheet SET RouteStatus = " 
                                    + Convert.ToInt32(newStatus).ToString() + " WHERE ID = " + ID.ToString(), conn);
                cmd.ExecuteNonQuery();
                closeConnection();
            }
            catch (SqlException ex)
            {
                CCFBGlobal.appendErrorToErrorReport("Update HDRouteStatus FAILED for " + ID + " newStatus = " + newStatus.ToString(),
                    ex.GetBaseException().ToString());
            }
        }

        public string rptPath()
        {
            string[] stmp = TrxDate.ToShortDateString().Split('/');
            return @"RouteSheet\HDRS_" + stmp[2] + "-" + stmp[0] + "-" + stmp[1];
        }

        public string rptFileName()
        {
            return @"\HDRS-" + RouteTitle + ".doc";
        }
    }
}

