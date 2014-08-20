using System;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Media;
using Microsoft.Win32;
using System.Threading;

namespace ClientcardFB3
{
	public partial class EditVolunteerForm : SqlGridDataSet
	{
		#region ----------Constants----------

		const string BTN_CANCEL_ADD		= "&Cancel Add";
		const string BTN_CANCEL_UPDATE	= "&Cancel Update";
		const string BTN_EDIT_BEGIN		= "&Begin Edit";
		const string BTN_EDIT_SAVE		= "&Save";				// Save new record.
		const string BTN_EDIT_UPDATE	= "&Save Updates";		// Update edit record.

		// Database columns listed in the order that they defined in the database table.
		const string COLUMN_ID					= "ID";
		const string COLUMN_INACTIVE			= "InActive";
		const string COLUMN_NAME				= "Name";
		const string COLUMN_ADDRESS				= "Address";
		const string COLUMN_CITY				= "City";
		const string COLUMN_STATE				= "State";
		const string COLUMN_ZIPCODE				= "ZipCode";
		const string COLUMN_PHONE				= "Phone";
		const string COLUMN_CELL_PHONE			= "CellPhone";
		const string COLUMN_WORK_PHONE			= "WorkPhone";
		const string COLUMN_COMPANY				= "Company";
		const string COLUMN_CONTACT_NAME		= "ContactName";
		const string COLUMN_CONTACT_PHONE		= "ContactPhone";
		const string COLUMN_RCD_TYPE			= "RcdType";
		const string COLUMN_NOTE				= "Notes";
		const string COLUMN_SEX				    = "Sex";
		const string COLUMN_AUTO_ALERT			= "AutoAlert";
		const string COLUMN_USERFLAG_0			= "UserFlag0";
		const string COLUMN_USERFLAG_1			= "UserFlag1";
		const string COLUMN_Vehicle				= "Vehicle";
		const string COLUMN_BackgroundCheck		= "BackgroundCheck";
		const string COLUMN_DATE_1				= "Date1";
		const string COLUMN_DATE_2				= "Date2";
		const string COLUMN_CREATED				= "Created";
		const string COLUMN_CREATEDBY			= "CreatedBy";
		const string COLUMN_MODIFIED			= "Modified";
        const string COLUMN_ModifiEDBY          = "ModifiedBy";
		const string COLUMN_NOT_ON_HOURS_LIST	= "NotOnHoursList";
        const string COLUMN_EmailAddress        = "EmailAddress";

		const string COLUMN_GRID_RCD_TYPE		= "gridEditVol_RcdType";

        DateTime oldDate;

        /// <summary>
		/// The functional state of the form. Used by DisplayControls to set the state of the
		/// controls.
		/// </summary>
		enum STATE
		{
			ADD,											// Add new record.
			DISPLAY,										// Normal display/browse mode.
			EDIT											// Edit current record mode.
		}		// end of enum STATE

		#endregion


		#region ----------Variables----------

		/// <summary>
		/// When running in SELECT mode then return the value of the record ID columnm.
		/// </summary>
		private int m_selectedId = 0;

		/// <summary>
		/// If FALSE then use the form in normal edit mode and if TRUE then operate in SELECT mode.
		/// </summary>
		private bool m_formSelectMode = false;
        private bool dataHasChanged = false;
        private bool bLoadingCombo = false;
        private bool stopEditMode = false;
        private bool bNormalMode = false;
        private bool bGroupsChanged = false;
        private bool bJobsChanged = false;
        private bool bInAddNewHoursMode = false;
        private string oriTBValue = "";
        private string lastSearchText = "";
        private string sOrderBy = " ORDER BY [Name]";
        private string sFilterColumn = "";
        private string sFilterValue = " ORDER BY [Name]";
        private string sWhereClause = " WHERE Inactive = 0 ";
        //bool[] volGroupValues;
        //SqlCommand command;
        SqlConnection conn = new SqlConnection(CCFBGlobal.connectionString);
        Volunteers clsVolunteersGroups = new Volunteers(CCFBGlobal.connectionString);
        Volunteers clsVols = new Volunteers(CCFBGlobal.connectionString);
        VolunteerHours clsVolHours = new VolunteerHours(CCFBGlobal.connectionString);
        Zipcodes clsZipcodes = new Zipcodes(CCFBGlobal.connectionString);

        int rowIndex = 0;
        bool loadingName = false;
        DateTime beginMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day).AddDays(-30);
        DateTime endMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
        bool cellChangesMade = false;
        List<TextBox> tbList = new List<TextBox>();
        bool loadingData = false;
        bool inEditMode = false;
        bool inAddMode = false;
        List<CheckBox> chkList = new List<CheckBox>();
        List<ComboBox> cboList = new List<ComboBox>();
        int currentRow = 0;

		#endregion

		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// ***** Constructor *****
		/// </summary>
		//-----------------------------------------------------------------------------------------
		public EditVolunteerForm (string a_dbConnectionString)
		{
			InitializeComponent();
            ConnectionString = a_dbConnectionString;
            FormSelectMode = false;
            initForm();
		}		// end of constructor


		public EditVolunteerForm (string a_dbConnectionString, bool a_selectMode)
		{
			InitializeComponent();
			ConnectionString	= a_dbConnectionString;
			FormSelectMode		= a_selectMode;
            initForm();
		}		// end of constructor

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// If data has changed then prompt the user to save the data before closing the dialog.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //-----------------------------------------------------------------------------------------
        private void EditVolunteerForm_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (FormSelectMode)								// Do not exit the form if in SELECT
            {												//   mode so cancel the form exit.
                e.Cancel = true;							// This prevents a dialog.Close.
                this.Visible = false;						// Hide the dialog in SELECT mode.
            }
            else
            {
                SelectedId = 0;									// Set to invalid DB record ID.
            }
            // In Normal mode control falls through and the dialog will just close.
        }		// end of EditvolunteerForm_FormClosing

        private void EditVolunteerForm_SizeChanged(object sender, EventArgs e)
        {
            if (WindowState != FormWindowState.Minimized)
            {
                gridEditVol.Width = Width - (gridEditVol.Left + 20);
                gridEditVol.Height = Height - (gridEditVol.Top + 40);
            }
        }

        //private void lvwVol_Group_Enter(object sender, EventArgs e)
        //{
        //    volGroupValues = new bool[lvwVol_Group.Items.Count];
        //    for (int i = 0; i < lvwVol_Group.Items.Count; i++)
        //    {
        //        volGroupValues[i] = lvwVol_Group.Items[i].Checked;
        //    }
        //}

        private void btnAddNewVolHrs_Click(object sender, EventArgs e)
        {
            bInAddNewHoursMode = true;
            dgvVolHours.Rows.Add();
            dgvVolHours.CurrentCell = dgvVolHours[0, dgvVolHours.Rows.Count - 1];
            changeAddNewHrsButtons(true);
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //-----------------------------------------------------------------------------------------
        private void btnAdd_Click(object sender, EventArgs e)
        {
            inAddMode = true;
            inEditMode = false;
            changeControlStates();
            DisplayControls(STATE.ADD);
            editVol_chkInactive.Checked = false;
        }		// end of btnAdd_Click


        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Restore the contents of the original record (after prompting the user).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //-----------------------------------------------------------------------------------------
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (dataHasChanged == true)
            {
                if (DialogResult.No == MessageBox.Show(
                        "Are you sure you want to cancel your changes?",
                        "Cancel Edit",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question))
                { return; }
            }

            clsVols.openWhere(sWhereClause);
            clsVols.find(SelectedId);
            fillForm();
            inAddMode = false;
            inEditMode = false;
            changeControlStates();
            DisplayControls(STATE.DISPLAY);
            gridEditVol.Focus();

        }		// end of btnCancel_Click

        private void btnCancelNewHrs_Click(object sender, EventArgs e)
        {
            bInAddNewHoursMode = false;
            changeAddNewHrsButtons(false);
            dgvVolHours.Rows.RemoveAt(dgvVolHours.Rows.Count - 1);
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// If the data was changed then save those updates to the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //-----------------------------------------------------------------------------------------
        private void btnClose_Click(object sender, EventArgs e)
        {
            SelectedId = 0;									// Set to invalid DB record ID.
            this.DialogResult = System.Windows.Forms.DialogResult.No;
            if (FormSelectMode)
                this.Visible = false;
            else
                Close();
        }		// end of btnClose_Click


        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Delete the current row.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //-----------------------------------------------------------------------------------------
        private void btnDelete_Click(object sender, EventArgs e)
        {
            //ClearFields("");
            if (gridEditVol.SelectedRows.Count > 0)
            {
                if (DialogResult.Yes == MessageBox.Show(
                        "Are you sure you want to delete the current record?",
                        "Delete Record",
                        MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Question))
                {
                    clsVols.delete(Convert.ToInt32(gridEditVol.CurrentRow.Cells[0].Value));
                    clsVolHours.delete(Convert.ToInt32(gridEditVol.CurrentRow.Cells[0].Value));
                    loadList();
                    fillForm();
                }
            }
            else
                MessageBox.Show("No Volunteer Selected. Please Select A Volunteer And Try Again.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

            editVol_btnEdit.Enabled = gridEditVol.Rows.Count > 0;
        }		// end of btnDelete_Click

        private void btnDeleteVolHrs_Click(object sender, EventArgs e)
        {
            if (dgvVolHours.CurrentRow.Tag != null)
            {
                clsVolHours.delete(Convert.ToInt32(dgvVolHours.CurrentRow.Tag));
                loadVolunteerHours();
            }
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Toggle edit/non-edit mode.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //-----------------------------------------------------------------------------------------
        private void btnEdit_Click(object sender, EventArgs e)
        {
            switch (editVol_btnEdit.Text)
            {
                case BTN_EDIT_BEGIN:								// Go into record edit mode.
                    DisplayControls(STATE.EDIT);
                    dataHasChanged = false;
                    inEditMode = true;
                    break;

                case BTN_EDIT_SAVE:									// New record save mode.
                    insert();
                    loadList();
                    fillForm();
                    DisplayControls(STATE.DISPLAY);
                    inAddMode = false;
                    break;

                case BTN_EDIT_UPDATE:								// Edit record update mode.
                    clsVols.update();
                    loadList();
                    DisplayControls(STATE.DISPLAY);
                    inEditMode = false;
                    break;
            }
            changeControlStates();
        }		// end of btnEdit_Click

        private void btnLoadCustom_Click(object sender, EventArgs e)
        {
            loadVolunteerHours();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if ((gridEditVol.CurrentRow != null) && (gridEditVol.CurrentRow.Index < gridEditVol.RowCount-1))
            {
                gridEditVol.CurrentCell = gridEditVol[0, gridEditVol.CurrentRow.Index + 1];
            }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if ((gridEditVol.CurrentRow != null) && (gridEditVol.CurrentRow.Index > 0))
            {
                gridEditVol.CurrentCell = gridEditVol[0, gridEditVol.CurrentRow.Index - 1];
            }

        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //-----------------------------------------------------------------------------------------
        private void btnSelect_Click(object sender, EventArgs e)
        {
            SelectedId = Convert.ToInt32(gridEditVol.Rows[currentRow].Cells[0].Value);
            this.Visible = false;
        }		// end of btnSelect_Click

        private void cboFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bLoadingCombo == false && sFilterColumn != "")
            {
                if (cboFilter.SelectedIndex != 0)
                {
                    if (sFilterColumn == "RcdType")
                        sFilterValue = sFilterColumn + "=" + cboFilter.Text.Substring(0, cboFilter.Text.IndexOf("=") - 1);
                    else
                        sFilterValue = sFilterColumn + "= '" + cboFilter.Text.Substring(0, 30).TrimEnd() + "'";
                }
                else
                {
                    sFilterValue = "";
                }
                SetWhereClause();
                clsVols.openWhere(sWhereClause);
                loadList();
            }
        }

        private void cboHoursPeriod_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cboHoursPeriod.Focused == true)
            {
                loadVolunteerHours();
            }
        }

        private void cboOrderBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboFilter.Visible = false;
            cboFilter.Text = "";
            lblFilterBy.Visible = false;
            string temp = " ORDER BY ";
            sFilterColumn = "";
            sFilterValue = "";
            switch (cboOrderBy.SelectedIndex)
            {
                case 0:
                    { temp += "Name"; break; }
                case 1:
                    { temp += "Address, Name"; break; }
                case 2:
                    {
                        temp += "City, Name";
                        sFilterColumn = "City"; break;
                    }
                case 3:
                    {
                        temp += "ZipCode, Name";
                        sFilterColumn = "Zipcode"; break;
                    }
                case 4:
                    { temp += "Phone, Name"; break; }
                case 5:
                    { temp += "ID"; break; }
                case 6:
                    {
                        temp += "RcdType, Name";
                        sFilterColumn = "RcdType"; break;
                    }
                default:
                    { temp += "ID"; break; }
            }
            if (temp != sOrderBy)
            {
                sOrderBy = temp;
                SetWhereClause();
                clsVols.openWhere(sWhereClause + sOrderBy);
                loadList();
            }
            if (sFilterColumn != "")
            {
                cboFilter.Visible = true;
                getDistints(sFilterColumn);
            }
            lblFilterBy.Visible = cboFilter.Visible;
        }

        private void changeAddNewHrsButtons(bool addNewHrs)
        {
            btnAddNewVolHrs.Visible = !addNewHrs;
            btnDeleteVolHrs.Enabled = !addNewHrs;
            btnCancelNewHrs.Visible = addNewHrs;
            btnCancelNewHrs.Enabled = addNewHrs;
        }

        private void changeControlStates()
        {
            foreach (TextBox tb in tbList)
            {
                if (inEditMode == true || inAddMode == true)
                {
                    tb.ReadOnly = false;
                }
                else
                {
                    tb.ReadOnly = true;
                    tb.BackColor = Color.White;
                }
            }
            foreach (ComboBox cbo in cboList)
                cbo.Enabled = inEditMode || inAddMode;
            foreach (CheckBox chk in chkList)
                chk.Enabled = inEditMode || inAddMode;

            lvwVol_Group.Enabled = inEditMode || inAddMode;
            lvwVol_Jobs.Enabled = inEditMode || inAddMode;
        }

        private void chkIncludeInactive_CheckedChanged(object sender, EventArgs e)
        {
            SetWhereClause();
            loadList();
            fillForm();
        }

        private void chkList_CheckedChanged(object sender, EventArgs e)
        {
            if (inEditMode == true)
            {
                CheckBox chkBox = (CheckBox)sender;
                clsVols.SetDataValue(chkBox.Tag.ToString(), chkBox.Checked);
            }
        }

        private void clearFields()
        {
            foreach (CheckBox cb in chkList)
                cb.Checked = false;

            foreach (TextBox tb in tbList)
                tb.Text = "";

            foreach (ComboBox cb in cboList)
                cb.SelectedValue = "0";

            for (int i = 0; i < lvwVol_Group.Items.Count; i++)
            {
                lvwVol_Group.Items[i].Checked = false;
            }

            for (int i = 0; i < lvwVol_Jobs.Items.Count; i++)
            {
                lvwVol_Jobs.Items[i].Checked = false;
            }
        }

        private void cmsVolGrid_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            cmsVolGrid.Close();
            if (e.ClickedItem.Text == "Export To Excel")
            {
                if (sender.GetType().ToString() == "")
                {
                    CCFBGlobal.ExportToExcel(gridEditVol, "Volunteers_" +
                        DateTime.Today.Year.ToString() + "_" +
                        CCFBGlobal.formatNumberWithLeadingZero(DateTime.Today.Month));
                }
                else
                {
                    CCFBGlobal.ExportToExcel(dgvVolHours, editVol_txtName.Text + " Hours_" +
                        DateTime.Today.Year.ToString() + "_" +
                        CCFBGlobal.formatNumberWithLeadingZero(DateTime.Today.Month));
                }
            }
        }

        private void dgvVolHours_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                cellChangesMade = true;
                oldDate = Convert.ToDateTime(dgvVolHours[e.ColumnIndex, e.RowIndex].Value);
            }
        }

        private void dgvVolHours_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvVolHours.Rows[e.RowIndex].Tag != null && stopEditMode == false)
            {
                clsVolHours.find(e.RowIndex);
                clsVolHours.SetDataValue("TrxDate", dgvVolHours["colDate", e.RowIndex].Value.ToString());
                clsVolHours.SetDataValue("NumVolHours", dgvVolHours["colHours", e.RowIndex].Value.ToString());
                clsVolHours.update();
                stopEditMode = true;
            }
            else if (dgvVolHours.Rows[e.RowIndex].Tag == null && dgvVolHours["colDate", e.RowIndex].Value != null
                && dgvVolHours["colHours", e.RowIndex].Value != null)
            {
                DataRow drow = clsVolHours.DSet.Tables[0].NewRow();
                drow["TrxDate"] = dgvVolHours["colDate", e.RowIndex].Value;
                drow["VolId"] = gridEditVol.CurrentRow.Cells["gridEditVol_ID"].Value;
                drow["NumVolunteers"] = 1;
                drow["NumVolHours"] = dgvVolHours["colHours", e.RowIndex].Value;
                clsVolHours.DSet.Tables[0].Rows.Add(drow);
                clsVolHours.insert();
                loadVolunteerHours();
                changeAddNewHrsButtons(false);
                stopEditMode = true;
            }
            else if (stopEditMode == true && dgvVolHours.Rows[e.RowIndex].Tag != null)
            {
                dgvVolHours["colDate", e.RowIndex].Value = clsVolHours.TrxDate.ToShortDateString();
                dgvVolHours["colHours", e.RowIndex].Value = clsVolHours.NumVolHours;
            }
            else if (stopEditMode == true && dgvVolHours.Rows[e.RowIndex].Tag == null)
            {
                dgvVolHours[e.ColumnIndex, e.RowIndex].Value = "";
            }
            cellChangesMade = false;
        }

        private void dgvVolHours_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
                oldDate = Convert.ToDateTime(dgvVolHours[e.ColumnIndex, e.RowIndex].Value);
        }

        private void dgvVolHours_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            stopEditMode = false;
            switch (e.ColumnIndex)
            {
                case 0:
                    {
                        if (bInAddNewHoursMode == true)
                        {
                            if (dgvVolHours[e.ColumnIndex, e.RowIndex].Value == null
                                || dgvVolHours[e.ColumnIndex, e.RowIndex].Value.ToString().Trim() == "")
                            {
                                break;
                            }
                        }
                        try
                        {
                            DateTime newDate = Convert.ToDateTime(dgvVolHours[e.ColumnIndex, e.RowIndex]
                                .GetEditedFormattedValue(e.RowIndex, DataGridViewDataErrorContexts.Commit));
                            if (CCFBGlobal.currentUser_PermissionLevel != CCFBGlobal.permissions_Admin
                                && newDate < beginMonth || newDate > endMonth)
                            {

                                if (MessageBox.Show("The Date You Entered Is Outside Of The 30 Day Range.  Please Re-Enter The Date And Try Again",
                                "DATE FORMATE INCORRECT", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == System.Windows.Forms.DialogResult.Retry)
                                {
                                    e.Cancel = true;
                                }
                                else
                                    stopEditMode = true;
                            }
                            else if (cellChangesMade == true && newDate != oldDate && clsVolHours.checkExistingDateForVol(newDate,
                                Convert.ToInt32(tbVolunteerId.Text)) == true)
                            {
                                if (MessageBox.Show("This Volunteer Already Has A TrxDate Matching This One. Please Re-Enter The Date And Try Again",
                                    "DATE ALREADY EXISTS FOR THIS VOLUNTEER", MessageBoxButtons.RetryCancel,
                                    MessageBoxIcon.Error) == System.Windows.Forms.DialogResult.Retry)
                                {
                                    e.Cancel = true;
                                }
                                else
                                {
                                    stopEditMode = true;
                                    cellChangesMade = false;
                                }
                            }
                        }
                        catch
                        {
                            if (MessageBox.Show("The Date You Entered Is Not In Proper Format.  Please Re-Format Date To MM/DD/YYYY",
                                "DATE FORMATE INCORRECT", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == System.Windows.Forms.DialogResult.Retry)
                            {
                                e.Cancel = true;
                            }
                            else
                                stopEditMode = true;
                        }
                        break;
                    }
                case 1:
                    {
                        try
                        {
                            Convert.ToDecimal(dgvVolHours[e.ColumnIndex, e.RowIndex]
                                .GetEditedFormattedValue(e.RowIndex, DataGridViewDataErrorContexts.Commit));
                        }
                        catch
                        {
                            if (MessageBox.Show("The Value You Entered Is Not In Proper Format.  Please Re-Format Value And Try Again",
                                "VALUE FORMATE INCORRECT", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == System.Windows.Forms.DialogResult.Retry)
                            {
                                e.Cancel = true;
                            }
                            else
                                stopEditMode = true;
                        }
                        break;
                    }
            }
        }

        private void dgvVolHours_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvVolHours.Rows[e.RowIndex].Tag != null)
            {
                clsVolHours.setDataRow(e.RowIndex);
                if (CCFBGlobal.currentUser_PermissionLevel == 1)
                {
                    if (clsVolHours.TrxDate >= beginMonth && clsVolHours.TrxDate <= endMonth)
                        btnDeleteVolHrs.Enabled = true;
                    else
                        btnDeleteVolHrs.Enabled = false;
                }
                else
                    btnDeleteVolHrs.Enabled = true;
            }
            stopEditMode = false;
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Set the state of the controls on the form.
        /// </summary>
        /// <param name="a_state"></param>
        //-----------------------------------------------------------------------------------------
        private void DisplayControls(STATE a_state)
        {
            editVol_btnAdd.Enabled = (a_state != STATE.EDIT);
            editVol_btnCancel.Visible = (a_state != STATE.DISPLAY);
            editVol_btnClose.Enabled = (a_state == STATE.DISPLAY);
            editVol_btnDelete.Enabled = (a_state == STATE.DISPLAY);
            editVol_btnEdit.Enabled = gridEditVol.Rows.Count > 0;
            btnAddNewVolHrs.Enabled = gridEditVol.Rows.Count > 0;
            editVol_btnPrint.Enabled = (a_state == STATE.DISPLAY);
            editVol_btnSelect.Visible = FormSelectMode;
            gridEditVol.Enabled = (a_state == STATE.DISPLAY);
            EditEnabled = (a_state == STATE.EDIT);
            editVol_btnSelect.Enabled = FormSelectMode;

            switch (a_state)
            {
                case STATE.ADD:
                    clearFields();
                    EditEnabled = true;						// Enable the input fields.

                    editVol_btnCancel.Text = BTN_CANCEL_ADD;
                    editVol_btnEdit.Text = BTN_EDIT_SAVE;
                    editVol_btnEdit.Enabled = true;
                    break;

                case STATE.DISPLAY:
                    editVol_btnEdit.Text = BTN_EDIT_BEGIN;
                    break;

                case STATE.EDIT:
                    editVol_btnCancel.Text = BTN_CANCEL_UPDATE;
                    editVol_btnEdit.Text = BTN_EDIT_UPDATE;
                    break;

                default:									// Should never happen in production app.
                    MessageBox.Show("Invalid STATE in DisplayControls");
                    break;
            }
        }		// end of DisplayControls

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Special handling is required to display the TypeCode value in the DataGridView. This
        /// method provides the necessary processing to the base class.
        /// </summary>
        //-----------------------------------------------------------------------------------------
        public override bool DisplayGridLineSpecial(int a_dataSetRow, int a_gridRow)
        {
            string rcdType = CCFBGlobal.LongNameFromId(CCFBGlobal.parmTbl_VolType
                , Convert.ToInt16(DataSetValue(a_dataSetRow, COLUMN_RCD_TYPE)));	// Get the ID from DataSet row.
            gridEditVol[COLUMN_GRID_RCD_TYPE, a_gridRow].Value = rcdType;

            if (Convert.ToBoolean(DataSetValue(a_dataSetRow, COLUMN_INACTIVE)) == true)
                gridEditVol.Rows[a_gridRow].DefaultCellStyle.ForeColor = Color.Red;

            return (rcdType.Length > 0);
        }		// end of DisplayGridLineSpecial

        private void editVol_cboRcdType_SelectedValueChanged(object sender, EventArgs e)
        {
            if(inEditMode == true)
                clsVols.SetDataValue(editVol_cboRcdType.Tag.ToString(), editVol_cboRcdType.SelectedValue.ToString());
        }

        private void editVol_txtCity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (clsZipcodes.getZip(editVol_txtCity.Text, editVol_txtState.Text) == true)
                {
                    editVol_txtZipCode.Text = clsZipcodes.ZipCode;
                    editVol_txtCity.Text = clsZipcodes.City;
                    editVol_txtState.Text = clsZipcodes.State.ToUpper();
                    e.SuppressKeyPress = true;
                    editVol_cboRcdType.Focus();
                }
            }
        }

        private void editVol_txtZipCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (clsZipcodes.getCity(editVol_txtZipCode.Text) == true)
                {
                    editVol_txtCity.Text = clsZipcodes.City;
                    editVol_txtState.Text = clsZipcodes.State.ToUpper();
                    e.SuppressKeyPress = true;
                    editVol_cboRcdType.Focus();
                }
            }
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Clear the input fields, put them in edit mode, and set the buttons properly.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //-----------------------------------------------------------------------------------------
        private void EditVolForm_btnAdd_Click(object sender, EventArgs e)
        {
            DisplayControls(STATE.ADD);
        }		// end of EditVolForm_btnAdd_Click

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Read the Type Codes database table and display the data in a DataGridView control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //-----------------------------------------------------------------------------------------
        private void EditVolForm_Load(object sender, EventArgs e)
        {
            TableName = "Volunteers";				// Read from this SQL table.
            ControlPage = this.Controls;
            DataGridViewObject = gridEditVol;
            editVol_cboRcdType.DataSource = CCFBGlobal.TypeCodesArray(CCFBGlobal.parmTbl_VolType);
            editVol_cboRcdType.DisplayMember = "LongName";
            editVol_cboRcdType.ValueMember = "UID";
            loadUserFieldLabels();
            LoadlvwGroups();
            LoadlvwJobs();
            //RefreshGrid();
            btnCancelNewHrs.Enabled = false;
            if (gridEditVol.SelectedRows.Count > 0)
                fillForm();
        }		// end of EditVolForm_Load

        ////-----------------------------------------------------------------------------------------
        ///// <summary>
        ///// Do some special handling to convert the row data to the display format.
        ///// </summary>
        ///// <param name="a_row">The row being processed.</param>
        ////-----------------------------------------------------------------------------------------
        public override void Event_SelectionChangedSpecial(int a_row)
        {
            tbVolunteerId.Text = clsVols.ID.ToString();
            loadVolunteerHours();
            clsVolunteersGroups.getVolGrpsForVol(tbVolunteerId.Text);
            setActiveVolGroups();
            clsVolunteersGroups.getVolJobsForVol(tbVolunteerId.Text);
            setActiveVolJobs();
        }		// end of Event_SelectionChangedSpecial

        private void fillForm()
        {
            if (clsVols.RowCount > 0)
            {
                foreach (TextBox tb in tbList.OfType<TextBox>())
                {
                    if (tb.Tag.ToString() != "")
                        tb.Text = clsVols.GetDataValue(tb.Tag.ToString()).ToString();
                }
                foreach (CheckBox cb in chkList)
                {
                    if (cb.Tag != null && cb.Tag.ToString() != "")
                        cb.Checked = (bool)clsVols.GetDataValue(cb.Tag.ToString());
                }
                Event_SelectionChangedSpecial(currentRow);
                editVol_cboRcdType.SelectedValue = clsVols.RcdType.ToString();
            }
        }

        private void FindByName(string colNameFull)
        {
            int rowStart = 0;
            if (loadingName == false)
            {
                if (tbFindName.TextLength >= lastSearchText.Length)
                    rowStart = rowIndex;
                else
                    rowStart = 0;
                lastSearchText = tbFindName.Text.ToUpper().Trim();
                int row = 0;

                for (int i = rowStart; i < gridEditVol.Rows.Count; i++)
                {
                    if (gridEditVol.Rows[i].Cells[colNameFull].FormattedValue.ToString().ToUpper().StartsWith(lastSearchText) == true)
                    {
                        //GridSelectionHandlerEnabled = false;
                        //gridEditVol.CurrentCell = gridEditVol[0, i];
                        //Event_GridSelectionChanged(null, null);
                        //if (i < gridEditVol.FirstDisplayedScrollingRowIndex
                        //    || i > gridEditVol.Rows.GetLastRow(DataGridViewElementStates.Displayed) - 5)
                        //    if (i > 5)
                        //        gridEditVol.FirstDisplayedScrollingRowIndex = i - 5;
                        //    else
                        //        gridEditVol.FirstDisplayedScrollingRowIndex = i;
                        row = i;
                        break;
                    }
                    else if (gridEditVol.Rows[i].Cells[colNameFull].FormattedValue.ToString().CompareTo(lastSearchText) == -1)
                    {
                        row = i;
                    }
                }

                if (row < gridEditVol.Rows.Count)
                {
                    GridSelectionHandlerEnabled = false;
                    gridEditVol.CurrentCell = gridEditVol[0, row];
                    Event_GridSelectionChanged(null, null);
                    if (row < gridEditVol.FirstDisplayedScrollingRowIndex
                                || row > gridEditVol.Rows.GetLastRow(DataGridViewElementStates.Displayed) - 5)
                        if (row > 5)
                            gridEditVol.FirstDisplayedScrollingRowIndex = row - 5;
                        else
                            gridEditVol.FirstDisplayedScrollingRowIndex = row;
                }
            }
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// If FALSE then use the form in normal edit mode and if TRUE then operate in SELECT mode.
        /// </summary>
        //-----------------------------------------------------------------------------------------
        public bool FormSelectMode
        {
            get { return (m_formSelectMode); }
            set { m_formSelectMode = value; }
        }		// end of property FormSelectMode

        /// <summary>
        /// Retrives all distinct values for a given column Name in the Household Table
        /// </summary>
        /// <param name="colName">The Name of the Column to get distincts for</param>
        private void getDistints(string colName)
        {
            bool foundMatch = false;
            bLoadingCombo = true;
            cboFilter.Items.Clear();
            cboFilter.Items.Add("No Filter");
            cboFilter.SelectedIndex = 0;

            //Gets And Adds to the filter combo the distinct values of the column from the household table
            string whereClause = "";
            if (chkIncludeInactive.Checked == false)
                whereClause = " WHERE Inactive=0 ";
            clsVolunteersGroups.getDistincts(colName, whereClause);
            string sVal = "";
            System.Collections.ArrayList typesVolunteer = CCFBGlobal.TypeCodesArray(CCFBGlobal.parmTbl_VolType);
            int iD = 0;
            for (int i = 0; i < clsVolunteersGroups.RowCount; i++)
            {
                sVal = clsVolunteersGroups.DSet.Tables[0].Rows[i][0].ToString();
                foundMatch = true;
                if (colName == "RcdType")
                {
                    iD = Convert.ToInt32(sVal);
                    sVal = CCFBGlobal.formatNumberWithLeadingZero(iD);
                    foundMatch = false;
                    for (int j = 0; j < typesVolunteer.Count; j++)
                    {
                        parmType pt = (parmType)typesVolunteer[j];
                        if (pt.ID == iD)
                        {
                            sVal += " = " + pt.LongName;
                            foundMatch = true;
                            break;
                        }
                    }
                }
                if (foundMatch == true)
                    cboFilter.Items.Add(sVal + new String((char)32, (30 - sVal.Length)) + "[ " + clsVolunteersGroups.DSet.Tables[0].Rows[i][1].ToString() + " ]");
            }
            bLoadingCombo = false;
        }

        private void gridEditVol_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (loadingData == false)
            {
                currentRow = e.RowIndex;
                clsVols.find(Convert.ToInt32(gridEditVol.Rows[e.RowIndex].Cells["gridEditVol_ID"].Value));
                SelectedId = clsVols.ID;
                fillForm();
            }
        }

        private void initForm()
        {
            editVol_btnPrint.Visible = false;
            DataGridViewObject = gridEditVol;
            loadingData = true;
            loadList();
            loadingData = false;
            traverseAndAddControlsToCollections(this.Controls);
            fillForm();
            DisplayControls(STATE.DISPLAY);
            changeControlStates();
            //editVol_btnSelect.Visible = false;
            //editVol_btnCancel.Visible = false;
        }

        private void insert()
        {
            DataRow drow = clsVols.DSet.Tables[0].NewRow();
            foreach (TextBox tb in tbList)
            {
                if (tb.Tag.ToString() == "Date1" || tb.Tag.ToString() == "Date2")
                {
                    try { drow[tb.Tag.ToString()] = Convert.ToDateTime(tb.Text); }
                    catch { drow[tb.Tag.ToString()] = "01/01/1900"; }
                }
                else if (tb.Tag.ToString() != "")
                    drow[tb.Tag.ToString()] = tb.Text;
            }
            foreach (CheckBox chk in chkList)
            {
                drow[chk.Tag.ToString()] = chk.Checked;
            }
            foreach (ComboBox cb in cboList)
            {
                drow[cb.Tag.ToString()] = cb.SelectedValue;
            }
            drow["Created"] = DateTime.Now.ToString();
            drow["CreatedBy"] = CCFBGlobal.dbUserName;
            clsVols.DSet.Tables[0].Rows.Add(drow);
            clsVols.update();
            clsVols.openWhere(" Where Name = '" + editVol_txtName.Text + "' And Created = '" + drow["Created"].ToString() + "'");
            if (clsVols.RowCount > 0)
            {
                for (int i = 0; i < lvwVol_Group.Items.Count; i++)
                {
                    if (lvwVol_Group.Items[i].Checked == true)
                    {
                        CCFBGlobal.executeQuery("Insert Into VolunteerGroups (VolID, GroupID)"
                            + " Values(" + clsVols.ID.ToString()
                            + ", " + lvwVol_Group.Items[i].Text + ")");
                    }
                }
            }
        }

        private void loadList()
        {
            loadingData = true;
            gridEditVol.Rows.Clear();
            clsVols.openWhere(sWhereClause + sOrderBy);
            for (int i = 0; i < clsVols.RowCount; i++)
            {
                clsVols.setRecord(i);
                gridEditVol.Rows.Add();
                gridEditVol["gridEditVol_ID", i].Value = clsVols.ID;
                gridEditVol["gridEditVol_Name", i].Value = clsVols.Name;
                gridEditVol["gridEditVol_Address", i].Value = clsVols.Address;
                gridEditVol["gridEditVol_City", i].Value = clsVols.City;
                gridEditVol["gridEditVol_Zip", i].Value = clsVols.ZipCode;
                gridEditVol["gridEditVol_Phone", i].Value = clsVols.Phone;
                gridEditVol["gridEditVol_RcdType", i].Value = CCFBGlobal.LongNameFromId(CCFBGlobal.parmTbl_VolType,
                    Convert.ToInt32(clsVols.RcdType));
                gridEditVol.PerformLayout();
            }

            if (gridEditVol.Rows.Count > currentRow)
            {
                gridEditVol.CurrentCell = gridEditVol[0, currentRow];
                clsVols.find(Convert.ToInt32(gridEditVol[0, currentRow].Value));
            }
            else if (gridEditVol.Rows.Count > currentRow - 1 && currentRow != 0)
            {
                gridEditVol.CurrentCell = gridEditVol[0, currentRow - 1];
                clsVols.find(Convert.ToInt32(gridEditVol[0, currentRow - 1].Value));
            }
            loadingData = false;
        }

        private void LoadlvwGroups()
        {
            lvwVol_Group.Items.Clear();
            foreach (parmType item in CCFBGlobal.TypeCodesArray(CCFBGlobal.parmTbl_VolGroups))
            {
                ListViewItem lvItm = new ListViewItem(item.ID.ToString());
                lvItm.SubItems.Add(item.LongName);
                lvwVol_Group.Items.Add(lvItm);
            }
        }

        private void LoadlvwJobs()
        {
            lvwVol_Jobs.Items.Clear();
            foreach (parmType item in CCFBGlobal.TypeCodesArray(CCFBGlobal.parmTbl_FBJobs))
            {
                ListViewItem lvItm = new ListViewItem(item.ID.ToString());
                lvItm.SubItems.Add(item.LongName);
                lvwVol_Jobs.Items.Add(lvItm);
            }
        }

        /// <summary>  Loads User Check Box Labels from UserFields Table
        /// </summary>
        private void loadUserFieldLabels()
        {
            UserFields clsUserFields = new UserFields(CCFBGlobal.connectionString);
            clsUserFields.open("Volunteers");
            clsUserFields.setDataRow("UserFlag0");
            if ("UserFlag0" == clsUserFields.FldName)
            {
                chkUserFlag0.Text = clsUserFields.EditLabel.Trim();
                chkUserFlag0.Visible = (chkUserFlag0.Text != "");
            }
            clsUserFields.setDataRow("UserFlag1");
            if ("UserFlag1" == clsUserFields.FldName)
            {
                chkUserFlag1.Text = clsUserFields.EditLabel.Trim();
                chkUserFlag1.Visible = (chkUserFlag1.Text != "");
            }
            clsUserFields.setDataRow("Date1");
            if ("Date1" == clsUserFields.FldName)
            {
                lblDate1.Text = clsUserFields.EditLabel.Trim();
                tbDate1.Visible = (lblDate1.Text != "");
                lblDate1.Visible = (lblDate1.Text != "");
            }
            clsUserFields.setDataRow("Date2");
            if ("Date2" == clsUserFields.FldName)
            {
                lblDate2.Text = clsUserFields.EditLabel.Trim();
                tbDate2.Visible = (lblDate2.Text != "");
                lblDate2.Visible = (lblDate2.Text != "");
            }
        }

        private void loadVolunteerHours()
        {
            string sCurrMonthStart = DateTime.Today.Month.ToString() + "/01/" + DateTime.Today.Year.ToString();
            string sVolHrsWhereClause = "";
            bInAddNewHoursMode = false;
            if (cboHoursPeriod.SelectedIndex == -1)
                cboHoursPeriod.SelectedIndex = 0;
            switch (cboHoursPeriod.SelectedIndex)
            {
                case 0:
                    {
                        sVolHrsWhereClause = " WHERE TrxDate BETWEEN '" + sCurrMonthStart + "' AND '"
                            + Convert.ToDateTime(sCurrMonthStart).AddMonths(1).AddDays(-1).ToShortDateString() + "'";
                        break;
                    }
                case 1:
                    {
                        sVolHrsWhereClause = " WHERE TrxDate BETWEEN '" + Convert.ToDateTime(sCurrMonthStart).AddMonths(-1).ToShortDateString()
                            + "' AND '" + Convert.ToDateTime(sCurrMonthStart).AddDays(-1).ToShortDateString() + "'";
                        break;
                    }
                case 2:
                    {
                        sVolHrsWhereClause = " WHERE TrxDate BETWEEN '" + DateTime.Today.AddDays(-90).ToShortDateString()
                            + "' AND '" + DateTime.Today.ToShortDateString() + "'";
                        break;
                    }
                case 3:
                    {
                        sVolHrsWhereClause = " WHERE TrxDate BETWEEN '" + Convert.ToDateTime("1/1/" + DateTime.Today.Year).ToShortDateString()
                            + "' AND '" + DateTime.Today.ToShortDateString() + "'";
                        break;
                    }
                case 4:
                    {
                        sVolHrsWhereClause = " WHERE TrxDate BETWEEN '" + Convert.ToDateTime("1/1/" + DateTime.Today.Year).AddYears(-1).ToShortDateString()
                            + "' AND '" + Convert.ToDateTime("12/31/" + DateTime.Today.Year).AddYears(-1).ToShortDateString() + "'";
                        break;
                    }
                case 5:
                    {
                        sVolHrsWhereClause = "WHERE TrxDate Is NOT NULL";
                        break;
                    }
                case 6:
                    {
                        sVolHrsWhereClause = " WHERE TrxDate BETWEEN '" + dtpFrom.Text + "' AND '" + dtpTo.Text + "'";
                        break;
                    }
            }
            dgvVolHours.Rows.Clear();
            clsVolHours.openWhere(sVolHrsWhereClause + " AND VolId = " + tbVolunteerId.Text + " Order By TrxDate");
            if (clsVolHours.RowCount > 0)
            {
                int row = 0;
                DateTime trxDate;
                foreach (DataRow dRow in clsVolHours.DSet.Tables[0].Rows)
                {
                    dgvVolHours.Rows.Add();
                    trxDate = Convert.ToDateTime(dRow["TrxDate"]);
                    dgvVolHours[0, row].Value = trxDate.ToShortDateString();
                    dgvVolHours[1, row].Value = dRow["NumVolHours"].ToString();
                    dgvVolHours.Rows[row].Tag = dRow["ID"];
                    row++;
                    dgvVolHours.PerformLayout();
                    //Application.DoEvents();
                }

                btnDeleteVolHrs.Enabled = true;
            }
            else
            {
                btnDeleteVolHrs.Enabled = false;
            }
        }

        private void lvwVol_Group_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (bNormalMode == true)
                bGroupsChanged = true;
        }

        private void lvwVol_Group_Leave(object sender, EventArgs e)
        {
            if (bGroupsChanged == true && inAddMode == false)
            {
                CCFBGlobal.executeQuery("Delete From VolunteerGroups Where VolID= " + tbVolunteerId.Text);
                for (int i = 0; i < lvwVol_Group.Items.Count; i++)
                {
                    if (lvwVol_Group.Items[i].Checked == true)
                    {
                        CCFBGlobal.executeQuery("Insert Into VolunteerGroups (VolID, GroupID)"
                            + " Values(" + tbVolunteerId.Text
                            + ", " + lvwVol_Group.Items[i].Text + ")");
                    }
                }
            }
            bGroupsChanged = false;
        }

        private void lvwVol_Jobs_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (bNormalMode == true)
                bJobsChanged = true;
        }

        private void lvwVol_Jobs_Leave(object sender, EventArgs e)
        {
            if (bJobsChanged == true && inAddMode == false)
            {
                CCFBGlobal.executeQuery("Delete From VolunteerJobs Where VolID= " + tbVolunteerId.Text);
                for (int i = 0; i < lvwVol_Jobs.Items.Count; i++)
                {
                    if (lvwVol_Jobs.Items[i].Checked == true)
                    {
                        CCFBGlobal.executeQuery("Insert Into VolunteerJobs (VolID, JobID)"
                                + " Values(" + tbVolunteerId.Text
                                + ", " + lvwVol_Jobs.Items[i].Text + ")");
                    }
                }
            }
            bJobsChanged = false;
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Return the record ID for the selected record in the grid (used in SELECT mode). This
        /// value is only populated in the SELECT button.
        /// </summary>
        //-----------------------------------------------------------------------------------------
        public int SelectedId
        {
            get { return (m_selectedId); }
            set { m_selectedId = value; }
        }		// end of property SelectedId

        private void setActiveVolGroups()
        {
            int[] groups = clsVolunteersGroups.Groups;
            bNormalMode = false;
            for (int i = 0; i < lvwVol_Group.Items.Count; i++)
            {
                lvwVol_Group.Items[i].Checked = false;
                for (int j = 0; j < groups.Length; j++)
                {
                    if (Convert.ToInt32(lvwVol_Group.Items[i].Text) == groups[j])
                    {
                        lvwVol_Group.Items[i].Checked = true;
                    }
                }
            }
            bNormalMode = true;
            bGroupsChanged = false;
        }

        private void setActiveVolJobs()
        {
            int[] jobs = clsVolunteersGroups.Jobs;
            bNormalMode = false;
            for (int i = 0; i < lvwVol_Jobs.Items.Count; i++)
            {
                lvwVol_Jobs.Items[i].Checked = false;
                for (int j = 0; j < jobs.Length; j++)
                {
                    if (Convert.ToInt32(lvwVol_Jobs.Items[i].Text) == jobs[j])
                    {
                        lvwVol_Jobs.Items[i].Checked = true;
                    }
                }
            }
            bNormalMode = true;
            bJobsChanged = false;
        }

        private void SetWhereClause()
        {
            if (chkIncludeInactive.CheckState == CheckState.Unchecked)
            {
                sWhereClause = "WHERE Inactive = 0";
                if (sFilterColumn != "" && sFilterValue != "")
                    sWhereClause += " AND " + sFilterValue;
            }
            else
            {
                if (sFilterColumn != "" && sFilterValue != "")
                    sWhereClause = " WHERE " + sFilterValue;
                else
                    sWhereClause = "";
            }
        }

        private void tbFindName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
                btnNext_Click(null, null);
            else if (e.KeyCode == Keys.Up)
                btnPrev_Click(null, null);
        }

        private void tbFindName_TextChanged(object sender, EventArgs e)
        {
            if (tbFindName.Text.Trim() == "" && gridEditVol.Rows.Count > 0)
            { gridEditVol.CurrentCell = gridEditVol[0, 0]; }
            else
            {
                switch (cboOrderBy.SelectedIndex)
                {
                    //case 0:
                    //    { FindByName("gridEditVol_Name"); break; }
                    case 1:
                        { FindByName("gridEditVol_Address"); break; }
                    //case 2:
                    //    { FindByName("gridEditVol_Name"); break; }
                    //case 3:
                    //    { FindByName("gridEditVol_Name"); break; }
                    case 4:
                        { FindByName("gridEditVol_Phone"); break; }
                    case 5:
                        { FindByName("gridEditVol_ID"); break; }
                    //case 6:
                    //    { FindByName("gridEditVol_Name"); break; }
                    default:
                        { FindByName("gridEditVol_Name"); break; }
                }
            }
        }

        private void tbList_Enter(object sender, EventArgs e)
        {
            if (sender != null)
            {
                TextBox tb1 = (TextBox)sender;
                oriTBValue = tb1.Text;
            }
            else
            {
                oriTBValue = "";
            }
        }

        private void tbList_Leave(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Enabled == true && inEditMode == true)
            {
                if (oriTBValue != tb.Text)
                {
                    dataHasChanged = true;
                    editVol_btnEdit.Enabled = true;
                    clsVols.SetDataValue(tb.Tag.ToString(), tb.Text);
                }
            }
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
                                tbList.Add((TextBox)cntrl);
                                cntrl.Enter += new System.EventHandler(this.tbList_Enter);
                                cntrl.Leave += new System.EventHandler(this.tbList_Leave);
                            }
                            break;
                        }
                    case "CheckBox":
                        {
                            CheckBox chk = (CheckBox)cntrl;
                            if (chk.Tag != null && chk.Tag.ToString() != "")
                            {
                                chk.CheckedChanged += new System.EventHandler(this.chkList_CheckedChanged);
                                //chk.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkList_KeyDown);
                                chkList.Add(chk);
                            }
                            break;
                        }
                    case "ComboBox":
                        {
                            if (cntrl.Tag != null)
                            {
                                if (cntrl.Tag.ToString().Trim() != "")
                                {
                                    cboList.Add((ComboBox)cntrl);
                                }
                            }
                            break;
                        }
                }

                traverseAndAddControlsToCollections(cntrl.Controls);
            }
        }
    } // end of class
}		// end of namespace
