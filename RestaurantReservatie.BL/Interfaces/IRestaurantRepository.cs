using RestaurantReservatie.BL.Models;

namespace RestaurantReservatie.BL.Interfaces; 

public interface IRestaurantRepository {
    
    Restaurant AddRestaurant(Restaurant restaurant);
    
    List<Restaurant> GetRestaurants(string filter);
    List<Restaurant> GetAllRestaurants();
    void DeleteRestaurant(int id);
    Restaurant UpdateRestaurant(Restaurant restaurant);
    List<Restaurant> SearchRestaurants(Location? location, string? cuisine);
    
    List<Restaurant> GetRestaurantByCuisine(string cuisine);
    List<Restaurant> GetRestaurantByLocation(string city);
    List<Table> GetOpenTables(int restaurantId, DateTime date, DateTime time, int numberOfPersons);
    
    List<Table> GetTables(int restaurantId);
    
    bool RestaurantExists(int id);
    
}