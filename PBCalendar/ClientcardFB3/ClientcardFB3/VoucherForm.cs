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

        bool inEditMode = true;

        string comments = "";
        double amount = -1;
        int rowIndex = -1;

        public VoucherForm(Client clsClientIn)
        {
            InitializeComponent();

            splitContainer1.Panel2.BackColor = CCFBGlobal.bkColorBaseEdit;
            tpDemographics.BackColor = CCFBGlobal.bkColorBaseEdit;
            gridVouchers.BackgroundColor = CCFBGlobal.bkColorBaseEdit;

            clsClient = clsClientIn;

            foreach (TextBox tb in tpDemographics.Controls.OfType<TextBox>())
            {
                tbFamData.Add(tb);
                tb.LostFocus += new System.EventHandler(this.tbFamData_LostFocus);
            }

            dtpTrxDate.Value = DateTime.Today;

            tbClient.Text = clsClient.clsHH.ID + Environment.NewLine + clsClient.clsHH.Name;

            fillForm();
            loadDescriptions();
            loadList();
        }

        public void fillForm()
        {
            foreach (TextBox tb in tbFamData)
            {
                tb.Text = clsClient.clsHH.GetDataValue(tb.Tag.ToString()).ToString();
            }
        }

        private void loadDescriptions()
        {
            try
            {
                conn.Open();
                command = new SqlCommand("Select * From VoucherItems Order By Description ASC", conn);
                reader = command.ExecuteReader();
                rowIndex = 0;
               
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        gridVouchers.Rows.Add();
                        gridVouchers["clmDesc", rowIndex].Tag = reader.GetValue(0);
                        gridVouchers["clmDesc", rowIndex].Value = reader.GetValue(1);
                        gridVouchers["clmAmount", rowIndex].Value = 0;
                        gridVouchers["clmComments", rowIndex].Value = "";
                        rowIndex++;
                    }
                }
                conn.Close();
            }
            catch (SqlException ex)
            {
                CCFBGlobal.appendErrorToErrorReport("Command Text= " + command.CommandText,
                    ex.GetBaseException().ToString(), CCFBGlobal.serverName);
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
                                gridVouchers["clmAmount", i].Value = reader.GetDecimal(1);
                                gridVouchers["clmComments", i].Value = reader.GetValue(2);
                                gridVouchers.Rows[i].Tag = reader.GetValue(3);

                                if (gridVouchers["clmComments", i].Value == null)
                                    gridVouchers["clmComments", i].Value = "";

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
                    ex.GetBaseException().ToString(), CCFBGlobal.serverName);
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

        private void gridVouchers_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            comments = gridVouchers["clmComments", e.RowIndex].Value.ToString();
            amount = Convert.ToDouble(gridVouchers["clmAmount", e.RowIndex].Value);
            rowIndex = e.RowIndex;
        }

        private void gridVouchers_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (gridVouchers["clmComments", e.RowIndex].GetEditedFormattedValue(
                e.RowIndex, DataGridViewDataErrorContexts.Commit).ToString() != comments
                || amount != Convert.ToDouble(gridVouchers["clmAmount", e.RowIndex].Value))
            {
                comments = gridVouchers["clmComments", e.RowIndex].GetEditedFormattedValue(
                    e.RowIndex, DataGridViewDataErrorContexts.Commit).ToString();
                amount = Convert.ToDouble(gridVouchers["clmAmount", e.RowIndex].GetEditedFormattedValue(
                    e.RowIndex, DataGridViewDataErrorContexts.Commit));

                if (gridVouchers.Rows[e.RowIndex].Tag != null && amount > 0)
                    updateLog();
                else if (gridVouchers.Rows[e.RowIndex].Tag != null)
                    delete();
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
                loadList();
            }
            catch(System.Exception ex) 
            {
                CCFBGlobal.appendErrorToErrorReport("Command Text = " + command.CommandText, ex.GetBaseException().ToString(),
                    CCFBGlobal.serverName);
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
                + ", Disabled=" + tbDisabled.Text + " Where trxID=" + gridVouchers.Rows[rowIndex].Tag.ToString(), conn);
                command.ExecuteNonQuery();
                conn.Close();
            }
            catch (System.Exception ex)
            {
                CCFBGlobal.appendErrorToErrorReport("Command Text = " + command.CommandText,
                    ex.GetBaseException().ToString(), CCFBGlobal.serverName);
            }
        }

        private void insertIntoLog()
        {
            try
            {
                conn.Open();
                command = new SqlCommand("Insert Into VoucherLog (TrxDate, HouseholdID, Amount, Notes, VoucherItemID,"
                    + "Infants, Youth, Teens, Eighteen, Adults, Seniors, TotalFamily, Disabled) "
                    + "Values('" + dtpTrxDate.Value.ToShortDateString() + "', " + clsClient.clsHH.ID.ToString() + ", "
                    + amount.ToString() + ", '" + comments + "', " + gridVouchers["clmDesc", rowIndex].Tag.ToString() 
                    + ", " + tbInfants.Text + ", " + tbYouth.Text + ", " + tbTeens.Text + ", " + "0," + tbAdults.Text + ", " 
                    + tbSeniors.Text + ", " + tbTotalFam.Text + ", " + tbDisabled.Text + ")", conn);
                command.ExecuteNonQuery();
                conn.Close();
                loadList();
            }
            catch(System.Exception ex) 
            {
                CCFBGlobal.appendErrorToErrorReport("Command Text = " + command.CommandText,
                    ex.GetBaseException().ToString(), CCFBGlobal.serverName);
            }
        }

        private void dtpTrxDate_ValueChanged(object sender, EventArgs e)
        {
            loadList();
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
                    ex.GetBaseException().ToString(), CCFBGlobal.serverName);
            }
        }
    }
}
