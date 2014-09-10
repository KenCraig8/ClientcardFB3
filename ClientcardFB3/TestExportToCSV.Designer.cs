namespace ClientcardFB3
{
    partial class TestExportToCSV
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
            this.btnStart = new System.Windows.Forms.Button();
            this.tbSqlTextFile = new System.Windows.Forms.TextBox();
            this.tbFormatFile = new System.Windows.Forms.TextBox();
            this.tbRptQueryPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(240, 124);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(100, 56);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "&Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // tbSqlTextFile
            // 
            this.tbSqlTextFile.Location = new System.Drawing.Point(192, 65);
            this.tbSqlTextFile.Name = "tbSqlTextFile";
            this.tbSqlTextFile.Size = new System.Drawing.Size(221, 20);
            this.tbSqlTextFile.TabIndex = 1;
            this.tbSqlTextFile.Text = "SchSupplyListing.sql";
            // 
            // tbFormatFile
            // 
            this.tbFormatFile.Location = new System.Drawing.Point(192, 89);
            this.tbFormatFile.Name = "tbFormatFile";
            this.tbFormatFile.Size = new System.Drawing.Size(221, 20);
            this.tbFormatFile.TabIndex = 2;
            this.tbFormatFile.Text = "SchSupplyListing.fmt";
            // 
            // tbRptQueryPath
            // 
            this.tbRptQueryPath.Location = new System.Drawing.Point(19, 30);
            this.tbRptQueryPath.Name = "tbRptQueryPath";
            this.tbRptQueryPath.Size = new System.Drawing.Size(516, 20);
            this.tbRptQueryPath.TabIndex = 3;
            this.tbRptQueryPath.Text = "C:\\ClientcardFB3\\CustomQuery\\";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Custom Query Definition Path";
            // 
            // TestExportToCSV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 199);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbRptQueryPath);
            this.Controls.Add(this.tbFormatFile);
            this.Controls.Add(this.tbSqlTextFile);
            this.Controls.Add(this.btnStart);
            this.Name = "TestExportToCSV";
            this.Text = "TestExportToCSV";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TextBox tbSqlTextFile;
        private System.Windows.Forms.TextBox tbFormatFile;
        private System.Windows.Forms.TextBox tbRptQueryPath;
        private System.Windows.Forms.Label label1;
    }
}