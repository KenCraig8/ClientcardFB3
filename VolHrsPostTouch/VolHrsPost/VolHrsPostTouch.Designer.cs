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
            this.tbVolID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbTimeIN = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbTimeOUT = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSpace = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnPM = new System.Windows.Forms.Button();
            this.btnAM = new System.Windows.Forms.Button();
            this.btnColon = new System.Windows.Forms.Button();
            this.btnBS = new System.Windows.Forms.Button();
            this.btnENTER = new System.Windows.Forms.Button();
            this.btn0 = new System.Windows.Forms.Button();
            this.btn3 = new System.Windows.Forms.Button();
            this.btn2 = new System.Windows.Forms.Button();
            this.btn1 = new System.Windows.Forms.Button();
            this.btn6 = new System.Windows.Forms.Button();
            this.btn5 = new System.Windows.Forms.Button();
            this.btn4 = new System.Windows.Forms.Button();
            this.btn9 = new System.Windows.Forms.Button();
            this.btn8 = new System.Windows.Forms.Button();
            this.btn7 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.tbHours = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.dtpTrxDate = new System.Windows.Forms.DateTimePicker();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnToggleList = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnFinish
            // 
            this.btnFinish.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFinish.Location = new System.Drawing.Point(333, 321);
            this.btnFinish.Name = "btnFinish";
            this.btnFinish.Size = new System.Drawing.Size(178, 60);
            this.btnFinish.TabIndex = 0;
            this.btnFinish.Text = "&POST";
            this.btnFinish.UseVisualStyleBackColor = true;
            this.btnFinish.Click += new System.EventHandler(this.btnFinish_Click);
            // 
            // tbVolID
            // 
            this.tbVolID.Font = new System.Drawing.Font("Courier New", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbVolID.Location = new System.Drawing.Point(395, 81);
            this.tbVolID.Name = "tbVolID";
            this.tbVolID.Size = new System.Drawing.Size(116, 35);
            this.tbVolID.TabIndex = 1;
            this.tbVolID.Text = "123545";
            this.tbVolID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbVolID.Enter += new System.EventHandler(this.tbVolID_Enter);
            this.tbVolID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbVolID_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(326, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Volunteer ID";
            // 
            // tbTimeIN
            // 
            this.tbTimeIN.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbTimeIN.Location = new System.Drawing.Point(412, 185);
            this.tbTimeIN.Name = "tbTimeIN";
            this.tbTimeIN.Size = new System.Drawing.Size(99, 32);
            this.tbTimeIN.TabIndex = 4;
            this.tbTimeIN.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbTimeIN.Enter += new System.EventHandler(this.tbTimeIN_Enter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(350, 193);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Time IN";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(350, 231);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Time OUT";
            // 
            // tbTimeOUT
            // 
            this.tbTimeOUT.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbTimeOUT.Location = new System.Drawing.Point(412, 221);
            this.tbTimeOUT.Name = "tbTimeOUT";
            this.tbTimeOUT.Size = new System.Drawing.Size(99, 32);
            this.tbTimeOUT.TabIndex = 6;
            this.tbTimeOUT.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbTimeOUT.Enter += new System.EventHandler(this.tbTimeOUT_Enter);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnSpace);
            this.panel1.Controls.Add(this.btnStart);
            this.panel1.Controls.Add(this.btnPM);
            this.panel1.Controls.Add(this.btnAM);
            this.panel1.Controls.Add(this.btnColon);
            this.panel1.Controls.Add(this.btnBS);
            this.panel1.Controls.Add(this.btnENTER);
            this.panel1.Controls.Add(this.btn0);
            this.panel1.Controls.Add(this.btn3);
            this.panel1.Controls.Add(this.btn2);
            this.panel1.Controls.Add(this.btn1);
            this.panel1.Controls.Add(this.btn6);
            this.panel1.Controls.Add(this.btn5);
            this.panel1.Controls.Add(this.btn4);
            this.panel1.Controls.Add(this.btn9);
            this.panel1.Controls.Add(this.btn8);
            this.panel1.Controls.Add(this.btn7);
            this.panel1.Location = new System.Drawing.Point(12, 60);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(275, 406);
            this.panel1.TabIndex = 8;
            // 
            // btnSpace
            // 
            this.btnSpace.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSpace.Location = new System.Drawing.Point(68, 271);
            this.btnSpace.Name = "btnSpace";
            this.btnSpace.Size = new System.Drawing.Size(60, 60);
            this.btnSpace.TabIndex = 15;
            this.btnSpace.Text = "SP";
            this.btnSpace.UseVisualStyleBackColor = true;
            this.btnSpace.Click += new System.EventHandler(this.btnSpace_Click);
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.Location = new System.Drawing.Point(67, 7);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(126, 60);
            this.btnStart.TabIndex = 12;
            this.btnStart.Text = "&BEGIN";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnPM
            // 
            this.btnPM.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPM.Location = new System.Drawing.Point(210, 205);
            this.btnPM.Name = "btnPM";
            this.btnPM.Size = new System.Drawing.Size(60, 60);
            this.btnPM.TabIndex = 14;
            this.btnPM.Text = "PM";
            this.btnPM.UseVisualStyleBackColor = true;
            this.btnPM.Click += new System.EventHandler(this.btnPM_Click);
            // 
            // btnAM
            // 
            this.btnAM.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAM.Location = new System.Drawing.Point(210, 139);
            this.btnAM.Name = "btnAM";
            this.btnAM.Size = new System.Drawing.Size(60, 60);
            this.btnAM.TabIndex = 13;
            this.btnAM.Text = "AM";
            this.btnAM.UseVisualStyleBackColor = true;
            this.btnAM.Click += new System.EventHandler(this.btnAM_Click);
            // 
            // btnColon
            // 
            this.btnColon.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnColon.Location = new System.Drawing.Point(210, 271);
            this.btnColon.Name = "btnColon";
            this.btnColon.Size = new System.Drawing.Size(60, 60);
            this.btnColon.TabIndex = 12;
            this.btnColon.Text = ":";
            this.btnColon.UseVisualStyleBackColor = true;
            this.btnColon.Click += new System.EventHandler(this.btnColon_Click);
            // 
            // btnBS
            // 
            this.btnBS.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBS.Location = new System.Drawing.Point(133, 271);
            this.btnBS.Name = "btnBS";
            this.btnBS.Size = new System.Drawing.Size(60, 60);
            this.btnBS.TabIndex = 11;
            this.btnBS.Text = "BS";
            this.btnBS.UseVisualStyleBackColor = true;
            this.btnBS.Click += new System.EventHandler(this.btnBS_Click);
            // 
            // btnENTER
            // 
            this.btnENTER.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnENTER.Location = new System.Drawing.Point(67, 338);
            this.btnENTER.Name = "btnENTER";
            this.btnENTER.Size = new System.Drawing.Size(126, 60);
            this.btnENTER.TabIndex = 10;
            this.btnENTER.Text = "&ENTER";
            this.btnENTER.UseVisualStyleBackColor = true;
            this.btnENTER.Click += new System.EventHandler(this.btnENTER_Click);
            // 
            // btn0
            // 
            this.btn0.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn0.Location = new System.Drawing.Point(3, 271);
            this.btn0.Name = "btn0";
            this.btn0.Size = new System.Drawing.Size(60, 60);
            this.btn0.TabIndex = 9;
            this.btn0.Text = "0";
            this.btn0.UseVisualStyleBackColor = true;
            this.btn0.Click += new System.EventHandler(this.btnNBR_Click);
            // 
            // btn3
            // 
            this.btn3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn3.Location = new System.Drawing.Point(133, 205);
            this.btn3.Name = "btn3";
            this.btn3.Size = new System.Drawing.Size(60, 60);
            this.btn3.TabIndex = 8;
            this.btn3.Text = "3";
            this.btn3.UseVisualStyleBackColor = true;
            this.btn3.Click += new System.EventHandler(this.btnNBR_Click);
            // 
            // btn2
            // 
            this.btn2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn2.Location = new System.Drawing.Point(68, 205);
            this.btn2.Name = "btn2";
            this.btn2.Size = new System.Drawing.Size(60, 60);
            this.btn2.TabIndex = 7;
            this.btn2.Text = "2";
            this.btn2.UseVisualStyleBackColor = true;
            this.btn2.Click += new System.EventHandler(this.btnNBR_Click);
            // 
            // btn1
            // 
            this.btn1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn1.Location = new System.Drawing.Point(3, 205);
            this.btn1.Name = "btn1";
            this.btn1.Size = new System.Drawing.Size(60, 60);
            this.btn1.TabIndex = 6;
            this.btn1.Text = "1";
            this.btn1.UseVisualStyleBackColor = true;
            this.btn1.Click += new System.EventHandler(this.btnNBR_Click);
            // 
            // btn6
            // 
            this.btn6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn6.Location = new System.Drawing.Point(133, 139);
            this.btn6.Name = "btn6";
            this.btn6.Size = new System.Drawing.Size(60, 60);
            this.btn6.TabIndex = 5;
            this.btn6.Text = "6";
            this.btn6.UseVisualStyleBackColor = true;
            this.btn6.Click += new System.EventHandler(this.btnNBR_Click);
            // 
            // btn5
            // 
            this.btn5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn5.Location = new System.Drawing.Point(67, 139);
            this.btn5.Name = "btn5";
            this.btn5.Size = new System.Drawing.Size(60, 60);
            this.btn5.TabIndex = 4;
            this.btn5.Text = "5";
            this.btn5.UseVisualStyleBackColor = true;
            this.btn5.Click += new System.EventHandler(this.btnNBR_Click);
            // 
            // btn4
            // 
            this.btn4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn4.Location = new System.Drawing.Point(3, 139);
            this.btn4.Name = "btn4";
            this.btn4.Size = new System.Drawing.Size(60, 60);
            this.btn4.TabIndex = 3;
            this.btn4.Text = "4";
            this.btn4.UseVisualStyleBackColor = true;
            this.btn4.Click += new System.EventHandler(this.btnNBR_Click);
            // 
            // btn9
            // 
            this.btn9.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn9.Location = new System.Drawing.Point(133, 73);
            this.btn9.Name = "btn9";
            this.btn9.Size = new System.Drawing.Size(60, 60);
            this.btn9.TabIndex = 2;
            this.btn9.Text = "9";
            this.btn9.UseVisualStyleBackColor = true;
            this.btn9.Click += new System.EventHandler(this.btnNBR_Click);
            // 
            // btn8
            // 
            this.btn8.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn8.Location = new System.Drawing.Point(67, 73);
            this.btn8.Name = "btn8";
            this.btn8.Size = new System.Drawing.Size(60, 60);
            this.btn8.TabIndex = 1;
            this.btn8.Text = "8";
            this.btn8.UseVisualStyleBackColor = true;
            this.btn8.Click += new System.EventHandler(this.btnNBR_Click);
            // 
            // btn7
            // 
            this.btn7.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn7.Location = new System.Drawing.Point(3, 73);
            this.btn7.Name = "btn7";
            this.btn7.Size = new System.Drawing.Size(60, 60);
            this.btn7.TabIndex = 0;
            this.btn7.Text = "7";
            this.btn7.UseVisualStyleBackColor = true;
            this.btn7.Click += new System.EventHandler(this.btnNBR_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(347, 266);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Num Hours";
            // 
            // tbHours
            // 
            this.tbHours.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbHours.ForeColor = System.Drawing.Color.MidnightBlue;
            this.tbHours.Location = new System.Drawing.Point(412, 256);
            this.tbHours.Name = "tbHours";
            this.tbHours.Size = new System.Drawing.Size(99, 32);
            this.tbHours.TabIndex = 9;
            this.tbHours.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbHours.Enter += new System.EventHandler(this.tbHours_Enter);
            // 
            // lblName
            // 
            this.lblName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(295, 130);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(216, 27);
            this.lblName.TabIndex = 11;
            this.lblName.Text = "James Craig";
            // 
            // dtpTrxDate
            // 
            this.dtpTrxDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpTrxDate.Location = new System.Drawing.Point(16, 12);
            this.dtpTrxDate.Name = "dtpTrxDate";
            this.dtpTrxDate.Size = new System.Drawing.Size(495, 35);
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
            this.btnToggleList.Location = new System.Drawing.Point(466, 428);
            this.btnToggleList.Name = "btnToggleList";
            this.btnToggleList.Size = new System.Drawing.Size(45, 34);
            this.btnToggleList.TabIndex = 16;
            this.btnToggleList.Text = "&List";
            this.btnToggleList.UseVisualStyleBackColor = true;
            this.btnToggleList.Click += new System.EventHandler(this.btnToggleList_Click);
            // 
            // VolLogMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(519, 472);
            this.Controls.Add(this.btnToggleList);
            this.Controls.Add(this.dtpTrxDate);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbHours);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbTimeOUT);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbTimeIN);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbVolID);
            this.Controls.Add(this.btnFinish);
            this.Location = new System.Drawing.Point(5, 50);
            this.MaximizeBox = false;
            this.Name = "VolLogMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Volunteer Check-IN/OUT";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.VolLogMain_FormClosed);
            this.Load += new System.EventHandler(this.VolLogMain_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnFinish;
        private System.Windows.Forms.TextBox tbVolID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbTimeIN;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbTimeOUT;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnENTER;
        private System.Windows.Forms.Button btn0;
        private System.Windows.Forms.Button btn3;
        private System.Windows.Forms.Button btn2;
        private System.Windows.Forms.Button btn1;
        private System.Windows.Forms.Button btn6;
        private System.Windows.Forms.Button btn5;
        private System.Windows.Forms.Button btn4;
        private System.Windows.Forms.Button btn9;
        private System.Windows.Forms.Button btn8;
        private System.Windows.Forms.Button btn7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbHours;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Button btnPM;
        private System.Windows.Forms.Button btnAM;
        private System.Windows.Forms.Button btnColon;
        private System.Windows.Forms.Button btnBS;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.DateTimePicker dtpTrxDate;
        private System.Windows.Forms.Button btnSpace;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnToggleList;
    }
}

