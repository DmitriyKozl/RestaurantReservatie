using RestaurantReservatie.BL.Exceptions;
using RestaurantReservatie.BL.Interfaces;
using RestaurantReservatie.BL.Models;

namespace RestaurantReservatie.BL.Managers;

public class RestaurantManager {
    IRestaurantRepository  _restaurantRepository;
    IReservationRepository _reservationRepository;

    public RestaurantManager(IRestaurantRepository restaurantRepository, IReservationRepository reservationRepository) {
        _restaurantRepository = restaurantRepository;
        _reservationRepository = reservationRepository;
    }

    #region Restaurant

    public Restaurant AddRestaurant(Restaurant restaurant) {
        try {
            if (restaurant == null)
                throw new RestaurantManagerException("VoegRestaurantToe - Restaurant mag niet null zijn");
            if (_restaurantRepository.RestaurantExists(restaurant.RestaurantId))
                throw new RestaurantManagerException("VoegRestaurantToe - Restaurant bestaat al");
            return _restaurantRepository.AddRestaurant(restaurant);
        }
        catch (Exception ex) {
            throw new RestaurantManagerException("VoegRestaurantToe - Er is een fout opgetreden", ex);
        }
    }

    public Restaurant UpdateRestaurant(Restaurant restaurant) {
        try {
            if (restaurant == null)
                throw new RestaurantManagerException("UpdateRestaurant - Restaurant mag niet null zijn");
            if (!_restaurantRepository.RestaurantExists(restaurant.RestaurantId))
                throw new RestaurantManagerException("UpdateRestaurant - Restaurant bestaat niet");
            return _restaurantRepository.UpdateRestaurant(restaurant);
        }
        catch (Exception ex) {
            throw new RestaurantManagerException("UpdateRestaurant - Er is een fout opgetreden", ex);
        }
    }

    public void DeleteRestaurant(int id) {
        try {
            if (id == null)
                throw new RestaurantManagerException("VerwijderRestaurant - RestaurantId mag niet null zijn");
            if (!_restaurantRepository.RestaurantExists(id))
                throw new RestaurantManagerException("VerwijderRestaurant - Restaurant bestaat niet");
            _restaurantRepository.DeleteRestaurant(id);
        }
        catch (Exception ex) {
            throw new RestaurantManagerException("VerwijderRestaurant - Er is een fout opgetreden", ex);
        }
    }

    public bool RestaurantExists(int id) {
        try {
            if (id == null) throw new RestaurantManagerException("RestaurantExists - ID mag niet null zijn");
            return _restaurantRepository.RestaurantExists(id);
        }
        catch (Exception ex) {
            throw new RestaurantManagerException("RestaurantExists - Er is een fout opgetreden", ex);
        }
    }

    public Restaurant GetRestaurants(int id) {
        try {
            if (id == null) throw new RestaurantManagerException("GetRestaurant - ID mag niet null zijn");
            if (!_restaurantRepository.RestaurantExists(id))
                throw new RestaurantManagerException("GetRestaurant - Restaurant bestaat niet");
            return _restaurantRepository.GetRestaurants(id);
        }
        catch (Exception ex) {
            throw new RestaurantManagerException("RestaurantManager GetRestaurant - Er is een fout opgetreden", ex);
        }
    }

    public List<Table> GetTableByCapacity(int restaurantId, DateTime date, int capacity) {
        if (restaurantId == null) throw new RestaurantManagerException("GetTableByCapacity - Restaurant mag niet null zijn");
        if (capacity <= 0) throw new RestaurantManagerException("GetTableByCapacity - Aantal plaatsen moet groter zijn dan 0");
        return _restaurantRepository.GetTableByCapacity(restaurantId, date, capacity);
    }
    
    public List<Restaurant> GetRestaurantByCuisine(string cuisine) {
        try {
            if (cuisine == null)
                throw new RestaurantManagerException("GetRestaurantByCuisine - Keuken mag niet null zijn");
            return _restaurantRepository.GetRestaurantByCuisine(cuisine);
        }
        catch (Exception ex) {
            throw new RestaurantManagerException("GetRestaurantByCuisine - Er is een fout opgetreden", ex);
        }
    }
    
    public Restaurant GetRestaurantByName(string name) {
        try {
            if (name == null)
                throw new RestaurantManagerException("GetRestaurantByName - Naam mag niet null zijn");
            return _restaurantRepository.GetRestaurantByName(name);
        }
        catch (Exception ex) {
            throw new RestaurantManagerException("GetRestaurantByName - Er is een fout opgetreden", ex);
        }
    }

    #endregion


    #region Table

    public Table AddTable(Table table) {
        try {
            if (table == null)
                throw new RestaurantManagerException("AddTable - Tafel mag niet null zijn");
            if (!_restaurantRepository.RestaurantExists(table.RestaurantID))
                throw new RestaurantManagerException("AddTable - Restaurant bestaat niet");
            if (_restaurantRepository.TableExists(table))
                throw new RestaurantManagerException("AddTable - Tafel bestaat al");
            return _restaurantRepository.AddTable(table);
        }
        catch (Exception ex) {
            throw new RestaurantManagerException("AddTable - Er is een fout opgetreden", ex);
        }
    }

    public Table UpdateTable(int restaurantId, Table table) {
        try {
            if (table == null)
                throw new RestaurantManagerException("UpdateTable - Tafel mag niet null zijn");
            if (!_restaurantRepository.RestaurantExists(restaurantId))
                throw new RestaurantManagerException("UpdateTable - Restaurant bestaat niet");
            if (!_restaurantRepository.TableExists(table))
                throw new RestaurantManagerException("UpdateTable - Tafel bestaat niet");
            return _restaurantRepository.UpdateTable(restaurantId, table);
        }
        catch (Exception ex) {
            throw new RestaurantManagerException("UpdateTable - Er is een fout opgetreden", ex);
        }
    }

    public void DeleteTable(int restaurantId, Table table) {
        try {
            if (restaurantId == null)
                throw new RestaurantManagerException("DeleteTable - Restaurant mag niet null zijn");
            if (table == null) throw new RestaurantManagerException("DeleteTable - Tafel mag niet null zijn");
            if (!_restaurantRepository.TableExists(table))
                throw new RestaurantManagerException("DeleteTable - Restaurant bestaat niet");
            if (!_restaurantRepository.TableExists(table))
                throw new RestaurantManagerException("DeleteTable - Tafel bestaat niet");
            _restaurantRepository.DeleteTable(restaurantId, table.TableId);
        }
        catch (Exception ex) {
            throw new RestaurantManagerException("DeleteTable - Er is een fout opgetreden", ex);
        }
    }

    public Table GetTable(int restaurantId,int tableId) {
        if (tableId == null) throw new RestaurantManagerException("GetTable - Tafel mag niet null zijn");
        if (restaurantId == null) throw new RestaurantManagerException("GetTable - Restaurant mag niet null zijn");
        if (!_restaurantRepository.RestaurantExists(restaurantId))
            throw new RestaurantManagerException("GetTable - Restaurant bestaat niet");
        return _restaurantRepository.GetTable(tableId, restaurantId);
    }

    public bool TableExists(Table table) {
        if (table == null) throw new RestaurantManagerException("TableExists - Tafel mag niet null zijn");
        if (!_restaurantRepository.RestaurantExists(table.RestaurantID))
            throw new RestaurantManagerException("TableExists - Restaurant bestaat niet");
        return _restaurantRepository.TableExists(table);
    }
    

    #endregion

    #region Reservation

    public List<Reservation> GetReservationsByDate(int id, DateTime date) {
        try {
            if (id == null)
                throw new RestaurantManagerException("GetReservationsByDate - ID mag niet null zijn");
            if (!_restaurantRepository.RestaurantExists(id))
                throw new RestaurantManagerException("GetReservationsByDate - Restaurant bestaat niet");
            return _restaurantRepository.GetReservationsByDate(id, date);
        }
        catch (Exception ex) {
            throw new RestaurantManagerException("GetReservationsByDate - Er is een fout opgetreden", ex);
        }
    }

    #endregion
}