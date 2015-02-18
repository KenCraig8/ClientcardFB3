using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Office.Interop.Word;

namespace ClientcardFB3
{
    class RptNewEFAP : IDisposable
    {
        TrxLogPeriodTotals clsMonthStats;
        bool error = false;
        bool _disposed = false;

        public bool Error
        {
            get
            {
                return error;
            }
        }

        public RptNewEFAP(TrxLogPeriodTotals clsStatsIn)
        {
            clsMonthStats = clsStatsIn;
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
                    if (clsMonthStats != null)
                        clsMonthStats.Dispose();
                }

                // Indicate that the instance has been disposed.
                clsMonthStats = null;
                _disposed = true;
            }
        }

        public void createReport(string foodBankName, string month, string county,
            string preparedBy, object saveAs, string templatePath, string rptDate)
        {
            Object oMissing = System.Reflection.Missing.Value;
            Object missing = System.Reflection.Missing.Value;
            Object oTrue = true;
            Object oFalse = false;
            Application oWord = new Application();
            Document oWordDoc = new Document();
            oWord.Visible = false;
            
                Object oTemplatePath = templatePath;
                try
                {
                    oWordDoc = oWord.Documents.Add(ref oTemplatePath, ref oMissing, ref oMissing, ref oMissing);

                    //Save so that the template is free to be used by the next user
                    oWordDoc.SaveAs(ref saveAs, ref missing, ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref missing, ref missing);

                    Object oBookMarkName = "FoodBankName";
                    Microsoft.Office.Interop.Word.Range wrdRange = oWordDoc.Bookmarks.get_Item(ref oBookMarkName).Range;
                    wrdRange.Text = foodBankName;

                    oBookMarkName = "MonthYear";
                    wrdRange = oWordDoc.Bookmarks.get_Item(ref oBookMarkName).Range;
                    wrdRange.Text = month.Substring(5) + "/" + month.Substring(0,4);

                    oBookMarkName = "rptDate";
                    wrdRange = oWordDoc.Bookmarks.get_Item(ref oBookMarkName).Range;
                    wrdRange.Text = rptDate;

                    Table table = oWordDoc.Tables[2];
                    table.Cell(2, 2).Range.Text = county;

                    table = oWordDoc.Tables[3];
                    table.Cell(5, 5).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.InfantsNew);
                    table.Cell(5, 7).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.InfantsReturning);
                    table.Cell(7, 5).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.YouthNew + clsMonthStats.TeensNew + clsMonthStats.EighteenNew);
                    table.Cell(7, 7).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.YouthReturning + clsMonthStats.TeensReturning + clsMonthStats.EighteenReturning);
                    table.Cell(9, 5).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.AdultsNew);
                    table.Cell(9, 7).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.AdultsReturning);
                    table.Cell(11, 5).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.SeniorsNew);
                    table.Cell(11, 7).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.SeniorsReturning);
                    table.Cell(13, 4).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.TotalFamilyNew);
                    table.Cell(13, 6).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.TotalFamilyReturning);
                    table.Cell(15, 3).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.HHTotalServedNew);
                    table.Cell(15, 5).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.HHTotalServedReturning);
                    table.Cell(18, 3).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.CntSpecialDietNew);
                    table.Cell(18, 5).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.CntSpecialDietReturning);
                    table.Cell(20, 3).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.HHRcvdSupplementalNew);
                    table.Cell(20, 5).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.HHRcvdSupplementalReturning);

                    table = oWordDoc.Tables[4];
                    table.Cell(1, 2).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.LbsStandard + clsMonthStats.LbsOther + clsMonthStats.LbsCommodity + clsMonthStats.LbsBabySvc);
                    table.Cell(3, 2).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.LbsSupplemental);

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
