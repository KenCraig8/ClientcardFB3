using System;
using System.Data;
using Microsoft.Office.Interop.Word;

namespace ClientcardFB3
{
    class RptFBCoalition
    {
        TrxLogPeriodTotals clsMonthStats;
        VolunteerStats clsVolunteerStats;
        bool error = false;

        public bool Error
        {
            get
            {
                return error;
            }
        }

        public RptFBCoalition(TrxLogPeriodTotals clsStatsIn, VolunteerStats clsVolStats)
        {
            clsMonthStats = clsStatsIn;
            clsVolunteerStats = clsVolStats;
        }

        public void createReport(string foodBankName, string reportMonth, string fiscalYear,
            string preparedBy, object saveAs, string templatePath, string rptDate, bool withTeens)
        {
                Object oMissing = System.Reflection.Missing.Value;
                Object missing = System.Reflection.Missing.Value;
                Object oTrue = true;
                Object oFalse = false;
                Application oWord = new Application();
                Document oWordDoc = new Document();
                oWord.Visible = false;
                Object oTemplatePath = templatePath;
            try
            {

                oWordDoc = oWord.Documents.Add(ref oTemplatePath, ref oMissing, ref oMissing, ref oMissing);

                //Save so that the template is free to be used by the next user
                oWordDoc.SaveAs(ref saveAs, ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing);

                //Object oBookMarkName = "FBName";
                oWordDoc.Tables[1].Cell(1, 1).Range.Text = foodBankName;
                oWordDoc.Tables[1].Cell(1, 2).Range.Text = "MONTH: " + reportMonth;
                oWordDoc.Tables[8].Cell(1, 4).Range.Text = rptDate;

                //Individual Info
                fillTableSet(oWordDoc.Tables[2], 3, 2, clsMonthStats.InfantsReturning, clsMonthStats.InfantsNew, clsMonthStats.Infants);
                int nRow = 5;
                if (withTeens == true)
                {
                    fillTableSet(oWordDoc.Tables[2], 4, 2, clsMonthStats.YouthReturning, clsMonthStats.YouthNew, clsMonthStats.Youth);
                    fillTableSet(oWordDoc.Tables[2], nRow, 2, clsMonthStats.TeensReturning + clsMonthStats.EighteenReturning
                                                         , clsMonthStats.TeensNew + clsMonthStats.EighteenNew
                                                         , clsMonthStats.Teens + clsMonthStats.Eighteen);
                    nRow++;
                }
                else
                {
                    fillTableSet(oWordDoc.Tables[2], 4, 2, clsMonthStats.ChildrenReturning + clsMonthStats.EighteenReturning
                                                         , clsMonthStats.ChildrenNew + clsMonthStats.EighteenNew
                                                         , clsMonthStats.Children + clsMonthStats.Eighteen);
                }
                fillTableSet(oWordDoc.Tables[2], nRow, 2, clsMonthStats.AdultsReturning
                                                     , clsMonthStats.AdultsNew
                                                     , clsMonthStats.Adults);
                fillTableSet(oWordDoc.Tables[2], nRow + 1, 2, clsMonthStats.SeniorsReturning
                                                     , clsMonthStats.SeniorsNew
                                                     , clsMonthStats.Seniors);
                fillTableSet(oWordDoc.Tables[2], nRow + 2, 2, clsMonthStats.TotalFamilyReturning
                                                     , clsMonthStats.TotalFamilyNew
                                                     , clsMonthStats.TotalFamily);
                ///Dissabled and Special Diet
                fillTableSet(oWordDoc.Tables[3], 1, 2, clsMonthStats.CntDisabledReturning, clsMonthStats.CntDisabledNew, clsMonthStats.CntDisabled);
                fillTableSet(oWordDoc.Tables[3], 2, 2, clsMonthStats.CntSpecialDietReturning, clsMonthStats.CntSpecialDietNew, clsMonthStats.CntSpecialDiet);
                //Household Info
                fillTableSet(oWordDoc.Tables[4], 3, 2, clsMonthStats.HHRcvdCommodityReturning, clsMonthStats.HHRcvdCommodityNew, clsMonthStats.HHRcvdCommodity);
                fillTableSet(oWordDoc.Tables[4], 4, 2, clsMonthStats.HHRcvdStdReturning, clsMonthStats.HHRcvdStdNew, clsMonthStats.HHRcvdStd);
                fillTableSet(oWordDoc.Tables[4], 5, 2, clsMonthStats.HHRcvdSupplementalReturning, clsMonthStats.HHRcvdSupplementalNew, clsMonthStats.HHRcvdSupplemental);
                fillTableSet(oWordDoc.Tables[4], 6, 2, clsMonthStats.HHTotalServedReturning, clsMonthStats.HHTotalServedNew, clsMonthStats.HHTotalServed);
                fillTableSet(oWordDoc.Tables[4], 8, 2, clsMonthStats.HHRcvdBabyServicesReturning, clsMonthStats.HHRcvdBabyServicesNew, clsMonthStats.HHRcvdBabyServices);
                fillTableSet(oWordDoc.Tables[4], 9, 2, clsMonthStats.HHHomelessReturning, clsMonthStats.HHHomelessNew, clsMonthStats.HHHomeless);
                fillTableSet(oWordDoc.Tables[4], 10, 2, clsMonthStats.HHTransientReturning, clsMonthStats.HHTransientNew, clsMonthStats.HHTransient);
                fillTableSet(oWordDoc.Tables[4], 11, 2, clsMonthStats.HHSingleFemaleReturning, clsMonthStats.HHSingleFemaleNew, clsMonthStats.HHSingleFemale);
                fillTableSet(oWordDoc.Tables[4], 12, 2, clsMonthStats.HHSingleMaleReturning, clsMonthStats.HHSingleMaleNew, clsMonthStats.HHSingleMale);
                fillTableSet(oWordDoc.Tables[4], 13, 2, clsMonthStats.HHOneParentFemaleReturning, clsMonthStats.HHOneParentFemaleNew, clsMonthStats.HHOneParentFemale);
                fillTableSet(oWordDoc.Tables[4], 14, 2, clsMonthStats.HHOneParentMaleReturning, clsMonthStats.HHOneParentMaleNew, clsMonthStats.HHOneParentMale);
                //Pounds Info
                Table table = oWordDoc.Tables[5];
                table.Cell(2, 2).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.LbsStandard);
                table.Cell(3, 2).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.LbsOther);
                table.Cell(4, 2).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.LbsCommodity);
                table.Cell(5, 2).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.LbsSupplemental);
                table.Cell(6, 2).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.LbsBabySvc);
                table.Cell(7, 2).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.LbsTotalServed);
                table.Cell(8, 2).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.LbsNonFood);
                //Nbr Days Open
                table.Cell(9, 2).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.NbrDaysOpen);
                //Volunteers
                oWordDoc.Tables[6].Cell(2, 2).Range.Text = CCFBGlobal.formatNumberWithCommas(clsVolunteerStats.NumVols);
                oWordDoc.Tables[6].Cell(3, 2).Range.Text = CCFBGlobal.formatNumberWithCommas(clsVolunteerStats.NumVolHours);
                //----------------------- Year To Date Columns ----------------
                clsMonthStats.setYTDRow();
                //YTD - Individual Info 
                fillTableSet(oWordDoc.Tables[2], 3, 6, clsMonthStats.InfantsReturning, clsMonthStats.InfantsNew, clsMonthStats.Infants);
                nRow = 5;
                if (withTeens == true)
                {
                    fillTableSet(oWordDoc.Tables[2], 4, 6, clsMonthStats.YouthReturning, clsMonthStats.YouthNew, clsMonthStats.Youth);
                    fillTableSet(oWordDoc.Tables[2], nRow, 6, clsMonthStats.TeensReturning + clsMonthStats.EighteenReturning
                                                         , clsMonthStats.TeensNew + clsMonthStats.EighteenNew
                                                         , clsMonthStats.Teens + clsMonthStats.Eighteen);
                    nRow++;
                }
                else
                {
                    fillTableSet(oWordDoc.Tables[2], 4, 6, clsMonthStats.ChildrenReturning + clsMonthStats.EighteenReturning
                                                         , clsMonthStats.ChildrenNew + clsMonthStats.EighteenNew
                                                         , clsMonthStats.Children + clsMonthStats.Eighteen);
                }
                fillTableSet(oWordDoc.Tables[2], nRow, 6, clsMonthStats.AdultsReturning
                                                     , clsMonthStats.AdultsNew
                                                     , clsMonthStats.Adults);
                fillTableSet(oWordDoc.Tables[2], nRow + 1, 6, clsMonthStats.SeniorsReturning
                                                     , clsMonthStats.SeniorsNew
                                                     , clsMonthStats.Seniors);
                fillTableSet(oWordDoc.Tables[2], nRow + 2, 6, clsMonthStats.TotalFamilyReturning
                                                     , clsMonthStats.TotalFamilyNew
                                                     , clsMonthStats.TotalFamily);

                //YTD - Dissabled and Special Diet
                fillTableSet(oWordDoc.Tables[3], 1, 6, clsMonthStats.CntDisabledReturning, clsMonthStats.CntDisabledNew, clsMonthStats.CntDisabled);
                fillTableSet(oWordDoc.Tables[3], 2, 6, clsMonthStats.CntSpecialDietReturning, clsMonthStats.CntSpecialDietNew, clsMonthStats.CntSpecialDiet);
                //Household Info
                fillTableSet(oWordDoc.Tables[4], 3, 6, clsMonthStats.HHRcvdCommodityReturning, clsMonthStats.HHRcvdCommodityNew, clsMonthStats.HHRcvdCommodity);
                fillTableSet(oWordDoc.Tables[4], 4, 6, clsMonthStats.HHRcvdStdReturning, clsMonthStats.HHRcvdStdNew, clsMonthStats.HHRcvdStd);
                fillTableSet(oWordDoc.Tables[4], 5, 6, clsMonthStats.HHRcvdSupplementalReturning, clsMonthStats.HHRcvdSupplementalNew, clsMonthStats.HHRcvdSupplemental);
                fillTableSet(oWordDoc.Tables[4], 6, 6, clsMonthStats.HHTotalServedReturning, clsMonthStats.HHTotalServedNew, clsMonthStats.HHTotalServed);
                fillTableSet(oWordDoc.Tables[4], 8, 6, clsMonthStats.HHRcvdBabyServicesReturning, clsMonthStats.HHRcvdBabyServicesNew, clsMonthStats.HHRcvdBabyServices);
                fillTableSet(oWordDoc.Tables[4], 9, 6, clsMonthStats.HHHomelessReturning, clsMonthStats.HHHomelessNew, clsMonthStats.HHHomeless);
                fillTableSet(oWordDoc.Tables[4], 10, 6, clsMonthStats.HHTransientReturning, clsMonthStats.HHTransientNew, clsMonthStats.HHTransient);
                fillTableSet(oWordDoc.Tables[4], 11, 6, clsMonthStats.HHSingleFemaleReturning, clsMonthStats.HHSingleFemaleNew, clsMonthStats.HHSingleFemale);
                fillTableSet(oWordDoc.Tables[4], 12, 6, clsMonthStats.HHSingleMaleReturning, clsMonthStats.HHSingleMaleNew, clsMonthStats.HHSingleMale);
                fillTableSet(oWordDoc.Tables[4], 13, 6, clsMonthStats.HHOneParentFemaleReturning, clsMonthStats.HHOneParentFemaleNew, clsMonthStats.HHOneParentFemale);
                fillTableSet(oWordDoc.Tables[4], 14, 6, clsMonthStats.HHOneParentMaleReturning, clsMonthStats.HHOneParentMaleNew, clsMonthStats.HHOneParentMale);
                //Pounds Info
                table = oWordDoc.Tables[5];
                table.Cell(2, 4).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.LbsStandard);
                table.Cell(3, 4).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.LbsOther);
                table.Cell(4, 4).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.LbsCommodity);
                table.Cell(5, 4).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.LbsSupplemental);
                table.Cell(6, 4).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.LbsBabySvc);
                table.Cell(7, 4).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.LbsTotalServed);
                table.Cell(8, 4).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.LbsNonFood);
                //Nbr Days Open
                table.Cell(9, 4).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.NbrDaysOpen);
                //Volunteers
                clsVolunteerStats.setYTDRow();
                oWordDoc.Tables[6].Cell(2, 4).Range.Text = CCFBGlobal.formatNumberWithCommas(clsVolunteerStats.NumVols);
                oWordDoc.Tables[6].Cell(3, 4).Range.Text = CCFBGlobal.formatNumberWithCommas(clsVolunteerStats.NumVolHours);

                //*********************NEED TO PUT THIS BACK IN ONCE KEN GETS LBSINKIND WORKING******************************
                //table = oWordDoc.Tables[7];
                //table.Cell(3, 5).Range.Text = CCFBGlobal.formatNumberWithCommas(clsMonthStats.GetDataValue["LbsInkind"]);
                

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

        private void fillTableSet(Table table, int row, int colStart, int v1, int v2, int v3)
        {
            table.Cell(row, colStart).Range.Text = CCFBGlobal.formatNumberWithCommas(v1);
            table.Cell(row, colStart + 1).Range.Text = CCFBGlobal.formatNumberWithCommas(v2);
            table.Cell(row, colStart + 2).Range.Text = CCFBGlobal.formatNumberWithCommas(v3);
        }
    }
}
