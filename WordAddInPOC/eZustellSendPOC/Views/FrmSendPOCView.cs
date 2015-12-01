using eZustellSendPOC.Services;
using eZustellSendPOC.ViewModels;
using SimpleEventBroker;
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
    public partial class FrmSendPOCView : FormService
    {
        //BindingSource mailBindingSource = new BindingSource() ;
        //BindingSource ViewModelBindingSource = new BindingSource();

        public FrmSendPOCView(SendPOCController viewModel)
        {
            InitializeComponent();

            ViewModel = viewModel;

            sendPOCControllerBindingSource.DataSource = ((SendPOCController)ViewModel);
            //ViewModelBindingSource.DataSource = ((SendPOCController)ViewModel).SendPocVM;
            sendPOCViewmodelBindingSource.DataSource = ((SendPOCController)ViewModel).SendPocVM;
 
            cBxReceiver.SetMemberList(((SendPOCController)ViewModel).MemberList);

            cBxReceiver.DataSource = ((SendPOCController)ViewModel).SendPocVM.ReceiverList;
            cBxReceiver.SelectedItem = ((SendPOCController)ViewModel).SendPocVM.SelectedReceiver;
            cBxReceiver.SelectedIndexChanged += base.FormService_SelectedIndexChanged;

            cBxDocumentClass.DataSource = ((SendPOCController)ViewModel).SendPocVM.DocClasses;
            cBxDocumentClass.SelectedItem = ((SendPOCController)ViewModel).SendPocVM.SelectedDocumentClass;
            cBxDocumentClass.SelectedIndexChanged += base.FormService_SelectedIndexChanged;

            cBxDeliveryQuality.DataSource = ((SendPOCController)ViewModel).SendPocVM.DeliverQualityList;
            cBxDeliveryQuality.SelectedItem = ((SendPOCController)ViewModel).SendPocVM.selectedDeliveryQuality;
            cBxDeliveryQuality.SelectedIndexChanged += base.FormService_SelectedIndexChanged;
            
            //mailBindingSource.DataSource = sendPOCViewmodelBindingSource;
            //mailBindingSource.DataMember = "ConfirmationEMailList"; // "eMailAddressList";
            cBxMailConfirm.DataSource = ((SendPOCController)ViewModel).SendPocVM.ConfirmationEMailList;


        }

        private void beendenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmSendPOCView_Shown(object sender, EventArgs e)
        {
            ((SendPOCController)ViewModel).InitCertificateCommand.Execute(null);
            if (((SendPOCController)ViewModel).IsClosing)
            {
                Close();
            }
        }


        [SubscribesTo(EventService.GetTextBoxContent)]
        public void OnGetTextBoxContent(object sender, EventArgs args)
        {
            EventService.GetTextBoxContentEventArgs arg = args as EventService.GetTextBoxContentEventArgs;
            arg.MessageText = tBxMessage.Text;
            arg.ReferenceText = tBxReference.Text;
            arg.SubjectText = tBxSubject.Text;
            arg.WebserviceUrlText = tBxCallbackUrl.Text;
        }

    }
}
