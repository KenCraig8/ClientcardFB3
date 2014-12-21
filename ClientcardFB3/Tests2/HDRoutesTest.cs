using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using NUnit.Extensions.Forms;
using Moq.AutoMock;

namespace ClientcardFB3.Tests
{
    [TestFixture]
    class HDRoutesTest
    {
        [Test]
        public void findTest(){
            AutoMocker hdRoutesMock = new AutoMocker();
            hdRoutesMock.CreateSelfMock<HDRoutes>();
            hdRoutesMock.GetMock<string>();
            hdRoutesMock.GetMock<Volunteers>();
        }
    }
}
