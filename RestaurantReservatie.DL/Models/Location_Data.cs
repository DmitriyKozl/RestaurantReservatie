using System.ComponentModel.DataAnnotations;

namespace RestaurantReservatie.DL.Models; 

public class Location_Data {
    [Key ]
    public int LocationId { get; set; }       
    [Required]
    public string PostalCode { get; set; }
    [Required]
    public string City { get; set; }
    public string StreetName { get; set; }
    public string HouseNumber { get; set; }

    public Location_Data(string postalCode, string city)
    {
        PostalCode = postalCode;
        City = city;
    }


    public Location_Data(string postalCode, string city, string streetName, string houseNumber)
    {
        PostalCode = postalCode;
        City = city;
        StreetName = streetName;
        HouseNumber = houseNumber;
    }

    public Location_Data()
    {
    }
}