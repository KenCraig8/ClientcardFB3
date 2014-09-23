using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ClientcardFB3
{
    public partial class AccessReportsForm : Form
    {
        AccessReports clsAccessReports = new AccessReports(CCFBGlobal.connectionString);
        SqlConnection conn = new SqlConnection(CCFBGlobal.connectionString);
        SqlDataAdapter dadAdpt = new SqlDataAdapter();
        DataSet dset = new DataSet();
        System.Collections.ArrayList codesFilter = new System.Collections.ArrayList();

        public AccessReportsForm()
        {
            InitializeComponent();
            pnlDatePicker.SetBounds(pnlDateRange.Location.X, pnlDateRange.Location.Y, pnlDatePicker.Width, pnlDatePicker.Height);
            pnlCustomCombo.SetBounds(pnlDateRange.Location.X, pnlDateRange.Location.Y, pnlCustomCombo.Width, pnlCustomCombo.Height);
            pnlYearRange.SetBounds(pnlDateRange.Location.X, pnlDateRange.Location.Y, pnlYearRange.Width, pnlYearRange.Height);
          //grpbxActive.SetBounds(pnlDateRange.Location.X, pnlDateRange.Location.Y, grpbxActive.Width, grpbxActive.Height);
            //Open distinct Report Groups
            HideAllPanels();
            clsAccessReports.getDistincts("RptGroup");
            dtFrom.Format = DateTimePickerFormat.Custom;
            dtFrom.CustomFormat = "MMMM dd, yyyy";
            dtTo.Format = DateTimePickerFormat.Custom;
            dtTo.CustomFormat = "MMMM dd, yyyy";
            //Fill the Report Group Combo
            fillRptCombo();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            //Close the form
            this.Close();
        }

        /// <summary>
        /// Event Triggers when user clicks on the Create Report Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreateReport_Click(object sender, EventArgs e)
        {
            clsAccessReports.update();
            if (lvwRptSelector.FocusedItem != null)
            {

                //Get Where clause if report uses it
                string sWhereClause = "";
                string tmp = "";

                if (clsAccessReports.UseWhere == true)
                    sWhereClause = clsAccessReports.WhereClause;

                //Switch on the Report Type
                switch (clsAccessReports.DateRangeType)
                {
                    case 0:
                        {
                            sWhereClause += AddInactiveFlag(sWhereClause, clsAccessReports.RptGroup);
                            createReport(sWhereClause,"");
                            break;
                        }
                    case 1:   //TrxDate = #mdy#
                        {
                            //Put user selected values into where clause
                            sWhereClause = sWhereClause.Replace("mdy", dtDate.Value.ToShortDateString());
                            //Create the report
                            sWhereClause += AddInactiveFlag(sWhereClause, clsAccessReports.RptGroup);
                            createReport(sWhereClause,"");
                            break;
                        }
                    case 2: //  "xxx*"
                        {
                            parmType itmResult;
                            //Put user selected values into where clause
                            switch (clsAccessReports.CboResultType)
                            {
                                case 0:
                                    sWhereClause = clsAccessReports.WhereClause.Replace("xxx", gatherInListNumbers());
                                    break;
                                case 1:
                                    sWhereClause = clsAccessReports.WhereClause.Replace("xxx", gatherInListString());
                                    break;
                                case 2:
                                    foreach (ListViewItem item in lvwFilter.Items)
	                                {
                                        if (item.Checked == true)
                                        {
                                            itmResult = (parmType)codesFilter[item.Index];
                                            tmp = itmResult.ShortName;
                                            break;
                                        }
                                    }
                                    sWhereClause = clsAccessReports.WhereClause.Replace("xxx", tmp);
                                    break;

                                default:
                                    break;
                            }
                            sWhereClause += AddInactiveFlag(sWhereClause, clsAccessReports.RptGroup);
                            
                            //Create Report
                            createReport(sWhereClause,"");
                            break;
                        }
                    case 5: //Between #mdy0# AND #mdy1#
                        {

                            //Put user selected values into where clause
                            sWhereClause = sWhereClause.Replace("mdy0", dtFrom.Value.ToShortDateString()).Replace("mdy1", dtTo.Value.ToShortDateString());

                            //Create Report
                            sWhereClause += AddInactiveFlag(sWhereClause, clsAccessReports.RptGroup);
                            createReport(sWhereClause,"");
                            break;
                        }
                    case 6:
                        {
                            sWhereClause = sWhereClause.Replace("mdy0", dtFrom.Value.ToShortDateString()).Replace("mdy1", dtTo.Value.ToShortDateString());
                            // parmType cboResult = (parmType)cboUserSelection.SelectedItem;
                            //Put user selected values into where clause
                            switch (clsAccessReports.CboResultType)
                            {
                                case 0:
                                    sWhereClause = sWhereClause.Replace("xxx", gatherInListNumbers());
                                    break;
                                case 1:
                                    sWhereClause = sWhereClause.Replace("xxx", gatherInListString());
                                    break;
                                case 2:
                                    //if (cboResult.ID != 99)
                                    //{
                                    //    sWhereClause = sWhereClause.Replace("xxx", cboResult.ShortName);
                                    //    //sWhereClause = sWhereClause.Replace("zz", cboResult.ID.ToString());
                                    //}
                                    break;

                                default:
                                    break;
                            }
                            sWhereClause += AddInactiveFlag(sWhereClause, clsAccessReports.RptGroup);
                            //Create Report
                            createReport(sWhereClause,"");
                            break;
                        }
                    case 7: //Between "xxx0" And "xxx1"
                        {
                            //Put user selected values into where clause
                            sWhereClause = sWhereClause.Replace("xxx0", tbYearFrom.Text);
                            sWhereClause = sWhereClause.Replace("xxx1", tbYearTo.Text);

                            //Create Report
                            createReport(sWhereClause,"");
                            break;
                        }
                    case 8: //Between "xxx0" And "xxx1" Bue Uses A Form
                        {
                            //Put user selected values into where clause
                            sWhereClause = sWhereClause.Replace("xxx0", tbYearFrom.Text);
                            sWhereClause = sWhereClause.Replace("xxx1", tbYearTo.Text);

                            //Create Report
                            openReportForm(sWhereClause);
                            break;
                        }
                    case 9:
                        {
                            tmp = clsAccessReports.SqlQuery;
                            createReport("", tmp);
                            break;
                        }
                }
            }
        }

        private void lvwRptGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Open all reports for the selected group
            if (lvwRptGroups.SelectedItems.Count > 0)
            {
                clsAccessReports.openWhere(" Where RptGroup='" + lvwRptGroups.SelectedItems[0].Text + "'");
                //Load the listview with the report names
                loadLVReports();
                btnCreateReport.Enabled = false;
            }
        }

        /// <summary>
        /// Opens the report in Access, fills it, and is previewed or printed
        /// </summary>
        /// <param name="whereClause">The where clasue for the Access Object OpenReport Method</param>
        private void createReport(string whereClause, string recordsource)
        {
            Microsoft.Office.Interop.Access.Application oAccess = null;
                oAccess = new Microsoft.Office.Interop.Access.Application();
            try
            {
                

                if (chkPreview.Checked == true)
                    oAccess.Visible = true;

                // Open a database in exclusive mode:
                oAccess.OpenCurrentDatabase(
                   System.IO.Path.Combine(CCFBGlobal.pathExe, "ClientcardFB3Reports.accdb"), //filepath
                   false //Exclusive
                   );
                CCFBGlobal.appendErrorToErrorReport("WhereClause: " + whereClause,"CreateReport: " + clsAccessReports.ReportTitle);
               // oAccess.DoCmd.DoMenuItem("Shutter Bar", "Navigation Pane", "Close", Type.Missing);
                if (recordsource != "")
                {
                    oAccess.DoCmd.OpenReport(
                            clsAccessReports.ReportTitle, //ReportName
                            Microsoft.Office.Interop.Access.AcView.acViewDesign,  //acViewPreview, //View
                            System.Reflection.Missing.Value, //FilterName
                            System.Reflection.Missing.Value); //WhereCondition
                    oAccess.Reports[0].RecordSource = recordsource;
                    oAccess.DoCmd.RunCommand(Microsoft.Office.Interop.Access.AcCommand.acCmdPrintPreview);
                }
                else
                {
                    if (clsAccessReports.UseFilter == false)
                    {
                        // Preview a report
                        if (clsAccessReports.UseWhere == true || whereClause !="")
                            oAccess.DoCmd.OpenReport(
                               clsAccessReports.ReportTitle, //ReportName
                               Microsoft.Office.Interop.Access.AcView.acViewPreview, //View
                               System.Reflection.Missing.Value, //FilterName
                               whereClause); //WhereCondition
                        else
                            oAccess.DoCmd.OpenReport(
                                    clsAccessReports.ReportTitle, //ReportName
                                    Microsoft.Office.Interop.Access.AcView.acViewPreview, //View
                                    System.Reflection.Missing.Value, //FilterName
                                    System.Reflection.Missing.Value); //WhereCondition
                    }
                    else
                    {
                        // Preview a report named Sales:
                        oAccess.DoCmd.OpenReport(
                           clsAccessReports.ReportTitle, //ReportName
                           Microsoft.Office.Interop.Access.AcView.acViewPreview, //View
                           clsAccessReports.FilterName, //FilterName
                           whereClause //WhereCondition
                           );
                    }
                }
                if (chkPreview.Checked == true)
                {
                      
                    MessageBox.Show("When Done Viewing Report Click OK", "Report Viewer", MessageBoxButtons.OK);

                    // Close the report preview window: 
                    oAccess.DoCmd.Close(
                       Microsoft.Office.Interop.Access.AcObjectType.acReport, //ObjectType
                       clsAccessReports.ReportTitle, //ObjectName
                       Microsoft.Office.Interop.Access.AcCloseSave.acSaveNo //Save
                       );
                }
                else
                {
                    // Print 1 copy of the active object: 
                    oAccess.DoCmd.PrintOut(
                       Microsoft.Office.Interop.Access.AcPrintRange.acPrintAll, //PrintRange
                       System.Reflection.Missing.Value,                         //PageFrom
                       System.Reflection.Missing.Value,                         //PageTo
                       Microsoft.Office.Interop.Access.AcPrintQuality.acHigh,   //PrintQuality
                       numericUpDown1.Value,                                    //Copies
                       false                                                    //CollateCopies
                       );
                   
                    MessageBox.Show("Click OK When Report Is Done Printing", "Printing Report", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                CCFBGlobal.appendErrorToErrorReport("createReport: " + whereClause + recordsource, ex.GetBaseException().ToString());
            }
            ((Microsoft.Office.Interop.Access._Application)oAccess).
                Quit(Microsoft.Office.Interop.Access.AcQuitOption.acQuitSaveNone);
        }

        /// <summary>
        /// Opens the report in Access, fills it, and is previewed or printed
        /// </summary>
        /// <param name="whereClause">The where clasue for the Access Object OpenReport Method</param>
        private void openReportForm(string whereClause)
        {
            Microsoft.Office.Interop.Access.Application oAccess = null;
                oAccess = new Microsoft.Office.Interop.Access.Application();
            try
            {
                if (chkPreview.Checked == true)
                    oAccess.Visible = true;

                // Open a database in exclusive mode:
                oAccess.OpenCurrentDatabase(
                   System.IO.Path.Combine(CCFBGlobal.pathExe,"ClientcardFB3Reports.accdb"), //filepath
                   false //Exclusive
                   );

                // oAccess.DoCmd.DoMenuItem("Shutter Bar", "Navigation Pane", "Close", Type.Missing);

                if (clsAccessReports.UseFilter == false)
                {
                    oAccess.DoCmd.OpenForm
                           ( clsAccessReports.ReportTitle                           //ReportName
                           , Microsoft.Office.Interop.Access.AcFormView.acPreview   //View
                           , System.Reflection.Missing.Value                        //FilterName
                           , whereClause                                            //WhereCondition
                       );
                }
                else
                {
                    // Preview a report named Sales:
                    oAccess.DoCmd.OpenForm( clsAccessReports.ReportTitle                            //ReportName
                                          , Microsoft.Office.Interop.Access.AcFormView.acPreview    //View
                                          , clsAccessReports.FilterName                             //FilterName
                                          , whereClause                                             //WhereCondition
                                          );
                }

                if (chkPreview.Checked == true)
                {
                    MessageBox.Show("When Done Viewing Report Click OK", "Report Viewer", MessageBoxButtons.OK);

                    // Close the report preview window: 
                    oAccess.DoCmd.Close(
                       Microsoft.Office.Interop.Access.AcObjectType.acReport,   //ObjectType
                       clsAccessReports.ReportTitle,                            //ObjectName
                       Microsoft.Office.Interop.Access.AcCloseSave.acSaveNo     //Save
                       );
                }
                else
                {
                    // Print 1 copies of the active object: 
                    oAccess.DoCmd.PrintOut(
                       Microsoft.Office.Interop.Access.AcPrintRange.acPrintAll, //PrintRange
                       System.Reflection.Missing.Value,                         //PageFrom
                       System.Reflection.Missing.Value,                         //PageTo
                       Microsoft.Office.Interop.Access.AcPrintQuality.acHigh,   //PrintQuality
                       numericUpDown1.Value,                                    //Copies
                       false                                                    //CollateCopies
                       );

                    MessageBox.Show("Click OK When Report Is Done Printing", "Printing Report", MessageBoxButtons.OK);
                }
                
            }
            catch (Exception ex)
            {
                CCFBGlobal.appendErrorToErrorReport("openReportForm: " + whereClause, ex.GetBaseException().ToString());
            }
            ((Microsoft.Office.Interop.Access._Application)oAccess).Quit(Microsoft.Office.Interop.Access.AcQuitOption.acQuitSaveNone);
        }

        /// <summary>
        /// Fills the Report Groups Combo Box
        /// </summary>
        private void fillRptCombo()
        {
            lvwRptGroups.Items.Clear();
         
            //For each report group in dataset
            for (int i = 0; i < clsAccessReports.RowCount; i++)
            {
                //Add the Report Group
                lvwRptGroups.Items.Add(clsAccessReports.DSet.Tables[0].Rows[i]["RptGroup"].ToString());
            }
            //Set the index
            if (lvwRptGroups.Items.Count > 0)
                lvwRptGroups.Items[0].Selected= true;

        }

        /// <summary>
        /// Loads the reports into the Listview for selection
        /// </summary>
        public void loadLVReports()
        {
            lvwRptSelector.Items.Clear();
            lvwRptSelector.Groups.Clear();
            string sGrouping = "";
            ListViewGroup lvwGrp = null;
            for (int i = 0; i < clsAccessReports.RowCount; i++)
            {
                //Set datarow in Access Reports Class
                clsAccessReports.setDataRow(i);
                //insert report name using get set accessors
                sGrouping = clsAccessReports.GetDataValue("Grouping").ToString();
                if (lvwGrp == null || lvwGrp.Header != sGrouping)
                {
                    lvwGrp = new ListViewGroup(sGrouping, HorizontalAlignment.Left);
                    lvwRptSelector.Groups.Add(lvwGrp);
                }
                ListViewItem lvwItm = new ListViewItem(lvwGrp);
                lvwItm.SubItems.Add(clsAccessReports.DisplayName);
                lvwItm.Tag = clsAccessReports.ID.ToString();
                lvwRptSelector.Items.Add(lvwItm);
                //Insert the Reports ID into the tag for the row
            }

        }

        /// <summary>
        /// Event triggers when the selection is changed on the listview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvwRptSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Check to make sure there is a focused item
            clsAccessReports.update();
            if (lvwRptSelector.FocusedItem != null)
            {
                HideAllPanels();
                btnCreateReport.Enabled = true;

                //Find teh currently selected report and set datarow
                clsAccessReports.find(Convert.ToInt32(lvwRptSelector.FocusedItem.Tag));
                chkPreview.Checked = clsAccessReports.Preview;
                numericUpDown1.Value = clsAccessReports.NbrCopies;
                grpbxActive.Visible = clsAccessReports.UseActive;

                switch (clsAccessReports.DateRangeType)
                {
                    case 0:
                        {
                            showlvwFilter(false);
                            break;
                        }
                    case 1:
                        {
                            showlvwFilter(false);
                            lblType1.Text = clsAccessReports.LabelLowDate;
                            dtDate.Value = Convert.ToDateTime(clsAccessReports.Date0);
                            pnlDatePicker.Visible = true;
                            break;
                        }
                    case 2:
                        {
                            loadlvwFilter();
                            //lblType2.Text = clsAccessReports.LabelLowDate;
                            //pnlCustomCombo.Visible = true;
                            break;
                        }
                    case 5:
                        {
                            showlvwFilter(false);
                            dtFrom.Value = Convert.ToDateTime(clsAccessReports.Date0);
                            dtTo.Value = Convert.ToDateTime(clsAccessReports.Date1);
                            lblType5Low.Text = clsAccessReports.LabelLowDate;
                            lblType5Hi.Text = clsAccessReports.LabelHiDate;
                            pnlDateRange.Visible = true;
                            break;
                        }
                    case 6:
                        {
                            dtFrom.Value = Convert.ToDateTime(clsAccessReports.Date0);
                            dtTo.Value = Convert.ToDateTime(clsAccessReports.Date1);
                            lblType5Low.Text = clsAccessReports.LabelLowDate;
                            lblType5Hi.Text = clsAccessReports.LabelHiDate;
                            pnlDateRange.Visible = true;
                            loadlvwFilter();
                            break;
                        }
                    case 7:
                        {
                            showlvwFilter(false);
                            tbYearFrom.Text = clsAccessReports.Date0;
                            tbYearTo.Text = clsAccessReports.Date1;
                            lblType7Low.Text = clsAccessReports.LabelLowDate;
                            lblType7Hi.Text = clsAccessReports.LabelHiDate;
                            pnlYearRange.Visible = true;
                            break;
                        }
                    case 8:
                        {
                            tbYearFrom.Text = clsAccessReports.Date0;
                            tbYearTo.Text = clsAccessReports.Date1;
                            lblType7Low.Text = clsAccessReports.LabelLowDate;
                            lblType7Hi.Text = clsAccessReports.LabelHiDate;
                            pnlYearRange.Visible = true;
                            break;
                        }
                }
            }
            else
            {
                btnCreateReport.Enabled = false;
            }
        }

        /// <summary>
        /// Fires when the user presses a key while in a textbox 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbYear_KeyDown(object sender, KeyEventArgs e)
        {
            //Make sure the key pressed was a value that is in correct format
            CCFBGlobal.checkForIntOnKeyPress(e);
        }

        private void dtp_Leave(object sender, EventArgs e)
        {
            DateTimePicker dtp = (DateTimePicker)sender;
            clsAccessReports.SetDataValue(dtp.Tag.ToString(), dtp.Value.ToShortDateString());
        }

        private void tb_Leave(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            clsAccessReports.SetDataValue(tb.Tag.ToString(), tb.Text);
        }

        private void cboUserSelection_Leave(object sender, EventArgs e)
        {
            clsAccessReports.SetDataValue(cboUserSelection.Tag.ToString(), 
                cboUserSelection.SelectedItem.ToString());
        }

        private void HideAllPanels()
        {   //Set all panels visibility to false
            pnlDateRange.Visible = false;
            pnlDatePicker.Visible = false;
            pnlCustomCombo.Visible = false;
            pnlYearRange.Visible = false;
            grpbxActive.Visible = false;
        }

        private string AddInactiveFlag(string sOriClause, string reportGroup)
        {
            string sRetVal = "";
            string nameTbl = "hh";
            switch (clsAccessReports.RptGroup)
            {
                case "Donors": nameTbl = "Donors";
                    break;
                case "Volunteers": nameTbl = "Volunteers";
                    break;
                default: 
                    break;
            }

            if (grpbxActive.Visible == true)
            {
                if (rdoOnlyActive.Checked == true)
                {
                    if (sOriClause != "")
                        sRetVal = " AND ";
                    sRetVal += nameTbl + ".Inactive = 0";
                }
                else if (rdoOnlyInactive.Checked == true)
                {
                    if (sOriClause != "")
                        sRetVal = " AND ";
                    sRetVal += nameTbl + ".Inactive = 1";
                }
            }
            return sRetVal;
        }

        private void showlvwFilter(bool isVisible)
        {
            lvwFilter.Visible = isVisible;
            chkAllItems.Visible = isVisible;
            lblType1.Visible = false;
        }

        private void loadlvwFilter()
        {
            lvwFilter.Items.Clear();
            showlvwFilter(true);
            codesFilter = new System.Collections.ArrayList();
            try
            {
                //cboUserSelection.DataSource = null;
                //cboUserSelection.Items.Clear();
                //cboUserSelection.Text = "";
                int iRowCount = 0;
                //Fill dataset with the reports SqlQuery to fill combobox
                SqlCommand sqlComm = new SqlCommand(clsAccessReports.FilterName, conn);
                dadAdpt.SelectCommand = sqlComm;
                dset.Tables.Clear();
                dset.Clear();
                iRowCount = dadAdpt.Fill(dset, "FilterTable");

                if (clsAccessReports.AllowBlank == true)
                {
                    //codes.Add(new parmType(99, "All Values", 99, "All"));
                    chkAllItems.Text = "ALL " + clsAccessReports.RptGroup;
                    chkAllItems.Visible = true;
                    chkAllItems.Checked = false;
                    lblType1.Visible = false;
                }
                else
                {
                    chkAllItems.Visible = false;
                    lblType1.Text = clsAccessReports.LabelLowDate;
                    lblType1.Visible = true;
                }
                //Go through the dataset and add to ArrayList
                for (int i = 0; i < iRowCount; i++)
                {
                    codesFilter.Add(new parmType(dset.Tables[0].Rows[i]));
                    ListViewItem lvi = new ListViewItem(dset.Tables[0].Rows[i].Field<string>(1));
                    lvi.Tag = Convert.ToInt32(dset.Tables[0].Rows[i][0].ToString());
                    lvwFilter.Items.Add(lvi);
                }
                //cboUserSelection.DataSource = codes;
                //cboUserSelection.DisplayMember = "LongName";
                //cboUserSelection.ValueMember = "UID";

                ////Set the index
                //if (cboUserSelection.Items.Count > 0 && cboUserSelection.SelectedIndex == -1)
                //    cboUserSelection.SelectedIndex = 0;
            }
            catch (SqlException ex)
            {
                //If there is an error, append to the Error Log
                CCFBGlobal.appendErrorToErrorReport("FilterName=" +
                    clsAccessReports.FilterName, ex.GetBaseException().ToString());
            }
        }

        private void pnlDatePicker_Paint(object sender, PaintEventArgs e)
        {

        }

        private string gatherInListNumbers()
        {
            string tmp = "";
            foreach (ListViewItem item in lvwFilter.Items)
            {
                if (item.Checked == true || chkAllItems.Checked == true)
                {
                    if (tmp != "")
                        tmp += ",";
                    tmp += item.Tag.ToString();
                }
            }
            return tmp;
        }

        private string gatherInListString()
        {
            string tmp = "";
            foreach (ListViewItem item in lvwFilter.Items)
            {
                if (item.Checked == true || chkAllItems.Checked == true)
                {
                    if (tmp != "")
                        tmp += ",";
                    tmp += "'" + item.Text + "'";
                    //tmp += "'" + CCFBGlobal.SQLApostrophe(item.Text) + "'";
                }
            }
            return tmp;
        }

        private void chkAllItems_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkPreview_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPreview.Focused == true)
            {
                setCopiesVisible();
            }
        }

        private void setCopiesVisible()
        {
            bool isVisible = (chkPreview.Checked == false);
            numericUpDown1.Visible = isVisible;
            lblCopies.Visible = isVisible;
        }

        private void AccessReportsForm_Load(object sender, EventArgs e)
        {
            setCopiesVisible();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown1.Focused == true)
            {
                clsAccessReports.NbrCopies = Convert.ToInt32(numericUpDown1.Value);
            }
        }
    }
}
