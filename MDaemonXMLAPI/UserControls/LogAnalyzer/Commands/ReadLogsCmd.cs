using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MDaemonXMLAPI.Model;
using MDaemonXMLAPI.Model.Xml;
using MDaemonXMLAPI.UserControls.LogAnalyzer;
using MDaemonXMLAPI.Cmd;
using LogAnalyzer.Data;
using LogAnalalyzer.Bl;

namespace MDaemonXMLAPI.UserControls.LogAnalyzer.Commands
{
    public class ReadLogsCmd : DelegateCommand
    {

        private new VMLogAnalyzer _viewModel;
        private bool _enableButton;

        public ReadLogsCmd( VMLogAnalyzer viewModel )
            : base(null)
        {
            _viewModel = viewModel;
            _enableButton = true;
        }
        public async override void Execute(object parameter)
        {
            string dir = _viewModel.DirectoryForSearch;
            _viewModel.ReadLogBeginExecute();
            _enableButton = false;
            AppConfig.WriteLogDir(dir);
            this.RaiseCanExecuteChanged();
            await Task.Run( () => _viewModel.Cntrl.GetAllSessinsMod(_viewModel.FileList.ToList()) );
            _enableButton = true;
            this.RaiseCanExecuteChanged();
            _viewModel.ReadLogEndExecute();
        }

        public override bool CanExecute(Object pararameter)
        {
            return _enableButton;
        }
    }
}
