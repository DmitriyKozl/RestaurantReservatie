using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantReservatie.DL.Models; 

public class Restaurant_Data {
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    // Foreign key for Location
    public int LocationId { get; set; }

    [ForeignKey("LocationId")]
    public Location_Data Location { get; set; }

    // Navigation property for Reservations
    public virtual ICollection<Reservation_Data> Reservations { get; set; }
    
    
    public Restaurant_Data(string name, Location_Data location) {
        Name = name;
        Location = location;
    }
    
    public Restaurant_Data() {
        
    }
}