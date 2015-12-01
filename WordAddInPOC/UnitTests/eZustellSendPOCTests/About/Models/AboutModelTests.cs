using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eZustellSendPOC.About.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace eZustellSendPOC.About.Models.Tests
{
    [TestClass()]
    public class AboutModelTests
    {
        [TestMethod()]
        public void LoadTest()
        {
            AboutModel about = AboutModel.Load();
            Assert.IsNotNull(about);
        }
    }
}
