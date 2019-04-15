using LogAnalyzer.Data;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace LogAnalalyzer.Bl
{
    public class Parser
    {

        public static event EventHandler FinishNewSession;
        private bool debug = false;

        public static bool CheckEndSession(ref string line)
        {
            Boolean IsEnd;
            IsEnd = (RegexpsCollection.ForAll[Regexps.EndSession].IsMatch(line)) ? true : false;
            return IsEnd;
        }

        internal void ParsePerSession(StringBuilder rawSession, ref Proto proto, ref Direct direct)
        {
            Session session = new Session() { Proto = proto, Direction = direct };
            session.Log = rawSession.ToString();
            IsSessionInfo(ref session);
            IsHeaderFrom(ref session);
            if (direct == Direct.output)
                IsHeaderSubject(ref session);
            IsSessionClose(ref session);

            FinishNewSessionFunc(ref session);
        }

        private void IsHeaderSubject(ref Session session)
        {
            Match headerSubject = RegexpsCollection.ForSMTP[Regexps.HeaderSubject].Match(session.Log);
            if(headerSubject != null)
                session.Subject = headerSubject.Groups[nameof(RegexpsCollection.Groups.HeaderSubject)].Value;
        }

        private void IsHeaderFrom(ref Session session)
        {
            Match headerFrom = RegexpsCollection.ForSMTP[Regexps.HeaderFrom].Match(session.Log);
            if (headerFrom != null)
                session.From = headerFrom.Groups[nameof(RegexpsCollection.Groups.HeaderFrom)].Value;
        }

        private void FinishNewSessionFunc(ref Session session)
        {
            FinishNewSession?.Invoke(null, new EventArgsNewSession(session));
        }




        public void IsFileInfo(ref Proto proto, ref Direct direct, string str)
        {
            if (debug) Logging.AddInf("fileinfo");
            Match fileInfo = RegexpsCollection.ForAll[Regexps.FileInfo].Match(str);
            if(fileInfo != null)
            {
                String sProto = fileInfo.Groups[RegexpsCollection.Groups.FileInfoProto.ToString()].Value;
                String sDirect = fileInfo.Groups[RegexpsCollection.Groups.FileInfoDirect.ToString()].Value;
                switch (sProto)
                {
                    case nameof(Proto.SMTP):
                        proto = Proto.SMTP;
                        if (sDirect == " (in)")
                            direct = Direct.input;
                        else if (sDirect == " (out)")
                            direct = Direct.output;
                        break;
                    case nameof(Proto.IMAP):
                        proto = Proto.IMAP;
                        break;
                    case nameof(Proto.POP):
                        proto = Proto.POP;
                        break;
                }
            }
        }

        private void IsSessionInfo(ref Session session)
        {
            if (debug) Logging.AddInf("sessioninfo");
            //_currentSession++
            var sId = new Int32();

            Match sessionInfo = RegexpsCollection.ForAll[Regexps.SessionInfo].Match(session.Log);
            if (sessionInfo != null)
            {
                Int32.TryParse(sessionInfo.Groups[nameof(RegexpsCollection.Groups.SessionInfoId)].Value, out sId);
                session.Id = sId;
                DateTime startSession;
                if (DateTime.TryParseExact(
                    sessionInfo.Groups[nameof(RegexpsCollection.Groups.SessionInfoDateTime)].Value,
                    "yyyy-MM-dd HH:mm:ss.FFF", null, DateTimeStyles.None , out startSession))
                    session.Start = startSession;
            }
        }

        private void IsSessionClose(ref Session session)
            => IsSessionClose(ref session, session.Log);
        private void IsSessionClose(ref Session session, string str)
        {
            if (debug) Logging.AddInf("closeCon");
            Match sessionClose = RegexpsCollection.ForSMTP[Regexps.SessionClose].Match(str);
            if (sessionClose != null)
            {
                DateTime endSession;
                if (DateTime.TryParseExact(
                    sessionClose.Groups[nameof(RegexpsCollection.Groups.SessionInfoDateTime)].Value,
                    "yyyy-MM-dd HH:mm:ss.FFF", null, DateTimeStyles.None, out endSession))
                    session.End = endSession;
                session.Status = sessionClose.Groups[nameof(RegexpsCollection.Groups.SessionStatus)].Value;
            }
        }
    }
}
