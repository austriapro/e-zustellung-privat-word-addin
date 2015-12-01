using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using WinFormsMvvm;
using WinFormsMvvm.DialogService;
using WinFormsMvvm.ExtensionMethods;
using System.ComponentModel;
using eZustellSendPOC.Models;
using eZustellSendPOC.RCService;
using eZustellSendPOC.Properties;
using System.IO;
using eZustellSendPOC.Services;
using LogService;

namespace eZustellSendPOC.ViewModels
{
    /// <summary>
    /// Controller for FrmSendPOC
    /// </summary>
    public class SendPOCViewmodel : ViewModelBase
    {
        private IAddressBookModel _addressBook;

        public SendPOCViewmodel(IAddressBookModel addressBook)
        {
            _docClasses = new BindingList<DocumentClass>(DocumentClass.GetDocumentClassList());
            _selectedDocumentClass = _docClasses[0];

            _deliverQualityList = new BindingList<Models.DeliveryQuality>(Models.DeliveryQuality.GetDeliveryQualityList());
            _selectedDeliveryQuality = _deliverQualityList[0];
            _addressBook = addressBook;

            _confirmationEMailList = new BindingList<string>(_addressBook.GetMailAdresslist());
            _confirmationEMailList.Insert(0, Properties.Settings.Default.PleaseChoose);
            _selectedEmailToConfirm = _confirmationEMailList[0];
            _confirmByEMail = true;
            _confirmByWebServiceUrl = false;
            _lockedUntilDateTime = DateTime.Today.AddDays(1);
            _lockedUtilTime = new DateTime(_lockedUntilDateTime.Year, _lockedUntilDateTime.Month, _lockedUntilDateTime.Day, 0, 0, 0);
        }

        #region Receiver
        private BindingList<ReceiverViewModel> _receiverList = new BindingList<ReceiverViewModel>() { new ReceiverViewModel() };
        /// <summary>
        /// Liste der Empfänger aus dem Adressbuch
        /// </summary>
        public BindingList<ReceiverViewModel> ReceiverList
        {
            get { return _receiverList; }
            set
            {
                if (_receiverList == value)
                    return;
                _receiverList = value;
                OnPropertyChanged();
            }
        }

        private ReceiverViewModel _selectedReceiver;
        public ReceiverViewModel SelectedReceiver
        {
            get { return _selectedReceiver; }
            set
            {
                if (_selectedReceiver == value)
                    return;
                _selectedReceiver = value;
                OnPropertyChanged();
            }
        }

        #endregion

        private string _subject;
        /// <summary>
        /// Subject: Betreff der Nachricht
        /// </summary>
        public string Subject
        {
            get { return _subject; }
            set
            {
                if (_subject == value)
                    return;
                _subject = value;
                OnPropertyChanged();
            }
        }

        private string _reference;
        /// <summary>
        /// Referenz des Senders
        /// </summary>
        public string Reference
        {
            get { return _reference; }
            set
            {
                if (_reference == value)
                    return;
                _reference = value;
                OnPropertyChanged();
            }
        }

        #region Document Class
        private BindingList<DocumentClass> _docClasses;
        /// <summary>
        /// Gets or sets the document classes.
        /// </summary>
        /// <value>
        /// The document classes.
        /// </value>
        public BindingList<DocumentClass> DocClasses
        {
            get { return _docClasses; }
            set
            {
                if (_docClasses == value)
                    return;
                _docClasses = value;
                OnPropertyChanged();
            }
        }

        private DocumentClass _selectedDocumentClass;
        /// <summary>
        /// Gets or sets the selected document class.
        /// </summary>
        /// <value>
        /// The selected document class.
        /// </value>
        public DocumentClass SelectedDocumentClass
        {
            get { return _selectedDocumentClass; }
            set
            {
                if (_selectedDocumentClass == value)
                    return;
                _selectedDocumentClass = value;
                OnPropertyChanged();
            }
        }


        #endregion

        #region Delivery Quality
        private BindingList<Models.DeliveryQuality> _deliverQualityList;
        /// <summary>
        /// Gets or sets the deliver quality list.
        /// </summary>
        /// <value>
        /// The deliver quality list.
        /// </value>
        public BindingList<Models.DeliveryQuality> DeliverQualityList
        {
            get { return _deliverQualityList; }
            set
            {
                if (_deliverQualityList == value)
                    return;
                _deliverQualityList = value;
                OnPropertyChanged();
            }
        }

        private Models.DeliveryQuality _selectedDeliveryQuality;
        /// <summary>
        /// Gets or sets the selected delivery quality.
        /// </summary>
        /// <value>
        /// The selected delivery quality.
        /// </value>
        public Models.DeliveryQuality selectedDeliveryQuality
        {
            get { return _selectedDeliveryQuality; }
            set
            {
                if (_selectedDeliveryQuality == value)
                    return;
                _selectedDeliveryQuality = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Lock Until DateTime
        private bool _lockDeliveryUntil;
        /// <summary>
        /// Gets or sets a value indicating whether [lock delivery until].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [lock delivery until]; otherwise, <c>false</c>.
        /// </value>
        public bool LockDeliveryUntil
        {
            get { return _lockDeliveryUntil; }
            set
            {
                if (_lockDeliveryUntil == value)
                    return;
                _lockDeliveryUntil = value;
                OnPropertyChanged();
            }
        }


        private DateTime _lockedUntilDateTime;
        /// <summary>
        /// Gets or sets the locked until date time.
        /// </summary>
        /// <value>
        /// The locked until date time.
        /// </value>
        public DateTime LockedUntilDateTime
        {
            get { return _lockedUntilDateTime; }
            set
            {
                if (_lockedUntilDateTime == value)
                    return;
                _lockedUntilDateTime = new DateTime(value.Year, value.Month, value.Day, _lockedUtilTime.Hour, _lockedUtilTime.Minute, _lockedUtilTime.Second);
                OnPropertyChanged();
            }
        }

        private DateTime _lockedUtilTime;
        /// <summary>
        /// Gets or sets the locked utility time.
        /// </summary>
        /// <value>
        /// The locked utility time.
        /// </value>
        public DateTime LockedUtilTime
        {
            get { return _lockedUtilTime; }
            set
            {
                if (_lockedUtilTime == value)
                    return;
                _lockedUtilTime = value;
                _lockedUntilDateTime = new DateTime(_lockedUntilDateTime.Year, _lockedUntilDateTime.Month, _lockedUntilDateTime.Day, _lockedUtilTime.Hour, _lockedUtilTime.Minute, _lockedUtilTime.Second);
                _lockedUtilTime = _lockedUntilDateTime;
                OnPropertyChanged();
                OnPropertyChanged("LockedUntilDateTime");
            }
        }

        #endregion

        #region E-Mail Confirmation
        private bool _confirmByEMail;
        /// <summary>
        /// Gets or sets a value indicating whether [confirm by e mail].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [confirm by e mail]; otherwise, <c>false</c>.
        /// </value>
        public bool ConfirmByEMail
        {
            get { return _confirmByEMail; }
            set
            {
                if (_confirmByEMail == value)
                    return;
                _confirmByEMail = value;
                _confirmByWebServiceUrl = !_confirmByEMail;
                OnPropertyChanged();
                OnPropertyChanged("ConfirmByWebServiceUrl");
            }
        }


        private BindingList<string> _confirmationEMailList;
        /// <summary>
        /// Gets or sets the confirmation e mail list.
        /// </summary>
        /// <value>
        /// The confirmation e mail list.
        /// </value>
        public BindingList<string> ConfirmationEMailList
        {
            get { return _confirmationEMailList; }
            set
            {
                if (_confirmationEMailList == value)
                    return;
                _confirmationEMailList = value;
                OnPropertyChanged();
            }
        }


        private string _selectedEmailToConfirm;
        /// <summary>
        /// Gets or sets the selected email to confirm.
        /// </summary>
        /// <value>
        /// The selected email to confirm.
        /// </value>
        public string SelectedEmailToConfirm
        {
            get { return _selectedEmailToConfirm; }
            set
            {
                if (_selectedEmailToConfirm == value)
                    return;
                _selectedEmailToConfirm = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Webservice Url
        private bool _confirmByWebServiceUrl;
        /// <summary>
        /// Gets or sets a value indicating whether [confirm by web service URL].
        /// </summary>
        /// <value>
        /// <c>true</c> if [confirm by web service URL]; otherwise, <c>false</c>.
        /// </value>
        public bool ConfirmByWebServiceUrl
        {
            get { return _confirmByWebServiceUrl; }
            set
            {
                if (_confirmByWebServiceUrl == value)
                    return;
                _confirmByWebServiceUrl = value;
                _confirmByEMail = !_confirmByWebServiceUrl;
                OnPropertyChanged();
                OnPropertyChanged("ConfirmByEMail");
            }
        }


        private string _confirmationWebServiceUrl;
        /// <summary>
        /// Gets or sets the confirmation web service URL.
        /// </summary>
        /// <value>
        /// The confirmation web service URL.
        /// </value>
        public string ConfirmationWebServiceUrl
        {
            get { return _confirmationWebServiceUrl; }
            set
            {
                if (_confirmationWebServiceUrl == value)
                    return;
                _confirmationWebServiceUrl = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region IsZustellungEnabled - bool
        private bool _IsZustellungEnabled = true;
        public bool IsZustellungEnabled
        {
            get
            {
                bool allRequiredEntered = true;
                allRequiredEntered = allRequiredEntered && !string.IsNullOrEmpty(Reference);
                allRequiredEntered = allRequiredEntered && (!string.IsNullOrEmpty(MessageText)||!string.IsNullOrWhiteSpace(AttachmentFileName));
                allRequiredEntered = allRequiredEntered && (selectedDeliveryQuality.Value != "");
                allRequiredEntered = allRequiredEntered && (SelectedDocumentClass.Value != "");
                allRequiredEntered = allRequiredEntered && (SelectedReceiver.edID!= "");
                if (ConfirmByEMail)
                {
                    allRequiredEntered = allRequiredEntered && (SelectedEmailToConfirm != "");

                }
                if (ConfirmByWebServiceUrl)
                {
                    allRequiredEntered = allRequiredEntered && (ConfirmationWebServiceUrl != "");
                }
                allRequiredEntered = allRequiredEntered && (SelectedReceiver.PersTypeString != "");
                return _IsZustellungEnabled || allRequiredEntered;
            }
            set
            {
                if (_IsZustellungEnabled == value)
                    return;
                _IsZustellungEnabled = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region MessageText
        private string _MessageText;
        public string MessageText
        {
            get { return _MessageText; }
            set
            {
                if (_MessageText == value)
                    return;
                _MessageText = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region AttachmentFileName
        private string _attachmentFileName;
        /// <summary>
        /// Gets or sets the name of the attachment file.
        /// </summary>
        /// <value>
        /// The name of the attachment file.
        /// </value>
        public string AttachmentFileName
        {
            get { return _attachmentFileName; }
            set
            {
                if (_attachmentFileName == value)
                    return;
                _attachmentFileName = value;
                OnPropertyChanged();
            }
        }
        #endregion

        /// <summary>
        /// Performs an implicit conversion from <see cref="SendPOCViewmodel"/> to <see cref="SendDeliveryRequestType"/>.
        /// </summary>
        /// <param name="vm">The vm.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator SendDeliveryRequestType(SendPOCViewmodel vm)
        {
            SendDeliveryRequestType sd = new SendDeliveryRequestType()
            {
                Sender = new SendDeliveryRequestTypeSender()
            };
            sd.RecipientEdid = vm.SelectedReceiver.edID;
            sd.Subject = vm.Subject;
            sd.MetaData = new MetaDataType();
            sd.MetaData.DeliveryQuality = vm.selectedDeliveryQuality.Value;
            sd.MetaData.DocumentClass = vm.SelectedDocumentClass.Value;
            sd.MetaData.SendingApplicationID = vm.Reference;
            string stmp = DateTime.Now.ToString("s") + " " + Guid.NewGuid().ToString();
            sd.MetaData.SendingServiceID = stmp;
            sd.Documents = new DocumentsType();
            var notification = new NotificationAddressType();
            if (vm.ConfirmByEMail)
            {
                notification.Item = "email";
            }
            else if (vm.ConfirmByWebServiceUrl)
            {
                notification.Item = vm.ConfirmationWebServiceUrl;
            }
            if (vm.LockDeliveryUntil)
            {
                sd.MetaData.DeliverBefore = vm.LockedUntilDateTime;
            }
            sd.Sender.NotificationAddress = notification;
            List<DocumentType> docsToSend = new List<DocumentType>();
            if (!string.IsNullOrWhiteSpace(vm.MessageText))
            {
                string tempFn = Path.Combine(Path.GetTempPath(), "Message.txt");
                File.WriteAllText(tempFn, vm.MessageText);
                docsToSend.Add(GetDocumentToAttach(tempFn));
            }
            if (!string.IsNullOrWhiteSpace(vm.AttachmentFileName))
            {
                docsToSend.Add(GetDocumentToAttach(vm.AttachmentFileName));

            }
            if (vm.LockDeliveryUntil)
            {
                sd.MetaData.LockUntil = vm.LockedUntilDateTime;
                sd.MetaData.LockUntilSpecified = true;
            }
            sd.Documents.Document = docsToSend.ToArray();
            Log.Debug("Request: {@SendDeliveryRequestType}", CallerInfo.Create(), sd);

            return sd;
        }

        private static DocumentType GetDocumentToAttach(string fn)
        {
            byte[] binDoc = File.ReadAllBytes(fn);
            DocumentType doc1 = new DocumentType();
            doc1.Base64Content = binDoc;
            doc1.FileName = Path.GetFileName(fn);
            doc1.MIMEType = GetMimeType(fn);
            doc1.Size = binDoc.Length.ToString();
            doc1.CheckSum = new DocumentTypeCheckSum()
            {
                AlgorithmID = CertificateService.CheckSumAlgorithm.SHA256.ToString(),
                Value = CertificateService.GetChecksum(binDoc, CertificateService.CheckSumAlgorithm.SHA256)
            };
            return doc1;
        }

        private static string GetMimeType(string fileName)
        {
            string mimeType = "application/unknown";
            string ext = System.IO.Path.GetExtension(fileName).ToLower();
            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            if (regKey != null && regKey.GetValue("Content Type") != null)
                mimeType = regKey.GetValue("Content Type").ToString();
            return mimeType;
        }
    }


}
