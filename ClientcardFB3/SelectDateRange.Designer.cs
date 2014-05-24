namespace ClientcardFB3
{
    partial class SelectDateRange
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpLast = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpFirst = new System.Windows.Forms.DateTimePicker();
            this.rdoCustom = new System.Windows.Forms.RadioButton();
            this.rdoPrevCal = new System.Windows.Forms.RadioButton();
            this.rdoPrevFiscal = new System.Windows.Forms.RadioButton();
            this.rdoCurCal = new System.Windows.Forms.RadioButton();
            this.rdoCurFiscal = new System.Windows.Forms.RadioButton();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dtpLast);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dtpFirst);
            this.groupBox1.Controls.Add(this.rdoCustom);
            this.groupBox1.Controls.Add(this.rdoPrevCal);
            this.groupBox1.Controls.Add(this.rdoPrevFiscal);
            this.groupBox1.Controls.Add(this.rdoCurCal);
            this.groupBox1.Controls.Add(this.rdoCurFiscal);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(11, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(277, 228);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Service Date Range";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 195);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 15);
            this.label2.TabIndex = 8;
            this.label2.Text = "Last";
            // 
            // dtpLast
            // 
            this.dtpLast.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpLast.Location = new System.Drawing.Point(52, 193);
            this.dtpLast.Name = "dtpLast";
            this.dtpLast.Size = new System.Drawing.Size(212, 20);
            this.dtpLast.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 170);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "First";
            // 
            // dtpFirst
            // 
            this.dtpFirst.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFirst.Location = new System.Drawing.Point(52, 168);
            this.dtpFirst.Name = "dtpFirst";
            this.dtpFirst.Size = new System.Drawing.Size(212, 20);
            this.dtpFirst.TabIndex = 7;
            // 
            // rdoCustom
            // 
            this.rdoCustom.AutoSize = true;
            this.rdoCustom.Location = new System.Drawing.Point(24, 140);
            this.rdoCustom.Name = "rdoCustom";
            this.rdoCustom.Size = new System.Drawing.Size(153, 21);
            this.rdoCustom.TabIndex = 5;
            this.rdoCustom.Tag = "4";
            this.rdoCustom.Text = "Custom Date Range";
            this.rdoCustom.UseVisualStyleBackColor = true;
            this.rdoCustom.CheckedChanged += new System.EventHandler(this.rdoRangeType_CheckedChanged);
            // 
            // rdoPrevCal
            // 
            this.rdoPrevCal.AutoSize = true;
            this.rdoPrevCal.Location = new System.Drawing.Point(24, 112);
            this.rdoPrevCal.Name = "rdoPrevCal";
            this.rdoPrevCal.Size = new System.Drawing.Size(176, 21);
            this.rdoPrevCal.TabIndex = 4;
            this.rdoPrevCal.Tag = "3";
            this.rdoPrevCal.Text = "Previous Calendar Year";
            this.rdoPrevCal.UseVisualStyleBackColor = true;
            this.rdoPrevCal.CheckedChanged += new System.EventHandler(this.rdoRangeType_CheckedChanged);
            // 
            // rdoPrevFiscal
            // 
            this.rdoPrevFiscal.AutoSize = true;
            this.rdoPrevFiscal.Location = new System.Drawing.Point(24, 84);
            this.rdoPrevFiscal.Name = "rdoPrevFiscal";
            this.rdoPrevFiscal.Size = new System.Drawing.Size(155, 21);
            this.rdoPrevFiscal.TabIndex = 3;
            this.rdoPrevFiscal.Tag = "2";
            this.rdoPrevFiscal.Text = "Previous Fiscal Year";
            this.rdoPrevFiscal.UseVisualStyleBackColor = true;
            this.rdoPrevFiscal.CheckedChanged += new System.EventHandler(this.rdoRangeType_CheckedChanged);
            // 
            // rdoCurCal
            // 
            this.rdoCurCal.AutoSize = true;
            this.rdoCurCal.Location = new System.Drawing.Point(24, 58);
            this.rdoCurCal.Name = "rdoCurCal";
            this.rdoCurCal.Size = new System.Drawing.Size(168, 21);
            this.rdoCurCal.TabIndex = 2;
            this.rdoCurCal.Tag = "1";
            this.rdoCurCal.Text = "Current Calendar Year";
            this.rdoCurCal.UseVisualStyleBackColor = true;
            this.rdoCurCal.CheckedChanged += new System.EventHandler(this.rdoRangeType_CheckedChanged);
            // 
            // rdoCurFiscal
            // 
            this.rdoCurFiscal.AutoSize = true;
            this.rdoCurFiscal.Checked = true;
            this.rdoCurFiscal.Location = new System.Drawing.Point(24, 32);
            this.rdoCurFiscal.Name = "rdoCurFiscal";
            this.rdoCurFiscal.Size = new System.Drawing.Size(147, 21);
            this.rdoCurFiscal.TabIndex = 1;
            this.rdoCurFiscal.TabStop = true;
            this.rdoCurFiscal.Tag = "0";
            this.rdoCurFiscal.Text = "Current Fiscal Year";
            this.rdoCurFiscal.UseVisualStyleBackColor = true;
            this.rdoCurFiscal.CheckedChanged += new System.EventHandler(this.rdoRangeType_CheckedChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(12, 240);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 20);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(184, 242);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(80, 20);
            this.btnOk.TabIndex = 11;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // SelectDateRange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(296, 267);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox1);
            this.Name = "SelectDateRange";
            this.Text = "Households with Lastest Service";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdoCustom;
        private System.Windows.Forms.RadioButton rdoPrevCal;
        private System.Windows.Forms.RadioButton rdoPrevFiscal;
        private System.Windows.Forms.RadioButton rdoCurCal;
        private System.Windows.Forms.RadioButton rdoCurFiscal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpLast;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpFirst;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
    }
}