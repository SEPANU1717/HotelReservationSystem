using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelReservationSystem.Interface.Reservation;
using HotelReservationSystem.Model.Reservation;
using HotelReservationSystem.Model.Rooms;
using Newtonsoft.Json.Linq;

namespace HotelReservationSystem.Repositories
{
    public class ReservationRepository : BaseRepository, IReservationRepository
    {
        public ReservationRepository(string connectionString) : base(connectionString) {}

        //<-----------------------Add Reservation--------------------------/>
        public void Add(ReservationModel reservation)
        {
            using (var connection = new SqlConnection(connectionString))
            using(var checkCommand = connection.CreateCommand())
            using(var insertCommand = connection.CreateCommand())
            {
                connection.Open();
                checkCommand.CommandText = "SELECT COUNT (*) FROM Reservations WHERE CustomerId = @cId";
                checkCommand.Parameters.Add("@cId", SqlDbType.Int).Value = reservation.CustomerId;

                int count = (int)checkCommand.ExecuteScalar();
                if (count > 0) throw new Exception($"Customer id '{reservation.CustomerId}' already exists.");

                insertCommand.Connection = connection;
                insertCommand.CommandText = @"
                    INSERT INTO Reservations (ReservationId, CustomerId, RoomId, CheckInDate, CheckOutDate, TotalAmount, ReservationStatus) 
                    VALUES (@ReservationId, @CustomerId, @RoomId, @CheckInDate, @CheckOutDate, @TotalAmount, @ReservationStatus)";

                insertCommand.Parameters.Add("@ReservationId", SqlDbType.Int).Value = reservation.ReservationId;
                insertCommand.Parameters.Add("@CustomerId", SqlDbType.Int).Value = reservation.CustomerId;
                insertCommand.Parameters.Add("@RoomId", SqlDbType.Int).Value = reservation.RoomId;
                insertCommand.Parameters.Add("@CheckInDate", SqlDbType.DateTime).Value = reservation.CheckInDate;
                insertCommand.Parameters.Add("@CheckOutDate", SqlDbType.DateTime).Value = reservation.CheckOutDate;
                insertCommand.Parameters.Add("@TotalAmount", SqlDbType.Decimal).Value = reservation.TotalPrice;
                insertCommand.Parameters.Add("@ReservationStatus", SqlDbType.VarChar).Value = reservation.ReservationStatus;

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
                command.CommandText = "DELETE FROM Reservations WHERE ReservatioId = @id";

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
                                        SET CustomerId = @customerid,
                                        RoomId = roomId,
                                        CheckInDate = @inDate,
                                        CheckOutDate = @outDate,
                                        TotalAmount = @amount,
                                        ReservationStatus = @status
                                        WHERE Reservationid = @reserveId";

                command.Parameters.Add("@customerId", SqlDbType.Int).Value = reservation.CustomerId;
                command.Parameters.Add("@roomId", SqlDbType.Int).Value = reservation.RoomId;
                command.Parameters.Add("@inDate", SqlDbType.DateTime).Value = reservation.CheckInDate;
                command.Parameters.Add("@outDate", SqlDbType.DateTime).Value = reservation.CheckOutDate;
                command.Parameters.Add("@amount", SqlDbType.Decimal).Value = reservation.TotalPrice;
                command.Parameters.Add("@status", SqlDbType.NVarChar, 50).Value = reservation.ReservationStatus;
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
                        reservation.ReservationId = (int)(reader[0]);
                        reservation.CustomerId = (int)(reader[1]);
                        reservation.RoomId = (int)(reader[2]);
                        reservation.CheckInDate = (DateTime)(reader[3]);
                        reservation.CheckOutDate = (DateTime)(reader[4]);
                        reservation.TotalPrice = Convert.ToDecimal(reader[5]);
                        reservation.ReservationStatus = reader[6].ToString();
                        reservation.CreatedAt = Convert.ToDateTime(reader[7]);

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
                                WHERE ReservationId = @id OR ReservationStatus LIKE @status 
                                ORDER BY ReservationId DESC";

                command.Parameters.Add("@id", SqlDbType.Int).Value = reservationId;
                command.Parameters.Add("@status", SqlDbType.NVarChar).Value = $"%{value}%";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        reservationList.Add(new ReservationModel
                        {
                            ReservationId = Convert.ToInt32(reader[0]),
                            CustomerId = Convert.ToInt32(reader[1]),
                            RoomId = Convert.ToInt32(reader[2]),
                            CheckInDate = Convert.ToDateTime(reader[3]),
                            CheckOutDate = Convert.ToDateTime(reader[4]),
                            TotalPrice = Convert.ToDecimal(reader[5]),
                            ReservationStatus = reader[6].ToString()
                        });
                    }
                }
            }

            return reservationList;
        }

    }
}
