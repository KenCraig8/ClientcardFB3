using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace ClientcardFB3
{
    public partial class FBJobsPlanForm : Form
    {
        bool bAlreadyHere = false;
        bool bEditingCell = false;
        bool bNormalMode = false;
        string celloriginalval;
        FBJobsPlan clsJobsPlan;
        Volunteers clsVols;
        int curJobId = 0;
        int curWeekDay = 0;
        string whereClauseVols;
        List<DataGridViewRow> copyList = new List<DataGridViewRow>();
        int copyListWeekDay = -1;
        public FBJobsPlanForm()
        {
            InitializeComponent();
            clsJobsPlan = new FBJobsPlan(CCFBGlobal.connectionString);
            clsVols = new Volunteers(CCFBGlobal.connectionString);
            showJobsView();
            InitlvwJobs(CCFBGlobal.TypeCodesArray(CCFBGlobal.parmTbl_FBJobs));
            tsb1.Checked = true;
            setupCopyMenu();
        }

        private void btnAddJobsToPlan_Click(object sender, EventArgs e)
        {
            Application.UseWaitCursor = true;
            foreach (ListViewItem item in lvwJobs.SelectedItems)
            {
                clsJobsPlan.insert(curWeekDay, Convert.ToInt32(item.Tag.ToString()), item.Text, tbStart.Text, tbEnd.Text, 0, 0);
            }
            Application.UseWaitCursor = false; 
            fillForm();
        }

        private void dgvJobPlans_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            setupMenu();
        }

        private void dgvJobPlans_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (bNormalMode == true)
            {
                DataGridViewRow dgvr = dgvJobPlans.Rows[e.RowIndex];
                int col = e.ColumnIndex;
                string planid = dgvr.Tag.ToString();
                switch (dgvJobPlans.Columns[col].DataPropertyName.ToLower())
                {
                    case "jobtitle":
                        clsJobsPlan.updateField(planid, dgvJobPlans.Columns[col].DataPropertyName, dgvr.Cells[col].Value.ToString());
                        break;
                    case "shiftstart":
                    case "shiftend":
                        {
                            string testval = dgvr.Cells[col].Value.ToString().Replace(".",":").Trim();
                            if (testval.Length >0)
                            {
                                dgvr.Cells[col].ErrorText = "";
                                verifyTimeHasColon(ref testval);
                                if (validateTime(testval) == false)
                                {
                                    dgvr.Cells[col].ErrorText = "Time is Invalid";
                                    dgvr.Cells[col].Style.BackColor = Color.LightPink;
                                    MessageBox.Show("Time is Invalid");
                                }
                                else
                                {
                                    dgvr.Cells[col].Value = testval;
                                    dgvr.Cells[col].Style.BackColor = Color.White;
                                    clsJobsPlan.updateField(planid, dgvJobPlans.Columns[col].DataPropertyName, dgvr.Cells[col].Value.ToString());
                                }
                            }
                            break;
                        }
                    case "volidprimary":
                    case "volidbackup":
                        clsJobsPlan.updateField(planid, dgvJobPlans.Columns[col].DataPropertyName, Convert.ToInt32(dgvr.Cells[col].Tag.ToString()));
                        break;
                    default:
                        break;
                }
            }
        }

        private void dgvJobPlans_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(ListViewItem)) == true)
            {
                ListViewItem data = (ListViewItem)e.Data.GetData(typeof(ListViewItem));
                if (data == null) { return; }
                Point cursorPosition = dgvJobPlans.PointToClient(Cursor.Position);
                DataGridView.HitTestInfo hitPosInfo = dgvJobPlans.HitTest(cursorPosition.X, cursorPosition.Y);
                if (hitPosInfo.RowIndex >= 0)
                {
                    if (hitPosInfo.ColumnIndex >= 3 && hitPosInfo.ColumnIndex <= 4)
                    {
                        dgvJobPlans.Rows[hitPosInfo.RowIndex].Cells[hitPosInfo.ColumnIndex].Tag = data.Tag; 
                        dgvJobPlans.Rows[hitPosInfo.RowIndex].Cells[hitPosInfo.ColumnIndex].Value = data.Text;
                    }
                }
            }
        }

        private void dgvJobPlans_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void dgvJobPlans_DragLeave(object sender, EventArgs e)
        {
            
        }

        private void dgvJobPlans_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void dgvJobPlans_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (bNormalMode == true)
            {
                if (e.RowIndex >= 0 && dgvJobPlans.CurrentRow.Index != e.RowIndex)
                {
                    curJobId = Convert.ToInt32(dgvJobPlans.Rows[e.RowIndex].Cells[0].Tag);
                    InitlvwVols();
                }
            }
        }

        private void fillForm()
        {
            bNormalMode = false;
            Application.UseWaitCursor = true;
            int newRowIndex = 0;
            dgvJobPlans.Rows.Clear();
            dgvJobPlans.Tag = curWeekDay;
            Application.DoEvents();
            clsJobsPlan.openForWeekDay(curWeekDay);
            for (int i = 0; i < clsJobsPlan.RowCount; i++)
            {
                clsJobsPlan.SetRecord(i);
                newRowIndex = dgvJobPlans.Rows.Add();
                DataGridViewRow dgvr = dgvJobPlans.Rows[newRowIndex];
                dgvr.Tag = clsJobsPlan.PlanID;
                foreach (DataGridViewColumn dgvCol in dgvJobPlans.Columns)
                {
                    int col = dgvCol.DisplayIndex;
                    switch (dgvCol.DataPropertyName.ToLower())
                    {
                        case "jobtitle":
                            dgvr.Cells[col].Value = clsJobsPlan.JobTitle;
                            dgvr.Cells[col].Tag = clsJobsPlan.JobID;
                            dgvr.Cells[col].Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                            break;
                        case "shiftstart":
                            dgvr.Cells[col].Value = clsJobsPlan.ShiftStart;
                            break;
                        case "shiftend":
                            dgvr.Cells[col].Value = clsJobsPlan.ShiftEnd;
                            break;
                        case "volidprimary":
                            dgvr.Cells[col].Value = clsJobsPlan.PrimaryName;
                            dgvr.Cells[col].Tag = clsJobsPlan.VolIdPrimary;
                            break;
                        case "volidbackup":
                            dgvr.Cells[col].Value = clsJobsPlan.BackupName;
                            dgvr.Cells[col].Tag = clsJobsPlan.VolIdBackup;
                            dgvr.Cells[col].Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                            break;
                        default:
                            break;
                    }
                }
            }
            Application.UseWaitCursor = false;
            bNormalMode = true;
            if (dgvJobPlans.CurrentRow != null)
            {
                curJobId = Convert.ToInt32(dgvJobPlans.CurrentRow.Cells[0].Tag);
                InitlvwVols();
            }
            setupCopyMenu();
            setupMenu();
        }

        private void InitlvwJobs(System.Collections.ArrayList list)
        {
            lvwJobs.Items.Clear();
            foreach (parmType item in list)
            {
                ListViewItem lvi = new ListViewItem(item.LongName);
                lvi.Tag = item.ID;
                if (curJobId < 0)
                    curJobId = item.ID;
                lvwJobs.Items.Add(lvi);
            }
        }

        private void InitlvwVols()
        {
            lvwVols.Items.Clear();
            lvwVols.Columns[0].Text = lvwJobs.Items[curJobId].Text;
            Application.DoEvents();
            clsVols.openByJobID(curJobId.ToString());
            for (int i = 0; i < clsVols.RowCount; i++)
            {
                clsVols.setRecord(i);
                ListViewItem lvi = new ListViewItem(clsVols.Name);
                lvi.Tag = clsVols.ID;
                lvwVols.Items.Add(lvi);
            }
        }

        private bool isVolItemOK(DataGridViewCell tst1Cell, DataGridViewCell tst2Cell)
        {
            return (lvwVols.Visible && lvwVols.SelectedItems.Count > 0
                && lvwVols.SelectedItems[0].Tag.ToString() != cellTagString(tst1Cell)
                && lvwVols.SelectedItems[0].Tag.ToString() != cellTagString(tst2Cell));
        }

        private void lvwVols_ItemActivate(object sender, EventArgs e)
        {
            if ((mnuAddBackup.Enabled) || (mnuAddPrimary.Enabled))
            {
                postVolunteerToGrid();
            }
        }

        private void lvwVols_MouseDown(object sender, MouseEventArgs e)
        {
            if (lvwVols.SelectedItems.Count > 0)
                this.lvwVols.DoDragDrop(this.lvwVols.SelectedItems[0], DragDropEffects.Copy);
        }

        private void lvwVols_SelectedIndexChanged(object sender, EventArgs e)
        {
            setupMenu();
        }

        private void markBackupEnabledFalse()
        {
            mnuAddBackup.Enabled = false;
            mnuDeleteBackup.Enabled = false;
            //toolStripSeparator4.Visible = false;
        }

        private void markPrimaryEnabledFalse()
        {
            mnuAddPrimary.Enabled = false;
            mnuDeletePrimary.Enabled = false;
            //toolStripSeparator3.Visible = false;
        }

        private void mnuAddJPVol_Click(object sender, EventArgs e)
        {
            postVolunteerToGrid();
        }

        private void mnuDeleteJob_Click(object sender, EventArgs e)
        {
            DataGridViewCell theCell = dgvJobPlans.CurrentCell;
            if (theCell.ColumnIndex == 0 && theCell.RowIndex >= 0)
            {
                clsJobsPlan.delete(Convert.ToInt32(dgvJobPlans.CurrentRow.Tag.ToString()));
                fillForm();
            }
        }

        private void mnuDeleteJPVol_Click(object sender, EventArgs e)
        {
            DataGridViewCell theCell = dgvJobPlans.CurrentCell;
            if ((theCell.ColumnIndex == 3 || theCell.ColumnIndex == 4) && theCell.RowIndex >= 0)
            {
                theCell.Value = "--------";
                theCell.Tag = null;
                clsJobsPlan.updateField(dgvJobPlans.CurrentRow.Tag.ToString()
                    , dgvJobPlans.Columns[theCell.ColumnIndex].DataPropertyName, 0);
                setupMenu();
            }
        }

        private void postVolunteerToGrid()
        {
            DataGridViewCell theCell = dgvJobPlans.CurrentCell;
            if ((theCell.ColumnIndex == 3 || theCell.ColumnIndex == 4) && theCell.RowIndex >= 0)
            {
                theCell.Value = lvwVols.SelectedItems[0].Text;
                theCell.Tag = lvwVols.SelectedItems[0].Tag;
                clsJobsPlan.updateField(dgvJobPlans.CurrentRow.Tag.ToString()
                    , dgvJobPlans.Columns[theCell.ColumnIndex].DataPropertyName, Convert.ToInt32(theCell.Tag));
                setupMenu();
            }
        }

        private void setupCopyMenu()
        {
            tsmiPaste.Enabled = (copyList.Count > 0 && curWeekDay != copyListWeekDay);
            tsmiCopyAll.Enabled = (dgvJobPlans.RowCount > 0);
            tsmiCopySelected.Enabled = (dgvJobPlans.SelectedRows.Count > 0);
        }

        private void setupMenu()
        {
            mnuAddJobs.Enabled = lvwJobs.Visible;
            mnuDeleteJob.Enabled = false;
            if (dgvJobPlans.CurrentCell != null)
            {
                DataGridViewCell theCell = dgvJobPlans.CurrentCell;
                switch (dgvJobPlans.Columns[theCell.ColumnIndex].DataPropertyName.ToLower())
                {
                    case "jobtitle":
                        mnuDeleteJob.Enabled = true;
                        break;
                    case "shiftstart":
                    case "shiftend":
                        markPrimaryEnabledFalse();
                        markBackupEnabledFalse();
                        
                        break;
                    case "volidprimary":
                        mnuAddPrimary.Enabled = isVolItemOK(theCell, dgvJobPlans.CurrentRow.Cells[theCell.ColumnIndex + 1]);
                        mnuDeletePrimary.Enabled = (Convert.ToInt32(theCell.Tag) > 0);
                        //toolStripSeparator3.Visible = (lvwVols.Visible && lvwVols.SelectedItems.Count > 0 && lvwVols.SelectedItems[0].Tag.ToString() != theCell.Tag.ToString()) || (Convert.ToInt32(theCell.Tag) > 0);
                        markBackupEnabledFalse();
                        break;
                    case "volidbackup":
                        mnuAddBackup.Enabled = isVolItemOK(theCell, dgvJobPlans.CurrentRow.Cells[theCell.ColumnIndex-1]);
                        mnuDeleteBackup.Enabled = (Convert.ToInt32(theCell.Tag) > 0);
                        //toolStripSeparator4.Visible = (lvwVols.Visible && lvwVols.SelectedItems.Count > 0 && lvwVols.SelectedItems[0].Tag.ToString() != theCell.Tag.ToString()) || (Convert.ToInt32(theCell.Tag) > 0);
                        markPrimaryEnabledFalse();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                markPrimaryEnabledFalse();
                markBackupEnabledFalse();
            }
        }

        private void showJobsView()
        {
            spltcJobs.Visible = true;
            tsbShowVols.Checked = false;
            lvwVols.Visible = false;
            setupMenu();
        }

        private void tsb_CheckedChanged(object sender, EventArgs e)
        {
            if (bAlreadyHere == false)
            {
                bAlreadyHere = true;
                ToolStripButton tsb = (ToolStripButton)sender;
                curWeekDay = Convert.ToInt32(tsb.Tag);
                foreach (ToolStripButton tsbItem in toolStrip2.Items)
                {
                    if (tsbItem != tsb)
                        tsbItem.Checked = false;
                }
                bAlreadyHere = false;
                fillForm();
            }
        }

        private void tsbShowJobs_Click(object sender, EventArgs e)
        {
            showJobsView();
        }

        private void tsbShowVols_Click(object sender, EventArgs e)
        {
            spltcJobs.Visible = false;
            tsbShowJobs.Checked = false;
            lvwVols.Visible = true;
            setupMenu();
        }

        public bool validateTime(string thetime)
        {
            //Regex checktime = new Regex(@"^(20|21|22|23|[01]d|d)(([:][0-5]d){1,2})$");
            Regex checktime = new Regex(@"^([0-9]|1[0-2]|0[1-9]){1}(:[0-5][0-9] [aApP][mM]|:[0-5][0-9][aApP][mM]){1}$");
            if (checktime.IsMatch(thetime) == true)
                return true;
            Regex checktime2 = new Regex(@"^(20|21|22|23|[01]\d|\d)(([:][0-5]\d){1,2})$");
            return checktime2.IsMatch(thetime);
        }

        public void verifyTimeHasColon(ref string val)
        {
            string newval="";
            if (val.Contains(":") == true)
                return;
            switch (val.Length)
            {
                case 1:
                case 2:
                    newval = val + ":00";
                    break;
                case 3:
                case 4:
                    newval = val.Insert(val.Length-2, ":");
                    break;
                default:
                    string[] tmp = val.Split(' ');
                    if (tmp.Length > 1)
                    {
                        newval = tmp[0];
                        switch (newval.Length)
                        {
                            case 1:
                            case 2:
                                newval = newval + ":00";
                                break;
                            case 3:
                            case 4:
                                newval.Insert(newval.Length - 2, ":");
                                break;
                        }
                        for (int i = 1; i < tmp.Length; i++)
                        {
                            newval += " " + tmp[i];
                        }
                    }
                    else
                    {
                        char[] ampm = new char[4];
                        ampm[0] = 'a';
                        ampm[1] = 'A';
                        ampm[2] = 'p';
                        ampm[3] = 'P';

                        int pos = val.IndexOfAny(ampm);
                        if (pos > 0)
                            newval = val.Insert(pos - 1, ":");
                        else
                            newval = val;
                    }
                    break;
            }
            val = newval;
        }

        private string cellTagString(DataGridViewCell tstcell)
        {
            if (tstcell.Tag == null)
                return "";
            return tstcell.Tag.ToString();   
        }

        private void lvwJobs_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            btnAddJobsToPlan.Enabled = (lvwJobs.SelectedItems.Count > 0);
        }

        private void tsmiCopyAll_Click(object sender, EventArgs e)
        {
            copyList = new List<DataGridViewRow>();
            foreach (DataGridViewRow item in dgvJobPlans.Rows)
            {
                copyList.Add(item);
            }
            tsmiPaste.Enabled = false;
        }

        private void tsmiCopySelected_Click(object sender, EventArgs e)
        {
            copyList = new List<DataGridViewRow>();
            if (dgvJobPlans.SelectedRows.Count>0)
            {
                copyList.Add(dgvJobPlans.SelectedRows[0]);
            }
            tsmiPaste.Enabled = false;
        }

        private void tsmiPaste_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow item in copyList)
            {
                clsJobsPlan.insert(curWeekDay, item.Tag.ToString()
                                , item.Cells["colJobTitle"].ToString()
                                , item.Cells["colShiftStart"].ToString()
                                , item.Cells["colShiftEnd"].ToString()
                                , item.Cells["colVolIdPrimary"].ToString()
                                , item.Cells["colVolIdBackup"].ToString());
            }
            tsmiPaste.Enabled = false;
            fillForm();
        }
    }
}
