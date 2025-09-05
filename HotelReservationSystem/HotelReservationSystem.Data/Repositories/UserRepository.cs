using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HotelReservationSystem.Data.Repositories;
using HotelReservationSystem.Domain.Interface;
using HotelReservationSystem.Domain.Model;
using HotelReservationSystem.Infrastructure.Security;

namespace HotelReservationSystem.Infrastructure.Repository
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        private readonly IPasswordHasher passwordHasher;
        public UserRepository(string connectionString, IPasswordHasher passwordHasher) : base(connectionString)
        {
            this.passwordHasher = passwordHasher;
        }


        public void Add(UserModel user)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"INSERT INTO Users (LastName, FirstName, MiddleName, BirthDate, 
                               Username, PasswordHash, Email, Gender, Role, CreatedAt, IsActive)
                               VALUES (@LastName, @FirstName, @MiddleName, @BirthDate, 
                               @Username, @PasswordHash, @Email, @Gender, @Role, @CreatedAt, @IsActive)";

                AddUserParameters(command, user);
                command.ExecuteNonQuery();
            }
        }

        public void Edit(UserModel user)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"UPDATE Users SET LastName = @LastName, FirstName = @FirstName, 
                               MiddleName = @MiddleName, BirthDate = @BirthDate, Username = @Username, 
                               Email = @Email, Gender = @Gender, Role = @Role, IsActive = @IsActive";

                if (!string.IsNullOrEmpty(user.PasswordHash))
                {
                    command.CommandText += ", PasswordHash = @PasswordHash";
                }

                command.CommandText += " WHERE UserId = @UserId";

                AddUserParameters(command, user);
                command.Parameters.AddWithValue("@UserId", user.UserId);
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
                command.CommandText = "DELETE FROM Users WHERE UserId = @UserId";
                command.Parameters.AddWithValue("@UserId", id);
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<UserModel> GetAll()
        {
            var users = new List<UserModel>();
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"SELECT UserId, LastName, FirstName, MiddleName, BirthDate, 
                               Username, PasswordHash, Email, Gender, Role, CreatedAt, IsActive 
                               FROM Users ORDER BY LastName, FirstName";

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    users.Add(MapUserFromReader(reader));
                }
            }
            return users;
        }

        public IEnumerable<UserModel> GetByValue(string value)
        {
            var users = new List<UserModel>();
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"SELECT UserId, LastName, FirstName, MiddleName, BirthDate, 
                               Username, PasswordHash, Email, Gender, Role, CreatedAt, IsActive 
                               FROM Users 
                               WHERE LastName LIKE @Value OR FirstName LIKE @Value OR 
                                     Username LIKE @Value OR Email LIKE @Value OR Role LIKE @Value
                               ORDER BY LastName, FirstName";
                command.Parameters.AddWithValue("@Value", $"%{value}%");

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    users.Add(MapUserFromReader(reader));
                }
            }
            return users;
        }

        public UserModel GetById(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"SELECT UserId, LastName, FirstName, MiddleName, BirthDate, 
                               Username, PasswordHash, Email, Gender, Role, CreatedAt, IsActive 
                               FROM Users WHERE UserId = @UserId";
                command.Parameters.AddWithValue("@UserId", id);

                var reader = command.ExecuteReader();
                return reader.Read() ? MapUserFromReader(reader) : null;
            }
        }

        public UserModel AuthenticateUser(string usernameOrEmail, string password)
        {
            var user = GetByUsernameOrEmail(usernameOrEmail);
            if (user == null) return null;
            

            if (!passwordHasher.VerifyPassword(password, user.PasswordHash)) return null;
            return user;
        }

        public UserModel GetByUsernameOrEmail(string usernameOrEmail)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"SELECT UserId, LastName, FirstName, MiddleName, BirthDate, 
                                       Username, PasswordHash, Email, Gender, Role, CreatedAt, IsActive 
                                       FROM Users WHERE Username = @UsernameOrEmail OR Email = @UsernameOrEmail";
                command.Parameters.AddWithValue("@UsernameOrEmail", usernameOrEmail);

                var reader = command.ExecuteReader();
                return reader.Read() ? MapUserFromReader(reader) : null;
            }
        }
        private UserModel MapUserFromReader(SqlDataReader reader)
        {
            return new UserModel
            {
                UserId = (int)reader["UserId"],
                LastName = reader["LastName"].ToString(),
                FirstName = reader["FirstName"].ToString(),
                MiddleName = reader["MiddleName"]?.ToString(),
                BirthDate = (DateTime)reader["BirthDate"],
                Username = reader["Username"].ToString(),
                PasswordHash = reader["PasswordHash"].ToString(),
                Email = reader["Email"].ToString(),
                Gender = reader["Gender"].ToString(),
                Role = reader["Role"].ToString(),
                CreatedAt = (DateTime)reader["CreatedAt"],
                IsActive = reader["IsActive"] == DBNull.Value || (bool)reader["IsActive"]
            };
        }

        private void AddUserParameters(SqlCommand command, UserModel user)
        {
            command.Parameters.AddWithValue("@LastName", user.LastName);
            command.Parameters.AddWithValue("@FirstName", user.FirstName);
            command.Parameters.AddWithValue("@MiddleName", user.MiddleName ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@BirthDate", user.BirthDate);
            command.Parameters.AddWithValue("@Username", user.Username);
            command.Parameters.AddWithValue("@Email", user.Email);
            command.Parameters.AddWithValue("@Gender", user.Gender);
            command.Parameters.AddWithValue("@Role", user.Role);
            command.Parameters.AddWithValue("@CreatedAt", user.CreatedAt);
            command.Parameters.AddWithValue("@IsActive", user.IsActive);

            if (!string.IsNullOrEmpty(user.PasswordHash))
            {
                command.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
            }
        }
    }
}
