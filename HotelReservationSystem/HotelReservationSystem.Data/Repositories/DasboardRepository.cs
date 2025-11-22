using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HotelReservationSystem.Domain.Model;

namespace HotelReservationSystem.Data.Repositories
{
    public class DasboardRepository : BaseRepository
    {

            //<-----------------------Get All Reservation--------------------------/>
            public IEnumerable<ReservationModel> GetAll()
            {
                var reservationList = new List<ReservationModel>();
                using (var connection = new SqlConnection(connectionString))
                using (var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                command.CommandText = "SELECT * FROM Reservations WHERE ReservationStatus = 'Reserved' ORDER BY CreatedAt DESC";

                using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var reservation = new ReservationModel();
                            reservation.ReservationId = (int)(reader["ReservationId"]);
                            reservation.CustomerName = reader["CustomerName"].ToString();
                            reservation.CheckInDate = (DateTime)(reader["CheckInDate"]);
                            reservation.CheckOutDate = (DateTime)(reader["CheckOutDate"]);
                            reservation.TotalPrice = Convert.ToDecimal(reader["TotalAmount"]);
                            reservation.ReservationStatus = reader["ReservationStatus"].ToString();
                            reservation.CreatedAt = Convert.ToDateTime(reader["CreatedAt"]);
                            reservation.RoomNumber = reader["RoomNumber"] == DBNull.Value ? null : reader["RoomNumber"].ToString();

                            reservationList.Add(reservation);
                        }
                    }
                }
                return reservationList;
            }
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

        //<-----------------------Get Reserved Room Count--------------------------/>
        public int GetReservedRoomCount()
        {
            int count = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM Reservations WHERE ReservationStatus = 'Reserved'";
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

