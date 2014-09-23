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
//			Client			parm_ClientType
//			Donor			parm_DonorType
//			Language		parm_LanguageType
//			Phone			parm_PhoneType
//			Volunteer		parm_VolunteerTypes
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
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ClientCardFB3
{
	public partial class FrmTypeCodes : SqlList
	{
		#region ----------Constants----------

		public const string COLUMN_DELETE_BTN	= "Delete";			// Text used on DELETE button.
		public const string COLUMN_ID			= "ID";				// Primary key (unique).
		public const string COLUMN_SHORT_NAME	= "ShortName";		// Name used in ClientCard input forms.
		public const string COLUMN_SORT_ORDER	= "SortOrder";		// Sort order field.
		public const string COLUMN_TYPE			= "Type";			// Long name or description of the code.

		public const int	COLUMN_NO_ID		= 0;				// Primary key (unique).
		public const int	COLUMN_NO_TYPE		= 1;				// Long name or description of the code.
		public const int	COLUMN_NO_SHORT_NAME= 2;				// Name used in ClientCard input forms.
		public const int	COLUMN_NO_SORT_ORDER= 3;				// Sort order field.
		public const int	COLUMN_NO_DELETE	= 4;				// Delete button

		/// <summary>
		/// Value of ID column for new row. When saving the row the ID field will be excluded from
		/// the SQL INSERT command and the database will assign and store a unique ID value.
		/// </summary>
		public const string	NEW_ROW_ID_TEMP		= "-1";
		#endregion


		#region ----------Variables----------

		/// <summary>
		/// Set this flag (in the CellBeginEdit handler) when a cell is being edited. This flag is
		/// used in the validation method so that it only checks cells that have been edited.
		/// </summary>
		private bool m_cellEdited = false;

		/// <summary>
		/// Normal dialog background color.
		/// </summary>
		private Color m_colorBackground = Color.Cornsilk;

		/// <summary>
		/// Dialog color when in EDIT mode.
		/// </summary>
		private Color m_colorEdit = Color.LightGreen;

		/// <summary>
		/// This flag is TRUE if any data in the grid was modified. It is used when closing the
		/// dialog to determine how to handle database updates.
		/// </summary>
		private bool m_dataChanged = false;

		/// <summary>
		/// The list of all Type Code lists. Select the proper Type Code list from a combo box.
		/// </summary>
		private string[] m_typeCodeList = {
			"Client",
			"Donor",
			"Language",
			"Phone",
			"Volunteer" };

		/// <summary>
		/// The database tables for each Type Code list.
		/// </summary>
		private string[] m_typeCodeTable = {
			"parm_ClientType",
			"parm_DonorType",
			"parm_LanguageType",
			"parm_PhoneType",
			"parm_VolunteerType" };


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
		public FrmTypeCodes (string a_dbConnectionString)
		{
			InitializeComponent();

			ConnectionString = a_dbConnectionString;
		}		// end of constructor


		public FrmTypeCodes (string a_dbServer, string a_dbName, string a_dbUsername, string a_dbPassword)
		{
			InitializeComponent();

			CreateConnectionString (a_dbServer, a_dbName, a_dbUsername, a_dbPassword);
		}		// end of constructor


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Add the new Type Code (must be unique) to the grid view.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		//-----------------------------------------------------------------------------------------
		private void btnTypeCodes_Add_Click (object sender, EventArgs e)
		{
			// If the new Type Name string is not unique then display error message and leave.
			if (FindValueInColumn (COLUMN_NO_TYPE, txtTypeCodes_Add.Text, "Type Name") >= 0)
				return;
			// The table in SqlList is not used after it is ready but this is the easiest way to
			// create and populate a new row. All operations will be done to the grid and the data
			// will be written to the SQL database directly from the DataGridView object.
			int newRow = AddNewRow ();
			SetColumnValue (COLUMN_TYPE, newRow, txtTypeCodes_Add.Text);	// New text field.
			SetColumnValue (COLUMN_ID,   newRow, NEW_ROW_ID_TEMP);			// New ID (primary key);
			FormatGridRow (newRow);							// Add new row to grid and display it.
			txtTypeCodes_Add.Clear ();

			DataGridViewCell cell = gridTypeCodes.Rows[newRow].Cells[COLUMN_NO_SHORT_NAME];
			gridTypeCodes.CurrentCell = cell; 
			gridTypeCodes.BeginEdit (true);
			this.ActiveControl.Text = gridTypeCodes[COLUMN_NO_SHORT_NAME,newRow].FormattedValue.ToString();
		}		// end of btnTypeCodes_Add_Click


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Clear the data in the Type Codes table and replace it with the updated data in the
		/// list structure. If no data was changed then leave the current table intact.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		//-----------------------------------------------------------------------------------------
		private void btnTypeCodes_Close_Click (object sender, EventArgs e)
		{
			if (m_dataChanged)								// If the data was modified then save it.
			{
				if (!Save ())								// Some catastrophic database error
					MessageBox.Show (						//   occurred (cannot recover from this).
						"Error saving " + TableName + " type codes data!",
						"Save Error",
						MessageBoxButtons.OK,
						MessageBoxIcon.Error);
			}
			m_dataChanged = false;
			Close ();										// Close the dialog.
		}		// end of btnTypeCodes_Close_Click


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Display the selected Type Code table and set the controls.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		//-----------------------------------------------------------------------------------------
		private void cboTypeCode_Select_SelectedIndexChanged (object sender, EventArgs e)
		{
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
			this.Text = cboTypeCode_Select.Text;			// Set dialog title.
			TableName = m_typeCodeTable[cboTypeCode_Select.SelectedIndex];
			btnTypeCodes_Add.Enabled = false;				// Enabled when Add text box contains data.

			//********** Read the Type Codes from database **********
			if (ReadTable(TableName) < 1)
			{
				MessageBox.Show (
					"Unable to read the " + TableName + " Type Codes table",
					TableName + " DB Error",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
				return;
			}
			//********** Populate the Type Codes DataGridView **********
			gridTypeCodes.Rows.Clear();						// Start with cleared grid.
			for (int i=0;  i<Count;  i++)		// Display all type codes in the DB.
				FormatGridRow (i);
		}		// end of cboTypeCode_Select_SelectedIndexChanged


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Get/Set the color used for the normal dialog background.
		/// </summary>
		//-----------------------------------------------------------------------------------------
		public Color ColorBackground
		{
			get	{	return (m_colorBackground);		}
			set	{	m_colorBackground = value;		}
		}		// end of property ColorBackground


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Get/Set the color used for the normal dialog background.
		/// </summary>
		//-----------------------------------------------------------------------------------------
		public Color ColorEdit
		{
			get	{	return (m_colorEdit);		}
			set	{	m_colorEdit = value;		}
		}		// end of property ColorEdit


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
		/// Search the column in the grid view object for the specified value. This is normally
		/// used to make sure the value is unique.
		/// </summary>
		/// <param name="idxColumn">The index of the DataGridView column to check.</param>
		/// <param name="a_value">Check the column for this value.</param>
		/// <returns>The row number where the value was found or -1 if the value was not found.</returns>
		//-----------------------------------------------------------------------------------------
		private int FindValueInColumn (int idxColumn, string a_value, string a_message)
		{
			for (int i=0; i<gridTypeCodes.Rows.Count; i++)	// Check all Type Codes in the grid.
			{
				if (a_value == gridTypeCodes[idxColumn, i].FormattedValue.ToString())
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
		/// Format the DB table row in m_typeCodes into a grid view row.
		/// </summary>
		/// <param name="a_idxRow">Row in m_typeCodes list to format.</param>
		//-----------------------------------------------------------------------------------------
		private void FormatGridRow (int a_idxRow)
		{
			gridTypeCodes.Rows.Add (
				GetColumnValue (COLUMN_ID,			a_idxRow),	// Search key (hidden in grid).
				GetColumnValue (COLUMN_TYPE,		a_idxRow),
				GetColumnValue (COLUMN_SHORT_NAME,	a_idxRow),
				GetColumnValue (COLUMN_SORT_ORDER,	a_idxRow),
				COLUMN_DELETE_BTN);							// DELETE button.
		}		// end of FormatGridRow


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Close the form and prompt the user to save the data if it was modified.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		//-----------------------------------------------------------------------------------------
		private void FrmTypeCodes_FormClosing (object sender, FormClosingEventArgs e)
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
		}		// end of FrmTypeCodes_FormClosing


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Read the Type Codes database table and display the data in a DataGridView control.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		//-----------------------------------------------------------------------------------------
		private void FrmTypeCodes_Load (object sender, EventArgs e)
		{
			// Populate Type Codes combobox, default to 1st item, and display the Type Code data.
			cboTypeCode_Select.Items.AddRange (m_typeCodeList);
			cboTypeCode_Select.SelectedIndex = 0;
			cboTypeCode_Select_SelectedIndexChanged (null, null);

			this.BackColor = m_colorBackground;
		}		// end of FrmTypeCodes_Load


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Final dialog tweeks after all controls have been drawn.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		//-----------------------------------------------------------------------------------------
		private void FrmTypeCodes_Shown (object sender, EventArgs e)
		{
			if (gridTypeCodes.Rows.Count <= 0)
				txtTypeCodes_Add.Focus ();
		}		// end of FrmTypeCodes_Shown


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		//-----------------------------------------------------------------------------------------
		private void gridTypeCodes_CellBeginEdit (object sender, DataGridViewCellCancelEventArgs e)
		{
			m_dataChanged	= true;							// Flag that data has changed.
			m_cellEdited	= true;
		}		// end of gridTypeCodes_CellBeginEdit


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Detect the DELETE button and delete the row from the grid and the database. Re-order
		/// the Sort Order values and decrement the values that are greater than the one deleted.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		//-----------------------------------------------------------------------------------------
		private void gridTypeCodes_CellClick (object sender, DataGridViewCellEventArgs e)
		{
			int		row = CurrentRow;						// Current row in the grid.
			string	sortOrder;								// Sort Order value temp (used for renumbering).
			string	sortOrderDeleted;						// Sort order value of the deleted row.
			string	typeCodeName;							// The "Type" field from the DB.

			if (CurrentCellValue != COLUMN_DELETE_BTN)
				return;
			typeCodeName	= gridTypeCodes.Rows[row].Cells[COLUMN_NO_TYPE		].FormattedValue.ToString();
			sortOrderDeleted= gridTypeCodes.Rows[row].Cells[COLUMN_NO_SORT_ORDER].FormattedValue.ToString();
			if (DialogResult.Yes == MessageBox.Show (
					"Delete the \"" + typeCodeName + "\" Type Code?\n" +
						"Are you sure?",
					"Delete Type Code",
					MessageBoxButtons.YesNoCancel,
					MessageBoxIcon.Question))
			{
				gridTypeCodes.Rows.RemoveAt (row);			// Remove the current line from grid.
				m_dataChanged = true;
			}
			// De-select the DELETE button so that it will not appear highlighted.
			gridTypeCodes.Rows[row].Cells[COLUMN_NO_DELETE].Selected = false;

			// Adjust the sequencing of the Sort Order fields. Decrement all of the Sort Order
			// values that are greater than the one that was deleted.
			for (int i=0;  i<gridTypeCodes.Rows.Count;  i++)
			{
				sortOrder = gridTypeCodes.Rows[i].Cells[COLUMN_NO_SORT_ORDER].FormattedValue.ToString();
				if (sortOrder.CompareTo (sortOrderDeleted) > 0)
					gridTypeCodes [COLUMN_NO_SORT_ORDER, i].Value =		// Decrement the Sort Order
						(Convert.ToInt32(sortOrder) - 1).ToString ();	//   value of current cell.
			}
		}		// end of gridTypeCodes_CellClick


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// This method is called when focus moves away from a cell in the grid control. This event
		/// is triggered whether or not the data in the cell was modified. The CellBeginEdit is
		/// called on the first character of a cell being modified and the CellBeginEdit event
		/// handler sets the m_cellEdited flag. If this flag is set then the contents of the cell
		/// was modified and needs to be validated.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		//-----------------------------------------------------------------------------------------
		private void gridTypeCodes_CellValidating (object sender, DataGridViewCellValidatingEventArgs e)
		{
			bool isValid = true;							// Status of the current cell data.

			if (m_cellEdited)								// Set in CellBeginEdit event handler.
			{
				switch (gridTypeCodes.SelectedCells[0].ColumnIndex)
				{
					case COLUMN_NO_ID:			break;
					case COLUMN_NO_TYPE:					// Make sure the Type Name value is unique.
						isValid = (FindValueInColumn(COLUMN_NO_TYPE, this.ActiveControl.Text, "Type Name") < 0);
						break;
					case COLUMN_NO_SHORT_NAME:				// Make sure the Short Name value is unique.
						isValid = (FindValueInColumn(COLUMN_NO_SHORT_NAME, this.ActiveControl.Text, "Short Name") < 0);
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
			string test1 = this.ActiveControl.Text;
					e.Cancel = true;						// Keeps focus on the current cell.
					this.ActiveControl.Text = gridTypeCodes[CurrentColumn,CurrentRow].FormattedValue.ToString ();
					gridTypeCodes[CurrentColumn, CurrentRow].Selected = true;
					gridTypeCodes.BeginEdit (true);
				}
				m_cellEdited = false;
			}
		}		// end of gridTypeCodes_CellValidating


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
			for (int i=0;  i<gridTypeCodes.Rows.Count;  i++)
			{
				// Move the values from the grid cell to the row buffer.
				ColumnValue (COLUMN_ID,			gridTypeCodes[0,i].FormattedValue.ToString());
				ColumnValue (COLUMN_TYPE,		gridTypeCodes[1,i].FormattedValue.ToString());
				ColumnValue (COLUMN_SHORT_NAME,	gridTypeCodes[2,i].FormattedValue.ToString());
				ColumnValue (COLUMN_SORT_ORDER,	gridTypeCodes[3,i].FormattedValue.ToString());
				if (ColumnValue (COLUMN_ID) == NEW_ROW_ID_TEMP)
					RowInsert (true);						// Insert the new row w/Identity key.
				else
					RowUpdate ();							// Update row in buffer to database.
			}
			return (true);
		}		// end of Save


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Enable the ADD button if this field contains data and disable it if there is no data.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		//-----------------------------------------------------------------------------------------
		private void txtTypeCodes_Add_TextChanged (object sender, EventArgs e)
		{
			btnTypeCodes_Add.Enabled = (txtTypeCodes_Add.Text.Length > 0);
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
			string currentSortOrder;

			//                                           Sort Order value before the edit
			//                     Column # in grid      |                        No user messages
			//                     |                     |                        |
			if (FindValueInColumn (COLUMN_NO_SORT_ORDER, this.ActiveControl.Text, "") >= 0)
			{
				// There is a duplicate value for the sort order. Reorder the sort order values so
				// that the current entry is unique.
			}
			return (true);
		}		// end of Validate_SortOrder


		private void PlaceHolderSoLastEndingBraceRemainIntact ()
		{
		}

	}		// end of class
}		// end of namespace
