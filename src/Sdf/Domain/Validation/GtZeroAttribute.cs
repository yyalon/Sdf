using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sdf.Domain.Validation
{
    public class GtZeroAttribute : ValidationAttribute
    {
        public GtZeroAttribute(string errorMessage) : base(errorMessage)
        {

        }
        public override bool IsValid(object value)
        {
            string valueStr = value.ToString();
            try
            {
                double num = 0d;
                Double.TryParse(valueStr,out num);
                return num > 0;
            }
            catch { }
            return false;
        }
    }
}
