using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using WinFormsMvvm;
using WinFormsMvvm.DialogService;
using WinFormsMvvm.Controls;
using eZustellSendPOC.Views;
using SimpleEventBroker;
using eZustellSendPOC.Services;
//using System.Windows.Forms;

namespace eZustellSendPOC.ViewModels
{
    public class LoadSaveCertificateController : ViewModelBase
    {
        internal IUnityContainer _uc;

        public LoadSaveCertificateController(IUnityContainer uc, IDialogService dlgService)
            : base(dlgService)
        {
            _uc = uc;
        }

        private LoadSaveCertificateViewModel _VM;
        public LoadSaveCertificateViewModel VM
        {
            get { return _VM; }
            set
            {
                if (_VM == value)
                    return;
                _VM = value;
                OnPropertyChanged();
            }
        }

        #region LoadSaveButton
        private RelayCommand _LoadSaveButtonCommand;
        public virtual RelayCommand LoadSaveButtonCommand
        {
            get
            {
                _LoadSaveButtonCommand = _LoadSaveButtonCommand ?? new RelayCommand(LoadSaveButtonClick);
                return _LoadSaveButtonCommand;
            }
        }
        private void LoadSaveButtonClick(object obj)
        {
        }

        #endregion

        #region SelectCertificateFile
        private RelayCommand _SelectCertificateFileCommand;
        public virtual RelayCommand SelectCertificateFileCommand
        {
            get
            {
                _SelectCertificateFileCommand = _SelectCertificateFileCommand ?? new RelayCommand(SelectCertificateFileClick);
                return _SelectCertificateFileCommand;
            }
        }
        private void SelectCertificateFileClick(object obj)
        {
        }
        #endregion

        #region NewCertificate
        private RelayCommand _NewCertificateCommand;
        public virtual RelayCommand NewCertificateCommand
        {
            get
            {
                _NewCertificateCommand = _NewCertificateCommand ?? new RelayCommand(NewCertificateClick);
                return _NewCertificateCommand;
            }
        }
        private void NewCertificateClick(object obj)
        {
            var rc = _dlg.ShowDialog<FrmCertificate, CertificateController>();
            if (rc == DialogResult.OK)
            {
                CloseView(rc);
            }
        }
        #endregion


        [Publishes(EventService.CertificateLoadedEvent)]
        public event EventHandler CertificateLoadedEventEvent;
        protected void CertificateLoadedEventFire()
        {
            EventHandler handler = CertificateLoadedEventEvent;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }
}
