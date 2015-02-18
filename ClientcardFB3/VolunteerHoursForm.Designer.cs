namespace ClientcardFB3
{
    partial class VolunteerHoursForm
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
                if (clsVolGroups != null)
                {
                    clsVolGroups.Dispose();
                }
                if (clsVolHrs != null)
                {
                    clsVolHrs.Dispose();
                }
                if (clsVols != null)
                {
                    clsVols.Dispose();
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
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Check Groups to Display", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Select Volunteer Group(s)", System.Windows.Forms.HorizontalAlignment.Center);
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "",
            "Show All Groups",
            ""}, -1, System.Drawing.Color.Empty, System.Drawing.Color.Empty, new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnAddTrxDate = new System.Windows.Forms.Button();
            this.lvDates = new System.Windows.Forms.ListView();
            this.clmDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmNumVols = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmVolHrs = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnDeleteTrxDate = new System.Windows.Forms.Button();
            this.tbTotVolHrs = new System.Windows.Forms.TextBox();
            this.rdoAllDates = new System.Windows.Forms.RadioButton();
            this.tbNumVols = new System.Windows.Forms.TextBox();
            this.rdoPrevYear = new System.Windows.Forms.RadioButton();
            this.rdoCurMonth = new System.Windows.Forms.RadioButton();
            this.rdoCurYear = new System.Windows.Forms.RadioButton();
            this.rdoPrevMonth = new System.Windows.Forms.RadioButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.chkShowWithData = new System.Windows.Forms.CheckBox();
            this.lvVolGroups = new System.Windows.Forms.ListView();
            this.colVolGrp0 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colVolGrpName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colVolGrpHours = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnSaveHrs = new System.Windows.Forms.Button();
            this.dgvVols = new System.Windows.Forms.DataGridView();
            this.clmVol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmTimeIn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmTimeOut = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmHrs = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmVHID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmVolID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbSumHrs = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.tbWorkDate = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVols)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnAddTrxDate);
            this.panel1.Controls.Add(this.lvDates);
            this.panel1.Controls.Add(this.btnDeleteTrxDate);
            this.panel1.Controls.Add(this.tbTotVolHrs);
            this.panel1.Controls.Add(this.rdoAllDates);
            this.panel1.Controls.Add(this.tbNumVols);
            this.panel1.Controls.Add(this.rdoPrevYear);
            this.panel1.Controls.Add(this.rdoCurMonth);
            this.panel1.Controls.Add(this.rdoCurYear);
            this.panel1.Controls.Add(this.rdoPrevMonth);
            this.panel1.ForeColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(264, 708);
            this.panel1.TabIndex = 0;
            // 
            // btnAddTrxDate
            // 
            this.btnAddTrxDate.Location = new System.Drawing.Point(135, 86);
            this.btnAddTrxDate.Name = "btnAddTrxDate";
            this.btnAddTrxDate.Size = new System.Drawing.Size(125, 28);
            this.btnAddTrxDate.TabIndex = 6;
            this.btnAddTrxDate.Text = "Add Date";
            this.btnAddTrxDate.UseVisualStyleBackColor = true;
            this.btnAddTrxDate.Click += new System.EventHandler(this.btnAddTrxDate_Click);
            // 
            // lvDates
            // 
            this.lvDates.BackColor = System.Drawing.Color.Cornsilk;
            this.lvDates.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvDates.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmDate,
            this.clmNumVols,
            this.clmVolHrs});
            this.lvDates.FullRowSelect = true;
            this.lvDates.GridLines = true;
            this.lvDates.HideSelection = false;
            this.lvDates.Location = new System.Drawing.Point(3, 144);
            this.lvDates.MultiSelect = false;
            this.lvDates.Name = "lvDates";
            this.lvDates.Size = new System.Drawing.Size(253, 561);
            this.lvDates.TabIndex = 0;
            this.lvDates.UseCompatibleStateImageBehavior = false;
            this.lvDates.View = System.Windows.Forms.View.Details;
            this.lvDates.SelectedIndexChanged += new System.EventHandler(this.lvDates_SelectedIndexChanged);
            // 
            // clmDate
            // 
            this.clmDate.Text = "Date";
            this.clmDate.Width = 100;
            // 
            // clmNumVols
            // 
            this.clmNumVols.Text = "# Vols.";
            this.clmNumVols.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.clmNumVols.Width = 70;
            // 
            // clmVolHrs
            // 
            this.clmVolHrs.Text = "Hours";
            this.clmVolHrs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.clmVolHrs.Width = 80;
            // 
            // btnDeleteTrxDate
            // 
            this.btnDeleteTrxDate.Location = new System.Drawing.Point(6, 85);
            this.btnDeleteTrxDate.Name = "btnDeleteTrxDate";
            this.btnDeleteTrxDate.Size = new System.Drawing.Size(125, 28);
            this.btnDeleteTrxDate.TabIndex = 5;
            this.btnDeleteTrxDate.Text = "Delete Date";
            this.btnDeleteTrxDate.UseVisualStyleBackColor = true;
            this.btnDeleteTrxDate.Click += new System.EventHandler(this.btnDeleteTrxDate_Click);
            // 
            // tbTotVolHrs
            // 
            this.tbTotVolHrs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbTotVolHrs.ForeColor = System.Drawing.Color.MediumBlue;
            this.tbTotVolHrs.Location = new System.Drawing.Point(175, 120);
            this.tbTotVolHrs.Name = "tbTotVolHrs";
            this.tbTotVolHrs.Size = new System.Drawing.Size(80, 23);
            this.tbTotVolHrs.TabIndex = 2;
            this.tbTotVolHrs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // rdoAllDates
            // 
            this.rdoAllDates.AutoSize = true;
            this.rdoAllDates.Location = new System.Drawing.Point(12, 61);
            this.rdoAllDates.Name = "rdoAllDates";
            this.rdoAllDates.Size = new System.Drawing.Size(82, 21);
            this.rdoAllDates.TabIndex = 4;
            this.rdoAllDates.Text = "All Dates";
            this.rdoAllDates.UseVisualStyleBackColor = true;
            this.rdoAllDates.CheckedChanged += new System.EventHandler(this.rdoAllDates_CheckedChanged);
            // 
            // tbNumVols
            // 
            this.tbNumVols.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbNumVols.ForeColor = System.Drawing.Color.MediumBlue;
            this.tbNumVols.Location = new System.Drawing.Point(104, 120);
            this.tbNumVols.Name = "tbNumVols";
            this.tbNumVols.Size = new System.Drawing.Size(70, 23);
            this.tbNumVols.TabIndex = 1;
            this.tbNumVols.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // rdoPrevYear
            // 
            this.rdoPrevYear.AutoSize = true;
            this.rdoPrevYear.Location = new System.Drawing.Point(143, 30);
            this.rdoPrevYear.Name = "rdoPrevYear";
            this.rdoPrevYear.Size = new System.Drawing.Size(115, 21);
            this.rdoPrevYear.TabIndex = 3;
            this.rdoPrevYear.Text = "Previous Year";
            this.rdoPrevYear.UseVisualStyleBackColor = true;
            this.rdoPrevYear.CheckedChanged += new System.EventHandler(this.rdoPrevYear_CheckedChanged);
            // 
            // rdoCurMonth
            // 
            this.rdoCurMonth.AutoSize = true;
            this.rdoCurMonth.Checked = true;
            this.rdoCurMonth.Location = new System.Drawing.Point(12, 9);
            this.rdoCurMonth.Name = "rdoCurMonth";
            this.rdoCurMonth.Size = new System.Drawing.Size(116, 21);
            this.rdoCurMonth.TabIndex = 0;
            this.rdoCurMonth.TabStop = true;
            this.rdoCurMonth.Tag = "CurMonth";
            this.rdoCurMonth.Text = "Current Month";
            this.rdoCurMonth.UseVisualStyleBackColor = true;
            this.rdoCurMonth.CheckedChanged += new System.EventHandler(this.rdoCurMonth_CheckedChanged);
            // 
            // rdoCurYear
            // 
            this.rdoCurYear.AutoSize = true;
            this.rdoCurYear.Location = new System.Drawing.Point(143, 10);
            this.rdoCurYear.Name = "rdoCurYear";
            this.rdoCurYear.Size = new System.Drawing.Size(107, 21);
            this.rdoCurYear.TabIndex = 2;
            this.rdoCurYear.Text = "Current Year";
            this.rdoCurYear.UseVisualStyleBackColor = true;
            this.rdoCurYear.CheckedChanged += new System.EventHandler(this.rdoCurYear_CheckedChanged);
            // 
            // rdoPrevMonth
            // 
            this.rdoPrevMonth.AutoSize = true;
            this.rdoPrevMonth.Location = new System.Drawing.Point(12, 32);
            this.rdoPrevMonth.Name = "rdoPrevMonth";
            this.rdoPrevMonth.Size = new System.Drawing.Size(121, 21);
            this.rdoPrevMonth.TabIndex = 1;
            this.rdoPrevMonth.Text = "Prevous Month";
            this.rdoPrevMonth.UseVisualStyleBackColor = true;
            this.rdoPrevMonth.CheckedChanged += new System.EventHandler(this.rdoPrevMonth_CheckedChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.chkShowWithData);
            this.panel3.Controls.Add(this.lvVolGroups);
            this.panel3.Controls.Add(this.btnSaveHrs);
            this.panel3.Controls.Add(this.dgvVols);
            this.panel3.Controls.Add(this.tbSumHrs);
            this.panel3.Controls.Add(this.btnClose);
            this.panel3.Controls.Add(this.tbWorkDate);
            this.panel3.Location = new System.Drawing.Point(264, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(738, 708);
            this.panel3.TabIndex = 1;
            // 
            // chkShowWithData
            // 
            this.chkShowWithData.AutoSize = true;
            this.chkShowWithData.Location = new System.Drawing.Point(454, 7);
            this.chkShowWithData.Name = "chkShowWithData";
            this.chkShowWithData.Size = new System.Drawing.Size(168, 21);
            this.chkShowWithData.TabIndex = 10;
            this.chkShowWithData.Text = "Show Only With Hours";
            this.chkShowWithData.UseVisualStyleBackColor = true;
            this.chkShowWithData.CheckedChanged += new System.EventHandler(this.chkShowWithData_CheckedChanged);
            // 
            // lvVolGroups
            // 
            this.lvVolGroups.Alignment = System.Windows.Forms.ListViewAlignment.Default;
            this.lvVolGroups.BackColor = System.Drawing.Color.LightYellow;
            this.lvVolGroups.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvVolGroups.CheckBoxes = true;
            this.lvVolGroups.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colVolGrp0,
            this.colVolGrpName,
            this.colVolGrpHours});
            this.lvVolGroups.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvVolGroups.ForeColor = System.Drawing.Color.Maroon;
            listViewGroup1.Header = "Check Groups to Display";
            listViewGroup1.Name = "lvGrpAll";
            listViewGroup2.Header = "Select Volunteer Group(s)";
            listViewGroup2.HeaderAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            listViewGroup2.Name = "lvGrpGroups";
            this.lvVolGroups.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2});
            this.lvVolGroups.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            listViewItem1.Checked = true;
            listViewItem1.Group = listViewGroup1;
            listViewItem1.StateImageIndex = 1;
            listViewItem1.Tag = "0";
            this.lvVolGroups.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.lvVolGroups.Location = new System.Drawing.Point(455, 137);
            this.lvVolGroups.Name = "lvVolGroups";
            this.lvVolGroups.Size = new System.Drawing.Size(275, 509);
            this.lvVolGroups.TabIndex = 9;
            this.lvVolGroups.UseCompatibleStateImageBehavior = false;
            this.lvVolGroups.View = System.Windows.Forms.View.Details;
            this.lvVolGroups.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvVolGroups_ItemChecked);
            // 
            // colVolGrp0
            // 
            this.colVolGrp0.Text = "";
            this.colVolGrp0.Width = 25;
            // 
            // colVolGrpName
            // 
            this.colVolGrpName.Text = "Volunteer Group";
            this.colVolGrpName.Width = 160;
            // 
            // colVolGrpHours
            // 
            this.colVolGrpHours.Text = "Hrs";
            this.colVolGrpHours.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colVolGrpHours.Width = 70;
            // 
            // btnSaveHrs
            // 
            this.btnSaveHrs.Location = new System.Drawing.Point(454, 33);
            this.btnSaveHrs.Name = "btnSaveHrs";
            this.btnSaveHrs.Size = new System.Drawing.Size(117, 27);
            this.btnSaveHrs.TabIndex = 8;
            this.btnSaveHrs.Text = "Quit Edit Mode";
            this.btnSaveHrs.UseVisualStyleBackColor = true;
            this.btnSaveHrs.Click += new System.EventHandler(this.btnSaveHrs_Click);
            // 
            // dgvVols
            // 
            this.dgvVols.AllowUserToAddRows = false;
            this.dgvVols.AllowUserToDeleteRows = false;
            this.dgvVols.AllowUserToResizeRows = false;
            this.dgvVols.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgvVols.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVols.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmVol,
            this.clmTimeIn,
            this.clmTimeOut,
            this.clmHrs,
            this.clmId,
            this.clmVHID,
            this.clmVolID});
            this.dgvVols.Dock = System.Windows.Forms.DockStyle.Left;
            this.dgvVols.Location = new System.Drawing.Point(0, 0);
            this.dgvVols.Name = "dgvVols";
            this.dgvVols.RowHeadersVisible = false;
            this.dgvVols.RowHeadersWidth = 20;
            this.dgvVols.RowTemplate.Height = 24;
            this.dgvVols.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvVols.Size = new System.Drawing.Size(448, 708);
            this.dgvVols.TabIndex = 4;
            this.dgvVols.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvVols_CellEndEdit);
            this.dgvVols.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvVols_CellEnter);
            this.dgvVols.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvVols_CellValidating);
            // 
            // clmVol
            // 
            this.clmVol.HeaderText = "Volunteer";
            this.clmVol.MinimumWidth = 40;
            this.clmVol.Name = "clmVol";
            this.clmVol.ReadOnly = true;
            this.clmVol.Width = 240;
            // 
            // clmTimeIn
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.clmTimeIn.DefaultCellStyle = dataGridViewCellStyle1;
            this.clmTimeIn.HeaderText = "Time In";
            this.clmTimeIn.Name = "clmTimeIn";
            this.clmTimeIn.Width = 60;
            // 
            // clmTimeOut
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.clmTimeOut.DefaultCellStyle = dataGridViewCellStyle2;
            this.clmTimeOut.HeaderText = "Time Out";
            this.clmTimeOut.Name = "clmTimeOut";
            this.clmTimeOut.Width = 60;
            // 
            // clmHrs
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.clmHrs.DefaultCellStyle = dataGridViewCellStyle3;
            this.clmHrs.HeaderText = "Hours";
            this.clmHrs.MinimumWidth = 20;
            this.clmHrs.Name = "clmHrs";
            this.clmHrs.Width = 60;
            // 
            // clmId
            // 
            this.clmId.HeaderText = "ID";
            this.clmId.Name = "clmId";
            this.clmId.ReadOnly = true;
            this.clmId.Visible = false;
            // 
            // clmVHID
            // 
            this.clmVHID.HeaderText = "VHID";
            this.clmVHID.Name = "clmVHID";
            this.clmVHID.Visible = false;
            // 
            // clmVolID
            // 
            this.clmVolID.HeaderText = "VolID";
            this.clmVolID.Name = "clmVolID";
            this.clmVolID.Visible = false;
            // 
            // tbSumHrs
            // 
            this.tbSumHrs.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbSumHrs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbSumHrs.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbSumHrs.ForeColor = System.Drawing.Color.MediumBlue;
            this.tbSumHrs.Location = new System.Drawing.Point(586, 36);
            this.tbSumHrs.Name = "tbSumHrs";
            this.tbSumHrs.ReadOnly = true;
            this.tbSumHrs.Size = new System.Drawing.Size(79, 23);
            this.tbSumHrs.TabIndex = 3;
            this.tbSumHrs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(672, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(52, 24);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // tbWorkDate
            // 
            this.tbWorkDate.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbWorkDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbWorkDate.ForeColor = System.Drawing.Color.MediumBlue;
            this.tbWorkDate.Location = new System.Drawing.Point(455, 105);
            this.tbWorkDate.Name = "tbWorkDate";
            this.tbWorkDate.ReadOnly = true;
            this.tbWorkDate.Size = new System.Drawing.Size(262, 23);
            this.tbWorkDate.TabIndex = 2;
            // 
            // VolunteerHoursForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Cornsilk;
            this.ClientSize = new System.Drawing.Size(1006, 714);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "VolunteerHoursForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Volunteer Hours Form";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.VolunteerHoursForm_FormClosing);
            this.Shown += new System.EventHandler(this.VolunteerHoursForm_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVols)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ListView lvDates;
        private System.Windows.Forms.TextBox tbTotVolHrs;
        private System.Windows.Forms.TextBox tbNumVols;
        private System.Windows.Forms.ColumnHeader clmDate;
        private System.Windows.Forms.ColumnHeader clmNumVols;
        private System.Windows.Forms.ColumnHeader clmVolHrs;
        private System.Windows.Forms.RadioButton rdoAllDates;
        private System.Windows.Forms.RadioButton rdoPrevYear;
        private System.Windows.Forms.RadioButton rdoCurYear;
        private System.Windows.Forms.RadioButton rdoPrevMonth;
        private System.Windows.Forms.RadioButton rdoCurMonth;
        private System.Windows.Forms.Button btnAddTrxDate;
        private System.Windows.Forms.Button btnDeleteTrxDate;
        private System.Windows.Forms.DataGridView dgvVols;
        private System.Windows.Forms.TextBox tbSumHrs;
        private System.Windows.Forms.TextBox tbWorkDate;
        private System.Windows.Forms.Button btnSaveHrs;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ListView lvVolGroups;
        private System.Windows.Forms.ColumnHeader colVolGrp0;
        private System.Windows.Forms.ColumnHeader colVolGrpName;
        private System.Windows.Forms.ColumnHeader colVolGrpHours;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmVol;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmTimeIn;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmTimeOut;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmHrs;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmId;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmVHID;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmVolID;
        private System.Windows.Forms.CheckBox chkShowWithData;
    }
}