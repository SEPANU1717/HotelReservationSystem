using System;
using System.ComponentModel;

namespace HotelReservationSystem.Domain.DTOs
{
    public class BillingDto
    {
        [DisplayName("Bill ID")]
        public int BillId { get; set; }

        [DisplayName("Reservation ID")]
        public int ReservationId { get; set; }

        [DisplayName("Customer")]
        public string CustomerName { get; set; }

        [DisplayName("Room")]
        public string RoomNumber { get; set; }

        [DisplayName("Room Type")]
        public string RoomType { get; set; }

        [DisplayName("Check-In")]
        public DateTime CheckInDate { get; set; }

        [DisplayName("Check-Out")]
        public DateTime CheckOutDate { get; set; }

        [DisplayName("Total Amount")]
        public decimal TotalAmount { get; set; }

        [DisplayName("Status")]
        public string PaymentStatus { get; set; }

        [DisplayName("Payment Method")]
        public string PaymentMethod { get; set; }

        [DisplayName("Date")]
        public DateTime DateBilled { get; set; }

        [DisplayName("Billed By")]
        public string BilledBy { get; set; }
    }
}
