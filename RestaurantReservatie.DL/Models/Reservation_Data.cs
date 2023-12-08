using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RestaurantReservatie.BL.Models;

namespace RestaurantReservatie.DL.Models; 

public class Reservation_Data {
    [Key]
    public int Id { get; set; }

    // Foreign keys for Customer and Restaurant
    public int CustomerId { get; set; }
    public int RestaurantId { get; set; }

    [ForeignKey("CustomerId")]
    public Customer Customer { get; set; }

    [ForeignKey("RestaurantId")]
    public Restaurant_Data Restaurant { get; set; }

    [Required]
    public int NumberOfPersons { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required]
    public DateTime Time { get; set; }

    [Required]
    public int TableNumber { get; set; }
}