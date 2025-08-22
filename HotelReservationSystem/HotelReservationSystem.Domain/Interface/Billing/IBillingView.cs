using System;
using System.Windows.Forms;

namespace HotelReservationSystem.Domain.Interface.Billing
{
    public interface IBillingView
    {
        string BillId { get; set; }
        string ReservationId { get; set; }
        string CustomerName { get; set; }
        string RoomType { get; set; }
        string RoomNumber { get; set; }
        string TotalAmount { get; set; }
        string PaymentStatus { get; set; }

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

        void SetBillingListBindingSource(BindingSource billingList);
    }
}
