using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using WinFormsMvvm;
using WinFormsMvvm.DialogService;
using WinFormsMvvm.ExtensionMethods;
using eZustellSendPOC.RCService;
using System.ComponentModel;

namespace eZustellSendPOC.ViewModels
{
    public class ReceiverViewModel : ViewModelBase
    {
        public ReceiverViewModel()
        {
            _name = string.Empty;
            _persType = PersonType.unknown;
            _companyName = string.Empty;
            _edID = string.Empty;
        }
        public enum PersonType
        {
            [Description(" ")]
            unknown = 0,
            [Description("Person")]
            natPersion,
            [Description("Firma")]
            jurPerson
        }
        private string _name;
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// FullName der Person
        /// </value>
        public string FullName
        {
            get { return _name; }
            set
            {
                if (_name == value)
                    return;
                _name = value;
                OnPropertyChanged();
            }
        }

        private string _companyName;
        /// <summary>
        /// Gets or sets the name of the company.
        /// </summary>
        /// <value>
        /// The name of the company.
        /// </value>
        public string CompanyName
        {
            get { return _companyName; }
            set
            {
                if (_companyName == value)
                    return;
                _companyName = value;
                OnPropertyChanged();
            }
        }

        private PersonType _persType;
        /// <summary>
        /// Gets or sets the type of the pers. (Nat or Jur)
        /// </summary>
        /// <value>
        /// The type of the pers.
        /// </value>
        public PersonType PersType
        {
            get { return _persType; }
            set
            {
                if (_persType == value)
                    return;
                _persType = value;
                OnPropertyChanged();
                OnPropertyChanged("PersTypeString");
            }
        }

        /// <summary>
        /// Gets or sets the pers type string. Use just for Display
        /// </summary>
        /// <value>
        /// The pers type string.
        /// </value>
        public string PersTypeString
        {
            get { return _persType.GetDescriptionFromValue(); }
            set
            {
                //if (_persTypeString == value)
                //    return;
                string s = value;
                _persType = StringExtensions.GetValueFromDescription<PersonType>(value);
                OnPropertyChanged();
                OnPropertyChanged("PersType");
            }
        }

        private string _edID;
        /// <summary>
        /// Gets or sets the ed identifier.
        /// </summary>
        /// <value>
        /// edID der Person
        /// </value>
        public string edID
        {
            get { return _edID; }
            set
            {
                if (_edID == value)
                    return;
                _edID = value;
                OnPropertyChanged();
            }
        }

        private BindingList<string> _eMailAddressList = new BindingList<string>();
        public BindingList<string> eMailAddressList
        {
            get { return _eMailAddressList; }
            set
            {
                if (_eMailAddressList == value)
                    return;
                _eMailAddressList = value;
                OnPropertyChanged();
            }
        }


        /// <summary>
        /// Performs an implicit conversion from <see cref="AbstractPersonType"/> to <see cref="ReceiverViewModel"/>.
        /// </summary>
        /// <param name="person">The person.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator ReceiverViewModel(PersonDataType personData)
        {
            var receiver = new ReceiverViewModel();
            var person = personData.Item;
            var edID = person.Items.Where(p => p.Type as string == "edID");
            if (edID.Any())
            {
                receiver.edID = (string)edID.FirstOrDefault().Value;
            }
            if (person is edNatPersonType)
            {
                edNatPersonType natPerson = (edNatPersonType)person;
                receiver.FullName = natPerson.Name.FamilyName.Value + " " + natPerson.Name.GivenName;
                receiver.PersType = PersonType.natPersion;
                receiver.CompanyName = string.Empty;
                
            }
            if (person is edJurPersonType)
            {
                edJurPersonType jurPerson = (edJurPersonType)person;
                receiver.CompanyName = jurPerson.Organization;
                receiver.PersType = PersonType.jurPerson;
                receiver.FullName = jurPerson.FullName;
            }
            var eMailAddresses = personData.Items.Where(p => p is InternetAddressType);
            receiver.eMailAddressList.Clear();
            foreach (var addr in eMailAddresses)
            {
                receiver.eMailAddressList.Add(((InternetAddressType)addr).Address);
            }
            return receiver;
            
        }

        /// <summary>
        /// Gets the receiver from address book.
        /// </summary>
        /// <param name="addressBook">The address book.</param>
        /// <param name="initFirst">if set to <c>true</c> [initializes first entry].</param>
        /// <returns>
        /// BindingList of ReceiverViewModel <see cref="ReceiverViewModel" />
        /// </returns>
        public static BindingList<ReceiverViewModel> GetReceiverFromAddressBook(List<PersonDataType> addressBook, bool initFirst)
        {
            BindingList<ReceiverViewModel> receiverList = new BindingList<ReceiverViewModel>();
            if (initFirst)
            {
                receiverList.Add(new ReceiverViewModel()
                {
                    FullName = Properties.Settings.Default.PleaseChoose,
                    PersType = ReceiverViewModel.PersonType.unknown,
                });
                
            } if (addressBook == null)
            {
                return receiverList;
            }
            
            foreach (var item in addressBook)
            {
                receiverList.Add(item);
                
            }
            return receiverList;
        }

    }

}
