using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ClientcardFB3
{
    public partial class WebPageForm : Form
    {
        public WebPageForm(string title, string url)
        {
            InitializeComponent();
            this.Text = title;
            webBrowser1.Navigate(url);
        }
    }
}
