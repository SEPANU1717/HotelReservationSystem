using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HotelReservationSystem.Domain.Model
{
    public class UserModel
    {

        [DisplayName("User ID")]
        public int UserId { get; set; }

        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Last name is required")]
        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters")]
        public string LastName { get; set; }

        [DisplayName("First Name")]
        [Required(ErrorMessage = "First name is required")]
        [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters")]
        public string FirstName { get; set; }

        [DisplayName("Middle Name")]
        [StringLength(50, ErrorMessage = "Middle name cannot exceed 50 characters")]
        public string MiddleName { get; set; }

        [DisplayName("Birth Date")]
        [Required(ErrorMessage = "Birth date is required")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [DisplayName("Username")]
        [Required(ErrorMessage = "Username is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters")]
        public string Username { get; set; }

        [DisplayName("Password")]
        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
        public string Email { get; set; }

        [DisplayName("Gender")]
        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }

        [DisplayName("Role")]
        [Required(ErrorMessage = "Role is required")]
        public string Role { get; set; }

        [DisplayName("Date Created")]
        public DateTime CreatedAt { get; set; }

        [DisplayName("Is Active")]
        public bool IsActive { get; set; } = true;

        public string PasswordHash { get; set; }
        [DisplayName("Full Name")]
        public string FullName => $"{FirstName} {(!string.IsNullOrEmpty(MiddleName) ? MiddleName + " " : "")}{LastName}";

        [DisplayName("Age")]
        public int Age => DateTime.Now.Year - BirthDate.Year - (DateTime.Now.DayOfYear < BirthDate.DayOfYear ? 1 : 0);
    }
}
