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
    public class UpdUserOnSrvCmd : DelegateCommand
    {
        public UpdUserOnSrvCmd( ViewModel viewModel )
            : base(viewModel) { }
        public override void Execute(object parameter)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены?", "Обновить пользователя на сервере.", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.No)
                return;
            var password = _viewModel.PasswordBox.Password;
            var userName = _viewModel.UserNameForMailServer;
            var host = _viewModel.MailServer;
            var request = new XmlUpdUserReq(_viewModel.SelectedMailBox);
            System.Windows.MessageBox.Show(request.ToString());
            var response = ApiClient.Request(host, userName, password, request.ToString());
            System.Windows.MessageBox.Show(response);
        }

        public override bool CanExecute(object parameter)
        {
            return (_viewModel.SelectedMailBox != null);
        }
    }
}
