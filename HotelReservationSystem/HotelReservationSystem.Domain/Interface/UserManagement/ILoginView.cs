using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Domain.Interface
{
    public interface ILoginView
    {
        string UsernameOrEmail { get; set; }
        string Password { get; set; }
        bool RememberMe { get; set; }

        bool isSuccessful { get; set; }
        string Message { get; set; }

        event EventHandler LoginEvent;
        event EventHandler CancelEvent;

        void Show();
        void Hide();
        void ClearFields();
    }
}
