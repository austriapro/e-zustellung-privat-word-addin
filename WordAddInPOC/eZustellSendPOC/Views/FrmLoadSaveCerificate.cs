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
using eZustellSendPOC.ViewModels;

namespace eZustellSendPOC.Views
{
    public partial class FrmLoadSaveCertificate : FormService
    {
        public FrmLoadSaveCertificate(LoadSaveCertificateController viewModel)
            : base(viewModel)
        {
            InitializeComponent();
            loadSaveCertificateControllerBindingSource.DataSource = (LoadSaveCertificateController)ViewModel;
            loadSaveCertificateViewModelBindingSource.DataSource = ((LoadSaveCertificateController)ViewModel).VM;
            
        }

        private void commandButton2_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        private void commandButton3_Click(object sender, EventArgs e)
        {
            //if (((LoadSaveCertificateController)ViewModel).IsClosing)
            //{
            //    this.DialogResult = ((LoadSaveCertificateController)ViewModel).DialogRC != null ? ((LoadSaveCertificateController)ViewModel).DialogRC.DialogResult : this.DialogResult;
            //    Close();
            //}
            return;
        }
    }
}
