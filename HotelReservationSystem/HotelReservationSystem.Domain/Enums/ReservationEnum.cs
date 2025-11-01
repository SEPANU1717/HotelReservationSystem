using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Domain.Enums
{
    public class ReservationEnum
    {
        public enum RoomType
        {
                Standard,
                Deluxe,
                Suite,
                Family,
                Single
            }

        public enum PaymentState { Pending, Paid, Partial }
        public enum PaymentMethod { Cash, GCash, PayMaya, PayPal }
    }
}
