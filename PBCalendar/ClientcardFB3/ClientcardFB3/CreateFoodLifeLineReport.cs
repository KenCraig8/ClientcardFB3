using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Office.Interop.Word;

namespace ClientcardFB3
{
    class CreateFoodLifeLineReport
    {
        System.Data.DataTable monthStatsTable;

        public CreateFoodLifeLineReport(System.Data.DataTable dataTableIn)
        {
            monthStatsTable = dataTableIn;
        }

        public void createReport(string foodBankName, string month, string year, string county,
            string totHHServed, string totIndServed, string preparedBy, string phoneNumer,
            string totLbs, object saveAs, string templatePath)
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
                table.Cell(1, 4).Range.Text = month + "/" + year;
                table.Cell(2, 2).Range.Text = county;
                table.Cell(3, 2).Range.Text = preparedBy;
                table.Cell(3, 4).Range.Text = phoneNumer;

                table = oWordDoc.Tables[2];
                table.Cell(1, 3).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[0]["NbrDaysOpen"]);
                table = oWordDoc.Tables[3];
                table.Cell(2, 3).Range.Text = CCFBGlobal.formatNumberWithCommas(totLbs);

                int returningInf = Convert.ToInt32(monthStatsTable.Rows[0]["ReturningFiscalInfants"]);
                int returningYth = Convert.ToInt32(monthStatsTable.Rows[0]["ReturningFiscalYouth"]);
                int returningTeens = Convert.ToInt32(monthStatsTable.Rows[0]["ReturningFiscalTeens"]);
                int returningAdults = Convert.ToInt32(monthStatsTable.Rows[0]["ReturningFiscalAdults"]);
                int returingSeniors = Convert.ToInt32(monthStatsTable.Rows[0]["ReturningFiscalSeniors"]);

                int newInf = Convert.ToInt32(monthStatsTable.Rows[0]["NewFiscalInfants"]);
                int newYth = Convert.ToInt32(monthStatsTable.Rows[0]["NewFiscalYouth"]);
                int newTeens = Convert.ToInt32(monthStatsTable.Rows[0]["NewFiscalTeens"]);
                int newAdults = Convert.ToInt32(monthStatsTable.Rows[0]["NewFiscalAdults"]);
                int newSeniors = Convert.ToInt32(monthStatsTable.Rows[0]["NewFiscalSeniors"]);

                table = oWordDoc.Tables[4];
                table.Cell(2, 3).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[0]["HHReturningFiscal"].ToString());
                table.Cell(2, 5).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[0]["HHNewFiscal"].ToString());
                table.Cell(2, 7).Range.Text = CCFBGlobal.formatNumberWithCommas((Convert.ToInt32(monthStatsTable.Rows[0]["HHReturningFiscal"]) +
                Convert.ToInt32(monthStatsTable.Rows[0]["HHNewFiscal"])).ToString());

                table.Cell(4, 3).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[0]["ReturningFiscalInfants"].ToString());
                table.Cell(4, 5).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[0]["NewFiscalInfants"].ToString());
                table.Cell(4, 7).Range.Text = CCFBGlobal.formatNumberWithCommas((returningInf + newInf));

                table.Cell(5, 3).Range.Text = CCFBGlobal.formatNumberWithCommas((returningYth + returningTeens));
                table.Cell(5, 5).Range.Text = CCFBGlobal.formatNumberWithCommas((newYth + newTeens));
                table.Cell(5, 7).Range.Text = CCFBGlobal.formatNumberWithCommas((returningYth + returningTeens +
                    newTeens + newYth));
               
                table.Cell(6, 3).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[0]["ReturningFiscalAdults"].ToString());
                table.Cell(6, 5).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[0]["NewFiscalAdults"].ToString());
                table.Cell(6, 7).Range.Text = CCFBGlobal.formatNumberWithCommas((returningAdults + newAdults));

                table.Cell(7, 3).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[0]["ReturningFiscalSeniors"].ToString());
                table.Cell(7, 5).Range.Text = CCFBGlobal.formatNumberWithCommas(monthStatsTable.Rows[0]["NewFiscalSeniors"].ToString());
                table.Cell(7, 7).Range.Text = (returingSeniors + newSeniors).ToString();

                table.Cell(8, 3).Range.Text = CCFBGlobal.formatNumberWithCommas((returningInf + returningYth + returningTeens
                    + returningAdults + returingSeniors));

                table.Cell(8, 5).Range.Text = CCFBGlobal.formatNumberWithCommas(
                    (newInf + newYth + newTeens + newAdults + newSeniors));
                table.Cell(8, 7).Range.Text = CCFBGlobal.formatNumberWithCommas(totIndServed);

                oWordDoc.SaveAs(ref saveAs, ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing);

                ((_Application)oWord).Quit(SaveChanges: false, OriginalFormat: false, RouteDocument: false);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oWord);

                CCFBGlobal.openDocumentOutsideCCFB(saveAs.ToString());
            }
            catch (Exception ex)
            {
                ((_Application)oWord).Quit(SaveChanges: false, OriginalFormat: false, RouteDocument: false);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oWord);
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString(), CCFBGlobal.serverName);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oWord);
            }
        }
    }
}
