using eZustellSendPOC.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsMvvm.DialogService;

namespace eZustellSendPOC.Views
{
    public partial class FrmCertificate : FormService
    {
        public FrmCertificate(CertificateController viewModel) : base(viewModel)
        {
            InitializeComponent();
            certificateControllerBindingSource.DataSource = (CertificateController)ViewModel;
            certificateViewModelBindingSource.DataSource = ((CertificateController)ViewModel).Vm;
        }

        private void commandButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void commandButton2_Click(object sender, EventArgs e)
        {

        }
    }
}
