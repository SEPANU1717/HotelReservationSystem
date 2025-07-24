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

        [DisplayName("Customer ID")]
        public int CustomerID { get; set; }

        [DisplayName("Full Name")]
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be 3–50 characters")]
        public string Name { get; set; }

        [DisplayName("ID Type")]
        [Required(ErrorMessage = "ID Type is required")]
        public string IDType { get; set; }

        [DisplayName("Contact")]
        [Required(ErrorMessage = "Contact is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string Contact { get; set; }

        [DisplayName("Address")]
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
        }
    }

