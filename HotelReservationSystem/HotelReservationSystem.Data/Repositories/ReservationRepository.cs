using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using HotelReservationSystem.Domain.Interface.Reservation;
using HotelReservationSystem.Domain.Model;
using static HotelReservationSystem.Domain.Enums.ReservationEnum;

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

                // If caller provided a ReservationId (walk-in flow), insert with that ID using IDENTITY_INSERT
                if (reservation.ReservationId > 0)
                {
                    insertCommand.CommandText = @"SET IDENTITY_INSERT Reservations ON;
    INSERT INTO Reservations 
    (ReservationId, CustomerName, CheckInDate, CheckOutDate, TotalAmount, ReservationStatus, RoomNumber, RoomType,
     DownPayment, AmountPaid, IsDownPaymentPaid, PaymentMethod, PaymentStatus, DownPaymentDate, CreatedAt)
    VALUES 
    (@ReservationId, @CustomerName, @CheckInDate, @CheckOutDate, @TotalAmount, @ReservationStatus, @RoomNumber, @RoomType,
     @DownPayment, @AmountPaid, @IsDownPaymentPaid, @PaymentMethod, @PaymentStatus, @DownPaymentDate, GETDATE());
    SET IDENTITY_INSERT Reservations OFF;";

                    insertCommand.Parameters.Add("@ReservationId", SqlDbType.Int).Value = reservation.ReservationId;
                }
                else
                {
                    insertCommand.CommandText = @"
    INSERT INTO Reservations 
    (CustomerName, CheckInDate, CheckOutDate, TotalAmount, ReservationStatus, RoomNumber, RoomType,
     DownPayment, AmountPaid, IsDownPaymentPaid, PaymentMethod, PaymentStatus, DownPaymentDate, CreatedAt)
    VALUES 
    (@CustomerName, @CheckInDate, @CheckOutDate, @TotalAmount, @ReservationStatus, @RoomNumber, @RoomType,
     @DownPayment, @AmountPaid, @IsDownPaymentPaid, @PaymentMethod, @PaymentStatus, @DownPaymentDate, GETDATE())";
                }

                // Common parameters
                insertCommand.Parameters.Add("@RoomType", SqlDbType.VarChar).Value = (object)reservation.RoomType ?? DBNull.Value;
                insertCommand.Parameters.Add("@CustomerName", SqlDbType.VarChar).Value = reservation.CustomerName;
                insertCommand.Parameters.Add("@CheckInDate", SqlDbType.DateTime).Value = reservation.CheckInDate;
                insertCommand.Parameters.Add("@CheckOutDate", SqlDbType.DateTime).Value = reservation.CheckOutDate;
                insertCommand.Parameters.Add("@TotalAmount", SqlDbType.Decimal).Value = reservation.TotalPrice;
                insertCommand.Parameters.Add("@ReservationStatus", SqlDbType.VarChar).Value = reservation.ReservationStatus;
                insertCommand.Parameters.Add("@RoomNumber", SqlDbType.VarChar).Value = (object)reservation.RoomNumber ?? DBNull.Value;

                // Payment fields
                insertCommand.Parameters.Add("@DownPayment", SqlDbType.Decimal).Value = reservation.DownPayment;
                insertCommand.Parameters.Add("@AmountPaid", SqlDbType.Decimal).Value = reservation.AmountPaid;
                insertCommand.Parameters.Add("@IsDownPaymentPaid", SqlDbType.Bit).Value = reservation.IsDownPaymentPaid;
                insertCommand.Parameters.Add("@PaymentMethod", SqlDbType.VarChar).Value = (object)reservation.PaymentMethod ?? DBNull.Value;
                insertCommand.Parameters.Add("@PaymentStatus", SqlDbType.VarChar).Value = reservation.PaymentStatus.ToString();
                insertCommand.Parameters.Add("@DownPaymentDate", SqlDbType.DateTime).Value = (object)reservation.DownPaymentDate ?? DBNull.Value;

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
                command.CommandText = @"
    UPDATE Reservations
    SET CustomerName = @CustomerName,
        CheckInDate = @inDate,
        CheckOutDate = @outDate,
        TotalAmount = @amount,
        ReservationStatus = @status,
        RoomNumber = @RoomNumber,
        RoomType = @RoomType,
        DownPayment = @DownPayment,
        AmountPaid = @AmountPaid,
        IsDownPaymentPaid = @IsDownPaymentPaid,
        PaymentMethod = @PaymentMethod,
        PaymentStatus = @PaymentStatus,
        DownPaymentDate = @DownPaymentDate
    WHERE ReservationId = @reserveId";

                command.Parameters.Add("@RoomType", SqlDbType.VarChar).Value = (object)reservation.RoomType ?? DBNull.Value;
                command.Parameters.Add("@CustomerName", SqlDbType.VarChar).Value = reservation.CustomerName;
                command.Parameters.Add("@inDate", SqlDbType.DateTime).Value = reservation.CheckInDate;
                command.Parameters.Add("@outDate", SqlDbType.DateTime).Value = reservation.CheckOutDate;
                command.Parameters.Add("@amount", SqlDbType.Decimal).Value = reservation.TotalPrice;
                command.Parameters.Add("@status", SqlDbType.VarChar).Value = reservation.ReservationStatus;
                command.Parameters.Add("@RoomNumber", SqlDbType.VarChar).Value = (object)reservation.RoomNumber ?? DBNull.Value;
                command.Parameters.Add("@DownPayment", SqlDbType.Decimal).Value = reservation.DownPayment;
                command.Parameters.Add("@AmountPaid", SqlDbType.Decimal).Value = reservation.AmountPaid;
                command.Parameters.Add("@IsDownPaymentPaid", SqlDbType.Bit).Value = reservation.IsDownPaymentPaid;
                command.Parameters.Add("@PaymentMethod", SqlDbType.VarChar).Value = (object)reservation.PaymentMethod ?? DBNull.Value;
                command.Parameters.Add("@PaymentStatus", SqlDbType.VarChar).Value = reservation.PaymentStatus.ToString();
                command.Parameters.Add("@DownPaymentDate", SqlDbType.DateTime).Value = (object)reservation.DownPaymentDate ?? DBNull.Value;
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
                // Exclude internal WalkIn reservations from reservation listings
                // Order by CreatedAt DESC so newest reservations appear on top in grids
                command.CommandText = "SELECT * FROM Reservations WHERE ReservationStatus <> 'WalkIn' ORDER BY CreatedAt DESC";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        reservationList.Add(MapReaderToModel(reader));
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
                // Exclude WalkIn reservations from search results used by Reservation UI
                // Order by CreatedAt DESC so newest matching reservations appear first
                command.CommandText = @"
                    SELECT * FROM Reservations 
                    WHERE (ReservationId = @id OR CustomerName LIKE @cid)
                    AND ReservationStatus <> 'WalkIn'
                    ORDER BY CreatedAt DESC";

                command.Parameters.Add("@id", SqlDbType.Int).Value = reservationId;
                command.Parameters.Add("@cid", SqlDbType.VarChar).Value = $"%{value}%";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        reservationList.Add(MapReaderToModel(reader));
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

        //<-----------------------Get Reservation By Id--------------------------/>
        public ReservationModel GetById(int reservationId)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                // Get reservation by id including WalkIn (used by check-in flow), so do not filter here
                command.CommandText = "SELECT * FROM Reservations WHERE ReservationId = @id";
                command.Parameters.Add("@id", SqlDbType.Int).Value = reservationId;

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return MapReaderToModel(reader);
                    }
                }
            }

            return null;
        }

        //<-----------------------Get Reservation By Customer Name--------------------------/>
        public ReservationModel GetByCustomerName(string customerName)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                // Exclude WalkIn when used by Reservation UI; callers needing walk-in should query CheckIns
                command.CommandText = "SELECT * FROM Reservations WHERE CustomerName = @name AND ReservationStatus <> 'WalkIn'";
                command.Parameters.Add("@name", SqlDbType.VarChar).Value = customerName;

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return MapReaderToModel(reader);
                    }
                }
            }
            return null;
        }

        //<-----------------------Helper Method: Map Reader to Model--------------------------/>
        private ReservationModel MapReaderToModel(SqlDataReader reader)
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
                RoomNumber = reader["RoomNumber"] == DBNull.Value ? null : reader["RoomNumber"].ToString(),
                RoomType = reader["RoomType"] == DBNull.Value ? null : reader["RoomType"].ToString(),
                DownPayment = reader["DownPayment"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["DownPayment"]),
                AmountPaid = reader["AmountPaid"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["AmountPaid"]),
                IsDownPaymentPaid = reader["IsDownPaymentPaid"] != DBNull.Value && Convert.ToBoolean(reader["IsDownPaymentPaid"]),
                PaymentMethod = reader["PaymentMethod"] == DBNull.Value ? null : reader["PaymentMethod"].ToString(),
                PaymentStatus = Enum.TryParse(reader["PaymentStatus"]?.ToString(), out PaymentState status) ? status : PaymentState.Pending,
                DownPaymentDate = reader["DownPaymentDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["DownPaymentDate"])
            };
        }

        //<-----------------------Check for Overlapping Reservations--------------------------/>
        /// <summary>
        /// Checks if a room has any overlapping reservations for the given date range
        /// </summary>
        public bool HasOverlappingReservation(string roomNumber, DateTime checkInDate, DateTime checkOutDate, int? excludeReservationId = null)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = @"
                    SELECT COUNT(*) 
                    FROM Reservations 
                    WHERE RoomNumber = @roomNumber
                    AND ReservationStatus NOT IN ('Cancelled/No Show', 'CheckedOut')
                    AND (CheckInDate < @checkOutDate AND CheckOutDate > @checkInDate)
                    AND (@excludeReservationId IS NULL OR ReservationId != @excludeReservationId)";

                command.Parameters.Add("@roomNumber", SqlDbType.VarChar).Value = roomNumber;
                command.Parameters.Add("@checkInDate", SqlDbType.DateTime).Value = checkInDate;
                command.Parameters.Add("@checkOutDate", SqlDbType.DateTime).Value = checkOutDate;
                command.Parameters.Add("@excludeReservationId", SqlDbType.Int).Value = (object)excludeReservationId ?? DBNull.Value;

                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }
    }
}