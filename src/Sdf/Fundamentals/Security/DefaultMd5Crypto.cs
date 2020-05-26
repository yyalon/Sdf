using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Sdf.Fundamentals.Security
{
    public class DefaultMd5Crypto : IMd5Crypto
    {
        public string Md5crypto32(string str)
        {
            if (String.IsNullOrEmpty(str))
                return null;
            return ComputeMd5(Encoding.Default.GetBytes(str));
        }
        public string ComputeMd5(byte[]  bytes)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] data = md5Hasher.ComputeHash(bytes);
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }
}
