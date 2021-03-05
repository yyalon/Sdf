using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Sdf.Common.Enums
{
    public class EnumHelper
    {
        public static List<EnumModel> GetEnumModels<TEnum>() where TEnum : Enum
        {
            List<EnumModel> enumModels = new List<EnumModel>();
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
                EnumModel enumModel = new EnumModel()
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
