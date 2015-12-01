using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eZustellSendPOC.Models
{
    /// <summary>
    /// Beschreibt die Dokumentenklasse für den Versand
    /// </summary>
    public class DocumentClass // : ViewModelBase
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

        /// <summary>
        /// Gets the document class list.
        /// </summary>
        /// <returns>List of DocumentClass <see cref="DocumentClass"/> </returns>
        public static List<DocumentClass> GetDocumentClassList()
        {
            List<DocumentClass> _documentClassList = new List<DocumentClass>()
            { 
                new DocumentClass(){Value="", Description=Properties.Settings.Default.PleaseChoose},
                new DocumentClass(){Value="x-edclass/billing", Description="Rechnungslegung: Rechnung, Mahnung"},
                new DocumentClass(){Value="x-edclass/procurement", Description="Auftragswesen: Angebot, Bestellung, Auftragsbestätigung, Lieferschein"},
                new DocumentClass(){Value="x-edclass/tender", Description="Ausschreibung: Ausschreibungsunterlagen, Einreichung _"},
                new DocumentClass(){Value="x-edclass/legal", Description="Vertragswesen"},
                new DocumentClass(){Value="x-edclass/banking", Description="Bankwesen: Zahlscheine, Kontoauszüge ..."},
                new DocumentClass(){Value="x-edclass/insurance", Description="Versicherungswesen: Polizzen ..."},
                new DocumentClass(){Value="x-edclass/medical", Description="Medizin: Arztbriefe, Befunde ..."},
                new DocumentClass(){Value="x-edclass/news", Description="Zeitungen, Newsletter ..."},
                new DocumentClass(){Value="x-edclass/sv", Description="Sozialversicherung: Beitragsvorschreibungen,  Abrechnungen"},
                new DocumentClass(){Value="x-edclass/information", Description="Allgemeine Informationen"},
                new DocumentClass(){Value="x-edclass/advertisment", Description="Werbung"},
                new DocumentClass(){Value="x-edclass/private", Description="Private Mitteilungen, Dokumente ..."},
                new DocumentClass(){Value="x-edclass/government", Description="Behördliche Dokumente (die nicht den Anforderungen der behördlichen e-Zustellung unterliegen)"}
            };
            return _documentClassList;
        }
    }

}
