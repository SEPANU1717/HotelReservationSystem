using System;
using HotelReservationSystem.Domain.DTOs;
using HotelReservationSystem.Domain.Interface;
using HotelReservationSystem.Domain.Model;

namespace HotelReservationSystem.Presenter.Mapper
{
    public static class UserMapper
    {
        public static UserModel FromUserView(IUserView userView)
        {
            if(userView is null) throw new ArgumentNullException(nameof(userView));

            return new UserModel()
            {
                UserId = string.IsNullOrEmpty(userView.UserId) ? 0 : int.Parse(userView.UserId),
                LastName = userView.LastName?.Trim(),
                FirstName = userView.FirstName?.Trim(),
                MiddleName = userView.MiddleName?.Trim(),
                BirthDate = userView.BirthDate,
                Username = userView.Username?.Trim(),
                Password = userView.Password,
                Email = userView.Email?.Trim(),
                Gender = userView.Gender,
                Role = userView.Role,
                IsActive = userView.IsActive,
                CreatedAt = DateTime.Now
            };
        }

        public static UserDto FromUserModel(UserModel user)
        {
            if(user is null) return null;

            return new UserDto
            {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email,
                FullName = user.FullName,
                Gender = user.Gender,
                Role = user.Role,
                Age = DateTime.Now.Year - user.BirthDate.Year - (DateTime.Now.DayOfYear < user.BirthDate.DayOfYear ? 1 : 0),
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt
            };
        }
    }
}
