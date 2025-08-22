using System;
using System.Windows.Forms;

namespace HotelReservationSystem.Domain.Interface.Service.Laundry
{
    public interface ILaundryView
    {
        string LaundryId { get; set; }
        string LaundryName { get; set; }
        string Quantity { get; set; }
        string LPrice { get; set; }
        
        event EventHandler AddEvent;
        event EventHandler ClearEvent;
        event EventHandler CompleteEvent;
        event EventHandler CancelEvent;
        
        void SetLaundryListBindingSource(BindingSource laundryList);
    }
}
