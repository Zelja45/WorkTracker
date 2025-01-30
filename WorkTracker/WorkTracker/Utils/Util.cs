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
        public static decimal calculateIncome(TimeOnly workTime,decimal hourlyRate)
        {
            decimal totalHours = workTime.Hour +
                             workTime.Minute / 60m;

            return totalHours * hourlyRate;
        }

        public static TimeOnly SumTimeOnly(TimeOnly time1, TimeOnly time2) 
        {
            int totalSeconds1 = time1.Hour * 3600 + time1.Minute * 60 + time1.Second;
            int totalSeconds2 = time2.Hour * 3600 + time2.Minute * 60 + time2.Second;

            int totalSeconds = totalSeconds1 + totalSeconds2;

            int hours = (totalSeconds / 3600) % 24;
            int minutes = (totalSeconds % 3600) / 60;
            int seconds = totalSeconds % 60;

            return new TimeOnly(hours, minutes, seconds);
        }
    }
}
