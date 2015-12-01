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
using System.Xml.Linq;
using eZustellSendPOC.Services;
using HandySignatur;
using LogService;
using WinFormsMvvm.ExtensionMethods;
using System.Diagnostics;
using System.Windows.Forms;

namespace eZustellSendPOC.ViewModels
{
    /// <summary>
    /// Controller for FrmCertificate
    /// </summary>
    public class CertificateController : ViewModelBase
    {
        private IUnityContainer _uc;
        private ICertificateService _certService;
        private IRegistrationService _eZService;


        private CertificateViewModel _vm;
        /// <summary>
        /// Gets or sets the Viewmodel.
        /// </summary>
        /// <value>
        /// The Viewmodel Instance.
        /// </value>
        public CertificateViewModel Vm
        {
            get { return _vm; }
            set
            {
                if (_vm == value)
                    return;
                _vm = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CertificateController"/> class.
        /// </summary>
        /// <param name="uc">The uc.</param>
        /// <param name="dlgService">The dialog service.</param>
        /// <param name="vm">The vm.</param>
        public CertificateController(IUnityContainer uc,
                                     IDialogService dlgService,
                                     CertificateViewModel vm,
                                     IRegistrationService eZServ,
            ICertificateService certService)
            : base(dlgService)
        {
            _uc = uc;
            _vm = vm;
            _eZService = eZServ;
            _certService = certService;
        }

        #region CreateCertificate
        private RelayCommand _CreateCertificateCommand;
        /// <summary>
        /// Gets the create certificate command.
        /// </summary>
        /// <value>
        /// The create certificate command.
        /// </value>
        public RelayCommand CreateCertificateCommand
        {
            get
            {
                _CreateCertificateCommand = _CreateCertificateCommand ?? new RelayCommand(CreateCertificateClick);
                return _CreateCertificateCommand;
            }
        }
        /// <summary>
        /// Creates the certificate click.
        /// </summary>
        /// <param name="obj">The object.</param>
        private void CreateCertificateClick(object obj)
        {
            if (string.IsNullOrWhiteSpace(Vm.EdId))
            {
                _dlg.ShowMessageBox("Die edID darf nicht leer sein");
                return;
            }
            var binCert = _certService.GenerateCertificateAndRegister(Vm.EdId);
            if (binCert != CertificateService.CertificateRC.OK)
            {
                _dlg.ShowMessageBox(binCert.GetDescriptionFromValue());
                CloseView(DialogResult.Abort);
                return;
            }
            var rc = _dlg.ShowDialog<FrmLoadSaveCertificate, SavePkcs12Controller>();

            CloseView(rc);
        }
        #endregion
    }
}
