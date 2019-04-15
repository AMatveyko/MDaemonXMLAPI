using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace MDaemonXMLAPI.Converters
{
    public class FrozenDisableToColorConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string frozen = values[0] as String;
            string disabled = values[1] as String;
            if ((frozen == null) && (disabled == null))
            {
                return Brushes.LightGray;
            }
            else
            {
                if ((frozen == "No") && (disabled == "No"))
                    return Brushes.Honeydew;
                else if (disabled == "Yes")
                    return Brushes.LightPink;
                else if (frozen == "Yes")
                    return Brushes.Lavender;
                else
                    return Brushes.Wheat;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
