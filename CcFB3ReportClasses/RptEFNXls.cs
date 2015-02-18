using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using Microsoft.Office.Interop.Excel;

namespace ClientcardFB3
{
    class RptEFNXls : IDisposable
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

        public RptEFNXls(TrxLogPeriodTotals clsMonthStatsIn)
        {
            clsMonthStats = clsMonthStatsIn;
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

        public void createReport(string foodBankName, string reportPeriod,
            string preparedBy, object saveAs, string templatePath, string rptDate, string totalLbs)
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
                    , false, false,missing,missing, missing, true, missing, missing, true
                    , missing, missing, false, missing, true);

                Debug.Print( m_oBook.Sheets.Count.ToString());

                m_oSheet = (_Worksheet)m_oBook.Sheets[1];
                

                m_oSheet.Cells[4, 2] = CCFBPrefs.FoodBankName;
                m_oSheet.Cells[6, 2] = preparedBy;
                m_oSheet.Cells[4, 6] = reportPeriod;


                m_oSheet.Cells[16, 2] = clsMonthStats.InfantsNew;
                m_oSheet.Cells[17, 2] = clsMonthStats.YouthNew + clsMonthStats.TeensNew + clsMonthStats.EighteenNew;
                m_oSheet.Cells[18, 2] = clsMonthStats.AdultsNew;
                m_oSheet.Cells[19, 2] = clsMonthStats.SeniorsNew;

                m_oSheet.Cells[16, 3] = clsMonthStats.InfantsReturning;
                m_oSheet.Cells[17, 3] = clsMonthStats.YouthReturning + clsMonthStats.TeensReturning + clsMonthStats.EighteenReturning;
                m_oSheet.Cells[18, 3] = clsMonthStats.AdultsReturning;
                m_oSheet.Cells[19, 3] = clsMonthStats.SeniorsReturning;

                m_oSheet.Cells[23, 2] = clsMonthStats.HHTotalServedNew;
                m_oSheet.Cells[23, 3] = clsMonthStats.HHTotalServedReturning;

                m_oSheet.Cells[25, 2] = clsMonthStats.CntSpecialDietNew;
                m_oSheet.Cells[25, 3] = clsMonthStats.CntSpecialDietReturning;

                m_oSheet.Cells[27, 2] = clsMonthStats.HHRcvdSupplementalNew;
                m_oSheet.Cells[27, 3] = clsMonthStats.HHRcvdSupplementalReturning;

                m_oSheet.Cells[33, 2] = totalLbs;
                m_oSheet.Cells[35, 2] = clsMonthStats.LbsSupplemental;

                //Race
                m_oSheet.Cells[13, 7] = clsMonthStats.WhiteNew;
                m_oSheet.Cells[14, 7] = clsMonthStats.BlackNew;
                m_oSheet.Cells[15, 7] = clsMonthStats.AsianNew;
                m_oSheet.Cells[16, 7] = clsMonthStats.NativeNew;
                m_oSheet.Cells[18, 7] = clsMonthStats.PacificNew;
                m_oSheet.Cells[20, 7] = clsMonthStats.WhiteNativeNew;
                m_oSheet.Cells[22, 7] = clsMonthStats.WhiteAsianNew;
                m_oSheet.Cells[23, 7] = clsMonthStats.WhiteBlackNew;
                m_oSheet.Cells[26, 7] = clsMonthStats.BlackNativeNew;
                m_oSheet.Cells[28, 7] = clsMonthStats.OtherNew;
                m_oSheet.Cells[29, 7] = clsMonthStats.UndisclosedNew;

                m_oSheet.Cells[13, 8] = clsMonthStats.WhiteHispanicNew;
                m_oSheet.Cells[14, 8] = clsMonthStats.BlackHispanicNew;
                m_oSheet.Cells[15, 8] = clsMonthStats.AsianHispanicNew;
                m_oSheet.Cells[16, 8] = clsMonthStats.NativeHispanicNew;
                m_oSheet.Cells[18, 8] = clsMonthStats.PacificHispanicNew;
                m_oSheet.Cells[20, 8] = clsMonthStats.WhiteNativeHispanicNew;
                m_oSheet.Cells[22, 8] = clsMonthStats.WhiteAsianHispanicNew;
                m_oSheet.Cells[23, 8] = clsMonthStats.WhiteBlackHispanicNew;
                m_oSheet.Cells[26, 8] = clsMonthStats.BlackNativeHispanicNew;
                m_oSheet.Cells[28, 8] = clsMonthStats.OtherHispanicNew;
                m_oSheet.Cells[29, 8] = clsMonthStats.UndisclosedHispanicNew;
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

