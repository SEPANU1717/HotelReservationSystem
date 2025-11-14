using System;

namespace HotelReservationSystem.Domain.Model
{
    public class PasswordResetToken
    {
        public int TokenId { get; set; }
        public string UsernameOrEmail { get; set; }
        public string Token { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsUsed { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}