using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ClientcardFB3
{
    public partial class EditAlertForm : Form
    {
        bool bSavePressed = false;
        public EditAlertForm(string rtfTextIn)
        {
            InitializeComponent();
            if (rtfTextIn.Contains("{") == true)
            {
                rtbAlertText.Rtf = rtfTextIn;
            }
            else
            {
                rtbAlertText.Text = rtfTextIn;
            }
        }
        public bool PressedSave()
        {
            return bSavePressed;
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            bSavePressed = true;
            this.Hide();
        }

        private void tsbCancel_Click(object sender, EventArgs e)
        {
            bSavePressed = false;
            this.Hide();
        }

        private void tsbCut_Click(object sender, EventArgs e)
        {
            rtbAlertText.Cut();
        }

        private void tsbCopy_Click(object sender, EventArgs e)
        {
            rtbAlertText.Copy();
        }

        private void tsbPaste_Click(object sender, EventArgs e)
        {
            rtbAlertText.Paste();
        }

        private void tsbUndo_Click(object sender, EventArgs e)
        {
            rtbAlertText.Undo();
        }

        private void tsbRedo_Click(object sender, EventArgs e)
        {
            rtbAlertText.Redo();
        }

        private void setFontStyle(bool isBold, bool isItalics, bool isUnderLined)
        {
            if (isBold == true && isItalics == true && isUnderLined == true)
            {
                rtbAlertText.SelectionFont = new System.Drawing.Font(rtbAlertText.SelectionFont, FontStyle.Bold | FontStyle.Italic | FontStyle.Underline);
            }
            else if (isBold == true && isItalics == true && isUnderLined == false)
            {
                rtbAlertText.SelectionFont = new System.Drawing.Font(rtbAlertText.SelectionFont, FontStyle.Bold | FontStyle.Italic);
            }
            else if (isBold == true && isItalics == false && isUnderLined == false)
            {
                rtbAlertText.SelectionFont = new System.Drawing.Font(rtbAlertText.SelectionFont, FontStyle.Bold);
            }
            else if (isBold == true && isItalics == false && isUnderLined == true)
            {
                rtbAlertText.SelectionFont = new System.Drawing.Font(rtbAlertText.SelectionFont, FontStyle.Bold | FontStyle.Underline);
            }
            else if (isBold == false && isItalics == true && isUnderLined == true)
            {
                rtbAlertText.SelectionFont = new System.Drawing.Font(rtbAlertText.SelectionFont, FontStyle.Italic | FontStyle.Underline);
            }
            else if (isBold == false && isItalics == true && isUnderLined == false)
            {
                rtbAlertText.SelectionFont = new System.Drawing.Font(rtbAlertText.SelectionFont, FontStyle.Italic);
            }
            else if (isBold == false && isItalics == false && isUnderLined == true)
            {
                rtbAlertText.SelectionFont = new System.Drawing.Font(rtbAlertText.SelectionFont, FontStyle.Underline);
            }
            else
            {
                rtbAlertText.SelectionFont = new System.Drawing.Font(rtbAlertText.SelectionFont, FontStyle.Regular);
            }
        }

        private void tsbFontStyle_CheckStateChanged(object sender, EventArgs e)
        {
            setFontStyle(tsbBold.Checked, tsbItalics.Checked, tsbUnderLine.Checked);
        }

        private void tsbFont_Click(object sender, EventArgs e)
        {
            FontDialog fontfrm = new FontDialog();
            fontfrm.Font = rtbAlertText.Font;
            DialogResult dResult = fontfrm.ShowDialog(this);
            if (dResult == System.Windows.Forms.DialogResult.OK)
            {
                rtbAlertText.SelectionFont = fontfrm.Font;
            }
            fontfrm.Dispose();
        }
        public string Rtf()
        {
            return rtbAlertText.Rtf;
        }

        private void tsbColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorFrm = new ColorDialog();
            colorFrm.Color = tsbColor.BackColor;
            DialogResult dResult = colorFrm.ShowDialog(this);
            if (dResult == System.Windows.Forms.DialogResult.OK)
            {
                rtbAlertText.SelectionColor = colorFrm.Color;
                tsbColor.BackColor = colorFrm.Color;
            }
            colorFrm.Dispose();
        }
    }
}
