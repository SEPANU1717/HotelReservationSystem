using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Domain.DTOs
{
    public class ReservationDto
    {
        [DisplayName("Reservation ID")]
        public int ReservationId { get; set; }

        [DisplayName("Customer Name")]
        public string CustomerName { get; set; }

        [DisplayName("Room Name")]
        public string RoomNumber { get; set; }

        [DisplayName("Check-In Date")]
        public DateTime CheckInDate { get; set; }

        [DisplayName("Check-Out Date")]
        public DateTime CheckOutDate { get; set; }

        [DisplayName("Total Amount")]
        public decimal TotalPrice { get; set; }

        [DisplayName("Amount Paid")]
        public decimal AmountPaid { get; set; }

        [DisplayName("Balance Due")]
        public decimal BalanceDue { get; set; }

        [DisplayName("Payment Status")]
        public string PaymentStatus { get; set; }

        [DisplayName("Reservation Status")]
        public string ReservationStatus { get; set; }
    }
}
