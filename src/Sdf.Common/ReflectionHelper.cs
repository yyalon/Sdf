using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Sdf.Common
{
    public class ReflectionHelper
    {
        public static bool TypeIsSame(Type type1, Type type2)
        {
            return type1.FullName.ToLower() == type2.FullName.ToLower();
        }
        /// <summary>
        /// 类型是否含有无参构造函数
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsHaveNoparameterConstructor(Type type)
        {
            var constructors = type.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (var item in constructors)
            {
                if (item.GetParameters().Length == 0)
                    return true;
            }
            return false;
        }
        public static T CreateInastanceByPrivateConstructor<T>(Type type)
        {
            var constructors = type.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            ConstructorInfo constructor = null;
            foreach (var item in constructors)
            {
                if (item.GetParameters().Length == 0)
                {
                    constructor = item;
                    break;
                }
            }
            if (constructor != null)
                return (T)constructor.Invoke(null);
            return default(T);
        }
    }
}
