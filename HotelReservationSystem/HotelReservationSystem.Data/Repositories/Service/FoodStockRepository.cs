using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using HotelReservationSystem.Domain.Interface.Service.Food;
using HotelReservationSystem.Domain.Model.Service;

namespace HotelReservationSystem.Data.Repositories.Service
{
    public class FoodStockRepository : BaseRepository, IFoodStockRepository
    {
        public FoodStockRepository(string connectionString) : base(connectionString)
        {
        }

        //<-----------------------Add New Stock Food--------------------------/>
        public void Add(FoodStockModel foodStock)
        {
            string foodNameTrimmed = foodStock.FoodName.Trim();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var checkCommand = connection.CreateCommand())
                {
                    checkCommand.CommandText = "SELECT COUNT(*) FROM FoodStock WHERE UPPER(LTRIM(RTRIM(FoodName))) = UPPER(@name)";
                    checkCommand.Parameters.Add("@name", SqlDbType.NVarChar).Value = foodNameTrimmed;

                    int count = (int)checkCommand.ExecuteScalar();
                    if (count > 0)
                        throw new Exception($"Food '{foodNameTrimmed}' already exists.");
                }

                using (var insertCommand = connection.CreateCommand())
                {
                    insertCommand.CommandText = @"INSERT INTO FoodStock 
                (FoodName, Description, Price, Stock)  
                VALUES (@name, @desc, @price, @stock)";
                    insertCommand.Parameters.Add("@name", SqlDbType.NVarChar).Value = foodNameTrimmed;
                    insertCommand.Parameters.Add("@desc", SqlDbType.NVarChar).Value = foodStock.Description ?? (object)DBNull.Value;
                    insertCommand.Parameters.Add("@price", SqlDbType.Decimal).Value = foodStock.Price;
                    insertCommand.Parameters.Add("@stock", SqlDbType.Int).Value = foodStock.Stock;

                    insertCommand.ExecuteNonQuery();
                }
            }
        }


        //<-----------------------Delete Food--------------------------/>
        public void Delete(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "DELETE FROM FoodStock WHERE FoodId = @id";
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                command.ExecuteNonQuery();
            }
        }

        //<-----------------------Edit Food--------------------------/>
        public void Edit(FoodStockModel foodStock)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"UPDATE FoodStock 
                        SET FoodName = @name,
                            Description = @desc,
                            Price = @price,
                            Stock = @stock
                        WHERE FoodId = @id";

                command.Parameters.Add("@id", SqlDbType.Int).Value = foodStock.FoodId;
                command.Parameters.Add("@name", SqlDbType.NVarChar).Value = foodStock.FoodName;
                command.Parameters.Add("@desc", SqlDbType.NVarChar).Value = foodStock.Description ?? (object)DBNull.Value;
                command.Parameters.Add("@price", SqlDbType.Decimal).Value = foodStock.Price;
                command.Parameters.Add("@stock", SqlDbType.Int).Value = foodStock.Stock;
                command.ExecuteNonQuery();
            }
        }

        //<-----------------------Get All Food--------------------------/>
        public IEnumerable<FoodStockModel> GetAll()
        {
            var foodList = new List<FoodStockModel>();
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM FoodStock ORDER BY FoodId DESC";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var food = new FoodStockModel
                        {
                            FoodId = (int)reader["FoodId"],
                            FoodName = reader["FoodName"].ToString(),
                            Description = reader["Description"].ToString(),
                            Price = (decimal)reader["Price"],
                            Stock = (int)reader["Stock"]
                        };
                        foodList.Add(food);
                    }
                }
            }
            return foodList;
        }

        //<-----------------------Get By Value Food--------------------------/>
        public IEnumerable<FoodStockModel> GetByValue(string value)
        {
            var foodList = new List<FoodStockModel>();
            int foodId = int.TryParse(value, out _) ? Convert.ToInt32(value) : 0;
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"SELECT * FROM FoodStock WHERE FoodId = @id OR FoodName LIKE @name
                                          ORDER BY FoodId DESC";
                command.Parameters.Add("@id", SqlDbType.Int).Value = foodId;
                command.Parameters.Add("@name", SqlDbType.NVarChar).Value = $"%{value}%";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        foodList.Add(new FoodStockModel
                        {
                            FoodId = (int)reader["FoodId"],
                            FoodName = reader["FoodName"].ToString(),
                            Description = reader["Description"].ToString(),
                            Price = (decimal)reader["Price"],
                            Stock = (int)reader["Stock"]
                        });
                    }
                }
            }
            return foodList;
        }

        //<-----------------------Get Next Food Id--------------------------/>
        public int GetNextFoodId()
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand("SELECT ISNULL(MAX(FoodId), 0) + 1 FROM FoodStock", connection))
            {
                connection.Open();
                return (int)command.ExecuteScalar();
            }
        }
    }
}