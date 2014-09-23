using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Office.Interop.Word;

namespace ClientcardFB3
{
    class RptEFNDoc
    {
        TrxLogPeriodTotals clsMonthStats;
        bool error = false;

        public bool Error
        {
            get
            {
                return error;
            }
        }

        public RptEFNDoc(TrxLogPeriodTotals statsTrxLog)
        {
            clsMonthStats = statsTrxLog;
        }

        public void createReport(string foodBankName, string monthyear
                               , string preparedBy, object saveAs, string templatePath
                               , string rptDate, string namePeriod)
        {
            Object oMissing = System.Reflection.Missing.Value;

            object missing = System.Reflection.Missing.Value;
            Object oTrue = true;
            Object oFalse = false;
            Application oWord = new Application();
            Document oWordDoc = new Document();
            oWord.Visible = false;
            //DirectoryInfo di = new DirectoryInfo(Defaults.getVal("ExportFile", "Reports"));

            //if (di.Exists == false)

            //    di.Create();
            Object oTemplatePath = templatePath;

            try
            {

                oWordDoc = oWord.Documents.Add(ref oTemplatePath, ref oMissing, ref oMissing, ref oMissing);

                //Save so that the template is free to be used by the next user
                oWordDoc.SaveAs(ref saveAs, ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing);

                Object oBookMarkName = "PeriodName";
                oWordDoc.Bookmarks.get_Item(ref oBookMarkName).Range.Text = namePeriod;
                //FoodBankNmae,ContactPerson,Month/Year
                Table table = oWordDoc.Tables[1];
                table.Cell(1, 2).Range.Text = foodBankName;
                table.Cell(2, 2).Range.Text = preparedBy;
                table.Cell(2, 4).Range.Text = monthyear;
                //Family Counts
                table = oWordDoc.Tables[2];
                table.Cell(3, 2).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.InfantsNew);
                table.Cell(4, 2).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.YouthNew + clsMonthStats.TeensNew + clsMonthStats.EighteenNew);
                table.Cell(5, 2).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.AdultsNew);
                table.Cell(6, 2).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.SeniorsNew);
                table.Cell(7, 2).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.TotalFamilyNew);

                table.Cell(3, 3).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.InfantsReturning);
                table.Cell(4, 3).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.YouthReturning + clsMonthStats.TeensReturning + clsMonthStats.EighteenReturning);
                table.Cell(5, 3).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.AdultsReturning);
                table.Cell(6, 3).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.SeniorsReturning);
                table.Cell(7, 3).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.TotalFamilyReturning);

                table.Cell(9, 2).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.HHTotalServedNew);
                table.Cell(9, 3).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.HHTotalServedReturning);

                table.Cell(11, 2).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.CntSpecialDietNew);
                table.Cell(11, 3).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.CntSpecialDietReturning);

                table.Cell(13, 2).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.HHRcvdSupplementalNew);
                table.Cell(13, 3).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.HHRcvdSupplementalReturning);
                //Lbs Served
                table = oWordDoc.Tables[3];
                table.Cell(1, 2).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.LbsTotalServed);
                table.Cell(2, 2).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.LbsSupplemental);
                //Race
                table = oWordDoc.Tables[4];
                table.Cell(2, 2).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.WhiteNew);
                table.Cell(3, 2).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.BlackNew);
                table.Cell(4, 2).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.AsianNew);
                table.Cell(5, 2).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.NativeNew);
                table.Cell(6, 2).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.PacificNew);
                table.Cell(7, 2).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.WhiteNativeNew);
                table.Cell(8, 2).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.WhiteAsianNew);
                table.Cell(9, 2).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.WhiteBlackNew);
                table.Cell(10, 2).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.BlackNativeNew);
                table.Cell(11, 2).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.OtherNew);
                table.Cell(12, 2).Range.Text = CCFBGlobal.formatNumberWithCommas(
                    clsMonthStats.WhiteNew + clsMonthStats.BlackNew + clsMonthStats.AsianNew + clsMonthStats.NativeNew + clsMonthStats.PacificNew
                  + clsMonthStats.WhiteNativeNew + clsMonthStats.WhiteAsianNew + clsMonthStats.WhiteBlackNew + clsMonthStats.BlackNativeNew + clsMonthStats.OtherNew);

                table.Cell(2, 3).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.WhiteHispanicNew);
                table.Cell(3, 3).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.BlackHispanicNew);
                table.Cell(4, 3).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.AsianHispanicNew);
                table.Cell(5, 3).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.NativeHispanicNew);
                table.Cell(6, 3).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.PacificHispanicNew);
                table.Cell(7, 3).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.WhiteNativeHispanicNew);
                table.Cell(8, 3).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.WhiteAsianHispanicNew);
                table.Cell(9, 3).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.WhiteBlackHispanicNew);
                table.Cell(10, 3).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.BlackNativeHispanicNew);
                table.Cell(11, 3).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.OtherHispanicNew);
                table.Cell(12, 3).Range.Text = CCFBGlobal.formatNumberWithCommas(
                    clsMonthStats.WhiteHispanicNew + + clsMonthStats.BlackHispanicNew + + clsMonthStats.AsianHispanicNew + + clsMonthStats.NativeHispanicNew + + clsMonthStats.PacificHispanicNew
                  + clsMonthStats.WhiteNativeHispanicNew + + clsMonthStats.WhiteAsianHispanicNew + + clsMonthStats.WhiteBlackHispanicNew + + clsMonthStats.BlackNativeHispanicNew + + clsMonthStats.OtherHispanicNew);
                /*************************************************************************************/

                oWordDoc.SaveAs(ref saveAs, ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing);

                ((_Application)oWord).Quit(SaveChanges: false, OriginalFormat: false, RouteDocument: false);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oWord);

                CCFBGlobal.openDocumentOutsideCCFB(saveAs.ToString());
            }
            catch (Exception ex)
            {
                CCFBGlobal.appendErrorToErrorReport("File Path = " + oTemplatePath.ToString(),
                        ex.GetBaseException().ToString());
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oWord);
                error = true;
            }
        }
    }
}
