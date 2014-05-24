namespace ClientcardFB3
{
    partial class YearlyForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(YearlyForm));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.pbCalendar3 = new Pabo.Calendar.PBCalendar();
            this.dateItem1 = new Pabo.Calendar.DateItem();
            this.pbCalendar1 = new Pabo.Calendar.PBCalendar();
            this.pbCalendar2 = new Pabo.Calendar.PBCalendar();
            this.butPrevQuarter = new System.Windows.Forms.Button();
            this.butNextQuarter = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lvwSpclFood = new System.Windows.Forms.ListView();
            this.Descr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.key = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.rbMarkSpclFood = new System.Windows.Forms.RadioButton();
            this.rbMarkCommodity = new System.Windows.Forms.RadioButton();
            this.rbAddDaysOpen = new System.Windows.Forms.RadioButton();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.butMarkOpenDays = new System.Windows.Forms.Button();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.rbOverWrite = new System.Windows.Forms.RadioButton();
            this.rbClearBeforeWrite = new System.Windows.Forms.RadioButton();
            this.chkMarkCommodity = new System.Windows.Forms.CheckBox();
            this.dtpPeriodEnd = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.dtpPeriodStart = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cboOpenSun = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cboOpenSat = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cboOpenFri = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cboOpenThurs = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cboOpenWeds = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboOpenTues = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboOpenMon = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkNextFiscalYr = new System.Windows.Forms.CheckBox();
            this.chkCurrFiscalYr = new System.Windows.Forms.CheckBox();
            this.chkPrevFiscalYr = new System.Windows.Forms.CheckBox();
            this.SaveCal1 = new System.Windows.Forms.Button();
            this.SaveCal2 = new System.Windows.Forms.Button();
            this.SaveCal3 = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Lime;
            this.imageList1.Images.SetKeyName(0, "C2.png");
            // 
            // pbCalendar3
            // 
            this.pbCalendar3.ActiveMonth.Month = 9;
            this.pbCalendar3.ActiveMonth.Year = 2010;
            this.pbCalendar3.BorderColor = System.Drawing.Color.DimGray;
            this.pbCalendar3.Culture = new System.Globalization.CultureInfo("en-US");
            this.pbCalendar3.Dates.AddRange(new Pabo.Calendar.DateItem[] {
            this.dateItem1});
            this.pbCalendar3.Footer.BackColor1 = System.Drawing.Color.LightSteelBlue;
            this.pbCalendar3.Footer.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pbCalendar3.Footer.GradientMode = Pabo.Calendar.mcGradientMode.Horizontal;
            this.pbCalendar3.Footer.ShowToday = false;
            this.pbCalendar3.Footer.Text = "Click Mode";
            this.pbCalendar3.Footer.TextColor = System.Drawing.Color.Indigo;
            this.pbCalendar3.Header.Font = new System.Drawing.Font("Verdana", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pbCalendar3.Header.MonthSelectors = false;
            this.pbCalendar3.Header.TextColor = System.Drawing.Color.White;
            this.pbCalendar3.ImageList = this.imageList1;
            this.pbCalendar3.Location = new System.Drawing.Point(528, 291);
            this.pbCalendar3.Margin = new System.Windows.Forms.Padding(2);
            this.pbCalendar3.MaxDate = new System.DateTime(2011, 6, 1, 0, 0, 0, 0);
            this.pbCalendar3.MinDate = new System.DateTime(2010, 7, 1, 0, 0, 0, 0);
            this.pbCalendar3.Month.BackgroundImage = null;
            this.pbCalendar3.Month.BorderStyles.Focus = System.Windows.Forms.ButtonBorderStyle.Dotted;
            this.pbCalendar3.Month.BorderStyles.Normal = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.pbCalendar3.Month.BorderStyles.Selected = System.Windows.Forms.ButtonBorderStyle.None;
            this.pbCalendar3.Month.Colors.Days.BackColor1 = System.Drawing.Color.LemonChiffon;
            this.pbCalendar3.Month.Colors.Days.Border = System.Drawing.Color.DarkGray;
            this.pbCalendar3.Month.Colors.Focus.BackColor = System.Drawing.Color.Gold;
            this.pbCalendar3.Month.Colors.Focus.Border = System.Drawing.Color.Gold;
            this.pbCalendar3.Month.Colors.Focus.Date = System.Drawing.Color.Red;
            this.pbCalendar3.Month.Colors.Focus.Text = System.Drawing.Color.Red;
            this.pbCalendar3.Month.Colors.Weekend.Date = System.Drawing.Color.DimGray;
            this.pbCalendar3.Month.DateAlign = Pabo.Calendar.mcItemAlign.TopRight;
            this.pbCalendar3.Month.DateFont = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pbCalendar3.Month.TextFont = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pbCalendar3.Name = "pbCalendar3";
            this.pbCalendar3.SelectionMode = Pabo.Calendar.mcSelectionMode.None;
            this.pbCalendar3.SelectTrailingDates = false;
            this.pbCalendar3.ShowTrailingDates = false;
            this.pbCalendar3.Size = new System.Drawing.Size(251, 253);
            this.pbCalendar3.TabIndex = 2;
            this.pbCalendar3.TodayColor = System.Drawing.Color.DimGray;
            this.pbCalendar3.Weekdays.BackColor1 = System.Drawing.Color.Lavender;
            this.pbCalendar3.Weekdays.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pbCalendar3.Weekdays.TextColor = System.Drawing.Color.Black;
            this.pbCalendar3.Weeknumbers.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.pbCalendar3.DayClick += new Pabo.Calendar.DayClickEventHandler(this.pbCalendar_DayClick);
            this.pbCalendar3.DayDoubleClick += new Pabo.Calendar.DayClickEventHandler(this.pbCalendar_DayDoubleClick);
            // 
            // dateItem1
            // 
            this.dateItem1.BackColor1 = System.Drawing.Color.PaleGreen;
            this.dateItem1.BackColor2 = System.Drawing.Color.White;
            this.dateItem1.BackgroundImage = null;
            this.dateItem1.BoldedDate = true;
            this.dateItem1.Date = new System.DateTime(2010, 10, 25, 0, 0, 0, 0);
            this.dateItem1.DateColor = System.Drawing.Color.Empty;
            this.dateItem1.Enabled = true;
            this.dateItem1.GradientMode = Pabo.Calendar.mcGradientMode.None;
            this.dateItem1.Image = ((System.Drawing.Image)(resources.GetObject("dateItem1.Image")));
            this.dateItem1.ImageListIndex = 0;
            this.dateItem1.Index = 0;
            this.dateItem1.Pattern = Pabo.Calendar.mcDayInfoRecurrence.None;
            this.dateItem1.Range = new System.DateTime(2010, 10, 25, 0, 0, 0, 0);
            this.dateItem1.Tag = null;
            this.dateItem1.Text = "";
            this.dateItem1.TextColor = System.Drawing.Color.Red;
            this.dateItem1.Weekend = false;
            // 
            // pbCalendar1
            // 
            this.pbCalendar1.ActiveMonth.Month = 7;
            this.pbCalendar1.ActiveMonth.Year = 2010;
            this.pbCalendar1.BorderColor = System.Drawing.Color.DimGray;
            this.pbCalendar1.Culture = new System.Globalization.CultureInfo("en-US");
            this.pbCalendar1.Dates.AddRange(new Pabo.Calendar.DateItem[] {
            this.dateItem1});
            this.pbCalendar1.Footer.BackColor1 = System.Drawing.Color.LightSteelBlue;
            this.pbCalendar1.Footer.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pbCalendar1.Footer.GradientMode = Pabo.Calendar.mcGradientMode.Horizontal;
            this.pbCalendar1.Footer.ShowToday = false;
            this.pbCalendar1.Footer.Text = "Click Mode";
            this.pbCalendar1.Footer.TextColor = System.Drawing.Color.Indigo;
            this.pbCalendar1.Header.Font = new System.Drawing.Font("Verdana", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pbCalendar1.Header.MonthSelectors = false;
            this.pbCalendar1.Header.TextColor = System.Drawing.Color.White;
            this.pbCalendar1.ImageList = this.imageList1;
            this.pbCalendar1.Location = new System.Drawing.Point(2, 292);
            this.pbCalendar1.Margin = new System.Windows.Forms.Padding(2);
            this.pbCalendar1.MaxDate = new System.DateTime(2011, 6, 1, 0, 0, 0, 0);
            this.pbCalendar1.MinDate = new System.DateTime(2010, 7, 1, 0, 0, 0, 0);
            this.pbCalendar1.Month.BackgroundImage = null;
            this.pbCalendar1.Month.BorderStyles.Focus = System.Windows.Forms.ButtonBorderStyle.Dotted;
            this.pbCalendar1.Month.BorderStyles.Normal = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.pbCalendar1.Month.BorderStyles.Selected = System.Windows.Forms.ButtonBorderStyle.None;
            this.pbCalendar1.Month.Colors.Days.BackColor1 = System.Drawing.Color.LemonChiffon;
            this.pbCalendar1.Month.Colors.Days.Border = System.Drawing.Color.DarkGray;
            this.pbCalendar1.Month.Colors.Focus.BackColor = System.Drawing.Color.Gold;
            this.pbCalendar1.Month.Colors.Focus.Border = System.Drawing.Color.Gold;
            this.pbCalendar1.Month.Colors.Focus.Date = System.Drawing.Color.Red;
            this.pbCalendar1.Month.Colors.Focus.Text = System.Drawing.Color.Red;
            this.pbCalendar1.Month.Colors.Selected.BackColor = System.Drawing.Color.PaleGreen;
            this.pbCalendar1.Month.Colors.Weekend.Date = System.Drawing.Color.DimGray;
            this.pbCalendar1.Month.DateAlign = Pabo.Calendar.mcItemAlign.TopRight;
            this.pbCalendar1.Month.DateFont = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pbCalendar1.Month.TextFont = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pbCalendar1.Name = "pbCalendar1";
            this.pbCalendar1.SelectButton = System.Windows.Forms.MouseButtons.None;
            this.pbCalendar1.SelectionMode = Pabo.Calendar.mcSelectionMode.None;
            this.pbCalendar1.SelectTrailingDates = false;
            this.pbCalendar1.ShowTrailingDates = false;
            this.pbCalendar1.Size = new System.Drawing.Size(251, 252);
            this.pbCalendar1.TabIndex = 0;
            this.pbCalendar1.TodayColor = System.Drawing.Color.DimGray;
            this.pbCalendar1.Weekdays.BackColor1 = System.Drawing.Color.Lavender;
            this.pbCalendar1.Weekdays.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pbCalendar1.Weekdays.TextColor = System.Drawing.Color.Black;
            this.pbCalendar1.Weeknumbers.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.pbCalendar1.DayClick += new Pabo.Calendar.DayClickEventHandler(this.pbCalendar_DayClick);
            this.pbCalendar1.DayDoubleClick += new Pabo.Calendar.DayClickEventHandler(this.pbCalendar_DayDoubleClick);
            // 
            // pbCalendar2
            // 
            this.pbCalendar2.ActiveMonth.Month = 8;
            this.pbCalendar2.ActiveMonth.Year = 2010;
            this.pbCalendar2.BorderColor = System.Drawing.Color.DimGray;
            this.pbCalendar2.Culture = new System.Globalization.CultureInfo("en-US");
            this.pbCalendar2.Dates.AddRange(new Pabo.Calendar.DateItem[] {
            this.dateItem1});
            this.pbCalendar2.Footer.BackColor1 = System.Drawing.Color.LightSteelBlue;
            this.pbCalendar2.Footer.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pbCalendar2.Footer.GradientMode = Pabo.Calendar.mcGradientMode.Horizontal;
            this.pbCalendar2.Footer.ShowToday = false;
            this.pbCalendar2.Footer.Text = "Click Mode";
            this.pbCalendar2.Footer.TextColor = System.Drawing.Color.Indigo;
            this.pbCalendar2.Header.Font = new System.Drawing.Font("Verdana", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pbCalendar2.Header.MonthSelectors = false;
            this.pbCalendar2.Header.TextColor = System.Drawing.Color.White;
            this.pbCalendar2.ImageList = this.imageList1;
            this.pbCalendar2.Location = new System.Drawing.Point(264, 292);
            this.pbCalendar2.Margin = new System.Windows.Forms.Padding(2);
            this.pbCalendar2.MaxDate = new System.DateTime(2011, 6, 1, 0, 0, 0, 0);
            this.pbCalendar2.MinDate = new System.DateTime(2010, 7, 1, 0, 0, 0, 0);
            this.pbCalendar2.Month.BackgroundImage = null;
            this.pbCalendar2.Month.BorderStyles.Focus = System.Windows.Forms.ButtonBorderStyle.Dotted;
            this.pbCalendar2.Month.BorderStyles.Normal = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.pbCalendar2.Month.BorderStyles.Selected = System.Windows.Forms.ButtonBorderStyle.None;
            this.pbCalendar2.Month.Colors.Days.BackColor1 = System.Drawing.Color.LemonChiffon;
            this.pbCalendar2.Month.Colors.Days.Border = System.Drawing.Color.DarkGray;
            this.pbCalendar2.Month.Colors.Focus.BackColor = System.Drawing.Color.Gold;
            this.pbCalendar2.Month.Colors.Focus.Border = System.Drawing.Color.Gold;
            this.pbCalendar2.Month.Colors.Focus.Date = System.Drawing.Color.Red;
            this.pbCalendar2.Month.Colors.Focus.Text = System.Drawing.Color.Red;
            this.pbCalendar2.Month.Colors.Selected.BackColor = System.Drawing.Color.PaleGreen;
            this.pbCalendar2.Month.Colors.Weekend.Date = System.Drawing.Color.DimGray;
            this.pbCalendar2.Month.DateAlign = Pabo.Calendar.mcItemAlign.TopRight;
            this.pbCalendar2.Month.DateFont = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pbCalendar2.Month.TextFont = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pbCalendar2.Name = "pbCalendar2";
            this.pbCalendar2.SelectButton = System.Windows.Forms.MouseButtons.None;
            this.pbCalendar2.SelectionMode = Pabo.Calendar.mcSelectionMode.None;
            this.pbCalendar2.SelectTrailingDates = false;
            this.pbCalendar2.ShowTrailingDates = false;
            this.pbCalendar2.Size = new System.Drawing.Size(251, 252);
            this.pbCalendar2.TabIndex = 7;
            this.pbCalendar2.TodayColor = System.Drawing.Color.DimGray;
            this.pbCalendar2.Weekdays.BackColor1 = System.Drawing.Color.Lavender;
            this.pbCalendar2.Weekdays.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pbCalendar2.Weekdays.TextColor = System.Drawing.Color.Black;
            this.pbCalendar2.Weeknumbers.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.pbCalendar2.DayClick += new Pabo.Calendar.DayClickEventHandler(this.pbCalendar_DayClick);
            this.pbCalendar2.DayDoubleClick += new Pabo.Calendar.DayClickEventHandler(this.pbCalendar_DayDoubleClick);
            // 
            // butPrevQuarter
            // 
            this.butPrevQuarter.Location = new System.Drawing.Point(26, 251);
            this.butPrevQuarter.Margin = new System.Windows.Forms.Padding(2);
            this.butPrevQuarter.Name = "butPrevQuarter";
            this.butPrevQuarter.Size = new System.Drawing.Size(90, 37);
            this.butPrevQuarter.TabIndex = 29;
            this.butPrevQuarter.Text = "Previous Period";
            this.butPrevQuarter.UseVisualStyleBackColor = true;
            this.butPrevQuarter.Click += new System.EventHandler(this.butPrevQuarter_Click);
            // 
            // butNextQuarter
            // 
            this.butNextQuarter.Location = new System.Drawing.Point(120, 251);
            this.butNextQuarter.Margin = new System.Windows.Forms.Padding(2);
            this.butNextQuarter.Name = "butNextQuarter";
            this.butNextQuarter.Size = new System.Drawing.Size(90, 37);
            this.butNextQuarter.TabIndex = 32;
            this.butNextQuarter.Text = "Next Period";
            this.butNextQuarter.UseVisualStyleBackColor = true;
            this.butNextQuarter.Click += new System.EventHandler(this.butNextQuarter_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.ItemSize = new System.Drawing.Size(200, 26);
            this.tabControl1.Location = new System.Drawing.Point(2, 6);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(774, 234);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 33;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.LightYellow;
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Location = new System.Drawing.Point(4, 30);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage1.Size = new System.Drawing.Size(766, 200);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Single Day";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.lvwSpclFood);
            this.groupBox2.Controls.Add(this.rbMarkSpclFood);
            this.groupBox2.Controls.Add(this.rbMarkCommodity);
            this.groupBox2.Controls.Add(this.rbAddDaysOpen);
            this.groupBox2.Location = new System.Drawing.Point(21, 4);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(403, 197);
            this.groupBox2.TabIndex = 32;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Click Mode";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(139, 31);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(255, 14);
            this.label1.TabIndex = 33;
            this.label1.Text = "Double Click will unmark an open day";
            // 
            // lvwSpclFood
            // 
            this.lvwSpclFood.CheckBoxes = true;
            this.lvwSpclFood.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Descr,
            this.key});
            this.lvwSpclFood.GridLines = true;
            this.lvwSpclFood.Location = new System.Drawing.Point(28, 96);
            this.lvwSpclFood.Margin = new System.Windows.Forms.Padding(2);
            this.lvwSpclFood.Name = "lvwSpclFood";
            this.lvwSpclFood.Size = new System.Drawing.Size(207, 94);
            this.lvwSpclFood.TabIndex = 35;
            this.lvwSpclFood.UseCompatibleStateImageBehavior = false;
            this.lvwSpclFood.View = System.Windows.Forms.View.Details;
            this.lvwSpclFood.Visible = false;
            // 
            // Descr
            // 
            this.Descr.Text = "Special Food Item";
            this.Descr.Width = 250;
            // 
            // key
            // 
            this.key.Text = "Key";
            this.key.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.key.Width = 0;
            // 
            // rbMarkSpclFood
            // 
            this.rbMarkSpclFood.AutoSize = true;
            this.rbMarkSpclFood.Location = new System.Drawing.Point(11, 73);
            this.rbMarkSpclFood.Margin = new System.Windows.Forms.Padding(2);
            this.rbMarkSpclFood.Name = "rbMarkSpclFood";
            this.rbMarkSpclFood.Size = new System.Drawing.Size(178, 18);
            this.rbMarkSpclFood.TabIndex = 9;
            this.rbMarkSpclFood.TabStop = true;
            this.rbMarkSpclFood.Text = "Toggle Special Food Day";
            this.rbMarkSpclFood.UseVisualStyleBackColor = true;
            this.rbMarkSpclFood.CheckedChanged += new System.EventHandler(this.rbMarkSpclFood_CheckedChanged);
            // 
            // rbMarkCommodity
            // 
            this.rbMarkCommodity.AutoSize = true;
            this.rbMarkCommodity.ImageIndex = 0;
            this.rbMarkCommodity.ImageList = this.imageList1;
            this.rbMarkCommodity.Location = new System.Drawing.Point(11, 51);
            this.rbMarkCommodity.Margin = new System.Windows.Forms.Padding(2);
            this.rbMarkCommodity.Name = "rbMarkCommodity";
            this.rbMarkCommodity.Size = new System.Drawing.Size(189, 20);
            this.rbMarkCommodity.TabIndex = 8;
            this.rbMarkCommodity.Text = "Toggle Commodity Day";
            this.rbMarkCommodity.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.rbMarkCommodity.UseVisualStyleBackColor = true;
            this.rbMarkCommodity.CheckedChanged += new System.EventHandler(this.rbMarkCommodity_CheckedChanged);
            // 
            // rbAddDaysOpen
            // 
            this.rbAddDaysOpen.AutoSize = true;
            this.rbAddDaysOpen.Checked = true;
            this.rbAddDaysOpen.Location = new System.Drawing.Point(11, 29);
            this.rbAddDaysOpen.Margin = new System.Windows.Forms.Padding(2);
            this.rbAddDaysOpen.Name = "rbAddDaysOpen";
            this.rbAddDaysOpen.Size = new System.Drawing.Size(122, 18);
            this.rbAddDaysOpen.TabIndex = 7;
            this.rbAddDaysOpen.TabStop = true;
            this.rbAddDaysOpen.Text = "Add Days Open";
            this.rbAddDaysOpen.UseVisualStyleBackColor = true;
            this.rbAddDaysOpen.CheckedChanged += new System.EventHandler(this.rbAddDaysOpen_CheckedChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.LightYellow;
            this.tabPage2.Controls.Add(this.butMarkOpenDays);
            this.tabPage2.Controls.Add(this.radioButton1);
            this.tabPage2.Controls.Add(this.rbOverWrite);
            this.tabPage2.Controls.Add(this.rbClearBeforeWrite);
            this.tabPage2.Controls.Add(this.chkMarkCommodity);
            this.tabPage2.Controls.Add(this.dtpPeriodEnd);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.dtpPeriodStart);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.cboOpenSun);
            this.tabPage2.Controls.Add(this.cboOpenSat);
            this.tabPage2.Controls.Add(this.cboOpenFri);
            this.tabPage2.Controls.Add(this.cboOpenThurs);
            this.tabPage2.Controls.Add(this.cboOpenWeds);
            this.tabPage2.Controls.Add(this.cboOpenTues);
            this.tabPage2.Controls.Add(this.cboOpenMon);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage2.Location = new System.Drawing.Point(4, 30);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage2.Size = new System.Drawing.Size(766, 200);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Multiple Days by Period";
            // 
            // butMarkOpenDays
            // 
            this.butMarkOpenDays.Location = new System.Drawing.Point(540, 65);
            this.butMarkOpenDays.Margin = new System.Windows.Forms.Padding(2);
            this.butMarkOpenDays.Name = "butMarkOpenDays";
            this.butMarkOpenDays.Size = new System.Drawing.Size(129, 75);
            this.butMarkOpenDays.TabIndex = 65;
            this.butMarkOpenDays.Text = "Mark Open Days for This Period";
            this.butMarkOpenDays.UseVisualStyleBackColor = true;
            this.butMarkOpenDays.Click += new System.EventHandler(this.butMarkOpenDays_Click);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(319, 120);
            this.radioButton1.Margin = new System.Windows.Forms.Padding(2);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(172, 18);
            this.radioButton1.TabIndex = 64;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Append New Days Only";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // rbOverWrite
            // 
            this.rbOverWrite.AutoSize = true;
            this.rbOverWrite.Location = new System.Drawing.Point(319, 98);
            this.rbOverWrite.Margin = new System.Windows.Forms.Padding(2);
            this.rbOverWrite.Name = "rbOverWrite";
            this.rbOverWrite.Size = new System.Drawing.Size(188, 18);
            this.rbOverWrite.TabIndex = 63;
            this.rbOverWrite.Text = "Overwrite Matching Dates";
            this.rbOverWrite.UseVisualStyleBackColor = true;
            // 
            // rbClearBeforeWrite
            // 
            this.rbClearBeforeWrite.AutoSize = true;
            this.rbClearBeforeWrite.Font = new System.Drawing.Font("Arial", 8.25F);
            this.rbClearBeforeWrite.Location = new System.Drawing.Point(319, 75);
            this.rbClearBeforeWrite.Margin = new System.Windows.Forms.Padding(2);
            this.rbClearBeforeWrite.Name = "rbClearBeforeWrite";
            this.rbClearBeforeWrite.Size = new System.Drawing.Size(115, 18);
            this.rbClearBeforeWrite.TabIndex = 62;
            this.rbClearBeforeWrite.Text = "Clear Existing Data";
            this.rbClearBeforeWrite.UseVisualStyleBackColor = true;
            // 
            // chkMarkCommodity
            // 
            this.chkMarkCommodity.Location = new System.Drawing.Point(300, 143);
            this.chkMarkCommodity.Margin = new System.Windows.Forms.Padding(2);
            this.chkMarkCommodity.Name = "chkMarkCommodity";
            this.chkMarkCommodity.Size = new System.Drawing.Size(237, 41);
            this.chkMarkCommodity.TabIndex = 61;
            this.chkMarkCommodity.Text = "Mark Every Day as a Commodity Day";
            this.chkMarkCommodity.UseVisualStyleBackColor = true;
            // 
            // dtpPeriodEnd
            // 
            this.dtpPeriodEnd.CalendarFont = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpPeriodEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpPeriodEnd.Location = new System.Drawing.Point(374, 40);
            this.dtpPeriodEnd.Margin = new System.Windows.Forms.Padding(2);
            this.dtpPeriodEnd.Name = "dtpPeriodEnd";
            this.dtpPeriodEnd.Size = new System.Drawing.Size(108, 22);
            this.dtpPeriodEnd.TabIndex = 60;
            this.dtpPeriodEnd.Value = new System.DateTime(2011, 6, 30, 0, 0, 0, 0);
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(298, 40);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(72, 21);
            this.label10.TabIndex = 59;
            this.label10.Text = "Period End";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpPeriodStart
            // 
            this.dtpPeriodStart.CalendarFont = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpPeriodStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpPeriodStart.Location = new System.Drawing.Point(374, 10);
            this.dtpPeriodStart.Margin = new System.Windows.Forms.Padding(2);
            this.dtpPeriodStart.Name = "dtpPeriodStart";
            this.dtpPeriodStart.Size = new System.Drawing.Size(108, 22);
            this.dtpPeriodStart.TabIndex = 58;
            this.dtpPeriodStart.Value = new System.DateTime(2010, 7, 1, 0, 0, 0, 0);
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(298, 10);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(72, 21);
            this.label9.TabIndex = 57;
            this.label9.Text = "Period Start";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(30, 16);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 14);
            this.label8.TabIndex = 56;
            this.label8.Text = "Sunday:";
            // 
            // cboOpenSun
            // 
            this.cboOpenSun.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboOpenSun.FormattingEnabled = true;
            this.cboOpenSun.Location = new System.Drawing.Point(96, 15);
            this.cboOpenSun.Margin = new System.Windows.Forms.Padding(2);
            this.cboOpenSun.Name = "cboOpenSun";
            this.cboOpenSun.Size = new System.Drawing.Size(187, 21);
            this.cboOpenSun.TabIndex = 55;
            this.cboOpenSun.Tag = "Sunday";
            this.cboOpenSun.SelectionChangeCommitted += new System.EventHandler(this.cboOpenDay_SelectionChangeCommitted);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 175);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 14);
            this.label6.TabIndex = 54;
            this.label6.Text = "Saturday:";
            // 
            // cboOpenSat
            // 
            this.cboOpenSat.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboOpenSat.FormattingEnabled = true;
            this.cboOpenSat.Location = new System.Drawing.Point(96, 175);
            this.cboOpenSat.Margin = new System.Windows.Forms.Padding(2);
            this.cboOpenSat.Name = "cboOpenSat";
            this.cboOpenSat.Size = new System.Drawing.Size(187, 21);
            this.cboOpenSat.TabIndex = 53;
            this.cboOpenSat.Tag = "Saturday";
            this.cboOpenSat.SelectionChangeCommitted += new System.EventHandler(this.cboOpenDay_SelectionChangeCommitted);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(39, 143);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 14);
            this.label7.TabIndex = 52;
            this.label7.Text = "Friday:";
            // 
            // cboOpenFri
            // 
            this.cboOpenFri.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboOpenFri.FormattingEnabled = true;
            this.cboOpenFri.Location = new System.Drawing.Point(96, 142);
            this.cboOpenFri.Margin = new System.Windows.Forms.Padding(2);
            this.cboOpenFri.Name = "cboOpenFri";
            this.cboOpenFri.Size = new System.Drawing.Size(187, 21);
            this.cboOpenFri.TabIndex = 51;
            this.cboOpenFri.Tag = "Friday";
            this.cboOpenFri.SelectionChangeCommitted += new System.EventHandler(this.cboOpenDay_SelectionChangeCommitted);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 118);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 14);
            this.label4.TabIndex = 50;
            this.label4.Text = "Thursday:";
            // 
            // cboOpenThurs
            // 
            this.cboOpenThurs.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboOpenThurs.FormattingEnabled = true;
            this.cboOpenThurs.Location = new System.Drawing.Point(96, 118);
            this.cboOpenThurs.Margin = new System.Windows.Forms.Padding(2);
            this.cboOpenThurs.Name = "cboOpenThurs";
            this.cboOpenThurs.Size = new System.Drawing.Size(187, 21);
            this.cboOpenThurs.TabIndex = 49;
            this.cboOpenThurs.Tag = "Thursday";
            this.cboOpenThurs.SelectionChangeCommitted += new System.EventHandler(this.cboOpenDay_SelectionChangeCommitted);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(2, 95);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 14);
            this.label5.TabIndex = 48;
            this.label5.Text = "Wednesday:";
            // 
            // cboOpenWeds
            // 
            this.cboOpenWeds.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboOpenWeds.FormattingEnabled = true;
            this.cboOpenWeds.Location = new System.Drawing.Point(96, 94);
            this.cboOpenWeds.Margin = new System.Windows.Forms.Padding(2);
            this.cboOpenWeds.Name = "cboOpenWeds";
            this.cboOpenWeds.Size = new System.Drawing.Size(187, 21);
            this.cboOpenWeds.TabIndex = 47;
            this.cboOpenWeds.Tag = "Wednesday";
            this.cboOpenWeds.SelectionChangeCommitted += new System.EventHandler(this.cboOpenDay_SelectionChangeCommitted);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 71);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 14);
            this.label3.TabIndex = 46;
            this.label3.Text = "Tuesday:";
            // 
            // cboOpenTues
            // 
            this.cboOpenTues.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboOpenTues.FormattingEnabled = true;
            this.cboOpenTues.Location = new System.Drawing.Point(96, 69);
            this.cboOpenTues.Margin = new System.Windows.Forms.Padding(2);
            this.cboOpenTues.Name = "cboOpenTues";
            this.cboOpenTues.Size = new System.Drawing.Size(187, 21);
            this.cboOpenTues.TabIndex = 45;
            this.cboOpenTues.Tag = "Tuesday";
            this.cboOpenTues.SelectionChangeCommitted += new System.EventHandler(this.cboOpenDay_SelectionChangeCommitted);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 46);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 14);
            this.label2.TabIndex = 44;
            this.label2.Text = "Monday:";
            // 
            // cboOpenMon
            // 
            this.cboOpenMon.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboOpenMon.FormattingEnabled = true;
            this.cboOpenMon.Location = new System.Drawing.Point(96, 43);
            this.cboOpenMon.Margin = new System.Windows.Forms.Padding(2);
            this.cboOpenMon.Name = "cboOpenMon";
            this.cboOpenMon.Size = new System.Drawing.Size(187, 21);
            this.cboOpenMon.TabIndex = 43;
            this.cboOpenMon.Tag = "Monday";
            this.cboOpenMon.SelectionChangeCommitted += new System.EventHandler(this.cboOpenDay_SelectionChangeCommitted);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkNextFiscalYr);
            this.groupBox1.Controls.Add(this.chkCurrFiscalYr);
            this.groupBox1.Controls.Add(this.chkPrevFiscalYr);
            this.groupBox1.Location = new System.Drawing.Point(422, 246);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(352, 42);
            this.groupBox1.TabIndex = 42;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Calendar Display Range";
            // 
            // chkNextFiscalYr
            // 
            this.chkNextFiscalYr.AutoSize = true;
            this.chkNextFiscalYr.Location = new System.Drawing.Point(247, 17);
            this.chkNextFiscalYr.Margin = new System.Windows.Forms.Padding(2);
            this.chkNextFiscalYr.Name = "chkNextFiscalYr";
            this.chkNextFiscalYr.Size = new System.Drawing.Size(103, 17);
            this.chkNextFiscalYr.TabIndex = 43;
            this.chkNextFiscalYr.Text = "Next Fiscal Year";
            this.chkNextFiscalYr.UseVisualStyleBackColor = true;
            this.chkNextFiscalYr.CheckedChanged += new System.EventHandler(this.chkFiscalYr_CheckedChanged);
            // 
            // chkCurrFiscalYr
            // 
            this.chkCurrFiscalYr.AutoSize = true;
            this.chkCurrFiscalYr.Checked = true;
            this.chkCurrFiscalYr.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCurrFiscalYr.Enabled = false;
            this.chkCurrFiscalYr.Location = new System.Drawing.Point(133, 17);
            this.chkCurrFiscalYr.Margin = new System.Windows.Forms.Padding(2);
            this.chkCurrFiscalYr.Name = "chkCurrFiscalYr";
            this.chkCurrFiscalYr.Size = new System.Drawing.Size(115, 17);
            this.chkCurrFiscalYr.TabIndex = 42;
            this.chkCurrFiscalYr.Text = "Current Fiscal Year";
            this.chkCurrFiscalYr.ThreeState = true;
            this.chkCurrFiscalYr.UseVisualStyleBackColor = true;
            // 
            // chkPrevFiscalYr
            // 
            this.chkPrevFiscalYr.AutoSize = true;
            this.chkPrevFiscalYr.Location = new System.Drawing.Point(9, 17);
            this.chkPrevFiscalYr.Margin = new System.Windows.Forms.Padding(2);
            this.chkPrevFiscalYr.Name = "chkPrevFiscalYr";
            this.chkPrevFiscalYr.Size = new System.Drawing.Size(122, 17);
            this.chkPrevFiscalYr.TabIndex = 41;
            this.chkPrevFiscalYr.Text = "Previous Fiscal Year";
            this.chkPrevFiscalYr.UseVisualStyleBackColor = true;
            this.chkPrevFiscalYr.CheckedChanged += new System.EventHandler(this.chkFiscalYr_CheckedChanged);
            // 
            // SaveCal1
            // 
            this.SaveCal1.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveCal1.Location = new System.Drawing.Point(227, 294);
            this.SaveCal1.Margin = new System.Windows.Forms.Padding(2);
            this.SaveCal1.Name = "SaveCal1";
            this.SaveCal1.Size = new System.Drawing.Size(21, 19);
            this.SaveCal1.TabIndex = 45;
            this.SaveCal1.Text = "Pr";
            this.SaveCal1.UseVisualStyleBackColor = true;
            this.SaveCal1.Click += new System.EventHandler(this.SaveCal1_Click);
            // 
            // SaveCal2
            // 
            this.SaveCal2.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveCal2.Location = new System.Drawing.Point(490, 295);
            this.SaveCal2.Margin = new System.Windows.Forms.Padding(2);
            this.SaveCal2.Name = "SaveCal2";
            this.SaveCal2.Size = new System.Drawing.Size(21, 19);
            this.SaveCal2.TabIndex = 46;
            this.SaveCal2.Text = "Pr";
            this.SaveCal2.UseVisualStyleBackColor = true;
            this.SaveCal2.Click += new System.EventHandler(this.SaveCal2_Click);
            // 
            // SaveCal3
            // 
            this.SaveCal3.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveCal3.Location = new System.Drawing.Point(752, 293);
            this.SaveCal3.Margin = new System.Windows.Forms.Padding(2);
            this.SaveCal3.Name = "SaveCal3";
            this.SaveCal3.Size = new System.Drawing.Size(21, 19);
            this.SaveCal3.TabIndex = 47;
            this.SaveCal3.Text = "Pr";
            this.SaveCal3.UseVisualStyleBackColor = true;
            this.SaveCal3.Click += new System.EventHandler(this.SaveCal3_Click);
            // 
            // YearlyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Cornsilk;
            this.ClientSize = new System.Drawing.Size(784, 550);
            this.Controls.Add(this.SaveCal3);
            this.Controls.Add(this.SaveCal2);
            this.Controls.Add(this.SaveCal1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pbCalendar1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.butNextQuarter);
            this.Controls.Add(this.butPrevQuarter);
            this.Controls.Add(this.pbCalendar2);
            this.Controls.Add(this.pbCalendar3);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "YearlyForm";
            this.Text = "Yearly Calendar";
            this.Load += new System.EventHandler(this.YearlyForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Pabo.Calendar.PBCalendar pbCalendar1;
        private Pabo.Calendar.DateItem dateItem1;
        private System.Windows.Forms.ImageList imageList1;
        private Pabo.Calendar.PBCalendar pbCalendar3;
        private Pabo.Calendar.PBCalendar pbCalendar2;
        private System.Windows.Forms.Button butPrevQuarter;
        private System.Windows.Forms.Button butNextQuarter;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbMarkSpclFood;
        private System.Windows.Forms.RadioButton rbMarkCommodity;
        private System.Windows.Forms.RadioButton rbAddDaysOpen;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cboOpenSun;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cboOpenSat;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cboOpenFri;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboOpenThurs;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboOpenWeds;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboOpenTues;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboOpenMon;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpPeriodStart;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker dtpPeriodEnd;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.RadioButton rbClearBeforeWrite;
        private System.Windows.Forms.CheckBox chkMarkCommodity;
        private System.Windows.Forms.RadioButton rbOverWrite;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Button butMarkOpenDays;
        private System.Windows.Forms.ListView lvwSpclFood;
        private System.Windows.Forms.ColumnHeader Descr;
        private System.Windows.Forms.ColumnHeader key;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkNextFiscalYr;
        private System.Windows.Forms.CheckBox chkCurrFiscalYr;
        private System.Windows.Forms.CheckBox chkPrevFiscalYr;
        private System.Windows.Forms.Button SaveCal1;
        private System.Windows.Forms.Button SaveCal2;
        private System.Windows.Forms.Button SaveCal3;

    }
}

