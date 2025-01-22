using Sdf.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sdf.Domain.Validation
{
    public class IpAttribute: ValidationAttribute
    {
        public IpAttribute() 
        {
            ErrorMessage = "IP地址格式错误";
        }
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }
            if (value is string)
            {
                return VerifyUtils.VerifyIP(value.ToString());
            }
            return false;
        }
    }
}
