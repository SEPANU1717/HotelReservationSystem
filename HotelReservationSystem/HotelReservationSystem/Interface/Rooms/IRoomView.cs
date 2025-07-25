using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelReservationSystem.Interface.Rooms
{
    public interface IRoomView
    {
         string RoomId { get; set; }
         string RoomNumber { get; set; }
         string RoomType { get; set; }
         string RoomStatus { get; set; }
         string RoomPrice { get; set; }
         string BedCount { get; set; }
         string RoomGuests { get; set; }
         string RoomDescription { get; set; }
         string SearchValue { get; set; }
         bool isEdit { get; set; }
         bool isSuccessful { get; set; }
         string Message { get; set; }


        event EventHandler SearchEvent;
        event EventHandler AddNewEvent;
        event EventHandler EditEvent;
        event EventHandler DeleteEvent;
        event EventHandler SaveEvent;
        event EventHandler CancelEvent;
        event EventHandler UpdateEvent;

        void SetRoomListBindingSource(BindingSource roomList);
        void Show();
    }
}
