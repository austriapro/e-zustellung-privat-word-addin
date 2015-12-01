using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using WinFormsMvvm;
using WinFormsMvvm.DialogService;
using WinFormsMvvm.Controls;
using LogService;
using eZustellSendPOC.Views;
using WinFormsMvvm.DialogService.FrameworkDialogs.OpenFile;
using System.Windows.Forms;
using eZustellSendPOC.Services;
using WinFormsMvvm.ExtensionMethods;
using SimpleEventBroker;
using WinFormsMvvm.DialogService.FrameworkDialogs.SaveFile;
using eZustellSendPOC.RCService;
using System.IO;
using eZustellSendPOC.About.Views;

namespace eZustellSendPOC.ViewModels
{
    /// <summary>
    /// Controller for FrmSendPOCView
    /// </summary>
    public class SendPOCController : ViewModelBase
    {
        private IUnityContainer _uc;
        private IeZustellService _eZServ;
        private ICertificateService _cerServ;

        private SendPOCViewmodel _sendPocVM;
        /// <summary>
        /// Gets or sets the send poc vm.
        /// </summary>
        /// <value>
        /// The Datamodel for the View
        /// </value>
        public SendPOCViewmodel SendPocVM
        {
            get { return _sendPocVM; }
            set
            {
                if (_sendPocVM == value)
                    return;
                _sendPocVM = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The member list to select the Member to display
        /// </summary>
        public List<string> MemberList = new List<string>() { "FullName", "CompanyName", "PersTypeString" };

        /// <summary>
        /// Initializes a new instance of the <see cref="SendPOCController"/> class.
        /// </summary>
        /// <param name="uc">The uc.</param>
        /// <param name="dlgService">The dialog service.</param>
        /// <param name="vm">The vm.</param>
        public SendPOCController(IUnityContainer uc, IDialogService dlgService, SendPOCViewmodel vm, ICertificateService certServ, IeZustellService eZServ)
            : base(dlgService)
        {
            _uc = uc;
            _sendPocVM = vm;
            SendPocVM.ReceiverList = ReceiverViewModel.GetReceiverFromAddressBook(null, true);
            SendPocVM.SelectedReceiver = SendPocVM.ReceiverList[0];
            _eZServ = eZServ;
            _cerServ = certServ;
            SendPocVM.IsZustellungEnabled = true;
            if (_cerServ.Certificate == null)
            {
                SendPocVM.IsZustellungEnabled = false;
            }
        }

        #region OpenAddresBook
        private RelayCommand _OpenAddresBookCommand;
        /// <summary>
        /// Gets the open addres book command.
        /// </summary>
        /// <value>
        /// The open addres book command.
        /// </value>
        public RelayCommand OpenAddresBookCommand
        {
            get
            {
                _OpenAddresBookCommand = _OpenAddresBookCommand ?? new RelayCommand(OpenAddresBookClick);
                return _OpenAddresBookCommand;
            }
        }
        /// <summary>
        /// Opens the addres book click.
        /// </summary>
        /// <param name="obj">The object.</param>
        private void OpenAddresBookClick(object obj)
        {
            var addrBook = _eZServ.GetAddressBook();
            if (_eZServ.LastRC != eZustellServiceBase.ZustellServiceRC.OK)
            {
                _dlg.ShowMessageBox(_eZServ.GetMessage());
                return;
            }
            var recList = ReceiverViewModel.GetReceiverFromAddressBook(addrBook, true);
            SendPocVM.ReceiverList.Clear();
            foreach (var item in recList)
            {
                SendPocVM.ReceiverList.Add(item);
            }
            SendPocVM.SelectedReceiver = SendPocVM.ReceiverList[0];
        }
        #endregion

        #region MakeCertificate
        private RelayCommand _MakeCertificateCommand;
        /// <summary>
        /// Gets the make certificate command.
        /// </summary>
        /// <value>
        /// The make certificate command.
        /// </value>
        public RelayCommand MakeCertificateCommand
        {
            get
            {
                _MakeCertificateCommand = _MakeCertificateCommand ?? new RelayCommand(param => MakeCertificateClick());
                return _MakeCertificateCommand;
            }
        }

        /// <summary>
        /// Makes the certificate click.
        /// </summary>
        private void MakeCertificateClick()
        {
            Log.Information("{0}", CallerInfo.Create(), "MakeCertificate clicked");
            var rc = _dlg.ShowDialog<FrmCertificate, CertificateController>();
        }
        #endregion

        #region AddAttachment
        private RelayCommand _AddAttachmentCommand;
        public RelayCommand AddAttachmentCommand
        {
            get
            {
                _AddAttachmentCommand = _AddAttachmentCommand ?? new RelayCommand(AddAttachmentClick);
                return _AddAttachmentCommand;
            }
        }
        private void AddAttachmentClick(object obj)
        {
            var ofd = _uc.Resolve<IOpenFileDialog>();
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            ofd.Filter = "PDF Datei (*.pdf)|*.pdf|XML Datei (*.xml)|*.xml|Alle Dateien (*.*)|*.*";
            ofd.Multiselect = false;
            var rc = _dlg.ShowOpenFileDialog(ofd);
            if (rc == DialogResult.OK)
            {
                SendPocVM.AttachmentFileName = ofd.FileName;
            }
        }
        #endregion

        #region Deliver
        private RelayCommand _DeliverCommand;
        public RelayCommand DeliverCommand
        {
            get
            {
                _DeliverCommand = _DeliverCommand ?? new RelayCommand(DeliverClick);
                return _DeliverCommand;
            }
        }
        private void DeliverClick(object obj)
        {
            //GetTextBoxContentFire();
            if (!SendPocVM.IsZustellungEnabled)
            {
                _dlg.ShowMessageBox("Es sind noch nicht alle benötigen Felder ausgefüllt");
                return;
            }
            SendDeliveryRequestType sendRequ = SendPocVM;
            var resp = _eZServ.SendDelivery(sendRequ);
            if (_eZServ.LastRC != eZustellServiceBase.ZustellServiceRC.OK)
            {
                _dlg.ShowMessageBox(_eZServ.GetMessage());
                return;
            }
            
            _dlg.ShowMessageBox("Ihre Zustellung wurde abgeschickt."+Environment.NewLine+"Ihre Referenz: "+resp.SentDelivery.SendingApplicationId+Environment.NewLine+
                "Geschäftszahl: "+resp.SentDelivery.DeliveryId);
        }
        #endregion

        #region LoadCertificate
        private RelayCommand _LoadCertificateCommand;
        public RelayCommand LoadCertificateCommand
        {
            get
            {
                _LoadCertificateCommand = _LoadCertificateCommand ?? new RelayCommand(LoadCertificateClick);
                return _LoadCertificateCommand;
            }
        }
        private void LoadCertificateClick(object obj)
        {
            var rc = _dlg.ShowDialog<FrmLoadSaveCertificate, LoadCertificateController>();
            if ((rc != DialogResult.OK) || (_cerServ.Certificate == null))
            {
                SendPocVM.IsZustellungEnabled = false;
            }
            else
            {
                SendPocVM.IsZustellungEnabled = true;
            }

        }
        #endregion

        #region InitCertificate
        private RelayCommand _InitCertificateCommand;
        public RelayCommand InitCertificateCommand
        {
            get
            {
                _InitCertificateCommand = _InitCertificateCommand ?? new RelayCommand(InitCertificateClick);
                return _InitCertificateCommand;
            }
        }
        private void InitCertificateClick(object obj)
        {
            LoadCertificateController ctrl = _uc.Resolve<LoadCertificateController>();
            ctrl.VM.ShowNewButton = true;
            var rc = _dlg.ShowDialog<FrmLoadSaveCertificate>(ctrl);
            if (rc != DialogResult.OK)
            {
                CloseView(DialogResult.Cancel);
            }
            SetInitialPdfFilename();
        }
        #endregion

        #region SaveCertificate
        private RelayCommand _SaveCertificateCommand;
        public RelayCommand SaveCertificateCommand
        {
            get
            {
                _SaveCertificateCommand = _SaveCertificateCommand ?? new RelayCommand(SaveCertificateClick);
                return _SaveCertificateCommand;
            }
        }
        private void SaveCertificateClick(object obj)
        {
            string fn;
            var sfd = _uc.Resolve<ISaveFileDialog>();
            sfd.Filter = "Zertifikate (*.cer)|*.cer";
            if (string.IsNullOrWhiteSpace(Properties.Settings.Default.LastCertificatePath))
            {
                sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            }
            else
            {
                sfd.InitialDirectory = Properties.Settings.Default.LastCertificatePath;
            }
            var rc = _dlg.ShowSaveFileDialog(sfd);
            if (rc == DialogResult.OK)
            {
                fn = sfd.FileName;
                var rcCert = _cerServ.SaveAsCertificate(fn);
            }
        }
        #endregion

                #region ShowAbout
        private RelayCommand _ShowAboutCommand;
        public RelayCommand ShowAboutCommand 
        {            
            get
            {
                _ShowAboutCommand = _ShowAboutCommand ?? new RelayCommand(ShowAboutClick);
                return _ShowAboutCommand;
            }
        }
        private void ShowAboutClick(object obj)
        {
            _dlg.ShowDialog<FrmAboutView, About.ViewModels.AboutViewController>();
        }
        #endregion

        #region GetOwnAddressbookEntry
        private RelayCommand _GetOwnAddressbookEntryCommand;
        public RelayCommand GetOwnAddressbookEntryCommand
        {
            get
            {
                _GetOwnAddressbookEntryCommand = _GetOwnAddressbookEntryCommand ?? new RelayCommand(GetOwnAddressbookEntryClick);
                return _GetOwnAddressbookEntryCommand;
            }
        }
        private void GetOwnAddressbookEntryClick(object obj)
        {
            var myPersonData = _eZServ.SearchOwnAddress();
            var myEntry = ReceiverViewModel.GetReceiverFromAddressBook(myPersonData, false);
            SendPocVM.ConfirmationEMailList.Clear();
            SendPocVM.ConfirmationEMailList.AddRange(myEntry.FirstOrDefault().eMailAddressList);
            SendPocVM.SelectedEmailToConfirm = SendPocVM.ConfirmationEMailList[0];
        }
        #endregion

        [SubscribesTo(EventService.CertificateLoadedEvent)]
        public void OnCertificateLoadedEvent(object sender, EventArgs args)
        {
            //  EventService.CertificateLoadedEventEventArgs arg = args as  EventService.CertificateLoadedEventEventArgs;

            // Get own PersonalData
            GetOwnAddressbookEntryClick(null);
            //SendPocVM.ConfirmationEMailList.Clear();
            //SendPocVM.ConfirmationEMailList.Add("josef@bogad.at");
            //SendPocVM.ConfirmationEMailList.Add("jbogad@hotmail.com");
        }

        private void SetInitialPdfFilename()
        {
            string[] args = Environment.GetCommandLineArgs();

            if (args.Length < 2)
            {
                //label5.Text = "Zuwenig Parameter.";
                return;
            }
            string fn = args[1];
            if (!File.Exists(fn))
            {
                _dlg.ShowMessageBox("Datei " + fn + " nicht gefunden.");
                return;
            }
            SendPocVM.AttachmentFileName = fn;
        }

        [Publishes(EventService.GetTextBoxContent)]
        public event EventHandler GetTextBoxContentEvent;
        private void GetTextBoxContentFire()
        {
            EventHandler handler = GetTextBoxContentEvent;
            EventService.GetTextBoxContentEventArgs args = new EventService.GetTextBoxContentEventArgs();
            if (handler != null)
            {
                handler(this, args);
                SendPocVM.MessageText = args.MessageText;
            }
            return;
        }


    }
}
