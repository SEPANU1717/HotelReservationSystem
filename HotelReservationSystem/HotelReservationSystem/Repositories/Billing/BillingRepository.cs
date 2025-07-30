using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelReservationSystem.Interface.Billing;
using HotelReservationSystem.Model.Billing;

namespace HotelReservationSystem.Repositories.Billing
{
    public class BillingRepository : BaseRepository, IBillingRepository
    {
        public BillingRepository(string connectionString) : base(connectionString) { }

        // Add Billing
        public void Add(BillingModel billing)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = @"
                        INSERT INTO Billing (ReservationId, CustomerName, RoomType, RoomNumber, TotalAmount, PaymentStatus, DateBilled)
                        VALUES (@ReservationId, @CustomerName, @RoomType, @RoomNumber, @TotalAmount, @PaymentStatus, @DateBilled)";

                command.Parameters.Add("@ReservationId", SqlDbType.Int).Value = billing.ReservationId;
                command.Parameters.Add("@CustomerName", SqlDbType.NVarChar, 100).Value = billing.CustomerName;
                command.Parameters.Add("@RoomType", SqlDbType.NVarChar, 50).Value = (object)billing.RoomType ?? DBNull.Value;
                command.Parameters.Add("@RoomNumber", SqlDbType.NVarChar, 20).Value = billing.RoomNumber;
                command.Parameters.Add("@TotalAmount", SqlDbType.Decimal).Value = billing.TotalAmount;
                command.Parameters.Add("@PaymentStatus", SqlDbType.NVarChar, 20).Value = (object)billing.PaymentStatus ?? DBNull.Value;
                command.Parameters.Add("@DateBilled", SqlDbType.DateTime).Value = billing.DateBilled;

                command.ExecuteNonQuery();
            }
        }

        // Delete Billing
        public void Delete(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = "DELETE FROM Billing WHERE BillId = @id";
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                command.ExecuteNonQuery();
            }
        }

        // Edit Billing
        public void Edit(BillingModel billing)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = @"
                        UPDATE Billing
                        SET ReservationId = @ReservationId,
                            CustomerName = @CustomerName,
                            RoomType = @RoomType,
                            RoomNumber = @RoomNumber,
                            TotalAmount = @TotalAmount,
                            PaymentStatus = @PaymentStatus,
                            DateBilled = @DateBilled
                        WHERE BillId = @BillId";

                command.Parameters.Add("@ReservationId", SqlDbType.Int).Value = billing.ReservationId;
                command.Parameters.Add("@CustomerName", SqlDbType.NVarChar, 100).Value = billing.CustomerName;
                command.Parameters.Add("@RoomType", SqlDbType.NVarChar, 50).Value = (object)billing.RoomType ?? DBNull.Value;
                command.Parameters.Add("@RoomNumber", SqlDbType.NVarChar, 20).Value = billing.RoomNumber;
                command.Parameters.Add("@TotalAmount", SqlDbType.Decimal).Value = billing.TotalAmount;
                command.Parameters.Add("@PaymentStatus", SqlDbType.NVarChar, 20).Value = (object)billing.PaymentStatus ?? DBNull.Value;
                command.Parameters.Add("@DateBilled", SqlDbType.DateTime).Value = billing.DateBilled;
                command.Parameters.Add("@BillId", SqlDbType.Int).Value = billing.BillId;

                command.ExecuteNonQuery();
            }
        }

        // Get All Billing
        public IEnumerable<BillingModel> GetAll()
        {
            var billingList = new List<BillingModel>();
            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = "SELECT * FROM Billing ORDER BY BillId DESC";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        billingList.Add(new BillingModel
                        {
                            BillId = Convert.ToInt32(reader["BillId"]),
                            ReservationId = Convert.ToInt32(reader["ReservationId"]),
                            CustomerName = reader["CustomerName"].ToString(),
                            RoomType = reader["RoomType"] == DBNull.Value ? null : reader["RoomType"].ToString(),
                            RoomNumber = reader["RoomNumber"].ToString(),
                            TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                            PaymentStatus = reader["PaymentStatus"] == DBNull.Value ? null : reader["PaymentStatus"].ToString(),
                            DateBilled = Convert.ToDateTime(reader["DateBilled"])
                        });
                    }
                }
            }
            return billingList;
        }

        // Get By Value Billing
        public IEnumerable<BillingModel> GetByValue(string value)
        {
            var billingList = new List<BillingModel>();
            int billId = int.TryParse(value, out var id) ? id : 0;

            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = @"
                        SELECT * FROM Billing
                        WHERE BillId = @id OR CustomerName LIKE @name
                        ORDER BY BillId DESC";

                command.Parameters.Add("@id", SqlDbType.Int).Value = billId;
                command.Parameters.Add("@name", SqlDbType.NVarChar, 100).Value = $"%{value}%";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        billingList.Add(new BillingModel
                        {
                            BillId = Convert.ToInt32(reader["BillId"]),
                            ReservationId = Convert.ToInt32(reader["ReservationId"]),
                            CustomerName = reader["CustomerName"].ToString(),
                            RoomType = reader["RoomType"] == DBNull.Value ? null : reader["RoomType"].ToString(),
                            RoomNumber = reader["RoomNumber"].ToString(),
                            TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                            PaymentStatus = reader["PaymentStatus"] == DBNull.Value ? null : reader["PaymentStatus"].ToString(),
                            DateBilled = Convert.ToDateTime(reader["DateBilled"])
                        });
                    }
                }
            }
            return billingList;
        }

        public int GetNextBillingId()
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand("SELECT ISNULL(MAX(BillId), 0) + 1 FROM Billing", connection))
            {
                connection.Open();
                return (int)command.ExecuteScalar();
            }
        }
    }
}
