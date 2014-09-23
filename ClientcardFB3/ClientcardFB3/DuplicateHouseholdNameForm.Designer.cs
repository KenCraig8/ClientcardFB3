namespace ClientcardFB3
{
    partial class DuplicateHouseholdNameForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lvwPeople = new System.Windows.Forms.ListView();
            this.colLastName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFirstName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colBirthDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colAge = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colHholdId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colHHMInactive = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwHouseholds = new System.Windows.Forms.ListView();
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colInactive = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colAddress = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCityStateZip = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colNbrTrans = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colHHId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSearchForName = new System.Windows.Forms.Button();
            this.tbHHName = new System.Windows.Forms.TextBox();
            this.lblLName = new System.Windows.Forms.Label();
            this.btnConfirmName = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.lvwPeople);
            this.panel1.Controls.Add(this.lvwHouseholds);
            this.panel1.Location = new System.Drawing.Point(5, 144);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(989, 254);
            this.panel1.TabIndex = 27;
            // 
            // lvwPeople
            // 
            this.lvwPeople.BackColor = System.Drawing.Color.Gainsboro;
            this.lvwPeople.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colLastName,
            this.colFirstName,
            this.colBirthDate,
            this.colAge,
            this.colHholdId,
            this.colHHMInactive});
            this.lvwPeople.FullRowSelect = true;
            this.lvwPeople.GridLines = true;
            this.lvwPeople.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvwPeople.Location = new System.Drawing.Point(260, 120);
            this.lvwPeople.Name = "lvwPeople";
            this.lvwPeople.Size = new System.Drawing.Size(720, 125);
            this.lvwPeople.TabIndex = 15;
            this.lvwPeople.TabStop = false;
            this.lvwPeople.UseCompatibleStateImageBehavior = false;
            this.lvwPeople.View = System.Windows.Forms.View.Details;
            // 
            // colLastName
            // 
            this.colLastName.Text = "Last Name";
            this.colLastName.Width = 240;
            // 
            // colFirstName
            // 
            this.colFirstName.Text = "First Name";
            this.colFirstName.Width = 150;
            // 
            // colBirthDate
            // 
            this.colBirthDate.Text = "Birth Date";
            this.colBirthDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colBirthDate.Width = 100;
            // 
            // colAge
            // 
            this.colAge.Text = "Age";
            this.colAge.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colAge.Width = 45;
            // 
            // colHholdId
            // 
            this.colHholdId.Text = "HHID";
            this.colHholdId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colHholdId.Width = 80;
            // 
            // colHHMInactive
            // 
            this.colHHMInactive.Text = "Inactive";
            this.colHHMInactive.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colHHMInactive.Width = 70;
            // 
            // lvwHouseholds
            // 
            this.lvwHouseholds.BackColor = System.Drawing.Color.Gainsboro;
            this.lvwHouseholds.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colInactive,
            this.colAddress,
            this.colCityStateZip,
            this.colNbrTrans,
            this.colHHId});
            this.lvwHouseholds.FullRowSelect = true;
            this.lvwHouseholds.GridLines = true;
            this.lvwHouseholds.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvwHouseholds.LabelWrap = false;
            this.lvwHouseholds.Location = new System.Drawing.Point(3, 3);
            this.lvwHouseholds.Name = "lvwHouseholds";
            this.lvwHouseholds.Size = new System.Drawing.Size(977, 111);
            this.lvwHouseholds.TabIndex = 14;
            this.lvwHouseholds.TabStop = false;
            this.lvwHouseholds.UseCompatibleStateImageBehavior = false;
            this.lvwHouseholds.View = System.Windows.Forms.View.Details;
            // 
            // colName
            // 
            this.colName.Text = "Name";
            this.colName.Width = 220;
            // 
            // colInactive
            // 
            this.colInactive.Text = "Inactive";
            this.colInactive.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colInactive.Width = 70;
            // 
            // colAddress
            // 
            this.colAddress.Text = "Address";
            this.colAddress.Width = 240;
            // 
            // colCityStateZip
            // 
            this.colCityStateZip.Text = "City, State Zipcode";
            this.colCityStateZip.Width = 200;
            // 
            // colNbrTrans
            // 
            this.colNbrTrans.Text = "# Trx";
            this.colNbrTrans.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // colHHId
            // 
            this.colHHId.Text = "HH ID";
            this.colHHId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colHHId.Width = 80;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(560, 56);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(92, 31);
            this.btnCancel.TabIndex = 26;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSearchForName
            // 
            this.btnSearchForName.Location = new System.Drawing.Point(445, 7);
            this.btnSearchForName.Name = "btnSearchForName";
            this.btnSearchForName.Size = new System.Drawing.Size(210, 31);
            this.btnSearchForName.TabIndex = 25;
            this.btnSearchForName.Text = "&Search For Name";
            this.btnSearchForName.UseVisualStyleBackColor = true;
            this.btnSearchForName.Click += new System.EventHandler(this.btnSearchForName_Click);
            // 
            // tbHHName
            // 
            this.tbHHName.Location = new System.Drawing.Point(113, 9);
            this.tbHHName.Name = "tbHHName";
            this.tbHHName.Size = new System.Drawing.Size(240, 20);
            this.tbHHName.TabIndex = 15;
            this.tbHHName.Tag = "LastName";
            this.tbHHName.TextChanged += new System.EventHandler(this.tbHHName_TextChanged);
            // 
            // lblLName
            // 
            this.lblLName.AutoSize = true;
            this.lblLName.Location = new System.Drawing.Point(10, 9);
            this.lblLName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLName.Name = "lblLName";
            this.lblLName.Size = new System.Drawing.Size(92, 13);
            this.lblLName.TabIndex = 14;
            this.lblLName.Text = "Household Name:";
            // 
            // btnConfirmName
            // 
            this.btnConfirmName.Location = new System.Drawing.Point(671, 6);
            this.btnConfirmName.Name = "btnConfirmName";
            this.btnConfirmName.Size = new System.Drawing.Size(210, 31);
            this.btnConfirmName.TabIndex = 28;
            this.btnConfirmName.Text = "&Confirm New Member";
            this.btnConfirmName.UseVisualStyleBackColor = true;
            this.btnConfirmName.Visible = false;
            this.btnConfirmName.Click += new System.EventHandler(this.btnConfirmName_Click);
            // 
            // DuplicateHouseholdNameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Cornsilk;
            this.ClientSize = new System.Drawing.Size(999, 405);
            this.Controls.Add(this.btnConfirmName);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSearchForName);
            this.Controls.Add(this.tbHHName);
            this.Controls.Add(this.lblLName);
            this.Name = "DuplicateHouseholdNameForm";
            this.Text = "DuplicateHouseholdNameForm";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView lvwPeople;
        private System.Windows.Forms.ColumnHeader colLastName;
        private System.Windows.Forms.ColumnHeader colFirstName;
        private System.Windows.Forms.ColumnHeader colBirthDate;
        private System.Windows.Forms.ColumnHeader colAge;
        private System.Windows.Forms.ColumnHeader colHholdId;
        private System.Windows.Forms.ColumnHeader colHHMInactive;
        private System.Windows.Forms.ListView lvwHouseholds;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colInactive;
        private System.Windows.Forms.ColumnHeader colAddress;
        private System.Windows.Forms.ColumnHeader colCityStateZip;
        private System.Windows.Forms.ColumnHeader colNbrTrans;
        private System.Windows.Forms.ColumnHeader colHHId;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSearchForName;
        private System.Windows.Forms.TextBox tbHHName;
        private System.Windows.Forms.Label lblLName;
        private System.Windows.Forms.Button btnConfirmName;
    }
}