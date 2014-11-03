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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gridUserField = new System.Windows.Forms.DataGridView();
            this.btnSave = new System.Windows.Forms.Button();
            this.cboTable_Select = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.gridUserField_TableName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridUserField_ControlType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridUserField_FldName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridUserField_EditLabel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridUserField_EditTip = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dbRow = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridUserField)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridUserField
            // 
            this.gridUserField.AllowUserToAddRows = false;
            this.gridUserField.AllowUserToDeleteRows = false;
            this.gridUserField.AllowUserToResizeRows = false;
            this.gridUserField.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridUserField.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.gridUserField_TableName,
            this.gridUserField_ControlType,
            this.gridUserField_FldName,
            this.gridUserField_EditLabel,
            this.gridUserField_EditTip,
            this.dbRow});
            this.gridUserField.GridColor = System.Drawing.Color.SaddleBrown;
            this.gridUserField.Location = new System.Drawing.Point(5, 92);
            this.gridUserField.Margin = new System.Windows.Forms.Padding(4);
            this.gridUserField.Name = "gridUserField";
            this.gridUserField.RowTemplate.Height = 24;
            this.gridUserField.Size = new System.Drawing.Size(1008, 409);
            this.gridUserField.TabIndex = 0;
            this.gridUserField.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.grid_CellBeginEdit);
            this.gridUserField.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_CellClick);
            this.gridUserField.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridUserField_CellEndEdit);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnSave.Enabled = false;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(321, 32);
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
            this.cboTable_Select.Size = new System.Drawing.Size(235, 28);
            this.cboTable_Select.TabIndex = 3;
            this.cboTable_Select.SelectedIndexChanged += new System.EventHandler(this.cboTable_Select_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboTable_Select);
            this.groupBox1.Location = new System.Drawing.Point(12, 17);
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
            this.btnClose.Location = new System.Drawing.Point(956, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(57, 26);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // gridUserField_TableName
            // 
            this.gridUserField_TableName.HeaderText = "TableName";
            this.gridUserField_TableName.Name = "gridUserField_TableName";
            this.gridUserField_TableName.Visible = false;
            // 
            // gridUserField_ControlType
            // 
            this.gridUserField_ControlType.HeaderText = "ControlType";
            this.gridUserField_ControlType.Name = "gridUserField_ControlType";
            this.gridUserField_ControlType.Visible = false;
            // 
            // gridUserField_FldName
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gridUserField_FldName.DefaultCellStyle = dataGridViewCellStyle1;
            this.gridUserField_FldName.FillWeight = 110F;
            this.gridUserField_FldName.HeaderText = "Field Name";
            this.gridUserField_FldName.Name = "gridUserField_FldName";
            this.gridUserField_FldName.ReadOnly = true;
            this.gridUserField_FldName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.gridUserField_FldName.Width = 140;
            // 
            // gridUserField_EditLabel
            // 
            this.gridUserField_EditLabel.FillWeight = 200F;
            this.gridUserField_EditLabel.HeaderText = "Edit Label";
            this.gridUserField_EditLabel.Name = "gridUserField_EditLabel";
            this.gridUserField_EditLabel.Width = 300;
            // 
            // gridUserField_EditTip
            // 
            this.gridUserField_EditTip.HeaderText = "Edit Tip";
            this.gridUserField_EditTip.Name = "gridUserField_EditTip";
            this.gridUserField_EditTip.Width = 500;
            // 
            // dbRow
            // 
            this.dbRow.HeaderText = "dbRow";
            this.dbRow.Name = "dbRow";
            this.dbRow.ReadOnly = true;
            this.dbRow.Visible = false;
            // 
            // EditUserFields
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGreen;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(1018, 508);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.gridUserField);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditUserFields";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Edit User Defined Fields";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditUserFields_FormClosing);
            this.Load += new System.EventHandler(this.EditUserFields_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridUserField)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView gridUserField;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.ComboBox cboTable_Select;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridViewTextBoxColumn gridUserField_TableName;
        private System.Windows.Forms.DataGridViewTextBoxColumn gridUserField_ControlType;
        private System.Windows.Forms.DataGridViewTextBoxColumn gridUserField_FldName;
        private System.Windows.Forms.DataGridViewTextBoxColumn gridUserField_EditLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn gridUserField_EditTip;
        private System.Windows.Forms.DataGridViewTextBoxColumn dbRow;
	}
}