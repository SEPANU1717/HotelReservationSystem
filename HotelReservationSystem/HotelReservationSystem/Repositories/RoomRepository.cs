using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelReservationSystem.Interface.Rooms;
using HotelReservationSystem.Model.Customer;
using HotelReservationSystem.Model.Rooms;

namespace HotelReservationSystem.Repositories.Rooms
{
    public class RoomRepository : BaseRepository, IRoomRepository
    {
        public RoomRepository(string connectionString) : base(connectionString) { }

        //<-----------------------Add Room--------------------------/>
        public void Add(RoomModel room)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var checkCommand = connection.CreateCommand())
            using (var insertCommand = connection.CreateCommand())
            {
                connection.Open();
                checkCommand.CommandText = "SELECT COUNT(*) FROM Rooms WHERE RoomNumber = @number";
                checkCommand.Parameters.Add("@number", SqlDbType.NVarChar).Value = room.RoomNumber;

                int count = (int)checkCommand.ExecuteScalar();

                if (count > 0) throw new Exception($"Room number '{room.RoomNumber}' already exists.");

                insertCommand.Connection = connection;
                insertCommand.CommandText = @"INSERT INTO Rooms 
                        (RoomNumber, RoomType, RoomStatus, RoomPrice, BedCount, MaxGuests, RoomDescription)  
                        VALUES (@number, @type, @status, @price, @bed, @guest, @description)";

                insertCommand.Parameters.Add("@number", SqlDbType.NVarChar).Value = room.RoomNumber;
                insertCommand.Parameters.Add("@type", SqlDbType.NVarChar).Value = room.RoomType;
                insertCommand.Parameters.Add("@status", SqlDbType.NVarChar).Value = room.RoomStatus;
                insertCommand.Parameters.Add("@price", SqlDbType.Decimal).Value = decimal.Parse(room.RoomPrice);
                insertCommand.Parameters.Add("@bed", SqlDbType.Int).Value = int.Parse(room.BedCount);
                insertCommand.Parameters.Add("@guest", SqlDbType.Int).Value = int.Parse(room.RoomGuests);
                insertCommand.Parameters.Add("@description", SqlDbType.NVarChar).Value = room.RoomDescription ?? (object)DBNull.Value;
                insertCommand.ExecuteNonQuery();
            }
        }

        //<-----------------------Delete Room--------------------------/>
        public void Delete(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "DELETE FROM Rooms WHERE RoomId = @id";
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                command.ExecuteNonQuery();
            }
        }

        //<-----------------------Edit Room--------------------------/>
        public void Edit(RoomModel room)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"UPDATE Rooms 
                        SET RoomNumber = @number,
                            RoomType = @type,
                            RoomStatus = @status,
                            RoomPrice = @price,
                            BedCount = @bed,
                            MaxGuests = @guest,
                            RoomDescription = @description
                        WHERE RoomId = @id";

                command.Parameters.Add("@id", SqlDbType.Int).Value = room.RoomId;
                command.Parameters.Add("@number", SqlDbType.NVarChar).Value = room.RoomNumber;
                command.Parameters.Add("@type", SqlDbType.NVarChar).Value = room.RoomType;
                command.Parameters.Add("@status", SqlDbType.NVarChar).Value = room.RoomStatus;
                command.Parameters.Add("@price", SqlDbType.Decimal).Value = decimal.Parse(room.RoomPrice);
                command.Parameters.Add("@bed", SqlDbType.Int).Value = int.Parse(room.BedCount);
                command.Parameters.Add("@guest", SqlDbType.Int).Value = int.Parse(room.RoomGuests);
                command.Parameters.Add("@description", SqlDbType.NVarChar).Value = room.RoomDescription ?? (object)DBNull.Value;
                command.ExecuteNonQuery();
            }
        }

        //<-----------------------Get All Room--------------------------/>
        public IEnumerable<RoomModel> GetAll()
        {
            var roomList = new List<RoomModel>();
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM Rooms ORDER BY RoomId DESC";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var roomModel = new RoomModel();
                        roomModel.RoomId = (int)reader["RoomId"];
                        roomModel.RoomNumber = reader["RoomNumber"].ToString();
                        roomModel.RoomType = reader["RoomType"].ToString();
                        roomModel.RoomStatus = reader["RoomStatus"].ToString();
                        roomModel.RoomPrice = reader["RoomPrice"].ToString();
                        roomModel.BedCount = reader["BedCount"].ToString();
                        roomModel.RoomGuests = reader["MaxGuests"].ToString();
                        roomModel.RoomDescription = reader["RoomDescription"].ToString();
                        roomList.Add(roomModel);
                    }
                }
            }
            return roomList;
        }

        //<-----------------------Get by Value--------------------------/>
        public IEnumerable<RoomModel> GetByValue(string value)
        {
            var roomList = new List<RoomModel>();
            int roomId = int.TryParse(value, out _) ? Convert.ToInt32(value) : 0;
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"SELECT * FROM Rooms WHERE RoomId = @id OR RoomStatus LIKE @status
                                          ORDER BY RoomId DESC";
                command.Parameters.Add("@id", SqlDbType.Int).Value = roomId;
                command.Parameters.Add("@status", SqlDbType.NVarChar).Value = $"%{value}%";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        roomList.Add(new RoomModel
                        {
                            RoomId = Convert.ToInt32(reader["RoomId"]),
                            RoomNumber = reader["RoomNumber"].ToString(),
                            RoomType = reader["RoomType"].ToString(),
                            RoomStatus = reader["RoomStatus"].ToString(),
                            RoomPrice = reader["RoomPrice"].ToString(),
                            BedCount = reader["BedCount"].ToString(),
                            RoomGuests = reader["MaxGuests"].ToString(),
                            RoomDescription = reader["RoomDescription"].ToString()
                        });
                    }
                }
            }
            return roomList;
        }

        //<-----------------------Get Next Room Id--------------------------/>
        public int GetNextRoomId()
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand("SELECT ISNULL(MAX(RoomId), 0) + 1 FROM Rooms", connection))
            {
                connection.Open();
                return (int)command.ExecuteScalar();
            }
        }

        public IEnumerable<RoomModel> GetAvailableRoomsByType(string roomType)
        {
            var list = new List<RoomModel>();
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("SELECT * FROM Rooms WHERE RoomType = @type AND RoomStatus = 'Available'", conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@type", roomType);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new RoomModel
                        {
                            RoomId = (int)reader["RoomId"],
                            RoomNumber = reader["RoomNumber"].ToString(),
                            RoomType = reader["RoomType"].ToString(),
                            RoomStatus = reader["RoomStatus"].ToString(),
                            RoomPrice = reader["RoomPrice"].ToString(),
                            BedCount = reader["BedCount"].ToString(),
                            RoomGuests = reader["MaxGuests"].ToString(),
                            RoomDescription = reader["RoomDescription"].ToString()
                        });
                    }
                }
            }
            return list;
        }

        public RoomModel GetByNumber(string roomNumber)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand("SELECT * FROM Rooms WHERE RoomNumber = @number", connection))
            {
                command.Parameters.Add("@number", SqlDbType.NVarChar).Value = roomNumber;
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new RoomModel
                        {
                            RoomId = Convert.ToInt32(reader["RoomId"]),
                            RoomNumber = reader["RoomNumber"].ToString(),
                            RoomType = reader["RoomType"].ToString(),
                            RoomStatus = reader["RoomStatus"].ToString(),
                            RoomPrice = reader["RoomPrice"].ToString(),
                            BedCount = reader["BedCount"].ToString(),
                            RoomGuests = reader["MaxGuests"].ToString(),
                            RoomDescription = reader["RoomDescription"].ToString()
                        };
                    }
                }
            }
            return null;
        }
    }
}
