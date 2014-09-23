namespace ClientCardFB3 {
	partial class FrmTypeCodes {
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
			this.gridTypeCodes = new System.Windows.Forms.DataGridView();
			this.gridTypeCodes_Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.gridTypeCodes_TypeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.gridTypeCodes_ShortName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.gridTypeCodes_SortOrder = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.gridTypeCodes_BtnDelete = new System.Windows.Forms.DataGridViewButtonColumn();
			this.btnTypeCodes_Close = new System.Windows.Forms.Button();
			this.grpTypeCode = new System.Windows.Forms.GroupBox();
			this.txtTypeCodes_Add = new System.Windows.Forms.TextBox();
			this.btnTypeCodes_Add = new System.Windows.Forms.Button();
			this.cboTypeCode_Select = new System.Windows.Forms.ComboBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			((System.ComponentModel.ISupportInitialize)(this.gridTypeCodes)).BeginInit();
			this.grpTypeCode.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// gridTypeCodes
			// 
			this.gridTypeCodes.AllowUserToAddRows = false;
			this.gridTypeCodes.AllowUserToDeleteRows = false;
			this.gridTypeCodes.AllowUserToResizeRows = false;
			this.gridTypeCodes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridTypeCodes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.gridTypeCodes_Id,
            this.gridTypeCodes_TypeName,
            this.gridTypeCodes_ShortName,
            this.gridTypeCodes_SortOrder,
            this.gridTypeCodes_BtnDelete});
			this.gridTypeCodes.Location = new System.Drawing.Point(35, 75);
			this.gridTypeCodes.Margin = new System.Windows.Forms.Padding(4);
			this.gridTypeCodes.Name = "gridTypeCodes";
			this.gridTypeCodes.Size = new System.Drawing.Size(575, 480);
			this.gridTypeCodes.TabIndex = 0;
			this.gridTypeCodes.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.gridTypeCodes_CellBeginEdit);
			this.gridTypeCodes.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.gridTypeCodes_CellValidating);
			this.gridTypeCodes.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridTypeCodes_CellClick);
			// 
			// gridTypeCodes_Id
			// 
			this.gridTypeCodes_Id.HeaderText = "ID";
			this.gridTypeCodes_Id.Name = "gridTypeCodes_Id";
			this.gridTypeCodes_Id.Visible = false;
			// 
			// gridTypeCodes_TypeName
			// 
			this.gridTypeCodes_TypeName.HeaderText = "Type Name";
			this.gridTypeCodes_TypeName.Name = "gridTypeCodes_TypeName";
			this.gridTypeCodes_TypeName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.gridTypeCodes_TypeName.Width = 200;
			// 
			// gridTypeCodes_ShortName
			// 
			this.gridTypeCodes_ShortName.HeaderText = "Short Name";
			this.gridTypeCodes_ShortName.Name = "gridTypeCodes_ShortName";
			this.gridTypeCodes_ShortName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.gridTypeCodes_ShortName.Width = 110;
			// 
			// gridTypeCodes_SortOrder
			// 
			this.gridTypeCodes_SortOrder.HeaderText = "Sort Order";
			this.gridTypeCodes_SortOrder.Name = "gridTypeCodes_SortOrder";
			this.gridTypeCodes_SortOrder.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// gridTypeCodes_BtnDelete
			// 
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.Silver;
			this.gridTypeCodes_BtnDelete.DefaultCellStyle = dataGridViewCellStyle1;
			this.gridTypeCodes_BtnDelete.HeaderText = "";
			this.gridTypeCodes_BtnDelete.Name = "gridTypeCodes_BtnDelete";
			// 
			// btnTypeCodes_Close
			// 
			this.btnTypeCodes_Close.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.btnTypeCodes_Close.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnTypeCodes_Close.Location = new System.Drawing.Point(495, 10);
			this.btnTypeCodes_Close.Margin = new System.Windows.Forms.Padding(4);
			this.btnTypeCodes_Close.Name = "btnTypeCodes_Close";
			this.btnTypeCodes_Close.Size = new System.Drawing.Size(133, 37);
			this.btnTypeCodes_Close.TabIndex = 1;
			this.btnTypeCodes_Close.Text = "Save && Close";
			this.btnTypeCodes_Close.UseVisualStyleBackColor = false;
			this.btnTypeCodes_Close.Click += new System.EventHandler(this.btnTypeCodes_Close_Click);
			// 
			// grpTypeCode
			// 
			this.grpTypeCode.Controls.Add(this.txtTypeCodes_Add);
			this.grpTypeCode.Controls.Add(this.btnTypeCodes_Add);
			this.grpTypeCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grpTypeCode.Location = new System.Drawing.Point(85, 575);
			this.grpTypeCode.Margin = new System.Windows.Forms.Padding(4);
			this.grpTypeCode.Name = "grpTypeCode";
			this.grpTypeCode.Padding = new System.Windows.Forms.Padding(4);
			this.grpTypeCode.Size = new System.Drawing.Size(480, 80);
			this.grpTypeCode.TabIndex = 2;
			this.grpTypeCode.TabStop = false;
			this.grpTypeCode.Text = "Add New Type Code";
			// 
			// txtTypeCodes_Add
			// 
			this.txtTypeCodes_Add.Location = new System.Drawing.Point(27, 35);
			this.txtTypeCodes_Add.Margin = new System.Windows.Forms.Padding(4);
			this.txtTypeCodes_Add.Name = "txtTypeCodes_Add";
			this.txtTypeCodes_Add.Size = new System.Drawing.Size(265, 22);
			this.txtTypeCodes_Add.TabIndex = 3;
			this.txtTypeCodes_Add.TextChanged += new System.EventHandler(this.txtTypeCodes_Add_TextChanged);
			// 
			// btnTypeCodes_Add
			// 
			this.btnTypeCodes_Add.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.btnTypeCodes_Add.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnTypeCodes_Add.Location = new System.Drawing.Point(320, 29);
			this.btnTypeCodes_Add.Margin = new System.Windows.Forms.Padding(4);
			this.btnTypeCodes_Add.Name = "btnTypeCodes_Add";
			this.btnTypeCodes_Add.Size = new System.Drawing.Size(133, 37);
			this.btnTypeCodes_Add.TabIndex = 2;
			this.btnTypeCodes_Add.Text = "Add Type Code";
			this.btnTypeCodes_Add.UseVisualStyleBackColor = false;
			this.btnTypeCodes_Add.Click += new System.EventHandler(this.btnTypeCodes_Add_Click);
			// 
			// cboTypeCode_Select
			// 
			this.cboTypeCode_Select.FormattingEnabled = true;
			this.cboTypeCode_Select.Location = new System.Drawing.Point(18, 20);
			this.cboTypeCode_Select.Name = "cboTypeCode_Select";
			this.cboTypeCode_Select.Size = new System.Drawing.Size(235, 24);
			this.cboTypeCode_Select.TabIndex = 3;
			this.cboTypeCode_Select.SelectedIndexChanged += new System.EventHandler(this.cboTypeCode_Select_SelectedIndexChanged);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.cboTypeCode_Select);
			this.groupBox1.Location = new System.Drawing.Point(35, 10);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(270, 55);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Select Type Code";
			// 
			// FrmTypeCodes
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.LightGreen;
			this.ClientSize = new System.Drawing.Size(642, 673);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.grpTypeCode);
			this.Controls.Add(this.btnTypeCodes_Close);
			this.Controls.Add(this.gridTypeCodes);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Margin = new System.Windows.Forms.Padding(4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FrmTypeCodes";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.FrmTypeCodes_Load);
			this.Shown += new System.EventHandler(this.FrmTypeCodes_Shown);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmTypeCodes_FormClosing);
			((System.ComponentModel.ISupportInitialize)(this.gridTypeCodes)).EndInit();
			this.grpTypeCode.ResumeLayout(false);
			this.grpTypeCode.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView gridTypeCodes;
		private System.Windows.Forms.Button btnTypeCodes_Close;
		private System.Windows.Forms.GroupBox grpTypeCode;
		private System.Windows.Forms.TextBox txtTypeCodes_Add;
		private System.Windows.Forms.Button btnTypeCodes_Add;
		private System.Windows.Forms.DataGridViewTextBoxColumn gridTypeCodes_Id;
		private System.Windows.Forms.DataGridViewTextBoxColumn gridTypeCodes_TypeName;
		private System.Windows.Forms.DataGridViewTextBoxColumn gridTypeCodes_ShortName;
		private System.Windows.Forms.DataGridViewTextBoxColumn gridTypeCodes_SortOrder;
		private System.Windows.Forms.DataGridViewButtonColumn gridTypeCodes_BtnDelete;
		private System.Windows.Forms.ComboBox cboTypeCode_Select;
		private System.Windows.Forms.GroupBox groupBox1;
	}
}