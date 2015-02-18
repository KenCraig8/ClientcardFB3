using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using Microsoft.Office.Interop.Excel;


namespace ClientcardFB3
{
    class CreateCAList
    {
        Household clsHHList = new Household(CCFBGlobal.connectionString);
        HHMembers clsHHmList = new HHMembers(CCFBGlobal.connectionString);
        System.Data.DataTable dtCAOrgs = new System.Data.DataTable("CAOrganization");
        Application m_oExcelApp;
        _Workbook m_oBook;
        _Worksheet m_oSheet;
        object oMissing = Type.Missing;
        bool error = false;

        public bool Error
        {
            get
            {
                return error;
            }
        }

        public CreateCAList(string hhWhereClause, string hhOrderBy)
        {
            SqlConnection sqlConn = new SqlConnection(CCFBGlobal.connectionString);
            SqlCommand sqlCmd = new System.Data.SqlClient.SqlCommand("SELECT * FROM CAOrganizations ORDER BY UID", sqlConn);
            SqlDataAdapter sqlAdpt = new SqlDataAdapter(sqlCmd);
            int nbrRows = sqlAdpt.Fill(dtCAOrgs);
        }

        public void createExport(object saveAs, object templatePath)
        {
            Object oTrue = true;
            Object oFalse = false;

            m_oExcelApp = new Application();
            m_oExcelApp.Visible = true;
            m_oSheet = null;
            object oMissing = Type.Missing;

            Object oTemplatePath = templatePath;

            try
            {
                m_oBook = m_oExcelApp.Workbooks.Add(oTemplatePath);

                fillSheet(2, "CAFlag = 1 AND InActive = 0 AND CAFoodBoxOnly = 1", "Name");
                fillSheet(3, "CAFlag = 1 AND InActive = 0 AND CAFoodBoxOnly = 0 AND CAFoodBoxRequest = 1", "Name");
                fillSheet(4, "CAFlag = 1 AND InActive = 0 AND CAFoodBoxOnly = 0 AND CAFoodBoxRequest = 0", "Name");
                fillSheet(1, "CAFlag = 1 AND InActive = 0", "Name");

                m_oBook.SaveAs(saveAs, XlFileFormat.xlExcel8, oMissing,
                oMissing, oMissing, oMissing, XlSaveAsAccessMode.xlShared, oMissing, oMissing, oMissing, oMissing,
                oMissing);



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

        private void fillSheet(int shtNbr, string sWhereClause, string sOrderBy)
        {
            try
            {
                int rowCurrent = 3;
                int rowCopyFrom = 6;
                int rowCopyTo = 9;
                int rowhhm = 0;
                int rowsToAdd;
                int nbrHhMembers = 0;
                string foodAdoptedBy = "";

                m_oSheet = (_Worksheet)m_oBook.Sheets[shtNbr];
                m_oSheet.Activate();
                clsHHList.openWhere(sWhereClause, sOrderBy);

                for (int i = 0; i < clsHHList.RowCount; i++)
                {
                    clsHHList.SetRecord(i);
                    clsHHmList.openWhere(" WHERE HouseholdID = " + clsHHList.ID.ToString() + " AND CAGiftFlag = 1 AND InActive = 0", "Age DESC", false);
                    rowsToAdd = 3;
                    if (rowCurrent >= rowCopyFrom)
                    {
                        m_oSheet.Rows[rowCopyFrom.ToString() + ":" + (rowCopyFrom + 2).ToString()].Select();
                        m_oExcelApp.Selection.Copy();
                        m_oSheet.Range["A" + rowCopyTo.ToString()].Select();
                        m_oBook.ActiveSheet.Paste();
                        rowCopyFrom = rowCopyTo;
                        rowCopyTo += 3;
                    }
                    for (int j = 2; j < clsHHmList.RowCount; j++)
                    {
                        string rowTxt = (rowCurrent + 1).ToString();
                        m_oSheet.Range[rowTxt + ":" + rowTxt].Copy();
                        m_oSheet.Range[rowTxt + ":" + rowTxt].Insert(XlInsertShiftDirection.xlShiftDown);
                        rowsToAdd++;
                        rowCopyFrom++;
                        rowCopyTo++;
                    }
                    System.Windows.Forms.Clipboard.Clear();
                    m_oSheet.Cells[rowCurrent, 1].Select();
                    m_oSheet.Cells[rowCurrent, 1] = clsHHList.Name;
                    m_oSheet.Cells[rowCurrent + 1, 1] = "[" + clsHHList.ID.ToString() + "]";
                    m_oSheet.Cells[rowCurrent + 1, 2] = clsHHList.Phone;
                    m_oSheet.Cells[rowCurrent, 3] = clsHHList.Address + System.Environment.NewLine + clsHHList.City + ", " + clsHHList.State + " " + clsHHList.Zipcode;
                    m_oSheet.Cells[rowCurrent, 4] = CCFBGlobal.YesNo(clsHHList.CAFoodBoxRequest);
                    if (clsHHList.CAFoodBoxRequest == true || clsHHList.CAFoodBoxOnly == true)
                    {
                        foodAdoptedBy = "";
                        int itmp = clsHHList.CAAdoptedBy;
                        int itst = 0;
                        foreach (DataRow item in dtCAOrgs.Rows)
                        {
                            itst = Convert.ToInt32(item["UID"]);
                            if (itst == itmp)
                            {
                                foodAdoptedBy = item["OrgName"].ToString();
                                break;
                            }
                        }
                        m_oSheet.Cells[rowCurrent, 5] = foodAdoptedBy;
                    }
                    if (clsHHList.CAFoodBoxOnly == true)
                    {
                        m_oSheet.Cells[rowCurrent + 1, 4] = "FOOD ONLY";
                    }
                    else
                    {
                        rowhhm = rowCurrent;
                        nbrHhMembers += clsHHmList.RowCount;
                        for (int j = 0; j < clsHHmList.RowCount; j++)
                        {
                            clsHHmList.SetRecord(j);
                            rowhhm = rowCurrent + j;
                            m_oSheet.Cells[rowhhm, 6] = clsHHmList.FirstName + " " + clsHHmList.LastName;
                            m_oSheet.Cells[rowhhm, 7] = clsHHmList.Age;
                            m_oSheet.Cells[rowhhm, 8] = clsHHmList.Sex;
                            m_oSheet.Cells[rowhhm, 9] = clsHHmList.CASize;
                            m_oSheet.Cells[rowhhm, 10] = clsHHmList.CAGiftIdeas;
                            m_oSheet.Cells[rowhhm, 11] = clsHHmList.CAHobbies;
                            m_oSheet.Cells[rowhhm, 13] = clsHHmList.CAAdoptedName;
                            foodAdoptedBy = "";
                            int itmp = clsHHmList.CAAdoptedBy;
                            int itst = 0;
                            foreach (DataRow item in dtCAOrgs.Rows)
                            {
                                itst = Convert.ToInt32(item["UID"]);
                                if (itst == itmp)
                                {
                                    foodAdoptedBy = item["OrgName"].ToString();
                                    break;
                                }
                            }
                            m_oSheet.Cells[rowhhm, 12] = foodAdoptedBy;
                        }
                    }
                    rowCurrent += rowsToAdd;
                }
                m_oSheet.Cells[rowCurrent, 1] = clsHHList.RowCount;
                m_oSheet.Cells[rowCurrent, 2] = "Total Households";
                m_oSheet.Cells[rowCurrent + 1, 1] = nbrHhMembers;
                m_oSheet.Cells[rowCurrent + 1, 2] = "Total Family Members";

            }
            catch (Exception ex)
            {
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString());
            }
        }
    }
}
