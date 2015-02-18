using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Office.Interop.Word;


namespace ClientcardFB3
{
    class RptVoucherOrder01 : IDisposable
    {
        Household clsHH;
        VoucherLog clsVLItm;
        bool error = false;
        string rptDate;
        string savePath;
        bool _disposed = false;

        public bool Error
        {
            get
            {
                return error;
            }
        }
        public RptVoucherOrder01(Household hhIN, VoucherLog vlitmIN)
        {
            clsHH = hhIN;
            clsVLItm = vlitmIN;
            rptDate = clsVLItm.TrxDate.ToShortDateString();
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
                    if (clsHH != null)
                        clsHH.Dispose();
                    if (clsVLItm != null)
                        clsVLItm.Dispose();
                }

                // Indicate that the instance has been disposed.
                clsHH = null;
                clsVLItm = null;
                _disposed = true;
            }
        }

        public void createReport(string templatePath, string servicecode)
        {
                Object oMissing = System.Reflection.Missing.Value;
                Object missing = System.Reflection.Missing.Value;
                Object oTrue = true;
                Object oFalse = false;
                Application oWord = new Application();
                Document oWordDoc = new Document();
                oWord.Visible = false;
                Object oTemplatePath = templatePath;
                object saveAs = CCFBGlobal.pathLog + "tmp.docx";
            try
            {

                oWordDoc = oWord.Documents.Add(ref oTemplatePath, ref oMissing, ref oMissing, ref oMissing);

                //Save so that the template is free to be used by the next user
                oWordDoc.SaveAs(ref saveAs, ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing);

                oWordDoc.Tables[1].Cell(1, 2).Range.Text = clsHH.Name;
                oWordDoc.Tables[1].Cell(1, 4).Range.Text = clsHH.ID.ToString();
                oWordDoc.Tables[1].Cell(2, 2).Range.Text = servicecode;
                oWordDoc.Tables[1].Cell(3, 2).Range.Text = clsVLItm.TrxDate.ToShortDateString();

                oWordDoc.Tables[2].Cell(2, 2).Range.Text = clsVLItm.Infants.ToString();
                oWordDoc.Tables[2].Cell(3, 2).Range.Text = clsVLItm.Youth.ToString();
                oWordDoc.Tables[2].Cell(4, 2).Range.Text = (clsVLItm.Teens + clsVLItm.Eighteen).ToString();
                oWordDoc.Tables[2].Cell(5, 2).Range.Text = clsVLItm.Adults.ToString();
                oWordDoc.Tables[2].Cell(6, 2).Range.Text = clsVLItm.Seniors.ToString();
                oWordDoc.Tables[2].Cell(7, 2).Range.Text = clsVLItm.TotalFamily.ToString();

                oWordDoc.Tables[3].Cell(1, 2).Range.Text = clsVLItm.Notes;
                oWordDoc.Tables[6].Cell(1, 2).Range.Text = clsVLItm.CreatedBy;
                oWordDoc.Tables[6].Cell(1, 4).Range.Text = DateTime.Today.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();

                oWord.Options.PrintBackground = false;
                oWordDoc.PrintOut(ref oFalse, ref oFalse, ref oMissing,
                                  ref oMissing, ref oMissing, ref oMissing,
                                  ref oMissing, ref oMissing, ref oMissing,
                                  ref oMissing, ref oMissing, ref oMissing,
                                  ref oMissing, ref oMissing, ref oMissing,
                                  ref oMissing, ref oMissing, ref oMissing);
                //}
                System.Windows.Forms.Application.DoEvents();
                System.Threading.Thread.Sleep(100);
                ((_Application)oWord).Quit(SaveChanges: false, OriginalFormat: false, RouteDocument: false);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oWord);
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
