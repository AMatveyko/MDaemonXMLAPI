using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogAnalalyzer.Bl
{
    public static class WorkDone
    {
        public static int SessionCount { get; internal set; }
        public static int SessionDone { get; internal set; }
        public static int FileCount { get; internal set; }
        public static int FileDone { get; internal set; }

        public static event EventHandler StartWork;
        public static event EventHandler EventSessionDone;
        public static event EventHandler EventFileDone;
        public static event EventHandler WorkIsDone;
        public static event EventHandler ChangeState;

        internal static void Start(int fileCount)
        {
            FileCount = fileCount;
            StartWork?.Invoke(null, EventArgs.Empty);
            ChangeState?.Invoke(null, EventArgs.Empty);
        }

        internal static void BeginParseFile(int sessionCountInFile)
        {
            SessionCount += sessionCountInFile;
            ChangeState?.Invoke(null, EventArgs.Empty);
        }

        internal static void SesDone()
        {
            SessionDone++;
            EventSessionDone?.Invoke(null,EventArgs.Empty);
            ChangeState?.Invoke(null, EventArgs.Empty);
        }

        internal static void FlDone()
        {
            FileDone++;
            EventFileDone?.Invoke(null, EventArgs.Empty);
            ChangeState?.Invoke(null, EventArgs.Empty);
            //SessionDone = 0;
            //SessionCount = 0;
        }

        internal static void Done()
        {
            WorkIsDone?.Invoke(null, EventArgs.Empty);
            ChangeState?.Invoke(null, EventArgs.Empty);
            FileCount = 0;
            FileDone = 0;
            SessionCount = 0;
            SessionDone = 0;
        }
    }
}
