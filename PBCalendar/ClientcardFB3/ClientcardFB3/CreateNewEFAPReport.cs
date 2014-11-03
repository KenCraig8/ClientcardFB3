using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Office.Interop.Word;

namespace ClientcardFB3
{
    class CreateNewEFAPReport
    {
        System.Data.DataTable monthStatsTable;

        public CreateNewEFAPReport(System.Data.DataTable dataTableIn)
        {
            monthStatsTable = dataTableIn;
        }

        public void createReport(string foodBankName, string monthYear, string county,
            string preparedBy, object saveAs, string templatePath)
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
                    wrdRange.Text = monthYear;

                    oBookMarkName = "rptDate";
                    wrdRange = oWordDoc.Bookmarks.get_Item(ref oBookMarkName).Range;
                    wrdRange.Text = DateTime.Now.ToShortDateString();

                    Table table = oWordDoc.Tables[2];
                    table.Cell(2, 2).Range.Text = county;

                    table = oWordDoc.Tables[3];
                    table.Cell(5, 5).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[0]["NewFiscalInfants"].ToString());
                    table.Cell(5, 7).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[0]["ReturningFiscalInfants"].ToString());
                    table.Cell(7, 5).Range.Text = CCFBGlobal.formatNumberWithCommas(
                        Convert.ToInt32(monthStatsTable.Rows[0]["NewFiscalYouth"]) + 
                        Convert.ToInt32(monthStatsTable.Rows[0]["NewFiscalTeens"]));
                    table.Cell(7, 7).Range.Text = CCFBGlobal.formatNumberWithCommas(
                        Convert.ToInt32(monthStatsTable.Rows[0]["ReturningFiscalYouth"]) +
                        Convert.ToInt32(monthStatsTable.Rows[0]["ReturningFiscalTeens"]));
                    table.Cell(9, 5).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[0]["NewFiscalAdults"].ToString());
                    table.Cell(9, 7).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[0]["ReturningFiscalAdults"].ToString());
                    table.Cell(11, 5).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[0]["NewFiscalSeniors"].ToString());
                    table.Cell(11, 7).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[0]["ReturningFiscalSeniors"].ToString());
                    table.Cell(13, 4).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[0]["NewFiscalTotalFamily"].ToString());
                    table.Cell(13, 6).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[0]["ReturningFiscalTotalFamily"].ToString());
                    table.Cell(15, 3).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[0]["HHNewFiscal"].ToString());
                    table.Cell(15, 5).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[0]["HHReturningFiscal"].ToString());
                    table.Cell(18, 3).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[0]["NewFiscalSpecialDiet"].ToString());
                    table.Cell(18, 5).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[0]["ReturningFiscalSpecialDiet"].ToString());

                    table = oWordDoc.Tables[4];
                    table.Cell(1, 2).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[0]["LbsStandard"].ToString());
                    table.Cell(3, 2).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[0]["LbsSupplemental"].ToString());

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
