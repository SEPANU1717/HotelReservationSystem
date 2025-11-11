using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelReservationSystem.Domain.Enums;

namespace HotelReservationSystem.Domain.Helper
{
    public static class RoomRateHelper
    {
        public static decimal GetRoomRate(ReservationEnum.RoomType roomType)
        {
            switch (roomType)
            {
                case ReservationEnum.RoomType.Standard: return 2000m;
                case ReservationEnum.RoomType.Deluxe: return 3500m;
                case ReservationEnum.RoomType.Suite: return 5000m;
                case ReservationEnum.RoomType.Family: return 4000m;
                case ReservationEnum.RoomType.Single: return 1500m;
                default: return 0m;
            }
        }
        public static decimal GetAutoDownPayment(decimal totalPrice, decimal percentage = 0.5m)
        {
            return Math.Round(totalPrice * percentage, 2);
        }
    }
}

