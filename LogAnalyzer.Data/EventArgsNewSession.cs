using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogAnalyzer.Data
{
    public class EventArgsNewSession : EventArgs
    {
        public Session Session { get; private set; }

        public int CountSessions { get; private set; }
        public int CurrentSession { get; private set; }

        public EventArgsNewSession(Session session)
            : this(session, 0, 0)
        { }
        public EventArgsNewSession(Session session, int countSessions, int currentSession)
        {
            Session = session;
            CountSessions = countSessions;
            CurrentSession = currentSession;
        }
    }
}
