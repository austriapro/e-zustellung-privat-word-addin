using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;

namespace WordAddInPOC
{
    public partial class eZustellRibbon
    {
        /// <summary>
        /// Handles the Load event of the eZustellRibbon control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RibbonUIEventArgs"/> instance containing the event data.</param>
        private void eZustellRibbon_Load(object sender, RibbonUIEventArgs e)
        {

        }

        /// <summary>
        /// Handles the Click event of the btnDeliver control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RibbonControlEventArgs"/> instance containing the event data.</param>
        private void btnDeliver_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.SendByZustellung();
        }

    }
}
