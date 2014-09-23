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

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (webBrowser1.IsBusy == false)
            {
                webBrowser1.GoBack();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            webBrowser1.GoForward();
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            btnPrevious.Enabled = webBrowser1.CanGoBack;
            btnNext.Enabled = webBrowser1.CanGoForward;
        }
    }
}
