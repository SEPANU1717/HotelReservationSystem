using System;
using System.Windows.Forms;

namespace HotelReservationSystem.Interface
{
    public interface IMainView
    {
        event EventHandler ShowCustomerView;
        event EventHandler ShowRoomView;
        event EventHandler ShowReservationView;
        event EventHandler ShowLoginView;
        void LoadUserControl(UserControl control);
    }
}
