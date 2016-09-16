using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ff.coffee.webapp.Helpers
{
    public static class SecurityHelpers
    {
        public static string EncryptText(string textToEncrypt)
        {
            StringBuilder result = new StringBuilder();

            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(textToEncrypt));
                int length = data.Length;

                for (int i = 0; i < length; i++)
                {
                    result.Append(data[i].ToString("x2"));
                }

            }
            return result.ToString();
        }
    }
}