using RestaurantReservatie.BL.Models;

namespace RestaurantReservatie.BL.Interfaces;

public interface IReservationRepository {
    
    #region CRUD Reservations

    Reservation AddReservation(Reservation reservation);
    List<Reservation> GetReservations();
    Reservation UpdateReservation(Reservation reservation);

    void DeleteReservation(int id);

    #endregion
    
    #region Get reservations

    List<Reservation> GetPersonalReservations(Customer customer);

    Reservation GetReservation(int id);
    List<Reservation> GetRestaurantReservations(int restaurantId);
    List<Reservation> GetReservationByDate(DateTime? start, DateTime? end);
    List<Reservation> GetReservationFromCustomer(Customer customer);
    List<Reservation> GetReservationForCustomerWithDate(int id, DateTime? start, DateTime? end);
    List<Reservation> GetReservationFromCustomerWithDate(int id, DateTime? date);

    #endregion
    
    #region Booleans

    bool ReservationExists(int id);
    bool ReservationExists(Reservation reservation);

    bool ReservationIsTheSame(Reservation reservation);

    bool TableHasReservations(int tableNumber);

    #endregion
}