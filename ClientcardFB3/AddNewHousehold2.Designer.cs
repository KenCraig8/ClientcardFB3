namespace ClientcardFB3
{
    partial class AddNewHousehold2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                if (clsHH != null)
                {
                    clsHH.Dispose();
                }
                if (clsHHM != null)
                {
                    clsHHM.Dispose();
                }
                if (clsZipcodes != null)
                {
                    clsZipcodes.Dispose();
                }
                if (csdgdataaccess != null)
                {
                    csdgdataaccess.Dispose();
                }
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddNewHousehold2));
            this.lblFName = new System.Windows.Forms.Label();
            this.lblLName = new System.Windows.Forms.Label();
            this.tbFirstName = new System.Windows.Forms.TextBox();
            this.tbLastName = new System.Windows.Forms.TextBox();
            this.lblBDay = new System.Windows.Forms.Label();
            this.lblSex = new System.Windows.Forms.Label();
            this.tbSex = new System.Windows.Forms.TextBox();
            this.btnSaveClient = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbAge = new System.Windows.Forms.TextBox();
            this.chkEnterAge = new System.Windows.Forms.CheckBox();
            this.cboSpecialLang = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tbeState = new System.Windows.Forms.TextBox();
            this.chkInCityLimits = new System.Windows.Forms.CheckBox();
            this.chkHomeless = new System.Windows.Forms.CheckBox();
            this.tbeZipCode = new System.Windows.Forms.TextBox();
            this.cboClientType = new System.Windows.Forms.ComboBox();
            this.tbeCity = new System.Windows.Forms.TextBox();
            this.tbeAddress = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lvwHHMByBirthdate = new System.Windows.Forms.ListView();
            this.clmLastName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmFirstName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmHHMInactive = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmHHName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmAddress = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmCity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmZip = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmHHId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmHHInactive = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pnl = new System.Windows.Forms.Panel();
            this.lblDupHHMError = new System.Windows.Forms.Label();
            this.chkNoCommodities = new System.Windows.Forms.CheckBox();
            this.chkSupplOnly = new System.Windows.Forms.CheckBox();
            this.cboIDType = new System.Windows.Forms.ComboBox();
            this.lblVerifyMethod = new System.Windows.Forms.Label();
            this.tbBabySvcDescr = new System.Windows.Forms.TextBox();
            this.chkBabyServices = new System.Windows.Forms.CheckBox();
            this.chkSingleHeadHH = new System.Windows.Forms.CheckBox();
            this.tbeApt = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lblBizName = new System.Windows.Forms.Label();
            this.tbPhone = new System.Windows.Forms.MaskedTextBox();
            this.cboPhoneType = new System.Windows.Forms.ComboBox();
            this.lblPhoneNum = new System.Windows.Forms.Label();
            this.spltcontMain = new System.Windows.Forms.SplitContainer();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lblDupHHError = new System.Windows.Forms.Label();
            this.lblHeahh = new System.Windows.Forms.Label();
            this.chkHispanic = new System.Windows.Forms.CheckBox();
            this.cboRace = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.chkDissabled = new System.Windows.Forms.CheckBox();
            this.chkSpecialDiet = new System.Windows.Forms.CheckBox();
            this.tbBirthDate = new System.Windows.Forms.MaskedTextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbLastName = new System.Windows.Forms.ToolStripButton();
            this.tsbPeople = new System.Windows.Forms.ToolStripButton();
            this.tsbBirthDate = new System.Windows.Forms.ToolStripButton();
            this.tsbHouseNbr = new System.Windows.Forms.ToolStripButton();
            this.lblDateError = new System.Windows.Forms.Label();
            this.cboMemIDType = new System.Windows.Forms.ComboBox();
            this.lblMemIdType = new System.Windows.Forms.Label();
            this.lblMemIdNbr = new System.Windows.Forms.Label();
            this.tbMemIdNbr = new System.Windows.Forms.TextBox();
            this.lvwSameHouseNbr = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colAddress = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colAptNbr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwPeople = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader15 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwLastName = new System.Windows.Forms.ListView();
            this.clmLNLastName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmLNFirstName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmLNBirthDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmLNHHName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmLNNbrSvcs = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmLNHHInactive = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmLNAddress = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmLNCity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmLNZip = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmLNHHID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblHelp = new System.Windows.Forms.Label();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.pnl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spltcontMain)).BeginInit();
            this.spltcontMain.Panel1.SuspendLayout();
            this.spltcontMain.Panel2.SuspendLayout();
            this.spltcontMain.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblFName
            // 
            this.lblFName.Location = new System.Drawing.Point(1, 56);
            this.lblFName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFName.Name = "lblFName";
            this.lblFName.Size = new System.Drawing.Size(80, 16);
            this.lblFName.TabIndex = 2;
            this.lblFName.Text = "First Name:";
            this.lblFName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblLName
            // 
            this.lblLName.Location = new System.Drawing.Point(1, 30);
            this.lblLName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLName.Name = "lblLName";
            this.lblLName.Size = new System.Drawing.Size(80, 16);
            this.lblLName.TabIndex = 0;
            this.lblLName.Text = "Last Name:";
            this.lblLName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbFirstName
            // 
            this.tbFirstName.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.helpProvider1.SetHelpString(this.tbFirstName, "Please type in the head of household first name");
            this.tbFirstName.Location = new System.Drawing.Point(88, 56);
            this.tbFirstName.Name = "tbFirstName";
            this.helpProvider1.SetShowHelp(this.tbFirstName, true);
            this.tbFirstName.Size = new System.Drawing.Size(315, 23);
            this.tbFirstName.TabIndex = 3;
            this.tbFirstName.Tag = "FirstName";
            this.tbFirstName.Text = "william";
            this.tbFirstName.Enter += new System.EventHandler(this.tbCtl_Enter);
            this.tbFirstName.Leave += new System.EventHandler(this.tbFirstName_Leave);
            // 
            // tbLastName
            // 
            this.tbLastName.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.helpProvider1.SetHelpString(this.tbLastName, "Please type in the head of household last name");
            this.tbLastName.Location = new System.Drawing.Point(88, 27);
            this.tbLastName.Name = "tbLastName";
            this.helpProvider1.SetShowHelp(this.tbLastName, true);
            this.tbLastName.Size = new System.Drawing.Size(315, 23);
            this.tbLastName.TabIndex = 1;
            this.tbLastName.Tag = "LastName";
            this.tbLastName.Text = "123456789012345678901234567890";
            this.tbLastName.Enter += new System.EventHandler(this.tbCtl_Enter);
            this.tbLastName.Leave += new System.EventHandler(this.tbLastName_Leave);
            // 
            // lblBDay
            // 
            this.lblBDay.Location = new System.Drawing.Point(1, 137);
            this.lblBDay.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBDay.Name = "lblBDay";
            this.lblBDay.Size = new System.Drawing.Size(80, 16);
            this.lblBDay.TabIndex = 5;
            this.lblBDay.Tag = "Birthdate";
            this.lblBDay.Text = "Birth Date:";
            this.lblBDay.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSex
            // 
            this.lblSex.AutoSize = true;
            this.lblSex.Location = new System.Drawing.Point(312, 115);
            this.lblSex.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSex.Name = "lblSex";
            this.lblSex.Size = new System.Drawing.Size(56, 16);
            this.lblSex.TabIndex = 9;
            this.lblSex.Tag = "Sex";
            this.lblSex.Text = "&Gender:";
            // 
            // tbSex
            // 
            this.tbSex.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbSex.Location = new System.Drawing.Point(315, 134);
            this.tbSex.MaxLength = 1;
            this.tbSex.Name = "tbSex";
            this.tbSex.Size = new System.Drawing.Size(27, 26);
            this.tbSex.TabIndex = 10;
            this.tbSex.Tag = "Sex";
            this.tbSex.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbSex.Enter += new System.EventHandler(this.tbCtl_Enter);
            this.tbSex.Leave += new System.EventHandler(this.tbSex_Leave);
            // 
            // btnSaveClient
            // 
            this.btnSaveClient.Enabled = false;
            this.btnSaveClient.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveClient.Location = new System.Drawing.Point(16, 256);
            this.btnSaveClient.Name = "btnSaveClient";
            this.btnSaveClient.Size = new System.Drawing.Size(129, 28);
            this.btnSaveClient.TabIndex = 47;
            this.btnSaveClient.Text = "&Save Client Household";
            this.btnSaveClient.UseVisualStyleBackColor = true;
            this.btnSaveClient.Click += new System.EventHandler(this.btnSaveClient_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(168, 256);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(70, 28);
            this.btnCancel.TabIndex = 48;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.tbCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(229, 114);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 16);
            this.label1.TabIndex = 7;
            this.label1.Text = "Age:";
            // 
            // tbAge
            // 
            this.tbAge.Enabled = false;
            this.tbAge.Font = new System.Drawing.Font("Lucida Console", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbAge.Location = new System.Drawing.Point(232, 134);
            this.tbAge.Name = "tbAge";
            this.tbAge.Size = new System.Drawing.Size(42, 22);
            this.tbAge.TabIndex = 8;
            this.tbAge.Tag = "Age";
            this.tbAge.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbAge.Enter += new System.EventHandler(this.tbCtl_Enter);
            this.tbAge.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbAge_KeyDown);
            this.tbAge.Leave += new System.EventHandler(this.tbAge_Leave);
            // 
            // chkEnterAge
            // 
            this.chkEnterAge.AutoSize = true;
            this.helpProvider1.SetHelpString(this.chkEnterAge, "Check if you do not have the birth date - Press the Space Bar to toggle this valu" +
        "e");
            this.chkEnterAge.Location = new System.Drawing.Point(8, 114);
            this.chkEnterAge.Name = "chkEnterAge";
            this.helpProvider1.SetShowHelp(this.chkEnterAge, true);
            this.chkEnterAge.Size = new System.Drawing.Size(166, 20);
            this.chkEnterAge.TabIndex = 6;
            this.chkEnterAge.Tag = "UseAge";
            this.chkEnterAge.Text = "Enter Age Not Birthdate";
            this.chkEnterAge.UseVisualStyleBackColor = true;
            this.chkEnterAge.CheckedChanged += new System.EventHandler(this.chkEnterAge_CheckedChanged);
            this.chkEnterAge.Enter += new System.EventHandler(this.chkEnterAge_Enter);
            this.chkEnterAge.Leave += new System.EventHandler(this.chkBox_Leave);
            // 
            // cboSpecialLang
            // 
            this.cboSpecialLang.BackColor = System.Drawing.Color.White;
            this.cboSpecialLang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSpecialLang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboSpecialLang.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboSpecialLang.FormattingEnabled = true;
            this.helpProvider1.SetHelpString(this.cboSpecialLang, " - Alt down arrow will display the list");
            this.cboSpecialLang.Location = new System.Drawing.Point(112, 136);
            this.cboSpecialLang.Margin = new System.Windows.Forms.Padding(4);
            this.cboSpecialLang.Name = "cboSpecialLang";
            this.helpProvider1.SetShowHelp(this.cboSpecialLang, true);
            this.cboSpecialLang.Size = new System.Drawing.Size(166, 24);
            this.cboSpecialLang.TabIndex = 37;
            this.cboSpecialLang.Tag = "EthnicSpeaking";
            this.cboSpecialLang.Enter += new System.EventHandler(this.cboCtl_Enter);
            this.cboSpecialLang.Leave += new System.EventHandler(this.cboCtl_Leave);
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(7, 134);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(98, 22);
            this.label11.TabIndex = 36;
            this.label11.Text = "Form Language";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbeState
            // 
            this.tbeState.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbeState.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbeState.Location = new System.Drawing.Point(418, 76);
            this.tbeState.Margin = new System.Windows.Forms.Padding(4);
            this.tbeState.Name = "tbeState";
            this.tbeState.Size = new System.Drawing.Size(40, 23);
            this.tbeState.TabIndex = 33;
            this.tbeState.Tag = "State";
            this.tbeState.Text = "WW";
            this.tbeState.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbeState.Enter += new System.EventHandler(this.tbCtl_Enter);
            this.tbeState.Leave += new System.EventHandler(this.tbCtl_Leave);
            // 
            // chkInCityLimits
            // 
            this.chkInCityLimits.AutoSize = true;
            this.chkInCityLimits.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.helpProvider1.SetHelpString(this.chkInCityLimits, " - Toggle checkmark with the Space Bar");
            this.chkInCityLimits.Location = new System.Drawing.Point(350, 106);
            this.chkInCityLimits.Margin = new System.Windows.Forms.Padding(4);
            this.chkInCityLimits.Name = "chkInCityLimits";
            this.helpProvider1.SetShowHelp(this.chkInCityLimits, true);
            this.chkInCityLimits.Size = new System.Drawing.Size(110, 22);
            this.chkInCityLimits.TabIndex = 40;
            this.chkInCityLimits.Tag = "InCityLimits";
            this.chkInCityLimits.Text = "In City Limits";
            this.chkInCityLimits.UseVisualStyleBackColor = true;
            this.chkInCityLimits.Enter += new System.EventHandler(this.chkBox_Enter);
            this.chkInCityLimits.Leave += new System.EventHandler(this.chkBox_Leave);
            // 
            // chkHomeless
            // 
            this.chkHomeless.AutoSize = true;
            this.chkHomeless.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.helpProvider1.SetHelpString(this.chkHomeless, " - Toggle checkmark with the Space Bar");
            this.chkHomeless.Location = new System.Drawing.Point(350, 130);
            this.chkHomeless.Margin = new System.Windows.Forms.Padding(4);
            this.chkHomeless.Name = "chkHomeless";
            this.helpProvider1.SetShowHelp(this.chkHomeless, true);
            this.chkHomeless.Size = new System.Drawing.Size(95, 22);
            this.chkHomeless.TabIndex = 41;
            this.chkHomeless.Tag = "Homeless";
            this.chkHomeless.Text = "Homeless";
            this.chkHomeless.UseVisualStyleBackColor = true;
            this.chkHomeless.Enter += new System.EventHandler(this.chkBox_Enter);
            this.chkHomeless.Leave += new System.EventHandler(this.chkBox_Leave);
            // 
            // tbeZipCode
            // 
            this.tbeZipCode.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.helpProvider1.SetHelpString(this.tbeZipCode, "Press ENTER to lookup City and State");
            this.tbeZipCode.Location = new System.Drawing.Point(18, 76);
            this.tbeZipCode.Margin = new System.Windows.Forms.Padding(4);
            this.tbeZipCode.Name = "tbeZipCode";
            this.helpProvider1.SetShowHelp(this.tbeZipCode, true);
            this.tbeZipCode.Size = new System.Drawing.Size(64, 23);
            this.tbeZipCode.TabIndex = 29;
            this.tbeZipCode.Tag = "Zipcode";
            this.tbeZipCode.Text = "98072";
            this.tbeZipCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbeZipCode.Enter += new System.EventHandler(this.tbCtl_Enter);
            this.tbeZipCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbeZipCode_KeyDown);
            this.tbeZipCode.Leave += new System.EventHandler(this.tbeZipCode_Leave);
            // 
            // cboClientType
            // 
            this.cboClientType.BackColor = System.Drawing.Color.White;
            this.cboClientType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboClientType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboClientType.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboClientType.FormattingEnabled = true;
            this.helpProvider1.SetHelpString(this.cboClientType, " - Alt down arrow will display the list");
            this.cboClientType.ItemHeight = 16;
            this.cboClientType.Location = new System.Drawing.Point(112, 106);
            this.cboClientType.Margin = new System.Windows.Forms.Padding(4);
            this.cboClientType.Name = "cboClientType";
            this.helpProvider1.SetShowHelp(this.cboClientType, true);
            this.cboClientType.Size = new System.Drawing.Size(215, 24);
            this.cboClientType.TabIndex = 35;
            this.cboClientType.Tag = "ClientType";
            this.cboClientType.Enter += new System.EventHandler(this.cboCtl_Enter);
            this.cboClientType.Leave += new System.EventHandler(this.cboCtl_Leave);
            // 
            // tbeCity
            // 
            this.tbeCity.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbeCity.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.helpProvider1.SetHelpString(this.tbeCity, "Press ENTER to lookup Zip Code");
            this.tbeCity.Location = new System.Drawing.Point(112, 76);
            this.tbeCity.Margin = new System.Windows.Forms.Padding(4);
            this.tbeCity.Name = "tbeCity";
            this.helpProvider1.SetShowHelp(this.tbeCity, true);
            this.tbeCity.Size = new System.Drawing.Size(286, 23);
            this.tbeCity.TabIndex = 31;
            this.tbeCity.Tag = "City";
            this.tbeCity.Text = "123456789012345678901234567890";
            this.tbeCity.Enter += new System.EventHandler(this.tbCtl_Enter);
            this.tbeCity.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbeCity_KeyDown);
            this.tbeCity.Leave += new System.EventHandler(this.tbCtl_Leave);
            // 
            // tbeAddress
            // 
            this.tbeAddress.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbeAddress.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbeAddress.Location = new System.Drawing.Point(112, 3);
            this.tbeAddress.Margin = new System.Windows.Forms.Padding(4);
            this.tbeAddress.Name = "tbeAddress";
            this.tbeAddress.Size = new System.Drawing.Size(400, 23);
            this.tbeAddress.TabIndex = 25;
            this.tbeAddress.Tag = "Address";
            this.tbeAddress.Text = "12345678901234567890123456789012345678901234567890";
            this.tbeAddress.Enter += new System.EventHandler(this.tbCtl_Enter);
            this.tbeAddress.Leave += new System.EventHandler(this.tbeAddress_Leave);
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(6, 104);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(98, 22);
            this.label8.TabIndex = 34;
            this.label8.Text = "Category:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(15, 60);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 14);
            this.label6.TabIndex = 28;
            this.label6.Text = "Zip Code:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(415, 60);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(24, 13);
            this.label5.TabIndex = 32;
            this.label5.Text = "St:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(109, 60);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 30;
            this.label4.Text = "City:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 3);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 22);
            this.label3.TabIndex = 24;
            this.label3.Text = "Address:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lvwHHMByBirthdate
            // 
            this.lvwHHMByBirthdate.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lvwHHMByBirthdate.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmLastName,
            this.clmFirstName,
            this.clmHHMInactive,
            this.clmHHName,
            this.clmAddress,
            this.clmCity,
            this.clmZip,
            this.clmHHId,
            this.clmHHInactive});
            this.lvwHHMByBirthdate.FullRowSelect = true;
            this.lvwHHMByBirthdate.GridLines = true;
            this.lvwHHMByBirthdate.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvwHHMByBirthdate.Location = new System.Drawing.Point(3, 179);
            this.lvwHHMByBirthdate.Name = "lvwHHMByBirthdate";
            this.lvwHHMByBirthdate.Size = new System.Drawing.Size(400, 141);
            this.lvwHHMByBirthdate.TabIndex = 102;
            this.lvwHHMByBirthdate.TabStop = false;
            this.lvwHHMByBirthdate.UseCompatibleStateImageBehavior = false;
            this.lvwHHMByBirthdate.View = System.Windows.Forms.View.Details;
            this.lvwHHMByBirthdate.Visible = false;
            // 
            // clmLastName
            // 
            this.clmLastName.Text = "Last Name";
            this.clmLastName.Width = 150;
            // 
            // clmFirstName
            // 
            this.clmFirstName.Text = "First Name";
            this.clmFirstName.Width = 120;
            // 
            // clmHHMInactive
            // 
            this.clmHHMInactive.Text = "HHM Ina";
            this.clmHHMInactive.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clmHHMInactive.Width = 70;
            // 
            // clmHHName
            // 
            this.clmHHName.Text = "HH Name";
            this.clmHHName.Width = 150;
            // 
            // clmAddress
            // 
            this.clmAddress.Text = "Address";
            this.clmAddress.Width = 180;
            // 
            // clmCity
            // 
            this.clmCity.Text = "City";
            this.clmCity.Width = 90;
            // 
            // clmZip
            // 
            this.clmZip.Text = "Zip";
            this.clmZip.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // clmHHId
            // 
            this.clmHHId.Text = "HHID";
            this.clmHHId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clmHHId.Width = 80;
            // 
            // clmHHInactive
            // 
            this.clmHHInactive.Text = "HH Ina";
            this.clmHHInactive.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // pnl
            // 
            this.pnl.BackColor = System.Drawing.Color.PaleGreen;
            this.pnl.Controls.Add(this.lblDupHHMError);
            this.pnl.Controls.Add(this.chkNoCommodities);
            this.pnl.Controls.Add(this.chkSupplOnly);
            this.pnl.Controls.Add(this.cboIDType);
            this.pnl.Controls.Add(this.lblVerifyMethod);
            this.pnl.Controls.Add(this.tbBabySvcDescr);
            this.pnl.Controls.Add(this.chkBabyServices);
            this.pnl.Controls.Add(this.chkSingleHeadHH);
            this.pnl.Controls.Add(this.tbeApt);
            this.pnl.Controls.Add(this.label7);
            this.pnl.Controls.Add(this.tbeAddress);
            this.pnl.Controls.Add(this.btnCancel);
            this.pnl.Controls.Add(this.btnSaveClient);
            this.pnl.Controls.Add(this.label3);
            this.pnl.Controls.Add(this.cboSpecialLang);
            this.pnl.Controls.Add(this.label4);
            this.pnl.Controls.Add(this.label11);
            this.pnl.Controls.Add(this.label5);
            this.pnl.Controls.Add(this.tbeState);
            this.pnl.Controls.Add(this.label6);
            this.pnl.Controls.Add(this.label8);
            this.pnl.Controls.Add(this.chkInCityLimits);
            this.pnl.Controls.Add(this.tbeCity);
            this.pnl.Controls.Add(this.cboClientType);
            this.pnl.Controls.Add(this.chkHomeless);
            this.pnl.Controls.Add(this.tbeZipCode);
            this.pnl.Controls.Add(this.lblBizName);
            this.pnl.Location = new System.Drawing.Point(410, 3);
            this.pnl.Name = "pnl";
            this.pnl.Size = new System.Drawing.Size(571, 325);
            this.pnl.TabIndex = 24;
            this.pnl.Visible = false;
            // 
            // lblDupHHMError
            // 
            this.lblDupHHMError.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.lblDupHHMError.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDupHHMError.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDupHHMError.ForeColor = System.Drawing.Color.DarkRed;
            this.lblDupHHMError.Location = new System.Drawing.Point(8, 285);
            this.lblDupHHMError.Name = "lblDupHHMError";
            this.lblDupHHMError.Size = new System.Drawing.Size(556, 24);
            this.lblDupHHMError.TabIndex = 131;
            this.lblDupHHMError.Text = "Duplicate First and Last Name with Same Birth Date  IS NOT ALLOWED";
            this.lblDupHHMError.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblDupHHMError.Visible = false;
            // 
            // chkNoCommodities
            // 
            this.chkNoCommodities.AutoSize = true;
            this.chkNoCommodities.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.helpProvider1.SetHelpString(this.chkNoCommodities, "Check if client lives out of service area, or receives TEFAP eslewhere, or does n" +
        "ot qualify - Toggle checkmark with the Space Bar");
            this.chkNoCommodities.Location = new System.Drawing.Point(350, 202);
            this.chkNoCommodities.Margin = new System.Windows.Forms.Padding(4);
            this.chkNoCommodities.Name = "chkNoCommodities";
            this.helpProvider1.SetShowHelp(this.chkNoCommodities, true);
            this.chkNoCommodities.Size = new System.Drawing.Size(140, 22);
            this.chkNoCommodities.TabIndex = 44;
            this.chkNoCommodities.Tag = "NoCommodities";
            this.chkNoCommodities.Text = "No Commodities";
            this.chkNoCommodities.UseVisualStyleBackColor = true;
            this.chkNoCommodities.Enter += new System.EventHandler(this.chkBox_Enter);
            this.chkNoCommodities.Leave += new System.EventHandler(this.chkBox_Leave);
            // 
            // chkSupplOnly
            // 
            this.chkSupplOnly.AutoSize = true;
            this.chkSupplOnly.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.helpProvider1.SetHelpString(this.chkSupplOnly, " - Toggle checkmark with the Space Bar");
            this.chkSupplOnly.Location = new System.Drawing.Point(350, 178);
            this.chkSupplOnly.Margin = new System.Windows.Forms.Padding(4);
            this.chkSupplOnly.Name = "chkSupplOnly";
            this.helpProvider1.SetShowHelp(this.chkSupplOnly, true);
            this.chkSupplOnly.Size = new System.Drawing.Size(150, 22);
            this.chkSupplOnly.TabIndex = 43;
            this.chkSupplOnly.Tag = "SupplOnly";
            this.chkSupplOnly.Text = "Supplemental Only";
            this.chkSupplOnly.UseVisualStyleBackColor = true;
            this.chkSupplOnly.Enter += new System.EventHandler(this.chkBox_Enter);
            this.chkSupplOnly.Leave += new System.EventHandler(this.chkBox_Leave);
            // 
            // cboIDType
            // 
            this.cboIDType.BackColor = System.Drawing.Color.White;
            this.cboIDType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboIDType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboIDType.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboIDType.FormattingEnabled = true;
            this.helpProvider1.SetHelpString(this.cboIDType, "Select document type used to verify the address - Alt down arrow will display the" +
        " list");
            this.cboIDType.Location = new System.Drawing.Point(112, 167);
            this.cboIDType.Margin = new System.Windows.Forms.Padding(4);
            this.cboIDType.Name = "cboIDType";
            this.helpProvider1.SetShowHelp(this.cboIDType, true);
            this.cboIDType.Size = new System.Drawing.Size(215, 24);
            this.cboIDType.TabIndex = 39;
            this.cboIDType.Tag = "IdType";
            this.cboIDType.SelectedValueChanged += new System.EventHandler(this.cboIDType_SelectedValueChanged);
            this.cboIDType.Enter += new System.EventHandler(this.cboCtl_Enter);
            this.cboIDType.Leave += new System.EventHandler(this.cboCtl_Leave);
            // 
            // lblVerifyMethod
            // 
            this.lblVerifyMethod.BackColor = System.Drawing.Color.Transparent;
            this.lblVerifyMethod.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVerifyMethod.Location = new System.Drawing.Point(7, 165);
            this.lblVerifyMethod.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblVerifyMethod.Name = "lblVerifyMethod";
            this.lblVerifyMethod.Size = new System.Drawing.Size(98, 22);
            this.lblVerifyMethod.TabIndex = 38;
            this.lblVerifyMethod.Text = "Verify Address:";
            this.lblVerifyMethod.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbBabySvcDescr
            // 
            this.tbBabySvcDescr.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbBabySvcDescr.Location = new System.Drawing.Point(372, 248);
            this.tbBabySvcDescr.Multiline = true;
            this.tbBabySvcDescr.Name = "tbBabySvcDescr";
            this.tbBabySvcDescr.Size = new System.Drawing.Size(193, 30);
            this.tbBabySvcDescr.TabIndex = 46;
            this.tbBabySvcDescr.Tag = "BabySvcDescr";
            this.tbBabySvcDescr.Enter += new System.EventHandler(this.tbCtl_Enter);
            this.tbBabySvcDescr.Leave += new System.EventHandler(this.tbCtl_Leave);
            // 
            // chkBabyServices
            // 
            this.chkBabyServices.AutoSize = true;
            this.chkBabyServices.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkBabyServices.Location = new System.Drawing.Point(350, 226);
            this.chkBabyServices.Margin = new System.Windows.Forms.Padding(4);
            this.chkBabyServices.Name = "chkBabyServices";
            this.chkBabyServices.Size = new System.Drawing.Size(121, 22);
            this.chkBabyServices.TabIndex = 45;
            this.chkBabyServices.Tag = "BabyServices";
            this.chkBabyServices.Text = "Baby Services";
            this.chkBabyServices.UseVisualStyleBackColor = true;
            this.chkBabyServices.Enter += new System.EventHandler(this.chkBox_Enter);
            this.chkBabyServices.Leave += new System.EventHandler(this.chkBox_Leave);
            // 
            // chkSingleHeadHH
            // 
            this.chkSingleHeadHH.AutoSize = true;
            this.chkSingleHeadHH.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.helpProvider1.SetHelpString(this.chkSingleHeadHH, " - Toggle checkmark with the Space Bar");
            this.chkSingleHeadHH.Location = new System.Drawing.Point(350, 154);
            this.chkSingleHeadHH.Margin = new System.Windows.Forms.Padding(4);
            this.chkSingleHeadHH.Name = "chkSingleHeadHH";
            this.helpProvider1.SetShowHelp(this.chkSingleHeadHH, true);
            this.chkSingleHeadHH.Size = new System.Drawing.Size(190, 22);
            this.chkSingleHeadHH.TabIndex = 42;
            this.chkSingleHeadHH.Tag = "SingleHeadHh";
            this.chkSingleHeadHH.Text = "Single Parent Household";
            this.chkSingleHeadHH.UseVisualStyleBackColor = true;
            this.chkSingleHeadHH.Enter += new System.EventHandler(this.chkBox_Enter);
            this.chkSingleHeadHH.Leave += new System.EventHandler(this.chkBox_Leave);
            // 
            // tbeApt
            // 
            this.tbeApt.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbeApt.Location = new System.Drawing.Point(112, 30);
            this.tbeApt.Margin = new System.Windows.Forms.Padding(4);
            this.tbeApt.Name = "tbeApt";
            this.tbeApt.Size = new System.Drawing.Size(75, 23);
            this.tbeApt.TabIndex = 27;
            this.tbeApt.Tag = "AptNbr";
            this.tbeApt.Text = "123456789";
            this.tbeApt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbeApt.Enter += new System.EventHandler(this.tbCtl_Enter);
            this.tbeApt.Leave += new System.EventHandler(this.tbCtl_Leave);
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(7, 30);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(98, 22);
            this.label7.TabIndex = 26;
            this.label7.Text = "Apt#";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblBizName
            // 
            this.lblBizName.AutoSize = true;
            this.lblBizName.BackColor = System.Drawing.Color.Gainsboro;
            this.lblBizName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblBizName.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBizName.Location = new System.Drawing.Point(18, 87);
            this.lblBizName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBizName.Name = "lblBizName";
            this.lblBizName.Size = new System.Drawing.Size(2, 19);
            this.lblBizName.TabIndex = 41;
            this.lblBizName.Visible = false;
            // 
            // tbPhone
            // 
            this.tbPhone.Font = new System.Drawing.Font("Lucida Console", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPhone.Location = new System.Drawing.Point(88, 191);
            this.tbPhone.Mask = "(999) 000-0000 aaaaaaaaaaaa";
            this.tbPhone.Name = "tbPhone";
            this.tbPhone.Size = new System.Drawing.Size(190, 21);
            this.tbPhone.TabIndex = 14;
            this.tbPhone.Tag = "phone";
            this.tbPhone.Enter += new System.EventHandler(this.tbPhone_Enter);
            this.tbPhone.Leave += new System.EventHandler(this.tbCtl_Leave);
            // 
            // cboPhoneType
            // 
            this.cboPhoneType.BackColor = System.Drawing.Color.White;
            this.cboPhoneType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPhoneType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboPhoneType.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboPhoneType.FormattingEnabled = true;
            this.helpProvider1.SetHelpString(this.cboPhoneType, " - Alt down arrow will display the list");
            this.cboPhoneType.Location = new System.Drawing.Point(282, 191);
            this.cboPhoneType.Margin = new System.Windows.Forms.Padding(4);
            this.cboPhoneType.Name = "cboPhoneType";
            this.helpProvider1.SetShowHelp(this.cboPhoneType, true);
            this.cboPhoneType.Size = new System.Drawing.Size(122, 21);
            this.cboPhoneType.TabIndex = 15;
            this.cboPhoneType.Tag = "PhoneType";
            this.cboPhoneType.Enter += new System.EventHandler(this.cboCtl_Enter);
            this.cboPhoneType.Leave += new System.EventHandler(this.cboCtl_Leave);
            // 
            // lblPhoneNum
            // 
            this.lblPhoneNum.BackColor = System.Drawing.Color.Transparent;
            this.lblPhoneNum.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPhoneNum.Location = new System.Drawing.Point(1, 191);
            this.lblPhoneNum.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPhoneNum.Name = "lblPhoneNum";
            this.lblPhoneNum.Size = new System.Drawing.Size(80, 22);
            this.lblPhoneNum.TabIndex = 13;
            this.lblPhoneNum.Text = "Phone Num:";
            this.lblPhoneNum.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // spltcontMain
            // 
            this.spltcontMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spltcontMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.spltcontMain.IsSplitterFixed = true;
            this.spltcontMain.Location = new System.Drawing.Point(0, 0);
            this.spltcontMain.Name = "spltcontMain";
            this.spltcontMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // spltcontMain.Panel1
            // 
            this.spltcontMain.Panel1.BackColor = System.Drawing.Color.LightGreen;
            this.spltcontMain.Panel1.Controls.Add(this.label9);
            this.spltcontMain.Panel1.Controls.Add(this.textBox2);
            this.spltcontMain.Panel1.Controls.Add(this.label2);
            this.spltcontMain.Panel1.Controls.Add(this.cboPhoneType);
            this.spltcontMain.Panel1.Controls.Add(this.tbPhone);
            this.spltcontMain.Panel1.Controls.Add(this.textBox1);
            this.spltcontMain.Panel1.Controls.Add(this.lblDupHHError);
            this.spltcontMain.Panel1.Controls.Add(this.lblHeahh);
            this.spltcontMain.Panel1.Controls.Add(this.chkHispanic);
            this.spltcontMain.Panel1.Controls.Add(this.lblPhoneNum);
            this.spltcontMain.Panel1.Controls.Add(this.cboRace);
            this.spltcontMain.Panel1.Controls.Add(this.label10);
            this.spltcontMain.Panel1.Controls.Add(this.chkDissabled);
            this.spltcontMain.Panel1.Controls.Add(this.chkSpecialDiet);
            this.spltcontMain.Panel1.Controls.Add(this.tbBirthDate);
            this.spltcontMain.Panel1.Controls.Add(this.toolStrip1);
            this.spltcontMain.Panel1.Controls.Add(this.lblDateError);
            this.spltcontMain.Panel1.Controls.Add(this.cboMemIDType);
            this.spltcontMain.Panel1.Controls.Add(this.lblMemIdType);
            this.spltcontMain.Panel1.Controls.Add(this.lblMemIdNbr);
            this.spltcontMain.Panel1.Controls.Add(this.tbMemIdNbr);
            this.spltcontMain.Panel1.Controls.Add(this.lblLName);
            this.spltcontMain.Panel1.Controls.Add(this.pnl);
            this.spltcontMain.Panel1.Controls.Add(this.lblFName);
            this.spltcontMain.Panel1.Controls.Add(this.tbFirstName);
            this.spltcontMain.Panel1.Controls.Add(this.label1);
            this.spltcontMain.Panel1.Controls.Add(this.tbLastName);
            this.spltcontMain.Panel1.Controls.Add(this.tbAge);
            this.spltcontMain.Panel1.Controls.Add(this.chkEnterAge);
            this.spltcontMain.Panel1.Controls.Add(this.lblBDay);
            this.spltcontMain.Panel1.Controls.Add(this.tbSex);
            this.spltcontMain.Panel1.Controls.Add(this.lblSex);
            // 
            // spltcontMain.Panel2
            // 
            this.spltcontMain.Panel2.BackColor = System.Drawing.Color.Tan;
            this.spltcontMain.Panel2.Controls.Add(this.lvwSameHouseNbr);
            this.spltcontMain.Panel2.Controls.Add(this.lvwHHMByBirthdate);
            this.spltcontMain.Panel2.Controls.Add(this.lvwPeople);
            this.spltcontMain.Panel2.Controls.Add(this.lvwLastName);
            this.spltcontMain.Size = new System.Drawing.Size(984, 662);
            this.spltcontMain.SplitterDistance = 356;
            this.spltcontMain.SplitterWidth = 1;
            this.spltcontMain.TabIndex = 135;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(1, 221);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(80, 16);
            this.label9.TabIndex = 15;
            this.label9.Text = "Email:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.White;
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.ForeColor = System.Drawing.Color.Black;
            this.helpProvider1.SetHelpString(this.textBox2, "Scan barcode on drivers license or enter preferred user id number");
            this.textBox2.Location = new System.Drawing.Point(88, 216);
            this.textBox2.Name = "textBox2";
            this.helpProvider1.SetShowHelp(this.textBox2, true);
            this.textBox2.Size = new System.Drawing.Size(281, 23);
            this.textBox2.TabIndex = 16;
            this.textBox2.Tag = "EMail";
            this.textBox2.Text = "KENCRAIG8@COMCAST.net";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(1, 84);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Middle:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label2.Visible = false;
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.helpProvider1.SetHelpString(this.textBox1, "Please type in the head of household first name");
            this.textBox1.Location = new System.Drawing.Point(88, 84);
            this.textBox1.Name = "textBox1";
            this.helpProvider1.SetShowHelp(this.textBox1, true);
            this.textBox1.Size = new System.Drawing.Size(184, 23);
            this.textBox1.TabIndex = 5;
            this.textBox1.Tag = "MInitial";
            this.textBox1.Text = "A";
            this.textBox1.Visible = false;
            // 
            // lblDupHHError
            // 
            this.lblDupHHError.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.lblDupHHError.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDupHHError.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDupHHError.ForeColor = System.Drawing.Color.DarkRed;
            this.lblDupHHError.Location = new System.Drawing.Point(44, 3);
            this.lblDupHHError.Name = "lblDupHHError";
            this.lblDupHHError.Size = new System.Drawing.Size(359, 21);
            this.lblDupHHError.TabIndex = 130;
            this.lblDupHHError.Text = "DUPLICATE HOUSEHOLD NAME IS NOT ALLOWED";
            this.lblDupHHError.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblDupHHError.Visible = false;
            // 
            // lblHeahh
            // 
            this.lblHeahh.AutoSize = true;
            this.lblHeahh.BackColor = System.Drawing.Color.Cornsilk;
            this.lblHeahh.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeahh.ForeColor = System.Drawing.Color.Blue;
            this.lblHeahh.Location = new System.Drawing.Point(2, 2);
            this.lblHeahh.Name = "lblHeahh";
            this.lblHeahh.Size = new System.Drawing.Size(147, 20);
            this.lblHeahh.TabIndex = 137;
            this.lblHeahh.Text = "Head of Household";
            // 
            // chkHispanic
            // 
            this.chkHispanic.AutoSize = true;
            this.chkHispanic.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkHispanic.Location = new System.Drawing.Point(9, 300);
            this.chkHispanic.Name = "chkHispanic";
            this.chkHispanic.Size = new System.Drawing.Size(133, 21);
            this.chkHispanic.TabIndex = 21;
            this.chkHispanic.Tag = "Hispanic";
            this.chkHispanic.Text = "Hispanic/Latino";
            this.chkHispanic.UseVisualStyleBackColor = true;
            this.chkHispanic.Enter += new System.EventHandler(this.chkBox_Enter);
            // 
            // cboRace
            // 
            this.cboRace.BackColor = System.Drawing.Color.White;
            this.cboRace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRace.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboRace.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboRace.FormattingEnabled = true;
            this.helpProvider1.SetHelpString(this.cboRace, "HUD Race Categories - Alt down arrow will display the list");
            this.cboRace.Items.AddRange(new object[] {
            "American Indian/Alaskan Native & Black/African American"});
            this.cboRace.Location = new System.Drawing.Point(88, 162);
            this.cboRace.Margin = new System.Windows.Forms.Padding(4);
            this.cboRace.Name = "cboRace";
            this.helpProvider1.SetShowHelp(this.cboRace, true);
            this.cboRace.Size = new System.Drawing.Size(316, 21);
            this.cboRace.TabIndex = 12;
            this.cboRace.Tag = "Race";
            this.cboRace.Enter += new System.EventHandler(this.cboCtl_Enter);
            this.cboRace.Leave += new System.EventHandler(this.cboCtl_Leave);
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(1, 167);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(80, 17);
            this.label10.TabIndex = 11;
            this.label10.Text = "Race:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkDissabled
            // 
            this.chkDissabled.AutoSize = true;
            this.chkDissabled.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.helpProvider1.SetHelpString(this.chkDissabled, "Check if this person is disabled - Toggle checkmark with the Space Bar");
            this.chkDissabled.Location = new System.Drawing.Point(315, 300);
            this.chkDissabled.Name = "chkDissabled";
            this.helpProvider1.SetShowHelp(this.chkDissabled, true);
            this.chkDissabled.Size = new System.Drawing.Size(86, 21);
            this.chkDissabled.TabIndex = 23;
            this.chkDissabled.Tag = "IsDisabled";
            this.chkDissabled.Text = "Disabled";
            this.chkDissabled.UseVisualStyleBackColor = true;
            this.chkDissabled.Enter += new System.EventHandler(this.chkBox_Enter);
            // 
            // chkSpecialDiet
            // 
            this.chkSpecialDiet.AutoSize = true;
            this.chkSpecialDiet.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.helpProvider1.SetHelpString(this.chkSpecialDiet, "Check if this person has special dietary needs - Toggle checkmark with the Space " +
        "Bar");
            this.chkSpecialDiet.Location = new System.Drawing.Point(166, 300);
            this.chkSpecialDiet.Name = "chkSpecialDiet";
            this.helpProvider1.SetShowHelp(this.chkSpecialDiet, true);
            this.chkSpecialDiet.Size = new System.Drawing.Size(108, 21);
            this.chkSpecialDiet.TabIndex = 22;
            this.chkSpecialDiet.Tag = "SpecialDiet";
            this.chkSpecialDiet.Text = "Special Diet";
            this.chkSpecialDiet.UseVisualStyleBackColor = true;
            this.chkSpecialDiet.Enter += new System.EventHandler(this.chkBox_Enter);
            // 
            // tbBirthDate
            // 
            this.tbBirthDate.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.helpProvider1.SetHelpString(this.tbBirthDate, "date format MM/DD/YYYY - leading zeros required - example: 01/04/1948 or 01/04/48" +
        " is OK");
            this.tbBirthDate.Location = new System.Drawing.Point(88, 134);
            this.tbBirthDate.Mask = "00/00/0000";
            this.tbBirthDate.Name = "tbBirthDate";
            this.helpProvider1.SetShowHelp(this.tbBirthDate, true);
            this.tbBirthDate.Size = new System.Drawing.Size(108, 23);
            this.tbBirthDate.TabIndex = 7;
            this.tbBirthDate.Tag = "BirthDate";
            this.tbBirthDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbBirthDate.ValidatingType = typeof(System.DateTime);
            this.tbBirthDate.Enter += new System.EventHandler(this.tbBirthDate_Enter);
            this.tbBirthDate.Leave += new System.EventHandler(this.tbBirthDate_Leave);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbLastName,
            this.tsbPeople,
            this.tsbBirthDate,
            this.tsbHouseNbr});
            this.toolStrip1.Location = new System.Drawing.Point(0, 328);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(350, 25);
            this.toolStrip1.TabIndex = 136;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbLastName
            // 
            this.tsbLastName.BackColor = System.Drawing.Color.Wheat;
            this.tsbLastName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.tsbLastName.Checked = true;
            this.tsbLastName.CheckOnClick = true;
            this.tsbLastName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsbLastName.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbLastName.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.tsbLastName.ForeColor = System.Drawing.Color.DarkBlue;
            this.tsbLastName.Image = ((System.Drawing.Image)(resources.GetObject("tsbLastName.Image")));
            this.tsbLastName.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLastName.Name = "tsbLastName";
            this.tsbLastName.Size = new System.Drawing.Size(67, 22);
            this.tsbLastName.Text = "Last Name";
            this.tsbLastName.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.tsbLastName.Click += new System.EventHandler(this.tsbLastName_Click);
            // 
            // tsbPeople
            // 
            this.tsbPeople.BackColor = System.Drawing.Color.Wheat;
            this.tsbPeople.CheckOnClick = true;
            this.tsbPeople.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbPeople.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.tsbPeople.Image = ((System.Drawing.Image)(resources.GetObject("tsbPeople.Image")));
            this.tsbPeople.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPeople.Name = "tsbPeople";
            this.tsbPeople.Size = new System.Drawing.Size(115, 22);
            this.tsbPeople.Text = "Last and First Name";
            this.tsbPeople.Click += new System.EventHandler(this.tsbPeople_Click);
            // 
            // tsbBirthDate
            // 
            this.tsbBirthDate.BackColor = System.Drawing.Color.Wheat;
            this.tsbBirthDate.CheckOnClick = true;
            this.tsbBirthDate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbBirthDate.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.tsbBirthDate.Image = ((System.Drawing.Image)(resources.GetObject("tsbBirthDate.Image")));
            this.tsbBirthDate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbBirthDate.Name = "tsbBirthDate";
            this.tsbBirthDate.Size = new System.Drawing.Size(64, 22);
            this.tsbBirthDate.Text = "Birth Date";
            this.tsbBirthDate.Click += new System.EventHandler(this.tsbBirthDate_Click);
            // 
            // tsbHouseNbr
            // 
            this.tsbHouseNbr.BackColor = System.Drawing.Color.Wheat;
            this.tsbHouseNbr.Checked = true;
            this.tsbHouseNbr.CheckOnClick = true;
            this.tsbHouseNbr.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsbHouseNbr.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbHouseNbr.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.tsbHouseNbr.Image = ((System.Drawing.Image)(resources.GetObject("tsbHouseNbr.Image")));
            this.tsbHouseNbr.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbHouseNbr.Name = "tsbHouseNbr";
            this.tsbHouseNbr.Size = new System.Drawing.Size(92, 22);
            this.tsbHouseNbr.Text = "House Number";
            this.tsbHouseNbr.Click += new System.EventHandler(this.tsbHouseNbr_Click);
            // 
            // lblDateError
            // 
            this.lblDateError.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.lblDateError.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDateError.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateError.ForeColor = System.Drawing.Color.DarkRed;
            this.lblDateError.Location = new System.Drawing.Point(88, 108);
            this.lblDateError.Name = "lblDateError";
            this.lblDateError.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblDateError.Size = new System.Drawing.Size(154, 24);
            this.lblDateError.TabIndex = 129;
            this.lblDateError.Text = "INVALID BIRTHDATE";
            this.lblDateError.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblDateError.Visible = false;
            // 
            // cboMemIDType
            // 
            this.cboMemIDType.BackColor = System.Drawing.Color.White;
            this.cboMemIDType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMemIDType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboMemIDType.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboMemIDType.FormattingEnabled = true;
            this.helpProvider1.SetHelpString(this.cboMemIDType, "Type of ID Listed in ID Number - Alt down arrow will display the list");
            this.cboMemIDType.Location = new System.Drawing.Point(88, 273);
            this.cboMemIDType.Margin = new System.Windows.Forms.Padding(4);
            this.cboMemIDType.Name = "cboMemIDType";
            this.helpProvider1.SetShowHelp(this.cboMemIDType, true);
            this.cboMemIDType.Size = new System.Drawing.Size(281, 22);
            this.cboMemIDType.TabIndex = 20;
            this.cboMemIDType.Tag = "MemIdType";
            this.cboMemIDType.Enter += new System.EventHandler(this.cboCtl_Enter);
            this.cboMemIDType.Leave += new System.EventHandler(this.cboCtl_Leave);
            // 
            // lblMemIdType
            // 
            this.lblMemIdType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMemIdType.BackColor = System.Drawing.Color.Transparent;
            this.lblMemIdType.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMemIdType.Location = new System.Drawing.Point(1, 275);
            this.lblMemIdType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMemIdType.Name = "lblMemIdType";
            this.lblMemIdType.Size = new System.Drawing.Size(80, 17);
            this.lblMemIdType.TabIndex = 19;
            this.lblMemIdType.Text = "ID Type:";
            this.lblMemIdType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblMemIdNbr
            // 
            this.lblMemIdNbr.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMemIdNbr.Location = new System.Drawing.Point(1, 249);
            this.lblMemIdNbr.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMemIdNbr.Name = "lblMemIdNbr";
            this.lblMemIdNbr.Size = new System.Drawing.Size(80, 16);
            this.lblMemIdNbr.TabIndex = 17;
            this.lblMemIdNbr.Text = "ID Number:";
            this.lblMemIdNbr.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbMemIdNbr
            // 
            this.tbMemIdNbr.BackColor = System.Drawing.Color.White;
            this.tbMemIdNbr.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbMemIdNbr.ForeColor = System.Drawing.Color.Black;
            this.helpProvider1.SetHelpString(this.tbMemIdNbr, "Scan barcode on drivers license or enter preferred user id number");
            this.tbMemIdNbr.Location = new System.Drawing.Point(88, 244);
            this.tbMemIdNbr.Name = "tbMemIdNbr";
            this.helpProvider1.SetShowHelp(this.tbMemIdNbr, true);
            this.tbMemIdNbr.Size = new System.Drawing.Size(281, 26);
            this.tbMemIdNbr.TabIndex = 18;
            this.tbMemIdNbr.Tag = "MemIDNbr";
            this.tbMemIdNbr.Text = "CRAIGKA529BD0120110001";
            this.tbMemIdNbr.Enter += new System.EventHandler(this.tbCtl_Enter);
            this.tbMemIdNbr.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbMemIdNbr_KeyPress);
            this.tbMemIdNbr.Leave += new System.EventHandler(this.tbMemIdNbr_Leave);
            // 
            // lvwSameHouseNbr
            // 
            this.lvwSameHouseNbr.BackColor = System.Drawing.Color.Gainsboro;
            this.lvwSameHouseNbr.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.colAddress,
            this.colAptNbr,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.lvwSameHouseNbr.FullRowSelect = true;
            this.lvwSameHouseNbr.GridLines = true;
            this.lvwSameHouseNbr.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvwSameHouseNbr.LabelWrap = false;
            this.lvwSameHouseNbr.Location = new System.Drawing.Point(3, 3);
            this.lvwSameHouseNbr.Name = "lvwSameHouseNbr";
            this.lvwSameHouseNbr.Size = new System.Drawing.Size(400, 113);
            this.lvwSameHouseNbr.TabIndex = 103;
            this.lvwSameHouseNbr.TabStop = false;
            this.lvwSameHouseNbr.UseCompatibleStateImageBehavior = false;
            this.lvwSameHouseNbr.View = System.Windows.Forms.View.Details;
            this.lvwSameHouseNbr.Visible = false;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 220;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Inactive";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 70;
            // 
            // colAddress
            // 
            this.colAddress.Text = "Address";
            this.colAddress.Width = 240;
            // 
            // colAptNbr
            // 
            this.colAptNbr.Text = "Apt Nbr";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "City, State Zipcode";
            this.columnHeader4.Width = 200;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "# Trx";
            this.columnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "HH ID";
            this.columnHeader6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader6.Width = 80;
            // 
            // lvwPeople
            // 
            this.lvwPeople.BackColor = System.Drawing.Color.Cornsilk;
            this.lvwPeople.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader12,
            this.columnHeader13,
            this.columnHeader14,
            this.columnHeader15});
            this.lvwPeople.GridLines = true;
            this.lvwPeople.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvwPeople.Location = new System.Drawing.Point(433, 159);
            this.lvwPeople.Name = "lvwPeople";
            this.lvwPeople.Size = new System.Drawing.Size(400, 137);
            this.lvwPeople.TabIndex = 101;
            this.lvwPeople.TabStop = false;
            this.lvwPeople.UseCompatibleStateImageBehavior = false;
            this.lvwPeople.View = System.Windows.Forms.View.Details;
            this.lvwPeople.Visible = false;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Last Name";
            this.columnHeader3.Width = 150;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "First Name";
            this.columnHeader7.Width = 120;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Birth Date";
            this.columnHeader8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader8.Width = 100;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "HH Name";
            this.columnHeader9.Width = 150;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "#Svcs";
            this.columnHeader10.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader10.Width = 55;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "HH Ina";
            this.columnHeader11.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "Address";
            this.columnHeader12.Width = 180;
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "City";
            this.columnHeader13.Width = 90;
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "Zip";
            this.columnHeader14.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "HHID";
            this.columnHeader15.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader15.Width = 80;
            // 
            // lvwLastName
            // 
            this.lvwLastName.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.lvwLastName.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmLNLastName,
            this.clmLNFirstName,
            this.clmLNBirthDate,
            this.clmLNHHName,
            this.clmLNNbrSvcs,
            this.clmLNHHInactive,
            this.clmLNAddress,
            this.clmLNCity,
            this.clmLNZip,
            this.clmLNHHID});
            this.lvwLastName.GridLines = true;
            this.lvwLastName.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvwLastName.Location = new System.Drawing.Point(433, 3);
            this.lvwLastName.Name = "lvwLastName";
            this.lvwLastName.Size = new System.Drawing.Size(400, 122);
            this.lvwLastName.TabIndex = 100;
            this.lvwLastName.TabStop = false;
            this.lvwLastName.UseCompatibleStateImageBehavior = false;
            this.lvwLastName.View = System.Windows.Forms.View.Details;
            this.lvwLastName.Visible = false;
            // 
            // clmLNLastName
            // 
            this.clmLNLastName.Text = "Last Name";
            this.clmLNLastName.Width = 150;
            // 
            // clmLNFirstName
            // 
            this.clmLNFirstName.Text = "First Name";
            this.clmLNFirstName.Width = 120;
            // 
            // clmLNBirthDate
            // 
            this.clmLNBirthDate.Text = "Birth Date";
            this.clmLNBirthDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clmLNBirthDate.Width = 100;
            // 
            // clmLNHHName
            // 
            this.clmLNHHName.Text = "HH Name";
            this.clmLNHHName.Width = 150;
            // 
            // clmLNNbrSvcs
            // 
            this.clmLNNbrSvcs.Text = "#Svcs";
            this.clmLNNbrSvcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clmLNNbrSvcs.Width = 55;
            // 
            // clmLNHHInactive
            // 
            this.clmLNHHInactive.Text = "HH Ina";
            this.clmLNHHInactive.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // clmLNAddress
            // 
            this.clmLNAddress.Text = "Address";
            this.clmLNAddress.Width = 180;
            // 
            // clmLNCity
            // 
            this.clmLNCity.Text = "City";
            this.clmLNCity.Width = 90;
            // 
            // clmLNZip
            // 
            this.clmLNZip.Text = "Zip";
            this.clmLNZip.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // clmLNHHID
            // 
            this.clmLNHHID.Text = "HHID";
            this.clmLNHHID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clmLNHHID.Width = 80;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblHelp);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 638);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(984, 24);
            this.panel1.TabIndex = 136;
            // 
            // lblHelp
            // 
            this.lblHelp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHelp.Location = new System.Drawing.Point(0, 0);
            this.lblHelp.Name = "lblHelp";
            this.lblHelp.Size = new System.Drawing.Size(984, 24);
            this.lblHelp.TabIndex = 0;
            this.lblHelp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AddNewHousehold2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Cornsilk;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(984, 662);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.spltcontMain);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimizeBox = false;
            this.Name = "AddNewHousehold2";
            this.Text = "Add New Client Household";
            this.pnl.ResumeLayout(false);
            this.pnl.PerformLayout();
            this.spltcontMain.Panel1.ResumeLayout(false);
            this.spltcontMain.Panel1.PerformLayout();
            this.spltcontMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spltcontMain)).EndInit();
            this.spltcontMain.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblFName;
        private System.Windows.Forms.Label lblLName;
        private System.Windows.Forms.TextBox tbFirstName;
        private System.Windows.Forms.TextBox tbLastName;
        private System.Windows.Forms.Label lblBDay;
        private System.Windows.Forms.Label lblSex;
        private System.Windows.Forms.TextBox tbSex;
        private System.Windows.Forms.Button btnSaveClient;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbAge;
        private System.Windows.Forms.CheckBox chkEnterAge;
        private System.Windows.Forms.ComboBox cboSpecialLang;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbeState;
        private System.Windows.Forms.CheckBox chkInCityLimits;
        private System.Windows.Forms.CheckBox chkHomeless;
        private System.Windows.Forms.TextBox tbeZipCode;
        private System.Windows.Forms.ComboBox cboClientType;
        private System.Windows.Forms.TextBox tbeCity;
        private System.Windows.Forms.TextBox tbeAddress;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView lvwHHMByBirthdate;
        private System.Windows.Forms.ColumnHeader clmLastName;
        private System.Windows.Forms.ColumnHeader clmFirstName;
        private System.Windows.Forms.ColumnHeader clmHHId;
        private System.Windows.Forms.ColumnHeader clmHHMInactive;
        private System.Windows.Forms.ColumnHeader clmHHName;
        private System.Windows.Forms.ColumnHeader clmAddress;
        private System.Windows.Forms.ColumnHeader clmCity;
        private System.Windows.Forms.ColumnHeader clmZip;
        private System.Windows.Forms.ColumnHeader clmHHInactive;
        private System.Windows.Forms.Panel pnl;
        private System.Windows.Forms.SplitContainer spltcontMain;
        private System.Windows.Forms.ListView lvwSameHouseNbr;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader colAddress;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Label lblDateError;
        private System.Windows.Forms.Label lblDupHHError;
        private System.Windows.Forms.TextBox tbeApt;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox chkSingleHeadHH;
        private System.Windows.Forms.TextBox tbBabySvcDescr;
        private System.Windows.Forms.CheckBox chkBabyServices;
        private System.Windows.Forms.ComboBox cboIDType;
        private System.Windows.Forms.Label lblVerifyMethod;
        private System.Windows.Forms.ComboBox cboPhoneType;
        private System.Windows.Forms.Label lblPhoneNum;
        private System.Windows.Forms.ColumnHeader colAptNbr;
        private System.Windows.Forms.Label lblDupHHMError;
        private System.Windows.Forms.ListView lvwLastName;
        private System.Windows.Forms.ColumnHeader clmLNLastName;
        private System.Windows.Forms.ColumnHeader clmLNFirstName;
        private System.Windows.Forms.ColumnHeader clmLNNbrSvcs;
        private System.Windows.Forms.ColumnHeader clmLNHHName;
        private System.Windows.Forms.ColumnHeader clmLNAddress;
        private System.Windows.Forms.ColumnHeader clmLNCity;
        private System.Windows.Forms.ColumnHeader clmLNZip;
        private System.Windows.Forms.ColumnHeader clmLNHHID;
        private System.Windows.Forms.ColumnHeader clmLNHHInactive;
        private System.Windows.Forms.ColumnHeader clmLNBirthDate;
        private System.Windows.Forms.CheckBox chkNoCommodities;
        private System.Windows.Forms.CheckBox chkSupplOnly;
        private System.Windows.Forms.ListView lvwPeople;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.ColumnHeader columnHeader14;
        private System.Windows.Forms.ColumnHeader columnHeader15;
        private System.Windows.Forms.MaskedTextBox tbPhone;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbLastName;
        private System.Windows.Forms.ToolStripButton tsbPeople;
        private System.Windows.Forms.ToolStripButton tsbBirthDate;
        private System.Windows.Forms.ToolStripButton tsbHouseNbr;
        private System.Windows.Forms.ComboBox cboMemIDType;
        private System.Windows.Forms.Label lblMemIdType;
        private System.Windows.Forms.Label lblMemIdNbr;
        private System.Windows.Forms.TextBox tbMemIdNbr;
        private System.Windows.Forms.MaskedTextBox tbBirthDate;
        private System.Windows.Forms.Label lblBizName;
        private System.Windows.Forms.CheckBox chkHispanic;
        private System.Windows.Forms.ComboBox cboRace;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox chkDissabled;
        private System.Windows.Forms.CheckBox chkSpecialDiet;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblHelp;
        private System.Windows.Forms.Label lblHeahh;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox2;
    }
}