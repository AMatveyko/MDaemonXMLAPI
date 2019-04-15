using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDaemonXMLAPI.UserControls.LogAnalyzer.Commands;

namespace MDaemonXMLAPI.UserControls.LogAnalyzer
{
    public partial class VMLogAnalyzer
    {
        private ReadLogsCmd _readLogs;
        public ReadLogsCmd ReadLogs
        { get => _readLogs ?? ( _readLogs = new ReadLogsCmd(this) ); }
    }
}
