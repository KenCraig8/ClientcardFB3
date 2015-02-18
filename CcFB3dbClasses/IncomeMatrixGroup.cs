using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ClientcardFB3
{
    class IncomeGroupMatrix
    {
        int grpId = 0;
        string description = "";
        string shortName = "";
        string notes = "";
        DateTime dateAsOf;
        int processID = 0;
        DateTime dateCreated;
        string createdBy = "";
        DateTime dateModified;
        string modifiedBy = "";
        List<IncomeMatrixItem> incomeCategories = new List<IncomeMatrixItem>(4);

        public IncomeGroupMatrix(int groupId)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CCFBGlobal.connectionString;
            SqlCommand objCmd = new SqlCommand("SELECT [Id],[Description],[ShortName],[Notes],[AsOfDate],[ProcessID],[Created],[CreatedBy],[Modified],[ModifiedBy] FROM IncomeGroups WHERE Id = " + groupId, conn);
            conn.Open();
            SqlDataReader myDataReader = objCmd.ExecuteReader();
            while (myDataReader.Read())
            {
                grpId = myDataReader.GetInt32(0);
                description = myDataReader.GetSqlString(1).Value;
                shortName = myDataReader.GetSqlString(2).Value;
                notes = myDataReader.GetSqlString(3).Value;
                dateAsOf = myDataReader.GetSqlDateTime(4).Value;
                processID = myDataReader.GetInt32(5);
                dateCreated = myDataReader.GetSqlDateTime(6).Value;
                createdBy = myDataReader.GetSqlString(7).Value;
                dateModified = myDataReader.GetSqlDateTime(8).Value;
                modifiedBy = myDataReader.GetSqlString(9).Value;
            }
            myDataReader.Dispose();
            objCmd.CommandText = "SELECT * FROM IncomeMatrix WHERE IncomeGroup = " + groupId;
            DataSet dset = new DataSet();
            SqlDataAdapter dataAdpt = new SqlDataAdapter(objCmd);
            if (dataAdpt.Fill(dset) > 0)
            {
                foreach (DataRow drow in dset.Tables[0].Rows)
                {
                    incomeCategories.Add(new IncomeMatrixItem(drow));
                }
            }
            dataAdpt.Dispose();
            dset.Dispose();
            objCmd.Dispose();
            conn.Dispose();
        }
        #region Get/Set Accessors
        public int ID
        {
            get { return grpId; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public string ShortName
        {
            get { return shortName; }
            set { shortName = value; }
        }
        public string Notes
        {
            get { return notes; }
            set { notes = value; }
        }
        public DateTime AsOfDate
        {
            get { return dateAsOf; }
            set { dateAsOf = value; }
        }
        public int ProcessID
        {
            get { return processID; }
            set { processID = value; }
        }
        public DateTime Created
        {
            get { return dateCreated; }
            set { dateCreated = value; }
        }
        public string CreatedBy
        {
            get { return createdBy; }
            set { createdBy = value; }
        }
        public DateTime Modified
        {
            get { return dateModified; }
            set { dateModified = value; }
        }
        public string ModifiedBy
        {
            get { return modifiedBy; }
            set { modifiedBy = value; }
        }
        #endregion

        public string GetIncomeCategory(int annualIncome, int FamilySize)
        {
            foreach (IncomeMatrixItem item in incomeCategories)
            {
                if (annualIncome >= item.IncomeLow(FamilySize) && annualIncome <= item.IncomeHi(FamilySize))
                    return item.CatLabel2; 
            }
            return "...";
        }
    }
}
