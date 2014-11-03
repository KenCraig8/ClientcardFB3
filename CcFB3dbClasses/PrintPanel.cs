using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace ClientcardFB3
{
    class PrintPanel
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

        public PrintPanel(Panel pnl1, string saveAsIn)
        {
            panel1_ = pnl1;
            saveAs = saveAsIn;

            //printdoc1.PrintPage += (printdoc1_PrintPage);
            //MemoryImage1 = new Bitmap(pnl1.Width, pnl1.Height);
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
