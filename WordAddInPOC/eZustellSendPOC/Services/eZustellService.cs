using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eZustellSendPOC.RCService;
using eZustellSendPOC.RCSInit;
using System.ServiceModel.Channels;
using System.ServiceModel;
using SoapEncoder;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Security;
using LogService;
using System.Security;
using System.ComponentModel;
using WinFormsMvvm.ExtensionMethods;
using Microsoft.Practices.Unity;
using System.Runtime.CompilerServices;

namespace eZustellSendPOC.Services
{
    /// <summary>
    /// Class to implement the actual communication with eZustell web service
    /// per Nov.2015 verfügbare Methoden der Webservice Schnittstelle:
    /// - DeleteAddressBookEntry
    /// - GetAddressBook
    /// - GetReceivedDeliveries
    /// - GetReceivedDeliveryAttachment
    /// - GetReceivedDeliveryAttachments
    /// - GetReceivedDeliveryDetails
    /// - SearchAddress
    /// - SendDelivery
    /// </summary>
    public class eZustellService : eZustellServiceBase, IeZustellService
    {
        private bool UseTimestamp = Properties.Settings.Default.UseTimeStamp;
        private ChannelFactory<RemoteControl> channel;
        private RemoteControl client;

        //public string Message { get; private set; }
        private CertificateService _certService;
        private IUnityContainer _uc;

        public enum IDTypeEnum
        {
            gvZbPK,
            wZbPK,
            edID
        }
        public eZustellService(IUnityContainer uc, CertificateService cerServ)
            : base(uc)
        {
            _uc = uc;
            _certService = cerServ;
        }

        /// <summary>
        /// Determines whether [is channel open].
        /// </summary>
        /// <returns></returns>
        public bool IsChannelOpen()
        {
            if (channel == null)
            {
                return false;
            }
            return channel.State == CommunicationState.Opened;
        }

        /// <summary>
        /// Opens the specified function certificate client.
        /// </summary>
        /// <param name="fnCertificateClient">The Certificate of the client (SW-Zertifkat).</param>
        /// <param name="fnCertificateServer">The TLS Certificate of the Server.</param>
        /// <param name="password">The password for Certificate of the client .</param>
        /// <returns></returns>
        public ZustellServiceRC Open()
        {
            if (_certService.Certificate == null)
            {

                return ZustellServiceRC.SwCertificateNotLoaded;
            }
            return CallOpen();
        }

        /// <summary>
        /// Gets the address book.
        /// </summary>
        /// <param name="edID">The edID identifier.</param>
        /// <param name="addrBook">The address book.</param>
        /// <returns>true: Success, false: otherwise</returns>
        public List<PersonDataType> GetAddressBook()
        {
            List<PersonDataType> addrBook = new List<PersonDataType>();
            LastRC = ZustellServiceRC.OK;

            GetAddressBookRequest requ = new GetAddressBookRequest()
            {
                GetAddressBook = new RequestType()
                {
                    Edid = _certService.EdID
                }
            };
            Log.Debug("Request: {@RequestType}", CallerInfo.Create(), requ.GetAddressBook);
            var resp = CallGetAddressBook(requ);
            if (LastRC == ZustellServiceRC.OK)
            {
                addrBook = resp.AddressBook.ToList();
                Log.Debug("Response: {@PersonDataType}", CallerInfo.Create(), addrBook);
            }
            return addrBook;

        }

        /// <summary>
        /// Searches for the an address entry.
        /// </summary>
        /// <param name="searchTarget">The search target.</param>
        /// <returns>The list of PersonDataType Entries</returns>
        public List<PersonDataType> SearchAddress(SearchAddressType searchTarget)
        {
            List<PersonDataType> addrBook = new List<PersonDataType>();
            LastRC = ZustellServiceRC.OK;
            SearchAddressRequest requ = new SearchAddressRequest()
            {
                SearchAddress = searchTarget
            };
            Log.Debug("Request: {@SearchAddressType}", CallerInfo.Create(), requ.SearchAddress);

            var resp = CallSearchAddress(requ);
            if (LastRC == ZustellServiceRC.OK)
            {
                addrBook = resp.AddressBook.ToList();
                Log.Debug("Response: {@PersonDataType}", CallerInfo.Create(), addrBook);
            }
            return addrBook;

        }

        /// <summary>
        /// Searches the own address.
        /// </summary>
        /// <returns>The list of PersonDataType Entries</returns>
        public List<PersonDataType> SearchOwnAddress()
        {
            List<PersonDataType> addrBook = new List<PersonDataType>();
            LastRC = ZustellServiceRC.OK;
            var searchAddress = new SearchAddressType()
              {
                  addToAddressbook = false,
                  addToAddressbookSpecified = false,
                  Edid = _certService.EdID,
                  
              };
            List<IDType> idList= new List<IDType>();
            idList.Add(new IDType()
            { 
                Type = IDTypeEnum.edID.ToString(),
                Value = _certService.EdID
            });

            List<AbstractAddressType> addr = new List<AbstractAddressType>();
            var pers = new PersonDataType() { Item = new AbstractPersonType() };
            pers.Items = addr.ToArray();
            pers.Item.Items = idList.ToArray();
            searchAddress.Entry = pers;
            Log.Debug("Request: {@SearchAddressType}", CallerInfo.Create(), searchAddress);

            var resp = SearchAddress(searchAddress);
            if (LastRC == ZustellServiceRC.OK)
            {
                addrBook = resp;
                Log.Debug("Response: {@PersonDataType}", CallerInfo.Create(), addrBook);
            }
            return addrBook;
        }

        public SendDeliveryResponse SendDelivery(SendDeliveryRequestType toSend)
        {
            SendDeliveryRequest resp = new SendDeliveryRequest(toSend);
            LastRC = ZustellServiceRC.OK;

            toSend.Sender.Edid = _certService.EdID;
            Log.Debug("Request: {@SendDeliveryRequestType}", CallerInfo.Create(), toSend);
            SendDeliveryRequest req = new SendDeliveryRequest() { SendDelivery = toSend };
            var deliverResponse = CallSendDelivery(req);
            if (LastRC == ZustellServiceRC.OK)
            {
                Log.Debug("Response: {@SentDeliveryType}", CallerInfo.Create(), deliverResponse.SentDelivery);
                return deliverResponse;
            }
            return new SendDeliveryResponse();
        }

        /// <summary>
        /// Creates the channel.
        /// </summary>
        /// <returns>The Client Instance<see cref="RemoteControl"/></returns>
        private ZustellServiceRC createChannel()
        {

            //Setup custom binding with HTTPS + Body Signing + Soap1.1
            CustomBinding binding = new CustomBinding();
            //WSHttpBinding binding = new WSHttpBinding();
            //HTTPS Transport
            HttpsTransportBindingElement transport = new HttpsTransportBindingElement();

            //Body signing
            // MessageSecurityVersion.WSSecurity10WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11BasicSecurityProfile10
            AsymmetricSecurityBindingElement asec = (AsymmetricSecurityBindingElement)SecurityBindingElement.CreateMutualCertificateBindingElement(MessageSecurityVersion.WSSecurity10WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11BasicSecurityProfile10);
            asec.SetKeyDerivation(false);
            asec.AllowInsecureTransport = true;
            asec.IncludeTimestamp = UseTimestamp;
            asec.EnableUnsecuredResponse = true;
            asec.RequireSignatureConfirmation = false;
            asec.SetKeyDerivation(false);


            //Setup for SOAP 11 and UTF8 Encoding
            // MessageVersion.Soap11
            TextMessageEncodingBindingElement textMessageEncoding = new TextMessageEncodingBindingElement(MessageVersion.CreateVersion(EnvelopeVersion.Soap11, AddressingVersion.None), Encoding.UTF8);

            //Bind in order (Security layer, message layer, transport layer)
            binding.Elements.Add(asec);
            //binding.Elements.Add(textMessageEncoding);
            binding.Elements.Add(new SoapMessageEncodingBindingElement(textMessageEncoding));
            binding.Elements.Add(transport);

            EndpointAddress epAddr = new EndpointAddress(new Uri("https://labs1.austriapro.at:443/ZustellserviceWko/services/2015/remote-control"),
                EndpointIdentity.CreateDnsIdentity("labs2.austriapro.at"));
            channel = new ChannelFactory<RemoteControl>(binding, epAddr);

            string fingerprint = "‎‎c5 21 52 09 e4 3d 74 7d 7d af da 29 b5 07 7f 99 f1 06 ce 5b";
            int disc;
            byte[] fprint = fingerprint.GetBytes(out disc);
            //channel.Credentials.ServiceCertificate.DefaultCertificate = new X509Certificate2(@"C:\GIT\eZustellung\POC\eZustellungPlugIn\eZustellungPlugIn\Cert\Labs1AustriaPro.cer");
            channel.Credentials.ServiceCertificate.SetDefaultCertificate(StoreLocation.CurrentUser,
                StoreName.AddressBook, X509FindType.FindByThumbprint, fprint);
            channel.Credentials.ServiceCertificate.SslCertificateAuthentication = new X509ServiceCertificateAuthentication()
            {
                RevocationMode = X509RevocationMode.NoCheck,
                CertificateValidationMode = X509CertificateValidationMode.None,

            };
            channel.Credentials.ClientCertificate.Certificate = _certService.Certificate;
            //new X509Certificate2(fnCertificate, password.DecryptSecureString(seed));
            channel.Credentials.ServiceCertificate.Authentication.CertificateValidationMode =
                System.ServiceModel.Security.X509CertificateValidationMode.None;

            System.ServiceModel.Description.MustUnderstandBehavior mub = new System.ServiceModel.Description.MustUnderstandBehavior(false);
            channel.Endpoint.EndpointBehaviors.Add(mub);
            client = channel.CreateChannel();
            return ZustellServiceRC.OK;
        }

        private ZustellServiceRC CallOpen()
        {
            return CallService(() => createChannel());
        }

        private GetAddressBookResponse CallGetAddressBook(GetAddressBookRequest request)
        {
            if (channel == null || channel.State != CommunicationState.Opened)
            {
                var rc = CallOpen();
                if (LastRC != ZustellServiceRC.OK)
                {
                    return null;
                }
            }
            return CallService(() => client.GetAddressBook(request));
        }

        private SearchAddressResponse CallSearchAddress(SearchAddressRequest request)
        {
            if (channel == null || channel.State != CommunicationState.Opened)
            {
                var rc = CallOpen();
                if (LastRC != ZustellServiceRC.OK)
                {
                    return null;
                }
            }
            return CallService(() => client.SearchAddress(request));
        }

        private SendDeliveryResponse CallSendDelivery(SendDeliveryRequest request)
        {
            if (channel == null || channel.State != CommunicationState.Opened)
            {
                var rc = CallOpen();
                if (LastRC != ZustellServiceRC.OK)
                {
                    return null;
                }
            }
            return CallService(() => client.SendDelivery(request));

        }
    }
}
