using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LogAnalyzer.Data
{
    static public class RegexpsCollection
    {

        private static string _endSession =
            "^[a-zA-Z]{3} [0-9,-]{10} [0-9,:,.]{12}: ----------$";

        private static string _fileInfo =
            "START Event Log \\/ MDaemon PRO v[0-9]{1,2}\\.[0-9]{1,2}\\.[0-9]{1,2}, " +
            "(?<" + nameof(Groups.FileInfoProto) + ">(SMTP|POP|IMAP){1})" +
            "(?<" + nameof(Groups.FileInfoDirect) + ">( \\(in\\)| \\(out\\)){0,1}) log information";

        private static string _sessionInfo =
            "[A-Z][a-z]{2} (?<" + nameof(Groups.SessionInfoDateTime) + ">([0-9]{4}-[0-9]{2}-[0-9]{2} [0-9]{2}:[0-9]{2}:[0-9]{2}.[0-9]{3})): Session " +
            "(?<" + nameof(Groups.SessionInfoId) + ">([0-9]{1,})); child [0-9]{4}";

        private static string _connectionInfo =
            "([a-zA-Z0-9]| |:|-|.){29}Accepting (SMTP|POP){1} connection from " +
            "(?<" + nameof(Groups.ConnectionInfoSrcAddr) + ">([0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3})):(?<" + nameof(Groups.ConnectionInfoSrcPort) + ">([0-9]{1,5})) to " +
            "(?<" + nameof(Groups.ConnectionInfoDstAddr) + ">([0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3})):(?<" + nameof(Groups.ConnectionInfoDstPort) + ">([0-9]{1,5}))";

        private static string _sessionClose =
            "[A-Z][a-z]{2} (?<" + nameof(Groups.SessionInfoDateTime) + ">([0-9]{4}-[0-9]{2}-[0-9]{2} [0-9]{2}:[0-9]{2}:[0-9]{2}.[0-9]{3})): SMTP session " + 
            "(?<" + nameof(Groups.SessionStatus) + ">(successful|terminated)) \\(Bytes in\\/out: [0-9]{1,10}\\/[0-9]{1,10}\\)";

        private static string _headerFrom =
            "[A-Z][a-z]{2} [0-9]{4}-[0-9]{2}-[0-9]{2} [0-9]{2}:[0-9]{2}:[0-9]{2}.[0-9]{3}: (<--|-->)( MAIL FROM:| MAIL From:) ?<(?<HeaderFrom>(.{3,}))>";

        private static string _headerSubject =
            "[A-Z][a-z]{2} [0-9]{4}-[0-9]{2}-[0-9]{2} [0-9]{2}:[0-9]{2}:[0-9]{2}.[0-9]{3}: \\*  Subject:(?<HeaderSubject>(.{0,}))";

        private static string _regexp =
            //"[a-zA-Z]{3} (?<SessionInfoDateTime>([0-9]{4}-[0-9]{2}-[0-9]{2} [0-9]{2}:[0-9]{2}:[0-9]{2}.[0-9]{3})): Session (?<SessionInfoId>([0-9]{1,})); child [0-9]{4}.{0,}" +
            //"(<-- MAIL FROM:|--> MAIL From:)( )?(?<HeaderFrom>(<.{3,}>)).{0,}" +
            "[A-Z][a-z]{2} (?<SessionInfoDateTime2>([0-9]{4}-[0-9]{2}-[0-9]{2} [0-9]{2}:[0-9]{2}:[0-9]{2}.[0-9]{3})): SMTP session (?<SessionStatus>(successful|terminated)) \\(Bytes in\\/out: [0-9]{1,10}\\/[0-9]{1,10}\\)";

        public static Dictionary<Regexps, Regex> ForAll = new Dictionary<Regexps, Regex>() {
            { Regexps.FileInfo, new Regex(_fileInfo, RegexOptions.Compiled) },
            { Regexps.SessionInfo, new Regex(_sessionInfo, RegexOptions.Compiled) },
            { Regexps.EndSession, new Regex(_endSession, RegexOptions.Compiled) },
            { Regexps.AllRegexp, new Regex(_regexp, RegexOptions.Compiled)}
        };
        public static Dictionary<Regexps, Regex> ForSMTP = new Dictionary<Regexps, Regex>() {
            { Regexps.ConnectionInfo, new Regex(_connectionInfo, RegexOptions.Compiled) },
            { Regexps.SessionClose, new Regex(_sessionClose, RegexOptions.Compiled) },
            { Regexps.HeaderFrom, new Regex(_headerFrom, RegexOptions.Compiled) },
            { Regexps.HeaderSubject, new Regex(_headerSubject, RegexOptions.Compiled) }
        };

        public enum Groups
        {
            FileInfoProto,
            FileInfoDirect,
            SessionInfoDateTime,
            SessionInfoDateTime2,
            SessionInfoId,
            ConnectionInfoSrcAddr,
            ConnectionInfoSrcPort,
            ConnectionInfoDstAddr,
            ConnectionInfoDstPort,
            SessionStatus,
            SessionByteIn,
            SessionByteOut,
            HeaderFrom,
            HeaderSubject,
            EndSession
        }
    }
}
