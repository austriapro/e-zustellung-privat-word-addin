using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using WinFormsMvvm;
using WinFormsMvvm.DialogService;
using eZustellSendPOC.Services;
using WinFormsMvvm.Controls;
using WinFormsMvvm.ExtensionMethods;
using WinFormsMvvm.DialogService.FrameworkDialogs.OpenFile;
using WinFormsMvvm.DialogService.FrameworkDialogs.SaveFile;
using System.IO;

namespace eZustellSendPOC.ViewModels
{
    public class SaveCertificateController : LoadSaveCertificateController
    {
        ICertificateService _certService;
        public SaveCertificateController(IUnityContainer uc, IDialogService dlgService, ICertificateService certService)
            : base(uc, dlgService)
        {
            VM = uc.Resolve<SavePkcs12ViewModel>();
            _certService = certService;
            VM.ConfirmPasswort = "";
            VM.Passwort = "";
        }

        #region LoadSaveButton
        private RelayCommand _LoadSaveButtonCommand;
        public override RelayCommand LoadSaveButtonCommand
        {
            get
            {
                _LoadSaveButtonCommand = _LoadSaveButtonCommand ?? new RelayCommand(LoadSaveButtonClick);
                return _LoadSaveButtonCommand;
            }
        }
        private void LoadSaveButtonClick(object obj)
        {
            var certRc = _certService.SaveAsCertificate(VM.FnSwCertificate);
            if (certRc != CertificateService.CertificateRC.OK)
            {
                _dlg.ShowMessageBox(certRc.GetDescriptionFromValue());
                return;
            }
            CloseView(DialogResult.OK);
        }

        #endregion

        #region SelectCertificateFile
        private RelayCommand _SelectCertificateFileCommand;
        public override RelayCommand SelectCertificateFileCommand
        {
            get
            {
                _SelectCertificateFileCommand = _SelectCertificateFileCommand ?? new RelayCommand(SelectCertificateFileClick);
                return _SelectCertificateFileCommand;
            }
        }
        private void SelectCertificateFileClick(object obj)
        {
            var sfd = _uc.Resolve<ISaveFileDialog>();
            sfd.Filter = "Zertifikate (*.cer)|*.cer";
            if (string.IsNullOrWhiteSpace(Properties.Settings.Default.LastCertificatePath))
            {
                sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            }
            else
            {
                sfd.InitialDirectory = Properties.Settings.Default.LastCertificatePath;
            }
            var rc = _dlg.ShowSaveFileDialog(sfd);
            if (rc == DialogResult.OK)
            {
                VM.FnSwCertificate = sfd.FileName;
            }
        }
        #endregion
    }
}
