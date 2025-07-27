using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HotelReservationSystem.Model.Customer
{
    public class CustomerModel
    {
        //Id
        [DisplayName("Customer ID")]
        public int CustomerID { get; set; }

        //First name
        [DisplayName("First Name")]
        [Required(ErrorMessage = "First name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be 3–50 characters")]
        public string FirstName { get; set; }

        //Last name
        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Last name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be 3–50 characters")]
        public string LastName { get; set; }

        //Id type
        [DisplayName("ID Type")]
        [Required(ErrorMessage = "ID Type is required")]
        public string IDType { get; set; }


        //Contact
        [DisplayName("Contact")]
        [Required(ErrorMessage = "Contact is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string Contact { get; set; }

        //Address
        [DisplayName("Address")]
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
        }
    }

