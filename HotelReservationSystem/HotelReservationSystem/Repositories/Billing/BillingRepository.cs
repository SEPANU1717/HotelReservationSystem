using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using HotelReservationSystem.Interface.Billing;
using HotelReservationSystem.Model.Billing;

namespace HotelReservationSystem.Repositories.Billing
{
    public class BillingRepository : BaseRepository, IBillingRepository
    {
        public BillingRepository(string connectionString) : base(connectionString) { }

        //<-----------------------Add Bill--------------------------/>
        public void Add(BillingModel billing)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = @"INSERT INTO Billing 
                    (ReservationId, CustomerName, RoomNumber, RoomType, CheckInDate, CheckOutDate, 
                     NumberOfNights, RoomRate, RoomTotal, ServiceCharges, TaxAmount, DiscountAmount, 
                     AdditionalCharges, AdditionalChargesDescription, Subtotal, TotalAmount, 
                     AmountPaid, BalanceDue, PaymentMethod, PaymentStatus, BillDate, DueDate, Notes, CreatedAt, UpdatedAt)
                    VALUES 
                    (@ReservationId, @CustomerName, @RoomNumber, @RoomType, @CheckInDate, @CheckOutDate, 
                     @NumberOfNights, @RoomRate, @RoomTotal, @ServiceCharges, @TaxAmount, @DiscountAmount, 
                     @AdditionalCharges, @AdditionalChargesDescription, @Subtotal, @TotalAmount, 
                     @AmountPaid, @BalanceDue, @PaymentMethod, @PaymentStatus, @BillDate, @DueDate, @Notes, @CreatedAt, @UpdatedAt)";

                command.Parameters.Add("@ReservationId", SqlDbType.Int).Value = billing.ReservationId;
                command.Parameters.Add("@CustomerName", SqlDbType.NVarChar, 100).Value = billing.CustomerName;
                command.Parameters.Add("@RoomNumber", SqlDbType.NVarChar, 10).Value = billing.RoomNumber;
                command.Parameters.Add("@RoomType", SqlDbType.NVarChar, 50).Value = billing.RoomType ?? (object)DBNull.Value;
                command.Parameters.Add("@CheckInDate", SqlDbType.DateTime).Value = billing.CheckInDate;
                command.Parameters.Add("@CheckOutDate", SqlDbType.DateTime).Value = billing.CheckOutDate;
                command.Parameters.Add("@NumberOfNights", SqlDbType.Int).Value = billing.NumberOfNights;
                command.Parameters.Add("@RoomRate", SqlDbType.Decimal).Value = billing.RoomRate;
                command.Parameters.Add("@RoomTotal", SqlDbType.Decimal).Value = billing.RoomTotal;
                command.Parameters.Add("@ServiceCharges", SqlDbType.Decimal).Value = billing.ServiceCharges;
                command.Parameters.Add("@TaxAmount", SqlDbType.Decimal).Value = billing.TaxAmount;
                command.Parameters.Add("@DiscountAmount", SqlDbType.Decimal).Value = billing.DiscountAmount;
                command.Parameters.Add("@AdditionalCharges", SqlDbType.Decimal).Value = billing.AdditionalCharges;
                command.Parameters.Add("@AdditionalChargesDescription", SqlDbType.NVarChar, 500).Value = billing.AdditionalChargesDescription ?? (object)DBNull.Value;
                command.Parameters.Add("@Subtotal", SqlDbType.Decimal).Value = billing.Subtotal;
                command.Parameters.Add("@TotalAmount", SqlDbType.Decimal).Value = billing.TotalAmount;
                command.Parameters.Add("@AmountPaid", SqlDbType.Decimal).Value = billing.AmountPaid;
                command.Parameters.Add("@BalanceDue", SqlDbType.Decimal).Value = billing.BalanceDue;
                command.Parameters.Add("@PaymentMethod", SqlDbType.NVarChar, 50).Value = billing.PaymentMethod ?? (object)DBNull.Value;
                command.Parameters.Add("@PaymentStatus", SqlDbType.NVarChar, 50).Value = billing.PaymentStatus;
                command.Parameters.Add("@BillDate", SqlDbType.DateTime).Value = billing.BillDate;
                command.Parameters.Add("@DueDate", SqlDbType.DateTime).Value = billing.DueDate;
                command.Parameters.Add("@Notes", SqlDbType.NVarChar, 1000).Value = billing.Notes ?? (object)DBNull.Value;
                command.Parameters.Add("@CreatedAt", SqlDbType.DateTime).Value = billing.CreatedAt;
                command.Parameters.Add("@UpdatedAt", SqlDbType.DateTime).Value = billing.UpdatedAt;

                command.ExecuteNonQuery();
            }
        }

        //<-----------------------Edit Bill--------------------------/>
        public void Edit(BillingModel billing)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = @"UPDATE Billing SET 
                    ReservationId = @ReservationId, CustomerName = @CustomerName, RoomNumber = @RoomNumber, 
                    RoomType = @RoomType, CheckInDate = @CheckInDate, CheckOutDate = @CheckOutDate, 
                    NumberOfNights = @NumberOfNights, RoomRate = @RoomRate, RoomTotal = @RoomTotal, 
                    ServiceCharges = @ServiceCharges, TaxAmount = @TaxAmount, DiscountAmount = @DiscountAmount, 
                    AdditionalCharges = @AdditionalCharges, AdditionalChargesDescription = @AdditionalChargesDescription, 
                    Subtotal = @Subtotal, TotalAmount = @TotalAmount, AmountPaid = @AmountPaid, 
                    BalanceDue = @BalanceDue, PaymentMethod = @PaymentMethod, PaymentStatus = @PaymentStatus, 
                    BillDate = @BillDate, DueDate = @DueDate, Notes = @Notes, UpdatedAt = @UpdatedAt
                    WHERE BillId = @BillId";

                command.Parameters.Add("@BillId", SqlDbType.Int).Value = billing.BillId;
                command.Parameters.Add("@ReservationId", SqlDbType.Int).Value = billing.ReservationId;
                command.Parameters.Add("@CustomerName", SqlDbType.NVarChar, 100).Value = billing.CustomerName;
                command.Parameters.Add("@RoomNumber", SqlDbType.NVarChar, 10).Value = billing.RoomNumber;
                command.Parameters.Add("@RoomType", SqlDbType.NVarChar, 50).Value = billing.RoomType ?? (object)DBNull.Value;
                command.Parameters.Add("@CheckInDate", SqlDbType.DateTime).Value = billing.CheckInDate;
                command.Parameters.Add("@CheckOutDate", SqlDbType.DateTime).Value = billing.CheckOutDate;
                command.Parameters.Add("@NumberOfNights", SqlDbType.Int).Value = billing.NumberOfNights;
                command.Parameters.Add("@RoomRate", SqlDbType.Decimal).Value = billing.RoomRate;
                command.Parameters.Add("@RoomTotal", SqlDbType.Decimal).Value = billing.RoomTotal;
                command.Parameters.Add("@ServiceCharges", SqlDbType.Decimal).Value = billing.ServiceCharges;
                command.Parameters.Add("@TaxAmount", SqlDbType.Decimal).Value = billing.TaxAmount;
                command.Parameters.Add("@DiscountAmount", SqlDbType.Decimal).Value = billing.DiscountAmount;
                command.Parameters.Add("@AdditionalCharges", SqlDbType.Decimal).Value = billing.AdditionalCharges;
                command.Parameters.Add("@AdditionalChargesDescription", SqlDbType.NVarChar, 500).Value = billing.AdditionalChargesDescription ?? (object)DBNull.Value;
                command.Parameters.Add("@Subtotal", SqlDbType.Decimal).Value = billing.Subtotal;
                command.Parameters.Add("@TotalAmount", SqlDbType.Decimal).Value = billing.TotalAmount;
                command.Parameters.Add("@AmountPaid", SqlDbType.Decimal).Value = billing.AmountPaid;
                command.Parameters.Add("@BalanceDue", SqlDbType.Decimal).Value = billing.BalanceDue;
                command.Parameters.Add("@PaymentMethod", SqlDbType.NVarChar, 50).Value = billing.PaymentMethod ?? (object)DBNull.Value;
                command.Parameters.Add("@PaymentStatus", SqlDbType.NVarChar, 50).Value = billing.PaymentStatus;
                command.Parameters.Add("@BillDate", SqlDbType.DateTime).Value = billing.BillDate;
                command.Parameters.Add("@DueDate", SqlDbType.DateTime).Value = billing.DueDate;
                command.Parameters.Add("@Notes", SqlDbType.NVarChar, 1000).Value = billing.Notes ?? (object)DBNull.Value;
                command.Parameters.Add("@UpdatedAt", SqlDbType.DateTime).Value = DateTime.Now;

                command.ExecuteNonQuery();
            }
        }

        //<-----------------------Delete Bill--------------------------/>
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

        //<-----------------------Get All Bills--------------------------/>
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
                        billingList.Add(MapReaderToBillingModel(reader));
                    }
                }
            }
            return billingList;
        }

        //<-----------------------Get Bill By Value--------------------------/>
        public IEnumerable<BillingModel> GetByValue(string value)
        {
            var billingList = new List<BillingModel>();
            int billId = int.TryParse(value, out _) ? Convert.ToInt32(value) : 0;

            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = @"SELECT * FROM Billing 
                    WHERE BillId = @id OR CustomerName LIKE @value OR RoomNumber LIKE @value OR PaymentStatus LIKE @value
                    ORDER BY BillId DESC";

                command.Parameters.Add("@id", SqlDbType.Int).Value = billId;
                command.Parameters.Add("@value", SqlDbType.NVarChar).Value = $"%{value}%";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        billingList.Add(MapReaderToBillingModel(reader));
                    }
                }
            }
            return billingList;
        }

        //<-----------------------Get Bill By ID--------------------------/>
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
                        return MapReaderToBillingModel(reader);
                    }
                }
            }
            return null;
        }

        //<-----------------------Get Bill By Reservation ID--------------------------/>
        public BillingModel GetByReservationId(int reservationId)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = "SELECT * FROM Billing WHERE ReservationId = @reservationId";
                command.Parameters.Add("@reservationId", SqlDbType.Int).Value = reservationId;

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return MapReaderToBillingModel(reader);
                    }
                }
            }
            return null;
        }

        //<-----------------------Get Next Bill ID--------------------------/>
        public int GetNextBillId()
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand("SELECT ISNULL(MAX(BillId), 0) + 1 FROM Billing", connection))
            {
                connection.Open();
                return (int)command.ExecuteScalar();
            }
        }

        //<-----------------------Get Bills By Payment Status--------------------------/>
        public IEnumerable<BillingModel> GetByPaymentStatus(string paymentStatus)
        {
            var billingList = new List<BillingModel>();
            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = "SELECT * FROM Billing WHERE PaymentStatus = @status ORDER BY BillId DESC";
                command.Parameters.Add("@status", SqlDbType.NVarChar).Value = paymentStatus;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        billingList.Add(MapReaderToBillingModel(reader));
                    }
                }
            }
            return billingList;
        }

        //<-----------------------Get Bills By Date Range--------------------------/>
        public IEnumerable<BillingModel> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            var billingList = new List<BillingModel>();
            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = "SELECT * FROM Billing WHERE BillDate BETWEEN @startDate AND @endDate ORDER BY BillId DESC";
                command.Parameters.Add("@startDate", SqlDbType.DateTime).Value = startDate;
                command.Parameters.Add("@endDate", SqlDbType.DateTime).Value = endDate;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        billingList.Add(MapReaderToBillingModel(reader));
                    }
                }
            }
            return billingList;
        }

        //<-----------------------Update Payment Status--------------------------/>
        public void UpdatePaymentStatus(int billId, string paymentStatus, decimal amountPaid)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = @"UPDATE Billing SET 
                    PaymentStatus = @paymentStatus, 
                    AmountPaid = @amountPaid, 
                    BalanceDue = TotalAmount - @amountPaid,
                    UpdatedAt = @updatedAt
                    WHERE BillId = @billId";

                command.Parameters.Add("@billId", SqlDbType.Int).Value = billId;
                command.Parameters.Add("@paymentStatus", SqlDbType.NVarChar).Value = paymentStatus;
                command.Parameters.Add("@amountPaid", SqlDbType.Decimal).Value = amountPaid;
                command.Parameters.Add("@updatedAt", SqlDbType.DateTime).Value = DateTime.Now;

                command.ExecuteNonQuery();
            }
        }

        //<-----------------------Helper Method to Map Reader to Model--------------------------/>
        private BillingModel MapReaderToBillingModel(SqlDataReader reader)
        {
            return new BillingModel
            {
                BillId = Convert.ToInt32(reader["BillId"]),
                ReservationId = Convert.ToInt32(reader["ReservationId"]),
                CustomerName = reader["CustomerName"].ToString(),
                RoomNumber = reader["RoomNumber"].ToString(),
                RoomType = reader["RoomType"].ToString(),
                CheckInDate = Convert.ToDateTime(reader["CheckInDate"]),
                CheckOutDate = Convert.ToDateTime(reader["CheckOutDate"]),
                NumberOfNights = Convert.ToInt32(reader["NumberOfNights"]),
                RoomRate = Convert.ToDecimal(reader["RoomRate"]),
                RoomTotal = Convert.ToDecimal(reader["RoomTotal"]),
                ServiceCharges = Convert.ToDecimal(reader["ServiceCharges"]),
                TaxAmount = Convert.ToDecimal(reader["TaxAmount"]),
                DiscountAmount = Convert.ToDecimal(reader["DiscountAmount"]),
                AdditionalCharges = Convert.ToDecimal(reader["AdditionalCharges"]),
                AdditionalChargesDescription = reader["AdditionalChargesDescription"]?.ToString(),
                Subtotal = Convert.ToDecimal(reader["Subtotal"]),
                TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                AmountPaid = Convert.ToDecimal(reader["AmountPaid"]),
                BalanceDue = Convert.ToDecimal(reader["BalanceDue"]),
                PaymentMethod = reader["PaymentMethod"]?.ToString(),
                PaymentStatus = reader["PaymentStatus"].ToString(),
                BillDate = Convert.ToDateTime(reader["BillDate"]),
                DueDate = Convert.ToDateTime(reader["DueDate"]),
                Notes = reader["Notes"]?.ToString(),
                CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                UpdatedAt = Convert.ToDateTime(reader["UpdatedAt"])
            };
        }
    }
}