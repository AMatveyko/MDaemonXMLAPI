using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using MDaemonXMLAPI.Model;
using MDaemonXMLAPI.Model.Xml;
using System.Windows;
using System.Windows.Threading;

namespace MDaemonXMLAPI.Cmd
{
    public class WriteListOnServerCmd : DelegateCommand
    {

        private bool _canExecute = true;

        public WriteListOnServerCmd( ViewModel viewModel )
            : base(viewModel) { }
        public override void Execute(object parameter)
        {
            _canExecute = false;
            WorkAsync(parameter);
        }

        private async void WorkAsync(object parameter)
        {
            await Task.Run(() =>
            {
                App.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => RaiseCanExecuteChanged() ));
                PasswordBox passwordBox = (parameter is PasswordBox) ? (PasswordBox)parameter : null;
                string password = passwordBox.Password;
                string userName = _viewModel.UserNameForMailServer;
                string host = _viewModel.MailServer;

                foreach (MailBox mailBox in _viewModel.NewMails)
                {
                    XmlGetUserInfoReq request = new XmlGetUserInfoReq(mailBox.Domain, mailBox.Name);
                    string result = ApiClient.Request(host, userName, password, request.ToString());
                    if (!XmlResponse.IsUserExist(result))
                    {
                        XmlCreateUserReq user = new XmlCreateUserReq(mailBox);
                        mailBox.IsOk();
                        mailBox.IsEdit = false;
                        ApiClient.Request(host, userName, password, user.ToString());
                        _viewModel.Logging($"добавлен {mailBox.Name}@{mailBox.Domain}");
                    }
                    else
                    {
                        mailBox.NoOk();
                        _viewModel.Logging($"{mailBox.Name}@{mailBox.Domain} уже существует");
                    }
                }
                _canExecute = true;
                App.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => RaiseCanExecuteChanged()));
            });
        }

        private string Convert(string str)
        {
            Encoding utf8 = Encoding.GetEncoding("UTF-8");
            Encoding win1251 = Encoding.GetEncoding("Windows-1251");
            byte[] win1251Bytes = utf8.GetBytes(str);
            byte[] utf8Bytes = Encoding.Convert(win1251, utf8, win1251Bytes);
            return utf8.GetString(utf8Bytes);

            //Encoding utf8 = Encoding.GetEncoding("UTF-8");
            //Encoding win1251 = Encoding.GetEncoding("Windows-1251");
            //byte[] utf8Bytes = win1251.GetBytes(str);
            //byte[] win1251Bytes = Encoding.Convert(utf8, win1251, utf8Bytes);
            //return win1251.GetString(win1251Bytes);
        }

        public override bool CanExecute(object parameter)
        {
            return ( (_viewModel.NewMails?.Count > 0) && (_canExecute) );
        }
    }
}
