using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SqlInstaller
{
    public class SqlInstaller
    {
        /// <summary>
        /// 
        /// </summary>
        public static void install()
        {
            string sqlInstallName = "SQLEXPRWT_ENU.exe";

            WebClient webClinet = new WebClient();

            string sqlInstallDownloadName;

            if (Environment.Is64BitOperatingSystem)
            {
                sqlInstallDownloadName = "https://www.dropbox.com/s/5c1f9hosz1f8gw2/SQLEXPRWT_x64_ENU.exe?dl=1";
            }
            else
            {
                sqlInstallDownloadName = "https://www.dropbox.com/s/tohv09k9os60yd1/SQLEXPRWT_x86_ENU.exe?dl=1";
            }

            string scriptsPath = "C:\\Users\\Public\\ClientcardFB3\\Scripts\\";
            webClinet.DownloadFile(sqlInstallDownloadName, sqlInstallName);
            string installArgs = " /ACTION=Install /CONFIGURATIONFILE=\"" + scriptsPath + "ConfigurationFile.ini\"";
            Process.Start(sqlInstallName, installArgs);

            Process.Start("CMD.exe", "/C SQLCMD -S %COMPUTERNAME%\\SQLEXPRESS -i "+scriptsPath+"RestoreDatabase.sql");
            Process.Start("CMD.exe", "/C SQLCMD -S %COMPUTERNAME%\\SQLEXPRESS -i " + scriptsPath + "SetUser.sql");
            Process.Start("CMD.exe", "/C SQLCMD -S %COMPUTERNAME%\\SQLEXPRESS -i " + scriptsPath + "ResetCCFBUser.sql");

        }
    }
}
