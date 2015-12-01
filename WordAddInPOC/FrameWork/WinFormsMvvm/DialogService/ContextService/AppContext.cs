using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Practices.EnterpriseLibrary;
using Microsoft.Practices.Unity.InterceptionExtension;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using WinFormsMvvm.DialogService.FrameworkDialogs.SaveFile;
using WinFormsMvvm.DialogService.FrameworkDialogs.OpenFile;
using WinFormsMvvm.DialogService.FrameworkDialogs.FolderBrowse;


namespace WinFormsMvvm.DialogService.ContextService
{
    /// <summary>
    /// Die AppContext Klasse erweitert das Winform MVVM Framework, damit das Main-Form aufgerufen werden kann
    /// Dazu muss in der static class Program folgende Zeile eingefügt werden bzw geändert werden
    ///             AppContext appContext = new AppContext(typeof(main form),typeof(View Model Controller für Mail form));
    ///            Application.Run(appContext);
    /// </summary>
    public partial class AppContext : ApplicationContext
    {
        public delegate void RegisterContainerCallback(IUnityContainer iuc);
        public IUnityContainer uc { get; set; }
        public AppContext(Type form, Type controller, RegisterContainerCallback callback = null)
        {
            IConfigurationSource source = ConfigurationSourceFactory.Create();
            var validationFactory = ConfigurationValidatorFactory.FromConfigurationSource(source);
            // Unity Container
            uc = new UnityContainer()
                     .AddNewExtension<Interception>();
            uc.RegisterInstance(uc, new ContainerControlledLifetimeManager());

            // Dialogservice
            uc.RegisterType<ISaveFileDialog, SaveFileDialogViewModel>();
            uc.RegisterType<IOpenFileDialog, OpenFileDialogViewModel>();
            uc.RegisterType<IFolderBrowserDialog, FolderBrowserDialogViewModel>();
            uc.RegisterType<IDialogService, DialogService>(new ContainerControlledLifetimeManager());
            uc.RegisterType(controller, new ContainerControlledLifetimeManager());
            uc.RegisterType(form);
            if (callback != null)
            {
                callback(uc);
            }
            var dlg = uc.Resolve<IDialogService>();
            var ctl = uc.Resolve(controller);
            var frm = uc.Resolve(form) as FormService;
            frm.FormClosed += frm_FormClosed;
            frm.Show();
            return;
        }

        void frm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ExitThread();
        }
    }
}
