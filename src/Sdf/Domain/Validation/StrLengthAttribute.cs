using System;
using System.ComponentModel.DataAnnotations;

namespace Sdf.Domain.Validation
{
    public class StrLengthAttribute:ValidationAttribute
    {

        public StrLengthAttribute(int maximumLength)
        {
            this.MaximumLength = maximumLength;
        }

        public int MaximumLength { get; private set; }

        public int MinimumLength { get; set; }

        public override string FormatErrorMessage(string name)
        {
            this.EnsureLegalLengths();
            return String.Format(ErrorMessage, name, this.MaximumLength, this.MinimumLength);
        }
        public override bool IsValid(object value)
        {
            this.EnsureLegalLengths();
            if (value == null || String.IsNullOrEmpty(value.ToString()))
            {
                return true;
            }
            if (value is string)
            {
                string str = value as string;
                if (str.Length > MaximumLength || str.Length < MinimumLength)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }
        private void EnsureLegalLengths()
        {
            if (this.MaximumLength < 0)
            {
                throw new InvalidOperationException("最大长度不能小于0");
            }

            if (this.MaximumLength < this.MinimumLength)
            {
                throw new InvalidOperationException("最大长度不能小于最小长度");
            }
        }
    }
}
