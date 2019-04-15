using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MDaemonXMLAPI.Model
{
    public class MailBox : INotifyPropertyChanged
    {
        private bool _isEdit;
        public bool IsEdit
        {
            get => _isEdit;
            set
            {
                _isEdit = value;
                OnPropertyChanged(nameof(IsEdit));
            }
        }
        public string Name { get; set; }
        public string Mailbox { get => Name; }
        public string Domain { get; set; }
        public string Email { get => String.Format($"{Name}@{Domain}"); }
        public string FullName { get => String.Format($"{FirstName} {LastName}"); }
        public string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }
        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }
        private string _frozen;
        public string Frozen
        {
            get => _frozen;
            set
            {
                _frozen = value;
                OnPropertyChanged(nameof(Frozen));
            }
        }

        private string _disabled;
        public string Disabled
        {
            get => _disabled;
            set
            {
                _disabled = value;
                OnPropertyChanged(nameof(Disabled));
            }
        }
        private string _mailDir;
        public string MailDir
        {
            get => _mailDir;
            set
            {
                _mailDir = value;
                OnPropertyChanged(nameof(MailDir));
            }
        }
        public string MustChangepassword { get; protected set; } = "No";
        public string DontExpirePassword { get; protected set; } = "Yes";
        private string _descriptions;
        public string Descriptions
        {
            get => _descriptions;
            set
            {
                _descriptions = value;
                OnPropertyChanged(nameof(Descriptions));
            }
        }
        public string AdminNotes { get; protected set; } = "";
        public string Title { get; protected set; } = "";
        public string MiddleName { get; protected set; } = "";
        public string Suffix { get; protected set; } = "";

        private Brush _color = Brushes.LightGray;

        public Brush Color
        {
            get => _color;
            set
            {
                _color = value;
                OnPropertyChanged(nameof(Color));
            }
        }

        public MailBox(string name, string domain, string password="", string username = "", string userSurname = "")
        {
            Name = name;
            Domain = domain;
            Password = password;
            FirstName = (username == "")? name : username;
            LastName = userSurname;
            IsEdit = true;
            _descriptions = "";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void IsOk()
            => Color = Brushes.Azure;
        public void NoOk()
            => Color = Brushes.LightPink;
    }
}
