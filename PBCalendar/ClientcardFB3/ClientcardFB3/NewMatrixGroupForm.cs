using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ClientcardFB3
{
    public partial class NewMatrixGroupForm : Form
    {
        SqlConnection conn = new SqlConnection(CCFBGlobal.connectionString);
        IncomeGroups clsIncomeGroups = new IncomeGroups();
        IncomeMatrix clsIncomeMatrix = new IncomeMatrix();
        string groupName = "";

        public string GroupName
        {
            get
            {
                return groupName;
            }
        }
        public NewMatrixGroupForm()
        {
            InitializeComponent();
            dtpAsOfDate.Value = DateTime.Today;
            clsIncomeGroups.openAll();
            clsIncomeMatrix.openWhere("");
            CCFBGlobal.InitCombo(cboProcessID, "parm_IncomeProcessID");
        }

        private void NewMatrixGroupForm_Load(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DataRow drow = clsIncomeGroups.DSet.Tables[0].NewRow();

            drow["Description"] = tbDesc.Text;
            drow["ShortName"] = tbShortName.Text;
            drow["Notes"] = tbNotes.Text;
            drow["AsOfDate"] = dtpAsOfDate.Value;
            drow["Created"] = DateTime.Now;
            drow["CreatedBy"] = CCFBGlobal.currentUser_Name;
            drow["Modified"] = DateTime.Now;
            drow["ModifiedBy"] = CCFBGlobal.currentUser_Name;

            clsIncomeGroups.DSet.Tables[0].Rows.Add(drow);

            clsIncomeGroups.update();
            clsIncomeGroups.openWhere("");
            clsIncomeGroups.setDataRow(clsIncomeGroups.RowCount -1);

            clsIncomeMatrix.DSet.Tables[0].Rows.Add(createDataRow(clsIncomeMatrix.DSet.Tables[0].NewRow(), "Catagory A", "Very Low", clsIncomeGroups.Id, 0,0));
            clsIncomeMatrix.DSet.Tables[0].Rows.Add(createDataRow(clsIncomeMatrix.DSet.Tables[0].NewRow(), "Catagory B", "Low", clsIncomeGroups.Id, 0, 0));
            clsIncomeMatrix.DSet.Tables[0].Rows.Add(createDataRow(clsIncomeMatrix.DSet.Tables[0].NewRow(), "Catagory C", "Moderate", clsIncomeGroups.Id, 0, 0));
            clsIncomeMatrix.DSet.Tables[0].Rows.Add(createDataRow(clsIncomeMatrix.DSet.Tables[0].NewRow(), "Catagory D", "Hi", clsIncomeGroups.Id, 0, 999999));
            
            clsIncomeMatrix.update();

            groupName = tbDesc.Text;

            this.Close();
        }

        private DataRow createDataRow(DataRow drow, string matrixLabel1, string matrixLabel2,
            int group, int incomeLow, int incomeHi)
        {
            drow["IncomeGroup"] = group;
            drow["Label1"] = matrixLabel1;
            drow["Label2"] = matrixLabel2;
            drow["Label3"] = "";
            drow["IncomeLow1"] = incomeLow;
            drow["IncomeLow2"] = incomeLow;
            drow["IncomeLow3"] = incomeLow;
            drow["IncomeLow4"] = incomeLow;
            drow["IncomeLow5"] = incomeLow;
            drow["IncomeLow6"] = incomeLow;
            drow["IncomeLow7"] = incomeLow;
            drow["IncomeLow8"] = incomeLow;
            drow["IncomeLow9"] = incomeLow;
            drow["IncomeLow10"] = incomeLow;
            drow["IncomeHi1"] = incomeHi;
            drow["IncomeHi2"] = incomeHi;
            drow["IncomeHi3"] = incomeHi;
            drow["IncomeHi4"] = incomeHi;
            drow["IncomeHi5"] = incomeHi;
            drow["IncomeHi6"] = incomeHi;
            drow["IncomeHi7"] = incomeHi;
            drow["IncomeHi8"] = incomeHi;
            drow["IncomeHi9"] = incomeHi;
            drow["IncomeHi10"] = incomeHi;
            drow["Created"] = DateTime.Now;
            drow["CreatedBy"] = CCFBGlobal.currentUser_Name;
            drow["Modified"] = DateTime.Now;
            drow["ModifiedBy"] = CCFBGlobal.currentUser_Name;
            return drow;
        }
    }
}
