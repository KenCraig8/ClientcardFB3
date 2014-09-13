namespace CustomSQL
{
    partial class CustSqlWhere
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
            this.gvPreview = new System.Windows.Forms.DataGridView();
            this.btnLoadData = new System.Windows.Forms.Button();
            this.tblWhereSelection = new System.Windows.Forms.TableLayoutPanel();
            this.btnSave = new System.Windows.Forms.Button();
            this.sfdSaveQuery = new System.Windows.Forms.SaveFileDialog();
            this.flpStringSelect = new System.Windows.Forms.FlowLayoutPanel();
            this.lstOrder = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.gvPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // gvPreview
            // 
            this.gvPreview.Location = new System.Drawing.Point(831, 12);
            this.gvPreview.Name = "gvPreview";
            this.gvPreview.Size = new System.Drawing.Size(467, 444);
            this.gvPreview.TabIndex = 1;
            // 
            // btnLoadData
            // 
            this.btnLoadData.Location = new System.Drawing.Point(192, 462);
            this.btnLoadData.Name = "btnLoadData";
            this.btnLoadData.Size = new System.Drawing.Size(128, 30);
            this.btnLoadData.TabIndex = 2;
            this.btnLoadData.Text = "Load Data";
            this.btnLoadData.UseVisualStyleBackColor = true;
            this.btnLoadData.Click += new System.EventHandler(this.btnLoadData_Click);
            // 
            // tblWhereSelection
            // 
            this.tblWhereSelection.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tblWhereSelection.ColumnCount = 6;
            this.tblWhereSelection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblWhereSelection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblWhereSelection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblWhereSelection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblWhereSelection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tblWhereSelection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 777F));
            this.tblWhereSelection.Location = new System.Drawing.Point(169, 12);
            this.tblWhereSelection.MinimumSize = new System.Drawing.Size(0, 200);
            this.tblWhereSelection.Name = "tblWhereSelection";
            this.tblWhereSelection.RowCount = 1;
            this.tblWhereSelection.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblWhereSelection.Size = new System.Drawing.Size(656, 227);
            this.tblWhereSelection.TabIndex = 3;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(326, 462);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(138, 31);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save Statement";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // sfdSaveQuery
            // 
            this.sfdSaveQuery.DefaultExt = "txt";
            // 
            // flpStringSelect
            // 
            this.flpStringSelect.Location = new System.Drawing.Point(169, 246);
            this.flpStringSelect.Name = "flpStringSelect";
            this.flpStringSelect.Size = new System.Drawing.Size(656, 210);
            this.flpStringSelect.TabIndex = 5;
            // 
            // lstOrder
            // 
            this.lstOrder.AllowDrop = true;
            this.lstOrder.FormattingEnabled = true;
            this.lstOrder.ItemHeight = 16;
            this.lstOrder.Location = new System.Drawing.Point(13, 13);
            this.lstOrder.Name = "lstOrder";
            this.lstOrder.Size = new System.Drawing.Size(150, 436);
            this.lstOrder.TabIndex = 6;
            this.lstOrder.DragDrop += new System.Windows.Forms.DragEventHandler(this.lstOrder_DragDrop);
            this.lstOrder.DragOver += new System.Windows.Forms.DragEventHandler(this.lstOrder_DragOver);
            this.lstOrder.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lstOrder_MouseDown);
            // 
            // CustSqlWhere
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1310, 549);
            this.Controls.Add(this.lstOrder);
            this.Controls.Add(this.flpStringSelect);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tblWhereSelection);
            this.Controls.Add(this.btnLoadData);
            this.Controls.Add(this.gvPreview);
            this.Name = "CustSqlWhere";
            this.Text = "CustSqlWhere";
            this.Load += new System.EventHandler(this.CustSqlWhere_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvPreview)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gvPreview;
        private System.Windows.Forms.Button btnLoadData;
        private System.Windows.Forms.TableLayoutPanel tblWhereSelection;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.SaveFileDialog sfdSaveQuery;
        private System.Windows.Forms.FlowLayoutPanel flpStringSelect;
        private System.Windows.Forms.ListBox lstOrder;
    }
}