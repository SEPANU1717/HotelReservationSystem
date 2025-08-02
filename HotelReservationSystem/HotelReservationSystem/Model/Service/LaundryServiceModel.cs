using System.ComponentModel.DataAnnotations;

namespace HotelReservationSystem.Model.Service
{
    public class LaundryServiceModel
    {
        [Display(Name = "Laundry ID")]
        public int LaundryId { get; set; }

        [Required(ErrorMessage = "Service name is required.")]
        [Display(Name = "Service Name")]
        public string ServiceName { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        [Display(Name = "Price")]
        public decimal Price { get; set; }
    }
}