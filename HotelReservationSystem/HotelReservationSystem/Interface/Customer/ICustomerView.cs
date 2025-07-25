using System;
using System.Windows.Forms;

namespace HotelReservationSystem.Interface
{
    public interface ICustomerView
    {
        string CustomerID { get; set; }
        string CustomerName { get; set; }
        string CustomerIdType { get; set; }
        string CustomerContact { get; set; }
        string CustomerAddress { get; set; }
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

        void SetCustomerListBindingSource(BindingSource customerList);
        void Show();
    }
}
