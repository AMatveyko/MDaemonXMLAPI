using LogAnalyzer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LogAnalalyzer.Bl
{
    internal static class FileParserOld
    {
        static public List<Session> SliceFile (List<string> file)
        {
            List<Session> sessions = new List<Session>();
            Session session = null;
            Boolean startFile = true;
            Direct direct = Direct.NULL;
            Proto proto = Proto.NULL;

            //Task.Factory.StartNew(() =>
            //{
                foreach (var str in file)
                {
                    //Logging.AddInf("iter " + i++);
                    //If FileInfo
                    //"START Event Log / MDaemon PRO v18.0.1, SMTP (in) log information"
                    if ((startFile) && (RegexpsCollection.ForAll[Regexps.FileInfo].IsMatch(str)))
                    {
                        IsFileInfo(ref proto, ref direct, str);
                        startFile = false;
                        continue;
                    }
                    if (!startFile)
                    {
                        //If SessionInfo
                        //Thu 2019-03-14 00:00:17.768: Session 981023; child 0002
                        if (RegexpsCollection.ForAll[Regexps.SessionInfo].IsMatch(str))
                        {
                            IsSessionInfo(ref proto, ref direct, ref session, ref sessions, str);
                        }
                        if (direct == Direct.input)
                        {
                            //If Start connection
                            //Thu 2019-03-14 00:00:17.768: Accepting SMTP connection from 10.2.6.35:64407 to 91.226.133.9:587
                            if (RegexpsCollection.ForSMTP[Regexps.ConnectionInfo].IsMatch(str))
                            {
                                IsConnectionInfo(ref session, str);
                            }
                            else if (RegexpsCollection.ForSMTP[Regexps.SessionClose].IsMatch(str))
                            {
                                IsSessionClose(ref session, str);
                            }
                        }
                        session.Log += str;
                    }
                }
            //});
            Logging.AddInf("end");
            return sessions;
        }

        private static void IsFileInfo(ref Proto proto, ref Direct direct, string str)
        {
            MatchCollection fileInfo = RegexpsCollection.ForAll[Regexps.FileInfo].Matches(str);
            foreach (Match m in fileInfo)
            {
                String sProto = m.Groups[RegexpsCollection.Groups.FileInfoProto.ToString()].Value;
                String sDirect = m.Groups[RegexpsCollection.Groups.FileInfoDirect.ToString()].Value;

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

        private static void IsSessionInfo(ref Proto proto, ref Direct direct, ref Session session, ref List<Session> sessions, string str)
        {
            session = new Session() { Proto = proto, Direction = direct };
            sessions.Add(session);
            var sId = new Int32();

            MatchCollection sessionInfo = RegexpsCollection.ForAll[Regexps.SessionInfo].Matches(str);
            foreach (Match m in sessionInfo)
            {
                Int32.TryParse(m.Groups[nameof(RegexpsCollection.Groups.SessionInfoId)].Value, out sId);
                session.Id = sId;
                session.Start = DateTime.ParseExact(m.Groups[nameof(RegexpsCollection.Groups.SessionInfoDateTime)].Value, "yyyy-MM-dd HH:mm:ss.FFF", null);
            }
        }

        private static void IsConnectionInfo(ref Session session, string str)
        {
            MatchCollection connectionInfo = RegexpsCollection.ForSMTP[Regexps.ConnectionInfo].Matches(str);
            foreach (Match m in connectionInfo)
            {
                session.SrcAddr = m.Groups[nameof(RegexpsCollection.Groups.ConnectionInfoSrcAddr)].Value;
                session.SrcPort = m.Groups[nameof(RegexpsCollection.Groups.ConnectionInfoSrcPort)].Value;
                session.DstAddr = m.Groups[nameof(RegexpsCollection.Groups.ConnectionInfoDstAddr)].Value;
                session.DstPort = m.Groups[nameof(RegexpsCollection.Groups.ConnectionInfoDstPort)].Value;
            }
        }

        private static void IsSessionClose(ref Session session, string str)
        {
            MatchCollection sessionClose = RegexpsCollection.ForSMTP[Regexps.SessionClose].Matches(str);
            foreach(Match m in sessionClose)
            {
                //session.End = DateTime.ParseExact(m.Groups[nameof(RegexpsCollection.Groups.SessionInfoDateTime)].Value, "yyyy-MM-dd HH:mm:ss.FFF", null);
                session.Status = m.Groups[nameof(RegexpsCollection.Groups.SessionStatus)].Value;
                Logging.AddInf(session.Status + " status");
                //int byteIn;
                //if (Int32.TryParse(m.Groups[nameof(RegexpsCollection.Groups.SessionByteIn)].Value, out byteIn))
                //{
                //    session.ByteIn = byteIn;
                //}
                //else
                //    Logging.AddWrn("Не парсится buteIn сессии");
                //int byteOut;
                //if (Int32.TryParse(m.Groups[nameof(RegexpsCollection.Groups.SessionByteOut)].Value, out byteOut))
                //{
                //    session.ByteOut = byteOut;
                //}
                //else
                //    Logging.AddWrn("Не парсится buteOut сессии");
            }
        }
    }
}
