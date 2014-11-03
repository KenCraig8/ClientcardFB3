using System;
using System.Data;
using System.IO;
using Microsoft.Office.Interop.Word;

namespace ClientcardFB3
{
    class CreateCoalitionReport
    {
        System.Data.DataTable monthStatsTable;
        public CreateCoalitionReport(System.Data.DataTable dataTableIn)
        {
            monthStatsTable = dataTableIn;
        }

        public void createReport(string foodBankName, string reportMonth, string periodTotalsIn,
            string fiscalTotalsIn, string HHPerTotIn, string HHFiscalTotIn, string lbsPerTotServedIn, string lbsCumTotServedIn,
            string preparedBy, object saveAs, string templatePath, string imagePath)
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
            oWord.Visible = true;
            Object oTemplatePath = templatePath;
            try
            {

                oWordDoc = oWord.Documents.Add(ref oTemplatePath, ref oMissing, ref oMissing, ref oMissing);

                //Save so that the template is free to be used by the next user
                oWordDoc.SaveAs(ref saveAs, ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing);

                Object oLinkToFile = false;  //default
                Object oSaveWithDocument = true;//default

                string[] files = Directory.GetFiles(imagePath);
                string sBookMark = "";
                foreach (string file in files)
                {
                    FileInfo fi = new FileInfo(file);
                    sBookMark = fi.Name.Substring(0, fi.Name.Length - 4); 
                    Object oBookMarkName = sBookMark;
                    try
                    {
                        oWordDoc.Bookmarks.get_Item(ref oBookMarkName).Range.InlineShapes.AddPicture(file
                            , ref  oLinkToFile, ref  oSaveWithDocument, ref  oMissing);
                    }
                    catch (Exception ex)
                    {
                        CCFBGlobal.appendErrorToErrorReport("BookMark = " + sBookMark, ex.GetBaseException().ToString());
                    }
                }

                oWordDoc.SaveAs(ref saveAs, ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing);

                ((_Application)oWord).Quit(SaveChanges: false, OriginalFormat: false, RouteDocument: false);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oWord);

                CCFBGlobal.openDocumentOutsideCCFB(saveAs.ToString());
            }
            catch (Exception ex)
            {
                CCFBGlobal.appendErrorToErrorReport("File Path = " + oTemplatePath.ToString(), ex.GetBaseException().ToString());
                ((_Application)oWord).Quit(SaveChanges: false, OriginalFormat: false, RouteDocument: false);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oWord);
            }
        }
    }
}
