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
        string sbarcode = "";

        public BarCodeEntryForm()
        {
            InitializeComponent();
            lblClientName.Text = "";
            tbBarcode.Text = "";
            chkAutoService.Checked = CCFBPrefs.AutomaticallyGiveService;
        }

        public string BarCode
        {
            get { return sbarcode; }
        }

        public Boolean GiveService()
        {
            return chkAutoService.Checked;
        }

        private void tbBarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                if (tbBarcode.Text.Length >0)
                {
                    try
                    {
                        sbarcode = tbBarcode.Text.Trim();
                        if (sbarcode.Length > 0)
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
            sbarcode = "";
            this.Hide();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        public void INIT(string scaption, string clientname, bool showGiveService)
        {
            this.Text = scaption;
            lblClientName.Text = clientname;
            tbBarcode.Text = "";
            sbarcode = "";
            chkAutoService.Visible = showGiveService;
            tbBarcode.Focus();
        }

        private void BarCodeEntryForm_Activated(object sender, EventArgs e)
        {
            this.ActiveControl = tbBarcode;
        }

        private void tbBarcode_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(sbarcode) == true)
            {
                sbarcode = tbBarcode.Text.Trim().ToUpper();
            }
        }
    }
}
