using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FoodReceipt;

namespace ClientcardFB3
{
    public partial class FoodDonation : Form
    {
 
        string donorID,donorName;

        public FoodDonation( string val1, String val2)
        {
            InitializeComponent();
            donorID = val1;
            donorName = val2;
        }
        public FoodDonation()
        {
            InitializeComponent();
        }

        private void FoodDonation_Load(object sender, EventArgs e)
        {
            textBox1.Text = donorID;
            textBox2.Text = donorName;
        }
       
    }
}
