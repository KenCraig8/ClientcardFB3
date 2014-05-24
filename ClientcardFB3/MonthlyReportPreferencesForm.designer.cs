namespace ClientcardFB3
{
    partial class MonthlyReportPreferencesForm
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
            this.dataGridViewReicpitents = new System.Windows.Forms.DataGridView();
            this.clmAddRecpt = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.clmID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmEmailAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbTemplatePath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridViewReports = new System.Windows.Forms.DataGridView();
            this.clmReportActive = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.clmMonthlyReports = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmReportId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAddRecipients = new System.Windows.Forms.Button();
            this.btnDeleteRecipients = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReicpitents)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReports)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewReicpitents
            // 
            this.dataGridViewReicpitents.AllowUserToAddRows = false;
            this.dataGridViewReicpitents.AllowUserToDeleteRows = false;
            this.dataGridViewReicpitents.AllowUserToResizeColumns = false;
            this.dataGridViewReicpitents.AllowUserToResizeRows = false;
            this.dataGridViewReicpitents.BackgroundColor = System.Drawing.Color.Cornsilk;
            this.dataGridViewReicpitents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewReicpitents.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmAddRecpt,
            this.clmID,
            this.clmName,
            this.clmEmailAddress});
            this.dataGridViewReicpitents.Location = new System.Drawing.Point(366, 48);
            this.dataGridViewReicpitents.MultiSelect = false;
            this.dataGridViewReicpitents.Name = "dataGridViewReicpitents";
            this.dataGridViewReicpitents.RowHeadersVisible = false;
            this.dataGridViewReicpitents.RowTemplate.Height = 24;
            this.dataGridViewReicpitents.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridViewReicpitents.Size = new System.Drawing.Size(633, 276);
            this.dataGridViewReicpitents.TabIndex = 2;
            this.dataGridViewReicpitents.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewReicpitents_CellEndEdit);
            this.dataGridViewReicpitents.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewReicpitents_CellMouseUp);
            this.dataGridViewReicpitents.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewReicpitents_RowEnter);
            this.dataGridViewReicpitents.Leave += new System.EventHandler(this.dataGridViewReicpitents_Leave);
            // 
            // clmAddRecpt
            // 
            this.clmAddRecpt.HeaderText = "";
            this.clmAddRecpt.Name = "clmAddRecpt";
            this.clmAddRecpt.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clmAddRecpt.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.clmAddRecpt.Width = 30;
            // 
            // clmID
            // 
            this.clmID.HeaderText = "ID";
            this.clmID.Name = "clmID";
            this.clmID.Visible = false;
            // 
            // clmName
            // 
            this.clmName.HeaderText = "Recipient Name";
            this.clmName.Name = "clmName";
            this.clmName.Width = 240;
            // 
            // clmEmailAddress
            // 
            this.clmEmailAddress.HeaderText = "Email Address";
            this.clmEmailAddress.Name = "clmEmailAddress";
            this.clmEmailAddress.Width = 360;
            // 
            // tbTemplatePath
            // 
            this.tbTemplatePath.Location = new System.Drawing.Point(12, 351);
            this.tbTemplatePath.Name = "tbTemplatePath";
            this.tbTemplatePath.Size = new System.Drawing.Size(987, 26);
            this.tbTemplatePath.TabIndex = 3;
            this.tbTemplatePath.Tag = "ReportPath";
            this.tbTemplatePath.Leave += new System.EventHandler(this.tbTemplatePath_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 327);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(177, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Report Template Path:";
            // 
            // dataGridViewReports
            // 
            this.dataGridViewReports.AllowUserToAddRows = false;
            this.dataGridViewReports.AllowUserToDeleteRows = false;
            this.dataGridViewReports.AllowUserToResizeColumns = false;
            this.dataGridViewReports.AllowUserToResizeRows = false;
            this.dataGridViewReports.BackgroundColor = System.Drawing.Color.Cornsilk;
            this.dataGridViewReports.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewReports.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmReportActive,
            this.clmMonthlyReports,
            this.clmReportId});
            this.dataGridViewReports.Location = new System.Drawing.Point(12, 33);
            this.dataGridViewReports.Name = "dataGridViewReports";
            this.dataGridViewReports.RowHeadersVisible = false;
            this.dataGridViewReports.RowTemplate.Height = 24;
            this.dataGridViewReports.Size = new System.Drawing.Size(340, 291);
            this.dataGridViewReports.TabIndex = 5;
            this.dataGridViewReports.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewReports_CellMouseUp);
            this.dataGridViewReports.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewReports_CellValueChanged);
            this.dataGridViewReports.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewReports_RowEnter);
            // 
            // clmReportActive
            // 
            this.clmReportActive.HeaderText = "";
            this.clmReportActive.Name = "clmReportActive";
            this.clmReportActive.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clmReportActive.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.clmReportActive.Width = 30;
            // 
            // clmMonthlyReports
            // 
            this.clmMonthlyReports.HeaderText = "Monthly Report List";
            this.clmMonthlyReports.Name = "clmMonthlyReports";
            this.clmMonthlyReports.ReadOnly = true;
            this.clmMonthlyReports.Width = 300;
            // 
            // clmReportId
            // 
            this.clmReportId.HeaderText = "ID";
            this.clmReportId.Name = "clmReportId";
            this.clmReportId.Visible = false;
            // 
            // btnAddRecipients
            // 
            this.btnAddRecipients.Location = new System.Drawing.Point(705, 12);
            this.btnAddRecipients.Name = "btnAddRecipients";
            this.btnAddRecipients.Size = new System.Drawing.Size(136, 30);
            this.btnAddRecipients.TabIndex = 6;
            this.btnAddRecipients.Text = "Add Recipient";
            this.btnAddRecipients.UseVisualStyleBackColor = true;
            this.btnAddRecipients.Click += new System.EventHandler(this.btnAddRecipients_Click);
            // 
            // btnDeleteRecipients
            // 
            this.btnDeleteRecipients.Location = new System.Drawing.Point(863, 12);
            this.btnDeleteRecipients.Name = "btnDeleteRecipients";
            this.btnDeleteRecipients.Size = new System.Drawing.Size(136, 30);
            this.btnDeleteRecipients.TabIndex = 7;
            this.btnDeleteRecipients.Text = "Delete Recipient";
            this.btnDeleteRecipients.UseVisualStyleBackColor = true;
            this.btnDeleteRecipients.Click += new System.EventHandler(this.btnDeleteRecipients_Click);
            // 
            // MonthlyReportPrefrencesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Cornsilk;
            this.ClientSize = new System.Drawing.Size(1005, 483);
            this.Controls.Add(this.btnDeleteRecipients);
            this.Controls.Add(this.btnAddRecipients);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbTemplatePath);
            this.Controls.Add(this.dataGridViewReicpitents);
            this.Controls.Add(this.dataGridViewReports);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MonthlyReportPrefrencesForm";
            this.Text = "Monthly Report Preferences Form";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MonthlyReportPrefrences_FormClosing);
            this.Load += new System.EventHandler(this.MonthlyReportPrefrences_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReicpitents)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReports)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbTemplatePath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridViewReports;
        private System.Windows.Forms.Button btnAddRecipients;
        private System.Windows.Forms.Button btnDeleteRecipients;
        private System.Windows.Forms.DataGridView dataGridViewReicpitents;
        private System.Windows.Forms.DataGridViewCheckBoxColumn clmReportActive;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmMonthlyReports;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmReportId;
        private System.Windows.Forms.DataGridViewCheckBoxColumn clmAddRecpt;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmID;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmEmailAddress;
    }
}