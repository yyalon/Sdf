using System;

namespace Sdf.Common.Enums
{
    [AttributeUsage(AttributeTargets.Enum)]
    public class EnumDescriptionAttribute: Attribute
    {
        public string EnumCode { get; }

        public EnumDescriptionAttribute(string enumCode)
        {
            EnumCode = enumCode ?? throw new ArgumentNullException(nameof(enumCode));
        }
    }
}
