using System;
using System.IO;
using System.Xml;
using System.Windows.Forms;
using System.Reflection;
using System.Xml.Linq;


namespace HandySignatur
{
    // Diese Klasse dient als Parameter-Objekt für den Background Worker zum senden/empfangen des HTTP Request/Response

    public class XMLHandySignatur
    {
        private string ViewState;
        private string eventVal;
        const string Prefix_sl = "http://www.buergerkarte.at/namespaces/securitylayer/1.2#";
        const string Prefix_dsig = "http://www.w3.org/2000/09/xmldsig#";
        XNamespace _sl = Prefix_sl;
        XNamespace _dsig = Prefix_dsig;
        //public string postString { get; set; }
        // public bool rc;
        public enum RequestResult { undefined = 0, OK, Cancelled, Error };
        public RequestResult Rc { get; set; }
        public enum RequestTarget { TestSystem = 0, ProductionSystem };
        private int _ReqestTarget;
        public RequestTarget Target
        {
            get
            {
                return (RequestTarget)_ReqestTarget;
            }
            set
            {
                _ReqestTarget = (int)value;
                request_url = test_url;
                if (value == RequestTarget.ProductionSystem)
                {
                    request_url = production_url;
                }
            }
        }
        private enum ExpectedType { Html, Xml };

        private string request_url;

        /// <summary>
        /// test_url
        /// Verwendet für Requests zum Testsystem der A-Trust
        /// Test Handy Nummer im Code Behind des WebDialog Fensters
        /// Signaturkennwort: 123456789
        /// TAN: 123456
        /// </summary>
        private const string test_url = @"https://test1.a-trust.at/https-security-layer-request/";
        private const string production_url = @"https://www.a-trust.at/mobile/https-security-layer-request/";

        public XMLHandySignatur()
        {
            Target = RequestTarget.TestSystem;
        }

        /// <summary>
        /// Erstellt eine XMLDsig für das übergebene ebInterface XML Dokument 
        /// über den Handy Signatur Server der A-Trust
        /// </summary>
        /// <param name="xdoc1">
        /// zu signierendes ebInterface XML Dokument
        /// </param>
        /// <returns></returns>
        public string SignXML(XElement xdoc1)
        {
            bool preserve = true;
#if DEBUG && SAVE_HTML
           savehtml(xdoc1.InnerXml, "xdoc1AtEntry.xml");
#endif
            //XmlDocument xmlrq = CreateXmlRequest(xdoc1);
            XmlDocument xmlrq = new XmlDocument();
            xmlrq.PreserveWhitespace = true;

            string StrXmlSignReq = "";
            StrXmlSignReq = Properties.Resources.XMLSignRequest;
            xmlrq.LoadXml(StrXmlSignReq);
            XmlNamespaceManager xqrMgr = new XmlNamespaceManager(xmlrq.NameTable);

            xqrMgr.AddNamespace("sl", Prefix_sl);
            XmlNode xn = xmlrq.SelectSingleNode("//sl:XMLContent", xqrMgr);
            xn.InnerXml = xdoc1.ToString();
#if DEBUG
            // string ebInterfaceDoc = String.Empty;
            // da ist was zu tun ....

            //XmlDocument xdoc2 = new XmlDocument();
            //xdoc2.PreserveWhitespace = true;
            //xdoc2 = BeautifyXml(xdoc1);
            //savehtml(xdoc2.InnerXml, "xdoc2.xml");
            //XmlNamespaceManager ebMgr = new XmlNamespaceManager(xdoc2.NameTable);
            //ebMgr.AddNamespace("eb", InvoiceXML.PrefixURLeb);
            //ebMgr.AddNamespace("dsig", InvoiceXML.PrefixURLsig);
            //XmlNode xeb = xdoc2.SelectSingleNode("eb:Invoice", ebMgr);
            //xn.InnerXml = xeb.OuterXml;
            //string toSend = xmlrq.OuterXml;
            //savehtml(toSend, "toSend.xml");
#endif
            string xmlPage = "";
            DlgWebSign WebSign = new DlgWebSign();
            DialogResult rc;
            WebSign.Xmlrequest = xmlrq.OuterXml;
            WebSign.Target = Target;
            rc = WebSign.ShowDialog();
            if (rc == DialogResult.Cancel)
            {
                //WebSign.Dispose();
                MessageBox.Show("Handy Signatur abgebrochen.");
                return null;
            }
            xmlPage = WebSign.Xmlresult;
            //WebSign.Close();
            //WebSign.Dispose();
            if (xmlPage.IndexOf("sl:ErrorResponse") > 0)
            {
                throw new Exception("Fehler bei Handy Signatur:\r\n" + GetErrorResponse(xmlPage));
            }
            #region Signierte Rechnung zurückgeben
            XmlDocument xdoc = new XmlDocument();
            xdoc.PreserveWhitespace = preserve;
            xdoc.LoadXml(xmlPage);
            XmlElement root = xdoc.DocumentElement;
#if DEBUG && SAVE_HTML
           savehtml(root.InnerXml, "rootOfPage3.xml");
#endif
            //XmlDocument signedDoc = new XmlDocument();
            //signedDoc.PreserveWhitespace = true;
            //signedDoc.LoadXml(root.InnerXml);
#if DEBUG && SAVE_HTML
           //savehtml(signedDoc.OuterXml, "signedDOC.xml");
#endif
            #endregion

            return root.InnerXml;
        }
        private void savehtml(string toSave, string file)
        {
            const string saveDir = "Daten";
            if (!Directory.Exists(saveDir))
            {
                Directory.CreateDirectory(saveDir);
            }
            File.WriteAllText(Path.Combine(saveDir, file), toSave);
        }
        private string GetErrorResponse(string xmlerror)
        {
            string resp = String.Empty;
            XmlDocument xdoc = new XmlDocument();
            xdoc.PreserveWhitespace = true;
            xdoc.LoadXml(xmlerror);
            XmlNamespaceManager xqrMgr = new XmlNamespaceManager(xdoc.NameTable);
            string Prefix_sl = "http://www.buergerkarte.at/namespaces/securitylayer/1.2#";
            xqrMgr.AddNamespace("sl", Prefix_sl);
            XmlNode xn = xdoc.SelectSingleNode("/sl:ErrorResponse/sl:ErrorCode", xqrMgr);
            string ErrCode = "";
            if (xn != null)
            {
                ErrCode = xn.InnerText;
            }
            xn = xdoc.SelectSingleNode("/sl:ErrorResponse/sl:Info", xqrMgr);
            string Info = "";
            if (xn != null)
            {
                Info = xn.InnerText;
            }
            resp = "ErrorCode=" + ErrCode + "\r\nInfo=" + Info;
            return resp;
        }

#if DEBUG && SAVE_HTML
       public void savehtml(string html, string pagename)
       {
           StreamWriter ofile = File.CreateText(pagename);
           ofile.Write(html);
           ofile.Close();

       }
#endif
        #region Webrequests
        #endregion

        public static XmlDocument BeautifyXml(XmlDocument xdoc)
        {
            //MemoryStream aus dem Xml-Dokument erzeugen
            XmlTextWriter xmlWriter;

            StringWriter textWriter;

            // Format the Xml document with indentation and save it to a string.

            textWriter = new StringWriter();
            xmlWriter = new XmlTextWriter(textWriter);
            xmlWriter.Formatting = Formatting.Indented;
            xdoc.Save(xmlWriter);
            string xText = textWriter.ToString();
            XmlDocument beautyXML = new XmlDocument();
            beautyXML.PreserveWhitespace = true;
            beautyXML.LoadXml(xText);
#if DEBUG && SAVE_HTML
           savehtml(beautyXML.OuterXml, "BeautyXML.xml");
#endif
            return beautyXML;
        }

        /// <summary>
        /// Returns a string containg the XML Data from resource
        /// </summary>
        /// <param name="resourceName"></param>
        /// <returns></returns>
        public string GetFromXMLResource(string resourceName)
        {
            Assembly Asm = Assembly.GetExecutingAssembly();
            Stream St = Asm.GetManifestResourceStream(resourceName);
            StreamReader ResRd = new StreamReader(St);
            if (ResRd != null)
            {
                string xmlData = ResRd.ReadToEnd();
                return (xmlData);
            }

            return (null);
        }

        protected XmlDocument CreateXmlRequest(XElement xmlCsr)
        {
            //  <sl:CreateXMLSignatureRequest xmlns:sl='http://www.buergerkarte.at/namespaces/securitylayer/1.2#' xmlns:dsig='http://www.w3.org/2000/09/xmldsig#'>
            //  <sl:KeyboxIdentifier>SecureSignatureKeypair</sl:KeyboxIdentifier>
            //  <sl:DataObjectInfo Structure='detached'>
            //    <sl:DataObject Reference=''/>
            //    <sl:TransformsInfo>
            //      <dsig:Transforms>
            //        <dsig:Transform Algorithm='http://www.w3.org/2000/09/xmldsig#enveloped-signature'/>
            //      </dsig:Transforms>
            //      <sl:FinalDataMetaInfo>
            //        <sl:MimeType>text/html</sl:MimeType>
            //      </sl:FinalDataMetaInfo>
            //    </sl:TransformsInfo>
            //  </sl:DataObjectInfo>
            //  <sl:SignatureInfo>
            //    <sl:SignatureEnvironment>
            //      <sl:XMLContent>
            //      </sl:XMLContent>
            //    </sl:SignatureEnvironment>
            //    <sl:SignatureLocation Index='0'>/csr:processAuthenticationCSRRequest</sl:SignatureLocation>
            //  </sl:SignatureInfo>
            //</sl:CreateXMLSignatureRequest>

            XElement xmlRequ = new XElement(_sl + "CreateXMLSignatureRequest",
            new XAttribute(XNamespace.Xmlns + "sl", _sl),
            new XAttribute(XNamespace.Xmlns + "dsig", _dsig),
             new XElement(_sl + "KeyboxIdentifier", "SecureSignatureKeypair"),
                new XElement(_sl + "DataObjectInfo",new XAttribute("Structure", "detached"),
                    new XElement(_sl+"DataObject",new XAttribute("Reference","")), /* DataObject */
                    new XElement(_sl + "TransformsInfo",
                        new XElement(_dsig + "Transforms", new XAttribute("Algorithm", "http://www.w3.org/2000/09/xmldsig#enveloped-signature")), /* Transforms */
                        new XElement(_sl+"FinalDataMetaInfo",new XElement(_sl+"MimeType","text/html")) /* FinalDataMetaInfo */
                        ) /* TransformsInfo */
                        ), /* DataObjectInfo */
                new XElement(_sl + "SignatureInfo",
                    new XElement(_sl+"SignatureEnvironment",
                        new XElement(_sl+"XMLContent"/*, xmlCsr.ToString() */)), // SignatureEnvironment
                    new XElement(_sl+"SignatureLocation",new XAttribute("Index","0"),"/csr:processAuthenticationCSRRequest")) // SignatureInfo
            );
            
            XmlDocument xdoc = new XmlDocument();
            XmlDeclaration xDecl = xdoc.CreateXmlDeclaration("1.0", "utf-8", "no");
            xdoc.AppendChild(xDecl);
            
            //xdoc.LoadXml(xmlRequ.ToString());
            using (var xmlReader = xmlRequ.CreateReader())
            {
                xdoc.Load(xmlReader);
            }
            //XElement xelNew = new XElement(csr + "processAuthenticationCSRRequest",
            //    new XAttribute(XNamespace.Xmlns + "csr", csr),
            //    new XAttribute(XNamespace.Xmlns + "dsig", dsig),
            //    //new XElement(dsig + "Signature"),
            //    //new XElement(csr + "MandateEdid"),
            //    new XElement(csr + "validForYears"),
            //    new XElement(csr + "pkcs10CSR"));
            //return xelNew;
            return xdoc;
        }
    }


}
