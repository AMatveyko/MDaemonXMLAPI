using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MDaemonXMLAPI.Cmd
{
    public abstract class DelegateCommand : ICommand
    {

        public event EventHandler CanExecuteChanged;

        protected ViewModel _viewModel;

        public DelegateCommand(ViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public virtual bool CanExecute(object parameter)
            => true;

        public abstract void Execute(object parameter);

        public virtual void RaiseCanExecuteChanged()
            => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

}
