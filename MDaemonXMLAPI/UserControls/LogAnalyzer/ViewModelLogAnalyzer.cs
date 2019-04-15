using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using LogAnalalyzer.Bl;
using LogAnalyzer.Data;
using MDaemonXMLAPI.Model;

namespace MDaemonXMLAPI.UserControls.LogAnalyzer
{
    public partial class VMLogAnalyzer
    {

        public Controller Cntrl { get; private set; }

        public VMLogAnalyzer()
        {
            Cntrl = new Controller();
            SessionList = new ObservableCollection<Session>();
            _sessionListCopy = new ObservableCollection<Session>();
            FileList = new List<FileForRead>();
            SearchString = "";
            DirectoryForSearch = AppConfig.ReadDirLog();
            SearchTableEnable = false;
            Cntrl.FinishNewSession += FinishNewSessionFunc;
            CurrentSession = 0;
            CountSession = 0;
            MinValProgressBar = 0;
            ResetCounts();
            Subscribes();
        }


        private void ResetCounts()
        {
            InputSession = 0;
            OutputSession = 0;
            SuccessSession = 0;
            TerminatedSession = 0;
            AllSession = 0;
        }

        private void Subscribes()
        {
            Logging.AddingLog += AddLog;
            //WorkDone.ChangeState += (o,e) =>
            WorkDone.EventFileDone += (o,e) =>
            {
                    CountFile = WorkDone.FileCount;
                    CurrentFile = WorkDone.FileDone;
                    CurrentSession = WorkDone.SessionDone;
            };
        }


        private void FinishNewSessionFunc(object o, EventArgs e)
        {
            EventArgsNewSession eventArgs = (EventArgsNewSession) e;
            Session session = eventArgs.Session;
            App.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            {
                SessionList.Add(session);
            }));

            if (session.Direction == Direct.input)
                InputSession++;
            else if (session.Direction == Direct.output)
                OutputSession++;
            if (session.Status == "successful")
                SuccessSession++;
            else if (session.Status == "terminated")
                TerminatedSession++;
            AllSession++;
        }

        public void ReadLogBeginExecute()
        {
            _sessionListCopy.Clear();
            SessionList.Clear();
            SearchString = "";
            OnPropertyChanged(nameof(SearchString));
            SearchTableEnable = false;
            LogText = String.Empty;
            ResetCounts();
        }

        public void ReadLogEndExecute()
        {
            SearchTableEnable = true;
            _sessionListCopy = _sessionList;
            SortSessionList();
        }

        private void SortSessionList()
        {
            ObservableCollection<Session> tmpList = new ObservableCollection<Session>();
            var sortList = _sessionListCopy.OrderByDescending(x => x.Start).Select(x => x);
            foreach (var session in sortList)
            {
                tmpList.Add(session);
            }
            SessionList = tmpList;
        }

        private void AddLog(Object o, EventArgs e)
        {
            EventArgsLog eventArgsLog = (EventArgsLog)e;
            LogText = $"{eventArgsLog.LogLevel}: {eventArgsLog.LogText}\n" + LogText;
        }

        private async void ChangeDirAsync()
        {
            List<FileForRead> fileList = await Cntrl.GetFileListFromDirectoryAsync(DirectoryForSearch);
            FileList = fileList.OrderByDescending(x => x.FilePath).ToList(); ;
            OnPropertyChanged(nameof(SelecteFileForRead));
        }
    }
}
