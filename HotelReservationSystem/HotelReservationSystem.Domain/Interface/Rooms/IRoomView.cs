using System;
using System.Windows.Forms;

namespace HotelReservationSystem.Domain.Interface.Rooms
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

        string StatusFilter { get; set; }
        event EventHandler FilterEvent;
        event EventHandler SearchEvent;
        event EventHandler AddNewEvent;
        event EventHandler EditEvent;
        event EventHandler DeleteEvent;
        event EventHandler SaveEvent;
        event EventHandler CancelEvent;

        void SetRoomListBindingSource(BindingSource roomList);
        void Show();
    }
}
