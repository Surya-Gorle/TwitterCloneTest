using System.Text;
using System.Security.Cryptography;

namespace TCloneApp.Models
{
    public static class EncryptUtil
    {
        public static string Md5Hash(string text)
        {
            var strBuilder = new StringBuilder();
            if (!string.IsNullOrEmpty(text))
            {
                MD5 md5 = new MD5CryptoServiceProvider();
                md5.ComputeHash(Encoding.ASCII.GetBytes(text));
                var result = md5.Hash;
                foreach (var t in result)
                {
                    strBuilder.Append(t.ToString("x2"));
                }
            }
            return strBuilder.ToString();
        }
    }
}