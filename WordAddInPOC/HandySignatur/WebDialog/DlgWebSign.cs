using System;
using System.Text;
using System.Windows.Forms;
using System.Net;

namespace HandySignatur
{
    public partial class DlgWebSign : Form
    {
        public string Xmlresult;
        public string Xmlrequest;
//        public string RequestURI;
        private const string ready = @"Bereit";
        private const string Busy = @"Warte auf Handy Signatur Server ...";
//        public enum RequestTarget { TestSystem = 0, ProductionSystem };
        private int _reqestTarget;
        private string _requestUrl=null;
        private const string TestUrl = @"https://test1.a-trust.at/https-security-layer-request/";
        private const string ProductionUrl = @"https://www.a-trust.at/mobile/https-security-layer-request/";

        public XMLHandySignatur.RequestTarget Target
        {
            get
            {
                return (XMLHandySignatur.RequestTarget)_reqestTarget;
            }
            set
            {
                _reqestTarget = (int)value;
                _requestUrl = TestUrl;
                if (value == XMLHandySignatur.RequestTarget.ProductionSystem)
                {
                    _requestUrl = ProductionUrl;
                }
            }
        }


        public DlgWebSign()
        {
            InitializeComponent();
        }

        private void DlgWebSign_Load(object sender, EventArgs e)
        {
            // this.DialogResult = DialogResult.Cancel;
            StringBuilder sb = new StringBuilder();
            // sb.Append(RequestURI);
            sb.Append("XMLRequest="+Uri.EscapeDataString(Xmlrequest));            
            sb.Append("&appletwidth=");
            sb.Append(wbBrHandySig.ClientRectangle.Width);
            sb.Append("&appletheight=");
            sb.Append(wbBrHandySig.ClientRectangle.Height);
            // wbBrHandySig.Navigate(sb.ToString());
            //Uri hsUri = new Uri(sb.ToString());
            //wbBrHandySig.Navigate(hsUri);
            byte[] postBytes = System.Text.Encoding.UTF8.GetBytes(sb.ToString());
            wbBrHandySig.Navigate(_requestUrl, null, postBytes, "Content-Type: application/x-www-form-urlencoded");
            StartBar();

            // wbBrHandySig.Url = new Uri(sb.ToString());
        }

        private void wbBrHandySig_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            string url = e.Url.ToString();

            if (url.IndexOf("/https-security-layer-request/response.aspx",StringComparison.InvariantCultureIgnoreCase) > 0)
            {
                WebRequest req = WebRequest.Create(url);
                WebResponse resp = req.GetResponse();

                System.IO.Stream str = resp.GetResponseStream();
                System.IO.StreamReader strRead = new System.IO.StreamReader(str, System.Text.Encoding.GetEncoding("utf-8"));
                Xmlresult = strRead.ReadToEnd();
                StopBar();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void wbBrHandySig_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            StopBar();
            string url = e.Url.ToString();

            if (url.IndexOf("/https-security-layer-request/identification.aspx",StringComparison.InvariantCultureIgnoreCase) > 0)
            {
                string handynr = "";
                string vorwahl = "";
                switch (Target)
                {
                    case XMLHandySignatur.RequestTarget.TestSystem:
                        vorwahl="10301";
                        handynr = "1122334455";
                        break;
                    case XMLHandySignatur.RequestTarget.ProductionSystem:
                        vorwahl = ""; 
                        handynr = "";
                        break;
                    default:
                        break;
                }
                HtmlElement iField = wbBrHandySig.Document.GetElementById("handynummer");
                if (iField != null)
                {
                    iField.InnerText = vorwahl+handynr;
                    //HtmlElement iSel = wbBrHandySig.Document.GetElementById("vorwahl");
                    //if (iSel != null)
                    //{
                    //    HtmlElementCollection iSelColl = iSel.Children;
                    //    foreach (HtmlElement item in iSelColl)
                    //    {
                    //        item.SetAttribute("selected", "False");
                    //        if (item.InnerText == vorwahl)
                    //        {
                    //            item.SetAttribute("selected", "True");
                    //            break;
                    //        }
                    //    }
                    //}
                }
            }
        }

        private void wbBrHandySig_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            StartBar();
        }
        private void StartBar()
        {
            tStripLabelReady.Text = Busy;
            tSProgrBar4Web.Style = ProgressBarStyle.Marquee;
            tSProgrBar4Web.MarqueeAnimationSpeed = 50;
        }
        private void StopBar()
        {
            tStripLabelReady.Text = ready;
            tSProgrBar4Web.Style = ProgressBarStyle.Continuous;
            tSProgrBar4Web.MarqueeAnimationSpeed = 0;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

    }
}
