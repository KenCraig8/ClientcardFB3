namespace ClientcardFB3
{
    partial class FreshAllianceForm
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
                if (clsFoodDonations != null)
                {
                    clsFoodDonations.Dispose();
                }
                if (clsMonthlyReports != null)
                {
                    clsMonthlyReports.Dispose();
                }
                if (dset != null)
                    dset.Dispose();
                if (conn != null)
                    conn.Dispose();
                if (dadAdpt != null)
                    dadAdpt.Dispose();
                if (commandByDay != null)
                    commandByDay.Dispose();
                if (commandByWeek != null)
                    commandByWeek.Dispose();
                if (commBuilder != null)
                    commBuilder.Dispose();
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle28 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle37 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle38 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle39 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle40 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle41 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle42 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle43 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle44 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle45 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle46 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle47 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle48 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle29 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle30 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle31 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle32 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle33 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle34 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle35 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle36 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle49 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle50 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle51 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle52 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle53 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle54 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cboYear = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cboReportMonth = new System.Windows.Forms.ComboBox();
            this.btnLoadPeriodData = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cboStore = new System.Windows.Forms.ComboBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.dgvMonthReceipts = new System.Windows.Forms.DataGridView();
            this.pnlEditDaily = new System.Windows.Forms.Panel();
            this.cboDonationType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnEmailRpt = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
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
            this.clmItem1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmItem2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmItem3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmItem4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmItem5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmItem6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClmItem7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmItem8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClmItem9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmFRC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClmRowTotals = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMonthReceipts)).BeginInit();
            this.pnlEditDaily.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cboYear
            // 
            this.cboYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboYear.FormattingEnabled = true;
            this.cboYear.Location = new System.Drawing.Point(11, 72);
            this.cboYear.Name = "cboYear";
            this.cboYear.Size = new System.Drawing.Size(61, 24);
            this.cboYear.TabIndex = 1;
            this.cboYear.SelectionChangeCommitted += new System.EventHandler(this.cbo_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 38;
            this.label1.Text = "Year";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(75, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 39;
            this.label2.Text = "Month";
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
            this.cboReportMonth.Location = new System.Drawing.Point(78, 72);
            this.cboReportMonth.Name = "cboReportMonth";
            this.cboReportMonth.Size = new System.Drawing.Size(107, 24);
            this.cboReportMonth.TabIndex = 40;
            this.cboReportMonth.SelectionChangeCommitted += new System.EventHandler(this.cbo_SelectionChangeCommitted);
            // 
            // btnLoadPeriodData
            // 
            this.btnLoadPeriodData.Location = new System.Drawing.Point(206, 67);
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
            this.label3.Location = new System.Drawing.Point(8, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 43;
            this.label3.Text = "Store Name";
            // 
            // cboStore
            // 
            this.cboStore.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboStore.FormattingEnabled = true;
            this.cboStore.Location = new System.Drawing.Point(11, 24);
            this.cboStore.Name = "cboStore";
            this.cboStore.Size = new System.Drawing.Size(305, 24);
            this.cboStore.TabIndex = 42;
            this.cboStore.SelectionChangeCommitted += new System.EventHandler(this.cbo_SelectionChangeCommitted);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(706, 20);
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
            dataGridViewCellStyle28.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle28.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            dataGridViewCellStyle28.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle28.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle28.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle28.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle28.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMonthReceipts.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle28;
            this.dgvMonthReceipts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMonthReceipts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmItem1,
            this.clmItem2,
            this.clmItem3,
            this.clmItem4,
            this.clmItem5,
            this.clmItem6,
            this.ClmItem7,
            this.clmItem8,
            this.ClmItem9,
            this.clmFRC,
            this.ClmRowTotals});
            dataGridViewCellStyle37.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle37.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle37.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle37.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle37.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle37.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle37.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMonthReceipts.DefaultCellStyle = dataGridViewCellStyle37;
            this.dgvMonthReceipts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMonthReceipts.GridColor = System.Drawing.Color.Gray;
            this.dgvMonthReceipts.Location = new System.Drawing.Point(0, 0);
            this.dgvMonthReceipts.MultiSelect = false;
            this.dgvMonthReceipts.Name = "dgvMonthReceipts";
            this.dgvMonthReceipts.RowHeadersWidth = 100;
            this.dgvMonthReceipts.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvMonthReceipts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvMonthReceipts.Size = new System.Drawing.Size(874, 371);
            this.dgvMonthReceipts.TabIndex = 1;
            this.dgvMonthReceipts.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMonthReceipts_CellEndEdit);
            this.dgvMonthReceipts.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMonthReceipts_CellEnter);
            this.dgvMonthReceipts.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvMonthReceipts_CellValidating);
            // 
            // pnlEditDaily
            // 
            this.pnlEditDaily.BackColor = System.Drawing.Color.LightGreen;
            this.pnlEditDaily.Controls.Add(this.dgvMonthReceipts);
            this.pnlEditDaily.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlEditDaily.Location = new System.Drawing.Point(0, 0);
            this.pnlEditDaily.Margin = new System.Windows.Forms.Padding(0);
            this.pnlEditDaily.Name = "pnlEditDaily";
            this.pnlEditDaily.Size = new System.Drawing.Size(874, 371);
            this.pnlEditDaily.TabIndex = 38;
            this.pnlEditDaily.Visible = false;
            // 
            // cboDonationType
            // 
            this.cboDonationType.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDonationType.FormattingEnabled = true;
            this.cboDonationType.Location = new System.Drawing.Point(344, 24);
            this.cboDonationType.Name = "cboDonationType";
            this.cboDonationType.Size = new System.Drawing.Size(178, 24);
            this.cboDonationType.TabIndex = 44;
            this.cboDonationType.SelectionChangeCommitted += new System.EventHandler(this.cboDonationType_SelectionChangeCommitted);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(344, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 45;
            this.label4.Text = "Donation Type";
            // 
            // btnPrint
            // 
            this.btnPrint.Enabled = false;
            this.btnPrint.Location = new System.Drawing.Point(706, 55);
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
            this.btnEmailRpt.Location = new System.Drawing.Point(792, 56);
            this.btnEmailRpt.Name = "btnEmailRpt";
            this.btnEmailRpt.Size = new System.Drawing.Size(82, 32);
            this.btnEmailRpt.TabIndex = 47;
            this.btnEmailRpt.Text = "Email Report";
            this.btnEmailRpt.UseVisualStyleBackColor = true;
            this.btnEmailRpt.Visible = false;
            this.btnEmailRpt.Click += new System.EventHandler(this.btnEmailRpt_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnClose);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.btnEmailRpt);
            this.splitContainer1.Panel1.Controls.Add(this.cboDonationType);
            this.splitContainer1.Panel1.Controls.Add(this.btnPrint);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.cboStore);
            this.splitContainer1.Panel1.Controls.Add(this.cboYear);
            this.splitContainer1.Panel1.Controls.Add(this.btnLoadPeriodData);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.cboReportMonth);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pnlEditDaily);
            this.splitContainer1.Size = new System.Drawing.Size(874, 485);
            this.splitContainer1.SplitterDistance = 110;
            this.splitContainer1.TabIndex = 48;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewCellStyle38.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle38.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle38;
            this.dataGridViewTextBoxColumn1.HeaderText = "Food Class";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn1.Width = 180;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewCellStyle39.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle39;
            this.dataGridViewTextBoxColumn2.HeaderText = "Sunday";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn2.Width = 95;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewCellStyle40.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle40;
            this.dataGridViewTextBoxColumn3.HeaderText = "Monday";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn3.Width = 95;
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewCellStyle41.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle41;
            this.dataGridViewTextBoxColumn4.HeaderText = "Tuesday";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn4.Width = 95;
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewCellStyle42.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle42;
            this.dataGridViewTextBoxColumn5.HeaderText = "Wednesday";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn5.Width = 95;
            // 
            // dataGridViewTextBoxColumn6
            // 
            dataGridViewCellStyle43.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn6.DefaultCellStyle = dataGridViewCellStyle43;
            this.dataGridViewTextBoxColumn6.HeaderText = "Thursday";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn6.Width = 95;
            // 
            // dataGridViewTextBoxColumn7
            // 
            dataGridViewCellStyle44.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn7.DefaultCellStyle = dataGridViewCellStyle44;
            this.dataGridViewTextBoxColumn7.HeaderText = "Friday";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn7.Width = 95;
            // 
            // dataGridViewTextBoxColumn8
            // 
            dataGridViewCellStyle45.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn8.DefaultCellStyle = dataGridViewCellStyle45;
            this.dataGridViewTextBoxColumn8.HeaderText = "Saturday";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn8.Width = 95;
            // 
            // dataGridViewTextBoxColumn9
            // 
            dataGridViewCellStyle46.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle46.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            dataGridViewCellStyle46.Format = "N0";
            dataGridViewCellStyle46.NullValue = null;
            this.dataGridViewTextBoxColumn9.DefaultCellStyle = dataGridViewCellStyle46;
            this.dataGridViewTextBoxColumn9.HeaderText = "Totals";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            this.dataGridViewTextBoxColumn9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn9.Width = 95;
            // 
            // dataGridViewTextBoxColumn10
            // 
            dataGridViewCellStyle47.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle47.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.dataGridViewTextBoxColumn10.DefaultCellStyle = dataGridViewCellStyle47;
            this.dataGridViewTextBoxColumn10.HeaderText = "Food Class";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            this.dataGridViewTextBoxColumn10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn10.Width = 180;
            // 
            // dataGridViewTextBoxColumn11
            // 
            dataGridViewCellStyle48.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle48.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.dataGridViewTextBoxColumn11.DefaultCellStyle = dataGridViewCellStyle48;
            this.dataGridViewTextBoxColumn11.HeaderText = "Week 1";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.ReadOnly = true;
            this.dataGridViewTextBoxColumn11.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // clmItem1
            // 
            dataGridViewCellStyle29.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle29.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.clmItem1.DefaultCellStyle = dataGridViewCellStyle29;
            this.clmItem1.HeaderText = "Bread";
            this.clmItem1.Name = "clmItem1";
            this.clmItem1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.clmItem1.Width = 95;
            // 
            // clmItem2
            // 
            dataGridViewCellStyle30.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle30.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.clmItem2.DefaultCellStyle = dataGridViewCellStyle30;
            this.clmItem2.HeaderText = "Dry";
            this.clmItem2.Name = "clmItem2";
            this.clmItem2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.clmItem2.Width = 95;
            // 
            // clmItem3
            // 
            dataGridViewCellStyle31.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle31.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.clmItem3.DefaultCellStyle = dataGridViewCellStyle31;
            this.clmItem3.HeaderText = "Produce";
            this.clmItem3.Name = "clmItem3";
            this.clmItem3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.clmItem3.Width = 95;
            // 
            // clmItem4
            // 
            dataGridViewCellStyle32.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle32.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.clmItem4.DefaultCellStyle = dataGridViewCellStyle32;
            this.clmItem4.HeaderText = "Cooler";
            this.clmItem4.Name = "clmItem4";
            this.clmItem4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.clmItem4.Width = 95;
            // 
            // clmItem5
            // 
            dataGridViewCellStyle33.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle33.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.clmItem5.DefaultCellStyle = dataGridViewCellStyle33;
            this.clmItem5.HeaderText = "Dairy";
            this.clmItem5.Name = "clmItem5";
            this.clmItem5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.clmItem5.Width = 95;
            // 
            // clmItem6
            // 
            dataGridViewCellStyle34.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle34.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.clmItem6.DefaultCellStyle = dataGridViewCellStyle34;
            this.clmItem6.HeaderText = "Milk";
            this.clmItem6.Name = "clmItem6";
            this.clmItem6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.clmItem6.Width = 95;
            // 
            // ClmItem7
            // 
            dataGridViewCellStyle35.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle35.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.ClmItem7.DefaultCellStyle = dataGridViewCellStyle35;
            this.ClmItem7.HeaderText = "Frozen";
            this.ClmItem7.Name = "ClmItem7";
            this.ClmItem7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ClmItem7.Width = 95;
            // 
            // clmItem8
            // 
            dataGridViewCellStyle36.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle36.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            dataGridViewCellStyle36.Format = "N0";
            dataGridViewCellStyle36.NullValue = null;
            dataGridViewCellStyle36.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.clmItem8.DefaultCellStyle = dataGridViewCellStyle36;
            this.clmItem8.HeaderText = "Meat";
            this.clmItem8.Name = "clmItem8";
            this.clmItem8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.clmItem8.Width = 95;
            // 
            // ClmItem9
            // 
            this.ClmItem9.HeaderText = "Non";
            this.ClmItem9.Name = "ClmItem9";
            // 
            // clmFRC
            // 
            this.clmFRC.HeaderText = "FRC";
            this.clmFRC.Name = "clmFRC";
            // 
            // ClmRowTotals
            // 
            this.ClmRowTotals.HeaderText = "Totals";
            this.ClmRowTotals.Name = "ClmRowTotals";
            this.ClmRowTotals.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn12
            // 
            dataGridViewCellStyle49.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle49.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.dataGridViewTextBoxColumn12.DefaultCellStyle = dataGridViewCellStyle49;
            this.dataGridViewTextBoxColumn12.HeaderText = "Week 2";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.ReadOnly = true;
            this.dataGridViewTextBoxColumn12.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn13
            // 
            dataGridViewCellStyle50.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle50.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.dataGridViewTextBoxColumn13.DefaultCellStyle = dataGridViewCellStyle50;
            this.dataGridViewTextBoxColumn13.HeaderText = "Week 3";
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            this.dataGridViewTextBoxColumn13.ReadOnly = true;
            this.dataGridViewTextBoxColumn13.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn14
            // 
            dataGridViewCellStyle51.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle51.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.dataGridViewTextBoxColumn14.DefaultCellStyle = dataGridViewCellStyle51;
            this.dataGridViewTextBoxColumn14.HeaderText = "Week 4";
            this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
            this.dataGridViewTextBoxColumn14.ReadOnly = true;
            this.dataGridViewTextBoxColumn14.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn15
            // 
            dataGridViewCellStyle52.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle52.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.dataGridViewTextBoxColumn15.DefaultCellStyle = dataGridViewCellStyle52;
            this.dataGridViewTextBoxColumn15.HeaderText = "Week 5";
            this.dataGridViewTextBoxColumn15.Name = "dataGridViewTextBoxColumn15";
            this.dataGridViewTextBoxColumn15.ReadOnly = true;
            this.dataGridViewTextBoxColumn15.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn16
            // 
            dataGridViewCellStyle53.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle53.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.dataGridViewTextBoxColumn16.DefaultCellStyle = dataGridViewCellStyle53;
            this.dataGridViewTextBoxColumn16.HeaderText = "Week 6";
            this.dataGridViewTextBoxColumn16.Name = "dataGridViewTextBoxColumn16";
            this.dataGridViewTextBoxColumn16.ReadOnly = true;
            this.dataGridViewTextBoxColumn16.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn17
            // 
            dataGridViewCellStyle54.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle54.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.dataGridViewTextBoxColumn17.DefaultCellStyle = dataGridViewCellStyle54;
            this.dataGridViewTextBoxColumn17.HeaderText = "Period Totals";
            this.dataGridViewTextBoxColumn17.Name = "dataGridViewTextBoxColumn17";
            this.dataGridViewTextBoxColumn17.ReadOnly = true;
            this.dataGridViewTextBoxColumn17.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // FreshAllianceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Cornsilk;
            this.ClientSize = new System.Drawing.Size(874, 485);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FreshAllianceForm";
            this.Text = "Fresh Alliance";
            ((System.ComponentModel.ISupportInitialize)(this.dgvMonthReceipts)).EndInit();
            this.pnlEditDaily.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cboYear;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboReportMonth;
        private System.Windows.Forms.Button btnLoadPeriodData;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboStore;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridView dgvMonthReceipts;
        private System.Windows.Forms.Panel pnlEditDaily;
        private System.Windows.Forms.ComboBox cboDonationType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnEmailRpt;
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
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn16;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn17;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmItem1;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmItem2;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmItem3;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmItem4;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmItem5;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmItem6;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClmItem7;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmItem8;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClmItem9;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmFRC;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClmRowTotals;
    }
}