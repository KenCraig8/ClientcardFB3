using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;

namespace ClientcardFB3
{
    class CreateGroceryRescueReport
    {
        DataGridView dgv;

        public CreateGroceryRescueReport(DataGridView dgvIn)
        {
            dgv = dgvIn;
        }

        public void createReport(object saveAs, string templatePath, string DonorName)
        {
            Object oMissing = System.Reflection.Missing.Value;
            Object missing = System.Reflection.Missing.Value;
            Object oTrue = true;
            Object oFalse = false;
            Microsoft.Office.Interop.Word.Application oWord = 
                new Microsoft.Office.Interop.Word.Application();
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

                Object oBookMarkName = "date";
                oWordDoc.Bookmarks.get_Item(ref oBookMarkName).Range.Text = DateTime.Now.ToString("MMMM") + " - " + DateTime.Now.Year;

                Table table = oWordDoc.Tables[1];
                table.Cell(1, 3).Range.Text = CCFBPrefs.FoodBankName;
                table.Cell(1, 5).Range.Text = CCFBPrefs.EmailAddress;
                table.Cell(2, 3).Range.Text = CCFBPrefs.PreparedBy;
                table.Cell(2, 5).Range.Text = CCFBPrefs.PhoneNumber;
                table.Cell(3, 3).Range.Text = DonorName;
                table.Cell(3, 5).Range.Text = CCFBPrefs.FaxNumber;

                table = oWordDoc.Tables[2];
                int row = 3;
                int col = 1;
                for (int i = 2; col < dgv.Columns.Count; i++)
                {
                    table.Columns.Add(Type.Missing);
                    table.Cell(2, i).Range.Text = dgv.Columns[col].HeaderText.Replace(" ", "");
                    col++;
                }

                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    for (int j = 1; j <= dgv.Columns.Count; j++)
                    {
                        if(dgv.Rows[i].Cells[j-1].Value != null)
                            table.Cell(row, j).Range.Text = dgv.Rows[i].Cells[j-1].Value.ToString();
                    }
                    row++;
                    table.Rows.Add(Type.Missing);
                }
                table.Columns.AutoFit();
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
