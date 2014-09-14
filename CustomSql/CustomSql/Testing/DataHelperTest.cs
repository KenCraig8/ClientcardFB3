using System;
using NUnit.Framework;
using NUnit.Extensions.Forms;
using CustomSQL;

namespace Tests
{
    [TestFixture]
    public class DataHelperTest
    {
        [Test]
        public void enumToSqlInTest()
        {
            // Test with normal input
            System.Collections.IEnumerable inEnum = new string[]{"first", "2nd", "3rd", "4th"};
            string result = DataHelper.enumToSqlIn(inEnum);
            Assert.AreEqual("('first', '2nd', '3rd', '4th')", result);

            // Test with empty array
            inEnum = new string[] {};
            result = DataHelper.enumToSqlIn(inEnum);
            Assert.AreEqual("()", result);

            // Test with nulls
            inEnum = new string[] {null, null};
            result = DataHelper.enumToSqlIn(inEnum);
            Assert.AreEqual("()", result);

            // Test with empty string
            inEnum = new string[] { "" };
            result = DataHelper.enumToSqlIn(inEnum);
            Assert.AreEqual("('')", result);

            // Test with null and regular
            inEnum = new string[] { "first", null, "2nd", null, null, "3rd", "4th" };
            result = DataHelper.enumToSqlIn(inEnum);
            Assert.AreEqual("('first', '2nd', '3rd', '4th')", result);
        }
    }
}