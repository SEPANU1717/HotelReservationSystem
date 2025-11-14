using System;
using HotelReservationSystem.Domain.DTOs;
using HotelReservationSystem.Domain.Model;

namespace HotelReservationSystem.Presenter.Mapper
{
    public static class CompanionMapper
    {
        public static CompanionDto ToDto(CompanionModel model)
        {
            if (model == null)
                return null;

            return new CompanionDto
            {
                CompanionId = model.CompanionId,
                MainReservationId = model.MainReservationId,
                CompanionName = model.CompanionName,
                ContactNumber = model.ContactNumber,
                Email = model.Email,
                Relationship = model.Relationship,
                RoomType = model.RoomType,
                RoomNumber = model.RoomNumber,
                RoomPrice = model.RoomPrice,
                Nights = model.Nights,
                CheckInDate = model.CheckInDate,
                CheckOutDate = model.CheckOutDate,
                TotalCost = model.TotalCost,
                CreatedAt = model.CreatedAt,
                CreatedBy = model.CreatedBy
            };
        }

        public static CompanionModel ToModel(CompanionDto dto)
        {
            if (dto == null)
                return null;

            return new CompanionModel
            {
                CompanionId = dto.CompanionId,
                MainReservationId = dto.MainReservationId,
                CompanionName = dto.CompanionName,
                ContactNumber = dto.ContactNumber,
                Email = dto.Email,
                Relationship = dto.Relationship,
                RoomType = dto.RoomType,
                RoomNumber = dto.RoomNumber,
                RoomPrice = dto.RoomPrice,
                Nights = dto.Nights,
                CheckInDate = dto.CheckInDate,
                CheckOutDate = dto.CheckOutDate,
                TotalCost = dto.TotalCost,
                CreatedAt = dto.CreatedAt,
                CreatedBy = dto.CreatedBy
            };
        }
    }
}