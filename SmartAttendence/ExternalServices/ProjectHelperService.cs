using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SmartAttendence.ExternalServices
{
   public static class ProjectHelperService
    {
        private static string key = "zlgmb";
        public static string Encode(string encodeMe)
        {
            byte[] clearBytes = Encoding.Unicode.GetBytes(encodeMe);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(key, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Dispose();
                    }
                    encodeMe = Convert.ToBase64String(ms.ToArray());
                }
            }
            return encodeMe;
        }
        public static string Decode(string decodeMe)
        {
            byte[] cipherBytes = Convert.FromBase64String(decodeMe);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(key, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Dispose();
                    }
                    decodeMe = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return decodeMe;
        }
        public static string GetUniqueDigits()
        {
            var bytes = new byte[4];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);
            uint random = BitConverter.ToUInt32(bytes, 0) % 100000000;
            return String.Format("{0:D8}", random);
        }


        public static DateTime GetLastDateOfCurrentMonth(this DateTime dateTime)
        {
            DateTime lastDateOfMonth;
            try
            {
                lastDateOfMonth = new DateTime(dateTime.Year, dateTime.Month, DateTime.DaysInMonth(dateTime.Year, dateTime.Month));
            }
            catch (Exception)
            {
                lastDateOfMonth = DateTime.MinValue;
            }
            return lastDateOfMonth;
        }

        public static int GetLastDayOfCurrentMonth(int? Year, int? Month)
        {
            int lastDayOfMonth = 0;
            try
            {
                lastDayOfMonth = DateTime.DaysInMonth(Year.Value, Month.Value);
            }
            catch (Exception)
            {
                lastDayOfMonth = 0;
            }
            return lastDayOfMonth;
        }

        public static DateTime FirstDateOfCurrentMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1, 0, 0, 0);
        }

    }
}
