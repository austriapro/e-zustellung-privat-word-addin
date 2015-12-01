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
    /// <summary>
    /// Datamodel for FrmCertificate
    /// </summary>
    public class CertificateViewModel : ViewModelBase
    {

        private string _edId;
        /// <summary>
        /// Gets or sets the electronic delivery identifier.
        /// </summary>
        /// <value>
        /// The electronic delivery identifier.
        /// </value>
        public string EdId
        {
            get { return _edId; }
            set
            {
                if (_edId == value)
                    return;
                _edId = value.Trim();
                OnPropertyChanged();
            }
        }

        
    }
}
