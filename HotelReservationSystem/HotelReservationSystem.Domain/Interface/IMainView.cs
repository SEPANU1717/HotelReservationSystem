using System;
using System.Windows.Forms;

namespace HotelReservationSystem.Domain.Interface
{
    public interface IMainView
    {
        event EventHandler ShowCustomerView;
        event EventHandler ShowRoomView;
        event EventHandler ShowReservationView;
        event EventHandler ShowBillingView;
        event EventHandler ShowServiceView;
        event EventHandler ShowUserView;

        void LoadUserControl(UserControl control);
    }
}
