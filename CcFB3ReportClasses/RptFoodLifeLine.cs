using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Office.Interop.Word;

namespace ClientcardFB3
{
    class RptFoodLifeLine : IDisposable
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

        public RptFoodLifeLine(TrxLogPeriodTotals dataTableIn)
        {
            clsMonthStats = dataTableIn;
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

        public void createReport(string foodBankName, string month, string year, string county,
            string preparedBy, string phoneNumer,
            string totLbs, object saveAs, string templatePath, bool autoPrint)
        {
            Object oMissing = System.Reflection.Missing.Value;
            Object missing = System.Reflection.Missing.Value;
            Object oTrue = true;
            Object oFalse = false;
            Application oWord = new Application();
            Document oWordDoc = new Document();
            oWord.Visible = false;

            try
            {
                Object oTemplatePath = templatePath;

                oWordDoc = oWord.Documents.Add(ref oTemplatePath, ref oMissing, ref oMissing, ref oMissing);
               
                //Save so that the template is free to be used by the next user
                oWordDoc.SaveAs(ref saveAs, ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing);

                Table table = oWordDoc.Tables[1];
                table.Cell(1, 2).Range.Text = foodBankName;
                table.Cell(1, 4).Range.Text = month.Substring(5) + " / " + month.Substring(0,4);
                table.Cell(2, 2).Range.Text = county;
                table.Cell(3, 2).Range.Text = preparedBy;
                table.Cell(3, 4).Range.Text = phoneNumer;

                table = oWordDoc.Tables[2];
                table.Cell(1, 3).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.NbrDaysOpen);
                table = oWordDoc.Tables[3];
                table.Cell(2, 3).Range.Text = CCFBGlobal.formatNumberWithCommas(totLbs);

                int returningInf = clsMonthStats.InfantsReturning;
                int returningYth = clsMonthStats.YouthReturning;
                int returningTeens = clsMonthStats.TeensReturning;
                int retuningEighteens = clsMonthStats.EighteenReturning;
                int returningAdults = clsMonthStats.AdultsReturning;
                int returingSeniors = clsMonthStats.SeniorsReturning;

                int newInf = clsMonthStats.InfantsNew;
                int newYth = clsMonthStats.YouthNew;
                int newTeens = clsMonthStats.TeensNew;
                int newEighteens = clsMonthStats.EighteenNew;
                int newAdults = clsMonthStats.AdultsNew;
                int newSeniors = clsMonthStats.SeniorsNew;

                table = oWordDoc.Tables[4];
                table.Cell(2, 3).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.HHTotalServedReturning);
                table.Cell(2, 5).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.HHTotalServedNew);
                table.Cell(2, 7).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.HHTotalServedNew + clsMonthStats.HHTotalServedReturning);

                table.Cell(4, 3).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.InfantsReturning);
                table.Cell(4, 5).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.InfantsNew);
                table.Cell(4, 7).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.InfantsNew + clsMonthStats.InfantsReturning);

                table.Cell(5, 3).Range.Text = CCFBGlobal.formatNumberWithCommas((returningYth + returningTeens + retuningEighteens));
                table.Cell(5, 5).Range.Text = CCFBGlobal.formatNumberWithCommas((newYth + newTeens + newEighteens));
                table.Cell(5, 7).Range.Text = CCFBGlobal.formatNumberWithCommas((returningYth + returningTeens + retuningEighteens +
                    newTeens + newYth + newEighteens));
               
                table.Cell(6, 3).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.AdultsReturning);
                table.Cell(6, 5).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.AdultsNew);
                table.Cell(6, 7).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.AdultsNew + clsMonthStats.AdultsReturning);

                table.Cell(7, 3).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.SeniorsReturning);
                table.Cell(7, 5).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.SeniorsNew);
                table.Cell(7, 7).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.SeniorsNew + clsMonthStats.SeniorsReturning);

                table.Cell(8, 3).Range.Text = CCFBGlobal.formatNumberWithCommas((returningInf + returningYth + returningTeens + retuningEighteens
                    + returningAdults + returingSeniors));

                table.Cell(8, 5).Range.Text = CCFBGlobal.formatNumberWithCommas(
                    (newInf + newYth + newTeens + newEighteens + newAdults + newSeniors));
                table.Cell(8, 7).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.TotalFamily);

                oWordDoc.SaveAs(ref saveAs, ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing);

                if (autoPrint == true)
                    oWordDoc.PrintOut(oFalse, oFalse, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing,
                        oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing);

                ((_Application)oWord).Quit(SaveChanges: false, OriginalFormat: false, RouteDocument: false);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oWord);

                if (!autoPrint)
                    CCFBGlobal.openDocumentOutsideCCFB(saveAs.ToString());
            }
            catch (Exception ex)
            {
                ((_Application)oWord).Quit(SaveChanges: false, OriginalFormat: false, RouteDocument: false);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oWord);
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString());
                error = true;
            }
        }
    }
}
