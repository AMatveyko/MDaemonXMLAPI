using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogAnalyzer.Data;

namespace LogAnalalyzer.Bl
{
    public static class Logging
    {
        public static event EventHandler AddingLog;

        internal enum Level
        {
            info = 1,
            warning,
            error
        }

        internal static void AddErr(string log)
            => AddLog(Level.error, log);
        internal static void AddWrn(string log)
            => AddLog(Level.warning, log);
        internal static void AddInf(string log)
            => AddLog(Level.info, log);

        internal static void AddLog(string log)
            => AddLog(Level.info, log);

        internal static void AddLog(Level level, string log)
        {
            EventArgsLog eventArgsLog = new EventArgsLog(level.ToString(), log);
            AddingLog?.Invoke(null, eventArgsLog);
        }

    }
}
