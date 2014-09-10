//*************************************************************************************************
//
// This class provides some basic operations for the edit forms that use TypeCodes. Use this class
// to open the TypeCode database table, create a list for the combo box, return the index of a
// TypeCode string in the list, and convert the TypeCode value to a TypeCode string. This very
// small class is used only for reading and displaying the TypeCodes.
//
//=================================================================================================
//
// Date			Version		Author				Description
// ----------	-------		------------------	--------------------------------------------------
// 2010-11-09	00.01.00	T. Cataldo			Moved code from Donor edit form to here.
//
//*************************************************************************************************

using System;												// For Exception, etc.
using System.Collections.Generic;							// For List<>, etc.
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace FoodReceipt
{
    public class parmTypeCodes
    {
        #region ----------Variables and Constant----------
        private System.Collections.ArrayList rcdlist;
        private string parmtablename;

        #endregion
        public parmTypeCodes(string parmTableName, string connectString, string sWhereClause)
        {
            parmtablename = parmTableName;
            rcdlist = new System.Collections.ArrayList();
            SqlConnection conn = new SqlConnection(connectString);
            DataTable dset = new DataTable();
            SqlCommand command = new SqlCommand("SELECT * FROM " + parmtablename + " " + sWhereClause + " ORDER BY SortOrder ASC", conn);
            SqlDataAdapter dadAdpt = new SqlDataAdapter(command);
            try
            {
                if (dadAdpt.Fill(dset) > 0)
                {
                    foreach (DataRow drow in dset.Rows)
                    {
                        rcdlist.Add(new parmType(
                                    Convert.ToInt32(drow["ID"])
                                  , drow["Type"].ToString()
                                  , Convert.ToInt32(drow["SortOrder"])
                                  , drow["ShortName"].ToString())
                                  );
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loading typecode table '" + parmtablename + "' encountered an error\r\n"
                    + ex.Message.ToString()
                    , "parmTypesCodes Error"
                    , MessageBoxButtons.OK
                    , MessageBoxIcon.Error);
            }
        }
        #region Get/Set Accessors
        public string ParmTable
        {
            get { return parmtablename; }
            set { parmtablename = value; }
        }

        /// <summary>
        /// Return a parmType array used to populate a ComboBox control.
        /// </summary>
        public System.Collections.ArrayList TypeCodesArray
        {
            get { return rcdlist; }
            set { rcdlist = value; }
        }

        public int GetId(string typeName)
        {
            foreach (parmType item in rcdlist)
            {
                if (item.TypeName == typeName)
                    return item.ID;
            }
            return (-1);
        }		// end of GetId

        public string GetLongName(int typeCodeId)
        {
            foreach (parmType item in rcdlist)
            {
                if (item.ID == typeCodeId)
                    return item.TypeName;
            }
            return (CCFBGlobal.INVALID_TYPE_CODE);
        }		// end of LongName

        public string GetShortName(int typeCodeId)
        {
            foreach (parmType item in rcdlist)
            {
                if (item.ID == typeCodeId)
                    return item.ShortName;
            }
            return (CCFBGlobal.INVALID_TYPE_CODE);
        }		// end of LongName

        #endregion
    }		// end of class
    public class parmType
    {
        private int id;
        private string typename;
        private int sortorder;
        private string shortname;

        public parmType(int uid, string typeName, int sortOrder, string shortName)
        {
            id = uid;
            typename = typeName;
            sortorder = sortOrder;
            shortname = shortName;
        }

        public parmType(DataRow drow)
        {
            id = Convert.ToInt32(drow[0].ToString());
            typename = drow.Field<string>(1);
            sortorder = Convert.ToInt32(drow[2].ToString());
            shortname = drow.Field<string>(3);
        }

        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        public string TypeName
        {
            get { return typename; }
            set { typename = value; }
        }
        public int SortOrder
        {
            get { return sortorder; }
            set { sortorder = value; }
        }
        public string ShortName
        {
            get { return shortname; }
            set { shortname = value; }
        }
        public string LongName
        {
            get { return typename; }
        }
        public string UID
        {
            get { return id.ToString(); }
        }
    }
}		// end of namespace