using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static HotelReservationSystem.Domain.Enums.ReservationEnum;

namespace HotelReservationSystem.Domain.Model
{
    public class ReservationModel
    {
        [DisplayName("Reservation ID")] public int ReservationId { get; set; }
        [DisplayName("Customer Name")][Required(ErrorMessage = "Customer ID is required")] public string CustomerName { get; set; }
        [DisplayName("Room Type")] [Required(ErrorMessage = "Room type is required")] public string RoomType { get; set; }
        [DisplayName("Room Name")][Required(ErrorMessage = "Room name is required")] public string RoomNumber { get; set; }
        [DisplayName("Check-In Date")][Required(ErrorMessage = "Check-in date is required")] public DateTime CheckInDate { get; set; } 
        [DisplayName("Check-Out Date")][Required(ErrorMessage = "Check-out date is required")] public DateTime CheckOutDate { get; set; }
        [DisplayName("Check-Out Date")][Required(ErrorMessage = "Time arrival date is required")] public DateTime TimeArrival { get; set; }
        [DisplayName("Total Amount")][Required(ErrorMessage = "Total amount is required")] public decimal TotalPrice { get; set; }
        [DisplayName("Down Payment")] public decimal DownPayment { get; set; }
        [DisplayName("Amount Paid")] public decimal AmountPaid { get; set; }
        [DisplayName("Down Payment Paid")] public bool IsDownPaymentPaid { get; set; }
        [DisplayName("Balance Due")] public decimal BalanceDue => TotalPrice - AmountPaid;
        [DisplayName("Payment Method")] public string PaymentMethod { get; set; }
        [DisplayName("Payment Reference Number")] public string PaymentReference { get; set; }
        [DisplayName("Payment Status")] public PaymentState PaymentStatus { get; set; }
        [DisplayName("Reservation Status")][Required(ErrorMessage = "Status is required")] public string ReservationStatus { get; set; }
        [DisplayName("Down Payment Date")] public DateTime? DownPaymentDate { get; set; }
        [DisplayName("Date Created")] public DateTime CreatedAt { get; set; }

    }
}
