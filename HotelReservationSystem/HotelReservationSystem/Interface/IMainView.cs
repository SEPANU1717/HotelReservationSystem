using System;
using System.Windows.Forms;

namespace HotelReservationSystem.Interface
{
    public interface IMainView
    {
        event EventHandler ShowCustomerView;
        event EventHandler ShowRoomView;
        void LoadUserControl(UserControl control);
    }
}
