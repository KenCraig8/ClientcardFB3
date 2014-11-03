namespace ClientcardFB3
{
    partial class ZipcodeSelectForm
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
            this.lblSearchType = new System.Windows.Forms.Label();
            this.lvZipcodes = new System.Windows.Forms.ListView();
            this.clmZipcode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmCity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmState = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmArea = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmCounty = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblSearchType
            // 
            this.lblSearchType.AutoSize = true;
            this.lblSearchType.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearchType.Location = new System.Drawing.Point(8, 24);
            this.lblSearchType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSearchType.Name = "lblSearchType";
            this.lblSearchType.Size = new System.Drawing.Size(145, 24);
            this.lblSearchType.TabIndex = 0;
            this.lblSearchType.Text = "Search Value = ";
            // 
            // lvZipcodes
            // 
            this.lvZipcodes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmZipcode,
            this.clmCity,
            this.clmState,
            this.clmArea,
            this.clmCounty});
            this.lvZipcodes.FullRowSelect = true;
            this.lvZipcodes.GridLines = true;
            this.lvZipcodes.Location = new System.Drawing.Point(8, 64);
            this.lvZipcodes.MultiSelect = false;
            this.lvZipcodes.Name = "lvZipcodes";
            this.lvZipcodes.Size = new System.Drawing.Size(424, 288);
            this.lvZipcodes.TabIndex = 1;
            this.lvZipcodes.UseCompatibleStateImageBehavior = false;
            this.lvZipcodes.View = System.Windows.Forms.View.Details;
            this.lvZipcodes.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvZipcodes_MouseDoubleClick);
            // 
            // clmZipcode
            // 
            this.clmZipcode.Text = "Zipcode";
            this.clmZipcode.Width = 70;
            // 
            // clmCity
            // 
            this.clmCity.Text = "City";
            this.clmCity.Width = 150;
            // 
            // clmState
            // 
            this.clmState.Text = "State";
            this.clmState.Width = 50;
            // 
            // clmArea
            // 
            this.clmArea.Text = "Area";
            this.clmArea.Width = 50;
            // 
            // clmCounty
            // 
            this.clmCounty.Text = "County";
            this.clmCounty.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clmCounty.Width = 100;
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(448, 64);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(88, 40);
            this.btnSelect.TabIndex = 2;
            this.btnSelect.Text = "Select";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(448, 112);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(88, 40);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ZipcodeSelectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Cornsilk;
            this.ClientSize = new System.Drawing.Size(553, 358);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.lvZipcodes);
            this.Controls.Add(this.lblSearchType);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ZipcodeSelectForm";
            this.Text = "Select A Zipcode";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSearchType;
        private System.Windows.Forms.ListView lvZipcodes;
        private System.Windows.Forms.ColumnHeader clmZipcode;
        private System.Windows.Forms.ColumnHeader clmCity;
        private System.Windows.Forms.ColumnHeader clmState;
        private System.Windows.Forms.ColumnHeader clmArea;
        private System.Windows.Forms.ColumnHeader clmCounty;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnCancel;
    }
}