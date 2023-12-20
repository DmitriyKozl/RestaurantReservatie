using RestaurantReservatie.BL.Models;
using RestaurantReservatie.Rest.Models.Output;

namespace RestaurantReservatie.Rest.Mappers;

public class ReservationMapper {
    public static ReservationOutputDTO MapFromDomain(
        Reservation reservation) {
        return new ReservationOutputDTO(
            reservation.Id,
            reservation.Restaurant,
            reservation.Customer,
            reservation.NumberOfPersons,
            reservation.Date,
            reservation.Time,
            reservation.TableNumber
        );
    }

    public static Reservation MapToDomain(
        ReservationOutputDTO reservation) {
        return new Reservation(
            reservation.Customer,
            reservation.Restaurant,
            reservation.Capacity,
            reservation.Date,
            reservation.Time,
            reservation.Tablenumber
        );
    }
}