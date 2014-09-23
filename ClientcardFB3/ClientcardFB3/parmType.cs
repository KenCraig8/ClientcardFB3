using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ClientcardFB3
{
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
}
