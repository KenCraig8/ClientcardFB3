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
    public partial class FamilyCardUserListPlaceholder : Form
    {
        List<UserFieldItem> userFieldList = new List<UserFieldItem>();

        public FamilyCardUserListPlaceholder()
        {
            InitializeComponent();
            //Family Card
            loadUserFieldLabels();
            tbSlot1.Text = userFieldText(CCFBPrefs.FamilyCardSlot1);
            tbSlot2.Text = userFieldText(CCFBPrefs.FamilyCardSlot2);
            tbSlot3.Text = userFieldText(CCFBPrefs.FamilyCardSlot3);
            tbSlot4.Text = userFieldText(CCFBPrefs.FamilyCardSlot4);
        }

        private void loadUserFieldLabels()
        {
            UserFields clsUserFields = new UserFields(CCFBGlobal.connectionString);
            lstbxHHUserFields.Items.Clear();
            string fldName = "";
            clsUserFields.open("Household");
            userFieldList.Clear();
            for (int i = 0; i < clsUserFields.RowCount; i++)
            {
                fldName = "UserFlag" + i.ToString();
                clsUserFields.setDataRow(fldName);
                if (fldName == clsUserFields.FldName)
                {
                    if (clsUserFields.EditLabel != "")
                    {
                        UserFieldItem fieldRow = new UserFieldItem(i, clsUserFields.EditLabel, fldName);
                        userFieldList.Add(fieldRow);
                        lstbxHHUserFields.Items.Add(fieldRow.EditLabel);
                    }
                }
            }
        }

        private void listBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (lstbxHHUserFields.SelectedItems.Count > 0)
                this.lstbxHHUserFields.DoDragDrop(this.lstbxHHUserFields.SelectedItem, DragDropEffects.Move);
        }

        private void tb_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void tb_DragDrop(object sender, DragEventArgs e)
        {
            object data = e.Data.GetData(typeof(string));
            if (data == null) { return; }
            TextBox tb = (TextBox)sender;
            tb.Text = data.ToString();
            this.lstbxHHUserFields.Items.Remove(data);
        }

        private void tb_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void listBox1_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void listBox1_DragDrop(object sender, DragEventArgs e)
        {
            object data = e.Data.GetData(typeof(String));
            if (data == null) { return; }
            this.lstbxHHUserFields.Items.Add(data.ToString());
            if (tbSlot1.Text == data.ToString())
                tbSlot1.Text = "";
            else if (tbSlot2.Text == data.ToString())
                tbSlot2.Text = "";
            else if (tbSlot3.Text == data.ToString())
                tbSlot3.Text = "";
            else if (tbSlot4.Text == data.ToString())
                tbSlot4.Text = "";
        }

        private void tb_MouseDown(object sender, MouseEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text != "")
                tb.DoDragDrop(tb.Text, DragDropEffects.Move);
        }

        private string userFieldText(int UserFlagIndex)
        {
            foreach (UserFieldItem item in userFieldList)
            {
                if (UserFlagIndex == item.FldIndex)
                    return item.EditLabel;
            }
            return "";
        }
    }
}
