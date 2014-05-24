namespace ClientcardFB3
{
    partial class NewMatrixGroupForm
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
            this.tbDesc = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbShortName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbNotes = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpAsOfDate = new System.Windows.Forms.DateTimePicker();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.cboProcessID = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // tbDesc
            // 
            this.tbDesc.Font = new System.Drawing.Font("Verdana", 10F);
            this.tbDesc.Location = new System.Drawing.Point(116, 13);
            this.tbDesc.Name = "tbDesc";
            this.tbDesc.Size = new System.Drawing.Size(231, 24);
            this.tbDesc.TabIndex = 11;
            this.tbDesc.Tag = "ItemDesc";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Verdana", 9F);
            this.label1.Location = new System.Drawing.Point(10, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 17);
            this.label1.TabIndex = 10;
            this.label1.Text = "Descripton:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbShortName
            // 
            this.tbShortName.Font = new System.Drawing.Font("Verdana", 10F);
            this.tbShortName.Location = new System.Drawing.Point(116, 41);
            this.tbShortName.Name = "tbShortName";
            this.tbShortName.Size = new System.Drawing.Size(231, 24);
            this.tbShortName.TabIndex = 13;
            this.tbShortName.Tag = "ItemDesc";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Verdana", 9F);
            this.label2.Location = new System.Drawing.Point(10, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 17);
            this.label2.TabIndex = 12;
            this.label2.Text = "Short Name:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbNotes
            // 
            this.tbNotes.Font = new System.Drawing.Font("Verdana", 10F);
            this.tbNotes.Location = new System.Drawing.Point(116, 70);
            this.tbNotes.Multiline = true;
            this.tbNotes.Name = "tbNotes";
            this.tbNotes.Size = new System.Drawing.Size(231, 40);
            this.tbNotes.TabIndex = 15;
            this.tbNotes.Tag = "ItemDesc";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Verdana", 9F);
            this.label3.Location = new System.Drawing.Point(10, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 17);
            this.label3.TabIndex = 14;
            this.label3.Text = "Notes:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Verdana", 9F);
            this.label4.Location = new System.Drawing.Point(10, 116);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 17);
            this.label4.TabIndex = 16;
            this.label4.Text = "As Of Date:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpAsOfDate
            // 
            this.dtpAsOfDate.Location = new System.Drawing.Point(116, 116);
            this.dtpAsOfDate.Name = "dtpAsOfDate";
            this.dtpAsOfDate.Size = new System.Drawing.Size(228, 20);
            this.dtpAsOfDate.TabIndex = 17;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(216, 176);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 26);
            this.btnCancel.TabIndex = 19;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(100, 176);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(90, 26);
            this.btnOK.TabIndex = 18;
            this.btnOK.Text = "Add Matrix";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Verdana", 9F);
            this.label5.Location = new System.Drawing.Point(8, 144);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 17);
            this.label5.TabIndex = 20;
            this.label5.Text = "Matrix Type:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboProcessID
            // 
            this.cboProcessID.FormattingEnabled = true;
            this.cboProcessID.Location = new System.Drawing.Point(116, 142);
            this.cboProcessID.Name = "cboProcessID";
            this.cboProcessID.Size = new System.Drawing.Size(231, 21);
            this.cboProcessID.TabIndex = 21;
            this.cboProcessID.Tag = "parm_IncomeProcessID";
            // 
            // NewMatrixGroupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Cornsilk;
            this.ClientSize = new System.Drawing.Size(386, 212);
            this.Controls.Add(this.cboProcessID);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.dtpAsOfDate);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbNotes);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbShortName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbDesc);
            this.Controls.Add(this.label1);
            this.Name = "NewMatrixGroupForm";
            this.Text = "New Matrix Group";
            this.Load += new System.EventHandler(this.NewMatrixGroupForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbDesc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbShortName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbNotes;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpAsOfDate;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboProcessID;
    }
}