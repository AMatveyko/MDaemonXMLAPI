using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogAnalyzer.Data
{
    public class EventArgsLog : EventArgs
    {
        public string LogLevel { get; private set; }
        public string LogText { get; private set; }
        
        public EventArgsLog(string logLevel, string logText)
        {
            LogLevel = logLevel;
            LogText = logText;
        }
    }
}
