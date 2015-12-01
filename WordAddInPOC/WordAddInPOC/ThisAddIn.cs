using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Word = Microsoft.Office.Interop.Word;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Word;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Diagnostics;

namespace WordAddInPOC
{
    public partial class ThisAddIn
    {
        /// <summary>
        /// Handles the Startup event of the ThisAddIn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
        }

        /// <summary>
        /// Handles the Shutdown event of the ThisAddIn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }

        /// <summary>
        /// Sends the by zustellung.
        /// </summary>
        public void SendByZustellung()
        {
            Word.Document doc = Globals.ThisAddIn.Application.ActiveDocument;
            SaveFileDialog ofd = new SaveFileDialog();
            ofd.Filter = "PDF-Datei(*.pdf) | *.pdf";
            if (doc.Saved)
            {
                ofd.FileName = Path.ChangeExtension(Path.GetFileName(doc.FullName), "pdf");
                ofd.InitialDirectory = Path.GetDirectoryName(doc.FullName);

            }
            else
            {
                ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }
            ofd.Title = "Für eZustellung als PDF Datei speichern";
            var rc = ofd.ShowDialog();
            if (rc!=DialogResult.OK)
            {
                return;
            }
            
            if (!SaveDocumentAsPdf(ref doc, ofd.FileName))
            {
                return;
            }
            string pgm = Properties.Settings.Default.eZustellProgram;
            if (string.IsNullOrEmpty(pgm) || !File.Exists(pgm))
            {
                OpenFileDialog pofd = new OpenFileDialog();
                pofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
                pofd.Title = "eZustellprogramm auswählen";
                pofd.Multiselect = false;
                 rc = pofd.ShowDialog();
                if (rc != DialogResult.OK)
                {
                    return;
                }
                Properties.Settings.Default.eZustellProgram = pofd.FileName;
                Properties.Settings.Default.Save();
                pgm = pofd.FileName;
            }
            string parm = '"'+ofd.FileName+'"';
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = pgm;
            startInfo.Arguments = parm;
            Process.Start(startInfo);

        }

        /// <summary>
        /// Saves the document as PDF.
        /// </summary>
        /// <param name="document">The document.</param>
        /// <param name="targetPath">The target path.</param>
        /// <returns></returns>
        private bool SaveDocumentAsPdf(ref Word.Document document, string targetPath)
        {
            Word.WdExportFormat exportFormat = Word.WdExportFormat.wdExportFormatPDF;
            bool openAfterExport = false;
            Word.WdExportOptimizeFor exportOptimizeFor = Word.WdExportOptimizeFor.wdExportOptimizeForPrint;
            Word.WdExportRange exportRange = Word.WdExportRange.wdExportAllDocument;
            int startPage = 0;
            int endPage = 0;
            Word.WdExportItem exportItem = Word.WdExportItem.wdExportDocumentContent;
            bool includeDocProps = false;
            bool keepIRM = true;
            Word.WdExportCreateBookmarks createBookmarks = Word.WdExportCreateBookmarks.wdExportCreateNoBookmarks;
            bool docStructureTags = true;
            bool bitmapMissingFonts = true;
            bool useISO19005_1 = false;
            object missing = Missing.Value;

            // Export it in the specified format.  
            try
            {
                document.ExportAsFixedFormat(
                    targetPath,
                    exportFormat,
                    openAfterExport,
                    exportOptimizeFor,
                    exportRange,
                    startPage,
                    endPage,
                    exportItem,
                    includeDocProps,
                    keepIRM,
                    createBookmarks,
                    docStructureTags,
                    bitmapMissingFonts,
                    useISO19005_1,
                    ref missing);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler beim Speichern als PDF:" + Environment.NewLine + ex.Message);
                return false;
            }
            return true;
        }

        #region Von VSTO generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}
