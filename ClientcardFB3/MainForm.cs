using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Win32;

namespace ClientcardFB3
{
    public partial class MainForm : Form, IMainForm
    {
        public static DailyItemsClass clsDailyItems;
        #region Variables
        string[] ageGroups;     //An array of age groups used to set AgeGroup in HHMem listview

        private static Bitmap bmpScreenshot;
        bool bOriValue = false;
        List<ComboBox> cboList = new List<ComboBox>();
        List<CheckBox> chkList = new List<CheckBox>();  //Collection of all Checkboxes
        List<CheckBox> chksChanged = new List<CheckBox>();
        Client clsClient;
        DaysOpen clsDaysOpen;
        UserFields clsUserFields;

        Zipcodes clsZipcodes;
        SqlCommand command;
        SqlConnection conn;
        const int constHHTabWidth = 294;
        bool ethnicitySelectedFirstTime = false;
        bool allowNewServiceOverRide = false;

        bool formClear = false;
        EditServiceShortForm frmEditServices = new EditServiceShortForm();
        ClientSearch frmClientSearch;
        HDBuildings clsHdBuildings = new HDBuildings(CCFBGlobal.connectionString);
        FindClientForm frmFindClient;   //A pointer to the FindClientForm
        ILoginForm frmLogIn;
        TrxLogForm frmTrxLog;
        //CreateUnitedWayExport clsCreateUWExport;
        BarCodeEntryForm frmBarCodeEntry; //= new BarCodeEntryForm();
        private static Graphics gfxScreenshot;
        List<IncomeGroupMatrix> incomeGroups = new List<IncomeGroupMatrix>();

        bool inEditMode = false;    //Used to tell if in Edit mode or not
        bool loadingInfo = true;    //Stops events from firing if loading
        bool logout = false;
        string svcWarningText = "";
        Image photo;
        string sBackPackList = "";
        string sCSFPList = "";
        string clientBDays = "";
        List<TextBox> tbList = new List<TextBox>();     //Collection of Editable Textboxes
        List<TextBox> tbmList = new List<TextBox>();     //Collection of Members Controled Textboxes
        List<TextBox> tbdList = new List<TextBox>();     //Collection of Date non-editable Textboxes

        private TabPage tpTmpUserFields;

        List<string> userFlagNames = new List<string>();    //Collection of all UserFlag Names
        
        int curtrxLogID = 0;
        //const int lvTrxLogIDItem = 18;
        const int trxlogFldDisplay = 20;
        const int trxlogFldTrxId = 17;
        const int trxlogFldTrxStatus = 20;
        int transferMemId = -1;
        string rtAlert = "";
        DateTime ptsWeekOf;
        int ptsDoW = 0;
        HHPoints clsPts;

        #endregion
        const string constTrxLogPeriod = "TrxLogPeriod";
        const string constVoucherLogPeriod = "VoucherLogPeriod";
        Client.statsTrx clientTrxStats = new Client.statsTrx();


        public MainForm(ILoginForm FrmLogInIn)
        {
            frmLogIn = FrmLogInIn;
        }
        
        // Initialises the variables in the form
        public void InitialiseForm(){
            InitializeComponent();
            conn = new SqlConnection(CCFBGlobal.connectionString);

            for (int i = 0; i < chkLstBxUserFields.Items.Count; i++)
            {
                userFlagNames.Add(chkLstBxUserFields.Items[i].ToString());
            }

            cboTrxLogPeriod.SelectedIndex = Convert.ToInt32(Registry.GetValue(CCFBGlobal.registryKeyCurrentUser, constTrxLogPeriod, 3));
            cboVoucherLogPeriod.SelectedIndex = Convert.ToInt32(Registry.GetValue(CCFBGlobal.registryKeyCurrentUser, constVoucherLogPeriod, 3)); ;


            dgvHHMembers.ColumnHeadersDefaultCellStyle.Font =
                new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Regular,
                    System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            //dataGridMembers.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.DarkRed;
            CCFBGlobal.LoadTypes();
            clsClient = new Client();
            clsZipcodes = new Zipcodes(CCFBGlobal.connectionString);
            clsUserFields = new UserFields(CCFBGlobal.connectionString);
            clsDaysOpen = new DaysOpen(CCFBGlobal.connectionString);
            clsHdBuildings.openWhere("");

            frmFindClient = new FindClientForm(this);
            frmClientSearch = new ClientSearch(CCFBGlobal.connectionString, this);

            clsClient.open(frmFindClient.CurrentHHId, true, true);
            clsDailyItems = new DailyItemsClass();

            //Fills the AgeGroups array
            ageGroups = new string[6];
            ageGroups[0] = "Inf";
            ageGroups[1] = "Yth";
            ageGroups[2] = "Teen";
            ageGroups[3] = "18";
            ageGroups[4] = "Adlt";
            ageGroups[5] = "Sen";

            //Loads the Collections of Textbox's, ComboBox's and CheckBox's.
            traverseAndAddControlsToCollections(this.Controls);

            //Sets visibility level on the menu items 
            //depending on CurrentUsers Permissions Level
            setPermissionsForMenu();
            mnuClient_SchSupply.Visible = CCFBPrefs.EnableSchSupply;

            SetEnvironmentFromPrefs();
            if (CCFBPrefs.EnablePointsTracking == true)
            {
                clsPts = new HHPoints(conn);
            }
            SetCurrentServiceDate();    //Sets Default Service Date
            SetCurrentApptDate();       //Sets Default Appt Date

            loadParmData();         //Loads the ComboBox's with the Parm Data
            chkLstBxUserFields.Enabled = false;
            loadUserFieldLabels();  //Loads User Check Box Labels
            PopulatelvIncomeGroups();
            changeEditMode(false);
            splitContainer2.SplitterDistance = 94;
            if (clsClient.clsHH.RowCount > 0)
            {
                setHousehold(clsClient.clsHH.ID, 0);// Calls fillForm with the Household data
            }
            else
            {
                clearForm();
            }
        }

        private void addHousehold(int memID)
        {
            AddNewHousehold2 frmAddNewClient = new AddNewHousehold2(clsClient);
            if (memID > 0)
            {
                frmAddNewClient.setHHMember(new HHMemberItem(clsClient.clsHHmem.DRowHhm,clsClient.clsHHmem.DSet.Tables[0].Columns
                                                            ,clsClient.clsHHmem.DRowDemograhics, clsClient.clsHHmem.DSet.Tables[1].Columns)); 
            }
            //frmAddNewClient.TopMost = false;
            frmAddNewClient.ShowDialog(this);
            if (frmAddNewClient.HHID > 0)
            {
                clsClient.open(frmAddNewClient.HHID, true, true);
                if (clsClient.clsHH.UseFamilyList == true)
                {
                    clsClient.UpdateDataBasedOn(DateTime.Parse(CCFBGlobal.DefaultServiceDate));
                }
                if (CCFBPrefs.FindClientAutoRefresh == true)
                {
                    frmFindClient.loadList();
                    //Thread thread = new Thread(new ThreadStart(frmFindClient.loadList));
                    //thread.IsBackground = true;
                    //thread.Start();
                }
            }
            else
            { clsClient.RefreshHousehold(); }
            fillForm();
        }

        /// <summary>
        /// Retrives the Age Group Name From the AgeGroups Array
        /// </summary>
        /// <param name="age">The Integer value from database and 
        /// corresponding index in array for the age group to retrive</param>
        /// <returns></returns>
        private string AgeGroupName(int age)
        {
            if (age >= 0 && age <= 6)
                return ageGroups[age];
            else
                return "unk";
        }

        /// <summary>
        /// Event triggered when the Edit button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBeginEdit_Click(object sender, EventArgs e)
        {
            if (inEditMode == true)
            {
                saveClientRecord();
                enabletsbNewService();
            }
            else
            {
                changeEditMode(true);
                tbeAddress.Focus();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cancelEdit();
        }

        private void btnColapse_Click(object sender, EventArgs e)
        {
            splitMemTrans.Panel1Collapsed = false;
            btnEnlarge.Visible = true;
            btnColapse.Visible = false;
            btnVoucherColapse.Visible = false;
            btnVoucherEnlarge.Visible = true;
        }

        private void btnDeleteHHMem_Click(object sender, EventArgs e)
        {
            deleteHHMem();
        }

        private void btnEditTransLog_Click(object sender, EventArgs e)
        {
            if (lvTrxLog.SelectedItems.Count > 0)
            {
                if (lvTrxLog.SelectedItems[0].SubItems[3].Text != "CSFP")
                {
                    curtrxLogID = Convert.ToInt32(lvTrxLog.SelectedItems[0].Tag);
                    openEditServiceTrx(curtrxLogID);
                }
            }
        }

        private void btnEnlarge_Click(object sender, EventArgs e)
        {
            splitMemTrans.Panel1Collapsed = true;
            btnEnlarge.Visible = false;
            btnColapse.Visible = true;
            btnVoucherEnlarge.Visible = false;
            btnVoucherColapse.Visible = true;
        }

        /// <summary>
        /// Event Triggers when next client button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNext_Click(object sender, EventArgs e)
        {
            int hhid = clsClient.clsHH.ID;
            clearForm();
            frmFindClient.getNextClient(hhid);
        }

        /// <summary>
        /// Event triggered when the Previous Client Buttton is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrevious_Click(object sender, EventArgs e)
        {
            int hhid = clsClient.clsHH.ID;
            clearForm();
            frmFindClient.getPrevClient(hhid);
        }

        private void cancelEdit()
        {
            changeEditMode(false);
            clsClient.RefreshHousehold();
            fillForm();
            btnBeginEdit.Text = "Begin Edit";
            btnCancel.Visible = false;
            btnNext.Enabled = true;
            btnPrevious.Enabled = true;
            toolStrip1.Enabled = true;
            toolStrip2.Enabled = true;
            dgvHHMembers.BackgroundColor = Color.Cornsilk;
            this.ActiveControl = tbID;
        }

        /// <summary>
        /// Captrues the Active Form, saves it as a ScreenShot, and Prints that ScreenShot.
        /// </summary>
        private void CaptureAndPrintForm()
        {
            foreach (CheckBox cb in chkList)
            {
                cb.Enabled = true;
            }

            bmpScreenshot = new Bitmap(this.Bounds.Width, this.Bounds.Height, PixelFormat.Format32bppArgb);
            // Create a graphics object from the bitmap
            gfxScreenshot = Graphics.FromImage(bmpScreenshot);
            // Take the screenshot from the upper left corner to the right bottom corner
            gfxScreenshot.CopyFromScreen(this.Bounds.X,
                this.Bounds.Y, 0, 0, this.Bounds.Size,
                CopyPixelOperation.SourceCopy);

            //Bitmap printscreen = new Bitmap(this.Width, this.Height);

            //Graphics graphics = Graphics.FromImage(printscreen as Image);

            //graphics.CopyFromScreen(0, 0, 0, 0, printscreen.Size);

            foreach (CheckBox cb in chkList)
            {
                cb.Enabled = false;
            }
            string tmp = System.IO.Path.Combine(CCFBGlobal.pathScreenshots,"HH-" + clsClient.clsHH.ID.ToString() + ".jpg");
            if (File.Exists(tmp) == true)
            {
                File.Delete(tmp);
            }
            bmpScreenshot.Save(tmp, ImageFormat.Jpeg);

            photo = Image.FromFile(tmp);

            printDocument1.DefaultPageSettings.Landscape = true;
            dlg.Document = printDocument1;

            DialogResult result = dlg.ShowDialog();

            // If the result is OK then print the document.
            if (result == DialogResult.OK)
            {
                printDocument1.Print();
            }
            Application.DoEvents();
            photo = Image.FromFile(CCFBGlobal.fb3TemplatesPath + "Tmp.bmp");
            gfxScreenshot.Clear(Color.White);
            bmpScreenshot = null;
            printDocument1.DocumentName = null;
        }

        private void cboClientType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (loadingInfo == false)
            clsClient.clsHH.ClientType = ((parmType)cboClientType.SelectedItem).ID;
        }

        private void cboIDType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (inEditMode == true)
            {
                clsClient.clsHH.IdType = ((parmType)cboIDType.SelectedItem).ID;

                if (cboIDType.SelectedItem.ToString() != "None")
                {
                    clsClient.clsHH.DateIDVerified = DateTime.Today;
                    tbeDateIDVerified.Text = DateTime.Today.ToString();
                    formatDate(ref tbeDateIDVerified);
                    chkNeedVerifyID.Checked = false;
                }
                else
                {
                    tbeDateIDVerified.Text = "";
                    clsClient.clsHH.DSet.Tables[0].Rows[0]["DateIDVerified"] = DBNull.Value;
                }
            }
        }

        private void cboHUDCategory_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (loadingInfo == false)
                clsClient.clsHH.HUDCategory = ((parmType)cboHUDCategory.SelectedItem).ID;
        }

        private void cboPhoneType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (loadingInfo == false)
            clsClient.clsHH.PhoneType = ((parmType)cboPhoneType.SelectedItem).ID;
        }

        private void cboSpecialLang_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (loadingInfo == false)
            clsClient.clsHH.EthnicSpeaking = ((parmType)cboSpecialLang.SelectedItem).ID;
        }

        private void cboTransportation_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (loadingInfo == false)
                clsClient.clsHH.Transportation = ((parmType)cboTransportation.SelectedItem).ID;
        }

        private void cboTrxLogPeriod_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Registry.SetValue(CCFBGlobal.registryKeyCurrentUser, constTrxLogPeriod, cboTrxLogPeriod.SelectedIndex);
            getClientLogForPeriod();

        }

        /// <summary>
        /// Changes the inEditMode mode state and either enables or dissables the 
        /// proper controls
        /// </summary>
        private void changeEditMode(bool bNewEditMode)
        {
            Color baseBackColor;
            bool baseEnabled;
            inEditMode = bNewEditMode;
            if (inEditMode == true)
            {
                baseBackColor = CCFBGlobal.bkColorBaseEdit; //Change backcolor to edit color
                baseEnabled = false;                        //disable toolbars
                btnBeginEdit.Text = "Save";
            }
            else
            {
                baseBackColor = CCFBGlobal.bkColorFormDflt; //Change backcolor to default
                baseEnabled = true;                         //enable toolbars
                btnBeginEdit.Text = "Begin Edit";
            }
            splCntrCardMembers.BackColor = baseBackColor;
            splCntrCardMembers.Panel1.BackColor = baseBackColor;
            dgvHHMembers.BackgroundColor = baseBackColor;
            setEditStateForControls(baseBackColor);    //Set the states of the controls for edit mode
            splitContainer2.Panel1.BackColor = baseBackColor;

            btnCancel.Visible = inEditMode;
            btnNext.Enabled = baseEnabled;
            btnPrevious.Enabled = baseEnabled;
            toolStrip1.Enabled = baseEnabled;
            toolStrip2.Enabled = baseEnabled;

            mnuClient_CancelEdit.Enabled = inEditMode;
            EnableIncomeGrp(inEditMode);
            EnableVerifyIdGrp(inEditMode);
        }

        /// <summary>
        /// Checks if Textbox value can be parsed to an int
        /// </summary>
        /// <param name="tb">the textbox to be parsed</param>
        /// <returns>Returns the int value</returns>
        private int checkParseTextboxToInt(ref TextBox tb)
        {
            try
            {
                return Int32.Parse(tb.Text);
            }
            catch (FormatException ex)
            {
                CCFBGlobal.appendErrorToErrorReport("Textbox =" + tb.Name,
                    ex.GetBaseException().ToString());
                tb.Text = clsClient.clsHH.GetDataValue(tb.Tag.ToString()).ToString();
                return 0;
            }
        }


        private void chkBabyServices_CheckedChanged(object sender, EventArgs e)
        {
            if (inEditMode == true)
                fillAutoAlert();
        }

        private void chkbox_Enter(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            cb.BackColor = CCFBGlobal.bkColorHasFocus;
            setCheckBoxLabelBackColor(cb.Tag.ToString(), CCFBGlobal.bkColorHasFocus);
        }

        private void chkbox_Leave(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            if (inEditMode == true)
            {
                cb.BackColor = CCFBGlobal.bkColorBaseEdit;
                setCheckBoxLabelBackColor(cb.Tag.ToString(), CCFBGlobal.bkColorBaseEdit);
            }
            else
            {
                cb.BackColor = CCFBGlobal.bkColorFormAlt;
                setCheckBoxLabelBackColor(cb.Tag.ToString(), CCFBGlobal.bkColorFormAlt);
            }
        }

        private void chkNeedCommSig_CheckedChanged(object sender, EventArgs e)
        {
            if (inEditMode == true)
            {
                fillAutoAlert();

                if (chkNeedCommSig.Checked == false)
                    clsClient.clsHH.TEFAPSignDate = DateTime.Today;
            }
        }

        /// <summary>
        /// Deals with a CheckBox checked state being changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkList_CheckedChanged(object sender, EventArgs e)
        {
            
            CheckBox chkHH = (CheckBox)sender;
            if (chkHH.Focused == true)
            {
                bool foundInChanged = false;

                foreach (CheckBox cb in chksChanged)
                {
                    if (chkHH == cb)
                        foundInChanged = true;
                }

                if (foundInChanged == false)
                    chksChanged.Add(chkHH);

                if (inEditMode == true)
                {
                    if (chkHH.Checked.ToString() != clsClient.clsHH.GetDataValue(chkHH.Tag.ToString()).ToString())
                    {
                        clsClient.clsHH.SetDataValue(chkHH.Tag.ToString(), chkHH.Checked.ToString());
                    }
                }
                else
                {
                    if (clsClient.clsHH.GetDataValue(chkHH.Tag.ToString()).ToString().Trim() != "")
                    {
                        chkHH.Checked = bool.Parse(clsClient.clsHH.GetDataValue(chkHH.Tag.ToString()).ToString());
                    }
                }
            }
        }

        private void chkList_KeyDown(object sender, KeyEventArgs e)
        {
            if (inEditMode == true && e.KeyCode == Keys.Enter)
            {
                CheckBox chkHH = (CheckBox)sender;
                chkHH.Checked = !chkHH.Checked;
            }
        }

        private void chkLstBxUserFields_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (loadingInfo == false)
            {
                if (chkLstBxUserFields.Items[e.Index].ToString() == "")
                    e.NewValue = CheckState.Indeterminate;
                else
                {
                    if (e.NewValue == CheckState.Checked)
                        clsClient.clsHH.SetDataValue("UserFlag" + e.Index.ToString(), true);
                    else
                        clsClient.clsHH.SetDataValue("UserFlag" + e.Index.ToString(), false);
                }
            }
        }

        private void chkNoCommodities_CheckedChanged(object sender, EventArgs e)
        {
            bool bIsVisible = (chkNoCommodities.Checked == false);
            tbdLastComodity.Visible = bIsVisible;
            chkNeedCommSig.Visible = bIsVisible;
            lblNeedCommodSignature.Visible = bIsVisible;
            lblLastComm.Visible = bIsVisible;
            tbxNbrTEFAPThisMonth.Visible = bIsVisible;
            lblNbrTEFAP.Visible = bIsVisible;
        }

        /// <summary>
        /// Event triggered when the UseFamilyList checkbox is checked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkUseFamList_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUseFamList.Checked == false)
            {
                if (loadingInfo == false)
                {
                    clsClient.clsHH.UseFamilyList = false;
                    //clsClient.clsHH.update(true);
                }
            }
            else
            {//If UseFamilyList is checked, none are editable
                clsClient.clsHH.UseFamilyList = true;
                //Recalculate the ages
                clsClient.UpdateDataBasedOn(Convert.ToDateTime(CCFBGlobal.DefaultServiceDate));
                //Set total family
                clsClient.clsHH.TotalFamily = clsClient.clsHH.Infants + clsClient.clsHH.Youth + clsClient.clsHH.Teens
                    + clsClient.clsHH.Eighteens + clsClient.clsHH.Adults + clsClient.clsHH.Seniors;
                //Update the Textbox
                tbTotalFam.Text = clsClient.clsHH.TotalFamily.ToString();
                tbmInfants.Text = clsClient.clsHH.Infants.ToString();
                tbmYouth.Text = clsClient.clsHH.Youth.ToString();
                tbmTeens.Text = clsClient.clsHH.Teens.ToString();
                tbmAdults.Text = clsClient.clsHH.Adults.ToString();
                tbmSeniors.Text = clsClient.clsHH.Seniors.ToString();
                //Update the Household table
                //clsClient.clsHH.update(true);
                //chksChanged.Clear();
            }

            setFamilyStatsMode();
        }

        private void chkVerifyID_CheckedChanged(object sender, EventArgs e)
        {
            if (inEditMode == true)
                fillAutoAlert();
        }

        /// <summary>
        /// Clears the MainForm of all existing client data and sets
        /// appropriate items to be dissabled
        /// </summary>
        private void clearForm()
        {
            loadingInfo = true;
            formClear = true;
            lblName.Text = "";
            tbPhone.Text = "";
            foreach (TextBox tb in tbList)
            {
                //if (tb.Name == "tbeName")
                //{
                //    string s = "";
                //}
                tb.Text = "";
            }
            foreach (CheckBox cb in chkList)
            {
                cb.Checked = false;
            }
            for (int i = 0; i < chkLstBxUserFields.Items.Count; i++)
            {
                chkLstBxUserFields.SetItemChecked(i, false);
            }
            cboPhoneType.SelectedValue = "9";
            cboIDType.SelectedValue = "4";
            cboSpecialLang.SelectedValue = "1";
            cboClientType.SelectedValue = "0";
            btnBeginEdit.Enabled = false;
            tsbCreateAppt.Enabled = false;
            tsbNewService.Enabled = false;
            tsbPrintClientcard.Enabled = mnuClient_PrintClientCard.Enabled = false;
            tsbHHMem.Enabled = false;
            lvTrxLog.Items.Clear();
            dgvHHMembers.Rows.Clear();
            //            btnManageHHMem.Enabled = false;
            //btnNext.Enabled = false;
            //btnPrevious.Enabled = false;
            loadingInfo = false;
            mnuClient_BeginEditClient.Enabled = false;
            mnuClient_CancelEdit.Enabled = false;
            mnuClient_SaveHHChanges.Enabled = false;
            mnuClient_DeleteClient.Enabled = false;
            //menuHHMembers.Enabled = false;
            menuTrx.Enabled = false;
        }

        /// <summary>
        /// Closes a connection to the Database
        /// </summary>
        private void closeConnection()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        private void cmsHHMembers_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            cmsHHMembers.Close();
            switch (e.ClickedItem.Tag.ToString())
            {
                //case "Expand HH Members":
                //    {
                //        splitContainer2.Panel1Collapsed = false;
                //        splitContainer2.Panel2Collapsed = true;
                //        cmsHHMembers.Items[0].Text = "Shrink";
                //        cmsHHMembers.Items[1].Text = "Export To Excel";
                //        break;

                //    }
                //case "Shrink":
                //    {
                //        splitContainer2.Panel1Collapsed = false;
                //        splitContainer2.Panel2Collapsed = false;
                //        cmsHHMembers.Items[0].Text = "Expand HH Members";
                //        cmsHHMembers.Items[1].Text = "Export To Excel";
                //        break;
                //    }
                case "MOVE":
                    {
                        if (MessageBox.Show("Are you sure you want to move family member " + Environment.NewLine + "'" + dgvHHMembers.CurrentRow.Cells["clmLastName"].Value.ToString()
                            + ", " + dgvHHMembers.CurrentRow.Cells["clmFirstName"].Value.ToString() + "'" + Environment.NewLine + " to a different client's household?",
                            "Move Household Family Member", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            transferMemId = Convert.ToInt32(dgvHHMembers.CurrentRow.Cells["clmHMID"].Value);
                            frmFindClient.TransferHHMemMode = true;
                            frmFindClient.Visible = true;
                        }

                        break;
                    }
                case "EXPORT":
                    {
                        CCFBGlobal.ExportToExcel(dgvHHMembers, "HouseholdMembers_"
                            + clsClient.clsHH.Name.Replace(',', '_').Trim().ToUpper());
                        break;
                    }
                case "NEW":
                        if (MessageBox.Show("Are you sure you want to create a new household using " + Environment.NewLine + "'" + dgvHHMembers.CurrentRow.Cells["clmLastName"].Value.ToString()
                            + ", " + dgvHHMembers.CurrentRow.Cells["clmFirstName"].Value.ToString() + "'",
                            "Create NEW Household using Family Member", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            transferMemId = Convert.ToInt32(dgvHHMembers.CurrentRow.Cells["clmHMID"].Value);
                            addHousehold(transferMemId);
                            //frmFindClient.TransferHHMemMode = true;
                            //frmFindClient.Visible = true;
                        }
                    break;
                default:
                    break;
            }
        }

        public void transferMember()
        {
            try
            {
                openConnection();
                command = new SqlCommand("Update HouseholdMembers Set HouseholdID = " + frmFindClient.CurrentHHId
                    + " Where ID = " + transferMemId.ToString(), conn);
                command.ExecuteNonQuery();
                closeConnection();

                if (MessageBox.Show("Go To Transfered Household?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == System.Windows.Forms.DialogResult.Yes)
                {
                    clsClient.open(frmFindClient.CurrentHHId, true, true);
                    clearForm();
                    fillForm();
                }
                else
                {
                    clsClient.clsHHmem.openHHID(clsClient.clsHH.ID);
                    loadHHMems(true);
                    clsClient.calcSpecialDietAndDissabled();
                    ShowFamData();
                }
            }
            catch (SqlException ex)
            {
                CCFBGlobal.appendErrorToErrorReport("Command Text = " +
                    command.CommandText, ex.GetBaseException().ToString());
            }
            frmFindClient.TransferHHMemMode = false;
        }

        private void cmsLog_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            //if (e.ClickedItem.Text == "Expand HH Transactions")
            if (e.ClickedItem.Name == "tsmiExpandLog")
            {
                if (e.ClickedItem.Text == "Expand HH Transactions")
                {
                    splitContainer2.Panel1Collapsed = true;
                    splitContainer2.Panel2Collapsed = false;
                    cmsLog.Items["tsmiExpandLog"].Text = "Shrink";
                    //cmsLog.Items[1].Text = "Export To Excel";
                }
                else 
                {
                    splitContainer2.Panel1Collapsed = false;
                    splitContainer2.Panel2Collapsed = false;
                    cmsLog.Items["tsmiExpandLog"].Text = "Expand HH Transactions";
                    //cmsLog.Items[1].Text = "Export To Excel";
                }
            }
            else if (e.ClickedItem.Name == "tsmiExportToExcel")
            {
                CCFBGlobal.ExportToExcell(lvTrxLog, "TrxLog_"
                    + clsClient.clsHH.Name.Replace(',', '_').Trim().ToUpper());
            }
            else if (e.ClickedItem.Name == "tsmiShowSignature")
            {
                TrxLogSig tls = new TrxLogSig(CCFBGlobal.connectionString);
                if (tls.LoadImage(Convert.ToInt32(lvTrxLog.SelectedItems[0].Tag),clsClient.clsHH.ID) == true)
                {
                    picSignature.Image = tls.SigImage;
                    picSignature.Visible = true;
                    cmsLog.Visible = false;
                    MessageBox.Show(this,"Close Signature Display");
                    picSignature.Visible = false;
                }
                else
                {
                    MessageBox.Show("No Signature Found");
                }
            }
        }

        private void CreateNewAppointment()
        {
            frmEditServices.initForm(clsClient, true, true, -1, clsClient.clsHHmem.getMemName(clsClient.ServingHHMemID, clsClient.clsHH.Name), tbAlert.Rtf);
            frmEditServices.ShowDialog();
            getClientLogForPeriod();
        }

        public void CreateNewFoodService()
        {
            int trxid = -1;
            if (CCFBPrefs.WarnSvcEachPerson == false)
            {
                clsClient.clsHHSvcTrans.openUsingDateRange(clsClient.clsHH.ID, Convert.ToDateTime(CCFBGlobal.DefaultServiceDate),
                    Convert.ToDateTime(CCFBGlobal.DefaultServiceDate));
                if (clsClient.clsHHSvcTrans.RowCount > 0)
                    trxid = clsClient.clsHHSvcTrans.TrxId;
            }
            if (clsClient.ServingHHMemID == 0)
            {
                clsClient.ServingHHMemID = clsClient.clsHHmem.getHeadHHId(clsClient.clsHH.ID);
            }
            frmEditServices.initForm(clsClient, true, false, trxid, clsClient.clsHHmem.getMemName(clsClient.ServingHHMemID, clsClient.clsHH.Name), tbAlert.Rtf);
            frmEditServices.ShowDialog();
            if (frmEditServices.TrxCanceled == false)
            {
                clsClient.clsHH.UpdateLatestServiceDates(CCFBGlobal.DefaultServiceDate);
                clsClient.open(clsClient.clsHH.ID, true, true);
                getClientLogForPeriod();
                fillForm();  //this routine contains enabletsbNewService
            }
            else
                enabletsbNewService();
        }

        private void dgvHHMembers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvHHMembers.Rows.Count > 0 && (e.ColumnIndex == 9 || e.ColumnIndex == 10))
            {
                dgvHHMembers.EndEdit();
                bOriValue = (bool)dgvHHMembers.Rows[e.RowIndex].Cells[e.ColumnIndex].
                    GetEditedFormattedValue(e.RowIndex, DataGridViewDataErrorContexts.Commit);
                clsClient.clsHHmem.SetDataValue(
                    dgvHHMembers[e.ColumnIndex, e.RowIndex].Tag.ToString(), bOriValue.ToString());
                clsClient.UpdateDataBasedOn(Convert.ToDateTime(CCFBGlobal.DefaultServiceDate));
                clsClient.clsHHmem.update(true);
                tbmDiet.Text = clsClient.clsHH.SpecialDiet.ToString();
                tbmDisabled.Text = clsClient.clsHH.Disabled.ToString();
                dgvHHMembers[e.ColumnIndex, e.RowIndex].ReadOnly = false;
            }
        }

        private void dgvHHMembers_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvHHMembers.Focused == true && loadingInfo == false)
            {
                int rowNbr = dgvHHMembers.CurrentRow.Index;
                int colNbr = dgvHHMembers.CurrentCell.ColumnIndex;
                if (rowNbr <= clsClient.clsHHmem.RowCount - 1)
                {
                    if (colNbr != 9 || colNbr != 10)
                    {
                        try
                        {
                            if (dgvHHMembers.Columns[colNbr].ReadOnly == false)
                            {
                                clsClient.clsHHmem.SetDataValue(dgvHHMembers.CurrentCell.Tag.ToString(),
                                    dgvHHMembers.CurrentCell.GetEditedFormattedValue(e.RowIndex, DataGridViewDataErrorContexts.Commit).ToString());

                                clsClient.UpdateDataBasedOn(Convert.ToDateTime(CCFBGlobal.DefaultServiceDate));

                                clsClient.clsHHmem.Modified = DateTime.Now;
                                clsClient.clsHHmem.ModifiedBy = CCFBGlobal.currentUser_Name;
                                if (clsClient.clsHHmem.update(true) == true)
                                {
                                    dgvHHMembers.Rows[rowNbr].Cells["clmGroup"].Value =
                                        ageGroups[clsClient.clsHHmem.AgeGroup];
                                    dgvHHMembers.Rows[rowNbr].Cells["clmAge"].Value =
                                        clsClient.clsHHmem.Age;
                                    ShowFamData();
                                }
                            }
                        }
                        catch
                        {
                            dgvHHMembers.Rows[rowNbr].Cells[dgvHHMembers.CurrentCell.ColumnIndex].Value =
                                clsClient.clsHHmem.DSet.Tables[0].Rows[rowNbr].Field<DateTime>
                                (dgvHHMembers.CurrentCell.Tag.ToString()).ToShortDateString();
                        }
                    }
                }
                else
                {
                    dgvHHMembers.Rows[rowNbr].Cells[dgvHHMembers.CurrentCell.ColumnIndex].Value = "";
                }

                Color foreClr = new Color();
                if ((bool)dgvHHMembers.Rows[e.RowIndex].Cells[0].Value == true)
                    foreClr = Color.Maroon;
                else
                    foreClr = Color.Black;
                for (int i = 0; i < dgvHHMembers.ColumnCount; i++)
                {
                    dgvHHMembers.Rows[e.RowIndex].Cells[i].Style.ForeColor = foreClr;
                }
            }
        }

        private void dgvHHMembers_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvHHMembers.Columns.Count - 1)
                dgvHHMembers.CurrentCell = dgvHHMembers[0, e.RowIndex];
        }

        private void dgvHHMembers_DoubleClick(object sender, EventArgs e)
        {
            HHMemGridForm2 frmHHmem = new HHMemGridForm2(clsClient,
               Convert.ToInt32(dgvHHMembers.CurrentRow.Cells["clmHMID"].Value));
            frmHHmem.ShowDialog();
        }

        private void dgvHHMembers_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (loadingInfo == false)
                    clsClient.clsHHmem.find(Convert.ToInt32(dgvHHMembers.Rows[e.RowIndex].Cells["clmHMID"].Value));
            }
            catch { }
        }

        private void deleteHHMem()
        {
            if (dgvHHMembers.CurrentRow != null)
            {
                if ((bool)dgvHHMembers.CurrentRow.Cells["clmHeadHH"].Value != true
                    && MessageBox.Show("Are You Sure You Want To Delete "
                    + dgvHHMembers.CurrentRow.Cells["clmLastName"].Value.ToString().ToUpper()
                    + ", " + dgvHHMembers.CurrentRow.Cells["clmFirstName"].Value.ToString().ToUpper() + "?",
                    "Delete Household Member", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes
                    && dgvHHMembers.RowCount > 1)
                {
                    clsClient.clsHHmem.delete(Convert.ToInt32(dgvHHMembers.CurrentRow.Cells["clmHMId"].Value));
                    clsClient.clsHHmem.openHHID(clsClient.clsHH.ID);
                    fillForm();
                }
                else
                {
                    if ((bool)dgvHHMembers.CurrentRow.Cells["clmHeadHH"].Value == true)
                    {
                        MessageBox.Show("You Are Trying To Delete The Head Of Household OR The "
                        + "Last Household Memeber. If you would like to delete this "
                        + "Household Member Please either change the Head of Household "
                        + "OR Delete the Household under the Client Menue");
                    }
                }
            }
        }

        private void editHHMem()
        {
            int hhMemID = 0;
            if (dgvHHMembers.CurrentRow != null)
            {
                hhMemID = Convert.ToInt32(dgvHHMembers.CurrentRow.Cells["clmHMId"].Value);
                clsClient.clsHHmem.find(hhMemID);
            }
            HHMemGridForm2 frmHHMem = new HHMemGridForm2(clsClient, hhMemID);
            frmHHMem.ShowDialog();
            lblName.Text = clsClient.clsHH.Name;
            loadHHMems(true);
        }

        private void editServiceTrxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvTrxLog.FocusedItem != null)
            {
                openEditServiceTrx(Convert.ToInt32(lvTrxLog.FocusedItem.SubItems[16].Text));
            }
            else
            {
                MessageBox.Show("Please Select A Transaction To Edit and Try Again");
            }
        }

        private void EnableIncomeGrp(bool bEnabled)
        {
            tbIncomeMonthly.ReadOnly = !bEnabled;
            tbAnualIncome.ReadOnly = !bEnabled;
        }

        private void EnableVerifyIdGrp(bool bEnabled)
        {
            chkNeedVerifyID.Enabled = bEnabled;
            cboIDType.Enabled = bEnabled;
            tbeDateIDVerified.ReadOnly = !bEnabled;
        }

        private void mnuTools_GroceryRescueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FoodReceiptsGroceryRescueForm frmTemp = new FoodReceiptsGroceryRescueForm(-1, 6, true);
            frmTemp.Show();
        }

        /// <summary>
        /// Fills the AutoAlert Textbox with Proper info
        /// </summary>
        private void fillAutoAlert()
        {
            tbAlert.Text = "";
            tbAlert.BackColor = Color.LightYellow;
            rtAlert = "";
            if (clsClient.clsHH.RowCount > 0)
            {
                if (CCFBPrefs.AlertMonthSvc > 0)
                {
                    if (clientTrxStats.NbrTrxThisMonth >= CCFBPrefs.AlertMonthSvc)
                    {
                        tbAlert.SelectionColor = Color.Red;
                        tbAlert.SelectedText = CCFBPrefs.AlertMonthSvcText + Environment.NewLine;
                    }
                }
                if (CCFBPrefs.AlertWeekSvc > 0)
                {
                    if (clientTrxStats.NbrTrxThisWeek >= CCFBPrefs.AlertWeekSvc)
                    {
                        tbAlert.SelectionColor = Color.Red;
                        tbAlert.SelectedText = CCFBPrefs.AlertWeekSvcText + Environment.NewLine;
                    }
                }
                if (CCFBPrefs.WarnSvcEachPerson == true)
                {
                    if (svcWarningText != "")
                    {
                        string[] lines = svcWarningText.Split('~');
                        foreach (string item in lines)
                        {
                            tbAlert.SelectionColor = Color.IndianRed;
                            tbAlert.SelectedText = item + Environment.NewLine;
                        }
                    }
                }
                if (CCFBPrefs.AlertMinimumDays > 0 && tbxDaysSinceLstSrvc.Text != "")
                {
                    if (Convert.ToInt32(tbxDaysSinceLstSrvc.Text) < CCFBPrefs.AlertMinimumDays)
                    {
                        tbAlert.SelectionColor = Color.Red;
                        tbAlert.SelectedText = CCFBPrefs.AlertMinDaysText + Environment.NewLine;
                    }
                }
                if (CCFBPrefs.AlertMinimumMonths > 0 && tbxMonthsSinceLstSrvc.Text != "")
                {
                    if (Convert.ToInt32(tbxMonthsSinceLstSrvc.Text) < CCFBPrefs.AlertMinimumMonths)
                    {
                        tbAlert.SelectionColor = Color.Red;
                        tbAlert.SelectedText = CCFBPrefs.AlertMinMonthsText + Environment.NewLine;
                    }
                }

                if (clientBDays != "")
                {
                    tbAlert.SelectionColor = Color.Black;
                    tbAlert.SelectedText = clientBDays;
                }
                if (clsClient.clsHH.NeedToVerifyID == true && CCFBPrefs.EnableVerifyId == true)
                {
                    tbAlert.SelectionColor = Color.OrangeRed;
                    tbAlert.SelectedText = "Need Proof of Address" + Environment.NewLine;
                }
                if (clsClient.clsHH.NoCommodities == true && CCFBPrefs.EnableTEFAP == true)
                {
                    tbAlert.SelectionColor = Color.OrangeRed;
                    tbAlert.SelectedText = "NO COMMODITIES" + Environment.NewLine;
                }
                else if (clsClient.clsHH.NeedCommoditySignature == true && clsClient.clsHH.NoCommodities == false && CCFBPrefs.EnableTEFAP == true)
                {
                    tbAlert.SelectionColor = Color.OrangeRed;
                    tbAlert.SelectedText = "Need Commodity Signature" + Environment.NewLine;

                    if (clsClient.clsHH.TEFAPSignDate.ToShortDateString() != CCFBGlobal.OURNULLDATE
                        && clsClient.clsHH.TEFAPSignDate.ToShortDateString() != CCFBGlobal.OUROTHERNULLDATE)
                        tbAlert.SelectedText = " As Of " + clsClient.clsHH.TEFAPSignDate.ToShortDateString() + Environment.NewLine;
                }
                if (clsClient.clsHH.NeedIncomeVerification == true && CCFBPrefs.EnableHouseholdIncome == true)
                {
                    tbAlert.SelectionColor = Color.OrangeRed;
                    tbAlert.SelectedText = "Need Income Verification";

                    if (clsClient.clsHH.IncomeVerifiedDate.ToShortDateString() != CCFBGlobal.OURNULLDATE
                        && clsClient.clsHH.IncomeVerifiedDate.ToShortDateString() != CCFBGlobal.OUROTHERNULLDATE)
                        tbAlert.SelectedText = " As Of " + clsClient.clsHH.IncomeVerifiedDate.ToShortDateString();

                    tbAlert.SelectedText += Environment.NewLine;
                }
                if (clsClient.clsHH.ServiceMethod == (int)CCFBGlobal.ServiceMethodCodes.OneTimeService)
                {
                    tbAlert.SelectionColor = Color.OrangeRed;
                    tbAlert.SelectedText = "One Time Service Only" + Environment.NewLine;
                }
                if (clsClient.clsHH.SupplOnly && CCFBPrefs.EnableSupplemental == true)
                {
                    tbAlert.SelectionColor = Color.Blue;
                    tbAlert.SelectedText = "SUPPLEMENTAL SERVICE ONLY" + Environment.NewLine;
                }
                if (clsClient.clsHH.BabyServices && CCFBPrefs.EnableBabyServices == true)
                {
                    tbAlert.SelectionColor = Color.Blue;
                    tbAlert.SelectedText = "BABY SERVICES" + Environment.NewLine;
                }
                if (clsClient.clsHH.Infants > 0)
                {
                    for (int i = 0; i < clsClient.clsHHmem.RowCount; i++)
                    {
                        clsClient.clsHHmem.SetRecord(i);
                        if (clsClient.clsHHmem.Birthdate > DateTime.Today.AddMonths(-13))
                        {
                            tbAlert.SelectionColor = Color.Blue;
                            tbAlert.SelectedText = clsClient.clsHHmem.FirstName + " is less than 13 months old" + Environment.NewLine;
                        }
                    }
                }
                if (clsClient.clsHH.SurveyComplete && CCFBPrefs.EnableAdditionalHHMDataTab == true)
                {
                    tbAlert.SelectionColor = Color.Black;
                    tbAlert.SelectedText = "Survey Complete" + Environment.NewLine;
                }

                for (int i = 0; i < chkLstBxUserFields.Items.Count; i++)
                {
                    clsUserFields.setDataRow(i);
                    if (clsUserFields.AutoAlert == true)
                    {
                        if (chkLstBxUserFields.GetItemChecked(i) == true)
                        {
                            tbAlert.SelectionColor = Color.Maroon;
                            tbAlert.SelectedText = clsUserFields.AutoAlertText + Environment.NewLine;
                        }
                    }
                }
                if (sCSFPList != "")
                {
                    tbAlert.SelectionColor = Color.Green;
                    tbAlert.SelectedText = sCSFPList;
                }
                if (sBackPackList != "")
                {
                    tbAlert.SelectionColor = Color.Green;
                    tbAlert.SelectedText = sBackPackList;
                }
                if (clsClient.clsHH.SchSupplyFlag == true)
                {
                    tbAlert.SelectionColor = Color.Black;
                    tbAlert.SelectedText = "Registered For School Supplies" + Environment.NewLine;
                }
                rtAlert = tbAlert.Rtf;
                mergeRTF(tbAlert,rtAlert, clsClient.clsHH.AlertText);
            }
        }

        /// <summary>
        /// Fills the form with the data from the client in database
        /// </summary>
        public void fillForm()
        {
            clearForm();
            formClear = false;
            ethnicitySelectedFirstTime = false;
            loadingInfo = true;
            btnBeginEdit.Enabled = false;
            chksChanged.Clear();
            tbxDaysSinceLstSrvc.Text = "";
            tbxMonthsSinceLstSrvc.Text = "";
            clearPointsDisplay();
            if (clsClient.clsHH.RowCount > 0)
            {
                showUserInfo();
                lblName.Text = clsClient.clsHH.Name;
                tpgFamilyDetail.Text = "[" + clsClient.clsHH.TotalFamily.ToString() + "] Family Members";
                btnBeginEdit.Enabled = true;
                tsbCreateAppt.Enabled = true;
                tsbHHMem.Enabled = true;   
                btnNext.Enabled = true;
                btnPrevious.Enabled = true;
                tsbVouchers.Enabled = true;
                tsbPrintClientcard.Enabled = mnuClient_PrintClientCard.Enabled = CCFBPrefs.EnablePrintClientCard;
                if (clsClient.clsHH.BarCode > 0)
                    btnAssignBarcode.BackColor = Color.MediumAquamarine;
                else
                    btnAssignBarcode.BackColor = Color.Khaki;

                mnuClient_BeginEditClient.Enabled = true;
                mnuClient_SaveHHChanges.Enabled = true;
                mnuClient_DeleteClient.Enabled = true;
                //menuHHMembers.Enabled = true;
                menuTrx.Enabled = true;
                btnEditTransLog.Enabled = false;
                curtrxLogID = 0;
                tbTotalFam.Text = (clsClient.clsHH.Infants + clsClient.clsHH.Youth + clsClient.clsHH.Teens
                + clsClient.clsHH.Eighteens + clsClient.clsHH.Adults + clsClient.clsHH.Seniors).ToString();

                foreach (TextBox tb in tbdList)
                {
                    tb.Text = "";
                }
                clsClient.getClientTrxStats(ref clientTrxStats, clsClient.clsHH.ID.ToString()
                    , Convert.ToDateTime(CCFBGlobal.DefaultServiceDate).ToShortDateString());
                tbxNbrTrxThisMonth.Text = clientTrxStats.NbrTrxThisMonth.ToString();
                tbxNbrTrxThisWeek.Text = clientTrxStats.NbrTrxThisWeek.ToString();
                tbxNbrTEFAPThisMonth.Text = clientTrxStats.NbrTEFAPThisMonth.ToString();
                tbxNbrSupplThisMonth.Text = clientTrxStats.NbrSupplThisMonth.ToString();
                DateTime dateTmp = CCFBGlobal.ValidDate(clsClient.clsHH.FirstSvcThisYear);
                if (DateTime.Compare(dateTmp, CCFBGlobal.CurrentFiscalStartDate()) >= 0)
                {
                    tbdFirstFiscal.Text = dateTmp.ToShortDateString();
                }
                else
                {
                    tbdFirstFiscal.Text = "...";
                }
                dateTmp = CCFBGlobal.ValidDate(clsClient.clsHH.FirstCalService);
                if (DateTime.Compare(dateTmp, DateTime.Parse("01/01/" + DateTime.Now.Year.ToString()) ) >= 0)
                {
                    tbdFirstCalService.Text = dateTmp.ToShortDateString();
                }
                tbdLastComodity.Text = CCFBGlobal.ValidDateString(clsClient.clsHH.LastCommodityService);
                tbdLastService.Text = CCFBGlobal.ValidDateString(clsClient.clsHH.LatestService);
                tbdLastSupplService.Text = CCFBGlobal.ValidDateString(clsClient.clsHH.LastSupplService);

                tbID.Text = clsClient.clsHH.GetDataValue(tbID.Tag.ToString()).ToString().Trim();
                foreach (TextBox tb in tbList)
                {
                    if (tb.Tag != null && tb.Tag.ToString().Trim() != "")
                    {
                        tb.Text = clsClient.clsHH.GetDataValue(tb.Tag.ToString()).ToString().Trim();
                    }
                }
                tbPhone.Text = clsClient.clsHH.GetDataValue(tbPhone.Tag.ToString()).ToString().Trim();
                foreach (TextBox tb in tbmList)
                {
                    if (tb.Tag != null && tb.Tag.ToString().Trim() != "")
                    {
                        tb.Text = clsClient.clsHH.GetDataValue(tb.Tag.ToString()).ToString().Trim();
                    }
                }

                //tbLastService.Text = CCFBGlobal.ValidDateString(clsClient.clsHH.LatestService);
                //tbLastComodity.Text = CCFBGlobal.ValidDateString(clsClient.clsHH.LastCommodityService);

                //formatDate(ref tbdLastService);
                //formatDate(ref tbdLastComodity);
                formatDate(ref tbeFirstService);
                formatDate(ref tbeDateIDVerified);

                foreach (ComboBox cb in cboList)
                {
                    try
                    {
                        int newval = Int32.Parse(clsClient.clsHH.GetDataValue(cb.Tag.ToString()).ToString());
                        cb.SelectedValue = newval.ToString();
                    }
                    catch (Exception)
                    {
                        cb.SelectedValue = 0;
                    }
                }

                //Sets the checkboxes in form
                foreach (CheckBox cb in chkList)
                {
                    if (cb.Tag != null && cb.Tag.ToString().Trim() != "")
                    {
                        if (clsClient.clsHH.GetDataValue(cb.Tag.ToString()).ToString() == "True")
                        {
                            cb.Checked = true;
                        }
                        else
                        {
                            cb.Checked = false;
                        }
                    }
                }

                //Sets the check boxes for the user fields
                setChecksInUserFields();

                //Sets number of days since last service
                DateTime dateTest = clsClient.clsHH.LatestService;
                if (dateTest != null && dateTest.Year > 1990)
                {
                    TimeSpan diff = DateTime.Today - dateTest;
                    tbxDaysSinceLstSrvc.Text = diff.Days.ToString();
                    tbxMonthsSinceLstSrvc.Text = System.Math.Abs(CCFBGlobal.MonthDiff(DateTime.Today, dateTest)).ToString();
                }
                if (clsClient.clsHH.AnnualIncome > 0)
                {
                    int monthlyincome = Convert.ToInt32(clsClient.clsHH.AnnualIncome) / 12;
                    tbIncomeMonthly.Text = monthlyincome.ToString();
                }
                else
                    tbIncomeMonthly.Text = "";

                SetIncomeGroups();

                loadHHMems(true);
                getClientLogForPeriod();
                getVoucherLogForPeriod();
                if (clsClient.clsHH.UseFamilyList == true)
                    tabFamily.SelectTab(0);
                else
                    tabFamily.SelectTab(1);
                btnBeginEdit.Enabled = true;
                loadingInfo = false;
                if (CCFBPrefs.CommSigValidFor > 0)
                {
                    if (chkNeedCommSig.Checked == false && clsClient.clsHH.TEFAPSignDate.AddMonths(CCFBPrefs.CommSigValidFor)
                        < DateTime.Today)
                    {
                        chkNeedCommSig.Checked = true;
                        clsClient.clsHH.NeedCommoditySignature = true;
                        clsClient.clsHH.update(false);
                    }
                }
                enabletsbNewService();
                showPoints();
            }
            else
            {
                clsClient.getClientTrxStats(ref clientTrxStats, "0"
                    , Convert.ToDateTime(CCFBGlobal.DefaultServiceDate).ToShortDateString());
                tsbNewService.Enabled = false;
                tsbHHMem.Enabled = false;
                btnNext.Enabled = false;
                btnPrevious.Enabled = false;
                btnBeginEdit.Enabled = false;
                mnuClient_BeginEditClient.Enabled = false;
                mnuClient_SaveHHChanges.Enabled = false;
                mnuClient_DeleteClient.Enabled = false;
                //menuHHMembers.Enabled = true;
                menuTrx.Enabled = false;
                btnEditTransLog.Enabled = false;
                tsbVouchers.Enabled = false;
            }
            ShowRoute();
            fillAutoAlert();
            Application.DoEvents();
        }

        /// <summary>
        /// Sets the check boxes for the user fields
        /// </summary>
        private void setChecksInUserFields()
        {
            for (int i = 0; i < chkLstBxUserFields.Items.Count; i++)
            {
                if (chkLstBxUserFields.Items[i].ToString() != "")
                {
                    if (clsClient.clsHH.DSet.Tables[0].Rows[0].Field<bool>(userFlagNames[i]) == true)
                        chkLstBxUserFields.SetItemCheckState(i, CheckState.Checked);
                    else
                        chkLstBxUserFields.SetItemCheckState(i, CheckState.Unchecked);
                }
            }
        }

        /// <summary>
        /// Fills the given cell with the proper value and sets the color scheme
        /// </summary>
        /// <param name="dgvRow">The DataGridView row that the cell exists in</param>
        /// <param name="ColName">The Column Name of the cell to fill</param>
        /// <param name="FieldName">The Field Name of the value to retrive from  the given row of the database</param>
        /// <param name="IsBoolean">If the value seeked is a bool value or not</param>
        /// <param name="CellForeColor">The Text Color Wanted</param>
        /// <param name="dsetRowIndex">The Row that you need from the database</param>
        private void FillGridMembersCell(DataGridViewRow dgvRow, String ColName, String FieldName, Boolean IsBoolean, Color CellForeColor, int dsetRowIndex)
        {
            DataGridViewCell dgvCell = dgvRow.Cells[ColName];
            if (IsBoolean)
            {
                if (dgvCell.EditType != null)
                {
                    if ((bool)clsClient.clsHHmem.DSet.Tables[0].Rows[dsetRowIndex][FieldName] == true)
                        dgvCell.Value = "Y";
                    else
                        dgvCell.Value = "";
                }
                else
                    dgvCell.Value = (bool)clsClient.clsHHmem.DSet.Tables[0].Rows[dsetRowIndex][FieldName];
            }
            else
            { dgvCell.Value = clsClient.clsHHmem.DSet.Tables[0].Rows[dsetRowIndex][FieldName]; }

            dgvCell.Tag = FieldName;

            if (dgvCell.Value == null)
                dgvCell.Value = "";

            dgvCell.Style.ForeColor = CellForeColor;
        }

        /// <summary>
        /// Formats a DateTime Textbox to proper format
        /// </summary>
        /// <param name="tb">Refrence to the Textbox thats Text needs to be formated</param>
        private void formatDate(ref TextBox tb)
        {
            if (tb.Text != "")
            {
                DateTime d = DateTime.Parse(tb.Text);
                if (d > DateTime.Parse(CCFBGlobal.OURNULLDATE))
                    tb.Text = d.ToShortDateString();
                else
                    tb.Text = "";
            }
        }

        private void getClientLogForPeriod()
        {
            switch (cboTrxLogPeriod.SelectedIndex)
            {
                case 0: //Current Month
                    {
                        //Finds current month and sets the range from the 1st to the last day of the month
                        int month = DateTime.Now.Month;
                        DateTime from = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                        DateTime to = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
                        //Opens Log for the date range
                        // clsClient.clsHHSvcTrans.openUsingDateRange(clsClient.clsHH.ID, from, to);
                        //Reload the listview for the Log
                        loadTransLog(" And TrxDate Between '" + from.ToString() + "' ANd '" + to.ToString() + "'");
                        break;
                    }
                case 1: //Last 90 Days
                    {
                        //Open the tansactions for the Date Range
                        //clsClient.clsHHSvcTrans.openUsingDateRange(clsClient.clsHH.ID, new DateTime
                        //    (year, month, DateTime.Today.Day), DateTime.Today);
                        //Reload the trans log listview
                        loadTransLog(" And TrxDate Between '" + new DateTime(
                            DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day).AddDays(-90).ToString()
                        + "' And '" + DateTime.Today.ToString() + "'");
                        break;
                    }
                case 2: //Current Calendar Year
                    {
                        //Open trans log for the date range
                        //clsClient.clsHHSvcTrans.openUsingDateRange(clsClient.clsHH.ID, new DateTime(DateTime.Today.Year, 1, 1),
                        //    new DateTime(DateTime.Today.Year, 12, 31));
                        //Reload the trans log listview
                        loadTransLog(" And TrxDate Between '" + new DateTime(DateTime.Today.Year, 1, 1).ToString()
                            + "' And '" + new DateTime(DateTime.Today.Year, 12, 31).ToString() + "'");
                        break;
                    }
                case 3: //Current Fiscal Year
                    {
                        //Open trans log for the date range
                        //clsClient.clsHHSvcTrans.openUsingDateRange(clsClient.clsHH.ID, CCFBGlobal.CurrentFiscalStartDate(),
                        //    CCFBGlobal.CurrentFiscalEndDate());
                        //Reload the translog listview
                        loadTransLog(" And TrxDate Between '" + CCFBGlobal.CurrentFiscalStartDate().ToString() + "' And '"
                         + CCFBGlobal.CurrentFiscalEndDate().ToString() + "'");
                        break;
                    }
                case 4: //Previous Calendar Year
                    {
                        //Open trans log for the date range
                        //clsClient.clsHHSvcTrans.openUsingDateRange(clsClient.clsHH.ID, new DateTime(DateTime.Today.Year - 1, 1, 1),
                        //    new DateTime(DateTime.Today.Year - 1, 12, 31));
                        //Reload the translog listview
                        loadTransLog(" And TrxDate Between '" + new DateTime(DateTime.Today.Year - 1, 1, 1).ToString() + "' ANd '"
                            + new DateTime(DateTime.Today.Year - 1, 12, 31).ToString() + "'");
                        break;
                    }
                case 5: //Previous Fiscal Year
                    {
                        //Open trans log for the date range
                        //clsClient.clsHHSvcTrans.openUsingDateRange(clsClient.clsHH.ID, CCFBGlobal.PreviousFiscalStartDate(),
                        //   CCFBGlobal.PreviousFiscalEndDate());
                        //Reload the translog listview
                        loadTransLog(" And TrxDate Between '" + CCFBGlobal.PreviousFiscalStartDate().ToString() + "' And '"
                            + CCFBGlobal.PreviousFiscalEndDate().ToString() + "'");
                        break;
                    }
                default://ALL
                    {
                        //Opens all transactions for the household
                        //clsClient.clsHHSvcTrans.openForHH(clsClient.clsHH.ID);
                        //Reloads the translog listview
                        loadTransLog("");
                        break;
                    }
            }
        }

        private void getVoucherLogForPeriod()
        {
            switch (cboVoucherLogPeriod.SelectedIndex)
            {
                case 0: //Current Month
                    {
                        //Finds current month and sets the range from the 1st to the last day of the month
                        int month = DateTime.Now.Month;
                        DateTime from = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                        DateTime to = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
                        //Opens Log for the date range and reloads the listview for the Log
                        loadVoucherLog(" And TrxDate Between '" + from.ToString() + "' ANd '" + to.ToString() + "'");
                        break;
                    }
                case 1: //Last 90 Days
                    {
                        //Open the tansactions for the Date Range and reloads the trans log listview
                        loadVoucherLog(" And TrxDate Between '" + new DateTime(
                            DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day).AddDays(-90).ToString()
                        + "' And '" + DateTime.Today.ToString() + "'");
                        break;
                    }
                case 2: //Current Calendar Year
                    {
                        //Open trans log for the date range and reloads the trans log listview
                        loadVoucherLog(" And TrxDate Between '" + new DateTime(DateTime.Today.Year, 1, 1).ToString()
                            + "' And '" + new DateTime(DateTime.Today.Year, 12, 31).ToString() + "'");
                        break;
                    }
                case 3: //Current Fiscal Year
                    {
                        //Open trans log for the date range and reloads the translog listview
                        loadVoucherLog(" And TrxDate Between '" + CCFBGlobal.CurrentFiscalStartDate().ToString() + "' And '"
                         + CCFBGlobal.CurrentFiscalEndDate().ToString() + "'");
                        break;
                    }
                case 4: //Previous Calendar Year
                    {
                        //Open trans log for the date range and reloads the translog listview
                        loadVoucherLog(" And TrxDate Between '" + new DateTime(DateTime.Today.Year - 1, 1, 1).ToString() + "' ANd '"
                            + new DateTime(DateTime.Today.Year - 1, 12, 31).ToString() + "'");
                        break;
                    }
                case 5: //Previous Fiscal Year
                    {
                        //Open vouchers log for the date range and reloads the translog listview
                        loadVoucherLog(" And TrxDate Between '" + CCFBGlobal.PreviousFiscalStartDate().ToString() + "' And '"
                            + CCFBGlobal.PreviousFiscalEndDate().ToString() + "'");
                        break;
                    }
                default://ALL
                    {
                        //Opens all vouchers for the householdand reloads the translog listview
                        loadVoucherLog("");
                        break;
                    }
            }
        }

        /// <summary>
        /// Loads the Household Members Listview with the Household Members
        /// </summary>
        private void loadHHMems(bool clearRows)
        {
            DateTime d;
            DateTime dateFirst = CCFBGlobal.FirstDayOfMonth(DateTime.Now);
            DateTime dateLast = CCFBGlobal.LastDayOfMonth(DateTime.Now);
            Color CellForeColor;
            DataGridViewRow dvr;
            bool oriloadingInfo = loadingInfo;
            loadingInfo = true;
            if (clearRows == true)
            {
                dgvHHMembers.Rows.Clear();
            }

            int rowCount = 0;
            int NbrCSFP = 0;
            sCSFPList = "";
            clientBDays = "";
            int NbrBackPack = 0;
            sBackPackList = "";
            for (int i = 0; i < clsClient.clsHHmem.RowCount; i++)
            {
                dgvHHMembers.Rows.Add();
                if ((bool)clsClient.clsHHmem.DSet.Tables[0].Rows[i]["Inactive"])
                { CellForeColor = Color.Maroon; }
                else
                { CellForeColor = Color.Black; }
                dvr = dgvHHMembers.Rows[rowCount];
                FillGridMembersCell(dvr, "clmInactive", "Inactive", true, CellForeColor, i);
                //                    FillGridMembersCell(dvr, "clmHeadHH", "HeadHH", true, CellForeColor, i);
                FillGridMembersCell(dvr, "clmLastName", "LastName", false, CellForeColor, i);
                FillGridMembersCell(dvr, "clmFirstName", "FirstName", false, CellForeColor, i);
                if ((bool)clsClient.clsHHmem.DSet.Tables[0].Rows[i]["HeadHH"] == true)
                    dvr.Cells["clmFirstName"].Value += " (HH)";
                FillGridMembersCell(dvr, "clmHMID", "ID", false, CellForeColor, i);
                if ((bool)clsClient.clsHHmem.DSet.Tables[0].Rows[i]["NotCounted"] == true)
                {
                }
                else
                {


                FillGridMembersCell(dvr, "clmAge", "Age", false, CellForeColor, i);
                FillGridMembersCell(dvr, "clmSex", "Sex", false, CellForeColor, i);
                FillGridMembersCell(dvr, "clmDiet", "SpecialDiet", true, CellForeColor, i);
                FillGridMembersCell(dvr, "Disabled", "IsDisabled", true, CellForeColor, i);
                if ((Boolean)clsClient.clsHHmem.DSet.Tables[0].Rows[i]["CSFP"] == true)
                {
                    string tmp = clsClient.clsHHmem.DSet.Tables[0].Rows[i]["CSFPExpiration"].ToString();
                    if (tmp != "")
                    {
                        d = (DateTime)clsClient.clsHHmem.DSet.Tables[0].Rows[i]["CSFPExpiration"];
                        if (d > CCFBGlobal.FBNullDateValue)
                        {
                            dvr.Cells["clmCSFP"].Value = d.ToShortDateString();
                        }
                    }
                    else
                    {
                        dvr.Cells["clmCSFP"].Value = tmp;
                    }
                    dvr.Cells["clmCSFP"].Tag = "CSFPExpiration";
                }


                if (clsClient.clsHHmem.DSet.Tables[0].Rows[i]["Birthdate"].ToString().Trim() != "")
                {
                    d = (DateTime)clsClient.clsHHmem.DSet.Tables[0].Rows[i]["Birthdate"];
                    dvr.Cells["clmBirthdate"].Value = d.ToShortDateString();
                    dvr.Cells["clmBirthdate"].Tag = "BirthDate";
                    DateTime newBirthday;
                    int thisYear = DateTime.Today.Year;

                    if (d.Month == 2 && d.Day == 29 && DateTime.IsLeapYear(thisYear) == false)
                        newBirthday = new DateTime(thisYear, d.Month, 28);
                    else
                        newBirthday = new DateTime(thisYear, d.Month, d.Day);

                    //if (newBirthday >= DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek) &&
                    //   newBirthday <= DateTime.Today.AddDays(7 - (int)DateTime.Today.DayOfWeek))
                    if (newBirthday >= DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek) &&
                        newBirthday <= DateTime.Today.AddDays(7 - (int)DateTime.Today.DayOfWeek))
                    {
                        if (newBirthday > DateTime.Today)
                            clientBDays += "Birthday: " + clsClient.clsHHmem.DSet.Tables[0].Rows[i]["FirstName"].ToString()
                                + " This " + newBirthday.DayOfWeek.ToString() + Environment.NewLine;
                        else if (newBirthday < DateTime.Today)
                            clientBDays += "Birthday: " + clsClient.clsHHmem.DSet.Tables[0].Rows[i]["FirstName"].ToString()
                                        + " Last " + newBirthday.DayOfWeek.ToString() + Environment.NewLine;
                        else
                            clientBDays += "Birthday: " + clsClient.clsHHmem.DSet.Tables[0].Rows[i]["FirstName"].ToString()
                                        + " Today" + Environment.NewLine;
                    }
                    else if ((newBirthday >= dateFirst) &&
                        (newBirthday <= dateLast))
                    {
                        clientBDays += "Birthday: " + clsClient.clsHHmem.DSet.Tables[0].Rows[i]["FirstName"].ToString()
                                    + " " + newBirthday.DayOfWeek.ToString()
                                    + " the " + newBirthday.Day.ToString() + Environment.NewLine;
                    }
                }
                }

                //Uses the Age to Add the proper AgeGroup to the listview
                int agegroup = Int32.Parse(clsClient.clsHHmem.DSet.Tables[0].Rows[i]["AgeGroup"].ToString());
                if (agegroup >= 0 && agegroup <= 5)
                    dvr.Cells["clmGroup"].Value = AgeGroupName(agegroup);
                if (CCFBPrefs.EnableCSFP == true)
                {
                    if ((bool)clsClient.clsHHmem.DSet.Tables[0].Rows[i]["CSFP"] == true)
                    {
                        NbrCSFP++;
                        sCSFPList += "CSFP for " + clsClient.clsHHmem.DSet.Tables[0].Rows[i]["FirstName"].ToString();
                        if (clsClient.clsHHmem.DSet.Tables[0].Rows[i]["CSFPExpiration"] != null
                            && clsClient.clsHHmem.DSet.Tables[0].Rows[i]["CSFPExpiration"].ToString() != "")
                        {
                            if (Convert.ToDateTime(clsClient.clsHHmem.DSet.Tables[0].Rows[i]["CSFPExpiration"]) < Convert.ToDateTime(CCFBGlobal.DefaultServiceDate))
                            {
                                sCSFPList += " expired: " + Convert.ToDateTime(
                                clsClient.clsHHmem.DSet.Tables[0].Rows[i]["CSFPExpiration"]).ToShortDateString();
                            }
                        }
                        d = clsClient.clsHHmem.LastCSFPLogEntry(Convert.ToDateTime(CCFBGlobal.DefaultServiceDate));
                        if (d >= new DateTime(Convert.ToInt32(CCFBGlobal.DefaultServiceDate.Substring(6)), Convert.ToInt32(CCFBGlobal.DefaultServiceDate.Substring(0, 2)), 1))
                        {
                            sCSFPList += " Served " + d.ToShortDateString();
                        }
                        if (clsClient.clsHHmem.DSet.Tables[0].Rows[i]["CSFPComments"].ToString() != "")
                        {
                            sCSFPList += "\r\n";
                            sCSFPList += clsClient.clsHHmem.DSet.Tables[0].Rows[i]["CSFPComments"].ToString();
                        }
                        sCSFPList += "\r\n";
                    }
                }
                if ((bool)clsClient.clsHHmem.DSet.Tables[0].Rows[i]["BackPack"] && CCFBPrefs.EnableBackPack == true)
                {
                    NbrBackPack++;
                    sBackPackList += "BackPack for " + clsClient.clsHHmem.DSet.Tables[0].Rows[i]["FirstName"].ToString();
                    /*
                    if (clsClient.clsHHmem.DSet.Tables[0].Rows[i]["CSFPExpiration"] != null
                        && clsClient.clsHHmem.DSet.Tables[0].Rows[i]["CSFPExpiration"].ToString() != "")
                    {
                        sCSFPList += " expires: " + Convert.ToDateTime(
                        clsClient.clsHHmem.DSet.Tables[0].Rows[i]["CSFPExpiration"]).ToShortDateString();
                    }
                     */
                    if (clsClient.clsHHmem.DSet.Tables[0].Rows[i]["BPSchool"].ToString() != "")
                    {
                        sBackPackList += " for " + CCFBGlobal.LongNameFromId(CCFBGlobal.parmTbl_BackPackSchool,Convert.ToInt32(clsClient.clsHHmem.DSet.Tables[0].Rows[i]["BPSchool"]));
                    }
                    if (clsClient.clsHHmem.DSet.Tables[0].Rows[i]["BPNotes"].ToString() != "")
                    {
                        sBackPackList += "\r\n";
                        sBackPackList += clsClient.clsHHmem.DSet.Tables[0].Rows[i]["BPNotes"].ToString();
                    }
                    sBackPackList += "\r\n";
                }
                rowCount++;
            }
            fillAutoAlert();
            if (clsClient.clsHH.UseFamilyList)
            {
                clsClient.clsHH.NbrCSFP = NbrCSFP;
                tbmCSFP.Text = NbrCSFP.ToString();
            }
            loadingInfo = oriloadingInfo;
        }

        /// <summary>Loads the ComboBox's with parm_Table Data 
        /// </summary>
        private void loadParmData()
        {
            CCFBGlobal.InitCombo(cboPhoneType, CCFBGlobal.parmTbl_Phone);
            CCFBGlobal.InitCombo(cboSpecialLang, CCFBGlobal.parmTbl_Language);
            CCFBGlobal.InitCombo(cboIDType, CCFBGlobal.parmTbl_AddressID);
            CCFBGlobal.InitCombo(cboClientType, CCFBGlobal.parmTbl_Client);
            CCFBGlobal.InitCombo(cboServiceMethod, CCFBGlobal.parmTbl_ServiceMethod);
            CCFBGlobal.InitCombo(cboHUDCategory, CCFBGlobal.parmTbl_HUDCategory);
            CCFBGlobal.InitCombo(cboTransportation, CCFBGlobal.parmTbl_Transportation);

            CCFBGlobal.dtPopulateCombo(cboHDRoute,"SELECT * FROM HDRoutes ORDER BY ID","RouteTitle","ID","No Routes Available",conn) ;
            CCFBGlobal.dtPopulateCombo(cboHDBuilding, "SELECT * FROM HDBuildings ORDER BY ID", "BldgName", "ID", "Private Residence", conn);
            CCFBGlobal.dtPopulateCombo(cboHDItem, "SELECT * FROM HDItems ORDER BY ID", "Description", "ID", "No Selection", conn);
            CCFBGlobal.InitCombo(cboHDPrograms, CCFBGlobal.parmTbl_HDPrograms);
        }

        /// <summary>
        /// Opens the TrxLog for the Client, uses the SQLDataReader 
        /// to read the rows and Loads them into ListView
        /// </summary>
        public void loadTransLog(string whereClause)
        {
            string query = "";
            lvTrxLog.Items.Clear();
            btnEditTransLog.Enabled = false;
            ListViewItem lvi2 = null;
            
            try
            {
                query = "Select Convert(varchar(10),t.trxDate,101), CASE t.trxStatus WHEN 2 THEN 'Appointment' WHEN 3 THEN 'No Show' "
                    + "else CASE WHEN t.HHMemID is null or t.HHMemID = '0' THEN '....' else RTRIM(t.HHMemID) end end HHMemName"
                    + ", t.FoodSvcList + t.BabySvcList ServiceList, t.NonFoodSvcList"
                    + ", t.LbsStd, t.LbsOther, t.LbsCommodity, t.LbsSupplemental, t.LbsBabySvc, t.Notes"
                    + ", t.Infants, t.Youth, t.teens, t.Eighteen, t.Adults, t.Seniors"
                    + ", t.TotalFamily, t.trxID, t.CreatedBy, t.Created, t.trxStatus, TrxDate, t.HouseholdID, tls.TrxId"
                    + " From TrxLog t LEFT JOIN TrxLogSig tls ON t.TrxId = tls.TrxId"
                    + " Where t.HouseholdID=" + clsClient.clsHH.ID.ToString()
                    + " " + whereClause;
                if (CCFBPrefs.EnableCSFP == true)
                {
                    query += " UNION "
                          + "Select Convert(varchar(10),t.TrxDate,101), FirstName HHMemName"
                          + ", 'CSFP' ServiceList, '' NonFoodSvcList"
                          + ", 0 LbsStd, 0 LbsOther, Lbs LbsCommodity, 0 LbsSupplemental, 0 LbsBabySvc, '' Notes"
                          + ", CAST(CASE WHEN AGE < 3 THEN 1 ELSE 0 END AS smallint) Infants"
                          + ", CAST(CASE WHEN AGE >= 3 AND AGE < 6 THEN 1 ELSE 0 END AS smallint) Youth"
                          + ", 0 Teens, 0 Eighteen, CAST(0 AS smallint) Adults"
                          + ", CAST(CASE WHEN Age>=60 THEN 1 ELSE 0 END AS smallint) Seniors"
                          + ", 1 TotalFamily, t.ID, t.CreatedBy, t.Created, 0 TrxStatus, TrxDate, hm.HouseholdID, Null"
                          + " FROM CSFPLog t "
                          + " LEFT JOIN HouseholdMembers hm ON t.MemID = hm.ID"
                          + " WHERE t.MemID IN (SELECT ID FROM HouseholdMembers WHERE HouseholdID = " + clsClient.clsHH.ID.ToString() + ") "
                          + whereClause;
                }
                query +=  " ORDER BY TrxDate DESC";
                
                //The order of the select statements directly corresponds to what shows on the ListView
                command = new SqlCommand( query, conn);
                openConnection();
                SqlDataReader reader = command.ExecuteReader();

                int rowcount = 0;
                bool bHaveNonFood = false;
                bool bHaveLbsOther = false;
                bool bHaveLbsTEFAP = false;
                bool bHaveLbsSuppl = false;
                bool bHaveLbsBaby = false;
                bool bHaveNotes = false;
                bool bHaveInfants = false;
                bool bHaveYouth = false;
                bool bHaveTeens = false;
                bool bHaveEighteen = false;
                bool bHaveAdults = false;
                bool bHaveSeniors = false;
                btnEditTransLog.Enabled = reader.HasRows;

                while (reader.Read())
                {
                    rowcount++;
                    lvi2 = new ListViewItem(rowcount.ToString());
                    lvi2.Tag = reader.GetInt32(trxlogFldTrxId);
                    for (int i = 0; i < trxlogFldDisplay; i++)
                    {
                        lvi2.SubItems.Add(reader.GetValue(i).ToString());
                        switch (i)
                        {
                            case 3:
                                if (reader.GetString(i) != "")
                                { bHaveNonFood = true; }
                                break;
                            case 5:
                                if (reader.GetInt32(i) > 0)
                                { bHaveLbsOther = true; }
                                break;
                            case 6:
                                if (reader.GetInt32(i) > 0)
                                { bHaveLbsTEFAP = true; }
                                break;
                            case 7:
                                if (reader.GetInt32(i) > 0)
                                { bHaveLbsSuppl = true; }
                                break;
                            case 8:
                                if (reader.GetInt32(i) > 0)
                                { bHaveLbsBaby = true; }
                                break;
                            case 9:
                                if (reader.GetValue(i) != DBNull.Value)
                                {
                                    if (reader.GetString(i) != "")
                                    { bHaveNotes = true; }
                                }
                                break;
                            case 10:
                                if (reader.GetInt16(i) > 0)
                                { bHaveInfants = true; }
                                break;
                            case 11:
                                if (reader.GetInt16(i) > 0)
                                { bHaveYouth = true; }
                                break;
                            case 12:
                                if (reader.GetInt32(i) > 0)
                                { bHaveTeens = true; }
                                break;
                            case 13:
                                if (reader.GetInt32(i) > 0)
                                { bHaveEighteen = true; }
                                break;
                            case 14:
                                if (reader.GetInt16(i) > 0)
                                { bHaveAdults = true; }
                                break;
                            case 15:
                                if (reader.GetInt16(i) > 0)
                                { bHaveSeniors = true; }
                                break;
                            default:
                                break;
                        }
                    }
                    switch (Convert.ToInt32(reader.GetValue(trxlogFldTrxStatus)))
                    {
                        case CCFBGlobal.statusTrxLog_Service:
                        case CCFBGlobal.statusTrxLog_FastTrack:
                            {
                                if (reader.GetValue(0).ToString() == CCFBGlobal.DefaultServiceDate)
                                { lvi2.BackColor = CCFBGlobal.bkColorBaseEdit; break; }
                                else
                                { lvi2.BackColor = Color.White; break; }
                            }
                        case CCFBGlobal.statusTrxLog_NewAppt:
                            { lvi2.BackColor = Color.PaleTurquoise; break; }
                        case CCFBGlobal.statusTrxLog_NoShow:
                            { lvi2.BackColor = Color.MistyRose; break; }
                    }

                    //lvi2.SubItems.Add(reader.GetValue(i).ToString());
                    if (curtrxLogID > 0 && Convert.ToInt32(lvi2.Tag) == curtrxLogID)
                    {
                        lvi2.Selected = true;
                    }
                    if (reader[reader.FieldCount - 1] != DBNull.Value)
                    {
                        lvi2.ImageIndex = 0;
                    }
                    lvTrxLog.Items.Add(lvi2);
                }
                reader.Close();
                setFldWidth(bHaveNonFood, 4, 0, 100);
                setFldWidth(bHaveLbsOther, 6, 0, 40);
                setFldWidth(bHaveLbsTEFAP, 7, 0, 40);
                setFldWidth(bHaveLbsSuppl, 8, 0, 40);
                setFldWidth(bHaveLbsBaby, 9, 0, 40);
                setFldWidth(bHaveNotes, 10, 50, 100);
                setFldWidth(bHaveInfants, 11, 0, 34);
                setFldWidth(bHaveYouth, 12, 0, 34);
                setFldWidth(bHaveTeens, 13, 0, 34);
                setFldWidth(bHaveEighteen, 14, 0, 34);
                setFldWidth(bHaveAdults, 15, 0, 34);
                setFldWidth(bHaveSeniors, 16, 0, 34);
                if (curtrxLogID < 1)
                {
                    lvTrxLog.SelectedIndices.Clear();
                    lvTrxLog.SelectedItems.Clear();
                    btnEditTransLog.Enabled = false;
                    //lvTrxLog.FocusedItem = null;
                    //lvTrxLog.Refresh();
                }
                closeConnection();
            }
            catch (SqlException ex)
            {
                closeConnection();
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString());
            }
            catch (Exception ex)
            {
                closeConnection();
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString());
            }
        }

        private void setFldWidth(bool flag, int idx, int minWidth, int baseWidth)
        {
            int fldWidth = minWidth;
            if (flag == true)
            { fldWidth = baseWidth; }
            lvTrxLog.Columns[idx].Width = fldWidth;
        }
        
        /// <summary>
        /// Opens the VoucherLog for the Client, uses the SQLDataReader 
        /// to read the rows and Loads them into ListView
        /// </summary>
        public void loadVoucherLog(string whereClause)
        {
            lvVoucherLog.Items.Clear();
            btnEditVoucherLog.Tag = "";
            ListViewItem lvi2 = null;
            SqlDataReader reader = null;
            try
            {
                //The order of the select statements directly corresponds to what shows on the ListView
                command = new SqlCommand("Select Convert(varchar(10),v.trxDate,101), vi.Description, v.Amount, v.Notes, v.TrxID "
                    + "From VoucherLog v Left Join VoucherItems vi on vi.uid = v.VoucherItemID "
                    + "Where v.HouseholdID=" + clsClient.clsHH.ID.ToString()
                    + " " + whereClause 
                    + " ORDER BY TrxDate Desc", conn);
                openConnection();
                reader = command.ExecuteReader();

                int count = 0;
                btnEditVoucherLog.Enabled = reader.HasRows;

                while (reader.Read())
                {
                    lvi2 = new ListViewItem(count.ToString());
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        if (i == 2)
                        {
                            lvi2.SubItems.Add(reader.GetDecimal(i).ToString("F"));
                        }
                        else
                            lvi2.SubItems.Add(reader.GetValue(i).ToString());
                    }
                    lvVoucherLog.Items.Add(lvi2);
                    count++;
                }
                closeConnection();
            }
            catch (SqlException ex)
            {
                closeConnection();
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString());
            }
            catch (Exception ex)
            {
                closeConnection();
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString());
            }

            if (lvVoucherLog.Items.Count > 0)
            {
                lvVoucherLog.Items[0].Selected = true;
                btnEditVoucherLog.Tag = lvVoucherLog.Items[0].SubItems[1].Text;
            }
        }

        /// <summary>  Loads User Check Box Labels from UserFields Table
        /// </summary>
        private void loadUserFieldLabels()
        {
            bool fieldsUsed = false;
            clsUserFields.open("Household");
            loadingInfo = true;
            for (int i = 0; i < chkLstBxUserFields.Items.Count; i++)
            {
                clsUserFields.setDataRow(userFlagNames[i]);
                if (userFlagNames[i] == clsUserFields.FldName)
                {
                    chkLstBxUserFields.Items[i] = clsUserFields.EditLabel;
                    if (chkLstBxUserFields.Items[i].ToString() == "")
                        chkLstBxUserFields.SetItemCheckState(i, CheckState.Indeterminate);
                    else
                    {
                        chkLstBxUserFields.SetItemCheckState(i, CheckState.Unchecked);
                        fieldsUsed = true;
                    }
                }
            }
            loadingInfo = false;
            clsUserFields.setDataRow(tbUserNum0.Tag.ToString());
            if (tbUserNum0.Tag.ToString() == clsUserFields.FldName)
            {
                lblUserNum0.Text = clsUserFields.EditLabel;
                tbUserNum0.Visible = (lblUserNum0.Text != "");
                lblUserNum0.Visible = (lblUserNum0.Text != "");
                if (lblUserNum0.Text != "")
                    fieldsUsed = true;
            }
            clsUserFields.setDataRow(tbUserNum1.Tag.ToString());
            if (tbUserNum1.Tag.ToString() == clsUserFields.FldName)
            {
                lblUserNum1.Text = clsUserFields.EditLabel;
                tbUserNum1.Visible = (lblUserNum1.Text != "");
                lblUserNum1.Visible = (lblUserNum1.Text != "");
                if (lblUserNum1.Text != "")
                    fieldsUsed = true;
            }
            if (fieldsUsed == false)
                for (int i = 0; i < tabCntrlMain.TabCount; i++)
                {
                    if (tabCntrlMain.TabPages[i].Name == "tpUserFields")
                    {
                        tpTmpUserFields = tabCntrlMain.TabPages[i];
                        tabCntrlMain.TabPages.RemoveAt(i);
                        break;
                    }
                }
            else
                if (tpTmpUserFields != null)
                {
                    if (tabCntrlMain.TabCount > 0)
                    {
                        if (tabCntrlMain.TabPages[0].Name == "tpHHData")
                            tabCntrlMain.TabPages.Insert(1, tpgUserFields);
                        else
                            tabCntrlMain.TabPages.Insert(0, tpgUserFields);
                    }
                    else
                        tabCntrlMain.TabPages.Add(tpgUserFields);
                    tpgUserFields = null;
                }
        }

        private void lvEthnicity_Enter(object sender, EventArgs e)
        {
            if (ethnicitySelectedFirstTime == false)
                ethnicitySelectedFirstTime = true;
        }

        private void lvEthnicity_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (inEditMode == false && loadingInfo == false && ethnicitySelectedFirstTime == true)
                e.NewValue = e.CurrentValue;
        }

        private void lvEthnicity_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (loadingInfo == false)
                clsClient.clsHH.SetDataValue(e.Item.Tag.ToString(), e.Item.Checked);
        }

        private void lvHHMembers_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            //If not loding the Items
            if (loadingInfo == false)
            {
                if (e.Item.Checked == true)
                {
                    clsClient.clsHHmem.DSet.Tables[0].Rows[e.Item.Index]["Inactive"] = true;
                }
                else
                {
                    clsClient.clsHHmem.DSet.Tables[0].Rows[e.Item.Index]["Inactive"] = false;
                }
                //Calc Ages
                clsClient.UpdateDataBasedOn(Convert.ToDateTime(CCFBGlobal.DefaultServiceDate));
                //Update
                clsClient.clsHHmem.update(true);
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (logout == false)
                frmLogIn.Close();
            else
            {
                frmLogIn.Visible = true;
            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            splitMemTrans.SplitterDistance = constHHTabWidth;
            if (splCntrCardMembers.SplitterDistance < 290)
                splCntrCardMembers.SplitterDistance = 290;
        }

        private void mnuAdmin_EmailRecipients_Click(object sender, EventArgs e)
        {
            MonthlyReportPreferencesForm frmEmailRecipts = new MonthlyReportPreferencesForm();
            frmEmailRecipts.ShowDialog();
        }

        private void mnuAdmin_IncomeMatrix_Click(object sender, EventArgs e)
        {
            IncomeMatrixForm frmIncomeMatrix = new IncomeMatrixForm();
            frmIncomeMatrix.ShowDialog();
        }

        private void mnuAdmin_PreferencesForm_Click(object sender, EventArgs e)
        {
            EditPreferencesForm frmEditDefaults = new EditPreferencesForm();
            frmEditDefaults.ShowDialog();
            SetEnvironmentFromPrefs();
            if (CCFBPrefs.EnableAppointments == true && CCFBGlobal.DefalutApptDate == "")
            {
                SetCurrentApptDate();
            }
        }

        private void mnuAdmin_ServiceItemsForm_Click(object sender, EventArgs e)
        {
            ServiceItemsForm frmServiceItems = new ServiceItemsForm();
            frmServiceItems.ShowDialog();
            if (CCFBGlobal.ServiceItemsChanged == true)
                clsDailyItems.Refresh(false);
        }

        private void mnuAdmin_UserDefinedFields_Click(object sender, EventArgs e)
        {
            EditUserFields frmEditUserFields = new EditUserFields();
            frmEditUserFields.ShowDialog();
            
            if(frmEditUserFields.FieldsLableChanged == true)
                loadUserFieldLabels();
            if (frmEditUserFields.UserFieldsReset == true)
            {
                clsClient.open(clsClient.clsHH.ID, false, false);
                fillForm();
            }

            fillAutoAlert();
        }

        private void mnuAdmin_YearlyCalendarForm_Click(object sender, EventArgs e)
        {
            YearlyForm frmYearlyCalendar = new YearlyForm();
            frmYearlyCalendar.ShowDialog();
        }

        private void mnuClient_BeginEditClient_Click(object sender, EventArgs e)
        {
            changeEditMode(true);
        }

        private void mnuClient_CancelEdit_Click(object sender, EventArgs e)
        {
            cancelEdit();
        }

        private void mnuClient_DeleteClient_Click(object sender, EventArgs e)
        {
            btnBeginEdit.Focus(); //Make sure focus not on a grid and that it is not in edit mode
            Application.DoEvents();
            clsClient.clsHHSvcTrans.openForHH(clsClient.clsHH.ID);
            DeleteHousehold frmDeleteHH = new DeleteHousehold(clsClient);
            frmDeleteHH.ShowDialog(this);

            if (frmDeleteHH.ClientDeleted() == true)
            {
                frmFindClient.loadList();
                clearForm();
                int newHHId = frmDeleteHH.ClientIdMovedTo();
                frmDeleteHH.Close();
                if (newHHId > 0)
                    setHousehold(newHHId,0);
                else
                    frmFindClient.Show();
            }
            else if (frmDeleteHH.ClientMarkedInactive() == true)
            {
                clsClient.RefreshHousehold();
                frmDeleteHH.Close();
            }
            else
                frmDeleteHH.Close();
        }

        private void mnuClient_PrintForm_Click(object sender, EventArgs e)
        {
            CaptureAndPrintForm();
        }

        private void mnuClient_SaveHHChanges_Click(object sender, EventArgs e)
        {
            saveClientRecord();
        }

        private void mnuClient_SchSupply_Click(object sender, EventArgs e)
        {
            EditSchoolSupplyPickupTicket frmSchSupply = new EditSchoolSupplyPickupTicket(clsClient);
            frmSchSupply.ShowDialog();
        }

        private void mnuFile_FindClient_Click(object sender, EventArgs e)
        {
            frmFindClient.Visible = true;
        }

        private void mnuFile_AddHH_Click(object sender, EventArgs e)
        {
            addHousehold(0);
        }

        private void mnuFile_PrintAllClientCards_Click(object sender, EventArgs e)
        {
            int curHHID = clsClient.clsHH.ID;
            printAllClientCards();
            if (curHHID != clsClient.clsHH.ID)
            {
                setHousehold(curHHID, 0);
            }
        }

        private void mnuFile_LogOut_Click(object sender, EventArgs e)
        {
            logout = true;
            frmLogIn.resetForm();
            this.Close();
        }

        private void mnuFile_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mnuHelp_About_Click(object sender, EventArgs e)
        {
            AboutBoxCSDG frmAbout = new AboutBoxCSDG();
            frmAbout.ShowDialog(this);
        }

        private void mnuHHMember_Edit_Click(object sender, EventArgs e)
        {
            editHHMem();
        }

        private void mnuReports_AccessRpts_Click(object sender, EventArgs e)
        {
            AccessReportsForm frmReports = new AccessReportsForm();
            frmReports.Show();
        }

        private void mnuReports_MonthlyForm_Click(object sender, EventArgs e)
        {
            MonthEndReportsForm frmMonthlyReports = new MonthEndReportsForm(this);
            frmMonthlyReports.Show();
        }

        private void mnuTools_DonationsForm_Click(object sender, EventArgs e)
        {
            ShowDonationsForm();
        }

        private void mnuTools_DonorsForm_Click(object sender, EventArgs e)
        {
            EditDonorForm frmEditDonor = new EditDonorForm(CCFBGlobal.connectionString);
            frmEditDonor.Show();
        }

        private void mnuTools_VolHoursForm_Click(object sender, EventArgs e)
        {
            VolunteerHoursForm frmVolHrs = new VolunteerHoursForm();
            frmVolHrs.Show();
        }

        private void mnuTools_VolunteersForm_Click(object sender, EventArgs e)
        {
            EditVolunteerForm frmEditVols = new EditVolunteerForm(CCFBGlobal.connectionString);
            frmEditVols.Show();
        }

        private void mnuTrx_Delete_Click(object sender, EventArgs e)
        {
            if (lvTrxLog.FocusedItem != null  && lvTrxLog.Visible == true )
            {
                if (clsClient.clsHHSvcTrans.PromptDelete(lvTrxLog.FocusedItem.Tag.ToString()))
                {
                    clsClient.clsHH.UpdateLatestServiceDates(DateTime.Now.ToShortDateString());
                    clsClient.clsHHSvcTrans.openForHH(clsClient.clsHH.ID);
                    getClientLogForPeriod();
                }
            }
            else if (lvVoucherLog.FocusedItem != null)
            {
                if (MessageBox.Show("Are You Sure You Want To Delete Voucher From "
                    + lvVoucherLog.FocusedItem.SubItems[1].Text + " With TrxID = " +
                    lvVoucherLog.FocusedItem.SubItems[5].Text + "?", "Delete Voucher",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    deleteVoucher();
                }
            }
            else
            {
                MessageBox.Show("Please Select A Transaction And Try Again");
            }
        }

        private void deleteVoucher()
        {
            try
            {
                command = new SqlCommand("Delete From VoucherLog Where TrxID = "
                    + lvVoucherLog.FocusedItem.SubItems[5].Text, conn);
                openConnection();
                command.ExecuteNonQuery();
                closeConnection();
                getVoucherLogForPeriod();
            }
            catch (SqlException ex)
            {
                CCFBGlobal.appendErrorToErrorReport(command.CommandText, ex.GetBaseException().ToString());
                closeConnection();
            }
        }

        private void mnuTrx_Edit_Click(object sender, EventArgs e)
        {
            if (lvTrxLog.FocusedItem != null)
            {
                openEditServiceTrx(Convert.ToInt32(lvTrxLog.FocusedItem.SubItems[16].Text));
            }
            else
            {
                MessageBox.Show("Please Select A Transaction To Edit and Try Again");
            }

        }

        private void mnuTrx_NewAppointment_Click(object sender, EventArgs e)
        {
            CreateNewAppointment();
        }

        private void newServiceTrxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateNewFoodService();
        }

        /// <summary>
        /// Opens a connection to the Database
        /// </summary>
        private void openConnection()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
        }

        private void openEditServiceTrx(int trxLogId)
        {
            clsClient.clsHHSvcTrans.open(trxLogId);
            if (CCFBPrefs.TestAllowEditTrxDate(Convert.ToDateTime(clsClient.clsHHSvcTrans.TrxDate), true) == true)
            {
                frmEditServices.initForm(clsClient, false, false, trxLogId, clsClient.clsHHSvcTrans.HHMemID, tbAlert.Rtf);
                frmEditServices.ShowDialog();
                getClientLogForPeriod();
            }
        }

        private void PopulatelvIncomeGroups()
        {
            SqlConnection conn = new SqlConnection(CCFBGlobal.connectionString);
            SqlCommand cmdObj = new SqlCommand("SELECT Id FROM IncomeGroups", conn);
            conn.Open();
            SqlDataReader reader = cmdObj.ExecuteReader();
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    incomeGroups.Add(new IncomeGroupMatrix(reader.GetSqlInt32(0).Value));
                }
                foreach (IncomeGroupMatrix incomeGrp in incomeGroups)
                {
                    ListViewItem lvItm = new ListViewItem(incomeGrp.ID.ToString());
                    ListViewItem.ListViewSubItem lvSubItm = new ListViewItem.ListViewSubItem();
                    lvSubItm.Name = "colIncomeGrpName";
                    lvSubItm.Text = incomeGrp.ShortName;
                    lvItm.SubItems.Add(lvSubItm);
                    lvSubItm = new ListViewItem.ListViewSubItem();
                    lvSubItm.Name = "colIncomeCat";
                    lvSubItm.Text = "...";
                    lvItm.SubItems.Add(lvSubItm);

                    lvIncomeGroups.Items.Add(lvItm);
                }
            }
            reader.Close();
            conn.Close();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Point ulCorner = new Point(10, 10);
            e.Graphics.DrawImage(photo, new Rectangle(50, 50, 1000, 750));
        }

        private void saveClientRecord()
        {
            clsClient.clsHH.update(true);
            fillAutoAlert();
            showUserInfo();
            changeEditMode(false);
        }

        private void setCheckBoxLabelBackColor(string FieldName, Color bkColor)
        {
            switch (FieldName.ToLower())
            {
                case "babyservices": { lblBabyServices.BackColor = bkColor; break; }
                case "homeless": { lblHomeless.BackColor = bkColor; break; }
                case "incitylimits": { lblInCityLimits.BackColor = bkColor; break; }
                case "needcommoditysignature": { lblNeedCommodSignature.BackColor = bkColor; break; }
                case "needtoverifyid": { lblNeedToVerifyId.BackColor = bkColor; break; }
                case "nocommodities": { lblNoCommodities.BackColor = bkColor; break; }
                case "supplonly": { lblSupplOnly.BackColor = bkColor; break; }
                case "singleheadhh": { lblSingleHeadHH.BackColor = bkColor; break; }
                default:
                    break;
            }
        }

        private void SetCurrentApptDate()
        {
            CCFBGlobal.DefalutApptDate = clsDaysOpen.FindServiceDateNext(DateTime.Today.ToShortDateString());
            if (CCFBGlobal.DefalutApptDate == "" && CCFBPrefs.EnableAppointments == true)
            {
                MessageBox.Show("No Default Appointment Date Found.  Please Select Default Appointment Date");
            }
            tsbDfltApptDate.Text = CCFBGlobal.DefalutApptDate;
        }

        private void SetCurrentServiceDate()
        {
            CCFBGlobal.DefaultServiceDate = clsDaysOpen.FindServiceDatePrev(DateTime.Today.ToShortDateString());
            if (CCFBGlobal.DefaultServiceDate == "")
            {
                CCFBGlobal.DefaultServiceDate = tsbDfltSvcDate.Text = DateTime.Today.ToShortDateString();
            }
            else
                tsbDfltSvcDate.Text = DateTime.Parse(CCFBGlobal.DefaultServiceDate).ToShortDateString();
            setPointsWeekOf();
        }

        /// <summary>
        /// Converts the Textbox's mode based on inEditMode
        /// </summary>
        private void setEditStateForControls(Color baseBackColor)
        {
            bool baseReadOnly = !inEditMode;
            
            tbID.ReadOnly = false;
            tbID.TabStop = !inEditMode;

            foreach (TabPage tp in tabCntrlMain.Controls.OfType<TabPage>())
            {
                tp.BackColor = baseBackColor;
            }
            tbPhone.ReadOnly = baseReadOnly;
            tbPhone.BackColor = Color.White;
            foreach (TextBox tb in tbList)
            {
                tb.ReadOnly = baseReadOnly;
                tb.BackColor = Color.White;
            }

            tbID.ReadOnly = false;

            foreach (ComboBox cbo in cboList)
            {
                cbo.Enabled = inEditMode;
                cbo.BackColor = Color.White;
                cbo.ForeColor = Color.Black;
            }

            foreach (CheckBox cb in chkList)
            {
                if (cb.Tag != null && cb.Tag.ToString().Trim() != "")
                {
                    cb.Enabled = inEditMode;
                    cb.BackColor = baseBackColor;
                    setCheckBoxLabelBackColor(cb.Tag.ToString(), baseBackColor);
                }
            }
            chkLstBxUserFields.Enabled = inEditMode;
            chkLstBxUserFields.BackColor = baseBackColor;
//            setFamilyStatsMode();
        }

        /// <summary>
        /// Turns off or on the different controls for how they were
        /// set in the prefrences
        /// </summary>
        private void SetEnvironmentFromPrefs()
        {
            this.Text = CCFBPrefs.FoodBankName + " - Client Household Form";
            splitMemTrans.Panel1Collapsed = false;
            if (   CCFBPrefs.EnableClientPhone == false
                && CCFBPrefs.EnableVerifyId == false
                && CCFBPrefs.EnableTEFAP == false
                && CCFBPrefs.EnableHUDCategory == false
                && CCFBPrefs.EnableBabyServices == false
                && CCFBPrefs.EnableHHUserDefinedFields == false
                && CCFBPrefs.EnableHouseholdIncome == false
                && CCFBPrefs.EnableHomeDeliv == false
                && CCFBPrefs.EnablePointsTracking == false)
            {
                    tabCntrlMain.Visible = false;
                    splitMemTrans.Panel1Collapsed = true;
            }
            else
            {
                //if (   CCFBPrefs.EnableClientPhone == false
                //    && CCFBPrefs.EnableVerifyId == false
                //    && CCFBPrefs.EnableTEFAP == false
                //    && CCFBPrefs.EnableHUDCategory == false
                //    && CCFBPrefs.EnableBabyServices == false)
                //{
                //    splitMemTrans.Panel1Collapsed = false;
                //    tabCntrlMain.TabPages.RemoveByKey("tpgHHData");  //Remove HHData Tab
                //}
                //else
                {
                    //Enable Phone Number
                    tbPhone.Visible = cboPhoneType.Visible = CCFBPrefs.EnableClientPhone;
                    lblPhoneNum.Visible = lblPhoneType.Visible = CCFBPrefs.EnableClientPhone;
                    //Enable Verify ID
                    grpbxVerifyId.Visible = CCFBPrefs.EnableVerifyId;
                    //Enable Need Commodity Signature
                    chkNeedCommSig.Visible = lblNeedCommodSignature.Visible = CCFBPrefs.EnableTEFAP;
                    //Enable Baby Services
                    chkBabyServices.Visible = lblBabyServices.Visible = tbBabySvcDescr.Visible = CCFBPrefs.EnableBabyServices;
                    //Enable HUD Category
                    cboHUDCategory.Visible = lblHUDCategory.Visible = CCFBPrefs.EnableHUDCategory;
                }
                if (CCFBPrefs.EnableHHUserDefinedFields == false)
                    tabCntrlMain.TabPages.RemoveByKey("tpgUserFields");  //Remove UserFields Tab
                if (CCFBPrefs.EnableHouseholdIncome == false)
                    tabCntrlMain.TabPages.RemoveByKey("tpgIncome");  //Remove Income Tab
                if (CCFBPrefs.EnablePointsTracking == false)
                    tabCntrlMain.TabPages.RemoveByKey("tpgPointSys");  //Remove Points Tab
            }
            if (CCFBPrefs.EnableHomeDeliv == false)
            {
                tabCntrlMain.TabPages.RemoveByKey("tpgHD");  //Remove Home Delivery Tab
                mnuHD.Visible = false;
            }
            else
            {
                mnuHD.Visible = true;
            }
            
            //Enable Vouchers
            tsbVouchers.Visible = mnuTools_MaintainVoucherItems.Visible = CCFBPrefs.EnableVouchers;
            //EnablePrint Clientcard
            tsbPrintClientcard.Visible = mnuClient_PrintClientCard.Visible = CCFBPrefs.EnablePrintClientCard;
            mnuFile_PrintAllClientCards.Visible = CCFBPrefs.EnablePrintClientCard
                        && CCFBGlobal.currentUser_PermissionLevel >= (Int32.Parse(mnuFile_PrintAllClientCards.Tag.ToString()));
            //Enable Cash Donations
            mnuTools_CashDonations.Visible = CCFBPrefs.EnableCashDonations;
            //Enable Food Donations
            tsbFoodDonations.Visible = mnuTools_DonationsForm.Visible = CCFBPrefs.EnableFoodDonations;
            //Enable Appointments
            tsbCreateAppt.Visible = tsbDfltApptDate.Visible = mnuTrx_NewAppointment.Visible = CCFBPrefs.EnableAppointments;
            //Enable Food Services
            tsbNewService.Visible = tsbDfltSvcDate.Visible = mnuTrx_NewServiceTrx.Visible = CCFBPrefs.EnableFoodServices;
            //Enable TEFAP
            chkNoCommodities.Visible = lblNoCommodities.Visible = CCFBPrefs.EnableTEFAP;
            lblLastComm.Visible = tbdLastComodity.Visible = CCFBPrefs.EnableTEFAP;
            lblNbrTEFAP.Visible = tbxNbrTEFAPThisMonth.Visible = CCFBPrefs.EnableTEFAP;
            if (CCFBPrefs.EnableTEFAP == false)
                lvTrxLog.Columns[7].Width = 0;
            else
                lvTrxLog.Columns[7].Width = 40;
            //Enable Supplemental
            chkSupplOnly.Visible = lblSupplOnly.Visible = CCFBPrefs.EnableSupplemental;
            if (CCFBPrefs.EnableSupplemental == false)
                lvTrxLog.Columns[8].Width = 0;
            else
                lvTrxLog.Columns[8].Width = 40;
            //Enable CSFP
            tbmCSFP.Visible = lblCSFP.Visible = CCFBPrefs.EnableCSFP;
            dgvHHMembers.Columns["clmCSFP"].Visible = CCFBPrefs.EnableCSFP;
            tsbCSFP.Visible = CCFBPrefs.EnableCSFP;
            mnuTools_CSFPServices.Visible = CCFBPrefs.EnableCSFP;
            //Use Family List
            chkUseFamList.Visible = CCFBPrefs.UseFamilyList != CCFBPrefs.UseFamilyListCode.Never;
            //Enable Volunteers
            mnuTools_VolHoursForm.Visible = CCFBPrefs.EnableVolunteerHours;
            btnAssignBarcode.Visible = CCFBPrefs.EnableBarcodePrompts && (CCFBPrefs.BarcodeUseFamilyMember == false);
            mnuClient_NewSvcOverRide.Tag = CCFBPrefs.OverRideLevel;
            //Enable Vouchers
            if (CCFBPrefs.EnableVouchers == false && tabCntrlLog.TabPages.Count > 1)
                tabCntrlLog.TabPages.RemoveAt(1);

            if (frmTrxLog != null)
                frmTrxLog.PrefsChanged();
            //Enable Backpack
            mnuTools_Backpack.Visible = CCFBPrefs.EnableBackPack;
            if (CCFBPrefs.EnableBarcodePrompts == true)
            {
                frmBarCodeEntry = new BarCodeEntryForm();
            }
        }

        private void setFamilyStatsMode()
        {
            if (clsClient.clsHH.RowCount > 0)
            {
                if (clsClient.clsHH.UseFamilyList == true)
                {
                    foreach (TextBox tb in tbmList)
                    {
                        tb.ReadOnly = true;
                        tb.BackColor = CCFBGlobal.bkColorFormAlt;
                        tb.ForeColor = Color.DarkBlue;
                        tb.TabStop = false;
                    }
                }
                else
                {
                    foreach (TextBox tb in tbmList)
                    {
                        tb.ReadOnly = !inEditMode;
                        tb.BackColor = Color.White;
                        tb.ForeColor = Color.Black;
                        tb.TabStop = inEditMode;
                    }
                }
            }
        }

        //private void updateFirstService()
        //{
        //    try
        //    {
        //        if (conn.State == ConnectionState.Closed)
        //            conn.Open();

        //        command = new SqlCommand("UpdateHouseholdFirstServiceDate", conn);
        //        command.CommandType = CommandType.StoredProcedure;
        //        command.Parameters.Add(new SqlParameter("@HHID", clsClient.clsHH.ID));
        //        command.ExecuteNonQuery();

        //        if (conn.State == ConnectionState.Open)
        //            conn.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        CCFBGlobal.appendErrorToErrorReport(command.CommandText, ex.GetBaseException().ToString());
        //    }
        //}

        /// <summary>
        /// Opens the selected household and re-fills the form
        /// </summary>
        /// <param name="newHhID">HouseholdID</param>
        public void setHousehold(int newHhID, int memberId)
        {
            clearForm();
            clsClient.open(newHhID, true, true);
            if (memberId > 0)
                clsClient.ServingHHMemID =  memberId;
            else
                clsClient.ServingHHMemID = clsClient.clsHHmem.getHeadHHId(newHhID);

            fillForm();
        }

        private void SetIncomeGroups()
        {
            int i = 0;
            foreach (IncomeGroupMatrix item in incomeGroups)
            {
                lvIncomeGroups.Items[i].SubItems[2].Text = item.GetIncomeCategory(clsClient.clsHH.AnnualIncome, clsClient.clsHH.TotalFamily);
                i++;
            }
        }

        /// <summary>
        /// Set main menu items to invisible if Tag > permission;
        /// Set sub-menu items to disabled if Tag > permission;
        /// 0 = Intake, 1=Intake Admin, 2 = Admin
        /// </summary>
        private void setPermissionsForMenu()
        {
            foreach (ToolStripMenuItem mi in menuStrip1.Items.OfType<ToolStripMenuItem>())
            {
                if (Int32.Parse(mi.Tag.ToString()) > CCFBGlobal.currentUser_PermissionLevel)
                { mi.Visible = false; }
                else
                {
                    mi.Visible = true;
                    foreach (ToolStripDropDownItem ddi in mi.DropDownItems.OfType<ToolStripDropDownItem>())
                    {
                        if (Int32.Parse(ddi.Tag.ToString()) > CCFBGlobal.currentUser_PermissionLevel)
                        { ddi.Enabled = false; }
                        else
                        { ddi.Enabled = true; }
                    }
                }
            }
        }

        private void ShowDonationsForm()
        {
            FoodDonationsForm frmFoodDonation = new FoodDonationsForm();
            frmFoodDonation.Show();
        }

        /// <summary>
        /// Populates the family demographic textboxes
        /// </summary>
        private void ShowFamData()
        {
            //Sets all the family lists in form and in database, updates if
            //different then it was before function call
            tbmAdults.Text = clsClient.clsHH.Adults.ToString();
            tbmTeens.Text = clsClient.clsHH.Teens.ToString();
            tbmSeniors.Text = clsClient.clsHH.Seniors.ToString();
            tbmYouth.Text = clsClient.clsHH.Youth.ToString();
            tbmInfants.Text = clsClient.clsHH.Infants.ToString();
            tbTotalFam.Text = clsClient.clsHH.TotalFamily.ToString();
            tpgFamilyDetail.Text = "[" + clsClient.clsHH.TotalFamily.ToString() + "] " + "Family Members";
        }

        private void splCntrCardMembers_SplitterMoved(object sender, SplitterEventArgs e)
        {
            tabFamily.Height = splCntrCardMembers.SplitterDistance - tabFamily.Top - 5;
            //tbeNotes.Height = splCntrCardMembers.SplitterDistance - tbeNotes.Top - 5; 
        }

        private void splitContainer2_Panel1_SizeChanged(object sender, EventArgs e)
        {
            if (splitContainer2.Panel1Collapsed == false)
            {
                dgvHHMembers.Width = splitContainer2.Panel1.ClientSize.Width;
                dgvHHMembers.Height = splitContainer2.Panel1.ClientSize.Height - dgvHHMembers.Top;
            }
        }

        private void tbeCity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (clsZipcodes.getZip(tbeCity.Text, tbeState.Text)==true)
                {
                    clsClient.clsHH.Zipcode = tbeZipCode.Text = clsZipcodes.ZipCode;
                    clsClient.clsHH.City = tbeCity.Text = clsZipcodes.City;
                    clsClient.clsHH.State = tbeState.Text = clsZipcodes.State.ToUpper();
                }
            }
        }

        ////private void tbeName_KeyDown(object sender, KeyEventArgs e)
        ////{
        ////    if (inEditMode == true)
        ////    {
        ////        e.SuppressKeyPress = true;
        ////        DuplicateHouseholdNameForm frmDupHHName =
        ////            new DuplicateHouseholdNameForm(clsClient, tbeName.Text);
        ////        frmDupHHName.ShowDialog();

        ////        if (frmDupHHName.Canceled == false)
        ////        {
        ////            tbeName.Text = frmDupHHName.HHName;
        ////        }
        ////        else
        ////        {
        ////            tbeName.Text = clsClient.clsHH.Name;
        ////        }
        ////    }
        ////    else
        ////    {
        ////        if (CCFBPrefs.EnableBarcodePrompts == true && e.KeyCode == Keys.F2)
        ////        {
        ////            ShowBarcodePrompt();
        ////        }
        ////    }
        ////}

        private void tbeZipCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (inEditMode == true && e.KeyCode == Keys.Enter)
            {
                if (clsZipcodes.getCity(tbeZipCode.Text)==true)
                {
                    clsClient.clsHH.City = tbeCity.Text = clsZipcodes.City;
                    clsClient.clsHH.State = tbeState.Text = clsZipcodes.State.ToUpper();
                }
            }
        }

        private void tbID_KeyDown(object sender, KeyEventArgs e)
        {
            //if (CCFBPrefs.EnableBarcodePrompts == true && e.KeyCode == Keys.F2 && inEditMode == false)
            //{
            //    ShowBarcodePrompt();
            //}
            //else
            //{
                if (e.Modifiers != Keys.Alt)
                {
                    if (formClear == false)
                    {
                        clearForm();
                        //tbID.Text = e.KeyValue.ToString();
                    }
                    if (e.KeyCode == Keys.Enter && tbID.ReadOnly == false && tbID.Text !="")
                    {
                        int testID = 0;
                        try
                        {
                            testID = Convert.ToInt32(tbID.Text);
                        }
                        catch
                        {
                            MessageBox.Show("The Household Number You Entered Is Not In Proper Format. Please Check ID Number And Try Again",
                                "ERROR: ID NOT PRESENT", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            clearForm();
                            tbID.Focus();
                        }
                        if (clsClient.householdExists(testID) == true)
                        {
                            setHousehold(testID, 0);
                            //loadHousehold(testID.ToString());
                            //btnBeginEdit.Enabled = true;  this is done in fillform called from loadHousehold
                        }
                        else
                        {
                            MessageBox.Show("The Household Number You Entered Is Not In Client List. Please Check ID Number And Try Again",
                                "ERROR: ID NOT PRESENT", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            clearForm();
                            tbID.Focus();
                        }
                        Application.DoEvents();
                    }
                    Application.DoEvents();
                }
            //}
        }

        private void tbID_TextChanged(object sender, EventArgs e)
        {
            btnBeginEdit.Enabled = false;
        }

        private void tbInteger_KeyDown(object sender, KeyEventArgs e)
        {
            CCFBGlobal.checkForIntOnKeyPress(e);
        }

        /// <summary>
        /// Used to deal with a textbox losing focus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbList_LostFocus(object sender, EventArgs e)
        {

            if (inEditMode == true)
            {
                TextBox tbHH = (TextBox)sender; //Get the correct textbox
                if (tbHH.Tag.ToString() != "")
                {
                    //If current value does not = value of textbox
                    if (tbHH.Text.ToString() != clsClient.clsHH.GetDataString(tbHH.Tag.ToString()))
                    {
                        if (tbHH.Tag.ToString() == "AnnualIncome")
                        {
                            if (tbHH.Text.Trim() == "")
                                tbHH.Text = "0";

                            clsClient.clsHH.SetDataValue(tbHH.Tag.ToString(), tbHH.Text.Trim());
                            SetIncomeGroups();
                        }
                        else
                            clsClient.clsHH.SetDataValue(tbHH.Tag.ToString(), tbHH.Text.Trim());
                    }
                }
            }
        }

        private void tbList_KeyDown(object sender, KeyEventArgs e)
        {
            //if (CCFBPrefs.EnableBarcodePrompts == true && e.KeyCode == Keys.F2 && inEditMode == false)
            //{
            //    ShowBarcodePrompt();
            //}
        }

        private void tbsPrintClientcard_Click(object sender, EventArgs e)
        {
            if (CCFBPrefs.CaptureSignature == true)
            {
                printFamilyCard2();
            }
            else
            {
                printFamilyCard();
            }
        }

        private void printFamilyCard()
        {
            PRNTFamilyCard clsCreateClientCard = new PRNTFamilyCard(clsClient);
            parmType pt = (parmType)cboSpecialLang.SelectedItem;

            string templatePath =  CCFBGlobal.fb3TemplatesPath + "Clientcard" + pt.ShortName + ".doc";
            string fbName = CCFBPrefs.FoodBankName;

            //int index = 0;
            //string[] fldNames = new string[4];
            //string[] fldVals = new string[4];

            if (File.Exists(templatePath) == false)
                templatePath = CCFBGlobal.fb3TemplatesPath + "ClientcardENG.doc";

            //if (CCFBPrefs.FamilyCardSlot1 != -1)
            //{
            //    clsUserFields.setDataRow("UserFlag" + CCFBPrefs.FamilyCardSlot1.ToString());
            //    fldNames[index] = clsUserFields.EditLabel;
            //    fldVals[index] = clsClient.clsHH.GetDataValue(clsUserFields.FldName).ToString();
            //    index++;
            //}
            //if (CCFBPrefs.FamilyCardSlot2 != -1)
            //{
            //    clsUserFields.setDataRow("UserFlag" + CCFBPrefs.FamilyCardSlot2.ToString());
            //    fldNames[index] = clsUserFields.EditLabel;
            //    fldVals[index] = clsClient.clsHH.GetDataValue(clsUserFields.FldName).ToString();
            //    index++;
            //}
            //if (CCFBPrefs.FamilyCardSlot3 != -1)
            //{
            //    clsUserFields.setDataRow("UserFlag" + CCFBPrefs.FamilyCardSlot3.ToString());
            //    fldNames[index] = clsUserFields.EditLabel;
            //    fldVals[index] = clsClient.clsHH.GetDataValue(clsUserFields.FldName).ToString();
            //    index++;
            //}
            //if (CCFBPrefs.FamilyCardSlot4 != -1)
            //{
            //    clsUserFields.setDataRow("UserFlag" + CCFBPrefs.FamilyCardSlot4.ToString());
            //    fldNames[index] = clsUserFields.EditLabel;
            //    fldVals[index] = clsClient.clsHH.GetDataValue(clsUserFields.FldName).ToString();
            //    index++;
            //}

            if (File.Exists(templatePath))
            {
                FormWindowState tmpWindowState = this.WindowState;
                this.WindowState = FormWindowState.Minimized;
                Application.DoEvents();
                clsCreateClientCard.createReport(fbName, templatePath, true);
                //, fldNames, fldVals);
                this.WindowState = tmpWindowState;
                Application.DoEvents();
            }
            else
                MessageBox.Show("ERROR: " + templatePath + " Not Found", "Temlate Not Found",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void printFamilyCard2()
        {
            parmType pt = (parmType)cboSpecialLang.SelectedItem;
            string templatePath = CCFBGlobal.fb3TemplatesPath + "FamilyCardSigPad" + pt.ShortName + ".doc";

            SignFamilyCard frmSignFamilyCard = new SignFamilyCard(clsClient);
            frmSignFamilyCard.createReport(CCFBPrefs.FoodBankName, templatePath);
            frmSignFamilyCard.ShowDialog();
        }

        /// <summary>
        /// Traverses all controls on the page using recursion and adds the proper ones
        /// to their proper collections and adds LostFocus event to Textboxes and Checkboxes
        /// </summary>
        /// <param name="controlList"></param>
        private void traverseAndAddControlsToCollections(Control.ControlCollection controlList)
        {
            foreach (Control cntrl in controlList.OfType<Control>())
            {
                switch (cntrl.GetType().Name)
                {
                    case "TextBox":
                        {
                            if (cntrl.Tag != null 
                                && cntrl.Tag.ToString().Trim() != "TotalFamily" 
                                && cntrl.Tag.ToString() != "")
                            {
                                if (cntrl.Name.Substring(0, 3) == "tbm")
                                {
                                    tbmList.Add((TextBox)cntrl);
                                    cntrl.Leave += new System.EventHandler(this.tbmList_Leave);
                                }
                                else if (cntrl.Name.Substring(0, 3) == "tbd")
                                {
                                    tbdList.Add((TextBox)cntrl);
                                }

                                else
                                {
                                    if (cntrl.Name != "tbID")
                                    {
                                        cntrl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbList_KeyDown);
                                        cntrl.Leave += new System.EventHandler(this.tbList_LostFocus);
                                        tbList.Add((TextBox)cntrl);
                                    }
                                }
                            }
                            break;
                        }
                    case "CheckBox":
                        {
                            CheckBox chk = (CheckBox)cntrl;
                            chk.CheckedChanged += new System.EventHandler(this.chkList_CheckedChanged);
                            chk.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkList_KeyDown);
                            chkList.Add(chk);
                            break;
                        }
                    case "ComboBox":
                        {
                            if (cntrl.Tag != null)
                            {
                                if (cntrl.Tag.ToString().Trim() != "")
                                {
                                    cboList.Add((ComboBox)cntrl);
                                }
                            }
                            break;
                        }
                }

                traverseAndAddControlsToCollections(cntrl.Controls);
            }
        }

        private void tsbAddClient_Click(object sender, EventArgs e)
        {
            addHousehold(0);
        }

        private void tsbCreateAppt_Click(object sender, EventArgs e)
        {
            CreateNewAppointment();
        }

        private void tsbCSFP_Click(object sender, EventArgs e)
        {
            showCSFPForm();
        }

        private void tsbDfltApptDate_Click(object sender, EventArgs e)
        {
            DateSelectionForm frmDfltDatePicker = new DateSelectionForm(true);
            frmDfltDatePicker.ShowDialog();
            if (frmDfltDatePicker.DateIsSelected == true)
            {
                CCFBGlobal.DefalutApptDate = frmDfltDatePicker.DateSelected;
                tsbDfltApptDate.Text = CCFBGlobal.DefalutApptDate;
            }
        }

        private void tsbDfltSvcDate_Click(object sender, EventArgs e)
        {
            DateSelectionForm frmDfltDatePicker = new DateSelectionForm(false);
            frmDfltDatePicker.ShowDialog();
            try
            {
                if (frmDfltDatePicker.DateIsSelected == true)
                {
                    CCFBGlobal.DefaultServiceDate = frmDfltDatePicker.DateSelected;
                    tsbDfltSvcDate.Text = CCFBGlobal.DefaultServiceDate;
                    setPointsWeekOf();
                    fillForm();
                    //enabletsbNewService();
                }
            }
            catch { }
        }

        /// <summary>
        /// Deals with Find Client Button being clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbFindClient_Click(object sender, EventArgs e)
        {
            frmFindClient.Visible = true;   //Makes the Find Client Form Visible
        }

        private void tsbFoodDonations_Click(object sender, EventArgs e)
        {
            ShowDonationsForm();
        }

        private void tsbHHMem_Click(object sender, EventArgs e)
        {
            editHHMem();
            ShowFamData();
        }

        private void tsbLog_Click(object sender, EventArgs e)
        {
            if (frmTrxLog == null)
            {
                frmTrxLog = new TrxLogForm(this, clsClient);
                frmTrxLog.Show();
            }
            else
            {
                frmTrxLog.Visible = true;
            }
        }

        private void tsbNewService_Click(object sender, EventArgs e)
        {
            CreateNewFoodService();
        }

        private void tsbVouchers_Click(object sender, EventArgs e)
        {
            VoucherShortForm frmVouchers = new VoucherShortForm(clsClient);

            //VoucherForm frmVouchers = new VoucherForm(clsClient,DateTime.Today);
            frmVouchers.ShowDialog();
            getVoucherLogForPeriod();
        }

        private void typeCodesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditTypeCodes frmTypeCodes = new EditTypeCodes(CCFBGlobal.connectionString);
            frmTypeCodes.ShowDialog();
            loadParmData();
        }

        private void userListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UsersForm frmUsers = new UsersForm();
            frmUsers.ShowDialog();
        }

        private void tsbSearchClient_Click(object sender, EventArgs e)
        {
            frmClientSearch.BarCode = "";
            frmClientSearch.Visible = true;
        }

        private void btnAssignBarcode_Click(object sender, EventArgs e)
        {
            frmBarCodeEntry.INIT("Assign Barcode Form", clsClient.clsHH.Name, false);
            DialogResult dr = frmBarCodeEntry.ShowDialog();
            if (dr == DialogResult.OK)
            {
                try
                {
                    int barcode = Convert.ToInt32(frmBarCodeEntry.BarCode);
                    clsClient.clsHH.BarCode = barcode;
                    clsClient.clsHH.update(true);
                    showUserInfo();
                    btnAssignBarcode.BackColor = Color.MediumAquamarine;
                }
                catch (Exception)
                {
                }
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (CCFBPrefs.EnableBarcodePrompts == true && e.KeyCode == Keys.F2 && inEditMode == false)
            {
                ShowBarcodePrompt();
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.F2)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void ShowBarcodePrompt()
        {
            int iHHM = 0;
            frmBarCodeEntry.INIT("Barcode Reader Form", "Enter barcode for household", true);
            DialogResult dr = frmBarCodeEntry.ShowDialog(this);
            if (dr == DialogResult.OK)
            {
                string testBarCode = frmBarCodeEntry.BarCode;
                int testID = -1;
                if (CCFBPrefs.BarcodeUseFamilyMember == true)
                {
                    testID = CCFBGlobal.getHHFromBarCode(testBarCode, ref iHHM);
                }
                else
                {
                    testID = CCFBGlobal.getClientFromBarCode(testBarCode);
                }
                if (testID > 0)
                {
                    setHousehold(testID, iHHM);
                    if (frmBarCodeEntry.GiveService() == true && tsbNewService.Enabled == true )
                    {
                        CreateNewFoodService();
                    }
                }
                else
                {
                    clearForm();
                    tbAlert.Text = "No Client found where Barcode = " + testBarCode.ToString();
                    CCFBGlobal.playQBeep();
                    frmFindClient.Visible = true;
                }
            }
        }
        
        private void mnuHelp_Index_Click(object sender, EventArgs e)
        {
//            ReceiptPrinter clsRcptPrnt = new ReceiptPrinter();
//            clsRcptPrnt.printIssaquah(clsClient);
//            frmTestPrint frmTmp = new frmTestPrint(clsClient);
//            frmTmp.ShowDialog();
        }

        private void mnuTools_CashDonations_Click(object sender, EventArgs e)
        {
            CashDonationsForm frmCashDonations = new CashDonationsForm();
            frmCashDonations.ShowDialog();
        }

        private void mnuAdmin_BackupDatabase_Click(object sender, EventArgs e)
        {
            BackupDBForm tmpFrm = new BackupDBForm();
            tmpFrm.ShowDialog();
        }
        private void chkNeedIncomeVerification_CheckedChanged(object sender, EventArgs e)
        {
            if (inEditMode == true)
            {
                fillAutoAlert();

                if (chkNeedIncomeVerification.Checked == false)
                    clsClient.clsHH.IncomeVerifiedDate = DateTime.Today;
            }
        }

        private void cboVoucherLogPeriod_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Registry.SetValue(CCFBGlobal.registryKeyCurrentUser, constVoucherLogPeriod, cboTrxLogPeriod.SelectedIndex);
            getVoucherLogForPeriod();
        }

        private void btnEditVoucherLog_Click(object sender, EventArgs e)
        {
            //VoucherxForm frmVouchers = new VoucherxForm(clsClient, Convert.ToDateTime(btnEditVoucherLog.Tag));
            //frmVouchers.ShowDialog();
            //getVoucherLogForPeriod();
        }

        private void tbmList_Leave(object sender, EventArgs e)
        {

            if (inEditMode == true)
            {
                TextBox tbHH = (TextBox)sender; //Get the correct textbox
                if (tbHH.Tag.ToString() != "")
                {
                    //If current value does not = value of textbox
                    if (tbHH.Text.ToString() != clsClient.clsHH.GetDataValue(tbHH.Tag.ToString()).ToString())
                    {
                        clsClient.clsHH.SetDataValue(tbHH.Tag.ToString(), tbHH.Text.Trim());
                        //Recalculate total family if FamilyList is changed
                        if (tbHH.Tag.ToString() == "Infants" || tbHH.Tag.ToString() == "Youth"
                            || tbHH.Tag.ToString() == "Teens" || tbHH.Tag.ToString() == "Adults"
                            || tbHH.Tag.ToString() == "Seniors")
                        {
                            checkParseTextboxToInt(ref tbHH);
                            int newTotalFamily = (clsClient.clsHH.Infants + clsClient.clsHH.Youth + clsClient.clsHH.Adults
                                + clsClient.clsHH.Seniors + clsClient.clsHH.Teens + clsClient.clsHH.Eighteens);
                            tbTotalFam.Text = newTotalFamily.ToString();
                            if (clsClient.clsHH.TotalFamily != newTotalFamily)
                            {
                                clsClient.clsHH.TotalFamily = newTotalFamily;
                                SetIncomeGroups();
                            }
                        }
                    }
                }
            }
        }

        private void tbMonthlyIncome_Leave(object sender, EventArgs e)
        {
            if (inEditMode == true)
            {
                TextBox tbHH = (TextBox)sender; //Get the correct textbox
                int monthlyincome = 0;
                try
                {
                    monthlyincome = Convert.ToInt32(tbHH.Text);
                }
                catch { monthlyincome = 0; }
                if (monthlyincome > 0)
                {
                    int annualincome = monthlyincome * 12;
                    if (annualincome != clsClient.clsHH.AnnualIncome)
                    {
                        clsClient.clsHH.AnnualIncome = annualincome;
                        tbAnualIncome.Text = annualincome.ToString();
                        SetIncomeGroups();
                    }
                }
            }
        }

        private void mnuDatabaseStatistics_Click(object sender, EventArgs e)
        {
            CCFBStatisticsForm frmStats = new CCFBStatisticsForm(CCFBGlobal.connectionString);
            frmStats.ShowDialog();
        }

        private void mnuAdmin_SaveAsNewClientDefaults_Click(object sender, EventArgs e)
        {

        }

        private void mnuAdmin_EditJobsPlan_Click(object sender, EventArgs e)
        {
            FBJobsPlanForm frmTemp = new FBJobsPlanForm();
            frmTemp.ShowDialog();
        }

        private void cmsHHMembers_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            bool isVisible = true;
            if (clsClient.clsHHmem.HeadHH == true)
            { isVisible = false; }
            cmsHHMembers.Items["tsmiMoveHHMem"].Visible = isVisible;
            cmsHHMembers.Items["tsmiCreateHH"].Visible = isVisible;
            if (dgvHHMembers.CurrentRow.Index > 0)
            {
                string sname = dgvHHMembers.CurrentRow.Cells["clmLastName"].Value.ToString() + ", " + dgvHHMembers.CurrentRow.Cells["clmFirstName"].Value.ToString();
                cmsHHMembers.Items["tsmiCreateHH"].Text = "Create New Household using '" + sname + "'";
                cmsHHMembers.Items["tsmiMoveHHMem"].Text = "Move '" + sname + "' to different Household";
            }
        }

        private void cboServiceMethod_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (loadingInfo == false)
            {
                clsClient.clsHH.ServiceMethod = ((parmType)cboServiceMethod.SelectedItem).ID;
                ShowRoute();
            }
        }

        private void ShowRoute()
        {
            lblHDRoute.Visible = cboHDRoute.Visible = lblHDPrograms.Visible = lblHDBuildings.Visible 
                = cboHDBuilding.Visible = cboHDPrograms.Visible = lblHDRoute.Visible 
                = lblHDItem.Visible = cboHDItem.Visible = tbDriverNotes.Visible =
                ((parmType)cboServiceMethod.SelectedItem).ID == (int)CCFBGlobal.ServiceMethodCodes.HomeDeliveryActive;
            
        }

        private void mnuTools_HomeDelivery_Click(object sender, EventArgs e)
        {
        }

        private void enabletsbNewService()
        {
            bool allowFoodService = true;
            int nbrSvcs = 0;
            if (tbxNbrTrxThisWeek.Text != "")
            { nbrSvcs = Convert.ToInt32(tbxNbrTrxThisWeek.Text); }
            if (CCFBPrefs.AlertWeekSvc > 0
             && nbrSvcs >= CCFBPrefs.AlertWeekSvc)
            {
                allowFoodService = false;
            }
            else
            {
                nbrSvcs = 0;
                if (tbxNbrTrxThisMonth.Text != "")
                { nbrSvcs = Convert.ToInt32(tbxNbrTrxThisMonth.Text); }
                if (CCFBPrefs.AlertMonthSvc > 0
                 && nbrSvcs >= CCFBPrefs.AlertMonthSvc)
                {
                    allowFoodService = false;
                }
            }
            if (CCFBPrefs.WarnSvcEachPerson == true)
            {
                svcWarningText = clsClient.svcWarning(CCFBGlobal.DefaultServiceDate);
                if (svcWarningText != "")
                { fillAutoAlert(); }
                tsbNewService.Enabled = (clsClient.clsHH.Inactive == false);
            }
            else
            {
                tsbNewService.Enabled = (clsClient.clsHH.Inactive == false 
                                      && clsClient.hasTrxLogEntry(CCFBGlobal.DefaultServiceDate) == false 
                                      && allowFoodService ==true);
            }
            mnuClient_NewSvcOverRide.Visible = !tsbNewService.Enabled && CCFBGlobal.currentUser_PermissionLevel >= CCFBPrefs.OverRideLevel;
            allowNewServiceOverRide = false;
        }

        private void tsmiResetPOAFlag_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("WARNING: You Are About To Reset All Need Proof Of Address Flags To True.  Do You Want To Continue?",
                "WARNING: Need Proof Of Address Flag About To Change", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) ==
                System.Windows.Forms.DialogResult.Yes)
            {
                CCFBGlobal.executeQuery("Update Household Set NeedToVerifyID = 1");
            }

            clsClient.open(clsClient.clsHH.ID, false, false);
            fillForm();
        }

        private void tsmiResetNeedCommSigFlag_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("WARNING: You Are About To Reset All Need Commodity Signiture Flags To True.  Do You Want To Continue?",
                "WARNING: Need Commodity Signiture Flag About To Change", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) ==
                System.Windows.Forms.DialogResult.Yes)
            {
                CCFBGlobal.executeQuery("Update Household Set NeedCommoditySignature = 1");
            }

            clsClient.open(clsClient.clsHH.ID, false, false);
            fillForm();
        }

        private void tsmiResetInactiveFlag_Click(object sender, EventArgs e)
        {
            MarkHouseholdInactive frmMarkInactive = new MarkHouseholdInactive();
            frmMarkInactive.ShowDialog(this);
            if (frmMarkInactive.NeedToRefresh == true)
            {
                frmFindClient.loadList();
                frmFindClient.SetIdAndClose();
            }
            ////////if (MessageBox.Show("WARNING: You Are About To Set All Households Who Have Not Had A Transaction For The Last Year As Inactive. Do You Want To Continue?",
            ////////    "WARNING: Inactive Flags About To Change", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) ==
            ////////    System.Windows.Forms.DialogResult.Yes)
            ////////{
            ////////    CCFBGlobal.executeQuery("Update Household Set Inactive = 1 Where LatestService < '" 
            ////////        + DateTime.Today.AddYears(-1).ToShortDateString() + "'");
            ////////}
        }

        private void cboHD_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (loadingInfo == false)
            {
                ComboBox cbo = (ComboBox)sender;
                clsClient.clsHH.SetDataValue(cbo.Tag.ToString(), cbo.SelectedValue.ToString());
            }
        }

        private void btnNavigate_KeyDown(object sender, KeyEventArgs e)
        {
            if (inEditMode == true)
            { }
            //else if (CCFBPrefs.EnableBarcodePrompts == true && e.KeyCode == Keys.F2)
            //{
            //    ShowBarcodePrompt();
            //    e.SuppressKeyPress = true;
            //}


        }

        private void tsmiCreateUnitedWayExport_Click(object sender, EventArgs e)
        {
            UWKCExportForm frmUWKCExport = new UWKCExportForm();
            frmUWKCExport.ShowDialog();
        }

        private void tsmiKingCountyReport_Click(object sender, EventArgs e)
        {
            KCReportForm frmKCReport = new KCReportForm();
            frmKCReport.ShowDialog();
        }

        private void lvVoucherLog_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvVoucherLog.FocusedItem != null)
                btnEditVoucherLog.Tag = lvVoucherLog.FocusedItem.SubItems[1].Text;
        }

        private void tpgTrxLog_Resize(object sender, EventArgs e)
        {
            lvTrxLog.Width = tpgTrxLog.ClientSize.Width;
            lvTrxLog.Height = tpgTrxLog.ClientSize.Height - lvTrxLog.Top;
        }

        private void tbeAddress_Leave(object sender, EventArgs e)
        {
            if (inEditMode == true)
            {
                string tmp = CCFBGlobal.cleanAddress(tbeAddress.Text);
                if (tmp != tbeAddress.Text)
                    tbeAddress.Text = tmp;
            }
        }

        private void tbPhone_Leave(object sender, EventArgs e)
        {
            if (inEditMode == true)
            {
                MaskedTextBox tbHH = (MaskedTextBox)sender; //Get the correct textbox
                if (tbHH.Tag.ToString() != "")
                {       //If current value does not = value of textbox
                    if (tbHH.Text.ToString() != clsClient.clsHH.GetDataString(tbHH.Tag.ToString()))
                    {
                        clsClient.clsHH.SetDataValue(tbHH.Tag.ToString(), tbHH.Text.Trim());
                    }
                }
            }
        }

        private void mnuTools_MaintainVoucherItems_Click(object sender, EventArgs e)
        {
            EditVouchersItemForm frmEditVoucherItems = new EditVouchersItemForm();
            frmEditVoucherItems.Show();
        }

        private void mnuHD_Planner_Click(object sender, EventArgs e)
        {
            HDPlannerForm frmHDPlanner = new HDPlannerForm(this);
            // Use Show instead of ShowDialog here. This way we can edit the main form while the dialog is open
            frmHDPlanner.Show();
            // Don't populate the combo box here. It is already updated automaticly as the user makes changes in the planner
        }

        public void refreshHDRoute()
        {
            int preSelected = cboHDRoute.SelectedIndex;
            CCFBGlobal.dtPopulateCombo(cboHDRoute, "SELECT * FROM HDRoutes ORDER BY ID", "RouteTitle", "ID", "No Routes Available", conn);
            cboHDRoute.SelectedIndex = preSelected;
        }

        private void mnuHD_FundingPrograms_Click(object sender, EventArgs e)
        {
            //HDProcessorForm frmHDProcessor = new HDProcessorForm();
            //frmHDProcessor.Show();
        }

        private void mnuHD_Buildings_Click(object sender, EventArgs e)
        {

        }

        private void pbxEditAlert_Click(object sender, EventArgs e)
        {
            EditAlertForm frmEditAlert = new EditAlertForm(clsClient.clsHH.AlertText);
            frmEditAlert.ShowDialog(this);
            if (frmEditAlert.PressedSave() == true)
            {
                mergeRTF(tbAlert, rtAlert, frmEditAlert.Rtf());
                clsClient.clsHH.AlertText = frmEditAlert.Rtf();
                if (inEditMode == false)
                {
                    clsClient.clsHH.update(true);
                    showUserInfo();
                }
            }
        }
        
        private void mergeRTF(RichTextBox rtb, string rtf1, string rtf2)
        {
            rtb.Rtf = rtf1;
            if (rtf2 != "")
            {
                Clipboard.SetText(rtf2, TextDataFormat.Rtf);
                rtb.SelectionStart = rtb.Text.Length;
                rtb.ReadOnly = false;
                rtb.Paste();
                rtb.ReadOnly = true;
            }
        }

        private void tsmiToggleUserInfo_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = !groupBox1.Visible;
        }

        //private void printClientReceipt()
        //{
        //    ReceiptPrinter clsRcptPrnt = new ReceiptPrinter();
        //    clsRcptPrnt.printIssaquah(clsClient);
        //}
        private void showUserInfo()
        {
                lblHHCreated.Text =  " Created: " + clsClient.clsHH.Created.ToShortDateString() + " " + clsClient.clsHH.Created.ToShortTimeString() + "  " + clsClient.clsHH.CreatedBy;
                lblHHModified.Text = "Modified: " + clsClient.clsHH.Modified.ToShortDateString() + " " + clsClient.clsHH.Modified.ToShortTimeString() + "  " + clsClient.clsHH.ModifiedBy;
        }

        private void mnuClient_NewSvcOverRide_Click(object sender, EventArgs e)
        {
            allowNewServiceOverRide = !allowNewServiceOverRide;
            tsbNewService.Enabled = allowNewServiceOverRide;
        }

        private void lvTrxLog_SelectedIndexChanged(object sender, EventArgs e)
        {
            curtrxLogID = 0;
            if (lvTrxLog.SelectedItems.Count > 0)
            {
                if (lvTrxLog.SelectedItems[0].SubItems[3].Text != "CSFP")
                {
                    curtrxLogID = Convert.ToInt32(lvTrxLog.SelectedItems[0].Tag);
                }
                cmsLog.Items["tsmiShowSignature"].Visible = (lvTrxLog.SelectedItems[0].ImageIndex == 0);
            }
            btnEditTransLog.Enabled = (curtrxLogID > 0);
        }

        private void lvTrxLog_DoubleClick(object sender, EventArgs e)
        {
            if (lvTrxLog.SelectedItems != null)
            {
                openEditServiceTrx(Convert.ToInt32(lvTrxLog.SelectedItems[0].Tag));
            }
        }

        private void mnuWSDAFAP_Click(object sender, EventArgs e)
        {
            string sTitle = "Washington State Food Assistance Programs";
            string sURL = "http://agr.wa.gov/FoodProg/";
            switch (CCFBPrefs.DefaultState)
            {
                case "OR":
                    sTitle = "Oregon State Food Assistance Programs";
                    sURL = "http://www.oregon.gov/ohcs/pages/css_emergency_food_assistance_usda_commodities_oregon.aspx";
                    break;
                default:
                    break;
            }
            WebPageForm frmTemp = new WebPageForm(sTitle,
                sURL);
            frmTemp.ShowDialog();
        }

        private void mnuClient_NewService_Click(object sender, EventArgs e)
        {
            CreateNewFoodService();
        }

        private void printAllClientCards()
        {
            SelectDateRange frmDateRange = new SelectDateRange();
            if (frmDateRange.ShowDialog() == DialogResult.OK)
            {
                DateTime dtFirst = frmDateRange.FirstDate();
                DateTime dtLast = frmDateRange.LastDate();
                SqlConnection sqlConn = new SqlConnection(CCFBGlobal.connectionString);
                SqlCommand sqlCmd = new SqlCommand(
                    "SELECT ID, Name FROM Household WHERE LatestService Between '"
                    + dtFirst.ToShortDateString() + "' AND '"
                    + dtLast.ToShortDateString() + "' ORDER BY Name Desc", sqlConn);
                sqlConn.Open();
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCmd);
                DataTable dtblHH = new DataTable();
                int iRowCount = sqlAdapter.Fill(dtblHH);
                if (iRowCount > 0)
                {
                    DialogResult dr = MessageBox.Show("Do you want to print family cards for " + iRowCount.ToString() + " households?"
                        , "Print All Housweholds"
                        , MessageBoxButtons.YesNo
                        , MessageBoxIcon.Question
                        , MessageBoxDefaultButton.Button1);
                    if (dr == DialogResult.Yes)
                    {
                        int cntr = 0;
                        splCntrCardMembers.Hide();
                        pnlProgress.Show();
                        lblProgress.Text = "Print All Family Cards Progress";
                        progressBar1.Minimum = 0;
                        progressBar1.Maximum = dtblHH.Rows.Count;
                        Application.DoEvents();
                        foreach (DataRow drow in dtblHH.Rows)
                        {
                            cntr ++;
                            progressBar1.Value = cntr;
                            Application.DoEvents();
                            setHousehold(Convert.ToInt32(drow[0]), 0);
                            printFamilyCard();
                        }
                        pnlProgress.Hide();
                        splCntrCardMembers.Show();
                    }
                }
                sqlConn.Close();
            }
        }

        private void setPointsWeekOf()
        {
            if (CCFBPrefs.EnablePointsTracking == true)
            {
                DateTime dt = Convert.ToDateTime(CCFBGlobal.DefaultServiceDate);
                ptsDoW = (int)dt.DayOfWeek;
                ptsWeekOf = dt.AddDays(-ptsDoW);
                lvwPoints.Columns[1].Text = ptsWeekOf.ToShortDateString(); 

            }
        }

        private void clearPointsDisplay()
        {
            foreach (ListViewItem  item in lvwPoints.Items)
            {
                item.SubItems[1].Text= "0";
            }
        }

        private void showPoints()
        {
            if (CCFBPrefs.EnablePointsTracking == true)
            {
                if (clsPts == null)
                {
                    clsPts = new HHPoints(conn);
                }
                string whereclause = "HhID = " + clsClient.clsHH.ID.ToString() + " AND WeekOf = '" + ptsWeekOf.ToShortDateString() + "'";
                clsPts.openWhere(whereclause);
                if (clsPts.RowCount == 0)
                {
                    clsPts.insert();
                    clsPts.HhID = clsClient.clsHH.ID;
                    clsPts.WeekOf = ptsWeekOf;
                    if (clsClient.clsHH.ClientType == 1)
                    {
                        clsPts.Allocated = CCFBPrefs.PointsPerWeek + CCFBPrefs.PointsPerFamMbr * (clsClient.clsHH.TotalFamily - 1);
                        if (clsPts.Allocated > CCFBPrefs.MaxPointsPerWeek)
                        {
                            clsPts.Allocated = CCFBPrefs.MaxPointsPerWeek;
                        }
                    }
                    else
                    {
                        clsPts.Allocated = CCFBPrefs.PointsPerWeekOutOfArea; //+ CCFBPrefs.PointsPerFamMbr * (clsClient.clsHH.TotalFamily - 1);
                    }
                    clsPts.update();
                    clsPts.refreshDataTable();
                }
                if (clsPts.RowCount > 0)
                {
                    lvwPoints.Items[0].SubItems[1].Text = clsPts.Allocated.ToString();
                    lvwPoints.Items[1].SubItems[1].Text = clsPts.Pts1.ToString();
                    lvwPoints.Items[2].SubItems[1].Text = clsPts.Pts2.ToString();
                    lvwPoints.Items[3].SubItems[1].Text = clsPts.Pts3.ToString();
                    lvwPoints.Items[4].SubItems[1].Text = clsPts.Pts4.ToString();
                    lvwPoints.Items[5].SubItems[1].Text = clsPts.Pts5.ToString();
                    lvwPoints.Items[6].SubItems[1].Text = clsPts.Pts6.ToString();
                    lvwPoints.Items[7].SubItems[1].Text = (clsPts.Allocated - clsPts.Pts1 - clsPts.Pts2 - clsPts.Pts3 - clsPts.Pts4 - clsPts.Pts5 - clsPts.Pts6).ToString();
                    lvwPoints.Items[ptsDoW].Selected = true;
                    lblPoints.Text = "Points for " + Convert.ToDateTime(CCFBGlobal.DefaultServiceDate).DayOfWeek;
                    tbpPoints.Text = lvwPoints.Items[ptsDoW].SubItems[1].Text;
                }
                try
                {
                    if (clsClient.clsHH.ClientType == 1)
                    {
                        lvwPoints.BackColor = Color.Cornsilk;
                    }
                    else
                    {
                        lvwPoints.BackColor = Color.LightSalmon;
                    }
                }
                catch (Exception)
                { }
            }
        }

        private void tbpPoints_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                int newval = 0;
                try
                {
                    newval = Convert.ToInt32(tbpPoints.Text);
                }
                catch (Exception)
                { }
                lvwPoints.Items[ptsDoW].SubItems[1].Text = newval.ToString();
                clsPts.SetDataValue("pts" + ptsDoW.ToString(), newval.ToString());
                lvwPoints.Items[7].SubItems[1].Text = (clsPts.Allocated - clsPts.Pts1 - clsPts.Pts2 - clsPts.Pts3 - clsPts.Pts4 - clsPts.Pts5 - clsPts.Pts6).ToString();
                clsPts.update();
            }
        }

        private void showCSFPForm()
        {
            EditCSFPForm frmEditCSFP = new EditCSFPForm();
            frmEditCSFP.Show();
        }

        private void mnuTools_Backpack_Click(object sender, EventArgs e)
        {
            EditBackpackForm frmEditBackpack = new EditBackpackForm();
            frmEditBackpack.Show();
        }

        private void mnuTools_CSFPServices_Click(object sender, EventArgs e)
        {
            showCSFPForm();
        }

        public bool get_tsbNewServiceEnabled()
        {
            return tsbNewService.Enabled;
        }

        public string HHName()
        {
            return clsClient.clsHH.Name;
        }

        public string HHAddress()
        {
            return clsClient.clsHH.Address + Environment.NewLine + clsClient.clsHH.City + "  " + clsClient.clsHH.Zipcode;
        }

        private void tpgVouchers_Resize(object sender, EventArgs e)
        {
            lvVoucherLog.Width = tpgVouchers.ClientSize.Width;
            lvVoucherLog.Height = tpgVouchers.ClientSize.Height - lvVoucherLog.Top;
        }

        private void tsmiPrintClientForm_Click(object sender, EventArgs e)
        {
            PRNTFamilyCard clsCreateClientCard = new PRNTFamilyCard(clsClient);
            ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;
            string templatePath =  CCFBGlobal.fb3TemplatesPath + "Clientcard" + tsmi.Name.ToString().Substring(tsmi.Name.Length-3) + ".doc";
            string fbName = CCFBPrefs.FoodBankName;

            if (File.Exists(templatePath) == false)
                templatePath = CCFBGlobal.fb3TemplatesPath + "ClientcardENG.doc";

            if (File.Exists(templatePath))
            {
                FormWindowState tmpWindowState = this.WindowState;
                this.WindowState = FormWindowState.Minimized;
                Application.DoEvents();
                clsCreateClientCard.createReport(fbName, templatePath, false);
                this.WindowState = tmpWindowState;
                Application.DoEvents();
            }
            else
                MessageBox.Show("ERROR: " + templatePath + " Not Found", "Template Not Found",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void freshAllianceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FreshAllianceForm fa = new FreshAllianceForm(-1, 6, true);
            fa.Show();
        }

        private void mnuClient_PrintClientCard_Click(object sender, EventArgs e)
        {
            if (CCFBPrefs.CaptureSignature == true)
            {
                printFamilyCard2();
            }
            else
            {
                printFamilyCard();
            }
        }

        private void mnuReports_CustomQuery_Click(object sender, EventArgs e)
        {
            TestExportToCSV frmTest = new TestExportToCSV();
            frmTest.ShowDialog();
        }
    }
}
