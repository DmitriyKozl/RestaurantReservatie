using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RestaurantReservatie.BL.Models;

namespace RestaurantReservatie.DL.Models;

public class Reservation_Data {
    [Key] public int Id { get; set; }
    [Required] public Restaurant_Data RestaurantData { get; set; }
    [Required] public Customer_Data CustomerData { get; set; }
    [Required] public int Party { get; set; }
    [Required]  public DateTime Date { get; set; } // Combined date and time
    
    [Required] public int TableNumber { get; set; }

    public bool Deleted { get; set; }

    public Reservation_Data(int id, Restaurant_Data restaurantData, Customer_Data customerData, int party,
        DateTime date, int tableNumber) {
        Id = id;
        RestaurantData = restaurantData;
        CustomerData = customerData;
        Party = party;
        Date = date;
        TableNumber = tableNumber;
        Deleted = false;
    }

    public Reservation_Data(Restaurant_Data restaurantData, Customer_Data customerData, int party, DateTime date,
        int tableNumber) {
        RestaurantData = restaurantData;
        CustomerData = customerData;
        Party = party;
        Date = date;
        TableNumber = tableNumber;
        Deleted = false;
    }

    public Reservation_Data() { }
}