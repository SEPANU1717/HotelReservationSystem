using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using HotelReservationSystem.Domain.Interface.Customer;
using HotelReservationSystem.Domain.Model;

namespace HotelReservationSystem.Data.Repositories
{
    public class CustomerRepository : BaseRepository, ICustomerRepository
    {
        public CustomerRepository(string connectionString) : base(connectionString) { }


        //<-----------------------Add Customer--------------------------/>
        public void Add(CustomerModel customer)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = @"INSERT INTO Customers 
                    (FirstName, LastName, MiddleName, IDType, Contact, Address, Email, DateOfBirth, Gender, Nationality, Notes)
                    VALUES (@FirstName, @LastName, @MiddleName, @IDType, @Contact, @Address, @Email, @DateOfBirth, @Gender, @Nationality, @Notes)";

                    AddCustomerParameters(command, customer);
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Database error occurred while adding a customer.", ex);
            }
        }

        //<-----------------------Delete Curtomer--------------------------/>
        public void Delete(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "DELETE FROM Customers WHERE CustomerID = @CustomerID";
                command.Parameters.AddWithValue("@CustomerID", id);
                command.ExecuteNonQuery();
            }
        }

        //<-----------------------Edit Customer--------------------------/>

        public void Edit(CustomerModel customer)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"UPDATE Customers SET 
                    FirstName = @FirstName, LastName = @LastName, MiddleName = @MiddleName, IDType = @IDType, 
                    Contact = @Contact, Address = @Address, Email = @Email, DateOfBirth = @DateOfBirth, 
                    Gender = @Gender, Nationality = @Nationality, Notes = @Notes
                    WHERE CustomerID = @CustomerID";

                AddCustomerParameters(command, customer);
                command.Parameters.AddWithValue("@CustomerID", customer.CustomerID);
                command.ExecuteNonQuery();
            }
        }

        //<-----------------------Get All Customer--------------------------/>

        public IEnumerable<CustomerModel> GetAll()
        {
            var customerList = new List<CustomerModel>();
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                // Order customers by Created date if available, otherwise by CustomerID DESC
                command.CommandText = "SELECT CustomerID, FirstName, LastName, MiddleName, IDType, Contact, Address, Email, DateOfBirth, Gender, Nationality, Notes FROM Customers ORDER BY CustomerID DESC";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        customerList.Add(MapCustomerFromReader(reader));
                    }
                }
            }
            return customerList;
        }

        //<-----------------------Get By Value--------------------------/>

        public IEnumerable<CustomerModel> GetByValue(string value)
        {
            var customerList = new List<CustomerModel>();
            int customerId = int.TryParse(value, out _) ? Convert.ToInt32(value) : 0;

            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"SELECT CustomerID, FirstName, LastName, MiddleName, IDType, Contact, Address, Email, DateOfBirth, Gender, Nationality, Notes 
                    FROM Customers WHERE CustomerID = @CustomerID OR LastName LIKE @LastName ORDER BY CustomerID DESC";

                command.Parameters.AddWithValue("@CustomerID", customerId);
                command.Parameters.AddWithValue("@LastName", $"%{value}%");

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        customerList.Add(MapCustomerFromReader(reader));
                    }
                }
            }
            return customerList;
        }

        private CustomerModel MapCustomerFromReader(SqlDataReader reader)
        {
            return new CustomerModel
            {
                CustomerID = reader["CustomerID"] != DBNull.Value ? Convert.ToInt32(reader["CustomerID"]) : 0,
                FirstName = reader["FirstName"]?.ToString(),
                LastName = reader["LastName"]?.ToString(),
                MiddleName = reader["MiddleName"]?.ToString(),
                IDType = reader["IDType"]?.ToString(),
                Contact = reader["Contact"]?.ToString(),
                Address = reader["Address"]?.ToString(),
                Email = reader["Email"]?.ToString(),
                DateOfBirth = reader["DateOfBirth"] != DBNull.Value ? (DateTime?)reader["DateOfBirth"] : null,
                Gender = reader["Gender"]?.ToString(),
                Nationality = reader["Nationality"]?.ToString(),
                Notes = reader["Notes"]?.ToString()
            };
        }

        private void AddCustomerParameters(SqlCommand command, CustomerModel customer)
        {
            command.Parameters.AddWithValue("@FirstName", customer.FirstName);
            command.Parameters.AddWithValue("@LastName", customer.LastName);
            command.Parameters.AddWithValue("@MiddleName", (object)customer.MiddleName ?? DBNull.Value);
            command.Parameters.AddWithValue("@IDType", customer.IDType);
            command.Parameters.AddWithValue("@Contact", customer.Contact);
            command.Parameters.AddWithValue("@Address", customer.Address);
            command.Parameters.AddWithValue("@Email", customer.Email);
            command.Parameters.AddWithValue("@DateOfBirth", (object)customer.DateOfBirth ?? DBNull.Value);
            command.Parameters.AddWithValue("@Gender", (object)customer.Gender ?? DBNull.Value);
            command.Parameters.AddWithValue("@Nationality", (object)customer.Nationality ?? DBNull.Value);
            command.Parameters.AddWithValue("@Notes", (object)customer.Notes ?? DBNull.Value);
        }


        //<-----------------------Get Next Customer Id--------------------------/>

        public int GetNextCustomerId()
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand("SELECT ISNULL(MAX(CustomerID), 0) + 1 FROM Customers", connection))
            {
                connection.Open();
                return (int)command.ExecuteScalar();
            }
        }

        public IEnumerable<string> GetCustomerNamesWithoutReservation()
        {
            var names = new List<string>();
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @" SELECT DISTINCT (FirstName + ' ' + LastName) AS FullName
                                         FROM Customers WHERE (FirstName + ' ' + LastName) NOT IN (
                                         SELECT CustomerName FROM Reservations WHERE ReservationStatus = 'Reserved')";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        names.Add(reader.GetString(0));
                    }
                }
            }
            return names;
        }

        public CustomerModel GetByCustomerName(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                return null;

            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"SELECT TOP 1 * FROM Customers 
                                       WHERE (FirstName + ' ' + LastName) = @FullName 
                                       OR (FirstName + ' ' + ISNULL(MiddleName + ' ', '') + LastName) = @FullName
                                       ORDER BY CustomerID DESC";
                command.Parameters.AddWithValue("@FullName", fullName);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return MapCustomerFromReader(reader);
                    }
                }
            }
            return null;
        }
    }
}
