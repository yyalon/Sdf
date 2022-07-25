using Sdf.Common;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Sdf.Fundamentals.Security
{
    public class AESEncryptionProvider : IEncryptionProvider
    {
        private byte[] defaultKey;
        private byte[] vectorKey;

        public AESEncryptionProvider(string key, string vector)
        {
            defaultKey = GetByteArr(key, 32);
            vectorKey = GetByteArr(vector, 16);
        }

        public AESEncryptionProvider()
        {
        }

        public void SetDefaultKey(string key, string vector)
        {
            defaultKey = GetByteArr(key, 32);
            vectorKey = GetByteArr(vector, 16);
        }

        public string Decrypt(string encryptedText, string key = null)
        {
            encryptedText = encryptedText.Replace(' ', '+');

            byte[] inputByteArray = Convert.FromBase64String(encryptedText);
            byte[] resultArray = InternalDecryptAES(inputByteArray, key);

            return Encoding.UTF8.GetString(resultArray);
        }

        public string DecryptFromNumber(string encryptedText, string key = null)
        {
            string[] arr = encryptedText.Split('-');
            byte[] inputArray = new byte[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                inputArray[i] = (byte)(arr[i].ToInt());
            }
            byte[] resultArray = InternalDecryptAES(inputArray, key);

            return Encoding.UTF8.GetString(resultArray);
        }

        public string Encrypt(string text, string key = null)
        {
            var encryptArr = InternalEncryptAES(text, key);

            return Convert.ToBase64String(encryptArr);
        }

        public string EncryptToNumber(string text, string key = null)
        {
            var encryptArr = InternalEncryptAES(text, key);

            StringBuilder stringBuilder = new(encryptArr.Length);
            int i = 0;
            foreach (var item in encryptArr)
            {
                if (i == 0)
                {
                    stringBuilder.Append(item);
                }
                else
                {
                    stringBuilder.Append($"-{item}");
                }
                i++;
            }
            return stringBuilder.ToString();
        }

        private byte[] InternalEncryptAES(string encryptString, string key = null)
        {
            byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
            Aes aes = Aes.Create();

            MemoryStream mStream = new();
            CryptoStream cStream = new(mStream, aes.CreateEncryptor(key == null ? defaultKey : GetByteArr(key, 32), vectorKey), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();

            return mStream.ToArray();
        }

        private byte[] InternalDecryptAES(byte[] inputByteArray, string key)
        {
            Aes DCSP = Aes.Create();

            MemoryStream mStream = new ();
            CryptoStream cStream = new (mStream, DCSP.CreateDecryptor(key == null ? defaultKey : GetByteArr(key, 32), vectorKey), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();

            return mStream.ToArray();
        }

        private static byte[] GetByteArr(string str, int length)
        {
            var arr = Encoding.UTF8.GetBytes(str);
            if (arr.Length != length)
            {
                throw new Exception("加密参数长度错误");
            }

            return arr;
        }
    }
}
