using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Office.Interop.Word;

namespace ClientcardFB3
{
    class RptFoodOrder02 : IDisposable
    {
        Household clsHH;
        TrxLogItem clsTLItm;
        bool error = false;
        bool _disposed = false;
        string rptDate;

        public bool Error
        {
            get
            {
                return error;
            }
        }

        public RptFoodOrder02(Household hhIN, TrxLogItem tlitmIN)
        {
            clsHH = hhIN;
            clsTLItm = tlitmIN;
            rptDate = clsTLItm.TrxDate.ToShortDateString();
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
                }

                // Indicate that the instance has been disposed.
                clsHH = null;
                _disposed = true;
            }
        }

        public void createReport(string templatePath)
        {
                Object oMissing = System.Reflection.Missing.Value;
                Object missing = System.Reflection.Missing.Value;
                Object oTrue = true;
                Object oFalse = false;
                Application oWord = new Application();
                Document oWordDoc = new Document();
                oWord.Visible = true;
                Object oTemplatePath = templatePath;
                Object oBookMarkName = "EnteredBy";
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
                oWordDoc.Tables[1].Cell(2, 2).Range.Text = clsTLItm.FoodSvcList;
                oWordDoc.Tables[1].Cell(3, 2).Range.Text = clsTLItm.TrxDate.ToShortDateString();

                if (clsTLItm.Infants > 0)
                { oWordDoc.Tables[2].Cell(2, 2).Range.Text = clsTLItm.Infants.ToString(); }
                if (clsTLItm.Youths > 0)
                { oWordDoc.Tables[2].Cell(3, 2).Range.Text = clsTLItm.Youths.ToString(); }
                if (clsTLItm.Teens + clsTLItm.Eighteen > 0)
                { oWordDoc.Tables[2].Cell(4, 2).Range.Text = (clsTLItm.Teens + clsTLItm.Eighteen).ToString(); }
                if (clsTLItm.Adults > 0)
                { oWordDoc.Tables[2].Cell(5, 2).Range.Text = clsTLItm.Adults.ToString(); }
                if (clsTLItm.Seniors > 0)
                { oWordDoc.Tables[2].Cell(6, 2).Range.Text = clsTLItm.Seniors.ToString(); }
                oWordDoc.Tables[2].Cell(7, 2).Range.Text = clsTLItm.TotalFamily.ToString();

                
                oWordDoc.Tables[3].Cell(1, 2).Range.Text = clsTLItm.Notes;

                if (clsTLItm.LbsStandard > 0)
                { oWordDoc.Tables[4].Cell(1, 1).Range.Text = "Food Box"; }
                //oWordDoc.Tables[4].Cell(2, 1).Range.Text = clsTLItm.LbsOther.ToString();
                if (clsTLItm.RcvdCommodity == true)
                { oWordDoc.Tables[4].Cell(3, 1).Range.Text = "Commodities"; }
                //oWordDoc.Tables[4].Cell(4, 2).Range.Text = clsTLItm.LbsSupplemental.ToString();
                //oWordDoc.Tables[4].Cell(5, 2).Range.Text = clsTLItm.LbsBabySvc.ToString();
                //oWordDoc.Tables[4].Cell(6, 2).Range.Text = (clsTLItm.LbsStandard + clsTLItm.LbsOther + clsTLItm.LbsCommodities + clsTLItm.LbsSupplemental + clsTLItm.LbsBabySvc).ToString();

                //oWordDoc.Tables[5].Cell(1, 2).Range.Text = clsTLItm.CreatedBy;
                oWordDoc.Bookmarks.get_Item(ref oBookMarkName).Range.Text = clsTLItm.CreatedBy;

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
                //((_Application)oWord).Quit(SaveChanges: false, OriginalFormat: false, RouteDocument: false);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oWord);
                error = true;
            }
        }
    }
}
