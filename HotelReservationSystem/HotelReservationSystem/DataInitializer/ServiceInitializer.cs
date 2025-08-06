using HotelReservationSystem.UserControls;

namespace HotelReservationSystem.DataInitializer
{
    public class ServiceInitializer
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

        public static void Drink7(UCService drinkInstance)
        {
            drinkInstance.FoodName = "Tropical Bliss";
            drinkInstance.Price = "129.99";
            drinkInstance.Stock = "1";
            drinkInstance.Description = "Tropical fruits and sweetness over ice.";
        }
        public static void Drink8(UCService drinkInstance)
        {
            drinkInstance.FoodName = "Sunset Sparkler";
            drinkInstance.Price = "129.99";
            drinkInstance.Stock = "1";
            drinkInstance.Description = "Fizzy orange drink, summer sunset essence.";
        }
        public static void Drink9(UCService drinkInstance)
        {
            drinkInstance.FoodName = "Berry Fizz Delight";
            drinkInstance.Price = "149.99";
            drinkInstance.Stock = "1";
            drinkInstance.Description = "Fresh berries mixed with sparkling soda.";
        }
        public static void Drink10(UCService drinkInstance)
        {
            drinkInstance.FoodName = "Cherry Splash";
            drinkInstance.Price = "149.99";
            drinkInstance.Stock = "1";
            drinkInstance.Description = "Colorful cherry blend, sweet and tart.";
        }
        public static void Drink11(UCService drinkInstance)
        {
            drinkInstance.FoodName = "Citrus Cooler";
            drinkInstance.Price = "149.99";
            drinkInstance.Stock = "1";
            drinkInstance.Description = "Sharp citrus and sweetness, refreshingly revitalizing.";
        }
        public static void Drink12(UCService drinkInstance)
        {
            drinkInstance.FoodName = "Melon Medley";
            drinkInstance.Price = "169.99";
            drinkInstance.Stock = "1";
            drinkInstance.Description = "Refreshing melon blend with mint garnish.";
        }
        
        public static void LaundryBlouse(UCService laundryInstance)
        {
            laundryInstance.LaundryName = "Blouse/Skirt";
            laundryInstance.LPrice = "99.99";
        }
        
        public static void FormalAttire(UCService laundryInstance)
        {
            laundryInstance.LaundryName = "Formal Attire/Suit";
            laundryInstance.LPrice = "199.99";
        }
        
        public static void PantsTrouser(UCService laundryInstance)
        {
            laundryInstance.LaundryName = "Pants/Trouser";
            laundryInstance.LPrice = "109.99";
        }
        
        public static void Socks(UCService laundryInstance)
        {
            laundryInstance.LaundryName = "Socks/Underwear";
            laundryInstance.LPrice = "39.99";
        }
        
        public static void Sensitive(UCService laundryInstance)
        {
            laundryInstance.LaundryName = "Sensitive Fabrics";
            laundryInstance.LPrice = "249.99";
        }
    }
}
