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
    public partial class DuplicateHouseholdNameForm : Form
    {
        Client clsClient;
        bool canceled = false;

        public bool Canceled
        {
            get
            {
                return canceled;
            }
        }

        public string HHName
        {
            get
            {
                return tbHHName.Text; ;
            }
        }

        public DuplicateHouseholdNameForm(Client clsClientIn, string nameIn)
        {
            InitializeComponent();
            clsClient = clsClientIn;

            tbHHName.Text = nameIn;
            panel1.Visible = true;
            panel1.BackColor = CCFBGlobal.bkColorFormAlt;
            BackColor = CCFBGlobal.bkColorBaseEdit;
            lvwPeople.Visible = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            canceled = true;
            this.Close();
        }

        private void btnSearchForName_Click(object sender, EventArgs e)
        {
            lvwHouseholds.Items.Clear();
            lvwPeople.Items.Clear();

            if (TestForHousehold(0) == false)
            {
                btnConfirmName.Visible = true;
            }
            else
            {
                btnConfirmName.Visible = false;
                MessageBox.Show("The Household Name Already Exists. Please "
                    + "Change Your New Household Name And Try Again", "Household Name Already Exists",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private bool TestForHHMember()
        {
            string[] splitName = tbHHName.Text.Split(',');
            panel1.Visible = false;
            lvwPeople.Items.Clear();
            HHMembers clsTmpHHM = new HHMembers(CCFBGlobal.connectionString);
            if (clsTmpHHM.openWhere("WHERE LastName = '" + splitName[0].Trim() 
                + "' AND FirstName = '" + splitName[0].Trim() + "'",true) == true)
            {
                for (int i = 0; i < clsTmpHHM.RowCount; i++)
                {
                    clsTmpHHM.SetRecord(i);
                    ListViewItem lvItm = new ListViewItem(clsTmpHHM.LastName);
                    lvItm.SubItems.Add(clsTmpHHM.FirstName);
                    lvItm.SubItems.Add(clsTmpHHM.Birthdate.ToShortDateString());
                    lvItm.SubItems.Add(clsTmpHHM.Age.ToString());
                    lvItm.SubItems.Add(clsTmpHHM.HouseholdID.ToString());
                    if (i % 2 == 0)
                        lvItm.BackColor = Color.White;
                    else
                        lvItm.BackColor = Color.LightYellow;

                    lvwPeople.Items.Add(lvItm);
                    //TestForHousehold(clsTmpHHM.HouseholdID);
                }
                panel1.Visible = true;
                return true;
            }
            return TestForHousehold(0);
        }

        private bool TestForHousehold(int TestHHId)
        {
            Household clsTmpHH = new Household(CCFBGlobal.connectionString);
            string sqlWhere = "Name = '" + tbHHName.Text + "'";
            if (TestHHId > 0)
                sqlWhere += " OR ID = " + TestHHId.ToString();
            clsTmpHH.openWhere(sqlWhere);
            if (clsTmpHH.RowCount > 0)
            {
                lvwHouseholds.Items.Clear();
                for (int i = 0; i < clsTmpHH.RowCount; i++)
                {
                    clsTmpHH.SetRecord(i);
                    ListViewItem lvItm = new ListViewItem(clsTmpHH.Name);
                    lvItm.SubItems.Add(clsTmpHH.Inactive.ToString());
                    lvItm.SubItems.Add(clsTmpHH.Address.ToString());
                    lvItm.SubItems.Add(clsTmpHH.City.ToString() + ", " + clsTmpHH.State.ToString() + " " + clsTmpHH.Zipcode.ToString());
                    lvItm.SubItems.Add(clsTmpHH.HHMembersCount(clsTmpHH.ID).ToString());
                    lvItm.SubItems.Add(clsTmpHH.ID.ToString());
                    if (i % 2 == 0)
                        lvItm.BackColor = Color.White;
                    else
                        lvItm.BackColor = Color.LightYellow;


                    lvwHouseholds.Items.Add(lvItm);
                }
                panel1.Visible = true;
                return true;
            }
            return false;
        }

        private void btnConfirmName_Click(object sender, EventArgs e)
        {
            canceled = false;
            this.Close();
        }

        private void tbHHName_TextChanged(object sender, EventArgs e)
        {
            btnConfirmName.Visible = false;
        }
    }
}
