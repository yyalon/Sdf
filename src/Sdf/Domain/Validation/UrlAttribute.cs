using Sdf.Common;
using System.ComponentModel.DataAnnotations;

namespace Sdf.Domain.Validation
{
    public class UrlAttribute : ValidationAttribute
    {
        public UrlAttribute() { }
        public override bool IsValid(object value)
        {
            if (value==null)
            {
                return true;
            }
            if (value is string)
            {
                return VerifyUtils.VerifyUrl(value.ToString());
            }
            return false;
        }
    } 
}
