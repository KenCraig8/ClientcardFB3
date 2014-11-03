using System;
using Microsoft.Office.Interop.Word;

namespace ClientcardFB3
{
    class CreateClientCard
    {
        Client clsClient;

        public CreateClientCard(Client clientIn)
        {
            clsClient = clientIn;
        }

        public void createReport(string foodBankName, string templatePath, string clientType,
            string[] fldNames, string[] fldVals)
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

                Object oBookMarkName = "FoodBankName";
                oWordDoc.Bookmarks.get_Item(ref oBookMarkName).Range.Text = foodBankName;

                oBookMarkName = "Date";
                oWordDoc.Bookmarks.get_Item(ref oBookMarkName).Range.Text = DateTime.Today.ToShortDateString();

                oBookMarkName = "clientID";
                oWordDoc.Bookmarks.get_Item(ref oBookMarkName).Range.Text = clsClient.clsHH.ID.ToString();
          
                Table table = oWordDoc.Tables[1];
                //table.Cell(1, 1).Range.Text = clsClient.clsHH.Name;
                table.Cell(3, 1).Range.Text = clsClient.clsHH.Address;
                table.Cell(5, 1).Range.Text = clsClient.clsHH.City + ", " + clsClient.clsHH.State;
                table.Cell(5, 2).Range.Text = clsClient.clsHH.Zipcode;
                int row = 2;
                Table table2 = oWordDoc.Tables[2];
                for (int i = 0; i < clsClient.clsHHmem.RowCount; i++)
                {
                    clsClient.clsHHmem.SetRecord(i);
                    if (clsClient.clsHHmem.HeadHH == true)
                    {
                        if(clsClient.clsHHmem.UseAge == false)
                        table.Cell(7, 1).Range.Text = clsClient.clsHHmem.Birthdate.ToShortDateString();
                        else
                            table.Cell(7, 1).Range.Text = clsClient.clsHHmem.Age.ToString();

                        table.Cell(7, 2).Range.Text = clsClient.clsHHmem.Sex;
                        table.Cell(7, 3).Range.Text = clsClient.clsHH.Phone;
                        table.Cell(1, 1).Range.Text = clsClient.clsHHmem.LastName;
                        table.Cell(1, 3).Range.Text = clsClient.clsHHmem.FirstName;
                    }
                    table2.Cell(row, 1).Range.Text = clsClient.clsHHmem.LastName + ", " + clsClient.clsHHmem.FirstName;
                    if (clsClient.clsHHmem.UseAge == true)
                        table2.Cell(row, 2).Range.Text = clsClient.clsHHmem.Age.ToString();
                    else
                        table2.Cell(row, 2).Range.Text = clsClient.clsHHmem.Birthdate.ToShortDateString();

                    table2.Cell(row, 3).Range.Text = clsClient.clsHHmem.Sex;
                    row++;
                }

                //table = oWordDoc.Tables[4];
                //table.Cell(1, 3).Range.Text = clsClient.clsHH.Infants.ToString();
                //table.Cell(2, 3).Range.Text = clsClient.clsHH.Youth.ToString();
                //table.Cell(3, 3).Range.Text = clsClient.clsHH.Teens.ToString();
                //table.Cell(4, 3).Range.Text = clsClient.clsHH.Adults.ToString();
                //table.Cell(5, 3).Range.Text = clsClient.clsHH.Seniors.ToString();

                //row = 1;
                //for (int i = 0; i < fldNames.Length; i++)
                //{
                //    if (fldNames[i] != null)
                //    {
                //        table.Cell(row, 4).Range.Text = fldNames[i];
                        
                //        if (fldVals[i] == "True")
                //            table.Cell(row, 5).Range.Text = "Yes";
                //        else
                //            table.Cell(row, 5).Range.Text = "No";

                //        row++;
                //    }
                //}
                
                Dialog varDlg = oWord.Application.Dialogs[WdWordDialog.wdDialogFilePrint];
                int userChoice = varDlg.Show(ref oMissing);
               
                //If User did not cancel Print, then Print the document
                if (userChoice == -1)
                {
                    oWordDoc.PrintOut(ref oTrue, ref oFalse, ref oMissing,
                            ref oMissing, ref oMissing, ref oMissing,
                            ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                            ref oMissing, ref oMissing, ref oMissing,
                            ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                            ref oMissing);
                }

                //((_Application)oWord).Quit(SaveChanges: false, OriginalFormat: false, RouteDocument: false);
                //System.Runtime.InteropServices.Marshal.ReleaseComObject(oWord);

                //CCFBGlobal.openDocumentOutsideCCFB(saveAs.ToString());
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
