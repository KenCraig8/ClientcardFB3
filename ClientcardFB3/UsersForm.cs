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
    public partial class UsersForm : Form
    {
        Userlist clsUsers = new Userlist(CCFBGlobal.connectionString);
        bool inInsertMode;
        string newUserName;
        string newUserRole;
        string newPassword;
        public UsersForm()
        {
            InitializeComponent();

            pannelPassword.Visible = false;
            inInsertMode = false;
            newUserName = "";
            newUserRole = "";
            newPassword = "";

            clsUsers.openAll();
            loadGrid();

            dataGridViewUsers.Columns["clmUserName"].Tag = "UserName";
            dataGridViewUsers.Columns["clmUserRole"].Tag = "UserRole";

            //this.BackColor = CCFBGlobal.bkColorBaseEdit;
            pannelPassword.BackColor = CCFBGlobal.bkColorBaseEdit;
            dataGridViewUsers.BackgroundColor = CCFBGlobal.bkColorBaseEdit;

        }

        private void loadGrid()
        {
            dataGridViewUsers.Rows.Clear();

            for (int i = 0; i < clsUsers.RowCount; i++)
            {
                dataGridViewUsers.Rows.Add();
                dataGridViewUsers["clmUserName", i].Value = clsUsers.DSet.Tables[0].Rows[i]["Username"];
                dataGridViewUsers["clmUserRole", i].Value = clsUsers.DSet.Tables[0].Rows[i]["UserRole"];
                dataGridViewUsers["clmID", i].Value = clsUsers.DSet.Tables[0].Rows[i]["ID"];
            }
        }

        private void dataGridViewUsers_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = dataGridViewUsers.CurrentRow.Index;
            if (dataGridViewUsers.CurrentRow.Cells["clmId"].Value != null)
            {
                clsUsers.SetDataValue(rowIndex, "UserRole",dataGridViewUsers.CurrentRow.Cells[1].Value.ToString());

                clsUsers.SetDataValue(rowIndex, "UserName",dataGridViewUsers.CurrentRow.Cells[0].Value.ToString());

                clsUsers.update();
            }
            else
            {
                switch (e.ColumnIndex)
                {
                    case 0:
                        {
                        newUserName = CCFBGlobal.NullToBlank (dataGridViewUsers.Rows[rowIndex].Cells[0].Value);
                        if (String.IsNullOrEmpty(newUserName) == true)
                        {
                            if (MessageBox.Show ("Do you want me to delete this row?","",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
                                dataGridViewUsers.Rows.RemoveAt(rowIndex);
                        }
                        dataGridViewUsers.Rows[rowIndex].Cells[1].Value = CCFBGlobal.nameUserRole_Intake;
                        break;
                        }
                    default:
                        {

                        newUserRole = dataGridViewUsers.Rows[rowIndex].Cells[1].GetEditedFormattedValue
                                      (rowIndex, DataGridViewDataErrorContexts.LeaveControl).ToString();
                        break;
                        }
                }
                pannelPassword.Visible = true;
                btnSetPassword.Visible = false;
                btnDelete.Enabled = false;
                tbPassword1.Focus();
            }
        }

        private void UsersForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (dataGridViewUsers.IsCurrentRowDirty == true && 
                dataGridViewUsers.CurrentRow.Cells["clmId"].Value != null)
            {
                clsUsers.SetDataValue(dataGridViewUsers.CurrentRow.Index, "UserRole",
                  dataGridViewUsers.CurrentRow.Cells[1].GetEditedFormattedValue
                  (dataGridViewUsers.CurrentRow.Index, DataGridViewDataErrorContexts.LeaveControl).ToString());

                clsUsers.SetDataValue(dataGridViewUsers.CurrentRow.Index, "UserName",
                  dataGridViewUsers.CurrentRow.Cells[0].GetEditedFormattedValue
                  (dataGridViewUsers.CurrentRow.Index, DataGridViewDataErrorContexts.LeaveControl).ToString());

                clsUsers.update();
            }
            else if (dataGridViewUsers.CurrentRow.Cells["clmId"] == null)
            {
                DataRow newRow = clsUsers.DSet.Tables[0].NewRow();
                newRow["UserName"] = dataGridViewUsers.Rows[dataGridViewUsers.CurrentRow.Index].Cells[0].Value;
                newRow["UserRole"] = dataGridViewUsers.Rows[dataGridViewUsers.CurrentRow.Index].Cells[1].GetEditedFormattedValue
                  (dataGridViewUsers.CurrentRow.Index, DataGridViewDataErrorContexts.LeaveControl).ToString();
                clsUsers.DSet.Tables[0].Rows.Add(newRow);
                clsUsers.insert();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSetPassword_Click(object sender, EventArgs e)
        {
            tbPassword1.Text = "";
            tbPassword2.Text = "";
            lblUsername.Text = CCFBGlobal.NullToBlank(dataGridViewUsers.CurrentRow.Cells[0].Value); 
            pannelPassword.Visible = true;
            btnSetPassword.Visible = false;
            btnDelete.Enabled = false;
            tbPassword1.Focus();
        }
        private void btnConfirm_Click(object sender, EventArgs e)
        { TestPassword(); }

        private void btnCancelSetPwd_Click(object sender, EventArgs e)
        {
            pannelPassword.Visible = false;
            btnSetPassword.Visible = true;
            btnDelete.Enabled = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Delete User " + dataGridViewUsers.
                Rows[dataGridViewUsers.CurrentRow.Index].Cells["clmUserName"].Value.ToString(), "Delete User", MessageBoxButtons.YesNo)
                == System.Windows.Forms.DialogResult.Yes)
            {
                clsUsers.delete(Int32.Parse(dataGridViewUsers["clmID", dataGridViewUsers.CurrentRow.Index].Value.ToString()));
                clsUsers.openAll();
                loadGrid();
            }
        }

        private void dataGridViewUsers_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            inInsertMode = true;
            btnDelete.Enabled = false; 
        }

        private void InsertNewUser()
        {
            DataRow newRow = clsUsers.DSet.Tables[0].NewRow();
            newRow["UserName"] = newUserName;
            newRow["UserRole"] = newUserRole;
            newRow["Password"] = newPassword;
            clsUsers.DSet.Tables[0].Rows.Add(newRow);
            clsUsers.insert();
            clsUsers.openAll();
            dataGridViewUsers.EndEdit(DataGridViewDataErrorContexts.InitialValueRestoration);
            loadGrid();
            for (int i = 0; i < dataGridViewUsers.Rows.Count; i++)
            {
                if (dataGridViewUsers["clmUserName", i].Value.ToString() == newUserName)
                {
                    dataGridViewUsers.Rows[i].Selected = true;
                    break;
                }
            }
            inInsertMode = false;
            newUserRole = "";
            newUserName = "";
            newPassword = "";
            pannelPassword.Visible = false;
            btnSetPassword.Visible = true;
            btnDelete.Enabled = true;
        }

        private void dataGridViewUsers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tbPassword2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == "\r") 
                { TestPassword(); }
        }
        private void TestPassword()
        {
            if ((tbPassword1.Text.Trim() == tbPassword2.Text.Trim())
                        && tbPassword1.Text.Trim().Length >0)
            {
                if (inInsertMode == true)
                {
                    if (newUserName.Length >0)
                    {
                        if (String.IsNullOrEmpty(newUserRole) == true)
                            newUserRole = CCFBGlobal.nameUserRole_Intake;
                        newPassword = tbPassword1.Text.Trim();
                        InsertNewUser();
                        pannelPassword.Visible = false;
                        btnSetPassword.Visible = true;
                        btnDelete.Enabled = true;
                    }
                    else
                    {
                    }
                }
                else
                {
                    clsUsers.SetDataValue(dataGridViewUsers.CurrentRow.Index, "Password", tbPassword1.Text.Trim());

                    if (clsUsers.update() == true)
                    {
                        MessageBox.Show("The Password Was Updated");
                        pannelPassword.Visible = false;
                        btnSetPassword.Visible = true;
                        btnDelete.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("New Password Same As Old Password. Please Try again");
                    }
                }
            }
            else
            {
                MessageBox.Show("The Passwords You Entered Were Either Empty Or Did Not Match");
            }
        }


    }
}
