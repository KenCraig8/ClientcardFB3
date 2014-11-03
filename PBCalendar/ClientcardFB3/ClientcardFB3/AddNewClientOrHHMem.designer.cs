namespace ClientcardFB3
{
    partial class AddNewClientOrHHMem
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
            this.lblFName = new System.Windows.Forms.Label();
            this.lblLName = new System.Windows.Forms.Label();
            this.tbFirstName = new System.Windows.Forms.TextBox();
            this.tbLastName = new System.Windows.Forms.TextBox();
            this.tbBirthDate = new System.Windows.Forms.TextBox();
            this.lblBDay = new System.Windows.Forms.Label();
            this.lblSex = new System.Windows.Forms.Label();
            this.tbSex = new System.Windows.Forms.TextBox();
            this.btnAddNewMem = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbAge = new System.Windows.Forms.TextBox();
            this.chkEnterAge = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblFName
            // 
            this.lblFName.AutoSize = true;
            this.lblFName.Location = new System.Drawing.Point(10, 47);
            this.lblFName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFName.Name = "lblFName";
            this.lblFName.Size = new System.Drawing.Size(76, 16);
            this.lblFName.TabIndex = 2;
            this.lblFName.Text = "First Name:";
            // 
            // lblLName
            // 
            this.lblLName.AutoSize = true;
            this.lblLName.Location = new System.Drawing.Point(10, 12);
            this.lblLName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLName.Name = "lblLName";
            this.lblLName.Size = new System.Drawing.Size(76, 16);
            this.lblLName.TabIndex = 0;
            this.lblLName.Text = "Last Name:";
            // 
            // tbFirstName
            // 
            this.tbFirstName.Location = new System.Drawing.Point(114, 44);
            this.tbFirstName.Name = "tbFirstName";
            this.tbFirstName.Size = new System.Drawing.Size(240, 22);
            this.tbFirstName.TabIndex = 3;
            this.tbFirstName.Tag = "FirstName";
            this.tbFirstName.Leave += new System.EventHandler(this.tbFirstName_Leave);
            // 
            // tbLastName
            // 
            this.tbLastName.Location = new System.Drawing.Point(113, 12);
            this.tbLastName.Name = "tbLastName";
            this.tbLastName.Size = new System.Drawing.Size(240, 22);
            this.tbLastName.TabIndex = 1;
            this.tbLastName.Tag = "LastName";
            this.tbLastName.Leave += new System.EventHandler(this.tbLastName_Leave);
            // 
            // tbBirthDate
            // 
            this.tbBirthDate.Location = new System.Drawing.Point(114, 102);
            this.tbBirthDate.Name = "tbBirthDate";
            this.tbBirthDate.Size = new System.Drawing.Size(126, 22);
            this.tbBirthDate.TabIndex = 6;
            this.tbBirthDate.Tag = "Birthdate";
            this.tbBirthDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbBirthDate.Leave += new System.EventHandler(this.tbBirthDate_Leave);
            // 
            // lblBDay
            // 
            this.lblBDay.AutoSize = true;
            this.lblBDay.Location = new System.Drawing.Point(16, 106);
            this.lblBDay.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBDay.Name = "lblBDay";
            this.lblBDay.Size = new System.Drawing.Size(69, 16);
            this.lblBDay.TabIndex = 5;
            this.lblBDay.Tag = "Birthdate";
            this.lblBDay.Text = "Birth Date:";
            // 
            // lblSex
            // 
            this.lblSex.AutoSize = true;
            this.lblSex.Location = new System.Drawing.Point(371, 103);
            this.lblSex.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSex.Name = "lblSex";
            this.lblSex.Size = new System.Drawing.Size(34, 16);
            this.lblSex.TabIndex = 9;
            this.lblSex.Tag = "Sex";
            this.lblSex.Text = "Sex:";
            // 
            // tbSex
            // 
            this.tbSex.Location = new System.Drawing.Point(421, 103);
            this.tbSex.Name = "tbSex";
            this.tbSex.Size = new System.Drawing.Size(27, 22);
            this.tbSex.TabIndex = 10;
            this.tbSex.Tag = "Sex";
            this.tbSex.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnAddNewMem
            // 
            this.btnAddNewMem.Location = new System.Drawing.Point(445, 10);
            this.btnAddNewMem.Name = "btnAddNewMem";
            this.btnAddNewMem.Size = new System.Drawing.Size(210, 31);
            this.btnAddNewMem.TabIndex = 11;
            this.btnAddNewMem.Text = "&Add New Household Member";
            this.btnAddNewMem.UseVisualStyleBackColor = true;
            this.btnAddNewMem.Click += new System.EventHandler(this.btnAddNewMem_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(563, 69);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(92, 31);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.tbCancel_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.lvwPeople);
            this.panel1.Controls.Add(this.lvwHouseholds);
            this.panel1.Location = new System.Drawing.Point(5, 147);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(989, 254);
            this.panel1.TabIndex = 13;
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(262, 106);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 16);
            this.label1.TabIndex = 7;
            this.label1.Text = "Age:";
            // 
            // tbAge
            // 
            this.tbAge.Enabled = false;
            this.tbAge.Location = new System.Drawing.Point(312, 102);
            this.tbAge.Name = "tbAge";
            this.tbAge.Size = new System.Drawing.Size(42, 22);
            this.tbAge.TabIndex = 8;
            this.tbAge.Tag = "Age";
            this.tbAge.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbAge.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbAge_KeyDown);
            this.tbAge.Leave += new System.EventHandler(this.tbAge_Leave);
            // 
            // chkEnterAge
            // 
            this.chkEnterAge.AutoSize = true;
            this.chkEnterAge.Location = new System.Drawing.Point(114, 76);
            this.chkEnterAge.Name = "chkEnterAge";
            this.chkEnterAge.Size = new System.Drawing.Size(166, 20);
            this.chkEnterAge.TabIndex = 4;
            this.chkEnterAge.Tag = "UseAge";
            this.chkEnterAge.Text = "Enter Age Not Birthdate";
            this.chkEnterAge.UseVisualStyleBackColor = true;
            this.chkEnterAge.CheckedChanged += new System.EventHandler(this.chkEnterAge_CheckedChanged);
            // 
            // AddNewClientOrHHMem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Cornsilk;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(999, 405);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbAge);
            this.Controls.Add(this.chkEnterAge);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAddNewMem);
            this.Controls.Add(this.lblSex);
            this.Controls.Add(this.tbSex);
            this.Controls.Add(this.lblBDay);
            this.Controls.Add(this.tbBirthDate);
            this.Controls.Add(this.tbLastName);
            this.Controls.Add(this.tbFirstName);
            this.Controls.Add(this.lblLName);
            this.Controls.Add(this.lblFName);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AddNewClientOrHHMem";
            this.Text = "AddHHMem";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFName;
        private System.Windows.Forms.Label lblLName;
        private System.Windows.Forms.TextBox tbFirstName;
        private System.Windows.Forms.TextBox tbLastName;
        private System.Windows.Forms.TextBox tbBirthDate;
        private System.Windows.Forms.Label lblBDay;
        private System.Windows.Forms.Label lblSex;
        private System.Windows.Forms.TextBox tbSex;
        private System.Windows.Forms.Button btnAddNewMem;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbAge;
        private System.Windows.Forms.CheckBox chkEnterAge;
        private System.Windows.Forms.ListView lvwHouseholds;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colInactive;
        private System.Windows.Forms.ColumnHeader colAddress;
        private System.Windows.Forms.ColumnHeader colCityStateZip;
        private System.Windows.Forms.ColumnHeader colNbrTrans;
        private System.Windows.Forms.ColumnHeader colHHId;
        private System.Windows.Forms.ListView lvwPeople;
        private System.Windows.Forms.ColumnHeader colLastName;
        private System.Windows.Forms.ColumnHeader colBirthDate;
        private System.Windows.Forms.ColumnHeader colHholdId;
        private System.Windows.Forms.ColumnHeader colFirstName;
        private System.Windows.Forms.ColumnHeader colAge;
        private System.Windows.Forms.ColumnHeader colHHMInactive;
    }
}