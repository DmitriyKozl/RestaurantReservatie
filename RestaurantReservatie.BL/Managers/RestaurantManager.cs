using RestaurantReservatie.BL.Exceptions;
using RestaurantReservatie.BL.Interfaces;
using RestaurantReservatie.BL.Models;

namespace RestaurantReservatie.BL.Managers;

public class RestaurantManager {
    IRestaurantRepository _restaurantRepository;
    IReservationRepository _reservationRepository;

    public RestaurantManager(IRestaurantRepository restaurantRepository, IReservationRepository reservationRepository) {
        _restaurantRepository = restaurantRepository;
        _reservationRepository = reservationRepository;
    }

    public List<Restaurant> GetAllRestaurants() {
        return _restaurantRepository.GetAllRestaurants();
    }
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
    public List<Restaurant> GetRestaurants(string filter) {
        return _restaurantRepository.GetRestaurants(filter);
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
    public List<Restaurant> SearchRestaurants(string? city, string? cuisine) {
        var allRestaurants = _restaurantRepository.GetAllRestaurants();

        var filteredRestaurants = allRestaurants
            .Where(r => (city == null || r.Location.City == city) &&
                        (cuisine == null || r.Cuisine == cuisine))
            .ToList();

        return filteredRestaurants;
    }
    public bool RestaurantExists(int id) {
        try {
            if (id == null) throw new RestaurantManagerException("BestaatRestaurant - ID mag niet null zijn");
            return _restaurantRepository.RestaurantExists(id);
        }
        catch (Exception ex) {
            throw new RestaurantManagerException("BestaatRestaurant - Er is een fout opgetreden", ex);
        }
    }
    
    public List<Table> GetTables(int restaurantId) {
        try {
            if (restaurantId == 0)
                throw new RestaurantManagerException("GetTables - RestaurantId mag niet 0 zijn");
            if (!_restaurantRepository.RestaurantExists(restaurantId))
                throw new RestaurantManagerException("GetTables - Restaurant bestaat niet");
            return _restaurantRepository.GetTables(restaurantId);
        }
        catch (Exception ex) {
            throw new RestaurantManagerException("RestaurantManager get tables - Er is een fout opgetreden", ex);
        }
    }
}