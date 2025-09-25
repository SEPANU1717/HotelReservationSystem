using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Domain.DTOs
{
    public class DashboardDto
    {
        public int ReservationId { get; set; }
        public string CustomerName { get; set; }
        public string RoomType { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
