using System;
using System.ComponentModel;

namespace HotelReservationSystem.Domain.DTOs
{
    /// <summary>
    /// Data Transfer Object for Billing - Simplified view for displaying billing records
    /// Only includes essential fields for grid display and forms
    /// </summary>
    public class BillingDto
    {
        [DisplayName("Bill ID")]
        public int BillId { get; set; }

        [DisplayName("Reservation ID")]
        public int ReservationId { get; set; }

        [DisplayName("Customer Name")]
        public string CustomerName { get; set; }

        [DisplayName("Room")]
        public string RoomNumber { get; set; }

        [DisplayName("Type")]
        public string RoomType { get; set; }

        [DisplayName("Check-In")]
        public DateTime CheckInDate { get; set; }

        [DisplayName("Check-Out")]
        public DateTime CheckOutDate { get; set; }

        [DisplayName("Actual Check-Out")]
        public DateTime? ActualCheckOutDate { get; set; }

        [DisplayName("Room Charge")]
        public string RoomCharge { get; set; }

        [DisplayName("Late Fee")]
        public string LateCheckoutFee { get; set; }

        [DisplayName("Damage Fee")]
        public string DamageFee { get; set; }

        [DisplayName("Total Amount")]
        public string TotalAmount { get; set; }

        [DisplayName("Paid Before")]
        public string AmountPaidBefore { get; set; }

        [DisplayName("Paid at Checkout")]
        public string AmountPaidAtCheckout { get; set; }

        [DisplayName("Balance Due")]
        public string BalanceDue { get; set; }

        [DisplayName("Payment Status")]
        public string PaymentStatus { get; set; }

        [DisplayName("Payment Method")]
        public string PaymentMethod { get; set; }

        [DisplayName("Date Billed")]
        public DateTime DateBilled { get; set; }

        [DisplayName("Billed By")]
        public string BilledBy { get; set; }

        // Calculated display fields
        [DisplayName("Nights")]
        public int NumberOfNights { get; set; }

        [DisplayName("Late Checkout")]
        public bool IsLateCheckout { get; set; }

        // Display-friendly formatted strings
        public string CheckInDisplay => CheckInDate.ToString("MM/dd/yyyy");
        public string CheckOutDisplay => CheckOutDate.ToString("MM/dd/yyyy");
        public string ActualCheckOutDisplay => ActualCheckOutDate?.ToString("MM/dd/yyyy hh:mm tt") ?? "N/A";
        public string DateBilledDisplay => DateBilled.ToString("MM/dd/yyyy hh:mm tt");
    }
}
