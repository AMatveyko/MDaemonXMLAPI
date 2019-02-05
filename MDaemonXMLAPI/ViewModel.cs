using MDaemonXMLAPI.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDaemonXMLAPI.Cmd;
using System.ComponentModel;
using System.Collections.Specialized;
using System.Windows.Controls;

namespace MDaemonXMLAPI
{
    public class ViewModel : INotifyPropertyChanged, IForLogging
    {

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region View


        /// <summary>
        /// new string
        /// </summary>

        public PasswordBox PasswordBox { get; set; }


        private string _log;
        public string Log
        {
            get => _log;
            set
            {
                _log = value;
                OnPropertyChanged(nameof(Log));
            }
        }

        public string MailServer { get; set; }
        public string UserNameForMailServer { get; set; }
        public string UserPasswordForMailServer { get; set; }
        private ObservableCollection<MailBox> _newMails;
        public ObservableCollection<MailBox> NewMails
        {
            get => _newMails;
            set
            {
                _newMails = value;
                OnPropertyChanged(nameof(NewMails));
                ClearListCmd.RaiseCanExecuteChanged();
                WriteListOnServerCmd.RaiseCanExecuteChanged();
                NewMails.CollectionChanged += (o, e) =>
                {
                    ClearListCmd.RaiseCanExecuteChanged();
                    WriteListOnServerCmd.RaiseCanExecuteChanged();
                };
            }
        }

        public bool IsEnabledCreateUsers
        {
            get => _domainList?.Count > 0;
            set => OnPropertyChanged(nameof(IsEnabledCreateUsers));
        }
        private List<string> _domainList;
        public List<string> DomainList
        {
            get => _domainList;
            set
            {
                _domainList = value;
                IsEnabledCreateUsers = true;
                DomainListInfoUsers = value;
                OnPropertyChanged(nameof(DomainList));
            }
        }
        private string _selectedDomain;
        public string SelectedDomain
        {
            get => _selectedDomain;
            set
            {
                _selectedDomain = value;
                OnPropertyChanged(nameof(SelectedDomain));
            }
        }
        private List<string> _domainListInfoUsers;
        public List<string> DomainListInfoUsers
        {
            get => _domainListInfoUsers;
            set
            {
                _domainListInfoUsers = new List<string>() { "*" };
                _domainListInfoUsers.AddRange(value);
                OnPropertyChanged(nameof(DomainListInfoUsers));
            }
        }
        private string _selectedDomainInfoUser;
        public string SelectedDomainInfoUser
        {
            get => _selectedDomainInfoUser;
            set
            {
                _selectedDomainInfoUser = value;
                OnPropertyChanged(nameof(SelectedDomainInfoUser));
                FilteringAllMailBox();
            }
        }

        private string _filterNameAllMailBox;
        public string FilterNameAllMailBox
        {
            get => _filterNameAllMailBox;
            set
            {
                _filterNameAllMailBox = value;
                FilteringAllMailBox();
            }
        }

        public string CreateNewUserName { get; set; }

        public ObservableCollection<MailBox> FilteredAllMailBox { get; set; } = new ObservableCollection<MailBox>();

        private ObservableCollection<MailBox> _allMailBox;
        public ObservableCollection<MailBox> AllMailBox { get => _allMailBox; set => _allMailBox = value; }

        private MailBox _selectedMailBox;
        public MailBox SelectedMailBox
        {
            get
            {
                return _selectedMailBox;
            }
            set
            {
                MDaemonXMLAPI.Model.Xml.XmlResponse.FillingUserInfoAsync(value, this);
                _selectedMailBox = value;
                OnPropertyChanged(nameof(SelectedMailBox));
            }
        }

        private bool _isReadUserInfo;
        public bool IsReadUserInfo
        {
            get => _isReadUserInfo;
            set
            {
                _isReadUserInfo = value;
                OnPropertyChanged(nameof(IsReadUserInfo));
            }
        }

        public string UserNameTemplate { get; set; }

        private string _mailBoxesCount;
        public string MailBoxesCount
        {
            get => _mailBoxesCount;
            set
            {
                _mailBoxesCount = value;
                OnPropertyChanged(nameof(MailBoxesCount));
            }
        }

        private string _passwordLength;
        public string PasswordLength
        {
            get => _passwordLength;
            set
            {
                _passwordLength = value;
                OnPropertyChanged(nameof(PasswordLength));
            }
        }
        public bool SpecSimvoly { get; set; }
        public string CreateNewPasswordLength { get; set; }

        #region Cmd
        private CheckLoginCmd _checkLoginCmd;
        public CheckLoginCmd CheckLoginCmd { get => _checkLoginCmd ?? (_checkLoginCmd = new CheckLoginCmd(this)); }
        private GenNewBoxesCmd _genNewBoxesCmd;
        public GenNewBoxesCmd GenNewBoxesCmd { get => _genNewBoxesCmd ?? (_genNewBoxesCmd = new GenNewBoxesCmd(this)); }
        private AddNewBoxCmd _addNewBoxCmd;
        public AddNewBoxCmd AddNewBoxCmd { get => _addNewBoxCmd ?? (_addNewBoxCmd = new AddNewBoxCmd(this)); }
        private RemoveItemCmd _removeItemCmd;
        public RemoveItemCmd RemoveItemCmd { get => _removeItemCmd ?? (_removeItemCmd = new RemoveItemCmd(this)); }
        private ClearListCmd _clearListCmd;
        public ClearListCmd ClearListCmd { get => _clearListCmd ?? (_clearListCmd = new ClearListCmd(this)); }
        private WriteListOnServerCmd _writeListOnServerCmd;
        public WriteListOnServerCmd WriteListOnServerCmd { get => _writeListOnServerCmd ?? (_writeListOnServerCmd = new WriteListOnServerCmd(this)); }
        private WriteListToFileCmd _writeNewMailBoxToFileCmd;
        public WriteListToFileCmd WriteNewMailBoxToFileCmd { get => _writeNewMailBoxToFileCmd ?? (_writeNewMailBoxToFileCmd = new WriteListToFileCmd(this)); }
        #endregion

        #endregion

        public ViewModel()
        {
            MailServer = "91.226.133.9";
            UserNameForMailServer = "userForApiTest@bg-corp.net";
            UserNameTemplate = "mailBox";
            MailBoxesCount = "10";
            _passwordLength = "10";
            SpecSimvoly = true;
            _selectedDomainInfoUser = "*";
            _allMailBox = new ObservableCollection<MailBox>();
            IsReadUserInfo = false;
            AllMailBox.CollectionChanged += FilteringAllMailBox;
            FilterNameAllMailBox = "";
            CreateNewPasswordLength = "10";
        }

        public void Logging(string str)
        {
            Log = $"{str}\n" + Log;
        }

        private void FilteringAllMailBox(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    FilteredAllMailBox.Add(e.NewItems[0] as MailBox);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    FilteredAllMailBox.Remove(e.NewItems[0] as MailBox);
                    break;
                case NotifyCollectionChangedAction.Reset:
                    FilteredAllMailBox.Clear();
                    break;
            }
        }

        private void FilteringAllMailBox()
        {
            FilteredAllMailBox.Clear();
            string domainName;
            string name = FilterNameAllMailBox.ToLower();
            if (SelectedDomainInfoUser != "*")
                domainName = SelectedDomainInfoUser;
            else
                domainName = "";
            foreach (MailBox mailBox in AllMailBox.Where(x => x.Domain.Contains(domainName)).Where(x=>x.Name.ToLower().Contains(name)))
            {
                FilteredAllMailBox.Add(mailBox);
            }
        }
    }
}
