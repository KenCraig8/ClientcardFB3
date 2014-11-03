using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace ClientcardFB3
{
    class IncomeMatrixItem
    {
        int id;
        int grpid;
        string catLabel1;
        string catLabel2;
        string catLabel3;
        DateTime created;
        string createdby;
        DateTime modified;
        string modifiedby;

        struct MatrixItem
        {
            public int IncomeLow;
            public int IncomeHi;
        }
        List<MatrixItem> incomeRange = new List<MatrixItem>(10);

        public IncomeMatrixItem(DataRow newdrow)
        {
            id = Convert.ToInt32(newdrow["ID"]);
            grpid = Convert.ToInt32(newdrow["IncomeGroup"]);
            catLabel1 = newdrow["Label1"].ToString();
            catLabel2 = newdrow["Label2"].ToString();
            catLabel3 = newdrow["Label3"].ToString();
            created = Convert.ToDateTime(newdrow["Created"]);
            createdby = newdrow["CreatedBy"].ToString();
            modified = Convert.ToDateTime(newdrow["Modified"]);
            modifiedby = newdrow["ModifiedBy"].ToString();
            for (int i = 1; i < 11; i++)
            {
                MatrixItem mItm = new MatrixItem();
                mItm.IncomeLow = Convert.ToInt32(newdrow["IncomeLow" + i.ToString()]);
                mItm.IncomeHi = Convert.ToInt32(newdrow["IncomeHi" + i.ToString()]);
                incomeRange.Add(mItm);
            }
        }

        #region Get/Set Accessors
        public int ID
        {
            get { return id; }
        }
        public int GroupId
        {
            get { return grpid; }
        }
        public string CatLabel1
        {
            get { return catLabel1; }
        }
        public string CatLabel2
        {
            get { return catLabel2; }
        }
        public string CatLabel3
        {
            get { return catLabel3; }
        }
        public int IncomeLow(int familySize)
        {
            if (familySize < 1 )
                return incomeRange[0].IncomeLow;
            if (familySize > 10 )
                return incomeRange[9].IncomeLow;
            return incomeRange[familySize -1].IncomeLow;
        }
        public int IncomeHi(int familySize)
        {
            if (familySize < 1)
                return incomeRange[0].IncomeHi;
            if (familySize > 10)
                return incomeRange[9].IncomeHi;
            return incomeRange[familySize - 1].IncomeHi;
        }
        public DateTime Created
        {
            get { return created; }
        }
        public string CreatedBy
        {
            get { return createdby; }
        }
        public DateTime Modified
        {
            get { return modified; }
        }
        public string ModifiedBy
        {
            get { return modifiedby; }
        }
        #endregion Get/Set Accessors
    }
}
