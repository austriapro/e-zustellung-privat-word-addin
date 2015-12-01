using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eZustellSendPOC.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using eZustellSendPOCTests;
using Microsoft.Practices.Unity.InterceptionExtension;
using Microsoft.Practices.Unity;


namespace eZustellSendPOC.Services.Tests
{
    [TestClass()]
    public class RegistrationServiceTests : TestBase
    {
        [TestMethod()]
        public void GetCertificateTest()
        {
            //const string fnCSR = @"C:\GIT\eZustellung\TestDaten\TestDatenSammlung\TestDatenSammlung\Daten\eZustellPOC\CSR-Request.xml";
            const string fnCSR = @"C:\GIT\eZustellung\TestDaten\TestDatenSammlung\TestDatenSammlung\Daten\eZustellPOC\CSR-Request.bin";

            byte[] csr = File.ReadAllBytes(fnCSR);
            var _regService = uc.Resolve<IRegistrationService>();
            byte[] bincert = _regService.GetCertificate(csr);

            Assert.AreEqual( CertificateService.CertificateRC.OK,_regService.LastRC);
        }
    }
}
