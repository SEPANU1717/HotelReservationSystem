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

        public RoomRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public int GetRoomCount()
        {
            int count = 0;
            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM Rooms";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    count = Convert.ToInt32(command.ExecuteScalar());
                }
            }

            return count;
        }

        public void Add(RoomModel room)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"INSERT INTO Rooms 
            (RoomNumber, RoomType, RoomStatus, RoomPrice, BedCount, MaxGuests, RoomDescription) 
            VALUES 
            (@number, @type, @status, @price, @bed, @guest, @description)";


                command.Parameters.Add("@number", SqlDbType.NVarChar).Value = room.RoomNumber;
                command.Parameters.Add("@type", SqlDbType.NVarChar).Value = room.RoomType;
                command.Parameters.Add("@status", SqlDbType.NVarChar).Value = room.RoomStatus;
                command.Parameters.Add("@price", SqlDbType.NVarChar).Value = room.RoomPrice;
                command.Parameters.Add("@bed", SqlDbType.NVarChar).Value = room.BedCount;
                command.Parameters.Add("@guest", SqlDbType.NVarChar).Value = room.RoomGuests;
                command.Parameters.Add("@description", SqlDbType.NVarChar).Value = room.RoomDescription;
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
                command.CommandText = "DELETE FROM Rooms WHERE RoomId = @id";
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                command.ExecuteNonQuery();
            }
        }

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
                command.Parameters.Add("@price", SqlDbType.NVarChar).Value = room.RoomPrice;
                command.Parameters.Add("@bed", SqlDbType.NVarChar).Value = room.BedCount;
                command.Parameters.Add("@guest", SqlDbType.NVarChar).Value = room.RoomGuests;
                command.Parameters.Add("@description", SqlDbType.NVarChar).Value = room.RoomDescription;
                command.ExecuteNonQuery();

            }
        }

        public IEnumerable<RoomModel> GetAll()
        {
            var roomList = new List<RoomModel>();

            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM Rooms order by RoomId desc";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var roomModel = new RoomModel();

                        roomModel.RoomId = (int)(reader[0]);
                        roomModel.RoomNumber = reader[1].ToString();
                        roomModel.RoomType = reader[2].ToString();
                        roomModel.RoomStatus = reader[3].ToString();
                        roomModel.RoomPrice = reader[4].ToString();
                        roomModel.BedCount = reader[5].ToString();
                        roomModel.RoomGuests = reader[6].ToString();
                        roomModel.RoomDescription = reader[7].ToString();

                        roomList.Add(roomModel);

                    }
                }
            }
            return roomList;
        }

        public IEnumerable<RoomModel> GetByValue(string value)
        {
            var roomList = new List<RoomModel>();
            int roomId = int.TryParse(value, out _) ? Convert.ToInt32(value) : 0;

            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"SELECT * FROM Rooms 
                                WHERE RoomId = @id OR RoomNumber LIKE @number 
                                ORDER BY RoomId DESC";

                command.Parameters.Add("@id", SqlDbType.Int).Value = roomId;
                command.Parameters.Add("@number", SqlDbType.NVarChar).Value = $"%{value}%";

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
    }
}
