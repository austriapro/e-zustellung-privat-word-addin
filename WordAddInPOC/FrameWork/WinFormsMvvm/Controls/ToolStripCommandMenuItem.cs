﻿using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Windows.Input;
using LogService;

namespace WinFormsMvvm.Controls
{
    [System.ComponentModel.DesignerCategory("")]
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)]
    public class ToolStripCommandMenuItem : ToolStripMenuItem, IBindableComponent
    {
        private ICommand _command;
        private ControlBindingsCollection _dataBindings;
        private BindingContext _bindingContext;

        [DefaultValue(null)]
        [Browsable(false)]
        [Bindable(true)]
        public ICommand Command
        {
            get { return _command; }
            set
            {
                if (_command == value)
                    return;
                SetCommand(value);
            }
        }

        private void SetCommand(ICommand command)
        {
            if (_command != null)
            {
                _command.CanExecuteChanged -= CommandOnCanExecuteChanged;
            }

            _command = command;

            if (_command != null)
            {
                Enabled = command.CanExecute(null);
                _command.CanExecuteChanged += CommandOnCanExecuteChanged;
            }
        }

        protected override void OnClick(EventArgs e)
        {
            
            base.OnClick(e);
            if (_command != null && _command.CanExecute(null))
            {
                try
                {
                    Log.Information("Execute {0} in {1}", CallerInfo.Create(), this.Name, this.GetType().FullName);

                    _command.Execute(null);

                }
                catch (Exception ex)
                {
                    Log.Error(ex,CallerInfo.Create(),"OnClick: "+this.Name);
                }
            }
        }

        private void CommandOnCanExecuteChanged(object sender, EventArgs eventArgs)
        {
            if (_command != null)
            {
                Enabled = _command.CanExecute(null);
            }
        }

        #region IBindableComponent

        [Category("Data")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [RefreshProperties(RefreshProperties.All)]
        [ParenthesizePropertyName(true)]
        [Description("The data bindings for the controls.")]
        public ControlBindingsCollection DataBindings
        {
            get
            {
                return _dataBindings = _dataBindings ?? new ControlBindingsCollection(this);
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public BindingContext BindingContext
        {
            get
            {
                return _bindingContext = _bindingContext ?? new BindingContext();
            }
            set { _bindingContext = value; }
        }

        #endregion
    }
}
