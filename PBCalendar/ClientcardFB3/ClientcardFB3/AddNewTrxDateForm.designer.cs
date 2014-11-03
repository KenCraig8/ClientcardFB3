namespace ClientcardFB3
{
    partial class AddNewTrxDateForm
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
            this.lblEnterVolHrs = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.dtpNewDate = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // lblEnterVolHrs
            // 
            this.lblEnterVolHrs.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblEnterVolHrs.Location = new System.Drawing.Point(32, 24);
            this.lblEnterVolHrs.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEnterVolHrs.Name = "lblEnterVolHrs";
            this.lblEnterVolHrs.Size = new System.Drawing.Size(339, 33);
            this.lblEnterVolHrs.TabIndex = 0;
            this.lblEnterVolHrs.Text = "Enter Date For Volunteer Hours";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(267, 78);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(83, 40);
            this.button1.TabIndex = 2;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(356, 78);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(83, 40);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // dtpNewDate
            // 
            this.dtpNewDate.CustomFormat = "MM/dd/yyyy dddd";
            this.dtpNewDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNewDate.Location = new System.Drawing.Point(12, 83);
            this.dtpNewDate.Name = "dtpNewDate";
            this.dtpNewDate.Size = new System.Drawing.Size(226, 23);
            this.dtpNewDate.TabIndex = 4;
            // 
            // AddNewTrxDateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Cornsilk;
            this.ClientSize = new System.Drawing.Size(470, 138);
            this.Controls.Add(this.dtpNewDate);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblEnterVolHrs);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AddNewTrxDateForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New TrxDate";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblEnterVolHrs;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DateTimePicker dtpNewDate;
    }
}