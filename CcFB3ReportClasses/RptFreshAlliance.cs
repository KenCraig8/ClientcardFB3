using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using System.Drawing;

namespace ClientcardFB3
{
    class RptFreshAlliance
    {
        DataGridView dgv;
        public RptFreshAlliance(DataGridView dgvIn)
        {
            dgv = dgvIn;
        }

        public void createReport(object saveAs, string templatePath, string DonorName, string ReportMonth)
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
                
                //string tempLogo = "";
                
                //if(isFreshAlliance == true)
                // tempLogo = CCFBGlobal.fb3TemplatesPath + "LifeLineLogo.bmp";
                //else
                //    tempLogo = CCFBGlobal.fb3TemplatesPath + "FBLogo.bmp";

                //m_oSheet.Shapes.AddPicture(tempLogo, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoTrue,
                //    300, 10, 100, 100);

                m_oSheet.Range["Agency_Name", missing].Value = CCFBPrefs.FoodBankName;
                m_oSheet.Range["ReportMonth", missing].Value = ReportMonth;
                m_oSheet.Range["DonorName", missing].Value = DonorName;

                int rowRange = 8;
                int columnRange = 1;

                for (int i = 0; i < dgv.Rows.Count - 1; i++)
                {
                        m_oSheet.Cells[rowRange + i, columnRange] = dgv.Rows[i].HeaderCell.Value.ToString();
                }


                for (int i = 0; i < dgv.Rows.Count-1; i++)
                {
                    columnRange = 2;
                    for (int j = 0; j < dgv.Columns.Count-1; j++)
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

