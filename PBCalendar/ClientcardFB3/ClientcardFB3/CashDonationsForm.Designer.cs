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
            this.gbDailyLog = new System.Windows.Forms.GroupBox();
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
            this.tbTotalLbs = new System.Windows.Forms.TextBox();
            this.tbTotalCount = new System.Windows.Forms.TextBox();
            this.gbDnrHist = new System.Windows.Forms.GroupBox();
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
            this.tbDonorLbs = new System.Windows.Forms.TextBox();
            this.tbDonorCnt = new System.Windows.Forms.TextBox();
            this.gbLogEntry = new System.Windows.Forms.GroupBox();
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
            this.gbDailyLog.SuspendLayout();
            this.gbDnrHist.SuspendLayout();
            this.gbLogEntry.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbDailyLog
            // 
            this.gbDailyLog.BackColor = System.Drawing.Color.Cornsilk;
            this.gbDailyLog.Controls.Add(this.btnLoadPeriodData);
            this.gbDailyLog.Controls.Add(this.cboReportMonth);
            this.gbDailyLog.Controls.Add(this.label1);
            this.gbDailyLog.Controls.Add(this.label3);
            this.gbDailyLog.Controls.Add(this.cboYear);
            this.gbDailyLog.Controls.Add(this.lvFoodDonations);
            this.gbDailyLog.Controls.Add(this.btnNewDonation);
            this.gbDailyLog.Controls.Add(this.btnClose);
            this.gbDailyLog.Controls.Add(this.btnShowDnrHist);
            this.gbDailyLog.Controls.Add(this.btnEditDnrTrx);
            this.gbDailyLog.Controls.Add(this.tbTotalLbs);
            this.gbDailyLog.Controls.Add(this.tbTotalCount);
            this.gbDailyLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbDailyLog.Location = new System.Drawing.Point(0, 0);
            this.gbDailyLog.Margin = new System.Windows.Forms.Padding(4);
            this.gbDailyLog.Name = "gbDailyLog";
            this.gbDailyLog.Padding = new System.Windows.Forms.Padding(4);
            this.gbDailyLog.Size = new System.Drawing.Size(1005, 206);
            this.gbDailyLog.TabIndex = 0;
            this.gbDailyLog.TabStop = false;
            this.gbDailyLog.Text = "Cash Donation Daily Log";
            this.gbDailyLog.Enter += new System.EventHandler(this.gbDailyLog_Enter);
            this.gbDailyLog.Resize += new System.EventHandler(this.gbDailyLog_Resize);
            // 
            // btnLoadPeriodData
            // 
            this.btnLoadPeriodData.Location = new System.Drawing.Point(206, 35);
            this.btnLoadPeriodData.Name = "btnLoadPeriodData";
            this.btnLoadPeriodData.Size = new System.Drawing.Size(110, 32);
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
            this.cboReportMonth.Location = new System.Drawing.Point(78, 41);
            this.cboReportMonth.Name = "cboReportMonth";
            this.cboReportMonth.Size = new System.Drawing.Size(107, 24);
            this.cboReportMonth.TabIndex = 45;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(75, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 17);
            this.label1.TabIndex = 44;
            this.label1.Text = "Month";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 17);
            this.label3.TabIndex = 43;
            this.label3.Text = "Year";
            // 
            // cboYear
            // 
            this.cboYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboYear.FormattingEnabled = true;
            this.cboYear.Location = new System.Drawing.Point(10, 41);
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
            this.lvFoodDonations.FullRowSelect = true;
            this.lvFoodDonations.GridLines = true;
            this.lvFoodDonations.HideSelection = false;
            this.lvFoodDonations.Location = new System.Drawing.Point(4, 158);
            this.lvFoodDonations.Name = "lvFoodDonations";
            this.lvFoodDonations.Size = new System.Drawing.Size(1000, 60);
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
            this.colDollars.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
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
            this.btnNewDonation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewDonation.Location = new System.Drawing.Point(56, 112);
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
            this.btnClose.Location = new System.Drawing.Point(900, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(96, 28);
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
            this.btnShowDnrHist.Location = new System.Drawing.Point(612, 33);
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
            this.btnEditDnrTrx.Location = new System.Drawing.Point(405, 33);
            this.btnEditDnrTrx.Name = "btnEditDnrTrx";
            this.btnEditDnrTrx.Size = new System.Drawing.Size(201, 72);
            this.btnEditDnrTrx.TabIndex = 13;
            this.btnEditDnrTrx.UseVisualStyleBackColor = true;
            this.btnEditDnrTrx.Click += new System.EventHandler(this.btnEditDnrTrx_Click);
            // 
            // tbTotalLbs
            // 
            this.tbTotalLbs.Location = new System.Drawing.Point(434, 133);
            this.tbTotalLbs.Name = "tbTotalLbs";
            this.tbTotalLbs.Size = new System.Drawing.Size(84, 23);
            this.tbTotalLbs.TabIndex = 12;
            this.tbTotalLbs.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbTotalCount
            // 
            this.tbTotalCount.Location = new System.Drawing.Point(4, 133);
            this.tbTotalCount.Name = "tbTotalCount";
            this.tbTotalCount.Size = new System.Drawing.Size(42, 23);
            this.tbTotalCount.TabIndex = 10;
            this.tbTotalCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // gbDnrHist
            // 
            this.gbDnrHist.BackColor = System.Drawing.Color.Tan;
            this.gbDnrHist.Controls.Add(this.lvDonorHistory);
            this.gbDnrHist.Controls.Add(this.cboDonorPeriod);
            this.gbDnrHist.Controls.Add(this.btnLoadCustom);
            this.gbDnrHist.Controls.Add(this.lblTo);
            this.gbDnrHist.Controls.Add(this.lblFrom);
            this.gbDnrHist.Controls.Add(this.dtpTo);
            this.gbDnrHist.Controls.Add(this.dtpFrom);
            this.gbDnrHist.Controls.Add(this.btnHideDonorHist);
            this.gbDnrHist.Controls.Add(this.tbDonorLbs);
            this.gbDnrHist.Controls.Add(this.tbDonorCnt);
            this.gbDnrHist.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbDnrHist.Location = new System.Drawing.Point(3, 225);
            this.gbDnrHist.Margin = new System.Windows.Forms.Padding(4);
            this.gbDnrHist.Name = "gbDnrHist";
            this.gbDnrHist.Padding = new System.Windows.Forms.Padding(4);
            this.gbDnrHist.Size = new System.Drawing.Size(1000, 257);
            this.gbDnrHist.TabIndex = 15;
            this.gbDnrHist.TabStop = false;
            this.gbDnrHist.Text = "Donor History";
            this.gbDnrHist.Visible = false;
            this.gbDnrHist.Resize += new System.EventHandler(this.gbDnrHist_Resize);
            // 
            // lvDonorHistory
            // 
            this.lvDonorHistory.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colHisCount,
            this.colHisDate,
            this.colHisDollars,
            this.colHisNotes,
            this.colHisTrxID});
            this.lvDonorHistory.FullRowSelect = true;
            this.lvDonorHistory.GridLines = true;
            this.lvDonorHistory.HideSelection = false;
            this.lvDonorHistory.Location = new System.Drawing.Point(0, 130);
            this.lvDonorHistory.Name = "lvDonorHistory";
            this.lvDonorHistory.Size = new System.Drawing.Size(1000, 60);
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
            this.colHisDollars.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
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
            this.cboDonorPeriod.Location = new System.Drawing.Point(51, 22);
            this.cboDonorPeriod.Name = "cboDonorPeriod";
            this.cboDonorPeriod.Size = new System.Drawing.Size(126, 24);
            this.cboDonorPeriod.TabIndex = 25;
            this.cboDonorPeriod.SelectedIndexChanged += new System.EventHandler(this.cboDonorPeriod_SelectedIndexChanged);
            // 
            // btnLoadCustom
            // 
            this.btnLoadCustom.Location = new System.Drawing.Point(188, 22);
            this.btnLoadCustom.Name = "btnLoadCustom";
            this.btnLoadCustom.Size = new System.Drawing.Size(69, 28);
            this.btnLoadCustom.TabIndex = 24;
            this.btnLoadCustom.Text = "&Refresh";
            this.btnLoadCustom.UseVisualStyleBackColor = true;
            this.btnLoadCustom.Visible = false;
            this.btnLoadCustom.Click += new System.EventHandler(this.btnLoadCustom_Click);
            // 
            // lblTo
            // 
            this.lblTo.Location = new System.Drawing.Point(19, 84);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(29, 17);
            this.lblTo.TabIndex = 22;
            this.lblTo.Text = "To:";
            this.lblTo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblTo.Visible = false;
            // 
            // lblFrom
            // 
            this.lblFrom.Location = new System.Drawing.Point(4, 55);
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
            this.dtpTo.Location = new System.Drawing.Point(51, 82);
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
            this.dtpFrom.Location = new System.Drawing.Point(51, 52);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(206, 21);
            this.dtpFrom.TabIndex = 21;
            this.dtpFrom.Value = new System.DateTime(2010, 11, 17, 12, 1, 0, 0);
            this.dtpFrom.Visible = false;
            // 
            // btnHideDonorHist
            // 
            this.btnHideDonorHist.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHideDonorHist.Location = new System.Drawing.Point(792, 17);
            this.btnHideDonorHist.Name = "btnHideDonorHist";
            this.btnHideDonorHist.Size = new System.Drawing.Size(201, 35);
            this.btnHideDonorHist.TabIndex = 13;
            this.btnHideDonorHist.Text = "&Hide Donor History";
            this.btnHideDonorHist.UseVisualStyleBackColor = true;
            this.btnHideDonorHist.Click += new System.EventHandler(this.btnHideDonorHist_Click);
            // 
            // tbDonorLbs
            // 
            this.tbDonorLbs.Location = new System.Drawing.Point(220, 105);
            this.tbDonorLbs.Name = "tbDonorLbs";
            this.tbDonorLbs.Size = new System.Drawing.Size(80, 23);
            this.tbDonorLbs.TabIndex = 12;
            this.tbDonorLbs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbDonorCnt
            // 
            this.tbDonorCnt.Location = new System.Drawing.Point(3, 105);
            this.tbDonorCnt.Name = "tbDonorCnt";
            this.tbDonorCnt.Size = new System.Drawing.Size(40, 23);
            this.tbDonorCnt.TabIndex = 10;
            this.tbDonorCnt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // gbLogEntry
            // 
            this.gbLogEntry.BackColor = System.Drawing.Color.LightGreen;
            this.gbLogEntry.Controls.Add(this.label6);
            this.gbLogEntry.Controls.Add(this.tbDollars);
            this.gbLogEntry.Controls.Add(this.tbName);
            this.gbLogEntry.Controls.Add(this.tbDonorID);
            this.gbLogEntry.Controls.Add(this.tbTrxID);
            this.gbLogEntry.Controls.Add(this.dtDonationDate);
            this.gbLogEntry.Controls.Add(this.btnBrowse);
            this.gbLogEntry.Controls.Add(this.btnLogEntrySave);
            this.gbLogEntry.Controls.Add(this.btnLogEntryCancel);
            this.gbLogEntry.Controls.Add(this.label4);
            this.gbLogEntry.Controls.Add(this.tbNotes);
            this.gbLogEntry.Controls.Add(this.label2);
            this.gbLogEntry.Controls.Add(this.lblID);
            this.gbLogEntry.Controls.Add(this.lblTrxID);
            this.gbLogEntry.Location = new System.Drawing.Point(45, 489);
            this.gbLogEntry.Name = "gbLogEntry";
            this.gbLogEntry.Size = new System.Drawing.Size(744, 256);
            this.gbLogEntry.TabIndex = 17;
            this.gbLogEntry.TabStop = false;
            this.gbLogEntry.Text = "Edit Cash Donation Log Entry";
            this.gbLogEntry.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(14, 108);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 15);
            this.label6.TabIndex = 60;
            this.label6.Text = "Dollar Amount";
            // 
            // tbDollars
            // 
            this.tbDollars.Location = new System.Drawing.Point(136, 104);
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
            this.tbName.Location = new System.Drawing.Point(193, 62);
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
            this.tbDonorID.Location = new System.Drawing.Point(137, 62);
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
            this.tbTrxID.Location = new System.Drawing.Point(636, 31);
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
            this.dtDonationDate.Location = new System.Drawing.Point(138, 27);
            this.dtDonationDate.Name = "dtDonationDate";
            this.dtDonationDate.Size = new System.Drawing.Size(224, 21);
            this.dtDonationDate.TabIndex = 51;
            this.dtDonationDate.Value = new System.DateTime(2010, 11, 17, 12, 1, 0, 0);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowse.Location = new System.Drawing.Point(479, 62);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(63, 26);
            this.btnBrowse.TabIndex = 55;
            this.btnBrowse.Text = "&Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnLogEntrySave
            // 
            this.btnLogEntrySave.Location = new System.Drawing.Point(636, 62);
            this.btnLogEntrySave.Name = "btnLogEntrySave";
            this.btnLogEntrySave.Size = new System.Drawing.Size(91, 40);
            this.btnLogEntrySave.TabIndex = 68;
            this.btnLogEntrySave.Text = "&Save";
            this.btnLogEntrySave.UseVisualStyleBackColor = true;
            this.btnLogEntrySave.Click += new System.EventHandler(this.btnLogEntrySave_Click);
            // 
            // btnLogEntryCancel
            // 
            this.btnLogEntryCancel.Location = new System.Drawing.Point(636, 118);
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
            this.label4.Location = new System.Drawing.Point(85, 147);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 15);
            this.label4.TabIndex = 64;
            this.label4.Text = "Notes";
            // 
            // tbNotes
            // 
            this.tbNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbNotes.Location = new System.Drawing.Point(136, 144);
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
            this.label2.Location = new System.Drawing.Point(29, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 15);
            this.label2.TabIndex = 50;
            this.label2.Text = "Donation Date";
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblID.Location = new System.Drawing.Point(23, 65);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(91, 15);
            this.lblID.TabIndex = 52;
            this.lblID.Text = "Donor Id/Name";
            // 
            // lblTrxID
            // 
            this.lblTrxID.AutoSize = true;
            this.lblTrxID.Location = new System.Drawing.Point(656, 12);
            this.lblTrxID.Name = "lblTrxID";
            this.lblTrxID.Size = new System.Drawing.Size(41, 17);
            this.lblTrxID.TabIndex = 66;
            this.lblTrxID.Text = "TrxID";
            // 
            // CashDonationsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Cornsilk;
            this.ClientSize = new System.Drawing.Size(1006, 725);
            this.Controls.Add(this.gbLogEntry);
            this.Controls.Add(this.gbDailyLog);
            this.Controls.Add(this.gbDnrHist);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "CashDonationsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Food Donations Maintenance Form";
            this.ResizeEnd += new System.EventHandler(this.FoodDonationsForm_ResizeEnd);
            this.gbDailyLog.ResumeLayout(false);
            this.gbDailyLog.PerformLayout();
            this.gbDnrHist.ResumeLayout(false);
            this.gbDnrHist.PerformLayout();
            this.gbLogEntry.ResumeLayout(false);
            this.gbLogEntry.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbDailyLog;
        private System.Windows.Forms.ListView lvFoodDonations;
        private System.Windows.Forms.ColumnHeader colCntLog;
        private System.Windows.Forms.ColumnHeader colTrxIdLog;
        private System.Windows.Forms.ColumnHeader colDonorLog;
        private System.Windows.Forms.ColumnHeader colNotesLog;
        private System.Windows.Forms.TextBox tbTotalCount;
        private System.Windows.Forms.TextBox tbTotalLbs;
        private System.Windows.Forms.Button btnShowDnrHist;
        private System.Windows.Forms.Button btnEditDnrTrx;
        private System.Windows.Forms.GroupBox gbLogEntry;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.TextBox tbDonorID;
        private System.Windows.Forms.Label lblTrxID;
        private System.Windows.Forms.TextBox tbTrxID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox gbDnrHist;
        private System.Windows.Forms.Button btnHideDonorHist;
        private System.Windows.Forms.TextBox tbDonorLbs;
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
    }
}