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

    private void SaveAndClear() {
        _context.SaveChanges();
        _context.ChangeTracker.Clear();
    }

    public Reservation AddReservation(Reservation reservation) {
        try {
            _context.Reservation.Add(ReservationMapper.MapToDB(reservation, _context));
            _context.SaveChanges();
            return ReservationMapper.MapToDomain(_context.Reservation
                .Include(r => r.RestaurantData.Table)
                .Include(r => r.CustomerData)
                .OrderBy(r => r.Id).Last());
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

    public Reservation UpdateReservation(Reservation reservation) {
        try {
            _context.Reservation.Update(ReservationMapper.MapToDB(reservation, _context));
            _context.SaveChanges();
            return ReservationMapper
                .MapToDomain(_context.Reservation
                    .Include(r => r.RestaurantData.Table)
                    .Include(r => r.CustomerData)
                    .ThenInclude(c => c.Location)
                    .OrderBy(r => r.Id)
                    .Where(r => r.Id == reservation.Id)
                    .FirstOrDefault());
        }
        catch (Exception ex) {
            throw new RepositoryException("UpdateReservatie - Er is een fout opgetreden", ex);
        }
    }

    public void DeleteReservation(int id) {
        try {
            Reservation_Data reservationData = _context.Reservation.Find(id);
            reservationData.Deleted = true;
            _context.Reservation.Update(reservationData);
            _context.SaveChanges();
        }
        catch (Exception ex) {
            throw new RepositoryException("VerwijderReservatie - Er is een fout opgetreden", ex);
        }
    }
    public List<Reservation> GetPersonalReservations(Customer customer) {
        try {
            return _context.Reservation.Where(r => r.CustomerData.CustomerId == customer.CustomerId)
                .Select(r => ReservationMapper.MapToDomain(r)).ToList();
        }
        catch (Exception ex) {
            throw new RepositoryException("GeefReservatiesVanGebruiker - Er is een fout opgetreden", ex);
        }
    }
    public Reservation GetReservation(int id) {
        try {
            return ReservationMapper
                .MapToDomain(_context.Reservation
                .Include(r => r.RestaurantData)
                .ThenInclude(r => r.Location)
                .Include(r => r.RestaurantData.Table)
                .Include(r => r.CustomerData)
                .ThenInclude(c => c.Location)
                .Where(r => r.Id == id)
                .FirstOrDefault());
        }
        catch (Exception ex) {
            throw new RepositoryException("ReservationRepository: GeefReservatie - Er is een fout opgetreden", ex);
        }
    }
    public List<Reservation> GetRestaurantReservations(int restaurantId) {
        try {
            return _context.Reservation.Include(r => r.RestaurantData).ThenInclude(r => r.Location)
                .Include(r => r.RestaurantData.Table)
                .Include(r => r.CustomerData).ThenInclude(c => c.Location)
                .Where(r => r.RestaurantData.RestaurantID == restaurantId).Select(r => ReservationMapper.MapToDomain(r))
                .ToList();
        }
        catch (Exception ex) {
            throw new RepositoryException("GeefReservatiesVanRestaurant - Er is een fout opgetreden", ex);
        }
    }
    public List<Reservation> GetReservationByDate(DateTime? start, DateTime? end) {
        try {
            return _context.Reservation.Include(r => r.RestaurantData)
                .Include(r => r.RestaurantData)
                .Where(r => r.Date >= start.Value.Date &&
                            r.Date <= end.Value.Date).Select(r => ReservationMapper.MapToDomain(r))
                .ToList();
        }
        catch (Exception ex) {
            throw new RepositoryException("GetReservationByDate - Er is een fout opgetreden", ex);
        }
    }

    public List<Reservation> GetReservationFromCustomer(Customer customer) {
        try {
            return _context.Reservation.Where(r => r.CustomerData.CustomerId == customer.CustomerId)
                .Select(r => ReservationMapper.MapToDomain(r)).ToList();
        }
        catch (Exception ex) {
            throw new RepositoryException("GetReservationFromCustomer - Er is een fout opgetreden", ex);
        }
    }

    public List<Reservation> GetReservationForCustomerWithDate(int id, DateTime? start, DateTime? end) {
        try {
            return _context.Reservation
                .Include(r => r.RestaurantData)
                .ThenInclude(r => r.Location)
                .Include(r => r.RestaurantData.Table)
                .Include(r => r.CustomerData)
                .ThenInclude(c => c.Location)
                .Where(r => r.CustomerData.CustomerId == id && r.Date >= start &&
                            r.Date <= end)
                .Select(r => ReservationMapper.MapToDomain(r)).ToList();
        }
        catch (Exception) {
            throw new RepositoryException("GetReservationForCustomerWithDate - Er is een fout opgetreden");
        }
    }

    public List<Reservation> GetReservationFromCustomerWithDate(int id, DateTime? date) {
        try {
            return _context.Reservation.Include(r => r.RestaurantData)
                .ThenInclude(r => r.Location)
                .Include(r => r.RestaurantData.Table)
                .Include(r => r.CustomerData)
                .ThenInclude(c => c.Location)
                .Where(r => r.CustomerData.CustomerId == id && r.Date == date.Value.Date)
                .Select(r => ReservationMapper.MapToDomain(r)).ToList();
        }
        catch (Exception ex) {
            throw new RepositoryException("GetReservationFromCustomerWithDate - Er is een fout opgetreden", ex);
        }
    }

    public bool ReservationExists(int id) {
        try {
            return _context.Reservation.Any(r => r.Id == id);
        }
        catch (Exception ex) {
            throw new RepositoryException("ReservationExists - Er is een fout opgetreden", ex);
        }
    }

    public bool ReservationExists(Reservation reservation) {
        try {
            return _context.Reservation.Any(r =>
                r.Date == reservation.Date && r.TableNumber == reservation.TableNumber &&
                r.RestaurantData.RestaurantID == reservation.Restaurant.RestaurantId);
        }
        catch (Exception ex) {
            throw new RepositoryException("ReservationExists - Er is een fout opgetreden", ex);
        }
    }

    public bool ReservationIsTheSame(Reservation reservation) {
        try {
            return _context.Reservation.Any(r =>
                r.Id == reservation.Id && r.Date == reservation.Date &&
                r.RestaurantData.Table.Capacity == reservation.NumberOfPersons &&
                r.CustomerData.CustomerId == reservation.Customer.CustomerId &&
                r.RestaurantData.RestaurantID == reservation.Restaurant.RestaurantId);
        }
        catch (Exception ex) {
            throw new RepositoryException("ReservationIsTheSame - Er is een fout opgetreden", ex);
        }
    }

    public bool TableHasReservations(int tableNumber) {
        try {
            return _context.Reservation.Any(r => r.TableNumber == tableNumber);
        }
        catch (Exception ex) {
            throw new RepositoryException("TableHasReservations - Er is een fout opgetreden", ex);
        }
    }
}