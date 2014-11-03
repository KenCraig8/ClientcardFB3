using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Windows.Forms;

namespace ClientcardFB3
{
    public partial class FindClientForm : Form
    {
        bool bNormalMode = false;
        bool refreshing = false;
        bool canceled = false;
        bool transferHHMemMode = false;
        Client clsClient = new Client();
        parm_ClientType clsParm_ClientType;
        int currentHHId = 0;
        string firstColName = "colNameLF";
        MainForm frmMainIn;

        //Used to tell if the list includes inactives or not
        string donotincludeInactivePhrase = " m.Inactive = 0 AND h.Inactive = 0 AND m.NotIncludedInClientList=0 ";
        string includeInactiveClause = "";
        int indexFindClient = 0;
        string lastSearchText = "";
        bool loadingName = false;
        List<string> rdoButtons = new List<string>();
        int rowIndex = 0;
        string sOrderBy;
        string filterByFldName = "";
        string filterByClause = "";
        string sWhereClause = "";
        string filterDateRange = "";
        string filterDateFldName = "";
        int hhMemID = 0;
        int rowCount = 0;

        public int HHMemID
        {
            get { return hhMemID; }
        }

        public int RowCount
        {
            get { return rowCount; }
        }
        public bool Canceled
        {
            get { return canceled; }
        }

        public bool TransferHHMemMode
        {
            get { return transferHHMemMode; }
            set { transferHHMemMode = value; }
        }

        public FindClientForm(MainForm FrmMainIn)
        {
            InitializeComponent();
            frmMainIn = FrmMainIn;
            clsParm_ClientType = new parm_ClientType(clsClient.connectionString);
            clsParm_ClientType.openAll();
            includeInactiveClause = donotincludeInactivePhrase;
            cboOrderBy.SelectedIndex = 0;
        }

        public FindClientForm()
        {
            InitializeComponent();
            clsParm_ClientType = new parm_ClientType(clsClient.connectionString);
            clsParm_ClientType.openAll();
            includeInactiveClause = donotincludeInactivePhrase;
            cboOrderBy.SelectedIndex = 0;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            canceled = true;
            this.Visible = false;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            refreshing = true;
            btnRefresh.Enabled = false;

            if (filterByFldName == "City" || filterByFldName == "Zipcode")
                getDistints(filterByFldName);

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
                    if (filterByFldName != "ClientType")
                    {
                        filterByClause = filterByFldName + "='" + cboFilter.SelectedItem.ToString() + "'";
                    }
                    else
                    {
                        filterByClause = filterByFldName + "='" + clsParm_ClientType.getParmInt(cboFilter.SelectedItem.ToString()) + "'";
                    }
                }
                else
                {
                    filterByClause = "";
                }
                if (refreshing == false)
                    loadList();
            }
        }

        private void cboOrderBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            string temp = sOrderBy;
            setOrderBy();
            refreshing = true;
            switch (cboOrderBy.SelectedIndex)
            {
                case 3:
                    {
                        cboFilter.Visible = true; getDistints("AptNbr"); break;
                    }
                case 4:
                    {
                        cboFilter.Visible = true; getDistints("City"); break;
                    }
                case 5:
                    {
                        cboFilter.Visible = true; getDistints("Zipcode"); break;
                    }
                case 8:
                    {
                        cboFilter.Visible = true; getDistints("ClientType"); break;
                    }
                default:
                    { break; }
            }
            refreshing = false;
            if (temp != sOrderBy)
            {
                loadList();
            }
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
                includeInactiveClause = " m.NotIncludedInClientList=0 ";
                lblInactiveHH.Visible = true;
                lblInactiveHHMem.Visible = true;
            }
            else
            {
                includeInactiveClause =  donotincludeInactivePhrase;
                lblInactiveHH.Visible = false;
                lblInactiveHHMem.Visible = false;
            }

            if (filterByFldName == "City" || filterByFldName == "Zipcode" || filterByFldName == "ClientType" || filterByFldName == "Apartment Number")
                getDistints(filterByFldName);

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
                displayClientInfo(e.RowIndex);
            }
        }

        private void displayClientInfo(int newRow)
        {
            loadingName = true;
            rowIndex = newRow;
            fillTextBoxes();
            loadingName = false;
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
                if (dgvRow.Cells["colNameLF"].Value.ToString() == dgvRow.Cells["colHHName"].Value.ToString())
                {
                    tbName.Text = dgvRow.Cells["colHHName"].Value.ToString();
                }
                else
                {
                    tbName.Text = dgvRow.Cells["colNameLF"].Value.ToString() + "  [" + dgvRow.Cells["colHHName"].Value.ToString() + "]";
                }
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
                    if (tbFindName.Text.CompareTo(lastSearchText) >= 0 && lastSearchText != "")
                        rowStart = rowIndex;
                    else
                        rowStart = 0;
                    lastSearchText = tbFindName.Text.ToUpper().Trim();
                    for (int i = rowStart; i < dgvClientList.Rows.Count; i++)
                    {
                        if (dgvClientList.Rows[i].Cells[colNameFull].FormattedValue.ToString().ToUpper().StartsWith(lastSearchText) == true)
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
                bNormalMode = false;
                canceled = false;
                tbFindName.Text = "";
                lastSearchText = "";
                dgvClientList.FirstDisplayedScrollingRowIndex = indexFindClient;
                SetCurrentRow(indexFindClient, false);
                displayClientInfo(indexFindClient);
                bNormalMode = true;
                tbFindName.Focus();
                this.ActiveControl = tbFindName;
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
            filterByFldName = colName;
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
                SetCurrentRow(indexFindClient, true);
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
            SetCurrentRow(indexFindClient, true);
        }

        private void AppendToWhereClause(string testclause)
        {
            if (testclause != "")
            {
                if (sWhereClause == "")
                {
                    sWhereClause = testclause;
                }
                else
                {
                    sWhereClause += " AND " + testclause;
                }
            }
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
            sWhereClause = "";
            setFilterDateRange();
            AppendToWhereClause(filterByClause);
            AppendToWhereClause(includeInactiveClause);
            AppendToWhereClause(filterDateRange);
            if (chkHeahHouseOnly.Checked == true)
            {
                AppendToWhereClause("m.HeadHH = 1");
            }

            clsClient.openWhere(sWhereClause + sOrderBy);
            progressBar1.Maximum = clsClient.RowCount;

            for (int i = 0; i < clsClient.RowCount; i++)
            {
                DataRow drow = clsClient.DSet.Tables[0].Rows[i];
                dgvClientList.Rows.Add();
                dgvClientList.Rows[i].Tag = drow["HHId"].ToString();
                if ((bool)drow["HHInactive"] == true)
                    dgvClientList.Rows[i].DefaultCellStyle.ForeColor = Color.Maroon;
                else if (Convert.ToBoolean(CCFBGlobal.NullToZero(drow["Inactive"])) == true)
                    dgvClientList.Rows[i].DefaultCellStyle.ForeColor = Color.DarkViolet;
                else
                    dgvClientList.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                if (drow[firstColName] == DBNull.Value)
                    dgvClientList.Rows[i].Cells["colName"].Value = drow["Name"].ToString();
                else
                    dgvClientList.Rows[i].Cells["colName"].Value = drow[firstColName].ToString();
                dgvClientList.Rows[i].Cells["clmCity"].Value = drow["City"].ToString();
                if (drow["ID"].ToString() == "")
                    dgvClientList.Rows[i].Cells["clmID"].Value = "0";
                else
                    dgvClientList.Rows[i].Cells["clmID"].Value = drow["ID"].ToString();
                dgvClientList.Rows[i].Cells["clmZip"].Value = drow["Zipcode"].ToString();
                dgvClientList.Rows[i].Cells["clmHHID"].Value = drow["HHId"].ToString();
                dgvClientList.Rows[i].Cells["clmPhone"].Value = drow["Phone"].ToString();
                dgvClientList.Rows[i].Cells["clmAddress"].Value = drow["Address"].ToString();
                dgvClientList.Rows[i].Cells["clmAptNbr"].Value = drow["AptNbr"].ToString();
                dgvClientList.Rows[i].Cells["clmHeadHH"].Value = CCFBGlobal.NullToBlank(drow["HeadHH"].ToString());
                dgvClientList.Rows[i].Cells["clmClientType"].Value = CCFBGlobal.LongNameFromId(CCFBGlobal.parmTbl_Client, Convert.ToInt32(drow["ClientType"]));
                dgvClientList.Rows[i].Cells["colHHName"].Value = drow["Name"].ToString();
                dgvClientList.Rows[i].Cells["colNameLF"].Value = drow["colNameLF"].ToString().ToUpper().Trim();
                dgvClientList.Rows[i].Cells["colNameFL"].Value = drow["colNameFL"].ToString().ToUpper().Trim();
                if (drow["LatestService"].ToString() != "")
                {
                    dgvClientList.Rows[i].Cells["clmLatestService"].Value =
                        CCFBGlobal.formatDateYMD(clsClient.DSet.Tables[0].Rows[i].Field<DateTime>("LatestService"));
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

        public void SetCurrentRow(int newRowNbr, bool loadMainHH)
        {
            if (dgvClientList.SelectedRows.Count > 0)
            {
                dgvClientList.SelectedRows[0].Selected = false;
            }
            dgvClientList.Rows[newRowNbr].Selected = true;
            hhMemID = Convert.ToInt32(dgvClientList.SelectedRows[0].Cells["clmID"].Value);
            currentHHId = Convert.ToInt32(dgvClientList.SelectedRows[0].Cells["clmHHID"].Value);
            if (frmMainIn != null && loadMainHH == true)
            {
                frmMainIn.setHousehold(currentHHId, hhMemID);
            }
        }

        //Sets the household dataRow with the new HouseholdID
        //And sets the index for maing form.
        public void SetIdAndClose()
        {
            if (dgvClientList.CurrentRow != null)
            {
                hhMemID = Convert.ToInt32(dgvClientList.CurrentRow.Cells["clmID"].Value.ToString());
                int newHHId = Convert.ToInt32(dgvClientList.CurrentRow.Cells["clmHHID"].Value.ToString());
                if (frmMainIn != null)
                {
                    if (transferHHMemMode == false)
                    {
                        frmMainIn.setHousehold(newHHId, hhMemID);
                        indexFindClient = dgvClientList.CurrentRow.Index;
                        currentHHId = newHHId;
                        this.Visible = false;
                    }
                    else
                    {
                        if (MessageBox.Show("Are You Sure You Want To Transfer The Household Member To Household "
                            + newHHId.ToString() + " With Member Name " + dgvClientList.CurrentRow.Cells["colName"].Value.ToString()
                            + "?", "Transfer Member?", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                             == System.Windows.Forms.DialogResult.Yes)
                        {
                            frmMainIn.transferMember();
                            indexFindClient = dgvClientList.CurrentRow.Index;
                            currentHHId = newHHId;
                            this.Visible = false;
                        }
                    }
                }
                indexFindClient = dgvClientList.CurrentRow.Index;
                currentHHId = newHHId;
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
            if (bNormalMode == true)
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
                            { FindByName("clmAptNbr", "colNameLF"); break; }
                        case 4:
                            { FindByName("clmCity", "colNameLF"); break; }
                        case 5:
                            { FindByName("clmZip", "colNameLF"); break; }
                        case 6:
                            { FindByName("clmPhone", "colNameLF"); break; }
                        case 7:
                            { FindByName("clmHHID", "colNameLF"); break; }
                        case 8:
                            { FindByName("clmType", "colNameLF"); break; }
                        case 9:
                            { FindByName("clmLatestService", "colNameLF"); break; }
                        default:
                            { FindByName("clmMemberID", "colNameLF"); break; }
                    }
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
                CCFBGlobal.ExportToExcel(dgvClientList, "ClientList_" + DateTime.Now.Year.ToString()
                    + "_" + CCFBGlobal.formatNumberWithLeadingZero(DateTime.Now.Month));
            }
        }

        private void bntAddNewClient_Click(object sender, EventArgs e)
        {
            AddNewHousehold2 frmAddNew = new AddNewHousehold2(clsClient);
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
                if (tbID.Text != "")
                    SetIdAndClose();
            }
        }

        private void rdoAscending_CheckedChanged(object sender, EventArgs e)
        {
            setOrderBy();
        }

        private void tbID_KeyPress(object sender, KeyPressEventArgs e)
        {
            int newHhID = 0;
            if (tbID.Focused == true)
            {
                if (e.KeyChar == (char)Keys.Return)
                {
                    try
                    {
                        newHhID = Convert.ToInt32(tbID.Text);
                        frmMainIn.setHousehold(newHhID, 0);
                        currentHHId = newHhID;
                        this.Visible = false;
                    }
                    catch (Exception)
                    {


                    }
                }
            }
        }

        private void FindClientForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (CCFBPrefs.EnableBarcodePrompts == true && e.KeyCode == Keys.F2)
            {
                ShowBarcodePrompt();
                e.SuppressKeyPress = true;
            }

        }

        private void ShowBarcodePrompt()
        {
            int iHHM = 0;
            BarCodeEntryForm frmBarCodeEntry = new BarCodeEntryForm();
            frmBarCodeEntry.INIT("Barcode Reader Form", "Enter barcode for household", true);
            DialogResult dr = frmBarCodeEntry.ShowDialog(this);
            if (dr == DialogResult.OK)
            {
                ClearHeader();
                string testBarCode = frmBarCodeEntry.BarCode;
                int testID = -1;
                if (CCFBPrefs.BarcodeUseFamilyMember == true)
                {
                    testID = CCFBGlobal.getHHFromBarCode(testBarCode, ref iHHM);
                }
                else
                {
                    testID = CCFBGlobal.getClientFromBarCode(testBarCode);
                }
                if (testID > 0)
                {
                    currentHHId = testID;
                    int newrow = getRowNbr(testID);
                    if (newrow > 0)
                    {
                        indexFindClient = newrow;
                        SetCurrentRow(indexFindClient, true);
                        dgvClientList.CurrentCell = dgvClientList[0, indexFindClient];
                        dgvClientList.FirstDisplayedScrollingRowIndex = indexFindClient;
                    }
                    else
                    {
                        frmMainIn.setHousehold(currentHHId, iHHM);
                    }
                    tbID.Text = currentHHId.ToString();
                    tbName.Text = frmMainIn.HHName();
                    tbAddress.Text = frmMainIn.HHAddress();
                    if (frmBarCodeEntry.GiveService() == true && frmMainIn.get_tsbNewServiceEnabled() == true)
                    {
                        frmMainIn.CreateNewFoodService();
                    }
                    else
                    {
                        this.Visible = false;
                    }
                }
                else
                {
                    CCFBGlobal.playQBeep();
                }
            }
        }

        private void setOrderBy()
        {
            cboFilter.Visible = false;
            cboFilter.Text = "";
            lblFilterBy.Visible = false;
            string temp = " ORDER BY ";
            string sortOrder = "";
            if (rdoDescending.Checked == true)
            {
                sortOrder = " DESC";
            }
            sWhereClause = " ";
            switch (cboOrderBy.SelectedIndex)
            {
                case 0:
                    { temp += "colNameLF" + sortOrder; firstColName = "colNameLF"; break; }
                case 1:
                    { temp += "colNameFL" + sortOrder; firstColName = "colNameFL"; break; }
                case 2:
                    { temp += "Address" + sortOrder + ", AptNbr, colNameLF"; firstColName = "colNameLF"; break; }
                case 3:
                    {
                        temp += "AptNbr" + sortOrder + ", colNameLF"; firstColName = "colNameLF";
                        cboFilter.Visible = true; break;
                    }
                case 4:
                    {
                        temp += "City" + sortOrder + ", colNameLF"; firstColName = "colNameLF";
                        cboFilter.Visible = true; break;
                    }
                case 5:
                    {
                        temp += "ZipCode" + sortOrder + ", colNameLF"; firstColName = "colNameLF";
                        cboFilter.Visible = true; break;
                    }
                case 6:
                    { temp += "Phone" + sortOrder + ", colNameLF"; firstColName = "colNameLF"; break; }
                case 7:
                    { temp += "h.Id" + sortOrder + ", colNameLF"; firstColName = "colNameLF"; break; }
                case 8:
                    {
                        temp += "ClientTypeName" + sortOrder + ", colNameLF"; sOrderBy = temp; firstColName = "colNameLF";
                        cboFilter.Visible = true; break;
                    }
                case 9:
                    { temp += "LatestService" + sortOrder + ", colNameLF"; firstColName = "colNameLF"; break; }
                default:
                    { temp += "m.Id" + sortOrder + ", colNameLF"; firstColName = "colNameLF"; break; }
            }
            lblFilterBy.Visible = cboFilter.Visible;
            sOrderBy = temp;
        }

        private void tbID_KeyDown(object sender, KeyEventArgs e)
        {
            CCFBGlobal.checkForIntOnKeyPress(e);
        }

        public int getRowNbr(int hhid)
        {
            int newrow = 0;
            for (int i = 0; i < dgvClientList.RowCount; i++)
            {
                if (Convert.ToInt32(dgvClientList.Rows[i].Tag) == hhid)
                {
                    newrow = i;
                    break;
                }
            }
            return newrow;
        }

        private void cboDateRangeField_SelectionChangeCommitted(object sender, EventArgs e)
        {
            bool makeVisible = (cboDateRangeField.SelectedIndex > 0);
            lblFirst.Visible = makeVisible;
            lblLast.Visible = makeVisible;
            dtpFirst.Visible = makeVisible;
            dtpLast.Visible = makeVisible;
            
        }

        private void setFilterDateFld()
        {
            filterDateFldName = "";
            if (cboDateRangeField.SelectedIndex > 0)
            {
                switch (cboDateRangeField.SelectedIndex)
                {
                    case 1: filterDateFldName = "LatestService "; break;
                    case 2: filterDateFldName = "FirstSvcThisYear"; break;
                    case 3: filterDateFldName = "FirstCalService"; break;
                    case 4: filterDateFldName = "LastCommodityService"; break;
                    case 5: filterDateFldName = "LastSupplService"; break;
                    case 6: filterDateFldName = "SchSupplyRegDate"; break;
                    case 7: filterDateFldName = "DateIdVerified"; break;
                    case 8: filterDateFldName = "IncomeVerifiedDate"; break;
                    case 9: filterDateFldName = "TEFAPSignDate"; break;
                    case 10: filterDateFldName = "Created"; break;
                    case 11: filterDateFldName = "Modified"; break;
                    default:
                        break;
                }
            }
        }

        private void setFilterDateRange()
        {
            setFilterDateFld();
            if (filterDateFldName == "")
            {
                filterDateRange = "";
            }
            else
            {
                if (dtpFirst.Value == dtpLast.Value)
                {
                    filterDateRange = filterDateFldName + "= '" + dtpFirst.Value.ToShortDateString() + "'";
                }
                else
                {
                    filterDateRange = filterDateFldName
                        + " BETWEEN '" + dtpFirst.Value.ToShortDateString() + "'"
                        + " AND '" + dtpLast.Value.ToShortDateString() + "' ";
                }
            }
        }
    }
}
