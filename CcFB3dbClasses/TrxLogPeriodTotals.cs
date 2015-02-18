
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Diagnostics;
using Microsoft.SqlServer.Server;

namespace ClientcardFB3
{
    public class TrxLogPeriodTotals : IDisposable
    {
        string connString;
        DataTable dtable;
        System.Data.SqlClient.SqlConnection conn;
        DataRow drow;
        DataRow drowYTD;
        int iRowCount = 0;
        int iCurrentRow = 0;
        const string constKeyName = "FiscalPeriod";
        private bool _disposed;

        public TrxLogPeriodTotals(string connStringIn)
        {
            conn = new System.Data.SqlClient.SqlConnection();
            conn.ConnectionString = connString = connStringIn;
            dtable = new DataTable();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            // If you need thread safety, use a lock around these 
            // operations, as well as in your methods that use the resource.
            if (!_disposed)
            {
                if (disposing)
                {
                    if (conn != null)
                        conn.Dispose();
                    if (dtable != null)
                        dtable.Dispose();
                }

                // Indicate that the instance has been disposed.
                conn = null;
                dtable = null;
                _disposed = true;
            }
        }

        #region Get/Set Accessors
        public int CurrentRow
        {
            get { return iCurrentRow; }
        }
        public int RowCount
        {
            get { return iRowCount; }
        }
        public DateTime TrxDate
        {
            get { return Convert.ToDateTime("TrxDate"); }
            set { drow["TrxDate"] = value; }
        }
        public string FiscalPeriod
        {
            get { return drow["FiscalPeriod"].ToString(); }
            set { drow["FiscalPeriod"] = value; }
        }
        public string YearMonth
        {
            get { return drow["YearMonth"].ToString(); }
            set { drow["YearMonth"] = value; }
        }
        public int HHTotalServed
        {
            get { return Convert.ToInt32(drow["HHTotalServed"]); }
            set { drow["HHTotalServed"] = value; }
        }
        public int HHRcvdCommodity
        {
            get { return Convert.ToInt32(drow["HHRcvdCommodity"]); }
            set { drow["HHRcvdCommodity"] = value; }
        }
        public int HHRcvdStd
        {
            get { return Convert.ToInt32(drow["HHRcvdStd"]); }
            set { drow["HHRcvdStd"] = value; }
        }
        public int HHRcvdSupplemental
        {
            get { return Convert.ToInt32(drow["HHRcvdSupplemental"]); }
            set { drow["HHRcvdSupplemental"] = value; }
        }
        public int HHRcvdBabyServices
        {
            get { return Convert.ToInt32(drow["HHRcvdBabyServices"]); }
            set { drow["HHRcvdBabyServices"] = value; }
        }
        public int HHHomeless
        {
            get { return Convert.ToInt32(drow["HHHomeless"]); }
            set { drow["HHHomeless"] = value; }
        }
        public int HHInCityLimits
        {
            get { return Convert.ToInt32(drow["HHInCityLimits"]); }
            set { drow["HHInCityLimits"] = value; }
        }
        public int HHTransient
        {
            get { return Convert.ToInt32(drow["HHTransient"]); }
            set { drow["HHTransient"] = value; }
        }
        public int HHSingleFemale
        {
            get { return Convert.ToInt32(drow["HHSingleFemale"]); }
            set { drow["HHSingleFemale"] = value; }
        }
        public int HHSingleMale
        {
            get { return Convert.ToInt32(drow["HHSingleMale"]); }
            set { drow["HHSingleMale"] = value; }
        }
        public int HHSingleOther
        {
            get { return Convert.ToInt32(drow["HHSingleOther"]); }
            set { drow["HHSingleOther"] = value; }
        }
        public int HHOneParentFemale
        {
            get { return Convert.ToInt32(drow["HHOneParentFemale"]); }
            set { drow["HHOneParentFemale"] = value; }
        }
        public int HHOneParentMale
        {
            get { return Convert.ToInt32(drow["HHOneParentMale"]); }
            set { drow["HHOneParentMale"] = value; }
        }
        public int HHOneParentOther
        {
            get { return Convert.ToInt32(drow["HHOneParentOther"]); }
            set { drow["HHOneParentOther"] = value; }
        }
        public int HHTotalServedNew
        {
            get { return Convert.ToInt32(drow["HHTotalServedNew"]); }
            set { drow["HHTotalServedNew"] = value; }
        }
        public int HHRcvdCommodityNew
        {
            get { return Convert.ToInt32(drow["HHRcvdCommodityNew"]); }
            set { drow["HHRcvdCommodityNew"] = value; }
        }
        public int HHRcvdStdNew
        {
            get { return Convert.ToInt32(drow["HHRcvdStdNew"]); }
            set { drow["HHRcvdStdNew"] = value; }
        }
        public int HHRcvdSupplementalNew
        {
            get { return Convert.ToInt32(drow["HHRcvdSupplementalNew"]); }
            set { drow["HHRcvdSupplementalNew"] = value; }
        }
        public int HHRcvdBabyServicesNew
        {
            get { return Convert.ToInt32(drow["HHRcvdBabyServicesNew"]); }
            set { drow["HHRcvdBabyServicesNew"] = value; }
        }
        public int HHHomelessNew
        {
            get { return Convert.ToInt32(drow["HHHomelessNew"]); }
            set { drow["HHHomelessNew"] = value; }
        }
        public int HHInCityLimitsNew
        {
            get { return Convert.ToInt32(drow["HHInCityLimitsNew"]); }
            set { drow["HHInCityLimitsNew"] = value; }
        }
        public int HHTransientNew
        {
            get { return Convert.ToInt32(drow["HHTransientNew"]); }
            set { drow["HHTransientNew"] = value; }
        }
        public int HHSingleFemaleNew
        {
            get { return Convert.ToInt32(drow["HHSingleFemaleNew"]); }
            set { drow["HHSingleFemaleNew"] = value; }
        }
        public int HHSingleMaleNew
        {
            get { return Convert.ToInt32(drow["HHSingleMaleNew"]); }
            set { drow["HHSingleMaleNew"] = value; }
        }
        public int HHSingleOtherNew
        {
            get { return Convert.ToInt32(drow["HHSingleOtherNew"]); }
            set { drow["HHSingleOtherNew"] = value; }
        }
        public int HHOneParentFemaleNew
        {
            get { return Convert.ToInt32(drow["HHOneParentFemaleNew"]); }
            set { drow["HHOneParentFemaleNew"] = value; }
        }
        public int HHOneParentMaleNew
        {
            get { return Convert.ToInt32(drow["HHOneParentMaleNew"]); }
            set { drow["HHOneParentMaleNew"] = value; }
        }
        public int HHOneParentOtherNew
        {
            get { return Convert.ToInt32(drow["HHOneParentOtherNew"]); }
            set { drow["HHOneParentOtherNew"] = value; }
        }


        public int HHTotalServedReturning
        {
            get { return Convert.ToInt32(drow["HHTotalServedReturning"]); }
            set { drow["HHTotalServedReturning"] = value; }
        }
        public int HHRcvdCommodityReturning
        {
            get { return Convert.ToInt32(drow["HHRcvdCommodityReturning"]); }
            set { drow["HHRcvdCommodityReturning"] = value; }
        }
        public int HHRcvdStdReturning
        {
            get { return Convert.ToInt32(drow["HHRcvdStdReturning"]); }
            set { drow["HHRcvdStdReturning"] = value; }
        }
        public int HHRcvdSupplementalReturning
        {
            get { return Convert.ToInt32(drow["HHRcvdSupplementalReturning"]); }
            set { drow["HHRcvdSupplementalReturning"] = value; }
        }
        public int HHRcvdBabyServicesReturning
        {
            get { return Convert.ToInt32(drow["HHRcvdBabyServicesReturning"]); }
            set { drow["HHRcvdBabyServicesReturning"] = value; }
        }
        public int HHHomelessReturning
        {
            get { return Convert.ToInt32(drow["HHHomelessReturning"]); }
            set { drow["HHHomelessReturning"] = value; }
        }
        public int HHInCityLimitsReturning
        {
            get { return Convert.ToInt32(drow["HHInCityLimitsReturning"]); }
            set { drow["HHInCityLimitsReturning"] = value; }
        }
        public int HHTransientReturning
        {
            get { return Convert.ToInt32(drow["HHTransientReturning"]); }
            set { drow["HHTransientReturning"] = value; }
        }
        public int HHSingleFemaleReturning
        {
            get { return Convert.ToInt32(drow["HHSingleFemaleReturning"]); }
            set { drow["HHSingleFemaleReturning"] = value; }
        }
        public int HHSingleMaleReturning
        {
            get { return Convert.ToInt32(drow["HHSingleMaleReturning"]); }
            set { drow["HHSingleMaleReturning"] = value; }
        }
        public int HHSingleOtherReturning
        {
            get { return Convert.ToInt32(drow["HHSingleOtherReturning"]); }
            set { drow["HHSingleOtherReturning"] = value; }
        }
        public int HHOneParentFemaleReturning
        {
            get { return Convert.ToInt32(drow["HHOneParentFemaleReturning"]); }
            set { drow["HHOneParentFemaleReturning"] = value; }
        }
        public int HHOneParentMaleReturning
        {
            get { return Convert.ToInt32(drow["HHOneParentMaleReturning"]); }
            set { drow["HHOneParentMaleReturning"] = value; }
        }
        public int HHOneParentOtherReturning
        {
            get { return Convert.ToInt32(drow["HHOneParentOtherReturning"]); }
            set { drow["HHOneParentOtherReturning"] = value; }
        }

        public int Infants
        {
            get { return Convert.ToInt32(drow["Infants"]); }
            set { drow["Infants"] = value; }
        }
        public int Youth
        {
            get { return Convert.ToInt32(drow["Youth"]); }
            set { drow["Youth"] = value; }
        }
        public int Children
        {
            get { return Convert.ToInt32(drow["Children"]); }
            set { drow["Children"] = value; }
        }

        public int Teens
        {
            get { return Convert.ToInt32(drow["Teens"]); }
            set { drow["Teens"] = value; }
        }
        public int Eighteen
        {
            get { return Convert.ToInt32(drow["Eighteen"]); }
            set { drow["Eighteen"] = value; }
        }
        public int Adults
        {
            get { return Convert.ToInt32(drow["Adults"]); }
            set { drow["Adults"] = value; }
        }
        public int Seniors
        {
            get { return Convert.ToInt32(drow["Seniors"]); }
            set { drow["Seniors"] = value; }
        }
        public int TotalFamily
        {
            get { return Convert.ToInt32(drow["TotalFamily"]); }
            set { drow["TotalFamily"] = value; }
        }
        public int InfantsNew
        {
            get { return Convert.ToInt32(drow["InfantsNew"]); }
            set { drow["InfantsNew"] = value; }
        }
        public int YouthNew
        {
            get { return Convert.ToInt32(drow["YouthNew"]); }
            set { drow["YouthNew"] = value; }
        }
        public int ChildrenNew
        {
            get { return Convert.ToInt32(drow["ChildrenNew"]); }
            set { drow["ChildrenNew"] = value; }
        }
        public int TeensNew
        {
            get { return Convert.ToInt32(drow["TeensNew"]); }
            set { drow["TeensNew"] = value; }
        }
        public int EighteenNew
        {
            get { return Convert.ToInt32(drow["EighteenNew"]); }
            set { drow["EighteenNew"] = value; }
        }
        public int AdultsNew
        {
            get { return Convert.ToInt32(drow["AdultsNew"]); }
            set { drow["AdultsNew"] = value; }
        }
        public int SeniorsNew
        {
            get { return Convert.ToInt32(drow["SeniorsNew"]); }
            set { drow["SeniorsNew"] = value; }
        }
        public int TotalFamilyNew
        {
            get { return Convert.ToInt32(drow["TotalFamilyNew"]); }
            set { drow["TotalFamilyNew"] = value; }
        }
        public int CntSpecialDiet
        {
            get { return Convert.ToInt32(drow["CntSpecialDiet"]); }
            set { drow["CntSpecialDiet"] = value; }
        }
        public int CntSpecialDietNew
        {
            get { return Convert.ToInt32(drow["CntSpecialDietNew"]); }
            set { drow["CntSpecialDietNew"] = value; }
        }
        public int CntSpecialDietReturning
        {
            get { return Convert.ToInt32(drow["CntSpecialDietReturning"]); }
            set { drow["CntSpecialDietReturning"] = value; }
        }
        public int CntDisabled
        {
            get { return Convert.ToInt32(drow["CntDisabled"]); }
            set { drow["CntDisabled"] = value; }
        }
        public int CntDisabledNew
        {
            get { return Convert.ToInt32(drow["CntDisabledNew"]); }
            set { drow["CntDisabledNew"] = value; }
        }
        public int CntDisabledReturning
        {
            get { return Convert.ToInt32(drow["CntDisabledReturning"]); }
            set { drow["CntDisabledReturning"] = value; }
        }
        public int LbsStandard
        {
            get { return Convert.ToInt32(drow["LbsStandard"]); }
            set { drow["LbsStandard"] = value; }
        }
        public int LbsOther
        {
            get { return Convert.ToInt32(drow["LbsOther"]); }
            set { drow["LbsOther"] = value; }
        }
        public int LbsCommodity
        {
            get { return Convert.ToInt32(drow["LbsCommodity"]); }
            set { drow["LbsCommodity"] = value; }
        }
        public int LbsSupplemental
        {
            get { return Convert.ToInt32(drow["LbsSupplemental"]); }
            set { drow["LbsSupplemental"] = value; }
        }
        public int LbsBabySvc
        {
            get { return Convert.ToInt32(drow["LbsBabySvc"]); }
            set { drow["LbsBabySvc"] = value; }
        }
        public int LbsNonFood
        {
            get { return Convert.ToInt32(drow["LbsNonFood"]); }
            set { drow["LbsNonFood"] = value; }
        }
        public int LbsTotalServed
        {
            get { return Convert.ToInt32(drow["LbsStandard"]) + Convert.ToInt32(drow["LbsOther"]) + Convert.ToInt32(drow["LbsCommodity"]) + Convert.ToInt32(drow["LbsSupplemental"]) + Convert.ToInt32(drow["LbsBabySvc"]); }
        }
        public int Bags
        {
            get { return Convert.ToInt32(drow["Bags"]); }
            set { drow["Bags"] = value; }
        }
        public int Meals
        {
            get { return Convert.ToInt32(drow["Meals"]); }
            set { drow["Meals"] = value; }
        }
        public int NbrDaysOpen
        {
            get { return Convert.ToInt32(drow["NbrDaysOpen"]); }
            set { drow["NbrDaysOpen"] = value; }
        }
        public int InfantsReturning
        {
            get { return Convert.ToInt32(drow["InfantsReturning"]); }
        }
        public int YouthReturning
        {
            get { return Convert.ToInt32(drow["YouthReturning"]); }
        }
        public int ChildrenReturning
        {
            get { return Convert.ToInt32(drow["ChildrenReturning"]); }
        }
        public int TeensReturning
        {
            get { return Convert.ToInt32(drow["TeensReturning"]); }
        }
        public int EighteenReturning
        {
            get { return Convert.ToInt32(drow["EighteenReturning"]); }
        }
        public int AdultsReturning
        {
            get { return Convert.ToInt32(drow["AdultsReturning"]); }
        }
        public int SeniorsReturning
        {
            get { return Convert.ToInt32(drow["SeniorsReturning"]); }
        }
        public int TotalFamilyReturning
        {
            get { return Convert.ToInt32(drow["TotalFamilyReturning"]); }
        }

        public int White
        {
            get { return Convert.ToInt32(drow["White"]); }
            set { drow["White"] = value; }
        }
        public int Black
        {
            get { return Convert.ToInt32(drow["Black"]); }
            set { drow["Black"] = value; }
        }
        public int Asian
        {
            get { return Convert.ToInt32(drow["Asian"]); }
            set { drow["Asian"] = value; }
        }
        public int Native
        {
            get { return Convert.ToInt32(drow["Native"]); }
            set { drow["Native"] = value; }
        }
        public int Pacific
        {
            get { return Convert.ToInt32(drow["Pacific"]); }
            set { drow["Pacific"] = value; }
        }
        public int WhiteNative
        {
            get { return Convert.ToInt32(drow["WhiteNative"]); }
            set { drow["WhiteNative"] = value; }
        }
        public int WhiteAsian
        {
            get { return Convert.ToInt32(drow["WhiteAsian"]); }
            set { drow["WhiteAsian"] = value; }
        }
        public int WhiteBlack
        {
            get { return Convert.ToInt32(drow["WhiteBlack"]); }
            set { drow["WhiteBlack"] = value; }
        }
        public int BlackNative
        {
            get { return Convert.ToInt32(drow["BlackNative"]); }
            set { drow["BlackNative"] = value; }
        }
        public int Other
        {
            get { return Convert.ToInt32(drow["Other"]); }
            set { drow["Other"] = value; }
        }
        public int Undisclosed
        {
            get { return Convert.ToInt32(drow["Undisclosed"]); }
            set { drow["Undisclosed"] = value; }
        }

        public int WhiteHispanic
        {
            get { return Convert.ToInt32(drow["WhiteHispanic"]); }
            set { drow["WhiteHispanic"] = value; }
        }
        public int BlackHispanic
        {
            get { return Convert.ToInt32(drow["BlackHispanic"]); }
            set { drow["BlackHispanic"] = value; }
        }
        public int AsianHispanic
        {
            get { return Convert.ToInt32(drow["AsianHispanic"]); }
            set { drow["AsianHispanic"] = value; }
        }
        public int NativeHispanic
        {
            get { return Convert.ToInt32(drow["NativeHispanic"]); }
            set { drow["NativeHispanic"] = value; }
        }
        public int PacificHispanic
        {
            get { return Convert.ToInt32(drow["PacificHispanic"]); }
            set { drow["PacificHispanic"] = value; }
        }
        public int WhiteNativeHispanic
        {
            get { return Convert.ToInt32(drow["WhiteNativeHispanic"]); }
            set { drow["WhiteNativeHispanic"] = value; }
        }
        public int WhiteAsianHispanic
        {
            get { return Convert.ToInt32(drow["WhiteAsianHispanic"]); }
            set { drow["WhiteAsianHispanic"] = value; }
        }
        public int WhiteBlackHispanic
        {
            get { return Convert.ToInt32(drow["WhiteBlackHispanic"]); }
            set { drow["WhiteBlackHispanic"] = value; }
        }
        public int BlackNativeHispanic
        {
            get { return Convert.ToInt32(drow["BlackNativeHispanic"]); }
            set { drow["BlackNativeHispanic"] = value; }
        }
        public int OtherHispanic
        {
            get { return Convert.ToInt32(drow["OtherHispanic"]); }
            set { drow["OtherHispanic"] = value; }
        }
        public int UndisclosedHispanic
        {
            get { return Convert.ToInt32(drow["UndisclosedHispanic"]); }
            set { drow["UndisclosedHispanic"] = value; }
        }


        public int WhiteNew
        {
            get { return Convert.ToInt32(drow["WhiteNew"]); }
            set { drow["WhiteNew"] = value; }
        }
        public int BlackNew
        {
            get { return Convert.ToInt32(drow["BlackNew"]); }
            set { drow["BlackNew"] = value; }
        }
        public int AsianNew
        {
            get { return Convert.ToInt32(drow["AsianNew"]); }
            set { drow["AsianNew"] = value; }
        }
        public int NativeNew
        {
            get { return Convert.ToInt32(drow["NativeNew"]); }
            set { drow["NativeNew"] = value; }
        }
        public int PacificNew
        {
            get { return Convert.ToInt32(drow["PacificNew"]); }
            set { drow["PacificNew"] = value; }
        }
        public int WhiteNativeNew
        {
            get { return Convert.ToInt32(drow["WhiteNativeNew"]); }
            set { drow["WhiteNativeNew"] = value; }
        }
        public int WhiteAsianNew
        {
            get { return Convert.ToInt32(drow["WhiteAsianNew"]); }
            set { drow["WhiteAsianNew"] = value; }
        }
        public int WhiteBlackNew
        {
            get { return Convert.ToInt32(drow["WhiteBlackNew"]); }
            set { drow["WhiteBlackNew"] = value; }
        }
        public int BlackNativeNew
        {
            get { return Convert.ToInt32(drow["BlackNativeNew"]); }
            set { drow["BlackNativeNew"] = value; }
        }
        public int OtherNew
        {
            get { return Convert.ToInt32(drow["OtherNew"]); }
            set { drow["OtherNew"] = value; }
        }
        public int UndisclosedNew
        {
            get { return Convert.ToInt32(drow["UndisclosedNew"]); }
            set { drow["UndisclosedNew"] = value; }
        }

        public int WhiteHispanicNew
        {
            get { return Convert.ToInt32(drow["WhiteHispanicNew"]); }
            set { drow["WhiteHispanicNew"] = value; }
        }
        public int BlackHispanicNew
        {
            get { return Convert.ToInt32(drow["BlackHispanicNew"]); }
            set { drow["BlackHispanicNew"] = value; }
        }
        public int AsianHispanicNew
        {
            get { return Convert.ToInt32(drow["AsianHispanicNew"]); }
            set { drow["AsianHispanicNew"] = value; }
        }
        public int NativeHispanicNew
        {
            get { return Convert.ToInt32(drow["NativeHispanicNew"]); }
            set { drow["NativeHispanicNew"] = value; }
        }
        public int PacificHispanicNew
        {
            get { return Convert.ToInt32(drow["PacificHispanicNew"]); }
            set { drow["PacificHispanicNew"] = value; }
        }
        public int WhiteNativeHispanicNew
        {
            get { return Convert.ToInt32(drow["WhiteNativeHispanicNew"]); }
            set { drow["WhiteNativeHispanicNew"] = value; }
        }
        public int WhiteAsianHispanicNew
        {
            get { return Convert.ToInt32(drow["WhiteAsianHispanicNew"]); }
            set { drow["WhiteAsianHispanicNew"] = value; }
        }
        public int WhiteBlackHispanicNew
        {
            get { return Convert.ToInt32(drow["WhiteBlackHispanicNew"]); }
            set { drow["WhiteBlackHispanicNew"] = value; }
        }
        public int BlackNativeHispanicNew
        {
            get { return Convert.ToInt32(drow["BlackNativeHispanicNew"]); }
            set { drow["BlackNativeHispanicNew"] = value; }
        }
        public int OtherHispanicNew
        {
            get { return Convert.ToInt32(drow["OtherHispanicNew"]); }
            set { drow["OtherHispanicNew"] = value; }
        }
        public int UndisclosedHispanicNew
        {
            get { return Convert.ToInt32(drow["UndisclosedHispanicNew"]); }
            set { drow["UndisclosedHispanicNew"] = value; }
        }
        #endregion Get/Set Accessors
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
            Debug.Assert(FieldName.StartsWith("HH"), FieldName);
            try
            {
                return drow[FieldName];
            }
            catch
            {
                return "0";
            }
        }


        //Gets property through use of just the column name in database as string
        public object GetDataString(string FieldName)
        {
            if (dtable.Rows.Count > 0)
            {
                int fldIndex = dtable.Columns.IndexOf(FieldName);
                if (fldIndex >= 0)
                {
                    if (dtable.Columns[fldIndex].DataType.Name == "DateTime")
                        if (drow[FieldName].ToString().Length >0)
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
        /// Sets the DataRow for the given RowIndex
        /// </summary>
        /// <param name='rowIndex'></param>
        public DataRow setDataRow(int rowindex)
        {
            if (rowindex < iRowCount)
            {
                drow = dtable.Rows[rowindex];
                iCurrentRow = rowindex;
                CalculateCum();
                return drow;
            }
            return null;
        }

        public Boolean findFiscalPeriod(string key)
        {
            for (int i = 0; i < iRowCount; i++)
            {
                if (dtable.Rows[i]["FiscalPeriod"].ToString() == key)
                {
                    drow = dtable.Rows[i];
                    iCurrentRow = i;
                    CalculateCum();
                    return true;
                }
            }
            return false;
        }

        public Boolean findFiscalPeriod(int period)
        {
            string key = CCFBGlobal.formatNumberWithLeadingZero(period);
            for (int i = 0; i < iRowCount; i++)
            {
                if (dtable.Rows[i]["FiscalPeriod"].ToString().EndsWith(key) == true)
                {
                    drow = dtable.Rows[i];
                    iCurrentRow = i;
                    CalculateCum();
                    return true;
                }
            }
            return false;
        }

        public void findYearMonth(string key)
        {
            for (int i = 0; i < iRowCount; i++)
            {
                if (dtable.Rows[i]["YearMonth"].ToString() == key)
                {
                    drow = dtable.Rows[i];
                    iCurrentRow = i;
                    CalculateCum();
                    return;
                }
            }
        }

        public void open(string startDate, string endDate)
        {
            //SP Uses TrxLogFiscalTotalsByDay View
            dtable = new DataTable();
            SqlCommand sqlCmd = new SqlCommand("MonthEndStatistics", conn);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add(new SqlParameter("@StartDate", startDate));
            sqlCmd.Parameters.Add(new SqlParameter("@EndDate", endDate));
            iRowCount = TransferDataToLocalDataTable(sqlCmd, dtable);
            if (iRowCount > 0)
            {
                drow = dtable.Rows[0];
            }
            iCurrentRow = 0;
        }

        public void openCal(string startDate, string endDate)
        {
            //SP Uses TrxLogFiscalTotalsByDay View
            dtable = new DataTable();
            SqlCommand sqlCmd = new SqlCommand("MonthEndCalStatistics", conn);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add(new SqlParameter("@StartDate", startDate));
            sqlCmd.Parameters.Add(new SqlParameter("@EndDate", endDate));
            iRowCount = TransferDataToLocalDataTable(sqlCmd, dtable);
            if (iRowCount > 0)
            {
                drow = dtable.Rows[0];
            }
            iCurrentRow = 0;
        }

        public void openHD(string startDate, string endDate)
        {
            //SP Uses TrxLogFiscalTotalsHDByDay View
            dtable = new DataTable();
            SqlCommand sqlCmd = new SqlCommand("MonthEndHDStatistics", conn);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add(new SqlParameter("@StartDate", startDate));
            sqlCmd.Parameters.Add(new SqlParameter("@EndDate", endDate));
            iRowCount = TransferDataToLocalDataTable(sqlCmd, dtable);
            if (iRowCount > 0)
                drow = dtable.Rows[0];
            iCurrentRow = 0;
        }

        public void setYTDRow()
        {
            drow = drowYTD;
        }

        private int TransferDataToLocalDataTable(SqlCommand sqlCmd, DataTable dt)
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

            return dt.Rows.Count;
        }
 
        private void CalculateCum()
        {
            drowYTD = dtable.NewRow();
            drowYTD["FiscalPeriod"] = drowYTD["YearMonth"] = FiscalPeriod.Substring(0, 4) + "99";
            for (int i = 2; i < dtable.Columns.Count; i++)
            {
                drowYTD[i] = 0;
            }
            for (int r = 0; r <= iCurrentRow; r++)
            {
                for (int i = 2; i < dtable.Columns.Count; i++)
                {
                    drowYTD[i] = Convert.ToInt32(drowYTD[i]) + Convert.ToInt32(dtable.Rows[r][i]);
                }
            }
        }
    }
}

