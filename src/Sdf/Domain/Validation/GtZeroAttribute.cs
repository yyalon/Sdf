using System.ComponentModel.DataAnnotations;

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

            if (double.TryParse(valueStr, out double num))
            {
                return num > 0;
            }

            return false;
        }
    }
}
