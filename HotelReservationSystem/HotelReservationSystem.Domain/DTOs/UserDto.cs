using System;

namespace HotelReservationSystem.Domain.DTOs
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string Role { get; set; }
        public int Age { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        
    }
}
