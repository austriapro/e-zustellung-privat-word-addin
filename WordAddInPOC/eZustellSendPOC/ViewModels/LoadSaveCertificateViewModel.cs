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
    public class LoadSaveCertificateViewModel : ViewModelBase
    {
        private IUnityContainer _uc;
        public LoadSaveCertificateViewModel(IUnityContainer uc, IDialogService dlgService)
            : base(dlgService)
        {
            _uc = uc;
        }

        private string _FormTitle;
        public string FormTitle
        {
            get { return _FormTitle; }
            set
            {
                if (_FormTitle == value)
                    return;
                _FormTitle = value;
                OnPropertyChanged();
            }
        }

        private string _LoadSaveButtonText;
        public string LoadSaveButtonText
        {
            get { return _LoadSaveButtonText; }
            set
            {
                if (_LoadSaveButtonText == value)
                    return;
                _LoadSaveButtonText = value;
                OnPropertyChanged();
            }
        }


        private string _fnSwCertificate;
        public string FnSwCertificate
        {
            get { return _fnSwCertificate; }
            set
            {
                if (_fnSwCertificate == value)
                    return;
                _fnSwCertificate = value;
                OnPropertyChanged();
                OnPropertyChanged("LoadSaveEnabled");
            }
        }

        //private bool _LoadSaveEnabled;
        public bool LoadSaveEnabled
        {
            get
            {
                bool res = (!string.IsNullOrWhiteSpace(FnSwCertificate))&&!(string.IsNullOrWhiteSpace(Passwort));
                return res;
            }
            //set
            //{
            //    if (_LoadSaveEnabled == value)
            //        return;
            //    _LoadSaveEnabled = value;
            //    OnPropertyChanged();
            //}
        }

        private bool _IsSaveAsCertVisible;
        public bool IsSaveAsCertVisible
        {
            get { return _IsSaveAsCertVisible; }
            set
            {
                if (_IsSaveAsCertVisible == value)
                    return;
                _IsSaveAsCertVisible = value;
                OnPropertyChanged();
            }
        }

        private bool _SaveAsCert;
        public bool SaveAsCert
        {
            get { return _SaveAsCert; }
            set
            {
                if (_SaveAsCert == value)
                    return;
                _SaveAsCert = value;
                OnPropertyChanged();
            }
        }


        private string _ConfirmPasswort;
        public string ConfirmPasswort
        {
            get { return _ConfirmPasswort; }
            set
            {
                if (_ConfirmPasswort == value)
                    return;
                _ConfirmPasswort = value;
                OnPropertyChanged();
            }
        }

        private string _Passwort;
        public string Passwort
        {
            get { return _Passwort; }
            set
            {
                if (_Passwort == value)
                    return;
                _Passwort = value;
                OnPropertyChanged();
                OnPropertyChanged("LoadSaveEnabled");
            }
        }


        private bool _ShowNewButton;
        public bool ShowNewButton
        {
            get { return _ShowNewButton; }
            set
            {
                if (_ShowNewButton == value)
                    return;
                _ShowNewButton = value;
                OnPropertyChanged();
            }
        }

        private bool _ShowConfirmPasswort;
        public bool ShowConfirmPasswort
        {
            get { return _ShowConfirmPasswort; }
            set
            {
                if (_ShowConfirmPasswort == value)
                    return;
                _ShowConfirmPasswort = value;
                OnPropertyChanged();
            }
        }

    }
}
