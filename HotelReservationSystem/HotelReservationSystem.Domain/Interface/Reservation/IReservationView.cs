using System;
using System.Windows.Forms;

namespace HotelReservationSystem.Domain.Interface.Reservation
{
    public interface IReservationView
    {
        // Reservation details
        string ReservationId { get; set; }
        string CustomerName { get; set; }
        string RoomNumber { get; set; }
        string RoomType { get; set; }
        string Guests { get; set; }
        DateTime CheckInDate { get; set; }
        DateTime CheckOutDate { get; set; }
        string TotalPrice { get; set; }
        string ReservationStatus { get; set; }

        // Search or filter
        string SearchValue { get; set; }

        // Flags
        bool isEdit { get; set; }
        bool isSuccessful { get; set; }
        string Message { get; set; }

        // Events for CRUD
        event EventHandler SearchEvent;
        event EventHandler AddNewEvent;
        event EventHandler EditEvent;
        event EventHandler DeleteEvent;
        event EventHandler SaveEvent;
        event EventHandler CancelEvent;

        // Load data to DataGridView
        void SetReservationListBindingSource(BindingSource reservationList);
        void Show();

    }
}
