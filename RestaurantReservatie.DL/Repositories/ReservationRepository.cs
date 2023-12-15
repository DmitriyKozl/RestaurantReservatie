using RestaurantReservatie.BL.Interfaces;
using RestaurantReservatie.BL.Models;

namespace RestaurantReservatie.DL.Repositories; 

public class ReservationRepository : IReservationRepository {
    public void AddReservation(Reservation reservation) {
        throw new NotImplementedException();
    }

    public List<Reservation> GetReservations(string filter) {
        throw new NotImplementedException();
    }

    public void DeleteReservation(int id) {
        throw new NotImplementedException();
    }

    public List<Reservation> GetPersonalReservations(int customerId, string? date) {
        throw new NotImplementedException();
    }

    public void UpdateReservation(Reservation reservation) {
        throw new NotImplementedException();
    }
}