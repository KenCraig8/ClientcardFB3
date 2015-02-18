using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Sql;
using Microsoft.Office.Interop.Excel;

namespace ClientcardFB3
{
    class RptCDBGWorksheet
    {
        System.Data.DataTable dtDemographics;
        System.Data.DataTable dtByZip;
        System.Data.DataTable dtPM;
        bool error = false;

        public bool Error
        {
            get
            {
                return error;
            }
        }

        public RptCDBGWorksheet(System.Data.DataTable dtDemo, System.Data.DataTable dtZip, System.Data.DataTable dtPerfMeasures)
        {
            dtDemographics = dtDemo;
            dtByZip = dtZip;
            dtPM = dtPerfMeasures;
        }

        public void createExport(object saveAs, object templatePath, int year)
        {
            Application m_oExcelApp;
            _Workbook m_oBook;
            _Worksheet m_oSheet;

            Object oTrue = true;
            Object oFalse = false;
            Object oMissing = Type.Missing;

            m_oExcelApp = new Application();
            m_oExcelApp.Visible = true;
            m_oSheet = null;
            

            Object oTemplatePath = templatePath;

            try
            {
                m_oBook = m_oExcelApp.Workbooks.Add(oTemplatePath);
                CCFBGlobal.DeleteFile(saveAs.ToString());
                m_oBook.SaveAs(saveAs, XlFileFormat.xlWorkbookNormal, null,
                null, null, null, XlSaveAsAccessMode.xlShared, null, oTrue, null, null,
                null);

                int col = 0;
                int row = 0;
                m_oSheet = (_Worksheet)m_oBook.Sheets[1];
                m_oSheet.Activate();

                m_oSheet.Range["A1", oMissing].Value = CCFBPrefs.FoodBankName + " " + year;
                
                for (int i = 0; i < dtPM.Rows.Count; i++)
                {
                    col = Convert.ToInt32(dtPM.Rows[i]["CalQuarter"]) + 1;
                    row = Convert.ToInt32(dtPM.Rows[i]["mymonth"]) + 4;

                    m_oSheet.Cells[row, col] = dtPM.Rows[i]["unduplHH"];
                    m_oSheet.Cells[row + 6, col] = dtPM.Rows[i]["unduplIndv"];
                    m_oSheet.Cells[row + 12, col] = dtPM.Rows[i]["totIndv"];
                    m_oSheet.Cells[row + 18, col] = dtPM.Rows[i]["totLbs"];
                }

                m_oSheet = (_Worksheet)m_oBook.Sheets[2];
                m_oSheet.Activate();

                m_oSheet.Range["A1", oMissing].Value = CCFBPrefs.FoodBankName + " " + year;
                
                for (int i = 0; i < dtDemographics.Rows.Count; i++)
                {
                    col = Convert.ToInt32(dtDemographics.Rows[i]["CalQuarter"].ToString().Substring(4)) + 1;

                    //Service #'s:
                    m_oSheet.Cells[4, col] = dtDemographics.Rows[i]["TotalHouseholds"];
                    m_oSheet.Cells[5, col] = dtDemographics.Rows[i]["TotalIndividuals"];

                    //Unincorporated Area Residence
                    m_oSheet.Cells[11, col] = dtDemographics.Rows[i]["HHInCityLimits"];
                    m_oSheet.Cells[12, col] = dtDemographics.Rows[i]["HHOutsideCityLimits"];
                    m_oSheet.Cells[13, col] = dtDemographics.Rows[i]["HHOutsideCounty"];
                    m_oSheet.Cells[14, col] = dtDemographics.Rows[i]["HHNoZipCode"]; ;

                    //Household Composition
                    //A. Single Person Households (HH size = 1)
                    m_oSheet.Cells[19, col] = dtDemographics.Rows[i]["HHSingleFemale"];
                    m_oSheet.Cells[20, col] = dtDemographics.Rows[i]["HHSingleMale"];
                    m_oSheet.Cells[21, col] = dtDemographics.Rows[i]["HHSingleMinor"];

                    //B. Households with Minors Under 18
                    m_oSheet.Cells[24, col] = dtDemographics.Rows[i]["HHOneParentFemale"];
                    m_oSheet.Cells[25, col] = dtDemographics.Rows[i]["HHOneParentMale"];
                    m_oSheet.Cells[26, col] = dtDemographics.Rows[i]["HHTwoParent"];
                    m_oSheet.Cells[27, col] = dtDemographics.Rows[i]["HHOtherRelated"];

                    //C. Shared Adult Households with No Minors
                    m_oSheet.Cells[28, col] = dtDemographics.Rows[i]["HHPartnered"];
                    m_oSheet.Cells[29, col] = dtDemographics.Rows[i]["HHOtherRelated"];

                    //Household Income Level
                    m_oSheet.Cells[37, col] = dtDemographics.Rows[i]["HHIncomeVeryLow"];
                    m_oSheet.Cells[38, col] = dtDemographics.Rows[i]["HHIncomeLow"];
                    m_oSheet.Cells[39, col] = dtDemographics.Rows[i]["HHIncomeModerate"];
                    m_oSheet.Cells[40, col] = dtDemographics.Rows[i]["HHIncomeAboveModerate"];

                    //Information by Individual
                    //Homeless
                    m_oSheet.Cells[47, col] = dtDemographics.Rows[i]["IndHomelessYes"];
                    m_oSheet.Cells[48, col] = dtDemographics.Rows[i]["IndHomelessNo"];
                    m_oSheet.Cells[49, col] = Convert.ToInt32(dtDemographics.Rows[i]["TotalIndividuals"]) - (Convert.ToInt32(dtDemographics.Rows[i]["IndHomelessYes"]) + Convert.ToInt32(dtDemographics.Rows[i]["IndHomelessNo"]));

                    //Gender
                    m_oSheet.Cells[68, col] = dtDemographics.Rows[i]["IndFemale"];
                    m_oSheet.Cells[69, col] = dtDemographics.Rows[i]["IndMale"];
                    m_oSheet.Cells[70, col] = dtDemographics.Rows[i]["IndGenderOther"];
                    m_oSheet.Cells[71, col] = dtDemographics.Rows[i]["IndGenderUnknown"];

                    //Persons with Disabilities
                    m_oSheet.Cells[75, col] = dtDemographics.Rows[i]["IndDisabilitiesYes"];
                    m_oSheet.Cells[76, col] = dtDemographics.Rows[i]["IndDisabilitiesNo"];
                    m_oSheet.Cells[77, col] = Convert.ToInt32(dtDemographics.Rows[i]["TotalIndividuals"]) - (Convert.ToInt32(dtDemographics.Rows[i]["IndDisabilitiesYes"]) + Convert.ToInt32(dtDemographics.Rows[i]["IndDisabilitiesNo"])); ;

                    //Refugee/Immigrant
                    m_oSheet.Cells[81, col] = dtDemographics.Rows[i]["IndRefugeeYes"];
                    m_oSheet.Cells[82, col] = dtDemographics.Rows[i]["IndRefugeeNo"];
                    m_oSheet.Cells[83, col] = dtDemographics.Rows[i]["IndRefugeeUnknown"];

                    //Limited English Proficiency
                    m_oSheet.Cells[87, col] = dtDemographics.Rows[i]["IndLimitedEnglishYes"];
                    m_oSheet.Cells[88, col] = dtDemographics.Rows[i]["IndLimitedEnglishNo"];
                    m_oSheet.Cells[89, col] = dtDemographics.Rows[i]["IndLimitedEnglishUnknown"];

                    //Employment Status at Intake
                    m_oSheet.Cells[92, col] = dtDemographics.Rows[i]["IndEmployedFullTime"];
                    m_oSheet.Cells[93, col] = dtDemographics.Rows[i]["IndEmployedPartTime"];
                    m_oSheet.Cells[94, col] = dtDemographics.Rows[i]["IndEmployedSeasonal"];
                    m_oSheet.Cells[95, col] = dtDemographics.Rows[i]["IndEmployedSeeking"];
                    m_oSheet.Cells[96, col] = dtDemographics.Rows[i]["IndEmployedNotSeeking"];
                    m_oSheet.Cells[97, col] = dtDemographics.Rows[i]["IndEmployedUnknown"];

                    //Education Level for Adults
                    m_oSheet.Cells[102, col] = dtDemographics.Rows[i]["IndEducationLessThanHS"];
                    m_oSheet.Cells[103, col] = dtDemographics.Rows[i]["IndEducationHSGraduate"];
                    m_oSheet.Cells[104, col] = dtDemographics.Rows[i]["IndEducationSomeCollege"];
                    m_oSheet.Cells[105, col] = dtDemographics.Rows[i]["IndEducationCertificate"];
                    m_oSheet.Cells[106, col] = dtDemographics.Rows[i]["IndEducationAssociate"];
                    m_oSheet.Cells[107, col] = dtDemographics.Rows[i]["IndEducationBachelors"];
                    m_oSheet.Cells[108, col] = dtDemographics.Rows[i]["IndEducationUnder18"];
                    m_oSheet.Cells[109, col] = dtDemographics.Rows[i]["IndEducationUnknown"];

                    //Veterans/Military Status
                    m_oSheet.Cells[113, col] = dtDemographics.Rows[i]["IndMilitaryYes"];
                    m_oSheet.Cells[114, col] = dtDemographics.Rows[i]["IndMilitaryPartner"];
                    m_oSheet.Cells[115, col] = dtDemographics.Rows[i]["IndMilitaryMinor"];
                    m_oSheet.Cells[116, col] = dtDemographics.Rows[i]["IndMilitaryNo"];
                    m_oSheet.Cells[117, col] = dtDemographics.Rows[i]["IndMilitaryUnknown"];

                    //Veterans Discharge Status
                    m_oSheet.Cells[121, col] = dtDemographics.Rows[i]["IndMilitaryHonorable"];
                    m_oSheet.Cells[122, col] = dtDemographics.Rows[i]["IndMilitaryGeneral"];
                    m_oSheet.Cells[123, col] = dtDemographics.Rows[i]["IndMilitaryMedical"];
                    m_oSheet.Cells[124, col] = dtDemographics.Rows[i]["IndMilitaryBadConduct"];
                    m_oSheet.Cells[125, col] = dtDemographics.Rows[i]["IndMilitaryDisHonorable"];
                    m_oSheet.Cells[126, col] = dtDemographics.Rows[i]["IndMilitaryActive"];
                    m_oSheet.Cells[127, col] = dtDemographics.Rows[i]["IndMilitaryUnknown"];

                    //Long Term Homeless
                    m_oSheet.Cells[131, col] = dtDemographics.Rows[i]["IndLongTermHomelessYes"];
                    m_oSheet.Cells[132, col] = dtDemographics.Rows[i]["IndLongTermHomelessUnknown"];

                    //Chronically Homeless
                    m_oSheet.Cells[135, col] = dtDemographics.Rows[i]["IndChronicallyHomelessYes"];
                    m_oSheet.Cells[136, col] = dtDemographics.Rows[i]["IndChronicallyHomelessUnknown"];
                }

                //string zip = "";
                int rowFirst = 139; 
                //int rowLast = 187;
                int rowCurrent = 0;
                for (int rowData = 0; rowData < dtByZip.Rows.Count; rowData++)
                {
                    rowCurrent = rowFirst + rowData;
                    //zip = Convert.ToString(m_oSheet.Range["A" + (rowFirst+rowData).ToString(), oMissing].Text);
                    //if (zip.Trim() == dtByZip.Rows[rowData][0].ToString().Trim())
                    //{
                    m_oSheet.Cells[rowCurrent, 1] = dtByZip.Rows[rowData][0];
                    m_oSheet.Cells[rowCurrent, 2] = dtByZip.Rows[rowData][1];
                    m_oSheet.Cells[rowCurrent, 3] = dtByZip.Rows[rowData][2];
                    m_oSheet.Cells[rowCurrent, 4] = dtByZip.Rows[rowData][3];
                    m_oSheet.Cells[rowCurrent, 5] = dtByZip.Rows[rowData][4];
                    m_oSheet.Range[m_oSheet.Cells[rowCurrent, 1],m_oSheet.Cells[rowCurrent, 5]].Select();
                    m_oExcelApp.Selection.EntireRow.Hidden = false;
                    //}
                }

                m_oSheet = (_Worksheet)m_oBook.Sheets[3];
                m_oSheet.Activate();
                
                m_oSheet.Range["A1", oMissing].Value = CCFBPrefs.FoodBankName + " " + year;
                col = 0;
                for (int i = 0; i < dtDemographics.Rows.Count; i++)
                {
                    col = Convert.ToInt32(dtDemographics.Rows[i]["CalQuarter"].ToString().Substring(4)) * 2;
                    m_oSheet.Cells[4, col] = Convert.ToInt32(dtDemographics.Rows[i]["White"]) + Convert.ToInt32(dtDemographics.Rows[i]["WhiteHispanic"]);
                    m_oSheet.Cells[4, col + 1] = Convert.ToInt32(dtDemographics.Rows[i]["WhiteHispanic"]);

                    m_oSheet.Cells[5, col] = Convert.ToInt32(dtDemographics.Rows[i]["Black"]) + Convert.ToInt32(dtDemographics.Rows[i]["BlackHispanic"]);
                    m_oSheet.Cells[5, col + 1] = Convert.ToInt32(dtDemographics.Rows[i]["BlackHispanic"]);

                    m_oSheet.Cells[6, col] = Convert.ToInt32(dtDemographics.Rows[i]["Asian"]) + Convert.ToInt32(dtDemographics.Rows[i]["AsianHispanic"]);
                    m_oSheet.Cells[6, col + 1] = Convert.ToInt32(dtDemographics.Rows[i]["AsianHispanic"]);

                    m_oSheet.Cells[7, col] = Convert.ToInt32(dtDemographics.Rows[i]["Native"]) + Convert.ToInt32(dtDemographics.Rows[i]["NativeHispanic"]);
                    m_oSheet.Cells[7, col + 1] = Convert.ToInt32(dtDemographics.Rows[i]["NativeHispanic"]);

                    m_oSheet.Cells[8, col] = Convert.ToInt32(dtDemographics.Rows[i]["Pacific"]) + Convert.ToInt32(dtDemographics.Rows[i]["PacificHispanic"]);
                    m_oSheet.Cells[8, col + 1] = Convert.ToInt32(dtDemographics.Rows[i]["PacificHispanic"]);

                    m_oSheet.Cells[9, col] = Convert.ToInt32(dtDemographics.Rows[i]["WhiteNative"]) + Convert.ToInt32(dtDemographics.Rows[i]["WhiteNativeHispanic"]);
                    m_oSheet.Cells[9, col + 1] = Convert.ToInt32(dtDemographics.Rows[i]["WhiteNativeHispanic"]);

                    m_oSheet.Cells[10, col] = Convert.ToInt32(dtDemographics.Rows[i]["WhiteAsian"]) + Convert.ToInt32(dtDemographics.Rows[i]["WhiteAsianHispanic"]);
                    m_oSheet.Cells[10, col + 1] = Convert.ToInt32(dtDemographics.Rows[i]["WhiteAsianHispanic"]);

                    m_oSheet.Cells[11, col] = Convert.ToInt32(dtDemographics.Rows[i]["WhiteBlack"]) + Convert.ToInt32(dtDemographics.Rows[i]["WhiteBlackHispanic"]);
                    m_oSheet.Cells[11, col + 1] = Convert.ToInt32(dtDemographics.Rows[i]["WhiteBlackHispanic"]);

                    m_oSheet.Cells[12, col] = Convert.ToInt32(dtDemographics.Rows[i]["BlackNative"]) + Convert.ToInt32(dtDemographics.Rows[i]["BlackNativeHispanic"]);
                    m_oSheet.Cells[12, col + 1] = Convert.ToInt32(dtDemographics.Rows[i]["BlackNativeHispanic"]);

                    m_oSheet.Cells[13, col] = Convert.ToInt32(dtDemographics.Rows[i]["Other"]) + Convert.ToInt32(dtDemographics.Rows[i]["OtherHispanic"]);
                    m_oSheet.Cells[13, col + 1] = Convert.ToInt32(dtDemographics.Rows[i]["OtherHispanic"]);

                    m_oSheet.Cells[14, col] = Convert.ToInt32(dtDemographics.Rows[i]["Undisclosed"]) + Convert.ToInt32(dtDemographics.Rows[i]["UndisclosedHispanic"]);
                    m_oSheet.Cells[14, col + 1] = Convert.ToInt32(dtDemographics.Rows[i]["UndisclosedHispanic"]);
                }


                m_oBook.Save();



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
