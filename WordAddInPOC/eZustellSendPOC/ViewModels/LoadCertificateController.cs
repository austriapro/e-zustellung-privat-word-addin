using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using WinFormsMvvm;
using WinFormsMvvm.DialogService;
using WinFormsMvvm.Controls;
using WinFormsMvvm.DialogService.FrameworkDialogs.OpenFile;
using WinFormsMvvm.ExtensionMethods;
//using System.Windows.Forms;

using eZustellSendPOC.Services;
using System.IO;

namespace eZustellSendPOC.ViewModels
{
    public class LoadCertificateController : LoadSaveCertificateController
    {
        public LoadCertificateController(IUnityContainer uc, IDialogService dlgService)
            : base(uc, dlgService)
        {
            VM = _uc.Resolve<LoadCertificateViewModel>();

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
                CloseView(DialogResult.Cancel);
                return;
            }
            if (string.IsNullOrWhiteSpace(VM.FnSwCertificate))
            {
                _dlg.ShowMessageBox("Zertifikat nicht ausgewählt.");
                CloseView(DialogResult.Cancel);
                return;
            }
            var certService = _uc.Resolve<ICertificateService>();
            var certRc = certService.Load(VM.FnSwCertificate, VM.Passwort);
            if (certRc != CertificateService.CertificateRC.OK)
            {
                _dlg.ShowMessageBox(certRc.GetDescriptionFromValue());
                CloseView(DialogResult.Cancel);
                return;
            };
            CertificateLoadedEventFire();
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
            var ofd = _uc.Resolve<IOpenFileDialog>();
            ofd.Filter = "Zertifikate (*.pfx, *.p12)|*.pfx; *.p12";
            if (string.IsNullOrWhiteSpace(Properties.Settings.Default.LastCertificatePath))
            {
                ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            }
            else
            {
                ofd.InitialDirectory = Properties.Settings.Default.LastCertificatePath;
            }
            ofd.Multiselect = false;
            var rc = _dlg.ShowOpenFileDialog(ofd);
            if (rc == DialogResult.OK)
            {
                Properties.Settings.Default.LastCertificatePath = Path.GetDirectoryName(ofd.FileName);
                Properties.Settings.Default.Save();
                VM.FnSwCertificate = ofd.FileName;
            }
        }
        #endregion
    }
}
