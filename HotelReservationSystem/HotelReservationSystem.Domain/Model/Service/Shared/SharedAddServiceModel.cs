using System.ComponentModel.DataAnnotations;

namespace HotelReservationSystem.Domain.Model.Service.Shared
{
    public class SharedAddServiceModel
    {
        //[Display(Name = "#")]
        //public int Id { get; set; }

        [Display(Name = "Item Name")]
        [Required(ErrorMessage = "Item name is required.")]
        [StringLength(100, ErrorMessage = "Item name must be less than 100 characters.")]
        public string ItemName { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public decimal Price { get; set; }
        [Display(Name = "Total Price")]
        public decimal TotalPrice => Quantity * Price;
    }
}