using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FoodReceipt;
using System.Data.SqlClient;
using System.Collections;

namespace FoodReceipt
{
    public partial class SelectDonor : Form
    {

        LoginForm frmLogIn;
        FoodDonations clsFoodDonations = new FoodDonations(CCFBGlobal.connectionString);
        Donors clsDonors = new Donors(CCFBGlobal.connectionString);
        parmTypeCodes parmDonorTypeCode = new parmTypeCodes(CCFBGlobal.parmTbl_Donor, CCFBGlobal.connectionString, "");
        public DataGridView dataGridView1;
        

        public SelectDonor(LoginForm loginform)
        {
            frmLogIn = loginform;
            InitializeComponent();
            formLoad();
        }
        private void formLoad()
        {
            IEnumerator enumerator = this.tabPage2.Controls.GetEnumerator();
            IEnumerator enumerator1 = this.tabPage1.Controls.GetEnumerator();
            clsFoodDonations.getFavorite();
            System.Windows.Forms.Button button;
            try
            {
                for (int i = 0; i < clsFoodDonations.RowCount; i++)
                {
                    enumerator.MoveNext();
                    button = (System.Windows.Forms.Button)enumerator.Current;
                    button.Text = clsFoodDonations.DSet.Tables["FoodDonations"].Rows[i][1].ToString();
                    button.Name = clsFoodDonations.DSet.Tables["FoodDonations"].Rows[i][0].ToString();
                }   
                for (int j = 0; j < this.tabPage2.Controls.Count; j++)
                {
                    enumerator.MoveNext();
                    button = (System.Windows.Forms.Button)enumerator.Current;
                    if (button.Text == "")
                    {
                        button.Hide();
                    }
                }
                for (int i = 0; i < parmDonorTypeCode.TypeCodesArray.Count; i++)
                {
                    enumerator1.MoveNext();
                    button = (System.Windows.Forms.Button)enumerator1.Current;
                    button.Text = parmDonorTypeCode.GetLongName(i);
                    string tmp = parmDonorTypeCode.GetLongName(i);
                    button.Name = parmDonorTypeCode.GetId(tmp).ToString();
                }
                for (int j = 0; j < this.tabPage1.Controls.Count; j++)
                {
                    enumerator1.MoveNext();
                    button = (System.Windows.Forms.Button)enumerator1.Current;
                    if (button.Text == "")
                    {
                        button.Hide();
                    }
                }

            }
            catch (Exception ex)
            {
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString());
            }
        }

        private void SelectDonor_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmLogIn.Close();
        }

        private void fillDataGrid_DnrType(String buttonID)
        {
            int val = Convert.ToInt32(buttonID);
            string whereClause = " WHERE RcdType=" + val;
            clsDonors.openWhere(whereClause);
            fillDgv();
        }
        private void fillDataGridFilterByAlphabet(String StartsWith)
        {
            string whereClause = " WHERE Name LIKE '" + StartsWith + "%'";
            clsDonors.openWhere(whereClause);
            fillDgv();
        }

        private void fillDataGrid_DnrID(String text)
        {
            if (text != "")
            {
                int val = Convert.ToInt32(text);
                clsDonors.open(val);
                fillDgv();
            }
        }

        public void fillDgv()
        {
            dgvDonorList.Rows.Clear();
            string ID = "";
            string Name = "";
            DataGridViewRow dvr;
            int rowCount = 0;
            for (int i = 0; i < clsDonors.RowCount; i++)
            {
                try
                {
                    clsDonors.setDataRow(i);
                    Name = clsDonors.DSet.Tables["Donors"].Rows[i][2].ToString();
                    ID = clsDonors.DSet.Tables["Donors"].Rows[i][0].ToString();

                    //DataGrid View
                    dgvDonorList.Rows.Add(ID, Name);
                    dvr = dgvDonorList.Rows[rowCount];
                    rowCount++;
                }
                catch (Exception ex)
                {
                    CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString());
                }
            }
        }

        public void dgvDonorList_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                int selectedRowIndex = dgvDonorList.CurrentCell.RowIndex;
                string ID = dgvDonorList.Rows[selectedRowIndex].Cells[0].Value.ToString().Trim();
                string Name = dgvDonorList.Rows[selectedRowIndex].Cells[1].Value.ToString();
                FoodDonationForm frm2 = new FoodDonationForm(ID, Name);
                frm2.Show();
            }
            catch (Exception ex)
            {
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString());
            }
        }

        //Favorite button click

        private void favBtn_Click(object sender, MouseEventArgs e)
        {
            Button b = (Button)sender;
            FoodDonationForm frm2 = new FoodDonationForm(b.Name, b.Text);
            frm2.Show();
        }

        //Donor Type button click  

        private void dnrTypeBtn_Click(object sender, MouseEventArgs e)
        {
            fillDataGrid_DnrType(((Button)sender).Name);
        }
        //Alphabet button click

        private void alphabetBtn_Click(object sender, MouseEventArgs e)
        {
            fillDataGridFilterByAlphabet(((Button)sender).Text);
        }

        //NumPad button click

        private void numPadBtn_Click(object sender, MouseEventArgs e)
        {
            int num = Convert.ToInt32(((Button)sender).Text);

            if (string.IsNullOrEmpty(textBox1.Text) && num == 0)
            {
                return;
            }

            textBox1.Text += ((Button)sender).Text;
        }

        private void numpadCLR_Click(object sender, MouseEventArgs e)
        {
            textBox1.Clear();
        }

        private void btnSearch_Click(object sender, MouseEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                return;
            }
            fillDataGrid_DnrID(textBox1.Text);
        }
        void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvDonorList.Rows.Clear();
        }
        private void dgvDonorList_Resize(object sender, EventArgs e)
        {
            ResizeGridColumns();
        }

        private void ResizeGridColumns()
        {
            //get sum of non-resizable columns width
            int diffWidth = 0;
            foreach (DataGridViewColumn col in dgvDonorList.Columns)
            {
                if (col.Resizable == DataGridViewTriState.False && col.Visible) diffWidth += col.Width;
            }

            //calculate available width
            int totalResizableWith = dgvDonorList.Width - diffWidth;

            //resize column width based on previous proportions
            for (int i = 0; i < dgvDonorList.ColumnCount; i++)
            {
                try
                {
                    if (dgvDonorList.Columns[i].Resizable != DataGridViewTriState.False && dgvDonorList.Columns[i].Visible)
                    {
                        dgvDonorList.Columns[i].Width = (int)Math.Floor((decimal)totalResizableWith / dgvDonorList.ColumnCount);
                    }
                }
                catch { }
            }
            //dgvFT.Columns[dgvFT.ColumnCount - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDonorList.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
    }
}
