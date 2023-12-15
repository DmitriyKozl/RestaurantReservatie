using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RestaurantReservatie.BL.Models;

namespace RestaurantReservatie.DL.Models; 

public class Reservation_Data {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ReservationID { get; set; }

    public int  RestaurantId { get; set; }
    
    

    public Customer_Data Customer { get; set; }

    [Required]
    public int AmoutOfPeople { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required]
    public TimeSpan StartTime { get; set; }

    public int TableNumber { get; set; }
    
    public Restaurant_Data Restaurant { get; set; }
    
    public Reservation_Data() {
        
    }
}