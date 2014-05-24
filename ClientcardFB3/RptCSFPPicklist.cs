using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;

namespace ClientcardFB3
{
    class RptCSFPPicklist
    {
        DataGridView dgv;
        string FBName = "";

        public RptCSFPPicklist(string foodbankname)
        {
            FBName = foodbankname;
        }

        public void createReport(string sYear, string sMonth,string templatePath, 
            DataGridView dgvIn, DateTime delieveryDate, string route)
        {
            string tmp = "";
            dgv = dgvIn;
            string savepath = CCFBGlobal.pathCSFP + sYear + @"\";
            CCFBGlobal.verifyPath(savepath);
            Object saveAs = savepath + sYear + sMonth + "-" + "CSFP PickList" + "-" + route + ".doc";

            CCFBGlobal.DeleteFile(saveAs.ToString());
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

                Table table = oWordDoc.Tables[1];
                table.Cell(1, 2).Range.Text = delieveryDate.ToShortDateString();
                table.Cell(2, 2).Range.Text = FBName;
                table.Cell(2, 4).Range.Text = route;
                //table.Cell(3, 2).Range.Text = driver;

                table = oWordDoc.Tables[2];
                if (CCFBPrefs.EnableCSFPShowRoutes == false)
                {
                    table.Cell(1, 5).Range.Text = "Previous Svc";
                }
                int row = 2;
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    if (row > table.Rows.Count)
                    {
                        table.Rows.Add(Type.Missing);
                    }
                    table.Cell(row, 1).Range.Text = (row-1).ToString() + ": " + dgv.Rows[i].Cells["HouseholdID"].Value.ToString();
                    table.Cell(row, 2).Range.Text = dgv.Rows[i].Cells["clmName"].Value.ToString();
                    tmp = dgv.Rows[i].Cells["Address"].Value.ToString();
                    if (dgv.Rows[i].Cells["AptNbr"].Value.ToString() != "")
                    {
                        tmp += " unit " + dgv.Rows[i].Cells["AptNbr"].Value.ToString();
                    }
                    table.Cell(row, 3).Range.Text = tmp;
                    table.Cell(row, 4).Range.Text = dgv.Rows[i].Cells["CSFPExpiration"].Value.ToString();
                    if (dgv.Rows[i].Cells["CSFPExpiration"].Style.BackColor == System.Drawing.Color.Yellow)
                    {
                        table.Cell(row, 4).Range.HighlightColorIndex = WdColorIndex.wdYellow;
                    }
                    else
                    {
                        table.Cell(row, 4).Range.HighlightColorIndex = WdColorIndex.wdNoHighlight;
                    }
                    table.Cell(row, 5).Range.Text = dgv.Rows[i].Cells["Route"].Value.ToString();
                    table.Cell(row, 6).Range.Text = dgv.Rows[i].Cells["clmDateServed"].Value.ToString();
                    table.Cell(row, 7).Range.Text = dgv.Rows[i].Cells["clmLbs"].Value.ToString();
                    row += 1;
                }
                oWordDoc.Save();
                //oWordDoc.SaveAs(ref saveAs, ref missing, ref missing, ref missing, ref missing,
                //    ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
                //    ref missing, ref missing, ref missing, ref missing, ref missing);

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
