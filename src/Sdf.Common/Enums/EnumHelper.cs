using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Sdf.Common.Enums
{
    public class EnumHelper
    {
        static Dictionary<string, Type> enumTypes = new Dictionary<string, Type>();
        static EnumHelper()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (var assemblie in assemblies)
            {
                var types = assemblie.GetTypes();

                foreach (var type in types)
                {
                    var enumDescriptionAttributes = type.GetCustomAttributes(typeof(EnumDescriptionAttribute), false);
                    if (enumDescriptionAttributes != null && enumDescriptionAttributes.Length > 0)
                    {
                        var first = enumDescriptionAttributes[0] as EnumDescriptionAttribute;
                        enumTypes.Add(first.EnumCode, type);
                    }
                }
            }
        }

        public static List<EnumModel> GetEnumModels<TEnum>() where TEnum : Enum
        {
            List<EnumModel> enumModels = new();
            Type enumType = typeof(TEnum);
            var values = Enum.GetValues(enumType);
            foreach (var item in values)
            {
                int index = (int)item;
                string label = item.ToString();
                DescriptionAttribute[] customAttributes = (DescriptionAttribute[])enumType.GetField(label).GetCustomAttributes(typeof(DescriptionAttribute), false);
                if ((customAttributes != null) && (customAttributes.Length == 1))
                {
                    label = customAttributes[0].Description;
                }
                EnumModel enumModel = new ()
                {
                    EnumIndex = index,
                    EnumLabel = label
                };
                enumModels.Add(enumModel);
            }
            return enumModels;
        }

        public static List<EnumModel> GetEnumModelsByCode(string enumCode)
        {
            List<EnumModel> enumModels = new();
            if (!enumTypes.TryGetValue(enumCode, out Type enumType))
            {
                return new List<EnumModel>();
            }
            
            var values = Enum.GetValues(enumType);
            foreach (var item in values)
            {
                int index = (int)item;
                string label = item.ToString();
                DescriptionAttribute[] customAttributes = (DescriptionAttribute[])enumType.GetField(label).GetCustomAttributes(typeof(DescriptionAttribute), false);
                if ((customAttributes != null) && (customAttributes.Length == 1))
                {
                    label = customAttributes[0].Description;
                }
                EnumModel enumModel = new()
                {
                    EnumIndex = index,
                    EnumLabel = label
                };
                enumModels.Add(enumModel);
            }
            return enumModels;
        }
    }
}
