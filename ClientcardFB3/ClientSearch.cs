using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ClientcardFB3
{
    public partial class ClientSearch : Form
    {
        bool bNormalMode = false;
        SqlCommand cmdObj;
        SqlConnection conn = new SqlConnection();
        int currentrownbr = -1;
        MainForm frmMain;
        int selectedhhid = 0;
        string sortorder = "";
        string sql = "";

        string sQueryBase = "select hh.Id hhID, hh.Name, hh.Address, hh.City, Zipcode, Phone"
                          + ", LatestService, CASE WHEN parm.Type IS NULL THEN '----' ELSE parm.Type end Category"
                          + ", rtrim(hhm.LastName) + ', ' + rtrim(hhm.FirstName) ClientName"
                          + ", hhm.Id hhmID, hhm.Inactive HHMInactive, hh.Inactive HHInactive"
                          + "  from householdmembers hhm inner join Household hh on hhm.HouseholdID = hh.ID "
                          + " left join parm_ClientType parm on hh.ClientType = parm.ID"
                          + " WHERE hhm.NotIncludedInClientList = 0";

        public ClientSearch(string connectionstring, MainForm parentform)
        {
            InitializeComponent();
            frmMain = parentform;
            ClearForm();
            conn.ConnectionString = connectionstring;
            tbBarCode.Visible = CCFBPrefs.EnableBarcodePrompts;
            lblBarCode.Visible = CCFBPrefs.EnableBarcodePrompts;
            tbBirthDate.Focus();
        }

        public string BarCode
        {
            get { return tbBarCode.Text; }
            set { tbBarCode.Text = value; }
        }

        private string appendClause(string newclause)
        {
            return " AND " + newclause;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            selectedhhid = 0;
            this.Visible = false;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            loadList();
        }

        private void ClearForm()
        {
            btnSelect.Enabled = false;
            dgvClientList.Rows.Clear();
            iterateControls(this.Controls);
        }

        private void dgvClientList_SelectionChanged(object sender, EventArgs e)
        {
            if (bNormalMode == true)
                fillClientInfo(dgvClientList.CurrentRow.Index);
        }

        private void fillClientInfo(int rownbr)
        {
            currentrownbr = rownbr;
            tbClientInfo.Tag = dgvClientList.Rows[rownbr].Cells["clmHHID"].Value.ToString();
            tbClientInfo.Text = "ID " + dgvClientList.Rows[rownbr].Cells["clmHHID"].Value + "\r\n"
                              + dgvClientList.Rows[rownbr].Cells["clmName"].Value + " \r\n"
                              + dgvClientList.Rows[rownbr].Cells["clmAddress"].Value  + " \r\n"
                              + dgvClientList.Rows[rownbr].Cells["clmCity"].Value + ", "
                              + dgvClientList.Rows[rownbr].Cells["clmZip"].Value  + " \r\n"
                              + dgvClientList.Rows[rownbr].Cells["clmPhone"].Value;
            tbClientInfo.BackColor = Color.White;
            btnSelect.Enabled = true; 
        }

        private string getWhereClause()
        {
            sql = "";
            sortorder = "Name, hh.ID, HeadHH DESC, ClientName";
            if (chkIncludeInactive.Checked == false)
                sql += appendClause("hh.Inactive = 0");

            if (tbBirthDate.Text.Trim().Length >0)
            {
                sql += appendClause("BirthDate = '" + tbBirthDate.Text.Trim() + "'");
            }
            if (tbLastName.Text.Length >0)
            {
                sql += appendClause("LastName ");
                if (rdoLastStartsWith.Checked == true)
                    sql += " like '" + tbLastName.Text.Trim() + "%'";
                else if (rdoLastContains.Checked == true)
                    sql += " like '%" + tbLastName.Text.Trim() + "%'";
                else
                    sql += " = '" + tbLastName.Text.Trim() + "'";
            }
            if (tbFirstName.Text.Length >0)
            {
                sql += appendClause("FirstName ");
                if (rdoFirstStartsWith.Checked == true)
                    sql += " like '" + tbFirstName.Text.Trim() + "%'";
                else if (rdoFirstContains.Checked == true)
                    sql += " like '%" + tbFirstName.Text.Trim() + "%'";
                else
                    sql += " = '" + tbFirstName.Text.Trim() + "'";
                //sortorder = "Name, hh.ID, HeadHH DESC, FirstName";
            }
            if (tbHouseNbr.Text.Length >0)
            {
                sql += appendClause("Address like '" + tbHouseNbr.Text.Trim() + "%'");
                sortorder = "Address, hh.ID, HeadHH DESC, ClientName";
            }
            if (tbAptNbr.Text.Length >0)
            {
                sql += appendClause("AptNbr LIKE '%" + tbAptNbr.Text.Trim() + "%'");
                sortorder = "AptNbr, Address, hh.ID, HeadHH DESC, ClientName";
            }
            if (tbZipCode.Text.Length >0)
            {
                sql += appendClause("Zipcode = '" + tbZipCode.Text.Trim() + "'");
            }
            return sql;
        }

        /// <summary>
        /// Traverses all controls on the page using recursion and adds the proper ones
        /// to their proper collections and adds LostFocus event to Textboxes and Checkboxes
        /// </summary>
        /// <param name="controlList"></param>
        private void iterateControls(Control.ControlCollection controlList)
        {
            foreach (Control ctrl in controlList.OfType<Control>())
            {
                switch (ctrl.GetType().Name)
                {
                    case "TextBox":
                        {
                            ((TextBox)ctrl).Text = "";
                            break;
                        }
                    default:
                        {
                            iterateControls(ctrl.Controls);
                            break;
                        }
                }
            }
            
        }

        
        /// <summary>
        /// Clears Datagrid and Loads the Clients
        /// </summary>
        public void loadList()
        {
            string hhname = "";
            string memname = "";
            bNormalMode = false;
            int hhid = 0;
            tbClientInfo.Text = "";
            dgvClientList.Rows.Clear();
            Application.DoEvents();

            cmdObj = new SqlCommand(sQueryBase + getWhereClause() + " ORDER BY " + sortorder, conn);
            sqlConnectionOpen();
            SqlDataReader reader = cmdObj.ExecuteReader(CommandBehavior.SingleResult);
            if (reader.HasRows)
            {
                int i = 0;
                while (reader.Read())
                {
                    hhname = reader.GetSqlString(1).ToString();
                    memname = reader.GetSqlString(8).ToString();
                    dgvClientList.Rows.Add();
                    //if (hhid != reader.GetInt32(0))
                    //{
                        hhid = reader.GetInt32(0);
                        dgvClientList.Rows[i].Cells["clmHHID"].Value = hhid.ToString();
                        dgvClientList.Rows[i].Cells["clmName"].Value = hhname;
                        dgvClientList.Rows[i].Cells["clmAddress"].Value = reader.GetSqlString(2);
                        dgvClientList.Rows[i].Cells["clmCity"].Value = reader.GetSqlString(3);
                        dgvClientList.Rows[i].Cells["clmZip"].Value = reader.GetSqlString(4);
                        dgvClientList.Rows[i].Cells["clmPhone"].Value = reader.GetSqlString(5);
                        if (reader.GetSqlDateTime(6).IsNull == true)
                            dgvClientList.Rows[i].Cells["clmLatestService"].Value = "";
                        else
                            dgvClientList.Rows[i].Cells["clmLatestService"].Value = reader.GetDateTime(6).ToShortDateString();

                        dgvClientList.Rows[i].Cells["clmCategory"].Value = reader.GetSqlString(7);
                    //}
                    //else
                    //{
                    //    dgvClientList.Rows[i].Cells["clmHHID"].Value = hhid.ToString();
                    //}
                    dgvClientList.Rows[i].Cells["clmMemberName"].Value = reader.GetSqlString(8);
                    dgvClientList.Rows[i].Cells["clmMemID"].Value = reader.GetInt32(9).ToString();
                    if (reader.GetBoolean(11) == true)
                        dgvClientList.Rows[i].DefaultCellStyle.ForeColor = Color.Maroon;
                    else
                        dgvClientList.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                    //    dgvClientList.Rows[i].DefaultCellStyle.ForeColor = Color.DarkViolet;
                    //else if (Convert.ToBoolean(CCFBGlobal.NullToZero(clsClient.DSet.Tables[0].Rows[i]["Inactive"])) == true)
                    //    
                    //else
                    //    dgvClientList.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                    i++;
                }
            }
            sqlConnectionClose();
            Application.DoEvents();
            if (dgvClientList.RowCount > 0)
            {
                dgvClientList.Rows[0].Selected = true;
                fillClientInfo(0);
            }
            bNormalMode = true;
        }

        public int SelectedHHID
        {
            get { return selectedhhid; }
        }

        /// <summary>
        /// Closes a connection to the Database
        /// </summary>
        private void sqlConnectionClose()
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }

        /// <summary>
        /// Opens a connection to the Database
        /// </summary>
        private void sqlConnectionOpen()
        {
            if (conn.State == ConnectionState.Closed)
                conn.Open();
        }

        private void tbBarCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                if (tbBarCode.Text.Length >0)
                {
                    string testBarCode = tbBarCode.Text;
                    int testID = -1;
                    int iHHM = 0;
                    if (CCFBPrefs.BarcodeUseFamilyMember == true)
                    {
                        testID = CCFBGlobal.getClientFromBarCode(testBarCode);
                    }
                    else
                    {
                        testID = CCFBGlobal.getHHFromBarCode(testBarCode, ref iHHM);
                    }
                    if (testID > 0)
                    {
                        frmMain.setHousehold(selectedhhid, 0);
                        this.Visible = false;
                    }
                    else
                    {
                        MessageBox.Show("This barcode is not in the database", "Barcode Search", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                }
            }
        }

        private void tbFliter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
                loadList();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            frmMain.setHousehold(Convert.ToInt32(tbClientInfo.Tag.ToString()), 0);
            this.Visible = false;
        }
    }
}
