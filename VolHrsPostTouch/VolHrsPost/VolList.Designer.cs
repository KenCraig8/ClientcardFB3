namespace ClientcardFB3
{
    partial class VolList
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
            this.lvwVols = new System.Windows.Forms.ListView();
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colIN = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colOut = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colVolId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // lvwVols
            // 
            this.lvwVols.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvwVols.CheckBoxes = true;
            this.lvwVols.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colIN,
            this.colOut,
            this.colVolId});
            this.lvwVols.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwVols.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvwVols.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvwVols.Location = new System.Drawing.Point(0, 0);
            this.lvwVols.Name = "lvwVols";
            this.lvwVols.Size = new System.Drawing.Size(334, 562);
            this.lvwVols.TabIndex = 0;
            this.lvwVols.UseCompatibleStateImageBehavior = false;
            this.lvwVols.View = System.Windows.Forms.View.Details;
            this.lvwVols.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvwVols_ItemChecked);
            // 
            // colName
            // 
            this.colName.Text = "Volunteer";
            this.colName.Width = 300;
            // 
            // colIN
            // 
            this.colIN.Text = "Time IN";
            this.colIN.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colIN.Width = 0;
            // 
            // colOut
            // 
            this.colOut.Text = "Time OUT";
            this.colOut.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colOut.Width = 0;
            // 
            // colVolId
            // 
            this.colVolId.Text = "ID";
            this.colVolId.Width = 0;
            // 
            // VolList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 562);
            this.ControlBox = false;
            this.Controls.Add(this.lvwVols);
            this.Location = new System.Drawing.Point(542, 0);
            this.MaximizeBox = false;
            this.Name = "VolList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Volunteer Log";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvwVols;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colIN;
        private System.Windows.Forms.ColumnHeader colOut;
        private System.Windows.Forms.ColumnHeader colVolId;
    }
}