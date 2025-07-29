using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HotelReservationSystem.Model.Billing
{
    public class BillingLineItem
    {
        [DisplayName("Line Item ID")]
        public int LineItemId { get; set; }

        [DisplayName("Bill ID")]
        [Required(ErrorMessage = "Bill ID is required.")]
        public int BillId { get; set; }

        [DisplayName("Item Type")]
        [Required(ErrorMessage = "Item type is required.")]
        [StringLength(50, ErrorMessage = "Item type cannot exceed 50 characters.")]
        public string ItemType { get; set; }

        [DisplayName("Item Description")]
        [Required(ErrorMessage = "Item description is required.")]
        [StringLength(200, ErrorMessage = "Item description cannot exceed 200 characters.")]
        public string ItemDescription { get; set; }

        [DisplayName("Quantity")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Quantity must be greater than 0.")]
        public decimal Quantity { get; set; }

        [DisplayName("Unit Price")]
        [Range(0, double.MaxValue, ErrorMessage = "Unit price must be a positive value.")]
        [DataType(DataType.Currency)]
        public decimal UnitPrice { get; set; }

        [DisplayName("Total Amount")]
        [Range(0, double.MaxValue, ErrorMessage = "Total amount must be a positive value.")]
        [DataType(DataType.Currency)]
        public decimal TotalAmount { get; set; }

        [DisplayName("Date Added")]
        public DateTime DateAdded { get; set; }

        [DisplayName("Service Date")]
        public DateTime? ServiceDate { get; set; }

        [DisplayName("Notes")]
        [StringLength(300, ErrorMessage = "Notes cannot exceed 300 characters.")]
        public string Notes { get; set; }

        [DisplayName("Taxable")]
        public bool IsTaxable { get; set; }

        [DisplayName("Category")]
        [StringLength(30, ErrorMessage = "Category cannot exceed 30 characters.")]
        public string Category { get; set; }

        // Calculated property
        public decimal CalculatedTotal => Quantity * UnitPrice;

        // Business logic methods
        public void CalculateTotal()
        {
            TotalAmount = Quantity * UnitPrice;
        }

        public BillingLineItem()
        {
            DateAdded = DateTime.Now;
            Quantity = 1;
            IsTaxable = true;
        }
    }
}