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

                AddRoomParameters(command, room);
                command.Parameters.Add("@id", SqlDbType.Int).Value = room.RoomId;
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

        public int GetNextRoomId()
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand("SELECT ISNULL(MAX(RoomId), 0) + 1 FROM Rooms", connection))
            {
                connection.Open();
                return (int)command.ExecuteScalar();
            }
        }


        public IEnumerable<RoomModel> GetAvailableRoomsByTypeAndDateRange(string roomType, DateTime checkInDate, DateTime checkOutDate, int? excludeReservationId = null)
        {
            var list = new List<RoomModel>();
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                
                string query = @"
                    SELECT r.RoomId, r.RoomNumber, r.RoomType, r.RoomStatus, r.RoomPrice, r.BedCount, r.MaxGuests, r.RoomDescription
                    FROM Rooms r
                    WHERE r.RoomType = @type
                    AND r.RoomNumber NOT IN (
                        SELECT res.RoomNumber 
                        FROM Reservations res
                        WHERE res.RoomNumber IS NOT NULL
                        AND res.ReservationStatus NOT IN ('Cancelled/No Show', 'CheckedOut')
                        AND (
                            -- Check for date overlap
                            (res.CheckInDate < @checkOutDate AND res.CheckOutDate > @checkInDate)
                        )
                        AND (@excludeReservationId IS NULL OR res.ReservationId != @excludeReservationId)
                    )
                    ORDER BY r.RoomNumber";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@type", roomType);
                    cmd.Parameters.AddWithValue("@checkInDate", checkInDate);
                    cmd.Parameters.AddWithValue("@checkOutDate", checkOutDate);
                    cmd.Parameters.AddWithValue("@excludeReservationId", (object)excludeReservationId ?? DBNull.Value);
                    
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(MapRoomFromReader(reader));
                        }
                    }
                }
            }
            return list;
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
            var now = DateTime.Now.Date;
            
            // Get all active reservations for this room (not cancelled or checked out)
            var activeReservations = reserveRepo.GetAll()
                .Where(r => r.RoomNumber == roomNumber 
                         && r.ReservationStatus != "Cancelled/No Show" 
                         && r.ReservationStatus != "CheckedOut")
                .OrderBy(r => r.CheckInDate)
                .ToList();

            var room = GetByNumber(roomNumber);
            if (room == null) return;

            // Check if there's a current active reservation (today falls within check-in and check-out dates)
            var currentReservation = activeReservations
                .FirstOrDefault(r => r.CheckInDate.Date <= now && r.CheckOutDate.Date > now);

            if (currentReservation != null)
            {
                // There's an active reservation for today
                if (currentReservation.ReservationStatus == "CheckedIn")
                {
                    if (room.RoomStatus != "Occupied")
                    {
                        room.RoomStatus = "Occupied";
                        Edit(room);
                    }
                }
                else
                {
                    if (room.RoomStatus != "Reserved")
                    {
                        room.RoomStatus = "Reserved";
                        Edit(room);
                    }
                }
            }
            else if (activeReservations.Any(r => r.CheckInDate.Date > now))
            {
                // There are future reservations but no current one - room is available now
                if (room.RoomStatus != "Available")
                {
                    room.RoomStatus = "Available";
                    Edit(room);
                }
            }
            else
            {
                // No active reservations at all
                if (room.RoomStatus != "Available")
                {
                    room.RoomStatus = "Available";
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
                RoomDescription = reader["RoomDescription"] == DBNull.Value ? string.Empty : reader["RoomDescription"].ToString()
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

        public IEnumerable<RoomModel> GetByStatusFilter(string statusFilter)
        {
            var roomList = new List<RoomModel>();
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;

                if (statusFilter == "All")
                {
                    command.CommandText = "SELECT * FROM Rooms ORDER BY RoomId DESC";
                }
                else
                {
                    command.CommandText = "SELECT * FROM Rooms WHERE RoomStatus = @status ORDER BY RoomId DESC";
                    command.Parameters.Add("@status", SqlDbType.NVarChar).Value = statusFilter;
                }

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
    }
}
