using System;
using System.Windows.Forms;

namespace HotelReservationSystem.Domain.Interface.CheckInOut
{
    public interface ICheckInOutView
    {
        // Properties
        string ReservationId { get; set; }
        string CustomerName { get; set; }
        string RoomType { get; set; }
        string RoomNumber { get; set; }
        string RoomGuests { get; set; }
        string PaymentMethod { get; set; }
        string PaymentStatus { get; set; }
        string ReservationStatus { get; set; }
        string SearchValue { get; set; }
        string PaymentReference { get; set; }
        DateTime CheckInDate { get; set; }
        DateTime CheckOutDate { get; set; }
        DateTime TimeArrival { get; set; }
        decimal TotalPrice { get; set; }
        decimal DownPayment { get; set; }
        decimal AmountPaid { get; set; }
        decimal BalanceDue { get; set; }
        int CompanionCount { get; set; }


        // Status Properties
        bool isSuccessful { get; set; }
        bool isEdit { get; set; }
        string Message { get; set; }

        // Events
        event EventHandler SearchEvent;
        event EventHandler AddNewEvent;
        event EventHandler EditEvent;
        event EventHandler DeleteEvent;
        event EventHandler SaveEvent;
        event EventHandler CancelEvent;
        event EventHandler AddCompanionEvent;
        event EventHandler CompanionSaveEvent;
        event EventHandler CompanionCancelEvent;
        event EventHandler<string> RoomTypeChangedEvent;
        event EventHandler<string> RoomNumberChangedEvent;

        // Methods
        void SetReservationListBindingSource(BindingSource checkInList);
        void ShowMessage(string message, string title);
        void LoadRoomTypes(string[] roomTypes);
        void LoadAvailableRooms(string[] roomNumbers);
        void SetFieldEnabled(string fieldName, bool enabled);
        int GetSelectedReservationId();
        void ShowTab(int tabIndex); // NEW
        void ClearForm(); // NEW
    }
}