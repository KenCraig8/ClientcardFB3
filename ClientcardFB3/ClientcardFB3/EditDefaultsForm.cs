//*************************************************************************************************
//
// This class provides an interface to the ClientCardFB Defaults database table. This table
// defines preference and configuration values used through the application. The user can set the
// values through input forms. The "FldName" column in the Defaults table is the name of the input
// form used to view and modify the values.
//
//		Read (DbCredentials, tableName)			Read Defaults table to memory.
//		EditForm (defaultsFormName)				Display edit form and allow field maintenance.
//		SaveForm ()								Write the fields in edit form to the database.
//		Value (fieldName)						Get the value of the specified default field.
//		Value (fieldName, value)				Set the field to the specified value.
//
// Database Details:  The Defaults database normally consists of these fields:
//
//		FldName			The unique name of the field (must be unique across all Defaults forms).
//		FldVal			The value of this field (can be modified by the application or user).
//		EditForm		This field will be displayed to the user on this input form.
//		EditLabel		Label displayed with the input field.
//		EditTip			Description to be displayed when this field has focus on the input form.
//		FldType			"textbox" (date, integer, text), "combobox" ('|' separated item list).
//		ControlType		Valid types are: textbox, combobox, combo, 
//		ControlWidth	Width of the control in pixels (?), this must be converted to .NET format.
//
//
// Date			Version		Author				Description
// ----------	-------		------------------	--------------------------------------------------
// 2010-10-15	00.01.00	T. Cataldo			Consolidated various test methods into this class.
//
//*************************************************************************************************

using System;
using System.Collections.Generic;						// For List, etc.
using System.IO;										// For basic file I/O.
using System.Windows.Forms;



namespace ClientCardFB3
{
	partial class EditDefaultsForm : SqlGridDataSet
	{
		#region ----------Constants----------

		private const string COLUMN_ID				= "ID";					// Primary key.
		private const string COLUMN_FLD_NAME		= "FldName";			// Name of UI field on form.
		private const string COLUMN_FLD_VAL			= "FldVal";				// Value (user can change).
		private const string COLUMN_EDIT_FORM		= "EditForm";			// Display field on this form.
		private const string COLUMN_EDIT_LABEL		= "EditLabel";			// Field prompt string.
		private const string COLUMN_EDIT_TIP		= "EditTip";			// Field description.
		private const string COLUMN_FLD_TYPE		= "FldType";			// Text, Combobox, etc.
		private const string COLUMN_CONTROL_TYPE	= "ControlType";		// 
		private const string COLUMN_CONTROL_WIDTH	= "ControlWidth";		// No longer used.

		private const string COLUMN_GRID_ID			= "gridDefaults_ID";
		private const string COLUMN_GRID_FLD_NAME	= "gridDefaults_FldName";
		private const string COLUMN_GRID_FLD_VAL	= "gridDefaults_FldVal";
		private const string COLUMN_GRID_EDIT_LABEL	= "gridDefaults_EditLabel";
		private const string COLUMN_GRID_EDIT_TIP	= "gridDefaults_EditTip";

		private const string CONTROL_COMBOBOX		= "combobox";
		private const string CONTROL_NAME			= "ctl_Name";
		private const string CONTROL_TEXTBOX		= "textbox";
		#endregion


		#region ----------Variables----------

		/// <summary>
		/// All of the data in the SQL database table "Defaults".
		/// </summary>
		private Button btnDefaults_Close;
		private ComboBox cbo_DefaultsType;
		private GroupBox groupBox1;

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
		public EditDefaultsForm (string a_dbServer, string a_dbName, string a_dbUsername, string a_dbPassword)
		{
			InitializeComponent();
			ConnectionStringCreate (a_dbServer, a_dbName, a_dbUsername, a_dbPassword);
		}		// end of constructor


		public EditDefaultsForm (string a_connectionString)
		{
			InitializeComponent();
			ConnectionString = a_connectionString;
		}		// end of constructor


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		//-----------------------------------------------------------------------------------------
		private void btnSave_Click (object sender, EventArgs e)
		{
			string newValue = "";

			if (cbo_NewValue.Visible)
				newValue = cbo_NewValue.Text;
			else
				if (txt_NewValue.Visible)
					newValue = txt_NewValue.Text;

			if (newValue != null)
			{
				gridDefaults[COLUMN_GRID_FLD_VAL, GridCurrentRow()].Value = newValue;
				DataSetValue (-1, COLUMN_FLD_VAL, newValue);
				DbSave ();
			}
		}		// end of btnSave_Click


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Display a specific set of Defaults rows depending on the value in the ComboBox.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		//-----------------------------------------------------------------------------------------
		private void cbo_DefaultsType_SelectedIndexChanged (object sender, EventArgs e)
		{
			int gridRow = -1;								// Display data in this grid row.

			GridSelectionHandlerEnabled = false;			// Turn off grid events.
			gridDefaults.Rows.Clear();						// Start with clean grid control.
			for (int i=0;  i<DataSetCount;  i++)			// For all columns in the DataSet.
			{
				// If this DataSet row matches the currently selected TableName column.
				if (cbo_DefaultsType.Text == m_dataSet.Tables[TableName].Rows[i][COLUMN_EDIT_FORM].ToString())
				{
					gridDefaults.Rows.Add();				// Add a new grid row to current data.
					// The .OpenToGrid method adds the IDX_DATA_ROW column to the DataGridView and
					// this columns maps the grid row to the row in the DataSet.
					gridDefaults[SqlGridDataSet.COLUMN_IDX_DATA_SET,++gridRow].Value = i.ToString();

					DisplayGridLine (i, gridRow);			// Display DataSet data in new grid row.
				}
			}
			gridDefaults.Rows[0].Selected = true;			// Default to 1st row in grid.
			Event_SelectionChangedSpecial (0);				// Update controls for 1st grid row.
			GridSelectionHandlerEnabled = true;				// Turn ON the grid events.
		}		// end of cbo_DefaultsType_SelectedIndexChanged


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Display a list of EditForms from the DataSet.
		/// </summary>
		/// <param name="a_comboBox">Display the table names in this ComboBox.</param>
		//-----------------------------------------------------------------------------------------
		private void DisplayEditFormNames (ComboBox a_comboBox)
		{
			string tableName;								// Current TableName from DataSet.

			a_comboBox.Items.Clear ();						// Start with clean ComboBox.
			a_comboBox.Sorted = true;
			for (int i=0;  i<DataSetCount;  i++)
			{
				tableName = m_dataSet.Tables[TableName].Rows[i][COLUMN_EDIT_FORM].ToString();
				if (!a_comboBox.Items.Contains(tableName))	// Add table name if not in combobox
					a_comboBox.Items.Add (tableName);
			}
			if (a_comboBox.Items.Count > 0)					// If there are items in the list then
				a_comboBox.SelectedIndex = 0;				//   select the first item.
		}		// end of DisplayEditFormNames


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// If the data was changed then save those updates to the database.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		//-----------------------------------------------------------------------------------------
		private void EditDefaultsForm_btnSave_Click (object sender, EventArgs e)
		{
			if (DataSetHasChanges)							// If changes were made then save the
				DbSave ();									//   changes in the database.
			Close ();										// Close the dialog form.
		}		// end of EditDefaultsForm_btnSave_Click


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// If data has changed then prompt the user to save the data before closing the dialog.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		//-----------------------------------------------------------------------------------------
		private void EditDefaultsForm_FormClosing (object sender, FormClosingEventArgs e)
		{
			// The VS Designer get very confused if you try to call this event handler directly.
			Event_FormClosing (sender, e);
		}		// end of EditDefaultsForm_FormClosing


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		//-----------------------------------------------------------------------------------------
		private void EditDefaultsForm_Load (object sender, EventArgs e)
		{
			ControlPage			= this.Controls;
			DataGridViewObject	= gridDefaults;
			TableName			= "Defaults";

			if (!DbOpenToGrid (TableName, "SELECT * FROM " + TableName + " ORDER BY " + COLUMN_EDIT_LABEL))
			{
				MessageBox.Show ("Error opening the " + TableName + " DB table");
				return;
			}

			// The column property in a DataGridView does not expose the .Tag property. These
			// calls map the column in the DataSet to the column in the DataGridView. Note that
			// the "gridEditDonor_RcdType" is NOT in the list since it is handled in the
			// "DisplayGridLineSpecial" method (requires TypeCode name lookup).
			SetGridColumn (COLUMN_ID,			COLUMN_GRID_ID			);
			SetGridColumn (COLUMN_FLD_NAME,		COLUMN_GRID_FLD_NAME	);
			SetGridColumn (COLUMN_FLD_VAL,		COLUMN_GRID_FLD_VAL		);
			SetGridColumn (COLUMN_EDIT_LABEL,	COLUMN_GRID_EDIT_LABEL	);
			SetGridColumn (COLUMN_EDIT_TIP,		COLUMN_GRID_EDIT_TIP	);

			DisplayEditFormNames (cbo_DefaultsType);
		}		// end of EditDefaultsForm_Load


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Use this override method to do any special processing of the current record data. The
		/// normal event handler simply moves the data to the display fields but you may need to
		/// convert the record to another format in order for it to display properly.
		/// </summary>
		//-----------------------------------------------------------------------------------------
		public override void Event_SelectionChangedSpecial (int a_row)
		{
			string	controlType;
			int		dataSetRow = DatasetRowOfGridRow;

			controlType = DataSetValue (dataSetRow, COLUMN_CONTROL_TYPE);
            string fldType = DataSetValue(dataSetRow, COLUMN_FLD_TYPE);	// ComboBox list values;
            string fldValue = DataSetValue(dataSetRow, COLUMN_FLD_VAL);	// Value of field from DataSet.
            int fldWidth = Convert.ToInt32(DataSetValue(dataSetRow, COLUMN_CONTROL_WIDTH));
            grp_NewValue.Text = DataSetValue(dataSetRow, COLUMN_EDIT_LABEL);
            switch (controlType.ToLower())
			{
				case "combo":
				case "combobox":
				{
					txt_NewValue.Visible	= false;
					cbo_NewValue.Visible	= true;
                    grp_NewValue.Visible    = true;

					// The combo box list items are contained in a single string with the items separated by
					// a '|' character. If this string is defined then set the combo box list items.
					char[]	separators	= new char[] {'|'};			// List of separators for combo item list.
					cbo_NewValue.Items.Clear ();
					if (fldType.Length > 0)
						cbo_NewValue.Items.AddRange (fldType.Split (separators));

					// Define the contents of the drop list and the value in the text box part of the control.
                    cbo_NewValue.Width = fldWidth;
					cbo_NewValue.SelectedIndex = cbo_NewValue.FindString (fldValue);
					if (cbo_NewValue.SelectedIndex < 0)				// The value was not in the combo list
						cbo_NewValue.SelectedIndex = 0;				//   so default to 1st value in list.
					break;
				}

				case "text":
				case "textbox":
				{
					txt_NewValue.Visible	= true;
					cbo_NewValue.Visible	= false;
                    grp_NewValue.Visible    = true;
					txt_NewValue.Text		= DataSetValue (DatasetRowOfGridRow, COLUMN_FLD_VAL);
                    txt_NewValue.Width      = fldWidth;
					break;
				}

				default:
					MessageBox.Show (
						"Unknown control type (" + controlType + ")",
						"Control Error",
						MessageBoxButtons.OK,
						MessageBoxIcon.Error);
					break;
			}
			gridDefaults.Rows[GridCurrentRow()].Selected = true;
		}		// end of Event_SelectionChangedSpecial


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Get the EditForm string from the combo box and read all of the rows with that EditForm
		/// values. Each row read describes a field in the form. Display a dynamic form where each
		/// edit field is defined by a row read from the database.
		/// </summary>
		//-----------------------------------------------------------------------------------------
		public bool GenerateForm (string a_formName)
		{
#if false
			DeleteControls ();								// Clear any previous controls from form.
			for (int i=0;  i<m_defaults.Count;  i++)
			{
				if (a_formName == m_defaults.ColumnValue(COLUMN_EDIT_FORM, i))
					ControlAdd (
						m_defaults.ColumnValue(COLUMN_FLD_NAME,		i),	// Control name
						m_defaults.ColumnValue(COLUMN_EDIT_LABEL,	i),	// Control label
						m_defaults.ColumnValue(COLUMN_EDIT_TIP,		i),	// Edit tip
						m_defaults.ColumnValue(COLUMN_CONTROL_TYPE,	i),	// Control type
						m_defaults.ColumnValue(COLUMN_FLD_TYPE,		i),	// Field type
						400,											// X position (automatic)
						300,											// Y position
						100,    // Convert.ToInt32 (dynamicFields.GetColumnValue(DYNAMIC_CONTROL_WIDTH,i)),	// X length
						0,												// Y length (default)
						m_defaults.ColumnValue(COLUMN_FLD_VAL, i));		// Current field value.
			}
			return (CreateControls ());
#endif
	return (true);
		}		// end of GenerateForm


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Update the values in the dynamic form to the Defaults database.
		/// </summary>
		/// <returns>TRUE if the fields were updated, FALSE if an error occurred.</returns>
		//-----------------------------------------------------------------------------------------
		public bool Save ()
		{
#if false
			string controlValue;

			for (int i=0;  i<m_controls.Count;  i++)		// Check/Update all controls on form.
			{
				controlValue = Value (m_controls[i].controlName);
				ValueDb (m_controls[i].controlName, controlValue);
			}
#endif
			return (true);
		}		// end of Save


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Return the value of the specified field from the FldName column Defaults database
		/// table. The FldName column is the primary key and it contains the field names.
		/// </summary>
		/// <param name="a_fieldName">Return the value of this field.</param>
		/// <returns>Value of the specified FldName field (in string format).</returns>
		//-----------------------------------------------------------------------------------------
		public string ValueDb (string a_fieldName)
		{
#if false
			int idxRow;									// DB row with specified field name.

			// The field name (a_fieldName) is a value in the FldName column of the database. This
			// field is unique in the Defaults table. Find the row with the specified field name
			// and then we can easily read the value column in that row.
			idxRow = m_defaults.FindValueInColumn (COLUMN_FLD_NAME, a_fieldName);
			if (idxRow >= 0)							// If the fieldname is in the table...
				return (								// Return the value in the FldValue
					m_defaults.ColumnValue (			//   column for this DB row.
						COLUMN_FLD_VAL, idxRow));
#endif
			return ("");								// The field name was not in the list.
		}		// end of ValueDb


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Set the value of the specified field from the FldName column Defaults database table.
		/// The FldName column is the primary key and it contains the field names.
		/// </summary>
		/// <param name="a_fieldName">Set the value of this field.</param>
		/// <param name="a_value">Set the field to this value.</param>
		/// <returns>Value of the specified FldName field (in string format).</returns>
		//-----------------------------------------------------------------------------------------
		public bool ValueDb (string a_fieldName, string a_value)
		{
#if false
			// The field name (a_fieldName) is a value in the FldName column of the database. This
			// field is unique in the Defaults table. Verify that the field name is valid.
			if (m_defaults.FindValueInColumn (COLUMN_FLD_NAME, a_fieldName) >= 0)
			{
				// The update command is in this format:
				//		UPDATE Defaults SET FldVal='newValue' WHERE FldName='UseFamilyList';
				m_defaults.ExecuteCommand (
					"UPDATE "	+ DEFAULTS_TABLE_NAME	+
					" SET "		+ COLUMN_FLD_VAL		+ "='" + a_value	+ "'" +
					" WHERE "	+ COLUMN_FLD_NAME		+ "='" + a_fieldName+ "';");

				return (true);
			}
#endif
			return (false);								// The field name was not in the list.
		}		// end of ValueDb


		private void gridDefaults_KeyDown(object sender, KeyEventArgs e) {

		}		

		private void DummyFunction ()
		{
		}

        private void gridDefaults_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

	}		// end of class
}		// end of namespace