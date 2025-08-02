using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Model
{
    public class BillingModel
    {
        [Display(Name = "Bill ID")] public int BillId { get; set; }
        [Required(ErrorMessage = "Reservation ID is required.")][Display(Name = "Reservation ID")] public int ReservationId { get; set; }
        [Required(ErrorMessage = "Customer Name is required.")][Display(Name = "Customer Name")] public string CustomerName { get; set; }
        [Required(ErrorMessage = "Room Type is required.")][Display(Name = "Room Type")] public string RoomType { get; set; }
        [Required(ErrorMessage = "Room Number is required.")][Display(Name = "Room Number")] public string RoomNumber { get; set; }
        [Required(ErrorMessage = "Total Amount is required.")]
        [Range(1, double.MaxValue, ErrorMessage = "Total Amount must be greater than 0.")]
        [Display(Name = "Total Amount")] public decimal TotalAmount { get; set; }
        [Required(ErrorMessage = "Payment Status is required.")][Display(Name = "Payment Status")] public string PaymentStatus { get; set; }
        [DisplayName("Date Billed")] public DateTime DateBilled { get; set; } = DateTime.Now;
    }
}