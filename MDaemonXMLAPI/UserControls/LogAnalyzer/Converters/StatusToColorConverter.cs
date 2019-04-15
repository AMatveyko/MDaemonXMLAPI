using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace MDaemonXMLAPI.UserControls.LogAnalyzer.Converters
{

    public class StatusToColorConverter : IValueConverter
    {

        private static Dictionary<string, Brush> colorDict = new Dictionary<string, Brush>()
        {
            { "successful", Brushes.Lavender },
            { "terminated", Brushes.MistyRose },
            { "null", Brushes.LightGray }
        };

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                switch (value.ToString())
                {
                    case "successful":
                        return colorDict["successful"];
                    case "terminated":
                        return colorDict["terminated"];
                    default:
                        return colorDict["null"];
                }
            }
            else
                return colorDict["null"];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
