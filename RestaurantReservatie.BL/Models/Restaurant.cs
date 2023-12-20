using System.Formats.Asn1;
using System.Text.RegularExpressions;
using RestaurantReservatie.BL.Exceptions;

namespace RestaurantReservatie.BL.Models;

public class Restaurant {
        public int RestaurantId { get;  set; }
        public string RestaurantName { get; private set; }
        public Location Location { get; set; }
        public string Cuisine { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public List<Table> Tables { get; private set; }

        public Restaurant(string restaurantName, Location location, string cuisine, string phone, string email)
        {
            ZetNaam(restaurantName);
            Location = location;
            Cuisine = cuisine;
            ZetTelefoonnummer(phone);
            ZetEmail(email);
        }

        public Restaurant(int restaurantId, string restaurantName, Location location, string cuisine, string phone, string email, List<Table> tables)
        {
            RestaurantId = restaurantId;
            RestaurantName = restaurantName;
            Location = location;
            Cuisine = cuisine;
            Phone = phone;
            Email = email;
             Tables = tables;
        }

        public Restaurant(int restaurantId, string restaurantName, Location location, string cuisine, string phone, string email)
        {
            RestaurantId = restaurantId;
            RestaurantName = restaurantName;
            Location = location;
            Cuisine = cuisine;
            Phone = phone;
            Email = email;
        }

        public Restaurant()
        {
        }

        public void ZetNaam(string restaurantName)
        {
            if (string.IsNullOrWhiteSpace(restaurantName)) throw new RestaurantException("Naam mag niet leeg zijn");
            RestaurantName = restaurantName;
        }
        public void ZetEmail(string email) {
            if (string.IsNullOrWhiteSpace(email)) throw new CustomerException("ZetEmail - Email mag niet leeg zijn");
            if (!Regex.IsMatch(email,
                    @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$"))
                throw new CustomerException(
                    "ZetEmail - Email is niet geldig");
            Email = email;
        }


        public void ZetTelefoonnummer(string phone) {
            if (string.IsNullOrWhiteSpace(phone))
                throw new CustomerException("ZetTelefoonnummer - Telefoonnummer mag niet leeg zijn");
            if (!Regex.IsMatch(phone, @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$"))
                throw new CustomerException("ZetTelefoonnummer - Telefoonnummer is niet geldig");
            Phone = phone;
        }

        public void ZetId(int restaurantId)
        {
            if (restaurantId < 0) throw new RestaurantException("ZetId - Id mag niet kleiner zijn dan 0");
            RestaurantId = restaurantId;
        }
        
}