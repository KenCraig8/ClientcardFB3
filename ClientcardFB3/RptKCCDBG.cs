using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Sql;
using Microsoft.Office.Interop.Excel;

namespace ClientcardFB3
{
    class RptKCCDBG
    {
        System.Data.DataTable dtDemographics;
        System.Data.DataTable dtByZip;
        bool error = false;

        public bool Error
        {
            get
            {
                return error;
            }
        }

        public RptKCCDBG(System.Data.DataTable dtDemo, System.Data.DataTable dtZip)
        {
            dtDemographics = dtDemo;
            dtByZip = dtZip;
        }

        public void createExport(object saveAs, object templatePath, int year)
        {
            Application m_oExcelApp;
            //Workbooks m_oBooks;
            _Workbook m_oBook;
            _Worksheet m_oSheet;
            //Range excelRange;

            Object oTrue = true;
            Object oFalse = false;

            m_oExcelApp = new Application();
            m_oExcelApp.Visible = true;
            //m_oBooks = m_oExcelApp.Workbooks;
            m_oSheet = null;
            // m_oBooks = null;
            //excelRange = null;
            object oMissing = Type.Missing;

            Object oTemplatePath = templatePath;

            try
            {
                m_oBook = m_oExcelApp.Workbooks.Add(oTemplatePath);
                m_oSheet = (_Worksheet)m_oBook.Sheets[2];
                
                m_oSheet.Range["A1", oMissing].Value = CCFBPrefs.FoodBankName + " " + year;
                int col = 0;
                for (int i = 0; i < dtDemographics.Rows.Count; i++)
                {
                    col = i + 2;

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

                string zip = "";
                for (int i = 139; i < 188; i++)
                {
                    for (int j = 0; j < dtByZip.Rows.Count; j++)
                    {
                        zip = Convert.ToString(m_oSheet.Range["A" + i.ToString(), oMissing].Text);
                        if (zip.Trim() == dtByZip.Rows[j][0].ToString().Trim())
                        {
                            m_oSheet.Cells[i, 2] = dtByZip.Rows[j][1];
                            m_oSheet.Cells[i, 3] = dtByZip.Rows[j][2];
                            m_oSheet.Cells[i, 4] = dtByZip.Rows[j][3];
                            m_oSheet.Cells[i, 5] = dtByZip.Rows[j][4];
                        }
                    }
                }

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
