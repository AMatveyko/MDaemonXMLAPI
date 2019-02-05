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
    public class GenNewBoxesCmd : DelegateCommand
    {

        private int count;
        private int passwordLength;
        private bool specInPass;
        private string password;
        private string name;
        private string domain;

        public GenNewBoxesCmd( ViewModel viewModel )
            : base(viewModel) { }

        public override void Execute(object parameter)
        {
            if (!CheckError())
            {
                if (_viewModel.NewMails == null)
                    _viewModel.NewMails = new ObservableCollection<MailBox>();
                int postfixName = 1;
                for (int i = 1; i <= count; i++)
                {
                    string newName = $"{name}{postfixName}";
                    if (Checks.NoDublicate(newName, domain, _viewModel.NewMails))
                    {
                        password = PasGen.Gen(specInPass, passwordLength);
                        MailBox mailBox = new MailBox(newName, domain, password);
                        _viewModel.NewMails.Add(mailBox);
                    }
                    else
                        i--;
                    postfixName++;
                }
            }
        }

        private bool CheckError()
        {
            bool error = false;
                count = 0;
                passwordLength = 0;
                specInPass = _viewModel.SpecSimvoly;
                password = String.Empty;
                name = _viewModel.UserNameTemplate;
                domain = _viewModel.SelectedDomain;

                if (!Int32.TryParse(_viewModel.MailBoxesCount, out count))
                {
                    System.Windows.MessageBox.Show("Введите верное количество!\n От 1 до 100 .");
                    error = true;
                }
                if (count > 100)
                {
                    System.Windows.MessageBox.Show("Будет сгенерированно 100 ящиков.");
                    count = 100;
                    _viewModel.MailBoxesCount = count.ToString();
                }
                else if (count <= 0)
                {
                    System.Windows.MessageBox.Show("Как можно сгенерировать 0 или меньше нуля????\nБудет сгенерирован 1 ящик.");
                    count = 1;
                    _viewModel.MailBoxesCount = count.ToString();
                }

                if(!Checks.MailBoxName(name))
                {
                    string errorString;
                    errorString =  $"Не верный шаблон!.\n";
                    errorString += $"Шаблон имени может содержать только буквы латинского алфавита,цифры, тире и точку.";
                    errorString += $"Тире или тока не могут быть последним или первым символом";
                    System.Windows.MessageBox.Show(errorString);
                    error = true;
                }

                int maxLength = 16;
                int minLength = 8;
                int defLenght = 10;

                if(!Checks.Password(_viewModel.PasswordLength, out passwordLength,defLenght,maxLength,minLength))
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
