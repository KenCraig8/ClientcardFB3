using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using Microsoft.Office.Interop.Excel;

namespace ClientcardFB3
{
    class RptFoodMenu01 : IDisposable
    {
        Household clsHH;
        HHMembers clsHHM;
        TrxLogItem clsTLItm;
        bool error = false;
        bool _disposed = false;
        string rptDate;
        string savePath;

        public bool Error
        {
            get { return error; }
        }

        public RptFoodMenu01(Household hhIN, HHMembers hhmemIN, TrxLogItem tlitmIN)
        {
            clsHH = hhIN;
            clsHHM = hhmemIN;
            clsTLItm = tlitmIN;
            rptDate = clsTLItm.TrxDate.ToShortDateString();
            savePath = CCFBGlobal.pathLog + "Services" + rptDate.Substring(6, 4) + "\\" + rptDate.Substring(0, 2) + "\\" + rptDate.Substring(3, 2) +"\\";
            CCFBGlobal.verifyPath(savePath);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            // If you need thread safety, use a lock around these 
            // operations, as well as in your methods that use the resource.
            if (!_disposed)
            {
                if (disposing)
                {
                    if (clsHH != null)
                        clsHH.Dispose();
                    if (clsHHM != null)
                        clsHHM.Dispose();
                }

                // Indicate that the instance has been disposed.
                clsHH = null;
                clsHHM = null;
                _disposed = true;
            }
        }

        public void createReport(string templatePath)
        {
            Application m_oExcelApp;
            _Workbook m_oBook;
            _Worksheet m_oSheet;

            m_oExcelApp = new Application();
            m_oExcelApp.WindowState = XlWindowState.xlNormal; 
            m_oExcelApp.Visible = true;
            m_oSheet = null;
            object missing = Type.Missing;

            Object oTemplatePath = templatePath;

            try
            {
                
                m_oBook = m_oExcelApp.Workbooks.Open(templatePath
                    , false, false,missing,missing, missing, true, missing, missing, true
                    , missing, missing, false, missing, true);

                Debug.Print( m_oBook.Sheets.Count.ToString());
                string saveAs = savePath + "UID" + clsHH.ID.ToString() + ".xls";
                if (File.Exists(saveAs) == true)
                {
                    File.Delete(saveAs.ToString());
                }
                m_oBook.SaveAs(saveAs, XlFileFormat.xlWorkbookNormal, null,
                null, null, null, XlSaveAsAccessMode.xlShared, null, null, null, null,
                null);

                m_oSheet = (_Worksheet)m_oBook.Sheets[1];
                string tmpA = "Yes";
                string tmpC = "No";
                string tmpDislikes = "";
                

                m_oSheet.Cells[1, 2] = CCFBPrefs.FoodBankName + ": Food Order";


                m_oSheet.Cells[4, 2] = "( " + clsHH.ID.ToString() + " ) " + clsHH.Name;
                m_oSheet.Cells[5, 3] = clsTLItm.HHMemID;
                m_oSheet.Cells[6, 3] = clsTLItm.TotalFamily.ToString();
                if (clsTLItm.Homeless == true)
                { m_oSheet.Cells[7, 3] = tmpA; }
                else
                { m_oSheet.Cells[7, 3] = tmpC; }
                tmpA = "";
                tmpC = "";
                for (int i = 0; i < clsHH.TotalFamily; i++)
                {
                    clsHHM.SetRecord(i);
                    if (clsHHM.Inactive == false)
                    {
                        if (clsHHM.Notes.Length >0)
                        {
                            if (tmpDislikes.Length >0)
                            { tmpDislikes += ", "; }
                            tmpDislikes += clsHHM.Notes;
                        }
                        if (clsHHM.Age > 18)
                        {
                            if (tmpA.Length >0)
                            { tmpA += ", "; }
                            tmpA += clsHHM.Age.ToString();
                        }
                        else
                        {
                            if (tmpC.Length >0)
                            { tmpC += ", "; }
                            tmpC += clsHHM.Age.ToString();
                        }
                    }
                }
                m_oSheet.Cells[8, 3] = tmpA;
                m_oSheet.Cells[9, 3] = tmpC;
                m_oSheet.Cells[10, 3] = clsTLItm.FoodSvcList;
                m_oSheet.Cells[11, 3] = tmpDislikes;
                if (clsHH.UserFlag9 == true) 
                { m_oSheet.Cells[12, 3] = "Yes"; }
                else
                { m_oSheet.Cells[12, 3] = "No"; }
                m_oSheet.Cells[13, 3] = rptDate;

                m_oSheet.Calculate();
                m_oBook.Save();
                m_oBook.PrintOutEx();
                m_oExcelApp.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(m_oExcelApp);
                m_oSheet = null;
                m_oBook = null;
                m_oExcelApp = null;
                error = false;

                //CCFBGlobal.openDocumentOutsideCCFB(saveAs.ToString());
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

