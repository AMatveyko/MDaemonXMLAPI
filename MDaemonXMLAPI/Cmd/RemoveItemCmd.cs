using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using MDaemonXMLAPI.Model;
using MDaemonXMLAPI.Model.Xml;
using System.Windows;

namespace MDaemonXMLAPI.Cmd
{
    public class RemoveItemCmd : DelegateCommand
    {
        public RemoveItemCmd( ViewModel viewModel )
            : base(viewModel) { }

        public override void Execute(object parameter)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены?","Удаление элемента из списка.",MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                MailBox mailBox = parameter as MailBox;
                if (mailBox != null)
                    _viewModel.NewMails.Remove(mailBox);
            }
        }
    }
}
