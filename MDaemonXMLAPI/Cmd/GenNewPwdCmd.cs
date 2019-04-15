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
    public class GenNewPwdCmd : DelegateCommand
    {
        public GenNewPwdCmd( ViewModel viewModel )
            : base(viewModel) { }
        public override void Execute(object parameter)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены?", "Сгенерировать новый пароль.", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.No)
                return;
            var specInPass = true;
            var passwordLength = 10;
            _viewModel.SelectedMailBox.Password = PasGen.Gen(specInPass, passwordLength);
        }

        public override bool CanExecute(object parameter)
        {
            return (_viewModel.SelectedMailBox != null);
        }
    }
}
