using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestSgPadCtrl
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
            sigPadInputCtrl1.initSigPad();
            button1.Visible = sigPadInputCtrl1.HaveSigPad();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sigPadInputCtrl1.ClearPromptList();
            sigPadInputCtrl1.AddPromptItem("I am declaring that members of my household are in need of this food and we meet the income eligibility standards."
                                          ,"Exit");
            sigPadInputCtrl1.AddPromptItem("This is the ONLY Food Bank where I receive federal commodities."
                                          , "Back");
            sigPadInputCtrl1.StartCapture();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            img1.Image = sigPadInputCtrl1.GetImage();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            sigPadInputCtrl1.initSigPad();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            img1.SizeMode = PictureBoxSizeMode.AutoSize;
            label1.Text = img1.Size.ToString();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            img1.SizeMode = PictureBoxSizeMode.StretchImage;
            label1.Text = img1.Size.ToString();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            img1.SizeMode = PictureBoxSizeMode.Normal;
            label1.Text = img1.Size.ToString();
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            img1.SizeMode = PictureBoxSizeMode.CenterImage;
            label1.Text = img1.Size.ToString();
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            img1.SizeMode = PictureBoxSizeMode.Zoom;
            label1.Text = img1.Size.ToString();
        }
    }
}
