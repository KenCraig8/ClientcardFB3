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

namespace ClientcardFB3
{
    public partial class MainForm : Form
    {

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
        const int constHHTabWidth = 304;
        bool ethnicitySelectedFirstTime = false;

        bool formClear = false;
        EditServiceShortForm frmEditServices = new EditServiceShortForm();
        ClientSearch frmClientSearch;

        FindClientForm frmFindClient;   //A pointer to the FindClientForm
        LoginForm frmLogIn;
        TrxLogForm frmTrxLog;
        BarCodeEntryForm frmBarCodeEntry = new BarCodeEntryForm();
        private static Graphics gfxScreenshot;
        List<IncomeGroupMatrix> incomeGroups = new List<IncomeGroupMatrix>();

        bool inEditMode = false;    //Used to tell if in Edit mode or not
        bool loadingInfo = true;    //Stops events from firing if loading
        bool logout = false;
        Image photo;
        string sCSFPList = "";
        List<TextBox> tbList = new List<TextBox>();     //Collection of Editable Textboxes
        List<TextBox> tbmList = new List<TextBox>();     //Collection of Members Controled Textboxes

        private TabPage tpTmpUserFields;

        List<string> userFlagNames = new List<string>();    //Collection of all UserFlag Names
        #endregion

        public MainForm(LoginForm FrmLogInIn)
        {
            frmLogIn = FrmLogInIn;

            InitializeComponent();

            conn = new SqlConnection(CCFBGlobal.connectionString);

            for (int i = 0; i < chkLstBxUserFields.Items.Count; i++)
            {
                userFlagNames.Add(chkLstBxUserFields.Items[i].ToString());
            }

            cboTrxLogPeriod.SelectedIndex = 3;

            SetEnvironmentFromPrefs();

            dataGridMembers.ColumnHeadersDefaultCellStyle.Font =
                new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Regular,
                    System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            //dataGridMembers.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.DarkRed;
            CCFBGlobal.LoadTypes();
            clsClient = new Client();
            clsZipcodes = new Zipcodes(CCFBGlobal.connectionString);
            clsUserFields = new UserFields(CCFBGlobal.connectionString);
            clsDaysOpen = new DaysOpen(CCFBGlobal.connectionString);

            frmFindClient = new FindClientForm(this);
            frmClientSearch = new ClientSearch(CCFBGlobal.connectionString, this);

            clsClient.open(frmFindClient.CurrentHHId, true, true);
            CCFBGlobal.clsDailyItems = new DailyItemsClass();

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

            SetCurrentServiceDate();    //Sets Default Service Date
            SetCurrentApptDate();       //Sets Default Appt Date

            loadParmData();         //Loads the ComboBox's with the Parm Data
            chkLstBxUserFields.Enabled = false;
            loadUserFieldLabels();  //Loads User Check Box Labels
            PopulatelvIncomeGroups();
            changeEditMode(false);
            splitContainer2.SplitterDistance = 120;
            fillForm();             //Fills the form with the Household data
        }

        private void addHousehold()
        {
            AddNewClientOrHHMem frmAddNewClient = new AddNewClientOrHHMem(clsClient, false);
            frmAddNewClient.ShowDialog();
            if (frmAddNewClient.HHID > 0)
            {
                clsClient.open(frmAddNewClient.HHID, true, true);
                if (clsClient.clsHH.UseFamilyList == true)
                {
                    clsClient.UpdateDataBasedOn(DateTime.Parse(CCFBGlobal.DefaultServiceDate));
                }
                if (CCFBPrefs.FindClientAutoRefresh == true)
                    frmFindClient.loadList();
            }
            else
                clsClient.RefreshHousehold();
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
        }

        private void btnDeleteHHMem_Click(object sender, EventArgs e)
        {
            deleteHHMem();
        }

        private void btnEditTransLog_Click(object sender, EventArgs e)
        {
            if (btnEditTransLog.Tag.ToString() != "" && lvHHLog.SelectedItems.Count > 0)
            {
                openEditServiceTrx(Convert.ToInt32(lvHHLog.SelectedItems[0].SubItems[18].Text));

            }
        }

        private void btnEnlarge_Click(object sender, EventArgs e)
        {
            splitMemTrans.Panel1Collapsed = true;
            btnEnlarge.Visible = false;
            btnColapse.Visible = true;
        }

        /// <summary>
        /// Event Triggers when next client button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNext_Click(object sender, EventArgs e)
        {
            int hhid = Convert.ToInt32(tbID.Text);
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
            int hhid = Convert.ToInt32(tbID.Text);
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
            dataGridMembers.BackgroundColor = Color.Cornsilk;
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

            bmpScreenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height,
                PixelFormat.Format32bppArgb);
            // Create a graphics object from the bitmap
            gfxScreenshot = Graphics.FromImage(bmpScreenshot);
            // Take the screenshot from the upper left corner to the right bottom corner
            gfxScreenshot.CopyFromScreen(Screen.PrimaryScreen.Bounds.X,
                Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size,
                CopyPixelOperation.SourceCopy);

            Bitmap printscreen = new Bitmap(this.Width, this.Height);

            Graphics graphics = Graphics.FromImage(printscreen as Image);

            graphics.CopyFromScreen(0, 0, 0, 0, printscreen.Size);

            foreach (CheckBox cb in chkList)
            {
                cb.Enabled = false;
            }

            printscreen.Save(@"C:\ClientcardFB3\ScreenShots\ClientcardFB3.screen.jpg", ImageFormat.Jpeg);

            photo = Image.FromFile(@"C:\ClientcardFB3\ScreenShots\ClientcardFB3.screen.jpg");

            printDocument1.DefaultPageSettings.Landscape = true;
            dlg.Document = printDocument1;

            DialogResult result = dlg.ShowDialog();

            // If the result is OK then print the document.
            if (result == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void cboClientType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            clsClient.clsHH.ClientType = ((parmType)cboClientType.SelectedItem).ID;
        }

        private void cboIDType_SelectionChangeCommitted(object sender, EventArgs e)
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

        private void cboPhoneType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            clsClient.clsHH.PhoneType = ((parmType)cboPhoneType.SelectedItem).ID;
        }

        private void cboSpecialLang_SelectionChangeCommitted(object sender, EventArgs e)
        {
            clsClient.clsHH.EthnicSpeaking = ((parmType)cboSpecialLang.SelectedItem).ID;
        }

        private void cboTrxLogPeriod_SelectionChangeCommitted(object sender, EventArgs e)
        {
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
            dataGridMembers.BackgroundColor = baseBackColor;
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

        private void chkCommSigOnFile_CheckedChanged(object sender, EventArgs e)
        {
            if (inEditMode == true)
                fillAutoAlert();
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
                        if (chkHH.Tag.ToString() == "Inactive")
                            tsbNewService.Enabled = !chkHH.Checked;
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
            tbLastComodity.Visible = bIsVisible;
            chkNeedCommSig.Visible = bIsVisible;
            lblNeedCommodSignature.Visible = bIsVisible;
            lblLastComm.Visible = bIsVisible;
        }

        /// <summary>
        /// Event triggered when the UseFamilyList checkbox is checked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkUseFamList_CheckedChanged(object sender, EventArgs e)
        {
            setFamilyStatsMode();

            if (chkUseFamList.Checked == false)
            {
                if (loadingInfo == false)
                {
                    clsClient.clsHH.UseFamilyList = false;
                    //clsClient.clsHH.update();
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
                //clsClient.clsHH.update();
                //chksChanged.Clear();
            }
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
            foreach (TextBox tb in tbList)
            {
                if (tb.Name == "tbeName")
                {
                    string s = "";
                }
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
            cboPhoneType.SelectedValue = 9;
            cboIDType.SelectedValue = 4;
            cboSpecialLang.SelectedValue = 1;
            cboClientType.SelectedValue = 0;
            btnBeginEdit.Enabled = false;
            tsbCreateAppt.Enabled = false;
            tsbNewService.Enabled = false;
            lvHHLog.Items.Clear();
            dataGridMembers.Rows.Clear();
            //            btnManageHHMem.Enabled = false;
            btnNext.Enabled = false;
            btnPrevious.Enabled = false;
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
            if (e.ClickedItem.Text == "Expand HH Members")
            {
                splitContainer2.Panel1Collapsed = false;
                splitContainer2.Panel2Collapsed = true;
                cmsHHMembers.Items[0].Text = "Shrink";
                cmsHHMembers.Items[1].Text = "Export To Excel";

            }
            else if (e.ClickedItem.Text == "Shrink")
            {
                splitContainer2.Panel1Collapsed = false;
                splitContainer2.Panel2Collapsed = false;
                cmsHHMembers.Items[0].Text = "Expand HH Members";
                cmsHHMembers.Items[1].Text = "Export To Excel";
            }
            else
            {
                CCFBGlobal.ExportToExcell(dataGridMembers, "HouseholdMembers_"
                    + clsClient.clsHH.Name.Replace(',', '_').Trim().ToUpper());
            }
        }

        private void cmsLog_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "Expand HH Transactions")
            {
                splitContainer2.Panel1Collapsed = true;
                splitContainer2.Panel2Collapsed = false;
                cmsLog.Items[0].Text = "Shrink";
                cmsLog.Items[1].Text = "Export To Excel";
            }
            else if (e.ClickedItem.Text == "Shrink")
            {
                splitContainer2.Panel1Collapsed = false;
                splitContainer2.Panel2Collapsed = false;
                cmsLog.Items[0].Text = "Expand HH Transactions";
                cmsLog.Items[1].Text = "Export To Excel";
                //lvHHLog.Height = splitContainer2.Panel2.Height;
            }
            else
            {
                CCFBGlobal.ExportToExcell(lvHHLog, "TrxLog_"
                    + clsClient.clsHH.Name.Replace(',', '_').Trim().ToUpper());
            }
        }

        private void CreateNewAppointment()
        {
            frmEditServices.initForm(clsClient, true, true, -1, clsClient.ServingHHMemID);
            frmEditServices.ShowDialog();
            getClientLogForPeriod();
        }

        private void CreateNewFoodService()
        {
            int trxid = -1;
            clsClient.clsHHSvcTrans.openUsingDateRange(clsClient.clsHH.ID, Convert.ToDateTime(CCFBGlobal.DefaultServiceDate), 
                Convert.ToDateTime(CCFBGlobal.DefaultServiceDate));
            if (clsClient.clsHHSvcTrans.RowCount > 0)
                trxid = clsClient.clsHHSvcTrans.TrxId;
            frmEditServices.initForm(clsClient, true, false, trxid, clsClient.ServingHHMemID);
            frmEditServices.ShowDialog();
            if (clsClient.clsHH.LatestService != frmEditServices.ServiceDate)
            {
                clsClient.UpdateLatestServiceDates();
            }
            clsClient.open(clsClient.clsHH.ID, true, true);
            getClientLogForPeriod();
            fillForm();
            setFirstService();
        }

        private void dataGridMembers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridMembers.Rows.Count > 0 && (e.ColumnIndex == 9 || e.ColumnIndex == 10))
            {
                dataGridMembers.EndEdit();
                bOriValue = (bool)dataGridMembers.Rows[e.RowIndex].Cells[e.ColumnIndex].
                    GetEditedFormattedValue(e.RowIndex, DataGridViewDataErrorContexts.Commit);
                clsClient.clsHHmem.SetDataValue(
                    dataGridMembers[e.ColumnIndex, e.RowIndex].Tag.ToString(), bOriValue.ToString());
                clsClient.UpdateDataBasedOn(Convert.ToDateTime(CCFBGlobal.DefaultServiceDate));
                clsClient.clsHHmem.update();
                tbmDiet.Text = clsClient.clsHH.SpecialDiet.ToString();
                tbmDisabled.Text = clsClient.clsHH.Disabled.ToString();
                dataGridMembers[e.ColumnIndex, e.RowIndex].ReadOnly = false;
            }
        }

        private void dataGridMembers_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridMembers.Focused == true && loadingInfo == false)
            {
                int rowNbr = dataGridMembers.CurrentRow.Index;
                int colNbr = dataGridMembers.CurrentCell.ColumnIndex;
                if (rowNbr <= clsClient.clsHHmem.RowCount - 1)
                {
                    if (colNbr != 9 || colNbr != 10)
                    {
                        try
                        {
                            if (dataGridMembers.Columns[colNbr].ReadOnly == false)
                            {
                                clsClient.clsHHmem.SetDataValue(dataGridMembers.CurrentCell.Tag.ToString(),
                                    dataGridMembers.CurrentCell.GetEditedFormattedValue(e.RowIndex, DataGridViewDataErrorContexts.Commit).ToString());

                                clsClient.UpdateDataBasedOn(Convert.ToDateTime(CCFBGlobal.DefaultServiceDate));

                                clsClient.clsHHmem.Modified = DateTime.Now;
                                clsClient.clsHHmem.ModifiedBy = CCFBGlobal.currentUser_Name;
                                if (clsClient.clsHHmem.update() == true)
                                {
                                    dataGridMembers.Rows[rowNbr].Cells["clmGroup"].Value =
                                        ageGroups[clsClient.clsHHmem.AgeGroup];
                                    dataGridMembers.Rows[rowNbr].Cells["clmAge"].Value =
                                        clsClient.clsHHmem.Age;
                                    ShowFamData();
                                }
                            }
                        }
                        catch
                        {
                            dataGridMembers.Rows[rowNbr].Cells[dataGridMembers.CurrentCell.ColumnIndex].Value =
                                clsClient.clsHHmem.DSet.Tables[0].Rows[rowNbr].Field<DateTime>
                                (dataGridMembers.CurrentCell.Tag.ToString()).ToShortDateString();
                        }
                    }
                }
                else
                {
                    dataGridMembers.Rows[rowNbr].Cells[dataGridMembers.CurrentCell.ColumnIndex].Value = "";
                }

                Color foreClr = new Color();
                if ((bool)dataGridMembers.Rows[e.RowIndex].Cells[0].Value == true)
                    foreClr = Color.Maroon;
                else
                    foreClr = Color.Black;
                for (int i = 0; i < dataGridMembers.ColumnCount; i++)
                {
                    dataGridMembers.Rows[e.RowIndex].Cells[i].Style.ForeColor = foreClr;
                }
            }
        }

        private void dataGridMembers_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridMembers.Columns.Count - 1)
                dataGridMembers.CurrentCell = dataGridMembers[0, e.RowIndex];
        }

        private void dataGridMembers_DoubleClick(object sender, EventArgs e)
        {
            HHMemGridForm frmHHmem = new HHMemGridForm(clsClient, this,
               Convert.ToInt32(dataGridMembers.CurrentRow.Cells["clmHMID"].Value));
            frmHHmem.ShowDialog();
        }

        private void dataGridMembers_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (loadingInfo == false)
                    clsClient.clsHHmem.find(Convert.ToInt32(dataGridMembers.Rows[e.RowIndex].Cells["clmHMID"].Value));
            }
            catch { }
        }

        private void deleteHHMem()
        {
            if (dataGridMembers.CurrentRow != null)
            {
                if ((bool)dataGridMembers.CurrentRow.Cells["clmHeadHH"].Value != true
                    && MessageBox.Show("Are You Sure You Want To Delete "
                    + dataGridMembers.CurrentRow.Cells["clmLastName"].Value.ToString().ToUpper()
                    + ", " + dataGridMembers.CurrentRow.Cells["clmFirstName"].Value.ToString().ToUpper() + "?",
                    "Delete Household Member", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes
                    && dataGridMembers.RowCount > 1)
                {
                    clsClient.clsHHmem.delete(Convert.ToInt32(dataGridMembers.CurrentRow.Cells["clmHMId"].Value));
                    clsClient.clsHHmem.openHHID(clsClient.clsHH.ID);
                    fillForm();
                }
                else
                {
                    if ((bool)dataGridMembers.CurrentRow.Cells["clmHeadHH"].Value == true)
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
            if (dataGridMembers.CurrentRow != null)
            {
                hhMemID = Convert.ToInt32(dataGridMembers.CurrentRow.Cells["clmHMId"].Value);
                clsClient.clsHHmem.find(hhMemID);
            }
            HHMemGridForm frmHHMem = new HHMemGridForm(clsClient, this, hhMemID);
            frmHHMem.ShowDialog();
            loadHHMems(true);
        }

        private void editServiceTrxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvHHLog.FocusedItem != null)
            {
                openEditServiceTrx(Convert.ToInt32(lvHHLog.FocusedItem.SubItems[16].Text));
            }
            else
            {
                MessageBox.Show("Please Select A Transaction To Edit and Try Again");
            }
        }

        private void EnableIncomeGrp(bool bEnabled)
        {
            tbAnualIncome.ReadOnly = !bEnabled;
        }

        private void EnableVerifyIdGrp(bool bEnabled)
        {
            chkNeedVerifyID.Enabled = bEnabled;
            cboIDType.Enabled = bEnabled;
            tbeDateIDVerified.ReadOnly = !bEnabled;
        }

        private void enterGroceryRescueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmFoodReceipts frmTemp = new frmFoodReceipts(-1, 6);
            frmTemp.ShowDialog();
        }

        ///// <summary>
        ///// Calculates the Households Last Commodity and Sets that value in the LastCommodity Textbox
        ///// </summary>
        //private void getLastCommodity()
        //{
        //    for (int i = 0; i < clsClient.clsHHSvcTrans.RowCount; i++)
        //    {
        //        if (clsClient.clsHHSvcTrans.DSet.Tables[0].Rows[i].Field<int>("LbsCommodity") > 0)
        //        {
        //            if (tbLastComodity.Text == "" ||
        //                DateTime.Parse(tbLastComodity.Text) <
        //                clsClient.clsHHSvcTrans.DSet.Tables[0].Rows[i].Field<DateTime>("TrxDate"))
        //            {
        //                tbLastComodity.Text = clsClient.clsHHSvcTrans.DSet.Tables[0].Rows[i]["TrxDate"].ToString();
        //            }
        //        }
        //    }
        //}

        /// <summary>
        /// Fills the AutoAlert Textbox with Proper info
        /// </summary>
        private void fillAutoAlert()
        {
            tbAlert.Text = "";
            if (clsClient.clsHH.NeedToVerifyID == true && CCFBPrefs.EnableVerifyId == true)
            {
                tbAlert.SelectionColor = Color.OrangeRed;
                tbAlert.SelectedText = "Need to Verify ID" + Environment.NewLine;
            }
            if (clsClient.clsHH.NeedCommoditySignature == true && clsClient.clsHH.NoCommodities == false && CCFBPrefs.EnableTEFAP == true)
            {
                tbAlert.SelectionColor = Color.OrangeRed;
                tbAlert.SelectedText = "Need Commodity Signature" + Environment.NewLine;
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
            if (clsClient.clsHH.SurveyComplete && CCFBPrefs.EnableAdditionalHHMDataTab == true)
            {
                tbAlert.SelectionColor = Color.Black;
                tbAlert.SelectedText = "Survey Complete" + Environment.NewLine;
            }
            tbAlert.SelectionColor = Color.Green;
            tbAlert.SelectedText = sCSFPList;
        }

        /// <summary>
        /// Fills the form with the data from the client in database
        /// </summary>
        public void fillForm()
        {
            formClear = false;
            ethnicitySelectedFirstTime = false;
            loadingInfo = true;
            btnBeginEdit.Enabled = false;
            chksChanged.Clear();
            tbDaysSinceLstSrvc.Text = "";

            if (clsClient.clsHH.RowCount > 0)
            {
                btnBeginEdit.Enabled = true;
                tsbCreateAppt.Enabled = true;
                
                btnNext.Enabled = true;
                btnPrevious.Enabled = true;
                if (clsClient.clsHH.BarCode > 0)
                    btnAssignBarcode.BackColor = Color.MediumAquamarine;
                else
                    btnAssignBarcode.BackColor = Color.Khaki;

                mnuClient_BeginEditClient.Enabled = true;
                mnuClient_SaveHHChanges.Enabled = true;
                mnuClient_DeleteClient.Enabled = true;
                //menuHHMembers.Enabled = true;
                menuTrx.Enabled = true;
                btnEditTransLog.Enabled = true;

                tbTotalFam.Text = (clsClient.clsHH.Infants + clsClient.clsHH.Youth + clsClient.clsHH.Teens
                + clsClient.clsHH.Eighteens + clsClient.clsHH.Adults + clsClient.clsHH.Seniors).ToString();

                tbNumTransThisMonth.Text = clsClient.clsHHSvcTrans.getServicesThisMonthCount(
                    DateTime.Today.Month, DateTime.Today.Year).ToString();

                tbID.Text = clsClient.clsHH.GetDataValue(tbID.Tag.ToString()).ToString().Trim();
                foreach (TextBox tb in tbList)
                {
                    if (tb.Tag != null && tb.Tag.ToString().Trim() != "")
                    {
                        tb.Text = clsClient.clsHH.GetDataValue(tb.Tag.ToString()).ToString().Trim();
                    }
                }
                foreach (TextBox tb in tbmList)
                {
                    if (tb.Tag != null && tb.Tag.ToString().Trim() != "")
                    {
                        tb.Text = clsClient.clsHH.GetDataValue(tb.Tag.ToString()).ToString().Trim();
                    }
                }
                //tbLastService.Text = CCFBGlobal.ValidDateString(clsClient.clsHH.LatestService);
                //tbLastComodity.Text = CCFBGlobal.ValidDateString(clsClient.clsHH.LastCommodityService);

                formatDate(ref tbLastService);
                formatDate(ref tbLastComodity);
                formatDate(ref tbeFirstService);
                formatDate(ref tbeDateIDVerified);

                foreach (ComboBox cb in cboList)
                {
                    int newval = Int32.Parse(clsClient.clsHH.GetDataValue(cb.Tag.ToString()).ToString());
                    cb.SelectedValue = newval.ToString();
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

                //Sets number of days since last service
                DateTime dateTest = clsClient.clsHH.LatestService;
                if (dateTest != null && dateTest.Year > 1990)
                {
                    TimeSpan diff = DateTime.Today - clsClient.clsHH.LatestService;
                    tbDaysSinceLstSrvc.Text = diff.Days.ToString();
                }
                SetIncomeGroups(clsClient.clsHH.AnnualIncome, clsClient.clsHH.TotalFamily);

                loadHHMems(true);
                getClientLogForPeriod();
                if (clsClient.clsHH.UseFamilyList == true)
                    tabFamily.SelectTab(0);
                else
                    tabFamily.SelectTab(1);
                btnBeginEdit.Enabled = true;
                loadingInfo = false;
                tsbNewService.Enabled = (clsClient.clsHH.Inactive == false);
            }
            else
            {
                tsbNewService.Enabled = false;
                btnNext.Enabled = false;
                btnPrevious.Enabled = false;
                btnBeginEdit.Enabled = false;
                mnuClient_BeginEditClient.Enabled = false;
                mnuClient_SaveHHChanges.Enabled = false;
                mnuClient_DeleteClient.Enabled = false;
                //menuHHMembers.Enabled = true;
                menuTrx.Enabled = false;
                btnEditTransLog.Enabled = false;
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

        /// <summary>
        /// Loads the Household Members Listview with the Household Members
        /// </summary>
        private void loadHHMems(bool clearRows)
        {
            Color CellForeColor;
            DataGridViewRow dvr;
            bool oriloadingInfo = loadingInfo;
            loadingInfo = true;
            if (clearRows == true)
            {
                dataGridMembers.Rows.Clear();
            }

            int rowCount = 0;
            int NbrCSFP = 0;
            sCSFPList = "";
            for (int i = 0; i < clsClient.clsHHmem.RowCount; i++)
            {
                dataGridMembers.Rows.Add();
                if ((bool)clsClient.clsHHmem.DSet.Tables[0].Rows[i]["Inactive"])
                { CellForeColor = Color.Maroon; }
                else
                { CellForeColor = Color.Black; }
                dvr = dataGridMembers.Rows[rowCount];
                FillGridMembersCell(dvr, "clmInactive", "Inactive", true, CellForeColor, i);
                //                    FillGridMembersCell(dvr, "clmHeadHH", "HeadHH", true, CellForeColor, i);
                FillGridMembersCell(dvr, "clmLastName", "LastName", false, CellForeColor, i);
                FillGridMembersCell(dvr, "clmFirstName", "FirstName", false, CellForeColor, i);
                if ((bool)clsClient.clsHHmem.DSet.Tables[0].Rows[i]["HeadHH"] == true)
                    dvr.Cells["clmFirstName"].Value += " (HH)";
                FillGridMembersCell(dvr, "clmAge", "Age", false, CellForeColor, i);
                FillGridMembersCell(dvr, "clmSex", "Sex", false, CellForeColor, i);
                FillGridMembersCell(dvr, "clmDiet", "SpecialDiet", true, CellForeColor, i);
                FillGridMembersCell(dvr, "Disabled", "IsDisabled", true, CellForeColor, i);
                FillGridMembersCell(dvr, "clmCSFP", "CSFP", true, CellForeColor, i);
                FillGridMembersCell(dvr, "clmHMID", "ID", false, CellForeColor, i);

                if (clsClient.clsHHmem.DSet.Tables[0].Rows[i]["Birthdate"].ToString().Trim() != "")
                {
                    DateTime d = (DateTime)clsClient.clsHHmem.DSet.Tables[0].Rows[i]["Birthdate"];
                    dvr.Cells["clmBirthdate"].Value = d.ToShortDateString();
                    dvr.Cells["clmBirthdate"].Tag = "BirthDate";
                }

                //Uses the Age to Add the proper AgeGroup to the listview
                int agegroup = Int32.Parse(clsClient.clsHHmem.DSet.Tables[0].Rows[i]["AgeGroup"].ToString());
                if (agegroup >= 0 && agegroup <= 5)
                    dvr.Cells["clmGroup"].Value = AgeGroupName(agegroup);

                if ((bool)clsClient.clsHHmem.DSet.Tables[0].Rows[i]["CSFP"] && CCFBPrefs.EnableCSFP == true)
                {
                    NbrCSFP++;
                    sCSFPList += "CSFP for " + clsClient.clsHHmem.DSet.Tables[0].Rows[i]["FirstName"].ToString();
                    if (clsClient.clsHHmem.DSet.Tables[0].Rows[i]["CSFPExpiration"] != null
                        && clsClient.clsHHmem.DSet.Tables[0].Rows[i]["CSFPExpiration"].ToString() != "")
                    {
                        sCSFPList += " expires: " + Convert.ToDateTime(
                        clsClient.clsHHmem.DSet.Tables[0].Rows[i]["CSFPExpiration"]).ToShortDateString();
                    }
                    sCSFPList += "\r\n";
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
            CCFBGlobal.InitCombo(cboIDType, CCFBGlobal.parmTbl_IdVerify);
            CCFBGlobal.InitCombo(cboClientType, CCFBGlobal.parmTbl_Client);
        }

        /// <summary>
        /// Opens the TrxLog for the Client, uses the SQLDataReader 
        /// to read the rows and Loads them into ListView
        /// </summary>
        public void loadTransLog(string whereClause)
        {
            lvHHLog.Items.Clear();
            btnEditTransLog.Tag = "";
            ListViewItem lvi2 = null;
            SqlDataReader reader = null;
            try
            {
                //The order of the select statements directly corresponds to what shows on the ListView
                command = new SqlCommand("Select Convert(varchar(10),t.trxDate,101), CASE t.trxStatus WHEN 0 THEN CASE WHEN hhm.id is null THEN '....' "
                    + "else RTRIM(hhm.FirstName + ' ' + hhm.LastName) end WHEN 1 THEN 'Appointment' WHEN 2 THEN 'No Show' else '---' end HHMemName"
                    + ", t.FoodSvcList, t.NonFoodSvcList, "
                    + "t.LbsStd, t.LbsOther, t.LbsCommodity, t.LbsSupplemental, t.Infants, t.Youth, t.Eighteen, t.teens, t.Adults, t.Seniors, "
                    + "t.TotalFamily, t.Notes, t.HouseholdID, t.trxID, t.trxStatus From TrxLog t Left Join HouseholdMembers hhm on hhm.id = t.HHMemID "
                    + "Where t.HouseholdID=" + clsClient.clsHH.ID.ToString()
                    + " " + whereClause, conn);
                openConnection();
                reader = command.ExecuteReader();

                int count = 0;
                btnEditTransLog.Enabled = reader.HasRows;

                while (reader.Read())
                {
                    lvi2 = new ListViewItem(count.ToString());
                    for (int i = 0; i < reader.FieldCount - 1; i++)
                    {
                        lvi2.SubItems.Add(reader.GetValue(i).ToString());
                    }
                    switch (Convert.ToInt32(reader.GetValue(reader.FieldCount - 1)))
                    {
                        case CCFBGlobal.statusTrxLog_Service:
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
                    lvHHLog.Items.Add(lvi2);
                    count++;
                }
                closeConnection();
            }
            catch (SqlException ex)
            {
                closeConnection();
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString(), CCFBGlobal.serverName);
            }
            catch (Exception ex)
            {
                closeConnection();
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString());
            }

            if (lvHHLog.Items.Count > 0)
            {
                lvHHLog.Items[0].Selected = true;
                btnEditTransLog.Tag = lvHHLog.Items[0].SubItems[17].Text;
            }
        }

        /// <summary>  Loads User Check Box Labels from UserFields Table
        /// </summary>
        private void loadUserFieldLabels()
        {
            bool fieldsUsed = false;
            clsUserFields.open("Household");
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
                            tabCntrlMain.TabPages.Insert(1, tpUserFields);
                        else
                            tabCntrlMain.TabPages.Insert(0, tpUserFields);
                    }
                    else
                        tabCntrlMain.TabPages.Add(tpUserFields);
                    tpUserFields = null;
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

        private void lvHHLog_DoubleClick(object sender, EventArgs e)
        {
            if (lvHHLog.SelectedItems != null)
            {
                openEditServiceTrx(Convert.ToInt32(lvHHLog.SelectedItems[0].SubItems[18].Text));
            }
        }

        private void lvHHLog_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvHHLog.FocusedItem != null)
                btnEditTransLog.Tag = int.Parse(lvHHLog.FocusedItem.Text);
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
                clsClient.clsHHmem.update();
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
            MonthlyReportPrefrencesForm frmEmailRecipts = new MonthlyReportPrefrencesForm();
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
                CCFBGlobal.clsDailyItems.Refresh();
        }

        private void mnuAdmin_UserDefinedFields_Click(object sender, EventArgs e)
        {
            EditUserFields frmEditUserFields = new EditUserFields(CCFBGlobal.connectionString);
            frmEditUserFields.ShowDialog();
            loadUserFieldLabels();
        }

        private void mnuAdmin_YearlyCalendarForm_Click(object sender, EventArgs e)
        {
            YearlyForm frmYearlyCalendar = new YearlyForm();
            frmYearlyCalendar.ShowDialog();
        }

        private void mnuClient_AddHH_Click(object sender, EventArgs e)
        {
            addHousehold();
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

        private void mnuClient_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mnuClient_FindClient_Click(object sender, EventArgs e)
        {
            frmFindClient.Visible = true;
        }

        private void mnuClient_LogOut_Click(object sender, EventArgs e)
        {
            logout = true;
            frmLogIn.resetForm();
            this.Close();
        }

        private void mnuClient_PrintForm_Click(object sender, EventArgs e)
        {
            CaptureAndPrintForm();
        }

        private void mnuClient_SaveHHChanges_Click(object sender, EventArgs e)
        {
            saveClientRecord();
        }

        private void mnuHelp_About_Click(object sender, EventArgs e)
        {

        }

        private void mnuHHMember_Edit_Click(object sender, EventArgs e)
        {
            editHHMem();
        }

        private void mnuReports_AccessRpts_Click(object sender, EventArgs e)
        {
            AccessReportsForm frmReports = new AccessReportsForm();
            frmReports.ShowDialog();
        }

        private void mnuReports_MonthlyForm_Click(object sender, EventArgs e)
        {
            MonthEndReportsForm frmMonthlyReports = new MonthEndReportsForm(this);
            frmMonthlyReports.ShowDialog();
        }

        private void mnuTools_DonationsForm_Click(object sender, EventArgs e)
        {
            ShowDonationsForm();
        }

        private void mnuTools_DonorsForm_Click(object sender, EventArgs e)
        {
            EditDonorForm frmEditDonor = new EditDonorForm(CCFBGlobal.connectionString);
            frmEditDonor.ShowDialog();
        }

        private void mnuTools_VolHoursForm_Click(object sender, EventArgs e)
        {
            VolunteerHoursForm frmVolHrs = new VolunteerHoursForm();
            frmVolHrs.ShowDialog();
        }

        private void mnuTools_VolunteersForm_Click(object sender, EventArgs e)
        {
            EditVolunteerForm frmEditVols = new EditVolunteerForm(CCFBGlobal.connectionString);
            frmEditVols.ShowDialog();
        }

        private void mnuTrx_Delete_Click(object sender, EventArgs e)
        {
            if (lvHHLog.FocusedItem != null)
            {
                if (clsClient.clsHHSvcTrans.PromptDelete(lvHHLog.FocusedItem.SubItems[18].Text))
                {
                    clsClient.clsHHSvcTrans.openForHH(clsClient.clsHH.ID);
                    getClientLogForPeriod();
                    setFirstService();
                }
            }
            else
            {
                MessageBox.Show("Please Select A Transaction And Try Again");
            }
        }

        private void mnuTrx_Edit_Click(object sender, EventArgs e)
        {
            if (lvHHLog.FocusedItem != null)
            {
                openEditServiceTrx(Convert.ToInt32(lvHHLog.FocusedItem.SubItems[16].Text));
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
                frmEditServices.initForm(clsClient, false, false, trxLogId, clsClient.clsHHSvcTrans.HHMemID);
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
            e.Graphics.DrawImage(photo, new Rectangle(100, 100, 700, 600));
        }

        private void saveClientRecord()
        {
            clsClient.clsHH.update();
            fillAutoAlert();
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
        }

        /// <summary>
        /// Converts the Textbox's mode based on inEditMode
        /// </summary>
        private void setEditStateForControls(Color baseBackColor)
        {
            bool baseReadOnly = !inEditMode;
            bool baseTabStop = inEditMode;
            tbID.ReadOnly = false;
            tbID.TabStop = !inEditMode;

            foreach (TabPage tp in tabCntrlMain.Controls.OfType<TabPage>())
            {
                tp.BackColor = baseBackColor;
            }

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
            setFamilyStatsMode();
        }

        /// <summary>
        /// Turns off or on the different controls for how they were
        /// set in the prefrences
        /// </summary>
        private void SetEnvironmentFromPrefs()
        {
            if (CCFBPrefs.EnableClientPhone == false
             && CCFBPrefs.EnableVerifyId == false)
            {
                if (CCFBPrefs.EnableHHUserDefinedFields == false
                 && CCFBPrefs.EnableHouseholdIncome == false)
                {
                    tabCntrlMain.Visible = false;
                    splitMemTrans.Panel1Collapsed = true;
                }
                else
                {
                    splitMemTrans.Panel1Collapsed = false;
                    tabCntrlMain.TabPages.RemoveByKey("tpHHData");  //Remove HHData Tab
                    if (CCFBPrefs.EnableHHUserDefinedFields == false && CCFBPrefs.EnableHouseholdIncome == false)
                        tabCntrlMain.TabPages.RemoveByKey("tpUserFields");  //Remove UserFields Tab
                    else
                    {
                        grpbxIncome.Visible = CCFBPrefs.EnableHouseholdIncome;
                        chkLstBxUserFields.Visible = CCFBPrefs.EnableHHUserDefinedFields;
                    }
                }
            }
            else
            {
                //Enable Phone Number
                tbPhone.Visible = CCFBPrefs.EnableClientPhone;
                cboPhoneType.Visible = CCFBPrefs.EnableClientPhone;
                lblPhoneNum.Visible = CCFBPrefs.EnableClientPhone;
                lblPhoneType.Visible = CCFBPrefs.EnableClientPhone;
                //Enable Verify ID
                grpbxVerifyId.Visible = CCFBPrefs.EnableVerifyId;
                splitMemTrans.Panel1Collapsed = false;
                if (CCFBPrefs.EnableHHUserDefinedFields == false && CCFBPrefs.EnableHouseholdIncome == false)
                    tabCntrlMain.TabPages.RemoveByKey("tpUserFields");  //Remove UserFields Tab
                else
                {
                    //Enable Income
                    grpbxIncome.Visible = CCFBPrefs.EnableHouseholdIncome;
                    chkLstBxUserFields.Visible = CCFBPrefs.EnableHHUserDefinedFields;
                }
            }
            //Enable Vouchers
            tsbVouchers.Visible = CCFBPrefs.EnableVouchers;
            maintainVoucherItemsToolStripMenuItem.Visible = CCFBPrefs.EnableVouchers;
            //EnablePrint Clientcard
            tbsPrintClientcard.Visible = CCFBPrefs.EnablePrintClientCard;
            mnuClient_PrintClientCard.Visible = CCFBPrefs.EnablePrintClientCard;
            //Enable Cash Donations
            mnuTools_CashDonations.Visible = CCFBPrefs.EnableCashDonations;
            //Enable Food Donations
            tsbFoodDonations.Visible = CCFBPrefs.EnableFoodDonations;
            mnuTools_DonationsForm.Visible = CCFBPrefs.EnableFoodDonations;
            //Enable Appointments
            tsbCreateAppt.Visible = CCFBPrefs.EnableAppointments;
            tsbDfltApptDate.Visible = CCFBPrefs.EnableAppointments;
            mnuTrx_NewAppointment.Visible = CCFBPrefs.EnableAppointments;
            //Enable Food Services
            tsbNewService.Visible = CCFBPrefs.EnableFoodServices;
            tsbDfltSvcDate.Visible = CCFBPrefs.EnableFoodServices;
            mnuTrx_NewServiceTrx.Visible = CCFBPrefs.EnableFoodServices;
            //Enable TEFAP
            chkNoCommodities.Visible = CCFBPrefs.EnableTEFAP;
            lblNoCommodities.Visible = CCFBPrefs.EnableTEFAP;
            chkNeedCommSig.Visible = CCFBPrefs.EnableTEFAP;
            lblNeedCommodSignature.Visible = CCFBPrefs.EnableTEFAP;
            lblLastComm.Visible = CCFBPrefs.EnableTEFAP;
            tbLastComodity.Visible = CCFBPrefs.EnableTEFAP;
            if (CCFBPrefs.EnableTEFAP == false)
                lvHHLog.Columns[10].Width = 0;
            else
                lvHHLog.Columns[10].Width = 40;
            //Enable Supplemental
            chkSupplOnly.Visible = CCFBPrefs.EnableSupplemental;
            if (CCFBPrefs.EnableSupplemental == false)
                lvHHLog.Columns[11].Width = 0;
            else
                lvHHLog.Columns[11].Width = 40;
            //Enable Baby Services
            chkBabyServices.Visible = CCFBPrefs.EnableBabyServices;
            tbBabySvcDescr.Visible = CCFBPrefs.EnableBabyServices;
            lblBabyServices.Visible = CCFBPrefs.EnableBabyServices;
            //Enable CSFP
            lblCSFP.Visible = CCFBPrefs.EnableCSFP;
            tbmCSFP.Visible = CCFBPrefs.EnableCSFP;
            dataGridMembers.Columns["clmCSFP"].Visible = CCFBPrefs.EnableCSFP;
            tsbCSFP.Visible = CCFBPrefs.EnableCSFP;
            mnuTrx_CSFPServices.Visible = CCFBPrefs.EnableCSFP;
            //Use Family List
            chkUseFamList.Visible = CCFBPrefs.UseFamilyList != CCFBPrefs.UseFamilyListCode.Never;
            mnuTools_VolHoursForm.Visible = CCFBPrefs.EnableVolunteerHours;
            btnAssignBarcode.Visible = CCFBPrefs.EnableBarcodePrompts;
            if (frmTrxLog != null)
                frmTrxLog.PrefsChanged();
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

        private void setFirstService()
        {
            DateTime trxDate = DateTime.MaxValue;
            for (int i = 0; i < clsClient.clsHHSvcTrans.RowCount; i++)
            {
                clsClient.clsHHSvcTrans.setDataRow(i);
                if (trxDate > clsClient.clsHHSvcTrans.TrxDate)
                    trxDate = clsClient.clsHHSvcTrans.TrxDate;
            }

            if (clsClient.clsHH.FirstService != trxDate)
            {
                clsClient.clsHH.FirstService = trxDate;
                tbeFirstService.Text = trxDate.ToShortDateString();
                clsClient.clsHH.update();
            }
        }

        /// <summary>
        /// Opens the selected household and re-fills the form
        /// </summary>
        /// <param name="newHhID">HouseholdID</param>
        public void setHousehold(int newHhID, int memberId)
        {
            clearForm();
            clsClient.open(newHhID, true, true);
            if (memberId > 0)
                clsClient.ServingHHMemID = memberId;
            else
                clsClient.ServingHHMemID = clsClient.clsHHmem.getHeadHH(newHhID);

            fillForm();
        }

        private void SetIncomeGroups(int income, int familySize)
        {
            int i = 0;
            foreach (IncomeGroupMatrix item in incomeGroups)
            {
                lvIncomeGroups.Items[i].SubItems[2].Text = item.GetIncomeCategory(income, familySize);
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
            frmFoodDonation.ShowDialog();
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
        }

        private void splCntrCardMembers_SplitterMoved(object sender, SplitterEventArgs e)
        {
            tabFamily.Height = splCntrCardMembers.SplitterDistance - tabFamily.Top - 5;
            tbeNotes.Height = splCntrCardMembers.SplitterDistance - tbeNotes.Top - 5; 
        }

        private void splitContainer2_Panel1_SizeChanged(object sender, EventArgs e)
        {
            if (splitContainer2.Panel1Collapsed == false)
            {
                dataGridMembers.Width = splitContainer2.Panel1.ClientSize.Width;
                dataGridMembers.Height = splitContainer2.Panel1.ClientSize.Height - dataGridMembers.Top;
            }
        }

        private void splitContainer2_Panel2_SizeChanged(object sender, EventArgs e)
        {
            if (splitContainer2.Panel2Collapsed == false)
            {
                lvHHLog.Top = 29;
                lvHHLog.Width = splitContainer2.Panel2.ClientSize.Width;
                lvHHLog.Height = splitContainer2.Panel2.Height - lvHHLog.Top;
            }
        }

        private void tbeCity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                clsZipcodes.open(tbeCity.Text, tbeCity.Tag.ToString());

                if (clsZipcodes.RowCount > 1)
                {
                    ZipcodeSelectForm frmZipSelect = new ZipcodeSelectForm(clsZipcodes, "City: " + tbeCity.Text);
                    frmZipSelect.ShowDialog();
                    if (frmZipSelect.Canceled == false)
                    {
                        clsClient.clsHH.Zipcode = tbeZipCode.Text = clsZipcodes.ZipCode;
                        clsClient.clsHH.City = tbeCity.Text = clsZipcodes.City.ToUpper();
                        clsClient.clsHH.State = tbeState.Text = clsZipcodes.State.ToUpper();
                    }
                }
            }
        }

        private void tbeName_KeyDown(object sender, KeyEventArgs e)
        {
            if (inEditMode == true)
            {
                e.SuppressKeyPress = true;
                DuplicateHouseholdNameForm frmDupHHName =
                    new DuplicateHouseholdNameForm(clsClient, tbeName.Text);
                frmDupHHName.ShowDialog();

                if (frmDupHHName.Canceled == false)
                {
                    tbeName.Text = frmDupHHName.HHName;
                }
                else
                {
                    tbeName.Text = clsClient.clsHH.Name;
                }
            }
        }

        private void tbeZipCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (inEditMode == true && e.KeyCode == Keys.Enter)
            {
                clsZipcodes.open(tbeZipCode.Text, tbeZipCode.Tag.ToString());
                if (clsZipcodes.IsValid == true)
                {
                    clsClient.clsHH.City = tbeCity.Text = clsZipcodes.City.ToUpper();
                    clsClient.clsHH.State = tbeState.Text = clsZipcodes.State.ToUpper();
                }
            }
        }

        private void tbID_KeyDown(object sender, KeyEventArgs e)
        {
            if (formClear == false)
            {
                clearForm();
                //tbID.Text = e.KeyValue.ToString();
            }
            if (e.KeyCode == Keys.Enter && tbID.ReadOnly == false)
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
            TextBox tbHH = (TextBox)sender; //Get the correct textbox

            if (inEditMode == true)
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
                        tbTotalFam.Text = (clsClient.clsHH.Infants + clsClient.clsHH.Youth + clsClient.clsHH.Adults
                            + clsClient.clsHH.Seniors + clsClient.clsHH.Teens).ToString();
                        clsClient.clsHH.SetDataValue(tbTotalFam.Tag.ToString(), tbTotalFam.Text.Trim());
                        SetIncomeGroups(clsClient.clsHH.AnnualIncome, clsClient.clsHH.TotalFamily);
                    }
                    else if (tbHH.Tag.ToString() == "AnnualIncome")
                    {
                        SetIncomeGroups(clsClient.clsHH.AnnualIncome, clsClient.clsHH.TotalFamily);
                    }
                }
            }
            else if(formClear == false)
            {
                //If not in edit mode, reset the text with value from class
                if (tbHH.Tag.ToString() != "" && tbHH.Tag.ToString() != "ID")
                    tbHH.Text = clsClient.clsHH.GetDataValue(tbHH.Tag.ToString()).ToString();

                formatDate(ref tbLastService);
                formatDate(ref tbLastComodity);
                formatDate(ref tbeFirstService);
                formatDate(ref tbeDateIDVerified);
            }
        }

        private void tbsPrintClientcard_Click(object sender, EventArgs e)
        {
            CreateClientCard clsCreateClientCard = new CreateClientCard(clsClient);
            parmType pt = (parmType)cboSpecialLang.SelectedItem;

            string templatePath = @"C:\ClientcardFB3\Templates\Clientcard" + pt.ShortName + ".doc";
            string fbName = CCFBPrefs.FoodBankName;

            int index = 0;
            string[] fldNames = new string[4];
            string[] fldVals = new string[4];

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

            if(File.Exists(templatePath))
            clsCreateClientCard.createReport(fbName, templatePath,
                cboClientType.SelectedItem.ToString(), fldNames, fldVals);
            else if (File.Exists(@"C:\ClientcardFB3\Templates\ClientcardENG.doc"))
                clsCreateClientCard.createReport(fbName, @"C:\ClientcardFB3\Templates\ClientcardENG.doc",
                cboClientType.SelectedItem.ToString(), fldNames, fldVals);
            else
                MessageBox.Show("ERROR: " + templatePath + " Not Found", "Temlate Not Found", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Traverses all controls on the page using recursion and adds the proper ones
        /// to their proper collections and adds LostFocus event to Textboxes and Checkboxes
        /// </summary>
        /// <param name="controlList"></param>
        private void traverseAndAddControlsToCollections(Control.ControlCollection controlList)
        {
            foreach (Control controls in controlList.OfType<Control>())
            {
                switch (controls.GetType().Name)
                {
                    case "TextBox":
                        {
                            if (controls.Tag != null && controls.Tag.ToString().Trim() != "TotalFamily" &&
                            controls.Tag.ToString() != "")
                            {
                                tbList.Add((TextBox)controls);
                                controls.LostFocus += new System.EventHandler(this.tbList_LostFocus);
                            }
                            break;
                        }
                    case "CheckBox":
                        {
                            CheckBox chk = (CheckBox)controls;
                            chk.CheckedChanged += new System.EventHandler(this.chkList_CheckedChanged);
                            chk.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkList_KeyDown);
                            chkList.Add(chk);
                            break;
                        }
                    case "ComboBox":
                        {
                            if (controls.Tag != null)
                            {
                                if (controls.Tag.ToString().Trim() != "")
                                {
                                    cboList.Add((ComboBox)controls);
                                }
                            }
                            break;
                        }
                }

                traverseAndAddControlsToCollections(controls.Controls);
            }
        }

        private void tsbAddClient_Click(object sender, EventArgs e)
        {
            addHousehold();
        }

        private void tsbCreateAppt_Click(object sender, EventArgs e)
        {
            CreateNewAppointment();
        }

        private void tsbCSFP_Click(object sender, EventArgs e)
        {
            EditCSFPForm frmEditCSFP = new EditCSFPForm();
            frmEditCSFP.ShowDialog();
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
            FoodDonationsForm frmFoodDonation = new FoodDonationsForm();
            frmFoodDonation.ShowDialog();
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
            VoucherForm frmVouchers = new VoucherForm(clsClient);
            frmVouchers.ShowDialog();
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
            frmBarCodeEntry.INIT("Assign Barcode Form", clsClient.clsHH.Name);
            DialogResult dr = frmBarCodeEntry.ShowDialog();
            if (dr == DialogResult.OK)
            {
                clsClient.clsHH.BarCode = frmBarCodeEntry.BarCode;
                clsClient.clsHH.update();
                btnAssignBarcode.BackColor = Color.MediumAquamarine;
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (CCFBPrefs.EnableBarcodePrompts == true)
            {
                if (e.KeyCode == Keys.F2)
                {
                    frmBarCodeEntry.INIT("Barcode Reader Form", "Enter barcode for household");
                    DialogResult dr = frmBarCodeEntry.ShowDialog(this);
                    if (dr == DialogResult.OK)
                    {
                        int testBarCode = frmBarCodeEntry.BarCode;
                        int testID = CCFBGlobal.getClientFromBarCode(testBarCode.ToString());
                        if (testID > 0)
                            setHousehold(testID, 0);
                        else
                        {
                            clearForm();
                            tbAlert.Text = "No Client found where Barcode = " + testBarCode.ToString();
                            SoundPlayer sp = new SoundPlayer(@"C:\Windows\Media\notify.wav");
                            sp.Play();
                        }
                    }
                }
            }
        }

        private void maintainVoucherItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditVouchersItemForm frmEditVoucherItems = new EditVouchersItemForm();
            frmEditVoucherItems.ShowDialog();
        }

        private void mnuHelp_Index_Click(object sender, EventArgs e)
        {
            HHMemGridForm frmHHM = new HHMemGridForm(clsClient, this, 0);
            frmHHM.ShowDialog();
        }

        private void mnuTools_CashDonations_Click(object sender, EventArgs e)
        {
            CashDonationsForm frmCashDonations = new CashDonationsForm();
            frmCashDonations.ShowDialog();
        }
    }
}
