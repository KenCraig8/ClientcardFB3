//*************************************************************************************************
//
//
//
// Date			Version		Author				Description
// ----------	-------		------------------	--------------------------------------------------
// 2010-11-04	00.01.00	T. Cataldo			Started UserFields class.
// 2010-11-08	00.02.00	T. Cataldo			Switched code to use DataSet for DB interface.
// 2011-06-09   00.03.00    Steven Staley       Completly revamped code to follow our coding practices. 
//                                              No Longer Uses Tonys Classes now                                
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
	public partial class EditUserFields :Form
	{
		#region ----------Variables----------

        UserFields clsUserFields = new UserFields(CCFBGlobal.connectionString);
		private bool m_dataChanged = false;
        bool fieldLabelsChanged = false;
        int editRow = 0;
        string currentValue = "";
        bool userFieldsReset = false;
		#endregion

		public EditUserFields ()
		{
			InitializeComponent();
            fillTableNamesCombo();
            loadList();
		}

        public bool UserFieldsReset
        {
            get
            {
                return userFieldsReset;
            }
        }

        public bool FieldsLabelChanged
        {
            get
            {
                return fieldLabelsChanged;
            }
        }

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
            checkForEmptyAlertText();
            clsUserFields.update();
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
        private void cboTable_Select_SelectedIndexChanged(object sender, EventArgs e)
		{
            clsUserFields.open(cboTable_Select.SelectedItem.ToString());
            loadList();
           
		}		// end of cboTable_Select_SelectedIndexChanged


        /// <summary>
        /// Checks the dataset for rows where AutoAlert = true but No text is entered
        /// It then puts the edit lable in the AutoAlertText Field
        /// </summary>
        public void checkForEmptyAlertText()
        {
            for (int i = 0; i < clsUserFields.RowCount; i++)
            {
                clsUserFields.setDataRow(i);
                if (clsUserFields.AutoAlert == true && String.IsNullOrEmpty(clsUserFields.AutoAlertText) == true)
                    clsUserFields.AutoAlertText = clsUserFields.EditLabel;
            }
        }

        /// <summary>
        /// Event that triggers when the Context Menue Strip Opens.
        /// Sets Proper Visibility of the Items For the DataGrid's CurrentRow's field.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void csmGridUserFields_Opening(object sender, CancelEventArgs e)
        {
            if (gridUserField.CurrentRow != null && gridUserField.CurrentRow.Cells["colFldName"].Value.ToString().Contains("Flag"))
            {
                csmGridUserFields.Items[0].Visible = true;
                csmGridUserFields.Items[1].Visible = false;
            }
            else
            {
                csmGridUserFields.Items[0].Visible = false;
                csmGridUserFields.Items[1].Visible = true;
            }

        }
		
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
			
		}		// end of EditUserFields_Load

        /// <summary>
        /// Fills the table selection combo with the distinc table names in database
        /// </summary>
        private void fillTableNamesCombo()
        {
            clsUserFields.getUniqeTableNames();
            for (int i = 0; i < clsUserFields.RowCount; i++)
            {
                cboTable_Select.Items.Add(clsUserFields.DSet.Tables[0].Rows[i][0].ToString());
            }
            if (cboTable_Select.Items.Count > 0)
                cboTable_Select.SelectedIndex = 0;
        }


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		//-----------------------------------------------------------------------------------------
		private void grid_CellBeginEdit (object sender, DataGridViewCellCancelEventArgs e)
		{
            currentValue = gridUserField[e.ColumnIndex, e.RowIndex].Value.ToString();
            btnSave.Enabled = true;
            m_dataChanged = true;
            fieldLabelsChanged = true;
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

		}		// end of grid_CellClick

        private void gridUserField_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
                if (gridUserField[e.ColumnIndex, e.RowIndex].Value != null)
                    clsUserFields.SetDataValue(gridUserField.Columns[e.ColumnIndex].DataPropertyName,
                        gridUserField[e.ColumnIndex, e.RowIndex].Value.ToString());
                else
                    clsUserFields.SetDataValue(gridUserField.Columns[e.ColumnIndex].DataPropertyName, "");
        }

        
        private void gridUserField_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            editRow = e.RowIndex;
            clsUserFields.setDataRow(editRow);
        }

        /// <summary>
        /// Loads the list of UserDefinedFields
        /// </summary>
        private void loadList()
        {
            gridUserField.Rows.Clear();
            for (int i = 0; i < clsUserFields.RowCount; i++)
            {
                gridUserField.Rows.Add();
                clsUserFields.setDataRow(i);
                gridUserField["colFldName", i].Value = clsUserFields.FldName;
                gridUserField["colEditLabel", i].Value = clsUserFields.EditLabel;
                gridUserField["colEditTip", i].Value = clsUserFields.EditTip;
                gridUserField["colAutoAlert", i].Value = clsUserFields.AutoAlert;
                gridUserField["colAutoAlertText", i].Value = clsUserFields.AutoAlertText;
            }
        }

        /// <summary>
        /// Gets Confirmation from user that they actually do want to reset UserField
        /// and then calls the clsUserFields reset method.
        /// </summary>
        /// <param name="value">The New Value (ie 0 or 1)</param>
        /// <param name="confirmationDisplayValue">The Value To Display in the MessageBox (ie Checked, Unchecked, 0)</param>
        private void resetValue(string value, string confirmationDisplayValue)
        {

            if (MessageBox.Show("WARNING: You Are About To Reset " + gridUserField.CurrentRow.Cells[1].Value.ToString()
                + " To " + confirmationDisplayValue + "!  Do You Want To Continue?", "WARNING: CHANGING FIELD VALUE",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
            {
                clsUserFields.resetUserField(cboTable_Select.SelectedItem.ToString(),
                    gridUserField.CurrentRow.Cells[1].Value.ToString(), value);

                userFieldsReset = true;
            }
        }

        private void tsmiResetTrue_Click(object sender, EventArgs e)
        {
            resetValue("1", "Checked");
        }

        private void tsmiResetFalse_Click(object sender, EventArgs e)
        {
            resetValue("0", "Unchecked");
        }

        private void tsmiResetUserNumber_Click(object sender, EventArgs e)
        {
            resetValue("0", "0");
        }
	}		// end of class
}		// end of namespace
