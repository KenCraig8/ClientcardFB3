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
        /// Automaticly installs the required microsoft sql server bassed on the other install paramaters
        /// </summary>
        public static void install()
        {
            string scriptsPath = "C:\\Users\\Public\\ClientcardFB3\\Scripts\\";
            string configFileName = "ConfigSqlFullInstall.ini";

            WebClient webClinet = new WebClient();

            // chage latter to use actual params
            string installType = "full";

            // figure out which file to install for microsoft sql
            // either sql management studio or sql server 32 or 64 bit
            if (installType != "client") {
                string sqlInstallName = "SQLEXPRWT_ENU.exe";
                string sqlInstallDownloadName;
                // About install args: http://msdn.microsoft.com/en-us/library/ms144259.aspx#Feature
                string installArgs;

                if (installType == "full")
                {
                    // About config file: http://msdn.microsoft.com/en-us/library/dd239405.aspx
                    installArgs = " /IACCEPTSQLSERVERLICENSETERMS /ACTION=Install /CONFIGURATIONFILE=\"" + scriptsPath + configFileName + "\""; //(add the end quote with \"
                    if (Environment.Is64BitOperatingSystem)
                        sqlInstallDownloadName = "https://www.dropbox.com/s/5c1f9hosz1f8gw2/SQLEXPRWT_x64_ENU.exe?dl=1";
                    else
                        sqlInstallDownloadName = "https://www.dropbox.com/s/tohv09k9os60yd1/SQLEXPRWT_x86_ENU.exe?dl=1";
                }
                else // if (installType == "manage")
                {
                    // Default install so don't need config file
                    installArgs = "/IACCEPTSQLSERVERLICENSETERMS /ACTION=Install /QS";
                    if (Environment.Is64BitOperatingSystem)
                        sqlInstallDownloadName = "https://www.dropbox.com/s/ivhodmlvjhhoagw/SQLManagementStudio_x64_ENU.exe?dl=1";
                    else
                        sqlInstallDownloadName = "https://www.dropbox.com/s/2gapc02ohk6s6uo/SQLManagementStudio_x86_ENU.exe?dl=1";
                }

                // Download the file
                webClinet.DownloadFile(sqlInstallDownloadName, sqlInstallName);

                // install
                Process.Start(sqlInstallName, installArgs);
            }
            else if (installType == "clinet")
            {
                // msi file for client so different process
                string sqlInstallDownloadName;
                string clientInstallName = "SQLClient.msi";

                if (Environment.Is64BitOperatingSystem)
                    sqlInstallDownloadName = "https://www.dropbox.com/s/8l6doaevd0akj9l/SQLClient_64.msi?dl=1";
                else
                    sqlInstallDownloadName = "https://www.dropbox.com/s/iclcr7iaiykw9ji/SQLClient_x86.msi?dl=1";

                webClinet.DownloadFile(sqlInstallDownloadName, clientInstallName);

                // Command from: http://technet.microsoft.com/en-us/library/ms131321(v=sql.110).aspx
                Process.Start("msiexec", "/i "+ sqlInstallDownloadName + "APPGUID={0CC618CE-F36A-415E-84B4-FB1BFF6967E1} /q");
            }

            if (installType == "full")
            {
                // Configure the database
                Process.Start("CMD.exe", "/C SQLCMD -S %COMPUTERNAME%\\SQLEXPRESS -i " + scriptsPath + "RestoreDatabase.sql");
                Process.Start("CMD.exe", "/C SQLCMD -S %COMPUTERNAME%\\SQLEXPRESS -i " + scriptsPath + "SetUser.sql");
                Process.Start("CMD.exe", "/C SQLCMD -S %COMPUTERNAME%\\SQLEXPRESS -i " + scriptsPath + "ResetCCFBUser.sql");
            }

            // Command from: http://msdn.microsoft.com/en-us/library/cc646023.aspx

            Process.Start("netsh", "firewall set portopening protocol = TCP port = 1433 name = SQLPortTCP mode = ENABLE scope = SUBNET profile = CURRENT");
            Process.Start("netsh", "firewall set portopening protocol = UDP port = 1434 name = SQLPortUDP mode = ENABLE scope = SUBNET profile = CURRENT");

            // Might also need to follow these steps: http://stackoverflow.com/questions/11278114/enable-remote-connections-for-sql-server-express-2012
        }
    }
}
