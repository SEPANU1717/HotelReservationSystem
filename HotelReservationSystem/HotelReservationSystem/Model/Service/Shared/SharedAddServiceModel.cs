using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HotelReservationSystem.Model.Service
{
    public class SharedAddServiceModel
    {
        //[Display(Name = "#")]
        //public int Id { get; set; }

        [Required(ErrorMessage = "Item name is required.")]
        [StringLength(100, ErrorMessage = "Item name must be less than 100 characters.")]
        public string ItemName { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public decimal Price { get; set; }

        public decimal TotalPrice => Quantity * Price;
    }
}