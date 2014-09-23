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
    public partial class UserInputForm : Form
    {

        ServiceItemsForm frmSvcItmsIn;

        public UserInputForm(ServiceItemsForm frmIn)
        {
            InitializeComponent();

            frmSvcItmsIn = frmIn;
            CCFBGlobal.InitCombo(cboRules, CCFBGlobal.parmTbl_SvcRules);
            CCFBGlobal.InitCombo(cboType, CCFBGlobal.parmTbl_SvcCategory);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            frmSvcItmsIn.AddNewItem(tbItemDesc.Text, Convert.ToInt32(cboRules.SelectedValue), Convert.ToInt32(cboType.SelectedValue) );
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
