using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelReservationSystem.Interface.Service.Food
{
    public interface IOrderFoodView
    {
        string ItemName { get; set; }
        string FoodQuantity { get; set; }
        string FoodPrice { get; set; }

        event EventHandler OrderAddEvent;
        event EventHandler OrderClearEvent;
        event EventHandler OrderCompleteEvent;
        event EventHandler OrderCancelEvent;

        void SetOrderListBindingSource(BindingSource orderList);
    }
}
