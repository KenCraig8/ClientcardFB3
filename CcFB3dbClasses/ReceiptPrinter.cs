using System;
using System.IO;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace ClientcardFB3
{
    public class ReceiptPrinter
    {
        private Font printFont;
        private Client mclsClient;

        public ReceiptPrinter(Client tmpClient)
        {
            mclsClient = tmpClient;
        }

        public void printIssaquah()
        {
            
            try
            {
                try
                {
                    PrintDocument pd = new PrintDocument();
                    pd.DefaultPageSettings.Margins.Left = 0;
                    pd.DefaultPageSettings.Margins.Right = 0;
                    pd.DefaultPageSettings.Margins.Bottom = 10;
                    pd.DefaultPageSettings.Margins.Top = 10;
                    pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);

                    pd.PrinterSettings.PrinterName = "EPSON TM-T88V Receipt";

                    pd.Print();
                }
                finally
                {
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        // The PrintPage event is raised for each page to be printed.
        private void pd_PrintPage(object sender, PrintPageEventArgs ev)
        {
            float yPos = 0;
            float leftMargin = ev.MarginBounds.Left;
            float topMargin = ev.MarginBounds.Top;
            printFont = new Font("Arial", 14);
            yPos = printFont.GetHeight(ev.Graphics);
            Font pfTmp = new System.Drawing.Font("Arial", 26);
            ev.Graphics.DrawString("ISSAQUAH", pfTmp, Brushes.Black, 45, 0, new StringFormat());
            topMargin += 24;
            pfTmp = new System.Drawing.Font("Arial", 14);
            ev.Graphics.DrawString("Food & Clothing Bank", pfTmp, Brushes.Black, 50, topMargin, new StringFormat());
            topMargin += 20;
            pfTmp = new System.Drawing.Font("Arial", 9);
            ev.Graphics.DrawString("(425) 392-4123", pfTmp, Brushes.Black, 98, topMargin, new StringFormat());
            ev.Graphics.DrawString(mclsClient.getFirstName(), printFont, Brushes.Black, leftMargin, topMargin + yPos, new StringFormat());
            ev.Graphics.DrawString(mclsClient.getLastName(), printFont, Brushes.Black, leftMargin, topMargin + 2 * yPos, new StringFormat());
            ev.Graphics.DrawString("FS: " + mclsClient.clsHH.TotalFamily.ToString(), printFont, Brushes.Black, leftMargin, topMargin + 3 * yPos, new StringFormat());
            //            ev.Graphics.DrawString("Next Appointment:", pfTmp, Brushes.Black, leftMargin, topMargin + 4 * yPos + yPos/3, new StringFormat());
            pfTmp = new System.Drawing.Font("Arial", 10);
            //            ev.Graphics.DrawString("Tuesday 11/01/2011 10 to 10:20am", pfTmp, Brushes.Black, leftMargin, topMargin + 5 * yPos, new StringFormat());
            ev.Graphics.DrawString("________________________________", pfTmp, Brushes.Black, leftMargin, topMargin + 5 * yPos + yPos / 4, new StringFormat());
            ev.HasMorePages = false;
        }
    }
}
