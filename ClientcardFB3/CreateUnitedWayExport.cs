using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using Microsoft.Office.Interop.Excel;

namespace ClientcardFB3
{
    class CreateUnitedWayExport
    {
        System.Data.DataTable dtExport;
        bool error = false;

        public bool Error
        {
            get
            {
                return error;
            }
        }

        public CreateUnitedWayExport(System.Data.DataTable dt)
        {
            dtExport = dt;
        }

        public void createExport(object saveAs, object templatePath)
        {
            Application m_oExcelApp;
            //Workbooks m_oBooks;
            _Workbook m_oBook;
            _Worksheet m_oSheet;
            //Range excelRange;

            Object oTrue = true;
            Object oFalse = false;

            m_oExcelApp = new Application();
            m_oExcelApp.Visible = false;
            //m_oBooks = m_oExcelApp.Workbooks;
            m_oSheet = null;
            // m_oBooks = null;
            //excelRange = null;
            object oMissing = Type.Missing;

            Object oTemplatePath = templatePath;

            try
            {
                m_oBook = m_oExcelApp.Workbooks.Add(oTemplatePath);
                m_oSheet = (_Worksheet)m_oBook.ActiveSheet;
                int row = 4;
                int column = 3;
                //m_oBook.VBProject.VBComponents.Add(Microsoft.Vbe.Interop.vbext_ComponentType.vbext_ct_StdModule);

                for (int i = 0; i < dtExport.Rows.Count; i++)
                {
                    for (int j = 0; j < dtExport.Columns.Count; j++)
                    {
                        m_oSheet.Cells[row, column] = dtExport.Rows[i][j];
                        column++;
                    }
                    column = 3;
                    row++;
                }
               
               
                //string template = Application.StartupPath;
                //string strRunReport = templatePath + "\\" + "SalesReport.xls";
                m_oBook.SaveAs(saveAs, XlFileFormat.xlWorkbookNormal, null,
                null, null, null, XlSaveAsAccessMode.xlShared, null, null, null, null,
                null);



                //if (autoPrint == true)
                //    m_oBook.PrintOutEx(oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing);

                m_oExcelApp.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(m_oExcelApp);
                m_oSheet = null;
                m_oBook = null;
                m_oExcelApp = null;
                error = false;

                //if (!autoPrint)
                    CCFBGlobal.openDocumentOutsideCCFB(saveAs.ToString());
            }
            catch (Exception ex)
            {
                m_oExcelApp.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(m_oExcelApp);
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString());
                error = true;
                m_oSheet = null;
                m_oBook = null;
                m_oExcelApp = null;
                //MessageBox.Show("Error accessing Excel: " + ex.ToString());
            }
        }
    }
}
