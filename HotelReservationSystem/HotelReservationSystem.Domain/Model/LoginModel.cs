using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Domain.Model
{
    public class LoginModel
    {
        [DisplayName("Username or Email")]
        [Required(ErrorMessage = "Username or Email is required")]
        [StringLength(100, ErrorMessage = "Username or Email cannot exceed 100 characters")]
        public string UsernameOrEmail { get; set; }

        [DisplayName("Password")]
        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, ErrorMessage = "Password cannot exceed 100 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Remember Me")]
        public bool RememberMe { get; set; }

        public enum UserRole
        {
            Admin,
            Staff
        }

        public enum Gender
        {
            Male,
            Female,
            Other
        }
    }
}
