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
        parmTypeCodes parmDonorTypeCode = null;  //new parmTypeCodes(CCFBGlobal.parmTbl_Donor, CCFBGlobal.connectionString, "");

        List<Button> btnListFavorites = new List<Button>();     //Collection of tpgFavorites Buttons
        List<Button> btnListDonorType = new List<Button>();     //Collection of tpgDonorType Buttons

        public DataGridView dataGridView1;
        

        public SelectDonor(LoginForm loginform)
        {
            frmLogIn = loginform;
            InitializeComponent();
            parmDonorTypeCode = CCFBGlobal.parmTableTypeCodes(CCFBGlobal.parmTbl_Donor);
            LoadListFavoritesButtons();
            LoadListDonorTypeButtons();
        }

        private void SelectDonor_Load(object sender, EventArgs e)
        {
//            IEnumerator enumerator1 = this.tpgDonorType.Controls.GetEnumerator();
            clsFoodDonations.getFavorite();
            System.Windows.Forms.Button button;
            try
            {
                for (int j = 0; j < this.tpgFavorites.Controls.Count; j++)
                {
                    btnListFavorites[j].Hide();
                    btnListFavorites[j].Text = "";
                }
                for (int i = 0; i < clsFoodDonations.RowCount; i++)
                {
                    button = btnListFavorites[i];
                    button.Text = clsFoodDonations.DSet.Tables["FoodDonations"].Rows[i][1].ToString();
                    button.Tag = clsFoodDonations.DSet.Tables["FoodDonations"].Rows[i][0].ToString();
                    button.Show();
                }

                for (int j = 0; j < this.tpgDonorType.Controls.Count; j++)
                {
                    btnListDonorType[j].Hide();
                    btnListDonorType[j].Text = "";
                }
                int btnCnt = -1;
                //for (int i = 0; i < parmDonorTypeCode.TypeCodesArray.Count; i++)
                foreach (parmType item in parmDonorTypeCode.TypeCodesArray)
                {
                    int itmID = item.ID;
                    clsDonors.openWhere(" WHERE RcdType = " + itmID.ToString());
                    if (clsDonors.RowCount > 0)
                    {
                        btnCnt ++;
                        button = btnListDonorType[btnCnt];
                        button.Text = item.LongName;
                        button.Tag = itmID;
                        button.Show();
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

        private void fillDataGrid_DnrType(Button btnSelected)
        {
            tbGridDesc.Text = "DONORS with Type Code = " + btnSelected.Text;
            string whereClause = " WHERE RcdType=" + btnSelected.Tag.ToString() + " ORDER BY Name";
            clsDonors.openWhere(whereClause);
            fillDgv();
        }
        private void fillDataGridFilterByAlphabet(Button btnSelected)
        {
            tbGridDesc.Text = "DONORS starting with " + btnSelected.Text;
            string whereClause = " WHERE Name LIKE '" + btnSelected.Text + "%'";
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
            FoodDonationForm frm2 = new FoodDonationForm(b.Tag.ToString(), b.Text);
            frm2.Show();
        }

        //Donor Type button click  

        private void dnrTypeBtn_Click(object sender, MouseEventArgs e)
        {
            fillDataGrid_DnrType((Button)sender);
        }
        //Alphabet button click

        private void alphabetBtn_Click(object sender, MouseEventArgs e)
        {
            fillDataGridFilterByAlphabet((Button)sender);
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
            tbGridDesc.Text = "";
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

        private void LoadListFavoritesButtons()
        {
            btnListFavorites.Clear();
            btnListFavorites.Add(btnFavorites0);
            btnListFavorites.Add(btnFavorites1);
            btnListFavorites.Add(btnFavorites2);
            btnListFavorites.Add(btnFavorites3);
            btnListFavorites.Add(btnFavorites4);
            btnListFavorites.Add(btnFavorites5);
            btnListFavorites.Add(btnFavorites6);
            btnListFavorites.Add(btnFavorites7);
            btnListFavorites.Add(btnFavorites8);
            btnListFavorites.Add(btnFavorites9);
            btnListFavorites.Add(btnFavorites10);
            btnListFavorites.Add(btnFavorites11);
            btnListFavorites.Add(btnFavorites12);
            btnListFavorites.Add(btnFavorites13);
            btnListFavorites.Add(btnFavorites14);
            btnListFavorites.Add(btnFavorites15); 
            btnListFavorites.Add(btnFavorites16); 
            btnListFavorites.Add(btnFavorites17); 
            btnListFavorites.Add(btnFavorites18); 
            btnListFavorites.Add(btnFavorites19); 
            btnListFavorites.Add(btnFavorites20);
        }

        private void LoadListDonorTypeButtons()
        {
            btnListDonorType.Clear();
            btnListDonorType.Add(btnDonorType0);
            btnListDonorType.Add(btnDonorType1);
            btnListDonorType.Add(btnDonorType2);
            btnListDonorType.Add(btnDonorType3);
            btnListDonorType.Add(btnDonorType4);
            btnListDonorType.Add(btnDonorType5);
            btnListDonorType.Add(btnDonorType6);
            btnListDonorType.Add(btnDonorType7);
            btnListDonorType.Add(btnDonorType8);
            btnListDonorType.Add(btnDonorType9);
            btnListDonorType.Add(btnDonorType10);
            btnListDonorType.Add(btnDonorType11);
            btnListDonorType.Add(btnDonorType12);
            btnListDonorType.Add(btnDonorType13);
            btnListDonorType.Add(btnDonorType14);
            btnListDonorType.Add(btnDonorType15);
            btnListDonorType.Add(btnDonorType16);
            btnListDonorType.Add(btnDonorType17);
        }
    }
}
