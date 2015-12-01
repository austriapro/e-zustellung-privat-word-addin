using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using WinFormsMvvm;
using WinFormsMvvm.DialogService;

namespace eZustellSendPOC.ViewModels
{
    public class LoadCertificateViewModel : LoadSaveCertificateViewModel
    {
        public LoadCertificateViewModel(IUnityContainer uc, IDialogService dlgService)
            : base(uc, dlgService)
        {
            ShowConfirmPasswort = false;
            ShowNewButton = true;
            FormTitle = "SW-Zertifikat laden";
            LoadSaveButtonText = "Laden";
        }

    }
}
