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
using System.IO;

namespace MDaemonXMLAPI.Cmd
{
    public class WriteListToFileCmd : DelegateCommand
    {

        public WriteListToFileCmd(ViewModel viewModel )
            : base(viewModel) { }

        public override void Execute(object parameter)
        {
            if (_viewModel.NewMails?.Count > 0)
            {
                var dialog = new Microsoft.Win32.SaveFileDialog();
                dialog.Filter = "Rich Text File (*.txt)|*.txt|All Files (*.*)|*.*";
                dialog.FileName = "NewMailBox.txt"; //set initial filename
                if (dialog.ShowDialog() == true)
                {
                    try
                    {
                        string text = String.Empty;
                        text += "Российская Федерация\r\n";
                        text += "SMTP(s): smtp.ru.bg-corp.net порт 465 (предпочтительно)\r\n";
                        text += "POP(s): pop.ru.bg-corp.net порт 995 (предпочтительно)\r\n";
                        text += "IMAP(s): pop.ru.bg-corp.net порт 993 (предпочтительно)\r\n";
                        text += "SMTP: smtp.ru.bg-corp.net порт 587 \r\n";
                        text += "POP: pop.ru.bg-corp.net порт 110 \r\n";
                        text += "IMAP: pop.ru.bg-corp.net порт 143 \r\n";
                        text += "\r\n";
                        text += "украина\r\n";
                        text += "SMTP(s): smtp.ua.bg-corp.net порт 465 (предпочтительно)\r\n";
                        text += "POP(s): pop.ua.bg-corp.net порт 995 (предпочтительно)\r\n";
                        text += "IMAP(s): pop.ua.bg-corp.net порт 993 (предпочтительно)\r\n";
                        text += "SMTP: smtp.ua.bg-corp.net порт 587 \r\n";
                        text += "POP: pop.ua.bg-corp.net порт 110 \r\n";
                        text += "IMAP: pop.ua.bg-corp.net порт 143 \r\n";
                        text += "------------------------------------------------------- \r\n";
                        foreach (var mailbox in _viewModel.NewMails)
                        {
                            text += $"Имя пользователя: {mailbox.Name}@{mailbox.Domain}\r\n";
                            text += $"Пароль:           {mailbox.Password}\r\n";
                            text += $"Имя:              {mailbox.FirstName}\r\n";
                            text += $"Фамилия:          {mailbox.LastName}\r\n";
                            text += $"Описание:         {mailbox.Descriptions}\r\n";
                            text += $"\r\n";
                        }
                        File.WriteAllText(dialog.FileName, text);
                    }
                    catch (Exception e)
                    {
                        System.Windows.MessageBox.Show($"Не удалось записать. {e.Message}");
                    }
                }
            }
            else
                System.Windows.MessageBox.Show("Список пуст следовательно записывать нечего :(");
        }
    }
}
