using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelReservationSystem.Presenter.Common
{
    public class UserInfoDisplay
    {
        public static void UpdateUserInfoDisplay(Label usernameLabel, Label roleLabel)
        {
            if (usernameLabel != null)
            {
                Action setUserName = () => usernameLabel.Text = UserSession.Username ?? string.Empty;
                if (usernameLabel.InvokeRequired)
                    usernameLabel.Invoke(setUserName);
                else
                    setUserName();
            }

            if (roleLabel != null)
            {
                Action setRole = () => roleLabel.Text = UserSession.Role ?? string.Empty;
                if (roleLabel.InvokeRequired)
                    roleLabel.Invoke(setRole);
                else
                    setRole();
            }
        }
    }
}
