using System;
using Microsoft.Office.Interop.Word;

namespace ClientcardFB3
{
    class PRNTFamilyCard
    {
        Client clsClient;
        Application oWord = new Application();
        Document oWordDoc = new Document();

        public PRNTFamilyCard(Client clientIn)
        {
            clsClient = clientIn;
        }

        public void createReport(string foodBankName, string templatePath, string clientType)
            //string[] fldNames, string[] fldVals)
        {
            Object oMissing = System.Reflection.Missing.Value;
            Object missing = System.Reflection.Missing.Value;
            Object oTrue = true;
            Object oFalse = false;
            oWord.Visible = true;
            Object oTemplatePath = templatePath;

            try
            {
                oWordDoc = oWord.Documents.Add(ref oTemplatePath, ref oMissing, ref oMissing, ref oMissing);

                fillBookMark("FoodBankName",foodBankName);
                fillBookMark("FBName1",foodBankName);
                fillBookMark("FBName2",foodBankName);
                fillBookMark("Date",DateTime.Today.ToShortDateString());
                fillBookMark("clientID",clsClient.clsHH.ID.ToString());

                Table table = oWordDoc.Tables[1];
                //table.Cell(1, 1).Range.Text = clsClient.clsHH.Name;
                string fullAddress = clsClient.clsHH.Address;
                if (clsClient.clsHH.AptNbr.Trim() != "")
                {
                    fullAddress += "  Unit " + clsClient.clsHH.AptNbr.Trim();
                }
                table.Cell(3, 1).Range.Text = fullAddress ;
                table.Cell(5, 1).Range.Text = clsClient.clsHH.City + ", " + clsClient.clsHH.State;
                table.Cell(5, 2).Range.Text = clsClient.clsHH.Zipcode;
                int row = 2;
                Table table2 = oWordDoc.Tables[2];
                for (int i = 0; i < clsClient.clsHHmem.RowCount; i++)
                {
                    clsClient.clsHHmem.SetRecord(i);
                    if (clsClient.clsHHmem.HeadHH == true)
                    {
                        if (clsClient.clsHHmem.UseAge == false)
                            table.Cell(7, 1).Range.Text = CCFBGlobal.ValidDateString(clsClient.clsHHmem.Birthdate);
                        else
                            table.Cell(7, 1).Range.Text = clsClient.clsHHmem.Age.ToString();

                        table.Cell(7, 2).Range.Text = clsClient.clsHHmem.Sex;
                        table.Cell(7, 3).Range.Text = clsClient.clsHH.Phone;
                        table.Cell(1, 3).Range.Text = clsClient.clsHHmem.LastName;
                        table.Cell(1, 1).Range.Text = clsClient.clsHHmem.FirstName;
                    }
                    else
                    {
                        if (clsClient.clsHHmem.Inactive == false)
                        {
                            table2.Cell(row, 1).Range.Text = clsClient.clsHHmem.LastName + ", " + clsClient.clsHHmem.FirstName;
                            if (clsClient.clsHHmem.UseAge == true)
                                table2.Cell(row, 2).Range.Text = clsClient.clsHHmem.Age.ToString();
                            else
                                table2.Cell(row, 2).Range.Text = CCFBGlobal.ValidDateString(clsClient.clsHHmem.Birthdate);

                            table2.Cell(row, 3).Range.Text = clsClient.clsHHmem.Sex;
                            if (table2.Columns.Count > 3)
                            {
                                if (clsClient.clsHHmem.IsDisabled == true)
                                { table2.Cell(row, 4).Range.Text = "X"; }
                                if (clsClient.clsHHmem.SpecialDiet == true)
                                { table2.Cell(row, 5).Range.Text = "X"; }
                            }
                            row++;
                        }
                    }
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
                
                //Dialog varDlg = oWord.Application.Dialogs[WdWordDialog.wdDialogFilePrint];
                //int userChoice = varDlg.Show(ref oMissing);
               
                ////If User did not cancel Print, then Print the document
                //if (userChoice == -1)
                //{
                CCFBGlobal.appendErrorToErrorReport(clsClient.clsHH.Name,"Print Clientcard");
                oWord.Options.PrintBackground = false;
                oWordDoc.PrintOut(ref oFalse, ref oFalse, ref oMissing,
                                  ref oMissing, ref oMissing, ref oMissing,
                                  ref oMissing, ref oMissing, ref oMissing, 
                                  ref oMissing, ref oMissing, ref oMissing, 
                                  ref oMissing, ref oMissing, ref oMissing, 
                                  ref oMissing, ref oMissing, ref oMissing);
                //}
                System.Windows.Forms.Application.DoEvents();
                System.Threading.Thread.Sleep(100);
                ((_Application)oWord).Quit(SaveChanges: false, OriginalFormat: false, RouteDocument: false);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oWord);

                //CCFBGlobal.openDocumentOutsideCCFB(saveAs.ToString());
            }
            catch (Exception ex)
            {
                CCFBGlobal.appendErrorToErrorReport("File Path = " + oTemplatePath.ToString(), ex.GetBaseException().ToString());
                ((_Application)oWord).Quit(SaveChanges: false, OriginalFormat: false, RouteDocument: false);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oWord);
            }
        }

        private void fillBookMark(String sBookMarkName, String sBookMarkText )
        {
            Object oBookMarkName;
            try
            {
                oBookMarkName = sBookMarkName;
                oWordDoc.Bookmarks.get_Item(ref oBookMarkName).Range.Text = sBookMarkText;
            }
            catch (Exception)
            {
            }
        }
    }
}
