using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Sdf.Common
{
    public  static partial class  ExtensionMethods
    {
        /// <summary>
        /// 扩展方法：Object转换Int32,失败返回0
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int ToInt(this object obj)
        {
            try
            {
                return Convert.ToInt32(obj);
            }
            catch
            {
                return 0;
            }
        }
        /// <summary>
        /// 扩展方法：Object转换Int64,失败返回0
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static long ToInt64(this object obj)
        {
            try
            {
                return Convert.ToInt64(obj);
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static double ToDouble(this object obj)
        {
            try
            {
                return Convert.ToDouble(obj);
            }
            catch (Exception)
            {
                return 0.00f;
            }
        }
        /// <summary>
        /// 截取指定长度的字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string MySubString(this string str, int length)
        {
            if (String.IsNullOrEmpty(str))
            {
                return "";
            }
            if (str.Length <= length)
                return str;
            else
            {
                return str.Substring(0, length);
            }
        }
        /// <summary>
        /// 按字节长度（一个汉字占两个字节），截取固定长度的字符串,返回截取后的长度，后面追加"..."
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string MySubStringByBt(this string str, int length)
        {
            if (String.IsNullOrEmpty(str))
            {
                return "";
            }

            if (str.GetLengthB() <= (length * 2))
                return str;
            else
            {
                char[] cArr = str.ToCharArray();
                int i = 0;
                int j = 0;
                foreach (char item in cArr)
                {
                    if (i >= length * 2)
                        break;
                    if ((int)item > 255)
                        i = i + 2;
                    else
                        i++;
                    j++;
                }
                return str.Substring(0, j);
            }
        }
        /// <summary>
        /// 返回字符串的字节长度（汉字占两个字节）
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int GetLengthB(this string str)
        {
            if (String.IsNullOrEmpty(str))
                return 0;
            int i = 0;
            char[] cArr = str.ToCharArray();
            foreach (char item in cArr)
            {
                if ((int)item > 255)
                    i = i + 2;
                else
                    i++;
            }
            return i;
        }
        public static string MySubString(this string str, int length, string Replace)
        {
            if (String.IsNullOrEmpty(str))
            {
                return "";
            }
            if (str.Length <= length)
                return str;
            else
            {
                return str.Substring(0, length) + Replace;
            }
        }

        /// <summary>
        /// to bool
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool ToBool(this object obj)
        {
            try
            {
                return Convert.ToBoolean(obj);
            }
            catch
            {
                return false;
            }
        }
        
        public static string ListToStr(this ICollection list, string delimiter = ",")
        {
            string res = "";
            foreach (var item in list)
            {
                if (res.Length == 0)
                    res = item.ToString();
                else
                    res = res + delimiter + item.ToString();
            }
            return res;
        }
        public static List<T> StrToList<T>(this string str, char delimiter = ',')
        {
            List<T> list = new List<T>();
            if (String.IsNullOrEmpty(str))
                return list;
            string[] arr = str.Split(delimiter);
            foreach (var item in arr)
            {
                if (!String.IsNullOrEmpty(item))
                {
                    try
                    {
                        T t = (T)Convert.ChangeType(item, typeof(T));
                        list.Add(t);
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }
            return list;
        }
        public static bool IsSubclassOfOrInherit(this Type myselfType, Type type)
        {
            return myselfType.IsSubclassOf(type) || myselfType == type;
        }
        public static string ObjectPropertToString(this object obj,string separator=" ")
        {
            Type type = obj.GetType();
            var pros = type.GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < pros.Length; i++)
            {
                var item = pros[i];
                var value = item.GetValue(obj);
                if (i != pros.Length - 1)
                {
                    stringBuilder.Append($"{item.Name}:{value}{separator}");
                }
                else
                {
                    stringBuilder.Append($"{item.Name}:{value}");
                }
                
            }
            return stringBuilder.ToString();
        }
    }
}
