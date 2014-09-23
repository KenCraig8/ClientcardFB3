using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace ClientcardFB3
{
    public partial class UWKCExportForm : Form
    {

        CreateUnitedWayExport clsCreateUWExport;
        SqlConnection conn;

        public UWKCExportForm()
        {
            InitializeComponent();
            tbAgencyNumber.Text = CCFBPrefs.AgencyNumber;
            conn = new SqlConnection(CCFBGlobal.connectionString);
        }

        private void tbAgencyNumber_Leave(object sender, EventArgs e)
        {
            CCFBPrefs.AgencyNumber = tbAgencyNumber.Text;
            CCFBPrefs.SaveValue(tbAgencyNumber.Tag.ToString(), tbAgencyNumber.Text);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtUWExport = new DataTable();
                SqlCommand cmd = new SqlCommand("UnitedWayExport", conn);
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = cmd.Parameters.Add("@UseSurveyComplete", SqlDbType.Int);
                param.Value = Convert.ToInt32(rdoUseOnlySurveyComplete.Checked);
                SqlParameter param2 = cmd.Parameters.Add("@FoodBankID", SqlDbType.VarChar);
                param2.Value = tbAgencyNumber.Text;

                openConnection();

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dtUWExport);
                }
                clsCreateUWExport = new CreateUnitedWayExport(dtUWExport);
                this.Cursor = Cursors.WaitCursor;
                clsCreateUWExport.createExport(System.IO.Path.Combine(CCFBPrefs.ReportsSavePath,"UnitedWayExport.xls"),
                        CCFBGlobal.fb3TemplatesPath + "2011-2012 ElectronicTemplate.xls");
                this.Cursor = Cursors.Default;
            }
            catch (SqlException ex)
            {
                CCFBGlobal.appendErrorToErrorReport("UnitedWayExport", ex.GetBaseException().ToString());
                closeConnection();
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
