using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Domain.Enums
{
    public class RoomEnum
    {
        public enum RoomAvailability
        {
            Available,
            Occupied,
            Reserved,
            UnderMaintenance
        }

        public enum RoomStatusFilter
        {
            All,
            Available,
            Occupied,
            Reserved,
            UnderMaintenance
         }
    }
}
