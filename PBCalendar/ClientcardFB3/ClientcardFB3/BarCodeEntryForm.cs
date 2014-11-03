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
    public partial class BarCodeEntryForm : Form
    {
        int ibarcode = 0;

        public BarCodeEntryForm()
        {
            InitializeComponent();
            lblClientName.Text = "";
            tbBarcode.Text = "";
            tbBarcode.Focus();
        }

        public int BarCode
        {
            get { return ibarcode; }
        }

        private void tbBarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                if (tbBarcode.Text != "")
                {
                    try
                    {
                        ibarcode = Convert.ToInt32(tbBarcode.Text);
                        if (ibarcode > 0)
                            this.DialogResult = DialogResult.OK;
                        else
                            this.DialogResult = DialogResult.Cancel;
                        this.Hide();
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ibarcode = 0;
            this.Hide();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        public void INIT(string scaption, string clientname)
        {
            this.Text = scaption;
            lblClientName.Text = clientname;
            tbBarcode.Text = "";
        }
    }
}
