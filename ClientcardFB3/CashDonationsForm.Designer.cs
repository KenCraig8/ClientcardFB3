namespace ClientcardFB3
{
    partial class CashDonationsForm
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
                if (clsCashDonations != null)
                {
                    clsCashDonations.Dispose();
                }
                if (clsDonors != null)
                {
                    clsDonors.Dispose();
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
            this.btnDeleteTrx = new System.Windows.Forms.Button();
            this.btnLoadPeriodData = new System.Windows.Forms.Button();
            this.cboReportMonth = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cboYear = new System.Windows.Forms.ComboBox();
            this.lvFoodDonations = new System.Windows.Forms.ListView();
            this.colCntLog = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDonorLog = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDollars = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colNotesLog = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDonorIdLog = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTrxIdLog = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnNewDonation = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnShowDnrHist = new System.Windows.Forms.Button();
            this.btnEditDnrTrx = new System.Windows.Forms.Button();
            this.tbTotalDollars = new System.Windows.Forms.TextBox();
            this.tbTotalCount = new System.Windows.Forms.TextBox();
            this.lvDonorHistory = new System.Windows.Forms.ListView();
            this.colHisCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colHisDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colHisDollars = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colHisNotes = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colHisTrxID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cboDonorPeriod = new System.Windows.Forms.ComboBox();
            this.btnLoadCustom = new System.Windows.Forms.Button();
            this.lblTo = new System.Windows.Forms.Label();
            this.lblFrom = new System.Windows.Forms.Label();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.btnHideDonorHist = new System.Windows.Forms.Button();
            this.tbTotalDonorDollars = new System.Windows.Forms.TextBox();
            this.tbDonorCnt = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbDollars = new System.Windows.Forms.TextBox();
            this.tbName = new System.Windows.Forms.TextBox();
            this.tbDonorID = new System.Windows.Forms.TextBox();
            this.tbTrxID = new System.Windows.Forms.TextBox();
            this.dtDonationDate = new System.Windows.Forms.DateTimePicker();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnLogEntrySave = new System.Windows.Forms.Button();
            this.btnLogEntryCancel = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.tbNotes = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblID = new System.Windows.Forms.Label();
            this.lblTrxID = new System.Windows.Forms.Label();
            this.spltcontLog = new System.Windows.Forms.SplitContainer();
            this.pnlPeriod = new System.Windows.Forms.Panel();
            this.cboDisplayType = new System.Windows.Forms.ComboBox();
            this.pnlEntryDate = new System.Windows.Forms.Panel();
            this.dtDateDL = new System.Windows.Forms.DateTimePicker();
            this.btnPrevDL = new System.Windows.Forms.Button();
            this.btnFirstDL = new System.Windows.Forms.Button();
            this.btnLastDL = new System.Windows.Forms.Button();
            this.btnNextDL = new System.Windows.Forms.Button();
            this.spltcontEdit = new System.Windows.Forms.SplitContainer();
            this.spltcontHistory = new System.Windows.Forms.SplitContainer();
            this.lblText = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblDonorHist = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.spltcontLog)).BeginInit();
            this.spltcontLog.Panel1.SuspendLayout();
            this.spltcontLog.Panel2.SuspendLayout();
            this.spltcontLog.SuspendLayout();
            this.pnlPeriod.SuspendLayout();
            this.pnlEntryDate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spltcontEdit)).BeginInit();
            this.spltcontEdit.Panel1.SuspendLayout();
            this.spltcontEdit.Panel2.SuspendLayout();
            this.spltcontEdit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spltcontHistory)).BeginInit();
            this.spltcontHistory.Panel1.SuspendLayout();
            this.spltcontHistory.Panel2.SuspendLayout();
            this.spltcontHistory.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnDeleteTrx
            // 
            this.btnDeleteTrx.Enabled = false;
            this.btnDeleteTrx.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteTrx.ForeColor = System.Drawing.Color.DarkRed;
            this.btnDeleteTrx.Location = new System.Drawing.Point(555, 110);
            this.btnDeleteTrx.Name = "btnDeleteTrx";
            this.btnDeleteTrx.Size = new System.Drawing.Size(201, 38);
            this.btnDeleteTrx.TabIndex = 47;
            this.btnDeleteTrx.Text = "Delete";
            this.btnDeleteTrx.UseVisualStyleBackColor = true;
            this.btnDeleteTrx.Click += new System.EventHandler(this.btnDeleteTrx_Click);
            // 
            // btnLoadPeriodData
            // 
            this.btnLoadPeriodData.Location = new System.Drawing.Point(210, 20);
            this.btnLoadPeriodData.Name = "btnLoadPeriodData";
            this.btnLoadPeriodData.Size = new System.Drawing.Size(110, 28);
            this.btnLoadPeriodData.TabIndex = 46;
            this.btnLoadPeriodData.Text = "Load Period Data";
            this.btnLoadPeriodData.UseVisualStyleBackColor = true;
            this.btnLoadPeriodData.Click += new System.EventHandler(this.btnLoadPeriodData_Click);
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
            this.cboReportMonth.Location = new System.Drawing.Point(82, 23);
            this.cboReportMonth.Name = "cboReportMonth";
            this.cboReportMonth.Size = new System.Drawing.Size(107, 24);
            this.cboReportMonth.TabIndex = 45;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(79, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 17);
            this.label1.TabIndex = 44;
            this.label1.Text = "Month";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 17);
            this.label3.TabIndex = 43;
            this.label3.Text = "Year";
            // 
            // cboYear
            // 
            this.cboYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboYear.FormattingEnabled = true;
            this.cboYear.Location = new System.Drawing.Point(14, 23);
            this.cboYear.Name = "cboYear";
            this.cboYear.Size = new System.Drawing.Size(61, 24);
            this.cboYear.TabIndex = 42;
            // 
            // lvFoodDonations
            // 
            this.lvFoodDonations.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colCntLog,
            this.colDate,
            this.colDonorLog,
            this.colDollars,
            this.colNotesLog,
            this.colDonorIdLog,
            this.colTrxIdLog});
            this.lvFoodDonations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvFoodDonations.FullRowSelect = true;
            this.lvFoodDonations.GridLines = true;
            this.lvFoodDonations.Location = new System.Drawing.Point(0, 0);
            this.lvFoodDonations.Name = "lvFoodDonations";
            this.lvFoodDonations.Size = new System.Drawing.Size(1110, 532);
            this.lvFoodDonations.TabIndex = 11;
            this.lvFoodDonations.UseCompatibleStateImageBehavior = false;
            this.lvFoodDonations.View = System.Windows.Forms.View.Details;
            this.lvFoodDonations.SelectedIndexChanged += new System.EventHandler(this.lvFoodDonations_SelectedIndexChanged);
            // 
            // colCntLog
            // 
            this.colCntLog.Text = "Cnt";
            this.colCntLog.Width = 38;
            // 
            // colDate
            // 
            this.colDate.Text = "Donate Date";
            this.colDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colDate.Width = 90;
            // 
            // colDonorLog
            // 
            this.colDonorLog.Text = "Donor Name";
            this.colDonorLog.Width = 300;
            // 
            // colDollars
            // 
            this.colDollars.Text = "Dollars";
            this.colDollars.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colDollars.Width = 85;
            // 
            // colNotesLog
            // 
            this.colNotesLog.Text = "Notes";
            this.colNotesLog.Width = 500;
            // 
            // colDonorIdLog
            // 
            this.colDonorIdLog.Text = "Dnr Id";
            this.colDonorIdLog.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colDonorIdLog.Width = 0;
            // 
            // colTrxIdLog
            // 
            this.colTrxIdLog.Text = "TrxId";
            this.colTrxIdLog.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colTrxIdLog.Width = 0;
            // 
            // btnNewDonation
            // 
            this.btnNewDonation.Enabled = false;
            this.btnNewDonation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewDonation.Location = new System.Drawing.Point(98, 105);
            this.btnNewDonation.Name = "btnNewDonation";
            this.btnNewDonation.Size = new System.Drawing.Size(160, 35);
            this.btnNewDonation.TabIndex = 23;
            this.btnNewDonation.Text = "&Add New Donation";
            this.btnNewDonation.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNewDonation.UseVisualStyleBackColor = true;
            this.btnNewDonation.Click += new System.EventHandler(this.btnNewDonation_Click);
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(1060, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(47, 22);
            this.btnClose.TabIndex = 22;
            this.btnClose.Text = "&Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnShowDnrHist
            // 
            this.btnShowDnrHist.Enabled = false;
            this.btnShowDnrHist.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShowDnrHist.ForeColor = System.Drawing.Color.DarkRed;
            this.btnShowDnrHist.Location = new System.Drawing.Point(554, 31);
            this.btnShowDnrHist.Name = "btnShowDnrHist";
            this.btnShowDnrHist.Size = new System.Drawing.Size(201, 72);
            this.btnShowDnrHist.TabIndex = 14;
            this.btnShowDnrHist.UseVisualStyleBackColor = true;
            this.btnShowDnrHist.Click += new System.EventHandler(this.btnShowDnrHist_Click);
            // 
            // btnEditDnrTrx
            // 
            this.btnEditDnrTrx.Enabled = false;
            this.btnEditDnrTrx.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditDnrTrx.ForeColor = System.Drawing.Color.DarkRed;
            this.btnEditDnrTrx.Location = new System.Drawing.Point(347, 31);
            this.btnEditDnrTrx.Name = "btnEditDnrTrx";
            this.btnEditDnrTrx.Size = new System.Drawing.Size(201, 72);
            this.btnEditDnrTrx.TabIndex = 13;
            this.btnEditDnrTrx.UseVisualStyleBackColor = true;
            this.btnEditDnrTrx.Click += new System.EventHandler(this.btnEditDnrTrx_Click);
            // 
            // tbTotalDollars
            // 
            this.tbTotalDollars.Location = new System.Drawing.Point(429, 147);
            this.tbTotalDollars.Name = "tbTotalDollars";
            this.tbTotalDollars.Size = new System.Drawing.Size(84, 23);
            this.tbTotalDollars.TabIndex = 12;
            this.tbTotalDollars.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbTotalCount
            // 
            this.tbTotalCount.Location = new System.Drawing.Point(1, 149);
            this.tbTotalCount.Name = "tbTotalCount";
            this.tbTotalCount.Size = new System.Drawing.Size(39, 23);
            this.tbTotalCount.TabIndex = 10;
            this.tbTotalCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lvDonorHistory
            // 
            this.lvDonorHistory.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colHisCount,
            this.colHisDate,
            this.colHisDollars,
            this.colHisNotes,
            this.colHisTrxID});
            this.lvDonorHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvDonorHistory.FullRowSelect = true;
            this.lvDonorHistory.GridLines = true;
            this.lvDonorHistory.HideSelection = false;
            this.lvDonorHistory.Location = new System.Drawing.Point(0, 0);
            this.lvDonorHistory.Name = "lvDonorHistory";
            this.lvDonorHistory.Size = new System.Drawing.Size(1110, 368);
            this.lvDonorHistory.TabIndex = 26;
            this.lvDonorHistory.UseCompatibleStateImageBehavior = false;
            this.lvDonorHistory.View = System.Windows.Forms.View.Details;
            // 
            // colHisCount
            // 
            this.colHisCount.Text = "Cnt";
            this.colHisCount.Width = 38;
            // 
            // colHisDate
            // 
            this.colHisDate.Text = "Donate Date";
            this.colHisDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colHisDate.Width = 90;
            // 
            // colHisDollars
            // 
            this.colHisDollars.Text = "Dollars";
            this.colHisDollars.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colHisDollars.Width = 85;
            // 
            // colHisNotes
            // 
            this.colHisNotes.Text = "Notes";
            this.colHisNotes.Width = 500;
            // 
            // colHisTrxID
            // 
            this.colHisTrxID.Text = "TrxId";
            this.colHisTrxID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colHisTrxID.Width = 0;
            // 
            // cboDonorPeriod
            // 
            this.cboDonorPeriod.FormattingEnabled = true;
            this.cboDonorPeriod.Items.AddRange(new object[] {
            "Current Month",
            "Previous Month",
            "Last 90 Days",
            "Current Year",
            "Previous Year",
            "All",
            "Custom Range"});
            this.cboDonorPeriod.Location = new System.Drawing.Point(132, 16);
            this.cboDonorPeriod.Name = "cboDonorPeriod";
            this.cboDonorPeriod.Size = new System.Drawing.Size(140, 24);
            this.cboDonorPeriod.TabIndex = 25;
            this.cboDonorPeriod.SelectedIndexChanged += new System.EventHandler(this.cboDonorPeriod_SelectedIndexChanged);
            // 
            // btnLoadCustom
            // 
            this.btnLoadCustom.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadCustom.Location = new System.Drawing.Point(278, 72);
            this.btnLoadCustom.Name = "btnLoadCustom";
            this.btnLoadCustom.Size = new System.Drawing.Size(88, 21);
            this.btnLoadCustom.TabIndex = 24;
            this.btnLoadCustom.Text = "&Refresh Log";
            this.btnLoadCustom.UseVisualStyleBackColor = true;
            this.btnLoadCustom.Visible = false;
            this.btnLoadCustom.Click += new System.EventHandler(this.btnLoadCustom_Click);
            // 
            // lblTo
            // 
            this.lblTo.Location = new System.Drawing.Point(34, 74);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(29, 17);
            this.lblTo.TabIndex = 22;
            this.lblTo.Text = "To:";
            this.lblTo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblTo.Visible = false;
            // 
            // lblFrom
            // 
            this.lblFrom.Location = new System.Drawing.Point(19, 49);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(44, 17);
            this.lblFrom.TabIndex = 20;
            this.lblFrom.Text = "From:";
            this.lblFrom.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblFrom.Visible = false;
            // 
            // dtpTo
            // 
            this.dtpTo.CustomFormat = "MM/dd/yyyy dddd";
            this.dtpTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTo.Location = new System.Drawing.Point(66, 72);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(206, 21);
            this.dtpTo.TabIndex = 23;
            this.dtpTo.Value = new System.DateTime(2010, 11, 17, 12, 1, 0, 0);
            this.dtpTo.Visible = false;
            // 
            // dtpFrom
            // 
            this.dtpFrom.CustomFormat = "MM/dd/yyyy dddd";
            this.dtpFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFrom.Location = new System.Drawing.Point(66, 46);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(206, 21);
            this.dtpFrom.TabIndex = 21;
            this.dtpFrom.Value = new System.DateTime(2010, 11, 17, 12, 1, 0, 0);
            this.dtpFrom.Visible = false;
            // 
            // btnHideDonorHist
            // 
            this.btnHideDonorHist.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHideDonorHist.Location = new System.Drawing.Point(1000, 0);
            this.btnHideDonorHist.Name = "btnHideDonorHist";
            this.btnHideDonorHist.Size = new System.Drawing.Size(107, 24);
            this.btnHideDonorHist.TabIndex = 13;
            this.btnHideDonorHist.Text = "&Hide Donor History";
            this.btnHideDonorHist.UseVisualStyleBackColor = true;
            this.btnHideDonorHist.Click += new System.EventHandler(this.btnHideDonorHist_Click);
            // 
            // tbTotalDonorDollars
            // 
            this.tbTotalDonorDollars.Location = new System.Drawing.Point(129, 99);
            this.tbTotalDonorDollars.Name = "tbTotalDonorDollars";
            this.tbTotalDonorDollars.Size = new System.Drawing.Size(88, 23);
            this.tbTotalDonorDollars.TabIndex = 12;
            this.tbTotalDonorDollars.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbDonorCnt
            // 
            this.tbDonorCnt.Location = new System.Drawing.Point(1, 99);
            this.tbDonorCnt.Name = "tbDonorCnt";
            this.tbDonorCnt.Size = new System.Drawing.Size(40, 23);
            this.tbDonorCnt.TabIndex = 10;
            this.tbDonorCnt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(28, 109);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 15);
            this.label6.TabIndex = 60;
            this.label6.Text = "Dollar Amount";
            // 
            // tbDollars
            // 
            this.tbDollars.Location = new System.Drawing.Point(132, 106);
            this.tbDollars.Name = "tbDollars";
            this.tbDollars.Size = new System.Drawing.Size(96, 23);
            this.tbDollars.TabIndex = 61;
            this.tbDollars.Tag = "DollarValue";
            this.tbDollars.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbDollars_KeyDown);
            this.tbDollars.Validating += new System.ComponentModel.CancelEventHandler(this.tbDollars_Validating);
            // 
            // tbName
            // 
            this.tbName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbName.ForeColor = System.Drawing.Color.MediumBlue;
            this.tbName.Location = new System.Drawing.Point(188, 68);
            this.tbName.Name = "tbName";
            this.tbName.ReadOnly = true;
            this.tbName.Size = new System.Drawing.Size(280, 21);
            this.tbName.TabIndex = 54;
            this.tbName.TabStop = false;
            // 
            // tbDonorID
            // 
            this.tbDonorID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbDonorID.ForeColor = System.Drawing.Color.MediumBlue;
            this.tbDonorID.Location = new System.Drawing.Point(132, 68);
            this.tbDonorID.Name = "tbDonorID";
            this.tbDonorID.Size = new System.Drawing.Size(50, 21);
            this.tbDonorID.TabIndex = 53;
            this.tbDonorID.Tag = "DonorID";
            this.tbDonorID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbDonorID_KeyDown);
            this.tbDonorID.Leave += new System.EventHandler(this.tbDonorID_Leave);
            // 
            // tbTrxID
            // 
            this.tbTrxID.BackColor = System.Drawing.Color.White;
            this.tbTrxID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbTrxID.ForeColor = System.Drawing.Color.MediumBlue;
            this.tbTrxID.Location = new System.Drawing.Point(133, 6);
            this.tbTrxID.Name = "tbTrxID";
            this.tbTrxID.ReadOnly = true;
            this.tbTrxID.Size = new System.Drawing.Size(91, 21);
            this.tbTrxID.TabIndex = 67;
            this.tbTrxID.TabStop = false;
            this.tbTrxID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // dtDonationDate
            // 
            this.dtDonationDate.CustomFormat = "MM/dd/yyyy dddd";
            this.dtDonationDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtDonationDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtDonationDate.Location = new System.Drawing.Point(133, 33);
            this.dtDonationDate.Name = "dtDonationDate";
            this.dtDonationDate.Size = new System.Drawing.Size(224, 21);
            this.dtDonationDate.TabIndex = 51;
            this.dtDonationDate.Value = new System.DateTime(2010, 11, 17, 12, 1, 0, 0);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowse.Location = new System.Drawing.Point(474, 66);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(63, 26);
            this.btnBrowse.TabIndex = 55;
            this.btnBrowse.Text = "&Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnLogEntrySave
            // 
            this.btnLogEntrySave.Location = new System.Drawing.Point(620, 147);
            this.btnLogEntrySave.Name = "btnLogEntrySave";
            this.btnLogEntrySave.Size = new System.Drawing.Size(91, 40);
            this.btnLogEntrySave.TabIndex = 68;
            this.btnLogEntrySave.Text = "&Save";
            this.btnLogEntrySave.UseVisualStyleBackColor = true;
            this.btnLogEntrySave.Click += new System.EventHandler(this.btnLogEntrySave_Click);
            // 
            // btnLogEntryCancel
            // 
            this.btnLogEntryCancel.Location = new System.Drawing.Point(717, 146);
            this.btnLogEntryCancel.Name = "btnLogEntryCancel";
            this.btnLogEntryCancel.Size = new System.Drawing.Size(91, 40);
            this.btnLogEntryCancel.TabIndex = 69;
            this.btnLogEntryCancel.Text = "&Cancel";
            this.btnLogEntryCancel.UseVisualStyleBackColor = true;
            this.btnLogEntryCancel.Click += new System.EventHandler(this.btnLogEntryCancel_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(74, 152);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 15);
            this.label4.TabIndex = 64;
            this.label4.Text = "Notes";
            // 
            // tbNotes
            // 
            this.tbNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbNotes.Location = new System.Drawing.Point(132, 146);
            this.tbNotes.Multiline = true;
            this.tbNotes.Name = "tbNotes";
            this.tbNotes.Size = new System.Drawing.Size(464, 48);
            this.tbNotes.TabIndex = 65;
            this.tbNotes.Tag = "Notes";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(27, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 15);
            this.label2.TabIndex = 50;
            this.label2.Text = "Donation Date";
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblID.Location = new System.Drawing.Point(22, 71);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(91, 15);
            this.lblID.TabIndex = 52;
            this.lblID.Text = "Donor Id/Name";
            // 
            // lblTrxID
            // 
            this.lblTrxID.AutoSize = true;
            this.lblTrxID.Location = new System.Drawing.Point(68, 10);
            this.lblTrxID.Name = "lblTrxID";
            this.lblTrxID.Size = new System.Drawing.Size(41, 17);
            this.lblTrxID.TabIndex = 66;
            this.lblTrxID.Text = "TrxID";
            // 
            // spltcontLog
            // 
            this.spltcontLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spltcontLog.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.spltcontLog.IsSplitterFixed = true;
            this.spltcontLog.Location = new System.Drawing.Point(0, 0);
            this.spltcontLog.Name = "spltcontLog";
            this.spltcontLog.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // spltcontLog.Panel1
            // 
            this.spltcontLog.Panel1.Controls.Add(this.pnlPeriod);
            this.spltcontLog.Panel1.Controls.Add(this.cboDisplayType);
            this.spltcontLog.Panel1.Controls.Add(this.btnDeleteTrx);
            this.spltcontLog.Panel1.Controls.Add(this.btnNewDonation);
            this.spltcontLog.Panel1.Controls.Add(this.btnClose);
            this.spltcontLog.Panel1.Controls.Add(this.pnlEntryDate);
            this.spltcontLog.Panel1.Controls.Add(this.btnShowDnrHist);
            this.spltcontLog.Panel1.Controls.Add(this.tbTotalCount);
            this.spltcontLog.Panel1.Controls.Add(this.btnEditDnrTrx);
            this.spltcontLog.Panel1.Controls.Add(this.tbTotalDollars);
            // 
            // spltcontLog.Panel2
            // 
            this.spltcontLog.Panel2.Controls.Add(this.lvFoodDonations);
            this.spltcontLog.Size = new System.Drawing.Size(1110, 706);
            this.spltcontLog.SplitterDistance = 173;
            this.spltcontLog.SplitterWidth = 1;
            this.spltcontLog.TabIndex = 18;
            // 
            // pnlPeriod
            // 
            this.pnlPeriod.Controls.Add(this.btnLoadPeriodData);
            this.pnlPeriod.Controls.Add(this.label3);
            this.pnlPeriod.Controls.Add(this.label1);
            this.pnlPeriod.Controls.Add(this.cboYear);
            this.pnlPeriod.Controls.Add(this.cboReportMonth);
            this.pnlPeriod.Location = new System.Drawing.Point(16, 37);
            this.pnlPeriod.Name = "pnlPeriod";
            this.pnlPeriod.Size = new System.Drawing.Size(328, 63);
            this.pnlPeriod.TabIndex = 55;
            // 
            // cboDisplayType
            // 
            this.cboDisplayType.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.cboDisplayType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboDisplayType.ForeColor = System.Drawing.Color.Maroon;
            this.cboDisplayType.FormattingEnabled = true;
            this.cboDisplayType.Items.AddRange(new object[] {
            "List By Entry Date",
            "List By Donation Date"});
            this.cboDisplayType.Location = new System.Drawing.Point(87, 6);
            this.cboDisplayType.Name = "cboDisplayType";
            this.cboDisplayType.Size = new System.Drawing.Size(198, 24);
            this.cboDisplayType.TabIndex = 48;
            this.cboDisplayType.SelectedIndexChanged += new System.EventHandler(this.cboDisplayType_SelectedIndexChanged);
            // 
            // pnlEntryDate
            // 
            this.pnlEntryDate.Controls.Add(this.dtDateDL);
            this.pnlEntryDate.Controls.Add(this.btnPrevDL);
            this.pnlEntryDate.Controls.Add(this.btnFirstDL);
            this.pnlEntryDate.Controls.Add(this.btnLastDL);
            this.pnlEntryDate.Controls.Add(this.btnNextDL);
            this.pnlEntryDate.Location = new System.Drawing.Point(769, 38);
            this.pnlEntryDate.Name = "pnlEntryDate";
            this.pnlEntryDate.Size = new System.Drawing.Size(329, 63);
            this.pnlEntryDate.TabIndex = 54;
            // 
            // dtDateDL
            // 
            this.dtDateDL.CustomFormat = "MM/dd/yyyy dddd";
            this.dtDateDL.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtDateDL.Location = new System.Drawing.Point(50, 18);
            this.dtDateDL.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.dtDateDL.Name = "dtDateDL";
            this.dtDateDL.Size = new System.Drawing.Size(224, 23);
            this.dtDateDL.TabIndex = 53;
            this.dtDateDL.Value = new System.DateTime(2010, 11, 17, 11, 57, 0, 0);
            this.dtDateDL.ValueChanged += new System.EventHandler(this.dtDateDL_ValueChanged);
            // 
            // btnPrevDL
            // 
            this.btnPrevDL.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            this.btnPrevDL.Location = new System.Drawing.Point(282, 7);
            this.btnPrevDL.Margin = new System.Windows.Forms.Padding(0);
            this.btnPrevDL.Name = "btnPrevDL";
            this.btnPrevDL.Size = new System.Drawing.Size(37, 25);
            this.btnPrevDL.TabIndex = 51;
            this.btnPrevDL.Tag = "1";
            this.btnPrevDL.Text = "&Prev";
            this.btnPrevDL.UseVisualStyleBackColor = true;
            this.btnPrevDL.Click += new System.EventHandler(this.btnNavDL_Click);
            // 
            // btnFirstDL
            // 
            this.btnFirstDL.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            this.btnFirstDL.Location = new System.Drawing.Point(10, 7);
            this.btnFirstDL.Margin = new System.Windows.Forms.Padding(0);
            this.btnFirstDL.Name = "btnFirstDL";
            this.btnFirstDL.Size = new System.Drawing.Size(37, 25);
            this.btnFirstDL.TabIndex = 49;
            this.btnFirstDL.Tag = "99";
            this.btnFirstDL.Text = "&First";
            this.btnFirstDL.UseVisualStyleBackColor = true;
            this.btnFirstDL.Click += new System.EventHandler(this.btnNavDL_Click);
            // 
            // btnLastDL
            // 
            this.btnLastDL.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            this.btnLastDL.Location = new System.Drawing.Point(10, 31);
            this.btnLastDL.Margin = new System.Windows.Forms.Padding(0);
            this.btnLastDL.Name = "btnLastDL";
            this.btnLastDL.Size = new System.Drawing.Size(37, 25);
            this.btnLastDL.TabIndex = 50;
            this.btnLastDL.Tag = "0";
            this.btnLastDL.Text = "&Last";
            this.btnLastDL.UseVisualStyleBackColor = true;
            this.btnLastDL.Click += new System.EventHandler(this.btnNavDL_Click);
            // 
            // btnNextDL
            // 
            this.btnNextDL.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            this.btnNextDL.Location = new System.Drawing.Point(282, 31);
            this.btnNextDL.Margin = new System.Windows.Forms.Padding(0);
            this.btnNextDL.Name = "btnNextDL";
            this.btnNextDL.Size = new System.Drawing.Size(37, 25);
            this.btnNextDL.TabIndex = 52;
            this.btnNextDL.Tag = "-1";
            this.btnNextDL.Text = "&Next";
            this.btnNextDL.UseVisualStyleBackColor = true;
            this.btnNextDL.Click += new System.EventHandler(this.btnNavDL_Click);
            // 
            // spltcontEdit
            // 
            this.spltcontEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spltcontEdit.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.spltcontEdit.Location = new System.Drawing.Point(0, 0);
            this.spltcontEdit.Name = "spltcontEdit";
            this.spltcontEdit.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // spltcontEdit.Panel1
            // 
            this.spltcontEdit.Panel1.BackColor = System.Drawing.Color.LightGreen;
            this.spltcontEdit.Panel1.Controls.Add(this.tbTrxID);
            this.spltcontEdit.Panel1.Controls.Add(this.label6);
            this.spltcontEdit.Panel1.Controls.Add(this.btnBrowse);
            this.spltcontEdit.Panel1.Controls.Add(this.btnLogEntrySave);
            this.spltcontEdit.Panel1.Controls.Add(this.dtDonationDate);
            this.spltcontEdit.Panel1.Controls.Add(this.btnLogEntryCancel);
            this.spltcontEdit.Panel1.Controls.Add(this.tbDollars);
            this.spltcontEdit.Panel1.Controls.Add(this.lblTrxID);
            this.spltcontEdit.Panel1.Controls.Add(this.lblID);
            this.spltcontEdit.Panel1.Controls.Add(this.tbName);
            this.spltcontEdit.Panel1.Controls.Add(this.label2);
            this.spltcontEdit.Panel1.Controls.Add(this.tbDonorID);
            this.spltcontEdit.Panel1.Controls.Add(this.tbNotes);
            this.spltcontEdit.Panel1.Controls.Add(this.label4);
            // 
            // spltcontEdit.Panel2
            // 
            this.spltcontEdit.Panel2.Controls.Add(this.spltcontHistory);
            this.spltcontEdit.Size = new System.Drawing.Size(1110, 706);
            this.spltcontEdit.SplitterDistance = 209;
            this.spltcontEdit.TabIndex = 19;
            // 
            // spltcontHistory
            // 
            this.spltcontHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spltcontHistory.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.spltcontHistory.IsSplitterFixed = true;
            this.spltcontHistory.Location = new System.Drawing.Point(0, 0);
            this.spltcontHistory.Name = "spltcontHistory";
            this.spltcontHistory.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // spltcontHistory.Panel1
            // 
            this.spltcontHistory.Panel1.BackColor = System.Drawing.Color.Tan;
            this.spltcontHistory.Panel1.Controls.Add(this.lblText);
            this.spltcontHistory.Panel1.Controls.Add(this.label5);
            this.spltcontHistory.Panel1.Controls.Add(this.lblDonorHist);
            this.spltcontHistory.Panel1.Controls.Add(this.btnHideDonorHist);
            this.spltcontHistory.Panel1.Controls.Add(this.cboDonorPeriod);
            this.spltcontHistory.Panel1.Controls.Add(this.tbDonorCnt);
            this.spltcontHistory.Panel1.Controls.Add(this.btnLoadCustom);
            this.spltcontHistory.Panel1.Controls.Add(this.tbTotalDonorDollars);
            this.spltcontHistory.Panel1.Controls.Add(this.lblTo);
            this.spltcontHistory.Panel1.Controls.Add(this.dtpFrom);
            this.spltcontHistory.Panel1.Controls.Add(this.lblFrom);
            this.spltcontHistory.Panel1.Controls.Add(this.dtpTo);
            // 
            // spltcontHistory.Panel2
            // 
            this.spltcontHistory.Panel2.Controls.Add(this.lvDonorHistory);
            this.spltcontHistory.Size = new System.Drawing.Size(1110, 493);
            this.spltcontHistory.SplitterDistance = 124;
            this.spltcontHistory.SplitterWidth = 1;
            this.spltcontHistory.TabIndex = 0;
            // 
            // lblText
            // 
            this.lblText.AutoSize = true;
            this.lblText.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.lblText.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblText.Location = new System.Drawing.Point(285, 36);
            this.lblText.Name = "lblText";
            this.lblText.Size = new System.Drawing.Size(145, 18);
            this.lblText.TabIndex = 28;
            this.lblText.Text = "Donation History";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(19, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(109, 21);
            this.label5.TabIndex = 27;
            this.label5.Text = "Display Period:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label5.Visible = false;
            // 
            // lblDonorHist
            // 
            this.lblDonorHist.AutoSize = true;
            this.lblDonorHist.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.lblDonorHist.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDonorHist.Location = new System.Drawing.Point(285, 18);
            this.lblDonorHist.Name = "lblDonorHist";
            this.lblDonorHist.Size = new System.Drawing.Size(107, 18);
            this.lblDonorHist.TabIndex = 26;
            this.lblDonorHist.Text = "lblDonorHist";
            // 
            // CashDonationsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Cornsilk;
            this.ClientSize = new System.Drawing.Size(1110, 706);
            this.Controls.Add(this.spltcontLog);
            this.Controls.Add(this.spltcontEdit);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "CashDonationsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Cash Donations Maintenance Form";
            this.spltcontLog.Panel1.ResumeLayout(false);
            this.spltcontLog.Panel1.PerformLayout();
            this.spltcontLog.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spltcontLog)).EndInit();
            this.spltcontLog.ResumeLayout(false);
            this.pnlPeriod.ResumeLayout(false);
            this.pnlPeriod.PerformLayout();
            this.pnlEntryDate.ResumeLayout(false);
            this.spltcontEdit.Panel1.ResumeLayout(false);
            this.spltcontEdit.Panel1.PerformLayout();
            this.spltcontEdit.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spltcontEdit)).EndInit();
            this.spltcontEdit.ResumeLayout(false);
            this.spltcontHistory.Panel1.ResumeLayout(false);
            this.spltcontHistory.Panel1.PerformLayout();
            this.spltcontHistory.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spltcontHistory)).EndInit();
            this.spltcontHistory.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvFoodDonations;
        private System.Windows.Forms.ColumnHeader colCntLog;
        private System.Windows.Forms.ColumnHeader colTrxIdLog;
        private System.Windows.Forms.ColumnHeader colDonorLog;
        private System.Windows.Forms.ColumnHeader colNotesLog;
        private System.Windows.Forms.TextBox tbTotalCount;
        private System.Windows.Forms.TextBox tbTotalDollars;
        private System.Windows.Forms.Button btnShowDnrHist;
        private System.Windows.Forms.Button btnEditDnrTrx;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.TextBox tbDonorID;
        private System.Windows.Forms.Label lblTrxID;
        private System.Windows.Forms.TextBox tbTrxID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnHideDonorHist;
        private System.Windows.Forms.TextBox tbTotalDonorDollars;
        private System.Windows.Forms.TextBox tbDonorCnt;
        private System.Windows.Forms.Button btnLogEntrySave;
        private System.Windows.Forms.Button btnLogEntryCancel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbNotes;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.DateTimePicker dtDonationDate;
        private System.Windows.Forms.ColumnHeader colDonorIdLog;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnNewDonation;
        private System.Windows.Forms.ColumnHeader colDate;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.Button btnLoadCustom;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbDollars;
        private System.Windows.Forms.ComboBox cboDonorPeriod;
        private System.Windows.Forms.ColumnHeader colDollars;
        private System.Windows.Forms.ListView lvDonorHistory;
        private System.Windows.Forms.ColumnHeader colHisCount;
        private System.Windows.Forms.ColumnHeader colHisDate;
        private System.Windows.Forms.ColumnHeader colHisDollars;
        private System.Windows.Forms.ColumnHeader colHisNotes;
        private System.Windows.Forms.ColumnHeader colHisTrxID;
        private System.Windows.Forms.Button btnLoadPeriodData;
        private System.Windows.Forms.ComboBox cboReportMonth;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboYear;
        private System.Windows.Forms.Button btnDeleteTrx;
        private System.Windows.Forms.SplitContainer spltcontLog;
        private System.Windows.Forms.SplitContainer spltcontEdit;
        private System.Windows.Forms.SplitContainer spltcontHistory;
        private System.Windows.Forms.Label lblDonorHist;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboDisplayType;
        private System.Windows.Forms.Panel pnlEntryDate;
        private System.Windows.Forms.DateTimePicker dtDateDL;
        private System.Windows.Forms.Button btnFirstDL;
        private System.Windows.Forms.Button btnNextDL;
        private System.Windows.Forms.Button btnLastDL;
        private System.Windows.Forms.Button btnPrevDL;
        private System.Windows.Forms.Panel pnlPeriod;
        private System.Windows.Forms.Label lblText;
    }
}