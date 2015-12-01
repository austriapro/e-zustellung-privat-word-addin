using System;

namespace eZustellSendPOC.Services
{
    public interface IRegistrationService
    {
        byte[] GetCertificate(byte[] binCsr);
        eZustellSendPOC.Services.eZustellServiceBase.ZustellServiceRC Open();
        eZustellService.ZustellServiceRC LastRC { get; }

    }
}
