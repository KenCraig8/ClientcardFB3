namespace ClientcardFB3
{
    partial class IncomeMatrixForm
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
                if (clsIncomeGroups != null)
                {
                    clsIncomeGroups.Dispose();
                }
                if (clsIncomeMatrix != null)
                {
                    clsIncomeMatrix.Dispose();
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle28 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.cboMatrixGroups = new System.Windows.Forms.ComboBox();
            this.dgvIncomeMatrix = new System.Windows.Forms.DataGridView();
            this.clmCat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmLowHi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmInc1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmInc2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmInc3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmInc4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmInc5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmInc6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmInc7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmInc8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmInc9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmInc10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addNewMatrixToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIncomeMatrix)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(11, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Matrix Group:";
            // 
            // cboMatrixGroups
            // 
            this.cboMatrixGroups.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboMatrixGroups.FormattingEnabled = true;
            this.cboMatrixGroups.Location = new System.Drawing.Point(11, 59);
            this.cboMatrixGroups.Name = "cboMatrixGroups";
            this.cboMatrixGroups.Size = new System.Drawing.Size(336, 32);
            this.cboMatrixGroups.TabIndex = 1;
            this.cboMatrixGroups.SelectionChangeCommitted += new System.EventHandler(this.cboMatrixGroups_SelectionChangeCommitted);
            // 
            // dgvIncomeMatrix
            // 
            this.dgvIncomeMatrix.AllowUserToAddRows = false;
            this.dgvIncomeMatrix.AllowUserToDeleteRows = false;
            this.dgvIncomeMatrix.AllowUserToResizeRows = false;
            this.dgvIncomeMatrix.BackgroundColor = System.Drawing.Color.Cornsilk;
            this.dgvIncomeMatrix.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvIncomeMatrix.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmCat,
            this.clmDesc,
            this.clmLowHi,
            this.clmInc1,
            this.clmInc2,
            this.clmInc3,
            this.clmInc4,
            this.clmInc5,
            this.clmInc6,
            this.clmInc7,
            this.clmInc8,
            this.clmInc9,
            this.clmInc10,
            this.clmID});
            this.dgvIncomeMatrix.Location = new System.Drawing.Point(8, 96);
            this.dgvIncomeMatrix.Name = "dgvIncomeMatrix";
            this.dgvIncomeMatrix.RowHeadersVisible = false;
            this.dgvIncomeMatrix.RowHeadersWidth = 15;
            dataGridViewCellStyle28.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvIncomeMatrix.RowsDefaultCellStyle = dataGridViewCellStyle28;
            this.dgvIncomeMatrix.Size = new System.Drawing.Size(976, 368);
            this.dgvIncomeMatrix.TabIndex = 2;
            this.dgvIncomeMatrix.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvIncomeMatrix_CellEndEdit);
            this.dgvIncomeMatrix.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvIncomeMatrix_CellValidating);
            // 
            // clmCat
            // 
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clmCat.DefaultCellStyle = dataGridViewCellStyle15;
            this.clmCat.HeaderText = "Cat.";
            this.clmCat.Name = "clmCat";
            this.clmCat.Width = 40;
            // 
            // clmDesc
            // 
            dataGridViewCellStyle16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clmDesc.DefaultCellStyle = dataGridViewCellStyle16;
            this.clmDesc.HeaderText = "Description";
            this.clmDesc.Name = "clmDesc";
            // 
            // clmLowHi
            // 
            dataGridViewCellStyle17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clmLowHi.DefaultCellStyle = dataGridViewCellStyle17;
            this.clmLowHi.HeaderText = "Low/Hi";
            this.clmLowHi.Name = "clmLowHi";
            this.clmLowHi.ReadOnly = true;
            this.clmLowHi.Width = 60;
            // 
            // clmInc1
            // 
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle18.Format = "C0";
            dataGridViewCellStyle18.NullValue = "0";
            this.clmInc1.DefaultCellStyle = dataGridViewCellStyle18;
            this.clmInc1.HeaderText = "1 Person";
            this.clmInc1.Name = "clmInc1";
            this.clmInc1.Width = 77;
            // 
            // clmInc2
            // 
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle19.Format = "C0";
            dataGridViewCellStyle19.NullValue = "0";
            this.clmInc2.DefaultCellStyle = dataGridViewCellStyle19;
            this.clmInc2.HeaderText = "2  Persons";
            this.clmInc2.Name = "clmInc2";
            this.clmInc2.Width = 77;
            // 
            // clmInc3
            // 
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle20.Format = "C0";
            dataGridViewCellStyle20.NullValue = "0";
            this.clmInc3.DefaultCellStyle = dataGridViewCellStyle20;
            this.clmInc3.HeaderText = "3 Persons";
            this.clmInc3.Name = "clmInc3";
            this.clmInc3.Width = 77;
            // 
            // clmInc4
            // 
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle21.Format = "C0";
            dataGridViewCellStyle21.NullValue = "0";
            this.clmInc4.DefaultCellStyle = dataGridViewCellStyle21;
            this.clmInc4.HeaderText = "4  Persons";
            this.clmInc4.Name = "clmInc4";
            this.clmInc4.Width = 77;
            // 
            // clmInc5
            // 
            dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle22.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle22.Format = "C0";
            dataGridViewCellStyle22.NullValue = "0";
            this.clmInc5.DefaultCellStyle = dataGridViewCellStyle22;
            this.clmInc5.HeaderText = "5 Persons";
            this.clmInc5.Name = "clmInc5";
            this.clmInc5.Width = 77;
            // 
            // clmInc6
            // 
            dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle23.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle23.Format = "C0";
            dataGridViewCellStyle23.NullValue = "0";
            this.clmInc6.DefaultCellStyle = dataGridViewCellStyle23;
            this.clmInc6.HeaderText = "6 Persons";
            this.clmInc6.Name = "clmInc6";
            this.clmInc6.Width = 77;
            // 
            // clmInc7
            // 
            dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle24.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle24.Format = "C0";
            dataGridViewCellStyle24.NullValue = "0";
            this.clmInc7.DefaultCellStyle = dataGridViewCellStyle24;
            this.clmInc7.HeaderText = "7 Persons";
            this.clmInc7.Name = "clmInc7";
            this.clmInc7.Width = 77;
            // 
            // clmInc8
            // 
            dataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle25.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle25.Format = "C0";
            this.clmInc8.DefaultCellStyle = dataGridViewCellStyle25;
            this.clmInc8.HeaderText = "8 Persons";
            this.clmInc8.Name = "clmInc8";
            this.clmInc8.Width = 77;
            // 
            // clmInc9
            // 
            dataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle26.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle26.Format = "C0";
            this.clmInc9.DefaultCellStyle = dataGridViewCellStyle26;
            this.clmInc9.HeaderText = "9 Persons";
            this.clmInc9.Name = "clmInc9";
            this.clmInc9.Width = 77;
            // 
            // clmInc10
            // 
            dataGridViewCellStyle27.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle27.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle27.Format = "C0";
            this.clmInc10.DefaultCellStyle = dataGridViewCellStyle27;
            this.clmInc10.HeaderText = "10 Persons";
            this.clmInc10.Name = "clmInc10";
            this.clmInc10.Width = 79;
            // 
            // clmID
            // 
            this.clmID.HeaderText = "ID";
            this.clmID.Name = "clmID";
            this.clmID.ReadOnly = true;
            this.clmID.Visible = false;
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(560, 24);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(136, 32);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save Changes";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(848, 24);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(136, 32);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(704, 24);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(136, 32);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel Changes";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Visible = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(988, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNewMatrixToolStripMenuItem,
            this.toolStripSeparator1,
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // addNewMatrixToolStripMenuItem
            // 
            this.addNewMatrixToolStripMenuItem.Name = "addNewMatrixToolStripMenuItem";
            this.addNewMatrixToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.addNewMatrixToolStripMenuItem.Text = "Add New Matrix";
            this.addNewMatrixToolStripMenuItem.Click += new System.EventHandler(this.addNewMatrixToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(156, 6);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // IncomeMatrixForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Cornsilk;
            this.ClientSize = new System.Drawing.Size(988, 474);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dgvIncomeMatrix);
            this.Controls.Add(this.cboMatrixGroups);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "IncomeMatrixForm";
            this.Text = "IncomeMatrixForm";
            ((System.ComponentModel.ISupportInitialize)(this.dgvIncomeMatrix)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboMatrixGroups;
        private System.Windows.Forms.DataGridView dgvIncomeMatrix;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmCat;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmDesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmLowHi;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmInc1;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmInc2;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmInc3;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmInc4;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmInc5;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmInc6;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmInc7;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmInc8;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmInc9;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmInc10;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmID;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addNewMatrixToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
    }
}