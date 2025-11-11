using System.Collections.Generic;
using HotelReservationSystem.Domain.Model.CheckInOut;

namespace HotelReservationSystem.Domain.Interface.CheckInOut
{
    public interface ICheckInOutRepository
    {
        void Add(CheckInOutModel checkIn);
        void Edit(CheckInOutModel checkIn);
        void Delete(int reservationId);
        CheckInOutModel GetById(int checkInId);
        CheckInOutModel GetByReservationId(int reservationId);
        IEnumerable<CheckInOutModel> GetAll();
        IEnumerable<CheckInOutModel> GetByValue(string value);
        IEnumerable<CheckInOutModel> GetActiveCheckIns();
        int GetNextCheckInId();
    }
}