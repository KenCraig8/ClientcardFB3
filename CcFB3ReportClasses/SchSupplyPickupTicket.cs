using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Excel;

namespace ClientcardFB3
{
    class SchSupplyPickupTicket
    {
        bool error = false;

        public bool Error
        {
            get
            {
                return error;
            }
        }
        public SchSupplyPickupTicket()
        {

        }

        public void createReport(Client clsClient)
        {
            Application m_oExcelApp;
            _Workbook m_oBook;
            _Worksheet m_oSheet;

            int eRow = 7;
            object saveAs = "";
            string templatePath = "";
            string savePath = CCFBGlobal.pathExe + "SchoolSupply\\PickupTickets" + DateTime.Today.Year.ToString() + "\\";

            m_oExcelApp = new Application();
            m_oExcelApp.Visible = true;
            m_oSheet = null;
            object missing = Type.Missing;

            CCFBGlobal.verifyPath(savePath);
            saveAs = savePath + "HhID" + CCFBGlobal.formatNumberWithSixLeadingZeros(clsClient.clsHH.ID) + ".xlsx";

            templatePath = CCFBGlobal.pathTemplates + "NCCFBSchoolSupplyPickupTicket.xlsx";

            //Object oTemplatePath = templatePath;

            try
            {

                m_oBook = m_oExcelApp.Workbooks.Open(templatePath
                    , XlUpdateLinks.xlUpdateLinksNever, false, missing, missing, missing, true, missing, missing, true
                    , missing, missing, false, missing, XlCorruptLoad.xlNormalLoad);

                Debug.Print(m_oBook.Sheets.Count.ToString());

                m_oSheet = (_Worksheet)m_oBook.Sheets[1];


                //m_oSheet.Cells[4, 2] = CCFBPrefs.FoodBankName;
                m_oSheet.Cells[3, 2] = clsClient.clsHH.ID.ToString();
                m_oSheet.Cells[4, 5] = clsClient.clsHH.SchSupplyPickupPerson;
                for (int i = 0; i < clsClient.clsHHmem.RowCount; i++)
                {
                    clsClient.clsHHmem.SetRecord(i);
                    if (clsClient.clsHHmem.Inactive == false && clsClient.clsHHmem.SchSupply == true)
                    {
                        eRow++;
                        m_oSheet.Cells[eRow, 1] = clsClient.clsHHmem.Name;
                        m_oSheet.Cells[eRow, 4] = clsClient.clsHHmem.Grade.ToString();
                        m_oSheet.Cells[eRow, 5] = clsClient.clsHHmem.Age.ToString();
                        m_oSheet.Cells[eRow, 6] = clsClient.clsHHmem.Sex;
                        m_oSheet.Cells[eRow, 7] = CCFBGlobal.LongNameFromId(CCFBGlobal.parmTbl_SchSupplySchool, clsClient.clsHHmem.SchSupplySchool);
                    }
                }
                if (File.Exists(saveAs.ToString()) == true)
                {
                    File.Delete(saveAs.ToString());
                }
                m_oBook.SaveAs(saveAs, XlFileFormat.xlWorkbookDefault, missing,
                missing, true, false, XlSaveAsAccessMode.xlShared, missing,false, missing, missing,true);

                m_oExcelApp.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(m_oExcelApp);
                m_oSheet = null;
                m_oBook = null;
                m_oExcelApp = null;
                error = false;

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
