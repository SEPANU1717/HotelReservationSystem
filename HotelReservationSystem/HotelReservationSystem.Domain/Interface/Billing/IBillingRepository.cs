using System.Collections.Generic;
using HotelReservationSystem.Domain.Model;

namespace HotelReservationSystem.Domain.Interface.Billing
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
