using System;

namespace HotelReservationSystem.Domain.DTOs
{
    public class CompanionDto
    {
        public int CompanionId { get; set; }
        public int MainReservationId { get; set; }
        public string CompanionName { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string Relationship { get; set; }
        public string RoomType { get; set; }
        public string RoomNumber { get; set; }
        public decimal RoomPrice { get; set; }
        public int Nights { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalCost { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
    }
}