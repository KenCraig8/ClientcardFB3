using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;

namespace ClientcardFB3
{
    class RptHDRouteSheet
    {

        public RptHDRouteSheet()
        {

        }

        public void createReport(object saveAs, string templatePath, HDRouteSheet clsRS)
        {
            Household clsHH = new Household(CCFBGlobal.connectionString);
            Object oMissing = System.Reflection.Missing.Value;
            Object missing = System.Reflection.Missing.Value;
            Object oTrue = true;
            Object oFalse = false;
            Microsoft.Office.Interop.Word.Application oWord = 
                new Microsoft.Office.Interop.Word.Application();
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
                oWordDoc.Bookmarks.get_Item(ref oBookMarkName).Range.Text = CCFBPrefs.FoodBankName;

                oBookMarkName = "RouteTitle";
                oWordDoc.Bookmarks.get_Item(ref oBookMarkName).Range.Text = clsRS.RouteTitle;

                oBookMarkName = "ServiceDate";
                oWordDoc.Bookmarks.get_Item(ref oBookMarkName).Range.Text = clsRS.TrxDate.ToShortDateString();

                oBookMarkName = "BagDescr";
                oWordDoc.Bookmarks.get_Item(ref oBookMarkName).Range.Text = clsRS.BagDescr;

                oBookMarkName = "FBContact";
                oWordDoc.Bookmarks.get_Item(ref oBookMarkName).Range.Text = clsRS.FBContact;

                oBookMarkName = "FBPhone";
                oWordDoc.Bookmarks.get_Item(ref oBookMarkName).Range.Text = clsRS.FBContactPhone;
/////////
                oBookMarkName = "DriverName";
                oWordDoc.Bookmarks.get_Item(ref oBookMarkName).Range.Text = clsRS.DriverName;

                oBookMarkName = "RouteNote";
                oWordDoc.Bookmarks.get_Item(ref oBookMarkName).Range.Text = clsRS.Notes;

                oBookMarkName = "DriverPhone";
                oWordDoc.Bookmarks.get_Item(ref oBookMarkName).Range.Text = clsRS.DriverPhone;

                oBookMarkName = "RouteTime";
                oWordDoc.Bookmarks.get_Item(ref oBookMarkName).Range.Text = clsRS.NbrHours.ToString("F");

                oBookMarkName = "RouteMileage";
                oWordDoc.Bookmarks.get_Item(ref oBookMarkName).Range.Text = clsRS.ActMiles.ToString("F");


                Table table = oWordDoc.Tables[2];
                int row = 2;
                for (int i = 0; i < clsRS.RSClients.RowCount; i++)
                {
                    if (table.Rows.Count <= i)
                    {
                        table.Rows.Add(Type.Missing);
                    }
                    clsRS.RSClients.setDataRow(i);
                    clsHH.open(clsRS.RSClients.HHID);
                    table.Cell(row, 1).Range.Text = clsHH.Name;
                    table.Cell(row, 2).Range.Text = clsHH.Address + " unit " + clsHH.AptNbr;
                    table.Cell(row, 3).Range.Text = clsHH.Phone;
                    table.Cell(row, 4).Range.Text = clsRS.RSClients.HDFamilySize(clsRS.RSClients.HHID);
                    table.Cell(row, 5).Range.Text = clsRS.RSClients.ClientComments;
                    table.Cell(row, 6).Range.Text = clsRS.RSClients.DriverNotes;
                    if (clsRS.RSClients.Status == 5)
                        table.Cell(row, 7).Range.Text = "XXX";
                    row += 1;
                }

                oWordDoc.SaveAs(ref saveAs, ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing);

                ((_Application)oWord).Quit(SaveChanges: false, OriginalFormat: false, RouteDocument: false);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oWord);

                CCFBGlobal.openDocumentOutsideCCFB(saveAs.ToString());
                clsRS.updateRouteStatus(HDRouteSheet.HDRSStatus.Printed);
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
