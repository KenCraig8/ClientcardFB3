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
    public partial class FindClientForm : Form
    {
        bool bNormalMode = false;
        bool refreshing = false;
        public Client clsClient = new Client();
        parm_ClientType clsParm_ClientType;
        private int currentHHId = 0;
        string firstColName = "colNameLF";
        MainForm frmMainIn;

        //Used to tell if the list includes inactives or not
        string includeInactive = " (m.Inactive=0 or m.Inactive IS NULL) AND h.Inactive = 0 AND "
                + " (m.NotIncludedInClientList=0 or m.NotIncludedInClientList IS NULL) ";
        public int indexFindClient = 0;
        string lastSearchText = "";
        bool loadingName = false;
        List<string> rdoButtons = new List<string>();
        int rowIndex = 0;
        string sOrderBy;
        string sortBy = "";
        string sWhereClause;
        int hhMemID = 0;
        int rowCount = 0;

        public int HHMemID
        {
            get
            {
                return hhMemID;
            }
        }

        public int RowCount
        {
            get
            {
                return rowCount;
            }
        }

        public FindClientForm(MainForm FrmMainIn)
        {
            InitializeComponent();
            frmMainIn = FrmMainIn;
            clsParm_ClientType = new parm_ClientType(clsClient.connectionString);
            clsParm_ClientType.openAll();
            sWhereClause = includeInactive;
            cboOrderBy.SelectedIndex = 0;
        }

        public FindClientForm()
        {
            InitializeComponent();
            clsParm_ClientType = new parm_ClientType(clsClient.connectionString);
            clsParm_ClientType.openAll();
            sWhereClause = includeInactive;
            cboOrderBy.SelectedIndex = 0;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            refreshing = true;
            btnRefresh.Enabled = false;

            if (sortBy == "City" || sortBy == "Zipcode")
                getDistints(sortBy);

            loadList();
            btnRefresh.Enabled = true;
            tbFindName.Focus();
            refreshing = false;
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            SetIdAndClose();
        }

        private void cboFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (loadingName == false)
            {
                if (cboFilter.SelectedIndex != 0)
                {
                    indexFindClient = 0;
                    if (sortBy != "ClientType")
                    {
                        sWhereClause = sortBy + "='" + cboFilter.SelectedItem.ToString() + "' And ";
                    }
                    else
                    {
                        sWhereClause = sortBy + "='" + clsParm_ClientType.getParmInt(cboFilter.SelectedItem.ToString())
                            + "' And ";
                    }
                }
                else
                {
                    sWhereClause = "";
                }
                if (refreshing == false)
                    loadList();
            }
        }

        private void cboOrderBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboFilter.Visible = false;
            cboFilter.Text = "";
            lblFilterBy.Visible = false;
            string temp = " ORDER BY ";
            sWhereClause = " ";
            switch (cboOrderBy.SelectedIndex)
            {
                case 0:
                    { temp += "LastName, FirstName"; firstColName = "colNameLF"; break; }
                case 1:
                    { temp += "FirstName, LastName"; firstColName = "colNameFL"; break; }
                case 2:
                    { temp += "Address, LastName, FirstName"; firstColName = "colNameLF"; break; }
                case 3:
                    {
                        temp += "City, LastName, FirstName"; firstColName = "colNameLF";
                        cboFilter.Visible = true; getDistints("City"); break;
                    }
                case 4:
                    {
                        temp += "ZipCode, LastName, FirstName"; firstColName = "colNameLF";
                        cboFilter.Visible = true; getDistints("Zipcode"); break;
                    }
                case 5:
                    { temp += "Phone, LastName, FirstName"; firstColName = "colNameLF"; break; }
                case 6:
                    { temp += "h.Id, LastName, FirstName"; firstColName = "colNameLF"; break; }
                case 7:
                    {
                        temp += "ClientTypeName, LastName, FirstName"; firstColName = "colNameLF";
                        cboFilter.Visible = true; getDistints("ClientType"); break;
                    }
                default:
                    { temp += "m.Id, LastName, FirstName"; firstColName = "colNameLF"; break; }
            }
            if (temp != sOrderBy)
            {
                sOrderBy = temp;

                if (cboOrderBy.SelectedIndex != 3 && cboOrderBy.SelectedIndex != 4
                    && cboOrderBy.SelectedIndex != 7)
                {
                    loadList();
                }
            }
            lblFilterBy.Visible = cboFilter.Visible;
            tbFindName.Focus();
            Application.DoEvents();
        }

        /// <summary>
        /// Reloads the clients but also includes the inactive clients
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkIncludeInactive_CheckStateChanged(object sender, EventArgs e)
        {
            loadingName = true;
            if (chkIncludeInactive.Checked == true)
            {
                includeInactive = " (m.NotIncludedInClientList=0 or m.NotIncludedInClientList IS NULL) ";
                lblInactiveHH.Visible = true;
                lblInactiveHHMem.Visible = true;
            }
            else
            {
                includeInactive = " (m.Inactive=0 or m.Inactive IS NULL) AND h.Inactive = 0 AND "
                + " (m.NotIncludedInClientList=0 or m.NotIncludedInClientList IS NULL) ";
                lblInactiveHH.Visible = false;
                lblInactiveHHMem.Visible = false;
            }

            if (sortBy == "City" || sortBy == "Zipcode" || sortBy == "ClientType")
                getDistints(sortBy);


            sWhereClause = "";
            loadingName = false;
            loadList();
        }

        private void ClearHeader()
        {
            tbAddress.Text = "";
            tbID.Text = "";
            tbLastService.Text = "";
            tbName.Text = "";
            Application.DoEvents();
        }

        public int CurrentHHId
        {
            get { return currentHHId; }
            set { currentHHId = value; }
        }

        private void dataGridViewClients_DoubleClick(object sender, EventArgs e)
        {
            SetIdAndClose();
        }

        private void dataGridViewClients_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (bNormalMode == true)
            {
                loadingName = true;
                rowIndex = e.RowIndex;
                fillTextBoxes();
                loadingName = false;
            }
        }

        /// <summary>
        /// Fills the textbox's for the currently selected client
        /// </summary>
        private void fillTextBoxes()
        {
            try
            {
                lblPos.Text = (rowIndex + 1).ToString() + " of " + dgvClientList.Rows.Count.ToString();
                DataGridViewRow dgvRow = dgvClientList.Rows[rowIndex];

                tbName.Text = dgvRow.Cells["colHHName"].Value.ToString();
                tbAddress.Text = dgvRow.Cells["clmAddress"].Value.ToString()
                    + "\r\n" + dgvRow.Cells["clmCity"].Value.ToString()
                    + " " + dgvRow.Cells["clmZip"].Value.ToString();
                tbID.Text = dgvRow.Cells["clmHHID"].Value.ToString();
                currentHHId = Convert.ToInt32(tbID.Text);
                tbLastService.Text = dgvRow.Cells["clmLatestService"].Value.ToString();
            }
            catch (Exception ex)
            {
                CCFBGlobal.appendErrorToErrorReport("fillTextBoxes", ex.GetBaseException().ToString());
            }
        }

        private void FindByName(string colNameFull, string colNameSecond)
        {
            int rowStart = 0;
            if (tbFindName.Text.StartsWith(","))
            {
                lastSearchText = tbFindName.Text.ToUpper().Trim();
                try
                {
                    for (int i = 0; i < dgvClientList.Rows.Count; i++)
                    {

                        if (dgvClientList.Rows[i].Cells[colNameSecond].FormattedValue.ToString()
                                .StartsWith(lastSearchText) == true)
                        {
                            dgvClientList.CurrentCell = dgvClientList[0, i];
                            dgvClientList.FirstDisplayedScrollingRowIndex = i;
                            break;
                        }
                    }
                }
                catch { }
            }
            else
            {
                if (loadingName == false)
                {
                    if (tbFindName.TextLength >= lastSearchText.Length)
                        rowStart = rowIndex;
                    else
                        rowStart = 0;
                    lastSearchText = tbFindName.Text.ToUpper().Trim();
                    for (int i = rowStart; i < dgvClientList.Rows.Count; i++)
                    {
                        if (dgvClientList.Rows[i].Cells[colNameFull].FormattedValue.ToString().StartsWith(lastSearchText) == true)
                        {
                            dgvClientList.CurrentCell = dgvClientList[0, i];
                            if (i < dgvClientList.FirstDisplayedScrollingRowIndex
                                || i > dgvClientList.Rows.GetLastRow(DataGridViewElementStates.Displayed) - 5)
                                if (i > 5)
                                    dgvClientList.FirstDisplayedScrollingRowIndex = i - 5;
                                else
                                    dgvClientList.FirstDisplayedScrollingRowIndex = i;
                            break;
                        }
                    }
                }
            }
        }

        private void FindClientForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Visible = false;
        }

        private void FindClientForm_Resize(object sender, EventArgs e)
        {
            dgvClientList.Width = Width - dgvClientList.Left - 20;
            dgvClientList.Height = Height - dgvClientList.Top - 40;
        }

        private void FindClientForm_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible && rowCount > 0)
            {
                dgvClientList.FirstDisplayedScrollingRowIndex = indexFindClient;
                tbFindName.Text = "";
                lastSearchText = "";
                tbFindName.Focus();
                Application.DoEvents();
            }
        }

        /// <summary>
        /// Retrives all distinct values for a given column Name in the Household Table
        /// </summary>
        /// <param name="colName">The Name of the Column to get distincts for</param>
        private void getDistints(string colName)
        {
            cboFilter.Items.Clear();
            cboFilter.Items.Add("No Filter");
            cboFilter.SelectedIndex = 0;

            //Accesses A different Class if Column Name is ClientType
            if (colName != "ClientType")
            {
                //Gets And Adds to the filter combo 
                //the distinct values of the column from the household table
                if (chkIncludeInactive.Checked == true)
                    clsClient.clsHH.getDistincts(colName, "");
                else
                    clsClient.clsHH.getDistincts(colName, " Where Inactive=0 ");

                for (int i = 0; i < clsClient.clsHH.RowCount; i++)
                {
                    cboFilter.Items.Add(clsClient.clsHH.DSet.Tables[0].Rows[i][0].ToString());
                }
            }
            else
            {
                for (int i = 0; i < clsParm_ClientType.RowCount; i++)
                {
                    cboFilter.Items.Add(clsParm_ClientType.DSet.Tables[0].Rows[i]["Type"].ToString());
                }
            }
            sortBy = colName;
        }

        public void getNextClient(int hhID)
        {
            //As long as the frmFindClient listview has clients
            if (dgvClientList.Rows.Count > 0)
            {
                //If index is > amount of Items in listview
                if (indexFindClient >= dgvClientList.Rows.Count)
                {
                    indexFindClient = 0;  //Set index to first 
                }

                //Finds the next HouseholdID
                for (int i = 0; i < dgvClientList.Rows.Count; i++)
                {
                    if (hhID == Convert.ToInt32(dgvClientList.Rows[indexFindClient].Cells["clmHHID"].Value))
                    {
                        if (indexFindClient + 1 < dgvClientList.Rows.Count)
                            indexFindClient++;
                        else
                            indexFindClient = 0;
                    }
                    else
                    {
                        break;
                    }
                }

                //Open the client and fill the form
                SetCurrentRow(indexFindClient);
            }
        }

        public void getPrevClient(int hhID)
        {
            //if index < 0, set it to the last instance in the list
            if (indexFindClient == 0)
            {
                indexFindClient = dgvClientList.Rows.Count - 1;
            }

            //Finds the Previous HouseholdID
            for (int i = 0; i < dgvClientList.Rows.Count - 1; i++)
            {
                if (hhID == Int32.Parse(dgvClientList.Rows[indexFindClient].Cells["clmHHID"].Value.ToString()))
                {
                    if (indexFindClient - 1 >= 0)
                        indexFindClient--;
                    else
                        indexFindClient = dgvClientList.Rows.Count - 1;
                }
                else
                {
                    break;
                }
            }

            //Opens client and fills form
            SetCurrentRow(indexFindClient);
        }

        /// <summary>
        /// Clears Datagrid and Loads the Clients
        /// </summary>
        public void loadList()
        {
            bNormalMode = false;
            tbFindName.Text = "";
            lastSearchText = "";
            ClearHeader();
            dgvClientList.Rows.Clear();
            progressBar1.Value = 0;
            progressBar1.Maximum = 100;
            progressBar1.Show();
            tbFindName.Visible = false;
            Application.DoEvents();

            clsClient.openWhere(sWhereClause + includeInactive + sOrderBy);
            progressBar1.Maximum = clsClient.RowCount;

            for (int i = 0; i < clsClient.RowCount; i++)
            {
                dgvClientList.Rows.Add();

                if ((bool)clsClient.DSet.Tables[0].Rows[i]["HHInactive"] == true)
                    dgvClientList.Rows[i].DefaultCellStyle.ForeColor = Color.Maroon;
                else if (Convert.ToBoolean(CCFBGlobal.NullToZero(clsClient.DSet.Tables[0].Rows[i]["Inactive"])) == true)
                    dgvClientList.Rows[i].DefaultCellStyle.ForeColor = Color.DarkViolet;
                else
                    dgvClientList.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                if (clsClient.DSet.Tables[0].Rows[i][firstColName] == DBNull.Value)
                    dgvClientList.Rows[i].Cells["colName"].Value = clsClient.DSet.Tables[0].Rows[i]["Name"].ToString();
                else
                    dgvClientList.Rows[i].Cells["colName"].Value = clsClient.DSet.Tables[0].Rows[i][firstColName].ToString();
                dgvClientList.Rows[i].Cells["clmCity"].Value = clsClient.DSet.Tables[0].Rows[i]["City"].ToString();
                if (clsClient.DSet.Tables[0].Rows[i]["ID"].ToString() == "")
                    dgvClientList.Rows[i].Cells["clmID"].Value = "0";
                else
                    dgvClientList.Rows[i].Cells["clmID"].Value = clsClient.DSet.Tables[0].Rows[i]["ID"].ToString();
                dgvClientList.Rows[i].Cells["clmZip"].Value = clsClient.DSet.Tables[0].Rows[i]["Zipcode"].ToString();
                dgvClientList.Rows[i].Cells["clmHHID"].Value = clsClient.DSet.Tables[0].Rows[i]["HHId"].ToString();
                dgvClientList.Rows[i].Cells["clmPhone"].Value = clsClient.DSet.Tables[0].Rows[i]["Phone"].ToString();
                dgvClientList.Rows[i].Cells["clmAddress"].Value = clsClient.DSet.Tables[0].Rows[i]["Address"].ToString();
                dgvClientList.Rows[i].Cells["clmHeadHH"].Value = CCFBGlobal.NullToBlank(clsClient.DSet.Tables[0].Rows[i]["HeadHH"].ToString());
                dgvClientList.Rows[i].Cells["clmClientType"].Value = CCFBGlobal.LongNameFromId(CCFBGlobal.parmTbl_Client, Convert.ToInt32(clsClient.DSet.Tables[0].Rows[i]["ClientType"]));
                dgvClientList.Rows[i].Cells["colHHName"].Value = clsClient.DSet.Tables[0].Rows[i]["Name"].ToString();
                dgvClientList.Rows[i].Cells["colNameLF"].Value = clsClient.DSet.Tables[0].Rows[i]["colNameLF"].ToString().ToUpper().Trim();
                dgvClientList.Rows[i].Cells["colNameFL"].Value = clsClient.DSet.Tables[0].Rows[i]["colNameFL"].ToString().ToUpper().Trim();
                if (clsClient.DSet.Tables[0].Rows[i]["LatestService"].ToString() != "")
                {
                    dgvClientList.Rows[i].Cells["clmLatestService"].Value =
                        clsClient.DSet.Tables[0].Rows[i].Field<DateTime>("LatestService").ToShortDateString();
                }
                else
                {
                    dgvClientList.Rows[i].Cells["clmLatestService"].Value = "";
                }

                progressBar1.PerformStep();
            }

            //Fills the textbox's for the current selected client
            rowIndex = 0;
            fillTextBoxes();
            progressBar1.Value = 0;
            progressBar1.Visible = false;
            tbFindName.Visible = true;
            bNormalMode = true;
            rowCount = dgvClientList.RowCount;
        }

        public int NextRow()
        {
            if (dgvClientList.CurrentCell.RowIndex + 1 < dgvClientList.Rows.Count)
            {
                dgvClientList.CurrentCell = dgvClientList[0, dgvClientList.CurrentCell.RowIndex + 1];
                return Convert.ToInt32(dgvClientList.Rows[rowIndex].Cells["clmHHID"].Value);
            }
            return 0;
        }

        public void SetCurrentRow(int newRowNbr)
        {
            dgvClientList.Rows[newRowNbr].Selected = true;
            hhMemID = Convert.ToInt32(dgvClientList.SelectedRows[0].Cells["clmID"].Value);
            currentHHId = Convert.ToInt32(dgvClientList.SelectedRows[0].Cells["clmHHID"].Value);
            if (frmMainIn != null)
            frmMainIn.setHousehold(currentHHId, hhMemID);
        }

        //Sets the household dataRow with the new HouseholdID
        //And sets the index for maing form.
        private void SetIdAndClose()
        {
            if (dgvClientList.CurrentRow != null)
            {
                hhMemID = Convert.ToInt32(dgvClientList.CurrentRow.Cells["clmID"].Value.ToString());
                int newHHId = Convert.ToInt32(dgvClientList.CurrentRow.Cells["clmHHID"].Value.ToString());
                if (frmMainIn != null)
                frmMainIn.setHousehold(newHHId,hhMemID);

                indexFindClient = dgvClientList.CurrentRow.Index;
                currentHHId = newHHId;
                // frmMainIn.setIndex(dgvClientList.CurrentRow.Index);
                this.Visible = false;
            }
        }

        private void tbFindName_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                    { NextRow(); break; }
                case Keys.Up:
                    {
                        if (dgvClientList.CurrentCell.RowIndex - 1 >= 0)
                        {
                            dgvClientList.CurrentCell = dgvClientList[0, dgvClientList.CurrentCell.RowIndex - 1];
                        }
                        break;
                    }
                case Keys.Back:
                    {
                        tbFindName.AutoCompleteMode = AutoCompleteMode.None;
                        break;
                    }
                case Keys.Enter:
                    {
                        SetIdAndClose(); break;
                    }
            }
        }

        private void tbFindName_TextChanged(object sender, EventArgs e)
        {
            if (tbFindName.Text.Trim() == "")
            { dgvClientList.CurrentCell = dgvClientList[0, 0]; }
            else
            {
                switch (cboOrderBy.SelectedIndex)
                {
                    case 0:
                        { FindByName("colNameLF", "clmNameFL"); break; }
                    case 1:
                        { FindByName("colNameFL", "clmNameLF"); break; }
                    case 2:
                        { FindByName("clmAddress", "colNameLF"); break; }
                    case 3:
                        { FindByName("clmCity", "colNameLF"); break; }
                    case 4:
                        { FindByName("clmZip", "colNameLF"); break; }
                    case 5:
                        { FindByName("clmPhone", "colNameLF"); break; }
                    case 6:
                        { FindByName("clmHHID", "colNameLF"); break; }
                    case 7:
                        { FindByName("clmType", "colNameLF"); break; }
                    default:
                        { FindByName("clmMemberID", "colNameLF"); break; }
                }
            }
        }

        private void cmsFindClient_Opening(object sender, CancelEventArgs e)
        {
            if (CCFBGlobal.currentUser_PermissionLevel < CCFBGlobal.permissions_Admin)
                e.Cancel = true;
        }

        private void cmsFindClient_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "Export To Excel")
            {
                CCFBGlobal.ExportToExcell(dgvClientList, "ClientList_" + DateTime.Now.Year.ToString()
                    + "_" + CCFBGlobal.formatNumberWithLeadingZero(DateTime.Now.Month));
            }
        }

        private void bntAddNewClient_Click(object sender, EventArgs e)
        {
            AddNewClientOrHHMem frmAddNew = new AddNewClientOrHHMem(clsClient, false);
            frmAddNew.ShowDialog();

            if (frmAddNew.HHID > 0)
            {
                frmMainIn.setHousehold(frmAddNew.HHID, 0);
                if (CCFBPrefs.FindClientAutoRefresh == true)
                    loadList();
            }
            
            this.Close();
        }

        private void dgvClientList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (tbID.Text !="")
                    SetIdAndClose(); 
            }
        }

    }
}
