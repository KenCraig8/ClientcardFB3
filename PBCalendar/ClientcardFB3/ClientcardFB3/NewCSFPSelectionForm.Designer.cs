namespace ClientcardFB3
{
    partial class NewCSFPSelectionForm
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
            this.gbRenewExpDate = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.dtpExpDate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.lvHHMems = new System.Windows.Forms.ListView();
            this.clmName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmAge = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmBirthdate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmHHMemID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.gbRenewExpDate.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbRenewExpDate
            // 
            this.gbRenewExpDate.Controls.Add(this.btnCancel);
            this.gbRenewExpDate.Controls.Add(this.btnSave);
            this.gbRenewExpDate.Controls.Add(this.dtpExpDate);
            this.gbRenewExpDate.Controls.Add(this.label5);
            this.gbRenewExpDate.Location = new System.Drawing.Point(8, 8);
            this.gbRenewExpDate.Name = "gbRenewExpDate";
            this.gbRenewExpDate.Size = new System.Drawing.Size(520, 80);
            this.gbRenewExpDate.TabIndex = 82;
            this.gbRenewExpDate.TabStop = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(392, 16);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(104, 40);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(272, 16);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(104, 40);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dtpExpDate
            // 
            this.dtpExpDate.Location = new System.Drawing.Point(8, 32);
            this.dtpExpDate.Name = "dtpExpDate";
            this.dtpExpDate.Size = new System.Drawing.Size(256, 20);
            this.dtpExpDate.TabIndex = 1;
            this.dtpExpDate.Leave += new System.EventHandler(this.dtpExpDate_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Expiration Date:";
            // 
            // lvHHMems
            // 
            this.lvHHMems.CheckBoxes = true;
            this.lvHHMems.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmName,
            this.clmAge,
            this.clmBirthdate,
            this.clmHHMemID});
            this.lvHHMems.FullRowSelect = true;
            this.lvHHMems.GridLines = true;
            this.lvHHMems.HideSelection = false;
            this.lvHHMems.Location = new System.Drawing.Point(8, 96);
            this.lvHHMems.MultiSelect = false;
            this.lvHHMems.Name = "lvHHMems";
            this.lvHHMems.Size = new System.Drawing.Size(520, 272);
            this.lvHHMems.TabIndex = 83;
            this.lvHHMems.UseCompatibleStateImageBehavior = false;
            this.lvHHMems.View = System.Windows.Forms.View.Details;
            this.lvHHMems.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvHHMems_ItemChecked);
            this.lvHHMems.SelectedIndexChanged += new System.EventHandler(this.lvHHMems_SelectedIndexChanged);
            // 
            // clmName
            // 
            this.clmName.Text = "Name";
            this.clmName.Width = 300;
            // 
            // clmAge
            // 
            this.clmAge.Text = "Age";
            this.clmAge.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clmAge.Width = 50;
            // 
            // clmBirthdate
            // 
            this.clmBirthdate.Text = "Birthdate";
            this.clmBirthdate.Width = 100;
            // 
            // clmHHMemID
            // 
            this.clmHHMemID.Text = "HHMemID";
            // 
            // NewCSFPSelectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Cornsilk;
            this.ClientSize = new System.Drawing.Size(539, 373);
            this.Controls.Add(this.lvHHMems);
            this.Controls.Add(this.gbRenewExpDate);
            this.Name = "NewCSFPSelectionForm";
            this.Text = "NewCSFPSelectionForm";
            this.gbRenewExpDate.ResumeLayout(false);
            this.gbRenewExpDate.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbRenewExpDate;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DateTimePicker dtpExpDate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListView lvHHMems;
        private System.Windows.Forms.ColumnHeader clmName;
        private System.Windows.Forms.ColumnHeader clmAge;
        private System.Windows.Forms.ColumnHeader clmBirthdate;
        private System.Windows.Forms.ColumnHeader clmHHMemID;
    }
}