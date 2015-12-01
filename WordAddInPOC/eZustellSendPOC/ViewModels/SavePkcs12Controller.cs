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
using System.Windows.Forms;
using WinFormsMvvm.DialogService.FrameworkDialogs.OpenFile;
using WinFormsMvvm.DialogService.FrameworkDialogs.SaveFile;
using System.IO;

namespace eZustellSendPOC.ViewModels
{
    public class SavePkcs12Controller : LoadSaveCertificateController
    {
        ICertificateService _certService;
        public SavePkcs12Controller(IUnityContainer uc, IDialogService dlgService, ICertificateService certService)
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
            if (string.IsNullOrWhiteSpace(VM.Passwort))
            {
                _dlg.ShowMessageBox("Passwort nicht angegeben.");
                return;
            }
            if (VM.Passwort != VM.ConfirmPasswort)
            {
                _dlg.ShowMessageBox("Passwort und Passwort Bestätigung sind unterschiedlich");
                return;
            }
            if (string.IsNullOrWhiteSpace(VM.FnSwCertificate))
            {
                _dlg.ShowMessageBox("Zertifikat nicht ausgewählt.");
                return;
            }
            var certRc = _certService.Save(VM.FnSwCertificate, VM.Passwort);
            if (certRc != CertificateService.CertificateRC.OK)
            {
                _dlg.ShowMessageBox(certRc.GetDescriptionFromValue());
                return;
            }
            CertificateLoadedEventFire();
            int i = VM.Passwort.Length;
            VM.Passwort = new String('*', i);
            VM.ConfirmPasswort = new String('*', i);
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
            sfd.Filter = "PKCS12-Zertifikate (*.pfx, *.p12)|*.pfx; *.p12";
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
                Properties.Settings.Default.LastCertificatePath = Path.GetDirectoryName(sfd.FileName);
                Properties.Settings.Default.Save();
            }
        }
        #endregion
    }
}
