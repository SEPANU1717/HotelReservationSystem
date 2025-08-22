using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using HotelReservationSystem.Domain.Interface.Reservation;
using HotelReservationSystem.Domain.Model;

namespace HotelReservationSystem.Data.Repositories
{
    public class ReservationRepository : BaseRepository, IReservationRepository
    {
        public ReservationRepository(string connectionString) : base(connectionString) { }

        //<-----------------------Add Reservation--------------------------/>
        public void Add(ReservationModel reservation)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var insertCommand = connection.CreateCommand())
            {
                connection.Open();
                insertCommand.CommandText = @" INSERT INTO Reservations (CustomerName, CheckInDate, CheckOutDate, TotalAmount, ReservationStatus, RoomNumber) 
                                               VALUES (@CustomerName, @CheckInDate, @CheckOutDate, @TotalAmount, @ReservationStatus, @RoomNumber)";

                insertCommand.Parameters.Add("@CustomerName", SqlDbType.VarChar).Value = reservation.CustomerName;
                insertCommand.Parameters.Add("@CheckInDate", SqlDbType.DateTime).Value = reservation.CheckInDate;
                insertCommand.Parameters.Add("@CheckOutDate", SqlDbType.DateTime).Value = reservation.CheckOutDate;
                insertCommand.Parameters.Add("@TotalAmount", SqlDbType.Decimal).Value = reservation.TotalPrice;
                insertCommand.Parameters.Add("@ReservationStatus", SqlDbType.VarChar).Value = reservation.ReservationStatus;
                insertCommand.Parameters.Add("@RoomNumber", SqlDbType.VarChar).Value = (object)reservation.RoomNumber ?? DBNull.Value;

                insertCommand.ExecuteNonQuery();
            }
        }

        //<-----------------------Delete Reservation--------------------------/>
        public void Delete(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "DELETE FROM Reservations WHERE ReservationId = @id";

                command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                command.ExecuteNonQuery();
            }
        }

        //<-----------------------Edit Reservation--------------------------/>
        public void Edit(ReservationModel reservation)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"UPDATE Reservations
                                            SET CustomerName = @CustomerName,
                                                CheckInDate = @inDate,
                                                CheckOutDate = @outDate,
                                                TotalAmount = @amount,
                                                ReservationStatus = @status,
                                                RoomNumber = @RoomNumber
                                            WHERE Reservationid = @reserveId";

                command.Parameters.Add("@CustomerName", SqlDbType.VarChar).Value = reservation.CustomerName;
                command.Parameters.Add("@inDate", SqlDbType.DateTime).Value = reservation.CheckInDate;
                command.Parameters.Add("@outDate", SqlDbType.DateTime).Value = reservation.CheckOutDate;
                command.Parameters.Add("@amount", SqlDbType.Decimal).Value = reservation.TotalPrice;
                command.Parameters.Add("@status", SqlDbType.NVarChar, 50).Value = reservation.ReservationStatus;
                command.Parameters.Add("@RoomNumber", SqlDbType.VarChar).Value = (object)reservation.RoomNumber ?? DBNull.Value;
                command.Parameters.Add("@reserveId", SqlDbType.Int).Value = reservation.ReservationId;

                command.ExecuteNonQuery();
            }
        }

        //<-----------------------Get All Reservation--------------------------/>
        public IEnumerable<ReservationModel> GetAll()
        {
            var reservationList = new List<ReservationModel>();
            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM Reservations order by ReservationId desc";

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

        //<-----------------------Get Reservation By value--------------------------/>
        public IEnumerable<ReservationModel> GetByValue(string value)
        {
            var reservationList = new List<ReservationModel>();
            int reservationId = int.TryParse(value, out _) ? Convert.ToInt32(value) : 0;

            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"SELECT * FROM Reservations 
                                    WHERE ReservationId = @id OR CustomerName LIKE @cid 
                                    ORDER BY ReservationId DESC";

                command.Parameters.Add("@id", SqlDbType.Int).Value = reservationId;
                command.Parameters.Add("@cid", SqlDbType.VarChar).Value = $"%{value}%";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        reservationList.Add(new ReservationModel
                        {
                            ReservationId = Convert.ToInt32(reader["ReservationId"]),
                            CustomerName = reader["CustomerName"].ToString(),
                            CheckInDate = Convert.ToDateTime(reader["CheckInDate"]),
                            CheckOutDate = Convert.ToDateTime(reader["CheckOutDate"]),
                            TotalPrice = Convert.ToDecimal(reader["TotalAmount"]),
                            ReservationStatus = reader["ReservationStatus"].ToString(),
                            CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                            RoomNumber = reader["RoomNumber"] == DBNull.Value ? null : reader["RoomNumber"].ToString()
                        });
                    }
                }
            }

            return reservationList;
        }

        //<-----------------------Get Next Reservation Id--------------------------/>
        public int GetNextReservationId()
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand("SELECT ISNULL(MAX(ReservationId), 0) + 1 FROM Reservations", connection))
            {
                connection.Open();
                return (int)command.ExecuteScalar();
            }
        }

        public ReservationModel GetById(int reservationId)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = "SELECT * FROM Reservations WHERE ReservationId = @id";
                command.Parameters.Add("@id", SqlDbType.Int).Value = reservationId;

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new ReservationModel
                        {
                            ReservationId = Convert.ToInt32(reader["ReservationId"]),
                            CustomerName = reader["CustomerName"].ToString(),
                            CheckInDate = Convert.ToDateTime(reader["CheckInDate"]),
                            CheckOutDate = Convert.ToDateTime(reader["CheckOutDate"]),
                            TotalPrice = Convert.ToDecimal(reader["TotalAmount"]),
                            ReservationStatus = reader["ReservationStatus"].ToString(),
                            CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                            RoomNumber = reader["RoomNumber"] == DBNull.Value ? null : reader["RoomNumber"].ToString()
                        };
                    }
                }
            }
            return null;
        }
    }
}
