using RestaurantReservatie.BL.Models;

namespace RestaurantReservatie.Rest.Models.Output;

public class CustomerOutputDTO {
    public int CustomerID { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public Location Location { get; set; }


    public CustomerOutputDTO(int customerID, string name, string email, string phoneNumber, Location location) {
        CustomerID = customerID;
        Name = name;
        Email = email;
        PhoneNumber = phoneNumber;
        Location = location;
    }
}