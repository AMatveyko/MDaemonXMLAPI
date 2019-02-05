using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MDaemonXMLAPI.Model
{
    public static class Checks
    {
        public static bool Password(string passwordInput, out int length, int defaultLength, int maxLength, int minLength)
        {
            int newLength;
            if (Int32.TryParse(passwordInput, out newLength))
            {
                if((newLength > maxLength)||(newLength < minLength))
                {
                    length = defaultLength;
                    return false;
                }
                else
                {
                    length = newLength;
                    return true;
                }
            }
            else
            {
                length = defaultLength; ;
                return false;
            }
        }
        public static bool MailBoxName(string userName)
        {
            Regex rgxFirst = new Regex("^[A-Za-z0-9]");
            Regex rgxCenter = new Regex("^[A-Za-z0-9-.]+$");
            Regex rgxLast = new Regex("[A-Za-z0-9]$");

            if (userName == null)
                return false;
            if (!(rgxFirst.IsMatch(userName)))
            {
                //System.Windows.MessageBox.Show("Не верный шаблон!\nПервый символ только буква латинского алфавита или цифра.");
                return false;
            }
            if (!(rgxCenter.IsMatch(userName)))
            {
                //System.Windows.MessageBox.Show("Не верный шаблон!\nШаблон имени может содержать только буквы латинского алфавита,цифры, тире и точку.");
                return false;
            }
            if (!(rgxLast.IsMatch(userName)))
            {
                //System.Windows.MessageBox.Show("Не верный шаблон!\nПоследний символ только буква латинского алфавита или цифра.");
                return false;
            }
            return true;
        }
        public static bool NoDublicate( string newName, string domain, ObservableCollection<MailBox> mailBoxes)
        {
            return ( mailBoxes.FirstOrDefault(x=>(x.Name == newName)&&(x.Domain == domain)) == null );
        }
    }
}
