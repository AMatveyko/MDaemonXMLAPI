using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MDaemonXMLAPI.Model;
using MDaemonXMLAPI.Model.Xml;

namespace MDaemonXMLAPI.Cmd
{
    public class ClearListCmd : DelegateCommand
    {
        public ClearListCmd( ViewModel viewModel )
            : base(viewModel) { }
        public override void Execute(object parameter)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены?", "Очистка всего списка.", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
                _viewModel.NewMails.Clear();
        }

        public override bool CanExecute(object parameter)
        {
            return (_viewModel.NewMails?.Count > 0);
        }
    }
}
