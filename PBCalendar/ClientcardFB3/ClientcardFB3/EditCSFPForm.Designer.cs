namespace ClientcardFB3
{
    partial class EditCSFPForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditCSFPForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuActions = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEditExpiration = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiGiveCSFPService = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDeleteService = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPrintPicketlist = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiMarkNewCSFPClient = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFNSWebSite = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCSFPFederal = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCSFPState = new System.Windows.Forms.ToolStripMenuItem();
            this.cboMonth = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboYear = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dgvCSFP = new System.Windows.Forms.DataGridView();
            this.clmHHID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmExpiration = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmMethod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmMethodAsInt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmDateServed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmLbs = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmHHMemID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNameFL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNameLF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmCSFP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmLogID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnRefreshList = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cboOrderBy = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblFilterBy = new System.Windows.Forms.Label();
            this.cboFilter = new System.Windows.Forms.ComboBox();
            this.tbFindName = new System.Windows.Forms.TextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.gbFindClient = new System.Windows.Forms.GroupBox();
            this.gbRenewExpDate = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.chkCSFP = new System.Windows.Forms.CheckBox();
            this.dtpExpDate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.gbNewService = new System.Windows.Forms.GroupBox();
            this.dtpSvcDate = new System.Windows.Forms.DateTimePicker();
            this.lblService = new System.Windows.Forms.Label();
            this.tbLbsCSFP = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSvcCancel = new System.Windows.Forms.Button();
            this.btnSvcSave = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbEditExp = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tbsGiveSvc = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tbsDeleteSvc = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tbsMarkNewCSFP = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tbsPrintPickList = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCSFP)).BeginInit();
            this.gbFindClient.SuspendLayout();
            this.gbRenewExpDate.SuspendLayout();
            this.gbNewService.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuActions,
            this.menuHelp});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(980, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // menuActions
            // 
            this.menuActions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiEditExpiration,
            this.tsmiGiveCSFPService,
            this.tsmiDeleteService,
            this.tsmiPrintPicketlist,
            this.toolStripSeparator1,
            this.tsmiMarkNewCSFPClient,
            this.toolStripSeparator2,
            this.closeToolStripMenuItem});
            this.menuActions.Name = "menuActions";
            this.menuActions.Size = new System.Drawing.Size(70, 24);
            this.menuActions.Text = "&Actions";
            // 
            // tsmiEditExpiration
            // 
            this.tsmiEditExpiration.Name = "tsmiEditExpiration";
            this.tsmiEditExpiration.Size = new System.Drawing.Size(223, 24);
            this.tsmiEditExpiration.Text = "&Edit Expiration";
            this.tsmiEditExpiration.Click += new System.EventHandler(this.EditExpiration_Click);
            // 
            // tsmiGiveCSFPService
            // 
            this.tsmiGiveCSFPService.Name = "tsmiGiveCSFPService";
            this.tsmiGiveCSFPService.Size = new System.Drawing.Size(223, 24);
            this.tsmiGiveCSFPService.Text = "&Give Service";
            this.tsmiGiveCSFPService.Click += new System.EventHandler(this.GiveCSFPService_Click);
            // 
            // tsmiDeleteService
            // 
            this.tsmiDeleteService.Name = "tsmiDeleteService";
            this.tsmiDeleteService.Size = new System.Drawing.Size(223, 24);
            this.tsmiDeleteService.Text = "&Delete Service";
            this.tsmiDeleteService.Click += new System.EventHandler(this.DeleteService_Click);
            // 
            // tsmiPrintPicketlist
            // 
            this.tsmiPrintPicketlist.Name = "tsmiPrintPicketlist";
            this.tsmiPrintPicketlist.Size = new System.Drawing.Size(223, 24);
            this.tsmiPrintPicketlist.Text = "&Print Picketlist";
            this.tsmiPrintPicketlist.Click += new System.EventHandler(this.PrintPicketlist_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(220, 6);
            // 
            // tsmiMarkNewCSFPClient
            // 
            this.tsmiMarkNewCSFPClient.Name = "tsmiMarkNewCSFPClient";
            this.tsmiMarkNewCSFPClient.Size = new System.Drawing.Size(223, 24);
            this.tsmiMarkNewCSFPClient.Text = "Mark &New CSFP Client";
            this.tsmiMarkNewCSFPClient.Click += new System.EventHandler(this.MarkNewCSFPClient_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(220, 6);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(223, 24);
            this.closeToolStripMenuItem.Text = "&Close";
            // 
            // menuHelp
            // 
            this.menuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFNSWebSite,
            this.menuCSFPFederal,
            this.menuCSFPState});
            this.menuHelp.Name = "menuHelp";
            this.menuHelp.Size = new System.Drawing.Size(53, 24);
            this.menuHelp.Text = "&Help";
            // 
            // menuFNSWebSite
            // 
            this.menuFNSWebSite.Name = "menuFNSWebSite";
            this.menuFNSWebSite.Size = new System.Drawing.Size(245, 24);
            this.menuFNSWebSite.Text = "FNS &Web Site";
            this.menuFNSWebSite.Click += new System.EventHandler(this.menuFNSWebSite_Click);
            // 
            // menuCSFPFederal
            // 
            this.menuCSFPFederal.Name = "menuCSFPFederal";
            this.menuCSFPFederal.Size = new System.Drawing.Size(245, 24);
            this.menuCSFPFederal.Text = "&Federal CSFP Information";
            this.menuCSFPFederal.Click += new System.EventHandler(this.menuCSFPRules_Click);
            // 
            // menuCSFPState
            // 
            this.menuCSFPState.Name = "menuCSFPState";
            this.menuCSFPState.Size = new System.Drawing.Size(245, 24);
            this.menuCSFPState.Text = "&State CSFP Information";
            this.menuCSFPState.Click += new System.EventHandler(this.menuCSFPState_Click);
            // 
            // cboMonth
            // 
            this.cboMonth.FormattingEnabled = true;
            this.cboMonth.Items.AddRange(new object[] {
            "January",
            "Febuary",
            "March",
            "April",
            "May",
            "June",
            "July",
            "August",
            "September",
            "October",
            "November",
            "December"});
            this.cboMonth.Location = new System.Drawing.Point(82, 149);
            this.cboMonth.Margin = new System.Windows.Forms.Padding(4);
            this.cboMonth.Name = "cboMonth";
            this.cboMonth.Size = new System.Drawing.Size(90, 28);
            this.cboMonth.TabIndex = 31;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 130);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 17);
            this.label2.TabIndex = 28;
            this.label2.Text = "Year:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // cboYear
            // 
            this.cboYear.FormattingEnabled = true;
            this.cboYear.Location = new System.Drawing.Point(4, 149);
            this.cboYear.Margin = new System.Windows.Forms.Padding(4);
            this.cboYear.Name = "cboYear";
            this.cboYear.Size = new System.Drawing.Size(70, 28);
            this.cboYear.TabIndex = 30;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(82, 130);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 17);
            this.label3.TabIndex = 29;
            this.label3.Text = "Month:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // dgvCSFP
            // 
            this.dgvCSFP.AllowUserToAddRows = false;
            this.dgvCSFP.AllowUserToDeleteRows = false;
            this.dgvCSFP.AllowUserToResizeRows = false;
            this.dgvCSFP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCSFP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCSFP.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmHHID,
            this.clmName,
            this.clmAddress,
            this.clmExpiration,
            this.clmMethod,
            this.clmMethodAsInt,
            this.clmDateServed,
            this.clmLbs,
            this.clmHHMemID,
            this.colNameFL,
            this.colNameLF,
            this.clmCSFP,
            this.clmLogID});
            this.dgvCSFP.Location = new System.Drawing.Point(0, 248);
            this.dgvCSFP.Name = "dgvCSFP";
            this.dgvCSFP.ReadOnly = true;
            this.dgvCSFP.RowHeadersVisible = false;
            this.dgvCSFP.RowTemplate.Height = 24;
            this.dgvCSFP.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCSFP.Size = new System.Drawing.Size(980, 435);
            this.dgvCSFP.TabIndex = 32;
            // 
            // clmHHID
            // 
            this.clmHHID.HeaderText = "HHID";
            this.clmHHID.Name = "clmHHID";
            this.clmHHID.ReadOnly = true;
            this.clmHHID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.clmHHID.Width = 60;
            // 
            // clmName
            // 
            this.clmName.HeaderText = "Family Member Name";
            this.clmName.Name = "clmName";
            this.clmName.ReadOnly = true;
            this.clmName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.clmName.Width = 200;
            // 
            // clmAddress
            // 
            this.clmAddress.HeaderText = "Address";
            this.clmAddress.Name = "clmAddress";
            this.clmAddress.ReadOnly = true;
            this.clmAddress.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.clmAddress.Width = 200;
            // 
            // clmExpiration
            // 
            this.clmExpiration.HeaderText = "Exp. Date";
            this.clmExpiration.Name = "clmExpiration";
            this.clmExpiration.ReadOnly = true;
            this.clmExpiration.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // clmMethod
            // 
            this.clmMethod.HeaderText = "Method";
            this.clmMethod.Name = "clmMethod";
            this.clmMethod.ReadOnly = true;
            this.clmMethod.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.clmMethod.Width = 120;
            // 
            // clmMethodAsInt
            // 
            this.clmMethodAsInt.HeaderText = "Column1";
            this.clmMethodAsInt.Name = "clmMethodAsInt";
            this.clmMethodAsInt.ReadOnly = true;
            this.clmMethodAsInt.Visible = false;
            // 
            // clmDateServed
            // 
            this.clmDateServed.HeaderText = "Date Served";
            this.clmDateServed.Name = "clmDateServed";
            this.clmDateServed.ReadOnly = true;
            this.clmDateServed.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // clmLbs
            // 
            this.clmLbs.HeaderText = "Lbs";
            this.clmLbs.Name = "clmLbs";
            this.clmLbs.ReadOnly = true;
            this.clmLbs.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.clmLbs.Width = 90;
            // 
            // clmHHMemID
            // 
            this.clmHHMemID.HeaderText = "HHMemID";
            this.clmHHMemID.Name = "clmHHMemID";
            this.clmHHMemID.ReadOnly = true;
            this.clmHHMemID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.clmHHMemID.Width = 80;
            // 
            // colNameFL
            // 
            this.colNameFL.HeaderText = "NameFL";
            this.colNameFL.Name = "colNameFL";
            this.colNameFL.ReadOnly = true;
            this.colNameFL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.colNameFL.Visible = false;
            // 
            // colNameLF
            // 
            this.colNameLF.HeaderText = "NameLF";
            this.colNameLF.Name = "colNameLF";
            this.colNameLF.ReadOnly = true;
            this.colNameLF.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.colNameLF.Visible = false;
            // 
            // clmCSFP
            // 
            this.clmCSFP.HeaderText = "CSFP";
            this.clmCSFP.Name = "clmCSFP";
            this.clmCSFP.ReadOnly = true;
            this.clmCSFP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.clmCSFP.Visible = false;
            // 
            // clmLogID
            // 
            this.clmLogID.HeaderText = "ID";
            this.clmLogID.Name = "clmLogID";
            this.clmLogID.ReadOnly = true;
            this.clmLogID.Visible = false;
            // 
            // btnRefreshList
            // 
            this.btnRefreshList.Location = new System.Drawing.Point(21, 180);
            this.btnRefreshList.Name = "btnRefreshList";
            this.btnRefreshList.Size = new System.Drawing.Size(122, 27);
            this.btnRefreshList.TabIndex = 33;
            this.btnRefreshList.Text = "Load Period";
            this.btnRefreshList.UseVisualStyleBackColor = true;
            this.btnRefreshList.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 16);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 20);
            this.label1.TabIndex = 79;
            this.label1.Tag = "";
            this.label1.Text = "Search Field:";
            // 
            // cboOrderBy
            // 
            this.cboOrderBy.BackColor = System.Drawing.Color.OldLace;
            this.cboOrderBy.DropDownHeight = 225;
            this.cboOrderBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboOrderBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboOrderBy.ForeColor = System.Drawing.Color.Black;
            this.cboOrderBy.FormattingEnabled = true;
            this.cboOrderBy.IntegralHeight = false;
            this.cboOrderBy.Items.AddRange(new object[] {
            "Last, First Name",
            "FirstName, LastName",
            "DistributionMethod",
            "HouseholdID",
            "Expiration Date"});
            this.cboOrderBy.Location = new System.Drawing.Point(224, 37);
            this.cboOrderBy.Margin = new System.Windows.Forms.Padding(4);
            this.cboOrderBy.Name = "cboOrderBy";
            this.cboOrderBy.Size = new System.Drawing.Size(157, 26);
            this.cboOrderBy.TabIndex = 78;
            this.cboOrderBy.SelectedIndexChanged += new System.EventHandler(this.cboOrderBy_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(221, 16);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 20);
            this.label4.TabIndex = 77;
            this.label4.Tag = "SortOrder";
            this.label4.Text = "Order By:";
            // 
            // lblFilterBy
            // 
            this.lblFilterBy.AutoSize = true;
            this.lblFilterBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilterBy.Location = new System.Drawing.Point(389, 16);
            this.lblFilterBy.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFilterBy.Name = "lblFilterBy";
            this.lblFilterBy.Size = new System.Drawing.Size(95, 20);
            this.lblFilterBy.TabIndex = 72;
            this.lblFilterBy.Tag = "SortOrder";
            this.lblFilterBy.Text = "Filtered By:";
            // 
            // cboFilter
            // 
            this.cboFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFilter.FormattingEnabled = true;
            this.cboFilter.Location = new System.Drawing.Point(389, 36);
            this.cboFilter.Margin = new System.Windows.Forms.Padding(4);
            this.cboFilter.Name = "cboFilter";
            this.cboFilter.Size = new System.Drawing.Size(125, 28);
            this.cboFilter.TabIndex = 73;
            this.cboFilter.Visible = false;
            this.cboFilter.SelectedIndexChanged += new System.EventHandler(this.cboFilter_SelectedIndexChanged);
            // 
            // tbFindName
            // 
            this.tbFindName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.tbFindName.HideSelection = false;
            this.tbFindName.Location = new System.Drawing.Point(6, 36);
            this.tbFindName.Margin = new System.Windows.Forms.Padding(4);
            this.tbFindName.Name = "tbFindName";
            this.tbFindName.Size = new System.Drawing.Size(210, 28);
            this.tbFindName.TabIndex = 70;
            this.tbFindName.WordWrap = false;
            this.tbFindName.TextChanged += new System.EventHandler(this.tbFindName_TextChanged);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 35);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(4);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(204, 28);
            this.progressBar1.Step = 1;
            this.progressBar1.TabIndex = 76;
            this.progressBar1.Value = 1;
            this.progressBar1.Visible = false;
            // 
            // gbFindClient
            // 
            this.gbFindClient.Controls.Add(this.tbFindName);
            this.gbFindClient.Controls.Add(this.progressBar1);
            this.gbFindClient.Controls.Add(this.label1);
            this.gbFindClient.Controls.Add(this.cboFilter);
            this.gbFindClient.Controls.Add(this.cboOrderBy);
            this.gbFindClient.Controls.Add(this.lblFilterBy);
            this.gbFindClient.Controls.Add(this.label4);
            this.gbFindClient.Location = new System.Drawing.Point(218, 98);
            this.gbFindClient.Name = "gbFindClient";
            this.gbFindClient.Size = new System.Drawing.Size(522, 70);
            this.gbFindClient.TabIndex = 80;
            this.gbFindClient.TabStop = false;
            this.gbFindClient.Visible = false;
            // 
            // gbRenewExpDate
            // 
            this.gbRenewExpDate.Controls.Add(this.btnCancel);
            this.gbRenewExpDate.Controls.Add(this.btnSave);
            this.gbRenewExpDate.Controls.Add(this.chkCSFP);
            this.gbRenewExpDate.Controls.Add(this.dtpExpDate);
            this.gbRenewExpDate.Controls.Add(this.label5);
            this.gbRenewExpDate.Location = new System.Drawing.Point(218, 179);
            this.gbRenewExpDate.Name = "gbRenewExpDate";
            this.gbRenewExpDate.Size = new System.Drawing.Size(522, 63);
            this.gbRenewExpDate.TabIndex = 81;
            this.gbRenewExpDate.TabStop = false;
            this.gbRenewExpDate.Visible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(443, 20);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(71, 30);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(358, 20);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(79, 30);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // chkCSFP
            // 
            this.chkCSFP.Location = new System.Drawing.Point(6, 24);
            this.chkCSFP.Name = "chkCSFP";
            this.chkCSFP.Size = new System.Drawing.Size(79, 20);
            this.chkCSFP.TabIndex = 2;
            this.chkCSFP.Text = "CSFP";
            this.chkCSFP.UseVisualStyleBackColor = true;
            // 
            // dtpExpDate
            // 
            this.dtpExpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpExpDate.Location = new System.Drawing.Point(225, 24);
            this.dtpExpDate.Name = "dtpExpDate";
            this.dtpExpDate.Size = new System.Drawing.Size(127, 26);
            this.dtpExpDate.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(98, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(129, 20);
            this.label5.TabIndex = 0;
            this.label5.Text = "Expiration Date:";
            // 
            // gbNewService
            // 
            this.gbNewService.Controls.Add(this.dtpSvcDate);
            this.gbNewService.Controls.Add(this.lblService);
            this.gbNewService.Controls.Add(this.tbLbsCSFP);
            this.gbNewService.Controls.Add(this.label6);
            this.gbNewService.Controls.Add(this.btnSvcCancel);
            this.gbNewService.Controls.Add(this.btnSvcSave);
            this.gbNewService.Location = new System.Drawing.Point(746, 170);
            this.gbNewService.Name = "gbNewService";
            this.gbNewService.Size = new System.Drawing.Size(522, 72);
            this.gbNewService.TabIndex = 82;
            this.gbNewService.TabStop = false;
            this.gbNewService.Visible = false;
            // 
            // dtpSvcDate
            // 
            this.dtpSvcDate.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpSvcDate.Location = new System.Drawing.Point(6, 37);
            this.dtpSvcDate.Name = "dtpSvcDate";
            this.dtpSvcDate.Size = new System.Drawing.Size(293, 26);
            this.dtpSvcDate.TabIndex = 110;
            // 
            // lblService
            // 
            this.lblService.AutoSize = true;
            this.lblService.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblService.Location = new System.Drawing.Point(9, 16);
            this.lblService.Name = "lblService";
            this.lblService.Size = new System.Drawing.Size(125, 20);
            this.lblService.TabIndex = 109;
            this.lblService.Text = "Date of Service";
            this.lblService.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbLbsCSFP
            // 
            this.tbLbsCSFP.Location = new System.Drawing.Point(305, 37);
            this.tbLbsCSFP.Name = "tbLbsCSFP";
            this.tbLbsCSFP.Size = new System.Drawing.Size(70, 26);
            this.tbLbsCSFP.TabIndex = 6;
            this.tbLbsCSFP.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbLbsCSFP_KeyDown);
            this.tbLbsCSFP.Leave += new System.EventHandler(this.tbLbsCSFP_Leave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(305, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 18);
            this.label6.TabIndex = 5;
            this.label6.Text = "Lbs CSFP";
            // 
            // btnSvcCancel
            // 
            this.btnSvcCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSvcCancel.Location = new System.Drawing.Point(454, 33);
            this.btnSvcCancel.Name = "btnSvcCancel";
            this.btnSvcCancel.Size = new System.Drawing.Size(60, 30);
            this.btnSvcCancel.TabIndex = 4;
            this.btnSvcCancel.Text = "Cancel";
            this.btnSvcCancel.UseVisualStyleBackColor = true;
            this.btnSvcCancel.Click += new System.EventHandler(this.btnSvcCancel_Click);
            // 
            // btnSvcSave
            // 
            this.btnSvcSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSvcSave.Location = new System.Drawing.Point(388, 33);
            this.btnSvcSave.Name = "btnSvcSave";
            this.btnSvcSave.Size = new System.Drawing.Size(60, 30);
            this.btnSvcSave.TabIndex = 3;
            this.btnSvcSave.Text = "Save";
            this.btnSvcSave.UseVisualStyleBackColor = true;
            this.btnSvcSave.Click += new System.EventHandler(this.btnSvcSave_Click);
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(8, 98);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(150, 32);
            this.label7.TabIndex = 83;
            this.label7.Text = "CSFP Period";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.Color.Cornsilk;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbEditExp,
            this.toolStripSeparator3,
            this.tbsGiveSvc,
            this.toolStripSeparator4,
            this.tbsDeleteSvc,
            this.toolStripSeparator5,
            this.tbsMarkNewCSFP,
            this.toolStripSeparator6,
            this.tbsPrintPickList,
            this.toolStripSeparator7});
            this.toolStrip1.Location = new System.Drawing.Point(0, 28);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(980, 70);
            this.toolStrip1.TabIndex = 84;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbEditExp
            // 
            this.tsbEditExp.AutoSize = false;
            this.tsbEditExp.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.tsbEditExp.Image = ((System.Drawing.Image)(resources.GetObject("tsbEditExp.Image")));
            this.tsbEditExp.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.tsbEditExp.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbEditExp.ImageTransparentColor = System.Drawing.Color.Red;
            this.tsbEditExp.Name = "tsbEditExp";
            this.tsbEditExp.Size = new System.Drawing.Size(100, 65);
            this.tsbEditExp.Text = "&Edit Expiration";
            this.tsbEditExp.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.tsbEditExp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbEditExp.Click += new System.EventHandler(this.EditExpiration_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 70);
            // 
            // tbsGiveSvc
            // 
            this.tbsGiveSvc.AutoSize = false;
            this.tbsGiveSvc.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.tbsGiveSvc.Image = ((System.Drawing.Image)(resources.GetObject("tbsGiveSvc.Image")));
            this.tbsGiveSvc.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.tbsGiveSvc.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tbsGiveSvc.ImageTransparentColor = System.Drawing.Color.Red;
            this.tbsGiveSvc.Name = "tbsGiveSvc";
            this.tbsGiveSvc.Size = new System.Drawing.Size(90, 65);
            this.tbsGiveSvc.Text = "&Give Service";
            this.tbsGiveSvc.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.tbsGiveSvc.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbsGiveSvc.Click += new System.EventHandler(this.GiveCSFPService_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 70);
            // 
            // tbsDeleteSvc
            // 
            this.tbsDeleteSvc.AutoSize = false;
            this.tbsDeleteSvc.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.tbsDeleteSvc.Image = ((System.Drawing.Image)(resources.GetObject("tbsDeleteSvc.Image")));
            this.tbsDeleteSvc.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.tbsDeleteSvc.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tbsDeleteSvc.ImageTransparentColor = System.Drawing.Color.Red;
            this.tbsDeleteSvc.Name = "tbsDeleteSvc";
            this.tbsDeleteSvc.Size = new System.Drawing.Size(100, 65);
            this.tbsDeleteSvc.Text = "&Delete Service";
            this.tbsDeleteSvc.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.tbsDeleteSvc.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbsDeleteSvc.Click += new System.EventHandler(this.DeleteService_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 70);
            // 
            // tbsMarkNewCSFP
            // 
            this.tbsMarkNewCSFP.AutoSize = false;
            this.tbsMarkNewCSFP.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.tbsMarkNewCSFP.Image = ((System.Drawing.Image)(resources.GetObject("tbsMarkNewCSFP.Image")));
            this.tbsMarkNewCSFP.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.tbsMarkNewCSFP.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tbsMarkNewCSFP.ImageTransparentColor = System.Drawing.Color.Red;
            this.tbsMarkNewCSFP.Name = "tbsMarkNewCSFP";
            this.tbsMarkNewCSFP.Size = new System.Drawing.Size(115, 65);
            this.tbsMarkNewCSFP.Text = "&New CSFP Client";
            this.tbsMarkNewCSFP.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.tbsMarkNewCSFP.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbsMarkNewCSFP.Click += new System.EventHandler(this.MarkNewCSFPClient_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 70);
            // 
            // tbsPrintPickList
            // 
            this.tbsPrintPickList.AutoSize = false;
            this.tbsPrintPickList.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.tbsPrintPickList.Image = ((System.Drawing.Image)(resources.GetObject("tbsPrintPickList.Image")));
            this.tbsPrintPickList.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.tbsPrintPickList.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tbsPrintPickList.ImageTransparentColor = System.Drawing.Color.Red;
            this.tbsPrintPickList.Name = "tbsPrintPickList";
            this.tbsPrintPickList.Size = new System.Drawing.Size(90, 65);
            this.tbsPrintPickList.Text = "&Print Picklist";
            this.tbsPrintPickList.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.tbsPrintPickList.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbsPrintPickList.Click += new System.EventHandler(this.PrintPicketlist_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 70);
            // 
            // EditCSFPForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Cornsilk;
            this.ClientSize = new System.Drawing.Size(980, 665);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.dgvCSFP);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.gbRenewExpDate);
            this.Controls.Add(this.btnRefreshList);
            this.Controls.Add(this.gbNewService);
            this.Controls.Add(this.cboMonth);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboYear);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.gbFindClient);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "EditCSFPForm";
            this.Text = "CSFP Maintenance Form";
            this.Load += new System.EventHandler(this.EditCSFPForm_Load);
            this.Resize += new System.EventHandler(this.EditCSFPForm_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCSFP)).EndInit();
            this.gbFindClient.ResumeLayout(false);
            this.gbFindClient.PerformLayout();
            this.gbRenewExpDate.ResumeLayout(false);
            this.gbRenewExpDate.PerformLayout();
            this.gbNewService.ResumeLayout(false);
            this.gbNewService.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuActions;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditExpiration;
        private System.Windows.Forms.ToolStripMenuItem tsmiMarkNewCSFPClient;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmiGiveCSFPService;
        private System.Windows.Forms.ToolStripMenuItem tsmiPrintPicketlist;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ComboBox cboMonth;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboYear;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgvCSFP;
        private System.Windows.Forms.Button btnRefreshList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboOrderBy;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblFilterBy;
        private System.Windows.Forms.ComboBox cboFilter;
        public System.Windows.Forms.TextBox tbFindName;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.GroupBox gbFindClient;
        private System.Windows.Forms.GroupBox gbRenewExpDate;
        private System.Windows.Forms.CheckBox chkCSFP;
        private System.Windows.Forms.DateTimePicker dtpExpDate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox gbNewService;
        private System.Windows.Forms.Button btnSvcCancel;
        private System.Windows.Forms.Button btnSvcSave;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbLbsCSFP;
        private System.Windows.Forms.DateTimePicker dtpSvcDate;
        private System.Windows.Forms.Label lblService;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ToolStripMenuItem menuHelp;
        private System.Windows.Forms.ToolStripMenuItem menuCSFPFederal;
        private System.Windows.Forms.ToolStripMenuItem menuFNSWebSite;
        private System.Windows.Forms.ToolStripMenuItem menuCSFPState;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmHHID;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmExpiration;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmMethod;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmMethodAsInt;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmDateServed;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmLbs;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmHHMemID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNameFL;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNameLF;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmCSFP;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmLogID;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeleteService;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbEditExp;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tbsGiveSvc;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton tbsDeleteSvc;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton tbsMarkNewCSFP;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton tbsPrintPickList;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
    }
}