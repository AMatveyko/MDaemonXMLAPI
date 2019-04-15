using LogAnalyzer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogAnalalyzer.Bl
{
    public class ControllerOld
    {

        private ReadLogsOld _readLogs;

        public ControllerOld()
        {
            _readLogs = new ReadLogsOld();
        }

        public IEnumerable<Session> GetAllSessions(string dir)
        {

            List<Session> sessions = new List<Session>();
            List<List<string>> files = new List<List<string>>();

            //_readLogs.ReadSmtpIn(dir);

            files.AddRange(_readLogs.ReadSmtpIn(dir));
            //files.AddRange(_readLogs.ReadSmtpOut(dir));
            //files.AddRange(_readLogs.ReadPop(dir));
            //files.AddRange(_readLogs.ReadImap(dir));
            foreach (var file in files)
            {
                //sessions.AddRange(FileParserOld.SliceFile(file));
                //////////////////////sessions.AddRange(Parser.SliceFile(file));
            }
            //files.AddRange(_readLogs.ReadSmtpOut(dir));
            return sessions;
        }
    }
}
