using System;
using System.Windows.Forms;

namespace HotelReservationSystem.Domain.Interface
{
    public interface IMainView
    {
        event EventHandler ShowCustomerView;
        event EventHandler ShowRoomView;
        event EventHandler ShowReservationView;
        event EventHandler ShowLoginView;
        event EventHandler ShowBillingView;
        event EventHandler ShowServiceView;
        
        void LoadUserControl(UserControl control);
    }
}
