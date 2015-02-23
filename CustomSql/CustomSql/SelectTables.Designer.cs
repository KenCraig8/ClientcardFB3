namespace ClientcardFB3
{
    partial class SelectTables
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
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.lstSelectTable = new System.Windows.Forms.ListBox();
            this.btnSelectWhere = new System.Windows.Forms.Button();
            this.lstSelectColumns = new System.Windows.Forms.ListBox();
            this.btnExport = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstSelectTable
            // 
            this.lstSelectTable.FormattingEnabled = true;
            this.lstSelectTable.ItemHeight = 20;
            this.lstSelectTable.Location = new System.Drawing.Point(14, 15);
            this.lstSelectTable.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lstSelectTable.Name = "lstSelectTable";
            this.lstSelectTable.Size = new System.Drawing.Size(426, 124);
            this.lstSelectTable.TabIndex = 1;
            this.lstSelectTable.SelectedValueChanged += new System.EventHandler(this.lstSelectTable_SelectedValueChanged);
            // 
            // btnSelectWhere
            // 
            this.btnSelectWhere.Location = new System.Drawing.Point(119, 468);
            this.btnSelectWhere.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSelectWhere.Name = "btnSelectWhere";
            this.btnSelectWhere.Size = new System.Drawing.Size(152, 40);
            this.btnSelectWhere.TabIndex = 2;
            this.btnSelectWhere.Text = "select conditions";
            this.btnSelectWhere.UseVisualStyleBackColor = true;
            this.btnSelectWhere.Click += new System.EventHandler(this.btnSelectWhere_Click);
            // 
            // lstSelectColumns
            // 
            this.lstSelectColumns.FormattingEnabled = true;
            this.lstSelectColumns.ItemHeight = 20;
            this.lstSelectColumns.Location = new System.Drawing.Point(15, 149);
            this.lstSelectColumns.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lstSelectColumns.Name = "lstSelectColumns";
            this.lstSelectColumns.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstSelectColumns.Size = new System.Drawing.Size(425, 244);
            this.lstSelectColumns.TabIndex = 3;
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(304, 468);
            this.btnExport.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(136, 39);
            this.btnExport.TabIndex = 4;
            this.btnExport.Text = "Export to Excel";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // SelectTables
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(483, 546);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.lstSelectColumns);
            this.Controls.Add(this.btnSelectWhere);
            this.Controls.Add(this.lstSelectTable);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "SelectTables";
            this.Text = "Select Tables";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SelectTables_FormClosed);
            this.Load += new System.EventHandler(this.SelectForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ListBox lstSelectTable;
        private System.Windows.Forms.Button btnSelectWhere;
        private System.Windows.Forms.ListBox lstSelectColumns;
        private System.Windows.Forms.Button btnExport;

    }
}

