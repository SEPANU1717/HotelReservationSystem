using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Model.Service.Laundry
{
    public class ConfirmLaundryModel
    {
        public int LaundryId { get; set; }
        public string ServiceType { get; set; }
        public string CustomerName { get; set; }
        public string RoomNumber { get; set; }
        public int TotalItems { get; set; }
        public string Status { get; set; }
        public decimal ServiceFee { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
