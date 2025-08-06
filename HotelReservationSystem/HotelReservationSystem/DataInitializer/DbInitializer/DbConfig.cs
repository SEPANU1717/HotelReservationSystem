using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Helper
{
    public static class DbConfig
    {
        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;
        }
    }
}
