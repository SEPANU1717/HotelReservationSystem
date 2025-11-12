using System;
using System.Collections.Generic;
using System.Windows.Forms;
using HotelReservationSystem.Domain.Model;
using static HotelReservationSystem.Domain.Enums.ReservationEnum;

namespace HotelReservationSystem.Domain.Interface.Reservation
{
    public interface IReservationView
    {
        // Properties
        string ReservationId { get; set; }
        string CustomerName { get; set; }
        string RoomNumber { get; set; }
        string RoomType { get; set; }
        string Guests { get; set; }
        DateTime CheckInDate { get; set; }
        DateTime CheckOutDate { get; set; }
        string TotalPrice { get; set; }
        string DownPayment { get; set; }
        string BalanceDue { get; set; }
        string AmountPaid { get; set; }
        string PaymentMethod { get; set; }
        PaymentState PaymentStatus { get; set; }
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
        event EventHandler ShowCheckInOutView;

        // New events for presenter communication
        event EventHandler<int> LoadReservationForEditEvent;
        event EventHandler<string> SetCustomerForReservationEvent;
        event EventHandler<string> RoomTypeChangedEvent;
        event EventHandler<string> PaymentTypeChangedEvent; // ADD THIS NEW EVENT


        // Methods
        void SetReservationListBindingSource(BindingSource reservationList);
        void Show();

        // New methods for view control
        void LoadAvailableRooms(IEnumerable<RoomModel> rooms);
        void PopulateEditForm(ReservationModel reservation, RoomModel room);
        void ShowTab(int tabIndex);
        void EnableField(string fieldName, bool enabled);
        void SetOriginalDates(DateTime checkIn, DateTime checkOut);
        void SetOriginalRoomNumber(string roomNumber);
        int GetSelectedReservationId();
        void ClearForm();
        void ShowSuccessMessage(string message);
        void ShowErrorMessage(string message);
    }
}