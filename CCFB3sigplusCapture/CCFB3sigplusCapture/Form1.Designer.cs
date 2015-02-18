namespace CCFB3sigplusCapture
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.axSigPlus1 = new AxSIGPLUSLib.AxSigPlus();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.staPage1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.staPage2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.staSignature = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.axSigPlus1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // axSigPlus1
            // 
            this.axSigPlus1.Enabled = true;
            this.axSigPlus1.Location = new System.Drawing.Point(12, 12);
            this.axSigPlus1.Name = "axSigPlus1";
            this.axSigPlus1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axSigPlus1.OcxState")));
            this.axSigPlus1.Size = new System.Drawing.Size(235, 52);
            this.axSigPlus1.TabIndex = 0;
            this.axSigPlus1.PenDown += new System.EventHandler(this.axSigPlus1_PenDown);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(341, 12);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(71, 40);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "Get Signature";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(341, 71);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(71, 40);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save Signature";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Visible = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.staPage1,
            this.staPage2,
            this.staSignature});
            this.statusStrip1.Location = new System.Drawing.Point(0, 120);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(424, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Visible = false;
            // 
            // staPage1
            // 
            this.staPage1.AutoSize = false;
            this.staPage1.Name = "staPage1";
            this.staPage1.Size = new System.Drawing.Size(50, 17);
            this.staPage1.Text = "Page 1";
            // 
            // staPage2
            // 
            this.staPage2.AutoSize = false;
            this.staPage2.Name = "staPage2";
            this.staPage2.Size = new System.Drawing.Size(50, 17);
            this.staPage2.Text = "Page 2";
            // 
            // staSignature
            // 
            this.staSignature.AutoSize = false;
            this.staSignature.Name = "staSignature";
            this.staSignature.Size = new System.Drawing.Size(70, 17);
            this.staSignature.Text = "Signature";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 142);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.axSigPlus1);
            this.Name = "Form1";
            this.Text = "FB3 Receiving Service";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.axSigPlus1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AxSIGPLUSLib.AxSigPlus axSigPlus1;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel staPage1;
        private System.Windows.Forms.ToolStripStatusLabel staPage2;
        private System.Windows.Forms.ToolStripStatusLabel staSignature;

    }
}

