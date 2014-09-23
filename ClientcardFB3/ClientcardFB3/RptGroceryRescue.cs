using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using Microsoft.Office.Interop.Word;
using Microsoft.Office.Interop.Excel;
using System.Drawing;

namespace ClientcardFB3
{
    class RptGroceryRescue
    {
        DataGridView dgv;
        public RptGroceryRescue(DataGridView dgvIn)
        {
            dgv = dgvIn;
        }

        public void createReport(object saveAs, string templatePath, string DonorName, bool isGrouceryRescue)
        {
             Microsoft.Office.Interop.Excel.Application m_oExcelApp;
            //Workbooks m_oBooks;
            _Workbook m_oBook;
            _Worksheet m_oSheet;

            m_oExcelApp = new Microsoft.Office.Interop.Excel.Application();
            m_oExcelApp.Visible = true;
            //m_oBooks = m_oExcelApp.Workbooks;
            m_oSheet = null;
            // m_oBooks = null;
            object missing = Type.Missing;

            Object oTemplatePath = templatePath;

            try
            {
                m_oBook = m_oExcelApp.Workbooks.Add(oTemplatePath);
                m_oSheet = (_Worksheet)m_oBook.ActiveSheet;
                
                string tempLogo = "";
                
                if(isGrouceryRescue == true)
                 tempLogo = CCFBGlobal.fb3TemplatesPath + "LifeLineLogo.bmp";
                else
                    tempLogo = CCFBGlobal.fb3TemplatesPath + "FBLogo.bmp";

                m_oSheet.Shapes.AddPicture(tempLogo, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoTrue,
                    300, 10, 100, 100);

                m_oSheet.Range["Agency_Name", missing].Value = CCFBPrefs.FoodBankName;
                m_oSheet.Range["Contact", missing].Value = CCFBPrefs.PreparedBy;
                m_oSheet.Range["Phone", missing].Value = CCFBPrefs.PhoneNumber;
                m_oSheet.Range["Email", missing].Value = CCFBPrefs.EmailAddress;
                m_oSheet.Range["Fax", missing].Value = CCFBPrefs.FaxNumber;

                int rowRange = 14;
                int columnRange = 2;

                for (int i = 1; i < dgv.Columns.Count; i++)
                {
                    if (dgv.Columns[i].Visible == true)
                    {
                        m_oSheet.Cells[rowRange, columnRange] = dgv.Columns[i].HeaderText;
                        columnRange++;
                    }
                }
                rowRange++;

                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    columnRange = 1;
                    for (int j = 0; j < dgv.Columns.Count; j++)
                    {
                        if (dgv.Columns[j].Visible == true)
                        {
                            m_oSheet.Cells[rowRange, columnRange] = dgv[j, i].Value;
                            columnRange++;
                        }
                    }
                    rowRange++;
                }
               
                m_oBook.SaveAs(saveAs, XlFileFormat.xlWorkbookNormal, null,
                null, null, null, XlSaveAsAccessMode.xlNoChange, XlListConflict.xlListConflictDiscardAllConflicts, null, null, null,
                null);

                m_oExcelApp.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(m_oExcelApp);
                m_oSheet = null;
                m_oBook = null;
                m_oExcelApp = null;

                CCFBGlobal.openDocumentOutsideCCFB(saveAs.ToString());
            }
            catch (Exception ex)
            {
                m_oExcelApp.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(m_oExcelApp);
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString());
                m_oSheet = null;
                m_oBook = null;
                m_oExcelApp = null;
            }
            
        }
    }
}

/*Microsoft.Office.Interop.Word.Application oWord = 
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
            }*/
