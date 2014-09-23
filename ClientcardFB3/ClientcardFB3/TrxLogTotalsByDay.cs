
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

namespace ClientcardFB3
{
    public class TrxLogTotalsByDay
    {
        string connString;
        DataTable dtable;
        SqlCommand command;
        System.Data.SqlClient.SqlConnection conn;
        DataRow dRow;
        int iRowCount = 0;

        public TrxLogTotalsByDay(string connStringIn)
        {
            conn = new System.Data.SqlClient.SqlConnection();
            conn.ConnectionString = connString = connStringIn;
            dtable = new DataTable();
        }

        #region Get/Set Accessors
        
        public DataTable DSet
        { get { return dtable; } }

        public DataRow DRow
        { get { return dRow; } }

        public int RowCount
        { get { return iRowCount; } }

        public DateTime TrxDate
        {
            get { return Convert.ToDateTime(dRow["TrxDate"]); }
            set { dRow["TrxDate"] = value; }
        }
        public string FiscalPeriod
        {
            get { return dRow["FiscalPeriod"].ToString(); }
            set { dRow["FiscalPeriod"] = value; }
        }
        public string YearMonth
        {
            get { return dRow["YearMonth"].ToString(); }
            set { dRow["YearMonth"] = value; }
        }
        public int HHTotalServed
        {
            get { return Convert.ToInt32(dRow["HHTotalServed"]); }
            set { dRow["HHTotalServed"] = value; }
        }
        public int HHNewFiscal
        {
            get { return Convert.ToInt32(dRow["HHNewFiscal"]); }
            set { dRow["HHNewFiscal"] = value; }
        }
        public int HHNewCal
        {
            get { return Convert.ToInt32(dRow["HHNewCal"]); }
            set { dRow["HHNewCal"] = value; }
        }
        public int HHRcvdSupplemental
        {
            get { return Convert.ToInt32(dRow["HHRcvdSupplemental"]); }
            set { dRow["HHRcvdSupplemental"] = value; }
        }
        public int HHNewRcvdSupplemental
        {
            get { return Convert.ToInt32(dRow["HHNewRcvdSupplemental"]); }
            set { dRow["HHNewRcvdSupplemental"] = value; }
        }
        public int HHRcvdCommodity
        {
            get { return Convert.ToInt32(dRow["HHRcvdCommodity"]); }
            set { dRow["HHRcvdCommodity"] = value; }
        }
        public int HHNewRcvdCommodity
        {
            get { return Convert.ToInt32(dRow["HHNewRcvdCommodity"]); }
            set { dRow["HHNewRcvdCommodity"] = value; }
        }
        public int AllInfants
        {
            get { return Convert.ToInt32(dRow["AllInfants"]); }
            set { dRow["AllInfants"] = value; }
        }
        public int AllYouth
        {
            get { return Convert.ToInt32(dRow["AllYouth"]); }
            set { dRow["AllYouth"] = value; }
        }
        public int AllTeens
        {
            get { return Convert.ToInt32(dRow["AllTeens"]); }
            set { dRow["AllTeens"] = value; }
        }
        public int AllEighteen
        {
            get { return Convert.ToInt32(dRow["AllEighteen"]); }
            set { dRow["AllEighteen"] = value; }
        }
        public int AllAdults
        {
            get { return Convert.ToInt32(dRow["AllAdults"]); }
            set { dRow["AllAdults"] = value; }
        }
        public int AllSeniors
        {
            get { return Convert.ToInt32(dRow["AllSeniors"]); }
            set { dRow["AllSeniors"] = value; }
        }
        public int AllTotalFamily
        {
            get { return Convert.ToInt32(dRow["AllTotalFamily"]); }
            set { dRow["AllTotalFamily"] = value; }
        }
        public int NewFiscalInfants
        {
            get { return Convert.ToInt32(dRow["NewFiscalInfants"]); }
            set { dRow["NewFiscalInfants"] = value; }
        }
        public int NewFiscalYouth
        {
            get { return Convert.ToInt32(dRow["NewFiscalYouth"]); }
            set { dRow["NewFiscalYouth"] = value; }
        }
        public int NewFiscalTeens
        {
            get { return Convert.ToInt32(dRow["NewFiscalTeens"]); }
            set { dRow["NewFiscalTeens"] = value; }
        }
        public int NewFiscalEighteen
        {
            get { return Convert.ToInt32(dRow["NewFiscalEighteen"]); }
            set { dRow["NewFiscalEighteen"] = value; }
        }
        public int NewFiscalAdults
        {
            get { return Convert.ToInt32(dRow["NewFiscalAdults"]); }
            set { dRow["NewFiscalAdults"] = value; }
        }
        public int NewFiscalSeniors
        {
            get { return Convert.ToInt32(dRow["NewFiscalSeniors"]); }
            set { dRow["NewFiscalSeniors"] = value; }
        }
        public int NewFiscalTotalFamily
        {
            get { return Convert.ToInt32(dRow["NewFiscalTotalFamily"]); }
            set { dRow["NewFiscalTotalFamily"] = value; }
        }
        public int NewCalInfants
        {
            get { return Convert.ToInt32(dRow["NewCalInfants"]); }
            set { dRow["NewCalInfants"] = value; }
        }
        public int NewCalYouth
        {
            get { return Convert.ToInt32(dRow["NewCalYouth"]); }
            set { dRow["NewCalYouth"] = value; }
        }
        public int NewCalTeens
        {
            get { return Convert.ToInt32(dRow["NewCalTeens"]); }
            set { dRow["NewCalTeens"] = value; }
        }
        public int NewCalEighteen
        {
            get { return Convert.ToInt32(dRow["NewCalEighteen"]); }
            set { dRow["NewCalEighteen"] = value; }
        }
        public int NewCalAdults
        {
            get { return Convert.ToInt32(dRow["NewCalAdults"]); }
            set { dRow["NewCalAdults"] = value; }
        }
        public int NewCalSeniors
        {
            get { return Convert.ToInt32(dRow["NewCalSeniors"]); }
            set { dRow["NewCalSeniors"] = value; }
        }
        public int NewCalTotalFamily
        {
            get { return Convert.ToInt32(dRow["NewCalTotalFamily"]); }
            set { dRow["NewCalTotalFamily"] = value; }
        }
        public int SpecialDiet
        {
            get { return Convert.ToInt32(dRow["SpecialDiet"]); }
            set { dRow["SpecialDiet"] = value; }
        }
        public int NewFiscalSpecialDiet
        {
            get { return Convert.ToInt32(dRow["NewFiscalSpecialDiet"]); }
            set { dRow["NewFiscalSpecialDiet"] = value; }
        }
        public int NewCalSpecialDiet
        {
            get { return Convert.ToInt32(dRow["NewCalSpecialDiet"]); }
            set { dRow["NewCalSpecialDiet"] = value; }
        }
        public int Disabled
        {
            get { return Convert.ToInt32(dRow["Disabled"]); }
            set { dRow["Disabled"] = value; }
        }
        public int NewFiscalDisabled
        {
            get { return Convert.ToInt32(dRow["NewFiscalDisabled"]); }
            set { dRow["NewFiscalDisabled"] = value; }
        }
        public int NewCalDisabled
        {
            get { return Convert.ToInt32(dRow["NewCalDisabled"]); }
            set { dRow["NewCalDisabled"] = value; }
        }
        public int LbsStandard
        {
            get { return Convert.ToInt32(dRow["LbsStandard"]); }
            set { dRow["LbsStandard"] = value; }
        }
        public int LbsOther
        {
            get { return Convert.ToInt32(dRow["LbsOther"]); }
            set { dRow["LbsOther"] = value; }
        }
        public int LbsCommodity
        {
            get { return Convert.ToInt32(dRow["LbsCommodity"]); }
            set { dRow["LbsCommodity"] = value; }
        }
        public int LbsSupplemental
        {
            get { return Convert.ToInt32(dRow["LbsSupplemental"]); }
            set { dRow["LbsSupplemental"] = value; }
        }
        public int LbsBabySvc
        {
            get { return Convert.ToInt32(dRow["LbsBabySvc"]); }
            set { dRow["LbsBabySvc"] = value; }
        }
        public int LbsNonFood
        {
            get { return Convert.ToInt32(dRow["LbsNonFood"]); }
            set { dRow["LbsNonFood"] = value; }
        }
        public int HHHomeless
        {
            get { return Convert.ToInt32(dRow["HHHomeless"]); }
            set { dRow["HHHomeless"] = value; }
        }
        public int NewFiscalHHHomeless
        {
            get { return Convert.ToInt32(dRow["NewFiscalHHHomeless"]); }
            set { dRow["NewFiscalHHHomeless"] = value; }
        }
        public int NewCalHHHomeless
        {
            get { return Convert.ToInt32(dRow["NewCalHHHomeless"]); }
            set { dRow["NewCalHHHomeless"] = value; }
        }
        public int HHInCityLimits
        {
            get { return Convert.ToInt32(dRow["HHInCityLimits"]); }
            set { dRow["HHInCityLimits"] = value; }
        }
        public int NewFiscalHHInCityLimits
        {
            get { return Convert.ToInt32(dRow["NewFiscalHHInCityLimits"]); }
            set { dRow["NewFiscalHHInCityLimits"] = value; }
        }
        public int NewCalHHInCityLimits
        {
            get { return Convert.ToInt32(dRow["NewCalHHInCityLimits"]); }
            set { dRow["NewCalHHInCityLimits"] = value; }
        }
        public int AllTransient
        {
            get { return Convert.ToInt32(dRow["AllTransient"]); }
            set { dRow["AllTransient"] = value; }
        }
        public int NewFiscalTransient
        {
            get { return Convert.ToInt32(dRow["NewFiscalTransient"]); }
            set { dRow["NewFiscalTransient"] = value; }
        }
        public int NewCalTransient
        {
            get { return Convert.ToInt32(dRow["NewCalTransient"]); }
            set { dRow["NewCalTransient"] = value; }
        }
        public int Bags
        {
            get { return Convert.ToInt32(dRow["Bags"]); }
            set { dRow["Bags"] = value; }
        }
        public int Meals
        {
            get { return Convert.ToInt32(dRow["Meals"]); }
            set { dRow["Meals"] = value; }
        }
        public int SingleFemale
        {
            get { return Convert.ToInt32(dRow["SingleFemale"]); }
            set { dRow["SingleFemale"] = value; }
        }
        public int SingleMale
        {
            get { return Convert.ToInt32(dRow["SingleMale"]); }
            set { dRow["SingleMale"] = value; }
        }
        public int SingleOther
        {
            get { return Convert.ToInt32(dRow["SingleOther"]); }
            set { dRow["SingleOther"] = value; }
        }
        public int SingleFemaleNewMonth
        {
            get { return Convert.ToInt32(dRow["SingleFemaleNewMonth"]); }
            set { dRow["SingleFemaleNewMonth"] = value; }
        }
        public int SingleMaleNewMonth
        {
            get { return Convert.ToInt32(dRow["SingleMaleNewMonth"]); }
            set { dRow["SingleMaleNewMonth"] = value; }
        }
        public int SingleOtherNewMonth
        {
            get { return Convert.ToInt32(dRow["SingleOtherNewMonth"]); }
            set { dRow["SingleOtherNewMonth"] = value; }
        }
        public int SingleFemaleNewFiscal
        {
            get { return Convert.ToInt32(dRow["SingleFemaleNewFiscal"]); }
            set { dRow["SingleFemaleNewFiscal"] = value; }
        }
        public int SingleMaleNewFiscal
        {
            get { return Convert.ToInt32(dRow["SingleMaleNewFiscal"]); }
            set { dRow["SingleMaleNewFiscal"] = value; }
        }
        public int SingleOtherNewFiscal
        {
            get { return Convert.ToInt32(dRow["SingleOtherNewFiscal"]); }
            set { dRow["SingleOtherNewFiscal"] = value; }
        }
        public int SingleFemaleNewCal
        {
            get { return Convert.ToInt32(dRow["SingleFemaleNewCal"]); }
            set { dRow["SingleFemaleNewCal"] = value; }
        }
        public int SingleMaleNewCal
        {
            get { return Convert.ToInt32(dRow["SingleMaleNewCal"]); }
            set { dRow["SingleMaleNewCal"] = value; }
        }
        public int SingleOtherNewCal
        {
            get { return Convert.ToInt32(dRow["SingleOtherNewCal"]); }
            set { dRow["SingleOtherNewCal"] = value; }
        }
        public int NewFiscalSingleHeadFemale
        {
            get { return Convert.ToInt32(dRow["NewFiscalSingleHeadFemale"]); }
            set { dRow["NewFiscalSingleHeadFemale"] = value; }
        }
        public int NewFiscalSingleHeadMale
        {
            get { return Convert.ToInt32(dRow["NewFiscalSingleHeadMale"]); }
            set { dRow["NewFiscalSingleHeadMale"] = value; }
        }
        public int NewFiscalSingleHeadOther
        {
            get { return Convert.ToInt32(dRow["NewFiscalSingleHeadOther"]); }
            set { dRow["NewFiscalSingleHeadOther"] = value; }
        }
        public int NewCalSingleHeadFemale
        {
            get { return Convert.ToInt32(dRow["NewCalSingleHeadFemale"]); }
            set { dRow["NewCalSingleHeadFemale"] = value; }
        }
        public int NewCalSingleHeadMale
        {
            get { return Convert.ToInt32(dRow["NewCalSingleHeadMale"]); }
            set { dRow["NewCalSingleHeadMale"] = value; }
        }
        public int NewCalSingleHeadOther
        {
            get { return Convert.ToInt32(dRow["NewCalSingleHeadOther"]); }
            set { dRow["NewCalSingleHeadOther"] = value; }
        }
        public int AllSingleHeadFemale
        {
            get { return Convert.ToInt32(dRow["AllSingleHeadFemale"]); }
            set { dRow["AllSingleHeadFemale"] = value; }
        }
        public int AllSingleheadMale
        {
            get { return Convert.ToInt32(dRow["AllSingleheadMale"]); }
            set { dRow["AllSingleheadMale"] = value; }
        }
        public int AllSingleHeadOther
        {
            get { return Convert.ToInt32(dRow["AllSingleHeadOther"]); }
            set { dRow["AllSingleHeadOther"] = value; }
        }
        #endregion Get/Set Accessors

        public void setRecord(int index)
        {
            if (iRowCount < index)
                dRow = dtable.Rows[index];
        }

        public void find(string Period)
        {
            for (int i = 0; i < iRowCount; i++)
            {
                if (dtable.Rows[i]["FiscalPeriod"].ToString() == Period)
                {
                    dRow = dtable.Rows[i];
                    return;
                }
            }
        }

        public void open(SqlCommand comm)
        {
            dtable = new DataTable();
            TransferDataToLocalDataTable(comm, dtable);
            if (iRowCount > 0)
                dRow = dtable.Rows[0];
        }

        private void TransferDataToLocalDataTable(SqlCommand sqlCmd, DataTable dt)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();        // Open the connection and execute the reader.

                SqlDataReader reader = sqlCmd.ExecuteReader();

                if (reader.HasRows)
                {
                    DataTable readerSchema = reader.GetSchemaTable().Copy();
                    for (int i = 0; i < readerSchema.Rows.Count; i++)
                    {
                        dt.Columns.Add(readerSchema.Rows[i]["ColumnName"].ToString());
                    }
                    while (reader.Read())
                    {
                        object[] values = new object[reader.FieldCount];
                        for (int i = 0; i < values.Length; i++)
                        {
                            if (values[i] == DBNull.Value)
                                values[i] = 0;
                        }
                        reader.GetValues(values);
                        dt.Rows.Add(values);
                    }
                }
            }
            catch { };

            if (conn.State == ConnectionState.Open)
                conn.Close();

            iRowCount = dt.Rows.Count;
        }
    }
}

