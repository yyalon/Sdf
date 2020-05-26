using Sdf.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace Sdf.Domain.Validation
{
    public class NotAllowdSpecialChartAttribute: ValidationAttribute
    {
        public NotAllowdSpecialChartAttribute():base("字段不允许出现特殊字符") { }
        public override bool IsValid(object value)
        {
            if (value == null || String.IsNullOrEmpty(value.ToString()))
            {
                return true;
            }
            if (value is string)
            {
                return !VerifyUtils.HasSpecialChart(value.ToString());
            }
            return false;
        }
    }
}
