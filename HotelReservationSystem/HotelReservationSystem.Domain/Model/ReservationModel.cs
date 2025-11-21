using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using HotelReservationSystem.Domain.Validation;
using static HotelReservationSystem.Domain.Enums.ReservationEnum;

namespace HotelReservationSystem.Domain.Model
{
    public class ReservationModel
    {
        [DisplayName("Reservation ID")] 
        public int ReservationId { get; set; }
        
        [DisplayName("Customer Name")]
        [Required(ErrorMessage = "Customer name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Customer name must be between 2 and 100 characters")]
        public string CustomerName { get; set; }
        
        [DisplayName("Room Type")] 
        [Required(ErrorMessage = "Room type is required")]
        public string RoomType { get; set; }
        
        [DisplayName("Room Number")]
        [Required(ErrorMessage = "Room number is required")]
        public string RoomNumber { get; set; }
        
        [DisplayName("Check-In Date")]
        [Required(ErrorMessage = "Check-in date is required")]
        [DataType(DataType.Date)]
        [FutureDate(allowToday: true, ErrorMessage = "Check-in date cannot be in the past")]
        public DateTime CheckInDate { get; set; }
        
        [DisplayName("Check-Out Date")]
        [Required(ErrorMessage = "Check-out date is required")]
        [DataType(DataType.Date)]
        [DateRangeValidation("CheckInDate")]
        public DateTime CheckOutDate { get; set; }
        
        [DisplayName("Time Arrival")]
        [DataType(DataType.DateTime)]
        public DateTime TimeArrival { get; set; }
        
        [DisplayName("Total Amount")]
        [Required(ErrorMessage = "Total amount is required")]
        [DecimalRange(0.01, 1000000.00, ErrorMessage = "Total amount must be between $0.01 and $1,000,000.00")]
        public decimal TotalPrice { get; set; }
        
        [DisplayName("Down Payment")]
        [DecimalRange(0, 1000000.00, ErrorMessage = "Down payment cannot exceed $1,000,000.00")]
        public decimal DownPayment { get; set; }
        
        [DisplayName("Amount Paid")]
        [DecimalRange(0, 1000000.00, ErrorMessage = "Amount paid cannot exceed $1,000,000.00")]
        public decimal AmountPaid { get; set; }
        
        [DisplayName("Down Payment Paid")] 
        public bool IsDownPaymentPaid { get; set; }
        
        [DisplayName("Balance Due")] 
        public decimal BalanceDue => (TotalPrice - AmountPaid) < 0 ? 0 : (TotalPrice - AmountPaid);
        
        [DisplayName("Payment Method")]
        [StringLength(50, ErrorMessage = "Payment method cannot exceed 50 characters")]
        public string PaymentMethod { get; set; }
        
        [DisplayName("Payment Reference Number")]
        [StringLength(100, ErrorMessage = "Payment reference cannot exceed 100 characters")]
        public string PaymentReference { get; set; }
        
        [DisplayName("Payment Status")] 
        public PaymentState PaymentStatus { get; set; }
        
        [DisplayName("Reservation Status")]
        [Required(ErrorMessage = "Reservation status is required")]
        public string ReservationStatus { get; set; }
        
        [DisplayName("Down Payment Date")] 
        public DateTime? DownPaymentDate { get; set; }
        
        [DisplayName("Date Created")] 
        public DateTime CreatedAt { get; set; }
        
        [DisplayName("Number of Nights")]
        public int NumberOfNights => (CheckOutDate.Date - CheckInDate.Date).Days;
    }
}
