using Sdf.Common;
using System.ComponentModel.DataAnnotations;

namespace Sdf.Domain.Validation
{
    public  class IDCardAttribute :  ValidationAttribute
    {
        public IDCardAttribute() { }
        public override bool IsValid(object value)
        {
            if (value == null || value.ToString()=="")
            {
                return true;
            }
            if (value is string)
            {
                return VerifyUtils.VerifyIDCard(value.ToString());
            }
            return false;
        }
    }
}
