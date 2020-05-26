using Sdf.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Sdf.Fundamentals.Security
{
    public class TripleDES : IDESProvider
    {

        public TripleDES()
        {
            
        }
       
        public string DefaultCryptoKey { get; set; }
       
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="clearText"></param>
        /// <returns>Base64的加密字符串</returns>
        public string Encrypt(string text, string key = null)
        {
            byte[] resultArray = _Encrypt(text, key);
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        public string EncryptToNumber(string text, string key = null)
        {
            byte[] resultArray = _Encrypt(text, key);
            StringBuilder stringBuilder = new StringBuilder(resultArray.Length);
            int i = 0;
            foreach (var item in resultArray)
            {
                if (i == 0)
                {
                    stringBuilder.Append(item.ToString());
                }
                else
                {
                    stringBuilder.Append($"-{item}");
                }
                i++;
            }
            return stringBuilder.ToString();
        }
        public string DecryptFromNumber(string encryptedText, string key = null)
        {
            string[] arr = encryptedText.Split('-');
            byte[] inputArray = new byte[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                inputArray[i] = (byte)(arr[i].ToInt());
            }
            byte[] resultArray = _Decrypt(inputArray, key);
            return Encoding.UTF8.GetString(resultArray);
        }
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="encryptedText">Base64的加密字符串</param>
        /// <returns></returns>
        public string Decrypt(string encryptedText, string key=null)
        {
            byte[] inputArray = Convert.FromBase64String(encryptedText);
            byte[] resultArray = _Decrypt(inputArray, key);
            return Encoding.UTF8.GetString(resultArray);
        }
        private byte[] _Encrypt(string text, string key = null)
        {
            byte[] inputArray = Encoding.UTF8.GetBytes(text);
            var tripleDES = System.Security.Cryptography.TripleDES.Create();
            var byteKey = Encoding.UTF8.GetBytes(key != null ? key : DefaultCryptoKey);
            byte[] allKey = new byte[24];
            Buffer.BlockCopy(byteKey, 0, allKey, 0, 16);
            Buffer.BlockCopy(byteKey, 0, allKey, 16, 8);
            tripleDES.Key = allKey;
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            return resultArray;
        }
        private byte[] _Decrypt(byte[] inputArray, string key = null)
        {
            var tripleDES = System.Security.Cryptography.TripleDES.Create();
            var byteKey = Encoding.UTF8.GetBytes(key != null ? key : DefaultCryptoKey);
            byte[] allKey = new byte[24];
            Buffer.BlockCopy(byteKey, 0, allKey, 0, 16);
            Buffer.BlockCopy(byteKey, 0, allKey, 16, 8);
            tripleDES.Key = allKey;
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            return resultArray;
        }
    }
}
