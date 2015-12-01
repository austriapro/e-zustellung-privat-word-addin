using eZustellSendPOC.About.ViewModels;
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

namespace eZustellSendPOC.About.Views
{
    public partial class FrmAboutView : FormService
    {
        public FrmAboutView(AboutViewController vm)
        {
            InitializeComponent();
            ViewModel = vm;
            mCBxOpenSourceList.DataSource = ((AboutViewController)ViewModel).VM.OpenSourceList;
        }
    }
}
