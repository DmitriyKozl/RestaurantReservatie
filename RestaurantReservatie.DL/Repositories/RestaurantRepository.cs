using Microsoft.EntityFrameworkCore;
using RestaurantReservatie.BL.Exceptions;
using RestaurantReservatie.BL.Interfaces;
using RestaurantReservatie.BL.Models;
using RestaurantReservatie.DL.Data;
using RestaurantReservatie.DL.Mapper;
using RestaurantReservatie.DL.Models;

namespace RestaurantReservatie.DL.Repositories;

public class RestaurantRepository : IRestaurantRepository {
    private RestaurantReservatieContext _context;

    public RestaurantRepository(string connectionString) {
        _context = new RestaurantReservatieContext(connectionString);
    }

    public Restaurant AddRestaurant(Restaurant restaurant) {
        try {
            _context.Restaurant.Add(RestaurantMapper.MapToDB(restaurant, _context));
            _context.SaveChanges();
            return RestaurantMapper.MapToDomain(_context.Restaurant.OrderBy(r => r.RestaurantID).Last());
        }
        catch (Exception ex) {
            throw new RepositoryException("VoegRestaurantToe - Er is een fout opgetreden", ex);
        }
    }

    public List<Restaurant> GetRestaurants(string filter) {
        try {
            var restaurantEntities = _context.Restaurant
                .Include(r => r.Location)
                // .Include(r => r.Table)
                .Where(r => (string.IsNullOrEmpty(filter) || r.RestaurantName.Contains(filter)) && !r.Deleted)
                .ToList();

            return restaurantEntities.Select(r => RestaurantMapper.MapToDomain(r)).ToList();
        }
        catch (Exception ex) {
            throw new RepositoryException("GetRestaurants - Er is een fout opgetreden", ex);
        }
    }

    public List<Restaurant> GetAllRestaurants() {
        try {
            return _context.Restaurant
                .Include(r => r.Location)
                .Select(r => RestaurantMapper.MapToDomain(r))
                .ToList();
        }
        catch (Exception ex) {
            throw new RepositoryException("GetAllRestaurants - Er is een fout opgetreden", ex);
        }
    }

    public void DeleteRestaurant(int id) {
        try {
            Restaurant_Data r = _context.Restaurant.Find(id) ?? throw new InvalidOperationException();
            r.Deleted = true;
            _context.Restaurant.Update(r);
            _context.SaveChanges();
        }
        catch (Exception ex) {
            throw new RepositoryException("DeleteRestaurant - Er is een fout opgetreden", ex);
        }
    }

    public Restaurant UpdateRestaurant(Restaurant restaurant) {
        try {
            _context.Restaurant.Update(RestaurantMapper.MapToDB(restaurant, _context));
            _context.SaveChanges();
            return RestaurantMapper
                .MapToDomain(_context.Restaurant
                                 // .Include(r => r.Table)
                                 .FirstOrDefault(r => r.RestaurantID == restaurant.RestaurantId) ??
                             throw new RepositoryException("UpdateRestaurant - Er is een fout opgetreden"));
        }
        catch (Exception ex) {
            throw new RepositoryException("UpdateRestaurant - Er is een fout opgetreden", ex);
        }
    }

    public List<Restaurant> SearchRestaurants(Location? location, string? cuisine) {
        try {
            return _context.Restaurant
                .Where(r => location != null && r.Cuisine == cuisine &&
                            r.Location == LocationMapper.MapToDB(location, _context))
                .Select(r => RestaurantMapper.MapToDomain(r)).ToList();
        }
        catch (Exception ex) {
            throw new RepositoryException("SearchRestaurants - Er is een fout opgetreden");
        }
    }

    public List<Restaurant> GetRestaurantByCuisine(string cuisine) {
        try {
            return _context.Restaurant.Include(r => r.Location)
                // .Include(r => r.Table)
                .Where(r => r.Cuisine == cuisine && r.Deleted == false).Select(r => RestaurantMapper.MapToDomain(r))
                .ToList();
        }
        catch (Exception ex) {
            throw new RepositoryException("GetRestaurantByCuisine - Er is een fout opgetreden", ex);
        }
    }


    public List<Restaurant> GetRestaurantByLocation(string city) {
        try {
            return _context.Restaurant.Include(r => r.Location)
                .Where(r => r.Location.City == city && r.Deleted == false)
                .Select(r => RestaurantMapper.MapToDomain(r)).ToList();
        }
        catch (Exception ex) {
            throw new RepositoryException("GetRestaurantByLocation - Er is een fout opgetreden", ex);
        }
    }

    public List<Table> GetOpenTables(int restaurantId, DateTime date, DateTime time, int numberOfPersons) {
        try {
            // Define the time range for the reservation
            var startTime = date.AddMinutes(-90);
            var endTime = date.AddMinutes(90);

            return _context.Table
                .Where(t => t.RestaurantID == restaurantId && t.Capacity >= numberOfPersons)
                .OrderBy(t => t.Capacity)
                .ThenBy(t => t.ID)
                .Where(t => !_context.Reservation.Any(r =>
                    r.TableNumber == t.TableNumber &&
                    r.Date > startTime &&
                    r.Date < endTime))
                .Select(t => TableMapper.MapToDomain(t))
                .ToList(); // Changed from FirstOrDefault to ToList
        }
        catch (Exception ex) {
            throw new RepositoryException("GetOpenTables - Er is een fout opgetreden", ex);
        }
    }

    public List<Table> GetTables(int restaurantId) {
        try {
            return _context.Table
                .Where(t => t.RestaurantID == restaurantId)
                .Select(t => TableMapper.MapToDomain(t))
                .ToList();
        }
        catch (Exception ex) {
            throw new RepositoryException("GetTables: RestaurantRepository - Er is een fout opgetreden", ex);
        }
    }

    public bool RestaurantExists(int id) {
        try {
            return _context.Restaurant.Any(r => r.RestaurantID == id);
        }
        catch (Exception ex) {
            throw new RepositoryException("RestaurantExists - Er is een fout opgetreden", ex);
        }
    }
}