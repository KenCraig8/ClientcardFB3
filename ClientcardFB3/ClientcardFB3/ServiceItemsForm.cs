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
    public partial class ServiceItemsForm : Form
    {

        ServiceItems clsSvcItems;
        List<TextBox> tbList = new List<TextBox>();     //A collection of the textboxes in form
        List<CheckBox> chkList = new List<CheckBox>();  //A collection of the checkboxes in form

        bool loadingData = true;
        bool loadingListView = true;

        CheckBox[] chkArray = new CheckBox[5];

        ListViewColumnSorter lvColSorter;

        public ServiceItemsForm()
        {
            InitializeComponent();

            clsSvcItems = new ServiceItems(CCFBGlobal.connectionString);
            grpFamilySizes.BackColor = CCFBGlobal.bkColorBaseEdit;
            pnlEditArea.BackColor = CCFBGlobal.bkColorBaseEdit;

            lvColSorter = new ListViewColumnSorter();
            lvColSorter.Order = SortOrder.None;

            CCFBGlobal.InitCombo(cboType, CCFBGlobal.parmTbl_SvcCategory);
            CCFBGlobal.InitCombo(cboRules, CCFBGlobal.parmTbl_SvcRules);

            CCFBGlobal.InitCombo(cboFilter, CCFBGlobal.parmTbl_SvcCategory);

            traverseAndAddControlsToCollections(this.Controls);
            loadingData = false;
            cboFilter.SelectedValue = "0";

            chkArray[0] = chkFirstSvc;
            chkArray[1] = chkSecondSvc;
            chkArray[2] = chkThirdSvc;
            chkArray[3] = chkFourthSvc;
            chkArray[4] = chkFivePlusSvc;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int itemKey = Convert.ToInt32(lvServiceItems.SelectedItems[0].SubItems[3].Text);
            int iResult = clsSvcItems.ItemUsedRecently(itemKey);
            if (iResult > 0)
            {
                if (MessageBox.Show(this, "ItemKey = " + itemKey.ToString() + " has " + iResult.ToString() + " Service Transactions logged .\r\n"
                              + "Press Ok to Mark as Not Available.\r\n"
                              + "Press Cancel to do nothing.", "Delete Service Item", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    chkNotAvailable.Checked = true;
                }

            }
            else
            {
                clsSvcItems.delete(itemKey);
                CCFBGlobal.ServiceItemsChanged = true;
                clsSvcItems.openWhere("");
                loadList("", false);

                if (lvServiceItems.Items.Count > 0)
                {
                    lvServiceItems.Items[0].Selected = true;
                }
                else
                {
                    foreach (TextBox tb in tbList)
                    {
                        tb.Text = "";
                    }

                    cboRules.Text = "";
                    cboType.SelectedIndex = 0;
                }
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            int iPos = lvServiceItems.SelectedItems[0].Index;
            if (iPos < lvServiceItems.Items.Count - 1)
                lvServiceItems.Items[iPos + 1].Selected = true;
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            int iPos = lvServiceItems.SelectedItems[0].Index;
            if (iPos > 0)
                lvServiceItems.Items[iPos - 1].Selected = true;
        }

        private void cboFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            setLoadFilter();
        }

        private void chkIncludeNotAvailable_CheckedChanged(object sender, EventArgs e)
        {
            setLoadFilter();
        }

        private void chkList_CheckedChanged(object sender, EventArgs e)
        {
            if (tbID.Text != "" && loadingListView == false && loadingData == false)
            {
                CheckBox chkTest = (CheckBox)sender;

                if (chkTest.Checked != clsSvcItems.GetDataValueBool(chkTest.Tag.ToString()))
                {
                    clsSvcItems.SetDataValue(chkTest.Tag.ToString(), chkTest.Checked);
                    SaveChangesToDB(false);
                    if (chkTest.Tag.ToString() == "NotAvailable")
                        setlvServicesImage(chkTest.Checked, lvServiceItems.Items[tbID.Text]);
                    else if (chkTest.Tag.ToString() == "UseAgeGroup")
                        ShowAgeGroupBox(chkLimitByAgeGroups.Checked);
                }
            }
        }

        private void fillForm(int itemKey)
        {
            loadingData = true;
            if (tbID.Text != itemKey.ToString())
                clsSvcItems.find(itemKey);

            foreach (TextBox tb in tbList)
            { tb.Text = clsSvcItems.GetDataValue(tb.Tag.ToString()).ToString(); }

            foreach (CheckBox chk in chkList)
            { chk.Checked = clsSvcItems.GetDataValueBool(chk.Tag.ToString()); }

            cboRules.SelectedValue = clsSvcItems.ItemRule.ToString();
            cboType.SelectedValue = clsSvcItems.ItemType.ToString();
            ShowAgeGroupBox(chkLimitByAgeGroups.Checked);
            lblServiceItems.Text = clsSvcItems.DSet.Tables[0].Rows.Count.ToString() + " Service Items";

            grpbxMask.Enabled = false;

            for (int i = 0; i < chkArray.Length; i++)
            {
                chkArray[i].Checked = false;
            }

            if (clsSvcItems.ItemRule == CCFBGlobal.itemRule_MaskArray)
            {
                grpbxMask.Enabled = true;
                char[] useForServiceCount = clsSvcItems.Mask.ToCharArray();
                for (int i = 0; i < chkArray.Length; i++)
                {
                    if (useForServiceCount[i] == '1')
                        chkArray[i].Checked = true;
                }
            }

            loadingData = false;
            if (lvServiceItems.SelectedItems.Count > 0)
            {
                int iPos = lvServiceItems.SelectedItems[0].Index;
                btnPrevious.Visible = (iPos > 0);
                btnNext.Visible = (iPos < lvServiceItems.Items.Count - 1);
            }
            else
            {
                btnPrevious.Visible = true;
                btnNext.Visible = true;
            }
            tbLbsPerItem.Focus();
        }

        private void loadList(string testId, bool disableSelect)
        {
            loadingListView = disableSelect;
            lvServiceItems.ListViewItemSorter = null;
            lvServiceItems.Items.Clear();
            //Application.DoEvents();
            for (int i = 0; i < clsSvcItems.RowCount; i++)
            {
                ListViewItem lvi = new ListViewItem(CCFBGlobal.LongNameFromId(CCFBGlobal.parmTbl_SvcCategory
                                , Convert.ToInt32(clsSvcItems.GetDataValue("ItemType",i).ToString())));
                lvi.Name = clsSvcItems.GetDataValue("ItemKey",i).ToString();
                lvi.SubItems.Add(clsSvcItems.GetDataValue("ItemDesc",i).ToString());
                lvi.SubItems.Add(clsSvcItems.GetDataValue("LbsPerItem",i).ToString());
                lvi.SubItems.Add(clsSvcItems.GetDataValue("ItemKey",i).ToString());
                lvi.UseItemStyleForSubItems = false; 
                lvi.SubItems[3].ForeColor = Color.Blue;
                setlvServicesImage(clsSvcItems.GetDataValueBool("NotAvailable", i), lvi);
                lvServiceItems.Items.Add(lvi);
                Application.DoEvents();
            }
            if (testId != "")
                lvServiceItems.Items[testId].Selected = true;
            else if (lvServiceItems.Items.Count > 0)
            {
                loadingListView = false;
                lvServiceItems.Items[0].Selected = true;
            }
            lvServiceItems.ListViewItemSorter = lvColSorter;
            loadingListView = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            lvServiceItems.Focus();
            Application.DoEvents();
            this.Close();
        }

        private void lvServiceItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (loadingListView == false)
            {
                try
                {
                    if (lvServiceItems.SelectedItems.Count > 0 )
                        fillForm(Convert.ToInt32(lvServiceItems.SelectedItems[0].SubItems[3].Text));
                }
                catch (Exception ex)
                {
                    CCFBGlobal.appendErrorToErrorReport("sender=" + sender.ToString() + "EventArgs=" + e.ToString(),
                        ex.GetBaseException().ToString());
                }
            }
        }

        private void cboType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (loadingData == false)
            {
                clsSvcItems.ItemType = Convert.ToInt32(cboType.SelectedValue.ToString());
                SaveChangesToDB(false);
                
                if(lvServiceItems.Items.Count > 0)
                    lvServiceItems.Items[tbID.Text].Text = cboType.Text;
             
                //loadList(tbID.Text,true);
                //clsSvcItems.find(Convert.ToInt32(tbID.Text));
            }
        }

        private void cboRules_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (loadingData == false)
            {
                clsSvcItems.ItemRule = Convert.ToInt32(cboRules.SelectedValue.ToString());
                
                if (clsSvcItems.ItemRule == CCFBGlobal.itemRule_MaskArray)
                    grpbxMask.Enabled = true;

                SaveChangesToDB(false);
            }
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            UserInputForm frmUserInput = new UserInputForm(this);
            frmUserInput.ShowDialog();

        }

        public void AddNewItem(string description, int itemRule, int itemCategory)
        {
            DataRow newRow = clsSvcItems.DSet.Tables[0].NewRow();

            newRow["ItemDesc"] = description;
            newRow["ItemType"] = itemCategory;
            newRow["NotAvailable"] = 0;
            newRow["ItemRule"] = itemRule;
            newRow["LbsPerItem"] = 1;
            newRow["FS01"] = 1;
            newRow["FS02"] = 1;
            newRow["FS03"] = 1;
            newRow["FS04"] = 1;
            newRow["FS05"] = 1;
            newRow["FS06"] = 1;
            newRow["FS07"] = 1;
            newRow["FS08"] = 1;
            newRow["FS09"] = 1;
            newRow["FS10"] = 1;
            newRow["FS11"] = 1;
            newRow["FS12"] = 1;
            newRow["FS13"] = 1;
            newRow["FS14"] = 1;
            newRow["FS15"] = 1;
            newRow["FS16"] = 1;
            newRow["FS17"] = 1;
            newRow["FS18"] = 1;
            newRow["FS19"] = 1;
            newRow["FS20"] = 1;
            newRow["UseAgeGroup"] = 0;
            newRow["AllowInfants"] = 0;
            newRow["AllowYouth"] = 0;
            newRow["AllowTeens"] = 0;
            newRow["AllowAdults"] = 0;
            newRow["AllowSeniors"] = 0;
            newRow["CreatedBy"] = CCFBGlobal.dbUserName;
            newRow["Created"] = DateTime.Now;
            newRow["ModifiedBy"] = "";
            newRow["SvcMask"] = "00000";
            newRow["Exclusive"] = 0;

            clsSvcItems.DSet.Tables[0].Rows.Add(newRow);
            clsSvcItems.update(true);
            CCFBGlobal.ServiceItemsChanged = true;

            int maxId = clsSvcItems.findMaxId();
            loadList(maxId.ToString(),true);
            if (tbID.Text != maxId.ToString())
                fillForm(maxId);
        }

        private void lvServiceItems_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == lvColSorter.SortColumn)
            {   // Reverse the current sort direction for this column.
                if (lvColSorter.Order != SortOrder.Descending)
                {
                    lvColSorter.Order = SortOrder.Descending;
                }
                else
                {
                    lvColSorter.Order = SortOrder.Ascending;
                }
                lvServiceItems.Sort();     // Perform the sort with these new sort options.
            }
            else
            {   // Set the column number that is to be sorted; default to ascending.
                if (e.Column < 2 || e.Column == 3)
                {
                    lvColSorter.SortColumn = e.Column;
                    lvColSorter.Order = SortOrder.Ascending;
                    lvServiceItems.Sort();     // Perform the sort with these new sort options.
                }
            }
        }

        private void tbList_LostFocus(object sender, EventArgs e)
        {
            if (tbID.Text != "" && loadingListView == false)
            {
                TextBox tbWork = (TextBox)sender;

                if (tbWork.Tag.ToString() != "ItemKey")
                {
                    if (tbWork.Text.ToString().Trim() != clsSvcItems.GetDataValue(tbWork.Tag.ToString()).ToString())
                    {
                        clsSvcItems.SetDataValue(tbWork.Tag.ToString(), tbWork.Text);
                        SaveChangesToDB(false);
                        int subItemNbr = 0;
                        switch (tbWork.Tag.ToString())
                        {
                            case "ItemDesc":    { subItemNbr = 1; break; }
                            case "LbsPerItem":  { subItemNbr = 2; break; }
                        }
                        if (subItemNbr > 0)
                            lvServiceItems.Items[tbID.Text].SubItems[subItemNbr].Text = tbWork.Text;
                    }
                }
                else
                {
                    tbWork.Text = clsSvcItems.GetDataValue(tbWork.Tag.ToString()).ToString();
                }
            }
        }

        private void tbIntOnly_KeyDown(object sender, KeyEventArgs e)
        {
            CCFBGlobal.checkForIntOnKeyPress(e);
        }

        private void SaveChangesToDB(bool reLoad)
        {
            CCFBGlobal.ServiceItemsChanged = true;
            clsSvcItems.Modified = DateTime.Today;
            clsSvcItems.ModifiedBy = CCFBGlobal.currentUser_Name;
            clsSvcItems.update(reLoad);
        }

        
        private void ShowAgeGroupBox(bool isEnabled)
        {
           grpbxAgeGroups.Enabled = isEnabled;
           if (isEnabled == true)
               grpbxAgeGroups.BackColor = CCFBGlobal.bkColorAltEdit;
           else
               grpbxAgeGroups.BackColor = CCFBGlobal.bkColorFormAlt;
        }

        private void setLoadFilter()
        {
            if (loadingData == false)
            {
                string sFilter = "";
                if (cboFilter.SelectedItem != null)
                {
                    parmType pt = (parmType)cboFilter.SelectedItem;
                    if (pt.ID > 0)
                    {
                        sFilter = "ItemType = " + pt.ID.ToString();
                        if (chkIncludeNotAvailable.Checked == false)
                            sFilter += " AND NotAvailable =0";
                    }
                    else if (chkIncludeNotAvailable.Checked == false)
                        sFilter += " NotAvailable =0";
                }
                else if (chkIncludeNotAvailable.Checked == false)
                    sFilter += " NotAvailable =0";

                clsSvcItems.openWhere(sFilter);
                loadList("", false);
            }
        }

        private void setlvServicesImage(bool isChecked, ListViewItem lvi)
        {
            if (isChecked == true)
            {
                lvi.ForeColor = Color.DarkRed;
                lvi.SubItems[1].ForeColor = Color.DarkRed;
                lvi.SubItems[2].ForeColor = Color.DarkRed;
            }
            else
            {
                lvi.ForeColor = Color.Black;
                lvi.SubItems[1].ForeColor = Color.Black;
                lvi.SubItems[2].ForeColor = Color.Black;
            }
        }

        private void traverseAndAddControlsToCollections(Control.ControlCollection controlList)
        {
            foreach (Control cntrl in controlList.OfType<Control>())
            {
                if (cntrl.Tag != null && cntrl.Tag != "")
                {
                    switch (cntrl.GetType().Name)
                    {
                        case "TextBox":
                            {
                                TextBox tb = (TextBox)cntrl;
                                if (tb.Tag.ToString() != "ItemKey")
                                    tb.LostFocus += new System.EventHandler(this.tbList_LostFocus);
                                else
                                {
                                    tb.ReadOnly = true;
                                    tb.BackColor = Color.PaleGoldenrod;
                                }
                                tbList.Add(tb);
                                break;
                            }
                        case "CheckBox":
                            {
                                CheckBox chk = (CheckBox)cntrl;
                                chk.CheckedChanged += new System.EventHandler(this.chkList_CheckedChanged);
                                chkList.Add(chk);
                                break;
                            }
                    }
                }

                traverseAndAddControlsToCollections(cntrl.Controls);
            }
        }

        private void chkUseForSvc_CheckedChanged(object sender, EventArgs e)
        {
            if (loadingData == false)
            {
                string mask = "";
                for (int i = 0; i < chkArray.Length; i++)
                {
                    mask += Convert.ToInt32(chkArray[i].Checked).ToString();
                }
                clsSvcItems.SetDataValue("SvcMask", mask);
                clsSvcItems.update(false);
            }
        }

        private void pnlEditArea_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
