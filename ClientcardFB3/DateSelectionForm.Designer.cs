namespace ClientcardFB3
{
    partial class DateSelectionForm
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
                if (clsDaysOpen != null)
                {
                    clsDaysOpen.Dispose();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DateSelectionForm));
            this.pbCalendar = new Pabo.Calendar.PBCalendar();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.lblDateSelected = new System.Windows.Forms.Label();
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grpbxPeriod = new System.Windows.Forms.GroupBox();
            this.rdoCurrFiscalYr = new System.Windows.Forms.RadioButton();
            this.rdoPrevFiscalYr = new System.Windows.Forms.RadioButton();
            this.grpbxPeriod.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbCalendar
            // 
            this.pbCalendar.ActiveMonth.Month = 10;
            this.pbCalendar.ActiveMonth.Year = 2010;
            this.pbCalendar.BorderColor = System.Drawing.Color.DimGray;
            this.pbCalendar.Culture = new System.Globalization.CultureInfo("en-US");
            this.pbCalendar.Footer.BackColor1 = System.Drawing.Color.LightSteelBlue;
            this.pbCalendar.Footer.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pbCalendar.Footer.GradientMode = Pabo.Calendar.mcGradientMode.Horizontal;
            this.pbCalendar.Footer.ShowToday = false;
            this.pbCalendar.Footer.TextColor = System.Drawing.Color.Indigo;
            this.pbCalendar.Header.Font = new System.Drawing.Font("Verdana", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pbCalendar.Header.MonthContextMenu = false;
            this.pbCalendar.Header.TextColor = System.Drawing.Color.White;
            this.pbCalendar.ImageList = this.imageList1;
            this.pbCalendar.Location = new System.Drawing.Point(8, 94);
            this.pbCalendar.Margin = new System.Windows.Forms.Padding(2);
            this.pbCalendar.MaxDate = new System.DateTime(2011, 6, 1, 0, 0, 0, 0);
            this.pbCalendar.MinDate = new System.DateTime(2010, 7, 1, 0, 0, 0, 0);
            this.pbCalendar.Month.BackgroundImage = null;
            this.pbCalendar.Month.BorderStyles.Focus = System.Windows.Forms.ButtonBorderStyle.Dotted;
            this.pbCalendar.Month.BorderStyles.Normal = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.pbCalendar.Month.BorderStyles.Selected = System.Windows.Forms.ButtonBorderStyle.None;
            this.pbCalendar.Month.Colors.Days.BackColor1 = System.Drawing.Color.LemonChiffon;
            this.pbCalendar.Month.Colors.Days.Border = System.Drawing.Color.DarkGray;
            this.pbCalendar.Month.Colors.Focus.BackColor = System.Drawing.Color.Gold;
            this.pbCalendar.Month.Colors.Focus.Border = System.Drawing.Color.Gold;
            this.pbCalendar.Month.Colors.Focus.Date = System.Drawing.Color.Red;
            this.pbCalendar.Month.Colors.Focus.Text = System.Drawing.Color.Red;
            this.pbCalendar.Month.Colors.Weekend.Date = System.Drawing.Color.DimGray;
            this.pbCalendar.Month.DateAlign = Pabo.Calendar.mcItemAlign.TopRight;
            this.pbCalendar.Month.DateFont = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pbCalendar.Month.TextFont = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pbCalendar.Name = "pbCalendar";
            this.pbCalendar.SelectionMode = Pabo.Calendar.mcSelectionMode.One;
            this.pbCalendar.SelectTrailingDates = false;
            this.pbCalendar.ShowFocus = false;
            this.pbCalendar.ShowFooter = false;
            this.pbCalendar.ShowTrailingDates = false;
            this.pbCalendar.Size = new System.Drawing.Size(342, 289);
            this.pbCalendar.TabIndex = 3;
            this.pbCalendar.TodayColor = System.Drawing.Color.DimGray;
            this.pbCalendar.Weekdays.BackColor1 = System.Drawing.Color.Lavender;
            this.pbCalendar.Weekdays.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pbCalendar.Weekdays.TextColor = System.Drawing.Color.Black;
            this.pbCalendar.Weeknumbers.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.pbCalendar.DayClick += new Pabo.Calendar.DayClickEventHandler(this.pbCalendar_DayClick);
            this.pbCalendar.DayDoubleClick += new Pabo.Calendar.DayClickEventHandler(this.pbCalendar_DayDoubleClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Lime;
            this.imageList1.Images.SetKeyName(0, "C2.png");
            // 
            // lblDateSelected
            // 
            this.lblDateSelected.AutoSize = true;
            this.lblDateSelected.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateSelected.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblDateSelected.Location = new System.Drawing.Point(12, 14);
            this.lblDateSelected.Name = "lblDateSelected";
            this.lblDateSelected.Size = new System.Drawing.Size(127, 24);
            this.lblDateSelected.TabIndex = 4;
            this.lblDateSelected.Text = "Date Selected";
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(185, 9);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(75, 29);
            this.btnSelect.TabIndex = 5;
            this.btnSelect.Text = "&Select";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(275, 9);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 29);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // grpbxPeriod
            // 
            this.grpbxPeriod.Controls.Add(this.rdoCurrFiscalYr);
            this.grpbxPeriod.Controls.Add(this.rdoPrevFiscalYr);
            this.grpbxPeriod.Location = new System.Drawing.Point(188, 33);
            this.grpbxPeriod.Name = "grpbxPeriod";
            this.grpbxPeriod.Size = new System.Drawing.Size(161, 55);
            this.grpbxPeriod.TabIndex = 9;
            this.grpbxPeriod.TabStop = false;
            // 
            // rdoCurrFiscalYr
            // 
            this.rdoCurrFiscalYr.AutoSize = true;
            this.rdoCurrFiscalYr.Checked = true;
            this.rdoCurrFiscalYr.Location = new System.Drawing.Point(20, 30);
            this.rdoCurrFiscalYr.Name = "rdoCurrFiscalYr";
            this.rdoCurrFiscalYr.Size = new System.Drawing.Size(114, 17);
            this.rdoCurrFiscalYr.TabIndex = 1;
            this.rdoCurrFiscalYr.TabStop = true;
            this.rdoCurrFiscalYr.Text = "Current Fiscal Year";
            this.rdoCurrFiscalYr.UseVisualStyleBackColor = true;
            // 
            // rdoPrevFiscalYr
            // 
            this.rdoPrevFiscalYr.AutoSize = true;
            this.rdoPrevFiscalYr.Location = new System.Drawing.Point(20, 10);
            this.rdoPrevFiscalYr.Name = "rdoPrevFiscalYr";
            this.rdoPrevFiscalYr.Size = new System.Drawing.Size(121, 17);
            this.rdoPrevFiscalYr.TabIndex = 0;
            this.rdoPrevFiscalYr.TabStop = true;
            this.rdoPrevFiscalYr.Text = "Previous Fiscal Year";
            this.rdoPrevFiscalYr.UseVisualStyleBackColor = true;
            // 
            // DateSelectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Cornsilk;
            this.ClientSize = new System.Drawing.Size(359, 394);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.grpbxPeriod);
            this.Controls.Add(this.lblDateSelected);
            this.Controls.Add(this.pbCalendar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "DateSelectionForm";
            this.Text = "Service Date Selection Form";
            this.grpbxPeriod.ResumeLayout(false);
            this.grpbxPeriod.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Pabo.Calendar.PBCalendar pbCalendar;
        private System.Windows.Forms.Label lblDateSelected;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.GroupBox grpbxPeriod;
        private System.Windows.Forms.RadioButton rdoPrevFiscalYr;
        private System.Windows.Forms.RadioButton rdoCurrFiscalYr;
    }
}