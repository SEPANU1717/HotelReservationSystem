using System;
using System.ComponentModel.DataAnnotations;

namespace HotelReservationSystem.Domain.Validation
{

    public class DateRangeValidationAttribute : ValidationAttribute
    {
        private readonly string _startDatePropertyName;

        public DateRangeValidationAttribute(string startDatePropertyName)
        {
            _startDatePropertyName = startDatePropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var endDate = value as DateTime?;
            
            if (!endDate.HasValue)
            {
                return new ValidationResult("Check-out date is required");
            }

            var startDateProperty = validationContext.ObjectType.GetProperty(_startDatePropertyName);
            
            if (startDateProperty == null)
            {
                return new ValidationResult($"Property {_startDatePropertyName} not found");
            }

            var startDateValue = startDateProperty.GetValue(validationContext.ObjectInstance);
            
            if (startDateValue is DateTime startDate)
            {
                if (endDate.Value.Date <= startDate.Date)
                {
                    return new ValidationResult("Check-out date must be after check-in date");
                }

                var daysDifference = (endDate.Value.Date - startDate.Date).TotalDays;
                if (daysDifference > 365)
                {
                    return new ValidationResult("Reservation period cannot exceed 1 year");
                }

                return ValidationResult.Success;
            }

            return new ValidationResult("Invalid date format");
        }
    }
}
