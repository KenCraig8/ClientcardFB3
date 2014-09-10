namespace ClientcardFB3
{
    partial class EditSchoolSupplyPickupTicket
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.cboRegistration = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkSchoolSupplyFlag = new System.Windows.Forms.CheckBox();
            this.lblHHName = new System.Windows.Forms.Label();
            this.btnPrintTicket = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.cboPickupPerson = new System.Windows.Forms.ComboBox();
            this.tbID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvHHMembers = new System.Windows.Forms.DataGridView();
            this.clmSchSupply = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.clmName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmAge = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmGrade = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmGender = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmSchool = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.clmDelivered = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmHMID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHHMembers)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.cboRegistration);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.chkSchoolSupplyFlag);
            this.splitContainer1.Panel1.Controls.Add(this.lblHHName);
            this.splitContainer1.Panel1.Controls.Add(this.btnPrintTicket);
            this.splitContainer1.Panel1.Controls.Add(this.btnSave);
            this.splitContainer1.Panel1.Controls.Add(this.cboPickupPerson);
            this.splitContainer1.Panel1.Controls.Add(this.tbID);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvHHMembers);
            this.splitContainer1.Size = new System.Drawing.Size(716, 370);
            this.splitContainer1.SplitterDistance = 115;
            this.splitContainer1.TabIndex = 0;
            // 
            // cboRegistration
            // 
            this.cboRegistration.FormattingEnabled = true;
            this.cboRegistration.Location = new System.Drawing.Point(462, 34);
            this.cboRegistration.Name = "cboRegistration";
            this.cboRegistration.Size = new System.Drawing.Size(80, 21);
            this.cboRegistration.TabIndex = 66;
            this.cboRegistration.SelectionChangeCommitted += new System.EventHandler(this.cboRegistration_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(452, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 13);
            this.label1.TabIndex = 65;
            this.label1.Text = "Registration Location";
            // 
            // chkSchoolSupplyFlag
            // 
            this.chkSchoolSupplyFlag.AutoSize = true;
            this.chkSchoolSupplyFlag.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSchoolSupplyFlag.Location = new System.Drawing.Point(31, 40);
            this.chkSchoolSupplyFlag.Name = "chkSchoolSupplyFlag";
            this.chkSchoolSupplyFlag.Size = new System.Drawing.Size(235, 19);
            this.chkSchoolSupplyFlag.TabIndex = 64;
            this.chkSchoolSupplyFlag.Text = "Participates In School Supply Program";
            this.chkSchoolSupplyFlag.UseVisualStyleBackColor = true;
            this.chkSchoolSupplyFlag.CheckedChanged += new System.EventHandler(this.chkSchoolSupplyFlag_CheckedChanged);
            // 
            // lblHHName
            // 
            this.lblHHName.AutoSize = true;
            this.lblHHName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHHName.Location = new System.Drawing.Point(78, 9);
            this.lblHHName.Name = "lblHHName";
            this.lblHHName.Size = new System.Drawing.Size(95, 18);
            this.lblHHName.TabIndex = 63;
            this.lblHHName.Text = "Family Name";
            // 
            // btnPrintTicket
            // 
            this.btnPrintTicket.Location = new System.Drawing.Point(592, 44);
            this.btnPrintTicket.Name = "btnPrintTicket";
            this.btnPrintTicket.Size = new System.Drawing.Size(100, 28);
            this.btnPrintTicket.TabIndex = 62;
            this.btnPrintTicket.Text = "&Print Ticket";
            this.btnPrintTicket.UseVisualStyleBackColor = true;
            this.btnPrintTicket.Click += new System.EventHandler(this.btnPrintTicket_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(592, 12);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 28);
            this.btnSave.TabIndex = 61;
            this.btnSave.Text = "&Save Changes";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cboPickupPerson
            // 
            this.cboPickupPerson.FormattingEnabled = true;
            this.cboPickupPerson.Location = new System.Drawing.Point(94, 80);
            this.cboPickupPerson.Name = "cboPickupPerson";
            this.cboPickupPerson.Size = new System.Drawing.Size(235, 21);
            this.cboPickupPerson.TabIndex = 60;
            this.cboPickupPerson.SelectionChangeCommitted += new System.EventHandler(this.cboPickupPerson_SelectionChangeCommitted);
            this.cboPickupPerson.Leave += new System.EventHandler(this.cboPickupPerson_Leave);
            // 
            // tbID
            // 
            this.tbID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbID.Location = new System.Drawing.Point(5, 6);
            this.tbID.Name = "tbID";
            this.tbID.ReadOnly = true;
            this.tbID.Size = new System.Drawing.Size(60, 26);
            this.tbID.TabIndex = 2;
            this.tbID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Pick-up Person";
            // 
            // dgvHHMembers
            // 
            this.dgvHHMembers.AllowUserToAddRows = false;
            this.dgvHHMembers.AllowUserToDeleteRows = false;
            this.dgvHHMembers.AllowUserToResizeRows = false;
            this.dgvHHMembers.BackgroundColor = System.Drawing.Color.Cornsilk;
            this.dgvHHMembers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.DarkBlue;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvHHMembers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvHHMembers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHHMembers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmSchSupply,
            this.clmName,
            this.clmAge,
            this.clmGrade,
            this.clmGender,
            this.clmSchool,
            this.clmDelivered,
            this.clmHMID});
            this.dgvHHMembers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHHMembers.GridColor = System.Drawing.Color.Black;
            this.dgvHHMembers.Location = new System.Drawing.Point(0, 0);
            this.dgvHHMembers.MultiSelect = false;
            this.dgvHHMembers.Name = "dgvHHMembers";
            this.dgvHHMembers.RowHeadersVisible = false;
            this.dgvHHMembers.RowHeadersWidth = 5;
            this.dgvHHMembers.RowTemplate.Height = 24;
            this.dgvHHMembers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvHHMembers.Size = new System.Drawing.Size(716, 251);
            this.dgvHHMembers.TabIndex = 53;
            this.dgvHHMembers.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvHHMembers_CellEndEdit);
            this.dgvHHMembers.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvHHMembers_CellValidating);
            // 
            // clmSchSupply
            // 
            this.clmSchSupply.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.clmSchSupply.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.clmSchSupply.HeaderText = "Select";
            this.clmSchSupply.Name = "clmSchSupply";
            this.clmSchSupply.Width = 60;
            // 
            // clmName
            // 
            this.clmName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clmName.DefaultCellStyle = dataGridViewCellStyle2;
            this.clmName.HeaderText = "Student Name";
            this.clmName.Name = "clmName";
            this.clmName.ReadOnly = true;
            this.clmName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.clmName.Width = 200;
            // 
            // clmAge
            // 
            this.clmAge.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clmAge.DefaultCellStyle = dataGridViewCellStyle3;
            this.clmAge.HeaderText = "Age";
            this.clmAge.Name = "clmAge";
            this.clmAge.ReadOnly = true;
            this.clmAge.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.clmAge.Width = 50;
            // 
            // clmGrade
            // 
            this.clmGrade.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clmGrade.DefaultCellStyle = dataGridViewCellStyle4;
            this.clmGrade.HeaderText = "Grade";
            this.clmGrade.Name = "clmGrade";
            this.clmGrade.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.clmGrade.Width = 50;
            // 
            // clmGender
            // 
            this.clmGender.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clmGender.DefaultCellStyle = dataGridViewCellStyle5;
            this.clmGender.HeaderText = "Gender";
            this.clmGender.Name = "clmGender";
            this.clmGender.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.clmGender.Width = 60;
            // 
            // clmSchool
            // 
            this.clmSchool.HeaderText = "School";
            this.clmSchool.Name = "clmSchool";
            this.clmSchool.Width = 180;
            // 
            // clmDelivered
            // 
            this.clmDelivered.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.clmDelivered.HeaderText = "Delivered";
            this.clmDelivered.Name = "clmDelivered";
            this.clmDelivered.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clmDelivered.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.clmDelivered.Width = 90;
            // 
            // clmHMID
            // 
            this.clmHMID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clmHMID.DefaultCellStyle = dataGridViewCellStyle6;
            this.clmHMID.HeaderText = "ID";
            this.clmHMID.Name = "clmHMID";
            this.clmHMID.ReadOnly = true;
            this.clmHMID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.clmHMID.Visible = false;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridViewTextBoxColumn1.HeaderText = "Student Name";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn1.Width = 200;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridViewTextBoxColumn2.HeaderText = "Age";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn2.Width = 50;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridViewTextBoxColumn3.HeaderText = "Grade";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn3.Width = 50;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridViewTextBoxColumn4.HeaderText = "Gender";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn4.Width = 60;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn5.HeaderText = "Delivered";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn5.Width = 70;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewTextBoxColumn6.DefaultCellStyle = dataGridViewCellStyle11;
            this.dataGridViewTextBoxColumn6.HeaderText = "ID";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn6.Visible = false;
            // 
            // EditSchoolSupplyPickupTicket
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(716, 370);
            this.Controls.Add(this.splitContainer1);
            this.Name = "EditSchoolSupplyPickupTicket";
            this.Text = "School Supply Pickup Ticket";
            this.Load += new System.EventHandler(this.SchoolSupplyPickupTicket_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHHMembers)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox tbID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvHHMembers;
        private System.Windows.Forms.ComboBox cboPickupPerson;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.Button btnPrintTicket;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblHHName;
        private System.Windows.Forms.CheckBox chkSchoolSupplyFlag;
        private System.Windows.Forms.DataGridViewCheckBoxColumn clmSchSupply;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmAge;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmGrade;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmGender;
        private System.Windows.Forms.DataGridViewComboBoxColumn clmSchool;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmDelivered;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmHMID;
        private System.Windows.Forms.ComboBox cboRegistration;
        private System.Windows.Forms.Label label1;
    }
}