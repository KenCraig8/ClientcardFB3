using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.Office.Interop.Excel;
using System.Data.SqlClient;

namespace ClientcardFB3
{
    public partial class TestExportToCSV : Form
    {

        public TestExportToCSV()
        {
            InitializeComponent();
        }

        private void DoIt()
        {
            //SQLCMD -S . -d AdventureWorks2012 -Q “SELECT TOP 10 sp.BusinessEntityID, sp.TerritoryID, sp.SalesQuota, sp.Bonus, sp.CommissionPct FROM Sales.SalesPerson sp” -s “,” -o “e:\result.csv”        
        }
        public void createReport(string saveFileName, string basePath, string sqlQueryPath, string fileNameFormat)
        {
            System.IO.StreamReader inputStream = new StreamReader(basePath + sqlQueryPath);
            string sqlCmdText = inputStream.ReadToEnd();
            inputStream.DiscardBufferedData();
            inputStream.Close();
            inputStream.Dispose();

            SqlConnection sqlConn = new SqlConnection(CCFBGlobal.connectionString);
            sqlConn.Open();
            SqlCommand sqlCmd = new SqlCommand(sqlCmdText, sqlConn);
            SqlDataAdapter sqlDataAdpt = new SqlDataAdapter(sqlCmd);

            System.Data.DataTable tblWork = new System.Data.DataTable();
            int nbrRows= sqlDataAdpt.Fill(tblWork);
            if (nbrRows > 0)
            {

                Microsoft.Office.Interop.Excel.Application appExcel = new Microsoft.Office.Interop.Excel.Application();
                appExcel.Visible = true;

                Microsoft.Office.Interop.Excel._Workbook xlsWorkBook;
                Microsoft.Office.Interop.Excel._Worksheet xlsSheet;
 
                xlsSheet = null;
                object missing = Type.Missing;

                try
                {
                    xlsWorkBook = appExcel.Workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
                    xlsSheet = (_Worksheet)xlsWorkBook.ActiveSheet;
                    int idxCol = 1;
                    foreach (DataColumn dCol in tblWork.Columns)
                    {
                        xlsSheet.Cells[1, dCol.Ordinal + 1] = dCol.Caption;
                    }
                    int newRow = 1;
                    foreach (DataRow dRow in tblWork.Rows)
                    {
                        newRow ++;
                        foreach (DataColumn dCol in tblWork.Columns)
                        {
                            idxCol = dCol.Ordinal + 1;
                            switch (dCol.DataType.ToString())
                            {
                                case "System.String":
                                    xlsSheet.Cells[newRow, idxCol] = dRow[dCol.Ordinal].ToString();
                                    break;
                                case "System.Int32":
                                    xlsSheet.Cells[newRow, idxCol] = Convert.ToInt32(dRow[dCol.Ordinal]);
                                    break;
                                default:
                                    xlsSheet.Cells[1, dCol.Ordinal] = dCol.Caption;
                                    break;
                            }
                        }
                        
                    }
                    //excelRange = (Microsoft.Office.Interop.Excel.Range)appExcel.Selection;
                    //excelRange.Columns.AutoFit();
                    string tmp = "A:" + CCFBGlobal.ExcelColumnLetter(tblWork.Columns.Count);
                    appExcel.ActiveSheet.Columns(tmp).EntireColumn.AutoFit();
                    appExcel.ActiveSheet.Rows("1:1").Select();
                    appExcel.Selection.Font.Bold = true;
                    appExcel.ActiveSheet.PageSetup.PrintTitleRows = "$1:$1";
                    appExcel.ActiveSheet.PageSetup.PrintTitleColumns = "";

                    System.IO.StreamReader fileFormat = new StreamReader(basePath + fileNameFormat);
                    string txtFmt = "";
                    do 
                    {
                        txtFmt = fileFormat.ReadLine();
                        string [] fmtdata = txtFmt.Split(']');
                        fmtdata[1] = fmtdata[1].TrimStart(' ');
                        switch (fmtdata[0].Replace("[",""))
                        {
                            case "Border":
                                DoBorder(appExcel, fmtdata[1], fmtdata[2]);
                                break;
                            case "ColumnTotals":
                                doColumnTotals(appExcel, fmtdata[1]);
                                break;
                            case "ColumnAlignCenter":
                                doColumnAlignCenter(appExcel, fmtdata[1]);
                                break;
                            case "CenterHeaderText":
                                appExcel.ActiveSheet.PageSetup.CenterHeader = fmtdata[1];
                                break;
                            case "CenterFooterText":
                                appExcel.ActiveSheet.PageSetup.CenterFooter = fmtdata[1];
                                break;
                            case "Filename":
                                saveFileName = fmtdata[1];
                                break;
                            case "Margins":
                                string[] sMargins = fmtdata[1].Split(',');
                                appExcel.ActiveSheet.PageSetup.LeftMargin = appExcel.Application.InchesToPoints(Convert.ToDouble(sMargins[0]));
                                appExcel.ActiveSheet.PageSetup.RightMargin = appExcel.Application.InchesToPoints(Convert.ToDouble(sMargins[1]));
                                appExcel.ActiveSheet.PageSetup.TopMargin = appExcel.Application.InchesToPoints(Convert.ToDouble(sMargins[2]));
                                appExcel.ActiveSheet.PageSetup.BottomMargin = appExcel.Application.InchesToPoints(Convert.ToDouble(sMargins[3]));
                                appExcel.ActiveSheet.PageSetup.HeaderMargin = appExcel.Application.InchesToPoints(Convert.ToDouble(sMargins[4]));
                                appExcel.ActiveSheet.PageSetup.FooterMargin = appExcel.Application.InchesToPoints(Convert.ToDouble(sMargins[5]));
                                break;
                            case "Orientation":
                                if (fmtdata[1] == "Landscape")
                                {
                                    appExcel.ActiveSheet.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlLandscape;
                                }
                                else
                                {
                                    appExcel.ActiveSheet.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlPortrait;
                                }
                                break;
                            case "PaperSize":
                                if (fmtdata[1] == "Legal")
                                {
                                    appExcel.ActiveSheet.PageSetup.PaperSize = Microsoft.Office.Interop.Excel.XlPaperSize.xlPaperLegal;
                                }
                                else
                                {
                                    appExcel.ActiveSheet.PageSetup.PaperSize = Microsoft.Office.Interop.Excel.XlPaperSize.xlPaperLetter;
                                }
                                break;
                            case "RepeatHeader":
                                appExcel.ActiveCell.SpecialCells(Microsoft.Office.Interop.Excel.XlCellType.xlCellTypeLastCell).Select();
                                string lastRow = (appExcel.ActiveCell.Row + 1).ToString();
                                tmp = fmtdata[1].Replace("1",lastRow);
                                
                                appExcel.ActiveSheet.Range(fmtdata[1]).Select();
                                appExcel.Selection.Copy();
                                appExcel.ActiveSheet.Range(tmp).Select();
                                appExcel.ActiveSheet.Paste();

                                break;
                            case "RightFooterText":
                                appExcel.ActiveSheet.PageSetup.RightFooter = fmtdata[1];
                                break;
                            case "Sheetname":
                                appExcel.ActiveSheet.Name = fmtdata[1];
                                break;
                            case "Zoom":
                                appExcel.ActiveSheet.PageSetup.Zoom = Convert.ToInt32(fmtdata[1]);
                                break;

                            default:
                                break;
                        }
                    } while (fileFormat.EndOfStream == false);
                    fileFormat.DiscardBufferedData();
                    fileFormat.Close();
                    fileFormat.Dispose();
                    //for (int j = 1; j <= tblWork.Columns.Count; j++)
                    //{
                    //    xlsSheet.Columns("C:C").Select;
                    //    excelRange.EntireColumn.AutoFit();
                    //}

                    xlsWorkBook.SaveAs(saveFileName, XlFileFormat.xlWorkbookNormal, null,
                    null, null, null, XlSaveAsAccessMode.xlShared, null, null, null, null,
                    null);

                    appExcel.Quit();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(appExcel);
                    xlsSheet = null;
                    xlsWorkBook = null;
                    appExcel = null;
                    //error = false;

                    CCFBGlobal.openDocumentOutsideCCFB(saveFileName.ToString());
                }
                catch (Exception ex)
                {
                    appExcel.Quit();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(appExcel);
                    CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString());
                    //error = true;
                    xlsSheet = null;
                    xlsWorkBook = null;
                    appExcel = null;
                    //MessageBox.Show("Error accessing Excel: " + ex.ToString());
                }
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            createReport(@"C:\ClientcardFB3\Log\Temp.xls", tbRptQueryPath.Text, tbSqlTextFile.Text, tbFormatFile.Text);
        }

        private void DoBorder(Microsoft.Office.Interop.Excel.Application app, string fmt, string srange)
        {
            if (fmt == "Standard")
            {
                //app.ActiveSheet.Range("A1").Select();
                app.ActiveSheet.Range("A1", app.ActiveCell.SpecialCells(Microsoft.Office.Interop.Excel.XlCellType.xlCellTypeLastCell)).Select();
                app.Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlDiagonalDown).LineStyle = Microsoft.Office.Interop.Excel.Constants.xlNone;
                app.Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlDiagonalUp).LineStyle = Microsoft.Office.Interop.Excel.Constants.xlNone;
                
                drawLine(app, Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft, Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium);
                drawLine(app, Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop, Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium);
                drawLine(app, Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom, Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium);
                drawLine(app, Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight, Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium);
                drawLine(app, Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideVertical, Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin);
                drawLine(app, Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideHorizontal, Microsoft.Office.Interop.Excel.XlBorderWeight.xlHairline);
            }
            else if (fmt == "LastRow")
            {
                string[] brange = srange.Split(':');
                app.ActiveCell.SpecialCells(Microsoft.Office.Interop.Excel.XlCellType.xlCellTypeLastCell).Select();
                string lastRow = app.ActiveCell.Row.ToString();
                string tmp = brange[0] + lastRow + ":" + brange[1] + lastRow;
                app.ActiveSheet.Range(tmp).Select();
                app.Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlDiagonalDown).LineStyle = Microsoft.Office.Interop.Excel.Constants.xlNone;
                app.Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlDiagonalUp).LineStyle = Microsoft.Office.Interop.Excel.Constants.xlNone;
                app.Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideHorizontal).LineStyle = Microsoft.Office.Interop.Excel.Constants.xlNone;

                drawLine(app, Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft, Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium);
                drawLine(app, Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop, Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium);
                drawLine(app, Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom, Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium);
                drawLine(app, Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight, Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium);
                drawLine(app, Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideVertical, Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin);
            }
        }

        private void doColumnTotals(Microsoft.Office.Interop.Excel.Application app, string fmt)
        {
            app.ActiveCell.SpecialCells(Microsoft.Office.Interop.Excel.XlCellType.xlCellTypeLastCell).Select();
            int lastRow = app.ActiveCell.Row;
            //=SUM(H2:H51)
            string[] srange = fmt.Split(':');
            string tmp = "";
            foreach (string sCol in srange)
            {
                tmp = sCol + (lastRow + 1).ToString();
                app.ActiveSheet.Range(tmp).Select();
                tmp = "=SUM(" + sCol + "2:" + sCol + lastRow.ToString() + ")";
                app.ActiveCell.Formula = tmp;
            }
        }

        private void doColumnAlignCenter(Microsoft.Office.Interop.Excel.Application app, string fmt)
        {
            app.ActiveSheet.Range(fmt).Select();
            app.Selection.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            app.Selection.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignBottom;
            app.Selection.WrapText = false;
            app.Selection.Orientation = 0;
            app.Selection.AddIndent = false;
            app.Selection.IndentLevel = 0;
            app.Selection.ShrinkToFit = false;
            app.Selection.ReadingOrder = Microsoft.Office.Interop.Excel.Constants.xlContext;
            app.Selection.MergeCells = false;
        }

        private void drawLine(Microsoft.Office.Interop.Excel.Application app, Microsoft.Office.Interop.Excel.XlBordersIndex idxEdge, Microsoft.Office.Interop.Excel.XlBorderWeight borderweight)
        {
            app.Selection.Borders(idxEdge).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            app.Selection.Borders(idxEdge).ColorIndex = 0;
            app.Selection.Borders(idxEdge).TintAndShade = 0;
            app.Selection.Borders(idxEdge).Weight = borderweight;
        }
    }
}
