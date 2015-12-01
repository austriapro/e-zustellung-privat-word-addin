using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
//using SimpleEventBroker;
//using LogService;
using WinFormsMvvm.DialogService;
using System.Windows.Forms;
using LogService;
using WinFormsMvvm.Controls;

namespace WinFormsMvvm
{
    /// <summary>
    /// Base class for all ViewModel classes in the application.
    /// It provides support for property change notifications 
    /// and has a DisplayName property.  This class is abstract.
    /// See: http://msdn.microsoft.com/en-us/magazine/dd419663.aspx
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged, IDisposable, IEditableObject
    {
        #region Constructor
        protected IDialogService _dlg;
        protected ViewModelBase()
        {
            // ChangePending = false;
        }

        protected ViewModelBase(IDialogService dlg)
        {
            _dlg = dlg;
            IsClosing = false;
        }
        #endregion // Constructor

        #region DisplayName

        /// <summary>
        /// Returns the user-friendly name of this object.
        /// Child classes can set this property to a new value,
        /// or override it to determine the value on-demand.
        /// </summary>
        public virtual string DisplayName { get; protected set; }

        #endregion // DisplayName

        #region Debugging Aides

        /// <summary>
        /// Warns the developer if this object does not have
        /// a public property with the specified name. This 
        /// method does not exist in a Release build.
        /// </summary>
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public void VerifyPropertyName(string propertyName)
        {
            // Verify that the property name matches a real,  
            // public, instance property on this object.
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
            {
                string msg = "Invalid property name: " + propertyName;

                if (this.ThrowOnInvalidPropertyName)
                    throw new Exception(msg);
                else
                    Debug.Fail(msg);
            }
        }

        /// <summary>
        /// Returns whether an exception is thrown, or if a Debug.Fail() is used
        /// when an invalid property name is passed to the VerifyPropertyName method.
        /// The default value is false, but subclasses used by unit tests might 
        /// override this property's getter to return true.
        /// </summary>
        protected virtual bool ThrowOnInvalidPropertyName { get; private set; }

        #endregion // Debugging Aides

        #region INotifyPropertyChanged Members

        /// <summary>
        /// Raised when a property on this object has a new value.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private bool _changePending;
        public bool ChangePending
        {
            get
            {
                Log.Verbose("Changepending={0}", CallerInfo.Create(), _changePending.ToString());
                return _changePending;
            }
            set
            {
                _changePending = value;
                Log.Verbose("Changepending={0}", CallerInfo.Create(), _changePending.ToString());
                OnPropertyChanged();
            }
        }
        #endregion // INotifyPropertyChanged Members

        public object CurrentForm;

        // Zusammenfassung:
        //     Gibt Bezeichner an, die den Rückgabewert eines Dialogfelds angeben.
        public enum DialogResult
        {
            // Zusammenfassung:
            //     Nothing wird vom Dialogfeld zurückgegeben. Dies bedeutet, dass das modale
            //     Dialogfeld weiterhin ausgeführt wird.
            None = 0,
            //
            // Zusammenfassung:
            //     Der Rückgabewert des Dialogfelds ist OK (üblicherweise von der Schaltfläche
            //     OK gesendet).
            OK = 1,
            //
            // Zusammenfassung:
            //     Der Rückgabewert des Dialogfelds ist Cancel (üblicherweise von der Schaltfläche
            //     Abbrechen gesendet).
            Cancel = 2,
            //
            // Zusammenfassung:
            //     Der Rückgabewert des Dialogfelds ist Abort (üblicherweise von der Schaltfläche
            //     Abbrechen gesendet).
            Abort = 3,
            //
            // Zusammenfassung:
            //     Der Rückgabewert des Dialogfelds ist Retry (üblicherweise von der Schaltfläche
            //     Wiederholen gesendet).
            Retry = 4,
            //
            // Zusammenfassung:
            //     Der Rückgabewert des Dialogfelds ist Ignore (üblicherweise von der Schaltfläche
            //     Ignorieren gesendet).
            Ignore = 5,
            //
            // Zusammenfassung:
            //     Der Rückgabewert des Dialogfelds ist Yes (üblicherweise von der Schaltfläche
            //     Ja gesendet).
            Yes = 6,
            //
            // Zusammenfassung:
            //     Der Rückgabewert des Dialogfelds ist No (üblicherweise von der Schaltfläche
            //     Nein gesendet).
            No = 7,
        }
        public bool IsClosing {get;private set;}

        public DialogResult DialogRC;
        public void ClearDialogResult()
        {
            DialogRC = DialogResult.None;
        }

        public void CloseView(DialogResult dialogResult)
        {
            DialogRC = dialogResult;
            IsClosing = true;
        }

        /// <summary>
        /// Raises this object's PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">The property that has a new value.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            //  this.VerifyPropertyName(propertyName);

            //Log.Write("entered for " + (propertyName ?? "(null)"));
            Log.Verbose("entered for '{0}'", CallerInfo.Create(), propertyName);
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                if (propertyName!="ChangePending")
                {
                    _changePending = true;
                } 
                
                var e = new PropertyChangedEventArgs(propertyName);
                bool invokeRequired = false;
                if (CurrentForm != null)
                {
                    if ((CurrentForm is FormService) || (CurrentForm is Form))
                    {
                        invokeRequired = ((Form)CurrentForm).InvokeRequired;
                        if (invokeRequired)
                        {
                            ((Form)CurrentForm).Invoke((MethodInvoker)delegate
                             {
                                 handler(this, e);
                             });

                        }
                    }
                    if (CurrentForm is UserControl)
                    {
                        invokeRequired = ((UserControl)CurrentForm).InvokeRequired;
                        if (invokeRequired)
                        {
                            ((UserControl)CurrentForm).Invoke((MethodInvoker)delegate
                             {
                                 handler(this, e);
                             });
                        }
                    }
                }
                if (!invokeRequired)
                {
                    handler(this, e);
                }
            }
            Log.Verbose("Exiting for '{0}'", CallerInfo.Create(), propertyName);
        }

        public virtual bool OnFormsClosing(System.Windows.Forms.DialogResult resultFromForm, string title)
        {
            Log.Debug("entry for {0}", CallerInfo.Create(), title);
            bool cancelClose = false;
            if (_dlg == null)
            {
                Log.Error("IDialogService not initialized, Form='{0}'", CallerInfo.Create(), title);
                return false;
            }
            if (resultFromForm == System.Windows.Forms.DialogResult.OK)
            {
                ChangePending = false;
            }
            else
            {
                cancelClose = ChangePendingDialog(title);
            }
            Log.Debug("exit, rc={0}", CallerInfo.Create(), cancelClose.ToString());
            return cancelClose;

        }

        /// <summary>
        /// The Changes pending dialog.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <returns>true=Cancel changes, false=Continue to close form</returns>
        public bool ChangePendingDialog(string title)
        {
            bool cancelClose = false;
            if (ChangePending)
            {
                DialogResult rc = _dlg.ShowMessageBox("Das Formular enthält möglicherweise Daten, die noch nicht gespeichert wurden. Wollen Sie die Änderungen verwerfen?",
                    title, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (rc == DialogResult.No)
                {
                    cancelClose = true;
                }
            } 
            return cancelClose;
        }

        //public delegate void CloseFormEventHandler(object sender, EventArgs e);
        //public event CloseFormEventHandler IsClosing;

        //public void SetCloseView()
        //{
        //    //if (IsClosing != null)
        //    //{
        //    //    EventArgs e = new EventArgs();
        //    //    IsClosing(this, e);
        //    //}
        //    IsClosing = true;
        //}

        #region IDisposable Members

        /// <summary>
        /// Invoked when this object is being removed from the application
        /// and will be subject to garbage collection.
        /// </summary>
        public void Dispose()
        {
            this.OnDispose();
        }

        /// <summary>
        /// Child classes can override this method to perform 
        /// clean-up logic, such as removing event handlers.
        /// </summary>
        protected virtual void OnDispose()
        {
        }

#if DEBUG
        /// <summary>
        /// Useful for ensuring that ViewModel objects are properly garbage collected.
        /// </summary>
        ~ViewModelBase()
        {
            //string msg = string.Format("{0} ({1}) ({2}) Finalized", this.GetType().Name, this.DisplayName, this.GetHashCode());
            //Log.Debug(msg);
        }
#endif

        #endregion // IDisposable Members

        #region Binding specific handling

        private Hashtable props = null;
        // private bool _isEditing = false;

        public void BeginEdit()
        {
            if (props != null)
                return;
            List<PropertyInfo> properties =
                (this.GetType()).GetProperties(BindingFlags.Public | BindingFlags.Instance).ToList();
            props = new Hashtable();
            foreach (PropertyInfo info in properties)
            {
                if (info.GetSetMethod() != null)
                {
                    object value = info.GetValue(this, null);
                    props.Add(info.Name, value);
                }
            }
        }

        public void EndEdit()
        {
            props = null;
        }

        public void CancelEdit()
        {
            if (props == null)
                return;
            List<PropertyInfo> properties =
                (this.GetType()).GetProperties(BindingFlags.Public | BindingFlags.Instance).ToList();
            foreach (PropertyInfo info in properties)
            {
                if (info.Name != "ChangePending" && info.GetSetMethod() != null)
                {
                    object value = props[info.Name];
                    info.SetValue(this, value, null);
                }
            }
            props = null;
        }

        #endregion

        #region Cancel
        private RelayCommand _CancelCommand;
        public virtual RelayCommand CancelCommand
        {
            get
            {
                _CancelCommand = _CancelCommand ?? new RelayCommand(CancelClick);
                return _CancelCommand;
            }
        }
        private void CancelClick(object obj)
        {
            CloseView(DialogResult.Abort);
            IsClosing = true;
        }
        #endregion

    }

    //public class DialogResultOveride
    //{
    //    public DialogResult DialogResult = DialogResult.None;
    //}
}