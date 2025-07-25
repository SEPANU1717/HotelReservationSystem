using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelReservationSystem.Model.Customer;
using HotelReservationSystem.Model.Rooms;

namespace HotelReservationSystem.Interface.Rooms
{
    public interface IRoomRepository
    {
        void Add(RoomModel room);
        void Edit(RoomModel room);
        void Delete(int id);
        IEnumerable<RoomModel> GetAll();
        IEnumerable<RoomModel> GetByValue(string value);
    }
}
