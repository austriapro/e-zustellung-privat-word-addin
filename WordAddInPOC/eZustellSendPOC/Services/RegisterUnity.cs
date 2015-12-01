using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eZustellSendPOC.Models;
using eZustellSendPOC.Services;
using eZustellSendPOC.ViewModels;
using eZustellSendPOC.Views;
using LogService;
using EventBrokerExtension;
using eZustellSendPOC.About.Views;
using eZustellSendPOC.About.ViewModels;

namespace eZustellSendPOC.Services
{
    public static class RegisterUnity
    {
        public static void RegisterContainer(IUnityContainer uc)
        {
            Log.Information("Begin Unity registration", CallerInfo.Create(), "");
            uc.AddNewExtension<SimpleEventBrokerExtension>();
            uc.RegisterType<FrmCertificate>();
            uc.RegisterType<SendPOCController>();
            uc.RegisterType<SendPOCViewmodel>();
            uc.RegisterType<CertificateController>();
            uc.RegisterType<CertificateViewModel>();
            uc.RegisterType<IAddressBookModel, AddressBookModel>();
            uc.RegisterType<eZustellServiceBase>(new ContainerControlledLifetimeManager());
            uc.RegisterType<IeZustellService, eZustellService>(new ContainerControlledLifetimeManager());
            uc.RegisterType<IRegistrationService, RegistrationService>(new ContainerControlledLifetimeManager());
            uc.RegisterType<ICertificateService, CertificateService>(new ContainerControlledLifetimeManager());
            uc.RegisterType<FrmLoadSaveCertificate>();
            uc.RegisterType<LoadCertificateController>();
            uc.RegisterType<LoadCertificateViewModel>();
            uc.RegisterType<LoadSaveCertificateController>();
            uc.RegisterType<LoadSaveCertificateViewModel>();
            uc.RegisterType<SavePkcs12Controller>();
            uc.RegisterType<SavePkcs12ViewModel>();
            uc.RegisterType<SaveCertificateViewModel>();
            uc.RegisterType<SaveCertificateController>();
            uc.RegisterType<FrmAboutView>();
            uc.RegisterType<AboutViewController>();
            uc.RegisterType<AboutViewModel>();

        }
    }
}
