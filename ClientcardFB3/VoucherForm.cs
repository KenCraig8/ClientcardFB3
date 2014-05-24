using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace ClientcardFB3
{
    public partial class VoucherForm : Form
    {
        Household clsHH;
        VoucherLog clsVoucher;
        int curHHID = 0;
        SqlCommand command;
        SqlDataReader reader;
        SqlConnection conn = new SqlConnection(CCFBGlobal.connectionString);
        
        List<TextBox> tbFamData = new List<TextBox>();
        List<CheckBox> chkList = new List<CheckBox>();

        bool inEditMode = false;

        string comments = "";
        decimal amount = -1m;
        int rowIndex = -1;
        bool loadingData = true;

        public VoucherForm(Client clsClientIn)
        {
            InitializeComponent();
            clsHH = clsClientIn.clsHH;
            dtpTrxDate.Value = DateTime.Today;
            initForm();
        }

        public VoucherForm(Client clsClientIn, DateTime EditVoucherDate)
        {
            InitializeComponent();
            clsHH = clsClientIn.clsHH;
            dtpTrxDate.Value = EditVoucherDate;
            initForm();
        }

        private void cboVHistoryPeriod_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //Registry.SetValue(CCFBGlobal.registryKeyCurrentUser, constVoucherLogPeriod, cboTrxLogPeriod.SelectedIndex);
            getVoucherLogForPeriod();
        }

        public void initForm()
        {
            loadingData = true;
            curHHID = clsHH.ID;
            clsVoucher = new VoucherLog(conn);
            loadingData = true;
            dtpTrxDate.Value = DateTime.Today;
            pnlEditVLog.Dock = DockStyle.Fill;
            pnlSelect.Dock = DockStyle.Fill;
            setEditMode(false);
            
            CCFBGlobal.InitCombo(cboClientType, CCFBGlobal.parmTbl_Client);
            CCFBGlobal.dtPopulateCombo(cboNewVoucherItem, "SELECT * FROM VoucherItems ORDER BY UID", "Description", "UID", "No Selection", conn);

            foreach (TextBox tb in pnlEditVLog.Controls.OfType<TextBox>())
            {
                tb.Text = "";
                if (tb.Tag != null)
                {
                    if (tb.Tag.ToString() != "")
                    {
                        tb.Text = clsHH.GetDataString(tb.Tag.ToString());
                    }
                    tbFamData.Add(tb);
                    tb.LostFocus += new System.EventHandler(this.tbFamData_LostFocus);
                }
            }
            foreach (CheckBox chk in pnlEditVLog.Controls.OfType<CheckBox>())
            {
                chk.Checked = Convert.ToBoolean(clsHH.GetDataValue(chk.Tag.ToString())); 
                chkList.Add(chk);
            }
            tbClient.Text = curHHID.ToString() + Environment.NewLine + clsHH.Name;
            cboClientType.SelectedValue = clsHH.ClientType.ToString();

            //fillClientInfo();
            loadVItmsList();
            //loadList();
            loadingData = false;
        }

        public void CreateNewVoucher()
        {
            clsVoucher.open(0);

            clsVoucher.CreateNewRow();
            clsVoucher.Infants = clsHH.Infants;
            clsVoucher.Youth = clsHH.Youth;
            clsVoucher.Teens = clsHH.Teens;
            clsVoucher.Eighteen = clsHH.Eighteens;
            clsVoucher.Adults = clsHH.Adults;
            clsVoucher.Seniors = clsHH.Seniors;
            clsVoucher.TotalFamily = clsHH.TotalFamily;
            clsVoucher.HouseholdID = curHHID;
            clsVoucher.Homeless = clsHH.Homeless;
            clsVoucher.Disabled = clsHH.Disabled;
            clsVoucher.Zipcode = clsHH.Zipcode;
            clsVoucher.InCityLimits = clsHH.InCityLimits;
            clsVoucher.Created = DateTime.Now;
            clsVoucher.CreatedBy = CCFBGlobal.dbUserName;
            clsVoucher.ClientType = clsHH.ClientType;
            fillEditFields();
        }

        public void fillEditFields()
        {
            cboClientType.SelectedValue = clsVoucher.ClientType.ToString();
            dtpTrxDate.Value = clsVoucher.TrxDate;
            foreach (TextBox tb in tbFamData)
            {
                tb.Text = clsVoucher.GetDataValue(tb.Tag.ToString()).ToString();
            }
            foreach (CheckBox chk in chkList)
            {
                chk.Checked = (bool)clsVoucher.GetDataValue(chk.Tag.ToString());
            }
        }

        private void loadVItmsList()
        {
            ListViewItem lvItem;
            string tmp = "";
            decimal maxamt=0;
            decimal amtrcvd = 0;
            decimal defaultamt = 0;
            try
            {
                loadingData = true;
                Application.UseWaitCursor = true;
                lvwVItms.Items.Clear();
                Application.DoEvents();
                DateTime start = new DateTime(dtpTrxDate.Value.Year, 1, 1);
                conn.Open();
                command = new SqlCommand("select vi.uid, vi.Description"
                    + ",(SELECT SUM(amount) FROM VoucherLog WHERE TrxDate between '" + start + "' and '" + dtpTrxDate.Value.AddDays(-1) + "' "
                    + " and HouseholdID = " + curHHID.ToString() + " and VoucherItemId = vi.Uid) Cum "
                    + ", vi.maxAmount, vi.DefaultAmount "
                    + ", CASE WHEN vt.ShortName IS NULL THEN '--' ELSE vt.ShortName END vtype "
                    + ", vi.VoucherType "
                    + " FROM VoucherItems vi "
                    + " LEFT JOIN parm_VoucherType vt ON vi.VoucherType = vt.ID "
                    + "WHERE vi.Inactive = 0 Order BY vtype, Description", conn);
                //command = new SqlCommand("Select * From VoucherItems Order By Description ASC", conn);
                reader = command.ExecuteReader();
                decimal leftOver = 0;
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        amtrcvd = 0;
                        if (reader.GetValue(2) != DBNull.Value)
                             amtrcvd = Convert.ToDecimal(reader.GetValue(2));

                        maxamt = 0;
                        if (reader.GetValue(3) != DBNull.Value)
                            maxamt = Convert.ToDecimal(reader.GetValue(3));

                        defaultamt = 0;
                        if (reader.GetValue(4) != DBNull.Value)
                            defaultamt = Convert.ToDecimal(reader.GetValue(4));

                        lvItem = new ListViewItem();
                        lvItem.Tag = reader.GetValue(0);
                        lvItem.Text = reader.GetString(1);  //Description

                        if (amtrcvd == 0)
                        { tmp = "--"; }
                        else if (reader.GetInt32(6)==0)
                        { tmp = amtrcvd.ToString("C"); }
                        else
                        { tmp = Convert.ToInt32(amtrcvd).ToString(); }
                        lvItem.SubItems.Add(tmp);           //Amt Received

                        lvItem.SubItems.Add(reader.GetString(5));   //Voucher Type

                        if (maxamt != 0)
                        {
                            leftOver = maxamt - amtrcvd;
                            tmp = leftOver.ToString("C");
                        }
                        else
                        {
                            leftOver = 0;
                            tmp = "--";
                        }
                        lvItem.SubItems.Add(tmp);           //Amt Available
                        
                        lvItem.SubItems.Add(maxamt.ToString("C"));  //MAX Amt
                        
                        if (defaultamt != 0 )
                        {
                            if (defaultamt > leftOver)
                            {
                                tmp = leftOver.ToString("C");
                            }
                            else
                            {
                                tmp = defaultamt.ToString("C");
                            }
                        }
                        else
                        {
                            tmp = "0";
                        }
                        lvItem.SubItems.Add(tmp);           //Default Amt
                        lvwVItms.Items.Add(lvItem);
                    }
                }
                conn.Close();
                Application.UseWaitCursor = false;
                loadingData = false;
            }
            catch (SqlException ex)
            {
                CCFBGlobal.appendErrorToErrorReport("Command Text= " + command.CommandText,
                    ex.GetBaseException().ToString());
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
        }

        //private void loadList()
        //{
        //    try
        //    {
        //        conn.Open();
        //        command = new SqlCommand("Select v.VoucherItemID, v.Amount, v.Notes, V.trxID "
        //            + "From VoucherLog V Left Join Household H on "
        //            + "V.HouseholdID=H.ID Where H.ID=" + curHHID.ToString()
        //            + " And V.trxDate='" + dtpTrxDate.Value.ToShortDateString() + "'", conn);
        //        reader = command.ExecuteReader();

        //        if(reader.HasRows)
        //        {
        //            while(reader.Read())
        //            {
        //                int id = reader.GetInt32(0);
        //                for (int i = 0; i < lvwVItms.Items.Count; i++)
        //                {
        //                    if (Convert.ToInt32(gridVouchers["clmDesc", i].Tag) == id)
        //                    {
        //                        gridVouchers["clmAmountGiven", i].Value = reader.GetDecimal(1).ToString("C");
        //                        gridVouchers["clmComments", i].Value = reader.GetValue(2);
        //                        lvwVItms.Items[i].Tag = reader.GetValue(3);
        //                        gridVouchers["clmEnable", i].Value = CheckState.Checked;
 
        //                        if (gridVouchers["clmComments", i].Value == null)
        //                            gridVouchers["clmComments", i].Value = "";
        //                        //gridVouchers["clmAmountGiven", i].ReadOnly = true;
        //                        //gridVouchers["clmComments", i].ReadOnly = true;
        //                        break;
        //                    }
        //                }
        //            }
        //        }


        //        conn.Close();
        //    }
        //    catch(SqlException ex) 
        //    {
        //        CCFBGlobal.appendErrorToErrorReport("Command Text= " + command.CommandText, 
        //            ex.GetBaseException().ToString());
        //    }
           
        //}

        private void tbFamData_LostFocus(object sender, EventArgs e)
        {
            TextBox tbTmp = (TextBox)sender; //Get the correct textbox
            if (loadingData == false)
            {
                if (inEditMode == true)
                {
                    //If current value does not = value of textbox
                    tbTotalFam.Text = (Int32.Parse(tbYouth.Text) + Int32.Parse(tbAdults.Text) +
                        Int32.Parse(tbTeens.Text) + Int32.Parse(tbInfants.Text) + Int32.Parse(tbSeniors.Text)).ToString();
                }
                else
                {
                    //If not in edit mode, reset the text with value from class
                    tbTmp.Text = clsHH.GetDataValue(tbTmp.Tag.ToString()).ToString();
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //private void gridVouchers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (loadingData == false && e.ColumnIndex == 3 &&
        //       lvwVItms.Items[e.RowIndex].Cells[e.ColumnIndex].GetEditedFormattedValue(e.RowIndex, DataGridViewDataErrorContexts.Display).ToString() == "Checked")
        //    {
        //        decimal testValue = 0;
        //        //if (gridVouchers["clmAmntRcvd", e.RowIndex].Value.ToString() != "--")
        //        testValue = cleanDisplayValue(gridVouchers["clmAmntAvail", e.RowIndex].Value.ToString());
        //        if (testValue > 0)
        //        {
        //            if (cleanDisplayValue(gridVouchers["clmDefaultAmount", e.RowIndex].Value.ToString()) < cleanDisplayValue(gridVouchers["clmAmntAvail", e.RowIndex].Value.ToString()))
        //            {
        //                gridVouchers["clmAmountGiven", e.RowIndex].Value = gridVouchers["clmDefaultAmount", e.RowIndex].Value;
        //            }
        //            else
        //            {
        //                gridVouchers["clmAmountGiven", e.RowIndex].Value = gridVouchers["clmAmntAvail", e.RowIndex].Value;
        //            }
        //        }
        //        else
        //            gridVouchers["clmAmountGiven", e.RowIndex].Value = "0.00";
        //        gridVouchers["clmAmntRcvd", e.RowIndex].ReadOnly = false;
        //        gridVouchers["clmComments", e.RowIndex].ReadOnly = false;
        //    }
        //}

        //private void gridVouchers_RowEnter(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (loadingData == false)
        //    {
        //        if (gridVouchers["clmAmountGiven", e.RowIndex].Value.ToString() != "")
        //        {
        //            comments = gridVouchers["clmComments", e.RowIndex].Value.ToString();
        //            amount = cleanDisplayValue(gridVouchers["clmAmountGiven", e.RowIndex].Value.ToString());
        //            rowIndex = e.RowIndex;
        //        }
        //    }
        //}

        //private void gridVouchers_RowLeave(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (gridVouchers["clmComments", e.RowIndex].GetEditedFormattedValue(
        //        e.RowIndex, DataGridViewDataErrorContexts.Commit).ToString() != comments
        //        || amount != cleanDisplayValue(gridVouchers["clmAmountGiven", e.RowIndex].Value.ToString()))
        //    {
        //        comments = gridVouchers["clmComments", e.RowIndex].GetEditedFormattedValue(
        //            e.RowIndex, DataGridViewDataErrorContexts.Commit).ToString();
        //        amount = cleanDisplayValue(gridVouchers["clmAmountGiven", e.RowIndex].GetEditedFormattedValue(
        //            e.RowIndex, DataGridViewDataErrorContexts.Commit).ToString());

        //        if (lvwVItms.Items[e.RowIndex].Tag != null && amount > 0)
        //            updateLog();
        //        else if (lvwVItms.Items[e.RowIndex].Tag != null)
        //        {
        //            delete();
        //            timer1.Start();
        //        }
        //        else
        //            insertIntoLog();
        //    }
        //}

        private void delete()
        {
            try
            {
                conn.Open();
                command = new SqlCommand("Delete From VoucherLog Where TrxID=" + lvwVItms.Items[rowIndex].Tag, conn);
                command.ExecuteNonQuery();
                conn.Close();
            }
            catch(System.Exception ex) 
            {
                CCFBGlobal.appendErrorToErrorReport("Command Text = " + command.CommandText, ex.GetBaseException().ToString());
            }
        }

        private void updateLog()
        {
            try
            {
                //conn.Open();
                //command = new SqlCommand("Update VoucherLog Set TrxDate = '" + dtpTrxDate.Value.ToShortDateString() + "', "
                //+ "HouseholdID = " + curHHID.ToString() + ", Amount = " + amount.ToString() + "," 
                //+ "Notes = '" + comments + "', VoucherItemID=" + gridVouchers["clmDesc", rowIndex].Tag.ToString() + " ,"
                //+ "Infants= " + tbInfants.Text + ", Youth= " + tbYouth.Text + ", Teens=" + tbTeens.Text + ", "
                //+ "Eighteen=0, Adults=" + tbAdults.Text + ", Seniors=" + tbSeniors.Text + ", TotalFamily=" + tbTotalFam.Text
                //+ ", Disabled=" + tbDisabled.Text + ", Homeless = " + Convert.ToInt32(chkHomeless.Checked).ToString() 
                //+ ", Modified = '" + DateTime.Now.ToString() + "', ModifiedBy='" + CCFBGlobal.dbUserName + "'"
                //+ " Where trxID=" + lvwVItms.Items[rowIndex].Tag.ToString(), conn);
                //command.ExecuteNonQuery();
                //conn.Close();
            }
            catch (System.Exception ex)
            {
                CCFBGlobal.appendErrorToErrorReport("Command Text = " + command.CommandText,
                    ex.GetBaseException().ToString());
                conn.Close();
            }
        }

        private void insertIntoLog()
        {
            try
            {
                //conn.Open();
                //command = new SqlCommand("Insert Into VoucherLog (TrxDate, HouseholdID, Amount, Notes, VoucherItemID,"
                //    + "Infants, Youth, Teens, Eighteen, Adults, Seniors, TotalFamily, Disabled, Homeless, Transient, Created, CreatedBy"
                //    + ", FiscalFirstTime, CalFirstTime, MonthFirstTime) "
                //    + "Values('" + dtpTrxDate.Value.ToShortDateString() + "', " + curHHID.ToString() + ", "
                //    + amount.ToString() + ", '" + comments + "', " + gridVouchers["clmDesc", rowIndex].Tag.ToString() 
                //    + ", " + tbInfants.Text + ", " + tbYouth.Text + ", " + tbTeens.Text + ", " + "0," + tbAdults.Text + ", " 
                //    + tbSeniors.Text + ", " + tbTotalFam.Text + ", " + tbDisabled.Text 
                //    + ", " + Convert.ToInt32( chkHomeless.Checked).ToString() + "," + cboClientType.SelectedValue 
                //    + ",'" + DateTime.Now.ToString()  + "','" + CCFBGlobal.dbUserName + "',0,0,0)"
                //    , conn);
                //command.ExecuteNonQuery();
                //conn.Close();
                //loadVItmsList();
            }
            catch(System.Exception ex) 
            {
                CCFBGlobal.appendErrorToErrorReport("Command Text = " + command.CommandText,
                    ex.GetBaseException().ToString());
                conn.Close();
            }
        }

        private void dtpTrxDate_ValueChanged(object sender, EventArgs e)
        {
            if  (loadingData == false)
            {
                loadVItmsList();
                //loadDescriptions();
                //loadList();
            }
        }

        private void btnPost_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                command = new SqlCommand("Update VoucherLog Set Infants= " + tbInfants.Text + ", Youth= " 
                    + tbYouth.Text + ", Teens=" + tbTeens.Text + ", " + "Eighteen=0, Adults=" + tbAdults.Text 
                    + ", Seniors=" + tbSeniors.Text + ", TotalFamily=" + tbTotalFam.Text + ", Disabled=" 
                    + tbDisabled.Text + " Where TrxDate = '" + dtpTrxDate.Value.ToShortDateString() + "' And "
                    + "HouseholdID = " + curHHID.ToString(), conn);
                command.ExecuteNonQuery();
                conn.Close();
            }
            catch (System.Exception ex)
            {
                CCFBGlobal.appendErrorToErrorReport("Command Text = " + command.CommandText,
                    ex.GetBaseException().ToString());
            }
        }

        private void dtpTrxDate_Validated(object sender, EventArgs e)
        {
            if (loadingData == false)
            {
                //loadDescriptions();
                //loadList();
            }
        }

        private decimal cleanDisplayValue(string testValue)
        {
            if (testValue.Trim() != "")
            { return Convert.ToDecimal(testValue.Replace("$", "")); }
            return Convert.ToDecimal("0");
        }

        private void getVoucherLogForPeriod()
        {
            string newWhereClause = "";
            switch (cboVHistoryPeriod.SelectedIndex)
            {
                case 0: //Current Month
                        int month = DateTime.Now.Month;
                        DateTime from = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                        DateTime to = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
                        newWhereClause = " AND TrxDate Between '" + from.ToString() + "' AND '" + to.ToString() + "'";
                        break;
                case 1: //Last 90 Days
                        newWhereClause = " And TrxDate Between '" + new DateTime(
                            DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day).AddDays(-90).ToString()
                        + "' And '" + DateTime.Today.ToString() + "'";
                        break;
                case 2: //Current Calendar Year
                        newWhereClause = " And TrxDate Between '" + new DateTime(DateTime.Today.Year, 1, 1).ToString()
                            + "' And '" + new DateTime(DateTime.Today.Year, 12, 31).ToString() + "'";
                        break;
                case 3: //Current Fiscal Year
                        newWhereClause = " And TrxDate Between '" + CCFBGlobal.CurrentFiscalStartDate().ToString() + "' And '"
                         + CCFBGlobal.CurrentFiscalEndDate().ToString() + "'";
                        break;
                case 4: //Previous Calendar Year
                        newWhereClause = " And TrxDate Between '" + new DateTime(DateTime.Today.Year - 1, 1, 1).ToString() + "' ANd '"
                            + new DateTime(DateTime.Today.Year - 1, 12, 31).ToString() + "'";
                        break;
                case 5: //Previous Fiscal Year
                        newWhereClause = " And TrxDate Between '" + CCFBGlobal.PreviousFiscalStartDate().ToString() + "' And '"
                            + CCFBGlobal.PreviousFiscalEndDate().ToString() + "'";
                        break;
                default://ALL
                        newWhereClause = "";
                        break;
            }
            loadVoucherLog(newWhereClause);
        }

        /// <summary>
        /// Opens the VoucherLog for the Client, uses the SQLDataReader 
        /// to read the rows and Loads them into ListView
        /// </summary>
        public void loadVoucherLog(string whereClause)
        {
            btnEditVoucherLog.Tag = "";
            string queryText = "Select Convert(varchar(10),v.trxDate,101), vi.Description, v.Amount, v.Notes, v.TrxID"
                    + ", CASE WHEN vt.ShortName IS NULL THEN '--' ELSE vt.ShortName END vtype "
                    + " FROM VoucherLog v Left Join VoucherItems vi on vi.uid = v.VoucherItemID "
                    + " LEFT JOIN parm_VoucherType vt ON vi.VoucherType = vt.ID "
                    + "WHERE v.HouseholdID=" + curHHID.ToString()
                    + " " + whereClause;
            fillHistoryList(lvByDate, queryText, "TrxDate DESC, vtype, Description",0);
            fillHistoryList(lvByVType, queryText, "vtype, Description, TrxDate DESC",1);
            if (lvByDate.Items.Count > 0)
            {
                btnEditVoucherLog.Visible = true;
                lvByDate.Items[0].Selected = true;
                btnEditVoucherLog.Tag = lvByDate.Items[0].SubItems[1].Text;
            }

        }

        private void fillHistoryList(ListView lvw, string sqlText, string mysortorder, int groupindx)
        {
            ListViewItem lvi2 = null;
            SqlDataReader reader = null;
            lvw.Items.Clear();
            lvw.Groups.Clear();
            string curgroup = "";
            ListViewGroup lvgrp = new ListViewGroup("");
            try
            {
                //The order of the select statements directly corresponds to what shows on the ListView
                command = new SqlCommand(sqlText + " ORDER BY " + mysortorder, conn);
                openConnection();
                reader = command.ExecuteReader();

                int count = 0;
                while (reader.Read())
                {
                    count++;
                    lvi2 = new ListViewItem(count.ToString());
                    lvi2.Tag = reader.GetInt32(4); 
                    if (curgroup != reader.GetString(groupindx) || lvgrp.Header == "")
                    {
                        curgroup = reader.GetString(groupindx);
                        lvgrp = lvw.Groups.Add(curgroup, curgroup);
                    }
                    lvi2.Group = lvgrp;
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        if (i == 2)
                        {
                            if (reader.GetString(5) == "$")
                            {
                                lvi2.SubItems.Add(reader.GetDecimal(i).ToString("C"));
                            }
                            else
                            {
                                lvi2.SubItems.Add(Convert.ToInt64(reader.GetValue(i)).ToString());
                            }
                        }
                        else
                            lvi2.SubItems.Add(reader.GetValue(i).ToString());
                    }
                    lvw.Items.Add(lvi2);
                }
                closeConnection();
            }
            catch (SqlException ex)
            {
                closeConnection();
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString());
            }
            catch (Exception ex)
            {
                closeConnection();
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString());
            }
        }

        /// <summary>
        /// Opens a connection to the Database
        /// </summary>
        private void openConnection()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
        }
        /// <summary>
        /// Closes a connection to the Database
        /// </summary>
        private void closeConnection()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        private void tabControl1_TabIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnEditVoucherLog_Click(object sender, EventArgs e)
        {
            int trxID = Convert.ToInt32(btnEditVoucherLog.Tag);
            clsVoucher.open(trxID);
            fillEditFields();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            setEditMode(true);

        }

        private void setEditMode(bool editing)
        {
            pnlSelect.Visible = !editing;
            pnlEditVLog.Visible = editing;
            inEditMode = editing;
            if (editing == true)
            {
                spltcRight.SplitterDistance = 254;
            }
            else
            {
                spltcRight.SplitterDistance = 150;
            }
        }

        private void btnPost_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("put save in here");
        }

        private void lvwVItms_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (lvwVItms.SelectedItems.Count > 0)
            {
                cboNewVoucherItem.SelectedValue = lvwVItms.SelectedItems[0].Tag.ToString();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            setEditMode(false);
        }

        private void lvByDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (loadingData == false && lvByDate.SelectedItems.Count > 0)
            {
                btnEditVoucherLog.Tag = lvByDate.SelectedItems[0].Tag;
                btnEditVoucherLog.Text = "Edit " + btnEditVoucherLog.Tag.ToString();
            }
        }
    }
}
