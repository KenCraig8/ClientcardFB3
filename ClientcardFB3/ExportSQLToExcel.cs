using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.Office.Interop.Excel;
using Microsoft.Win32;


namespace ClientcardFB3
{
    public partial class ExportSQLToExcel : Form
    {
        string sqlFileName = "";
        string sqlReportName = "";
        string fmtFileName = "";
        bool needtoset = true;

        public ExportSQLToExcel()
        {
            InitializeComponent();
            tbSQLQueryPath.Text = CCFBGlobal.getRegSQLQueryPath();
            tbSQLResultsPath.Text = CCFBGlobal.getRegSQLQueryResultsPath();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            createReport(Path.Combine(tbSQLResultsPath.Text,sqlReportName)
                        , sqlFileName, fmtFileName);
        }

        private void DoIt()
        {
            //SQLCMD -S . -d AdventureWorks2012 -Q “SELECT TOP 10 sp.BusinessEntityID, sp.TerritoryID, sp.SalesQuota, sp.Bonus, sp.CommissionPct FROM Sales.SalesPerson sp” -s “,” -o “e:\result.csv”        
        }
        public void createReport(string saveFileName, string sqlQueryPath, string fileNameFormat)
        {
            System.IO.StreamReader inputStream = new StreamReader(sqlQueryPath);
            string sqlCmdText = inputStream.ReadToEnd();
            inputStream.DiscardBufferedData();
            inputStream.Close();

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
                    if (fileNameFormat.Length > 4)
                    {
                        System.IO.StreamReader fileFormat = new StreamReader(fileNameFormat);
                        string txtFmt = "";
                        do
                        {
                            txtFmt = fileFormat.ReadLine();
                            string[] fmtdata = txtFmt.Split(']');
                            fmtdata[1] = fmtdata[1].TrimStart(' ');
                            switch (fmtdata[0].Replace("[", ""))
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
                                    tmp = fmtdata[1].Replace("1", lastRow);

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
                    }

                    xlsWorkBook.SaveAs(saveFileName, XlFileFormat.xlWorkbookNormal, null,
                    null, null, null, XlSaveAsAccessMode.xlNoChange, null, true, null, null,
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

        private string getQueryPath(string oriPath, bool showmakefolder)
        {
            try
            {
                folderBrowserDialog1.SelectedPath = oriPath;
                folderBrowserDialog1.ShowNewFolderButton = showmakefolder;
                folderBrowserDialog1.ShowDialog();
                return folderBrowserDialog1.SelectedPath;
                //////Set default extension of the savefile dialog
                ////saveFileDialog1.DefaultExt = "sql";
                //////If no value exists in registry for default save location
                ////if (String.IsNullOrEmpty(oriPath) == true)
                ////{
                ////    FileInfo fi = new FileInfo(System.Windows.Forms.Application.ExecutablePath);

                ////    //Get all drives on computer
                ////    DriveInfo[] drives = DriveInfo.GetDrives();

                ////    //Travers drives in reverse looking for a removeable drive with enough space on it for the backup file
                ////    for (int i = drives.Length - 1; i >= 0; i--)
                ////    {
                ////        if (drives[i].DriveType == DriveType.Removable && drives[i].AvailableFreeSpace > fi.Length)
                ////        {
                ////            oriPath = drives[i].Name;
                ////            break;
                ////        }
                ////    }
                ////}
                ////saveFileDialog1.InitialDirectory = Path.GetDirectoryName(oriPath);
                ////saveFileDialog1.FileName = sqlFileName;
                ////saveFileDialog1.Filter = "SQL Query Files (*.sql)|*.sql";
                ////DialogResult dr = saveFileDialog1.ShowDialog();

                //////If user confirmed save
                ////if (dr == System.Windows.Forms.DialogResult.OK)
                ////{
                ////    //Set the value of the path in the registry
                ////    Registry.SetValue(CCFBGlobal.registryKeyCurrentUser, CCFBGlobal.regsubkeySQLQueryPath, saveFileDialog1.FileName);
                ////    return saveFileDialog1.FileName;
                ////}
            }
            catch (Exception ex)
            {
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString());
            }
            return oriPath;
        }
        private void loadSQLFilesList()
        {
            listBox1.Items.Clear();
            initFileDisplay();
            DirectoryInfo dinfo = new DirectoryInfo(tbSQLQueryPath.Text);
            FileInfo[] Files = dinfo.GetFiles("*.sql");
            foreach (FileInfo file in Files)
            {
                listBox1.Items.Add(file.Name);
            }            
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listBox1.Focused == true)
            {
                initFileDisplay();
                sqlFileName = Path.Combine(tbSQLQueryPath.Text,listBox1.SelectedItem.ToString());
                sqlReportName = listBox1.SelectedItem.ToString().Replace(".sql", ".xls");
                try
                {
                    rtbSQL.LoadFile(sqlFileName, RichTextBoxStreamType.PlainText);
                    lblSqlFile.Text = listBox1.SelectedItem.ToString();
                    btnStart.Enabled = true;
                }
                catch (Exception ex)
                {
                    
                    MessageBox.Show("SQL File:" + sqlFileName + Environment.NewLine + "Error: " + ex.Message,"ERROR READING SQL TEXT FILE");
                }
                fmtFileName = Path.Combine(tbSQLQueryPath.Text,listBox1.SelectedItem.ToString().Replace(".sql",".fmt"));
                if (File.Exists(fmtFileName) == true)
                {
                    try
                    {
                        rtbFMT.LoadFile(fmtFileName, RichTextBoxStreamType.PlainText);
                        lblFormatFile.Text = listBox1.SelectedItem.ToString().Replace(".sql", ".fmt");
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show("Format File:" + fmtFileName + Environment.NewLine + "Error: " + ex.Message, "ERROR READING FMT TEXT FILE");
                        fmtFileName = "";
                    }
                }          
                else
                {
                    fmtFileName = "";
                }
            }
        }

        private void initFileDisplay()
        {
            btnStart.Enabled = false;
            lblFormatFile.Text = "";
            lblSqlFile.Text = "";
            rtbFMT.Text = "";
            rtbSQL.Text = "";
            btnSaveSQLChanges.Visible = false;
            btnSaveFormatChanges.Visible = false;
            System.Windows.Forms.Application.DoEvents();
        }

        private void rtbSQL_TextChanged(object sender, EventArgs e)
        {
            if (rtbSQL.Focused == true)
            {
                btnSaveSQLChanges.Visible = true;
            }
        }

        private void btnSaveSQLChanges_Click(object sender, EventArgs e)
        {
            saveTextFile(rtbSQL, sqlFileName);
            btnSaveSQLChanges.Visible = false;
        }

        private void btnSaveFormatChanges_Click(object sender, EventArgs e)
        {
            saveTextFile(rtbFMT, fmtFileName);
            btnSaveFormatChanges.Visible = false;
        }

        private void btnSaveFormatChanges_TextChanged(object sender, EventArgs e)
        {
            if (rtbFMT.Focused == true)
            {
                btnSaveFormatChanges.Visible = true;
            }
        }

        private void saveTextFile(RichTextBox rtb, string filename)
        {
            File.WriteAllText(filename, rtb.Text, Encoding.UTF8);
        }

        private void btnGetResultsPath_Click(object sender, EventArgs e)
        {
            tbSQLResultsPath.Text = getQueryPath(tbSQLResultsPath.Text, true);
        }

        private void btnGetQueryPath_Click(object sender, EventArgs e)
        {
            tbSQLQueryPath.Text = getQueryPath(tbSQLQueryPath.Text, false);
            loadSQLFilesList();
        }

        private void ExportSQLToExcel_Resize(object sender, EventArgs e)
        {
            if (needtoset == true)
            {
                splitContainer2.SplitterDistance = btnGetQueryPath.Left + btnGetQueryPath.Width + 4;
                splitContainer3.SplitterDistance = this.Width / 2;
                splitContainer1.SplitterDistance = tbSQLResultsPath.Top + tbSQLResultsPath.Height + btnStart.Height + 8;
                splitContainer4.SplitterDistance = splitContainer5.SplitterDistance = lblSqlFile.Height + btnSaveSQLChanges.Height;
                
                needtoset = false;
            }
        }

        private void ExportSQLToExcel_Load(object sender, EventArgs e)
        {
            splitContainer4.SplitterWidth = 1;
            splitContainer5.SplitterWidth = 1;
            loadSQLFilesList();
        }

        private void rtbFMT_TextChanged(object sender, EventArgs e)
        {
            if (rtbFMT.Focused == true)
            {
                btnSaveFormatChanges.Visible = true;
            }
        }
    }
}
