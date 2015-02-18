using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ClientcardFB3
{
    public partial class EditChristmasGifts : Form
    {
        Client clsClient;
        DataTable dtCAOrgs = new DataTable("CAOrganization");
        List<parmType> lstFamilyMbr = new List<parmType>();
        List<parmType> lstAdoptedBy = new List<parmType>();
        List<parmType> lstGiftsAdoptedBy = new List<parmType>();
        List<TextBox> lstTBH = new List<TextBox>();
        List<TextBox> lstTBHM = new List<TextBox>();
        List<TextBox> lstTBCA = new List<TextBox>();
        List<CheckBox> lstCHK = new List<CheckBox>();
        List<DateTimePicker> lstDTP = new List<DateTimePicker>();

        string txtOriginal = "";

        private enum dgvCellTypeEnum
        { 
            IsText = 0,
            IsBoolean = 1,
            IsDate = 2,
            IsCombo = 3
        }
        public EditChristmasGifts()
        {
            InitializeComponent();
            initcboCAAdoptedBy();
            //Loads the Collections of Textbox's, ComboBox's and CheckBox's.
            traverseAndAddControlsToCollections(this.Controls);
        }

        public void initClient(Client newClient)
        {
            clsClient = newClient;
            int tmpPtr = 0;
            lstFamilyMbr.Clear();
            lstFamilyMbr.Add(new parmType(0, "No Selection", 0, "NO"));
            for (int i = 0; i < clsClient.clsHHmem.RowCount; i++)
            {
                clsClient.clsHHmem.SetRecord(i);
                if (clsClient.clsHHmem.Age >= 15)
                {
                    lstFamilyMbr.Add(new parmType(clsClient.clsHHmem.ID, clsClient.clsHHmem.FirstName.Trim() + " " + clsClient.clsHHmem.LastName.Trim(), i, clsClient.clsHHmem.FirstName));
                }
            }
            if (lstFamilyMbr.Count > 0)
            {
                cboSignedBy.DataSource = lstFamilyMbr;
                cboSignedBy.DisplayMember = "LongName";
                cboSignedBy.ValueMember = "LongName";
                cboSignedBy.SelectedIndex = tmpPtr;
            }
            else
            {
                cboSignedBy.DataSource = null;
                cboSignedBy.Items.Clear();
                cboSignedBy.Items.Add(clsClient.clsHH.Name);
                cboSignedBy.SelectedIndex = 0;
            }
            foreach (TextBox item in lstTBH)
            {
                if (item.Tag != null)
                {
                    if (item.Tag.ToString() != "")
                    {
                        item.Text = clsClient.clsHH.GetDataString(item.Tag.ToString());
                    }
                }
            }
            foreach (TextBox item in lstTBHM)
            {
                if (item.Tag != null)
                {
                    if (item.Tag.ToString() != "")
                    {
                        item.Text = clsClient.clsHHmem.GetDataString(item.Tag.ToString());
                    }
                }
            }
            foreach (TextBox item in lstTBCA)
            {
                if (item.Tag != null)
                {
                    if (item.Tag.ToString() != "")
                    {
                        item.Text = clsClient.clsHH.GetDataString(item.Tag.ToString());
                    }
                }
            }
            foreach (CheckBox item in lstCHK)
            {
                if (item.Tag != null)
                {
                    if (item.Tag.ToString() != "")
                    {
                        item.Checked = (bool)clsClient.clsHH.GetDataValue(item.Tag.ToString());
                    }
                }
            }
            foreach (DateTimePicker item in lstDTP)
            {
                if (item.Tag != null)
                {
                    if (item.Tag.ToString() != "")
                    {
                        item.Text = clsClient.clsHH.GetDataString(item.Tag.ToString());
                    }
                }
            }
            for (int i = 0; i < cboSignedBy.Items.Count; i++)
            {
                parmType pt = (parmType)cboSignedBy.Items[i];
                if (pt.ID == clsClient.clsHH.CASignedByID)
                {
                    cboSignedBy.SelectedIndex = i;
                    break;
                }
            }
            for (int i = 0; i < cboCAAdoptedBy.Items.Count; i++)
            {
                parmType pt = (parmType)cboCAAdoptedBy.Items[i];
                if (pt.ID == clsClient.clsHH.CAAdoptedBy)
                {
                    cboCAAdoptedBy.SelectedIndex = i;
                    break;
                }
            }
            loaddgvHHMembers(true);
            grpFood.Enabled = chkCAFoodBoxRequested.Checked;
            grpGifts.Enabled = !chkCAFoodBoxOnly.Checked;
        }

        private void initcboCAAdoptedBy()
        {
            SqlConnection sqlConn = new SqlConnection(CCFBGlobal.connectionString);
            SqlCommand sqlCmd = new System.Data.SqlClient.SqlCommand("SELECT * FROM CAOrganizations ORDER BY UID",sqlConn);
            SqlDataAdapter sqlAdpt = new SqlDataAdapter(sqlCmd);
            int nbrRows = sqlAdpt.Fill(dtCAOrgs);
            DataGridViewComboBoxColumn colCbo = (DataGridViewComboBoxColumn)dgvHHMembers.Columns["clmCAAdoptedBy"];

            cboCAAdoptedBy.Items.Clear();
            lstAdoptedBy.Clear();
            if (nbrRows > 0)
            {
                for (int i = 0; i < dtCAOrgs.Rows.Count; i++)
                {
                    lstAdoptedBy.Add(new parmType(Convert.ToInt32(dtCAOrgs.Rows[i]["UID"]), dtCAOrgs.Rows[i]["OrgName"].ToString(), i, dtCAOrgs.Rows[i]["OrgContactPerson"].ToString()));
                    lstGiftsAdoptedBy.Add(new parmType(Convert.ToInt32(dtCAOrgs.Rows[i]["UID"]), dtCAOrgs.Rows[i]["OrgName"].ToString(), i, dtCAOrgs.Rows[i]["OrgContactPerson"].ToString()));
                }
            }
            if (lstAdoptedBy.Count > 0)
            {
                cboCAAdoptedBy.DataSource = lstAdoptedBy;
                cboCAAdoptedBy.DisplayMember = "LongName";
                cboCAAdoptedBy.ValueMember = "LongName";
                cboCAAdoptedBy.SelectedIndex = 0;
            }
            else
            {
                cboCAAdoptedBy.DataSource = null;
                cboCAAdoptedBy.Items.Clear();
                cboCAAdoptedBy.Items.Add("None Listed");
                cboCAAdoptedBy.SelectedIndex = 0;
            }
            cboGiftsAdoptedBy.Items.Clear();
            if (lstGiftsAdoptedBy.Count > 0)
            {
                cboGiftsAdoptedBy.DataSource = lstGiftsAdoptedBy;
                cboGiftsAdoptedBy.DisplayMember = "LongName";
                cboGiftsAdoptedBy.ValueMember = "LongName";
                cboGiftsAdoptedBy.SelectedIndex = 0;
                colCbo.DataSource = lstGiftsAdoptedBy;
                colCbo.DisplayMember = "LongName";
                colCbo.ValueMember = "UID";
            }
            else
            {
                cboGiftsAdoptedBy.DataSource = null;
                cboGiftsAdoptedBy.Items.Clear();
                cboGiftsAdoptedBy.Items.Add("None Listed");
                cboGiftsAdoptedBy.SelectedIndex = 0;
                colCbo.DataSource = null;
                colCbo.Items.Add("None Listed");
            }
        }

        /// <summary>
        /// Loads the Children DataGrid with the School Age Members
        /// </summary>
        private void loaddgvHHMembers(bool clearRows)
        {
            Color myCellForeColor;
            DataGridViewRow dvr;
            if (clearRows == true)
            {
                dgvHHMembers.Rows.Clear();
            }

            int rowCount = 0;
            int calculatedAge = 0;
            int curMonth = DateTime.Today.Month;
            for (int i = 0; i < clsClient.clsHHmem.RowCount; i++)
            {
                clsClient.clsHHmem.SetRecord(i);
                if (clsClient.clsHHmem.Inactive == false)
                {
                    calculatedAge = CCFBGlobal.calcAge(clsClient.clsHHmem.Birthdate, DateTime.Today);
                    if (calculatedAge != clsClient.clsHHmem.Age )
                    {
                        clsClient.clsHHmem.Age = calculatedAge;
                    }
                    if (clsClient.clsHHmem.CAGiftFlag == true | clsClient.clsHHmem.Age <= 18)
                    {
                        dgvHHMembers.Rows.Add();
                        if ((bool)clsClient.clsHHmem.DSet.Tables[0].Rows[i]["CAGiftFlag"])
                            { myCellForeColor = Color.Maroon; }
                        else
                            { myCellForeColor = Color.Black; }
                        dvr = dgvHHMembers.Rows[rowCount];
                        dvr.Tag = clsClient.clsHHmem.ID.ToString();
                        FillGridMembersCell(dvr, "clmCAGiftFlag", "CAGiftFlag", dgvCellTypeEnum.IsBoolean, myCellForeColor, i, false);
                        dvr.Cells["clmName"].Value = clsClient.clsHHmem.FirstName + " " + clsClient.clsHHmem.LastName;
                        //FillGridMembersCell(dvr, "clmName", "Name", dgvCellTypeEnum.IsText, myCellForeColor, i, true);
                        FillGridMembersCell(dvr, "clmAge", "Age", dgvCellTypeEnum.IsText, myCellForeColor, i, true);
                        FillGridMembersCell(dvr, "clmGender", "Sex", dgvCellTypeEnum.IsText, myCellForeColor, i, true);
                        FillGridMembersCell(dvr, "clmCASize", "CASize", dgvCellTypeEnum.IsText, myCellForeColor, i, false);
                        FillGridMembersCell(dvr, "clmCAGiftIdeas", "CAGiftIdeas", dgvCellTypeEnum.IsText, myCellForeColor, i, false);
                        FillGridMembersCell(dvr, "clmCAHobbies", "CAHobbies", dgvCellTypeEnum.IsText, myCellForeColor, i, false);
                        FillGridMembersCell(dvr, "clmCAAdoptedBy", "CAAdoptedBy", dgvCellTypeEnum.IsCombo, myCellForeColor, i, false);
                        FillGridMembersCell(dvr, "clmCAAdoptedName", "CAAdoptedName", dgvCellTypeEnum.IsText, myCellForeColor, i, false);
                        FillGridMembersCell(dvr, "clmCAAdoptedDate", "CAAdoptedDate", dgvCellTypeEnum.IsDate, myCellForeColor, i, false);
                        FillGridMembersCell(dvr, "clmCAGiftReceived", "CAGiftReceived", dgvCellTypeEnum.IsBoolean, myCellForeColor, i, false);
                        FillGridMembersCell(dvr, "clmCAGiftReceivedDate", "CAGiftReceivedDate", dgvCellTypeEnum.IsDate, myCellForeColor, i, false);
                        FillGridMembersCell(dvr, "clmHMID", "ID", dgvCellTypeEnum.IsText, myCellForeColor, i, true);
                        rowCount++;
                    }
                }
            }
        }

        /// <summary>
        /// Fills the given cell with the proper value and sets the color scheme
        /// </summary>
        /// <param name="dgvRow">The DataGridView row that the cell exists in</param>
        /// <param name="ColName">The Column Name of the cell to fill</param>
        /// <param name="FieldName">The Field Name of the value to retrive from  the given row of the database</param>
        /// <param name="IsBoolean">If the value seeked is a bool value or not</param>
        /// <param name="cellForeColor">The Text Color Wanted</param>
        /// <param name="dsetRowIndex">The Row that you need from the database</param>
        private void FillGridMembersCell(DataGridViewRow dgvRow, String ColName, String FieldName, dgvCellTypeEnum fldCellType, Color cellForeColor, int dsetRowIndex, Boolean cellIsReadOnly)
        {
            DataGridViewCell dgvCell = dgvRow.Cells[ColName];
            switch (fldCellType)
            {
                case dgvCellTypeEnum.IsBoolean:
                    if (dgvCell.EditType != null)
                    {
                        if ((bool)clsClient.clsHHmem.DSet.Tables[0].Rows[dsetRowIndex][FieldName] == true)
                            dgvCell.Value = "Y";
                        else
                            dgvCell.Value = "";
                    }
                    else
                        dgvCell.Value = (bool)clsClient.clsHHmem.DSet.Tables[0].Rows[dsetRowIndex][FieldName];
                    break;
                case dgvCellTypeEnum.IsDate:
                    string tmp = "";
                    if (clsClient.clsHHmem.DSet.Tables[0].Rows[dsetRowIndex][FieldName].ToString() != "")
                    {
                        DateTime d = (DateTime)clsClient.clsHHmem.DSet.Tables[0].Rows[dsetRowIndex][FieldName];
                        if (d > CCFBGlobal.FBNullDateValue)
                        {
                            tmp = d.ToShortDateString();
                        }
                    }
                    dgvCell.Value = tmp;
                    break;
                case dgvCellTypeEnum.IsCombo:
                    dgvCell.Value = clsClient.clsHHmem.DSet.Tables[0].Rows[dsetRowIndex][FieldName].ToString();
                    break;
                default:
                    dgvCell.Value = clsClient.clsHHmem.DSet.Tables[0].Rows[dsetRowIndex][FieldName];
                    break;
            }
            dgvCell.Tag = FieldName;

            if (dgvCell.Value == null)
                dgvCell.Value = "";

            dgvCell.Style.ForeColor = cellForeColor;
            dgvCell.ReadOnly = cellIsReadOnly;
        }

        #region formEvents

        private void btnSave_Click(object sender, EventArgs e)
        {
            clsClient.clsHH.update(true);
            clsClient.clsHHmem.update(true);
        }

        private void btnPrintTicket_Click(object sender, EventArgs e)
        {
            SchSupplyPickupTicket clsPrintTicket = new SchSupplyPickupTicket();
            clsPrintTicket.createReport(clsClient);
        }

        private void cboPickupPerson_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cboSignedBy.Focused == true)
            {
                //clsClient.clsHH.SetDataValue("SchSupplyPickupPerson", ((parmType)cboPickupPerson.SelectedItem).LongName);
            }
        }

        private void dgvHHMembers_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            clsClient.clsHHmem.find(Convert.ToInt32(dgvHHMembers.Rows[e.RowIndex].Tag));
            switch (e.ColumnIndex)
            {
                case 0:
                    clsClient.clsHHmem.CAGiftFlag = Convert.ToBoolean(dgvHHMembers[e.ColumnIndex, e.RowIndex].Value);
                    break;
                case 4:
                    clsClient.clsHHmem.CASize = dgvHHMembers[e.ColumnIndex, e.RowIndex].Value.ToString();
                    break;
                case 5:
                    clsClient.clsHHmem.CAGiftIdeas = dgvHHMembers[e.ColumnIndex, e.RowIndex].Value.ToString();
                    break;
                case 6:
                    clsClient.clsHHmem.CAHobbies = dgvHHMembers[e.ColumnIndex, e.RowIndex].Value.ToString();
                    break;
                case 7:
                    string testText = dgvHHMembers[e.ColumnIndex, e.RowIndex].FormattedValue.ToString();
                    foreach (DataRow item in dtCAOrgs.Rows)
                    {
                        if (item["OrgName"].ToString() == testText)
                        {
                            clsClient.clsHHmem.CAAdoptedBy = Convert.ToInt32(item["UID"]);
                            break;
                        }
                    }
                    break;
                case 8:
                    clsClient.clsHHmem.CAAdoptedName = dgvHHMembers[e.ColumnIndex, e.RowIndex].Value.ToString();
                    break;
                case 9:
                    clsClient.clsHHmem.CAAdoptedDate = CCFBGlobal.ValidDate(dgvHHMembers[e.ColumnIndex, e.RowIndex].Value);
                    break;
                case 10:
                    clsClient.clsHHmem.CAGiftReceived = Convert.ToBoolean(dgvHHMembers[e.ColumnIndex, e.RowIndex].Value);
                    break;
                case 11:
                    clsClient.clsHHmem.CAGiftReceivedDate = CCFBGlobal.ValidDate( dgvHHMembers[e.ColumnIndex, e.RowIndex].Value);
                    break;
                default:
                    break;
            }
        }

        private void dgvHHMembers_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
        }
        #endregion

        private void chkCAFlag_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCAFlag.Focused == true)
            {
                clsClient.clsHH.CAFlag = chkCAFlag.Checked;
                if (chkCAFlag.Checked == true)
                {
                    clsClient.clsHH.CADBInputDate = DateTime.Now;
                }
            }
            btnPrintTicket.Enabled = chkCAFlag.Checked;
        }

        private void chkCAFoodBoxRequested_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCAFoodBoxRequested.Focused == true)
            {
                clsClient.clsHH.CAFoodBoxRequest = chkCAFoodBoxRequested.Checked;
                grpFood.Enabled = chkCAFoodBoxRequested.Checked;
            }
        }

        private void chkCAFoodBoxOnly_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCAFoodBoxOnly.Focused == true)
            {
                clsClient.clsHH.CAFoodBoxOnly = chkCAFoodBoxOnly.Checked;
                if (chkCAFoodBoxRequested.Checked == false && chkCAFoodBoxOnly.Checked == true)
                {
                    chkCAFoodBoxRequested.Checked = true;
                    clsClient.clsHH.CAFoodBoxRequest = true;
                }
                grpGifts.Enabled = !chkCAFoodBoxOnly.Checked;
            }
        }

        private void chkCAHasPickupInfo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCAHasPickupInfo.Focused == true)
            {
                clsClient.clsHH.CAHasPickupInfo = chkCAHasPickupInfo.Checked;
            }
        }

        private void chkCAReceived_CheckedChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Traverses all controls on the page using recursion and adds the proper ones
        /// to their proper collections and adds LostFocus event to Textboxes and Checkboxes
        /// </summary>
        /// <param name="controlList"></param>
        private void traverseAndAddControlsToCollections(Control.ControlCollection controlList)
        {
            foreach (Control cntrl in controlList.OfType<Control>())
            {
                switch (cntrl.GetType().Name)
                {
                    case "TextBox":
                        {
                            if (cntrl.Tag != null && cntrl.Tag.ToString() != "")
                            {
                                if (cntrl.Name.Substring(0, 3) == "tbh")
                                {
                                    cntrl.Enter += new System.EventHandler(this.tb_Enter);
                                    cntrl.Leave += new System.EventHandler(this.tbH_Leave); 
                                    lstTBH.Add((TextBox)cntrl);
                                }
                                else if (cntrl.Name.Substring(0, 4) == "tbfm")
                                {
                                    cntrl.Enter += new System.EventHandler(this.tb_Enter);
                                    cntrl.Leave += new System.EventHandler(this.tbHM_Leave);
                                    lstTBHM.Add((TextBox)cntrl);
                                }

                                else if (cntrl.Name.Substring(0, 4) == "tbCA")
                                {
                                        cntrl.Enter += new System.EventHandler(this.tbCA_Enter);
                                        cntrl.Leave += new System.EventHandler(this.tbCA_Leave);
                                        lstTBCA.Add((TextBox)cntrl);
                                }
                            }
                            break;
                        }
                    case "CheckBox":
                        {
                            CheckBox chk = (CheckBox)cntrl;
                            lstCHK.Add(chk);
                            break;
                        }
                    case "DateTimePicker":
                        {
                            if (cntrl.Tag != null && cntrl.Tag.ToString().Trim() != "")
                                {
                                    lstDTP.Add((DateTimePicker)cntrl);
                                }
                            break;
                        }
                }

                traverseAndAddControlsToCollections(cntrl.Controls);
            }
        }

        private void tb_Enter(object sender, EventArgs e)
        {
            txtOriginal = ((TextBox)sender).Text;
        }

        private void tbCA_Enter(object sender, EventArgs e)
        {
            txtOriginal = ((TextBox)sender).Text;
        }

        private void tbCA_Leave(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.ReadOnly == false)
            {
                if (tb.Text != txtOriginal)
                {
                    clsClient.clsHH.SetDataValue(tb.Tag.ToString(), tb.Text);
                }
            }
        }

        private void tbH_Leave(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.ReadOnly == false)
            {
                if (tb.Text != txtOriginal)
                {
                    clsClient.clsHH.SetDataValue(tb.Tag.ToString(), tb.Text);
                }
            }
        }

        private void tbHM_Leave(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.ReadOnly == false)
            {
                if (tb.Text != txtOriginal)
                {
                    clsClient.clsHHmem.SetDataValue(tb.Tag.ToString(), tb.Text);
                }
            }
        }

        private void dtpCA_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker dtp = (DateTimePicker)sender;
            if (dtp.Focused == true)
            {
                string fldName = dtp.Tag.ToString();
                if (dtp.Value != Convert.ToDateTime(clsClient.clsHH.GetDataValue(fldName)))
                {
                    clsClient.clsHH.SetDataValue(fldName, dtp.Value.ToShortDateString());
                }
            }
        }

        private void dtpDBInputDate_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker dtp = (DateTimePicker)sender;
            if (dtp.Focused == true)
            {
                clsClient.clsHH.CADBInputDate = dtp.Value;
            }
        }

        private void cboSignedBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cbo = (ComboBox)sender;
            if (cbo.Focused == true)
            {
                parmType pt = (parmType)cboSignedBy.SelectedItem;
                clsClient.clsHH.CASignedByID = pt.ID;
            }
        }

        private void cboCAAdoptedBy_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox cbo = (ComboBox)sender;
            if (cbo.Focused == true)
            {
                parmType pt = (parmType)cboCAAdoptedBy.SelectedItem;
                clsClient.clsHH.CAAdoptedBy = pt.ID;
                foreach (DataRow item in dtCAOrgs.Rows)
	            {
                    if (Convert.ToInt32(item["UID"].ToString()) == pt.ID)
		            {
                        clsClient.clsHH.CAAdoptedContactName = item["OrgContactPerson"].ToString();
                        clsClient.clsHH.CAAdoptedContactPhone = item["OrgContactPhone"].ToString();
                        tbCAAdoptedContactName.Text = clsClient.clsHH.CAAdoptedContactName;
                        tbCAAdoptedContactPhone.Text = clsClient.clsHH.CAAdoptedContactPhone;
                        break;
                    }
                }
                if (chkCAFoodBoxOnly.Checked == true)
                {
                    clsClient.clsHH.CAAdoptedDate = dtpDefaultAdoptedDate.Value; ;
                    tbCAAdoptedDate.Text = dtpDefaultAdoptedDate.Value.ToShortDateString();
                    dtpCAAdoptedDate.Value = dtpDefaultAdoptedDate.Value;
                }
            }
        }

        private void cboGiftsAdoptedBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cbo = (ComboBox)sender;
            if (cbo.Focused == true)
            {
                parmType pt = (parmType)cboGiftsAdoptedBy.SelectedItem;
                foreach (DataRow item in dtCAOrgs.Rows)
                {
                    if (Convert.ToInt32(item["UID"].ToString()) == pt.ID)
                    {
                        tbGiftsContactName.Text = item["OrgContactPerson"].ToString();
                        tbGiftsContactPhone.Text = item["OrgContactPhone"].ToString();
                        break;
                    }
                }
                clsClient.clsHH.CAAdoptedDate = dtpDefaultAdoptedDate.Value;
                tbCAAdoptedDate.Text = dtpDefaultAdoptedDate.Value.ToShortDateString();
                dtpCAAdoptedDate.Value = dtpDefaultAdoptedDate.Value;
            }
        }

        private void btnApplyGifts_Click(object sender, EventArgs e)
        {
            parmType pt = (parmType)cboGiftsAdoptedBy.SelectedItem;
            foreach (DataGridViewRow item in dgvHHMembers.Rows)
            {
                int hhmID = Convert.ToInt32(item.Tag);
                clsClient.clsHHmem.find(hhmID);
                item.Cells["clmCAAdoptedBy"].Value = pt.ID.ToString();
                item.Cells["clmCAAdoptedName"].Value = tbGiftsContactName.Text;
                item.Cells["clmCAAdoptedDate"].Value = clsClient.clsHH.CAAdoptedDate.ToShortDateString();
                Application.DoEvents();
                clsClient.clsHHmem.CAAdoptedBy = pt.ID;
                clsClient.clsHHmem.CAAdoptedName = pt.LongName;
                clsClient.clsHHmem.CAAdoptedDate = clsClient.clsHH.CAAdoptedDate;
            }
        }
    }
}
