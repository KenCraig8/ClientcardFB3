using System;
using System.Windows.Forms;

namespace ClientcardFB3
{
    public partial class IncomeMatrixForm : Form
    {
        IncomeGroups clsIncomeGroups = new IncomeGroups();
        IncomeMatrix clsIncomeMatrix = new IncomeMatrix();

        //Used so that certian events dont fire when data 
        //is being loaded from database into the controls
        bool loadingData = true; 

        //A holder for the current cell value, used in validation of Cell.Value
        string cellValue = "";
        
        /// <summary>
        /// Income Matrix Form Constructor.  Initializes all componants of the form and loads all data
        /// </summary>
        public IncomeMatrixForm()
        {
            InitializeComponent();

            clsIncomeGroups.openAll();  //Opens all income groups
            fillCombos();   //Fills the Income Group Combobox with all income groups

            //Get all values from the database where the income group = current selected income group
            clsIncomeMatrix.openWhere(" Where IncomeGroup="+ clsIncomeGroups.getCode(
                cboMatrixGroups.SelectedItem.ToString()));

            //Loads the income grid
            loadIncomeGrid();
            loadingData = false;
        }

        /// <summary>
        /// Fills the Income Groups Combo with the income groups in database
        /// </summary>
        private void fillCombos()
        {
            cboMatrixGroups.Items.Clear();

            //For each group in the database add the group to the combo
            for (int i = 0; i < clsIncomeGroups.RowCount; i++)
            {
                clsIncomeGroups.setDataRow(i);
                cboMatrixGroups.Items.Add(clsIncomeGroups.Description);
            }

            //Set datarow in income groups class to the first group
            clsIncomeGroups.setDataRow(0);

            //Set the combo's selected item to the first item in combo
            if (cboMatrixGroups.Items.Count > 0)
                cboMatrixGroups.SelectedIndex = 0;
        }

        /// <summary>
        /// Loads the income grid with the values in the database
        /// </summary>
        private void loadIncomeGrid()
        {
            dgvIncomeMatrix.Rows.Clear();

            int displayIndex = 0;

            //For each row in the database add the data to the grid
            for (int i = 0; i < clsIncomeMatrix.RowCount; i++)
            {
                #region Fills Each Row of Grid
                clsIncomeMatrix.setDataRow(i);
                dgvIncomeMatrix.Rows.Add();
                dgvIncomeMatrix.Rows.Add();
                dgvIncomeMatrix.Rows[displayIndex].Cells["clmCat"].Value = clsIncomeMatrix.Label1;
                dgvIncomeMatrix.Rows[displayIndex].Cells["clmCat"].Tag = "Label1";
                dgvIncomeMatrix.Rows[displayIndex + 1].Cells["clmCat"].Value = clsIncomeMatrix.Label1;
                dgvIncomeMatrix.Rows[displayIndex + 1].Cells["clmCat"].Tag = "Label1";
                dgvIncomeMatrix.Rows[displayIndex].Cells["clmDesc"].Value = clsIncomeMatrix.Label2;
                dgvIncomeMatrix.Rows[displayIndex].Cells["clmDesc"].Tag = "Label2";
                dgvIncomeMatrix.Rows[displayIndex + 1].Cells["clmDesc"].Value = clsIncomeMatrix.Label3;
                dgvIncomeMatrix.Rows[displayIndex + 1].Cells["clmDesc"].Tag = "Label3";
                dgvIncomeMatrix.Rows[displayIndex].Cells["clmLowHi"].Value = "LOW";
                dgvIncomeMatrix.Rows[displayIndex + 1].Cells["clmLowHi"].Value = "HI";
                dgvIncomeMatrix.Rows[displayIndex].Cells["clmInc1"].Value = clsIncomeMatrix.IncomeLow1;
                dgvIncomeMatrix.Rows[displayIndex].Cells["clmInc1"].Tag = "IncomeLow1";
                dgvIncomeMatrix.Rows[displayIndex + 1].Cells["clmInc1"].Value = clsIncomeMatrix.IncomeHi1;
                dgvIncomeMatrix.Rows[displayIndex + 1].Cells["clmInc1"].Tag = "IncomeHi1";
                dgvIncomeMatrix.Rows[displayIndex].Cells["clmInc2"].Value = clsIncomeMatrix.IncomeLow2;
                dgvIncomeMatrix.Rows[displayIndex].Cells["clmInc2"].Tag = "IncomeLow2";
                dgvIncomeMatrix.Rows[displayIndex + 1].Cells["clmInc2"].Value = clsIncomeMatrix.IncomeHi2;
                dgvIncomeMatrix.Rows[displayIndex + 1].Cells["clmInc2"].Tag = "IncomeHi2";
                dgvIncomeMatrix.Rows[displayIndex].Cells["clmInc3"].Value = clsIncomeMatrix.IncomeLow3;
                dgvIncomeMatrix.Rows[displayIndex].Cells["clmInc3"].Tag = "IncomeLow3";
                dgvIncomeMatrix.Rows[displayIndex + 1].Cells["clmInc3"].Value = clsIncomeMatrix.IncomeHi3;
                dgvIncomeMatrix.Rows[displayIndex + 1].Cells["clmInc3"].Tag = "IncomeHi3";
                dgvIncomeMatrix.Rows[displayIndex].Cells["clmInc4"].Value = clsIncomeMatrix.IncomeLow4;
                dgvIncomeMatrix.Rows[displayIndex].Cells["clmInc4"].Tag = "IncomeLow4";
                dgvIncomeMatrix.Rows[displayIndex + 1].Cells["clmInc4"].Value = clsIncomeMatrix.IncomeHi4;
                dgvIncomeMatrix.Rows[displayIndex + 1].Cells["clmInc4"].Tag = "IncomeHi4";
                dgvIncomeMatrix.Rows[displayIndex].Cells["clmInc5"].Value = clsIncomeMatrix.IncomeLow5;
                dgvIncomeMatrix.Rows[displayIndex].Cells["clmInc5"].Tag = "IncomeLow5";
                dgvIncomeMatrix.Rows[displayIndex + 1].Cells["clmInc5"].Value = clsIncomeMatrix.IncomeHi5;
                dgvIncomeMatrix.Rows[displayIndex + 1].Cells["clmInc5"].Tag = "IncomeHi5";
                dgvIncomeMatrix.Rows[displayIndex].Cells["clmInc6"].Value = clsIncomeMatrix.IncomeLow6;
                dgvIncomeMatrix.Rows[displayIndex].Cells["clmInc6"].Tag = "IncomeLow6";
                dgvIncomeMatrix.Rows[displayIndex + 1].Cells["clmInc6"].Value = clsIncomeMatrix.IncomeHi6;
                dgvIncomeMatrix.Rows[displayIndex + 1].Cells["clmInc6"].Tag = "IncomeHi6";
                dgvIncomeMatrix.Rows[displayIndex].Cells["clmInc7"].Value = clsIncomeMatrix.IncomeLow7;
                dgvIncomeMatrix.Rows[displayIndex].Cells["clmInc7"].Tag = "IncomeLow7";
                dgvIncomeMatrix.Rows[displayIndex + 1].Cells["clmInc7"].Value = clsIncomeMatrix.IncomeHi7;
                dgvIncomeMatrix.Rows[displayIndex + 1].Cells["clmInc7"].Tag = "IncomeHi7";
                dgvIncomeMatrix.Rows[displayIndex].Cells["clmInc8"].Value = clsIncomeMatrix.IncomeLow8;
                dgvIncomeMatrix.Rows[displayIndex].Cells["clmInc8"].Tag = "IncomeLow8";
                dgvIncomeMatrix.Rows[displayIndex + 1].Cells["clmInc8"].Value = clsIncomeMatrix.IncomeHi8;
                dgvIncomeMatrix.Rows[displayIndex + 1].Cells["clmInc8"].Tag = "IncomeHi8";
                dgvIncomeMatrix.Rows[displayIndex].Cells["clmInc9"].Value = clsIncomeMatrix.IncomeLow9;
                dgvIncomeMatrix.Rows[displayIndex].Cells["clmInc9"].Tag = "IncomeLow9";
                dgvIncomeMatrix.Rows[displayIndex + 1].Cells["clmInc9"].Value = clsIncomeMatrix.IncomeHi9;
                dgvIncomeMatrix.Rows[displayIndex + 1].Cells["clmInc9"].Tag = "IncomeHi9";
                dgvIncomeMatrix.Rows[displayIndex].Cells["clmInc10"].Value = clsIncomeMatrix.IncomeLow10;
                dgvIncomeMatrix.Rows[displayIndex].Cells["clmInc10"].Tag = "IncomeLow10";
                dgvIncomeMatrix.Rows[displayIndex + 1].Cells["clmInc10"].Value = clsIncomeMatrix.IncomeHi10;
                dgvIncomeMatrix.Rows[displayIndex + 1].Cells["clmInc10"].Tag = "IncomeHi10";
                dgvIncomeMatrix.Rows[displayIndex].Cells["clmID"].Value = clsIncomeMatrix.ID;
                dgvIncomeMatrix.Rows[displayIndex + 1].Cells["clmID"].Value = clsIncomeMatrix.ID;
                displayIndex += 2;
                #endregion
            }
        }

        /// <summary>
        /// Event that fires when the cell is done editing
        /// Inserts data to database and sets visibility on buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvIncomeMatrix_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvIncomeMatrix.CurrentCell != null && dgvIncomeMatrix.CurrentCell.Value != null)
            {
                //Find the datarow in dataset
                clsIncomeMatrix.find(Convert.ToInt32(dgvIncomeMatrix.CurrentRow.Cells["clmID"].Value));
                //Set data value into dataset
                clsIncomeMatrix.setDataValue(dgvIncomeMatrix.CurrentCell.Tag.ToString(), cellValue);        
                //Re-enter value into cell to get proper formating
                dgvIncomeMatrix.CurrentCell.Value = clsIncomeMatrix.getDataValue(dgvIncomeMatrix.CurrentCell.Tag.ToString());

                //Activate the cancel and save buttons
                btnCancel.Visible = true;
                btnSave.Enabled = true;
            }
            else
            {
                if (dgvIncomeMatrix.CurrentCell != null)
                {
                    dgvIncomeMatrix.CurrentCell.Value = 
                        clsIncomeMatrix.getDataValue(dgvIncomeMatrix.CurrentCell.Tag.ToString());
                }
            }
        }

        /// <summary>
        /// Event fires when the save button is clicked. 
        /// Updates the database with any changes made to the dataset
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            clsIncomeMatrix.update();
        }

        /// <summary>
        /// Event fires when a cell is trying to be left, and checks that the value is valid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvIncomeMatrix_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            //If not loading and in an dollar value cell
            if (loadingData == false && e.ColumnIndex > 2)
            {
                //Get the value of the cell without dollar signs and commas
                cellValue = dgvIncomeMatrix.CurrentCell.GetEditedFormattedValue(e.RowIndex,
                        DataGridViewDataErrorContexts.Parsing).ToString().
                        Replace("$", String.Empty).Replace(",", String.Empty);
                
                //Try to parse the value into an integer
                try
                {
                    int converted = int.Parse(cellValue);
                }
                catch
                { 
                    //If does not parse it is not in proper format.  This lets the user know that.
                    MessageBox.Show("You Have Entered An Invalid Value. Please Enter Only "
                        + "Whole Numbers In Currency Format or Number Format");
                    //Canel the current action and do not fire the cellEndEdit event
                    e.Cancel = true;
                    //Save button gets dissabled
                    btnSave.Enabled = false;
                }
            }
            else if (e.ColumnIndex == 1)
            {
                cellValue = cellValue = dgvIncomeMatrix.CurrentCell.GetEditedFormattedValue(e.RowIndex,
                        DataGridViewDataErrorContexts.Parsing).ToString();
            }

        }

        /// <summary>
        /// Event fires when the close button is fired
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            //Close form
            this.Close();
        }

        /// <summary>
        /// Event fires when the cancel button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            //Refill the dataset with values from table a reload grid
            openForIncomeGroupAndFillGrid();
        }

        /// <summary>
        /// Event fires when the combo box selection is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboMatrixGroups_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //Refill the dataset with values from table 
            //and reload grid using the selected group
            openForIncomeGroupAndFillGrid();
        }

        /// <summary>
        /// Retrives the income martix for the selected 
        /// group and fills the income matrix grid
        /// </summary>
        private void openForIncomeGroupAndFillGrid()
        {
            clsIncomeMatrix.openWhere(" Where IncomeGroup=" +
                clsIncomeGroups.getCode(cboMatrixGroups.SelectedItem.ToString()));
            loadIncomeGrid();
            btnSave.Enabled = false;
            btnCancel.Visible = false;
        }

        private void addNewMatrixToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewMatrixGroupForm frmMatrixGroup = new NewMatrixGroupForm();
            frmMatrixGroup.ShowDialog();
            clsIncomeGroups.openAll();
            fillCombos();
            setGroupIndexByName(frmMatrixGroup.GroupName);
            openForIncomeGroupAndFillGrid();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void setGroupIndexByName(string groupName)
        {
            for (int i = 0; i < cboMatrixGroups.Items.Count; i++)
            {
                if (groupName == cboMatrixGroups.Items[i].ToString())
                {
                    cboMatrixGroups.SelectedIndex = i;
                }
            }
        }
    }
}
