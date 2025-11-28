using System;
using System.Collections.Generic;
using System.Windows.Forms;
using HotelReservationSystem.Domain.Model;
using static HotelReservationSystem.Domain.Enums.ReservationEnum;

namespace HotelReservationSystem.Domain.Interface.Reservation
{
    public interface IReservationView
    {
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
        string PaymentReference { get; set; }
        PaymentState PaymentStatus { get; set; }
        string ReservationStatus { get; set; }
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
        event EventHandler ShowCheckInOutView;

        event EventHandler<int> LoadReservationForEditEvent;
        event EventHandler<string> SetCustomerForReservationEvent;
        event EventHandler<string> RoomTypeChangedEvent;
        event EventHandler<string> PaymentTypeChangedEvent; 


        void SetReservationListBindingSource(BindingSource reservationList);
        void Show();

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