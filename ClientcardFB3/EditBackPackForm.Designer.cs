namespace ClientcardFB3
{
    partial class EditBackpackForm
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
                if (clsBackpackLog != null)
                {
                    clsBackpackLog.Dispose();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditBackpackForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuActions = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEditExpiration = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiGiveBackpackService = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDeleteService = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPrintPicketlist = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiMarkNewBackpackClient = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFNSWebSite = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBackpackFederal = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBackpackState = new System.Windows.Forms.ToolStripMenuItem();
            this.cboMonth = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboYear = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dgvBackpack = new System.Windows.Forms.DataGridView();
            this.clmHHID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmAptNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmExpiration = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmMethod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmMethodAsInt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmDateServed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmLbs = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmHHMemID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNameFL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNameLF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmBackpack = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmLogID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnRefreshList = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbEditExp = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tbsGiveSvc = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tbsDeleteSvc = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tbsMarkNewBackpack = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tbsPrintPickList = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.gbOrderBy = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cboOrderBy2 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cboOrderBy1 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbFindName = new System.Windows.Forms.TextBox();
            this.cboOrderBy0 = new System.Windows.Forms.ComboBox();
            this.gbNewService = new System.Windows.Forms.GroupBox();
            this.dtpSvcDate = new System.Windows.Forms.DateTimePicker();
            this.tbLbsBackpack = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSvcCancel = new System.Windows.Forms.Button();
            this.btnSvcSave = new System.Windows.Forms.Button();
            this.lblService = new System.Windows.Forms.Label();
            this.gbFilter = new System.Windows.Forms.GroupBox();
            this.cboFilterFld = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cboFilter = new System.Windows.Forms.ComboBox();
            this.lblFilterBy = new System.Windows.Forms.Label();
            this.gbRenewExpDate = new System.Windows.Forms.GroupBox();
            this.btnAdd6Months = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.chkBackpack = new System.Windows.Forms.CheckBox();
            this.dtpExpDate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBackpack)).BeginInit();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.gbOrderBy.SuspendLayout();
            this.gbNewService.SuspendLayout();
            this.gbFilter.SuspendLayout();
            this.gbRenewExpDate.SuspendLayout();
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
            this.menuStrip1.Size = new System.Drawing.Size(984, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuActions
            // 
            this.menuActions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiEditExpiration,
            this.tsmiGiveBackpackService,
            this.tsmiDeleteService,
            this.tsmiPrintPicketlist,
            this.toolStripSeparator1,
            this.tsmiMarkNewBackpackClient,
            this.toolStripSeparator2,
            this.closeToolStripMenuItem});
            this.menuActions.Name = "menuActions";
            this.menuActions.Size = new System.Drawing.Size(59, 20);
            this.menuActions.Text = "&Actions";
            // 
            // tsmiEditExpiration
            // 
            this.tsmiEditExpiration.Name = "tsmiEditExpiration";
            this.tsmiEditExpiration.Size = new System.Drawing.Size(215, 22);
            this.tsmiEditExpiration.Text = "&Edit Expiration";
            this.tsmiEditExpiration.Click += new System.EventHandler(this.EditExpiration_Click);
            // 
            // tsmiGiveBackpackService
            // 
            this.tsmiGiveBackpackService.Name = "tsmiGiveBackpackService";
            this.tsmiGiveBackpackService.Size = new System.Drawing.Size(215, 22);
            this.tsmiGiveBackpackService.Text = "&Give Service";
            this.tsmiGiveBackpackService.Click += new System.EventHandler(this.GiveBackpackService_Click);
            // 
            // tsmiDeleteService
            // 
            this.tsmiDeleteService.Name = "tsmiDeleteService";
            this.tsmiDeleteService.Size = new System.Drawing.Size(215, 22);
            this.tsmiDeleteService.Text = "&Delete Service";
            this.tsmiDeleteService.Click += new System.EventHandler(this.DeleteService_Click);
            // 
            // tsmiPrintPicketlist
            // 
            this.tsmiPrintPicketlist.Name = "tsmiPrintPicketlist";
            this.tsmiPrintPicketlist.Size = new System.Drawing.Size(215, 22);
            this.tsmiPrintPicketlist.Text = "&Print Picketlist";
            this.tsmiPrintPicketlist.Click += new System.EventHandler(this.PrintPicketlist_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(212, 6);
            // 
            // tsmiMarkNewBackpackClient
            // 
            this.tsmiMarkNewBackpackClient.Name = "tsmiMarkNewBackpackClient";
            this.tsmiMarkNewBackpackClient.Size = new System.Drawing.Size(215, 22);
            this.tsmiMarkNewBackpackClient.Text = "Mark &New Backpack Client";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(212, 6);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.closeToolStripMenuItem.Text = "&Close";
            // 
            // menuHelp
            // 
            this.menuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFNSWebSite,
            this.menuBackpackFederal,
            this.menuBackpackState});
            this.menuHelp.Name = "menuHelp";
            this.menuHelp.Size = new System.Drawing.Size(44, 20);
            this.menuHelp.Text = "&Help";
            // 
            // menuFNSWebSite
            // 
            this.menuFNSWebSite.Name = "menuFNSWebSite";
            this.menuFNSWebSite.Size = new System.Drawing.Size(231, 22);
            this.menuFNSWebSite.Text = "FNS &Web Site";
            // 
            // menuBackpackFederal
            // 
            this.menuBackpackFederal.Name = "menuBackpackFederal";
            this.menuBackpackFederal.Size = new System.Drawing.Size(231, 22);
            this.menuBackpackFederal.Text = "&Federal Backpack Information";
            // 
            // menuBackpackState
            // 
            this.menuBackpackState.Name = "menuBackpackState";
            this.menuBackpackState.Size = new System.Drawing.Size(231, 22);
            this.menuBackpackState.Text = "&State Backpack Information";
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
            this.cboMonth.Location = new System.Drawing.Point(86, 60);
            this.cboMonth.Margin = new System.Windows.Forms.Padding(4);
            this.cboMonth.Name = "cboMonth";
            this.cboMonth.Size = new System.Drawing.Size(90, 24);
            this.cboMonth.TabIndex = 31;
            this.cboMonth.SelectionChangeCommitted += new System.EventHandler(this.cboPeriod_SelectionChangeCommitted);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 41);
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
            this.cboYear.Location = new System.Drawing.Point(8, 60);
            this.cboYear.Margin = new System.Windows.Forms.Padding(4);
            this.cboYear.Name = "cboYear";
            this.cboYear.Size = new System.Drawing.Size(70, 24);
            this.cboYear.TabIndex = 30;
            this.cboYear.SelectionChangeCommitted += new System.EventHandler(this.cboPeriod_SelectionChangeCommitted);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(86, 41);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 17);
            this.label3.TabIndex = 29;
            this.label3.Text = "Month:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // dgvBackpack
            // 
            this.dgvBackpack.AllowUserToAddRows = false;
            this.dgvBackpack.AllowUserToDeleteRows = false;
            this.dgvBackpack.AllowUserToResizeRows = false;
            this.dgvBackpack.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBackpack.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmHHID,
            this.clmName,
            this.clmAddress,
            this.clmAptNum,
            this.clmExpiration,
            this.clmMethod,
            this.clmMethodAsInt,
            this.clmDateServed,
            this.clmLbs,
            this.clmHHMemID,
            this.colNameFL,
            this.colNameLF,
            this.clmBackpack,
            this.clmLogID});
            this.dgvBackpack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBackpack.Location = new System.Drawing.Point(0, 0);
            this.dgvBackpack.Name = "dgvBackpack";
            this.dgvBackpack.ReadOnly = true;
            this.dgvBackpack.RowHeadersVisible = false;
            this.dgvBackpack.RowTemplate.Height = 24;
            this.dgvBackpack.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBackpack.Size = new System.Drawing.Size(984, 399);
            this.dgvBackpack.TabIndex = 32;
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
            // clmAptNum
            // 
            this.clmAptNum.HeaderText = "Apt. Num.";
            this.clmAptNum.Name = "clmAptNum";
            this.clmAptNum.ReadOnly = true;
            this.clmAptNum.Width = 60;
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
            // clmBackpack
            // 
            this.clmBackpack.HeaderText = "Backpack";
            this.clmBackpack.Name = "clmBackpack";
            this.clmBackpack.ReadOnly = true;
            this.clmBackpack.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.clmBackpack.Visible = false;
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
            this.btnRefreshList.Location = new System.Drawing.Point(25, 91);
            this.btnRefreshList.Name = "btnRefreshList";
            this.btnRefreshList.Size = new System.Drawing.Size(122, 27);
            this.btnRefreshList.TabIndex = 33;
            this.btnRefreshList.Text = "Load Period";
            this.btnRefreshList.UseVisualStyleBackColor = true;
            this.btnRefreshList.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(12, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(150, 32);
            this.label7.TabIndex = 83;
            this.label7.Text = "Backpack Period";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.Color.Cornsilk;
            this.toolStrip1.Enabled = false;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbEditExp,
            this.toolStripSeparator3,
            this.tbsGiveSvc,
            this.toolStripSeparator4,
            this.tbsDeleteSvc,
            this.toolStripSeparator5,
            this.tbsMarkNewBackpack,
            this.toolStripSeparator6,
            this.tbsPrintPickList,
            this.toolStripSeparator7});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(984, 60);
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
            this.tsbEditExp.Size = new System.Drawing.Size(90, 60);
            this.tsbEditExp.Text = "&Edit Expiration";
            this.tsbEditExp.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.tsbEditExp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbEditExp.Click += new System.EventHandler(this.EditExpiration_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 60);
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
            this.tbsGiveSvc.Size = new System.Drawing.Size(90, 60);
            this.tbsGiveSvc.Text = "&Give Service";
            this.tbsGiveSvc.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.tbsGiveSvc.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbsGiveSvc.Click += new System.EventHandler(this.GiveBackpackService_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 60);
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
            this.tbsDeleteSvc.Size = new System.Drawing.Size(90, 60);
            this.tbsDeleteSvc.Text = "&Delete Service";
            this.tbsDeleteSvc.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.tbsDeleteSvc.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbsDeleteSvc.Click += new System.EventHandler(this.DeleteService_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 60);
            // 
            // tbsMarkNewBackpack
            // 
            this.tbsMarkNewBackpack.AutoSize = false;
            this.tbsMarkNewBackpack.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.tbsMarkNewBackpack.Image = ((System.Drawing.Image)(resources.GetObject("tbsMarkNewBackpack.Image")));
            this.tbsMarkNewBackpack.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.tbsMarkNewBackpack.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tbsMarkNewBackpack.ImageTransparentColor = System.Drawing.Color.Red;
            this.tbsMarkNewBackpack.Name = "tbsMarkNewBackpack";
            this.tbsMarkNewBackpack.Size = new System.Drawing.Size(95, 60);
            this.tbsMarkNewBackpack.Text = "&New Backpack Client";
            this.tbsMarkNewBackpack.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.tbsMarkNewBackpack.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 60);
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
            this.tbsPrintPickList.Size = new System.Drawing.Size(90, 60);
            this.tbsPrintPickList.Text = "&Print Picklist";
            this.tbsPrintPickList.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.tbsPrintPickList.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbsPrintPickList.Click += new System.EventHandler(this.PrintPicketlist_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 60);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 84);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.gbOrderBy);
            this.splitContainer1.Panel1.Controls.Add(this.gbNewService);
            this.splitContainer1.Panel1.Controls.Add(this.gbFilter);
            this.splitContainer1.Panel1.Controls.Add(this.gbRenewExpDate);
            this.splitContainer1.Panel1.Controls.Add(this.label7);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.cboYear);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.btnRefreshList);
            this.splitContainer1.Panel1.Controls.Add(this.cboMonth);
            this.splitContainer1.Panel1.Controls.Add(this.progressBar1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvBackpack);
            this.splitContainer1.Size = new System.Drawing.Size(984, 598);
            this.splitContainer1.SplitterDistance = 195;
            this.splitContainer1.TabIndex = 85;
            // 
            // gbOrderBy
            // 
            this.gbOrderBy.Controls.Add(this.label10);
            this.gbOrderBy.Controls.Add(this.cboOrderBy2);
            this.gbOrderBy.Controls.Add(this.label1);
            this.gbOrderBy.Controls.Add(this.label9);
            this.gbOrderBy.Controls.Add(this.cboOrderBy1);
            this.gbOrderBy.Controls.Add(this.label4);
            this.gbOrderBy.Controls.Add(this.tbFindName);
            this.gbOrderBy.Controls.Add(this.cboOrderBy0);
            this.gbOrderBy.Location = new System.Drawing.Point(184, 8);
            this.gbOrderBy.Name = "gbOrderBy";
            this.gbOrderBy.Size = new System.Drawing.Size(344, 108);
            this.gbOrderBy.TabIndex = 88;
            this.gbOrderBy.TabStop = false;
            this.gbOrderBy.Text = "Sort Order";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(3, 79);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(33, 13);
            this.label10.TabIndex = 81;
            this.label10.Tag = "SortOrder";
            this.label10.Text = "Fld 3:";
            // 
            // cboOrderBy2
            // 
            this.cboOrderBy2.BackColor = System.Drawing.Color.OldLace;
            this.cboOrderBy2.DropDownHeight = 225;
            this.cboOrderBy2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboOrderBy2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboOrderBy2.ForeColor = System.Drawing.Color.Black;
            this.cboOrderBy2.FormattingEnabled = true;
            this.cboOrderBy2.IntegralHeight = false;
            this.cboOrderBy2.Items.AddRange(new object[] {
            "Last, First Name",
            "FirstName, LastName",
            "DistributionMethod",
            "HouseholdID",
            "Expiration Date",
            "Address"});
            this.cboOrderBy2.Location = new System.Drawing.Point(41, 76);
            this.cboOrderBy2.Margin = new System.Windows.Forms.Padding(4);
            this.cboOrderBy2.Name = "cboOrderBy2";
            this.cboOrderBy2.Size = new System.Drawing.Size(151, 23);
            this.cboOrderBy2.TabIndex = 82;
            this.cboOrderBy2.Tag = "2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(203, 2);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 17);
            this.label1.TabIndex = 79;
            this.label1.Tag = "";
            this.label1.Text = "Search Field:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(3, 52);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(33, 13);
            this.label9.TabIndex = 79;
            this.label9.Tag = "SortOrder";
            this.label9.Text = "Fld 2:";
            // 
            // cboOrderBy1
            // 
            this.cboOrderBy1.BackColor = System.Drawing.Color.OldLace;
            this.cboOrderBy1.DropDownHeight = 225;
            this.cboOrderBy1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboOrderBy1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboOrderBy1.ForeColor = System.Drawing.Color.Black;
            this.cboOrderBy1.FormattingEnabled = true;
            this.cboOrderBy1.IntegralHeight = false;
            this.cboOrderBy1.Items.AddRange(new object[] {
            "Last, First Name",
            "FirstName, LastName",
            "DistributionMethod",
            "HouseholdID",
            "Expiration Date",
            "Address"});
            this.cboOrderBy1.Location = new System.Drawing.Point(41, 49);
            this.cboOrderBy1.Margin = new System.Windows.Forms.Padding(4);
            this.cboOrderBy1.Name = "cboOrderBy1";
            this.cboOrderBy1.Size = new System.Drawing.Size(151, 23);
            this.cboOrderBy1.TabIndex = 80;
            this.cboOrderBy1.Tag = "1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 26);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 13);
            this.label4.TabIndex = 77;
            this.label4.Tag = "SortOrder";
            this.label4.Text = "Fld 1:";
            // 
            // tbFindName
            // 
            this.tbFindName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.tbFindName.HideSelection = false;
            this.tbFindName.Location = new System.Drawing.Point(195, 22);
            this.tbFindName.Margin = new System.Windows.Forms.Padding(4);
            this.tbFindName.Name = "tbFindName";
            this.tbFindName.Size = new System.Drawing.Size(146, 24);
            this.tbFindName.TabIndex = 70;
            this.tbFindName.WordWrap = false;
            // 
            // cboOrderBy0
            // 
            this.cboOrderBy0.BackColor = System.Drawing.Color.OldLace;
            this.cboOrderBy0.DropDownHeight = 225;
            this.cboOrderBy0.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboOrderBy0.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboOrderBy0.ForeColor = System.Drawing.Color.Black;
            this.cboOrderBy0.FormattingEnabled = true;
            this.cboOrderBy0.IntegralHeight = false;
            this.cboOrderBy0.Items.AddRange(new object[] {
            "Last, First Name",
            "FirstName, LastName",
            "DistributionMethod",
            "HouseholdID",
            "Expiration Date",
            "Address"});
            this.cboOrderBy0.Location = new System.Drawing.Point(41, 23);
            this.cboOrderBy0.Margin = new System.Windows.Forms.Padding(4);
            this.cboOrderBy0.Name = "cboOrderBy0";
            this.cboOrderBy0.Size = new System.Drawing.Size(151, 23);
            this.cboOrderBy0.TabIndex = 78;
            this.cboOrderBy0.Tag = "0";
            // 
            // gbNewService
            // 
            this.gbNewService.Controls.Add(this.dtpSvcDate);
            this.gbNewService.Controls.Add(this.tbLbsBackpack);
            this.gbNewService.Controls.Add(this.label6);
            this.gbNewService.Controls.Add(this.btnSvcCancel);
            this.gbNewService.Controls.Add(this.btnSvcSave);
            this.gbNewService.Controls.Add(this.lblService);
            this.gbNewService.Location = new System.Drawing.Point(535, 7);
            this.gbNewService.Name = "gbNewService";
            this.gbNewService.Size = new System.Drawing.Size(309, 109);
            this.gbNewService.TabIndex = 87;
            this.gbNewService.TabStop = false;
            this.gbNewService.Visible = false;
            // 
            // dtpSvcDate
            // 
            this.dtpSvcDate.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpSvcDate.Location = new System.Drawing.Point(6, 30);
            this.dtpSvcDate.Name = "dtpSvcDate";
            this.dtpSvcDate.Size = new System.Drawing.Size(293, 23);
            this.dtpSvcDate.TabIndex = 110;
            // 
            // tbLbsBackpack
            // 
            this.tbLbsBackpack.Location = new System.Drawing.Point(95, 73);
            this.tbLbsBackpack.Name = "tbLbsBackpack";
            this.tbLbsBackpack.Size = new System.Drawing.Size(60, 23);
            this.tbLbsBackpack.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(9, 76);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 15);
            this.label6.TabIndex = 5;
            this.label6.Text = "Lbs Backpack";
            // 
            // btnSvcCancel
            // 
            this.btnSvcCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSvcCancel.Location = new System.Drawing.Point(239, 69);
            this.btnSvcCancel.Name = "btnSvcCancel";
            this.btnSvcCancel.Size = new System.Drawing.Size(60, 30);
            this.btnSvcCancel.TabIndex = 4;
            this.btnSvcCancel.Text = "Cancel";
            this.btnSvcCancel.UseVisualStyleBackColor = true;
            // 
            // btnSvcSave
            // 
            this.btnSvcSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSvcSave.Location = new System.Drawing.Point(164, 69);
            this.btnSvcSave.Name = "btnSvcSave";
            this.btnSvcSave.Size = new System.Drawing.Size(60, 30);
            this.btnSvcSave.TabIndex = 3;
            this.btnSvcSave.Text = "Save";
            this.btnSvcSave.UseVisualStyleBackColor = true;
            // 
            // lblService
            // 
            this.lblService.AutoSize = true;
            this.lblService.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblService.Location = new System.Drawing.Point(9, 12);
            this.lblService.Name = "lblService";
            this.lblService.Size = new System.Drawing.Size(105, 17);
            this.lblService.TabIndex = 109;
            this.lblService.Text = "Date of Service";
            this.lblService.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // gbFilter
            // 
            this.gbFilter.Controls.Add(this.cboFilterFld);
            this.gbFilter.Controls.Add(this.label8);
            this.gbFilter.Controls.Add(this.cboFilter);
            this.gbFilter.Controls.Add(this.lblFilterBy);
            this.gbFilter.Location = new System.Drawing.Point(8, 122);
            this.gbFilter.Name = "gbFilter";
            this.gbFilter.Size = new System.Drawing.Size(413, 70);
            this.gbFilter.TabIndex = 85;
            this.gbFilter.TabStop = false;
            this.gbFilter.Visible = false;
            // 
            // cboFilterFld
            // 
            this.cboFilterFld.BackColor = System.Drawing.Color.OldLace;
            this.cboFilterFld.DropDownHeight = 225;
            this.cboFilterFld.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFilterFld.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboFilterFld.ForeColor = System.Drawing.Color.Black;
            this.cboFilterFld.FormattingEnabled = true;
            this.cboFilterFld.IntegralHeight = false;
            this.cboFilterFld.Items.AddRange(new object[] {
            "<None>",
            "Distribution Method",
            "Address",
            "Expiration Date"});
            this.cboFilterFld.Location = new System.Drawing.Point(11, 40);
            this.cboFilterFld.Margin = new System.Windows.Forms.Padding(4);
            this.cboFilterFld.Name = "cboFilterFld";
            this.cboFilterFld.Size = new System.Drawing.Size(148, 23);
            this.cboFilterFld.TabIndex = 83;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(5, 21);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 17);
            this.label8.TabIndex = 82;
            this.label8.Tag = "SortOrder";
            this.label8.Text = "Filter Field:";
            // 
            // cboFilter
            // 
            this.cboFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFilter.FormattingEnabled = true;
            this.cboFilter.Location = new System.Drawing.Point(165, 40);
            this.cboFilter.Margin = new System.Windows.Forms.Padding(4);
            this.cboFilter.Name = "cboFilter";
            this.cboFilter.Size = new System.Drawing.Size(237, 24);
            this.cboFilter.Sorted = true;
            this.cboFilter.TabIndex = 73;
            this.cboFilter.Visible = false;
            // 
            // lblFilterBy
            // 
            this.lblFilterBy.AutoSize = true;
            this.lblFilterBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilterBy.Location = new System.Drawing.Point(165, 22);
            this.lblFilterBy.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFilterBy.Name = "lblFilterBy";
            this.lblFilterBy.Size = new System.Drawing.Size(99, 17);
            this.lblFilterBy.TabIndex = 72;
            this.lblFilterBy.Tag = "SortOrder";
            this.lblFilterBy.Text = "Filtered Value:";
            // 
            // gbRenewExpDate
            // 
            this.gbRenewExpDate.Controls.Add(this.btnAdd6Months);
            this.gbRenewExpDate.Controls.Add(this.btnCancel);
            this.gbRenewExpDate.Controls.Add(this.btnSave);
            this.gbRenewExpDate.Controls.Add(this.chkBackpack);
            this.gbRenewExpDate.Controls.Add(this.dtpExpDate);
            this.gbRenewExpDate.Controls.Add(this.label5);
            this.gbRenewExpDate.Location = new System.Drawing.Point(852, 7);
            this.gbRenewExpDate.Name = "gbRenewExpDate";
            this.gbRenewExpDate.Size = new System.Drawing.Size(270, 105);
            this.gbRenewExpDate.TabIndex = 86;
            this.gbRenewExpDate.TabStop = false;
            this.gbRenewExpDate.Visible = false;
            // 
            // btnAdd6Months
            // 
            this.btnAdd6Months.Location = new System.Drawing.Point(144, 36);
            this.btnAdd6Months.Name = "btnAdd6Months";
            this.btnAdd6Months.Size = new System.Drawing.Size(112, 24);
            this.btnAdd6Months.TabIndex = 5;
            this.btnAdd6Months.Text = "Add 6 Months";
            this.btnAdd6Months.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(205, 64);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(60, 30);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(139, 64);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(60, 30);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // chkBackpack
            // 
            this.chkBackpack.AutoSize = true;
            this.chkBackpack.Location = new System.Drawing.Point(6, 16);
            this.chkBackpack.Name = "chkBackpack";
            this.chkBackpack.Size = new System.Drawing.Size(75, 17);
            this.chkBackpack.TabIndex = 2;
            this.chkBackpack.Text = "Backback";
            this.chkBackpack.UseVisualStyleBackColor = true;
            // 
            // dtpExpDate
            // 
            this.dtpExpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpExpDate.Location = new System.Drawing.Point(6, 71);
            this.dtpExpDate.Name = "dtpExpDate";
            this.dtpExpDate.Size = new System.Drawing.Size(127, 23);
            this.dtpExpDate.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(108, 17);
            this.label5.TabIndex = 0;
            this.label5.Text = "Expiration Date:";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(7, 92);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(4);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(169, 28);
            this.progressBar1.Step = 1;
            this.progressBar1.TabIndex = 89;
            this.progressBar1.Value = 1;
            this.progressBar1.Visible = false;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "HHID";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.dataGridViewTextBoxColumn1.Width = 60;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Family Member Name";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.dataGridViewTextBoxColumn2.Width = 200;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Address";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.dataGridViewTextBoxColumn3.Width = 200;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Exp. Date";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Method";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.dataGridViewTextBoxColumn5.Width = 120;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "Column1";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.Visible = false;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "Date Served";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.HeaderText = "Lbs";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.dataGridViewTextBoxColumn8.Width = 90;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.HeaderText = "HHMemID";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.dataGridViewTextBoxColumn9.Width = 80;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.HeaderText = "NameFL";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.dataGridViewTextBoxColumn10.Visible = false;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.HeaderText = "NameLF";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.dataGridViewTextBoxColumn11.Visible = false;
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.HeaderText = "Backpack";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.dataGridViewTextBoxColumn12.Visible = false;
            // 
            // dataGridViewTextBoxColumn13
            // 
            this.dataGridViewTextBoxColumn13.HeaderText = "ID";
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            this.dataGridViewTextBoxColumn13.Visible = false;
            // 
            // EditBackpackForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Cornsilk;
            this.ClientSize = new System.Drawing.Size(984, 682);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "EditBackpackForm";
            this.Text = "Backpack Maintenance Form";
            this.Load += new System.EventHandler(this.EditBackpackForm_Load);
            this.Resize += new System.EventHandler(this.EditBackpackForm_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBackpack)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.gbOrderBy.ResumeLayout(false);
            this.gbOrderBy.PerformLayout();
            this.gbNewService.ResumeLayout(false);
            this.gbNewService.PerformLayout();
            this.gbFilter.ResumeLayout(false);
            this.gbFilter.PerformLayout();
            this.gbRenewExpDate.ResumeLayout(false);
            this.gbRenewExpDate.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuActions;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditExpiration;
        private System.Windows.Forms.ToolStripMenuItem tsmiMarkNewBackpackClient;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmiGiveBackpackService;
        private System.Windows.Forms.ToolStripMenuItem tsmiPrintPicketlist;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ComboBox cboMonth;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboYear;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgvBackpack;
        private System.Windows.Forms.Button btnRefreshList;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ToolStripMenuItem menuHelp;
        private System.Windows.Forms.ToolStripMenuItem menuBackpackFederal;
        private System.Windows.Forms.ToolStripMenuItem menuFNSWebSite;
        private System.Windows.Forms.ToolStripMenuItem menuBackpackState;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeleteService;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbEditExp;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tbsGiveSvc;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton tbsDeleteSvc;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton tbsMarkNewBackpack;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton tbsPrintPickList;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmHHID;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmAptNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmExpiration;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmMethod;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmMethodAsInt;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmDateServed;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmLbs;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmHHMemID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNameFL;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNameLF;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmBackpack;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmLogID;
        private System.Windows.Forms.GroupBox gbOrderBy;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cboOrderBy2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cboOrderBy1;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox tbFindName;
        private System.Windows.Forms.ComboBox cboOrderBy0;
        private System.Windows.Forms.GroupBox gbNewService;
        private System.Windows.Forms.DateTimePicker dtpSvcDate;
        private System.Windows.Forms.TextBox tbLbsBackpack;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnSvcCancel;
        private System.Windows.Forms.Button btnSvcSave;
        private System.Windows.Forms.Label lblService;
        private System.Windows.Forms.GroupBox gbFilter;
        private System.Windows.Forms.ComboBox cboFilterFld;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cboFilter;
        private System.Windows.Forms.Label lblFilterBy;
        private System.Windows.Forms.GroupBox gbRenewExpDate;
        private System.Windows.Forms.Button btnAdd6Months;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.CheckBox chkBackpack;
        private System.Windows.Forms.DateTimePicker dtpExpDate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}