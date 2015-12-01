using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using WinFormsMvvm;
using WinFormsMvvm.DialogService;

namespace eZustellSendPOC.About.ViewModels
{
    public class AboutViewController : ViewModelBase
    {
        private IUnityContainer _uc;
        public AboutViewController(IUnityContainer uc, IDialogService dlgService, AboutViewModel vm)
            : base(dlgService)
        {
            _uc = uc;
            _VM = vm;
        }

        #region VM - AboutViewModel
        private AboutViewModel _VM;
        public AboutViewModel VM
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
        #endregion

    }
}
