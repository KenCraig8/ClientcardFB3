namespace ClientcardFB3
{
    partial class FoodDonationsForm
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
            this.btnDeleteTrx = new System.Windows.Forms.Button();
            this.lvFoodDonations = new System.Windows.Forms.ListView();
            this.colCntLog = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTrxDateLog = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTypeLog = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDonorLog = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colLbsLog = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colNotesLog = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFdClassLog = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDescrLog = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDonorIdLog = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTrxIdLog = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnNewDonation = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.dtDateDL = new System.Windows.Forms.DateTimePicker();
            this.btnShowDnrHist = new System.Windows.Forms.Button();
            this.btnEditDnrTrx = new System.Windows.Forms.Button();
            this.tbTotalLbs = new System.Windows.Forms.TextBox();
            this.tbTotalCount = new System.Windows.Forms.TextBox();
            this.btnNextDL = new System.Windows.Forms.Button();
            this.btnPrevDL = new System.Windows.Forms.Button();
            this.btnLastDL = new System.Windows.Forms.Button();
            this.btnFirstDL = new System.Windows.Forms.Button();
            this.cboDonorPeriod = new System.Windows.Forms.ComboBox();
            this.lvDonorHistory = new System.Windows.Forms.ListView();
            this.colCntDH = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTypeDH = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDateDH = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colLbsDH = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colNotesDH = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFdClassDH = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCodeDH = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colIdDH = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnLoadCustom = new System.Windows.Forms.Button();
            this.lblTo = new System.Windows.Forms.Label();
            this.lblFrom = new System.Windows.Forms.Label();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.btnHideDonorHist = new System.Windows.Forms.Button();
            this.tbDonorLbs = new System.Windows.Forms.TextBox();
            this.tbDonorCnt = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tbName = new System.Windows.Forms.TextBox();
            this.tbDonorID = new System.Windows.Forms.TextBox();
            this.tbTrxID = new System.Windows.Forms.TextBox();
            this.cboDonationType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dtDonationDate = new System.Windows.Forms.DateTimePicker();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnLogEntrySave = new System.Windows.Forms.Button();
            this.btnLogEntryCancel = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.tbNotes = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbLbs = new System.Windows.Forms.TextBox();
            this.cboFoodCat = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblID = new System.Windows.Forms.Label();
            this.lblTrxID = new System.Windows.Forms.Label();
            this.spltcntrDailyLog = new System.Windows.Forms.SplitContainer();
            this.cboDisplayType = new System.Windows.Forms.ComboBox();
            this.spltcntrEdit = new System.Windows.Forms.SplitContainer();
            this.label7 = new System.Windows.Forms.Label();
            this.lblDnrHist = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.spltcntrDailyLog)).BeginInit();
            this.spltcntrDailyLog.Panel1.SuspendLayout();
            this.spltcntrDailyLog.Panel2.SuspendLayout();
            this.spltcntrDailyLog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spltcntrEdit)).BeginInit();
            this.spltcntrEdit.Panel1.SuspendLayout();
            this.spltcntrEdit.Panel2.SuspendLayout();
            this.spltcntrEdit.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnDeleteTrx
            // 
            this.btnDeleteTrx.Enabled = false;
            this.btnDeleteTrx.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteTrx.ForeColor = System.Drawing.Color.DarkRed;
            this.btnDeleteTrx.Location = new System.Drawing.Point(571, 82);
            this.btnDeleteTrx.Name = "btnDeleteTrx";
            this.btnDeleteTrx.Size = new System.Drawing.Size(201, 35);
            this.btnDeleteTrx.TabIndex = 24;
            this.btnDeleteTrx.Text = "Delete";
            this.btnDeleteTrx.UseVisualStyleBackColor = true;
            this.btnDeleteTrx.Click += new System.EventHandler(this.btnDeleteTrx_Click);
            // 
            // lvFoodDonations
            // 
            this.lvFoodDonations.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colCntLog,
            this.colTrxDateLog,
            this.colTypeLog,
            this.colDonorLog,
            this.colLbsLog,
            this.colNotesLog,
            this.colFdClassLog,
            this.colDescrLog,
            this.colDonorIdLog,
            this.colTrxIdLog});
            this.lvFoodDonations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvFoodDonations.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvFoodDonations.FullRowSelect = true;
            this.lvFoodDonations.GridLines = true;
            this.lvFoodDonations.HideSelection = false;
            this.lvFoodDonations.Location = new System.Drawing.Point(0, 0);
            this.lvFoodDonations.Margin = new System.Windows.Forms.Padding(0);
            this.lvFoodDonations.Name = "lvFoodDonations";
            this.lvFoodDonations.Size = new System.Drawing.Size(1004, 565);
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
            // colTrxDateLog
            // 
            this.colTrxDateLog.Text = "Trx Date";
            this.colTrxDateLog.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colTrxDateLog.Width = 80;
            // 
            // colTypeLog
            // 
            this.colTypeLog.Text = "Type";
            this.colTypeLog.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colTypeLog.Width = 110;
            // 
            // colDonorLog
            // 
            this.colDonorLog.Text = "Donor Name";
            this.colDonorLog.Width = 200;
            // 
            // colLbsLog
            // 
            this.colLbsLog.Text = "Lbs";
            this.colLbsLog.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colLbsLog.Width = 79;
            // 
            // colNotesLog
            // 
            this.colNotesLog.Text = "Notes";
            this.colNotesLog.Width = 300;
            // 
            // colFdClassLog
            // 
            this.colFdClassLog.Text = "Fd Class";
            this.colFdClassLog.Width = 100;
            // 
            // colDescrLog
            // 
            this.colDescrLog.Text = "Fd Descr";
            this.colDescrLog.Width = 100;
            // 
            // colDonorIdLog
            // 
            this.colDonorIdLog.Text = "Dnr Id";
            this.colDonorIdLog.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // colTrxIdLog
            // 
            this.colTrxIdLog.Text = "TrxId";
            this.colTrxIdLog.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colTrxIdLog.Width = 70;
            // 
            // btnNewDonation
            // 
            this.btnNewDonation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewDonation.Location = new System.Drawing.Point(75, 67);
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
            this.btnClose.Location = new System.Drawing.Point(946, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(55, 22);
            this.btnClose.TabIndex = 22;
            this.btnClose.Text = "&Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // dtDateDL
            // 
            this.dtDateDL.CustomFormat = "MM/dd/yyyy dddd";
            this.dtDateDL.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtDateDL.Location = new System.Drawing.Point(52, 35);
            this.dtDateDL.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.dtDateDL.Name = "dtDateDL";
            this.dtDateDL.Size = new System.Drawing.Size(224, 23);
            this.dtDateDL.TabIndex = 15;
            this.dtDateDL.Value = new System.DateTime(2010, 11, 17, 11, 57, 0, 0);
            this.dtDateDL.ValueChanged += new System.EventHandler(this.dtDateDL_ValueChanged);
            // 
            // btnShowDnrHist
            // 
            this.btnShowDnrHist.Enabled = false;
            this.btnShowDnrHist.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShowDnrHist.ForeColor = System.Drawing.Color.DarkRed;
            this.btnShowDnrHist.Location = new System.Drawing.Point(571, 8);
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
            this.btnEditDnrTrx.Location = new System.Drawing.Point(364, 8);
            this.btnEditDnrTrx.Name = "btnEditDnrTrx";
            this.btnEditDnrTrx.Size = new System.Drawing.Size(201, 72);
            this.btnEditDnrTrx.TabIndex = 13;
            this.btnEditDnrTrx.UseVisualStyleBackColor = true;
            this.btnEditDnrTrx.Click += new System.EventHandler(this.btnEditDnrTrx_Click);
            // 
            // tbTotalLbs
            // 
            this.tbTotalLbs.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbTotalLbs.Location = new System.Drawing.Point(429, 102);
            this.tbTotalLbs.Margin = new System.Windows.Forms.Padding(0);
            this.tbTotalLbs.Name = "tbTotalLbs";
            this.tbTotalLbs.Size = new System.Drawing.Size(80, 23);
            this.tbTotalLbs.TabIndex = 12;
            this.tbTotalLbs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbTotalCount
            // 
            this.tbTotalCount.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbTotalCount.Location = new System.Drawing.Point(0, 102);
            this.tbTotalCount.Margin = new System.Windows.Forms.Padding(0);
            this.tbTotalCount.Name = "tbTotalCount";
            this.tbTotalCount.Size = new System.Drawing.Size(42, 23);
            this.tbTotalCount.TabIndex = 10;
            this.tbTotalCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbTotalCount.TextChanged += new System.EventHandler(this.tbTotalCount_TextChanged);
            // 
            // btnNextDL
            // 
            this.btnNextDL.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            this.btnNextDL.Location = new System.Drawing.Point(284, 48);
            this.btnNextDL.Margin = new System.Windows.Forms.Padding(0);
            this.btnNextDL.Name = "btnNextDL";
            this.btnNextDL.Size = new System.Drawing.Size(37, 25);
            this.btnNextDL.TabIndex = 8;
            this.btnNextDL.Text = "&Next";
            this.btnNextDL.UseVisualStyleBackColor = true;
            this.btnNextDL.Click += new System.EventHandler(this.btnNextDL_Click);
            // 
            // btnPrevDL
            // 
            this.btnPrevDL.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            this.btnPrevDL.Location = new System.Drawing.Point(284, 24);
            this.btnPrevDL.Margin = new System.Windows.Forms.Padding(0);
            this.btnPrevDL.Name = "btnPrevDL";
            this.btnPrevDL.Size = new System.Drawing.Size(37, 25);
            this.btnPrevDL.TabIndex = 7;
            this.btnPrevDL.Text = "&Prev";
            this.btnPrevDL.UseVisualStyleBackColor = true;
            this.btnPrevDL.Click += new System.EventHandler(this.btnPrevDL_Click);
            // 
            // btnLastDL
            // 
            this.btnLastDL.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            this.btnLastDL.Location = new System.Drawing.Point(12, 48);
            this.btnLastDL.Margin = new System.Windows.Forms.Padding(0);
            this.btnLastDL.Name = "btnLastDL";
            this.btnLastDL.Size = new System.Drawing.Size(37, 25);
            this.btnLastDL.TabIndex = 6;
            this.btnLastDL.Text = "&Last";
            this.btnLastDL.UseVisualStyleBackColor = true;
            this.btnLastDL.Click += new System.EventHandler(this.btnLastDL_Click);
            // 
            // btnFirstDL
            // 
            this.btnFirstDL.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            this.btnFirstDL.Location = new System.Drawing.Point(12, 24);
            this.btnFirstDL.Margin = new System.Windows.Forms.Padding(0);
            this.btnFirstDL.Name = "btnFirstDL";
            this.btnFirstDL.Size = new System.Drawing.Size(37, 25);
            this.btnFirstDL.TabIndex = 5;
            this.btnFirstDL.Text = "&First";
            this.btnFirstDL.UseVisualStyleBackColor = true;
            this.btnFirstDL.Click += new System.EventHandler(this.btnFirstDL_Click);
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
            this.cboDonorPeriod.Location = new System.Drawing.Point(51, 11);
            this.cboDonorPeriod.Name = "cboDonorPeriod";
            this.cboDonorPeriod.Size = new System.Drawing.Size(126, 24);
            this.cboDonorPeriod.TabIndex = 25;
            this.cboDonorPeriod.SelectedIndexChanged += new System.EventHandler(this.cboDonorPeriod_SelectedIndexChanged);
            // 
            // lvDonorHistory
            // 
            this.lvDonorHistory.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colCntDH,
            this.colTypeDH,
            this.colDateDH,
            this.colLbsDH,
            this.colNotesDH,
            this.colFdClassDH,
            this.colCodeDH,
            this.colIdDH});
            this.lvDonorHistory.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lvDonorHistory.GridLines = true;
            this.lvDonorHistory.Location = new System.Drawing.Point(0, 122);
            this.lvDonorHistory.Name = "lvDonorHistory";
            this.lvDonorHistory.Size = new System.Drawing.Size(1004, 328);
            this.lvDonorHistory.TabIndex = 11;
            this.lvDonorHistory.UseCompatibleStateImageBehavior = false;
            this.lvDonorHistory.View = System.Windows.Forms.View.Details;
            // 
            // colCntDH
            // 
            this.colCntDH.Text = "Cnt";
            this.colCntDH.Width = 38;
            // 
            // colTypeDH
            // 
            this.colTypeDH.Text = "Type";
            this.colTypeDH.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colTypeDH.Width = 90;
            // 
            // colDateDH
            // 
            this.colDateDH.Text = "Donate Date";
            this.colDateDH.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colDateDH.Width = 90;
            // 
            // colLbsDH
            // 
            this.colLbsDH.Text = "Lbs";
            this.colLbsDH.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colLbsDH.Width = 79;
            // 
            // colNotesDH
            // 
            this.colNotesDH.Text = "Notes";
            this.colNotesDH.Width = 300;
            // 
            // colFdClassDH
            // 
            this.colFdClassDH.Text = "Fd Class";
            this.colFdClassDH.Width = 100;
            // 
            // colCodeDH
            // 
            this.colCodeDH.Text = "Fd Descr";
            this.colCodeDH.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colCodeDH.Width = 100;
            // 
            // colIdDH
            // 
            this.colIdDH.Text = "Trx Id";
            this.colIdDH.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colIdDH.Width = 70;
            // 
            // btnLoadCustom
            // 
            this.btnLoadCustom.Location = new System.Drawing.Point(188, 11);
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
            this.lblTo.Location = new System.Drawing.Point(19, 73);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(29, 17);
            this.lblTo.TabIndex = 22;
            this.lblTo.Text = "To:";
            this.lblTo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblTo.Visible = false;
            // 
            // lblFrom
            // 
            this.lblFrom.Location = new System.Drawing.Point(4, 44);
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
            this.dtpTo.Location = new System.Drawing.Point(51, 71);
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
            this.dtpFrom.Location = new System.Drawing.Point(51, 41);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(206, 21);
            this.dtpFrom.TabIndex = 21;
            this.dtpFrom.Value = new System.DateTime(2010, 11, 17, 12, 1, 0, 0);
            this.dtpFrom.Visible = false;
            // 
            // btnHideDonorHist
            // 
            this.btnHideDonorHist.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHideDonorHist.Location = new System.Drawing.Point(888, 4);
            this.btnHideDonorHist.Name = "btnHideDonorHist";
            this.btnHideDonorHist.Size = new System.Drawing.Size(113, 28);
            this.btnHideDonorHist.TabIndex = 13;
            this.btnHideDonorHist.Text = "&Hide Donor History";
            this.btnHideDonorHist.UseVisualStyleBackColor = true;
            this.btnHideDonorHist.Click += new System.EventHandler(this.btnHideDonorHist_Click);
            // 
            // tbDonorLbs
            // 
            this.tbDonorLbs.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbDonorLbs.Location = new System.Drawing.Point(220, 94);
            this.tbDonorLbs.Name = "tbDonorLbs";
            this.tbDonorLbs.Size = new System.Drawing.Size(80, 21);
            this.tbDonorLbs.TabIndex = 12;
            this.tbDonorLbs.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbDonorCnt
            // 
            this.tbDonorCnt.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbDonorCnt.Location = new System.Drawing.Point(3, 94);
            this.tbDonorCnt.Name = "tbDonorCnt";
            this.tbDonorCnt.Size = new System.Drawing.Size(40, 21);
            this.tbDonorCnt.TabIndex = 10;
            this.tbDonorCnt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(12, 152);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 15);
            this.label6.TabIndex = 60;
            this.label6.Text = "Food Description";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(134, 148);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(224, 23);
            this.textBox1.TabIndex = 61;
            this.textBox1.Tag = "FoodCode";
            // 
            // tbName
            // 
            this.tbName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbName.ForeColor = System.Drawing.Color.MediumBlue;
            this.tbName.Location = new System.Drawing.Point(189, 50);
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
            this.tbDonorID.Location = new System.Drawing.Point(133, 50);
            this.tbDonorID.Name = "tbDonorID";
            this.tbDonorID.Size = new System.Drawing.Size(50, 21);
            this.tbDonorID.TabIndex = 53;
            this.tbDonorID.Tag = "DonorID";
            this.tbDonorID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbDonorID_KeyPress);
            // 
            // tbTrxID
            // 
            this.tbTrxID.BackColor = System.Drawing.Color.White;
            this.tbTrxID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbTrxID.ForeColor = System.Drawing.Color.MediumBlue;
            this.tbTrxID.Location = new System.Drawing.Point(635, 177);
            this.tbTrxID.Name = "tbTrxID";
            this.tbTrxID.ReadOnly = true;
            this.tbTrxID.Size = new System.Drawing.Size(91, 21);
            this.tbTrxID.TabIndex = 67;
            this.tbTrxID.TabStop = false;
            this.tbTrxID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // cboDonationType
            // 
            this.cboDonationType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDonationType.FormattingEnabled = true;
            this.cboDonationType.Location = new System.Drawing.Point(133, 82);
            this.cboDonationType.Name = "cboDonationType";
            this.cboDonationType.Size = new System.Drawing.Size(225, 23);
            this.cboDonationType.TabIndex = 57;
            this.cboDonationType.Tag = "DonationType";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 85);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(125, 15);
            this.label5.TabIndex = 56;
            this.label5.Text = "Food Funding Source";
            // 
            // dtDonationDate
            // 
            this.dtDonationDate.CustomFormat = "MM/dd/yyyy dddd";
            this.dtDonationDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtDonationDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtDonationDate.Location = new System.Drawing.Point(134, 15);
            this.dtDonationDate.Name = "dtDonationDate";
            this.dtDonationDate.Size = new System.Drawing.Size(224, 21);
            this.dtDonationDate.TabIndex = 51;
            this.dtDonationDate.Value = new System.DateTime(2010, 11, 17, 12, 1, 0, 0);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowse.Location = new System.Drawing.Point(475, 48);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(63, 26);
            this.btnBrowse.TabIndex = 55;
            this.btnBrowse.Text = "&Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnLogEntrySave
            // 
            this.btnLogEntrySave.Location = new System.Drawing.Point(635, 197);
            this.btnLogEntrySave.Name = "btnLogEntrySave";
            this.btnLogEntrySave.Size = new System.Drawing.Size(91, 40);
            this.btnLogEntrySave.TabIndex = 68;
            this.btnLogEntrySave.Text = "&Save";
            this.btnLogEntrySave.UseVisualStyleBackColor = true;
            this.btnLogEntrySave.Click += new System.EventHandler(this.btnLogEntrySave_Click);
            // 
            // btnLogEntryCancel
            // 
            this.btnLogEntryCancel.Location = new System.Drawing.Point(732, 197);
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
            this.label4.Location = new System.Drawing.Point(83, 212);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 15);
            this.label4.TabIndex = 64;
            this.label4.Text = "Notes";
            // 
            // tbNotes
            // 
            this.tbNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbNotes.Location = new System.Drawing.Point(134, 209);
            this.tbNotes.Name = "tbNotes";
            this.tbNotes.Size = new System.Drawing.Size(495, 21);
            this.tbNotes.TabIndex = 65;
            this.tbNotes.Tag = "Notes";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(72, 181);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 15);
            this.label3.TabIndex = 62;
            this.label3.Text = "Pounds";
            // 
            // tbLbs
            // 
            this.tbLbs.Location = new System.Drawing.Point(134, 177);
            this.tbLbs.Name = "tbLbs";
            this.tbLbs.Size = new System.Drawing.Size(93, 23);
            this.tbLbs.TabIndex = 63;
            this.tbLbs.Tag = "Pounds";
            this.tbLbs.Text = "0";
            this.tbLbs.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // cboFoodCat
            // 
            this.cboFoodCat.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboFoodCat.FormattingEnabled = true;
            this.cboFoodCat.Location = new System.Drawing.Point(133, 116);
            this.cboFoodCat.Name = "cboFoodCat";
            this.cboFoodCat.Size = new System.Drawing.Size(225, 23);
            this.cboFoodCat.TabIndex = 59;
            this.cboFoodCat.Tag = "FoodClass";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(42, 119);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 15);
            this.label1.TabIndex = 58;
            this.label1.Text = "Food Class";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(25, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 15);
            this.label2.TabIndex = 50;
            this.label2.Text = "Donation Date";
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblID.Location = new System.Drawing.Point(19, 53);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(91, 15);
            this.lblID.TabIndex = 52;
            this.lblID.Text = "Donor Id/Name";
            // 
            // lblTrxID
            // 
            this.lblTrxID.AutoSize = true;
            this.lblTrxID.Location = new System.Drawing.Point(655, 158);
            this.lblTrxID.Name = "lblTrxID";
            this.lblTrxID.Size = new System.Drawing.Size(41, 17);
            this.lblTrxID.TabIndex = 66;
            this.lblTrxID.Text = "TrxID";
            // 
            // spltcntrDailyLog
            // 
            this.spltcntrDailyLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spltcntrDailyLog.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.spltcntrDailyLog.IsSplitterFixed = true;
            this.spltcntrDailyLog.Location = new System.Drawing.Point(0, 0);
            this.spltcntrDailyLog.Name = "spltcntrDailyLog";
            this.spltcntrDailyLog.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // spltcntrDailyLog.Panel1
            // 
            this.spltcntrDailyLog.Panel1.Controls.Add(this.cboDisplayType);
            this.spltcntrDailyLog.Panel1.Controls.Add(this.btnClose);
            this.spltcntrDailyLog.Panel1.Controls.Add(this.tbTotalCount);
            this.spltcntrDailyLog.Panel1.Controls.Add(this.btnNewDonation);
            this.spltcntrDailyLog.Panel1.Controls.Add(this.tbTotalLbs);
            this.spltcntrDailyLog.Panel1.Controls.Add(this.btnDeleteTrx);
            this.spltcntrDailyLog.Panel1.Controls.Add(this.dtDateDL);
            this.spltcntrDailyLog.Panel1.Controls.Add(this.btnFirstDL);
            this.spltcntrDailyLog.Panel1.Controls.Add(this.btnNextDL);
            this.spltcntrDailyLog.Panel1.Controls.Add(this.btnLastDL);
            this.spltcntrDailyLog.Panel1.Controls.Add(this.btnPrevDL);
            this.spltcntrDailyLog.Panel1.Controls.Add(this.btnShowDnrHist);
            this.spltcntrDailyLog.Panel1.Controls.Add(this.btnEditDnrTrx);
            // 
            // spltcntrDailyLog.Panel2
            // 
            this.spltcntrDailyLog.Panel2.Controls.Add(this.lvFoodDonations);
            this.spltcntrDailyLog.Size = new System.Drawing.Size(1004, 691);
            this.spltcntrDailyLog.SplitterDistance = 125;
            this.spltcntrDailyLog.SplitterWidth = 1;
            this.spltcntrDailyLog.TabIndex = 18;
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
            this.cboDisplayType.Location = new System.Drawing.Point(59, 5);
            this.cboDisplayType.Name = "cboDisplayType";
            this.cboDisplayType.Size = new System.Drawing.Size(198, 24);
            this.cboDisplayType.TabIndex = 25;
            this.cboDisplayType.SelectedIndexChanged += new System.EventHandler(this.cboDisplayType_SelectedIndexChanged);
            // 
            // spltcntrEdit
            // 
            this.spltcntrEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spltcntrEdit.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.spltcntrEdit.IsSplitterFixed = true;
            this.spltcntrEdit.Location = new System.Drawing.Point(0, 0);
            this.spltcntrEdit.Name = "spltcntrEdit";
            this.spltcntrEdit.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // spltcntrEdit.Panel1
            // 
            this.spltcntrEdit.Panel1.BackColor = System.Drawing.Color.LightGreen;
            this.spltcntrEdit.Panel1.Controls.Add(this.label4);
            this.spltcntrEdit.Panel1.Controls.Add(this.cboDonationType);
            this.spltcntrEdit.Panel1.Controls.Add(this.label6);
            this.spltcntrEdit.Panel1.Controls.Add(this.btnBrowse);
            this.spltcntrEdit.Panel1.Controls.Add(this.tbNotes);
            this.spltcntrEdit.Panel1.Controls.Add(this.btnLogEntrySave);
            this.spltcntrEdit.Panel1.Controls.Add(this.dtDonationDate);
            this.spltcntrEdit.Panel1.Controls.Add(this.btnLogEntryCancel);
            this.spltcntrEdit.Panel1.Controls.Add(this.label3);
            this.spltcntrEdit.Panel1.Controls.Add(this.label5);
            this.spltcntrEdit.Panel1.Controls.Add(this.textBox1);
            this.spltcntrEdit.Panel1.Controls.Add(this.cboFoodCat);
            this.spltcntrEdit.Panel1.Controls.Add(this.tbLbs);
            this.spltcntrEdit.Panel1.Controls.Add(this.label1);
            this.spltcntrEdit.Panel1.Controls.Add(this.lblTrxID);
            this.spltcntrEdit.Panel1.Controls.Add(this.tbTrxID);
            this.spltcntrEdit.Panel1.Controls.Add(this.tbName);
            this.spltcntrEdit.Panel1.Controls.Add(this.label2);
            this.spltcntrEdit.Panel1.Controls.Add(this.lblID);
            this.spltcntrEdit.Panel1.Controls.Add(this.tbDonorID);
            // 
            // spltcntrEdit.Panel2
            // 
            this.spltcntrEdit.Panel2.BackColor = System.Drawing.Color.Tan;
            this.spltcntrEdit.Panel2.Controls.Add(this.label7);
            this.spltcntrEdit.Panel2.Controls.Add(this.lblDnrHist);
            this.spltcntrEdit.Panel2.Controls.Add(this.btnHideDonorHist);
            this.spltcntrEdit.Panel2.Controls.Add(this.lvDonorHistory);
            this.spltcntrEdit.Panel2.Controls.Add(this.cboDonorPeriod);
            this.spltcntrEdit.Panel2.Controls.Add(this.dtpTo);
            this.spltcntrEdit.Panel2.Controls.Add(this.lblFrom);
            this.spltcntrEdit.Panel2.Controls.Add(this.tbDonorCnt);
            this.spltcntrEdit.Panel2.Controls.Add(this.dtpFrom);
            this.spltcntrEdit.Panel2.Controls.Add(this.btnLoadCustom);
            this.spltcntrEdit.Panel2.Controls.Add(this.lblTo);
            this.spltcntrEdit.Panel2.Controls.Add(this.tbDonorLbs);
            this.spltcntrEdit.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.spltcntrEdit_Panel2_Paint);
            this.spltcntrEdit.Size = new System.Drawing.Size(1004, 691);
            this.spltcntrEdit.SplitterDistance = 240;
            this.spltcntrEdit.SplitterWidth = 1;
            this.spltcntrEdit.TabIndex = 20;
            this.spltcntrEdit.SizeChanged += new System.EventHandler(this.spltcntrEdit_SizeChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(278, 32);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(119, 18);
            this.label7.TabIndex = 27;
            this.label7.Text = "Donation History";
            // 
            // lblDnrHist
            // 
            this.lblDnrHist.AutoSize = true;
            this.lblDnrHist.BackColor = System.Drawing.Color.BurlyWood;
            this.lblDnrHist.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDnrHist.Location = new System.Drawing.Point(278, 14);
            this.lblDnrHist.Name = "lblDnrHist";
            this.lblDnrHist.Size = new System.Drawing.Size(46, 18);
            this.lblDnrHist.TabIndex = 26;
            this.lblDnrHist.Text = "label7";
            // 
            // FoodDonationsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Cornsilk;
            this.ClientSize = new System.Drawing.Size(1004, 691);
            this.Controls.Add(this.spltcntrDailyLog);
            this.Controls.Add(this.spltcntrEdit);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FoodDonationsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Food Donations Maintenance Form";
            this.spltcntrDailyLog.Panel1.ResumeLayout(false);
            this.spltcntrDailyLog.Panel1.PerformLayout();
            this.spltcntrDailyLog.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spltcntrDailyLog)).EndInit();
            this.spltcntrDailyLog.ResumeLayout(false);
            this.spltcntrEdit.Panel1.ResumeLayout(false);
            this.spltcntrEdit.Panel1.PerformLayout();
            this.spltcntrEdit.Panel2.ResumeLayout(false);
            this.spltcntrEdit.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spltcntrEdit)).EndInit();
            this.spltcntrEdit.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnNextDL;
        private System.Windows.Forms.Button btnPrevDL;
        private System.Windows.Forms.Button btnLastDL;
        private System.Windows.Forms.Button btnFirstDL;
        private System.Windows.Forms.ListView lvFoodDonations;
        private System.Windows.Forms.ColumnHeader colCntLog;
        private System.Windows.Forms.ColumnHeader colTrxIdLog;
        private System.Windows.Forms.ColumnHeader colDonorLog;
        private System.Windows.Forms.ColumnHeader colDescrLog;
        private System.Windows.Forms.ColumnHeader colLbsLog;
        private System.Windows.Forms.ColumnHeader colNotesLog;
        private System.Windows.Forms.TextBox tbTotalCount;
        private System.Windows.Forms.TextBox tbTotalLbs;
        private System.Windows.Forms.Button btnShowDnrHist;
        private System.Windows.Forms.Button btnEditDnrTrx;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.TextBox tbDonorID;
        private System.Windows.Forms.Label lblTrxID;
        private System.Windows.Forms.TextBox tbTrxID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbLbs;
        private System.Windows.Forms.ComboBox cboFoodCat;
        private System.Windows.Forms.Button btnHideDonorHist;
        private System.Windows.Forms.TextBox tbDonorLbs;
        private System.Windows.Forms.ListView lvDonorHistory;
        private System.Windows.Forms.ColumnHeader colCntDH;
        private System.Windows.Forms.ColumnHeader colIdDH;
        private System.Windows.Forms.ColumnHeader colDateDH;
        private System.Windows.Forms.ColumnHeader colCodeDH;
        private System.Windows.Forms.ColumnHeader colLbsDH;
        private System.Windows.Forms.ColumnHeader colNotesDH;
        private System.Windows.Forms.TextBox tbDonorCnt;
        private System.Windows.Forms.Button btnLogEntrySave;
        private System.Windows.Forms.Button btnLogEntryCancel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbNotes;
        private System.Windows.Forms.DateTimePicker dtDateDL;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.DateTimePicker dtDonationDate;
        private System.Windows.Forms.ComboBox cboDonationType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ColumnHeader colDonorIdLog;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnNewDonation;
        private System.Windows.Forms.ColumnHeader colTypeDH;
        private System.Windows.Forms.ColumnHeader colTypeLog;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.Button btnLoadCustom;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ColumnHeader colFdClassLog;
        private System.Windows.Forms.ColumnHeader colFdClassDH;
        private System.Windows.Forms.ComboBox cboDonorPeriod;
        private System.Windows.Forms.Button btnDeleteTrx;
        private System.Windows.Forms.SplitContainer spltcntrDailyLog;
        private System.Windows.Forms.SplitContainer spltcntrEdit;
        private System.Windows.Forms.Label lblDnrHist;
        private System.Windows.Forms.ComboBox cboDisplayType;
        private System.Windows.Forms.ColumnHeader colTrxDateLog;
        private System.Windows.Forms.Label label7;
    }
}