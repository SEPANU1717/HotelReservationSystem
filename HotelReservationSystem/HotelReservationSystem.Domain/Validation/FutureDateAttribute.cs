using System;
using System.ComponentModel.DataAnnotations;

namespace HotelReservationSystem.Domain.Validation
{
    /// <summary>
    /// Validation attribute to ensure a date is not in the past
    /// </summary>
    public class FutureDateAttribute : ValidationAttribute
    {
        private readonly bool _allowToday;

        public FutureDateAttribute(bool allowToday = true)
        {
            _allowToday = allowToday;
            ErrorMessage = allowToday 
                ? "Date cannot be in the past" 
                : "Date must be in the future";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success; // Let Required attribute handle null values
            }

            if (value is DateTime dateValue)
            {
                var today = DateTime.Today;
                
                if (_allowToday)
                {
                    if (dateValue.Date < today)
                    {
                        return new ValidationResult(ErrorMessage ?? "Date cannot be in the past");
                    }
                }
                else
                {
                    if (dateValue.Date <= today)
                    {
                        return new ValidationResult(ErrorMessage ?? "Date must be in the future");
                    }
                }

                return ValidationResult.Success;
            }

            return new ValidationResult("Invalid date format");
        }
    }
}
