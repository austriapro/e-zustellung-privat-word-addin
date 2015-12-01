using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eZustellSendPOC.Services
{
    /// <summary>
    /// All static definitions for the event broker
    /// </summary>
    public static class EventService
    {
        public const string CertificateLoadedEvent = "CertificateLoadedEvent";

        public const string GetTextBoxContent = "GetTextBoxContent";

        public class GetTextBoxContentEventArgs : EventArgs
        {
            public string MessageText { get; set; }
            public string SubjectText { get; set; }
            public string ReferenceText { get; set; }
            public string WebserviceUrlText { get; set; }
        }
    }
}
