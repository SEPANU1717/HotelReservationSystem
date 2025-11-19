using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using HotelReservationSystem.Domain.Interface.CheckInOut;
using HotelReservationSystem.Domain.Model.CheckInOut;
using static HotelReservationSystem.Domain.Enums.ReservationEnum;

namespace HotelReservationSystem.Data.Repositories.CheckInOutRepository
{
    public class CheckInOutRepository : BaseRepository, ICheckInOutRepository
    {
        public CheckInOutRepository(string connectionString) : base(connectionString) { }

        /// <summary>
        /// Check if check-in already exists for a reservation
        /// </summary>
        public bool ExistsForReservation(int reservationId)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = "SELECT COUNT(*) FROM CheckIns WHERE ReservationId = @id";
                command.Parameters.Add("@id", SqlDbType.Int).Value = reservationId;
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }

        public void Add(CheckInOutModel checkIn)
        {
            // PREVENT DUPLICATES - Validation happens in presenter now
            if (ExistsForReservation(checkIn.ReservationId))
            {
                throw new InvalidOperationException($"A check-in already exists for Reservation ID {checkIn.ReservationId}. Please use Edit instead.");
            }

            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = @"
                    INSERT INTO CheckIns 
                    (ReservationId, CustomerName, RoomType, RoomNumber, CheckInDate, CheckOutDate, TimeArrival,
                     TotalPrice, DownPayment, AmountPaid, CustomerEmail,
                     PaymentMethod, PaymentReference, PaymentStatus, ReservationStatus,
                     IsCheckedIn, IsCheckedOut, ActualCheckIn, CreatedAt)
                    VALUES 
                    (@ReservationId, @CustomerName, @RoomType, @RoomNumber, @CheckInDate, @CheckOutDate, @TimeArrival,
                     @TotalPrice, @DownPayment, @AmountPaid, @CustomerEmail,
                     @PaymentMethod, @PaymentReference, @PaymentStatus, @ReservationStatus,
                     @IsCheckedIn, @IsCheckedOut, @ActualCheckIn, GETDATE())";

                AddCheckInParameters(command, checkIn);
                command.ExecuteNonQuery();
            }
        }

        public void Edit(CheckInOutModel checkIn)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = @"
                    UPDATE CheckIns SET
                        CustomerName = @CustomerName,
                        RoomType = @RoomType,
                        RoomNumber = @RoomNumber,
                        CheckInDate = @CheckInDate,
                        CheckOutDate = @CheckOutDate,
                        TimeArrival = @TimeArrival,
                        TotalPrice = @TotalPrice,
                        DownPayment = @DownPayment,
                        AmountPaid = @AmountPaid,
                        CustomerEmail = @CustomerEmail,
                        PaymentMethod = @PaymentMethod,
                        PaymentReference = @PaymentReference,
                        PaymentStatus = @PaymentStatus,
                        ReservationStatus = @ReservationStatus,
                        IsCheckedIn = @IsCheckedIn,
                        IsCheckedOut = @IsCheckedOut,
                        ActualCheckIn = @ActualCheckIn
                    WHERE ReservationId = @ReservationId";

                AddCheckInParameters(command, checkIn);
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int reservationId)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = "DELETE FROM CheckIns WHERE ReservationId = @id";
                command.Parameters.Add("@id", SqlDbType.Int).Value = reservationId;
                command.ExecuteNonQuery();
            }
        }

        public CheckInOutModel GetById(int checkInId)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = "SELECT * FROM CheckIns WHERE CheckInId = @id";
                command.Parameters.Add("@id", SqlDbType.Int).Value = checkInId;

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

        public CheckInOutModel GetByReservationId(int reservationId)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = "SELECT * FROM CheckIns WHERE ReservationId = @id";
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

        public IEnumerable<CheckInOutModel> GetAll()
        {
            var list = new List<CheckInOutModel>();
            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = "SELECT * FROM CheckIns ORDER BY CheckInDate DESC";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(MapReaderToModel(reader));
                    }
                }
            }
            return list;
        }

        public IEnumerable<CheckInOutModel> GetByValue(string value)
        {
            var list = new List<CheckInOutModel>();
            int reservationId = int.TryParse(value, out _) ? Convert.ToInt32(value) : 0;

            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = @"
                    SELECT * FROM CheckIns 
                    WHERE ReservationId = @id 
                       OR CustomerName LIKE @value 
                       OR RoomNumber LIKE @value
                    ORDER BY CheckInDate DESC";

                command.Parameters.Add("@id", SqlDbType.Int).Value = reservationId;
                command.Parameters.Add("@value", SqlDbType.VarChar).Value = $"%{value}%";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(MapReaderToModel(reader));
                    }
                }
            }
            return list;
        }

        public IEnumerable<CheckInOutModel> GetActiveCheckIns()
        {
            var list = new List<CheckInOutModel>();
            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = @"
                    SELECT * FROM CheckIns 
                    WHERE IsCheckedIn = 1 AND IsCheckedOut = 0
                    ORDER BY CheckInDate DESC";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(MapReaderToModel(reader));
                    }
                }
            }
            return list;
        }

        public int GetNextCheckInId()
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand("SELECT ISNULL(MAX(CheckInId), 0) + 1 FROM CheckIns", connection))
            {
                connection.Open();
                return (int)command.ExecuteScalar();
            }
        }

        private void AddCheckInParameters(SqlCommand command, CheckInOutModel model)
        {
            command.Parameters.Add("@ReservationId", SqlDbType.Int).Value = model.ReservationId;
            command.Parameters.Add("@CustomerName", SqlDbType.VarChar).Value = model.CustomerName ?? (object)DBNull.Value;
            command.Parameters.Add("@RoomType", SqlDbType.VarChar).Value = model.RoomType ?? (object)DBNull.Value;
            command.Parameters.Add("@RoomNumber", SqlDbType.VarChar).Value = model.RoomNumber ?? (object)DBNull.Value;
            command.Parameters.Add("@CheckInDate", SqlDbType.DateTime).Value = model.CheckInDate;
            command.Parameters.Add("@CheckOutDate", SqlDbType.DateTime).Value = model.CheckOutDate;
            command.Parameters.Add("@TimeArrival", SqlDbType.DateTime).Value = model.TimeArrival;
            command.Parameters.Add("@TotalPrice", SqlDbType.Decimal).Value = model.TotalPrice;
            command.Parameters.Add("@DownPayment", SqlDbType.Decimal).Value = model.DownPayment;
            command.Parameters.Add("@AmountPaid", SqlDbType.Decimal).Value = model.AmountPaid;
            command.Parameters.Add("@CustomerEmail", SqlDbType.VarChar, 100).Value = model.CustomerEmail ?? (object)DBNull.Value;
            command.Parameters.Add("@PaymentMethod", SqlDbType.VarChar).Value = model.PaymentMethod ?? (object)DBNull.Value;
            command.Parameters.Add("@PaymentReference", SqlDbType.VarChar).Value = model.PaymentReference ?? (object)DBNull.Value;
            command.Parameters.Add("@PaymentStatus", SqlDbType.VarChar).Value = model.PaymentStatus.ToString();
            command.Parameters.Add("@ReservationStatus", SqlDbType.VarChar).Value = model.ReservationStatus ?? (object)DBNull.Value;
            command.Parameters.Add("@IsCheckedIn", SqlDbType.Bit).Value = model.IsCheckedIn;
            command.Parameters.Add("@IsCheckedOut", SqlDbType.Bit).Value = model.IsCheckedOut;
            command.Parameters.Add("@ActualCheckIn", SqlDbType.DateTime).Value = model.ActualCheckIn ?? (object)DBNull.Value;
        }

        private CheckInOutModel MapReaderToModel(SqlDataReader reader)
        {
            return new CheckInOutModel
            {
                CheckInId = Convert.ToInt32(reader["CheckInId"]),
                ReservationId = Convert.ToInt32(reader["ReservationId"]),
                CustomerName = reader["CustomerName"].ToString(),
                RoomType = reader["RoomType"] == DBNull.Value ? null : reader["RoomType"].ToString(),
                RoomNumber = reader["RoomNumber"] == DBNull.Value ? null : reader["RoomNumber"].ToString(),
                CheckInDate = Convert.ToDateTime(reader["CheckInDate"]),
                CheckOutDate = Convert.ToDateTime(reader["CheckOutDate"]),
                TimeArrival = reader["TimeArrival"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(reader["TimeArrival"]),
                TotalPrice = Convert.ToDecimal(reader["TotalPrice"]),
                TotalCompanionCost = 0,
                DownPayment = Convert.ToDecimal(reader["DownPayment"]),
                AmountPaid = Convert.ToDecimal(reader["AmountPaid"]),
                CustomerEmail = reader["CustomerEmail"] == DBNull.Value ? null : reader["CustomerEmail"].ToString(),
                PaymentMethod = reader["PaymentMethod"] == DBNull.Value ? null : reader["PaymentMethod"].ToString(),
                PaymentReference = reader["PaymentReference"] == DBNull.Value ? null : reader["PaymentReference"].ToString(),
                PaymentStatus = Enum.TryParse(reader["PaymentStatus"]?.ToString(), out PaymentState status) ? status : PaymentState.Pending,
                ReservationStatus = reader["ReservationStatus"] == DBNull.Value ? null : reader["ReservationStatus"].ToString(),
                IsCheckedIn = Convert.ToBoolean(reader["IsCheckedIn"]),
                IsCheckedOut = Convert.ToBoolean(reader["IsCheckedOut"]),
                ActualCheckIn = reader["ActualCheckIn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["ActualCheckIn"]),
                ActualCheckOut = reader["ActualCheckOut"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["ActualCheckOut"]),
                CheckedInBy = reader["CheckedInBy"] == DBNull.Value ? null : reader["CheckedInBy"].ToString(),
                CheckedOutBy = reader["CheckedOutBy"] == DBNull.Value ? null : reader["CheckedOutBy"].ToString(),
                CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
            };
        }

        /// <summary>
        /// Perform checkout and update the check-in record
        /// </summary>
        public void CheckOut(int reservationId, DateTime actualCheckOut, string checkedOutBy)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = @"
                    UPDATE CheckIns SET
                        IsCheckedOut = 1,
                        ActualCheckOut = @ActualCheckOut,
                        CheckedOutBy = @CheckedOutBy,
                        ReservationStatus = 'CheckedOut',
                        UpdatedAt = GETDATE()
                    WHERE ReservationId = @ReservationId AND IsCheckedIn = 1 AND IsCheckedOut = 0";

                command.Parameters.Add("@ReservationId", SqlDbType.Int).Value = reservationId;
                command.Parameters.Add("@ActualCheckOut", SqlDbType.DateTime).Value = actualCheckOut;
                command.Parameters.Add("@CheckedOutBy", SqlDbType.VarChar).Value = checkedOutBy ?? (object)DBNull.Value;

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected == 0)
                {
                    throw new InvalidOperationException("Unable to checkout. Guest may not be checked in or already checked out.");
                }
            }
        }
    }
}