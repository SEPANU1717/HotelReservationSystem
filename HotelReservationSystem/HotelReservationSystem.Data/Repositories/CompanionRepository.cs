using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HotelReservationSystem.Data.Repositories;
using HotelReservationSystem.Domain.Model;

namespace HotelReservationSystem.Data.Repositories.CheckInOutRepository
{
    public class CompanionRepository : BaseRepository
    {
        public CompanionRepository(string connectionString) : base(connectionString)
        {
        }

        public int Add(CompanionModel model)
        {
            const string sql = @"
                INSERT INTO [dbo].[Companions]
                ([MainReservationId], [CompanionName], [ContactNumber], [Email], [Relationship], 
                 [RoomType], [RoomNumber], [RoomPrice], [Nights], [CheckInDate], [CheckOutDate], 
                 [TotalCost], [CreatedAt], [CreatedBy])
                VALUES 
                (@MainReservationId, @CompanionName, @ContactNumber, @Email, @Relationship, 
                 @RoomType, @RoomNumber, @RoomPrice, @Nights, @CheckInDate, @CheckOutDate, 
                 @TotalCost, @CreatedAt, @CreatedBy);
                SELECT CAST(SCOPE_IDENTITY() AS INT);";

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@MainReservationId", model.MainReservationId);
                cmd.Parameters.AddWithValue("@CompanionName", model.CompanionName);
                cmd.Parameters.AddWithValue("@ContactNumber", (object)model.ContactNumber ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Email", (object)model.Email ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Relationship", (object)model.Relationship ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@RoomType", model.RoomType);
                cmd.Parameters.AddWithValue("@RoomNumber", model.RoomNumber);
                cmd.Parameters.AddWithValue("@RoomPrice", model.RoomPrice);
                cmd.Parameters.AddWithValue("@Nights", model.Nights);
                cmd.Parameters.AddWithValue("@CheckInDate", model.CheckInDate);
                cmd.Parameters.AddWithValue("@CheckOutDate", model.CheckOutDate);
                cmd.Parameters.AddWithValue("@TotalCost", model.TotalCost);
                cmd.Parameters.AddWithValue("@CreatedAt", model.CreatedAt == default(DateTime) ? DateTime.Now : model.CreatedAt);
                cmd.Parameters.AddWithValue("@CreatedBy", (object)model.CreatedBy ?? DBNull.Value);

                conn.Open();
                return (int)cmd.ExecuteScalar();
            }
        }

        public void Edit(CompanionModel model)
        {
            const string sql = @"
                UPDATE [dbo].[Companions] SET
                    [CompanionName] = @CompanionName,
                    [ContactNumber] = @ContactNumber,
                    [Email] = @Email,
                    [Relationship] = @Relationship,
                    [RoomType] = @RoomType,
                    [RoomNumber] = @RoomNumber,
                    [RoomPrice] = @RoomPrice,
                    [Nights] = @Nights,
                    [CheckInDate] = @CheckInDate,
                    [CheckOutDate] = @CheckOutDate,
                    [TotalCost] = @TotalCost
                WHERE [CompanionId] = @CompanionId";

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@CompanionId", model.CompanionId);
                cmd.Parameters.AddWithValue("@CompanionName", model.CompanionName);
                cmd.Parameters.AddWithValue("@ContactNumber", (object)model.ContactNumber ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Email", (object)model.Email ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Relationship", (object)model.Relationship ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@RoomType", model.RoomType);
                cmd.Parameters.AddWithValue("@RoomNumber", model.RoomNumber);
                cmd.Parameters.AddWithValue("@RoomPrice", model.RoomPrice);
                cmd.Parameters.AddWithValue("@Nights", model.Nights);
                cmd.Parameters.AddWithValue("@CheckInDate", model.CheckInDate);
                cmd.Parameters.AddWithValue("@CheckOutDate", model.CheckOutDate);
                cmd.Parameters.AddWithValue("@TotalCost", model.TotalCost);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int companionId)
        {
            const string sql = "DELETE FROM [dbo].[Companions] WHERE [CompanionId] = @CompanionId";

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@CompanionId", companionId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public CompanionModel GetById(int companionId)
        {
            const string sql = "SELECT * FROM [dbo].[Companions] WHERE [CompanionId] = @CompanionId";

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@CompanionId", companionId);
                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                        return MapFromReader(reader);
                }
            }

            return null;
        }

        public IEnumerable<CompanionModel> GetByReservationId(int reservationId)
        {
            const string sql = @"
                SELECT * FROM [dbo].[Companions] 
                WHERE [MainReservationId] = @ReservationId
                ORDER BY [CreatedAt]";

            var companions = new List<CompanionModel>();

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@ReservationId", reservationId);
                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        companions.Add(MapFromReader(reader));
                    }
                }
            }

            return companions;
        }

        public int GetCompanionCountByReservation(int reservationId)
        {
            const string sql = "SELECT COUNT(*) FROM [dbo].[Companions] WHERE [MainReservationId] = @ReservationId";

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@ReservationId", reservationId);
                conn.Open();
                return (int)cmd.ExecuteScalar();
            }
        }

        public decimal GetTotalCompanionCostByReservation(int reservationId)
        {
            const string sql = @"
                SELECT ISNULL(SUM([TotalCost]), 0) 
                FROM [dbo].[Companions] 
                WHERE [MainReservationId] = @ReservationId";

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@ReservationId", reservationId);
                conn.Open();
                return (decimal)cmd.ExecuteScalar();
            }
        }

        public IEnumerable<CompanionModel> GetAll()
        {
            const string sql = "SELECT * FROM [dbo].[Companions] ORDER BY [CreatedAt] DESC";
            var companions = new List<CompanionModel>();

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        companions.Add(MapFromReader(reader));
                    }
                }
            }

            return companions;
        }

        private CompanionModel MapFromReader(SqlDataReader reader)
        {
            return new CompanionModel
            {
                CompanionId = reader.GetInt32(reader.GetOrdinal("CompanionId")),
                MainReservationId = reader.GetInt32(reader.GetOrdinal("MainReservationId")),
                CompanionName = reader.GetString(reader.GetOrdinal("CompanionName")),
                ContactNumber = reader.IsDBNull(reader.GetOrdinal("ContactNumber")) ? null : reader.GetString(reader.GetOrdinal("ContactNumber")),
                Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? null : reader.GetString(reader.GetOrdinal("Email")),
                Relationship = reader.IsDBNull(reader.GetOrdinal("Relationship")) ? null : reader.GetString(reader.GetOrdinal("Relationship")),
                RoomType = reader.GetString(reader.GetOrdinal("RoomType")),
                RoomNumber = reader.GetString(reader.GetOrdinal("RoomNumber")),
                RoomPrice = reader.GetDecimal(reader.GetOrdinal("RoomPrice")),
                Nights = reader.GetInt32(reader.GetOrdinal("Nights")),
                CheckInDate = reader.GetDateTime(reader.GetOrdinal("CheckInDate")),
                CheckOutDate = reader.GetDateTime(reader.GetOrdinal("CheckOutDate")),
                TotalCost = reader.GetDecimal(reader.GetOrdinal("TotalCost")),
                CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                CreatedBy = reader.IsDBNull(reader.GetOrdinal("CreatedBy")) ? null : reader.GetString(reader.GetOrdinal("CreatedBy"))
            };
        }
    }
}