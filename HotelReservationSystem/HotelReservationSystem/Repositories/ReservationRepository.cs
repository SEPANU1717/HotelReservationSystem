using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelReservationSystem.Interface.Reservation;
using HotelReservationSystem.Model.Reservation;

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

                int count = (int)checkCommand.ExecuteNonQuery();
                if (count > 0) throw new Exception($"Customer id '{reservation.CustomerId}' already exists.");

                insertCommand.Parameters.Add("@ReservationId", SqlDbType.Int).Value = reservation.ReservationId;
                insertCommand.Parameters.Add("@CustomerId", SqlDbType.Int).Value = reservation.CustomerId;
                insertCommand.Parameters.Add("@RoomId", SqlDbType.Int).Value = reservation.RoomId;
                insertCommand.Parameters.Add("@CheckInDate", SqlDbType.DateTime).Value = reservation.CheckInDate;
                insertCommand.Parameters.Add("@CheckOutDate", SqlDbType.DateTime).Value = reservation.CheckOutDate;
                insertCommand.Parameters.Add("@TotalAmount", SqlDbType.Decimal).Value = reservation.TotalAmount;
                insertCommand.Parameters.Add("@ReservationStatus", SqlDbType.VarChar).Value = reservation.ReservationStatus;

                insertCommand.ExecuteNonQuery();
            }

        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Edit(ReservationModel reservation)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ReservationModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ReservationModel> GetByValue(string value)
        {
            throw new NotImplementedException();
        }
    }
}
