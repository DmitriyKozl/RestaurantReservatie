using RestaurantReservatie.BL.Models;

namespace RestaurantReservatie.Rest.Models.Output;

public class RestaurantOutputDTO {
    public int Id { get; set; }
    public string Name { get; set; }
    public Location Location { get; set; }
    public string Cuisine { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }

    public List<Table> Tables { get; set; }


    public RestaurantOutputDTO
    (int id,
        string name,
        Location location,
        string cuisine,
        string phone,
        string email, List<Table> table
    ) {
        Id = id;
        Name = name;
        Location = location;
        Cuisine = cuisine;
        Phone = phone;
        Email = email;
        Tables = table;
    }
}