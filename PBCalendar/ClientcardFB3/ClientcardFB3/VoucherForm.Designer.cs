namespace ClientcardFB3
{
    partial class VoucherForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dtpTrxDate = new System.Windows.Forms.DateTimePicker();
            this.btnClose = new System.Windows.Forms.Button();
            this.gridVouchers = new System.Windows.Forms.DataGridView();
            this.label8 = new System.Windows.Forms.Label();
            this.btnPost = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpVouchers = new System.Windows.Forms.TabPage();
            this.tpDemographics = new System.Windows.Forms.TabPage();
            this.tbEighteen = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbInfants = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tbYouth = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.tbTeens = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.tbAdults = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.tbSeniors = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.tbTotalFam = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.tbDisabled = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.tbClient = new System.Windows.Forms.TextBox();
            this.clmDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmComments = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridVouchers)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tpVouchers.SuspendLayout();
            this.tpDemographics.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tbClient);
            this.splitContainer1.Panel1.Controls.Add(this.dtpTrxDate);
            this.splitContainer1.Panel1.Controls.Add(this.btnClose);
            this.splitContainer1.Panel1.Controls.Add(this.label8);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(671, 424);
            this.splitContainer1.SplitterDistance = 110;
            this.splitContainer1.TabIndex = 0;
            // 
            // dtpTrxDate
            // 
            this.dtpTrxDate.Location = new System.Drawing.Point(264, 32);
            this.dtpTrxDate.Name = "dtpTrxDate";
            this.dtpTrxDate.Size = new System.Drawing.Size(248, 23);
            this.dtpTrxDate.TabIndex = 101;
            this.dtpTrxDate.ValueChanged += new System.EventHandler(this.dtpTrxDate_ValueChanged);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(584, 8);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(78, 36);
            this.btnClose.TabIndex = 100;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // gridVouchers
            // 
            this.gridVouchers.AllowUserToAddRows = false;
            this.gridVouchers.AllowUserToDeleteRows = false;
            this.gridVouchers.AllowUserToOrderColumns = true;
            this.gridVouchers.AllowUserToResizeRows = false;
            this.gridVouchers.BackgroundColor = System.Drawing.Color.Cornsilk;
            this.gridVouchers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridVouchers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridVouchers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridVouchers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmDesc,
            this.clmAmount,
            this.clmComments});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridVouchers.DefaultCellStyle = dataGridViewCellStyle5;
            this.gridVouchers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridVouchers.Location = new System.Drawing.Point(3, 3);
            this.gridVouchers.MultiSelect = false;
            this.gridVouchers.Name = "gridVouchers";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridVouchers.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.gridVouchers.RowHeadersVisible = false;
            this.gridVouchers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.gridVouchers.Size = new System.Drawing.Size(657, 275);
            this.gridVouchers.TabIndex = 99;
            this.gridVouchers.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridVouchers_RowEnter);
            this.gridVouchers.RowLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridVouchers_RowLeave);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(267, 13);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 17);
            this.label8.TabIndex = 98;
            this.label8.Text = "Trx Date";
            // 
            // btnPost
            // 
            this.btnPost.Location = new System.Drawing.Point(440, 24);
            this.btnPost.Name = "btnPost";
            this.btnPost.Size = new System.Drawing.Size(203, 31);
            this.btnPost.TabIndex = 76;
            this.btnPost.Text = "Post Demographics";
            this.btnPost.UseVisualStyleBackColor = true;
            this.btnPost.Click += new System.EventHandler(this.btnPost_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpVouchers);
            this.tabControl1.Controls.Add(this.tpDemographics);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(671, 310);
            this.tabControl1.TabIndex = 0;
            // 
            // tpVouchers
            // 
            this.tpVouchers.Controls.Add(this.gridVouchers);
            this.tpVouchers.Location = new System.Drawing.Point(4, 25);
            this.tpVouchers.Name = "tpVouchers";
            this.tpVouchers.Padding = new System.Windows.Forms.Padding(3);
            this.tpVouchers.Size = new System.Drawing.Size(663, 281);
            this.tpVouchers.TabIndex = 0;
            this.tpVouchers.Text = "Vouchers";
            this.tpVouchers.UseVisualStyleBackColor = true;
            // 
            // tpDemographics
            // 
            this.tpDemographics.Controls.Add(this.tbEighteen);
            this.tpDemographics.Controls.Add(this.label1);
            this.tpDemographics.Controls.Add(this.tbInfants);
            this.tpDemographics.Controls.Add(this.label12);
            this.tpDemographics.Controls.Add(this.tbYouth);
            this.tpDemographics.Controls.Add(this.label13);
            this.tpDemographics.Controls.Add(this.tbTeens);
            this.tpDemographics.Controls.Add(this.label15);
            this.tpDemographics.Controls.Add(this.tbAdults);
            this.tpDemographics.Controls.Add(this.label14);
            this.tpDemographics.Controls.Add(this.tbSeniors);
            this.tpDemographics.Controls.Add(this.label17);
            this.tpDemographics.Controls.Add(this.tbTotalFam);
            this.tpDemographics.Controls.Add(this.label16);
            this.tpDemographics.Controls.Add(this.tbDisabled);
            this.tpDemographics.Controls.Add(this.label21);
            this.tpDemographics.Controls.Add(this.btnPost);
            this.tpDemographics.Location = new System.Drawing.Point(4, 25);
            this.tpDemographics.Name = "tpDemographics";
            this.tpDemographics.Padding = new System.Windows.Forms.Padding(3);
            this.tpDemographics.Size = new System.Drawing.Size(663, 282);
            this.tpDemographics.TabIndex = 1;
            this.tpDemographics.Text = "Demographics";
            this.tpDemographics.UseVisualStyleBackColor = true;
            // 
            // tbEighteen
            // 
            this.tbEighteen.Location = new System.Drawing.Point(157, 33);
            this.tbEighteen.Margin = new System.Windows.Forms.Padding(4);
            this.tbEighteen.Name = "tbEighteen";
            this.tbEighteen.Size = new System.Drawing.Size(41, 23);
            this.tbEighteen.TabIndex = 119;
            this.tbEighteen.Tag = "Eighteen";
            this.tbEighteen.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(154, 13);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 17);
            this.label1.TabIndex = 120;
            this.label1.Text = "18\'s";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbInfants
            // 
            this.tbInfants.Location = new System.Drawing.Point(16, 33);
            this.tbInfants.Margin = new System.Windows.Forms.Padding(4);
            this.tbInfants.Name = "tbInfants";
            this.tbInfants.Size = new System.Drawing.Size(41, 23);
            this.tbInfants.TabIndex = 105;
            this.tbInfants.Tag = "Infants";
            this.tbInfants.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(13, 13);
            this.label12.Margin = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(48, 17);
            this.label12.TabIndex = 111;
            this.label12.Text = "Infants";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbYouth
            // 
            this.tbYouth.Location = new System.Drawing.Point(64, 32);
            this.tbYouth.Margin = new System.Windows.Forms.Padding(4);
            this.tbYouth.Name = "tbYouth";
            this.tbYouth.Size = new System.Drawing.Size(41, 23);
            this.tbYouth.TabIndex = 106;
            this.tbYouth.Tag = "Youth";
            this.tbYouth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(61, 13);
            this.label13.Margin = new System.Windows.Forms.Padding(0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(48, 17);
            this.label13.TabIndex = 112;
            this.label13.Text = "Youth";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbTeens
            // 
            this.tbTeens.Location = new System.Drawing.Point(112, 33);
            this.tbTeens.Margin = new System.Windows.Forms.Padding(4);
            this.tbTeens.Name = "tbTeens";
            this.tbTeens.Size = new System.Drawing.Size(41, 23);
            this.tbTeens.TabIndex = 107;
            this.tbTeens.Tag = "Teens";
            this.tbTeens.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label15
            // 
            this.label15.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(109, 13);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(48, 17);
            this.label15.TabIndex = 113;
            this.label15.Text = "Teens";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbAdults
            // 
            this.tbAdults.Location = new System.Drawing.Point(202, 33);
            this.tbAdults.Margin = new System.Windows.Forms.Padding(4);
            this.tbAdults.Name = "tbAdults";
            this.tbAdults.Size = new System.Drawing.Size(41, 23);
            this.tbAdults.TabIndex = 108;
            this.tbAdults.Tag = "Adults";
            this.tbAdults.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(199, 15);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(43, 17);
            this.label14.TabIndex = 114;
            this.label14.Text = "Adults";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbSeniors
            // 
            this.tbSeniors.Location = new System.Drawing.Point(245, 34);
            this.tbSeniors.Margin = new System.Windows.Forms.Padding(4);
            this.tbSeniors.Name = "tbSeniors";
            this.tbSeniors.Size = new System.Drawing.Size(41, 23);
            this.tbSeniors.TabIndex = 109;
            this.tbSeniors.Tag = "Seniors";
            this.tbSeniors.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label17
            // 
            this.label17.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(242, 14);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(56, 17);
            this.label17.TabIndex = 115;
            this.label17.Text = "Seniors:";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbTotalFam
            // 
            this.tbTotalFam.BackColor = System.Drawing.Color.Cornsilk;
            this.tbTotalFam.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbTotalFam.ForeColor = System.Drawing.Color.Blue;
            this.tbTotalFam.Location = new System.Drawing.Point(299, 34);
            this.tbTotalFam.Margin = new System.Windows.Forms.Padding(4);
            this.tbTotalFam.Name = "tbTotalFam";
            this.tbTotalFam.ReadOnly = true;
            this.tbTotalFam.Size = new System.Drawing.Size(41, 22);
            this.tbTotalFam.TabIndex = 110;
            this.tbTotalFam.TabStop = false;
            this.tbTotalFam.Tag = "TotalFamily";
            this.tbTotalFam.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label16
            // 
            this.label16.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(296, 13);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(96, 18);
            this.label16.TabIndex = 116;
            this.label16.Text = "Total Family";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbDisabled
            // 
            this.tbDisabled.Location = new System.Drawing.Point(13, 69);
            this.tbDisabled.Margin = new System.Windows.Forms.Padding(4);
            this.tbDisabled.Name = "tbDisabled";
            this.tbDisabled.Size = new System.Drawing.Size(41, 23);
            this.tbDisabled.TabIndex = 117;
            this.tbDisabled.Tag = "Disabled";
            this.tbDisabled.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(56, 72);
            this.label21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(56, 13);
            this.label21.TabIndex = 118;
            this.label21.Text = "Disabled";
            // 
            // tbClient
            // 
            this.tbClient.BackColor = System.Drawing.Color.LightYellow;
            this.tbClient.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.tbClient.Location = new System.Drawing.Point(8, 8);
            this.tbClient.Multiline = true;
            this.tbClient.Name = "tbClient";
            this.tbClient.ReadOnly = true;
            this.tbClient.Size = new System.Drawing.Size(248, 96);
            this.tbClient.TabIndex = 102;
            // 
            // clmDesc
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            this.clmDesc.DefaultCellStyle = dataGridViewCellStyle2;
            this.clmDesc.HeaderText = "Description";
            this.clmDesc.Name = "clmDesc";
            this.clmDesc.ReadOnly = true;
            this.clmDesc.Width = 150;
            // 
            // clmAmount
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            this.clmAmount.DefaultCellStyle = dataGridViewCellStyle3;
            this.clmAmount.HeaderText = "Amount";
            this.clmAmount.Name = "clmAmount";
            // 
            // clmComments
            // 
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            this.clmComments.DefaultCellStyle = dataGridViewCellStyle4;
            this.clmComments.HeaderText = "Coments";
            this.clmComments.Name = "clmComments";
            this.clmComments.Width = 400;
            // 
            // VoucherForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Cornsilk;
            this.ClientSize = new System.Drawing.Size(671, 424);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "VoucherForm";
            this.Text = "VoucherForm";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridVouchers)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tpVouchers.ResumeLayout(false);
            this.tpDemographics.ResumeLayout(false);
            this.tpDemographics.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DateTimePicker dtpTrxDate;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnPost;
        private System.Windows.Forms.DataGridView gridVouchers;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpVouchers;
        private System.Windows.Forms.TabPage tpDemographics;
        private System.Windows.Forms.TextBox tbEighteen;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbInfants;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbYouth;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tbTeens;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox tbAdults;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox tbSeniors;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox tbTotalFam;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox tbDisabled;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox tbClient;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmDesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmComments;


    }
}