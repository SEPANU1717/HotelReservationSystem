using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Model.Billing
{
    public class BillingModel
    {
        [DisplayName("Bill ID")]
        public int BillId { get; set; }

        [DisplayName("Reservation ID")]
        [Required(ErrorMessage = "Reservation ID is required.")]
        public int ReservationId { get; set; }

        [DisplayName("Customer ID")]
        [Required(ErrorMessage = "Customer ID is required.")]
        public int CustomerId { get; set; }

        [DisplayName("Customer Name")]
        [Required(ErrorMessage = "Customer name is required.")]
        [StringLength(100, ErrorMessage = "Customer name cannot exceed 100 characters.")]
        public string CustomerName { get; set; }

        [DisplayName("Room Number")]
        [Required(ErrorMessage = "Room number is required.")]
        [StringLength(20, ErrorMessage = "Room number cannot exceed 20 characters.")]
        public string RoomNumber { get; set; }

        [DisplayName("Check-In Date")]
        [Required(ErrorMessage = "Check-in date is required.")]
        public DateTime CheckInDate { get; set; }

        [DisplayName("Check-Out Date")]
        [Required(ErrorMessage = "Check-out date is required.")]
        public DateTime CheckOutDate { get; set; }

        [DisplayName("Room Rate")]
        [Range(0, double.MaxValue, ErrorMessage = "Room rate must be a positive value.")]
        [DataType(DataType.Currency)]
        public decimal RoomRate { get; set; }

        [DisplayName("Nights")]
        [Range(1, int.MaxValue, ErrorMessage = "Nights must be at least 1.")]
        public int Nights { get; set; }

        [DisplayName("Room Subtotal")]
        [Range(0, double.MaxValue, ErrorMessage = "Room subtotal must be a positive value.")]
        [DataType(DataType.Currency)]
        public decimal RoomSubtotal { get; set; }

        [DisplayName("Additional Services Total")]
        [Range(0, double.MaxValue, ErrorMessage = "Additional services total must be a positive value.")]
        [DataType(DataType.Currency)]
        public decimal AdditionalServicesTotal { get; set; }

        [DisplayName("Subtotal")]
        [Range(0, double.MaxValue, ErrorMessage = "Subtotal must be a positive value.")]
        [DataType(DataType.Currency)]
        public decimal Subtotal { get; set; }

        [DisplayName("Tax Rate")]
        [Range(0, 1, ErrorMessage = "Tax rate must be between 0 and 1.")]
        public decimal TaxRate { get; set; }

        [DisplayName("Tax Amount")]
        [Range(0, double.MaxValue, ErrorMessage = "Tax amount must be a positive value.")]
        [DataType(DataType.Currency)]
        public decimal TaxAmount { get; set; }

        [DisplayName("Discount Type")]
        [StringLength(20, ErrorMessage = "Discount type cannot exceed 20 characters.")]
        public string DiscountType { get; set; }

        [DisplayName("Discount Amount")]
        [Range(0, double.MaxValue, ErrorMessage = "Discount amount must be a positive value.")]
        [DataType(DataType.Currency)]
        public decimal DiscountAmount { get; set; }

        [DisplayName("Total Amount")]
        [Range(0, double.MaxValue, ErrorMessage = "Total amount must be a positive value.")]
        [DataType(DataType.Currency)]
        public decimal TotalAmount { get; set; }

        [DisplayName("Amount Paid")]
        [Range(0, double.MaxValue, ErrorMessage = "Amount paid must be a positive value.")]
        [DataType(DataType.Currency)]
        public decimal AmountPaid { get; set; }

        [DisplayName("Balance Due")]
        [DataType(DataType.Currency)]
        public decimal BalanceDue { get; set; }

        [DisplayName("Payment Method")]
        [Required(ErrorMessage = "Payment method is required.")]
        [StringLength(50, ErrorMessage = "Payment method cannot exceed 50 characters.")]
        public string PaymentMethod { get; set; }

        [DisplayName("Payment Status")]
        [Required(ErrorMessage = "Payment status is required.")]
        [StringLength(20, ErrorMessage = "Payment status cannot exceed 20 characters.")]
        public string PaymentStatus { get; set; }

        [DisplayName("Date Billed")]
        [Required(ErrorMessage = "Date billed is required.")]
        public DateTime DateBilled { get; set; }

        [DisplayName("Due Date")]
        public DateTime? DueDate { get; set; }

        [DisplayName("Date Paid")]
        public DateTime? DatePaid { get; set; }

        [DisplayName("Invoice Number")]
        [StringLength(50, ErrorMessage = "Invoice number cannot exceed 50 characters.")]
        public string InvoiceNumber { get; set; }

        [DisplayName("Notes")]
        [StringLength(500, ErrorMessage = "Notes cannot exceed 500 characters.")]
        public string Notes { get; set; }

        [DisplayName("Created At")]
        public DateTime CreatedAt { get; set; }

        [DisplayName("Updated At")]
        public DateTime UpdatedAt { get; set; }

        [DisplayName("Created By")]
        [StringLength(100, ErrorMessage = "Created by cannot exceed 100 characters.")]
        public string CreatedBy { get; set; }

        // Navigation properties
        public List<BillingLineItem> LineItems { get; set; } = new List<BillingLineItem>();
        public List<PaymentTransaction> PaymentTransactions { get; set; } = new List<PaymentTransaction>();

        // Calculated properties
        [DisplayName("Is Fully Paid")]
        public bool IsFullyPaid => BalanceDue <= 0;

        [DisplayName("Is Overdue")]
        public bool IsOverdue => DueDate.HasValue && DateTime.Now > DueDate.Value && BalanceDue > 0;

        // Business logic methods
        public void CalculateTotals()
        {
            RoomSubtotal = RoomRate * Nights;
            AdditionalServicesTotal = LineItems?.Where(x => x.ItemType != "Room").Sum(x => x.TotalAmount) ?? 0;
            Subtotal = RoomSubtotal + AdditionalServicesTotal;
            TaxAmount = Subtotal * TaxRate;
            TotalAmount = Subtotal + TaxAmount - DiscountAmount;
            BalanceDue = TotalAmount - AmountPaid;
            UpdatedAt = DateTime.Now;
        }

        public void AddLineItem(BillingLineItem lineItem)
        {
            if (LineItems == null)
                LineItems = new List<BillingLineItem>();
            
            LineItems.Add(lineItem);
            CalculateTotals();
        }

        public void AddPayment(PaymentTransaction payment)
        {
            if (PaymentTransactions == null)
                PaymentTransactions = new List<PaymentTransaction>();
            
            PaymentTransactions.Add(payment);
            AmountPaid = PaymentTransactions.Sum(x => x.Amount);
            
            if (IsFullyPaid)
            {
                PaymentStatus = "Paid";
                DatePaid = DateTime.Now;
            }
            else if (AmountPaid > 0)
            {
                PaymentStatus = "Partial";
            }
            
            CalculateTotals();
        }

        public string GenerateInvoiceNumber()
        {
            if (string.IsNullOrEmpty(InvoiceNumber))
            {
                InvoiceNumber = $"INV-{DateBilled.Year}-{BillId:D6}";
            }
            return InvoiceNumber;
        }
    }
}
