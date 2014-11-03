using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace CCFB3
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string filePath = @"C:\ClientCardFB3\ClientcardFB3.exe";
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());

            Process proc = new Process();
            proc.EnableRaisingEvents = false;
            proc.StartInfo.FileName = filePath;
            proc.Start();
        }
    }
}
