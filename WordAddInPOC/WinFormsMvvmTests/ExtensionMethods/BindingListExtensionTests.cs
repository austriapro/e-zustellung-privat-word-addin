using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsMvvm.ExtensionMethods;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel;


namespace WinFormsMvvm.ExtensionMethods.Tests
{
    [TestClass()]
    public class BindingListExtensionTests
    {
        [TestMethod()]
        public void AddRangeTest()
        {
            BindingList<string> b1 = new BindingList<string>() { "a", "b" };
            BindingList<string> b2 = new BindingList<string>();
            b2.AddRange(b1);
            Assert.AreEqual(2, b2.Count);
        }
    }
}
