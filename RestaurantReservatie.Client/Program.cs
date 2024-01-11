using RestaurantReservatie.Client;
using RestaurantReservatie.Rest.Models.Input;
using TestEFLayer.Output;
using System;
using System.Threading.Tasks;

namespace RestaurantReservatie.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var service = new ReservationService();
            
            Console.WriteLine("Enter desired cuisine:");
            string cuisine = Console.ReadLine();

            var restaurants = await service.GetRestaurantByCuisineAsync(cuisine);
            if (restaurants == null || restaurants.Count == 0)
            {
                Console.WriteLine("No restaurants found for this cuisine.");
                return;
            }

            Console.WriteLine("Available restaurants:");
            foreach (var restaurant in restaurants)
            {
                Console.WriteLine(restaurant.ToString());
            }

            Console.WriteLine("Enter the name of the restaurant you want to register with:");
            string restaurantName = Console.ReadLine();

            int restaurantId = await service.GetRestaurantByNameAsync(restaurantName);
            if (restaurantId == -1)
            {
                Console.WriteLine("Restaurant not found.");
                return;
            }

            Console.WriteLine("Enter your name:");
            string userName = Console.ReadLine();

            Console.WriteLine("Enter your phone number:");
            string userPhone = Console.ReadLine();

            Console.WriteLine("Enter your email:");
            string userEmail = Console.ReadLine();

            Console.WriteLine("Enter your city:");
            string userCity = Console.ReadLine();

            Console.WriteLine("Enter your postal code:");
            string userPostalCode = Console.ReadLine();

            Console.WriteLine("Enter your street:");
            string userStreet = Console.ReadLine();

            Console.WriteLine("Enter your house number:");
            string userHouseNumber = Console.ReadLine();

            var user = new CustomerInputDTO
            {
                Name = userName,
                PhoneNumber = userPhone,
                Email = userEmail,
                City = userCity,
                PostalCode = userPostalCode,
                Street = userStreet,
                HouseNumber = userHouseNumber
            };

            var addedUser = await service.AddUserAsync(user);
            if (addedUser == null)
            {
                Console.WriteLine("Failed to add user.");
                return;
            }
    

            Console.WriteLine("Enter the number of people for the reservation:");
            int capacity;
            while (!int.TryParse(Console.ReadLine(), out capacity) || capacity <= 0)
            {
                Console.WriteLine("Please enter a valid number greater than 0:");
            }

            Console.WriteLine("Enter the date for the reservation (format: yyyy-MM-dd):");
            DateTime date;
            while (!DateTime.TryParse(Console.ReadLine(), out date))
            {
                Console.WriteLine("Please enter a valid date in the format yyyy-MM-dd:");
            }

            Console.WriteLine("Enter the time for the reservation (format: HH:mm):");
            string time = Console.ReadLine(); // You may want to add validation here.

            var reservation = new ReservationInputDTO
            {
                RestaurantId = restaurantId,  // Assuming restaurantId is obtained from earlier in your code
                CustomerId = addedUser.CustomerId, // Assuming addedUser is the result of AddUserAsync
                Capacity = capacity,
                Date = date,
                Time = time
            };

            var addedReservation = await service.AddReservationAsync(reservation);
            if (addedReservation == null)
            {
                Console.WriteLine("Failed to add reservation.");
                return;
            }

            Console.WriteLine("Reservation successful!");
        }
    }
}
