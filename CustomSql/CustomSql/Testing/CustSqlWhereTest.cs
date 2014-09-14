using System;
using NUnit.Framework;
using NUnit.Extensions.Forms;
using Moq;
using CustomSQL;
using System.Data;

namespace Tests
{
    [TestFixture]
    public class CustSqlWhereTest
    {
        /// <summary>
        /// Tests that the query is correct and that the correct value is returned.
        /// </summary>
        [Test]
        public void getColsOfTypeTest()
        {
            var dataMock = new Mock<DataHelper>();
            string[] columnsList = new string[] { "1st", "2nd" };
            DataTable outTable = new DataTable();
            outTable.Columns.Add("COLUMN_NAME");
            foreach (string col in columnsList)
            {
                outTable.Rows.Add(new string[] { col });
            }
            string queryToMock = "SELECT COLUMN_NAME FROM [ClientCardFB3].information_schema.columns WHERE TABLE_NAME = 'HouseholdMembers' AND (DATA_TYPE IN ('int')) AND (COLUMN_NAME = '1st' OR COLUMN_NAME = '2nd')";
            dataMock.Setup(_ => _.sqlSelectQuery(It.IsAny<string>(), queryToMock)).Returns(outTable);

            CustSqlWhere sqlWhere = new CustSqlWhere(dataMock.Object, "", new string[]{"1st", "2nd"}, "ClientCardFB3", "HouseholdMembers");
            string[] columnAct = sqlWhere.getColsOfType("(DATA_TYPE IN ('int'))");

            Assert.AreEqual(columnsList, columnAct);
        }

        /// <summary>
        /// Tests that the query is correct and that the correct value is returned.
        /// </summary>
        [Test]
        public void getDistinctValsInColTest()
        {
            var dataMock = new Mock<DataHelper>();
            DataTable outTable = new DataTable();
            outTable.Columns.Add("LastName");
            outTable.Columns.Add("FirstName");
            string queryToMock = "SELECT DISTINCT LastName FROM [ClientcardFB3].[dbo].[HouseholdMembers]";
            dataMock.Setup(_ => _.sqlSelectQuery(It.IsAny<string>(), queryToMock)).Returns(outTable);

            CustSqlWhere sqlWhere = new CustSqlWhere(dataMock.Object, "", new string[] { "1st", "2nd" }, "ClientCardFB3", "HouseholdMembers");
            string[] columnAct = sqlWhere.getDistinctValsInCol("LastName");

            Assert.AreEqual(new string[]{"LastName"}, columnAct);
        }
    }
}