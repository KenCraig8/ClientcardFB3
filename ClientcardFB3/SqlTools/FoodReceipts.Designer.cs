namespace ClientCardFB3
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle69 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle70 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle71 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle72 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle73 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle74 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle75 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle76 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle77 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle78 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle79 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle80 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle81 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle82 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle83 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle84 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle85 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpByDay = new System.Windows.Forms.TabPage();
            this.dgvMonthReceipts = new System.Windows.Forms.DataGridView();
            this.tpByWeek = new System.Windows.Forms.TabPage();
            this.dgvMonthTotals = new System.Windows.Forms.DataGridView();
            this.clmWeekFoodClass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmWeek1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmWeek2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmWeek3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmWeek4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmWeek5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmWeek6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmWeekTotals = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFoodClass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColMonday = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTuesday = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colWednesday = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colThursday = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFriday = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSaturday = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmSunday = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpWeek.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpByDay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMonthReceipts)).BeginInit();
            this.tpByWeek.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMonthTotals)).BeginInit();
            this.SuspendLayout();
            // 
            // cboYear
            // 
            this.cboYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboYear.FormattingEnabled = true;
            this.cboYear.Location = new System.Drawing.Point(12, 70);
            this.cboYear.Name = "cboYear";
            this.cboYear.Size = new System.Drawing.Size(61, 24);
            this.cboYear.TabIndex = 1;
            this.cboYear.SelectionChangeCommitted += new System.EventHandler(this.cbo_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Year";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(76, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Month";
            // 
            // cboReportMonth
            // 
            this.cboReportMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboReportMonth.FormattingEnabled = true;
            this.cboReportMonth.Items.AddRange(new object[] {
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
            this.cboReportMonth.Location = new System.Drawing.Point(79, 70);
            this.cboReportMonth.Name = "cboReportMonth";
            this.cboReportMonth.Size = new System.Drawing.Size(107, 24);
            this.cboReportMonth.TabIndex = 28;
            this.cboReportMonth.SelectionChangeCommitted += new System.EventHandler(this.cbo_SelectionChangeCommitted);
            // 
            // btnLoadPeriodData
            // 
            this.btnLoadPeriodData.Location = new System.Drawing.Point(207, 65);
            this.btnLoadPeriodData.Name = "btnLoadPeriodData";
            this.btnLoadPeriodData.Size = new System.Drawing.Size(110, 32);
            this.btnLoadPeriodData.TabIndex = 29;
            this.btnLoadPeriodData.Text = "Load Period Data";
            this.btnLoadPeriodData.UseVisualStyleBackColor = true;
            this.btnLoadPeriodData.Click += new System.EventHandler(this.btnLoadPeriodData_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 31;
            this.label3.Text = "Store Name";
            // 
            // cboStore
            // 
            this.cboStore.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboStore.FormattingEnabled = true;
            this.cboStore.Location = new System.Drawing.Point(12, 22);
            this.cboStore.Name = "cboStore";
            this.cboStore.Size = new System.Drawing.Size(305, 24);
            this.cboStore.TabIndex = 30;
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
            this.grpWeek.Location = new System.Drawing.Point(342, 1);
            this.grpWeek.Name = "grpWeek";
            this.grpWeek.Size = new System.Drawing.Size(93, 151);
            this.grpWeek.TabIndex = 32;
            this.grpWeek.TabStop = false;
            // 
            // rdoWeek6
            // 
            this.rdoWeek6.AutoSize = true;
            this.rdoWeek6.Location = new System.Drawing.Point(16, 128);
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
            this.rdoWeek5.Location = new System.Drawing.Point(16, 108);
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
            this.rdoWeek4.Location = new System.Drawing.Point(16, 85);
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
            this.rdoWeek3.Location = new System.Drawing.Point(16, 62);
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
            this.rdoWeek2.Location = new System.Drawing.Point(16, 39);
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
            this.rdoWeek1.Location = new System.Drawing.Point(16, 16);
            this.rdoWeek1.Name = "rdoWeek1";
            this.rdoWeek1.Size = new System.Drawing.Size(63, 17);
            this.rdoWeek1.TabIndex = 0;
            this.rdoWeek1.TabStop = true;
            this.rdoWeek1.Tag = "0";
            this.rdoWeek1.Text = "Week 1";
            this.rdoWeek1.UseVisualStyleBackColor = true;
            this.rdoWeek1.CheckedChanged += new System.EventHandler(this.rdoWeek_CheckedChanged);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(896, 8);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(96, 48);
            this.btnClose.TabIndex = 34;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpByDay);
            this.tabControl1.Controls.Add(this.tpByWeek);
            this.tabControl1.ItemSize = new System.Drawing.Size(46, 30);
            this.tabControl1.Location = new System.Drawing.Point(0, 152);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1008, 320);
            this.tabControl1.TabIndex = 36;
            this.tabControl1.Visible = false;
            // 
            // tpByDay
            // 
            this.tpByDay.Controls.Add(this.dgvMonthReceipts);
            this.tpByDay.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tpByDay.Location = new System.Drawing.Point(4, 34);
            this.tpByDay.Name = "tpByDay";
            this.tpByDay.Padding = new System.Windows.Forms.Padding(3);
            this.tpByDay.Size = new System.Drawing.Size(1000, 282);
            this.tpByDay.TabIndex = 0;
            this.tpByDay.Text = "By Day";
            this.tpByDay.UseVisualStyleBackColor = true;
            // 
            // dgvMonthReceipts
            // 
            this.dgvMonthReceipts.AllowUserToAddRows = false;
            this.dgvMonthReceipts.AllowUserToDeleteRows = false;
            this.dgvMonthReceipts.BackgroundColor = System.Drawing.Color.WhiteSmoke;
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
            this.dgvMonthReceipts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMonthReceipts.GridColor = System.Drawing.Color.Gray;
            this.dgvMonthReceipts.Location = new System.Drawing.Point(3, 3);
            this.dgvMonthReceipts.Name = "dgvMonthReceipts";
            this.dgvMonthReceipts.RowHeadersVisible = false;
            this.dgvMonthReceipts.RowHeadersWidth = 5;
            this.dgvMonthReceipts.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvMonthReceipts.Size = new System.Drawing.Size(994, 276);
            this.dgvMonthReceipts.TabIndex = 1;
            this.dgvMonthReceipts.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMonthReceipts_CellEndEdit);
            this.dgvMonthReceipts.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvMonthReceipts_CellValidating);
            // 
            // tpByWeek
            // 
            this.tpByWeek.Controls.Add(this.dgvMonthTotals);
            this.tpByWeek.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tpByWeek.Location = new System.Drawing.Point(4, 34);
            this.tpByWeek.Name = "tpByWeek";
            this.tpByWeek.Padding = new System.Windows.Forms.Padding(3);
            this.tpByWeek.Size = new System.Drawing.Size(1000, 282);
            this.tpByWeek.TabIndex = 1;
            this.tpByWeek.Text = "By Week";
            this.tpByWeek.UseVisualStyleBackColor = true;
            // 
            // dgvMonthTotals
            // 
            this.dgvMonthTotals.AllowUserToAddRows = false;
            this.dgvMonthTotals.AllowUserToDeleteRows = false;
            this.dgvMonthTotals.BackgroundColor = System.Drawing.Color.WhiteSmoke;
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
            this.dgvMonthTotals.Location = new System.Drawing.Point(3, 3);
            this.dgvMonthTotals.Name = "dgvMonthTotals";
            this.dgvMonthTotals.RowHeadersVisible = false;
            this.dgvMonthTotals.RowHeadersWidth = 5;
            this.dgvMonthTotals.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvMonthTotals.Size = new System.Drawing.Size(994, 276);
            this.dgvMonthTotals.TabIndex = 2;
            // 
            // clmWeekFoodClass
            // 
            dataGridViewCellStyle69.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle69.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.clmWeekFoodClass.DefaultCellStyle = dataGridViewCellStyle69;
            this.clmWeekFoodClass.HeaderText = "Food Class";
            this.clmWeekFoodClass.Name = "clmWeekFoodClass";
            this.clmWeekFoodClass.ReadOnly = true;
            this.clmWeekFoodClass.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.clmWeekFoodClass.Width = 180;
            // 
            // clmWeek1
            // 
            dataGridViewCellStyle70.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle70.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.clmWeek1.DefaultCellStyle = dataGridViewCellStyle70;
            this.clmWeek1.HeaderText = "Week 1";
            this.clmWeek1.Name = "clmWeek1";
            this.clmWeek1.ReadOnly = true;
            this.clmWeek1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // clmWeek2
            // 
            dataGridViewCellStyle71.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle71.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.clmWeek2.DefaultCellStyle = dataGridViewCellStyle71;
            this.clmWeek2.HeaderText = "Week 2";
            this.clmWeek2.Name = "clmWeek2";
            this.clmWeek2.ReadOnly = true;
            this.clmWeek2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // clmWeek3
            // 
            dataGridViewCellStyle72.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle72.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.clmWeek3.DefaultCellStyle = dataGridViewCellStyle72;
            this.clmWeek3.HeaderText = "Week 3";
            this.clmWeek3.Name = "clmWeek3";
            this.clmWeek3.ReadOnly = true;
            this.clmWeek3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // clmWeek4
            // 
            dataGridViewCellStyle73.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle73.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.clmWeek4.DefaultCellStyle = dataGridViewCellStyle73;
            this.clmWeek4.HeaderText = "Week 4";
            this.clmWeek4.Name = "clmWeek4";
            this.clmWeek4.ReadOnly = true;
            this.clmWeek4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // clmWeek5
            // 
            dataGridViewCellStyle74.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle74.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.clmWeek5.DefaultCellStyle = dataGridViewCellStyle74;
            this.clmWeek5.HeaderText = "Week 5";
            this.clmWeek5.Name = "clmWeek5";
            this.clmWeek5.ReadOnly = true;
            this.clmWeek5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // clmWeek6
            // 
            dataGridViewCellStyle75.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle75.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.clmWeek6.DefaultCellStyle = dataGridViewCellStyle75;
            this.clmWeek6.HeaderText = "Week 6";
            this.clmWeek6.Name = "clmWeek6";
            this.clmWeek6.ReadOnly = true;
            this.clmWeek6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // clmWeekTotals
            // 
            dataGridViewCellStyle76.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle76.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.clmWeekTotals.DefaultCellStyle = dataGridViewCellStyle76;
            this.clmWeekTotals.HeaderText = "Period Totals";
            this.clmWeekTotals.Name = "clmWeekTotals";
            this.clmWeekTotals.ReadOnly = true;
            this.clmWeekTotals.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colFoodClass
            // 
            dataGridViewCellStyle77.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle77.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.colFoodClass.DefaultCellStyle = dataGridViewCellStyle77;
            this.colFoodClass.HeaderText = "Food Class";
            this.colFoodClass.Name = "colFoodClass";
            this.colFoodClass.ReadOnly = true;
            this.colFoodClass.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colFoodClass.Width = 180;
            // 
            // ColMonday
            // 
            dataGridViewCellStyle78.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ColMonday.DefaultCellStyle = dataGridViewCellStyle78;
            this.ColMonday.HeaderText = "Monday";
            this.ColMonday.Name = "ColMonday";
            this.ColMonday.ReadOnly = true;
            this.ColMonday.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColMonday.Width = 95;
            // 
            // colTuesday
            // 
            dataGridViewCellStyle79.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colTuesday.DefaultCellStyle = dataGridViewCellStyle79;
            this.colTuesday.HeaderText = "Tuesday";
            this.colTuesday.Name = "colTuesday";
            this.colTuesday.ReadOnly = true;
            this.colTuesday.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colTuesday.Width = 95;
            // 
            // colWednesday
            // 
            dataGridViewCellStyle80.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colWednesday.DefaultCellStyle = dataGridViewCellStyle80;
            this.colWednesday.HeaderText = "Wednesday";
            this.colWednesday.Name = "colWednesday";
            this.colWednesday.ReadOnly = true;
            this.colWednesday.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colWednesday.Width = 95;
            // 
            // colThursday
            // 
            dataGridViewCellStyle81.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colThursday.DefaultCellStyle = dataGridViewCellStyle81;
            this.colThursday.HeaderText = "Thursday";
            this.colThursday.Name = "colThursday";
            this.colThursday.ReadOnly = true;
            this.colThursday.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colThursday.Width = 95;
            // 
            // colFriday
            // 
            dataGridViewCellStyle82.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colFriday.DefaultCellStyle = dataGridViewCellStyle82;
            this.colFriday.HeaderText = "Friday";
            this.colFriday.Name = "colFriday";
            this.colFriday.ReadOnly = true;
            this.colFriday.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colFriday.Width = 95;
            // 
            // colSaturday
            // 
            dataGridViewCellStyle83.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colSaturday.DefaultCellStyle = dataGridViewCellStyle83;
            this.colSaturday.HeaderText = "Saturday";
            this.colSaturday.Name = "colSaturday";
            this.colSaturday.ReadOnly = true;
            this.colSaturday.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colSaturday.Width = 95;
            // 
            // clmSunday
            // 
            dataGridViewCellStyle84.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.clmSunday.DefaultCellStyle = dataGridViewCellStyle84;
            this.clmSunday.HeaderText = "Sunday";
            this.clmSunday.Name = "clmSunday";
            this.clmSunday.ReadOnly = true;
            this.clmSunday.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.clmSunday.Width = 95;
            // 
            // colTotal
            // 
            dataGridViewCellStyle85.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle85.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.colTotal.DefaultCellStyle = dataGridViewCellStyle85;
            this.colTotal.HeaderText = "Totals";
            this.colTotal.Name = "colTotal";
            this.colTotal.ReadOnly = true;
            this.colTotal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colTotal.Width = 95;
            // 
            // frmFoodReceipts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Cornsilk;
            this.ClientSize = new System.Drawing.Size(1008, 477);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.grpWeek);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cboStore);
            this.Controls.Add(this.btnLoadPeriodData);
            this.Controls.Add(this.cboReportMonth);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboYear);
            this.Name = "frmFoodReceipts";
            this.Text = "Grocery Rescue";
            this.grpWeek.ResumeLayout(false);
            this.grpWeek.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tpByDay.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMonthReceipts)).EndInit();
            this.tpByWeek.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMonthTotals)).EndInit();
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
        private System.Windows.Forms.RadioButton rdoWeek5;
        private System.Windows.Forms.RadioButton rdoWeek4;
        private System.Windows.Forms.RadioButton rdoWeek3;
        private System.Windows.Forms.RadioButton rdoWeek2;
        private System.Windows.Forms.RadioButton rdoWeek1;
        private System.Windows.Forms.RadioButton rdoWeek6;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpByDay;
        private System.Windows.Forms.DataGridView dgvMonthReceipts;
        private System.Windows.Forms.TabPage tpByWeek;
        private System.Windows.Forms.DataGridView dgvMonthTotals;
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
    }
}