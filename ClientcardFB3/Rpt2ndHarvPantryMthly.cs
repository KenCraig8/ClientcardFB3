using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Office.Interop.Word;

namespace ClientcardFB3
{
    class Rpt2ndHarvPantryMthly
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

        public Rpt2ndHarvPantryMthly(TrxLogPeriodTotals clsStatsIn)
        {
            clsMonthStats = clsStatsIn;
        }

        public void createReport(string FBName, string month, string year
            , string county, string city, string phoneNumber, string preparedBy, object saveAs, string templatePath)
        {
            Object oMissing = System.Reflection.Missing.Value;

            object missing = System.Reflection.Missing.Value;
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

                Table table = oWordDoc.Tables[1];
                table.Cell(1, 2).Range.Text = month.Substring(5) + " / " + month.Substring(0,4);
                table.Cell(2, 2).Range.Text = FBName;
                table.Cell(3, 2).Range.Text = city + "/" + county;
                table.Cell(4, 2).Range.Text = preparedBy;
                table.Cell(4, 4).Range.Text = phoneNumber;

                table = oWordDoc.Tables[2];
                table.Cell(1, 2).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.Infants + clsMonthStats.Youth + clsMonthStats.Teens + clsMonthStats.Eighteen);
                table.Cell(2, 2).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.Adults);
                table.Cell(3, 2).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.Seniors);
                table.Cell(4, 2).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.TotalFamily);

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
