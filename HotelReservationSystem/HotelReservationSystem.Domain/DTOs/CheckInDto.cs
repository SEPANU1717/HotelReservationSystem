using System;
using System.ComponentModel;

namespace HotelReservationSystem.Domain.DTOs
{
    public class CheckInDto
    {
        [DisplayName("Check-In ID")]
        public int CheckInId { get; set; }

        [DisplayName("Reservation ID")]
        public int ReservationId { get; set; }

        [DisplayName("Customer Name")]
        public string CustomerName { get; set; }

        [DisplayName("Room Number")]
        public string RoomNumber { get; set; }

        [DisplayName("Room Type")]
        public string RoomType { get; set; }

        [DisplayName("Check-In Date")]
        public DateTime CheckInDate { get; set; }

        [DisplayName("Check-Out Date")]
        public DateTime CheckOutDate { get; set; }

        [DisplayName("Grand Total")]
        public decimal GrandTotal { get; set; }

        [DisplayName("Balance Due")]
        public decimal BalanceDue { get; set; }

        [DisplayName("Payment Status")]
        public string PaymentStatus { get; set; }

        [DisplayName("Status")]
        public string ReservationStatus { get; set; }

        [DisplayName("Checked In")]
        public bool IsCheckedIn { get; set; }

        [DisplayName("Checked Out")]
        public bool IsCheckedOut { get; set; }
    }
}