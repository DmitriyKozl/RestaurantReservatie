using System.ComponentModel.DataAnnotations;

namespace RestaurantReservatie.DL.Models; 

public class Location_Data {
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string City { get; set; }

    [Required]
    [MaxLength(10)]
    public string PostalCode { get; set; }

    [MaxLength(100)]
    public string Street { get; set; }

    public string Number { get; set; }  // Nullable
    
    public Location_Data() {
        
    }
}