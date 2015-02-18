using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace ClientcardFB3
{
    class PrintPanel : IDisposable
    {
        readonly PrintDocument printdoc1 = new PrintDocument();
        readonly PrintPreviewDialog previewdlg = new PrintPreviewDialog();
        Bitmap MemoryImage1;
        //Bitmap MemoryImage2;
        //Bitmap MemoryImage3;
        //Bitmap MemoryImage4;
        //Bitmap MemoryImage5;
        Image image;
        string saveAs = "";
        private readonly Panel panel1_;
        private bool _disposed;

        public PrintPanel(Panel pnl1, string saveAsIn)
        {
            panel1_ = pnl1;
            saveAs = saveAsIn;

            //printdoc1.PrintPage += (printdoc1_PrintPage);
            //MemoryImage1 = new Bitmap(pnl1.Width, pnl1.Height);
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
                    if (printdoc1 != null)
                        printdoc1.Dispose();
                    if (previewdlg != null)
                        previewdlg.Dispose();
                    if (MemoryImage1 != null)
                        MemoryImage1.Dispose();
                    if (image != null)
                        image.Dispose();
                }

                // Indicate that the instance has been disposed.
                MemoryImage1 = null;
                image = null;
                _disposed = true;
            }
        }

        private void GetPrintArea(Control pnl)
        {
            MemoryImage1 = new Bitmap(pnl.Width, pnl.Height);
            pnl.DrawToBitmap(MemoryImage1, new Rectangle(0, 0, pnl.Width, pnl.Height));
        }

        private void printdoc1_PrintPage(object sender, PrintPageEventArgs e)
        {
            var pagearea = e.PageBounds;
            e.Graphics.DrawImage(MemoryImage1, (pagearea.Width / 2) - (panel1_.Width / 2), panel1_.Location.Y);
        }

        public void Print()
        {
            GetPrintArea(panel1_);
            image = MemoryImage1;
            Graphics gs = Graphics.FromImage(image);
            gs.DrawImage(image, new Point(0, 0));
            image.Save(saveAs + ".jpg");
            //previewdlg.Document = printdoc1;
            //previewdlg.ShowDialog();
        }
    }
}
