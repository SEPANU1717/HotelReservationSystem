using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelReservationSystem.Interface.Billing;
using HotelReservationSystem.Model.Billing;

namespace HotelReservationSystem.Repositories.Billing
{
    public class BillingRepository : BaseRepository ,IBillingRepository
    {
        public BillingRepository(string connectionString) : base(connectionString) {}


        //<-----------------------Add Billing--------------------------/>
        public void Add(BillingModel billingModel)
        {
            throw new NotImplementedException();
        }

        //<-----------------------Delete Billing--------------------------/>
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        //<-----------------------Edit Billing--------------------------/>
        public void Edit(BillingModel billingModel)
        {
            throw new NotImplementedException();
        }

        //<-----------------------Get All Billing--------------------------/>
        public IEnumerable<BillingModel> GetAll()
        {
            throw new NotImplementedException();
        }

        //<-----------------------Get By Value Billing--------------------------/>
        public IEnumerable<BillingModel> GetByValue(string value)
        {
            throw new NotImplementedException();
        }
    }
}
