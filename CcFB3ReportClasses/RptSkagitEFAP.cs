using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Office.Interop.Word;

namespace ClientcardFB3
{
    class RptSkagitEFAP : IDisposable
    {
        TrxLogPeriodTotals clsMonthStats;
        VolunteerStats clsVolunteerStats;
        System.Data.DataTable dtFoodReceipts;
        bool error = false;
        bool _disposed = false;

        public bool Error
        {
            get
            {
                return error;
            }
        }

        public RptSkagitEFAP(TrxLogPeriodTotals clsStatsIn, VolunteerStats clsVolStats, System.Data.DataTable dtfoodrcpts)
        {
            clsMonthStats = clsStatsIn;
            clsVolunteerStats = clsVolStats;
            dtFoodReceipts = dtfoodrcpts;
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
                    if (clsVolunteerStats != null)
                        clsVolunteerStats.Dispose();
                }

                // Indicate that the instance has been disposed.
                clsMonthStats = null;
                clsVolunteerStats = null;
                _disposed = true;
            }
        }

        public void createReport(string foodBankName, string reportMonth, string reportYear,
            string preparedBy, object saveAs, string templatePath, string rptDate)
        {
                Object oMissing = System.Reflection.Missing.Value;
                Object missing = System.Reflection.Missing.Value;
                Object oTrue = true;
                Object oFalse = false;
                Application oWord = new Application();
                Document oWordDoc = new Document();
                oWord.Visible = false;
                Object oTemplatePath = templatePath;
                int tmpValue = 0;
            try
            {

                oWordDoc = oWord.Documents.Add(ref oTemplatePath, ref oMissing, ref oMissing, ref oMissing);

                //Save so that the template is free to be used by the next user
                oWordDoc.SaveAs(ref saveAs, ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing);

                oWordDoc.Tables[1].Cell(1, 2).Range.Text = reportMonth;
                oWordDoc.Tables[1].Cell(1, 4).Range.Text = reportYear;
                oWordDoc.Tables[1].Cell(2, 2).Range.Text = foodBankName;

                oWordDoc.Tables[2].Cell(2, 2).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.HHTotalServedNew);
                oWordDoc.Tables[2].Cell(3, 2).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.HHTotalServedReturning);
                //Individual Info
                oWordDoc.Tables[2].Cell(2, 4).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.InfantsNew);
                oWordDoc.Tables[2].Cell(3, 4).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.InfantsReturning);
                oWordDoc.Tables[2].Cell(2, 5).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.ChildrenNew + clsMonthStats.EighteenNew);
                oWordDoc.Tables[2].Cell(3, 5).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.ChildrenReturning + clsMonthStats.EighteenReturning);
                oWordDoc.Tables[2].Cell(2, 6).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.AdultsNew);
                oWordDoc.Tables[2].Cell(3, 6).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.AdultsReturning);
                oWordDoc.Tables[2].Cell(2, 7).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.SeniorsNew);
                oWordDoc.Tables[2].Cell(3, 7).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.SeniorsReturning);
                oWordDoc.Tables[2].Cell(2, 8).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.TotalFamilyNew);
                oWordDoc.Tables[2].Cell(3, 8).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.TotalFamilyReturning);

                //Special Diet AND Supplemental
                oWordDoc.Tables[3].Cell(2, 1).Range.Text = "New: " + CCFBGlobal.formatNumberWithCommas(clsMonthStats.CntSpecialDietNew);
                oWordDoc.Tables[3].Cell(2, 2).Range.Text = "Returning: " + CCFBGlobal.formatNumberWithCommas(clsMonthStats.CntSpecialDietReturning);
                oWordDoc.Tables[3].Cell(2, 1).Range.Text = "New: " + CCFBGlobal.formatNumberWithCommas(clsMonthStats.HHRcvdSupplementalNew);
                oWordDoc.Tables[3].Cell(2, 2).Range.Text = "Returning: " + CCFBGlobal.formatNumberWithCommas(clsMonthStats.HHRcvdSupplementalReturning);

                //Volunteers
                oWordDoc.Tables[4].Cell(2, 4).Range.Text = CCFBGlobal.formatNumberWithCommas(clsVolunteerStats.NumVolHours);
                oWordDoc.Tables[4].Cell(2, 4).Range.Text = CCFBGlobal.formatNumberWithCommas(clsVolunteerStats.NumVolHours);

                tmpValue = Convert.ToInt32(dtFoodReceipts.Rows[0]["LbsInkind"]) + Convert.ToInt32(dtFoodReceipts.Rows[0]["LbsGroceryRescue"]);
                oWordDoc.Tables[4].Cell(3, 2).Range.Text = CCFBGlobal.formatNumberWithCommas(tmpValue);
                //Pounds Info
                tmpValue = clsMonthStats.LbsStandard + clsMonthStats.LbsOther + clsMonthStats.LbsBabySvc + clsMonthStats.LbsCommodity;
                oWordDoc.Tables[5].Cell(2, 2).Range.Text = CCFBGlobal.formatNumberWithCommas(tmpValue) + " Lbs";
                if (clsMonthStats.LbsSupplemental > 0)
                {
                    oWordDoc.Tables[5].Cell(3, 2).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.LbsSupplemental) + " Lbs";
                }
                //Nbr Days Open
//                table.Cell(9, 2).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.NbrDaysOpen);
                

                oWordDoc.SaveAs(ref saveAs, ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing);

                ((_Application)oWord).Quit(SaveChanges: false, OriginalFormat: false, RouteDocument: false);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oWord);

                CCFBGlobal.openDocumentOutsideCCFB(saveAs.ToString());
            }
            catch(Exception ex)
            {
                CCFBGlobal.appendErrorToErrorReport("File Path = " + oTemplatePath.ToString(), ex.GetBaseException().ToString());
                ((_Application)oWord).Quit(SaveChanges: false, OriginalFormat: false, RouteDocument: false);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oWord);
                error = true;
            }
        }
    }
}
