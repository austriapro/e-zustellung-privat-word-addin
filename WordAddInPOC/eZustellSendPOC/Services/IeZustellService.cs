using System;
namespace eZustellSendPOC.Services
{
    /// <summary>
    /// Interface für eZustellung RemoteControl
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
    public interface IeZustellService
    {
        System.Collections.Generic.List<eZustellSendPOC.RCService.PersonDataType> GetAddressBook();
        bool IsChannelOpen();
        eZustellServiceBase.ZustellServiceRC Open();
        System.Collections.Generic.List<eZustellSendPOC.RCService.PersonDataType> SearchAddress(eZustellSendPOC.RCService.SearchAddressType searchTarget);
        System.Collections.Generic.List<eZustellSendPOC.RCService.PersonDataType> SearchOwnAddress();
        eZustellSendPOC.RCService.SendDeliveryResponse SendDelivery(eZustellSendPOC.RCService.SendDeliveryRequestType toSend);
        eZustellService.ZustellServiceRC LastRC { get; }
        string Message { get; }
        string LastFunction { get; }
        string GetMessage();
    }
}
