namespace ClientCardFB3 {
	partial class EditDefaultsForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbo_DefaultsType = new System.Windows.Forms.ComboBox();
            this.gridDefaults = new System.Windows.Forms.DataGridView();
            this.gridDefaults_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridDefaults_FldName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridDefaults_EditLabel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridDefaults_FldVal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridDefaults_EditTip = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnClose = new System.Windows.Forms.Button();
            this.txt_NewValue = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.grp_NewValue = new System.Windows.Forms.GroupBox();
            this.cbo_NewValue = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridDefaults)).BeginInit();
            this.grp_NewValue.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbo_DefaultsType);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(13, 11);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(312, 68);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Show Preferences For";
            // 
            // cbo_DefaultsType
            // 
            this.cbo_DefaultsType.FormattingEnabled = true;
            this.cbo_DefaultsType.Location = new System.Drawing.Point(59, 27);
            this.cbo_DefaultsType.Margin = new System.Windows.Forms.Padding(4);
            this.cbo_DefaultsType.Name = "cbo_DefaultsType";
            this.cbo_DefaultsType.Size = new System.Drawing.Size(239, 28);
            this.cbo_DefaultsType.TabIndex = 3;
            this.cbo_DefaultsType.SelectedIndexChanged += new System.EventHandler(this.cbo_DefaultsType_SelectedIndexChanged);
            // 
            // gridDefaults
            // 
            this.gridDefaults.AllowUserToAddRows = false;
            this.gridDefaults.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridDefaults.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridDefaults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridDefaults.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.gridDefaults_ID,
            this.gridDefaults_FldName,
            this.gridDefaults_EditLabel,
            this.gridDefaults_FldVal,
            this.gridDefaults_EditTip});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridDefaults.DefaultCellStyle = dataGridViewCellStyle4;
            this.gridDefaults.Location = new System.Drawing.Point(1, 148);
            this.gridDefaults.Margin = new System.Windows.Forms.Padding(4);
            this.gridDefaults.Name = "gridDefaults";
            this.gridDefaults.RowTemplate.Height = 24;
            this.gridDefaults.Size = new System.Drawing.Size(998, 569);
            this.gridDefaults.TabIndex = 8;
            this.gridDefaults.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridDefaults_CellContentClick);
            this.gridDefaults.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridDefaults_KeyDown);
            // 
            // gridDefaults_ID
            // 
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridDefaults_ID.DefaultCellStyle = dataGridViewCellStyle2;
            this.gridDefaults_ID.HeaderText = "ID";
            this.gridDefaults_ID.Name = "gridDefaults_ID";
            this.gridDefaults_ID.ReadOnly = true;
            this.gridDefaults_ID.Visible = false;
            // 
            // gridDefaults_FldName
            // 
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridDefaults_FldName.DefaultCellStyle = dataGridViewCellStyle3;
            this.gridDefaults_FldName.HeaderText = "Field Name";
            this.gridDefaults_FldName.Name = "gridDefaults_FldName";
            this.gridDefaults_FldName.ReadOnly = true;
            this.gridDefaults_FldName.Visible = false;
            this.gridDefaults_FldName.Width = 200;
            // 
            // gridDefaults_EditLabel
            // 
            this.gridDefaults_EditLabel.HeaderText = "Edit Label";
            this.gridDefaults_EditLabel.Name = "gridDefaults_EditLabel";
            this.gridDefaults_EditLabel.ReadOnly = true;
            this.gridDefaults_EditLabel.Width = 200;
            // 
            // gridDefaults_FldVal
            // 
            this.gridDefaults_FldVal.HeaderText = "Value";
            this.gridDefaults_FldVal.Name = "gridDefaults_FldVal";
            this.gridDefaults_FldVal.ReadOnly = true;
            this.gridDefaults_FldVal.Width = 400;
            // 
            // gridDefaults_EditTip
            // 
            this.gridDefaults_EditTip.HeaderText = "EditTip";
            this.gridDefaults_EditTip.Name = "gridDefaults_EditTip";
            this.gridDefaults_EditTip.ReadOnly = true;
            this.gridDefaults_EditTip.Width = 400;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.SystemColors.Control;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(935, 2);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(64, 29);
            this.btnClose.TabIndex = 30;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.EditDefaultsForm_btnSave_Click);
            // 
            // txt_NewValue
            // 
            this.txt_NewValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_NewValue.Location = new System.Drawing.Point(33, 31);
            this.txt_NewValue.Margin = new System.Windows.Forms.Padding(4);
            this.txt_NewValue.Multiline = true;
            this.txt_NewValue.Name = "txt_NewValue";
            this.txt_NewValue.Size = new System.Drawing.Size(399, 29);
            this.txt_NewValue.TabIndex = 31;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.SystemColors.Control;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(453, 27);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(84, 37);
            this.btnSave.TabIndex = 39;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // grp_NewValue
            // 
            this.grp_NewValue.Controls.Add(this.cbo_NewValue);
            this.grp_NewValue.Controls.Add(this.txt_NewValue);
            this.grp_NewValue.Controls.Add(this.btnSave);
            this.grp_NewValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grp_NewValue.Location = new System.Drawing.Point(333, 38);
            this.grp_NewValue.Margin = new System.Windows.Forms.Padding(4);
            this.grp_NewValue.Name = "grp_NewValue";
            this.grp_NewValue.Padding = new System.Windows.Forms.Padding(4);
            this.grp_NewValue.Size = new System.Drawing.Size(547, 80);
            this.grp_NewValue.TabIndex = 40;
            this.grp_NewValue.TabStop = false;
            this.grp_NewValue.Text = "New Value";
            this.grp_NewValue.Visible = false;
            // 
            // cbo_NewValue
            // 
            this.cbo_NewValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbo_NewValue.FormattingEnabled = true;
            this.cbo_NewValue.Location = new System.Drawing.Point(33, 32);
            this.cbo_NewValue.Margin = new System.Windows.Forms.Padding(4);
            this.cbo_NewValue.Name = "cbo_NewValue";
            this.cbo_NewValue.Size = new System.Drawing.Size(399, 28);
            this.cbo_NewValue.TabIndex = 41;
            // 
            // EditDefaultsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1002, 720);
            this.Controls.Add(this.grp_NewValue);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.gridDefaults);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditDefaultsForm";
            this.Text = "Edit Defaults";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditDefaultsForm_FormClosing);
            this.Load += new System.EventHandler(this.EditDefaultsForm_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridDefaults)).EndInit();
            this.grp_NewValue.ResumeLayout(false);
            this.grp_NewValue.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView gridDefaults;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.TextBox txt_NewValue;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.DataGridViewTextBoxColumn gridDefaults_ID;
		private System.Windows.Forms.DataGridViewTextBoxColumn gridDefaults_FldName;
		private System.Windows.Forms.DataGridViewTextBoxColumn gridDefaults_EditLabel;
		private System.Windows.Forms.DataGridViewTextBoxColumn gridDefaults_FldVal;
		private System.Windows.Forms.DataGridViewTextBoxColumn gridDefaults_EditTip;
		private System.Windows.Forms.GroupBox grp_NewValue;
        private System.Windows.Forms.ComboBox cbo_NewValue;

	}
}