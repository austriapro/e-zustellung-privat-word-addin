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
    public class SaveCertificateViewModel : LoadSaveCertificateViewModel
    {

        public SaveCertificateViewModel(IUnityContainer uc, IDialogService dlgService)
            : base(uc, dlgService)
        {
            ShowConfirmPasswort = true;
            ShowNewButton = false;
            IsSaveAsCertVisible = true;
            SaveAsCert = true;
            FormTitle = "SW-Zertifikat speichern";
            LoadSaveButtonText = "Speichern";
        }
    }
}
