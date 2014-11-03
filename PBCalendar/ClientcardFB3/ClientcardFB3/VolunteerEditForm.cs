using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ClientCardFB3
{
    public partial class VolunteerEditForm : Form
    {
        bool bNormalMode = false;
        bool selectmode = false; 
        string connectionString;
        int currentVolId;
        int currentGridRow;
        VolGroups clsVolGroups;
        Volunteers clsVolunteers;

        List<TextBox> tbList = new List<TextBox>();
        List<CheckBox> chkList = new List<CheckBox>();

        const string btnEditSaveTag_Edit = "edit";
        const string btnEditSaveTag_Save = "save";

        enum STATE
        {
            ADD,											// Add new record.
            DISPLAY,										// Normal display/browse mode.
            EDIT											// Edit current record mode.
        }		// end of enum STATE

        public bool FormSelectMode
        {
            get { return FormSelectMode; }
            set { FormSelectMode = value; }
        }

        public VolunteerEditForm(string connStringIN)
        {
            InitializeComponent();

            foreach (TextBox tb in this.Controls.OfType<TextBox>())
            {
                tbList.Add(tb);
            }
            foreach (CheckBox chk in this.Controls.OfType<CheckBox>())
            {
                chkList.Add(chk);
            }
            clsVolGroups = new VolGroups(CCFBGlobal.connectionString);
            clsVolunteers = new Volunteers(CCFBGlobal.connectionString);
            parm_VolunteerType clsVolTypes = new parm_VolunteerType(CCFBGlobal.connectionString);
            ArrayList lstVolTypes = new ArrayList();
            clsVolTypes.openAll();
            for (int i = 0; i < clsVolTypes.RowCount; i++)
            {
                lstVolTypes.Add(new parmType(clsVolTypes.ID,clsVolTypes.Type, clsVolTypes.SortOrder, clsVolTypes.ShortName));
            }
            cboVolType.DataSource = lstVolTypes;
            cboVolType.DisplayMember = "LongName";
            cboVolType.ValueMember = "UID";
        }

        private void VolunteerEditForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            currentVolId = 0;									// Set to invalid DB record ID.
            if (selectmode == true)								// Do not exit the form if in SELECT
            {												//   mode so cancel the form exit.
                e.Cancel = true;							// This prevents a dialog.Close.
                this.Visible = false;						// Hide the dialog in SELECT mode.
            }
            // In Normal mode control falls through and the dialog will just close.
        }		// end of EditvolunteerForm_FormClosing

        private void VolunteerEditForm_Resize(object sender, EventArgs e)
        {
            if (WindowState != FormWindowState.Minimized)
            {
                gridEditVol.Width = Width - (gridEditVol.Left + 20);
                gridEditVol.Height = Height - (gridEditVol.Top + 40);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DisplayControls(STATE.ADD);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show(
                    "Are you sure you want to cancel your changes?",
                    "Cancel Changes",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question))
            {
                fillDataControls();
                DisplayControls(STATE.DISPLAY);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            SelectedId = 0;									
            if (FormSelectMode)
                this.Visible = false;
            else
                Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteCurrentRow(true);
        }

        private void btnEditSave_Click(object sender, EventArgs e)
        {
            switch (btnEditSave.Tag.ToString())
            {
                case btnEditSaveTag_Edit:								// Change to edit mode.
                    DisplayControls(STATE.EDIT);
                    break;

                case btnEditSaveTag_Save:								// save changes.
                    if (clsVolunteers.ID == 0)
                    {
                        RowInsert();
                        DbSave();
                        DisplayControls(STATE.DISPLAY);
                    }
                    else
                    {
                        MoveDisplayToDataSet();
                        DbSave();
                        DisplayControls(STATE.DISPLAY);
                    }
                    break;
            }
        }		// end of btnEdit_Click

        private void DisplayControls(STATE a_state)
        {
            editVol_btnAdd.Enabled = (a_state != STATE.EDIT);
            editVol_btnCancel.Visible = (a_state != STATE.DISPLAY);
            editVol_btnClose.Enabled = (a_state == STATE.DISPLAY);
            editVol_btnDelete.Enabled = (a_state == STATE.DISPLAY);
            btnEditSave.Enabled = true;
            editVol_btnPrint.Enabled = (a_state == STATE.DISPLAY);
            editVol_btnSelect.Visible = FormSelectMode;
            gridEditVol.Enabled = (a_state == STATE.DISPLAY);
            EditEnabled = (a_state == STATE.EDIT);
            editVol_btnSelect.Enabled = FormSelectMode;

            switch (a_state)
            {
                case STATE.ADD:
                    EditEnabled = true;						// Enable the input fields.
                    ClearFields();
                    editVol_btnCancel.Text = BTN_CANCEL_ADD;
                    btnEditSave.Text = BTN_EDIT_SAVE;
                    break;

                case STATE.DISPLAY:
                    btnEditSave.Text = BTN_EDIT_BEGIN;
                    break;

                case STATE.EDIT:
                    editVol_btnCancel.Text = BTN_CANCEL_UPDATE;
                    btnEditSave.Text = BTN_EDIT_UPDATE;
                    break;

                default:									// Should never happen in production app.
                    MessageBox.Show("Invalid STATE in DisplayControls");
                    break;
            }
        }		// end of DisplayControls

        private void btnSelect_Click(object sender, EventArgs e)
        {
            currentVolId = clsVolunteers.ID ;
            this.Visible = false;
        }		

        public bool DisplayGrid()
        {
            bNormalMode = false;			// Turn OFF the grid events.
            try
            {
                gridEditVol.Rows.Clear();					// Start with a clean grid.
                DataGridViewRow dgvRow;
                DataRow drow;
                for (int row = 0; row < clsVolunteers.RowCount; row++)	// Loop for all rows in the DataSet.
                {
                    drow = clsVolunteers.DSet.Tables[0].Rows[row];
                    gridEditVol.Rows.Add();
                    dgvRow = gridEditVol.Rows[gridEditVol.Rows.Count-1];
                    foreach (DataGridViewColumn dgvCol in gridEditVol.Columns)
                    {
                        dgvRow.Cells[dgvCol.Index].Value = drow[dgvCol.Tag.ToString()].ToString();
                    }
                }

                bNormalMode = true;			// Turn grid events ON.
                return (true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("DisplayGrid Error: " + ex.Message);
                return (false);
            }
        }		// end of DisplayGrid

        private void gridEditVol_SelectionChanged(object sender, EventArgs e)
        {
            if (bNormalMode == true)
            {
            }
        }

        private void cboVolType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
