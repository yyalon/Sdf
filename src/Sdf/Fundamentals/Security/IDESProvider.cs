using System;
using System.Collections.Generic;
using System.Text;

namespace Sdf.Fundamentals.Security
{
    public interface IDESProvider
    {
        /// <summary>
        ///  加密
        /// </summary>
        /// <param name="text">待加密字符串</param>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        string Encrypt(string text, string key=null);
        string EncryptToNumber(string text, string key = null);
        string DecryptFromNumber(string encryptedText, string key = null);
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="encryptedText">加密过后字符串</param>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        string Decrypt(string encryptedText, string key = null);
    }
}
