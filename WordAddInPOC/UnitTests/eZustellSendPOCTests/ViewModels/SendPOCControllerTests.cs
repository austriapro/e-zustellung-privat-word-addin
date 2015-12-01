using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eZustellSendPOC.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using eZustellSendPOCTests;
using Microsoft.Practices.Unity;
using eZustellSendPOC.RCService;
using LogService;

namespace eZustellSendPOC.ViewModels.Tests
{
    [TestClass()]
    public class SendPOCControllerTests : TestBase
    {
        [TestMethod()]
        public void SendPOCControllerTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void OnCertificateLoadedEventTest()
        {
            SendPOCController ctrl = uc.Resolve<SendPOCController>();
            Assert.Fail();
        }

        [TestMethod]
        public void SendPOCCreateDeliveryRequestTest()
        {
            var vm = uc.Resolve<SendPOCViewmodel>();
            vm.SelectedReceiver = new ReceiverViewModel()
            {
                edID = "Testedid"
            };
            vm.MessageText = "Testnachricht" + Environment.NewLine + "noch eine Zeile";
            vm.selectedDeliveryQuality = vm.DeliverQualityList[1];
            vm.SelectedDocumentClass = vm.DocClasses[1];
            vm.Subject = "Betrifft den Test";
            vm.Reference = "Aktenzeichen XY";
            string fn = @"C:\Users\jbogad\Documents\Adventkonzert_2015_Einladung.pdf";
            vm.AttachmentFileName = fn;
            vm.LockDeliveryUntil = true;
            vm.LockedUtilTime = new DateTime(1, 1, 1, 12, 00, 00);
            vm.LockedUntilDateTime = DateTime.Today.AddDays(30);
            SendDeliveryRequestType requ = vm;

            Assert.IsNotNull(requ);
        }

    }
}
