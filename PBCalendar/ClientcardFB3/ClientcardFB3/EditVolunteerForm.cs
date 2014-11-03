using System;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient; 

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
		const string COLUMN_INFO_1				= "Info1";
		const string COLUMN_INFO_2				= "Info2";
		const string COLUMN_DATE_1				= "Date1";
		const string COLUMN_DATE_2				= "Date2";
		const string COLUMN_CREATED				= "Created";
		const string COLUMN_CREATED_BY			= "CreatedBy";
		const string COLUMN_MODIFIED			= "Modified";
		const string COLUMN_NOT_ON_HOURS_LIST	= "NotOnHoursList";

		const string COLUMN_GRID_RCD_TYPE		= "gridEditVol_RcdType";


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
        private bool bLoadingCombo = false;
        private bool stopEditMode = false;
        private string oriTBValue = "";
        private string lastSearchText = "";
        private string sOrderBy = " ORDER BY [Name]";
        private string sFilterColumn = "";
        private string sFilterValue = "";
        private string sWhereClause = "";
        bool[] volGroupValues;
        SqlCommand command;
        SqlConnection conn = new SqlConnection(CCFBGlobal.connectionString);
        Volunteers clsVolunteers = new Volunteers(CCFBGlobal.connectionString);
        VolunteerHours clsVolHours = new VolunteerHours(CCFBGlobal.connectionString);
        int rowIndex = 0;
        bool loadingName = false;
        DateTime beginMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day).AddDays(-30);
        DateTime endMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
        bool cellChangesMade = false;

		#endregion


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// ***** Constructor *****
		/// </summary>
		/// <param name="a_dbServer">Connect to the database on this server.</param>
		/// <param name="a_dbName">Connect to this database.</param>
		/// <param name="a_dbLogoin">User ID to connect to the SQL database.</param>
		/// <param name="a_dbPassword">Password to connect to the SQL database.</param>
		/// <param name="a_dbTable">Database table that contains the Volunteer details.</param>
		//-----------------------------------------------------------------------------------------
		public EditVolunteerForm (string a_dbServer, string a_dbName, string a_dbUsername, string a_dbPassword)
		{
			InitializeComponent();
            cboHoursPeriod.SelectedIndex = 0;
			DataGridViewObject	= gridEditVol;
			ConnectionStringCreate (a_dbServer, a_dbName, a_dbUsername, a_dbPassword);
		}		// end of constructor


		public EditVolunteerForm (string a_dbConnectionString)
		{
			InitializeComponent();

			DataGridViewObject	= gridEditVol;
			ConnectionString	= a_dbConnectionString;
		}		// end of constructor


		public EditVolunteerForm (string a_dbConnectionString, bool a_selectMode)
		{
			InitializeComponent();

			DataGridViewObject	= gridEditVol;
			ConnectionString	= a_dbConnectionString;
			FormSelectMode		= a_selectMode;
		}		// end of constructor

        private void EditVolunteerForm_SizeChanged(object sender, EventArgs e)
        {
            if (WindowState != FormWindowState.Minimized)
            {
                gridEditVol.Width = Width - (gridEditVol.Left + 20);
                gridEditVol.Height = Height - (gridEditVol.Top + 40);
            }
        }

		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		//-----------------------------------------------------------------------------------------
		private void btnAdd_Click (object sender, EventArgs e)
		{
			DisplayControls (STATE.ADD);
		}		// end of btnAdd_Click


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Restore the contents of the original record (after prompting the user).
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		//-----------------------------------------------------------------------------------------
		private void btnCancel_Click (object sender, EventArgs e)
		{
			if (DialogResult.Yes == MessageBox.Show (
					"Are you sure you want to cancel your changes?",
					"Cancel Changes",
					MessageBoxButtons.YesNoCancel,
					MessageBoxIcon.Question))
			{
				Event_GridSelectionChanged (null, null);	// Restore original record data.
				DisplayControls (STATE.DISPLAY);
			}
		}		// end of btnCancel_Click


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// If the data was changed then save those updates to the database.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		//-----------------------------------------------------------------------------------------
		private void btnClose_Click (object sender, EventArgs e)
		{
			SelectedId = 0;									// Set to invalid DB record ID.
			if (FormSelectMode)
				this.Visible = false;
			else
				Close ();
		}		// end of btnClose_Click


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Delete the current row.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		//-----------------------------------------------------------------------------------------
		private void btnDelete_Click (object sender, EventArgs e)
		{
			//&&&&& test for use on empty database.
			RowDeleteCurrent (true);
		}		// end of btnDelete_Click


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Toggle edit/non-edit mode.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		//-----------------------------------------------------------------------------------------
		private void btnEdit_Click (object sender, EventArgs e)
		{
			switch (editVol_btnEdit.Text)
			{
				case BTN_EDIT_BEGIN:								// Go into record edit mode.
					DisplayControls (STATE.EDIT);
					break;

				case BTN_EDIT_SAVE:									// New record save mode.
					RowInsert ();
					DbSave ();
					DisplayControls (STATE.DISPLAY);
					break;

				case BTN_EDIT_UPDATE:								// Edit record update mode.
					MoveDisplayToDataSet ();
					DbSave ();
					DisplayControls (STATE.DISPLAY);
					break;
			}
		}		// end of btnEdit_Click


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		//-----------------------------------------------------------------------------------------
		private void btnSelect_Click (object sender, EventArgs e)
		{
			SelectedId = Convert.ToInt32 (DataSetValue (GridCurrentRow(), COLUMN_ID));
			this.Visible = false;
		}		// end of btnSelect_Click


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Set the state of the controls on the form.
		/// </summary>
		/// <param name="a_state"></param>
		//-----------------------------------------------------------------------------------------
		private void DisplayControls (STATE a_state)
		{
			editVol_btnAdd.Enabled		= (a_state != STATE.EDIT);
			editVol_btnCancel.Visible	= (a_state != STATE.DISPLAY);
			editVol_btnClose.Enabled	= (a_state == STATE.DISPLAY);
			editVol_btnDelete.Enabled	= (a_state == STATE.DISPLAY);
			editVol_btnEdit.Enabled		= true;
			editVol_btnPrint.Enabled	= (a_state == STATE.DISPLAY);
			editVol_btnSelect.Visible	= FormSelectMode;
			gridEditVol.Enabled			= (a_state == STATE.DISPLAY);
			EditEnabled					= (a_state == STATE.EDIT);
			editVol_btnSelect.Enabled	= FormSelectMode;

			switch (a_state)
			{
				case STATE.ADD:
					EditEnabled = true;						// Enable the input fields.
					ClearFields ();
					editVol_btnCancel.Text	= BTN_CANCEL_ADD;
					editVol_btnEdit.Text	= BTN_EDIT_SAVE;
					break;

				case STATE.DISPLAY:
					editVol_btnEdit.Text	= BTN_EDIT_BEGIN;
					break;

				case STATE.EDIT:
					editVol_btnCancel.Text	= BTN_CANCEL_UPDATE;
					editVol_btnEdit.Text	= BTN_EDIT_UPDATE;
					break;

				default:									// Should never happen in production app.
					MessageBox.Show ("Invalid STATE in DisplayControls");
					break;
			}
		}		// end of DisplayControls


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Special handling is required to display the TypeCode value in the DataGridView. This
		/// method provides the necessary processing to the base class.
		/// </summary>
		//-----------------------------------------------------------------------------------------
		public override bool DisplayGridLineSpecial (int a_dataSetRow, int a_gridRow)
		{
			string rcdType = CCFBGlobal.LongNameFromId(CCFBGlobal.parmTbl_VolType
				, Convert.ToInt16(DataSetValue (a_dataSetRow, COLUMN_RCD_TYPE)));	// Get the ID from DataSet row.
			gridEditVol[COLUMN_GRID_RCD_TYPE,a_gridRow].Value = rcdType;
			return (rcdType.Length > 0);
		}		// end of DisplayGridLineSpecial


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Clear the input fields, put them in edit mode, and set the buttons properly.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		//-----------------------------------------------------------------------------------------
		private void EditVolForm_btnAdd_Click (object sender, EventArgs e)
		{
			DisplayControls (STATE.ADD);
		}		// end of EditVolForm_btnAdd_Click


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// If data has changed then prompt the user to save the data before closing the dialog.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		//-----------------------------------------------------------------------------------------
		private void EditVolunteerForm_FormClosing (object sender, FormClosingEventArgs e)
		{
			SelectedId = 0;									// Set to invalid DB record ID.
			if (FormSelectMode)								// Do not exit the form if in SELECT
			{												//   mode so cancel the form exit.
				e.Cancel = true;							// This prevents a dialog.Close.
				this.Visible = false;						// Hide the dialog in SELECT mode.
            }
			// In Normal mode control falls through and the dialog will just close.
        }		// end of EditvolunteerForm_FormClosing


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Read the Type Codes database table and display the data in a DataGridView control.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		//-----------------------------------------------------------------------------------------
		private void EditVolForm_Load (object sender, EventArgs e)
		{
			TableName			= "Volunteers";				// Read from this SQL table.
			ControlPage			= this.Controls;
			DataGridViewObject	= gridEditVol;
            editVol_cboRcdType.DataSource = CCFBGlobal.TypeCodesArray(CCFBGlobal.parmTbl_VolType);
            editVol_cboRcdType.DisplayMember = "LongName";
            editVol_cboRcdType.ValueMember = "UID";
            loadUserFieldLabels();
            LoadlvwGroups();
            RefreshGrid();
		}		// end of EditVolForm_Load

        private void RefreshGrid()
        {
            DbOpenToGrid("Volunteers", "SELECT * FROM Volunteers " + sWhereClause + sOrderBy);
            // The column property in a DataGridView does not expose the .Tag property. These
            // calls map the column in the DataSet to the column in the DataGridView. Note that
            // the "gridEditVol_RcdType" is NOT in the list since it is handled in the
            // "DisplayGridLineSpecial" method (requires TypeCode name lookup).
            SetGridColumn(COLUMN_ADDRESS, "gridEditVol_Address");
            SetGridColumn(COLUMN_CITY, "gridEditVol_City");
            SetGridColumn(COLUMN_ID, "gridEditVol_ID");
            //			SetGridColumn (COLUMN_INACTIVE,	"gridEditVol_InActive");
            SetGridColumn(COLUMN_NAME, "gridEditVol_Name");
            SetGridColumn(COLUMN_PHONE, "gridEditVol_Phone");
            //			SetGridColumn (COLUMN_RCD_TYPE,	"gridEditVol_RcdType"	);
            SetGridColumn(COLUMN_ZIPCODE, "gridEditVol_Zip");
            DisplayGrid();
            DisplayControls(STATE.DISPLAY);
        }
        ////-----------------------------------------------------------------------------------------
        ///// <summary>
        ///// Do some special handling to convert the row data to the display format.
        ///// </summary>
        ///// <param name="a_row">The row being processed.</param>
        ////-----------------------------------------------------------------------------------------
        public override void Event_SelectionChangedSpecial(int a_row)
        {
            tbVolunteerId.Text = DataSetValue(GridCurrentRow(), COLUMN_ID);
            loadVolunteerHours();
            clsVolunteers.getVolGrpsForVol(Convert.ToInt32(tbVolunteerId.Text));
            setActiveVolGroups();
        }		// end of Event_SelectionChangedSpecial


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Return the record ID for the selected record in the grid (used in SELECT mode). This
		/// value is only populated in the SELECT button.
		/// </summary>
		//-----------------------------------------------------------------------------------------
		public int SelectedId
		{
			get	{	return (m_selectedId);	}
			set {	m_selectedId = value;	}
		}		// end of property SelectedId


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// If FALSE then use the form in normal edit mode and if TRUE then operate in SELECT mode.
		/// </summary>
		//-----------------------------------------------------------------------------------------
		public bool FormSelectMode
		{
			get	{	return (m_formSelectMode);		}
			set	{	m_formSelectMode = value;		}
		}		// end of property FormSelectMode

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

        /// <summary>  Loads User Check Box Labels from UserFields Table
        /// </summary>
        private void loadUserFieldLabels()
        {
            UserFields clsUserFields = new UserFields(CCFBGlobal.connectionString);
            clsUserFields.open("Volunteers");
            clsUserFields.setDataRow("UserFlag0");
            if ("UserFlag0" == clsUserFields.FldName)
            {
                chkUserFlag0.Text = clsUserFields.EditLabel;
                chkUserFlag0.Visible = (chkUserFlag0.Text != "");
            }
            clsUserFields.setDataRow("UserFlag1");
            if ("UserFlag1" == clsUserFields.FldName)
            {
                chkUserFlag1.Text = clsUserFields.EditLabel;
                chkUserFlag1.Visible = (chkUserFlag1.Text != "");
            }
            clsUserFields.setDataRow("Date1");
            if ("Date1" == clsUserFields.FldName)
            {
                lblDate1.Text = clsUserFields.EditLabel;
                tbDate1.Visible = (lblDate1.Text != "");
                lblDate1.Visible = (lblDate1.Text != "");
            }
            clsUserFields.setDataRow("Date2");
            if ("Date2" == clsUserFields.FldName)
            {
                lblDate2.Text = clsUserFields.EditLabel;
                tbDate2.Visible = (lblDate2.Text != "");
                lblDate2.Visible = (lblDate2.Text != "");
            }
        }

        private void chkIncludeInactive_CheckedChanged(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void tbFindName_TextChanged(object sender, EventArgs e)
        {
            if (tbFindName.Text.Trim() == "")
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
                for (int i = rowStart; i < gridEditVol.Rows.Count; i++)
                {
                    if (gridEditVol.Rows[i].Cells[colNameFull].FormattedValue.ToString().ToUpper().StartsWith(lastSearchText) == true)
                    {
                        gridEditVol.CurrentCell = gridEditVol[0, i];
                        if (i < gridEditVol.FirstDisplayedScrollingRowIndex
                            || i > gridEditVol.Rows.GetLastRow(DataGridViewElementStates.Displayed) - 5)
                            if (i > 5)
                                gridEditVol.FirstDisplayedScrollingRowIndex = i - 5;
                            else
                                gridEditVol.FirstDisplayedScrollingRowIndex = i;
                        break;
                    }
                }
            }
        }

        private void setActiveVolGroups()
        {
            int[] groups = clsVolunteers.Groups;
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
                RefreshGrid();
            }
            if (sFilterColumn != "")
            {
                cboFilter.Visible = true; 
                getDistints(sFilterColumn); 
            }
            lblFilterBy.Visible = cboFilter.Visible;
        }

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
            clsVolunteers.getDistincts(colName, whereClause);
            string sVal = "";
            System.Collections.ArrayList typesVolunteer = CCFBGlobal.TypeCodesArray(CCFBGlobal.parmTbl_VolType);
            int iD = 0;
            for (int i = 0; i < clsVolunteers.RowCount; i++)
            {
                sVal = clsVolunteers.DSet.Tables[0].Rows[i][0].ToString();
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
                    cboFilter.Items.Add(sVal + new String((char)32, (30 - sVal.Length)) + "[ " + clsVolunteers.DSet.Tables[0].Rows[i][1].ToString() + " ]");
            }
            bLoadingCombo = false;
        }

        private void cboFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bLoadingCombo == false && sFilterColumn != "")
            {
                if (sFilterColumn == "RcdType")
                    sFilterValue = sFilterColumn + "=" + cboFilter.Text.Substring(0, cboFilter.Text.IndexOf("=") - 1);
                else
                    sFilterValue = sFilterColumn + "= '" + cboFilter.Text.Substring(0, 30).TrimEnd() + "'";
                SetWhereClause();
                RefreshGrid();
            }
        }

        private void SetWhereClause()
        {
            if (chkIncludeInactive.CheckState == CheckState.Unchecked)
            {
                sWhereClause = "WHERE Inactive = 0";
                if (sFilterColumn != "" && sFilterValue != "")
                    sWhereClause += "' AND '" + sFilterValue;
            }
            else
            {
                if (sFilterColumn != "" && sFilterValue != "")
                    sWhereClause = " WHERE " + sFilterValue;
                else
                    sWhereClause = "";
            }
        }

        private void loadVolunteerHours()
        {
            string sCurrMonthStart = DateTime.Today.Month.ToString() + "/01/" + DateTime.Today.Year.ToString();
            string sVolHrsWhereClause = "";
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
                    sVolHrsWhereClause = " WHERE TrxDate BETWEEN '" + dtpFrom.Text + "' AND '" + dtpTo.Text + "'" ;
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
                    Application.DoEvents();
                }

                btnDeleteVolHrs.Enabled = true;
            }
            else
            {
                btnDeleteVolHrs.Enabled = false;
            }
        }

        private void btnLoadCustom_Click(object sender, EventArgs e)
        {
            loadVolunteerHours();
        }

        private void lvwVol_Group_Leave(object sender, EventArgs e)
        {
            try
            {
                command = new SqlCommand("Delete From VolunteerGroups Where VolID= "
                    + gridEditVol.CurrentRow.Cells["gridEditVol_ID"].Value.ToString(), conn);
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }
            catch (SqlException ex)
            {
                CCFBGlobal.appendErrorToErrorReport("Command = " + command.CommandText,
                    ex.GetBaseException().ToString(), CCFBGlobal.serverName);
                return;
            }
            for (int i = 0; i < lvwVol_Group.Items.Count; i++)
            {
                if (lvwVol_Group.Items[i].Checked == true)
                {
                    try
                    {
                        command = new SqlCommand("Insert Into VolunteerGroups (VolID, GroupID)"
                            + "Values(" + gridEditVol.CurrentRow.Cells["gridEditVol_ID"].Value.ToString()
                            + ", " + lvwVol_Group.Items[i].Text + ")", conn);
                        conn.Open();
                        command.ExecuteNonQuery();
                        conn.Close();
                    }
                    catch (SqlException ex)
                    {
                        CCFBGlobal.appendErrorToErrorReport("Command = " + command.CommandText,
                            ex.GetBaseException().ToString(), CCFBGlobal.serverName);
                    }
                }
            }
        }

        private void cmsVolGrid_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            cmsVolGrid.Close();
            if (e.ClickedItem.Text == "Export To Excel")
            {
                CCFBGlobal.ExportToExcell(gridEditVol, "Volunteers_" +
                    DateTime.Today.Year.ToString() + "_" + 
                    CCFBGlobal.formatNumberWithLeadingZero(DateTime.Today.Month));
            }
        }

        private void lvwVol_Group_Enter(object sender, EventArgs e)
        {
            volGroupValues = new bool[lvwVol_Group.Items.Count];
            for (int i = 0; i < lvwVol_Group.Items.Count; i++)
            {
                volGroupValues[i] = lvwVol_Group.Items[i].Checked;
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (gridEditVol.CurrentRow.Index < gridEditVol.RowCount)
            {
                gridEditVol.CurrentCell = gridEditVol[0, gridEditVol.CurrentRow.Index + 1];
            }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (gridEditVol.CurrentRow.Index > 0)
            {
                gridEditVol.CurrentCell = gridEditVol[0, gridEditVol.CurrentRow.Index - 1];
            }

        }

        private void cboHoursPeriod_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cboHoursPeriod.Focused == true)
            {
                loadVolunteerHours();
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
        }

        private void dgvVolHours_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            stopEditMode = false;
            switch (e.ColumnIndex)
            {
                case 0:
                    {
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
                            else if (cellChangesMade == true && clsVolHours.checkExistingDateForVol(newDate, 
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

        private void btnAddNewVolHrs_Click(object sender, EventArgs e)
        {
            dgvVolHours.Rows.Add();
            dgvVolHours.CurrentCell = dgvVolHours[0, dgvVolHours.Rows.Count - 1];
            changeAddNewHrsButtons(true);
        }

        private void changeAddNewHrsButtons(bool addNewHrs)
        {
            btnAddNewVolHrs.Visible = !addNewHrs;
            btnDeleteVolHrs.Enabled = !addNewHrs;
            btnCancelNewHrs.Visible = addNewHrs;
        }

        private void btnCancelNewHrs_Click(object sender, EventArgs e)
        {
            changeAddNewHrsButtons(false);
            dgvVolHours.Rows.RemoveAt(dgvVolHours.Rows.Count -1);
        }

        private void btnDeleteVolHrs_Click(object sender, EventArgs e)
        {
            if (dgvVolHours.CurrentRow.Tag != null)
            {
                clsVolHours.delete(Convert.ToInt32(dgvVolHours.CurrentRow.Tag));
                loadVolunteerHours();
            }
        }

        private void dgvVolHours_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == 0)
                cellChangesMade = true;
        }
    }		// end of class
}		// end of namespace
