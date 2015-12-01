using System;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Windows;
using System.Windows.Forms;
using Microsoft.Practices.Unity;
using WinFormsMvvm.DialogService.FrameworkDialogs.FolderBrowse;
using WinFormsMvvm.DialogService.FrameworkDialogs.OpenFile;
using WinFormsMvvm.DialogService.FrameworkDialogs.SaveFile;

namespace WinFormsMvvm.DialogService
{
    /// <summary>
    /// Interface responsible for abstracting ViewModels from Views.
    /// </summary>

    public interface IDialogService
    {
        /// <summary>
        /// Sets the wait cursor.
        /// </summary>
        void SetWaitCursor();
        /// <summary>
        /// Resets the cursor.
        /// </summary>
        void ResetCursor();

        /// <summary>
        /// Shows a dialog.
        /// </summary>
        /// <remarks>
        /// The dialog used to represent the ViewModel is retrieved from the registered mappings.
        /// </remarks>
        /// <param name="ownerViewModel">
        /// A ViewModel that represents the owner window of the dialog.
        /// </param>
        /// <param name="viewModel">The ViewModel of the new dialog.</param>
        /// <returns>
        /// A nullable value of type bool that signifies how a window was closed by the user.
        /// </returns>

        // ViewModelBase.DialogResult ShowDialog();

        // ViewModelBase.DialogResult RunProgressBar(ViewModelBase viewModel);
        ViewModelBase.DialogResult ShowDialog<T, T2>()
            where T : FormService
            where T2 : ViewModelBase;
        
        void Show<T, T2>()
            where T : FormService
            where T2 : ViewModelBase;
        ViewModelBase.DialogResult ShowDialog<T>(object viewModel) where T : FormService;

        //ViewModelBase.DialogResult ShowDialog<T>() where T : FormService;
        void UpdateUnityContainer(IUnityContainer uc);

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
        ViewModelBase.DialogResult ShowMessageBox(
            string messageBoxText,
            string caption,
            MessageBoxButtons button,
            MessageBoxIcon icon);

        ViewModelBase.DialogResult ShowMessageBox(string messageBoxText);

        /// <summary>
        /// Shows the OpenFileDialog.
        /// </summary>
        /// <param name="ownerViewModel">
        /// A ViewModel that represents the owner window of the dialog.
        /// </param>
        /// <param name="openFileDialog">The interface of a open file dialog.</param>
        /// <returns>ViewModelBase.DialogResult.OK if successful; otherwise ViewModelBase.DialogResult.Cancel.</returns>
        ViewModelBase.DialogResult ShowOpenFileDialog(IOpenFileDialog openFileDialog);

        ViewModelBase.DialogResult ShowSaveFileDialog(ISaveFileDialog saveFileDialog);

        /// <summary>
        /// Shows the FolderBrowserDialog.
        /// </summary>
        /// <param name="ownerViewModel">
        /// A ViewModel that represents the owner window of the dialog.
        /// </param>
        /// <param name="folderBrowserDialog">The interface of a folder browser dialog.</param>
        /// <returns>The ViewModelBase.DialogResult.OK if successful; otherwise ViewModelBase.DialogResult.Cancel.</returns>
        ViewModelBase.DialogResult ShowFolderBrowserDialog(IFolderBrowserDialog folderBrowserDialog);
    }
}
