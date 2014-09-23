namespace ClientcardFB3
{
    partial class BackupDBForm
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
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.label3 = new System.Windows.Forms.Label();
            this.tbServerName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbSvrPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbSavePath = new System.Windows.Forms.TextBox();
            this.btnGetSavePath = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label4 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(165, 146);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(85, 28);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(4, 9);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Server Name";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbServerName
            // 
            this.tbServerName.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbServerName.ForeColor = System.Drawing.Color.MidnightBlue;
            this.tbServerName.Location = new System.Drawing.Point(95, 7);
            this.tbServerName.Margin = new System.Windows.Forms.Padding(4);
            this.tbServerName.MaxLength = 50;
            this.tbServerName.Name = "tbServerName";
            this.tbServerName.Size = new System.Drawing.Size(329, 23);
            this.tbServerName.TabIndex = 11;
            this.tbServerName.Tag = "Address";
            this.tbServerName.Text = "12345678901234567890123456789012345678901234567890";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 49);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Online Location:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbSvrPath
            // 
            this.tbSvrPath.Font = new System.Drawing.Font("Lucida Console", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbSvrPath.ForeColor = System.Drawing.Color.MidnightBlue;
            this.tbSvrPath.Location = new System.Drawing.Point(7, 66);
            this.tbSvrPath.Margin = new System.Windows.Forms.Padding(4);
            this.tbSvrPath.MaxLength = 50;
            this.tbSvrPath.Name = "tbSvrPath";
            this.tbSvrPath.Size = new System.Drawing.Size(417, 21);
            this.tbSvrPath.TabIndex = 13;
            this.tbSvrPath.Tag = "Address";
            this.tbSvrPath.Text = "12345678901234567890123456789012345678901234567890";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(4, 101);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Offline Location:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbSavePath
            // 
            this.tbSavePath.Font = new System.Drawing.Font("Lucida Console", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbSavePath.ForeColor = System.Drawing.Color.MidnightBlue;
            this.tbSavePath.Location = new System.Drawing.Point(7, 118);
            this.tbSavePath.Margin = new System.Windows.Forms.Padding(4);
            this.tbSavePath.MaxLength = 50;
            this.tbSavePath.Name = "tbSavePath";
            this.tbSavePath.Size = new System.Drawing.Size(417, 21);
            this.tbSavePath.TabIndex = 15;
            this.tbSavePath.Tag = "Address";
            this.tbSavePath.Text = "12345678901234567890123456789012345678901234567890";
            // 
            // btnGetSavePath
            // 
            this.btnGetSavePath.Location = new System.Drawing.Point(429, 119);
            this.btnGetSavePath.Name = "btnGetSavePath";
            this.btnGetSavePath.Size = new System.Drawing.Size(25, 19);
            this.btnGetSavePath.TabIndex = 17;
            this.btnGetSavePath.Text = "- -";
            this.btnGetSavePath.UseVisualStyleBackColor = true;
            this.btnGetSavePath.Click += new System.EventHandler(this.btnGetSavePath_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(19, 190);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(404, 29);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 18;
            this.progressBar1.Visible = false;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DarkRed;
            this.label4.Location = new System.Drawing.Point(20, 229);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(402, 47);
            this.label4.TabIndex = 19;
            this.label4.Text = "Perrforming SQL Database Backup to Server";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(184, 190);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(55, 29);
            this.btnOk.TabIndex = 20;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // BackupDBForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 283);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnGetSavePath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbSavePath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbSvrPath);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbServerName);
            this.Controls.Add(this.btnStart);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BackupDBForm";
            this.Text = "Backup ClientcardFB3 SQL Database";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbServerName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbSvrPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbSavePath;
        private System.Windows.Forms.Button btnGetSavePath;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnOk;
    }
}