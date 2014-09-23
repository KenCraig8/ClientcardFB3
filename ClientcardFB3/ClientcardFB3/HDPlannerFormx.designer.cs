namespace ClientcardFB3
{
    partial class HDPlannerForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HDPlannerForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvHD = new System.Windows.Forms.DataGridView();
            this.clmSel = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.clmRouteID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmRouteTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmApt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmPhone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmFamilySize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmComments = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmDriverNotes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmSvcItem = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.clmLastSvc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.cboOrderBy = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblFilterBy = new System.Windows.Forms.Label();
            this.cboFilter = new System.Windows.Forms.ComboBox();
            this.tbFindName = new System.Windows.Forms.TextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btnSaveRoute = new System.Windows.Forms.Button();
            this.btnSvcSave = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.tsbAddress = new System.Windows.Forms.ToolStripButton();
            this.tsbApt = new System.Windows.Forms.ToolStripButton();
            this.tsbPhone = new System.Windows.Forms.ToolStripButton();
            this.tsbSize = new System.Windows.Forms.ToolStripButton();
            this.tsbComments = new System.Windows.Forms.ToolStripButton();
            this.tsbDriverNotes = new System.Windows.Forms.ToolStripButton();
            this.tsbSvcItem = new System.Windows.Forms.ToolStripButton();
            this.tsbLastSvc = new System.Windows.Forms.ToolStripButton();
            this.pnlGiveService = new System.Windows.Forms.Panel();
            this.pnlFindClient = new System.Windows.Forms.Panel();
            this.cboHDRoute = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlRouteInfo = new System.Windows.Forms.Panel();
            this.tbEstMiles = new System.Windows.Forms.TextBox();
            this.tbEstTime = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tbDriverComments = new System.Windows.Forms.TextBox();
            this.tbRouteNotes = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbPhone = new System.Windows.Forms.MaskedTextBox();
            this.lblPhone = new System.Windows.Forms.Label();
            this.btnSelectDriver = new System.Windows.Forms.Button();
            this.tbDriver = new System.Windows.Forms.TextBox();
            this.lblVolunteer = new System.Windows.Forms.Label();
            this.lblRowCnt = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpgReview = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.label9 = new System.Windows.Forms.Label();
            this.lvwStatus = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnLoad = new System.Windows.Forms.Button();
            this.dtpServiceDate = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.chkSelectAll = new System.Windows.Forms.CheckBox();
            this.lvwRouteStatus = new System.Windows.Forms.ListView();
            this.colRouteTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colNbrClients = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colRouteID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.tsbPrepare = new System.Windows.Forms.ToolStripButton();
            this.tsbPrint = new System.Windows.Forms.ToolStripButton();
            this.tsbPost = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbShowDelivDates = new System.Windows.Forms.ToolStripButton();
            this.tpgPlanner = new System.Windows.Forms.TabPage();
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
            ((System.ComponentModel.ISupportInitialize)(this.dgvHD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.pnlGiveService.SuspendLayout();
            this.pnlFindClient.SuspendLayout();
            this.pnlRouteInfo.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpgReview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            this.tpgPlanner.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvHD
            // 
            this.dgvHD.AllowUserToAddRows = false;
            this.dgvHD.AllowUserToDeleteRows = false;
            this.dgvHD.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgvHD.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightBlue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.CornflowerBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvHD.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvHD.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHD.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmSel,
            this.clmRouteID,
            this.clmRouteTitle,
            this.clmID,
            this.clmName,
            this.clmAddress,
            this.clmApt,
            this.clmPhone,
            this.clmFamilySize,
            this.clmComments,
            this.clmDriverNotes,
            this.clmSvcItem,
            this.clmLastSvc});
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.PaleGoldenrod;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvHD.DefaultCellStyle = dataGridViewCellStyle11;
            this.dgvHD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHD.Location = new System.Drawing.Point(0, 0);
            this.dgvHD.Name = "dgvHD";
            this.dgvHD.RowHeadersVisible = false;
            this.dgvHD.RowTemplate.Height = 24;
            this.dgvHD.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHD.Size = new System.Drawing.Size(920, 325);
            this.dgvHD.TabIndex = 32;
            this.dgvHD.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvHD_CellDoubleClick);
            // 
            // clmSel
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle2.NullValue = false;
            this.clmSel.DefaultCellStyle = dataGridViewCellStyle2;
            this.clmSel.Frozen = true;
            this.clmSel.HeaderText = "sel";
            this.clmSel.MinimumWidth = 30;
            this.clmSel.Name = "clmSel";
            this.clmSel.Width = 30;
            // 
            // clmRouteID
            // 
            this.clmRouteID.HeaderText = "RouteID";
            this.clmRouteID.Name = "clmRouteID";
            this.clmRouteID.ReadOnly = true;
            this.clmRouteID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.clmRouteID.Visible = false;
            this.clmRouteID.Width = 60;
            // 
            // clmRouteTitle
            // 
            this.clmRouteTitle.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.clmRouteTitle.DefaultCellStyle = dataGridViewCellStyle3;
            this.clmRouteTitle.HeaderText = "Rt";
            this.clmRouteTitle.Name = "clmRouteTitle";
            this.clmRouteTitle.ReadOnly = true;
            this.clmRouteTitle.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.clmRouteTitle.Visible = false;
            // 
            // clmID
            // 
            this.clmID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.clmID.DefaultCellStyle = dataGridViewCellStyle4;
            this.clmID.HeaderText = "ID";
            this.clmID.Name = "clmID";
            this.clmID.ReadOnly = true;
            this.clmID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.clmID.Width = 46;
            // 
            // clmName
            // 
            this.clmName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.clmName.HeaderText = "Client Name";
            this.clmName.MaxInputLength = 50;
            this.clmName.Name = "clmName";
            this.clmName.ReadOnly = true;
            this.clmName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.clmName.Width = 109;
            // 
            // clmAddress
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.clmAddress.DefaultCellStyle = dataGridViewCellStyle5;
            this.clmAddress.HeaderText = "Address";
            this.clmAddress.MaxInputLength = 50;
            this.clmAddress.Name = "clmAddress";
            this.clmAddress.ReadOnly = true;
            this.clmAddress.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.clmAddress.Width = 180;
            // 
            // clmApt
            // 
            this.clmApt.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.clmApt.DefaultCellStyle = dataGridViewCellStyle6;
            this.clmApt.HeaderText = "Apt";
            this.clmApt.MaxInputLength = 40;
            this.clmApt.Name = "clmApt";
            this.clmApt.ReadOnly = true;
            this.clmApt.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.clmApt.Width = 54;
            // 
            // clmPhone
            // 
            this.clmPhone.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.clmPhone.HeaderText = "Phone";
            this.clmPhone.MaxInputLength = 50;
            this.clmPhone.Name = "clmPhone";
            this.clmPhone.ReadOnly = true;
            this.clmPhone.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.clmPhone.Width = 74;
            // 
            // clmFamilySize
            // 
            this.clmFamilySize.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.clmFamilySize.DefaultCellStyle = dataGridViewCellStyle7;
            this.clmFamilySize.HeaderText = "Size";
            this.clmFamilySize.Name = "clmFamilySize";
            this.clmFamilySize.ReadOnly = true;
            this.clmFamilySize.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.clmFamilySize.Width = 60;
            // 
            // clmComments
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.clmComments.DefaultCellStyle = dataGridViewCellStyle8;
            this.clmComments.HeaderText = "Comments";
            this.clmComments.MaxInputLength = 300;
            this.clmComments.Name = "clmComments";
            this.clmComments.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.clmComments.Width = 160;
            // 
            // clmDriverNotes
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.clmDriverNotes.DefaultCellStyle = dataGridViewCellStyle9;
            this.clmDriverNotes.HeaderText = "Driver Notes";
            this.clmDriverNotes.Name = "clmDriverNotes";
            this.clmDriverNotes.Width = 160;
            // 
            // clmSvcItem
            // 
            this.clmSvcItem.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.clmSvcItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.clmSvcItem.HeaderText = "Svc Item";
            this.clmSvcItem.Name = "clmSvcItem";
            this.clmSvcItem.ReadOnly = true;
            this.clmSvcItem.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clmSvcItem.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.clmSvcItem.Width = 60;
            // 
            // clmLastSvc
            // 
            this.clmLastSvc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.clmLastSvc.DefaultCellStyle = dataGridViewCellStyle10;
            this.clmLastSvc.HeaderText = "Last Svc";
            this.clmLastSvc.MaxInputLength = 10;
            this.clmLastSvc.Name = "clmLastSvc";
            this.clmLastSvc.ReadOnly = true;
            this.clmLastSvc.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.clmLastSvc.Width = 87;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 6);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 15);
            this.label1.TabIndex = 79;
            this.label1.Tag = "";
            this.label1.Text = "Find:";
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
            "ID",
            "Client Name",
            "Address",
            "Apt. Number",
            "Last Service",
            "Route"});
            this.cboOrderBy.Location = new System.Drawing.Point(7, 65);
            this.cboOrderBy.Margin = new System.Windows.Forms.Padding(4);
            this.cboOrderBy.Name = "cboOrderBy";
            this.cboOrderBy.Size = new System.Drawing.Size(159, 23);
            this.cboOrderBy.TabIndex = 78;
            this.cboOrderBy.SelectedIndexChanged += new System.EventHandler(this.cboOrderBy_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(4, 49);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 15);
            this.label4.TabIndex = 77;
            this.label4.Tag = "SortOrder";
            this.label4.Text = "Order By:";
            // 
            // lblFilterBy
            // 
            this.lblFilterBy.AutoSize = true;
            this.lblFilterBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilterBy.Location = new System.Drawing.Point(8, 100);
            this.lblFilterBy.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFilterBy.Name = "lblFilterBy";
            this.lblFilterBy.Size = new System.Drawing.Size(53, 15);
            this.lblFilterBy.TabIndex = 72;
            this.lblFilterBy.Tag = "SortOrder";
            this.lblFilterBy.Text = "Filter By:";
            // 
            // cboFilter
            // 
            this.cboFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFilter.FormattingEnabled = true;
            this.cboFilter.Location = new System.Drawing.Point(7, 115);
            this.cboFilter.Margin = new System.Windows.Forms.Padding(4);
            this.cboFilter.Name = "cboFilter";
            this.cboFilter.Size = new System.Drawing.Size(159, 21);
            this.cboFilter.TabIndex = 73;
            this.cboFilter.Visible = false;
            this.cboFilter.SelectedIndexChanged += new System.EventHandler(this.cboFilter_SelectedIndexChanged);
            // 
            // tbFindName
            // 
            this.tbFindName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.tbFindName.HideSelection = false;
            this.tbFindName.Location = new System.Drawing.Point(4, 22);
            this.tbFindName.Margin = new System.Windows.Forms.Padding(4);
            this.tbFindName.Name = "tbFindName";
            this.tbFindName.Size = new System.Drawing.Size(162, 24);
            this.tbFindName.TabIndex = 70;
            this.tbFindName.WordWrap = false;
            this.tbFindName.TextChanged += new System.EventHandler(this.tbFindName_TextChanged);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(7, 21);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(4);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(159, 28);
            this.progressBar1.Step = 1;
            this.progressBar1.TabIndex = 76;
            this.progressBar1.Value = 1;
            this.progressBar1.Visible = false;
            // 
            // btnSaveRoute
            // 
            this.btnSaveRoute.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveRoute.Location = new System.Drawing.Point(591, 84);
            this.btnSaveRoute.Name = "btnSaveRoute";
            this.btnSaveRoute.Size = new System.Drawing.Size(60, 30);
            this.btnSaveRoute.TabIndex = 4;
            this.btnSaveRoute.Text = "&Save";
            this.btnSaveRoute.UseVisualStyleBackColor = true;
            this.btnSaveRoute.Click += new System.EventHandler(this.btnSaveRoute_Click);
            // 
            // btnSvcSave
            // 
            this.btnSvcSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSvcSave.Location = new System.Drawing.Point(201, 20);
            this.btnSvcSave.Name = "btnSvcSave";
            this.btnSvcSave.Size = new System.Drawing.Size(52, 30);
            this.btnSvcSave.TabIndex = 3;
            this.btnSvcSave.Text = "&Apply";
            this.btnSvcSave.UseVisualStyleBackColor = true;
            this.btnSvcSave.Click += new System.EventHandler(this.btnSvcSave_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip2);
            this.splitContainer1.Panel1.Controls.Add(this.pnlGiveService);
            this.splitContainer1.Panel1.Controls.Add(this.pnlRouteInfo);
            this.splitContainer1.Panel1.Controls.Add(this.lblRowCnt);
            this.splitContainer1.Panel1MinSize = 0;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvHD);
            this.splitContainer1.Size = new System.Drawing.Size(920, 577);
            this.splitContainer1.SplitterDistance = 248;
            this.splitContainer1.TabIndex = 85;
            // 
            // toolStrip2
            // 
            this.toolStrip2.AllowMerge = false;
            this.toolStrip2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAddress,
            this.tsbApt,
            this.tsbPhone,
            this.tsbSize,
            this.tsbComments,
            this.tsbDriverNotes,
            this.tsbSvcItem,
            this.tsbLastSvc});
            this.toolStrip2.Location = new System.Drawing.Point(112, 222);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(418, 25);
            this.toolStrip2.TabIndex = 138;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // tsbAddress
            // 
            this.tsbAddress.Checked = true;
            this.tsbAddress.CheckOnClick = true;
            this.tsbAddress.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsbAddress.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbAddress.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.tsbAddress.ForeColor = System.Drawing.Color.DarkBlue;
            this.tsbAddress.Image = ((System.Drawing.Image)(resources.GetObject("tsbAddress.Image")));
            this.tsbAddress.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAddress.Name = "tsbAddress";
            this.tsbAddress.Size = new System.Drawing.Size(53, 22);
            this.tsbAddress.Tag = "clmAddress";
            this.tsbAddress.Text = "Address";
            this.tsbAddress.ToolTipText = "Toggle Address and Apt columns";
            this.tsbAddress.CheckedChanged += new System.EventHandler(this.tsbToggle_CheckedChanged);
            // 
            // tsbApt
            // 
            this.tsbApt.Checked = true;
            this.tsbApt.CheckOnClick = true;
            this.tsbApt.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsbApt.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbApt.ForeColor = System.Drawing.Color.DarkBlue;
            this.tsbApt.Image = ((System.Drawing.Image)(resources.GetObject("tsbApt.Image")));
            this.tsbApt.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbApt.Name = "tsbApt";
            this.tsbApt.Size = new System.Drawing.Size(30, 22);
            this.tsbApt.Tag = "clmApt";
            this.tsbApt.Text = "Apt";
            this.tsbApt.ToolTipText = "Toggle Apt Column";
            this.tsbApt.Click += new System.EventHandler(this.tsbToggle_CheckedChanged);
            // 
            // tsbPhone
            // 
            this.tsbPhone.BackColor = System.Drawing.Color.Wheat;
            this.tsbPhone.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.tsbPhone.Checked = true;
            this.tsbPhone.CheckOnClick = true;
            this.tsbPhone.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsbPhone.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbPhone.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.tsbPhone.ForeColor = System.Drawing.Color.DarkBlue;
            this.tsbPhone.Image = ((System.Drawing.Image)(resources.GetObject("tsbPhone.Image")));
            this.tsbPhone.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPhone.Name = "tsbPhone";
            this.tsbPhone.Size = new System.Drawing.Size(45, 22);
            this.tsbPhone.Tag = "clmPhone";
            this.tsbPhone.Text = "Phone";
            this.tsbPhone.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.tsbPhone.ToolTipText = "Toggle Phone Column";
            this.tsbPhone.CheckedChanged += new System.EventHandler(this.tsbToggle_CheckedChanged);
            // 
            // tsbSize
            // 
            this.tsbSize.BackColor = System.Drawing.Color.Wheat;
            this.tsbSize.Checked = true;
            this.tsbSize.CheckOnClick = true;
            this.tsbSize.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsbSize.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbSize.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.tsbSize.ForeColor = System.Drawing.Color.DarkBlue;
            this.tsbSize.Image = ((System.Drawing.Image)(resources.GetObject("tsbSize.Image")));
            this.tsbSize.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSize.Name = "tsbSize";
            this.tsbSize.Size = new System.Drawing.Size(33, 22);
            this.tsbSize.Tag = "clmFamilySize";
            this.tsbSize.Text = "Size";
            this.tsbSize.ToolTipText = "Toggle Size column";
            this.tsbSize.CheckedChanged += new System.EventHandler(this.tsbToggle_CheckedChanged);
            // 
            // tsbComments
            // 
            this.tsbComments.BackColor = System.Drawing.Color.Wheat;
            this.tsbComments.Checked = true;
            this.tsbComments.CheckOnClick = true;
            this.tsbComments.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsbComments.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbComments.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.tsbComments.ForeColor = System.Drawing.Color.DarkBlue;
            this.tsbComments.Image = ((System.Drawing.Image)(resources.GetObject("tsbComments.Image")));
            this.tsbComments.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbComments.Name = "tsbComments";
            this.tsbComments.Size = new System.Drawing.Size(69, 22);
            this.tsbComments.Tag = "clmComments";
            this.tsbComments.Text = "Comments";
            this.tsbComments.ToolTipText = "Toggle Comments column";
            this.tsbComments.CheckedChanged += new System.EventHandler(this.tsbToggle_CheckedChanged);
            // 
            // tsbDriverNotes
            // 
            this.tsbDriverNotes.Checked = true;
            this.tsbDriverNotes.CheckOnClick = true;
            this.tsbDriverNotes.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsbDriverNotes.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbDriverNotes.ForeColor = System.Drawing.Color.DarkBlue;
            this.tsbDriverNotes.Image = ((System.Drawing.Image)(resources.GetObject("tsbDriverNotes.Image")));
            this.tsbDriverNotes.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDriverNotes.Name = "tsbDriverNotes";
            this.tsbDriverNotes.Size = new System.Drawing.Size(76, 22);
            this.tsbDriverNotes.Tag = "clmDriverNotes";
            this.tsbDriverNotes.Text = "Driver Notes";
            this.tsbDriverNotes.ToolTipText = "Toggle Driver Notes column";
            this.tsbDriverNotes.CheckedChanged += new System.EventHandler(this.tsbToggle_CheckedChanged);
            // 
            // tsbSvcItem
            // 
            this.tsbSvcItem.Checked = true;
            this.tsbSvcItem.CheckOnClick = true;
            this.tsbSvcItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsbSvcItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbSvcItem.ForeColor = System.Drawing.Color.DarkBlue;
            this.tsbSvcItem.Image = ((System.Drawing.Image)(resources.GetObject("tsbSvcItem.Image")));
            this.tsbSvcItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSvcItem.Name = "tsbSvcItem";
            this.tsbSvcItem.Size = new System.Drawing.Size(56, 22);
            this.tsbSvcItem.Tag = "clmsvcitem";
            this.tsbSvcItem.Text = "Svc Item";
            this.tsbSvcItem.ToolTipText = "Toggle Svc Item column";
            this.tsbSvcItem.CheckedChanged += new System.EventHandler(this.tsbToggle_CheckedChanged);
            // 
            // tsbLastSvc
            // 
            this.tsbLastSvc.Checked = true;
            this.tsbLastSvc.CheckOnClick = true;
            this.tsbLastSvc.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsbLastSvc.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbLastSvc.ForeColor = System.Drawing.Color.DarkBlue;
            this.tsbLastSvc.Image = ((System.Drawing.Image)(resources.GetObject("tsbLastSvc.Image")));
            this.tsbLastSvc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLastSvc.Name = "tsbLastSvc";
            this.tsbLastSvc.Size = new System.Drawing.Size(53, 22);
            this.tsbLastSvc.Tag = "clmLastSvc";
            this.tsbLastSvc.Text = "Last Svc";
            this.tsbLastSvc.ToolTipText = "Toggle Last Svc column";
            this.tsbLastSvc.CheckedChanged += new System.EventHandler(this.tsbToggle_CheckedChanged);
            // 
            // pnlGiveService
            // 
            this.pnlGiveService.BackColor = System.Drawing.Color.DarkKhaki;
            this.pnlGiveService.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlGiveService.Controls.Add(this.btnSvcSave);
            this.pnlGiveService.Controls.Add(this.pnlFindClient);
            this.pnlGiveService.Controls.Add(this.cboHDRoute);
            this.pnlGiveService.Controls.Add(this.label2);
            this.pnlGiveService.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlGiveService.Location = new System.Drawing.Point(640, 0);
            this.pnlGiveService.Name = "pnlGiveService";
            this.pnlGiveService.Size = new System.Drawing.Size(280, 248);
            this.pnlGiveService.TabIndex = 88;
            // 
            // pnlFindClient
            // 
            this.pnlFindClient.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlFindClient.Controls.Add(this.tbFindName);
            this.pnlFindClient.Controls.Add(this.label4);
            this.pnlFindClient.Controls.Add(this.label1);
            this.pnlFindClient.Controls.Add(this.lblFilterBy);
            this.pnlFindClient.Controls.Add(this.cboFilter);
            this.pnlFindClient.Controls.Add(this.cboOrderBy);
            this.pnlFindClient.Controls.Add(this.progressBar1);
            this.pnlFindClient.Location = new System.Drawing.Point(20, 66);
            this.pnlFindClient.Name = "pnlFindClient";
            this.pnlFindClient.Size = new System.Drawing.Size(174, 150);
            this.pnlFindClient.TabIndex = 87;
            // 
            // cboHDRoute
            // 
            this.cboHDRoute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHDRoute.FormattingEnabled = true;
            this.cboHDRoute.Location = new System.Drawing.Point(8, 22);
            this.cboHDRoute.Margin = new System.Windows.Forms.Padding(4);
            this.cboHDRoute.Name = "cboHDRoute";
            this.cboHDRoute.Size = new System.Drawing.Size(186, 21);
            this.cboHDRoute.TabIndex = 85;
            this.cboHDRoute.SelectionChangeCommitted += new System.EventHandler(this.cboHDRoute_SelectionChangeCommitted);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(5, 2);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 17);
            this.label2.TabIndex = 84;
            this.label2.Tag = "SortOrder";
            this.label2.Text = "Displayed Route:";
            // 
            // pnlRouteInfo
            // 
            this.pnlRouteInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlRouteInfo.Controls.Add(this.tbEstMiles);
            this.pnlRouteInfo.Controls.Add(this.tbEstTime);
            this.pnlRouteInfo.Controls.Add(this.label7);
            this.pnlRouteInfo.Controls.Add(this.label6);
            this.pnlRouteInfo.Controls.Add(this.tbDriverComments);
            this.pnlRouteInfo.Controls.Add(this.tbRouteNotes);
            this.pnlRouteInfo.Controls.Add(this.btnSaveRoute);
            this.pnlRouteInfo.Controls.Add(this.label5);
            this.pnlRouteInfo.Controls.Add(this.label3);
            this.pnlRouteInfo.Controls.Add(this.tbPhone);
            this.pnlRouteInfo.Controls.Add(this.lblPhone);
            this.pnlRouteInfo.Controls.Add(this.btnSelectDriver);
            this.pnlRouteInfo.Controls.Add(this.tbDriver);
            this.pnlRouteInfo.Controls.Add(this.lblVolunteer);
            this.pnlRouteInfo.Location = new System.Drawing.Point(0, 0);
            this.pnlRouteInfo.Name = "pnlRouteInfo";
            this.pnlRouteInfo.Size = new System.Drawing.Size(668, 216);
            this.pnlRouteInfo.TabIndex = 89;
            // 
            // tbEstMiles
            // 
            this.tbEstMiles.Location = new System.Drawing.Point(506, 23);
            this.tbEstMiles.Name = "tbEstMiles";
            this.tbEstMiles.Size = new System.Drawing.Size(54, 20);
            this.tbEstMiles.TabIndex = 39;
            this.tbEstMiles.Tag = "estmiles";
            this.tbEstMiles.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbEstMiles.Enter += new System.EventHandler(this.tbRoute_Enter);
            this.tbEstMiles.Leave += new System.EventHandler(this.tbRoute_Leave);
            // 
            // tbEstTime
            // 
            this.tbEstTime.Location = new System.Drawing.Point(441, 24);
            this.tbEstTime.Name = "tbEstTime";
            this.tbEstTime.Size = new System.Drawing.Size(54, 20);
            this.tbEstTime.TabIndex = 38;
            this.tbEstTime.Tag = "esthours";
            this.tbEstTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbEstTime.Enter += new System.EventHandler(this.tbRoute_Enter);
            this.tbEstTime.Leave += new System.EventHandler(this.tbRoute_Leave);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(257, 64);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 13);
            this.label7.TabIndex = 37;
            this.label7.Text = "Driver Notes:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(2, 64);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 13);
            this.label6.TabIndex = 36;
            this.label6.Text = "Route Notes:";
            // 
            // tbDriverComments
            // 
            this.tbDriverComments.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbDriverComments.Location = new System.Drawing.Point(260, 84);
            this.tbDriverComments.Multiline = true;
            this.tbDriverComments.Name = "tbDriverComments";
            this.tbDriverComments.Size = new System.Drawing.Size(291, 120);
            this.tbDriverComments.TabIndex = 35;
            this.tbDriverComments.Tag = "drivernotes";
            this.tbDriverComments.Text = "123456789012345678901234567890123456789";
            this.tbDriverComments.Enter += new System.EventHandler(this.tbRoute_Enter);
            this.tbDriverComments.Leave += new System.EventHandler(this.tbRoute_Leave);
            // 
            // tbRouteNotes
            // 
            this.tbRouteNotes.Location = new System.Drawing.Point(5, 84);
            this.tbRouteNotes.Multiline = true;
            this.tbRouteNotes.Name = "tbRouteNotes";
            this.tbRouteNotes.Size = new System.Drawing.Size(249, 120);
            this.tbRouteNotes.TabIndex = 34;
            this.tbRouteNotes.Tag = "notes";
            this.tbRouteNotes.Text = "123456789012345678901234567890123456789";
            this.tbRouteNotes.Enter += new System.EventHandler(this.tbRoute_Enter);
            this.tbRouteNotes.Leave += new System.EventHandler(this.tbRoute_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(504, 4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 13);
            this.label5.TabIndex = 32;
            this.label5.Text = "Est Miles:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(438, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 30;
            this.label3.Text = "Est Time:";
            // 
            // tbPhone
            // 
            this.tbPhone.AllowPromptAsInput = false;
            this.tbPhone.BeepOnError = true;
            this.tbPhone.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPhone.HidePromptOnLeave = true;
            this.tbPhone.Location = new System.Drawing.Point(260, 25);
            this.tbPhone.Mask = "(999) 000-0000 aaaaaaaaa";
            this.tbPhone.Name = "tbPhone";
            this.tbPhone.Size = new System.Drawing.Size(165, 23);
            this.tbPhone.TabIndex = 29;
            this.tbPhone.Tag = "phone";
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Location = new System.Drawing.Point(257, 4);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(72, 13);
            this.lblPhone.TabIndex = 3;
            this.lblPhone.Text = "Driver Phone:";
            // 
            // btnSelectDriver
            // 
            this.btnSelectDriver.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectDriver.Location = new System.Drawing.Point(225, 24);
            this.btnSelectDriver.Name = "btnSelectDriver";
            this.btnSelectDriver.Size = new System.Drawing.Size(29, 24);
            this.btnSelectDriver.TabIndex = 2;
            this.btnSelectDriver.Text = "...";
            this.btnSelectDriver.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSelectDriver.UseVisualStyleBackColor = true;
            // 
            // tbDriver
            // 
            this.tbDriver.BackColor = System.Drawing.Color.White;
            this.tbDriver.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbDriver.ForeColor = System.Drawing.Color.DarkBlue;
            this.tbDriver.Location = new System.Drawing.Point(5, 24);
            this.tbDriver.Name = "tbDriver";
            this.tbDriver.ReadOnly = true;
            this.tbDriver.Size = new System.Drawing.Size(213, 24);
            this.tbDriver.TabIndex = 1;
            this.tbDriver.TabStop = false;
            // 
            // lblVolunteer
            // 
            this.lblVolunteer.AutoSize = true;
            this.lblVolunteer.Location = new System.Drawing.Point(2, 4);
            this.lblVolunteer.Name = "lblVolunteer";
            this.lblVolunteer.Size = new System.Drawing.Size(70, 13);
            this.lblVolunteer.TabIndex = 0;
            this.lblVolunteer.Text = "Route Driver:";
            // 
            // lblRowCnt
            // 
            this.lblRowCnt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblRowCnt.AutoSize = true;
            this.lblRowCnt.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRowCnt.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblRowCnt.Location = new System.Drawing.Point(3, 230);
            this.lblRowCnt.Name = "lblRowCnt";
            this.lblRowCnt.Size = new System.Drawing.Size(46, 17);
            this.lblRowCnt.TabIndex = 86;
            this.lblRowCnt.Text = "[ 23 ]";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpgReview);
            this.tabControl1.Controls.Add(this.tpgPlanner);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(959, 637);
            this.tabControl1.TabIndex = 90;
            // 
            // tpgReview
            // 
            this.tpgReview.BackColor = System.Drawing.Color.Tan;
            this.tpgReview.Controls.Add(this.splitContainer2);
            this.tpgReview.Location = new System.Drawing.Point(4, 25);
            this.tpgReview.Name = "tpgReview";
            this.tpgReview.Padding = new System.Windows.Forms.Padding(3);
            this.tpgReview.Size = new System.Drawing.Size(951, 608);
            this.tpgReview.TabIndex = 1;
            this.tpgReview.Text = "Service Reviewer";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(3, 3);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.toolStrip3);
            this.splitContainer2.Size = new System.Drawing.Size(945, 602);
            this.splitContainer2.SplitterDistance = 373;
            this.splitContainer2.TabIndex = 0;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.label9);
            this.splitContainer3.Panel1.Controls.Add(this.lvwStatus);
            this.splitContainer3.Panel1.Controls.Add(this.btnLoad);
            this.splitContainer3.Panel1.Controls.Add(this.dtpServiceDate);
            this.splitContainer3.Panel1.Controls.Add(this.label8);
            this.splitContainer3.Panel1.Controls.Add(this.chkSelectAll);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.lvwRouteStatus);
            this.splitContainer3.Size = new System.Drawing.Size(373, 602);
            this.splitContainer3.SplitterDistance = 174;
            this.splitContainer3.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 60);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(37, 13);
            this.label9.TabIndex = 115;
            this.label9.Text = "Status";
            // 
            // lvwStatus
            // 
            this.lvwStatus.CheckBoxes = true;
            this.lvwStatus.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lvwStatus.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvwStatus.Location = new System.Drawing.Point(63, 59);
            this.lvwStatus.Name = "lvwStatus";
            this.lvwStatus.Size = new System.Drawing.Size(183, 97);
            this.lvwStatus.TabIndex = 114;
            this.lvwStatus.UseCompatibleStateImageBehavior = false;
            this.lvwStatus.View = System.Windows.Forms.View.Details;
            this.lvwStatus.SelectedIndexChanged += new System.EventHandler(this.lvwStatus_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Status Name";
            this.columnHeader1.Width = 170;
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(252, 69);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(57, 24);
            this.btnLoad.TabIndex = 113;
            this.btnLoad.Text = "&Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // dtpServiceDate
            // 
            this.dtpServiceDate.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpServiceDate.Location = new System.Drawing.Point(3, 30);
            this.dtpServiceDate.Name = "dtpServiceDate";
            this.dtpServiceDate.Size = new System.Drawing.Size(254, 20);
            this.dtpServiceDate.TabIndex = 112;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(6, 6);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(105, 17);
            this.label8.TabIndex = 111;
            this.label8.Text = "Date of Service";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.AutoSize = true;
            this.chkSelectAll.Location = new System.Drawing.Point(3, 151);
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Size = new System.Drawing.Size(70, 17);
            this.chkSelectAll.TabIndex = 0;
            this.chkSelectAll.Text = "Select All";
            this.chkSelectAll.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.chkSelectAll.UseVisualStyleBackColor = true;
            this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
            // 
            // lvwRouteStatus
            // 
            this.lvwRouteStatus.CheckBoxes = true;
            this.lvwRouteStatus.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colRouteTitle,
            this.colNbrClients,
            this.colStatus,
            this.colRouteID});
            this.lvwRouteStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwRouteStatus.Location = new System.Drawing.Point(0, 0);
            this.lvwRouteStatus.Name = "lvwRouteStatus";
            this.lvwRouteStatus.Size = new System.Drawing.Size(373, 424);
            this.lvwRouteStatus.TabIndex = 0;
            this.lvwRouteStatus.UseCompatibleStateImageBehavior = false;
            this.lvwRouteStatus.View = System.Windows.Forms.View.Details;
            // 
            // colRouteTitle
            // 
            this.colRouteTitle.Text = "Title";
            this.colRouteTitle.Width = 160;
            // 
            // colNbrClients
            // 
            this.colNbrClients.Text = "# Clients";
            this.colNbrClients.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colNbrClients.Width = 70;
            // 
            // colStatus
            // 
            this.colStatus.Text = "Status";
            this.colStatus.Width = 90;
            // 
            // colRouteID
            // 
            this.colRouteID.Text = "ID";
            this.colRouteID.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colRouteID.Width = 0;
            // 
            // toolStrip3
            // 
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbPrepare,
            this.tsbPrint,
            this.tsbPost,
            this.toolStripSeparator1,
            this.tsbShowDelivDates});
            this.toolStrip3.Location = new System.Drawing.Point(0, 0);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(568, 25);
            this.toolStrip3.TabIndex = 0;
            // 
            // tsbPrepare
            // 
            this.tsbPrepare.Image = ((System.Drawing.Image)(resources.GetObject("tsbPrepare.Image")));
            this.tsbPrepare.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbPrepare.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPrepare.Name = "tsbPrepare";
            this.tsbPrepare.Size = new System.Drawing.Size(127, 22);
            this.tsbPrepare.Text = "Prepare Route Lists";
            this.tsbPrepare.Click += new System.EventHandler(this.tsbPrepare_Click);
            // 
            // tsbPrint
            // 
            this.tsbPrint.Image = ((System.Drawing.Image)(resources.GetObject("tsbPrint.Image")));
            this.tsbPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPrint.Name = "tsbPrint";
            this.tsbPrint.Size = new System.Drawing.Size(123, 22);
            this.tsbPrint.Text = "Print Route Sheets";
            // 
            // tsbPost
            // 
            this.tsbPost.Image = ((System.Drawing.Image)(resources.GetObject("tsbPost.Image")));
            this.tsbPost.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbPost.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPost.Name = "tsbPost";
            this.tsbPost.Size = new System.Drawing.Size(95, 22);
            this.tsbPost.Text = "Post Services";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbShowDelivDates
            // 
            this.tsbShowDelivDates.Image = ((System.Drawing.Image)(resources.GetObject("tsbShowDelivDates.Image")));
            this.tsbShowDelivDates.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbShowDelivDates.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbShowDelivDates.Name = "tsbShowDelivDates";
            this.tsbShowDelivDates.Size = new System.Drawing.Size(133, 22);
            this.tsbShowDelivDates.Text = "Show Delivery Dates";
            // 
            // tpgPlanner
            // 
            this.tpgPlanner.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.tpgPlanner.Controls.Add(this.splitContainer1);
            this.tpgPlanner.Location = new System.Drawing.Point(4, 22);
            this.tpgPlanner.Name = "tpgPlanner";
            this.tpgPlanner.Padding = new System.Windows.Forms.Padding(3);
            this.tpgPlanner.Size = new System.Drawing.Size(976, 611);
            this.tpgPlanner.TabIndex = 0;
            this.tpgPlanner.Text = "Route Planner";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Household Name";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.dataGridViewTextBoxColumn1.Visible = false;
            this.dataGridViewTextBoxColumn1.Width = 250;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle12;
            this.dataGridViewTextBoxColumn2.HeaderText = "Address";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.dataGridViewTextBoxColumn2.Visible = false;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle13;
            this.dataGridViewTextBoxColumn3.HeaderText = "Apt.";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn4.HeaderText = "City";
            this.dataGridViewTextBoxColumn4.MaxInputLength = 50;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle14;
            this.dataGridViewTextBoxColumn5.HeaderText = "Route";
            this.dataGridViewTextBoxColumn5.MaxInputLength = 50;
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.dataGridViewTextBoxColumn5.Width = 120;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn6.DefaultCellStyle = dataGridViewCellStyle15;
            this.dataGridViewTextBoxColumn6.HeaderText = "Column1";
            this.dataGridViewTextBoxColumn6.MaxInputLength = 40;
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.dataGridViewTextBoxColumn6.Visible = false;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn7.HeaderText = "Date Served";
            this.dataGridViewTextBoxColumn7.MaxInputLength = 50;
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn8.DefaultCellStyle = dataGridViewCellStyle16;
            this.dataGridViewTextBoxColumn8.HeaderText = "ID";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn9.DefaultCellStyle = dataGridViewCellStyle17;
            this.dataGridViewTextBoxColumn9.HeaderText = "Size";
            this.dataGridViewTextBoxColumn9.MaxInputLength = 300;
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            this.dataGridViewTextBoxColumn9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn10.DefaultCellStyle = dataGridViewCellStyle18;
            this.dataGridViewTextBoxColumn10.HeaderText = "Date Served";
            this.dataGridViewTextBoxColumn10.MaxInputLength = 10;
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            this.dataGridViewTextBoxColumn10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn11.DefaultCellStyle = dataGridViewCellStyle19;
            this.dataGridViewTextBoxColumn11.HeaderText = "Comments";
            this.dataGridViewTextBoxColumn11.MaxInputLength = 10;
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.ReadOnly = true;
            this.dataGridViewTextBoxColumn11.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // HDPlannerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Cornsilk;
            this.ClientSize = new System.Drawing.Size(1099, 662);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1000, 700);
            this.Name = "HDPlannerForm";
            this.Text = "Home Delivery Form";
            this.Load += new System.EventHandler(this.HomeDeliveryForm_Load);
            this.Resize += new System.EventHandler(this.EditHDForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHD)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.pnlGiveService.ResumeLayout(false);
            this.pnlGiveService.PerformLayout();
            this.pnlFindClient.ResumeLayout(false);
            this.pnlFindClient.PerformLayout();
            this.pnlRouteInfo.ResumeLayout(false);
            this.pnlRouteInfo.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tpgReview.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.tpgPlanner.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvHD;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboOrderBy;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblFilterBy;
        private System.Windows.Forms.ComboBox cboFilter;
        public System.Windows.Forms.TextBox tbFindName;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btnSaveRoute;
        private System.Windows.Forms.Button btnSvcSave;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ComboBox cboHDRoute;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblRowCnt;
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
        private System.Windows.Forms.Panel pnlGiveService;
        private System.Windows.Forms.Panel pnlFindClient;
        private System.Windows.Forms.Panel pnlRouteInfo;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.Button btnSelectDriver;
        private System.Windows.Forms.TextBox tbDriver;
        private System.Windows.Forms.Label lblVolunteer;
        private System.Windows.Forms.MaskedTextBox tbPhone;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbDriverComments;
        private System.Windows.Forms.TextBox tbRouteNotes;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpgPlanner;
        private System.Windows.Forms.TabPage tpgReview;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton tsbAddress;
        private System.Windows.Forms.ToolStripButton tsbPhone;
        private System.Windows.Forms.ToolStripButton tsbSize;
        private System.Windows.Forms.ToolStripButton tsbComments;
        private System.Windows.Forms.ToolStripButton tsbDriverNotes;
        private System.Windows.Forms.ToolStripButton tsbSvcItem;
        private System.Windows.Forms.ToolStripButton tsbLastSvc;
        private System.Windows.Forms.ToolStripButton tsbApt;
        private System.Windows.Forms.DataGridViewCheckBoxColumn clmSel;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmRouteID;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmRouteTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmID;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmApt;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmPhone;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmFamilySize;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmComments;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmDriverNotes;
        private System.Windows.Forms.DataGridViewComboBoxColumn clmSvcItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmLastSvc;
        private System.Windows.Forms.TextBox tbEstTime;
        private System.Windows.Forms.TextBox tbEstMiles;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.CheckBox chkSelectAll;
        private System.Windows.Forms.ListView lvwRouteStatus;
        private System.Windows.Forms.ColumnHeader colRouteID;
        private System.Windows.Forms.ColumnHeader colRouteTitle;
        private System.Windows.Forms.ColumnHeader colStatus;
        private System.Windows.Forms.DateTimePicker dtpServiceDate;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripButton tsbPrepare;
        private System.Windows.Forms.ToolStripButton tsbPrint;
        private System.Windows.Forms.ToolStripButton tsbPost;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbShowDelivDates;
        private System.Windows.Forms.ColumnHeader colNbrClients;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ListView lvwStatus;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}