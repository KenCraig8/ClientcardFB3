using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Win32;

namespace ClientcardFB3
{
    public partial class VoucherShortForm : Form
    {
        Household clsHH;
        VoucherLog clsVoucher;
        VoucherLog clsVoucherExisitsTest;
        VoucherItems clsVItems;
        int curHHID = 0;
        DateTime dtDefault = DateTime.Today;
        SqlCommand command;
        SqlDataReader reader;
        SqlConnection SQLConn = new SqlConnection(CCFBGlobal.connectionString);

        bool loadingData = true;
        bool inEditMode = false;

        string oriText = "";
        bool oriBool = false;
        decimal oriAmt = 0;

        public VoucherShortForm(Client clsClientIn)
        {
            InitializeComponent();
            clsHH = clsClientIn.clsHH;
            curHHID = clsHH.ID;
            //tbClient.Text = curHHID.ToString() + Environment.NewLine + clsHH.Name;
            mnuHHName.Text = "[" + curHHID.ToString() + "] " + clsHH.Name;
            clsVItems = new VoucherItems(CCFBGlobal.connectionString);
            clsVoucher = new VoucherLog(SQLConn);
            clsVoucherExisitsTest = new VoucherLog(SQLConn);
            CCFBGlobal.InitCombo(cboHHCat, CCFBGlobal.parmTbl_Client);
            dtpTrxDate.Value = dtDefault;
            pnlEditVLog.Visible = false;
            pnlBtns.Visible = true;
            pnlBtns.Dock = DockStyle.Fill;
            loadVItmsList();
            cboVHistoryPeriod.SelectedIndex = 2;
            cboVHistoryPeriod_SelectionChangeCommitted(cboVHistoryPeriod, EventArgs.Empty);
        }
 
        private void loadVItmsList()
        {
            ListViewItem lvItem;
            int iType = 0;
            int i = 1;
            int iCnt = 0;
            string tmp = "";
            decimal maxamt = 0;
            decimal amtrcvd = 0;
            decimal defaultamt = 0;

            try
            {
                loadingData = true;
                Application.UseWaitCursor = true;
                lvwVItms.Items.Clear();
                Application.DoEvents();
                DateTime dtStart = new DateTime(DateTime.Today.Year, 1, 1);
                DateTime dtEnd = DateTime.Today;

                SQLConn.Open();
                command = new SqlCommand("select vi.uid, vi.Description"
                    + ",(SELECT SUM(amount) FROM VoucherLog WHERE TrxDate between '" + dtStart.ToShortDateString() + "' and '" + dtEnd.ToShortDateString() + "' "
                    + " and HouseholdID = " + curHHID.ToString() + " and VoucherItemId = vi.Uid) Cum "
                    + ", vi.maxAmount, vi.DefaultAmount "
                    + ", CASE WHEN vt.ShortName IS NULL THEN '--' ELSE vt.ShortName END vtype "
                    + ", vi.VoucherType, vi.DisplayCol, vi.DisplayRow, vi.DisplayBackColor "
                    + ",(SELECT Count(*) FROM VoucherLog WHERE TrxDate between '" + dtStart.ToShortDateString() + "' and '" + dtEnd.ToShortDateString() + "' "
                    + " and HouseholdID = " + curHHID.ToString() + " and VoucherItemId = vi.Uid) NbrSvcs "
                    + " FROM VoucherItems vi "
                    + " LEFT JOIN parm_VoucherType vt ON vi.VoucherType = vt.ID "
                    + "WHERE vi.Inactive = 0 Order BY vtype, Description", SQLConn);
                //command = new SqlCommand("Select * From VoucherItems Order By Description ASC", conn);
                reader = command.ExecuteReader();
                decimal leftOver = 0;
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        i++;
                        iType = reader.GetInt32(6);
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
                        else if (iType == 0)
                        { tmp = amtrcvd.ToString("C"); }
                        else
                        { tmp = Convert.ToInt32(amtrcvd).ToString(); }
                        iCnt = 0;
                        if (reader.GetValue(10) != DBNull.Value)
                            iCnt = Convert.ToInt32(reader.GetValue(10));
                        if (iCnt > 0)
                        {
                            if (iType == 2)
                            { tmp = "(" + tmp + ")"; }
                            else
                            { tmp += " (" + iCnt.ToString() + ")"; }
                        }
                        lvItem.SubItems.Add(tmp);           //Amt Received

                        lvItem.SubItems.Add(reader.GetString(5));   //Voucher Type

                        if (maxamt != 0)
                        {
                            leftOver = maxamt - amtrcvd;
                            tmp = fmtAmt(iType, leftOver);
                        }
                        else
                        {
                            leftOver = 0;
                            tmp = "--";
                        }
                        lvItem.SubItems.Add(tmp);           //Amt Available

                        lvItem.SubItems.Add(maxamt.ToString("C"));  //MAX Amt

                        if (defaultamt != 0)
                        {
                            if (defaultamt > leftOver)
                            {
                                tmp = fmtAmt(iType, leftOver);
                            }
                            else
                            {
                                tmp = fmtAmt(iType, defaultamt);
                            }
                        }
                        else
                        {
                            tmp = "0";
                        }
                        lvItem.SubItems.Add(tmp);           //Default Amt
                        lvwVItms.Items.Add(lvItem);
                        if (pnlBtns.Controls.Count < i)
                        {
                            addVoucherButton(iType, lvItem, reader.GetInt32(8), reader.GetInt32(7));
                        }
                    }
                }
                SQLConn.Close();
                Application.UseWaitCursor = false;
                loadingData = false;
            }
            catch (SqlException ex)
            {
                CCFBGlobal.appendErrorToErrorReport("Command Text= " + command.CommandText,
                    ex.GetBaseException().ToString());
                if (SQLConn.State == System.Data.ConnectionState.Open)
                    SQLConn.Close();
            }
        }

        private string fmtAmt(int vType, decimal vAmt)
        {
            if (vType == 0)
            {
                return vAmt.ToString("C");
            }
            return Convert.ToInt32(vAmt).ToString();
        }

        private void addVoucherButton(int iType, ListViewItem lvi, int iRow, int iCol)
        {
            Button btn = new Button();
            btn.Height = btnTemp.Height;
            btn.Width = btnTemp.Width;
            btn.Font = btnTemp.Font;
            switch (iType)
            {
                case 0:
                    btn.BackColor = Color.LightGreen;
                    break;
                case 1:
                    btn.BackColor = Color.LightYellow;
                    break;
                case 2:
                    btn.BackColor = Color.LightCyan;
                    break;
                default:
                    btn.BackColor = Color.Gainsboro;
                    break;
            }
            btn.Click += new EventHandler(btnVoucher_Click);
            btn.Text = lvi.Text;
            btn.Tag = lvi.Tag;
            int x = (iCol * (btnTemp.Width + 8)) + 4;
            int y = (iRow * (btnTemp.Height + 5)) + 4;
            btn.Location = new Point(x, y);
            pnlBtns.Controls.Add(btn);
        }

        private void btnVoucher_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            setEditMode(true);
            CreateNewVoucher(Convert.ToInt32(btn.Tag), btn.Text);

        }
        private void setEditMode(bool editing)
        {
            pnlBtns.Visible = !editing;
            pnlEditVLog.Visible = editing;
            inEditMode = editing;
        }


        private void rdo_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdo = (RadioButton)sender;
            setDisplay(rdo.Tag.ToString());
        }

        private void mnuShow_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem mitm = (ToolStripMenuItem)sender;
            setDisplay(mitm.Tag.ToString());
        }

        private void setDisplay(string sCode)
        {
            switch (sCode)
            {
                case "1":
                    spltrCtrls.Panel1Collapsed = false;
                    spltrCtrls.Panel2Collapsed = true;
                    break;
                case "2":
                    spltrCtrls.Panel1Collapsed = true;
                    spltrCtrls.Panel2Collapsed = false;
                    break;
                default:
                    spltrCtrls.Panel1Collapsed = false;
                    spltrCtrls.Panel2Collapsed = false;
                    break;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            setEditMode(false);
        }

        public void CreateNewVoucher(int newItemId, string itemDescr)
        {
            clsVItems.open(newItemId);
            clsVoucher.open(0);

            clsVoucher.CreateNewRow();
            clsVoucher.TrxDate = dtDefault;
            clsVoucher.VoucherItemID = newItemId;
            clsVoucher.Amount = clsVItems.DefaultAmount;
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
            clsVoucher.SpecialDiet = clsHH.SpecialDiet;

            fillEditFields(itemDescr);
        }

        public void fillEditFields(string itemDescr)
        {
            btnPost.Focus();
            //tbVoucherType.Text = clsVoucher.ClientType.ToString();
            tbVoucherId.Text = clsVoucher.TrxId.ToString();
            tbTrxDate.Text = clsVoucher.TrxDate.ToLongDateString(); // clsVoucher.TrxDate.ToShortDateString();
            if (clsVoucher.TrxId > 0)
            {
                dtpTrxDate.Value = clsVoucher.TrxDate;
            }
            foreach (TextBox tb in pnlDemographics.Controls.OfType<TextBox>())
            {
                tb.Text = clsVoucher.GetDataValue(tb.Tag.ToString()).ToString();
            }
            foreach (CheckBox chk in pnlDemographics.Controls.OfType<CheckBox>())
            {
                chk.Checked = (bool)clsVoucher.GetDataValue(chk.Tag.ToString());
            }
            
            tbNotes.Text = clsVoucher.Notes;
            tbVoucherType.Text = itemDescr;
            cboHHCat.SelectedValue = clsVoucher.ClientType.ToString();
            lblAmt.Text = CCFBGlobal.LongNameFromId(CCFBGlobal.parmTbl_VoucherType, clsVItems.VoucherType);
            oriAmt = clsVoucher.Amount;
            switch (clsVItems.VoucherType)
            {
                case 0:
                    tbQty.Text = clsVoucher.Amount.ToString();
                    tbQty.Visible = true;
                    break;
                case 1:
                    tbQty.Text = Convert.ToInt32(clsVoucher.Amount).ToString();
                    tbQty.Visible = true;
                    break;
                case 2:
                    tbQty.Text = Convert.ToInt32(clsVoucher.Amount).ToString();
                    tbQty.Visible = false;
                    break;
                default:
                    tbQty.Text = clsVoucher.Amount.ToString();
                    tbQty.Visible = true;
                    break;
            }
        }

        private void btnPost_Click(object sender, EventArgs e)
        {
            if (clsVoucher.TrxId == 0)
            {
                clsVoucher.AddNewRow();
            }
            clsVoucher.update();
            setEditMode(false);
            if (CCFBPrefs.EnableClientReceipt == true)
            {
                switch (CCFBPrefs.ServiceMenuType)
                {
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        RptVoucherOrder01 clsOrder01 = new RptVoucherOrder01(clsHH, clsVoucher);
                        clsOrder01.createReport(CCFBGlobal.pathTemplates + "VoucherOrderFormFES.docx", tbVoucherType.Text);
                        break;
                    default:
                        break;
                }
            }

            getVoucherLogForPeriod();
            loadVItmsList();
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
            fillHistoryList(lvByDate, queryText, "TrxDate DESC, vtype, Description", 0);
            fillHistoryList(lvByVType, queryText, "vtype, Description, TrxDate DESC", 1);
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
                command = new SqlCommand(sqlText + " ORDER BY " + mysortorder, SQLConn);
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
            if (SQLConn.State == ConnectionState.Closed)
            {
                SQLConn.Open();
            }
        }
        /// <summary>
        /// Closes a connection to the Database
        /// </summary>
        private void closeConnection()
        {
            if (SQLConn.State == ConnectionState.Open)
            {
                SQLConn.Close();
            }
        }

        private void cboVHistoryPeriod_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //Registry.SetValue(CCFBGlobal.registryKeyCurrentUser, constVoucherLogPeriod, cboTrxLogPeriod.SelectedIndex);
            getVoucherLogForPeriod();
        }

        private void btnEditVoucherLog_Click(object sender, EventArgs e)
        {
            try
            {
                int trxID = Convert.ToInt32(btnEditVoucherLog.Tag);
                clsVoucher.open(trxID);
                clsVItems.open(clsVoucher.VoucherItemID);
                fillEditFields(clsVItems.Description);
                setEditMode(true);
            }
            catch (Exception)
            {
            }
        }

        private void lvByDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (loadingData == false && lvByDate.SelectedItems.Count > 0)
            {
                btnEditVoucherLog.Tag = lvByDate.SelectedItems[0].Tag;
                btnEditVoucherLog.Text = "Edit TrxId: " + btnEditVoucherLog.Tag.ToString();
            }
        }

        private void lvByVType_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnEditVoucherLog.Tag = lvByVType.SelectedItems[0].Tag;
            btnEditVoucherLog.Text = "Edit TrxId: " + btnEditVoucherLog.Tag.ToString();
        }

        private void dtpTrxDate_ValueChanged(object sender, EventArgs e)
        {
            if (dtpTrxDate.Focused == true)
            {
                dtDefault = dtpTrxDate.Value;
                if (clsVoucher.RowCount > 0)
                {
                    clsVoucher.TrxDate = dtpTrxDate.Value;
                    if (inEditMode == true)
                    {
                        tbTrxDate.Text = clsVoucher.TrxDate.ToLongDateString();
                    }
                }
            }
        }

        private void tboxes_Leave(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text != oriText)
            {
                clsVoucher.SetDataValue(tb.Tag.ToString(), tb.Text);
                if (tb.Name.StartsWith("tbf") == true)
                {
                    clsVoucher.TotalFamily = clsVoucher.Infants + clsVoucher.Youth + clsVoucher.Teens
                                           + clsVoucher.Eighteen + clsVoucher.Adults + clsVoucher.Seniors;
                    tbTotalFamily.Text = clsVoucher.TotalFamily.ToString();
                }
            }
        }

        private void tbBoxes_Enter(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            oriText = tb.Text;
        }

        private void chkBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            if (chk.Focused == true)
            {
                clsVoucher.SetDataValue(chk.Tag.ToString(), chk.Checked); 
            }
        }

        private void chkBox_Enter(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            oriBool = chk.Checked;
        }

        private void cboHHCat_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox cbo = (ComboBox)sender;
            if (Convert.ToInt32(cbo.SelectedValue) != clsVoucher.ClientType)
            {
                clsVoucher.SetDataValue(cbo.Tag.ToString(), cbo.SelectedValue.ToString());
            }
        }

        private void tbQty_Leave(object sender, EventArgs e)
        {
            decimal newamt = -1;
            try
            {
                newamt = Convert.ToDecimal(tbQty.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid Amount [" + tbQty.Text + "]", "Amount Invalid");
                tbQty.Focus();
                return;
            }
            if (newamt != oriAmt)
            {
                clsVoucher.Amount = newamt;
                oriAmt = newamt;
            }
        }
    }
}
