using Sdf.Common;
using System.ComponentModel.DataAnnotations;

namespace Sdf.Domain.Validation
{
    public class PhoneAttribute : ValidationAttribute
    {
        public PhoneAttribute() { }
        public override bool IsValid(object value)
        {
            if (value == null || value.ToString() == "")
            {
                return true;
            }
            if (value is string)
            {
                return VerifyUtils.VerifyMobile(value.ToString());
            }
            return false;
        }
    }
}
