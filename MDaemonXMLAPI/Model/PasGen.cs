using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDaemonXMLAPI.Model
{
    public static class PasGen
    {
        private static int rnds = 0;
        public static string Gen(bool spec, int length)
        {
            rnds += 1;
            string pass = String.Empty;
            string dict = "qwertyuiopasdfghjklzxcvbnm";
            if(spec)
                dict += "!@#$%^&*()";
            dict += "123456789";
            dict += "QWERTYUIOPASDFGHJKLZXCVBNM";
            Random rnd = new Random(DateTime.Now.Millisecond + rnds);
            int lng = dict.Length;
            for (int i = 0; i < length; i++)
                pass += dict[rnd.Next(lng)];
            return pass;
        }
    }
}
