namespace TestEFLayer.Output;

public class ReservationOutputDto
{
    
    public int ReservationId { get; set; }
    public int RestaurantId { get; set; }
    public int CustomerId { get; set; }
    public int Capacity { get; set; }
    public DateTime Date { get; set; }
    
    public DateTime Time { get; set; }
    
    public override string ToString()
    {
        return $"ReservationId: {ReservationId}, RestaurantId: {RestaurantId}, CustomerId: {CustomerId}, Capacity: {Capacity}, Date: {Date}, Time: {Time}";
    }
}