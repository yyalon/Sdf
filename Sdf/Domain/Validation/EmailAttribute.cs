using Sdf.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace Sdf.Domain.Validation
{
    public class EmailAttribute: ValidationAttribute
    {
        public EmailAttribute() { }
        public override bool IsValid(object value)
        {
            if (value==null || String.IsNullOrEmpty(value.ToString()))
            {
                return true;
            }
            if (value is string)
            {
               return VerifyUtils.VerifyEmail(value.ToString());
            }
            return false;
        }
    }
}
