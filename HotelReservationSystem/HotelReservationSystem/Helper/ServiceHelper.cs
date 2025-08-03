using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelReservationSystem.UserControls;

namespace HotelReservationSystem.Helper
{
    public class ServiceHelper
    {
        public static void Food1(UCService foodInstance)
        {
            foodInstance.FoodName = "Salted Pasta";
            foodInstance.Price = "249.99";
            foodInstance.Stock = "1";
            foodInstance.Description = "Salted Pasta with mushroom sauce";
        }

        public static void Food2(UCService foodInstance)
        {
            foodInstance.FoodName = "Spicy Seafood Noodles";
            foodInstance.Price = "299.99";
            foodInstance.Stock = "1";
            foodInstance.Description = "Spicy seasoned seafood noodles";
        }
        public static void Food3(UCService foodInstance)
        {
            foodInstance.FoodName = "Beef Dumpling";
            foodInstance.Price = "309.99";
            foodInstance.Stock = "1";
            foodInstance.Description = "Beef dumpling in hot and sour soup";
        }
        public static void Food4(UCService foodInstance)
        {
            foodInstance.FoodName = "Healthy Noodles";
            foodInstance.Price = "249.99";
            foodInstance.Stock = "1";
            foodInstance.Description = "Healthy noodle with spinach leaf";
        }
        public static void Food5(UCService foodInstance)
        {
            foodInstance.FoodName = "Hot Fried Rice with Omelet";
            foodInstance.Price = "259.99";
            foodInstance.Stock = "1";
            foodInstance.Description = "Hot fried rice with omelet";
        }
        public static void Food6(UCService foodInstance)
        {
            foodInstance.FoodName = "Spicy Noodle w/omelet";
            foodInstance.Price = "249.99" ;
            foodInstance.Stock = "1";
            foodInstance.Description = "Spicy instant noodle with special omelette";
        }

    }
}
