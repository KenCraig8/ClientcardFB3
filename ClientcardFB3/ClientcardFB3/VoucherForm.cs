using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace ClientcardFB3
{
    public partial class VoucherForm : Form
    {
        Client clsClient;

        SqlCommand command;
        SqlDataReader reader;
        SqlConnection conn = new SqlConnection(CCFBGlobal.connectionString);
        
        List<TextBox> tbFamData = new List<TextBox>();
        List<CheckBox> chkList = new List<CheckBox>();

        bool inEditMode = true;

        string comments = "";
        decimal amount = -1m;
        int rowIndex = -1;
        bool loadingData = false;

        public VoucherForm(Client clsClientIn)
        {
            InitializeComponent();
            clsClient = clsClientIn;
            loadingData = true;
            dtpTrxDate.Value = DateTime.Today;
            initForm();
        }

        public VoucherForm(Client clsClientIn, DateTime EditVoucherDate)
        {
            InitializeComponent();
            clsClient = clsClientIn;
            loadingData = true;
            dtpTrxDate.Value = EditVoucherDate;
            initForm();
        }

        private void initForm()
        {
            loadingData = true;
            splitContainer1.Panel2.BackColor = CCFBGlobal.bkColorBaseEdit;
            splitContainer1.Panel1.BackColor = CCFBGlobal.bkColorBaseEdit;
            gridVouchers.BackgroundColor = CCFBGlobal.bkColorBaseEdit;
            //Load cboClientType
            CCFBGlobal.InitCombo(cboClientType, CCFBGlobal.parmTbl_Client);

            foreach (TextBox tb in splitContainer1.Panel1.Controls.OfType<TextBox>())
            {
                if (tb.Tag != null)
                {
                    tbFamData.Add(tb);
                    tb.LostFocus += new System.EventHandler(this.tbFamData_LostFocus);
                }
            }
            foreach (CheckBox chk in splitContainer1.Panel1.Controls.OfType<CheckBox>())
            {
                chkList.Add(chk);
            }
            tbClient.Text = clsClient.clsHH.ID + Environment.NewLine + clsClient.clsHH.Name;

            fillForm();
            loadDescriptions();
            loadList();
            loadingData = false;
        }

        public void fillForm()
        {
            cboClientType.SelectedValue = clsClient.clsHH.ClientType.ToString();
            foreach (TextBox tb in tbFamData)
            {
                tb.Text = clsClient.clsHH.GetDataValue(tb.Tag.ToString()).ToString();
            }
            foreach (CheckBox chk in chkList)
            {
                chk.Checked = (bool)clsClient.clsHH.GetDataValue(chk.Tag.ToString());
            }
        }

        private void loadDescriptions()
        {
            try
            {
                loadingData = true;
                Application.UseWaitCursor = true;
                gridVouchers.Rows.Clear();
                Application.DoEvents();
                DateTime start = new DateTime(dtpTrxDate.Value.Year, 1, 1);
                conn.Open();
                command = new SqlCommand("select vi.uid, vi.Description, vi.maxAmount, vi.DefaultAmount "
                    + ",(SELECT SUM(amount) FROM VoucherLog WHERE TrxDate between '" + start + "' and '" + DateTime.Today.AddDays(-1) + "' "
                    + " and HouseholdID = " + clsClient.clsHH.ID + " and VoucherItemId = vi.Uid) Cum "
                    + " From VoucherItems vi where vi.Inactive = 0", conn);
                //command = new SqlCommand("Select * From VoucherItems Order By Description ASC", conn);
                reader = command.ExecuteReader();
                rowIndex = 0;
                decimal leftOver = 0;
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        gridVouchers.Rows.Add();
                        gridVouchers["clmDesc", rowIndex].Tag = reader.GetValue(0);
                        gridVouchers["clmDesc", rowIndex].Value = reader.GetValue(1);

                        if (reader.GetValue(4).ToString() != "")
                            gridVouchers["clmAmntRcvd", rowIndex].Value = Convert.ToDecimal(reader.GetValue(4)).ToString("C");
                        else
                            gridVouchers["clmAmntRcvd", rowIndex].Value = "--";

                        if (reader.GetValue(4).ToString() != "")
                            leftOver = Convert.ToDecimal(reader.GetValue(2)) - Convert.ToDecimal(reader.GetValue(4));
                        else
                            leftOver = reader.GetDecimal(2);

                        gridVouchers["clmAmntAvail", rowIndex].Value = leftOver.ToString("C");
                        gridVouchers["clmAmountGiven", rowIndex].Value = "0";
                        gridVouchers["clmComments", rowIndex].Value = "";
                        gridVouchers["clmDefaultAmount", rowIndex].Value = reader.GetValue(3);
                        rowIndex++;
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

        private void loadList()
        {
            try
            {
                conn.Open();
                command = new SqlCommand("Select v.VoucherItemID, v.Amount, v.Notes, V.trxID "
                    + "From VoucherLog V Left Join Household H on "
                    + "V.HouseholdID=H.ID Where H.ID=" + clsClient.clsHH.ID.ToString()
                    + " And V.trxDate='" + dtpTrxDate.Value.ToShortDateString() + "'", conn);
                reader = command.ExecuteReader();

                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        for (int i = 0; i < gridVouchers.Rows.Count; i++)
                        {
                            if (Convert.ToInt32(gridVouchers["clmDesc", i].Tag) == id)
                            {
                                gridVouchers["clmAmountGiven", i].Value = reader.GetDecimal(1).ToString("C");
                                gridVouchers["clmComments", i].Value = reader.GetValue(2);
                                gridVouchers.Rows[i].Tag = reader.GetValue(3);
                                gridVouchers["clmEnable", i].Value = CheckState.Checked;
 
                                if (gridVouchers["clmComments", i].Value == null)
                                    gridVouchers["clmComments", i].Value = "";
                                //gridVouchers["clmAmountGiven", i].ReadOnly = true;
                                //gridVouchers["clmComments", i].ReadOnly = true;
                                break;
                            }
                        }
                    }
                }


                conn.Close();
            }
            catch(SqlException ex) 
            {
                CCFBGlobal.appendErrorToErrorReport("Command Text= " + command.CommandText, 
                    ex.GetBaseException().ToString());
            }
           
        }

        private void tbFamData_LostFocus(object sender, EventArgs e)
        {
            TextBox tbHH = (TextBox)sender; //Get the correct textbox

            if (inEditMode == true)
            {
                //If current value does not = value of textbox
                tbTotalFam.Text = (Int32.Parse(tbYouth.Text) + Int32.Parse(tbAdults.Text) +
                    Int32.Parse(tbTeens.Text) + Int32.Parse(tbInfants.Text) + Int32.Parse(tbSeniors.Text)).ToString();
            }
            else
            {
                //If not in edit mode, reset the text with value from class
                tbHH.Text = clsClient.clsHH.GetDataValue(tbHH.Tag.ToString()).ToString();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gridVouchers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (loadingData == false && e.ColumnIndex == 3 &&
               gridVouchers.Rows[e.RowIndex].Cells[e.ColumnIndex].GetEditedFormattedValue(e.RowIndex, DataGridViewDataErrorContexts.Display).ToString() == "Checked")
            {
                decimal testValue = 0;
                //if (gridVouchers["clmAmntRcvd", e.RowIndex].Value.ToString() != "--")
                testValue = cleanDisplayValue(gridVouchers["clmAmntAvail", e.RowIndex].Value.ToString());
                if (testValue > 0)
                {
                    if (cleanDisplayValue(gridVouchers["clmDefaultAmount", e.RowIndex].Value.ToString()) < cleanDisplayValue(gridVouchers["clmAmntAvail", e.RowIndex].Value.ToString()))
                    {
                        gridVouchers["clmAmountGiven", e.RowIndex].Value = gridVouchers["clmDefaultAmount", e.RowIndex].Value;
                    }
                    else
                    {
                        gridVouchers["clmAmountGiven", e.RowIndex].Value = gridVouchers["clmAmntAvail", e.RowIndex].Value;
                    }
                }
                else
                    gridVouchers["clmAmountGiven", e.RowIndex].Value = "0.00";
                gridVouchers["clmAmntRcvd", e.RowIndex].ReadOnly = false;
                gridVouchers["clmComments", e.RowIndex].ReadOnly = false;
            }
        }

        private void gridVouchers_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (loadingData == false)
            {
                if (gridVouchers["clmAmountGiven", e.RowIndex].Value.ToString() != "")
                {
                    comments = gridVouchers["clmComments", e.RowIndex].Value.ToString();
                    amount = cleanDisplayValue(gridVouchers["clmAmountGiven", e.RowIndex].Value.ToString());
                    rowIndex = e.RowIndex;
                }
            }
        }

        private void gridVouchers_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (gridVouchers["clmComments", e.RowIndex].GetEditedFormattedValue(
                e.RowIndex, DataGridViewDataErrorContexts.Commit).ToString() != comments
                || amount != cleanDisplayValue(gridVouchers["clmAmountGiven", e.RowIndex].Value.ToString()))
            {
                comments = gridVouchers["clmComments", e.RowIndex].GetEditedFormattedValue(
                    e.RowIndex, DataGridViewDataErrorContexts.Commit).ToString();
                amount = cleanDisplayValue(gridVouchers["clmAmountGiven", e.RowIndex].GetEditedFormattedValue(
                    e.RowIndex, DataGridViewDataErrorContexts.Commit).ToString());

                if (gridVouchers.Rows[e.RowIndex].Tag != null && amount > 0)
                    updateLog();
                else if (gridVouchers.Rows[e.RowIndex].Tag != null)
                {
                    delete();
                    timer1.Start();
                }
                else
                    insertIntoLog();
            }
        }

        private void delete()
        {
            try
            {
                conn.Open();
                command = new SqlCommand("Delete From VoucherLog Where TrxID=" + gridVouchers.Rows[rowIndex].Tag, conn);
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
                conn.Open();
                command = new SqlCommand("Update VoucherLog Set TrxDate = '" + dtpTrxDate.Value.ToShortDateString() + "', "
                + "HouseholdID = " + clsClient.clsHH.ID.ToString() + ", Amount = " + amount.ToString() + "," 
                + "Notes = '" + comments + "', VoucherItemID=" + gridVouchers["clmDesc", rowIndex].Tag.ToString() + " ,"
                + "Infants= " + tbInfants.Text + ", Youth= " + tbYouth.Text + ", Teens=" + tbTeens.Text + ", "
                + "Eighteen=0, Adults=" + tbAdults.Text + ", Seniors=" + tbSeniors.Text + ", TotalFamily=" + tbTotalFam.Text
                + ", Disabled=" + tbDisabled.Text + ", Homeless = " + Convert.ToInt32(chkHomeless.Checked).ToString() 
                + ", Modified = '" + DateTime.Now.ToString() + "', ModifiedBy='" + CCFBGlobal.dbUserName + "'"
                + " Where trxID=" + gridVouchers.Rows[rowIndex].Tag.ToString(), conn);
                command.ExecuteNonQuery();
                conn.Close();
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
                conn.Open();
                command = new SqlCommand("Insert Into VoucherLog (TrxDate, HouseholdID, Amount, Notes, VoucherItemID,"
                    + "Infants, Youth, Teens, Eighteen, Adults, Seniors, TotalFamily, Disabled, Homeless, Transient, Created, CreatedBy"
                    + ", FiscalFirstTime, CalFirstTime, MonthFirstTime) "
                    + "Values('" + dtpTrxDate.Value.ToShortDateString() + "', " + clsClient.clsHH.ID.ToString() + ", "
                    + amount.ToString() + ", '" + comments + "', " + gridVouchers["clmDesc", rowIndex].Tag.ToString() 
                    + ", " + tbInfants.Text + ", " + tbYouth.Text + ", " + tbTeens.Text + ", " + "0," + tbAdults.Text + ", " 
                    + tbSeniors.Text + ", " + tbTotalFam.Text + ", " + tbDisabled.Text 
                    + ", " + Convert.ToInt32( chkHomeless.Checked).ToString() + "," + cboClientType.SelectedValue 
                    + ",'" + DateTime.Now.ToString()  + "','" + CCFBGlobal.dbUserName + "',0,0,0)"
                    , conn);
                command.ExecuteNonQuery();
                conn.Close();
                loadList();
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
                timer1.Start();
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
                    + "HouseholdID = " + clsClient.clsHH.ID.ToString(), conn);
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            Application.DoEvents();

            loadDescriptions();
            loadList();
        }

        private decimal cleanDisplayValue(string testValue)
        {
            if (testValue.Trim() != "")
            { return Convert.ToDecimal(testValue.Replace("$", "")); }
            return Convert.ToDecimal("0");
        }
    }
}
