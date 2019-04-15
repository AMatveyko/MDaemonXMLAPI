using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MDaemonXMLAPI.UserControls.LogAnalyzer.Converters
{
    public class TranslateConverter : IValueConverter
    {

        private static Dictionary<string, string> dictionary = new Dictionary<string, string>()
        {
            { "input" , "Входящие" },
            { "output" , "Исходящие" },
            { "successful", "Успех" },
            { "terminated", "Провал" }
        };

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                string converValue = value.ToString();
                if (dictionary.ContainsKey(converValue))
                {
                    return dictionary[converValue];
                }
            }
            return "Нет перевода";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return String.Empty;
        }
    }
}
