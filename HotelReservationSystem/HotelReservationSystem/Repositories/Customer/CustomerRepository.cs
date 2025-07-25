using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelReservationSystem.Interface;
using HotelReservationSystem.Model.Customer;

namespace HotelReservationSystem.Repositories
{
    public class CustomerRepository : BaseRepository, ICustomerRepository
    {
        public CustomerRepository(string connectionString)
        {
            this.connectionString = connectionString;   
        }

        public void Add(CustomerModel customer)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "INSERT INTO Customers values(@name, @idType, @contact, @address)";
                command.Parameters.Add("@name", SqlDbType.NVarChar).Value = customer.Name;
                command.Parameters.Add("@idType", SqlDbType.NVarChar).Value = customer.IDType;
                command.Parameters.Add("@contact", SqlDbType.NVarChar).Value = customer.Contact;
                command.Parameters.Add("@address", SqlDbType.NVarChar).Value = customer.Address;
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "DELETE FROM Customers WHERE CustomerID = @id";
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                command.ExecuteNonQuery();
            }
        }

        public void Edit(CustomerModel customer)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"UPDATE Customers 
                                        SET Name = @name, IDType = @type, Contact = @contact, Address = @address 
                                        where CustomerID = @id";
                command.Parameters.Add("@name", SqlDbType.NVarChar).Value = customer.Name;
                command.Parameters.Add("@type", SqlDbType.NVarChar).Value = customer.IDType;
                command.Parameters.Add("@contact", SqlDbType.NVarChar).Value = customer.Contact;
                command.Parameters.Add("@address", SqlDbType.NVarChar).Value = customer.Address;

                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<CustomerModel> GetAll()
        {
            var customerList = new List<CustomerModel>();

            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM Customers order by CustomerID desc";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var customerModel = new CustomerModel();
                        customerModel.CustomerID = (int)(reader[0]);
                        customerModel.Name = reader[1].ToString();
                        customerModel.IDType = reader[2].ToString();
                        customerModel.Contact = reader[3].ToString();
                        customerModel.Address = reader[4].ToString();
                        customerList.Add(customerModel);
                    }
                }
            }

            return customerList;
        }

        public IEnumerable<CustomerModel> GetByValue(string value)
        {
            var customerList = new List<CustomerModel>();
            int customerId = int.TryParse(value, out _) ? Convert.ToInt32(value) : 0;

            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM Customers WHERE CustomerID = @id OR Name LIKE @name ORDER BY CustomerID DESC";
                command.Parameters.Add("@id", SqlDbType.Int).Value = customerId;
                command.Parameters.Add("@name", SqlDbType.NVarChar).Value = $"%{value}%";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        customerList.Add(new CustomerModel
                        {
                            CustomerID = Convert.ToInt32(reader["CustomerID"]),
                            Name = reader["Name"].ToString(),
                            IDType = reader["IDType"].ToString(),
                            Contact = reader["Contact"].ToString(),
                            Address = reader["Address"].ToString()
                        });
                    }
                }
            }
            return customerList;
        }

    }
}
