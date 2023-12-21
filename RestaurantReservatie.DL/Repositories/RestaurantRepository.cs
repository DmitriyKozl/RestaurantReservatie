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

    private void SaveAndClear() {
        _context.SaveChanges();
        _context.ChangeTracker.Clear();
    }

    public Restaurant AddRestaurant(Restaurant restaurant) {
        try {
            _context.Restaurant.Add(RestaurantMapper.MapToDB(restaurant, _context));
            _context.SaveChanges();
            return RestaurantMapper.MapToDomain(_context.Restaurant.OrderBy(r => r.RestaurantID).Last());
        }
        catch (Exception ex) {
            throw new RepositoryException("AddRestaurant - Er is een fout opgetreden", ex);
        }
    }

    public List<Restaurant> GetAllRestaurants() {
        try {
            return _context.Restaurant.Select(r => RestaurantMapper.MapToDomain(r)).ToList();
        }
        catch (Exception ex) {
            throw new RepositoryException("GetAllRestaurants - Er is een fout opgetreden", ex);
        }
    }

    public Restaurant GetRestaurants(int id) {
        try {
            return RestaurantMapper.MapToDomain(_context.Restaurant
                .Include(r => r.Location).Include(r => r.Table)
                .Where(r => r.RestaurantID == id && r.Deleted == false).FirstOrDefault());
        }
        catch (Exception ex) {
            throw new RepositoryException("GeefRestaurant - Er is een fout opgetreden", ex);
        }
    }

    public void DeleteRestaurant(int id) {
        try {
            Restaurant_Data r = _context.Restaurant.Find(id);
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
            return RestaurantMapper.MapToDomain(_context.Restaurant.Include(r => r.Table)
                .Where(r => r.RestaurantID == restaurant.RestaurantId).FirstOrDefault());
        }
        catch (Exception ex) {
            throw new RepositoryException("UpdateRestaurant - Er is een fout opgetreden", ex);
        }
    }

    public List<Restaurant> GetRestaurantByCuisine(string cuisine) {
        try {
            return _context.Restaurant.Include(r => r.Location).Include(r => r.Table)
                .Where(r => r.Cuisine == cuisine
                            && r.Deleted == false).Select(r => RestaurantMapper
                    .MapToDomain(r)).ToList();
        }
        catch (Exception ex) {
            throw new RepositoryException("GeefRestaurant - Er is een fout opgetreden", ex);
        }
    }

    public List<Restaurant> GetRestaurantByLocation(string city) {
        try {
            return _context.Restaurant.Include(r => r.Location).Include(r => r.Table)
                .Where(r => r.Location.City == city && r.Deleted == false).Select(r => RestaurantMapper
                    .MapToDomain(r)).ToList();
        }
        catch (Exception ex) {
            throw new RepositoryException("GeefRestaurant - Er is een fout opgetreden", ex);
        }
    }

    public bool RestaurantExists(int id) {
        try {
            return _context.Restaurant.Any(r => r.RestaurantID == id);
        }
        catch (Exception ex) {
            throw new RepositoryException("BestaatRestaurant - Er is een fout opgetreden", ex);
        }
    }

    public bool SameRestaurant(Restaurant restaurant) {
        try {
            return _context.Restaurant.Any(r =>
                r.RestaurantName == restaurant.RestaurantName && r.Location.StreetName == restaurant.Location.Street &&
                r.Email == restaurant.Email && r.PhoneNumber == restaurant.Phone &&
                r.Location.HouseNumber == restaurant.Location.HouseNumber &&
                r.Location.PostalCode == restaurant.Location.PostalCode);
        }
        catch (Exception ex) {
            throw new RepositoryException("IsDezelfde - Er is een fout opgetreden", ex);
        }
    }

    public Table AddTable(Table table) {
        try {
            _context.Table.Add(TableMapper.MapToDB(table, _context));
            _context.SaveChanges();
            return TableMapper.MapToDomain(_context.Table.OrderBy(t => t.ID).Last());
        }
        catch (Exception ex) {
            throw new RepositoryException("VoegTableToe - Er is een fout opgetreden", ex);
        }
    }

    public Table UpdateTable(int restaurantId, Table table) {
        try {
            Table_Data t = TableMapper.MapToDB(table, _context);
            _context.Table.Update(t);
            _context.SaveChanges();
            return TableMapper.MapToDomain(
                _context.Table.FirstOrDefault(table_data =>
                    table_data.ID == table.TableId && table_data.RestaurantID == restaurantId));
        }
        catch (Exception ex) {
            throw new RepositoryException("UpdateTable - Er is een fout opgetreden", ex);
        }
    }

    public Table DeleteTable(int restaurantId, int tableId) {
        try {
            Table_Data tableData = _context.Table
                .Where(table_data => table_data.ID == tableId && table_data.RestaurantID == restaurantId)
                .FirstOrDefault();
            tableData.Deleted = true;
            _context.Table.Update(tableData);
            _context.SaveChanges();

            return TableMapper.MapToDomain(tableData);
        }
        catch (Exception ex) {
            throw new RepositoryException("VerwijderTable - Er is een fout opgetreden", ex);
        }
    }

    public List<Table> GetOpenTables(DateTime datum, Location locatie, string cuisine
    ) {
        try {
            return _context.Table
                .Where(t => _context.Restaurant
                    .Any(r => r.Location == LocationMapper.MapToDB(locatie, _context) &&
                              r.Cuisine == cuisine) && _context.Reservation.Any(r => r.Date != datum))
                .Select(t => TableMapper.MapToDomain(t)).ToList();
        }
        catch (Exception ex) {
            throw new RepositoryException("GeefBeschikbareTafels - Er is een fout opgetreden", ex);
        }
    }

    public List<Table> GetTables(int restaurantId) {
        try {
            return _context.Table.Where(t => t.RestaurantID == restaurantId)
                .Select(t => TableMapper.MapToDomain(t))
                .ToList();
        }
        catch (Exception ex) {
            throw new RepositoryException("GeefTablesVanRestaurant - Er is een fout opgetreden", ex);
        }
    }

    public Table GetTable(int TableId, int restaurantId) {
        try {
            return TableMapper.MapToDomain(_context.Table.FirstOrDefault(Table =>
                Table.TableNumber == TableId && Table.RestaurantID == restaurantId));
        }
        catch (Exception ex) {
            throw new RepositoryException("GetTable - Er is een fout opgetreden", ex);
        }
    }

    public List<Table> GetRestaurantTables(Restaurant restaurant) {
        try {
            return _context.Table.Where(t => t.RestaurantID == restaurant.RestaurantId)
                .Select(t => TableMapper.MapToDomain(t))
                .ToList();
        }
        catch (Exception ex) {
            throw new RepositoryException("GetRestaurantTables - Er is een fout opgetreden", ex);
        }
    }

    public List<Table> GetRestaurantOpenTables(Restaurant restaurant) {
        try {
            return _context.Table.Where(t => t.RestaurantID == restaurant.RestaurantId)
                .Select(t => TableMapper.MapToDomain(t))
                .ToList();
        }
        catch (Exception ex) {
            throw new RepositoryException("GetRestaurantOpenTables - Er is een fout opgetreden", ex);
        }
    }

    public List<Table> GetTableByCapacity(int restaurantId, DateTime date, int capacity) {
        try {
            return _context.Table.Where(t => t.RestaurantID == restaurantId && t.Capacity >= capacity)
                    .OrderBy(t => t.Capacity)
                    .ThenBy(t => t.ID)
                    .Where(t => !_context.Reservation.Any(r =>
                        r.TableNumber == t.TableNumber && r.Date < date.AddMinutes(90) &&
                        r.Date > date.AddMinutes(-90)))
                    .Select(t => TableMapper.MapToDomain(t)).ToList()
                ;
        }
        catch (Exception ex) {
            throw new RepositoryException("GetTableByCapacity - Er is een fout opgetreden", ex);
        }
    }

    public bool TableExists(Table table) {
        try {
            return _context.Table.Any(t => t.TableNumber == table.TableNumber && t.RestaurantID == table.RestaurantID);
        }
        catch (Exception ex) {
            throw new RepositoryException("TableExists - Er is een fout opgetreden", ex);
        }
    }

    public bool SameTable(Table table) {
        try {
            return _context.Table.Where(t => t.ID == table.TableId && t.RestaurantID == table.RestaurantID)
                .Any(t => t.Capacity == table.Chairs);
        }
        catch (Exception ex) {
            throw new RepositoryException("SameTable - Er is een fout opgetreden", ex);
        }
    }

    public List<Reservation> GetReservationsByDate(int id, DateTime date) {
        try {
            return _context.Reservation
                .Where(r => r.RestaurantData.RestaurantID == id && r.Date.Date == date.Date)
                .Include(r => r.RestaurantData).ThenInclude(r => r.Location)
                .Include(r => r.RestaurantData.Table)
                .Include(r => r.CustomerData).ThenInclude(c => c.Location)
                .Select(r => ReservationMapper
                    .MapToDomain(r))
                .ToList();
        }
        catch (Exception ex) {
            throw new RepositoryException("GeefREservatiesOpDatum - Er is een fout opgetreden", ex);
        }
    }

    public List<Restaurant> GetRestaurantsAvailabilityByDate(DateTime date, int numSeats) {
        try {
            return _context.Restaurant.Include(r => r.Location).Include(r => r.Table)
                .Where(r =>
                    r.Deleted == false && r.Table.Any(t =>
                        t.Capacity >= numSeats && !_context.Reservation.Any(re =>
                            re.TableNumber == t.TableNumber && re.Date == date))).Select(r => RestaurantMapper
                    .MapToDomain(r)).ToList();
        }
        catch (Exception ex) {
            throw new RepositoryException("GeefRestaurantsOpDatum - Er is een fout opgetreden", ex);
        }
    }
}