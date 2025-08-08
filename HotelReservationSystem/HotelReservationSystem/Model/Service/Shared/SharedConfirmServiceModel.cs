using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    namespace HotelReservationSystem.Model.Service.Shared
    {
        public class SharedConfirmServiceModel
        {
            [Display(Name = "Service ID")]
            public int ServiceId { get; set; }

            [Required(ErrorMessage = "Service type is required.")]
            [Display(Name = "Service Type")]
            public string ServiceType { get; set; }

            [Required(ErrorMessage = "Customer name is required.")]
            [StringLength(100, ErrorMessage = "Customer name must be less than 100 characters.")]
            [Display(Name = "Customer Name")]
            public string CustomerName { get; set; }

            [Required(ErrorMessage = "Room number is required.")]
            [StringLength(10, ErrorMessage = "Room number must be less than 10 characters.")]
            [Display(Name = "Room Number")]
            public string RoomNumber { get; set; }

            [Required(ErrorMessage = "Total items is required.")]
            [Range(1, int.MaxValue, ErrorMessage = "Total items must be at least 1.")]
            [Display(Name = "Total Items")]
            public int TotalItems { get; set; }

            [Required(ErrorMessage = "Status is required.")]
            [Display(Name = "Status")]
            public string Status { get; set; }

            [Required(ErrorMessage = "Service fee is required.")]
            [Range(0, double.MaxValue, ErrorMessage = "Service fee must be zero or greater.")]
            [Display(Name = "Service Fee")]
            public decimal ServiceFee { get; set; }

            [Required(ErrorMessage = "Total amount is required.")]
            [Range(0, double.MaxValue, ErrorMessage = "Total amount must be zero or greater.")]
            [Display(Name = "Total Amount")]
            public decimal TotalAmount { get; set; }

            [Display(Name = "Grand Total")]
            public decimal GrandTotal => ServiceFee + TotalAmount;
        }
    }