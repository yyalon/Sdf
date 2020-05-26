using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Sdf.Common
{
    public class VerifyUtils
    {
        /// <summary>
        /// 验证手机号
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public static bool VerifyMobile(string mobile)
        {
            string pattern = @"^1[23456789]\d{9}$";
            return Regex.IsMatch(mobile, pattern);
        }
        /// <summary>
        /// 验证邮箱
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool VerifyEmail(string email)
        {
            string pattern = @"^[A-Za-z0-9\u4e00-\u9fa5]+@[a-zA-Z0-9_-]+(\.[a-zA-Z0-9_-]+)+$";
            return Regex.IsMatch(email, pattern);
        }
        /// <summary>
        /// 验证身份证号
        /// </summary>
        /// <param name="idCard"></param>
        /// <returns></returns>
        public static bool VerifyIDCard(string idCard)
        {
            string pattern = @"(^\d{15}$)|(^\d{18}$)|(^\d{17}(\d|X|x)$)";
            return Regex.IsMatch(idCard, pattern);
        }
        public static bool VerifyUrl(string url)
        {
            string pattern = @"^((ht | f)tps ?):\/\/[\w\-] + (\.[\w\-]+)+([\w\-\.,@?^=% &:\/ ~\+#]*[\w\-\@?^=%&\/~\+#])?$";
            return Regex.IsMatch(url, pattern);
        }
        /// <summary>
        /// 验证是否含有特殊字符
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool HasSpecialChart(string text)
        {
            string pattern1 = @"[`~!@#$%^&*_+<>?:\""{ },.\/; '\s]";
            string pattern2 = @"[·！#￥——：；“”‘、，|。？、\s]";
            return Regex.IsMatch(text, pattern1) || Regex.IsMatch(text, pattern2);
        }
    }
}
