namespace RestaurantReservatie.BL.Exceptions; 

public class RestaurantManagerException : Exception {
    
    public RestaurantManagerException() : base() { }
    public RestaurantManagerException(string message) : base(message) { }
    public RestaurantManagerException(string message, Exception innerException) : base(message, innerException) { }
}