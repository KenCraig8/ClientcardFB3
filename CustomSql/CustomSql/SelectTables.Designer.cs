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
            this.SuspendLayout();
            // 
            // lstSelectTable
            // 
            this.lstSelectTable.FormattingEnabled = true;
            this.lstSelectTable.ItemHeight = 16;
            this.lstSelectTable.Location = new System.Drawing.Point(12, 12);
            this.lstSelectTable.Name = "lstSelectTable";
            this.lstSelectTable.Size = new System.Drawing.Size(379, 100);
            this.lstSelectTable.TabIndex = 1;
            this.lstSelectTable.SelectedValueChanged += new System.EventHandler(this.lstSelectTable_SelectedValueChanged);
            // 
            // btnSelectWhere
            // 
            this.btnSelectWhere.Location = new System.Drawing.Point(106, 374);
            this.btnSelectWhere.Name = "btnSelectWhere";
            this.btnSelectWhere.Size = new System.Drawing.Size(135, 32);
            this.btnSelectWhere.TabIndex = 2;
            this.btnSelectWhere.Text = "select conditions";
            this.btnSelectWhere.UseVisualStyleBackColor = true;
            this.btnSelectWhere.Click += new System.EventHandler(this.btnSelectWhere_Click);
            // 
            // lstSelectColumns
            // 
            this.lstSelectColumns.FormattingEnabled = true;
            this.lstSelectColumns.ItemHeight = 16;
            this.lstSelectColumns.Location = new System.Drawing.Point(13, 119);
            this.lstSelectColumns.Name = "lstSelectColumns";
            this.lstSelectColumns.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstSelectColumns.Size = new System.Drawing.Size(378, 196);
            this.lstSelectColumns.TabIndex = 3;
            // 
            // SelectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(562, 437);
            this.Controls.Add(this.lstSelectColumns);
            this.Controls.Add(this.btnSelectWhere);
            this.Controls.Add(this.lstSelectTable);
            this.Name = "SelectForm";
            this.Text = "Select Tables";
            this.Load += new System.EventHandler(this.SelectForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ListBox lstSelectTable;
        private System.Windows.Forms.Button btnSelectWhere;
        private System.Windows.Forms.ListBox lstSelectColumns;

    }
}

