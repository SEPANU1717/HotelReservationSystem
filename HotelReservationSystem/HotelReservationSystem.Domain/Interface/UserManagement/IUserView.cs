using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelReservationSystem.Domain.Interface
{
    public interface IUserView
    {
        string UserId { get; set; }
        string LastName { get; set; }
        string FirstName { get; set; }
        string MiddleName { get; set; }
        DateTime BirthDate { get; set; }
        string Username { get; set; }
        string Password { get; set; }
        string Email { get; set; }
        string Gender { get; set; }
        string Role { get; set; }
        string SearchValue { get; set; }
        bool isEdit { get; set; }
        bool isSuccessful { get; set; }
        string Message { get; set; }

        event EventHandler SearchEvent;
        event EventHandler AddNewEvent;
        event EventHandler EditEvent;
        event EventHandler SaveEvent;
        event EventHandler CancelEvent;
        event EventHandler DeleteEvent;

        void SetUserListBindingSource(BindingSource bindingSource);
    }
}
