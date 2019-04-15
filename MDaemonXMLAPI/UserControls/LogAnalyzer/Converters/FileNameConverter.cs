using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MDaemonXMLAPI.UserControls.LogAnalyzer.Converters
{
    public class FileNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string fileName = value as String;
            string fileNameMod = String.Empty;
            if (fileName != null)
            {
                string[] nameSlice = fileName.Split('-');
                fileNameMod =
                    $"{nameSlice[1]}-{nameSlice[2]}-{nameSlice[3]} {nameSlice[4]} {nameSlice[5].Substring(0, nameSlice[5].Length - 4)}";
            }
            return fileNameMod;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
