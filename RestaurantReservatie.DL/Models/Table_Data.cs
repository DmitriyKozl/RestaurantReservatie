using System.ComponentModel.DataAnnotations;

namespace RestaurantReservatie.DL.Models; 

public class Table_Data {
    [Key ]
    public int ID { get; set; }
    [Required]
    public int Capacity { get; set; }
    [Required]
    public int RestaurantID { get; set; }
    [Required]
    public int TableNumber { get; set; }
    public Restaurant_Data Restaurant { get; set; }
    public bool Deleted { get; set; }


    public Table_Data(int iD, int capacity, int tableNumber, int restaurantID) {
        ID = iD;
        Capacity = capacity;
        TableNumber = tableNumber;
        RestaurantID = restaurantID;
        Deleted = false;
    }
    
    public Table_Data(int capacity, int tableNumber, int restaurantID) {
        Capacity = capacity;
        TableNumber = tableNumber;
        RestaurantID = restaurantID;
        Deleted = false;
    }
    
    public Table_Data() { }
}