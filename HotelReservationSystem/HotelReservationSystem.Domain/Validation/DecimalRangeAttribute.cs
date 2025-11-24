using System;
using System.ComponentModel.DataAnnotations;

namespace HotelReservationSystem.Domain.Validation
{
    /// <summary>
    /// Validation attribute to ensure a decimal value is within a specific range
    /// </summary>
    public class DecimalRangeAttribute : ValidationAttribute
    {
        private readonly decimal _minimum;
        private readonly decimal _maximum;

        public DecimalRangeAttribute(double minimum, double maximum)
        {
            _minimum = (decimal)minimum;
            _maximum = (decimal)maximum;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success; 
            }

            if (value is decimal decimalValue)
            {
                if (decimalValue < _minimum)
                {
                    return new ValidationResult(
                        ErrorMessage ?? $"{validationContext.DisplayName} must be at least {_minimum:C}"
                    );
                }

                if (decimalValue > _maximum)
                {
                    return new ValidationResult(
                        ErrorMessage ?? $"{validationContext.DisplayName} cannot exceed {_maximum:C}"
                    );
                }

                return ValidationResult.Success;
            }

            return new ValidationResult("Invalid decimal format");
        }
    }
}
