namespace ClientcardFB3
{
    partial class FBJobsPlanForm
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
                if (clsJobsPlan != null)
                {
                    clsJobsPlan.Dispose();
                }
                if (clsVols != null)
                {
                    clsVols.Dispose();
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FBJobsPlanForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.spltcMain = new System.Windows.Forms.SplitContainer();
            this.spltcJobPlan = new System.Windows.Forms.SplitContainer();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.tsb0 = new System.Windows.Forms.ToolStripButton();
            this.tsb1 = new System.Windows.Forms.ToolStripButton();
            this.tsb2 = new System.Windows.Forms.ToolStripButton();
            this.tsb3 = new System.Windows.Forms.ToolStripButton();
            this.tsb4 = new System.Windows.Forms.ToolStripButton();
            this.tsb5 = new System.Windows.Forms.ToolStripButton();
            this.tsb6 = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbShowJobs = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbShowVols = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.dgvJobPlans = new System.Windows.Forms.DataGridView();
            this.lvwVols = new System.Windows.Forms.ListView();
            this.col1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.spltcJobs = new System.Windows.Forms.SplitContainer();
            this.label3 = new System.Windows.Forms.Label();
            this.btnAddJobsToPlan = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tbStart = new System.Windows.Forms.TextBox();
            this.tbEnd = new System.Windows.Forms.TextBox();
            this.lvwJobs = new System.Windows.Forms.ListView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuAction = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAddJobs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuAddPrimary = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDeletePrimary = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuAddBackup = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDeleteBackup = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDeleteJob = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsJobPlans = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiCopyAll = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCopySelected = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.colJobTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colShiftStart = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colShiftEnd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrimary = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBackup = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.spltcMain)).BeginInit();
            this.spltcMain.Panel1.SuspendLayout();
            this.spltcMain.Panel2.SuspendLayout();
            this.spltcMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spltcJobPlan)).BeginInit();
            this.spltcJobPlan.Panel1.SuspendLayout();
            this.spltcJobPlan.Panel2.SuspendLayout();
            this.spltcJobPlan.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvJobPlans)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spltcJobs)).BeginInit();
            this.spltcJobs.Panel1.SuspendLayout();
            this.spltcJobs.Panel2.SuspendLayout();
            this.spltcJobs.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.cmsJobPlans.SuspendLayout();
            this.SuspendLayout();
            // 
            // spltcMain
            // 
            this.spltcMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.spltcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spltcMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.spltcMain.Location = new System.Drawing.Point(0, 24);
            this.spltcMain.Name = "spltcMain";
            // 
            // spltcMain.Panel1
            // 
            this.spltcMain.Panel1.Controls.Add(this.spltcJobPlan);
            // 
            // spltcMain.Panel2
            // 
            this.spltcMain.Panel2.Controls.Add(this.spltcJobs);
            this.spltcMain.Panel2.Controls.Add(this.lvwVols);
            this.spltcMain.Size = new System.Drawing.Size(879, 638);
            this.spltcMain.SplitterDistance = 663;
            this.spltcMain.TabIndex = 0;
            // 
            // spltcJobPlan
            // 
            this.spltcJobPlan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spltcJobPlan.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.spltcJobPlan.Location = new System.Drawing.Point(0, 0);
            this.spltcJobPlan.Name = "spltcJobPlan";
            this.spltcJobPlan.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // spltcJobPlan.Panel1
            // 
            this.spltcJobPlan.Panel1.Controls.Add(this.toolStrip2);
            this.spltcJobPlan.Panel1.Controls.Add(this.toolStrip1);
            // 
            // spltcJobPlan.Panel2
            // 
            this.spltcJobPlan.Panel2.Controls.Add(this.dgvJobPlans);
            this.spltcJobPlan.Size = new System.Drawing.Size(661, 636);
            this.spltcJobPlan.SplitterDistance = 80;
            this.spltcJobPlan.TabIndex = 0;
            // 
            // toolStrip2
            // 
            this.toolStrip2.BackColor = System.Drawing.Color.Gainsboro;
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb0,
            this.tsb1,
            this.tsb2,
            this.tsb3,
            this.tsb4,
            this.tsb5,
            this.tsb6});
            this.toolStrip2.Location = new System.Drawing.Point(0, 55);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(561, 25);
            this.toolStrip2.TabIndex = 3;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // tsb0
            // 
            this.tsb0.CheckOnClick = true;
            this.tsb0.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsb0.Image = ((System.Drawing.Image)(resources.GetObject("tsb0.Image")));
            this.tsb0.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb0.Name = "tsb0";
            this.tsb0.Size = new System.Drawing.Size(50, 22);
            this.tsb0.Tag = "0";
            this.tsb0.Text = "S&unday";
            this.tsb0.CheckedChanged += new System.EventHandler(this.tsb_CheckedChanged);
            // 
            // tsb1
            // 
            this.tsb1.CheckOnClick = true;
            this.tsb1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsb1.Image = ((System.Drawing.Image)(resources.GetObject("tsb1.Image")));
            this.tsb1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb1.Name = "tsb1";
            this.tsb1.Size = new System.Drawing.Size(55, 22);
            this.tsb1.Tag = "1";
            this.tsb1.Text = "&Monday";
            this.tsb1.CheckedChanged += new System.EventHandler(this.tsb_CheckedChanged);
            // 
            // tsb2
            // 
            this.tsb2.CheckOnClick = true;
            this.tsb2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsb2.Image = ((System.Drawing.Image)(resources.GetObject("tsb2.Image")));
            this.tsb2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb2.Name = "tsb2";
            this.tsb2.Size = new System.Drawing.Size(55, 22);
            this.tsb2.Tag = "2";
            this.tsb2.Text = "&Tuesday";
            this.tsb2.CheckedChanged += new System.EventHandler(this.tsb_CheckedChanged);
            // 
            // tsb3
            // 
            this.tsb3.CheckOnClick = true;
            this.tsb3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsb3.Image = ((System.Drawing.Image)(resources.GetObject("tsb3.Image")));
            this.tsb3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb3.Name = "tsb3";
            this.tsb3.Size = new System.Drawing.Size(72, 22);
            this.tsb3.Tag = "3";
            this.tsb3.Text = "&Wednesday";
            this.tsb3.CheckedChanged += new System.EventHandler(this.tsb_CheckedChanged);
            // 
            // tsb4
            // 
            this.tsb4.CheckOnClick = true;
            this.tsb4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsb4.Image = ((System.Drawing.Image)(resources.GetObject("tsb4.Image")));
            this.tsb4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb4.Name = "tsb4";
            this.tsb4.Size = new System.Drawing.Size(60, 22);
            this.tsb4.Text = "T&hursday";
            this.tsb4.CheckedChanged += new System.EventHandler(this.tsb_CheckedChanged);
            // 
            // tsb5
            // 
            this.tsb5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsb5.Image = ((System.Drawing.Image)(resources.GetObject("tsb5.Image")));
            this.tsb5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb5.Name = "tsb5";
            this.tsb5.Size = new System.Drawing.Size(43, 22);
            this.tsb5.Tag = "5";
            this.tsb5.Text = "&Friday";
            this.tsb5.CheckedChanged += new System.EventHandler(this.tsb_CheckedChanged);
            // 
            // tsb6
            // 
            this.tsb6.CheckOnClick = true;
            this.tsb6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsb6.Image = ((System.Drawing.Image)(resources.GetObject("tsb6.Image")));
            this.tsb6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb6.Name = "tsb6";
            this.tsb6.Size = new System.Drawing.Size(57, 22);
            this.tsb6.Tag = "6";
            this.tsb6.Text = "&Saturday";
            this.tsb6.CheckedChanged += new System.EventHandler(this.tsb_CheckedChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.Wheat;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Right;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbShowJobs,
            this.toolStripSeparator1,
            this.tsbShowVols,
            this.toolStripSeparator2});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(561, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(100, 80);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbShowJobs
            // 
            this.tsbShowJobs.AutoSize = false;
            this.tsbShowJobs.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tsbShowJobs.Checked = true;
            this.tsbShowJobs.CheckOnClick = true;
            this.tsbShowJobs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsbShowJobs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbShowJobs.Image = ((System.Drawing.Image)(resources.GetObject("tsbShowJobs.Image")));
            this.tsbShowJobs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbShowJobs.Name = "tsbShowJobs";
            this.tsbShowJobs.Size = new System.Drawing.Size(97, 24);
            this.tsbShowJobs.Text = "Show &Jobs";
            this.tsbShowJobs.Click += new System.EventHandler(this.tsbShowJobs_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(97, 6);
            // 
            // tsbShowVols
            // 
            this.tsbShowVols.AutoSize = false;
            this.tsbShowVols.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tsbShowVols.CheckOnClick = true;
            this.tsbShowVols.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbShowVols.Image = ((System.Drawing.Image)(resources.GetObject("tsbShowVols.Image")));
            this.tsbShowVols.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbShowVols.Name = "tsbShowVols";
            this.tsbShowVols.Size = new System.Drawing.Size(99, 24);
            this.tsbShowVols.Text = "Show &Volunteers";
            this.tsbShowVols.Click += new System.EventHandler(this.tsbShowVols_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(97, 6);
            // 
            // dgvJobPlans
            // 
            this.dgvJobPlans.AllowDrop = true;
            this.dgvJobPlans.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.dgvJobPlans.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvJobPlans.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.dgvJobPlans.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvJobPlans.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colJobTitle,
            this.colShiftStart,
            this.colShiftEnd,
            this.colPrimary,
            this.colBackup});
            this.dgvJobPlans.ContextMenuStrip = this.cmsJobPlans;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvJobPlans.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvJobPlans.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvJobPlans.GridColor = System.Drawing.Color.Silver;
            this.dgvJobPlans.Location = new System.Drawing.Point(0, 0);
            this.dgvJobPlans.MultiSelect = false;
            this.dgvJobPlans.Name = "dgvJobPlans";
            this.dgvJobPlans.RowHeadersWidth = 24;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            this.dgvJobPlans.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvJobPlans.RowTemplate.Height = 26;
            this.dgvJobPlans.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvJobPlans.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvJobPlans.Size = new System.Drawing.Size(661, 552);
            this.dgvJobPlans.TabIndex = 0;
            this.dgvJobPlans.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvJobPlans_CellEnter);
            this.dgvJobPlans.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvJobPlans_CellValueChanged);
            this.dgvJobPlans.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvJobPlans_RowEnter);
            this.dgvJobPlans.DragDrop += new System.Windows.Forms.DragEventHandler(this.dgvJobPlans_DragDrop);
            this.dgvJobPlans.DragEnter += new System.Windows.Forms.DragEventHandler(this.dgvJobPlans_DragEnter);
            this.dgvJobPlans.DragOver += new System.Windows.Forms.DragEventHandler(this.dgvJobPlans_DragOver);
            this.dgvJobPlans.DragLeave += new System.EventHandler(this.dgvJobPlans_DragLeave);
            // 
            // lvwVols
            // 
            this.lvwVols.Activation = System.Windows.Forms.ItemActivation.TwoClick;
            this.lvwVols.BackColor = System.Drawing.Color.Wheat;
            this.lvwVols.CausesValidation = false;
            this.lvwVols.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.col1});
            this.lvwVols.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwVols.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvwVols.GridLines = true;
            this.lvwVols.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvwVols.HideSelection = false;
            this.lvwVols.Location = new System.Drawing.Point(0, 0);
            this.lvwVols.MultiSelect = false;
            this.lvwVols.Name = "lvwVols";
            this.lvwVols.Size = new System.Drawing.Size(210, 636);
            this.lvwVols.TabIndex = 1;
            this.lvwVols.UseCompatibleStateImageBehavior = false;
            this.lvwVols.View = System.Windows.Forms.View.Details;
            this.lvwVols.ItemActivate += new System.EventHandler(this.lvwVols_ItemActivate);
            this.lvwVols.SelectedIndexChanged += new System.EventHandler(this.lvwVols_SelectedIndexChanged);
            this.lvwVols.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lvwVols_MouseDown);
            // 
            // col1
            // 
            this.col1.Width = 180;
            // 
            // spltcJobs
            // 
            this.spltcJobs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spltcJobs.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.spltcJobs.Location = new System.Drawing.Point(0, 0);
            this.spltcJobs.Name = "spltcJobs";
            this.spltcJobs.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // spltcJobs.Panel1
            // 
            this.spltcJobs.Panel1.BackColor = System.Drawing.Color.Wheat;
            this.spltcJobs.Panel1.Controls.Add(this.label3);
            this.spltcJobs.Panel1.Controls.Add(this.btnAddJobsToPlan);
            this.spltcJobs.Panel1.Controls.Add(this.label2);
            this.spltcJobs.Panel1.Controls.Add(this.tbStart);
            this.spltcJobs.Panel1.Controls.Add(this.tbEnd);
            // 
            // spltcJobs.Panel2
            // 
            this.spltcJobs.Panel2.Controls.Add(this.lvwJobs);
            this.spltcJobs.Size = new System.Drawing.Size(210, 636);
            this.spltcJobs.SplitterDistance = 80;
            this.spltcJobs.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(112, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Shift Ends";
            // 
            // btnAddJobsToPlan
            // 
            this.btnAddJobsToPlan.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnAddJobsToPlan.Enabled = false;
            this.btnAddJobsToPlan.Location = new System.Drawing.Point(0, 50);
            this.btnAddJobsToPlan.Name = "btnAddJobsToPlan";
            this.btnAddJobsToPlan.Size = new System.Drawing.Size(210, 30);
            this.btnAddJobsToPlan.TabIndex = 2;
            this.btnAddJobsToPlan.Text = "Add Selected Jobs to Plan";
            this.btnAddJobsToPlan.UseVisualStyleBackColor = true;
            this.btnAddJobsToPlan.Click += new System.EventHandler(this.btnAddJobsToPlan_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Shift Starts";
            // 
            // tbStart
            // 
            this.tbStart.Location = new System.Drawing.Point(27, 24);
            this.tbStart.Name = "tbStart";
            this.tbStart.Size = new System.Drawing.Size(72, 20);
            this.tbStart.TabIndex = 3;
            this.tbStart.Text = "9:30 AM";
            this.tbStart.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbEnd
            // 
            this.tbEnd.Location = new System.Drawing.Point(105, 24);
            this.tbEnd.Name = "tbEnd";
            this.tbEnd.Size = new System.Drawing.Size(72, 20);
            this.tbEnd.TabIndex = 4;
            this.tbEnd.Text = "1:45 PM";
            this.tbEnd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lvwJobs
            // 
            this.lvwJobs.BackColor = System.Drawing.Color.Wheat;
            this.lvwJobs.CausesValidation = false;
            this.lvwJobs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwJobs.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvwJobs.FullRowSelect = true;
            this.lvwJobs.GridLines = true;
            this.lvwJobs.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvwJobs.HideSelection = false;
            this.lvwJobs.Location = new System.Drawing.Point(0, 0);
            this.lvwJobs.Name = "lvwJobs";
            this.lvwJobs.Size = new System.Drawing.Size(210, 552);
            this.lvwJobs.TabIndex = 0;
            this.lvwJobs.UseCompatibleStateImageBehavior = false;
            this.lvwJobs.View = System.Windows.Forms.View.List;
            this.lvwJobs.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvwJobs_ItemSelectionChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuAction});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(879, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuAction
            // 
            this.menuAction.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAddJobs,
            this.mnuDeleteJob,
            this.toolStripSeparator3,
            this.mnuAddPrimary,
            this.mnuDeletePrimary,
            this.toolStripSeparator4,
            this.mnuAddBackup,
            this.mnuDeleteBackup,
            this.toolStripSeparator5,
            this.mnuExit});
            this.menuAction.Name = "menuAction";
            this.menuAction.Size = new System.Drawing.Size(54, 20);
            this.menuAction.Text = "&Action";
            // 
            // mnuAddJobs
            // 
            this.mnuAddJobs.Name = "mnuAddJobs";
            this.mnuAddJobs.Size = new System.Drawing.Size(255, 22);
            this.mnuAddJobs.Text = "Add Selected &Jobs to Plan";
            this.mnuAddJobs.Click += new System.EventHandler(this.btnAddJobsToPlan_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(252, 6);
            // 
            // mnuAddPrimary
            // 
            this.mnuAddPrimary.Enabled = false;
            this.mnuAddPrimary.Name = "mnuAddPrimary";
            this.mnuAddPrimary.Size = new System.Drawing.Size(255, 22);
            this.mnuAddPrimary.Text = "Add Selected Volunteer as &Primary";
            this.mnuAddPrimary.Click += new System.EventHandler(this.mnuAddJPVol_Click);
            // 
            // mnuDeletePrimary
            // 
            this.mnuDeletePrimary.Enabled = false;
            this.mnuDeletePrimary.Name = "mnuDeletePrimary";
            this.mnuDeletePrimary.Size = new System.Drawing.Size(255, 22);
            this.mnuDeletePrimary.Text = "Delete P&rimary";
            this.mnuDeletePrimary.Click += new System.EventHandler(this.mnuDeleteJPVol_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(252, 6);
            // 
            // mnuAddBackup
            // 
            this.mnuAddBackup.Enabled = false;
            this.mnuAddBackup.Name = "mnuAddBackup";
            this.mnuAddBackup.Size = new System.Drawing.Size(255, 22);
            this.mnuAddBackup.Text = "Add Selected Volunteer as &Backup";
            this.mnuAddBackup.Click += new System.EventHandler(this.mnuAddJPVol_Click);
            // 
            // mnuDeleteBackup
            // 
            this.mnuDeleteBackup.Enabled = false;
            this.mnuDeleteBackup.Name = "mnuDeleteBackup";
            this.mnuDeleteBackup.Size = new System.Drawing.Size(255, 22);
            this.mnuDeleteBackup.Text = "Delete B&ackup";
            this.mnuDeleteBackup.Click += new System.EventHandler(this.mnuDeleteJPVol_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(252, 6);
            // 
            // mnuExit
            // 
            this.mnuExit.Name = "mnuExit";
            this.mnuExit.Size = new System.Drawing.Size(255, 22);
            this.mnuExit.Text = "E&xit";
            // 
            // mnuDeleteJob
            // 
            this.mnuDeleteJob.Name = "mnuDeleteJob";
            this.mnuDeleteJob.Size = new System.Drawing.Size(255, 22);
            this.mnuDeleteJob.Text = "&Delete Job Plan";
            this.mnuDeleteJob.Click += new System.EventHandler(this.mnuDeleteJob_Click);
            // 
            // cmsJobPlans
            // 
            this.cmsJobPlans.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCopyAll,
            this.tsmiCopySelected,
            this.toolStripSeparator6,
            this.tsmiPaste});
            this.cmsJobPlans.Name = "cmsJobPlans";
            this.cmsJobPlans.Size = new System.Drawing.Size(177, 76);
            // 
            // tsmiCopyAll
            // 
            this.tsmiCopyAll.Name = "tsmiCopyAll";
            this.tsmiCopyAll.Size = new System.Drawing.Size(176, 22);
            this.tsmiCopyAll.Text = "Copy All Jobs";
            this.tsmiCopyAll.Click += new System.EventHandler(this.tsmiCopyAll_Click);
            // 
            // tsmiCopySelected
            // 
            this.tsmiCopySelected.Name = "tsmiCopySelected";
            this.tsmiCopySelected.Size = new System.Drawing.Size(176, 22);
            this.tsmiCopySelected.Text = "Copy Selected Item";
            this.tsmiCopySelected.Click += new System.EventHandler(this.tsmiCopySelected_Click);
            // 
            // tsmiPaste
            // 
            this.tsmiPaste.Name = "tsmiPaste";
            this.tsmiPaste.Size = new System.Drawing.Size(176, 22);
            this.tsmiPaste.Text = "Paste Items";
            this.tsmiPaste.Click += new System.EventHandler(this.tsmiPaste_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(173, 6);
            // 
            // colJobTitle
            // 
            this.colJobTitle.DataPropertyName = "JobTitle";
            this.colJobTitle.HeaderText = "Job Title";
            this.colJobTitle.Name = "colJobTitle";
            this.colJobTitle.Width = 150;
            // 
            // colShiftStart
            // 
            this.colShiftStart.DataPropertyName = "ShiftStart";
            this.colShiftStart.HeaderText = "Start Time";
            this.colShiftStart.Name = "colShiftStart";
            this.colShiftStart.Width = 70;
            // 
            // colShiftEnd
            // 
            this.colShiftEnd.DataPropertyName = "ShiftEnd";
            this.colShiftEnd.HeaderText = "End Time";
            this.colShiftEnd.Name = "colShiftEnd";
            this.colShiftEnd.Width = 70;
            // 
            // colPrimary
            // 
            this.colPrimary.DataPropertyName = "VolIDPrimary";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.colPrimary.DefaultCellStyle = dataGridViewCellStyle2;
            this.colPrimary.HeaderText = "Primary";
            this.colPrimary.Name = "colPrimary";
            this.colPrimary.ReadOnly = true;
            this.colPrimary.Width = 150;
            // 
            // colBackup
            // 
            this.colBackup.DataPropertyName = "VolIDBackup";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.colBackup.DefaultCellStyle = dataGridViewCellStyle3;
            this.colBackup.HeaderText = "Backup";
            this.colBackup.Name = "colBackup";
            this.colBackup.ReadOnly = true;
            this.colBackup.Width = 150;
            // 
            // FBJobsPlanForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(879, 662);
            this.Controls.Add(this.spltcMain);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FBJobsPlanForm";
            this.Text = "Food Bank Jobs Plan Form";
            this.spltcMain.Panel1.ResumeLayout(false);
            this.spltcMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spltcMain)).EndInit();
            this.spltcMain.ResumeLayout(false);
            this.spltcJobPlan.Panel1.ResumeLayout(false);
            this.spltcJobPlan.Panel1.PerformLayout();
            this.spltcJobPlan.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spltcJobPlan)).EndInit();
            this.spltcJobPlan.ResumeLayout(false);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvJobPlans)).EndInit();
            this.spltcJobs.Panel1.ResumeLayout(false);
            this.spltcJobs.Panel1.PerformLayout();
            this.spltcJobs.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spltcJobs)).EndInit();
            this.spltcJobs.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.cmsJobPlans.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer spltcMain;
        private System.Windows.Forms.SplitContainer spltcJobPlan;
        private System.Windows.Forms.DataGridView dgvJobPlans;
        private System.Windows.Forms.ListView lvwJobs;
        private System.Windows.Forms.Button btnAddJobsToPlan;
        private System.Windows.Forms.TextBox tbStart;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbEnd;
        private System.Windows.Forms.SplitContainer spltcJobs;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbShowJobs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbShowVols;
        private System.Windows.Forms.ListView lvwVols;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton tsb0;
        private System.Windows.Forms.ToolStripButton tsb1;
        private System.Windows.Forms.ToolStripButton tsb2;
        private System.Windows.Forms.ToolStripButton tsb3;
        private System.Windows.Forms.ToolStripButton tsb4;
        private System.Windows.Forms.ToolStripButton tsb5;
        private System.Windows.Forms.ToolStripButton tsb6;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuAction;
        private System.Windows.Forms.ToolStripMenuItem mnuAddJobs;
        private System.Windows.Forms.ToolStripMenuItem mnuAddPrimary;
        private System.Windows.Forms.ToolStripMenuItem mnuAddBackup;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem mnuDeletePrimary;
        private System.Windows.Forms.ToolStripMenuItem mnuDeleteBackup;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem mnuExit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ColumnHeader col1;
        private System.Windows.Forms.ToolStripMenuItem mnuDeleteJob;
        private System.Windows.Forms.ContextMenuStrip cmsJobPlans;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopyAll;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopySelected;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem tsmiPaste;
        private System.Windows.Forms.DataGridViewTextBoxColumn colJobTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn colShiftStart;
        private System.Windows.Forms.DataGridViewTextBoxColumn colShiftEnd;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPrimary;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBackup;
    }
}