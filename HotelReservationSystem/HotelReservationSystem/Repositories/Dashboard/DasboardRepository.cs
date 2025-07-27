using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Repositories.Dashboard
{
    internal class DasboardRepository : BaseRepository
    {
        public DasboardRepository(string connectionString) : base(connectionString)
        { }
        
        //<-----------------------Get Room Count--------------------------/>
        public int GetRoomCount()
        {
            int count = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM Rooms";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    count = Convert.ToInt32(command.ExecuteScalar());
                }
            }

            return count;
        }

        //<-----------------------Get Available Room--------------------------/>
        public int GetAvailableRoomCount()
        {
            int count = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM Rooms WHERE RoomStatus = 'Available'";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    count = Convert.ToInt32(command.ExecuteScalar());
                }
            }

            return count;
        }

        //<-----------------------Get Occupied Room--------------------------/>
        public int GetOccupiedRoomCount()
        {
            int count = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM Rooms WHERE RoomStatus = 'Occupied'";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    count = Convert.ToInt32(command.ExecuteScalar());
                }
            }

            return count;
        }

        //<-----------------------Get Total Guests--------------------------/>
        public int GetTotalGuest()
        {
            int count = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT SUM(MaxGuests) FROM Rooms";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    var result = command.ExecuteScalar();
                    count = result != DBNull.Value ? Convert.ToInt32(result) : 0;
                }
            }

            return count;
        }
    }
    }

