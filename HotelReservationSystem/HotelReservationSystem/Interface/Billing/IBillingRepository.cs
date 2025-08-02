using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelReservationSystem.Model;
using HotelReservationSystem.Model;

namespace HotelReservationSystem.Interface.Billing
{
    public interface IBillingRepository
    {
        void Add(BillingModel billingModel);
        void Edit(BillingModel billingModel);
        void Delete(int id);

        IEnumerable<BillingModel> GetAll();
        IEnumerable<BillingModel> GetByValue(string value);
    }
}
