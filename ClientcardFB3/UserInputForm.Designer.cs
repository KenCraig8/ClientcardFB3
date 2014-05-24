namespace ClientcardFB3
{
    partial class UserInputForm
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label24 = new System.Windows.Forms.Label();
            this.cboType = new System.Windows.Forms.ComboBox();
            this.label23 = new System.Windows.Forms.Label();
            this.cboRules = new System.Windows.Forms.ComboBox();
            this.tbItemDesc = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(111, 111);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(90, 26);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "Add Item";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(227, 111);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 26);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label24
            // 
            this.label24.Font = new System.Drawing.Font("Verdana", 9F);
            this.label24.Location = new System.Drawing.Point(5, 45);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(100, 17);
            this.label24.TabIndex = 10;
            this.label24.Text = "Category:";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboType
            // 
            this.cboType.Font = new System.Drawing.Font("Verdana", 10F);
            this.cboType.FormattingEnabled = true;
            this.cboType.Location = new System.Drawing.Point(111, 41);
            this.cboType.Name = "cboType";
            this.cboType.Size = new System.Drawing.Size(162, 24);
            this.cboType.TabIndex = 11;
            this.cboType.Tag = "ItemType";
            // 
            // label23
            // 
            this.label23.Font = new System.Drawing.Font("Verdana", 9F);
            this.label23.Location = new System.Drawing.Point(5, 74);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(100, 17);
            this.label23.TabIndex = 12;
            this.label23.Text = "Service Rule:";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboRules
            // 
            this.cboRules.Font = new System.Drawing.Font("Verdana", 10F);
            this.cboRules.FormattingEnabled = true;
            this.cboRules.Location = new System.Drawing.Point(111, 71);
            this.cboRules.Name = "cboRules";
            this.cboRules.Size = new System.Drawing.Size(162, 24);
            this.cboRules.TabIndex = 13;
            this.cboRules.Tag = "ItemRule";
            // 
            // tbItemDesc
            // 
            this.tbItemDesc.Font = new System.Drawing.Font("Verdana", 10F);
            this.tbItemDesc.Location = new System.Drawing.Point(111, 12);
            this.tbItemDesc.Name = "tbItemDesc";
            this.tbItemDesc.Size = new System.Drawing.Size(231, 24);
            this.tbItemDesc.TabIndex = 9;
            this.tbItemDesc.Tag = "ItemDesc";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Verdana", 9F);
            this.label1.Location = new System.Drawing.Point(5, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 17);
            this.label1.TabIndex = 8;
            this.label1.Text = "Descripton:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // UserInputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Cornsilk;
            this.ClientSize = new System.Drawing.Size(356, 150);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.cboType);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.cboRules);
            this.Controls.Add(this.tbItemDesc);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "UserInputForm";
            this.Text = "Add New Service Item Form";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.ComboBox cboType;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.ComboBox cboRules;
        private System.Windows.Forms.TextBox tbItemDesc;
        private System.Windows.Forms.Label label1;
    }
}