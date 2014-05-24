namespace ClientcardFB3
{
    partial class SignFamilyCard
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
            this.btnResetSig = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.chkCloseOnSave = new System.Windows.Forms.CheckBox();
            this.chkPrintOnSave = new System.Windows.Forms.CheckBox();
            this.sigPadInputCtrl1 = new SigPadCtrl.SigPadInputCtrl();
            this.SuspendLayout();
            // 
            // btnResetSig
            // 
            this.btnResetSig.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnResetSig.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnResetSig.Location = new System.Drawing.Point(248, 76);
            this.btnResetSig.Name = "btnResetSig";
            this.btnResetSig.Size = new System.Drawing.Size(68, 24);
            this.btnResetSig.TabIndex = 112;
            this.btnResetSig.Text = "&Reset Sig";
            this.btnResetSig.UseVisualStyleBackColor = true;
            this.btnResetSig.Visible = false;
            this.btnResetSig.Click += new System.EventHandler(this.btnResetSig_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(324, 76);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(68, 24);
            this.btnCancel.TabIndex = 110;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(248, 16);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(144, 44);
            this.btnSave.TabIndex = 109;
            this.btnSave.Text = "&Save Signature";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(8, 109);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(580, 527);
            this.webBrowser1.TabIndex = 113;
            this.webBrowser1.Url = new System.Uri("", System.UriKind.Relative);
            // 
            // chkCloseOnSave
            // 
            this.chkCloseOnSave.AutoSize = true;
            this.chkCloseOnSave.Checked = true;
            this.chkCloseOnSave.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCloseOnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCloseOnSave.Location = new System.Drawing.Point(400, 38);
            this.chkCloseOnSave.Name = "chkCloseOnSave";
            this.chkCloseOnSave.Size = new System.Drawing.Size(121, 21);
            this.chkCloseOnSave.TabIndex = 114;
            this.chkCloseOnSave.Text = "Close On Save";
            this.chkCloseOnSave.UseVisualStyleBackColor = true;
            // 
            // chkPrintOnSave
            // 
            this.chkPrintOnSave.AutoSize = true;
            this.chkPrintOnSave.Checked = true;
            this.chkPrintOnSave.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPrintOnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPrintOnSave.Location = new System.Drawing.Point(400, 16);
            this.chkPrintOnSave.Name = "chkPrintOnSave";
            this.chkPrintOnSave.Size = new System.Drawing.Size(115, 21);
            this.chkPrintOnSave.TabIndex = 115;
            this.chkPrintOnSave.Text = "Print On Save";
            this.chkPrintOnSave.UseVisualStyleBackColor = true;
            // 
            // sigPadInputCtrl1
            // 
            this.sigPadInputCtrl1.Location = new System.Drawing.Point(20, 8);
            this.sigPadInputCtrl1.Margin = new System.Windows.Forms.Padding(4);
            this.sigPadInputCtrl1.Name = "sigPadInputCtrl1";
            this.sigPadInputCtrl1.Signature = "300D0A300D0A";
            this.sigPadInputCtrl1.Size = new System.Drawing.Size(216, 90);
            this.sigPadInputCtrl1.TabIndex = 111;
            // 
            // SignFamilyCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(599, 639);
            this.Controls.Add(this.chkPrintOnSave);
            this.Controls.Add(this.chkCloseOnSave);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.btnResetSig);
            this.Controls.Add(this.sigPadInputCtrl1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Name = "SignFamilyCard";
            this.Text = "SignFamilyCard";
            this.Load += new System.EventHandler(this.SignFamilyCard_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnResetSig;
        private SigPadCtrl.SigPadInputCtrl sigPadInputCtrl1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.CheckBox chkCloseOnSave;
        private System.Windows.Forms.CheckBox chkPrintOnSave;

    }
}