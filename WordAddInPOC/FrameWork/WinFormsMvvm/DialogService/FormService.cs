using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
//using ExtensionMethods;
using WinFormsMvvm;
using WinFormsMvvm.Controls;
using WinFormsMvvm.ExtensionMethods;
//using LogService;
using LogService;


namespace WinFormsMvvm.DialogService
{
    public class FormService : Form
    {
        protected object ViewModel;
        protected List<BindingSource> Bindings;
        private Control _currentControl;
        public FormService()
            : base()
        {
            Load += LoadEvent;
            //FormClosing += FormClosingEvent;
            Bindings = new List<BindingSource>();
            this.StartPosition = FormStartPosition.CenterScreen;
            //this.Visible = true;
            //if ( == null)
            //{
            //    Logger.InitLog();
            //}

            Log.Information("Instanzierung", CallerInfo.Create(), "");
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            FormClosingEvent(e);
            base.OnFormClosing(e);
        }

        //private void FormClosingEvent(object sender, FormClosingEventArgs e)
        private void FormClosingEvent(FormClosingEventArgs e)
        {

            bool cancelClose = false;
            e.Cancel = false;
            try
            {
                if (ViewModel != null)
                {
                    //if (((ViewModelBase)ViewModel).DialogRC != null)
                    //{
                    // this.DialogResult = (DialogResult)((ViewModelBase)ViewModel).DialogRC;
                    //((ViewModelBase)ViewModel).ClearDialogResult();
                    //}
                    cancelClose = ((ViewModelBase)ViewModel).OnFormsClosing(this.DialogResult, this.Text);
                }
                else
                {
                    Log.Warning("Viewmodel is null", CallerInfo.Create(), "");
                }
                if (cancelClose)
                {
                    e.Cancel = true;
                }
                else
                {
                    //HandleBindings();
                }

            }
            catch (Exception ex)
            {

                Log.Error(ex, CallerInfo.Create(), "Forms Close failed");
            }            //base.OnFormClosing(e);
        }

        private void HandleBindings()
        {
            foreach (BindingSource binding in Bindings)
            {
                if (this.DialogResult == DialogResult.Cancel)
                {
                    binding.CancelEdit();
                }
                else
                {
                    //WriteAllValues();
                    binding.EndEdit();
                }
            }
        }

        private void LoadEvent(object sender, EventArgs e)
        {
            if (!(ViewModel is ViewModelBase))
            {
                return;
            }

            ((ViewModelBase)ViewModel).CurrentForm = this;
            Log.Information("entry", CallerInfo.Create(), "");
            // SetupTracking();
            SetupWriteDatabinding();
        }

        void FormService_CloseForm(object sender, EventArgs e)
        {
            base.DialogResult = ((ViewModelBase)ViewModel).DialogRC.ToEnum<DialogResult>() != DialogResult ?
                                ((ViewModelBase)ViewModel).DialogRC.ToEnum<DialogResult>() : this.DialogResult;
            this.Close();
        }

        public FormService(ViewModelBase viewModel)
            : this()
        {

            ViewModel = viewModel;
            ((ViewModelBase)ViewModel).CurrentForm = this;
        }

        private void SetupWriteDatabinding()
        {
            foreach (Control sender in Controls)
            {
                if (sender is MultiColumnComboBox || sender is ComboBox) // || sender is DateTimePicker)
                {
                    //((ComboBox)sender).SelectedIndexChanged += FormService_SelectedIndexChanged;
                    //((ComboBox)sender).SelectedValueChanged += FormService_SelectedValueChanged;
                }
            }
            foreach (CommandButton item in Controls.OfType<CommandButton>())
            {
                ((CommandButton)item).Click += FormService_Click;

            }
        }

        protected void FormService_SelectedValueChanged(object sender, EventArgs e)
        {
            foreach (Binding item in ((Control)sender).DataBindings)
            {
                item.WriteValue();
            }
        }

        void FormService_Click(object sender, EventArgs e)
        {
            if ((ViewModelBase)ViewModel != null)
            {
                if (((ViewModelBase)ViewModel).IsClosing)
                {
                    DialogResult = (System.Windows.Forms.DialogResult)((ViewModelBase)ViewModel).DialogRC;
                    Close();
                }
            }
        }

        protected void FormService_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (Binding item in ((Control)sender).DataBindings)
            {
                item.WriteValue();
            }
        }
        //public DialogResult ShowDialog(ViewModelBase viewModel)
        //{
        //    ViewModel = viewModel;
        //    // SetBindingSource(ViewModel);
        //    return ShowDialog();
        //}

        protected void ExecuteRelayCommand(RelayCommand cmd)
        {
            if (cmd != null && cmd.CanExecute(null))
            {
                //WriteAllValues();
                cmd.Execute(null);
            }
        }

        private void SetupTracking()
        {
            AddEvent(Controls);
        }

        private void AddEvent(Control.ControlCollection controls)
        {
            if (controls.Count == 0)
            {
                return;
            }
            foreach (Control sender in controls)
            {
                if (sender.Controls.Count > 0)
                    AddEvent(sender.Controls);
                else
                {
                    if (sender is TextBox || sender is MultiColumnComboBox || sender is ComboBox ||
                        sender is DateTimePicker)
                    {
                        sender.Enter += ControlEntering;
                    }
                }
            }

        }

        private void WriteAllValues()
        {
            if (_currentControl == null) return;
            foreach (Binding binding in _currentControl.DataBindings)
            {
                binding.WriteValue();
            }
        }

        public virtual void SetBindingSource(object bindSrc)
        {
            ViewModel = bindSrc;
        }

        private void ControlEntering(object sender, EventArgs e)
        {
            if (sender is TextBox || sender is MultiColumnComboBox || sender is ComboBox || sender is DateTimePicker)
            {
                _currentControl = (Control)sender;
            }
        }

    }
}
