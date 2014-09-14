using System;
using Moq;
using NUnit.Framework;
using NUnit.Extensions.Forms;
//using CustomSQL;
using System.Data;

namespace CustomSQL
{
    [TestFixture]
    public class SelectTablesTest
    {
        //private MockFactory mockFactory = new MockFactory();

        /// <summary>
        /// Test that the sqlQuery passed into dataHelper.sqlSelectQuery is correct.
        /// Also test that lstSelectColumns is filled correctly.
        /// </summary>
        [Test]
        public void lstSelectTable_SelectedValueChangedTest()
        {
            // Test it is correct when the form loads
            var dataMock = new Mock<DataHelper>();

            // Setup Sql query return value
            string[] columnsList = new string[] { "1st", "2nd" };
            DataTable outTable = new DataTable();
            outTable.Columns.Add("COLUMN_NAME");
            foreach (string col in columnsList)
            {
                outTable.Rows.Add(new string[] { col });
            }
            // Mock the sqlSelectQuery
            string tableName = "HouseholdMembers";
            string queryString = "SELECT COLUMN_NAME FROM [ClientcardFB3].information_schema.columns WHERE TABLE_NAME = '" + tableName + "'";
            dataMock.Setup(_ => _.sqlSelectQuery(It.IsAny<string>(), queryString)).Returns(outTable);

            // Initialize class
            SelectTables selectTablesForm = new SelectTables(dataMock.Object);
            selectTablesForm.Show();

            ListBoxTester lstSelectColumnsTester = new ListBoxTester("lstSelectColumns");
            string[] columnsListAct = (string[])lstSelectColumnsTester.Properties.DataSource;

            Assert.AreEqual(columnsList, columnsListAct);


            //Test it's correct when another value in the list box is selected
            string[] columnsList2 = new string[] { "3rd", "4th" };
            DataTable outTable2 = new DataTable();
            outTable2.Columns.Add("COLUMN_NAME");
            foreach (string col in columnsList2)
            {
                outTable2.Rows.Add(new string[] { col });
            }
            string tableName2 = "Household";
            string queryString2 = "SELECT COLUMN_NAME FROM [ClientcardFB3].information_schema.columns WHERE TABLE_NAME = '" + tableName2 + "'";
            dataMock.Setup(_ => _.sqlSelectQuery(It.IsAny<string>(), queryString2)).Returns(outTable2);

            ListBoxTester lstSelectTableTester = new ListBoxTester("lstSelectTable");
            lstSelectTableTester.Select(tableName2);

            columnsListAct = (string[])lstSelectColumnsTester.Properties.DataSource;
            Assert.AreEqual(columnsList2, columnsListAct);
        }

        /// <summary>
        /// Test this opens the sql where form and the parameters are passed correctly
        /// </summary>
        [Test]
        [STAThread]
        public void btnSelectWhere_ClickTest()
        {
            // Setup mock sqlSelectQuery
            var dataMock = new Mock<DataHelper>();
            DataTable outTable = new DataTable();
            outTable.Columns.Add("COLUMN_NAME");
            outTable.Rows.Add(new string[] { "1st" });
            outTable.Rows.Add(new string[] { "2nd" });
            dataMock.Setup(_ => _.sqlSelectQuery(It.IsAny<string>(), It.IsAny<string>())).Returns(outTable);

            SelectTables selectTablesForm = new SelectTables(dataMock.Object);
            selectTablesForm.Show();

            // Select "1st" from the list box and click the where clause btn
            ListBoxTester lstSelectColumnsTester = new ListBoxTester("lstSelectColumns");
            lstSelectColumnsTester.Select("1st");

            ButtonTester btnSelectWhereTester = new ButtonTester("btnSelectWhere");
            btnSelectWhereTester.Click();

            Assert.AreEqual("HouseholdMembers", selectTablesForm.sqlWhereForm.SelectedTableName);
            Assert.AreEqual(new string[] { "1st" }, selectTablesForm.sqlWhereForm.SelectedColumns);

            FormTester custSqlWhereTester = new FormTester("CustSqlWhere");
            Assert.DoesNotThrow(new TestDelegate(custSqlWhereTester.Close));
        }
    }
}