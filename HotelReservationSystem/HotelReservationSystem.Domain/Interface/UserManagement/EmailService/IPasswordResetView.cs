using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelReservationSystem.Domain.Interface.UserManagement.Email_Service
{
    public interface IPasswordResetView
    {
        string UsernameOrEmailRequest { get; set; }
        string UsernameOrEmailVerify { get; set; }
        string ResetToken { get; set; }
        string NewPassword { get; set; }
        string ConfirmPassword { get; set; }

        bool IsSuccessful { get; set; }
        string Message { get; set; }

        event EventHandler RequestResetEvent;
        event EventHandler VerifyAndResetEvent;
        event EventHandler CancelEvent;

        // Methods
        void ShowMessage(string message, string title, MessageBoxIcon icon);
        void ShowStep(int step);
        void ClearFields();
        void Show();
        void Close();
    }
}
