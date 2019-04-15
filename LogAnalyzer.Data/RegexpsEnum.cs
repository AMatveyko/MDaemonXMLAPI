using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogAnalyzer.Data
{
    public enum Regexps
    {
        FileInfo,
        SessionInfo,
        ConnectionInfo,
        SessionClose,
        HeaderFrom,
        HeaderSubject,
        AllRegexp,
        EndSession
    }

}
