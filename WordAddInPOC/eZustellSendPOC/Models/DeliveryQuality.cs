using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eZustellSendPOC.Models
{
    /// <summary>
    /// Beschreibt die Zustellqualität
    /// </summary>
    public class DeliveryQuality
    {
        private string _description;
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description
        {
            get { return _description; }
            set
            {
                if (_description == value)
                    return;
                _description = value;
                //OnPropertyChanged();
            }
        }

        private string _value;
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public string Value
        {
            get { return _value; }
            set
            {
                if (_value == value)
                    return;
                _value = value;
                //OnPropertyChanged();
            }
        }

        public static List<DeliveryQuality> GetDeliveryQualityList()
        {
            List<DeliveryQuality> delQuList = new List<DeliveryQuality>(){
                new DeliveryQuality(){Value = "", Description=Properties.Settings.Default.PleaseChoose},
                new DeliveryQuality(){Value = "x-edtype/standard", Description="Standardzustellung"},
                new DeliveryQuality(){Value = "x-edtype/registered", Description="Eingeschriebene Zustellung"},
                new DeliveryQuality(){Value = "x-edtype/ident", Description="Zustellung nach einem 'Identverfahren' möglich (eindeutige Identität lt. eGovG §2), analog behördlicher e-Zustellung"}
            };
            return delQuList;
        }
    }
}
