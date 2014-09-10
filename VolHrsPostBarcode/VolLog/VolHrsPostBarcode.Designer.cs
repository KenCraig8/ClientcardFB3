namespace ClientcardFB3
{
    partial class VolLogMain
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
            this.btnFinish = new System.Windows.Forms.Button();
            this.tbFBIDNbr = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbTimeIN = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbTimeOUT = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbHours = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.dtpTrxDate = new System.Windows.Forms.DateTimePicker();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnToggleList = new System.Windows.Forms.Button();
            this.btnFindVol = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnFinish
            // 
            this.btnFinish.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFinish.Location = new System.Drawing.Point(287, 238);
            this.btnFinish.Name = "btnFinish";
            this.btnFinish.Size = new System.Drawing.Size(127, 41);
            this.btnFinish.TabIndex = 0;
            this.btnFinish.Text = "&Post";
            this.btnFinish.UseVisualStyleBackColor = true;
            this.btnFinish.Click += new System.EventHandler(this.btnFinish_Click);
            // 
            // tbFBIDNbr
            // 
            this.tbFBIDNbr.Font = new System.Drawing.Font("Courier New", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbFBIDNbr.Location = new System.Drawing.Point(102, 72);
            this.tbFBIDNbr.Name = "tbFBIDNbr";
            this.tbFBIDNbr.Size = new System.Drawing.Size(312, 35);
            this.tbFBIDNbr.TabIndex = 1;
            this.tbFBIDNbr.Text = "123545";
            this.tbFBIDNbr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbFBIDNbr.TextChanged += new System.EventHandler(this.tbFBIDNbr_TextChanged);
            this.tbFBIDNbr.Enter += new System.EventHandler(this.tbVolID_Enter);
            this.tbFBIDNbr.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbFBIDNbr_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Volunteer ID";
            // 
            // tbTimeIN
            // 
            this.tbTimeIN.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbTimeIN.Location = new System.Drawing.Point(119, 176);
            this.tbTimeIN.Name = "tbTimeIN";
            this.tbTimeIN.Size = new System.Drawing.Size(99, 32);
            this.tbTimeIN.TabIndex = 4;
            this.tbTimeIN.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbTimeIN.Enter += new System.EventHandler(this.tbTimeIN_Enter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(57, 184);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Time IN";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(57, 222);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Time OUT";
            // 
            // tbTimeOUT
            // 
            this.tbTimeOUT.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbTimeOUT.Location = new System.Drawing.Point(119, 212);
            this.tbTimeOUT.Name = "tbTimeOUT";
            this.tbTimeOUT.Size = new System.Drawing.Size(99, 32);
            this.tbTimeOUT.TabIndex = 6;
            this.tbTimeOUT.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbTimeOUT.Enter += new System.EventHandler(this.tbTimeOUT_Enter);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(54, 257);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Num Hours";
            // 
            // tbHours
            // 
            this.tbHours.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbHours.ForeColor = System.Drawing.Color.MidnightBlue;
            this.tbHours.Location = new System.Drawing.Point(119, 247);
            this.tbHours.Name = "tbHours";
            this.tbHours.Size = new System.Drawing.Size(99, 32);
            this.tbHours.TabIndex = 9;
            this.tbHours.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbHours.Enter += new System.EventHandler(this.tbHours_Enter);
            // 
            // lblName
            // 
            this.lblName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(102, 120);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(312, 27);
            this.lblName.TabIndex = 11;
            this.lblName.Text = "James Craig";
            // 
            // dtpTrxDate
            // 
            this.dtpTrxDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpTrxDate.Location = new System.Drawing.Point(16, 12);
            this.dtpTrxDate.Name = "dtpTrxDate";
            this.dtpTrxDate.Size = new System.Drawing.Size(398, 29);
            this.dtpTrxDate.TabIndex = 15;
            this.dtpTrxDate.ValueChanged += new System.EventHandler(this.dtpTrxDate_ValueChanged);
            // 
            // timer1
            // 
            this.timer1.Interval = 3000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnToggleList
            // 
            this.btnToggleList.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnToggleList.Location = new System.Drawing.Point(285, 330);
            this.btnToggleList.Name = "btnToggleList";
            this.btnToggleList.Size = new System.Drawing.Size(129, 34);
            this.btnToggleList.TabIndex = 16;
            this.btnToggleList.Text = "Toggle &List";
            this.btnToggleList.UseVisualStyleBackColor = true;
            this.btnToggleList.Click += new System.EventHandler(this.btnToggleList_Click);
            // 
            // btnFindVol
            // 
            this.btnFindVol.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFindVol.Location = new System.Drawing.Point(284, 176);
            this.btnFindVol.Name = "btnFindVol";
            this.btnFindVol.Size = new System.Drawing.Size(127, 41);
            this.btnFindVol.TabIndex = 17;
            this.btnFindVol.Text = "&Find Volunteer";
            this.btnFindVol.UseVisualStyleBackColor = true;
            this.btnFindVol.Click += new System.EventHandler(this.btnFindVol_Click);
            // 
            // VolLogMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(423, 378);
            this.Controls.Add(this.btnFindVol);
            this.Controls.Add(this.btnToggleList);
            this.Controls.Add(this.dtpTrxDate);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbHours);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbTimeOUT);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbTimeIN);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbFBIDNbr);
            this.Controls.Add(this.btnFinish);
            this.Location = new System.Drawing.Point(5, 50);
            this.MaximizeBox = false;
            this.Name = "VolLogMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Volunteer Check-IN/OUT";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.VolLogMain_FormClosed);
            this.Load += new System.EventHandler(this.VolLogMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnFinish;
        private System.Windows.Forms.TextBox tbFBIDNbr;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbTimeIN;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbTimeOUT;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbHours;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.DateTimePicker dtpTrxDate;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnToggleList;
        private System.Windows.Forms.Button btnFindVol;
    }
}

