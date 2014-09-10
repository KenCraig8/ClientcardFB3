namespace ClientcardFB3
{
    partial class AccessReportsForm
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
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Clients", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Services", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "",
            "my report"}, -1);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
            "",
            "another report"}, -1, System.Drawing.Color.Empty, System.Drawing.Color.WhiteSmoke, null);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AccessReportsForm));
            this.lblType5Hi = new System.Windows.Forms.Label();
            this.lblType5Low = new System.Windows.Forms.Label();
            this.dtTo = new System.Windows.Forms.DateTimePicker();
            this.dtFrom = new System.Windows.Forms.DateTimePicker();
            this.lblType1 = new System.Windows.Forms.Label();
            this.dtDate = new System.Windows.Forms.DateTimePicker();
            this.tbYearTo = new System.Windows.Forms.TextBox();
            this.lblType7Hi = new System.Windows.Forms.Label();
            this.tbYearFrom = new System.Windows.Forms.TextBox();
            this.lblType7Low = new System.Windows.Forms.Label();
            this.cboUserSelection = new System.Windows.Forms.ComboBox();
            this.lblType2 = new System.Windows.Forms.Label();
            this.chkPreview = new System.Windows.Forms.CheckBox();
            this.btnCreateReport = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lvwRptGroups = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwRptSelector = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.grpbxActive = new System.Windows.Forms.GroupBox();
            this.rdoOnlyInactive = new System.Windows.Forms.RadioButton();
            this.rdoOnlyActive = new System.Windows.Forms.RadioButton();
            this.rdoAll = new System.Windows.Forms.RadioButton();
            this.pnlCustomCombo = new System.Windows.Forms.Panel();
            this.pnlYearRange = new System.Windows.Forms.Panel();
            this.pnlDateRange = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlDatePicker = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lvwFilter = new System.Windows.Forms.ListView();
            this.chkAllItems = new System.Windows.Forms.CheckBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.lblCopies = new System.Windows.Forms.Label();
            this.grpbxActive.SuspendLayout();
            this.pnlCustomCombo.SuspendLayout();
            this.pnlYearRange.SuspendLayout();
            this.pnlDateRange.SuspendLayout();
            this.pnlDatePicker.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblType5Hi
            // 
            this.lblType5Hi.AutoSize = true;
            this.lblType5Hi.Location = new System.Drawing.Point(45, 74);
            this.lblType5Hi.Name = "lblType5Hi";
            this.lblType5Hi.Size = new System.Drawing.Size(24, 15);
            this.lblType5Hi.TabIndex = 3;
            this.lblType5Hi.Text = "To:";
            // 
            // lblType5Low
            // 
            this.lblType5Low.AutoSize = true;
            this.lblType5Low.Location = new System.Drawing.Point(45, 27);
            this.lblType5Low.Name = "lblType5Low";
            this.lblType5Low.Size = new System.Drawing.Size(39, 15);
            this.lblType5Low.TabIndex = 2;
            this.lblType5Low.Text = "From:";
            // 
            // dtTo
            // 
            this.dtTo.Location = new System.Drawing.Point(45, 90);
            this.dtTo.Name = "dtTo";
            this.dtTo.Size = new System.Drawing.Size(204, 21);
            this.dtTo.TabIndex = 1;
            this.dtTo.Tag = "Date1";
            this.dtTo.Leave += new System.EventHandler(this.dtp_Leave);
            // 
            // dtFrom
            // 
            this.dtFrom.CalendarFont = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtFrom.CalendarForeColor = System.Drawing.Color.Black;
            this.dtFrom.CalendarMonthBackground = System.Drawing.Color.FloralWhite;
            this.dtFrom.CalendarTitleBackColor = System.Drawing.Color.MediumSeaGreen;
            this.dtFrom.CalendarTitleForeColor = System.Drawing.Color.Black;
            this.dtFrom.CalendarTrailingForeColor = System.Drawing.Color.DarkGray;
            this.dtFrom.Location = new System.Drawing.Point(45, 44);
            this.dtFrom.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.dtFrom.Name = "dtFrom";
            this.dtFrom.Size = new System.Drawing.Size(204, 21);
            this.dtFrom.TabIndex = 0;
            this.dtFrom.Tag = "Date0";
            this.dtFrom.Leave += new System.EventHandler(this.dtp_Leave);
            // 
            // lblType1
            // 
            this.lblType1.AutoSize = true;
            this.lblType1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblType1.Location = new System.Drawing.Point(545, 7);
            this.lblType1.Name = "lblType1";
            this.lblType1.Size = new System.Drawing.Size(47, 17);
            this.lblType1.TabIndex = 2;
            this.lblType1.Text = "Date:";
            this.lblType1.Visible = false;
            // 
            // dtDate
            // 
            this.dtDate.Location = new System.Drawing.Point(8, 35);
            this.dtDate.Name = "dtDate";
            this.dtDate.Size = new System.Drawing.Size(233, 21);
            this.dtDate.TabIndex = 0;
            this.dtDate.Tag = "Date0";
            this.dtDate.Leave += new System.EventHandler(this.dtp_Leave);
            // 
            // tbYearTo
            // 
            this.tbYearTo.Location = new System.Drawing.Point(17, 67);
            this.tbYearTo.MaxLength = 4;
            this.tbYearTo.Name = "tbYearTo";
            this.tbYearTo.Size = new System.Drawing.Size(102, 21);
            this.tbYearTo.TabIndex = 3;
            this.tbYearTo.Tag = "Date1";
            this.tbYearTo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbYearTo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbYear_KeyDown);
            this.tbYearTo.Leave += new System.EventHandler(this.tb_Leave);
            // 
            // lblType7Hi
            // 
            this.lblType7Hi.AutoSize = true;
            this.lblType7Hi.Location = new System.Drawing.Point(3, 50);
            this.lblType7Hi.Name = "lblType7Hi";
            this.lblType7Hi.Size = new System.Drawing.Size(60, 15);
            this.lblType7Hi.TabIndex = 2;
            this.lblType7Hi.Text = "To(YYYY):";
            // 
            // tbYearFrom
            // 
            this.tbYearFrom.Location = new System.Drawing.Point(19, 23);
            this.tbYearFrom.MaxLength = 4;
            this.tbYearFrom.Name = "tbYearFrom";
            this.tbYearFrom.Size = new System.Drawing.Size(100, 21);
            this.tbYearFrom.TabIndex = 1;
            this.tbYearFrom.Tag = "Date0";
            this.tbYearFrom.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbYearFrom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbYear_KeyDown);
            this.tbYearFrom.Leave += new System.EventHandler(this.tb_Leave);
            // 
            // lblType7Low
            // 
            this.lblType7Low.AutoSize = true;
            this.lblType7Low.Location = new System.Drawing.Point(10, 5);
            this.lblType7Low.Name = "lblType7Low";
            this.lblType7Low.Size = new System.Drawing.Size(75, 15);
            this.lblType7Low.TabIndex = 0;
            this.lblType7Low.Text = "From(YYYY):";
            // 
            // cboUserSelection
            // 
            this.cboUserSelection.FormattingEnabled = true;
            this.cboUserSelection.Location = new System.Drawing.Point(3, 27);
            this.cboUserSelection.Name = "cboUserSelection";
            this.cboUserSelection.Size = new System.Drawing.Size(152, 23);
            this.cboUserSelection.TabIndex = 3;
            this.cboUserSelection.Tag = "Date0";
            this.cboUserSelection.Leave += new System.EventHandler(this.cboUserSelection_Leave);
            // 
            // lblType2
            // 
            this.lblType2.AutoSize = true;
            this.lblType2.Location = new System.Drawing.Point(3, 9);
            this.lblType2.Name = "lblType2";
            this.lblType2.Size = new System.Drawing.Size(37, 15);
            this.lblType2.TabIndex = 2;
            this.lblType2.Text = "Items";
            // 
            // chkPreview
            // 
            this.chkPreview.AutoSize = true;
            this.chkPreview.Checked = true;
            this.chkPreview.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPreview.Location = new System.Drawing.Point(554, 357);
            this.chkPreview.Name = "chkPreview";
            this.chkPreview.Size = new System.Drawing.Size(69, 19);
            this.chkPreview.TabIndex = 6;
            this.chkPreview.Text = "Preview";
            this.chkPreview.UseVisualStyleBackColor = true;
            this.chkPreview.CheckedChanged += new System.EventHandler(this.chkPreview_CheckedChanged);
            // 
            // btnCreateReport
            // 
            this.btnCreateReport.Enabled = false;
            this.btnCreateReport.Location = new System.Drawing.Point(543, 382);
            this.btnCreateReport.Name = "btnCreateReport";
            this.btnCreateReport.Size = new System.Drawing.Size(98, 38);
            this.btnCreateReport.TabIndex = 7;
            this.btnCreateReport.Text = "Create Report";
            this.btnCreateReport.UseVisualStyleBackColor = true;
            this.btnCreateReport.Click += new System.EventHandler(this.btnCreateReport_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(863, 382);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(98, 38);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lvwRptGroups
            // 
            this.lvwRptGroups.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lvwRptGroups.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lvwRptGroups.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvwRptGroups.ForeColor = System.Drawing.Color.SaddleBrown;
            this.lvwRptGroups.FullRowSelect = true;
            this.lvwRptGroups.GridLines = true;
            this.lvwRptGroups.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvwRptGroups.HideSelection = false;
            this.lvwRptGroups.Location = new System.Drawing.Point(2, 2);
            this.lvwRptGroups.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.lvwRptGroups.MultiSelect = false;
            this.lvwRptGroups.Name = "lvwRptGroups";
            this.lvwRptGroups.Size = new System.Drawing.Size(233, 543);
            this.lvwRptGroups.TabIndex = 13;
            this.lvwRptGroups.UseCompatibleStateImageBehavior = false;
            this.lvwRptGroups.View = System.Windows.Forms.View.Details;
            this.lvwRptGroups.SelectedIndexChanged += new System.EventHandler(this.lvwRptGroups_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Report Category";
            this.columnHeader1.Width = 227;
            // 
            // lvwRptSelector
            // 
            this.lvwRptSelector.BackColor = System.Drawing.Color.Snow;
            this.lvwRptSelector.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader2});
            this.lvwRptSelector.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvwRptSelector.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lvwRptSelector.FullRowSelect = true;
            listViewGroup1.Header = "Clients";
            listViewGroup1.Name = "grpClient";
            listViewGroup2.Header = "Services";
            listViewGroup2.Name = "grpServices";
            this.lvwRptSelector.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2});
            this.lvwRptSelector.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvwRptSelector.HideSelection = false;
            listViewItem1.Group = listViewGroup1;
            listViewItem1.IndentCount = 10;
            listViewItem2.Group = listViewGroup2;
            listViewItem2.IndentCount = 10;
            this.lvwRptSelector.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
            this.lvwRptSelector.Location = new System.Drawing.Point(235, 27);
            this.lvwRptSelector.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.lvwRptSelector.MultiSelect = false;
            this.lvwRptSelector.Name = "lvwRptSelector";
            this.lvwRptSelector.Size = new System.Drawing.Size(302, 518);
            this.lvwRptSelector.TabIndex = 14;
            this.lvwRptSelector.UseCompatibleStateImageBehavior = false;
            this.lvwRptSelector.View = System.Windows.Forms.View.Details;
            this.lvwRptSelector.SelectedIndexChanged += new System.EventHandler(this.lvwRptSelector_SelectedIndexChanged);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "";
            this.columnHeader3.Width = 40;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Report Selector";
            this.columnHeader2.Width = 257;
            // 
            // grpbxActive
            // 
            this.grpbxActive.Controls.Add(this.rdoOnlyInactive);
            this.grpbxActive.Controls.Add(this.rdoOnlyActive);
            this.grpbxActive.Controls.Add(this.rdoAll);
            this.grpbxActive.Location = new System.Drawing.Point(540, 242);
            this.grpbxActive.Name = "grpbxActive";
            this.grpbxActive.Size = new System.Drawing.Size(158, 100);
            this.grpbxActive.TabIndex = 15;
            this.grpbxActive.TabStop = false;
            this.grpbxActive.Visible = false;
            // 
            // rdoOnlyInactive
            // 
            this.rdoOnlyInactive.AutoSize = true;
            this.rdoOnlyInactive.Location = new System.Drawing.Point(43, 72);
            this.rdoOnlyInactive.Name = "rdoOnlyInactive";
            this.rdoOnlyInactive.Size = new System.Drawing.Size(93, 19);
            this.rdoOnlyInactive.TabIndex = 2;
            this.rdoOnlyInactive.Text = "Only Inactive";
            this.rdoOnlyInactive.UseVisualStyleBackColor = true;
            // 
            // rdoOnlyActive
            // 
            this.rdoOnlyActive.AutoSize = true;
            this.rdoOnlyActive.Checked = true;
            this.rdoOnlyActive.Location = new System.Drawing.Point(43, 46);
            this.rdoOnlyActive.Name = "rdoOnlyActive";
            this.rdoOnlyActive.Size = new System.Drawing.Size(86, 19);
            this.rdoOnlyActive.TabIndex = 1;
            this.rdoOnlyActive.TabStop = true;
            this.rdoOnlyActive.Text = "Only Active ";
            this.rdoOnlyActive.UseVisualStyleBackColor = true;
            // 
            // rdoAll
            // 
            this.rdoAll.AutoSize = true;
            this.rdoAll.Location = new System.Drawing.Point(8, 21);
            this.rdoAll.Name = "rdoAll";
            this.rdoAll.Size = new System.Drawing.Size(87, 19);
            this.rdoAll.TabIndex = 0;
            this.rdoAll.Text = "All Records";
            this.rdoAll.UseVisualStyleBackColor = true;
            // 
            // pnlCustomCombo
            // 
            this.pnlCustomCombo.Controls.Add(this.cboUserSelection);
            this.pnlCustomCombo.Controls.Add(this.lblType2);
            this.pnlCustomCombo.Location = new System.Drawing.Point(543, 457);
            this.pnlCustomCombo.Name = "pnlCustomCombo";
            this.pnlCustomCombo.Size = new System.Drawing.Size(164, 64);
            this.pnlCustomCombo.TabIndex = 16;
            // 
            // pnlYearRange
            // 
            this.pnlYearRange.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlYearRange.Controls.Add(this.tbYearTo);
            this.pnlYearRange.Controls.Add(this.lblType7Low);
            this.pnlYearRange.Controls.Add(this.lblType7Hi);
            this.pnlYearRange.Controls.Add(this.tbYearFrom);
            this.pnlYearRange.Location = new System.Drawing.Point(679, 348);
            this.pnlYearRange.Name = "pnlYearRange";
            this.pnlYearRange.Size = new System.Drawing.Size(143, 94);
            this.pnlYearRange.TabIndex = 17;
            // 
            // pnlDateRange
            // 
            this.pnlDateRange.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlDateRange.Controls.Add(this.label1);
            this.pnlDateRange.Controls.Add(this.dtFrom);
            this.pnlDateRange.Controls.Add(this.lblType5Hi);
            this.pnlDateRange.Controls.Add(this.lblType5Low);
            this.pnlDateRange.Controls.Add(this.dtTo);
            this.pnlDateRange.Location = new System.Drawing.Point(710, 242);
            this.pnlDateRange.Name = "pnlDateRange";
            this.pnlDateRange.Size = new System.Drawing.Size(262, 126);
            this.pnlDateRange.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "Enter Date Range";
            // 
            // pnlDatePicker
            // 
            this.pnlDatePicker.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlDatePicker.Controls.Add(this.dtDate);
            this.pnlDatePicker.Controls.Add(this.label2);
            this.pnlDatePicker.Location = new System.Drawing.Point(710, 457);
            this.pnlDatePicker.Name = "pnlDatePicker";
            this.pnlDatePicker.Size = new System.Drawing.Size(262, 73);
            this.pnlDatePicker.TabIndex = 19;
            this.pnlDatePicker.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlDatePicker_Paint);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "Enter Report Date";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lvwFilter);
            this.panel1.Location = new System.Drawing.Point(543, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(437, 209);
            this.panel1.TabIndex = 20;
            // 
            // lvwFilter
            // 
            this.lvwFilter.CheckBoxes = true;
            this.lvwFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwFilter.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvwFilter.Location = new System.Drawing.Point(0, 0);
            this.lvwFilter.Name = "lvwFilter";
            this.lvwFilter.Size = new System.Drawing.Size(437, 209);
            this.lvwFilter.TabIndex = 0;
            this.lvwFilter.UseCompatibleStateImageBehavior = false;
            this.lvwFilter.View = System.Windows.Forms.View.List;
            // 
            // chkAllItems
            // 
            this.chkAllItems.AutoSize = true;
            this.chkAllItems.Checked = true;
            this.chkAllItems.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAllItems.Location = new System.Drawing.Point(548, 7);
            this.chkAllItems.Name = "chkAllItems";
            this.chkAllItems.Size = new System.Drawing.Size(72, 19);
            this.chkAllItems.TabIndex = 21;
            this.chkAllItems.Text = "All Items";
            this.chkAllItems.UseVisualStyleBackColor = true;
            this.chkAllItems.Visible = false;
            this.chkAllItems.CheckedChanged += new System.EventHandler(this.chkAllItems_CheckedChanged);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(556, 428);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(40, 21);
            this.numericUpDown1.TabIndex = 22;
            this.numericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // lblCopies
            // 
            this.lblCopies.AutoSize = true;
            this.lblCopies.Location = new System.Drawing.Point(600, 430);
            this.lblCopies.Name = "lblCopies";
            this.lblCopies.Size = new System.Drawing.Size(45, 15);
            this.lblCopies.TabIndex = 23;
            this.lblCopies.Text = "Copies";
            // 
            // AccessReportsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Cornsilk;
            this.ClientSize = new System.Drawing.Size(984, 557);
            this.Controls.Add(this.lblCopies);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.pnlYearRange);
            this.Controls.Add(this.lblType1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlDatePicker);
            this.Controls.Add(this.pnlCustomCombo);
            this.Controls.Add(this.pnlDateRange);
            this.Controls.Add(this.grpbxActive);
            this.Controls.Add(this.lvwRptSelector);
            this.Controls.Add(this.lvwRptGroups);
            this.Controls.Add(this.btnCreateReport);
            this.Controls.Add(this.chkPreview);
            this.Controls.Add(this.chkAllItems);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AccessReportsForm";
            this.Text = "AccessReportsForm";
            this.Load += new System.EventHandler(this.AccessReportsForm_Load);
            this.grpbxActive.ResumeLayout(false);
            this.grpbxActive.PerformLayout();
            this.pnlCustomCombo.ResumeLayout(false);
            this.pnlCustomCombo.PerformLayout();
            this.pnlYearRange.ResumeLayout(false);
            this.pnlYearRange.PerformLayout();
            this.pnlDateRange.ResumeLayout(false);
            this.pnlDateRange.PerformLayout();
            this.pnlDatePicker.ResumeLayout(false);
            this.pnlDatePicker.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblType5Hi;
        private System.Windows.Forms.Label lblType5Low;
        private System.Windows.Forms.DateTimePicker dtTo;
        private System.Windows.Forms.DateTimePicker dtFrom;
        private System.Windows.Forms.Label lblType1;
        private System.Windows.Forms.DateTimePicker dtDate;
        private System.Windows.Forms.ComboBox cboUserSelection;
        private System.Windows.Forms.Label lblType2;
        private System.Windows.Forms.CheckBox chkPreview;
        private System.Windows.Forms.Button btnCreateReport;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TextBox tbYearTo;
        private System.Windows.Forms.Label lblType7Hi;
        private System.Windows.Forms.TextBox tbYearFrom;
        private System.Windows.Forms.Label lblType7Low;
        private System.Windows.Forms.ListView lvwRptGroups;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ListView lvwRptSelector;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.GroupBox grpbxActive;
        private System.Windows.Forms.RadioButton rdoOnlyActive;
        private System.Windows.Forms.RadioButton rdoAll;
        private System.Windows.Forms.RadioButton rdoOnlyInactive;
        private System.Windows.Forms.Panel pnlCustomCombo;
        private System.Windows.Forms.Panel pnlYearRange;
        private System.Windows.Forms.Panel pnlDateRange;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlDatePicker;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView lvwFilter;
        private System.Windows.Forms.CheckBox chkAllItems;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label lblCopies;
    }
}