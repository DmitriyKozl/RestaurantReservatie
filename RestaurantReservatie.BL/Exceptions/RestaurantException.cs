namespace RestaurantReservatie.BL.Exceptions; 

public class RestaurantException : Exception{
    
    public RestaurantException() : base() { }
    public RestaurantException(string message) : base(message) { }
    public RestaurantException(string message, Exception innerException) : base(message, innerException) { }
    
}