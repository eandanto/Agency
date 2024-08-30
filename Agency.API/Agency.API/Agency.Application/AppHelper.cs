using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Application
{
    public class AppHelper
    {
        public static string HashPassword(string password)
        {
            UnicodeEncoding uEncode = new UnicodeEncoding();
            byte[] data = uEncode.GetBytes(password);
            data = new System.Security.Cryptography.SHA512Managed().ComputeHash(data);
            return Convert.ToBase64String(data);
        }
    }
}
