using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Office.Interop.Word;

namespace ClientcardFB3
{
    class RptFoodOrder01
    {
        Household clsHH;
        TrxLogItem clsTLItm;
        bool error = false;
        string rptDate;

        public bool Error
        {
            get
            {
                return error;
            }
        }

        public RptFoodOrder01(Household hhIN, TrxLogItem tlitmIN)
        {
            clsHH = hhIN;
            clsTLItm = tlitmIN;
            rptDate = clsTLItm.TrxDate.ToShortDateString();
        }

        public void createReport(string templatePath)
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
                oWordDoc.Tables[1].Cell(2, 2).Range.Text = clsTLItm.FoodSvcList;
                oWordDoc.Tables[1].Cell(3, 2).Range.Text = clsTLItm.TrxDate.ToShortDateString();

                if (clsHH.UserFlag0 == true)
                { oWordDoc.Tables[2].Cell(2, 1).Range.Text = "Stove"; }
                if (clsHH.UserFlag1 == true)
                { oWordDoc.Tables[2].Cell(3, 1).Range.Text = "Refrigerator"; }
                if (clsHH.UserFlag2 == true)
                { oWordDoc.Tables[2].Cell(4, 1).Range.Text = "Freezer"; }

                oWordDoc.Tables[2].Cell(2, 4).Range.Text = clsTLItm.Infants.ToString();
                oWordDoc.Tables[2].Cell(3, 4).Range.Text = clsTLItm.Youths.ToString();
                oWordDoc.Tables[2].Cell(4, 4).Range.Text = (clsTLItm.Teens + clsTLItm.Eighteen).ToString();
                oWordDoc.Tables[2].Cell(5, 4).Range.Text = clsTLItm.Adults.ToString();
                oWordDoc.Tables[2].Cell(6, 4).Range.Text = clsTLItm.Seniors.ToString();
                oWordDoc.Tables[2].Cell(7, 4).Range.Text = clsTLItm.TotalFamily.ToString();

                oWordDoc.Tables[3].Cell(1, 2).Range.Text = clsTLItm.Notes;

                oWordDoc.Tables[4].Cell(1, 2).Range.Text = clsTLItm.LbsStandard.ToString();
                oWordDoc.Tables[4].Cell(2, 2).Range.Text = clsTLItm.LbsOther.ToString();
                oWordDoc.Tables[4].Cell(3, 2).Range.Text = clsTLItm.LbsCommodities.ToString();
                oWordDoc.Tables[4].Cell(4, 2).Range.Text = clsTLItm.LbsSupplemental.ToString();
                oWordDoc.Tables[4].Cell(5, 2).Range.Text = clsTLItm.LbsBabySvc.ToString();
                oWordDoc.Tables[4].Cell(6, 2).Range.Text = (clsTLItm.LbsStandard + clsTLItm.LbsOther + clsTLItm.LbsCommodities + clsTLItm.LbsSupplemental + clsTLItm.LbsBabySvc).ToString();

                oWordDoc.Tables[5].Cell(1, 2).Range.Text = clsTLItm.CreatedBy;

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
