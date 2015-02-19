using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientcardFB3
{
    public partial class SelectTables : Form
    {
        const string databaseName = "ClientcardFB3";
        string connectionString = @"Server=MYCOMPUTER\SQLEXPRESS;initial catalog=" + databaseName + "; UID=CCFB_User; PWD='19800612'; Trusted_Connection = False; Connect Timeout=10;";
        DataHelper dataHelper;
        // Access this for testing
        public CustSqlWhere sqlWhereForm;

        /// <summary>
        /// For testing. Dependency injection.
        /// </summary>
        /// <param name="dataHelper"></param>
        public SelectTables(DataHelper dataHelper)
        {
            this.dataHelper = dataHelper;
            InitializeComponent();
        }

        string selectedTableName;
        public SelectTables() : this(new DataHelper()){}

        /// <summary>
        /// Display the user choices for the table in a list box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectForm_Load(object sender, EventArgs e)
        {
            ArrayList tableChoices = new ArrayList {"HouseholdMembers", "Household", "Donors", "Volunteers" };

            lstSelectTable.DataSource = tableChoices;
        }

        /// <summary>
        /// Change the columns in the list to match the selected columns
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstSelectTable_SelectedValueChanged(object sender, EventArgs e)
        {
            //use the table name the user selected in the list box
            selectedTableName = (string)lstSelectTable.SelectedItem;

            string sqlQuery = "SELECT COLUMN_NAME FROM [" + databaseName + "].information_schema.columns WHERE TABLE_NAME = '"+selectedTableName+"'";

            DataTable columnNamesTable = dataHelper.sqlSelectQuery(connectionString, sqlQuery);

            //put the columns in the list box
            string[] columnList = columnNamesTable.AsEnumerable().Select(row => row.Field<string>("COLUMN_NAME")).ToArray();
            lstSelectColumns.DataSource = columnList;
        }

        /// <summary>
        /// display the window for the user to select the where clause
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectWhere_Click(object sender, EventArgs e)
        {
            string[] selectedColumns = lstSelectColumns.SelectedItems.Cast<string>().ToArray();

            sqlWhereForm = new CustSqlWhere(dataHelper, connectionString, selectedColumns, databaseName, selectedTableName);
            sqlWhereForm.Show();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            ExportSQLToExcel exportForm = new ExportSQLToExcel();
            exportForm.Show();
        }
    }
}
