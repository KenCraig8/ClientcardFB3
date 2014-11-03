using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Office.Interop.Word;

namespace ClientcardFB3
{
    class CreateNWHarvestReport
    {
        System.Data.DataTable monthStatsTable;

        public CreateNWHarvestReport(System.Data.DataTable dataTableIn)
        {
            monthStatsTable = dataTableIn;
        }

        public void createReport(string foodBankName, string month, string year, string county,
            string totHHServed, string totIndServed, string preparedBy, object saveAs, string templatePath)
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

                Object oBookMarkName = "FoodBankName";
                oWordDoc.Bookmarks.get_Item(ref oBookMarkName).Range.Text = foodBankName;

                oBookMarkName = "rptMonth";
                oWordDoc.Bookmarks.get_Item(ref oBookMarkName).Range.Text = month;

                oBookMarkName = "rptYear";
                oWordDoc.Bookmarks.get_Item(ref oBookMarkName).Range.Text = year;

                oBookMarkName = "County";
                oWordDoc.Bookmarks.get_Item(ref oBookMarkName).Range.Text = county;

                oBookMarkName = "rptDate";
                oWordDoc.Bookmarks.get_Item(ref oBookMarkName).Range.Text = DateTime.Today.ToShortDateString();

                oBookMarkName = "ReportedBy";
                oWordDoc.Bookmarks.get_Item(ref oBookMarkName).Range.Text = preparedBy;

                Table table = oWordDoc.Tables[3];
                table.Cell(1, 3).Range.Text = CCFBGlobal.formatNumberWithCommas(totHHServed);
                table.Cell(2, 3).Range.Text = CCFBGlobal.formatNumberWithCommas(
                    (Convert.ToInt32(monthStatsTable.Rows[0]["ReturningFiscalInfants"]) +
                    Convert.ToInt32(monthStatsTable.Rows[0]["NewFiscalInfants"])));
                table.Cell(3, 3).Range.Text = CCFBGlobal.formatNumberWithCommas(
                    (Convert.ToInt32(monthStatsTable.Rows[0]["ReturningFiscalYouth"]) +
                    Convert.ToInt32(monthStatsTable.Rows[0]["NewFiscalYouth"]) +
                    Convert.ToInt32(monthStatsTable.Rows[0]["ReturningFiscalTeens"]) +
                    Convert.ToInt32(monthStatsTable.Rows[0]["NewFiscalTeens"])));
                table.Cell(4, 3).Range.Text = CCFBGlobal.formatNumberWithCommas(
                    (Convert.ToInt32(monthStatsTable.Rows[0]["ReturningFiscalAdults"]) +
                    Convert.ToInt32(monthStatsTable.Rows[0]["NewFiscalAdults"])));
                table.Cell(5, 3).Range.Text = CCFBGlobal.formatNumberWithCommas(
                    (Convert.ToInt32(monthStatsTable.Rows[0]["ReturningFiscalSeniors"]) +
                    Convert.ToInt32(monthStatsTable.Rows[0]["NewFiscalSeniors"])));
                table.Cell(6, 3).Range.Text = totIndServed;

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
                        ex.GetBaseException().ToString(), CCFBGlobal.serverName);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oWord);
            }
        }
    }
}
