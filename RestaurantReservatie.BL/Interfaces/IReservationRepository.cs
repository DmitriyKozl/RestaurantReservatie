using RestaurantReservatie.BL.Models;

namespace RestaurantReservatie.BL.Interfaces; 

public interface IReservationRepository {
    void AddReservation(Reservation reservation);
    List<Reservation> GetReservations(string filter);
    void DeleteReservation(int id);
    
    List<Reservation> GetPersonalReservations(int customerId, string? date);
    void UpdateReservation(Reservation reservation);
}