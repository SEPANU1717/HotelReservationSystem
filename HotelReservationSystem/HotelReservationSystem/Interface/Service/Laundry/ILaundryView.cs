using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HotelReservationSystem.Model.Service;

namespace HotelReservationSystem.Interface.Service.Laundry
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
