using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelReservationSystem.Domain.Model;

namespace HotelReservationSystem.Presenter.Common
{
    public class UserSession
    {
        private static UserModel _currentUser;
        public static UserModel CurrentUser
        {
            get { return _currentUser; }
            private set { _currentUser = value; }
        }

        public static bool IsLoggedIn => CurrentUser != null;
        public static string Username => CurrentUser?.Username ?? string.Empty;
        public static string FullName => CurrentUser?.FullName ?? string.Empty;
        public static string Role => CurrentUser?.Role ?? string.Empty;
        public static bool IsAdmin => Role.Equals("Admin", StringComparison.OrdinalIgnoreCase);
        public static bool IsStaff => Role.Equals("Staff", StringComparison.OrdinalIgnoreCase);
        public static void Login(UserModel user) => CurrentUser = user;
        public static void Logout() => CurrentUser = null;
    }
}
