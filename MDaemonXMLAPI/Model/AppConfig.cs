using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace MDaemonXMLAPI.Model
{
    public static class AppConfig
    {
        private static String _hostDefValue = "1.1.1.1";
        private static String _userNameDefValue = "userForApiTest@domain.test";
        private static String _dirLogDefValue = $@"MDaemonLogs";

        private static String _hostKey = "Host";
        private static String _userKey = "User";
        private static String _dirLogKey = "LogDir";

        private static String _section = "appSettings";


        public static void WriteHost(string newValue)
            => WriteValue(_hostKey, _hostDefValue, newValue);
        public static void WriteUser(string newValue)
            => WriteValue(_userKey, _userNameDefValue, newValue);
        public static void WriteLogDir(string newValue)
            => WriteValue(_dirLogKey, _dirLogDefValue, newValue);

        private static void WriteValue(string key, string defValue, string newValue)
        {
            if (!ConfigurationManager.AppSettings.AllKeys.Contains(key))
            {
                    System.Configuration.Configuration currentConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    currentConfig.AppSettings.Settings.Add(key, newValue);
                    currentConfig.Save(ConfigurationSaveMode.Modified);
                    ConfigurationManager.RefreshSection(_section);
            }
            else if (ConfigurationManager.AppSettings[key] != newValue)
            {
                System.Configuration.Configuration currentConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                currentConfig.AppSettings.Settings[key].Value = newValue;
                currentConfig.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(_section);
            }
        }

        public static string ReadHost()
            => ReadKey(_hostKey, _hostDefValue);

        public static string ReadUserName()
            => ReadKey(_userKey, _userNameDefValue);

        public static string ReadDirLog()
            => ReadKey(_dirLogKey, _dirLogDefValue);

        private static string ReadKey(string key, string defValue)
        {
            if (ConfigurationManager.AppSettings.AllKeys.Contains(key))
                return ConfigurationManager.AppSettings[key];
            else
                return defValue;
        }
    }
}
