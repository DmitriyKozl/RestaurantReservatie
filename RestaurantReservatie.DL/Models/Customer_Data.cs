using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RestaurantReservatie.BL.Models;

namespace RestaurantReservatie.DL.Models;

public class Customer_Data {
    [Key] public int CustomerId { get; set; }
    [Required] public string Name { get; set; }
    [Required] public string Email { get; set; }
    [Required] public string Phone { get; set; }
    [Required] public Location_Data Location { get; set; }

    public bool Deleted { get; set; }

    public Customer_Data(int customerId, string name, string email, string phone, Location_Data locatie) {
        CustomerId = customerId;
        Name = name;
        Email = email;
        Phone = phone;
        Location = locatie;
        Deleted = false;
    }

    public Customer_Data(string name, string email, string phone, Location_Data locatie) {
        Name = name;
        Email = email;
        Phone = phone;
        Location = locatie;
        Deleted = false;
    }


    public Customer_Data() { }
}