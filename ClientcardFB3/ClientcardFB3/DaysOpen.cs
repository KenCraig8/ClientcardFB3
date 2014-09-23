
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

namespace ClientcardFB3
{
    public class DaysOpen
    {
        string connString;
        SqlDataAdapter dadAdpt;
        DataSet dset;
        SqlCommand command;
        System.Data.SqlClient.SqlConnection conn;
        static string tbName = "DaysOpen";
        bool bisValid = false;
        int rowCurrent = 0;

        public DaysOpen(string connStringIn)
        {
            connString = connStringIn;
            conn = new System.Data.SqlClient.SqlConnection(connString);
            dset = new DataSet();
            dadAdpt = new SqlDataAdapter();
            command = new SqlCommand("UPDATE DaysOpen SET IsCommodity=@IsCommodity, "
                    + "SpecialItems=@SpecialItems WHERE date=@date", conn);
            command.Parameters.Add("@date", SqlDbType.DateTime, 8, "date");
            command.Parameters.Add("@IsCommodity", SqlDbType.Bit, 1, "IsCommodity");
            command.Parameters.Add("@SpecialItems", SqlDbType.NVarChar, 500, "SpecialItems");
            dadAdpt.UpdateCommand = command;

            command = new SqlCommand(" DELETE FROM DaysOpen WHERE date=@date", conn);
            command.Parameters.Add("@date", SqlDbType.DateTime, 8, "date");
            dadAdpt.DeleteCommand = command;

            command = new SqlCommand("Insert Into " + tbName
                    + "(date, IsCommodity, SpecialItems) "
                    + "Values(@date, @IsCommodity, @SpecialItems)", conn);
            command.Parameters.Add("@date", SqlDbType.DateTime, 8, "date");
            command.Parameters.Add("@IsCommodity", SqlDbType.Bit, 1, "IsCommodity");
            command.Parameters.Add("@SpecialItems", SqlDbType.NVarChar, 500, "SpecialItems");
            dadAdpt.InsertCommand = command;
        }

        #region Get/Set Accessors

        public int CurrentRow
        {
            get { return rowCurrent; }
            set { rowCurrent = value; }
        }

        public bool isValid
        { 
            get { return bisValid; }
            set { bisValid = value; }
        }

        public DataRow DRow
        {
            get { return dset.Tables[tbName].Rows[rowCurrent]; }
        }

        public DataSet DSet
        {
            get { return dset; }
            set { dset = value; }
        }

        public int RowCount
        {
            get
            {
                try { return dset.Tables[tbName].Rows.Count; }
                catch (Exception ex)
                {
                    CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString());
                    return 0;
                }
            }
        }

        public DateTime date
        {
            get { return dset.Tables[tbName].Rows[rowCurrent].Field<System.DateTime>("date"); }
            set { dset.Tables[tbName].Rows[rowCurrent]["date"] = value; }
        }

        public bool IsCommodity
        {
            get { return dset.Tables[tbName].Rows[rowCurrent].Field<System.Boolean>("IsCommodity"); }
            set { dset.Tables[tbName].Rows[rowCurrent]["IsCommodity"] = value; }
        }

        public string SpecialItems
        {
            get { return dset.Tables[tbName].Rows[rowCurrent].Field<System.String>("SpecialItems"); }
            set { dset.Tables[tbName].Rows[rowCurrent]["SpecialItems"] = value; }
        }
        #endregion

        public int FindDate(DateTime TestDate)
        {
            rowCurrent = -1;
            try
            {
                if ((dset.Tables[tbName] == null ||
                    dset.Tables[tbName].Rows[0].Field<DateTime>(0) > TestDate))
                {
                    openWhere("[Date] >= '" + TestDate.ToShortDateString() + "'");
                }
                for (int i = 0; i < dset.Tables[tbName].Rows.Count; i++)
                {
                    if (dset.Tables[tbName].Rows[i].RowState != DataRowState.Deleted)
                    {
                        if (dset.Tables[tbName].Rows[i].Field<DateTime>(0) == TestDate)
                        {
                            rowCurrent = i;
                            break;
                        }
                    }
                }
            }
            catch { }
            return rowCurrent ;
        }

        public string FindServiceDateNext(String TestDate)
        {
            if (openWhere("Date >= '" + TestDate + "' ORDER BY Date"))
            {
                return dset.Tables[0].Rows[0].Field<DateTime>("Date").ToShortDateString();
            }
            return "";
        }

        public string FindServiceDatePrev(String TestDate)
        {
            String LowDate = Convert.ToDateTime(TestDate).AddDays(-45).ToShortDateString(); 
            if (openWhere("Date BETWEEN '" + LowDate + "' AND '" + TestDate + "' ORDER BY Date DESC"))
            {
                return dset.Tables[0].Rows[0].Field<DateTime>("Date").ToShortDateString();
            }
            return "";
        }
        
        public bool openWhere(String WhereClause)
        {
            try
            {
                command = new SqlCommand("SELECT * FROM " + tbName, conn);
                if (WhereClause != "" )
                {
                    command.CommandText += " WHERE " + WhereClause;
                }
                dadAdpt.SelectCommand = command;
                dset.Clear();
                dadAdpt.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                dadAdpt.Fill(dset, tbName);
                rowCurrent = 0;
                bisValid =  (dset.Tables[0].Rows.Count > 0);
                return bisValid;
            }
            catch (SqlException ex) { return bisValid = false; }
        }

        public void openTopTwentyWithinDate(DateTime toFind)
        {
            try
            {
                command = new SqlCommand("select top 15 * from " + tbName + " daysopen where date <='" + toFind + "' ORDER BY Date DESC; " +
                "select top 5 * from daysopen where date > '" + toFind + "' ORDER BY Date ASC", conn);
                dadAdpt = new SqlDataAdapter(command);
                dadAdpt.SelectCommand = command;
                dset.Clear();
                dadAdpt.Fill(dset, tbName);
            }
            catch (SqlException ex) { }
        }

        public void deleteRow(int Index)
        {
            dset.Tables[tbName].Rows[Index].Delete();
        }

        public void deleteDate(String DateValue)
        {
            DataRow drow = dset.Tables[tbName].Rows.Find( DateValue);
            if (drow.RowState != DataRowState.Deleted)
                { drow.Delete(); } 
        }

         public void AddDate(DateTime OpenDate, Boolean IsCommodity, String SpecialItemList)
        {
            DataRow mydRow = dset.Tables[tbName].Rows.Find (OpenDate);
            if (mydRow == null)
                { dset.Tables[0].Rows.Add(new object[] { OpenDate, IsCommodity, SpecialItemList }); }
            else if (mydRow.RowState == DataRowState.Deleted)
                { mydRow.SetModified(); }
        }

        public void update()
        {
            if (dset.HasChanges() == true)
            {
                try
                    { dadAdpt.Update(dset, tbName); }
                catch (SqlException ex) { }
            }
        }

        private int DeleteDateRange(String FirstDate, String LastDate)
        {
            String srcSQL = "DELETE FROM DaysOpen WHERE Date BETWEEN '" + FirstDate + "' AND '" + LastDate + "'";
            SqlCommand myCommand = new SqlCommand(srcSQL,conn);
            conn.Open();
            int NbrRows= myCommand.ExecuteNonQuery();
            conn.Close();
            return NbrRows;
        }

        public void BatchProcess(DateTime FirstDate, DateTime LastDate, int InsertMode, bool ForceIsCommodity)
        {
            int NbrRowsCleared = 0; 
            if (InsertMode == 0) //Clear All Dates
            { 
                NbrRowsCleared = DeleteDateRange(FirstDate.ToString(), LastDate.ToString());
                dset.Tables[tbName].Clear();
            }
            int[] DOWRules = new int[7];

            //For each day of week
            for (int i = 0; i < CCFBOpenDayOfWeek.DSetDOW.Tables[0].Rows.Count; i++)
                { DOWRules[i] = Convert.ToInt32(CCFBOpenDayOfWeek.DSetDOW.Tables[0].Rows[i]["FldVal"]); }

            DateTime WorkDate = FirstDate;
            int myDOW = 0;
            int WeekofMonth = 0;
            do
            {
                myDOW = CCFBOpenDayOfWeek.findRow(WorkDate.DayOfWeek.ToString());
                if (DOWRules[myDOW] > 0)
                {

                    if (FindDate(WorkDate) >= 0)
                    {
                        if (InsertMode == 1)
                        {
                            IsCommodity = ForceIsCommodity;
                            SpecialItems = "";
                        }
                    }
                    else
                    {
                        switch (DOWRules[myDOW]) 
                        {
                            case 1: //Sets day of week for every week
                                {
                                    AddDate(WorkDate, ForceIsCommodity, "");
                                    break;
                                }
                            case 2: //Sets for 1st and 3rd weeks
                                {
                                    WeekofMonth = CalculateWeekofMonth(WorkDate);
                                    if (WeekofMonth == 1 || WeekofMonth == 3)
                                        { AddDate(WorkDate, ForceIsCommodity, ""); }
                                    break;
                                }
                            case 3: //Sets for 2nd and 4th weeks
                                {
                                    WeekofMonth = CalculateWeekofMonth(WorkDate);
                                    if (WeekofMonth == 2 || WeekofMonth == 4)
                                    { AddDate(WorkDate, ForceIsCommodity, ""); }
                                    break;
                                }
                            case 4: //Sets for 1st week
                                {
                                    WeekofMonth = CalculateWeekofMonth(WorkDate);
                                    if (WeekofMonth == 1)
                                    { AddDate(WorkDate, ForceIsCommodity, ""); }
                                    break;
                                }
                            case 5: //Sets for 2nd week
                                {
                                    WeekofMonth = CalculateWeekofMonth(WorkDate);
                                    if (WeekofMonth == 2)
                                    { AddDate(WorkDate, ForceIsCommodity, ""); }
                                    break;
                                }
                            case 6: //Sets for 3rd week
                                {
                                    WeekofMonth = CalculateWeekofMonth(WorkDate);
                                    if (WeekofMonth == 3)
                                    { AddDate(WorkDate, ForceIsCommodity, ""); }
                                    break;
                                }
                            case 7: //Sets for 4th week
                                {
                                    WeekofMonth = CalculateWeekofMonth(WorkDate);
                                    if (WeekofMonth == 4)
                                    { AddDate(WorkDate, ForceIsCommodity, ""); }
                                    break;
                                }
                        }
                    }
                }
                WorkDate = WorkDate.AddDays(1);
            } while (WorkDate <= LastDate);
            update();
        }

        private int CalculateWeekofMonth(DateTime TestDate)
        {
            int WOM = 0;
            int DayNum = TestDate.Day;
            do
            {
                WOM += 1;
                DayNum -= 7;
            } while (DayNum > 0);
            return WOM;
        }
    }
}

