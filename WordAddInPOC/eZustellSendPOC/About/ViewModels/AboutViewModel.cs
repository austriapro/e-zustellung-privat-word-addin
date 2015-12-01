using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using WinFormsMvvm;
using WinFormsMvvm.DialogService;
using eZustellSendPOC.About.Models;

namespace eZustellSendPOC.About.ViewModels
{
    public class AboutViewModel : ViewModelBase
    {
        private IUnityContainer _uc;
        public AboutViewModel(IUnityContainer uc, IDialogService dlgService)
            : base(dlgService)
        {
            _uc = uc;
            _OpenSourceList = new SortableBindingList<OpenSourceListModel>(AboutModel.Load().OpenSourceListModel);
        }

#region OpenSourceListModel - SortableBindingList<OpenSourceListComponent>
        private SortableBindingList<OpenSourceListModel> _OpenSourceList;
        public SortableBindingList<OpenSourceListModel> OpenSourceList
        {
            get { return _OpenSourceList; }
            set
            {
                if (_OpenSourceList==value)
                    return;
                _OpenSourceList = value;
                OnPropertyChanged();
            }
        }
#endregion

    }
}
