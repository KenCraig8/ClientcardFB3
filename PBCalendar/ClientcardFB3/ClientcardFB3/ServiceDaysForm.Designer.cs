namespace ClientCardFB3
{
    partial class ServiceDaysForm
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
            this.calSvcDates = new System.Windows.Forms.MonthCalendar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblDayOfWeek = new System.Windows.Forms.Label();
            this.tbCurDate = new System.Windows.Forms.TextBox();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnUseDate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnAddSvcDate = new System.Windows.Forms.Button();
            this.chkIsCommDay = new System.Windows.Forms.CheckBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.lvSvcItms = new System.Windows.Forms.ListView();
            this.clmDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmLbsPerItem = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmItemType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmItemKey = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // calSvcDates
            // 
            this.calSvcDates.Location = new System.Drawing.Point(21, 12);
            this.calSvcDates.Margin = new System.Windows.Forms.Padding(12, 11, 12, 11);
            this.calSvcDates.Name = "calSvcDates";
            this.calSvcDates.TabIndex = 0;
            this.calSvcDates.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.calSvcDates_DateSelected);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lvSvcItms);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.chkIsCommDay);
            this.panel1.Controls.Add(this.btnAddSvcDate);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Controls.Add(this.btnUseDate);
            this.panel1.Controls.Add(this.btnNext);
            this.panel1.Controls.Add(this.btnPrev);
            this.panel1.Controls.Add(this.tbCurDate);
            this.panel1.Controls.Add(this.lblDayOfWeek);
            this.panel1.Location = new System.Drawing.Point(343, 1);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(639, 468);
            this.panel1.TabIndex = 1;
            // 
            // lblDayOfWeek
            // 
            this.lblDayOfWeek.AutoSize = true;
            this.lblDayOfWeek.Location = new System.Drawing.Point(136, 22);
            this.lblDayOfWeek.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDayOfWeek.Name = "lblDayOfWeek";
            this.lblDayOfWeek.Size = new System.Drawing.Size(46, 17);
            this.lblDayOfWeek.TabIndex = 0;
            this.lblDayOfWeek.Text = "label1";
            this.lblDayOfWeek.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbCurDate
            // 
            this.tbCurDate.Location = new System.Drawing.Point(78, 42);
            this.tbCurDate.Name = "tbCurDate";
            this.tbCurDate.Size = new System.Drawing.Size(164, 23);
            this.tbCurDate.TabIndex = 1;
            this.tbCurDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnPrev
            // 
            this.btnPrev.Location = new System.Drawing.Point(41, 42);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(31, 23);
            this.btnPrev.TabIndex = 2;
            this.btnPrev.Text = "P";
            this.btnPrev.UseVisualStyleBackColor = true;
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(248, 42);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(31, 23);
            this.btnNext.TabIndex = 3;
            this.btnNext.Text = "N";
            this.btnNext.UseVisualStyleBackColor = true;
            // 
            // btnUseDate
            // 
            this.btnUseDate.Location = new System.Drawing.Point(88, 71);
            this.btnUseDate.Name = "btnUseDate";
            this.btnUseDate.Size = new System.Drawing.Size(141, 23);
            this.btnUseDate.TabIndex = 4;
            this.btnUseDate.Text = "Use This Date";
            this.btnUseDate.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(88, 100);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(141, 23);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(88, 129);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(141, 23);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Save Changes";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // btnAddSvcDate
            // 
            this.btnAddSvcDate.Location = new System.Drawing.Point(301, 35);
            this.btnAddSvcDate.Name = "btnAddSvcDate";
            this.btnAddSvcDate.Size = new System.Drawing.Size(141, 37);
            this.btnAddSvcDate.TabIndex = 7;
            this.btnAddSvcDate.Text = "Add Service Date";
            this.btnAddSvcDate.UseVisualStyleBackColor = true;
            this.btnAddSvcDate.Click += new System.EventHandler(this.btnAddSvcDate_Click);
            // 
            // chkIsCommDay
            // 
            this.chkIsCommDay.AutoSize = true;
            this.chkIsCommDay.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.chkIsCommDay.Location = new System.Drawing.Point(301, 88);
            this.chkIsCommDay.Name = "chkIsCommDay";
            this.chkIsCommDay.Size = new System.Drawing.Size(155, 21);
            this.chkIsCommDay.TabIndex = 8;
            this.chkIsCommDay.Text = "Is Commodity Day";
            this.chkIsCommDay.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(492, 11);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(115, 23);
            this.btnClose.TabIndex = 11;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // lvSvcItms
            // 
            this.lvSvcItms.CheckBoxes = true;
            this.lvSvcItms.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmDescription,
            this.clmLbsPerItem,
            this.clmItemType,
            this.clmItemKey});
            this.lvSvcItms.GridLines = true;
            this.lvSvcItms.Location = new System.Drawing.Point(3, 158);
            this.lvSvcItms.Name = "lvSvcItms";
            this.lvSvcItms.Size = new System.Drawing.Size(618, 307);
            this.lvSvcItms.TabIndex = 12;
            this.lvSvcItms.UseCompatibleStateImageBehavior = false;
            this.lvSvcItms.View = System.Windows.Forms.View.Details;
            // 
            // clmDescription
            // 
            this.clmDescription.Tag = "Description";
            this.clmDescription.Text = "Description";
            this.clmDescription.Width = 181;
            // 
            // clmLbsPerItem
            // 
            this.clmLbsPerItem.Text = "Lb/Item";
            // 
            // clmItemType
            // 
            this.clmItemType.Tag = "ItemType";
            this.clmItemType.Text = "Item Type";
            this.clmItemType.Width = 83;
            // 
            // clmItemKey
            // 
            this.clmItemKey.Tag = "ItemKey";
            this.clmItemKey.Text = "K";
            this.clmItemKey.Width = 25;
            // 
            // ServiceDaysForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Cornsilk;
            this.ClientSize = new System.Drawing.Size(983, 469);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.calSvcDates);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ServiceDaysForm";
            this.Text = "ServiceDaysForm";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MonthCalendar calSvcDates;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnUseDate;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.TextBox tbCurDate;
        private System.Windows.Forms.Label lblDayOfWeek;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.CheckBox chkIsCommDay;
        private System.Windows.Forms.Button btnAddSvcDate;
        private System.Windows.Forms.ListView lvSvcItms;
        private System.Windows.Forms.ColumnHeader clmDescription;
        private System.Windows.Forms.ColumnHeader clmLbsPerItem;
        private System.Windows.Forms.ColumnHeader clmItemType;
        private System.Windows.Forms.ColumnHeader clmItemKey;
    }
}