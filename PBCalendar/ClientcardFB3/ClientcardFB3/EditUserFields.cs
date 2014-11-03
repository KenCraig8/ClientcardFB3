//*************************************************************************************************
//
//
//
// Date			Version		Author				Description
// ----------	-------		------------------	--------------------------------------------------
// 2010-11-04	00.01.00	T. Cataldo			Started UserFields class.
// 2010-11-08	00.02.00	T. Cataldo			Switched code to use DataSet for DB interface.
//
//*************************************************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ClientcardFB3
{
	public partial class EditUserFields : SqlGridDataSet
	{
		#region ----------Constants----------

		public const string COLUMN_BTN_DELETE	= "Delete";			// Text used on DELETE button.
		public const string COLUMN_BTN_UNDELETE	= "Undelete";		// Text used on DELETE button.

		public const string COLUMN_TABLE_NAME	= "TableName";		// Primary key (unique).
		public const string COLUMN_FLD_NAME		= "FldName";		// Sort order field.
		public const string COLUMN_CONTROL_TYPE	= "ControlType";	// Name used in ClientCard input forms.
		public const string COLUMN_EDIT_LABEL	= "EditLabel";		// Long name or description of the code.
		public const string COLUMN_EDIT_TIP		= "EditTip";		// Long name or description of the code.

        //public const int	COLUMN_NO_TABLE_NAME	= 0;
        //public const int	COLUMN_NO_FLD_NAME		= 1;
        //public const int	COLUMN_NO_CONTROL_TYPE	= 2;
        //public const int	COLUMN_NO_EDIT_LABEL	= 3;
        //public const int	COLUMN_NO_EDIT_TIP		= 4;

		#endregion


		#region ----------Variables----------

		/// <summary>
		/// Set this flag (in the CellBeginEdit handler) when a cell is being edited. This flag is
		/// used in the validation method so that it only checks cells that have been edited.
		/// </summary>
		private int m_editColumn = -1;
		private DataGridViewCell m_editCell;
		private int m_editRow = -1;
		private string m_editOriginalValue = "";

		/// <summary>
		/// This flag is TRUE if any data in the grid was modified. It is used when closing the
		/// dialog to determine how to handle database updates.
		/// </summary>
		private bool m_dataChanged = false;

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
		public EditUserFields (string a_dbConnectionString)
		{
			InitializeComponent();

			ConnectionString = a_dbConnectionString;
		}		// end of constructor


		public EditUserFields (string a_dbServer, string a_dbName, string a_dbUsername, string a_dbPassword)
		{
			InitializeComponent();

			ConnectionStringCreate (a_dbServer, a_dbName, a_dbUsername, a_dbPassword);
		}		// end of constructor



        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        
        //-----------------------------------------------------------------------------------------
		/// <summary>
		/// Clear the data in the Type Codes table and replace it with the updated data in the
		/// list structure. If no data was changed then leave the current table intact.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		//-----------------------------------------------------------------------------------------
		private void btnSave_Click (object sender, EventArgs e)
		{
            gridUserField.EndEdit();
            DbSave();
            m_dataChanged = false; 
            Close();
		}		// end of btnSave_Click


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Display the selected Type Code table and set the controls.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		//-----------------------------------------------------------------------------------------
		private void cboTable_Select_SelectedIndexChanged (object sender, EventArgs e)
		{

            int gridRow = -1;								// Display data in this grid row.

			gridUserField.Rows.Clear();						// Start with clean grid control.
			for (int i=0;  i<DataSetCount;  i++)			// For all columns in the DataSet.
			{
				// If this DataSet row matches the currently selected TableName column.
				if (cboTable_Select.Text == m_dataSet.Tables[TableName].Rows[i][COLUMN_TABLE_NAME].ToString())
				{
					gridUserField.Rows.Add();				// Add a new grid row and display the
					DisplayGridLine (i, ++gridRow);			//   DataSet data in the grid row.
				}
			}
		}		// end of cboTable_Select_SelectedIndexChanged


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Return the column of the currently selected grid cell (makes code easier to read).
		/// </summary>
		//-----------------------------------------------------------------------------------------
		private int CurrentColumn
		{
			get	{	return (gridUserField.SelectedCells[0].ColumnIndex);	}
		}		// end of property CurrentColumn


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Return the row of the currently selected grid cell (makes code easier to read).
		/// </summary>
		//-----------------------------------------------------------------------------------------
		private int CurrentRow
		{
			get	{	return (gridUserField.SelectedCells[0].RowIndex);	}
		}		// end of property CurrentRow


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Return the value in the currently selected grid cell (makes code easier to read).
		/// </summary>
		//-----------------------------------------------------------------------------------------
		private string CurrentCellValue
		{
			get	{	return (gridUserField.SelectedCells[0].FormattedValue.ToString());	}
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
		/// Display a list of Table Names from the DataSet.
		/// </summary>
		/// <param name="a_comboBox">Display the table names in this ComboBox.</param>
		//-----------------------------------------------------------------------------------------
		private void DisplayTableNames (ComboBox a_comboBox)
		{
			string tableName;								// Current TableName from DataSet.

			a_comboBox.Items.Clear ();						// Start with clean ComboBox.
			a_comboBox.Sorted = true;
			for (int i=0;  i<DataSetCount;  i++)
			{
				tableName = m_dataSet.Tables[TableName].Rows[i][COLUMN_TABLE_NAME].ToString();
				if (!a_comboBox.Items.Contains(tableName))	// Add table name if not in combobox
					a_comboBox.Items.Add (tableName);
			}
			if (a_comboBox.Items.Count > 0)					// If there are items in the list then
				a_comboBox.SelectedIndex = 0;				//   select the first item.
		}		// end of DisplayTableNames


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Close the form and prompt the user to save the data if it was modified.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		//-----------------------------------------------------------------------------------------
		private void EditUserFields_FormClosing (object sender, FormClosingEventArgs e)
		{
			if (m_dataChanged)
			{
				DialogResult result = MessageBox.Show (
					"All changes made to the User Fields will be lost!\nAre you sure you want to exit?",
					"Exit Warning",
					MessageBoxButtons.YesNoCancel);
				if (result == DialogResult.Yes)				// YES to exit and NOT save data.
					m_dataChanged = false;					// Forget any changes that were made.
				else
					e.Cancel = true;						// Force execution to stay on this form.
//				e.Cancel = (result != DialogResult.Yes);	// TRUE cancels the close operation.
//				m_dataModified = e.Cancel;					// Cancel sets Modified to FALSE.
			}
		}		// end of EditUserFields_FormClosing


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Read the Type Codes database table and display the data in a DataGridView control.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		//-----------------------------------------------------------------------------------------
		private void EditUserFields_Load (object sender, EventArgs e)
		{
			ControlPage			= this.Controls;
			DataGridViewObject	= gridUserField;
			TableName			= "UserFields";

			if (!DbOpenToGrid (TableName, "SELECT * FROM UserFields ORDER BY TableName, FldName"))
			{
				MessageBox.Show ("Error opening the " + TableName + " DB table");
				return;
			}

			// The column property in a DataGridView does not expose the .Tag property. These
			// calls map the column in the DataSet to the column in the DataGridView. Note that
			// the "gridEditDonor_RcdType" is NOT in the list since it is handled in the
			// "DisplayGridLineSpecial" method (requires TypeCode name lookup).
			SetGridColumn (COLUMN_FLD_NAME,		"gridUserField_FldName"	 );
			SetGridColumn (COLUMN_EDIT_LABEL,	"gridUserField_EditLabel");
			SetGridColumn (COLUMN_EDIT_TIP,		"gridUserField_EditTip"  );

			DisplayTableNames (cboTable_Select);			// Display sorted list of table names.
		}		// end of EditUserFields_Load


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Final dialog tweeks after all controls have been drawn.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		//-----------------------------------------------------------------------------------------
		private void EditUserFields_Shown (object sender, EventArgs e)
		{
		}		// end of EditUserFields_Shown


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		//-----------------------------------------------------------------------------------------
		private void grid_CellBeginEdit (object sender, DataGridViewCellCancelEventArgs e)
		{
			if (m_editColumn != e.ColumnIndex || m_editRow != e.RowIndex)
			{
				m_editColumn	= CurrentColumn;
				m_editRow		= CurrentRow;
				m_editOriginalValue = gridUserField[m_editColumn,m_editRow].FormattedValue.ToString ();
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
#if false
			int		row = CurrentRow;						// Current row in the grid.
//			string	sortOrder;								// Sort Order value temp (used for renumbering).
			string	sortOrderDeleted;						// Sort order value of the deleted row.
			string	typeCodeName;							// The "Type" field from the DB.

			switch (CurrentCellValue)
			{
				case COLUMN_BTN_DELETE:
					if (AreTypeCodesUsed ())
					{
						// Do something here -- has not been determined how to handle this case.
					}
					typeCodeName	= gridTypeCodes.Rows[row].Cells[COLUMN_NO_TYPE		].FormattedValue.ToString();
					sortOrderDeleted= gridTypeCodes.Rows[row].Cells[COLUMN_NO_SORT_ORDER].FormattedValue.ToString();
					if (DialogResult.Yes == MessageBox.Show (
							"Delete the \"" + typeCodeName + "\" Type Code?\n" +
								"Are you sure?",
							"Delete Type Code",
							MessageBoxButtons.YesNoCancel,
							MessageBoxIcon.Question))
					{
//						gridTypeCodes.Rows.RemoveAt (row);			// Remove the current line from grid.
						gridTypeCodes[COLUMN_NO_DELETE, row].Value = COLUMN_BTN_UNDELETE;
						m_dataChanged = true;
					}
					// De-select the DELETE button so that it will not appear highlighted.
					gridTypeCodes[COLUMN_NO_DELETE,row].Selected = false;

#if false
					// Adjust the sequencing of the Sort Order fields. Decrement all of the Sort Order
					// values that are greater than the one that was deleted.
					for (int i=0;  i<gridTypeCodes.Rows.Count;  i++)
					{
						sortOrder = gridTypeCodes.Rows[i].Cells[COLUMN_NO_SORT_ORDER].FormattedValue.ToString();
						if (sortOrder.CompareTo (sortOrderDeleted) > 0)
							gridTypeCodes [COLUMN_NO_SORT_ORDER, i].Value =		// Decrement the Sort Order
								(Convert.ToInt32(sortOrder) - 1).ToString ();	//   value of current cell.
					}
#endif
					break;

				case COLUMN_BTN_UNDELETE:
					gridTypeCodes[COLUMN_NO_DELETE, row].Value = COLUMN_BTN_DELETE;
					break;

				default:
					return;
			}		// end of switch
#endif
		}		// end of grid_CellClick


		private void PlaceHolderMethodToPreserveFinalClosingBrace ()
		{
		}

        private void gridUserField_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == m_editColumn && e.RowIndex == m_editRow)
                if (m_editOriginalValue != CurrentCellValue)
                {
                    m_dataChanged = true;							// Flag that data has changed.
                    btnSave.Enabled = true;
                    DataSetValue(-1,e.ColumnIndex,CurrentCellValue);
                }
        }
	}		// end of class
}		// end of namespace
