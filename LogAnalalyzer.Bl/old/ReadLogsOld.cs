using LogAnalyzer.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace LogAnalalyzer.Bl
{
    class ReadLogsOld
    {

        private string _smtpInKey = "*-SMTP-(in)*";
        private string _smtpOutKey = "*-SMTP-(out)*";
        private string _popKey = "-POP";
        private string _imapKey = "-IMAP";

        public bool IsExist { get; private set; }


        public List<List<string>> ReadSmtpIn(string dir)
            => Read(dir, _smtpInKey);

        public List<List<string>> ReadSmtpOut(string dir)
            => Read(dir, _smtpOutKey);

        public List<List<string>> ReadPop(string dir)
            => Read(dir, _popKey);

        public List<List<string>> ReadImap(string dir)
            => Read(dir, _imapKey);

        private List<List<string>> Read(string dir, string nameRegexp)
        {
            List<List<string>> files = new List<List<string>>();
            if (Directory.Exists(dir))
            {
                string[] fileNames = Directory.GetFiles(dir, nameRegexp);
                foreach (var filename in fileNames)
                {
                    if (File.Exists(filename))
                    {
                        //using (StreamReader sr = new StreamReader(filename))
                        //{
                        //    Logging.AddInf($"Read file {filename}");
                        //    List<string> file = new List<string>();
                        //    string line;
                        //    while ((line = sr.ReadLine()) != null)
                        //    {
                        //        Logging.AddWrn("readLine");
                        //        file.Add(line);
                        //    }
                        //    files.Add(file);
                        //}
                        using (StreamReader sr = new StreamReader(filename))
                        {
                            String str = sr.ReadToEnd();
                            files.Add(str.Split('\n').ToList());
                        }
                    }
                    else
                    {
                        Logging.AddErr($"File {filename} not found!");
                    }
                }
            }
            else
            {
                Logging.AddErr($"Directory {dir} not found!");
                return null;
            }

            return files;
        }


        //public List<List<string>> ReadSmtpIn(string dir)
        //{
        //    List<List<string>> files = new List<List<string>>();
        //    if (Directory.Exists(dir))
        //    {
        //        string[] fileNames = Directory.GetFiles(dir, "*SMTP-(in)*");
        //        ReadFiles(out files, ref fileNames);
        //    }
        //    else
        //    {
        //        Logging.AddErr($"Directory {dir} not found!");
        //        return null;
        //    }

        //    return files;
        //}

        //public List<List<string>> ReadSmtpOut(string dir)
        //{
        //    List<List<string>> files = new List<List<string>>();
        //    if (Directory.Exists(dir))
        //    {
        //        string[] fileNames = Directory.GetFiles(dir, "*SMTP-(out)*");
        //        ReadFiles(out files, ref fileNames);
        //    }
        //    else
        //    {
        //        Logging.AddErr($"Directory {dir} not found!");
        //        return null;
        //    }

        //    return files;
        //}

        //private void ReadFiles(out List<List<string>> files, ref string[] fileNames)
        //{
        //    List<List<string>> filesTmp = new List<List<string>>();

        //    foreach (var filename in fileNames)
        //    {
        //        if (File.Exists(filename))
        //        {
        //            using (StreamReader sr = new StreamReader(filename))
        //            {
        //                Logging.AddInf($"Read file {filename}");
        //                List<string> file = new List<string>();
        //                string line;
        //                while ((line = sr.ReadLine()) != null)
        //                {
        //                    file.Add(line);
        //                }
        //                filesTmp.Add(file);
        //            }
        //        }
        //        else
        //        {
        //            Logging.AddErr($"File {filename} not found!");
        //        }
        //    }
        //    files = filesTmp;
        //}
    }
}
