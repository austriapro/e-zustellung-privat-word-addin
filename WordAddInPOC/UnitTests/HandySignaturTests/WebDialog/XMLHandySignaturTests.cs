using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandySignatur;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml;
using System.Xml.Linq;
using System.IO;

namespace HandySignatur.Tests
{
    [TestClass()]
    public class XMLHandySignaturTests
    {
        [TestMethod()]
        public void BeautifyXmlTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void CreateXmlRequest()
        {
            const string stringCsr = "<csr:processAuthenticationCSRRequest xmlns:csr=\"http://www.e-zustellung.at/namespaces/ed_remote_control_init_20150115\" xmlns:dsig=\"http://www.w3.org/2000/09/xmldsig#\">\n" +
  "<csr:validForYears>P2Y</csr:validForYears>+\n" +
  "<csr:pkcs10CSR>MIICbDCCAVQCAQAwKTEnMCUGCgmSJomT8ixkAQEMF25hbWVzcGFjZSBIYW5keVNpZ25hdHVyMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAj3Y5CcitZlRSpGsSn23NbiZrCwkmv38esg28+7sMMTg89dgDveVHl+n0plxMIgRFXMF39MIXpa+mDH8X7VZIPFbfDhLGveE9Pmx8BHB5Ocx1YmsabRIFrWF1l/y18ttUdCTSGu4W78TGLrlCQwVCdaLXGM2r+tHeBwOYFRVq8PwPfNdl9Wm4qk9N1ZVItl1QGvCZkAveAwqD/ZDlgXpQGPrBT5iDxkSw1C3AvuijveQB0WeHqFyuOetA0dj9guDFyD4EizY+5wPcp67ENdWcM5Z4udn8/4jR4QcLg22DET3SfQBqyM5FF/nhAZ3DfOv4ZUKGgaQ/L2bYdippzLTcawIDAQABMA0GCSqGSIb3DQEBCwUAA4IBAQCAgSms+6AsBK6auH/bZRSDis7DFiGqXWkLIcqd6HEjNujkFGzCr2lKGVN/MKlTAQH9OGNpsMYa4okhss4Xm+qnntAmrema91TFb3lwbaWeP3y+jHsxZZnzaly+0NrCYJmMCWEM9HE1sLa6NO7f6+RRcGKmOWn347OvT+47rEVxPCytFjq+aw4EVPdFciwitypZSfMwhCoVQ71h0IQAw3Iaqye2yuddvQcrfSQ4eATip0cj7rA6E9TtolYKjJRUCzD4FCLEjymuJ8q7JIlCn3kOt3cYMhOifwPXTDawKO3UIyR3WVMsyhinEUP4ik2hBDmGZPIqRW6kSk5ipiFmOrt8</csr:pkcs10CSR>\n" +
  "</csr:processAuthenticationCSRRequest>";
            const string fn = @"Data\Xmlrequ.xml";
            //XmlDocument xmlCsr = new XmlDocument();
            //xmlCsr.LoadXml(stringCsr);
            XElement xmlCsr = XElement.Parse(stringCsr);
            XMLHandySignaturWrapper xmlSign = new XMLHandySignaturWrapper();
            XmlDocument xmlRequ = xmlSign.CreateXmlRequest(xmlCsr);
            if (!Directory.Exists("Data"))
            {
                Directory.CreateDirectory("Data");
            }
            xmlRequ.Save(fn);
            Assert.IsNotNull(xmlRequ);
        }
    }

}

namespace HandySignatur
{
    public class XMLHandySignaturWrapper : XMLHandySignatur
    {

        public XmlDocument CreateXmlRequest(XElement xmlCsr)
        {
            return base.CreateXmlRequest(xmlCsr);
        }
    }
}
