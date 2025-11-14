using System;
using System.Data.SqlClient;
using HotelReservationSystem.Domain.Model;

namespace HotelReservationSystem.Data.Repositories
{
    public class PasswordResetRepository : BaseRepository
    {
        public PasswordResetRepository(string connectionString) : base(connectionString) { }

        public void SaveToken(PasswordResetToken token)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"
                    INSERT INTO PasswordResetTokens 
                    (UsernameOrEmail, Token, ExpiryDate, IsUsed, CreatedAt)
                    VALUES (@UsernameOrEmail, @Token, @ExpiryDate, 0, GETDATE())";

                command.Parameters.AddWithValue("@UsernameOrEmail", token.UsernameOrEmail);
                command.Parameters.AddWithValue("@Token", token.Token);
                command.Parameters.AddWithValue("@ExpiryDate", token.ExpiryDate);

                command.ExecuteNonQuery();
            }
        }

        public PasswordResetToken GetValidToken(string usernameOrEmail, string token)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"
                    SELECT TOP 1 * FROM PasswordResetTokens 
                    WHERE UsernameOrEmail = @UsernameOrEmail 
                    AND Token = @Token 
                    AND IsUsed = 0 
                    AND ExpiryDate > GETDATE()
                    ORDER BY CreatedAt DESC";

                command.Parameters.AddWithValue("@UsernameOrEmail", usernameOrEmail);
                command.Parameters.AddWithValue("@Token", token);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new PasswordResetToken
                        {
                            TokenId = (int)reader["TokenId"],
                            UsernameOrEmail = reader["UsernameOrEmail"].ToString(),
                            Token = reader["Token"].ToString(),
                            ExpiryDate = (DateTime)reader["ExpiryDate"],
                            IsUsed = (bool)reader["IsUsed"],
                            CreatedAt = (DateTime)reader["CreatedAt"]
                        };
                    }
                }
            }
            return null;
        }

        public void MarkTokenAsUsed(int tokenId)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "UPDATE PasswordResetTokens SET IsUsed = 1 WHERE TokenId = @TokenId";
                command.Parameters.AddWithValue("@TokenId", tokenId);
                command.ExecuteNonQuery();
            }
        }

        public void CleanupExpiredTokens()
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"
                    DELETE FROM PasswordResetTokens 
                    WHERE ExpiryDate < DATEADD(day, -1, GETDATE()) 
                    OR (IsUsed = 1 AND CreatedAt < DATEADD(day, -7, GETDATE()))";
                command.ExecuteNonQuery();
            }
        }

        public string GenerateSecureToken()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }
    }
}