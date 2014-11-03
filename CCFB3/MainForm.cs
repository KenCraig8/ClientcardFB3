using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Drawing;

namespace CCFB3
{
    public partial class MainForm : Form
    {
        const string registryKeyName = @"HKEY_CURRENT_USER\Software\CSDG\ClientcardFB3";
        const string regsubkeySavePath = "UpdatePath";
        string ClientcardFB3_local = @"C:\ClientCardFB3\ClientcardFB3.exe";

        public MainForm()
        {
            InitializeComponent();
            this.BackColor = Color.LightGreen;
        }

        private void checkVersion()
        {
            string registryVal = (string)Registry.GetValue(registryKeyName, regsubkeySavePath, "");
            if (registryVal != "")
            {
                try
                {
                    FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(ClientcardFB3_local);
                    FileVersionInfo fvi2 = FileVersionInfo.GetVersionInfo(registryVal);

                    if (fvi.ProductVersion.CompareTo(fvi2.ProductVersion) == -1)
                    {
                        this.Cursor = Cursors.WaitCursor;
                        File.Copy(registryVal, ClientcardFB3_local, true);
                    }
                }
                catch { }
                this.Cursor = Cursors.Default;
            }
            this.Close();
        }

        /// <summary>
        /// Opens the given file in a process outside of ClientcardFB3
        /// </summary>
        /// <param name="filePath"></param>
        public void openDocumentOutsideCCFB(string filePath)
        {
            if (File.Exists(filePath.ToString()) == true)
            {
                Process proc = new Process();
                proc.EnableRaisingEvents = false;
                proc.StartInfo.FileName = filePath;
                proc.Start();
            }
        }

        private void Form1_Activated(object sender, System.EventArgs e)
        {
            checkVersion();
        }
    }
}
