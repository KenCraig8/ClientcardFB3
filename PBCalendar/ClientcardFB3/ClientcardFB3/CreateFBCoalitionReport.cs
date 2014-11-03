using System;
using System.Data;
using Microsoft.Office.Interop.Word;

namespace ClientcardFB3
{
    class CreateFBCoalitionReport
    {
        System.Data.DataTable monthStatsTable;
        public CreateFBCoalitionReport(System.Data.DataTable dataTableIn)
        {
            monthStatsTable = dataTableIn;
        }

        public void createReport(string foodBankName, string reportMonth, string periodTotalsIn, 
            string fiscalTotalsIn, string HHPerTotIn, string HHFiscalTotIn, string lbsPerTotServedIn, string lbsCumTotServedIn,
            string preparedBy, object saveAs, string templatePath)
        {
            string fiscalTotals = fiscalTotalsIn.Replace(",", "");
            string periodTotals = periodTotalsIn.Replace(",", "");
            string HHFiscalTot = HHFiscalTotIn.Replace(",", "");
            string HHPerTot = HHPerTotIn.Replace(",", "");
            string lbsPerTotServed = lbsPerTotServedIn.Replace(",", "");
            string lbsCumTotServed = lbsCumTotServedIn.Replace(",", "");
            
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

                Object oBookMarkName = "FBName";
                oWordDoc.Bookmarks.get_Item(ref oBookMarkName).Range.Text = foodBankName;

                oBookMarkName = "rptMonth";
                oWordDoc.Bookmarks.get_Item(ref oBookMarkName).Range.Text = reportMonth;

                oBookMarkName = "rptDate";
                oWordDoc.Bookmarks.get_Item(ref oBookMarkName).Range.Text = DateTime.Now.ToShortDateString();

                Table table = oWordDoc.Tables[2];
                table.Cell(2, 3).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[0]["ReturningFiscalInfants"]);
                table.Cell(2, 5).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[0]["NewFiscalInfants"]);
                table.Cell(2, 7).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[1]["ReturningFiscalInfants"]);
                table.Cell(2, 9).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[1]["NewFiscalInfants"]);
                table.Cell(3, 3).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[0]["ReturningFiscalYouth"]);
                table.Cell(3, 5).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[0]["NewFiscalYouth"]);
                table.Cell(3, 7).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[1]["ReturningFiscalInfants"]);
                table.Cell(3, 9).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[1]["NewFiscalYouth"]);
                table.Cell(4, 3).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[0]["ReturningFiscalTeens"]);
                table.Cell(4, 5).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[0]["NewFiscalTeens"]);
                table.Cell(4, 7).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[1]["ReturningFiscalTeens"]);
                table.Cell(4, 9).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[1]["NewFiscalTeens"]);
                table.Cell(5, 3).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[0]["ReturningFiscalAdults"]);
                table.Cell(5, 5).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[0]["NewFiscalAdults"]);
                table.Cell(5, 7).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[1]["ReturningFiscalAdults"]);
                table.Cell(5, 9).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[1]["NewFiscalAdults"]);
                table.Cell(6, 3).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[0]["ReturningFiscalSeniors"]);
                table.Cell(6, 5).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[0]["NewFiscalSeniors"]);
                table.Cell(6, 7).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[1]["ReturningFiscalSeniors"]);
                table.Cell(6, 9).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[1]["NewFiscalSeniors"]);
                table.Cell(7, 3).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[0]["ReturningFiscalTotalFamily"]);
                table.Cell(7, 5).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[0]["NewFiscalTotalFamily"]);
                table.Cell(7, 7).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[1]["ReturningFiscalTotalFamily"]);
                table.Cell(7, 9).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[1]["NewFiscalTotalFamily"]);
                table.Cell(7, 11).Range.Text = CCFBGlobal.formatNumberWithCommas((int.Parse(periodTotals) + int.Parse(fiscalTotals)));

                table = oWordDoc.Tables[3];
                table.Cell(2, 3).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[0]["NewFiscalDisabled"]);
                table.Cell(2, 5).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[0]["ReturningFiscalDisabled"]);
                table.Cell(2, 7).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[1]["NewFiscalDisabled"]);
                table.Cell(2, 9).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[1]["ReturningFiscalDisabled"]);
                table.Cell(3, 3).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[0]["NewFiscalSpecialDiet"]);
                table.Cell(3, 5).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[0]["ReturningFiscalSpecialDiet"]);
                table.Cell(3, 7).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[1]["NewFiscalSpecialDiet"]);
                table.Cell(3, 9).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[1]["ReturningFiscalSpecialDiet"]);
                table.Cell(2, 11).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[1]["Disabled"]);
                table.Cell(3, 11).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[1]["SpecialDiet"]);

                table = oWordDoc.Tables[4];
                table.Cell(2, 3).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[0]["HHNewFiscal"]);
                table.Cell(2, 5).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[0]["HHReturningFiscal"]);
                table.Cell(2, 7).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[1]["HHNewFiscal"]);
                table.Cell(2, 9).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[1]["HHReturningFiscal"]);
                table.Cell(2, 11).Range.Text = CCFBGlobal.formatNumberWithCommas((int.Parse(HHPerTot) + int.Parse(HHFiscalTot)));
                table.Cell(3, 3).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[0]["NewFiscalHHHomeless"]);
                table.Cell(3, 5).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[0]["ReturningFiscalHHHomeless"]);
                table.Cell(3, 7).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[1]["NewFiscalHHHomeless"]);
                table.Cell(3, 9).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[1]["ReturningFiscalHHHomeless"]);
                table.Cell(3, 11).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[1]["HHHomeless"]);
                table.Cell(4, 3).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[0]["NewFiscalTransient"]);
                table.Cell(4, 5).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[0]["ReturningFiscalTransient"]);
                table.Cell(4, 7).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[1]["NewFiscalTransient"]);
                table.Cell(4, 9).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[1]["ReturningFiscalTransient"]);
                table.Cell(4, 11).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[1]["AllTransient"]);
                table.Cell(5, 3).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[0]["SingleNewFiscal"]);
                table.Cell(5, 5).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[0]["ReturningSingleFiscal"]);
                table.Cell(5, 7).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[1]["SingleNewFiscal"]);
                table.Cell(5, 9).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[1]["ReturningSingleFiscal"]);
                table.Cell(5, 11).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[1]["AllSingle"]);

                table = oWordDoc.Tables[5];
                table.Cell(2, 3).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[0]["LbsStandard"]);
                table.Cell(3, 3).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[0]["LbsOther"]);
                table.Cell(4, 3).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[0]["LbsCommodity"]);
                table.Cell(5, 3).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[0]["LbsSupplemental"]);
                table.Cell(6, 3).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[0]["LbsBabySvc"]);
                table.Cell(7, 3).Range.Text = CCFBGlobal.formatNumberWithCommas(int.Parse(lbsPerTotServed));
                table.Cell(8, 3).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[0]["LbsNonFood"]);
                table.Cell(2, 5).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[1]["LbsStandard"]);
                table.Cell(3, 5).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[1]["LbsOther"]);
                table.Cell(4, 5).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[1]["LbsCommodity"]);
                table.Cell(5, 5).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[1]["LbsSupplemental"]);
                table.Cell(6, 5).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[1]["LbsBabySvc"]);
                table.Cell(7, 5).Range.Text = CCFBGlobal.formatNumberWithCommas(int.Parse(lbsCumTotServed));
                table.Cell(8, 5).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[1]["LbsNonFood"]);

                table = oWordDoc.Tables[6];
                table.Cell(2, 5).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[0]["NbrVolunteers"]);
                table.Cell(3, 5).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[0]["NbrVolHours"]);

                //*********************NEED TO PUT THIS BACK IN ONCE KEN GETS LBSINKIND WORKING******************************
                //table = oWordDoc.Tables[7];
                //table.Cell(3, 5).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[0]["LbsInkind"]);

                table = oWordDoc.Tables[8];
                table.Cell(1, 2).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[0]["NbrDaysOpen"]);

                oWordDoc.SaveAs(ref saveAs, ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing);

                ((_Application)oWord).Quit(SaveChanges: false, OriginalFormat: false, RouteDocument: false);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oWord);

                CCFBGlobal.openDocumentOutsideCCFB(saveAs.ToString());
            }
            catch(Exception ex)
            {
                CCFBGlobal.appendErrorToErrorReport("File Path = " + oTemplatePath.ToString(), ex.GetBaseException().ToString());
                ((_Application)oWord).Quit(SaveChanges: false, OriginalFormat: false, RouteDocument: false);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oWord);
            }
        }
    }
}
