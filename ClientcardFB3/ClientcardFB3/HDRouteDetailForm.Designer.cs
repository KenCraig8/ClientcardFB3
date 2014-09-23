namespace ClientcardFB3
{
    partial class HDRouteDetailForm
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
            this.pnlRouteInfo = new System.Windows.Forms.Panel();
            this.tbEstMiles = new System.Windows.Forms.TextBox();
            this.tbEstTime = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tbDriverComments = new System.Windows.Forms.TextBox();
            this.tbRouteNotes = new System.Windows.Forms.TextBox();
            this.btnSaveRoute = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbPhone = new System.Windows.Forms.MaskedTextBox();
            this.lblPhone = new System.Windows.Forms.Label();
            this.btnSelectDriver = new System.Windows.Forms.Button();
            this.tbDriver = new System.Windows.Forms.TextBox();
            this.lblVolunteer = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlRouteInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlRouteInfo
            // 
            this.pnlRouteInfo.BackColor = System.Drawing.Color.Wheat;
            this.pnlRouteInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlRouteInfo.Controls.Add(this.textBox1);
            this.pnlRouteInfo.Controls.Add(this.label1);
            this.pnlRouteInfo.Controls.Add(this.tbEstMiles);
            this.pnlRouteInfo.Controls.Add(this.tbEstTime);
            this.pnlRouteInfo.Controls.Add(this.label7);
            this.pnlRouteInfo.Controls.Add(this.label6);
            this.pnlRouteInfo.Controls.Add(this.tbDriverComments);
            this.pnlRouteInfo.Controls.Add(this.tbRouteNotes);
            this.pnlRouteInfo.Controls.Add(this.btnSaveRoute);
            this.pnlRouteInfo.Controls.Add(this.label5);
            this.pnlRouteInfo.Controls.Add(this.label3);
            this.pnlRouteInfo.Controls.Add(this.tbPhone);
            this.pnlRouteInfo.Controls.Add(this.lblPhone);
            this.pnlRouteInfo.Controls.Add(this.btnSelectDriver);
            this.pnlRouteInfo.Controls.Add(this.tbDriver);
            this.pnlRouteInfo.Controls.Add(this.lblVolunteer);
            this.pnlRouteInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRouteInfo.Location = new System.Drawing.Point(0, 0);
            this.pnlRouteInfo.Name = "pnlRouteInfo";
            this.pnlRouteInfo.Size = new System.Drawing.Size(555, 245);
            this.pnlRouteInfo.TabIndex = 90;
            // 
            // tbEstMiles
            // 
            this.tbEstMiles.Location = new System.Drawing.Point(424, 59);
            this.tbEstMiles.Name = "tbEstMiles";
            this.tbEstMiles.Size = new System.Drawing.Size(54, 20);
            this.tbEstMiles.TabIndex = 39;
            this.tbEstMiles.Tag = "estmiles";
            this.tbEstMiles.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbEstTime
            // 
            this.tbEstTime.Location = new System.Drawing.Point(359, 60);
            this.tbEstTime.Name = "tbEstTime";
            this.tbEstTime.Size = new System.Drawing.Size(54, 20);
            this.tbEstTime.TabIndex = 38;
            this.tbEstTime.Tag = "esthours";
            this.tbEstTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(255, 98);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 13);
            this.label7.TabIndex = 37;
            this.label7.Text = "Driver Notes:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(0, 98);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 13);
            this.label6.TabIndex = 36;
            this.label6.Text = "Route Notes:";
            // 
            // tbDriverComments
            // 
            this.tbDriverComments.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbDriverComments.Location = new System.Drawing.Point(258, 118);
            this.tbDriverComments.Multiline = true;
            this.tbDriverComments.Name = "tbDriverComments";
            this.tbDriverComments.Size = new System.Drawing.Size(291, 120);
            this.tbDriverComments.TabIndex = 35;
            this.tbDriverComments.Tag = "drivernotes";
            this.tbDriverComments.Text = "123456789012345678901234567890123456789";
            // 
            // tbRouteNotes
            // 
            this.tbRouteNotes.Location = new System.Drawing.Point(3, 118);
            this.tbRouteNotes.Multiline = true;
            this.tbRouteNotes.Name = "tbRouteNotes";
            this.tbRouteNotes.Size = new System.Drawing.Size(249, 120);
            this.tbRouteNotes.TabIndex = 34;
            this.tbRouteNotes.Tag = "notes";
            this.tbRouteNotes.Text = "123456789012345678901234567890123456789";
            // 
            // btnSaveRoute
            // 
            this.btnSaveRoute.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveRoute.Location = new System.Drawing.Point(489, 3);
            this.btnSaveRoute.Name = "btnSaveRoute";
            this.btnSaveRoute.Size = new System.Drawing.Size(60, 30);
            this.btnSaveRoute.TabIndex = 4;
            this.btnSaveRoute.Text = "&Save";
            this.btnSaveRoute.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(422, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 13);
            this.label5.TabIndex = 32;
            this.label5.Text = "Est Miles:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(356, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 30;
            this.label3.Text = "Est Time:";
            // 
            // tbPhone
            // 
            this.tbPhone.AllowPromptAsInput = false;
            this.tbPhone.BeepOnError = true;
            this.tbPhone.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPhone.HidePromptOnLeave = true;
            this.tbPhone.Location = new System.Drawing.Point(79, 60);
            this.tbPhone.Mask = "(999) 000-0000 aaaaaaaaa";
            this.tbPhone.Name = "tbPhone";
            this.tbPhone.Size = new System.Drawing.Size(165, 23);
            this.tbPhone.TabIndex = 29;
            this.tbPhone.Tag = "phone";
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Location = new System.Drawing.Point(0, 65);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(72, 13);
            this.lblPhone.TabIndex = 3;
            this.lblPhone.Text = "Driver Phone:";
            // 
            // btnSelectDriver
            // 
            this.btnSelectDriver.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectDriver.Location = new System.Drawing.Point(298, 31);
            this.btnSelectDriver.Name = "btnSelectDriver";
            this.btnSelectDriver.Size = new System.Drawing.Size(29, 24);
            this.btnSelectDriver.TabIndex = 2;
            this.btnSelectDriver.Text = "...";
            this.btnSelectDriver.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSelectDriver.UseVisualStyleBackColor = true;
            // 
            // tbDriver
            // 
            this.tbDriver.BackColor = System.Drawing.Color.White;
            this.tbDriver.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbDriver.ForeColor = System.Drawing.Color.DarkBlue;
            this.tbDriver.Location = new System.Drawing.Point(79, 31);
            this.tbDriver.Name = "tbDriver";
            this.tbDriver.ReadOnly = true;
            this.tbDriver.Size = new System.Drawing.Size(213, 24);
            this.tbDriver.TabIndex = 1;
            this.tbDriver.TabStop = false;
            // 
            // lblVolunteer
            // 
            this.lblVolunteer.AutoSize = true;
            this.lblVolunteer.Location = new System.Drawing.Point(0, 38);
            this.lblVolunteer.Name = "lblVolunteer";
            this.lblVolunteer.Size = new System.Drawing.Size(70, 13);
            this.lblVolunteer.TabIndex = 0;
            this.lblVolunteer.Text = "Route Driver:";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.White;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.ForeColor = System.Drawing.Color.DarkBlue;
            this.textBox1.Location = new System.Drawing.Point(79, 1);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(213, 24);
            this.textBox1.TabIndex = 41;
            this.textBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 40;
            this.label1.Text = "Route Title:";
            // 
            // HDRouteDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 245);
            this.Controls.Add(this.pnlRouteInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HDRouteDetailForm";
            this.Text = "Home Delivery Route";
            this.pnlRouteInfo.ResumeLayout(false);
            this.pnlRouteInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlRouteInfo;
        private System.Windows.Forms.TextBox tbEstMiles;
        private System.Windows.Forms.TextBox tbEstTime;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbDriverComments;
        private System.Windows.Forms.TextBox tbRouteNotes;
        private System.Windows.Forms.Button btnSaveRoute;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.MaskedTextBox tbPhone;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.Button btnSelectDriver;
        private System.Windows.Forms.TextBox tbDriver;
        private System.Windows.Forms.Label lblVolunteer;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
    }
}