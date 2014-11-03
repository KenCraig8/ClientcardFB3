using System;
using System.Data;
using System.Data.SqlClient;

namespace ClientcardFB3
{
    public class Volunteers
    {
        string connString;
        SqlDataAdapter dadAdpt;
        DataSet dset;
        SqlCommand command;
        SqlConnection conn;
        static string tbName = "Volunteers";
        bool isValid = false;
        int iRowCount = 0;
        DataRow drow = null;
        DataSet volGrpsDset;
        SqlDataAdapter volGrpsDadAdapt;
        SqlCommand volGrpComm;
        int[] groupsForVolunteer;

        public Volunteers(string connStringIn)
        {
            conn = new SqlConnection();
            connString = connStringIn;
            conn.ConnectionString = connString;
            dset = new DataSet();
            dadAdpt = new SqlDataAdapter();
            volGrpsDadAdapt = new SqlDataAdapter();
            volGrpsDset = new DataSet();
        }

        #region Get/Set Accessors

        public int[] Groups
        {
            get
            {
                return groupsForVolunteer;
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
            get { return drow.Field<System.Int32>("ID"); }
            set { drow["ID"] = value; }
        }

        public bool InActive
        {
            get { return drow.Field<System.Boolean>("InActive"); }
            set { drow["InActive"] = value; }
        }

        public string Name
        {
            get { return drow["Name"].ToString(); }
            set { drow["Name"] = value; }
        }

        public string Address
        {
            get { return drow["Address"].ToString(); }
            set { drow["Address"] = value; }
        }

        public string City
        {
            get { return drow["City"].ToString(); }
            set { drow["City"] = value; }
        }

        public string State
        {
            get { return drow["State"].ToString(); }
            set { drow["State"] = value; }
        }

        public string ZipCode
        {
            get { return drow["ZipCode"].ToString(); }
            set { drow["ZipCode"] = value; }
        }

        public string Phone
        {
            get { return drow["Phone"].ToString(); }
            set { drow["Phone"] = value; }
        }

        public string CellPhone
        {
            get { return drow["CellPhone"].ToString(); }
            set { drow["CellPhone"] = value; }
        }

        public string WorkPhone
        {
            get { return drow["WorkPhone"].ToString(); }
            set { drow["WorkPhone"] = value; }
        }

        public string Company
        {
            get { return drow["Company"].ToString(); }
            set { drow["Company"] = value; }
        }

        public string ContactName
        {
            get { return drow["ContactName"].ToString(); }
            set { drow["ContactName"] = value; }
        }

        public string ContactPhone
        {
            get { return drow["ContactPhone"].ToString(); }
            set { drow["ContactPhone"] = value; }
        }

        public int RcdType
        {
            get { return drow.Field<System.Int32>("RcdType"); }
            set { drow["RcdType"] = value; }
        }

        public string Notes
        {
            get { return drow["Notes"].ToString(); }
            set { drow["Notes"] = value; }
        }

        public string Sex
        {
            get { return drow["Sex"].ToString(); }
            set { drow["Sex"] = value; }
        }

        public bool AutoAlert
        {
            get { return drow.Field<System.Boolean>("AutoAlert"); }
            set { drow["AutoAlert"] = value; }
        }

        public bool UserFlag0
        {
            get { return drow.Field<System.Boolean>("UserFlag0"); }
            set { drow["UserFlag0"] = value; }
        }

        public bool UserFlag1
        {
            get { return drow.Field<System.Boolean>("UserFlag1"); }
            set { drow["UserFlag1"] = value; }
        }

        public string Vehicle
        {
            get { return drow["Info1"].ToString(); }
            set { drow["Info1"] = value; }
        }

        public string BackgroundCheck
        {
            get { return drow["Info2"].ToString(); }
            set { drow["Info2"] = value; }
        }

        public DateTime Date1
        {
            get { return drow.Field<System.DateTime>("Date1"); }
            set { drow["Date1"] = value; }
        }

        public DateTime Date2
        {
            get { return drow.Field<System.DateTime>("Date2"); }
            set { drow["Date2"] = value; }
        }

        public DateTime Created
        {
            get { return drow.Field<System.DateTime>("Created"); }
            set { drow["Created"] = value; }
        }

        public string CreatedBy
        {
            get { return drow["CreatedBy"].ToString(); }
            set { drow["CreatedBy"] = value; }
        }

        public DateTime Modified
        {
            get { return drow.Field<System.DateTime>("Modified"); }
            set { drow["Modified"] = value; }
        }

        public string ModifiedBy
        {
            get { return drow["ModifiedBy"].ToString(); }
            set { drow["ModifiedBy"] = value; }
        }

        public bool NotOnHoursList
        {
            get { return drow.Field<System.Boolean>("NotOnHoursList"); }
            set { drow["NotOnHoursList"] = value; }
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
        //Sets data value when value is a int
        public void SetDataValue(string FieldName, int FieldValue)
        {
            drow[FieldName] = FieldValue;
        }
        //Sets data value when value is a bool
        public void SetDataValue(string FieldName, bool FieldValue)
        {
            drow[FieldName] = FieldValue;
        }
        //Sets data value when value is a DateTime
        public void SetDataValue(string FieldName, DateTime FieldValue)
        {
            drow[FieldName] = FieldValue;
        }
        //Sets data value when value is a float
        public void SetDataValue(string FieldName, float FieldValue)
        {
            drow[FieldName] = FieldValue;
        }
        //Gets property through use of just the collum name in database
        public object GetDataValue(string FieldName)
        {
            try
            {
                return drow[FieldName];
            }
            catch(IndexOutOfRangeException ex) 
            {
                CCFBGlobal.appendErrorToErrorReport("FieldName="+FieldName, ex.GetBaseException().ToString());
                return "";
            }         
        }
        #endregion

        /// <summary>
        /// Gets the ditinct values for any collumn
        /// </summary>
        /// <param name="columnName">Collumn name</param>
        public void getDistincts(string columnName, string whereClause)
        {
            try
            {
                openConnection();
                command = new SqlCommand("SELECT " + columnName + ", COUNT(*) FROM "
                    + tbName + whereClause
                    + " Group By " + columnName + " Order By " + columnName, conn);
                dadAdpt.SelectCommand = command;
                dset.Clear();
                dset.Tables.Clear();
                iRowCount = dadAdpt.Fill(dset, tbName);
                closeConnection();
                isValid = false;
                if (iRowCount > 0)
                    drow = dset.Tables[tbName].Rows[0];
            }
            catch (SqlException ex)
            {
                closeConnection();
                CCFBGlobal.appendErrorToErrorReport("columnName=" + columnName, ex.GetBaseException().ToString(),
                    CCFBGlobal.serverName);
                iRowCount = 0;
            }
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
                    drow = dset.Tables[tbName].Rows[0];
                return isValid = RowCount > 0;
            }
            catch (SqlException ex) 
            {
                CCFBGlobal.appendErrorToErrorReport("ID="+ ID.ToString(), ex.GetBaseException().ToString(),
                   CCFBGlobal.serverName);
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
                command = new SqlCommand("SELECT * FROM " + tbName + " " + whereClause
                    + " Order By [Name]", conn);
                dadAdpt.SelectCommand = command;
                dset.Clear();
                iRowCount = dadAdpt.Fill(dset, tbName);
                closeConnection();
                if (iRowCount > 0)
                    drow = dset.Tables[tbName].Rows[0];
            }
            catch (SqlException ex) 
            {
                CCFBGlobal.appendErrorToErrorReport("whereClause="+whereClause, ex.GetBaseException().ToString(),
                   CCFBGlobal.serverName);
                closeConnection();
                iRowCount = 0;
            }
        }

        public void openWithGroupIdWhere(string whereClause, string grouplist)
        {
            try
            {
                if (grouplist=="")
                    command = new SqlCommand("SELECT * FROM " + tbName + whereClause + " Order By [Name]", conn);
                else
                command = new SqlCommand("SELECT * FROM " + tbName + " v " + whereClause 
                    + " AND EXISTS(SELECT * FROM VolunteerGroups vg WHERE GroupID IN (" + grouplist + ") AND vg.Volid = v.Id)"
                    + " Order By [Name]", conn);

                dadAdpt.SelectCommand = command;
                dset.Clear();
                iRowCount = dadAdpt.Fill(dset, tbName);
                if (iRowCount > 0)
                    drow = dset.Tables[tbName].Rows[0];
            }
            catch (SqlException ex)
            {
                CCFBGlobal.appendErrorToErrorReport("whereClause=" + whereClause, ex.GetBaseException().ToString(),
                   CCFBGlobal.serverName);
                closeConnection();
                iRowCount = 0;
            }
        }

        public void openAll()
        {
            try
            {
                openConnection();
                command = new SqlCommand("SELECT * FROM " + tbName
                    + " Order By [Name]", conn);
                dadAdpt.SelectCommand = command;
                dset.Clear();
                iRowCount = dadAdpt.Fill(dset, tbName);
                closeConnection();
                if (iRowCount > 0)
                    drow = dset.Tables[tbName].Rows[0];
            }
            catch (SqlException ex) 
            {
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString(),
                   CCFBGlobal.serverName);
                closeConnection();
                iRowCount = 0;
            }
        }

        public void getVolGrpsForVol(int volID)
        {
            try
            {
                int rows;
                openConnection();
                volGrpComm = new SqlCommand("Select * From VolunteerGroups Where VolId = " + volID.ToString(), conn);
                volGrpsDadAdapt.SelectCommand = volGrpComm;
                volGrpsDset.Clear();
                rows = volGrpsDadAdapt.Fill(volGrpsDset);
                closeConnection();
                groupsForVolunteer = new int[rows];
                for (int i = 0; i < rows; i++)
                {
                    groupsForVolunteer[i] = volGrpsDset.Tables[0].Rows[i].Field<int>("GroupID");
                }
            }
            catch (SqlException ex)
            {
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString(),
                   CCFBGlobal.serverName);
                closeConnection();
            }
        }

        public void delete(System.Int32 key)
        {
            SqlCommand commDelete = new SqlCommand(" DELETE FROM Volunteers WHERE ID=" + ID.ToString(), conn);
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

                    if (dadAdpt.SelectCommand == null)
                    {
                        SqlCommandBuilder commBuilder = new SqlCommandBuilder(dadAdpt);
                    }

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

