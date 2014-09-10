namespace ClientcardFB3
{
    partial class FastTrackForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FastTrackForm));
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.dgvFT = new System.Windows.Forms.DataGridView();
            this.colHHID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFamilySize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFoodList = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBabySvcs = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNonFood = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNotes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLbsStd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLbsOther = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLbsTEFAP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLbsSuppl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLbsBaby = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDone = new System.Windows.Forms.DataGridViewButtonColumn();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lblFBName = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SelectedNameLabel = new System.Windows.Forms.Label();
            this.SelectedIdLabel = new System.Windows.Forms.Label();
            this.SelectedLabel = new System.Windows.Forms.Label();
            this.clearTotalWt = new System.Windows.Forms.Button();
            this.scaleWeightText = new System.Windows.Forms.TextBox();
            this.addWeightButton = new System.Windows.Forms.Button();
            this.tbTotalScaleWt = new System.Windows.Forms.TextBox();
            this.totalWeightText = new System.Windows.Forms.TextBox();
            this.tbScaleWt = new System.Windows.Forms.TextBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.enableScaleFeature = new System.Windows.Forms.ToolStripMenuItem();
            this.enableScale = new System.Windows.Forms.ToolStripMenuItem();
            this.disableScale = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tssStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslblsep = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslblMsg = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer
            // 
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // dgvFT
            // 
            this.dgvFT.AllowUserToAddRows = false;
            this.dgvFT.AllowUserToDeleteRows = false;
            this.dgvFT.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.LightBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvFT.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvFT.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.dgvFT.BackgroundColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.LightYellow;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightCyan;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvFT.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvFT.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colHHID,
            this.colName,
            this.colFamilySize,
            this.colFoodList,
            this.colBabySvcs,
            this.colNonFood,
            this.colNotes,
            this.colLbsStd,
            this.colLbsOther,
            this.colLbsTEFAP,
            this.colLbsSuppl,
            this.colLbsBaby,
            this.colDone});
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.LemonChiffon;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.Color.LightBlue;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvFT.DefaultCellStyle = dataGridViewCellStyle13;
            this.dgvFT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFT.GridColor = System.Drawing.Color.DimGray;
            this.dgvFT.Location = new System.Drawing.Point(0, 26);
            this.dgvFT.Name = "dgvFT";
            this.dgvFT.RowHeadersWidth = 10;
            this.dgvFT.RowTemplate.Height = 88;
            this.dgvFT.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvFT.Size = new System.Drawing.Size(1276, 561);
            this.dgvFT.TabIndex = 24;
            this.dgvFT.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvFT_CellBeginEdit);
            this.dgvFT.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFT_CellEndEdit);
            this.dgvFT.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvFT_CellMouseClick);
            this.dgvFT.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvFT_CellMouseDoubleClick);
            this.dgvFT.SelectionChanged += new System.EventHandler(this.dgvFT_SelectionChanged);
            this.dgvFT.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvFT_KeyDown);
            this.dgvFT.Resize += new System.EventHandler(this.dgvFT_Resize);
            // 
            // colHHID
            // 
            this.colHHID.DataPropertyName = "HouseholdId";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colHHID.DefaultCellStyle = dataGridViewCellStyle3;
            this.colHHID.DividerWidth = 1;
            this.colHHID.HeaderText = "ID";
            this.colHHID.Name = "colHHID";
            this.colHHID.ReadOnly = true;
            this.colHHID.Width = 88;
            // 
            // colName
            // 
            this.colName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colName.DataPropertyName = "Name";
            this.colName.DividerWidth = 5;
            this.colName.HeaderText = "Name";
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            this.colName.Width = 200;
            // 
            // colFamilySize
            // 
            this.colFamilySize.DataPropertyName = "FamilySize";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(3, 2, 0, 0);
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.colFamilySize.DefaultCellStyle = dataGridViewCellStyle4;
            this.colFamilySize.DividerWidth = 3;
            this.colFamilySize.HeaderText = "FamilySize";
            this.colFamilySize.Name = "colFamilySize";
            this.colFamilySize.ReadOnly = true;
            this.colFamilySize.Width = 87;
            // 
            // colFoodList
            // 
            this.colFoodList.DataPropertyName = "FoodSvcList";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle5.Padding = new System.Windows.Forms.Padding(3, 2, 0, 0);
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.colFoodList.DefaultCellStyle = dataGridViewCellStyle5;
            this.colFoodList.DividerWidth = 3;
            this.colFoodList.HeaderText = "Food List";
            this.colFoodList.MinimumWidth = 100;
            this.colFoodList.Name = "colFoodList";
            this.colFoodList.ReadOnly = true;
            // 
            // colBabySvcs
            // 
            this.colBabySvcs.DividerWidth = 2;
            this.colBabySvcs.HeaderText = "Baby Services";
            this.colBabySvcs.Name = "colBabySvcs";
            this.colBabySvcs.ReadOnly = true;
            this.colBabySvcs.Width = 88;
            // 
            // colNonFood
            // 
            this.colNonFood.DividerWidth = 2;
            this.colNonFood.HeaderText = "Non-Food List";
            this.colNonFood.Name = "colNonFood";
            this.colNonFood.ReadOnly = true;
            this.colNonFood.Width = 88;
            // 
            // colNotes
            // 
            this.colNotes.DataPropertyName = "Notes";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.Padding = new System.Windows.Forms.Padding(3, 3, 0, 0);
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.colNotes.DefaultCellStyle = dataGridViewCellStyle6;
            this.colNotes.DividerWidth = 5;
            this.colNotes.HeaderText = "Notes";
            this.colNotes.Name = "colNotes";
            this.colNotes.ReadOnly = true;
            this.colNotes.Width = 87;
            // 
            // colLbsStd
            // 
            this.colLbsStd.DataPropertyName = "LbsStd";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colLbsStd.DefaultCellStyle = dataGridViewCellStyle7;
            this.colLbsStd.DividerWidth = 1;
            this.colLbsStd.HeaderText = "Lbs Std";
            this.colLbsStd.MinimumWidth = 20;
            this.colLbsStd.Name = "colLbsStd";
            this.colLbsStd.Width = 88;
            // 
            // colLbsOther
            // 
            this.colLbsOther.DataPropertyName = "LbsOther";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colLbsOther.DefaultCellStyle = dataGridViewCellStyle8;
            this.colLbsOther.DividerWidth = 1;
            this.colLbsOther.HeaderText = "Lbs Other";
            this.colLbsOther.MinimumWidth = 20;
            this.colLbsOther.Name = "colLbsOther";
            this.colLbsOther.Width = 87;
            // 
            // colLbsTEFAP
            // 
            this.colLbsTEFAP.DataPropertyName = "LbsCommodity";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colLbsTEFAP.DefaultCellStyle = dataGridViewCellStyle9;
            this.colLbsTEFAP.DividerWidth = 1;
            this.colLbsTEFAP.HeaderText = "Lbs Comm";
            this.colLbsTEFAP.MinimumWidth = 20;
            this.colLbsTEFAP.Name = "colLbsTEFAP";
            this.colLbsTEFAP.Width = 88;
            // 
            // colLbsSuppl
            // 
            this.colLbsSuppl.DataPropertyName = "LbsSupplemental";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colLbsSuppl.DefaultCellStyle = dataGridViewCellStyle10;
            this.colLbsSuppl.DividerWidth = 1;
            this.colLbsSuppl.HeaderText = "Lbs Suppl";
            this.colLbsSuppl.MinimumWidth = 20;
            this.colLbsSuppl.Name = "colLbsSuppl";
            this.colLbsSuppl.Width = 88;
            // 
            // colLbsBaby
            // 
            this.colLbsBaby.DataPropertyName = "LbsBabySvc";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colLbsBaby.DefaultCellStyle = dataGridViewCellStyle11;
            this.colLbsBaby.DividerWidth = 3;
            this.colLbsBaby.HeaderText = "Lbs Baby";
            this.colLbsBaby.MinimumWidth = 20;
            this.colLbsBaby.Name = "colLbsBaby";
            this.colLbsBaby.Width = 87;
            // 
            // colDone
            // 
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.Padding = new System.Windows.Forms.Padding(0, 15, 0, 15);
            this.colDone.DefaultCellStyle = dataGridViewCellStyle12;
            this.colDone.DividerWidth = 2;
            this.colDone.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.colDone.HeaderText = "";
            this.colDone.Name = "colDone";
            this.colDone.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colDone.Text = "Save";
            this.colDone.UseColumnTextForButtonValue = true;
            this.colDone.Width = 88;
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
            this.splitContainer1.Panel1.Controls.Add(this.lblFBName);
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvFT);
            this.splitContainer1.Panel2.Controls.Add(this.menuStrip1);
            this.splitContainer1.Size = new System.Drawing.Size(1276, 682);
            this.splitContainer1.SplitterDistance = 91;
            this.splitContainer1.TabIndex = 4;
            // 
            // lblFBName
            // 
            this.lblFBName.AutoSize = true;
            this.lblFBName.Font = new System.Drawing.Font("Verdana", 14F);
            this.lblFBName.Location = new System.Drawing.Point(12, 14);
            this.lblFBName.Name = "lblFBName";
            this.lblFBName.Size = new System.Drawing.Size(172, 23);
            this.lblFBName.TabIndex = 26;
            this.lblFBName.Text = "Food Bank Name";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.SelectedNameLabel);
            this.panel1.Controls.Add(this.SelectedIdLabel);
            this.panel1.Controls.Add(this.SelectedLabel);
            this.panel1.Controls.Add(this.clearTotalWt);
            this.panel1.Controls.Add(this.scaleWeightText);
            this.panel1.Controls.Add(this.addWeightButton);
            this.panel1.Controls.Add(this.tbTotalScaleWt);
            this.panel1.Controls.Add(this.totalWeightText);
            this.panel1.Controls.Add(this.tbScaleWt);
            this.panel1.Controls.Add(this.btnRefresh);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(343, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(933, 91);
            this.panel1.TabIndex = 33;
            // 
            // SelectedNameLabel
            // 
            this.SelectedNameLabel.AutoSize = true;
            this.SelectedNameLabel.BackColor = System.Drawing.Color.Beige;
            this.SelectedNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectedNameLabel.Location = new System.Drawing.Point(245, 62);
            this.SelectedNameLabel.Name = "SelectedNameLabel";
            this.SelectedNameLabel.Size = new System.Drawing.Size(106, 20);
            this.SelectedNameLabel.TabIndex = 28;
            this.SelectedNameLabel.Text = "Name = None";
            // 
            // SelectedIdLabel
            // 
            this.SelectedIdLabel.AutoSize = true;
            this.SelectedIdLabel.BackColor = System.Drawing.Color.Beige;
            this.SelectedIdLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectedIdLabel.Location = new System.Drawing.Point(147, 62);
            this.SelectedIdLabel.Name = "SelectedIdLabel";
            this.SelectedIdLabel.Size = new System.Drawing.Size(81, 20);
            this.SelectedIdLabel.TabIndex = 26;
            this.SelectedIdLabel.Text = "ID = None";
            // 
            // SelectedLabel
            // 
            this.SelectedLabel.AutoSize = true;
            this.SelectedLabel.BackColor = System.Drawing.Color.Beige;
            this.SelectedLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectedLabel.Location = new System.Drawing.Point(47, 62);
            this.SelectedLabel.Name = "SelectedLabel";
            this.SelectedLabel.Size = new System.Drawing.Size(85, 20);
            this.SelectedLabel.TabIndex = 25;
            this.SelectedLabel.Text = "Selected:";
            // 
            // clearTotalWt
            // 
            this.clearTotalWt.Location = new System.Drawing.Point(841, 43);
            this.clearTotalWt.Name = "clearTotalWt";
            this.clearTotalWt.Size = new System.Drawing.Size(71, 35);
            this.clearTotalWt.TabIndex = 33;
            this.clearTotalWt.Text = "Clear";
            this.clearTotalWt.UseVisualStyleBackColor = true;
            this.clearTotalWt.Click += new System.EventHandler(this.clearTotalWt_Click);
            // 
            // scaleWeightText
            // 
            this.scaleWeightText.BackColor = System.Drawing.SystemColors.Menu;
            this.scaleWeightText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.scaleWeightText.Cursor = System.Windows.Forms.Cursors.No;
            this.scaleWeightText.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scaleWeightText.ForeColor = System.Drawing.SystemColors.WindowText;
            this.scaleWeightText.Location = new System.Drawing.Point(579, 18);
            this.scaleWeightText.Name = "scaleWeightText";
            this.scaleWeightText.Size = new System.Drawing.Size(95, 17);
            this.scaleWeightText.TabIndex = 32;
            this.scaleWeightText.Text = "Scale Weight";
            // 
            // addWeightButton
            // 
            this.addWeightButton.BackColor = System.Drawing.Color.AntiqueWhite;
            this.addWeightButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addWeightButton.Location = new System.Drawing.Point(685, 43);
            this.addWeightButton.Name = "addWeightButton";
            this.addWeightButton.Size = new System.Drawing.Size(38, 35);
            this.addWeightButton.TabIndex = 31;
            this.addWeightButton.Text = "+";
            this.addWeightButton.UseVisualStyleBackColor = false;
            this.addWeightButton.Click += new System.EventHandler(this.addWeightButton_Click);
            // 
            // tbTotalScaleWt
            // 
            this.tbTotalScaleWt.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.tbTotalScaleWt.Location = new System.Drawing.Point(747, 43);
            this.tbTotalScaleWt.Name = "tbTotalScaleWt";
            this.tbTotalScaleWt.ReadOnly = true;
            this.tbTotalScaleWt.Size = new System.Drawing.Size(75, 35);
            this.tbTotalScaleWt.TabIndex = 29;
            this.tbTotalScaleWt.Text = "0";
            this.tbTotalScaleWt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // totalWeightText
            // 
            this.totalWeightText.BackColor = System.Drawing.SystemColors.Menu;
            this.totalWeightText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.totalWeightText.Cursor = System.Windows.Forms.Cursors.No;
            this.totalWeightText.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalWeightText.ForeColor = System.Drawing.SystemColors.WindowText;
            this.totalWeightText.Location = new System.Drawing.Point(747, 18);
            this.totalWeightText.Name = "totalWeightText";
            this.totalWeightText.Size = new System.Drawing.Size(89, 17);
            this.totalWeightText.TabIndex = 28;
            this.totalWeightText.Text = "Total Weight";
            // 
            // tbScaleWt
            // 
            this.tbScaleWt.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.tbScaleWt.Location = new System.Drawing.Point(579, 43);
            this.tbScaleWt.Name = "tbScaleWt";
            this.tbScaleWt.ReadOnly = true;
            this.tbScaleWt.Size = new System.Drawing.Size(75, 35);
            this.tbScaleWt.TabIndex = 27;
            this.tbScaleWt.Text = "0";
            this.tbScaleWt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(482, 43);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(74, 35);
            this.btnRefresh.TabIndex = 25;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Beige;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.enableScaleFeature});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1276, 26);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // enableScaleFeature
            // 
            this.enableScaleFeature.BackColor = System.Drawing.Color.AntiqueWhite;
            this.enableScaleFeature.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.enableScale,
            this.disableScale});
            this.enableScaleFeature.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.enableScaleFeature.Name = "enableScaleFeature";
            this.enableScaleFeature.Size = new System.Drawing.Size(209, 22);
            this.enableScaleFeature.Tag = "0";
            this.enableScaleFeature.Text = "Automated Scale Feature";
            // 
            // enableScale
            // 
            this.enableScale.Name = "enableScale";
            this.enableScale.Size = new System.Drawing.Size(128, 22);
            this.enableScale.Text = "Enable";
            this.enableScale.Click += new System.EventHandler(this.enableScaleToolStripMenuItem1_Click);
            // 
            // disableScale
            // 
            this.disableScale.Name = "disableScale";
            this.disableScale.Size = new System.Drawing.Size(128, 22);
            this.disableScale.Text = "Disable";
            this.disableScale.Click += new System.EventHandler(this.disableScaleToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssStatus,
            this.tsslblsep,
            this.tsslblMsg});
            this.statusStrip1.Location = new System.Drawing.Point(0, 658);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1276, 24);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tssStatus
            // 
            this.tssStatus.AutoSize = false;
            this.tssStatus.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.tssStatus.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.tssStatus.Name = "tssStatus";
            this.tssStatus.Size = new System.Drawing.Size(118, 19);
            // 
            // tsslblsep
            // 
            this.tsslblsep.AutoSize = false;
            this.tsslblsep.BackColor = System.Drawing.SystemColors.Control;
            this.tsslblsep.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.tsslblsep.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.tsslblsep.Name = "tsslblsep";
            this.tsslblsep.Size = new System.Drawing.Size(150, 19);
            // 
            // tsslblMsg
            // 
            this.tsslblMsg.AutoSize = false;
            this.tsslblMsg.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tsslblMsg.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.tsslblMsg.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.tsslblMsg.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.tsslblMsg.ForeColor = System.Drawing.Color.Magenta;
            this.tsslblMsg.Name = "tsslblMsg";
            this.tsslblMsg.Size = new System.Drawing.Size(500, 19);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            // 
            // FastTrackForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Cornsilk;
            this.ClientSize = new System.Drawing.Size(1276, 682);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FastTrackForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ClientcardFB3 FAST TRACK";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FastTrackForm_FormClosing);
            this.Load += new System.EventHandler(this.FastTrackForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FastTrackForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFT)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.DataGridView dgvFT;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lblFBName;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tssStatus;
        private System.Windows.Forms.ToolStripStatusLabel tsslblsep;
        private System.Windows.Forms.ToolStripStatusLabel tsslblMsg;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHHID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFamilySize;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFoodList;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBabySvcs;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNonFood;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNotes;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLbsStd;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLbsOther;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLbsTEFAP;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLbsSuppl;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLbsBaby;
        private System.Windows.Forms.DataGridViewButtonColumn colDone;
        private System.Windows.Forms.TextBox tbScaleWt;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem enableScaleFeature;
        private System.Windows.Forms.ToolStripMenuItem enableScale;
        private System.Windows.Forms.ToolStripMenuItem disableScale;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Button addWeightButton;
        private System.Windows.Forms.TextBox tbTotalScaleWt;
        private System.Windows.Forms.TextBox totalWeightText;
        private System.Windows.Forms.TextBox scaleWeightText;
        private System.Windows.Forms.Label SelectedNameLabel;
        private System.Windows.Forms.Label SelectedIdLabel;
        private System.Windows.Forms.Label SelectedLabel;
        private System.Windows.Forms.Button clearTotalWt;
        private System.Windows.Forms.Panel panel1;
    }
}


