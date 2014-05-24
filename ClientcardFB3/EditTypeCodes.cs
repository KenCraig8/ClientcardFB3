//*************************************************************************************************
//
// ***** NOTE *****
// When defining the Type Code table you MUST set the Identity property of the ID key (primary
// key) to TRUE. This causes the ID field to be unique and auto-increment when a new row is added.
//
// TO DO:
// * validation is bypassed if another is clicked (works if TAB is used after edit).
// * SqlBase methods should be used to update row to database.
// * Remove duplication in the SqlBase and SqlList classes.
//
// Use this class to maintain the "Type Codes" tables in ClientCardFB. Using this class:
//
//		using SqlTools;
//
//		FrmTypeCodes dialog = new FrmTypeCodes (sqlDbServer, sqlDbName, sqlDbLogon, sqlDbPassword);
//			----------OR----------
//		FrmTypeCodes dialog = new FrmTypeCodes (sqlDbConnectionString);
//
//		dialog.ColorBackground = Color.PowderBlue;		// (optional) Set background color.
//		dialog.Show ();
//		if (dialog.DataChanged)							// If user updated Type Code values.
//			MessageBox.Show ("Reload combo boxes");
//
// The data is displayed in a DataGridView with a DELETE button on each row. These are other
// properties and characteristics of this class:
//
// * Type and ShortNames must be unique and the user is flagged if a duplicate value is entered.
// * The SortOrder value has unique characteristics:
//		*	When a row is deleted the remaining SortOrder values are reordered.
//		*	If a value is changed then the duplicate value is reordered. Example: If the SortOrder
//			values are 1, 2, 3, 4, 5 and #4 is changed to 2 then the old #2 is changed to 3, the
//			old 3 is changed to 4, and the old 5 remains unchanged.
// * The SAVE & CLOSE button saves the updated data to the database and closes the dialog.
// * The dialog cancel button (X button) prompts user to save if any data was changed.
// * The database table is not updated until the dialog is closed.
// * If a row is deleted then the SortOrder values greater than the current one are decremented.
// * If a SortOrder value is changed then the values following the new value are adjusted.
//
// The rows contain these columns (not necessarily in this order): (1) ID (primay key),
// (2) SortOrder, (3) Type (long Type name), and (4) ShortName. The type code tables are:
//
//			Type			DB Table Name
//			--------------  ------------------------------
//		Client Category	    parm_ClientType
//		Donor Category	    parm_DonorType
//		Language		    parm_LanguageType
//		Phone Type		    parm_PhoneType
//		Volunteer Category  parm_VolunteerTypes
//      Volunteer Group     parm_VolunteerGroups
//      Donation Type       parm_DonationType
//      Employment Status   parm_EmploymentStatus
//      Food Class          parm_FoodClass
//      ID Verification     parm_IDType
//      Military Discharge  parm_MilitaryDischarge
//      MilitaryService     parm_MilitaryService
//      FBProgram           parm_FBProgram
//      FBJobs              parm_FBJobs
//
//
// Date			Version		Author				Description
// ----------	-------		------------------	--------------------------------------------------
// 2010-10-20	00.01.00	T. Cataldo			Started TypeCodes class/namespace.
//
//*************************************************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ClientcardFB3
{
	public partial class EditTypeCodes : SqlGridDataSet
	{
		#region ----------Constants----------

		private const string BTN_TEXT_ADD			= "Add New";
		private const string BTN_TEXT_CANCEL		= "Cancel";
		private const string BTN_TEXT_DELETE		= "Delete";			// Text used on DELETE button.
		private const string BTN_TEXT_SAVE			= "Save";
		private const string BTN_TEXT_UNDELETE		= "Undelete";		// Text used on DELETE button.
		private const string BTN_TEXT_UPDATE		= "Update";

		private const string COLUMN_ID				= "ID";				// Primary key (unique).
		private const string COLUMN_SHORT_NAME		= "ShortName";		// Name used in ClientCard input forms.
		private const string COLUMN_SORT_ORDER		= "SortOrder";		// Sort order field.
		private const string COLUMN_TYPE			= "Type";			// Long name or description of the code.

		private const string COLUMN_GRID_ID			= "gridTypeCodes_Id";
		private const string COLUMN_GRID_SHORT_NAME	= "gridTypeCodes_ShortName";
		private const string COLUMN_GRID_SORT_ORDER	= "gridTypeCodes_SortOrder";
		private const string COLUMN_GRID_TYPE		= "gridTypeCodes_Type";
		private const string COLUMN_GRID_BUTTON		= "gridTypeCodes_BtnDelete";

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

		/// <summary>
		/// Value of ID column for new row. When saving the row the ID field will be excluded from
		/// the SQL INSERT command and the database will assign and store a unique ID value.
		/// </summary>
//		public const string	NEW_ROW_ID_TEMP		= "-1";
		#endregion


		#region ----------Variables----------

		/// <summary>
		/// Set this flag (in the CellBeginEdit handler) when a cell is being edited. This flag is
		/// used in the validation method so that it only checks cells that have been edited.
		/// </summary>
		private int m_editColumn = -1;
		//private DataGridViewCell m_editCell;
		private int m_editRow = -1;
		private string m_editOriginalValue = "";

		/// <summary>
		/// This flag is TRUE if any data in the grid was modified. It is used when closing the
		/// dialog to determine how to handle database updates.
		/// </summary>
		private bool m_dataChanged = false;

		/// <summary>
		/// The list of all Type Code lists. Select the proper Type Code list from a combo box.
		/// </summary>
		private string[] m_typeCodeList = 
        {
			"Client Category",
			"Donor Category",
            "Donation Type",
            "Food Class",
            "Id Verification",
            "Phone Type",
            "Special Language",
			"Volunteer Category",
            "Volunteer Groups",
            "Education Level",
            "Employment Status",
            "Military Discharge",
            "Military Service",
            "Food Bank Program",
            "Food Bank Jobs",
            "CSFP Routes",
            "Home Delivery Programs",
            "Home Delivery Building Operator",
            "HUD Income Category",
            "Transportation Method",
            "Race",
            "Relationship",
            "Service Method",
            "Service Groups",
            "Gender",
            "Address Verification ID"
        };

		/// <summary>
		/// This string array contains the database table names for each Type Code list.
		/// </summary>
		private string[] m_typeCodeTable = 
        {
			"parm_ClientType",
			"parm_DonorType",
            "parm_DonationType",
            "parm_FoodClass",
            "parm_IDType",
            "parm_PhoneType",
			"parm_LanguageType",
			"parm_VolunteerType",
			"parm_VolunteerGroups",
            "parm_EducationLevel",
            "parm_EmploymentStatus",
            "parm_MilitaryDischarge",
            "parm_MilitaryService",
            "parm_FBProgram",
            "parm_FBJobs",
            "parm_CSFPRoutes",
            "parm_FBProgram",
            "parm_HDBldgOperator",
            "parm_HUDCategory",
            "parm_Transportation",
            "parm_Race",
            "parm_Relationship",
            "parm_ServiceMethod",
            "parm_ServiceGroup",
            "parm_Gender",
            "parm_AddressID"
        };

        /// <summary>
        /// This string array contains the database table and field name for each Type Code list.
        /// </summary>
        private string[] m_typeCodeFields = 
        {
			"Household WHERE ClientType",
			"Donors WHERE RcdType",
            "FoodDonations WHERE DonationType",
            "FoodDonations WHERE FoodClass",
            "Household WHERE IdType",
            "Household WHERE PhoneType",
			"Household WHERE EthnicSpeaking",
			"Volunteers WHERE RcdType",
			"VolunteerGroups WHERE GroupID",
            "Demographics WHERE EducationLevel",
            "Demographics WHERE EmploymentStatus",
            "Demographics WHERE MilitaryDischarge",
            "Demographics WHERE MilitaryService",
            "TrxLog WHERE FBProgram",
            "VolunteerJobs WHERE JobId",
            "HouseholdMembers Where CSFPRoute",
            "Household Where HDProgram",
            "HDBuildings Where BldgOperator",
            "Household Where HUDCategory",
            "Household WHERE Transportation",
            "HouseholdMembers WHERE Race",
            "HouseholdMembers WHERE Relationship",
            "Household WHERE ServiceMethod",
            "TrxLog WHERE ServiceGroup",
            "HouseholdMembers WHERE Gender",
            "Household WHERE IdType"
        };

        /// <summary>
        /// This string array contains the database table and field name for each Type Code list.
        /// </summary>
        private string[] m_updatetypeCodeFields = 
        {
            "Household Set ClientType = xxx WHERE ClientType =",
            "Donors Set RcdType = xxx WHERE RcdType =",
            "FoodDonations DonationType = xxx WHERE DonationType =",
            "FoodDonations Set FoodCode = xxx WHERE FoodCode =",
            "Household Set IdType = xxx WHERE IdType =",
            "Household Set PhoneType = xxx WHERE PhoneType =",
            "Household Set EthnicSpeaking = xxx WHERE EthnicSpeaking =",
            "Volunteers Set RcdType = xxx WHERE RcdType =",
            "VolunteerGroups Set GroupID = xxx WHERE GroupID =",
            "Demographics Set EducationLevel = xxx WHERE EducationLevel = ",
            "Demographics Set EmploymentStatus = xxx WHERE EmploymentStatus = ",
            "Demographics Set MilitaryDischarge = xxx WHERE MilitaryDischarge =",
            "Demographics Set MilitaryService = xxx WHERE MilitaryService =",
            "TrxLog Set FBProgram = xxx WHERE FBProgram =",
            "VolunteerJobs Set JobId = xxx WHERE JobId =",
            "HouseholdMembers Set CSFPRoute = xxx WHERE CSFPRoute = ",
            "Household Set HDProgram = xxx WHERE HDProgram =",
            "Household Set HDBldgOperator = xxx WHERE HDBldgOperator =",
            "Household Set HUDCategory = xxx WHERE HUDCategory =",
            "Household Set Transportation = xxx WHERE Transportation =",
            "HouseholdMembers Set Race = xxx WHERE Race = ",
            "HouseholdMembers Set Relationship = xxx WHERE Relationship = ",
            "HouseholdMembers Set ServiceMethod = xxx WHERE ServiceMethod = ",
            "TrxLog Set ServiceGroup = xxx WHERE ServiceGroup =",
            "HouseholdMembers Set Gender = xxx WHERE Gender = ",
            "Household Set IDType = xxx WHERE IDType ="
        };

		#endregion


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// *****Constructor*****
		/// </summary>
		/// <param name="a_dbServer">Connect to the database on this server.</param>
		/// <param name="a_dbName">Connect to this database.</param>
		/// <param name="a_dbLogoin">User ID to connect to the SQL database.</param>
		/// <param name="a_dbPassword">Password to connect to the SQL database.</param>
		/// <param name="a_dbTable">Database table that contains the Type Codes.</param>
		/// <param name="a_title">Used for dialog title, etc.</param>
		//-----------------------------------------------------------------------------------------
		public EditTypeCodes (string a_dbConnectionString)
		{
			InitializeComponent();

			ConnectionString = a_dbConnectionString;
		}		// end of constructor


		public EditTypeCodes (string a_dbServer, string a_dbName, string a_dbUsername, string a_dbPassword)
		{
			InitializeComponent();

			ConnectionStringCreate (a_dbServer, a_dbName, a_dbUsername, a_dbPassword);
		}		// end of constructor


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Check if the current Type Code value (ID field) is used in any of the ClientCard
		/// database tables. We have not determined what to do if the Type Code is used but the
		/// hook is here.
		/// </summary>
		/// <returns></returns>
		//-----------------------------------------------------------------------------------------
		private int AreTypeCodesUsed ()
		{
            string queryText = " SELECT Count(*) FROM " + m_typeCodeFields[cboTypeCodes_TableName.SelectedIndex]
                + " = " + gridTypeCodes[COLUMN_GRID_ID, CurrentRow].FormattedValue.ToString();
            return ExecuteQuery(queryText);
		}		// end of AreTypeCodesUsed



		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Add the new Type Code (must be unique) to the grid view.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		//-----------------------------------------------------------------------------------------
		private void btn_Add_Click (object sender, EventArgs e)
		{
			int maxSortOrder = -1;						// Max SortOrder value in grid column.

			switch (btn_Add.Text)
			{
				case BTN_TEXT_ADD:
					// Find the maximum value for the SortOrder and increment that value and use it as the
					// default for the new row. Search the entire column because the user could have
					// manually changed sort order values.
					int sortOrderTemp;
					for (int i=0;  i<gridTypeCodes.Rows.Count;  i++)
					{
						sortOrderTemp = Convert.ToInt32(gridTypeCodes[COLUMN_GRID_SORT_ORDER,i].FormattedValue.ToString());
						if (sortOrderTemp > maxSortOrder)
							maxSortOrder = sortOrderTemp;
					}
                    txt_ID.Text = "";
					ClearFields ();									// Clear the edit fields.
					DisplayControls (STATE.ADD);
					txt_SortOrder.Text = (maxSortOrder + 1).ToString();				// SortOrder = max SortOrder + 1;
					txt_Type.Focus ();
					break;

				case BTN_TEXT_CANCEL:
                    txt_ID.Text = DataSetValue(CurrentRow, "ID");
                    MoveDataSetToDisplay(CurrentRow);
					DisplayControls (STATE.DISPLAY);
		// &&&&& re-display current line
					break;
			}

		}		// end of btn_Add_Click


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Clear the data in the Type Codes table and replace it with the updated data in the
		/// list structure. If no data was changed then leave the current table intact.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		//-----------------------------------------------------------------------------------------
		private void btn_Close_Click (object sender, EventArgs e)
		{
			if (m_dataChanged)								// If the data was modified then save it.
			{
				if (!Save())								// Some catastrophic database error
					MessageBox.Show (						//   occurred (cannot recover from this).
						"Error saving " + TableName + " type codes data!",
						"Save Error",
						MessageBoxButtons.OK,
						MessageBoxIcon.Error);
			}
			m_dataChanged = false;
			Close ();										// Close the dialog.
		}		// end of btn_Close_Click


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Update the edits made to the row data.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		//-----------------------------------------------------------------------------------------
		private void btn_Update_Click (object sender, EventArgs e)
		{
			switch (btn_Update.Text)
			{
				case BTN_TEXT_SAVE:
					RowInsert ();
					DbSave ();
                    refreshFromDB();
					DisplayControls (STATE.DISPLAY);
					break;

				case BTN_TEXT_UPDATE:
					MoveDisplayToDataSet ();
					DbSave ();
					DisplayControls (STATE.DISPLAY);
					break;
			}
		}		// end of btn_Update_Click


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Display the selected Type Code table and set the controls.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		//-----------------------------------------------------------------------------------------
		private void cboTypeCodes_TableName_SelectedIndexChanged (object sender, EventArgs e)
		{
			// If the user is changing tables but there are unsaved changes on the current table.
			if (m_dataChanged)
			{
				if (DialogResult.Yes == MessageBox.Show (
						"The " + this.Text + " Type Codes have been changed./nDo you want to save these changes?",
						"Save Type Codes",
						MessageBoxButtons.YesNoCancel,
						MessageBoxIcon.Question))
				{
					Save ();
				}
			}

			//********** Read the Type Codes from database **********
			TableName = m_typeCodeTable[cboTypeCodes_TableName.SelectedIndex];
            refreshFromDB();
			this.Text = "Edit " + cboTypeCodes_TableName.Text + " Type Codes";			// Set dialog title.
			//grp_Box.Text	= cboTypeCodes_TableName.Text;
			btn_Update.Enabled = false;						// Enabled when Add text box contains data.
		}		// end of cbo_SelectTable_SelectedIndexChanged


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Return the column of the currently selected grid cell (makes code easier to read).
		/// </summary>
		//-----------------------------------------------------------------------------------------
		private int CurrentColumn
		{
			get	{	return (gridTypeCodes.SelectedCells[0].ColumnIndex);	}
		}		// end of property CurrentColumn


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Return the row of the currently selected grid cell (makes code easier to read).
		/// </summary>
		//-----------------------------------------------------------------------------------------
		private int CurrentRow
		{
			get	{	return (gridTypeCodes.SelectedCells[0].RowIndex);	}
		}		// end of property CurrentRow


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Return the value in the currently selected grid cell (makes code easier to read).
		/// </summary>
		//-----------------------------------------------------------------------------------------
		private string CurrentCellValue
		{
			get	{	return (gridTypeCodes.SelectedCells[0].FormattedValue.ToString());	}
		}		// end of CurrentCellValue


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Returns TRUE if data was changed on the form. The calling code normally uses this
		/// property after the Dialog.Show. This class manages data used by other controls on the
		/// main application forms. After calling this class the application can check the
		/// DataChanged property to see if the controls on the main form need to be updated.
		/// </summary>
		//-----------------------------------------------------------------------------------------
		public bool DataChanged
		{
			get	{	return (m_dataChanged);		}
		}		// end of property DataChanged


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Set the state of the controls on the form.
		/// </summary>
		/// <param name="a_state"></param>
		//-----------------------------------------------------------------------------------------
		private void DisplayControls (STATE a_state)
		{
			switch (a_state)
			{
				case STATE.ADD:
					btn_Add.Enabled			= true;
					btn_Add.Text			= BTN_TEXT_CANCEL;
					btn_Update.Enabled		= true;
					btn_Update.Text			= BTN_TEXT_SAVE;
					gridTypeCodes.Enabled	= false;
					break;

				case STATE.DISPLAY:
					btn_Add.Enabled			= true;
					btn_Add.Text			= BTN_TEXT_ADD;
					btn_Update.Enabled		= true;
					btn_Update.Text			= BTN_TEXT_UPDATE;
					gridTypeCodes.Enabled	= true;
					break;

				case STATE.EDIT:
					btn_Add.Enabled			= true;
					btn_Add.Text			= BTN_TEXT_ADD;
					btn_Update.Enabled		= true;
					btn_Update.Text			= BTN_TEXT_UPDATE;
					gridTypeCodes.Enabled	= true;
					break;
			}		// end of switch
		}		// end of DisplayControls


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// This method adds the DELETE button when the base class displays a line in the 
		/// DataGridView.
		/// </summary>
		/// <param name="a_dataSetRow"></param>
		/// <param name="a_gridRow"></param>
		/// <returns></returns>
		//-----------------------------------------------------------------------------------------
		public override bool DisplayGridLineSpecial (int a_dataSetRow, int a_gridRow)
		{
			gridTypeCodes[COLUMN_GRID_BUTTON, a_gridRow].Value = BTN_TEXT_DELETE;
			return (true);
		}		// end of DisplayGridLineSpecial


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Close the form and prompt the user to save the data if it was modified.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		//-----------------------------------------------------------------------------------------
		private void EditTypeCodes_FormClosing (object sender, FormClosingEventArgs e)
		{
			if (m_dataChanged)
			{
				DialogResult result = MessageBox.Show (
					"All changes made to the Type Codes will be lost!\nAre you sure you want to exit?",
					"Exit Warning",
					MessageBoxButtons.YesNoCancel);
				if (result == DialogResult.Yes)				// YES to exit and NOT save data.
					m_dataChanged = false;					// Forget any changes that were made.
				else
					e.Cancel = true;						// Force execution to stay on this form.
//				e.Cancel = (result != DialogResult.Yes);	// TRUE cancels the close operation.
//				m_dataModified = e.Cancel;					// Cancel sets Modified to FALSE.
			}
		}		// end of EditTypeCodes_FormClosing


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Read the Type Codes database table and display the data in a DataGridView control.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		//-----------------------------------------------------------------------------------------
		private void EditTypeCodes_Load (object sender, EventArgs e)
		{
			ControlPage			= this.Controls;			// Current page with the controls.
			DataGridViewObject	= gridTypeCodes;			// Display Type Codes in this grid.

			// Populate Type Codes combobox, default to 1st item, and display the Type Code data.
			// The TypeCode table is opened and displayed in the SelectedIndexChanged event for
			// the Combobox and this event is fired when the SelectedIndex is set to zero.
			cboTypeCodes_TableName.Items.AddRange (m_typeCodeList);
			cboTypeCodes_TableName.SelectedIndex = 0;		// Set index & read/display table.
			DisplayControls (STATE.EDIT);
			gridTypeCodes.Focus ();
		}		// end of EditTypeCodes_Load


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Final dialog tweeks after all controls have been drawn.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		//-----------------------------------------------------------------------------------------
		private void EditTypeCodes_Shown (object sender, EventArgs e)
		{
			if (gridTypeCodes.Rows.Count <= 0)				// If the grid is empty then put focus
				txt_Type.Focus ();							//   on the ADD textbox.
		}		// end of EditTypeCodes_Shown


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Use this override method to do any special processing of the current record data. The
		/// normal event handler simply moves the data to the display fields but you may need to
		/// convert the record to another format in order for it to display properly.
		/// </summary>
		//-----------------------------------------------------------------------------------------
		public override void Event_SelectionChangedSpecial (int a_row)
		{
			txt_ID.Text = DataSetValue (a_row, "ID");
		}		// end of Event_SelectionChangedSpecial


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Search the column in the grid view object for the specified value. This is normally
		/// used to make sure the value is unique.
		/// </summary>
		/// <param name="columnName">The name of the DataGridView column to check.</param>
		/// <param name="a_value">Check the column for this value.</param>
		/// <returns>The row number where the value was found or -1 if the value was not found.</returns>
		//-----------------------------------------------------------------------------------------
		private int FindValueInColumn (string columnName, string a_value, string a_message)
		{
			for (int i=0; i<gridTypeCodes.Rows.Count; i++)	// Check all Type Codes in the grid.
			{
				if (a_value == gridTypeCodes[columnName, i].FormattedValue.ToString())
				{
					if (a_message.Length > 0)
						MessageBox.Show (
							"The " + a_message + " field must be unique!",
							"Duplicate Entry",
							MessageBoxButtons.OK,
							MessageBoxIcon.Information);
					return (i);
				}
			}
			return (-1);
		}		// end of FindValueInColumn


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		//-----------------------------------------------------------------------------------------
		private void grid_CellBeginEdit (object sender, DataGridViewCellCancelEventArgs e)
		{
			if (m_editColumn < 0)
			{
				m_dataChanged	= true;							// Flag that data has changed.
				m_editColumn	= CurrentColumn;
				m_editRow		= CurrentRow;
				m_editOriginalValue = gridTypeCodes[m_editColumn,m_editRow].FormattedValue.ToString ();
//				m_editCell = gridTypeCodes[m_editColumn,m_editRow];
			}
		}		// end of grid_CellBeginEdit


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Detect the DELETE button and delete the row from the grid and the database. Re-order
		/// the Sort Order values and decrement the values that are greater than the one deleted.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		//-----------------------------------------------------------------------------------------
		private void grid_CellClick (object sender, DataGridViewCellEventArgs e)
		{
            if (e.ColumnIndex == 4)
            {
                int row = CurrentRow;						// Current row in the grid.
                //			string	sortOrder;								// Sort Order value temp (used for renumbering).
                string sortOrderDeleted;						// Sort order value of the deleted row.
                string typeCodeName;							// The "Type" field from the DB.
                int NbrRows = 0;
                DialogResult retValue;
                //if (gridTypeCodes[e.ColumnIndex,e.RowIndex].ValueType 
                switch (gridTypeCodes[e.ColumnIndex, e.RowIndex].Value.ToString())
                {
                    case BTN_TEXT_DELETE:
                        typeCodeName = gridTypeCodes[COLUMN_GRID_TYPE, row].FormattedValue.ToString();
                        NbrRows = AreTypeCodesUsed();
                        if (NbrRows > 0)
                        {
                            retValue = MessageBox.Show("The type code '" + typeCodeName + "' has " + NbrRows.ToString()
                                + " entries int the database. \r\n"
                                + "Deleting this code will cause errors in the system.\r\n"
                                + "Press Yes to delete anyway.\r\n"
                                + "Press No to be prompted for a replacement code.\r\n"
                                + "Press Cancel to skip the delete process."
                                , "Delete Type Code Warning"
                                , MessageBoxButtons.YesNoCancel
                                , MessageBoxIcon.Question
                                , MessageBoxDefaultButton.Button2);
                            switch (retValue)
                            {
                                case DialogResult.Yes: //Delete anyway
                                    { break; }
                                case DialogResult.No:   //Prompt for new id
                                    {
                                        TypeCodeChangeForm frmChangeTypeCode = new TypeCodeChangeForm(TableName, 
                                            Convert.ToInt32(gridTypeCodes.CurrentRow.Cells[0].Value));
                                        frmChangeTypeCode.ShowDialog();

                                        if (frmChangeTypeCode.Canceled == false)
                                        {
                                            CCFBGlobal.executeQuery("Update " + m_updatetypeCodeFields[cboTypeCodes_TableName.SelectedIndex]
                                                .Replace("xxx", frmChangeTypeCode.SelectedID.ToString())
                                                + gridTypeCodes.CurrentRow.Cells[0].Value.ToString());
                                        }

                                        return;
                                    }
                                case DialogResult.Cancel: //Do nothing
                                    { return; }
                            }
                        }

                        sortOrderDeleted = gridTypeCodes[COLUMN_GRID_SORT_ORDER, row].FormattedValue.ToString();
                        if (DialogResult.OK == MessageBox.Show(
                                "Delete the \"" + typeCodeName + "\" Type Code?\r\n" +
                                    "Are you sure?",
                                "Delete Type Code",
                                MessageBoxButtons.OKCancel,
                                MessageBoxIcon.Question))
                        {
                            //						gridTypeCodes.Rows.RemoveAt (row);			// Remove the current line from grid.
                            string uid = gridTypeCodes[COLUMN_GRID_ID, e.RowIndex].Value.ToString();
                            if (CCFBGlobal.executeQuery("DELETE FROM " + TableName + " WHERE ID = " + uid) < 1)
                                MessageBox.Show("Deletion Failed");
                            else
                            {
                                if (m_dataChanged == true) 
                                    Save();

                                refreshFromDB();
                                m_dataChanged = false;
                            }

                        }
                        break;

                    case BTN_TEXT_UNDELETE:
                        gridTypeCodes[COLUMN_GRID_BUTTON, row].Value = BTN_TEXT_DELETE;
                        break;

                    default:
                        return;
                }		// end of switch
            }
		}		// end of grid_CellClick


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Replace the contents of the TypeCodes table with the contents of the m_typeCodes list.
		/// The m_typeCodes list will mirror the contents of the DataGridView although the grid
		/// data may be displayed in a different order (user can change sort order).
		/// </summary>
		/// <returns></returns>
		//-----------------------------------------------------------------------------------------
		private bool Save ()
		{
            DbSave();
#if false
			for (int i=0;  i<gridTypeCodes.Rows.Count;  i++)
			{
				// Move the values from the grid cell to the row buffer.
				ColumnValue (COLUMN_ID,			gridTypeCodes[0,i].FormattedValue.ToString());
				ColumnValue (COLUMN_TYPE,		gridTypeCodes[1,i].FormattedValue.ToString());
				ColumnValue (COLUMN_SHORT_NAME,	gridTypeCodes[2,i].FormattedValue.ToString());
				ColumnValue (COLUMN_SORT_ORDER,	gridTypeCodes[3,i].FormattedValue.ToString());

				// If the Type Code is to be deleted then leave the row in the database but mark
				// it as Deleted. In the future the user can reuse this Type Code and if this
				// value is used in another table it will show as "Deleted".
				if (gridTypeCodes[COLUMN_NO_DELETE,i].FormattedValue.ToString() == COLUMN_BTN_UNDELETE)
				{
					ColumnValue (COLUMN_TYPE,		"Deleted");
					ColumnValue (COLUMN_SHORT_NAME,	"---");
				}
				// If this row is to be added there is a special value for the ID. When the row is
				// INSERTed the database will assign a unique ID value.
				if (ColumnValue (COLUMN_ID) == NEW_ROW_ID_TEMP)
				{
					// If the user added and deleted the same row then do not write that row to
					// the database.
					if (gridTypeCodes[COLUMN_NO_DELETE,i].FormattedValue.ToString() != COLUMN_BTN_UNDELETE)
						RowInsert (true);						// Insert the new row w/Identity key.
				}
				else
					RowUpdate ();							// Update row in buffer to database.
			}
#endif
			return (true);
		}		// end of Save


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		//-----------------------------------------------------------------------------------------
		private void txt_Type_Leave (object sender, EventArgs e)
		{
			if (btn_Update.Text != BTN_TEXT_UPDATE)
			{
				// If the new Type Name string is not unique then display error message and leave.
				if (FindValueInColumn (COLUMN_GRID_TYPE, txt_Type.Text, "Type Name") >= 0)
					txt_Type.Focus ();
			}
		}		// end of txt_Type_Leave


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Enable the ADD button if this field contains data and disable it if there is no data.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		//-----------------------------------------------------------------------------------------
		private void txt_Add_TextChanged (object sender, EventArgs e)
		{
			btn_Update.Enabled = (txt_Type.Text.Length > 0);
		}		// end of txtTypeCodes_Add_TextChanged


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Validate the field at the CellEndEdit event. The SortOrder values be unique. If the
		/// value is not unique then reorder the list. For example, if there are 6 records:
		/// 
		///			1							1		change 2old to			1
		///			2							2old	3 and reorder			2new
		///			3      ==========>>			3		2old thru 6				3 (formerly 2old)
		///			4							4								4 (formerly 3)
		///			5 (user changes to 2)		2new   =================>>		5 (formerly 4)
		///			6							6								6 (unchanged)
		/// </summary>
		/// <returns></returns>
		//-----------------------------------------------------------------------------------------
		private bool Validate_SortOrder ()
		{
#if false
			string currentSortOrder;

			//                                           Sort Order value before the edit
			//                     Column # in grid      |                        No user messages
			//                     |                     |                        |
			if (FindValueInColumn (COLUMN_NO_SORT_ORDER, this.ActiveControl.Text, "") >= 0)
			{
				// There is a duplicate value for the sort order. Reorder the sort order values so
				// that the current entry is unique.
			}
#endif
			return (true);
		}		// end of Validate_SortOrder


		private void grid_CellEnter (object sender, DataGridViewCellEventArgs e)
		{
			ValidateEditedCell ();
		}		// end of grid_CellEnter


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		//-----------------------------------------------------------------------------------------
		private bool ValidateEditedCell ()
		{
#if false
			bool isValid = true;							// Status of the current cell data.

			if (m_editColumn >= 0)							// Set in CellBeginEdit event handler.
			{
				switch (m_editColumn)
				{
					case COLUMN_NO_ID:			break;
					case COLUMN_NO_TYPE:					// Make sure the Type Name value is unique.
						isValid = (FindValueInColumn(COLUMN_NO_TYPE, gridTypeCodes[m_editColumn,m_editRow].FormattedValue.ToString(), "Type Name") < 0);
						break;
					case COLUMN_NO_SHORT_NAME:				// Make sure the Short Name value is unique.
						isValid = (FindValueInColumn(COLUMN_NO_SHORT_NAME, gridTypeCodes[m_editColumn,m_editRow].FormattedValue.ToString(), "Short Name") < 0);
						break;
					case COLUMN_NO_SORT_ORDER:	isValid = Validate_SortOrder ();		break;
					case COLUMN_NO_DELETE:		break;								// DELETE button;
					default:
						// This error should NEVER occur;
						MessageBox.Show ("Invalid column value in Frm_TypeCodes:gridTypeCodes_CellEndEdit");
						break;
				}
				if (!isValid)
				{
				//	e.Cancel = true;						// Keeps focus on the current cell.
				//	this.ActiveControl.Text = gridTypeCodes[CurrentColumn,CurrentRow].FormattedValue.ToString ();
					gridTypeCodes.EndEdit ();
					gridTypeCodes.BeginEdit(false);			// Does not seem to do anything.
					gridTypeCodes[CurrentColumn,CurrentRow].Selected = false;
					gridTypeCodes[m_editColumn,m_editRow].Value = m_editOriginalValue;
					gridTypeCodes[m_editColumn,m_editRow].Selected = true;
				//	gridTypeCodes.BeginEdit (true);
					int row = CurrentRow;
					int column = CurrentColumn;
				MessageBox.Show ("(" + row + "," + column + ")");
					return (false);
				}
				m_editColumn= -1;
				m_editRow	= -1;
			}
			return (isValid);
#endif
		return (true);
		}		// end of ValidateEditedCell


		private void PlaceHolderMethodToPreserveFinalClosingBrace ()
		{
		}

		private void grid_CellLeave(object sender, DataGridViewCellEventArgs e)
		{

		}

        private void EditTypeCodes_SizeChanged(object sender, EventArgs e)
        {
            gridTypeCodes.Height = Height - gridTypeCodes.Top - 20;
            gridTypeCodes.Width = Width - gridTypeCodes.Left - 30;
        }

        private void refreshFromDB()
        {
            if (!DbOpenToGrid(TableName, "SELECT * FROM " + TableName + " ORDER BY " + COLUMN_SORT_ORDER))
            {
                MessageBox.Show(
                    "Unable to read the " + this.Text + " TypeCode data!",
                    TableName + " DB Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            // The column property in a DataGridView does not expose the .Tag property. These
            // calls map the column in the DataSet to the column in the DataGridView. Note that
            // the "gridEditDonor_RcdType" is NOT in the list since it is handled in the
            // "DisplayGridLineSpecial" method (requires TypeCode name lookup).
            SetGridColumn(COLUMN_ID, COLUMN_GRID_ID);
            SetGridColumn(COLUMN_SORT_ORDER, COLUMN_GRID_SORT_ORDER);
            SetGridColumn(COLUMN_TYPE, COLUMN_GRID_TYPE);
            SetGridColumn(COLUMN_SHORT_NAME, COLUMN_GRID_SHORT_NAME);

            // Display the data.
            DisplayGrid();
        }

	}		// end of class
}		// end of namespace
