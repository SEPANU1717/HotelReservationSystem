using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HotelReservationSystem.Model.Billing
{
    public class BillingModel
    {
        [DisplayName("Bill ID")]
        public int BillId { get; set; }

        [DisplayName("Reservation ID")]
        [Required(ErrorMessage = "Reservation ID is required")]
        public int ReservationId { get; set; }

        [DisplayName("Customer Name")]
        [Required(ErrorMessage = "Customer name is required")]
        public string CustomerName { get; set; }

        [DisplayName("Room Number")]
        [Required(ErrorMessage = "Room number is required")]
        public string RoomNumber { get; set; }

        [DisplayName("Room Type")]
        public string RoomType { get; set; }

        [DisplayName("Check-In Date")]
        [Required(ErrorMessage = "Check-in date is required")]
        public DateTime CheckInDate { get; set; }

        [DisplayName("Check-Out Date")]
        [Required(ErrorMessage = "Check-out date is required")]
        public DateTime CheckOutDate { get; set; }

        [DisplayName("Number of Nights")]
        public int NumberOfNights { get; set; }

        [DisplayName("Room Rate")]
        [Required(ErrorMessage = "Room rate is required")]
        public decimal RoomRate { get; set; }

        [DisplayName("Room Total")]
        public decimal RoomTotal { get; set; }

        [DisplayName("Service Charges")]
        public decimal ServiceCharges { get; set; }

        [DisplayName("Tax Amount")]
        public decimal TaxAmount { get; set; }

        [DisplayName("Discount Amount")]
        public decimal DiscountAmount { get; set; }

        [DisplayName("Additional Charges")]
        public decimal AdditionalCharges { get; set; }

        [DisplayName("Additional Charges Description")]
        public string AdditionalChargesDescription { get; set; }

        [DisplayName("Subtotal")]
        public decimal Subtotal { get; set; }

        [DisplayName("Total Amount")]
        [Required(ErrorMessage = "Total amount is required")]
        public decimal TotalAmount { get; set; }

        [DisplayName("Amount Paid")]
        public decimal AmountPaid { get; set; }

        [DisplayName("Balance Due")]
        public decimal BalanceDue { get; set; }

        [DisplayName("Payment Method")]
        public string PaymentMethod { get; set; }

        [DisplayName("Payment Status")]
        [Required(ErrorMessage = "Payment status is required")]
        public string PaymentStatus { get; set; }

        [DisplayName("Bill Date")]
        public DateTime BillDate { get; set; }

        [DisplayName("Due Date")]
        public DateTime DueDate { get; set; }

        [DisplayName("Notes")]
        public string Notes { get; set; }

        [DisplayName("Created At")]
        public DateTime CreatedAt { get; set; }

        [DisplayName("Updated At")]
        public DateTime UpdatedAt { get; set; }

        // Constructor
        public BillingModel()
        {
            BillDate = DateTime.Now;
            DueDate = DateTime.Now.AddDays(30);
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            PaymentStatus = "Pending";
            ServiceCharges = 0;
            TaxAmount = 0;
            DiscountAmount = 0;
            AdditionalCharges = 0;
            AmountPaid = 0;
        }

        // Calculate totals method
        public void CalculateTotals()
        {
            RoomTotal = RoomRate * NumberOfNights;
            Subtotal = RoomTotal + ServiceCharges + AdditionalCharges - DiscountAmount;
            TotalAmount = Subtotal + TaxAmount;
            BalanceDue = TotalAmount - AmountPaid;
        }
    }
}