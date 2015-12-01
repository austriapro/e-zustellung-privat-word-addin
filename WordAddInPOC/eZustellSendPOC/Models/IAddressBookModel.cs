using System;
using System.Collections.Generic;

namespace eZustellSendPOC.Models
{
    /// <summary>
    /// Infertace for getting the Addressbook from Webservice
    /// </summary>
    public interface IAddressBookModel
    {
        /// <summary>
        /// Gets the mail adresslist.
        /// </summary>
        /// <returns>The E-Mail Address List</returns>
        List<string> GetMailAdresslist();

        /// <summary>
        /// Loads the address book from server.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <returns>true: Success, false: Failure</returns>
        bool LoadAddressBookFromServer(string password);

        string GetFailureReason();
    }
}
