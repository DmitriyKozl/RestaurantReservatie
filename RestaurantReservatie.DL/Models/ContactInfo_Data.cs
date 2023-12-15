using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RestaurantReservatie.BL.Models;

namespace RestaurantReservatie.DL.Models; 

public class ContactInfo_Data {
    [Key]
    public int Id { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [Phone]
    public string PhoneNumber { get; set; }

    // Foreign key for Location
    public int LocationId { get; set; }

    [ForeignKey("LocationId")]
    public Location_Data Location { get; set; }
    
    public ContactInfo_Data() {
        
    }
}