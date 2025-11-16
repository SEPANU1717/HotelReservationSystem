using System;
using System.Collections.Generic;
using HotelReservationSystem.Domain.Model;

namespace HotelReservationSystem.Domain.Interface.Rooms
{
    public interface IRoomRepository
    {
        void Add(RoomModel room);
        void Edit(RoomModel room);
        void Delete(int id);
        IEnumerable<RoomModel> GetByStatusFilter(string statusFilter);

        IEnumerable<RoomModel> GetAll();
        IEnumerable<RoomModel> GetByValue(string value);
        IEnumerable<RoomModel> GetAvailableRoomsByTypeAndDateRange(string roomType, DateTime checkInDate, DateTime checkOutDate, int? excludeReservationId = null);
    }
}
