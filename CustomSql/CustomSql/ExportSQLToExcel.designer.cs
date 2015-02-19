namespace ClientcardFB3
{
    partial class ExportSQLToExcel
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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.label2 = new System.Windows.Forms.Label();
            this.btnGetResultsPath = new System.Windows.Forms.Button();
            this.tbSQLResultsPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGetQueryPath = new System.Windows.Forms.Button();
            this.tbSQLQueryPath = new System.Windows.Forms.TextBox();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.btnSaveSQLChanges = new System.Windows.Forms.Button();
            this.lblSqlFile = new System.Windows.Forms.Label();
            this.rtbSQL = new System.Windows.Forms.RichTextBox();
            this.splitContainer5 = new System.Windows.Forms.SplitContainer();
            this.btnSaveFormatChanges = new System.Windows.Forms.Button();
            this.lblFormatFile = new System.Windows.Forms.Label();
            this.rtbFMT = new System.Windows.Forms.RichTextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).BeginInit();
            this.splitContainer5.Panel1.SuspendLayout();
            this.splitContainer5.Panel2.SuspendLayout();
            this.splitContainer5.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.AutoSize = true;
            this.btnStart.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnStart.Location = new System.Drawing.Point(0, 170);
            this.btnStart.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(540, 51);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "&Run Query and Create Spreradsheet";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // listBox1
            // 
            this.listBox1.BackColor = System.Drawing.Color.LightYellow;
            this.listBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 18;
            this.listBox1.Location = new System.Drawing.Point(0, 0);
            this.listBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(503, 221);
            this.listBox1.TabIndex = 19;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.Sienna;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel1MinSize = 180;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer1.Panel2MinSize = 200;
            this.splitContainer1.Size = new System.Drawing.Size(1047, 595);
            this.splitContainer1.SplitterDistance = 221;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 20;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BackColor = System.Drawing.Color.SaddleBrown;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.BackColor = System.Drawing.Color.LightGray;
            this.splitContainer2.Panel1.Controls.Add(this.label2);
            this.splitContainer2.Panel1.Controls.Add(this.btnGetResultsPath);
            this.splitContainer2.Panel1.Controls.Add(this.tbSQLResultsPath);
            this.splitContainer2.Panel1.Controls.Add(this.btnStart);
            this.splitContainer2.Panel1.Controls.Add(this.label1);
            this.splitContainer2.Panel1.Controls.Add(this.btnGetQueryPath);
            this.splitContainer2.Panel1.Controls.Add(this.tbSQLQueryPath);
            this.splitContainer2.Panel1MinSize = 300;
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.BackColor = System.Drawing.Color.Gainsboro;
            this.splitContainer2.Panel2.Controls.Add(this.listBox1);
            this.splitContainer2.Panel2MinSize = 100;
            this.splitContainer2.Size = new System.Drawing.Size(1047, 221);
            this.splitContainer2.SplitterDistance = 540;
            this.splitContainer2.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 63);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 17);
            this.label2.TabIndex = 23;
            this.label2.Text = "Excel Results Path";
            // 
            // btnGetResultsPath
            // 
            this.btnGetResultsPath.Font = new System.Drawing.Font("Arial Narrow", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetResultsPath.Location = new System.Drawing.Point(441, 83);
            this.btnGetResultsPath.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnGetResultsPath.Name = "btnGetResultsPath";
            this.btnGetResultsPath.Size = new System.Drawing.Size(34, 23);
            this.btnGetResultsPath.TabIndex = 24;
            this.btnGetResultsPath.Text = ">>";
            this.btnGetResultsPath.UseVisualStyleBackColor = true;
            this.btnGetResultsPath.Click += new System.EventHandler(this.btnGetResultsPath_Click);
            // 
            // tbSQLResultsPath
            // 
            this.tbSQLResultsPath.Location = new System.Drawing.Point(7, 84);
            this.tbSQLResultsPath.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbSQLResultsPath.Name = "tbSQLResultsPath";
            this.tbSQLResultsPath.Size = new System.Drawing.Size(427, 22);
            this.tbSQLResultsPath.TabIndex = 22;
            this.tbSQLResultsPath.Text = "C:\\ClientcardFB3\\CustomQuery\\";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 10);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 17);
            this.label1.TabIndex = 20;
            this.label1.Text = "SQL Query Path";
            // 
            // btnGetQueryPath
            // 
            this.btnGetQueryPath.Font = new System.Drawing.Font("Arial Narrow", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetQueryPath.Location = new System.Drawing.Point(441, 30);
            this.btnGetQueryPath.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnGetQueryPath.Name = "btnGetQueryPath";
            this.btnGetQueryPath.Size = new System.Drawing.Size(34, 23);
            this.btnGetQueryPath.TabIndex = 21;
            this.btnGetQueryPath.Text = ">>";
            this.btnGetQueryPath.UseVisualStyleBackColor = true;
            this.btnGetQueryPath.Click += new System.EventHandler(this.btnGetQueryPath_Click);
            // 
            // tbSQLQueryPath
            // 
            this.tbSQLQueryPath.Location = new System.Drawing.Point(7, 30);
            this.tbSQLQueryPath.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbSQLQueryPath.Name = "tbSQLQueryPath";
            this.tbSQLQueryPath.Size = new System.Drawing.Size(427, 22);
            this.tbSQLQueryPath.TabIndex = 19;
            this.tbSQLQueryPath.Text = "C:\\ClientcardFB3\\CustomQuery\\";
            // 
            // splitContainer3
            // 
            this.splitContainer3.BackColor = System.Drawing.Color.SaddleBrown;
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.splitContainer4);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.splitContainer5);
            this.splitContainer3.Size = new System.Drawing.Size(1047, 368);
            this.splitContainer3.SplitterDistance = 532;
            this.splitContainer3.SplitterWidth = 5;
            this.splitContainer3.TabIndex = 0;
            // 
            // splitContainer4
            // 
            this.splitContainer4.BackColor = System.Drawing.Color.Tan;
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.btnSaveSQLChanges);
            this.splitContainer4.Panel1.Controls.Add(this.lblSqlFile);
            this.splitContainer4.Panel1MinSize = 40;
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.rtbSQL);
            this.splitContainer4.Panel2MinSize = 100;
            this.splitContainer4.Size = new System.Drawing.Size(532, 368);
            this.splitContainer4.SplitterDistance = 72;
            this.splitContainer4.SplitterWidth = 3;
            this.splitContainer4.TabIndex = 0;
            // 
            // btnSaveSQLChanges
            // 
            this.btnSaveSQLChanges.BackColor = System.Drawing.Color.LightGray;
            this.btnSaveSQLChanges.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnSaveSQLChanges.Location = new System.Drawing.Point(0, 40);
            this.btnSaveSQLChanges.Margin = new System.Windows.Forms.Padding(3, 2, 3, 0);
            this.btnSaveSQLChanges.Name = "btnSaveSQLChanges";
            this.btnSaveSQLChanges.Size = new System.Drawing.Size(532, 32);
            this.btnSaveSQLChanges.TabIndex = 4;
            this.btnSaveSQLChanges.Text = "Save SQL File Changes";
            this.btnSaveSQLChanges.UseVisualStyleBackColor = false;
            this.btnSaveSQLChanges.Visible = false;
            this.btnSaveSQLChanges.Click += new System.EventHandler(this.btnSaveSQLChanges_Click);
            // 
            // lblSqlFile
            // 
            this.lblSqlFile.BackColor = System.Drawing.Color.Tan;
            this.lblSqlFile.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSqlFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSqlFile.Location = new System.Drawing.Point(0, 0);
            this.lblSqlFile.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblSqlFile.Name = "lblSqlFile";
            this.lblSqlFile.Size = new System.Drawing.Size(532, 30);
            this.lblSqlFile.TabIndex = 5;
            this.lblSqlFile.Text = "super";
            this.lblSqlFile.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // rtbSQL
            // 
            this.rtbSQL.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbSQL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbSQL.Font = new System.Drawing.Font("Lucida Console", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbSQL.Location = new System.Drawing.Point(0, 0);
            this.rtbSQL.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rtbSQL.Name = "rtbSQL";
            this.rtbSQL.Size = new System.Drawing.Size(532, 293);
            this.rtbSQL.TabIndex = 0;
            this.rtbSQL.Text = "";
            this.rtbSQL.TextChanged += new System.EventHandler(this.rtbSQL_TextChanged);
            // 
            // splitContainer5
            // 
            this.splitContainer5.BackColor = System.Drawing.Color.Tan;
            this.splitContainer5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer5.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer5.IsSplitterFixed = true;
            this.splitContainer5.Location = new System.Drawing.Point(0, 0);
            this.splitContainer5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.splitContainer5.Name = "splitContainer5";
            this.splitContainer5.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer5.Panel1
            // 
            this.splitContainer5.Panel1.Controls.Add(this.btnSaveFormatChanges);
            this.splitContainer5.Panel1.Controls.Add(this.lblFormatFile);
            this.splitContainer5.Panel1MinSize = 40;
            // 
            // splitContainer5.Panel2
            // 
            this.splitContainer5.Panel2.Controls.Add(this.rtbFMT);
            this.splitContainer5.Panel2MinSize = 100;
            this.splitContainer5.Size = new System.Drawing.Size(510, 368);
            this.splitContainer5.SplitterDistance = 76;
            this.splitContainer5.SplitterWidth = 3;
            this.splitContainer5.TabIndex = 0;
            // 
            // btnSaveFormatChanges
            // 
            this.btnSaveFormatChanges.BackColor = System.Drawing.Color.LightGray;
            this.btnSaveFormatChanges.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnSaveFormatChanges.Location = new System.Drawing.Point(0, 44);
            this.btnSaveFormatChanges.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSaveFormatChanges.Name = "btnSaveFormatChanges";
            this.btnSaveFormatChanges.Size = new System.Drawing.Size(510, 32);
            this.btnSaveFormatChanges.TabIndex = 3;
            this.btnSaveFormatChanges.Text = "Save Format File Changes";
            this.btnSaveFormatChanges.UseVisualStyleBackColor = false;
            this.btnSaveFormatChanges.Visible = false;
            this.btnSaveFormatChanges.TextChanged += new System.EventHandler(this.btnSaveFormatChanges_TextChanged);
            this.btnSaveFormatChanges.Click += new System.EventHandler(this.btnSaveFormatChanges_Click);
            // 
            // lblFormatFile
            // 
            this.lblFormatFile.BackColor = System.Drawing.Color.Tan;
            this.lblFormatFile.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblFormatFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFormatFile.Location = new System.Drawing.Point(0, 0);
            this.lblFormatFile.Name = "lblFormatFile";
            this.lblFormatFile.Size = new System.Drawing.Size(510, 30);
            this.lblFormatFile.TabIndex = 6;
            this.lblFormatFile.Text = "super";
            this.lblFormatFile.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // rtbFMT
            // 
            this.rtbFMT.BackColor = System.Drawing.Color.WhiteSmoke;
            this.rtbFMT.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbFMT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbFMT.Location = new System.Drawing.Point(0, 0);
            this.rtbFMT.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rtbFMT.Name = "rtbFMT";
            this.rtbFMT.Size = new System.Drawing.Size(510, 289);
            this.rtbFMT.TabIndex = 0;
            this.rtbFMT.Text = "";
            this.rtbFMT.TextChanged += new System.EventHandler(this.rtbFMT_TextChanged);
            // 
            // ExportSQLToExcel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1047, 595);
            this.Controls.Add(this.splitContainer1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ExportSQLToExcel";
            this.Text = "Export SQL Query to Excel File";
            this.Load += new System.EventHandler(this.ExportSQLToExcel_Load);
            this.Resize += new System.EventHandler(this.ExportSQLToExcel_Resize);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.splitContainer5.Panel1.ResumeLayout(false);
            this.splitContainer5.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).EndInit();
            this.splitContainer5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGetQueryPath;
        private System.Windows.Forms.TextBox tbSQLQueryPath;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.RichTextBox rtbSQL;
        private System.Windows.Forms.SplitContainer splitContainer5;
        private System.Windows.Forms.RichTextBox rtbFMT;
        private System.Windows.Forms.Button btnSaveSQLChanges;
        private System.Windows.Forms.Button btnSaveFormatChanges;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnGetResultsPath;
        private System.Windows.Forms.TextBox tbSQLResultsPath;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label lblSqlFile;
        private System.Windows.Forms.Label lblFormatFile;
    }
}