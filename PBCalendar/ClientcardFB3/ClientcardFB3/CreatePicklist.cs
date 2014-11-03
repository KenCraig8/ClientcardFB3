using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;

namespace ClientcardFB3
{
    class CreatePicklist
    {
        DataGridView dgv;

        public CreatePicklist(DataGridView dgvIn)
        {
            dgv = dgvIn;
        }

        public void createReport(object saveAs, string templatePath, 
            string foodBankName, DateTime delieveryDate, string route)
        {
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

                Object oBookMarkName = "FBName";
                oWordDoc.Bookmarks.get_Item(ref oBookMarkName).Range.Text = foodBankName;

                oBookMarkName = "Date";
                oWordDoc.Bookmarks.get_Item(ref oBookMarkName).Range.Text = delieveryDate.ToShortDateString();

                oBookMarkName = "Routes";
                oWordDoc.Bookmarks.get_Item(ref oBookMarkName).Range.Text = route;

                Table table = oWordDoc.Tables[2];
                int row = 2;
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    table.Rows.Add(Type.Missing);
                    table.Cell(row, 1).Range.Text = dgv.Rows[i].Cells["clmHHID"].Value.ToString();
                    table.Cell(row, 2).Range.Text = dgv.Rows[i].Cells["clmName"].Value.ToString();
                    table.Cell(row, 3).Range.Text = dgv.Rows[i].Cells["clmAddress"].Value.ToString();
                    table.Cell(row, 4).Range.Text = dgv.Rows[i].Cells["clmExpiration"].Value.ToString();
                    table.Cell(row, 5).Range.Text = dgv.Rows[i].Cells["clmMethod"].Value.ToString();
                    table.Cell(row, 6).Range.Text = dgv.Rows[i].Cells["clmDateServed"].Value.ToString();
                    table.Cell(row, 7).Range.Text = dgv.Rows[i].Cells["clmLbs"].Value.ToString();
                    row += 1;
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
