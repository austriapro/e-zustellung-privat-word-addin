using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Practices.Unity;
using LogService;
using eZustellSendPOC.Services;
using eZustellSendPOC.Views;
using eZustellSendPOC.ViewModels;
using eZustellSendPOC.Models;
using WinFormsMvvm.DialogService.ContextService;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using WinFormsMvvm.DialogService.FrameworkDialogs.SaveFile;
using WinFormsMvvm.DialogService.FrameworkDialogs.OpenFile;
using WinFormsMvvm.DialogService.FrameworkDialogs.FolderBrowse;
using WinFormsMvvm.DialogService;
using Microsoft.Practices.Unity.InterceptionExtension;
using eZustellSendPOCTests;

namespace eZustellSendPOC.Tests
{
    [TestClass()]
    public class CertificateServiceTests : TestBase
    {

        [TestMethod()]
        public void LoadCertificateTest()
        {
            ICertificateService certServ = uc.Resolve<ICertificateService>();
            string fn = @"C:\GIT\eZustellung\TestDaten\TestDatenSammlung\TestDatenSammlung\Daten\eZustellPOC\cert.pfx";
            string pwd = "password";
            var certRc = certServ.Load(fn, pwd);
            Assert.AreEqual(CertificateService.CertificateRC.OK, certRc);
            
        }
    }
}
