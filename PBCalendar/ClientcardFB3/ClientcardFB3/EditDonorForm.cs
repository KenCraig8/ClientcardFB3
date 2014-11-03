using System;
using System.Drawing;
using System.Windows.Forms;

namespace ClientcardFB3
{
	public partial class EditDonorForm : SqlGridDataSet
	{
		#region ----------Constants----------

		const string BTN_CANCEL_ADD		= "&Cancel Add";
		const string BTN_CANCEL_UPDATE	= "&Cancel Update";
		const String BTN_EDIT_BEGIN		= "&Begin Edit";
		const string BTN_EDIT_SAVE		= "&Save";				// Save new record.
		const String BTN_EDIT_UPDATE	= "&Save Updates";		// Update edit record.

        const int posHistoryPanelBase = 118;

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
        const string COLUMN_RCD_TYPE            = "RcdType"; 
        const string COLUMN_CONTACT_NAME        = "ContactName";
		const string COLUMN_CONTACT_PHONE		= "ContactPhone";
		const string COLUMN_NOTE				= "Notes";
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
        const string COLUMN_DefaultDonationType = "DefaultDonationType";

		const string COLUMN_GRID_RCD_TYPE		= "gridEditDonor_RcdType";
        const string COLUMN_GRID_DFLTDONATETYPE = "gridEditDonor_DfltDonationType";

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
        private string m_selectedName = "";
        FoodDonations clsFoodDonations = new FoodDonations(CCFBGlobal.connectionString);        
		/// <summary>
		/// If FALSE then use the form in normal edit mode and if TRUE then operate in SELECT mode.
		/// </summary>
		private bool m_formSelectMode = false;
        private bool dataHasChanged = false;
        private bool bLoadingCombo = false;
        private string oriTBValue = "";
        private string lastSearchText = "";
        private string sOrderBy = " ORDER BY [Name]";
        private string sFilterColumn = "";
        private string sFilterValue = "";
        private string sWhereClause = "";
        int rowIndex = 0;
        bool loadingName = false;
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
		public EditDonorForm (string a_dbServer, string a_dbName, string a_dbUsername, string a_dbPassword)
		{
			InitializeComponent();
			ConnectionStringCreate (a_dbServer, a_dbName, a_dbUsername, a_dbPassword);
            SetGridSize();
		}		// end of constructor


		public EditDonorForm (string a_dbConnectionString)
		{
			InitializeComponent();
			ConnectionString= a_dbConnectionString;
            SetGridSize();
		}		// end of constructor


		public EditDonorForm (string a_dbConnectionString, bool a_selectMode)
		{
			InitializeComponent();
			ConnectionString	= a_dbConnectionString;
            FormSelectMode = a_selectMode;
            SetGridSize();
		}		// end of constructor


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
            if (dataHasChanged == true)
            {
                if (DialogResult.No == MessageBox.Show(
                        "Are you sure you want to cancel your changes?",
                        "Cancel Edit",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question))
                    { return; }
            }
            Event_GridSelectionChanged(null, null);	// Restore original record data.
            DisplayControls(STATE.DISPLAY);
            gridEditDonor.Focus();
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
            SelectedId = 0; // Set to invalid DB record ID.
            SelectedName = "";
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
			switch (editDonor_btnEdit.Text)
			{
				case BTN_EDIT_BEGIN:								// Go into record edit mode.
					DisplayControls (STATE.EDIT);
                    editDonor_txtName.Focus();
                    dataHasChanged = false;
                    editDonor_btnEdit.Enabled = false;
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
                    gridEditDonor.Focus();
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
            SelectedName = DataSetValue(GridCurrentRow(), COLUMN_NAME);
			//GridCurrentRow (0);
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
			editDonor_btnAdd.Enabled	= (a_state != STATE.EDIT);
			editDonor_btnCancel.Visible	= (a_state != STATE.DISPLAY);
			editDonor_btnClose.Enabled	= (a_state == STATE.DISPLAY);
			editDonor_btnDelete.Enabled	= (a_state == STATE.DISPLAY);
			editDonor_btnEdit.Enabled	= true;
			editDonor_btnPrint.Enabled	= (a_state == STATE.DISPLAY);
			editDonor_btnSelect.Visible	= FormSelectMode;
			gridEditDonor.Enabled		= (a_state == STATE.DISPLAY);
			EditEnabled					= (a_state == STATE.EDIT);

			switch (a_state)
			{
				case STATE.ADD:
					EditEnabled = true;						// Enable the input fields.
					ClearFields ();
					editDonor_btnCancel.Text= BTN_CANCEL_ADD;
					editDonor_btnEdit.Text	= BTN_EDIT_SAVE;
					break;

				case STATE.DISPLAY:
					editDonor_btnEdit.Text	= BTN_EDIT_BEGIN;
					break;

				case STATE.EDIT:
					editDonor_btnCancel.Text= BTN_CANCEL_UPDATE;
					editDonor_btnEdit.Text	= BTN_EDIT_UPDATE;
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
            string rcdType = CCFBGlobal.LongNameFromId(CCFBGlobal.parmTbl_Donation
                        , Convert.ToInt16(DataSetValue(a_dataSetRow, COLUMN_DefaultDonationType)));	// Get the ID from DataSet row.
            gridEditDonor[COLUMN_GRID_DFLTDONATETYPE, a_gridRow].Value = rcdType;

            rcdType = CCFBGlobal.LongNameFromId(CCFBGlobal.parmTbl_Donor
                        ,Convert.ToInt16(DataSetValue (a_dataSetRow, COLUMN_RCD_TYPE)));	// Get the ID from DataSet row.
			gridEditDonor[COLUMN_GRID_RCD_TYPE,a_gridRow].Value = rcdType;

            if (Convert.ToBoolean(DataSetValue(a_dataSetRow, COLUMN_INACTIVE)) == true)
                gridEditDonor.Rows[a_gridRow].DefaultCellStyle.ForeColor = Color.Red; 
            return (rcdType.Length > 0);
		}		// end of DisplayGridLineSpecial


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Close the form and return an invalid record ID.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		//-----------------------------------------------------------------------------------------
		private void EditDonorForm_FormClosing (object sender, FormClosingEventArgs e)
		{
			if (FormSelectMode)								// Do not exit the form if in SELECT
			{												//   mode so cancel the form exit.
				e.Cancel = true;							// This prevents a dialog.Close.
				this.Visible = false;						// Hide the dialog in SELECT mode.
			}
            else
                SelectedId = 0;				            // Set to invalid DB record ID.
			// In Normal mode control falls through and the dialog will just close.
		}		// end of EditDonorForm_FormClosing


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Read the Type Codes database table and display the data in a DataGridView control.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		//-----------------------------------------------------------------------------------------
		private void EditDonorForm_Load (object sender, EventArgs e)
		{
			TableName			= "Donors";					// Read from this SQL table.
			//ControlPage			= tpgCompanyInfo.Controls;
            ControlPage			= this.Controls;
			DataGridViewObject	= gridEditDonor;
            editDonor_cboRcdType.DataSource = CCFBGlobal.TypeCodesArray(CCFBGlobal.parmTbl_Donor);
            editDonor_cboRcdType.DisplayMember = "LongName";
            editDonor_cboRcdType.ValueMember = "UID";
            
            cboDonationType.DataSource = CCFBGlobal.TypeCodesArray(CCFBGlobal.parmTbl_Donation);
            cboDonationType.DisplayMember = "LongName";
            cboDonationType.ValueMember = "UID";
            loadUserFieldLabels();
            SetWhereClause();
            RefreshGrid();
            GridSelectionHandlerEnabled = true;
		}		// end of EditDonorForm_Load


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		//-----------------------------------------------------------------------------------------
		private void EditDonorForm_VisibleChanged (object sender, EventArgs e)
		{
			GridCurrentRow (0);
		}		// end of EditDonorForm_VisibleChanged


        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Do some special handling to convert the row data to the display format.
        /// </summary>
        /// <param name="a_row">The row being processed.</param>
        //-----------------------------------------------------------------------------------------
        public override void Event_SelectionChangedSpecial(int a_row)
        {
            tbDonorId.Text = DataSetValue(GridCurrentRow(), COLUMN_ID);
            loadDonorLogList();
        }		// end of Event_SelectionChangedSpecial


        private void RefreshGrid()
        {
            DbOpenToGrid("Donors", "SELECT * FROM Donors " + sWhereClause + sOrderBy);
            // The column property in a DataGridView does not expose the .Tag property. These
            // calls map the column in the DataSet to the column in the DataGridView. Note that
            // the "gridEditDonor_RcdType" is NOT in the list since it is handled in the
            // "DisplayGridLineSpecial" method (requires TypeCode name lookup).
            SetGridColumn(COLUMN_ADDRESS, "gridEditDonor_Address");
            SetGridColumn(COLUMN_CITY, "gridEditDonor_City");
            SetGridColumn(COLUMN_ID, "gridEditDonor_ID");
            SetGridColumn(COLUMN_INACTIVE, "gridEditDonor_InActive");
            SetGridColumn(COLUMN_NAME, "gridEditDonor_Name");
            SetGridColumn(COLUMN_PHONE, "gridEditDonor_Phone");
            //			SetGridColumn (COLUMN_RCD_TYPE,	"gridEditDonor_RcdType"	);
            SetGridColumn(COLUMN_ZIPCODE, "gridEditDonor_Zip");
            EditEnabled = false;
            DisplayGrid();
            DisplayControls(STATE.DISPLAY);
        }

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

        public string SelectedName
        {
            get { return m_selectedName; }
            set { m_selectedName = value; }
        }

		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// If FALSE then use the form in normal edit mode and if TRUE then operate in SELECT mode.
		/// </summary>
		//-----------------------------------------------------------------------------------------
		public bool FormSelectMode
		{
			get	{	return (m_formSelectMode);		}
			set	{	m_formSelectMode = value;		}
		}		// end of property RowSelectMode


		private void EmptyMethodToPreserveClosingBrace ()
		{
		}
        /// <summary>  Loads User Check Box Labels from UserFields Table
        /// </summary>
        private void loadUserFieldLabels()
        {
            UserFields clsUserFields = new UserFields(CCFBGlobal.connectionString);
            clsUserFields.open("Donors");
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
            clsUserFields.setDataRow("Info1");
            if ("Info1" == clsUserFields.FldName)
            {
                lblInfo1.Text = clsUserFields.EditLabel;
                tbInfo1.Visible = (lblInfo1.Text != "");
                lblInfo1.Visible = (lblInfo1.Text != "");
            }
            clsUserFields.setDataRow("Info2");
            if ("Info2" == clsUserFields.FldName)
            {
                lblInfo2.Text = clsUserFields.EditLabel;
                tbInfo2.Visible = (lblInfo2.Text != "");
                lblInfo2.Visible = (lblInfo2.Text != "");
            }
        }

        private void EditDonorForm_SizeChanged(object sender, EventArgs e)
        {
            if (WindowState != FormWindowState.Minimized)
                SetGridSize();
        }

        private void chkIncludeInactive_CheckedChanged(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void tbControls_Enter(object sender, EventArgs e)
        {
            oriTBValue = sender.ToString();
        }

        private void tbControls_Leave(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Enabled == true && dataHasChanged == false)
            {
                dataHasChanged = (oriTBValue != tb.Text);
                editDonor_btnEdit.Enabled = true;
            }
        }

        private void SetGridSize()
        {
            gridEditDonor.Width = Width - gridEditDonor.Left - 20;
            gridEditDonor.Height = this.Height - (gridEditDonor.Top + 40);
        }

        private void tbFindName_TextChanged(object sender, EventArgs e)
        {
            if (tbFindName.Text.Trim() == "")
            { gridEditDonor.CurrentCell = gridEditDonor[0, 0]; }
            else
            {
                switch (cboOrderBy.SelectedIndex)
                {
                    //case 0:
                    //    { FindByName("gridEditDonor_Name"); break; }
                    case 1:
                        { FindByName("gridEditDonor_Address"); break; }
                    //case 2:
                    //    { FindByName("gridEditDonor_Name"); break; }
                    //case 3:
                    //    { FindByName("gridEditDonor_Name"); break; }
                    case 4:
                        { FindByName("gridEditDonor_Phone"); break; }
                    case 5:
                        { FindByName("gridEditDonor_ID"); break; }
                    //case 6:
                    //    { FindByName("gridEditDonor_Name"); break; }
                    default:
                        { FindByName("gridEditDonor_Name"); break; }
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
                for (int i = rowStart; i < gridEditDonor.Rows.Count; i++)
                {
                    if (gridEditDonor.Rows[i].Cells[colNameFull].FormattedValue.ToString().ToUpper().StartsWith(lastSearchText) == true)
                    {
                        gridEditDonor.CurrentCell = gridEditDonor[0, i];
                        if (i < gridEditDonor.FirstDisplayedScrollingRowIndex
                            || i > gridEditDonor.Rows.GetLastRow(DataGridViewElementStates.Displayed) - 5)
                            if (i > 5)
                                gridEditDonor.FirstDisplayedScrollingRowIndex = i - 5;
                            else
                                gridEditDonor.FirstDisplayedScrollingRowIndex = i;
                        break;
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
            bLoadingCombo = true;
            cboFilter.Items.Clear();
            cboFilter.Items.Add("No Filter");
            cboFilter.SelectedIndex = 0;

            //Gets And Adds to the filter combo the distinct values of the column from the household table
            Donors clsDonor = new Donors(CCFBGlobal.connectionString);
            string whereClause = "";
            if (chkIncludeInactive.Checked == false)
                whereClause = " WHERE Inactive=0 ";
            clsDonor.getDistincts(colName, whereClause);
            string sVal = "";
            System.Collections.ArrayList typesDonor = CCFBGlobal.TypeCodesArray(CCFBGlobal.parmTbl_Donor);
            int iD = 0;
            for (int i = 0; i < clsDonor.RowCount; i++)
            {
                sVal = clsDonor.DSet.Tables[0].Rows[i][0].ToString();
                if (colName == "RcdType")
                {
                    iD = Convert.ToInt32(sVal);
                    sVal = CCFBGlobal.formatNumberWithLeadingZero(iD);
                    for (int j = 0; j < typesDonor.Count; j++)
                    {
                        parmType pt = (parmType)typesDonor[j];
                        if (pt.ID == iD)
                        {
                            sVal += " = " + pt.LongName; break;
                        }
                    }
                }   
                cboFilter.Items.Add(sVal + new String((char)32,(30-sVal.Length)) + "[ " + clsDonor.DSet.Tables[0].Rows[i][1].ToString() + " ]");
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

        private void EditDonorForm_Resize(object sender, EventArgs e)
        {
            sctnrFormBase.SplitterDistance = 258;
            sctnrBottomRight.SplitterDistance = 32;
            sctnrBottomLeft.SplitterDistance = 100;
        }

        private void cboDonorPeriod_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cboDonorPeriod.SelectedIndex == 6)
            {
                lblFrom.Visible = true;
                lblTo.Visible = true;
                dtpFrom.Visible = true;
                dtpTo.Visible = true;
                btnLoadCustom.Visible = true;
            }
            else
            {
                lblFrom.Visible = false;
                lblTo.Visible = false;
                dtpFrom.Visible = false;
                dtpTo.Visible = false;
                btnLoadCustom.Visible = false;
                loadDonorLogList();
            }
        }

        private void btnHistorySize_Click(object sender, EventArgs e)
        {
            if (btnHistorySize.ImageIndex == 1)
            {
                sctnrBottomBase.SplitterDistance = this.Width - 125;
                btnHistorySize.ImageIndex = 0;
            }
            else
            {
                sctnrBottomBase.SplitterDistance = posHistoryPanelBase;
                btnHistorySize.ImageIndex = 1;
            }
        }

        private void loadDonorLogList()
        {
            double totLbs = 0;
            int totCnt = 0;
            lvDonorHistory.Items.Clear();
            Application.DoEvents();
            ListViewItem lvItm;

            string donorWhereClause = " WHERE DonorID=" + tbDonorId.Text;
            switch (cboDonorPeriod.SelectedIndex)
            {
                case 0: donorWhereClause += CCFBGlobal.SQLDateRangeCurMonth(); break;
                case 1: donorWhereClause += CCFBGlobal.SQLDateRangePrevMonth(); break;
                case 2: donorWhereClause += CCFBGlobal.SQLDateRangeLast90Days(); break;
                case 3: donorWhereClause += CCFBGlobal.SQLDateRangeCurYear(); break;
                case 4: donorWhereClause += CCFBGlobal.SQLDateRangePrevYear(); break;
                case 6: donorWhereClause += DateRangeCustom(); break;
                default: break;
            }

            clsFoodDonations.openWhere(donorWhereClause);

            for (int i = 0; i < clsFoodDonations.RowCount; i++)
            {
                clsFoodDonations.setDataRow(i);
                lvItm = new ListViewItem((i + 1).ToString());
                lvItm.SubItems.Add(clsFoodDonations.TrxDate.ToShortDateString());
                lvItm.SubItems.Add(clsFoodDonations.Pounds.ToString());
                lvItm.SubItems.Add(CCFBGlobal.LongNameFromId(CCFBGlobal.parmTbl_Donation, clsFoodDonations.DonationType));
                lvItm.SubItems.Add(CCFBGlobal.LongNameFromId(CCFBGlobal.parmTbl_FoodClass, clsFoodDonations.FoodClass));
                lvItm.SubItems.Add(clsFoodDonations.Notes);
                lvItm.SubItems.Add(clsFoodDonations.FoodCode);
                lvItm.SubItems.Add(clsFoodDonations.TrxID.ToString());
                lvDonorHistory.Items.Add(lvItm);

                totCnt++;
                totLbs += clsFoodDonations.Pounds;
            }
            tbDonorCnt.Text = totCnt.ToString();
            tbDonorLbs.Text = totLbs.ToString();
        }

        public string DateRangeCustom()
        {
            return " And TrxDate BETWEEN '"
                + dtpFrom.Value.ToShortDateString() + "'"
                + " And '" + dtpTo.Value.ToShortDateString() + "'";
        }

        private void btnLoadCustom_Click(object sender, EventArgs e)
        {
            loadDonorLogList();
        }

        private void cmsDonors_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            cmsDonors.Close();
            if (e.ClickedItem.Text == "Export To Excel")
            {
                CCFBGlobal.ExportToExcell(gridEditDonor, "Donors_" +
                    DateTime.Today.Year.ToString() + "_" + CCFBGlobal.formatNumberWithLeadingZero(DateTime.Today.Month));
            }
        }

        private void btnEnterRcpts_Click(object sender, EventArgs e)
        {
            frmFoodReceipts frmFoodRecipts = new frmFoodReceipts(
                Convert.ToInt32(gridEditDonor.SelectedRows[0].Cells["gridEditDonor_ID"].Value),
               Convert.ToInt32(cboDonationType.SelectedValue));
            frmFoodRecipts.ShowDialog();
        }
    }		// end of class
}		// end of namespace
