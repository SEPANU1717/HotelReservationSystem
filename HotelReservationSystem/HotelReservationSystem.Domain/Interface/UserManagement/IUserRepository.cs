using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelReservationSystem.Domain.Model;

namespace HotelReservationSystem.Domain.Interface
{
    public interface IUserRepository
    {
        void Add(UserModel user);
        void Edit(UserModel user);
        void Delete(int id);
        IEnumerable<UserModel> GetAll();
        UserModel GetById(int id);
        IEnumerable<UserModel> GetByValue(string value);
        UserModel GetByUsernameOrEmail(string usernameOrEmail);
        UserModel AuthenticateUser(string usernameOrEmail, string password);
    }
}
