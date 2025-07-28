using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Model.Reservation
{
    public class ReservationModel
    {
            [DisplayName("Reservation ID")]
            public int ReservationId { get; set; }

            [DisplayName("Customer ID")]
            [Required(ErrorMessage = "Customer ID is required")]
            public int CustomerId { get; set; }

            [DisplayName("Room ID")]
            [Required(ErrorMessage = "Room ID is required")]
            public int RoomId { get; set; }

            [DisplayName("Check-In Date")]
            [Required(ErrorMessage = "Check-in date is required")]
            public DateTime CheckInDate { get; set; }

            [DisplayName("Check-Out Date")]
            [Required(ErrorMessage = "Check-out date is required")]
            public DateTime CheckOutDate { get; set; }

            [DisplayName("Total Amount")]
            [Required(ErrorMessage = "Total amount is required")]
            public decimal TotalAmount { get; set; }

            [DisplayName("Reservation Status")]
            [Required(ErrorMessage = "Status is required")]
            public string ReservationStatus { get; set; }
        }
    }

