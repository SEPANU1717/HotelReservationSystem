using System.ComponentModel.DataAnnotations;

namespace HotelReservationSystem.Model.Service.Food
{
    public class FoodOrderModel
    {
        [Display(Name = "Order ID")] public int OrderId { get; set; }

        [Required] [Display(Name = "Food Name")] public string FoodName { get; set; }

        [Required] [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")] [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [Required] [Range(0.01, double.MaxValue, ErrorMessage = "Unit price must be greater than zero.")] [Display(Name = "Unit Price")]
        public decimal UnitPrice { get; set; } [Display(Name = "Total Price")]
        public decimal TotalPrice => UnitPrice * Quantity;
    }
}