using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.Win32;

namespace ClientcardFB3
{
    public partial class LoginForm : Form
    {
        bool needtoUpdate = true;
        const string constLocalServer = "(Local)\\SQLExpress";
        const string ODBC_KeyPath = @"HKEY_LOCAL_MACHINE\Software\ODBC\ODBCINST.INI\SQL Server";
        const string registryKeyName = @"HKEY_CURRENT_USER\Software\CSDG\ClientcardFB3";
        const string regsubkeyServer = "SQLServer";
        const string regsubkeyUser = "CCFBUser";
        private const string ODBC_INI_REG_PATH = "HKEY_CURRENT_USER\\SOFTWARE\\ODBC\\ODBC.INI\\ClientcardFB3";
        string dsnDbName = "";
        string dsnDriverName = "";
        string dsnServer = "";
        string ServerNameFromRegistry = "";
        string UserFromRegistry = "";
        Userlist clsUsers;

        public LoginForm()
        { 
            InitializeComponent();
            ServerNameFromRegistry = (string)Registry.GetValue(registryKeyName, regsubkeyServer, constLocalServer);
            UserFromRegistry = (string)Registry.GetValue(registryKeyName, regsubkeyUser, "");
            dsnDbName = (string)Registry.GetValue(ODBC_INI_REG_PATH, "DataBase", "");
            dsnDriverName = (string)Registry.GetValue(ODBC_KeyPath, "Driver", "");
            dsnServer = (string)Registry.GetValue(ODBC_INI_REG_PATH, "Server", "");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogOn_Click(object sender, EventArgs e)
        {
            logIn();
        }

        private void btnRefreshServers_Click(object sender, EventArgs e)
        {
            loadServers();
        }

        private void cboServers_DropDownClosed(object sender, EventArgs e)
        {
            if (cboServers.SelectedItem != null)
            {
                SetVisibility(false);
                CCFBGlobal.setConnectionString(cboServers.SelectedItem.ToString());
            }
            else if (cboServers.Text != null && cboServers.Text != "")
            {
                SetVisibility(false);
                CCFBGlobal.setConnectionString(cboServers.Text);
            }

            if (TestSQLConnection("") == true) 
                loadUsers();
        }

        private void cboServers_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cboServers.Text = cboServers.SelectedItem.ToString();
        }

        public string CopyRightsDetail()
        {
            String CopyRightText = "";
            Assembly assembly = System.Reflection.Assembly.GetEntryAssembly();
            if (assembly != null)
            {
                object[] customAttributes = assembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if ((customAttributes != null) && (customAttributes.Length > 0))
                    { CopyRightText = ((AssemblyCopyrightAttribute)customAttributes[0]).Copyright; }
                
                if (string.IsNullOrEmpty(CopyRightText))
                    { CopyRightText = string.Empty; }
            }
            return CopyRightText;
        }

        private string getListOfServers()
        {
            System.IO.StreamReader outputStream = System.IO.StreamReader.Null;
            string data = "";
            string tempPath = System.IO.Path.GetTempPath();
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = "OSQL.exe";
            p.StartInfo.Arguments = "-L";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();

            if (p.WaitForExit(50000))
            {
                outputStream = p.StandardOutput;
                data = outputStream.ReadToEnd();
                //p.WaitForExit();
            }
            outputStream.Close();
            if (data == "")
                MessageBox.Show("Failed To Get List Of Servers");
            return data;
        }

        private int getPermissionsInt(string role)
        {
            switch (role)
            {
                case "Admin":       { return CCFBGlobal.permissions_Admin;       }
                case "IntakeAdmin": { return CCFBGlobal.permissions_IntakeAdmin; }
                case "Intake":      { return CCFBGlobal.permissions_Intake;      }
                default:            { return CCFBGlobal.permissions_Intake;      }
            }
        }

        private void loadServers()
        {
            cboServers.Visible = false;
            btnRefreshServers.Enabled = false;
            SetVisibility(false);
            CCFBGlobal.serverName = "";
            string serverlist = getListOfServers();
            if (serverlist != "")
            {
                processServerList(serverlist);
                cboServers.Visible = true;
            }
            btnRefreshServers.Enabled = true;
        }


        private void loadUsers()
        {
            string sUser = "";
            clsUsers = new Userlist(CCFBGlobal.connectionString);

            clsUsers.openAll();
            cboUsers.Items.Clear();
            tbPassword.Text = "";
            for (int i = 0; i < clsUsers.RowCount; i++)
            {
                sUser = clsUsers.DSet.Tables[0].Rows[i]["UserName"].ToString();
                cboUsers.Items.Add(sUser);
                if (sUser == UserFromRegistry)
                    cboUsers.SelectedIndex = cboUsers.Items.Count-1;
            }
            SetVisibility(cboUsers.Items.Count > 0);
        }

        private void logIn()
        {
            if (cboUsers.SelectedIndex >= 0 || cboUsers.Text != "")
            {
                for (int i = 0; i < clsUsers.RowCount; i++)
                {
                    if (clsUsers.DSet.Tables[0].Rows[i]["UserName"].ToString().ToUpper() ==
                        cboUsers.Text.ToUpper())
                    {
                        if (tbPassword.Text.ToUpper() ==
                            clsUsers.DSet.Tables[0].Rows[i]["Password"].ToString().ToUpper())
                        {
                            CCFBGlobal.currentUser_Name = clsUsers.DSet.Tables[0].Rows[i]["UserName"].ToString();
                            CCFBGlobal.currentUser_PermissionLevel =
                                getPermissionsInt(clsUsers.DSet.Tables[0].Rows[i].Field<string>("UserRole"));
                            CCFBGlobal.pcName = System.Windows.Forms.SystemInformation.ComputerName;
                            saveToRegistry();
                            writeToDSNValues();
                            CCFBPrefs.Init();
                            CCFBGlobal.LoadTypes();
                            MainForm frmMain = new MainForm(this);
                            this.Visible = false;
                            this.ShowInTaskbar = false;
                            frmMain.ShowDialog();
                            break;
                        }
                        else
                        {
                            MessageBox.Show("The Password Does Not Match For This User");
                            tbPassword.Text = "";
                            tbPassword.Focus();
                            Application.DoEvents();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please Select A User");
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            this.Text = Application.ProductName.ToString();
            label4.Text = Application.ProductName.ToString() + ": " + ProductDescription() + "\r\n"
                + "Version " + Application.ProductVersion.ToString() + "\r\n"
                + CopyRightsDetail() + "\r\n\r\n"
                + Application.ExecutablePath + "\r\n\r\n";

            if (CCFBGlobal.IsConnectedToInternet() == true || CCFBGlobal.isConnectedToNetwork() == true)
                label4.Text += "Connected To Network";
            else
                label4.Text += "No Network Found";
             
        }

        private void LoginForm_VisibleChanged(object sender, EventArgs e)
        {
            timer1.Enabled = needtoUpdate;
        }

        private void processServerList(string data)
        {
            string baseTestName = "";
            string newName = "";
            bool inList = false;
            if (ServerNameFromRegistry == null || ServerNameFromRegistry == "")
            {
                baseTestName = "(LOCAL)\\SQLEXPRESS";
            }
            else
            {
                baseTestName = ServerNameFromRegistry.ToUpper();
            }
            cboServers.Items.Clear();
            string[] splitData = data.Split(("\r\n").ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            string[] sqlservernames = new string[splitData.Length-1];
            int nbrnames = 0;
            for (int i = 0; i < splitData.Length; i++)
            {
                if (splitData[i] != "" && splitData[i] != "Servers:")
                {
                    newName = splitData[i].ToUpper().Trim();
                    sqlservernames[nbrnames] = newName;
                    nbrnames++;
                    cboServers.Items.Add(newName);
                    if (newName == baseTestName)
                    {
                        cboServers.SelectedIndex = cboServers.Items.Count - 1;
                    }
                }
            }
            if (cboServers.SelectedIndex >= 0)
                CCFBGlobal.setConnectionString(cboServers.Text);

                for (int i = 0; i < sqlservernames.Length; i++)
                {
                    if (sqlservernames[i].Contains(@"\") == false)
                    {
                        newName = sqlservernames[i] + "\\SQLEXPRESS";
                        inList = false;
                        for (int j = 0; j < cboServers.Items.Count; j++)
                        {
                            if (newName == cboServers.Items[j].ToString())
                            {
                                inList = true;
                                break;
                            }
                        }
                        if (inList == false)
                            cboServers.Items.Add(newName);
                    }
                }
        }

        public string ProductDescription()
        {
            string ProdDescr = "";
            Assembly assembly = System.Reflection.Assembly.GetEntryAssembly();
            if (assembly != null)
            {
                object[] customAttributes = assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if ((customAttributes != null) && (customAttributes.Length > 0))
                    { ProdDescr = ((AssemblyDescriptionAttribute)customAttributes[0]).Description; }
                
                if (string.IsNullOrEmpty(ProdDescr))
                    { ProdDescr = string.Empty; }
            }
            return ProdDescr;
        }

        public void resetForm()
        {
            loadUsers();
        }

        private void saveToRegistry()
        {
            if (CCFBGlobal.serverName != ServerNameFromRegistry)
            {
                Registry.SetValue(registryKeyName, regsubkeyServer, CCFBGlobal.serverName);
                ServerNameFromRegistry = CCFBGlobal.serverName;
            }
            if (CCFBGlobal.currentUser_Name != UserFromRegistry)
            {
                Registry.SetValue(registryKeyName, regsubkeyUser, CCFBGlobal.currentUser_Name);
                UserFromRegistry = CCFBGlobal.currentUser_Name;
            }
        }

        private void SetVisibility(bool isVisible)
        {
            cboUsers.Visible = isVisible;
            tbPassword.Visible = isVisible;
            btnLogOn.Enabled = isVisible;
            Application.DoEvents();
        }

        private void tbPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                logIn();
            }
        }

        private bool TestSQLConnection(string server)
        {
            string tmp = server;
            if (tmp == "")
                tmp = CCFBGlobal.serverName;
            lblTestStatus.Text = "Testing Server Connection - " + tmp;
            lblTestStatus.Visible = true;
            Application.DoEvents();
            CCFBGlobal.connectionTimeout = 20;
            CCFBGlobal.setConnectionString(server);
            System.Data.SqlClient.SqlConnection conn = new SqlConnection(CCFBGlobal.connectionString);
            try
            {
                cboUsers.Items.Clear();
            //    cboUsers.Text = "";
                conn.Open();
                conn.Close();
                lblTestStatus.Visible = false;
                return true;
            }
            catch (SqlException ex)
            {
//                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString(),
//               CCFBGlobal.serverName);
                MessageBox.Show("Error Connecting to Server " + CCFBGlobal.serverName + "\r\n" + ex.Message.ToString());
                lblTestStatus.Visible = false;
                return false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            needtoUpdate = false;
            SetVisibility(false);
            CCFBGlobal.setConnectionString(ServerNameFromRegistry);

            if (ServerNameFromRegistry == null || ServerNameFromRegistry == "")
            {
                loadServers();
                if (CCFBGlobal.serverName != "")
                {
                    if (TestSQLConnection("") == true)
                        loadUsers();
                }
            }
            else
            {
                if (TestSQLConnection("") == false)
                {
                    loadServers();
                    if (CCFBGlobal.serverName != "")
                    {
                        if (TestSQLConnection("") == true)
                            loadUsers();
                    }
                }
                else
                    if (cboServers.Items.Count == 0)
                    {
                        cboServers.Text = ServerNameFromRegistry;
                    }

                    loadUsers();
            }
            
            this.ShowInTaskbar = true;
            this.Focus();
            Application.DoEvents();

        }

        public void writeToDSNValues()
        {
            if (dsnDbName == "" || dsnServer != CCFBGlobal.serverName)
            {
                Registry.SetValue(ODBC_INI_REG_PATH, "Description", "The ODBC Connection For Access Reports");
                Registry.SetValue(ODBC_INI_REG_PATH, "DataBase", "ClientCardFB3");
                Registry.SetValue(ODBC_INI_REG_PATH, "Driver", dsnDriverName);
                Registry.SetValue(ODBC_INI_REG_PATH, "LastUser", "CCFB_User");
                Registry.SetValue(ODBC_INI_REG_PATH, "Server", CCFBGlobal.serverName);
            }
        }
    }
}
