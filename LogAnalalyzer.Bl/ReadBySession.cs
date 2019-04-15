using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogAnalyzer.Data;
using Microsoft.Win32;

namespace LogAnalalyzer.Bl
{
    internal class ReadBySession
    {
        private static string _smtpInKey = "*-SMTP-(in)*";
        private static string _smtpOutKey = "*-SMTP-(out)*";
        //private static string _popKey = "-POP";
        //private static string _imapKey = "-IMAP";
        private static int _fileInfoStringCount = 5;
        private static int _headerNumberString = 2;
        private static Encoding srcEncoding = Encoding.GetEncoding(1251);

        public Boolean SmtpIn { get; set; } = true;
        public Boolean SmtpOut { get; set; } = true;

        public ReadBySession()
        { }

        internal void Read(List<FileForRead> fileList)
        {
            Parser parser = new Parser();
            List<Task> readTasks = new List<Task>();

            Logging.AddInf($"Start {DateTime.Now.ToString()}");

            if (fileList.Count > 0)
            {
                foreach (var file in fileList.Where( x=>x.Enable ) )
                {
                    string filePath = file.FilePath;
                    WorkDone.Start(fileList.Count);
                    if (File.Exists(filePath))
                    {
                        WorkDone.BeginParseFile(0);

                        Proto proto = Proto.NULL;
                        Direct direct = Direct.NULL;

                        readTasks.Add(Task.Run((() =>
                        {
                            Logging.AddInf($"{filePath} read");
                            int strCounter = 1;
                            StringBuilder session = new StringBuilder();
                            using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                            using (StreamReader reader = new StreamReader(stream, srcEncoding))
                            {
                                string line;
                                while ((line = reader.ReadLine()) != null)
                                {
                                    if (strCounter > _fileInfoStringCount)
                                    {
                                        if (!Parser.CheckEndSession(ref line))
                                        {
                                            session.Append($"{line}\n");
                                        }
                                        else
                                        {
                                            parser.ParsePerSession(session, ref proto, ref direct);
                                            WorkDone.SesDone();
                                            session.Clear();
                                        }
                                    }
                                    else
                                    {
                                        if (strCounter == _headerNumberString)
                                            parser.IsFileInfo(ref proto, ref direct, $"{line}\r\n");
                                        strCounter++;
                                    }
                                }
                            }
                            WorkDone.FlDone();
                        })));
                    }
                    else
                    {
                        Logging.AddErr($"{filePath} is not exist!");
                    }
                }

                var t = Task.WhenAll(readTasks);
                t.Wait();

            }
            else
            {
                Logging.AddWrn($"file collection empty...");
            }

            Logging.AddInf($"finish {DateTime.Now.ToString()}");
        }

        public List<FileForRead> CheckDirAndGetFilepath(string dir)
        {
            List<string> fileList = new List<string>();

            List<FileForRead> files = new List<FileForRead>();

            if (Directory.Exists(dir))
            {
                if (SmtpIn)
                    fileList.AddRange(Directory.GetFiles(dir, _smtpInKey));
                if (SmtpOut)
                    fileList.AddRange(Directory.GetFiles(dir, _smtpOutKey));

                if (fileList.Count > 0)
                {
                    foreach (var filePath in fileList)
                    {
                        files.Add(new FileForRead() { Enable = false, FilePath = filePath });
                    }
                }
            }
            else
            {
                Logging.AddErr($"{dir} is not exist!");
            }
            return files;
        }

    }
}
