using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using Microsoft.Practices.Unity;
using WinFormsMvvm.DialogService.FrameworkDialogs;
using WinFormsMvvm.DialogService.FrameworkDialogs.FolderBrowse;
using WinFormsMvvm.DialogService.FrameworkDialogs.OpenFile;
using WinFormsMvvm.DialogService.FrameworkDialogs.SaveFile;
using MessageBox = System.Windows.Forms.MessageBox;
using OpenFileDialogMvvM = WinFormsMvvm.DialogService.FrameworkDialogs.OpenFile.OpenFileDialog;
using SaveFileDialogMvvm = WinFormsMvvm.DialogService.FrameworkDialogs.SaveFile.SaveFileDialog;
using FolderBrowserDialogMvvM = WinFormsMvvm.DialogService.FrameworkDialogs.FolderBrowse.FolderBrowserDialog;
using LogService;
using WinFormsMvvm.ExtensionMethods;
//using LogService;

namespace WinFormsMvvm.DialogService
{
    /// <summary>
    /// Class responsible for abstracting ViewModels from Views.
    /// </summary>
    public class DialogService : IDialogService
    {
        private FolderBrowserDialogMvvM _folderBrowser;
        private SaveFileDialogMvvm _saveFileDialog;
        private OpenFileDialogMvvM _openFileDialog;
        private IUnityContainer _uc;
        public DialogService(FrameworkDialogs.FolderBrowse.FolderBrowserDialog folderBrowser,
            FrameworkDialogs.SaveFile.SaveFileDialog saveFileDialog,
            FrameworkDialogs.OpenFile.OpenFileDialog openFileDialog,
            IUnityContainer container)
        {
            _folderBrowser = folderBrowser;
            _openFileDialog = openFileDialog;
            _saveFileDialog = saveFileDialog;
            _uc = container;
        }
        #region IDialogService Members

        Cursor oldCursor = null;

        /// <summary>
        /// Sets the wait cursor.
        /// </summary>
        public void SetWaitCursor()
        {
            if (Cursor.Current != Cursors.WaitCursor)
            {
                oldCursor = Cursor.Current; 
            }
            Cursor.Current = Cursors.WaitCursor;
        }

        /// <summary>
        /// Resets the cursor.
        /// </summary>
        public void ResetCursor()
        {
            if (oldCursor!=null)
            {
                Cursor.Current = oldCursor;

            }
        }
        /// <summary>
        /// Shows a dialog.
        /// </summary>
        /// <remarks>
        /// The dialog used to represent the ViewModel is retrieved from the registered mappings.
        /// </remarks>
        /// <param name="form">
        /// A ViewModel that represents the owner window of the dialog.
        /// </param>
        /// <param name="viewModel">The ViewModel of the new dialog.</param>
        /// <returns>
        /// A nullable value of type bool that signifies how a window was closed by the user.
        /// </returns>
        public ViewModelBase.DialogResult ShowDialog<T>(object viewModel) where T : FormService
        {
            // Form form2Show = form as Form;
            try
            {
                if (!_uc.IsRegistered(typeof(T)))
                {
                    Log.Error("{0} nicht registriert", CallerInfo.Create(), typeof(T).ToString());
                    return ViewModelBase.DialogResult.Abort;
                }

                var form2Show = _uc.Resolve<T>(new ParameterOverride("viewModel", viewModel));
                ((ViewModelBase)viewModel).CurrentForm = form2Show;
                Log.Information("Dialog: {0}", CallerInfo.Create(), form2Show.GetType().FullName);
                form2Show.SetBindingSource(viewModel);                
                var rc = form2Show.ShowDialog().ToEnum<ViewModelBase.DialogResult>();
                Log.Information("Dialog '{0}', rc={1} - {2}", CallerInfo.Create(), form2Show.GetType().FullName, rc.ToString(), rc.GetDescriptionFromValue());
                return rc;

            }
            catch (Exception ex)
            {
                Log.Error(ex,CallerInfo.Create(),"");

            }
            return ViewModelBase.DialogResult.Abort;
        }

        public ViewModelBase.DialogResult ShowDialog<T, T2>()
            where T : FormService
            where T2 : ViewModelBase
        {
            // Form form2Show = form as Form;
            try
            {
                if(!_uc.IsRegistered(typeof(T2)))
                {
                   Log.Error("{0} nicht registriert",CallerInfo.Create(), typeof(T2).ToString());
                    return ViewModelBase.DialogResult.Abort;
                }
                if (!_uc.IsRegistered(typeof(T)))
                {
                    Log.Error("{0} nicht registriert", CallerInfo.Create(), typeof(T).ToString());
                    return ViewModelBase.DialogResult.Abort;
                }
                var model = _uc.Resolve<T2>();
                var form2Show = _uc.Resolve<T>(new ParameterOverride("viewModel", model));
                model.CurrentForm = form2Show;
                Log.Information("Dialog: {0}",CallerInfo.Create(), form2Show.GetType().FullName);
                form2Show.SetBindingSource(model);
                var rc = form2Show.ShowDialog().ToEnum<ViewModelBase.DialogResult>();

                Log.Information("Dialog '{0}', rc={1} - {2}", CallerInfo.Create(), form2Show.GetType().FullName, rc.ToString(), rc.GetDescriptionFromValue());
                return rc;

            }
            catch (Exception ex)
            {
                Log.Error(ex,CallerInfo.Create(), "ShowDialog failed.");

            }
            return ViewModelBase.DialogResult.Abort;
        }
        public void Show<T, T2>()
            where T : FormService
            where T2 : ViewModelBase
        {
            // Form form2Show = form as Form;
            try
            {
                if (!_uc.IsRegistered(typeof(T2)))
                {
                    Log.Error("{0} nicht registriert",CallerInfo.Create(), typeof(T2).ToString());
                    throw new ArgumentException(string.Format("{0} nicht mit Unity registriert", typeof(T2).ToString()));
                }
                if (!_uc.IsRegistered(typeof(T)))
                {
                    Log.Error("{0} nicht registriert",CallerInfo.Create(), typeof(T).ToString());
                    throw new ArgumentException(string.Format("{0} nicht mit Unity registriert", typeof(T).ToString()));
                }
                var model = _uc.Resolve<T2>();
                var form2Show = _uc.Resolve<T>(new ParameterOverride("viewModel", model));
                model.CurrentForm = form2Show;
                Log.Information("Dialog: {0}",CallerInfo.Create(), form2Show.GetType().FullName);
                form2Show.SetBindingSource(model);
                form2Show.Show();

                return;

            }
            catch (Exception ex)
            {
                Log.Error(ex,CallerInfo.Create(),string.Format("Show failed for Form {0} and ViewModel {1}",typeof(T).Name, typeof(T2).Name));

            }
            return ;
        }

        public void UpdateUnityContainer(IUnityContainer uc)
        {
            _uc = uc;
        }
        //public ViewModelBase.DialogResult ShowDialog<T>(ViewModelBase viewModel) where T : FormService
        //{
        //    var form = _uc.Resolve<T>();
        //    return form.ShowDialog(viewModel);
        //}


        /// <summary>
        /// Shows a message box.
        /// </summary>
        /// <param name="ownerViewModel">
        /// A ViewModel that represents the owner window of the message box.
        /// </param>
        /// <param name="messageBoxText">A string that specifies the text to display.</param>
        /// <param name="caption">A string that specifies the title bar caption to display.</param>
        /// <param name="button">
        /// A MessageBoxButton value that specifies which button or buttons to display.
        /// </param>
        /// <param name="icon">A MessageBoxImage value that specifies the icon to display.</param>
        /// <returns>
        /// A MessageBoxResult value that specifies which message box button is clicked by the user.
        /// </returns>
        public ViewModelBase.DialogResult ShowMessageBox(
            string messageBoxText,
            string caption,
            MessageBoxButtons button,
            MessageBoxIcon icon)
        {
            Log.Information("Message={messageBoxText}",CallerInfo.Create(),messageBoxText);
            var rc = MessageBox.Show(messageBoxText, caption, button, icon).ToEnum<ViewModelBase.DialogResult>();
            Log.Information("MessageBox rc={rc}",CallerInfo.Create(), rc);
            return rc;
        }

        public ViewModelBase.DialogResult ShowMessageBox(string messageBoxText)
        {
            Log.Information("Message={messageBoxText}", CallerInfo.Create(), messageBoxText);
            var rc = MessageBox.Show(messageBoxText).ToEnum<ViewModelBase.DialogResult>();
            Log.Information("MessageBox rc={rc}", CallerInfo.Create(), rc);
            return rc;
        }

        public ViewModelBase.DialogResult ShowOpenFileDialog(IOpenFileDialog openFileDialog)
        {
            // OpenFileDialogMvvM openDlg = new OpenFileDialogMvvM(openFileDialog);
            return _openFileDialog.ShowDialog(openFileDialog).ToEnum<ViewModelBase.DialogResult>();
        }

        public ViewModelBase.DialogResult ShowSaveFileDialog(ISaveFileDialog saveFileDialog)
        {
            //SaveFileDialogMvvm saveDlg = new SaveFileDialogMvvm(saveFileDialog);
            return _saveFileDialog.ShowDialog(saveFileDialog).ToEnum<ViewModelBase.DialogResult>();
        }

        public ViewModelBase.DialogResult ShowFolderBrowserDialog(IFolderBrowserDialog folderBrowserDialog)
        {
            return _folderBrowser.ShowDialog(folderBrowserDialog).ToEnum<ViewModelBase.DialogResult>();
        }

        #endregion

    }
}
