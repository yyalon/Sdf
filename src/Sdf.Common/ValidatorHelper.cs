using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sdf.Common
{
    public class ValidatorHelper
    {
        public static List<ValidationResult> GetValidationResult(object entity)
        {
            var context = new ValidationContext(entity);
            var errors = new List<ValidationResult>();
            var attributerors = new List<ValidationAttribute>();
            Validator.TryValidateObject(entity, context, errors, true);
            return errors;
        }
    }
}
