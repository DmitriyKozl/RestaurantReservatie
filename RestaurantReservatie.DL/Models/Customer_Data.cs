using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RestaurantReservatie.BL.Models;

namespace RestaurantReservatie.DL.Models; 

public class Customer_Data {
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    // Foreign key for ContactInfo
    public int ContactInfoId { get; set; }

    [ForeignKey("ContactInfoId")]
    public ContactInfo ContactInfo { get; set; }

    // Navigation property for Reservations
    public virtual ICollection<Reservation> Reservations { get; set; }

}