using System;
using System.Data;
using System.IO;
using Microsoft.Office.Interop.Excel;

namespace ClientcardFB3
{
    class RptBMAC : IDisposable
    {
        //Asotin Food Bank 1546 Maple St.	99403

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

        public RptBMAC(TrxLogPeriodTotals clsMonthStatsIn)
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

        public void createReport(object saveAs, string templatePath, string totalLbs, string dollarsLabor,
            string lbsEFAP, string lbsTEFAP, string lbs2ndHarvest, string preparedBy, string reportMonth)
        {
            Application m_oExcelApp;
            //Workbooks m_oBooks;
            _Workbook m_oBook;
            _Worksheet m_oSheet;

            m_oExcelApp = new Application();
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
                m_oSheet.Range["FoodbankName", missing].Value = CCFBPrefs.FoodBankName;
                //m_oSheet.Range["AddressLine1", missing].Value = CCFBPrefs.PhysicalAddress;
                //m_oSheet.Range["AddressLine2", missing].Value = CCFBPrefs.PostalAddress;
                //m_oSheet.Range["AddressLine3", missing].Value = CCFBPrefs.DefaultCity + ", " 
                //    + CCFBPrefs.DefaultState + " " + CCFBPrefs.DefaultZipcode;
                //m_oSheet.Range["Contact", missing].Value = preparedBy;
                //m_oSheet.Range["Phone", missing].Value = CCFBPrefs.PhoneNumber;
                m_oSheet.Range["ReportMonthYear", missing].Value = reportMonth;


                m_oSheet.Range["InfantsNew", missing].Value = clsMonthStats.InfantsNew;
                m_oSheet.Range["ChildrenNew", missing].Value = clsMonthStats.YouthNew + clsMonthStats.TeensNew + clsMonthStats.EighteenNew;
                m_oSheet.Range["AdultsNew", missing].Value = clsMonthStats.AdultsNew;
                m_oSheet.Range["SeniorsNew", missing].Value = clsMonthStats.SeniorsNew;

                m_oSheet.Range["InfantsReturn", missing].Value = clsMonthStats.InfantsReturning;
                m_oSheet.Range["ChildrenReturn", missing].Value = clsMonthStats.YouthReturning + clsMonthStats.TeensReturning + clsMonthStats.EighteenReturning;
                m_oSheet.Range["AdultsReturn", missing].Value = clsMonthStats.AdultsReturning;
                m_oSheet.Range["SeniorsReturn", missing].Value = clsMonthStats.SeniorsReturning;

                m_oSheet.Range["HouseholdsNew", missing].Value = clsMonthStats.HHTotalServedNew;
                m_oSheet.Range["HouseholdsReturn", missing].Value = clsMonthStats.HHTotalServedReturning;

                m_oSheet.Range["TotalPounds", missing].Value = totalLbs;
                m_oSheet.Range["SupplPounds", missing].Value = clsMonthStats.LbsSupplemental;

                m_oSheet.Range["EFAPPounds", missing].Value = lbsEFAP;
                m_oSheet.Range["TEFAPPounds", missing].Value = lbsTEFAP;
                m_oSheet.Range["SecondHarvestPounds", missing].Value = lbs2ndHarvest;

                //string template = Application.StartupPath;
                //string strRunReport = templatePath + "\\" + "SalesReport.xls";
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

