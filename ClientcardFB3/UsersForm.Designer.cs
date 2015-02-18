namespace ClientcardFB3
{
    partial class UsersForm
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
                if (clsUsers != null)
                {
                    clsUsers.Dispose();
                }
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridViewUsers = new System.Windows.Forms.DataGridView();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSetPassword = new System.Windows.Forms.Button();
            this.pannelPassword = new System.Windows.Forms.Panel();
            this.lblUsername = new System.Windows.Forms.Label();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnCancelSetPwd = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tbPassword1 = new System.Windows.Forms.TextBox();
            this.tbPassword2 = new System.Windows.Forms.TextBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.clmUserName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmUserRole = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.clmId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUsers)).BeginInit();
            this.pannelPassword.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewUsers
            // 
            this.dataGridViewUsers.BackgroundColor = System.Drawing.Color.Cornsilk;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Tan;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewUsers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewUsers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmUserName,
            this.clmUserRole,
            this.clmId});
            this.dataGridViewUsers.Location = new System.Drawing.Point(2, 2);
            this.dataGridViewUsers.MultiSelect = false;
            this.dataGridViewUsers.Name = "dataGridViewUsers";
            this.dataGridViewUsers.RowTemplate.Height = 24;
            this.dataGridViewUsers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewUsers.Size = new System.Drawing.Size(392, 407);
            this.dataGridViewUsers.TabIndex = 0;
            this.dataGridViewUsers.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewUsers_CellContentClick);
            this.dataGridViewUsers.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewUsers_CellEndEdit);
            this.dataGridViewUsers.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dataGridViewUsers_UserAddedRow);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(549, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(45, 23);
            this.btnClose.TabIndex = 9;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSetPassword
            // 
            this.btnSetPassword.Location = new System.Drawing.Point(403, 72);
            this.btnSetPassword.Name = "btnSetPassword";
            this.btnSetPassword.Size = new System.Drawing.Size(94, 23);
            this.btnSetPassword.TabIndex = 1;
            this.btnSetPassword.Text = "&Reset Password";
            this.btnSetPassword.UseVisualStyleBackColor = true;
            this.btnSetPassword.Click += new System.EventHandler(this.btnSetPassword_Click);
            // 
            // pannelPassword
            // 
            this.pannelPassword.BackColor = System.Drawing.Color.LightGreen;
            this.pannelPassword.Controls.Add(this.lblUsername);
            this.pannelPassword.Controls.Add(this.btnConfirm);
            this.pannelPassword.Controls.Add(this.label3);
            this.pannelPassword.Controls.Add(this.btnCancelSetPwd);
            this.pannelPassword.Controls.Add(this.label2);
            this.pannelPassword.Controls.Add(this.tbPassword1);
            this.pannelPassword.Controls.Add(this.tbPassword2);
            this.pannelPassword.Location = new System.Drawing.Point(400, 72);
            this.pannelPassword.Name = "pannelPassword";
            this.pannelPassword.Size = new System.Drawing.Size(194, 115);
            this.pannelPassword.TabIndex = 2;
            // 
            // lblUsername
            // 
            this.lblUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsername.Location = new System.Drawing.Point(22, 6);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(164, 27);
            this.lblUsername.TabIndex = 9;
            this.lblUsername.Text = "[Admin]";
            this.lblUsername.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(112, 80);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(65, 23);
            this.btnConfirm.TabIndex = 7;
            this.btnConfirm.Text = "&Set Pwd";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Confirm Password:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnCancelSetPwd
            // 
            this.btnCancelSetPwd.Location = new System.Drawing.Point(22, 80);
            this.btnCancelSetPwd.Name = "btnCancelSetPwd";
            this.btnCancelSetPwd.Size = new System.Drawing.Size(65, 23);
            this.btnCancelSetPwd.TabIndex = 8;
            this.btnCancelSetPwd.Text = "Cancel";
            this.btnCancelSetPwd.UseVisualStyleBackColor = true;
            this.btnCancelSetPwd.Click += new System.EventHandler(this.btnCancelSetPwd_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "New Password:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbPassword1
            // 
            this.tbPassword1.Location = new System.Drawing.Point(101, 34);
            this.tbPassword1.Name = "tbPassword1";
            this.tbPassword1.Size = new System.Drawing.Size(86, 20);
            this.tbPassword1.TabIndex = 4;
            this.tbPassword1.UseSystemPasswordChar = true;
            // 
            // tbPassword2
            // 
            this.tbPassword2.Location = new System.Drawing.Point(101, 55);
            this.tbPassword2.Name = "tbPassword2";
            this.tbPassword2.Size = new System.Drawing.Size(86, 20);
            this.tbPassword2.TabIndex = 6;
            this.tbPassword2.UseSystemPasswordChar = true;
            this.tbPassword2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbPassword2_KeyPress);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(403, 23);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(94, 23);
            this.btnDelete.TabIndex = 10;
            this.btnDelete.TabStop = false;
            this.btnDelete.Text = "Delete User";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // clmUserName
            // 
            this.clmUserName.HeaderText = "User name";
            this.clmUserName.Name = "clmUserName";
            this.clmUserName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.clmUserName.Width = 140;
            // 
            // clmUserRole
            // 
            this.clmUserRole.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.clmUserRole.HeaderText = "Role";
            this.clmUserRole.Items.AddRange(new object[] {
            "Admin",
            "IntakeAdmin",
            "Intake"});
            this.clmUserRole.Name = "clmUserRole";
            this.clmUserRole.Width = 200;
            // 
            // clmId
            // 
            this.clmId.HeaderText = "ID";
            this.clmId.Name = "clmId";
            this.clmId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.clmId.Visible = false;
            // 
            // UsersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Cornsilk;
            this.ClientSize = new System.Drawing.Size(597, 417);
            this.Controls.Add(this.btnSetPassword);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.pannelPassword);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dataGridViewUsers);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "UsersForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit User List";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UsersForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUsers)).EndInit();
            this.pannelPassword.ResumeLayout(false);
            this.pannelPassword.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewUsers;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSetPassword;
        private System.Windows.Forms.Panel pannelPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbPassword1;
        private System.Windows.Forms.TextBox tbPassword2;
        private System.Windows.Forms.Button btnCancelSetPwd;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmUserName;
        private System.Windows.Forms.DataGridViewComboBoxColumn clmUserRole;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmId;
    }
}