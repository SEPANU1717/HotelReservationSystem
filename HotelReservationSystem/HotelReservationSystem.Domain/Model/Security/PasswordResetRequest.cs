using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HotelReservationSystem.Domain.Model
{
    public class PasswordResetRequest
    {
        [DisplayName("Username or Email")]
        [Required(ErrorMessage = "Username or Email is required")]
        [StringLength(100, ErrorMessage = "Username or Email cannot exceed 100 characters")]
        public string UsernameOrEmail { get; set; }

        [DisplayName("Reset Code")]
        [Required(ErrorMessage = "Reset code is required")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "Reset code must be 6 digits")]
        public string Token { get; set; }

        [DisplayName("New Password")]
        [Required(ErrorMessage = "New password is required")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DisplayName("Confirm Password")]
        [Required(ErrorMessage = "Please confirm your password")]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}