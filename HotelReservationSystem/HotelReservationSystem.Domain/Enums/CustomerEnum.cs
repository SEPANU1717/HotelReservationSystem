using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Domain.Enums
{
    public class CustomerEnum
    {
        public enum IdentificationType { Passport, DriversLicense, NationalID, Other }
        public enum Gender { Male, Female, PreferNotToSay }
    }
}
