using System.Configuration;

namespace HotelReservationSystem.DataInitializer.DbInitializer
{
    public static class DbConfig
    {
        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;
        }
    }
}
