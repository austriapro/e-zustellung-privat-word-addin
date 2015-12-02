using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eZustellSendPOC.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using eZustellSendPOCTests;
using Microsoft.Practices.Unity;
using eZustellSendPOC.ViewModels;
using LogService;

namespace eZustellSendPOC.Services.Tests
{
    [TestClass()]
    public class eZustellServiceTests : TestBase
    {
        List<RCService.PersonDataType> addrBook = new List<RCService.PersonDataType>();
        ICertificateService certServ;
        [TestMethod()]
        public void GetAddressBookTest()
        {
            var rc = LoadSwCertificate();
            Assert.AreEqual(CertificateService.CertificateRC.OK, rc);
            var ezServ = uc.Resolve<IeZustellService>();
            addrBook = ezServ.GetAddressBook();
            Assert.AreEqual(eZustellService.ZustellServiceRC.OK, ezServ.LastRC);
        }

        private CertificateService.CertificateRC LoadSwCertificate()
        {
            certServ = uc.Resolve<ICertificateService>();
            string fnCert = @"C:\GIT\eZustellung\TestDaten\TestDatenSammlung\TestDatenSammlung\Daten\eZustellPOC\jb_labs1.pfx";
            var rc = certServ.Load(fnCert, "password");
            return rc;
        }

        [TestMethod]
        public void GetReceiverFromAddressBook()
        {
            GetAddressBookTest();
            var receiver = ReceiverViewModel.GetReceiverFromAddressBook(addrBook,false);
            Assert.IsNotNull(receiver);
        }

        [TestMethod()]
        public void SearchOwnAddressTest()
        {
            var rc = LoadSwCertificate();
            Assert.AreEqual(CertificateService.CertificateRC.OK, rc);
            var ezServ = uc.Resolve<IeZustellService>();
            var resp = ezServ.SearchOwnAddress();
            Assert.AreEqual(eZustellService.ZustellServiceRC.OK, ezServ.LastRC);
            var me = ReceiverViewModel.GetReceiverFromAddressBook(resp,false);
            Log.Information("@ReceiverViewModel", CallerInfo.Create(), me);
            Assert.IsNotNull(me);

        }
    }
}
