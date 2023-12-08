namespace RestaurantReservatie.BL.Exceptions; 

public class LocationException : Exception  {
    
    public LocationException() {
    }

    public LocationException(string message) : base(message) {
    }

    public LocationException(string message, Exception innerException) : base(message, innerException) {
    }
}