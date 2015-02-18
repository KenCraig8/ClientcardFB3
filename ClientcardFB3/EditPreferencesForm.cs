using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ClientcardFB3
{
    public partial class EditPreferencesForm : Form
    {
        bool dataChanged = false;
        bool bNormalMode = false;
        bool oriBoolValue = false;
        string oriTextValue = "";
        decimal oriNUDValue = 0;
        Zipcodes clsZipcodes;
        List<TextBox> tbCntlList = new List<TextBox>();     //Collection of Editable Textboxes
        List<TextBox> tbdList = new List<TextBox>();        //Collection of DonorID Textboxes
        List<CheckBox> chkList = new List<CheckBox>();      //Collection of all Checkboxes
        List<NumericUpDown> nudList = new List<NumericUpDown>();      //Collection of all NumericUpDown controls

        public EditPreferencesForm()
        {
            InitializeComponent();
            bNormalMode = false;
            traverseAndAddControlsToCollections(this.Controls);
            SqlConnection sqlConn = new SqlConnection(CCFBGlobal.connectionString);
            SqlCommand sqlCmd = new SqlCommand("", sqlConn);
            sqlCmd.CommandType = CommandType.Text;
            SqlDataReader sqlReader;
            string fldname = "";
            sqlConn.Open();
            foreach (TextBox tb in tbCntlList)
            {
                fldname = tb.Tag.ToString();
                sqlCmd.CommandText = "SELECT FldVal,EditTip FROM Preferences WHERE FldName = '" + fldname + "'";
                sqlReader = sqlCmd.ExecuteReader();
                while (sqlReader.Read())
                {
                    tb.Text = sqlReader.GetString(0);
                }
                sqlReader.Close();
            }
            foreach (CheckBox chk in chkList)
            {
                fldname = chk.Tag.ToString();
                sqlCmd.CommandText = "SELECT BoolVal,EditTip FROM Preferences WHERE FldName = '" + fldname + "'";
                sqlReader = sqlCmd.ExecuteReader();
                while (sqlReader.Read())
                {
                    chk.Checked = sqlReader.GetBoolean(0);
                }
                sqlReader.Close();
            }
            foreach (NumericUpDown nud in nudList)
            {
                fldname = nud.Tag.ToString();
                sqlCmd.CommandText = "SELECT FldVal,EditTip FROM Preferences WHERE FldName = '" + fldname + "'";
                sqlReader = sqlCmd.ExecuteReader();
                while (sqlReader.Read())
                {
                    nud.Value = Convert.ToDecimal( sqlReader.GetString(0));
                }
                sqlReader.Close();
            }
            //sqlReader.Dispose();
            //sqlCmd.Dispose();
            //sqlConn.Dispose();
            cboSvcMnuTyp.SelectedIndex  = CCFBPrefs.ServiceMenuType;
            //Alerts
            //nudAlertMonthSvc.Value = CCFBPrefs.AlertMonthSvc;
            //nudAlertWeekSvc.Value = CCFBPrefs.AlertWeekSvc;
            //nudAlertMinimumDays.Value = CCFBPrefs.AlertMinimumDays;
            //nudAlertMinimumMonths.Value = CCFBPrefs.AlertMinimumMonths;
            //
            CCFBGlobal.InitCombo(cboFMIDType, CCFBGlobal.parmTbl_IdVerify);
            cboFMIDType.SelectedValue = CCFBPrefs.DefaultFMIDType.ToString();

            //Add Client Options
            if (CCFBPrefs.UseFamilyList == CCFBPrefs.UseFamilyListCode.Normally)
                rdobNormally.Checked = true;
            else if (CCFBPrefs.UseFamilyList == CCFBPrefs.UseFamilyListCode.Sometimes)
                rdobSometimes.Checked = true;
            else if (CCFBPrefs.UseFamilyList == CCFBPrefs.UseFamilyListCode.Always)
                rdobAlways.Checked = true;
            else if (CCFBPrefs.UseFamilyList == CCFBPrefs.UseFamilyListCode.Never)
                rdobNever.Checked = true;
            switch (CCFBPrefs.OverRideLevel)
            {
                case CCFBGlobal.permissions_Intake:
                    rdoOverRideIntake.Checked = true;
                    break;
                case CCFBGlobal.permissions_IntakeAdmin:
                    rdoOverRideInatkeAdmin.Checked = true;
                    break;
                default:
                    rdoOverRideAdmin.Checked = true;
                    break;
            }

            //Monthly Reports
            System.Collections.ArrayList ptList = new System.Collections.ArrayList();
            ptList.Add(new parmType(1, "January", 0, "JAN"));
            ptList.Add(new parmType(7, "July", 1, "JUL"));
            ptList.Add(new parmType(10, "October", 2, "OCT"));
            cboFiscalStartMonth.DataSource = ptList;
            cboFiscalStartMonth.DisplayMember = "LongName";
            cboFiscalStartMonth.ValueMember = "UID";
            cboFiscalStartMonth.SelectedValue = CCFBPrefs.FiscalYearStartMonth.ToString();


            tbDonorId01.Text = CCFBPrefs.DonorId01.ToString();
            tbDonorId02.Text = CCFBPrefs.DonorId02.ToString();
            tbDonorId03.Text = CCFBPrefs.DonorId03.ToString();
            tbDonorId04.Text = CCFBPrefs.DonorId04.ToString();
            tbDonorId05.Text = CCFBPrefs.DonorId05.ToString();
            tbDonorId06.Text = CCFBPrefs.DonorId06.ToString();
            tbDonorId07.Text = CCFBPrefs.DonorId07.ToString();
            tbDonorId08.Text = CCFBPrefs.DonorId08.ToString();
            tbDonorId09.Text = CCFBPrefs.DonorId09.ToString();
            tbDonorId10.Text = CCFBPrefs.DonorId10.ToString();
            tbDonorId11.Text = CCFBPrefs.DonorId11.ToString();
            tbDonorId12.Text = CCFBPrefs.DonorId12.ToString();
            tbDonorId13.Text = CCFBPrefs.DonorId13.ToString();
            tbDonorId14.Text = CCFBPrefs.DonorId14.ToString();
            tbDonorId15.Text = CCFBPrefs.DonorId15.ToString();
            tbDonorId16.Text = CCFBPrefs.DonorId16.ToString();
            tbDonorId17.Text = CCFBPrefs.DonorId17.ToString();
            tbDonorId18.Text = CCFBPrefs.DonorId18.ToString();
            tbDonorId19.Text = CCFBPrefs.DonorId19.ToString();
            tbDonorId20.Text = CCFBPrefs.DonorId20.ToString();
            if (CCFBPrefs.DonorPercentCalcMethod == CCFBPrefs.DonorCalcPercentMethod.LbsServed)
                rdoUseLbsServed.Checked = true;
            else if (CCFBPrefs.DonorPercentCalcMethod == CCFBPrefs.DonorCalcPercentMethod.LbsDonated)
                rdoUseLbsDonated.Checked = true;
            tbReportsSavePath.Text = CCFBPrefs.ReportsSavePath;
            LoadDonorLabelNames();
            dataChanged = false;
            bNormalMode = true;
            rdoViewByFullWeek.Checked = !CCFBPrefs.UseCalendarWeeks;
            rdoViewByCalendarWeek.Checked = CCFBPrefs.UseCalendarWeeks;
            clsZipcodes = new Zipcodes(CCFBGlobal.connectionString);

        }

        private void tbCntls_Leave(object sender, EventArgs e)
        {
            if (bNormalMode== true)
            {
                TextBox tb = (TextBox)sender;
                if (oriTextValue != tb.Text)
                {
                    CCFBPrefs.SaveValue(tb.Tag.ToString(), tb.Text);
                    dataChanged = true;
                }
            }
            clearToolTip();
        }

        private void tbCntls_Enter(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            oriTextValue = tb.Text;
            lblToolTip.Text = helpProvider1.GetHelpString(tb);
        }

        private void chkBoxes_CheckedChanged(object sender, EventArgs e)
        {
            if (bNormalMode == true)
            {
                CheckBox chk = (CheckBox)sender;
                if (oriBoolValue != chk.Checked)
                {
                    CCFBPrefs.SaveValue(chk.Tag.ToString(), chk.Checked);
                    oriBoolValue = chk.Checked;
                    dataChanged = true;
                }
            }
        }

        private void chkBoxes_Enter(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            oriBoolValue = chk.Checked;
            lblToolTip.Text = helpProvider1.GetHelpString(chk);
        }

        private void EditPreferencesForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (dataChanged == true)
                CCFBPrefs.Init();
        }

        private void cboFiscalStartMonth_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CCFBPrefs.SaveValue("FiscalYearStartMonth", ((parmType)cboFiscalStartMonth.SelectedItem).UID);
        }

        private void btnDonor_Click(object sender, EventArgs e)
        {
            EditDonorForm frmTmp = new EditDonorForm(CCFBGlobal.connectionString, true);
            DialogResult dr = frmTmp.ShowDialog(this);
            int newDonorId = frmTmp.SelectedId;
            string newDonorName = frmTmp.SelectedName;
            frmTmp.Dispose();
            if (dr == DialogResult.Yes)
            {
                Button btn = (Button)sender;
                string tagVal = btn.Tag.ToString();
                foreach (TextBox tb in tpDonorPercent.Controls.OfType<TextBox>())
                {
                    if (tb.Tag.ToString() == tagVal)
                    {
                        tb.Text = newDonorId.ToString();
                        CCFBPrefs.SaveValue(tagVal, newDonorId.ToString());
                        dataChanged = true;
                        break;
                    }
                }

                filllblDonor(tagVal, newDonorName);
            }
        }
        private void LoadDonorLabelNames()
        {
            Donors clsDonors = new Donors(CCFBGlobal.connectionString);
            int donorid = 0;
            foreach (Label lbl in tpDonorPercent.Controls.OfType<Label>())
	        {
                switch (lbl.Tag.ToString())
                {
                    case "DonorId01": donorid = CCFBPrefs.DonorId01; break;
                    case "DonorId02": donorid = CCFBPrefs.DonorId02; break;
                    case "DonorId03": donorid = CCFBPrefs.DonorId03; break;
                    case "DonorId04": donorid = CCFBPrefs.DonorId04; break;
                    case "DonorId05": donorid = CCFBPrefs.DonorId05; break;
                    case "DonorId06": donorid = CCFBPrefs.DonorId06; break;
                    case "DonorId07": donorid = CCFBPrefs.DonorId07; break;
                    case "DonorId08": donorid = CCFBPrefs.DonorId08; break;
                    case "DonorId09": donorid = CCFBPrefs.DonorId09; break;
                    case "DonorId10": donorid = CCFBPrefs.DonorId10; break;
                    case "DonorId11": donorid = CCFBPrefs.DonorId11; break;
                    case "DonorId12": donorid = CCFBPrefs.DonorId12; break;
                    case "DonorId13": donorid = CCFBPrefs.DonorId13; break;
                    case "DonorId14": donorid = CCFBPrefs.DonorId14; break;
                    case "DonorId15": donorid = CCFBPrefs.DonorId15; break;
                    case "DonorId16": donorid = CCFBPrefs.DonorId16; break;
                    case "DonorId17": donorid = CCFBPrefs.DonorId17; break;
                    case "DonorId18": donorid = CCFBPrefs.DonorId18; break;
                    case "DonorId19": donorid = CCFBPrefs.DonorId19; break;
                    case "DonorId20": donorid = CCFBPrefs.DonorId20; break;
                    default: break;
                }
                lbl.Text = ".....";
                if (donorid > 0)
                {
                    clsDonors.openWhere(" Where Id = " + donorid.ToString());
                    if (clsDonors.RowCount > 0)
                    {
                        if (clsDonors.ID == donorid)
                        {
                            lbl.Text = clsDonors.Name;
                        }
                    }
                }
	        }
            clsDonors.Dispose();
        }

        private void tbDonor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                TextBox tb = (TextBox)sender;
                int donorid = 0;
                if (tb.Text.Length >0)
                {
                    try
                    {
                        donorid = Convert.ToInt32(tb.Text);
                        Donors clsDonors = new Donors(CCFBGlobal.connectionString);
                        clsDonors.openWhere(" Where Id = " + donorid.ToString());
                        if (clsDonors.RowCount > 0)
                        {
                            if (clsDonors.ID == donorid)
                            {
                                filllblDonor(tb.Tag.ToString(), clsDonors.Name);
                                CCFBPrefs.SaveValue(tb.Tag.ToString(), donorid.ToString());
                                dataChanged = true;
                            }
                        }
                        else
                        {
                            donorid = 0;
                        }
                        clsDonors.Dispose();
                    }
                    catch (FormatException ex)
                    {
                        MessageBox.Show("Invalid number entered.\r\nThis will be ignored");
                    }
                    

                }
                if (donorid == 0 )
                    filllblDonor(tb.Tag.ToString(), "");
            }
        }

        private void filllblDonor(string tagVal, string donorname)
        {
            foreach (Label lbl in tpDonorPercent.Controls.OfType<Label>())
            {
                if (lbl.Tag.ToString() == tagVal)
                {
                    if (String.IsNullOrEmpty(donorname) == true)
                        lbl.Text = ".....";
                    else
                        lbl.Text = donorname;
                    Application.DoEvents();
                    break;
                }
            }
        }

        private void rdoViewBy_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdo = (RadioButton)sender;
            if (rdo.Checked == true)
            {
                CCFBPrefs.SaveValue(grpViewGRBy.Tag.ToString(), Convert.ToBoolean(rdo.Tag));
                dataChanged = true;
            }
        }

        private void tbCommSigValidFor_KeyDown(object sender, KeyEventArgs e)
        {
            CCFBGlobal.checkForIntOnKeyPress(e);
        }

        private void rdobUseFamList_CheckedChanged(object sender, EventArgs e)
        {
            if (rdobAlways.Checked == true)
            {
                CCFBPrefs.UseFamilyList = CCFBPrefs.UseFamilyListCode.Always;
                CCFBPrefs.SaveValue("UseFamilyList", "2");
            }
            else if (rdobNormally.Checked == true)
            {
                CCFBPrefs.UseFamilyList = CCFBPrefs.UseFamilyListCode.Normally;
                CCFBPrefs.SaveValue("UseFamilyList", "0");
            }
            else if (rdobSometimes.Checked == true)
            {
                CCFBPrefs.UseFamilyList = CCFBPrefs.UseFamilyListCode.Sometimes;
                CCFBPrefs.SaveValue("UseFamilyList", "1");
            }
            else if (rdobNever.Checked == true)
            {
                CCFBPrefs.UseFamilyList = CCFBPrefs.UseFamilyListCode.Never;
                CCFBPrefs.SaveValue("UseFamilyList", "3");
            }

            dataChanged = true;
        }

        private void nudList_Leave(object sender, EventArgs e)
        {
            NumericUpDown nud = (NumericUpDown)sender;
            if (nud.Value != oriNUDValue)
            {
                CCFBPrefs.SaveValue(nud.Tag.ToString(), nud.Value.ToString());
            }
            clearToolTip();
        }

        private void rdoOverRide_CheckedChanged(object sender, EventArgs e)
        {
            if (bNormalMode == true)
            {
                RadioButton rdo = (RadioButton)sender;
                if (rdo.Checked == true)
                {
                    CCFBPrefs.OverRideLevel = Convert.ToInt32(rdo.Tag);
                    CCFBPrefs.SaveValue("OverRideLevel", CCFBPrefs.OverRideLevel);
                }
            }
        }

        private string getFolderPath(TextBox tb)
        {
            string oriPath = tb.Text.Trim();
            try
            {

                //Set default extension of the savefile dialog
                folderBrowserDialog1.Description = "Save Path for Monthly Reports";
                //If no value exists in registry for default save location
                if (String.IsNullOrEmpty(oriPath) == true)
                {
                    folderBrowserDialog1.SelectedPath = Path.GetDirectoryName(Application.ExecutablePath);
                }
                else
                {
                    folderBrowserDialog1.SelectedPath = Path.GetDirectoryName(oriPath);
                }
                //saveFileDialog1.Filter = "SQL Backup Files (*.bak)|*.bak";
                DialogResult dr = folderBrowserDialog1.ShowDialog();

                //If user confirmed save
                if (dr == System.Windows.Forms.DialogResult.OK)
                {
                    if (oriPath != folderBrowserDialog1.SelectedPath)
                    {
                        oriPath = folderBrowserDialog1.SelectedPath;
                        CCFBPrefs.SaveValue(tb.Tag.ToString(), oriPath);
                        dataChanged = true;
                    }
                }
            }
            catch (Exception ex)
            {
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString());
            }
            return oriPath;
        }

        private void btnRptSaveFldr_Click(object sender, EventArgs e)
        {
            tbReportsSavePath.Text = getFolderPath(tbReportsSavePath);
        }

        private void tbDefaultZipcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (clsZipcodes.getCity(tbcDefaultZipcode.Text) == true)
                {
                    tbcDefaultCity.Text = clsZipcodes.City.ToUpper();
                    CCFBPrefs.SaveValue(tbcDefaultCity.Tag.ToString(), tbcDefaultCity.Text);
                    tbcDefaultState.Text = clsZipcodes.State.ToUpper();
                    CCFBPrefs.SaveValue(tbcDefaultState.Tag.ToString(), tbcDefaultState.Text);
                    dataChanged = true;
                }
                e.Handled = true;
            }
        }

        private void clearToolTip()
        {
            lblToolTip.Text = "";
        }

        private void chkBoxes_Leave(object sender, EventArgs e)
        {
            clearToolTip();
        }

        private void tbDonor_Enter(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            lblToolTip.Text = "Press the BUTTON 'Donor " + tb.Name.Substring(tb.Name.Length-2,2) + "' OR enter the desired donor id and press ENTER";
        }

        private void cboFMIDType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cboFMIDType.Focused == true)
            {
                CCFBPrefs.DefaultFMIDType = Convert.ToInt32(cboFMIDType.SelectedValue);
                CCFBPrefs.SaveValue("DefaultFMIDType", CCFBPrefs.DefaultFMIDType);
            }
        }

        private void cboSvcMnuTyp_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cboSvcMnuTyp.Focused == true)
            {
                CCFBPrefs.ServiceMenuType = cboSvcMnuTyp.SelectedIndex;
                CCFBPrefs.SaveValue("ServiceMenuType", CCFBPrefs.ServiceMenuType);
            }
        }

        private void chkUseLocalOutOfAreaAlerts_CheckedChanged(object sender, EventArgs e)
        {
            if (bNormalMode == true)
            {
                if (oriBoolValue != chkUseLocalOutOfAreaAlerts.Checked)
                {
                    CCFBPrefs.SaveValue(chkUseLocalOutOfAreaAlerts.Tag.ToString(), chkUseLocalOutOfAreaAlerts.Checked);
                    oriBoolValue = chkUseLocalOutOfAreaAlerts.Checked;
                    dataChanged = true;
                }
                setAllertGrpDisplay(chkUseLocalOutOfAreaAlerts.Checked);
            }
        }

        private void setAllertGrpDisplay(bool ischecked)
        {
            if (ischecked == true)
            {
                grpAll.Text = "Local Client Alerts";
            }
            else
            {
                grpAll.Text = "Alerts for All Clients";
            }
            grpOutofArea.Visible = ischecked;

        }
        /// <summary>
        /// Traverses all controls on the page using recursion and adds the proper ones
        /// to their proper collections and adds LostFocus event to Textboxes and Checkboxes
        /// </summary>
        /// <param name="controlList"></param>
        private void traverseAndAddControlsToCollections(Control.ControlCollection controlList)
        {
            foreach (Control cntrl in controlList.OfType<Control>())
            {
                switch (cntrl.GetType().Name)
                {
                    case "TextBox":
                        {
                            if (cntrl.Tag != null
                                && cntrl.Tag.ToString().Length > 0)
                            {
                                if (cntrl.Name.Substring(0, 3) == "tbc")
                                {
                                    tbCntlList.Add((TextBox)cntrl);
                                    cntrl.Enter += new System.EventHandler(this.tbCntls_Enter);
                                    cntrl.Leave += new System.EventHandler(this.tbCntls_Leave);
                                }
                                else if (cntrl.Name.Substring(0, 3) == "tbD")
                                {
                                    tbdList.Add((TextBox)cntrl);
                                }

                                else
                                {
                                    //if (cntrl.Name != "tbID")
                                    //{
                                    //    cntrl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbList_KeyDown);
                                    //    cntrl.Leave += new System.EventHandler(this.tbList_LostFocus);
                                    //    tbList.Add((TextBox)cntrl);
                                    //}
                                }
                            }
                            break;
                        }
                    case "CheckBox":
                        {
                            CheckBox chk = (CheckBox)cntrl;
                            chk.CheckedChanged += new System.EventHandler(this.chkBoxes_CheckedChanged);
                            chk.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkBoxes_KeyDown);
                            chk.Enter += new System.EventHandler(this.chkBoxes_Enter);
                            chk.Leave += new System.EventHandler(this.chkBoxes_Leave);
                            chkList.Add(chk);
                            break;
                        }
                    case "ComboBox":
                        {
                            //if (cntrl.Tag != null)
                            //{
                            //    if (cntrl.Tag.ToString().Trim().Length > 0)
                            //    {
                            //        cboList.Add((ComboBox)cntrl);
                            //    }
                            //}
                            break;
                        }
                    case "NumericUpDown":
                        {
                            NumericUpDown nud = (NumericUpDown)cntrl;
                            nud.Enter += new System.EventHandler(this.nudList_Enter);
                            nud.Leave += new System.EventHandler(this.nudList_Leave);
                            nudList.Add(nud);
                            break;
                        }
                }

                traverseAndAddControlsToCollections(cntrl.Controls);
            }
        }

        private void grpOutofArea_Enter(object sender, EventArgs e)
        {

        }

        private void chkBoxes_KeyDown(object sender, KeyEventArgs e)
        {
            if (bNormalMode == true && e.KeyCode == Keys.Enter)
            {
                CheckBox chkHH = (CheckBox)sender;
                chkHH.Checked = !chkHH.Checked;
            }
        }

        private void nudList_Enter(object sender, EventArgs e)
        {
            NumericUpDown nud = (NumericUpDown)sender;
            oriNUDValue = nud.Value;
        }
    }
}
