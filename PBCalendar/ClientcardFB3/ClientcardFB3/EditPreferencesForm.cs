using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ClientcardFB3
{
    public partial class EditPreferencesForm : Form
    {
        List<UserFieldItem> userFieldList = new List<UserFieldItem>();
        bool dataChanged = false;
        bool normalMode = false;
        bool oriBoolValue = false;
        string oriTextValue = "";

        public EditPreferencesForm()
        {
            InitializeComponent();
            normalMode = false;
        //My Food Bank
            tbFoodBankName.Text = CCFBPrefs.FoodBankName;
            tbPostalAddress.Text = CCFBPrefs.PostalAddress;
            tbPhysicalAddress.Text = CCFBPrefs.PhysicalAddress;
            tbPhoneNumber.Text = CCFBPrefs.PhoneNumber;
            tbEmailAddress.Text = CCFBPrefs.EmailAddress;
            tbFaxNum.Text = CCFBPrefs.FaxNumber;
            tbCounty.Text = CCFBPrefs.County;
        //Features
            chkEnableFoodService.Checked = CCFBPrefs.EnableFoodServices;
            chkEnableAppointments.Checked = CCFBPrefs.EnableAppointments;
            chkEnableFoodDonations.Checked = CCFBPrefs.EnableFoodDonations;
            chkEnableCashDonations.Checked = CCFBPrefs.EnableCashDonations;
            chkEnableVolunteerHours.Checked = CCFBPrefs.EnableVolunteerHours;
            chkEnablePrintClientCard.Checked = CCFBPrefs.EnablePrintClientCard;
            chkEnableVouchers.Checked = CCFBPrefs.EnableVouchers;
            chkEnableCSFP.Checked = CCFBPrefs.EnableCSFP;
            chkEnableTEFAP.Checked = CCFBPrefs.EnableTEFAP;
            chkMustBeACommodityDay.Checked = CCFBPrefs.MustBeACommodityDay;
            chkEnableSupplemental.Checked = CCFBPrefs.EnableSupplemental;
            chkEnableBabyServices.Checked = CCFBPrefs.EnableBabyServices;
            chkEnableBarcodePrompts.Checked = CCFBPrefs.EnableBarcodePrompts;
            chkEnableClientPhone.Checked = CCFBPrefs.EnableClientPhone;
            chkEnableVerifyId.Checked = CCFBPrefs.EnableVerifyId;
            chkEnableHouseholdIncome.Checked = CCFBPrefs.EnableHouseholdIncome;
            chkEnableHHUserDefinedFieldsTab.Checked = CCFBPrefs.EnableHHUserDefinedFields;
            chkEnableWorksInArea.Checked = CCFBPrefs.EnableWorksInArea;
            chkEnableAdditionalHHMDataTab.Checked = CCFBPrefs.EnableAdditionalHHMDataTab;
            chkEnableEthnicityHHMTab.Checked = CCFBPrefs.EnableEthnicityHHMTab;
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
        //Form Options
            chkFindClientAutoRefresh.Checked = CCFBPrefs.FindClientAutoRefresh;
            tbServiceLogRefreshRate.Text = CCFBPrefs.ServiceLogRefreshRate.ToString();
            tbApptLogRefreshRate.Text = CCFBPrefs.ApptLogRefreshRate.ToString();
            tbNbrMealsPerService.Text = CCFBPrefs.NbrMealsPerService.ToString();
            tbNbrDaysAllowMods.Text = CCFBPrefs.NbrDaysAllowMods.ToString();
            tbNbrSvcDatesFuture.Text = CCFBPrefs.NbrSvcDatesFuture.ToString();
            tbNbrSvcDatesPast.Text = CCFBPrefs.NbrSvcDatesPast.ToString();
        //Family Card
            loadUserFieldLabels();
            tbSlot1.Text = userFieldText(CCFBPrefs.FamilyCardSlot1);
            tbSlot2.Text = userFieldText(CCFBPrefs.FamilyCardSlot2);
            tbSlot3.Text = userFieldText(CCFBPrefs.FamilyCardSlot3);
            tbSlot4.Text = userFieldText(CCFBPrefs.FamilyCardSlot4);

        //Monthly Reports
            System.Collections.ArrayList ptList = new System.Collections.ArrayList();
            ptList.Add(new parmType(1, "January", 0, "JAN"));
            ptList.Add(new parmType(7, "July", 1, "JUL"));
            ptList.Add(new parmType(10, "October", 2, "OCT"));
            cboFiscalStartMonth.DataSource = ptList;
            cboFiscalStartMonth.DisplayMember = "LongName";
            cboFiscalStartMonth.ValueMember = "UID";
            cboFiscalStartMonth.SelectedValue = CCFBPrefs.FiscalYearStartMonth.ToString();

            tbInkindDollarsPerHour.Text = CCFBPrefs.InkindDollarsPerHr.ToString("F");
            tbInkindDollarsPerLb.Text = CCFBPrefs.InkindDollarsPerLb.ToString("F");
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
            chkIncludeCommodityLbsInCoalition.Checked = CCFBPrefs.IncludeCommodityLbsInCoalition;
            chkIncludeCommodityLbsInFoodLifeline.Checked = CCFBPrefs.IncludeCommodityLbsInFoodLifeline;
            chkIncludeCommodityLbsInNorthwestHarvest.Checked = CCFBPrefs.includecommoditylbsinnorhtwestharvest;
            chkIncludeCommodityLbsInSecondHarvestInlandNW.Checked = CCFBPrefs.includecommoditylbsinsecondharvestinland;
            if (CCFBPrefs.DonorPercentCalcMethod == CCFBPrefs.DonorCalcPercentMethod.LbsServed)
                rdoUseLbsServed.Checked = true;
            else if (CCFBPrefs.DonorPercentCalcMethod == CCFBPrefs.DonorCalcPercentMethod.LbsDonated)
                rdoUseLbsDonated.Checked = true;
            tbPreparedBy.Text = CCFBPrefs.PreparedBy;
            tbReportsSavePath.Text = CCFBPrefs.ReportsSavePath;
            LoadDonorLabelNames();
            dataChanged = false;
            normalMode = true;
            chkUseTimeInOutForVols.Checked = CCFBPrefs.UseTimeInOutForVols;
        }

        private void loadUserFieldLabels()
        {
            UserFields clsUserFields = new UserFields(CCFBGlobal.connectionString);
            lstbxHHUserFields.Items.Clear();
            string fldName = "";
            clsUserFields.open("Household");
            userFieldList.Clear();
            for (int i = 0; i < clsUserFields.RowCount ; i++)
            {
                fldName = "UserFlag" + i.ToString();
                clsUserFields.setDataRow(fldName);
                if (fldName == clsUserFields.FldName)
                {
                    if (clsUserFields.EditLabel != "")
                    {
                        UserFieldItem fieldRow = new UserFieldItem(i, clsUserFields.EditLabel, fldName);
                        userFieldList.Add(fieldRow);
                        lstbxHHUserFields.Items.Add(fieldRow.EditLabel);
                    }   
                }
            }
        }

        private void listBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (lstbxHHUserFields.SelectedItems.Count > 0 ) 
                this.lstbxHHUserFields.DoDragDrop(this.lstbxHHUserFields.SelectedItem , DragDropEffects.Move);
        }

        private void tb_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void tb_DragDrop(object sender, DragEventArgs e)
        {
            object data = e.Data.GetData(typeof(string));
            if (data == null) { return; }
            TextBox tb = (TextBox)sender;
            tb.Text =data.ToString();
            this.lstbxHHUserFields.Items.Remove(data);
        }

        private void tb_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void listBox1_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void listBox1_DragDrop(object sender, DragEventArgs e)
        {
            object data = e.Data.GetData(typeof(String));
            if (data == null) { return; }
            this.lstbxHHUserFields.Items.Add(data.ToString());
            if (tbSlot1.Text == data.ToString())
                tbSlot1.Text = "";
            else if (tbSlot2.Text == data.ToString())
                tbSlot2.Text = "";
            else if (tbSlot3.Text == data.ToString())
                tbSlot3.Text = "";
            else if (tbSlot4.Text == data.ToString())
                tbSlot4.Text = "";
        }

        private void tb_MouseDown(object sender, MouseEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text != "" )
                tb.DoDragDrop(tb.Text, DragDropEffects.Move);
        }

        private string userFieldText(int UserFlagIndex)
        {
            foreach (UserFieldItem item in userFieldList)
            {
                if (UserFlagIndex == item.FldIndex)
                    return item.EditLabel; 
            }
            return "";
        }

        private void textboxes_Leave(object sender, EventArgs e)
        {
            if (normalMode== true)
            {
                TextBox tb = (TextBox)sender;
                if (oriTextValue != tb.Text)
                {
                    CCFBPrefs.SaveValue(tb.Tag.ToString(), tb.Text);
                    dataChanged = true;
                }
            }
        }

        private void textboxes_Enter(object sender, EventArgs e)
        {
            oriTextValue = ((TextBox)sender).Text;
        }

        private void checkBoxes_CheckedChanged(object sender, EventArgs e)
        {
            if (normalMode == true)
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
            oriBoolValue = ((CheckBox)sender).Checked;
        }

        private void EditPreferencesForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (dataChanged == true)
                CCFBPrefs.Init();
        }

        private void groupBox1_Leave(object sender, EventArgs e)
        {
            if (rdobAlways.Checked == true)
            {
                CCFBPrefs.UseFamilyList = CCFBPrefs.UseFamilyListCode.Always;
                CCFBPrefs.SaveValue("UseFamilyList", "0");
            }
            else if (rdobNormally.Checked == true)
            {
                CCFBPrefs.UseFamilyList = CCFBPrefs.UseFamilyListCode.Normally;
                CCFBPrefs.SaveValue("UseFamilyList", "1");
            }
            else if (rdobSometimes.Checked == true)
            {
                CCFBPrefs.UseFamilyList = CCFBPrefs.UseFamilyListCode.Sometimes;
                CCFBPrefs.SaveValue("UseFamilyList", "2");
            }
            else if (rdobNever.Checked == true)
            {
                CCFBPrefs.UseFamilyList = CCFBPrefs.UseFamilyListCode.Never;
                CCFBPrefs.SaveValue("UseFamilyList", "3");
            }

            dataChanged = true;
        }

        private void cboFiscalStartMonth_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CCFBPrefs.SaveValue("FiscalYearStartMonth", ((parmType)cboFiscalStartMonth.SelectedItem).UID);
        }

        private void btnDonor_Click(object sender, EventArgs e)
        {
            EditDonorForm frmTmp = new EditDonorForm(CCFBGlobal.connectionString, true);
            frmTmp.ShowDialog(this);
            int newDonorId = frmTmp.SelectedId;
            string newDonorName = frmTmp.SelectedName;
            frmTmp.Close();
            frmTmp.Dispose ();
            Button btn = (Button)sender;
            string tagVal = btn.Tag.ToString();
            foreach (TextBox tb in grpbxDonors.Controls.OfType<TextBox>())
            {
                if (tb.Tag.ToString() == tagVal)
                {
                    tb.Text = newDonorId.ToString();
                    CCFBPrefs.SaveValue(tagVal,newDonorId.ToString());
                    dataChanged = true;
                    break;
                }
            }
            foreach (Label lbl in grpbxDonors.Controls.OfType<Label>())
            {
                if (lbl.Tag.ToString() == tagVal)
                {
                    if (newDonorName == "")
                        lbl.Text = ".....";
                    else
                        lbl.Text = newDonorName;
                    break;
                }
            }
        }
        private void LoadDonorLabelNames()
        {
            Donors clsDonors = new Donors(CCFBGlobal.connectionString);
            int donorid = 0;
            foreach (Label lbl in grpbxDonors.Controls.OfType<Label>())
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
                    default: break;
                }
                lbl.Text = ".....";
                if (donorid > 0)
                {
                    clsDonors.openWhere("Id = " + donorid.ToString());
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

        private void EditPreferencesForm_Load(object sender, EventArgs e)
        {

        }
    }
}
