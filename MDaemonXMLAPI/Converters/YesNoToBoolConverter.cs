using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace MDaemonXMLAPI.Converters
{
    public class YesNoToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Boolean enable;
            string valueStr = (string) value;
            if (valueStr == "Yes")
            {
                enable = true;
            }
            else
            {
                enable = false;
            }

            return enable;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string enable;
            bool valueBool = (bool) value;
            if (valueBool)
            {
                enable = "Yes";
            }
            else
            {
                enable = "No";
            }
            return enable;
        }
    }
}
