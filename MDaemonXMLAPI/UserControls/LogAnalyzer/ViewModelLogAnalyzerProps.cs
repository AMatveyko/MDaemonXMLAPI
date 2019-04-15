using LogAnalyzer.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents.DocumentStructures;

namespace MDaemonXMLAPI.UserControls.LogAnalyzer
{
    public partial class VMLogAnalyzer
    {
        private string _logText;
        public string LogText
        {
            get
            {
                return _logText;
            }
            set
            {
                _logText = value;
                OnPropertyChanged(nameof(LogText));
            }
        }

        private ObservableCollection<Session> _sessionListCopy;
        private ObservableCollection<Session> _sessionList;
        public ObservableCollection<Session> SessionList
        {
            get => _sessionList;
            set
            {
                _sessionList = value;
                OnPropertyChanged(nameof(SessionList));
            }
        }

        private Session _selectedSession;
        public Session SelectedSession
        {
            get => _selectedSession;
            set
            {
                _selectedSession = value;
                OnPropertyChanged(nameof(SelectedSession));
                if ( ( _selectedSession != null ) && ( _selectedSession.Log.Contains(SearchString) ) )
                {
                    SelectionStart = _selectedSession.Log.IndexOf(SearchString);
                    SelectionLength = SearchString.Length;
                }
                ChangeSelecktedSessionProp();
            }
        }

        private String _selectedSessionLog;
        public String SelectedSessionLog
        {
            get => _selectedSessionLog;
            set
            {
                _selectedSessionLog = value;
                OnPropertyChanged(nameof(SelectedSessionLog));
                OnPropertyChanged(nameof(StartCursorSelectionSelectedLog));
                OnPropertyChanged(nameof(LengthSelectionSelectedLog));
            }
        }

        private int _countSession;
        public int CountSession
        {
            get => _countSession;
            set
            {
                _countSession = value;
                OnPropertyChanged(nameof(CountSession));
            }
        }

        private int _curentSession;
        public int CurrentSession
        {
            get => _curentSession;
            set
            {
                _curentSession = value;
                OnPropertyChanged(nameof(CurrentSession));
            }
        }

        private int _countFile;

        public int CountFile
        {
            get => _countFile;
            set
            {
                _countFile = value;
                OnPropertyChanged(nameof(CountFile));
            }
        }

        private int _currentFile;

        public int CurrentFile
        {
            get => _currentFile;
            set
            {
                _currentFile = value;
                OnPropertyChanged(nameof(CurrentFile));
            }
        }

        private int _minValProgressBar;
        public int MinValProgressBar
        {
            get => _minValProgressBar;
            set
            {
                _minValProgressBar = value;
                OnPropertyChanged(nameof(MinValProgressBar));
            }
        }

        private int _inputSession;
        public int InputSession
        {
            get => _inputSession;
            set
            {
                _inputSession = value;
                OnPropertyChanged(nameof(InputSession));
            }
        }

        private int _outputSession;
        public int OutputSession
        {
            get => _outputSession;
            set
            {
                _outputSession = value;
                OnPropertyChanged(nameof(OutputSession));
            }
        }

        private int _successSession;
        public int SuccessSession
        {
            get => _successSession;
            set
            {
                _successSession = value;
                OnPropertyChanged(nameof(SuccessSession));
            }
        }

        private int _terminatedSession;
        public int TerminatedSession
        {
            get => _terminatedSession;
            set
            {
                _terminatedSession = value;
                OnPropertyChanged(nameof(TerminatedSession));
            }
        }

        private int _allSession;
        public int AllSession
        {
            get => _allSession;
            set
            {
                _allSession = value;
                OnPropertyChanged(nameof(AllSession));
            }
        }

        private string _searchString;
        public string SearchString
        {
            get => _searchString;
            set
            {
                _searchString = value;
                ObservableCollection<Session> filteredSessions = new ObservableCollection<Session>();
                foreach (var session in _sessionListCopy.Where(x=>x.Log.Contains(_searchString)).OrderByDescending(x=>x.Start))
                {
                    filteredSessions.Add(session);
                }

                SessionList = filteredSessions;
            }
        }

        private bool _directionTableEnable;
        public bool DirectionTableEnable
        {
            get => _directionTableEnable;
            set
            {
                _directionTableEnable = value;
                OnPropertyChanged(nameof(DirectionTableEnable));
            }
        }

        private bool _searchTableEnable;

        public bool SearchTableEnable
        {
            get => _searchTableEnable;
            set
            {
                _searchTableEnable = value;
                DirectionTableEnable = !value;
                OnPropertyChanged(nameof(SearchTableEnable));
            }
        }

        private int _selectionStart;
        public int SelectionStart
        {
            get => _selectionStart;
            set
            {
                _selectionStart = value;
                OnPropertyChanged(nameof(SelectionStart));
            }
        }

        private int _selectionLength;
        public int SelectionLength
        {
            get => _selectionLength;
            set
            {
                _selectionLength = value;
                OnPropertyChanged(nameof(SelectionLength));
            }
        }

        private string _directoryForSearch;
        public string DirectoryForSearch
        {
            get => _directoryForSearch;
            set
            {
                _directoryForSearch = $@"{value}";
                OnPropertyChanged(nameof(DirectoryForSearch));
                ChangeDirAsync();
            }
        }

        public int StartCursorSelectionSelectedLog
        {
            get
            {
                if(SearchString != "")
                {
                    int startCursor = SelectedSessionLog.IndexOf(SearchString);
                    startCursor = (startCursor > 0) ? startCursor : 0 ;
                    return startCursor;
                }
                return 0;
            }
            set { ; }
        }
        public int LengthSelectionSelectedLog
        {
            get
            {
                if (SearchString != "")
                {
                    return SearchString.Length;
                }
                return 0;
            }
            set { ; }
        }

        private List<FileForRead> _fileList;
        public List<FileForRead> FileList
        {
            get => _fileList;
            set
            {
                _fileList = value;
                OnPropertyChanged(nameof(FileList));
            }
        }

        public FileForRead SelecteFileForRead
        {
            get
            {
                if (FileList?.Count > 0)
                    return FileList[0];
                return null;
            }
        }

        #region Func
        private void ChangeSelecktedSessionProp()
        {
            if (SelectedSession != null)
            {
                SelectedSessionLog = SelectedSession.Log.ToString();
            }
        }
        #endregion
    }
}
