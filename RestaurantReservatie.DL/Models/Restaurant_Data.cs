using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantReservatie.DL.Models;

public class Restaurant_Data {
    [Key] public int RestaurantID { get; set; }
    [Required] public string RestaurantName { get; set; }
    [Required] public Location_Data Location { get; set; }
    [Required] public string Cuisine { get; set; }
    [Required] public string Email { get; set; }
    [Required] public string PhoneNumber { get; set; }
    public List<Table_Data> Table { get; set; }
    public bool Deleted { get; set; }

    public Restaurant_Data(int restaurantID, string restaurantName, Location_Data location, string cuisine,
        string email, string phoneNumber,
        List<Table_Data> table
    ) {
        RestaurantID = restaurantID;
        RestaurantName = restaurantName;
        Location = location;
        Cuisine = cuisine;
        Email = email;
        PhoneNumber = phoneNumber;
        Table = table;
        Deleted = false;
    }

    public Restaurant_Data(string restaurantName, Location_Data location, string cuisine, string email,
        string phoneNumber) {
        RestaurantName = restaurantName;
        Location = location;
        Cuisine = cuisine;
        Email = email;
        PhoneNumber = phoneNumber;
        Deleted = false;
    }

    public Restaurant_Data() { }
}