using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelReservationSystem.Interface.Rooms;
using HotelReservationSystem.Model;
using HotelReservationSystem.UserControls;

namespace HotelReservationSystem.Helper
{
    public class RoomHelper
    {

        public static void StandardRoom(UCRooms ucRoomsInstance)
        {
            ucRoomsInstance.RoomType = "Standard";
            ucRoomsInstance.RoomDescription = "Comfortable room with essential amenities.";
            ucRoomsInstance.RoomGuests = "2";
            ucRoomsInstance.BedCount = "1";
            ucRoomsInstance.RoomPrice = "2499";
            ucRoomsInstance.RoomNumber = "STD";
            ucRoomsInstance.RoomStatus = "Available";
        }

        public static void DeluxeRoom(UCRooms ucRoomsInstance)
        {
            ucRoomsInstance.RoomType = "Deluxe";
            ucRoomsInstance.RoomDescription = "Premium room with enhanced amenities.";
            ucRoomsInstance.RoomGuests = "2";
            ucRoomsInstance.BedCount = "1";
            ucRoomsInstance.RoomPrice = "5999";
            ucRoomsInstance.RoomNumber = "DLX";
            ucRoomsInstance.RoomStatus = "Available";
        }

        public static void SuiteRoom(UCRooms ucRoomsInstance)
        {
            ucRoomsInstance.RoomType = "Suite";
            ucRoomsInstance.RoomDescription = "Luxurious suite with separate living area.";
            ucRoomsInstance.RoomGuests = "4";
            ucRoomsInstance.BedCount = "2";
            ucRoomsInstance.RoomPrice = "7999";
            ucRoomsInstance.RoomNumber = "ST";
            ucRoomsInstance.RoomStatus = "Available";
        }

        public static void FamilyRoom(UCRooms ucRoomsInstance)
        {
            ucRoomsInstance.RoomType = "Family";
            ucRoomsInstance.RoomDescription = "Spacious room perfect for families.";
            ucRoomsInstance.RoomGuests = "6";
            ucRoomsInstance.BedCount = "3";
            ucRoomsInstance.RoomPrice = "4999";
            ucRoomsInstance.RoomNumber = "FML";
            ucRoomsInstance.RoomStatus = "Available";
        }

        public static void SingleRoom(UCRooms ucRoomsInstance)
        {
            ucRoomsInstance.RoomType = "Single";
            ucRoomsInstance.RoomDescription = "Cozy room ideal for solo travelers.";
            ucRoomsInstance.RoomGuests = "1";
            ucRoomsInstance.BedCount = "1";
            ucRoomsInstance.RoomPrice = "1999";
            ucRoomsInstance.RoomNumber = "SGL";
            ucRoomsInstance.RoomStatus = "Available";
        }
    }
}
