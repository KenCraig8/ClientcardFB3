namespace ClientcardFB3
{
    partial class CCFBStatisticsForm
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
            this.lvwData = new System.Windows.Forms.ListView();
            this.clmCnt = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTable = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmInactive = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colNbrRows = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmSurveyCmplt = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmSingleParent = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmUseFamilyList = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmNeedPOA = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmNeedTEFAP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmNoTEFAP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmSupplOnly = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmHomeless = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmNeedIncome = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmBabyServices = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmInfants = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmYouth = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmTeens = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmEighteen = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmAdults = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmSeniors = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmTotalFamily = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmAvgFamilySize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnLoadStats = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cboTableType = new System.Windows.Forms.ComboBox();
            this.grpbxActive = new System.Windows.Forms.GroupBox();
            this.rdoOnlyInactive = new System.Windows.Forms.RadioButton();
            this.rdoOnlyActive = new System.Windows.Forms.RadioButton();
            this.rdoAll = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.grpbxActive.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvwData
            // 
            this.lvwData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvwData.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmCnt,
            this.colTable,
            this.clmInactive,
            this.colNbrRows,
            this.clmSurveyCmplt,
            this.clmSingleParent,
            this.clmUseFamilyList,
            this.clmNeedPOA,
            this.clmNeedTEFAP,
            this.clmNoTEFAP,
            this.clmSupplOnly,
            this.clmHomeless,
            this.clmNeedIncome,
            this.clmBabyServices,
            this.clmInfants,
            this.clmYouth,
            this.clmTeens,
            this.clmEighteen,
            this.clmAdults,
            this.clmSeniors,
            this.clmTotalFamily,
            this.clmAvgFamilySize});
            this.lvwData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwData.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvwData.FullRowSelect = true;
            this.lvwData.GridLines = true;
            this.lvwData.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvwData.LabelWrap = false;
            this.lvwData.Location = new System.Drawing.Point(0, 0);
            this.lvwData.MultiSelect = false;
            this.lvwData.Name = "lvwData";
            this.lvwData.Size = new System.Drawing.Size(1008, 344);
            this.lvwData.TabIndex = 0;
            this.lvwData.UseCompatibleStateImageBehavior = false;
            this.lvwData.View = System.Windows.Forms.View.Details;
            // 
            // clmCnt
            // 
            this.clmCnt.Tag = "0";
            this.clmCnt.Text = "Cnt";
            this.clmCnt.Width = 0;
            // 
            // colTable
            // 
            this.colTable.Tag = "0";
            this.colTable.Text = "Data Table";
            this.colTable.Width = 120;
            // 
            // clmInactive
            // 
            this.clmInactive.Tag = "0";
            this.clmInactive.Text = "Active";
            this.clmInactive.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // colNbrRows
            // 
            this.colNbrRows.Tag = "1";
            this.colNbrRows.Text = "Rows";
            this.colNbrRows.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colNbrRows.Width = 70;
            // 
            // clmSurveyCmplt
            // 
            this.clmSurveyCmplt.Tag = "1";
            this.clmSurveyCmplt.Text = "Survey";
            this.clmSurveyCmplt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // clmSingleParent
            // 
            this.clmSingleParent.Tag = "1";
            this.clmSingleParent.Text = "1Parent";
            this.clmSingleParent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // clmUseFamilyList
            // 
            this.clmUseFamilyList.Tag = "1";
            this.clmUseFamilyList.Text = "UseFList";
            this.clmUseFamilyList.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // clmNeedPOA
            // 
            this.clmNeedPOA.Tag = "1";
            this.clmNeedPOA.Text = "Nd POA";
            this.clmNeedPOA.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // clmNeedTEFAP
            // 
            this.clmNeedTEFAP.Tag = "1";
            this.clmNeedTEFAP.Text = "Nd TEFAP";
            this.clmNeedTEFAP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // clmNoTEFAP
            // 
            this.clmNoTEFAP.Tag = "1";
            this.clmNoTEFAP.Text = "No TEFAP";
            this.clmNoTEFAP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // clmSupplOnly
            // 
            this.clmSupplOnly.Tag = "1";
            this.clmSupplOnly.Text = "Suppl Only";
            this.clmSupplOnly.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // clmHomeless
            // 
            this.clmHomeless.Tag = "1";
            this.clmHomeless.Text = "Homeless";
            this.clmHomeless.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // clmNeedIncome
            // 
            this.clmNeedIncome.Tag = "1";
            this.clmNeedIncome.Text = "Nd Income";
            this.clmNeedIncome.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // clmBabyServices
            // 
            this.clmBabyServices.Tag = "1";
            this.clmBabyServices.Text = "Baby";
            this.clmBabyServices.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // clmInfants
            // 
            this.clmInfants.Tag = "1";
            this.clmInfants.Text = "Infants";
            this.clmInfants.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // clmYouth
            // 
            this.clmYouth.Tag = "1";
            this.clmYouth.Text = "Youth";
            this.clmYouth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // clmTeens
            // 
            this.clmTeens.Tag = "1";
            this.clmTeens.Text = "Teens";
            this.clmTeens.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // clmEighteen
            // 
            this.clmEighteen.Tag = "1";
            this.clmEighteen.Text = "18";
            this.clmEighteen.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // clmAdults
            // 
            this.clmAdults.Tag = "1";
            this.clmAdults.Text = "Adults";
            this.clmAdults.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // clmSeniors
            // 
            this.clmSeniors.Tag = "1";
            this.clmSeniors.Text = "Seniors";
            this.clmSeniors.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // clmTotalFamily
            // 
            this.clmTotalFamily.Tag = "1";
            this.clmTotalFamily.Text = "All Indiv";
            this.clmTotalFamily.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.clmTotalFamily.Width = 80;
            // 
            // clmAvgFamilySize
            // 
            this.clmAvgFamilySize.Tag = "2";
            this.clmAvgFamilySize.Text = "Avg Size";
            this.clmAvgFamilySize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.clmAvgFamilySize.Width = 70;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnLoadStats);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.cboTableType);
            this.splitContainer1.Panel1.Controls.Add(this.grpbxActive);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lvwData);
            this.splitContainer1.Size = new System.Drawing.Size(1008, 444);
            this.splitContainer1.SplitterDistance = 96;
            this.splitContainer1.TabIndex = 1;
            // 
            // btnLoadStats
            // 
            this.btnLoadStats.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadStats.Location = new System.Drawing.Point(215, 31);
            this.btnLoadStats.Name = "btnLoadStats";
            this.btnLoadStats.Size = new System.Drawing.Size(85, 24);
            this.btnLoadStats.TabIndex = 19;
            this.btnLoadStats.Text = "Load Stats";
            this.btnLoadStats.UseVisualStyleBackColor = true;
            this.btnLoadStats.Click += new System.EventHandler(this.btnLoadStats_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 17);
            this.label1.TabIndex = 18;
            this.label1.Text = "Statistics Type";
            // 
            // cboTableType
            // 
            this.cboTableType.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboTableType.FormattingEnabled = true;
            this.cboTableType.Items.AddRange(new object[] {
            "Households",
            "Households By Category",
            "Household Members",
            "Donors",
            "Donations - Cash",
            "Food Receipts",
            "Food Services",
            "Volunteers",
            "Volunteer Hours"});
            this.cboTableType.Location = new System.Drawing.Point(17, 31);
            this.cboTableType.Name = "cboTableType";
            this.cboTableType.Size = new System.Drawing.Size(182, 24);
            this.cboTableType.TabIndex = 17;
            // 
            // grpbxActive
            // 
            this.grpbxActive.Controls.Add(this.rdoOnlyInactive);
            this.grpbxActive.Controls.Add(this.rdoOnlyActive);
            this.grpbxActive.Controls.Add(this.rdoAll);
            this.grpbxActive.Location = new System.Drawing.Point(319, 22);
            this.grpbxActive.Name = "grpbxActive";
            this.grpbxActive.Size = new System.Drawing.Size(274, 38);
            this.grpbxActive.TabIndex = 16;
            this.grpbxActive.TabStop = false;
            // 
            // rdoOnlyInactive
            // 
            this.rdoOnlyInactive.AutoSize = true;
            this.rdoOnlyInactive.Location = new System.Drawing.Point(94, 12);
            this.rdoOnlyInactive.Name = "rdoOnlyInactive";
            this.rdoOnlyInactive.Size = new System.Drawing.Size(87, 17);
            this.rdoOnlyInactive.TabIndex = 2;
            this.rdoOnlyInactive.Text = "Only Inactive";
            this.rdoOnlyInactive.UseVisualStyleBackColor = true;
            // 
            // rdoOnlyActive
            // 
            this.rdoOnlyActive.AutoSize = true;
            this.rdoOnlyActive.Checked = true;
            this.rdoOnlyActive.Location = new System.Drawing.Point(6, 12);
            this.rdoOnlyActive.Name = "rdoOnlyActive";
            this.rdoOnlyActive.Size = new System.Drawing.Size(82, 17);
            this.rdoOnlyActive.TabIndex = 1;
            this.rdoOnlyActive.TabStop = true;
            this.rdoOnlyActive.Text = "Only Active ";
            this.rdoOnlyActive.UseVisualStyleBackColor = true;
            // 
            // rdoAll
            // 
            this.rdoAll.AutoSize = true;
            this.rdoAll.Location = new System.Drawing.Point(187, 12);
            this.rdoAll.Name = "rdoAll";
            this.rdoAll.Size = new System.Drawing.Size(79, 17);
            this.rdoAll.TabIndex = 0;
            this.rdoAll.Text = "All Records";
            this.rdoAll.UseVisualStyleBackColor = true;
            // 
            // CCFBStatisticsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 444);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "CCFBStatisticsForm";
            this.Text = "CCFBStatisticsForm";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.grpbxActive.ResumeLayout(false);
            this.grpbxActive.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvwData;
        private System.Windows.Forms.ColumnHeader colTable;
        private System.Windows.Forms.ColumnHeader colNbrRows;
        private System.Windows.Forms.ColumnHeader clmInactive;
        private System.Windows.Forms.ColumnHeader clmSurveyCmplt;
        private System.Windows.Forms.ColumnHeader clmSingleParent;
        private System.Windows.Forms.ColumnHeader clmUseFamilyList;
        private System.Windows.Forms.ColumnHeader clmNeedPOA;
        private System.Windows.Forms.ColumnHeader clmNeedTEFAP;
        private System.Windows.Forms.ColumnHeader clmNoTEFAP;
        private System.Windows.Forms.ColumnHeader clmSupplOnly;
        private System.Windows.Forms.ColumnHeader clmHomeless;
        private System.Windows.Forms.ColumnHeader clmNeedIncome;
        private System.Windows.Forms.ColumnHeader clmBabyServices;
        private System.Windows.Forms.ColumnHeader clmInfants;
        private System.Windows.Forms.ColumnHeader clmYouth;
        private System.Windows.Forms.ColumnHeader clmTeens;
        private System.Windows.Forms.ColumnHeader clmEighteen;
        private System.Windows.Forms.ColumnHeader clmAdults;
        private System.Windows.Forms.ColumnHeader clmSeniors;
        private System.Windows.Forms.ColumnHeader clmTotalFamily;
        private System.Windows.Forms.ColumnHeader clmAvgFamilySize;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnLoadStats;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboTableType;
        private System.Windows.Forms.GroupBox grpbxActive;
        private System.Windows.Forms.RadioButton rdoOnlyInactive;
        private System.Windows.Forms.RadioButton rdoOnlyActive;
        private System.Windows.Forms.RadioButton rdoAll;
        private System.Windows.Forms.ColumnHeader clmCnt;
    }
}