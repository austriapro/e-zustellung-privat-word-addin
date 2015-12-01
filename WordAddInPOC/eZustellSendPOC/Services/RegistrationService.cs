using eZustellSendPOC.RCSInit;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using LogService;

namespace eZustellSendPOC.Services
{
    public class RegistrationService : eZustellServiceBase, eZustellSendPOC.Services.IRegistrationService
    {
        private ChannelFactory<RemoteControlInit> iniChannel;
        private RemoteControlInit iniClient;

        public RegistrationService(IUnityContainer uc)
            : base(uc)
        {

        }

        private ZustellServiceRC createInitChannel()
        {
            EndpointAddress ep = new EndpointAddress("https://labs1.austriapro.at:443/ZustellserviceWko/services/2015/remote-control-init");
            iniChannel = new ChannelFactory<RemoteControlInit>("RemoteControlInitPort");
            if (iniChannel==null)
            {
                return ZustellServiceRC.NoEndpointConfigurationFound;
            }
            iniClient = iniChannel.CreateChannel();
            return ZustellServiceRC.OK;
        }

        public byte[] GetCertificate(byte[] binCsr)
        {
            //ZustellServiceRC rc;
            registerRequest request = new registerRequest(binCsr);
            if (iniChannel == null)
            {
                LastRC = CallOpenInit();
                if (LastRC != ZustellServiceRC.OK)
                {
                    Log.Error("Open Channel fehlgeschlagen: rc={LastRC}", CallerInfo.Create(), LastRC);
                    return null;
                }
            }
            var resp = CallGetCertificate(request);
            if (LastRC!= ZustellServiceRC.OK)
            {
                Log.Error("GetCertificate fehlgeschlagen: rc={LastRC}",CallerInfo.Create(), LastRC);
                return null;
            }
            return resp.processAuthenticationCSRResponse;
        }

        public ZustellServiceRC Open()
        {
            if (iniChannel!=null && iniChannel.State== CommunicationState.Opened)
            {
                LastRC = ZustellServiceRC.OK;
                return ZustellServiceRC.OK;
            }
            return CallOpenInit();
        }

        private registerResponse CallGetCertificate(registerRequest request)
        {
            return CallService(() => iniClient.register(request));
        }

        private ZustellServiceRC CallOpenInit()
        {
            return CallService(() => createInitChannel());
        }

    }
}
