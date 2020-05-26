using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sdf.Exceptions
{
    public class InvalidateException: Exception
    {
        public ValidationResult ValidationResult { get; set; }
        public InvalidateException(ValidationResult validationResult):base(validationResult.ErrorMessage)
        {
            ValidationResult = validationResult;
        }
    }
}
