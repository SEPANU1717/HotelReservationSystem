using System;
using System.ComponentModel.DataAnnotations;

namespace HotelReservationSystem.Domain.Validation
{
    /// <summary>
    /// Validation attribute to ensure a person meets minimum age requirement
    /// </summary>
    public class MinimumAgeAttribute : ValidationAttribute
    {
        private readonly int _minimumAge;

        public MinimumAgeAttribute(int minimumAge)
        {
            _minimumAge = minimumAge;
            ErrorMessage = $"Must be at least {_minimumAge} years old";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Date of birth is required");
            }

            if (value is DateTime dateOfBirth)
            {
                var today = DateTime.Today;
                var age = today.Year - dateOfBirth.Year;
                
                // Adjust age if birthday hasn't occurred yet this year
                if (dateOfBirth.Date > today.AddYears(-age))
                {
                    age--;
                }

                if (age < _minimumAge)
                {
                    return new ValidationResult(ErrorMessage ?? $"Must be at least {_minimumAge} years old. Current age: {age}");
                }

                return ValidationResult.Success;
            }

            return new ValidationResult("Invalid date format");
        }
    }
}
