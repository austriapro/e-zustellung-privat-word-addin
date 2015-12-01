using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Practices.Unity;
using LogService;
using eZustellSendPOC;
using eZustellSendPOC.Services;
using eZustellSendPOC.Views;
using eZustellSendPOC.ViewModels;
using eZustellSendPOC.Models;
using WinFormsMvvm.DialogService.ContextService;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using WinFormsMvvm.DialogService.FrameworkDialogs.SaveFile;
using WinFormsMvvm.DialogService.FrameworkDialogs.OpenFile;
using WinFormsMvvm.DialogService.FrameworkDialogs.FolderBrowse;
using WinFormsMvvm.DialogService;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace eZustellSendPOCTests
{
    [TestClass]
    public class TestBase
    {
       public IUnityContainer uc  {get; set;}
        [TestInitialize]
        public void InitTest()
        {
            Log.InitLog();
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
            RegisterUnity.RegisterContainer(uc);
        }

    }

}