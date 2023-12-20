namespace RestaurantReservatie.Rest.Models.Input; 

public class ReseervationInputDTO {
    public int RestaurantId { get; set; }
    public int CustomerId { get; set; }
    public int Capacity { get; set; }
    public DateTime Date { get; set; }
    
}