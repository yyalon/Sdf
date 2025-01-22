using System;
using System.ComponentModel;

namespace Sdf.Common
{
    public static partial class ExtensionMethods
    {
        public static string GetDescriptionFromEnumValue(this object enumValue)
        {
            try
            {
                if (enumValue == null) 
                {
                    return string.Empty;
                }

                Type enumType = enumValue.GetType();
                object o = Enum.Parse(enumType, enumValue.ToString());

                string name = o.ToString();
                DescriptionAttribute[] customAttributes = (DescriptionAttribute[])enumType.GetField(name).GetCustomAttributes(typeof(DescriptionAttribute), false);
                if ((customAttributes != null) && (customAttributes.Length == 1))
                {
                    return customAttributes[0].Description;
                }
                return name;
            }
            catch
            {
                return "未知";
            }
        }
    }
}
