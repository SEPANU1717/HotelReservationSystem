using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Model.Rooms
{
    public class RoomModel
    {
        [DisplayName("Room ID")]
        public int RoomId { get; set; }
        [DisplayName("Room Number")]
        [Required(ErrorMessage = "Room number is required")]
        public string RoomNumber { get; set; }
        [DisplayName("Room Type")]
        [Required(ErrorMessage = "Room type is required")]
        public string RoomType { get; set; }
        [DisplayName("Room Status")]
        [Required(ErrorMessage = "Room status is required")]
        public string RoomStatus { get; set; }
        [DisplayName("Room Price")]
        [Required(ErrorMessage = "Room price is required")]
        public string RoomPrice { get; set; }
        [DisplayName("Bed Count")]
        [Required(ErrorMessage = "Bed count is required")]
        public string BedCount { get; set; }
        [DisplayName("Guests")]
        [Required(ErrorMessage = "guests count is required")]
        public string RoomGuests{ get; set; }
        [DisplayName("Description")]
        public string RoomDescription { get; set; }
    }
}
