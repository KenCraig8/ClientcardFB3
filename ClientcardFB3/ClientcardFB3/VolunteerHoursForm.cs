using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace ClientcardFB3
{
    public partial class VolunteerHoursForm : Form
    {
        VolunteerHours clsVolHrs = new VolunteerHours(CCFBGlobal.connectionString);
        Volunteers clsVols = new Volunteers(CCFBGlobal.connectionString);
        VolGroups clsVolGroups = new VolGroups(CCFBGlobal.connectionString);

        SqlDataAdapter dadAdpt;
        DataSet dset;
        SqlCommand command;
        SqlConnection conn;
        bool bAlreadyHere = true;
        string baseWhereClause = " WHERE Inactive = 0 AND NotOnHoursList = 0";
        string groupVolIdList = "";
        string sTrxDateMin = "1/1/1990";
        string sTrxDateMax = "12/31/2099";
        const string constGroupName = "lvGrpGroups";
        int[] idxGroupId;
        string timeBeforeFormat;
        bool bSkipTotal = false;

        public VolunteerHoursForm()
        {
            InitializeComponent();

            dgvVols.Columns["clmTimeIn"].Visible = CCFBPrefs.UseTimeInOutForVols;
            dgvVols.Columns["clmTimeOut"].Visible = CCFBPrefs.UseTimeInOutForVols;

            dadAdpt = new SqlDataAdapter();
            dset = new DataSet();
            conn = new SqlConnection(CCFBGlobal.connectionString);
            dgvVols.Visible = false;

            clsVolGroups.LoadAllMembers();
            idxGroupId = new int[clsVolGroups.NbrGroups + 1];
            filllvVolGroups();

            if (rdoCurMonth.Checked == true)
                ShowDatesForCurMonth();
            else if (rdoPrevMonth.Checked == true)
                ShowDatesForPrevMonth();
            else if (rdoCurYear.Checked == true)
                ShowDatesForCurYear();
            else if (rdoPrevYear.Checked == true)
                ShowDatesForPrevYear();
            else
                loadDatesList(sTrxDateMin, sTrxDateMax);
        }

        private void filllvVolGroups()
        {
            for (int i = 0; i < clsVolGroups.NbrGroups; i++)
            {
                clsVolGroups.GroupIndex = i;
                VolGroup volgrp = clsVolGroups.VolunGroup;
                idxGroupId[i] = volgrp.GroupID;
                ListViewItem lvItm = new ListViewItem();
                
                lvItm.Group = lvVolGroups.Groups[constGroupName];
                lvItm.Tag = volgrp.GroupID;
                lvItm.SubItems.Add(volgrp.GroupName);
                lvItm.SubItems.Add("0");
                lvVolGroups.Items.Add(lvItm);
            }
        }

        public void loadDatesList(string minDate, string maxDate)
        {
            sTrxDateMin = minDate;
            sTrxDateMax = maxDate;
            command = new SqlCommand("Select TrxDate, Sum(NumVolunteers) as NumVols, "
                    + "Sum(NumVolHours) as NumVolHrs from VolunteerHours Where TrxDate "
                    + "between '" + minDate.ToString() + "' and '" + maxDate.ToString() + "'"
                    + "Group By TrxDate Order By TrxDate Desc", conn);

            Single totHours = 0;
            int totVols = 0;

            try
            {
                dadAdpt.SelectCommand = command;
                dset.Clear();
                dadAdpt.Fill(dset);
            }
            catch (SqlException ex) 
            {
                closeConnection();
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString());
            }

            lvDates.Items.Clear();

            ListViewItem lvi;

            for (int i = 0; i < dset.Tables[0].Rows.Count; i++)
            {
                lvi = new ListViewItem(getFormatedDate(dset.Tables[0].Rows[i]["TrxDate"]));
                totHours += Convert.ToSingle(dset.Tables[0].Rows[i]["NumVolHrs"]);
                totVols += int.Parse(dset.Tables[0].Rows[i]["NumVols"].ToString());
                lvi.SubItems.Add(dset.Tables[0].Rows[i]["NumVols"].ToString());
                lvi.SubItems.Add(Convert.ToSingle(dset.Tables[0].Rows[i]["NumVolHrs"]).ToString("F"));
                lvDates.Items.Add(lvi);
            }

            dgvVols.Visible = false;
            initVolGroupValues();
            tbNumVols.Text = totVols.ToString();
            tbTotVolHrs.Text = totHours.ToString("F");
        }

        private string getFormatedDate(object date)
        {
            if(date != null)
            {
                return ((DateTime)date).ToShortDateString();
            }
            return "";
        }

        private void loadVolsGrid()
        {
            bool hrsSet;
            int volId = 0;
            Single sumVolHrs = 0;
            Single volHrs = 0;
            int[] hrsGroup = new int[clsVolGroups.NbrGroups + 1];
            dgvVols.Rows.Clear();
            Application.DoEvents();
            if (groupVolIdList == "")
                clsVols.openWhere(baseWhereClause + " Order By Name");
            else
                clsVols.openWhere(baseWhereClause + " AND Id IN ( " + groupVolIdList + ")" + " Order By Name"); 
            for (int k = 0; k < clsVolGroups.NbrGroups; k++)
			{
                clsVolGroups.GroupIndex = k;
			    clsVolGroups.VolunGroup.VolHrs = 0;
			}

            clsVolHrs.openWhere("WHERE TrxDate = '" + tbWorkDate.Tag.ToString() + "'");
            int i = 0;
            foreach (DataRow drow in clsVols.DSet.Tables[0].Rows)
            //for (int i = 0; i < clsVols.RowCount; i++)
            {
                dgvVols.Rows.Add();
                dgvVols["clmVol", i].Value = drow["Name"].ToString().ToUpper();
                volId = Convert.ToInt32(drow["ID"]);
                dgvVols["clmVolID", i].Value = volId.ToString();
                hrsSet = false;
                if (clsVolHrs.findVolId(volId) == true)
                {
                    if (Convert.ToInt32(clsVolHrs.GetDataValue("VolID")) == volId)
                    {
                        volHrs = Convert.ToSingle(clsVolHrs.GetDataValue("NumVolHours"));
                        dgvVols["clmTimeIn", i].Value = clsVolHrs.GetDataValue("VolTimeIn");
                        dgvVols["clmTimeOut", i].Value = clsVolHrs.GetDataValue("VolTimeOut");
                        dgvVols["clmHrs", i].Value = volHrs.ToString("F");
                        dgvVols.Rows[i].Cells["clmHrs"].Style.BackColor = Color.Yellow;
                        dgvVols["clmVHID", i].Value = clsVolHrs.GetDataValue("ID").ToString();
                        hrsSet = true;
                        sumVolHrs += volHrs;
                        for (int j = 0; j < clsVolGroups.NbrGroups; j++)
                        {
                            clsVolGroups.GroupIndex = j;
                            for (int k = 0; k < clsVolGroups.VolunGroup.NbrVols; k++)
                            {
                                if (volId == clsVolGroups.VolunGroup.GetVolId(k))
                                {
                                    clsVolGroups.VolunGroup.VolHrs += volHrs;
                                    break;
                                }
                            }
                        }
                    }
                }
                else
                {
                    dgvVols.Rows[i].Visible = (chkShowWithData.Checked == false);
                }
                if (hrsSet == false)
                {
                    dgvVols["clmHrs", i].Value = 0;
                    dgvVols["clmHrs", i].Style.BackColor = Color.White;
                }
                i++;
            }
            tbSumHrs.Text = sumVolHrs.ToString("F");
            fillVolGroupHours(sumVolHrs);
        }

        private void fillVolGroupHours(Single sumVolHrs)
        {
            lvVolGroups.Items[0].SubItems[2].Text = sumVolHrs.ToString("F");
            foreach (ListViewItem item in lvVolGroups.Groups[constGroupName].Items)
            {
                int grpId = Convert.ToInt32(item.Tag);
                for (int j = 0; j < clsVolGroups.NbrGroups; j++)
                {
                    clsVolGroups.GroupIndex = j;
                    if (clsVolGroups.VolunGroup.GroupID == grpId)
                    {
                        item.SubItems[2].Text = clsVolGroups.VolunGroup.VolHrs.ToString("F");
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Initializes all the hours for each group to Zero
        /// </summary>
        private void initVolGroupValues()
        {
            for (int i = 0; i < lvVolGroups.Items.Count; i++)
            {
                lvVolGroups.Items[i].SubItems[2].Text = "0";
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void openConnection()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
        }

        private void closeConnection()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        private void rdoPrevMonth_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoPrevMonth.Checked == true)
                ShowDatesForPrevMonth();
        }

        private void rdoCurMonth_CheckedChanged(object sender, EventArgs e)
        {
            if(rdoCurMonth.Checked == true)
                ShowDatesForCurMonth();
        }

        private void ShowDatesForCurMonth()
        {
            loadDatesList(new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).ToShortDateString(),
                         new DateTime(DateTime.Today.Year, DateTime.Today.Month,DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month)).ToShortDateString());
        }

        private void ShowDatesForCurYear()
        {
            loadDatesList(new DateTime(DateTime.Today.Year, 1, 1).ToShortDateString(),
                         new DateTime(DateTime.Today.Year, 12, 31).ToShortDateString());
        }

        private void ShowDatesForPrevMonth()
        {
            int month;

            if (DateTime.Today.Month > 1)
                month = DateTime.Today.Month - 1;
            else
                month = 12;
            loadDatesList(new DateTime(DateTime.Today.Year, month, 1).ToShortDateString(),
                         new DateTime(DateTime.Today.Year, month, DateTime.DaysInMonth(DateTime.Today.Year, month)).ToShortDateString());
        }

        private void ShowDatesForPrevYear()
        {
            loadDatesList(new DateTime(DateTime.Today.Year - 1, 1, 1).ToShortDateString(), 
                        new DateTime(DateTime.Today.Year - 1, 12, 31).ToShortDateString());
        }


        private void rdoCurYear_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoCurYear.Checked == true)
                ShowDatesForCurYear();
        }

        private void rdoPrevYear_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoPrevYear.Checked == true)
                ShowDatesForPrevYear();
        }

        private void rdoAllDates_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoAllDates.Checked == true)
            {
                loadDatesList("1/1/1990","12/31/2099");
            }
        }

        private void lvDates_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lvDates.SelectedItems.Count > 0)
                {
                    tbWorkDate.Tag = lvDates.FocusedItem.Text;
                    tbWorkDate.Text = lvDates.FocusedItem.Text
                        + "  " + Convert.ToDateTime(lvDates.FocusedItem.Text).ToString("dddd");
                    loadVolsGrid();
                    dgvVols.Visible = true;
                }
            }
            catch(NullReferenceException ex) { }
        }

        private void dgvVols_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvVols.Columns[e.ColumnIndex].Name == "clmTimeIn" ||
                dgvVols.Columns[e.ColumnIndex].Name == "clmTimeOut")
            {
                dgvVols["clmHrs", e.RowIndex].Value = 0;

                dgvVols[e.ColumnIndex, e.RowIndex].Value = timeBeforeFormat;
                if (dgvVols[1, e.RowIndex].Value != null
                    && dgvVols[2, e.RowIndex].Value != null
                    && dgvVols[1, e.RowIndex].Value.ToString().Trim() != ""
                    && dgvVols[2, e.RowIndex].Value.ToString().Trim() != "")
                {
                    TimeSpan difference;
                    try
                    {
                        DateTime d = Convert.ToDateTime("1/1/2010 " + dgvVols["clmTimeIn", e.RowIndex].Value.ToString().Trim());
                        DateTime d2 = Convert.ToDateTime("1/1/2010 " + dgvVols["clmTimeOut", e.RowIndex].Value.ToString().Trim());

                        if (d2 > d)
                            difference = d2.TimeOfDay - d.TimeOfDay;
                        else if(d2.Hour < 12)
                            difference = d2.AddHours(12).TimeOfDay - d.TimeOfDay;
                        else
                            difference = d2.TimeOfDay - d.TimeOfDay;

                        dgvVols["clmHrs", e.RowIndex].Value = difference.TotalHours.ToString("F");
                        setVolHrsInDatabase(e);
                        bSkipTotal = true;
                    }
                    catch (Exception ex) { }
                }
                else if (dgvVols["clmHrs", e.RowIndex].Value.ToString() == "0"
                    && dgvVols["clmVHID", e.RowIndex].Value != null)
                    setVolHrsInDatabase(e);
            }
            else
            {
                setVolHrsInDatabase(e);
            }
        }

        private void setVolHrsInDatabase(DataGridViewCellEventArgs e)
        {
            try
            {
                int curVolID = 0;
                Single numHours = Convert.ToSingle(dgvVols["clmHrs", e.RowIndex].Value);
                dgvVols["clmHrs", e.RowIndex].Value = numHours.ToString("F");
                if (numHours == 0 && dgvVols["clmVHID", e.RowIndex].Value != null)
                {
                    clsVolHrs.delete(int.Parse(dgvVols["clmVHID", e.RowIndex].Value.ToString()));
                    dgvVols["clmVHID", e.RowIndex].Value = null;
                }
                else if (numHours > 0 && dgvVols["clmVHID", e.RowIndex].Value == null)
                {
                    curVolID = Convert.ToInt32(dgvVols["clmVolID", e.RowIndex].Value);
                    DataRow drow = clsVolHrs.DSet.Tables[0].NewRow();
                    drow["VolID"] = dgvVols["clmVolID", e.RowIndex].Value;
                    drow["TrxDate"] = DateTime.Parse(lvDates.FocusedItem.Text);
                    drow["NumVolunteers"] = 1;
                    drow["NumVolHours"] = numHours;
                    drow["VolTimeIn"] = dgvVols["clmTimeIn", e.RowIndex].Value;
                    drow["VolTimeOut"] = dgvVols["clmTimeOut", e.RowIndex].Value;
                    clsVolHrs.DSet.Tables[0].Rows.Add(drow);
                    if (clsVolHrs.insert())
                    {
                        clsVolHrs.findVolId(curVolID);
                        dgvVols["clmVHID", e.RowIndex].Value = clsVolHrs.ID;
                    }
                }
                else
                {
                    curVolID = Convert.ToInt32(dgvVols["clmVolID", e.RowIndex].Value);
                    clsVolHrs.findVolId(curVolID);
                    if (curVolID == clsVolHrs.VolId)
                    {
                        clsVolHrs.NumVolHours = numHours;
                        if (dgvVols["clmTimeIn", e.RowIndex].Value != null)
                            clsVolHrs.VolTimeIn = dgvVols["clmTimeIn", e.RowIndex].Value.ToString();
                        else
                            clsVolHrs.VolTimeIn = "";
                        if (dgvVols["clmTimeOut", e.RowIndex].Value != null)
                            clsVolHrs.VolTimeOut = dgvVols["clmTimeOut", e.RowIndex].Value.ToString();
                        else
                            clsVolHrs.VolTimeOut = "";
                        clsVolHrs.update();
                    }
                }
            }
            catch (InvalidCastException ex) { dgvVols[e.ColumnIndex, e.RowIndex].Value = 0; }
            catch (FormatException ex) { dgvVols[e.ColumnIndex, e.RowIndex].Value = 0; }
        }

        private void dgvVols_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvVols.Columns[e.ColumnIndex].Name == "clmHrs")
            {
                dgvVols[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.Yellow;
            }
        }

        private void VolunteerHoursForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            dgvVols.EndEdit();
        }

        private void btnDeleteTrxDate_Click(object sender, EventArgs e)
        {
            if (lvDates.FocusedItem != null)
            {
                if (MessageBox.Show("Are you sure you want to DELETE TrxDate " +
                     lvDates.FocusedItem.Text + "?", "Delete TrxDate",
                     MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    clsVolHrs.delete(DateTime.Parse(lvDates.FocusedItem.Text));
                    loadDatesList(sTrxDateMin, sTrxDateMax);
                }
            }
            else
            {
                MessageBox.Show("No Date Is Selected, Please Select A TrxDate And Try Again");
            }
        }

        private void btnAddTrxDate_Click(object sender, EventArgs e)
        {
            AddNewTrxDateForm frmAddTrxDate = new AddNewTrxDateForm();
            frmAddTrxDate.ShowDialog(this);
            if (frmAddTrxDate.TrxDate > new DateTime(2000, 1, 1))
            {
                DateTime TrxDate = frmAddTrxDate.TrxDate;
                clsVolHrs.openWhere("WHERE TrxDate = '" + TrxDate.ToString() + "'");

                if (clsVolHrs.RowCount == 0)
                {
                    DataRow drow = clsVolHrs.DSet.Tables[0].NewRow();
                    drow["TrxDate"] = TrxDate;
                    drow["VolId"] = 0;
                    drow["NumVolunteers"] = 0;
                    drow["NumVolHours"] = 0;
                    clsVolHrs.DSet.Tables[0].Rows.Add(drow);
                    clsVolHrs.insert();
                    loadDatesList(sTrxDateMin,sTrxDateMax);
                }
                tbWorkDate.Tag = TrxDate;
            }
            frmAddTrxDate.Close();
        }

        private void btnSaveHrs_Click(object sender, EventArgs e)
        {
            loadDatesList(sTrxDateMin,sTrxDateMax);
        }

        private void lvVolGroups_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (bAlreadyHere == false)
            {
                bAlreadyHere = true;
                groupVolIdList = "";

                if (e.Item.Group.Name == "lvGrpAll")
                {
                    if (e.Item.Checked == true)
                    {
                        for (int i = 1; i < lvVolGroups.Items.Count; i++)
                        {
                            try
                            {
                                lvVolGroups.Items[i].Checked = false;
                            }
                            catch { }
                        }
                    }
                }
                else
                {
                    foreach (ListViewItem lvItm in lvVolGroups.Groups[constGroupName].Items)
                    {
                        if (lvItm.Checked == true )
                        {
                            if (groupVolIdList == "")
                                lvVolGroups.Items[0].Checked = false;
                            else
                                groupVolIdList += ",";
                            for (int i = 0; i < clsVolGroups.NbrGroups; i++)
			                {
                                clsVolGroups.GroupIndex = i;
                                if (clsVolGroups.VolunGroup.GroupID == Convert.ToInt32(lvItm.Tag))
                                    groupVolIdList += clsVolGroups.VolunGroup.GetVolIdString;
			                }
                            
                        }
                    }
                }
                loadVolsGrid();
                bAlreadyHere = false;
            }
        }

        private void VolunteerHoursForm_Shown(object sender, EventArgs e)
        {
            bAlreadyHere = false;
        }

         private void dgvVols_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dgvVols.Columns[e.ColumnIndex].Name == "clmTimeIn" ||
                dgvVols.Columns[e.ColumnIndex].Name == "clmTimeOut")
            {
                timeBeforeFormat = "";
                if (dgvVols[e.ColumnIndex, e.RowIndex].
                    GetEditedFormattedValue(e.RowIndex, DataGridViewDataErrorContexts.Formatting)
                    .ToString().Trim() != "")
                {
                    timeBeforeFormat = e.FormattedValue.ToString();

                    try
                    {
                        if (!timeBeforeFormat.Contains(":"))
                        {
                            timeBeforeFormat = timeBeforeFormat.Insert(timeBeforeFormat.Length - 2, ":");
                            dgvVols[e.ColumnIndex, e.RowIndex].Value = timeBeforeFormat;
                        }

                        string dateTime = "1/1/2010 " + timeBeforeFormat;

                        Convert.ToDateTime(dateTime);
                    }
                    catch { MessageBox.Show("Time Not In Correct Format.  Please Enter Time hh:mm Or hhmm Or Blank"); e.Cancel = true; }
                }
            }
        }

        private void chkShowWithData_CheckedChanged(object sender, EventArgs e)
        {
            if (dgvVols.Visible==true)
                loadVolsGrid();
        }
    }
}
