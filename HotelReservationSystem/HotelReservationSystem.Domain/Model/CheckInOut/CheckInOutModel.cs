using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static HotelReservationSystem.Domain.Enums.ReservationEnum;

namespace HotelReservationSystem.Domain.Model.CheckInOut
{
    public class CheckInOutModel
    {
        [DisplayName("Check-In ID")]
        public int CheckInId { get; set; }

        [DisplayName("Reservation ID")]
        [Required(ErrorMessage = "Reservation ID is required")]
        public int ReservationId { get; set; }

        [DisplayName("Customer Name")]
        [Required(ErrorMessage = "Customer name is required")]
        public string CustomerName { get; set; }

        [DisplayName("Room Type")]
        [Required(ErrorMessage = "Room type is required")]
        public string RoomType { get; set; }

        [DisplayName("Room Number")]
        [Required(ErrorMessage = "Room number is required")]
        public string RoomNumber { get; set; }

        [DisplayName("Check-In Date")]
        [Required(ErrorMessage = "Check-in date is required")]
        public DateTime CheckInDate { get; set; }

        [DisplayName("Check-Out Date")]
        [Required(ErrorMessage = "Check-out date is required")]
        public DateTime CheckOutDate { get; set; }

        [DisplayName("Time Arrival")]
        [Required(ErrorMessage = "Time arrival is required")]
        public DateTime TimeArrival { get; set; }

        [DisplayName("Total Price")]
        [Required(ErrorMessage = "Total amount is required")]
        public decimal TotalPrice { get; set; }

        [DisplayName("Companion Cost")]
        public decimal TotalCompanionCost { get; set; }

        [DisplayName("Down Payment")]
        public decimal DownPayment { get; set; }

        [DisplayName("Amount Paid")]
        public decimal AmountPaid { get; set; }

        [DisplayName("Customer Email")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
        public string CustomerEmail { get; set; }

        [DisplayName("Payment Method")]
        public string PaymentMethod { get; set; }

        [DisplayName("Payment Reference")]
        public string PaymentReference { get; set; }

        [DisplayName("Payment Status")]
        public PaymentState PaymentStatus { get; set; }

        [DisplayName("Reservation Status")]
        [Required(ErrorMessage = "Status is required")]
        public string ReservationStatus { get; set; }

        [DisplayName("Checked In")]
        public bool IsCheckedIn { get; set; }

        [DisplayName("Checked Out")]
        public bool IsCheckedOut { get; set; }

        [DisplayName("Actual Check-In")]
        public DateTime? ActualCheckIn { get; set; }

        [DisplayName("Actual Check-Out")]
        public DateTime? ActualCheckOut { get; set; }

        [DisplayName("Checked In By")]
        public string CheckedInBy { get; set; }

        [DisplayName("Checked Out By")]
        public string CheckedOutBy { get; set; }

        [DisplayName("Date Created")]
        public DateTime CreatedAt { get; set; }

        // Calculated Properties - Using expression-bodied members
        [DisplayName("Grand Total")]
        public decimal GrandTotal => TotalPrice + TotalCompanionCost;

        [DisplayName("Balance Due")]
        public decimal BalanceDue => GrandTotal - AmountPaid;
    }
}