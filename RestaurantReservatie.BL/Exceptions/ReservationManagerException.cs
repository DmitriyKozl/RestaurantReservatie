namespace RestaurantReservatie.BL.Exceptions; 

public class ReservationManagerException : Exception {
    public ReservationManagerException(string message) : base(message) { }
    
    public ReservationManagerException(string message, Exception innerException) : base(message, innerException) { }
}