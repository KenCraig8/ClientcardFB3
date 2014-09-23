namespace ClientcardFB3 {
	partial class EditUserFields {
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gridUserField = new System.Windows.Forms.DataGridView();
            this.colTableName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFldName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEditLabel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEditTip = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAutoAlert = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colAutoAlertText = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.csmGridUserFields = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiResetTrue = new System.Windows.Forms.ToolStripMenuItem();
            this.resetToCheckedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetToUncheckedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiResetUserNumber = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSave = new System.Windows.Forms.Button();
            this.cboTable_Select = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsmiFile = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridUserField)).BeginInit();
            this.csmGridUserFields.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridUserField
            // 
            this.gridUserField.AllowUserToAddRows = false;
            this.gridUserField.AllowUserToDeleteRows = false;
            this.gridUserField.AllowUserToResizeRows = false;
            this.gridUserField.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridUserField.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTableName,
            this.colFldName,
            this.colEditLabel,
            this.colEditTip,
            this.colAutoAlert,
            this.colAutoAlertText});
            this.gridUserField.ContextMenuStrip = this.csmGridUserFields;
            this.gridUserField.GridColor = System.Drawing.Color.SaddleBrown;
            this.gridUserField.Location = new System.Drawing.Point(5, 92);
            this.gridUserField.Margin = new System.Windows.Forms.Padding(4);
            this.gridUserField.Name = "gridUserField";
            this.gridUserField.RowHeadersVisible = false;
            this.gridUserField.RowTemplate.Height = 24;
            this.gridUserField.Size = new System.Drawing.Size(1008, 409);
            this.gridUserField.TabIndex = 0;
            this.gridUserField.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.grid_CellBeginEdit);
            this.gridUserField.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_CellClick);
            this.gridUserField.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridUserField_CellEndEdit);
            this.gridUserField.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridUserField_RowEnter);
            // 
            // colTableName
            // 
            this.colTableName.HeaderText = "TableName";
            this.colTableName.Name = "colTableName";
            this.colTableName.Visible = false;
            // 
            // colFldName
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.colFldName.DefaultCellStyle = dataGridViewCellStyle7;
            this.colFldName.FillWeight = 110F;
            this.colFldName.HeaderText = "Field Name";
            this.colFldName.Name = "colFldName";
            this.colFldName.ReadOnly = true;
            this.colFldName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colFldName.Width = 125;
            // 
            // colEditLabel
            // 
            this.colEditLabel.DataPropertyName = "EditLabel";
            this.colEditLabel.FillWeight = 200F;
            this.colEditLabel.HeaderText = "Edit Label";
            this.colEditLabel.Name = "colEditLabel";
            this.colEditLabel.Width = 300;
            // 
            // colEditTip
            // 
            this.colEditTip.DataPropertyName = "EditTip";
            this.colEditTip.HeaderText = "Edit Tip";
            this.colEditTip.Name = "colEditTip";
            this.colEditTip.Width = 250;
            // 
            // colAutoAlert
            // 
            this.colAutoAlert.DataPropertyName = "AutoAlert";
            this.colAutoAlert.HeaderText = "Auto Alert";
            this.colAutoAlert.Name = "colAutoAlert";
            this.colAutoAlert.Width = 50;
            // 
            // colAutoAlertText
            // 
            this.colAutoAlertText.DataPropertyName = "AutoAlertText";
            this.colAutoAlertText.HeaderText = "Auto Alert Text";
            this.colAutoAlertText.Name = "colAutoAlertText";
            this.colAutoAlertText.Width = 300;
            // 
            // csmGridUserFields
            // 
            this.csmGridUserFields.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiResetTrue,
            this.tsmiResetUserNumber});
            this.csmGridUserFields.Name = "csmGridUserFields";
            this.csmGridUserFields.Size = new System.Drawing.Size(176, 48);
            this.csmGridUserFields.Text = "Reset Field";
            this.csmGridUserFields.Opening += new System.ComponentModel.CancelEventHandler(this.csmGridUserFields_Opening);
            // 
            // tsmiResetTrue
            // 
            this.tsmiResetTrue.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resetToCheckedToolStripMenuItem,
            this.resetToUncheckedToolStripMenuItem});
            this.tsmiResetTrue.Name = "tsmiResetTrue";
            this.tsmiResetTrue.Size = new System.Drawing.Size(175, 22);
            this.tsmiResetTrue.Text = "Reset User Flag";
            // 
            // resetToCheckedToolStripMenuItem
            // 
            this.resetToCheckedToolStripMenuItem.Name = "resetToCheckedToolStripMenuItem";
            this.resetToCheckedToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.resetToCheckedToolStripMenuItem.Text = "Reset To Checked";
            this.resetToCheckedToolStripMenuItem.Click += new System.EventHandler(this.tsmiResetTrue_Click);
            // 
            // resetToUncheckedToolStripMenuItem
            // 
            this.resetToUncheckedToolStripMenuItem.Name = "resetToUncheckedToolStripMenuItem";
            this.resetToUncheckedToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.resetToUncheckedToolStripMenuItem.Text = "Reset To Unchecked";
            this.resetToUncheckedToolStripMenuItem.Click += new System.EventHandler(this.tsmiResetFalse_Click);
            // 
            // tsmiResetUserNumber
            // 
            this.tsmiResetUserNumber.Name = "tsmiResetUserNumber";
            this.tsmiResetUserNumber.Size = new System.Drawing.Size(175, 22);
            this.tsmiResetUserNumber.Text = "Reset User Number";
            this.tsmiResetUserNumber.Click += new System.EventHandler(this.tsmiResetUserNumber_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnSave.Enabled = false;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(321, 38);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(133, 37);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "&Save && Close";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cboTable_Select
            // 
            this.cboTable_Select.FormattingEnabled = true;
            this.cboTable_Select.Location = new System.Drawing.Point(18, 20);
            this.cboTable_Select.Name = "cboTable_Select";
            this.cboTable_Select.Size = new System.Drawing.Size(235, 24);
            this.cboTable_Select.TabIndex = 3;
            this.cboTable_Select.SelectedIndexChanged += new System.EventHandler(this.cboTable_Select_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboTable_Select);
            this.groupBox1.Location = new System.Drawing.Point(12, 31);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(270, 55);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select Table";
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(955, 41);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(57, 26);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFile});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1018, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tsmiFile
            // 
            this.tsmiFile.Name = "tsmiFile";
            this.tsmiFile.Size = new System.Drawing.Size(37, 20);
            this.tsmiFile.Text = "File";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "TableName";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridViewTextBoxColumn2.FillWeight = 110F;
            this.dataGridViewTextBoxColumn2.HeaderText = "Field Name";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn2.Width = 125;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.FillWeight = 200F;
            this.dataGridViewTextBoxColumn3.HeaderText = "Edit Label";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 200;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Edit Tip";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 250;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Auto Alert Text";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Width = 300;
            // 
            // EditUserFields
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGreen;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(1018, 508);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.gridUserField);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditUserFields";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Edit User Defined Fields";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditUserFields_FormClosing);
            this.Load += new System.EventHandler(this.EditUserFields_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridUserField)).EndInit();
            this.csmGridUserFields.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView gridUserField;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.ComboBox cboTable_Select;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiFile;
        private System.Windows.Forms.ContextMenuStrip csmGridUserFields;
        private System.Windows.Forms.ToolStripMenuItem tsmiResetTrue;
        private System.Windows.Forms.ToolStripMenuItem resetToCheckedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetToUncheckedToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTableName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFldName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEditLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEditTip;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colAutoAlert;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAutoAlertText;
        private System.Windows.Forms.ToolStripMenuItem tsmiResetUserNumber;
	}
}