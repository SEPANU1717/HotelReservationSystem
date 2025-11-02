using System;
using System.Windows.Forms;
using HotelReservationSystem.Domain.Model.Service.Shared;

namespace HotelReservationSystem.Domain.Interface.Service.Food
{
    public interface IOrderFoodView
    {

        event EventHandler OrderAddEvent;
        event EventHandler OrderClearEvent;
        event EventHandler OrderCompleteEvent;
        event EventHandler OrderCancelEvent;

        SharedAddServiceModel SelectedOrder { get; }

        void SetOrderListBindingSource(BindingSource orderList);
    }
}
