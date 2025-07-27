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
                insertCommand.Parameters.Add("@price", SqlDbType.NVarChar).Value = room.RoomPrice;
                insertCommand.Parameters.Add("@bed", SqlDbType.NVarChar).Value = room.BedCount;
                insertCommand.Parameters.Add("@guest", SqlDbType.NVarChar).Value = room.RoomGuests;
                insertCommand.Parameters.Add("@description", SqlDbType.NVarChar).Value = room.RoomDescription;
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
                command.Parameters.Add("@price", SqlDbType.NVarChar).Value = room.RoomPrice;
                command.Parameters.Add("@bed", SqlDbType.NVarChar).Value = room.BedCount;
                command.Parameters.Add("@guest", SqlDbType.NVarChar).Value = room.RoomGuests;
                command.Parameters.Add("@description", SqlDbType.NVarChar).Value = room.RoomDescription;
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
                command.CommandText = @"SELECT * FROM Rooms WHERE RoomId = @id OR RoomNumber LIKE @number
                                      ORDER BY RoomId DESC";

                command.Parameters.Add("@id", SqlDbType.Int).Value = roomId;
                command.Parameters.Add("@number", SqlDbType.NVarChar).Value = $"%{value}%";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        roomList.Add(new RoomModel
                        {
                            RoomId = Convert.ToInt32(reader[0]),
                            RoomNumber = reader[1].ToString(),
                            RoomType = reader[2].ToString(),
                            RoomStatus = reader[3].ToString(),
                            RoomPrice = reader[4].ToString(),
                            BedCount = reader[5].ToString(),
                            RoomGuests = reader[6].ToString(),
                            RoomDescription = reader[7].ToString()
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
    }
}
