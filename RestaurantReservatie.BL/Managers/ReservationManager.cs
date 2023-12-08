using RestaurantReservatie.BL.Interfaces;
using RestaurantReservatie.BL.Models;

namespace RestaurantReservatie.BL.Managers; 

public class ReservationManager  {
    
    IReservationRepository _reservationRepository;
    
    public ReservationManager(IReservationRepository reservationRepository){
        _reservationRepository = reservationRepository;
    }
    
    public void AddReservation(Reservation reservation){
        _reservationRepository.AddReservation(reservation);
    }
    
    public List<Reservation> GetReservations(string filter){
        return _reservationRepository.GetReservations(filter);
    }
    
    public void DeleteReservation(int id){
        _reservationRepository.DeleteReservation(id);
    }
    
    public List<Reservation> GetPersonalReservations(int customerId, string? date){
        return _reservationRepository.GetPersonalReservations(customerId, date);
    }
    
    public void UpdateReservation(Reservation reservation){
        _reservationRepository.UpdateReservation(reservation);
    }
}