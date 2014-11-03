namespace ClientcardFB3
{
    partial class FindClientForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tbID = new System.Windows.Forms.TextBox();
            this.tbName = new System.Windows.Forms.TextBox();
            this.tbAddress = new System.Windows.Forms.TextBox();
            this.tbFindName = new System.Windows.Forms.TextBox();
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.bntAddNewClient = new System.Windows.Forms.Button();
            this.lblFilterBy = new System.Windows.Forms.Label();
            this.chkIncludeInactive = new System.Windows.Forms.CheckBox();
            this.cboFilter = new System.Windows.Forms.ComboBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.label20 = new System.Windows.Forms.Label();
            this.tbLastService = new System.Windows.Forms.TextBox();
            this.dgvClientList = new System.Windows.Forms.DataGridView();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmCity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmZip = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmHHID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmPhone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmHeadHH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmClientType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmLatestService = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHHName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNameFL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNameLF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmsFindClient = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiExportToExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.cboOrderBy = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblPos = new System.Windows.Forms.Label();
            this.lblInactiveHH = new System.Windows.Forms.Label();
            this.lblInactiveHHMem = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientList)).BeginInit();
            this.cmsFindClient.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbID
            // 
            this.tbID.BackColor = System.Drawing.Color.Beige;
            this.tbID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.tbID.ForeColor = System.Drawing.Color.DarkBlue;
            this.tbID.Location = new System.Drawing.Point(17, 6);
            this.tbID.Margin = new System.Windows.Forms.Padding(4);
            this.tbID.Name = "tbID";
            this.tbID.ReadOnly = true;
            this.tbID.Size = new System.Drawing.Size(96, 23);
            this.tbID.TabIndex = 0;
            this.tbID.TabStop = false;
            this.tbID.Tag = "ID";
            this.tbID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbName
            // 
            this.tbName.BackColor = System.Drawing.Color.Beige;
            this.tbName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.tbName.ForeColor = System.Drawing.Color.DarkBlue;
            this.tbName.Location = new System.Drawing.Point(121, 6);
            this.tbName.Margin = new System.Windows.Forms.Padding(4);
            this.tbName.Name = "tbName";
            this.tbName.ReadOnly = true;
            this.tbName.Size = new System.Drawing.Size(380, 23);
            this.tbName.TabIndex = 1;
            this.tbName.TabStop = false;
            this.tbName.Tag = "Name";
            // 
            // tbAddress
            // 
            this.tbAddress.BackColor = System.Drawing.Color.Beige;
            this.tbAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.tbAddress.ForeColor = System.Drawing.Color.DarkBlue;
            this.tbAddress.Location = new System.Drawing.Point(121, 35);
            this.tbAddress.Margin = new System.Windows.Forms.Padding(4);
            this.tbAddress.Multiline = true;
            this.tbAddress.Name = "tbAddress";
            this.tbAddress.ReadOnly = true;
            this.tbAddress.Size = new System.Drawing.Size(380, 47);
            this.tbAddress.TabIndex = 2;
            this.tbAddress.TabStop = false;
            this.tbAddress.Tag = "Address";
            // 
            // tbFindName
            // 
            this.tbFindName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.tbFindName.HideSelection = false;
            this.tbFindName.Location = new System.Drawing.Point(16, 113);
            this.tbFindName.Margin = new System.Windows.Forms.Padding(4);
            this.tbFindName.Name = "tbFindName";
            this.tbFindName.Size = new System.Drawing.Size(370, 24);
            this.tbFindName.TabIndex = 3;
            this.tbFindName.WordWrap = false;
            this.tbFindName.TextChanged += new System.EventHandler(this.tbFindName_TextChanged);
            this.tbFindName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbFindName_KeyDown);
            // 
            // btnSelect
            // 
            this.btnSelect.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnSelect.Location = new System.Drawing.Point(509, 6);
            this.btnSelect.Margin = new System.Windows.Forms.Padding(4);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(115, 32);
            this.btnSelect.TabIndex = 4;
            this.btnSelect.Text = "&Select";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnCancel.Location = new System.Drawing.Point(843, 6);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(150, 32);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // bntAddNewClient
            // 
            this.bntAddNewClient.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.bntAddNewClient.Location = new System.Drawing.Point(843, 40);
            this.bntAddNewClient.Margin = new System.Windows.Forms.Padding(4);
            this.bntAddNewClient.Name = "bntAddNewClient";
            this.bntAddNewClient.Size = new System.Drawing.Size(150, 32);
            this.bntAddNewClient.TabIndex = 6;
            this.bntAddNewClient.Text = "&Add New Client";
            this.bntAddNewClient.UseVisualStyleBackColor = true;
            this.bntAddNewClient.Click += new System.EventHandler(this.bntAddNewClient_Click);
            // 
            // lblFilterBy
            // 
            this.lblFilterBy.AutoSize = true;
            this.lblFilterBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilterBy.Location = new System.Drawing.Point(645, 91);
            this.lblFilterBy.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFilterBy.Name = "lblFilterBy";
            this.lblFilterBy.Size = new System.Drawing.Size(79, 17);
            this.lblFilterBy.TabIndex = 9;
            this.lblFilterBy.Tag = "SortOrder";
            this.lblFilterBy.Text = "Filtered By:";
            // 
            // chkIncludeInactive
            // 
            this.chkIncludeInactive.AutoSize = true;
            this.chkIncludeInactive.Location = new System.Drawing.Point(866, 80);
            this.chkIncludeInactive.Margin = new System.Windows.Forms.Padding(4);
            this.chkIncludeInactive.Name = "chkIncludeInactive";
            this.chkIncludeInactive.Size = new System.Drawing.Size(102, 17);
            this.chkIncludeInactive.TabIndex = 16;
            this.chkIncludeInactive.Text = "Include Inactive";
            this.chkIncludeInactive.UseVisualStyleBackColor = true;
            this.chkIncludeInactive.CheckStateChanged += new System.EventHandler(this.chkIncludeInactive_CheckStateChanged);
            // 
            // cboFilter
            // 
            this.cboFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFilter.FormattingEnabled = true;
            this.cboFilter.Location = new System.Drawing.Point(644, 112);
            this.cboFilter.Margin = new System.Windows.Forms.Padding(4);
            this.cboFilter.Name = "cboFilter";
            this.cboFilter.Size = new System.Drawing.Size(191, 21);
            this.cboFilter.TabIndex = 17;
            this.cboFilter.Visible = false;
            this.cboFilter.SelectedIndexChanged += new System.EventHandler(this.cboFilter_SelectedIndexChanged);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(720, 6);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(4);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(115, 32);
            this.btnRefresh.TabIndex = 19;
            this.btnRefresh.Text = "Refresh List";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // label20
            // 
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(510, 39);
            this.label20.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(114, 18);
            this.label20.TabIndex = 60;
            this.label20.Text = "Last Service";
            this.label20.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // tbLastService
            // 
            this.tbLastService.BackColor = System.Drawing.Color.Beige;
            this.tbLastService.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbLastService.ForeColor = System.Drawing.Color.DarkBlue;
            this.tbLastService.Location = new System.Drawing.Point(510, 56);
            this.tbLastService.Margin = new System.Windows.Forms.Padding(5);
            this.tbLastService.Name = "tbLastService";
            this.tbLastService.Size = new System.Drawing.Size(114, 23);
            this.tbLastService.TabIndex = 59;
            this.tbLastService.TabStop = false;
            this.tbLastService.Tag = "LatestService";
            this.tbLastService.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // dgvClientList
            // 
            this.dgvClientList.AllowUserToAddRows = false;
            this.dgvClientList.AllowUserToDeleteRows = false;
            this.dgvClientList.AllowUserToResizeRows = false;
            this.dgvClientList.BackgroundColor = System.Drawing.Color.Cornsilk;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.PaleGoldenrod;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Peru;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvClientList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvClientList.ColumnHeadersHeight = 28;
            this.dgvClientList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvClientList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colName,
            this.clmAddress,
            this.clmCity,
            this.clmZip,
            this.clmHHID,
            this.clmPhone,
            this.clmHeadHH,
            this.clmClientType,
            this.clmLatestService,
            this.clmID,
            this.colHHName,
            this.colNameFL,
            this.colNameLF});
            this.dgvClientList.ContextMenuStrip = this.cmsFindClient;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.DarkViolet;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvClientList.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvClientList.Location = new System.Drawing.Point(2, 150);
            this.dgvClientList.Margin = new System.Windows.Forms.Padding(4);
            this.dgvClientList.MultiSelect = false;
            this.dgvClientList.Name = "dgvClientList";
            this.dgvClientList.ReadOnly = true;
            this.dgvClientList.RowHeadersWidth = 20;
            this.dgvClientList.RowTemplate.Height = 24;
            this.dgvClientList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvClientList.Size = new System.Drawing.Size(998, 573);
            this.dgvClientList.TabIndex = 61;
            this.dgvClientList.Tag = "";
            this.dgvClientList.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewClients_RowEnter);
            this.dgvClientList.DoubleClick += new System.EventHandler(this.dataGridViewClients_DoubleClick);
            this.dgvClientList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvClientList_KeyDown);
            // 
            // colName
            // 
            this.colName.HeaderText = "Client Name";
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            this.colName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colName.Width = 200;
            // 
            // clmAddress
            // 
            this.clmAddress.HeaderText = "Address";
            this.clmAddress.Name = "clmAddress";
            this.clmAddress.ReadOnly = true;
            this.clmAddress.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // clmCity
            // 
            this.clmCity.HeaderText = "City";
            this.clmCity.Name = "clmCity";
            this.clmCity.ReadOnly = true;
            this.clmCity.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // clmZip
            // 
            this.clmZip.HeaderText = "Zipcode";
            this.clmZip.Name = "clmZip";
            this.clmZip.ReadOnly = true;
            this.clmZip.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.clmZip.Width = 70;
            // 
            // clmHHID
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Format = "N0";
            dataGridViewCellStyle2.NullValue = null;
            this.clmHHID.DefaultCellStyle = dataGridViewCellStyle2;
            this.clmHHID.HeaderText = "HHID";
            this.clmHHID.Name = "clmHHID";
            this.clmHHID.ReadOnly = true;
            this.clmHHID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.clmHHID.Width = 70;
            // 
            // clmPhone
            // 
            this.clmPhone.HeaderText = "Phone";
            this.clmPhone.Name = "clmPhone";
            this.clmPhone.ReadOnly = true;
            this.clmPhone.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // clmHeadHH
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.clmHeadHH.DefaultCellStyle = dataGridViewCellStyle3;
            this.clmHeadHH.HeaderText = "HeadHH";
            this.clmHeadHH.Name = "clmHeadHH";
            this.clmHeadHH.ReadOnly = true;
            this.clmHeadHH.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.clmHeadHH.Width = 70;
            // 
            // clmClientType
            // 
            this.clmClientType.HeaderText = "Client Type";
            this.clmClientType.Name = "clmClientType";
            this.clmClientType.ReadOnly = true;
            // 
            // clmLatestService
            // 
            dataGridViewCellStyle4.Format = "d";
            dataGridViewCellStyle4.NullValue = null;
            this.clmLatestService.DefaultCellStyle = dataGridViewCellStyle4;
            this.clmLatestService.HeaderText = "Last Service";
            this.clmLatestService.Name = "clmLatestService";
            this.clmLatestService.ReadOnly = true;
            this.clmLatestService.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // clmID
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.Format = "N0";
            dataGridViewCellStyle5.NullValue = null;
            this.clmID.DefaultCellStyle = dataGridViewCellStyle5;
            this.clmID.HeaderText = "MemberID";
            this.clmID.Name = "clmID";
            this.clmID.ReadOnly = true;
            this.clmID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.clmID.Width = 80;
            // 
            // colHHName
            // 
            this.colHHName.HeaderText = "HH Name";
            this.colHHName.Name = "colHHName";
            this.colHHName.ReadOnly = true;
            this.colHHName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colNameFL
            // 
            this.colNameFL.HeaderText = "NameFL";
            this.colNameFL.Name = "colNameFL";
            this.colNameFL.ReadOnly = true;
            this.colNameFL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colNameFL.Visible = false;
            // 
            // colNameLF
            // 
            this.colNameLF.HeaderText = "NameLF";
            this.colNameLF.Name = "colNameLF";
            this.colNameLF.ReadOnly = true;
            this.colNameLF.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colNameLF.Visible = false;
            // 
            // cmsFindClient
            // 
            this.cmsFindClient.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiExportToExcel});
            this.cmsFindClient.Name = "cmsFindClient";
            this.cmsFindClient.Size = new System.Drawing.Size(154, 26);
            this.cmsFindClient.Opening += new System.ComponentModel.CancelEventHandler(this.cmsFindClient_Opening);
            this.cmsFindClient.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.cmsFindClient_ItemClicked);
            // 
            // tsmiExportToExcel
            // 
            this.tsmiExportToExcel.Name = "tsmiExportToExcel";
            this.tsmiExportToExcel.Size = new System.Drawing.Size(153, 22);
            this.tsmiExportToExcel.Text = "Export To Excel";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(16, 111);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(4);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(380, 28);
            this.progressBar1.Step = 1;
            this.progressBar1.TabIndex = 62;
            this.progressBar1.Value = 1;
            this.progressBar1.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(401, 92);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 17);
            this.label1.TabIndex = 64;
            this.label1.Tag = "SortOrder";
            this.label1.Text = "Order By:";
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
            "First Name Last",
            "Address",
            "City",
            "Zip",
            "Phone",
            "Household ID",
            "Client Category"});
            this.cboOrderBy.Location = new System.Drawing.Point(404, 113);
            this.cboOrderBy.Margin = new System.Windows.Forms.Padding(4);
            this.cboOrderBy.Name = "cboOrderBy";
            this.cboOrderBy.Size = new System.Drawing.Size(220, 23);
            this.cboOrderBy.TabIndex = 65;
            this.cboOrderBy.SelectedIndexChanged += new System.EventHandler(this.cboOrderBy_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(18, 89);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 17);
            this.label2.TabIndex = 66;
            this.label2.Tag = "";
            this.label2.Text = "Search Field:";
            // 
            // lblPos
            // 
            this.lblPos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPos.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblPos.Location = new System.Drawing.Point(0, 50);
            this.lblPos.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPos.Name = "lblPos";
            this.lblPos.Size = new System.Drawing.Size(120, 20);
            this.lblPos.TabIndex = 67;
            this.lblPos.Tag = "";
            this.lblPos.Text = "5555 of 9856";
            this.lblPos.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // lblInactiveHH
            // 
            this.lblInactiveHH.AutoSize = true;
            this.lblInactiveHH.ForeColor = System.Drawing.Color.DarkViolet;
            this.lblInactiveHH.Location = new System.Drawing.Point(863, 101);
            this.lblInactiveHH.Name = "lblInactiveHH";
            this.lblInactiveHH.Size = new System.Drawing.Size(110, 13);
            this.lblInactiveHH.TabIndex = 68;
            this.lblInactiveHH.Text = "Inactive HH Members";
            // 
            // lblInactiveHHMem
            // 
            this.lblInactiveHHMem.AutoSize = true;
            this.lblInactiveHHMem.ForeColor = System.Drawing.Color.Maroon;
            this.lblInactiveHHMem.Location = new System.Drawing.Point(863, 116);
            this.lblInactiveHHMem.Name = "lblInactiveHHMem";
            this.lblInactiveHHMem.Size = new System.Drawing.Size(99, 13);
            this.lblInactiveHHMem.TabIndex = 69;
            this.lblInactiveHHMem.Text = "Inactive Household";
            // 
            // FindClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Cornsilk;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(1006, 725);
            this.Controls.Add(this.lblInactiveHHMem);
            this.Controls.Add(this.lblInactiveHH);
            this.Controls.Add(this.lblPos);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboOrderBy);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvClientList);
            this.Controls.Add(this.tbLastService);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.chkIncludeInactive);
            this.Controls.Add(this.lblFilterBy);
            this.Controls.Add(this.bntAddNewClient);
            this.Controls.Add(this.cboFilter);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.tbAddress);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.tbID);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.tbFindName);
            this.Controls.Add(this.progressBar1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FindClientForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "";
            this.Text = "Find Client Household";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FindClientForm_FormClosing);
            this.VisibleChanged += new System.EventHandler(this.FindClientForm_VisibleChanged);
            this.Resize += new System.EventHandler(this.FindClientForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientList)).EndInit();
            this.cmsFindClient.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbID;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.TextBox tbAddress;
        public System.Windows.Forms.TextBox tbFindName;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button bntAddNewClient;
        private System.Windows.Forms.Label lblFilterBy;
        private System.Windows.Forms.CheckBox chkIncludeInactive;
        private System.Windows.Forms.ComboBox cboFilter;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox tbLastService;
        public System.Windows.Forms.DataGridView dgvClientList;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboOrderBy;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblPos;
        private System.Windows.Forms.Label lblInactiveHH;
        private System.Windows.Forms.Label lblInactiveHHMem;
        private System.Windows.Forms.ContextMenuStrip cmsFindClient;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportToExcel;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmCity;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmZip;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmHHID;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmPhone;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmHeadHH;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmClientType;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmLatestService;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHHName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNameFL;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNameLF;
    }
}