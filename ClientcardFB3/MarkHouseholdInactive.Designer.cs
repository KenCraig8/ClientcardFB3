namespace ClientcardFB3
{
    partial class MarkHouseholdInactive
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
            this.rdoBtn1 = new System.Windows.Forms.RadioButton();
            this.rdoBtn2 = new System.Windows.Forms.RadioButton();
            this.btnProcess = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // rdoBtn1
            // 
            this.rdoBtn1.AutoSize = true;
            this.rdoBtn1.Location = new System.Drawing.Point(20, 16);
            this.rdoBtn1.Name = "rdoBtn1";
            this.rdoBtn1.Size = new System.Drawing.Size(171, 17);
            this.rdoBtn1.TabIndex = 0;
            this.rdoBtn1.Text = "Mark Inactive ALL Households";
            this.rdoBtn1.UseVisualStyleBackColor = true;
            this.rdoBtn1.CheckedChanged += new System.EventHandler(this.rdoBtn_CheckedChanged);
            // 
            // rdoBtn2
            // 
            this.rdoBtn2.AutoSize = true;
            this.rdoBtn2.Checked = true;
            this.rdoBtn2.Location = new System.Drawing.Point(20, 44);
            this.rdoBtn2.Name = "rdoBtn2";
            this.rdoBtn2.Size = new System.Drawing.Size(327, 17);
            this.rdoBtn2.TabIndex = 1;
            this.rdoBtn2.TabStop = true;
            this.rdoBtn2.Text = "Mark Inactive Households That Have Not Had a Service Since:";
            this.rdoBtn2.UseVisualStyleBackColor = true;
            this.rdoBtn2.CheckedChanged += new System.EventHandler(this.rdoBtn_CheckedChanged);
            // 
            // btnProcess
            // 
            this.btnProcess.Location = new System.Drawing.Point(348, 80);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(112, 28);
            this.btnProcess.TabIndex = 12;
            this.btnProcess.Text = "&Process";
            this.btnProcess.UseVisualStyleBackColor = true;
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(156, 80);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(112, 28);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(356, 42);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(116, 20);
            this.dateTimePicker1.TabIndex = 2;
            // 
            // MarkHouseholdInactive
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 129);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnProcess);
            this.Controls.Add(this.rdoBtn2);
            this.Controls.Add(this.rdoBtn1);
            this.Name = "MarkHouseholdInactive";
            this.Text = "Mark Households Inactive";
            this.Load += new System.EventHandler(this.MarkHouseholdInactive_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rdoBtn1;
        private System.Windows.Forms.RadioButton rdoBtn2;
        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
    }
}