using System;
using System.Collections.Generic;

namespace Sdf.Common.Enums
{
    public static class EnumMathHelper
    {
        public static TEnum AddEnumType<TEnum>(TEnum sourceEnumType, TEnum removeEnumType) where TEnum : struct, Enum
        {
            var result = Convert.ToInt16(sourceEnumType) | Convert.ToInt16(removeEnumType);

            return (TEnum)Enum.ToObject(typeof(TEnum), result);
        }

        public static TEnum RemoveEnumType<TEnum>(TEnum sourceEnumType, TEnum removeEnumType) where TEnum : struct, Enum
        {
            var sourceTypeNum = Convert.ToInt32(sourceEnumType);
            var removeTargetTypeNum = Convert.ToInt32(removeEnumType);
            var result = sourceTypeNum &= ~removeTargetTypeNum;

            return (TEnum)Enum.ToObject(typeof(TEnum), result);
        }

        public static bool HasEnumType<TEnum>(TEnum sourceEnumType, TEnum targetEnumType) where TEnum : struct, Enum
        {
            var result = Convert.ToInt32(sourceEnumType) & Convert.ToInt32(targetEnumType);

            return result == Convert.ToInt32(targetEnumType);
        }

        public static List<TEnum> GetEnumTypes<TEnum>(TEnum enumType) where TEnum : struct, Enum
        {
            var list = new List<TEnum>();
            foreach (TEnum item in Enum.GetValues<TEnum>())
            {
                if ((Convert.ToInt16(enumType) & Convert.ToInt16(item)) == Convert.ToInt16(item))
                {
                    list.Add(item);
                }
            }

            return list;
        }
    }
}
