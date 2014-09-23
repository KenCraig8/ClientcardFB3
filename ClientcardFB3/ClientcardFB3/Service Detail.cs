
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

namespace ClientCardFB3
{
    public class ServiceDetail
    {
        string connString;
        SqlDataAdapter dadAdpt;
        DataSet dset;
        SqlCommand command;
        System.Data.SqlClient.SqlConnection conn;
        static string tbName = "Service Detail";
        bool bisValid = false;

        public ServiceDetail(string connStringIn)
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
            get
            {
                return bisValid;
            }
            set
            {
                bisValid = value;
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
                try
                {
                    return dset.Tables[0].Rows.Count;
                }
                catch (IndexOutOfRangeException ex)
                {
                    GlobalVariables.appendErrorToErrorReport("", ex.GetBaseException().ToString());
                    return 0;
                }
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

        public int DailyLogID
        {
            get
            {
                return dset.Tables[tbName].Rows[0].Field<System.Int32>("DailyLogID");
            }
            set
            {
                dset.Tables[tbName].Rows[0]["DailyLogID"] = value;
            }
        }

        public int ItemID
        {
            get
            {
                return dset.Tables[tbName].Rows[0].Field<System.Int32>("ItemID");
            }
            set
            {
                dset.Tables[tbName].Rows[0]["ItemID"] = value;
            }
        }

        public int Amount
        {
            get
            {
                return dset.Tables[tbName].Rows[0].Field<System.Int32>("Amount");
            }
            set
            {
                dset.Tables[tbName].Rows[0]["Amount"] = value;
            }
        }

        public string Comments
        {
            get
            {
                return dset.Tables[tbName].Rows[0].Field<System.String>("Comments");
            }
            set
            {
                dset.Tables[tbName].Rows[0]["Comments"] = value;
            }
        }

        public string CreatedBy
        {
            get
            {
                return dset.Tables[tbName].Rows[0].Field<System.String>("CreatedBy");
            }
            set
            {
                dset.Tables[tbName].Rows[0]["CreatedBy"] = value;
            }
        }

        public DateTime Created
        {
            get
            {
                return dset.Tables[tbName].Rows[0].Field<System.DateTime>("Created");
            }
            set
            {
                dset.Tables[tbName].Rows[0]["Created"] = value;
            }
        }

        public DateTime Modified
        {
            get
            {
                return dset.Tables[tbName].Rows[0].Field<System.DateTime>("Modified");
            }
            set
            {
                dset.Tables[tbName].Rows[0]["Modified"] = value;
            }
        }

        public string ModifiedBy
        {
            get
            {
                return dset.Tables[tbName].Rows[0].Field<System.String>("ModifiedBy");
            }
            set
            {
                dset.Tables[tbName].Rows[0]["ModifiedBy"] = value;
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
            return dset.Tables[tbName].Rows[0][FieldName];
        }

        #endregion

        public bool open(int UID)
        {
            try
            {
                openConnection();
                command = new SqlCommand("SELECT * FROM " + tbName + " WHERE UID=" + UID.ToString(), conn);
                dadAdpt.SelectCommand = command;
                dset.Clear();
                dadAdpt.Fill(dset, tbName);
                closeConnection();

                return bisValid = RowCount > 0;
            }
            catch (SqlException ex) 
            {
                GlobalVariables.appendErrorToErrorReport("ID=" + UID.ToString(), ex.GetBaseException().ToString(),
                    GlobalVariables.serverName);
                closeConnection(); 
                return bisValid = false; 
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
                dadAdpt.Fill(dset, tbName);
                closeConnection();
                bisValid = false;
            }
            catch (SqlException ex) 
            {
                GlobalVariables.appendErrorToErrorReport("", ex.GetBaseException().ToString(),
                    GlobalVariables.serverName);
                closeConnection(); 
                bisValid = false; 
            }
        }

        public void delete(System.Int32 key)
        {
            SqlCommand commDelete = new SqlCommand(" DELETE FROM Service Detail WHERE UID=" + key.ToString());
            command = new SqlCommand("SELECT * FROM Service Detail WHERE UID=" + key.ToString(), conn);
            dadAdpt = new SqlDataAdapter(command);
            dadAdpt.DeleteCommand = commDelete;
            dadAdpt.SelectCommand = command;
            dadAdpt.SelectCommand.Connection = conn;
            dadAdpt.DeleteCommand = commDelete;
            dadAdpt.DeleteCommand.Connection = conn;
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
                   
                    dset.Tables[tbName].Rows[0]["ModifiedBy"] = GlobalVariables.currentUser_Name;
                    dset.Tables[tbName].Rows[0]["Modified"] = DateTime.Now;

                    if (dadAdpt.UpdateCommand == null)
                    {
                        SqlCommandBuilder commBuilder = new SqlCommandBuilder(dadAdpt);
                    }
                    
                    dadAdpt.Update(dset, "Service Detail");
                    closeConnection();
                }
                catch (SqlException ex) 
                {
                    closeConnection();
                    GlobalVariables.appendErrorToErrorReport("", ex.GetBaseException().ToString(),
                   GlobalVariables.serverName);
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

