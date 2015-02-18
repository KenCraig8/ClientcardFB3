using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;

namespace ClientcardFB3
{
    public partial class SignFamilyCard : Form
    {
        Household clsHH;
        HHMembers clsHhM;
        Microsoft.Office.Interop.Word.Application oWord = new Microsoft.Office.Interop.Word.Application();
        Document oWordDoc = new Document();
        Object oMissing = System.Reflection.Missing.Value;
        Object oFalse = false;
        Object oTrue = true;
        const string constSigFile = @"C:\ClientcardFB3\Log\Sig.bmp";
        string idText;
        string filename = "";
        object fullsavepath = "";
        string savePath = "";

        public SignFamilyCard(Client clsClientIn)
        {
            InitializeComponent();
            clsHH = clsClientIn.clsHH;
            clsHhM = clsClientIn.clsHHmem;
            idText = CCFBGlobal.formatNumberWithSixLeadingZeros(clsHH.ID);
            if (sigPadInputCtrl1.initSigPad() == true)
            {
                sigPadInputCtrl1.ClearPromptList();
                SqlConnection conn = new SqlConnection(CCFBGlobal.connectionString);
                conn.Open();
                SqlCommand sqlCmd = new SqlCommand("SELECT PromptText, RightButtonText FROM SignaturePrompts WHERE PromptGroup = 2 ORDER BY UID", conn);
                sqlCmd.CommandType = CommandType.Text;
                SqlDataReader reader = sqlCmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        sigPadInputCtrl1.AddPromptItem(reader.GetString(0), reader.GetString(1));
                    }
                }
                conn.Close();
            }
            //btnSave.Enabled = false;
        }

        public void createReport(string foodBankName, string templatePath)
        {
            savePath = CCFBGlobal.pathFamilyCards 
                                + DateTime.Today.Year.ToString() + "\\" 
                                + idText.Substring(0, 2) + "\\" 
                                + idText.Substring(2, 2) + "\\";
            CCFBGlobal.verifyPath(savePath);
            fullsavepath = savePath + "tmp";

            if (File.Exists(templatePath) == false)
                templatePath = CCFBGlobal.fb3TemplatesPath + "FamilyCardSigPadENG.doc";
            if (File.Exists(templatePath))
            {
                Object missing = System.Reflection.Missing.Value;
                oWord.Visible = true;
                Object oTemplatePath = templatePath;

                try
                {
                    oWordDoc = oWord.Documents.Add(ref oTemplatePath, ref oMissing, ref oMissing, ref oMissing);
                    oWordDoc.SaveAs2(fullsavepath, ref oMissing);

                    fillBookMark("FoodBankName", foodBankName);
                    fillBookMark("FBName1", foodBankName);
                    fillBookMark("FBName2", foodBankName);
                    fillBookMark("Date", DateTime.Today.ToShortDateString());
                    fillBookMark("clientID", clsHH.ID.ToString());

                    Table table = oWordDoc.Tables[1];
                    //table.Cell(1, 1).Range.Text = clsHH.Name;
                    string fullAddress = clsHH.Address;
                    if (clsHH.AptNbr.Trim().Length >0)
                    {
                        fullAddress += "  Unit " + clsHH.AptNbr.Trim();
                    }
                    table.Cell(3, 1).Range.Text = fullAddress;
                    table.Cell(5, 1).Range.Text = clsHH.City + ", " + clsHH.State;
                    table.Cell(5, 2).Range.Text = clsHH.Zipcode;
                    int row = 2;
                    Table owdtblFamilyList = oWordDoc.Tables[2];
                    for (int i = 0; i < clsHhM.RowCount; i++)
                    {
                        clsHhM.SetRecord(i);
                        if (clsHhM.HeadHH == true)
                        {
                            if (clsHhM.UseAge == false)
                                table.Cell(7, 1).Range.Text = CCFBGlobal.ValidDateString(clsHhM.Birthdate);
                            else
                                table.Cell(7, 1).Range.Text = clsHhM.Age.ToString();

                            table.Cell(7, 2).Range.Text = clsHhM.Sex;
                            table.Cell(7, 3).Range.Text = clsHH.Phone;
                            table.Cell(1, 3).Range.Text = clsHhM.LastName;
                            table.Cell(1, 1).Range.Text = clsHhM.FirstName;
                        }
                        else
                        {
                            if (clsHhM.Inactive == false)
                            {
                                if (row > owdtblFamilyList.Rows.Count)
                                {
                                    owdtblFamilyList.Rows.Add();
                                }
                                owdtblFamilyList.Cell(row, 1).Range.Text = clsHhM.LastName + ", " + clsHhM.FirstName;
                                if (clsHhM.UseAge == true)
                                    owdtblFamilyList.Cell(row, 2).Range.Text = clsHhM.Age.ToString();
                                else
                                    owdtblFamilyList.Cell(row, 2).Range.Text = CCFBGlobal.ValidDateString(clsHhM.Birthdate);

                                owdtblFamilyList.Cell(row, 3).Range.Text = clsHhM.Sex;
                                if (owdtblFamilyList.Columns.Count > 3)
                                {
                                    if (clsHhM.IsDisabled == true)
                                    { owdtblFamilyList.Cell(row, 4).Range.Text = "X"; }
                                    if (clsHhM.SpecialDiet == true)
                                    { owdtblFamilyList.Cell(row, 5).Range.Text = "X"; }
                                }
                                row++;
                            }
                        }
                    }
                    oWordDoc.Save();
                    if (((_Application)oWord).ActiveWindow.View.SplitSpecial == WdSpecialPane.wdPaneNone)
                        ((_Application)oWord).ActiveWindow.ActivePane.View.Type = WdViewType.wdPrintView;
                    else
                        ((_Application)oWord).ActiveWindow.View.Type = WdViewType.wdPrintView;


                    //CCFBGlobal.openDocumentOutsideCCFB(saveAs.ToString());
                }
                catch (Exception ex)
                {
                    CCFBGlobal.appendErrorToErrorReport("File Path = " + oTemplatePath.ToString(), ex.GetBaseException().ToString());
                    ((_Application)oWord).Quit(SaveChanges: false, OriginalFormat: false, RouteDocument: false);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oWord);
                }
            }
            else
                MessageBox.Show("ERROR: " + templatePath + " Not Found", "Temlate Not Found",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void fillBookMark(String sBookMarkName, String sBookMarkText)
        {
            Object oBookMarkName;
            try
            {
                oBookMarkName = sBookMarkName;
                oWordDoc.Bookmarks.get_Item(ref oBookMarkName).Range.Text = sBookMarkText;
            }
            catch (Exception)
            {
            }
        }
        public void showSigPad()
        {
            sigPadInputCtrl1.Visible = false;
            btnResetSig.Visible = false;
            sigPadInputCtrl1.Visible = true;
            btnResetSig.Visible = true;
            if (sigPadInputCtrl1.ScreenMode < 0)
            {
                sigPadInputCtrl1.StartCapture();
            }
        }

        private void showSignature(int newUID)
        {
            sigPadInputCtrl1.Visible = false;
            btnResetSig.Visible = false;
            sigPadInputCtrl1.Visible = true;
            btnResetSig.Visible = true;
            FamilyCardSig clsFCSig = new FamilyCardSig(CCFBGlobal.connectionString);
            clsFCSig.LoadImage(newUID, clsHH.ID);
            if (clsFCSig.HaveSignature == true)
            {
                sigPadInputCtrl1.Signature = clsFCSig.SigString;
            }
        }

        private bool okToSave()
        {
            return sigPadInputCtrl1.IsSigned;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            sigPadInputCtrl1.initSigPad();
            this.Close();
        }

        private void btnResetSig_Click(object sender, EventArgs e)
        {
            if (sigPadInputCtrl1.initSigPad())
            {
                showSigPad();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int newTrxId = 0;
            object osavename = savePath + idText + ".pdf";
            object oFileFormat = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF;

            if (okToSave() == true)
            {
                btnSave.Enabled = false;
                if (sigPadInputCtrl1.Visible == true)
                {
                    if (sigPadInputCtrl1.IsSigned == true)
                    {
                        if (sigPadInputCtrl1.SaveSigImage(constSigFile))
                        {
                            oWord.Selection.EndKey(WdUnits.wdStory, WdMovementType.wdExtend);
                            oWord.Selection.MoveRight(WdUnits.wdCharacter, 1);
                            oWord.Selection.InlineShapes.AddPicture(constSigFile, false, true);
                            oWordDoc.Save();
                            filename = oWordDoc.FullName;

                            if (chkPrintOnSave.Checked == true)
                            {
                                CCFBGlobal.appendErrorToErrorReport(osavename.ToString(), "Sign and Print Family Card");
                                oWord.Options.PrintBackground = false;
                                oWordDoc.PrintOut(ref oFalse, ref oFalse, ref oMissing,
                                                  ref oMissing, ref oMissing, ref oMissing,
                                                  ref oMissing, ref oMissing, ref oMissing,
                                                  ref oMissing, ref oMissing, ref oMissing,
                                                  ref oMissing, ref oMissing, ref oMissing,
                                                  ref oMissing, ref oMissing, ref oMissing);
                                System.Windows.Forms.Application.DoEvents();
                                System.Threading.Thread.Sleep(100);
                            }
                            oWordDoc.SaveAs2(ref osavename, ref oFileFormat);
                            ((_Application)oWord).Quit(SaveChanges: true, OriginalFormat: false, RouteDocument: false);
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(oWord);
                            if (chkCloseOnSave.Checked == false)
                            {
                                CCFBGlobal.openDocumentOutsideCCFB(osavename.ToString());
                            }
                            CCFBGlobal.DeleteFile(filename);
                        }

                        //picSignature.Visible = true;
                        ////cmsLog.Visible = false;
                        //MessageBox.Show(this, "Close Signature Display");
                        //picSignature.Visible = false;

                        FamilyCardSig clsFCSig = new FamilyCardSig(CCFBGlobal.connectionString);
                        clsFCSig.LoadImage(newTrxId, clsHH.ID);
                        clsFCSig.UID = newTrxId;
                        clsFCSig.HhID = clsHH.ID;
                        clsFCSig.SigDate = Convert.ToDateTime(sigPadInputCtrl1.GetSigDate());
                        clsFCSig.DocPath = osavename.ToString();
                        clsFCSig.SigImage = sigPadInputCtrl1.GetImage();
                        clsFCSig.SigString = sigPadInputCtrl1.GetSignature();
                        if (clsFCSig.HaveSignature == true)
                        {
                            clsFCSig.Update();
                        }
                        else
                        {
                            clsFCSig.Insert();
                        }
                    }
                    sigPadInputCtrl1.ResetTablet();
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Signature is missing", "Saqve Signature");
            }
            btnSave.Enabled = true;
        }

        private void SignFamilyCard_Load(object sender, EventArgs e)
        {
            showSigPad();
            this.Text = sigPadInputCtrl1.State.ToString();
        }
    }
}
