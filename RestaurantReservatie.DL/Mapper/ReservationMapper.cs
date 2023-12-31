using RestaurantReservatie.BL.Exceptions;
using RestaurantReservatie.BL.Models;
using RestaurantReservatie.DL.Data;
using RestaurantReservatie.DL.Models;

namespace RestaurantReservatie.DL.Mapper;

public class ReservationMapper
{
    public static Reservation MapToDomain(Reservation_Data reservation)
    {
        try
        {
            return new Reservation(
                reservation.Id,
                CustomerMapper.MapToDomain(reservation.CustomerData),
                RestaurantMapper.MapToDomain(reservation.RestaurantData),
                reservation.Party,
                reservation.Date,
                reservation.Date.ToString("HH:mm"), // Extracting time part as a string
                reservation.TableNumber);
        }
        catch (Exception ex)
        {
            throw new MapperException("MapToDomain", ex);
        }
    }

    public static Reservation_Data MapToDB(Reservation reservation, RestaurantReservatieContext context)
    {
        try
        {
            Reservation_Data r = context.Reservation.Find(reservation.Id);

            if (r is not null)
            {
                r.Party = reservation.NumberOfPersons;
                r.Date = new DateTime(reservation.Date.Year, reservation.Date.Month, reservation.Date.Day,
                    reservation.Time.Hour, reservation.Time.Minute, reservation.Time.Second);
                r.CustomerData = CustomerMapper.MapToDB(reservation.Customer, context);
                r.RestaurantData = RestaurantMapper.MapToDB(reservation.Restaurant, context);
                r.TableNumber = reservation.TableNumber;
                return r;
            }

            return new Reservation_Data(RestaurantMapper
                    .MapToDB(reservation.Restaurant, context),
                CustomerMapper.MapToDB(reservation.Customer, context),
                reservation.NumberOfPersons,
                new DateTime(reservation.Date.Year, reservation.Date.Month, reservation.Date.Day,
                    reservation.Time.Hour, reservation.Time.Minute, reservation.Time.Second),
                reservation.TableNumber);
        }
        catch (Exception ex)
        {
            throw new MapperException("MapToDB", ex);
        }
    }
}