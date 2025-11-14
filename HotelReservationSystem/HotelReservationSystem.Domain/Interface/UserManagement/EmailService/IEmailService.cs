using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Domain.Interface.UserManagement.Email_Service
{
    public interface IEmailService
    {
        Task<bool> SendPasswordResetEmailAsync(string toEmail, string resetToken, string username);
        Task<bool> SendEmailAsync(string toEmail, string subject, string body);
    }
}
