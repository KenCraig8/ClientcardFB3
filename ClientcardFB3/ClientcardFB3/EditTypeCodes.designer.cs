namespace ClientcardFB3 {
	partial class EditTypeCodes {
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
            this.btn_Add = new System.Windows.Forms.Button();
            this.btn_Update = new System.Windows.Forms.Button();
            this.txt_ShortName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_SortOrder = new System.Windows.Forms.TextBox();
            this.txt_Type = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboTypeCodes_TableName = new System.Windows.Forms.ComboBox();
            this.btn_Close = new System.Windows.Forms.Button();
            this.gridTypeCodes = new System.Windows.Forms.DataGridView();
            this.gridTypeCodes_Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridTypeCodes_Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridTypeCodes_ShortName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridTypeCodes_SortOrder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridTypeCodes_BtnDelete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.txt_ID = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTypeCodes)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Add
            // 
            this.btn_Add.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_Add.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Add.Location = new System.Drawing.Point(230, 65);
            this.btn_Add.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(100, 30);
            this.btn_Add.TabIndex = 2;
            this.btn_Add.Text = "Add New";
            this.btn_Add.UseVisualStyleBackColor = false;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // btn_Update
            // 
            this.btn_Update.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_Update.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Update.Location = new System.Drawing.Point(230, 181);
            this.btn_Update.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Update.Name = "btn_Update";
            this.btn_Update.Size = new System.Drawing.Size(100, 30);
            this.btn_Update.TabIndex = 6;
            this.btn_Update.Text = "Update";
            this.btn_Update.UseVisualStyleBackColor = false;
            this.btn_Update.Click += new System.EventHandler(this.btn_Update_Click);
            // 
            // txt_ShortName
            // 
            this.txt_ShortName.Location = new System.Drawing.Point(130, 151);
            this.txt_ShortName.Margin = new System.Windows.Forms.Padding(4);
            this.txt_ShortName.MaxLength = 4;
            this.txt_ShortName.Name = "txt_ShortName";
            this.txt_ShortName.Size = new System.Drawing.Size(75, 22);
            this.txt_ShortName.TabIndex = 4;
            this.txt_ShortName.Tag = "d=ShortName";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 184);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 10;
            this.label2.Text = "Sort Order:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 154);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 16);
            this.label3.TabIndex = 9;
            this.label3.Text = "Short Name:";
            // 
            // txt_SortOrder
            // 
            this.txt_SortOrder.Location = new System.Drawing.Point(130, 181);
            this.txt_SortOrder.Margin = new System.Windows.Forms.Padding(4);
            this.txt_SortOrder.MaxLength = 2;
            this.txt_SortOrder.Name = "txt_SortOrder";
            this.txt_SortOrder.Size = new System.Drawing.Size(50, 22);
            this.txt_SortOrder.TabIndex = 5;
            this.txt_SortOrder.Tag = "d=SortOrder";
            this.txt_SortOrder.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_Type
            // 
            this.txt_Type.Location = new System.Drawing.Point(130, 121);
            this.txt_Type.Margin = new System.Windows.Forms.Padding(4);
            this.txt_Type.Name = "txt_Type";
            this.txt_Type.Size = new System.Drawing.Size(200, 22);
            this.txt_Type.TabIndex = 3;
            this.txt_Type.Tag = "d=Type";
            this.txt_Type.TextChanged += new System.EventHandler(this.txt_Add_TextChanged);
            this.txt_Type.Leave += new System.EventHandler(this.txt_Type_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(65, 124);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 16);
            this.label1.TabIndex = 8;
            this.label1.Text = "Name:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboTypeCodes_TableName);
            this.groupBox1.Location = new System.Drawing.Point(12, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(270, 55);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select Table";
            // 
            // cboTypeCodes_TableName
            // 
            this.cboTypeCodes_TableName.FormattingEnabled = true;
            this.cboTypeCodes_TableName.Location = new System.Drawing.Point(18, 20);
            this.cboTypeCodes_TableName.Name = "cboTypeCodes_TableName";
            this.cboTypeCodes_TableName.Size = new System.Drawing.Size(235, 24);
            this.cboTypeCodes_TableName.TabIndex = 1;
            this.cboTypeCodes_TableName.SelectedIndexChanged += new System.EventHandler(this.cboTypeCodes_TableName_SelectedIndexChanged);
            // 
            // btn_Close
            // 
            this.btn_Close.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_Close.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Close.Location = new System.Drawing.Point(118, 318);
            this.btn_Close.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(133, 37);
            this.btn_Close.TabIndex = 11;
            this.btn_Close.Text = "Save && Close";
            this.btn_Close.UseVisualStyleBackColor = false;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // gridTypeCodes
            // 
            this.gridTypeCodes.AllowUserToAddRows = false;
            this.gridTypeCodes.AllowUserToDeleteRows = false;
            this.gridTypeCodes.AllowUserToResizeRows = false;
            this.gridTypeCodes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridTypeCodes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.gridTypeCodes_Id,
            this.gridTypeCodes_Type,
            this.gridTypeCodes_ShortName,
            this.gridTypeCodes_SortOrder,
            this.gridTypeCodes_BtnDelete});
            this.gridTypeCodes.Location = new System.Drawing.Point(354, 3);
            this.gridTypeCodes.Margin = new System.Windows.Forms.Padding(4);
            this.gridTypeCodes.Name = "gridTypeCodes";
            this.gridTypeCodes.RowTemplate.Height = 24;
            this.gridTypeCodes.Size = new System.Drawing.Size(575, 466);
            this.gridTypeCodes.TabIndex = 8;
            this.gridTypeCodes.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.grid_CellBeginEdit);
            this.gridTypeCodes.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_CellClick);
            this.gridTypeCodes.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_CellEnter);
            this.gridTypeCodes.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_CellLeave);
            // 
            // gridTypeCodes_Id
            // 
            this.gridTypeCodes_Id.HeaderText = "ID";
            this.gridTypeCodes_Id.Name = "gridTypeCodes_Id";
            this.gridTypeCodes_Id.ReadOnly = true;
            this.gridTypeCodes_Id.Visible = false;
            // 
            // gridTypeCodes_Type
            // 
            this.gridTypeCodes_Type.HeaderText = "Type Name";
            this.gridTypeCodes_Type.Name = "gridTypeCodes_Type";
            this.gridTypeCodes_Type.ReadOnly = true;
            this.gridTypeCodes_Type.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.gridTypeCodes_Type.Width = 300;
            // 
            // gridTypeCodes_ShortName
            // 
            this.gridTypeCodes_ShortName.HeaderText = "Short Name";
            this.gridTypeCodes_ShortName.Name = "gridTypeCodes_ShortName";
            this.gridTypeCodes_ShortName.ReadOnly = true;
            this.gridTypeCodes_ShortName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.gridTypeCodes_ShortName.Width = 60;
            // 
            // gridTypeCodes_SortOrder
            // 
            this.gridTypeCodes_SortOrder.HeaderText = "Sort Order";
            this.gridTypeCodes_SortOrder.Name = "gridTypeCodes_SortOrder";
            this.gridTypeCodes_SortOrder.ReadOnly = true;
            this.gridTypeCodes_SortOrder.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.gridTypeCodes_SortOrder.Width = 60;
            // 
            // gridTypeCodes_BtnDelete
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Silver;
            this.gridTypeCodes_BtnDelete.DefaultCellStyle = dataGridViewCellStyle1;
            this.gridTypeCodes_BtnDelete.HeaderText = "";
            this.gridTypeCodes_BtnDelete.Name = "gridTypeCodes_BtnDelete";
            // 
            // txt_ID
            // 
            this.txt_ID.BackColor = System.Drawing.Color.White;
            this.txt_ID.Enabled = false;
            this.txt_ID.ForeColor = System.Drawing.Color.DarkBlue;
            this.txt_ID.Location = new System.Drawing.Point(130, 90);
            this.txt_ID.Margin = new System.Windows.Forms.Padding(4);
            this.txt_ID.Name = "txt_ID";
            this.txt_ID.Size = new System.Drawing.Size(50, 22);
            this.txt_ID.TabIndex = 12;
            this.txt_ID.Tag = "";
            this.txt_ID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(59, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 16);
            this.label4.TabIndex = 13;
            this.label4.Text = "Item Id:";
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Location = new System.Drawing.Point(230, 219);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(100, 30);
            this.btnDelete.TabIndex = 14;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Visible = false;
            // 
            // EditTypeCodes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGreen;
            this.ClientSize = new System.Drawing.Size(942, 473);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.txt_ID);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btn_Add);
            this.Controls.Add(this.txt_Type);
            this.Controls.Add(this.btn_Update);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.txt_ShortName);
            this.Controls.Add(this.gridTypeCodes);
            this.Controls.Add(this.txt_SortOrder);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditTypeCodes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Edit Type Codes";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditTypeCodes_FormClosing);
            this.Load += new System.EventHandler(this.EditTypeCodes_Load);
            this.Shown += new System.EventHandler(this.EditTypeCodes_Shown);
            this.SizeChanged += new System.EventHandler(this.EditTypeCodes_SizeChanged);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridTypeCodes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView gridTypeCodes;
		private System.Windows.Forms.Button btn_Close;
		private System.Windows.Forms.TextBox txt_Type;
		private System.Windows.Forms.Button btn_Update;
		private System.Windows.Forms.ComboBox cboTypeCodes_TableName;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox txt_ShortName;
		private System.Windows.Forms.TextBox txt_SortOrder;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.DataGridViewTextBoxColumn gridTypeCodes_Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn gridTypeCodes_Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn gridTypeCodes_ShortName;
        private System.Windows.Forms.DataGridViewTextBoxColumn gridTypeCodes_SortOrder;
        private System.Windows.Forms.DataGridViewButtonColumn gridTypeCodes_BtnDelete;
        private System.Windows.Forms.TextBox txt_ID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnDelete;
	}
}