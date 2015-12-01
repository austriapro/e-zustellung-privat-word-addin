using eZustellSendPOC.RCService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eZustellSendPOC.Models
{
    public class AddressBookModelDesign : IAddressBookModel
    {
        public List<string> GetMailAdresslist()
        {
            List<string> mailAddrs = new List<string>() { "mail1@nowhere.com", "mail2@woanders.at" };
            return mailAddrs;
        }


        public bool LoadAddressBookFromServer(string password)
        {
            throw new NotImplementedException();
        }


        public string GetFailureReason()
        {
            throw new NotImplementedException();
        }
    }
}
