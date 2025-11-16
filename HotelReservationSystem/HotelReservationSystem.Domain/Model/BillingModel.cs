using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using HotelReservationSystem.Domain.Validation;

namespace HotelReservationSystem.Domain.Model
{
    public class BillingModel
    {
        [Display(Name = "Bill ID")] 
        public int BillId { get; set; }
        
        [Required(ErrorMessage = "Reservation ID is required")]
        [Display(Name = "Reservation ID")] 
        public int ReservationId { get; set; }
        
        [Required(ErrorMessage = "Customer name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Customer name must be between 2 and 100 characters")]
        [Display(Name = "Customer Name")] 
        public string CustomerName { get; set; }
        
        [Required(ErrorMessage = "Room type is required")]
        [Display(Name = "Room Type")] 
        public string RoomType { get; set; }
        
        [Required(ErrorMessage = "Room number is required")]
        [Display(Name = "Room Number")] 
        public string RoomNumber { get; set; }
        
        [Display(Name = "Check-In Date")]
        public DateTime CheckInDate { get; set; }
        
        [Display(Name = "Check-Out Date")]
        public DateTime CheckOutDate { get; set; }
        
        [Display(Name = "Actual Check-Out Date")]
        public DateTime? ActualCheckOutDate { get; set; }
        
        // Room Charges
        [Required(ErrorMessage = "Room charge is required")]
        [DecimalRange(0, 1000000, ErrorMessage = "Room charge must be between $0 and $1,000,000")]
        [Display(Name = "Room Charge")]
        public decimal RoomCharge { get; set; }
        
        [DecimalRange(0, 100000, ErrorMessage = "Late checkout fee must be between $0 and $100,000")]
        [Display(Name = "Late Checkout Fee")]
        public decimal LateCheckoutFee { get; set; }
        
        [DecimalRange(0, 100000, ErrorMessage = "Damage fee must be between $0 and $100,000")]
        [Display(Name = "Damage Fee")]
        public decimal DamageFee { get; set; }
       
       
        
        // Calculated Fields
        [Display(Name = "Subtotal")]
        public decimal Subtotal => RoomCharge + LateCheckoutFee + DamageFee;
        
        [Display(Name = "Total Amount")]
        public decimal TotalAmount => Subtotal;
        
        // Payment Information
        [Display(Name = "Amount Paid Before")]
        public decimal AmountPaidBefore { get; set; }
        
        [Display(Name = "Amount Paid at Checkout")]
        public decimal AmountPaidAtCheckout { get; set; }
        
        [Display(Name = "Total Paid")]
        public decimal TotalPaid => AmountPaidBefore + AmountPaidAtCheckout;
        
        [Display(Name = "Balance Due")]
        public decimal BalanceDue => TotalAmount - TotalPaid;
        
        [Required(ErrorMessage = "Payment status is required")]
        [Display(Name = "Payment Status")] 
        public string PaymentStatus { get; set; }
        
        [StringLength(50, ErrorMessage = "Payment method cannot exceed 50 characters")]
        [Display(Name = "Payment Method")]
        public string PaymentMethod { get; set; }
        
        [StringLength(100, ErrorMessage = "Payment reference cannot exceed 100 characters")]
        [Display(Name = "Payment Reference")]
        public string PaymentReference { get; set; }
        
        // Billing Information
        [DisplayName("Date Billed")] 
        public DateTime DateBilled { get; set; } = DateTime.Now;
        
        [DisplayName("Billed By")]
        [StringLength(100, ErrorMessage = "Billed by cannot exceed 100 characters")]
        public string BilledBy { get; set; }
        

        
        [Display(Name = "Number of Nights")]
        public int NumberOfNights => ActualCheckOutDate.HasValue 
            ? (ActualCheckOutDate.Value.Date - CheckInDate.Date).Days 
            : (CheckOutDate.Date - CheckInDate.Date).Days;
            
        [Display(Name = "Is Late Checkout")]
        public bool IsLateCheckout => ActualCheckOutDate.HasValue && ActualCheckOutDate.Value > CheckOutDate;
    }
}