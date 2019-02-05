using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using MDaemonXMLAPI.Model;
using MDaemonXMLAPI.Model.Xml;
using System.Windows;

namespace MDaemonXMLAPI.Cmd
{
    public class AddNewBoxCmd : DelegateCommand
    {

        private int passwordLength;
        private string name;
        private string domain;

        public AddNewBoxCmd( ViewModel viewModel )
            : base(viewModel) { }

        public override void Execute(object parameter)
        {
            name = _viewModel.CreateNewUserName;
            domain = _viewModel.SelectedDomain;

            if (!CheckError())
            {
                string newName = $"{name}";
                if (_viewModel.NewMails == null)
                    _viewModel.NewMails = new ObservableCollection<MailBox>();
                if (Checks.NoDublicate(newName, domain, _viewModel.NewMails))
                {
                    string password;
                    password = PasGen.Gen(true, passwordLength);
                    MailBox newMailBox = new MailBox(newName, domain, password);
                    _viewModel.NewMails.Add(newMailBox);
                }
                else
                    System.Windows.MessageBox.Show("Такой ящик уже есть в списке");
            }
        }

        private bool CheckError()
        {
            bool error = false;
            if(!Checks.MailBoxName(name))
            {
                string errorString;
                errorString =  $"Не верное имя!.\n";
                errorString += $"Имя может содержать только буквы латинского алфавита,цифры, тире и точку.";
                errorString += $"Тире или тока не могут быть последним или первым символом";
                System.Windows.MessageBox.Show(errorString);
                error = true;
            }

            int maxLength = 16;
            int minLength = 8;
            int defLenght = 10;

            if(!Checks.Password(_viewModel.CreateNewPasswordLength, out passwordLength,defLenght,maxLength,minLength))
            {
                string errorString;
                errorString =  $"Вы ввели не верно длину пароля.\n";
                errorString += $"Длина пароля вводится только цифрами и должна быть в диапазоне от { minLength} до { maxLength}\n";
                errorString += $"Пароль будет сгенерирован длиной {defLenght} символов";
                System.Windows.MessageBox.Show(errorString);
                _viewModel.PasswordLength = passwordLength.ToString();
            }
            return error;
        }
    }
}
