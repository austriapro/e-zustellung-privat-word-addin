using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using WinFormsMvvm;
using WinFormsMvvm.DialogService;
using WinFormsMvvm.ExtensionMethods;
using System.ServiceModel;
using System.ComponentModel;
using LogService;
using eZustellSendPOC.RCService;
using System.Runtime.CompilerServices;


namespace eZustellSendPOC.Services
{
    public class eZustellServiceBase 
    {
        public enum ZustellServiceRC
        {
            [Description("Erfolgreich")]
            OK,
            [Description("Verbindung zu eZustellung nicht geöffnet")]
            NotOpen,
            [Description("SW-Zertifikat nicht geladen")]
            SwCertificateNotLoaded,
            [Description("Verbindung zu eZustellung wurde wg. Zeitüberschreitung abgebrochen:")]
            ConnectionTimeOut,
            [Description("Der Server hat einen Fehler gemeldet")]
            FaultFromServer,
            [Description("Es ist ein Kommunikationsfehler aufgetreten")]
            CommunicationException,
            [Description("Fehler in der Kommunikation")]
            OtherException,
            [Description("Endpoint Konfiguration nicht gefunden")]
            NoEndpointConfigurationFound

        }
        public ZustellServiceRC LastRC { get; internal set; }
        public string Message { get; internal set; }
        public string LastFunction { get; internal set; }


        private IUnityContainer _uc;
        private IDialogService _dlg;
        public eZustellServiceBase(IUnityContainer uc)
        {
            _uc = uc;
            _dlg = _uc.Resolve<IDialogService>();
        }

        public string GetMessage()
        {
            return LastRC.GetDescriptionFromValue() + Environment.NewLine + Message;
        }

        internal T CallService<T>(Func<T> function)
        {
            LastRC = ZustellServiceRC.OK;
            Message = "";
            _dlg.SetWaitCursor();
            try
            {
                return function();
            }
            catch (TimeoutException timeEx)
            {
                LastRC = ZustellServiceRC.ConnectionTimeOut;
                Log.Error(timeEx, CallerInfo.Create(), LastRC.GetDescriptionFromValue());
                return default(T);
            }
            catch (FaultException<RCSInit.FaultType> faultEx)
            {
                LastRC = ZustellServiceRC.FaultFromServer;
                Message = string.Format("{0}, Reason={1}", faultEx.Detail.Message,faultEx.Detail.Reason);
                Log.Error(faultEx, CallerInfo.Create(), LastRC.GetDescriptionFromValue());
                return default(T);

            }
            catch (FaultException<FaultType> faultEx)
            {
                LastRC = ZustellServiceRC.FaultFromServer;
                Message = string.Format("{0}, Reason={1}", faultEx.Detail.Message, faultEx.Detail.Reason);
                Log.Error(faultEx, CallerInfo.Create(), LastRC.GetDescriptionFromValue());
                return default(T);

            }
            catch (CommunicationException commsEx)
            {
                Message = string.Format("{0}", commsEx.Message);
                LastRC = ZustellServiceRC.CommunicationException;
                Log.Error(commsEx, CallerInfo.Create(), LastRC.GetDescriptionFromValue());
                return default(T);
            }
            catch (Exception ex)
            {
                Message = string.Format("{0}", ex.Message);
                LastRC = ZustellServiceRC.OtherException;
                Log.Error(ex, CallerInfo.Create(), LastRC.GetDescriptionFromValue());
                return default(T);
            }
            finally
            {
                _dlg.ResetCursor();
            }
        }
    }
}
