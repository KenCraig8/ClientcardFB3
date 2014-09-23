using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Data.SqlClient;
using System.IO;
using System.Media;
using Microsoft.Win32;
using System.Threading;

namespace ClientcardFB3
{
    public partial class BackupDBForm : Form
    {
        string serverBackupPath = "";
        const string sqlBackupFile = "FB3.bak";
        string fileSource = "";
        string fileDestination = "";
        
        string registryVal = "";

        SqlCommand command;
        SqlConnection conn;

        public BackupDBForm()
        {
            InitializeComponent();
            serverBackupPath = CCFBGlobal.getRegServerBackupPath();
            if (serverBackupPath == "")
            {
                if (CCFBGlobal.serverName == CCFBGlobal.pcName)
                {
                    serverBackupPath = System.IO.Path.Combine(CCFBGlobal.homeDrive, @"Users\Public\ClientcardFB3\Database\FB3.bak");
                }
                else
                {
                    string stmp = CCFBGlobal.getRegUpdatePath();
                    serverBackupPath = stmp.Replace("ClientcardFB3.exe", @"\Database\" + sqlBackupFile);
                }
            }
            tbSvrPath.Text = serverBackupPath;
            tbServerName.Text = CCFBGlobal.serverName;


            registryVal = CCFBGlobal.getRegExternalBackupPath();
            if (registryVal.EndsWith("bak") != true)
            {
                tbSavePath.Text = @"F:\" + sqlBackupFile;
            }
            else
            {
                tbSavePath.Text = registryVal;
            }
            label4.Visible = false;
            progressBar1.Visible = false;
            btnOk.Visible = false;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            label4.Text = "Performing SQL Database Backup to Online Location";
            label4.Visible = true;
            progressBar1.Visible = true;
            progressBar1.Value = 0;
            btnStart.Enabled = false;
            this.Cursor = Cursors.WaitCursor;
            Application.DoEvents();
            progressBar1.Increment(10);
            try
            {
                fileSource = tbSvrPath.Text;
                fileDestination = tbSavePath.Text;

                CCFBGlobal.verifyPath(Path.GetDirectoryName(fileSource));   //Verify server backup path exists and create it if not
                if (File.Exists(fileSource) == true)
                {
                    File.Delete(fileSource);
                }
                if (fileSource != serverBackupPath)
                {
                    CCFBGlobal.saveRegServerBackupPath(fileSource);
                }
                
                conn = new SqlConnection(CCFBGlobal.connectionString);
                if (conn.State != ConnectionState.Open)
                { 
                    conn.Open(); 
                }
                //Set backup command
                command = new SqlCommand(@"BACKUP DATABASE [ClientCardFB3] TO  DISK = N'" + fileSource + "' "
                    + "WITH NOFORMAT, COPY_ONLY, NAME = N'ClientCardFB3-Full Database Backup'", conn);
                
                //create backup
                progressBar1.Increment(20);
                Application.DoEvents();
                command.CommandTimeout = 1800;
                command.ExecuteNonQuery();
                conn.Close();

                try
                {
                    label4.Text = "Copy backup file from Online to Offline Location";
                    progressBar1.Increment(40);
                    Application.DoEvents();
                    //Copy file from server to saveBackupPath
                    CCFBGlobal.verifyPath(Path.GetDirectoryName(fileDestination));
                    File.Copy(fileSource, fileDestination, true);
                    progressBar1.Value = 100;
                    label4.Text ="Backup Complete. File Saved To " + fileDestination;
                    btnOk.Visible = true;
                    
                    //Thread mythread = new Thread(new ThreadStart(copy));
                    //mythread.IsBackground = true;
                    //mythread.Start();
                }
                catch (Exception ex)
                {
                    CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString());
                    MessageBox.Show("Could Not Copy File To " + saveFileDialog1.FileName, 
                        "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnStart.Enabled = true;
                    label4.Text = "Copy FAILED";
                }
            }
            catch (SqlException ex)
            {
                CCFBGlobal.appendErrorToErrorReport("Command=" + command.CommandText + "TimeOut = " + command.Connection.ConnectionTimeout.ToString(), ex.GetBaseException().ToString());
                MessageBox.Show("Could Not Create Backup", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnStart.Enabled = true;
                label4.Text = "Backup FAILED";
            }
            CCFBGlobal.setConnectionString(CCFBGlobal.sq1ServerName);
            this.Cursor = Cursors.Default;
           // progressBar1.Visible = false;
        }

        //private void copy()
        //{
        //    try
        //    {
        //        File.Copy(fileSource, fileDestination, true);
        //        MessageBox.Show("Backup Complete. File Saved To " + fileDestination);
        //    }
        //    catch (Exception ex)
        //    {
        //        CCFBGlobal.appendErrorToErrorReport("Copy " + fileSource + " to " + fileDestination, ex.GetBaseException().ToString());
        //        MessageBox.Show("Could Not Copy File\r\nFrom: " + fileSource + "\r\n  To: " + fileDestination,
        //            "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        private string getBackupPath(string oriPath)
        {
            try
            {
                //Set default extension of the savefile dialog
                saveFileDialog1.DefaultExt = "bak";
                //If no value exists in registry for default save location
                if (oriPath == "")
                {
                    FileInfo fi = new FileInfo(Application.ExecutablePath);

                    //Get all drives on computer
                    DriveInfo[] drives = DriveInfo.GetDrives();

                    //Travers drives in reverse looking for a removeable drive with enough space on it for the backup file
                    for (int i = drives.Length - 1; i >= 0; i--)
                    {
                        if (drives[i].DriveType == DriveType.Removable && drives[i].AvailableFreeSpace > fi.Length)
                        {
                            oriPath = drives[i].Name;
                            break;
                        }
                    }
                }
                saveFileDialog1.InitialDirectory = Path.GetDirectoryName(oriPath);
                saveFileDialog1.FileName = sqlBackupFile;
                saveFileDialog1.Filter = "SQL Backup Files (*.bak)|*.bak";
                DialogResult dr = saveFileDialog1.ShowDialog();

                //If user confirmed save
                if (dr == System.Windows.Forms.DialogResult.OK)
                {
                    //Set the value of the path in the registry
                    Registry.SetValue(CCFBGlobal.registryKeyCurrentUser, CCFBGlobal.regsubkeySavePath, saveFileDialog1.FileName);
                    return saveFileDialog1.FileName;
                }
            }
            catch (Exception ex)
            {
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString());
            }
            return oriPath;
        }

        private void btnGetSavePath_Click(object sender, EventArgs e)
        {
            tbSavePath.Text = getBackupPath(tbSavePath.Text);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
