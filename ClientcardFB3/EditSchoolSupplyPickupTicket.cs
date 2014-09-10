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
    public partial class EditSchoolSupplyPickupTicket : Form
    {
        Client clsClient;

        public EditSchoolSupplyPickupTicket(Client newClient)
        {
            InitializeComponent();
            clsClient = newClient;
            List<parmType> ptList = new List<parmType>();
            int tmpPtr = 0;
            ptList.Add(new parmType(0, "Parent/Guardian", 0, "P/G"));
            for (int i = 0; i < clsClient.clsHHmem.RowCount; i++)
            {
                clsClient.clsHHmem.SetRecord(i);
                if (clsClient.clsHHmem.Age >= 15)
                {
                    ptList.Add(new parmType(clsClient.clsHHmem.ID, clsClient.clsHHmem.FirstName.Trim() + " " + clsClient.clsHHmem.LastName.Trim(), i, clsClient.clsHHmem.FirstName));
                }
            }
            if (ptList.Count > 0)
            {
                cboPickupPerson.DataSource = ptList;
                cboPickupPerson.DisplayMember = "LongName";
                cboPickupPerson.ValueMember = "LongName";
                cboPickupPerson.SelectedIndex = tmpPtr;
            }
            else
            {
                cboPickupPerson.DataSource = null;
                cboPickupPerson.Items.Clear();
                cboPickupPerson.Items.Add(clsClient.clsHH.Name);
                cboPickupPerson.SelectedIndex = 0;
            }
            CCFBGlobal.InitCombo(cboRegistration, CCFBGlobal.parmTbl_SchSupplyRegistration);
            initSchoolComboBox();
        }

        private void initSchoolComboBox()
        {
            DataGridViewComboBoxColumn colCbo = (DataGridViewComboBoxColumn) dgvHHMembers.Columns["clmSchool"];
            System.Collections.ArrayList newList = new System.Collections.ArrayList(CCFBGlobal.TypeCodesArray(CCFBGlobal.parmTbl_SchSupplySchool));
            if (newList != null && newList.Count > 0)
            {
                colCbo.DataSource = newList;
                colCbo.DisplayMember = "LongName";
                colCbo.ValueMember = "UID";
            }
            else
            {
                colCbo.DataSource = null;
                colCbo.Items.Add("Not Initialized");
            }
            //dgvHHMembers.Columns.Add(colCbo);
        }

        /// <summary>
        /// Loads the Students Listview with the School Age Members
        /// </summary>
        private void loadHHMems(bool clearRows)
        {
            DateTime d;
            Color CellForeColor;
            DataGridViewRow dvr;
            if (clearRows == true)
            {
                dgvHHMembers.Rows.Clear();
            }

            int rowCount = 0;
            int calculatedAge = 0;
            int bMonth = 0;
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
                    if (clsClient.clsHHmem.SchSupply == true | (clsClient.clsHHmem.Age >= 5 && clsClient.clsHHmem.Age <= 18))
                    {
                        if (clsClient.clsHHmem.Grade < 0)
                        {
                            bMonth = clsClient.clsHHmem.Birthdate.Month;
                            if (bMonth < curMonth || bMonth >= 9)
                            {
                                clsClient.clsHHmem.Grade = calculatedAge - 5;
                            }
                            else
                            {
                                clsClient.clsHHmem.Grade = calculatedAge - 4;
                            }
                            if (clsClient.clsHHmem.Grade > 12)
                            {
                                clsClient.clsHHmem.Grade = 12;
                            }
                        }
                        dgvHHMembers.Rows.Add();
                        if ((bool)clsClient.clsHHmem.DSet.Tables[0].Rows[i]["SchSupply"])
                        { CellForeColor = Color.Maroon; }
                        else
                        { CellForeColor = Color.Black; }
                        dvr = dgvHHMembers.Rows[rowCount];
                        dvr.Tag = clsClient.clsHHmem.ID.ToString();
                        FillGridMembersCell(dvr, "clmSchSupply", "SchSupply", true, CellForeColor, i, false);
                        //                    FillGridMembersCell(dvr, "clmHeadHH", "HeadHH", true, CellForeColor, i);
                        dvr.Cells["clmName"].Value = clsClient.clsHHmem.Name;

                        FillGridMembersCell(dvr, "clmGrade", "Grade", false, CellForeColor, i, false);
                        FillGridMembersCell(dvr, "clmAge", "Age", false, CellForeColor, i, true);
                        FillGridMembersCell(dvr, "clmGender", "Sex", false, CellForeColor, i, true);
                        dvr.Cells["clmSchool"].Value = clsClient.clsHHmem.SchSupplySchool.ToString();
                        //dvr.Cells["clmSchool"].Value = CCFBGlobal.LongNameFromId(CCFBGlobal.parmTbl_SchSupplySchool, clsClient.clsHHmem.SchSupplySchool);

                        string tmp = "";
                        if (clsClient.clsHHmem.DSet.Tables[0].Rows[i]["SchSupplyDelivered"].ToString() != "")
                        {
                            d = (DateTime)clsClient.clsHHmem.DSet.Tables[0].Rows[i]["SchSupplyDelivered"];
                            if (d > CCFBGlobal.FBNullDateValue)
                            {
                                tmp = d.ToShortDateString();
                            }
                        }
                        dvr.Cells["clmDelivered"].Value = tmp;
                        dvr.Cells["clmDelivered"].Tag = "SchSupplyDelivered";
                        FillGridMembersCell(dvr, "clmHMID", "ID", false, CellForeColor, i, true);
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
        /// <param name="CellForeColor">The Text Color Wanted</param>
        /// <param name="dsetRowIndex">The Row that you need from the database</param>
        private void FillGridMembersCell(DataGridViewRow dgvRow, String ColName, String FieldName, Boolean IsBoolean, Color CellForeColor, int dsetRowIndex, Boolean cellIsReadOnly)
        {
            DataGridViewCell dgvCell = dgvRow.Cells[ColName];
            if (IsBoolean)
            {
                if (dgvCell.EditType != null)
                {
                    if ((bool)clsClient.clsHHmem.DSet.Tables[0].Rows[dsetRowIndex][FieldName] == true)
                        dgvCell.Value = "Y";
                    else
                        dgvCell.Value = "";
                }
                else
                    dgvCell.Value = (bool)clsClient.clsHHmem.DSet.Tables[0].Rows[dsetRowIndex][FieldName];
            }
            else
            { dgvCell.Value = clsClient.clsHHmem.DSet.Tables[0].Rows[dsetRowIndex][FieldName]; }

            dgvCell.Tag = FieldName;

            if (dgvCell.Value == null)
                dgvCell.Value = "";

            dgvCell.Style.ForeColor = CellForeColor;
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
            if (cboPickupPerson.Focused == true)
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
                    clsClient.clsHHmem.SchSupply = Convert.ToBoolean(dgvHHMembers[e.ColumnIndex, e.RowIndex].Value);
                    break;
                case 3:
                    clsClient.clsHHmem.Grade = Convert.ToInt32(dgvHHMembers[e.ColumnIndex, e.RowIndex].Value);
                    break;
                case 5:
                    clsClient.clsHHmem.SchSupplySchool = Convert.ToInt32(dgvHHMembers[e.ColumnIndex, e.RowIndex].Value);
                    break;
                default:
                    break;
            }
        }

        private void dgvHHMembers_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            int grade = -1;
            if (e.ColumnIndex == 3)
            {
                grade = Convert.ToInt32(dgvHHMembers[e.ColumnIndex, e.RowIndex].Value);
                if (grade >= 0 && grade <= 12)
                {
                }
                else
                {
                    MessageBox.Show("Grade out of range");
                }
            }
        }

        private void SchoolSupplyPickupTicket_Load(object sender, EventArgs e)
        {
            tbID.Text = clsClient.clsHH.ID.ToString();
            lblHHName.Text = clsClient.clsHH.Name;
            chkSchoolSupplyFlag.Checked = clsClient.clsHH.SchSupplyFlag;
            cboPickupPerson.Text = clsClient.clsHH.SchSupplyPickupPerson;
            cboRegistration.Text = CCFBGlobal.LongNameFromId(CCFBGlobal.parmTbl_SchSupplyRegistration, clsClient.clsHH.SchSupplyRegistration);
            loadHHMems(true);
        }
        #endregion

        private void chkSchoolSupplyFlag_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSchoolSupplyFlag.Focused == true)
            {
                clsClient.clsHH.SchSupplyFlag = chkSchoolSupplyFlag.Checked;
                if (chkSchoolSupplyFlag.Checked == true)
                {
                    clsClient.clsHH.SchSupplyRegDate = DateTime.Now;
                }
            }
            btnPrintTicket.Enabled = chkSchoolSupplyFlag.Checked;
        }

        private void cboRegistration_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cboRegistration.Focused == true)
            {
                clsClient.clsHH.SchSupplyRegistration = ((parmType)cboRegistration.SelectedItem).ID;
            }
        }

        private void cboPickupPerson_Leave(object sender, EventArgs e)
        {
            {
                clsClient.clsHH.SetDataValue("SchSupplyPickupPerson", cboPickupPerson.Text);
            }

        }
    }
}
