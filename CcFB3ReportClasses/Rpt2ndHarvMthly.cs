using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using Microsoft.Office.Interop.Excel;

namespace ClientcardFB3
{
    class Rpt2ndHarvMthly : IDisposable
    {
        TrxLogPeriodTotals clsMonthStats;
        bool error = false;
        bool _disposed = false;

        public bool Error
        {
            get
            {
                return error;
            }
        }

        public Rpt2ndHarvMthly(TrxLogPeriodTotals clsStatsIn)
        {
            clsMonthStats = clsStatsIn;
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
                    if (clsMonthStats != null)
                        clsMonthStats.Dispose();
                }

                // Indicate that the instance has been disposed.
                clsMonthStats = null;
                _disposed = true;
            }
        }

        public void createReport(string foodBankName, string reportPeriod, string preparedBy, object saveAs, string templatePath
            , string county, string phone
            , string totalLbs, string nbrDaysOpen)
        {
            Application m_oExcelApp;
            _Workbook m_oBook;
            _Worksheet m_oSheet;

            m_oExcelApp = new Application();
            m_oExcelApp.Visible = true;
            m_oSheet = null;
            object missing = Type.Missing;

            Object oTemplatePath = templatePath;

            try
            {

                m_oBook = m_oExcelApp.Workbooks.Open(templatePath
                    , false, false, missing, missing, missing, true, missing, missing, true
                    , missing, missing, false, missing, true);

                Debug.Print(m_oBook.Sheets.Count.ToString());

                m_oSheet = (_Worksheet)m_oBook.Sheets[1];


                m_oSheet.Cells[7, 4] = foodBankName;
                m_oSheet.Cells[8, 4] = county;
                m_oSheet.Cells[9, 4] = preparedBy;

                m_oSheet.Cells[7, 9] = reportPeriod.Substring(5) + " / " + reportPeriod.Substring(0, 4);
                m_oSheet.Cells[9, 9] = phone;
                m_oSheet.Cells[11, 9] = nbrDaysOpen;
                m_oSheet.Cells[13, 7] = totalLbs;

                m_oSheet.Cells[16, 7] = clsMonthStats.HHTotalServedNew;
                m_oSheet.Cells[16, 5] = clsMonthStats.HHTotalServedReturning;

                m_oSheet.Cells[19, 7] = clsMonthStats.InfantsNew;
                m_oSheet.Cells[20, 7] = clsMonthStats.YouthNew + clsMonthStats.TeensNew + clsMonthStats.EighteenNew;
                m_oSheet.Cells[21, 7] = clsMonthStats.AdultsNew;
                m_oSheet.Cells[22, 7] = clsMonthStats.SeniorsNew;

                m_oSheet.Cells[19, 5] = clsMonthStats.InfantsReturning;
                m_oSheet.Cells[20, 5] = clsMonthStats.YouthReturning + clsMonthStats.TeensReturning + clsMonthStats.EighteenReturning;
                m_oSheet.Cells[21, 5] = clsMonthStats.AdultsReturning;
                m_oSheet.Cells[22, 5] = clsMonthStats.SeniorsReturning;

                m_oSheet.Calculate();

                if (File.Exists(saveAs.ToString()) == true)
                {
                    File.Delete(saveAs.ToString());
                }
                m_oBook.SaveAs(saveAs, XlFileFormat.xlWorkbookNormal, null,
                null, null, null, XlSaveAsAccessMode.xlShared, null, null, null, null,
                null);

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
