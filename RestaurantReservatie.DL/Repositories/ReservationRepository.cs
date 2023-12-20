using Microsoft.EntityFrameworkCore;
using RestaurantReservatie.BL.Exceptions;
using RestaurantReservatie.BL.Interfaces;
using RestaurantReservatie.BL.Models;
using RestaurantReservatie.DL.Data;
using RestaurantReservatie.DL.Mapper;
using RestaurantReservatie.DL.Models;

namespace RestaurantReservatie.DL.Repositories;

public class ReservationRepository : IReservationRepository {
    readonly RestaurantReservatieContext _context;

    public ReservationRepository(string connectionString) {
        _context = new RestaurantReservatieContext(connectionString);
    }

    public bool ReservationExists(int id) {
        try {
            return _context.Reservation.Any(r => r.Id == id);
        }
        catch (Exception ex) {
            throw new RepositoryException("ReservationExists - Er is een fout opgetreden", ex);
        }
    }

    public Reservation AddReservation(Reservation reservation) {
        try {
            _context.Reservation.Add(ReservationMapper
                .MapToDB(reservation, _context));
            _context.SaveChanges();
            return ReservationMapper
                .MapToDomain(_context.Reservation
                    .Include(r => r.RestaurantData.Table)
                    .Include(r => r.CustomerData)
                    .OrderBy(r => r.Id)
                    .Last());
        }
        catch (Exception ex) {
            throw new RepositoryException("AddReservation - Er is een fout opgetreden", ex);
        }
    }

    public List<Reservation> GetReservations() {
        try {
            return _context.Reservation.Select(r => ReservationMapper.MapToDomain(r)).ToList();
        }
        catch (Exception ex) {
            throw new RepositoryException("GetReservations - Er is een fout opgetreden", ex);
        }
    }

    public void DeleteReservation(int id) {
        try {
            Reservation_Data reservation = _context.Reservation.Find(id) ?? throw new InvalidOperationException();
            reservation.Deleted = true;
            _context.Reservation.Update(reservation);
            _context.SaveChanges();
        }
        catch (Exception ex) {
            throw new RepositoryException("VerwijderReservatie - Er is een fout opgetreden", ex);
        }
    }

    public List<Reservation> GetPersonalReservations(int customerId) {
        try {
            return _context.Reservation
                .Where(r => r.CustomerData.CustomerId == customerId)
                .Select(r => ReservationMapper
                    .MapToDomain(r))
                .ToList();
        }
        catch (Exception ex) {
            throw new RepositoryException("GetPersonalReservations - Er is een fout opgetreden", ex);
        }
    }


    public Reservation UpdateReservation(Reservation reservation) {
        try {
            _context.Reservation.Update(ReservationMapper.MapToDB(reservation, _context));
            _context.SaveChanges();
            return ReservationMapper
                .MapToDomain(_context.Reservation
                                 // .Include(r => r.RestaurantData.Table)
                                 .Include(r => r.CustomerData).ThenInclude(c => c.Location)
                                 .OrderBy(r => r.Id)
                                 .FirstOrDefault(r => r.Id == reservation.Id) ??
                             throw new RepositoryException("ReservationMapper - is null"));
        }
        catch (Exception ex) {
            throw new RepositoryException("UpdateReservation - Er is een fout opgetreden", ex);
        }
    }

    public List<Reservation> GetRestaurantReservations(int restaurantId) {
        try {
            return _context.Reservation
                .Include(r => r.RestaurantData)
                .ThenInclude(r => r.Location)
                .Include(r => r
                    .RestaurantData.Table)
                .Include(r => r.CustomerData)
                .ThenInclude(c => c.Location)
                .Where(r => r.RestaurantData.RestaurantID == restaurantId)
                .Select(r => ReservationMapper.MapToDomain(r))
                .ToList();
        }
        catch (Exception ex) {
            throw new RepositoryException("GetRestaurantReservations - Er is een fout opgetreden", ex);
        }
    }
}