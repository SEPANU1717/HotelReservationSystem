using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using HotelReservationSystem.Domain.Interface.CheckInOut;
using HotelReservationSystem.Domain.Model.CheckInOut;
using static HotelReservationSystem.Domain.Enums.ReservationEnum;

namespace HotelReservationSystem.Data.Repositories.CheckInOut
{
    public class CheckInOutRepository : BaseRepository, ICheckInOutRepository
    {
        public CheckInOutRepository(string connectionString) : base(connectionString) { }

        //<-----------------------Add CheckIn--------------------------/>
        public void Add(CheckInOutModel checkIn)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = @"
                    INSERT INTO CheckIns 
                    (ReservationId, CustomerName, RoomType, RoomNumber, CheckInDate, CheckOutDate, TimeArrival,
                     TotalPrice, TotalCompanionCost, DownPayment, AmountPaid, CompanionCount,
                     PaymentMethod, PaymentReference, PaymentStatus, ReservationStatus,
                     IsCheckedIn, IsCheckedOut, ActualCheckIn, ActualCheckOut, 
                     CheckedInBy, CheckedOutBy, CheckInNotes, CheckOutNotes, CreatedAt)
                    VALUES 
                    (@ReservationId, @CustomerName, @RoomType, @RoomNumber, @CheckInDate, @CheckOutDate, @TimeArrival,
                     @TotalPrice, @TotalCompanionCost, @DownPayment, @AmountPaid, @CompanionCount,
                     @PaymentMethod, @PaymentReference, @PaymentStatus, @ReservationStatus,
                     @IsCheckedIn, @IsCheckedOut, @ActualCheckIn, @ActualCheckOut,
                     @CheckedInBy, @CheckedOutBy, @CheckInNotes, @CheckOutNotes, GETDATE())";

                AddCheckInParameters(command, checkIn);
                command.ExecuteNonQuery();
            }
        }

        //<-----------------------Edit CheckIn--------------------------/>
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
                        TotalCompanionCost = @TotalCompanionCost,
                        DownPayment = @DownPayment,
                        AmountPaid = @AmountPaid,
                        CompanionCount = @CompanionCount,
                        PaymentMethod = @PaymentMethod,
                        PaymentReference = @PaymentReference,
                        PaymentStatus = @PaymentStatus,
                        ReservationStatus = @ReservationStatus,
                        IsCheckedIn = @IsCheckedIn,
                        IsCheckedOut = @IsCheckedOut,
                        ActualCheckIn = @ActualCheckIn,
                        ActualCheckOut = @ActualCheckOut,
                        CheckedInBy = @CheckedInBy,
                        CheckedOutBy = @CheckedOutBy,
                        CheckInNotes = @CheckInNotes,
                        CheckOutNotes = @CheckOutNotes,
                        UpdatedAt = GETDATE()
                    WHERE ReservationId = @ReservationId";

                AddCheckInParameters(command, checkIn);
                command.ExecuteNonQuery();
            }
        }

        //<-----------------------Delete CheckIn--------------------------/>
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

        //<-----------------------Get CheckIn By Id--------------------------/>
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

        //<-----------------------Get CheckIn By Reservation Id--------------------------/>
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

        //<-----------------------Get All CheckIns--------------------------/>
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

        //<-----------------------Get CheckIns By Value--------------------------/>
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

        //<-----------------------Get Active CheckIns--------------------------/>
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

        //<-----------------------Get Next CheckIn Id--------------------------/>
        public int GetNextCheckInId()
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand("SELECT ISNULL(MAX(CheckInId), 0) + 1 FROM CheckIns", connection))
            {
                connection.Open();
                return (int)command.ExecuteScalar();
            }
        }

        //<-----------------------Helper Method: Add Parameters--------------------------/>
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
            command.Parameters.Add("@TotalCompanionCost", SqlDbType.Decimal).Value = model.TotalCompanionCost;
            command.Parameters.Add("@DownPayment", SqlDbType.Decimal).Value = model.DownPayment;
            command.Parameters.Add("@AmountPaid", SqlDbType.Decimal).Value = model.AmountPaid;
            command.Parameters.Add("@CompanionCount", SqlDbType.Int).Value = model.CompanionCount;
            command.Parameters.Add("@PaymentMethod", SqlDbType.VarChar).Value = model.PaymentMethod ?? (object)DBNull.Value;
            command.Parameters.Add("@PaymentReference", SqlDbType.VarChar).Value = model.PaymentReference ?? (object)DBNull.Value;
            command.Parameters.Add("@PaymentStatus", SqlDbType.VarChar).Value = model.PaymentStatus.ToString();
            command.Parameters.Add("@ReservationStatus", SqlDbType.VarChar).Value = model.ReservationStatus ?? (object)DBNull.Value;
            command.Parameters.Add("@IsCheckedIn", SqlDbType.Bit).Value = model.IsCheckedIn;
            command.Parameters.Add("@IsCheckedOut", SqlDbType.Bit).Value = model.IsCheckedOut;
            command.Parameters.Add("@ActualCheckIn", SqlDbType.DateTime).Value = model.ActualCheckIn ?? (object)DBNull.Value;
            command.Parameters.Add("@ActualCheckOut", SqlDbType.DateTime).Value = model.ActualCheckOut ?? (object)DBNull.Value;
            command.Parameters.Add("@CheckedInBy", SqlDbType.VarChar).Value = model.CheckedInBy ?? (object)DBNull.Value;
            command.Parameters.Add("@CheckedOutBy", SqlDbType.VarChar).Value = model.CheckedOutBy ?? (object)DBNull.Value;
            command.Parameters.Add("@CheckInNotes", SqlDbType.VarChar).Value = model.CheckInNotes ?? (object)DBNull.Value;
            command.Parameters.Add("@CheckOutNotes", SqlDbType.VarChar).Value = model.CheckOutNotes ?? (object)DBNull.Value;
        }

        //<-----------------------Helper Method: Map Reader to Model--------------------------/>
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
                TotalCompanionCost = reader["TotalCompanionCost"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["TotalCompanionCost"]),
                DownPayment = Convert.ToDecimal(reader["DownPayment"]),
                AmountPaid = Convert.ToDecimal(reader["AmountPaid"]),
                CompanionCount = reader["CompanionCount"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CompanionCount"]),
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
                CheckInNotes = reader["CheckInNotes"] == DBNull.Value ? null : reader["CheckInNotes"].ToString(),
                CheckOutNotes = reader["CheckOutNotes"] == DBNull.Value ? null : reader["CheckOutNotes"].ToString(),
                CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
            };
        }
    }
}