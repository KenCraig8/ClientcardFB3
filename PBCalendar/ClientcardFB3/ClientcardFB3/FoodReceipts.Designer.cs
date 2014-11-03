namespace ClientcardFB3
{
    partial class frmFoodReceipts
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cboYear = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cboReportMonth = new System.Windows.Forms.ComboBox();
            this.btnLoadPeriodData = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cboStore = new System.Windows.Forms.ComboBox();
            this.grpWeek = new System.Windows.Forms.GroupBox();
            this.rdoWeek6 = new System.Windows.Forms.RadioButton();
            this.rdoWeek5 = new System.Windows.Forms.RadioButton();
            this.rdoWeek4 = new System.Windows.Forms.RadioButton();
            this.rdoWeek3 = new System.Windows.Forms.RadioButton();
            this.rdoWeek2 = new System.Windows.Forms.RadioButton();
            this.rdoWeek1 = new System.Windows.Forms.RadioButton();
            this.btnClose = new System.Windows.Forms.Button();
            this.dgvMonthReceipts = new System.Windows.Forms.DataGridView();
            this.colFoodClass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColMonday = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTuesday = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colWednesday = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colThursday = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFriday = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSaturday = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmSunday = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvMonthTotals = new System.Windows.Forms.DataGridView();
            this.clmWeekFoodClass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmWeek1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmWeek2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmWeek3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmWeek4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmWeek5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmWeek6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmWeekTotals = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlMonthly = new System.Windows.Forms.Panel();
            this.pnlEditDaily = new System.Windows.Forms.Panel();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnSummary = new System.Windows.Forms.Button();
            this.cboDonationType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnEmailRpt = new System.Windows.Forms.Button();
            this.grpWeek.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMonthReceipts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMonthTotals)).BeginInit();
            this.pnlMonthly.SuspendLayout();
            this.pnlEditDaily.SuspendLayout();
            this.SuspendLayout();
            // 
            // cboYear
            // 
            this.cboYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboYear.FormattingEnabled = true;
            this.cboYear.Location = new System.Drawing.Point(12, 78);
            this.cboYear.Name = "cboYear";
            this.cboYear.Size = new System.Drawing.Size(61, 24);
            this.cboYear.TabIndex = 1;
            this.cboYear.SelectedIndexChanged += new System.EventHandler(this.cboYear_SelectedIndexChanged);
            this.cboYear.SelectionChangeCommitted += new System.EventHandler(this.cbo_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 38;
            this.label1.Text = "Year";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(76, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 39;
            this.label2.Text = "Month";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // cboReportMonth
            // 
            this.cboReportMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboReportMonth.FormattingEnabled = true;
            this.cboReportMonth.Items.AddRange(new object[] {
            "January",
            "February",
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
            this.cboReportMonth.Location = new System.Drawing.Point(79, 78);
            this.cboReportMonth.Name = "cboReportMonth";
            this.cboReportMonth.Size = new System.Drawing.Size(107, 24);
            this.cboReportMonth.TabIndex = 40;
            this.cboReportMonth.SelectedIndexChanged += new System.EventHandler(this.cboReportMonth_SelectedIndexChanged);
            this.cboReportMonth.SelectionChangeCommitted += new System.EventHandler(this.cbo_SelectionChangeCommitted);
            // 
            // btnLoadPeriodData
            // 
            this.btnLoadPeriodData.Location = new System.Drawing.Point(207, 73);
            this.btnLoadPeriodData.Name = "btnLoadPeriodData";
            this.btnLoadPeriodData.Size = new System.Drawing.Size(110, 32);
            this.btnLoadPeriodData.TabIndex = 41;
            this.btnLoadPeriodData.Text = "Load Period Data";
            this.btnLoadPeriodData.UseVisualStyleBackColor = true;
            this.btnLoadPeriodData.Click += new System.EventHandler(this.btnLoadPeriodData_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 43;
            this.label3.Text = "Store Name";
            // 
            // cboStore
            // 
            this.cboStore.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboStore.FormattingEnabled = true;
            this.cboStore.Location = new System.Drawing.Point(12, 30);
            this.cboStore.Name = "cboStore";
            this.cboStore.Size = new System.Drawing.Size(305, 24);
            this.cboStore.TabIndex = 42;
            this.cboStore.SelectionChangeCommitted += new System.EventHandler(this.cbo_SelectionChangeCommitted);
            // 
            // grpWeek
            // 
            this.grpWeek.Controls.Add(this.rdoWeek6);
            this.grpWeek.Controls.Add(this.rdoWeek5);
            this.grpWeek.Controls.Add(this.rdoWeek4);
            this.grpWeek.Controls.Add(this.rdoWeek3);
            this.grpWeek.Controls.Add(this.rdoWeek2);
            this.grpWeek.Controls.Add(this.rdoWeek1);
            this.grpWeek.Location = new System.Drawing.Point(188, 0);
            this.grpWeek.Name = "grpWeek";
            this.grpWeek.Size = new System.Drawing.Size(452, 37);
            this.grpWeek.TabIndex = 32;
            this.grpWeek.TabStop = false;
            // 
            // rdoWeek6
            // 
            this.rdoWeek6.AutoSize = true;
            this.rdoWeek6.Location = new System.Drawing.Point(372, 12);
            this.rdoWeek6.Name = "rdoWeek6";
            this.rdoWeek6.Size = new System.Drawing.Size(63, 17);
            this.rdoWeek6.TabIndex = 5;
            this.rdoWeek6.Tag = "5";
            this.rdoWeek6.Text = "Week 6";
            this.rdoWeek6.UseVisualStyleBackColor = true;
            this.rdoWeek6.CheckedChanged += new System.EventHandler(this.rdoWeek_CheckedChanged);
            // 
            // rdoWeek5
            // 
            this.rdoWeek5.AutoSize = true;
            this.rdoWeek5.Location = new System.Drawing.Point(294, 12);
            this.rdoWeek5.Name = "rdoWeek5";
            this.rdoWeek5.Size = new System.Drawing.Size(63, 17);
            this.rdoWeek5.TabIndex = 4;
            this.rdoWeek5.Tag = "4";
            this.rdoWeek5.Text = "Week 5";
            this.rdoWeek5.UseVisualStyleBackColor = true;
            this.rdoWeek5.CheckedChanged += new System.EventHandler(this.rdoWeek_CheckedChanged);
            // 
            // rdoWeek4
            // 
            this.rdoWeek4.AutoSize = true;
            this.rdoWeek4.Location = new System.Drawing.Point(225, 12);
            this.rdoWeek4.Name = "rdoWeek4";
            this.rdoWeek4.Size = new System.Drawing.Size(63, 17);
            this.rdoWeek4.TabIndex = 3;
            this.rdoWeek4.Tag = "3";
            this.rdoWeek4.Text = "Week 4";
            this.rdoWeek4.UseVisualStyleBackColor = true;
            this.rdoWeek4.CheckedChanged += new System.EventHandler(this.rdoWeek_CheckedChanged);
            // 
            // rdoWeek3
            // 
            this.rdoWeek3.AutoSize = true;
            this.rdoWeek3.Location = new System.Drawing.Point(156, 12);
            this.rdoWeek3.Name = "rdoWeek3";
            this.rdoWeek3.Size = new System.Drawing.Size(63, 17);
            this.rdoWeek3.TabIndex = 2;
            this.rdoWeek3.Tag = "2";
            this.rdoWeek3.Text = "Week 3";
            this.rdoWeek3.UseVisualStyleBackColor = true;
            this.rdoWeek3.CheckedChanged += new System.EventHandler(this.rdoWeek_CheckedChanged);
            // 
            // rdoWeek2
            // 
            this.rdoWeek2.AutoSize = true;
            this.rdoWeek2.Location = new System.Drawing.Point(84, 12);
            this.rdoWeek2.Name = "rdoWeek2";
            this.rdoWeek2.Size = new System.Drawing.Size(63, 17);
            this.rdoWeek2.TabIndex = 1;
            this.rdoWeek2.Tag = "1";
            this.rdoWeek2.Text = "Week 2";
            this.rdoWeek2.UseVisualStyleBackColor = true;
            this.rdoWeek2.CheckedChanged += new System.EventHandler(this.rdoWeek_CheckedChanged);
            // 
            // rdoWeek1
            // 
            this.rdoWeek1.AutoSize = true;
            this.rdoWeek1.Checked = true;
            this.rdoWeek1.Location = new System.Drawing.Point(15, 12);
            this.rdoWeek1.Name = "rdoWeek1";
            this.rdoWeek1.Size = new System.Drawing.Size(63, 17);
            this.rdoWeek1.TabIndex = 0;
            this.rdoWeek1.TabStop = true;
            this.rdoWeek1.Tag = "0";
            this.rdoWeek1.Text = "Week 1";
            this.rdoWeek1.UseVisualStyleBackColor = true;
            this.rdoWeek1.Click += new System.EventHandler(this.rdoWeek_CheckedChanged);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(896, 8);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(82, 32);
            this.btnClose.TabIndex = 34;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // dgvMonthReceipts
            // 
            this.dgvMonthReceipts.AllowUserToAddRows = false;
            this.dgvMonthReceipts.AllowUserToDeleteRows = false;
            this.dgvMonthReceipts.AllowUserToOrderColumns = true;
            this.dgvMonthReceipts.AllowUserToResizeRows = false;
            this.dgvMonthReceipts.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMonthReceipts.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvMonthReceipts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMonthReceipts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colFoodClass,
            this.ColMonday,
            this.colTuesday,
            this.colWednesday,
            this.colThursday,
            this.colFriday,
            this.colSaturday,
            this.clmSunday,
            this.colTotal});
            this.dgvMonthReceipts.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvMonthReceipts.GridColor = System.Drawing.Color.Gray;
            this.dgvMonthReceipts.Location = new System.Drawing.Point(0, 38);
            this.dgvMonthReceipts.Name = "dgvMonthReceipts";
            this.dgvMonthReceipts.RowHeadersVisible = false;
            this.dgvMonthReceipts.RowHeadersWidth = 5;
            this.dgvMonthReceipts.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvMonthReceipts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvMonthReceipts.Size = new System.Drawing.Size(980, 295);
            this.dgvMonthReceipts.TabIndex = 1;
            this.dgvMonthReceipts.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMonthReceipts_CellEndEdit);
            this.dgvMonthReceipts.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMonthReceipts_CellEnter);
            this.dgvMonthReceipts.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvMonthReceipts_CellValidating);
            // 
            // colFoodClass
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.colFoodClass.DefaultCellStyle = dataGridViewCellStyle2;
            this.colFoodClass.HeaderText = "Food Class";
            this.colFoodClass.Name = "colFoodClass";
            this.colFoodClass.ReadOnly = true;
            this.colFoodClass.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colFoodClass.Width = 180;
            // 
            // ColMonday
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ColMonday.DefaultCellStyle = dataGridViewCellStyle3;
            this.ColMonday.HeaderText = "Monday";
            this.ColMonday.Name = "ColMonday";
            this.ColMonday.ReadOnly = true;
            this.ColMonday.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColMonday.Width = 95;
            // 
            // colTuesday
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colTuesday.DefaultCellStyle = dataGridViewCellStyle4;
            this.colTuesday.HeaderText = "Tuesday";
            this.colTuesday.Name = "colTuesday";
            this.colTuesday.ReadOnly = true;
            this.colTuesday.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colTuesday.Width = 95;
            // 
            // colWednesday
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colWednesday.DefaultCellStyle = dataGridViewCellStyle5;
            this.colWednesday.HeaderText = "Wednesday";
            this.colWednesday.Name = "colWednesday";
            this.colWednesday.ReadOnly = true;
            this.colWednesday.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colWednesday.Width = 95;
            // 
            // colThursday
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colThursday.DefaultCellStyle = dataGridViewCellStyle6;
            this.colThursday.HeaderText = "Thursday";
            this.colThursday.Name = "colThursday";
            this.colThursday.ReadOnly = true;
            this.colThursday.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colThursday.Width = 95;
            // 
            // colFriday
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colFriday.DefaultCellStyle = dataGridViewCellStyle7;
            this.colFriday.HeaderText = "Friday";
            this.colFriday.Name = "colFriday";
            this.colFriday.ReadOnly = true;
            this.colFriday.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colFriday.Width = 95;
            // 
            // colSaturday
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colSaturday.DefaultCellStyle = dataGridViewCellStyle8;
            this.colSaturday.HeaderText = "Saturday";
            this.colSaturday.Name = "colSaturday";
            this.colSaturday.ReadOnly = true;
            this.colSaturday.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colSaturday.Width = 95;
            // 
            // clmSunday
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.clmSunday.DefaultCellStyle = dataGridViewCellStyle9;
            this.clmSunday.HeaderText = "Sunday";
            this.clmSunday.Name = "clmSunday";
            this.clmSunday.ReadOnly = true;
            this.clmSunday.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.clmSunday.Width = 95;
            // 
            // colTotal
            // 
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.colTotal.DefaultCellStyle = dataGridViewCellStyle10;
            this.colTotal.HeaderText = "Totals";
            this.colTotal.Name = "colTotal";
            this.colTotal.ReadOnly = true;
            this.colTotal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colTotal.Width = 95;
            // 
            // dgvMonthTotals
            // 
            this.dgvMonthTotals.AllowUserToAddRows = false;
            this.dgvMonthTotals.AllowUserToDeleteRows = false;
            this.dgvMonthTotals.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMonthTotals.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.dgvMonthTotals.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMonthTotals.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmWeekFoodClass,
            this.clmWeek1,
            this.clmWeek2,
            this.clmWeek3,
            this.clmWeek4,
            this.clmWeek5,
            this.clmWeek6,
            this.clmWeekTotals});
            this.dgvMonthTotals.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMonthTotals.GridColor = System.Drawing.Color.Gray;
            this.dgvMonthTotals.Location = new System.Drawing.Point(0, 0);
            this.dgvMonthTotals.Name = "dgvMonthTotals";
            this.dgvMonthTotals.RowHeadersVisible = false;
            this.dgvMonthTotals.RowHeadersWidth = 5;
            this.dgvMonthTotals.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvMonthTotals.Size = new System.Drawing.Size(973, 277);
            this.dgvMonthTotals.TabIndex = 2;
            // 
            // clmWeekFoodClass
            // 
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.clmWeekFoodClass.DefaultCellStyle = dataGridViewCellStyle12;
            this.clmWeekFoodClass.HeaderText = "Food Class";
            this.clmWeekFoodClass.Name = "clmWeekFoodClass";
            this.clmWeekFoodClass.ReadOnly = true;
            this.clmWeekFoodClass.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.clmWeekFoodClass.Width = 180;
            // 
            // clmWeek1
            // 
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.clmWeek1.DefaultCellStyle = dataGridViewCellStyle13;
            this.clmWeek1.HeaderText = "Week 1";
            this.clmWeek1.Name = "clmWeek1";
            this.clmWeek1.ReadOnly = true;
            this.clmWeek1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // clmWeek2
            // 
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle14.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.clmWeek2.DefaultCellStyle = dataGridViewCellStyle14;
            this.clmWeek2.HeaderText = "Week 2";
            this.clmWeek2.Name = "clmWeek2";
            this.clmWeek2.ReadOnly = true;
            this.clmWeek2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // clmWeek3
            // 
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle15.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.clmWeek3.DefaultCellStyle = dataGridViewCellStyle15;
            this.clmWeek3.HeaderText = "Week 3";
            this.clmWeek3.Name = "clmWeek3";
            this.clmWeek3.ReadOnly = true;
            this.clmWeek3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // clmWeek4
            // 
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle16.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.clmWeek4.DefaultCellStyle = dataGridViewCellStyle16;
            this.clmWeek4.HeaderText = "Week 4";
            this.clmWeek4.Name = "clmWeek4";
            this.clmWeek4.ReadOnly = true;
            this.clmWeek4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // clmWeek5
            // 
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle17.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.clmWeek5.DefaultCellStyle = dataGridViewCellStyle17;
            this.clmWeek5.HeaderText = "Week 5";
            this.clmWeek5.Name = "clmWeek5";
            this.clmWeek5.ReadOnly = true;
            this.clmWeek5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // clmWeek6
            // 
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle18.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.clmWeek6.DefaultCellStyle = dataGridViewCellStyle18;
            this.clmWeek6.HeaderText = "Week 6";
            this.clmWeek6.Name = "clmWeek6";
            this.clmWeek6.ReadOnly = true;
            this.clmWeek6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // clmWeekTotals
            // 
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle19.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.clmWeekTotals.DefaultCellStyle = dataGridViewCellStyle19;
            this.clmWeekTotals.HeaderText = "Period Totals";
            this.clmWeekTotals.Name = "clmWeekTotals";
            this.clmWeekTotals.ReadOnly = true;
            this.clmWeekTotals.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // pnlMonthly
            // 
            this.pnlMonthly.Controls.Add(this.dgvMonthTotals);
            this.pnlMonthly.Location = new System.Drawing.Point(1041, 228);
            this.pnlMonthly.Name = "pnlMonthly";
            this.pnlMonthly.Size = new System.Drawing.Size(973, 277);
            this.pnlMonthly.TabIndex = 37;
            // 
            // pnlEditDaily
            // 
            this.pnlEditDaily.BackColor = System.Drawing.Color.LightGreen;
            this.pnlEditDaily.Controls.Add(this.grpWeek);
            this.pnlEditDaily.Controls.Add(this.dgvMonthReceipts);
            this.pnlEditDaily.Location = new System.Drawing.Point(1, 124);
            this.pnlEditDaily.Margin = new System.Windows.Forms.Padding(0);
            this.pnlEditDaily.Name = "pnlEditDaily";
            this.pnlEditDaily.Size = new System.Drawing.Size(980, 333);
            this.pnlEditDaily.TabIndex = 38;
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.Location = new System.Drawing.Point(362, 92);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(0);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(110, 32);
            this.btnEdit.TabIndex = 40;
            this.btnEdit.Text = "&Edit Daily Receipts";
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnSummary
            // 
            this.btnSummary.BackColor = System.Drawing.Color.Gainsboro;
            this.btnSummary.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSummary.ForeColor = System.Drawing.Color.Black;
            this.btnSummary.Location = new System.Drawing.Point(472, 92);
            this.btnSummary.Margin = new System.Windows.Forms.Padding(0);
            this.btnSummary.Name = "btnSummary";
            this.btnSummary.Size = new System.Drawing.Size(110, 32);
            this.btnSummary.TabIndex = 41;
            this.btnSummary.Text = "Monthly &Summary";
            this.btnSummary.UseVisualStyleBackColor = false;
            this.btnSummary.Click += new System.EventHandler(this.btnSummary_Click);
            // 
            // cboDonationType
            // 
            this.cboDonationType.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDonationType.FormattingEnabled = true;
            this.cboDonationType.Location = new System.Drawing.Point(345, 30);
            this.cboDonationType.Name = "cboDonationType";
            this.cboDonationType.Size = new System.Drawing.Size(178, 24);
            this.cboDonationType.TabIndex = 44;
            this.cboDonationType.SelectionChangeCommitted += new System.EventHandler(this.cboDonationType_SelectionChangeCommitted);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(345, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 45;
            this.label4.Text = "Donation Type";
            // 
            // btnPrint
            // 
            this.btnPrint.Enabled = false;
            this.btnPrint.Location = new System.Drawing.Point(896, 43);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(82, 32);
            this.btnPrint.TabIndex = 46;
            this.btnPrint.Text = "Create Report";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnEmailRpt
            // 
            this.btnEmailRpt.Enabled = false;
            this.btnEmailRpt.Location = new System.Drawing.Point(896, 79);
            this.btnEmailRpt.Name = "btnEmailRpt";
            this.btnEmailRpt.Size = new System.Drawing.Size(82, 32);
            this.btnEmailRpt.TabIndex = 47;
            this.btnEmailRpt.Text = "Email Report";
            this.btnEmailRpt.UseVisualStyleBackColor = true;
            this.btnEmailRpt.Click += new System.EventHandler(this.btnEmailRpt_Click);
            // 
            // frmFoodReceipts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Cornsilk;
            this.ClientSize = new System.Drawing.Size(985, 454);
            this.Controls.Add(this.btnEmailRpt);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pnlMonthly);
            this.Controls.Add(this.cboDonationType);
            this.Controls.Add(this.btnSummary);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cboStore);
            this.Controls.Add(this.btnLoadPeriodData);
            this.Controls.Add(this.cboReportMonth);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboYear);
            this.Controls.Add(this.pnlEditDaily);
            this.Name = "frmFoodReceipts";
            this.Text = "Grocery Rescue";
            this.grpWeek.ResumeLayout(false);
            this.grpWeek.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMonthReceipts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMonthTotals)).EndInit();
            this.pnlMonthly.ResumeLayout(false);
            this.pnlEditDaily.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboYear;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboReportMonth;
        private System.Windows.Forms.Button btnLoadPeriodData;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboStore;
        private System.Windows.Forms.GroupBox grpWeek;
        private System.Windows.Forms.RadioButton rdoWeek6;
        private System.Windows.Forms.RadioButton rdoWeek5;
        private System.Windows.Forms.RadioButton rdoWeek4;
        private System.Windows.Forms.RadioButton rdoWeek3;
        private System.Windows.Forms.RadioButton rdoWeek2;
        private System.Windows.Forms.RadioButton rdoWeek1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridView dgvMonthReceipts;
        private System.Windows.Forms.DataGridView dgvMonthTotals;
        private System.Windows.Forms.Panel pnlMonthly;
        private System.Windows.Forms.Panel pnlEditDaily;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnSummary;
        private System.Windows.Forms.ComboBox cboDonationType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFoodClass;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColMonday;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTuesday;
        private System.Windows.Forms.DataGridViewTextBoxColumn colWednesday;
        private System.Windows.Forms.DataGridViewTextBoxColumn colThursday;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFriday;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSaturday;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmSunday;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmWeekFoodClass;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmWeek1;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmWeek2;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmWeek3;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmWeek4;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmWeek5;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmWeek6;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmWeekTotals;
        private System.Windows.Forms.Button btnEmailRpt;
    }
}