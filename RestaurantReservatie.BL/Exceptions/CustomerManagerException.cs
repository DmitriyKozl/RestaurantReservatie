﻿namespace RestaurantReservatie.BL.Exceptions; 

public class CustomerManagerException : Exception {
    
        
    
    public CustomerManagerException(string message) : base(message) {
    }

    public CustomerManagerException(string message, Exception innerException) : base(message, innerException) {
    }
    
}