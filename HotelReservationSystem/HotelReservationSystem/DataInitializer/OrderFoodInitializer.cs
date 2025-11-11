using HotelReservationSystem.Domain.Model.Service.Shared;
using HotelReservationSystem.UserControls;

namespace HotelReservationSystem.DataInitializer
{
    public static class OrderFoodInitializer
    {
        public static void OrderSaltedPasta(SharedAddServiceModel orderInstance)
        {
            orderInstance.ItemName = "Salted Pasta";
            orderInstance.Price = 249.99m; 
            orderInstance.Quantity = 1;
        }

        public static void OrderSpicySeafoodNoodles(SharedAddServiceModel orderInstance)
        {
            orderInstance.ItemName = "Spicy Seafood Noodles";
            orderInstance.Price = 299.99m;
            orderInstance.Quantity = 1;
        }

        public static void OrderBeefDumpling(SharedAddServiceModel orderInstance)
        {
            orderInstance.ItemName = "Beef Dumpling";
            orderInstance.Price = 309.99m;
            orderInstance.Quantity = 1;
        }

        public static void OrderHealthyNoodles(SharedAddServiceModel orderInstance)
        {
            orderInstance.ItemName = "Healthy Noodles";
            orderInstance.Price = 249.99m;
            orderInstance.Quantity = 1;
        }

        public static void OrderHotFriedRiceWithOmelet(SharedAddServiceModel orderInstance)
        {
            orderInstance.ItemName = "Hot Fried Rice with Omelet";
            orderInstance.Price = 259.99m;
            orderInstance.Quantity = 1;
        }

        public static void OrderSpicyNoodleWithOmelet(SharedAddServiceModel orderInstance)
        {
            orderInstance.ItemName = "Spicy Noodle w/omelet";
            orderInstance.Price = 249.99m;
            orderInstance.Quantity = 1;
        }

        public static void OrderTropicalBliss(SharedAddServiceModel orderInstance)
        {
            orderInstance.ItemName = "Tropical Bliss";
            orderInstance.Price = 129.99m;
            orderInstance.Quantity = 1;
        }

        public static void OrderSunsetSparkler(SharedAddServiceModel orderInstance)
        {
            orderInstance.ItemName = "Sunset Sparkler";
            orderInstance.Price = 129.99m;
            orderInstance.Quantity = 1;
        }

        public static void OrderBerryFizzDelight(SharedAddServiceModel orderInstance)
        {
            orderInstance.ItemName = "Berry Fizz Delight";
            orderInstance.Price = 149.99m;
            orderInstance.Quantity = 1;
        }

        public static void OrderCherrySplash(SharedAddServiceModel orderInstance)
        {
            orderInstance.ItemName = "Cherry Splash";
            orderInstance.Price = 149.99m;
            orderInstance.Quantity = 1;
        }

        public static void OrderCitrusCooler(SharedAddServiceModel orderInstance)
        {
            orderInstance.ItemName = "Citrus Cooler";
            orderInstance.Price = 149.99m;
            orderInstance.Quantity = 1;
        }

        public static void OrderMelonMedley(SharedAddServiceModel orderInstance)
        {
            orderInstance.ItemName = "Melon Medley";
            orderInstance.Price = 169.99m;
            orderInstance.Quantity = 1;
        }
    }
}
