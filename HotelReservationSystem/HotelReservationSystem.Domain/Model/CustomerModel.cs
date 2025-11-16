using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using HotelReservationSystem.Domain.Validation;

namespace HotelReservationSystem.Domain.Model
{
    public class CustomerModel
    {
        [DisplayName("Customer ID")]
        public int CustomerID { get; set; }

        [DisplayName("First Name")]
        [Required(ErrorMessage = "First name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First name must be between 2 and 50 characters")]
        [RegularExpression(@"^[a-zA-Z\s\-']+$", ErrorMessage = "First name can only contain letters, spaces, hyphens, and apostrophes")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Last name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last name must be between 2 and 50 characters")]
        [RegularExpression(@"^[a-zA-Z\s\-']+$", ErrorMessage = "Last name can only contain letters, spaces, hyphens, and apostrophes")]
        public string LastName { get; set; }

        [DisplayName("Middle Name")]
        [StringLength(50, ErrorMessage = "Middle name cannot exceed 50 characters")]
        [RegularExpression(@"^[a-zA-Z\s\-']*$", ErrorMessage = "Middle name can only contain letters, spaces, hyphens, and apostrophes")]
        public string MiddleName { get; set; }

        [DisplayName("ID Type")]
        [Required(ErrorMessage = "ID Type is required")]
        public string IDType { get; set; }

        [DisplayName("Contact")]
        [Required(ErrorMessage = "Contact number is required")]
        [Phone(ErrorMessage = "Invalid phone number format")]
        [RegularExpression(@"^[\d\s\-\+\(\)]+$", ErrorMessage = "Contact can only contain numbers, spaces, hyphens, plus sign, and parentheses")]
        public string Contact { get; set; }

        [DisplayName("Address")]
        [Required(ErrorMessage = "Address is required")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "Address must be between 5 and 200 characters")]
        public string Address { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address format")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
        public string Email { get; set; }

        [DisplayName("Date of Birth")]
        [Required(ErrorMessage = "Date of birth is required")]
        [DataType(DataType.Date)]
        [MinimumAge(18, ErrorMessage = "Customer must be at least 18 years old to make a reservation")]
        public DateTime? DateOfBirth { get; set; }

        [DisplayName("Gender")]
        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }

        [DisplayName("Nationality")]
        [Required(ErrorMessage = "Nationality is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Nationality must be between 2 and 50 characters")]
        public string Nationality { get; set; }

        [DisplayName("Notes")]
        [StringLength(500, ErrorMessage = "Notes cannot exceed 500 characters")]
        public string Notes { get; set; }

        [DisplayName("Full Name")]
        public string FullName => $"{FirstName} {(!string.IsNullOrEmpty(MiddleName) ? MiddleName + " " : "")}{LastName}";
        
        [DisplayName("Age")]
        public int Age => DateOfBirth.HasValue 
            ? DateTime.Now.Year - DateOfBirth.Value.Year - (DateTime.Now.DayOfYear < DateOfBirth.Value.DayOfYear ? 1 : 0) 
            : 0;
    }
}