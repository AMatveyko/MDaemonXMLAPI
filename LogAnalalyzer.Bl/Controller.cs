using LogAnalyzer.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogAnalalyzer.Bl
{
    public class Controller
    {

        private ReadBySession _readBySession;

        public event EventHandler FinishNewSession;

        public Controller()
        {
            _readBySession = new ReadBySession();
            Parser.FinishNewSession += FinishNewSessionFunc;
        }

        private void FinishNewSessionFunc(object o, EventArgs e)
        {
            FinishNewSession?.Invoke(o, e);
        }

        public async Task<List<FileForRead>> GetFileListFromDirectoryAsync(string dir)
        {
            return await Task.Run(() => _readBySession.CheckDirAndGetFilepath(dir) );
        }

        public void GetAllSessinsMod(List<FileForRead> fileList)
        {
            if (fileList.Count > 0)
            {
                _readBySession.Read(fileList);
                Logging.AddInf($"Готово!");
                WorkDone.Done();
            }
            else
            {
                Logging.AddWrn("Выберите файлы!");
            }
        }
    }
}
