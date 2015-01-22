using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using NUnit.Extensions.Forms;
using System.Windows.Forms;
using System.Data;
using UnitTestingTools = Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClientcardFB3.Tests
{
    [TestFixture]
    class HDPlannerFormTest
    {
        [Test]
        public void FillFilterByComboTest()
        {
            LoginFormFake loginFormFake = new LoginFormFake();
            Mock<MainForm> mainFormMock = new Mock<MainForm>(loginFormFake);
            Mock<HDRoutes> hdRoutesMock = new Mock<HDRoutes>("");
            Mock<EditVolunteerForm> voulenteerFormMock = new Mock<EditVolunteerForm>("");

            //TODO: Error here with main form. Replace the main form used with a fake, similar to the login form
            HDPlannerForm planForm = new HDPlannerForm(mainFormMock.Object, hdRoutesMock.Object, voulenteerFormMock.Object);

            ComboBox cbo = new ComboBox();

            // Set up the data table with information to test the function
            DataTable inTable = new DataTable();
            inTable.Columns.Add("COLUMN_NAME");
            string[] itemsList = new string[] { "1st", "2nd", "something", "item" };
            foreach (string item in itemsList)
            {
                inTable.Rows.Add(new string[] { item });
            }

            ComboBox outCombo = new ComboBox();

            UnitTestingTools.PrivateObject privPlanForm = new UnitTestingTools.PrivateObject(planForm);
            privPlanForm.Invoke("FillFilterByCombo", new object[] {inTable, outCombo});

            string[] outputItems = new string[itemsList.Length];
            outCombo.Items.CopyTo(outputItems, 0);

            Assert.AreEqual(itemsList, outputItems);
        }
     /*   [Test]
        public void FillFilterByComboTest()
        {
            AutoMocker planFormMoq = new AutoMocker();
            HDPlannerForm planForm = planFormMoq.CreateInstance<HDPlannerForm>();
            planFormMoq.GetMock<MainForm>();
            planFormMoq.GetMock<HDRoutes>();
            planFormMoq.GetMock<EditVolunteerForm>();

            // planForm.Fi
            //HDPlannerForm planForm = new HDPlannerForm(new MainForm(new LoginForm()), new HDRoutes("", new Volunteers("")), new EditVolunteerForm(""));
        
            // init
            mocker = new AutoMoqer();
 
            // mock a Banana Cake Factory
            var cakeFac = mocker.Create<BananaCakeFactory>();
 
            // inject our IRepo
            mocker.GetMock<IBananaRepo>();
 
            // do work
            var result = cakeFac.Bake();
        }*/
    }
}
