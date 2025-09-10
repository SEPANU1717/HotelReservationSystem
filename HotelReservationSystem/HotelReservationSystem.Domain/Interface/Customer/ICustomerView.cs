using System;
using System.Windows.Forms;

namespace HotelReservationSystem.Domain.Interface.Customer
{
    public interface ICustomerView
    {
        string CustomerID { get; set; }
        string CustomerFirstName { get; set; }
        string CustomerMiddleName { get; set; }
        string CustomerLastName { get; set; }
        DateTime CustomerBirthDate { get; set; }
        string CustomerIdType { get; set; }
        string CustomerContact { get; set; }
        string CustomerAddress { get; set; }
        string CustomerEmail { get; set; }          
        string CustomerGender { get; set; }        
        string CustomerNationality { get; set; }
        string CustomerNotes { get; set; }
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

        void SetCustomerListBindingSource(BindingSource customerList);
        void Show();
    }
}
