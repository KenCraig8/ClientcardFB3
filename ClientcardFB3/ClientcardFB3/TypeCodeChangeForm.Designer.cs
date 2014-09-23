namespace ClientcardFB3
{
    partial class TypeCodeChangeForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnSelect = new System.Windows.Forms.Button();
            this.dgvTypeCodes = new System.Windows.Forms.DataGridView();
            this.clmId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmShortName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmSortOrder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTypeCodes)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 34);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(202, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Please Select New Type Code:";
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(253, 5);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(83, 40);
            this.btnSelect.TabIndex = 1;
            this.btnSelect.Text = "Select";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // dgvTypeCodes
            // 
            this.dgvTypeCodes.AllowUserToAddRows = false;
            this.dgvTypeCodes.AllowUserToDeleteRows = false;
            this.dgvTypeCodes.AllowUserToOrderColumns = true;
            this.dgvTypeCodes.AllowUserToResizeRows = false;
            this.dgvTypeCodes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTypeCodes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmId,
            this.clmDescription,
            this.clmShortName,
            this.clmSortOrder});
            this.dgvTypeCodes.Location = new System.Drawing.Point(4, 56);
            this.dgvTypeCodes.MultiSelect = false;
            this.dgvTypeCodes.Name = "dgvTypeCodes";
            this.dgvTypeCodes.RowHeadersVisible = false;
            this.dgvTypeCodes.Size = new System.Drawing.Size(420, 264);
            this.dgvTypeCodes.TabIndex = 2;
            // 
            // clmId
            // 
            this.clmId.HeaderText = "ID";
            this.clmId.Name = "clmId";
            this.clmId.Visible = false;
            // 
            // clmDescription
            // 
            this.clmDescription.HeaderText = "Type Name";
            this.clmDescription.Name = "clmDescription";
            this.clmDescription.Width = 300;
            // 
            // clmShortName
            // 
            this.clmShortName.HeaderText = "Short Name";
            this.clmShortName.Name = "clmShortName";
            this.clmShortName.Width = 60;
            // 
            // clmSortOrder
            // 
            this.clmSortOrder.HeaderText = "Sort Order";
            this.clmSortOrder.Name = "clmSortOrder";
            this.clmSortOrder.Width = 60;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(339, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(83, 40);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // TypeCodeChangeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Cornsilk;
            this.ClientSize = new System.Drawing.Size(425, 322);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.dgvTypeCodes);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "TypeCodeChangeForm";
            this.Text = "TypeCodeChangeForm";
            ((System.ComponentModel.ISupportInitialize)(this.dgvTypeCodes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.DataGridView dgvTypeCodes;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmId;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmShortName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmSortOrder;
        private System.Windows.Forms.Button btnCancel;
    }
}