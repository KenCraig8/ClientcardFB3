using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        Zipcodes clsZipcodes;

        public EditPreferencesForm()
        {
            InitializeComponent();
            bNormalMode = false;
            //My Food Bank
            tbFoodBankName.Text = CCFBPrefs.FoodBankName;
            tbPostalAddress.Text = CCFBPrefs.PostalAddress;
            tbPhysicalAddress.Text = CCFBPrefs.PhysicalAddress;
            tbPhoneNumber.Text = CCFBPrefs.PhoneNumber;
            tbEmailAddress.Text = CCFBPrefs.EmailAddress;
            tbFaxNum.Text = CCFBPrefs.FaxNumber;
            tbCounty.Text = CCFBPrefs.County;
            chkCaptureSignature.Checked = CCFBPrefs.CaptureSignature;
            chkEnableCDBGReporting.Checked = CCFBPrefs.EnableCDBGReporting;
            tbAgencyNbr.Text = CCFBPrefs.AgencyNumber;
            tbEFAPLeadAgency.Text = CCFBPrefs.EFAPLeadAgency;
            //
            chkEnableHomeDeliv.Checked = CCFBPrefs.EnableHomeDeliv;
            chkEnableClientReceipt.Checked = CCFBPrefs.EnableClientReceipt;
            cboSvcMnuTyp.SelectedIndex  = CCFBPrefs.ServiceMenuType;
            chkEnablePointsTracking.Checked = CCFBPrefs.EnablePointsTracking;
            tbPointsPerWeek.Text = CCFBPrefs.PointsPerWeek.ToString();
            tbPointsPerFamMbr.Text = CCFBPrefs.PointsPerFamMbr.ToString();
            tbPointsPerWeekOutOfArea.Text = CCFBPrefs.PointsPerWeekOutOfArea.ToString();
            tbMaxPointsPerWeek.Text = CCFBPrefs.MaxPointsPerWeek.ToString();
            //Features
            chkEnableFoodService.Checked = CCFBPrefs.EnableFoodServices;
            chkEnableAppointments.Checked = CCFBPrefs.EnableAppointments;
            chkEnableFoodDonations.Checked = CCFBPrefs.EnableFoodDonations;
            chkEnableCashDonations.Checked = CCFBPrefs.EnableCashDonations;
            chkEnableVolunteerHours.Checked = CCFBPrefs.EnableVolunteerHours;
            chkEnablePrintClientCard.Checked = CCFBPrefs.EnablePrintClientCard;
            chkEnableVouchers.Checked = CCFBPrefs.EnableVouchers;
            chkEnableCSFP.Checked = CCFBPrefs.EnableCSFP;
            chkEnableCSFPOnNewSvc.Checked = CCFBPrefs.EnableCSFPOnNewSvc;
            chkCSFPShowRoutes.Checked = CCFBPrefs.EnableCSFPShowRoutes;
            chkEnableTEFAP.Checked = CCFBPrefs.EnableTEFAP;
            chkEnableBackPack.Checked = CCFBPrefs.EnableBackPack;
            chkMustBeACommodityDay.Checked = CCFBPrefs.MustBeACommodityDay;
            tbCommSigValidFor.Text = CCFBPrefs.CommSigValidFor.ToString();
            chkEnableSupplemental.Checked = CCFBPrefs.EnableSupplemental;
            chkEnableServiceGroups.Checked = CCFBPrefs.EnableServiceGroups;
            chkEnableBabyServices.Checked = CCFBPrefs.EnableBabyServices;
            chkEnableBarcodePrompts.Checked = CCFBPrefs.EnableBarcodePrompts;
            chkSearchFamilyMember.Checked = CCFBPrefs.BarcodeUseFamilyMember;
            chkAutoGiveService.Checked = CCFBPrefs.AutomaticallyGiveService;
            chkEnableClientPhone.Checked = CCFBPrefs.EnableClientPhone;
            chkEnableVerifyId.Checked = CCFBPrefs.EnableVerifyId;
            chkEnableHouseholdIncome.Checked = CCFBPrefs.EnableHouseholdIncome;
            chkEnableHUDIncomeCategory.Checked = CCFBPrefs.EnableHUDCategory;
            chkEnableHHUserDefinedFieldsTab.Checked = CCFBPrefs.EnableHHUserDefinedFields;
            chkEnableWorksInArea.Checked = CCFBPrefs.EnableWorksInArea;
            chkEnableAdditionalHHMDataTab.Checked = CCFBPrefs.EnableAdditionalHHMDataTab;
            chkEnableEthnicityHHMTab.Checked = CCFBPrefs.EnableEthnicityHHMTab;
            chkEnableIDFlds.Checked = CCFBPrefs.EnableIDFlds;
            chkEnableFastTrack.Checked = CCFBPrefs.EnableFastTrack;
            nudAlertMonthSvc.Value = CCFBPrefs.AlertMonthSvc;
            tbAlertMonthSvcText.Text = CCFBPrefs.AlertMonthSvcText;
            nudAlertWeekSvc.Value = CCFBPrefs.AlertWeekSvc;
            tbAlertWeekSvcText.Text = CCFBPrefs.AlertWeekSvcText;
            nudAlertMinimumDays.Value = CCFBPrefs.AlertMinimumDays;
            tbAlertMinDaysText.Text = CCFBPrefs.AlertMinDaysText;
            nudAlertMinimumMonths.Value = CCFBPrefs.AlertMinimumMonths;
            tbAlertMinMonthsText.Text = CCFBPrefs.AlertMinMonthsText;
            chkWarnSvcEachPerson.Checked = CCFBPrefs.WarnSvcEachPerson;
            CCFBGlobal.InitCombo(cboFMIDType, CCFBGlobal.parmTbl_IdVerify);
            cboFMIDType.SelectedValue = CCFBPrefs.DefaultFMIDType.ToString();
            chkCaptureTEFAPSign.Checked = CCFBPrefs.CaptureTEFAPSignature;
            chkIncludeLbsOnFoodSvcList.Checked = CCFBPrefs.IncludeLbsOnSvcList;

            nudAlertMonthSvc_ValueChanged(null, null);
            nudAlertWeekSvc_ValueChanged(null, null);
            //Add Client Options
            tbDefaultCity.Text = CCFBPrefs.DefaultCity;
            tbDefaultState.Text = CCFBPrefs.DefaultState;
            tbDefaultZipcode.Text = CCFBPrefs.DefaultZipcode;
            chkAllowDuplicateHHNames.Checked = CCFBPrefs.AllowDuplicateHHNames;
            chkAllowDuplicateMemberNames.Checked = CCFBPrefs.AllowDuplicateMemberNames;
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
            //Form Options
            chkFindClientAutoRefresh.Checked = CCFBPrefs.FindClientAutoRefresh;
            tbServiceLogRefreshRate.Text = CCFBPrefs.ServiceLogRefreshRate.ToString();
            tbLbsPerCSFPService.Text = CCFBPrefs.CSFPLbsPerService.ToString();
            tbApptLogRefreshRate.Text = CCFBPrefs.ApptLogRefreshRate.ToString();
            tbNbrMealsPerService.Text = CCFBPrefs.NbrMealsPerService.ToString();
            tbNbrDaysAllowMods.Text = CCFBPrefs.NbrDaysAllowMods.ToString();
            tbNbrSvcDatesFuture.Text = CCFBPrefs.NbrSvcDatesFuture.ToString();
            tbNbrSvcDatesPast.Text = CCFBPrefs.NbrSvcDatesPast.ToString();
            chkFilterPeriodFromAddress.Checked = CCFBPrefs.FilterPeriodFromAddress;

            //Monthly Reports
            System.Collections.ArrayList ptList = new System.Collections.ArrayList();
            ptList.Add(new parmType(1, "January", 0, "JAN"));
            ptList.Add(new parmType(7, "July", 1, "JUL"));
            ptList.Add(new parmType(10, "October", 2, "OCT"));
            cboFiscalStartMonth.DataSource = ptList;
            cboFiscalStartMonth.DisplayMember = "LongName";
            cboFiscalStartMonth.ValueMember = "UID";
            cboFiscalStartMonth.SelectedValue = CCFBPrefs.FiscalYearStartMonth.ToString();

            chkMeregeTeens.Checked = CCFBPrefs.MergeTeens;

            tbInkindDollarsPerHour.Text = CCFBPrefs.InkindDollarsPerHr.ToString("F");
            tbInkindDollarsPerLb.Text = CCFBPrefs.InkindDollarsPerLb.ToString("F");
            tbNWHDonorId.Text = CCFBPrefs.DonorIDNWH.ToString();
            tbDonorIdEFAP.Text = CCFBPrefs.DonorIDEFAP.ToString();
            tbDonorIdTEFAP.Text = CCFBPrefs.DonorIDTEFAP.ToString();
            tbDonorId2ndHarvest.Text = CCFBPrefs.DonorID2ndHarvest.ToString();

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
            chkIncludeCommodityLbsInCoalition.Checked = CCFBPrefs.IncludeCommodityLbsInCoalition;
            chkIncludeCommodityLbsInFoodLifeline.Checked = CCFBPrefs.IncludeCommodityLbsInFoodLifeline;
            chkIncludeCommodityLbsInNorthwestHarvest.Checked = CCFBPrefs.IncludeCommodityLbsInNorhtwestHarvest;
            chkIncludeCommodityLbsInSecondHarvestInlandNW.Checked = CCFBPrefs.IncludeCommodityLbsInSecondHarvestInland;
            if (CCFBPrefs.DonorPercentCalcMethod == CCFBPrefs.DonorCalcPercentMethod.LbsServed)
                rdoUseLbsServed.Checked = true;
            else if (CCFBPrefs.DonorPercentCalcMethod == CCFBPrefs.DonorCalcPercentMethod.LbsDonated)
                rdoUseLbsDonated.Checked = true;
            tbPreparedBy.Text = CCFBPrefs.PreparedBy;
            tbReportsSavePath.Text = CCFBPrefs.ReportsSavePath;
            LoadDonorLabelNames();
            dataChanged = false;
            bNormalMode = true;
            chkUseTimeInOutForVols.Checked = CCFBPrefs.UseTimeInOutForVols;
            rdoViewByFullWeek.Checked = !CCFBPrefs.UseCalendarWeeks;
            rdoViewByCalendarWeek.Checked = CCFBPrefs.UseCalendarWeeks;
            clsZipcodes = new Zipcodes(CCFBGlobal.connectionString);

        }

        private void textboxes_Leave(object sender, EventArgs e)
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

        private void textboxes_Enter(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            oriTextValue = tb.Text;
            lblToolTip.Text = helpProvider1.GetHelpString(tb);
        }

        private void checkBoxes_CheckedChanged(object sender, EventArgs e)
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

        private void checkBoxes_Enter(object sender, EventArgs e)
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
            if (dr == DialogResult.Yes)
            {
                int newDonorId = frmTmp.SelectedId;
                string newDonorName = frmTmp.SelectedName;
                frmTmp.Close();
                frmTmp.Dispose();
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
        }

        private void tbDonor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                TextBox tb = (TextBox)sender;
                int donorid = 0;
                if (tb.Text != "")
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
                    if (donorname == "")
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

        private void nudAlertMonthSvc_Leave(object sender, EventArgs e)
        {
            if (nudAlertMonthSvc.Value != CCFBPrefs.AlertMonthSvc)
            {
                CCFBPrefs.AlertMonthSvc = Convert.ToInt32(nudAlertMonthSvc.Value);
                CCFBPrefs.SaveValue("AlertMonthSvc", nudAlertMonthSvc.Value.ToString());
            }
            clearToolTip();
        }

        private void nudAlertWeekSvc_Leave(object sender, EventArgs e)
        {
            if (nudAlertWeekSvc.Value != CCFBPrefs.AlertWeekSvc)
            {
                CCFBPrefs.AlertWeekSvc = Convert.ToInt32(nudAlertWeekSvc.Value);
                CCFBPrefs.SaveValue("AlertWeekSvc", nudAlertWeekSvc.Value.ToString());
            }
            clearToolTip();
        }

        private void nudAlertMonthSvc_ValueChanged(object sender, EventArgs e)
        {
            tbAlertMonthSvcText.Visible = (nudAlertMonthSvc.Value > 0);
        }

        private void nudAlertWeekSvc_ValueChanged(object sender, EventArgs e)
        {
            tbAlertWeekSvcText.Visible = (nudAlertWeekSvc.Value > 0);
        }

        private void tbAlertMonthSvcText_Leave(object sender, EventArgs e)
        {
            if (CCFBPrefs.AlertMonthSvcText != tbAlertMonthSvcText.Text)
            {
                CCFBPrefs.AlertMonthSvcText = tbAlertMonthSvcText.Text;
                CCFBPrefs.SaveValue("AlertMonthSvcText", CCFBPrefs.AlertMonthSvcText);
            }
            clearToolTip();
        }

        private void tbAlertWeekSvcText_Leave(object sender, EventArgs e)
        {
            if (CCFBPrefs.AlertWeekSvcText != tbAlertWeekSvcText.Text)
            {
                CCFBPrefs.AlertWeekSvcText = tbAlertWeekSvcText.Text;
                CCFBPrefs.SaveValue("AlertWeekSvcText", CCFBPrefs.AlertWeekSvcText);
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
                if (oriPath == "")
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
                if (clsZipcodes.getCity(tbDefaultZipcode.Text) == true)
                {
                    tbDefaultCity.Text = clsZipcodes.City.ToUpper();
                    CCFBPrefs.SaveValue(tbDefaultCity.Tag.ToString(), tbDefaultCity.Text);
                    tbDefaultState.Text = clsZipcodes.State.ToUpper();
                    CCFBPrefs.SaveValue(tbDefaultState.Tag.ToString(), tbDefaultState.Text);
                    dataChanged = true;
                }
                e.Handled = true;
            }
        }

        private void clearToolTip()
        {
            lblToolTip.Text = "";
        }

        private void chkBox_Leave(object sender, EventArgs e)
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

        private void nudAlertMinimumDays_Leave(object sender, EventArgs e)
        {
            if (nudAlertMinimumDays.Value != CCFBPrefs.AlertMinimumDays)
            {
                CCFBPrefs.AlertMinimumDays = Convert.ToInt32(nudAlertMinimumDays.Value);
                CCFBPrefs.SaveValue("AlertMinimumDays", nudAlertMinimumDays.Value.ToString());
            }
            clearToolTip();

        }

        private void nudAlertMinimumDays_ValueChanged(object sender, EventArgs e)
        {
            tbAlertMinDaysText.Visible = (nudAlertMinimumDays.Value > 0);
        }

        private void tbAlertMinDaysText_Leave(object sender, EventArgs e)
        {
            if (CCFBPrefs.AlertMinDaysText != tbAlertMinDaysText.Text)
            {
                CCFBPrefs.AlertMinDaysText = tbAlertMinDaysText.Text;
                CCFBPrefs.SaveValue("AlertMinDaysText", CCFBPrefs.AlertMinDaysText);
            }
            clearToolTip();
        }
        private void nudAlertMinimumMonths_Leave(object sender, EventArgs e)
        {
            if (nudAlertMinimumMonths.Value != CCFBPrefs.AlertMinimumMonths)
            {
                CCFBPrefs.AlertMinimumMonths = Convert.ToInt32(nudAlertMinimumMonths.Value);
                CCFBPrefs.SaveValue("AlertMinimumMonths", nudAlertMinimumMonths.Value.ToString());
            }
            clearToolTip();

        }

        private void nudAlertMinimumMonths_ValueChanged(object sender, EventArgs e)
        {
            tbAlertMinMonthsText.Visible = (nudAlertMinimumMonths.Value > 0);
        }

        private void tbAlertMinMonthsText_Leave(object sender, EventArgs e)
        {
            if (CCFBPrefs.AlertMinMonthsText != tbAlertMinMonthsText.Text)
            {
                CCFBPrefs.AlertMinMonthsText = tbAlertMinMonthsText.Text;
                CCFBPrefs.SaveValue("AlertMinMonthsText", CCFBPrefs.AlertMinMonthsText);
            }
            clearToolTip();
        }

        private void cboSvcMnuTyp_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cboSvcMnuTyp.Focused == true)
            {
                CCFBPrefs.ServiceMenuType = cboSvcMnuTyp.SelectedIndex;
                CCFBPrefs.SaveValue("ServiceMenuType", CCFBPrefs.ServiceMenuType);
            }
        }
    }
}
