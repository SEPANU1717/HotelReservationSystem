using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using HotelReservationSystem.Domain.Interface.Billing;
using HotelReservationSystem.Domain.Model;

namespace HotelReservationSystem.Data.Repositories
{
    public class BillingRepository : BaseRepository, IBillingRepository
    {
        public BillingRepository(string connectionString) : base(connectionString) { }

        //<-----------------------Add Billing--------------------------/>
        public void Add(BillingModel billing)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = @"
                    INSERT INTO Billing (
                        ReservationId, CustomerName, RoomType, RoomNumber,
                        CheckInDate, CheckOutDate, ActualCheckOutDate,
                        RoomCharge, LateCheckoutFee, DamageFee,
                        AmountPaidBefore, AmountPaidAtCheckout,
                        PaymentStatus, PaymentMethod, PaymentReference,
                        DateBilled, BilledBy
                    )
                    VALUES (
                        @ReservationId, @CustomerName, @RoomType, @RoomNumber,
                        @CheckInDate, @CheckOutDate, @ActualCheckOutDate,
                        @RoomCharge, @LateCheckoutFee, @DamageFee,
                        @AmountPaidBefore, @AmountPaidAtCheckout,
                        @PaymentStatus, @PaymentMethod, @PaymentReference,
                        @DateBilled, @BilledBy
                    )";

                AddBillingParameters(command, billing);
                command.ExecuteNonQuery();
            }
        }

        //<-----------------------Delete Billing--------------------------/>
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

        //<-----------------------Edit Billing--------------------------/>
        public void Edit(BillingModel billing)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = @"
                    UPDATE Billing SET
                        CustomerName = @CustomerName,
                        RoomType = @RoomType,
                        RoomNumber = @RoomNumber,
                        CheckInDate = @CheckInDate,
                        CheckOutDate = @CheckOutDate,
                        ActualCheckOutDate = @ActualCheckOutDate,
                        RoomCharge = @RoomCharge,
                        LateCheckoutFee = @LateCheckoutFee,
                        DamageFee = @DamageFee,
                        AmountPaidBefore = @AmountPaidBefore,
                        AmountPaidAtCheckout = @AmountPaidAtCheckout,
                        PaymentStatus = @PaymentStatus,
                        PaymentMethod = @PaymentMethod,
                        PaymentReference = @PaymentReference,
                        DateBilled = @DateBilled,
                        BilledBy = @BilledBy
                    WHERE BillId = @BillId";

                AddBillingParameters(command, billing);
                command.Parameters.Add("@BillId", SqlDbType.Int).Value = billing.BillId;
                command.ExecuteNonQuery();
            }
        }

        //<-----------------------Get All Billing--------------------------/>
        public IEnumerable<BillingModel> GetAll()
        {
            var billingList = new List<BillingModel>();
            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = "SELECT * FROM Billing ORDER BY DateBilled DESC";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        billingList.Add(MapReaderToModel(reader));
                    }
                }
            }
            return billingList;
        }

        //<-----------------------Get By Value Billing--------------------------/>
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
                    WHERE BillId = @id 
                       OR CustomerName LIKE @value 
                       OR RoomNumber LIKE @value
                    ORDER BY DateBilled DESC";

                command.Parameters.Add("@id", SqlDbType.Int).Value = billId;
                command.Parameters.Add("@value", SqlDbType.NVarChar).Value = $"%{value}%";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        billingList.Add(MapReaderToModel(reader));
                    }
                }
            }
            return billingList;
        }

        //<-----------------------Get By Reservation ID--------------------------/>
        public BillingModel GetByReservationId(int reservationId)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = "SELECT * FROM Billing WHERE ReservationId = @id";
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

        //<-----------------------Get By Bill ID--------------------------/>
        public BillingModel GetById(int billId)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = "SELECT * FROM Billing WHERE BillId = @id";
                command.Parameters.Add("@id", SqlDbType.Int).Value = billId;

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

        //<-----------------------Check if billing exists for reservation--------------------------/>
        public bool ExistsForReservation(int reservationId)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = "SELECT COUNT(*) FROM Billing WHERE ReservationId = @id";
                command.Parameters.Add("@id", SqlDbType.Int).Value = reservationId;
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }

        //<-----------------------Get Next Billing ID--------------------------/>
        public int GetNextBillingId()
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand("SELECT ISNULL(MAX(BillId), 99) + 1 FROM Billing", connection))
            {
                connection.Open();
                return (int)command.ExecuteScalar();
            }
        }

        //<-----------------------Private Helper Methods--------------------------/>
        private void AddBillingParameters(SqlCommand command, BillingModel billing)
        {
            command.Parameters.Add("@ReservationId", SqlDbType.Int).Value = billing.ReservationId;
            command.Parameters.Add("@CustomerName", SqlDbType.NVarChar, 100).Value = billing.CustomerName;
            command.Parameters.Add("@RoomType", SqlDbType.NVarChar, 50).Value = billing.RoomType ?? (object)DBNull.Value;
            command.Parameters.Add("@RoomNumber", SqlDbType.NVarChar, 20).Value = billing.RoomNumber;
            
            // Dates
            command.Parameters.Add("@CheckInDate", SqlDbType.DateTime).Value = billing.CheckInDate;
            command.Parameters.Add("@CheckOutDate", SqlDbType.DateTime).Value = billing.CheckOutDate;
            command.Parameters.Add("@ActualCheckOutDate", SqlDbType.DateTime).Value = billing.ActualCheckOutDate ?? (object)DBNull.Value;
            
            // Charges - Simplified
            command.Parameters.Add("@RoomCharge", SqlDbType.Decimal).Value = billing.RoomCharge;
            command.Parameters.Add("@LateCheckoutFee", SqlDbType.Decimal).Value = billing.LateCheckoutFee;
            command.Parameters.Add("@DamageFee", SqlDbType.Decimal).Value = billing.DamageFee;
            
            // Payment
            command.Parameters.Add("@AmountPaidBefore", SqlDbType.Decimal).Value = billing.AmountPaidBefore;
            command.Parameters.Add("@AmountPaidAtCheckout", SqlDbType.Decimal).Value = billing.AmountPaidAtCheckout;
            command.Parameters.Add("@PaymentStatus", SqlDbType.NVarChar, 20).Value = billing.PaymentStatus ?? (object)DBNull.Value;
            command.Parameters.Add("@PaymentMethod", SqlDbType.NVarChar, 50).Value = billing.PaymentMethod ?? (object)DBNull.Value;
            command.Parameters.Add("@PaymentReference", SqlDbType.NVarChar, 100).Value = billing.PaymentReference ?? (object)DBNull.Value;
            
            // Billing Info
            command.Parameters.Add("@DateBilled", SqlDbType.DateTime).Value = billing.DateBilled;
            command.Parameters.Add("@BilledBy", SqlDbType.NVarChar, 100).Value = billing.BilledBy ?? (object)DBNull.Value;
        }

        private BillingModel MapReaderToModel(SqlDataReader reader)
        {
            return new BillingModel
            {
                BillId = Convert.ToInt32(reader["BillId"]),
                ReservationId = Convert.ToInt32(reader["ReservationId"]),
                CustomerName = reader["CustomerName"].ToString(),
                RoomType = reader["RoomType"] == DBNull.Value ? null : reader["RoomType"].ToString(),
                RoomNumber = reader["RoomNumber"].ToString(),
                
                // Dates
                CheckInDate = reader["CheckInDate"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(reader["CheckInDate"]),
                CheckOutDate = reader["CheckOutDate"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(reader["CheckOutDate"]),
                ActualCheckOutDate = reader["ActualCheckOutDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["ActualCheckOutDate"]),
                
                // Charges - Simplified
                RoomCharge = reader["RoomCharge"] == DBNull.Value ? 0m : Convert.ToDecimal(reader["RoomCharge"]),
                LateCheckoutFee = reader["LateCheckoutFee"] == DBNull.Value ? 0m : Convert.ToDecimal(reader["LateCheckoutFee"]),
                DamageFee = reader["DamageFee"] == DBNull.Value ? 0m : Convert.ToDecimal(reader["DamageFee"]),
                
                // Payment
                AmountPaidBefore = reader["AmountPaidBefore"] == DBNull.Value ? 0m : Convert.ToDecimal(reader["AmountPaidBefore"]),
                AmountPaidAtCheckout = reader["AmountPaidAtCheckout"] == DBNull.Value ? 0m : Convert.ToDecimal(reader["AmountPaidAtCheckout"]),
                PaymentStatus = reader["PaymentStatus"] == DBNull.Value ? null : reader["PaymentStatus"].ToString(),
                PaymentMethod = reader["PaymentMethod"] == DBNull.Value ? null : reader["PaymentMethod"].ToString(),
                PaymentReference = reader["PaymentReference"] == DBNull.Value ? null : reader["PaymentReference"].ToString(),
                
                // Billing Info
                DateBilled = Convert.ToDateTime(reader["DateBilled"]),
                BilledBy = reader["BilledBy"] == DBNull.Value ? null : reader["BilledBy"].ToString()
            };
        }
    }
}
