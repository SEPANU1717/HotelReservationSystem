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
        public CustomerRepository(string connectionString) : base(connectionString) { }


        //<-----------------------Add Customer--------------------------/>
        public void Add(CustomerModel customer)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            using (var insertCommand = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = "SELECT COUNT(*) FROM Customers WHERE LastName = @lname";
                command.Parameters.Add("@lname", SqlDbType.NVarChar).Value = customer.LastName;

                int count = (int)command.ExecuteScalar();
                if (count > 0) throw new Exception($"Customer last name '{customer.LastName}' already exists.");

                insertCommand.Connection = connection;
                insertCommand.CommandText = @"INSERT INTO Customers 
                                        (FirstName, LastName, IDType, Contact, Address)
                                        VALUES (@fname, @lname, @idType, @contact, @address)";

                insertCommand.Parameters.Add("@fname", SqlDbType.NVarChar).Value = customer.FirstName;
                insertCommand.Parameters.Add("@lname", SqlDbType.NVarChar).Value = customer.LastName;
                insertCommand.Parameters.Add("@idType", SqlDbType.NVarChar).Value = customer.IDType;
                insertCommand.Parameters.Add("@contact", SqlDbType.NVarChar).Value = customer.Contact;
                insertCommand.Parameters.Add("@address", SqlDbType.NVarChar).Value = customer.Address;
                insertCommand.ExecuteNonQuery();
            }
        }

        //<-----------------------Delete Curstomer--------------------------/>
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

        //<-----------------------Edit Customer--------------------------/>

        public void Edit(CustomerModel customer)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"UPDATE Customers 
                                        SET FirstName = @fname, Lastname = @lname, IDType = @type, Contact = @contact, Address = @address 
                                        where CustomerID = @id";

                command.Parameters.Add("@id", SqlDbType.Int).Value = customer.CustomerID;
                command.Parameters.Add("@fname", SqlDbType.NVarChar).Value = customer.FirstName;
                command.Parameters.Add("@lname", SqlDbType.NVarChar).Value = customer.LastName;
                command.Parameters.Add("@type", SqlDbType.NVarChar).Value = customer.IDType;
                command.Parameters.Add("@contact", SqlDbType.NVarChar).Value = customer.Contact;
                command.Parameters.Add("@address", SqlDbType.NVarChar).Value = customer.Address;

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
                command.CommandText = "SELECT * FROM Customers order by CustomerID desc";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var customerModel = new CustomerModel();
                        customerModel.CustomerID = (int)(reader[0]);
                        customerModel.FirstName = reader[1].ToString();
                        customerModel.LastName = reader[2].ToString();
                        customerModel.IDType = reader[3].ToString();
                        customerModel.Contact = reader[4].ToString();
                        customerModel.Address = reader[5].ToString();
                        customerList.Add(customerModel);
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
                command.CommandText = "SELECT * FROM Customers WHERE CustomerID = @id OR LastName LIKE @lname ORDER BY CustomerID DESC";

                command.Parameters.Add("@id", SqlDbType.Int).Value = customerId;
                command.Parameters.Add("@lname", SqlDbType.NVarChar).Value = $"%{value}%";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        customerList.Add(new CustomerModel
                        {
                            CustomerID = Convert.ToInt32(reader[0]),
                            FirstName = reader[1].ToString(),
                            LastName = reader[2].ToString(),
                            IDType = reader[3].ToString(),
                            Contact = reader[4].ToString(),
                            Address = reader[5].ToString()
                        });
                    }
                }
            }
            return customerList;
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
    }
}
