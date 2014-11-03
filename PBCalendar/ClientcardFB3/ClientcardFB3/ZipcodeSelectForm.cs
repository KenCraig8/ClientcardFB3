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
    public partial class ZipcodeSelectForm : Form
    {
        Zipcodes clsZipcodes;

        bool canceled = false;

        public bool Canceled
        {
            get
            {
                return canceled;
            }
        }

        public ZipcodeSelectForm(Zipcodes clsZipcodesIn, string toSelect)
        {
            InitializeComponent();
            lblSearchType.Text += toSelect;
            clsZipcodes = clsZipcodesIn;
            loadList();
        }

        private void loadList()
        {
            lvZipcodes.Items.Clear();

            ListViewItem lvi;
            for (int i = 0; i < clsZipcodes.RowCount; i++)
            {
                clsZipcodes.setDataRow(i);
                lvi = new ListViewItem(clsZipcodes.ZipCode);
                lvi.Tag = i.ToString();
                lvi.SubItems.Add(clsZipcodes.City);      
                lvi.SubItems.Add(clsZipcodes.State);
                lvi.SubItems.Add(clsZipcodes.AreaCode);
                lvi.SubItems.Add(clsZipcodes.County);
                lvZipcodes.Items.Add(lvi);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            canceled = true;
            this.Close();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            setDRowForSelectedItem();
        }

        private void lvZipcodes_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            setDRowForSelectedItem();
        }

        private void setDRowForSelectedItem()
        {
            if (lvZipcodes.FocusedItem != null)
            {
                clsZipcodes.setDataRow(Convert.ToInt32(lvZipcodes.FocusedItem.Tag));
                this.Close();
            }
        }
    }
}
