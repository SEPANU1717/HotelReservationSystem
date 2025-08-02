using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelReservationSystem.Interface.Service.Food
{
    public interface IFoodStockVIew
    {
        string FoodId { get; set; }
        string FoodName { get; set; }
        string Description { get; set; }
        string Price { get; set; }
        string Stock { get; set; }
        string SearchValue { get; set; }
        bool isEdit { get; set; }
        bool isSuccessful { get; set; }
        string Message { get; set; }

        event EventHandler SearchEvent;
        event EventHandler AddNewEvent;
        event EventHandler EditEvent;
        event EventHandler DeleteEvent;
        event EventHandler SaveEvent;
        event EventHandler CancelEvent;

        void SetFoodListBindingSource(BindingSource foodList);
    }
}
