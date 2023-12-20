using RestaurantReservatie.BL.Models;

namespace RestaurantReservatie.BL.Interfaces; 

public interface IReservationRepository {
    Reservation AddReservation(Reservation reservation);
    List<Reservation> GetReservations();
    void DeleteReservation(int id);
    
    List<Reservation> GetPersonalReservations(int customerId);
    Reservation UpdateReservation(Reservation reservation);
    
    List<Reservation> GetRestaurantReservations(int restaurantId);
    
    bool ReservationExists(int id);
    
    
}