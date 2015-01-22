//#define FASTTRACK
#define CCFB
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.Win32;

namespace ClientcardFB3
{
    public partial class LoginForm : Form, ILoginForm
    {
        bool needtoUpdate = false;
        bool cancelLogIn = false;
        string sAdminPwd = "";
        string dsnDbName = "";
        string dsnDriverName = "";
        string dsnServer = "";
        string ServerNameFromRegistry = "";
        string UserFromRegistry = "";
        Userlist clsUsers;
#if FASTTRACK
            FastTrackForm formMain;
#endif
#if CCFB
            MainForm formMain;
#endif
        public LoginForm()
        { 
            InitializeComponent();
            ServerNameFromRegistry = CCFBGlobal.getRegSQLServer();
            UserFromRegistry = CCFBGlobal.getRegDlftUser();
            dsnDbName = CCFBGlobal.getRegDSNDbName();
            dsnDriverName = CCFBGlobal.getRegDSNDriverName();
            dsnServer = CCFBGlobal.getRegDSNServer();
            needtoUpdate = true;
            CCFBGlobal.initPaths();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (btnCancel.Tag.ToString() == "logIn" || btnCancel.Tag.ToString() == "testsql")
            {
                cancelLogIn = true;
            }
            else
            {
                this.Close();
            }
        }

        private void btnLogOn_Click(object sender, EventArgs e)
        {
            logIn(cboUsers.Text.ToUpper(), tbPassword.Text.ToUpper());
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
            try
            {
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("The rountine failed?\r\n" + ex.Message ,
                    "Get List of Server", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
            CCFBGlobal.sq1ServerName = "";
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
            bool setPwDFocus = false;
            string sUser = "";
            sAdminPwd = "";
            clsUsers = new Userlist(CCFBGlobal.connectionString);

            clsUsers.openAll();
            cboUsers.Items.Clear();
            tbPassword.Text = "";
            for (int i = 0; i < clsUsers.RowCount; i++)
            {
                sUser = clsUsers.DSet.Tables[0].Rows[i]["UserName"].ToString();
                if (sUser == "Admin")
                {
                    sAdminPwd = clsUsers.DSet.Tables[0].Rows[i]["Password"].ToString();
                }
                cboUsers.Items.Add(sUser);
                if (sUser == UserFromRegistry)
                {
                    cboUsers.SelectedIndex = cboUsers.Items.Count - 1;
                    setPwDFocus = true;
                }
            }
            SetVisibility(cboUsers.Items.Count > 0);
            if (setPwDFocus == true)
            {
                tbPassword.Focus();
            }
        }

        private void logIn(string userName, string pwd)
        {
            if (userName != "")
            {
                this.Enabled = false;
                btnCancel.Tag = "login";
                for (int i = 0; i < clsUsers.RowCount; i++)
                {   if (cancelLogIn == false)
                    {
                        if (clsUsers.DSet.Tables[0].Rows[i]["UserName"].ToString().ToUpper() == userName)
                        {
                            if (pwd == clsUsers.DSet.Tables[0].Rows[i]["Password"].ToString().ToUpper())
                            {
                                CCFBGlobal.currentUser_Name = clsUsers.DSet.Tables[0].Rows[i]["UserName"].ToString();
                                CCFBGlobal.currentUser_PermissionLevel =
                                    getPermissionsInt(clsUsers.DSet.Tables[0].Rows[i].Field<string>("UserRole"));
                                CCFBGlobal.pcName = System.Windows.Forms.SystemInformation.ComputerName;
                                CCFBGlobal.dbUserName = CCFBGlobal.currentUser_Name + "/" + CCFBGlobal.pcName;
                                saveToRegistry();
                                writeToDSNValues();
                                CultureInfo cultureTest = new CultureInfo(CultureInfo.CurrentCulture.LCID, true);
                                if (cultureTest.DateTimeFormat.ShortDatePattern != CCFBGlobal.DefaultShortDateFmt || cultureTest.DateTimeFormat.ShortTimePattern != CCFBGlobal.DefaultShortTimeFmt)
                                {
                                    cultureTest.DateTimeFormat.ShortDatePattern = CCFBGlobal.DefaultShortDateFmt;
                                    cultureTest.DateTimeFormat.ShortTimePattern = CCFBGlobal.DefaultShortTimeFmt;
                                    Application.CurrentCulture = cultureTest;
                                }

                                //CCFBPrefs.Init();
                                CCFBGlobal.LoadTypes();
                                
                                CCFBGlobal.getRegTemplatePath();
                                this.Visible = false;
                                this.Enabled = true;
                                this.ShowInTaskbar = false;
                            #if FASTTRACK
                                FastTrackForm formMain = new FastTrackForm(this);
                            #endif
                            #if CCFB
                                CCFBPrefs.Init();
                                formMain = new MainForm(this);
                                formMain.InitialiseForm();
                            #endif
                                formMain.ShowDialog();
                                resetForm();
                                break;
                            }
                            else
                            {
                                MessageBox.Show("The Password Does Not Match For This User");
                                tbPassword.Text = "";
                                this.Enabled = true;
                                tbPassword.Focus();
                                Application.DoEvents();
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please Select A User");
            }
            btnCancel.Tag = "";
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            this.Text = Application.ProductName.ToString();
            string[] splitData = CopyRightsDetail().Split((";").ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            label4.Text = Application.ProductName.ToString() + " version " + Application.ProductVersion.ToString() + Environment.NewLine
                + ProductDescription() + Environment.NewLine
                + splitData[0];
            label6.Text = "";
            for (int i = 1; i < splitData.Length; i++)
            {
                label6.Text += splitData[i].TrimStart() + Environment.NewLine;
            }
            label5.Text = Application.ExecutablePath + Environment.NewLine + Environment.NewLine;

            if (CCFBGlobal.IsConnectedToInternet() == true || CCFBGlobal.isConnectedToNetwork() == true)
                label5.Text += "CONNECTED TO NETWORK";
            else
                label5.Text += "No Network Found";
            Application.DoEvents();
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
                        try
                        {
                            cboServers.SelectedIndex = cboServers.Items.Count - 1;
                        }
                        catch (Exception ex)
                        {
                        }
                        
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
            if (CCFBGlobal.sq1ServerName != ServerNameFromRegistry)
            {
                CCFBGlobal.saveRegSQLServer();
                ServerNameFromRegistry = CCFBGlobal.sq1ServerName;
            }
            if (CCFBGlobal.currentUser_Name != UserFromRegistry)
            {
                CCFBGlobal.saveRegDfltUser();
                UserFromRegistry = CCFBGlobal.currentUser_Name;
            }
            if (CCFBGlobal.serverName != "" && CCFBGlobal.getRegUpdatePath() == "") 
            {
                if(CCFBGlobal.serverName == System.Environment.MachineName)
                    CCFBGlobal.saveRegUpdatePath( @"\Users\Public\ClientcardFB3\ClientcardFB3.exe");
                else
                    CCFBGlobal.saveRegUpdatePath( @"\Public\ClientcardFB3\ClientcardFB3.exe");
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
                logIn(cboUsers.Text.ToUpper(), tbPassword.Text.ToUpper());
            }
            else
            {
                if (e.Control == true && e.Shift == true)
                {
                    if (tbPassword.Text == "@#")
                    {
                        logIn("ADMIN", sAdminPwd.ToUpper());
                    }
                }
            }
        }

        private bool TestSQLConnection(string server)
        {
            string tmp = server;
            string errmsg = "";
            int trycntr = 0;
            bool retVal = false;
            btnCancel.Tag = "testsql";
            if (tmp == "")
                tmp = CCFBGlobal.sq1ServerName;
            cboUsers.Items.Clear();
            while (trycntr < 2)
            {
                trycntr++;
                lblTestStatus.Text = "Testing Server Connection [" + trycntr + "] " + tmp;
                lblTestStatus.Visible = true;
                Application.DoEvents();
                if (cancelLogIn==true) 
                {
                    cancelLogIn = false;
                    btnCancel.Tag = "";
                    lblTestStatus.Text = "";
                    lblTestStatus.Visible = false;
                    return false;
                }
                try
                {
                    CCFBGlobal.connectionTimeout = 10 * trycntr;
                    CCFBGlobal.setConnectionString(server);
                    System.Data.SqlClient.SqlConnection conn = new SqlConnection(CCFBGlobal.connectionString);
                    conn.Open();
                    conn.Close();
                    trycntr = 10;
                    retVal = true;
                }
                catch (SqlException ex)
                {
                    errmsg = ex.Message.ToString();
                }
            }
            if (retVal == false)
                MessageBox.Show("Error Connecting to Server " + CCFBGlobal.sq1ServerName + "\r\n" + errmsg);
            btnCancel.Tag = "";
            lblTestStatus.Visible = false;
            Application.DoEvents();
            return retVal;
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
                if (CCFBGlobal.sq1ServerName != "")
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
                    if (CCFBGlobal.sq1ServerName != "")
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
            if (dsnServer == null)
                dsnServer = "";
            if (dsnServer == "" || dsnServer.ToUpper() != CCFBGlobal.sq1ServerName.ToUpper())
            {
                try
                {
                    Registry.SetValue(CCFBGlobal.ODBC_INI_REG_PATH, "Description", "The ODBC Connection For Access Reports");
                    Registry.SetValue(CCFBGlobal.ODBC_INI_REG_PATH, "DataBase", CCFBGlobal.DefaultDatabase);
                    Registry.SetValue(CCFBGlobal.ODBC_INI_REG_PATH, "Driver", dsnDriverName);
                    Registry.SetValue(CCFBGlobal.ODBC_INI_REG_PATH, "LastUser", "CCFB_User");
                    Registry.SetValue(CCFBGlobal.ODBC_INI_REG_PATH, "Server", CCFBGlobal.sq1ServerName);
                }
                catch (Exception ex)
                {
                    CCFBGlobal.appendErrorToErrorReport(dsnDriverName, ex.GetBaseException().ToString());
                }
            }
        }
    }
}
