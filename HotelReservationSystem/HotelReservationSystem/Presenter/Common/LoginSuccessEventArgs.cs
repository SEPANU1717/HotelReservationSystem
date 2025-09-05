using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelReservationSystem.Domain.Model;

namespace HotelReservationSystem.Presenter.Common
{
    public class LoginSuccessEventArgs : EventArgs
    {
        public UserModel User { get; }

        public LoginSuccessEventArgs(UserModel user)
        {
            User = user;
        }
    }
}
