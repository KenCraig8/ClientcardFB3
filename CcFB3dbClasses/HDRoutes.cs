
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace ClientcardFB3
{
    /// <summary>
    /// Stores the info for a voulenteer needed for this
    /// </summary>
    public class VolContactInfo
    {
        public int id;
        public string name;
        public string phone;

        public VolContactInfo()
        {
            reset();
        }

        public void reset()
        {
            id = 0;
            name = "";
            phone = "";
        }
    }

    /// <summary>
    /// Performes opperations on the routes
    /// Used by the HDPlannerForm
    /// </summary>
    public class HDRoutes
    {
        string connString;
        SqlDataAdapter dadAdpt;
        DataTable dtbl;
        SqlCommand command;
        System.Data.SqlClient.SqlConnection conn;
        DataRow drow;
        int iRowCount = 0;
        VolContactInfo driverInfo =  new VolContactInfo();
        VolContactInfo fbContactInfo =  new VolContactInfo();
        Volunteers clsVol;

        /// <summary>
        /// Injects the dependencies
        /// Used for testing
        /// </summary>
        /// <param name="connStringIn"></param>
        public HDRoutes(string connStringIn, Volunteers clsVol)
        {
            conn = new System.Data.SqlClient.SqlConnection();
            conn.ConnectionString = connStringIn;
            dtbl = new DataTable();
            dadAdpt = new SqlDataAdapter();
            this.clsVol = new Volunteers(connStringIn);
        }

        #region Get/Set Accessors
        public DataTable DTable
        {
            get { return dtbl; }
        }
        public int RowCount
        {
            get { return iRowCount; }
        }

        public string DriverName
        {
            get { return driverInfo.name; }
        }

        public string DriverPhone
        {
            get { return driverInfo.phone.Replace("-",""); }
        }

        public string FBContactName
        {
            get { return fbContactInfo.name; }
        }

        public string FBContactPhone
        {
            get { return fbContactInfo.phone.Replace("-", ""); }
        }

        public int ID
        {
            get { return Convert.ToInt32(drow["ID"]); }
            set { drow["ID"] = value; }
        }
        public string RouteTitle
        {
            get { return drow["RouteTitle"].ToString(); }
            set { drow["RouteTitle"] = value; } // arguement exeption here. Column 'RouteTitle' does not belong to table .
        }
        public int DeliveryDOW
        {
            get { return Convert.ToInt32(drow["DeliveryDOW"]); }
            set { drow["DeliveryDOW"] = value; }
        }
        public int DeliveryCycle
        {
            get { return Convert.ToInt32(drow["DeliveryCycle"]); }
            set { drow["DeliveryCycle"] = value; }
        }
        public bool InActive
        {
            get { return (bool)drow["InActive"]; }
            set { drow["InActive"] = value; }
        }
        public int DefaultDriver
        {
            get { return Convert.ToInt32(drow["DefaultDriver"]); }
            //Crashes right here. invalid argument exception on drow
            set { drow["DefaultDriver"] = value; }
        }
        public int FBContact
        {
            get { return Convert.ToInt32(drow["FBContact"]); }
            set { drow["FBContact"] = value; }
        }
        public string Notes
        {
            get { return drow["Notes"].ToString(); }
            set { drow["Notes"] = value; }
        }
        public decimal EstHours
        {
            get { return (decimal)drow["EstHours"]; }
            set { drow["EstHours"] = value; }
        }
        public decimal EstMiles
        {
            get { return (decimal)drow["EstMiles"]; }
            set { drow["EstMiles"] = value; }
        }
        public string DriverNotes
        {
            get { return drow["DriverNotes"].ToString(); }
            set { drow["DriverNotes"] = value; }
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

        /// <summary>
        /// Adds a new row to the data table and initializes the values
        /// </summary>
        /// <returns></returns>
        public int Add()
        {
            drow = dtbl.NewRow();
            DefaultDriver = 0;
            RouteTitle = "Route " + (dtbl.Rows.Count+1).ToString();
            Notes = "";
            DriverNotes = "";
            driverInfo = new VolContactInfo();
            fbContactInfo = new VolContactInfo();
            InActive = false;
            DeliveryDOW = 1;
            DeliveryCycle = 0;
            EstHours = 1;
            EstMiles = 1;
            FBContact = 0;
            Modified = DateTime.Now;
            ModifiedBy = CCFBGlobal.dbUserName;
            dtbl.Rows.Add(drow);
            update();
            refreshDataTable();
            int lastRouteID = maxRouteId();
            find(lastRouteID, false);
            return Convert.ToInt32(drow[0]);
        }
        
        //-------------------------------------Find------------------------------------
        /// <summary>
        /// Finds the TrxID in dataset and sets that row to the drow
        /// </summary>
        /// <param name="ID"></param>
        /// 
        public void find(int ID, bool getName)
        {
            if (getName == true)
            {
                driverInfo.reset();
                fbContactInfo.reset();

            }
            for (int i = 0; i < iRowCount; i++)
            {
                if (ID == dtbl.Rows[i].Field<int>("ID"))
                {
                    drow = dtbl.Rows[i];
                    if (getName == true)
                    {
                        loadVolInfo(DefaultDriver, driverInfo);
                        loadVolInfo(FBContact, fbContactInfo);
                    }
                    break;
                }
            }
        }

        /// <summary>
        /// Loads a Voulenteer with the id
        /// </summary>
        /// <param name="volId">Id to load</param>
        /// <param name="volInfo">The info to set it to</param>
        public void loadVolInfo(int volId, VolContactInfo volInfo)
        {
            volInfo.reset();
            if ((volId > 0) && (clsVol.open(volId) == true)){
                volInfo.id = volId;
                volInfo.name = clsVol.Name;
                volInfo.phone = clsVol.Phone;
            }
        }

        /// <summary>
        /// Loads either the driver or FBcontact info
        /// </summary>
        /// <param name="volId"></param>
        /// <param name="isDriver">If true, load to the driver, if false, load to fbContact</param>
        public void loadCertainVolInfo(int volId, bool isDriver)
        {
            if(isDriver)
                loadVolInfo(volId, driverInfo);
            else
                loadVolInfo(volId, fbContactInfo);
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

        //public void open(System.Int32 key)
        //{
        //    try
        //    {
        //        openConnection();
        //        command = new SqlCommand("SELECT * FROM HDRoutes WHERE ID=" + key.ToString(), conn);
        //        dadAdpt.SelectCommand = command;
        //        dtbl.Clear();
        //        iRowCount = dadAdpt.Fill(dtbl);
        //        closeConnection();
        //        if (iRowCount > 0)
        //            drow = dtbl.Rows[0];
        //    }
        //    catch (SqlException ex)
        //    {
        //        iRowCount = 0;
        //        drow = null;
        //        CCFBGlobal.appendErrorToErrorReport("Select Command = " + command.CommandText,
        //            ex.GetBaseException().ToString());
        //    }
        //}

        public void openWhere(string whereClause)
        {
            try
            {
                openConnection(); //Sql exception here
                command = new SqlCommand("SELECT * FROM HDRoutes " + whereClause, conn);
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
                string comText = command != null ? command.CommandText : "openConnection problem";
                CCFBGlobal.appendErrorToErrorReport("Select Command = " + comText,
                    ex.GetBaseException().ToString());
            }
        }

        public bool delete(int key)
        {
            SqlCommand cmdDelete = new SqlCommand(" DELETE FROM HDRoutes WHERE Id=" + key.ToString(), conn);
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
                CCFBGlobal.appendErrorToErrorReport("Update HDRoutes FAILED ",
                    ex.GetBaseException().ToString());
            }

        }
    }
}

