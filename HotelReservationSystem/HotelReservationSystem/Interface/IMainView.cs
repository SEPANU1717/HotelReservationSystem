using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelReservationSystem.Interface
{
    public interface IMainView
    {
        event EventHandler ShowCustomerView;
        void LoadUserControl(UserControl control);
    }
}
