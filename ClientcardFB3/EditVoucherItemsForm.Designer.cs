namespace ClientcardFB3
{
    partial class EditVouchersItemForm
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lvVouchers = new System.Windows.Forms.ListView();
            this.clmDesc = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmNumType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmDfltAmnt = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmMaxAmnout = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tbMaxAmount = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbDefaultAmount = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.cboVoucherType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnAddNew = new System.Windows.Forms.Button();
            this.tbDesc = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lvVouchers);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tbMaxAmount);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.tbDefaultAmount);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.btnDelete);
            this.splitContainer1.Panel2.Controls.Add(this.cboVoucherType);
            this.splitContainer1.Panel2.Controls.Add(this.label5);
            this.splitContainer1.Panel2.Controls.Add(this.btnClose);
            this.splitContainer1.Panel2.Controls.Add(this.btnAddNew);
            this.splitContainer1.Panel2.Controls.Add(this.tbDesc);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Size = new System.Drawing.Size(925, 418);
            this.splitContainer1.SplitterDistance = 495;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 0;
            // 
            // lvVouchers
            // 
            this.lvVouchers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmDesc,
            this.clmType,
            this.clmID,
            this.clmNumType,
            this.clmDfltAmnt,
            this.clmMaxAmnout});
            this.lvVouchers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvVouchers.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvVouchers.FullRowSelect = true;
            this.lvVouchers.GridLines = true;
            this.lvVouchers.HideSelection = false;
            this.lvVouchers.Location = new System.Drawing.Point(0, 0);
            this.lvVouchers.Margin = new System.Windows.Forms.Padding(4);
            this.lvVouchers.MultiSelect = false;
            this.lvVouchers.Name = "lvVouchers";
            this.lvVouchers.Size = new System.Drawing.Size(495, 418);
            this.lvVouchers.TabIndex = 0;
            this.lvVouchers.UseCompatibleStateImageBehavior = false;
            this.lvVouchers.View = System.Windows.Forms.View.Details;
            this.lvVouchers.SelectedIndexChanged += new System.EventHandler(this.lvVouchers_SelectedIndexChanged);
            // 
            // clmDesc
            // 
            this.clmDesc.Text = "Description";
            this.clmDesc.Width = 160;
            // 
            // clmType
            // 
            this.clmType.Text = "Voucher Type";
            this.clmType.Width = 130;
            // 
            // clmID
            // 
            this.clmID.Text = "ID";
            this.clmID.Width = 0;
            // 
            // clmNumType
            // 
            this.clmNumType.Width = 0;
            // 
            // clmDfltAmnt
            // 
            this.clmDfltAmnt.Text = "Default Amount";
            this.clmDfltAmnt.Width = 100;
            // 
            // clmMaxAmnout
            // 
            this.clmMaxAmnout.Text = "Max Amount";
            this.clmMaxAmnout.Width = 100;
            // 
            // tbMaxAmount
            // 
            this.tbMaxAmount.Font = new System.Drawing.Font("Verdana", 10F);
            this.tbMaxAmount.Location = new System.Drawing.Point(145, 182);
            this.tbMaxAmount.Margin = new System.Windows.Forms.Padding(4);
            this.tbMaxAmount.MaxLength = 50;
            this.tbMaxAmount.Name = "tbMaxAmount";
            this.tbMaxAmount.Size = new System.Drawing.Size(88, 24);
            this.tbMaxAmount.TabIndex = 44;
            this.tbMaxAmount.Tag = "MaxAmount";
            this.tbMaxAmount.Enter += new System.EventHandler(this.tb_Enter);
            this.tbMaxAmount.Leave += new System.EventHandler(this.tb_Leave);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Verdana", 9F);
            this.label3.Location = new System.Drawing.Point(8, 184);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 20);
            this.label3.TabIndex = 43;
            this.label3.Tag = "";
            this.label3.Text = "Max Amount:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbDefaultAmount
            // 
            this.tbDefaultAmount.Font = new System.Drawing.Font("Verdana", 10F);
            this.tbDefaultAmount.Location = new System.Drawing.Point(145, 150);
            this.tbDefaultAmount.Margin = new System.Windows.Forms.Padding(4);
            this.tbDefaultAmount.MaxLength = 50;
            this.tbDefaultAmount.Name = "tbDefaultAmount";
            this.tbDefaultAmount.Size = new System.Drawing.Size(88, 24);
            this.tbDefaultAmount.TabIndex = 42;
            this.tbDefaultAmount.Tag = "DefaulAmount";
            this.tbDefaultAmount.Enter += new System.EventHandler(this.tb_Enter);
            this.tbDefaultAmount.Leave += new System.EventHandler(this.tb_Leave);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Verdana", 9F);
            this.label2.Location = new System.Drawing.Point(8, 152);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 20);
            this.label2.TabIndex = 41;
            this.label2.Text = "Default Amount:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(169, 16);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(120, 30);
            this.btnDelete.TabIndex = 40;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // cboVoucherType
            // 
            this.cboVoucherType.FormattingEnabled = true;
            this.cboVoucherType.Location = new System.Drawing.Point(145, 117);
            this.cboVoucherType.Margin = new System.Windows.Forms.Padding(4);
            this.cboVoucherType.Name = "cboVoucherType";
            this.cboVoucherType.Size = new System.Drawing.Size(272, 24);
            this.cboVoucherType.TabIndex = 39;
            this.cboVoucherType.Tag = "parm_IncomeProcessID";
            this.cboVoucherType.SelectedValueChanged += new System.EventHandler(this.cboVoucherType_SelectedValueChanged);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Verdana", 9F);
            this.label5.Location = new System.Drawing.Point(8, 118);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(133, 23);
            this.label5.TabIndex = 38;
            this.label5.Text = "Voucher Type:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(303, 17);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(107, 30);
            this.btnClose.TabIndex = 37;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAddNew
            // 
            this.btnAddNew.Location = new System.Drawing.Point(35, 15);
            this.btnAddNew.Margin = new System.Windows.Forms.Padding(4);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(120, 30);
            this.btnAddNew.TabIndex = 36;
            this.btnAddNew.Text = "Add New";
            this.btnAddNew.UseVisualStyleBackColor = true;
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // tbDesc
            // 
            this.tbDesc.Font = new System.Drawing.Font("Verdana", 10F);
            this.tbDesc.Location = new System.Drawing.Point(145, 78);
            this.tbDesc.Margin = new System.Windows.Forms.Padding(4);
            this.tbDesc.MaxLength = 50;
            this.tbDesc.Name = "tbDesc";
            this.tbDesc.Size = new System.Drawing.Size(272, 24);
            this.tbDesc.TabIndex = 35;
            this.tbDesc.Tag = "ItemDesc";
            this.tbDesc.Enter += new System.EventHandler(this.tb_Enter);
            this.tbDesc.Leave += new System.EventHandler(this.tb_Leave);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Verdana", 9F);
            this.label1.Location = new System.Drawing.Point(8, 80);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 20);
            this.label1.TabIndex = 34;
            this.label1.Text = "Descripton:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // EditVouchersItemForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Cornsilk;
            this.ClientSize = new System.Drawing.Size(925, 418);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "EditVouchersItemForm";
            this.Text = "Edit Vouchers Form";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView lvVouchers;
        private System.Windows.Forms.ColumnHeader clmDesc;
        private System.Windows.Forms.ColumnHeader clmType;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.ComboBox cboVoucherType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnAddNew;
        private System.Windows.Forms.TextBox tbDesc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ColumnHeader clmID;
        private System.Windows.Forms.ColumnHeader clmNumType;
        private System.Windows.Forms.ColumnHeader clmDfltAmnt;
        private System.Windows.Forms.ColumnHeader clmMaxAmnout;
        private System.Windows.Forms.TextBox tbMaxAmount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbDefaultAmount;
        private System.Windows.Forms.Label label2;

    }
}