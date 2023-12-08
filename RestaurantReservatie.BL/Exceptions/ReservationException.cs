﻿namespace RestaurantReservatie.BL.Exceptions; 

public class ReservationException : Exception  {
    
    public ReservationException() : base() { }
    public ReservationException(string message) : base(message) { }
    public ReservationException(string message, Exception innerException) : base(message, innerException) { }
}