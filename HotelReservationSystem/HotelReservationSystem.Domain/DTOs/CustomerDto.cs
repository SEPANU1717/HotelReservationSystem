using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelReservationSystem.Domain.Model;

namespace HotelReservationSystem.Domain.DTOs
{
    public class CustomerDto
    {
        public int CustomerID { get; set; }
        public string Fullname { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string IDType { get; set; }
        public string Nationality { get; set; }

    }
}
