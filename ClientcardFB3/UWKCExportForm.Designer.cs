namespace ClientcardFB3
{
    partial class UWKCExportForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UWKCExportForm));
            this.label1 = new System.Windows.Forms.Label();
            this.tbAgencyNumber = new System.Windows.Forms.TextBox();
            this.rdoUseOnlySurveyComplete = new System.Windows.Forms.RadioButton();
            this.rdoExportAll = new System.Windows.Forms.RadioButton();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(275, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Please Enter Or Verify Agency ID Number:";
            // 
            // tbAgencyNumber
            // 
            this.tbAgencyNumber.Location = new System.Drawing.Point(24, 48);
            this.tbAgencyNumber.Name = "tbAgencyNumber";
            this.tbAgencyNumber.Size = new System.Drawing.Size(160, 23);
            this.tbAgencyNumber.TabIndex = 1;
            this.tbAgencyNumber.Tag = "AgencyNumber";
            this.tbAgencyNumber.Leave += new System.EventHandler(this.tbAgencyNumber_Leave);
            // 
            // rdoUseOnlySurveyComplete
            // 
            this.rdoUseOnlySurveyComplete.AutoSize = true;
            this.rdoUseOnlySurveyComplete.Checked = true;
            this.rdoUseOnlySurveyComplete.Location = new System.Drawing.Point(24, 88);
            this.rdoUseOnlySurveyComplete.Name = "rdoUseOnlySurveyComplete";
            this.rdoUseOnlySurveyComplete.Size = new System.Drawing.Size(210, 21);
            this.rdoUseOnlySurveyComplete.TabIndex = 2;
            this.rdoUseOnlySurveyComplete.TabStop = true;
            this.rdoUseOnlySurveyComplete.Text = "Export Only Survey Complete";
            this.rdoUseOnlySurveyComplete.UseVisualStyleBackColor = true;
            // 
            // rdoExportAll
            // 
            this.rdoExportAll.AutoSize = true;
            this.rdoExportAll.Location = new System.Drawing.Point(24, 112);
            this.rdoExportAll.Name = "rdoExportAll";
            this.rdoExportAll.Size = new System.Drawing.Size(85, 21);
            this.rdoExportAll.TabIndex = 3;
            this.rdoExportAll.Text = "Export All";
            this.rdoExportAll.UseVisualStyleBackColor = true;
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(22, 152);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(72, 48);
            this.btnExport.TabIndex = 4;
            this.btnExport.Text = "Export Data";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(232, 152);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 48);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // UWKCExportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Cornsilk;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(311, 209);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.rdoExportAll);
            this.Controls.Add(this.rdoUseOnlySurveyComplete);
            this.Controls.Add(this.tbAgencyNumber);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "UWKCExportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "UWKCExportForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbAgencyNumber;
        private System.Windows.Forms.RadioButton rdoUseOnlySurveyComplete;
        private System.Windows.Forms.RadioButton rdoExportAll;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnCancel;
    }
}