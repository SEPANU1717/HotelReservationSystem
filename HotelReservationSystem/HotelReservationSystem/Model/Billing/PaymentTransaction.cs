using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HotelReservationSystem.Model.Billing
{
    public class PaymentTransaction
    {
        [DisplayName("Transaction ID")]
        public int TransactionId { get; set; }

        [DisplayName("Bill ID")]
        [Required(ErrorMessage = "Bill ID is required.")]
        public int BillId { get; set; }

        [DisplayName("Payment Method")]
        [Required(ErrorMessage = "Payment method is required.")]
        [StringLength(50, ErrorMessage = "Payment method cannot exceed 50 characters.")]
        public string PaymentMethod { get; set; }

        [DisplayName("Amount")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0.")]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        [DisplayName("Transaction Date")]
        [Required(ErrorMessage = "Transaction date is required.")]
        public DateTime TransactionDate { get; set; }

        [DisplayName("Transaction Type")]
        [Required(ErrorMessage = "Transaction type is required.")]
        [StringLength(20, ErrorMessage = "Transaction type cannot exceed 20 characters.")]
        public string TransactionType { get; set; }

        [DisplayName("Transaction Status")]
        [Required(ErrorMessage = "Transaction status is required.")]
        [StringLength(20, ErrorMessage = "Transaction status cannot exceed 20 characters.")]
        public string TransactionStatus { get; set; }

        [DisplayName("Reference Number")]
        [StringLength(100, ErrorMessage = "Reference number cannot exceed 100 characters.")]
        public string ReferenceNumber { get; set; }

        [DisplayName("Authorization Code")]
        [StringLength(50, ErrorMessage = "Authorization code cannot exceed 50 characters.")]
        public string AuthorizationCode { get; set; }

        [DisplayName("Card Last Four")]
        [StringLength(4, ErrorMessage = "Card last four must be exactly 4 digits.")]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "Card last four must be 4 digits.")]
        public string CardLastFour { get; set; }

        [DisplayName("Card Type")]
        [StringLength(20, ErrorMessage = "Card type cannot exceed 20 characters.")]
        public string CardType { get; set; }

        [DisplayName("Gateway")]
        [StringLength(50, ErrorMessage = "Gateway cannot exceed 50 characters.")]
        public string Gateway { get; set; }

        [DisplayName("Gateway Transaction ID")]
        [StringLength(100, ErrorMessage = "Gateway transaction ID cannot exceed 100 characters.")]
        public string GatewayTransactionId { get; set; }

        [DisplayName("Processing Fee")]
        [Range(0, double.MaxValue, ErrorMessage = "Processing fee must be a positive value.")]
        [DataType(DataType.Currency)]
        public decimal ProcessingFee { get; set; }

        [DisplayName("Net Amount")]
        [DataType(DataType.Currency)]
        public decimal NetAmount { get; set; }

        [DisplayName("Notes")]
        [StringLength(500, ErrorMessage = "Notes cannot exceed 500 characters.")]
        public string Notes { get; set; }

        [DisplayName("Processed By")]
        [StringLength(100, ErrorMessage = "Processed by cannot exceed 100 characters.")]
        public string ProcessedBy { get; set; }

        [DisplayName("Created At")]
        public DateTime CreatedAt { get; set; }

        [DisplayName("Updated At")]
        public DateTime UpdatedAt { get; set; }

        // Calculated properties
        [DisplayName("Is Successful")]
        public bool IsSuccessful => TransactionStatus == "Completed" || TransactionStatus == "Approved";

        [DisplayName("Is Refund")]
        public bool IsRefund => TransactionType == "Refund";

        // Business logic methods
        public void CalculateNetAmount()
        {
            NetAmount = Amount - ProcessingFee;
            UpdatedAt = DateTime.Now;
        }

        public string GenerateReferenceNumber()
        {
            if (string.IsNullOrEmpty(ReferenceNumber))
            {
                ReferenceNumber = $"TXN-{TransactionDate.Year}{TransactionDate.Month:D2}{TransactionDate.Day:D2}-{TransactionId:D6}";
            }
            return ReferenceNumber;
        }

        public PaymentTransaction()
        {
            TransactionDate = DateTime.Now;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            TransactionStatus = "Pending";
            TransactionType = "Payment";
            ProcessingFee = 0;
        }
    }
}