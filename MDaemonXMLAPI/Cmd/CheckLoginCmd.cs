using System;
using MDaemonXMLAPI.Model;
using MDaemonXMLAPI.Model.Xml;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace MDaemonXMLAPI.Cmd
{
    public class CheckLoginCmd : DelegateCommand
    {

        private bool _isEnable = true;
        private string _password;
        private string _userName;
        private string _host;

        public CheckLoginCmd( ViewModel viewModel )
            : base(viewModel) { }
        public override void Execute(object parameter)
        {
            _isEnable = false;
            RaiseCanExecuteChanged();
            PasswordBox passwordBox = ( parameter is PasswordBox) ? (PasswordBox)parameter : null;
            _viewModel.PasswordBox = passwordBox;
            _password = passwordBox.Password;
            _userName = _viewModel.UserNameForMailServer;
            _host = _viewModel.MailServer;

            XmlGetDomainListReq request = new XmlGetDomainListReq();
            string result = ApiClient.Request(_host, _userName, _password, request.ToString());
            _viewModel.DomainList = XmlResponse.GetDomainList(result, _viewModel);
            if (_viewModel.DomainList.Count > 0)
                _viewModel.SelectedDomain = _viewModel.DomainList[0];
            GetAllMailBoxAsync();
        }

        private async void GetAllMailBoxAsync()
        {
            _viewModel.SelectedDomainInfoUser = "*";
            await Task.Run(() =>
            {
            App.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => _viewModel.AllMailBox.Clear() ));
                foreach (string domainName in _viewModel.DomainList)
                {
                    XmlGetDomainInfoReq request = new XmlGetDomainInfoReq(domainName);
                    string result = ApiClient.Request(_host, _userName, _password, request.ToString());
                    foreach (var mailBox in XmlResponse.GetUserList(result, domainName))
                        App.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => _viewModel.AllMailBox.Add(mailBox)));
                }
                _isEnable = true;
                App.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => RaiseCanExecuteChanged() ));
            });
        }

        public override bool CanExecute(object parameter)
        {
            return _isEnable;
        }
    }
}
