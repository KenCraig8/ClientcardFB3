using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Office.Interop.Word;

namespace ClientcardFB3
{
    class RptNWHarvest
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

        public RptNWHarvest(TrxLogPeriodTotals statsTrxLog)
        {
            clsMonthStats = statsTrxLog;
        }

        public void createReport(string foodBankName, string month, string year, string county,
            string totHHServed, string preparedBy, object saveAs, string templatePath, string rptDate,
            string percentNWH)
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
                

                Object oBookMarkName = "FoodBankName";
                oWordDoc.Bookmarks.get_Item(ref oBookMarkName).Range.Text = foodBankName;

                oBookMarkName = "rptMonth";
                oWordDoc.Bookmarks.get_Item(ref oBookMarkName).Range.Text = month.Substring(5);

                oBookMarkName = "rptYear";
                oWordDoc.Bookmarks.get_Item(ref oBookMarkName).Range.Text = month.Substring(0,4);

                oBookMarkName = "County";
                oWordDoc.Bookmarks.get_Item(ref oBookMarkName).Range.Text = county;

                oBookMarkName = "rptDate";
                oWordDoc.Bookmarks.get_Item(ref oBookMarkName).Range.Text = rptDate;

                oBookMarkName = "ReportedBy";
                oWordDoc.Bookmarks.get_Item(ref oBookMarkName).Range.Text = preparedBy;

                Table table = oWordDoc.Tables[3];
                table.Cell(1, 3).Range.Text = CCFBGlobal.formatNumberWithCommas(totHHServed);
                table.Cell(2, 3).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.Infants);
                table.Cell(3, 3).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.Youth + clsMonthStats.Teens + clsMonthStats.Eighteen);
                table.Cell(4, 3).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.Adults);
                table.Cell(5, 3).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.Seniors);
                table.Cell(6, 3).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.TotalFamily);
                table.Cell(7, 2).Range.Text = percentNWH;
                /*************************************************************************************
                //STILL NEED % of support from NW Harvest
                **************************************************************************************/

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
