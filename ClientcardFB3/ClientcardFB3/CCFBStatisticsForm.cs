using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

namespace ClientcardFB3
{
    public partial class CCFBStatisticsForm : Form
    {
        SqlConnection conn;
        string connectString;
        string sqlText;
        public CCFBStatisticsForm(string connectionstring)
        {
            InitializeComponent();
            connectString = connectionstring;
            conn = new SqlConnection(connectString);
            cboTableType.SelectedIndex = 0;
        }

        private void fillForm()
        {
            ListViewItem lvi;
            SqlDataReader reader;
            SqlCommand sqlCmd;
            lvwData.Items.Clear();
            try
            {
                //The order of the select statements directly corresponds to what shows on the ListView
                sqlCmd = new SqlCommand(sqlText , conn);
                conn.Open();
                reader = sqlCmd.ExecuteReader();

                int count = 0;
                int col = 0;
                int val = 0;
                //btnEditTransLog.Enabled = reader.HasRows;

                while (reader.Read())
                {
                    lvi = new ListViewItem(count.ToString());
                    for (int i = 0; i < reader.FieldCount - 1; i++)
                    {
                        col = i+1;
                        switch (Convert.ToInt32(lvwData.Columns[col].Tag))
                        {
                            case 1:
                                try { val = Convert.ToInt32(reader.GetValue(i)); }
                                catch { val = 0; }
                                lvi.SubItems.Add(CCFBGlobal.formatNumberWithCommas(val));
                                break;
                            case 2:
                                lvi.SubItems.Add(Convert.ToDecimal(reader.GetValue(i)).ToString("F"));
                                break;
                            default:
                                lvi.SubItems.Add(reader.GetValue(i).ToString());
                                break;
                        }
                        
                    }
                    lvwData.Items.Add(lvi);
                    count++;
                }
                conn.Close();
            }
            catch (SqlException ex)
            {
                //closeConnection();
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString());
            }
            catch (Exception ex)
            {
                //closeConnection();
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString());
            }


        }

        private void btnLoadStats_Click(object sender, EventArgs e)
        {
            string inactiveclause;
            if (rdoOnlyActive.Checked == true)
                inactiveclause = "Inactive = 0";
            else if (rdoOnlyInactive.Checked == true)
                inactiveclause = "Inactive = 1";
            else
                inactiveclause = "";

            switch (cboTableType.SelectedIndex)
            {
                case 0:         //Household
                    sqlText = "Select 'Household'"
                            + ", case Inactive WHEN 0 THEN 'Active' ELSE 'Inactive' END Active"
                            + ", count(*) NbrRows"
                            + ", SUM(cast(SurveyComplete as int))	Survey_Complete"
                            + ", SUM(cast(SingleHeadHh as int))		Single_Parent_Household"
                            + ", SUM(cast(UseFamilyList as int))	Use_Family_List"
                            + ", SUM(cast(NeedToVerifyID as int))	Need_Proof_Of_Address"
                            + ", SUM(cast(NeedCommoditySignature as int))	Need_Commodity_Signature"
                            + ", SUM(cast(NoCommodities as int))	No_Commodities"
                            + ", SUM(cast(SupplOnly as int))		Supplemental_Only"
                            + ", SUM(cast(Homeless as int))			Homeless"
                            + ", SUM(cast(NeedIncomeVerification as int))	Need_Income_Verification"
                            + ", SUM(cast(BabyServices as int))		BabyServices"
                            + ", sum(Infants) Infants"
                            + ", SUM(Youth) Children"
                            + ", SUM(Teens) Teens"
                            + ", SUM(Eighteen) Eighteens"
                            + ", SUM(Adults) Adults"
                            + ", SUM(Seniors) Seniors"
                            + ", SUM(TotalFamily) Total_Individuals"
                            + ", AVG(CAST(TotalFamily as Float)) Avg_Family_Size"
                            + " FROM Household";
                    if (inactiveclause != "")
                        sqlText += " WHERE " + inactiveclause;

                    sqlText += " GROUP by case Inactive WHEN 0 THEN 'Active' ELSE 'Inactive' END";
                    break;
                case 1:         //Household By Category
                    sqlText = "select pt.Type"
                            + ", case Inactive WHEN 0 THEN 'Active' ELSE 'Inactive' END Active"
                            + ", COUNT(*) NbrRows"
                            + ", SUM(cast(SurveyComplete as int))	Survey_Complete"
                            + ", SUM(cast(SingleHeadHh as int))		Single_Parent_Household"
                            + ", SUM(cast(UseFamilyList as int))	Use_Family_List"
                            + ", SUM(cast(NeedToVerifyID as int))	Need_Proof_Of_Address"
                            + ", SUM(cast(NeedCommoditySignature as int))	Need_Commodity_Signature"
                            + ", SUM(cast(NoCommodities as int))	No_Commodities"
                            + ", SUM(cast(SupplOnly as int))		Supplemental_Only"
                            + ", SUM(cast(Homeless as int))			Homeless"
                            + ", SUM(cast(NeedIncomeVerification as int))	Need_Income_Verification"
                            + ", SUM(cast(BabyServices as int))		BabyServices"
                            + ", sum(Infants) Infants"
                            + ", SUM(Youth) Children"
                            + ", SUM(Teens) Teens"
                            + ", SUM(Eighteen) Eighteens"
                            + ", SUM(Adults) Adults"
                            + ", SUM(Seniors) Seniors"
                            + ", SUM(TotalFamily) Total_Individuals"
                            + ", AVG(CAST(TotalFamily as Float)) Avg_Family_Size, h.ClientType"
                            + " FROM Household h LEFT join parm_ClientType pt on h.ClientType = pt.ID";
                     if (inactiveclause != "")
                        sqlText += " WHERE " + inactiveclause;

                     sqlText += " GROUP by h.ClientType, case Inactive WHEN 0 THEN 'Active' ELSE 'Inactive' END,pt.Type"
                             + " ORDER by case Inactive WHEN 0 THEN 'Active' ELSE 'Inactive' END,h.ClientType, pt.Type";
                    break;
                case 2:         //Household Members
                    break;
                case 3:         //Donors
                    break;
                case 4:         //Donations Cash
                    break;
                case 5:         //Food Receipts
                    break;
                case 6:         //Food Services
                    break;
                case 7:         //Volunteers
                    break;
                case 8:         //Volunteer Hours
                    break;
                default:
                    sqlText = "";
                    break;
            }
            if (sqlText != "")
                fillForm();
        }
    }
}
