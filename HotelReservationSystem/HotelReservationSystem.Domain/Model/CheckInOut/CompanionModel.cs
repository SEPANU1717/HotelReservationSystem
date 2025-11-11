using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HotelReservationSystem.Domain.Model
{
    public class CompanionModel
    {
        [DisplayName("Companion ID")]
        public int CompanionId { get; set; }

        [DisplayName("Main Reservation ID")]
        [Required(ErrorMessage = "Main Reservation ID is required")]
        public int MainReservationId { get; set; }

        [DisplayName("Companion Name")]
        [Required(ErrorMessage = "Companion name is required")]
        public string CompanionName { get; set; }

        [DisplayName("Contact Number")]
        public string ContactNumber { get; set; }

        [DisplayName("Email Address")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [DisplayName("Relationship")]
        public string Relationship { get; set; }

        [DisplayName("Room Type")]
        [Required(ErrorMessage = "Room type is required")]
        public string RoomType { get; set; }

        [DisplayName("Room Number")]
        [Required(ErrorMessage = "Room number is required")]
        public string RoomNumber { get; set; }

        [DisplayName("Room Price Per Night")]
        public decimal RoomPrice { get; set; }

        [DisplayName("Number of Nights")]
        public int Nights { get; set; }

        [DisplayName("Check-In Date")]
        public DateTime CheckInDate { get; set; }

        [DisplayName("Check-Out Date")]
        public DateTime CheckOutDate { get; set; }

        [DisplayName("Total Cost")]
        public decimal TotalCost { get; set; }

        [DisplayName("Created At")]
        public DateTime CreatedAt { get; set; }

        [DisplayName("Created By")]
        public string CreatedBy { get; set; }
    }
}