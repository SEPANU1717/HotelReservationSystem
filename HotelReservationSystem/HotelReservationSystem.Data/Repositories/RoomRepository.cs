using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using HotelReservationSystem.Domain.Interface.Rooms;
using HotelReservationSystem.Domain.Model;

namespace HotelReservationSystem.Data.Repositories
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

                AddRoomParameters(insertCommand, room);
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

                AddRoomParameters(command, room);
                command.Parameters.Add("@id", SqlDbType.Int).Value = room.RoomId;
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
                        roomList.Add(MapRoomFromReader(reader));
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
                command.CommandText = @"SELECT * FROM Rooms WHERE RoomId = @id OR RoomStatus LIKE @status OR RoomNumber LIKE @rnumber
                                  ORDER BY RoomId DESC";
                command.Parameters.Add("@id", SqlDbType.Int).Value = roomId;
                command.Parameters.Add("@rnumber", SqlDbType.NVarChar).Value = $"%{value}%";
                command.Parameters.Add("@status", SqlDbType.NVarChar).Value = $"%{value}%";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        roomList.Add(MapRoomFromReader(reader));
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
                        list.Add(MapRoomFromReader(reader));
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
                    return reader.Read() ? MapRoomFromReader(reader) : null;
                }
            }
        }

        public void SyncRoomStatusesWithReservations(string roomNumber, ReservationRepository reserveRepo)
        {
            var now = DateTime.Now;
            var reservations = reserveRepo.GetAll()
                .Where(r => r.RoomNumber == roomNumber && r.ReservationStatus == "Reserved")
                .OrderByDescending(r => r.CheckOutDate)
                .ToList();

            var room = GetByNumber(roomNumber);
            if (room == null) return;

            if (reservations.Count == 0 || reservations[0].CheckOutDate < now)
            {
                if (room.RoomStatus != "Available")
                {
                    room.RoomStatus = "Available";
                    Edit(room);
                }
            }
            else
            {
                bool isOccupied = reservations.Any(r => r.CheckInDate <= now && r.CheckOutDate >= now);
                string newStatus = isOccupied ? "Occupied" : "Reserved";
                if (room.RoomStatus != newStatus)
                {
                    room.RoomStatus = newStatus;
                    Edit(room);
                }
            }
        }

        private RoomModel MapRoomFromReader(SqlDataReader reader)
        {
            return new RoomModel
            {
                RoomId = (int)reader["RoomId"],
                RoomNumber = reader["RoomNumber"].ToString(),
                RoomType = reader["RoomType"].ToString(),
                RoomStatus = reader["RoomStatus"].ToString(),
                RoomPrice = reader["RoomPrice"].ToString(),
                BedCount = reader["BedCount"].ToString(),
                RoomGuests = reader["MaxGuests"].ToString(),
                RoomDescription = reader["RoomDescription"].ToString()
            };
        }

        private void AddRoomParameters(SqlCommand command, RoomModel room)
        {
            command.Parameters.Add("@number", SqlDbType.NVarChar).Value = room.RoomNumber;
            command.Parameters.Add("@type", SqlDbType.NVarChar).Value = room.RoomType;
            command.Parameters.Add("@status", SqlDbType.NVarChar).Value = room.RoomStatus;
            command.Parameters.Add("@price", SqlDbType.Decimal).Value = decimal.Parse(room.RoomPrice);
            command.Parameters.Add("@bed", SqlDbType.Int).Value = int.Parse(room.BedCount);
            command.Parameters.Add("@guest", SqlDbType.Int).Value = int.Parse(room.RoomGuests);
            command.Parameters.Add("@description", SqlDbType.NVarChar).Value = room.RoomDescription ?? (object)DBNull.Value;
        }
    }
}
