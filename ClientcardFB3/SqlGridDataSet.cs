// TO DO:
//
// * Cursor up/down through GridView.
// * Search field for grid searches

#if false
       public bool insert()
        {
            if (dadAdpt.UpdateCommand == null || dadAdpt.InsertCommand == null)
            {
                SqlCommandBuilder commBuild = new SqlCommandBuilder(dadAdpt);
            }
            try
            {
                openConnection();
                dadAdpt.Update(dset, tbName);
                closeConnection();
                return true;
            }
            catch (SqlException ex)
            {
                closeConnection();
                GlobalVariables.appendErrorToErrorReport("", ex.GetBaseException().ToString(),
                    GlobalVariables.serverName);
                return false; 
            }
        }
#endif
//*************************************************************************************************
//
//	*	Derive your user interface class from the SqlGridDataSet class, for example:
//
//				public class MyUserInputClass : SqlGridDataSet {  /* put class code here */  }
//
//	*	Use ConnectionString or ConnectionStringCreate to set the connection string for the SQL
//		database. See the notes on ConnectionString and ConnectionStringCreate(...) for details.
//
//	*	Set DataGridViewObject to the name of the DataGridView control used to display the data.
//
//	*	(optional in next cut) Use ControlPage to set the name of the form that has the user
//		fields. If none is specified then the "this" form is used.
//
//	*	Map each UI field to the database column that it will display. Add the database column
//		name to the .Tag property of the UI field using this format: d={dbColumnName}. The class
//		searches the controls on the form and builds a mapping table that allows the class to
//		automatically move data between the input fields and the columns in the DataSet.
//
//	*	(optional) Connect the FormClosing event of the form to Event_FormClosing to use the
//		standard form closing logic (prompt user to save with Yes/No/Cancel logic).
//		NOTE:	The designer will not display Event_FormClosing in the method drop list for the
//				FormClosing event of the dialog.
//
//=================================================================================================
//
// Date			Version		Author				Description
// ----------	-------		------------------	--------------------------------------------------
// 2010-11-06	00.01.00	T. Cataldo			Started initial version of this class.
//
//*************************************************************************************************

using System;												// For Exception, etc.
using System.Collections.Generic;							// For List<>, etc.
using System.Data;											// For DataSet, etc.
using System.Data.SqlClient;								// For SqlConnection, SqlDataAdapter, etc.
using System.Diagnostics;									// For Debug.Assert, etc.
using System.Windows.Forms;



namespace ClientcardFB3
{
	public class SqlGridDataSet : SqlBaseDataSet
	{
		#region ----------Constants----------

		/// <summary>
		/// This is the name of the column that is added to the DataGridView used to display the
		/// UI fields. This field is populated with the record number in the DataSet so that the
		/// proper DataSet record can be accessed easily if the grid is reordered.
		/// </summary>
		public const string COLUMN_IDX_DATA_SET = "IdxDataSet";

		private const string CONTROL_VALUE_FALSE	= "False";
		private const string CONTROL_VALUE_TRUE		= "True";
        public const string OURNULLDATE = "01/01/1900";
        #endregion

        int eventCounter = 0;
		#region ----------Variables----------

        private bool alreadyHere = false;
		/// <summary>
		/// Normal dialog background color.
		/// </summary>
		private System.Drawing.Color m_colorBackground = System.Drawing.Color.Cornsilk;

		/// <summary>
		/// Dialog background color when editing a record if the application uses a different
		/// background color when editing.
		/// </summary>
		private System.Drawing.Color m_colorEdit = System.Drawing.Color.LightGreen;

		/// <summary>
		/// This is the page on which the controls and fields are displayed. This can be a dialog
		/// box, tab control page, etc. You must set this value in order to have this class perform
		/// I/O on the data fields.
		/// </summary>
		private System.Windows.Forms.Control.ControlCollection m_controlPage;

		/// <summary>
		/// Display the database row fields in this DataGridView control.
		/// </summary>
		private System.Windows.Forms.DataGridView m_dataGrid = null;

		/// <summary>
		/// If TRUE then the entire grid row is a selected when a cell is selected.
		/// </summary>
		private bool m_displayEntireGridRow = true;

		/// <summary>
		/// TRUE if the record fields are enabled for editing and FALSE if they are disabled.
		/// </summary>
		private bool m_editEnabled = false;

		/// <summary>
		/// Number of controls on the form that map to database columns. DisplayGrid uses this
		/// value to optomize performance when there are no input controls on the form (such as
		/// when only a grid is used to display the database columns).
		/// </summary>
		private int m_noFormControls = 0;

		#endregion


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Clear all of the user fields on the current input form.
		/// </summary>
		//-----------------------------------------------------------------------------------------
		public void ClearFields ()
		{
			ClearFields ("");
		}		// end of ClearFields


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Set all of the user fields on the current input form to the specific value. Normally
		/// you test your field definitions by setting the fields to a non-blank value to make sure
		/// that the fields were defined properly.
		/// </summary>
		/// <param name="a_value">Set all fields on the page to this value.</param>
		//-----------------------------------------------------------------------------------------
		public void ClearFields (string a_value)
		{
			// Clear the fields in reverse order so that the index will be on the first field at the
			// end of the loop. Then put focus on the first control (if it is valid).
			for (int i=0;  i<m_map.Count;  i++)				// Clear each data field object on the
			{												//   the current tab control.
				SetControlValue (m_map.m_uiControl[i], a_value);
			}
// &&&&& fix this later
//			m_map.m_uiControl[0].Focus();					// Put focus on 1st field if it is valid.
		}		// end of ClearFields


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Get/Set the background color for the normal dialog.
		/// </summary>
		//-----------------------------------------------------------------------------------------
		public System.Drawing.Color ColorBackground
		{
			get	{	return (m_colorBackground);		}
			set	{	m_colorBackground = value;		}
		}		// end of ColorBackground


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Get/Set the background color when in EDIT mode.
		/// </summary>
		//-----------------------------------------------------------------------------------------
		public System.Drawing.Color ColorEdit
		{
			get	{	return (m_colorEdit);		}
			set	{	m_colorEdit = value;		}
		}		// end of ColorEdit


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Set or get the current control page. This is the page on which the data and controls
		/// are displayed. You must set this value to use any of the functions for displaying or
		/// saving the user fields. The control page can be a dialog box, tab page, etc.
		/// </summary>
		//-----------------------------------------------------------------------------------------
		public Control.ControlCollection ControlPage
		{
			get	{	return (m_controlPage);		}
			set {	m_controlPage = value;		}
		}		// end of ControlPage


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Get the value of the specified control and return it as a string. Valid control types:
		/// 
		///			Control				Value Returned
		///			-----------			----------------------------------------------
		///			CheckBox			"True" if checked, "False" if not checked.
		///			ComboBox			Text in edit field of ComboBox.
		///			TextBox				Text in TextBox.
		///			
		/// </summary>
		/// <param name="a_value">Set all fields on the page to this value.</param>
		//-----------------------------------------------------------------------------------------
		public string GetControlValue (Control a_control)
		{
			if (a_control == null)							// If this is null then .Find will
				return ("");								//   throw an exception.
			switch (a_control.GetType().Name.ToLower())
			{
				case "checkbox":
					CheckBox chkBox = (CheckBox) a_control;
					if (chkBox.Checked)
						return (CONTROL_VALUE_TRUE);
					return (CONTROL_VALUE_FALSE);
				case "combobox":
                    parmType pt = (parmType)((ComboBox)a_control).SelectedItem;
                    return (pt.ID.ToString());
				case "textbox":		
                    if (a_control.Name.Contains("Date"))
                    {
                        if (String.IsNullOrEmpty(a_control.Text) == true)
                            return CCFBGlobal.OURNULLDATE;
                        else
                        {
                            try
                            {
                                DateTime testDate = Convert.ToDateTime(a_control.Text);
                                if (testDate <= CCFBGlobal.FBNullDateValue)
                                    return CCFBGlobal.OURNULLDATE;
                                else
                                    return a_control.Text;
                            }
                            catch { return CCFBGlobal.OURNULLDATE; }
                        }
                    }
                    return (a_control.Text);
                default:
                    return "";
			}
            //MessageBox.Show (
            //    "Unknown control type (" + a_control.GetType().Name + " in dbGridView:FieldValue",
            //    "Control Error",
            //    MessageBoxButtons.OK,
            //    MessageBoxIcon.Information);
            //return ("");
		}		// end of ControlValue

		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Set the value of the specified control.
		/// </summary>
		/// <param name="a_value">Set all fields on the page to this value.</param>
		//-----------------------------------------------------------------------------------------
		public bool SetControlValue (Control a_control, string a_value)
		{
			bool status = true;								// TRUE=value set, FALSE = error.

			if (a_control == null)							// If this is null then .Find will
				return (false);								//   throw an exception.
			switch (a_control.GetType().Name.ToLower())
			{
				case "checkbox":
					CheckBox chkBox = (CheckBox) a_control;
					chkBox.Checked  = !(a_value=="0"  ||  a_value.ToLower()=="false");
					break;
				case "combobox":
                    ComboBox cb = (ComboBox)a_control;
                    if (String.IsNullOrEmpty(a_value) == true)
                    {
						if (cb.Items.Count > 0)
							cb.SelectedIndex = 0;
						break;
                    }
                    int testId = Convert.ToInt16(a_value);
                    for (int i = 0; i < cb.Items.Count; i++)
                    {
                        if (((parmType)cb.Items[i]).ID == testId)
                        {
                            cb.SelectedIndex = i;
                            break;
                        }
                    }
		            break;
				case "textbox":
	                if (a_control.Tag.ToString().Contains("Date") && a_value.Length >0)
                    { 
                        DateTime testDate = Convert.ToDateTime(a_value);
                        if (testDate <= CCFBGlobal.FBNullDateValue)
                            a_control.Text = "";
                        else
                            a_control.Text = testDate.ToShortDateString();
                    }
                    else
                        a_control.Text = a_value;		
                    break;
				default:
					status = false;
					MessageBox.Show (
						"Unknown control type (" + a_control.GetType().Name + " in dbGridView:FieldValue",
						"Control Error",
						MessageBoxButtons.OK,
						MessageBoxIcon.Information);
					break;
			}
			return (status);
		}		// end of ControlValue


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// If set then the entire DataGridView row is selected when any cell in that row is
		/// selected. If not set then the cells in the DataGridView are selected individually. This
		/// property defaults to TRUE if there are UI fields mapped to one or more columns in the
		/// DataSet. This property defaults to FALSE if no UI fields are mapped to the DataSet
		/// columns. The application can override these settings after the .Open call.
		/// </summary>
		//-----------------------------------------------------------------------------------------
		public bool DisplayEntireGridRow
		{
			get	{	return (m_displayEntireGridRow);	}
			set	{	m_displayEntireGridRow = value;		}
		}		// end of DisplayEntireGridRow


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Get/Set the value of the DataGridView object used to display the database data (or a
		/// subset of the DB data)
		/// </summary>
		//-----------------------------------------------------------------------------------------
		public System.Windows.Forms.DataGridView DataGridViewObject
		{
			get	{	return (m_dataGrid);	}
			set
			{
				m_dataGrid = value;
				m_dataGrid.MultiSelect = false;
			}
		}		// end of property DataGridViewObject

        public string DataSetColumnofGridCol(int colGrid)
        {
            for (int i = 0; i < m_map.Count; i++)
            {
 
            }
            return "";
        }

		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Get the DataSet row for the current DataGridView row. A hidden column (IDX_DATA_SET)
		/// was added to the DataGridView with the DataSet was displayed. The IDX_DATA_SET column
		/// contains the index into the DataSet for the DataGridView row. Use the value in this
		/// column to access the correct DataSet record regardless of the sort used in the grid.
		/// </summary>
		//-----------------------------------------------------------------------------------------
		public int DatasetRowOfGridRow
		{
			get	{	return (Convert.ToInt32 (m_dataGrid[COLUMN_IDX_DATA_SET,GridCurrentRow()].Value));	}
		}		// end of property GridDataSetRow


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Return the value from the DataSet of the specified column of the DataGridView record.
		/// </summary>
		/// <param name="a_row">Retrieve data from this row in the DataSet (-1 to retrieve data
		/// from the DataSet row that corresponds to the current row in the DataGridView).</param>
		/// <param name="a_column">Return the value of the column in current row.</param>
		/// <returns></returns>
		//-----------------------------------------------------------------------------------------
		public string DataSetValue (int a_row, string a_column)
		{
			if (a_row < 0)
				a_row = DatasetRowOfGridRow;
			return CCFBGlobal.Validate(m_dataSet.Tables[TableName].Rows[a_row][a_column]);
		}		// end of DataSetValue


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Set the value from the DataSet of the specified column of the DataGridView record.
		/// </summary>
		/// <param name="a_row">Set data in this row in the DataSet (-1 to retrieve data
		/// from the DataSet row that corresponds to the current row in the DataGridView).</param>
		/// <param name="a_column">Set the value of the column in a_row.</param>
		/// <returns></returns>
		//-----------------------------------------------------------------------------------------
		public void DataSetValue (int a_row, string a_column, string a_value)
		{
			if (a_row < 0)
				a_row = DatasetRowOfGridRow;
			m_dataSet.Tables[TableName].Rows[a_row][a_column] = a_value;
		}		// end of DataSetValue

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Set the value from the DataSet of the specified column of the DataGridView record.
        /// </summary>
        /// <param name="a_row">Retrieve data from this row in the DataSet (-1 to retrieve data
        /// from the DataSet row that corresponds to the current row in the DataGridView).</param>
        /// <param name="a_column">Return the value of the column in current row.</param>
        /// <returns></returns>
        //-----------------------------------------------------------------------------------------
        public void DataSetValue(int a_row, int a_column, string a_value)
        {
            if (a_row < 0)
                a_row = DatasetRowOfGridRow;
            m_dataSet.Tables[TableName].Rows[a_row][m_map.m_dbColumn[a_column]] = a_value;
        }		// end of DataSetValue


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Open the database table and read all rows from the database.
		/// </summary>
		/// <param name="a_tableName">Read the data from this database table.</param>
		/// <param name="a_sqlQueryString">Query string used to exact data from database.</param>
		/// <returns></returns>
		//-----------------------------------------------------------------------------------------
		public bool DbOpenToGrid (string a_tableName)
		{
			TableName = a_tableName;
			return (DbOpenToGrid (TableName, "SELECT * FROM " + TableName));
		}		// end of DbOpenToGrid


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Open the database table using the given query string.
		/// </summary>
		/// <param name="a_tableName">Read the data from this database table.</param>
		/// <param name="a_sqlQueryString">Query string used to exact data from database.</param>
		/// <returns></returns>
		//-----------------------------------------------------------------------------------------
		public bool DbOpenToGrid (string a_tableName, string a_sqlQueryString)
		{
			bool status = false;

			Debug.Assert (									// For debugging during development.
				(m_dataGrid != null),
				"Must set the DataGridView using \"DataGridViewObject\" prior to calling \"DbOpenToGrid\"");
			try
			{
				GridSelectionHandlerEnabled = false;
				if (!DbOpen (a_tableName, a_sqlQueryString))
					return (false);

	// &&&&& put in the proper checks here.
				// Add a new column to the DataGridView and this column will store the row number
				// of the corresponding record in the DataSet.
				DataGridViewColumn		idxColumn = new DataGridViewColumn ();
				DataGridViewTextBoxCell newCell	  = new DataGridViewTextBoxCell ();
				idxColumn.Name			= COLUMN_IDX_DATA_SET;
				idxColumn.HeaderText	= COLUMN_IDX_DATA_SET;
				idxColumn.Visible		= false;
				idxColumn.CellTemplate	= newCell;
				m_dataGrid.Columns.Add (idxColumn);

				// Search the .Tag properties in the control set for this form. If there is a
				// d={name} field then the {name} is the name of the database column that maps to
				// the display control. The call below set all of the DB<->Display mappings.
				MapDbColumnsToUiFields (Controls);

				DisplayEntireGridRow = (m_noFormControls > 0);
				status = true;
			}
			catch (Exception ex)
			{
				LastError = ex.Message + "(SqlGridDataSet:DbOpen)";
			}
			GridSelectionHandlerEnabled = true;
			return (status);
		}		// end of DbOpenToGrid


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Display the data in the DataSet in the DataGridView. Note that the DataGridView column
		/// properties do not expose the .Tag property. This property must be set using the
		/// SetGridColumn method to map each database column to a column in the DataGridView.
		/// </summary>
		//-----------------------------------------------------------------------------------------
		public bool DisplayGrid ()
		{
			Debug.Assert (									// For debugging during development.
				(m_dataGrid != null),
				"Must set the DataGridView using \"DataGridViewObject\" prior to calling \"DisplayGrid\"");
			GridSelectionHandlerEnabled = false;			// Turn OFF the grid events.
//			try
			{
				m_dataGrid.Rows.Clear ();					// Start with a clean grid.
				for (int row=0;  row<DataSetCount;  row++)	// Loop for all rows in the DataSet.
				{
					m_dataGrid.Rows.Add ();					// Append an empty row to the grid.
					// The IDX_DATA_ROW column was added by this class and this is used to map the
					// DataGridView row to the row in the DataSet.
					m_dataGrid[COLUMN_IDX_DATA_SET,row].Value = row;
					// When reading about 450 records it takes about 2.2 when using the method
					// call and about 2 seconds when the code is in-line. Therefore, I opted to
					// use the inline code for speed considerations.
#if false
					DisplayGridLine (row);
#else
					for (int i=0;  i<m_map.Count;  i++)		// Loop for all columns in the map.
					{
						if (m_map.m_gridColumn[i] != null)	// If grid column is defined...
						{
							// Set the specified column in the current grid row to the value in
							// the corresponding column of the DataSet.
                            try
                            {
                                m_dataGrid[m_map.m_gridColumn[i], row].Value =
                                    CCFBGlobal.Validate(m_dataSet.Tables[TableName].Rows[row][m_map.m_dbColumn[i]]);
                            }
                            catch (Exception ex)
                            {
                            }
						}
					}
					DisplayGridLineSpecial (row, row);
                   // Application.DoEvents();
#endif
				}
				Event_GridSelectionChanged (null, null);	// Display fields for 1st grid row.
				GridSelectionHandlerEnabled = true;			// Turn grid events ON.
				return (true);
			}
            //catch (Exception ex)
            //{
            //    MessageBox.Show ("DisplayGrid Error: " + ex.Message);
            //    LastError = ex.Message;
            //    return (false);
            //}			
		}		// end of DisplayGrid


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Display the DataGridView row with the data in the specified row in the DataSet. Use
		/// this method when there is a one-to-one relationsship between the rows in the DataSet
		/// and the rows in the DataGridView.
		/// </summary>
		/// <param name="a_row">Display the data in this row of the DataSet to the same row of the
		/// DataGridView.</param>
		/// <returns>TRUE if the data was display, FALSE if an error occurred.</returns>
		//-----------------------------------------------------------------------------------------
		public bool DisplayGridLine (int a_row)
		{
			return (DisplayGridLine (a_row, GridCurrentRow()));
		}		// end of DisplayGridLine


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Display the specified DataSet row data in the specified row in the DataGridView. Use
		/// this method when the DataGridView contains a subset of the data in the DataSet.
		/// </summary>
		/// <param name="a_dataSetRow">Display the data in this row of the DataSet.</param>
		/// <param name="a_gridRow">Display the data in this row of the DataGridView.</param>
		/// <returns>TRUE if the data was display, FALSE if an error occurred.</returns>
		//-----------------------------------------------------------------------------------------
		public bool DisplayGridLine (int a_dataSetRow, int a_gridRow)
		{
			try
			{
				for (int i=0;  i<m_map.Count;  i++)			// Loop for all columns in the map.
				{
					if (m_map.m_gridColumn[i] != null)		// If grid column is defined...
					{
						// Set the specified column in the current grid row to the value in
						// the corresponding column of the DataSet.
						m_dataGrid[m_map.m_gridColumn[i], a_gridRow].Value =
                            CCFBGlobal.Validate(m_dataSet.Tables[TableName].Rows[a_dataSetRow][m_map.m_dbColumn[i]]);
					}
				}
                m_dataGrid[COLUMN_IDX_DATA_SET, a_gridRow].Value = a_dataSetRow;
				// The above display look populates the grid row with any string data from the
				// database. Some columns require more processing to display the values and the
				// derived class can override the DisplayGridLineSpecial method to provide the
				// necessary processing.
				return (DisplayGridLineSpecial (a_dataSetRow, a_gridRow));
			}
			catch (Exception ex)
			{
				LastError = ex.Message;
				return (false);
			}			
		}		// end of DisplayGridLine


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// The application can override this method to provide any special processing required to
		/// populate the columns in the DataGridView. The class displays the string values stored
		/// in the database but the derived class can add additional process if that is required.
		/// </summary>
		//-----------------------------------------------------------------------------------------
		public virtual bool DisplayGridLineSpecial (int a_dataSetRow, int a_gridRow)
		{
			return (true);
		}		// end of DisplayGridLineSpecial


		//---------------------------------------------------------------------------------------------
		/// <summary>
		/// Enable/Disable all of the edit fields defined for the current record (focus on 1st field)
		/// or return the current edit status.
		/// </summary>
		//---------------------------------------------------------------------------------------------
		public bool EditEnabled
		{
			get	{	return (m_editEnabled);		}
			set 
			{
                for (int i = 0; i < m_map.Count; i++)			// Change the state of each edit control
                {
                    if (m_map.m_uiControl[i] != null)
                    {
                        if (m_map.m_uiControl[i].GetType().ToString() == "System.Windows.Forms.TextBox")
                        {
                            TextBox tb = (TextBox)m_map.m_uiControl[i];
                            tb.ReadOnly = !value;
                            tb.BackColor = System.Drawing.Color.White;
                        }
                        else
                            m_map.m_uiControl[i].Enabled = value;	// Enable or disable the control.

                        string s = m_map.m_uiControl[i].Name;
                    }
                }
				m_editEnabled = value;
				if (m_editEnabled)
					this.BackColor = ColorEdit;
				else
					this.BackColor = ColorBackground;
				m_dataGrid.Enabled = !m_editEnabled;		// In EDIT mode the grid is read-only.
//				m_map.m_uiControl[0].Focus();				// Focus on first input field on form.
			}
		}		// end of EditEnabled


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Prompt user to save data if changes were made and handle the form closing details. This
		/// method is normally called from the dialog FormClosing event. Do not call this method
		/// directly from the dialog because the Designer will generate a warning. As a workaround
		/// for this Designer issue just create a FormClosing event handler and call this method.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		//-----------------------------------------------------------------------------------------
		public void Event_FormClosing (object sender, FormClosingEventArgs e)
		{
			if (m_dataSet.HasChanges ())
			{
				switch (MessageBox.Show (
						"Information has been changed.\nDo you want to save the changes?",
						"Save Changes",
						MessageBoxButtons.YesNoCancel,
						MessageBoxIcon.Question))
				{
					case DialogResult.Cancel:
						e.Cancel = true;					// Stay on current dialog form.
						break;

					case DialogResult.No:					// Close current form without saving.
						break;								//   Let current FormClose finish.

					case DialogResult.Yes:
						DbSave ();							// Save DataSet data to the database.
						break;
				}		// end of switch
			}
		}		// end of Event_FormClosing


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Display the database column values in the form controls.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		//-----------------------------------------------------------------------------------------
		public void Event_GridSelectionChanged (object sender, EventArgs e)
// use rowenter event
		{
            if (alreadyHere == false )
            {
                alreadyHere = true;
                GridSelectionHandlerEnabled = false;
                int row;
                if (m_dataGrid.SelectedRows.Count > 0)
                    row = m_dataGrid.SelectedRows[0].Index;
                else
                    row = GridCurrentRow();						// Index of current row.
                try
                {
                    if (row >= 0)									// If the grid is not empty.
                    {
                        //Debug.Assert(
                        //    m_dataGrid[COLUMN_IDX_DATA_SET, row].Value != null,
                        //    "COLUMN_IDX_DATA_SET value is not defined in SqlGridDataSet:Event_GridSelectionChanged\n" +
                        //    "This is normally set in DisplayGrid(). If you are not using DisplayGrid() then you\n" +
                        //    "must set this value manually when you read or display the DataGridView.");
                        if (DisplayEntireGridRow)
                            m_dataGrid.Rows[row].Selected = true;	// Select entire grid row.
                        // The display order of the grid may have changed but this class added a column
                        // (IDX_DATA_SET) that contains the row number in the DataSet for this row.
                        // Get the DataSet row number and then display that record.
                        MoveDataSetToDisplay(DatasetRowOfGridRow);	// Display edit fields for current row.
                        Application.DoEvents();
                        // This class displays column values as they are stored in the database but some
                        // fields may require additional processing before they are displayed. The
                        // derived class can override this method to add any special display processing.
                        Event_SelectionChangedSpecial(row);
                    }
                    GridSelectionHandlerEnabled = true;
                }
                // An exception can occur when the DataGridView is empty or if no cell is selected.
                // This should probably not occur in a properly functioning production application.
                catch (Exception ex)
                {
                    LastError = "(SqlGridDataSet:Event_SelectionChanged) " + ex.Message;
                }
                alreadyHere = false;
            }
		}		// end of Event_GridSelectionChanged


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Use this override method to do any special processing of the current record data. The
		/// normal event handler simply moves the data to the display fields but you may need to
		/// convert the record to another format in order for it to display properly.
		/// </summary>
		//-----------------------------------------------------------------------------------------
		public virtual void Event_SelectionChangedSpecial (int a_row)
		{
		}		// end of Event_SelectionChangedSpecial


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Return the row of the currently selected grid cell (makes code easier to read).
		/// </summary>
		//-----------------------------------------------------------------------------------------
		public int GridCurrentRow ()
		{
		// &&&&& add error checking for null grid, no rows selected, etc.
            if (m_dataGrid.CurrentRow != null)
                return (m_dataGrid.CurrentRow.Index);
                
            else
                return -1;
		}		// end of property GridCurrentRow


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Return the row of the currently selected grid cell (makes code easier to read).
		/// </summary>
		//-----------------------------------------------------------------------------------------
		public void GridCurrentRow (int a_idxRow)
		{
            if (a_idxRow < m_dataGrid.Rows.Count)
			m_dataGrid.Rows[a_idxRow].Selected = true;
            Application.DoEvents();
		}		// end of property GridCurrentRow


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Return the row of the currently selected grid cell (makes code easier to read).
		/// </summary>
		//-----------------------------------------------------------------------------------------
#if false
		public int GridCurrentRow2
		{
		// &&&&& add error checking for null grid, no rows selected, etc.
			get	{	return (m_dataGrid.SelectedCells[0].RowIndex);	}
//			get	{	return (m_dataGrid.CurrentRow.Index);	}
			set	
			{
	//			m_dataGrid.Rows[0].Selected = true;
			}
		}		// end of property GridCurrentRow
#endif

		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Enable/Disable the SelectionChanged event handler for the DataGridView object.
		/// </summary>
		//-----------------------------------------------------------------------------------------
		public bool GridSelectionHandlerEnabled
		{
			set
			{
                if (value)
                {
                    if (eventCounter < 1)
                    {
                        m_dataGrid.SelectionChanged += new EventHandler(Event_GridSelectionChanged);
                        eventCounter++;
                    }
                }
                else
                {
                    while (eventCounter > 0)
                    {
                        m_dataGrid.SelectionChanged -= new EventHandler(Event_GridSelectionChanged);
                        eventCounter--;
                    }
                }
			}
		}		// end of 


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// This method maps the columns in the database to control objects on the current user
		/// UI form. UI controls are mapped to a database column by adding d={columnName} in the
		/// .Tag property of the control. 
		/// </summary>
		//-----------------------------------------------------------------------------------------
		private void MapDbColumnsToUiFields (Control.ControlCollection cc)
		{
// &&&&& this needs to be made recursive to find embedded controls.

			// Used to parse the control's Tag field.
			char [] separators = new char [] {'=', '|', ';', ','};

			//m_noFormControls = 0;
			// Get the control collection and parse each control for the .Tag field. If the
			// control is linked to the database then it will contain a v={DbColumnName} string.
			// Store the DB column name and the control name in a mapping table. This table is
			// used to display and edit the display and database data.
			for (int idxControl=0;  idxControl<cc.Count;  idxControl++)
			{
                if (cc[idxControl].Tag != null)
				{
					// Split the Tag field into tokens. A tag field of d=dbPhoneNo|v=Phone will be
					// split into an array of strings in this format:
					//			[0]	d						<-- Function code (d=database column)
					//			[1] dbPhoneNo				<-- Column name in database
					//			[2] v						<-- Validation to be used for this data
					//			[3] Phone					<-- Validation function (future)
                    string[] tagTokens = cc[idxControl].Tag.ToString().Split(separators);
					for (int i=0; i<tagTokens.Length; i=i+2)// Check the function codes.
					{
						switch (tagTokens[i].ToLower())
						{
							case "d":
                                m_map.AddControl(tagTokens[i + 1].ToLower(), cc[idxControl].Name.ToString(), cc[idxControl]);
								++m_noFormControls;			// # of mapped controls on the form.
								break;
                            case "f":
                            case "":
                                break;
							default:
								// This is an error message that should only be seen during development.
                                MessageBox.Show("Invalid .Tag function code in " + cc[idxControl].Name.ToString());
								break;
						}		// end of switch
					}		// end of for
				}
                MapDbColumnsToUiFields(cc[idxControl].Controls);
			}
		}		// end of MapDbColumnsToUiFields


		private void MapDbColumnsToUiFields_Save ()
		{
			// Used to parse the control's Tag field.
			char [] separators = new char [] {'=', '|', ';', ','};

			m_noFormControls = 0;
			// Get the control collection and parse each control for the .Tag field. If the
			// control is linked to the database then it will contain a v={DbColumnName} string.
			// Store the DB column name and the control name in a mapping table. This table is
			// used to display and edit the display and database data.
			for (int idxControl=0;  idxControl<ControlPage.Count;  idxControl++)
			{
				if (ControlPage[idxControl].Tag != null)
				{
					// Split the Tag field into tokens. A tag field of d=dbPhoneNo|v=Phone will be
					// split into an array of strings in this format:
					//			[0]	d						<-- Function code (d=database column)
					//			[1] dbPhoneNo				<-- Column name in database
					//			[2] v						<-- Validation to be used for this data
					//			[3] Phone					<-- Validation function (future)
					string [] tagTokens = ControlPage[idxControl].Tag.ToString().Split (separators);
					for (int i=0; i<tagTokens.Length; i=i+2)// Check the function codes.
					{
						switch (tagTokens[i].ToLower())
						{
							case "d":
								m_map.AddControl (tagTokens[i+1], ControlPage[idxControl].Name.ToString(), ControlPage[idxControl]);
								++m_noFormControls;			// # of mapped controls on the form.
								break;
							default:
								// This is an error message that should only be seen during development.
								MessageBox.Show ("Invalid .Tag function code in " + ControlPage[idxControl].Name.ToString());
								break;
						}		// end of switch
					}		// end of for
				}
			}
		}		// end of MapDbColumnsToUiFields


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Move the data from the DataSet to the display objects on the current form. The form
		/// control objects use the .Tag property to map the database column name to the form
		/// object (set the .Tag property to d={dbColumnName}).
		/// </summary>
		//-----------------------------------------------------------------------------------------
		public void MoveDataSetToDisplay (int a_row)
		{
			if (m_noFormControls <= 0)						// If this form does not use UI objects
				return;										//   to display the DB data then leave.
			for (int i=0;  i<m_map.Count;  i++)				// For each column defined in the DB row...
			{
				if (m_map.m_uiFieldName[i] != null)			// If this DB column has a screen control.
				{
					SetControlValue (							// Set UI control value(control,value).
						m_map.m_uiControl[i],				// The UI control on the current form.
							DataSetValue (					// DataSet value of the table cell
								a_row,						//   at this DataSet row and
								m_map.m_dbColumn[i]));		//   in this DataSet column.
				}
			}
		}		// end of MoveDataSetToDisplay


		//---------------------------------------------------------------------------------------------
		/// <summary>
		/// Move the data from the display objects on the current form to the DataSet. The form
		/// control objects use the .Tag property to map the database column name to the form
		/// object (set the .Tag property to d={dbColumnName}).
		/// </summary>
		//---------------------------------------------------------------------------------------------
		public void MoveDisplayToDataSet ()
		{
			Debug.Assert (									// For debugging during development.
				(m_controlPage != null),
				"Must define the ControlPage (normally done in dialog _Load function) - GenericSql:MoveDisplayToRecord");

			int row = DatasetRowOfGridRow;					// Current record row in DataSet.
			for (int i=0;  i<m_map.Count;  i++)				// Move the data for each column that
			{												//   has a display field on the form.
				if (m_map.m_uiControl != null)
				{
					if (m_map.m_uiControl[i] != null)
						m_dataSet.Tables[TableName].Rows[row][m_map.m_dbColumn[i]] = 
							GetControlValue (m_map.m_uiControl[i]);
				}
			}
			DisplayGridLine (row, GridCurrentRow());							// Move DataSet row data to grid row.
		}		// end of MoveDisplayToDataSet


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Insert the current record into the DataSet.
		/// </summary>
		/// <returns>TRUE if the row was inserted into database, FALSE if an error occurred.</returns>
		//-----------------------------------------------------------------------------------------
		public bool RowInsert ()
		{
			// Since this method manipulates the DataGridView object this event must be disabled.
			GridSelectionHandlerEnabled = false;

			// Create a blank row in the DataSet for the new record.
			DataRow newRow = m_dataSet.Tables[TableName].NewRow();
			m_dataSet.Tables[TableName].Rows.Add (newRow);
			int dsRowNo = m_dataSet.Tables[TableName].Rows.Count - 1;

			int gridRowNo = m_dataGrid.Rows.Add ();			// Add a new row in the grid for new record.

			// Point the new row in the DataGridView to the new row in the DataSet.
			m_dataGrid[COLUMN_IDX_DATA_SET,gridRowNo].Value = dsRowNo.ToString();
            m_dataGrid.CurrentCell = m_dataGrid[2, gridRowNo];  // Set the new row to be the current row.

			// Move the data in the UI fields into the DataSet. The Move method retrieves the value
			// in the COLUMN_IDX_DATA_SET hidden column of the DataGridView. This value is the row
			// index in the DataSet that holds the values of the current record and, in this case,
			// the current grid row is our new row and the DataSet row is our new DataSet row.
			MoveDisplayToDataSet ();						// Move display field data to row buffer.

			// The data in the UI fields are now in the DataSet row MoveDisplayToDataset  
			// Calls DisplayGridLine
            //DisplayGridLine(dsRowNo, gridRowNo);
			GridSelectionHandlerEnabled = true;
			return (true);
		}		// end of RowInsert


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Delete the current row in the DataGridView from the database.
		/// </summary>
		/// <param name="a_userPrompt">TRUE if a user prompt is to be issued, FALSE if no prompting.</param>
		/// <returns>TRUE if record deleted; FALSE if an error occurred or no record to delete.</returns>
		//-----------------------------------------------------------------------------------------
		public bool RowDeleteCurrent (bool a_userPrompt)
		{
			if (a_userPrompt)
			{
				if (DialogResult.Yes != MessageBox.Show (
						"Are you sure you want to delete the current record?",
						"Delete Record",
						MessageBoxButtons.YesNoCancel,
						MessageBoxIcon.Question))
					return (false);							// User aborted the delete operation.
			}
			// Remove the current row from the DataSet and from the DataGridView.
			m_dataSet.Tables[TableName].Rows.RemoveAt(DatasetRowOfGridRow);
			m_dataGrid.Rows.RemoveAt (GridCurrentRow());
			return (true);
		}		// end of RowDeleteCurrent


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Map the grid column to a database column. The DataGridView column properties do not
		/// include the .Tag property so the application must set the .Tag property manually.
		/// ***** IMPORTANT *****
		/// Call this method AFTER the application opens the database table. When the DB table
		/// is opened the mapping structure is cleared and re-populated. If the grid column
		/// map is set before the Open then all of the values will be erased!
		/// </summary>
		/// <param name="a_dbColumn">Map this database column to the specified grid column.</param>
		/// <param name="a_gridColumn">Map the DB column to this grid column.</param>
		/// <returns></returns>
		//-----------------------------------------------------------------------------------------
		public bool SetGridColumn (string a_dbColumn, string a_gridColumn)
		{
			return (m_map.AddGridColumn (a_dbColumn.ToLower(), a_gridColumn.ToLower()));
		}		// end of SetGridColumn

		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		//-----------------------------------------------------------------------------------------
		public bool ValidateMapping ()
		{
			// Read table into dataset.
			// Read .Tag fields from controls.
			// Check d={columnName} values to make sure valid DB columns are referenced.
			// Validate grid columns against DB column names
			return (true);
		}   // end of ValidateMapping

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // SqlGridDataSet
            // 
            this.ClientSize = new System.Drawing.Size(282, 255);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "SqlGridDataSet";
            this.ResumeLayout(false);

        }		


	}		// end of class
}		// end of namespace