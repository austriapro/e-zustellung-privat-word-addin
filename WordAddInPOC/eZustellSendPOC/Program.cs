using eZustellSendPOC.Models;
using eZustellSendPOC.Services;
using eZustellSendPOC.ViewModels;
using eZustellSendPOC.Views;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsMvvm.DialogService.ContextService;
using LogService;

namespace eZustellSendPOC
{
    static partial class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Log.InitLog();
            Log.SetLogLevel(Log.LogLevel.Debug);
            Log.Information("Application start",CallerInfo.Create(),"");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var appContext = new AppContext(typeof(FrmSendPOCView), typeof(eZustellSendPOC.ViewModels.SendPOCController),RegisterUnity.RegisterContainer);
            Application.Run(appContext);
        }

    }
}
