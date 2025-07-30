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
        [DisplayName("Bill ID")]public int BillId { get; set; }
        [DisplayName("Reservation ID")][Required(ErrorMessage = "Reservation ID is required.")] public int ReservationId { get; set; }
        [DisplayName("Customer Name")][Required(ErrorMessage = "Customer name is required.")]public string CustomerName { get; set; }
        [DisplayName("Room Number")][Required(ErrorMessage = "Room number is required.")][StringLength(20, ErrorMessage = "Room number cannot exceed 20 characters.")]public string RoomNumber { get; set; }
        [DisplayName("Check-In Date")][Required(ErrorMessage = "Check-in date is required.")] public DateTime CheckInDate { get; set; }
        [DisplayName("Check-Out Date")][Required(ErrorMessage = "Check-out date is required.")] public DateTime CheckOutDate { get; set; }
        [DisplayName("Room Rate")][Range(0, double.MaxValue, ErrorMessage = "Room rate must be a positive value.")] public decimal RoomRate { get; set; }
        [DisplayName("Nights")][Range(1, int.MaxValue, ErrorMessage = "Nights must be at least 1.")]public int Nights { get; set; }
        [DisplayName("Subtotal")][Range(0, double.MaxValue, ErrorMessage = "Subtotal must be a positive value.")]public decimal Subtotal { get; set; }
        [DisplayName("Tax Amount")][Range(0, double.MaxValue, ErrorMessage = "Tax amount must be a positive value.")]public decimal TaxAmount { get; set; }
        [DisplayName("Discount Amount")][Range(0, double.MaxValue, ErrorMessage = "Discount amount must be a positive value.")]public decimal DiscountAmount { get; set; }
        [DisplayName("Total Amount")][Range(0, double.MaxValue, ErrorMessage = "Total amount must be a positive value.")] public decimal TotalAmount { get; set; }
        [DisplayName("Payment Method")][Required(ErrorMessage = "Payment method is required.")][StringLength(50, ErrorMessage = "Payment method cannot exceed 50 characters.")] public string PaymentMethod { get; set; }
        [DisplayName("Payment Status")][Required(ErrorMessage = "Payment status is required.")][StringLength(20, ErrorMessage = "Payment status cannot exceed 20 characters.")]public string PaymentStatus { get; set; }
        [DisplayName("Date Billed")][Required(ErrorMessage = "Date billed is required.")]public DateTime DateBilled { get; set; }
    }
}
