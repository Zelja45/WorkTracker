using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTracker.Utils
{
    public class Util
    {
        public static string HashPassword(string password)
        {
            using (var myHash = SHA256.Create())
            {
                var byteArray = Encoding.UTF8.GetBytes(password);
                var hashedResult = myHash.ComputeHash(byteArray);

                string result = string.Concat(Array.ConvertAll(hashedResult, h => h.ToString("X2")));
                return result;
            }
        }
    }
}
