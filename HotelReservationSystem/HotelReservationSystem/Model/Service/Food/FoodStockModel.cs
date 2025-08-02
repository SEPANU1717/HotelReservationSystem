using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Model.Service.Food
{
    public class FoodStockModel
    {
        [Display(Name = "Food ID")]public int FoodId { get; set; }
        [Required(ErrorMessage = "Food name is required.")][Display(Name = "Food Name")]public string FoodName { get; set; }
        [Display(Name = "Description")]public string Description { get; set; }
        [Required(ErrorMessage = "Price is required.")][Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        [Display(Name = "Price")] public decimal Price { get; set; }
        [Required(ErrorMessage = "Stock is required.")][Range(0, int.MaxValue, ErrorMessage = "Stock cannot be negative.")][Display(Name = "Stock")]public int Stock { get; set; }

    }
}
