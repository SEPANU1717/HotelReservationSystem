using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using HotelReservationSystem.Interface.Service.Laundry;
using HotelReservationSystem.Model.Service;

namespace HotelReservationSystem.Repositories.Service.Food
{
    public class LaundryRepository : BaseRepository, ILaundryServiceRepository
    {
        public LaundryRepository(string connectionString) : base(connectionString) { }

        public void Add(SharedAddServiceModel laundry)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"INSERT INTO LaundryItems (ItemName, Quantity, Price) 
                                      VALUES (@ItemName, @Quantity, @Price)";

                command.Parameters.Add("@ItemName", SqlDbType.NVarChar, 100).Value = laundry.ItemName;
                command.Parameters.Add("@Quantity", SqlDbType.Int).Value = laundry.Quantity;
                command.Parameters.Add("@Price", SqlDbType.Decimal).Value = laundry.Price;

                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<SharedAddServiceModel> GetAll()
        {
            var laundryList = new List<SharedAddServiceModel>();
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM LaundryItems ORDER BY Id DESC";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var laundry = new SharedAddServiceModel
                        {
                            //LaundryId = (int)reader["Id"],
                            ItemName = reader["ItemName"].ToString(),
                            Quantity = (int)reader["Quantity"],
                            Price = (decimal)reader["Price"]
                        };
                        laundryList.Add(laundry);
                    }
                }
            }
            return laundryList;
        }

        public int GetNextReservationId()
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand("SELECT ISNULL(MAX(Id), 0) + 1 FROM LaundryItems", connection))
            {
                connection.Open();
                return (int)command.ExecuteScalar();
            }
        }
        public void ClearAll()
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                try
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "DELETE FROM LaundryItems";
                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine($"Deleted {rowsAffected} records from LaundryItems");
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error clearing laundry database: {ex.Message}", ex);
                }
            }
        }
    }
}