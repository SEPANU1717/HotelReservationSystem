using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HotelReservationSystem.Interface.Service.Food;
using HotelReservationSystem.Model.Service;

namespace HotelReservationSystem.Repositories.Service.Food
{
    public class FoodOrderRepository : BaseRepository, IOrderFoodRepository
    {
        public FoodOrderRepository(string connectionString) : base(connectionString) { }

        public void Add(SharedAddServiceModel food)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"INSERT INTO OrderFood (ItemName, Quantity, Price) 
                                        VALUES (@ItemName, @Quantity, @Price)";

                command.Parameters.AddWithValue("@ItemName", food.ItemName);
                command.Parameters.AddWithValue("@Quantity", food.Quantity);
                command.Parameters.AddWithValue("@Price", food.Price);

                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<SharedAddServiceModel> GetAll()
        {
            var foodList = new List<SharedAddServiceModel>();
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM OrderFood ORDER BY Id DESC";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var food = new SharedAddServiceModel
                        {
                            ItemName = reader["ItemName"].ToString(),
                            Quantity = (int)reader["Quantity"],
                            Price = (decimal)reader["Price"]
                        };
                        foodList.Add(food);
                    }
                }
            }
            return foodList;
        }

        public int GetStock(string itemName)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"SELECT ISNULL(Stock, 0) FROM FoodStock WHERE FoodName = @ItemName";
                command.Parameters.AddWithValue("@ItemName", itemName);

                var result = command.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : 0;
            }
        }

        // Clear only a specific food item
        public void ClearItem(string itemName)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "DELETE FROM OrderFood WHERE ItemName = @ItemName";
                command.Parameters.AddWithValue("@ItemName", itemName);
                command.ExecuteNonQuery();
            }
        }

        
        public void ClearAll()
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "DELETE FROM OrderFood";
                command.ExecuteNonQuery();
            }
        }
    }
}